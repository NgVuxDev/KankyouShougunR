using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Const;
using System.Drawing;

namespace Shougun.Core.Allocation.HaishaWariateDay
{
    #region - Class -

    /// <summary>(R657)配車割当表（車種／車輌）すクラス・コントロール</summary>
    public class ReportInfoR657 : ReportInfoBase
    {
        #region - Fields -

        private DateTime SagyouDate = DateTime.Now;

        private DataTable ReportTable = new DataTable();

        private DataTable HaishaTable = new DataTable();

        private bool IsShasyuSyaryou = false;
        private int KijunRetsu = 1;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        /// <summary>
        /// 会社情報エンティティ
        /// </summary>
        private M_CORP_INFO mCorpInfo;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR345_R350"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR657(DataTable haishaTable, int kijunRetsu , DateTime sagyouDate, bool isShashuSharyou)
        {
            this.HaishaTable = haishaTable;
            this.KijunRetsu = kijunRetsu;
            this.SagyouDate = sagyouDate;
            this.IsShasyuSyaryou = isShashuSharyou;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            this.ReportTable = CreateTableReport();
            int endKijunRetsu = this.KijunRetsu + ConstClass.DENPYOU_COUNT_REPORT - 1;
            int beginKijunRetsu = ConstClass.DENPYOU_COUNT_REPORT - (endKijunRetsu - this.KijunRetsu);
            if (endKijunRetsu > ConstClass.DENPYOU_COUNT)
            {
                endKijunRetsu = ConstClass.DENPYOU_COUNT;
            }

            int idxReportSum = 0;
            foreach (DataRow haishaRow in this.HaishaTable.Rows)
            {
                int idxReport = 1;
                int idxHaisha = KijunRetsu;
                DataRow reportRow = ReportTable.NewRow();
                reportRow["SHAIN_NAME_RYAKU"] = haishaRow["SHAIN_NAME_RYAKU"];
                reportRow["GYOUSHA_NAME_RYAKU"] = haishaRow["GYOUSHA_NAME_RYAKU"];
                reportRow["SHASHU_NAME_RYAKU"] = haishaRow["SHASHU_NAME_RYAKU"];
                reportRow["SHARYOU_NAME_RYAKU"] = haishaRow["SHARYOU_NAME_RYAKU"];

                for (int i = 0;i < ConstClass.DENPYOU_COUNT_REPORT; i++)
                {
                    idxReport = i + 1;
                    idxHaisha = idxHaisha + 1;
                    if (idxHaisha > ConstClass.DENPYOU_COUNT)
                    {
                        idxHaisha = 1;
                    }
                    var suffixReport = string.Format("{0:D2}", idxReport);
                    var suffixHaisha = string.Format("{0:D2}", idxHaisha);
                    reportRow[string.Format("HAISHA_SHURUI{0}", suffixReport)] = haishaRow[string.Format("HAISHA_SHURUI{0}", suffixHaisha)];
                    reportRow[string.Format("SAGYOUDATE_KUBUN{0}", suffixReport)] = haishaRow[string.Format("SAGYOUDATE_KUBUN{0}", suffixHaisha)];
                    //20150810 hoanghm edit #12063
                    var bkColor = haishaRow[string.Format("GENCHAKU_BACK_COLOR{0}", suffixHaisha)];
                    if (!bkColor.Equals(DBNull.Value))
                    {
                        Color color = Color.FromKnownColor((KnownColor)int.Parse(bkColor.ToString()));

                        reportRow[string.Format("GENCHAKU_JIKAN_COLOR_R{0}", suffixReport)] = color.R.ToString();
                        reportRow[string.Format("GENCHAKU_JIKAN_COLOR_G{0}", suffixReport)] = color.G.ToString();
                        reportRow[string.Format("GENCHAKU_JIKAN_COLOR_B{0}", suffixReport)] = color.B.ToString();
                    }
                    else
                    {
                        reportRow[string.Format("GENCHAKU_JIKAN_COLOR_R{0}", suffixReport)] = "255";
                        reportRow[string.Format("GENCHAKU_JIKAN_COLOR_G{0}", suffixReport)] = "255";
                        reportRow[string.Format("GENCHAKU_JIKAN_COLOR_B{0}", suffixReport)] = "255";
                    }
                    //20150810 hoanghm end edit #12063
                    reportRow[string.Format("GENCHAKU_JIKAN{0}", suffixReport)] = haishaRow[string.Format("GENCHAKU_JIKAN{0}", suffixHaisha)];
                    reportRow[string.Format("HAISHA_SIJISHO_STATUS{0}", suffixReport)] = haishaRow[string.Format("HAISHA_SIJISHO_STATUS{0}", suffixHaisha)];
                    reportRow[string.Format("MAIL_SEND_STATUS{0}", suffixReport)] = null;//haishaRow[string.Format("MAIL_SEND_STATUS{0}", suffixHaisha)];   //ThangNguyen [Delete] 20150729
                    reportRow[string.Format("DENPYOU_CONTENT{0}", suffixReport)] = haishaRow[string.Format("DENPYOU_CONTENT{0}", suffixHaisha)];
                   
                }
                idxReportSum++;
                this.ReportTable.Rows.Add(reportRow);
            }

            this.SetRecord(this.ReportTable);
        }
        
        private DataTable CreateTableReport()
        {
            DataTable haishaTable = new DataTable();
            haishaTable.Columns.Add("SHAIN_NAME_RYAKU");
            haishaTable.Columns.Add("GYOUSHA_NAME_RYAKU");
            haishaTable.Columns.Add("SHASHU_NAME_RYAKU");
            haishaTable.Columns.Add("SHARYOU_NAME_RYAKU");

            for (int i = 0; i < ConstClass.DENPYOU_COUNT_REPORT; ++i)
            {
                var n = i + 1;
                var suffix = string.Format("{0:D2}", n);

                haishaTable.Columns.Add(string.Format("HAISHA_SHURUI{0}", suffix));
                haishaTable.Columns.Add(string.Format("SAGYOUDATE_KUBUN{0}", suffix));
                //20150810 hoanghm edit #12063
                haishaTable.Columns.Add(string.Format("GENCHAKU_JIKAN_COLOR_R{0}", suffix));
                haishaTable.Columns.Add(string.Format("GENCHAKU_JIKAN_COLOR_G{0}", suffix));
                haishaTable.Columns.Add(string.Format("GENCHAKU_JIKAN_COLOR_B{0}", suffix));
                //20150810 hoanghm end edit #12063
                haishaTable.Columns.Add(string.Format("GENCHAKU_JIKAN{0}", suffix));
                haishaTable.Columns.Add(string.Format("HAISHA_SIJISHO_STATUS{0}", suffix));
                haishaTable.Columns.Add(string.Format("MAIL_SEND_STATUS{0}", suffix));
                haishaTable.Columns.Add(string.Format("DENPYOU_CONTENT{0}", suffix));
            }
            return haishaTable;
        }
        
        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            this.SetFieldName("FH_SAGYOU_DATE_VLB", "【日付】" + this.SagyouDate.ToString("yyyy/MM/dd"));
            //20150810 hoanghm edit #12065
            //this.SetFieldName("FH_SHUTSU_RYOKU_DATE_VLB", "出力日 " + DateTime.Now.ToString("yyyy/MM/dd"));
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //this.SetFieldName("FH_SHUTSU_RYOKU_DATE_VLB", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " 発行");
            this.SetFieldName("FH_SHUTSU_RYOKU_DATE_VLB", this.getDBDateTime().ToString("yyyy/MM/dd HH:mm:ss") + " 発行");
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.SetFieldName("FH_CORP_NAME_RYAKU_VLB1", this.mCorpInfo.CORP_RYAKU_NAME);
            //20150810 hoanghm end edit #12065

            int kijunRetsu = this.KijunRetsu;
            for (int i = 0; i < ConstClass.DENPYOU_COUNT_REPORT; i++)
            {
                if (kijunRetsu >= ConstClass.DENPYOU_COUNT)
                {
                    kijunRetsu = 0;
                }
                var suffix = string.Format("{0:D2}", i + 1);
                this.SetFieldName(string.Format("HDR_HEARDER_{0}_CTL", suffix), kijunRetsu.ToString());

                kijunRetsu += 1;
            }
            if (this.IsShasyuSyaryou)
            {
                this.SetFieldName("FH_TITLE_VLB", "配車割当表（車種／車輌）");
                this.SetFieldName("HDR_HEADER_MAIN_01_CTL", "車種/車輌");
                this.SetFieldName("HDR_HEADER_MAIN_02_CTL", "運転者/運搬業者");
                this.SetFieldVisible("DTL_SHAIN_NAME_RYAKU_02_CTL", false);
                this.SetFieldVisible("DTL_GYOUSHA_NAME_RYAKU_02_CTL", false);
                this.SetFieldVisible("DTL_SHASHU_NAME_RYAKU_02_CTL", false);
                this.SetFieldVisible("DTL_SHARYOU_NAME_RYAKU_02_CTL", false);
            }
            else
            {
                this.SetFieldName("FH_TITLE_VLB", "配車割当表（運転者）");
                this.SetFieldName("HDR_HEADER_MAIN_02_CTL", "車種/車輌");
                this.SetFieldName("HDR_HEADER_MAIN_01_CTL", "運転者/運搬業者");
                this.SetFieldVisible("DTL_SHAIN_NAME_RYAKU_01_CTL", false);
                this.SetFieldVisible("DTL_GYOUSHA_NAME_RYAKU_01_CTL", false);
                this.SetFieldVisible("DTL_SHASHU_NAME_RYAKU_01_CTL", false);
                this.SetFieldVisible("DTL_SHARYOU_NAME_RYAKU_01_CTL", false);
            }
        }


        #endregion - Methods -
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }

    #endregion - Class -
}
