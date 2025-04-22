// $Id: IS_NUMBER_DAYDaoTest.cs 3143 2013-10-09 02:26:33Z takeda $
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
    /// IS_NUMBER_DAYDaoTest のテスト クラスです。すべての
    /// IS_NUMBER_DAYDaoTest 単体テストをここに含めます
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
    public class IS_NUMBER_DAYDaoTest
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
            var fwDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DAYDao>();
            List<S_NUMBER_DAY> entitys = CreateTestDataList();
            foreach (S_NUMBER_DAY entity in entitys)
            {
                var result = fwDao.GetNumberDayData(entity);
                if (result == null)
                {
                    fwDao.Insert(entity);
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
            var fwDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DAYDao>();

            List<S_NUMBER_DAY> entitys = CreateTestDataList();
            foreach (S_NUMBER_DAY entity in entitys)
            {
                var result = fwDao.GetNumberDayData(entity);
                if (result != null)
                {
                    fwDao.Delete(entity);
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
        /// <returns>日連番リスト</returns>
        private static List<S_NUMBER_DAY> CreateTestDataList()
        {
            List<S_NUMBER_DAY> list = new List<S_NUMBER_DAY>();

            list.Add(CreateTestData(1000, 300, false));
            list.Add(CreateTestData(1000, 400, false));
            list.Add(CreateTestData(1000, 500, true));
            list.Add(CreateTestData(2000, 300, false));
            list.Add(CreateTestData(2000, 400, false));
            list.Add(CreateTestData(2000, 500, true));

            return list;
        }

        /// <summary>
        /// テストデータ作成
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <param name="deleteFlg">削除フラグ</param>
        /// <returns>日連番</returns>
        private static S_NUMBER_DAY CreateTestData(short denshuKbnCd, short kyotenCd, bool deleteFlg)
        {
            S_NUMBER_DAY entity = new S_NUMBER_DAY();
            entity.NUMBERED_DAY = SqlDateTime.Parse(DateTime.Today.ToString());
            entity.DENSHU_KBN_CD = SqlInt16.Parse(denshuKbnCd.ToString());
            entity.KYOTEN_CD = SqlInt16.Parse(kyotenCd.ToString());
            entity.CURRENT_NUMBER = 0;
            entity.DELETE_FLG = deleteFlg;

            DataBinderLogic<S_NUMBER_DAY> logic = new DataBinderLogic<S_NUMBER_DAY>(entity);
            logic.SetSystemProperty(entity, deleteFlg);

            return entity;
        }

        /// <summary>
        /// IS_NUMBER_DAYDaoの生成
        /// </summary>
        /// <returns></returns>
        internal virtual IS_NUMBER_DAYDao CreateIS_NUMBER_DAYDao()
        {
            IS_NUMBER_DAYDao target = DaoInitUtility.GetComponent<IS_NUMBER_DAYDao>();
            return target;
        }

        /// <summary>
        /// GetDataForEntity のテスト
        /// </summary>
        [TestMethod()]
        public void GetDataForEntityTest()
        {
            Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao target = CreateIS_NUMBER_DAYDao();

            // NUMBERED_DAY
            S_NUMBER_DAY data = new S_NUMBER_DAY();
            SqlDateTime numberdeDay = SqlDateTime.Parse(DateTime.Today.ToString());
            data.NUMBERED_DAY = numberdeDay;
            S_NUMBER_DAY[] actual = target.GetDataForEntity(data);

            int count = 0;
            foreach (S_NUMBER_DAY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.NUMBERED_DAY.Equals(numberdeDay))
                {
                    Assert.Fail(string.Format("Fail Data：NUMBERED_DAY {0},DENSHU_KBN_CD {1},KYOTEN_CD {2}"), entity.NUMBERED_DAY, entity.DENSHU_KBN_CD, entity.KYOTEN_CD);
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // DENSHU_KBN_CD
            data = new S_NUMBER_DAY();
            SqlInt16 denshuKbnCd = SqlInt16.Parse("100");
            data.DENSHU_KBN_CD = denshuKbnCd;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (S_NUMBER_DAY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.DENSHU_KBN_CD.Equals(denshuKbnCd))
                {
                    Assert.Fail(string.Format("Fail Data：NUMBERED_DAY {0},DENSHU_KBN_CD {1},KYOTEN_CD {2}"), entity.NUMBERED_DAY, entity.DENSHU_KBN_CD, entity.KYOTEN_CD);
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // KYOTEN_CD
            data = new S_NUMBER_DAY();
            SqlInt16 kyotenCd = SqlInt16.Parse("300");
            data.KYOTEN_CD = kyotenCd;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (S_NUMBER_DAY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.KYOTEN_CD.Equals(kyotenCd))
                {
                    Assert.Fail(string.Format("Fail Data：NUMBERED_DAY {0},DENSHU_KBN_CD {1},KYOTEN_CD {2}"), entity.NUMBERED_DAY, entity.DENSHU_KBN_CD, entity.KYOTEN_CD);
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // NUMBERED_DAY,DENSHU_KBN_CD,KYOTEN_CD
            data = new S_NUMBER_DAY();
            data.NUMBERED_DAY = numberdeDay;
            data.DENSHU_KBN_CD = denshuKbnCd;
            data.KYOTEN_CD = kyotenCd;
            actual = target.GetDataForEntity(data);

            foreach (S_NUMBER_DAY entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.NUMBERED_DAY.Equals(numberdeDay)
                    || !entity.DENSHU_KBN_CD.Equals(denshuKbnCd)
                    || !entity.KYOTEN_CD.Equals(kyotenCd))
                {
                    Assert.Fail(string.Format("Fail Data：NUMBERED_DAY {0},DENSHU_KBN_CD {1},KYOTEN_CD {2}"), entity.NUMBERED_DAY, entity.DENSHU_KBN_CD, entity.KYOTEN_CD);
                }
            }
            Assert.IsTrue(actual.Length == 1);
        }
    }
}
