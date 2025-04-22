using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Master.OboegakiIkkatuHoshu.DTO
{
    public class DTOCls
    {
      

        /// <summary>
        /// 検索条件  :伝票番号　DENPYOU_NUMBER
        /// </summary>
        public String Denpyou_Number { get; set; }
        /// <summary>
        /// 検索条件 : 排出事業者CD	HST_GYOUSHA_CD	
        /// </summary>
        public String Hst_Gyousha_Cd { get; set; }
        /// <summary>
        /// 検索条件 : 排出事業場CD	HST_GENBA_CD
        /// </summary>
        public String Hst_Genba_Cd { get; set; }
        /// <summary>
        /// 検索条件  :運搬業者CD　UNPAN_GYOUSHA_CD
        /// </summary>
        public String Unpan_Gyousha_Cd { get; set; }
        /// <summary>
        /// 検索条件 : 中間処分場所の検索パターン名		SHOBUN_PATTERN_NAME		
        /// </summary>
        public String Shobun_Pattern_Name { get; set; }
        /// <summary>
        /// 検索条件 : 最終処分場所の検索パターン名		SHOBUN_PATTERN_NAME		
        /// </summary>
        public String Last_Shobun_Pattern_Name { get; set; }

        /// <summary>
        /// 検索条件 : 契約開始日　KEIYAKU_BEGIN		
        /// </summary>
        public String Keiyaku_Begin { get; set; }
        /// <summary>
        /// 検索条件 : 契約開始日To　KEIYAKU_BEGIN_TO
        /// </summary>
        public String Keiyaku_Begin_To { get; set; }
        /// <summary>
        /// 検索条件 : 契約終了日KEIYAKU_END		
        /// </summary>
        public String Keiyaku_End { get; set; }
        /// <summary>
        /// 検索条件 : 契約終了日KEIYAKU_END_TO
        /// </summary>
        public String Keiyaku_End_To { get; set; }
        /// <summary>
        /// 検索条件 : 更新種別　Update_Shubetsu		
        /// </summary>
        public String Update_Shubetsu { get; set; }
        /// <summary>
        /// 検索条件 : 契約書種類　KEIYAKUSHO_SHURUI		
        /// </summary>
        public String Keiyakusho_Shurui { get; set; }  

        /// <summary>
        /// 検索条件 : DELETE_FLG
        /// </summary>
        public String Delete_Flg { get; set; }
        /// <summary>
        /// 検索条件 : DEFAULT_KBN
        /// </summary>
        public String Default_Kbn { get; set; }

        /// <summary>
        /// 検索条件 : REJECT_FLG(排他フラグ)
        /// </summary>
        private Boolean Reject_Flg = false;
        /// <summary>
        /// 検索条件 : Rjc_Flg(排他フラグ)
        /// </summary>
        public Boolean Rjc_Flg
        {
            get
            {
                return Reject_Flg;
            }
            set
            {
                Reject_Flg = value;
            }
        }

        public DTOCls()
        {
            this.ItakuMemoIkkatsuEntry = new T_ITAKU_MEMO_IKKATSU_ENTRY();
            this.ItakuMemoIkkatsuDetail = new T_ITAKU_MEMO_IKKATSU_DETAIL();
        }

        /// <summary>
        /// 覚書一括
        /// </summary>
        public T_ITAKU_MEMO_IKKATSU_ENTRY ItakuMemoIkkatsuEntry { get; set; }
        /// <summary>
        /// 覚書一括明細
        /// </summary>
        public T_ITAKU_MEMO_IKKATSU_DETAIL ItakuMemoIkkatsuDetail { get; set; }
        /// <summary>
        /// 覚書一括明細
        /// </summary>
        public T_ITAKU_MEMO_IKKATSU_DETAIL[] ItakuMemoIkkatsuDetailArry { get; set; }

        /// <summary>
        /// 委託契約基本情報
        /// </summary>
        public M_ITAKU_KEIYAKU_KIHON[] ItakuKeiyakuKihon { get; set; }

        /// <summary>
        /// 委託契約別表覚書
        /// </summary>
        public M_ITAKU_KEIYAKU_OBOE[] ItakuKeiyakuOboe { get; set; }

        /// <summary>
        /// 委託契約別表3_処分
        /// </summary>
        public M_ITAKU_KEIYAKU_BETSU3[] ItakuKeiyakuBetsu3 { get; set; }

        /// <summary>
        /// 委託契約別表4_最終処分
        /// </summary>
        public M_ITAKU_KEIYAKU_BETSU4[] ItakuKeiyakuBetsu4 { get; set; }
    }
}
