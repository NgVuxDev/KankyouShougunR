using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Stock.ZaikoHinmeiHuriwake
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>ボタンの設定用ファイルパス</summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Stock.ZaikoHinmeiHuriwake.Setting.ButtonSetting.xml";

        private UIForm form = null;
        private UIHeader header = null;
        private BusinessBaseForm parent = null;

        private M_SYS_INFO mSysInfo = null;
        private MessageBoxShowLogic MsgBox;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal LogicClass(UIForm form)
        {
            LogUtility.DebugMethodStart(form);

            this.form = form;

            this.mSysInfo = new DBAccessor().GetSysInfo();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(form);
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parent = this.form.Parent as BusinessBaseForm;
                this.header = this.parent.headerForm as UIHeader;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
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
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSettingLoader = new ButtonSetting();
            var buttonSettings =
                buttonSettingLoader.LoadButtonSetting(Assembly.GetExecutingAssembly(), this.ButtonInfoXmlPath);
            ButtonControlUtility.SetButtonInfo(buttonSettings, parent, WINDOW_TYPE.NONE); // ポップアップフォーム

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            this.parent.bt_func1.Click -= this.form.bt_func1_Click;
            this.parent.bt_func1.Click += this.form.bt_func1_Click;
            this.parent.bt_func9.Click -= this.form.bt_func9_Click;
            this.parent.bt_func9.Click += this.form.bt_func9_Click;
            this.parent.bt_func10.Click -= this.form.bt_func10_Click;
            this.parent.bt_func10.Click += this.form.bt_func10_Click;
            this.parent.bt_func11.Click -= this.form.bt_func11_Click;
            this.parent.bt_func11.Click += this.form.bt_func11_Click;
            this.parent.bt_func12.Click -= this.form.bt_func12_Click;
            this.parent.bt_func12.Click += this.form.bt_func12_Click;

            this.parent.FormClosing -= this.form.parentForm_FormClosing;
            this.parent.FormClosing += this.form.parentForm_FormClosing;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 初期画面項目値設定
        /// <summary>
        /// 
        /// </summary>
        internal bool EntryInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.header.txtHinmeiCd.Text = this.header.HinmeiCd;
                this.header.txtHinmeiName.Text = this.header.HinmeiName;
                this.form.txtNetJyuuryou.Text = this.form.NetJyuuryou.ToString(mSysInfo.SYS_JYURYOU_FORMAT);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EntryInit", ex);
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
        /// 
        /// </summary>
        internal bool DetailInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var zaikoTable = this.form.ZaikoTable;
                var zaikoMultiRow = this.form.gcZaikoHinmei;

                zaikoMultiRow.BeginEdit(false);

                zaikoMultiRow.Rows.Clear();
                if (zaikoTable.Rows.Count > 0)
                {
                    // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
                    zaikoMultiRow.Rows.Add(zaikoTable.Rows.Count);

                    this.MultiRowSet(zaikoTable);
                }

                zaikoMultiRow.EndEdit();
                zaikoMultiRow.NotifyCurrentCellDirty(false);
                SelectionActions.MoveToFirstCell.Execute(zaikoMultiRow);

                // 在庫比率・量合計再計算
                this.GoukeiCalculate();
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 比率クリア
        /// <summary>
        /// 比率クリア
        /// </summary>
        /// <remark>
        /// 明細の各在庫比率と在庫量をクリアし、
        /// 合計もクリアする。
        /// </remark>
        internal bool AllHiritsusClear()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                foreach (var row in this.form.gcZaikoHinmei.Rows)
                {
                    if (!this.CurrentHiritsuClear(row, false))
                    {
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }

                this.GoukeiCalculate();
            }
            catch (Exception ex)
            {
                LogUtility.Error("AllHiritsusClear", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 戻り値設定
        /// <summary>
        /// 
        /// </summary>
        internal bool ResultReturn()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var zaikoTable = this.form.ZaikoTable;
                var zaikoMultiRow = this.form.gcZaikoHinmei;

                zaikoMultiRow.EndEdit();

                zaikoTable.Rows.Clear();
                zaikoTable.Merge(this.MultiRowSave(), false, MissingSchemaAction.Ignore);
                for (int i = zaikoTable.Rows.Count - 1; i >= 0; i--)
                {
                    var dr = zaikoTable.Rows[i];
                    // どちらが空の場合該当データを飛ばす(実際一方だけは空の状況が存在しないはず)
                    if (Convert.IsDBNull(dr["ZAIKO_HINMEI_CD"]) || dr["ZAIKO_HINMEI_CD"] == null || string.IsNullOrEmpty(Convert.ToString(dr["ZAIKO_HINMEI_CD"])) ||
                        Convert.IsDBNull(dr["ZAIKO_HIRITSU"]) || dr["ZAIKO_HIRITSU"] == null || Convert.ToInt16(dr["ZAIKO_HIRITSU"]) == 0)
                    {
                        zaikoTable.Rows.Remove(dr);
                    }
                }
                zaikoTable.AcceptChanges();

                this.form.DialogResult = DialogResult.OK;
                this.header.DialogResult = DialogResult.OK;
                this.parent.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ResultReturn", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 行・合計計算又はクリア
        // 各計算とクリアはGcMultiRowのDataSource(this.form.DetailSourceTable)を直接処理し、
        // GcMultiRow.Refresh()で画面に反映する。
        /// <summary>
        /// 行在庫量計算
        /// </summary>
        /// <param name="row"></param>
        internal bool ZaikoRyouCalculate(Row row)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(row);

                if (row == null)
                {
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                this.form.gcZaikoHinmei.BeginEdit(false);

                if (!Convert.IsDBNull(row.Cells["ZAIKO_HIRITSU"].Value) && row.Cells["ZAIKO_HIRITSU"].Value != null && !string.IsNullOrEmpty(this.form.txtNetJyuuryou.Text))
                {
                    row.Cells["ZAIKO_RYOU"].Value = Convert.ToDecimal(this.form.txtNetJyuuryou.Text) * Convert.ToInt16(row.Cells["ZAIKO_HIRITSU"].Value) / 100;
                }
                else
                {
                    row.Cells["ZAIKO_RYOU"].Value = DBNull.Value;
                }

                this.form.gcZaikoHinmei.EndEdit();

                this.GoukeiCalculate();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoRyouCalculate", ex);
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
        /// 在庫比率クリア
        /// </summary>
        /// <param name="row"></param>
        /// <param name="goukeiCalculate">再計算指示</param>
        internal bool CurrentHiritsuClear(Row row, bool goukeiCalculate = true)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(row, goukeiCalculate);

                if (row == null)
                {
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                this.form.gcZaikoHinmei.EndEdit();
                row.Cells["ZAIKO_HIRITSU"].Value = DBNull.Value;
                row.Cells["ZAIKO_RYOU"].Value = DBNull.Value;

                if (goukeiCalculate)
                {
                    this.GoukeiCalculate();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CurrentHiritsuClear", ex);
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
        /// 在庫比率合計と在庫量合計計算
        /// </summary>
        private void GoukeiCalculate()
        {
            LogUtility.DebugMethodStart();

            var zaikoMultiRow = this.form.gcZaikoHinmei;
            var zaikoHiritsuGoukei = 0;
            var zaikoRyouGoukei = decimal.Zero;

            foreach (var row in zaikoMultiRow.Rows)
            {
                if (!Convert.IsDBNull(row.Cells["ZAIKO_HIRITSU"].Value) && row.Cells["ZAIKO_HIRITSU"].Value != null)
                {
                    zaikoHiritsuGoukei += Convert.ToInt16(row.Cells["ZAIKO_HIRITSU"].Value);
                }

                if (!Convert.IsDBNull(row.Cells["ZAIKO_RYOU"].Value) && row.Cells["ZAIKO_RYOU"].Value != null)
                {
                    zaikoRyouGoukei += Convert.ToDecimal(row.Cells["ZAIKO_RYOU"].Value);
                }
            }

            this.form.txtZaikoHiritsuGoukei.Text = zaikoHiritsuGoukei.ToString();
            this.form.txtZaikoRyouGoukei.Text = zaikoRyouGoukei.ToString(mSysInfo.SYS_JYURYOU_FORMAT);

            // 20150420 100%の場合、合計と正味重量を一致するため、最終行の在庫量を調整する(有価在庫不具合一覧110) Start
            if (zaikoHiritsuGoukei == 100)
            {
                for (int i = zaikoMultiRow.Rows.Count - 1; i >= 0; i--)
                {
                    Row row = zaikoMultiRow.Rows[i];
                    if (!Convert.IsDBNull(row.Cells["ZAIKO_HIRITSU"].Value) && row.Cells["ZAIKO_HIRITSU"].Value != null && row.Cells["ZAIKO_RYOU"].Value != null && !Convert.IsDBNull(row.Cells["ZAIKO_RYOU"].Value))
                    {
                        row.Cells["ZAIKO_RYOU"].Value = this.form.NetJyuuryou - (zaikoRyouGoukei - Convert.ToDecimal(row.Cells["ZAIKO_RYOU"].Value));
                        break;
                    }
                }

                this.form.txtZaikoRyouGoukei.Text = this.form.NetJyuuryou.ToString(mSysInfo.SYS_JYURYOU_FORMAT);
            }
            // 20150420 100%の場合、合計と正味重量を一致するため、最終行の在庫量を調整する(有価在庫不具合一覧110) End

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 行追加・削除
        /// <summary>
        /// 行追加
        /// </summary>
        internal void AddRow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var zaikoMultiRow = this.form.gcZaikoHinmei;
                if (zaikoMultiRow.CurrentRow != null)
                {
                    zaikoMultiRow.EndEdit();

                    var zaikoCur = zaikoMultiRow.CurrentRow;

                    // 行インデックス記録
                    int zaikoCurIndexSave = zaikoCur.Index;

                    // 行追加
                    zaikoMultiRow.Rows.Insert(zaikoCurIndexSave); // インデックス利用

                    zaikoMultiRow.ClearSelection();
                    zaikoMultiRow.AddSelection(zaikoCurIndexSave);

                    zaikoMultiRow.EndEdit();
                    zaikoMultiRow.NotifyCurrentCellDirty(false);

                    // 合計再計算
                    this.GoukeiCalculate();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("AddRow", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 行削除
        /// </summary>
        internal void RemoveRow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var zaikoMultiRow = this.form.gcZaikoHinmei;
                if (zaikoMultiRow.CurrentRow != null)
                {
                    zaikoMultiRow.EndEdit();

                    var zaikoCur = zaikoMultiRow.CurrentRow;
                    if (!zaikoCur.IsNewRow)
                    {
                        // 行インデックス記録
                        int zaikoCurIndexSave = zaikoCur.Index;
                        // 削除する時、末行がクリアされたの一時対応
                        var zaikoTable = this.MultiRowSave();

                        // 行削除
                        zaikoMultiRow.Rows.Remove(zaikoCur); // 該当行利用

                        zaikoMultiRow.ClearSelection();
                        zaikoMultiRow.AddSelection(zaikoCurIndexSave);

                        zaikoMultiRow.EndEdit();
                        zaikoMultiRow.NotifyCurrentCellDirty(false);

                        // 削除する時、末行がクリアされたの一時対応
                        this.MultiRowSet(zaikoTable, zaikoCurIndexSave);

                        // 合計再計算
                        this.GoukeiCalculate();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RemoveRow", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region IBuisinessLogic CRUD実装
        // 本画面DB関連はないので、全部実装しない。
        // 在庫品名CDポップ検索を本セクション外とみなす。
        public int Search()
        {
            // noop
            return 0;
        }
        public void Regist(bool errorFlag)
        {
            // noop
        }
        public void Update(bool errorFlag)
        {
            // noop
        }
        public void LogicalDelete()
        {
            // noop
        }
        public void PhysicalDelete()
        {
            // noop
        }
        #endregion

        #region チェック
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool ZaikoHinmeiCdMandatoryCheck()
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 各行チェック
                this.AddRequiredSetting();
                var autoCheckLogic = new AutoRegistCheckLogic(this.form.GetAllControl(), this.form.GetAllControl());
                if (autoCheckLogic.AutoRegistCheck())
                {
                    returnVal = false;
                }
                this.RemoveRequiredSetting();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoHinmeiCdMandatoryCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool ZaikoHinmeiCdDuplicationCheck()
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();

                var zaikoMultiRow = this.form.gcZaikoHinmei;

                var cell = zaikoMultiRow[zaikoMultiRow.CurrentRow.Index, "ZAIKO_HINMEI_CD"] as GcCustomTextBoxCell;
                if (cell != null &&
                    !Convert.IsDBNull(cell.Value) && cell.Value != null && !string.IsNullOrEmpty(Convert.ToString(cell.Value)) &&
                    !Convert.IsDBNull(cell.EditedFormattedValue) && cell.EditedFormattedValue != null && !string.IsNullOrEmpty(Convert.ToString(cell.EditedFormattedValue)) &&
                    Convert.ToString(cell.Value).ToUpper().Equals(Convert.ToString(cell.EditedFormattedValue).ToUpper()))
                {
                    var cells = new List<Cell>();
                    foreach (Row row in zaikoMultiRow.Rows)
                    {
                        if (row.IsNewRow || row.Index == zaikoMultiRow.CurrentRow.Index)
                        {
                            continue;
                        }

                        cells.Add(row.Cells["ZAIKO_HINMEI_CD"]);
                    }

                    Validator validator = new Validator(cell, cells.ToArray());
                    string returnMessage = validator.DuplicationCheck();
                    if (!string.IsNullOrEmpty(returnMessage))
                    {
                        this.form.messageShowLogic.MessageBoxShow("E003", "在庫品名CD", cell.Value.ToString());
                        returnVal = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoHinmeiCdDuplicationCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// 更新前在庫比率チェック
        /// </summary>
        /// <returns></returns>
        internal bool GoukeiHiritsuCheck()
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.form.gcZaikoHinmei.EndEdit();
                this.GoukeiCalculate();

                // long型に変換する(加算値オーバー防止)
                if (Convert.ToInt64(this.form.txtZaikoHiritsuGoukei.Text) > 100)
                {
                    this.form.messageShowLogic.MessageBoxShowError("在庫比率の合計が100%以下になるように入力してください。");
                    returnVal = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GoukeiHiritsuCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region ユーティリティ
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable MultiRowSave()
        {
            DataTable zaikoTable = this.form.ZaikoTable.DefaultView.ToTable();
            zaikoTable.Rows.Clear();

            var zaikoMultiRow = this.form.gcZaikoHinmei;
            for (int i = 0; i < zaikoMultiRow.Rows.Count - 1; i++)
            {
                var row = zaikoMultiRow.Rows[i];
                DataRow dr = zaikoTable.NewRow();
                dr["ZAIKO_HINMEI_CD"] = row.Cells["ZAIKO_HINMEI_CD"].Value;
                dr["ZAIKO_HINMEI_NAME"] = row.Cells["ZAIKO_HINMEI_NAME"].Value;
                // 20150420 ブランク行削除又は登録する時エラーが発生するバグ修正(有価在庫不具合一覧103、104) Start
                //dr["ZAIKO_HIRITSU"] = row.Cells["ZAIKO_HIRITSU"].Value;
                //dr["ZAIKO_TANKA"] = row.Cells["ZAIKO_TANKA"].Value;
                //dr["ZAIKO_RYOU"] = row.Cells["ZAIKO_RYOU"].Value;
                dr["ZAIKO_HIRITSU"] = row.Cells["ZAIKO_HIRITSU"].Value == null ? DBNull.Value : row.Cells["ZAIKO_HIRITSU"].Value;
                dr["ZAIKO_TANKA"] = row.Cells["ZAIKO_TANKA"].Value == null ? DBNull.Value : row.Cells["ZAIKO_TANKA"].Value;
                dr["ZAIKO_RYOU"] = row.Cells["ZAIKO_RYOU"].Value == null ? DBNull.Value : row.Cells["ZAIKO_RYOU"].Value;
                // 20150420 ブランク行削除又は登録する時エラーが発生するバグ修正(有価在庫不具合一覧103、104) End

                zaikoTable.Rows.Add(dr);
            }
            return zaikoTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zaikoTable"></param>
        /// <param name="removeAtIndex"></param>
        private void MultiRowSet(DataTable zaikoTable, int removeAtIndex = -1)
        {
            if (removeAtIndex >= 0 && removeAtIndex < zaikoTable.Rows.Count)
            {
                zaikoTable.Rows.RemoveAt(removeAtIndex);
            }

            var zaikoMultiRow = this.form.gcZaikoHinmei;
            for (int i = 0; i < zaikoTable.Rows.Count; i++)
            {
                var row = zaikoMultiRow.Rows[i];
                DataRow dr = zaikoTable.Rows[i];

                row.Cells["ZAIKO_HINMEI_CD"].Value = dr["ZAIKO_HINMEI_CD"];
                row.Cells["ZAIKO_HINMEI_NAME"].Value = dr["ZAIKO_HINMEI_NAME"];
                row.Cells["ZAIKO_HIRITSU"].Value = dr["ZAIKO_HIRITSU"];
                row.Cells["ZAIKO_TANKA"].Value = dr["ZAIKO_TANKA"];
                row.Cells["ZAIKO_RYOU"].Value = dr["ZAIKO_RYOU"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void AddRequiredSetting()
        {
            this.RemoveRequiredSetting();

            SelectCheckDto mandatoryCheck = new SelectCheckDto();
            mandatoryCheck.CheckMethodName = "必須チェック";

            Collection<SelectCheckDto> mandatoryChecks = new Collection<SelectCheckDto>();
            mandatoryChecks.Add(mandatoryCheck);

            foreach (var row in this.form.gcZaikoHinmei.Rows)
            {
                // 比率に入力値がある場合のみチェックする
                if (!Convert.IsDBNull(row.Cells["ZAIKO_HIRITSU"].Value) && row.Cells["ZAIKO_HIRITSU"].Value != null &&
                    Convert.ToInt16(row.Cells["ZAIKO_HIRITSU"].Value) != 0)
                {
                    PropertyUtility.SetValue(row.Cells["ZAIKO_HINMEI_CD"], "RegistCheckMethod", mandatoryChecks);
                }

                // 20150424 比率に必須チェックを追加 Start
                if (!Convert.IsDBNull(row.Cells["ZAIKO_HINMEI_CD"].Value) && row.Cells["ZAIKO_HINMEI_CD"].Value != null &&
                    !string.IsNullOrEmpty(Convert.ToString(row.Cells["ZAIKO_HINMEI_CD"].Value)))
                {
                    PropertyUtility.SetValue(row.Cells["ZAIKO_HIRITSU"], "RegistCheckMethod", mandatoryChecks);
                }
                // 20150424 比率に必須チェックを追加 End
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void RemoveRequiredSetting()
        {
            this.form.gcZaikoHinmei.SuspendLayout();

            foreach (var row in this.form.gcZaikoHinmei.Rows)
            {
                PropertyUtility.SetValue(row.Cells["ZAIKO_HINMEI_CD"], "RegistCheckMethod", null);
                // 20150424 比率に必須チェックを追加 Start
                PropertyUtility.SetValue(row.Cells["ZAIKO_HIRITSU"], "RegistCheckMethod", null);
                // 20150424 比率に必須チェックを追加 End
            }

            this.form.gcZaikoHinmei.ResumeLayout();
        }
        #endregion
    }
}
