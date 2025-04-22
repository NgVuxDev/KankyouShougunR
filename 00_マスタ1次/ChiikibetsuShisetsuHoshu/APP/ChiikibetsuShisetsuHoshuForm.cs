// $Id: ChiikibetsuShisetsuHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using ChiikibetsuShisetsuHoshu.Logic;
using GrapeCity.Win.MultiRow;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace ChiikibetsuShisetsuHoshu.APP
{
    /// <summary>
    /// メニュー権限保守画面
    /// </summary>
    [Implementation]
    public partial class ChiikibetsuShisetsuHoshuForm : SuperForm
    {
        /// <summary>
        /// メニュー権限保守画面ロジック
        /// </summary>
        private ChiikibetsuShisetsuHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChiikibetsuShisetsuHoshuForm(String Cd)
            : base(WINDOW_ID.M_CHIIKIBETSU_SHISETSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ChiikibetsuShisetsuHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            this.logic.ChiikiCD = Cd;
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

            if (!string.IsNullOrWhiteSpace(this.CHIIKI_CD.Text))
            {
                this.Search(null, e);
            }

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
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            var messageShowLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.CHIIKI_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E012", "地域");
                this.CHIIKI_CD.Focus();
            }
            else
            {
                this.Ichiran.AllowUserToAddRows = true;//thongh 2015/12/28 #1981
                int count = this.logic.Search();
                if (count == 0)
                {
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
            }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
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

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel();
        }

        //プレビュ機能削除
        ///// <summary>
        ///// プレビュー
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void Preview(object sender, EventArgs e)
        //{
        //    this.logic.Preview();
        //}

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
            this.logic.CancelCondition();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            // 地域コード
            Properties.Settings.Default.CHIIKI_CD_TEXT = this.CHIIKI_CD.Text;

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
        public virtual void Ichiran_CellEndEdit(object sender, GrapeCity.Win.MultiRow.CellEndEditEventArgs e)
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
        /// セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 地域CDが空白の場合、明細入力ができないようにする
            if ((this.CHIIKI_CD.TextLength <= 0) ||
                (this.logic.SearchResultAll == null))
            {
                this.Ichiran.CurrentRow.Selectable = false;
            }
            else
            {
                this.Ichiran.CurrentRow.Selectable = true;
            }

            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = false;
            }
            else
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = true;
            }
        }

        /// <summary>
        /// 報告用業種CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.ChiikibetsuShisetsuHoshuConstans.SHOBUN_HOUHOU_CD))
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

        /// <summary>
        /// 地域CD確定後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CHIIKI_CD_TextChanged(object sender, EventArgs e)
        {
            this.CHIIKI_NAME.Text = string.Empty;
            this.Ichiran.DataSource = null;
            this.logic.SearchResultAll = null;
            this.Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1981
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChiikibetsuShisetsuHoshuForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }
    }
}
