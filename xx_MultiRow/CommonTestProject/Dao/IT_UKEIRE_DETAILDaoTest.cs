// $Id: IT_UKEIRE_DETAILDaoTest.cs 3143 2013-10-09 02:26:33Z takeda $
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
    /// IT_UKEIRE_DETAILDaoTest のテスト クラスです。すべての
    /// IT_UKEIRE_DETAILDaoTest 単体テストをここに含めます
    /// </summary>
    /// <remarks>
    /// S2Daoの標準機能である下記メソッドのテストは省略
    /// ・Insert
    /// ・Update
    /// ・Delete
    /// </remarks>
    [TestClass()]
    public class IT_UKEIRE_DETAILDaoTest
    {


        private TestContext testContextInstance;

        #region 定数
        private static readonly SqlInt64 systemId = new SqlInt64(long.MaxValue - 1);

        private static readonly SqlInt32 seq = new SqlInt32(1);

        private static SqlInt64 detailSystemId = new SqlInt64(2);
        
        private static SqlInt64 ukeireNumber = new SqlInt64(22222);
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
            var dao = DaoInitUtility.GetComponent<IT_UKEIRE_DETAILDao>();
            List<T_UKEIRE_DETAIL> list = CreateTestDataList();

            foreach (T_UKEIRE_DETAIL entity in list)
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
            var dao = DaoInitUtility.GetComponent<IT_UKEIRE_DETAILDao>();

            List<T_UKEIRE_DETAIL> list = CreateTestDataList();

            foreach (T_UKEIRE_DETAIL entity in list)
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
        /// <returns>受入明細リスト</returns>
        private static List<T_UKEIRE_DETAIL> CreateTestDataList()
        {
            List<T_UKEIRE_DETAIL> list = new List<T_UKEIRE_DETAIL>();

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
        /// <param name="ukeireNumber">受入番号</param>
        /// <returns>受入明細</returns>
        private static T_UKEIRE_DETAIL CreateTestData(long systemId,int seq, long detailSystemId,long ukeireNumber)
        {
            T_UKEIRE_DETAIL entity = new T_UKEIRE_DETAIL();

            entity.SYSTEM_ID = systemId;
            entity.SEQ = seq;
            entity.DETAIL_SYSTEM_ID = detailSystemId;
            entity.UKEIRE_NUMBER = ukeireNumber;

            DataBinderLogic<T_UKEIRE_DETAIL> logic = new DataBinderLogic<T_UKEIRE_DETAIL>(entity);
            logic.SetSystemProperty(entity, false);

            return entity;
        }

        /// <summary>
        /// IT_UKEIRE_DETAILDaoの生成
        /// </summary>
        /// <returns></returns>
        internal virtual IT_UKEIRE_DETAILDao CreateIT_UKEIRE_DETAILDao()
        {
            IT_UKEIRE_DETAILDao target = DaoInitUtility.GetComponent<IT_UKEIRE_DETAILDao>();
            return target;
        }

        /// <summary>
        /// GetDataForEntity のテスト
        /// </summary>
        [TestMethod()]
        public void GetDataForEntityTest()
        {
            IT_UKEIRE_DETAILDao target = CreateIT_UKEIRE_DETAILDao();
            
            // 条件指定無し
            T_UKEIRE_DETAIL data = new T_UKEIRE_DETAIL();
            r_framework.Entity.T_UKEIRE_DETAIL[] actual= target.GetDataForEntity(data);

            Assert.IsTrue(actual.Length == 0);


            // SYSTEM_ID
            data = new T_UKEIRE_DETAIL();
            data.SYSTEM_ID = systemId;
            actual = target.GetDataForEntity(data);

            int count = 0;
            foreach (T_UKEIRE_DETAIL entity in actual)
            {
                if (!entity.SYSTEM_ID.Equals(systemId))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // SEQ
            data = new T_UKEIRE_DETAIL();
            data.SYSTEM_ID = systemId;
            data.SEQ = seq;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_UKEIRE_DETAIL entity in actual)
            {
                if (!entity.SEQ.Equals(seq))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // DETAIL_SYSTEM_ID
            data = new T_UKEIRE_DETAIL();
            data.SYSTEM_ID = systemId;
            data.DETAIL_SYSTEM_ID = detailSystemId;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_UKEIRE_DETAIL entity in actual)
            {
                if (!entity.DETAIL_SYSTEM_ID.Equals(detailSystemId))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // UKEIRE_NUMBER
            data = new T_UKEIRE_DETAIL();
            data.SYSTEM_ID = systemId;
            data.UKEIRE_NUMBER = ukeireNumber;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_UKEIRE_DETAIL entity in actual)
            {
                if (!entity.UKEIRE_NUMBER.Equals(ukeireNumber))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // SYSTEM_ID,SEQ,DETAIL_SYSTEM_ID,UKEIRE_NUMBER
            data = new T_UKEIRE_DETAIL();
            data.SYSTEM_ID = systemId;
            data.SEQ = seq;
            data.DETAIL_SYSTEM_ID = detailSystemId;
            data.UKEIRE_NUMBER = ukeireNumber;
            actual = target.GetDataForEntity(data);

            foreach (T_UKEIRE_DETAIL entity in actual)
            {
                if (!entity.SYSTEM_ID.Equals(systemId)
                    || !entity.SEQ.Equals(seq)
                    || !entity.DETAIL_SYSTEM_ID.Equals(detailSystemId)
                    || !entity.UKEIRE_NUMBER.Equals(ukeireNumber))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1},DETAIL_SYSTEM_ID {2}", entity.SYSTEM_ID, entity.SEQ, entity.DETAIL_SYSTEM_ID));
                }
            }
            Assert.IsTrue(actual.Length == 1);
        }
    }
}
