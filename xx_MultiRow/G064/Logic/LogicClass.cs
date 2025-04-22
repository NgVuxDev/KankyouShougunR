// $Id: LogicClass.cs 52539 2015-06-17 04:36:20Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.APP.Base.IchiranHeader;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.SalesManagement.UriageMotocho.Accessor;
using Shougun.Function.ShougunCSCommon.Utility;
using System.Text.RegularExpressions;
using r_framework.Dao;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.SalesManagement.UriageMotocho.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass
    {
        #region フィールド
        /// <summary>
        /// 売上元帳画面Form
        /// </summary>
        private UriageMotocho.APP.UIForm form;
        /// <summary>
        /// 範囲条件指定結果
        /// </summary>
        private MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param { get; set; }
        /// <summary>
        /// DBAccessor
        /// </summary>
        private DBAccessor dba;
        /// <summary>
        /// 取引先CDList
        /// </summary>
        private M_TORIHIKISAKI[] list;
        /// <summary>
        /// 取引先CD切り替え用CurrentIndex
        /// </summary>
        private long CurIndex;
        /// <summary>
        /// ParentForm
        /// </summary>
        private IchiranBaseForm parentForm;
        /// <summary>
        /// HeaderForm
        /// </summary>
        private IchiranHeaderForm2 headerForm;
        /// <summary>
        /// 明細用一覧DataTable
        /// </summary>
        private DataTable ichiranTable;
        /// <summary>
        /// 範囲条件にて指定した範囲の全てのDataTable
        /// </summary>
        private DataTable allDataTable;
        /// <summary>
        /// 
        /// </summary>
        private RibbonMainMenu ribbon;

        // No.3688-->
        /// <summary>
        /// System情報
        /// </summary>
        private M_SYS_INFO SysInfo;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        internal MessageBoxShowLogic errmessage;
        // No.3688<--
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">対象フォーム</param>
        internal LogicClass(UriageMotocho.APP.UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フィールドの初期化
            this.form = targetForm;
            this.dba = new DBAccessor();

            this.CurIndex = 0;

            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.errmessage = new MessageBoxShowLogic();
            LogUtility.DebugMethodEnd(targetForm);
        }

        #region Equals/GetHashCode/ToString
        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            LogicClass localLogic = other as LogicClass;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ParentFormのSet
                this.parentForm = (IchiranBaseForm)this.form.Parent;

                // HeaderFormのSet
                this.headerForm = (IchiranHeaderForm2)this.parentForm.headerForm;

                // RibbonMenuのSet
                this.ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 画面表示初期化
                this.SetInitDisp();

                // SystemInfo取得
                SysInfo = this.dba.GetSysInfo();    // No.3688
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
        /// 取引先戻し処理
        /// </summary>
        internal bool Prev()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 検索実行が行われている？
                if (this.list != null)
                {
                    // リスト上最初の取引先である場合は何もしない
                    if (this.CurIndex > 0)
                    {
                        // 次の取引先CD
                        this.CurIndex--;

                        // 一覧画面更新
                        this.SetIchiran();
                    }

                    // No.2287
                    if (this.CurIndex == 0)
                    {
                        // 前ボタン(F1)非活性化
                        this.parentForm.bt_func1.Enabled = false;
                    }
                    if (this.CurIndex < (this.list.Length - 1))
                    {
                        // 後ボタン(F2)活性化
                        this.parentForm.bt_func2.Enabled = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Prev", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Prev", ex);
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
        /// 取引先送り処理
        /// </summary>
        internal bool Next()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 検索実行が行われている？
                if (this.list != null)
                {
                    // リスト上最後の取引先である場合は何もしない
                    if (this.CurIndex < (this.list.Length - 1))
                    {
                        // 次の取引先CD
                        this.CurIndex++;

                        // 一覧画面更新
                        this.SetIchiran();
                    }

                    // No.2287
                    if (this.CurIndex > 0)
                    {
                        // 前ボタン(F1)活性化
                        this.parentForm.bt_func1.Enabled = true;
                    }
                    if (this.CurIndex == (this.list.Length - 1))
                    {
                        // 後ボタン(F2)非活性化
                        this.parentForm.bt_func2.Enabled = false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Next", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Next", ex);
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
        /// 印刷処理
        /// </summary>
        internal bool Print()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 検索実行が行われている？
                if (this.list != null)
                {
                    // 帳票用データ作成
                    var table = this.CreateReportData();
                    if (table != null)
                    {
                        // 現在表示されている一覧をレポート情報として生成
                        ReportInfoBase reportInfo = new ReportInfoBase(table);
                        reportInfo.Create(Const.UIConstans.OutputFormFullPathName, "UriageMotochoReport", table);

                        // 印刷ポップアップ表示
                        FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
                        reportPopup.ReportCaption = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                        reportPopup.ShowDialog();
                        reportPopup.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
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
        /// CSV出力処理
        /// </summary>
        internal bool CSV()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // CSV出力用のMultiRowを作成
                var EditMultiRow = this.form.Ichiran;

                // 検索実行が行われている？
                if (this.list != null)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)			// CSV出力しますか？
                    {
                        // DataGridView生成
                        var dgv = new CustomDataGridView();
                        dgv.Visible = false;

                        // DataTableを生成
                        var dt = new DataTable();

                        // 出力用のデータを作成
                        for (int index = 0; index < this.list.Length; index++)
                        {
                            dt.Merge(this.GetIchiranDataByCd(this.list[index].TORIHIKISAKI_CD));
                        }

                        // 抽出したデータをMultiRowへ
                        EditMultiRow.DataSource = dt;

                        this.form.Controls.Add(dgv);

                        // MultiRowからDataTableを抜き出してCSV用に整形し、DataGridViewのDataSourceとして登録
                        var binderUtil = new DataBinderUtility<M_GYOUSHA>();

                        // 取引先情報を切り替わりの一行のみにする
                        var torihikisakiSentei = binderUtil.GetDataTableForMultiRow(EditMultiRow);
                        //DataTable datasource = new DataTable();
                        foreach (var li in this.list)
                        {
                            var rows = torihikisakiSentei.AsEnumerable().Where(w => w["取引先CD"].ToString().Equals(li.TORIHIKISAKI_CD.ToString()));
                            bool isFirst = true;
                            foreach (var row in rows)
                            {
                                // 1行目以外の取引先CDと名称をブランクにする
                                if (!isFirst)
                                {
                                    row["取引先CD"] = string.Empty;
                                    row["取引先名"] = string.Empty;
                                }
                                isFirst = false;
                            }
                        }

                        // 数量と単位を分ける
                        var suryouTaniKirihanasi = torihikisakiSentei;
                        // 数量列を追加
                        var suuryouColumn = new DataColumn();
                        suuryouColumn.ColumnName = "数量";
                        suryouTaniKirihanasi.Columns.Add(suuryouColumn);
                        // 単位列を追加
                        var unitColumn = new DataColumn();
                        unitColumn.ColumnName = "単位";
                        suryouTaniKirihanasi.Columns.Add(unitColumn);

                        // "数量・単位"列のDBNullをすべて空白に変換
                        suryouTaniKirihanasi.Rows.Cast<DataRow>().ToList().Where(r => r["数量・単位"].Equals(DBNull.Value)).ToList().ForEach(f => f["数量・単位"] = string.Empty);

                        // 単位抽出用正規表現
                        //Regex unitRgx = new Regex("[^0-9.,]+$");
                        // 数量抽出用正規表現
                        //Regex suuryouRgx = new Regex("^[0-9](.+?)(?![0-9.,])");

                        foreach (DataRow row in suryouTaniKirihanasi.Rows)
                        {
                            // 数量・単位から単位を消したものを数量に設定
                            //row["数量"] = unitRgx.Replace(row["数量・単位"].ToString(), string.Empty);
                            // 数量・単位から数量を消したものを単位に設定
                            //row["単位"] = suuryouRgx.Replace(row["数量・単位"].ToString(), string.Empty);

                            if (row["数量・単位"] != null && !string.IsNullOrEmpty(Convert.ToString(row["数量・単位"])))
                            {
                                int indexrow = suryouTaniKirihanasi.Rows.IndexOf(row);
                                // 数量・単位から単位を消したものを数量に設定
                                row["数量"] = FormatUtility.ToAmountValue(Convert.ToString(dt.Rows[indexrow]["SUURYOU"]), SysInfo.SYS_SUURYOU_FORMAT);
                                // 数量・単位から数量を消したものを単位に設定
                                row["単位"] = Convert.ToString(dt.Rows[indexrow]["UNIT_NAME_RYAKU"]);
                            }
                        }

                        // 不要になったため削除
                        suryouTaniKirihanasi.Columns.Remove("数量・単位");

                        // CSV出力の為の並び替え
                        suryouTaniKirihanasi.Columns["取引先CD"].SetOrdinal(0);
                        suryouTaniKirihanasi.Columns["伝票番号"].SetOrdinal(4);
                        suryouTaniKirihanasi.Columns["現場CD"].SetOrdinal(7);
                        suryouTaniKirihanasi.Columns["現場名"].SetOrdinal(8);
                        suryouTaniKirihanasi.Columns["数量"].SetOrdinal(11);
                        suryouTaniKirihanasi.Columns["単位"].SetOrdinal(12);

                        // 請求番号項目削除対応(#2402)
                        suryouTaniKirihanasi.Columns.Remove("請求番号");

                        dgv.DataSource = suryouTaniKirihanasi;
                        dgv.Refresh();

                        // 共通部品を利用して、画面に表示されているデータをCSVに出力する
                        var CSVExport = new CSVExport();
                        CSVExport.ConvertCustomDataGridViewToCsv(dgv, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_URIAGE_MOTOCHO), this.form);

                        // 一覧画面更新
                        this.SetIchiran();
                    }
                }
                else
                {
                    // アラートを表示し、CSV出力処理はしない
                    this.errmessage.MessageBoxShow("E044");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CSV", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
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
        /// 範囲条件指定画面表示処理
        /// </summary>
        /// <param name="type">画面種別</param>
        internal bool ShowPopUp(MotochoHaniJokenPopUp.Const.UIConstans.DispType type)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(type);

                // ループフラグ
                Boolean searchFlg = true;

                // 受渡項目生成
                this.param = this.CreateParams(type);

                // 売上範囲条件指定画面表示
                var PopUpForm = new MotochoHaniJokenPopUp.APP.UIForm(this.param);

                // 20150928 katen #12048 「システム日付」の基準作成、適用 start
                PopUpForm.sysDate = this.parentForm.sysDate;
                // 20150928 katen #12048 「システム日付」の基準作成、適用 end

                while (searchFlg)
                {
                    PopUpForm.ShowDialog();

                    // 実行結果
                    switch (PopUpForm.DialogResult)
                    {
                        case DialogResult.OK:		// 「F8:検索実行」押下
                            // 抽出処理開始
                            this.form.Cursor = Cursors.WaitCursor;

                            // 結果を次回条件値に保存
                            this.param = PopUpForm.Joken;

                            // 指定された範囲における全てのデータ取得
                            bool hadSearched = false;
                            this.allDataTable = this.dba.GetIchiranData(this.param, this.ribbon.GlobalCommonInformation, this.form, ref hadSearched);

                            if (this.allDataTable.Rows.Count == 0)
                            {
                                // 抽出処理終了
                                this.form.Cursor = Cursors.Default;
                                // 開放
                                PopUpForm.Dispose();
                                // 画面表示初期化
                                PopUpForm = new MotochoHaniJokenPopUp.APP.UIForm(this.param);

                                // 20150928 katen #12048 「システム日付」の基準作成、適用 start
                                PopUpForm.sysDate = this.parentForm.sysDate;
                                // 20150928 katen #12048 「システム日付」の基準作成、適用 end
                                this.SetInitDisp();

                                // 検索完了時のみエラー表示
                                if (hadSearched)
                                {
                                    // 検索エラーメッセージ
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("C001");
                                }
                            }
                            else
                            {
                                searchFlg = false;
                                // 取得したデータより取引先Listを取得
                                this.list = this.GetTorihikisakiList();

                                // Indexを初期化
                                this.CurIndex = 0;

                                // 表示項目更新
                                this.form.DENPYOU_DATE_START.Value = this.param.StartDay;
                                this.form.DENPYOU_DATE_END.Value = this.param.EndDay;
                                this.form.SOU_KENSU.Text = this.list.Length.ToString();

                                // 一覧画面更新
                                this.SetIchiran();

                                // 抽出処理終了
                                this.form.Cursor = Cursors.Default;
                            }
                            break;
                        case DialogResult.Cancel:	// 「F12:キャンセル」押下
                            // 何もしない
                            searchFlg = false;
                            break;
                        default:
                            searchFlg = false;
                            break;
                    }
                }

                // 開放
                PopUpForm.Dispose();

                // 日付ラベルのテキスト設定
                if (this.param.TyuusyutuKBN == 1)
                {
                    this.form.DENPYOU_DATE_LABEL.Text = "伝票日付";
                }
                else
                {
                    this.form.DENPYOU_DATE_LABEL.Text = "売上日付";
                }

                //ボタン活性/非活性設定
                SetEnabled();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ShowPopUp", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowPopUp", ex);
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
        /// 選択したセルの伝票番号を用いて修正入力画面を開く
        /// </summary>
        internal bool UpdateWindowShow()
        {
            bool ret = true;
            try
            {
                long DenpyouNum = -1;
                int denshuKbn = 0;

                // 選択されたRowより伝票番号を検索する
                for (int i = 0; i < this.form.Ichiran.Columns.Count; i++)
                {
                    var setDate = this.form.Ichiran[this.form.Ichiran.CurrentRow.Index, i];
                    switch (setDate.Name)
                    {
                        case "DENPYOU_NUMBER":
                            if (false == string.IsNullOrEmpty(setDate.Value.ToString()))
                            {
                                // 伝票番号をセット
                                DenpyouNum = long.Parse(setDate.Value.ToString());
                            }
                            break;
                        case "DENSHU_KBN":
                            if (false == string.IsNullOrEmpty(setDate.Value.ToString()))
                            {
                                // 伝種区分をセット
                                denshuKbn = int.Parse(setDate.Value.ToString());
                            }
                            break;
                        default:
                            // NOTHING
                            break;
                    }
                }

                if (DenpyouNum != -1)
                {
                    // 伝種区分によって開く修正画面を切り替える
                    bool isExistForm;
                    switch (denshuKbn)
                    {
                        // 受入
                        case 1:
                            if (this.SysInfo.UKEIRESHUKA_GAMEN_SIZE == 2)
                            {
                                this.CheckUpdateAuthority("G051", DenpyouNum);
                            }
                            else
                            {
                                this.CheckUpdateAuthority("G721", DenpyouNum);
                            }
                            break;
                        // 出荷
                        case 2:
                            if (this.SysInfo.UKEIRESHUKA_GAMEN_SIZE == 2)
                            {
                                this.CheckUpdateAuthority("G053", DenpyouNum);
                            }
                            else
                            {
                                this.CheckUpdateAuthority("G722", DenpyouNum);
                            }
                            break;
                        // 売上支払
                        case 3:
                            // 20150602 代納伝票対応(代納不具合一覧52) Start
                            T_UR_SH_ENTRY entryUrSh = this.dba.GetUrShEntry(DenpyouNum).FirstOrDefault();
                            if (entryUrSh != null)
                            {
                                if (entryUrSh.DAINOU_FLG.IsNull || entryUrSh.DAINOU_FLG.IsFalse)
                                {
                                    this.CheckUpdateAuthority("G054", DenpyouNum);
                                }
                                else
                                {
                                    this.CheckUpdateAuthority("G161", DenpyouNum);
                                }
                            }
                            // 20150602 代納伝票対応(代納不具合一覧52) End
                            break;
                        // 入金
                        case 10:
                            T_NYUUKIN_ENTRY entry = this.dba.GetNyuukinEntry(DenpyouNum).FirstOrDefault();

                            if (!entry.TOK_INPUT_KBN.IsFalse)
                            {
                                var HeaderBaseForm = new Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.UIHeader();
                                Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.UIForm callForm = null;

                                // 修正モードの権限チェック
                                if (Manager.CheckAuthority("G459", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                                {
                                    callForm = new Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.UIForm(HeaderBaseForm, WINDOW_TYPE.UPDATE_WINDOW_FLAG, DenpyouNum.ToString());
                                }
                                // 参照モードの権限チェック
                                else if (Manager.CheckAuthority("G459", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                                {
                                    callForm = new Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.UIForm(HeaderBaseForm, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DenpyouNum.ToString());
                                }
                                else
                                {
                                    // 修正モードの権限なしのアラームを上げる
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E158", "修正");
                                    break;
                                }

                                var MasterBaseForm = new BusinessBaseForm(callForm, HeaderBaseForm);

                                isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                                if (!isExistForm)
                                {
                                    MasterBaseForm.Show();
                                }
                            }
                            else
                            {
                                var HeaderBaseForm = new Shougun.Core.ReceiptPayManagement.NyukinNyuryoku3.UIHeader();
                                Shougun.Core.ReceiptPayManagement.NyukinNyuryoku3.UIForm callForm = null;

                                // 修正モードの権限チェック
                                if (Manager.CheckAuthority("G619", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                                {
                                    callForm = new Shougun.Core.ReceiptPayManagement.NyukinNyuryoku3.UIForm(HeaderBaseForm, WINDOW_TYPE.UPDATE_WINDOW_FLAG, DenpyouNum.ToString());
                                }
                                // 参照モードの権限チェック
                                else if (Manager.CheckAuthority("G619", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                                {
                                    callForm = new Shougun.Core.ReceiptPayManagement.NyukinNyuryoku3.UIForm(HeaderBaseForm, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DenpyouNum.ToString());
                                }
                                else
                                {
                                    // 修正モードの権限なしのアラームを上げる
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E158", "修正");
                                    break;
                                }

                                var MasterBaseForm = new BusinessBaseForm(callForm, HeaderBaseForm);

                                isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                                if (!isExistForm)
                                {
                                    MasterBaseForm.Show();
                                }
                            }
                            break;
                        default:
                            // NOTHING
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateWindowShow", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 修正モード呼出時の権限チェック
        /// </summary>
        /// <param name="strFormId">フォームID</param>
        /// <param name="DenpyouNum">伝票番号</param>
        internal void CheckUpdateAuthority(string strFormId, long DenpyouNum)
        {
            // 修正モードの権限チェック
            if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, DenpyouNum);
            }
            // 参照モードの権限チェック
            else if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DenpyouNum);
            }
            else
            {
                // 修正モードの権限なしのアラームを上げる
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
            }
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            LogUtility.DebugMethodStart();

            // 表示項目更新
            this.form.TORIHIKISAKI_NAME.Text = (Strings.StrConv(this.list[this.CurIndex].TORIHIKISAKI_CD, VbStrConv.Wide, 0) + "　" + this.list[this.CurIndex].TORIHIKISAKI_NAME_RYAKU);		// CDを全角化
            this.form.HYOUJI_KENSU.Text = (this.CurIndex + 1).ToString();

            // 指定されたCDでフィルタリングした明細用一覧データの取得
            this.ichiranTable = this.GetIchiranDataByCd(this.list[this.CurIndex].TORIHIKISAKI_CD);

            // データ件数をセット
            this.headerForm.ReadDataNumber.Text = this.ichiranTable.Rows.Count.ToString();

            // 取得した一覧データをDataSourceにセット
            this.form.Ichiran.DataSource = this.ichiranTable;

            LogUtility.DebugMethodEnd();
        }

        #region private
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        /// <returns name="ButtonSetting[]">XMLに記載されたButtonのリスト</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            var tmp = buttonSetting.LoadButtonSetting(thisAssembly, Const.UIConstans.ButtonInfoXmlPath);
            return tmp;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // 前ボタン(F1)イベント生成
            this.parentForm.bt_func1.Click += new EventHandler(this.form.Prev);

            // 次ボタン(F2)イベント生成
            this.parentForm.bt_func2.Click += new EventHandler(this.form.Next);

            // 印刷ボタン(F5)イベント生成
            this.parentForm.bt_func5.Click += new EventHandler(this.form.Print);

            // CSVボタン(F6)イベント生成
            this.parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            // 検索ボタン(F8)イベント生成
            this.parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            // 閉じるボタン(F12)イベント生成
            this.parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 明細ダブルクリックイベント生成
            this.form.Ichiran.CellDoubleClick += new EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.CellDoubleClick);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        //ボタン活性/非活性
        /// </summary>
        /// <returns></returns>
        private void SetEnabled()
        {
            // 検索実行が行われている？
            if (this.list != null)
            {
                // 前ボタン(F1)活性化
                this.parentForm.bt_func1.Enabled = true;
                // 次ボタン(F2)活性化
                this.parentForm.bt_func2.Enabled = true;
                // 印刷ボタン(F5)活性化
                this.parentForm.bt_func5.Enabled = true;

                // No.2287
                if (this.CurIndex == 0)
                {
                    // 前ボタン(F1)非活性化
                    this.parentForm.bt_func1.Enabled = false;
                }
                if (this.CurIndex == (this.list.Length - 1))
                {
                    // 次ボタン(F2)非活性化
                    this.parentForm.bt_func2.Enabled = false;
                }
            }
            else
            {
                // 前ボタン(F1)非活性化
                this.parentForm.bt_func1.Enabled = false;
                // 次ボタン(F2)非活性化
                this.parentForm.bt_func2.Enabled = false;
                // 印刷ボタン(F5)非活性化
                this.parentForm.bt_func5.Enabled = false;
            }
        }

        /// <summary>
        /// 受渡項目生成
        /// </summary>
        /// <param name="type">画面種別</param>
        /// <returns name="ConditionInfo">範囲条件情報</returns>
        private MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo CreateParams(MotochoHaniJokenPopUp.Const.UIConstans.DispType type)
        {
            MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo info;
            if (this.param.DataSetFlag == false)
            {
                // 初期設定条件を設定
                info.ShowDisplay = type;					// 呼び出し画面

                //当月月初月末を設定
                DateTime today = this.parentForm.sysDate;
                DateTime firstDay = today.AddDays(-today.Day + 1);
                DateTime endDay = firstDay.AddMonths(1).AddDays(-1);
                info.StartDay = firstDay;		// 開始日付
                info.EndDay = endDay;			// 終了日付

                info.StartTorihikisakiCD = string.Empty;	// 開始取引先CD
                info.EndTorihikisakiCD = string.Empty;		// 終了取引先CD
                info.OutPutKBN = 1;							// 出力区分
                info.DataSetFlag = false;					// 値格納フラグ
                info.TorihikiKBN = 1;                       // 取引区分(元帳種類)
                info.TyuusyutuKBN = 1;                      // 抽出方法
                info.Shimebi = "";                          // 締日
            }
            else
            {
                // 前回値のまま
                info = this.param;
            }

            return info;
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="DataTable">帳票用データ</returns>
        private DataTable CreateReportData()
        {
            DataTable reportTable = new DataTable();

            /**********************************************************************/
            /**		TODO: 現状、セルのサイズ上、最大出力行を11行としているが	**/
            /**		仕様の調整が必要(少なすぎないか？幅の調整できるか？)		**/
            /**********************************************************************/

            // 帳票用データ作成
            reportTable = this.allDataTable.Copy();
            reportTable.Columns.Add("CORP_NAME");
            reportTable.Columns.Add("DENPYOU_DATE_START");
            reportTable.Columns.Add("DENPYOU_DATE_END");
            reportTable.Columns.Add("DATE_NAME");

            // 表示用列追加
            reportTable.Columns.Add("TANKA_STR", typeof(string));
            reportTable.Columns.Add("URIAGE_KINGAKU_STR", typeof(string));
            reportTable.Columns.Add("SHOUHIZEI_CALC_STR", typeof(string));
            reportTable.Columns.Add("NYUUKIN_KINGAKU_STR", typeof(string));
            reportTable.Columns.Add("SASHIHIKI_ZANDAKA_STR", typeof(string));

            reportTable.Columns["MEISAI_BIKOU"].ReadOnly = false;   // No.1708

            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            reportTable.Columns.Add("HAKKOU_DATE");
            DateTime now = this.getDBDateTime();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            foreach (DataRow row in reportTable.Rows)
            {
                // 固定情報のセット
                row["CORP_NAME"] = this.ribbon.GlobalCommonInformation.CorpInfo.CORP_RYAKU_NAME;
                row["DENPYOU_DATE_START"] = this.param.StartDay;
                row["DENPYOU_DATE_END"] = this.param.EndDay;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                row["HAKKOU_DATE"] = now.ToString("yyyy/MM/dd H:mm:ss");
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                // 日付区分のセット
                if (param.TyuusyutuKBN == 1)        // 抽出方法が「1.伝票日付：月初／月末」の場合
                {
                    row["DATE_NAME"] = "伝票日付";
                }
                else if (param.TyuusyutuKBN == 2)   // 抽出方法が「2.売上日付：締日」の場合
                {
                    row["DATE_NAME"] = "売上日付";
                }
                // 出力の正規化
                //row["TANKA_STR"] = string.Format("{0:#,0}", row["TANKA"]);
                row["TANKA_STR"] = string.Format("{0:" + SysInfo.SYS_TANKA_FORMAT + "}", row["TANKA"]);  // No.3688
                row["URIAGE_KINGAKU_STR"] = string.Format("{0:#,0}", row["URIAGE_KINGAKU"]);
                if (!string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()) && row["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                {
                    row["SHOUHIZEI_CALC_STR"] = string.Format("{0:#,0}", row["SHOUHIZEI"]);
                }
                else if (string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()) && row["ZEI_KBN_CD"].ToString().Equals("1"))
                {
                    row["SHOUHIZEI_CALC_STR"] = string.Format("{0:#,0}", row["SHOUHIZEI"]);
                }
                else if (string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()) && string.IsNullOrEmpty(row["ZEI_KBN_CD"].ToString()))
                {
                    row["SHOUHIZEI_CALC_STR"] = string.Format("{0:#,0}", row["SHOUHIZEI"]);
                }
                else
                {
                    row["SHOUHIZEI_CALC_STR"] = string.Empty;
                }
                row["NYUUKIN_KINGAKU_STR"] = string.Format("{0:#,0}", row["NYUUKIN_KINGAKU"]);
                row["SASHIHIKI_ZANDAKA_STR"] = string.Format("{0:#,0}", row["SASHIHIKI_ZANDAKA"]);
            }

            return reportTable;
        }

        /// <summary>
        /// 取得した全てのDataTableより取引先Listを取得
        /// </summary>
        /// <returns name="M_TORIHIKISAKI[]">取引先EntityのList</returns>
        private M_TORIHIKISAKI[] GetTorihikisakiList()
        {
            DataTable table;

            // 取引先CDが重複した行は除外した状態でDataTableを再構築
            DataView view = new DataView(this.allDataTable);
            table = view.ToTable(true, "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME");

            // entityにCDと略名を格納
            var entity = new M_TORIHIKISAKI[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                entity[i] = new M_TORIHIKISAKI();
                entity[i].TORIHIKISAKI_CD = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                entity[i].TORIHIKISAKI_NAME_RYAKU = table.Rows[i]["TORIHIKISAKI_NAME"].ToString();
            }

            return entity;
        }

        /// <summary>
        /// 指定されたCDでフィルタリングした明細用一覧データの取得
        /// </summary>
        /// <param name="showCD">表示対象となる取引先CD</param>
        /// <returns name="DataTable">データテーブル</returns>
        private DataTable GetIchiranDataByCd(string showCD)
        {
            // 指定されたCDと合致する行を取得しDataTableを再構築
            DataTable table = this.allDataTable.Clone();
            DataRow[] rows = this.allDataTable.Select(string.Format("TORIHIKISAKI_CD LIKE '%{0}%'", showCD));
            foreach (DataRow row in rows)
            {
                table.ImportRow(row);
            }

            return table;
        }

        /// <summary>
        /// 画面表示初期化
        /// </summary>
        private void SetInitDisp()
        {
            // 画面表示初期化
            this.list = null;
            this.form.TORIHIKISAKI_NAME.Text = string.Empty;
            this.form.HYOUJI_KENSU.Text = "0";
            this.form.DENPYOU_DATE_START.Value = null;
            this.form.DENPYOU_DATE_END.Value = null;
            this.form.SOU_KENSU.Text = "0";
            this.parentForm.ProcessButtonPanel.Visible = false;
            this.parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(WINDOW_TITLEExt.ToTitleString(this.form.WindowId));
            this.parentForm.lb_hint.Text = "";
            this.headerForm.ReadDataNumber.Text = "0";
            this.headerForm.alertNumber.TabStop = false;
            this.headerForm.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.headerForm.alertNumber.Visible = false;
            this.headerForm.lbl_アラート件数.Visible = false;
            this.ichiranTable = null;
            this.form.Ichiran.DataSource = this.ichiranTable;
            //ボタン活性/非活性設定
            SetEnabled();
        }
        #endregion

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
