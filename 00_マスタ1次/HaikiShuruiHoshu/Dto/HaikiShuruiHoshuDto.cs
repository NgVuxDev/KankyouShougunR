using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace HaikiShuruiHoshu.Dto
{
    public class HaikiShuruiHoshuDto
    {
        public M_HAIKI_SHURUI HaikiShuruiSearchString { get; set; }
        public M_HOUKOKUSHO_BUNRUI HoukokushoBunruiSearchString { get; set; }

        /// <summary>
        /// 報告書分類Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesHoukokushoBunruiExistCheck()
        {
            // TODO : 一例です。機能に合わせて各自で調整してください。
            //      : 記述内容はプロパティ(ここでいうshainSearchString)の中から、
            //      : 検索条件に含まれる項目に値が入っているかのチェックを行うメソッドです。
            //      : サンプルではサブEntityの検索条件となるプロパティに値が入っている場合、trueを返却しています。

            // 報告書分類コード
            if (!string.IsNullOrEmpty(this.HoukokushoBunruiSearchString.HOUKOKUSHO_BUNRUI_CD))
            {
                return true;
            }

            // 報告書分類略称
            if (!string.IsNullOrEmpty(this.HoukokushoBunruiSearchString.HOUKOKUSHO_BUNRUI_NAME_RYAKU))
            {
                return true;
            }

            return false;
        }
    }
}
