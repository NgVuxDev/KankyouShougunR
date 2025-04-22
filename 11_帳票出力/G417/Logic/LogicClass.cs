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

    #region - LogicClass -

    /// <summary>ビジネスロジック</summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>フォーム</summary>
        private G417_MeisaihyoSyukeihyoPatternSentakuPopupForm form;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>コンストラクタ</summary>
        public LogicClass(G417_MeisaihyoSyukeihyoPatternSentakuPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MasterListPatternLogic = new MListPatternLogic(targetForm);

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MasterListPatternColumnLogic = new MListPatternColumnLogic(targetForm);

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MasterListPatternFillCondLogic = new MListPatternFillCondLogic(targetForm);

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.SummaryListColumnSelectLogic = new SListColumnSelectLogic(targetForm);

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>マスターリストパターンロジック</summary>
        public IBuisinessLogic MasterListPatternLogic { get; private set; }

        /// <summary>マスターリストパターンカラムロジック</summary>
        public IBuisinessLogic MasterListPatternColumnLogic { get; private set; }

        /// <summary>マスターリストパターンフィルコンドロジック</summary>
        public IBuisinessLogic MasterListPatternFillCondLogic { get; private set; }

        /// <summary>DAO(マスターリストパターンカラムのDTO)</summary>
        public IBuisinessLogic SummaryListColumnSelectLogic { get; private set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>パターン削除処理を実行する</summary>
        public void LogicalDelete()
        {
            this.MasterListPatternLogic.LogicalDelete();
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

        /// <summary>パターン情報取得処理を実行する</summary>
        /// <param name="index">インデックス番号を表す数値</param>
        /// <param name="systemID">システムＩＤを表す数値</param>
        /// <param name="sequenceNo">シーケンス番号を表す数値</param>
        /// <param name="patternName">パターン名を表す文字列</param>
        public void GetPatternInfo(int index, ref long systemID, ref int sequenceNo, ref string patternName, ref string createUser, ref string createPC, ref DateTime createDate, ref object timeStamp)
        {
            PatternListItem item = ((MListPatternLogic)this.MasterListPatternLogic).PatternListItems[index];

            systemID = item.SystemID;
            sequenceNo = item.SequenceNo;
            patternName = item.PatternName;

            // 作成者名
            createUser = item.CreateUser;

            // 作成PC
            createPC = item.CreatePC;

            // 作成日付
            createDate = item.CreateDate;

            // タイムスタンプ
            timeStamp = item.CreateTimeStamp;
        }

        #endregion - Methods -
    }

    #endregion - LogicClass -

    #endregion - Class -
}
