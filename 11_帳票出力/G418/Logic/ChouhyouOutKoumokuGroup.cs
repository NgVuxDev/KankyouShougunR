using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Class -

    /// <summary>テーブル毎の出力可能項目を表すクラス・コントロール</summary>
    public class ChouhyouOutKoumokuGroup
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ChouhyouOutKoumokuGroup" /> class.</summary>
        /// <param name="chouhyouOutKoumokuList">出力可能項目リスト</param>
        public ChouhyouOutKoumokuGroup(ChouhyouOutKoumoku[] chouhyouOutKoumokuList)
        {
            this.ChouhyouOutKoumokuList = chouhyouOutKoumokuList;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>出力可能項目グループを保持するプロパティ</summary>
        public ChouhyouOutKoumoku[] ChouhyouOutKoumokuList { get; set; }

        #endregion - Properties -
    }

    #endregion - Class -
}
