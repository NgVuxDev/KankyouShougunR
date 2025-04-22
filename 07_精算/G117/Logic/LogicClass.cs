using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

//using Shougun.Core.Adjustment.ShiharaiShimeShori;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Adjustment.Shiharaicheckhyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region プロパティ

        /// <summary>
        /// 取引先検索結果
        /// </summary>
        public DataTable TorihikisakiSearchResult { get; set; }

        /// <summary>
        /// 取引先検索条件
        /// </summary>
        public TorihikisakiDTO TorihikisakiSearchString { get; set; }

        /// <summary>
        /// システム設定検索結果
        /// </summary>
        public DataTable SysInfoSearchResult { get; set; }

        /// <summary>
        /// 締処理エラー検索結果
        /// </summary>
        public DataTable ShimeShoriErrSearchResult { get; set; }

        /// <summary>
        /// 締処理エラー検索条件
        /// </summary>
        public ShimeShoriErrDTO ShimeShoriErrSearchString { get; set; }

        /// <summary>
        /// 締処理エラーグリッド表示データテーブル
        /// </summary>
        public DataTable ErrHyojiTable { get; set; }

        /// <summary>
        /// 更新条件
        /// </summary>
        public List<T_SHIME_SHORI_ERROR> TsseList { get; set; }

        /// <summary>
        /// システム設定テーブルの請求情報締処理区分
        /// </summary>
        public int SeikyuuShimeShoriKbn { get; set; }

        #endregion プロパティ

        #region 定数

        //伝票種類初期値
        private const String DENPYOKINDINIT = "1";

        //拠点CD初期値
        private const int KYOTENCDINIT = 99;

        //締日入力チェックメッセージ
        private const String SHIMEBIERRMSG = "【0,5,10,15,20,25,31】のみ入力してください。";

        //登録押下時未入力チェックメッセージ
        private const String TOROKUERRMSG = "該当データがありませんでした。";

        //登録押下時確認メッセージ
        private const String TOROKUINFOMSG = "エラー内容を登録しますか。";

        //入力チェックメッセージタイトル
        private const String DIALOGTITLE = "インフォメーション";

        //FLAGOFF
        private const String FLAGOFF = "0";

        //FLAGON
        private const String FLAGON = "1";

        //エラーコントロール
        private const String DENPYOSHURUI = "伝票種類";
        private const String KYOTENCD = "拠点コード";
        private const String SHIMEBI = "締日";
        private const String SEARCHDATE = "検索期間";
        private const String DENPYOSHURUICHECKBOX = "締日チェックボックス";

        //エラーメッセージID
        private const String E012 = "E012";
        private const String E049 = "E049";
        private const String C030 = "C030";
        private const String E080 = "E080";
        private const String C001 = "C001";
        private const String I001 = "I001";

        //使用画面指定：締めチェック画面
        private const int SHIYOUGMN = 2;

        //売上・支払い区分：支払
        private const int SHIHARAI = 2;

        #endregion 定数

        ///<summary>
        ///取引先一覧のDao
        ///</summary>
        private MTSDaoCls MtsDaoPatern;

        ///<summary>
        ///締処理エラーのDao
        ///</summary>
        private SSEDaoCls SseDaoPatern;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// システム設定
        /// </summary>
        private IM_SYS_INFODao mSysInfoDao;

        /// <summary>
        /// 自社情報
        /// </summary>
        private IM_CORP_INFODao mCorpInfoDao;

        /// <summary>
        /// DBアクセサ
        /// </summary>
        private DBAccessor dbAccessor;

        /// <summary>
        /// システム設定
        /// </summary>
        private M_SYS_INFO sysInfo;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// 帳票表示用会社名
        /// </summary>
        private string corpName;

        /// <summary>
        /// 他画面で更新あり
        /// </summary>
        private bool isUpdate;

        internal MessageBoxShowLogic errmessage;

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.TorihikisakiSearchString = new TorihikisakiDTO();
            this.MtsDaoPatern = DaoInitUtility.GetComponent<MTSDaoCls>();

            this.ShimeShoriErrSearchString = new ShimeShoriErrDTO();
            this.SseDaoPatern = DaoInitUtility.GetComponent<SSEDaoCls>();

            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();

            this.dbAccessor = new DBAccessor();

            this.errmessage = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion コンストラクタ

        #region 初期処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                //日付の初期値を今日の日付に設定(デザイナで設定すると自動で書き換えられるため)
                this.form.dtpSearchDateFrom.Value = this.parentForm.sysDate;
                this.form.dtpSearchDateTo.Value = this.parentForm.sysDate;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //================================CurrentUserCustomConfigProfile.xmlを読み込み============================
                XMLAccessor fileAccess = new XMLAccessor();
                CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

                this.headerForm.txtUserKyotenCD.Text = configProfile.ItemSetVal1.PadLeft(2, '0');

                //画面初期表示時、以下の項目を設定
                //【伝票種類】
                this.form.txtDenpyoKind.Text = DENPYOKINDINIT;
                //【拠点コード】
                this.form.txtKyotenCD.Text = KYOTENCDINIT.ToString();
                //【拠点名】
                this.form.txtKyotenName.Text = string.Empty;
                //【締日】
                this.form.cmbShimebi.SelectedIndex = ShimeDateInit();
                //【検索期間From】
                this.form.dtpSearchDateFrom.Value = null;
                //【検索期間To】
                this.form.dtpSearchDateTo.Value = this.parentForm.sysDate;

                // Anchor設定
                this.form.dgvTorihikisakiIchiran.Anchor = (AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
                this.form.dgvErrorNaiyo.Anchor = (AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom);

                // ユーザ拠点名称の取得
                bool catchErr = true;
                this.headerForm.txtUserKyotenName.Text = SelectKyotenNameRyaku(this.headerForm.txtUserKyotenCD.Text, out catchErr);
                if (!catchErr)
                {
                    ret = false;
                    return ret;
                }

                // 自社名を取得
                corpName = SelectCorpName();

                //システム設定値を取得
                sysInfo = new M_SYS_INFO();
                sysInfo = dbAccessor.GetSysInfo();

                // 請求情報締処理区分の取得
                this.SeikyuuShimeShoriKbn = GetShimeTani();

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G117", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.SetReferenceMode();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            string ButtonInfoXmlPath = this.GetType().Namespace;
            ButtonInfoXmlPath = ButtonInfoXmlPath + ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //前月ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.Function3Click);

            //翌月ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.Function4Click);

            //CSVボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Function5Click);

            //印刷ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.Function6Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Function8Click);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Function9Click);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 20141128 Houkakou 「支払チェック表」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.dtpSearchDateTo.MouseDoubleClick += new MouseEventHandler(dtpSearchDateTo_MouseDoubleClick);
            // 20141128 Houkakou 「支払チェック表」のダブルクリックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 締日の初期化処理
        /// </summary>
        private int ShimeDateInit()
        {
            LogUtility.DebugMethodStart();

            //システム日付を取得
            DateTime sysDate = this.parentForm.sysDate;
            int nowDay = sysDate.Day;
            int retVal = 0;
            if (nowDay < 5)
            {
                //31
                retVal = 6;
            }
            else if (nowDay < 10)
            {
                //5
                retVal = 1;
            }
            else if (nowDay < 15)
            {
                //10
                retVal = 2;
            }
            else if (nowDay < 20)
            {
                //15
                retVal = 3;
            }
            else if (nowDay < 25)
            {
                //20
                retVal = 4;
            }
            else
            {
                //25
                retVal = 5;
            }

            LogUtility.DebugMethodEnd(retVal);
            return retVal;
        }

        /// <summary>
        /// 参照モード用項目制御処理を行います
        /// </summary>
        private void SetReferenceMode()
        {
            /* FunctionButton */
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func9.Enabled = false;
        }

        #endregion 初期処理

        #region データ取得

        /// <summary>
        /// 取引先一覧取得
        /// </summary>
        [Transaction]
        public virtual bool GetTorihikisaki(String torihikisakiCd)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);

                this.TorihikisakiSearchResult = new DataTable();
                //検索条件を設定
                this.TorihikisakiSearchString.Delete_Flag = FLAGOFF;
                this.TorihikisakiSearchString.Shimebi1 = null;
                this.TorihikisakiSearchString.Shimebi2 = null;
                this.TorihikisakiSearchString.Shimebi3 = null;
                if (torihikisakiCd != null)
                {
                    this.TorihikisakiSearchString.TorihikisakiCd = torihikisakiCd;
                }
                else
                {
                    this.TorihikisakiSearchString.TorihikisakiCd = null;
                }
                this.TorihikisakiSearchResult = MtsDaoPatern.GetDataForEntity(this.TorihikisakiSearchString);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion データ取得

        #region グリッドデータ表示

        /// <summary>
        /// 取引先一覧をグリッドに表示
        /// </summary>
        public bool SetTorihikisaki()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //描画を停止
                this.form.dgvTorihikisakiIchiran.IsBrowsePurpose = false;
                this.form.dgvTorihikisakiIchiran.SuspendLayout();

                bool isShimeCheck = false;

                //締日が0以外の場合は行締チェックボックスと全締チェックボックスをONに設定
                if (this.form.cmbShimebi.Text != "0")
                {
                    isShimeCheck = true;

                    this.form.checkBoxAll.Checked = true;
                    this.form.dgvTorihikisakiIchiran.Refresh();
                }

                //前の結果をクリア
                int k = this.form.dgvTorihikisakiIchiran.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.form.dgvTorihikisakiIchiran.Rows.RemoveAt(this.form.dgvTorihikisakiIchiran.Rows[i - 1].Index);
                }

                //検索結果を設定する
                var table = this.TorihikisakiSearchResult;
                table.BeginLoadData();

                //検索結果設定
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    this.form.dgvTorihikisakiIchiran.Rows.Add();
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colShime"].Value = isShimeCheck;
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colTorihikisakiCD"].Value = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colTorihikisakiName"].Value = table.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colShimeDate1"].Value = table.Rows[i]["SHIMEBI1"];
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colShimeDate2"].Value = table.Rows[i]["SHIMEBI2"];
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colShimeDate3"].Value = table.Rows[i]["SHIMEBI3"];
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colAddress1"].Value = table.Rows[i]["TORIHIKISAKI_ADDRESS1"].ToString();
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colAddress2"].Value = table.Rows[i]["TORIHIKISAKI_ADDRESS2"].ToString();
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colTel"].Value = table.Rows[i]["TORIHIKISAKI_TEL"].ToString();
                    this.form.dgvTorihikisakiIchiran.Rows[i].Cells["colFax"].Value = table.Rows[i]["TORIHIKISAKI_FAX"].ToString();
                }

                //描画を再開
                this.form.dgvTorihikisakiIchiran.ResumeLayout();
                this.form.dgvTorihikisakiIchiran.IsBrowsePurpose = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetTorihikisaki", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisaki", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// チェック結果をグリッドに表示
        /// </summary>
        public bool SetCheckKekka()
        {
            LogUtility.DebugMethodStart();

            //描画を停止
            this.form.dgvTorihikisakiIchiran.IsBrowsePurpose = false;
            this.form.dgvErrorNaiyo.IsBrowsePurpose = false;
            this.form.dgvTorihikisakiIchiran.SuspendLayout();
            this.form.dgvErrorNaiyo.SuspendLayout();

            //共通処理より取得した値をグリッドに設定
            //前の結果をクリア
            int k = this.form.dgvErrorNaiyo.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.form.dgvErrorNaiyo.Rows.RemoveAt(this.form.dgvErrorNaiyo.Rows[i - 1].Index);
            }

            if (this.ErrHyojiTable != null)
            {
                //検索結果を設定する
                var table = this.ErrHyojiTable;
                table.BeginLoadData();

                //検索結果設定
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    this.form.dgvErrorNaiyo.Rows.Add();

                    //区分名(伝票種類CDを元に表示)
                    String denpyoKbnName = string.Empty;
                    int denpyouShuruiCD = int.Parse(table.Rows[i]["DENPYOU_SHURUI_CD"].ToString());
                    switch (denpyouShuruiCD)
                    {
                        case 1:
                            denpyoKbnName = "受入伝票";
                            break;

                        case 2:
                            denpyoKbnName = "出荷伝票";
                            break;

                        case 3:
                            denpyoKbnName = "売上/支払伝票";
                            if (table.Columns.Contains("DAINOU_FLG"))
                            {
                                bool bol = false;
                                if (bool.TryParse(Convert.ToString(table.Rows[i]["DAINOU_FLG"]), out bol))
                                {
                                    if (bol)
                                    {
                                        denpyoKbnName = "代納伝票";
                                    }
                                }
                            }
                            break;

                        default:
                            denpyoKbnName = "出金伝票";
                            break;
                    }

                    //拠点CDから拠点名略称を取得
                    bool catchErr = true;
                    String kyotenName = SelectKyotenNameRyaku((String)table.Rows[i]["KYOTEN_CD"], out catchErr);
                    if (!catchErr)
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    //取引先CDから取引先名略称を取得
                    if (!GetTorihikisaki((String)table.Rows[i]["TORIHIKISAKI_CD"]))
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    String torihikisakiRyakuName = "";
                    if (this.TorihikisakiSearchResult.Rows.Count > 0)
                    {
                        torihikisakiRyakuName = (String)this.TorihikisakiSearchResult.Rows[0]["TORIHIKISAKI_NAME_RYAKU"];
                    }

                    this.form.dgvErrorNaiyo.Rows[i].Cells["colDenpyoKubunName"].Value = denpyoKbnName;
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colKyotenName"].Value = kyotenName;
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colShiharaiDate"].Value = table.Rows[i]["SHIHARAI_DATE"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colDenpyoNo"].Value = table.Rows[i]["DENPYOU_NUMBER"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colErrorNaiyo"].Value = table.Rows[i]["ERROR_NAIYOU"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colRiyu"].Value = table.Rows[i]["RIYUU"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colRiyuKoushinmae"].Value = table.Rows[i]["RIYUU"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colTorihikisakiCDReport"].Value = table.Rows[i]["TORIHIKISAKI_CD"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colTorihikisakiRyakushoNameReport"].Value = torihikisakiRyakuName;
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colMeisaiGyoNoReport"].Value = table.Rows[i]["GYO_NUMBER"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colCheckKbn"].Value = table.Rows[i]["CHECK_KBN"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colDenpyoKubunCD"].Value = table.Rows[i]["DENPYOU_SHURUI_CD"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colSystemID"].Value = table.Rows[i]["SYSTEM_ID"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colSeq"].Value = table.Rows[i]["SEQ"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colDetailSystemID"].Value = table.Rows[i]["DETAIL_SYSTEM_ID"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colKyotenCD"].Value = table.Rows[i]["KYOTEN_CD"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colShoriKbn"].Value = table.Rows[i]["SHORI_KBN"];
                    this.form.dgvErrorNaiyo.Rows[i].Cells["colTimeStamp"].Value = table.Rows[i]["TIME_STAMP"];
                }
            }

            //描画を再開
            this.form.dgvTorihikisakiIchiran.ResumeLayout();
            this.form.dgvErrorNaiyo.ResumeLayout();
            this.form.dgvTorihikisakiIchiran.IsBrowsePurpose = true;
            this.form.dgvErrorNaiyo.IsBrowsePurpose = true;

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion グリッドデータ表示

        #region 入力チェック

        /// <summary>
        /// 未入力チェック
        /// </summary>
        public bool InputCheck(out String errKomoku, out String focusControl)
        {
            errKomoku = null;
            focusControl = null;

            LogUtility.DebugMethodStart();

            bool checkOk = true;
            String separator = ",";

            //伝票種類
            if (this.form.txtDenpyoKind.Text == string.Empty)
            {
                checkOk = false;
                errKomoku += DENPYOSHURUI;

                if (focusControl == null)
                {
                    focusControl = DENPYOSHURUI;
                }
            }

            //拠点CD
            if (this.form.txtKyotenCD.Text == string.Empty)
            {
                checkOk = false;
                if (errKomoku != null)
                {
                    errKomoku += separator;
                }
                errKomoku += KYOTENCD;

                if (focusControl == null)
                {
                    focusControl = KYOTENCD;
                }
            }

            //締日
            if (this.form.cmbShimebi.Text == string.Empty)
            {
                checkOk = false;
                if (errKomoku != null)
                {
                    errKomoku += separator;
                }
                errKomoku += SHIMEBI;

                if (focusControl == null)
                {
                    focusControl = SHIMEBI;
                }
            }

            //検索期間
            if ((this.form.dtpSearchDateFrom.Value == null) && (this.form.dtpSearchDateTo.Value == null))
            {
                checkOk = false;
                if (errKomoku != null)
                {
                    errKomoku += separator;
                }
                errKomoku += SEARCHDATE;

                if (focusControl == null)
                {
                    focusControl = SEARCHDATE;
                }
            }

            //締日チェックボックス
            bool isCheckedFlag = false;

            foreach (DataGridViewRow row in this.form.dgvTorihikisakiIchiran.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    isCheckedFlag = true;
                    break;
                }
            }

            if (!isCheckedFlag)
            {
                checkOk = false;
                if (errKomoku != null)
                {
                    errKomoku += separator;
                }
                errKomoku += DENPYOSHURUICHECKBOX;

                if (focusControl == null)
                {
                    focusControl = DENPYOSHURUICHECKBOX;
                }
            }

            LogUtility.DebugMethodEnd(errKomoku, focusControl, checkOk);
            return checkOk;
        }

        /// <summary>
        /// 検索期間関連チェック
        /// </summary>
        public bool SearchDateCheck()
        {
            LogUtility.DebugMethodStart();

            bool checkOk = true;

            if ((DateTime)this.form.dtpSearchDateTo.Value < (DateTime)this.form.dtpSearchDateFrom.Value)
            {
                // 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　start
                this.form.dtpSearchDateFrom.IsInputErrorOccured = true;
                this.form.dtpSearchDateTo.IsInputErrorOccured = true;
                this.form.dtpSearchDateFrom.BackColor = Constans.ERROR_COLOR;
                this.form.dtpSearchDateTo.BackColor = Constans.ERROR_COLOR;
                // 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　end
                checkOk = false;
            }

            LogUtility.DebugMethodEnd();

            return checkOk;
        }

        #endregion 入力チェック

        #region [F3]前月、[F4]翌月押下時

        /// <summary>
        /// 前月日付設定
        /// </summary>
        public bool SetDatePreviousMonth(out object valDateFrom, out object valDateTo, bool isNextMonth)
        {
            bool ret = true;
            valDateFrom = null;
            valDateTo = null;

            try
            {
                LogUtility.DebugMethodStart(valDateFrom, valDateTo, isNextMonth);

                int monthFlg = 0;

                if (isNextMonth)
                {
                    monthFlg = 1;
                }
                else
                {
                    monthFlg = -1;
                }

                //月末判定フラグ
                bool fromMonthFlg = false;
                bool toMonthFlg = false;

                if (this.form.dtpSearchDateFrom.Value != null)
                {
                    //月末か判定
                    DateTime fromD1 = (DateTime)this.form.dtpSearchDateFrom.Value;
                    DateTime fromD2 = new DateTime(fromD1.Year, fromD1.Month, DateTime.DaysInMonth(fromD1.Year, fromD1.Month));

                    if (fromD1.ToShortDateString().Equals(fromD2.ToShortDateString()))
                    {
                        fromMonthFlg = true;
                    }

                    fromD1 = fromD1.AddMonths(monthFlg);

                    //月末なら1か月前後の月末を設定
                    if (fromMonthFlg)
                    {
                        valDateFrom = new DateTime(fromD1.Year, fromD1.Month, DateTime.DaysInMonth(fromD1.Year, fromD1.Month));
                    }
                    else
                    {
                        valDateFrom = (object)fromD1;
                    }
                }

                if (this.form.dtpSearchDateTo.Value != null)
                {
                    //月末か判定
                    DateTime toD1 = (DateTime)this.form.dtpSearchDateTo.Value;
                    DateTime toD2 = new DateTime(toD1.Year, toD1.Month, DateTime.DaysInMonth(toD1.Year, toD1.Month));

                    if (toD1.ToShortDateString().Equals(toD2.ToShortDateString()))
                    {
                        toMonthFlg = true;
                    }

                    toD1 = toD1.AddMonths(monthFlg);

                    //月末なら1か月前後の月末を設定
                    if (toMonthFlg)
                    {
                        valDateTo = new DateTime(toD1.Year, toD1.Month, DateTime.DaysInMonth(toD1.Year, toD1.Month));
                    }
                    else
                    {
                        valDateTo = (object)toD1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDatePreviousMonth", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret, valDateFrom, valDateTo);
            return ret;
        }

        #endregion [F3]前月、[F4]翌月押下時

        #region [F5]印刷押下時

        internal bool Function5ClickLogic()
        {
            bool ret = true;
            DataTable csvDT = new DataTable();
            DataRow rowTmp;
            try
            {
                if (this.form.dgvErrorNaiyo.Rows.Count == 0)
                {
                    return ret;
                }

                string[] csvHead = { "区分","拠点", "支払日付", "伝票番号", "取引先CD", "取引先", 
                                         "明細番号", "エラー内容", "理由"};
                for (int i = 0; i < csvHead.Length; i++)
                {
                    csvDT.Columns.Add(csvHead[i]);
                }

                foreach (DataGridViewRow row in this.form.dgvErrorNaiyo.Rows)
                {
                    rowTmp = csvDT.NewRow();

                    if (row.Cells["colDenpyoKubunName"] != null && !string.IsNullOrEmpty(row.Cells["colDenpyoKubunName"].Value.ToString()))
                    {
                        rowTmp["区分"] = row.Cells["colDenpyoKubunName"].Value.ToString();
                    }

                    if (row.Cells["colKyotenName"] != null && !string.IsNullOrEmpty(row.Cells["colKyotenName"].Value.ToString()))
                    {
                        rowTmp["拠点"] = row.Cells["colKyotenName"].Value.ToString();
                    }

                    if (row.Cells["colShiharaiDate"] != null && !string.IsNullOrEmpty(row.Cells["colShiharaiDate"].Value.ToString()))
                    {
                        rowTmp["支払日付"] = Convert.ToDateTime(row.Cells["colShiharaiDate"].Value.ToString()).ToString("yyyy/MM/dd");
                    }

                    if (row.Cells["colDenpyoNo"] != null && !string.IsNullOrEmpty(row.Cells["colDenpyoNo"].Value.ToString()))
                    {
                        rowTmp["伝票番号"] = row.Cells["colDenpyoNo"].Value.ToString();
                    }

                    if (row.Cells["colTorihikisakiCDReport"] != null && !string.IsNullOrEmpty(row.Cells["colTorihikisakiCDReport"].Value.ToString()))
                    {
                        rowTmp["取引先CD"] = row.Cells["colTorihikisakiCDReport"].Value.ToString();
                    }

                    if (row.Cells["colTorihikisakiRyakushoNameReport"] != null && !string.IsNullOrEmpty(row.Cells["colTorihikisakiRyakushoNameReport"].Value.ToString()))
                    {
                        rowTmp["取引先"] = row.Cells["colTorihikisakiRyakushoNameReport"].Value.ToString();
                    }

                    if (row.Cells["colMeisaiGyoNoReport"] != null && !string.IsNullOrEmpty(row.Cells["colMeisaiGyoNoReport"].Value.ToString()))
                    {
                        rowTmp["明細番号"] = row.Cells["colMeisaiGyoNoReport"].Value.ToString();
                    }

                    if (row.Cells["colErrorNaiyo"] != null && !string.IsNullOrEmpty(row.Cells["colErrorNaiyo"].Value.ToString()))
                    {
                        rowTmp["エラー内容"] = row.Cells["colErrorNaiyo"].Value.ToString();
                    }

                    if (row.Cells["colRiyu"] != null && !string.IsNullOrEmpty(row.Cells["colRiyu"].Value.ToString()))
                    {
                        rowTmp["理由"] = row.Cells["colRiyu"].Value.ToString();
                    }
                    csvDT.Rows.Add(rowTmp);
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_SHIHARAI_CHECKHYOU), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function6ClickLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion [F5]印刷押下時

        #region [F6]印刷押下時

        internal bool Function6ClickLogic()
        {
            bool ret = true;
            try
            {
                // 印刷対象データがなければ処理終了
                if (this.form.dgvErrorNaiyo.Rows.Count == 0)
                {
                    return ret;
                }

                DataTable dt = new DataTable();

                dt.Columns.Add();

                System.Text.StringBuilder sBuilder;
                foreach (DataGridViewRow row in this.form.dgvErrorNaiyo.Rows)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    sBuilder = new StringBuilder();

                    sBuilder.Append("\"");
                    sBuilder.Append("1-1");
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["colDenpyoKubunName"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["colKyotenName"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["colShiharaiDate"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["colDenpyoNo"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["colTorihikisakiCDReport"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["colTorihikisakiRyakushoNameReport"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["colMeisaiGyoNoReport"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["colErrorNaiyo"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["colRiyu"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(corpName);
                    sBuilder.Append("\"");

                    dr[0] = sBuilder.ToString();
                    dt.Rows.Add(dr);
                }

                ReportInfoR387 report_r387 = new ReportInfoR387();

                report_r387.R387_Report(dt, this.parentForm.sysDate);

                // 印刷ポツプアップ画面表示
                using (FormReportPrintPopup report = new FormReportPrintPopup(report_r387))
                {
                    //レポートタイトルの設定
                    report.ReportCaption = "支払チェック表";

                    report.ShowDialog();
                    report.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function6ClickLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion [F6]印刷押下時

        #region [F8]検索押下時

        /// <summary>
        /// 検索ボタン押下時処理
        /// </summary>
        public bool Function8ClickLogic()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                String inputCheckMsgParam = string.Empty;
                String focusControl = string.Empty;
                String dateCheckMsgParam = string.Empty;
                String errMsgId = string.Empty;

                bool isSearch = false;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //入力値チェック
                if (InputCheck(out inputCheckMsgParam, out focusControl))
                {
                    if (!(this.form.dtpSearchDateFrom.Value == null) && !(this.form.dtpSearchDateTo.Value == null))
                    {
                        //検索期間の関連チェック
                        if (!SearchDateCheck())
                        {
                            //エラーメッセージ表示
                            // 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　start
                            msgLogic.MessageBoxShow("E030", "検索期間From", "検索期間To");
                            // 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　start

                            //フォーカス設定
                            this.form.dtpSearchDateFrom.Focus();

                            isSearch = false;
                        }
                        else
                        {
                            isSearch = true;
                        }
                    }
                    else
                    {
                        isSearch = true;
                    }
                }
                else
                {
                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow(E012, inputCheckMsgParam);

                    //フォーカス設定
                    if (focusControl == DENPYOSHURUI)
                    {
                        this.form.txtDenpyoKind.Focus();
                    }
                    else if (focusControl == KYOTENCD)
                    {
                        this.form.txtKyotenCD.Focus();
                    }
                    else if (focusControl == SHIMEBI)
                    {
                        this.form.cmbShimebi.Focus();
                    }
                    else if (focusControl == SEARCHDATE)
                    {
                        this.form.dtpSearchDateFrom.Focus();
                    }
                    else
                    {
                        this.form.dgvTorihikisakiIchiran.Focus();
                        if (this.form.dgvTorihikisakiIchiran.Rows.Count != 0)
                        {
                            this.form.dgvTorihikisakiIchiran.CurrentCell = this.form.dgvTorihikisakiIchiran[0, 0];
                        }
                    }

                    isSearch = false;
                }

                if (isSearch)
                {
                    DialogResult result = DialogResult.Yes;

                    //確認メッセージ表示
                    result = msgLogic.MessageBoxShow(C030, "支払");

                    if (result == DialogResult.Yes)
                    {
                        //検索処理実行
                        Cursor preCursor = Cursor.Current;
                        Cursor.Current = Cursors.WaitCursor;

                        ShimeCheck();
                        if (!SetCheckKekka())
                        {
                            ret = false;
                            return ret;
                        }

                        Cursor.Current = preCursor;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Function8ClickLogic", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function8ClickLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 検索処理(締めチェック処理実行)
        /// </summary>
        private void ShimeCheck()
        {
            ShimeCheckLogic checkLogic = new ShimeCheckLogic();
            CheckDto SeikyuShimeChk;
            List<CheckDto> seikyuShimeChkLst = new List<CheckDto>();

            //エラー表示内容テーブルの初期化
            ErrHyojiTable = null;

            //締めチェックボックスがONのレコードを取得
            foreach (DataGridViewRow row in this.form.dgvTorihikisakiIchiran.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    SeikyuShimeChk = new CheckDto();

                    string dateFrom = "";
                    string dateTo = "";
                    if (this.form.dtpSearchDateFrom.Value != null)
                    {
                        dateFrom = this.form.dtpSearchDateFrom.Value.ToString();
                        dateFrom = dateFrom.Remove(dateFrom.IndexOf(" "));
                        dateFrom = dateFrom + " 0:00:00";
                    }

                    if (this.form.dtpSearchDateTo.Value != null)
                    {
                        dateTo = this.form.dtpSearchDateTo.Value.ToString();
                    }
                    else
                    {
                        dateTo = DateTime.Today.ToString();
                    }
                    dateTo = dateTo.Remove(dateTo.IndexOf(" "));
                    dateTo = dateTo + " 0:00:00";

                    SeikyuShimeChk.TORIHIKISAKI_CD = (string)row.Cells["colTorihikisakiCD"].Value;
                    SeikyuShimeChk.KYOTEN_CD = int.Parse(this.form.txtKyotenCD.Text);
                    SeikyuShimeChk.DENPYOU_SHURUI = int.Parse(this.form.txtDenpyoKind.Text);
                    SeikyuShimeChk.KIKAN_FROM = dateFrom;
                    SeikyuShimeChk.KIKAN_TO = dateTo;
                    SeikyuShimeChk.SHIYOU_GAMEN = SHIYOUGMN;
                    SeikyuShimeChk.SHIME_TANI = SeikyuuShimeShoriKbn;
                    SeikyuShimeChk.URIAGE_SHIHARAI_KBN = SHIHARAI;

                    seikyuShimeChkLst.Add(SeikyuShimeChk);
                }
            }

            //共通締めチェッククラスの締め処理前チェック処理を実行
            DataTable resultTable = null;
            resultTable = checkLogic.checkShimeData(seikyuShimeChkLst);

            if (resultTable.Rows.Count == 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow(C001);
                return;
            }

            if (resultTable != null)
            {
                //データのソート
                //日付、拠点CD、伝票番号、区分の昇順
                DataView dv = new DataView();
                resultTable.TableName = "締め処理チェック結果";
                dv.Table = resultTable;

                dv.Sort = "DENPYOU_DATE, KYOTEN_CD, DENPYOU_NUMBER, DENPYOU_SHURUI_CD ASC";

                //フィルタ結果からデータテーブルを作成
                DataTable aftsorttable;
                aftsorttable = dv.ToTable();

                //グリッド表示用のデータテーブルを定義
                ErrHyojiTable = aftsorttable.Clone();
                ErrHyojiTable.Columns.Add("TIME_STAMP", Type.GetType("System.Object"));

                foreach (DataRow row in aftsorttable.Rows)
                {
                    //締処理エラーテーブルを検索
                    this.ShimeShoriErrSearchResult = new DataTable();

                    //検索実行
                    Dictionary<string, string> searchCondition = new Dictionary<string, string>();
                    searchCondition.Add("ShoriKbn", row["SHORI_KBN"].ToString());
                    searchCondition.Add("CheckKbn", row["CHECK_KBN"].ToString());
                    searchCondition.Add("DenpyouShuruiCD", row["DENPYOU_SHURUI_CD"].ToString());
                    searchCondition.Add("SystemId", row["SYSTEM_ID"].ToString());
                    searchCondition.Add("Seq", row["SEQ"].ToString());
                    searchCondition.Add("DetailSystemId", row["DETAIL_SYSTEM_ID"].ToString());

                    this.ShimeShoriErrSearchResult = SelectT_SHIME_SHORI_ERROR(searchCondition);

                    DataRow errHyojiRow = ErrHyojiTable.NewRow();

                    string detailSystemID;
                    if (row["DETAIL_SYSTEM_ID"].ToString() == "")
                    {
                        detailSystemID = "0";
                    }
                    else
                    {
                        detailSystemID = row["DETAIL_SYSTEM_ID"].ToString();
                    }

                    if (ShimeShoriErrSearchResult.Rows.Count == 0)
                    {
                        //取得レコードが0件の場合は、
                        //共通締めチェッククラスから取得したレコードをグリッド表示用データテーブルに設定

                        string gyoNo;
                        if (row["GYO_NUMBER"].ToString() == "")
                        {
                            gyoNo = "0";
                        }
                        else
                        {
                            gyoNo = row["GYO_NUMBER"].ToString();
                        }

                        errHyojiRow["SHORI_KBN"] = row["SHORI_KBN"].ToString();
                        errHyojiRow["CHECK_KBN"] = row["CHECK_KBN"].ToString();
                        errHyojiRow["DENPYOU_SHURUI_CD"] = row["DENPYOU_SHURUI_CD"].ToString();
                        errHyojiRow["SYSTEM_ID"] = row["SYSTEM_ID"].ToString();
                        errHyojiRow["SEQ"] = row["SEQ"].ToString();
                        errHyojiRow["DETAIL_SYSTEM_ID"] = detailSystemID;
                        errHyojiRow["GYO_NUMBER"] = gyoNo;
                        errHyojiRow["ERROR_NAIYOU"] = row["ERROR_NAIYOU"].ToString();
                        errHyojiRow["RIYUU"] = row["RIYUU"].ToString();
                        errHyojiRow["TORIHIKISAKI_CD"] = row["TORIHIKISAKI_CD"].ToString();
                        errHyojiRow["KYOTEN_CD"] = row["KYOTEN_CD"].ToString();
                        errHyojiRow["DENPYOU_DATE"] = row["DENPYOU_DATE"].ToString();
                        errHyojiRow["URIAGE_DATE"] = row["URIAGE_DATE"].ToString();
                        errHyojiRow["SHIHARAI_DATE"] = row["SHIHARAI_DATE"].ToString();
                        errHyojiRow["DENPYOU_NUMBER"] = row["DENPYOU_NUMBER"].ToString();
                    }
                    else
                    {
                        string gyoNo;
                        if (ShimeShoriErrSearchResult.Rows[0]["GYO_NUMBER"].ToString() == "")
                        {
                            gyoNo = "0";
                        }
                        else
                        {
                            gyoNo = ShimeShoriErrSearchResult.Rows[0]["GYO_NUMBER"].ToString();
                        }

                        //レコード取得できた場合は行番号、エラー内容、理由の項目を取得値で更新
                        //その他の項目は共通締めチェッククラスから取得したレコードの値を使用
                        errHyojiRow["SHORI_KBN"] = row["SHORI_KBN"].ToString();
                        errHyojiRow["CHECK_KBN"] = row["CHECK_KBN"].ToString();
                        errHyojiRow["DENPYOU_SHURUI_CD"] = row["DENPYOU_SHURUI_CD"].ToString();
                        errHyojiRow["SYSTEM_ID"] = row["SYSTEM_ID"].ToString();
                        errHyojiRow["SEQ"] = row["SEQ"].ToString();
                        errHyojiRow["DETAIL_SYSTEM_ID"] = detailSystemID;
                        errHyojiRow["GYO_NUMBER"] = gyoNo;
                        errHyojiRow["ERROR_NAIYOU"] = ShimeShoriErrSearchResult.Rows[0]["ERROR_NAIYOU"].ToString();
                        errHyojiRow["RIYUU"] = ShimeShoriErrSearchResult.Rows[0]["RIYUU"].ToString();
                        errHyojiRow["TORIHIKISAKI_CD"] = row["TORIHIKISAKI_CD"].ToString();
                        errHyojiRow["KYOTEN_CD"] = row["KYOTEN_CD"].ToString();
                        errHyojiRow["DENPYOU_DATE"] = row["DENPYOU_DATE"].ToString();
                        errHyojiRow["URIAGE_DATE"] = row["URIAGE_DATE"].ToString();
                        errHyojiRow["SHIHARAI_DATE"] = row["SHIHARAI_DATE"].ToString();
                        errHyojiRow["DENPYOU_NUMBER"] = row["DENPYOU_NUMBER"].ToString();
                        errHyojiRow["TIME_STAMP"] = ShimeShoriErrSearchResult.Rows[0]["TIME_STAMP"];
                    }
                    errHyojiRow["DAINOU_FLG"] = row["DAINOU_FLG"];

                    ErrHyojiTable.Rows.Add(errHyojiRow);
                }
            }
        }

        /// <summary>
        /// 締め単位取得
        /// </summary>
        private int GetShimeTani()
        {
            string shimeTani = sysInfo.SHIHARAI_SHIME_SHORI_KBN.ToString();

            int retVal = 0;

            switch (shimeTani)
            {
                case "1":
                    //期間単位
                    retVal = 1;
                    break;

                case "2":
                    //伝票単位
                    retVal = 2;
                    break;

                case "3":
                    //明細単位
                    retVal = 3;
                    break;

                case "4":
                    //期間単位
                    retVal = 1;
                    break;

                default:
                    //明細単位
                    retVal = 3;
                    break;
            }
            return retVal;
        }

        #endregion [F8]検索押下時

        #region [F9]登録押下時

        [Transaction]
        public virtual void Function9ClickLogic()
        {
            //他画面更新ありフラグを初期化
            isUpdate = false;

            //未入力チェック
            if (this.form.dgvErrorNaiyo.Rows.Count == 0)
            {
                MessageBox.Show(TOROKUERRMSG, DIALOGTITLE);
            }
            else
            {
                DialogResult result = MessageBox.Show(TOROKUINFOMSG, DIALOGTITLE, MessageBoxButtons.YesNo);

                //はいを選択時のみ処理を続行
                if (result == DialogResult.Yes)
                {
                    //検索処理実行
                    Cursor preCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;

                    //追加対象データ格納リスト
                    List<T_SHIME_SHORI_ERROR> insertTsseList = new List<T_SHIME_SHORI_ERROR>();

                    //更新対象データ格納リスト
                    List<T_SHIME_SHORI_ERROR> updateTsseList = new List<T_SHIME_SHORI_ERROR>();

                    try
                    {
                        foreach (DataGridViewRow row in this.form.dgvErrorNaiyo.Rows)
                        {
                            //レコードがテーブルに存在するかデータを取得
                            //締処理エラーテーブルを検索
                            this.ShimeShoriErrSearchResult = new DataTable();

                            //検索条件を設定
                            Dictionary<string, string> searchCondition = new Dictionary<string, string>();
                            searchCondition.Add("ShoriKbn", (string)row.Cells["colShoriKbn"].Value);
                            searchCondition.Add("CheckKbn", (string)row.Cells["colCheckKbn"].Value);
                            searchCondition.Add("DenpyouShuruiCD", (string)row.Cells["colDenpyoKubunCD"].Value);
                            searchCondition.Add("SystemId", (string)row.Cells["colSystemID"].Value);
                            searchCondition.Add("Seq", (string)row.Cells["colSeq"].Value);
                            searchCondition.Add("DetailSystemId", (string)row.Cells["colDetailSystemID"].Value);

                            this.ShimeShoriErrSearchResult = SelectT_SHIME_SHORI_ERROR(searchCondition);

                            if (ShimeShoriErrSearchResult.Rows.Count == 0)
                            {
                                //追加対象レコードリストに格納
                                insertTsseList.Add(TShimeShoriErrorData(row));
                            }
                            else
                            {
                                //理由が変更されている場合のみ更新対象とする
                                string riyu;
                                if (row.Cells["colRiyu"].Value == null)
                                {
                                    riyu = string.Empty;
                                }
                                else
                                {
                                    riyu = row.Cells["colRiyu"].Value.ToString();
                                }

                                string riyuKoshinmae;
                                if (row.Cells["colRiyuKoushinmae"].Value == null)
                                {
                                    riyuKoshinmae = string.Empty;
                                }
                                else
                                {
                                    riyuKoshinmae = row.Cells["colRiyuKoushinmae"].Value.ToString();
                                }

                                if (!(riyu.Equals(riyuKoshinmae)))
                                {
                                    //更新対象レコードリストに格納
                                    updateTsseList.Add(TShimeShoriErrorData(row));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtility.Error(ex);
                        this.errmessage.MessageBoxShow("E245", "");
                        return;
                    }

                    try
                    {
                        using (Transaction tran = new Transaction())
                        {
                            //追加実行
                            foreach (T_SHIME_SHORI_ERROR val in insertTsseList)
                            {
                                InsertT_SHIME_SHORI_ERROR(val);
                            }

                            //更新実行
                            foreach (T_SHIME_SHORI_ERROR val in updateTsseList)
                            {
                                UpdateT_SHIME_SHORI_ERROR(val);
                            }
                            // コミット
                            tran.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtility.Debug(ex);

                        if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                        {
                            this.errmessage.MessageBoxShow("E080");
                        }
                        else if (ex is SQLRuntimeException)
                        {
                            this.errmessage.MessageBoxShow("E093");
                        }
                        this.isUpdate = true;
                        return;
                    }

                    try
                    {
                        //更新後のタイムスタンプを再取得
                        foreach (DataGridViewRow row in this.form.dgvErrorNaiyo.Rows)
                        {
                            string riyu;
                            if (row.Cells["colRiyu"].Value == null)
                            {
                                riyu = string.Empty;
                            }
                            else
                            {
                                riyu = row.Cells["colRiyu"].Value.ToString();
                            }

                            string riyuKoshinmae;
                            if (row.Cells["colRiyuKoushinmae"].Value == null)
                            {
                                riyuKoshinmae = string.Empty;
                            }
                            else
                            {
                                riyuKoshinmae = row.Cells["colRiyuKoushinmae"].Value.ToString();
                            }

                            //理由が変更されている場合、排他エラーが起きいる場合更新
                            if (!(riyu.Equals(riyuKoshinmae)) || isUpdate)
                            {
                                //締処理エラーテーブルを検索
                                this.ShimeShoriErrSearchResult = new DataTable();

                                //検索条件を設定
                                Dictionary<string, string> searchCondition = new Dictionary<string, string>();
                                searchCondition.Add("ShoriKbn", (string)row.Cells["colShoriKbn"].Value);
                                searchCondition.Add("CheckKbn", (string)row.Cells["colCheckKbn"].Value);
                                searchCondition.Add("DenpyouShuruiCD", (string)row.Cells["colDenpyoKubunCD"].Value);
                                searchCondition.Add("SystemId", (string)row.Cells["colSystemID"].Value);
                                searchCondition.Add("Seq", (string)row.Cells["colSeq"].Value);
                                searchCondition.Add("DetailSystemId", (string)row.Cells["colDetailSystemID"].Value);

                                this.ShimeShoriErrSearchResult = SelectT_SHIME_SHORI_ERROR(searchCondition);

                                row.Cells["colRiyu"].Value = ShimeShoriErrSearchResult.Rows[0]["RIYUU"];
                                row.Cells["colRiyuKoushinmae"].Value = row.Cells["colRiyu"].Value;
                                row.Cells["colTimeStamp"].Value = ShimeShoriErrSearchResult.Rows[0]["TIME_STAMP"];
                            }
                        }
                    }
                    catch (SQLRuntimeException ex1)
                    {
                        LogUtility.Error(ex1);
                        this.errmessage.MessageBoxShow("E093", "");
                        return;
                    }
                    catch (Exception ex)
                    {
                        LogUtility.Error(ex);
                        this.errmessage.MessageBoxShow("E245", "");
                        return;
                    }

                    if (!isUpdate)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        //登録完了メッセージ表示
                        msgLogic.MessageBoxShow(I001, "登録");
                    }

                    Cursor.Current = preCursor;
                }
            }
        }

        #endregion [F9]登録押下時

        #region グリッド締列制御関連

        /// <summary>
        /// 列ヘッダチェックボックス表示
        /// </summary>
        public bool DgvTorihikisakiIchiranCellPaintingLogic(DataGridViewCellPaintingEventArgs e)
        {
            bool ret = true;
            try
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.ColumnIndex == 0 && e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(100, 100))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp.Width - this.form.checkBoxAll.Width) / 2, (bmp.Height - this.form.checkBoxAll.Height + 28) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        this.form.checkBoxAll.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                        // DataGridViewの現在描画中のセルの中央に描画
                        int x = (e.CellBounds.Width - bmp.Width) / 2;
                        int y = (e.CellBounds.Height - bmp.Height) / 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DgvTorihikisakiIchiranCellPaintingLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 列ヘッダチェックボックス表示切替
        /// </summary>
        public void DgvTorihikisakiIchiranCellClickLogic(DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart();

            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                this.form.checkBoxAll.Checked = !this.form.checkBoxAll.Checked;
                this.form.dgvTorihikisakiIchiran.Refresh();
                this.form.checkBoxAll.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 全行の締チェックボックス切替
        /// </summary>
        public bool CheckBoxAllCheckedChangedLogic()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                bool isChecked = false;
                foreach (DataGridViewRow row in this.form.dgvTorihikisakiIchiran.Rows)
                {
                    row.Cells[0].Value = this.form.checkBoxAll.Checked;
                    isChecked = true;
                }
                if (isChecked)
                {
                    this.form.dgvTorihikisakiIchiran.CurrentCell = this.form.dgvTorihikisakiIchiran.Rows[0].Cells[0];
                }

                this.form.dgvTorihikisakiIchiran.RefreshEdit();
                this.form.dgvTorihikisakiIchiran.Refresh();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckBoxAllCheckedChangedLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion グリッド締列制御関連

        #region 締日テキストボックス変更

        public bool TxtShimeDateValidatedLogic()
        {
            LogUtility.DebugMethodStart();

            if (this.form.cmbShimebi.Text == "0")
            {
                //検索期間FROM、TOに当日を設定
                this.form.dtpSearchDateFrom.Value = this.parentForm.sysDate;
                this.form.dtpSearchDateTo.Value = this.parentForm.sysDate;
            }
            else
            {
                //検索期間FROMを未入力状態に設定
                this.form.dtpSearchDateFrom.Value = null;
            }

            if (this.form.cmbShimebi.Text != "0" && this.form.cmbShimebi.Text != "5"
                && this.form.cmbShimebi.Text != "10" && this.form.cmbShimebi.Text != "15"
                && this.form.cmbShimebi.Text != "20" && this.form.cmbShimebi.Text != "25"
                && this.form.cmbShimebi.Text != "31" && this.form.cmbShimebi.Text != "")
            {
                MessageBox.Show(SHIMEBIERRMSG, DIALOGTITLE);
                this.form.cmbShimebi.SelectedIndex = 0;
            }
            else if (this.form.cmbShimebi.Text != "")
            {
                this.form.checkBoxAll.Checked = false;
                this.form.dgvTorihikisakiIchiran.Refresh();
                // チェック結果をクリアする。
                int cnt = this.form.dgvErrorNaiyo.Rows.Count;
                for (int i = cnt; i >= 1; i--)
                {
                    this.form.dgvErrorNaiyo.Rows.RemoveAt(this.form.dgvErrorNaiyo.Rows[i - 1].Index);
                }

                Cursor preCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                //取引先データ取得
                if (!GetTorihikisaki(null))
                {
                    return false;
                }
                //取引先データグリッド設定
                if (!SetTorihikisaki())
                {
                    return false;
                }
                Cursor.Current = preCursor;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion 締日テキストボックス変更

        #region 検索期間FROMロストフォーカス

        public bool dtpSearchDateFromValidatedLogic()
        {
            bool ret = true;
            try
            {
                if (this.form.dtpSearchDateTo.Value != null && this.form.dtpSearchDateFrom.Value != null &&
                    (DateTime)this.form.dtpSearchDateTo.Value < (DateTime)this.form.dtpSearchDateFrom.Value)
                {
                    this.form.dtpSearchDateTo.Refresh();
                    this.form.dgvTorihikisakiIchiran.Refresh();
                    this.form.dgvErrorNaiyo.Refresh();

                    this.form.dtpSearchDateFrom.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("dtpSearchDateFromValidatedLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion 検索期間FROMロストフォーカス

        #region 検索期間TOロストフォーカス

        public bool dtpSearchDateToValidatedLogic()
        {
            bool ret = true;
            try
            {
                if (this.form.dtpSearchDateFrom.Value != null && this.form.dtpSearchDateTo.Value != null &&
                    (DateTime)this.form.dtpSearchDateTo.Value < (DateTime)this.form.dtpSearchDateFrom.Value)
                {
                    this.form.dtpSearchDateFrom.Refresh();
                    this.form.dgvTorihikisakiIchiran.Refresh();
                    this.form.dgvErrorNaiyo.Refresh();

                    this.form.dtpSearchDateTo.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("dtpSearchDateToValidatedLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion 検索期間TOロストフォーカス

        #region SQL実行

        /// <summary>
        /// 締処理エラーテーブルSELECT
        /// </summary>
        [Transaction]
        public virtual DataTable SelectT_SHIME_SHORI_ERROR(Dictionary<string, string> searchCondition)
        {
            //検索条件を設定
            this.ShimeShoriErrSearchString.ShoriKbn = searchCondition["ShoriKbn"];
            this.ShimeShoriErrSearchString.CheckKbn = searchCondition["CheckKbn"];
            this.ShimeShoriErrSearchString.DenpyouShuruiCD = searchCondition["DenpyouShuruiCD"];
            this.ShimeShoriErrSearchString.SystemId = searchCondition["SystemId"];
            this.ShimeShoriErrSearchString.Seq = searchCondition["Seq"];
            this.ShimeShoriErrSearchString.DetailSystemId = searchCondition["DetailSystemId"];

            //検索実行
            return SseDaoPatern.GetDataForEntity(this.ShimeShoriErrSearchString);
        }

        /// <summary>
        /// 締処理エラーテーブルINSERT/UPDATEデータ作成
        /// </summary>
        private T_SHIME_SHORI_ERROR TShimeShoriErrorData(DataGridViewRow row)
        {
            T_SHIME_SHORI_ERROR tsse = new T_SHIME_SHORI_ERROR();

            //データが存在しなければ締処理エラーテーブルにレコード追加
            tsse.SHORI_KBN = System.Data.SqlTypes.SqlInt16.Parse(row.Cells["colShoriKbn"].Value.ToString());
            tsse.CHECK_KBN = System.Data.SqlTypes.SqlInt16.Parse(row.Cells["colCheckKbn"].Value.ToString());
            tsse.DENPYOU_SHURUI_CD = System.Data.SqlTypes.SqlInt16.Parse(row.Cells["colDenpyoKubunCD"].Value.ToString());
            tsse.SYSTEM_ID = System.Data.SqlTypes.SqlInt64.Parse(row.Cells["colSystemID"].Value.ToString());
            tsse.SEQ = System.Data.SqlTypes.SqlInt32.Parse(row.Cells["colSeq"].Value.ToString());
            tsse.DETAIL_SYSTEM_ID = System.Data.SqlTypes.SqlInt64.Parse(row.Cells["colDetailSystemID"].Value.ToString());
            tsse.GYO_NUMBER = System.Data.SqlTypes.SqlInt32.Parse(row.Cells["colMeisaiGyoNoReport"].Value.ToString());
            tsse.ERROR_NAIYOU = (string)row.Cells["colErrorNaiyo"].Value;
            tsse.RIYUU = (string)row.Cells["colRiyu"].Value;
            if (!string.IsNullOrEmpty(row.Cells["colTimeStamp"].Value.ToString()))
            {
                tsse.TIME_STAMP = (byte[])row.Cells["colTimeStamp"].Value;
            }

            //システム自動設定の値を取得
            var dataBinderEntry = new DataBinderLogic<T_SHIME_SHORI_ERROR>(tsse);
            dataBinderEntry.SetSystemProperty(tsse, false);

            return tsse;
        }

        /// <summary>
        /// 締処理エラーテーブルINSERT
        /// </summary>
        [Transaction]
        public virtual void InsertT_SHIME_SHORI_ERROR(T_SHIME_SHORI_ERROR val)
        {
            SseDaoPatern.Insert(val);
        }

        /// <summary>
        /// 締処理エラーテーブルUPDATE
        /// </summary>
        [Transaction]
        public virtual void UpdateT_SHIME_SHORI_ERROR(T_SHIME_SHORI_ERROR val)
        {
            SseDaoPatern.Update(val);
        }

        /// <summary>
        /// 拠点マスタテーブル拠点名略称SELECT
        /// </summary>
        public String SelectKyotenNameRyaku(String kyotenCD, out bool catchErr)
        {
            catchErr = true;
            try
            {
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(kyotenCD);
                if (mKyoten == null)
                {
                    return "";
                }
                else
                {
                    return mKyoten.KYOTEN_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SelectKyotenNameRyaku", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SelectKyotenNameRyaku", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return "";
        }

        /// <summary>
        /// 自社情報マスタテーブル会社名SELECT
        /// </summary>
        /// <returns></returns>
        private String SelectCorpName()
        {
            M_CORP_INFO searchCorpInfo = new M_CORP_INFO();
            M_CORP_INFO[] corpInfo;

            corpInfo = (M_CORP_INFO[])mCorpInfoDao.GetAllData();
            return corpInfo[0].CORP_NAME;
        }

        #endregion SQL実行

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

        public void setHeaderForm(UIHeader hs)
        {
            this.headerForm = hs;
        }

        // 20141128 Houkakou 「支払チェック表」のダブルクリックを追加する　start

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpSearchDateTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.dtpSearchDateFrom;
            var ToTextBox = this.form.dtpSearchDateTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // 20141128 Houkakou 「支払チェック表」のダブルクリックを追加する　end
    }
}