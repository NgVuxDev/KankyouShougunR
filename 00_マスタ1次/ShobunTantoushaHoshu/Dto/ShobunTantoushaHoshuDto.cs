using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace ShobunTantoushaHoshu.Dto
{
    public class ShobunTantoushaHoshuDto
    {
        public M_SHOBUN_TANTOUSHA ShobunTantoushaSearchString { get; set; }
        public M_SHAIN ShainSearchString { get; set; }

        /// <summary>
        /// 処分担当者Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesShobunTantoushaExistCheck()
        {
            // TODO : 一例です。機能に合わせて各自で調整してください。
            //      : 記述内容はプロパティ(ここでいうshainSearchString)の中から、
            //      : 検索条件に含まれる項目に値が入っているかのチェックを行うメソッドです。
            //      : サンプルではサブEntityの検索条件となるプロパティに値が入っている場合、trueを返却しています。

            // 社員コード
            if (!string.IsNullOrEmpty(this.ShobunTantoushaSearchString.SHAIN_CD))
            {
                return true;
            }

            // 備考
            if (!string.IsNullOrEmpty(this.ShobunTantoushaSearchString.SHOBUN_TANTOUSHA_BIKOU))
            {
                return true;
            }

            return false;
        }
    }
}
