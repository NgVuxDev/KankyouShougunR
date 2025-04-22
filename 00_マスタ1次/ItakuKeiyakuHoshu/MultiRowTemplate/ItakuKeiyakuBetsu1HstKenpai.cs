// $Id: ItakuKeiyakuBetsu1HstKenpai.cs 13257 2014-01-03 14:45:06Z gai $
using GrapeCity.Win.MultiRow;
using System.Data;

namespace ItakuKeiyakuHoshu.MultiRowTemplate
{
    public sealed partial class ItakuKeiyakuBetsu1HstKenpai : Template
    {
        public ItakuKeiyakuBetsu1HstKenpai()
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