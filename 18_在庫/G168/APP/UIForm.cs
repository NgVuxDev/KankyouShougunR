// $Id: UIForm.cs 50757 2015-05-27 05:37:50Z wuq@oec-h.com $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Entity;
using r_framework.Dto;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Shougun.Core.Stock.ZaikoTyouseiIchiran
{
    [Implementation]
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド

        private LogicClass Logic;

        private UIHeader header_new;

        private Boolean isLoaded;

        /// <summary>
        /// 業者CDの前回値
        /// </summary>
        internal string beforeGyoushaCd;

        /// <summary>
        /// 現場CDの前回値
        /// </summary>
        internal string beforeGenbaCd;

        /// <summary>
        /// 在庫品名CDの前回値
        /// </summary>
        internal string beforeZaikoHinmeiCd;

        /// <summary>
        /// エラーフラグ
        /// </summary>
        internal bool isError;

        /// <summary>
        /// 入力エラーの状態を保持
        /// </summary>
        private bool IsInputErrorOccured;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        public UIForm(UIHeader headerForm)
            : base(DENSHU_KBN.ZAIKO, false)
        {
            this.InitializeComponent();

            this.header_new = headerForm;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);

            Logic.SetHeader(header_new);

            isLoaded = false;
        }

        #region 画面コントロールイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            // 画面情報の初期化
            if (isLoaded == false)
            {
                if (!this.Logic.WindowInit()) { return; }
            }

            this.Table = new DataTable();
            DataColumn col = new DataColumn();
            for (int i = 0; i <= 13; i++)
            {
                switch (i)
                {
                    case 0:
                        col = new DataColumn();
                        col.ColumnName = "調整番号";
                        col.DataType = typeof(string);
                        break;
                    case 1:
                        col = new DataColumn();
                        col.ColumnName = "入力日付";
                        col.DataType = typeof(string);
                        break;
                    case 2:
                        col = new DataColumn();
                        col.ColumnName = "業者CD";
                        col.DataType = typeof(string);
                        break;
                    case 3:
                        col = new DataColumn();
                        col.ColumnName = "業者名";
                        col.DataType = typeof(string);
                        break;
                    case 4:
                        col = new DataColumn();
                        col.ColumnName = "現場CD";
                        col.DataType = typeof(string);
                        break;
                    case 5:
                        col = new DataColumn();
                        col.ColumnName = "現場名";
                        col.DataType = typeof(string);
                        break;
                    case 6:
                        col = new DataColumn();
                        col.ColumnName = "在庫品名CD";
                        col.DataType = typeof(string);
                        break;
                    case 7:
                        col = new DataColumn();
                        col.ColumnName = "在庫品名";
                        col.DataType = typeof(string);
                        break;
                    case 8:
                        col = new DataColumn();
                        col.ColumnName = "調整前在庫量";
                        col.DataType = typeof(string);
                        break;
                    case 9:
                        col = new DataColumn();
                        col.ColumnName = "調整量";
                        col.DataType = typeof(string);
                        break;
                    case 10:
                        col = new DataColumn();
                        col.ColumnName = "調整後在庫量";
                        col.DataType = typeof(string);
                        break;
                    case 11:
                        col = new DataColumn();
                        col.ColumnName = "作成者";
                        col.DataType = typeof(string);
                        break;
                    case 12:
                        col = new DataColumn();
                        col.ColumnName = "作成日時";
                        col.DataType = typeof(string);
                        break;
                    case 13:
                        col = new DataColumn();
                        col.ColumnName = "作成PC";
                        col.DataType = typeof(string);
                        break;
                }
                Table.Columns.Add(col);
            }

            isLoaded = true;

            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                this.logic.CreateDataGridView(this.Table);
                foreach (DataGridViewColumn column in this.customDataGridView1.Columns)
                {
                    switch (column.Name)
                    {
                        case "調整前在庫量":
                        case "調整量":
                        case "調整後在庫量":
                            //column.DefaultCellStyle.Format = this.Logic.sysInfo.SYS_JYURYOU_FORMAT;
                            column.DefaultCellStyle.Format = this.Logic.ZAIKO_RYOU_FORMAT;
                            break;
                    }
                }
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }

            isLoaded = true;
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);
        }

        /// <summary>
        /// 検索結果を表示
        /// </summary>
        /// <param name="e">イベント</param>
        public void SetSearch()
        {
            this.logic.CreateDataGridView(this.Logic.SearchResult);
            foreach (DataGridViewColumn column in this.customDataGridView1.Columns)
            {
                switch (column.Name)
                {
                    case "調整前在庫量":
                    case "調整量":
                    case "調整後在庫量":
                        //column.DefaultCellStyle.Format = this.Logic.sysInfo.SYS_JYURYOU_FORMAT;
                        column.DefaultCellStyle.Format = this.Logic.ZAIKO_RYOU_FORMAT;
                        break;
                }
            }
        }
        #endregion

        #region DataGridViewの列名とソート順を取得する
        /// <summary>
        /// 共通からSQL文でDataGridViewの列名とソート順を取得する
        /// </summary>
        public void SetLogicSelect()
        {
            this.Logic.selectQuery = this.Logic.SELECT;
            this.Logic.orderByQuery = this.logic.OrderByQuery;
            this.Logic.joinQuery = this.logic.JoinQuery;
        }

        #endregion

        private void HIDUKE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.HIDUKE_TO.Value = this.HIDUKE_FROM.Value;
        }

        /// <summary>
        /// 業者CDのCellEnter実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.IsInputErrorOccured && !this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.beforeGyoushaCd = this.GYOUSHA_CD.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDのCellValidated実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.GYOUSHA_CD_Validated();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDのCellEnter実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.IsInputErrorOccured && !this.isError)
            {
                // 現場CDが入力エラーで無い場合、現在の入力値を変更前現場CDとする
                this.beforeGenbaCd = this.GENBA_CD.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDのCellValidated実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.GENBA_CD_Validated();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 在庫品名CDのCellValidated実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void ZAIKO_HINMEI_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.ZAIKO_HINMEI_CD_Validated();

            LogUtility.DebugMethodEnd();
        }

        #region メソッド
        /// <summary>
        /// 業者CDポップアップを開け前に処理します
        /// </summary>
        public void GyoushaPopupBefore()
        {
            LogUtility.DebugMethodStart();
            this.beforeGyoushaCd = this.GYOUSHA_CD.Text;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDポップアップを閉じた後に処理します
        /// </summary>
        public void GyoushaPopupAfter()
        {
            LogUtility.DebugMethodStart();
            this.Logic.GYOUSHA_CD_Validated();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDポップアップを開け前に処理します
        /// </summary>
        public void GenbaPopupBefore()
        {
            LogUtility.DebugMethodStart();
            this.beforeGenbaCd = this.GENBA_CD.Text;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDポップアップを閉じた後に処理します
        /// </summary>
        public void GenbaPopupAfter()
        {
            LogUtility.DebugMethodStart();
            this.Logic.GENBA_CD_Validated();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 在庫品名CDポップアップを開け前に処理します
        /// </summary>
        public void ZaikoHinmeiPopupBefore()
        {
            LogUtility.DebugMethodStart();
            this.beforeZaikoHinmeiCd = this.ZAIKO_HINMEI_CD.Text;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 在庫品名CDポップアップを閉じた後に処理します
        /// </summary>
        public void ZaikoHinmeiPopupAfter()
        {
            LogUtility.DebugMethodStart();
            this.Logic.ZAIKO_HINMEI_CD_Validated();
            LogUtility.DebugMethodEnd();
        }

        #endregion
    }
}
