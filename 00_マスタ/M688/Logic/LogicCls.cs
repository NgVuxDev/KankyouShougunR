using System;
using System.ComponentModel;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Master.SystemKobetsuSetteiHoshu.APP;
using Shougun.Core.Master.SystemKobetsuSetteiHoshu.DAO;

using Shougun.Core.Common.BusinessCommon.Xml; //参照設定にそもそも追加しないとだめ。

namespace Shougun.Core.Master.SystemKobetsuSetteiHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        public MasterBaseForm parentForm;

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.SystemKobetsuSetteiHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_KYOTEN_DATA_SQL = "Shougun.Core.Master.SystemKobetsuSetteiHoshu.Sql.GetKyotenDataSql.sql";
                                                     
        private const string KYOTEN_CD = "拠点CD";

        private const string NIOROSHI_GYOUSHA_CD = "荷降業者CD";

        private const string NIOROSHI_GENBA_CD = "荷降現場CD";

        private const string NITSUMI_GYOUSHA_CD = "荷積業者CD";

        private const string NITSUMI_GENBA_CD = "荷積現場CD";

        private const string RYOUSHUUSHO = "領収書";

        private const string KEIZOKU_NYUURYOKU = "継続入力";

        private const string ENDDATE_USE_KBN_KOBETU = "終了日警告";

        private const string ITAKU_KEIYAKU_ALERT = "契約アラート";

        private const string CASHER_LINK = "キャッシャ連動";

        private const string UPDATE_CSV_OUTPUT = "更新CSV出力";
        private const string MASTER_TANKA_UPDATE = "マスタ単価更新";
        private const string CSV_OUTPUT_FILE_PATH = "CSV保管先";
        private const string DISPLAY_SIZE = "画面表示サイズ";
        private const string FILE_UPLOAD_PATH = "ファイルアップロード参照先";

		//PhuocLoc 2020/04/03 #134980 -Start
        private const string UR_SH_SEIKYUU_PRINT_KBN = "売上支払請求伝票";
        private const string UR_SH_SHIHARAI_PRINT_KBN = "売上支払支払伝票";
        private const string UR_SH_DENPYOU_PRINT_KBN = "売上支払仕切書出力区分";

        private const string SHUKKA_SEIKYUU_PRINT_KBN = "出荷請求伝票";
        private const string SHUKKA_SHIHARAI_PRINT_KBN = "出荷支払伝票";
        private const string SHUKKA_DENPYOU_PRINT_KBN = "出荷仕切書出力区分";
        private const string SHUKKA_KEIRYOU_PRINT_KBN = "出荷計量票出力区分";

        private const string UKEIRE_SEIKYUU_PRINT_KBN = "受入請求伝票";
        private const string UKEIRE_SHIHARAI_PRINT_KBN = "受入支払伝票";
        private const string UKEIRE_DENPYOU_PRINT_KBN = "受入仕切書出力区分";
        private const string UKEIRE_KEIRYOU_PRIRNT_KBN = "受入計量票出力区分";
		//PhuocLoc 2020/04/03 #134980 -End

        private const string TAIRYU_TAB_HYOUJI_KBN = "滞留タブ表示"; //PhuocLoc 2020/12/01 #136217

        private const string DENSHI_KEIYAKU_SHANAI_KEIRO = "電子契約社内経路";
        private const string DENSHI_KEIYAKU_SHANAI_KEIRO_CD = "電子契約社内経路CD";

        private const string GREETINGS = "挨拶文";
        private const string SIGNATURE = "署名";

        //20250306
        private const string NITTEI_HOKOKU_KBN = "日程報告書現場住所印字";

        private const string TORIKOMI_FORUDA = "取込フォルダ";
        private const string HOKAN_FORUDA = "保管フォルダ";
        private const string TOROKOMI_KANKAKU = "取込間隔";
        private const string CTI_RENKEI_FORUDA = "CTI連携フォルダ";
        private const string CTI_RENKEI_KANKAKU = "CTI連携間隔";

        #endregion

        /// <summary>
        /// 拠点のDao
        /// </summary>
        private DAOCls SystemKobetsuDaoCls;

        /// <summary>
        /// 社内経路名Dao
        /// </summary>
        private IM_DENSHI_KEIYAKU_SHANAI_KEIRO_NAMEDao shanaiKeiroNameDao;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.SystemKobetsuDaoCls = DaoInitUtility.GetComponent<DAOCls>();
            this.shanaiKeiroNameDao = DaoInitUtility.GetComponent<IM_DENSHI_KEIYAKU_SHANAI_KEIRO_NAMEDao>();

            LogUtility.DebugMethodEnd();
        }

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();    //保留

                // イベントの初期化処理
                this.EventInit();

                // 処理No（ESC)を入力不可にする
                this.parentForm.txb_process.Enabled = false;

                // システム情報を取得し、初期値をセットする
                this.GetSysInfoInit();

                //this.form.KYOTEN_CD.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion
        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (MasterBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (MasterBaseForm)this.form.Parent;

                //登録ボタン(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
                this.form.NIOROSHI_GYOUSHA_CD.Enter += new EventHandler(this.form.NIOROSHI_GYOUSHA_CD_Enter);
                this.form.NITSUMI_GYOUSHA_CD.Enter += new EventHandler(this.form.NITSUMI_GYOUSHA_CD_Enter);

                this.form.btnFileRef.Click += new EventHandler(this.form.FileRefClick);
                this.form.btnFileUploadRef.Click += new EventHandler(this.form.FileUploadRefClick);

                //20250306
                this.form.btnFileRef_1.Click += new EventHandler(this.form.FileRefClick_1);
                this.form.btnFileRef_2.Click += new EventHandler(this.form.FileRefClick_2);
                this.form.btnFileRef_3.Click += new EventHandler(this.form.FileRefClick_3);

            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        #region システム情報を取得し、初期値をセットする
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 設定データ取得
                GetStatus();

            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual bool RegistData(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            //独自チェックの記述例を書く
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    SetStatus();　
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion
        #region 取消処理
        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit(); 
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region ステータス取得
        /// <summary>
        /// ステータス取得
        /// </summary>
        public bool GetStatus()
        {
            try
            {
                // 個別タブ
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();

                this.form.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, KYOTEN_CD);
                if (this.form.KYOTEN_CD.Text.Length == 0)
                {
                    // 初期値：(00)本社
                    this.form.KYOTEN_CD.Text = "00";
                }
                M_KYOTEN kyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>().GetDataByCd(this.form.KYOTEN_CD.Text);
                if (kyoten != null)
                {
                    this.form.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU;
                }

                this.form.ENDDATE_USE_KBN_KOBETU.Text = this.GetUserProfileValue(userProfile, ENDDATE_USE_KBN_KOBETU);
                if (this.form.ENDDATE_USE_KBN_KOBETU.Text.Length == 0)
                {
                    this.form.ENDDATE_USE_KBN_KOBETU.Text = "2";
                }

                this.form.ITAKU_KEIYAKU_ALERT.Text = this.GetUserProfileValue(userProfile, ITAKU_KEIYAKU_ALERT);
                if (this.form.ITAKU_KEIYAKU_ALERT.Text.Length == 0)
                {
                    this.form.ITAKU_KEIYAKU_ALERT.Text = "2";
                }

                this.form.CASHER_LINK.Text = this.GetUserProfileValue(userProfile, CASHER_LINK);
                if (this.form.CASHER_LINK.Text.Length == 0)
                {
                    this.form.CASHER_LINK.Text = "2";
                }

                this.form.UPDATE_CSV_OUTPUT.Text = this.GetUserProfileValue(userProfile, UPDATE_CSV_OUTPUT);
                if (this.form.UPDATE_CSV_OUTPUT.Text.Length == 0)
                {
                    this.form.UPDATE_CSV_OUTPUT.Text = "2";
                }
                this.form.MASTER_TANKA_UPDATE.Text = this.GetUserProfileValue(userProfile, MASTER_TANKA_UPDATE);
                if (this.form.MASTER_TANKA_UPDATE.Text.Length == 0)
                {
                    this.form.MASTER_TANKA_UPDATE.Text = "2";
                }
                this.form.CSV_OUTPUT_FILE_PATH.Text = this.GetUserProfileValue(userProfile, CSV_OUTPUT_FILE_PATH);

                this.form.FILE_UPLOAD_PATH.Text = this.GetUserProfileValue(userProfile, FILE_UPLOAD_PATH);

                this.form.NIOROSHI_GYOUSHA_CD.Text = this.GetUserProfileValue(userProfile, NIOROSHI_GYOUSHA_CD);
                this.form.beforNioroshiGyousaCD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                M_GYOUSHA gyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(this.form.NIOROSHI_GYOUSHA_CD.Text);
                if (gyousha != null)
                {
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }

                this.form.NIOROSHI_GENBA_CD.Text = this.GetUserProfileValue(userProfile, NIOROSHI_GENBA_CD);
                M_GENBA condGenba = new M_GENBA();
                condGenba.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                condGenba.GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
                M_GENBA genba = DaoInitUtility.GetComponent<IM_GENBADao>().GetDataByCd(condGenba);
                if (genba != null)
                {
                    this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                }

                this.form.NITSUMI_GYOUSHA_CD.Text = this.GetUserProfileValue(userProfile, NITSUMI_GYOUSHA_CD);
                this.form.beforNitsumiGyousaCD = this.form.NITSUMI_GYOUSHA_CD.Text;
                gyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(this.form.NITSUMI_GYOUSHA_CD.Text);
                if (gyousha != null)
                {
                    this.form.NITSUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }

                this.form.NITSUMI_GENBA_CD.Text = this.GetUserProfileValue(userProfile, NITSUMI_GENBA_CD);
                condGenba = new M_GENBA();
                condGenba.GYOUSHA_CD = this.form.NITSUMI_GYOUSHA_CD.Text;
                condGenba.GENBA_CD = this.form.NITSUMI_GENBA_CD.Text;
                genba = DaoInitUtility.GetComponent<IM_GENBADao>().GetDataByCd(condGenba);
                if (genba != null)
                {
                    this.form.NITSUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                }

                var ryoushuusho = this.GetUserProfileValue(userProfile, RYOUSHUUSHO);
                if (String.IsNullOrEmpty(ryoushuusho))
                {
                    this.form.RYOUSHUUSHO.Text = "2";
                }
                else
                {
                    this.form.RYOUSHUUSHO.Text = ryoushuusho;
                }

                var keizokuNyuuryoku = this.GetUserProfileValue(userProfile, KEIZOKU_NYUURYOKU);
                if (String.IsNullOrEmpty(keizokuNyuuryoku))
                {
                    this.form.KEIZOKU_NYUURYOKU.Text = "2";
                }
                else
                {
                    this.form.KEIZOKU_NYUURYOKU.Text = keizokuNyuuryoku;
                }

                this.form.DISPLAY_SIZE.Text = this.GetUserProfileValue(userProfile, DISPLAY_SIZE);
                if (this.form.DISPLAY_SIZE.Text.Length == 0)
                {
                    this.form.DISPLAY_SIZE.Text = "1";
                }

				//PhuocLoc 2020/04/03 #134980 -Start
                this.form.UKEIRE_SEIKYUU_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, UKEIRE_SEIKYUU_PRINT_KBN);
                if (this.form.UKEIRE_SEIKYUU_PRINT_KBN.Text.Length == 0)
                {
                    this.form.UKEIRE_SEIKYUU_PRINT_KBN.Text = "2";
                }

                this.form.UKEIRE_SHIHARAI_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, UKEIRE_SHIHARAI_PRINT_KBN);
                if (this.form.UKEIRE_SHIHARAI_PRINT_KBN.Text.Length == 0)
                {
                    this.form.UKEIRE_SHIHARAI_PRINT_KBN.Text = "2";
                }

                this.form.UKEIRE_DENPYOU_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, UKEIRE_DENPYOU_PRINT_KBN);
                if (this.form.UKEIRE_DENPYOU_PRINT_KBN.Text.Length == 0)
                {
                    this.form.UKEIRE_DENPYOU_PRINT_KBN.Text = "1";
                }

                this.form.UKEIRE_KEIRYOU_PRIRNT_KBN.Text = this.GetUserProfileValue(userProfile, UKEIRE_KEIRYOU_PRIRNT_KBN);
                if (this.form.UKEIRE_KEIRYOU_PRIRNT_KBN.Text.Length == 0)
                {
                    this.form.UKEIRE_KEIRYOU_PRIRNT_KBN.Text = "2";
                }

                this.form.SHUKKA_SEIKYUU_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, SHUKKA_SEIKYUU_PRINT_KBN);
                if (this.form.SHUKKA_SEIKYUU_PRINT_KBN.Text.Length == 0)
                {
                    this.form.SHUKKA_SEIKYUU_PRINT_KBN.Text = "2";
                }

                this.form.SHUKKA_SHIHARAI_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, SHUKKA_SHIHARAI_PRINT_KBN);
                if (this.form.SHUKKA_SHIHARAI_PRINT_KBN.Text.Length == 0)
                {
                    this.form.SHUKKA_SHIHARAI_PRINT_KBN.Text = "2";
                }

                this.form.SHUKKA_DENPYOU_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, SHUKKA_DENPYOU_PRINT_KBN);
                if (this.form.SHUKKA_DENPYOU_PRINT_KBN.Text.Length == 0)
                {
                    this.form.SHUKKA_DENPYOU_PRINT_KBN.Text = "1";
                }

                this.form.SHUKKA_KEIRYOU_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, SHUKKA_KEIRYOU_PRINT_KBN);
                if (this.form.SHUKKA_KEIRYOU_PRINT_KBN.Text.Length == 0)
                {
                    this.form.SHUKKA_KEIRYOU_PRINT_KBN.Text = "2";
                }

                this.form.UR_SH_SEIKYUU_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, UR_SH_SEIKYUU_PRINT_KBN);
                if (this.form.UR_SH_SEIKYUU_PRINT_KBN.Text.Length == 0)
                {
                    this.form.UR_SH_SEIKYUU_PRINT_KBN.Text = "2";
                }

                this.form.UR_SH_SHIHARAI_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, UR_SH_SHIHARAI_PRINT_KBN);
                if (this.form.UR_SH_SHIHARAI_PRINT_KBN.Text.Length == 0)
                {
                    this.form.UR_SH_SHIHARAI_PRINT_KBN.Text = "2";
                }

                this.form.UR_SH_DENPYOU_PRINT_KBN.Text = this.GetUserProfileValue(userProfile, UR_SH_DENPYOU_PRINT_KBN);
                if (this.form.UR_SH_DENPYOU_PRINT_KBN.Text.Length == 0)
                {
                    this.form.UR_SH_DENPYOU_PRINT_KBN.Text = "1";
                }
				//PhuocLoc 2020/04/03 #134980 -End

                //PhuocLoc 2020/12/01 #136217 -Start
                this.form.TAIRYU_TAB_HYOUJI_KBN.Text = this.GetUserProfileValue(userProfile, TAIRYU_TAB_HYOUJI_KBN);
                if (this.form.TAIRYU_TAB_HYOUJI_KBN.Text.Length == 0)
                {
                    this.form.TAIRYU_TAB_HYOUJI_KBN.Text = "1";
                }
                //PhuocLoc 2020/12/01 #136217 -End

                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO.Text = this.GetUserProfileValue(userProfile, DENSHI_KEIYAKU_SHANAI_KEIRO);
                if (this.form.DENSHI_KEIYAKU_SHANAI_KEIRO.Text.Length == 0)
                {
                    this.form.DENSHI_KEIYAKU_SHANAI_KEIRO.Text = "2";
                }
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text = this.GetUserProfileValue(userProfile, DENSHI_KEIYAKU_SHANAI_KEIRO_CD);
                if (this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text.Length == 0)
                {
                    this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text = string.Empty;
                }
                else
                {
                    M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME param = new M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME();
                    param.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = Int16.Parse(GetUserProfileValue(userProfile, DENSHI_KEIYAKU_SHANAI_KEIRO_CD));
                    M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME[] keiroName = this.shanaiKeiroNameDao.GetAllValidData(param);
                    if (keiroName != null && keiroName.Length > 0)
                    {
                        this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = keiroName[0].DENSHI_KEIYAKU_SHANAI_KEIRO_NAME;
                    }
                }
                // 挨拶文、署名の改行文字を「\r\n」に変更し、改行文字を認識させる
                string[] gre = this.GetUserProfileValue(userProfile, GREETINGS).Split('\n');
                for(int i = 0; i < gre.Length; i++)
                {
                    if (gre.Length - i != 1)
                    {
                        this.form.GREETINGS.Text += gre[i] + "\r\n";
                    }
                    else
                    {
                        this.form.GREETINGS.Text += gre[i];
                    }
                }
                string[] sig = this.GetUserProfileValue(userProfile, SIGNATURE).Split('\n');
                for(int i = 0; i < sig.Length; i++)
                {
                    if (sig.Length - i != 1)
                    {
                        this.form.SIGNATURE.Text += sig[i] + "\r\n";
                    }
                    else
                    {
                        this.form.SIGNATURE.Text += sig[i];
                    }
                }

                #region 20250306
                //ID 99999
                this.form.NITTEI_HOKOKU_KBN.Text = this.GetUserProfileValue(userProfile, NITTEI_HOKOKU_KBN);
                if (this.form.NITTEI_HOKOKU_KBN.Text.Length == 0)
                {
                    this.form.NITTEI_HOKOKU_KBN.Text = "2";
                }

                this.form.TORIKOMI_FORUDA.Text = this.GetUserProfileValue(userProfile, TORIKOMI_FORUDA);
                this.form.HOKAN_FORUDA.Text = this.GetUserProfileValue(userProfile, HOKAN_FORUDA);
                this.form.TOROKOMI_KANKAKU.Text = this.GetUserProfileValue(userProfile, TOROKOMI_KANKAKU);
                if(this.form.TOROKOMI_KANKAKU.Text.Length == 0)
                {
                    this.form.TOROKOMI_KANKAKU.Text = "0";
                }

                this.form.CTI_RENKEI_FORUDA.Text = this.GetUserProfileValue(userProfile, CTI_RENKEI_FORUDA);
                this.form.CTI_RENKEI_KANKAKU.Text = this.GetUserProfileValue(userProfile, CTI_RENKEI_KANKAKU);
                if(this.form.CTI_RENKEI_KANKAKU.Text.Length == 0)
                {
                    this.form.CTI_RENKEI_KANKAKU.Text = "0";
                }

                #endregion 20250306

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetStatus", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }
        
        #endregion
        #region ステータス保存
        /// <summary>
        /// /// ステータス保存
        /// </summary>
        public void SetStatus()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.CurrentUserCustomConfigProfileWrite(
                           this.form.KYOTEN_CD.Text
                           , this.form.NIOROSHI_GYOUSHA_CD.Text
                           , this.form.NIOROSHI_GENBA_CD.Text
                           , this.form.NITSUMI_GYOUSHA_CD.Text
                           , this.form.NITSUMI_GENBA_CD.Text
                           , this.form.RYOUSHUUSHO.Text
                           , this.form.KEIZOKU_NYUURYOKU.Text
                           , this.form.ENDDATE_USE_KBN_KOBETU.Text
                           , this.form.ITAKU_KEIYAKU_ALERT.Text
                           , this.form.CASHER_LINK.Text
                           , this.form.UPDATE_CSV_OUTPUT.Text
                           , this.form.MASTER_TANKA_UPDATE.Text
                           , this.form.CSV_OUTPUT_FILE_PATH.Text
                           , this.form.DISPLAY_SIZE.Text
                           //PhuocLoc 2020/04/03 #134980 -Start
                           , this.form.UKEIRE_SEIKYUU_PRINT_KBN.Text
                           , this.form.UKEIRE_SHIHARAI_PRINT_KBN.Text
                           , this.form.UKEIRE_DENPYOU_PRINT_KBN.Text
                           , this.form.UKEIRE_KEIRYOU_PRIRNT_KBN.Text
                           , this.form.SHUKKA_SEIKYUU_PRINT_KBN.Text
                           , this.form.SHUKKA_SHIHARAI_PRINT_KBN.Text
                           , this.form.SHUKKA_DENPYOU_PRINT_KBN.Text
                           , this.form.SHUKKA_KEIRYOU_PRINT_KBN.Text
                           , this.form.UR_SH_SEIKYUU_PRINT_KBN.Text
                           , this.form.UR_SH_SHIHARAI_PRINT_KBN.Text
                           , this.form.UR_SH_DENPYOU_PRINT_KBN.Text
                           //PhuocLoc 2020/04/03 #134980 -End
                           , this.form.DENSHI_KEIYAKU_SHANAI_KEIRO.Text
                           , this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text
                           , this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text
                           , this.form.FILE_UPLOAD_PATH.Text
                           , this.form.TAIRYU_TAB_HYOUJI_KBN.Text //PhuocLoc 2020/12/01 #136217
                           , this.form.GREETINGS.Text
                           , this.form.SIGNATURE.Text

                           //20250306
                           , this.form.NITTEI_HOKOKU_KBN.Text
                           , this.form.TORIKOMI_FORUDA.Text
                           , this.form.HOKAN_FORUDA.Text
                           , this.form.TOROKOMI_KANKAKU.Text
                           , this.form.CTI_RENKEI_FORUDA.Text
                           , this.form.CTI_RENKEI_KANKAKU.Text
                       );
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetStatus", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// CurrentUserCustomConfigProfile書き込み
        /// </summary>
        /// <param name="kyotenCd"></param>
        /// <param name="nioroshiGyoushaCd"></param>
        /// <param name="nioroshiGenbaCd"></param>
        /// <param name="nitsumiGyoushaCd"></param>
        /// <param name="nitsumiGenbaCd"></param>
        /// <param name="ryoushuusho"></param>
        private void CurrentUserCustomConfigProfileWrite(string kyotenCd, string nioroshiGyoushaCd, string nioroshiGenbaCd, string nitsumiGyoushaCd, string nitsumiGenbaCd, string ryoushuusho, string keizokuNyuuryoku, string enddate_use_kbn_kobetu, string itaku_keiyaku_alert, string casher_link, string update_csv_output, string master_tanka_update, string csv_output_file_path, string display_size
            , string Ukeire_Seikyuu_Print_Kbn, string Ukeire_Shiharai_Print_Kbn, string Ukeire_Denpyou_Print_Kbn, string Ukeire_Keiryou_Prirnt_Kbn, string Shukka_Seikyuu_Print_Kbn, string Shukka_Shiharai_Print_Kbn, string Shukka_Denpyou_Print_Kbn, string Shukka_Keiryou_Print_Kbn, string Ur_Sh_Seikyuu_Print_Kbn, string Ur_Sh_Shiharai_Print_Kbn, string Ur_Sh_Denpyou_Print_Kbn, string Denshi_Keiyaku_Shanai_Keiro, string Denshi_Keiyaku_Shanai_Keiro_Cd,string Denshi_Keiyaku_Shanai_Keiro_Name,string file_upload_path, string tairyu_tab_kbn, string greetings, string signature //PhuocLoc 2020/04/03 #134980
            , string nittei_hokuko_kbn, string torikomi_foruda, string hokan_foruda, string torokomi_kankaku, string cti_renkei_foruda, string cti_renkei_kankaku)				
        {
            LogUtility.DebugMethodStart(kyotenCd, nioroshiGyoushaCd, nioroshiGenbaCd, nitsumiGyoushaCd, nitsumiGenbaCd, ryoushuusho, keizokuNyuuryoku, enddate_use_kbn_kobetu, itaku_keiyaku_alert, casher_link, update_csv_output, master_tanka_update, csv_output_file_path, display_size, Ukeire_Seikyuu_Print_Kbn, Ukeire_Shiharai_Print_Kbn, Ukeire_Denpyou_Print_Kbn, Ukeire_Keiryou_Prirnt_Kbn, Shukka_Seikyuu_Print_Kbn, Shukka_Shiharai_Print_Kbn, Shukka_Denpyou_Print_Kbn, Shukka_Keiryou_Print_Kbn, Ur_Sh_Seikyuu_Print_Kbn, Ur_Sh_Shiharai_Print_Kbn, Ur_Sh_Denpyou_Print_Kbn, Denshi_Keiyaku_Shanai_Keiro,Denshi_Keiyaku_Shanai_Keiro_Cd,Denshi_Keiyaku_Shanai_Keiro_Name, file_upload_path, tairyu_tab_kbn, greetings, signature //PhuocLoc 2020/04/03 #134980
                                        , nittei_hokuko_kbn, torikomi_foruda, hokan_foruda, torokomi_kankaku, cti_renkei_foruda, cti_renkei_kankaku); 

            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.SetUserProfileValue(userProfile, KYOTEN_CD, kyotenCd);
            this.SetUserProfileValue(userProfile, NIOROSHI_GYOUSHA_CD, nioroshiGyoushaCd);
            this.SetUserProfileValue(userProfile, NIOROSHI_GENBA_CD, nioroshiGenbaCd);
            this.SetUserProfileValue(userProfile, NITSUMI_GYOUSHA_CD, nitsumiGyoushaCd);
            this.SetUserProfileValue(userProfile, NITSUMI_GENBA_CD, nitsumiGenbaCd);
            this.SetUserProfileValue(userProfile, RYOUSHUUSHO, ryoushuusho);
            this.SetUserProfileValue(userProfile, KEIZOKU_NYUURYOKU, keizokuNyuuryoku);
            this.SetUserProfileValue(userProfile, ENDDATE_USE_KBN_KOBETU, enddate_use_kbn_kobetu);
            this.SetUserProfileValue(userProfile, ITAKU_KEIYAKU_ALERT, itaku_keiyaku_alert);
            this.SetUserProfileValue(userProfile, CASHER_LINK, casher_link);
            this.SetUserProfileValue(userProfile, UPDATE_CSV_OUTPUT, update_csv_output);
            this.SetUserProfileValue(userProfile, MASTER_TANKA_UPDATE, master_tanka_update);
            this.SetUserProfileValue(userProfile, CSV_OUTPUT_FILE_PATH, csv_output_file_path);
            this.SetUserProfileValue(userProfile, DISPLAY_SIZE, display_size);
            //PhuocLoc 2020/04/03 #134980 -Start
            this.SetUserProfileValue(userProfile, UKEIRE_SEIKYUU_PRINT_KBN, Ukeire_Seikyuu_Print_Kbn);
            this.SetUserProfileValue(userProfile, UKEIRE_SHIHARAI_PRINT_KBN, Ukeire_Shiharai_Print_Kbn);
            this.SetUserProfileValue(userProfile, UKEIRE_DENPYOU_PRINT_KBN, Ukeire_Denpyou_Print_Kbn);
            this.SetUserProfileValue(userProfile, UKEIRE_KEIRYOU_PRIRNT_KBN, Ukeire_Keiryou_Prirnt_Kbn);
            this.SetUserProfileValue(userProfile, SHUKKA_SEIKYUU_PRINT_KBN, Shukka_Seikyuu_Print_Kbn);
            this.SetUserProfileValue(userProfile, SHUKKA_SHIHARAI_PRINT_KBN, Shukka_Shiharai_Print_Kbn);
            this.SetUserProfileValue(userProfile, SHUKKA_DENPYOU_PRINT_KBN, Shukka_Denpyou_Print_Kbn);
            this.SetUserProfileValue(userProfile, SHUKKA_KEIRYOU_PRINT_KBN, Shukka_Keiryou_Print_Kbn);
            this.SetUserProfileValue(userProfile, UR_SH_SEIKYUU_PRINT_KBN, Ur_Sh_Seikyuu_Print_Kbn);
            this.SetUserProfileValue(userProfile, UR_SH_SHIHARAI_PRINT_KBN, Ur_Sh_Shiharai_Print_Kbn);
            this.SetUserProfileValue(userProfile, UR_SH_DENPYOU_PRINT_KBN, Ur_Sh_Denpyou_Print_Kbn);
            //PhuocLoc 2020/04/03 #134980 -End
            this.SetUserProfileValue(userProfile, DENSHI_KEIYAKU_SHANAI_KEIRO, Denshi_Keiyaku_Shanai_Keiro);
            this.SetUserProfileValue(userProfile, DENSHI_KEIYAKU_SHANAI_KEIRO_CD, Denshi_Keiyaku_Shanai_Keiro_Cd);
            this.SetUserProfileValue(userProfile, FILE_UPLOAD_PATH, file_upload_path);
            this.SetUserProfileValue(userProfile, TAIRYU_TAB_HYOUJI_KBN, tairyu_tab_kbn); //PhuocLoc 2020/12/01 #136217
            this.SetUserProfileValue(userProfile, GREETINGS, greetings);
            this.SetUserProfileValue(userProfile, SIGNATURE, signature);

            //20250306
            this.SetUserProfileValue(userProfile, NITTEI_HOKOKU_KBN, nittei_hokuko_kbn);
            this.SetUserProfileValue(userProfile, TORIKOMI_FORUDA, torikomi_foruda);
            this.SetUserProfileValue(userProfile, HOKAN_FORUDA, hokan_foruda);
            this.SetUserProfileValue(userProfile, TOROKOMI_KANKAKU, torokomi_kankaku);
            this.SetUserProfileValue(userProfile, CTI_RENKEI_FORUDA, cti_renkei_foruda);
            this.SetUserProfileValue(userProfile, CTI_RENKEI_KANKAKU, cti_renkei_kankaku);

            userProfile.Save();
            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// ユーザー定義情報設定処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SetUserProfileValue(CurrentUserCustomConfigProfile profile, string key, string value)
        {
            LogUtility.DebugMethodStart(profile, key, value);

            // キーが存在する場合は上書きを行う
            bool isSet = false;
            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    item.Value = value;
                    isSet = true;
                    break;
                }
            }

            // キーが存在しない場合は新規追加を行う
            if (!isSet)
            {
                CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item = new CurrentUserCustomConfigProfile.SettingsCls.ItemSettings();
                item.Name = key;
                item.Value = value;
                profile.Settings.DefaultValue.Add(item);
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 各個処理
        /// <summary>
        /// CD表示変更処理
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="maxLength"></param>
        public string CdFormatting(string cd, string maxLength, out bool catchErr)
        {
            try
            {
                catchErr = false;
                // ゼロパディング後、テキストへ設定
                return String.Format("{0:D" + maxLength + "}", int.Parse(cd));
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CdFormatting", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                return "";
            }
        }
        /// <summary>
        /// 拠点略称検索
        /// </summary>
        /// <param name="kyotenCd"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public string SearchKyotenName(string kyotenCd, CancelEventArgs e, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(kyotenCd, e);

                catchErr = false;
                string kyotenName = string.Empty;
                if (!string.IsNullOrWhiteSpace(kyotenCd))
                {
                    M_KYOTEN cond = new M_KYOTEN();
                    cond.KYOTEN_CD = Int16.Parse(kyotenCd);
                    var dt = this.SystemKobetsuDaoCls.GetKyotenData(cond);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        kyotenName = dt.Rows[0]["KYOTEN_NAME_RYAKU"].ToString();
                    }
                    else
                    {
                        if (e != null)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "拠点");
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(kyotenName, catchErr);
                return kyotenName;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("SearchKyotenName", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd("", catchErr);
                return "";
            }
        }

        /// <summary>
        /// 荷降業者CD変更時処理
        /// </summary>
        public bool ChangeNioroshiGyoushaCD(String text)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 空文字時
                if (string.IsNullOrEmpty(text)) //this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                    this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                    return false;
                }

                if (this.form.NIOROSHI_GYOUSHA_CD.Text.Length == this.form.NIOROSHI_GYOUSHA_CD.CharactersNumber)
                {
                    M_GYOUSHA entity = new M_GYOUSHA();
                    entity.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                    M_GYOUSHA[] gyoushas = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetAllValidData(entity);
                    // 存在チェック
                    if (gyoushas == null || gyoushas.Length < 1)
                    {
                        // 表示クリア
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;

                        // 存在しない
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");

                        this.form.NIOROSHI_GYOUSHA_CD.Focus();
                        return false;
                    }
                    // 業者区分出荷チェック
                    M_GYOUSHA gyousha = gyoushas[0];
                    if (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                        if (this.form.beforNioroshiGyousaCD != this.form.NIOROSHI_GYOUSHA_CD.Text)
                        {
                            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                            this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;

                            this.form.beforNioroshiGyousaCD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                        }

                    }
                    else
                    {
                        // 表示クリア
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;

                        // 対象外のコードが設定されました
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        //msgLogic.MessageBoxShow("E058", "");
                        msgLogic.MessageBoxShow("E020", "業者");

                        this.form.NIOROSHI_GYOUSHA_CD.Focus();
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeNioroshiGyoushaCD", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 荷積業者CDの存在チェック
        /// </summary>
        public bool ChangeNitsumiGyoushaCD(string text)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 空文字時
                if (string.IsNullOrEmpty(text)) //this.form.NITSUMI_GYOUSHA_CD.Text))
                {
                    this.form.NITSUMI_GYOUSHA_NAME.Text = string.Empty;
                    this.form.NITSUMI_GENBA_CD.Text = string.Empty;
                    this.form.NITSUMI_GENBA_NAME.Text = string.Empty;
                    return false;
                }

                // 業者CDの変更時
                if (this.form.NITSUMI_GYOUSHA_CD.Text.Length == this.form.NITSUMI_GYOUSHA_CD.CharactersNumber)
                {
                    M_GYOUSHA entity = new M_GYOUSHA();
                    entity.GYOUSHA_CD = this.form.NITSUMI_GYOUSHA_CD.Text;
                    M_GYOUSHA[] gyoushas = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetAllValidData(entity);
                    // 存在チェック
                    if (gyoushas == null || gyoushas.Length < 1)
                    {
                        // 表示クリア
                        this.form.NITSUMI_GYOUSHA_NAME.Text = string.Empty;
                        this.form.NITSUMI_GENBA_CD.Text = string.Empty;
                        this.form.NITSUMI_GENBA_NAME.Text = string.Empty;

                        // 存在しない
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");

                        this.form.NITSUMI_GYOUSHA_CD.Focus();
                        return false;
                    }
                    // 業者区分出荷チェック
                    M_GYOUSHA gyousha = gyoushas[0];
                    if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    {
                        this.form.NITSUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                        if (this.form.beforNitsumiGyousaCD != this.form.NITSUMI_GYOUSHA_CD.Text)
                        {
                            this.form.NITSUMI_GENBA_CD.Text = string.Empty;
                            this.form.NITSUMI_GENBA_NAME.Text = string.Empty;

                            this.form.beforNitsumiGyousaCD = this.form.NITSUMI_GYOUSHA_CD.Text;
                        }

                    }
                    else
                    {
                        // 表示クリア
                        this.form.NITSUMI_GYOUSHA_NAME.Text = string.Empty;
                        this.form.NITSUMI_GENBA_CD.Text = string.Empty;
                        this.form.NITSUMI_GENBA_NAME.Text = string.Empty;

                        // 対象外のコードが設定されました
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        //msgLogic.MessageBoxShow("E058", "");
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.form.NITSUMI_GYOUSHA_CD.Focus();
                        return false;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeNitsumiGyoushaCD", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 荷積現場CDの存在チェック
        /// </summary>
        public virtual bool ChangeNitsumiGenbaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.NITSUMI_GENBA_NAME.Text = string.Empty;
                this.form.NITSUMI_GENBA_NAME.ReadOnly = true;
                this.form.NITSUMI_GENBA_NAME.Tag = string.Empty;
                this.form.NITSUMI_GENBA_NAME.TabStop = false;

                if (string.IsNullOrEmpty(this.form.NITSUMI_GENBA_CD.Text))
                {
                    this.form.NITSUMI_GENBA_NAME.Text = string.Empty;
                    return false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                M_GENBA genbaFrom = new M_GENBA();
                genbaFrom.GENBA_CD = this.form.NITSUMI_GENBA_CD.Text;
                genbaFrom.GYOUSHA_CD = this.form.NITSUMI_GYOUSHA_CD.Text;

                // 業者チェック
                if (string.IsNullOrEmpty(this.form.NITSUMI_GYOUSHA_NAME.Text))
                {
                    msgLogic.MessageBoxShow("E051", "荷積業者");
                    this.form.NITSUMI_GENBA_CD.Text = string.Empty;
                    this.form.NITSUMI_GENBA_CD.Focus();
                    return false;
                }

                M_GENBA[] genbas = DaoInitUtility.GetComponent<IM_GENBADao>().GetAllValidData(genbaFrom);

                // 存在チェック
                if (genbas == null || genbas.Length < 1)
                {
                    // 存在しない
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.NITSUMI_GENBA_CD.Focus();
                    return false;
                }

                M_GENBA genba = genbas[0];
                if (!genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue && !genba.TSUMIKAEHOKAN_KBN.IsTrue)
                {
                    // 存在しない
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.NITSUMI_GENBA_CD.Focus();
                    return false;
                }

                // 現場名を表示
                this.form.NITSUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeNitsumiGenbaCD", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 荷降現場CD変更時処理
        /// </summary>
        public bool ChangeNioroshiGenbaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
                this.form.NIOROSHI_GENBA_NAME.Tag = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.TabStop = false;

                if (string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                {
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                    return false;
                }

                // Message
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 業者チェック
                if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_NAME.Text))
                {
                    msgLogic.MessageBoxShow("E051", "荷降業者");
                    this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                    this.form.NIOROSHI_GENBA_CD.Focus();
                    return false;
                }

                // 現場取得
                M_GENBA genbaFrom = new M_GENBA();
                genbaFrom.GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
                genbaFrom.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                M_GENBA[] genbas = DaoInitUtility.GetComponent<IM_GENBADao>().GetAllValidData(genbaFrom);

                // 存在チェック
                if (genbas == null || genbas.Length < 1)
                {
                    // 存在しない
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.NIOROSHI_GENBA_CD.Focus();
                    return false;
                }

                M_GENBA genba = genbas[0];
                if (!genba.SHOBUN_NIOROSHI_GENBA_KBN && !genba.SAISHUU_SHOBUNJOU_KBN && !genba.TSUMIKAEHOKAN_KBN.IsTrue)
                {
                    // 存在しない
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.NIOROSHI_GENBA_CD.Focus();
                    return false;
                }

                // 現場名を表示
                this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeNioroshiGenbaCD", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 社内経路名称CD変更時処理
        /// </summary>
        public bool ChangeShanaiKeiroCD()
        {
            try
            {

                LogUtility.DebugMethodStart();

                // 初期化
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = string.Empty;
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.ReadOnly = true;
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Tag = string.Empty;
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.TabStop = false;

                if (string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text))
                {
                    this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = string.Empty;
                    return false;
                }

                // Message
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 現場取得
                M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME param = new M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME();
                param.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = short.Parse(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text);
                M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME[] keiro = DaoInitUtility.GetComponent<IM_DENSHI_KEIYAKU_SHANAI_KEIRO_NAMEDao>().GetAllValidData(param);

                // 存在チェック
                if (keiro == null || keiro.Length < 1)
                {
                    // 存在しない
                    msgLogic.MessageBoxShow("E020", "社内経路名（電子）");
                    this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Focus();
                    return false;
                }

                // 経路名を表示
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = keiro[0].DENSHI_KEIYAKU_SHANAI_KEIRO_NAME;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShanaiKeiroCD", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #endregion

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 参照ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool FileRefClick()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //※※※※※暫定措置※※※※※
                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するファイルを選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    this.form.CSV_OUTPUT_FILE_PATH.Text = filePath;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileRefClick", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// ファイルアップロード参照ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool FileUploadRefClick()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するフォルダーを選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    this.form.FILE_UPLOAD_PATH.Text = filePath;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileUploadRefClick", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        //20250306
        public bool FileRefClick_1()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //※※※※※暫定措置※※※※※
                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するファイルを選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    this.form.TORIKOMI_FORUDA.Text = filePath;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileRefClick", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        public bool FileRefClick_2()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //※※※※※暫定措置※※※※※
                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するファイルを選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    this.form.HOKAN_FORUDA.Text = filePath;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileRefClick", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        public bool FileRefClick_3()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //※※※※※暫定措置※※※※※
                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するファイルを選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    this.form.CTI_RENKEI_FORUDA.Text = filePath;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileRefClick", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
    }
}
