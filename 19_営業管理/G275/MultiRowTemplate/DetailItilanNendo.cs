using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou
{
    public sealed partial class DetailItilanNendo : Template
    {
        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }

        public DetailItilanNendo()
        {
            InitializeComponent();
        }
    }
}
