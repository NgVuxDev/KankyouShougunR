using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.MihimodukeIchiran.DTO
{
    public class DTOClass
	{
        /// <summary>
        /// 検索条件 : 引渡し日From
        /// </summary>
        public string hikiWatashiDateFrom { get; set; }

        /// <summary>
        /// 検索条件 : 引渡し日To
        /// </summary>
        public string hikiWatashiDateTo { get; set; }

        /// <summary>
        /// 検索条件：マニフェスト番号From
        /// </summary>
        public string manifestIdFrom { get; set; }

        /// <summary>
        /// 検索条件：マニフェスト番号To
        /// </summary>
        public string manifestIdTo { get; set; }

        /// <summary>
        /// 検索条件：マスタ設定
        /// </summary>
        public int masterSettingCondition { get; set; }

        /// <summary>
        /// 検索条件：データ作成
        /// </summary>
        public int dataCondition { get; set; }

        /// <summary>
        /// 検索条件：排出事業者
        /// </summary>
        public string hstEdiMemberId { get; set; }

        /// <summary>
        /// 検索条件：排出事業場(事業場名)
        /// </summary>
        public string hstJouName { get; set; }

        /// <summary>
        /// 検索条件：排出事業場(事業場住所)
        /// </summary>
        public string hstJouAddress { get; set; }

        /// <summary>
        /// 検索条件：収集運搬業者
        /// </summary>
        public string upnEdiMemberId { get; set; }

        /// <summary>
        /// 検索条件：処分事業者
        /// </summary>
        public string sbnEdiMemberId { get; set; }

        /// <summary>
        /// 検索条件：処分事業場(事業場名)
        /// </summary>
        public string sbnJouName { get; set; }

        /// <summary>
        /// 検索条件：処分事業場(事業場住所)
        /// </summary>
        public string sbnJouAddress { get; set; }

        /// <summary>
        /// 検索条件：最終処分事業者
        /// </summary>
        public string lastSbnEdiMemberId { get; set; }

        /// <summary>
        /// 検索条件：最終処分事業場(事業場名)
        /// </summary>
        public string lastSbnJouName { get; set; }

        /// <summary>
        /// 検索条件：最終処分事業場(事業場住所)
        /// </summary>
        public string lastSbnJouAddress { get; set; }
    }
}
