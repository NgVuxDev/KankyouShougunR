using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - SyuukeiKoumokuHani -

    /// <summary>集計項目の有効・無効範囲を表すクラス・コントロール</summary>
    public class SyuukeiKoumokuHani
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="SyuukeiKoumokuHani"/> class.</summary>
        /// <param name="codeStart">開始コードを表す文字列</param>
        /// <param name="codeStartName">開始コード名を表す文字列</param>
        /// <param name="codeEnd">終了コードを表す文字列</param>
        /// <param name="codeEndName">終了コード名を表す文字列</param>
        public SyuukeiKoumokuHani(string codeStart = "", string codeStartName = "", string codeEnd = "", string codeEndName = "")
        {
            // 開始コード
            this.CodeStart = codeStart;

            // 開始コード名
            this.CodeStartName = codeStartName;

            // 終了コード
            this.CodeEnd = codeEnd;

            // 終了コード名
            this.CodeEndName = codeEndName;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>開始コードを保持するプロパティ</summary>
        public string CodeStart { get; set; }

        /// <summary>開始コード名を保持するプロパティ</summary>
        public string CodeStartName { get; set; }

        /// <summary>終了コードを保持するプロパティ</summary>
        public string CodeEnd { get; set; }

        /// <summary>終了コード名を保持するプロパティ</summary>
        public string CodeEndName { get; set; }

        #endregion - Properties -
    }

    #endregion - SyuukeiKoumokuHaniEnable -
}
