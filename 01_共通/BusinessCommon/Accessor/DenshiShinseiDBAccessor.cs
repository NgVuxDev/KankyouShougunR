using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.Logic;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.Dto;

namespace Shougun.Core.Common.BusinessCommon
{
    /// <summary>
    /// 電子申請用のDBAccessor
    /// </summary>
    public class DenshiShinseiDBAccessor
    {
        #region フィールド
        /// <summary>電子申請入力</summary>
        IT_DENSHI_SHINSEI_ENTRYDao DSEntry;

        /// <summary>電子申請明細</summary>
        IT_DENSHI_SHINSEI_DETAILDao DSDetail;

        /// <summary>電子申請状態</summary>
        IT_DENSHI_SHINSEI_STATUSDao DSStatus;

        /// <summary>電子申請明細承認否認</summary>
        IT_DENSHI_SHINSEI_DETAIL_ACTIONDao DSDetailAction;

        /// <summary>BusinessCommon.DBAccessor</summary>
        DBAccessor PlainDBAccessor;

        /// <summary>DenshiShinseiUtility</summary>
        DenshiShinseiUtility DSUtility;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenshiShinseiDBAccessor()
        {
            this.DSEntry = DaoInitUtility.GetComponent<IT_DENSHI_SHINSEI_ENTRYDao>();
            this.DSDetail = DaoInitUtility.GetComponent<IT_DENSHI_SHINSEI_DETAILDao>();
            this.DSStatus = DaoInitUtility.GetComponent<IT_DENSHI_SHINSEI_STATUSDao>();
            this.DSDetailAction = DaoInitUtility.GetComponent<IT_DENSHI_SHINSEI_DETAIL_ACTIONDao>();
            this.PlainDBAccessor = new DBAccessor();
            this.DSUtility = new DenshiShinseiUtility();
        }
        #endregion

        #region DBアクセッサ

        #region 電子申請入力登録
        /// <summary>
        /// 電子申請入力の登録
        /// 以下の項目はこのメソッド内で採番、設定します。
        /// SYSTEM_ID、SEQ、申請番号、
        /// 申請者CD、
        /// 作成者、作成日時、作成PC、最終更新者、最終更新日時、最終更新PC、
        /// 削除フラグ
        /// </summary>
        /// <param name="dsEntry">登録対象のEntity</param>
        public void InsertDSEntry(T_DENSHI_SHINSEI_ENTRY dsEntry, DenshiShinseiUtility.SHINSEI_MASTER_KBN materKbn)
        {
            if (dsEntry == null)
            {
                return;
            }

            // SYSTEM_ID採番
            dsEntry.SYSTEM_ID = this.PlainDBAccessor.createSystemId((int)DENSHU_KBN.DENSHI_SHINSEI);

            // 申請番号採番
            dsEntry.SHINSEI_NUMBER = this.PlainDBAccessor.createDenshuNumber((int)DENSHU_KBN.DENSHI_SHINSEI);

            // SEQは1固定
            dsEntry.SEQ = 1;

            // 申請マスタ区分
            dsEntry.SHINSEI_MASTER_KBN = (short)materKbn;

            // 申請者CD(ログインユーザ)
            dsEntry.SHINSEISHA_CD = SystemProperty.Shain.CD;

            // 作成者～採集更新PCまで設定
            var dsEntryBinderLogic = new DataBinderLogic<T_DENSHI_SHINSEI_ENTRY>(dsEntry);
            dsEntryBinderLogic.SetSystemProperty(dsEntry, false);

            // DELETE_FLGは0固定
            dsEntry.DELETE_FLG = false;

            this.DSEntry.Insert(dsEntry);
        }
        #endregion

        #region 電子申請明細登録
        /// <summary>
        /// 電子申請明細の登録
        /// DETAIL_SYSTEM_IDはこのメソッドで採番する。
        /// </summary>
        /// <param name="dsDetail">登録対象のEntity</param>
        /// <param name="sysId">関連するT_DENSHI_SHINSEI_ENTRY.SYSTEM_ID</param>
        /// <param name="seq">関連するT_DENSHI_SHINSEI_ENTRY.SEQ</param>
        /// <param name="shinseiNumber">T_DENSHI_SHINSEI_ENTRY.SHINSEI_NUMBER</param>
        public void InsertDSDetail(T_DENSHI_SHINSEI_DETAIL dsDetail, SqlInt64 sysId, SqlInt32 seq, SqlInt64 shinseiNumber)
        {
            if (dsDetail == null)
            {
                return;
            }

            dsDetail.SYSTEM_ID = sysId;
            dsDetail.SEQ = seq;
            dsDetail.SHINSEI_NUMBER = shinseiNumber;

            // DETAIL_SYSTEM_IDを採番
            dsDetail.DETAIL_SYSTEM_ID = this.PlainDBAccessor.createSystemId((int)DENSHU_KBN.DENSHI_SHINSEI);

            this.DSDetail.Insert(dsDetail);

        }
        #endregion

        #region 電子申請状態登録
        /// <summary>
        /// 電子申請状態の登録
        /// 以下の項目はこのメソッド内で採番、設定します。
        /// 更新回数、申請状態CD、更新状態
        /// 作成者、作成日時、作成PC、最終更新者、最終更新日時、最終更新PC、削除フラグ
        /// </summary>
        /// <param name="dsDetail">登録対象のEntity</param>
        /// <param name="sysId">関連するT_DENSHI_SHINSEI_ENTRY.SYSTEM_ID</param>
        /// <param name="seq">関連するT_DENSHI_SHINSEI_ENTRY.SEQ</param>
        public void InsertDSStatus(T_DENSHI_SHINSEI_STATUS dsStatus, SqlInt64 sysId, SqlInt32 seq)
        {
            if (dsStatus == null)
            {
                return;
            }

            dsStatus.SYSTEM_ID = sysId;
            dsStatus.SEQ = seq;
            dsStatus.UPDATE_NUM = 1;
            dsStatus.SHINSEI_STATUS_CD = (int)DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPLYING;
            dsStatus.SHINSEI_STATUS = this.DSUtility.ToString(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPLYING);

            // 作成者～採集更新PCまで設定
            var dsStatusBinderLogic = new DataBinderLogic<T_DENSHI_SHINSEI_STATUS>(dsStatus);
            dsStatusBinderLogic.SetSystemProperty(dsStatus, false);

            // DELETE_FLGは0固定
            dsStatus.DELETE_FLG = false;

            this.DSStatus.Insert(dsStatus);
        }
        #endregion

        #endregion
    }
}
