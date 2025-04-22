using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Accessor;
using Shougun.Core.Common.HanyoCSVShutsuryoku.APP;
using Shougun.Core.Common.HanyoCSVShutsuryoku.APP.Panel;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Const;
using Shougun.Core.Common.HanyoCSVShutsuryoku.DTO;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Logic.Panel;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Utility;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Logic
{
    internal class PatternLogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        ///
        /// </summary>
        private PatternForm callForm;

        /// <summary>
        ///
        /// </summary>
        private IPatternPanel patternPanel;

        /// <summary>
        ///
        /// </summary>
        public BusinessBaseForm parentForm;

        /// <summary>
        /// DBアクセス
        /// </summary>
        private PatternDbAccessor patternDbAccessor;

        /// <summary>
        ///
        /// </summary>
        private DBAccessor commonDbAccessor;

        /// <summary>
        /// メッセージロジック
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        ///
        /// </summary>
        private IPatternPanelLogic pnlLogic;

        /// <summary>
        /// 選択項目表示用テーブル
        /// </summary>
        /// <remarks>
        /// 伝種と出力区分の選択により、パターンXMLから生成
        /// フィルターする前の内容を保持
        /// </remarks>
        private DataTable dtSelect;

        #endregion

        #region コンストラクタ

        /// <summary>
        ///
        /// </summary>
        /// <param name="callForm"></param>
        public PatternLogicClass(PatternForm callForm, IPatternPanel patternPanel)
        {
            LogUtility.DebugMethodStart(callForm, patternPanel);

            this.callForm = callForm;
            this.patternPanel = patternPanel;

            this.patternDbAccessor = new PatternDbAccessor();
            this.commonDbAccessor = new DBAccessor();

            this.msgLogic = new MessageBoxShowLogic();

            // パネル関連ロジック
            this.pnlLogic = PatternPanelLogicFactory.Create(patternPanel);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region メソッド

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                this.parentForm = this.callForm.Parent as BusinessBaseForm;

                // タイトル変更
                var titleKbn = UIConstants.OUTPUT_HANI_KBNS.FirstOrDefault(t => t.Item1 == this.callForm.Pattern.HaniKbn);
                if (titleKbn != null)
                {
                    var titleControl = this.callForm.controlUtil.FindControl(this.parentForm, "lb_title");
                    titleControl.Text += string.Format("({0})", titleKbn.Item2);
                }

                // ボタン初期化
                this.ButtonInit();

                // イベント初期化
                this.EventInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            ButtonControlUtility.SetButtonInfo(
                new ButtonSetting().LoadButtonSetting(Assembly.GetExecutingAssembly(), UIConstants.PatternButtonInfoXmlPath),
                this.parentForm, this.callForm.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // パネルのイベントをバインド
            this.pnlLogic.EventInit();

            #region 既存イベント解除

            this.parentForm.bt_func1.Click -= this.callForm.bt_func1_Click;
            this.parentForm.bt_func2.Click -= this.callForm.bt_func2_Click;
            this.parentForm.bt_func3.Click -= this.callForm.bt_func3_Click;
            this.parentForm.bt_func4.Click -= this.callForm.bt_func4_Click;
            this.parentForm.bt_func6.Click -= this.callForm.bt_func6_Click;
            this.parentForm.bt_func8.Click -= this.callForm.bt_func8_Click;
            this.parentForm.bt_func9.Click -= this.callForm.bt_func9_Click;
            this.parentForm.bt_func12.Click -= this.callForm.bt_func12_Click;

            this.patternPanel.DenshuKbnCheckedChanged -= this.callForm.patternPanel_DenshuKbnCheckedChanged;

            #endregion

            #region 新しいイベントバインド

            this.parentForm.bt_func1.Click += this.callForm.bt_func1_Click;
            this.parentForm.bt_func2.Click += this.callForm.bt_func2_Click;
            this.parentForm.bt_func3.Click += this.callForm.bt_func3_Click;
            this.parentForm.bt_func4.Click += this.callForm.bt_func4_Click;
            this.parentForm.bt_func6.Click += this.callForm.bt_func6_Click;
            this.parentForm.bt_func8.Click += this.callForm.bt_func8_Click;
            this.parentForm.bt_func9.Click += this.callForm.bt_func9_Click;
            this.parentForm.bt_func12.Click += this.callForm.bt_func12_Click;

            this.patternPanel.DenshuKbnCheckedChanged += this.callForm.patternPanel_DenshuKbnCheckedChanged;

            #endregion

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を閉じる
        /// </summary>
        /// <remarks>F12実処理</remarks>
        internal void FormClose()
        {
            LogUtility.DebugMethodStart();

            this.callForm.Close();
            this.parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面設定
        /// </summary>
        public bool FormSet()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                this.pnlLogic.PanelSet();

                #region 利用可否

                switch (this.callForm.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 入力部
                        this.callForm.txtOutputKbn.Enabled = true;
                        this.callForm.rdoOutputKbn1.Enabled = true;
                        this.callForm.rdoOutputKbn2.Enabled = true;
                        this.callForm.txtCondition.Enabled = true;
                        this.callForm.txtPatternNm.Enabled = true;
                        this.callForm.txtPatternBikou.Enabled = true;
                        this.callForm.btnCondition.Enabled = true;
                        this.callForm.btnAdd.Enabled = true;
                        this.callForm.btnRemove.Enabled = true;
                        this.callForm.btnUp.Enabled = true;
                        this.callForm.btnDown.Enabled = true;
                        // ボタン部
                        this.parentForm.bt_func1.Enabled = true;
                        this.parentForm.bt_func2.Enabled = true;
                        this.parentForm.bt_func3.Enabled = true;
                        this.parentForm.bt_func4.Enabled = true;
                        this.parentForm.bt_func6.Enabled = true;
                        this.parentForm.bt_func8.Enabled = true;
                        this.parentForm.bt_func9.Enabled = true;
                        this.parentForm.bt_func12.Enabled = true;
                        break;

                    default:
                        // 入力部
                        this.callForm.txtOutputKbn.Enabled = false;
                        this.callForm.rdoOutputKbn1.Enabled = false;
                        this.callForm.rdoOutputKbn2.Enabled = false;
                        this.callForm.txtCondition.Enabled = false;
                        this.callForm.txtPatternNm.Enabled = false;
                        this.callForm.txtPatternBikou.Enabled = false;
                        this.callForm.btnCondition.Enabled = false;
                        this.callForm.btnAdd.Enabled = false;
                        this.callForm.btnRemove.Enabled = false;
                        this.callForm.btnUp.Enabled = false;
                        this.callForm.btnDown.Enabled = false;
                        // ボタン部
                        this.parentForm.bt_func1.Enabled = false;
                        this.parentForm.bt_func2.Enabled = false;
                        this.parentForm.bt_func3.Enabled = false;
                        this.parentForm.bt_func4.Enabled = false;
                        this.parentForm.bt_func6.Enabled = false;
                        this.parentForm.bt_func8.Enabled = false;
                        this.parentForm.bt_func9.Enabled = true;
                        this.parentForm.bt_func12.Enabled = true;
                        break;
                }

                #endregion

                #region 値設定

                switch (this.callForm.WindowType)
                {
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 出力区分
                        this.callForm.txtOutputKbn.Text =
                            this.callForm.Pattern.CsvPattern.OUTPUT_KBN.IsNull ?
                            "1" : this.callForm.Pattern.CsvPattern.OUTPUT_KBN.Value.ToString(); // デフォルトは伝票
                        // パターン
                        this.callForm.txtPatternNm.Text = this.callForm.Pattern.CsvPattern.PATTERN_NAME;
                        this.callForm.txtPatternBikou.Text = this.callForm.Pattern.CsvPattern.BIKOU;
                        break;

                    default:
                        // 出力区分
                        this.callForm.txtOutputKbn.Text = "1"; // デフォルトは伝票
                        // パターン
                        this.callForm.txtPatternNm.Text = string.Empty;
                        this.callForm.txtPatternBikou.Text = string.Empty;
                        break;
                }

                // 選択項目検索条件
                this.callForm.txtCondition.Text = string.Empty;

                // パターンから選択項目テーブルを生成
                this.SelectSourceCreate();

                // 出力項目グリッド再設定
                this.OutputSourceCreate();

                // 選択項目グリッド再設定
                this.SelectSourceRefresh();

                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormSet", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal void SelectSourceCreate()
        {
            if (this.dtSelect == null)
            {
                this.dtSelect = new DataTable();
                this.dtSelect.Columns.Add("OUTPUT_KBN", typeof(SqlInt32));
                this.dtSelect.Columns.Add("ID", typeof(SqlInt32));
                this.dtSelect.Columns.Add("DISP_NAME", typeof(string));
            }
            else
            {
                this.dtSelect.Rows.Clear();
            }

            var denshuKbns = this.pnlLogic.DenshuKbnsCreate();
            var outputKbn = 0;
            if (denshuKbns.Count > 0 && int.TryParse(this.callForm.txtOutputKbn.Text, out outputKbn))
            {
                var settings = CsvPatternManager.GetCsvPatternSetting(this.callForm.Pattern.HaniKbn, denshuKbns);
                var i = 1;
                var outputColumns = settings.Select(
                    // インデックスを付与
                    s => new
                    {
                        Seq = i++,
                        Groups = s.OutputGroups.Where(g => g.OutputKbn <= outputKbn)
                    }).SelectMany(
                    // グループを展開
                    s => s.Groups.Select(
                        g => new
                        {
                            s.Seq,
                            Group = g
                        })
                    ).SelectMany(
                    // 出力(選択)項目を平坦展開
                    s => s.Group.OutputColumns.Select(
                        c => new
                        {
                            s.Seq,
                            s.Group.OutputKbn,
                            OutputColumn = c
                        })
                    ).GroupBy(
                    // 出力区分により、出力(選択)項目をグループ化
                    c => new
                    {
                        c.OutputKbn,
                        c.OutputColumn.Id
                    }).Select(
                    // 出力区分とID毎により、唯一の出力(選択)項目をフィルター
                    g => new
                    {
                        g.Key.OutputKbn,
                        g.Key.Id,
                        g.OrderBy(c => c.Seq).FirstOrDefault().OutputColumn
                    });

                // 出力区分-表示番号-ID順で構成する
                foreach (var outputColumn in outputColumns.OrderBy(c => c.OutputKbn).ThenBy(d => d.OutputColumn.DispNumber).ThenBy(d => d.OutputColumn.Id))
                {
                    var row = this.dtSelect.NewRow();
                    row["OUTPUT_KBN"] = outputColumn.OutputKbn;
                    row["ID"] = outputColumn.OutputColumn.Id;
                    row["DISP_NAME"] = outputColumn.OutputColumn.DispName;
                    this.dtSelect.Rows.Add(row);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void OutputSourceCreate()
        {
            var dtOut = this.dtSelect.Clone();

            if (this.callForm.Pattern.CsvPatternColumns != null && this.callForm.Pattern.CsvPatternColumns.Count > 0)
            {
                var dtSel = this.dtSelect.AsEnumerable();
                var i = 1;

                foreach (var data in this.callForm.Pattern.CsvPatternColumns.OrderBy(x => x.SORT_NO))
                {
                    var row = dtOut.NewRow();

                    row["OUTPUT_KBN"] = data.OUTPUT_KBN;
                    row["ID"] = data.KOUMOKU_ID;

                    var rowSel = dtSel.FirstOrDefault(s => s["OUTPUT_KBN"].Equals(row["OUTPUT_KBN"]) && s["ID"].Equals(row["ID"]));
                    row["DISP_NAME"] = rowSel == null ? string.Empty : rowSel["DISP_NAME"];

                    dtOut.Rows.Add(row);
                }
            }

            this.callForm.dgvOutput.DataSource = dtOut;
        }

        /// <summary>
        ///
        /// </summary>
        internal void SelectSourceRefresh()
        {
            var dtSel = this.dtSelect.Copy();

            var dtOut = this.callForm.dgvOutput.DataSource as DataTable;
            if (dtOut != null && dtOut.Rows.Count > 0)
            {
                var rowsSel =
                    dtOut.AsEnumerable().SelectMany(
                    s => dtSel.AsEnumerable().Where(
                        o => o["OUTPUT_KBN"].Equals(s["OUTPUT_KBN"]) && o["ID"].Equals(s["ID"]))).ToArray();
                foreach (var rowSel in rowsSel)
                    dtSel.Rows.Remove(rowSel);
            }

            this.callForm.dgvSelect.DataSource = dtSel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="force"></param>
        internal void OutputSourceClear(bool force = false)
        {
            LogUtility.DebugMethodStart(force);

            var clear = force;
            if (!clear)
            {
                // 強制クリアされてない場合
                // 選択項目グリッドが変更されたかをチェック、変更がある場合、出力項目グリッドのクリアを指示
                var dtSel = this.callForm.dgvSelect.DataSource as DataTable;
                if (dtSel != null && this.dtSelect != null)
                    if (dtSel.AsEnumerable().Except(dtSelect.AsEnumerable(), new SelectRowComparer()).Count() > 0)
                        clear = true;
            }

            if (clear)
            {
                var dtOut = this.callForm.dgvOutput.DataSource as DataTable;
                if (dtOut != null)
                    dtOut.Rows.Clear();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="incre">インデックス変更(1又は-1)</param>
        /// <remarks>インデックスに、1又は-1を加算</remarks>
        internal bool OutputRowMove(int incre)
        {
            LogUtility.DebugMethodStart(incre);
            bool ret = true;

            try
            {
                // 選択中の行を確認
                if (this.callForm.dgvOutput.CurrentCell != null &&
                    this.callForm.dgvOutput.CurrentCell.RowIndex >= 0)
                {
                    var curDgvRowIndex = this.callForm.dgvOutput.CurrentCell.RowIndex;
                    switch (incre)
                    {
                        case -1:
                            // 1行目
                            if (curDgvRowIndex == 0)
                                ret = false;
                            break;

                        case 1:
                            // 最後行
                            if (curDgvRowIndex == this.callForm.dgvOutput.Rows.Count - 1)
                                ret = false;
                            break;

                        default:
                            break;
                    }

                    if (ret)
                    {
                        var dtOut = this.callForm.dgvOutput.DataSource as DataTable;
                        if (dtOut != null && dtOut.Rows.Count > 0)
                        {
                            var curDataRow = dtOut.Rows[curDgvRowIndex];
                            var newDataRow = dtOut.NewRow();
                            newDataRow["OUTPUT_KBN"] = curDataRow["OUTPUT_KBN"];
                            newDataRow["ID"] = curDataRow["ID"];
                            newDataRow["DISP_NAME"] = curDataRow["DISP_NAME"];

                            dtOut.Rows.RemoveAt(curDgvRowIndex);
                            dtOut.Rows.InsertAt(newDataRow, curDgvRowIndex + incre);

                            this.callForm.dgvOutput.CurrentCell =
                                this.callForm.dgvOutput.Rows[dtOut.Rows.IndexOf(newDataRow)].Cells.OfType<DataGridViewCell>().FirstOrDefault(x => x.Visible);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OutputRowMove", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal bool OutputRowRemove()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                // 選択中の行を確認
                if (this.callForm.dgvOutput.CurrentCell != null &&
                    this.callForm.dgvOutput.CurrentCell.RowIndex >= 0)
                {
                    var curDgvRowIndex = this.callForm.dgvOutput.CurrentCell.RowIndex;
                    var dtOut = this.callForm.dgvOutput.DataSource as DataTable;
                    if (dtOut != null && dtOut.Rows.Count > 0)
                    {
                        var curDataRow = dtOut.Rows[curDgvRowIndex];
                        var newDataRow = dtOut.NewRow(); // 先削除される行のキーを記録する
                        newDataRow["OUTPUT_KBN"] = curDataRow["OUTPUT_KBN"];
                        newDataRow["ID"] = curDataRow["ID"];

                        dtOut.Rows.RemoveAt(curDgvRowIndex);

                        // 選択中の行のインデックスを再計算
                        if (curDgvRowIndex > dtOut.Rows.Count - 1)
                            curDgvRowIndex--;

                        if (curDgvRowIndex >= 0)
                            this.callForm.dgvOutput.CurrentCell =
                                this.callForm.dgvOutput.Rows[curDgvRowIndex].Cells.OfType<DataGridViewCell>().FirstOrDefault(x => x.Visible);

                        // 選択項目グリッドの表示を更新
                        this.SelectSourceRefresh();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OutputRowRemove", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal bool OutputRowAdd()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                // 選択中の行を確認
                if (this.callForm.dgvSelect.CurrentCell != null &&
                    this.callForm.dgvSelect.CurrentCell.RowIndex >= 0)
                {
                    var curDgvRowIndex = this.callForm.dgvSelect.CurrentCell.RowIndex;
                    var dtSel = this.callForm.dgvSelect.DataSource as DataTable;
                    var dtOut = this.callForm.dgvOutput.DataSource as DataTable;
                    if (dtSel != null && dtSel.Rows.Count > 0 &&
                        dtOut != null)
                    {
                        dtOut.ImportRow(dtSel.Rows[curDgvRowIndex]);
                        dtSel.Rows.RemoveAt(curDgvRowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OutputRowAdd", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        internal bool SelectRowJump()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                if (ret)
                    if (string.IsNullOrWhiteSpace(this.callForm.txtCondition.Text))
                    {
                        this.msgLogic.MessageBoxShowError("検索したい文字列を入力してください"); // TODO: メッセージをID化
                        ret = false;
                    }

                if (ret)
                {
                    var dtSel = this.callForm.dgvSelect.DataSource as DataTable;
                    if (dtSel != null && dtSel.Rows.Count > 0)
                    {
                        var rowsSel = dtSel.AsEnumerable().Where(
                            x => !x.IsNull("DISP_NAME") && x["DISP_NAME"].ToString().Contains(this.callForm.txtCondition.Text.Trim())
                            ).ToList();
                        if (rowsSel != null && rowsSel.Count > 0)
                        {
                            var rowsSelIndexes = rowsSel.Select(r => dtSel.Rows.IndexOf(r));

                            var curDgvRowIndex = -1;
                            if (this.callForm.dgvSelect.CurrentCell != null &&
                                this.callForm.dgvSelect.CurrentCell.RowIndex >= 0)
                                curDgvRowIndex = this.callForm.dgvSelect.CurrentCell.RowIndex;

                            var nextDgvRowIndex =
                                rowsSelIndexes.Where(n => n > curDgvRowIndex).DefaultIfEmpty(rowsSelIndexes.First()).First();

                            this.callForm.dgvSelect.CurrentCell =
                                this.callForm.dgvSelect.Rows[nextDgvRowIndex].Cells.OfType<DataGridViewCell>().FirstOrDefault(x => x.Visible);
                        }
                        else
                        {
                            this.msgLogic.MessageBoxShowError("検索結果が存在しませんでした"); // TODO: メッセージをID化
                            ret = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SelectRowJump", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            if (!ret)
                this.callForm.txtCondition.Select();

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="needName"></param>
        /// <returns></returns>
        public bool FormCheck()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            this.pnlLogic.PreRegistCheck();

            if (ret)
                // AutoRegistCheckを利用し、全コントロールをチェック
                if (new AutoRegistCheckLogic(this.callForm.allControl).AutoRegistCheck())
                {
                    ret = false;

                    // 最初のエラーになったコントロールにフォーカス
                    foreach (var item in this.callForm.allControl.OrderBy(c => c.TabIndex))
                    {
                        if (item is ICustomAutoChangeBackColor && (item as ICustomAutoChangeBackColor).IsInputErrorOccured)
                        {
                            var custom = item as Control;
                            custom.Select();
                            if (!custom.TabStop)
                                (custom.Parent ?? this.callForm).SelectNextControl(custom, true, true, true, true);

                            break;
                        }
                    }

                    // フォーカス当たるコントロールを確定した後で、CausesValidationをTRUEに
                    foreach (var item in this.callForm.allControl.Where(c => c is CustomTextBox))
                        item.CausesValidation = true;
                }

            if (ret)
                // 登録データチェック
                if (this.callForm.dgvOutput.Rows.Count == 0)
                {
                    this.msgLogic.MessageBoxShow("E046", "項目が未選択", "一覧出力項目の");

                    // フォーカスを選択項目へ移動
                    this.callForm.dgvSelect.Select();
                    ret = false;
                }

            this.pnlLogic.PostRegistCheck();

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="realFlg">新規登録用Entityを構成するかどうか</param>
        /// <returns></returns>
        private PatternDto EntityCreate(bool realFlg = true)
        {
            var pattern = new PatternDto(this.callForm.Pattern.HaniKbn);

            #region CSV出力項目

            var csvPattern = new M_OUTPUT_CSV_PATTERN();

            // 新規登録用ではない場合、SYSTEM_IDなどを設定しない
            if (realFlg)
            {
                // システム自動設定のプロパティを設定する
                var csvPatternBinderLogic = new DataBinderLogic<M_OUTPUT_CSV_PATTERN>(csvPattern);
                csvPatternBinderLogic.SetSystemProperty(csvPattern, false);

                if (this.callForm.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 新規
                    csvPattern.SYSTEM_ID = commonDbAccessor.createSystemId((int)DENSHU_KBN.HANYO_CSV_SHUTSURYOKU); // 発番
                    csvPattern.SEQ = 1;
                }
                else
                {
                    // 更新または削除
                    csvPattern.SYSTEM_ID = this.callForm.Pattern.CsvPattern.SYSTEM_ID;
                    csvPattern.SEQ = this.callForm.Pattern.CsvPattern.SEQ + 1;

                    // 元データから作成情報を引き継ぐ
                    csvPattern.CREATE_USER = this.callForm.Pattern.CsvPattern.CREATE_USER;
                    csvPattern.CREATE_DATE = this.callForm.Pattern.CsvPattern.CREATE_DATE;
                    csvPattern.CREATE_PC = this.callForm.Pattern.CsvPattern.CREATE_PC;
                }
            }

            csvPattern.KBN = string.Format("{0:000}", this.callForm.Pattern.HaniKbn);
            csvPattern.OUTPUT_KBN = short.Parse(this.callForm.txtOutputKbn.Text);
            csvPattern.PATTERN_NAME = this.callForm.txtPatternNm.Text;
            csvPattern.BIKOU = this.callForm.txtPatternBikou.Text;
            csvPattern.DELETE_FLG = this.callForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG; // 新規と更新はFalseで、削除はTrue

            pattern.CsvPattern = csvPattern;

            #endregion

            #region CSV出力項目詳細

            var csvPatternColumns = new List<M_OUTPUT_CSV_PATTERN_COLUMN>();

            var dtOut = this.callForm.dgvOutput.DataSource as DataTable;
            if (dtOut != null)
            {
                for (int i = 0; i < dtOut.Rows.Count; i++)
                {
                    var csvPatternColumn = new M_OUTPUT_CSV_PATTERN_COLUMN();

                    csvPatternColumn.SYSTEM_ID = csvPattern.SYSTEM_ID;
                    csvPatternColumn.SEQ = csvPattern.SEQ;
                    csvPatternColumn.DETAIL_SYSTEM_ID = i + 1;
                    csvPatternColumn.OUTPUT_KBN = short.Parse(dtOut.Rows[i]["OUTPUT_KBN"].ToString());
                    csvPatternColumn.KOUMOKU_ID = int.Parse(dtOut.Rows[i]["ID"].ToString());
                    csvPatternColumn.SORT_NO = i + 1;

                    csvPatternColumns.Add(csvPatternColumn);
                }
            }

            pattern.CsvPatternColumns = csvPatternColumns;

            #endregion

            // パネル別でパターンを付加パターンを作成
            this.pnlLogic.PanelEntityCreate(pattern);

            return pattern;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <returns></returns>
        private bool EntityNewRegist(bool errorFlag, PatternDto pattern)
        {
            LogUtility.DebugMethodStart(errorFlag, pattern);
            bool ret = errorFlag;

            try
            {
                if (ret)
                    if (this.patternDbAccessor.InsertCsvPattern(pattern.CsvPattern) == 0)
                        // 更新行が当たらない
                        ret = false;

                if (ret)
                    if (this.patternDbAccessor.InsertCsvPatternColumns(pattern.CsvPatternColumns) != pattern.CsvPatternColumns.Count)
                        // 更新行が更新予定行と異なり
                        ret = false;

                if (ret)
                    if (this.pnlLogic.PanelNewRegist(errorFlag, pattern) == 0)
                        ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EntityNewRegist", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private bool EntityExistDelete(bool errorFlag)
        {
            LogUtility.DebugMethodStart();
            bool ret = errorFlag;

            try
            {
                // 削除に設定
                this.callForm.Pattern.CsvPattern.DELETE_FLG = true;

                if (ret)
                {
                    if (this.patternDbAccessor.UpdateCsvPattern(this.callForm.Pattern.CsvPattern) == 0)
                        // 更新行が当たらない
                        ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("EntityExistDelete", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        internal bool CsvOutput()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                if (ret)
                    ret = this.FormCheck();

                if (ret)
                    switch (this.callForm.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            if (ret)
                                if (this.msgLogic.MessageBoxShowConfirm("CSV出力項目パターンを登録しますか？") == DialogResult.Yes) // TODO: メッセージをID化
                                {
                                    ret = this.RegistEx(true);
                                    // 登録したら再検索を行う
                                    if (ret)
                                        this.Search();
                                }
                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            if (ret)
                                if (this.msgLogic.MessageBoxShowConfirm("CSV出力項目パターンを登録しますか？") == DialogResult.Yes) // TODO: メッセージをID化
                                {
                                    ret = this.UpdateEx(true);
                                    // 登録したら再検索を行う
                                    if (ret)
                                        this.Search();
                                }
                            break;

                        default:
                            break;
                    }

                if (ret)
                    new CsvOutputUtility(this.callForm, this.callForm.Joken, this.EntityCreate(false)).CsvOutput();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CsvOutput", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        [Transaction]
        public bool DeleteEx()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                if (this.msgLogic.MessageBoxShow("C026") == DialogResult.Yes)
                    using (var trans = new TransactionUtility())
                    {
                        var pattern = this.EntityCreate();
                        if (this.EntityExistDelete(true) && // 既存更新(DELETE_FLAGを削除に更新)
                            this.EntityNewRegist(true, pattern)) // 新規(削除データ)
                        {
                            trans.Commit();
                            this.msgLogic.MessageBoxShow("I001", "削除");
                        }
                    }
                else
                    ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public bool RegistEx(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            bool ret = true;

            try
            {
                if (this.FormCheck())
                    using (var trans = new TransactionUtility())
                    {
                        var pattern = this.EntityCreate();
                        if (this.EntityNewRegist(errorFlag, pattern))
                        {
                            // 画面モードと初期データ再設定
                            this.callForm.Pattern.CsvPattern = pattern.CsvPattern;
                            this.callForm.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                            trans.Commit();

                            this.msgLogic.MessageBoxShow("I001", "登録");
                        }
                    }
                else
                    ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public bool UpdateEx(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            bool ret = true;

            try
            {
                if (this.FormCheck())
                    using (var trans = new TransactionUtility())
                    {
                        var pattern = this.EntityCreate();
                        if (this.EntityExistDelete(errorFlag) && // 既存更新(DELETE_FLAGを削除に更新)
                            this.EntityNewRegist(errorFlag, pattern)) // 新規(更新データ)
                        {
                            // 初期データ再設定
                            this.callForm.Pattern.CsvPattern.SYSTEM_ID = pattern.CsvPattern.SYSTEM_ID;
                            this.callForm.Pattern.CsvPattern.SEQ = pattern.CsvPattern.SEQ;

                            trans.Commit();

                            this.msgLogic.MessageBoxShow("I001", "登録");
                        }
                    }
                else
                    ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Update", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #region インタフェース実装

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns>結果件数</returns>
        [Transaction]
        public int Search()
        {
            LogUtility.DebugMethodStart();
            int ret = 0;

            try
            {
                switch (this.callForm.WindowType)
                {
                    case r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case r_framework.Const.WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.callForm.Pattern.CsvPattern =
                            this.patternDbAccessor.GetCsvPattern(this.callForm.Pattern.HaniKbn, this.callForm.Pattern.CsvPattern);
                        this.callForm.Pattern.CsvPatternColumns =
                            this.patternDbAccessor.GetCsvPatternColumns(this.callForm.Pattern.CsvPattern);

                        if (this.callForm.Pattern == null ||
                            this.callForm.Pattern.CsvPatternColumns == null || this.callForm.Pattern.CsvPatternColumns.Count == 0)
                        {
                            this.msgLogic.MessageBoxShow("E045");
                            return ret;
                        }
                        else
                        {
                            ret = this.callForm.Pattern.CsvPatternColumns.Count;
                        }

                        break;

                    default:
                        break;
                }

                // 上部パネル独自検索
                this.pnlLogic.PanelSearch();

                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                return ret;
            }
            finally
            {
                // 取得件数
                LogUtility.DebugMethodEnd(ret);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }

    /// <summary>
    ///
    /// </summary>
    internal class SelectRowComparer : IEqualityComparer<DataRow>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(DataRow x, DataRow y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            var x1 = x["OUTPUT_KBN"].ToString();
            var x2 = x["ID"].ToString();
            var x3 = x["DISP_NAME"].ToString();
            var y1 = y["OUTPUT_KBN"].ToString();
            var y2 = y["ID"].ToString();
            var y3 = y["DISP_NAME"].ToString();

            if (x1.Equals(y1) && x2.Equals(y2) && x3.Equals(y3))
                return true;

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(DataRow obj)
        {
            if (object.ReferenceEquals(obj, null))
                return 0;

            var val = obj["OUTPUT_KBN"].ToString() + obj["ID"].ToString() + obj["DISP_NAME"].ToString();
            return val.GetHashCode();
        }
    }
}