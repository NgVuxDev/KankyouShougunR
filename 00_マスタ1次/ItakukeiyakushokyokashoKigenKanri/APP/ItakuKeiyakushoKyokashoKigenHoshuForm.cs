// $Id: ItakuKeiyakushoKyokashoKigenHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using ItakuKeiyakushoKyokashoKigenHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace ItakuKeiyakushoKyokashoKigenHoshu.APP
{
    /// <summary>
    /// メニュー権限保守画面
    /// </summary>
    [Implementation]
    public partial class ItakuKeiyakushoKyokashoKigenHoshuForm : SuperForm
    {
        /// <summary>
        /// メニュー権限保守画面ロジック
        /// </summary>
        private ItakuKeiyakushoKyokashoKigenHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItakuKeiyakushoKyokashoKigenHoshuForm()
            : base(WINDOW_ID.M_ITAKU_KEIYAKU_KIGEN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ItakuKeiyakushoKyokashoKigenHoshuLogic(this);

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
            this.logic.WindowInit();

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
            /// 20141203 Houkakou 「委託契約書許可証期限管理」の日付チェックを追加する　start
            if (this.logic.DateCheck())
                return;
            /// 20141203 Houkakou 「委託契約書許可証期限管理」の日付チェックを追加する　end

            int count = this.logic.Search();
            if (count == 0)
            {
                this.logic.SetIchiran();
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
                return;
            }
            else if (count < 0)
            {
                return;
            }

            this.logic.SetIchiran();
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

            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Jigyoushakubun_Text = this.Jigyoushakubun.Text;
            Properties.Settings.Default.GyoushaCode_Text = this.GyoushaCode.Text;
            Properties.Settings.Default.GenbaCode_Text = this.GenbaCode.Text;
            Properties.Settings.Default.ChiikiCode_Text = this.ChiikiCode.Text;
            Properties.Settings.Default.GyouseikyokaKubunCode_Text = this.GyouseikyokaKubunCode.Text;
            if (this.KigenFrom.Value != null)
            {
                Properties.Settings.Default.KigenFrom_Value = ((DateTime)this.KigenFrom.Value).ToString("yyyy/MM/dd");
            }
            if (this.KigenTo.Value != null)
            {
                Properties.Settings.Default.KigenTo_Value = ((DateTime)this.KigenTo.Value).ToString("yyyy/MM/dd");
            }
            Properties.Settings.Default.KyokaNo_Text = this.KyokaNo.Text;
            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 行政許可区分入力処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GyouseikyokaKubunCode_TextChanged(object sender, EventArgs e)
        {
            switch (this.GyouseikyokaKubunCode.Text)
            {
                case "1":
                    this.GyouseikyokaKubunName.Text = ItakuKeiyakushoKyokashoKigenHoshu.Properties.Resources.GYOUSEI_KYOKA_KBN_1;
                    break;

                case "2":
                    this.GyouseikyokaKubunName.Text = ItakuKeiyakushoKyokashoKigenHoshu.Properties.Resources.GYOUSEI_KYOKA_KBN_2;
                    break;

                default:
                    this.GyouseikyokaKubunName.Text = string.Empty;
                    break;
            }
        }

        // VUNGUYEN 20150525 #1294 START
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KigenFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                GyouseikyokaKubunCode.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                KigenTo.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KigenTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                KigenFrom.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                KyokaNo.Focus();
            }
        }
        // VUNGUYEN 20150525 #1294 END

        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
        /// <summary>
        /// 業者CDのフォーカスIN処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GyoushaCode_Enter(object sender, EventArgs e)
        {
            this.logic.befGyoushaCd = this.GyoushaCode.Text;
        }

        /// <summary>
        /// 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void Gyousha_PopupBeforeExecuteMethod()
        {
            this.logic.befGyoushaCd = this.GyoushaCode.Text;
        }

        /// <summary>
        /// 業者CD更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GyoushaCode_Validated(object sender, EventArgs e)
        {
            this.GyoushaCheck();
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        public void GyoushaCheck()
        {
            this.logic.GyoushaValidated();
        }

        /// <summary>
        /// 現場CD更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaCode_Validated(object sender, EventArgs e)
        {
            this.logic.GenbaValidated();
        }
        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
    }
}