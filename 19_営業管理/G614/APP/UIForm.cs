using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.GenbaKakunin.Logic;

namespace Shougun.Core.BusinessManagement.GenbaKakunin.APP
{
    /// <summary>
    /// 申請内容確認（現場）画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        // 201400709 syunrei #947 №19　start
        //メッセージ
        internal MessageBoxShowLogic messBSL = new MessageBoxShowLogic();
        // 201400709 syunrei #947 №19　end

        /// <summary>
        /// 現場保守画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// ポップアップ動作チェック用変数(Detial用)
        /// </summary>
        internal bool FlgDenpyouKbn;

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        /// <summary>
        /// 明細でエラーが起きたかどうか判断するためのフラグ
        /// </summary>
        internal bool bDetailErrorFlag = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ(【新規】モード起動時)
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_GENBA_KAKUNIN, WINDOW_TYPE.NONE)
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
        /// コンストラクタ(【修正】【削除】【複写】モード起動時)
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="gyoushaCd">選択されたデータの業者CD</param>
        /// <param name="genbaCd">選択されたデータの現場CD</param>
        /// <param name="useKariData">仮マスタ使用有無</param>
        public UIForm(WINDOW_TYPE windowType, bool hikiaiGyoushaUseFlg, string gyoushaCd, string genbaCd, bool useKariData)
            : base(WINDOW_ID.M_GENBA_KAKUNIN, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, hikiaiGyoushaUseFlg, gyoushaCd, genbaCd, useKariData);

                InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);
                this.logic.HikiaiGyoushaUseFlg = hikiaiGyoushaUseFlg;
                this.logic.GyoushaCd = gyoushaCd;
                this.logic.GenbaCd = genbaCd;
                this.logic.UseKariData = useKariData;

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
                TabPage now = this.ManiHensousakiKeishou2B1.SelectedTab;
                foreach (TabPage page in this.ManiHensousakiKeishou2B1.TabPages)
                {
                    this.ManiHensousakiKeishou2B1.SelectedTab = page;
                }
                this.ManiHensousakiKeishou2B1.SelectedTab = now;
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
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
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

        /// <summary>
        /// 画面表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.SetNameForTeikiData();
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm_Shown", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
    
}
