using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.Logic;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO
{
    /// <summary>
    /// 金額集計レコード
    /// </summary>
    [Serializable()]
    internal class ShukeiResultDetail
    {
        /// <summary>
        /// レコード種類
        /// </summary>
        public LogicKingakuData.DisplayDataRowType DisplayRecordType { get; set; }

        /// <summary>
        /// データ有無
        /// </summary>
        public bool hasEnableData { set; get; }

        /// <summary>
        /// 前回残高(外税)
        /// </summary>
        public decimal ZenkaiZandakaSoto { get; set; }
        /// <summary>
        /// 前回残高(内税)
        /// </summary>
        public decimal ZenkaiZandakaUchi { get; set; }
        /// <summary>
        /// 前回残高(非課税)
        /// </summary>
        public decimal ZenkaiZandakaHikazei { get; set; }

        /// <summary>
        /// 今回金額(外税)
        /// </summary>
        public decimal KonkaiKingakuSoto { get; set; }
        /// <summary>
        /// 今回金額(内税)
        /// </summary>
        public decimal KonkaiKingakuUchi { get; set; }
        /// <summary>
        /// 今回金額(非課税)
        /// </summary>
        public decimal KonkaiKingakuHikazei { get; set; }

        /// <summary>
        /// 今回税額(外税)
        /// </summary>
        public decimal KonkaiZeigakuSoto { get; set; }
        /// <summary>
        /// 今回税額(内税)
        /// </summary>
        public decimal KonkaiZeigakuUchi { get; set; }
        /// <summary>
        /// 今回税額(非課税)
        /// </summary>
        public decimal KonkaiZeigakuHikazei { get; set; }

        /// <summary>
        /// 今回取引(外税)
        /// </summary>
        public decimal KonkaiTorihikiSoto { get; set; }
        /// <summary>
        /// 今回取引(内税)
        /// </summary>
        public decimal KonkaiTorihikiUchi { get; set; }
        /// <summary>
        /// 今回取引(非課税)
        /// </summary>
        public decimal KonkaiTorihikiHikazei { get; set; }

        /// <summary>
        /// [帳票用]相殺金額(外税)
        /// </summary>
        public decimal SousaiSoto { get; set; }
        /// <summary>
        /// [帳票用]相殺金額(内税)
        /// </summary>
        public decimal SousaiUchi { get; set; }
        /// <summary>
        /// [帳票用]相殺金額(非課税)
        /// </summary>
        public decimal SousaiHikazei { get; set; }

        /// <summary>
        /// 差引残高(外税)
        /// </summary>
        public decimal SasihikiZandakaSoto { get; set; }
        /// <summary>
        /// 差引残高(内税)
        /// </summary>
        public decimal SasihikiZandakaUchi { get; set; }
        /// <summary>
        /// 差引残高(非課税)
        /// </summary>
        public decimal SasihikiZandakaHikazei { get; set; }

        /// <summary>
        /// 値コピー
        /// </summary>
        /// <remarks>
        /// レコード種類、データ有無は除く
        /// </remarks>
        /// <param name="target"></param>
        public void ToCopy(ShukeiResultDetail target)
        {
            // 前回残高(外税)
            target.ZenkaiZandakaSoto = this.ZenkaiZandakaSoto;
            // 前回残高(内税)
            target.ZenkaiZandakaUchi = this.ZenkaiZandakaUchi;
            // 前回残高(非課税)
            target.ZenkaiZandakaHikazei = this.ZenkaiZandakaHikazei;

            // 今回金額(外税)
            target.KonkaiKingakuSoto = this.KonkaiKingakuSoto;
            // 今回金額(内税)
            target.KonkaiKingakuUchi = this.KonkaiKingakuUchi;
            // 今回金額(非課税)
            target.KonkaiKingakuHikazei = this.KonkaiKingakuHikazei;

            // 今回税額(外税)
            target.KonkaiZeigakuSoto = this.KonkaiZeigakuSoto;
            // 今回税額(内税)
            target.KonkaiZeigakuUchi = this.KonkaiZeigakuUchi;
            // 今回税額(非課税)
            target.KonkaiZeigakuHikazei = this.KonkaiZeigakuHikazei;

            // 今回取引(外税)
            target.KonkaiTorihikiSoto = this.KonkaiTorihikiSoto;
            // 今回取引(内税)
            target.KonkaiTorihikiUchi = this.KonkaiTorihikiUchi;
            // 今回取引(非課税)
            target.KonkaiTorihikiHikazei = this.KonkaiTorihikiHikazei;

            // [帳票用]相殺金額(外税)
            target.SousaiSoto = this.SousaiSoto;
            // [帳票用]相殺金額(内税)
            target.SousaiUchi = this.SousaiUchi;
            // [帳票用]相殺金額(非課税)
            target.SousaiHikazei = this.SousaiHikazei;

            // 差引残高(外税)
            target.SasihikiZandakaSoto = this.SasihikiZandakaSoto;
            // 差引残高(内税)
            target.SasihikiZandakaUchi = this.SasihikiZandakaUchi;
            // 差引残高(非課税)
            target.SasihikiZandakaHikazei = this.SasihikiZandakaHikazei;
        }
    }
    
}
