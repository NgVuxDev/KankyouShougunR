// $Id: ItakuKeiyakuBetsu1Yotei.cs 39265 2015-01-12 07:16:04Z chenzz@oec-h.com $
using System.Data;
using GrapeCity.Win.MultiRow;

namespace ItakuKeiyakuHoshu.MultiRowTemplate
{
    public sealed partial class ItakuKeiyakuBetsu1Yotei : Template
    {
        public ItakuKeiyakuBetsu1Yotei()
        {
            InitializeComponent();

            DataRow row;
            row = this.ComboDataSet.Tables["YOTEI_KIKAN_TABLE"].NewRow();
            row["YOTEI_KIKAN_CD"] = 1;
            row["YOTEI_KIKAN_NAME"] = "年";
            this.ComboDataSet.Tables["YOTEI_KIKAN_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["YOTEI_KIKAN_TABLE"].NewRow();
            row["YOTEI_KIKAN_CD"] = 2;
            row["YOTEI_KIKAN_NAME"] = "月";
            this.ComboDataSet.Tables["YOTEI_KIKAN_TABLE"].Rows.Add(row);
        }
    }
}