using System;
using r_framework.APP.Base;

namespace Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku
{
    /// <summary>
    /// 建廃マニフェスト入力ヘッダー部
    /// </summary>
    public partial class KenpaiManifestoNyuryokuHeader : HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        // 20140609 katen No.730 規定値機能の追加について start‏
        /// <summary>
        /// 画面フォーム
        /// </summary>
        public KenpaiManifestoNyuryoku form;

        /// <summary>
        /// 前回入力した拠点の値
        /// </summary>
        public string old_kyoten;

        // 20140609 katen No.730 規定値機能の追加について end‏

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KenpaiManifestoNyuryokuHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ////base.lb_title.Text = "建廃マニフェスト一次";
        }

        // 20140609 katen No.730 規定値機能の追加について start‏
        /// <summary>
        /// 拠点CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ctxt_KyotenCd_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ctxt_KyotenCd.Text))
            {
                this.old_kyoten = string.Empty;
                return;
            }
            else if (this.ctxt_KyotenCd.Text == old_kyoten)
            {
                return;
            }
            else
            {
                this.form.isKyotenFocusOut = true;
                this.form.SetKiteiValue(this.ctxt_KyotenCd.Text);
                this.old_kyoten = this.ctxt_KyotenCd.Text;
                this.form.isKyotenFocusOut = false;
                return;
            }
        }

        // 20140609 katen No.730 規定値機能の追加について end‏
    }
}