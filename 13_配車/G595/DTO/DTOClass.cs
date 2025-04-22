using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.ContenaRirekiIchiranHyou
{
    /// <summary>
    /// コンテナ履歴一覧用DTO
    /// </summary>
    public class DTOClass
    {
        /// <summary>業者／現場設定</summary>
        public string gyoushaGenbaSetting { get; set; }

        /// <summary>出力内容</summary>
        public string outputSetting { get; set; }

        /// <summary>拠点CD</summary>
        public string kyotenCd { get; set; }

        /// <summary>操作区分</summary>
        public string sousaKbn { get; set; }

        /// <summary>日付範囲From</summary>
        public string dateFrom { get; set; }

        /// <summary>日付範囲To</summary>
        public string dateTo { get; set; }

        /// <summary>コンテナ種類From</summary>
        public string contenaShuruiFrom { get; set; }

        /// <summary>コンテナ種類To</summary>
        public string contenaShuruiTo { get; set; }

        /// <summary>業者From</summary>
        public string gyoushaFrom { get; set; }

        /// <summary>業者To</summary>
        public string gyoushaTo { get; set; }

        /// <summary>現場From</summary>
        public string genbaFrom { get; set; }

        /// <summary>現場To</summary>
        public string genbaTo { get; set; }

        /// <summary>コンテナFrom</summary>
        public string contenaFrom { get; set; }

        /// <summary>コンテナTo</summary>
        public string contenaTo { get; set; }

        /// <summary>
        /// DTOの中身を出力
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder returnStr = new StringBuilder();

            returnStr.Append("検索条件{ ");
            returnStr.AppendFormat("gyoushaGenbaSetting：{0}, ", this.gyoushaGenbaSetting);
            returnStr.AppendFormat("outputSetting：{0}, ", this.outputSetting);
            returnStr.AppendFormat("kyotenCd：{0}, ", this.kyotenCd);
            returnStr.AppendFormat("sousaKbn：{0}, ", this.sousaKbn);
            returnStr.AppendFormat("dateFrom：{0}, ", this.dateFrom);
            returnStr.AppendFormat("dateTo：{0}, ", this.dateTo);
            returnStr.AppendFormat("contenaShuruiFrom：{0}, ", this.contenaShuruiFrom);
            returnStr.AppendFormat("contenaShuruiTo：{0}, ", this.contenaShuruiTo);
            returnStr.AppendFormat("gyoushaFrom：{0}, ", this.gyoushaFrom);
            returnStr.AppendFormat("gyoushaTo：{0}, ", this.gyoushaTo);
            returnStr.AppendFormat("genbaFrom：{0}, ", this.genbaFrom);
            returnStr.AppendFormat("genbaTo：{0}, ", this.genbaTo);
            returnStr.AppendFormat("contenaFrom：{0}, ", this.contenaFrom);
            returnStr.AppendFormat("contenaTo：{0}, ", this.contenaTo);
            returnStr.Append("}");

            return returnStr.ToString();
        }
    }
}
