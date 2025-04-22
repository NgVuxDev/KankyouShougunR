using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    /// <summary>
    /// Dtoクラス・コントロール
    /// </summary>
    public class SeikyuShimeShoriDto
    {
        //請求/伝票/明細締処理パラメータ用--------------

        /// <summary>
        /// 伝票種類
        /// </summary>
        public int DENPYO_SHURUI { get; set; }
        
        /// <summary>
        /// 拠点コード
        /// </summary>
        public int KYOTEN_CD { get; set; }

        /// <summary>
        /// 締日
        /// </summary>
        public int SHIMEBI { get; set; }

        /// <summary>
        /// 請求締日FROM
        /// </summary>
        public string SEIKYUSHIMEBI_FROM { get; set; }

        /// <summary>
        /// 請求締日TO
        /// </summary>
        public string SEIKYUSHIMEBI_TO { get; set; }

        /// <summary>
        /// 支払締日FROM
        /// </summary>
        public string SHIHARAISHIMEBI_FROM { get; set; }

        /// <summary>
        /// 支払締日TO
        /// </summary>
        public string SHIHARAISHIMEBI_TO { get; set; }

        /// <summary>
        /// 請求(取引)先コード
        /// </summary>
        public string SEIKYU_CD { get; set; }

        /// <summary>
        /// 支払(取引)先コード
        /// </summary>
        public string SHIHARAI_CD { get; set; }

        /// <summary>
        /// データ種類
        /// </summary>
        public int DATA_SHURUI { get; set; }

        /// <summary>
        /// 売上データ取得回避フラグ
        /// </summary>
        public bool KAIHI_FLG { get; set; }

        /// <summary>
        /// 締実行番号
        /// </summary>
        public Int64 SHIME_JIKKOU_NO { get; set; }

        //伝票/明細締処理パラメータ用--------------
        /// <summary>
        /// 伝票番号
        /// </summary>
        public long DENPYOU_NUMBER { get; set; }

        /// <summary>
        /// 伝票番号リスト
        /// </summary>
        public List<string> DENPYOU_NUMBER_LIST { get; set; }


        //明細締処理パラメータ用--------------
        /// <summary>
        /// 明細番号
        /// </summary>
        public Int16 ROW_NO { get; set; }

        /// <summary>
        /// 明細番号リスト
        /// </summary>
        public List<string> MEISAI_NUMBER_LIST { get; set; }


        //チェック用パラメータ--------------

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


        /// <summary>
        /// 処理区分
        /// </summary>
        public string SHORI_KBN { get; set; }

        /// <summary>
        /// チェック区分
        /// </summary>
        public string CHECK_KBN { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        public Int64 SYSTEM_ID { get; set; }

        /// <summary>
        /// 枝番
        /// </summary>
        public int SEQ { get; set; }

        /// <summary>
        /// 明細システムID
        /// </summary>
        public Int64 DETAIL_SYSTEM_ID { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        public int GYO_NUMBER { get; set; }

        /// <summary>
        /// 請求(取引)先名称
        /// </summary>
        public string SEIKYU_NAME { get; set; }

        /// <summary>
        /// 支払(取引)先名称
        /// </summary>
        public string SHIHARAI_NAME { get; set; }

        /// <summary>
        /// エラー内容
        /// </summary>
        public string ERROR_NAIYOU { get; set; }

        /// <summary>
        /// 伝票日付
        /// </summary>
        public DateTime DENPYOU_DATE { get; set; }

        /// <summary>
        /// 売上日付を取得・設定します（売上日付がnullの場合があるため、null許容）
        /// </summary>
        public DateTime? URIAGE_DATE { get; set; }

        /// <summary>
        /// 支払日付を取得・設定します（支払日付がnullの場合があるため、null許容）
        /// </summary>
        public DateTime? SHIHARAI_DATE { get; set; }

        /// <summary>
        /// 伝票番号
        /// </summary>
        public long DENPYOU_BANGOU { get; set; }

        /// <summary>
        /// 伝票種類CD
        /// </summary>
        public int DENPYO_SHURUI_CD { get; set; }

        /// <summary>
        /// 伝票種類
        /// [1:受入/2:出荷/3:売上支払い/10:入金/20:出金]
        /// </summary>
        public int DENPYO_TYPE { get; set; }

        /// <summary>
        /// 合計金額
        /// </summary>
        public decimal GOUKEIGAKU { get; set; }

        /// <summary>
        /// 抽出条件
        /// </summary>
        public Boolean SHIMEI_CHECK { get; set; }

        /// <summary>
        /// 代納フラグ
        /// </summary>
        public bool DAINOU_FLG { get; set; }

        /// <summary>
        /// 再締フラグ
        /// </summary>
        public bool SAISHIME_FLG { get; set; }

        /// <summary>
        /// 再締請求番号リスト
        /// </summary>
        public List<Int64> SAISHIME_NUMBER_LIST { get; set; }

        /// <summary>
        /// 入金予定日
        /// </summary>
        public SqlDateTime NYUUKIN_YOTEI_BI { get; set; }//160013
        /// <summary>
        /// 出金予定日
        /// </summary>
        public SqlDateTime SHUKKIN_YOTEI_BI { get; set; }//160017

        #region 適格請求書
        /// <summary>
        /// 登録番号
        /// </summary>
        public string TOUROKU_NO { get; set; }

        /// <summary>
        /// 請求書フラグ[1.旧請求書 2.適格請求書 3.合算請求書]
        /// </summary>
        public int INVOICE_KBN { get; set; }
        #endregion 適格請求書

        /// <summary>
        /// 
        /// </summary>
        public SeikyuShimeShoriDto()
        {
            this.SAISHIME_FLG = false;
            this.SAISHIME_NUMBER_LIST = new List<Int64>();
        }
    }
}
