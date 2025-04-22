using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Allocation.TeikiJissekiHoukoku
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    internal class ConstCls
    {
        // 行数
        /// <summary>[行数]市区町村出力後</summary>
        public const int LINES_AFTER_SHIKUCHOUSON = 1;
        /// <summary>[行数]実績分類合計出力前</summary>
        public const int LINES_BEFORE_JISSEKI_BUNRUI_GOUKEI = 1;
        /// <summary>[行数]実績分類変更時</summary>
        public const int LINES_CHANGE_JISSEKI_BUNRUI = 1;
        /// <summary>[行数]市区町村変更時</summary>
        public const int LINES_CHANGE_SHIKUCHOUSON = 1;

        // ヘッダ文字列
        /// <summary>[ヘッダ]市区町村CD</summary>
        public const string HEADER_SHIKUCHOUSON_CD = "市区町村CD";
        /// <summary>[ヘッダ]市区町村名</summary>
        public const string HEADER_SHIKUCHOUSON_NAME = "市区町村名";
        /// <summary>[ヘッダ]実績分類CD</summary>
        public const string HEADER_JISSEKI_BUNRUI_CD = "実績分類CD";
        /// <summary>[ヘッダ]実績分類名</summary>
        public const string HEADER_JISSEKI_BUNRUI_NAME = "実績分類";
        /// <summary>[ヘッダ]業者CD</summary>
        public const string HEADER_GYOUSHA_CD = "業者CD";
        /// <summary>[ヘッダ]業者名</summary>
        public const string HEADER_GYOUSHA_NAME = "業者名";
        /// <summary>[ヘッダ]現場CD</summary>
        public const string HEADER_GENBA_CD = "現場CD";
        /// <summary>[ヘッダ]現場名</summary>
        public const string HEADER_GENBA_NAME = "現場名";
        /// <summary>[ヘッダ]荷降業者CD</summary>
        public const string HEADER_NIOROSHI_GYOUSHA_CD = "荷降業者CD";
        /// <summary>[ヘッダ]荷降業者名</summary>
        public const string HEADER_NIOROSHI_GYOUSHA_NAME = "荷降業者名";
        /// <summary>[ヘッダ]荷降現場CD</summary>
        public const string HEADER_NIOROSHI_GENBA_CD = "荷降現場CD";
        /// <summary>[ヘッダ]荷降現場名</summary>
        public const string HEADER_NIOROSHI_GENBA_NAME = "荷降現場名";
        /// <summary>[ヘッダ]単位名</summary>
        public const string HEADER_UNIT_NAME = "単位";
        /// <summary>[ヘッダ]実績分類毎合計</summary>
        public const string HEADER_JISSEKI_BUNRUI_GOUKEI = "合計";

        // 列番号
        /// <summary>[列番号]市区町村CD</summary>
        public const int COL_INDEX_SHIKUCHOUSON_CD = 0;
        /// <summary>[列番号]市区町村名</summary>
        public const int COL_INDEX_SHIKUCHOUSON_NAME = 1;

        /// <summary>[列番号][ヘッダ]実績分類CD</summary>
        public const int COL_INDEX_HEADER_JISSEKI_BUNRUI_CD = -1;
        /// <summary>[列番号]実績分類CD</summary>
        public const int COL_INDEX_JISSEKI_BUNRUI_CD = -1;
        /// <summary>[列番号][ヘッダ]実績分類名</summary>
        public const int COL_INDEX_HEADER_JISSEKI_BUNRUI_NAME = -1;
        /// <summary>[列番号]実績分類名1</summary>
        public const int COL_INDEX_JISSEKI_BUNRUI_NAME_1 = 4;
        /// <summary>[列番号]品名開始1</summary>
        public const int COL_INDEX_HINMEI_START_1 = 5;
        /// <summary>[列番号]実績分類名2</summary>
        public const int COL_INDEX_JISSEKI_BUNRUI_NAME_2 = 8;
        /// <summary>[列番号]品名開始2</summary>
        public const int COL_INDEX_HINMEI_START_2 = 9;
        /// <summary>[列番号]実績分類名3</summary>
        public const int COL_INDEX_JISSEKI_BUNRUI_NAME_3 = 4;
        /// <summary>[列番号]品名開始3</summary>
        public const int COL_INDEX_HINMEI_START_3 = 5;

        /// <summary>[列番号][ヘッダ]業者CD</summary>
        public const int COL_INDEX_HEADER_GYOUSHA_CD = 0;
        /// <summary>[列番号][ヘッダ]業者名</summary>
        public const int COL_INDEX_HEADER_GYOUSHA_NAME = 1;
        /// <summary>[列番号][ヘッダ]現場CD</summary>
        public const int COL_INDEX_HEADER_GENBA_CD = 2;
        /// <summary>[列番号][ヘッダ]業者名</summary>
        public const int COL_INDEX_HEADER_GENBA_NAME = 3;
        /// <summary>[列番号][ヘッダ]荷降業者CD</summary>
        public const int COL_INDEX_HEADER_NIOROSHI_GYOUSHA_CD = 4;
        /// <summary>[列番号][ヘッダ]荷降業者名</summary>
        public const int COL_INDEX_HEADER_NIOROSHI_GYOUSHA_NAME = 5;
        /// <summary>[列番号][ヘッダ]荷降現場CD</summary>
        public const int COL_INDEX_HEADER_NIOROSHI_GENBA_CD = 6;
        /// <summary>[列番号][ヘッダ]荷降業者名</summary>
        public const int COL_INDEX_HEADER_NIOROSHI_GENBA_NAME = 7;
        /// <summary>[列番号][ヘッダ]単位名</summary>
        public const int COL_INDEX_HEADER_UNIT_NAME_1 = 4;
        /// <summary>[列番号][ヘッダ]単位名</summary>
        public const int COL_INDEX_HEADER_UNIT_NAME_2 = 8;
        /// <summary>[列番号][ヘッダ]単位名</summary>
        public const int COL_INDEX_HEADER_UNIT_NAME_3 = 4;
        /// <summary>[列番号]単位開始1</summary>
        public const int COL_INDEX_UNIT_START_1 = 5;
        /// <summary>[列番号]単位開始2</summary>
        public const int COL_INDEX_UNIT_START_2 = 9;
        /// <summary>[列番号]単位開始3</summary>
        public const int COL_INDEX_UNIT_START_3 = 5;

        /// <summary>[列番号]数量開始1</summary>
        public const int COL_INDEX_SUURYOU_START_1 = 5;
        /// <summary>[列番号]数量開始2</summary>
        public const int COL_INDEX_SUURYOU_START_2 = 9;
        /// <summary>[列番号]数量開始3</summary>
        public const int COL_INDEX_SUURYOU_START_3 = 5;

        /// <summary>[列番号][ヘッダ]実績分類毎合計</summary>
        public const int COL_INDEX_HEADER_JISSEKI_BUNRUI_GOUKEI = 0;
        /// <summary>[列番号]実績分類毎合計開始1</summary>
        public const int COL_INDEX_JISSEKI_BUNRUI_GOUKEI_START_1 = 5;
        /// <summary>[列番号]実績分類毎合計開始2</summary>
        public const int COL_INDEX_JISSEKI_BUNRUI_GOUKEI_START_2 = 9;
        /// <summary>[列番号]実績分類毎合計開始3</summary>
        public const int COL_INDEX_JISSEKI_BUNRUI_GOUKEI_START_3 = 5;

        /// <summary>CSVタイトル</summary>
        public const string CSV_TITLE = "実績報告";
    }
}
