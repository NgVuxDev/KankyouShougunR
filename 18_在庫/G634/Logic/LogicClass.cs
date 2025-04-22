using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Stock.ZaikoKanriHyo.Accessor;
using Shougun.Core.Stock.ZaikoKanriHyo.Const;
using Shougun.Core.Stock.ZaikoKanriHyo.DTO;

namespace Shougun.Core.Stock.ZaikoKanriHyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// 在庫管理表画面Form
        /// </summary>
        private ZaikoKanriHyo.UIForm form;

        /// <summary>
        /// 範囲条件指定結果
        /// </summary>
        private UIConstans.ConditionInfo param { get; set; }

        /// <summary>
        /// DBAccessor
        /// </summary>
        private DBAccessor dba;

        /// <summary>
        /// ParentForm
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// 範囲条件にて指定した範囲の全てのDataTable
        /// </summary>
        private DataTable allDataTable;

        /// <summary>
        /// 帳票表示用会社名
        /// </summary>
        private string corpName;

        /// <summary>
        /// システム情報
        /// </summary>
        private M_SYS_INFO sysInfo;

        private MessageBoxShowLogic MsgBox;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dba = new DBAccessor();
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

        #region メソッド

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
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 画面表示項目初期化
                this.SetInitDisp();

                this.sysInfo = dba.GetSysInfoData();
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

        /// <summary>
        /// 印刷処理
        /// </summary>
        internal void Print()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if ((this.param.OutPutKBN == 1 && this.form.zaikoKanriHyo1.RowCount == 0)
                    || (this.param.OutPutKBN == 2 && this.form.zaikoKanriHyo2.RowCount == 0))
                {
                    // データなし時はやらない
                    return;
                }

                decimal preZaikoRyou = 0;
                decimal preZaikoKingaku = 0;
                decimal ukeireRyou = 0;
                decimal shukkaRyou = 0;
                decimal idouChouseiRyou = 0;
                decimal nowZaikoRyou = 0;
                decimal nowZaikoKingaku = 0;
                decimal totalZaikoRyou = 0;
                decimal totalZaikoKingaku = 0;

                // 業者名称取得
                string gyoushaFrom = string.Empty;
                string gyoushaTo = string.Empty;
                M_GYOUSHA GyoushaFromEntity = this.dba.gyoushaDao.GetDataByCd(param.GyoushaCdFrom);
                M_GYOUSHA GyoushaToEntity = this.dba.gyoushaDao.GetDataByCd(param.GyoushaCdTo);
                if (GyoushaFromEntity != null)
                {
                    gyoushaFrom = GyoushaFromEntity.GYOUSHA_NAME_RYAKU;
                }
                if (GyoushaToEntity != null)
                {
                    gyoushaTo = GyoushaToEntity.GYOUSHA_NAME_RYAKU;
                }

                // 現場名称取得
                string genbaFrom = string.Empty;
                string genbaTo = string.Empty;
                if (param.GyoushaCdFrom == param.GyoushaCdTo)
                {
                    M_GENBA genba = new M_GENBA();
                    genba.GYOUSHA_CD = param.GyoushaCdFrom;
                    genba.GENBA_CD = param.GenbaCdFrom;
                    M_GENBA GenbaFromEntity = this.dba.genbaDao.GetDataByCd(genba);
                    genba.GENBA_CD = param.GenbaCdTo;
                    M_GENBA GenbaToEntity = this.dba.genbaDao.GetDataByCd(genba);
                    if (GenbaFromEntity != null)
                    {
                        genbaFrom = GenbaFromEntity.GENBA_NAME_RYAKU;
                    }
                    if (GenbaToEntity != null)
                    {
                        genbaTo = GenbaToEntity.GENBA_NAME_RYAKU;
                    }
                }

                // 在庫品名名称取得
                string zaikoHinmeiFrom = string.Empty;
                string zaikoHinmeiTo = string.Empty;
                M_ZAIKO_HINMEI ZaikoHinmeiFromEntity = this.dba.zaikoHinmeiDao.GetDataByCd(param.ZaikoHinmeiCdFrom);
                M_ZAIKO_HINMEI ZaikoHinmeiToEntity = this.dba.zaikoHinmeiDao.GetDataByCd(param.ZaikoHinmeiCdTo);
                if (ZaikoHinmeiFromEntity != null)
                {
                    zaikoHinmeiFrom = ZaikoHinmeiFromEntity.ZAIKO_HINMEI_NAME_RYAKU;
                }
                if (ZaikoHinmeiToEntity != null)
                {
                    zaikoHinmeiTo = ZaikoHinmeiToEntity.ZAIKO_HINMEI_NAME_RYAKU;
                }

                DataTable dt = new DataTable();

                dt.Columns.Add();

                System.Text.StringBuilder sBuilder;

                DataRow dr;

                dr = dt.NewRow();

                sBuilder = new StringBuilder();

                sBuilder.Append("\"");
                sBuilder.Append("1-1");
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.DateFrom.ToString("yyyy/MM/dd"));
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.DateTo.ToString("yyyy/MM/dd"));
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.GyoushaCdFrom);
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.GyoushaCdTo);
                sBuilder.Append("\",\"");
                sBuilder.Append(gyoushaFrom);
                sBuilder.Append("\",\"");
                sBuilder.Append(gyoushaTo);
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.GenbaCdFrom);
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.GenbaCdTo);
                sBuilder.Append("\",\"");
                sBuilder.Append(genbaFrom);
                sBuilder.Append("\",\"");
                sBuilder.Append(genbaTo);
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.ZaikoHinmeiCdFrom);
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.ZaikoHinmeiCdTo);
                sBuilder.Append("\",\"");
                sBuilder.Append(zaikoHinmeiFrom);
                sBuilder.Append("\",\"");
                sBuilder.Append(zaikoHinmeiTo);
                sBuilder.Append("\",\"");
                sBuilder.Append(sysInfo.ZAIKO_HYOUKA_HOUHOU.Value);
                sBuilder.Append("\",\"");
                sBuilder.Append(corpName);
                sBuilder.Append("\"");

                dr[0] = sBuilder.ToString();
                dt.Rows.Add(dr);

                if (this.param.OutPutKBN == 1)
                {
                    foreach (Row row in this.form.zaikoKanriHyo1.Rows)
                    {
                        dr = dt.NewRow();

                        sBuilder = new StringBuilder();

                        sBuilder.Append("\"");
                        sBuilder.Append("2-1");
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["GYOUSHA_CD"].Value.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["GYOUSHA_NAME"].Value.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["GENBA_CD"].Value.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["GENBA_NAME"].Value.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["ZAIKO_HINMEI_CD"].Value.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["ZAIKO_HINMEI_NAME"].Value.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["KUROKOSHI_ZAIKO_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["KUROKOSHI_ZAIKO_KINGAKU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["UKEIRE_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["SHUKKA_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["IDOU_CHOUSEI_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["TOUGETU_ZAIKO_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["TOUGETU_ZAIKO_KINGAKU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["GOUKEI_ZAIKO_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["GOUKEI_ZAIKO_KINGAKU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["ZAIKO_TANKA"].FormattedValue.ToString());
                        sBuilder.Append("\"");

                        dr[0] = sBuilder.ToString();
                        dt.Rows.Add(dr);

                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["KUROKOSHI_ZAIKO_RYOU"].Value)))
                        {
                            preZaikoRyou += decimal.Parse(row.Cells["KUROKOSHI_ZAIKO_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["KUROKOSHI_ZAIKO_KINGAKU"].Value)))
                        {
                            preZaikoKingaku += decimal.Parse(row.Cells["KUROKOSHI_ZAIKO_KINGAKU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["UKEIRE_RYOU"].Value)))
                        {
                            ukeireRyou += decimal.Parse(row.Cells["UKEIRE_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["SHUKKA_RYOU"].Value)))
                        {
                            shukkaRyou += decimal.Parse(row.Cells["SHUKKA_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["IDOU_CHOUSEI_RYOU"].Value)))
                        {
                            idouChouseiRyou += decimal.Parse(row.Cells["IDOU_CHOUSEI_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["TOUGETU_ZAIKO_RYOU"].Value)))
                        {
                            nowZaikoRyou += decimal.Parse(row.Cells["TOUGETU_ZAIKO_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["TOUGETU_ZAIKO_KINGAKU"].Value)))
                        {
                            nowZaikoKingaku += decimal.Parse(row.Cells["TOUGETU_ZAIKO_KINGAKU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["GOUKEI_ZAIKO_RYOU"].Value)))
                        {
                            totalZaikoRyou += decimal.Parse(row.Cells["GOUKEI_ZAIKO_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["GOUKEI_ZAIKO_KINGAKU"].Value)))
                        {
                            totalZaikoKingaku += decimal.Parse(row.Cells["GOUKEI_ZAIKO_KINGAKU"].Value.ToString());
                        }
                    }
                }
                else
                {
                    foreach (Row row in this.form.zaikoKanriHyo2.Rows)
                    {
                        dr = dt.NewRow();

                        sBuilder = new StringBuilder();

                        sBuilder.Append("\"");
                        sBuilder.Append("2-1");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["ZAIKO_HINMEI_CD"].Value.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["ZAIKO_HINMEI_NAME"].Value.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["KUROKOSHI_ZAIKO_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["KUROKOSHI_ZAIKO_KINGAKU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["UKEIRE_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["SHUKKA_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["IDOU_CHOUSEI_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["TOUGETU_ZAIKO_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["TOUGETU_ZAIKO_KINGAKU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["GOUKEI_ZAIKO_RYOU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["GOUKEI_ZAIKO_KINGAKU"].FormattedValue.ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(row.Cells["ZAIKO_TANKA"].FormattedValue.ToString());
                        sBuilder.Append("\"");

                        dr[0] = sBuilder.ToString();
                        dt.Rows.Add(dr);

                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["KUROKOSHI_ZAIKO_RYOU"].Value)))
                        {
                            preZaikoRyou += decimal.Parse(row.Cells["KUROKOSHI_ZAIKO_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["KUROKOSHI_ZAIKO_KINGAKU"].Value)))
                        {
                            preZaikoKingaku += decimal.Parse(row.Cells["KUROKOSHI_ZAIKO_KINGAKU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["UKEIRE_RYOU"].Value)))
                        {
                            ukeireRyou += decimal.Parse(row.Cells["UKEIRE_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["SHUKKA_RYOU"].Value)))
                        {
                            shukkaRyou += decimal.Parse(row.Cells["SHUKKA_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["IDOU_CHOUSEI_RYOU"].Value)))
                        {
                            idouChouseiRyou += decimal.Parse(row.Cells["IDOU_CHOUSEI_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["TOUGETU_ZAIKO_RYOU"].Value)))
                        {
                            nowZaikoRyou += decimal.Parse(row.Cells["TOUGETU_ZAIKO_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["TOUGETU_ZAIKO_KINGAKU"].Value)))
                        {
                            nowZaikoKingaku += decimal.Parse(row.Cells["TOUGETU_ZAIKO_KINGAKU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["GOUKEI_ZAIKO_RYOU"].Value)))
                        {
                            totalZaikoRyou += decimal.Parse(row.Cells["GOUKEI_ZAIKO_RYOU"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["GOUKEI_ZAIKO_KINGAKU"].Value)))
                        {
                            totalZaikoKingaku += decimal.Parse(row.Cells["GOUKEI_ZAIKO_KINGAKU"].Value.ToString());
                        }
                    }
                }

                dr = dt.NewRow();

                sBuilder = new StringBuilder();
                string formatKingaku = "{0:#,##0}";

                sBuilder.Append("\"");
                sBuilder.Append("2-2");
                sBuilder.Append("\",\"");
                sBuilder.Append(preZaikoRyou.ToString(sysInfo.SYS_JYURYOU_FORMAT));
                sBuilder.Append("\",\"");
                sBuilder.Append(string.Format(formatKingaku, preZaikoKingaku));
                sBuilder.Append("\",\"");
                sBuilder.Append(ukeireRyou.ToString(sysInfo.SYS_JYURYOU_FORMAT));
                sBuilder.Append("\",\"");
                sBuilder.Append(shukkaRyou.ToString(sysInfo.SYS_JYURYOU_FORMAT));
                sBuilder.Append("\",\"");
                sBuilder.Append(idouChouseiRyou.ToString(sysInfo.SYS_JYURYOU_FORMAT));
                sBuilder.Append("\",\"");
                sBuilder.Append(nowZaikoRyou.ToString(sysInfo.SYS_JYURYOU_FORMAT));
                sBuilder.Append("\",\"");
                sBuilder.Append(string.Format(formatKingaku, nowZaikoKingaku));
                sBuilder.Append("\",\"");
                sBuilder.Append(totalZaikoRyou.ToString(sysInfo.SYS_JYURYOU_FORMAT));
                sBuilder.Append("\",\"");
                sBuilder.Append(string.Format(formatKingaku, totalZaikoKingaku));
                sBuilder.Append("\"");

                dr[0] = sBuilder.ToString();
                dt.Rows.Add(dr);

                ReportInfoR634 report_r634 = new ReportInfoR634();

                report_r634.R634_Reprt(dt, this.param.OutPutKBN);

                // 印刷ポツプアップ画面表示
                using (FormReportPrintPopup report = new FormReportPrintPopup(report_r634))
                {
                    report.ReportCaption = "在庫管理表";
                    report.ShowDialog();
                    report.Dispose();
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
        /// CSV出力処理
        /// </summary>
        internal void CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();

                // CSV出力用のMultiRowを作成
                var EditMultiRow = this.form.zaikoKanriHyo1;
                if (this.param.OutPutKBN == 2)
                {
                    EditMultiRow = this.form.zaikoKanriHyo2;
                }

                // 一覧に明細行がない場合
                if (EditMultiRow.RowCount == 0)
                {
                    // アラートを表示し、CSV出力処理はしない
                    msgLogic.MessageBoxShow("E044");
                }
                else
                {
                    // CSV出力確認メッセージを表示する
                    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        // DataGridView生成
                        var dgv = new CustomDataGridView();
                        dgv.Visible = false;

                        if (this.param.OutPutKBN == 1)
                        {
                            EditMultiRow.ColumnHeaders[0].Cells["GYOUSHA_CD_LABEL"].Value = "業者CD";
                            EditMultiRow.ColumnHeaders[0].Cells["GENBA_CD_LABEL"].Value = "現場CD";
                        }
                        EditMultiRow.ColumnHeaders[0].Cells["ZAIKO_HINMEI_CD_LABEL"].Value = "在庫品名CD";

                        this.form.Controls.Add(dgv);

                        string name = "";
                        string value = "";
                        DataTable table = new DataTable();
                        List<string> list = new List<string>();
                        foreach (Cell cell in EditMultiRow.ColumnHeaders[0].Cells)
                        {
                            name = "";
                            value = "";
                            if (!string.IsNullOrEmpty(cell.Name))
                            {
                                name = cell.Name.Replace("_LABEL", "");
                            }
                            value = Convert.ToString(cell.Value);

                            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                            {
                                // 表示しているものをTableとして登録
                                table.Columns.Add(name, typeof(string));
                                list.Add(value);
                            }
                        }

                        foreach (Row mRow in EditMultiRow.Rows)
                        {
                            DataRow row = table.NewRow();
                            foreach (DataColumn Column in table.Columns)
                            {
                                row[Column.ColumnName] = mRow[Column.ColumnName].FormattedValue;
                            }
                            table.Rows.Add(row);
                        }

                        int i = 0;
                        foreach (DataColumn Column in table.Columns)
                        {
                            name = list[i];
                            Column.ColumnName = name.Trim();
                            i++;
                        }

                        dgv.DataSource = table;
                        dgv.Refresh();

                        if (this.param.OutPutKBN == 1)
                        {
                            EditMultiRow.ColumnHeaders[0].Cells["GYOUSHA_CD_LABEL"].Value = "業者";
                            EditMultiRow.ColumnHeaders[0].Cells["GENBA_CD_LABEL"].Value = "現場";
                        }
                        EditMultiRow.ColumnHeaders[0].Cells["ZAIKO_HINMEI_CD_LABEL"].Value = "在庫品名";

                        // 共通部品を利用して、画面に表示されているデータをCSVに出力する
                        var CSVExport = new CSVExport();
                        CSVExport.ConvertCustomDataGridViewToCsv(dgv, true, true, "在庫管理表", this.form);
                    }
                }
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
        /// 在庫管理表範囲条件指定画面表示処理
        /// </summary>
        /// <param name="type">画面種別</param>
        internal void ShowPopUp()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ループフラグ
                Boolean searchFlg = true;

                // 受渡項目生成
                this.param = this.CreateParams();

                // 売上範囲条件指定画面表示
                var PopUpForm = new PopupForm(this.param);

                while (searchFlg)
                {
                    PopUpForm.ShowDialog();

                    // 実行結果
                    switch (PopUpForm.DialogResult)
                    {
                        case DialogResult.OK:		// 「F9:検索実行」押下
                            // 結果を次回条件値に保存
                            this.param = PopUpForm.Joken;

                            // 指定された範囲における全てのデータ取得
                            Cursor preCursor = Cursor.Current;
                            Cursor.Current = Cursors.WaitCursor;
                            bool hadSearched = false;
                            this.allDataTable = this.dba.GetIchiranData(this.param, this.form, ref hadSearched);
                            Cursor.Current = preCursor;

                            if (this.allDataTable == null || this.allDataTable.Rows.Count == 0)
                            {
                                PopUpForm.Dispose();
                                // 売上範囲条件指定画面表示
                                PopUpForm = new PopupForm(this.param);
                                // 画面表示初期化
                                this.SetInitDisp();

                                if (this.param.OutPutKBN == 1)
                                {
                                    this.form.zaikoKanriHyo1.Visible = true;
                                    this.form.zaikoKanriHyo2.Visible = false;
                                }
                                else
                                {
                                    this.form.zaikoKanriHyo1.Visible = false;
                                    this.form.zaikoKanriHyo2.Visible = true;
                                }
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
                                // 表示項目更新
                                this.headerForm.dtp_DenpyoDateFrom.Value = this.param.DateFrom;
                                this.headerForm.dtp_DenpyoDateTo.Value = this.param.DateTo;

                                // 一覧画面更新
                                if (!this.SetIchiran()) { return; }
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
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.form.zaikoKanriHyo1.Rows.Clear();
                this.form.zaikoKanriHyo2.Rows.Clear();

                DataView dv = new DataView();
                this.allDataTable.TableName = "在庫管理表";
                dv.Table = this.allDataTable;

                string formatTanka = "{0:" + sysInfo.SYS_TANKA_FORMAT + "}";
                string formatKingaku = "{0:#,##0}";

                //フィルタ結果からデータテーブルを作成
                DataTable aftsorttable;
                aftsorttable = dv.ToTable();

                // データ件数をセット
                this.headerForm.txt_YomikomiDataKensu.Text = aftsorttable.Rows.Count.ToString();
                // 取得した一覧データをDataSourceにセット
                if (this.param.OutPutKBN == 1)
                {
                    this.form.zaikoKanriHyo1.Visible = true;
                    this.form.zaikoKanriHyo2.Visible = false;

                    this.form.zaikoKanriHyo1.IsBrowsePurpose = false;

                    string gyoushaCd = "";
                    string genbaCd = "";
                    string zaikoHinmeiCd = "";
                    int cnt = 0;
                    for (int i = 0; i < aftsorttable.Rows.Count; i++)
                    {
                        gyoushaCd = Convert.ToString(aftsorttable.Rows[i]["GYOUSHA_CD"]);
                        genbaCd = Convert.ToString(aftsorttable.Rows[i]["GENBA_CD"]);
                        zaikoHinmeiCd = Convert.ToString(aftsorttable.Rows[i]["ZAIKO_HINMEI_CD"]);
                        ZaikoDTO dto = this.dba.GetZaiko1(gyoushaCd, genbaCd, zaikoHinmeiCd, this.param.DateFrom, this.param.DateTo, parentForm.sysDate.Date);
                        if (dto.preZaikoRyou == 0 && dto.ukeireRyou == 0 && dto.shukkaRyou == 0 && dto.chouseiIdouRyou == 0)
                        {
                            continue;
                        }

                        this.form.zaikoKanriHyo1.Rows.Add();
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["GYOUSHA_CD"].Value = aftsorttable.Rows[i]["GYOUSHA_CD"];
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["GYOUSHA_NAME"].Value = aftsorttable.Rows[i]["GYOUSHA_NAME"];
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["GENBA_CD"].Value = aftsorttable.Rows[i]["GENBA_CD"];
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["GENBA_NAME"].Value = aftsorttable.Rows[i]["GENBA_NAME"];
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["ZAIKO_HINMEI_CD"].Value = aftsorttable.Rows[i]["ZAIKO_HINMEI_CD"];
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["ZAIKO_HINMEI_NAME"].Value = aftsorttable.Rows[i]["ZAIKO_HINMEI_NAME"];
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["KUROKOSHI_ZAIKO_RYOU"].Value = this.formatJyuuRyou(dto.preZaikoRyou);
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["KUROKOSHI_ZAIKO_KINGAKU"].Value = string.Format(formatKingaku, dto.preZaikoKingaku);
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["UKEIRE_RYOU"].Value = this.formatJyuuRyou(dto.ukeireRyou);
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["SHUKKA_RYOU"].Value = this.formatJyuuRyou(dto.shukkaRyou);
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["IDOU_CHOUSEI_RYOU"].Value = this.formatJyuuRyou(dto.chouseiIdouRyou);
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["TOUGETU_ZAIKO_RYOU"].Value = this.formatJyuuRyou(dto.nowZaikoRyou);
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["TOUGETU_ZAIKO_KINGAKU"].Value = string.Format(formatKingaku, dto.nowZaikoKingaku);
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["GOUKEI_ZAIKO_RYOU"].Value = this.formatJyuuRyou(dto.totalZaikoRyou);
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["GOUKEI_ZAIKO_KINGAKU"].Value = string.Format(formatKingaku, dto.totalZaikoKingaku);
                        this.form.zaikoKanriHyo1.Rows[cnt].Cells["ZAIKO_TANKA"].Value = dto.zaikoTanka;
                        cnt++;
                    }

                    this.form.zaikoKanriHyo1.IsBrowsePurpose = true;
                }
                else
                {
                    this.form.zaikoKanriHyo1.Visible = false;
                    this.form.zaikoKanriHyo2.Visible = true;

                    this.form.zaikoKanriHyo2.IsBrowsePurpose = false;

                    string zaikoHinmeiCd = "";
                    int cnt = 0;
                    for (int i = 0; i < aftsorttable.Rows.Count; i++)
                    {
                        zaikoHinmeiCd = Convert.ToString(aftsorttable.Rows[i]["ZAIKO_HINMEI_CD"]);
                        ZaikoDTO dto = this.dba.GetZaiko2(zaikoHinmeiCd, this.param.DateFrom, this.param.DateTo, parentForm.sysDate.Date);
                        if (dto.preZaikoRyou == 0 && dto.ukeireRyou == 0 && dto.shukkaRyou == 0 && dto.chouseiIdouRyou == 0)
                        {
                            continue;
                        }
                        this.form.zaikoKanriHyo2.Rows.Add();
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["ZAIKO_HINMEI_CD"].Value = aftsorttable.Rows[i]["ZAIKO_HINMEI_CD"];
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["ZAIKO_HINMEI_NAME"].Value = aftsorttable.Rows[i]["ZAIKO_HINMEI_NAME"];
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["KUROKOSHI_ZAIKO_RYOU"].Value = this.formatJyuuRyou(dto.preZaikoRyou);
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["KUROKOSHI_ZAIKO_KINGAKU"].Value = string.Format(formatKingaku, dto.preZaikoKingaku);
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["UKEIRE_RYOU"].Value = this.formatJyuuRyou(dto.ukeireRyou);
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["SHUKKA_RYOU"].Value = this.formatJyuuRyou(dto.shukkaRyou);
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["IDOU_CHOUSEI_RYOU"].Value = this.formatJyuuRyou(dto.chouseiIdouRyou);
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["TOUGETU_ZAIKO_RYOU"].Value = this.formatJyuuRyou(dto.nowZaikoRyou);
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["TOUGETU_ZAIKO_KINGAKU"].Value = string.Format(formatKingaku, dto.nowZaikoKingaku);
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["GOUKEI_ZAIKO_RYOU"].Value = this.formatJyuuRyou(dto.totalZaikoRyou);
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["GOUKEI_ZAIKO_KINGAKU"].Value = string.Format(formatKingaku, dto.totalZaikoKingaku);
                        this.form.zaikoKanriHyo2.Rows[cnt].Cells["ZAIKO_TANKA"].Value = dto.zaikoTanka;
                        cnt++;
                    }

                    this.form.zaikoKanriHyo2.IsBrowsePurpose = true;
                }
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
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

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

            // 印刷ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Print);

            // CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            // 検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受渡項目生成
        /// </summary>
        /// <param name="type">画面種別</param>
        /// <returns name="ConditionInfo">範囲条件情報</returns>
        private UIConstans.ConditionInfo CreateParams()
        {
            var info = new UIConstans.ConditionInfo();
            if (this.param.DataSetFlag == false)
            {
                int year = parentForm.sysDate.Year;
                int month = parentForm.sysDate.Month;
                // 初期設定条件を設定
                info.OutPutKBN = 1;							                // 出力区分
                info.DateFrom = new DateTime(year, month, 1);				// 開始日付
                info.DateTo = info.DateFrom.AddMonths(1).AddDays(-1);	    // 終了日付
                info.GyoushaCdFrom = string.Empty;                          // 開始業者CD
                info.GyoushaCdTo = string.Empty;                            // 終了業者CD
                info.GenbaCdFrom = string.Empty;                            // 開始現場CD
                info.GenbaCdTo = string.Empty;                              // 終了現場CD
                info.ZaikoHinmeiCdFrom = string.Empty;                      // 開始在庫品名CD
                info.ZaikoHinmeiCdTo = string.Empty;		                // 終了在庫品名CD
                info.DataSetFlag = false;					                // 値格納フラグ
            }
            else
            {
                // 前回値のまま
                info = this.param;
            }

            return info;
        }

        /// <summary>
        /// 画面表示初期化
        /// </summary>
        private void SetInitDisp()
        {
            // 画面表示初期化
            this.headerForm.dtp_DenpyoDateFrom.Value = null;
            this.headerForm.dtp_DenpyoDateTo.Value = null;
            this.headerForm.txt_YomikomiDataKensu.Text = "0";
            this.parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle("在庫管理表");
            this.parentForm.lb_hint.Text = "";
            this.headerForm.lb_title.Text = "在庫管理表";
            this.form.zaikoKanriHyo1.Rows.Clear();
            this.form.zaikoKanriHyo1.DataSource = null;
            // 自社名を取得
            corpName = this.dba.SelectCorpName();
        }

        #endregion

        public string formatJyuuRyou(decimal val)
        {
            string format = "{0:#,##0.###}";
            string ret = string.Format(format, val);

            return ret;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        //
        public void setHeaderForm(UIHeader hs)
        {
            this.headerForm = hs;
        }

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

        #endregion
    }
}