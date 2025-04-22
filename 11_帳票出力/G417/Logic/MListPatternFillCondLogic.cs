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

    /// <summary>マスターリストパターンコンド用ビジネスロジック</summary>
    internal class MListPatternFillCondLogic : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>データーベースアクセサを保持するフィールド</summary>
        private DBAccessor dbAccessor = new DBAccessor();

        /// <summary>DAO(マスターリストパターンフィルコンドのDTO)</summary>
        private IMLPFCDao dao;

        /// <summary>DTO(マスターリストパターンフィルコンドのDTO)</summary>
        private M_LIST_PATTERN_FILL_COND dto;

        /// <summary>フォーム</summary>
        private G417_MeisaihyoSyukeihyoPatternSentakuPopupForm form;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>コンストラクタ</summary>
        public MListPatternFillCondLogic(G417_MeisaihyoSyukeihyoPatternSentakuPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new M_LIST_PATTERN_FILL_COND();
            this.dao = DaoInitUtility.GetComponent<IMLPFCDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>削除処理を実行する</summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            //M_LIST_PATTERN mListPattern = new M_LIST_PATTERN();

            //mListPattern.SYSTEM_ID = new SqlInt64((long)this.dataTable.Rows[this.form.SelectedItemIndex].ItemArray[dataTable.Columns.IndexOf("SYSTEM_ID")]);

            //mListPattern.SEQ = new SqlInt32((int)this.dataTable.Rows[this.form.SelectedItemIndex].ItemArray[dataTable.Columns.IndexOf("SEQ")]);

            //mListPattern.WINDOW_ID = new SqlInt32((int)this.dataTable.Rows[this.form.SelectedItemIndex].ItemArray[dataTable.Columns.IndexOf("WINDOW_ID")]);

            ////mListPattern.OUTPUT_KBN = new SqlInt16((short)this.dataTable.Rows[this.form.SelectedItemIndex].ItemArray[dataTable.Columns.IndexOf("OUTPUT_KBN")]);

            //mListPattern.PATTERN_NAME = (string)this.dataTable.Rows[this.form.SelectedItemIndex].ItemArray[dataTable.Columns.IndexOf("PATTERN_NAME")];

            //mListPattern.DELETE_FLG = SqlBoolean.True;

            //// データーベース更新
            //this.mlpDao.Update(mListPattern);

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

        /// <summary>マスターリストパターンデータの取得処理を実行する</summary>
        public DataTable GetMasterListPatternFillCondData()
        {
            return this.dao.GetMListPatternFillCondData((int)this.form.SystemID, (int)this.form.SequenceID);
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
