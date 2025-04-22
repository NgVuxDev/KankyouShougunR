// $Id: LogicClass.cs 46153 2015-03-31 04:03:07Z d-sato $
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
using Shougun.Core.Carriage.UnchinDaichou.Accessor;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Function.ShougunCSCommon.Utility;
using r_framework.Dao;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Carriage.UnchinDaichou.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass
    {
        #region フィールド
        /// <summary>
        /// 運賃台帳画面Form
        /// </summary>
        private UnchinDaichou.APP.UIForm form;
        /// <summary>
        /// 範囲条件指定結果
        /// </summary>
        private UnchinDaichouHaniJokenPopUp.Const.UIConstans.ConditionInfo param { get; set; }
        /// <summary>
        /// DBAccessor
        /// </summary>
        private DBAccessor dbAccessor;
        /// <summary>
        /// 運搬業者CDList
        /// </summary>
        private List<M_GYOUSHA> gyoushaList;
        /// <summary>
        /// 取引先CD切り替え用CurrentIndex
        /// </summary>
        private int curIndex;
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
        private M_SYS_INFO sysInfo;
        // No.3688<--

        /// <summary>
        /// CSV出力列
        /// </summary>
        private readonly string[] csvTitles = new string[] {
            "UNPAN_GYOUSHA_CD_LABEL", "UNPAN_GYOUSHA_NAME_LABEL", "DENPYOU_DATE_LABEL", "DENSHU_KBN_CD_LABEL", "DENSHU_KBN_NAME_LABEL", "DENPYOU_NUMBER_LABEL",
            "SHASHU_CD_LABEL", "SHASHU_NAME_LABEL", "SHARYOU_CD_LABEL", "SHARYOU_NAME_LABEL", "UNTENSHA_CD_LABEL", "UNTENSHA_NAME_LABEL", "KEITAI_KBN_CD_LABEL", "KEITAI_KBN_NAME_LABEL",
            "NIZUMI_GYOUSHA_CD_LABEL", "NIZUMI_GYOUSHA_NAME_LABEL", "NIZUMI_GENBA_CD_LABEL", "NIZUMI_GENBA_NAME_LABEL",
            "NIOROSHI_GYOUSHA_CD_LABEL", "NIOROSHI_GYOUSHA_NAME_LABEL", "NIOROSHI_GENBA_CD_LABEL", "NIOROSHI_GENBA_NAME_LABEL",
            "UNCHIN_HINMEI_CD_LABEL", "UNCHIN_HINMEI_NAME_LABEL", "NET_JYUURYOU_LABEL", "SUURYOU_LABEL", 
            "UNIT_CD_LABEL", "UNIT_NAME_RYAKU_LABEL", "TANKA_LABEL", "KINGAKU_LABEL", "MEISAI_BIKOU_LABEL"
        };
        private readonly string[] csvTitlesReplace = new string[]{
            "DENSHU_KBN_NAME_LABEL", "SHASHU_NAME_LABEL", "SHARYOU_NAME_LABEL", "UNTENSHA_NAME_LABEL", "KEITAI_KBN_NAME_LABEL",
            "NIZUMI_GYOUSHA_NAME_LABEL", "NIZUMI_GENBA_NAME_LABEL", "NIOROSHI_GYOUSHA_NAME_LABEL", "NIOROSHI_GENBA_NAME_LABEL",
            "UNIT_NAME_RYAKU_LABEL"
        };
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        private MessageBoxShowLogic MsgBox;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">対象フォーム</param>
        internal LogicClass(UnchinDaichou.APP.UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フィールドの初期化
            this.form = targetForm;
            this.dbAccessor = new DBAccessor();

            this.curIndex = 0;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.MsgBox = new MessageBoxShowLogic();

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
        /// <returns></returns>
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
                sysInfo = this.dbAccessor.GetSysInfo(); // No.3688
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
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
        internal void Prev()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索実行が行われている？
                if (this.gyoushaList != null)
                {
                    // リスト上最初の取引先である場合は何もしない
                    if (this.curIndex > 0)
                    {
                        // 次の取引先CD
                        this.curIndex--;

                        // 一覧画面更新
                        if (!this.SetIchiran()) { return; }
                    }

                    if (this.curIndex == 0)
                    {
                        // 前ボタン(F1)非活性化
                        this.parentForm.bt_func1.Enabled = false;
                    }
                    if (this.curIndex < (this.gyoushaList.Count - 1))
                    {
                        // 後ボタン(F2)活性化
                        this.parentForm.bt_func2.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Prev", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先送り処理
        /// </summary>
        internal void Next()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索実行が行われている？
                if (this.gyoushaList != null)
                {
                    // リスト上最後の取引先である場合は何もしない
                    if (this.curIndex < (this.gyoushaList.Count - 1))
                    {
                        // 次の取引先CD
                        this.curIndex++;

                        // 一覧画面更新
                        if (!this.SetIchiran()) { return; }
                    }

                    if (this.curIndex > 0)
                    {
                        // 前ボタン(F1)活性化
                        this.parentForm.bt_func1.Enabled = true;
                    }
                    if (this.curIndex == (this.gyoushaList.Count - 1))
                    {
                        // 後ボタン(F2)非活性化
                        this.parentForm.bt_func2.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Next", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 印刷処理
        /// </summary>
        internal void Print()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索実行が行われている？
                if (this.gyoushaList != null)
                {
                    // 帳票用データ作成
                    var table = this.CreateReportData();
                    if (table != null)
                    {
                        ReportInfoUnchinDaichou reportInfo = new ReportInfoUnchinDaichou();
                        reportInfo.Title = "運賃台帳";
                        reportInfo.UnchinDaichou_Reprt(table);

                        // 印刷アプリ初期動作(プレビュー)
                        FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
                        reportPopup.Caption = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                        reportPopup.ShowDialog();
                        reportPopup.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// CSV出力処理
        /// </summary>
        internal void CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 検索実行が行われている？
                if (this.gyoushaList != null)
                {
                    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes) // CSV出力しますか？
                    {
                        using (var gcmr = new GcCustomMultiRow())
                        using (var dgv = new CustomDataGridView())
                        {
                            // DataTableを生成
                            var dt = new DataTable();

                            // 出力用のデータを作成
                            for (int index = 0; index < this.gyoushaList.Count; index++)
                            {
                                dt.Merge(this.GetIchiranDataByCd(this.gyoushaList[index].GYOUSHA_CD));
                            }

                            // CSV出力用のMultiRowを作成
                            // 20150526 数量単位分割対応(運賃不具合一覧146) Start
                            var outputTemplate = this.form.Ichiran.Template.Clone();
                            outputTemplate.ColumnHeaders[0].Cells.Remove(outputTemplate.ColumnHeaders[0].Cells["SUURYOU_UNIT_LABEL"]);
                            outputTemplate.Row.Cells.Remove(outputTemplate.Row.Cells["SUURYOU_UNIT"]);
                            // 20150526 数量単位分割対応(運賃不具合一覧146) End
                            // 20150526 出力項目横表示順調整(運賃不具合一覧213、214) Start
                            for (int i = 0; i < csvTitles.Length; i++)
                            {
                                outputTemplate.ColumnHeaders[0].Cells[csvTitles[i]].Location = new Point(0, i);
                            }
                            for (int i = 0; i < csvTitlesReplace.Length; i++)
                            {
                                outputTemplate.ColumnHeaders[0].Cells[csvTitlesReplace[i]].Value += "名";
                            }
                            // 20150526 出力項目横表示順調整(運賃不具合一覧213、214) End
                            gcmr.Template = outputTemplate;
                            gcmr.Visible = false;
                            gcmr.AllowUserToAddRows = false;
                            gcmr.AllowUserToDeleteRows = false;
                            this.form.Controls.Add(gcmr);
                            // 抽出したデータをMultiRowへ
                            gcmr.DataSource = dt;
                            gcmr.Refresh();

                            // MultiRowからDataTableを抜き出してCSV用に整形し、DataGridViewのDataSourceとして登録
                            var unpanGyoushaSentei = new DataBinderUtility<T_UNCHIN_ENTRY>().GetDataTableForMultiRow(gcmr);
                            // 運搬業者情報を切り替わりの一行のみにする
                            foreach (var li in this.gyoushaList)
                            {
                                var rows = unpanGyoushaSentei.AsEnumerable().Where(w => w["運搬業者CD"].ToString().Equals(li.GYOUSHA_CD.ToString()));
                                bool isFirst = true;
                                foreach (var row in rows)
                                {
                                    // 1行目以外の取引先CDと名称をブランクにする
                                    if (!isFirst)
                                    {
                                        row["運搬業者CD"] = string.Empty;
                                        row["運搬業者名"] = string.Empty;
                                    }
                                    isFirst = false;

                                    if (row["備考"] != null && !string.IsNullOrEmpty(row["備考"].ToString()))
                                    {
                                        row["備考"] = row["備考"].ToString().Replace("\n", "");
                                    }
                                }
                            }

                            // DataGridView生成
                            dgv.Visible = false;
                            this.form.Controls.Add(dgv);
                            dgv.DataSource = unpanGyoushaSentei;
                            dgv.Refresh();

                            // 共通部品を利用して、画面に表示されているデータをCSVに出力する
                            var CSVExport = new CSVExport();
                            CSVExport.ConvertCustomDataGridViewToCsv(dgv, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_UNCHIN_DAICHOU), this.form);

                            dgv.DataSource = null;
                            gcmr.DataSource = null;
                            this.form.Controls.Remove(dgv);
                            this.form.Controls.Remove(gcmr);
                            dgv.Dispose();
                            gcmr.Dispose();
                        }
                    }
                }
                else
                {
                    msgLogic.MessageBoxShow("E044");
                }

                // 一覧画面更新
                if (!this.SetIchiran()) { return; }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 範囲条件指定画面表示処理
        /// </summary>
        /// <param name="type">画面種別</param>
        internal void ShowPopUp()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ループフラグ
                bool searchFlg = true;

                // 受渡項目生成
                this.param = this.CreateParams();

                // 売上範囲条件指定画面表示
                var pop = new UnchinDaichouHaniJokenPopUp.APP.UIForm(this.param);

                while (searchFlg)
                {
                    pop.ShowDialog();

                    // 実行結果
                    switch (pop.DialogResult)
                    {
                        // 「F8:検索実行」押下
                        case DialogResult.OK:
                            // 抽出処理開始
                            this.form.Cursor = Cursors.WaitCursor;

                            // 結果を次回条件値に保存
                            this.param = pop.Joken;

                            // 指定された範囲における全てのデータ取得
                            bool hadSearched = false;
                            this.allDataTable = this.dbAccessor.GetIchiranData(this.param, this.sysInfo, ref hadSearched);

                            if (this.allDataTable.Rows.Count == 0)
                            {
                                // 抽出処理終了
                                this.form.Cursor = Cursors.Default;
                                // 開放
                                pop.Dispose();
                                // 画面表示初期化
                                pop = new UnchinDaichouHaniJokenPopUp.APP.UIForm(this.param);
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
                                this.gyoushaList = this.GetUnpanGyoushaList();

                                // Indexを初期化
                                this.curIndex = 0;

                                // 表示項目更新
                                this.form.DENPYOU_DATE_START.Value = this.param.StartDay;
                                this.form.DENPYOU_DATE_END.Value = this.param.EndDay;
                                this.form.SOU_KENSU.Text = this.gyoushaList.Count.ToString();

                                // 一覧画面更新
                                if (!this.SetIchiran()) { return; }

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
                pop.Dispose();

                //ボタン活性/非活性設定
                this.SetEnabled();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowPopUp", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 選択したセルの伝票番号を用いて修正入力画面を開く
        /// </summary>
        internal void UpdateWindowShow()
        {
            try
            {
                long denpyouNum = -1;
                int denshuKbn = DENSHU_KBN.DAINOU.GetHashCode();

                // 選択されたRowより伝票番号を検索する
                for (int i = 0; i < this.form.Ichiran.Columns.Count; i++)
                {
                    var setData = this.form.Ichiran[this.form.Ichiran.CurrentRow.Index, i];
                    switch (setData.Name)
                    {
                        case "DENPYOU_NUMBER":
                            if (false == string.IsNullOrEmpty(setData.Value.ToString()))
                            {
                                // 伝票番号をセット
                                denpyouNum = long.Parse(setData.Value.ToString());
                            }
                            break;
                        case "DENSHU_KBN_CD":
                            if (false == string.IsNullOrEmpty(setData.Value.ToString()))
                            {
                                // 伝種区分をセット
                                denshuKbn = int.Parse(setData.Value.ToString());
                            }
                            break;
                        default:
                            // NOTHING
                            break;
                    }
                }

                if (denpyouNum != -1)
                {
                    this.EditDetail("G153", denshuKbn, denpyouNum);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateWindowShow", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 修正モードで画面を開く
        /// </summary>
        /// <param name="strFormId">フォームID</param>
        /// <param name="denshuKbn">伝種区分</param>
        /// <param name="denpyouNum">伝票番号</param>
        /// <remarks>権限チェックも行う</remarks>
        internal void EditDetail(string strFormId, int denshuKbn, long denpyouNum)
        {
            // 修正モードの権限チェック
            if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyouNum);
            }
            // 参照モードの権限チェック
            else if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyouNum);
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
        /// <returns></returns>
        internal bool SetIchiran()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.gyoushaList == null || this.curIndex >= this.gyoushaList.Count)
                {
                    this.headerForm.ReadDataNumber.Text = "0";
                    this.form.Ichiran.DataSource = null;
                    return ret;
                }

                // 表示項目更新
                this.form.UNPAN_GYOUSHA_NAME.Text = (
                    Strings.StrConv(this.gyoushaList[this.curIndex].GYOUSHA_CD, VbStrConv.Wide, 0) + "　" + this.gyoushaList[this.curIndex].GYOUSHA_NAME_RYAKU
                    ); // CDを全角化
                this.form.HYOUJI_KENSU.Text = (this.curIndex + 1).ToString();

                // 指定されたCDでフィルタリングした明細用一覧データの取得
                this.ichiranTable = this.GetIchiranDataByCd(this.gyoushaList[this.curIndex].GYOUSHA_CD);

                // データ件数をセット
                this.headerForm.ReadDataNumber.Text = this.ichiranTable.Rows.Count.ToString();

                // 取得した一覧データをDataSourceにセット
                this.form.Ichiran.DataSource = this.ichiranTable;
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("SetIchiran", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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
            if (this.gyoushaList != null)
            {
                // 前ボタン(F1)活性化
                this.parentForm.bt_func1.Enabled = true;
                // 次ボタン(F2)活性化
                this.parentForm.bt_func2.Enabled = true;
                // 印刷ボタン(F5)活性化
                this.parentForm.bt_func5.Enabled = true;

                if (this.curIndex == 0)
                {
                    // 前ボタン(F1)非活性化
                    this.parentForm.bt_func1.Enabled = false;
                }
                if (this.curIndex == (this.gyoushaList.Count - 1))
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
        private UnchinDaichouHaniJokenPopUp.Const.UIConstans.ConditionInfo CreateParams()
        {
            UnchinDaichouHaniJokenPopUp.Const.UIConstans.ConditionInfo info;
            if (this.param.DataSetFlag == false)
            {
                info = new UnchinDaichouHaniJokenPopUp.Const.UIConstans.ConditionInfo();
                // 当月月初月末を設定
                DateTime today = parentForm.sysDate;
                DateTime firstDay = today.AddDays(-today.Day + 1);
                DateTime endDay = firstDay.AddMonths(1).AddDays(-1);
                info.StartDay = firstDay;		            // 開始日付
                info.EndDay = endDay;			            // 終了日付
                info.StartUnpanGyoushaCD = string.Empty;	// 開始運搬業者CD
                info.EndUnpanGyoushaCD = string.Empty;		// 終了運搬業者CD
            }
            else
            {
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
            reportTable.Columns.Add("NET_JYUURYOU_STR", typeof(string));
            reportTable.Columns.Add("TANKA_STR", typeof(string));
            reportTable.Columns.Add("KINGAKU_STR", typeof(string));

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

                // 出力の正規化
                row["NET_JYUURYOU_STR"] = string.Format("{0:#,##0.###}", row["NET_JYUURYOU"]);
                row["TANKA_STR"] = string.Format("{0:#,##0.###}", row["TANKA"]);
                row["KINGAKU_STR"] = string.Format("{0:#,0}", row["KINGAKU"]);
            }

            return reportTable;
        }

        /// <summary>
        /// 取得した全てのDataTableより運搬業者Listを取得
        /// </summary>
        /// <returns name="M_GYOUSHA[]">運搬業者EntityのArray</returns>
        private List<M_GYOUSHA> GetUnpanGyoushaList()
        {
            // 取引先CDが重複した行は除外した状態でDataTableを再構築
            // 20150522 業者配列の生成方法を修正(不具合一覧62) Start
            var entities = new List<M_GYOUSHA>();
            var unpanGyoushaGroupCollection = this.allDataTable.AsEnumerable().GroupBy(
                x => Convert.IsDBNull(x["UNPAN_GYOUSHA_CD"]) ? string.Empty : Convert.ToString(x["UNPAN_GYOUSHA_CD"])
                );
            foreach (var unpanGyoushaGroup in unpanGyoushaGroupCollection)
            {
                var gyoushaName = unpanGyoushaGroup.
                    OrderBy(x => Convert.IsDBNull(x["DENPYOU_DATE"]) ? DateTime.MinValue : Convert.ToDateTime(x["DENPYOU_DATE"])).
                    ThenBy(x => Convert.IsDBNull(x["DENPYOU_NUMBER"]) ? 0 : Convert.ToInt64(x["DENPYOU_NUMBER"])).
                    ThenBy(x => Convert.IsDBNull(x["ROW_NO"]) ? 0 : Convert.ToInt16(x["ROW_NO"])).
                    FirstOrDefault()["UNPAN_GYOUSHA_NAME"];

                var entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = unpanGyoushaGroup.Key;
                entity.GYOUSHA_NAME_RYAKU = Convert.IsDBNull(gyoushaName) ? string.Empty : Convert.ToString(gyoushaName);

                entities.Add(entity);
            }
            // 20150522 業者配列の生成方法を修正(不具合一覧62) End
            return entities;
        }

        /// <summary>
        /// 指定されたCDでフィルタリングした明細用一覧データの取得
        /// </summary>
        /// <param name="showCd">表示対象となる取引先CD</param>
        /// <returns name="DataTable">データテーブル</returns>
        private DataTable GetIchiranDataByCd(string showCd)
        {
            // 指定されたCDと合致する行を取得しDataTableを再構築
            var table = this.allDataTable.DefaultView;
            table.RowFilter = string.Format("UNPAN_GYOUSHA_CD LIKE '%{0}%'", showCd);
            return table.ToTable();
        }

        /// <summary>
        /// 画面表示初期化
        /// </summary>
        private void SetInitDisp()
        {
            // 画面表示初期化
            this.gyoushaList = null;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
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

            // ボタン活性/非活性設定
            this.SetEnabled();
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
