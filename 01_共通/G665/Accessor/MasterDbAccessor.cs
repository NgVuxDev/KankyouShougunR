using System.Linq;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Accessor
{
    /// <summary>
    /// マスタ関連DBアクセス
    /// </summary>
    internal class MasterDbAccessor
    {
        #region フィールド

        /// <summary>
        /// 拠点DAO
        /// </summary>
        private IM_KYOTENDao mKyotenDao;

        /// <summary>
        /// 取引先DAO
        /// </summary>
        private IM_TORIHIKISAKIDao mTorihikisakiDao;

        /// <summary>
        /// 業者DAO
        /// </summary>
        private IM_GYOUSHADao mGyoushaDao;

        /// <summary>
        /// 現場DAO
        /// </summary>
        private IM_GENBADao mGenbaDao;

        /// <summary>
        /// 入金先DAO
        /// </summary>
        private IM_NYUUKINSAKIDao mNyuukinsakiDao;

        /// <summary>
        /// 銀行DAO
        /// </summary>
        private IM_BANKDao mBankDao;

        /// <summary>
        /// 銀行支店DAO
        /// </summary>
        private IM_BANK_SHITENDao mBankShitenDao;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MasterDbAccessor()
        {
            // 各DAO初期化
            this.mKyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mTorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.mGyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mGenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.mNyuukinsakiDao = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
            this.mBankDao = DaoInitUtility.GetComponent<IM_BANKDao>();
            this.mBankShitenDao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 拠点検索
        /// </summary>
        /// <param name="kyotenCd">拠点CD</param>
        /// <param name="isNotNeedDeleteFlg">削除も含むかどうか</param>
        /// <returns>検索結果(拠点1件)</returns>
        internal M_KYOTEN GetKyoten(short kyotenCd, bool isNotNeedDeleteFlg = true)
        {
            LogUtility.DebugMethodStart(kyotenCd, isNotNeedDeleteFlg);

            var cond = new M_KYOTEN()
            {
                KYOTEN_CD = kyotenCd,
                ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg
            };
            var ret = this.mKyotenDao.GetAllValidData(cond).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 取引先検索
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="isNotNeedDeleteFlg">削除も含むかどうか</param>
        /// <returns>検索結果(拠点1件)</returns>
        internal M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, bool isNotNeedDeleteFlg = true)
        {
            LogUtility.DebugMethodStart(torihikisakiCd, isNotNeedDeleteFlg);

            var cond = new M_TORIHIKISAKI()
            {
                TORIHIKISAKI_CD = torihikisakiCd,
                ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg
            };
            var ret = this.mTorihikisakiDao.GetAllValidData(cond).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 業者検索
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="isNotNeedDeleteFlg">削除も含むかどうか</param>
        /// <returns>検索結果(拠点1件)</returns>
        internal M_GYOUSHA GetGyousha(string gyoushaCd, bool isNotNeedDeleteFlg = true)
        {
            LogUtility.DebugMethodStart(gyoushaCd, isNotNeedDeleteFlg);

            var cond = new M_GYOUSHA()
            {
                GYOUSHA_CD = gyoushaCd,
                ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg
            };
            var ret = this.mGyoushaDao.GetAllValidData(cond).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 現場検索
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="isNotNeedDeleteFlg">削除も含むかどうか</param>
        /// <returns>検索結果(拠点1件)</returns>
        internal M_GENBA GetGenba(string gyoushaCd, string genbaCd, bool isNotNeedDeleteFlg = true)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd, isNotNeedDeleteFlg);

            var cond = new M_GENBA()
            {
                GYOUSHA_CD = gyoushaCd,
                GENBA_CD = genbaCd,
                ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg
            };
            var ret = this.mGenbaDao.GetAllValidData(cond).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 入金先検索
        /// </summary>
        /// <param name="nyuukinsakiCd">入金先CD</param>
        /// <param name="isNotNeedDeleteFlg">削除も含むかどうか</param>
        /// <returns>検索結果(拠点1件)</returns>
        internal M_NYUUKINSAKI GetNyuukinsaki(string nyuukinsakiCd, bool isNotNeedDeleteFlg = true)
        {
            LogUtility.DebugMethodStart(nyuukinsakiCd, isNotNeedDeleteFlg);

            var cond = new M_NYUUKINSAKI()
            {
                NYUUKINSAKI_CD = nyuukinsakiCd,
                ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg
            };
            var ret = this.mNyuukinsakiDao.GetAllValidData(cond).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 銀行検索
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="isNotNeedDeleteFlg">削除も含むかどうか</param>
        /// <returns>検索結果(拠点1件)</returns>
        internal M_BANK GetBank(string bankCd, bool isNotNeedDeleteFlg = true)
        {
            LogUtility.DebugMethodStart(bankCd, isNotNeedDeleteFlg);

            var cond = new M_BANK()
            {
                BANK_CD = bankCd,
                ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg
            };
            var ret = this.mBankDao.GetAllValidData(cond).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 銀行支店検索
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <param name="isNotNeedDeleteFlg">削除も含むかどうか</param>
        /// <returns>検索結果(拠点1件)</returns>
        internal M_BANK_SHITEN GetBankShiten(string bankCd, string bankShitenCd, bool isNotNeedDeleteFlg = true)
        {
            LogUtility.DebugMethodStart(bankCd, bankShitenCd, isNotNeedDeleteFlg);

            var cond = new M_BANK_SHITEN()
            {
                BANK_CD = bankCd,
                BANK_SHITEN_CD = bankShitenCd,
                ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg
            };
            var ret = this.mBankShitenDao.GetAllValidData(cond).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion
    }
}