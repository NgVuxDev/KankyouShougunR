using r_framework.Logic;
using r_framework.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Setting;
using System.Reflection;
using System;
using System.Linq;
using r_framework.Configuration;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;
using System.Windows.Forms;
using Shougun.Core.Message;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Dto;
using r_framework.CustomControl;
using Shougun.Core.PaperManifest.ManifestoJissekiIchiran.DAO;
using System.Text;
using System.IO;
using System.Collections.Generic;
using r_framework.Dto;

namespace Shougun.Core.PaperManifest.ManifestoJissekiIchiran
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
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestoJissekiIchiran.Setting.ButtonSetting.xml";

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm parentbaseform { get; set; }

        /// <summary>
        /// DAO
        /// </summary>
        private HokokushoDaoCls dao_Hokokusho;
        private GetTMEDaoCls dao_GetTME;
        private GetDMTDaoCls dao_GetDMT;

        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        public string joinQuery { get; set; }

        /// <summary>
        /// 検索結果(マニフェストパターン)_画面遷移の確認用
        /// </summary>
        public DataTable Search_TME_Check { get; set; }

        /// <summary>
        /// 検索結果(マニフェストパターン)
        /// </summary>
        public DataTable Search_TME { get; set; }

        /// <summary>
        /// 検索結果(共通)
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        //交付年月日（初期値：当日）
        public String KoufuDateFrom = DateTime.Now.Date.ToString();
        public String KoufuDateTo = DateTime.Now.Date.ToString();
        //交付年月日区分（初期値：1 交付年月日あり）
        public String KoufuDateKbn = "1";

        /// <summary>共通</summary>
        Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;
        internal BusinessBaseForm footer;

        private MessageBoxShowLogic MsgBox;

        #region 一覧列名

        /// <summary>SYSTEM_ID</summary>
        internal readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>SEQ</summary>
        internal readonly string HIDDEN_SEQ = "HIDDEN_SEQ";

        /// <summary>LATEST_SEQ</summary>
        internal readonly string HIDDEN_LATEST_SEQ = "HIDDEN_LATEST_SEQ";

        /// <summary>HAIKI_KBN</summary>
        internal readonly string HIDDEN_HAIKI_KBN = "HIDDEN_HAIKI_KBN";

        /// <summary>KANRI_ID</summary>
        internal readonly string HIDDEN_KANRI_ID = "HIDDEN_KANRI_ID";

        /// <summary>DETAIL_SYSTEM_ID</summary>
        internal readonly string HIDDEN_DETAIL_SYSTEM_ID = "HIDDEN_DETAIL_SYSTEM_ID";

        #endregion

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();

            this.MsgBox = new MessageBoxShowLogic();

            this.dao_Hokokusho = DaoInitUtility.GetComponent<HokokushoDaoCls>();
            this.dao_GetTME = DaoInitUtility.GetComponent<GetTMEDaoCls>();
            this.dao_GetDMT = DaoInitUtility.GetComponent<GetDMTDaoCls>();

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

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentbaseform.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            parentForm.bt_func8.ProcessKbn = PROCESS_KBN.NEW;

            //並替移動ボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            //フィルタボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //【1】パターン一覧(1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            //【2】検索条件設定(2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

            // ダブルクリック時にFrom項目の入力内容をコピーする
            this.form.KOUFU_DATE_TO.MouseDoubleClick += new MouseEventHandler(KOUFU_DATE_TO_MouseDoubleClick);

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();
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

                this.parentbaseform = (BusinessBaseForm)this.form.Parent;

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                //交付年月日（初期値：当日）
                KoufuDateFrom = this.parentbaseform.sysDate.ToString();
                KoufuDateTo = this.parentbaseform.sysDate.ToString();

                // 継承したフォームのDGVのプロパティはデザイナで変更できない為、ここで設定
                this.form.customDataGridView1.AllowUserToAddRows = false;                                //行の追加オプション(false)
                this.form.customDataGridView1.Height = 263;

                this.form.customDataGridView1.TabIndex = 41;

                //アラート件数
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                this.header.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                this.header.NumberAlert = this.header.InitialNumberAlert;
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
        /// 画面クリア
        /// </summary>
        public bool ClearScreen(String Kbn)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(Kbn);
                switch (Kbn)
                {
                    case "Initial"://初期表示
                        //タイトル
                        string titleText = "マニフェスト実績一覧";
                        this.header.lb_title.Text = titleText;

                        //検索条件
                        this.form.searchString.Text = "";

                        //ヒント
                        this.footer.lb_hint.Text = "";

                        //処理No（ESC）
                        this.footer.txb_process.Text = "";

                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();
                        this.form.customSearchHeader1.ClearCustomSearchSetting();

                        break;

                    case "ClsSearchCondition"://検索条件をクリア
                        //アラート件数
                        this.header.NumberAlert = this.header.InitialNumberAlert;

                        //交付年月日
                        KoufuDateFrom = this.parentbaseform.sysDate.ToString();
                        KoufuDateTo = this.parentbaseform.sysDate.ToString();

                        //一覧の項目を消去
                        this.Search_TME.Clear();

                        //交付年月日区分
                        KoufuDateKbn = "1";
                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();
                        this.form.customSearchHeader1.ClearCustomSearchSetting();

                        break;
                }

                //拠点
                this.header.KYOTEN_CD.Text = "99";
                this.header.KYOTEN_NAME.Text = "全社";

                //廃棄物区分
                this.header.HaikiKbn_1.Checked = true;
                this.header.HaikiKbn_2.Checked = true;
                this.header.HaikiKbn_3.Checked = true;
                this.header.HaikiKbn_4.Checked = true;

                //一次二次区分
                this.header.MANI_KBN.Text = "3";

                //読込データ件数
                this.header.ReadDataNumber.Text = "0";

                //アラート件数
                this.header.AlertNumber.Text = this.header.NumberAlert.ToString();
                //排出事業者
                this.form.cantxt_HaisyutuGyousyaCd.Text = string.Empty;
                this.form.ctxt_HaisyutuGyousyaName.Text = string.Empty;
                //排出事業場
                this.form.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
                this.form.ctxt_HaisyutuJigyoubaName.Text = string.Empty;
                //運搬受託者
                this.form.cantxt_UnpanJyutakuNameCd.Text = string.Empty;
                this.form.ctxt_UnpanJyutakuName.Text = string.Empty;
                //処分受託者
                this.form.cantxt_SyobunJyutakuNameCd.Text = string.Empty;
                this.form.ctxt_SyobunJyutakuName.Text = string.Empty;
                //運搬先の事業場
                this.form.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                this.form.ctxt_UnpanJyugyobaName.Text = string.Empty;
                //報告書分類
                this.form.cantxt_HokokushoBunrui.Text = string.Empty;
                this.form.ctxt_HokokushoBunrui.Text = string.Empty;
                //処分方法
                this.form.SHOBUN_HOUHOU_CD.Text = string.Empty;
                this.form.SHOBUN_HOUHOU_NAME.Text = string.Empty;
                this.form.SHOBUN_HOUHOU_MI.Checked = false;
                //運搬自社
                this.form.JISHA_UNPAN_KBN.Text = "3";
                //交付年月日
                switch (KoufuDateFrom)
                {
                    case "":
                        this.form.KOUFU_DATE_FROM.Text = KoufuDateFrom;
                        break;

                    default:
                        this.form.KOUFU_DATE_FROM.Value = DateTime.Parse(KoufuDateFrom);
                        break;
                }

                switch (KoufuDateTo)
                {
                    case "":
                        this.form.KOUFU_DATE_TO.Text = KoufuDateTo;
                        break;

                    default:
                        this.form.KOUFU_DATE_TO.Value = DateTime.Parse(KoufuDateTo);
                        break;
                }

                //交付年月日区分
                if (String.IsNullOrEmpty(KoufuDateKbn))
                {
                    KoufuDateKbn = "1";
                }
                this.form.KOUFU_DATE_KBN.Text = KoufuDateKbn;
                
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearScreen", ex);
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

        //アラート件数
        public Boolean CheckNumberAlert(Int32 Kensu)
        {
            LogUtility.DebugMethodStart();

            Boolean check = false;
            if (Int32.Parse(this.header.NumberAlert.ToString()) < Kensu)
            {
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

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録
        /// </summary>
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            try
            {

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();
            Int32 count_TME = 0;
            try
            {
                var DateFrom = string.Empty;
                var DateTo = string.Empty;

                // 日付FROMのNULL対策
                if (this.form.KOUFU_DATE_FROM.Value != null)
                {
                    DateFrom = this.form.KOUFU_DATE_FROM.Value.ToString();
                }

                // 日付TOのNULL対策
                if (this.form.KOUFU_DATE_TO.Value != null)
                {
                    DateTo = this.form.KOUFU_DATE_TO.Value.ToString();
                }

                count_TME = this.Get_Search_TME(
                        DateFrom,
                        DateTo,
                        "false",
                        "",
                        "",
                        this.form.KOUFU_DATE_KBN.Text.ToString(),
                        "",
                        "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
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
        /// 更新
        /// </summary>
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            try
            {

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="NameFlg">現場名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_gyoshaCd">業者CD</param>
        /// <param name="txt_JigyoubaCd">現場CD</param>
        /// <param name="txt_JigyoubaName">現場名</param>
        /// <param name="HAISHUTSU_NIZUMI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SAISHUU_SHOBUNJOU_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SHOBUN_NIOROSHI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="TSUMIKAEHOKAN_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="ISNOT_NEED_DELETE_FLG">削除チェックいるかどうかの判断フラッグ</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int SetAddressJigyouba(string NameFlg, CustomTextBox txt_gyoshaCd, CustomTextBox txt_JigyoubaCd, CustomTextBox txt_JigyoubaName
            , bool HAISHUTSU_NIZUMI_GENBA_KBN
            , bool SAISHUU_SHOBUNJOU_KBN
            , bool SHOBUN_NIOROSHI_GENBA_KBN
            , bool TSUMIKAEHOKAN_KBN
            , bool ISNOT_NEED_DELETE_FLG = false
            )
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(NameFlg, txt_gyoshaCd, txt_JigyoubaCd, txt_JigyoubaName
                    , HAISHUTSU_NIZUMI_GENBA_KBN, SAISHUU_SHOBUNJOU_KBN, SHOBUN_NIOROSHI_GENBA_KBN, TSUMIKAEHOKAN_KBN, ISNOT_NEED_DELETE_FLG);

                //空
                if (txt_gyoshaCd.Text == string.Empty || txt_JigyoubaCd.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt_gyoshaCd.Text;
                Serch.GENBA_CD = txt_JigyoubaCd.Text;
                Serch.ISNOT_NEED_DELETE_FLG = ISNOT_NEED_DELETE_FLG;

                //区分
                if (HAISHUTSU_NIZUMI_GENBA_KBN)
                {
                    Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "1";
                    Serch.HAISHUTSU_NIZUMI_GENBA_KBN = "1";
                }
                if (SAISHUU_SHOBUNJOU_KBN)
                {
                    Serch.SAISHUU_SHOBUNJOU_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (SHOBUN_NIOROSHI_GENBA_KBN)
                {
                    Serch.SHOBUN_NIOROSHI_GENBA_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (TSUMIKAEHOKAN_KBN)
                {
                    Serch.TSUMIKAEHOKAN_KBN = "1";
                }

                DataTable dt = this.mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 1://正常
                        break;

                    default://エラー
                        ret = 2;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                }

                //現場名
                if (txt_JigyoubaName != null)
                {

                    switch (NameFlg)
                    {
                        case "All"://「正式名称1 + 正式名称2」をセットする。
                            txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME"].ToString();
                            break;

                        case "Part1"://「正式名称1」をセットする。
                            txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME1"].ToString();
                            break;

                        case "Ryakushou_Name"://「略称名」をセットする。
                            txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetAddressJigyouba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 業者チェック(排出事業者、運搬受託者、処分受託者) 
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkGyosya(object obj, string colname)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(obj, colname);

                TextBox txt = (TextBox)obj;
                if (txt.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt.Text;
                Serch.GYOUSHAKBN_MANI = "True";
                Serch.ISNOT_NEED_DELETE_FLG = true;
                //最終処分業者の場合、最終処分場区分の条件を追加した
                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGenbaAll(Serch);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][colname].ToString() == "True")
                        {
                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                    }
                }
                this.form.messageShowLogic.MessageBoxShow("E020", "業者");

                txt.Focus();
                txt.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGyosya", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 現場チェック(排出事業者、運搬受託者、処分受託者) 
        /// </summary>
        /// <param name="genba">現場CD</param>
        /// <param name="gyosya">事業者CD</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <param name="colname2">[処分事業場専用]チェックカラム名称2</param>
        /// <param name="genba">[処分事業場専用]現場名</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkJigyouba(object genba, object gyosya, string colname, string colname2 = "", object genbaName = null)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(genba, gyosya, colname, colname2, genbaName);

                TextBox txt1 = (TextBox)genba;
                TextBox txt2 = (TextBox)gyosya;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (txt2.Text == string.Empty)
                {
                    switch (colname)
                    {
                        case "HAISHUTSU_NIZUMI_GENBA_KBN":
                            this.form.messageShowLogic.MessageBoxShow("E051", "排出事業者");
                            break;

                        case "SHOBUN_NIOROSHI_GENBA_KBN":
                            this.form.messageShowLogic.MessageBoxShow("E051", "処分受託者");
                            break;

                        case "TSUMIKAEHOKAN_KBN":
                            this.form.messageShowLogic.MessageBoxShow("E051", "積替保管業者");
                            break;
                    }
                    txt1.Text = string.Empty;
                    txt1.Focus();
                    txt1.SelectAll();

                    ret = 2;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GENBA_CD = txt1.Text;
                Serch.GYOUSHA_CD = txt2.Text;
                Serch.ISNOT_NEED_DELETE_FLG = true;

                this.SearchResult = new DataTable();
                DataTable dt = this.mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "現場");
                        break;

                    case 1:
                        if (dt.Rows[0][colname].ToString() == "True" && string.IsNullOrEmpty(colname2))
                        {
                            txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();

                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        else if (!string.IsNullOrEmpty(colname2) && (dt.Rows[0][colname].ToString() == "True" || dt.Rows[0][colname2].ToString() == "True"))
                        {
                            /* 処分事業場用(現場の名称も同時に設定) */
                            txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                            ((TextBox)genbaName).Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();

                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        this.form.messageShowLogic.MessageBoxShow("E058");
                        break;

                    default:
                        switch (colname)
                        {
                            case "HAISHUTSU_NIZUMI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "排出事業者");
                                break;

                            case "SAISHUU_SHOBUNJOU_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "最終処分の業者");
                                break;

                            case "SHOBUN_NIOROSHI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "処分受託者");
                                break;

                            case "TSUMIKAEHOKAN_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "積替保管業者");
                                break;

                            case "UNPAN_JUTAKUSHA_KAISHA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "運搬の受託者");
                                break;
                        }
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkJigyouba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 報告書分類チェック
        /// </summary>
        /// <param name="houkokushoBunrui">報告書分類CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkHoukokushoBunrui(object houkokushoBunrui)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(houkokushoBunrui);

                TextBox txt1 = (TextBox)houkokushoBunrui;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new HokokushoDtoCls();
                Serch.HOUKOKUSHO_BUNRUI_CD = txt1.Text;

                this.SearchResult = new DataTable();
                DataTable dt = this.dao_Hokokusho.GetDataForEntity(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.ctxt_HokokushoBunrui.Text = string.Empty;
                        this.form.messageShowLogic.MessageBoxShow("E020", "報告書分類");
                        this.form.cantxt_HokokushoBunrui.Focus();
                        break;

                    case 1:
                        this.form.ctxt_HokokushoBunrui.Text = Convert.ToString(dt.Rows[0]["HOUKOKUSHO_BUNRUI_NAME_RYAKU"]);
                        ret = 0;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;

                    default:
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHoukokushoBunrui", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// データ取得
        /// </summary>
        public int Get_Search_TME(String KOUFU_DATE_FROM, String KOUFU_DATE_TO, String DELETE_FLG,
                                    String SYSTEM_ID, String SEQ, String KOUFU_DATE_KBN,
                                    String KANRI_ID, String LATEST_SEQ)
        {
            LogUtility.DebugMethodStart(KOUFU_DATE_FROM, KOUFU_DATE_TO, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);

            var count = -1;

            // 検索文字列取得
            this.selectQuery = this.form.SelectQuery;
            this.orderByQuery = this.form.OrderByQuery;
            this.joinQuery = this.form.JoinQuery;

            count = this.Get_Search_AllMani(KOUFU_DATE_FROM, KOUFU_DATE_TO, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);

            LogUtility.DebugMethodEnd(count);

            return count;
        }

        /// <summary>
        /// データ取得（全て）
        /// </summary>
        /// <param name="KOUFU_DATE_FROM"></param>
        /// <param name="KOUFU_DATE_TO"></param>
        /// <param name="HAIKI_KBN_CD"></param>
        /// <param name="DELETE_FLG"></param>
        /// <param name="SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <param name="KOUFU_DATE_KBN"></param>
        /// <param name="KANRI_ID"></param>
        /// <param name="LATEST_SEQ"></param>
        /// <returns></returns>
        private int Get_Search_AllMani(String KOUFU_DATE_FROM, String KOUFU_DATE_TO, String DELETE_FLG,
                                    String SYSTEM_ID, String SEQ, String KOUFU_DATE_KBN, String KANRI_ID, String LATEST_SEQ)
        {
            LogUtility.DebugMethodStart(KOUFU_DATE_FROM, KOUFU_DATE_TO, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);

            int count = 0;

            try
            {
                var sql = new StringBuilder();

                #region SELECT句

                sql.Append(" SELECT DISTINCT ");
                sql.Append(this.selectQuery);
                sql.AppendFormat(", SUMMARY.SYSTEM_ID AS {0} ", this.HIDDEN_SYSTEM_ID);
                sql.AppendFormat(", SUMMARY.SEQ AS {0} ", this.HIDDEN_SEQ);
                sql.AppendFormat(", SUMMARY.LATEST_SEQ AS {0} ", this.HIDDEN_LATEST_SEQ);
                sql.AppendFormat(", SUMMARY.KANRI_ID AS {0} ", this.HIDDEN_KANRI_ID);
                sql.AppendFormat(", SUMMARY.HAIKI_KBN_CD AS {0} ", this.HIDDEN_HAIKI_KBN);
                sql.AppendFormat(", SUMMARY.DETAIL_SYSTEM_ID AS {0} ", this.HIDDEN_DETAIL_SYSTEM_ID);

                #endregion

                #region FROM句

                sql.Append(" FROM ( ");

                if (this.header.HaikiKbn_1.Checked || this.header.HaikiKbn_2.Checked || this.header.HaikiKbn_3.Checked)
                {
                    #region 紙マニ

                    if (this.header.MANI_KBN.Text == "1" || this.header.MANI_KBN.Text == "3")
                    {
                        #region 一次マニ

                        #region SELECT

                        sql.Append(" SELECT ");
                        // マニ
                        sql.Append("   TME.SYSTEM_ID ");                                                                      // システムID
                        sql.Append(" , TME.SEQ ");                                                                            // 枝番
                        sql.Append(" , NULL AS LATEST_SEQ ");                                                                 // 最終枝番（紙マニは必ず空）
                        sql.Append(" , NULL AS KANRI_ID ");                                                                   // 管理ID（紙マニは必ず空）
                        sql.Append(" , TME.HAIKI_KBN_CD ");                                                                   // 廃棄区分CD
                        sql.Append(" , TME.KYOTEN_CD ");                                                                      // 拠点
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 1 THEN MHS1.HOUKOKUSHO_BUNRUI_CD ");
                        sql.Append("   WHEN 2 THEN MHS2.HOUKOKUSHO_BUNRUI_CD ");
                        sql.Append("   WHEN 3 THEN MHS3.HOUKOKUSHO_BUNRUI_CD ELSE '' END AS HOUKOKUSHO_BUNRUI_CD ");          // 報告書分類ＣＤ
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 1 THEN MHB1.HOUKOKUSHO_BUNRUI_NAME_RYAKU ");
                        sql.Append("   WHEN 2 THEN MHB2.HOUKOKUSHO_BUNRUI_NAME_RYAKU ");
                        sql.Append("   WHEN 3 THEN MHB3.HOUKOKUSHO_BUNRUI_NAME_RYAKU ELSE '' END AS HOUKOKUSHO_BUNRUI_NAME ");// 報告書分類名
                        sql.Append(" , TMD.HAIKI_NAME_CD AS HAIKI_NAME_CD ");                                                 // 廃棄物名称CD
                        sql.Append(" , MHN.HAIKI_NAME_RYAKU AS HAIKI_NAME ");                                                 // 廃棄物名称
                        sql.Append(" , TMD.HAIKI_SUU AS HAIKI_SUU ");                                                         // マニフェスト数量
                        sql.Append(" , TMD.HAIKI_UNIT_CD AS HAIKI_UNIT_CD ");                                                 // 単位CD1
                        sql.Append(" , TMD_MU.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME ");                                          // 単位名1
                        sql.Append(" , TMD.KANSAN_SUU AS KANSAN_SUU ");                                                       // 運搬委託量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD2 ");                            // 単位CD2
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME2 ");                                      // 単位名2
                        sql.Append(" , TMD.KANSAN_SUU AS SBN_SUU ");                                                          // 処分受託量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD3 ");                            // 単位CD3
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME3 ");                                      // 単位名3
                        sql.Append(" , TME.FIRST_MANIFEST_KBN AS FIRST_MANIFEST_KBN ");                                       // 一次マニ区分
                        sql.Append(" , TME.HST_GYOUSHA_CD AS HST_GYOUSHA_CD ");                                               // 排出事業者CD
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GYOUSHA_NAME, ''),1, 40)) + ");
                        sql.Append("   SUBSTRING(ISNULL(TME.HST_GYOUSHA_NAME, ''),41, 40)) AS HST_GYOUSHA_NAME  ");           // 排出事業者名
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GYOUSHA_ADDRESS, ''),1, 48)) + ");
                        sql.Append("   SUBSTRING(ISNULL(TME.HST_GYOUSHA_ADDRESS, ''),49, 40)) AS HST_GYOUSHA_ADDRESS  ");     // 排出事業者住所
                        sql.Append(" , HST_GYOUSHA.CHIIKI_CD AS HST_GYOUSHA_CHIIKI_CD ");                                     // 排出事業者地域CD
                        sql.Append(" , HST_GYOUSHA_CHIIKI.CHIIKI_NAME_RYAKU AS HST_GYOUSHA_CHIIKI_NAME ");                    // 排出事業者地域名
                        sql.Append(" , HST_GYOUSHA.GYOUSHU_CD AS HST_GYOUSHA_GYOUSHU_CD ");                                   // 排出事業者業種CD
                        sql.Append(" , HST_GYOUSHA_GYOUSHU.GYOUSHU_NAME_RYAKU AS HST_GYOUSHA_GYOUSHU_NAME ");                 // 排出事業者業種名
                        sql.Append(" , TME.HST_GENBA_CD AS HST_GENBA_CD ");                                                   // 排出事業場CD
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GENBA_NAME, ''),1, 40)) + ");
                        sql.Append("   SUBSTRING(ISNULL(TME.HST_GENBA_NAME, ''),41, 40)) AS HST_GENBA_NAME  ");               // 排出事業場名
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GENBA_ADDRESS, ''),1, 48)) + ");
                        sql.Append("   SUBSTRING(ISNULL(TME.HST_GENBA_ADDRESS, ''),49, 40)) AS HST_GENBA_ADDRESS  ");         // 排出事業場住所
                        sql.Append(" , HST_GENBA.CHIIKI_CD AS HST_GENBA_CHIIKI_CD ");                                         // 排出事業場地域CD
                        sql.Append(" , HST_GENBA_CHIIKI.CHIIKI_NAME_RYAKU AS HST_GENBA_CHIIKI_NAME ");                        // 排出事業場地域名
                        sql.Append(" , HST_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS HST_UPN_CHIIKI_CD ");                  // 排出事業場運搬報告地域CD
                        sql.Append(" , HST_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU AS HST_UPN_CHIIKI_NAME ");                      // 排出事業場運搬報告地域名
                        sql.Append(" , HST_GENBA.GYOUSHU_CD AS HST_GENBA_GYOUSHU_CD ");                                       // 排出事業場業種CD
                        sql.Append(" , HST_GENBA_GYOUSHU.GYOUSHU_NAME_RYAKU AS HST_GENBA_GYOUSHU_NAME ");                     // 排出事業場業種名
                        sql.Append(" , TMD.HAIKI_SHURUI_CD AS HAIKI_SHURUI_CD ");                                             // 廃棄物種類CD1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 1 THEN MHS1.HAIKI_SHURUI_NAME_RYAKU ");
                        sql.Append("   WHEN 2 THEN MHS2.HAIKI_SHURUI_NAME_RYAKU ");
                        sql.Append("   WHEN 3 THEN MHS3.HAIKI_SHURUI_NAME_RYAKU ELSE '' END AS HAIKI_SHURUI_NAME ");          // 廃棄物種類名1
                        sql.Append(" , TME.MANIFEST_ID AS MANIFEST_ID ");                                                     // 交付番号1
                        sql.Append(" , TME.KOUFU_DATE AS KOUFU_DATE ");                                                       // 交付年月日1

                        // 収集運搬
                        sql.Append(" , UPN1.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_1 ");                                           // 運搬受託者CD1_1
                        sql.Append(" , UPN1.UPN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME1_1 ");                                       // 運搬受託者名1_1
                        sql.Append(" , UPN1.UPN_END_DATE AS UPN_END_DATE1_1 ");                                               // 運搬終了年月日1_1
                        sql.Append(" , UPN2.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_2 ");                                           // 運搬受託者CD1_2
                        sql.Append(" , UPN2.UPN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME1_2 ");                                       // 運搬受託者名1_2
                        sql.Append(" , UPN2.UPN_END_DATE AS UPN_END_DATE1_2 ");                                               // 運搬終了年月日1_2
                        sql.Append(" , UPN3.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_3 ");                                           // 運搬受託者CD1_3
                        sql.Append(" , UPN3.UPN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME1_3 ");                                       // 運搬受託者名1_3
                        sql.Append(" , UPN3.UPN_END_DATE AS UPN_END_DATE1_3 ");                                               // 運搬終了年月日1_3
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD1_4 ");                                                          // 運搬受託者CD1_4
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME1_4 ");                                                        // 運搬受託者名1_4
                        sql.Append(" , NULL AS UPN_END_DATE1_4 ");                                                            // 運搬終了年月日1_4
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD1_5 ");                                                          // 運搬受託者CD1_5
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME1_5 ");                                                        // 運搬受託者名1_5
                        sql.Append(" , NULL AS UPN_END_DATE1_5 ");                                                            // 運搬終了年月日1_5

                        // 積替保管情報
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN1.UPN_SAKI_GYOUSHA_CD ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_1 ");                                   // 積替保管業者CD1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GYOUSHA1.GYOUSHA_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GYOUSHA_NAME END AS TMH_GYOUSHA_NAME1_1 ");                               // 積替保管業者名1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN1.UPN_SAKI_GENBA_CD ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GENBA_CD END AS TMH_GENBA_CD1_1 ");                                       // 積替保管現場CD1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN1.UPN_SAKI_GENBA_NAME ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GENBA_NAME END AS TMH_GENBA_NAME1_1 ");                                   // 積替保管現場名1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN1.UPN_SAKI_GENBA_ADDRESS ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GENBA_ADDRESS END AS TMH_GENBA_ADDRESS1_1 ");                             // 積替保管場所住所1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GENBA1.CHIIKI_CD ELSE '' END  ");
                        sql.Append("   ELSE TMH_GENBA.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_1 ");                             // 積替保管場所地域CD1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE TMH_GENBA_CHIIKI.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_1 ");            // 積替保管場所地域名1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END  ");
                        sql.Append("   ELSE TMH_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_1 ");      // 積替保管場所運搬報告先地域CD1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE TMH_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_1 ");          // 積替保管場所運搬報告先地域名1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN2.UPN_SAKI_GYOUSHA_CD ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GYOUSHA_CD1_2 ");                                                   // 積替保管業者CD1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GYOUSHA2.GYOUSHA_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GYOUSHA_NAME1_2 ");                                                 // 積替保管業者名1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN2.UPN_SAKI_GENBA_CD ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_CD1_2 ");                                                     // 積替保管現場CD1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN2.UPN_SAKI_GENBA_NAME ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_NAME1_2 ");                                                   // 積替保管現場名1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN2.UPN_SAKI_GENBA_ADDRESS ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_ADDRESS1_2 ");                                                // 積替保管場所住所1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GENBA2.CHIIKI_CD ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_CHIIKI_CD1_2 ");                                              // 積替保管場所地域CD1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_CHIIKI_NAME1_2 ");                                            // 積替保管場所地域名1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_UPN_CHIIKI_CD1_2 ");                                                // 積替保管場所運搬報告先地域CD1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_UPN_CHIIKI_NAME1_2 ");                                              // 積替保管場所運搬報告先地域名1_2
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD1_3 ");                                                          // 積替保管業者CD1_3
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME1_3 ");                                                        // 積替保管業者名1_3
                        sql.Append(" , NULL AS TMH_GENBA_CD1_3 ");                                                            // 積替保管現場CD1_3
                        sql.Append(" , NULL AS TMH_GENBA_NAME1_3 ");                                                          // 積替保管現場名1_3
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS1_3 ");                                                       // 積替保管場所住所1_3
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD1_3 ");                                                     // 積替保管場所地域CD1_3
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME1_3 ");                                                   // 積替保管場所地域名1_3
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD1_3 ");                                                       // 積替保管場所運搬報告先地域CD1_3
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME1_3 ");                                                     // 積替保管場所運搬報告先地域名1_3
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD1_4 ");                                                          // 積替保管業者CD1_4
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME1_4 ");                                                        // 積替保管業者名1_4
                        sql.Append(" , NULL AS TMH_GENBA_CD1_4 ");                                                            // 積替保管現場CD1_4
                        sql.Append(" , NULL AS TMH_GENBA_NAME1_4 ");                                                          // 積替保管現場名1_4
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS1_4 ");                                                       // 積替保管場所住所1_4
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD1_4 ");                                                     // 積替保管場所地域CD1_4
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME1_4 ");                                                   // 積替保管場所地域名1_4
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD1_4 ");                                                       // 積替保管場所運搬報告先地域CD1_4
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME1_4 ");                                                     // 積替保管場所運搬報告先地域名1_4

                        // 処分受託者情報
                        sql.Append(" , TME.SBN_GYOUSHA_CD AS SBN_GYOUSHA_CD ");                                               // 処分業者CD
                        sql.Append(" , TME.SBN_GYOUSHA_NAME AS SBN_GYOUSHA_NAME ");                                           // 処分業者名
                        sql.Append(" , TME.SBN_GYOUSHA_ADDRESS AS SBN_GYOUSHA_ADDRESS ");                                     // 処分業者住所
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN1.UPN_SAKI_GENBA_CD ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN2.UPN_SAKI_GENBA_CD ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN3.UPN_SAKI_GENBA_CD ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_CD END AS SBN_GENBA_CD ");                                    // 処分事業場CD
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN1.UPN_SAKI_GENBA_NAME ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN2.UPN_SAKI_GENBA_NAME ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN3.UPN_SAKI_GENBA_NAME ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_NAME END AS SBN_GENBA_NAME ");                                // 処分事業場名称
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN1.UPN_SAKI_GENBA_ADDRESS ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN2.UPN_SAKI_GENBA_ADDRESS ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN3.UPN_SAKI_GENBA_ADDRESS ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_ADDRESS END AS SBN_GENBA_ADDRESS ");                          // 処分事業場住所
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN_GENBA1.CHIIKI_CD ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN_GENBA2.CHIIKI_CD ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN_GENBA3.CHIIKI_CD ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE SBN_GENBA.CHIIKI_CD END AS SBN_GENBA_CHIIKI_CD ");                                // 処分事業場地域CD
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN_GENBA_CHIIKI3.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE SBN_GENBA_CHIIKI.CHIIKI_NAME_RYAKU END AS SBN_GENBA_CHIIKI_NAME ");               // 処分事業場地域名
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE SBN_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS SBN_UPN_CHIIKI_CD ");         // 処分事業場運搬報告地域CD
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN_GENBA_UPN_CHIIKI3.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE SBN_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU END AS SBN_UPN_CHIIKI_NAME ");             // 処分事業場運搬報告地域名

                        // 明細
                        sql.Append(" , TMD.SBN_HOUHOU_CD AS DETAIL_SBN_HOUHOU_CD ");                                          // 処分方法CD
                        sql.Append(" , DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU AS DETAIL_SBN_HOUHOU_NAME ");               // 処分方法名
                        sql.Append(" , TMD.SBN_END_DATE AS DETAIL_SBN_END_DATE ");                                            // 処分終了年月日1
                        sql.Append(" , TMD.LAST_SBN_END_DATE AS DETAIL_LAST_SBN_END_DATE ");                                  // 最終処分終了年月日1
                        sql.Append(" , TMD.DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID ");                                           // 明細システムID
                        // 二次マニ
                        sql.Append(" , TME2.MANIFEST_ID AS MANIFEST_ID2 ");                                                   // 交付番号2
                        sql.Append(" , TME2.KOUFU_DATE AS KOUFU_DATE2 ");                                                     // 交付年月日2

                        // 収集運搬
                        sql.Append(" , TME2.UPN_GYOUSHA_CD1 AS UPN_GYOUSHA_CD2_1 ");                                          // 運搬受託者CD2_1
                        sql.Append(" , TME2.UPN_GYOUSHA_NAME1 AS UPN_GYOUSHA_NAME2_1 ");                                      // 運搬受託者名2_1
                        sql.Append(" , TME2.UPN_GYOUSHA_CD2 AS UPN_GYOUSHA_CD2_2 ");                                          // 運搬受託者CD2_2
                        sql.Append(" , TME2.UPN_GYOUSHA_NAME2 AS UPN_GYOUSHA_NAME2_2 ");                                      // 運搬受託者名2_2
                        sql.Append(" , TME2.UPN_GYOUSHA_CD3 AS UPN_GYOUSHA_CD2_3 ");                                          // 運搬受託者CD2_3
                        sql.Append(" , TME2.UPN_GYOUSHA_NAME3 AS UPN_GYOUSHA_NAME2_3 ");                                      // 運搬受託者名2_3
                        sql.Append(" , TME2.UPN_GYOUSHA_CD4 AS UPN_GYOUSHA_CD2_4 ");                                          // 運搬受託者CD2_4
                        sql.Append(" , TME2.UPN_GYOUSHA_NAME4 AS UPN_GYOUSHA_NAME2_4 ");                                      // 運搬受託者名2_4
                        sql.Append(" , TME2.UPN_GYOUSHA_CD5 AS UPN_GYOUSHA_CD2_5 ");                                          // 運搬受託者CD2_5
                        sql.Append(" , TME2.UPN_GYOUSHA_NAME5 AS UPN_GYOUSHA_NAME2_5 ");                                      // 運搬受託者名2_5
                        // 積替保管情報
                        sql.Append(" , TME2.TMH_GYOUSHA_CD2_1 AS TMH_GYOUSHA_CD2_1 ");                                        // 積替保管業者CD2_1
                        sql.Append(" , TME2.TMH_GYOUSHA_NAME2_1 AS TMH_GYOUSHA_NAME2_1 ");                                    // 積替保管業者名2_1
                        sql.Append(" , TME2.TMH_GENBA_CD2_1 AS TMH_GENBA_CD2_1 ");                                            // 積替保管現場CD2_1
                        sql.Append(" , TME2.TMH_GENBA_NAME2_1 AS TMH_GENBA_NAME2_1 ");                                        // 積替保管現場名2_1
                        sql.Append(" , TME2.TMH_GENBA_ADDRESS2_1 AS TMH_GENBA_ADDRESS2_1 ");                                  // 積替保管場所住所2_1
                        sql.Append(" , TME2.TMH_GENBA_CHIIKI_CD2_1 AS TMH_GENBA_CHIIKI_CD2_1 ");                              // 積替保管場所地域CD2_1
                        sql.Append(" , TME2.TMH_GENBA_CHIIKI_NAME2_1 AS TMH_GENBA_CHIIKI_NAME2_1 ");                          // 積替保管場所地域名2_1
                        sql.Append(" , TME2.TMH_UPN_CHIIKI_CD2_1 AS TMH_UPN_CHIIKI_CD2_1 ");                                  // 積替保管場所運搬報告先地域CD2_1
                        sql.Append(" , TME2.TMH_UPN_CHIIKI_NAME2_1 AS TMH_UPN_CHIIKI_NAME2_1 ");                              // 積替保管場所運搬報告先地域名2_1
                        sql.Append(" , TME2.TMH_GYOUSHA_CD2_2 AS TMH_GYOUSHA_CD2_2 ");                                        // 積替保管業者CD2_2
                        sql.Append(" , TME2.TMH_GYOUSHA_NAME2_2 AS TMH_GYOUSHA_NAME2_2 ");                                    // 積替保管業者名2_2
                        sql.Append(" , TME2.TMH_GENBA_CD2_2 AS TMH_GENBA_CD2_2 ");                                            // 積替保管現場CD2_2
                        sql.Append(" , TME2.TMH_GENBA_NAME2_2 AS TMH_GENBA_NAME2_2 ");                                        // 積替保管現場名2_2
                        sql.Append(" , TME2.TMH_GENBA_ADDRESS2_2 AS TMH_GENBA_ADDRESS2_2 ");                                  // 積替保管場所住所2_2
                        sql.Append(" , TME2.TMH_GENBA_CHIIKI_CD2_2 AS TMH_GENBA_CHIIKI_CD2_2 ");                              // 積替保管場所地域CD2_2
                        sql.Append(" , TME2.TMH_GENBA_CHIIKI_NAME2_2 AS TMH_GENBA_CHIIKI_NAME2_2 ");                          // 積替保管場所地域名2_2
                        sql.Append(" , TME2.TMH_UPN_CHIIKI_CD2_2 AS TMH_UPN_CHIIKI_CD2_2 ");                                  // 積替保管場所運搬報告先地域CD2_2
                        sql.Append(" , TME2.TMH_UPN_CHIIKI_NAME2_2 AS TMH_UPN_CHIIKI_NAME2_2 ");                              // 積替保管場所運搬報告先地域名2_2
                        sql.Append(" , TME2.TMH_GYOUSHA_CD2_3 AS TMH_GYOUSHA_CD2_3 ");                                        // 積替保管業者CD2_3
                        sql.Append(" , TME2.TMH_GYOUSHA_NAME2_3 AS TMH_GYOUSHA_NAME2_3 ");                                    // 積替保管業者名2_3
                        sql.Append(" , TME2.TMH_GENBA_CD2_3 AS TMH_GENBA_CD2_3 ");                                            // 積替保管現場CD2_3
                        sql.Append(" , TME2.TMH_GENBA_NAME2_3 AS TMH_GENBA_NAME2_3 ");                                        // 積替保管現場名2_3
                        sql.Append(" , TME2.TMH_GENBA_ADDRESS2_3 AS TMH_GENBA_ADDRESS2_3 ");                                  // 積替保管場所住所2_3
                        sql.Append(" , TME2.TMH_GENBA_CHIIKI_CD2_3 AS TMH_GENBA_CHIIKI_CD2_3 ");                              // 積替保管場所地域CD2_3
                        sql.Append(" , TME2.TMH_GENBA_CHIIKI_NAME2_3 AS TMH_GENBA_CHIIKI_NAME2_3 ");                          // 積替保管場所地域名2_3
                        sql.Append(" , TME2.TMH_UPN_CHIIKI_CD2_3 AS TMH_UPN_CHIIKI_CD2_3 ");                                  // 積替保管場所運搬報告先地域CD2_3
                        sql.Append(" , TME2.TMH_UPN_CHIIKI_NAME2_3 AS TMH_UPN_CHIIKI_NAME2_3 ");                              // 積替保管場所運搬報告先地域名2_3
                        sql.Append(" , TME2.TMH_GYOUSHA_CD2_4 AS TMH_GYOUSHA_CD2_4 ");                                        // 積替保管業者CD2_4
                        sql.Append(" , TME2.TMH_GYOUSHA_NAME2_4 AS TMH_GYOUSHA_NAME2_4 ");                                    // 積替保管業者名2_4
                        sql.Append(" , TME2.TMH_GENBA_CD2_4 AS TMH_GENBA_CD2_4 ");                                            // 積替保管現場CD2_4
                        sql.Append(" , TME2.TMH_GENBA_NAME2_4 AS TMH_GENBA_NAME2_4 ");                                        // 積替保管現場名2_4
                        sql.Append(" , TME2.TMH_GENBA_ADDRESS2_4 AS TMH_GENBA_ADDRESS2_4 ");                                  // 積替保管場所住所2_4
                        sql.Append(" , TME2.TMH_GENBA_CHIIKI_CD2_4 AS TMH_GENBA_CHIIKI_CD2_4 ");                              // 積替保管場所地域CD2_4
                        sql.Append(" , TME2.TMH_GENBA_CHIIKI_NAME2_4 AS TMH_GENBA_CHIIKI_NAME2_4 ");                          // 積替保管場所地域名2_4
                        sql.Append(" , TME2.TMH_UPN_CHIIKI_CD2_4 AS TMH_UPN_CHIIKI_CD2_4 ");                                  // 積替保管場所運搬報告先地域CD2_4
                        sql.Append(" , TME2.TMH_UPN_CHIIKI_NAME2_4 AS TMH_UPN_CHIIKI_NAME2_4 ");                              // 積替保管場所運搬報告先地域名2_4
                        // 明細
                        sql.Append(" , TME2.HAIKI_SHURUI_CD2 AS HAIKI_SHURUI_CD2 ");                                          // 廃棄物種類CD2
                        sql.Append(" , TME2.HAIKI_SHURUI_NAME2 AS HAIKI_SHURUI_NAME2 ");                                      // 廃棄物種類名2
                        sql.Append(" , TMD.GENNYOU_SUU AS HIKIWATASHI ");                                                     // 引渡量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD2_2 ");                          // 単位CD（引渡量）
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME2_2 ");                                    // 単位名（引渡量）
                        sql.Append(" , TME2.ITAKU_SBN_CD AS ITAKU_SBN_CD ");                                                  // 委託処分方法CD
                        sql.Append(" , TME2.ITAKU_SBN_NAME AS ITAKU_SBN_NAME ");                                              // 委託処分方法

                        #endregion

                        #region FROM

                        // マニ入力
                        sql.Append(" FROM T_MANIFEST_ENTRY TME ");

                        // 明細
                        sql.Append(" LEFT JOIN T_MANIFEST_DETAIL TMD ");
                        sql.Append(" ON TME.SYSTEM_ID = TMD.SYSTEM_ID AND TME.SEQ = TMD.SEQ ");
                        // 廃棄種類
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI MHS1 ");
                        sql.Append(" ON TMD.HAIKI_SHURUI_CD = MHS1.HAIKI_SHURUI_CD AND MHS1.HAIKI_KBN_CD = 1 AND MHS1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI MHS2 ");
                        sql.Append(" ON TMD.HAIKI_SHURUI_CD = MHS2.HAIKI_SHURUI_CD AND MHS2.HAIKI_KBN_CD = 2 AND MHS2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI MHS3 ");
                        sql.Append(" ON TMD.HAIKI_SHURUI_CD = MHS3.HAIKI_SHURUI_CD AND MHS3.HAIKI_KBN_CD = 3 AND MHS3.DELETE_FLG = 0 ");
                        // 報告書分類
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI MHB1 ");
                        sql.Append(" ON MHS1.HOUKOKUSHO_BUNRUI_CD = MHB1.HOUKOKUSHO_BUNRUI_CD AND MHB1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI MHB2 ");
                        sql.Append(" ON MHS2.HOUKOKUSHO_BUNRUI_CD = MHB2.HOUKOKUSHO_BUNRUI_CD AND MHB2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI MHB3 ");
                        sql.Append(" ON MHS3.HOUKOKUSHO_BUNRUI_CD = MHB3.HOUKOKUSHO_BUNRUI_CD AND MHB3.DELETE_FLG = 0 ");
                        // 廃棄名称
                        sql.Append(" LEFT JOIN M_HAIKI_NAME MHN ");
                        sql.Append(" ON TMD.HAIKI_NAME_CD = MHN.HAIKI_NAME_CD AND MHN.DELETE_FLG = 0 ");
                        // 単位
                        sql.Append(" LEFT JOIN M_UNIT TMD_MU ");
                        sql.Append(" ON TMD.HAIKI_UNIT_CD = TMD_MU.UNIT_CD AND TMD_MU.DELETE_FLG = 0 ");
                        // 排出事業者
                        sql.Append(" LEFT JOIN M_GYOUSHA HST_GYOUSHA ");
                        sql.Append(" ON TME.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD AND HST_GYOUSHA.DELETE_FLG = 0 ");
                        // 排出事業者地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GYOUSHA_CHIIKI ");
                        sql.Append(" ON HST_GYOUSHA.CHIIKI_CD = HST_GYOUSHA_CHIIKI.CHIIKI_CD AND HST_GYOUSHA_CHIIKI.DELETE_FLG = 0 ");
                        // 排出事業者業種
                        sql.Append(" LEFT JOIN M_GYOUSHU HST_GYOUSHA_GYOUSHU ");
                        sql.Append(" ON HST_GYOUSHA.GYOUSHU_CD = HST_GYOUSHA_GYOUSHU.GYOUSHU_CD AND HST_GYOUSHA_GYOUSHU.DELETE_FLG = 0 ");
                        // 排出事業場
                        sql.Append(" LEFT JOIN M_GENBA HST_GENBA ");
                        sql.Append(" ON TME.HST_GYOUSHA_CD = HST_GENBA.GYOUSHA_CD AND TME.HST_GENBA_CD = HST_GENBA.GENBA_CD AND HST_GENBA.DELETE_FLG = 0 ");
                        // 排出事業場地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GENBA_CHIIKI ");
                        sql.Append(" ON HST_GENBA.CHIIKI_CD = HST_GENBA_CHIIKI.CHIIKI_CD AND HST_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        // 排出事業場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON HST_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = HST_GENBA_UPN_CHIIKI.CHIIKI_CD AND HST_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        // 排出事業場業種
                        sql.Append(" LEFT JOIN M_GYOUSHU HST_GENBA_GYOUSHU ");
                        sql.Append(" ON HST_GENBA.GYOUSHU_CD = HST_GENBA_GYOUSHU.GYOUSHU_CD AND HST_GENBA_GYOUSHU.DELETE_FLG = 0 ");
                        // 運搬受託者
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN UPN1 ");
                        sql.Append(" ON TME.SYSTEM_ID = UPN1.SYSTEM_ID AND TME.SEQ = UPN1.SEQ ");
                        sql.Append(" AND UPN1.UPN_ROUTE_NO = 1 ");
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN UPN2 ");
                        sql.Append(" ON TME.SYSTEM_ID = UPN2.SYSTEM_ID AND TME.SEQ = UPN2.SEQ ");
                        sql.Append(" AND UPN2.UPN_ROUTE_NO = 2 ");
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN UPN3 ");
                        sql.Append(" ON TME.SYSTEM_ID = UPN3.SYSTEM_ID AND TME.SEQ = UPN3.SEQ ");
                        sql.Append(" AND UPN3.UPN_ROUTE_NO = 3 ");
                        sql.Append(" LEFT JOIN M_GYOUSHA UPN_GYOUSHA1 ");
                        sql.Append(" ON UPN1.UPN_SAKI_GYOUSHA_CD = UPN_GYOUSHA1.GYOUSHA_CD AND UPN_GYOUSHA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GYOUSHA UPN_GYOUSHA2 ");
                        sql.Append(" ON UPN2.UPN_SAKI_GYOUSHA_CD = UPN_GYOUSHA2.GYOUSHA_CD AND UPN_GYOUSHA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GYOUSHA UPN_JISHA_GYOUSHA1 ");
                        sql.Append(" ON UPN1.UPN_GYOUSHA_CD = UPN_JISHA_GYOUSHA1.GYOUSHA_CD AND UPN_JISHA_GYOUSHA1.DELETE_FLG = 0 ");
                        // 積替保管現場
                        sql.Append(" LEFT JOIN M_GENBA TMH_GENBA ");
                        sql.Append(" ON TME.TMH_GYOUSHA_CD = TMH_GENBA.GYOUSHA_CD AND TME.TMH_GENBA_CD = TMH_GENBA.GENBA_CD AND TMH_GENBA.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UPN_GENBA1 ");
                        sql.Append(" ON UPN1.UPN_SAKI_GYOUSHA_CD = UPN_GENBA1.GYOUSHA_CD AND UPN1.UPN_SAKI_GENBA_CD = UPN_GENBA1.GENBA_CD AND UPN_GENBA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UPN_GENBA2 ");
                        sql.Append(" ON UPN2.UPN_SAKI_GYOUSHA_CD = UPN_GENBA2.GYOUSHA_CD AND UPN2.UPN_SAKI_GENBA_CD = UPN_GENBA2.GENBA_CD AND UPN_GENBA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UPN_GENBA3 ");
                        sql.Append(" ON UPN3.UPN_SAKI_GYOUSHA_CD = UPN_GENBA3.GYOUSHA_CD AND UPN3.UPN_SAKI_GENBA_CD = UPN_GENBA3.GENBA_CD AND UPN_GENBA3.DELETE_FLG = 0 ");
                        // 積替保管現場地域
                        sql.Append(" LEFT JOIN M_CHIIKI TMH_GENBA_CHIIKI ");
                        sql.Append(" ON TMH_GENBA.CHIIKI_CD = TMH_GENBA_CHIIKI.CHIIKI_CD AND TMH_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_CHIIKI1 ");
                        sql.Append(" ON UPN_GENBA1.CHIIKI_CD = UPN_GENBA_CHIIKI1.CHIIKI_CD AND UPN_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_CHIIKI2 ");
                        sql.Append(" ON UPN_GENBA2.CHIIKI_CD = UPN_GENBA_CHIIKI2.CHIIKI_CD AND UPN_GENBA_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_CHIIKI3 ");
                        sql.Append(" ON UPN_GENBA3.CHIIKI_CD = UPN_GENBA_CHIIKI3.CHIIKI_CD AND UPN_GENBA_CHIIKI3.DELETE_FLG = 0 ");
                        // 積替保管現場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI TMH_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON TMH_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = TMH_GENBA_UPN_CHIIKI.CHIIKI_CD AND TMH_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON UPN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_GENBA_UPN_CHIIKI1.CHIIKI_CD AND UPN_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_UPN_CHIIKI2 ");
                        sql.Append(" ON UPN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_GENBA_UPN_CHIIKI2.CHIIKI_CD AND UPN_GENBA_UPN_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_UPN_CHIIKI3 ");
                        sql.Append(" ON UPN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_GENBA_UPN_CHIIKI3.CHIIKI_CD AND UPN_GENBA_UPN_CHIIKI3.DELETE_FLG = 0 ");
                        // 処分事業場
                        sql.Append(" LEFT JOIN M_GENBA SBN_GENBA ");
                        sql.Append(" ON TME.SBN_GYOUSHA_CD = SBN_GENBA.GYOUSHA_CD AND UPN1.UPN_SAKI_GENBA_CD = SBN_GENBA.GENBA_CD AND SBN_GENBA.DELETE_FLG = 0 ");
                        // 処分事業場地域
                        sql.Append(" LEFT JOIN M_CHIIKI SBN_GENBA_CHIIKI ");
                        sql.Append(" ON SBN_GENBA.CHIIKI_CD = SBN_GENBA_CHIIKI.CHIIKI_CD AND SBN_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        // 処分事業場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI SBN_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON SBN_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = SBN_GENBA_UPN_CHIIKI.CHIIKI_CD AND SBN_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        // 処分方法
                        sql.Append(" LEFT JOIN M_SHOBUN_HOUHOU DETAIL_SBN_HOUHOU ");
                        sql.Append(" ON TMD.SBN_HOUHOU_CD = DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_CD AND DETAIL_SBN_HOUHOU.DELETE_FLG = 0 ");
                        // 検索用運搬受託者､処分事業場
                        sql.Append("            LEFT JOIN T_MANIFEST_UPN TMU_SEARCH ");
                        sql.Append("                   ON TME.SYSTEM_ID = TMU_SEARCH.SYSTEM_ID ");
                        sql.Append("                  AND TME.SEQ = TMU_SEARCH.SEQ ");
                        sql.Append("                  AND TMU_SEARCH.UPN_ROUTE_NO = (SELECT MAX(T_MANIFEST_UPN.UPN_ROUTE_NO) FROM T_MANIFEST_UPN ");
                        sql.Append("                  WHERE SYSTEM_ID = TME.SYSTEM_ID AND SEQ = TME.SEQ AND UPN_SAKI_KBN = 1) ");
                        sql.Append("                  AND TMU_SEARCH.UPN_SAKI_KBN = 1 ");

                        #region 紐づく二次マニ

                        // 二次マニ
                        sql.Append(" LEFT JOIN T_MANIFEST_RELATION ");
                        sql.Append(" ON TMD.DETAIL_SYSTEM_ID = T_MANIFEST_RELATION.FIRST_SYSTEM_ID ");
                        sql.Append(" AND T_MANIFEST_RELATION.DELETE_FLG = 0 ");
                        sql.Append(" AND T_MANIFEST_RELATION.FIRST_HAIKI_KBN_CD <> 4 ");
                        sql.Append(" AND T_MANIFEST_RELATION.REC_SEQ = (SELECT MAX(TMP.REC_SEQ) FROM T_MANIFEST_RELATION TMP ");
                        sql.Append(" WHERE TMP.FIRST_SYSTEM_ID = T_MANIFEST_RELATION.FIRST_SYSTEM_ID AND TMP.DELETE_FLG = 0 AND TMP.FIRST_HAIKI_KBN_CD <> 4) ");

                        sql.Append(" LEFT JOIN ( ");
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(" MR.NEXT_SYSTEM_ID AS SYSTEM_ID ");
                        sql.Append(" ,MR.NEXT_HAIKI_KBN_CD AS HAIKI_KBN_CD ");
                        // 交付番号2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN R18EX.MANIFEST_ID ELSE ME.MANIFEST_ID END) AS MANIFEST_ID ");
                        // 交付年月日2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN ");
                        sql.Append("        CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ELSE NULL END ");
                        sql.Append(" ELSE ME.KOUFU_DATE END) AS KOUFU_DATE ");
                        // 運搬受託者CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX1.UPN_GYOUSHA_CD ELSE T_MANIFEST_UPN1.UPN_GYOUSHA_CD END) AS UPN_GYOUSHA_CD1 ");
                        // 運搬受託者名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R191.UPN_SHA_NAME ELSE T_MANIFEST_UPN1.UPN_GYOUSHA_NAME END) AS UPN_GYOUSHA_NAME1 ");
                        // 運搬受託者CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX2.UPN_GYOUSHA_CD ELSE T_MANIFEST_UPN2.UPN_GYOUSHA_CD END) AS UPN_GYOUSHA_CD2 ");
                        // 運搬受託者名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R192.UPN_SHA_NAME ELSE T_MANIFEST_UPN2.UPN_GYOUSHA_NAME END) AS UPN_GYOUSHA_NAME2 ");
                        // 運搬受託者CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX3.UPN_GYOUSHA_CD ELSE T_MANIFEST_UPN3.UPN_GYOUSHA_CD END) AS UPN_GYOUSHA_CD3 ");
                        // 運搬受託者名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R193.UPN_SHA_NAME ELSE T_MANIFEST_UPN3.UPN_GYOUSHA_NAME END) AS UPN_GYOUSHA_NAME3 ");
                        // 運搬受託者CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX4.UPN_GYOUSHA_CD ELSE NULL END) AS UPN_GYOUSHA_CD4 ");
                        // 運搬受託者名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R194.UPN_SHA_NAME ELSE NULL END) AS UPN_GYOUSHA_NAME4 ");
                        // 運搬受託者CD2_5
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX5.UPN_GYOUSHA_CD ELSE NULL END) AS UPN_GYOUSHA_CD5 ");
                        // 運搬受託者名2_5
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R195.UPN_SHA_NAME ELSE NULL END) AS UPN_GYOUSHA_NAME5 ");
                        // 積替保管業者CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX1.UPNSAKI_GYOUSHA_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN1.UPN_SAKI_GYOUSHA_CD ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GYOUSHA_CD END END )AS TMH_GYOUSHA_CD2_1 ");
                        // 積替保管業者名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R191.UPNSAKI_NAME END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GYOUSHA1.GYOUSHA_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GYOUSHA_NAME END END )AS TMH_GYOUSHA_NAME2_1 ");
                        // 積替保管現場CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX1.UPNSAKI_GENBA_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN1.UPN_SAKI_GENBA_CD ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GENBA_CD END END )AS TMH_GENBA_CD2_1 ");
                        // 積替保管現場名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R191.UPNSAKI_JOU_NAME END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN1.UPN_SAKI_GENBA_NAME ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GENBA_NAME END END )AS TMH_GENBA_NAME2_1 ");
                        // 積替保管場所住所2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE ISNULL(DT_R191.UPNSAKI_JOU_ADDRESS1, '') + ");
                        sql.Append("        ISNULL(DT_R191.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DT_R191.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R191.UPNSAKI_JOU_ADDRESS4, '') END  ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN1.UPN_SAKI_GENBA_ADDRESS ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GENBA_ADDRESS END END )AS TMH_GENBA_ADDRESS2_1 ");
                        // 積替保管場所地域CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA1.CHIIKI_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA1.CHIIKI_CD ELSE '' END ");
                        sql.Append("        ELSE TME_TMH_GENBA.CHIIKI_CD END END )AS TMH_GENBA_CHIIKI_CD2_1 ");
                        // 積替保管場所地域名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE TME_TMH_GENBA_CHIIKI.CHIIKI_NAME_RYAKU END END )AS TMH_GENBA_CHIIKI_NAME2_1 ");
                        // 積替保管場所運搬報告先地域CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END ");
                        sql.Append("        ELSE TME_TMH_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END END )AS TMH_UPN_CHIIKI_CD2_1 ");
                        // 積替保管場所運搬報告先地域名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE TME_TMH_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU END END )AS TMH_UPN_CHIIKI_NAME2_1 ");
                        // 積替保管業者CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX2.UPNSAKI_GYOUSHA_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN2.UPN_SAKI_GYOUSHA_CD ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GYOUSHA_CD2_2 ");
                        // 積替保管業者名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R192.UPNSAKI_NAME END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GYOUSHA2.GYOUSHA_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE '' END END ) AS TMH_GYOUSHA_NAME2_2 ");
                        // 積替保管現場CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX2.UPNSAKI_GENBA_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN2.UPN_SAKI_GENBA_CD ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_CD2_2 ");
                        // 積替保管現場名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R192.UPNSAKI_JOU_NAME END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN2.UPN_SAKI_GENBA_NAME ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_NAME2_2 ");
                        // 積替保管場所住所2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE ISNULL(DT_R192.UPNSAKI_JOU_ADDRESS1, '') + ");
                        sql.Append("        ISNULL(DT_R192.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DT_R192.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R192.UPNSAKI_JOU_ADDRESS4, '') END  ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN2.UPN_SAKI_GENBA_ADDRESS ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_ADDRESS2_2 ");
                        // 積替保管場所地域CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA2.CHIIKI_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA2.CHIIKI_CD ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_CHIIKI_CD2_2 ");
                        // 積替保管場所地域名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_CHIIKI_NAME2_2 ");
                        // 積替保管場所運搬報告先地域CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_UPN_CHIIKI_CD2_2 ");
                        // 積替保管場所運搬報告先地域名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_UPN_CHIIKI_NAME2_2 ");
                        // 積替保管業者CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX3.UPNSAKI_GYOUSHA_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GYOUSHA_CD2_3 ");
                        // 積替保管業者名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R193.UPNSAKI_NAME END ");
                        sql.Append("   ELSE NULL END ) AS TMH_GYOUSHA_NAME2_3 ");
                        // 積替保管現場CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX3.UPNSAKI_GENBA_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CD2_3 ");
                        // 積替保管現場名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R193.UPNSAKI_JOU_NAME END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_NAME2_3 ");
                        // 積替保管場所住所2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE ISNULL(DT_R193.UPNSAKI_JOU_ADDRESS1, '') + ");
                        sql.Append("        ISNULL(DT_R193.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DT_R193.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R193.UPNSAKI_JOU_ADDRESS4, '') END  ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_ADDRESS2_3 ");
                        // 積替保管場所地域CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA3.CHIIKI_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CHIIKI_CD2_3 ");
                        // 積替保管場所地域名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_CHIIKI3.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CHIIKI_NAME2_3 ");
                        // 積替保管場所運搬報告先地域CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_UPN_CHIIKI_CD2_3 ");
                        // 積替保管場所運搬報告先地域名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_UPN_CHIIKI3.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE NULL END )AS TMH_UPN_CHIIKI_NAME2_3 ");
                        // 積替保管業者CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX4.UPNSAKI_GYOUSHA_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GYOUSHA_CD2_4 ");
                        // 積替保管業者名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R194.UPNSAKI_NAME END ");
                        sql.Append("   ELSE NULL END ) AS TMH_GYOUSHA_NAME2_4 ");
                        // 積替保管現場CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX4.UPNSAKI_GENBA_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CD2_4 ");
                        // 積替保管現場名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_R194.UPNSAKI_JOU_NAME END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_NAME2_4 ");
                        // 積替保管場所住所2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE ISNULL(DT_R194.UPNSAKI_JOU_ADDRESS1, '') + ");
                        sql.Append("        ISNULL(DT_R194.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DT_R194.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R194.UPNSAKI_JOU_ADDRESS4, '') END  ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_ADDRESS2_4 ");
                        // 積替保管場所地域CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA4.CHIIKI_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CHIIKI_CD2_4 ");
                        // 積替保管場所地域名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_CHIIKI4.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CHIIKI_NAME2_4 ");
                        // 積替保管場所運搬報告先地域CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA4.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_UPN_CHIIKI_CD2_4 ");
                        // 積替保管場所運搬報告先地域名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_UPN_CHIIKI4.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE NULL END )AS TMH_UPN_CHIIKI_NAME2_4 ");
                        // 廃遺物種類CD2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN ISNULL(R18.HAIKI_DAI_CODE, '') + ISNULL(R18.HAIKI_CHU_CODE, '') + ");
                        sql.Append("                                           ISNULL(R18.HAIKI_SHO_CODE, '') + ISNULL(R18.HAIKI_SAI_CODE, '') ");
                        sql.Append("   ELSE ME.HAIKI_SHURUI_CD END )AS HAIKI_SHURUI_CD2 ");
                        // 廃棄物種類名2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN R18.HAIKI_SHURUI ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 1 THEN TME_MHS1.HAIKI_SHURUI_NAME_RYAKU ");
                        sql.Append("        WHEN 2 THEN TME_MHS2.HAIKI_SHURUI_NAME_RYAKU WHEN 3 THEN TME_MHS3.HAIKI_SHURUI_NAME_RYAKU ELSE '' END ");
                        sql.Append("   END ) AS HAIKI_SHURUI_NAME2 ");
                        // 引渡量
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN R18EX.GENNYOU_SUU ");
                        sql.Append("   ELSE ME.GENNYOU_SUU END ) AS HIKIWATASHI ");
                        // 委託処分方法CD
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN R18EX.SBN_HOUHOU_CD ");
                        sql.Append("   ELSE ME.SBN_HOUHOU_CD END ) AS ITAKU_SBN_CD ");
                        // 委託処分方法
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU ");
                        sql.Append("   ELSE ME_DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU END ) AS ITAKU_SBN_NAME ");

                        sql.Append(" FROM T_MANIFEST_RELATION AS MR");
                        // 紙マニ
                        sql.Append(" LEFT JOIN ");
                        sql.Append(" (SELECT T_MANIFEST_ENTRY.*,T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID, ");
                        sql.Append("         T_MANIFEST_DETAIL.HAIKI_SHURUI_CD, T_MANIFEST_DETAIL.GENNYOU_SUU, T_MANIFEST_DETAIL.SBN_HOUHOU_CD ");
                        sql.Append("    FROM T_MANIFEST_ENTRY LEFT JOIN T_MANIFEST_DETAIL ON ");
                        sql.Append("    T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                        sql.Append("  AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ ");
                        sql.Append("  WHERE DELETE_FLG = 0) AS ME ");
                        sql.Append(" ON MR.NEXT_SYSTEM_ID = ME.DETAIL_SYSTEM_ID ");
                        sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = ME.HAIKI_KBN_CD ");

                        sql.Append(" LEFT JOIN M_GENBA TME_TMH_GENBA ");
                        sql.Append(" ON ME.TMH_GYOUSHA_CD = TME_TMH_GENBA.GYOUSHA_CD AND ME.TMH_GENBA_CD = TME_TMH_GENBA.GENBA_CD AND TME_TMH_GENBA.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_TMH_GENBA_CHIIKI ");
                        sql.Append(" ON TME_TMH_GENBA.CHIIKI_CD = TME_TMH_GENBA_CHIIKI.CHIIKI_CD AND TME_TMH_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_TMH_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON TME_TMH_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = TME_TMH_GENBA_UPN_CHIIKI.CHIIKI_CD AND TME_TMH_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        // 廃棄種類
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI TME_MHS1 ");
                        sql.Append(" ON ME.HAIKI_SHURUI_CD = TME_MHS1.HAIKI_SHURUI_CD AND TME_MHS1.HAIKI_KBN_CD = 1 AND TME_MHS1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI TME_MHS2 ");
                        sql.Append(" ON ME.HAIKI_SHURUI_CD = TME_MHS2.HAIKI_SHURUI_CD AND TME_MHS2.HAIKI_KBN_CD = 2 AND TME_MHS2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI TME_MHS3 ");
                        sql.Append(" ON ME.HAIKI_SHURUI_CD = TME_MHS3.HAIKI_SHURUI_CD AND TME_MHS3.HAIKI_KBN_CD = 3 AND TME_MHS3.DELETE_FLG = 0 ");
                        // 運搬受託者1
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN1 ");
                        sql.Append(" ON ME.SYSTEM_ID = T_MANIFEST_UPN1.SYSTEM_ID AND ME.SEQ = T_MANIFEST_UPN1.SEQ ");
                        sql.Append(" AND T_MANIFEST_UPN1.UPN_ROUTE_NO = 1 ");

                        sql.Append(" LEFT JOIN M_GYOUSHA TME_UPNSAKI_GYOUSHA1 ");
                        sql.Append(" ON T_MANIFEST_UPN1.UPN_SAKI_GYOUSHA_CD = TME_UPNSAKI_GYOUSHA1.GYOUSHA_CD AND TME_UPNSAKI_GYOUSHA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA TME_UPNSAKI_GENBA1 ");
                        sql.Append(" ON T_MANIFEST_UPN1.UPN_SAKI_GYOUSHA_CD = TME_UPNSAKI_GENBA1.GYOUSHA_CD ");
                        sql.Append(" AND T_MANIFEST_UPN1.UPN_SAKI_GENBA_CD = TME_UPNSAKI_GENBA1.GENBA_CD AND TME_UPNSAKI_GENBA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_UPNSAKI_GENBA_CHIIKI1 ");
                        sql.Append(" ON TME_UPNSAKI_GENBA1.CHIIKI_CD = TME_UPNSAKI_GENBA_CHIIKI1.CHIIKI_CD AND TME_UPNSAKI_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_UPNSAKI_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON TME_UPNSAKI_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = TME_UPNSAKI_GENBA_UPN_CHIIKI1.CHIIKI_CD AND TME_UPNSAKI_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        // 運搬受託者2
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN2 ");
                        sql.Append(" ON ME.SYSTEM_ID = T_MANIFEST_UPN2.SYSTEM_ID AND ME.SEQ = T_MANIFEST_UPN2.SEQ ");
                        sql.Append(" AND T_MANIFEST_UPN2.UPN_ROUTE_NO = 2 ");

                        sql.Append(" LEFT JOIN M_GYOUSHA TME_UPNSAKI_GYOUSHA2 ");
                        sql.Append(" ON T_MANIFEST_UPN2.UPN_SAKI_GYOUSHA_CD = TME_UPNSAKI_GYOUSHA2.GYOUSHA_CD AND TME_UPNSAKI_GYOUSHA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA TME_UPNSAKI_GENBA2 ");
                        sql.Append(" ON T_MANIFEST_UPN2.UPN_SAKI_GYOUSHA_CD = TME_UPNSAKI_GENBA2.GYOUSHA_CD ");
                        sql.Append(" AND T_MANIFEST_UPN2.UPN_SAKI_GENBA_CD = TME_UPNSAKI_GENBA2.GENBA_CD AND TME_UPNSAKI_GENBA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_UPNSAKI_GENBA_CHIIKI2 ");
                        sql.Append(" ON TME_UPNSAKI_GENBA2.CHIIKI_CD = TME_UPNSAKI_GENBA_CHIIKI2.CHIIKI_CD AND TME_UPNSAKI_GENBA_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_UPNSAKI_GENBA_UPN_CHIIKI2 ");
                        sql.Append(" ON TME_UPNSAKI_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = TME_UPNSAKI_GENBA_UPN_CHIIKI2.CHIIKI_CD AND TME_UPNSAKI_GENBA_UPN_CHIIKI2.DELETE_FLG = 0 ");
                        // 運搬受託者3
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN3 ");
                        sql.Append(" ON ME.SYSTEM_ID = T_MANIFEST_UPN3.SYSTEM_ID AND ME.SEQ = T_MANIFEST_UPN3.SEQ ");
                        sql.Append(" AND T_MANIFEST_UPN3.UPN_ROUTE_NO = 3 ");
                        // 処分方法
                        sql.Append(" LEFT JOIN M_SHOBUN_HOUHOU ME_DETAIL_SBN_HOUHOU ");
                        sql.Append(" ON ME.SBN_HOUHOU_CD = ME_DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_CD AND ME_DETAIL_SBN_HOUHOU.DELETE_FLG = 0 ");
                        // 電マニ
                        sql.Append(" LEFT JOIN ");
                        sql.Append(" (SELECT * FROM DT_R18_EX WHERE DELETE_FLG = 0) AS R18EX ");
                        sql.Append(" ON MR.NEXT_SYSTEM_ID = R18EX.SYSTEM_ID ");
                        sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = 4 ");
                        sql.Append(" LEFT JOIN DT_MF_TOC DT_TOC ");
                        sql.Append(" ON R18EX.KANRI_ID = DT_TOC.KANRI_ID ");
                        sql.Append(" LEFT JOIN DT_R18 R18 ");
                        sql.Append(" ON DT_TOC.KANRI_ID = R18.KANRI_ID AND DT_TOC.LATEST_SEQ = R18.SEQ ");
                        // 運搬受託者1
                        sql.Append(" LEFT JOIN DT_R19 DT_R191 ");
                        sql.Append(" ON DT_R191.KANRI_ID = R18.KANRI_ID ");
                        sql.Append(" AND DT_R191.SEQ =  R18.SEQ ");
                        sql.Append(" AND DT_R191.UPN_ROUTE_NO = 1 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX1 ");
                        sql.Append(" ON R18EX.SYSTEM_ID = DT_R19_EX1.SYSTEM_ID AND R18EX.SEQ = DT_R19_EX1.SEQ ");
                        sql.Append(" AND R18EX.KANRI_ID = DT_R19_EX1.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX1.UPN_ROUTE_NO = DT_R191.UPN_ROUTE_NO ");

                        sql.Append(" LEFT JOIN M_GENBA DT_UNPAN_GENBA1 ");
                        sql.Append(" ON DT_R19_EX1.UPNSAKI_GYOUSHA_CD = DT_UNPAN_GENBA1.GYOUSHA_CD AND DT_R19_EX1.UPNSAKI_GENBA_CD = DT_UNPAN_GENBA1.GENBA_CD ");
                        sql.Append(" AND DT_UNPAN_GENBA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_CHIIKI1 ");
                        sql.Append(" ON DT_UNPAN_GENBA1.CHIIKI_CD = DT_UNPAN_GENBA_CHIIKI1.CHIIKI_CD AND DT_UNPAN_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON DT_UNPAN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = DT_UNPAN_GENBA_UPN_CHIIKI1.CHIIKI_CD AND DT_UNPAN_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        // 運搬受託者2
                        sql.Append(" LEFT JOIN DT_R19 DT_R192 ");
                        sql.Append(" ON DT_R192.KANRI_ID = R18.KANRI_ID ");
                        sql.Append(" AND DT_R192.SEQ =  R18.SEQ ");
                        sql.Append(" AND DT_R192.UPN_ROUTE_NO = 2 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX2 ");
                        sql.Append(" ON R18EX.SYSTEM_ID = DT_R19_EX2.SYSTEM_ID AND R18EX.SEQ = DT_R19_EX2.SEQ ");
                        sql.Append(" AND R18EX.KANRI_ID = DT_R19_EX2.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX2.UPN_ROUTE_NO = DT_R192.UPN_ROUTE_NO ");

                        sql.Append(" LEFT JOIN M_GENBA DT_UNPAN_GENBA2 ");
                        sql.Append(" ON DT_R19_EX2.UPNSAKI_GYOUSHA_CD = DT_UNPAN_GENBA2.GYOUSHA_CD AND DT_R19_EX2.UPNSAKI_GENBA_CD = DT_UNPAN_GENBA2.GENBA_CD ");
                        sql.Append(" AND DT_UNPAN_GENBA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_CHIIKI2 ");
                        sql.Append(" ON DT_UNPAN_GENBA2.CHIIKI_CD = DT_UNPAN_GENBA_CHIIKI2.CHIIKI_CD AND DT_UNPAN_GENBA_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_UPN_CHIIKI2 ");
                        sql.Append(" ON DT_UNPAN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = DT_UNPAN_GENBA_UPN_CHIIKI2.CHIIKI_CD AND DT_UNPAN_GENBA_UPN_CHIIKI2.DELETE_FLG = 0 ");
                        // 運搬受託者3
                        sql.Append(" LEFT JOIN DT_R19 DT_R193 ");
                        sql.Append(" ON DT_R193.KANRI_ID = R18.KANRI_ID ");
                        sql.Append(" AND DT_R193.SEQ =  R18.SEQ ");
                        sql.Append(" AND DT_R193.UPN_ROUTE_NO = 3 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX3 ");
                        sql.Append(" ON R18EX.SYSTEM_ID = DT_R19_EX3.SYSTEM_ID AND R18EX.SEQ = DT_R19_EX3.SEQ ");
                        sql.Append(" AND R18EX.KANRI_ID = DT_R19_EX3.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX3.UPN_ROUTE_NO = DT_R193.UPN_ROUTE_NO ");

                        sql.Append(" LEFT JOIN M_GENBA DT_UNPAN_GENBA3 ");
                        sql.Append(" ON DT_R19_EX3.UPNSAKI_GYOUSHA_CD = DT_UNPAN_GENBA3.GYOUSHA_CD AND DT_R19_EX3.UPNSAKI_GENBA_CD = DT_UNPAN_GENBA3.GENBA_CD ");
                        sql.Append(" AND DT_UNPAN_GENBA3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_CHIIKI3 ");
                        sql.Append(" ON DT_UNPAN_GENBA3.CHIIKI_CD = DT_UNPAN_GENBA_CHIIKI3.CHIIKI_CD AND DT_UNPAN_GENBA_CHIIKI3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_UPN_CHIIKI3 ");
                        sql.Append(" ON DT_UNPAN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = DT_UNPAN_GENBA_UPN_CHIIKI3.CHIIKI_CD AND DT_UNPAN_GENBA_UPN_CHIIKI3.DELETE_FLG = 0 ");
                        // 運搬受託者4
                        sql.Append(" LEFT JOIN DT_R19 DT_R194 ");
                        sql.Append(" ON DT_R194.KANRI_ID = R18.KANRI_ID ");
                        sql.Append(" AND DT_R194.SEQ =  R18.SEQ ");
                        sql.Append(" AND DT_R194.UPN_ROUTE_NO = 4 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX4 ");
                        sql.Append(" ON R18EX.SYSTEM_ID = DT_R19_EX4.SYSTEM_ID AND R18EX.SEQ = DT_R19_EX4.SEQ ");
                        sql.Append(" AND R18EX.KANRI_ID = DT_R19_EX4.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX4.UPN_ROUTE_NO = DT_R194.UPN_ROUTE_NO ");

                        sql.Append(" LEFT JOIN M_GENBA DT_UNPAN_GENBA4 ");
                        sql.Append(" ON DT_R19_EX4.UPNSAKI_GYOUSHA_CD = DT_UNPAN_GENBA4.GYOUSHA_CD AND DT_R19_EX4.UPNSAKI_GENBA_CD = DT_UNPAN_GENBA4.GENBA_CD ");
                        sql.Append(" AND DT_UNPAN_GENBA4.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_CHIIKI4 ");
                        sql.Append(" ON DT_UNPAN_GENBA4.CHIIKI_CD = DT_UNPAN_GENBA_CHIIKI4.CHIIKI_CD AND DT_UNPAN_GENBA_CHIIKI4.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_UPN_CHIIKI4 ");
                        sql.Append(" ON DT_UNPAN_GENBA4.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = DT_UNPAN_GENBA_UPN_CHIIKI4.CHIIKI_CD AND DT_UNPAN_GENBA_UPN_CHIIKI4.DELETE_FLG = 0 ");
                        // 運搬受託者5
                        sql.Append(" LEFT JOIN DT_R19 DT_R195 ");
                        sql.Append(" ON DT_R195.KANRI_ID = R18.KANRI_ID ");
                        sql.Append(" AND DT_R195.SEQ =  R18.SEQ ");
                        sql.Append(" AND DT_R195.UPN_ROUTE_NO = 5 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX5 ");
                        sql.Append(" ON R18EX.SYSTEM_ID = DT_R19_EX5.SYSTEM_ID AND R18EX.SEQ = DT_R19_EX5.SEQ ");
                        sql.Append(" AND R18EX.KANRI_ID = DT_R19_EX5.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX5.UPN_ROUTE_NO = DT_R195.UPN_ROUTE_NO ");
                        // 処分方法
                        sql.Append(" LEFT JOIN M_SHOBUN_HOUHOU DT_DETAIL_SBN_HOUHOU ");
                        sql.Append(" ON R18EX.SBN_HOUHOU_CD = DT_DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_CD AND DT_DETAIL_SBN_HOUHOU.DELETE_FLG = 0 ");

                        sql.Append(" WHERE MR.DELETE_FLG = 0 ");
                        sql.Append(" ) AS TME2 ");
                        sql.Append(" ON T_MANIFEST_RELATION.NEXT_SYSTEM_ID = TME2.SYSTEM_ID ");
                        sql.Append(" AND T_MANIFEST_RELATION.NEXT_HAIKI_KBN_CD = TME2.HAIKI_KBN_CD ");

                        #endregion

                        #endregion

                        #region WHERE

                        sql.Append(" WHERE TME.DELETE_FLG = 0 ");
                        sql.Append(" AND TME.FIRST_MANIFEST_KBN = 0 ");
                        if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                        {
                            sql.AppendFormat(" AND (UPN1.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat("   OR UPN2.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat("   OR UPN3.UPN_GYOUSHA_CD = '{0}' ) ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                        }
                        if (!string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
                        {
                            sql.AppendFormat(" AND TME.SBN_GYOUSHA_CD = '{0}' ", this.form.cantxt_SyobunJyutakuNameCd.Text);
                        }

                        //処分事業場
                        if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
                        {
                            //産廃（積替） 
                            sql.AppendFormat(" AND (");
                            sql.AppendFormat(" (TME.HAIKI_KBN_CD <> 3 AND UPN1.UPN_SAKI_GENBA_CD = '{0}') ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                            sql.AppendFormat(" OR (TME.HAIKI_KBN_CD = 3 ");
                            sql.AppendFormat(" AND TMU_SEARCH.UPN_SAKI_GENBA_CD = '{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                            sql.AppendFormat(" ) ");
                            sql.AppendFormat(" ) ");
                        }

                        // 廃棄物区分
                        string WhereHaikiKbn = string.Empty;
                        if (this.header.HaikiKbn_1.Checked)
                        {
                            WhereHaikiKbn = "1";
                        }
                        if (this.header.HaikiKbn_2.Checked)
                        {
                            if (!string.IsNullOrEmpty(WhereHaikiKbn))
                            {
                                WhereHaikiKbn += " , 3";
                            }
                            else
                            {
                                WhereHaikiKbn = "3";
                            }
                        }
                        if (this.header.HaikiKbn_3.Checked)
                        {
                            if (!string.IsNullOrEmpty(WhereHaikiKbn))
                            {
                                WhereHaikiKbn += " , 2";
                            }
                            else
                            {
                                WhereHaikiKbn = "2";
                            }
                        }
                        sql.Append(" AND ( ");
                        sql.AppendFormat(" TME.HAIKI_KBN_CD IN ({0}) ", WhereHaikiKbn);
                        sql.Append(" ) ");

                        if (!string.IsNullOrEmpty(this.form.KOUFU_DATE_KBN.Text))
                        {
                            //年月日（開始）
                            if (KOUFU_DATE_FROM != "" && this.form.KOUFU_DATE_KBN.Text != "2")
                            {
                                sql.Append(" AND ( ");

                                if (this.form.KOUFU_DATE_KBN.Text == "1")
                                {
                                    // 交付年月日
                                    sql.AppendFormat("  TME.KOUFU_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "3")
                                {
                                    // 処分終了日
                                    sql.AppendFormat(" TMD.SBN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "4")
                                {
                                    // 最終処分終了日
                                    sql.AppendFormat(" TMD.LAST_SBN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                }

                                sql.Append(" ) ");
                            }
                            //年月日（終了）
                            if (KOUFU_DATE_TO != "" && this.form.KOUFU_DATE_KBN.Text != "2")
                            {
                                sql.Append(" AND ( ");

                                if (this.form.KOUFU_DATE_KBN.Text == "1")
                                {
                                    // 交付年月日
                                    sql.AppendFormat("  TME.KOUFU_DATE <= '{0}' ", KOUFU_DATE_TO);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "3")
                                {
                                    // 処分終了日
                                    sql.AppendFormat(" TMD.SBN_END_DATE <= '{0}' ", KOUFU_DATE_TO);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "4")
                                {
                                    // 最終処分終了日
                                    sql.AppendFormat(" TMD.LAST_SBN_END_DATE <= '{0}' ", KOUFU_DATE_TO);
                                }

                                sql.Append(" ) ");
                            }

                            // 運搬終了日
                            if (this.form.KOUFU_DATE_KBN.Text == "2")
                            {
                                if (KOUFU_DATE_FROM != "" && KOUFU_DATE_TO != "")
                                {
                                    sql.Append(" AND ( ");
                                    if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                                    {
                                        sql.AppendFormat(" ((UPN1.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat("  AND  (   UPN1.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN1.UPN_END_DATE <= '{0}' )) ", KOUFU_DATE_TO);
                                        sql.AppendFormat("   OR (UPN2.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND (   UPN2.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN2.UPN_END_DATE <= '{0}' )) ", KOUFU_DATE_TO);
                                        sql.AppendFormat("   OR (UPN3.UPN_GYOUSHA_CD = '{0}' ) ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND (   UPN3.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN3.UPN_END_DATE <= '{0}' )) ", KOUFU_DATE_TO);
                                    }
                                    else
                                    {
                                        sql.AppendFormat(" (  (   UPN1.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN1.UPN_END_DATE <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (   UPN2.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN2.UPN_END_DATE <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (   UPN3.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN3.UPN_END_DATE <= '{0}' )) ", KOUFU_DATE_TO);
                                    }
                                    sql.Append(" ) ");
                                }
                                else if (KOUFU_DATE_FROM != "")
                                {
                                    sql.Append(" AND ( ");
                                    sql.AppendFormat(" ( UPN1.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" OR UPN2.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" OR UPN3.UPN_END_DATE >= '{0}' ) ", KOUFU_DATE_FROM);
                                    sql.Append(" ) ");
                                }
                                else if (KOUFU_DATE_TO != "")
                                {
                                    sql.Append(" AND ( ");
                                    sql.AppendFormat(" ( UPN1.UPN_END_DATE <= '{0}' ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" OR UPN2.UPN_END_DATE <= '{0}' ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" OR UPN3.UPN_END_DATE <= '{0}' ) ", KOUFU_DATE_TO);
                                    sql.Append(" ) ");
                                }
                            }
                        }
                        // 自社運搬
                        if (!string.IsNullOrEmpty(this.form.JISHA_UNPAN_KBN.Text))
                        {
                            if (this.form.JISHA_UNPAN_KBN.Text == "1")
                            {
                                sql.Append(" AND UPN_JISHA_GYOUSHA1.JISHA_KBN = 1 ");
                                sql.Append(" AND HST_GYOUSHA.JISHA_KBN = 1 ");
                            }
                            else if (this.form.JISHA_UNPAN_KBN.Text == "2")
                            {
                                sql.Append(" AND ( ISNULL(UPN_JISHA_GYOUSHA1.JISHA_KBN, 0) <> 1 ");
                                sql.Append("   OR ISNULL(HST_GYOUSHA.JISHA_KBN, 0) <> 1) ");
                            }
                        }

                        // 処分方法
                        if (!string.IsNullOrEmpty(this.form.SHOBUN_HOUHOU_CD.Text))
                        {
                            sql.AppendFormat(" AND TMD.SBN_HOUHOU_CD = '{0}' ", this.form.SHOBUN_HOUHOU_CD.Text);
                        }
                        else if (this.form.SHOBUN_HOUHOU_MI.Checked)
                        {
                            sql.Append(" AND (TMD.SBN_HOUHOU_CD IS NULL OR TMD.SBN_HOUHOU_CD = '') ");
                        }

                        // 報告書分類
                        if (!string.IsNullOrEmpty(this.form.cantxt_HokokushoBunrui.Text))
                        {
                            sql.Append("  AND ( CASE TME.HAIKI_KBN_CD WHEN 1 THEN MHS1.HOUKOKUSHO_BUNRUI_CD ");
                            sql.Append("   WHEN 2 THEN MHS2.HOUKOKUSHO_BUNRUI_CD ");
                            sql.AppendFormat("   WHEN 3 THEN MHS3.HOUKOKUSHO_BUNRUI_CD ELSE '' END ) = '{0}' ", this.form.cantxt_HokokushoBunrui.Text);
                        }

                        // 排出事業者
                        if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text))
                        {
                            sql.AppendFormat(" AND TME.HST_GYOUSHA_CD = '{0}' ", this.form.cantxt_HaisyutuGyousyaCd.Text);

                            if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuJigyoubaName.Text))
                            {
                                sql.AppendFormat(" AND TME.HST_GENBA_CD = '{0}' ", this.form.cantxt_HaisyutuJigyoubaName.Text);
                            }
                        }

                        #endregion

                        #endregion
                    }

                    if (this.header.MANI_KBN.Text == "3")
                    {
                        sql.Append(" UNION ALL ");
                    }

                    if (this.header.MANI_KBN.Text == "2" || this.header.MANI_KBN.Text == "3")
                    {
                        #region 二次マニ

                        #region SELECT

                        sql.Append(" SELECT ");
                        // マニ
                        sql.Append("   TME.SYSTEM_ID ");                                                                      // システムID
                        sql.Append(" , TME.SEQ ");                                                                            // 枝番
                        sql.Append(" , NULL AS LATEST_SEQ ");                                                                 // 最終枝番（紙マニは必ず空）
                        sql.Append(" , NULL AS KANRI_ID ");                                                                   // 管理ID（紙マニは必ず空）
                        sql.Append(" , TME.HAIKI_KBN_CD ");                                                                   // 廃棄区分CD
                        sql.Append(" , TME.KYOTEN_CD ");                                                                      // 拠点
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 1 THEN MHS1.HOUKOKUSHO_BUNRUI_CD ");
                        sql.Append("   WHEN 2 THEN MHS2.HOUKOKUSHO_BUNRUI_CD ");
                        sql.Append("   WHEN 3 THEN MHS3.HOUKOKUSHO_BUNRUI_CD ELSE '' END AS HOUKOKUSHO_BUNRUI_CD ");          // 報告書分類ＣＤ
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 1 THEN MHB1.HOUKOKUSHO_BUNRUI_NAME_RYAKU ");
                        sql.Append("   WHEN 2 THEN MHB2.HOUKOKUSHO_BUNRUI_NAME_RYAKU ");
                        sql.Append("   WHEN 3 THEN MHB3.HOUKOKUSHO_BUNRUI_NAME_RYAKU ELSE '' END AS HOUKOKUSHO_BUNRUI_NAME ");// 報告書分類名
                        sql.Append(" , TMD.HAIKI_NAME_CD AS HAIKI_NAME_CD ");                                                 // 廃棄物名称CD
                        sql.Append(" , MHN.HAIKI_NAME_RYAKU AS HAIKI_NAME ");                                                 // 廃棄物名称
                        sql.Append(" , TMD.HAIKI_SUU AS HAIKI_SUU ");                                                         // マニフェスト数量
                        sql.Append(" , TMD.HAIKI_UNIT_CD AS HAIKI_UNIT_CD ");                                                 // 単位CD1
                        sql.Append(" , TMD_MU.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME ");                                          // 単位名1
                        sql.Append(" , TMD.KANSAN_SUU AS KANSAN_SUU ");                                                       // 運搬委託量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD2 ");                            // 単位CD2
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME2 ");                                      // 単位名2
                        sql.Append(" , TMD.KANSAN_SUU AS SBN_SUU ");                                                          // 処分受託量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD3 ");                            // 単位CD3
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME3 ");                                      // 単位名3
                        sql.Append(" , TME.FIRST_MANIFEST_KBN AS FIRST_MANIFEST_KBN ");                                       // 一次マニ区分
                        sql.Append(" , TME.HST_GYOUSHA_CD AS HST_GYOUSHA_CD ");                                               // 排出事業者CD
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GYOUSHA_NAME, ''),1, 40)) + ");
                        sql.Append("   SUBSTRING(ISNULL(TME.HST_GYOUSHA_NAME, ''),41, 40)) AS HST_GYOUSHA_NAME  ");           // 排出事業者名
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GYOUSHA_ADDRESS, ''),1, 48)) + ");
                        sql.Append("   SUBSTRING(ISNULL(TME.HST_GYOUSHA_ADDRESS, ''),49, 40)) AS HST_GYOUSHA_ADDRESS  ");     // 排出事業者住所
                        sql.Append(" , HST_GYOUSHA.CHIIKI_CD AS HST_GYOUSHA_CHIIKI_CD ");                                     // 排出事業者地域CD
                        sql.Append(" , HST_GYOUSHA_CHIIKI.CHIIKI_NAME_RYAKU AS HST_GYOUSHA_CHIIKI_NAME ");                    // 排出事業者地域名
                        sql.Append(" , HST_GYOUSHA.GYOUSHU_CD AS HST_GYOUSHA_GYOUSHU_CD ");                                   // 排出事業者業種CD
                        sql.Append(" , HST_GYOUSHA_GYOUSHU.GYOUSHU_NAME_RYAKU AS HST_GYOUSHA_GYOUSHU_NAME ");                 // 排出事業者業種名
                        sql.Append(" , TME.HST_GENBA_CD AS HST_GENBA_CD ");                                                   // 排出事業場CD
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GENBA_NAME, ''),1, 40)) + ");
                        sql.Append("   SUBSTRING(ISNULL(TME.HST_GENBA_NAME, ''),41, 40)) AS HST_GENBA_NAME  ");               // 排出事業場名
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GENBA_ADDRESS, ''),1, 48)) + ");
                        sql.Append("   SUBSTRING(ISNULL(TME.HST_GENBA_ADDRESS, ''),49, 40)) AS HST_GENBA_ADDRESS  ");         // 排出事業場住所
                        sql.Append(" , HST_GENBA.CHIIKI_CD AS HST_GENBA_CHIIKI_CD ");                                         // 排出事業場地域CD
                        sql.Append(" , HST_GENBA_CHIIKI.CHIIKI_NAME_RYAKU AS HST_GENBA_CHIIKI_NAME ");                        // 排出事業場地域名
                        sql.Append(" , HST_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS HST_UPN_CHIIKI_CD ");                  // 排出事業場運搬報告地域CD
                        sql.Append(" , HST_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU AS HST_UPN_CHIIKI_NAME ");                      // 排出事業場運搬報告地域名
                        sql.Append(" , HST_GENBA.GYOUSHU_CD AS HST_GENBA_GYOUSHU_CD ");                                       // 排出事業場業種CD
                        sql.Append(" , HST_GENBA_GYOUSHU.GYOUSHU_NAME_RYAKU AS HST_GENBA_GYOUSHU_NAME ");                     // 排出事業場業種名
                        sql.Append(" , TMD.HAIKI_SHURUI_CD AS HAIKI_SHURUI_CD ");                                             // 廃棄物種類CD1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 1 THEN MHS1.HAIKI_SHURUI_NAME_RYAKU ");
                        sql.Append("   WHEN 2 THEN MHS2.HAIKI_SHURUI_NAME_RYAKU ");
                        sql.Append("   WHEN 3 THEN MHS3.HAIKI_SHURUI_NAME_RYAKU ELSE '' END AS HAIKI_SHURUI_NAME ");          // 廃棄物種類名1
                        sql.Append(" , TME.MANIFEST_ID AS MANIFEST_ID ");                                                     // 交付番号1
                        sql.Append(" , TME.KOUFU_DATE AS KOUFU_DATE ");                                                       // 交付年月日1

                        // 収集運搬
                        sql.Append(" , UPN1.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_1 ");                                           // 運搬受託者CD1_1
                        sql.Append(" , UPN1.UPN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME1_1 ");                                       // 運搬受託者名1_1
                        sql.Append(" , UPN1.UPN_END_DATE AS UPN_END_DATE1_1 ");                                               // 運搬終了年月日1_1
                        sql.Append(" , UPN2.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_2 ");                                           // 運搬受託者CD1_2
                        sql.Append(" , UPN2.UPN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME1_2 ");                                       // 運搬受託者名1_2
                        sql.Append(" , UPN2.UPN_END_DATE AS UPN_END_DATE1_2 ");                                               // 運搬終了年月日1_2
                        sql.Append(" , UPN3.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_3 ");                                           // 運搬受託者CD1_3
                        sql.Append(" , UPN3.UPN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME1_3 ");                                       // 運搬受託者名1_3
                        sql.Append(" , UPN3.UPN_END_DATE AS UPN_END_DATE1_3 ");                                               // 運搬終了年月日1_3
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD1_4 ");                                                          // 運搬受託者CD1_4
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME1_4 ");                                                        // 運搬受託者名1_4
                        sql.Append(" , NULL AS UPN_END_DATE1_4 ");                                                            // 運搬終了年月日1_4
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD1_5 ");                                                          // 運搬受託者CD1_5
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME1_5 ");                                                        // 運搬受託者名1_5
                        sql.Append(" , NULL AS UPN_END_DATE1_5 ");                                                            // 運搬終了年月日1_5

                        // 積替保管情報
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN1.UPN_SAKI_GYOUSHA_CD ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_1 ");                                   // 積替保管業者CD1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GYOUSHA1.GYOUSHA_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GYOUSHA_NAME END AS TMH_GYOUSHA_NAME1_1 ");                               // 積替保管業者名1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN1.UPN_SAKI_GENBA_CD ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GENBA_CD END AS TMH_GENBA_CD1_1 ");                                       // 積替保管現場CD1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN1.UPN_SAKI_GENBA_NAME ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GENBA_NAME END AS TMH_GENBA_NAME1_1 ");                                   // 積替保管現場名1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN1.UPN_SAKI_GENBA_ADDRESS ELSE '' END  ");
                        sql.Append("   ELSE TME.TMH_GENBA_ADDRESS END AS TMH_GENBA_ADDRESS1_1 ");                             // 積替保管場所住所1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GENBA1.CHIIKI_CD ELSE '' END  ");
                        sql.Append("   ELSE TMH_GENBA.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_1 ");                             // 積替保管場所地域CD1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE TMH_GENBA_CHIIKI.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_1 ");            // 積替保管場所地域名1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END  ");
                        sql.Append("   ELSE TMH_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_1 ");      // 積替保管場所運搬報告先地域CD1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 2 THEN UPN_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE TMH_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_1 ");          // 積替保管場所運搬報告先地域名1_1
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN2.UPN_SAKI_GYOUSHA_CD ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GYOUSHA_CD1_2 ");                                                   // 積替保管業者CD1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GYOUSHA2.GYOUSHA_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GYOUSHA_NAME1_2 ");                                                 // 積替保管業者名1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN2.UPN_SAKI_GENBA_CD ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_CD1_2 ");                                                     // 積替保管現場CD1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN2.UPN_SAKI_GENBA_NAME ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_NAME1_2 ");                                                   // 積替保管現場名1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN2.UPN_SAKI_GENBA_ADDRESS ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_ADDRESS1_2 ");                                                // 積替保管場所住所1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GENBA2.CHIIKI_CD ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_CHIIKI_CD1_2 ");                                              // 積替保管場所地域CD1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_GENBA_CHIIKI_NAME1_2 ");                                            // 積替保管場所地域名1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_UPN_CHIIKI_CD1_2 ");                                                // 積替保管場所運搬報告先地域CD1_2
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 2 THEN UPN_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU ELSE '' END  ");
                        sql.Append("   ELSE '' END AS TMH_UPN_CHIIKI_NAME1_2 ");                                              // 積替保管場所運搬報告先地域名1_2
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD1_3 ");                                                          // 積替保管業者CD1_3
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME1_3 ");                                                        // 積替保管業者名1_3
                        sql.Append(" , NULL AS TMH_GENBA_CD1_3 ");                                                            // 積替保管現場CD1_3
                        sql.Append(" , NULL AS TMH_GENBA_NAME1_3 ");                                                          // 積替保管現場名1_3
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS1_3 ");                                                       // 積替保管場所住所1_3
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD1_3 ");                                                     // 積替保管場所地域CD1_3
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME1_3 ");                                                   // 積替保管場所地域名1_3
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD1_3 ");                                                       // 積替保管場所運搬報告先地域CD1_3
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME1_3 ");                                                     // 積替保管場所運搬報告先地域名1_3
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD1_4 ");                                                          // 積替保管業者CD1_4
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME1_4 ");                                                        // 積替保管業者名1_4
                        sql.Append(" , NULL AS TMH_GENBA_CD1_4 ");                                                            // 積替保管現場CD1_4
                        sql.Append(" , NULL AS TMH_GENBA_NAME1_4 ");                                                          // 積替保管現場名1_4
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS1_4 ");                                                       // 積替保管場所住所1_4
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD1_4 ");                                                     // 積替保管場所地域CD1_4
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME1_4 ");                                                   // 積替保管場所地域名1_4
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD1_4 ");                                                       // 積替保管場所運搬報告先地域CD1_4
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME1_4 ");                                                     // 積替保管場所運搬報告先地域名1_4

                        // 処分受託者情報
                        sql.Append(" , TME.SBN_GYOUSHA_CD AS SBN_GYOUSHA_CD ");                                               // 処分業者CD
                        sql.Append(" , TME.SBN_GYOUSHA_NAME AS SBN_GYOUSHA_NAME ");                                           // 処分業者名
                        sql.Append(" , TME.SBN_GYOUSHA_ADDRESS AS SBN_GYOUSHA_ADDRESS ");                                     // 処分業者住所
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN1.UPN_SAKI_GENBA_CD ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN2.UPN_SAKI_GENBA_CD ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN3.UPN_SAKI_GENBA_CD ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_CD END AS SBN_GENBA_CD ");                                    // 処分事業場CD
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN1.UPN_SAKI_GENBA_NAME ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN2.UPN_SAKI_GENBA_NAME ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN3.UPN_SAKI_GENBA_NAME ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_NAME END AS SBN_GENBA_NAME ");                                // 処分事業場名称
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN1.UPN_SAKI_GENBA_ADDRESS ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN2.UPN_SAKI_GENBA_ADDRESS ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN3.UPN_SAKI_GENBA_ADDRESS ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_ADDRESS END AS SBN_GENBA_ADDRESS ");                          // 処分事業場住所
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN_GENBA1.CHIIKI_CD ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN_GENBA2.CHIIKI_CD ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN_GENBA3.CHIIKI_CD ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE SBN_GENBA.CHIIKI_CD END AS SBN_GENBA_CHIIKI_CD ");                                // 処分事業場地域CD
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN_GENBA_CHIIKI3.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE SBN_GENBA_CHIIKI.CHIIKI_NAME_RYAKU END AS SBN_GENBA_CHIIKI_NAME ");               // 処分事業場地域名
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE SBN_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS SBN_UPN_CHIIKI_CD ");         // 処分事業場運搬報告地域CD
                        sql.Append(" , CASE TME.HAIKI_KBN_CD WHEN 3 THEN ");
                        sql.Append("     CASE WHEN ISNULL(UPN1.UPN_SAKI_KBN, 0) != 0 AND UPN1.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("     UPN_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU ELSE ");
                        sql.Append("       CASE WHEN ISNULL(UPN2.UPN_SAKI_KBN, 0) != 0 AND UPN2.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("       UPN_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU ELSE ");
                        sql.Append("         CASE WHEN ISNULL(UPN3.UPN_SAKI_KBN, 0) != 0 AND UPN3.UPN_SAKI_KBN = 1 THEN ");
                        sql.Append("         UPN_GENBA_UPN_CHIIKI3.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("       END");
                        sql.Append("     END");
                        sql.Append("   ELSE SBN_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU END AS SBN_UPN_CHIIKI_NAME ");             // 処分事業場運搬報告地域名

                        // 明細
                        sql.Append(" , TMD.SBN_HOUHOU_CD AS DETAIL_SBN_HOUHOU_CD ");                                          // 処分方法CD
                        sql.Append(" , DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU AS DETAIL_SBN_HOUHOU_NAME ");               // 処分方法名
                        sql.Append(" , TMD.SBN_END_DATE AS DETAIL_SBN_END_DATE ");                                            // 処分終了年月日1
                        sql.Append(" , TMD.LAST_SBN_END_DATE AS DETAIL_LAST_SBN_END_DATE ");                                  // 最終処分終了年月日1
                        sql.Append(" , TMD.DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID ");                                           // 明細システムID

                        sql.Append(" , NULL AS MANIFEST_ID2 ");                                                               // 交付番号2
                        sql.Append(" , NULL AS KOUFU_DATE2 ");                                                                // 交付年月日2
                        // 収集運搬
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_1 ");                                                          // 運搬受託者CD2_1
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_1 ");                                                        // 運搬受託者名2_1
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_2 ");                                                          // 運搬受託者CD2_2
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_2 ");                                                        // 運搬受託者名2_2
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_3 ");                                                          // 運搬受託者CD2_3
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_3 ");                                                        // 運搬受託者名2_3
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_4 ");                                                          // 運搬受託者CD2_4
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_4 ");                                                        // 運搬受託者名2_4
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_5 ");                                                          // 運搬受託者CD2_5
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_5 ");                                                        // 運搬受託者名2_5
                        // 積替保管情報
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD2_1 ");                                                          // 積替保管業者CD2_1
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME2_1 ");                                                        // 積替保管業者名2_1
                        sql.Append(" , NULL AS TMH_GENBA_CD2_1 ");                                                            // 積替保管現場CD2_1
                        sql.Append(" , NULL AS TMH_GENBA_NAME2_1 ");                                                          // 積替保管現場名2_1
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS2_1 ");                                                       // 積替保管場所住所2_1
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD2_1 ");                                                     // 積替保管場所地域CD2_1
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME2_1 ");                                                   // 積替保管場所地域名2_1
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD2_1 ");                                                       // 積替保管場所運搬報告先地域CD2_1
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME2_1 ");                                                     // 積替保管場所運搬報告先地域名2_1
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD2_2 ");                                                          // 積替保管業者CD2_2
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME2_2 ");                                                        // 積替保管業者名2_2
                        sql.Append(" , NULL AS TMH_GENBA_CD2_2 ");                                                            // 積替保管現場CD2_2
                        sql.Append(" , NULL AS TMH_GENBA_NAME2_2 ");                                                          // 積替保管現場名2_2
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS2_2 ");                                                       // 積替保管場所住所2_2
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD2_2 ");                                                     // 積替保管場所地域CD2_2
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME2_2 ");                                                   // 積替保管場所地域名2_2
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD2_2 ");                                                       // 積替保管場所運搬報告先地域CD2_2
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME2_2 ");                                                     // 積替保管場所運搬報告先地域名2_2
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD2_3 ");                                                          // 積替保管業者CD2_3
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME2_3 ");                                                        // 積替保管業者名2_3
                        sql.Append(" , NULL AS TMH_GENBA_CD2_3 ");                                                            // 積替保管現場CD2_3
                        sql.Append(" , NULL AS TMH_GENBA_NAME2_3 ");                                                          // 積替保管現場名2_3
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS2_3 ");                                                       // 積替保管場所住所2_3
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD2_3 ");                                                     // 積替保管場所地域CD2_3
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME2_3 ");                                                   // 積替保管場所地域名2_3
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD2_3 ");                                                       // 積替保管場所運搬報告先地域CD2_3
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME2_3 ");                                                     // 積替保管場所運搬報告先地域名2_3
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD2_4 ");                                                          // 積替保管業者CD2_4
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME2_4 ");                                                        // 積替保管業者名2_4
                        sql.Append(" , NULL AS TMH_GENBA_CD2_4 ");                                                            // 積替保管現場CD2_4
                        sql.Append(" , NULL AS TMH_GENBA_NAME2_4 ");                                                          // 積替保管現場名2_4
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS2_4 ");                                                       // 積替保管場所住所2_4
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD2_4 ");                                                     // 積替保管場所地域CD2_4
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME2_4 ");                                                   // 積替保管場所地域名2_4
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD2_4 ");                                                       // 積替保管場所運搬報告先地域CD2_4
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME2_4 ");                                                     // 積替保管場所運搬報告先地域名2_4
                        // 明細
                        sql.Append(" , NULL AS HAIKI_SHURUI_CD2 ");                                                           // 廃棄物種類CD2
                        sql.Append(" , NULL AS HAIKI_SHURUI_NAME2 ");                                                         // 廃棄物種類名2
                        sql.Append(" , NULL AS HIKIWATASHI ");                                                                // 引渡量
                        sql.Append(" , NULL AS SYS_UNIT_CD2_2 ");                                                             // 単位CD（引渡量）
                        sql.Append(" , NULL AS SYS_UNIT_NAME2_2 ");                                                           // 単位名（引渡量）
                        sql.Append(" , NULL AS ITAKU_SBN_CD ");                                                               // 委託処分方法CD
                        sql.Append(" , NULL AS ITAKU_SBN_NAME ");                                                             // 委託処分方法

                        #endregion

                        #region FROM

                        // マニ入力
                        sql.Append(" FROM T_MANIFEST_ENTRY TME ");

                        // 明細
                        sql.Append(" LEFT JOIN T_MANIFEST_DETAIL TMD ");
                        sql.Append(" ON TME.SYSTEM_ID = TMD.SYSTEM_ID AND TME.SEQ = TMD.SEQ ");
                        // 廃棄種類
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI MHS1 ");
                        sql.Append(" ON TMD.HAIKI_SHURUI_CD = MHS1.HAIKI_SHURUI_CD AND MHS1.HAIKI_KBN_CD = 1 AND MHS1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI MHS2 ");
                        sql.Append(" ON TMD.HAIKI_SHURUI_CD = MHS2.HAIKI_SHURUI_CD AND MHS2.HAIKI_KBN_CD = 2 AND MHS2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI MHS3 ");
                        sql.Append(" ON TMD.HAIKI_SHURUI_CD = MHS3.HAIKI_SHURUI_CD AND MHS3.HAIKI_KBN_CD = 3 AND MHS3.DELETE_FLG = 0 ");
                        // 報告書分類
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI MHB1 ");
                        sql.Append(" ON MHS1.HOUKOKUSHO_BUNRUI_CD = MHB1.HOUKOKUSHO_BUNRUI_CD AND MHB1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI MHB2 ");
                        sql.Append(" ON MHS2.HOUKOKUSHO_BUNRUI_CD = MHB2.HOUKOKUSHO_BUNRUI_CD AND MHB2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI MHB3 ");
                        sql.Append(" ON MHS3.HOUKOKUSHO_BUNRUI_CD = MHB3.HOUKOKUSHO_BUNRUI_CD AND MHB3.DELETE_FLG = 0 ");
                        // 廃棄名称
                        sql.Append(" LEFT JOIN M_HAIKI_NAME MHN ");
                        sql.Append(" ON TMD.HAIKI_NAME_CD = MHN.HAIKI_NAME_CD AND MHN.DELETE_FLG = 0 ");
                        // 単位
                        sql.Append(" LEFT JOIN M_UNIT TMD_MU ");
                        sql.Append(" ON TMD.HAIKI_UNIT_CD = TMD_MU.UNIT_CD AND TMD_MU.DELETE_FLG = 0 ");
                        // 排出事業者
                        sql.Append(" LEFT JOIN M_GYOUSHA HST_GYOUSHA ");
                        sql.Append(" ON TME.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD AND HST_GYOUSHA.DELETE_FLG = 0 ");
                        // 排出事業者地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GYOUSHA_CHIIKI ");
                        sql.Append(" ON HST_GYOUSHA.CHIIKI_CD = HST_GYOUSHA_CHIIKI.CHIIKI_CD AND HST_GYOUSHA_CHIIKI.DELETE_FLG = 0 ");
                        // 排出事業者業種
                        sql.Append(" LEFT JOIN M_GYOUSHU HST_GYOUSHA_GYOUSHU ");
                        sql.Append(" ON HST_GYOUSHA.GYOUSHU_CD = HST_GYOUSHA_GYOUSHU.GYOUSHU_CD AND HST_GYOUSHA_GYOUSHU.DELETE_FLG = 0 ");
                        // 排出事業場
                        sql.Append(" LEFT JOIN M_GENBA HST_GENBA ");
                        sql.Append(" ON TME.HST_GYOUSHA_CD = HST_GENBA.GYOUSHA_CD AND TME.HST_GENBA_CD = HST_GENBA.GENBA_CD AND HST_GENBA.DELETE_FLG = 0 ");
                        // 排出事業場地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GENBA_CHIIKI ");
                        sql.Append(" ON HST_GENBA.CHIIKI_CD = HST_GENBA_CHIIKI.CHIIKI_CD AND HST_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        // 排出事業場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON HST_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = HST_GENBA_UPN_CHIIKI.CHIIKI_CD AND HST_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        // 排出事業場業種
                        sql.Append(" LEFT JOIN M_GYOUSHU HST_GENBA_GYOUSHU ");
                        sql.Append(" ON HST_GENBA.GYOUSHU_CD = HST_GENBA_GYOUSHU.GYOUSHU_CD AND HST_GENBA_GYOUSHU.DELETE_FLG = 0 ");
                        // 運搬受託者
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN UPN1 ");
                        sql.Append(" ON TME.SYSTEM_ID = UPN1.SYSTEM_ID AND TME.SEQ = UPN1.SEQ ");
                        sql.Append(" AND UPN1.UPN_ROUTE_NO = 1 ");
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN UPN2 ");
                        sql.Append(" ON TME.SYSTEM_ID = UPN2.SYSTEM_ID AND TME.SEQ = UPN2.SEQ ");
                        sql.Append(" AND UPN2.UPN_ROUTE_NO = 2 ");
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN UPN3 ");
                        sql.Append(" ON TME.SYSTEM_ID = UPN3.SYSTEM_ID AND TME.SEQ = UPN3.SEQ ");
                        sql.Append(" AND UPN3.UPN_ROUTE_NO = 3 ");
                        sql.Append(" LEFT JOIN M_GYOUSHA UPN_GYOUSHA1 ");
                        sql.Append(" ON UPN1.UPN_SAKI_GYOUSHA_CD = UPN_GYOUSHA1.GYOUSHA_CD AND UPN_GYOUSHA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GYOUSHA UPN_GYOUSHA2 ");
                        sql.Append(" ON UPN2.UPN_SAKI_GYOUSHA_CD = UPN_GYOUSHA2.GYOUSHA_CD AND UPN_GYOUSHA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GYOUSHA UPN_JISHA_GYOUSHA1 ");
                        sql.Append(" ON UPN1.UPN_GYOUSHA_CD = UPN_JISHA_GYOUSHA1.GYOUSHA_CD AND UPN_JISHA_GYOUSHA1.DELETE_FLG = 0 ");
                        // 積替保管現場
                        sql.Append(" LEFT JOIN M_GENBA TMH_GENBA ");
                        sql.Append(" ON TME.TMH_GYOUSHA_CD = TMH_GENBA.GYOUSHA_CD AND TME.TMH_GENBA_CD = TMH_GENBA.GENBA_CD AND TMH_GENBA.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UPN_GENBA1 ");
                        sql.Append(" ON UPN1.UPN_SAKI_GYOUSHA_CD = UPN_GENBA1.GYOUSHA_CD AND UPN1.UPN_SAKI_GENBA_CD = UPN_GENBA1.GENBA_CD AND UPN_GENBA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UPN_GENBA2 ");
                        sql.Append(" ON UPN2.UPN_SAKI_GYOUSHA_CD = UPN_GENBA2.GYOUSHA_CD AND UPN2.UPN_SAKI_GENBA_CD = UPN_GENBA2.GENBA_CD AND UPN_GENBA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UPN_GENBA3 ");
                        sql.Append(" ON UPN3.UPN_SAKI_GYOUSHA_CD = UPN_GENBA3.GYOUSHA_CD AND UPN3.UPN_SAKI_GENBA_CD = UPN_GENBA3.GENBA_CD AND UPN_GENBA3.DELETE_FLG = 0 ");
                        // 積替保管現場地域
                        sql.Append(" LEFT JOIN M_CHIIKI TMH_GENBA_CHIIKI ");
                        sql.Append(" ON TMH_GENBA.CHIIKI_CD = TMH_GENBA_CHIIKI.CHIIKI_CD AND TMH_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_CHIIKI1 ");
                        sql.Append(" ON UPN_GENBA1.CHIIKI_CD = UPN_GENBA_CHIIKI1.CHIIKI_CD AND UPN_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_CHIIKI2 ");
                        sql.Append(" ON UPN_GENBA2.CHIIKI_CD = UPN_GENBA_CHIIKI2.CHIIKI_CD AND UPN_GENBA_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_CHIIKI3 ");
                        sql.Append(" ON UPN_GENBA3.CHIIKI_CD = UPN_GENBA_CHIIKI3.CHIIKI_CD AND UPN_GENBA_CHIIKI3.DELETE_FLG = 0 ");
                        // 積替保管現場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI TMH_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON TMH_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = TMH_GENBA_UPN_CHIIKI.CHIIKI_CD AND TMH_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON UPN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_GENBA_UPN_CHIIKI1.CHIIKI_CD AND UPN_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_UPN_CHIIKI2 ");
                        sql.Append(" ON UPN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_GENBA_UPN_CHIIKI2.CHIIKI_CD AND UPN_GENBA_UPN_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UPN_GENBA_UPN_CHIIKI3 ");
                        sql.Append(" ON UPN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_GENBA_UPN_CHIIKI3.CHIIKI_CD AND UPN_GENBA_UPN_CHIIKI3.DELETE_FLG = 0 ");
                        // 処分事業場
                        sql.Append(" LEFT JOIN M_GENBA SBN_GENBA ");
                        sql.Append(" ON TME.SBN_GYOUSHA_CD = SBN_GENBA.GYOUSHA_CD AND UPN1.UPN_SAKI_GENBA_CD = SBN_GENBA.GENBA_CD AND SBN_GENBA.DELETE_FLG = 0 ");
                        // 処分事業場地域
                        sql.Append(" LEFT JOIN M_CHIIKI SBN_GENBA_CHIIKI ");
                        sql.Append(" ON SBN_GENBA.CHIIKI_CD = SBN_GENBA_CHIIKI.CHIIKI_CD AND SBN_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        // 処分事業場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI SBN_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON SBN_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = SBN_GENBA_UPN_CHIIKI.CHIIKI_CD AND SBN_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        // 処分方法
                        sql.Append(" LEFT JOIN M_SHOBUN_HOUHOU DETAIL_SBN_HOUHOU ");
                        sql.Append(" ON TMD.SBN_HOUHOU_CD = DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_CD AND DETAIL_SBN_HOUHOU.DELETE_FLG = 0 ");
                        // 検索用運搬受託者､処分事業場
                        sql.Append("            LEFT JOIN T_MANIFEST_UPN TMU_SEARCH ");
                        sql.Append("                   ON TME.SYSTEM_ID = TMU_SEARCH.SYSTEM_ID ");
                        sql.Append("                  AND TME.SEQ = TMU_SEARCH.SEQ ");
                        sql.Append("                  AND TMU_SEARCH.UPN_ROUTE_NO = (SELECT MAX(T_MANIFEST_UPN.UPN_ROUTE_NO) FROM T_MANIFEST_UPN ");
                        sql.Append("                  WHERE SYSTEM_ID = TME.SYSTEM_ID AND SEQ = TME.SEQ AND UPN_SAKI_KBN = 1) ");
                        sql.Append("                  AND TMU_SEARCH.UPN_SAKI_KBN = 1 ");

                        #endregion

                        #region WHERE

                        sql.Append(" WHERE TME.DELETE_FLG = 0 ");
                        sql.Append(" AND TME.FIRST_MANIFEST_KBN = 1 ");
                        if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                        {
                            sql.AppendFormat(" AND (UPN1.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat("   OR UPN2.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat("   OR UPN3.UPN_GYOUSHA_CD = '{0}' ) ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                        }
                        if (!string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
                        {
                            sql.AppendFormat(" AND TME.SBN_GYOUSHA_CD = '{0}' ", this.form.cantxt_SyobunJyutakuNameCd.Text);
                        }

                        //処分事業場
                        if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
                        {
                            //産廃（積替） 
                            sql.AppendFormat(" AND (");
                            sql.AppendFormat(" (TME.HAIKI_KBN_CD <> 3 AND UPN1.UPN_SAKI_GENBA_CD = '{0}') ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                            sql.AppendFormat(" OR (TME.HAIKI_KBN_CD = 3 ");
                            sql.AppendFormat(" AND TMU_SEARCH.UPN_SAKI_GENBA_CD = '{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                            sql.AppendFormat(" ) ");
                            sql.AppendFormat(" ) ");
                        }

                        string WhereHaikiKbn = string.Empty;
                        if (this.header.HaikiKbn_1.Checked)
                        {
                            WhereHaikiKbn = "1";
                        }
                        if (this.header.HaikiKbn_2.Checked)
                        {
                            if (!string.IsNullOrEmpty(WhereHaikiKbn))
                            {
                                WhereHaikiKbn += " , 3";
                            }
                            else
                            {
                                WhereHaikiKbn = "3";
                            }
                        }
                        if (this.header.HaikiKbn_3.Checked)
                        {
                            if (!string.IsNullOrEmpty(WhereHaikiKbn))
                            {
                                WhereHaikiKbn += " , 2";
                            }
                            else
                            {
                                WhereHaikiKbn = "2";
                            }
                        }

                        sql.Append(" AND ( ");
                        sql.AppendFormat(" TME.HAIKI_KBN_CD IN ({0}) ", WhereHaikiKbn);
                        sql.Append(" ) ");

                        if (!string.IsNullOrEmpty(this.form.KOUFU_DATE_KBN.Text))
                        {
                            //年月日（開始）
                            if (KOUFU_DATE_FROM != "" && this.form.KOUFU_DATE_KBN.Text != "2")
                            {
                                sql.Append(" AND ( ");

                                if (this.form.KOUFU_DATE_KBN.Text == "1")
                                {
                                    // 交付年月日
                                    sql.AppendFormat("  TME.KOUFU_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "3")
                                {
                                    // 処分終了日
                                    sql.AppendFormat(" TMD.SBN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "4")
                                {
                                    // 最終処分終了日
                                    sql.AppendFormat(" TMD.LAST_SBN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                }

                                sql.Append(" ) ");
                            }
                            //年月日（終了）
                            if (KOUFU_DATE_TO != "" && this.form.KOUFU_DATE_KBN.Text != "2")
                            {
                                sql.Append(" AND ( ");

                                if (this.form.KOUFU_DATE_KBN.Text == "1")
                                {
                                    // 交付年月日
                                    sql.AppendFormat("  TME.KOUFU_DATE <= '{0}' ", KOUFU_DATE_TO);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "3")
                                {
                                    // 処分終了日
                                    sql.AppendFormat(" TMD.SBN_END_DATE <= '{0}' ", KOUFU_DATE_TO);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "4")
                                {
                                    // 最終処分終了日
                                    sql.AppendFormat(" TMD.LAST_SBN_END_DATE <= '{0}' ", KOUFU_DATE_TO);
                                }

                                sql.Append(" ) ");
                            }

                            // 運搬終了日
                            if (this.form.KOUFU_DATE_KBN.Text == "2")
                            {
                                if (KOUFU_DATE_FROM != "" && KOUFU_DATE_TO != "")
                                {
                                    sql.Append(" AND ( ");
                                    if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                                    {
                                        sql.AppendFormat(" ((UPN1.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat("  AND  (   UPN1.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN1.UPN_END_DATE <= '{0}' )) ", KOUFU_DATE_TO);
                                        sql.AppendFormat("   OR (UPN2.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND (   UPN2.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN2.UPN_END_DATE <= '{0}' )) ", KOUFU_DATE_TO);
                                        sql.AppendFormat("   OR (UPN3.UPN_GYOUSHA_CD = '{0}' ) ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND (   UPN3.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN3.UPN_END_DATE <= '{0}' )) ", KOUFU_DATE_TO);
                                    }
                                    else
                                    {
                                        sql.AppendFormat(" (  (   UPN1.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN1.UPN_END_DATE <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (   UPN2.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN2.UPN_END_DATE <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (   UPN3.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND UPN3.UPN_END_DATE <= '{0}' )) ", KOUFU_DATE_TO);
                                    }
                                    sql.Append(" ) ");
                                }
                                else if (KOUFU_DATE_FROM != "")
                                {
                                    sql.Append(" AND ( ");
                                    sql.AppendFormat(" ( UPN1.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" OR UPN2.UPN_END_DATE >= '{0}' ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" OR UPN3.UPN_END_DATE >= '{0}' ) ", KOUFU_DATE_FROM);
                                    sql.Append(" ) ");
                                }
                                else if (KOUFU_DATE_TO != "")
                                {
                                    sql.Append(" AND ( ");
                                    sql.AppendFormat(" ( UPN1.UPN_END_DATE <= '{0}' ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" OR UPN2.UPN_END_DATE <= '{0}' ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" OR UPN3.UPN_END_DATE <= '{0}' ) ", KOUFU_DATE_TO);
                                    sql.Append(" ) ");
                                }
                            }
                        }

                        // 自社運搬
                        if (!string.IsNullOrEmpty(this.form.JISHA_UNPAN_KBN.Text))
                        {
                            if (this.form.JISHA_UNPAN_KBN.Text == "1")
                            {
                                sql.Append(" AND UPN_JISHA_GYOUSHA1.JISHA_KBN = 1 ");
                                sql.Append(" AND HST_GYOUSHA.JISHA_KBN = 1 ");
                            }
                            else if (this.form.JISHA_UNPAN_KBN.Text == "2")
                            {
                                sql.Append(" AND (ISNULL(UPN_JISHA_GYOUSHA1.JISHA_KBN, 0) <> 1 ");
                                sql.Append("   OR ISNULL(HST_GYOUSHA.JISHA_KBN, 0) <> 1) ");
                            }
                        }

                        // 処分方法
                        if (!string.IsNullOrEmpty(this.form.SHOBUN_HOUHOU_CD.Text))
                        {
                            sql.AppendFormat(" AND TMD.SBN_HOUHOU_CD = '{0}' ", this.form.SHOBUN_HOUHOU_CD.Text);
                        }
                        else if (this.form.SHOBUN_HOUHOU_MI.Checked)
                        {
                            sql.Append(" AND (TMD.SBN_HOUHOU_CD IS NULL OR TMD.SBN_HOUHOU_CD = '') ");
                        }

                        // 報告書分類
                        if (!string.IsNullOrEmpty(this.form.cantxt_HokokushoBunrui.Text))
                        {
                            sql.Append("  AND ( CASE TME.HAIKI_KBN_CD WHEN 1 THEN MHS1.HOUKOKUSHO_BUNRUI_CD ");
                            sql.Append("   WHEN 2 THEN MHS2.HOUKOKUSHO_BUNRUI_CD ");
                            sql.AppendFormat("   WHEN 3 THEN MHS3.HOUKOKUSHO_BUNRUI_CD ELSE '' END ) = '{0}' ", this.form.cantxt_HokokushoBunrui.Text);
                        }

                        // 排出事業者
                        if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text))
                        {
                            sql.AppendFormat(" AND TME.HST_GYOUSHA_CD = '{0}' ", this.form.cantxt_HaisyutuGyousyaCd.Text);

                            if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuJigyoubaName.Text))
                            {
                                sql.AppendFormat(" AND TME.HST_GENBA_CD = '{0}' ", this.form.cantxt_HaisyutuJigyoubaName.Text);
                            }
                        }

                        #endregion

                        #endregion
                    }

                    #endregion
                }

                if ((this.header.HaikiKbn_1.Checked || this.header.HaikiKbn_2.Checked || this.header.HaikiKbn_3.Checked)
                     && this.header.HaikiKbn_4.Checked)
                {
                    sql.Append(" UNION ALL ");
                }

                if (this.header.HaikiKbn_4.Checked)
                {
                    #region 電マニ

                    if (this.header.MANI_KBN.Text == "1" || this.header.MANI_KBN.Text == "3")
                    {
                        #region 一次マニ

                        #region SELECT

                        sql.Append(" SELECT ");
                        // マニ
                        sql.Append("   R18EX.SYSTEM_ID ");                                                                    // システムID
                        sql.Append(" , R18EX.SEQ ");                                                                          // 枝番
                        sql.Append(" , DMT.LATEST_SEQ AS LATEST_SEQ ");                                                       // 最終枝番（紙マニは必ず空）
                        sql.Append(" , DMT.KANRI_ID AS KANRI_ID ");                                                           // 管理ID（紙マニは必ず空）
                        sql.Append(" , 4 AS HAIKI_KBN_CD ");                                                                  // 廃棄区分CD
                        sql.Append(" , NULL AS KYOTEN_CD ");                                                                  // 拠点
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18_MDHS1.HOUKOKUSHO_BUNRUI_CD ");
                        sql.Append("   ELSE R18MIX_MDHS1.HOUKOKUSHO_BUNRUI_CD END AS HOUKOKUSHO_BUNRUI_CD ");                 // 報告書分類ＣＤ
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18_MHB1.HOUKOKUSHO_BUNRUI_NAME_RYAKU ");
                        sql.Append("   ELSE R18MIX_MHB1.HOUKOKUSHO_BUNRUI_NAME_RYAKU END AS HOUKOKUSHO_BUNRUI_NAME ");        // 報告書分類名
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18EX.HAIKI_NAME_CD ");
                        sql.Append("   ELSE R18MIX.HAIKI_NAME_CD END AS HAIKI_NAME_CD ");                                     // 廃棄物名称CD
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18.HAIKI_NAME ");
                        sql.Append("   ELSE R18MIX_MDHN.HAIKI_NAME END AS HAIKI_NAME ");                                      // 廃棄物名称
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18.HAIKI_SUU ");
                        sql.Append("   ELSE R18MIX.HAIKI_SUU END AS HAIKI_SUU");                                              // マニフェスト数量
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18.HAIKI_UNIT_CODE ");
                        sql.Append("   ELSE R18MIX.HAIKI_UNIT_CD END AS HAIKI_UNIT_CD ");                                     // 単位CD1
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18_MU.UNIT_NAME_RYAKU ");
                        sql.Append("   ELSE R18MIX_MU.UNIT_NAME_RYAKU END AS HAIKI_UNIT_NAME ");                              // 単位名1
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18EX.KANSAN_SUU ");
                        sql.Append("   ELSE R18MIX.KANSAN_SUU END AS KANSAN_SUU ");                                           // 運搬委託量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD2 ");                            // 単位CD2
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME2 ");                                      // 単位名2
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18EX.KANSAN_SUU ");
                        sql.Append("   ELSE R18MIX.KANSAN_SUU END AS SBN_SUU ");                                              // 処分受託量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD3 ");                            // 単位CD3
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME3 ");                                      // 単位名3
                        sql.Append(" , CASE WHEN R18.FIRST_MANIFEST_FLAG IS NULL THEN 0 ");
                        sql.Append("        WHEN R18.FIRST_MANIFEST_FLAG = '' THEN 0 ");
                        sql.Append("        WHEN ISNULL(FIRST_HST_GYOUSHA.JISHA_KBN, 0) = 0 THEN 0 ");
                        sql.Append("   ELSE 1 END AS FIRST_MANIFEST_KBN ");                                                   // 一次マニ区分
                        sql.Append(" , R18EX.HST_GYOUSHA_CD AS HST_GYOUSHA_CD ");                                             // 排出事業者CD
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(R18.HST_SHA_NAME, ''),1, 40)) + ");
                        sql.Append("   SUBSTRING(ISNULL(R18.HST_SHA_NAME, ''),41, 40)) AS HST_GYOUSHA_NAME ");                // 排出事業者名
                        sql.Append(" , ISNULL(R18.HST_SHA_ADDRESS1, '') + ISNULL(R18.HST_SHA_ADDRESS2, '') ");
                        sql.Append(" + ISNULL(R18.HST_SHA_ADDRESS3, '') + ISNULL(R18.HST_SHA_ADDRESS4, '') ");
                        sql.Append("   AS HST_GYOUSHA_ADDRESS ");                                                             // 排出事業者住所
                        sql.Append(" , HST_GYOUSHA1.CHIIKI_CD AS HST_GYOUSHA_CHIIKI_CD ");                                    // 排出事業者地域CD
                        sql.Append(" , HST_GYOUSHA_CHIIKI1.CHIIKI_NAME_RYAKU AS HST_GYOUSHA_CHIIKI_NAME ");                   // 排出事業者地域名
                        sql.Append(" , HST_GYOUSHA1.GYOUSHU_CD AS HST_GYOUSHA_GYOUSHU_CD ");                                  // 排出事業者業種CD
                        sql.Append(" , HST_GYOUSHA_GYOUSHU1.GYOUSHU_NAME_RYAKU AS HST_GYOUSHA_GYOUSHU_NAME ");                // 排出事業者業種名
                        sql.Append(" , R18EX.HST_GENBA_CD AS HST_GENBA_CD ");                                                 // 排出事業場CD
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(R18.HST_JOU_NAME, ''),1, 40)) + ");
                        sql.Append("   SUBSTRING(ISNULL(R18.HST_JOU_NAME, ''),41, 40)) AS HST_GENBA_NAME ");                  // 排出事業場名
                        sql.Append(" , ISNULL(R18.HST_JOU_ADDRESS1, '') + ISNULL(R18.HST_JOU_ADDRESS2, '') ");
                        sql.Append(" + ISNULL(R18.HST_JOU_ADDRESS3, '') + ISNULL(R18.HST_JOU_ADDRESS4, '') ");
                        sql.Append("   AS HST_GENBA_ADDRESS ");                                                               // 排出事業場住所
                        sql.Append(" , HST_GENBA1.CHIIKI_CD AS HST_GENBA_CHIIKI_CD ");                                        // 排出事業場地域CD
                        sql.Append(" , HST_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU AS HST_GENBA_CHIIKI_NAME ");                       // 排出事業場地域名
                        sql.Append(" , HST_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS HST_UPN_CHIIKI_CD ");                 // 排出事業場運搬報告地域CD
                        sql.Append(" , HST_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU AS HST_UPN_CHIIKI_NAME ");                     // 排出事業場運搬報告地域名
                        sql.Append(" , HST_GENBA1.GYOUSHU_CD AS HST_GENBA_GYOUSHU_CD ");                                      // 排出事業場業種CD
                        sql.Append(" , HST_GENBA_GYOUSHU1.GYOUSHU_NAME_RYAKU AS HST_GENBA_GYOUSHU_NAME ");                    // 排出事業場業種名
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN ");
                        sql.Append("     ISNULL(R18.HAIKI_DAI_CODE, '') + ISNULL(R18.HAIKI_CHU_CODE, '')+ ISNULL(R18.HAIKI_SHO_CODE, '') + ISNULL(R18.HAIKI_SAI_CODE, '') ");
                        sql.Append("   ELSE R18MIX.HAIKI_SHURUI_CD END AS HAIKI_SHURUI_CD ");                                 // 廃棄物種類CD1
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18.HAIKI_SHURUI ");
                        sql.Append("   ELSE R18MIX_MDHS1.HAIKI_SHURUI_NAME END AS HAIKI_SHURUI_NAME ");                       // 廃棄物種類名1
                        sql.Append(" , R18.MANIFEST_ID AS MANIFEST_ID ");                                                     // 交付番号1
                        sql.Append(" , CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ");
                        sql.Append("   ELSE NULL END AS KOUFU_DATE ");                                                        // 交付年月日1

                        // 収集運搬
                        sql.Append(" , UPN_EX1.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_1 ");                                        // 運搬受託者CD1_1
                        sql.Append(" , UPN1.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_1 ");                                           // 運搬受託者名1_1
                        sql.Append(" , CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_1 ");                                                   // 運搬終了年月日1_1
                        sql.Append(" , UPN_EX2.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_2 ");                                        // 運搬受託者CD1_2
                        sql.Append(" , UPN2.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_2 ");                                           // 運搬受託者名1_2
                        sql.Append(" , CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_2 ");                                                   // 運搬終了年月日1_2
                        sql.Append(" , UPN_EX3.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_3 ");                                        // 運搬受託者CD1_3
                        sql.Append(" , UPN3.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_3 ");                                           // 運搬受託者名1_3
                        sql.Append(" , CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_3 ");                                                   // 運搬終了年月日1_3
                        sql.Append(" , UPN_EX4.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_4 ");                                        // 運搬受託者CD1_4
                        sql.Append(" , UPN4.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_4 ");                                           // 運搬受託者名1_4
                        sql.Append(" , CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_4 ");                                                   // 運搬終了年月日1_4
                        sql.Append(" , UPN_EX5.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_5 ");                                        // 運搬受託者CD1_5
                        sql.Append(" , UPN5.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_5 ");                                           // 運搬受託者名1_5
                        sql.Append(" , CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_5 ");                                                   // 運搬終了年月日1_5

                        // 積替保管情報
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX1.UPNSAKI_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_1 ");                           // 積替保管業者CD1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN1.UPNSAKI_NAME END AS TMH_GYOUSHA_NAME1_1 ");                                  // 積替保管業者名1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX1.UPNSAKI_GENBA_CD END AS TMH_GENBA_CD1_1 ");                               // 積替保管現場CD1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN1.UPNSAKI_JOU_NAME END AS TMH_GENBA_NAME1_1 ");                                // 積替保管現場名1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE ISNULL(UPN1.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN1.UPNSAKI_JOU_ADDRESS2, '') + ");
                        sql.Append("        ISNULL(UPN1.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN1.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   END AS TMH_GENBA_ADDRESS1_1 ");                                                        // 積替保管場所住所1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA1.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_1 ");                          // 積替保管場所地域CD1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_1 ");         // 積替保管場所地域名1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_1 ");   // 積替保管場所運搬報告先地域CD1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_1 ");       // 積替保管場所運搬報告先地域名1_1
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX2.UPNSAKI_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_2 ");                           // 積替保管業者CD1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN2.UPNSAKI_NAME END AS TMH_GYOUSHA_NAME1_2 ");                                  // 積替保管業者名1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX2.UPNSAKI_GENBA_CD END AS TMH_GENBA_CD1_2 ");                               // 積替保管現場CD1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN2.UPNSAKI_JOU_NAME END AS TMH_GENBA_NAME1_2 ");                                // 積替保管現場名1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE ISNULL(UPN2.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN2.UPNSAKI_JOU_ADDRESS2, '') + ");
                        sql.Append("        ISNULL(UPN2.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN2.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   END AS TMH_GENBA_ADDRESS1_2 ");                                                        // 積替保管場所住所1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA2.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_2 ");                          // 積替保管場所地域CD1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_2 ");         // 積替保管場所地域名1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_2 ");   // 積替保管場所運搬報告先地域CD1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_2 ");       // 積替保管場所運搬報告先地域名1_2
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX3.UPNSAKI_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_3 ");                           // 積替保管業者CD1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN3.UPNSAKI_NAME END AS TMH_GYOUSHA_NAME1_3 ");                                  // 積替保管業者名1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX3.UPNSAKI_GENBA_CD END AS TMH_GENBA_CD1_3 ");                               // 積替保管現場CD1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN3.UPNSAKI_JOU_NAME END AS TMH_GENBA_NAME1_3 ");                                // 積替保管現場名1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE ISNULL(UPN3.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN3.UPNSAKI_JOU_ADDRESS2, '') + ");
                        sql.Append("        ISNULL(UPN3.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN3.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   END AS TMH_GENBA_ADDRESS1_3 ");                                                        // 積替保管場所住所1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA3.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_3 ");                          // 積替保管場所地域CD1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_CHIIKI3.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_3 ");         // 積替保管場所地域名1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_3 ");   // 積替保管場所運搬報告先地域CD1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_UPN_CHIIKI3.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_3 ");       // 積替保管場所運搬報告先地域名1_3
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX4.UPNSAKI_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_4 ");                           // 積替保管業者CD1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN4.UPNSAKI_NAME END AS TMH_GYOUSHA_NAME1_4 ");                                  // 積替保管業者名1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX4.UPNSAKI_GENBA_CD END AS TMH_GENBA_CD1_4 ");                               // 積替保管現場CD1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN4.UPNSAKI_JOU_NAME END AS TMH_GENBA_NAME1_4 ");                                // 積替保管現場名1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE ISNULL(UPN4.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN4.UPNSAKI_JOU_ADDRESS2, '') + ");
                        sql.Append("        ISNULL(UPN4.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN4.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   END AS TMH_GENBA_ADDRESS1_4 ");                                                        // 積替保管場所住所1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA4.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_4 ");                          // 積替保管場所地域CD1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_CHIIKI4.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_4 ");         // 積替保管場所地域名1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA4.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_4 ");   // 積替保管場所運搬報告先地域CD1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_UPN_CHIIKI4.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_4 ");           // 積替保管場所運搬報告先地域名1_4

                        // 処分受託者情報
                        sql.Append(" , R18EX.SBN_GYOUSHA_CD AS SBN_GYOUSHA_CD ");                                             // 処分業者CD
                        sql.Append(" , R18.SBN_SHA_NAME AS SBN_GYOUSHA_NAME ");                                               // 処分業者名
                        sql.Append(" , ISNULL(R18.SBN_SHA_ADDRESS1, '') + ISNULL(R18.SBN_SHA_ADDRESS2, '') ");
                        sql.Append(" + ISNULL(R18.SBN_SHA_ADDRESS3, '') + ISNULL(R18.SBN_SHA_ADDRESS4, '') ");
                        sql.Append("   AS SBN_GYOUSHA_ADDRESS ");                                                             // 処分業者住所
                        sql.Append(" , R18EX.SBN_GENBA_CD AS SBN_GENBA_CD ");                                                 // 処分事業場CD
                        sql.Append(" , DT_R19_LAST.UPNSAKI_JOU_NAME AS SBN_GENBA_NAME ");                                     // 処分事業場名称
                        sql.Append(" , ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS2, '') ");
                        sql.Append(" + ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   AS SBN_GENBA_ADDRESS ");                                                               // 処分事業場住所
                        sql.Append(" , SBN_GENBA.CHIIKI_CD AS SBN_GENBA_CHIIKI_CD ");                                         // 処分事業場地域CD
                        sql.Append(" , SBN_GENBA_CHIIKI.CHIIKI_NAME_RYAKU AS SBN_GENBA_CHIIKI_NAME ");                        // 処分事業場地域名
                        sql.Append(" , SBN_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS SBN_UPN_CHIIKI_CD ");                  // 処分事業場運搬報告地域CD
                        sql.Append(" , SBN_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU AS SBN_UPN_CHIIKI_NAME ");                      // 処分事業場運搬報告地域名

                        // 明細
                        sql.Append(" , CASE WHEN R18MIX.SYSTEM_ID IS NOT NULL THEN R18MIX.SBN_HOUHOU_CD ");
                        sql.Append("   ELSE");
                        sql.Append("       CASE WHEN ISNULL(R18EX.SBN_HOUHOU_CD,'') = '' THEN R18.SBN_WAY_CODE");
                        sql.Append("       ELSE R18EX.SBN_HOUHOU_CD END");
                        sql.Append("   END DETAIL_SBN_HOUHOU_CD ");                                        // 処分方法CD
                        sql.Append(" , CASE WHEN R18MIX.SYSTEM_ID IS NOT NULL THEN MIX_SBN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU ");
                        sql.Append("   ELSE");
                        sql.Append("       CASE WHEN ISNULL(R18EX.SBN_HOUHOU_CD,'') = '' THEN R18.SBN_WAY_NAME");
                        sql.Append("       ELSE DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU END");
                        sql.Append("   END DETAIL_SBN_HOUHOU_NAME ");               // 処分方法名
                        sql.Append(" , CASE WHEN ISDATE(R18.SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.SBN_END_DATE) END ");
                        sql.Append("   AS DETAIL_SBN_END_DATE ");                                                             // 処分終了年月日1
                        sql.Append(" , CASE WHEN ISDATE(R18.LAST_SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.LAST_SBN_END_DATE) END ");
                        sql.Append("   AS DETAIL_LAST_SBN_END_DATE ");                                                        // 最終処分終了年月日1
                        sql.Append(" , NULL AS DETAIL_SYSTEM_ID ");                                                           // 明細システムID
                        // 二次マニ
                        sql.Append(" , NIJI.MANIFEST_ID AS MANIFEST_ID2 ");                                                   // 交付番号2
                        sql.Append(" , NIJI.KOUFU_DATE AS KOUFU_DATE2 ");                                                     // 交付年月日2
                        // 収集運搬
                        sql.Append(" , NIJI.UPN_GYOUSHA_CD1 AS UPN_GYOUSHA_CD2_1 ");                                          // 運搬受託者CD2_1
                        sql.Append(" , NIJI.UPN_GYOUSHA_NAME1 AS UPN_GYOUSHA_NAME2_1 ");                                      // 運搬受託者名2_1
                        sql.Append(" , NIJI.UPN_GYOUSHA_CD2 AS UPN_GYOUSHA_CD2_2 ");                                          // 運搬受託者CD2_2
                        sql.Append(" , NIJI.UPN_GYOUSHA_NAME2 AS UPN_GYOUSHA_NAME2_2 ");                                      // 運搬受託者名2_2
                        sql.Append(" , NIJI.UPN_GYOUSHA_CD3 AS UPN_GYOUSHA_CD2_3 ");                                          // 運搬受託者CD2_3
                        sql.Append(" , NIJI.UPN_GYOUSHA_NAME3 AS UPN_GYOUSHA_NAME2_3 ");                                      // 運搬受託者名2_3
                        sql.Append(" , NIJI.UPN_GYOUSHA_CD4 AS UPN_GYOUSHA_CD2_4 ");                                          // 運搬受託者CD2_4
                        sql.Append(" , NIJI.UPN_GYOUSHA_NAME4 AS UPN_GYOUSHA_NAME2_4 ");                                      // 運搬受託者名2_4
                        sql.Append(" , NIJI.UPN_GYOUSHA_CD5 AS UPN_GYOUSHA_CD2_5 ");                                          // 運搬受託者CD2_5
                        sql.Append(" , NIJI.UPN_GYOUSHA_NAME5 AS UPN_GYOUSHA_NAME2_5 ");                                      // 運搬受託者名2_5
                        // 積替保管情報
                        sql.Append(" , NIJI.TMH_GYOUSHA_CD2_1 AS TMH_GYOUSHA_CD2_1 ");                                        // 積替保管業者CD2_1
                        sql.Append(" , NIJI.TMH_GYOUSHA_NAME2_1 AS TMH_GYOUSHA_NAME2_1 ");                                    // 積替保管業者名2_1
                        sql.Append(" , NIJI.TMH_GENBA_CD2_1 AS TMH_GENBA_CD2_1 ");                                            // 積替保管現場CD2_1
                        sql.Append(" , NIJI.TMH_GENBA_NAME2_1 AS TMH_GENBA_NAME2_1 ");                                        // 積替保管現場名2_1
                        sql.Append(" , NIJI.TMH_GENBA_ADDRESS2_1 AS TMH_GENBA_ADDRESS2_1 ");                                  // 積替保管場所住所2_1
                        sql.Append(" , NIJI.TMH_GENBA_CHIIKI_CD2_1 AS TMH_GENBA_CHIIKI_CD2_1 ");                              // 積替保管場所地域CD2_1
                        sql.Append(" , NIJI.TMH_GENBA_CHIIKI_NAME2_1 AS TMH_GENBA_CHIIKI_NAME2_1 ");                          // 積替保管場所地域名2_1
                        sql.Append(" , NIJI.TMH_UPN_CHIIKI_CD2_1 AS TMH_UPN_CHIIKI_CD2_1 ");                                  // 積替保管場所運搬報告先地域CD2_1
                        sql.Append(" , NIJI.TMH_UPN_CHIIKI_NAME2_1 AS TMH_UPN_CHIIKI_NAME2_1 ");                              // 積替保管場所運搬報告先地域名2_1
                        sql.Append(" , NIJI.TMH_GYOUSHA_CD2_2 AS TMH_GYOUSHA_CD2_2 ");                                        // 積替保管業者CD2_2
                        sql.Append(" , NIJI.TMH_GYOUSHA_NAME2_2 AS TMH_GYOUSHA_NAME2_2 ");                                    // 積替保管業者名2_2
                        sql.Append(" , NIJI.TMH_GENBA_CD2_2 AS TMH_GENBA_CD2_2 ");                                            // 積替保管現場CD2_2
                        sql.Append(" , NIJI.TMH_GENBA_NAME2_2 AS TMH_GENBA_NAME2_2 ");                                        // 積替保管現場名2_2
                        sql.Append(" , NIJI.TMH_GENBA_ADDRESS2_2 AS TMH_GENBA_ADDRESS2_2 ");                                  // 積替保管場所住所2_2
                        sql.Append(" , NIJI.TMH_GENBA_CHIIKI_CD2_2 AS TMH_GENBA_CHIIKI_CD2_2 ");                              // 積替保管場所地域CD2_2
                        sql.Append(" , NIJI.TMH_GENBA_CHIIKI_NAME2_2 AS TMH_GENBA_CHIIKI_NAME2_2 ");                          // 積替保管場所地域名2_2
                        sql.Append(" , NIJI.TMH_UPN_CHIIKI_CD2_2 AS TMH_UPN_CHIIKI_CD2_2 ");                                  // 積替保管場所運搬報告先地域CD2_2
                        sql.Append(" , NIJI.TMH_UPN_CHIIKI_NAME2_2 AS TMH_UPN_CHIIKI_NAME2_2 ");                              // 積替保管場所運搬報告先地域名2_2
                        sql.Append(" , NIJI.TMH_GYOUSHA_CD2_3 AS TMH_GYOUSHA_CD2_3 ");                                        // 積替保管業者CD2_3
                        sql.Append(" , NIJI.TMH_GYOUSHA_NAME2_3 AS TMH_GYOUSHA_NAME2_3 ");                                    // 積替保管業者名2_3
                        sql.Append(" , NIJI.TMH_GENBA_CD2_3 AS TMH_GENBA_CD2_3 ");                                            // 積替保管現場CD2_3
                        sql.Append(" , NIJI.TMH_GENBA_NAME2_3 AS TMH_GENBA_NAME2_3 ");                                        // 積替保管現場名2_3
                        sql.Append(" , NIJI.TMH_GENBA_ADDRESS2_3 AS TMH_GENBA_ADDRESS2_3 ");                                  // 積替保管場所住所2_3
                        sql.Append(" , NIJI.TMH_GENBA_CHIIKI_CD2_3 AS TMH_GENBA_CHIIKI_CD2_3 ");                              // 積替保管場所地域CD2_3
                        sql.Append(" , NIJI.TMH_GENBA_CHIIKI_NAME2_3 AS TMH_GENBA_CHIIKI_NAME2_3 ");                          // 積替保管場所地域名2_3
                        sql.Append(" , NIJI.TMH_UPN_CHIIKI_CD2_3 AS TMH_UPN_CHIIKI_CD2_3 ");                                  // 積替保管場所運搬報告先地域CD2_3
                        sql.Append(" , NIJI.TMH_UPN_CHIIKI_NAME2_3 AS TMH_UPN_CHIIKI_NAME2_3 ");                              // 積替保管場所運搬報告先地域名2_3
                        sql.Append(" , NIJI.TMH_GYOUSHA_CD2_4 AS TMH_GYOUSHA_CD2_4 ");                                        // 積替保管業者CD2_4
                        sql.Append(" , NIJI.TMH_GYOUSHA_NAME2_4 AS TMH_GYOUSHA_NAME2_4 ");                                    // 積替保管業者名2_4
                        sql.Append(" , NIJI.TMH_GENBA_CD2_4 AS TMH_GENBA_CD2_4 ");                                            // 積替保管現場CD2_4
                        sql.Append(" , NIJI.TMH_GENBA_NAME2_4 AS TMH_GENBA_NAME2_4 ");                                        // 積替保管現場名2_4
                        sql.Append(" , NIJI.TMH_GENBA_ADDRESS2_4 AS TMH_GENBA_ADDRESS2_4 ");                                  // 積替保管場所住所2_4
                        sql.Append(" , NIJI.TMH_GENBA_CHIIKI_CD2_4 AS TMH_GENBA_CHIIKI_CD2_4 ");                              // 積替保管場所地域CD2_4
                        sql.Append(" , NIJI.TMH_GENBA_CHIIKI_NAME2_4 AS TMH_GENBA_CHIIKI_NAME2_4 ");                          // 積替保管場所地域名2_4
                        sql.Append(" , NIJI.TMH_UPN_CHIIKI_CD2_4 AS TMH_UPN_CHIIKI_CD2_4 ");                                  // 積替保管場所運搬報告先地域CD2_4
                        sql.Append(" , NIJI.TMH_UPN_CHIIKI_NAME2_4 AS TMH_UPN_CHIIKI_NAME2_4 ");                              // 積替保管場所運搬報告先地域名2_4
                        // 明細
                        sql.Append(" , NIJI.HAIKI_SHURUI_CD2 AS HAIKI_SHURUI_CD2 ");                                          // 廃棄物種類CD2
                        sql.Append(" , NIJI.HAIKI_SHURUI_NAME2 AS HAIKI_SHURUI_NAME2 ");                                      // 廃棄物種類名2
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18EX.GENNYOU_SUU ");
                        sql.Append("   ELSE R18MIX.GENNYOU_SUU END AS HIKIWATASHI");                                          // 引渡量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD2_2 ");                          // 単位CD（引渡量）
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME2_2 ");                                    // 単位名（引渡量）
                        sql.Append(" , NIJI.ITAKU_SBN_CD AS ITAKU_SBN_CD ");                                                  // 委託処分方法CD
                        sql.Append(" , NIJI.ITAKU_SBN_NAME AS ITAKU_SBN_NAME ");                                              // 委託処分方法

                        #endregion

                        #region FROM

                        // マニ入力
                        sql.Append(" FROM DT_MF_TOC DMT ");
                        // マニ情報
                        sql.Append(" INNER JOIN DT_R18 R18 ON DMT.KANRI_ID = R18.KANRI_ID AND DMT.LATEST_SEQ = R18.SEQ ");
                        // マニ情報拡張
                        sql.Append(" LEFT JOIN ");
                        sql.Append("( ");
                        sql.Append("SELECT ");
                        sql.Append("  R18EX.SYSTEM_ID ");
                        sql.Append(" ,R18EX.SEQ ");
                        sql.Append(" ,R18EX.KANRI_ID ");
                        sql.Append(" ,R18EX.MANIFEST_ID ");
                        sql.Append(" ,R18EX.HST_GYOUSHA_CD ");
                        sql.Append(" ,R18EX.HST_GENBA_CD ");
                        sql.Append(" ,R18EX.SBN_GYOUSHA_CD ");
                        sql.Append(" ,R18EX.SBN_GENBA_CD ");
                        sql.Append(" ,R18EX.NO_REP_SBN_EDI_MEMBER_ID ");
                        sql.Append(" ,R18EX.SBN_HOUHOU_CD ");
                        sql.Append(" ,R18EX.HOUKOKU_TANTOUSHA_CD ");
                        sql.Append(" ,R18EX.SBN_TANTOUSHA_CD ");
                        sql.Append(" ,R18EX.UPN_TANTOUSHA_CD ");
                        sql.Append(" ,R18EX.SHARYOU_CD ");
                        sql.Append(" ,R18EX.KANSAN_SUU ");
                        sql.Append(" ,CREATE_DATA.CREATE_USER ");
                        sql.Append(" ,R18EX.CREATE_DATE ");
                        sql.Append(" ,CREATE_DATA.CREATE_PC ");
                        sql.Append(" ,R18EX.UPDATE_USER ");
                        sql.Append(" ,R18EX.UPDATE_DATE ");
                        sql.Append(" ,R18EX.UPDATE_PC ");
                        sql.Append(" ,R18EX.DELETE_FLG ");
                        sql.Append(" ,R18EX.TIME_STAMP ");
                        sql.Append(" ,R18EX.HAIKI_NAME_CD ");
                        sql.Append(" ,R18EX.GENNYOU_SUU ");
                        sql.Append("FROM  ");
                        sql.Append("  DT_R18_EX R18EX ");
                        sql.Append(" ,( ");
                        sql.Append("   SELECT ");
                        sql.Append("    R18EX.SYSTEM_ID ");
                        sql.Append("   ,R18EX.SEQ ");
                        sql.Append("   ,R18EX.KANRI_ID ");
                        sql.Append("   ,R18EX.CREATE_USER ");
                        sql.Append("   ,R18EX.CREATE_DATE ");
                        sql.Append("   ,R18EX.CREATE_PC ");
                        sql.Append("  FROM ");
                        sql.Append("   DT_R18_EX R18EX ");
                        sql.Append("   ,(SELECT ");
                        sql.Append("      SYSTEM_ID ");
                        sql.Append("     ,MIN(SEQ) MIN_SEQ ");
                        sql.Append("     FROM ");
                        sql.Append("      DT_R18_EX ");
                        sql.Append("     GROUP BY  ");
                        sql.Append("      SYSTEM_ID ");
                        sql.Append("     ) SEQ_DATA ");
                        sql.Append("  WHERE ");
                        sql.Append("       R18EX.SYSTEM_ID = seq_data.SYSTEM_ID ");
                        sql.Append("   AND R18EX.SEQ = SEQ_DATA.MIN_SEQ ");
                        sql.Append("   ) CREATE_DATA ");
                        sql.Append("WHERE ");
                        sql.Append(" R18EX.SYSTEM_ID = CREATE_DATA.SYSTEM_ID ");
                        sql.Append(") R18EX ");
                        sql.Append(" ON R18.KANRI_ID = R18EX.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = ( SELECT MAX(SYSTEM_ID) FROM DT_R18_EX TMP WHERE R18.KANRI_ID = TMP.KANRI_ID ) ");
                        sql.Append(" AND R18EX.SEQ = ( SELECT MAX(SEQ) FROM DT_R18_EX TMP1 WHERE TMP1.SYSTEM_ID = ( SELECT MAX(SYSTEM_ID) FROM DT_R18_EX TMP2 WHERE R18.KANRI_ID = TMP2.KANRI_ID )  ) ");
                        sql.Append(" LEFT JOIN DT_R18_MIX R18MIX ");
                        sql.Append(" ON R18EX.SYSTEM_ID = R18MIX.SYSTEM_ID ");
                        sql.Append(" AND R18EX.KANRI_ID = R18MIX.KANRI_ID ");
                        sql.Append(" AND R18MIX.DELETE_FLG = 0 ");
                        // 廃棄種類
                        sql.Append(" LEFT JOIN M_DENSHI_HAIKI_SHURUI R18MIX_MDHS1 ");
                        sql.Append(" ON ISNULL(R18MIX.HAIKI_DAI_CODE, 0) + ISNULL(R18MIX.HAIKI_CHU_CODE, 0) + ISNULL(R18MIX.HAIKI_SHO_CODE, 0) = R18MIX_MDHS1.HAIKI_SHURUI_CD ");
                        sql.Append(" AND R18MIX_MDHS1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_DENSHI_HAIKI_SHURUI R18_MDHS1 ");
                        sql.Append(" ON ISNULL(R18.HAIKI_DAI_CODE, 0) + ISNULL(R18.HAIKI_CHU_CODE, 0) + ISNULL(R18.HAIKI_SHO_CODE, 0) = R18_MDHS1.HAIKI_SHURUI_CD ");
                        sql.Append(" AND R18_MDHS1.DELETE_FLG = 0 ");
                        // 報告書分類
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI R18MIX_MHB1 ");
                        sql.Append(" ON R18MIX_MDHS1.HOUKOKUSHO_BUNRUI_CD = R18MIX_MHB1.HOUKOKUSHO_BUNRUI_CD AND R18MIX_MHB1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI R18_MHB1 ");
                        sql.Append(" ON R18_MDHS1.HOUKOKUSHO_BUNRUI_CD = R18_MHB1.HOUKOKUSHO_BUNRUI_CD ");
                        sql.Append(" AND R18_MHB1.DELETE_FLG = 0 ");
                        // 廃棄物名称
                        sql.Append(" LEFT JOIN M_DENSHI_HAIKI_NAME R18MIX_MDHN ");
                        sql.Append(" ON R18MIX.HAIKI_NAME_CD = R18MIX_MDHN.HAIKI_NAME_CD AND R18MIX_MDHN.DELETE_FLG = 0 ");
                        sql.Append(" AND R18.HST_SHA_EDI_MEMBER_ID = R18MIX_MDHN.EDI_MEMBER_ID ");
                        // 単位
                        sql.Append(" LEFT JOIN M_UNIT R18MIX_MU ");
                        sql.Append(" ON R18MIX.HAIKI_UNIT_CD = R18MIX_MU.UNIT_CD AND R18MIX_MU.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_UNIT R18_MU ");
                        sql.Append(" ON R18.HAIKI_UNIT_CODE = R18_MU.UNIT_CD AND R18_MU.DELETE_FLG = 0 ");
                        // 排出事業者
                        sql.Append(" LEFT JOIN M_GYOUSHA HST_GYOUSHA1 ");
                        sql.Append(" ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA1.GYOUSHA_CD AND HST_GYOUSHA1.DELETE_FLG = 0 ");
                        // 排出事業者地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GYOUSHA_CHIIKI1 ");
                        sql.Append(" ON HST_GYOUSHA1.CHIIKI_CD = HST_GYOUSHA_CHIIKI1.CHIIKI_CD AND HST_GYOUSHA_CHIIKI1.DELETE_FLG = 0 ");
                        // 排出事業者業種
                        sql.Append(" LEFT JOIN M_GYOUSHU HST_GYOUSHA_GYOUSHU1 ");
                        sql.Append(" ON HST_GYOUSHA1.GYOUSHU_CD = HST_GYOUSHA_GYOUSHU1.GYOUSHU_CD AND HST_GYOUSHA_GYOUSHU1.DELETE_FLG = 0 ");
                        // 排出事業場
                        sql.Append(" LEFT JOIN M_GENBA HST_GENBA1 ");
                        sql.Append(" ON R18EX.HST_GYOUSHA_CD = HST_GENBA1.GYOUSHA_CD AND R18EX.HST_GENBA_CD = HST_GENBA1.GENBA_CD AND HST_GENBA1.DELETE_FLG = 0 ");
                        // 排出事業場地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GENBA_CHIIKI1 ");
                        sql.Append(" ON HST_GENBA1.CHIIKI_CD = HST_GENBA_CHIIKI1.CHIIKI_CD AND HST_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        // 排出事業場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON HST_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = HST_GENBA_UPN_CHIIKI1.CHIIKI_CD AND HST_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        // 排出事業場業種
                        sql.Append(" LEFT JOIN M_GYOUSHU HST_GENBA_GYOUSHU1 ");
                        sql.Append(" ON HST_GENBA1.GYOUSHU_CD = HST_GENBA_GYOUSHU1.GYOUSHU_CD AND HST_GENBA_GYOUSHU1.DELETE_FLG = 0 ");
                        // 収集運搬1
                        sql.Append(" LEFT JOIN DT_R19 UPN1 ");
                        sql.Append(" ON R18.KANRI_ID = UPN1.KANRI_ID AND R18.SEQ = UPN1.SEQ ");
                        sql.Append(" AND UPN1.UPN_ROUTE_NO = 1 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX1 ");
                        sql.Append(" ON UPN1.KANRI_ID = UPN_EX1.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX1.SYSTEM_ID AND R18EX.SEQ = UPN_EX1.SEQ AND UPN_EX1.UPN_ROUTE_NO = 1 ");
                        sql.Append(" LEFT JOIN M_GYOUSHA UPN_GYOUSHA1 ");
                        sql.Append(" ON UPN_EX1.UPN_GYOUSHA_CD = UPN_GYOUSHA1.GYOUSHA_CD AND UPN_GYOUSHA1.DELETE_FLG = 0 ");
                        // 収集運搬2
                        sql.Append(" LEFT JOIN DT_R19 UPN2 ");
                        sql.Append(" ON R18.KANRI_ID = UPN2.KANRI_ID AND R18.SEQ = UPN2.SEQ ");
                        sql.Append(" AND UPN2.UPN_ROUTE_NO = 2 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX2 ");
                        sql.Append(" ON UPN2.KANRI_ID = UPN_EX2.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX2.SYSTEM_ID AND R18EX.SEQ = UPN_EX2.SEQ AND UPN_EX2.UPN_ROUTE_NO = 2 ");
                        // 収集運搬3
                        sql.Append(" LEFT JOIN DT_R19 UPN3 ");
                        sql.Append(" ON R18.KANRI_ID = UPN3.KANRI_ID AND R18.SEQ = UPN3.SEQ ");
                        sql.Append(" AND UPN3.UPN_ROUTE_NO = 3 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX3 ");
                        sql.Append(" ON UPN3.KANRI_ID = UPN_EX3.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX3.SYSTEM_ID AND R18EX.SEQ = UPN_EX3.SEQ AND UPN_EX3.UPN_ROUTE_NO = 3 ");
                        // 収集運搬4
                        sql.Append(" LEFT JOIN DT_R19 UPN4 ");
                        sql.Append(" ON R18.KANRI_ID = UPN4.KANRI_ID AND R18.SEQ = UPN4.SEQ ");
                        sql.Append(" AND UPN4.UPN_ROUTE_NO = 4 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX4 ");
                        sql.Append(" ON UPN4.KANRI_ID = UPN_EX4.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX4.SYSTEM_ID AND R18EX.SEQ = UPN_EX4.SEQ AND UPN_EX4.UPN_ROUTE_NO = 4 ");
                        // 収集運搬5
                        sql.Append(" LEFT JOIN DT_R19 UPN5 ");
                        sql.Append(" ON R18.KANRI_ID = UPN5.KANRI_ID AND R18.SEQ = UPN5.SEQ ");
                        sql.Append(" AND UPN5.UPN_ROUTE_NO = 5 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX5 ");
                        sql.Append(" ON UPN5.KANRI_ID = UPN_EX5.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX5.SYSTEM_ID AND R18EX.SEQ = UPN_EX5.SEQ AND UPN_EX5.UPN_ROUTE_NO = 5 ");
                        // 積替保管現場
                        sql.Append(" LEFT JOIN M_GENBA UNPAN_GENBA1 ");
                        sql.Append(" ON UPN_EX1.UPNSAKI_GYOUSHA_CD = UNPAN_GENBA1.GYOUSHA_CD ");
                        sql.Append(" AND UPN_EX1.UPNSAKI_GENBA_CD = UNPAN_GENBA1.GENBA_CD AND UNPAN_GENBA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UNPAN_GENBA2 ");
                        sql.Append(" ON UPN_EX2.UPNSAKI_GYOUSHA_CD = UNPAN_GENBA2.GYOUSHA_CD ");
                        sql.Append(" AND UPN_EX2.UPNSAKI_GENBA_CD = UNPAN_GENBA2.GENBA_CD AND UNPAN_GENBA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UNPAN_GENBA3 ");
                        sql.Append(" ON UPN_EX3.UPNSAKI_GYOUSHA_CD = UNPAN_GENBA3.GYOUSHA_CD ");
                        sql.Append(" AND UPN_EX3.UPNSAKI_GENBA_CD = UNPAN_GENBA3.GENBA_CD AND UNPAN_GENBA3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UNPAN_GENBA4 ");
                        sql.Append(" ON UPN_EX4.UPNSAKI_GYOUSHA_CD = UNPAN_GENBA4.GYOUSHA_CD ");
                        sql.Append(" AND UPN_EX4.UPNSAKI_GENBA_CD = UNPAN_GENBA4.GENBA_CD AND UNPAN_GENBA4.DELETE_FLG = 0 ");
                        // 積替保管現場地域
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_CHIIKI1 ");
                        sql.Append(" ON UNPAN_GENBA1.CHIIKI_CD = UNPAN_GENBA_CHIIKI1.CHIIKI_CD AND UNPAN_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_CHIIKI2 ");
                        sql.Append(" ON UNPAN_GENBA2.CHIIKI_CD = UNPAN_GENBA_CHIIKI2.CHIIKI_CD AND UNPAN_GENBA_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_CHIIKI3 ");
                        sql.Append(" ON UNPAN_GENBA3.CHIIKI_CD = UNPAN_GENBA_CHIIKI3.CHIIKI_CD AND UNPAN_GENBA_CHIIKI3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_CHIIKI4 ");
                        sql.Append(" ON UNPAN_GENBA4.CHIIKI_CD = UNPAN_GENBA_CHIIKI4.CHIIKI_CD AND UNPAN_GENBA_CHIIKI4.DELETE_FLG = 0 ");
                        // 積替保管現場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON UNPAN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UNPAN_GENBA_UPN_CHIIKI1.CHIIKI_CD ");
                        sql.Append(" AND UNPAN_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_UPN_CHIIKI2 ");
                        sql.Append(" ON UNPAN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UNPAN_GENBA_UPN_CHIIKI2.CHIIKI_CD ");
                        sql.Append(" AND UNPAN_GENBA_UPN_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_UPN_CHIIKI3 ");
                        sql.Append(" ON UNPAN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UNPAN_GENBA_UPN_CHIIKI3.CHIIKI_CD ");
                        sql.Append(" AND UNPAN_GENBA_UPN_CHIIKI3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_UPN_CHIIKI4 ");
                        sql.Append(" ON UNPAN_GENBA4.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UNPAN_GENBA_UPN_CHIIKI4.CHIIKI_CD ");
                        sql.Append(" AND UNPAN_GENBA_UPN_CHIIKI4.DELETE_FLG = 0 ");
                        // マニ運搬情報（最終区間）
                        sql.Append(" LEFT JOIN DT_R19 DT_R19_LAST");
                        sql.Append(" ON DMT.KANRI_ID = DT_R19_LAST.KANRI_ID AND DMT.LATEST_SEQ = DT_R19_LAST.SEQ ");
                        sql.Append(" AND DT_R19_LAST.UPN_ROUTE_NO = (SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 R19 WHERE DMT.KANRI_ID = R19.KANRI_ID AND DMT.LATEST_SEQ = R19.SEQ) ");
                        // 一次排出事業者
                        sql.Append(" LEFT JOIN M_GYOUSHA FIRST_HST_GYOUSHA ");
                        sql.Append(" ON R18EX.HST_GYOUSHA_CD = FIRST_HST_GYOUSHA.GYOUSHA_CD ");
                        // 処分事業場
                        sql.Append(" LEFT JOIN M_GENBA SBN_GENBA ");
                        sql.Append(" ON R18EX.SBN_GYOUSHA_CD = SBN_GENBA.GYOUSHA_CD AND R18EX.SBN_GENBA_CD = SBN_GENBA.GENBA_CD AND SBN_GENBA.DELETE_FLG = 0 ");
                        // 処分事業場地域
                        sql.Append(" LEFT JOIN M_CHIIKI SBN_GENBA_CHIIKI ");
                        sql.Append(" ON SBN_GENBA.CHIIKI_CD = SBN_GENBA_CHIIKI.CHIIKI_CD AND SBN_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        // 処分事業場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI SBN_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON SBN_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = SBN_GENBA_UPN_CHIIKI.CHIIKI_CD AND SBN_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        // 処分方法
                        sql.Append(" LEFT JOIN M_SHOBUN_HOUHOU DETAIL_SBN_HOUHOU ");
                        sql.Append(" ON R18EX.SBN_HOUHOU_CD = DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_CD AND DETAIL_SBN_HOUHOU.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_SHOBUN_HOUHOU MIX_SBN_HOUHOU ");
                        sql.Append(" ON R18MIX.SBN_HOUHOU_CD = MIX_SBN_HOUHOU.SHOBUN_HOUHOU_CD AND MIX_SBN_HOUHOU.DELETE_FLG = 0 ");

                        #region 紐づく二次マニ

                        // 二次マニ
                        sql.Append(" LEFT JOIN T_MANIFEST_RELATION REL ");
                        sql.Append(" ON (CASE WHEN ISNULL(R18MIX.DETAIL_SYSTEM_ID, 0) = 0 THEN R18EX.SYSTEM_ID ELSE R18MIX.DETAIL_SYSTEM_ID END) = ");
                        sql.Append(" REL.FIRST_SYSTEM_ID ");
                        sql.Append(" AND REL.DELETE_FLG = 0 ");
                        sql.Append(" AND REL.FIRST_HAIKI_KBN_CD = 4 ");
                        sql.Append(" AND REL.REC_SEQ = (SELECT MAX(TMP.REC_SEQ) FROM T_MANIFEST_RELATION TMP ");
                        sql.Append(" WHERE TMP.FIRST_SYSTEM_ID = REL.FIRST_SYSTEM_ID AND TMP.DELETE_FLG = 0 AND TMP.FIRST_HAIKI_KBN_CD = 4 ) ");

                        sql.Append(" LEFT JOIN ( ");
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(" MR.NEXT_SYSTEM_ID AS SYSTEM_ID ");
                        sql.Append(" ,MR.NEXT_HAIKI_KBN_CD AS HAIKI_KBN_CD ");
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN R18EX2.MANIFEST_ID ELSE ME.MANIFEST_ID END) AS MANIFEST_ID ");
                        // 交付年月日2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN ");
                        sql.Append("        CASE WHEN ISDATE(R182.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R182.HIKIWATASHI_DATE) ELSE NULL END ");
                        sql.Append("   ELSE ME.KOUFU_DATE END) AS KOUFU_DATE ");
                        // 運搬受託者CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX1.UPN_GYOUSHA_CD ELSE T_MANIFEST_UPN1.UPN_GYOUSHA_CD END) AS UPN_GYOUSHA_CD1 ");
                        // 運搬受託者名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R191.UPN_SHA_NAME ELSE T_MANIFEST_UPN1.UPN_GYOUSHA_NAME END) AS UPN_GYOUSHA_NAME1 ");
                        // 運搬受託者CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX2.UPN_GYOUSHA_CD ELSE T_MANIFEST_UPN2.UPN_GYOUSHA_CD END) AS UPN_GYOUSHA_CD2 ");
                        // 運搬受託者名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R192.UPN_SHA_NAME ELSE T_MANIFEST_UPN2.UPN_GYOUSHA_NAME END) AS UPN_GYOUSHA_NAME2 ");
                        // 運搬受託者CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX3.UPN_GYOUSHA_CD ELSE T_MANIFEST_UPN3.UPN_GYOUSHA_CD END) AS UPN_GYOUSHA_CD3 ");
                        // 運搬受託者名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R193.UPN_SHA_NAME ELSE T_MANIFEST_UPN3.UPN_GYOUSHA_NAME END) AS UPN_GYOUSHA_NAME3 ");
                        // 運搬受託者CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX4.UPN_GYOUSHA_CD ELSE NULL END) AS UPN_GYOUSHA_CD4 ");
                        // 運搬受託者名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R194.UPN_SHA_NAME ELSE NULL END) AS UPN_GYOUSHA_NAME4 ");
                        // 運搬受託者CD2_5
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R19_EX5.UPN_GYOUSHA_CD ELSE NULL END) AS UPN_GYOUSHA_CD5 ");
                        // 運搬受託者名2_5
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_R195.UPN_SHA_NAME ELSE NULL END) AS UPN_GYOUSHA_NAME5 ");
                        // 積替保管業者CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX1.UPNSAKI_GYOUSHA_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN1.UPN_SAKI_GYOUSHA_CD ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GYOUSHA_CD END END )AS TMH_GYOUSHA_CD2_1 ");
                        // 積替保管業者名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R191.UPNSAKI_NAME END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GYOUSHA1.GYOUSHA_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GYOUSHA_NAME END END )AS TMH_GYOUSHA_NAME2_1 ");
                        // 積替保管現場CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX1.UPNSAKI_GENBA_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN1.UPN_SAKI_GENBA_CD ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GENBA_CD END END )AS TMH_GENBA_CD2_1 ");
                        // 積替保管現場名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R191.UPNSAKI_JOU_NAME END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN1.UPN_SAKI_GENBA_NAME ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GENBA_NAME END END )AS TMH_GENBA_NAME2_1 ");
                        // 積替保管場所住所2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE ISNULL(DT_R191.UPNSAKI_JOU_ADDRESS1, '') + ");
                        sql.Append("        ISNULL(DT_R191.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DT_R191.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R191.UPNSAKI_JOU_ADDRESS4, '') END  ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN1.UPN_SAKI_GENBA_ADDRESS ELSE '' END ");
                        sql.Append("        ELSE ME.TMH_GENBA_ADDRESS END END )AS TMH_GENBA_ADDRESS2_1 ");
                        // 積替保管場所地域CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA1.CHIIKI_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA1.CHIIKI_CD ELSE '' END ");
                        sql.Append("        ELSE TME_TMH_GENBA.CHIIKI_CD END END )AS TMH_GENBA_CHIIKI_CD2_1 ");
                        // 積替保管場所地域名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE TME_TMH_GENBA_CHIIKI.CHIIKI_NAME_RYAKU END END )AS TMH_GENBA_CHIIKI_NAME2_1 ");
                        // 積替保管場所運搬報告先地域CD2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END ");
                        sql.Append("        ELSE TME_TMH_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END END )AS TMH_UPN_CHIIKI_CD2_1 ");
                        // 積替保管場所運搬報告先地域名2_1
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX1.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX1.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN1.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE TME_TMH_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU END END )AS TMH_UPN_CHIIKI_NAME2_1 ");
                        // 積替保管業者CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX2.UPNSAKI_GYOUSHA_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN2.UPN_SAKI_GYOUSHA_CD ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GYOUSHA_CD2_2 ");
                        // 積替保管業者名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R192.UPNSAKI_NAME END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GYOUSHA2.GYOUSHA_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE '' END END ) AS TMH_GYOUSHA_NAME2_2 ");
                        // 積替保管現場CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX2.UPNSAKI_GENBA_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN2.UPN_SAKI_GENBA_CD ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_CD2_2 ");
                        // 積替保管現場名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R192.UPNSAKI_JOU_NAME END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN2.UPN_SAKI_GENBA_NAME ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_NAME2_2 ");
                        // 積替保管場所住所2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE ISNULL(DT_R192.UPNSAKI_JOU_ADDRESS1, '') + ");
                        sql.Append("        ISNULL(DT_R192.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DT_R192.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R192.UPNSAKI_JOU_ADDRESS4, '') END  ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN T_MANIFEST_UPN2.UPN_SAKI_GENBA_ADDRESS ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_ADDRESS2_2 ");
                        // 積替保管場所地域CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA2.CHIIKI_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA2.CHIIKI_CD ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_CHIIKI_CD2_2 ");
                        // 積替保管場所地域名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_GENBA_CHIIKI_NAME2_2 ");
                        // 積替保管場所運搬報告先地域CD2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_UPN_CHIIKI_CD2_2 ");
                        // 積替保管場所運搬報告先地域名2_2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX2.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX2.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 3 THEN CASE WHEN ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) != 0 ");
                        sql.Append("             AND T_MANIFEST_UPN2.UPN_SAKI_KBN = 2 THEN TME_UPNSAKI_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU ELSE '' END ");
                        sql.Append("        ELSE '' END END )AS TMH_UPN_CHIIKI_NAME2_2 ");
                        // 積替保管業者CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX3.UPNSAKI_GYOUSHA_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GYOUSHA_CD2_3 ");
                        // 積替保管業者名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R193.UPNSAKI_NAME END ");
                        sql.Append("   ELSE NULL END ) AS TMH_GYOUSHA_NAME2_3 ");
                        // 積替保管現場CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX3.UPNSAKI_GENBA_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CD2_3 ");
                        // 積替保管現場名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R193.UPNSAKI_JOU_NAME END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_NAME2_3 ");
                        // 積替保管場所住所2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE ISNULL(DT_R193.UPNSAKI_JOU_ADDRESS1, '') + ");
                        sql.Append("        ISNULL(DT_R193.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DT_R193.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R193.UPNSAKI_JOU_ADDRESS4, '') END  ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_ADDRESS2_3 ");
                        // 積替保管場所地域CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA3.CHIIKI_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CHIIKI_CD2_3 ");
                        // 積替保管場所地域名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_CHIIKI3.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CHIIKI_NAME2_3 ");
                        // 積替保管場所運搬報告先地域CD2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_UPN_CHIIKI_CD2_3 ");
                        // 積替保管場所運搬報告先地域名2_3
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX3.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX3.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_UPN_CHIIKI3.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE NULL END )AS TMH_UPN_CHIIKI_NAME2_3 ");
                        // 積替保管業者CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX4.UPNSAKI_GYOUSHA_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GYOUSHA_CD2_4 ");
                        // 積替保管業者名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R194.UPNSAKI_NAME END ");
                        sql.Append("   ELSE NULL END ) AS TMH_GYOUSHA_NAME2_4 ");
                        // 積替保管現場CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R19_EX4.UPNSAKI_GENBA_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CD2_4 ");
                        // 積替保管現場名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_R194.UPNSAKI_JOU_NAME END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_NAME2_4 ");
                        // 積替保管場所住所2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE ISNULL(DT_R194.UPNSAKI_JOU_ADDRESS1, '') + ");
                        sql.Append("        ISNULL(DT_R194.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DT_R194.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R194.UPNSAKI_JOU_ADDRESS4, '') END  ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_ADDRESS2_4 ");
                        // 積替保管場所地域CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA4.CHIIKI_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CHIIKI_CD2_4 ");
                        // 積替保管場所地域名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_CHIIKI4.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE NULL END )AS TMH_GENBA_CHIIKI_NAME2_4 ");
                        // 積替保管場所運搬報告先地域CD2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA4.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END ");
                        sql.Append("   ELSE NULL END )AS TMH_UPN_CHIIKI_CD2_4 ");
                        // 積替保管場所運搬報告先地域名2_4
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN CASE WHEN DT_R19_EX4.UPNSAKI_GYOUSHA_CD = R18EX2.SBN_GYOUSHA_CD ");
                        sql.Append("        AND DT_R19_EX4.UPNSAKI_GENBA_CD = R18EX2.SBN_GENBA_CD THEN NULL ELSE DT_UNPAN_GENBA_UPN_CHIIKI4.CHIIKI_NAME_RYAKU END ");
                        sql.Append("   ELSE NULL END )AS TMH_UPN_CHIIKI_NAME2_4 ");
                        // 廃遺物種類CD2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN ISNULL(R182.HAIKI_DAI_CODE, '') + ISNULL(R182.HAIKI_CHU_CODE, '') + ");
                        sql.Append("                                           ISNULL(R182.HAIKI_SHO_CODE, '') + ISNULL(R182.HAIKI_SAI_CODE, '') ");
                        sql.Append("   ELSE ME.HAIKI_SHURUI_CD END )AS HAIKI_SHURUI_CD2 ");
                        // 廃棄物種類名2
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN R182.HAIKI_SHURUI ");
                        sql.Append("   ELSE CASE ME.HAIKI_KBN_CD WHEN 1 THEN TME_MHS1.HAIKI_SHURUI_NAME_RYAKU ");
                        sql.Append("        WHEN 2 THEN TME_MHS2.HAIKI_SHURUI_NAME_RYAKU WHEN 3 THEN TME_MHS3.HAIKI_SHURUI_NAME_RYAKU ELSE '' END ");
                        sql.Append("   END ) AS HAIKI_SHURUI_NAME2");
                        // 引渡量
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN R18EX2.GENNYOU_SUU ");
                        sql.Append("   ELSE ME.GENNYOU_SUU END) AS HIKIWATASHI ");
                        // 委託処分方法CD
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN R18EX2.SBN_HOUHOU_CD ");
                        sql.Append("   ELSE ME.SBN_HOUHOU_CD END) AS ITAKU_SBN_CD ");
                        // 委託処分方法
                        sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN DT_DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU ");
                        sql.Append("   ELSE ME_DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU END ) AS ITAKU_SBN_NAME ");

                        sql.Append(" FROM T_MANIFEST_RELATION AS MR ");
                        // 紙マニ
                        sql.Append(" LEFT JOIN ");
                        sql.Append(" (SELECT T_MANIFEST_ENTRY.*,T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID, ");
                        sql.Append("         T_MANIFEST_DETAIL.HAIKI_SHURUI_CD, T_MANIFEST_DETAIL.GENNYOU_SUU, T_MANIFEST_DETAIL.SBN_HOUHOU_CD ");
                        sql.Append("    FROM T_MANIFEST_ENTRY LEFT JOIN T_MANIFEST_DETAIL ON ");
                        sql.Append("    T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                        sql.Append("  AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ ");
                        sql.Append("  WHERE T_MANIFEST_ENTRY.DELETE_FLG = 0) AS ME ");
                        sql.Append(" ON MR.NEXT_SYSTEM_ID = ME.DETAIL_SYSTEM_ID ");
                        sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = ME.HAIKI_KBN_CD ");

                        sql.Append(" LEFT JOIN M_GENBA TME_TMH_GENBA ");
                        sql.Append(" ON ME.TMH_GYOUSHA_CD = TME_TMH_GENBA.GYOUSHA_CD AND ME.TMH_GENBA_CD = TME_TMH_GENBA.GENBA_CD AND TME_TMH_GENBA.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_TMH_GENBA_CHIIKI ");
                        sql.Append(" ON TME_TMH_GENBA.CHIIKI_CD = TME_TMH_GENBA_CHIIKI.CHIIKI_CD AND TME_TMH_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_TMH_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON TME_TMH_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = TME_TMH_GENBA_UPN_CHIIKI.CHIIKI_CD AND TME_TMH_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        // 廃棄種類
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI TME_MHS1 ");
                        sql.Append(" ON ME.HAIKI_SHURUI_CD = TME_MHS1.HAIKI_SHURUI_CD AND TME_MHS1.HAIKI_KBN_CD = 1 AND TME_MHS1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI TME_MHS2 ");
                        sql.Append(" ON ME.HAIKI_SHURUI_CD = TME_MHS2.HAIKI_SHURUI_CD AND TME_MHS2.HAIKI_KBN_CD = 2 AND TME_MHS2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HAIKI_SHURUI TME_MHS3 ");
                        sql.Append(" ON ME.HAIKI_SHURUI_CD = TME_MHS3.HAIKI_SHURUI_CD AND TME_MHS3.HAIKI_KBN_CD = 3 AND TME_MHS3.DELETE_FLG = 0 ");
                        // 運搬受託者1
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN1 ");
                        sql.Append(" ON ME.SYSTEM_ID = T_MANIFEST_UPN1.SYSTEM_ID AND ME.SEQ = T_MANIFEST_UPN1.SEQ ");
                        sql.Append(" AND T_MANIFEST_UPN1.UPN_ROUTE_NO = 1 ");

                        sql.Append(" LEFT JOIN M_GYOUSHA TME_UPNSAKI_GYOUSHA1 ");
                        sql.Append(" ON T_MANIFEST_UPN1.UPN_SAKI_GYOUSHA_CD = TME_UPNSAKI_GYOUSHA1.GYOUSHA_CD AND TME_UPNSAKI_GYOUSHA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA TME_UPNSAKI_GENBA1 ");
                        sql.Append(" ON T_MANIFEST_UPN1.UPN_SAKI_GYOUSHA_CD = TME_UPNSAKI_GENBA1.GYOUSHA_CD ");
                        sql.Append(" AND T_MANIFEST_UPN1.UPN_SAKI_GENBA_CD = TME_UPNSAKI_GENBA1.GENBA_CD AND TME_UPNSAKI_GENBA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_UPNSAKI_GENBA_CHIIKI1 ");
                        sql.Append(" ON TME_UPNSAKI_GENBA1.CHIIKI_CD = TME_UPNSAKI_GENBA_CHIIKI1.CHIIKI_CD AND TME_UPNSAKI_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_UPNSAKI_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON TME_UPNSAKI_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = TME_UPNSAKI_GENBA_UPN_CHIIKI1.CHIIKI_CD AND TME_UPNSAKI_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        // 運搬受託者2
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN2 ");
                        sql.Append(" ON ME.SYSTEM_ID = T_MANIFEST_UPN2.SYSTEM_ID AND ME.SEQ = T_MANIFEST_UPN2.SEQ ");
                        sql.Append(" AND T_MANIFEST_UPN2.UPN_ROUTE_NO = 2 ");

                        sql.Append(" LEFT JOIN M_GYOUSHA TME_UPNSAKI_GYOUSHA2 ");
                        sql.Append(" ON T_MANIFEST_UPN2.UPN_SAKI_GYOUSHA_CD = TME_UPNSAKI_GYOUSHA2.GYOUSHA_CD AND TME_UPNSAKI_GYOUSHA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA TME_UPNSAKI_GENBA2 ");
                        sql.Append(" ON T_MANIFEST_UPN2.UPN_SAKI_GYOUSHA_CD = TME_UPNSAKI_GENBA2.GYOUSHA_CD ");
                        sql.Append(" AND T_MANIFEST_UPN2.UPN_SAKI_GENBA_CD = TME_UPNSAKI_GENBA2.GENBA_CD AND TME_UPNSAKI_GENBA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_UPNSAKI_GENBA_CHIIKI2 ");
                        sql.Append(" ON TME_UPNSAKI_GENBA2.CHIIKI_CD = TME_UPNSAKI_GENBA_CHIIKI2.CHIIKI_CD AND TME_UPNSAKI_GENBA_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI TME_UPNSAKI_GENBA_UPN_CHIIKI2 ");
                        sql.Append(" ON TME_UPNSAKI_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = TME_UPNSAKI_GENBA_UPN_CHIIKI2.CHIIKI_CD AND TME_UPNSAKI_GENBA_UPN_CHIIKI2.DELETE_FLG = 0 ");
                        // 運搬受託者3
                        sql.Append(" LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN3 ");
                        sql.Append(" ON ME.SYSTEM_ID = T_MANIFEST_UPN3.SYSTEM_ID AND ME.SEQ = T_MANIFEST_UPN3.SEQ ");
                        sql.Append(" AND T_MANIFEST_UPN3.UPN_ROUTE_NO = 3 ");
                        // 処分方法
                        sql.Append(" LEFT JOIN M_SHOBUN_HOUHOU ME_DETAIL_SBN_HOUHOU ");
                        sql.Append(" ON ME.SBN_HOUHOU_CD = ME_DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_CD AND ME_DETAIL_SBN_HOUHOU.DELETE_FLG = 0 ");
                        // 電マニ
                        sql.Append(" LEFT JOIN ");
                        sql.Append(" (SELECT * FROM DT_R18_EX WHERE DELETE_FLG = 0) AS R18EX2 ");
                        sql.Append(" ON MR.NEXT_SYSTEM_ID = R18EX2.SYSTEM_ID ");
                        sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = 4 ");
                        sql.Append(" LEFT JOIN DT_MF_TOC DT_TOC ");
                        sql.Append(" ON R18EX2.KANRI_ID = DT_TOC.KANRI_ID ");
                        sql.Append(" LEFT JOIN DT_R18 R182 ");
                        sql.Append(" ON DT_TOC.KANRI_ID = R182.KANRI_ID AND DT_TOC.LATEST_SEQ = R182.SEQ ");
                        // 運搬受託者1
                        sql.Append(" LEFT JOIN DT_R19 DT_R191 ");
                        sql.Append(" ON DT_R191.KANRI_ID = R182.KANRI_ID ");
                        sql.Append(" AND DT_R191.SEQ =  R182.SEQ ");
                        sql.Append(" AND DT_R191.UPN_ROUTE_NO = 1 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX1 ");
                        sql.Append(" ON R18EX2.SYSTEM_ID = DT_R19_EX1.SYSTEM_ID AND R18EX2.SEQ = DT_R19_EX1.SEQ ");
                        sql.Append(" AND R18EX2.KANRI_ID = DT_R19_EX1.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX1.UPN_ROUTE_NO = DT_R191.UPN_ROUTE_NO ");

                        sql.Append(" LEFT JOIN M_GENBA DT_UNPAN_GENBA1 ");
                        sql.Append(" ON DT_R19_EX1.UPNSAKI_GYOUSHA_CD = DT_UNPAN_GENBA1.GYOUSHA_CD AND DT_R19_EX1.UPNSAKI_GENBA_CD = DT_UNPAN_GENBA1.GENBA_CD ");
                        sql.Append(" AND DT_UNPAN_GENBA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_CHIIKI1 ");
                        sql.Append(" ON DT_UNPAN_GENBA1.CHIIKI_CD = DT_UNPAN_GENBA_CHIIKI1.CHIIKI_CD AND DT_UNPAN_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON DT_UNPAN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = DT_UNPAN_GENBA_UPN_CHIIKI1.CHIIKI_CD AND DT_UNPAN_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        // 運搬受託者2
                        sql.Append(" LEFT JOIN DT_R19 DT_R192 ");
                        sql.Append(" ON DT_R192.KANRI_ID = R182.KANRI_ID ");
                        sql.Append(" AND DT_R192.SEQ =  R182.SEQ ");
                        sql.Append(" AND DT_R192.UPN_ROUTE_NO = 2 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX2 ");
                        sql.Append(" ON R18EX2.SYSTEM_ID = DT_R19_EX2.SYSTEM_ID AND R18EX2.SEQ = DT_R19_EX2.SEQ ");
                        sql.Append(" AND R18EX2.KANRI_ID = DT_R19_EX2.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX2.UPN_ROUTE_NO = DT_R192.UPN_ROUTE_NO ");

                        sql.Append(" LEFT JOIN M_GENBA DT_UNPAN_GENBA2 ");
                        sql.Append(" ON DT_R19_EX2.UPNSAKI_GYOUSHA_CD = DT_UNPAN_GENBA2.GYOUSHA_CD AND DT_R19_EX2.UPNSAKI_GENBA_CD = DT_UNPAN_GENBA2.GENBA_CD ");
                        sql.Append(" AND DT_UNPAN_GENBA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_CHIIKI2 ");
                        sql.Append(" ON DT_UNPAN_GENBA2.CHIIKI_CD = DT_UNPAN_GENBA_CHIIKI2.CHIIKI_CD AND DT_UNPAN_GENBA_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_UPN_CHIIKI2 ");
                        sql.Append(" ON DT_UNPAN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = DT_UNPAN_GENBA_UPN_CHIIKI2.CHIIKI_CD AND DT_UNPAN_GENBA_UPN_CHIIKI2.DELETE_FLG = 0 ");
                        // 運搬受託者3
                        sql.Append(" LEFT JOIN DT_R19 DT_R193 ");
                        sql.Append(" ON DT_R193.KANRI_ID = R182.KANRI_ID ");
                        sql.Append(" AND DT_R193.SEQ =  R182.SEQ ");
                        sql.Append(" AND DT_R193.UPN_ROUTE_NO = 3 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX3 ");
                        sql.Append(" ON R18EX2.SYSTEM_ID = DT_R19_EX3.SYSTEM_ID AND R18EX2.SEQ = DT_R19_EX3.SEQ ");
                        sql.Append(" AND R18EX2.KANRI_ID = DT_R19_EX3.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX3.UPN_ROUTE_NO = DT_R193.UPN_ROUTE_NO ");

                        sql.Append(" LEFT JOIN M_GENBA DT_UNPAN_GENBA3 ");
                        sql.Append(" ON DT_R19_EX3.UPNSAKI_GYOUSHA_CD = DT_UNPAN_GENBA3.GYOUSHA_CD AND DT_R19_EX3.UPNSAKI_GENBA_CD = DT_UNPAN_GENBA3.GENBA_CD ");
                        sql.Append(" AND DT_UNPAN_GENBA3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_CHIIKI3 ");
                        sql.Append(" ON DT_UNPAN_GENBA3.CHIIKI_CD = DT_UNPAN_GENBA_CHIIKI3.CHIIKI_CD AND DT_UNPAN_GENBA_CHIIKI3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_UPN_CHIIKI3 ");
                        sql.Append(" ON DT_UNPAN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = DT_UNPAN_GENBA_UPN_CHIIKI3.CHIIKI_CD AND DT_UNPAN_GENBA_UPN_CHIIKI3.DELETE_FLG = 0 ");
                        // 運搬受託者4
                        sql.Append(" LEFT JOIN DT_R19 DT_R194 ");
                        sql.Append(" ON DT_R194.KANRI_ID = R182.KANRI_ID ");
                        sql.Append(" AND DT_R194.SEQ =  R182.SEQ ");
                        sql.Append(" AND DT_R194.UPN_ROUTE_NO = 4 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX4 ");
                        sql.Append(" ON R18EX2.SYSTEM_ID = DT_R19_EX4.SYSTEM_ID AND R18EX2.SEQ = DT_R19_EX4.SEQ ");
                        sql.Append(" AND R18EX2.KANRI_ID = DT_R19_EX4.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX4.UPN_ROUTE_NO = DT_R194.UPN_ROUTE_NO ");

                        sql.Append(" LEFT JOIN M_GENBA DT_UNPAN_GENBA4 ");
                        sql.Append(" ON DT_R19_EX4.UPNSAKI_GYOUSHA_CD = DT_UNPAN_GENBA4.GYOUSHA_CD AND DT_R19_EX4.UPNSAKI_GENBA_CD = DT_UNPAN_GENBA4.GENBA_CD ");
                        sql.Append(" AND DT_UNPAN_GENBA4.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_CHIIKI4 ");
                        sql.Append(" ON DT_UNPAN_GENBA4.CHIIKI_CD = DT_UNPAN_GENBA_CHIIKI4.CHIIKI_CD AND DT_UNPAN_GENBA_CHIIKI4.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI DT_UNPAN_GENBA_UPN_CHIIKI4 ");
                        sql.Append(" ON DT_UNPAN_GENBA4.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = DT_UNPAN_GENBA_UPN_CHIIKI4.CHIIKI_CD AND DT_UNPAN_GENBA_UPN_CHIIKI4.DELETE_FLG = 0 ");
                        // 運搬受託者5
                        sql.Append(" LEFT JOIN DT_R19 DT_R195 ");
                        sql.Append(" ON DT_R195.KANRI_ID = R182.KANRI_ID ");
                        sql.Append(" AND DT_R195.SEQ =  R182.SEQ ");
                        sql.Append(" AND DT_R195.UPN_ROUTE_NO = 5 ");
                        sql.Append(" LEFT JOIN DT_R19_EX DT_R19_EX5 ");
                        sql.Append(" ON R18EX2.SYSTEM_ID = DT_R19_EX5.SYSTEM_ID AND R18EX2.SEQ = DT_R19_EX5.SEQ ");
                        sql.Append(" AND R18EX2.KANRI_ID = DT_R19_EX5.KANRI_ID ");
                        sql.Append(" AND DT_R19_EX5.UPN_ROUTE_NO = DT_R195.UPN_ROUTE_NO ");
                        // 処分方法
                        sql.Append(" LEFT JOIN M_SHOBUN_HOUHOU DT_DETAIL_SBN_HOUHOU ");
                        sql.Append(" ON R18EX2.SBN_HOUHOU_CD = DT_DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_CD AND DT_DETAIL_SBN_HOUHOU.DELETE_FLG = 0 ");

                        sql.Append(" WHERE MR.DELETE_FLG = 0 ");
                        sql.Append(" ) AS NIJI ");
                        sql.Append(" ON REL.NEXT_SYSTEM_ID = NIJI.SYSTEM_ID ");
                        sql.Append(" AND REL.NEXT_HAIKI_KBN_CD = NIJI.HAIKI_KBN_CD ");

                        sql.Append(" LEFT JOIN DT_R19 UPN");
                        sql.Append(" ON R18.KANRI_ID = UPN.KANRI_ID");
                        sql.Append(" AND R18.SEQ = UPN.SEQ");
                        sql.Append(" AND UPN.UPNSAKI_JOU_KBN IN (2,3,4)");

                        #endregion

                        #endregion

                        #region WHERE

                        sql.Append(" WHERE (DMT.KIND = 4 or DMT.KIND = 5 or DMT.KIND IS NULL) ");
                        sql.Append(" AND (DMT.STATUS_FLAG = 3 OR DMT.STATUS_FLAG = 4) ");
                        sql.Append(" AND R18EX.DELETE_FLG = 0 ");
                        sql.Append(" AND ISNULL(R18.FIRST_MANIFEST_FLAG, '') = '' ");

                        if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                        {
                            sql.Append(" AND ( ");
                            sql.AppendFormat(" UPN_EX1.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat(" OR UPN_EX2.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat(" OR UPN_EX3.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat(" OR UPN_EX4.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat(" OR UPN_EX5.UPN_GYOUSHA_CD = '{0}' ) ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                        }
                        if (!string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
                        {
                            sql.AppendFormat(" AND R18EX.SBN_GYOUSHA_CD = '{0}' ", this.form.cantxt_SyobunJyutakuNameCd.Text);
                        }

                        //処分事業場
                        if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
                        {

                            sql.AppendFormat(" AND R18EX.SBN_GENBA_CD = '{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                        }

                        if (!string.IsNullOrEmpty(this.form.KOUFU_DATE_KBN.Text))
                        {
                            //年月日（開始）
                            if (KOUFU_DATE_FROM != "" && this.form.KOUFU_DATE_KBN.Text != "2")
                            {
                                sql.Append(" AND ( ");

                                if (this.form.KOUFU_DATE_KBN.Text == "1")
                                {
                                    // 交付年月日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "3")
                                {
                                    // 処分終了日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.SBN_END_DATE) END) >= '{0}' ", KOUFU_DATE_FROM);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "4")
                                {
                                    // 最終処分終了日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.LAST_SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.LAST_SBN_END_DATE) END) >= '{0}' ", KOUFU_DATE_FROM);
                                }

                                sql.Append(" ) ");
                            }
                            //年月日（終了）
                            if (KOUFU_DATE_TO != "" && this.form.KOUFU_DATE_KBN.Text != "2")
                            {
                                sql.Append(" AND ( ");

                                if (this.form.KOUFU_DATE_KBN.Text == "1")
                                {
                                    // 交付年月日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ELSE NULL END) <= '{0}' ", KOUFU_DATE_TO);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "3")
                                {
                                    // 処分終了日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.SBN_END_DATE) END) <= '{0}' ", KOUFU_DATE_TO);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "4")
                                {
                                    // 最終処分終了日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.LAST_SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.LAST_SBN_END_DATE) END) <= '{0}' ", KOUFU_DATE_TO);
                                }

                                sql.Append(" ) ");
                            }
                            
                            // 運搬終了日
                            if (this.form.KOUFU_DATE_KBN.Text == "2")
                            {
                                if (KOUFU_DATE_FROM != "" && KOUFU_DATE_TO != "")
                                {
                                    sql.Append(" AND ( ");
                                    if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                                    {
                                        sql.AppendFormat(" (  (UPN_EX1.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN1.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN_EX2.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN2.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN_EX3.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN3.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN_EX4.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN4.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN_EX5.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN5.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) <= '{0}' )) ", KOUFU_DATE_TO);
                                    }
                                    else
                                    {
                                        sql.AppendFormat(" (  (UPN1.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN2.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN3.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN4.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN5.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) <= '{0}' )) ", KOUFU_DATE_TO);
                                    }
                                    sql.Append(" ) ");
                                }
                                else if (KOUFU_DATE_FROM != "")
                                {
                                    sql.Append(" AND ( ");
                                    sql.AppendFormat(" ( (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) >= '{0}' OR ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) >= '{0}' OR ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) >= '{0}' OR ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) >= '{0}' OR ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) >= '{0}' ) ", KOUFU_DATE_FROM);
                                    sql.Append(" ) ");
                                }
                                else if (KOUFU_DATE_TO != "")
                                {
                                    sql.Append(" AND ( ");
                                    sql.AppendFormat(" ( (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) <= '{0}' OR ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) <= '{0}' OR ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) <= '{0}' OR ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) <= '{0}' OR ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                    sql.Append(" ) ");
                                }
                            }
                        }

                        // 自社運搬
                        if (!string.IsNullOrEmpty(this.form.JISHA_UNPAN_KBN.Text))
                        {
                            if (this.form.JISHA_UNPAN_KBN.Text == "1")
                            {
                                sql.Append(" AND ISNULL(R18.HST_SHA_EDI_MEMBER_ID, '') = ISNULL(UPN1.UPN_SHA_EDI_MEMBER_ID, '') ");
                            }
                            else if (this.form.JISHA_UNPAN_KBN.Text == "2")
                            {
                                sql.Append(" AND ISNULL(R18.HST_SHA_EDI_MEMBER_ID, '') <> ISNULL(UPN1.UPN_SHA_EDI_MEMBER_ID, '') ");
                            }
                        }

                        // 処分方法
                        if (!string.IsNullOrEmpty(this.form.SHOBUN_HOUHOU_CD.Text))
                        {
                            sql.AppendFormat(" AND R18EX.SBN_HOUHOU_CD = '{0}' ", this.form.SHOBUN_HOUHOU_CD.Text);
                        }
                        else if (this.form.SHOBUN_HOUHOU_MI.Checked)
                        {
                            sql.Append(" AND (R18EX.SBN_HOUHOU_CD IS NULL OR R18EX.SBN_HOUHOU_CD = '') ");
                        }

                        // 報告書分類
                        if (!string.IsNullOrEmpty(this.form.cantxt_HokokushoBunrui.Text))
                        {
                            sql.Append(" AND ( CASE WHEN ISNULL(R18MIX.HAIKI_SHURUI_CD, '') = '' THEN R18_MDHS1.HOUKOKUSHO_BUNRUI_CD ");
                            sql.AppendFormat("   ELSE R18MIX_MDHS1.HOUKOKUSHO_BUNRUI_CD END ) = '{0}' ", this.form.cantxt_HokokushoBunrui.Text);
                        }

                        // 排出事業者
                        if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text))
                        {
                            sql.AppendFormat(" AND R18EX.HST_GYOUSHA_CD = '{0}' ", this.form.cantxt_HaisyutuGyousyaCd.Text);

                            if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuJigyoubaName.Text))
                            {
                                sql.AppendFormat(" AND R18EX.HST_GENBA_CD = '{0}' ", this.form.cantxt_HaisyutuJigyoubaName.Text);
                            }
                        }

                        #endregion

                        #endregion
                    }

                    if (this.header.MANI_KBN.Text == "3")
                    {
                        sql.Append(" UNION ALL ");
                    }

                    if (this.header.MANI_KBN.Text == "2" || this.header.MANI_KBN.Text == "3")
                    {
                        #region 二次マニ

                        #region SELECT

                        sql.Append(" SELECT ");
                        // マニ
                        sql.Append("   R18EX.SYSTEM_ID ");                                                                    // システムID
                        sql.Append(" , R18EX.SEQ ");                                                                          // 枝番
                        sql.Append(" , DMT.LATEST_SEQ AS LATEST_SEQ ");                                                       // 最終枝番（紙マニは必ず空）
                        sql.Append(" , DMT.KANRI_ID AS KANRI_ID ");                                                           // 管理ID（紙マニは必ず空）
                        sql.Append(" , 4 AS HAIKI_KBN_CD ");                                                                  // 廃棄区分CD
                        sql.Append(" , NULL AS KYOTEN_CD ");                                                                  // 拠点
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18_MDHS1.HOUKOKUSHO_BUNRUI_CD ");
                        sql.Append("   ELSE R18MIX_MDHS1.HOUKOKUSHO_BUNRUI_CD END AS HOUKOKUSHO_BUNRUI_CD ");                 // 報告書分類ＣＤ
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18_MHB1.HOUKOKUSHO_BUNRUI_NAME_RYAKU ");
                        sql.Append("   ELSE R18MIX_MHB1.HOUKOKUSHO_BUNRUI_NAME_RYAKU END AS HOUKOKUSHO_BUNRUI_NAME ");        // 報告書分類名
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18EX.HAIKI_NAME_CD ");
                        sql.Append("   ELSE R18MIX.HAIKI_NAME_CD END AS HAIKI_NAME_CD ");                                     // 廃棄物名称CD
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18.HAIKI_NAME ");
                        sql.Append("   ELSE R18MIX_MDHN.HAIKI_NAME END AS HAIKI_NAME ");                                      // 廃棄物名称
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18.HAIKI_SUU ");
                        sql.Append("   ELSE R18MIX.HAIKI_SUU END AS HAIKI_SUU");                                              // マニフェスト数量
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18.HAIKI_UNIT_CODE ");
                        sql.Append("   ELSE R18MIX.HAIKI_UNIT_CD END AS HAIKI_UNIT_CD ");                                     // 単位CD1
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18_MU.UNIT_NAME_RYAKU ");
                        sql.Append("   ELSE R18MIX_MU.UNIT_NAME_RYAKU END AS HAIKI_UNIT_NAME ");                              // 単位名1
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18EX.KANSAN_SUU ");
                        sql.Append("   ELSE R18MIX.KANSAN_SUU END AS KANSAN_SUU ");                                           // 運搬委託量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD2 ");                            // 単位CD2
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME2 ");                                      // 単位名2
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18EX.KANSAN_SUU ");
                        sql.Append("   ELSE R18MIX.KANSAN_SUU END AS SBN_SUU ");                                              // 処分受託量
                        sql.Append(" , ( SELECT MANI_KANSAN_KIHON_UNIT_CD ");
                        sql.Append("    FROM M_SYS_INFO WHERE DELETE_FLG = 0 ) AS SYS_UNIT_CD3 ");                            // 単位CD3
                        sql.Append(" , ( SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_SYS_INFO ");
                        sql.Append("   LEFT JOIN M_UNIT ON M_SYS_INFO.MANI_KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD ");
                        sql.Append("   AND M_UNIT.DELETE_FLG = 0 ) AS SYS_UNIT_NAME3 ");                                      // 単位名3
                        sql.Append(" , CASE WHEN R18.FIRST_MANIFEST_FLAG IS NULL THEN 0 ");
                        sql.Append("        WHEN R18.FIRST_MANIFEST_FLAG = '' THEN 0 ");
                        sql.Append("        WHEN ISNULL(FIRST_HST_GYOUSHA.JISHA_KBN, 0) = 0 THEN 0 ");
                        sql.Append("   ELSE 1 END AS FIRST_MANIFEST_KBN ");                                                   // 一次マニ区分
                        sql.Append(" , R18EX.HST_GYOUSHA_CD AS HST_GYOUSHA_CD ");                                             // 排出事業者CD
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(R18.HST_SHA_NAME, ''),1, 40)) + ");
                        sql.Append("   SUBSTRING(ISNULL(R18.HST_SHA_NAME, ''),41, 40)) AS HST_GYOUSHA_NAME ");                // 排出事業者名
                        sql.Append(" , ISNULL(R18.HST_SHA_ADDRESS1, '') + ISNULL(R18.HST_SHA_ADDRESS2, '') ");
                        sql.Append(" + ISNULL(R18.HST_SHA_ADDRESS3, '') + ISNULL(R18.HST_SHA_ADDRESS4, '') ");
                        sql.Append("   AS HST_GYOUSHA_ADDRESS ");                                                             // 排出事業者住所
                        sql.Append(" , HST_GYOUSHA1.CHIIKI_CD AS HST_GYOUSHA_CHIIKI_CD ");                                    // 排出事業者地域CD
                        sql.Append(" , HST_GYOUSHA_CHIIKI1.CHIIKI_NAME_RYAKU AS HST_GYOUSHA_CHIIKI_NAME ");                   // 排出事業者地域名
                        sql.Append(" , HST_GYOUSHA1.GYOUSHU_CD AS HST_GYOUSHA_GYOUSHU_CD ");                                  // 排出事業者業種CD
                        sql.Append(" , HST_GYOUSHA_GYOUSHU1.GYOUSHU_NAME_RYAKU AS HST_GYOUSHA_GYOUSHU_NAME ");                // 排出事業者業種名
                        sql.Append(" , R18EX.HST_GENBA_CD AS HST_GENBA_CD ");                                                 // 排出事業場CD
                        sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(R18.HST_JOU_NAME, ''),1, 40)) + ");
                        sql.Append("   SUBSTRING(ISNULL(R18.HST_JOU_NAME, ''),41, 40)) AS HST_GENBA_NAME ");                  // 排出事業場名
                        sql.Append(" , ISNULL(R18.HST_JOU_ADDRESS1, '') + ISNULL(R18.HST_JOU_ADDRESS2, '') ");
                        sql.Append(" + ISNULL(R18.HST_JOU_ADDRESS3, '') + ISNULL(R18.HST_JOU_ADDRESS4, '') ");
                        sql.Append("   AS HST_GENBA_ADDRESS ");                                                               // 排出事業場住所
                        sql.Append(" , HST_GENBA1.CHIIKI_CD AS HST_GENBA_CHIIKI_CD ");                                        // 排出事業場地域CD
                        sql.Append(" , HST_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU AS HST_GENBA_CHIIKI_NAME ");                       // 排出事業場地域名
                        sql.Append(" , HST_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS HST_UPN_CHIIKI_CD ");                 // 排出事業場運搬報告地域CD
                        sql.Append(" , HST_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU AS HST_UPN_CHIIKI_NAME ");                     // 排出事業場運搬報告地域名
                        sql.Append(" , HST_GENBA1.GYOUSHU_CD AS HST_GENBA_GYOUSHU_CD ");                                      // 排出事業場業種CD
                        sql.Append(" , HST_GENBA_GYOUSHU1.GYOUSHU_NAME_RYAKU AS HST_GENBA_GYOUSHU_NAME ");                    // 排出事業場業種名
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN ");
                        sql.Append("     ISNULL(R18.HAIKI_DAI_CODE, '') + ISNULL(R18.HAIKI_CHU_CODE, '')+ ISNULL(R18.HAIKI_SHO_CODE, '') + ISNULL(R18.HAIKI_SAI_CODE, '') ");
                        sql.Append("   ELSE R18MIX.HAIKI_SHURUI_CD END AS HAIKI_SHURUI_CD ");                                 // 廃棄物種類CD1
                        sql.Append(" , CASE WHEN ISNULL(R18MIX.SYSTEM_ID, '') = '' THEN R18.HAIKI_SHURUI ");
                        sql.Append("   ELSE R18MIX_MDHS1.HAIKI_SHURUI_NAME END AS HAIKI_SHURUI_NAME ");                       // 廃棄物種類名1
                        sql.Append(" , R18.MANIFEST_ID AS MANIFEST_ID ");                                                     // 交付番号1
                        sql.Append(" , CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ");
                        sql.Append("   ELSE NULL END AS KOUFU_DATE ");                                                        // 交付年月日1

                        // 収集運搬
                        sql.Append(" , UPN_EX1.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_1 ");                                        // 運搬受託者CD1_1
                        sql.Append(" , UPN1.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_1 ");                                           // 運搬受託者名1_1
                        sql.Append(" , CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_1 ");                                                   // 運搬終了年月日1_1
                        sql.Append(" , UPN_EX2.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_2 ");                                        // 運搬受託者CD1_2
                        sql.Append(" , UPN2.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_2 ");                                           // 運搬受託者名1_2
                        sql.Append(" , CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_2 ");                                                   // 運搬終了年月日1_2
                        sql.Append(" , UPN_EX3.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_3 ");                                        // 運搬受託者CD1_3
                        sql.Append(" , UPN3.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_3 ");                                           // 運搬受託者名1_3
                        sql.Append(" , CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_3 ");                                                   // 運搬終了年月日1_3
                        sql.Append(" , UPN_EX4.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_4 ");                                        // 運搬受託者CD1_4
                        sql.Append(" , UPN4.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_4 ");                                           // 運搬受託者名1_4
                        sql.Append(" , CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_4 ");                                                   // 運搬終了年月日1_4
                        sql.Append(" , UPN_EX5.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1_5 ");                                        // 運搬受託者CD1_5
                        sql.Append(" , UPN5.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1_5 ");                                           // 運搬受託者名1_5
                        sql.Append(" , CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ");
                        sql.Append("   ELSE NULL END AS UPN_END_DATE1_5 ");                                                   // 運搬終了年月日1_5

                        // 積替保管情報
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX1.UPNSAKI_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_1 ");                           // 積替保管業者CD1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN1.UPNSAKI_NAME END AS TMH_GYOUSHA_NAME1_1 ");                                  // 積替保管業者名1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX1.UPNSAKI_GENBA_CD END AS TMH_GENBA_CD1_1 ");                               // 積替保管現場CD1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN1.UPNSAKI_JOU_NAME END AS TMH_GENBA_NAME1_1 ");                                // 積替保管現場名1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE ISNULL(UPN1.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN1.UPNSAKI_JOU_ADDRESS2, '') + ");
                        sql.Append("        ISNULL(UPN1.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN1.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   END AS TMH_GENBA_ADDRESS1_1 ");                                                        // 積替保管場所住所1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA1.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_1 ");                          // 積替保管場所地域CD1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_CHIIKI1.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_1 ");         // 積替保管場所地域名1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_1 ");   // 積替保管場所運搬報告先地域CD1_1
                        sql.Append(" , CASE WHEN UPN_EX1.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX1.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_UPN_CHIIKI1.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_1 ");       // 積替保管場所運搬報告先地域名1_1
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX2.UPNSAKI_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_2 ");                           // 積替保管業者CD1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN2.UPNSAKI_NAME END AS TMH_GYOUSHA_NAME1_2 ");                                  // 積替保管業者名1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX2.UPNSAKI_GENBA_CD END AS TMH_GENBA_CD1_2 ");                               // 積替保管現場CD1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN2.UPNSAKI_JOU_NAME END AS TMH_GENBA_NAME1_2 ");                                // 積替保管現場名1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE ISNULL(UPN2.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN2.UPNSAKI_JOU_ADDRESS2, '') + ");
                        sql.Append("        ISNULL(UPN2.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN2.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   END AS TMH_GENBA_ADDRESS1_2 ");                                                        // 積替保管場所住所1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA2.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_2 ");                          // 積替保管場所地域CD1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_CHIIKI2.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_2 ");         // 積替保管場所地域名1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_2 ");   // 積替保管場所運搬報告先地域CD1_2
                        sql.Append(" , CASE WHEN UPN_EX2.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX2.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_UPN_CHIIKI2.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_2 ");       // 積替保管場所運搬報告先地域名1_2
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX3.UPNSAKI_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_3 ");                           // 積替保管業者CD1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN3.UPNSAKI_NAME END AS TMH_GYOUSHA_NAME1_3 ");                                  // 積替保管業者名1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX3.UPNSAKI_GENBA_CD END AS TMH_GENBA_CD1_3 ");                               // 積替保管現場CD1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN3.UPNSAKI_JOU_NAME END AS TMH_GENBA_NAME1_3 ");                                // 積替保管現場名1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE ISNULL(UPN3.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN3.UPNSAKI_JOU_ADDRESS2, '') + ");
                        sql.Append("        ISNULL(UPN3.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN3.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   END AS TMH_GENBA_ADDRESS1_3 ");                                                        // 積替保管場所住所1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA3.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_3 ");                          // 積替保管場所地域CD1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_CHIIKI3.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_3 ");         // 積替保管場所地域名1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_3 ");   // 積替保管場所運搬報告先地域CD1_3
                        sql.Append(" , CASE WHEN UPN_EX3.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX3.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_UPN_CHIIKI3.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_3 ");       // 積替保管場所運搬報告先地域名1_3
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX4.UPNSAKI_GYOUSHA_CD END AS TMH_GYOUSHA_CD1_4 ");                           // 積替保管業者CD1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN4.UPNSAKI_NAME END AS TMH_GYOUSHA_NAME1_4 ");                                  // 積替保管業者名1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN_EX4.UPNSAKI_GENBA_CD END AS TMH_GENBA_CD1_4 ");                               // 積替保管現場CD1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UPN4.UPNSAKI_JOU_NAME END AS TMH_GENBA_NAME1_4 ");                                // 積替保管現場名1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE ISNULL(UPN4.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN4.UPNSAKI_JOU_ADDRESS2, '') + ");
                        sql.Append("        ISNULL(UPN4.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN4.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   END AS TMH_GENBA_ADDRESS1_4 ");                                                        // 積替保管場所住所1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA4.CHIIKI_CD END AS TMH_GENBA_CHIIKI_CD1_4 ");                          // 積替保管場所地域CD1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_CHIIKI4.CHIIKI_NAME_RYAKU END AS TMH_GENBA_CHIIKI_NAME1_4 ");         // 積替保管場所地域名1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA4.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD END AS TMH_UPN_CHIIKI_CD1_4 ");   // 積替保管場所運搬報告先地域CD1_4
                        sql.Append(" , CASE WHEN UPN_EX4.UPNSAKI_GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD AND UPN_EX4.UPNSAKI_GENBA_CD = R18EX.SBN_GENBA_CD THEN NULL ");
                        sql.Append("   ELSE UNPAN_GENBA_UPN_CHIIKI4.CHIIKI_NAME_RYAKU END AS TMH_UPN_CHIIKI_NAME1_4 ");           // 積替保管場所運搬報告先地域名1_4

                        // 処分受託者情報
                        sql.Append(" , R18EX.SBN_GYOUSHA_CD AS SBN_GYOUSHA_CD ");                                             // 処分業者CD
                        sql.Append(" , R18.SBN_SHA_NAME AS SBN_GYOUSHA_NAME ");                                               // 処分業者名
                        sql.Append(" , ISNULL(R18.SBN_SHA_ADDRESS1, '') + ISNULL(R18.SBN_SHA_ADDRESS2, '') ");
                        sql.Append(" + ISNULL(R18.SBN_SHA_ADDRESS3, '') + ISNULL(R18.SBN_SHA_ADDRESS4, '') ");
                        sql.Append("   AS SBN_GYOUSHA_ADDRESS ");                                                             // 処分業者住所
                        sql.Append(" , R18EX.SBN_GENBA_CD AS SBN_GENBA_CD ");                                                 // 処分事業場CD
                        sql.Append(" , DT_R19_LAST.UPNSAKI_JOU_NAME AS SBN_GENBA_NAME ");                                     // 処分事業場名称
                        sql.Append(" , ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS2, '') ");
                        sql.Append(" + ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R19_LAST.UPNSAKI_JOU_ADDRESS4, '') ");
                        sql.Append("   AS SBN_GENBA_ADDRESS ");                                                               // 処分事業場住所
                        sql.Append(" , SBN_GENBA.CHIIKI_CD AS SBN_GENBA_CHIIKI_CD ");                                         // 処分事業場地域CD
                        sql.Append(" , SBN_GENBA_CHIIKI.CHIIKI_NAME_RYAKU AS SBN_GENBA_CHIIKI_NAME ");                        // 処分事業場地域名
                        sql.Append(" , SBN_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS SBN_UPN_CHIIKI_CD ");                  // 処分事業場運搬報告地域CD
                        sql.Append(" , SBN_GENBA_UPN_CHIIKI.CHIIKI_NAME_RYAKU AS SBN_UPN_CHIIKI_NAME ");                      // 処分事業場運搬報告地域名

                        // 明細
                        sql.Append(" , CASE WHEN ISNULL(R18EX.SBN_HOUHOU_CD,'') = '' THEN R18.SBN_WAY_CODE");
                        sql.Append("       ELSE R18EX.SBN_HOUHOU_CD END AS DETAIL_SBN_HOUHOU_CD ");                                        // 処分方法CD
                        sql.Append(" , CASE WHEN ISNULL(R18EX.SBN_HOUHOU_CD,'') = '' THEN R18.SBN_WAY_NAME");
                        sql.Append("       ELSE DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU END AS DETAIL_SBN_HOUHOU_NAME ");               // 処分方法名
                        sql.Append(" , CASE WHEN ISDATE(R18.SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.SBN_END_DATE) END ");
                        sql.Append("   AS DETAIL_SBN_END_DATE ");                                                             // 処分終了年月日1
                        sql.Append(" , CASE WHEN ISDATE(R18.LAST_SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.LAST_SBN_END_DATE) END ");
                        sql.Append("   AS DETAIL_LAST_SBN_END_DATE ");                                                        // 最終処分終了年月日1
                        sql.Append(" , NULL AS DETAIL_SYSTEM_ID ");                                                           // 明細システムID
                        // 二次マニ
                        sql.Append(" , NULL AS MANIFEST_ID2 ");                                                               // 交付番号2
                        sql.Append(" , NULL AS KOUFU_DATE2 ");                                                                // 交付年月日2
                        // 収集運搬
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_1 ");                                                          // 運搬受託者CD2_1
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_1 ");                                                        // 運搬受託者名2_1
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_2 ");                                                          // 運搬受託者CD2_2
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_2 ");                                                        // 運搬受託者名2_2
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_3 ");                                                          // 運搬受託者CD2_3
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_3 ");                                                        // 運搬受託者名2_3
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_4 ");                                                          // 運搬受託者CD2_4
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_4 ");                                                        // 運搬受託者名2_4
                        sql.Append(" , NULL AS UPN_GYOUSHA_CD2_5 ");                                                          // 運搬受託者CD2_5
                        sql.Append(" , NULL AS UPN_GYOUSHA_NAME2_5 ");                                                        // 運搬受託者名2_5
                        // 積替保管情報
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD2_1 ");                                                          // 積替保管業者CD2_1
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME2_1 ");                                                        // 積替保管業者名2_1
                        sql.Append(" , NULL AS TMH_GENBA_CD2_1 ");                                                            // 積替保管現場CD2_1
                        sql.Append(" , NULL AS TMH_GENBA_NAME2_1 ");                                                          // 積替保管現場名2_1
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS2_1 ");                                                       // 積替保管場所住所2_1
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD2_1 ");                                                     // 積替保管場所地域CD2_1
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME2_1 ");                                                   // 積替保管場所地域名2_1
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD2_1 ");                                                       // 積替保管場所運搬報告先地域CD2_1
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME2_1 ");                                                     // 積替保管場所運搬報告先地域名2_1
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD2_2 ");                                                          // 積替保管業者CD2_2
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME2_2 ");                                                        // 積替保管業者名2_2
                        sql.Append(" , NULL AS TMH_GENBA_CD2_2 ");                                                            // 積替保管現場CD2_2
                        sql.Append(" , NULL AS TMH_GENBA_NAME2_2 ");                                                          // 積替保管現場名2_2
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS2_2 ");                                                       // 積替保管場所住所2_2
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD2_2 ");                                                     // 積替保管場所地域CD2_2
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME2_2 ");                                                   // 積替保管場所地域名2_2
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD2_2 ");                                                       // 積替保管場所運搬報告先地域CD2_2
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME2_2 ");                                                     // 積替保管場所運搬報告先地域名2_2
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD2_3 ");                                                          // 積替保管業者CD2_3
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME2_3 ");                                                        // 積替保管業者名2_3
                        sql.Append(" , NULL AS TMH_GENBA_CD2_3 ");                                                            // 積替保管現場CD2_3
                        sql.Append(" , NULL AS TMH_GENBA_NAME2_3 ");                                                          // 積替保管現場名2_3
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS2_3 ");                                                       // 積替保管場所住所2_3
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD2_3 ");                                                     // 積替保管場所地域CD2_3
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME2_3 ");                                                   // 積替保管場所地域名2_3
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD2_3 ");                                                       // 積替保管場所運搬報告先地域CD2_3
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME2_3 ");                                                     // 積替保管場所運搬報告先地域名2_3
                        sql.Append(" , NULL AS TMH_GYOUSHA_CD2_4 ");                                                          // 積替保管業者CD2_4
                        sql.Append(" , NULL AS TMH_GYOUSHA_NAME2_4 ");                                                        // 積替保管業者名2_4
                        sql.Append(" , NULL AS TMH_GENBA_CD2_4 ");                                                            // 積替保管現場CD2_4
                        sql.Append(" , NULL AS TMH_GENBA_NAME2_4 ");                                                          // 積替保管現場名2_4
                        sql.Append(" , NULL AS TMH_GENBA_ADDRESS2_4 ");                                                       // 積替保管場所住所2_4
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_CD2_4 ");                                                     // 積替保管場所地域CD2_4
                        sql.Append(" , NULL AS TMH_GENBA_CHIIKI_NAME2_4 ");                                                   // 積替保管場所地域名2_4
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_CD2_4 ");                                                       // 積替保管場所運搬報告先地域CD2_4
                        sql.Append(" , NULL AS TMH_UPN_CHIIKI_NAME2_4 ");                                                     // 積替保管場所運搬報告先地域名2_4
                        // 明細
                        sql.Append(" , NULL AS HAIKI_SHURUI_CD2 ");                                                           // 廃棄物種類CD2
                        sql.Append(" , NULL AS HAIKI_SHURUI_NAME2 ");                                                         // 廃棄物種類名2
                        sql.Append(" , NULL AS HIKIWATASHI ");                                                                // 引渡量
                        sql.Append(" , NULL AS SYS_UNIT_CD2_2 ");                                                             // 単位CD（引渡量）
                        sql.Append(" , NULL AS SYS_UNIT_NAME2_2 ");                                                           // 単位名（引渡量）
                        sql.Append(" , NULL AS ITAKU_SBN_CD ");                                                               // 委託処分方法CD
                        sql.Append(" , NULL AS ITAKU_SBN_NAME ");                                                             // 委託処分方法

                        #endregion

                        #region FROM

                        // マニ入力
                        sql.Append(" FROM DT_MF_TOC DMT ");
                        // マニ情報
                        sql.Append(" INNER JOIN DT_R18 R18 ON DMT.KANRI_ID = R18.KANRI_ID AND DMT.LATEST_SEQ = R18.SEQ ");
                        // マニ情報拡張
                        sql.Append(" LEFT JOIN ");
                        sql.Append("( ");
                        sql.Append("SELECT ");
                        sql.Append("  R18EX.SYSTEM_ID ");
                        sql.Append(" ,R18EX.SEQ ");
                        sql.Append(" ,R18EX.KANRI_ID ");
                        sql.Append(" ,R18EX.MANIFEST_ID ");
                        sql.Append(" ,R18EX.HST_GYOUSHA_CD ");
                        sql.Append(" ,R18EX.HST_GENBA_CD ");
                        sql.Append(" ,R18EX.SBN_GYOUSHA_CD ");
                        sql.Append(" ,R18EX.SBN_GENBA_CD ");
                        sql.Append(" ,R18EX.NO_REP_SBN_EDI_MEMBER_ID ");
                        sql.Append(" ,R18EX.SBN_HOUHOU_CD ");
                        sql.Append(" ,R18EX.HOUKOKU_TANTOUSHA_CD ");
                        sql.Append(" ,R18EX.SBN_TANTOUSHA_CD ");
                        sql.Append(" ,R18EX.UPN_TANTOUSHA_CD ");
                        sql.Append(" ,R18EX.SHARYOU_CD ");
                        sql.Append(" ,R18EX.KANSAN_SUU ");
                        sql.Append(" ,CREATE_DATA.CREATE_USER ");
                        sql.Append(" ,R18EX.CREATE_DATE ");
                        sql.Append(" ,CREATE_DATA.CREATE_PC ");
                        sql.Append(" ,R18EX.UPDATE_USER ");
                        sql.Append(" ,R18EX.UPDATE_DATE ");
                        sql.Append(" ,R18EX.UPDATE_PC ");
                        sql.Append(" ,R18EX.DELETE_FLG ");
                        sql.Append(" ,R18EX.TIME_STAMP ");
                        sql.Append(" ,R18EX.HAIKI_NAME_CD ");
                        sql.Append(" ,R18EX.GENNYOU_SUU ");
                        sql.Append("FROM  ");
                        sql.Append("  DT_R18_EX R18EX ");
                        sql.Append(" ,( ");
                        sql.Append("   SELECT ");
                        sql.Append("    R18EX.SYSTEM_ID ");
                        sql.Append("   ,R18EX.SEQ ");
                        sql.Append("   ,R18EX.KANRI_ID ");
                        sql.Append("   ,R18EX.CREATE_USER ");
                        sql.Append("   ,R18EX.CREATE_DATE ");
                        sql.Append("   ,R18EX.CREATE_PC ");
                        sql.Append("  FROM ");
                        sql.Append("   DT_R18_EX R18EX ");
                        sql.Append("   ,(SELECT ");
                        sql.Append("      SYSTEM_ID ");
                        sql.Append("     ,MIN(SEQ) MIN_SEQ ");
                        sql.Append("     FROM ");
                        sql.Append("      DT_R18_EX ");
                        sql.Append("     GROUP BY  ");
                        sql.Append("      SYSTEM_ID ");
                        sql.Append("     ) SEQ_DATA ");
                        sql.Append("  WHERE ");
                        sql.Append("       R18EX.SYSTEM_ID = seq_data.SYSTEM_ID ");
                        sql.Append("   AND R18EX.SEQ = SEQ_DATA.MIN_SEQ ");
                        sql.Append("   ) CREATE_DATA ");
                        sql.Append("WHERE ");
                        sql.Append(" R18EX.SYSTEM_ID = CREATE_DATA.SYSTEM_ID ");
                        sql.Append(") R18EX ");
                        sql.Append(" ON R18.KANRI_ID = R18EX.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = ( SELECT MAX(SYSTEM_ID) FROM DT_R18_EX TMP WHERE R18.KANRI_ID = TMP.KANRI_ID ) ");
                        sql.Append(" AND R18EX.SEQ = ( SELECT MAX(SEQ) FROM DT_R18_EX TMP1 WHERE TMP1.SYSTEM_ID = ( SELECT MAX(SYSTEM_ID) FROM DT_R18_EX TMP2 WHERE R18.KANRI_ID = TMP2.KANRI_ID )  ) ");
                        sql.Append(" LEFT JOIN DT_R18_MIX R18MIX ");
                        sql.Append(" ON R18EX.SYSTEM_ID = R18MIX.SYSTEM_ID ");
                        sql.Append(" AND R18EX.KANRI_ID = R18MIX.KANRI_ID ");
                        sql.Append(" AND R18MIX.DELETE_FLG = 0 ");
                        // 廃棄種類
                        sql.Append(" LEFT JOIN M_DENSHI_HAIKI_SHURUI R18MIX_MDHS1 ");
                        sql.Append(" ON ISNULL(R18MIX.HAIKI_DAI_CODE, 0) + ISNULL(R18MIX.HAIKI_CHU_CODE, 0) + ISNULL(R18MIX.HAIKI_SHO_CODE, 0) = R18MIX_MDHS1.HAIKI_SHURUI_CD ");
                        sql.Append(" AND R18MIX_MDHS1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_DENSHI_HAIKI_SHURUI R18_MDHS1 ");
                        sql.Append(" ON ISNULL(R18.HAIKI_DAI_CODE, 0) + ISNULL(R18.HAIKI_CHU_CODE, 0) + ISNULL(R18.HAIKI_SHO_CODE, 0) = R18_MDHS1.HAIKI_SHURUI_CD ");
                        sql.Append(" AND R18_MDHS1.DELETE_FLG = 0 ");
                        // 報告書分類
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI R18MIX_MHB1 ");
                        sql.Append(" ON R18MIX_MDHS1.HOUKOKUSHO_BUNRUI_CD = R18MIX_MHB1.HOUKOKUSHO_BUNRUI_CD AND R18MIX_MHB1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI R18_MHB1 ");
                        sql.Append(" ON R18_MDHS1.HOUKOKUSHO_BUNRUI_CD = R18_MHB1.HOUKOKUSHO_BUNRUI_CD ");
                        sql.Append(" AND R18_MHB1.DELETE_FLG = 0 ");
                        // 廃棄物名称
                        sql.Append(" LEFT JOIN M_DENSHI_HAIKI_NAME R18MIX_MDHN ");
                        sql.Append(" ON R18MIX.HAIKI_NAME_CD = R18MIX_MDHN.HAIKI_NAME_CD AND R18MIX_MDHN.DELETE_FLG = 0 ");
                        sql.Append(" AND R18.HST_SHA_EDI_MEMBER_ID = R18MIX_MDHN.EDI_MEMBER_ID ");
                        // 単位
                        sql.Append(" LEFT JOIN M_UNIT R18MIX_MU ");
                        sql.Append(" ON R18MIX.HAIKI_UNIT_CD = R18MIX_MU.UNIT_CD AND R18MIX_MU.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_UNIT R18_MU ");
                        sql.Append(" ON R18.HAIKI_UNIT_CODE = R18_MU.UNIT_CD AND R18_MU.DELETE_FLG = 0 ");
                        // 排出事業者
                        sql.Append(" LEFT JOIN M_GYOUSHA HST_GYOUSHA1 ");
                        sql.Append(" ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA1.GYOUSHA_CD AND HST_GYOUSHA1.DELETE_FLG = 0 ");
                        // 排出事業者地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GYOUSHA_CHIIKI1 ");
                        sql.Append(" ON HST_GYOUSHA1.CHIIKI_CD = HST_GYOUSHA_CHIIKI1.CHIIKI_CD AND HST_GYOUSHA_CHIIKI1.DELETE_FLG = 0 ");
                        // 排出事業者業種
                        sql.Append(" LEFT JOIN M_GYOUSHU HST_GYOUSHA_GYOUSHU1 ");
                        sql.Append(" ON HST_GYOUSHA1.GYOUSHU_CD = HST_GYOUSHA_GYOUSHU1.GYOUSHU_CD AND HST_GYOUSHA_GYOUSHU1.DELETE_FLG = 0 ");
                        // 排出事業場
                        sql.Append(" LEFT JOIN M_GENBA HST_GENBA1 ");
                        sql.Append(" ON R18EX.HST_GYOUSHA_CD = HST_GENBA1.GYOUSHA_CD AND R18EX.HST_GENBA_CD = HST_GENBA1.GENBA_CD AND HST_GENBA1.DELETE_FLG = 0 ");
                        // 排出事業場地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GENBA_CHIIKI1 ");
                        sql.Append(" ON HST_GENBA1.CHIIKI_CD = HST_GENBA_CHIIKI1.CHIIKI_CD AND HST_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        // 排出事業場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI HST_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON HST_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = HST_GENBA_UPN_CHIIKI1.CHIIKI_CD AND HST_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        // 排出事業場業種
                        sql.Append(" LEFT JOIN M_GYOUSHU HST_GENBA_GYOUSHU1 ");
                        sql.Append(" ON HST_GENBA1.GYOUSHU_CD = HST_GENBA_GYOUSHU1.GYOUSHU_CD AND HST_GENBA_GYOUSHU1.DELETE_FLG = 0 ");
                        // 収集運搬1
                        sql.Append(" LEFT JOIN DT_R19 UPN1 ");
                        sql.Append(" ON R18.KANRI_ID = UPN1.KANRI_ID AND R18.SEQ = UPN1.SEQ ");
                        sql.Append(" AND UPN1.UPN_ROUTE_NO = 1 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX1 ");
                        sql.Append(" ON UPN1.KANRI_ID = UPN_EX1.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX1.SYSTEM_ID AND R18EX.SEQ = UPN_EX1.SEQ AND UPN_EX1.UPN_ROUTE_NO = 1 ");
                        sql.Append(" LEFT JOIN M_GYOUSHA UPN_GYOUSHA1 ");
                        sql.Append(" ON UPN_EX1.UPN_GYOUSHA_CD = UPN_GYOUSHA1.GYOUSHA_CD AND UPN_GYOUSHA1.DELETE_FLG = 0 ");
                        // 収集運搬2
                        sql.Append(" LEFT JOIN DT_R19 UPN2 ");
                        sql.Append(" ON R18.KANRI_ID = UPN2.KANRI_ID AND R18.SEQ = UPN2.SEQ ");
                        sql.Append(" AND UPN2.UPN_ROUTE_NO = 2 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX2 ");
                        sql.Append(" ON UPN2.KANRI_ID = UPN_EX2.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX2.SYSTEM_ID AND R18EX.SEQ = UPN_EX2.SEQ AND UPN_EX2.UPN_ROUTE_NO = 2 ");
                        // 収集運搬3
                        sql.Append(" LEFT JOIN DT_R19 UPN3 ");
                        sql.Append(" ON R18.KANRI_ID = UPN3.KANRI_ID AND R18.SEQ = UPN3.SEQ ");
                        sql.Append(" AND UPN3.UPN_ROUTE_NO = 3 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX3 ");
                        sql.Append(" ON UPN3.KANRI_ID = UPN_EX3.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX3.SYSTEM_ID AND R18EX.SEQ = UPN_EX3.SEQ AND UPN_EX3.UPN_ROUTE_NO = 3 ");
                        // 収集運搬4
                        sql.Append(" LEFT JOIN DT_R19 UPN4 ");
                        sql.Append(" ON R18.KANRI_ID = UPN4.KANRI_ID AND R18.SEQ = UPN4.SEQ ");
                        sql.Append(" AND UPN4.UPN_ROUTE_NO = 4 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX4 ");
                        sql.Append(" ON UPN4.KANRI_ID = UPN_EX4.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX4.SYSTEM_ID AND R18EX.SEQ = UPN_EX4.SEQ AND UPN_EX4.UPN_ROUTE_NO = 4 ");
                        // 収集運搬5
                        sql.Append(" LEFT JOIN DT_R19 UPN5 ");
                        sql.Append(" ON R18.KANRI_ID = UPN5.KANRI_ID AND R18.SEQ = UPN5.SEQ ");
                        sql.Append(" AND UPN5.UPN_ROUTE_NO = 5 ");
                        sql.Append(" LEFT JOIN DT_R19_EX UPN_EX5 ");
                        sql.Append(" ON UPN5.KANRI_ID = UPN_EX5.KANRI_ID ");
                        sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX5.SYSTEM_ID AND R18EX.SEQ = UPN_EX5.SEQ AND UPN_EX5.UPN_ROUTE_NO = 5 ");
                        // 積替保管現場
                        sql.Append(" LEFT JOIN M_GENBA UNPAN_GENBA1 ");
                        sql.Append(" ON UPN_EX1.UPNSAKI_GYOUSHA_CD = UNPAN_GENBA1.GYOUSHA_CD ");
                        sql.Append(" AND UPN_EX1.UPNSAKI_GENBA_CD = UNPAN_GENBA1.GENBA_CD AND UNPAN_GENBA1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UNPAN_GENBA2 ");
                        sql.Append(" ON UPN_EX2.UPNSAKI_GYOUSHA_CD = UNPAN_GENBA2.GYOUSHA_CD ");
                        sql.Append(" AND UPN_EX2.UPNSAKI_GENBA_CD = UNPAN_GENBA2.GENBA_CD AND UNPAN_GENBA2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UNPAN_GENBA3 ");
                        sql.Append(" ON UPN_EX3.UPNSAKI_GYOUSHA_CD = UNPAN_GENBA3.GYOUSHA_CD ");
                        sql.Append(" AND UPN_EX3.UPNSAKI_GENBA_CD = UNPAN_GENBA3.GENBA_CD AND UNPAN_GENBA3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_GENBA UNPAN_GENBA4 ");
                        sql.Append(" ON UPN_EX4.UPNSAKI_GYOUSHA_CD = UNPAN_GENBA4.GYOUSHA_CD ");
                        sql.Append(" AND UPN_EX4.UPNSAKI_GENBA_CD = UNPAN_GENBA4.GENBA_CD AND UNPAN_GENBA4.DELETE_FLG = 0 ");
                        // 積替保管現場地域
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_CHIIKI1 ");
                        sql.Append(" ON UNPAN_GENBA1.CHIIKI_CD = UNPAN_GENBA_CHIIKI1.CHIIKI_CD AND UNPAN_GENBA_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_CHIIKI2 ");
                        sql.Append(" ON UNPAN_GENBA2.CHIIKI_CD = UNPAN_GENBA_CHIIKI2.CHIIKI_CD AND UNPAN_GENBA_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_CHIIKI3 ");
                        sql.Append(" ON UNPAN_GENBA3.CHIIKI_CD = UNPAN_GENBA_CHIIKI3.CHIIKI_CD AND UNPAN_GENBA_CHIIKI3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_CHIIKI4 ");
                        sql.Append(" ON UNPAN_GENBA4.CHIIKI_CD = UNPAN_GENBA_CHIIKI4.CHIIKI_CD AND UNPAN_GENBA_CHIIKI4.DELETE_FLG = 0 ");
                        // 積替保管現場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_UPN_CHIIKI1 ");
                        sql.Append(" ON UNPAN_GENBA1.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UNPAN_GENBA_UPN_CHIIKI1.CHIIKI_CD ");
                        sql.Append(" AND UNPAN_GENBA_UPN_CHIIKI1.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_UPN_CHIIKI2 ");
                        sql.Append(" ON UNPAN_GENBA2.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UNPAN_GENBA_UPN_CHIIKI2.CHIIKI_CD ");
                        sql.Append(" AND UNPAN_GENBA_UPN_CHIIKI2.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_UPN_CHIIKI3 ");
                        sql.Append(" ON UNPAN_GENBA3.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UNPAN_GENBA_UPN_CHIIKI3.CHIIKI_CD ");
                        sql.Append(" AND UNPAN_GENBA_UPN_CHIIKI3.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN M_CHIIKI UNPAN_GENBA_UPN_CHIIKI4 ");
                        sql.Append(" ON UNPAN_GENBA4.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UNPAN_GENBA_UPN_CHIIKI4.CHIIKI_CD ");
                        sql.Append(" AND UNPAN_GENBA_UPN_CHIIKI4.DELETE_FLG = 0 ");
                        // マニ運搬情報（最終区間）
                        sql.Append(" LEFT JOIN DT_R19 DT_R19_LAST");
                        sql.Append(" ON DMT.KANRI_ID = DT_R19_LAST.KANRI_ID AND DMT.LATEST_SEQ = DT_R19_LAST.SEQ ");
                        sql.Append(" AND DT_R19_LAST.UPN_ROUTE_NO = (SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 R19 WHERE DMT.KANRI_ID = R19.KANRI_ID AND DMT.LATEST_SEQ = R19.SEQ) ");
                        // 一次排出事業者
                        sql.Append(" LEFT JOIN M_GYOUSHA FIRST_HST_GYOUSHA ");
                        sql.Append(" ON R18EX.HST_GYOUSHA_CD = FIRST_HST_GYOUSHA.GYOUSHA_CD ");
                        // 処分事業場
                        sql.Append(" LEFT JOIN M_GENBA SBN_GENBA ");
                        sql.Append(" ON R18EX.SBN_GYOUSHA_CD = SBN_GENBA.GYOUSHA_CD AND R18EX.SBN_GENBA_CD = SBN_GENBA.GENBA_CD AND SBN_GENBA.DELETE_FLG = 0 ");
                        // 処分事業場地域
                        sql.Append(" LEFT JOIN M_CHIIKI SBN_GENBA_CHIIKI ");
                        sql.Append(" ON SBN_GENBA.CHIIKI_CD = SBN_GENBA_CHIIKI.CHIIKI_CD AND SBN_GENBA_CHIIKI.DELETE_FLG = 0 ");
                        // 処分事業場運搬報告地域
                        sql.Append(" LEFT JOIN M_CHIIKI SBN_GENBA_UPN_CHIIKI ");
                        sql.Append(" ON SBN_GENBA.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = SBN_GENBA_UPN_CHIIKI.CHIIKI_CD AND SBN_GENBA_UPN_CHIIKI.DELETE_FLG = 0 ");
                        // 処分方法
                        sql.Append(" LEFT JOIN M_SHOBUN_HOUHOU DETAIL_SBN_HOUHOU ");
                        sql.Append(" ON R18EX.SBN_HOUHOU_CD = DETAIL_SBN_HOUHOU.SHOBUN_HOUHOU_CD AND DETAIL_SBN_HOUHOU.DELETE_FLG = 0 ");
                        sql.Append(" LEFT JOIN DT_R19 UPN");
                        sql.Append(" ON R18.KANRI_ID = UPN.KANRI_ID");
                        sql.Append(" AND R18.SEQ = UPN.SEQ");
                        sql.Append(" AND UPN.UPNSAKI_JOU_KBN IN (2,3,4)");

                        #endregion

                        #region WHERE

                        sql.Append(" WHERE (DMT.KIND = 4 or DMT.KIND = 5 or DMT.KIND IS NULL) ");
                        sql.Append(" AND (DMT.STATUS_FLAG = 3 OR DMT.STATUS_FLAG = 4) ");
                        sql.Append(" AND R18EX.DELETE_FLG = 0 ");
                        sql.Append(" AND ISNULL(R18.FIRST_MANIFEST_FLAG, '') <> '' ");
                        if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                        {
                            sql.Append(" AND ( ");
                            sql.AppendFormat(" UPN_EX1.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat(" OR UPN_EX2.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat(" OR UPN_EX3.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat(" OR UPN_EX4.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                            sql.AppendFormat(" OR UPN_EX5.UPN_GYOUSHA_CD = '{0}' ) ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                        }
                        if (!string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
                        {
                            sql.AppendFormat(" AND R18EX.SBN_GYOUSHA_CD = '{0}' ", this.form.cantxt_SyobunJyutakuNameCd.Text);
                        }

                        //処分事業場
                        if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
                        {

                            sql.AppendFormat(" AND R18EX.SBN_GENBA_CD = '{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                        }

                        if (this.form.KOUFU_DATE_KBN.Text != null)
                        {
                            //年月日（開始）
                            if (KOUFU_DATE_FROM != "" && this.form.KOUFU_DATE_KBN.Text != "2")
                            {
                                sql.Append(" AND ( ");

                                if (this.form.KOUFU_DATE_KBN.Text == "1")
                                {
                                    // 交付年月日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "3")
                                {
                                    // 処分終了日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.SBN_END_DATE) END) >= '{0}' ", KOUFU_DATE_FROM);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "4")
                                {
                                    // 最終処分終了日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.LAST_SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.LAST_SBN_END_DATE) END) >= '{0}' ", KOUFU_DATE_FROM);
                                }

                                sql.Append(" ) ");
                            }
                            //年月日（終了）
                            if (KOUFU_DATE_TO != "" && this.form.KOUFU_DATE_KBN.Text != "2")
                            {
                                sql.Append(" AND ( ");

                                if (this.form.KOUFU_DATE_KBN.Text == "1")
                                {
                                    // 交付年月日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ELSE NULL END) <= '{0}' ", KOUFU_DATE_TO);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "3")
                                {
                                    // 処分終了日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.SBN_END_DATE) END) <= '{0}' ", KOUFU_DATE_TO);
                                }
                                else if (this.form.KOUFU_DATE_KBN.Text == "4")
                                {
                                    // 最終処分終了日
                                    sql.AppendFormat(" (CASE WHEN ISDATE(R18.LAST_SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.LAST_SBN_END_DATE) END) <= '{0}' ", KOUFU_DATE_TO);
                                }

                                sql.Append(" ) ");
                            }

                            // 運搬終了日
                            if (this.form.KOUFU_DATE_KBN.Text == "2")
                            {
                                if (KOUFU_DATE_FROM != "" && KOUFU_DATE_TO != "")
                                {
                                    sql.Append(" AND ( ");
                                    if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                                    {
                                        sql.AppendFormat(" (  ( UPN_EX1.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN1.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR ( UPN_EX2.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN2.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR ( UPN_EX3.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN3.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR ( UPN_EX4.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN4.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR ( UPN_EX5.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                                        sql.AppendFormat(" AND UPN5.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) <= '{0}' )) ", KOUFU_DATE_TO);
                                    }
                                    else
                                    {
                                        sql.AppendFormat(" (  (UPN1.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN2.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN3.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN4.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                        sql.AppendFormat(" OR (UPN5.UPN_ROUTE_NO <= UPN.UPN_ROUTE_NO AND (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) >= '{0}' ", KOUFU_DATE_FROM);
                                        sql.AppendFormat("    AND (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) <= '{0}' )) ", KOUFU_DATE_TO);
                                    }
                                    sql.Append(" ) ");
                                }
                                else if (KOUFU_DATE_FROM != "")
                                {
                                    sql.Append(" AND ( ");
                                    sql.AppendFormat(" ( (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) >= '{0}' OR ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) >= '{0}' OR ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) >= '{0}' OR ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) >= '{0}' OR ", KOUFU_DATE_FROM);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) >= '{0}' ) ", KOUFU_DATE_FROM);
                                    sql.Append(" ) ");
                                }
                                else if (KOUFU_DATE_TO != "")
                                {
                                    sql.Append(" AND ( ");
                                    sql.AppendFormat(" ( (CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END) <= '{0}' OR ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN2.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN2.UPN_END_DATE) ELSE NULL END) <= '{0}' OR ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN3.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN3.UPN_END_DATE) ELSE NULL END) <= '{0}' OR ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN4.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN4.UPN_END_DATE) ELSE NULL END) <= '{0}' OR ", KOUFU_DATE_TO);
                                    sql.AppendFormat(" (CASE WHEN ISDATE(UPN5.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN5.UPN_END_DATE) ELSE NULL END) <= '{0}' ) ", KOUFU_DATE_TO);
                                    sql.Append(" ) ");
                                }
                            }
                        }

                        // 自社運搬
                        if (!string.IsNullOrEmpty(this.form.JISHA_UNPAN_KBN.Text))
                        {
                            if (this.form.JISHA_UNPAN_KBN.Text == "1")
                            {
                                sql.Append(" AND ISNULL(R18.HST_SHA_EDI_MEMBER_ID, '') = ISNULL(UPN1.UPN_SHA_EDI_MEMBER_ID, '') ");
                            }
                            else if (this.form.JISHA_UNPAN_KBN.Text == "2")
                            {
                                sql.Append(" AND ISNULL(R18.HST_SHA_EDI_MEMBER_ID, '') <> ISNULL(UPN1.UPN_SHA_EDI_MEMBER_ID, '') ");
                            }
                        }

                        // 処分方法
                        if (!string.IsNullOrEmpty(this.form.SHOBUN_HOUHOU_CD.Text))
                        {
                            sql.AppendFormat(" AND R18EX.SBN_HOUHOU_CD = '{0}' ", this.form.SHOBUN_HOUHOU_CD.Text);
                        }
                        else if (this.form.SHOBUN_HOUHOU_MI.Checked)
                        {
                            sql.Append(" AND (R18EX.SBN_HOUHOU_CD IS NULL OR R18EX.SBN_HOUHOU_CD = '') ");
                        }

                        // 報告書分類
                        if (!string.IsNullOrEmpty(this.form.cantxt_HokokushoBunrui.Text))
                        {
                            sql.Append(" AND ( CASE WHEN ISNULL(R18MIX.HAIKI_SHURUI_CD, '') = '' THEN R18_MDHS1.HOUKOKUSHO_BUNRUI_CD ");
                            sql.AppendFormat("   ELSE R18MIX_MDHS1.HOUKOKUSHO_BUNRUI_CD END ) = '{0}' ", this.form.cantxt_HokokushoBunrui.Text);
                        }

                        // 排出事業者
                        if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text))
                        {
                            sql.AppendFormat(" AND R18EX.HST_GYOUSHA_CD = '{0}' ", this.form.cantxt_HaisyutuGyousyaCd.Text);

                            if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuJigyoubaName.Text))
                            {
                                sql.AppendFormat(" AND R18EX.HST_GENBA_CD = '{0}' ", this.form.cantxt_HaisyutuJigyoubaName.Text);
                            }
                        }

                        #endregion

                        #endregion
                    }

                    #endregion

                }

                sql.Append(" ) AS SUMMARY ");
                sql.Append(this.joinQuery);

                #endregion

                #region WHERE句

                sql.AppendFormat(" WHERE 1 = 1");

                // 拠点
                if (!string.IsNullOrEmpty(this.header.KYOTEN_CD.Text)
                    && this.header.KYOTEN_CD.Text != "99")
                {
                    sql.AppendFormat(" AND ( SUMMARY.KYOTEN_CD = {0} OR SUMMARY.HAIKI_KBN_CD = 4) ", this.header.KYOTEN_CD.Text);
                }

                #endregion

                #region ORDER BY句

                if (!string.IsNullOrEmpty(this.orderByQuery))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(this.orderByQuery);
                }

                #endregion

                this.Search_TME = dao_GetTME.getdateforstringsql(sql.ToString());

                count = this.Search_TME.Rows.Count;

                return count;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        public void Set_Search_TME()
        {
            LogUtility.DebugMethodStart();

            //初期化
            this.form.customDataGridView1.DataSource = null;
            this.form.customDataGridView1.Rows.Clear();
            this.form.customDataGridView1.Columns.Clear();

            this.form.Table = this.Search_TME;

            //一覧へデータをセット
            this.form.ShowData();

            //読込データ件数
            this.header.ReadDataNumber.Text = this.Search_TME.Rows.Count.ToString();
            if (this.form.customDataGridView1 != null)
            {
                this.header.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.header.ReadDataNumber.Text = "0";
            }

            //フォーカス初期化
            if (this.form.customDataGridView1.Columns.Count > 0 && this.form.customDataGridView1.Rows.Count > 0)
            {
                this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[0, 0];

                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                if (this.form.customDataGridView1.Columns.Contains("マニフェスト数量"))
                {
                    this.form.customDataGridView1.Columns["マニフェスト数量"].DefaultCellStyle.Format = mSysInfo.MANIFEST_SUURYO_FORMAT;
                }
                if (this.form.customDataGridView1.Columns.Contains("運搬委託量"))
                {
                    this.form.customDataGridView1.Columns["運搬委託量"].DefaultCellStyle.Format = mSysInfo.MANIFEST_SUURYO_FORMAT;
                }
                if (this.form.customDataGridView1.Columns.Contains("処分受託量"))
                {
                    this.form.customDataGridView1.Columns["処分受託量"].DefaultCellStyle.Format = mSysInfo.MANIFEST_SUURYO_FORMAT;
                }
                if (this.form.customDataGridView1.Columns.Contains("引渡量"))
                {
                    this.form.customDataGridView1.Columns["引渡量"].DefaultCellStyle.Format = mSysInfo.MANIFEST_SUURYO_FORMAT;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        public bool FileOutput(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                LogUtility.Warn("出力対象データがありません。");
                this.MsgBox.MessageBoxShowWarn("対象データが無い為、出力を中止しました");
                return false;
            }

            #region ファイル名設定
            string fileFormat = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_MANIFEST_JISSEKI_ICHIRAN) + "_{0}.csv";
            string fileName = string.Format(fileFormat, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            var directoryName = string.Empty;
            using (var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder())
            {
                var title = "CSVファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;

                directoryName = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);
            }

            if (string.IsNullOrWhiteSpace(directoryName) || string.IsNullOrWhiteSpace(fileName))
            {
                LogUtility.Error("出力パスは空文字です。");
                return false;
            }

            // 最終出力ファイル名設定
            string fullName = Path.Combine(directoryName, fileName);
            if (!Directory.Exists(directoryName))
            {
                try
                {
                    Directory.CreateDirectory(directoryName);
                }
                catch (Exception ex)
                {
                    this.MsgBox.MessageBoxShowError("出力パスエラー、修正してくだしさい。");
                    return false;
                }
            }
            #endregion ファイル名設定

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");

            StreamWriter sw = null;
            try
            {
                // Create
                using (sw = new StreamWriter(fullName, false, encoding))
                {
                    var values = new List<string>();
                    string value;
                    // ヘッダ出力する
                    foreach (DataColumn head in dt.Columns)
                    {
                        if (head.ColumnName.Contains("HIDDEN"))
                        {
                            continue;
                        }
                        values.Add(head.ColumnName);
                    }

                    this.WriteCsvLine(sw, values);

                    // 行ループ
                    foreach (DataRow row in dt.Rows)
                    {
                        // 行編集
                        values = new List<string>();
                        foreach (DataColumn column in dt.Columns)
                        {
                            value = string.Empty;
                            var obj = row[column.ColumnName];

                            if (column.ColumnName.Contains("HIDDEN"))
                            {
                                continue;
                            }

                            if (column.ColumnName.Contains("年月日"))
                            {
                                if (obj != null)
                                {
                                    value = string.Format("{0:yyyy/MM/dd(ddd)}", obj);
                                }
                            }
                            else if (column.ColumnName.Equals("マニフェスト数量")
                                || column.ColumnName.Equals("運搬委託量")
                                || column.ColumnName.Equals("処分受託量")
                                || column.ColumnName.Equals("引渡量"))
                            {
                                if (obj != null)
                                {
                                    value = string.Format("{0:#,##0.####}", obj);
                                }
                            }
                            else
                            {
                                if (obj != null)
                                {
                                    value = obj.ToString();
                                }
                            }

                            values.Add(value);
                        }

                        // 行書込み
                        this.WriteCsvLine(sw, values);
                    }

                    sw.Close();

                    return true;
                }
            }
            finally
            {
                #region StreamWriter後処理

                if (sw != null)
                {
                    try
                    {
                        sw.Close();
                        sw.Dispose();
                    }
                    catch
                    {
                        // 処理なし
                    }
                    finally
                    {
                        sw = null;
                    }
                }

                #endregion StreamWriter後処理
            }
        }

        /// <summary>
        /// CSV行書き込み
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="values"></param>
        /// <remarks>valuesを(必要なら囲む)","連結して、CSVファイルに書き込む。</remarks>
        private void WriteCsvLine(StreamWriter sw, IEnumerable<string> values)
        {
            sw.WriteLine(string.Join(",", values.Select(
                x =>
                {
                    if (x.Contains('"') || x.Contains(',') || x.Contains('\r') || x.Contains('\n') ||
                        x.StartsWith(" ") || x.EndsWith(" ") ||
                    x.StartsWith("\t") || x.EndsWith("\t"))
                    {
                        if (x.Contains('"'))
                        {
                            // ["]を[""]とする
                            x = x.Replace("\"", "\"\"");
                        }
                        return "\"" + x + "\"";
                    }
                    else
                    {
                        return x;
                    }
                })));
        }
        #endregion

        #region 必須チェック
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal bool SearchCheck()
        {
            this.form.KOUFU_DATE_FROM.IsInputErrorOccured = false;
            this.form.KOUFU_DATE_TO.IsInputErrorOccured = false;
            var allControlAndHeaderControls = allControl.ToList();
            allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.header));
            var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (this.form.RegistErrorFlag)
            {
                //必須チェックエラーフォーカス処理
                this.SetErrorFocus();

                return true;
            }

            if (!this.form.RegistErrorFlag)
            {
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                if (string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text)
                    && string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
                {
                    this.form.cantxt_UnpanJyutakuNameCd.IsInputErrorOccured = true;
                    this.form.cantxt_SyobunJyutakuNameCd.IsInputErrorOccured = true;
                    msglogic.MessageBoxShowError("運搬受託者、または処分受託者は必須項目です。入力してください");
                    this.form.cantxt_UnpanJyutakuNameCd.Focus();
                    return true;
                }
                if (!string.IsNullOrEmpty(this.form.KOUFU_DATE_FROM.GetResultText())
                    && !string.IsNullOrEmpty(this.form.KOUFU_DATE_TO.GetResultText()))
                {
                    DateTime dtpFrom = DateTime.Parse(this.form.KOUFU_DATE_FROM.GetResultText());
                    DateTime dtpTo = DateTime.Parse(this.form.KOUFU_DATE_TO.GetResultText());
                    DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                    DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                    int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                    if (0 < diff)
                    {
                        //対象期間内でないならエラーメッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        this.form.KOUFU_DATE_FROM.IsInputErrorOccured = true;
                        this.form.KOUFU_DATE_TO.IsInputErrorOccured = true;
                        if (this.form.KOUFU_DATE_KBN.Text == "1")
                        {
                            string[] errorMsg = { "交付年月日From", "交付年月日To" };
                            msglogic.MessageBoxShow("E030", errorMsg);
                        }
                        else if (this.form.KOUFU_DATE_KBN.Text == "2")
                        {
                            string[] errorMsg = { "運搬終了日From", "運搬終了日To" };
                            msglogic.MessageBoxShow("E030", errorMsg);
                        }
                        else if (this.form.KOUFU_DATE_KBN.Text == "3")
                        {
                            string[] errorMsg = { "処分終了日From", "処分終了日To" };
                            msglogic.MessageBoxShow("E030", errorMsg);
                        }
                        else if (this.form.KOUFU_DATE_KBN.Text == "4")
                        {
                            string[] errorMsg = { "最終処分終了日From", "最終処分終了日To" };
                            msglogic.MessageBoxShow("E030", errorMsg);
                        }
                        this.form.KOUFU_DATE_FROM.Focus();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        private void SetErrorFocus()
        {
            Control target = null;
            foreach (Control control in this.form.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        if (target != null)
                        {
                            if (target.TabIndex > control.TabIndex)
                            {
                                target = control;
                            }
                        }
                        else
                        {
                            target = control;
                        }
                    }
                }
            }
            //ヘッダーチェック
            foreach (Control control in this.header.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        target = control;
                    }
                }
            }
            if (target != null)
            {
                target.Focus();
            }
        }
        #endregion

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOUFU_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var hidukeFromTextBox = this.form.KOUFU_DATE_FROM;
            var hidukeToTextBox = this.form.KOUFU_DATE_TO;
            hidukeToTextBox.Text = hidukeFromTextBox.Text;
        }
        #endregion

    }
}