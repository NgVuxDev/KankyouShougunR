// $Id: IT_MANIFEST_ENTRYDaoTest.cs 3143 2013-10-09 02:26:33Z takeda $
using System;
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
    /// IT_MANIFEST_ENTRYDaoTest のテスト クラスです。すべての
    /// IT_MANIFEST_ENTRYDaoTest 単体テストをここに含めます
    /// </summary>
    /// <remarks>
    /// S2Daoの標準機能である下記メソッドのテストは省略
    /// ・Insert
    /// ・Update
    /// ・Delete
    /// 未使用(Obsolete属性)の下記メソッドもテスト除外
    /// ・GetAllMasterDataForPopup
    /// ・GetAllValidDataForPopUp
    /// ・GetDateForStringSql
    /// </remarks>
    [TestClass()]
    public class IT_MANIFEST_ENTRYDaoTest
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
            var dao = DaoInitUtility.GetComponent<IT_MANIFEST_ENTRYDao>();
            List<T_MANIFEST_ENTRY> list = CreateTestData();

            foreach (T_MANIFEST_ENTRY entity in list)
            {
                try
                {
                    dao.Insert(entity);
                }
                catch (Exception)
                {
                    dao.Update(entity);
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
            var dao = DaoInitUtility.GetComponent<IT_MANIFEST_ENTRYDao>();

            List<T_MANIFEST_ENTRY> list = CreateTestData();
            foreach (T_MANIFEST_ENTRY entity in list)
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
        /// テストデータ作成
        /// </summary>
        /// <returns>マニフェスト</returns>
        private static List<T_MANIFEST_ENTRY> CreateTestData()
        {
            List<T_MANIFEST_ENTRY> list = new List<T_MANIFEST_ENTRY>();

            {
                T_MANIFEST_ENTRY entity = new T_MANIFEST_ENTRY();
                entity.SYSTEM_ID = new SqlInt64(123456);
                entity.SEQ = new SqlInt32(234567);
                SetCommonColumn(entity);

                list.Add(entity);
            }

            {
                T_MANIFEST_ENTRY entity = new T_MANIFEST_ENTRY();
                entity.SYSTEM_ID = new SqlInt64(234567);
                entity.SEQ = new SqlInt32(345678);
                SetCommonColumn(entity);
                entity.DELETE_FLG = true;

                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// 共通項目、テスト対象項目を設定
        /// </summary>
        /// <param name="entity"></param>
        private static void SetCommonColumn(T_MANIFEST_ENTRY entity)
        {
            entity.RENKEI_DENSHU_KBN_CD = new SqlInt16(11111);
            entity.RENKEI_SYSTEM_ID = new SqlInt64(22222);
            entity.RENKEI_MEISAI_SYSTEM_ID = new SqlInt64(33333);
            entity.DELETE_FLG = false;

            DataBinderLogic<T_MANIFEST_ENTRY> logic = new DataBinderLogic<T_MANIFEST_ENTRY>(entity);
            logic.SetSystemProperty(entity, false);
        }

        /// <summary>
        /// IT_MANIFEST_ENTRYDaoの生成
        /// </summary>
        /// <returns></returns>
        internal virtual Shougun.Function.ShougunCSCommon.Dao.IT_MANIFEST_ENTRYDao CreateIT_MANIFEST_ENTRYDao()
        {
            IT_MANIFEST_ENTRYDao target = DaoInitUtility.GetComponent<IT_MANIFEST_ENTRYDao>();
            return target;
        }

        /// <summary>
        /// GetDataForEntity のテスト
        /// </summary>
        [TestMethod()]
        public void GetDataForEntityTest()
        {
            IT_MANIFEST_ENTRYDao target = CreateIT_MANIFEST_ENTRYDao();
            T_MANIFEST_ENTRY testData = new T_MANIFEST_ENTRY();
            SetCommonColumn(testData);

            // RENKEI_DENSHU_KBN_CD
            T_MANIFEST_ENTRY data = new T_MANIFEST_ENTRY();
            data.RENKEI_DENSHU_KBN_CD = testData.RENKEI_DENSHU_KBN_CD;
            T_MANIFEST_ENTRY[] actual = target.GetDataForEntity(data);

            int count = 0;
            foreach (T_MANIFEST_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.RENKEI_DENSHU_KBN_CD.Equals(testData.RENKEI_DENSHU_KBN_CD))
                {
                    Assert.Fail(string.Format("Fail Data：RENKEI_DENSHU_KBN_CD {0},RENKEI_SYSTEM_ID {1},RENKEI_MEISAI_SYSTEM_ID {2}"), entity.RENKEI_DENSHU_KBN_CD, entity.RENKEI_SYSTEM_ID, entity.RENKEI_MEISAI_SYSTEM_ID);
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // RENKEI_SYSTEM_ID
            data = new T_MANIFEST_ENTRY();
            data.RENKEI_SYSTEM_ID = testData.RENKEI_SYSTEM_ID;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_MANIFEST_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.RENKEI_SYSTEM_ID.Equals(testData.RENKEI_SYSTEM_ID))
                {
                    Assert.Fail(string.Format("Fail Data：RENKEI_DENSHU_KBN_CD {0},RENKEI_SYSTEM_ID {1},RENKEI_MEISAI_SYSTEM_ID {2}"), entity.RENKEI_DENSHU_KBN_CD, entity.RENKEI_SYSTEM_ID, entity.RENKEI_MEISAI_SYSTEM_ID);
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // RENKEI_MEISAI_SYSTEM_ID
            data = new T_MANIFEST_ENTRY();
            data.RENKEI_MEISAI_SYSTEM_ID = testData.RENKEI_MEISAI_SYSTEM_ID;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (T_MANIFEST_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.RENKEI_MEISAI_SYSTEM_ID.Equals(testData.RENKEI_MEISAI_SYSTEM_ID))
                {
                    Assert.Fail(string.Format("Fail Data：RENKEI_DENSHU_KBN_CD {0},RENKEI_SYSTEM_ID {1},RENKEI_MEISAI_SYSTEM_ID {2}"), entity.RENKEI_DENSHU_KBN_CD, entity.RENKEI_SYSTEM_ID, entity.RENKEI_MEISAI_SYSTEM_ID);
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // RENKEI_DENSHU_KBN_CD,RENKEI_SYSTEM_ID,RENKEI_MEISAI_SYSTEM_ID
            data = new T_MANIFEST_ENTRY();
            data.RENKEI_DENSHU_KBN_CD = testData.RENKEI_DENSHU_KBN_CD;
            data.RENKEI_SYSTEM_ID = testData.RENKEI_SYSTEM_ID;
            data.RENKEI_MEISAI_SYSTEM_ID = testData.RENKEI_MEISAI_SYSTEM_ID;
            actual = target.GetDataForEntity(data);

            foreach (T_MANIFEST_ENTRY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.RENKEI_DENSHU_KBN_CD.Equals(testData.RENKEI_DENSHU_KBN_CD)
                    || !entity.RENKEI_SYSTEM_ID.Equals(testData.RENKEI_SYSTEM_ID)
                    || !entity.RENKEI_MEISAI_SYSTEM_ID.Equals(testData.RENKEI_MEISAI_SYSTEM_ID)
                    )
                {
                    Assert.Fail(string.Format("Fail Data：RENKEI_DENSHU_KBN_CD {0},RENKEI_SYSTEM_ID {1},RENKEI_MEISAI_SYSTEM_ID {2}"), entity.RENKEI_DENSHU_KBN_CD, entity.RENKEI_SYSTEM_ID, entity.RENKEI_MEISAI_SYSTEM_ID);
                }
            }
            Assert.IsTrue(actual.Length == 1);
        }
    }
}
