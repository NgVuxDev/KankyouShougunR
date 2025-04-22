using System;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.MobileJoukyouInfo
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        #endregion

        #region コンストラクタ

        public UIForm()
            //コンストラクタ
            : base(WINDOW_ID.T_MOBILE_JOUKYOU_SHOUSAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);
        }

        public UIForm(WINDOW_TYPE windowType, string haishaDenpyouNo, string haishaKbn)
            //コンストラクタ
            : base(WINDOW_ID.T_MOBILE_JOUKYOU_SHOUSAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            // (配車)伝票番号設定
            int denpyouNo = 0;
            this.logic.haishaDenpyouNo = SqlInt64.Null;
            if (int.TryParse(haishaDenpyouNo, out denpyouNo))
            {
                this.logic.haishaDenpyouNo = denpyouNo;
            }

            // (配車)配車区分設定
            int kbn = 0;
            this.logic.haishaKbn = SqlInt32.Null;
            if (int.TryParse(haishaKbn, out kbn))
            {
                this.logic.haishaKbn = kbn;
            }

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

                base.OnLoad(e);

                // 画面情報の初期化
                this.logic.WindowInit(base.WindowType);
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