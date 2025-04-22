// $Id: ItakuKeiyakuBetsu1Hst.cs 1173 2013-09-04 02:10:11Z gai $
using GrapeCity.Win.MultiRow;
using System.Data;

namespace ItakuKeiyakuHoshu.MultiRowTemplate
{
    public sealed partial class ItakuKeiyakuBetsu1Hst : Template
    {
        public ItakuKeiyakuBetsu1Hst()
        {
            InitializeComponent();

            DataRow row;
            row = this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].NewRow();
            row["HAIKI_KBN_CD"] = 0;
            row["HAIKI_KBN_NAME"] = string.Empty;
            this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].NewRow();
            row["HAIKI_KBN_CD"] = 1;
            row["HAIKI_KBN_NAME"] = "普通産廃";
            this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].NewRow();
            row["HAIKI_KBN_CD"] = 3;
            row["HAIKI_KBN_NAME"] = "特管産廃";
            this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].Rows.Add(row);
        }
    }
}