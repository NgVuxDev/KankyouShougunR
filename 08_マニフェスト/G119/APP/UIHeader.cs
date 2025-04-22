using System;
using r_framework.APP.Base;

namespace Shougun.Core.PaperManifest.SampaiManifestoChokkou
{
    /// <summary>
    /// 産廃マニフェスト入力ヘッダー部
    /// </summary>
    public partial class SampaiManifestoChokkouHeader : HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        // 20140606 ria No.730 規定値機能の追加について start
        public SampaiManifestoChokkou form1;
        // 20140606 ria No.730 規定値機能の追加について end

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SampaiManifestoChokkouHeader()
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

        // 20140606 ria No.730 規定値機能の追加について start
        public string KyotenCd;

        private void ctxt_KyotenCd_Validated(object sender, EventArgs e)
        {
            // 20140612 syunrei  EV004722_拠点について start
            //if (this.ctxt_KyotenCd.Text != KyotenCd)
            //{
            //    this.form1.KyotenCd_Validated(this.ctxt_KyotenCd.Text,e);
            //}
            //KyotenCd = this.ctxt_KyotenCd.Text;
            if (!this.form1.isHeadVF)
            {
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
            }
            // 20140612 syunrei  EV004722_拠点について end
        }
        // 20140606 ria No.730 規定値機能の追加について end
    }
}