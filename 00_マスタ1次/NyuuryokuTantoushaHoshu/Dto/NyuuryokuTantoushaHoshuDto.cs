using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace NyuuryokuTantoushaHoshu.Dto
{
    public class NyuuryokuTantoushaHoshuDto
    {
        public M_SHAIN ShainSearchString { get; set; }
        public M_NYUURYOKU_TANTOUSHA nyuuryokuTantoushaSearchString { get; set; }
        
        /// <summary>
        /// 社員Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesShainExistCheck()
        {
            // 社員CD
            if (!string.IsNullOrEmpty(this.ShainSearchString.SHAIN_CD))
            {
                return true;
            }

            // 社員名
            if (!string.IsNullOrEmpty(this.ShainSearchString.SHAIN_NAME))
            {
                return true;
            }

            // 社員フリガナ
            if (!string.IsNullOrEmpty(this.ShainSearchString.SHAIN_FURIGANA))
            {
                return true;
            }

            return false;
        }
    }
}
