using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using Shougun.Core.Master.ContenaHoshu.Const;

namespace Shougun.Core.Master.ContenaHoshu.MultiRowTemplate
{
    public sealed partial class UIDetail : Template
    {
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public UIDetail()
        {
            InitializeComponent();

            //コンテナCD
            ContenaHoshuConstans.CONTENACD_MAXLENGTH = this.txtContenaCd.MaxLength.ToString();

            //コンテナ種類CD
            ContenaHoshuConstans.CONTENASYURUICD_MAXLENGTH = this.txtContenaSyuruiCd.MaxLength.ToString();

        }
    }
}
