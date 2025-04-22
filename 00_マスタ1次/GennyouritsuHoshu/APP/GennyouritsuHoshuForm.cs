// $Id: GennyouritsuHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using GennyouritsuHoshu.Logic;
using GrapeCity.Win.MultiRow;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace GennyouritsuHoshu.APP
{
    /// <summary>
    /// 減容率保守画面
    /// </summary>
    [Implementation]
    public partial class GennyouritsuHoshuForm : SuperForm
    {
        /// <summary>
        /// 減容率保守画面ロジック
        /// </summary>
        private GennyouritsuHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GennyouritsuHoshuForm()
            : base(WINDOW_ID.M_GENNYOURITSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new GennyouritsuHoshuLogic(this);

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

            if (!string.IsNullOrEmpty(this.HOUKOKUSHO_BUNRUI_CD.Text))
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

            bool catchErr = false;
            if (this.logic.NotSetHoukoushoBunruiCD(out catchErr) && !catchErr)
            {
                this.Ichiran.AllowUserToAddRows = true;//thongh 2015/12/28 #1979
                int count = this.logic.Search();
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                }
                else if (count > 0)
                {
                    this.Ichiran.ReadOnly = false;
                    this.logic.SetIchiran();
                }
                else
                {
                    return;
                }
            }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            if (catchErr)
            {
                return;
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            bool catchErr = false;
            if (!this.logic.NotSetHoukoushoBunruiCD(out catchErr))
            {
                return;
            }

            if (catchErr)
            {
                return;
            }

            if (!base.RegistErrorFlag)
            {
                catchErr = this.logic.CreateEntity(false);
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
            bool catchErr = false;
            if (!this.logic.NotSetHoukoushoBunruiCD(out catchErr))
            {
                return;
            }

            if (catchErr)
            {
                return;
            }

            if (!base.RegistErrorFlag)
            {
                catchErr = this.logic.CreateEntity(true);
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
            bool catchErr = this.logic.Cancel();
            if (catchErr)
            {
                return;
            }
            if (this.Ichiran.DataSource != null)
            {
                this.Ichiran.DataSource = null;
            }

            this.HOUKOKUSHO_BUNRUI_CD.Focus();
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Preview(object sender, EventArgs e)
        {
            bool catchErr = false;
            if (!this.logic.NotSetHoukoushoBunruiCD(out catchErr))
            {
                return;
            }

            if (catchErr)
            {
                return;
            }

            this.logic.Preview();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            bool catchErr = false;
            if (!this.logic.NotSetHoukoushoBunruiCD(out catchErr))
            {
                return;
            }

            if (catchErr)
            {
                return;
            }
            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            //20150416 minhhoang edit #1748
            //現状はキャンセルと同じ処理にしておく
            //this.logic.Cancel();
            //if (this.Ichiran.DataSource != null)
            //{
            //    this.Ichiran.DataSource = null;
            //}

            //this.HOUKOKUSHO_BUNRUI_CD.Focus();
            this.logic.CancelCondition();
            //20150416 minhhoang end edit #1748
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

            // 報告書分類指定の保存
            Properties.Settings.Default.HoukokushoBunruiNameRyaku_Text = this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text;
            Properties.Settings.Default.HoukokushoBunruiCd_Text = this.HOUKOKUSHO_BUNRUI_CD.Text;
            Properties.Settings.Default.HoukokushoBunruiCd_DBFieldsName = this.HOUKOKUSHO_BUNRUI_CD.DBFieldsName;
            Properties.Settings.Default.HoukokushoBunruiCd_ItemDefinedTypes = this.HOUKOKUSHO_BUNRUI_CD.ItemDefinedTypes;

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
        /// 廃棄物名称CD、所分方法CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.GennyouritsuHoshuConstans.HAIKI_NAME_CD))
            {
                // 廃棄物名称が空の場合r_framework.CustomControl.GcCustomTextBoxCell.SetResultText()で
                // 空白が自動でDBNullになってしまうためここで空白に戻す。
                // （値が変更されると更新日・更新PCなどが変更されるのでその対策）
                if (this.Ichiran.Rows[e.RowIndex][Const.GennyouritsuHoshuConstans.HAIKI_NAME_RYAKU].Value == DBNull.Value)
                {
                    this.Ichiran.Rows[e.RowIndex][Const.GennyouritsuHoshuConstans.HAIKI_NAME_RYAKU].Value = string.Empty;
                }

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

            if (e.CellName.Equals(Const.GennyouritsuHoshuConstans.SHOBUN_HOUHOU_CD))
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
        /// 報告書分類のIMEモード制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void HOUKOKUSHO_BUNRUI_CD_ImeModeChanged(object sender, EventArgs e)
        {
            this.HOUKOKUSHO_BUNRUI_CD.ImeMode = System.Windows.Forms.ImeMode.Alpha;
        }

        /// <summary>
        /// 報告書分類名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HOUKOKUSHO_BUNRUI_CD_TextChanged(object sender, EventArgs e)
        {
            Ichiran.DataSource = null;
            Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1979
            this.logic.SearchResult = null;
            this.logic.SearchResultAll = null;
            this.logic.SearchString = null;
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }
        /// <summary>
        /// セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 報告分類CDが空白の場合、明細入力ができないようにする
            if ((this.HOUKOKUSHO_BUNRUI_CD.TextLength <= 0) ||
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
    }
}
