using System;
using System.Linq;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Const;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.APP.Panel
{
    internal interface IPatternPanel
    {
        event EventHandler DenshuKbnCheckedChanged;
    }

    internal class PatternPanelFactory
    {
        internal static IPatternPanel Create(int haniKbn)
        {
            try
            {
                var patternPanelType = UIConstants.OUTPUT_HANI_KBNS.FirstOrDefault(x => x.Item1 == haniKbn);
                var patternPanelTypeName = Type.GetType(string.Concat(typeof(IPatternPanel).Namespace, ".", patternPanelType.Item4));
                return Activator.CreateInstance(patternPanelTypeName) as IPatternPanel;
            }
            catch
            {
                return null;
            }
        }
    }
}