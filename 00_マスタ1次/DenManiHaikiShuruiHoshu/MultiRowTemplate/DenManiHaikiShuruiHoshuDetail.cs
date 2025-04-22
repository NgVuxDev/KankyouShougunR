// $Id: DenManiHaikiShuruiHoshuDetail.cs 50187 2015-05-20 08:27:22Z takeda $
using GrapeCity.Win.MultiRow;
using System;
using System.Data;

namespace DenManiHaikiShuruiHoshu.MultiRowTemplate
{
    public sealed partial class DenManiHaikiShuruiHoshuDetail : Template
    {
        public DenManiHaikiShuruiHoshuDetail()
        {
            InitializeComponent();

            DataRow row;
            row = this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].NewRow();
            row["HAIKI_KBN_CD"] = DBNull.Value;
            row["HAIKI_KBN_NAME"] = string.Empty;
            this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].NewRow();
            row["HAIKI_KBN_CD"] = 1;
            row["HAIKI_KBN_NAME"] = "普通の産廃";
            this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].NewRow();
            row["HAIKI_KBN_CD"] = 2;
            row["HAIKI_KBN_NAME"] = "不可分一体";
            this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].NewRow();
            row["HAIKI_KBN_CD"] = 3;
            row["HAIKI_KBN_NAME"] = "特別管理型";
            this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].NewRow();
            row["HAIKI_KBN_CD"] = 4;
            row["HAIKI_KBN_NAME"] = "特定産業廃棄物";
            this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].NewRow();
            row["HAIKI_KBN_CD"] = 5;
            row["HAIKI_KBN_NAME"] = "特定産業廃棄物（特別管理型）";
            this.ComboDataSet.Tables["HAIKI_KBN_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_BUNRUI_TABLE"].NewRow();
            row["HAIKI_BUNRUI_CD"] = DBNull.Value;
            row["HAIKI_BUNRUI_NAME"] = string.Empty;
            this.ComboDataSet.Tables["HAIKI_BUNRUI_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_BUNRUI_TABLE"].NewRow();
            row["HAIKI_BUNRUI_CD"] = 1;
            row["HAIKI_BUNRUI_NAME"] = "大分類";
            this.ComboDataSet.Tables["HAIKI_BUNRUI_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_BUNRUI_TABLE"].NewRow();
            row["HAIKI_BUNRUI_CD"] = 2;
            row["HAIKI_BUNRUI_NAME"] = "中分類";
            this.ComboDataSet.Tables["HAIKI_BUNRUI_TABLE"].Rows.Add(row);
            row = this.ComboDataSet.Tables["HAIKI_BUNRUI_TABLE"].NewRow();
            row["HAIKI_BUNRUI_CD"] = 3;
            row["HAIKI_BUNRUI_NAME"] = "小分類";
            this.ComboDataSet.Tables["HAIKI_BUNRUI_TABLE"].Rows.Add(row);
        }
    }
}