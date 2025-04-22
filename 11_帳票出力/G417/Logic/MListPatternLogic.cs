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
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup
{
    #region - Class -

    /// <summary>マスターリストパターン用ビジネスロジックを表すクラス・コントロール</summary>
    internal class MListPatternLogic : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>データーベースアクセサを保持するフィールド</summary>
        private DBAccessor dbAccessor = new DBAccessor();

        /// <summary>DTO(マスターリストパターンのDTO)</summary>
        private M_LIST_PATTERN dto;

        /// <summary>フォーム</summary>
        private G417_MeisaihyoSyukeihyoPatternSentakuPopupForm form;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>コンストラクタ</summary>
        public MListPatternLogic(G417_MeisaihyoSyukeihyoPatternSentakuPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.PatternListItems = new List<PatternListItem>();

            this.form = targetForm;
            this.dto = new M_LIST_PATTERN();
            this.Dao = DaoInitUtility.GetComponent<IMLPDao>();
            this.mLPFCDao = DaoInitUtility.GetComponent<IMLPFCDao>();
            this.mLPCDao = DaoInitUtility.GetComponent<IMLPCDao>();

            //// 最大シーケンス番号取得
            //this.GetMaxSequenceNo();

            // パターンリスト情報の取得
            this.GetPatternListItems();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>パターンリストアイテム</summary>
        public List<PatternListItem> PatternListItems { get; private set; }

        /// <summary>DAO(マスターリストパターンのDTO)</summary>
        public IMLPDao Dao { get; private set; }

        /// <summary>DAO(マスターリストパターンのDTO)</summary>
        public IMLPFCDao mLPFCDao { get; private set; }

        /// <summary>DAO(マスターリストパターンのDTO)</summary>
        public IMLPCDao mLPCDao { get; private set; }

        #endregion - Properties -

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

        /// <summary>パターンリスト情報の取得処理を実行する</summary>
        internal void GetPatternListItems()
        {
            DataTable dataTable = this.Dao.GetMListPatternData((int)this.form.WindowID);

            // システムＩＤ
            int indexSystemID = dataTable.Columns.IndexOf("SYSTEM_ID");
            // シーケンス番号
            int indexSequenceNo = dataTable.Columns.IndexOf("SEQ");
            // パターン名
            int indexPatternName = dataTable.Columns.IndexOf("PATTERN_NAME");
            // 作成者
            int indexCreateUser = dataTable.Columns.IndexOf("CREATE_USER");
            // 作成ＰＣ
            int indexCreatePC = dataTable.Columns.IndexOf("CREATE_PC");
            // 作成日付
            int indexCreateDate = dataTable.Columns.IndexOf("CREATE_DATE");
            // タイムスタンプ
            int indexTimeStamp = dataTable.Columns.IndexOf("TIME_STAMP");

            this.form.PatternNameList.Clear();
            this.form.customListBox.Items.Clear();
            //this.form.customListBox.Items.Add("新規の場合はここを選択");
            this.form.customListBox.Items.Add(string.Empty);
            this.PatternListItems.Add(new PatternListItem(-1, -1, string.Empty, string.Empty, string.Empty, DateTime.Now, null));
            foreach (DataRow dataRow in dataTable.Rows)
            {
                this.PatternListItems.Add(new PatternListItem((long)dataRow.ItemArray[indexSystemID], (int)dataRow.ItemArray[indexSequenceNo], (string)dataRow.ItemArray[indexPatternName], (string)dataRow.ItemArray[indexCreateUser], (string)dataRow.ItemArray[indexCreatePC], (DateTime)dataRow.ItemArray[indexCreateDate], dataRow.ItemArray[indexTimeStamp]));

                this.form.PatternNameList.Add((string)dataRow.ItemArray[indexPatternName]);

                this.form.customListBox.Items.Add(dataRow.ItemArray[indexPatternName]);
            }

            this.form.customListBox.SelectedIndex = 0;
        }

        ///// <summary>システムＩＤ及びシーケンス番号の最大値取得処理を取得する</summary>
        //internal void GetMaxSequenceNo()
        //{
        //    // SEQ項目の最大値取得(Test)
        //    string sql = string.Format("SELECT * FROM M_LIST_PATTERN WHERE WINDOW_ID = {0} AND DELETE_FLG = 0 AND SEQ = (SELECT MAX(SEQ) FROM M_LIST_PATTERN WHERE WINDOW_ID = {1} AND DELETE_FLG = 0)", (int)this.form.WindowId, (int)this.form.WindowId);
        //    DataTable dataTable = this.Dao.GetDateForStringSql(sql);

        //    if (dataTable.Rows.Count != 0)
        //    {
        //        int index;
        //        DataColumnCollection columns = dataTable.Columns;

        //        // システムＩＤ
        //        index = columns.IndexOf("SYSTEM_ID");
        //        this.form.SystemID = (long)dataTable.Rows[0].ItemArray[index];

        //        // 最大シーケンス番号
        //        index = columns.IndexOf("SEQ");
        //        this.form.MaxSequenceNo = (int)dataTable.Rows[0].ItemArray[index];
        //    }
        //}

        internal void Delete(long systemID, int sequenceNo, string patternName, string createUser, string createPC, DateTime createDate, object timeStamp)
        {
            M_LIST_PATTERN masterListPattern = new M_LIST_PATTERN();

            // WHOカラム設定
            var who = new DataBinderLogic<M_LIST_PATTERN>(masterListPattern);
            who.SetSystemProperty(masterListPattern, true);

            //DateTime dateTime = DateTime.Now;
            //masterListPattern.CREATE_USER = "Create-User";
            //masterListPattern.CREATE_DATE = dateTime;
            //masterListPattern.SEARCH_CREATE_DATE = dateTime.ToString("yyyy/MM/dd");
            //masterListPattern.CREATE_PC = "Create-PC";
            //masterListPattern.UPDATE_USER = "Update-User";
            //masterListPattern.UPDATE_DATE = dateTime;
            //masterListPattern.SEARCH_UPDATE_DATE = dateTime.ToString("yyyy/MM/dd");
            //masterListPattern.UPDATE_PC = "Update-PC";
            ////mlistPattern.TIME_STAMP = dateTime.ToString("yyyy/MM/dd");
            //masterListPattern.UPDATE_PC = "Update-PC";

            masterListPattern.SYSTEM_ID = systemID;

            masterListPattern.SEQ = sequenceNo;

            int windowID = (int)this.form.WindowID;
            masterListPattern.WINDOW_ID = windowID;

            masterListPattern.PATTERN_NAME = patternName;

            masterListPattern.DELETE_FLG = SqlBoolean.True;

            masterListPattern.CREATE_USER = createUser;
            masterListPattern.CREATE_PC = createPC;
            masterListPattern.CREATE_DATE = createDate;
            masterListPattern.TIME_STAMP = (byte[])timeStamp;

            // データーベース更新
            this.Dao.Update(masterListPattern);
            masterListPattern.SEQ = sequenceNo + 1;
            this.Dao.Insert(masterListPattern);

            #region - M_LIST_PATTERN_FILL_COND -

            M_LIST_PATTERN_FILL_COND mlistPatternFillCond = new M_LIST_PATTERN_FILL_COND();
            mlistPatternFillCond.SYSTEM_ID = systemID;
            mlistPatternFillCond.SEQ = sequenceNo;
            DataTable dtPFC = mLPFCDao.GetDataForEntity(mlistPatternFillCond);

            mlistPatternFillCond = new M_LIST_PATTERN_FILL_COND();
            mlistPatternFillCond.SYSTEM_ID = systemID;
            mlistPatternFillCond.SEQ = sequenceNo + 1;
            mlistPatternFillCond.FILL_COND_DATE_KBN = dtPFC.Rows[0].Field<Int16>("FILL_COND_DATE_KBN");
            mlistPatternFillCond.FILL_COND_DATE_BEGIN = dtPFC.Rows[0].Field<DateTime>("FILL_COND_DATE_BEGIN");
            mlistPatternFillCond.FILL_COND_DATE_END = dtPFC.Rows[0].Field<DateTime>("FILL_COND_DATE_END");
            mlistPatternFillCond.FILL_COND_KYOTEN_CD = dtPFC.Rows[0].Field<Int16>("FILL_COND_KYOTEN_CD");
            mlistPatternFillCond.FILL_COND_DENPYOU_SBT = dtPFC.Rows[0].Field<Int16>("FILL_COND_DENPYOU_SBT");
            mlistPatternFillCond.FILL_COND_DENPYOU_KBN = dtPFC.Rows[0].Field<Int16>("FILL_COND_DENPYOU_KBN");
            mlistPatternFillCond.FILL_COND_ID_1 = dtPFC.Rows[0].Field<int>("FILL_COND_ID_1");
            mlistPatternFillCond.FILL_COND_CD_BEGIN_1 = dtPFC.Rows[0].Field<string>("FILL_COND_CD_BEGIN_1");
            mlistPatternFillCond.FILL_COND_CD_END_1 = dtPFC.Rows[0].Field<string>("FILL_COND_CD_END_1");
            mlistPatternFillCond.FILL_COND_ID_2 = dtPFC.Rows[0].Field<int>("FILL_COND_ID_2");
            mlistPatternFillCond.FILL_COND_CD_BEGIN_2 = dtPFC.Rows[0].Field<string>("FILL_COND_CD_BEGIN_2");
            mlistPatternFillCond.FILL_COND_CD_END_2 = dtPFC.Rows[0].Field<string>("FILL_COND_CD_END_2");
            mlistPatternFillCond.FILL_COND_ID_3 = dtPFC.Rows[0].Field<int>("FILL_COND_ID_3");
            mlistPatternFillCond.FILL_COND_CD_BEGIN_3 = dtPFC.Rows[0].Field<string>("FILL_COND_CD_BEGIN_3");
            mlistPatternFillCond.FILL_COND_CD_END_3 = dtPFC.Rows[0].Field<string>("FILL_COND_CD_END_3");
            mlistPatternFillCond.FILL_COND_ID_4 = dtPFC.Rows[0].Field<int>("FILL_COND_ID_4");
            mlistPatternFillCond.FILL_COND_CD_BEGIN_4 = dtPFC.Rows[0].Field<string>("FILL_COND_CD_BEGIN_4");
            mlistPatternFillCond.FILL_COND_CD_END_4 = dtPFC.Rows[0].Field<string>("FILL_COND_CD_END_4");
            mLPFCDao.Insert(mlistPatternFillCond);
            #endregion

            #region - M_LIST_PATTERN_COLUMN -
            // システムID
            DBAccessor dbAccessor = new DBAccessor();
            int densyuKubun = (int)DENSHU_KBN.HANYOU_CHOUHYOU;

            M_LIST_PATTERN_COLUMN mlistPatternColumn = new M_LIST_PATTERN_COLUMN();
            mlistPatternColumn.SYSTEM_ID = systemID;
            mlistPatternColumn.SEQ = sequenceNo;

            DataTable dtPC = mLPCDao.GetDataForEntity(mlistPatternColumn);

            foreach (DataRow row in dtPC.Rows)
            {
                mlistPatternColumn = new M_LIST_PATTERN_COLUMN();
                mlistPatternColumn.SYSTEM_ID = systemID;
                mlistPatternColumn.SEQ = sequenceNo + 1;
                mlistPatternColumn.DETAIL_SYSTEM_ID = dbAccessor.createSystemId((SqlInt16)densyuKubun);
                mlistPatternColumn.DETAIL_KBN = row.Field<bool>("DETAIL_KBN");
                mlistPatternColumn.ROW_NO = row.Field<Int16>("ROW_NO");
                mlistPatternColumn.WINDOW_ID = row.Field<int>("WINDOW_ID");
                mlistPatternColumn.KOUMOKU_ID = row.Field<int>("KOUMOKU_ID");
                mLPCDao.Insert(mlistPatternColumn);
            }
            #endregion
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
