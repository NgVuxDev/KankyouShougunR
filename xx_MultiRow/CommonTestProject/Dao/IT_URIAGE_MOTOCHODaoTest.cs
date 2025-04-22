using Shougun.Function.ShougunCSCommon.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Collections.Generic;
using r_framework.Entity;
using r_framework.Logic;
using Seasar.Framework.Container.Factory;
using r_framework.Utility;

namespace CommonTestProject.Dao
{
    /// <summary>
    ///IT_URIAGE_MOTOCHODaoTest のテスト クラスです。すべての
    ///IT_URIAGE_MOTOCHODaoTest 単体テストをここに含めます
    ///</summary>
	[TestClass()]
	public class IT_URIAGE_MOTOCHODaoTest
	{
		private TestContext testContextInstance;

		/// <summary>
		///現在のテストの実行についての情報および機能を
		///提供するテスト コンテキストを取得または設定します。
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region 追加のテスト属性
		// 
		//テストを作成するときに、次の追加属性を使用することができます:
		//
		//クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
		[ClassInitialize()]
		public static void MyClassInitialize(TestContext testContext)
		{
            // テストプロジェクトに追加したDiconファイルから読み込み、こちらなら取得できる
            // アプリケーション構成ファイルに定義されたDiconファイルのパスを取得
            var diconPath = SingletonS2ContainerFactory.ConfigPath;
            // S2Containerを初期化
            var container = S2ContainerFactory.Create(diconPath);
            SingletonS2ContainerFactory.Container = container;

            // テストデータを作成
			var ukeireEntryDao = DaoInitUtility.GetComponent<IT_UKEIRE_ENTRYDao>();
			var ukeireEntryEntity = CreateTestData_UkeireEntry();
            try
            {
				ukeireEntryDao.Insert(ukeireEntryEntity);
            }
            catch (Exception)
            {
				ukeireEntryDao.Update(ukeireEntryEntity);
			}
			var ukeireDetailDao = DaoInitUtility.GetComponent<IT_UKEIRE_DETAILDao>();
			var ukeireDetailEntity = CreateTestData_UkeireMeisai();
			try
			{
				ukeireDetailDao.Insert(ukeireDetailEntity);
			}
			catch(Exception)
			{
				ukeireDetailDao.Update(ukeireDetailEntity);
			}
		}
		//
		//クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//各テストを実行する前にコードを実行するには、TestInitialize を使用
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//各テストを実行した後にコードを実行するには、TestCleanup を使用
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion

		private static readonly SqlInt64 SYSTEM_ID = 9000;
		private static readonly SqlInt32 SEQ = 9001;
		private static readonly SqlInt64 DETAIL_SYSTEM_ID = 9002;
		private static readonly string TORIHIKISAKI_CD = "000001";

		/// <summary>
		/// テストデータ作成(受入入力)
		/// </summary>
		/// <returns>T_UKEIRE_ENTRY</returns>
		private static T_UKEIRE_ENTRY CreateTestData_UkeireEntry()
		{
			// テストデータ格納
			var entity = new T_UKEIRE_ENTRY();
			entity.SYSTEM_ID = SYSTEM_ID;
			entity.SEQ = SEQ;
			entity.URIAGE_DATE = new SqlDateTime(2013, 9, 27, 0, 0, 0);
			entity.URIAGE_TORIHIKI_KBN_CD = 1;
			entity.UKEIRE_NUMBER = 9003;
			entity.GYOUSHA_CD = "000001";
			entity.GENBA_CD = "000001";
			entity.RECEIPT_NUMBER = 9004;
			entity.TORIHIKISAKI_CD = TORIHIKISAKI_CD;
			entity.URIAGE_ZEI_KEISAN_KBN_CD = 1;
			entity.URIAGE_TAX_SOTO = 4;
			entity.URIAGE_TAX_UCHI = 1;

			// 固定設定
			DataBinderLogic<T_UKEIRE_ENTRY> logic = new DataBinderLogic<T_UKEIRE_ENTRY>(entity);
			logic.SetSystemProperty(entity, false);
			entity.DELETE_FLG = new SqlBoolean(false);

			return entity;
		}

		/// <summary>
		/// テストデータ作成(受入明細)
		/// </summary>
		/// <returns>T_UKEIRE_DETAIL</returns>
		private static T_UKEIRE_DETAIL CreateTestData_UkeireMeisai()
		{
			// テストデータ格納
			var entity = new T_UKEIRE_DETAIL();
			entity.SYSTEM_ID = SYSTEM_ID;
			entity.SEQ = SEQ;
			entity.DETAIL_SYSTEM_ID = SEQ;
			entity.DENPYOU_KBN_CD = 1;
			entity.HINMEI_CD = "000001";
			entity.SUURYOU = 1234;
			entity.UNIT_CD = 1;
			entity.TANKA = 2345;
			entity.KINGAKU = 3456;
			entity.TAX_SOTO = 2;
			entity.TAX_UCHI = 1;
			entity.HINMEI_TAX_SOTO = 2;
			entity.HINMEI_TAX_UCHI = 1;
			entity.MEISAI_BIKOU = "テスト備考";
	
			// 固定設定
			DataBinderLogic<T_UKEIRE_DETAIL> logic = new DataBinderLogic<T_UKEIRE_DETAIL>(entity);
			logic.SetSystemProperty(entity, false);
			return entity;
		}

		/// <summary>
		/// IT_URIAGE_MOTOCHODaoの生成
		/// </summary>
		internal virtual IT_URIAGE_MOTOCHODao CreateIT_URIAGE_MOTOCHODao()
		{
			IT_URIAGE_MOTOCHODao target = DaoInitUtility.GetComponent<IT_URIAGE_MOTOCHODao>();
			return target;
		}

		/// <summary>
		///GetIchiranData のテスト
		///</summary>
		[TestMethod()]
		public void GetIchiranDataTest()
		{
			IT_URIAGE_MOTOCHODao target = CreateIT_URIAGE_MOTOCHODao();

			// 比較用データの取得
			var ukeireEntryDao = DaoInitUtility.GetComponent<IT_UKEIRE_ENTRYDao>();
			var ukeireEntryEntity = CreateTestData_UkeireEntry();
			var ukeireDetailDao = DaoInitUtility.GetComponent<IT_UKEIRE_DETAILDao>();
			var ukeireDetailEntity = CreateTestData_UkeireMeisai();
	
			DateTime date = new DateTime(2013, 9, 27, 0, 0, 0);
			string startCD = TORIHIKISAKI_CD;
			string endCD = TORIHIKISAKI_CD;
			string startDay = date.Date.ToString();
			string endDay = date.Date.ToString();
			DataTable table = target.GetIchiranData(startCD, endCD, startDay, endDay);
			Assert.IsNotNull(table);
			Assert.AreEqual(table.Rows[0]["DENSHU_KBN"], 1);
			Assert.AreEqual(table.Rows[0]["MEISAI_DATE"], DateTime.Parse(ukeireEntryEntity.URIAGE_DATE.ToString()));
			Assert.AreEqual(table.Rows[0]["DENPYOU_NUMBER"], Int64.Parse(ukeireEntryEntity.UKEIRE_NUMBER.ToString()));
			Assert.AreEqual(table.Rows[0]["GYOUSHA_CD"], ukeireEntryEntity.GYOUSHA_CD);
			Assert.AreEqual(table.Rows[0]["GENBA_CD"], ukeireEntryEntity.GENBA_CD);
			Assert.AreEqual(table.Rows[0]["HINMEI_CD"], ukeireDetailEntity.HINMEI_CD);
			Assert.AreEqual(table.Rows[0]["SEIKYUU_NUMBER"], Int64.Parse(ukeireEntryEntity.RECEIPT_NUMBER.ToString()));
			Assert.AreEqual(table.Rows[0]["TANKA"], decimal.Parse(ukeireDetailEntity.TANKA.ToString()));
			Assert.AreEqual(table.Rows[0]["URIAGE_KINGAKU"], decimal.Parse(ukeireDetailEntity.KINGAKU.ToString()));
			Assert.AreEqual(table.Rows[0]["SHOUHIZEI"], decimal.Parse((ukeireDetailEntity.TAX_SOTO + ukeireDetailEntity.TAX_UCHI + ukeireDetailEntity.HINMEI_TAX_SOTO + ukeireDetailEntity.HINMEI_TAX_UCHI).ToString()));
			Assert.IsNotNull(table.Rows[0]["NYUUKIN_KINGAKU"]);
			Assert.IsNotNull(table.Rows[0]["SASHIHIKI_ZANDAKA"]);
			Assert.AreEqual(table.Rows[0]["MEISAI_BIKOU"], ukeireDetailEntity.MEISAI_BIKOU);
			Assert.AreEqual(table.Rows[0]["TORIHIKISAKI_CD"], ukeireEntryEntity.TORIHIKISAKI_CD);
			Assert.AreEqual(table.Rows[0]["DENPYOU_MAI_ZEI"], decimal.Parse((ukeireEntryEntity.URIAGE_TAX_SOTO + ukeireEntryEntity.URIAGE_TAX_UCHI).ToString()));
			Assert.IsNotNull(table.Rows[0]["SEIKYUU_MAI_SOTO_ZEI"]);
		}

		/// <summary>
		///GetRecentSeikyuuZandaka のテスト
		///</summary>
		[TestMethod()]
		public void GetRecentSeikyuuZandakaTest()
		{
			IT_URIAGE_MOTOCHODao target = CreateIT_URIAGE_MOTOCHODao(); // TODO: 適切な値に初期化してください
			string torihikisakiCD = string.Empty; // TODO: 適切な値に初期化してください
			string startDay = string.Empty; // TODO: 適切な値に初期化してください
			DataTable expected = null; // TODO: 適切な値に初期化してください
			DataTable actual;
			actual = target.GetRecentSeikyuuZandaka(torihikisakiCD, startDay);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("このテストメソッドの正確性を確認します。");
		}

		/// <summary>
		///GetSeikyuuMaiZeiIchiran のテスト
		///</summary>
		[TestMethod()]
		public void GetSeikyuuMaiZeiIchiranTest()
		{
			IT_URIAGE_MOTOCHODao target = CreateIT_URIAGE_MOTOCHODao(); // TODO: 適切な値に初期化してください
			string startCD = string.Empty; // TODO: 適切な値に初期化してください
			string endCD = string.Empty; // TODO: 適切な値に初期化してください
			string startDay = string.Empty; // TODO: 適切な値に初期化してください
			string endDay = string.Empty; // TODO: 適切な値に初期化してください
			DataTable expected = null; // TODO: 適切な値に初期化してください
			DataTable actual;
			actual = target.GetSeikyuuMaiZeiIchiran(startCD, endCD, startDay, endDay);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("このテストメソッドの正確性を確認します。");
		}

		/// <summary>
		///GetTorihikisakiList のテスト
		///</summary>
		[TestMethod()]
		public void GetTorihikisakiListTest()
		{
			IT_URIAGE_MOTOCHODao target = CreateIT_URIAGE_MOTOCHODao(); // TODO: 適切な値に初期化してください
			string startCD = string.Empty; // TODO: 適切な値に初期化してください
			string endCD = string.Empty; // TODO: 適切な値に初期化してください
			M_TORIHIKISAKI[] expected = null; // TODO: 適切な値に初期化してください
			M_TORIHIKISAKI[] actual;
			actual = target.GetTorihikisakiList(startCD, endCD);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("このテストメソッドの正確性を確認します。");
		}
	}
}
