// $Id: LogicClass.cs 24789 2014-07-07 01:25:23Z j-kikuchi $
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Allocation.MobileShougunTorikomi.DAO;
using Shougun.Core.Allocation.MobileShougunTorikomi.DTO;
using Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku;
using Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku;
using Shougun.Core.Common.BusinessCommon.Const;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.Accessor
{
    /// <summary>
    /// DBAccessするためのクラス
    /// </summary>
    public class DBAccessor
    {
        #region フィールド
        
        /// <summary>
        /// 伝種区分CD(売上支払)
        /// </summary>
        public SqlInt16 DENSHU_KBN_CD_URIAGESHIHARAI = 3;

        /// <summary>
        /// 伝種区分CD(定期実績)
        /// </summary>
        public SqlInt16 DENSHU_KBN_CD_TEIKIJISSEKI = 130;


        /// <summary>
        /// IM_SYS_INFODao
        /// </summary>
        r_framework.Dao.IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// IS_NUMBER_DENSHUDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// IS_NUMBER_YEARDao
        /// </summary>
        IS_NUMBER_YEARDao numberYearDao;

        /// <summary>
        /// IS_NUMBER_DAYDao
        /// </summary>
        IS_NUMBER_DAYDao numberDayDao;

		/// <summary>
		/// MobileShougunTorikomiDAOClass
		/// </summary>
		private MobileShougunTorikomiDAOClass torikomiDAO;

		/// <summary>
		/// IM_SHOUHIZEIDao
		/// </summary>
		private r_framework.Dao.IM_SHOUHIZEIDao shouhizeiDAO;

        /// <summary>
        /// IT_TEIKI_JISSEKI_ENTRYDao
        /// </summary>
        private IT_TEIKI_JISSEKI_ENTRYDao teikiJissekiEntryDAO;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private r_framework.Dao.GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

		#endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            this.numberYearDao = DaoInitUtility.GetComponent<Shougun.Core.Allocation.MobileShougunTorikomi.DAO.IS_NUMBER_YEARDao>();
            this.numberDayDao = DaoInitUtility.GetComponent<Shougun.Core.Allocation.MobileShougunTorikomi.DAO.IS_NUMBER_DAYDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
			this.torikomiDAO = DaoInitUtility.GetComponent<MobileShougunTorikomiDAOClass>();
			this.shouhizeiDAO = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHOUHIZEIDao>();
            this.teikiJissekiEntryDAO = DaoInitUtility.GetComponent<IT_TEIKI_JISSEKI_ENTRYDao>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
		}
        #endregion

        #region DBアクセッサ

        /// <summary>
        /// 年連番のデータを取得
        /// </summary>
        /// <param name="numberedYear">年度</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        //public S_NUMBER_YEAR[] GetNumberYear(SqlInt32 numberedYear, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        public DataTable GetNumberYear(SqlInt32 numberedYear, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
            StringBuilder whereStr = new StringBuilder();
            whereStr.Append(" WHERE S_NUMBER_YEAR.DELETE_FLG = 0 ");
            whereStr.Append(" AND S_NUMBER_YEAR.NUMBERED_YEAR = ");
            whereStr.Append(numberedYear.ToString());
            whereStr.Append(" AND S_NUMBER_YEAR.DENSHU_KBN_CD = ");
            whereStr.Append(denshuKbnCd.ToString());
            whereStr.Append(" AND S_NUMBER_YEAR.KYOTEN_CD = ");
            whereStr.Append(kyotenCd.ToString());
            return this.numberYearDao.GetAllMasterDataForPopup(whereStr.ToString());
            //S_NUMBER_YEAR entity = new S_NUMBER_YEAR();
            //entity.NUMBERED_YEAR = numberedYear;
            //entity.DENSHU_KBN_CD = denshuKbnCd;
            //entity.KYOTEN_CD = kyotenCd;
            //return this.numberYearDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 年連番を登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertNumberYear(S_NUMBER_YEAR entity)
        {
            return this.numberYearDao.Insert(entity);
        }

        /// <summary>
        /// 年連番を更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateNumberYear(S_NUMBER_YEAR entity)
        {
            return this.numberYearDao.Update(entity);
        }

        /// <summary>
        /// 日連番のデータを取得
        /// </summary>
        /// <param name="numberedDay">日付</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        //public S_NUMBER_DAY[] GetNumberDay(DateTime numberedDay, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        public DataTable GetNumberDay(DateTime numberedDay, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
            StringBuilder whereStr = new StringBuilder();
            whereStr.Append(" WHERE S_NUMBER_DAY.DELETE_FLG = 0 ");
            whereStr.Append(" AND S_NUMBER_DAY.NUMBERED_DAY = '");
            whereStr.Append(numberedDay.Date.ToString());
            whereStr.Append("' ");
            whereStr.Append(" AND S_NUMBER_DAY.DENSHU_KBN_CD = ");
            whereStr.Append(denshuKbnCd.ToString());
            whereStr.Append(" AND S_NUMBER_DAY.KYOTEN_CD = ");
            whereStr.Append(kyotenCd.ToString());
            return this.numberDayDao.GetAllMasterDataForPopup(whereStr.ToString());
            //S_NUMBER_DAY entity = new S_NUMBER_DAY();
            //entity.NUMBERED_DAY = numberedDay.Date;
            //entity.DENSHU_KBN_CD = denshuKbnCd;
            //entity.KYOTEN_CD = kyotenCd;
            //return this.numberDayDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 日連番を登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertNumberDay(S_NUMBER_DAY entity)
        {
            return this.numberDayDao.Insert(entity);
        }

        /// <summary>
        /// 日連番を更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateNumberDay(S_NUMBER_DAY entity)
        {
            return this.numberDayDao.Update(entity);
        }

        /// <summary>
        /// 売上／支払入力用のSYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemIdForUrsh()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = DENSHU_KBN_CD_URIAGESHIHARAI;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = DENSHU_KBN_CD_URIAGESHIHARAI;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 売上／支払番号採番処理
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createUrshNumber()
        {
            SqlInt64 returnInt = -1;

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = DENSHU_KBN_CD_URIAGESHIHARAI;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = DENSHU_KBN_CD_URIAGESHIHARAI;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberDenshuDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberDenshuDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 定期実績用のSYSTEM_ID採番処理
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemIdForTj()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = DENSHU_KBN_CD_TEIKIJISSEKI;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = DENSHU_KBN_CD_TEIKIJISSEKI;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 定期実績番号採番処理
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createTjNumber()
        {
            SqlInt64 returnInt = -1;

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = DENSHU_KBN_CD_TEIKIJISSEKI;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = DENSHU_KBN_CD_TEIKIJISSEKI;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberDenshuDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberDenshuDao.Update(updateEntity);
            }

            return returnInt;
        }

		/// <summary>
		/// 受付伝票更新処理
		/// </summary>
		/// <param name="intUketsukeNum">更新対象の受付番号</param>
		/// <remarks>受付番号に該当する受付伝票の配車状況を「売上」にする</remarks>
		internal void UpdateUketsukeDenpyo(Int64 intUketsukeNum)
		{
			LogUtility.DebugMethodStart();

			// 収集受付Dao生成
			var UketsukeEntryDao = DaoInitUtility.GetComponent<T_UKETSUKE_SS_ENTRYDao>();
			var UketsukeDetailDao = DaoInitUtility.GetComponent<T_UKETSUKE_SS_DETAILDao>();
            var ContenaDao = DaoInitUtility.GetComponent<T_CONTENA_RESERVEDao>();

			// 受付番号に該当する収集受付伝票取得
			UketsukeSsDTOClass findDTO = new UketsukeSsDTOClass();
			findDTO.UKETSUKE_NUMBER = intUketsukeNum;
			DataTable table = this.torikomiDAO.GetUketsukeSsEntryForEntity(findDTO);
			// ※SYSTEM_ID, SEQの最大値の伝票を取得するため、データは唯一となる

			if(table.Rows.Count != 0)
			{
				// DataTableからEntityを生成
				T_UKETSUKE_SS_ENTRY[] entryResult = EntityUtility.DataTableToEntity<T_UKETSUKE_SS_ENTRY>(table);
				entryResult[0].TIME_STAMP = (byte[])table.Rows[0]["TIME_STAMP"];

				// 受付番号に該当する収集受付明細取得
				findDTO.SYSTEM_ID = Int64.Parse(entryResult[0].SYSTEM_ID.ToString());
				findDTO.SEQ = Int32.Parse(entryResult[0].SEQ.ToString());
				table = this.torikomiDAO.GetUketsukeSsDetailForEntity(findDTO);
				T_UKETSUKE_SS_DETAIL[] detailResult = EntityUtility.DataTableToEntity<T_UKETSUKE_SS_DETAIL>(table);

				// 受付番号に該当する収集受付伝票を論理削除
                entryResult[0].DELETE_FLG = true;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //entryResult[0].UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                entryResult[0].UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
				entryResult[0].UPDATE_USER = SystemProperty.UserName;
				entryResult[0].UPDATE_PC = SystemInformation.ComputerName;
				UketsukeEntryDao.Update(entryResult[0]);

				// 配車状況を「売上」とし伝票を新規登録
                entryResult[0].DELETE_FLG = false;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //entryResult[0].UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                entryResult[0].UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
				entryResult[0].SEQ += 1;
				entryResult[0].HAISHA_JOKYO_CD = 3;
				entryResult[0].HAISHA_JOKYO_NAME = "計上";
				UketsukeEntryDao.Insert(entryResult[0]);

				// 収集受付伝票が新規登録されたため明細を紐付け直す
				foreach(T_UKETSUKE_SS_DETAIL entity in detailResult)
				{
					entity.SEQ = entryResult[0].SEQ;
					UketsukeDetailDao.Insert(entity);
				}

                /**
                 * Ver 1.15暫定対策
                 * モバイル取込(コンテナ)の仕様が決定していないため、
                 * 収集受付データからコンテナ情報が消えてしまうのを防ぐ
                 */
                // コンテナ情報も付け直す
                var contenaSearchCondition = new Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku.DTOClass();
                contenaSearchCondition.SystemID = (long)entryResult[0].SYSTEM_ID;
                contenaSearchCondition.SEQ = (int)entryResult[0].SEQ - 1;
                var contenaInfo = ContenaDao.GetDataForEntity(contenaSearchCondition);
                if (contenaInfo != null)
                {
                    foreach(var contenaData in contenaInfo)
                    {
                        // 論理削除
                        contenaData.DELETE_FLG = true;
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //contenaData.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                        contenaData.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        contenaData.UPDATE_USER = SystemProperty.UserName;
                        contenaData.UPDATE_PC = SystemInformation.ComputerName;
                        ContenaDao.Update(contenaData);

                        // データの付け直し
                        contenaData.CALC_DAISUU_FLG = true;
                        contenaData.DELETE_FLG = false;
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //contenaData.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                        contenaData.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        contenaData.SEQ = (int)entryResult[0].SEQ;
                        ContenaDao.Insert(contenaData);

                        // コンテナ入力マスタの更新
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        DateTime sagyouDate = this.getDBDateTime();
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        if (DateTime.TryParse(entryResult[0].SAGYOU_DATE, out sagyouDate))
                        {
                            this.UpdateContenaMaster(contenaData, sagyouDate);
                        }
                    }
                }

			}

			LogUtility.DebugMethodEnd();
		}

        /// <summary>
        /// コンテナ入力マスタの更新(Ver 1.15暫定対策用)
        /// </summary>
        /// <param name="contenaReserve">更新対象のコンテナ情報</param>
        /// <pparam name="sagyouDate">T_UKETSUKE_SS_ENTRY.SAGYOU_DATE</pparam>
        private void UpdateContenaMaster(T_CONTENA_RESERVE contenaReserve, DateTime sagyouDate)
        {
            var ContenaMasterDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CONTENADao>();

            M_CONTENA conditionValue = new M_CONTENA();
            conditionValue.CONTENA_SHURUI_CD = contenaReserve.CONTENA_SHURUI_CD;
            conditionValue.CONTENA_CD = contenaReserve.CONTENA_CD;
            var contenaMaster = ContenaMasterDao.GetDataByCd(conditionValue);

            if (contenaMaster != null)
            {
                // 画面上削除済み、適用期間外のデータは選択できなはずだが念のためチェック
                if (contenaMaster.DELETE_FLG)
                {
                    return;
                }

                // 設置日、引揚日をチェック
                if ((!contenaMaster.SECCHI_DATE.IsNull
                    && contenaMaster.SECCHI_DATE.Value.Date > sagyouDate.Date)
                    || (!contenaMaster.HIKIAGE_DATE.IsNull
                    && contenaMaster.HIKIAGE_DATE.Value.Date > sagyouDate.Date))
                {
                    // 設置日、引揚日が作業日より新しい場合は何もしない。
                    return;
                }

                // コンテナマスタ更新
                contenaMaster.SHARYOU_CD = string.Empty;
                if (contenaReserve.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                {
                    // 設置の場合
                    if (sagyouDate != null)
                    {
                        contenaMaster.SECCHI_DATE = sagyouDate;
                        contenaMaster.HIKIAGE_DATE = SqlDateTime.Null;
                    }
                    contenaMaster.JOUKYOU_KBN = 2;
                }
                else if (contenaReserve.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                {
                    // 引揚の場合
                    contenaMaster.HIKIAGE_DATE = sagyouDate;
                    contenaMaster.JOUKYOU_KBN = 3;
                }
                // 自動設定項目
                string createUser = contenaMaster.CREATE_USER;
                SqlDateTime createDate = contenaMaster.CREATE_DATE;
                string createPC = contenaMaster.CREATE_PC;
                var dataBinderUkeireEntry = new DataBinderLogic<M_CONTENA>(contenaMaster);
                dataBinderUkeireEntry.SetSystemProperty(contenaMaster, false);
                // Create情報は前の状態を引き継ぐ
                contenaMaster.CREATE_USER = createUser;
                contenaMaster.CREATE_DATE = createDate;
                contenaMaster.CREATE_PC = createPC;

                ContenaMasterDao.Update(contenaMaster);
            }
        }

		/// <summary>
		/// 単価取得処理
		/// </summary>
		/// <param name="kbnCD">伝票区分CD</param>
		/// <param name="entryEntity">Entry伝票</param>
		/// <param name="hinmeiCD">品名CD</param>
		/// <param name="unitCD">単位CD</param>
		/// <returns name="decimal">単価</returns>
		/// <remarks>対象項目を直接指定し単価を取得する。個別品名単価⇒基本品名単価の順で取得する</remarks>
		internal decimal GetTanka(Int16 kbnCD, T_UR_SH_ENTRY entryEntity, string hinmeiCD, Int16 unitCD)
		{
			var CommonDBAccessor = new Common.BusinessCommon.DBAccessor();
			decimal tanka = 0;

			// 個別品名単価から取得
            var kobetsuEntity = CommonDBAccessor.GetKobetsuhinmeiTanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                kbnCD,
                                                                entryEntity.TORIHIKISAKI_CD,
                                                                entryEntity.GYOUSHA_CD,
                                                                entryEntity.GENBA_CD,
                                                                entryEntity.UNPAN_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GENBA_CD,
                                                                hinmeiCD,
                                                                unitCD);
            if(kobetsuEntity != null)
			{
				// 単価をセット
				if(false == string.IsNullOrEmpty(kobetsuEntity.TANKA.ToString()))
				{
					tanka = decimal.Parse(kobetsuEntity.TANKA.ToString());
				}
			}
			else
			{
				// 基本品名単価から取得
                var kihonEntity = CommonDBAccessor.GetKihonHinmeitanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                kbnCD,
                                                                entryEntity.UNPAN_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GENBA_CD,
                                                                hinmeiCD,
                                                                unitCD);
                if(kihonEntity != null)
				{
					// 単価をセット
					if(false == string.IsNullOrEmpty(kihonEntity.TANKA.ToString()))
					{
						tanka = decimal.Parse(kihonEntity.TANKA.ToString());
					}
				}
				else
				{
					// 登録情報がない場合は0
					tanka = 0;
				}
			}

			return tanka;
		}

		/// <summary>
		/// 単価取得処理
		/// </summary>
		/// <param name="kbnCD">伝票区分CD</param>
		/// <param name="findDTO">検索指定項目</param>
		/// <returns name="decimal">単価</returns>
		/// <remarks>検索項目に該当する単価を取得する。受付明細⇒個別品名単価⇒基本品名単価の順で取得する</remarks>
		internal decimal GetTanka(Int16 kbnCD, UketsukeSsDTOClass findDTO)
		{
			var table = new DataTable();
			var CommonDBAccessor = new Common.BusinessCommon.DBAccessor();
			decimal tanka = 0;

			// 受付明細から取得
			table = this.torikomiDAO.GetUketsukeSsDetailForEntity(findDTO);
			if(table.Rows.Count != 0)
			{
				// ヒットしたレコードを返却
				if(false == string.IsNullOrEmpty(table.Rows[0]["TANKA"].ToString()))
				{
					tanka = decimal.Parse(table.Rows[0]["TANKA"].ToString());
				}
			}
			else
			{
				// 受付番号に該当する収集受付伝票取得
				var dto = new UketsukeSsDTOClass();
				dto.UKETSUKE_NUMBER = findDTO.UKETSUKE_NUMBER;
				table = this.torikomiDAO.GetUketsukeSsEntryForEntity(dto);
				// ※SYSTEM_ID, SEQの最大値の伝票を取得するため、データは唯一となる

				if(table.Rows.Count != 0)
				{
					// DataTableからEntityを生成
					T_UKETSUKE_SS_ENTRY[] entryResult = EntityUtility.DataTableToEntity<T_UKETSUKE_SS_ENTRY>(table);

					// 個別品名単価から取得
					var kobetsuEntity = CommonDBAccessor.GetKobetsuhinmeiTanka(kbnCD,
																		entryResult[0].TORIHIKISAKI_CD,
																		entryResult[0].GYOUSHA_CD,
																		entryResult[0].GENBA_CD,
																		entryResult[0].UNPAN_GYOUSHA_CD,
																		entryResult[0].NIOROSHI_GYOUSHA_CD,
																		entryResult[0].NIOROSHI_GENBA_CD,
																		findDTO.HINMEI_CD,
																		findDTO.UNIT_CD);
					if(kobetsuEntity != null)
					{
						// 単価をセット
						if(false == string.IsNullOrEmpty(kobetsuEntity.TANKA.ToString()))
						{
							tanka = decimal.Parse(kobetsuEntity.TANKA.ToString());
						}
					}
					else
					{
						// 基本品名単価から取得
						var kihonEntity = CommonDBAccessor.GetKihonHinmeitanka(kbnCD,
																		entryResult[0].UNPAN_GYOUSHA_CD,
																		entryResult[0].NIOROSHI_GYOUSHA_CD,
																		entryResult[0].NIOROSHI_GENBA_CD,
																		findDTO.HINMEI_CD,
																		findDTO.UNIT_CD);
						if(kihonEntity != null)
						{
							// 単価をセット
							if(false == string.IsNullOrEmpty(kihonEntity.TANKA.ToString()))
							{
								tanka = decimal.Parse(kihonEntity.TANKA.ToString());
							}
						}
						else
						{
							// 登録情報がない場合は0
							tanka = 0;
						}
					}
				}
				else
				{
					// 登録情報がない場合は0
					tanka = 0;
				}
			}

			return tanka;
		}

		/// <summary>
		/// 受付収集伝票取得処理
		/// </summary>
		/// <param name="denpyouNo">受付伝票番号</param>
		/// <returns name="T_UKETSUKE_SS_ENTRY">受付伝票Entity</returns>
		/// <remarks>
		/// 受付番号に紐付く受付収集伝票を取得する
		/// 該当データが存在しない場合はnullを返却
		/// </remarks>
		internal T_UKETSUKE_SS_ENTRY getUketsukeSsEntry(int denpyouNo)
		{
			T_UKETSUKE_SS_ENTRY entryResult;
	
			// 受付番号に紐付く受付収集伝票を取得
			var dto = new UketsukeSsDTOClass();
			dto.UKETSUKE_NUMBER = denpyouNo;
			var table = this.torikomiDAO.GetUketsukeSsEntryForEntity(dto);

			if(table.Rows.Count != 0)
			{
				// DataTableからEntityを生成
				var entitys = EntityUtility.DataTableToEntity<T_UKETSUKE_SS_ENTRY>(table);
				entryResult = entitys[0];
			}
			else
			{
				entryResult = null;
			}

			return entryResult;
		}

		/// <summary>
		/// 受付収集明細取得処理
		/// </summary>
		/// <param name="findDTO">検索指定項目</param>
		/// <returns name="T_UKETSUKE_SS_DETAIL">受付明細Entity</returns>
		/// <remarks>
		/// 検索条件に該当する受付収集伝票を取得する
		/// 該当データが存在しない場合はnullを返却
		/// </remarks>
		internal T_UKETSUKE_SS_DETAIL getUketsukeSsDetail(UketsukeSsDTOClass findDTO)
		{
			T_UKETSUKE_SS_DETAIL entryResult;

			// 検索条件に該当する受付収集明細を取得
			var table = this.torikomiDAO.GetUketsukeSsDetailForEntity(findDTO);

			if(table.Rows.Count != 0)
			{
				// DataTableからEntityを生成
				var entitys = EntityUtility.DataTableToEntity<T_UKETSUKE_SS_DETAIL>(table);
				entryResult = entitys[0];
			}
			else
			{
				entryResult = null;
			}

			return entryResult;
		}

        /// <summary>
        /// 消費税率を取得
        /// </summary>
        /// <param name="denpyouDate">伝票日付</param>
		/// <returns name="decimal">消費税率</returns>
		/// <remarks>
		/// 伝票日付の期間に該当する消費税率を取得
		/// </remarks>
		public decimal GetShouhizeiRate(DateTime denpyouDate)
		{
			// 伝票日付期間の消費税率を取得
			decimal rate = 0;
			M_SHOUHIZEI[] entityList = this.shouhizeiDAO.GetAllData();
			foreach(M_SHOUHIZEI entity in entityList)
			{
				if((entity.TEKIYOU_BEGIN.IsNull == false) && (entity.TEKIYOU_END.IsNull == false))
				{
					// 適用開始～適用終了が存在する場合
					if((entity.TEKIYOU_BEGIN.Value.Date <= denpyouDate.Date) && (entity.TEKIYOU_END.Value.Date >= denpyouDate.Date))
					{
						// 消費税率を取得
						rate = entity.SHOUHIZEI_RATE.Value;
					}
				}
				else if(entity.TEKIYOU_BEGIN.IsNull == false)
				{
					// 適用開始以降が有効となっている場合
					if(entity.TEKIYOU_BEGIN.Value.Date <= denpyouDate.Date)
					{
						// 消費税率を取得
						rate = entity.SHOUHIZEI_RATE.Value;
					}
				}
				else if(entity.TEKIYOU_END.IsNull == false)
				{
					// 適用終了以前が有効となっている場合
					if(entity.TEKIYOU_END.Value.Date >= denpyouDate.Date)
					{
						// 消費税率を取得
						rate = entity.SHOUHIZEI_RATE.Value;
					}
				}
			}

			return rate;
		}

		/// <summary>
		/// 定期配車入力伝票取得処理
		/// </summary>
		/// <param name="haishaNum">配車伝票番号</param>
		/// <returns name="T_TEIKI_HAISHA_ENTRY">定期配車入力伝票Entity</returns>
		/// <remarks>
		/// 配車伝票番号に紐付く定期配車入力伝票を取得する
		/// 該当データが存在しない場合はnullを返却
		/// </remarks>
		internal T_TEIKI_HAISHA_ENTRY GetTeikiHaishaEntry(Int64 haishaNum)
		{
			T_TEIKI_HAISHA_ENTRY entryResult;

			// 配車伝票番号に紐付く定期配車入力伝票を取得
			var table = this.torikomiDAO.GetTeikiHaishaEntry(haishaNum);
			if(table.Rows.Count != 0)
			{
				// DataTableからEntityを生成
				var entitys = EntityUtility.DataTableToEntity<T_TEIKI_HAISHA_ENTRY>(table);
				entryResult = entitys[0];
			}
			else
			{
				entryResult = null;
			}

			return entryResult;
		}

        /// <summary>
        /// 定期配車番号が既に使用されているかのチェック
        /// </summary>
        /// <param name="row">編集行</param>
        /// <returns name="bool">TRUE:使用済み、FALSE:未使用</returns>
        internal bool isTeikiHaishaNumUsed(DataGridViewRow row)
        {
            bool ret = false;

            // 編集行より定期配車番号を取得
            var teikiHaishaNum = row.Cells["HAISHA_DENPYOU_NO"].Value.ToString();

            // 定期配車番号を基に、定期実績より検索
            var findDTO = new TeikiHaisyaJisekiNyuuryoku.DTOClass();
            findDTO.TeikiHaishaNumber = teikiHaishaNum;
            var table = this.teikiJissekiEntryDAO.ExistHaishaNumber(findDTO);

            if(table.Rows.Count > 0)
            {
                // 該当実績が存在する
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 定期における品名詳細を取得する
        /// </summary>
        /// <param name="dto">検索条件</param>
        /// <returns name='M_GENBA_TEIKI_HINMEI'>定期品名詳細情報</returns>
        /// <remarks>
        /// 定期配車 > 現場定期品名 > 品名マスタ の順で参照する
        /// </remarks>
        internal M_GENBA_TEIKI_HINMEI getTeikiHinmeiInfo(MobileShougunTorikomiDTOClass dto)
        {
            var retEntity = new M_GENBA_TEIKI_HINMEI();
            bool anbunGetFlag = false;

            // 検索条件に紐付く、定期配車情報を取得
            DataTable table = this.torikomiDAO.GetTeikiHinmeiInfo(dto);
            if((table != null) && (table.Rows.Count > 0))
            {
                // 伝票区分
                var denpyouKbnCD = table.Rows[0]["DENPYOU_KBN_CD"].ToString();
                if(false == string.IsNullOrEmpty(denpyouKbnCD))
                {
                    retEntity.DENPYOU_KBN_CD = Int16.Parse(denpyouKbnCD);
                }

                // 契約区分
                var keiyakuKbn = table.Rows[0]["KEIYAKU_KBN"].ToString();
                if(false == string.IsNullOrEmpty(keiyakuKbn))
                {
                    retEntity.KEIYAKU_KBN = Int16.Parse(keiyakuKbn);

                }

                // 契約区分が単価の場合のみ計上区分をセットする
                if(retEntity.KEIYAKU_KBN == CommonConst.KEIYAKU_KBN_TANKA)
                {
                    // 計上区分(月極区分)
                    var keijyouKbn = table.Rows[0]["KEIJYOU_KBN"].ToString();
                    if(false == string.IsNullOrEmpty(keijyouKbn))
                    {
                        retEntity.KEIJYOU_KBN = Int16.Parse(keijyouKbn);
                    }
                }
            }
            else
            {
                // 定期配車情報が存在しなかった場合、現場定期品名から取得
                table = this.torikomiDAO.GetGenbaTeikiHinmeiDataForEntity(dto);
                if((table != null) && (table.Rows.Count > 0))
                {
                    if(table.Rows.Count == 1)
                    {
                        // 伝票区分
                        var denpyouKbnCD = table.Rows[0]["DENPYOU_KBN_CD"].ToString();
                        if(false == string.IsNullOrEmpty(denpyouKbnCD))
                        {
                            retEntity.DENPYOU_KBN_CD = Int16.Parse(denpyouKbnCD);
                        }
                    }
                    else
                    {
                        // 該当情報が複数件の場合は、1.売上をセット
                        retEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                    }

                    // 契約区分
                    var keiyakuKbn = table.Rows[0]["KEIYAKU_KBN"].ToString();
                    if(false == string.IsNullOrEmpty(keiyakuKbn))
                    {
                        retEntity.KEIYAKU_KBN = Int16.Parse(keiyakuKbn);

                    }

                    // 契約区分が単価の場合のみ計上区分をセットする
                    if(retEntity.KEIYAKU_KBN == CommonConst.KEIYAKU_KBN_TANKA)
                    {
                        // 計上区分(月極区分)
                        var keijyouKbn = table.Rows[0]["KEIJYOU_KBN"].ToString();
                        if(false == string.IsNullOrEmpty(keijyouKbn))
                        {
                            retEntity.KEIJYOU_KBN = Int16.Parse(keijyouKbn);
                        }
                    }

                    // 按分フラグ
                    anbunGetFlag = true;
                    var anbunFlag = table.Rows[0]["ANBUN_FLG"].ToString();
                    if(false == string.IsNullOrEmpty(anbunFlag))
                    {
                        retEntity.ANBUN_FLG = Boolean.Parse(anbunFlag);
                    }
                }
                else
                {
                    // 現場定期品名情報が存在しなかった場合、品名マスタから取得
                    table = this.torikomiDAO.GetHinmeiDataForEntity(dto);
                    if((table != null) && (table.Rows.Count > 0))
                    {
                        // 伝票区分
                        var denpyouKbnCD = table.Rows[0]["DENPYOU_KBN_CD"].ToString();
                        if(false == string.IsNullOrEmpty(denpyouKbnCD))
                        {
                            var cd = Int16.Parse(denpyouKbnCD);
                            if(cd == CommonConst.DENPYOU_KBN_KYOUTSUU)
                            {
                                // 9.共通の場合は、1.売上をセット
                                retEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                            }
                            else
                            {
                                retEntity.DENPYOU_KBN_CD = cd;
                            }
                        }

                        // 契約区分は単価をセット
                        retEntity.KEIYAKU_KBN = CommonConst.KEIYAKU_KBN_TANKA;

                        // 計上区分は伝票をセット
                        retEntity.KEIJYOU_KBN = CommonConst.KEIJYOU_KBN_DENPYOU;
                    }
                }
            }

            if((table == null) || (table.Rows.Count <= 0))
            {
                // 該当情報が無ければ、デフォルト値をセット

                // 伝票区分は売上をセット
                retEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;

                // 契約区分は単価をセット
                retEntity.KEIYAKU_KBN = CommonConst.KEIYAKU_KBN_TANKA;

                // 計上区分は伝票をセット
                retEntity.KEIJYOU_KBN = CommonConst.KEIJYOU_KBN_DENPYOU;
            }

            // 按分フラグは現場定期品名から取得する
            if(anbunGetFlag == false)
            {
                table = this.torikomiDAO.GetGenbaTeikiHinmeiDataForEntity(dto);
                if((table != null) && (table.Rows.Count > 0))
                {
                    var anbunFlag = table.Rows[0]["ANBUN_FLG"].ToString();
                    if(false == string.IsNullOrEmpty(anbunFlag))
                    {
                        retEntity.ANBUN_FLG = Boolean.Parse(anbunFlag);
                    }
                }
            }

            return retEntity;
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

    }
}
