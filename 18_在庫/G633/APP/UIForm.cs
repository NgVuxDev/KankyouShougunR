using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Shougun.Core.Stock.ZaikoHinmeiHuriwake
{
    /// <summary>
    /// 在庫品名振分
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        // 画面ロジック
        private LogicClass logic = null;

        // メッセージロジック
        internal MessageBoxShowLogic messageShowLogic = null;

        // 入出力プロパティ
        public int DenshuKbnCd { get; private set; }
        public decimal NetJyuuryou { get; private set; }
        public DataTable ZaikoTable { get; internal set; }

        // 前回値チェック用変数(Detail用)
        private Dictionary<string, string> beforeValuesForRow = new Dictionary<string, string>();

        // グリッド編集中フラグ(追加削除行補助用)
        private bool editingMultiRowFlag = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm(int denshuKbnCd, decimal netJyuuryou, DataTable zaikoTable) :
            base(WINDOW_ID.T_ZAIKO_HINMEI_HURIWAKE, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();

            this.DenshuKbnCd = denshuKbnCd;
            this.NetJyuuryou = netJyuuryou;
            this.ZaikoTable = zaikoTable;

            //メッセージクラス
            this.messageShowLogic = new MessageBoxShowLogic();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        #endregion コンストラクタ

        #region 画面イベント
        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.logic.WindowInit()) { return; }
            if (!this.logic.EntryInit()) { return; }
            if (!this.logic.DetailInit()) { return; }
        }
        #endregion

        #region ファンクションボタン
        /// <summary>
        /// (F1)比率クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func1_Click(object sender, EventArgs e)
        {
            this.logic.AllHiritsusClear();
        }

        /// <summary>
        /// (F9)反映
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            if (!this.logic.ZaikoHinmeiCdMandatoryCheck() || !this.logic.GoukeiHiritsuCheck())
            {
                return;
            }

            this.messageShowLogic.MessageBoxShow("I014");
            this.logic.ResultReturn();

            // 画面を閉じる
            //var parentForm = this.Parent as BusinessBaseForm;
            //this.Close();
            //parentForm.Close();
            (this.Parent as BusinessBaseForm).Close();
        }

        /// <summary>
        /// (F10)行追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func10_Click(object sender, EventArgs e)
        {
            if (this.editingMultiRowFlag)
            {
                return;
            }

            this.editingMultiRowFlag = true;
            this.logic.AddRow();
            this.editingMultiRowFlag = false;
        }

        /// <summary>
        /// (F11)行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func11_Click(object sender, EventArgs e)
        {
            if (this.editingMultiRowFlag)
            {
                return;
            }

            this.editingMultiRowFlag = true;
            this.logic.RemoveRow();
            this.editingMultiRowFlag = false;
        }

        /// <summary>
        /// (F12)閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            // 画面を閉じる
            //var parentForm = this.Parent as BusinessBaseForm;
            //this.Close();
            //parentForm.Close();
            (this.Parent as BusinessBaseForm).Close();
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// 親画面閉じるイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void parentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // [F9]反映以外を押下する時
            if (this.DialogResult != DialogResult.OK)
            {
                if (this.messageShowLogic.MessageBoxShow("C058") == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcZaikoHinmei_CellValidating(object sender, CellValidatingEventArgs e)
        {
            Row row = (sender as GcMultiRow).CurrentRow;
            if (row == null)
            {
                return;
            }

            switch (e.CellName)
            {
                case "ZAIKO_HINMEI_CD":
                    e.Cancel = !this.logic.ZaikoHinmeiCdDuplicationCheck();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcZaikoHinmei_CellValidated(object sender, CellEventArgs e)
        {
            Row row = (sender as GcMultiRow).CurrentRow;
            if (row == null)
            {
                return;
            }

            // 前回値と変更が無かったら処理中断
            if (!beforeValuesForRow.ContainsKey(e.CellName) ||
                beforeValuesForRow[e.CellName].Equals(Convert.ToString(row.Cells[e.CellName].Value)))
            {
                return;
            }

            switch (e.CellName)
            {
                case "ZAIKO_HINMEI_CD":
                    if (!this.logic.CurrentHiritsuClear(row)) { return; }
                    break;
                case "ZAIKO_HIRITSU":
                    if (!this.logic.ZaikoRyouCalculate(row)) { return; }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcZaikoHinmei_CellEnter(object sender, CellEventArgs e)
        {
            Row row = (sender as GcMultiRow).CurrentRow;
            if (row == null)
            {
                return;
            }

            beforeValuesForRow[e.CellName] = Convert.ToString(row.Cells[e.CellName].Value);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ZaikoHinmeiCdPopupAfter()
        {
            Row row = this.gcZaikoHinmei.CurrentRow;
            if (row == null)
            {
                return;
            }

            // 前回値と変更が無かったら処理中断
            if (beforeValuesForRow.ContainsKey("ZAIKO_HINMEI_CD")
                && beforeValuesForRow["ZAIKO_HINMEI_CD"].Equals(Convert.ToString(row.Cells["ZAIKO_HINMEI_CD"].Value)))
            {
                return;
            }

            if (this.logic.CurrentHiritsuClear(row))
            {
                return;
            }
        }
        #endregion

        #region ユーティリティ
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        internal Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls((this.Parent as BusinessBaseForm).headerForm));
            allControl.AddRange(controlUtil.GetAllControls(this.Parent as BusinessBaseForm));

            return allControl.ToArray();
        }
        #endregion
    }
}
