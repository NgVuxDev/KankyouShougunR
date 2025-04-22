// $Id: IT_UKEIRE_ENTRYDaoTest.cs 3143 2013-10-09 02:26:33Z takeda $
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
    /// IT_UKEIRE_ENTRYDaoTest のテスト クラスです。すべての
    /// IT_UKEIRE_ENTRYDaoTest 単体テストをここに含めます
    /// </summary>
    [TestClass()]
    public class IT_UKEIRE_ENTRYDaoTest
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
            var dao = DaoInitUtility.GetComponent<IT_UKEIRE_ENTRYDao>();
            List<T_UKEIRE_ENTRY> list = CreateTestDataList();

            foreach (T_UKEIRE_ENTRY entity in list)
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
            var dao = DaoInitUtility.GetComponent<IT_UKEIRE_ENTRYDao>();

            List<T_UKEIRE_ENTRY> list = CreateTestDataList();
            foreach (T_UKEIRE_ENTRY entity in list)
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
        /// <returns>受入入力リスト</returns>
        private static List<T_UKEIRE_ENTRY> CreateTestDataList()
        {
            List<T_UKEIRE_ENTRY> list = new List<T_UKEIRE_ENTRY>();

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
        /// <param name="ukeireNumber">受入番号</param>
        /// <param name="deleteFlg">削除フラグ</param>
        /// <returns>受入入力</returns>
        private static T_UKEIRE_ENTRY CreateTestData(long systemId, int seq, long ukeireNumber, bool deleteFlg)
        {
            T_UKEIRE_ENTRY entity = new T_UKEIRE_ENTRY();

            entity.SYSTEM_ID = systemId;
            entity.SEQ = seq;
            entity.UKEIRE_NUMBER = ukeireNumber;
            entity.DELETE_FLG = deleteFlg;

            DataBinderLogic<T_UKEIRE_ENTRY> logic = new DataBinderLogic<T_UKEIRE_ENTRY>(entity);
            logic.SetSystemProperty(entity, false);

            return entity;
        }


        /// <summary>
        /// IT_UKEIRE_ENTRYDaoの生成
        /// </summary>
        /// <returns></returns>
        internal virtual IT_UKEIRE_ENTRYDao CreateIT_UKEIRE_ENTRYDao()
        {
            IT_UKEIRE_ENTRYDao target = DaoInitUtility.GetComponent<IT_UKEIRE_ENTRYDao>();
            return target;
        }

        /// <summary>
        /// GetDataForEntity のテスト
        /// </summary>
        [TestMethod()]
        public void GetDataForEntityTest()
        {
            IT_UKEIRE_ENTRYDao target = CreateIT_UKEIRE_ENTRYDao();
            r_framework.Entity.T_UKEIRE_ENTRY data = new T_UKEIRE_ENTRY();

            // SYSTEM_ID
            SqlInt64 systemId = long.MaxValue - 1;
            data.SYSTEM_ID = systemId;
            T_UKEIRE_ENTRY[] actual = target.GetDataForEntity(data);

            int count = 0;
            foreach (T_UKEIRE_ENTRY entity in actual)
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
            data = new T_UKEIRE_ENTRY();
            SqlInt32 seq = 1111;
            data.SEQ = seq;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_UKEIRE_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.SEQ.Equals(seq))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1}", entity.SYSTEM_ID, entity.SEQ));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // UKEIRE_NUMBER
            data = new T_UKEIRE_ENTRY();
            SqlInt64 ukeireNumber = 11111;
            data.UKEIRE_NUMBER = ukeireNumber;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_UKEIRE_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.UKEIRE_NUMBER.Equals(ukeireNumber))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1}", entity.SYSTEM_ID, entity.SEQ));
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // SYSTEM_ID,SEQ,UKEIRE_NUMBER
            data = new T_UKEIRE_ENTRY();
            data.SYSTEM_ID = systemId;
            data.SEQ = seq;
            data.UKEIRE_NUMBER = ukeireNumber;
            actual = target.GetDataForEntity(data);

            foreach (T_UKEIRE_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.SYSTEM_ID.Equals(systemId)
                    || !entity.SEQ.Equals(seq)
                    || !entity.UKEIRE_NUMBER.Equals(ukeireNumber))
                {
                    Assert.Fail(string.Format("Fail Data：SYSTEM_ID {0},SEQ {1}", entity.SYSTEM_ID, entity.SEQ));
                }
            }
            Assert.IsTrue(actual.Length == 1);
        }
    }
}
