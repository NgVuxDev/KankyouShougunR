using System.Collections.Generic;
using r_framework.Const;
using Shougun.Core.Common.HanyoCSVShutsuryoku.APP.Panel;
using Shougun.Core.Common.HanyoCSVShutsuryoku.DTO;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Logic.Panel
{
    internal interface IPatternPanelLogic
    {
        List<DENSHU_KBN> DenshuKbnsCreate();

        void EventInit();

        void PanelSet();

        void PanelEntityCreate(PatternDto pattern);

        int PanelSearch();

        int PanelNewRegist(bool errorFlag, PatternDto pattern);

        void PreRegistCheck();

        void PostRegistCheck();
    }

    internal class DummyPatternPanelLogic : IPatternPanelLogic
    {
        public List<DENSHU_KBN> DenshuKbnsCreate()
        {
            return null;
        }

        public void EventInit()
        {
        }

        public void PanelSet()
        {
        }

        public void PanelEntityCreate(PatternDto pattern)
        {
        }

        public int PanelSearch()
        {
            return 0;
        }

        public int PanelNewRegist(bool errorFlag, PatternDto pattern)
        {
            return 0;
        }

        public void PreRegistCheck()
        {
        }

        public void PostRegistCheck()
        {
        }
    }

    internal class PatternPanelLogicFactory
    {
        internal static IPatternPanelLogic Create(IPatternPanel panel)
        {
            if (panel is HanbaikanriPatternPanel)
            {
                return new HanbaikanriPatternPanelLogic(panel as HanbaikanriPatternPanel);
            }
            else if (panel is NyuushukkinPatternPanel)
            {
                return new NyuushukkinPatternPanelLogic(panel as NyuushukkinPatternPanel);
            }
            else
            {
                // Nullをならないようにダミーを戻る
                return new DummyPatternPanelLogic();
            }
        }
    }
}