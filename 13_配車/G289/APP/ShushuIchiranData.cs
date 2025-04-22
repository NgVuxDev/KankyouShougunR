// $Id: ShushuIchiranData.cs 25413 2014-07-11 10:45:11Z j-kikuchi $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
{ 
     /// <summary>詳細一覧データを表すクラス・コントロール</summary>
    internal class ShushuIchiranData
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the  class.</summary>
        public ShushuIchiranData()
        {
        }

        #endregion - Constructors -

        #region - 業者リスト -

        /// <summary>業者リストを保持するプロパティ</summary>
        public List<HinmeiUnitColumn> HinmeiUnitInfoList { get; set; }

        /// <summary>業者リストを保持するプロパティ</summary>
        //public List<Gyousha> GyoushaList { get; set; }
        public List<Genba> GenbaList { get; set; }


        #endregion - Properties -

        #region - Inner Class -
        
        /// <summary>業者情報を表すクラス・コントロール</summary>
        internal class Gyousha
        {
            #region - Constructors -

            /// <summary>Initializes a new instance of the <see cref="Gyousha"/> class.</summary>
            public Gyousha()
            {
            }

            #endregion - Constructors -

            #region - Properties -

            /// <summary>回数を保持するプロパティ</summary>
            public string RoundNo { get; set; }

            /// <summary>業者CDを保持するプロパティ</summary>
            public string GyoushaCD { get; set; }

            /// <summary>業者名を保持するプロパティ</summary>
            public string GyoushaName { get; set; }          

            /// <summary>現場リストを保持するプロパティ</summary>
            public List<Genba> GenbaList { get; set; }

            #endregion - Properties -
        }

        /// <summary>現場情報を表すクラス・コントロール</summary>
        internal class Genba
        {
            #region - Constructors -

            /// <summary>Initializes a new instance of the <see cref="Genba"/> class.</summary>
            public Genba()
            {
            }

            #endregion - Constructors -

            #region - Properties -
            /// <summary>回数を保持するプロパティ</summary>
            public string RoundNo { get; set; }

            /// <summary>業者CDを保持するプロパティ</summary>
            public string GyoushaCD { get; set; }

            /// <summary>業者名を保持するプロパティ</summary>
            public string GyoushaName { get; set; }          

            /// <summary>現場CDを保持するプロパティ</summary>
            public string GenbaCD { get; set; }

            /// <summary>現場名を保持するプロパティ</summary>
            public string GenbaName { get; set; }

            /// <summary>業者と現場組み合わせを保持するプロパティ</summary>
            public string GyoushaGenba { get; set; }

            /// <summary>収集備考を保持するプロパティ</summary>
            public string SyuusyuuMemo { get; set; }

            /// <summary>品名リストを保持するプロパティ</summary>
            public List<Hinmei> HinmeiList { get; set; }

            #endregion - Properties -
        }

        /// <summary>品名情報を表すクラス・コントロール</summary>
        internal class Hinmei
        {
            #region - Constructors -

            /// <summary>Initializes a new instance of the <see cref="Hinmei"/> class.</summary>
            public Hinmei()
            {
            }

            #endregion - Constructors -

            #region - Properties -

            /// <summary>サブシステムIDを保持するプロパティ</summary>
            public string　 DetailSystemId { get; set; }

            /// <summary>確定を保持するプロパティ</summary>
            public bool KakuteiFlg { get; set; }
            /// <summary>品名CDを保持するプロパティ</summary>
            public string HinmeiCD { get; set; }
            
            /// <summary>品名を保持するプロパティ</summary>
            public string HinmeiName { get; set; }
            
            /// <summary>数量を保持するプロパティ</summary>
            public string Suuryou { get; set; }

            /// <summary>単位CDを保持するプロパティ</summary>
            public string UnitCD { get; set; }

            /// <summary>単位を保持するプロパティ</summary>
            public string UnitName { get; set; }

            /// <summary>換算後数量を保持するプロパティ</summary>
            public string KansangoSuuryou { get; set; }

            /// <summary>換算後単位CDを保持するプロパティ</summary>
            public string KansangoUnitCD { get; set; }

            /// <summary>換算後単位を保持するプロパティ</summary>
            public string KansangoUnitName { get; set; }

            /// <summary>按分後数量を保持するプロパティ</summary>
            public string AnbungoSuuryou { get; set; }
           
            
            /// <summary>荷降Noを保持するプロパティ</summary>
            public string NioroshiNo { get; set; }

            /// <summary>収集時間を保持するプロパティ</summary>
            public string SyuusyuuTime { get; set; }
            /// <summary>収集時を保持するプロパティ</summary>
            public string SyuusyuuHour { get; set; }

            /// <summary>収集分を保持するプロパティ</summary>
            public string SyuusyuuMin { get; set; }

            /// <summary>品名備考を保持するプロパティ</summary>
            public string HinmeiMemo { get; set; }

            /// <summary>収集備考を保持するプロパティ</summary>
            public string SyuusyuuMemo { get; set; }

            /// <summary>品名と単位組み合わせを保持するプロパティ</summary>
            public string HinmeiUnit { get; set; }

            /// <summary>品名と換算後単位組み合わせを保持するプロパティ</summary>
            public string HinmeiKansanUnit { get; set; }

            /// <summary>数量の取得先</summary>
            /// <remarks>true:換算後数量,false:数量 から取得</remarks>
            public bool IsKansan { get; set; }

            /// <summary>換算値</summary>
            public string Kansanchi { get; set; }

            /// <summary>換算後単位モバイル出力フラグ</summary>
            public bool KansanUnitMobileOutputFlg { get; set; }

            /// <summary>売上支払番号</summary>
            public string UrShNumberNumeric { get; set; }

            #endregion - Properties -
        }

        internal class HinmeiUnitColumn
        {
            #region - Constructors -

            /// <summary>Initializes a new instance of the <see cref="Genba"/> class.</summary>
            public HinmeiUnitColumn()
            {
            }

            #endregion - Constructors -

            #region - Properties -

            /// <summary>品名と単位組み合わせを保持するプロパティ</summary>
            public string HinmeiUnit { get; set; }

            /// <summary>業者と現場組み合わせを保持するプロパティ</summary>
            public string GyoushaGenba { get; set; }

            /// <summary>回数を保持するプロパティ</summary>
            public string RoundNo { get; set; }

            /// <summary>業者CDを保持するプロパティ</summary>
            public string GyoushaCD { get; set; }

            /// <summary>現場CDを保持するプロパティ</summary>
            public string GenbaCD { get; set; }

            /// <summary>品名CDを保持するプロパティ</summary>
            public string HinmeiCD { get; set; }

            /// <summary>品名を保持するプロパティ</summary>
            public string HinmeiName { get; set; }

            /// <summary>単位CDを保持するプロパティ</summary>
            public string UnitCD { get; set; }

            /// <summary>単位を保持するプロパティ</summary>
            public string UnitName { get; set; }

            /// <summary>換算後単位CDを保持するプロパティ</summary>
            public string KansanUnitCD { get; set; }

            /// <summary>換算後単位を保持するプロパティ</summary>
            public string KansanUnitName { get; set; }

            /// <summary>伝票区分CDを保持するプロパティ</summary>
            public string DenpyouKbnCd { get; set; }

            /// <summary>単位コードか</summary>
            /// <remarks>true:単位CD, false:換算後単位CD</remarks>
            public bool IsUnit { get; set; }
            public string RowNumber { get; set; }

            #endregion - Properties -

            /// <summary>
            /// 表示用単位CD
            /// </summary>
            /// <returns></returns>
            public string DispUnitCD()
            {
                if (IsUnit)
                {
                    return UnitCD;
                }
                else
                {
                    return KansanUnitCD;
                }
            }

            /// <summary>
            /// 表示用単位名称
            /// </summary>
            /// <returns></returns>
            public string DispUnitName()
            {
                if (IsUnit)
                {
                    return UnitName;
                }
                else
                {
                    return KansanUnitName;
                }
            }
        }

        #endregion - Inner Class -
    }
}


