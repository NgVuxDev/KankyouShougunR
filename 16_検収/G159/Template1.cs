using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;

namespace G159_1
{
    public sealed partial class Template1 : Template
    {
        public Template1()
        {
            InitializeComponent();
            SHUKKA_ENTRY_SHUKKA_NUMBER.CellIndex = 0;
            ROWNO.CellIndex = 1;
            SHUKKA_ENTRY_DENPYOU_DATE.CellIndex = 2;
            SHUKKA_ENTRY_KENSYU_DATE.CellIndex = 3;
            SHUKKA_ENTRY_TORIHIKISAKI_CD.CellIndex = 4;
            SHUKKA_ENTRY_TORIHIKISAKI_NAME.CellIndex = 5;
            SHUKKA_ENTRY_GYOUSHA_CD.CellIndex = 6;
            SHUKKA_ENTRY_GYOUSHA_NAME.CellIndex = 7;
            SHUKKA_ENTRY_GENBA_CD.CellIndex = 8;
            SHUKKA_ENTRY_GENBA_NAME.CellIndex = 9;
            SHUKKA_DETAIL_HINMEI_CD.CellIndex = 10;
            SHUKKA_DETAIL_HINMEI_NAME.CellIndex = 11;
            KENSHU_DETAIL_HINMEI_CD.CellIndex = 12;
            KENSHU_DETAIL_HINMEI_NAME.CellIndex = 13;
            SHUKKA_DETAIL_DENPYOU_KBN.CellIndex = 14;
            KENSHU_DETAIL_DENPYOU_KBN.CellIndex = 15;
            SHUKKA_DETAIL_NET_JYUURYOU.CellIndex = 16;
            KENSHU_DETAIL_KENSHU_NET.CellIndex = 17;
            SHUKKA_DETAIL_SUURYOU.CellIndex = 18;
            KENSHU_DETAIL_SUURYOU.CellIndex = 19;
            M_UNIT_SHUKKA_DETAIL_UNIT_NAME_RYAKU.CellIndex = 20;
            M_UNIT_KENSHU_DETAIL_UNIT_NAME_RYAKU.CellIndex = 21;
            KENSHU_DETAIL_BUBIKI.CellIndex = 22;
            KENSHU_DETAIL_TANKA.CellIndex = 23;
            KENSHU_DETAIL_KINGAKU.CellIndex = 24;
        }
    }
}
