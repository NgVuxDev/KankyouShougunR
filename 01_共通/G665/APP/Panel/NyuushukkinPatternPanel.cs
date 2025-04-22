using System;
using System.Windows.Forms;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.APP.Panel
{
    public partial class NyuushukkinPatternPanel : UserControl, IPatternPanel
    {
        public event EventHandler DenshuKbnCheckedChanged
        {
            add
            {
                this.chkDenshuKbnNyuukin.CheckedChanged += value;
                this.chkDenshuKbnShukkin.CheckedChanged += value;
            }
            remove
            {
                this.chkDenshuKbnNyuukin.CheckedChanged -= value;
                this.chkDenshuKbnShukkin.CheckedChanged -= value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public NyuushukkinPatternPanel()
        {
            this.InitializeComponent();
        }
    }
}