using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup
{
    #region - Class -

    /// <summary>一覧表項目選択用ビジネスロジック</summary>
    internal class SListColumnSelectLogic : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>データーベースアクセサを保持するフィールド</summary>
        private DBAccessor dbAccessor = new DBAccessor();

        /// <summary>DAO(マスターリストパターンカラムのDTO)</summary>
        private ISLCSDao slcsDao;

        /// <summary>DTO(マスターリストパターンカラムのDTO)</summary>
        private S_LIST_COLUMN_SELECT slcsDto;

        /// <summary>フォーム</summary>
        private G417_MeisaihyoSyukeihyoPatternSentakuPopupForm form;

        /// <summary>出力選択可能項目（伝票）を保持するフィールド</summary>
        private DataTable dataTableOutputSelectEnableDenpyoItems;

        /// <summary>出力選択可能項目（明細）を保持するフィールド</summary>
        private DataTable dataTableOutputSelectEnableMeisaiItems;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>コンストラクタ</summary>
        public SListColumnSelectLogic(G417_MeisaihyoSyukeihyoPatternSentakuPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.slcsDto = new S_LIST_COLUMN_SELECT();
            this.slcsDao = DaoInitUtility.GetComponent<ISLCSDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>削除処理を実行する</summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>出力選択可能項目（伝票及び明細）の取得処理を実行する</summary>
        public void GetOutputSelectEnableItems()
        {
            int indexItem = 0;
            int indexKoumokuRonriName;
            int indexKoumokuID;
            int indexTableName;
            int indexButsuriName;
            int indexDataType;
            int indexOutputFormat;
            int indexOutputAlign;
            int indexTotalKubun;

            int id;
            int prevID = 0;
            string koumokuRonriName = string.Empty;
            string tableName = string.Empty;
            string butsuriName = string.Empty;
            string dataType = string.Empty;
            string outputFormat = string.Empty;
            int outputAlign = -1;
            bool isTotalKubun = false;
            Type type = null;

            string sql;
            DataTable dataTableTmp;

            // 出力選択可能項目（伝票）
            this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList.Clear();

            if (this.form.CommonChouhyou.OutEnableKoumokuDenpyou)
            {   // 出力可能（伝票）
                this.dataTableOutputSelectEnableDenpyoItems = this.slcsDao.OutputSelectEnableItems((int)this.form.WindowID, 0);

                indexKoumokuID = this.dataTableOutputSelectEnableDenpyoItems.Columns.IndexOf("KOUMOKU_ID");
                indexKoumokuRonriName = this.dataTableOutputSelectEnableDenpyoItems.Columns.IndexOf("KOUMOKU_RONRI_NAME");
                indexTableName = this.dataTableOutputSelectEnableDenpyoItems.Columns.IndexOf("TABLE_NAME");
                indexButsuriName = this.dataTableOutputSelectEnableDenpyoItems.Columns.IndexOf("BUTSURI_NAME");
                indexDataType = this.dataTableOutputSelectEnableDenpyoItems.Columns.IndexOf("DATA_TYPE");
                indexOutputFormat = this.dataTableOutputSelectEnableDenpyoItems.Columns.IndexOf("OUTPUT_FORMAT");
                indexOutputAlign = this.dataTableOutputSelectEnableDenpyoItems.Columns.IndexOf("OUTPUT_ALIGN");
                indexTotalKubun = this.dataTableOutputSelectEnableDenpyoItems.Columns.IndexOf("TOTAL_KBN");

                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = null;
                ChouhyouOutKoumoku[] chouhyouOutKoumokuList = null;
                foreach (DataRow row in this.dataTableOutputSelectEnableDenpyoItems.Rows)
                {
                    id = (int)row.ItemArray[indexKoumokuID];

                    if (id != prevID)
                    {
                        if (prevID != 0)
                        {
                            chouhyouOutKoumokuGroup = new ChouhyouOutKoumokuGroup(chouhyouOutKoumokuList);
                            this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList.Add(chouhyouOutKoumokuGroup);
                        }

                        prevID = id;

                        chouhyouOutKoumokuList = new ChouhyouOutKoumoku[4];

                        koumokuRonriName = (string)row.ItemArray[indexKoumokuRonriName];
                    }

                    koumokuRonriName = (string)row.ItemArray[indexKoumokuRonriName];
                    tableName = (string)row.ItemArray[indexTableName];
                    butsuriName = (string)row.ItemArray[indexButsuriName];
                    dataType = (string)row.ItemArray[indexDataType];
                    outputFormat = (string)row.ItemArray[indexOutputFormat];
                    outputAlign = (short)row.ItemArray[indexOutputAlign];
                    isTotalKubun = (bool)row.ItemArray[indexTotalKubun];

                    if (dataType.Contains("datetime") || dataType.Contains("date") || dataType.Contains("time"))
                    {
                        type = typeof(DateTime);
                    }
                    else if (dataType.Contains("decimal") || dataType.Contains("money"))
                    {
                        type = typeof(decimal);
                    }
                    else if (dataType.Contains("float"))
                    {
                        type = typeof(double);
                    }
                    else if (dataType.Contains("bigint"))
                    {
                        type = typeof(long);
                    }
                    else if (dataType.Contains("int"))
                    {
                        type = typeof(int);
                    }
                    else if (dataType.Contains("bit"))
                    {
                        type = typeof(bool);
                    }
                    else
                    {
                        type = typeof(string);
                    }

                    switch (this.form.WindowID)
                    {
                        case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:
                        case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU:
                        case WINDOW_ID.R_NYUUKIN_MEISAIHYOU:
                        case WINDOW_ID.R_SYUKKINN_MEISAIHYOU:
                        case WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU:
                        case WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU:
                        case WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU:
                        case WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU:
                            indexItem = 0;
                            break;

                        case WINDOW_ID.R_URIAGE_MEISAIHYOU:
                        case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:
                        case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:
                        case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:
                        case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:
                        case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:
                            if (tableName.Equals("T_UKEIRE_ENTRY"))
                            {
                                indexItem = 0;
                            }
                            else if (tableName.Equals("T_SHUKKA_ENTRY"))
                            {
                                indexItem = 1;
                            }
                            else if (tableName.Equals("T_UR_SH_ENTRY"))
                            {
                                indexItem = 2;
                            }

                            break;
                        case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                        case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                        case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:              // R432(売上/支払推移表)
                        case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                      // R432(計量推移表)
                        case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_SUIIHYOU:          // R432(売上・支払（全て）推移表)
                        case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)
                        case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                        case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:            // R433(売上/支払順位表)
                        case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                    // R433(計量順位表)
                        case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_JYUNNIHYOU:        // R433(売上・支払（全て）順位表)
                        case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                        case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                        case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:      // R434(売上/支払前年対比表)
                        case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:              // R434(計量前年対比表)
                        case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_ZENNEN_TAIHIHYOU:  // R434(売上・支払（全て）前年対比表)

                            break;
                        case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:                   // R342 受付明細表
                            if (tableName.Equals("T_UKETSUKE_SS_ENTRY"))
                            {   // 受付収集
                                indexItem = 0;
                            }
                            else if (tableName.Equals("T_UKETSUKE_SK_ENTRY"))
                            {   // 受付出荷
                                indexItem = 1;
                            }
                            else if (tableName.Equals("T_UKETSUKE_MK_ENTRY"))
                            {   // 受付持込
                                indexItem = 2;
                            }
                            else if (tableName.Equals("T_UKETSUKE_BP_ENTRY"))
                            {   // 受付物販
                                indexItem = 3;
                            }

                            break;
                        case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:                    // R398 運賃明細表

                            break;
                        case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:                    // R351 計量明細表
                        case WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU:                   // R352 計量集計表

                            break;
                        default:
                            break;
                    }

                    switch (outputFormat)
                    {
                        case "SYS_SUURYOU_FORMAT":
                            sql = "SELECT M_SYS_INFO.SYS_SUURYOU_FORMAT FROM M_SYS_INFO";
                            dataTableTmp = this.slcsDao.GetDateForStringSql(sql);
                            outputFormat = (string)dataTableTmp.Rows[0].ItemArray[0];

                            break;
                        case "SYS_JYURYOU_FORMAT":
                            sql = "SELECT M_SYS_INFO.SYS_JYURYOU_FORMAT FROM M_SYS_INFO";
                            dataTableTmp = this.slcsDao.GetDateForStringSql(sql);
                            outputFormat = (string)dataTableTmp.Rows[0].ItemArray[0];

                            break;
                        case "SYS_TANKA_FORMAT":
                            sql = "SELECT M_SYS_INFO.SYS_TANKA_FORMAT FROM M_SYS_INFO";
                            dataTableTmp = this.slcsDao.GetDateForStringSql(sql);
                            outputFormat = (string)dataTableTmp.Rows[0].ItemArray[0];
                            
                            break;
                        default:

                            break;
                    }

                    chouhyouOutKoumokuList[indexItem] = new ChouhyouOutKoumoku(id, koumokuRonriName, tableName, butsuriName, type, outputFormat, outputAlign, isTotalKubun);
                }

                chouhyouOutKoumokuGroup = new ChouhyouOutKoumokuGroup(chouhyouOutKoumokuList);

                this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList.Add(chouhyouOutKoumokuGroup);
            }

            // 出力選択・可能項目（明細）
            this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList.Clear();

            prevID = 0;
            if (this.form.CommonChouhyou.OutEnableKoumokuMeisai)
            {   // 出力可能（明細）
                this.dataTableOutputSelectEnableMeisaiItems = this.slcsDao.OutputSelectEnableItems((int)this.form.WindowID, 1);

                indexKoumokuID = this.dataTableOutputSelectEnableMeisaiItems.Columns.IndexOf("KOUMOKU_ID");
                indexKoumokuRonriName = this.dataTableOutputSelectEnableMeisaiItems.Columns.IndexOf("KOUMOKU_RONRI_NAME");
                indexTableName = this.dataTableOutputSelectEnableMeisaiItems.Columns.IndexOf("TABLE_NAME");
                indexButsuriName = this.dataTableOutputSelectEnableMeisaiItems.Columns.IndexOf("BUTSURI_NAME");
                indexDataType = this.dataTableOutputSelectEnableMeisaiItems.Columns.IndexOf("DATA_TYPE");
                indexOutputFormat = this.dataTableOutputSelectEnableMeisaiItems.Columns.IndexOf("OUTPUT_FORMAT");
                indexOutputAlign = this.dataTableOutputSelectEnableMeisaiItems.Columns.IndexOf("OUTPUT_ALIGN");
                indexTotalKubun = this.dataTableOutputSelectEnableMeisaiItems.Columns.IndexOf("TOTAL_KBN");

                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = null;
                ChouhyouOutKoumoku[] chouhyouOutKoumokuList = null;
                foreach (DataRow row in this.dataTableOutputSelectEnableMeisaiItems.Rows)
                {
                    id = (int)row.ItemArray[indexKoumokuID];

                    if (id != prevID)
                    {
                        if (prevID != 0)
                        {
                            chouhyouOutKoumokuGroup = new ChouhyouOutKoumokuGroup(chouhyouOutKoumokuList);
                            this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList.Add(chouhyouOutKoumokuGroup);
                        }

                        prevID = id;
                        indexItem = 0;

                        chouhyouOutKoumokuList = new ChouhyouOutKoumoku[4];

                        koumokuRonriName = (string)row.ItemArray[indexKoumokuRonriName];
                    }

                    koumokuRonriName = (string)row.ItemArray[indexKoumokuRonriName];
                    tableName = (string)row.ItemArray[indexTableName];
                    butsuriName = (string)row.ItemArray[indexButsuriName];
                    dataType = (string)row.ItemArray[indexDataType];
                    outputFormat = (string)row.ItemArray[indexOutputFormat];
                    outputAlign = (short)row.ItemArray[indexOutputAlign];
                    isTotalKubun = (bool)row.ItemArray[indexTotalKubun];

                    if (dataType.Contains("datetime") || dataType.Contains("date") || dataType.Contains("time"))
                    {
                        type = typeof(DateTime);
                    }
                    else if (dataType.Contains("decimal") || dataType.Contains("money"))
                    {
                        type = typeof(decimal);
                    }
                    else if (dataType.Contains("float"))
                    {
                        type = typeof(double);
                    }
                    else if (dataType.Contains("bigint"))
                    {
                        type = typeof(long);
                    }
                    else if (dataType.Contains("int"))
                    {
                        type = typeof(int);
                    }
                    else if (dataType.Contains("bit"))
                    {
                        type = typeof(bool);
                    }
                    else
                    {
                        type = typeof(string);
                    }

                    switch (outputFormat)
                    {
                        case "SYS_SUURYOU_FORMAT":
                            sql = "SELECT M_SYS_INFO.SYS_SUURYOU_FORMAT FROM M_SYS_INFO";
                            dataTableTmp = this.slcsDao.GetDateForStringSql(sql);
                            outputFormat = (string)dataTableTmp.Rows[0].ItemArray[0];

                            break;
                        case "SYS_JYURYOU_FORMAT":
                            sql = "SELECT M_SYS_INFO.SYS_JYURYOU_FORMAT FROM M_SYS_INFO";
                            dataTableTmp = this.slcsDao.GetDateForStringSql(sql);
                            outputFormat = (string)dataTableTmp.Rows[0].ItemArray[0];

                            break;
                        case "SYS_TANKA_FORMAT":
                            sql = "SELECT M_SYS_INFO.SYS_TANKA_FORMAT FROM M_SYS_INFO";
                            dataTableTmp = this.slcsDao.GetDateForStringSql(sql);
                            outputFormat = (string)dataTableTmp.Rows[0].ItemArray[0];

                            break;
                        default:

                            break;
                    }

                    chouhyouOutKoumokuList[indexItem] = new ChouhyouOutKoumoku(id, koumokuRonriName, tableName, butsuriName, type, outputFormat, outputAlign, isTotalKubun);
                    
                    indexItem++;
                }

                chouhyouOutKoumokuGroup = new ChouhyouOutKoumokuGroup(chouhyouOutKoumokuList);
                this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList.Add(chouhyouOutKoumokuGroup);
            }
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
