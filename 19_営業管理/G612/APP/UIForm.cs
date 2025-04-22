// $Id: UIForm.cs 26166 2014-07-18 13:27:01Z sanbongi $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.TorihikisakiKakunin.Logic;
using r_framework.Logic;

namespace Shougun.Core.BusinessManagement.TorihikisakiKakunin.APP
{
    /// <summary>
    /// 申請内容確認（取引先）画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 引合取引先入力画面ロジック
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
        public UIForm()
            : base(WINDOW_ID.M_TORIHIKISAKI_KAKUNIN, WINDOW_TYPE.NONE)
        {
            try
            {
                LogUtility.DebugMethodStart();

                InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

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
        /// コンストラクタ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="torihikisakiCd"></param>
        public UIForm(WINDOW_TYPE type, string torihikisakiCd, string hikiaiFlg)
            : base(WINDOW_ID.M_TORIHIKISAKI_KAKUNIN, type)
        {
            try
            {
                LogUtility.DebugMethodStart(type, torihikisakiCd, hikiaiFlg);

                InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);
                this.logic.torihikisakiCD = torihikisakiCd;
                this.logic.hikiaiFLG = hikiaiFlg;
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

                //※※※　何故か、タブが一度選択されないと修正時に敬称がうまくセットされないので
                //※※※　強制的に一度全タブを選択して戻すようにすることで一旦解決
                TabPage now = this.tabData.SelectedTab;
                foreach (TabPage page in this.tabData.TabPages)
                {
                    this.tabData.SelectedTab = page;
                }
                this.tabData.SelectedTab = now;
                //※※※　強引な対応ここまで

                if (this.logic.WindowInit(WindowType)) { return; }

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
