using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.ExternalConnection.NaviTimeMasterRenkei
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>連携マスタ 1.社員</summary>
        public static readonly int RENKEI_MASTER_SHAIN = 1;
        /// <summary>連携マスタ 2.現場</summary>
        public static readonly int RENKEI_MASTER_GENBA = 2;
        /// <summary>連携マスタ 3.出発現場、荷降現場</summary>
        public static readonly int RENKEI_MASTER_SHUPPATSU_GENBA = 3;
        /// <summary>連携マスタ 4.車種</summary>
        public static readonly int RENKEI_MASTER_SHASHU = 4;

        /// <summary>連携候補 1.未連携</summary>
        public static readonly int RENKEI_KOUHO_MIRENKEI = 1;
        /// <summary>連携候補 2.連携済み</summary>
        public static readonly int RENKEI_KOUHO_RENKEIZUMI = 2;
        /// <summary>連携候補 3.再連携候補</summary>
        public static readonly int RENKEI_KOUHO_SAIRENKEI = 3;

        /// <summary>検索条件 1.連携対象</summary>
        public static readonly int SEARCH_RENKEI_TAISHO = 1;
        /// <summary>検索条件 2.連携除外</summary>
        public static readonly int SEARCH_RENKEI_JOGAI = 2;

        /// <summary>一覧GRID：X＝0</summary>
        public static readonly int ICHIRAN_LOCATION_X = 0;
        /// <summary>一覧GRID：Y＝46</summary>
        public static readonly int ICHIRAN_LOCATION_Y = 46;

        /// <summary>ステータス</summary>
        public static readonly int STATUS_MIRENKEI = 0;
        public static readonly int STATUS_RENKEIKAKUNINNYOU = 1;
        public static readonly int STATUS_RENKEIKANRYOU = 2;
        public static readonly int STATUS_SAIRENKEIKOUHO = 3;
        public static readonly int STATUS_SASHIMODOSHI = 9;
        public static readonly string STATUS_MIRENKEI_STRING = "未連携";
        public static readonly string STATUS_RENKEIKAKUNINNYOU_STRING = "連携確認要";
        public static readonly string STATUS_RENKEIKANRYOU_STRING = "連携完了";
        public static readonly string STATUS_SAIRENKEIKOUHO_STRING = "再連携候補";
        public static readonly string STATUS_SASHIMODOSHI_STRING = "差し戻し";

    }
}
