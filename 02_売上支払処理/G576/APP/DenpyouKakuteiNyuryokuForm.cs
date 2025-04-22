// $Id: DenpyouKakuteiNyuryokuForm.cs 21598 2014-05-28 02:32:57Z j-kikuchi $
using System;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Windows.Forms;

namespace Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku
{
    [Implementation]
    public partial class DenpyouKakuteiNyuryokuForm : IchiranSuperForm
    {
        #region フィールド
        private DenpyouKakuteiNyuryoku.LogicClass DenpyouKakuteiNyuryokuLogic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
		public DenpyouKakuteiNyuryokuForm()
            : base(DENSHU_KBN.DENPYOU_KAKUTEI_NYUURYOKU, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
			this.DenpyouKakuteiNyuryokuLogic = new LogicClass(this);

            // グリッドのタブインデックスをセット（デザインで設定できないため）
            Ichiran.TabIndex = 9;
            Ichiran.IsBrowsePurpose = true;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
        }

        #endregion コンストラクタ

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);

            // 画面の初期化
            this.DenpyouKakuteiNyuryokuLogic.WindowInit();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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
    }
}
