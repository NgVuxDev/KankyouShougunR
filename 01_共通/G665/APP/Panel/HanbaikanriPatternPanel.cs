using System;
using System.Windows.Forms;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.APP.Panel
{
    public partial class HanbaikanriPatternPanel : UserControl, IPatternPanel
    {
        /// <summary>
        ///
        /// </summary>
        public event EventHandler DenshuKbnCheckedChanged
        {
            add
            {
                this.chkDenshuKbnUkeire.CheckedChanged += value;
                this.chkDenshuKbnShukka.CheckedChanged += value;
                this.chkDenshuKbnUrSh.CheckedChanged += value;
                this.chkDenshuKbnDainou.CheckedChanged += value;
            }
            remove
            {
                this.chkDenshuKbnUkeire.CheckedChanged -= value;
                this.chkDenshuKbnShukka.CheckedChanged -= value;
                this.chkDenshuKbnUrSh.CheckedChanged -= value;
                this.chkDenshuKbnDainou.CheckedChanged -= value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public HanbaikanriPatternPanel()
        {
            this.InitializeComponent();
        }
    }
}