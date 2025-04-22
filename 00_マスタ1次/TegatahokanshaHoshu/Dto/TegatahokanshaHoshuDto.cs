using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace TegatahokanshaHoshu.Dto
{
    public class TegatahokanshaHoshuDto
    {
        public M_SHAIN ShainSearchString { get; set; }
        public M_TEGATA_HOKANSHA TegatahokanshaSearchString { get; set; }

        /// <summary>
        /// 社員Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesShainExistCheck()
        {
            // TODO : 一例です。機能に合わせて各自で調整してください。
            //      : 記述内容はプロパティ(ここでいうshainSearchString)の中から、
            //      : 検索条件に含まれる項目に値が入っているかのチェックを行うメソッドです。
            //      : サンプルではサブEntityの検索条件となるプロパティに値が入っている場合、trueを返却しています。

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

            // フリガナ
            if (!string.IsNullOrEmpty(this.ShainSearchString.SHAIN_FURIGANA))
            {
                return true;
            }

            return false;
        }
    }
}
