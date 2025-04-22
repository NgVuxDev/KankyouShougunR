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

    /// <summary>パターンリストアイテムを表すクラス・コントロール</summary>
    internal class PatternListItem
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="PatternListItem"/> class.</summary>
        /// <param name="systemID">システムＩＤを表す数値</param>
        /// <param name="sequenceNo">シーケンス番号を表す数値</param>
        /// <param name="patternName">パターン名を表す文字列</param>
        public PatternListItem(long systemID, int sequenceNo, string patternName, string createUser, string createPC, DateTime createDate, object timeStamp)
        {
            this.SystemID = systemID;

            this.SequenceNo = sequenceNo;

            this.PatternName = patternName;

            // 作成者名
            this.CreateUser = createUser;

            // 作成PC
            this.CreatePC = createPC;

            // 作成日付
            this.CreateDate = createDate;

            // タイムスタンプ
            this.CreateTimeStamp = timeStamp;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>システムＩＤを保持するプロパティ</summary>
        public long SystemID { get; private set; }

        /// <summary>シーケンス番号を保持するプロパティ</summary>
        public int SequenceNo { get; private set; }

        /// <summary>パターン名を保持するプロパティ</summary>
        public string PatternName { get; private set; }

        /// <summary>作成者名を保持するプロパティ</summary>
        public string CreateUser { get; private set; }

        /// <summary>作成PCを保持するプロパティ</summary>
        public string CreatePC { get; private set; }

        /// <summary>作成日付を保持するプロパティ</summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>タイムスタンプを保持するプロパティ</summary>
        public object CreateTimeStamp { get; private set; }

        #endregion - Properties -
    }

    #endregion - Class -
}
