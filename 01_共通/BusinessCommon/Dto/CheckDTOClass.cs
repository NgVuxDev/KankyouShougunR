using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    /// <summary>
    /// Dtoクラス・コントロール
    /// </summary>
    public class CheckDto
    {
        //締チェック用DTO
        //複数件対象の場合は、Listにして渡す

        /// <summary>
        /// 取引(請求)先コード
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 拠点コード
        /// </summary>
        public int KYOTEN_CD { get; set; }

        /// <summary>
        /// 表示伝票種類
        /// [1:すべて/2:受入/3:出荷/4:売上支払い/5:入金]
        /// </summary>
        public int DENPYOU_SHURUI { get; set; }

        /// <summary>
        /// 伝票種類
        /// [1:受入/2:出荷/3:売上支払い]
        /// </summary>
        public int DENPYOU_TYPE { get; set; }

        /// <summary>
        /// 期間FROM
        /// </summary>
        public string KIKAN_FROM { get; set; }

        /// <summary>
        /// 期間TO
        /// </summary>
        public string KIKAN_TO { get; set; }

        /// <summary>
        /// 使用画面
        /// [1:締処理画面/2:締チェック画面]
        /// </summary>
        public int SHIYOU_GAMEN { get; set; }
        
        /// <summary>
        /// 締め単位
        /// [1:期間単位/2:伝票単位/3:明細単位]
        /// </summary>
        public int SHIME_TANI { get; set; }

        /// <summary>
        /// 売上・支払い区分
        /// [1:売上/2:支払]
        /// </summary>
        public int URIAGE_SHIHARAI_KBN { get; set; }

        //伝票/明細締処理パラメータ用--------------
        /// <summary>
        /// 伝票番号
        /// </summary>
        public long DENPYOU_NUMBER { get; set; }
        //明細締処理パラメータ用--------------
        /// <summary>
        /// 明細番号
        /// </summary>
        public Int16 ROW_NO { get; set; }

        /// <summary>
        /// クライアントコンピュータ名
        /// </summary>
        public string CLIENT_COMPUTER_NAME { get; set; }

        /// <summary>
        /// クライアントユーザー名
        /// </summary>
        public string CLIENT_USER_NAME { get; set; }

        /// <summary>
        /// 再締フラグ
        /// </summary>
        public bool SAISHIME_FLG { get; set; }

        /// <summary>
        /// 再締請求番号リスト
        /// </summary>
        public List<Int64> SAISHIME_NUMBER_LIST { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CheckDto()
        {
            this.SAISHIME_FLG = false;
            this.SAISHIME_NUMBER_LIST = new List<Int64>();
        }
    }
}
