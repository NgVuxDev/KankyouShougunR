using System.Collections.Generic;

namespace Shougun.Core.Common.IchiranCommon
{
    public class PatternManager
    {
        /// <summary>
        /// 伝種区分と出力項目リストのハッシュマップ
        /// </summary>
        private Dictionary<int, PatternSetting> patternSettings;

        /// <summary>
        /// インスタンス
        /// </summary>
        private static PatternManager instance = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private PatternManager()
        {
            this.patternSettings = new Dictionary<int, PatternSetting>();
        }

        /// <summary>
        /// 初期化。ただ一つのインスタンスを生成する。
        /// </summary>
        private static void initiaize()
        {
            if (instance == null)
            {
                instance = new PatternManager();
            }
        }

        /// <summary>
        /// 指定した伝種区分のパターンデータをコレクションに追加します。
        /// </summary>
        /// <param name="denshuKbn"></param>
        internal static PatternSetting GetPatternSetting(int denshuKbn)
        {
            initiaize();
            if (!instance.patternSettings.ContainsKey(denshuKbn))
            {
                var patternSetting = new PatternSetting(denshuKbn);
                patternSetting.LoadSetting();
                instance.patternSettings.Add(denshuKbn, patternSetting);
            }
            return instance.patternSettings[denshuKbn];
        }
    }
}
