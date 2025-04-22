// $Id: IS_NUMBER_YEARDaoTest.cs 3143 2013-10-09 02:26:33Z takeda $
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
    /// IS_NUMBER_YEARDaoTest のテスト クラスです。すべての
    /// IS_NUMBER_YEARDaoTest 単体テストをここに含めます
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
    public class IS_NUMBER_YEARDaoTest
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
            var fwDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_YEARDao>();
            List<S_NUMBER_YEAR> entitys = CreateTestDataList();
            foreach (S_NUMBER_YEAR entity in entitys)
            {
                var result = fwDao.GetNumberYearData(entity);
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
            var fwDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_YEARDao>();

            List<S_NUMBER_YEAR> entitys = CreateTestDataList();
            foreach (S_NUMBER_YEAR entity in entitys)
            {
                var result = fwDao.GetNumberYearData(entity);
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
        /// <returns>年連番リスト</returns>
        private static List<S_NUMBER_YEAR> CreateTestDataList()
        {
            List<S_NUMBER_YEAR> list = new List<S_NUMBER_YEAR>();

            list.Add(CreateTestData(DateTime.Today.Year, 1000, 3000, false));
            list.Add(CreateTestData(DateTime.Today.Year, 1000, 4000, false));
            list.Add(CreateTestData(DateTime.Today.Year, 1000, 5000, true));
            list.Add(CreateTestData(DateTime.Today.Year, 2000, 3000, false));
            list.Add(CreateTestData(DateTime.Today.Year, 2000, 4000, false));
            list.Add(CreateTestData(DateTime.Today.Year, 2000, 5000, true));

            return list;
        }

        /// <summary>
        /// テストデータ作成
        /// </summary>
        /// <param name="numberedYear">年度</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <param name="deleteFlg">削除フラグ</param>
        /// <returns>年連番</returns>
        private static S_NUMBER_YEAR CreateTestData(int numberedYear, short denshuKbnCd, short kyotenCd, bool deleteFlg)
        {
            S_NUMBER_YEAR entity = new S_NUMBER_YEAR();
            entity.NUMBERED_YEAR = SqlInt32.Parse(numberedYear.ToString());
            entity.DENSHU_KBN_CD = SqlInt16.Parse(denshuKbnCd.ToString());
            entity.KYOTEN_CD = SqlInt16.Parse(kyotenCd.ToString());
            entity.CURRENT_NUMBER = 0;
            entity.DELETE_FLG = deleteFlg;

            DataBinderLogic<S_NUMBER_YEAR> logic = new DataBinderLogic<S_NUMBER_YEAR>(entity);
            logic.SetSystemProperty(entity, deleteFlg);

            return entity;
        }

        /// <summary>
        /// IS_NUMBER_YEARDaoの生成
        /// </summary>
        /// <returns></returns>
        internal virtual IS_NUMBER_YEARDao CreateIS_NUMBER_YEARDao()
        {
            IS_NUMBER_YEARDao target = DaoInitUtility.GetComponent<IS_NUMBER_YEARDao>();
            return target;
        }

        /// <summary>
        /// GetDataForEntity のテスト
        /// </summary>
        [TestMethod()]
        public void GetDataForEntityTest()
        {
            IS_NUMBER_YEARDao target = CreateIS_NUMBER_YEARDao();

            // NUMBERED_YEAR
            S_NUMBER_YEAR data = new S_NUMBER_YEAR();
            SqlInt32 numberdeYear = SqlInt32.Parse(DateTime.Today.Year.ToString());
            data.NUMBERED_YEAR = numberdeYear;
            S_NUMBER_YEAR[] actual = target.GetDataForEntity(data);

            int count = 0;
            foreach (S_NUMBER_YEAR entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.NUMBERED_YEAR.Equals(numberdeYear))
                {
                    Assert.Fail(string.Format("Fail Data：NUMBERED_YEAR {0},DENSHU_KBN_CD {1},KYOTEN_CD {2}"), entity.NUMBERED_YEAR, entity.DENSHU_KBN_CD, entity.KYOTEN_CD);
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // DENSHU_KBN_CD
            data = new S_NUMBER_YEAR();
            SqlInt16 denshuKbnCd = SqlInt16.Parse("1000");
            data.DENSHU_KBN_CD = denshuKbnCd;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (S_NUMBER_YEAR entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.DENSHU_KBN_CD.Equals(denshuKbnCd))
                {
                    Assert.Fail(string.Format("Fail Data：NUMBERED_YEAR {0},DENSHU_KBN_CD {1},KYOTEN_CD {2}"), entity.NUMBERED_YEAR, entity.DENSHU_KBN_CD, entity.KYOTEN_CD);
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // KYOTEN_CD
            data = new S_NUMBER_YEAR();
            SqlInt16 kyotenCd = SqlInt16.Parse("3000");
            data.KYOTEN_CD = kyotenCd;
            actual = target.GetDataForEntity(data);

            count = 0;
            foreach (S_NUMBER_YEAR entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.KYOTEN_CD.Equals(kyotenCd))
                {
                    Assert.Fail(string.Format("Fail Data：NUMBERED_YEAR {0},DENSHU_KBN_CD {1},KYOTEN_CD {2}"), entity.NUMBERED_YEAR, entity.DENSHU_KBN_CD, entity.KYOTEN_CD);
                }
                count++;
            }
            Assert.IsTrue(actual.Length == count);

            // NUMBERED_YEAR,DENSHU_KBN_CD,KYOTEN_CD
            data = new S_NUMBER_YEAR();
            data.NUMBERED_YEAR = numberdeYear;
            data.DENSHU_KBN_CD = denshuKbnCd;
            data.KYOTEN_CD = kyotenCd;
            actual = target.GetDataForEntity(data);

            foreach (S_NUMBER_YEAR entity in actual)
            {
                if (entity.DELETE_FLG.IsTrue
                    || !entity.NUMBERED_YEAR.Equals(numberdeYear)
                    || !entity.DENSHU_KBN_CD.Equals(denshuKbnCd)
                    || !entity.KYOTEN_CD.Equals(kyotenCd))
                {
                    Assert.Fail(string.Format("Fail Data：NUMBERED_YEAR {0},DENSHU_KBN_CD {1},KYOTEN_CD {2}"), entity.NUMBERED_YEAR, entity.DENSHU_KBN_CD, entity.KYOTEN_CD);
                }
            }
            Assert.IsTrue(actual.Length == 1);
        }
    }
}
