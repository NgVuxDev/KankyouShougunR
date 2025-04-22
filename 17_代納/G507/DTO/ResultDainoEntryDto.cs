using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO
{
    /// <summary>
    /// 代納番号の基本情報クラス
    /// </summary>
    [Serializable()]
    internal class ResultDainoEntryDto
    {
        /// <summary>
        ///	レコード種別(1:受入請求、2:受入支払、3:出荷請求、4:出荷支払)
        /// </summary>		
        public int DAINOUU_INOUT_TYPE { get; set; }

        /// <summary>
        /// [ラジオ初期値] 税計算区分CD
        /// </summary>
        public int ZEI_KEISAN_KBN_CD { get; set; }
        /// <summary>
        /// [ラジオ初期値] 税区分CD
        /// </summary>
        public int ZEI_KBN_CD { get; set; }

        /// <summary>
        ///	[前回残高用] (請求・支払)額
        /// </summary>		
        public Nullable<decimal> KONKAI_GAKU { get; set; }
        /// <summary>
        ///	[前回残高用] 開始(売掛・買掛)残高
        /// </summary>		
        public decimal KAISHI_ZANDAKA { get; set; }

        /// <summary>
        ///	[DB更新用] システムID
        /// </summary>		
        public SqlInt64 SYSTEM_ID { get; set; }
        /// <summary>
        ///	[DB更新用] 枝番
        /// </summary>		
        public SqlInt32 SEQ { get; set; }
        /// <summary>
        ///	[DB更新用] タイムスタンプ
        /// </summary>		
        public byte[] TIME_STAMP { get; set; }

        /// <summary>
        /// [帳票用] 代納番号
        /// </summary>
        public long DAINOU_NUMBER { get; set; }
        /// <summary>
        /// [帳票用] 代納日付
        /// </summary>
        public SqlDateTime DENPYOU_DATE { get; set; }
        /// <summary>
        /// [帳票用] 取引先CD
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }
        /// <summary>
        /// [帳票用] 取引先名１
        /// </summary>
        public string TORIHIKISAKI_NAME1 { get; set; }
        /// <summary>
        /// [帳票用] 取引先名２
        /// </summary>
        public string TORIHIKISAKI_NAME2 { get; set; }
        /// <summary>
        /// [帳票用] 取引先敬称名１
        /// </summary>
        public string TORIHIKISAKI_KEISHOU1 { get; set; }
        /// <summary>
        /// [帳票用] 取引先敬称名2
        /// </summary>
        public string TORIHIKISAKI_KEISHOU2 { get; set; }
        /// <summary>
        /// [帳票用] 現場名略称
        /// </summary>
        public string GENBA_NAME_RYAKU { get; set; }
        /// <summary>
        /// [帳票用] 拠点名略称
        /// </summary>
        public string KYOTEN_NAME_RYAKU { get; set; }
        /// <summary>
        /// [帳票用] 拠点郵便番号
        /// </summary>
        public string KYOTEN_POST { get; set; }
        /// <summary>
        /// [帳票用] 拠点住所１
        /// </summary>
        public string KYOTEN_ADDRESS1 { get; set; }
        /// <summary>
        /// [帳票用] 拠点住所２
        /// </summary>
        public string KYOTEN_ADDRESS2 { get; set; }
        /// <summary>
        /// [帳票用] 拠点電話番号
        /// </summary>
        public string KYOTEN_TEL { get; set; }
        /// <summary>
        /// [帳票用] 拠点FAX番号
        /// </summary>
        public string KYOTEN_FAX { get; set; }
    }

}
