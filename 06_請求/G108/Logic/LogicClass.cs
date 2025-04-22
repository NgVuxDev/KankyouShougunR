using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Billing.Seikyucheckhyo.DAO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Billing.Seikyucheckhyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region "プロパティ"

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
        /// システム設定検索条件
        /// </summary>
        public SysInfoDTO SysInfoSearchString { get; set; }

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

        #endregion "プロパティ"

        #region "定数"

        //伝票種類初期値
        private const String DENPYOKINDINIT = "1";

        //拠点CD初期値
        private const int KYOTENCDINIT = 99;

        //締日入力チェックメッセージ
        private const String SHIMEBIERRMSG = "【0,5,10,15,20,25,31】のみ入力してください。";

        //登録押下時未入力チェックメッセージ
        private const String TOROKUERRMSG = "該当データがありませんでした。";

        //登録押下時確認メッセージ
        private const String TOROKUINFOMSG = "エラー内容を登録しますか？";

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

        //売上・支払い区分：売上
        private const int URIAGE = 1;

        #endregion "定数"

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

        /// <summary>
        /// 拠点リスト
        /// </summary>
        private M_KYOTEN[] mKyotenList;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private BusinessBaseForm parentForm;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        internal MessageBoxShowLogic errmessage;

        #region "コンストラクタ"

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.TorihikisakiSearchString = new TorihikisakiDTO();
            this.MtsDaoPatern = DaoInitUtility.GetComponent<DAO.MTSDaoCls>();

            this.ShimeShoriErrSearchString = new ShimeShoriErrDTO();
            this.SseDaoPatern = DaoInitUtility.GetComponent<DAO.SSEDaoCls>();

            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();

            this.dbAccessor = new DBAccessor();
            this.errmessage = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion "コンストラクタ"

        #region "初期処理"

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // 拠点リストの取得
                M_KYOTEN mKyoten = new M_KYOTEN() { ISNOT_NEED_DELETE_FLG = true };
                this.mKyotenList = mkyotenDao.GetAllValidData(mKyoten);

                //================================CurrentUserCustomConfigProfile.xmlを読み込み============================
                XMLAccessor fileAccess = new XMLAccessor();
                CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

                this.headerForm.txt_KyotenCDHeader.Text = configProfile.ItemSetVal1.PadLeft(2, '0');

                //画面初期表示時、以下の項目を設定
                //【伝票種類】
                this.form.txt_DenpyoKind.Text = DENPYOKINDINIT;
                //【拠点コード】
                this.form.txt_KyotenCD.Text = KYOTENCDINIT.ToString();
                //【拠点名】
                this.form.txt_KyotenName.Text = string.Empty;
                //【締日】
                this.form.cmbShimebi.SelectedIndex = ShimeDateInit();
                //【検索期間From】
                this.form.dtp_SearchDateFrom.Value = null;
                //【検索期間To】
                this.form.dtp_SearchDateTo.Value = this.parentForm.sysDate;

                // Anchor設定
                this.form.dgv_TorihikisakiIchiran.Anchor = (AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
                this.form.dgv_ErrorNaiyo.Anchor = (AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom);

                // ユーザ拠点名称の取得
                bool catchErr = true;
                this.headerForm.txt_KyotenNameHeader.Text = SelectKyotenNameRyaku(this.headerForm.txt_KyotenCDHeader.Text, out catchErr);
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
                if (!r_framework.Authority.Manager.CheckAuthority("G108", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
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

            // 20141201 Houkakou 「運賃集計表」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.dtp_SearchDateTo.MouseDoubleClick += new MouseEventHandler(dtp_SearchDateTo_MouseDoubleClick);
            // 20141201 Houkakou 「運賃集計表」のダブルクリックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 締日の初期化処理
        /// </summary>
        private int ShimeDateInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            //システム日付を取得
            DateTime sysDate = parentForm.sysDate;
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

        #endregion "初期処理"

        #region "データ取得"

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

        #endregion "データ取得"

        #region "グリッドデータ表示"

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
                this.form.dgv_TorihikisakiIchiran.IsBrowsePurpose = false;
                this.form.dgv_TorihikisakiIchiran.SuspendLayout();

                bool isShimeCheck = false;

                //締日が0以外の場合は行締チェックボックスと全締チェックボックスをONに設定
                if (this.form.cmbShimebi.Text != "0")
                {
                    isShimeCheck = true;

                    this.form.checkBoxAll.Checked = true;
                    this.form.dgv_TorihikisakiIchiran.Refresh();
                }

                //前の結果をクリア
                int k = this.form.dgv_TorihikisakiIchiran.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.form.dgv_TorihikisakiIchiran.Rows.RemoveAt(this.form.dgv_TorihikisakiIchiran.Rows[i - 1].Index);
                }

                //検索結果を設定する
                var table = this.TorihikisakiSearchResult;
                table.BeginLoadData();

                //検索結果設定
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    this.form.dgv_TorihikisakiIchiran.Rows.Add();
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_Shime"].Value = isShimeCheck;
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_TorihikisakiCD"].Value = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_TorihikisakiName"].Value = table.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_ShimeDate1"].Value = table.Rows[i]["SHIMEBI1"];
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_ShimeDate2"].Value = table.Rows[i]["SHIMEBI2"];
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_ShimeDate3"].Value = table.Rows[i]["SHIMEBI3"];
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_Address1"].Value = table.Rows[i]["TORIHIKISAKI_ADDRESS1"].ToString();
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_Address2"].Value = table.Rows[i]["TORIHIKISAKI_ADDRESS2"].ToString();
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_Tel"].Value = table.Rows[i]["TORIHIKISAKI_TEL"].ToString();
                    this.form.dgv_TorihikisakiIchiran.Rows[i].Cells["col_Fax"].Value = table.Rows[i]["TORIHIKISAKI_FAX"].ToString();
                }

                //描画を再開
                this.form.dgv_TorihikisakiIchiran.ResumeLayout();
                this.form.dgv_TorihikisakiIchiran.IsBrowsePurpose = true;
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
            this.form.dgv_TorihikisakiIchiran.IsBrowsePurpose = false;
            this.form.dgv_ErrorNaiyo.IsBrowsePurpose = false;
            this.form.dgv_TorihikisakiIchiran.SuspendLayout();
            this.form.dgv_ErrorNaiyo.SuspendLayout();

            //共通処理より取得した値をグリッドに設定
            //前の結果をクリア
            int k = this.form.dgv_ErrorNaiyo.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.form.dgv_ErrorNaiyo.Rows.RemoveAt(this.form.dgv_ErrorNaiyo.Rows[i - 1].Index);
            }

            // グリッド設定用のDataTableを作成
            DataTable errorNaiyoTable = new DataTable();
            errorNaiyoTable.Columns.Add("col_DenpyoKubunName");
            errorNaiyoTable.Columns.Add("col_KyotenName");
            errorNaiyoTable.Columns.Add("col_UriageDate");
            errorNaiyoTable.Columns.Add("col_DenpyoNo");
            errorNaiyoTable.Columns.Add("col_ErrorNaiyo");
            errorNaiyoTable.Columns.Add("col_Riyu");
            errorNaiyoTable.Columns.Add("colRiyuKoushinmae");
            errorNaiyoTable.Columns.Add("col_TorihikisakiCDReport");
            errorNaiyoTable.Columns.Add("col_TorihikisakiRyakushoNameReport");
            errorNaiyoTable.Columns.Add("col_MeisaiGyoNoReport");
            errorNaiyoTable.Columns.Add("col_CheckKbn");
            errorNaiyoTable.Columns.Add("col_DenpyoKubunCD");
            errorNaiyoTable.Columns.Add("col_SystemID");
            errorNaiyoTable.Columns.Add("col_Seq");
            errorNaiyoTable.Columns.Add("col_DetailSystemID");
            errorNaiyoTable.Columns.Add("col_KyotenCD");
            errorNaiyoTable.Columns.Add("col_ShoriKbn");
            errorNaiyoTable.Columns.Add("colTimeStamp", Type.GetType("System.Object"));

            if (this.ErrHyojiTable != null)
            {
                //検索結果を設定する
                var table = this.ErrHyojiTable;
                table.BeginLoadData();

                //検索結果設定
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    errorNaiyoTable.Rows.Add();

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
                            denpyoKbnName = "入金伝票";
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
                    if (this.TorihikisakiSearchResult.Rows.Count > 0 
                        && !this.TorihikisakiSearchResult.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].IsNull())
                    {
                        torihikisakiRyakuName = (String)this.TorihikisakiSearchResult.Rows[0]["TORIHIKISAKI_NAME_RYAKU"];
                    }

                    errorNaiyoTable.Rows[i]["col_DenpyoKubunName"] = denpyoKbnName;
                    errorNaiyoTable.Rows[i]["col_KyotenName"] = kyotenName;
                    errorNaiyoTable.Rows[i]["col_UriageDate"] = table.Rows[i]["URIAGE_DATE"];
                    errorNaiyoTable.Rows[i]["col_DenpyoNo"] = table.Rows[i]["DENPYOU_NUMBER"];
                    errorNaiyoTable.Rows[i]["col_ErrorNaiyo"] = table.Rows[i]["ERROR_NAIYOU"];
                    errorNaiyoTable.Rows[i]["col_Riyu"] = table.Rows[i]["RIYUU"];
                    errorNaiyoTable.Rows[i]["colRiyuKoushinmae"] = table.Rows[i]["RIYUU"];
                    errorNaiyoTable.Rows[i]["col_TorihikisakiCDReport"] = table.Rows[i]["TORIHIKISAKI_CD"];
                    errorNaiyoTable.Rows[i]["col_TorihikisakiRyakushoNameReport"] = torihikisakiRyakuName;
                    errorNaiyoTable.Rows[i]["col_MeisaiGyoNoReport"] = table.Rows[i]["GYO_NUMBER"];
                    errorNaiyoTable.Rows[i]["col_CheckKbn"] = table.Rows[i]["CHECK_KBN"];
                    errorNaiyoTable.Rows[i]["col_DenpyoKubunCD"] = table.Rows[i]["DENPYOU_SHURUI_CD"];
                    errorNaiyoTable.Rows[i]["col_SystemID"] = table.Rows[i]["SYSTEM_ID"];
                    errorNaiyoTable.Rows[i]["col_Seq"] = table.Rows[i]["SEQ"];
                    errorNaiyoTable.Rows[i]["col_DetailSystemID"] = table.Rows[i]["DETAIL_SYSTEM_ID"];
                    errorNaiyoTable.Rows[i]["col_KyotenCD"] = table.Rows[i]["KYOTEN_CD"];
                    errorNaiyoTable.Rows[i]["col_ShoriKbn"] = table.Rows[i]["SHORI_KBN"];
                    errorNaiyoTable.Rows[i]["colTimeStamp"] = table.Rows[i]["TIME_STAMP"];
                }
            }

            // 列が自動的に作成されないようにする
            this.form.dgv_ErrorNaiyo.AutoGenerateColumns = false;
            this.form.dgv_ErrorNaiyo.DataSource = errorNaiyoTable;

            //描画を再開
            this.form.dgv_TorihikisakiIchiran.ResumeLayout();
            this.form.dgv_ErrorNaiyo.ResumeLayout();
            this.form.dgv_TorihikisakiIchiran.IsBrowsePurpose = true;
            this.form.dgv_ErrorNaiyo.IsBrowsePurpose = true;

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion "グリッドデータ表示

        #region "入力チェック"

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
            if (this.form.txt_DenpyoKind.Text == string.Empty)
            {
                checkOk = false;
                errKomoku += DENPYOSHURUI;

                if (focusControl == null)
                {
                    focusControl = DENPYOSHURUI;
                }
            }

            //拠点CD
            if (this.form.txt_KyotenCD.Text == string.Empty)
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
            if ((this.form.dtp_SearchDateFrom.Value == null) && (this.form.dtp_SearchDateTo.Value == null))
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

            foreach (DataGridViewRow row in this.form.dgv_TorihikisakiIchiran.Rows)
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

            if ((DateTime)this.form.dtp_SearchDateTo.Value < (DateTime)this.form.dtp_SearchDateFrom.Value)
            {
                // 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　start
                this.form.dtp_SearchDateFrom.IsInputErrorOccured = true;
                this.form.dtp_SearchDateTo.IsInputErrorOccured = true;
                this.form.dtp_SearchDateFrom.BackColor = Constans.ERROR_COLOR;
                this.form.dtp_SearchDateTo.BackColor = Constans.ERROR_COLOR;
                // 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　end
                checkOk = false;
            }

            LogUtility.DebugMethodEnd();

            return checkOk;
        }

        #endregion "入力チェック"

        #region "[F3]前月、[F4]翌月押下時"

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

                if (this.form.dtp_SearchDateFrom.Value != null)
                {
                    //月末か判定
                    DateTime fromD1 = (DateTime)this.form.dtp_SearchDateFrom.Value;
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

                if (this.form.dtp_SearchDateTo.Value != null)
                {
                    //月末か判定
                    DateTime toD1 = (DateTime)this.form.dtp_SearchDateTo.Value;
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

        #endregion "[F3]前月、[F4]翌月押下時"

        #region [F5]印刷押下時

        internal bool Function5ClickLogic()
        {
            bool ret = true;
            DataTable csvDT = new DataTable();
            DataRow rowTmp;
            try
            {
                if (this.form.dgv_ErrorNaiyo.Rows.Count == 0)
                {
                    return ret;
                }

                string[] csvHead = { "区分","拠点", "売上日付", "伝票番号", "取引先CD", "取引先", 
                                         "明細番号", "エラー内容", "理由"};
                for (int i = 0; i < csvHead.Length; i++)
                {
                    csvDT.Columns.Add(csvHead[i]);
                }

                foreach (DataGridViewRow row in this.form.dgv_ErrorNaiyo.Rows)
                {
                    rowTmp = csvDT.NewRow();

                    if (row.Cells["col_DenpyoKubunName"] != null && !string.IsNullOrEmpty(row.Cells["col_DenpyoKubunName"].Value.ToString()))
                    {
                        rowTmp["区分"] = row.Cells["col_DenpyoKubunName"].Value.ToString();
                    }

                    if (row.Cells["col_KyotenName"] != null && !string.IsNullOrEmpty(row.Cells["col_KyotenName"].Value.ToString()))
                    {
                        rowTmp["拠点"] = row.Cells["col_KyotenName"].Value.ToString();
                    }

                    if (row.Cells["col_UriageDate"] != null && !string.IsNullOrEmpty(row.Cells["col_UriageDate"].Value.ToString()))
                    {
                        rowTmp["売上日付"] = Convert.ToDateTime(row.Cells["col_UriageDate"].Value.ToString()).ToString("yyyy/MM/dd");
                    }

                    if (row.Cells["col_DenpyoNo"] != null && !string.IsNullOrEmpty(row.Cells["col_DenpyoNo"].Value.ToString()))
                    {
                        rowTmp["伝票番号"] = row.Cells["col_DenpyoNo"].Value.ToString();
                    }

                    if (row.Cells["col_TorihikisakiCDReport"] != null && !string.IsNullOrEmpty(row.Cells["col_TorihikisakiCDReport"].Value.ToString()))
                    {
                        rowTmp["取引先CD"] = row.Cells["col_TorihikisakiCDReport"].Value.ToString();
                    }

                    if (row.Cells["col_TorihikisakiRyakushoNameReport"] != null && !string.IsNullOrEmpty(row.Cells["col_TorihikisakiRyakushoNameReport"].Value.ToString()))
                    {
                        rowTmp["取引先"] = row.Cells["col_TorihikisakiRyakushoNameReport"].Value.ToString();
                    }

                    if (row.Cells["col_MeisaiGyoNoReport"] != null && !string.IsNullOrEmpty(row.Cells["col_MeisaiGyoNoReport"].Value.ToString()))
                    {
                        rowTmp["明細番号"] = row.Cells["col_MeisaiGyoNoReport"].Value.ToString();
                    }

                    if (row.Cells["col_ErrorNaiyo"] != null && !string.IsNullOrEmpty(row.Cells["col_ErrorNaiyo"].Value.ToString()))
                    {
                        rowTmp["エラー内容"] = row.Cells["col_ErrorNaiyo"].Value.ToString();
                    }

                    if (row.Cells["col_Riyu"] != null && !string.IsNullOrEmpty(row.Cells["col_Riyu"].Value.ToString()))
                    {
                        rowTmp["理由"] = row.Cells["col_Riyu"].Value.ToString();
                    }
                    csvDT.Rows.Add(rowTmp);
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_SEIKYUU_CHECKHYOU), this.form);
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
                if (this.form.dgv_ErrorNaiyo.Rows.Count == 0)
                {
                    return ret;
                }

                DataTable dt = new DataTable();

                dt.Columns.Add();

                System.Text.StringBuilder sBuilder;
                foreach (DataGridViewRow row in this.form.dgv_ErrorNaiyo.Rows)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    sBuilder = new StringBuilder();

                    sBuilder.Append("\"");
                    sBuilder.Append("1-1");
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_DenpyoKubunName"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_KyotenName"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_UriageDate"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_DenpyoNo"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_TorihikisakiCDReport"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_TorihikisakiRyakushoNameReport"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_MeisaiGyoNoReport"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_ErrorNaiyo"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_Riyu"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(corpName);
                    sBuilder.Append("\"");

                    dr[0] = sBuilder.ToString();
                    dt.Rows.Add(dr);
                }

                ReportInfoR382 report_r382 = new ReportInfoR382();

                report_r382.R382_Report(dt);

                // 印刷ポツプアップ画面表示
                using (FormReportPrintPopup report = new FormReportPrintPopup(report_r382))
                {
                    //レポートタイトルの設定
                    report.ReportCaption = "請求チェック表";

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

        #region "[F8]検索押下時"

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
                    if (!(this.form.dtp_SearchDateFrom.Value == null) && !(this.form.dtp_SearchDateTo.Value == null))
                    {
                        //検索期間の関連チェック
                        if (!SearchDateCheck())
                        {
                            //エラーメッセージ表示
                            // 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　start
                            msgLogic.MessageBoxShow("E030", "検索期間From", "検索期間To");
                            // 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　start

                            //フォーカス設定
                            this.form.dtp_SearchDateFrom.Focus();

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
                        this.form.txt_DenpyoKind.Focus();
                    }
                    else if (focusControl == KYOTENCD)
                    {
                        this.form.txt_KyotenCD.Focus();
                    }
                    else if (focusControl == SHIMEBI)
                    {
                        this.form.cmbShimebi.Focus();
                    }
                    else if (focusControl == SEARCHDATE)
                    {
                        this.form.dtp_SearchDateFrom.Focus();
                    }
                    else
                    {
                        this.form.dgv_TorihikisakiIchiran.Focus();
                        if (this.form.dgv_TorihikisakiIchiran.Rows.Count != 0)
                        {
                            this.form.dgv_TorihikisakiIchiran.CurrentCell = this.form.dgv_TorihikisakiIchiran[0, 0];
                        }
                    }

                    isSearch = false;
                }

                if (isSearch)
                {
                    DialogResult result = DialogResult.Yes;

                    //確認メッセージ表示
                    result = msgLogic.MessageBoxShow(C030, "請求");

                    if (result == DialogResult.Yes)
                    {
                        //検索処理実行
                        Cursor preCursor = Cursor.Current;
                        Cursor.Current = Cursors.WaitCursor;
                        ShimeCheck();
                        if (!SetCheckKekka())
                        {
                            ret = false;
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

            LogUtility.DebugMethodEnd(ret);
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
            var parentForm = (BusinessBaseForm)this.form.Parent;

            //エラー表示内容テーブルの初期化
            ErrHyojiTable = null;

            //締めチェックボックスがONのレコードを取得
            foreach (DataGridViewRow row in this.form.dgv_TorihikisakiIchiran.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    SeikyuShimeChk = new CheckDto();

                    string dateFrom = "";
                    string dateTo = "";
                    if (this.form.dtp_SearchDateFrom.Value != null)
                    {
                        dateFrom = this.form.dtp_SearchDateFrom.Value.ToString();
                        dateFrom = dateFrom.Remove(dateFrom.IndexOf(" "));
                        dateFrom = dateFrom + " 0:00:00";
                    }

                    if (this.form.dtp_SearchDateTo.Value != null)
                    {
                        dateTo = this.form.dtp_SearchDateTo.Value.ToString();
                    }
                    else
                    {
                        dateTo = parentForm.sysDate.ToString();
                    }
                    dateTo = dateTo.Remove(dateTo.IndexOf(" "));
                    dateTo = dateTo + " 0:00:00";

                    SeikyuShimeChk.TORIHIKISAKI_CD = (string)row.Cells["col_TorihikisakiCD"].Value;
                    SeikyuShimeChk.KYOTEN_CD = int.Parse(this.form.txt_KyotenCD.Text);
                    SeikyuShimeChk.DENPYOU_SHURUI = int.Parse(this.form.txt_DenpyoKind.Text);
                    SeikyuShimeChk.KIKAN_FROM = dateFrom;
                    SeikyuShimeChk.KIKAN_TO = dateTo;
                    SeikyuShimeChk.SHIYOU_GAMEN = SHIYOUGMN;
                    SeikyuShimeChk.SHIME_TANI = SeikyuuShimeShoriKbn;
                    SeikyuShimeChk.URIAGE_SHIHARAI_KBN = URIAGE;

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

                T_SHIME_SHORI_ERROR[] aryDtoShimeShoriError = SelectT_SHIME_SHORI_ERROR();

                Dictionary<String, T_SHIME_SHORI_ERROR> dicShimeShoriError = new Dictionary<string, T_SHIME_SHORI_ERROR>();
                if (aryDtoShimeShoriError != null && aryDtoShimeShoriError.Length > 0)
                {
                    foreach (T_SHIME_SHORI_ERROR dtoShimeShoriError in aryDtoShimeShoriError)
                    {
                        string strShoriKbn = dtoShimeShoriError.SHORI_KBN.ToString();
                        string strCheckKbn = dtoShimeShoriError.CHECK_KBN.ToString();
                        string strDenpyoKindCd = dtoShimeShoriError.DENPYOU_SHURUI_CD.ToString();
                        string strSystemId = dtoShimeShoriError.SYSTEM_ID.ToString();
                        string strSeq = dtoShimeShoriError.SEQ.ToString();
                        string strDetailSytemId = dtoShimeShoriError.DETAIL_SYSTEM_ID.ToString();

                        string keys =
                            strShoriKbn
                            + ',' + strCheckKbn
                            + ',' + strDenpyoKindCd
                            + ',' + strSystemId
                            + ',' + strSeq
                            + ',' + strDetailSytemId;

                        if (!dicShimeShoriError.ContainsKey(keys))
                        {
                            dicShimeShoriError.Add(keys, dtoShimeShoriError);
                        }
                    }
                }

                foreach (DataRow row in aftsorttable.Rows)
                {
                    //締処理エラーテーブルを検索
                    //this.ShimeShoriErrSearchResult = new DataTable();

                    //検索実行
                    //Dictionary<string, string> searchCondition = new Dictionary<string, string>();
                    //searchCondition.Add("ShoriKbn", row["SHORI_KBN"].ToString());
                    //searchCondition.Add("CheckKbn", row["CHECK_KBN"].ToString());
                    //searchCondition.Add("DenpyouShuruiCD", row["DENPYOU_SHURUI_CD"].ToString());
                    //searchCondition.Add("SystemId", row["SYSTEM_ID"].ToString());
                    //searchCondition.Add("Seq", row["SEQ"].ToString());
                    //searchCondition.Add("DetailSystemId", row["DETAIL_SYSTEM_ID"].ToString());

                    //this.ShimeShoriErrSearchResult = SelectT_SHIME_SHORI_ERROR();

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

                    string strShoriKbn = row["SHORI_KBN"].ToString();
                    string strCheckKbn = row["CHECK_KBN"].ToString();
                    string strDenpyoKindCd = row["DENPYOU_SHURUI_CD"].ToString();
                    string strSystemId = row["SYSTEM_ID"].ToString();
                    string strSeq = row["SEQ"].ToString();
                    string strDetailSytemId = row["DETAIL_SYSTEM_ID"].ToString();

                    string keys =
                        strShoriKbn
                        + ',' + strCheckKbn
                        + ',' + strDenpyoKindCd
                        + ',' + strSystemId
                        + ',' + strSeq
                        + ',' + strDetailSytemId;

                    if (!dicShimeShoriError.ContainsKey(keys))
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
                        errHyojiRow["URIAGE_DATE"] = row["URIAGE_DATE"].ToString();
                        errHyojiRow["DENPYOU_NUMBER"] = row["DENPYOU_NUMBER"].ToString();
                    }
                    else
                    {
                        T_SHIME_SHORI_ERROR dtoShimeShoriError = dicShimeShoriError[keys];

                        string gyoNo;
                        if (dtoShimeShoriError.GYO_NUMBER.ToString() == "")
                        {
                            gyoNo = "0";
                        }
                        else
                        {
                            gyoNo = dtoShimeShoriError.GYO_NUMBER.ToString();
                        }

                        //レコード取得できた場合は行番号、エラー内容、理由の項目を取得値で更新
                        //その他の項目は共通締めチェッククラスから取得したレコードの値を使用
                        errHyojiRow["SHORI_KBN"] = row["SHORI_KBN"];
                        errHyojiRow["CHECK_KBN"] = row["CHECK_KBN"];
                        errHyojiRow["DENPYOU_SHURUI_CD"] = row["DENPYOU_SHURUI_CD"];
                        errHyojiRow["SYSTEM_ID"] = row["SYSTEM_ID"];
                        errHyojiRow["SEQ"] = row["SEQ"];
                        errHyojiRow["DETAIL_SYSTEM_ID"] = detailSystemID;
                        errHyojiRow["GYO_NUMBER"] = gyoNo;
                        errHyojiRow["ERROR_NAIYOU"] = dtoShimeShoriError.ERROR_NAIYOU;
                        errHyojiRow["RIYUU"] = dtoShimeShoriError.RIYUU;
                        errHyojiRow["TORIHIKISAKI_CD"] = row["TORIHIKISAKI_CD"];
                        errHyojiRow["KYOTEN_CD"] = row["KYOTEN_CD"];
                        errHyojiRow["URIAGE_DATE"] = row["URIAGE_DATE"];
                        errHyojiRow["DENPYOU_NUMBER"] = row["DENPYOU_NUMBER"];
                        errHyojiRow["TIME_STAMP"] = dtoShimeShoriError.TIME_STAMP;
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
            string shimeTani = sysInfo.SEIKYUU_SHIME_SHORI_KBN.ToString();

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

        #endregion "[F8]検索押下時"

        #region "[F9]登録押下時"

        [Transaction]
        public virtual bool Function9ClickLogic()
        {
            bool ret = true;
            try
            {
                //他画面更新ありフラグを初期化
                isUpdate = false;

                //未入力チェック
                if (this.form.dgv_ErrorNaiyo.Rows.Count == 0)
                {
                    MessageBox.Show(TOROKUERRMSG, DIALOGTITLE);
                }
                else
                {
                    DialogResult result = this.errmessage.MessageBoxShowConfirm(TOROKUINFOMSG);

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

                        foreach (DataGridViewRow row in this.form.dgv_ErrorNaiyo.Rows)
                        {
                            //レコードがテーブルに存在するかデータを取得
                            //締処理エラーテーブルを検索
                            this.ShimeShoriErrSearchResult = new DataTable();

                            //検索条件を設定
                            Dictionary<string, string> searchCondition = new Dictionary<string, string>();
                            searchCondition.Add("ShoriKbn", (string)row.Cells["col_ShoriKbn"].Value);
                            searchCondition.Add("CheckKbn", (string)row.Cells["col_CheckKbn"].Value);
                            searchCondition.Add("DenpyouShuruiCD", (string)row.Cells["col_DenpyoKubunCD"].Value);
                            searchCondition.Add("SystemId", (string)row.Cells["col_SystemID"].Value);
                            searchCondition.Add("Seq", (string)row.Cells["col_Seq"].Value);
                            searchCondition.Add("DetailSystemId", (string)row.Cells["col_DetailSystemID"].Value);

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
                                if (row.Cells["col_Riyu"].Value == null)
                                {
                                    riyu = string.Empty;
                                }
                                else
                                {
                                    riyu = row.Cells["col_Riyu"].Value.ToString();
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

                        try
                        {
                            // トランザクション開始
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
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow(E080);
                                isUpdate = true;
                            }
                            else
                            {
                                throw;
                            }
                        }

                        //更新後のタイムスタンプを再取得
                        foreach (DataGridViewRow row in this.form.dgv_ErrorNaiyo.Rows)
                        {
                            string riyu;
                            if (row.Cells["col_Riyu"].Value == null)
                            {
                                riyu = string.Empty;
                            }
                            else
                            {
                                riyu = row.Cells["col_Riyu"].Value.ToString();
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
                                searchCondition.Add("ShoriKbn", (string)row.Cells["col_ShoriKbn"].Value);
                                searchCondition.Add("CheckKbn", (string)row.Cells["col_CheckKbn"].Value);
                                searchCondition.Add("DenpyouShuruiCD", (string)row.Cells["col_DenpyoKubunCD"].Value);
                                searchCondition.Add("SystemId", (string)row.Cells["col_SystemID"].Value);
                                searchCondition.Add("Seq", (string)row.Cells["col_Seq"].Value);
                                searchCondition.Add("DetailSystemId", (string)row.Cells["col_DetailSystemID"].Value);

                                this.ShimeShoriErrSearchResult = SelectT_SHIME_SHORI_ERROR(searchCondition);

                                row.Cells["col_Riyu"].Value = ShimeShoriErrSearchResult.Rows[0]["RIYUU"];
                                row.Cells["colRiyuKoushinmae"].Value = row.Cells["col_Riyu"].Value;
                                row.Cells["colTimeStamp"].Value = ShimeShoriErrSearchResult.Rows[0]["TIME_STAMP"];
                            }
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
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Function9ClickLogic", ex1);
                this.errmessage.MessageBoxShow("E080", "");
                ret = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Function9ClickLogic", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function9ClickLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion "[F9]登録押下時"

        #region "グリッド締列制御関連"

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
                this.form.dgv_TorihikisakiIchiran.Refresh();
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
                foreach (DataGridViewRow row in this.form.dgv_TorihikisakiIchiran.Rows)
                {
                    row.Cells[0].Value = this.form.checkBoxAll.Checked;
                    isChecked = true;
                }
                if (isChecked)
                {
                    this.form.dgv_TorihikisakiIchiran.CurrentCell = this.form.dgv_TorihikisakiIchiran.Rows[0].Cells[0];
                }

                this.form.dgv_TorihikisakiIchiran.RefreshEdit();
                this.form.dgv_TorihikisakiIchiran.Refresh();
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

        #endregion "グリッド締列制御関連"

        #region "締日テキストボックス変更"

        public bool TxtShimeDateValidatedLogic()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.cmbShimebi.Text == "0")
                {
                    //検索期間FROM、TOに当日を設定
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //this.form.dtp_SearchDateFrom.Value = DateTime.Now;
                    //this.form.dtp_SearchDateTo.Value = DateTime.Now;
                    this.form.dtp_SearchDateFrom.Value = this.parentForm.sysDate;
                    this.form.dtp_SearchDateTo.Value = this.parentForm.sysDate;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                }
                else
                {
                    //検索期間FROMを未入力状態に設定
                    this.form.dtp_SearchDateFrom.Value = null;
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
                    this.form.dgv_TorihikisakiIchiran.Refresh();
                    // チェック結果をクリアする。
                    int cnt = this.form.dgv_ErrorNaiyo.Rows.Count;
                    for (int i = cnt; i >= 1; i--)
                    {
                        this.form.dgv_ErrorNaiyo.Rows.RemoveAt(this.form.dgv_ErrorNaiyo.Rows[i - 1].Index);
                    }

                    Cursor preCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    //取引先データ取得
                    if (!GetTorihikisaki(null))
                    {
                        ret = false;
                        return ret;
                    }
                    //取引先データグリッド設定
                    if (!SetTorihikisaki())
                    {
                        ret = false;
                        return ret;
                    }
                    Cursor.Current = preCursor;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TxtShimeDateValidatedLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion "締日テキストボックス変更"

        #region "検索期間FROMロストフォーカス"

        public void dtpSearchDateFromValidatedLogic()
        {
            if (this.form.dtp_SearchDateTo.Value != null && this.form.dtp_SearchDateFrom.Value != null &&
                (DateTime)this.form.dtp_SearchDateTo.Value < (DateTime)this.form.dtp_SearchDateFrom.Value)
            {
                this.form.dtp_SearchDateTo.Refresh();
                this.form.dgv_TorihikisakiIchiran.Refresh();
                this.form.dgv_ErrorNaiyo.Refresh();

                this.form.dtp_SearchDateFrom.Focus();
            }
        }

        #endregion "検索期間FROMロストフォーカス"

        #region "検索期間TOロストフォーカス"

        public void dtpSearchDateToValidatedLogic()
        {
            if (this.form.dtp_SearchDateFrom.Value != null && this.form.dtp_SearchDateTo.Value != null &&
                (DateTime)this.form.dtp_SearchDateTo.Value < (DateTime)this.form.dtp_SearchDateFrom.Value)
            {
                this.form.dtp_SearchDateFrom.Refresh();
                this.form.dgv_TorihikisakiIchiran.Refresh();
                this.form.dgv_ErrorNaiyo.Refresh();

                this.form.dtp_SearchDateTo.Focus();
            }
        }

        #endregion "検索期間TOロストフォーカス"

        #region "SQL実行"

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
        /// 締処理エラーテーブルSELECT
        /// </summary>
        [Transaction]
        public virtual T_SHIME_SHORI_ERROR[] SelectT_SHIME_SHORI_ERROR()
        {
            //検索条件を設定

            //検索実行
            return SseDaoPatern.GetAllData();
        }

        /// <summary>
        /// 締処理エラーテーブルINSERT/UPDATEデータ作成
        /// </summary>
        private T_SHIME_SHORI_ERROR TShimeShoriErrorData(DataGridViewRow row)
        {
            T_SHIME_SHORI_ERROR tsse = new T_SHIME_SHORI_ERROR();

            //データが存在しなければ締処理エラーテーブルにレコード追加
            tsse.SHORI_KBN = System.Data.SqlTypes.SqlInt16.Parse(row.Cells["col_ShoriKbn"].Value.ToString());
            tsse.CHECK_KBN = System.Data.SqlTypes.SqlInt16.Parse(row.Cells["col_CheckKbn"].Value.ToString());
            tsse.DENPYOU_SHURUI_CD = System.Data.SqlTypes.SqlInt16.Parse(row.Cells["col_DenpyoKubunCD"].Value.ToString());
            tsse.SYSTEM_ID = System.Data.SqlTypes.SqlInt64.Parse(row.Cells["col_SystemID"].Value.ToString());
            tsse.SEQ = System.Data.SqlTypes.SqlInt32.Parse(row.Cells["col_Seq"].Value.ToString());
            tsse.DETAIL_SYSTEM_ID = System.Data.SqlTypes.SqlInt64.Parse(row.Cells["col_DetailSystemID"].Value.ToString());
            tsse.GYO_NUMBER = System.Data.SqlTypes.SqlInt32.Parse(row.Cells["col_MeisaiGyoNoReport"].Value.ToString());
            tsse.ERROR_NAIYOU = (string)row.Cells["col_ErrorNaiyo"].Value;
            tsse.RIYUU = (string)row.Cells["col_Riyu"].Value;
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
            String kyotenNameRyaku = "";
            catchErr = true;

            try
            {
                if (string.IsNullOrEmpty(kyotenCD))
                {
                    return kyotenNameRyaku;
                }

                foreach (M_KYOTEN kyoten in this.mKyotenList)
                {
                    if (kyoten.KYOTEN_CD == Convert.ToInt16(kyotenCD))
                    {
                        kyotenNameRyaku = kyoten.KYOTEN_NAME_RYAKU;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SelectKyotenNameRyaku", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return kyotenNameRyaku;
        }

        /// <summary>
        /// 自社情報マスタテーブル会社名SELECT
        /// </summary>
        /// <returns></returns>
        private String SelectCorpName()
        {
            M_CORP_INFO[] corpInfo;

            corpInfo = (M_CORP_INFO[])mCorpInfoDao.GetAllData();
            return corpInfo[0].CORP_NAME;
        }

        #endregion "SQL実行"

        // 20141201 Houkakou 「運賃集計表」のダブルクリックを追加する　start

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_SearchDateTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.dtp_SearchDateFrom;
            var ToTextBox = this.form.dtp_SearchDateTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // 20141201 Houkakou 「運賃集計表」のダブルクリックを追加する　end

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
    }
}