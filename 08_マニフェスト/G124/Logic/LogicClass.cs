using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Report;
using Seasar.Framework.Exceptions;
using Shougun.Core.PaperManifest.ManifestCheckHyo.APP;
using Shougun.Core.Common.BusinessCommon.Xml;
using r_framework.Dao;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.ManifestCheckHyo
{
    /// <summary> 検索条件 </summary>
    public class JoukenParam
    {
        public string CkTaisyou { get; set; }       // チェック対象
        public string CkJouken { get; set; }        // チェック条件
        public string NengappiFrom { get; set; }    // 開始日付
        public string NengappiTo { get; set; }      // 終了日付
        public string CkBunrui { get; set; }        // チェック分類
        public string CkKyoten { get; set; }        // チェック拠点
        public bool CkDeleteFlg { get; set; }     // マスタ削除FLG
        // 20140623 ria EV004852 一覧と抽出条件の変更 start
        public bool[] CkItem { get; set; }        // チェック項目
        public bool CkYoyakuFlg { get; set; }     // 電マニ予約FLG
        public string CkUPNCD { get; set; }         // 運搬受託者CD
        public string CkSBNJCD { get; set; }        // 処分受託者CD
        public string CkSBNBCD { get; set; }        // 処分事業場CD
        public string CkChiikiCD { get; set; }      // チェック地域CD

        public JoukenParam()
        {
            CkItem = new bool[28];
        }
        // 20140623 ria EV004852 一覧と抽出条件の変更 end
    }

    /// <summary> ビジネスロジック </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>メッセージロジックオブジェクトを保持するフィールド</summary>
        private MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        #region - Fields -

        //private RibbonMainMenu ribbonForm;

        /// <summary> ボタン設定格納ファイル </summary>
        private string buttonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestCheckHyo.Setting.ButtonSetting.xml";

        /// <summary> DTO </summary>
        private DTOClass dto;
        private SerchCheckManiDtoCls serchCMDto;

        /// <summary> 帳票表示用会社名 </summary>
        private string corpName = string.Empty;

        /// 取引先請求情報のDao
        /// </summary>
        private IM_CORP_NAMEDaoCls corpNameDao;
        private CheckGyoushaMasterDaoCls checkGyoushaMasterDao;
        private CheckGenbaMasterDaoCls checkGenbaMasterDao;
        private CheckHaikiShuruiMasterDaoCls checkHaikiShuruiMasterDao;
        private CheckDenHaikiShuruiMasterDaoCls checkDenHaikiShuruiMasterDao;
        private CheckDenshiJigyoushaDaoCls checkDenshiJigyoushaDao;
        private CheckDenshiJigyoujouDaoCls checkDenshiJigyoujouDao;
        private CheckChiikibetsuGyoushuDaoCls checkChiikibetsuGyoushu;
        private CheckChiikibetsuShisetsuDaoCls checkChiikibetsuShisetsu;
        private CheckChiikibetsuJuushoDaoCls checkChiikibetsujuusho;
        private CheckChiikibetsuShobunDaoCls checkChiikibetsuShobun;
        private CheckChiikibetsuBunruiDaoCls checkChiikibetsuBunrui;
        private CheckManifestDaoCls checkManifest;
        private IM_GYOUSHADao daoGyousha;
        private IM_GENBADao daoGenba;

        //初期表示フラグ
        private bool InitialFlg = false;

        #endregion - Constructors -

        #region - Constructors -

        /// <summary> コンストラクタ </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.Form = targetForm;
            this.dto = new DTOClass();
            this.serchCMDto = new SerchCheckManiDtoCls();
            this.corpNameDao = DaoInitUtility.GetComponent<IM_CORP_NAMEDaoCls>();
            this.checkGyoushaMasterDao = DaoInitUtility.GetComponent<CheckGyoushaMasterDaoCls>();
            this.checkGenbaMasterDao = DaoInitUtility.GetComponent<CheckGenbaMasterDaoCls>();
            this.checkHaikiShuruiMasterDao = DaoInitUtility.GetComponent<CheckHaikiShuruiMasterDaoCls>();
            this.checkDenHaikiShuruiMasterDao = DaoInitUtility.GetComponent<CheckDenHaikiShuruiMasterDaoCls>();
            this.checkDenshiJigyoushaDao = DaoInitUtility.GetComponent<CheckDenshiJigyoushaDaoCls>();
            this.checkDenshiJigyoujouDao = DaoInitUtility.GetComponent<CheckDenshiJigyoujouDaoCls>();
            this.checkChiikibetsuGyoushu = DaoInitUtility.GetComponent<CheckChiikibetsuGyoushuDaoCls>();
            this.checkChiikibetsuShisetsu = DaoInitUtility.GetComponent<CheckChiikibetsuShisetsuDaoCls>();
            this.checkChiikibetsujuusho = DaoInitUtility.GetComponent<CheckChiikibetsuJuushoDaoCls>();
            this.checkChiikibetsuShobun = DaoInitUtility.GetComponent<CheckChiikibetsuShobunDaoCls>();
            this.checkChiikibetsuBunrui = DaoInitUtility.GetComponent<CheckChiikibetsuBunruiDaoCls>();
            this.checkManifest = DaoInitUtility.GetComponent<CheckManifestDaoCls>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary> マニフェストチェック表のForm </summary>
        public UIForm Form { get; set; }

        /// <summary> マニフェストチェック表のHeaderForm</summary>
        public UIHeader Headerform { get; set; }

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm Parentbaseform { get; set; }

        /// <summary> 検索条件 </summary>
        public JoukenParam JoukenParam { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary> 論理削除処理 </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary> 物理削除処理 </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary> 登録処理 </summary>
        /// <param name="errorFlag"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary> 検索処理 </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary> 更新処理 </summary>
        /// <param name="errorFlag"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary> グリッド表示用にデータを取得、加工する </summary>
        /// <returns></returns>
        internal DataTable MakeGridData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnTable = new DataTable();

            if (this.JoukenParam.CkTaisyou == "1")
            {
                // マニフェスト
                // マニフェスト時の加工処理をおこなう
                returnTable = this.MakeManiCheckData(this.JoukenParam);
            }
            else if (this.JoukenParam.CkTaisyou == "2")
            {
                // マスタ
                // マスタ時の加工処理をおこなう
                returnTable = this.MakeMasterCheckData(this.JoukenParam);
            }

            LogUtility.DebugMethodEnd(returnTable);

            return returnTable;
        }

        /// <summary> 表示初期処理 </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // フォームインスタンスを取得
                this.Parentbaseform = (BusinessBaseForm)this.Form.Parent;
                this.Headerform = (UIHeader)this.Parentbaseform.headerForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.Headerform.lb_title.Text = "マニフェストチェック表";

                this.MakeCustumDataGridView();  // カスタムグリッドのカラム作成

                this.corpName = this.GetCorpName();

                this.JoukenParam = new ManifestCheckHyo.JoukenParam();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary> 条件指定ポップアップ </summary>
        /// <param name="param">param</param>
        internal bool ShowPopUp(JoukenParam param)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(param);

                // 再検索時、初期値を設定する
                param = this.CreateParams();

                // 売上範囲条件指定画面表示
                var popUpForm = new JoukenPopupForm(param);
                popUpForm.ShowDialog();

                // 子画面で入力された条件をセット
                this.JoukenParam = popUpForm.Joken;

                // 実行結果
                switch (popUpForm.DialogResult)
                {
                    case DialogResult.OK:

                        DataTable dt = new DataTable();
                        dt = this.MakeGridData();

                        this.CustumGridViewSetting();                   // グリッドの設定
                        this.Form.customDataGridView1.IsBrowsePurpose = false;
                        this.Form.customDataGridView1.DataSource = dt;  // グリッドのデータソースを指定

                        if (this.Form.customDataGridView1.Rows.Count < 1)
                        {
                            this.msgLogic.MessageBoxShow("C001");
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkTaisyou == "1")
                        {
                            this.Form.customDataGridView1.Columns["Hidden_Haiki_Kbn"].Visible = false;
                            this.Form.customDataGridView1.Columns["Hidden_Mani_Id"].Visible = false;
                            this.Form.customDataGridView1.Columns["Hidden_System_Id"].Visible = false;
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        this.Form.customDataGridView1.IsBrowsePurpose = true;
                        this.Form.customDataGridView1.Refresh();        // 画面再描画

                        break;

                    case DialogResult.Cancel:
                        // 何もしない
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowPopUp", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary> 印刷ボタン押下時処理 </summary>
        internal void Func5()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable printData = new DataTable();

                if (this.Form.customDataGridView1.RowCount == 0)
                {
                    // 出力する該当データがありません。
                    msgLogic.MessageBoxShow("E044", new string[] { string.Empty });

                    return;
                }

                if (this.JoukenParam.CkTaisyou == "1")
                {
                    // マニフェスト
                    // マニフェスト時の印刷データ作成処理をおこなう
                    printData = this.MekeManiPrintData();
                }
                else if (this.JoukenParam.CkTaisyou == "2")
                {
                    // マスタ
                    // マスタ時の印刷データ作成処理をおこなう
                    printData = this.MekeMasterPrintData();
                }

                ReportInfoR389 report_r389 = new ReportInfoR389();

                //report_r389.OutputFormLayout = "LAYOUT2";
                // 画面側から渡ってきた帳票用データテーブルを引数へ設定
                report_r389.R389_Report(printData);

                // ファイル名、レイアウトを変更する場合はここで設定する
                //report_r382.OutputFormFullPathName = "R382_R387-Form.xml";
                //report_r382.OutputFormLayout = "LAYOUT1";

                // 印刷ポツプアップ画面表示
                using (FormReportPrintPopup report = new FormReportPrintPopup(report_r389))
                {

                    //レポートタイトルの設定
                    if (this.JoukenParam.CkTaisyou == "1")
                    {
                        // マニフェスト
                        report.ReportCaption = "マニフェストチェック表";
                    }
                    else if (this.JoukenParam.CkTaisyou == "2")
                    {
                        // マスタ
                        report.ReportCaption = "マスタチェック表";
                    }

                    report.ShowDialog();
                    report.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func5", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        ///// <summary> CSV出力ボタン押下時処理 </summary>
        //internal void Func6()
        //{
        //    LogUtility.DebugMethodStart();

        //    MessageBox.Show("CSV出力");

        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary> マニフェストチェックデータの情報を取得する </summary>
        /// <param name="joukenParam">joukenParam:検索条件</param>
        /// <returns>マニフェストチェックデータ</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private DataTable MakeManiCheckData(JoukenParam joukenParam)
        {
            //検索処理中　カーソルを砂時計に変更
            Cursor.Current = Cursors.WaitCursor;

            LogUtility.DebugMethodStart(joukenParam);

            DataTable returnTable = new DataTable();

            // 条件を設定する
            this.serchCMDto.BUNRUI = joukenParam.CkBunrui;
            // 20140623 ria EV004852 一覧と抽出条件の変更 start
            if (joukenParam.CkBunrui == "3")
            {
                this.serchCMDto.BUNRUI = "2";
            }
            else if (joukenParam.CkBunrui == "2")
            {
                this.serchCMDto.BUNRUI = "3";
            }
            // 20140623 ria EV004852 一覧と抽出条件の変更 end
            this.serchCMDto.DATE_START = joukenParam.NengappiFrom;
            this.serchCMDto.DATE_END = joukenParam.NengappiTo;
            this.serchCMDto.JOUKEN = joukenParam.CkJouken;
            this.serchCMDto.KYOTEN = joukenParam.CkKyoten;
            this.serchCMDto.YOYAKU_FLG = joukenParam.CkYoyakuFlg;
            this.serchCMDto.UPN_CD = joukenParam.CkUPNCD;
            this.serchCMDto.SBNJ_CD = joukenParam.CkSBNJCD;
            this.serchCMDto.SBNB_CD = joukenParam.CkSBNBCD;
            this.serchCMDto.CHIIKI_CD = joukenParam.CkChiikiCD;

            // 取得データ格納用データテーブル
            DataTable getDataTable;
            // 一時格納テーブル
            DataTable tempDataTable = new DataTable();
            tempDataTable.Columns.Add("FIRST_MANIFEST_KBN");
            tempDataTable.Columns.Add("HAIKI_KBN_CD");
            tempDataTable.Columns.Add("HAIKI_KBN_NAME");
            tempDataTable.Columns.Add("MANIFEST_ID");
            tempDataTable.Columns.Add("SYSTEM_ID");
            tempDataTable.Columns.Add("CK_KOUMOKU");

            // 20140623 ria EV004852 一覧と抽出条件の変更 start
            //// チェック分類が3(電子マニフェスト)以外
            //if (joukenParam.CkBunrui != "3")
            // チェック分類が4(電子マニフェスト)以外
            if (joukenParam.CkBunrui != "4")
            // 20140623 ria EV004852 一覧と抽出条件の変更 end
            {
                // 20140626 ria EV004852 一覧と抽出条件の変更 start
                #region -マニフェスト交付番号チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[1])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiKofuBangouCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -マニフェスト交付年月日チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[0])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiKofuNengappiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -マニフェスト排出事業者CDチェックデータの取得および加工-
                if (this.JoukenParam.CkItem[2])
                {
                    //// マニフェスト
                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //getDataTable = this.checkManifest.GetManiHaishutsuJigyoushaCDCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}

                    // 業者マスタ
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiHaishutsuJigyoushaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiHaishutsuJigyoushaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ 業者分類（排出事業者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiHaishutsuJigyoushaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -マニフェスト排出事業場CDチェックデータの取得および加工-
                if (this.JoukenParam.CkItem[3])
                {
                    //// マニフェスト
                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //getDataTable = this.checkManifest.GetManiHaishutsuJigyoujouCDCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}

                    // 現場マスタ
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiHaishutsuJigyoujouCDGenbaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 現場マスタ_排出事業場CD（現場分類）
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiHaishutsuJigyoujouCDGenbaMCheckGenbaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -マニフェスト廃棄物種類CDチェックデータの取得および加工-
                if (this.JoukenParam.CkItem[4])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiHaikiButsuShuruiCDCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}
                }
                #endregion

                #region -マニフェスト換算後数量チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[7])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiKansangoSuuryouCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -マニフェスト処分方法CDチェックデータの取得および加工-
                if (this.JoukenParam.CkItem[8])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiShobunHouhouCDCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    // チェックデータを取得する
                    getDataTable = this.checkManifest.GetManiChiikiShobunHouhouCDCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                // 20140623 ria EV004852 一覧と抽出条件の変更 start
                if (this.JoukenParam.CkItem[5])
                {
                    #region -数量データの取得および加工-
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：61
                    getDataTable = this.checkManifest.GetManiSuuryouCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                    #endregion

                #region -単位CDデータの取得および加工-
                if (this.JoukenParam.CkItem[6])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：71
                    getDataTable = this.checkManifest.GetManiTaniCDCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion
                // 20140623 ria EV004852 一覧と抽出条件の変更 end

                #region -マニフェスト運搬受託者CDチェックデータの取得および加工-
                if (this.JoukenParam.CkItem[9])
                {
                    //// マニフェスト
                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //getDataTable = this.checkManifest.GetManiUnpanJutakushaCDCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}

                    // 業者マスタ
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "1";
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanJutakushaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanJutakushaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ 業者分類（運搬受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanJutakushaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト処分受託者CDチェックデータの取得および加工-
                if (this.JoukenParam.CkItem[10])
                {
                    //// マニフェスト
                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //getDataTable = this.checkManifest.GetManiShobunJutakushaCDCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}

                    // 業者マスタ
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiShobunJutakushaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiShobunJutakushaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ 業者分類（処分受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiShobunJutakushaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -マニフェスト処分事業場CDチェックデータの取得および加工-
                if (this.JoukenParam.CkItem[11])
                {
                    //// マニフェスト
                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //getDataTable = this.checkManifest.GetManiShobunJigyoujouCDCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}

                    // 業者マスタ
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "1";
                    getDataTable = new DataTable();
                    if (this.serchCMDto.BUNRUI != "2")
                    {
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMCheckData(this.serchCMDto);
                    }
                    else
                    {
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMKenpaiCheckData(this.serchCMDto);
                    }
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    if (this.serchCMDto.BUNRUI == "5")
                    {
                        getDataTable = new DataTable();
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMKenpaiCheckData(this.serchCMDto);
                        if (getDataTable.Rows.Count != 0)
                        {
                            tempDataTable = this.AddRow(getDataTable, tempDataTable);
                        }
                    }

                    // 業者マスタ 現場分類（運搬先事業場）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    if (this.serchCMDto.BUNRUI != "2")
                    {
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMCheckGenbaBunrui(this.serchCMDto);
                    }
                    else
                    {
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMKenpaiCheckGenbaBunrui(this.serchCMDto);
                    }
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    if (this.serchCMDto.BUNRUI == "5")
                    {
                        getDataTable = new DataTable();
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMKenpaiCheckGenbaBunrui(this.serchCMDto);
                        if (getDataTable.Rows.Count != 0)
                        {
                            tempDataTable = this.AddRow(getDataTable, tempDataTable);
                        }
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region 処分施設有無（積替）
                if (this.JoukenParam.CkItem[27])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiShobunShisetuTumikaeCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }

                #endregion

                #region -マニフェスト運搬終了年月日チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[12])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "1";
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanShuuryouNengappiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト処分終了年月日チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[13])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiShobunShuuryouNengappiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -マニフェスト最終処分終了年月日チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[14])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiSaishuuShobunShuuryouNengappiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -交付年月日なしデータの取得および加工-
                // チェックデータを取得する
                getDataTable = new DataTable();
                // 表示順SEQ：210
                getDataTable = this.checkManifest.GetManiKoufuNengappiNashiCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                }
                #endregion

                #region -紐付不整合(紙)データの取得および加工-
                // チェックデータを取得する
                getDataTable = new DataTable();
                getDataTable = this.checkManifest.GetHimodukeFuseigouKamiCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                }
                #endregion
                // 20140626 ria EV004852 一覧と抽出条件の変更 end

                #region -マニフェスト運搬受託者CD（区間2）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[15])
                {
                    // 業者マスタ
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "2";
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanJutakushaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanJutakushaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ 業者分類（運搬受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanJutakushaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬先の事業場CD（区間2）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[16])
                {
                    // 業者マスタ
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "2";
                    getDataTable = new DataTable();
                    if (this.serchCMDto.BUNRUI != "2")
                    {
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMCheckData(this.serchCMDto);
                    }
                    else
                    {
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMKenpaiCheckData(this.serchCMDto);
                    }
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    if (this.serchCMDto.BUNRUI == "5")
                    {
                        getDataTable = new DataTable();
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMKenpaiCheckData(this.serchCMDto);
                        if (getDataTable.Rows.Count != 0)
                        {
                            tempDataTable = this.AddRow(getDataTable, tempDataTable);
                        }
                    }

                    // 業者マスタ 現場分類（運搬先事業場）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    if (this.serchCMDto.BUNRUI != "2")
                    {
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMCheckGenbaBunrui(this.serchCMDto);
                    }
                    else
                    {
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMKenpaiCheckGenbaBunrui(this.serchCMDto);
                    }
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    if (this.serchCMDto.BUNRUI == "5")
                    {
                        getDataTable = new DataTable();
                        getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMKenpaiCheckGenbaBunrui(this.serchCMDto);
                        if (getDataTable.Rows.Count != 0)
                        {
                            tempDataTable = this.AddRow(getDataTable, tempDataTable);
                        }
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬終了年月日（区間2）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[17])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "2";
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanShuuryouNengappiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬受託者CD（区間3）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[18])
                {
                    // 業者マスタ
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "3";
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanJutakushaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanJutakushaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ 業者分類（運搬受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanJutakushaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬先の事業場CD（区間2）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[19])
                {
                    // 業者マスタ
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "3";
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 業者マスタ 現場分類（運搬先事業場）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiShobunJigyoujouCDGyoushaMCheckGenbaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬終了年月日（区間3）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[20])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "3";
                    getDataTable = new DataTable();
                    getDataTable = this.checkManifest.GetManiUnpanShuuryouNengappiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion
            }

            // 20140623 ria EV004852 一覧と抽出条件の変更 start
            //// チェック分類が2(紙マニフェスト)以外
            //if (joukenParam.CkBunrui != "2")
            // チェック分類が1、2(産廃)と3(建廃)以外
            if (joukenParam.CkBunrui != "1" && joukenParam.CkBunrui != "2" && joukenParam.CkBunrui != "3")
            // 20140623 ria EV004852 一覧と抽出条件の変更 end
            {
                // 20140626 ria EV004852 一覧と抽出条件の変更 start
                #region -マニフェスト／予約番号チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[1])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：10
                    getDataTable = this.checkManifest.GetDenManiManiIDCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -引き渡し日チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[0])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：20
                    getDataTable = this.checkManifest.GetDenManiHikiwatashiBiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                if (this.JoukenParam.CkItem[2])
                {
                    #region -排出事業者CD(マニフェスト)データの取得および加工-

                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //// 表示順SEQ：30
                    //getDataTable = this.checkManifest.GetDenManiHaishutuJigyoushaCDManiCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}
                    #endregion

                    #region -排出事業者CD(業者マスタ)データの取得および加工-
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：31
                    getDataTable = this.checkManifest.GetDenManiHaishutuJigyoushaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 排出事業者_マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：31
                    getDataTable = this.checkManifest.GetDenManiHaishutuJigyoushaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 排出事業者_業者分類（排出事業者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：31
                    getDataTable = this.checkManifest.GetDenManiHaishutuJigyoushaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    #endregion
                }

                if (this.JoukenParam.CkItem[3])
                {
                    //#region -排出事業場CD(マニフェスト)データの取得および加工-
                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //// 表示順SEQ：40
                    //getDataTable = this.checkManifest.GetDenManiHaishutuJigyoujouCDManiCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}
                    //#endregion

                    #region -排出事業場CD(現場マスタ)データの取得および加工-
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：41
                    getDataTable = this.checkManifest.GetDenManiHaishutuJigyoujouCDGenbaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 排出事業場_現場分類（排出事業場）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：41
                    getDataTable = this.checkManifest.GetDenManiHaishutuJigyoujouCDGenbaMCheckGenbaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    #endregion
                }

                if (this.JoukenParam.CkItem[4])
                {
                    #region -廃棄物種類(大分類)データの取得および加工-
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：51
                    getDataTable = this.checkManifest.GetDenManiHaikibutuShuruiDaiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    #endregion

                    #region -廃棄物種類(中分類)データの取得および加工-
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：52
                    getDataTable = this.checkManifest.GetDenManiHaikibutuShuruiChuCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    #endregion

                    #region -廃棄物種類(小分類)データの取得および加工-
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：53
                    getDataTable = this.checkManifest.GetDenManiHaikibutuShuruiShoCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    #endregion

                    #region -廃棄物種類(細分類)データの取得および加工-
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：54
                    getDataTable = this.checkManifest.GetDenManiHaikibutuShuruiSaiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    #endregion
                }

                #region -換算後数量データの取得および加工-
                if (this.JoukenParam.CkItem[7])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：60
                    getDataTable = this.checkManifest.GetDenManiKansanGoSuuryouCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -処分方法CDデータの取得および加工-
                if (this.JoukenParam.CkItem[8])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：70
                    getDataTable = this.checkManifest.GetDenManiShobunHouhouCDCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    //地域処分方法CDのチェック
                    getDataTable = this.checkManifest.GetDenManiChiikiShobunHouhouCDCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                // 20140623 ria EV004852 一覧と抽出条件の変更 start
                #region -数量データの取得および加工-
                if (this.JoukenParam.CkItem[5])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：61
                    getDataTable = this.checkManifest.GetDenManiSuuryouCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region 単位CDデータの取得および加工-
                if (this.JoukenParam.CkItem[6])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：71
                    getDataTable = this.checkManifest.GetDenManiTaniCDCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion
                // 20140623 ria EV004852 一覧と抽出条件の変更 end

                if (this.JoukenParam.CkItem[9])
                {
                    //#region -収集運搬業者CD(マニフェスト)データの取得および加工-
                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //// 表示順SEQ：80
                    //getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDManiCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}
                    //#endregion

                    #region -収集運搬業者CD(業者マスタ)データの取得および加工-
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "1";
                    getDataTable = new DataTable();
                    // 表示順SEQ：81
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間１）_マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：81
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間１）_業者分類（運搬受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：81
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                    #endregion
                }

                if (this.JoukenParam.CkItem[10])
                {
                    //#region -処分受託者CD(マニフェスト)データの取得および加工-
                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //// 表示順SEQ：90
                    //getDataTable = this.checkManifest.GetDenManiShobunJutakushaCDManiCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}
                    //#endregion

                    #region -処分受託者CD(業者マスタ)データの取得および加工-
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：91
                    getDataTable = this.checkManifest.GetDenManiShobunJutakushaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 処分受託者_マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：91
                    getDataTable = this.checkManifest.GetDenManiShobunJutakushaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 処分受託者_業者分類（処分受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：91
                    getDataTable = this.checkManifest.GetDenManiShobunJutakushaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    #endregion
                }

                if (this.JoukenParam.CkItem[11])
                {
                    //#region -処分事業場CD(マニフェスト)データの取得および加工-
                    //// チェックデータを取得する
                    //getDataTable = new DataTable();
                    //// 表示順SEQ：100
                    //getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDManiCheckData(this.serchCMDto);
                    //// 該当レコードの取得ができたら
                    //if (getDataTable.Rows.Count != 0)
                    //{
                    //    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    //}
                    //#endregion

                    #region -処分事業場CD(業者マスタ)データの取得および加工-
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "1";
                    getDataTable = new DataTable();
                    // 表示順SEQ：101
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬先の事業場CD_現場分類（運搬先事業場）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：101
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckGenbaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                    #endregion
                }

                #region -運搬終了日データの取得および加工-
                if (this.JoukenParam.CkItem[12])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "1";
                    getDataTable = new DataTable();
                    // 表示順SEQ：110
                    getDataTable = this.checkManifest.GetDenManiUnpanshuuryouBiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -処分終了日データの取得および加工-
                if (this.JoukenParam.CkItem[13])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：120
                    getDataTable = this.checkManifest.GetDenManiShobunshuuryouBiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -最終処分終了日データの取得および加工-
                if (this.JoukenParam.CkItem[14])
                {
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：130
                    getDataTable = this.checkManifest.GetDenManiSaishuuShobunshuuryouBiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                }
                #endregion

                #region -マニフェスト運搬受託者CD（区間2）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[15])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "2";
                    getDataTable = new DataTable();
                    // 表示順SEQ：92
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間２）_マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：92
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間２）_業者分類（運搬受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：92
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬先の事業場CD（区間2）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[16])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "2";
                    getDataTable = new DataTable();
                    // 表示順SEQ：102
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬先の事業場CD_現場分類（運搬先事業場）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：102
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckGenbaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -運搬終了日（区間2）データの取得および加工-
                if (this.JoukenParam.CkItem[17])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "2";
                    getDataTable = new DataTable();
                    // 表示順SEQ：111
                    getDataTable = this.checkManifest.GetDenManiUnpanshuuryouBiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬受託者CD（区間3）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[18])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "3";
                    getDataTable = new DataTable();
                    // 表示順SEQ：93
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間３）_マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：81
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間３）_業者分類（運搬受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：81
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬先の事業場CD（区間3）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[19])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "3";
                    getDataTable = new DataTable();
                    // 表示順SEQ：103
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬先の事業場CD_現場分類（運搬先事業場）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：103
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckGenbaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -運搬終了日（区間3）データの取得および加工-
                if (this.JoukenParam.CkItem[20])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "3";
                    getDataTable = new DataTable();
                    // 表示順SEQ：112
                    getDataTable = this.checkManifest.GetDenManiUnpanshuuryouBiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬受託者CD（区間4）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[21])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "4";
                    getDataTable = new DataTable();
                    // 表示順SEQ：94
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間４）_マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：81
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間４）_業者分類（運搬受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：81
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬先の事業場CD（区間4）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[22])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "4";
                    getDataTable = new DataTable();
                    // 表示順SEQ：104
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬先の事業場CD_現場分類（運搬先事業場）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：104
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckGenbaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -運搬終了日（区間4）データの取得および加工-
                if (this.JoukenParam.CkItem[23])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "4";
                    getDataTable = new DataTable();
                    // 表示順SEQ：113
                    getDataTable = this.checkManifest.GetDenManiUnpanshuuryouBiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬受託者CD（区間5）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[24])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "5";
                    getDataTable = new DataTable();
                    // 表示順SEQ：95
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間５）_マニ記載業者チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：81
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckManiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬受託者（区間５）_業者分類（運搬受託者）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：81
                    getDataTable = this.checkManifest.GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckGyoushaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -マニフェスト運搬先の事業場CD（区間5）チェックデータの取得および加工-
                if (this.JoukenParam.CkItem[25])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "5";
                    getDataTable = new DataTable();
                    // 表示順SEQ：105
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }

                    // 運搬先の事業場CD_現場分類（運搬先事業場）チェック
                    // チェックデータを取得する
                    getDataTable = new DataTable();
                    // 表示順SEQ：105
                    getDataTable = this.checkManifest.GetDenManiShobunJigyoujouCDGembaMCheckGenbaBunrui(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -運搬終了日（区間5）データの取得および加工-
                if (this.JoukenParam.CkItem[26])
                {
                    // チェックデータを取得する
                    this.serchCMDto.UPN_ROUTE_NO = "5";
                    getDataTable = new DataTable();
                    // 表示順SEQ：114
                    getDataTable = this.checkManifest.GetDenManiUnpanshuuryouBiCheckData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        tempDataTable = this.AddRow(getDataTable, tempDataTable);
                    }
                    this.serchCMDto.UPN_ROUTE_NO = null;
                }
                #endregion

                #region -交付年月日なしデータの取得および加工-
                // チェックデータを取得する
                getDataTable = new DataTable();
                // 表示順SEQ：211
                getDataTable = this.checkManifest.GetDenManiKoufuNengappiNashiCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                }
                #endregion

                #region -紐付不整合(電子)データの取得および加工-
                // チェックデータを取得する
                getDataTable = new DataTable();
                // 表示順SEQ：310
                getDataTable = this.checkManifest.GetHimodukeFuseigouDenshiCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    tempDataTable = this.AddRow(getDataTable, tempDataTable);
                }
                #endregion
                // 20140626 ria EV004852 一覧と抽出条件の変更 end
            }

            // 画面表示用にデータを加工する
            returnTable = this.MakeDispData(tempDataTable);

            LogUtility.DebugMethodEnd(returnTable);

            return returnTable;
        }

        /// <summary> 表示用にデータを加工する </summary>
        /// <param name="tempTable">tempTable</param>
        /// <returns></returns>
        private DataTable MakeDispData(DataTable tempTable)
        {
            LogUtility.DebugMethodStart(tempTable);

            DataTable returnTable = new DataTable();
            returnTable.Columns.Add("Mani_Kbn");
            returnTable.Columns.Add("Haiki_Kbn");
            returnTable.Columns.Add("Kofu_Bangou");
            returnTable.Columns.Add("Check_Kmk");
            // 20140623 ria EV004852 一覧と抽出条件の変更 start
            returnTable.Columns.Add("Hidden_Haiki_Kbn");
            returnTable.Columns.Add("Hidden_Mani_Id");
            // 20140623 ria EV004852 一覧と抽出条件の変更 end
            returnTable.Columns.Add("Hidden_System_Id");
            DataRow retDataRow;

            string maniKbn = string.Empty;
            string haikiKbn = string.Empty;
            string maniId = string.Empty;
            string systemId = string.Empty;
            string checkKoumoku = string.Empty;
            //string zenCheckKoumoku = string.Empty;

            int rowCnt = 1;

            // tempTablをソートする
            DataRow[] selectedRows = tempTable.Select(string.Empty, "FIRST_MANIFEST_KBN, HAIKI_KBN_CD,MANIFEST_ID,SYSTEM_ID,CK_KOUMOKU");
            // 項目を日本語にする
            foreach (DataRow row in selectedRows)
            {
                maniKbn = row["FIRST_MANIFEST_KBN"].ToString();
                haikiKbn = row["HAIKI_KBN_CD"].ToString();
                maniId = row["MANIFEST_ID"].ToString();
                systemId = row["SYSTEM_ID"].ToString();

                #region -チェック項目編集-
                switch (row["CK_KOUMOKU"].ToString())
                {
                    case "10":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[1])
                        {
                            checkKoumoku += "交付番号,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "11":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[1])
                        {
                            checkKoumoku += "マニフェスト／予約番号,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "20":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[0])
                        {
                            checkKoumoku += "交付年月日,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "21":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[0])
                        {
                            checkKoumoku += "引渡し日,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "30":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        //checkKoumoku += "排出事業者,";
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "31":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[2])
                        {
                            checkKoumoku += "排出事業者CD,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "31_2":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[2])
                        {
                            checkKoumoku += "排出事業者CD（マニ記載）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "31_3":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[2])
                        {
                            checkKoumoku += "排出事業者CD（業者分類）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "40":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        //checkKoumoku += "排出事業場,";
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "41":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[3])
                        {
                            checkKoumoku += "排出事業場CD,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "41_2":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[3])
                        {
                            checkKoumoku += "排出事業場CD（現場分類）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "50":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[4])
                        {
                            checkKoumoku += "廃棄物種類,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "51":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[4])
                        {
                            checkKoumoku += "大分類,";
                        }
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "52":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[4])
                        {
                            checkKoumoku += "中分類,";
                        }
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "53":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[4])
                        {
                            checkKoumoku += "小分類,";
                        }
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "54":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[4])
                        {
                            checkKoumoku += "細分類,";
                        }
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "60":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[7])
                        {
                            checkKoumoku += "換算後数量,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "70":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[8])
                        {
                            checkKoumoku += "処分方法,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "70_2":
                        if (this.JoukenParam.CkItem[8])
                        {
                            checkKoumoku += "（地域別処分）処分方法,";
                        }
                        break;

                    // 20140623 ria EV004852 一覧と抽出条件の変更 start
                    case "61":
                        if (this.JoukenParam.CkItem[5])
                        {
                            checkKoumoku += "数量,";
                        }
                        break;

                    case "71":
                        if (this.JoukenParam.CkItem[6])
                        {
                            checkKoumoku += "単位CD,";
                        }
                        break;
                    // 20140623 ria EV004852 一覧と抽出条件の変更 end

                    case "80":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        //checkKoumoku += "運搬受託者,";
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "81":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[9])
                        {
                            checkKoumoku += "運搬受託者CD（区間1）,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "81_2":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[9])
                        {
                            checkKoumoku += "運搬受託者CD(区間１)（マニ記載）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "81_3":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[9])
                        {
                            checkKoumoku += "運搬受託者CD(区間１)（業者分類）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "82":
                        checkKoumoku += "収集運搬業者,";
                        break;

                    case "90":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        //checkKoumoku += "処分受託者,";
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "91":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[10])
                        {
                            checkKoumoku += "処分受託者CD,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "91_2":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[10])
                        {
                            checkKoumoku += "処分受託者CD（マニ記載）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "91_3":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[10])
                        {
                            checkKoumoku += "処分受託者CD（業者分類）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "92":
                        if (this.JoukenParam.CkItem[15])
                        {
                            checkKoumoku += "運搬受託者CD（区間2）,";
                        }
                        break;

                    case "92_2":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[15])
                        {
                            checkKoumoku += "運搬受託者CD(区間２)（マニ記載）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "92_3":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[9])
                        {
                            checkKoumoku += "運搬受託者CD(区間２)（業者分類）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "93":
                        if (this.JoukenParam.CkItem[18])
                        {
                            checkKoumoku += "運搬受託者CD（区間3）,";
                        }
                        break;

                    case "93_2":
                        if (this.JoukenParam.CkItem[18])
                        {
                            checkKoumoku += "運搬受託者CD(区間３)（マニ記載）,";
                        }
                        break;

                    case "93_3":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[9])
                        {
                            checkKoumoku += "運搬受託者CD(区間３)（業者分類）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "94":
                        if (this.JoukenParam.CkItem[21])
                        {
                            checkKoumoku += "運搬受託者CD（区間4）,";
                        }
                        break;

                    case "94_2":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[9])
                        {
                            checkKoumoku += "運搬受託者CD(区間４)（マニ記載）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "94_3":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[9])
                        {
                            checkKoumoku += "運搬受託者CD(区間４)（業者分類）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "95":
                        if (this.JoukenParam.CkItem[24])
                        {
                            checkKoumoku += "運搬受託者CD（区間5）,";
                        }
                        break;

                    case "95_2":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[9])
                        {
                            checkKoumoku += "運搬受託者CD(区間５)（マニ記載）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "95_3":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[9])
                        {
                            checkKoumoku += "運搬受託者CD(区間５)（業者分類）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "100":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        //checkKoumoku += "処分事業場,";
                        //checkKoumoku += "運搬先の事業場,";
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "101":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        //checkKoumoku += "処分事業場CD,";
                        if (this.JoukenParam.CkItem[11])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間1）,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "101_2":
                        // 20181026 wangxh マニフェストチェック表 #120313 start
                        if (this.JoukenParam.CkItem[11])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間１）（現場分類）,";
                        }
                        // 20181026 wangxh マニフェストチェック表 #120313 end
                        break;

                    case "102":
                        if (this.JoukenParam.CkItem[16])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間2）,";
                        }
                        break;

                    case "102_2":
                        if (this.JoukenParam.CkItem[16])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間２）（現場分類）,";
                        }
                        break;

                    case "103":
                        if (this.JoukenParam.CkItem[19])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間3）,";
                        }
                        break;

                    case "103_2":
                        if (this.JoukenParam.CkItem[19])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間３）（現場分類）,";
                        }
                        break;

                    case "104":
                        if (this.JoukenParam.CkItem[22])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間4）,";
                        }
                        break;

                    case "104_2":
                        if (this.JoukenParam.CkItem[22])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間４）（現場分類）,";
                        }
                        break;

                    case "105":
                        if (this.JoukenParam.CkItem[25])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間5）,";
                        }
                        break;

                    case "105_2":
                        if (this.JoukenParam.CkItem[22])
                        {
                            checkKoumoku += "運搬先の事業場CD（区間５）（現場分類）,";
                        }
                        break;

                    case "106":
                        if (this.JoukenParam.CkItem[27])
                        {
                            checkKoumoku += "処分施設,";
                        }
                        break;

                    case "110":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        //checkKoumoku += "運搬終了年月日,";
                        if (this.JoukenParam.CkItem[12])
                        {
                            checkKoumoku += "運搬終了日（区間1）,";
                        }
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "111":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[17])
                        {
                            checkKoumoku += "運搬終了日（区間2）,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "112":
                        if (this.JoukenParam.CkItem[20])
                        {
                            checkKoumoku += "運搬終了日（区間3）,";
                        }
                        break;

                    case "113":
                        if (this.JoukenParam.CkItem[23])
                        {
                            checkKoumoku += "運搬終了日（区間4）,";
                        }
                        break;

                    case "114":
                        if (this.JoukenParam.CkItem[26])
                        {
                            checkKoumoku += "運搬終了日（区間5）,";
                        }
                        break;

                    case "120":
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        //checkKoumoku += "処分終了年月日,";
                        if (this.JoukenParam.CkItem[13])
                        {
                            checkKoumoku += "処分終了日,";
                        }
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "121":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[13])
                        {
                            checkKoumoku += "処分終了日,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "130":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        //checkKoumoku += "最終処分年月日,";
                        if (this.JoukenParam.CkItem[14])
                        {
                            checkKoumoku += "最終処分終了日,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "131":
                        // 20140623 ria EV004852 一覧と抽出条件の変更 start
                        if (this.JoukenParam.CkItem[14])
                        {
                            checkKoumoku += "最終処分終了日,";
                        }
                        // 20140623 ria EV004852 一覧と抽出条件の変更 end
                        break;

                    case "210":
                        checkKoumoku += "交付年月日,";
                        break;

                    case "211":
                        checkKoumoku += "引渡し日,";
                        break;

                    case "310":
                        checkKoumoku += "紐付不整合,";
                        break;

                    default:
                        break;
                }
                #endregion

                // 最終行である、または、次の行の(マニ区分、廃棄物区分、交付番号)が異なる場合に表示用データテーブルに追加
                if ((tempTable.Rows.Count == rowCnt)
                    || ((maniKbn != selectedRows[rowCnt]["FIRST_MANIFEST_KBN"].ToString())
                       || (haikiKbn != selectedRows[rowCnt]["HAIKI_KBN_CD"].ToString())
                       || (maniId != selectedRows[rowCnt]["MANIFEST_ID"].ToString())
                       || (systemId != selectedRows[rowCnt]["SYSTEM_ID"].ToString())))
                {
                    retDataRow = returnTable.NewRow();

                    switch (maniKbn)
                    {
                        case "0":
                            retDataRow["Mani_Kbn"] = "一次マニフェスト";
                            break;

                        case "1":
                            retDataRow["Mani_Kbn"] = "二次マニフェスト";
                            break;

                        default:
                            break;
                    }

                    retDataRow["Haiki_Kbn"] = row["HAIKI_KBN_NAME"].ToString();
                    retDataRow["Kofu_Bangou"] = maniId;

                    // 20140623 ria EV004852 一覧と抽出条件の変更 start
                    retDataRow["Hidden_Haiki_Kbn"] = haikiKbn;
                    retDataRow["Hidden_Mani_Id"] = maniId;
                    // 20140623 ria EV004852 一覧と抽出条件の変更 end
                    retDataRow["Hidden_System_Id"] = systemId;

                    if (checkKoumoku.Trim().Length > 0)
                    {
                        // チェック項目の最後の文字(カンマ)を削除
                        checkKoumoku = checkKoumoku.Trim().Remove(checkKoumoku.Length - 1);
                        // チェック項目の規定文字数(７０文字)以上を切り捨てる
                        // 20140626 ria EV004852 一覧と抽出条件の変更 start
                        //if (checkKoumoku.Length > 70)
                        //{
                        //    checkKoumoku = checkKoumoku.Trim().Remove(70);
                        //}
                        // 20140626 ria EV004852 一覧と抽出条件の変更 end
                    }

                    retDataRow["Check_Kmk"] = checkKoumoku;
                    returnTable.Rows.Add(retDataRow);

                    maniKbn = string.Empty;
                    haikiKbn = string.Empty;
                    maniId = string.Empty;
                    systemId = string.Empty;
                    checkKoumoku = string.Empty;
                }
                rowCnt++;
            }

            LogUtility.DebugMethodEnd(returnTable);

            return returnTable;
        }

        /// <summary> 取得したデータを一時テーブルに追加する </summary>
        /// <param name="getDataTable">getDataTable：取得したデータ</param>
        /// <param name="tempDataTable">tempDataTable：一時テーブル</param>
        /// <returns>一時テーブル</returns>
        private DataTable AddRow(DataTable getDataTable, DataTable tempDataTable)
        {
            LogUtility.DebugMethodStart(tempDataTable, tempDataTable);

            foreach (DataRow dr in getDataTable.Rows)
            {
                tempDataTable.ImportRow(dr);
            }

            LogUtility.DebugMethodEnd(tempDataTable);

            return tempDataTable;
        }

        /// <summary> マスタチェックデータの情報を取得する </summary>
        /// <param name="joukenParam">joukenParam:検索条件</param>
        /// <returns>マスタチェックデータ</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private DataTable MakeMasterCheckData(JoukenParam joukenParam)
        {
            LogUtility.DebugMethodStart(joukenParam);

            // 返却用データテーブル
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add("Masuta_Syurui");
            returnTable.Columns.Add("Code");
            returnTable.Columns.Add("Meisyou");
            returnTable.Columns.Add("Check_Kmk");
            DataRow retDr;

            // 条件を設定する
            this.serchCMDto.BUNRUI = joukenParam.CkBunrui;
            this.serchCMDto.DATE_START = joukenParam.NengappiFrom;
            this.serchCMDto.DATE_END = joukenParam.NengappiTo;
            this.serchCMDto.JOUKEN = joukenParam.CkJouken;
            this.serchCMDto.DELETE_FLG = joukenParam.CkDeleteFlg;

            // 取得データ格納用データテーブル
            DataTable getDataTable;

            // 20140623 ria EV004852 一覧と抽出条件の変更 start
            //// チェック分類が3(電子のみ)以外
            //if (joukenParam.CkBunrui != "3")
            // チェック分類が4(電子のみ)以外
            if (joukenParam.CkBunrui != "4")
            // 20140623 ria EV004852 一覧と抽出条件の変更 end
            {
                #region -業者マスタチェックデータの取得および加工-
                // 現場マスタチェックデータを取得する
                getDataTable = new DataTable();
                getDataTable = this.checkGyoushaMasterDao.GetGyoushaMasterCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "業者";
                        retDr["Code"] = dr["GYOUSHA_CD"];
                        retDr["Meisyou"] = dr["GYOUSHA_NAME_RYAKU"];
                        retDr["Check_Kmk"] = "地域CD";
                        returnTable.Rows.Add(retDr);
                    }
                }

                getDataTable = new DataTable();
                getDataTable = this.checkGyoushaMasterDao.GetGyoushaMasterCheckDataU(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "業者";
                        retDr["Code"] = dr["GYOUSHA_CD"];
                        retDr["Meisyou"] = dr["GYOUSHA_NAME_RYAKU"];
                        retDr["Check_Kmk"] = "運搬報告書提出先";
                        returnTable.Rows.Add(retDr);
                    }
                }
                #endregion

                #region -現場マスタチェックデータの取得および加工-
                // 現場マスタチェックデータを取得する
                getDataTable = new DataTable();
                getDataTable = this.checkGenbaMasterDao.GetGenbaMasterCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "現場";
                        retDr["Code"] = dr["GYOUSHA_CD"].ToString() + '、' + dr["GENBA_CD"].ToString();
                        retDr["Meisyou"] = dr["GENBA_NAME_RYAKU"];
                        retDr["Check_Kmk"] = "地域CD";
                        returnTable.Rows.Add(retDr);
                    }
                }

                getDataTable = new DataTable();
                getDataTable = this.checkGenbaMasterDao.GetGenbaMasterCheckDataU(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "現場";
                        retDr["Code"] = dr["GYOUSHA_CD"].ToString() + '、' + dr["GENBA_CD"].ToString();
                        retDr["Meisyou"] = dr["GENBA_NAME_RYAKU"];
                        retDr["Check_Kmk"] = "運搬報告書提出先";
                        returnTable.Rows.Add(retDr);
                    }
                }

                // 現場マスタ_地域妥当性チェック
                getDataTable = new DataTable();
                getDataTable = this.checkGenbaMasterDao.GetGenbaChiikiDatouseiCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "現場";
                        retDr["Code"] = dr["GYOUSHA_CD"].ToString() + '、' + dr["GENBA_CD"].ToString();
                        retDr["Meisyou"] = dr["GENBA_NAME_RYAKU"];
                        retDr["Check_Kmk"] = "現場（都道府県CD・地域CD）";
                        returnTable.Rows.Add(retDr);
                    }
                }

                // 現場_運搬報告書提出先妥当性チェック
                getDataTable = new DataTable();
                getDataTable = this.checkGenbaMasterDao.GetGenbaUnpanHoukokushoDatouseiCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "現場";
                        retDr["Code"] = dr["GYOUSHA_CD"].ToString() + '、' + dr["GENBA_CD"].ToString();
                        retDr["Meisyou"] = dr["GENBA_NAME_RYAKU"];
                        retDr["Check_Kmk"] = "現場（都道府県CD・運搬報告書提出先CD）";
                        returnTable.Rows.Add(retDr);
                    }
                }
                #endregion
            }

            #region -廃棄物種類マスタチェックデータの取得および加工-
            // 廃棄物種類マスタチェックデータを取得する
            getDataTable = new DataTable();
            getDataTable = this.checkHaikiShuruiMasterDao.GetHaikiShuruiCheckData(this.serchCMDto);
            // 該当レコードの取得ができたら
            if (getDataTable.Rows.Count != 0)
            {
                // 取得したデータを表示用に加工する
                foreach (DataRow dr in getDataTable.Rows)
                {
                    retDr = returnTable.NewRow();
                    retDr["Masuta_Syurui"] = "廃棄物種類";
                    retDr["Code"] = dr["HAIKI_KBN_CD"].ToString() + '、' + dr["HAIKI_SHURUI_CD"].ToString();
                    retDr["Meisyou"] = dr["HAIKI_SHURUI_NAME_RYAKU"];
                    retDr["Check_Kmk"] = "報告書分類CD";
                    returnTable.Rows.Add(retDr);
                }
            }
            #endregion

            #region -電子廃棄物種類マスタチェックデータの取得および加工-
            // 電子廃棄物種類マスタチェックデータを取得する
            getDataTable = new DataTable();
            getDataTable = this.checkDenHaikiShuruiMasterDao.GetDenHaikiShuruiCheckData(this.serchCMDto);
            // 該当レコードの取得ができたら
            if (getDataTable.Rows.Count != 0)
            {
                // 取得したデータを表示用に加工する
                foreach (DataRow dr in getDataTable.Rows)
                {
                    retDr = returnTable.NewRow();
                    retDr["Masuta_Syurui"] = "電子廃棄物種類";
                    retDr["Code"] = dr["HAIKI_SHURUI_CD"];
                    retDr["Meisyou"] = dr["HAIKI_SHURUI_NAME"];
                    retDr["Check_Kmk"] = "報告書分類CD";
                    returnTable.Rows.Add(retDr);
                }
            }
            #endregion

            // 20140623 ria EV004852 一覧と抽出条件の変更 start
            //// チェック分類が2(紙のみ)以外
            //if (joukenParam.CkBunrui != "2")
            // チェック分類が1、2(産廃)と3(建廃)以外
            if (joukenParam.CkBunrui != "1" && joukenParam.CkBunrui != "2" && joukenParam.CkBunrui != "3")
            // 20140623 ria EV004852 一覧と抽出条件の変更 end
            {
                #region -電子事業者マスタチェックデータの取得および加工-
                // 電子事業者マスタチェックデータを取得する
                getDataTable = new DataTable();
                getDataTable = this.checkDenshiJigyoushaDao.GetDenshiJigyoushaCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "電子事業者";
                        retDr["Code"] = dr["EDI_MEMBER_ID"];
                        retDr["Meisyou"] = dr["JIGYOUSHA_NAME"];
                        retDr["Check_Kmk"] = "将軍連携業者CD";
                        returnTable.Rows.Add(retDr);
                    }
                }
                #endregion

                #region -電子事業場マスタチェックデータの取得および加工-
                // 電子事業場マスタチェックデータを取得する
                getDataTable = new DataTable();
                getDataTable = this.checkDenshiJigyoujouDao.GetDenshiJigyoujouCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "電子事業場";
                        retDr["Code"] = dr["EDI_MEMBER_ID"].ToString() + '、' + dr["JIGYOUJOU_CD"].ToString();
                        retDr["Meisyou"] = dr["JIGYOUJOU_NAME"];
                        retDr["Check_Kmk"] = "将軍連携現場CD";
                        returnTable.Rows.Add(retDr);
                    }
                }

                // 連携事業場_地域妥当性チェックデータを取得する
                getDataTable = new DataTable();
                getDataTable = this.checkDenshiJigyoujouDao.GetDenshiJigyoujouChiikiCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "電子事業場";
                        retDr["Code"] = dr["EDI_MEMBER_ID"].ToString() + '、' + dr["JIGYOUJOU_CD"].ToString();
                        retDr["Meisyou"] = dr["JIGYOUJOU_NAME"];
                        retDr["Check_Kmk"] = "事業場（都道府県・将軍連携　業者　（業者CD、現場CD）の地域CD）";
                        returnTable.Rows.Add(retDr);
                    }
                }

                // 連携事業場_地域妥当性チェックデータを取得する
                getDataTable = new DataTable();
                getDataTable = this.checkDenshiJigyoujouDao.GetDenshiJigyoujouUnpanHoukokushoCheckData(this.serchCMDto);
                // 該当レコードの取得ができたら
                if (getDataTable.Rows.Count != 0)
                {
                    // 取得したデータを表示用に加工する
                    foreach (DataRow dr in getDataTable.Rows)
                    {
                        retDr = returnTable.NewRow();
                        retDr["Masuta_Syurui"] = "電子事業場";
                        retDr["Code"] = dr["EDI_MEMBER_ID"].ToString() + '、' + dr["JIGYOUJOU_CD"].ToString();
                        retDr["Meisyou"] = dr["JIGYOUJOU_NAME"];
                        retDr["Check_Kmk"] = "事業場（都道府県・将軍連携　業者　（業者CD、現場CD）の運搬報告書提出先CD）";
                        returnTable.Rows.Add(retDr);
                    }
                }
                #endregion
            }

            #region -地域別業種マスタチェックデータの取得および加工-
            // 地域別業種マスタチェックデータを取得する
            getDataTable = new DataTable();
            getDataTable = this.checkChiikibetsuGyoushu.GetChiikiBetsuGyoushuCheckData(this.serchCMDto);
            // 該当レコードの取得ができたら
            if (getDataTable.Rows.Count != 0)
            {
                // 取得したデータを表示用に加工する
                foreach (DataRow dr in getDataTable.Rows)
                {
                    retDr = returnTable.NewRow();
                    retDr["Masuta_Syurui"] = "地域別業種";

                    if (dr["CHIIKI_CD"].ToString() == string.Empty)
                    {
                        retDr["Code"] = dr["GYOUSHU_CD"].ToString();
                    }
                    else if (dr["GYOUSHU_CD"].ToString() == string.Empty)
                    {
                        retDr["Code"] = dr["CHIIKI_CD"].ToString();
                    }
                    else if (dr["GYOUSHU_CD"].ToString() != string.Empty && dr["CHIIKI_CD"].ToString() != string.Empty)
                    {
                        retDr["Code"] = dr["CHIIKI_CD"].ToString() + '、' + dr["GYOUSHU_CD"].ToString();
                    }
                    else
                    {
                        retDr["Code"] = string.Empty;
                    }

                    retDr["Meisyou"] = dr["NAME_RYAKU"];
                    retDr["Check_Kmk"] = "地域CD・業種CD";
                    returnTable.Rows.Add(retDr);
                }
            }
            #endregion

            #region -地域別施設マスタチェックデータの取得および加工-
            // 地域別施設マスタチェックデータを取得する
            getDataTable = new DataTable();
            getDataTable = this.checkChiikibetsuShisetsu.GetChiikiBetsuShisetsuCheckData(this.serchCMDto);
            // 該当レコードの取得ができたら
            if (getDataTable.Rows.Count != 0)
            {
                // 取得したデータを表示用に加工する
                foreach (DataRow dr in getDataTable.Rows)
                {
                    retDr = returnTable.NewRow();
                    retDr["Masuta_Syurui"] = "地域別施設";
                    retDr["Code"] = dr["CHIIKI_CD"].ToString();
                    retDr["Meisyou"] = dr["NAME_RYAKU"];
                    retDr["Check_Kmk"] = "地域CD";
                    returnTable.Rows.Add(retDr);
                }
            }
            #endregion

            #region -地域別住所マスタチェックデータの取得および加工-
            // 地域別住所マスタチェックデータを取得する
            getDataTable = new DataTable();
            getDataTable = this.checkChiikibetsujuusho.GetChiikiBetsuJuushoCheckData(this.serchCMDto);
            // 該当レコードの取得ができたら
            if (getDataTable.Rows.Count != 0)
            {
                // 取得したデータを表示用に加工する
                foreach (DataRow dr in getDataTable.Rows)
                {
                    retDr = returnTable.NewRow();
                    retDr["Masuta_Syurui"] = "地域別住所";
                    retDr["Code"] = dr["CHIIKI_CD"].ToString();
                    retDr["Meisyou"] = dr["NAME_RYAKU"];
                    retDr["Check_Kmk"] = "地域CD";
                    returnTable.Rows.Add(retDr);
                }
            }
            #endregion

            #region -地域別処分マスタチェックデータの取得および加工-
            // 地域別処分マスタチェックデータを取得する
            getDataTable = new DataTable();
            getDataTable = this.checkChiikibetsuShobun.GetChiikiBetsuShobunCheckData(this.serchCMDto);
            // 該当レコードの取得ができたら
            if (getDataTable.Rows.Count != 0)
            {
                // 取得したデータを表示用に加工する
                foreach (DataRow dr in getDataTable.Rows)
                {
                    retDr = returnTable.NewRow();
                    retDr["Masuta_Syurui"] = "地域別処分";
                    retDr["Code"] = dr["CHIIKI_CD"].ToString();
                    retDr["Meisyou"] = dr["NAME_RYAKU"];
                    retDr["Check_Kmk"] = "地域CD";
                    returnTable.Rows.Add(retDr);
                }
            }
            #endregion

            #region -地域別分類マスタチェックデータの取得および加工-
            // 地域別分類マスタチェックデータを取得する
            getDataTable = new DataTable();
            getDataTable = this.checkChiikibetsuBunrui.GetChiikiBetsuBunruiCheckData(this.serchCMDto);
            // 該当レコードの取得ができたら
            if (getDataTable.Rows.Count != 0)
            {
                // 取得したデータを表示用に加工する
                foreach (DataRow dr in getDataTable.Rows)
                {
                    retDr = returnTable.NewRow();
                    retDr["Masuta_Syurui"] = "地域別分類";
                    retDr["Code"] = dr["CHIIKI_CD"].ToString();
                    retDr["Meisyou"] = dr["NAME_RYAKU"];
                    retDr["Check_Kmk"] = "地域CD";
                    returnTable.Rows.Add(retDr);
                }
            }
            #endregion

            LogUtility.DebugMethodEnd(returnTable);

            return returnTable;
        }

        /// <summary> チェック対象により、グリッドのカラムのプロパティを変える </summary>
        private void CustumGridViewSetting()
        {
            LogUtility.DebugMethodStart();

            if (this.JoukenParam.CkTaisyou == "1")
            {
                // マニフェストチェック表

                // マニフェスト区分
                this.Form.customDataGridView1.Columns[0].Width = 180;                       // カラム幅
                this.Form.customDataGridView1.Columns[0].HeaderText = "マニフェスト区分";   // ヘッダーテキスト
                this.Form.customDataGridView1.Columns[0].DataPropertyName = "Mani_Kbn";     // データソースとの関連付け

                // 廃棄物区分
                this.Form.customDataGridView1.Columns[1].Width = 120;                       // カラム幅
                this.Form.customDataGridView1.Columns[1].HeaderText = "廃棄物区分";         // ヘッダーテキスト
                this.Form.customDataGridView1.Columns[1].DataPropertyName = "Haiki_Kbn";    // データソースとの関連付け

                // 交付番号
                this.Form.customDataGridView1.Columns[2].Width = 100;                       // カラム幅
                this.Form.customDataGridView1.Columns[2].HeaderText = "交付番号";           // ヘッダーテキスト
                this.Form.customDataGridView1.Columns[2].DataPropertyName = "Kofu_Bangou";  // データソースとの関連付け

                // 交付番号
                this.Form.customDataGridView1.Columns[3].Width = 1000;                      // カラム幅
                this.Form.customDataGridView1.Columns[3].HeaderText = "チェック項目";       // ヘッダーテキスト
                this.Form.customDataGridView1.Columns[3].DataPropertyName = "Check_Kmk";    // データソースとの関連付け
            }
            else if (this.JoukenParam.CkTaisyou == "2")
            {
                // データチェック表

                // マスタ種類
                this.Form.customDataGridView1.Columns[0].Name = "Masuta_Syurui";
                this.Form.customDataGridView1.Columns[0].Width = 171;                           // カラム幅
                this.Form.customDataGridView1.Columns[0].HeaderText = "マスタ種類";             // ヘッダーテキスト
                this.Form.customDataGridView1.Columns[0].DataPropertyName = "Masuta_Syurui";    // データソースとの関連付け

                // 廃棄物区分
                this.Form.customDataGridView1.Columns[1].Name = "Code";
                this.Form.customDataGridView1.Columns[1].Width = 188;                           // カラム幅
                this.Form.customDataGridView1.Columns[1].HeaderText = "コード";                 // ヘッダーテキスト
                this.Form.customDataGridView1.Columns[1].DataPropertyName = "Code";             // データソースとの関連付け

                // 名称
                this.Form.customDataGridView1.Columns[2].Name = "Meisyou";
                this.Form.customDataGridView1.Columns[2].Width = 400;                           // カラム幅
                this.Form.customDataGridView1.Columns[2].HeaderText = "名称";                   // ヘッダーテキスト
                this.Form.customDataGridView1.Columns[2].DataPropertyName = "Meisyou";          // データソースとの関連付け

                // 交付番号
                this.Form.customDataGridView1.Columns[3].Name = "Check_Kmk";
                this.Form.customDataGridView1.Columns[3].Width = 560;                           // カラム幅
                this.Form.customDataGridView1.Columns[3].HeaderText = "チェック項目";           // ヘッダーテキスト
                this.Form.customDataGridView1.Columns[3].DataPropertyName = "Check_Kmk";        // データソースとの関連付け
            }

            this.Form.customDataGridView1.Refresh();

            LogUtility.DebugMethodEnd();
        }

        /// <summary> 条件指定用パラメータデフォルト値設定 </summary>
        /// <returns></returns>
        private JoukenParam CreateParams()
        {
            LogUtility.DebugMethodStart();

            var info = new JoukenParam();
            if (!InitialFlg)
            {
                // 初期設定条件を設定        
                info.CkTaisyou = "1";                           // チェック対象
                info.CkJouken = "1";                            // チェック条件
                info.NengappiFrom = this.Parentbaseform.sysDate.ToString();    // 開始日付
                info.NengappiTo = this.Parentbaseform.sysDate.ToString();      // 終了日付
                // 20140623 ria EV004852 一覧と抽出条件の変更 start
                //info.CkBunrui = "1";                            // チェック分類
                info.CkBunrui = "5";                            // チェック分類
                for (int i = 0; i < 15; i++)
                {
                    info.CkItem[i] = true;                      // チェック項目
                }
                // 20140623 ria EV004852 一覧と抽出条件の変更 end
                info.CkItem[27] = true;                      // チェック項目

                // XMLから拠点CDを取得
                const string XML_KYOTEN_CD_KEY_NAME = "拠点CD";
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                info.CkKyoten = this.GetUserProfileValue(userProfile, XML_KYOTEN_CD_KEY_NAME);
                if (!string.IsNullOrEmpty(info.CkKyoten))
                {
                    info.CkKyoten = info.CkKyoten.PadLeft(info.CkKyoten.Length, '0');
                }
                info.CkChiikiCD = null; //チェック地域CD
                InitialFlg = true;
            }
            else
            {
                info = this.JoukenParam;
            }
            LogUtility.DebugMethodEnd(info);
            return info;
        }

        /// <summary> マニフェスト時の印刷データ作成 </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private DataTable MekeManiPrintData()
        {
            LogUtility.DebugMethodStart();

            string data = string.Empty;
            //string maniKbn = string.Empty;
            string maniKbn = " ";
            string haikiKbn = string.Empty;
            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            DataRow dr;
            dr = returnTable.NewRow();

            data = "\"0-1\",\"1\",\"" + this.corpName + "\"";

            dr[0] = data;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.Form.customDataGridView1.DataSource;

            foreach (DataRow row in gridData.Rows)
            {
                if (row["Mani_Kbn"].ToString() != maniKbn)
                {
                    maniKbn = row["Mani_Kbn"].ToString();
                    dr = returnTable.NewRow();
                    data = "\"1-1\",\"";
                    data += maniKbn + "\"";
                    dr[0] = data;
                    returnTable.Rows.Add(dr);
                }

                if (row["Haiki_Kbn"].ToString() != haikiKbn)
                {
                    maniKbn = row["Haiki_Kbn"].ToString() + "マニフェスト";
                    dr = returnTable.NewRow();
                    data = "\"1-2\",\"";
                    data += maniKbn + "\"";
                    dr[0] = data;
                    returnTable.Rows.Add(dr);
                }

                dr = returnTable.NewRow();
                data = "\"1-3\",\"";
                data += row["Kofu_Bangou"].ToString() + "\",\"";
                data += row["Check_Kmk"].ToString() + "\"";
                dr[0] = data;
                returnTable.Rows.Add(dr);
            }

            LogUtility.DebugMethodEnd(returnTable);

            //検索終了　カーソルを砂時計から元に変更
            Cursor.Current = Cursors.Default;

            return returnTable;
        }

        /// <summary> マスタ時の印刷データ作成 </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private DataTable MekeMasterPrintData()
        {
            LogUtility.DebugMethodStart();

            string data = string.Empty;
            string maniKbn = string.Empty;
            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            DataRow dr;
            dr = returnTable.NewRow();

            data = "\"0-1\",\"2\",\"" + this.corpName + "\"";

            dr[0] = data;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.Form.customDataGridView1.DataSource;

            foreach (DataRow row in gridData.Rows)
            {
                if (row["Masuta_Syurui"].ToString() != maniKbn)
                {
                    maniKbn = row["Masuta_Syurui"].ToString();
                    dr = returnTable.NewRow();
                    data = "\"1-1\",\"" + maniKbn + "\"";
                    dr[0] = data;
                    returnTable.Rows.Add(dr);
                }

                dr = returnTable.NewRow();
                data = "\"1-2\",\"" + row["Code"].ToString() + "\",\"";
                data += row["Meisyou"].ToString() + "\",\"";
                data += row["Check_Kmk"].ToString() + "\"";
                dr[0] = data;
                returnTable.Rows.Add(dr);
            }

            LogUtility.DebugMethodEnd(returnTable);

            return returnTable;
        }

        /// <summary> 会社名を取得 </summary>
        /// <returns></returns>
        private string GetCorpName()
        {
            LogUtility.DebugMethodStart();

            string corpName = string.Empty;

            corpName = this.corpNameDao.GetCorpName();

            LogUtility.DebugMethodEnd(corpName);

            return corpName;
        }

        /// <summary> カスタムグリッドのカラム作成する </summary>
        /// <remarks> 他のプロパティはチェック対象により動的に変わるため別途設定する</remarks>
        private void MakeCustumDataGridView()
        {
            LogUtility.DebugMethodStart();

            DataGridViewTextBoxColumn column;

            // 初期表示はマニフェストで行う
            column = new DataGridViewTextBoxColumn();
            column.Name = "Column1";
            column.Width = 180;
            column.HeaderText = "マニフェスト区分";
            column.DataPropertyName = "Mani_Kbn";
            this.Form.customDataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Column2";
            column.Width = 120;
            column.HeaderText = "廃棄物区分";
            column.DataPropertyName = "Haiki_Kbn";
            this.Form.customDataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Column3";
            column.Width = 100;
            column.HeaderText = "交付番号";
            column.DataPropertyName = "Kofu_Bangou";
            this.Form.customDataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Column4";
            column.Width = 1000;
            column.HeaderText = "チェック項目";
            column.DataPropertyName = "Check_Kmk";
            this.Form.customDataGridView1.Columns.Add(column);

            // 新規行追加不可
            this.Form.customDataGridView1.AllowUserToAddRows = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>イベントの初期化処理</summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.Form.Parent;

            // 印刷ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.Form.ButtonFunc5_Clicked);
            // CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.Form.ButtonFunc6_Clicked);
            // 検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.Form.ButtonFunc8_Clicked);
            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.Form.ButtonFunc12_Clicked);

            // 20140623 ria EV004852 一覧と抽出条件の変更 start
            // 修正ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.Form.ButtonFunc3_Clicked);
            //// 並び替えボタン(F10)イベント生成
            //parentForm.bt_func10.Click += new EventHandler(this.Form.ButtonFunc10_Clicked);
            // 20140623 ria EV004852 一覧と抽出条件の変更 end

            LogUtility.DebugMethodEnd();
        }

        /// <summary> ボタンの初期化 </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.Form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.Form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary> ボタン設定の読込 </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);
        }

        // 20140623 ria EV004852 一覧と抽出条件の変更 start
        /// <summary>
        /// 画面遷移
        /// </summary>
        public void FormChanges(WINDOW_TYPE WindowType)
        {
            LogUtility.DebugMethodStart();

            if (this.Form.customDataGridView1.RowCount == 0)
            {
                // 対象データを選択してください。
                msgLogic.MessageBoxShow("E051", "対象データ");
                return;
            }

            String maniFlag = string.Empty;
            String haikiKbn = string.Empty;
            String maniId = string.Empty;
            String systemId = string.Empty;
            String kanriId = string.Empty;
            String latestSeq = string.Empty;

            String MasutaS = string.Empty;
            String CD1 = string.Empty;
            String CD2 = string.Empty;
            String Bunkatu = string.Empty;

            try
            {
                #region 修正
                if (WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    int i = this.Form.customDataGridView1.CurrentRow.Index;

                    //画面起動
                    if (this.JoukenParam.CkTaisyou == "1")
                    {
                        #region マニフェスト
                        haikiKbn = this.Form.customDataGridView1.Rows[i].Cells["Hidden_Haiki_Kbn"].Value.ToString();
                        maniId = this.Form.customDataGridView1.Rows[i].Cells["Hidden_Mani_Id"].Value.ToString();
                        systemId = this.Form.customDataGridView1.Rows[i].Cells["Hidden_System_Id"].Value.ToString();

                        serchCMDto.S_HAIKI_KBN = haikiKbn;
                        serchCMDto.S_MANIFEST_ID = maniId;
                        serchCMDto.S_SYSTEM_ID = systemId;

                        DataTable dt = new DataTable();

                        switch (haikiKbn)
                        {
                            case "1":
                            case "2":
                            case "3":
                                dt = this.checkManifest.GetManiSystemID(this.serchCMDto);
                                break;
                            case "4":
                                dt = this.checkManifest.GetDenManiSystemID(this.serchCMDto);
                                break;
                        }

                        this.Form.ParamOut_WinType = (int)WindowType;
                        this.Form.ParamOut_SysID = dt.Rows[0]["SYSTEM_ID"].ToString();
                        latestSeq = dt.Rows[0]["LATEST_SEQ"].ToString();
                        kanriId = dt.Rows[0]["KANRI_ID"].ToString();

                        var formId = String.Empty;

                        switch (haikiKbn)
                        {
                            case "1"://G119 産廃（直行）マニフェスト一覧
                                formId = "G119";
                                break;
                            case "3"://G120 産廃（積替）マニフェスト一覧
                                formId = "G120";
                                break;
                            case "2"://G121 建廃マニフェスト一覧
                                formId = "G121";
                                break;
                            case "4"://電子
                                formId = "G141";
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
                            if (haikiKbn != "4")
                            {
                                this.Form.ParamOut_WinType = (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                                FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, "", this.Form.ParamOut_SysID, "", this.Form.ParamOut_WinType);
                            }
                            else
                            {
                                // 電マニは引数が違う
                                FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, kanriId, latestSeq);
                            }
                        }
                        else if (isReference)
                        {
                            if (haikiKbn != "4")
                            {
                                this.Form.ParamOut_WinType = (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, "", this.Form.ParamOut_SysID, "", this.Form.ParamOut_WinType);
                            }
                            else
                            {
                                // 電マニは引数が違う
                                FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kanriId, latestSeq);
                            }
                        }
                        else
                        {
                            new MessageBoxShowLogic().MessageBoxShow("E158", "修正");
                        }
                        #endregion
                    }
                    else
                    {
                        #region マスタ

                        #region キーCDの取得
                        string CD_1 = String.Empty;
                        string CD_2 = String.Empty;

                        MasutaS = this.Form.customDataGridView1.Rows[i].Cells["Masuta_Syurui"].Value.ToString();
                        Bunkatu = Convert.ToString(this.Form.customDataGridView1.Rows[i].Cells["Code"].Value.ToString().IndexOf("、"));
                        
                        if (Bunkatu.Equals("-1"))
                        {
                            CD_1 = this.Form.customDataGridView1.Rows[i].Cells["Code"].Value.ToString();
                        }
                        else
                        {
                            CD_1 = this.Form.customDataGridView1.Rows[i].Cells["Code"].Value.ToString().Substring(0, Convert.ToInt16(Bunkatu));
                            CD_2 = this.Form.customDataGridView1.Rows[i].Cells["Code"].Value.ToString().Substring(Convert.ToInt16(Bunkatu)+1);
                        }
                        #endregion キーCDの取得

                        #region フォームIDを設定
                        var formId = String.Empty;
                        switch (MasutaS)
                        {
                            case "業者":
                                formId = "M215";
                                break;
                            case "現場":
                                formId = "M217";
                                break;
                            case "廃棄物種類":
                                formId = "M229";
                                break;
                            case "電子廃棄物種類":
                                formId = "M320";
                                break;
                            case "電子事業者":
                                formId = "M309";
                                break;
                            case "電子事業場":
                                formId = "M312";
                                break;
                            case "地域別業種":
                                formId = "M238";
                                break;
                            case "地域別施設":
                                formId = "M239";
                                break;
                            case "地域別住所":
                                formId = "M240";
                                break;
                            case "地域別処分":
                                formId = "M241";
                                break;
                            case "地域別分類":
                                formId = "M242";
                                break;
                        }
                        #endregion フォームIDを設定

                        #region 業者/現場マスタのDELETE_FLGチェック
                        //⇒DELETE_FLGが立ってるときは、アラートを出す（業者/現場一覧からの遷移に合わせる）
                        // 削除されている明細を一覧から修正実行されたときは復活をさせるかさせないかの選択ダイアログを表示
                        // 「はい」を選択した場合は修正モードで表示を行い、登録することにより削除フラグを外す。
                        if (MasutaS.Equals("業者"))
                        {
                            M_GYOUSHA data = this.daoGyousha.GetDataByCd(CD_1);
                            if (data.DELETE_FLG)
                            {
                                var result = msgLogic.MessageBoxShow("C057");
                                if (result != DialogResult.Yes)
                                {
                                    LogUtility.DebugMethodEnd();
                                    return;
                                }
                            }
                        }

                        if (MasutaS.Equals("現場"))
                        {
                            M_GENBA condition = new M_GENBA();
                            condition.GYOUSHA_CD = CD_1;
                            condition.GENBA_CD = CD_2;
                            M_GENBA data = this.daoGenba.GetDataByCd(condition);
                            if (data.DELETE_FLG)
                            {
                                var result = msgLogic.MessageBoxShow("C057");
                                if (result != DialogResult.Yes)
                                {
                                    LogUtility.DebugMethodEnd();
                                    return;
                                }
                            }
                        }
                        #endregion 業者/現場マスタのDELETE_FLGチェック

                        #region 権限チェック
                        // 修正権限あり → 修正モード
                        // 修正権限なし 参照権限あり → 参照モード
                        // その他 → 開かない
                        var isUpdate = Manager.CheckAuthority(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
                        var isReference = Manager.CheckAuthority(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
                        if (isUpdate)
                        {
                            switch (MasutaS)
                            {
                                case "業者":
                                case "電子事業者":
                                case "廃棄物種類":
                                case "電子廃棄物種類":
                                case "地域別業種":
                                case "地域別施設":
                                case "地域別住所":
                                case "地域別処分":
                                case "地域別分類":
                                    FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, CD_1);
                                    break;
                                case "現場":
                                case "電子事業場":
                                    FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, CD_1,CD_2);
                                    break;
                            }
                        }
                        else if (isReference)
                        {
                            switch (MasutaS)
                            {
                                case "業者":
                                case "電子事業者":
                                case "廃棄物種類":
                                case "電子廃棄物種類":
                                case "地域別業種":
                                case "地域別施設":
                                case "地域別住所":
                                case "地域別処分":
                                case "地域別分類":
                                    FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, CD_1);
                                    break;
                                case "現場":
                                case "電子事業場":
                                    FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, CD_1, CD_2);
                                    break;
                            }
                        }
                        else
                        {
                            new MessageBoxShowLogic().MessageBoxShow("E158", "修正");
                        }
                        #endregion 権限チェック

                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormChanges", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }

            LogUtility.DebugMethodEnd();

            return;
        }
        // 20140623 ria EV004852 一覧と抽出条件の変更 end

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

        #endregion - Methods -
    }
}
