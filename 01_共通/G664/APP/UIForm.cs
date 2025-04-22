// $Id: UIForm.cs 156434 2020-01-08 05:09:26Z ichijo@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.CustomControl;

namespace Shougun.Core.Common.NioroshiNoSettei
{
    /// <summary>
    /// 荷降No設定画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 荷降No設定画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        public Dictionary<string, List<DetailDto>> RetDetailList { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">処理モード</param>
        public UIForm(WINDOW_TYPE windowType, List<NioroshiDto> nioroshiList, List<DetailDto> detailList)
            : base(WINDOW_ID.C_NIOROSHI_NO_SETTEI, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, nioroshiList, detailList);

                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
                this.logic.nioroshiList = nioroshiList;
                this.logic.detailList = detailList;

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
        #endregion

        #region 画面Load処理
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

                this.logic.WindowInit();

                // デザイナの設定が初期化されてしまうので、ここで設定
                this.NioroshiIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                this.DetailIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {            

            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }
        #endregion

        #region Functionボタン 押下処理
        /// <summary>
        /// F1一括設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void IkkatsuSet(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.RegistErrorFlag)
                {
                    if (this.HINMEI_CD_HEAD.IsInputErrorOccured)
                    {
                        this.HINMEI_CD_HEAD.Focus();
                    }
                    return;
                }
                this.logic.IkkatsuSet();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKirikaeFrom", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 登録時の入力チェックを行う
                if (!this.logic.RegistCheck())
                {
                    return;
                }

                // 登録用データの作成
                this.logic.Regist(base.RegistErrorFlag);

                // 新規権限が無い場合は画面を閉じる
                this.FormClose(sender, e);
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
        /// F12 Formクローズ処理
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
                parentForm.Close();
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
        #endregion

        /// <summary>
        /// 品名CD（FocusOutCheckMethodと併用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void HINMEI_CDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.HINMEI_CD_HEAD.Text))
                {
                    // 品名名の初期化は行う
                    this.HINMEI_NAME_HEAD.Text = string.Empty;
                    this.logic.isInputError = false;
                    this.HINMEI_CD_HEAD.IsInputErrorOccured = false;
                    return;
                }

                if (!this.logic.HINMEI_CDValidated())
                {
                    this.logic.isInputError = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.DetailIchiran != null)
            {
                this.DetailIchiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }

            if (this.NioroshiIchiran != null)
            {
                this.NioroshiIchiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            }

            base.OnSizeChanged(e);
        }

        private void DetailIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.logic.DetailIchiran_CellValidating(e);
        }

        private void NIOROSHI_NUMBER_HEAD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.NIOROSHI_NUMBER_HEAD_Validating())
            {
                e.Cancel = true;
            }
        }
    }
}
