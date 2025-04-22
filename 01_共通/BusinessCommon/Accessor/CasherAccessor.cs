// $Id: CasherAccessor.cs 42061 2015-02-10 10:10:07Z j-kikuchi $
using System;
using System.Windows.Forms;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dao;

namespace Shougun.Core.Common.BusinessCommon
{
    /// <summary>
    /// キャッシャ連携Accessor
    /// </summary>
    public class CasherAccessor
    {
        #region - Field -
        /// <summary>キャッシャ連携DAO</summary>
        CasherDAOClass dao;

        #endregion - Field -

        #region - Constructor -
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CasherAccessor()
        {
            // フィールドの初期化
            this.dao = DaoInitUtility.GetComponent<CasherDAOClass>();
        }

        #endregion - Constructor -

        #region - Utility -
        /// <summary>
        /// キャッシャ情報セット
        /// </summary>
        /// <param name="dto">キャッシャ連携DTO</param>
        public void setCasherData(CasherDTOClass dto)
        {
            var entity = new T_CASHERDATA();
            Int16 kbn = CommonConst.CASHER_MONEY_KBN_NYUUKIN;
            decimal kingaku = dto.KINGAKU;

            if(dto.DENSHU_KBN_CD == CommonConst.DENSHU_KBN_SHUKKINN)
            {
                // 伝種区分が「出金」の場合は入出金区分の初期値は「出金」にする
                kbn = CommonConst.CASHER_MONEY_KBN_SHUKKIN;
            }

            if(kingaku < 0)
            {
                // 金額が負の値の場合は正の値に補正し、入出金区分の初期値を逆転する
                kingaku *= -1;
                if(kbn == CommonConst.CASHER_MONEY_KBN_SHUKKIN)
                {
                    // 入出金区分「入金」
                    kbn = CommonConst.CASHER_MONEY_KBN_NYUUKIN;
                }
                else
                {
                    // 入出金区分「出金」
                    kbn = CommonConst.CASHER_MONEY_KBN_SHUKKIN;
                }
            }
            
            // 各情報のセット
            // ※ID, 依頼日付はDB側で自動でセットされる
            // ※処理日時はcash_drive.exeが書き込む
            entity.伝票日付 = dto.DENPYOU_DATE.ToString("yyyyMMdd");
            entity.担当者CD = dto.NYUURYOKU_TANTOUSHA_CD;
            entity.会計番号 = dto.DENPYOU_NUMBER;
            entity.科目CD = "1";
            entity.精算金額 = kingaku;
            entity.備考 = dto.BIKOU;
            entity.CLIENT = SystemInformation.ComputerName;
            entity.入出金区分 = kbn;
            entity.拠点CD = dto.KYOTEN_CD;

            // 登録
            this.dao.Insert(entity);
        }
        
        #endregion - Utility -

        #region - PrivateUtility -
        #endregion - PrivateUtility -
    }
}
