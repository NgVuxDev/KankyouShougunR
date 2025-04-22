// $Id: IT_SHUKKA_ENTRYDaoTest.cs 3143 2013-10-09 02:26:33Z takeda $
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
    /// IT_SHUKKA_ENTRYDaoTest のテスト クラスです。すべての
    /// IT_SHUKKA_ENTRYDaoTest 単体テストをここに含めます
    /// </summary>
    /// <remarks>
    /// S2Daoの標準機能である下記メソッドのテストは省略
    /// ・Insert
    /// ・Update
    /// ・Delete
    /// </remarks>
    [TestClass()]
    public class IT_SHUKKA_ENTRYDaoTest
    {


        private TestContext testContextInstance;

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
            var dao = DaoInitUtility.GetComponent<IT_SHUKKA_ENTRYDao>();
            List<T_SHUKKA_ENTRY> list = CreateTestDataList();

            foreach (T_SHUKKA_ENTRY entity in list)
            {
                try
                {
                    dao.Insert(entity);
                }
                catch
                {
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
            var dao = DaoInitUtility.GetComponent<IT_SHUKKA_ENTRYDao>();

            List<T_SHUKKA_ENTRY> list = CreateTestDataList();
            foreach (T_SHUKKA_ENTRY entity in list)
            {
                try
                {
                    dao.Delete(entity);
                }
                catch
                {
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
        /// <returns>出荷入力リスト</returns>
        private static List<T_SHUKKA_ENTRY> CreateTestDataList()
        {
            List<T_SHUKKA_ENTRY> list = new List<T_SHUKKA_ENTRY>();

            list.Add(CreateTestData(long.MaxValue - 2, 1111, 11111, true));
            list.Add(CreateTestData(long.MaxValue - 2, 2222, 22222, true));
            list.Add(CreateTestData(long.MaxValue - 2, 3333, 33333, true));
            list.Add(CreateTestData(long.MaxValue - 2, 4444, 44444, true));

            list.Add(CreateTestData(long.MaxValue - 1, 1111, 11111, false));
            list.Add(CreateTestData(long.MaxValue - 1, 2222, 22222, false));
            list.Add(CreateTestData(long.MaxValue - 1, 3333, 33333, false));
            list.Add(CreateTestData(long.MaxValue - 1, 4444, 44444, true));

            return list;
        }

        /// <summary>
        /// テストデータ作成
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="shukkaNumber">出荷番号</param>
        /// <param name="deleteFlg">削除フラグ</param>
        /// <returns></returns>
        private static T_SHUKKA_ENTRY CreateTestData(long systemId, int seq, long shukkaNumber, bool deleteFlg)
        {
            T_SHUKKA_ENTRY entity = new T_SHUKKA_ENTRY();

            entity.SYSTEM_ID = systemId;
            entity.SEQ = seq;
            entity.SHUKKA_NUMBER = shukkaNumber;
            entity.DELETE_FLG = deleteFlg;

            DataBinderLogic<T_SHUKKA_ENTRY> logic = new DataBinderLogic<T_SHUKKA_ENTRY>(entity);
            logic.SetSystemProperty(entity, false);

            return entity;
        }

        /// <summary>
        /// IT_SHUKKA_ENTRYDaoの生成
        /// </summary>
        /// <returns></returns>
        internal virtual IT_SHUKKA_ENTRYDao CreateIT_SHUKKA_ENTRYDao()
        {
            IT_SHUKKA_ENTRYDao target = DaoInitUtility.GetComponent<IT_SHUKKA_ENTRYDao>();
            return target;
        }

        /// <summary>
        /// GetDataForEntity のテスト
        /// </summary>
        [TestMethod()]
        public void GetDataForEntityTest()
        {
            IT_SHUKKA_ENTRYDao target = CreateIT_SHUKKA_ENTRYDao();
            r_framework.Entity.T_SHUKKA_ENTRY data = new T_SHUKKA_ENTRY();

            // SYSTEM_ID
            SqlInt64 systemId = long.MaxValue - 1;
            data.SYSTEM_ID = systemId;
            T_SHUKKA_ENTRY[] actual = target.GetDataForEntity(data);

            int count = 0;
            foreach (T_SHUKKA_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.SYSTEM_ID.Equals(systemId))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1}", entity.SYSTEM_ID, entity.SEQ));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // SEQ
            data = new T_SHUKKA_ENTRY();
            SqlInt32 seq = 1111;
            data.SEQ = seq;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_SHUKKA_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.SEQ.Equals(seq))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1}", entity.SYSTEM_ID, entity.SEQ));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // SHUKKA_NUMBER
            data = new T_SHUKKA_ENTRY();
            SqlInt64 shukkaNumber = 11111;
            data.SHUKKA_NUMBER = shukkaNumber;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_SHUKKA_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.SHUKKA_NUMBER.Equals(shukkaNumber))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1}", entity.SYSTEM_ID, entity.SEQ));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // SYSTEM_ID,SEQ,SHUKKA_NUMBER
            data = new T_SHUKKA_ENTRY();
            data.SYSTEM_ID = systemId;
            data.SEQ = seq;
            data.SHUKKA_NUMBER = shukkaNumber;
            actual = target.GetDataForEntity(data);

            foreach (T_SHUKKA_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.SYSTEM_ID.Equals(systemId)
                    || !entity.SEQ.Equals(seq)
                    || !entity.SHUKKA_NUMBER.Equals(shukkaNumber))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1}", entity.SYSTEM_ID, entity.SEQ));
                }
            }
            Assert.IsTrue(actual.Length == 1);
        }
    }
}
