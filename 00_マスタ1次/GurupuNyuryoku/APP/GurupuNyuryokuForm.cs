// $Id: GurupuNyuryokuForm.cs 99999 2025-03-17 09:22:52Z nguyenanhvu $
using System;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using GurupuNyuryoku.Logic;
using System.Windows.Forms;

using r_framework.Utility;

namespace GurupuNyuryoku.APP
{
    /// <summary>
    /// 単位入力画面
    /// </summary>
    [Implementation]
    public partial class GurupuNyuryokuForm : SuperForm
    {

        /// <summary>
        /// 単位入力画面ロジック
        /// </summary>
        private GurupuNyuryokuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GurupuNyuryokuForm()
            : base(WINDOW_ID.M_GURUPU_NYURYOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new GurupuNyuryokuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            this.Search(null, e);
            Settitle();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
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
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            // 検索条件入力チェック
            if (!this.logic.CheckSearchString())
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E084", CONDITION_ITEM.Text);
                CONDITION_VALUE.Focus();
                return;
            }

            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            int count = this.logic.Search();
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
            }
            else if (count > 0)
            {
                bool catchErr = this.logic.SetIchiran();
                if (catchErr)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // 主キーを非活性にする
            //this.logic.EditableToPrimaryKey();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                bool catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }
                this.logic.Regist(base.RegistErrorFlag);
                if (base.RegistErrorFlag)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                if (this.logic.CheckDelete())
                {
                    bool catchErr = this.logic.CreateEntity(true);
                    if (catchErr)
                    {
                        return;
                    }
                    this.logic.LogicalDelete();
                    if (base.RegistErrorFlag)
                    {
                        return;
                    }
                    this.Search(sender, e);
                }
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            bool catchErr = this.logic.Cancel();
            if (catchErr)
            {
                return;
            }
            Search(sender, e);
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Preview(object sender, EventArgs e)
        {
            this.logic.Preview();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            bool catchErr = this.logic.CancelCondition();
            if (catchErr)
            {
                return;
            }
            //20150414 minhhoang edit #1748
            //do not reload search result when F7 press
            //Search(sender, e);
            this.logic.SetIchiran();
            //20150414 minhhoang end edit #1748
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
            Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
            Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
            Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;

            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 日付コントロールの初期設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEndEdit(object sender, GrapeCity.Win.MultiRow.CellEndEditEventArgs e)
        {
            GcMultiRow gcMultiRow = (GcMultiRow)sender;
            if (e.EditCanceled == false)
            {
                if (gcMultiRow.CurrentCell is GcCustomDataTimePicker)
                {
                    if (gcMultiRow.CurrentCell.Value == null
                        || string.IsNullOrEmpty(gcMultiRow.CurrentCell.Value.ToString()))
                    {
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                        //gcMultiRow.CurrentCell.Value = DateTime.Today;
                        gcMultiRow.CurrentCell.Value = this.logic.parentForm.sysDate.Date;
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                    }
                }
            }

        }

        /// <summary>
        /// 単位CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.GurupuNyuryokuConstans.GURUPU_CD))
            {
                bool isNoErr = this.logic.DuplicationCheck();
                if (!isNoErr)
                {
                    e.Cancel = true;

                    GcMultiRow gc = sender as GcMultiRow;
                    if (gc != null && gc.EditingControl != null)
                    {
                        ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                    }

                    return;
                }
            }

        }

        ///// <summary>
        ///// CD表示変更イベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //internal virtual void CdFormatting(object sender, CellFormattingEventArgs e)
        //{
        //    if (e.CellName.Equals("UNIT_CD"))
        //    {
        //        // 2015/11/24 黄孝坤 12037数値項目の動作統一 start
        //        //this.logic.CdFormatting(e);
        //        // 2015/11/24 黄孝坤 12037数値項目の動作統一 end
        //    }
        //}

        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = false;
            }
            else
            {
                bool catchErr = false;
                // テーブル固定値のデータかどうかを調べる
                if (!this.logic.CheckFixedRow(this.Ichiran.Rows[e.RowIndex], out catchErr) && !catchErr)
                {
                    this.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = true;
                }
            }
        }

        /// <summary>
        /// 表示時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Form_Shown(object sender, EventArgs e)
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
            this.logic.SetFixedIchiran();

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GurupuNyuryokuForm_Shown(object sender, EventArgs e)
        {
        	// 主キーを非活性にする
            //this.logic.EditableToPrimaryKey();
        }

        public void BeforeRegist()
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
            this.logic.SetFixedIchiran();
            this.logic.EditableToPrimaryKey();
            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }

        /// <summary>
        ///  title初期化
        /// </summary>
        private bool Settitle()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.Parent;

                //title
                var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");

                //画面初期表示時、売上で画面作っていたためシステム設定値を読み込む
                bool catchErr = this.logic.TitleInit();
                if (catchErr)
                {
                    return true;
                }
                var titleCnt = titleControl.Text.Length;
                titleControl.Width = titleCnt * 30 + 60;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Settitle", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        ///　Title処理
        /// </summary>
        public virtual void Change(object sender, EventArgs e)
        {
            bool catchErr = this.logic.TitleInit();
            if (catchErr)
            {
                return;
            }
            this.Search(sender, e);
        }
    }
}
