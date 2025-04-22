// $Id: IT_SHUKKA_DETAILDaoTest.cs 3143 2013-10-09 02:26:33Z takeda $
using System.Collections.Generic;
using System.Data.SqlTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Container.Factory;
using Shougun.Function.ShougunCSCommon.Dao;

namespace CommonTestProject.Dao
{


    /// <summary>
    /// IT_SHUKKA_DETAILDaoTest のテスト クラスです。すべての
    /// IT_SHUKKA_DETAILDaoTest 単体テストをここに含めます
    /// </summary>
    /// <remarks>
    /// S2Daoの標準機能である下記メソッドのテストは省略
    /// ・Insert
    /// ・Update
    /// ・Delete
    /// </remarks>
    [TestClass()]
    public class IT_SHUKKA_DETAILDaoTest
    {


        private TestContext testContextInstance;

        #region 定数
        private static readonly SqlInt64 systemId = new SqlInt64(long.MaxValue - 1);

        private static readonly SqlInt32 seq = new SqlInt32(1);

        private static SqlInt64 detailSystemId = new SqlInt64(2);

        private static SqlInt64 shukkaNumber = new SqlInt64(22222);
        #endregion

        /// <summary>
        /// 現在のテストの実行についての情報および機能を
        /// 提供するテスト コンテキストを取得または設定します。
        /// </summary>
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
        /// <summary>
        /// テストクラスの初期化
        /// </summary>
        /// <param name="testContext"></param>
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
            var dao = DaoInitUtility.GetComponent<IT_SHUKKA_DETAILDao>();
            List<T_SHUKKA_DETAIL> list = CreateTestDataList();

            foreach (T_SHUKKA_DETAIL entity in list)
            {
                var result = dao.GetDataForEntity(entity);
                if (result.Length == 0)
                {
                    dao.Insert(entity);
                }
            }
        }

        /// <summary>
        /// テストクラスの最後処理
        /// </summary>
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            // テストデータ削除
            var dao = DaoInitUtility.GetComponent<IT_SHUKKA_DETAILDao>();

            List<T_SHUKKA_DETAIL> list = CreateTestDataList();

            foreach (T_SHUKKA_DETAIL entity in list)
            {
                var results = dao.GetDataForEntity(entity);
                foreach (var result in results)
                {
                    // 1件除外
                    dao.Delete(entity);
                    break;
                }
            }
        }
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

        /// <summary>
        /// テストデータリスト作成
        /// </summary>
        /// <returns>出荷明細リスト</returns>
        private static List<T_SHUKKA_DETAIL> CreateTestDataList()
        {
            List<T_SHUKKA_DETAIL> list = new List<T_SHUKKA_DETAIL>();

            list.Add(CreateTestData(long.MaxValue - 2, 1, 1, 11111));
            list.Add(CreateTestData(long.MaxValue - 2, 1, 2, 22222));
            list.Add(CreateTestData(long.MaxValue - 2, 1, 3, 33333));
            list.Add(CreateTestData(long.MaxValue - 2, 1, 4, 55555));
            list.Add(CreateTestData(long.MaxValue - 2, 2, 1, 11111));
            list.Add(CreateTestData(long.MaxValue - 2, 2, 2, 22222));
            list.Add(CreateTestData(long.MaxValue - 2, 2, 3, 33333));
            list.Add(CreateTestData(long.MaxValue - 2, 2, 4, 55555));

            list.Add(CreateTestData(long.MaxValue - 1, 1, 1, 11111));
            list.Add(CreateTestData(long.MaxValue - 1, 1, 2, 22222));
            list.Add(CreateTestData(long.MaxValue - 1, 1, 3, 33333));
            list.Add(CreateTestData(long.MaxValue - 1, 1, 4, 44444));
            list.Add(CreateTestData(long.MaxValue - 1, 2, 1, 11111));
            list.Add(CreateTestData(long.MaxValue - 1, 2, 2, 22222));
            list.Add(CreateTestData(long.MaxValue - 1, 2, 3, 33333));
            list.Add(CreateTestData(long.MaxValue - 1, 2, 4, 44444));

            return list;
        }

        /// <summary>
        /// テストデータ作成
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="shukkaNumber">出荷番号</param>
        /// <returns>出荷明細</returns>
        private static T_SHUKKA_DETAIL CreateTestData(long systemId, int seq, long detailSystemId, long shukkaNumber)
        {
            T_SHUKKA_DETAIL entity = new T_SHUKKA_DETAIL();
            entity.SYSTEM_ID = new SqlInt64(systemId);
            entity.SEQ = new SqlInt32(seq);
            entity.DETAIL_SYSTEM_ID = new SqlInt64(detailSystemId);
            entity.SHUKKA_NUMBER = new SqlInt64(shukkaNumber);

            DataBinderLogic<T_SHUKKA_DETAIL> logic = new DataBinderLogic<T_SHUKKA_DETAIL>(entity);
            logic.SetSystemProperty(entity, false);

            return entity;
        }

        /// <summary>
        /// IT_SHUKKA_DETAILDaoの生成
        /// </summary>
        /// <returns>出荷明細</returns>
        internal virtual IT_SHUKKA_DETAILDao CreateIT_SHUKKA_DETAILDao()
        {
            IT_SHUKKA_DETAILDao target = DaoInitUtility.GetComponent<IT_SHUKKA_DETAILDao>();
            return target;
        }

        /// <summary>
        /// GetDataForEntity のテスト
        /// </summary>
        [TestMethod()]
        public void GetDataForEntityTest()
        {
            IT_SHUKKA_DETAILDao target = CreateIT_SHUKKA_DETAILDao();

            // 条件指定無し
            T_SHUKKA_DETAIL data = new T_SHUKKA_DETAIL();
            T_SHUKKA_DETAIL[] actual = target.GetDataForEntity(data);

            Assert.IsTrue(actual.Length == 0);

            // SYSTEM_ID
            data = new T_SHUKKA_DETAIL();
            data.SYSTEM_ID = systemId;
            actual = target.GetDataForEntity(data);

            int count = 0;
            foreach (T_SHUKKA_DETAIL entity in actual)
            {
                if (!entity.SYSTEM_ID.Equals(systemId))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // SEQ
            data = new T_SHUKKA_DETAIL();
            data.SYSTEM_ID = systemId;
            data.SEQ = seq;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_SHUKKA_DETAIL entity in actual)
            {
                if (!entity.SEQ.Equals(seq))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // DETAIL_SYSTEM_ID
            data = new T_SHUKKA_DETAIL();
            data.SYSTEM_ID = systemId;
            data.DETAIL_SYSTEM_ID = detailSystemId;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_SHUKKA_DETAIL entity in actual)
            {
                if (!entity.DETAIL_SYSTEM_ID.Equals(detailSystemId))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // SHUKKA_NUMBER
            data = new T_SHUKKA_DETAIL();
            data.SYSTEM_ID = systemId;
            data.SHUKKA_NUMBER = shukkaNumber;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_SHUKKA_DETAIL entity in actual)
            {
                if (!entity.SHUKKA_NUMBER.Equals(shukkaNumber))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // SYSTEM_ID,SEQ,DETAIL_SYSTEM_ID,SHUKKA_NUMBER
            data = new T_SHUKKA_DETAIL();
            data.SYSTEM_ID = systemId;
            data.SEQ = seq;
            data.DETAIL_SYSTEM_ID = detailSystemId;
            data.SHUKKA_NUMBER = shukkaNumber;
            actual = target.GetDataForEntity(data);

            foreach (T_SHUKKA_DETAIL entity in actual)
            {
                if (!entity.SYSTEM_ID.Equals(systemId)
                    || !entity.SEQ.Equals(seq)
                    || !entity.DETAIL_SYSTEM_ID.Equals(detailSystemId)
                    || !entity.SHUKKA_NUMBER.Equals(shukkaNumber))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
            }
            Assert.IsTrue(actual.Length == 1);
        }
    }
}
