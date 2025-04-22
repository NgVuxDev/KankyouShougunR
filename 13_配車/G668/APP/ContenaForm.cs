using System;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.MobileJoukyouInfo
{
    public partial class ContenaForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private ContenaLogicCls logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// SEQ_NO
        /// </summary>
        internal string seqNO = string.Empty;

        #endregion

        #region コンストラクタ

        public ContenaForm(string SEQ_NO)
            //コンストラクタ
            : base(WINDOW_ID.T_CONTENA_JOUKYOU_SHOUSAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            this.seqNO = SEQ_NO;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ContenaLogicCls(this);

            

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        #endregion

        #region 画面 Loadイベント

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                // 画面情報の初期化
                this.logic.WindowInit(base.WindowType);

                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion
    }
}