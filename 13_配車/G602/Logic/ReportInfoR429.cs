using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;

namespace Shougun.Core.Allocation.Teikijissekihoukoku
{
    #region - Class -

    /// <summary>(R429)定期配車実績表(月報)を表すクラス・コントロール</summary>
    public class ReportInfoR429 : ReportInfoBase
    {
        #region - Fields -

        /// <summary>最大品名数を保持するフィールド</summary>
        private const int ConstMaxHinmeiCount = 10;

        /// <summary>最大品名数を保持するフィールド</summary>
        private const int ConstSampleHinmeiCount = 0;

        /// <summary>最大月数を保持するフィールド</summary>
        private const int ConstMaxDay = 31;

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR429"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR429(WINDOW_ID windowID)
        {
            this.windowID = windowID;
        }

        #endregion - Constructors -

        #region - Properties -

        #endregion - Properties -

        #region - Methods -

        /// <summary>サンプルデータの作成処理を実行する</summary>
        public void CreateSampleData()
        {
            DataTable dataTableTmp;
            DataRow rowTmp;
            string ctrlName = string.Empty;

            bool isPrint = true;
            bool isPrintH = true;

            string tmp;

            for (int year = 1; year <= 1; year++)
            {
                for (int month = 1; month <= 1; month++)
                {
                    for (int gyoushaCD = 1; gyoushaCD <= 5; gyoushaCD++)
                    {
                        for (int genbaCD = 1; genbaCD <= 7; genbaCD++)
                        {
                            tmp = string.Format("{0}_{1}_{2}_{3}", year, month, gyoushaCD, genbaCD);
                            this.DataTablePageList[tmp] = new Dictionary<string, DataTable>();

                            #region - Header -

                            dataTableTmp = new DataTable();
                            dataTableTmp.TableName = "Header";

                            // 会社名
                            dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                            //// 発行日時
                            //dataTableTmp.Columns.Add("PRINT_DATE");
                            // タイトル
                            //dataTableTmp.Columns.Add("TITLE");

                            //// 年 月分
                            //dataTableTmp.Columns.Add("TITLE_DATE_YY");

                            // 業者CD
                            dataTableTmp.Columns.Add("GYOUSHA_CD");
                            // 業者名
                            dataTableTmp.Columns.Add("GYOUSYA_NAME");
                            // 現場CD
                            dataTableTmp.Columns.Add("GENBA_CD");
                            // 現場名
                            dataTableTmp.Columns.Add("GENBA_NAME");

                            if ((genbaCD - 1) % 2 == 0)
                            {
                                for (int i = 0; i < ConstMaxHinmeiCount + ConstSampleHinmeiCount; i++)
                                {
                                    // 品名
                                    ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                                    dataTableTmp.Columns.Add(ctrlName);
                                    // 単位
                                    ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                                    dataTableTmp.Columns.Add(ctrlName);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < ConstMaxHinmeiCount; i++)
                                {
                                    // 品名
                                    ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                                    dataTableTmp.Columns.Add(ctrlName);
                                    // 単位
                                    ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                                    dataTableTmp.Columns.Add(ctrlName);
                                }
                            }

                            if (isPrintH)
                            {
                                rowTmp = dataTableTmp.NewRow();

                                // 会社名
                                rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそ";
                                //// 発行日時
                                //rowTmp["PRINT_DATE"] = "";
                                // タイトル
                                //rowTmp["TITLE"] = "";

                                //// 年
                                //rowTmp["TITLE_DATE_YY"] = "";
                                //// 月分
                                //rowTmp["TITLE_DATE_MM"] = "";

                                // 業者CD
                                rowTmp["GYOUSHA_CD"] = "GyoushaCD";
                                // 業者名
                                rowTmp["GYOUSYA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";
                                // 現場CD
                                rowTmp["GENBA_CD"] = "Genba_CD";
                                // 現場名
                                rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほ";

                                if ((genbaCD - 1) % 2 == 0)
                                {
                                    for (int i = 0; i < ConstMaxHinmeiCount + ConstSampleHinmeiCount; i++)
                                    {
                                        // 品名
                                        ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                                        rowTmp[ctrlName] = string.Format("No.{0}_あいうえお", i + 1);

                                        // 単位
                                        ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                                        rowTmp[ctrlName] = string.Format("No.{0}_あいうえお", i + 1);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < ConstMaxHinmeiCount; i++)
                                    {
                                        // 品名
                                        ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                                        rowTmp[ctrlName] = string.Format("No.{0}_あいうえお", i + 1);

                                        // 単位
                                        ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                                        rowTmp[ctrlName] = string.Format("No.{0}_あいうえお", i + 1);
                                    }
                                }

                                dataTableTmp.Rows.Add(rowTmp);
                            }

                            this.DataTablePageList[tmp]["Header"] = dataTableTmp;

                            #endregion - Header -

                            #region - Detail -

                            dataTableTmp = new DataTable();
                            dataTableTmp.TableName = "Detail";

                            // 日付
                            dataTableTmp.Columns.Add("DATE");

                            if ((genbaCD - 1) % 2 == 0)
                            {
                                // 数量
                                for (int i = 0; i < ConstMaxHinmeiCount + ConstSampleHinmeiCount; i++)
                                {
                                    ctrlName = string.Format("HINMEI_SURYO_{0}", i + 1);
                                    dataTableTmp.Columns.Add(ctrlName);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < ConstMaxHinmeiCount; i++)
                                {
                                    ctrlName = string.Format("HINMEI_SURYO_{0}", i + 1);
                                    dataTableTmp.Columns.Add(ctrlName);
                                }
                            }

                            if (isPrint)
                            {
                                for (int i = 0; i < 31; i++)
                                {
                                    rowTmp = dataTableTmp.NewRow();

                                    // 日付
                                    rowTmp["DATE"] = (i + 1).ToString();

                                    // 数量
                                    if ((genbaCD - 1) % 2 == 0)
                                    {
                                        for (int j = 0; j < ConstMaxHinmeiCount + ConstSampleHinmeiCount; j++)
                                        {
                                            ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                                            rowTmp[ctrlName] = string.Format("SURYO{0}", j + 1);
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < ConstMaxHinmeiCount; j++)
                                        {
                                            ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                                            rowTmp[ctrlName] = string.Format("SURYO{0}", j + 1);
                                        }
                                    }

                                    dataTableTmp.Rows.Add(rowTmp);
                                }
                            }

                            this.DataTablePageList[tmp]["Detail"] = dataTableTmp;

                            #endregion - Detail -

                            #region - Footer -

                            dataTableTmp = new DataTable();
                            dataTableTmp.TableName = "Footer";

                            // 合計
                            if ((genbaCD - 1) % 2 == 0)
                            {
                                for (int i = 0; i < ConstMaxHinmeiCount + ConstSampleHinmeiCount; i++)
                                {
                                    ctrlName = string.Format("GOUGEI_{0}", i + 1);
                                    dataTableTmp.Columns.Add(ctrlName);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < ConstMaxHinmeiCount; i++)
                                {
                                    ctrlName = string.Format("GOUGEI_{0}", i + 1);
                                    dataTableTmp.Columns.Add(ctrlName);
                                }
                            }

                            if (isPrint)
                            {
                                rowTmp = dataTableTmp.NewRow();

                                // 合計
                                if ((genbaCD - 1) % 2 == 0)
                                {
                                    for (int i = 0; i < ConstMaxHinmeiCount + ConstSampleHinmeiCount; i++)
                                    {
                                        ctrlName = string.Format("GOUGEI_{0}", i + 1);
                                        rowTmp[ctrlName] = string.Format("Goukei{0}", i + 1);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < ConstMaxHinmeiCount; i++)
                                    {
                                        ctrlName = string.Format("GOUGEI_{0}", i + 1);
                                        rowTmp[ctrlName] = string.Format("Goukei{0}", i + 1);
                                    }
                                }

                                dataTableTmp.Rows.Add(rowTmp);
                            }

                            this.DataTablePageList[tmp]["Footer"] = dataTableTmp;

                            #endregion - Footer -
                        }
                    }
                }
            }
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            int index;
            int x, y;
            DataRow row;
            string ctrlName = string.Empty;
            DataTable dataTableHeaderTmp = null;
            DataTable dataTableDetailTmp = null;
            DataTable dataTableFooterTmp = null;

            int year;
            int month;
            //int gyoushaCD;
            //int genbaCD;
            string gyoushaCD;
            string genbaCD;

            string[] keysName2;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            this.dataTable = new DataTable();
            this.dataTable.TableName = "Detail";

            // 年 月分
            this.dataTable.Columns.Add("PHN_TITLE_DATE_YY_FLB");
            this.dataTable.Columns.Add("PHN_TITLE_DATE_YY_FLB_FORCE");

            // 業者CD
            this.dataTable.Columns.Add("PHN_GYOUSHA_CD_FLB");
            this.dataTable.Columns.Add("PHN_GYOUSHA_CD_FLB_FORCE");
            // 業者名
            this.dataTable.Columns.Add("PHN_GYOUSYA_NAME_FLB");
            // 現場CD
            this.dataTable.Columns.Add("PHN_GENBA_CD_FLB");
            this.dataTable.Columns.Add("PHN_GENBA_CD_FLB_FORCE");
            // 現場名
            this.dataTable.Columns.Add("PHN_GENBA_NAME_FLB");

            for (int i = 0; i < ConstMaxHinmeiCount; i++)
            {
                // 品名
                this.dataTable.Columns.Add(string.Format("PHN_HINMEI_NANE_{0}_FLB", i + 1));

                // 単位
                this.dataTable.Columns.Add(string.Format("PHN_HINMEI_UNIT_UAME_{0}_FLB", i + 1));
            }

            // 日付
            this.dataTable.Columns.Add("PHN_DATE_FLB");
            // 数量
            for (x = 0; x < ConstMaxHinmeiCount; x++)
            {
                this.dataTable.Columns.Add(string.Format("PHN_SURYO_{0}_FLB", x + 1));
            }

            // 合計
            for (x = 0; x < ConstMaxHinmeiCount; x++)
            {
                this.dataTable.Columns.Add(string.Format("PHN_GOUGEI_{0}_FLB", x + 1));
            }

            bool isDateForceChange = false;
            foreach (string keyName1 in this.DataTablePageList.Keys)
            {
                keysName2 = keyName1.Split('_');
                if (keysName2.Length != 4)
                {   // 仕様外
                    return;
                }

                year = int.Parse(keysName2[0]);
                month = int.Parse(keysName2[1]);
                //gyoushaCD = int.Parse(keysName2[2]);
                //genbaCD = int.Parse(keysName2[3]);
                gyoushaCD = keysName2[2].ToString();
                genbaCD = keysName2[3].ToString();

                if (this.DataTablePageList[keyName1].Keys.Count != 3)
                {   // 仕様外
                    return;
                }

                dataTableHeaderTmp = this.DataTablePageList[keyName1]["Header"];
                dataTableDetailTmp = this.DataTablePageList[keyName1]["Detail"];
                dataTableFooterTmp = this.DataTablePageList[keyName1]["Footer"];

                int roopCount = (int)Math.Ceiling((double)(dataTableDetailTmp.Columns.Count - 1) / ConstMaxHinmeiCount);

                for (int colRoop = 0; colRoop < roopCount; colRoop++)
                {
                    isDateForceChange = !isDateForceChange;

                    for (y = 0; y < ConstMaxDay; y++)
                    {
                        row = this.dataTable.NewRow();

                        row["PHN_TITLE_DATE_YY_FLB_FORCE"] = isDateForceChange.ToString();

                        #region - Header -

                        // 年月分
                        row["PHN_TITLE_DATE_YY_FLB"] = string.Format("{0}年{1}月分", year, month);

                        // 業者CD
                        row["PHN_GYOUSHA_CD_FLB"] = keysName2[2];
                        // 現場CD
                        row["PHN_GENBA_CD_FLB"] = keysName2[3];

                        if (dataTableHeaderTmp.Rows.Count > 0)
                        {
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSYA_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                row["PHN_GYOUSYA_NAME_FLB"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row["PHN_GYOUSYA_NAME_FLB"] = string.Empty;
                            }

                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                row["PHN_GENBA_NAME_FLB"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row["PHN_GENBA_NAME_FLB"] = string.Empty;
                            }
                        }
                        else
                        {
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSYA_NAME");
                            row["PHN_GYOUSYA_NAME_FLB"] = string.Empty;
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            row["PHN_GENBA_NAME_FLB"] = string.Empty;
                        }

                        for (int i = 0; i < ConstMaxHinmeiCount; i++)
                        {
                            if (dataTableHeaderTmp.Rows.Count > 0)
                            {
                                // 品名
                                ctrlName = string.Format("HINMEI_NANE_{0}", i + (colRoop * ConstMaxHinmeiCount) + 1);
                                index = dataTableHeaderTmp.Columns.IndexOf(ctrlName);
                                ctrlName = string.Format("PHN_HINMEI_NANE_{0}_FLB", i + 1);
                                if (index != -1)
                                {
                                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                                    {
                                        byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                        if (byteArray.Length > 20)
                                        {
                                            row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 単位
                                ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + (colRoop * ConstMaxHinmeiCount) + 1);
                                index = dataTableHeaderTmp.Columns.IndexOf(ctrlName);
                                ctrlName = string.Format("PHN_HINMEI_UNIT_UAME_{0}_FLB", i + 1);
                                if (index != -1)
                                {
                                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                                    {
                                        byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                        if (byteArray.Length > 6)
                                        {
                                            row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }
                            }
                            else
                            {
                                // 品名
                                ctrlName = string.Format("HINMEI_NANE_{0}", i + (colRoop * ConstMaxHinmeiCount) + 1);
                                index = dataTableHeaderTmp.Columns.IndexOf(ctrlName);
                                ctrlName = string.Format("PHN_HINMEI_NANE_{0}_FLB", i + 1);
                                row[ctrlName] = string.Empty;

                                // 単位
                                ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + (colRoop * ConstMaxHinmeiCount) + 1);
                                index = dataTableHeaderTmp.Columns.IndexOf(ctrlName);
                                ctrlName = string.Format("PHN_HINMEI_UNIT_UAME_{0}_FLB", i + 1);
                                row[ctrlName] = string.Empty;
                            }
                        }

                        #endregion - Header -

                        #region - Detail -

                        if (y >= dataTableDetailTmp.Rows.Count)
                        {   // 全て空白

                            // 日付
                            row["PHN_DATE_FLB"] = string.Empty;

                            // 数量
                            for (x = 0; x < ConstMaxHinmeiCount; x++)
                            {
                                ctrlName = string.Format("PHN_SURYO_{0}_FLB", x + 1);
                                row[ctrlName] = string.Empty;
                            }
                        }
                        else
                        {
                            // 日付
                            index = dataTableDetailTmp.Columns.IndexOf("DATE");
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[y].ItemArray[index]))
                            {
                                row["PHN_DATE_FLB"] = dataTableDetailTmp.Rows[y].ItemArray[index];
                            }
                            else
                            {
                                row["PHN_DATE_FLB"] = string.Empty;
                            }

                            // 数量
                            for (x = 0; x < ConstMaxHinmeiCount; x++)
                            {
                                ctrlName = string.Format("HINMEI_SURYO_{0}", x + (colRoop * ConstMaxHinmeiCount) + 1);
                                index = dataTableDetailTmp.Columns.IndexOf(ctrlName);
                                ctrlName = string.Format("PHN_SURYO_{0}_FLB", x + 1);
                                if (index != -1)
                                {
                                    if (!this.IsDBNull(dataTableDetailTmp.Rows[y].ItemArray[index]))
                                    {
                                        row[ctrlName] = dataTableDetailTmp.Rows[y].ItemArray[index];
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }
                            }
                        }

                        #endregion - Detail -

                        #region - Footer -

                        // 合計
                        for (x = 0; x < ConstMaxHinmeiCount; x++)
                        {
                            if (dataTableFooterTmp.Rows.Count > 0)
                            {
                                ctrlName = string.Format("GOUGEI_{0}", x + (colRoop * ConstMaxHinmeiCount) + 1);
                                index = dataTableFooterTmp.Columns.IndexOf(ctrlName);
                                ctrlName = string.Format("PHN_GOUGEI_{0}_FLB", x + 1);
                                if (index != -1)
                                {
                                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                                    {
                                        row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }
                            }
                            else
                            {
                                ctrlName = string.Format("GOUGEI_{0}", x + (colRoop * ConstMaxHinmeiCount) + 1);
                                index = dataTableFooterTmp.Columns.IndexOf(ctrlName);
                                ctrlName = string.Format("PHN_GOUGEI_{0}_FLB", x + 1);
                                row[ctrlName] = string.Empty;
                            }
                        }

                        #endregion - Footer -

                        this.dataTable.Rows.Add(row);
                    }
                }
            }

            this.SetRecord(this.dataTable);
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
        }

        /// <summary>帳票出力用データテーブル作成処理を実行する</summary>
        private void SetChouhyouInfo()
        {
            try
            {
                int index;
                DataTable dataTableHeaderTmp = null;

                int year;
                int month;
                string gyoushaCD;
                string genbaCD;
                string ctrlName = string.Empty;

                Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                byte[] byteArray;

                string[] keysName2;
                foreach (string keyName1 in this.DataTablePageList.Keys)
                {
                    keysName2 = keyName1.Split('_');
                    if (keysName2.Length != 4)
                    {   // 仕様外
                        return;
                    }

                    year = int.Parse(keysName2[0]);
                    month = int.Parse(keysName2[1]);
                    gyoushaCD = keysName2[2].ToString();
                    genbaCD = keysName2[3].ToString();

                    if (this.DataTablePageList[keyName1].Keys.Count != 3)
                    {   // 仕様外
                        return;
                    }

                    dataTableHeaderTmp = this.DataTablePageList[keyName1]["Header"];

                    #region - Header -

                    // タイトル
                    this.SetFieldName("PHY_TITLE_FLB", r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.windowID));

                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", string.Empty);
                        }
                    }
                    else
                    {
                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                        this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", string.Empty);
                    }

                    #endregion - Header -
                }
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
