using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.GyoushaKakunin.Logic;
using r_framework.Logic;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.APP
{
    /// <summary>
    /// 申請内容確認（業者）画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 申請内容確認（業者）画面ロジック
        /// </summary>
        private LogicCls logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="gyoushaCd">選択されたデータの入金先CD</param>
        public UIForm(WINDOW_TYPE windowType, string gyoushaCd, int gyosha_kbn)
            : base(WINDOW_ID.M_GYOUSHA_KAKUNIN, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, gyoushaCd, gyosha_kbn);

                InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

                this.logic.GyoushaCd = gyoushaCd;

                this.logic.GyoshaKbn = gyosha_kbn;

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                //※※※　何故か、現場分類タブが一度選択されないと修正時に敬称がうまくセットされないので
                //※※※　強制的に一度全タブを選択して戻すようにすることで一旦解決
                TabPage now = this.JOHOU.SelectedTab;
                foreach (TabPage page in this.JOHOU.TabPages)
                {
                    this.JOHOU.SelectedTab = page;
                }
                this.JOHOU.SelectedTab = now;
                //※※※　強引な対応ここまで

                if (this.logic.WindowInit(base.WindowType)) { return; }

                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.Parent;

                this.Close();
                if (parentForm != null)
                {
                    parentForm.Close();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}