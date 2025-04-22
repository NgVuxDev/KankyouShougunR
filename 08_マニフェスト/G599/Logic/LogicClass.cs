using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Message;
using System.Data.SqlTypes;
using r_framework.Dao;
using CommonChouhyouPopup.App;
using r_framework.Authority;
using Seasar.Framework.Exceptions;
namespace Shougun.Core.PaperManifest.ManifestSyuuryoubiIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestSyuuryoubiIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;
        private BusinessBaseForm footer;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        private MessageBoxShowLogic MsgBox;
        #endregion

        #region プロパティ

        /// <summary>
        /// DTO
        /// </summary>
        private GetSyuuryobiIchiranDaoCls dto_TME;

        /// <summary>
        /// DTO
        /// </summary>
        private M_SYS_INFODaoCls dto_Sys;

        /// <summary>検索結果</summary>
        public DataTable SearchResult { get; set; }

        // 20140715 katen start
        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }
        // 20140715 katen end

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            //DAO
            this.dto_TME = DaoInitUtility.GetComponent<GetSyuuryobiIchiranDaoCls>();
            //警告日数取得
            this.dto_Sys = DaoInitUtility.GetComponent<M_SYS_INFODaoCls>();

            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.MsgBox = new MessageBoxShowLogic();
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.header = targetHeader;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            this.header.lb_title.Text = "マニフェスト終了日警告一覧";
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();

            //親フォームのボタン表示
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

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

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 印刷ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

            //修正ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            // 20140715 katen start
            //並び替え(F10)イベント生成
            parentForm.bt_func10.Click += new System.EventHandler(this.form.bt_func10_Click);
            // 20140715 katen end‏‏

            //フィルタ(F11)イベント生成
            parentForm.bt_func11.Click += new System.EventHandler(this.form.bt_func11_Click);        

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // 一覧
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.form.customDataGridView1_CellDoubleClick);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
                // 継承したフォームのDGVのプロパティはデザイナで変更できない為、ここで設定
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;
                // 20140715 katen start‏
                //アラート件数
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                this.header.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                this.header.NumberAlert = this.header.InitialNumberAlert;
                // システム情報からアラート件数を取得
                this.alertCount = (int)mSysInfo.ICHIRAN_ALERT_KENSUU;

                //読込データ件数
                this.header.ReadDataNumber.Text = "0";

                //アラート件数
                this.header.alertNumber.Text = this.header.NumberAlert.ToString();
                // 20140715 katen end‏

                //画面一覧データ取得する
                if (this.Search() == -1)
                {
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion
        /// <summary>
        /// 検索
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            Int32 count_TME = 0;
            try
            {
                //明細データ
                this.SearchResult = new DataTable();

                TMEDtoCls SearchString = new TMEDtoCls();

                //システム設定（M_SYS_INFO）を取得
                DataTable dtsys = new DataTable();
                SearchString.SYSTEM_ID = "0";
                dtsys = this.dto_Sys.GetDataForEntity(SearchString);

                if (dtsys.Rows.Count > 0)
                {
                    SearchString.sys_nitsusuu_upn = String.IsNullOrEmpty(dtsys.Rows[0]["MANIFEST_UNPAN_DAYS"].ToString()) ? 0 : SqlInt16.Parse(dtsys.Rows[0]["MANIFEST_UNPAN_DAYS"].ToString());
                    SearchString.sys_nitsusuu_sbu = String.IsNullOrEmpty(dtsys.Rows[0]["MANIFEST_SBN_DAYS"].ToString()) ? 0 : SqlInt16.Parse(dtsys.Rows[0]["MANIFEST_SBN_DAYS"].ToString());
                    SearchString.sys_nitsusuu_tk_sbu = String.IsNullOrEmpty(dtsys.Rows[0]["MANIFEST_TOK_SBN_DAYS"].ToString()) ? 0 : SqlInt16.Parse(dtsys.Rows[0]["MANIFEST_TOK_SBN_DAYS"].ToString());
                    SearchString.sys_nitsusuu_last_sbu = String.IsNullOrEmpty(dtsys.Rows[0]["MANIFEST_LAST_SBN_DAYS"].ToString()) ? 0 : SqlInt16.Parse(dtsys.Rows[0]["MANIFEST_LAST_SBN_DAYS"].ToString());

                    this.SearchResult = this.dto_TME.GetDataForEntity(SearchString);
                    count_TME = this.SearchResult.Rows.Count;
                    // 20140715 katen start‏
                    //if (this.SearchResult.Rows.Count > 0)
                    //{
                    //    this.form.customDataGridView1.DataSource = this.SearchResult;                       
                    //}
                    //else
                    //{
                    //    this.SearchResult = null;                       
                    //}
                    //初期化
                    this.form.customDataGridView1.DataSource = null;
                    this.form.customDataGridView1.Rows.Clear();
                    this.form.customDataGridView1.Columns.Clear();

                    this.form.Table = this.SearchResult;

                    // 20140715 katen end‏
                    //初期列名設定
                    this.form.SetInitCol();

                    //読込データ件数
                    if (this.form.customDataGridView1 != null)
                    {
                        this.header.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                    }
                    else
                    {
                        this.header.ReadDataNumber.Text = "0";
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                count_TME = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count_TME);
            }
            //取得件数
            return count_TME;
        }

        /// <summary>
        /// DB値有無判断
        /// </summary>
        private bool IsNullOrEmpty(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// DB値変換
        /// </summary>
        private string DbToString(object obj)
        {
            if (IsNullOrEmpty(obj))
            {
                return string.Empty;
            }

            return obj.ToString();
        }

        /// <summary>
        /// 日付フォーマット
        /// </summary>
        private string DateFormat(object obj)
        {
            string objStr = DbToString(obj);
            if (objStr.Length == 8)
            {
                objStr = objStr.Substring(0, 4) + "/" + objStr.Substring(4, 2) + "/" + objStr.Substring(6, 2);
            }

            return objStr;
        }


        public void Update(bool bl)
        {
        }

        public void Regist(bool bl)
        {
        }
        public void PhysicalDelete()
        {
        }
        public void LogicalDelete()
        {
        }

        /// <summary>
        /// 明細表作成
        /// </summary>
        internal void Print()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
                {
                    #region 印刷データ定義 - Detail
                    DataTable printDetailData = new DataTable();
                    //廃棄区分
                    printDetailData.Columns.Add("D_HAIKI_KBN_VLB");
                    //終了日警告日数
                    printDetailData.Columns.Add("D_SYUURYOBI_NITSU_SUU_VLB");
                    //経過日数
                    printDetailData.Columns.Add("D_KEIKA_NITSU_CNT_VLB");
                    //交付番号
                    printDetailData.Columns.Add("D_KOUHU_NUM_VLB");
                    //排出事業者名
                    printDetailData.Columns.Add("D_HST_GYOSHA_NAME_VLB");
                    //排出事業場名
                    printDetailData.Columns.Add("D_HST_GENBA_NAME_VLB");
                    //運搬受託者名
                    printDetailData.Columns.Add("D_UPN_JTK_NAME_VLB");
                    //処分受託者名
                    printDetailData.Columns.Add("D_SHOBU_JTK_NAME_VLB");
                    //最終処分場所名
                    printDetailData.Columns.Add("D_LAST_SBN_GB_NAME_VLB");
                    //廃棄物種類
                    printDetailData.Columns.Add("D_HKSR_VLB");
                    //数量
                    printDetailData.Columns.Add("D_SUURYO_VLB");
                    //単位
                    printDetailData.Columns.Add("D_TANI_VLB");

                    #endregion


                    #region 印刷データ定義 - Header
                    DataTable printHeaderData = new DataTable();
                    // 自社名
                    printHeaderData.Columns.Add("FH_CORP_RYAKU_NAME_VLB");
                    // 発行日(年月日時分秒)
                    printHeaderData.Columns.Add("FH_PRINT_DATE_VLB");
                    // 自社拠点
                    printHeaderData.Columns.Add("FH_KYOTEN_NAME_RYAKU_VLB");

                    #endregion

                    /* 印刷データ(Header)作成 */
                    DataRow printHeaderRow = printHeaderData.NewRow();

                    M_CORP_INFO[] kaiShaInfo;
                    kaiShaInfo = GetCorpName();
                    // 自社情報
                    printHeaderRow["FH_CORP_RYAKU_NAME_VLB"] = kaiShaInfo[0].CORP_RYAKU_NAME;
                    // 自社拠点
                    printHeaderRow["FH_KYOTEN_NAME_RYAKU_VLB"] = kaiShaInfo[0].BANK_CD;

                    // 発効日
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //DateTime dt = DateTime.Now;
                    DateTime dt = this.getDBDateTime();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    string dateTime = dt.ToString("yyyy/MM/dd HH:mm:ss");
                    printHeaderRow["FH_PRINT_DATE_VLB"] = dateTime;

                    printHeaderData.Rows.Add(printHeaderRow);

                    //並び替え

                    DataView dv = new DataView();
                    dv = this.SearchResult.DefaultView;
                    dv.Sort = "HAIKI_KBN_CD,一次二次,終了日警告区分,経過日数";
                    this.SearchResult = dv.ToTable();

                    DataTable dtsys = new DataTable();
                    TMEDtoCls SearchString = new TMEDtoCls();
                    SearchString.SYSTEM_ID = "0";
                    dtsys = this.dto_Sys.GetDataForEntity(SearchString);

                    // 印刷データ(Detail)一行分作成
                    for (int i = 0; i < this.SearchResult.Rows.Count; i++)
                    {
                        DataRow printDetailRow = printDetailData.NewRow();

                        var dHaikiKbnVlb = String.Empty;
                        var ichijiNijiKbn = this.SearchResult.Rows[i]["一次二次"].ToString();
                        var haikiKbnCd = this.SearchResult.Rows[i]["HAIKI_KBN_CD"].ToString();
                        if ("1" == haikiKbnCd)
                        {
                            dHaikiKbnVlb = "産廃マニフェスト(直行)" + ichijiNijiKbn;
                        }
                        else if ("2" == haikiKbnCd)
                        {
                            dHaikiKbnVlb = "建廃マニフェスト" + ichijiNijiKbn;
                        }
                        else if ("3" == haikiKbnCd)
                        {
                            dHaikiKbnVlb = "産廃マニフェスト" + "(" + this.SearchResult.Rows[i]["廃棄物区分"].ToString() + ")" + ichijiNijiKbn;
                        }
                        else if ("4" == haikiKbnCd)
                        {
                            dHaikiKbnVlb = "電子マニフェスト" + " " + ichijiNijiKbn;
                        }

                        string shuuryobi = "";
                        if (dtsys.Rows.Count > 0)
                        {
                            switch (this.SearchResult.Rows[i]["終了日警告区分"].ToString())
                            {
                                case "運搬":
                                    shuuryobi = "運搬終了日が設定値を" + dtsys.Rows[0]["MANIFEST_UNPAN_DAYS"].ToString() + "日以上過ぎているマニフェスト";
                                    break;
                                case "中間処分":
                                    shuuryobi = "処分終了日が設定値を" + dtsys.Rows[0]["MANIFEST_SBN_DAYS"].ToString() + "日以上過ぎているマニフェスト";
                                    break;
                                case "特管処分":
                                    shuuryobi = "特管処分終了日が設定値を" + dtsys.Rows[0]["MANIFEST_TOK_SBN_DAYS"].ToString() + "日以上過ぎているマニフェスト";
                                    break;
                                case "最終処分":
                                    shuuryobi = "最終処分終了日が設定値を" + dtsys.Rows[0]["MANIFEST_LAST_SBN_DAYS"].ToString() + "日以上過ぎているマニフェスト";
                                    break;
                            }
                        }

                        printDetailRow["D_HAIKI_KBN_VLB"] = dHaikiKbnVlb;
                        printDetailRow["D_SYUURYOBI_NITSU_SUU_VLB"] = shuuryobi;
                        printDetailRow["D_KEIKA_NITSU_CNT_VLB"] = this.SearchResult.Rows[i]["経過日数"].ToString() + "日";
                        printDetailRow["D_KOUHU_NUM_VLB"] = this.SearchResult.Rows[i]["交付番号"].ToString();
                        printDetailRow["D_HST_GYOSHA_NAME_VLB"] = this.SearchResult.Rows[i]["排出事業者名"].ToString();
                        printDetailRow["D_HST_GENBA_NAME_VLB"] = this.SearchResult.Rows[i]["排出事業場名"].ToString();
                        printDetailRow["D_UPN_JTK_NAME_VLB"] = this.SearchResult.Rows[i]["運搬受託者名"].ToString();
                        printDetailRow["D_SHOBU_JTK_NAME_VLB"] = this.SearchResult.Rows[i]["処分受託者名"].ToString();
                        printDetailRow["D_LAST_SBN_GB_NAME_VLB"] = this.SearchResult.Rows[i]["最終処分場所名"].ToString();
                        printDetailRow["D_HKSR_VLB"] = this.SearchResult.Rows[i]["廃棄物種類"].ToString();
                        printDetailRow["D_SUURYO_VLB"] = this.SearchResult.Rows[i]["数量"].ToString();
                        printDetailRow["D_TANI_VLB"] = this.SearchResult.Rows[i]["単位"].ToString();
                        printDetailData.Rows.Add(printDetailRow);
                    }
                    // プリント
                    ReportInfoR600 reportInfo = new ReportInfoR600(printHeaderData, printDetailData);
                    reportInfo.R600_Reprt();
                    reportInfo.Title = "マニフェスト終了日警告一覧表";

                    FormReportPrintPopup report = new FormReportPrintPopup(reportInfo, "R600");
                    report.PrintInitAction = 2;
                    report.PrintXPS();
                    report.Dispose();
                }
                else
                {
                    this.form.messageShowLogic.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 画面遷移
        /// </summary>
        public void FormChanges(WINDOW_TYPE WindowType)
        {
            LogUtility.DebugMethodStart();

            var denmaniWindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            var denmaniKanriId = String.Empty;
            var denmaniSeq = String.Empty;
            var isUpdateDenmani = false;
            var isReferenceDenmani = false;

            String latestSeq = string.Empty;
            try
            {
                //検索結果(マニフェストパターン)が1件もない場合
                if (this.form.customDataGridView1.Rows.Count <= 0)
                {
                    switch (WindowType)
                    {
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                            MessageBoxUtility.MessageBoxShow("E029", "修正するマニフェスト", "マニフェスト終了日警告一覧");
                            //break;
                            return;
                    }
                }

                //画面で行が選択されていない場合

                if (this.form.customDataGridView1.Rows.Count > 0
                    && this.form.customDataGridView1.CurrentRow == null)
                {
                    switch (WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                            MessageBoxUtility.MessageBoxShow("E029", "追加するマニフェスト", "マニフェスト終了日警告一覧");
                            //break ;
                            return;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                            MessageBoxUtility.MessageBoxShow("E029", "修正するマニフェスト", "マニフェスト終了日警告一覧");
                            //break;
                            return;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                            MessageBoxUtility.MessageBoxShow("E029", "削除するマニフェスト", "マニフェスト終了日警告一覧");
                            //break;
                            return;
                    }
                    return;
                }

                Int32 count_TME = 0;
                int i = this.form.customDataGridView1.CurrentRow.Index;

                switch (Convert.ToString(this.form.customDataGridView1.Rows[i].Cells["HAIKI_KBN_CD"].Value))
                {
                    case "1"://産廃（直行）
                    case "2"://産廃（積替）
                    case "3"://建廃

                        //SYSTEM_IDが取得できない場合。
                        if (string.IsNullOrEmpty(Convert.ToString(this.form.customDataGridView1.Rows[i].Cells["SYSTEM_ID"].Value)) ||
                        string.IsNullOrEmpty(Convert.ToString(this.form.customDataGridView1.Rows[i].Cells["SEQ"].Value)))
                        {
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }

                        //SYSTEM_IDが取得できた場合の存在チェック。
                        count_TME = this.SearchResult.Rows.Count;
                        if (count_TME <= 0)
                        {
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }
                        break;
                    case "4":
                        // 電子マニフェスト
                        denmaniKanriId = this.form.customDataGridView1.Rows[i].Cells["DENMANI_KANRI_ID"].Value.ToString();
                        denmaniSeq = this.form.customDataGridView1.Rows[i].Cells["DENMANI_SEQ"].Value.ToString();
                        var ediPassword = this.form.customDataGridView1.Rows[i].Cells["EDI_PASSWORD"].Value;
                        var inputKbn = this.form.customDataGridView1.Rows[i].Cells["KIND"].Value;

                        isUpdateDenmani = Manager.CheckAuthority("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
                        isReferenceDenmani = Manager.CheckAuthority("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);

                        if (inputKbn == null || inputKbn.ToString() == "4")
                        {
                            var displayAlert = false;
                            if (ediPassword != null && !String.IsNullOrEmpty(ediPassword.ToString()))
                            {
                                // 入力区分:自動 排出事業者のパスワード:あり 「アラートを表示して修正モード」
                                denmaniWindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                                if (isUpdateDenmani || isReferenceDenmani)
                                {
                                    displayAlert = true;
                                }
                            }
                            else
                            {
                                // 入力区分:自動 排出事業者のパスワード:なし 「アラートを表示して参照モード」
                                denmaniWindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                
                                if (isReferenceDenmani)
                                {
                                    displayAlert = true;
                                }
                            }

                            // 画面の表示権限がある場合にアラート表示(無い場合は権限のアラートを表示するため)
                            if (displayAlert)
                            {
                                new MessageBoxShowLogic().MessageBoxShow("E201");
                            }
                        }
                        else
                        {
                            // 入力区分:手動                             「修正モード」
                            denmaniWindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        }

                        break;
                }

                // 画面起動
                string haikiKbn = this.form.customDataGridView1.Rows[i].Cells["HAIKI_KBN_CD"].Value.ToString();
                switch (haikiKbn)
                {
                    case "1":
                    case "2":
                    case "3":
                        this.form.ParamOut_WinType = (int)WindowType;
                        this.form.ParamOut_SysID = this.form.customDataGridView1.Rows[i].Cells["SYSTEM_ID"].Value.ToString();
                        latestSeq = this.form.customDataGridView1.Rows[i].Cells["SEQ"].Value.ToString().Trim();
                        var formId = String.Empty;
                        switch (haikiKbn)
                        {
                            case "1"://G119 産廃（直行）マニフェスト一覧
                                formId = "G119";
                                break;
                            case "2"://G121 建廃マニフェスト一覧
                                formId = "G121";
                                break;
                            case "3"://G120 産廃（積替）マニフェスト一覧
                                formId = "G120";
                                break;
                            default:
                                break;
                        }

                        // 権限チェック
                        // 修正権限あり → 修正モード
                        // 修正権限なし 参照権限あり → 参照モード
                        // その他 → 開かない
                        var isUpdate = Manager.CheckAuthority(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
                        var isReference = Manager.CheckAuthority(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
                        if (isUpdate)
                        {
                            this.form.ParamOut_WinType = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                            FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                        }
                        else if (isReference)
                        {
                            this.form.ParamOut_WinType = (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                        }
                        else
                        {
                            new MessageBoxShowLogic().MessageBoxShow("E158", "修正");
                        }
                        break;
                    case "4":
                        if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == denmaniWindowType)
                        {
                            // 修正権限

                            // 権限チェック
                            // 修正権限あり → 修正モード
                            // 修正権限なし 参照権限あり → 参照モード
                            // その他 → 開かない
                            if (isUpdateDenmani)
                            {
                                FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, denmaniKanriId, denmaniSeq);
                            }
                            else if (isReferenceDenmani)
                            {
                                FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denmaniKanriId, denmaniSeq);
                            }
                            else
                            {
                                new MessageBoxShowLogic().MessageBoxShow("E158", "修正");
                            }
                        }
                        else
                        {
                            // 参照権限
                            FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denmaniKanriId, denmaniSeq);
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormChanges", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }

            LogUtility.DebugMethodEnd();

            return;
        }
        /// <summary>
        /// 自社情報 - 自社名を取得します
        /// </summary>
        /// <returns>自社名</returns>
        private M_CORP_INFO[] GetCorpName()
        {
            IM_CORP_INFODao dao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            M_CORP_INFO searchCorpInfo = new M_CORP_INFO();
            M_CORP_INFO[] corpInfo;
            corpInfo = (M_CORP_INFO[])dao.GetAllData();
            return corpInfo;
        }

        // 20140715 katen start‏
        //アラート件数
        public Boolean CheckNumberAlert(Int32 Kensu)
        {
            LogUtility.DebugMethodStart();

            Boolean check = false;

            if (Int32.Parse(this.header.NumberAlert.ToString()) < Kensu)
            {
                //検索件数がアラート件数を超えた場合
                //メッセージ「検索件数がアラート件数を超えました。\n表示を行いますか？」を表示する
                switch (MessageBoxUtility.MessageBoxShow("C025"))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        check = true;
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
            return check;
        }
        // 20140715 katen end‏
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}
