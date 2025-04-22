namespace ChouhyouPatternPopup
{
    public class ConstClass
    {
        #region 売上＆支払推移表

        /// <summary>
        /// 業者 項目ID
        /// </summary>
        public static int SUIIHYO_GYOUSHA_KOUMOKU_ID = 5;

        /// <summary>
        /// 現場 項目ID
        /// </summary>
        public static int SUIIHYO_GENBA_KOUMOKU_ID = 6;

        /// <summary>
        /// 荷卸業者 項目ID
        /// </summary>
        public static int SUIIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID = 10;

        /// <summary>
        /// 荷卸現場 項目ID
        /// </summary>
        public static int SUIIHYO_NIOROSHI_GENBA_KOUMOKU_ID = 11;

        /// <summary>
        /// 荷積業者 項目ID
        /// </summary>
        public static int SUIIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID = 12;

        /// <summary>
        /// 荷積現場 項目ID
        /// </summary>
        public static int SUIIHYO_NIDUMI_GENBA_KOUMOKU_ID = 13;

        /// <summary>
        /// 車輌 項目ID
        /// </summary>
        public static int SUIIHYO_SHARYO_KOUMOKU_ID = 16;

        /// <summary>
        /// 運搬業者 項目ID
        /// </summary>
        public static int SUIIHYO_UNPAN_GYOUSHA_KOUMOKU_ID = 18;

        #endregion

        #region 売上＆支払集計表

        /// <summary>
        /// 業者 項目ID
        /// </summary>
        public static int UR_SH_SHUKEIHYO_GYOUSHA_KOUMOKU_ID = 5;

        /// <summary>
        /// 現場 項目ID
        /// </summary>
        public static int UR_SH_SHUKEIHYO_GENBA_KOUMOKU_ID = 6;

        /// <summary>
        /// 荷降業者 項目ID
        /// </summary>
        public static int UR_SH_SHUKEIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID = 10;

        /// <summary>
        /// 荷降現場 項目ID
        /// </summary>
        public static int UR_SH_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID = 11;

        /// <summary>
        /// 荷積業者 項目ID
        /// </summary>
        public static int UR_SH_SHUKEIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID = 12;

        /// <summary>
        /// 荷積現場 項目ID
        /// </summary>
        public static int UR_SH_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID = 13;

        /// <summary>
        /// 車輌 項目ID
        /// </summary>
        public static int UR_SH_SHUKEIHYO_SHARYO_KOUMOKU_ID = 16;

        /// <summary>
        /// 運搬業者 項目ID
        /// </summary>
        public static int UR_SH_SHUKEIHYO_UNPAN_GYOUSHA_KOUMOKU_ID = 18;

        //VAN 20200326 #134974, #134977 S
        /// <summary>
        /// 品名 項目ID
        /// </summary>
        public static int UR_SH_SHUKEIHYO_HINMEI_KOUMOKU_ID = 7;
        //VAN 20200326 #134974, #134977 E
        #endregion

        #region 運賃集計表

        /// <summary>
        /// 荷降業者 項目ID
        /// </summary>
        public static int UNCHIN_SHUKEIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID = 5;

        /// <summary>
        /// 荷降現場 項目ID
        /// </summary>
        public static int UNCHIN_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID = 6;

        /// <summary>
        /// 荷積業者 項目ID
        /// </summary>
        public static int UNCHIN_SHUKEIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID = 7;

        /// <summary>
        /// 荷積現場 項目ID
        /// </summary>
        public static int UNCHIN_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID = 8;

        /// <summary>
        /// 車輌 項目ID
        /// </summary>
        public static int UNCHIN_SHUKEIHYO_SHARYO_KOUMOKU_ID = 10;

        /// <summary>
        /// 運搬業者 項目ID
        /// </summary>
        public static int UNCHIN_SHUKEIHYO_UNPAN_GYOUSHA_KOUMOKU_ID = 4;

        #endregion

        #region 計量集計表

        /// <summary>
        /// 業者 項目ID
        /// </summary>
        public static int KEIRYOU_SHUKEIHYO_GYOUSHA_KOUMOKU_ID = 4;

        /// <summary>
        /// 現場 項目ID
        /// </summary>
        public static int KEIRYOU_SHUKEIHYO_GENBA_KOUMOKU_ID = 5;

        /// <summary>
        /// 荷降業者 項目ID
        /// </summary>
        public static int KEIRYOU_SHUKEIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID = 17;

        /// <summary>
        /// 荷降現場 項目ID
        /// </summary>
        public static int KEIRYOU_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID = 18;

        /// <summary>
        /// 荷積業者 項目ID
        /// </summary>
        public static int KEIRYOU_SHUKEIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID = 15;

        /// <summary>
        /// 荷積現場 項目ID
        /// </summary>
        public static int KEIRYOU_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID = 16;

        /// <summary>
        /// 車輌 項目ID
        /// </summary>
        public static int KEIRYOU_SHUKEIHYO_SHARYO_KOUMOKU_ID = 12;

        /// <summary>
        /// 運搬業者 項目ID
        /// </summary>
        public static int KEIRYOU_SHUKEIHYO_UNPAN_GYOUSHA_KOUMOKU_ID = 10;

        #endregion

        //VAN 20200326 #134974, #134977 S
        public static readonly string MSG_ERR_CAN_NOT_SET_HINMEI_MEISAI = "集計項目で品名を選択しているため、明細項目に品名を選択することはできません。（集計項目で選択した「品名」を帳票に表示します）";
        public static readonly string MSG_ERR_REQUIRED_HINMEI = "集計項目、明細項目のいずれかで「品名」を選択する必要があります。";
        //VAN 20200326 #134974, #134977 E
    }
}
