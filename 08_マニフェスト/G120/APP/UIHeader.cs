using System;
using r_framework.APP.Base;

namespace Shougun.Core.PaperManifest.SampaiManifestoThumiKae
{
    /// <summary>
    /// 産廃マニフェスト入力ヘッダー部
    /// </summary>
    public partial class SampaiManifestoThumiKaeHeader : HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        // 20140606 syunrei No.730 規定値機能の追加について start
        public SampaiManifestoThumiKae form1;

        public string KyotenCd;

        // 20140606 syunrei No.730 規定値機能の追加について end
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SampaiManifestoThumiKaeHeader()
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

            ////base.lb_title.Text = "産廃マニフェスト一次";
        }

        // 20140606 syunrei No.730 規定値機能の追加について start

        private void ctxt_KyotenCd_Validated(object sender, EventArgs e)
        {
            if (!this.form1.isHeadVF)
            {
                // 20140612 syunrei  EV004722_拠点について start

                //if (this.ctxt_KyotenCd.Text != KyotenCd)
                //{
                //    this.form1.KyotenCd_Validated(this.ctxt_KyotenCd.Text, e);
                //}
                //KyotenCd = this.ctxt_KyotenCd.Text;

                if (string.IsNullOrEmpty(this.ctxt_KyotenCd.Text))
                {
                    this.KyotenCd = string.Empty;
                    return;
                }
                else if (this.ctxt_KyotenCd.Text == KyotenCd)
                {
                    return;
                }
                else
                {
                    this.form1.KyotenCd_Validated(this.ctxt_KyotenCd.Text, e);
                    this.KyotenCd = this.ctxt_KyotenCd.Text;
                    return;
                }
                // 20140612 syunrei  EV004722_拠点について end
            }
        }

        // 20140606 syunrei No.730 規定値機能の追加について end
    }
}