// $Id: SalesPaymentConstansTest.cs 2498 2013-09-25 11:05:33Z sanbongi $
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shougun.Function.ShougunCSCommon.Const;

namespace CommonTestProject.Const
{


    /// <summary>
    /// SalesPaymentConstansTest のテスト クラスです。すべての
    /// SalesPaymentConstansTest 単体テストをここに含めます
    /// </summary>
    [TestClass()]
    public class SalesPaymentConstansTest
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
        // 
        //テストを作成するときに、次の追加属性を使用することができます:
        //
        //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
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

        /// <summary>
        /// GetKakuteiKbnName のテスト
        /// </summary>
        [TestMethod()]
        public void GetKakuteiKbnNameTest()
        {
            short kbn = 0;
            string expected = string.Empty;
            string actual = SalesPaymentConstans.GetKakuteiKbnName(kbn);
            Assert.AreEqual(expected, actual);

            kbn = 1;
            expected = "確定伝票";
            actual = SalesPaymentConstans.GetKakuteiKbnName(kbn);
            Assert.AreEqual(expected, actual);

            kbn = 2;
            expected = "未確定伝票";
            actual = SalesPaymentConstans.GetKakuteiKbnName(kbn);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// GetDenpyouKbnNameRyaku のテスト
        /// </summary>
        [TestMethod()]
        public void GetDenpyouKbnNameRyakuTest()
        {
            short denpyouKbnCd = 0;
            string expected = string.Empty;
            string actual = SalesPaymentConstans.GetDenpyouKbnNameRyaku(denpyouKbnCd);
            Assert.AreEqual(expected, actual);

            denpyouKbnCd = 1;
            expected = "売上";
            actual = SalesPaymentConstans.GetDenpyouKbnNameRyaku(denpyouKbnCd);
            Assert.AreEqual(expected, actual);

            denpyouKbnCd = 2;
            expected = "支払";
            actual = SalesPaymentConstans.GetDenpyouKbnNameRyaku(denpyouKbnCd);
            Assert.AreEqual(expected, actual);
        }
    }
}
