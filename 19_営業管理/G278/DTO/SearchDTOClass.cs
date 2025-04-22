using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou
{
    internal class JuchuuYojitsuDto
    {

        /// <summary>
        /// 検索条件  :年度(設定年度
        /// </summary>
        public string NENDO_01 { get; set; }
        public string JINEN_01 { get; set; }
        public string STARTNENDO_01 { get; set; }
        public string ENDNENDO_01 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の年度(-1)
        /// </summary>
        public string NENDO_02 { get; set; }
        public string JINEN_02 { get; set; }
        public string STARTNENDO_02 { get; set; }
        public string ENDNENDO_02 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の年度(-2)
        /// </summary>
        public string NENDO_03 { get; set; }
        public string JINEN_03 { get; set; }
        public string STARTNENDO_03 { get; set; }
        public string ENDNENDO_03 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の年度(-3)
        /// </summary>
        public string NENDO_04 { get; set; }
        public string JINEN_04 { get; set; }
        public string STARTNENDO_04 { get; set; }
        public string ENDNENDO_04 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の年度(-4)
        /// </summary>
        public string NENDO_05 { get; set; }
        public string JINEN_05 { get; set; }
        public string STARTNENDO_05 { get; set; }
        public string ENDNENDO_05 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の年度(-5)
        /// </summary>
        public string NENDO_06 { get; set; }
        public string JINEN_06 { get; set; }
        public string STARTNENDO_06 { get; set; }
        public string ENDNENDO_06 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の年度(-6)
        /// </summary>
        public string NENDO_07 { get; set; }
        public string JINEN_07 { get; set; }
        public string STARTNENDO_07 { get; set; }
        public string ENDNENDO_07 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の年度(-7)
        /// </summary>
        public string NENDO_08 { get; set; }
        public string JINEN_08 { get; set; }
        public string STARTNENDO_08 { get; set; }
        public string ENDNENDO_08 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の年度(-8)
        /// </summary>
        public string NENDO_09 { get; set; }
        public string JINEN_09 { get; set; }
        public string STARTNENDO_09 { get; set; }
        public string ENDNENDO_09 { get; set; }

        /// <summary>
        /// 検索条件  :年度開始
        /// </summary>
        public string STARTNENDO { get; set; }

        /// <summary>
        /// 検索条件  :年度終了
        /// </summary>
        public string ENDNENDO { get; set; }

        /// <summary>
        /// 検索条件  :年度開始月(期首)
        /// </summary>
        public string STARTMONTH { get; set; }

        /// <summary>
        /// 検索条件  :年度終了月
        /// </summary>
        public string ENDMONTH { get; set; }

        /// <summary>
        /// 検索条件  :次年度フラグ
        /// </summary>
        public bool JINEN_FLG_01 { get; set; }
        public bool JINEN_FLG_02 { get; set; }
        public bool JINEN_FLG_03 { get; set; }
        public bool JINEN_FLG_04 { get; set; }
        public bool JINEN_FLG_05 { get; set; }
        public bool JINEN_FLG_06 { get; set; }
        public bool JINEN_FLG_07 { get; set; }
        public bool JINEN_FLG_08 { get; set; }
        public bool JINEN_FLG_09 { get; set; }
        public bool JINEN_FLG_10 { get; set; }
        public bool JINEN_FLG_11 { get; set; }
        public bool JINEN_FLG_12 { get; set; }


        /// <summary>
        /// 検索条件  :対象期間の月次1
        /// </summary>
        //public string[] MONTH = new string[12] ;
        public string MONTH_01 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次2
        /// </summary>
        public string MONTH_02 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次3
        /// </summary>
        public string MONTH_03 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次4
        /// </summary>
        public string MONTH_04 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次5
        /// </summary>
        public string MONTH_05 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次6
        /// </summary>
        public string MONTH_06 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次7
        /// </summary>
        public string MONTH_07 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次8
        /// </summary>
        public string MONTH_08 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次9
        /// </summary>
        public string MONTH_09 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次10
        /// </summary>
        public string MONTH_10 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次11
        /// </summary>
        public string MONTH_11 { get; set; }

        /// <summary>
        /// 検索条件  :対象期間の月次12
        /// </summary>
        public string MONTH_12 { get; set; }

        /// <summary>
        /// 拠部署コード
        /// </summary>
        public string BUSHO_CD { get; set; }
    }
}
