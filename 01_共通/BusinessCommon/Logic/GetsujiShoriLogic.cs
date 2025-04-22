using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Message;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    /// <summary>
    /// 月次処理実行計算クラス
    /// 計算結果はプロパティ(MonthlyLockUrDatas、MonthlyLockShDatas)から取得してください。
    /// </summary>
    public class GetsujiShoriLogic
    {
        /// <summary>
        /// 月次処理タイプ
        /// </summary>
        /// <remarks>
        /// 売上と支払片方で処理する必要場合(売掛一覧など画面から呼出し場合)、タイプを設定する。
        /// デフォルト(月次処理画面から呼出し場合)では両方を処理する。
        /// 
        /// 片方設定されたか判断は下記参照(ビットAND計算)
        /// if ((param & SEIKYUU) == SEIKYUU) { ... } ← SEIKYUUが設定された(BOTHも含む：0x01 & 0x11 = 0x01)
        /// </remarks>
        public enum GETSUJI_SHORI_TYPE : short
        {
            /// <summary>
            /// 請求・売上・売掛
            /// </summary>
            /// <remarks>0x01</remarks>
            SEIKYUU = 1,
            /// <summary>
            /// 支払・買掛
            /// </summary>
            /// <remarks>0x10</remarks>
            SHIHARAI = 2,
            /// <summary>
            /// 両方
            /// </summary>
            /// <remarks>0x11</remarks>
            BOTH = 3
        }

        #region Field

        #region DAO

        /// <summary>月次処理用DAO</summary>
        private GetsujiShoriDao dao;
        /// <summary>月次処理中用DAO</summary>
        private T_GETSUJI_SHORI_CHUDao getsujiShoriChuDao;
        /// <summary>取引先マスタ用DAO</summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;
        /// <summary>取引先請求マスタ用DAO</summary>
        private IM_TORIHIKISAKI_SEIKYUUDao torihikisakiSeikyuuDao;
        /// <summary>取引先支払マスタ用DAO</summary>
        private IM_TORIHIKISAKI_SHIHARAIDao torihikisakiShiharaiDao;

        #region 在庫月次処理を追加 chenzz
        /// <summary>現場マスタ用DAO</summary>
        private IM_SYS_INFODao sysInfoDao;
        private IM_GENBADao genbaDao;
        private IM_ZAIKO_HINMEIDao zaikoHinmeiDao;
        private IM_KAISHI_ZAIKO_INFODao kaishiZaikoInfoDao;
        #endregion

        #endregion

        #region 締処理集計用変数

        //今回入金額
        private decimal konkaiNyuukingaku = 0;
        //今回出金額
        private decimal konkaiShukingaku = 0;
        //今回調整額
        private decimal konkaiChouseigaku = 0;
        //今回売上額
        private decimal konkaiUriagegaku = 0;
        //今回支払額
        private decimal konkaiShiharaigaku = 0;
        //今回請内税額
        private decimal konkaiSeiUtizeigaku = 0;
        //今回請外税額
        private decimal konkaiSeisotozeigaku = 0;
        //今回伝内税額
        private decimal konkaiDenUtizeigaku = 0;
        //今回伝外税額
        private decimal konkaiDensotozeigaku = 0;
        //今回明内税額
        private decimal konkaiMeiUtizeigaku = 0;
        //今回明外税額
        private decimal konkaiMeisotozeigaku = 0;
        //今回御請求額
        private decimal konkaiSeikyuugaku = 0;

        #endregion

        #region 一時保持用変数
        /// <summary>
        /// 保存用システムID
        /// </summary>
        private string saveSysID;

        /// <summary>
        /// 保存用伝種区分
        /// </summary>
        private string saveDenshuKbn;
        #endregion

        #region 進行状況表示用プログレスバー

        /// <summary>
        /// 進行状況表示用プログレスバー
        /// </summary>
        ToolStripProgressBar progressBar = null;

        #endregion

        #region 適格請求書用変数
        /// <summary>
        /// 適格区分(1.旧処理、2.適格対応)
        /// </summary>
        private int LInvoceKBN { get; set; }
        #endregion

        #endregion

        #region Property

        /// <summary>
        /// 取引先_請求マスタEntityのList
        /// </summary>
        public List<M_TORIHIKISAKI_SEIKYUU> TorihikisakiSeikyuList { get; private set; }

        /// <summary>
        /// 取引先_支払マスタEntityのList
        /// </summary>
        public List<M_TORIHIKISAKI_SHIHARAI> TorihikisakiShiharaiList { get; private set; }

        /// <summary>
        /// 売上月次処理データ格納Dictionary
        /// Key:月次年月(yyyy/MM)　Value:該当月次年月における取引先別の売上月次処理データリスト
        /// </summary>
        public Dictionary<string, List<T_MONTHLY_LOCK_UR>> MonthlyLockUrDatas { get; private set; }

        /// <summary>
        /// 支払月次処理データ格納Dictionary
        /// Key:月次年月(yyyy/MM)　Value:該当月次年月における取引先別の支払月次処理データリスト
        /// </summary>
        public Dictionary<string, List<T_MONTHLY_LOCK_SH>> MonthlyLockShDatas { get; private set; }

        #region 在庫月次処理を追加 chenzz
        /// <summary>
        /// 在庫月次処理データ格納Dictionary
        /// Key:月次年月(yyyy/MM)　Value:該当月次年月における現場別の支払月次処理データリスト
        /// </summary>
        public Dictionary<string, List<T_MONTHLY_LOCK_ZAIKO>> MonthlyLockZaikoDatas { get; private set; }

        /// <summary>
        /// システム設定エンティティを取得・設定します
        /// </summary>
        internal M_SYS_INFO SysInfo { get; private set; }
        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GetsujiShoriLogic()
        {
            this.dao = DaoInitUtility.GetComponent<GetsujiShoriDao>();
            this.getsujiShoriChuDao = DaoInitUtility.GetComponent<T_GETSUJI_SHORI_CHUDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.torihikisakiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();

            this.TorihikisakiSeikyuList = new List<M_TORIHIKISAKI_SEIKYUU>();
            this.TorihikisakiShiharaiList = new List<M_TORIHIKISAKI_SHIHARAI>();
            this.MonthlyLockUrDatas = new Dictionary<string, List<T_MONTHLY_LOCK_UR>>();
            this.MonthlyLockShDatas = new Dictionary<string, List<T_MONTHLY_LOCK_SH>>();

            // 在庫月次処理を追加 chenzz start
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.zaikoHinmeiDao = DaoInitUtility.GetComponent<IM_ZAIKO_HINMEIDao>();
            this.MonthlyLockZaikoDatas = new Dictionary<string, List<T_MONTHLY_LOCK_ZAIKO>>();
            this.kaishiZaikoInfoDao = DaoInitUtility.GetComponent<IM_KAISHI_ZAIKO_INFODao>();
            // 在庫月次処理を追加 chenzz end
        }

        /// <summary>
        /// コンストラクタ
        /// 起動元画面でプログレスバーによる進行状況を表示したい場合に使用してください
        /// </summary>
        /// <param name="progresBar">進行状況表示用プログレスバー</param>
        public GetsujiShoriLogic(ToolStripProgressBar progresBar)
            : this()
        {
            if (progresBar != null)
            {
                this.progressBar = progresBar;
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// 全取引先の月次処理を行います【本メソッドを使用する場合は、月次処理中テーブルを呼び出し元で更新してください】
        /// </summary>
        /// <param name="year">月次処理を行う年</param>
        /// <param name="month">月次処理を行う月</param>
        /// <param name="isDispAlert">過去データが無い場合に過去の締め処理実行をアラートを表示して確認するかのフラグ(False：確認せずに実行)</param>
        /// <param name="InvoiceKBN">適格区分(1.旧処理、2.適格対応)</param>
        /// <returns>True：正常終了　False：処理キャンセル</returns>
        /// <param name="windowId">実行元画面のWindowId</param>
        public bool GetsujiShori(int year, int month,
            bool isDispAlert, int InvoiceKBN, r_framework.Const.WINDOW_ID windowId = r_framework.Const.WINDOW_ID.NONE, GETSUJI_SHORI_TYPE shoriType = GETSUJI_SHORI_TYPE.BOTH)
        {
            /* ========================================    注意    ============================================
             * ===  このメソッドを使用する場合は、月次処理中テーブルを呼び出し元で更新してください。        ===
             * ================================================================================================ */

            /* ==========================================    特記事項    ==========================================  */
            /* 過去の月次データが無い(歯抜けも含む)場合、『過去の伝票を合算した』月次処理を行った後に本来指定されて  */
            /* いる年月の月次処理を行う。                                                                     　　　 */
            /* ※過去分の残高を算出するため。                                                                        */
            /* 最終的に作成されるデータは取引先1件に対して1件の売上・支払月次データとし、合算による月次データは残高  */
            /* の取得のみに使用し保存は行わない                                                                      */
            /* ※処理の都合上、プロパティに合算の月次データも保持してる事に注意                                      */
            /* ※月次処理画面では歯抜けの状態で月次処理データを作成する事は禁止としている。                          */
            /*                                                                                                       */
            /* 取引先の設定が「現金」の場合は、当月の入出金、伝票の金額を0円とし、過去データがあれば残高を引き継ぐだ */
            /* けのデータを作成する。                                                                                */
            /* 現金/掛けの変更は月次処理を行った後に変更する運用としているため、場合によっては不正なデータが作成され */
            /* るが現状は不問としている。                                                                            */

            LogUtility.DebugMethodStart(year, month, isDispAlert, InvoiceKBN, windowId, shoriType);

            try
            {
                // 在庫月次処理を追加 chenzz start
                this.SysInfo = this.sysInfoDao.GetAllData().FirstOrDefault();
                List<GetusjiShoriZaikoDTOClass> zaikoDtoList = new List<GetusjiShoriZaikoDTOClass>();
                if (this.SysInfo.ZAIKO_KANRI == 1)
                {
                    M_GENBA condition = new M_GENBA();
                    condition.JISHA_KBN = true;
                    M_GENBA[] genbaDatas = DaoInitUtility.GetComponent<IM_GENBADao>().GetAllValidData(condition);

                    M_ZAIKO_HINMEI[] zaikoHinmeiDatas = this.zaikoHinmeiDao.GetAllData();
                    for (int i = 0; i < genbaDatas.Length; i++)
                    {
                        for (int j = 0; j < zaikoHinmeiDatas.Length; j++)
                        {
                            GetusjiShoriZaikoDTOClass zaikoDto = new GetusjiShoriZaikoDTOClass();
                            zaikoDto.GYOUSHA_CD = genbaDatas[i].GYOUSHA_CD;
                            zaikoDto.GENBA_CD = genbaDatas[i].GENBA_CD;
                            zaikoDto.ZAIKO_HINMEI_CD = zaikoHinmeiDatas[j].ZAIKO_HINMEI_CD;
                            zaikoDtoList.Add(zaikoDto);
                        }
                    }
                }
                // 在庫月次処理を追加 chenzz end
                // 締済チェック用取引先CDリスト
                List<string> torihikisakiCdList = new List<string>();

                // 取引先マスタデータ取得(削除されているものも含む)
                M_TORIHIKISAKI[] torihikisakiDatas = this.GetTorihikisaki();
                for (int i = 0; i < torihikisakiDatas.Length; i++)
                {
                    M_TORIHIKISAKI torihikisakiData = torihikisakiDatas[i];
                    torihikisakiCdList.Add(torihikisakiData.TORIHIKISAKI_CD);
                }

                // 1ヶ月前の締済データが無い場合、アラートを表示して過去データも締めるかを確認する
                // ※過去データも締めると時間がかかるため確認を行う
                // ※アラートでキャンセル押下時は処理中断
                if (isDispAlert)
                {
                    DateTime checkDate = new DateTime(year, month, 1);
                    checkDate = checkDate.AddMonths(-1);

                    // 締済チェック(True:前月データ有り　False:前月データ無し)
                    bool isCheck = this.CheckShimezumiData(torihikisakiCdList, checkDate.Year, checkDate.Month, shoriType);
                    bool isCheck2 = this.CheckZaikoShimezumiData(zaikoDtoList, checkDate.Year, checkDate.Month);

                    if (!isCheck || !isCheck2)
                    {
                        // アラート
                        StringBuilder msg = new StringBuilder();
                        if (windowId == r_framework.Const.WINDOW_ID.T_GETSUJI)
                        {
                            /* 月次処理画面の文言 */
                            msg.Append("前月までの月次処理がされていないため、前月までの伝票を全て合算して月次処理を行います。");
                            msg.Append("\r\n");
                            msg.Append("処理に時間がかかる場合がありますが、実行してよろしいですか？");
                        }
                        else
                        {
                            // 月次処理画面以外の文言(掛金一覧表、元帳)
                            msg.Append("前月までの月次処理がされていないため、");
                            msg.Append("\r\n");
                            msg.Append("表示に時間がかかる場合があります。");
                            msg.Append("\r\n");
                            msg.Append("実行してよろしいですか？");
                        }

                        DialogResult result = MessageBoxUtility.MessageBoxShowConfirm(msg.ToString());
                        if (result != DialogResult.Yes)
                        {
                            // 月次処理中断
                            return false;
                        }
                    }
                }

                // プログレスバーが指定されている場合は進行状況を表示する
                if (this.progressBar != null)
                {
                    InitProgressBar(0, torihikisakiCdList.Count);
                }

                // DBアクセス多発防止用に取引先_請求＆支払マスタを取得し、保持しておく
                // 20150715 分別処理を備えため、一応追加する Start
                //          (片方だけ処理するわけがないはず)
                if ((shoriType & GETSUJI_SHORI_TYPE.SEIKYUU) == GETSUJI_SHORI_TYPE.SEIKYUU)
                {
                    M_TORIHIKISAKI_SEIKYUU[] mtses = this.torihikisakiSeikyuuDao.GetAllData();
                    this.TorihikisakiSeikyuList.AddRange(mtses);
                }
                if ((shoriType & GETSUJI_SHORI_TYPE.SHIHARAI) == GETSUJI_SHORI_TYPE.SHIHARAI)
                {
                    M_TORIHIKISAKI_SHIHARAI[] mtshs = this.torihikisakiShiharaiDao.GetAllData();
                    this.TorihikisakiShiharaiList.AddRange(mtshs);
                }
                // 20150715 分別処理を備えため、一応追加する End

                // 月次処理実行
                int count = 1;
                foreach (string torihikisakiCd in torihikisakiCdList)
                {
                    this.GetsujiShori(torihikisakiCd, year, month, false, InvoiceKBN, windowId, shoriType);

                    if (this.progressBar != null)
                    {
                        // プログレスバー更新
                        this.SetProgressBarValue(count);
                    }
                    count++;
                }

                // 在庫月次処理を追加 chenzz start
                // 在庫月次処理実行
                count = 1;
                if (this.SysInfo.ZAIKO_KANRI == 1)
                {
                    foreach (GetusjiShoriZaikoDTOClass zaikoDto in zaikoDtoList)
                    {
                        this.GetsujiZaikoShori(zaikoDto, year, month);

                        if (this.progressBar != null)
                        {
                            // プログレスバー更新
                            this.SetProgressBarValue(count);
                        }
                        count++;
                    }
                }
                // 在庫月次処理を追加 chenzz end

                // プログレスバーリセット
                if (this.progressBar != null)
                {
                    this.ResetProgBar();
                }

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.DebugMethodEnd();
                throw ex;
            }
        }

        /// <summary>
        /// 引数で指定された年月で指定取引先の月次処理を実行します【本メソッドを使用する場合は、月次処理中テーブルを呼び出し元で更新してください】
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="year">月次処理を行う年</param>
        /// <param name="month">月次処理を行う月</param>
        /// <param name="isDispAlert">過去データが無い場合に過去の締め処理実行をアラートを表示して確認するかのフラグ(False：確認せずに実行)</param>
        /// <param name="InvoiceKBN">適格区分(1.旧処理、2.適格対応)</param>
        /// <param name="windowId">実行元画面のWindowId</param>
        public bool GetsujiShori(string torihikisakiCd, int year, int month,
            bool isDispAlert, int InvoiceKBN, r_framework.Const.WINDOW_ID windowId = r_framework.Const.WINDOW_ID.NONE, GETSUJI_SHORI_TYPE shoriType = GETSUJI_SHORI_TYPE.BOTH)
        {
            /* ========================================    注意    ============================================
             * ===  このメソッドを使用する場合は、月次処理中テーブルを呼び出し元で更新してください。        ===
             * ================================================================================================ */

            /* ==========================================    特記事項    ==========================================  */
            /* 過去の月次データが無い(歯抜けも含む)場合、『過去の伝票を合算した』月次処理を行った後に本来指定されて  */
            /* いる年月の月次処理を行う。                                                                     　　　 */
            /* ※過去分の残高を算出するため。                                                                        */
            /* 最終的に作成されるデータは取引先1件に対して1件の売上・支払月次データとし、合算による月次データは残高  */
            /* の取得のみに使用し保存は行わない                                                                      */
            /* ※処理の都合上、プロパティに合算の月次データも保持してる事に注意                                      */
            /* ※月次処理画面では歯抜けの状態で月次処理データを作成する事は禁止としている。                          */
            /*                                                                                                       */
            /* 取引先の設定が「現金」の場合は、当月の入出金、伝票の金額を0円とし、過去データがあれば残高を引き継ぐだ */
            /* けのデータを作成する。                                                                                */
            /* 現金/掛けの変更は月次処理を行った後に変更する運用としているため、場合によっては不正なデータが作成され */
            /* るが現状は不問としている。                                                                            */

            LogUtility.DebugMethodStart(torihikisakiCd, year, month, isDispAlert, InvoiceKBN, windowId, shoriType);

            // 適格区分(1.旧処理、2.適格対応)
            LInvoceKBN = InvoiceKBN;

            // 1ヶ月前の締済データが無い場合、アラートを表示して過去データも締めるかを確認する
            // ※過去データも締めると時間がかかるため確認を行う
            // ※アラートでキャンセル押下時は処理中断
            if (isDispAlert)
            {
                List<string> torihikisakiCdList = new List<string>();
                torihikisakiCdList.Add(torihikisakiCd);
                DateTime checkDate = new DateTime(year, month, 1);
                checkDate = checkDate.AddMonths(-1);

                // 締済チェック(True:前月データ有り　False:前月データ無し)
                bool isCheck = this.CheckShimezumiData(torihikisakiCdList, checkDate.Year, checkDate.Month, shoriType);
                if (!isCheck)
                {
                    // アラート
                    StringBuilder msg = new StringBuilder();
                    if (windowId == r_framework.Const.WINDOW_ID.T_GETSUJI)
                    {
                        /* 月次処理画面の文言 */
                        msg.Append("前月までの月次処理がされていないため、前月までの伝票を全て合算して月次処理を行います。");
                        msg.Append("\r\n");
                        msg.Append("処理に時間がかかる場合がありますが、実行してよろしいですか？");
                    }
                    else
                    {
                        // 月次処理画面以外の文言(掛金一覧表、元帳)
                        msg.Append("前月までの月次処理がされていないため、");
                        msg.Append("\r\n");
                        msg.Append("表示に時間がかかる場合があります。");
                        msg.Append("\r\n");
                        msg.Append("実行してよろしいですか？");
                    }

                    DialogResult result = MessageBoxUtility.MessageBoxShowConfirm(msg.ToString());
                    if (result != DialogResult.Yes)
                    {
                        // 月次処理中断
                        return false;
                    }
                }
            }

            #region 売上月次処理

            // 20150715 月次処理以外の画面の処理速度改善ため、処理タイプを設定した場合処理する Start
            if ((shoriType & GETSUJI_SHORI_TYPE.SEIKYUU) == GETSUJI_SHORI_TYPE.SEIKYUU)
            // 20150715 月次処理以外の画面の処理速度改善ため、処理タイプを設定した場合処理する End
            {
                // 取引先_請求情報取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
                // ※月次処理実行時の速度問題によりListを優先で使用。（Listが空の場合は月次処理画面以外からの呼び出し）
                M_TORIHIKISAKI_SEIKYUU mtse;
                if (this.TorihikisakiSeikyuList != null && this.TorihikisakiSeikyuList.Count > 0)
                {
                    mtse = this.TorihikisakiSeikyuList.Find(delegate(M_TORIHIKISAKI_SEIKYUU data) { return data.TORIHIKISAKI_CD.Equals(torihikisakiCd); });

                    // Listに無いというのはありえないが念のため回避用
                    if (mtse == null)
                    {
                        mtse = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                    }
                }
                else
                {
                    mtse = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                }

                if (mtse != null && mtse.TORIHIKI_KBN_CD == 2)
                {
                    /* 取引先の設定が掛けの場合、伝票データをから当月の金額を算出する */

                    // 売上用のDTO作成
                    GetsujiShoriDTOClass uriageDto = new GetsujiShoriDTOClass();
                    uriageDto.TORIHIKISAKI_CD = torihikisakiCd;

                    DateTime fromDate = new DateTime(year, month, 1);
                    uriageDto.FROM_DATE = fromDate.ToString("yyyy/MM/dd");

                    DateTime toDate = new DateTime(year, month, 1);
                    toDate = toDate.AddMonths(1).AddDays(-1);
                    uriageDto.TO_DATE = toDate.ToString("yyyy/MM/dd");


                    //前回(1ヶ月前)の繰越額の取得
                    decimal zandaka = 0;
                    DateTime searchDate = new DateTime(year, month, 1);
                    searchDate = searchDate.AddMonths(-1);
                    bool isExist = GetUriageKurikosigaku(torihikisakiCd, searchDate.Year, searchDate.Month, ref zandaka);
                    if (isExist)
                    {
                        // 前回(1ヶ月前)のデータがあった場合
                        uriageDto.PREVIOUS_MONTH_BALANCE = zandaka;
                    }
                    else
                    {
                        // データが無い場合は1ヶ月前までの月次データを作成して取得する
                        uriageDto.PREVIOUS_MONTH_BALANCE = this.GetUriageKurikosigakuFromPastGetsujiData(torihikisakiCd, year, month);
                    }

                    // 売上データ作成開始
                    this.ExecUriageGetsujiShori(uriageDto);
                }
                else
                {
                    /* 取引先の設定が現金の場合、当月の金額は0円とし過去データがある場合は繰越残高だけ引き継いだデータを作成する */

                    /* 過去データがあるか調べ、ある場合は残高を取得 */
                    decimal zandaka = 0;
                    DateTime shimeDate = new DateTime(year, month, 1);
                    DataTable latestGetsujiData = this.dao.GetLatestGetsujiDateByTorihikisakiCd(torihikisakiCd, shimeDate.ToString("yyyy/MM/dd"));
                    if (latestGetsujiData != null && latestGetsujiData.Rows.Count > 0)
                    {
                        int latestGetsujiShoriYear = int.Parse(latestGetsujiData.Rows[0]["YEAR"].ToString());
                        int latestGetsujiShoriMonth = int.Parse(latestGetsujiData.Rows[0]["MONTH"].ToString());
                        this.GetUriageKurikosigaku(torihikisakiCd, latestGetsujiShoriYear, latestGetsujiShoriMonth, ref zandaka);
                    }

                    /* 売上月次処理データ作成 */
                    T_MONTHLY_LOCK_UR monthlyLockUrData = new T_MONTHLY_LOCK_UR();
                    //取引先
                    monthlyLockUrData.TORIHIKISAKI_CD = torihikisakiCd;
                    //年
                    monthlyLockUrData.YEAR = short.Parse(year.ToString());
                    //月
                    monthlyLockUrData.MONTH = short.Parse(month.ToString());
                    //繰越残高
                    monthlyLockUrData.PREVIOUS_MONTH_BALANCE = zandaka;
                    //入金額
                    monthlyLockUrData.NYUUKIN_KINGAKU = 0;
                    //税抜金額
                    monthlyLockUrData.KINGAKU = 0;
                    //消費税額
                    monthlyLockUrData.TAX = 0;
                    //締内税額	
                    monthlyLockUrData.SHIME_UTIZEI_GAKU = 0;
                    //締外税額	
                    monthlyLockUrData.SHIME_SOTOZEI_GAKU = 0;
                    //伝内税額	
                    monthlyLockUrData.DEN_UTIZEI_GAKU = 0;
                    //伝外税額	
                    monthlyLockUrData.DEN_SOTOZEI_GAKU = 0;
                    //明内税額	
                    monthlyLockUrData.MEI_UTIZEI_GAKU = 0;
                    //明外税額	
                    monthlyLockUrData.MEI_SOTOZEI_GAKU = 0;
                    //税込金額
                    monthlyLockUrData.TOTAL_KINGAKU = 0;
                    //調整前差引残高
                    monthlyLockUrData.ZANDAKA = zandaka;
                    //適格区分(1.旧 2.適格請求)
                    monthlyLockUrData.INVOICE_KBN = LInvoceKBN;

                    // 月次処理データを格納
                    DateTime keyDate = new DateTime(year, month, 1);
                    string keyString = keyDate.ToString("yyyy/MM");
                    if (this.MonthlyLockUrDatas.ContainsKey(keyString))
                    {
                        List<T_MONTHLY_LOCK_UR> dataList = this.MonthlyLockUrDatas[keyString];
                        dataList.Add(monthlyLockUrData);
                    }
                    else
                    {
                        List<T_MONTHLY_LOCK_UR> dataList = new List<T_MONTHLY_LOCK_UR>();
                        dataList.Add(monthlyLockUrData);
                        this.MonthlyLockUrDatas.Add(keyString, dataList);
                    }
                }
            }

            #endregion

            #region 支払月次処理

            // 20150715 月次処理以外の画面の処理速度改善ため、処理タイプを設定した場合処理する Start
            if ((shoriType & GETSUJI_SHORI_TYPE.SHIHARAI) == GETSUJI_SHORI_TYPE.SHIHARAI)
            // 20150715 月次処理以外の画面の処理速度改善ため、処理タイプを設定した場合処理する End
            {
                // 取引先_支払情報取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
                // ※月次処理実行時の速度問題によりListを優先で使用。（Listが空の場合は月次処理画面以外からの呼び出し）
                M_TORIHIKISAKI_SHIHARAI mtsh;
                if (this.TorihikisakiShiharaiList != null && this.TorihikisakiShiharaiList.Count > 0)
                {
                    mtsh = this.TorihikisakiShiharaiList.Find(delegate(M_TORIHIKISAKI_SHIHARAI data) { return data.TORIHIKISAKI_CD.Equals(torihikisakiCd); });

                    // Listに無いというのはありえないが念のため回避用
                    if (mtsh == null)
                    {
                        mtsh = this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
                    }
                }
                else
                {
                    mtsh = this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
                }

                if (mtsh != null && mtsh.TORIHIKI_KBN_CD == 2)
                {
                    /* 取引先の設定が掛けの場合、伝票データをから当月の金額を算出する */

                    GetsujiShoriDTOClass shiharaiDto = new GetsujiShoriDTOClass();
                    shiharaiDto.TORIHIKISAKI_CD = torihikisakiCd;

                    DateTime fromDate = new DateTime(year, month, 1);
                    shiharaiDto.FROM_DATE = fromDate.ToString("yyyy/MM/dd");

                    DateTime toDate = new DateTime(year, month, 1);
                    toDate = toDate.AddMonths(1).AddDays(-1);
                    shiharaiDto.TO_DATE = toDate.ToString("yyyy/MM/dd");

                    //前回からの繰越額の取得
                    decimal zandaka = 0;
                    DateTime searchDate = new DateTime(year, month, 1);
                    searchDate = searchDate.AddMonths(-1);
                    bool isExist = GetShiharaiKurikosigaku(torihikisakiCd, searchDate.Year, searchDate.Month, ref zandaka);
                    if (isExist)
                    {
                        // 前回(1ヶ月前)のデータがあった場合
                        shiharaiDto.PREVIOUS_MONTH_BALANCE = zandaka;
                    }
                    else
                    {
                        // データが無い場合は1ヶ月前までの月次データを作成して取得する
                        shiharaiDto.PREVIOUS_MONTH_BALANCE = this.GetShiharaiKurikosigakuFromPastGetsujiData(torihikisakiCd, year, month);
                    }

                    // 支払月次データ作成開始
                    this.ExecShiharaiGetsujiShori(shiharaiDto);
                }
                else
                {
                    /* 取引先の設定が現金の場合、当月の金額は0円とし過去データがある場合は繰越残高だけ引き継いだデータを作成する */

                    /* 過去データがあるか調べ、ある場合は残高を取得 */
                    decimal zandaka = 0;
                    DateTime shimeDate = new DateTime(year, month, 1);
                    DataTable latestGetsujiData = this.dao.GetLatestGetsujiDateByTorihikisakiCd(torihikisakiCd, shimeDate.ToString("yyyy/MM/dd"));
                    if (latestGetsujiData != null && latestGetsujiData.Rows.Count > 0)
                    {
                        int latestGetsujiShoriYear = int.Parse(latestGetsujiData.Rows[0]["YEAR"].ToString());
                        int latestGetsujiShoriMonth = int.Parse(latestGetsujiData.Rows[0]["MONTH"].ToString());
                        this.GetShiharaiKurikosigaku(torihikisakiCd, latestGetsujiShoriYear, latestGetsujiShoriMonth, ref zandaka);
                    }

                    /* 売上月次処理データ作成 */
                    T_MONTHLY_LOCK_SH monthlyLockShData = new T_MONTHLY_LOCK_SH();
                    //取引先
                    monthlyLockShData.TORIHIKISAKI_CD = torihikisakiCd;
                    //年
                    monthlyLockShData.YEAR = short.Parse(year.ToString());
                    //月
                    monthlyLockShData.MONTH = short.Parse(month.ToString());
                    //繰越残高
                    monthlyLockShData.PREVIOUS_MONTH_BALANCE = zandaka;
                    //出金額
                    monthlyLockShData.SHUKKIN_KINGAKU = 0;
                    //税抜金額
                    monthlyLockShData.KINGAKU = 0;
                    //消費税額
                    monthlyLockShData.TAX = 0;
                    //締内税額	
                    monthlyLockShData.SHIME_UTIZEI_GAKU = 0;
                    //締外税額	
                    monthlyLockShData.SHIME_SOTOZEI_GAKU = 0;
                    //伝内税額	
                    monthlyLockShData.DEN_UTIZEI_GAKU = 0;
                    //伝外税額	
                    monthlyLockShData.DEN_SOTOZEI_GAKU = 0;
                    //明内税額	
                    monthlyLockShData.MEI_UTIZEI_GAKU = 0;
                    //明外税額	
                    monthlyLockShData.MEI_SOTOZEI_GAKU = 0;
                    //税込金額
                    monthlyLockShData.TOTAL_KINGAKU = 0;
                    //調整前差引残高
                    monthlyLockShData.ZANDAKA = zandaka;
                    //適格区分(1.旧 2.適格請求)
                    monthlyLockShData.INVOICE_KBN = LInvoceKBN;

                    // 月次処理データを格納
                    DateTime keyDate = new DateTime(year, month, 1);
                    string keyString = keyDate.ToString("yyyy/MM");
                    if (this.MonthlyLockShDatas.ContainsKey(keyString))
                    {
                        List<T_MONTHLY_LOCK_SH> dataList = this.MonthlyLockShDatas[keyString];
                        dataList.Add(monthlyLockShData);
                    }
                    else
                    {
                        List<T_MONTHLY_LOCK_SH> dataList = new List<T_MONTHLY_LOCK_SH>();
                        dataList.Add(monthlyLockShData);
                        this.MonthlyLockShDatas.Add(keyString, dataList);
                    }
                }
            }

            #endregion

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 引数で指定された年月で在庫の月次処理を実行します【本メソッドを使用する場合は、月次処理中テーブルを呼び出し元で更新してください】
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="year">月次処理を行う年</param>
        /// <param name="month">月次処理を行う月</param>
        /// <param name="isDispAlert">過去データが無い場合に過去の締め処理実行をアラートを表示して確認するかのフラグ(False：確認せずに実行)</param>
        public bool GetsujiZaikoShori(GetusjiShoriZaikoDTOClass zaikoDto, int year, int month)
        {
            LogUtility.DebugMethodStart(zaikoDto, year, month);
            #region 在庫月次処理
            DataTable zaikoDatas = this.dao.GetMonthlyLockZaiko(zaikoDto);
            DateTime fromDate = new DateTime();
            if (zaikoDatas != null && zaikoDatas.Rows.Count > 0)
            {
                int fromYear = Convert.ToInt32(zaikoDatas.Rows[0]["YEAR"]);
                int fromMonth = Convert.ToInt32(zaikoDatas.Rows[0]["MONTH"]);
                fromDate = new DateTime(fromYear, fromMonth, 1);
                fromDate = fromDate.AddMonths(1);
                zaikoDto.FROM_DATE = fromDate;
            }
            else
            {
                zaikoDto.FROM_DATE = SqlDateTime.Null;
            }

            DateTime toDate = new DateTime(year, month, 1);
            toDate = toDate.AddMonths(1).AddDays(-1);
            zaikoDto.TO_DATE = toDate;
            // 前回在庫月次処理から、対象期間まで、受入量、出荷量、調整量、移動量を取得
            DataTable zen_henkouZaikoRyou = this.dao.GetHenkouZaikoRyou(zaikoDto);

            // 前回在庫月次処理以後、在庫品名の受入量、出荷量、調整量、移動量データがない場合、処理しない
            if ((zen_henkouZaikoRyou == null || zen_henkouZaikoRyou.Rows.Count == 0) && (zaikoDatas == null || zaikoDatas.Rows.Count == 0))
            {
                return true;
            }
            toDate = new DateTime(year, month, 1);
            toDate = toDate.AddDays(-1);
            zaikoDto.TO_DATE = toDate;
            DataTable zen_henkouZaikoRyou2 = this.dao.GetHenkouZaikoRyou(zaikoDto);
            //最新の在庫月次データの取得
            // 繰越在庫量用 
            decimal GOUKEI_ZAIKO_RYOU = 0;                        // 合計在庫量
            decimal UKEIRE_RYOU = 0;                              // 受入量
            decimal SHUKKA_RYOU = 0;                              // 出荷量
            decimal TYOUSEI_RYOU = 0;                             // 調整量
            decimal IDOU_OUT_RYOU = 0;　　　　　　　　　　　　　　// 該当現場から移動する移動量
            decimal IDOU_IN_RYOU = 0;                             // 該当現場に移動する移動量
            decimal PREVIOUS_MONTH_ZAIKO_RYOU = 0;                // 繰越在庫量
            // 繰越在庫金額用
            decimal GOUKEI_KINGAKU = 0;                           // 最新の月次処理の合計在庫金額
            decimal ZAIKO_KINGAKU_TOTAL = 0;                      // 受入入力の在庫金額合計
            decimal PREVIOUS_MONTH_KINGAKU = 0;                   // 繰越在庫金額
            // 移動量
            decimal IDOU_RYOU = 0;
            // 当月在庫量
            decimal MONTH_ZAIKO_RYOU = 0;
            // 当月在庫金額
            decimal MONTH_KINGAKU = 0;
            // 金額
            decimal TMP_KINGAKU = 0;
            // 開始在庫
            decimal KAISHI_ZAIKO_KINGAKU = 0;
            decimal KAISHI_ZAIKO_RYOU = 0;

            foreach (DataRow row in zen_henkouZaikoRyou2.Rows)
            {
                switch (Convert.ToString(row["KBN"]))
                {
                    case "1":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out UKEIRE_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        ZAIKO_KINGAKU_TOTAL += TMP_KINGAKU;
                        break;
                    case "2":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out SHUKKA_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        ZAIKO_KINGAKU_TOTAL += TMP_KINGAKU;
                        break;
                    case "3":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out TYOUSEI_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        ZAIKO_KINGAKU_TOTAL += TMP_KINGAKU;
                        break;
                    case "4":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out IDOU_OUT_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        ZAIKO_KINGAKU_TOTAL += TMP_KINGAKU;
                        break;
                    case "5":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out IDOU_IN_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        ZAIKO_KINGAKU_TOTAL += TMP_KINGAKU;
                        break;
                }
            }

            M_KAISHI_ZAIKO_INFO kaishiZaikoInfo = new M_KAISHI_ZAIKO_INFO();
            kaishiZaikoInfo.GYOUSHA_CD = zaikoDto.GYOUSHA_CD;
            kaishiZaikoInfo.GENBA_CD = zaikoDto.GENBA_CD;
            kaishiZaikoInfo.ZAIKO_HINMEI_CD = zaikoDto.ZAIKO_HINMEI_CD;
            M_KAISHI_ZAIKO_INFO[] result = this.kaishiZaikoInfoDao.GetAllValidData(kaishiZaikoInfo);
            DataTable henkouZaikoRyou = new DataTable();
            if (zaikoDatas != null && zaikoDatas.Rows.Count > 0)
            {
                // 繰越在庫量
                decimal.TryParse(Convert.ToString(zaikoDatas.Rows[0]["GOUKEI_ZAIKO_RYOU"]), out GOUKEI_ZAIKO_RYOU);
                KAISHI_ZAIKO_RYOU = GOUKEI_ZAIKO_RYOU + UKEIRE_RYOU + SHUKKA_RYOU + TYOUSEI_RYOU + IDOU_OUT_RYOU + IDOU_IN_RYOU;
                // 繰越在庫金額
                decimal.TryParse(Convert.ToString(zaikoDatas.Rows[0]["GOUKEI_KINGAKU"]), out GOUKEI_KINGAKU);
                KAISHI_ZAIKO_KINGAKU = GOUKEI_KINGAKU + ZAIKO_KINGAKU_TOTAL;
            }
            else
            {
                // 繰越在庫量
                if (result != null && result.Length > 0 && !result[0].KAISHI_ZAIKO_RYOU.IsNull)
                {
                    KAISHI_ZAIKO_RYOU = result[0].KAISHI_ZAIKO_RYOU.Value;
                }
                PREVIOUS_MONTH_ZAIKO_RYOU = UKEIRE_RYOU + SHUKKA_RYOU + TYOUSEI_RYOU + IDOU_OUT_RYOU + IDOU_IN_RYOU;
                // 繰越在庫金額
                if (result != null && result.Length > 0 && !result[0].KAISHI_ZAIKO_KINGAKU.IsNull)
                {
                    KAISHI_ZAIKO_KINGAKU = result[0].KAISHI_ZAIKO_KINGAKU.Value;
                }
                PREVIOUS_MONTH_KINGAKU = ZAIKO_KINGAKU_TOTAL;
            }
            fromDate = new DateTime(year, month, 1);
            zaikoDto.FROM_DATE = fromDate;
            toDate = new DateTime(year, month, 1);
            toDate = toDate.AddMonths(1).AddDays(-1);
            zaikoDto.TO_DATE = toDate;
            henkouZaikoRyou = this.dao.GetHenkouZaikoRyou(zaikoDto);

            UKEIRE_RYOU = 0;                              // 受入量
            SHUKKA_RYOU = 0;                              // 出荷量
            TYOUSEI_RYOU = 0;                             // 調整量
            IDOU_OUT_RYOU = 0;　　　　　　　　　　　　　　// 該当現場から移動する移動量
            IDOU_IN_RYOU = 0;                             // 該当現場に移動する移動量
            foreach (DataRow row in henkouZaikoRyou.Rows)
            {
                switch (Convert.ToString(row["KBN"]))
                {
                    case "1":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out UKEIRE_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        MONTH_KINGAKU += TMP_KINGAKU;
                        break;
                    case "2":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out SHUKKA_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        MONTH_KINGAKU += TMP_KINGAKU;
                        break;
                    case "3":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out TYOUSEI_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        MONTH_KINGAKU += TMP_KINGAKU;
                        break;
                    case "4":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out IDOU_OUT_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        MONTH_KINGAKU += TMP_KINGAKU;
                        break;
                    case "5":
                        decimal.TryParse(Convert.ToString(row["ZAIKO_RYOU"]), out IDOU_IN_RYOU);
                        TMP_KINGAKU = 0;
                        decimal.TryParse(Convert.ToString(row["ZAIKO_KINGAKU"]), out TMP_KINGAKU);
                        MONTH_KINGAKU += TMP_KINGAKU;
                        break;
                }
            }
            // 移動量
            IDOU_RYOU = IDOU_OUT_RYOU + IDOU_IN_RYOU;
            // 当月在庫量
            MONTH_ZAIKO_RYOU = UKEIRE_RYOU + SHUKKA_RYOU + TYOUSEI_RYOU + IDOU_RYOU;
            // 当月在庫金額
            if (this.SysInfo.ZAIKO_HYOUKA_HOUHOU == 1)
            {
                M_ZAIKO_HINMEI hinmei = this.zaikoHinmeiDao.GetDataByCd(zaikoDto.ZAIKO_HINMEI_CD);
                if (hinmei != null && !hinmei.ZAIKO_TANKA.IsNull)
                {
                    PREVIOUS_MONTH_KINGAKU = PREVIOUS_MONTH_ZAIKO_RYOU * hinmei.ZAIKO_TANKA.Value;
                    MONTH_KINGAKU = MONTH_ZAIKO_RYOU * hinmei.ZAIKO_TANKA.Value;
                }
                else
                {
                    PREVIOUS_MONTH_KINGAKU = 0;
                    MONTH_KINGAKU = 0;
                }
            }
            PREVIOUS_MONTH_ZAIKO_RYOU += KAISHI_ZAIKO_RYOU;
            PREVIOUS_MONTH_KINGAKU += KAISHI_ZAIKO_KINGAKU;
            // 合計在庫量
            GOUKEI_ZAIKO_RYOU = PREVIOUS_MONTH_ZAIKO_RYOU + MONTH_ZAIKO_RYOU;
            // 合計在庫金額
            PREVIOUS_MONTH_KINGAKU = Math.Round(PREVIOUS_MONTH_KINGAKU, MidpointRounding.AwayFromZero);
            MONTH_KINGAKU = Math.Round(MONTH_KINGAKU, MidpointRounding.AwayFromZero);
            GOUKEI_KINGAKU = PREVIOUS_MONTH_KINGAKU + MONTH_KINGAKU;

            /* 在庫月次処理データ作成 */
            T_MONTHLY_LOCK_ZAIKO monthlyLockZaikoData = new T_MONTHLY_LOCK_ZAIKO();
            monthlyLockZaikoData.GYOUSHA_CD = zaikoDto.GYOUSHA_CD;
            monthlyLockZaikoData.GENBA_CD = zaikoDto.GENBA_CD;
            monthlyLockZaikoData.ZAIKO_HINMEI_CD = zaikoDto.ZAIKO_HINMEI_CD;
            monthlyLockZaikoData.YEAR = Convert.ToInt16(year);
            monthlyLockZaikoData.MONTH = Convert.ToInt16(month);
            monthlyLockZaikoData.GENBA_CD = zaikoDto.GENBA_CD;
            monthlyLockZaikoData.PREVIOUS_MONTH_ZAIKO_RYOU = PREVIOUS_MONTH_ZAIKO_RYOU;
            monthlyLockZaikoData.PREVIOUS_MONTH_KINGAKU = PREVIOUS_MONTH_KINGAKU;
            monthlyLockZaikoData.UKEIRE_RYOU = UKEIRE_RYOU;
            monthlyLockZaikoData.SHUKKA_RYOU = SHUKKA_RYOU * -1;
            monthlyLockZaikoData.TYOUSEI_RYOU = TYOUSEI_RYOU;
            monthlyLockZaikoData.IDOU_RYOU = IDOU_RYOU;
            monthlyLockZaikoData.MONTH_ZAIKO_RYOU = MONTH_ZAIKO_RYOU;
            monthlyLockZaikoData.MONTH_KINGAKU = MONTH_KINGAKU;
            monthlyLockZaikoData.GOUKEI_ZAIKO_RYOU = GOUKEI_ZAIKO_RYOU;
            monthlyLockZaikoData.GOUKEI_KINGAKU = GOUKEI_KINGAKU;
            // TODO
            monthlyLockZaikoData.DELETE_FLG = false;

            // 月次処理データを格納
            DateTime keyDate = new DateTime(year, month, 1);
            string keyString = keyDate.ToString("yyyy/MM");
            if (this.MonthlyLockZaikoDatas.ContainsKey(keyString))
            {
                List<T_MONTHLY_LOCK_ZAIKO> dataList = this.MonthlyLockZaikoDatas[keyString];
                dataList.Add(monthlyLockZaikoData);
            }
            else
            {
                List<T_MONTHLY_LOCK_ZAIKO> dataList = new List<T_MONTHLY_LOCK_ZAIKO>();
                dataList.Add(monthlyLockZaikoData);
                this.MonthlyLockZaikoDatas.Add(keyString, dataList);
            }
            #endregion
            LogUtility.DebugMethodEnd();
            return true;
        }

        #region 締済データ存在チェック

        /// <summary>
        /// 指定年月の締済データがあるかチェックを行います
        /// </summary>
        /// <param name="torihikisakiCdList">チェック対象の取引先CDリスト</param>
        /// <param name="year">締済チェックの年</param>
        /// <param name="month">締済チェックの月</param>
        /// <param name="zaikoDtoList">在庫用DTOリスト chenzz追加</param>
        /// <returns>True：指定取引先全ての締済データが存在する　False：締済データが存在しない</returns>
        private bool CheckShimezumiData(List<string> torihikisakiCdList, int year, int month, GETSUJI_SHORI_TYPE shoriType = GETSUJI_SHORI_TYPE.BOTH)
        {
            bool val = true;

            // チェックデータ取得用Where句作成
            StringBuilder whereString = new StringBuilder();
            whereString.Append("WHERE DELETE_FLG = 0 AND TORIHIKISAKI_CD IN (");
            for (int i = 0; i < torihikisakiCdList.Count; i++)
            {
                string torihikisakiCd = torihikisakiCdList[i];

                if (i != 0)
                {
                    whereString.Append(",");
                }

                whereString.Append("'");
                whereString.Append(torihikisakiCd);
                whereString.Append("'");
            }
            whereString.Append(")");
            whereString.Append(" AND YEAR = ");
            whereString.Append(year);
            whereString.Append(" AND MONTH = ");
            whereString.Append(month);

            // データ取得
            // 20150715 分別処理に応じて、片方でチェックするように Start
            if ((shoriType & GETSUJI_SHORI_TYPE.SEIKYUU) == GETSUJI_SHORI_TYPE.SEIKYUU)
            {
                DataTable urData = this.dao.GetMonthlyLockUrCheckData(whereString.ToString());
                if (urData == null || urData.Rows.Count == 0)
                {
                    val = false;
                }
            }
            if ((shoriType & GETSUJI_SHORI_TYPE.SHIHARAI) == GETSUJI_SHORI_TYPE.SHIHARAI)
            {
                DataTable shData = this.dao.GetMonthlyLockShCheckData(whereString.ToString());
                if (shData == null || shData.Rows.Count == 0)
                {
                    val = false;
                }
            }
            // 20150715 分別処理に応じて、片方でチェックするように Start

            return val;
        }

        /// <summary>
        /// 指定年月在庫の締済データがあるかチェックを行います
        /// </summary>
        /// <param name="torihikisakiCdList">チェック対象のDTOリスト</param>
        /// <param name="year">締済チェックの年</param>
        /// <param name="month">締済チェックの月</param>
        /// <param name="zaikoDtoList">在庫用DTOリスト chenzz追加</param>
        /// <returns>True：指定取引先全ての締済データが存在する　False：締済データが存在しない</returns>
        private bool CheckZaikoShimezumiData(List<GetusjiShoriZaikoDTOClass> zaikoDtoList, int year, int month)
        {
            bool val = true;
            if (this.SysInfo.ZAIKO_KANRI == 1)
            {
                // チェックデータ取得用Where句作成
                StringBuilder whereString = new StringBuilder();
                whereString.Append("WHERE DELETE_FLG = 0 ");
                if (zaikoDtoList.Count > 0)
                {
                    whereString.Append("AND (");
                }
                for (int i = 0; i < zaikoDtoList.Count; i++)
                {
                    whereString.Append("(GYOUSHA_CD = '");
                    whereString.Append(zaikoDtoList[i].GYOUSHA_CD);
                    whereString.Append("' AND GENBA_CD = '");
                    whereString.Append(zaikoDtoList[i].GENBA_CD);
                    whereString.Append("' AND ZAIKO_HINMEI_CD = '");
                    whereString.Append(zaikoDtoList[i].ZAIKO_HINMEI_CD);
                    whereString.Append("')");
                    if (i != zaikoDtoList.Count - 1)
                    {
                        whereString.Append("OR ");
                    }

                }
                if (zaikoDtoList.Count > 0)
                {
                    whereString.Append(") ");
                }

                whereString.Append(" AND YEAR = ");
                whereString.Append(year);
                whereString.Append(" AND MONTH = ");
                whereString.Append(month);

                // データ取得
                DataTable zaikoData = this.dao.GetMonthlyLockZaikoCheckData(whereString.ToString());

                // 売上・支払の片方だけないのは仕様上ありえないが念のため
                if (zaikoData == null || zaikoData.Rows.Count == 0)
                {
                    val = false;
                }
            }

            return val;
        }

        #endregion

        #region 月次処理中テーブル更新処理

        /// <summary>
        /// 月次処理中テーブルに処理中データを作成します
        /// </summary>
        /// <param name="year">現在実行している月次処理の年</param>
        /// <param name="month">現在実行している月次処理の月</param>
        public void InsertGetsujiShoriChu(int year, int month)
        {
            LogUtility.DebugMethodStart(year, month);

            /* 登録用データ作成 */
            T_GETSUJI_SHORI_CHU data = new T_GETSUJI_SHORI_CHU();
            data.YEAR = short.Parse(year.ToString());
            data.MONTH = short.Parse(month.ToString());
            // WHOカラム設定
            var dataBind_seisanDetailEntityAdd = new DataBinderLogic<T_GETSUJI_SHORI_CHU>(data);
            dataBind_seisanDetailEntityAdd.SetSystemProperty(data, false);
            GetEnvironmentInfoClass environment = new GetEnvironmentInfoClass();
            var name = environment.GetComputerAndUserName();
            // クライアントコンピュータ名
            data.CLIENT_COMPUTER_NAME = name.Item1;
            // クライアントユーザー名
            data.CLIENT_USER_NAME = name.Item2;

            using (Transaction tran = new Transaction())
            {
                // Insert
                this.getsujiShoriChuDao.Insert(data);
                tran.Commit();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 月次処理中テーブルのデータを削除します
        /// </summary>
        /// <param name="year">現在実行している月次処理の年</param>
        /// <param name="month">現在実行している月次処理の月</param>
        public void DeleteGetsujiShoriChu(int year, int month)
        {
            LogUtility.DebugMethodStart(year, month);

            // 削除用データ生成
            T_GETSUJI_SHORI_CHU data = new T_GETSUJI_SHORI_CHU();
            data.YEAR = short.Parse(year.ToString());
            data.MONTH = short.Parse(month.ToString());

            // データがあるかSELECT
            DataTable dt = this.getsujiShoriChuDao.GetDataByKey(year, month);

            if (dt != null && dt.Rows.Count > 0)
            {
                // データがある場合TIME_STAMP設定
                data.TIME_STAMP = (byte[])dt.Rows[0]["TIME_STAMP"];

                using (Transaction tran = new Transaction())
                {
                    // Delete
                    this.getsujiShoriChuDao.Delete(data);
                    tran.Commit();
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 繰越額の取得

        #region 売上

        /// <summary>
        /// 指定した年月で売上月次処理の繰越額を取得します
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        /// <returns>前回締データから取得：True 前回データ無し：False</returns>
        private bool GetUriageKurikosigaku(string torihikisakiCd, int year, int month, ref decimal zandaka)
        {
            bool val = false;

            DataTable dt = this.dao.GetUriageZandaka(torihikisakiCd, year, month);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CHOUSEI_ZANDAKA"] != null && !string.IsNullOrEmpty(dt.Rows[0]["CHOUSEI_ZANDAKA"].ToString()))
                {
                    // 調整後残高が優先
                    zandaka = decimal.Parse(dt.Rows[0]["CHOUSEI_ZANDAKA"].ToString());
                }
                else
                {
                    // 調整前残高
                    zandaka = decimal.Parse(dt.Rows[0]["ZANDAKA"].ToString());
                }
                val = true;
            }

            return val;
        }

        /// <summary>
        /// 過去伝票を締めてた月次データから繰越額を取得します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private decimal GetUriageKurikosigakuFromPastGetsujiData(string torihikisakiCd, int year, int month)
        {
            // 本来締める年月
            DateTime shimeDate = new DateTime(year, month, 1);
            // 過去の伝票を締める年月のTo
            string toDate = shimeDate.AddDays(-1).ToString("yyyy/MM/dd");

            /* 過去の伝票を締める年月Fromを調べる */
            int latestGetsujiShoriYear = 0;
            int latestGetsujiShoriMonth = 0;
            string fromDate = string.Empty;
            // 月次年月を降順ソートした売上月次処理データを取得
            DataTable latestGetsujiData = this.dao.GetLatestGetsujiDateByTorihikisakiCd(torihikisakiCd, shimeDate.ToString("yyyy/MM/dd"));
            if (latestGetsujiData != null && latestGetsujiData.Rows.Count > 0)
            {
                // 過去の月次データがある場合、過去の最新年月 + 1ヶ月 が締める年月のFrom
                latestGetsujiShoriYear = int.Parse(latestGetsujiData.Rows[0]["YEAR"].ToString());
                latestGetsujiShoriMonth = int.Parse(latestGetsujiData.Rows[0]["MONTH"].ToString());
                DateTime tmpDate = new DateTime(latestGetsujiShoriYear, latestGetsujiShoriMonth, 1);
                tmpDate = tmpDate.AddMonths(1);
                fromDate = tmpDate.ToString("yyyy/MM/dd");
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                /* 過去月次データが有る場合は、本来締める年月より前を全て合算締めて残高を算出 */

                // 最新月次処理データの残高
                decimal zandaka = 0;
                this.GetUriageKurikosigaku(torihikisakiCd, latestGetsujiShoriYear, latestGetsujiShoriMonth, ref zandaka);

                // 月次処理用DTO作成
                GetsujiShoriDTOClass dto = new GetsujiShoriDTOClass();
                dto.TORIHIKISAKI_CD = torihikisakiCd;
                dto.PREVIOUS_MONTH_BALANCE = zandaka;
                dto.FROM_DATE = fromDate;
                dto.TO_DATE = toDate;

                // 月次処理開始
                this.ExecUriageGetsujiShori(dto);

                // 月次処理結果から残高を取得
                string key = DateTime.Parse(toDate).ToString("yyyy/MM");
                List<T_MONTHLY_LOCK_UR> dataList = this.MonthlyLockUrDatas[key];
                T_MONTHLY_LOCK_UR data = dataList.Find(delegate(T_MONTHLY_LOCK_UR x) { return x.TORIHIKISAKI_CD.Equals(torihikisakiCd); });
                decimal kurikosiZandaka = decimal.Parse(data.ZANDAKA.ToString());
                return kurikosiZandaka;
            }
            else
            {
                /* 過去月次データが無い場合は取引先の開始残高を使用して過去分の伝票を全て合算した締め処理を行い本来の残高を算出する */

                // 取引先_請求取得
                //M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                // 取引先_請求情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
                M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData;
                if (this.TorihikisakiSeikyuList != null && this.TorihikisakiSeikyuList.Count > 0)
                {
                    torihikisakiSeikyuuData = this.TorihikisakiSeikyuList.Find(delegate(M_TORIHIKISAKI_SEIKYUU m) { return m.TORIHIKISAKI_CD.Equals(torihikisakiCd); });

                    // Listに無いというのはありえないが念のため回避用
                    if (torihikisakiSeikyuuData == null)
                    {
                        torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                    }
                }
                else
                {
                    torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                }

                decimal zandaka = 0;
                if (!torihikisakiSeikyuuData.KAISHI_URIKAKE_ZANDAKA.IsNull)
                {
                    zandaka = torihikisakiSeikyuuData.KAISHI_URIKAKE_ZANDAKA.Value;
                }

                // 月次処理用DTO作成
                GetsujiShoriDTOClass dto = new GetsujiShoriDTOClass();
                dto.TORIHIKISAKI_CD = torihikisakiCd;
                dto.PREVIOUS_MONTH_BALANCE = zandaka;
                dto.TO_DATE = toDate;

                // 月次処理開始
                this.ExecUriageGetsujiShori(dto);

                // 月次処理結果から残高を取得
                string key = DateTime.Parse(toDate).ToString("yyyy/MM");

                List<T_MONTHLY_LOCK_UR> dataList = this.MonthlyLockUrDatas[key];
                T_MONTHLY_LOCK_UR data = dataList.Find(delegate(T_MONTHLY_LOCK_UR x) { return x.TORIHIKISAKI_CD.Equals(torihikisakiCd); });
                decimal kurikosiZandaka = 0;
                if (data != null && !data.ZANDAKA.IsNull)
                {
                    kurikosiZandaka = decimal.Parse(data.ZANDAKA.ToString());
                }
                return kurikosiZandaka;
            }
        }

        #endregion

        #region 支払

        /// <summary>
        /// 指定した年月で支払月次処理の繰越額を取得します
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        /// <returns>前回締データから取得：True 前回データ無し：False</returns>
        private bool GetShiharaiKurikosigaku(string torihikisakiCd, int year, int month, ref decimal zandaka)
        {
            bool val = false;

            DataTable dt = this.dao.GetShiharaiZandaka(torihikisakiCd, year, month);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CHOUSEI_ZANDAKA"] != null && !string.IsNullOrEmpty(dt.Rows[0]["CHOUSEI_ZANDAKA"].ToString()))
                {
                    // 調整後残高が優先
                    zandaka = decimal.Parse(dt.Rows[0]["CHOUSEI_ZANDAKA"].ToString());
                }
                else
                {
                    // 調整前残高
                    zandaka = decimal.Parse(dt.Rows[0]["ZANDAKA"].ToString());
                }
                val = true;
            }

            return val;
        }

        /// <summary>
        /// 過去伝票を締めてた月次データから繰越額を取得します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private decimal GetShiharaiKurikosigakuFromPastGetsujiData(string torihikisakiCd, int year, int month)
        {
            // 本来締める年月
            DateTime shimeDate = new DateTime(year, month, 1);
            // 過去の伝票を締める年月のTo
            string toDate = shimeDate.AddDays(-1).ToString("yyyy/MM/dd");

            /* 過去の伝票を締める年月Fromを調べる */
            int latestGetsujiShoriYear = 0;
            int latestGetsujiShoriMonth = 0;
            string fromDate = string.Empty;
            // 月次年月を降順ソートした売上月次処理データを取得
            DataTable latestGetsujiData = this.dao.GetLatestGetsujiDateByTorihikisakiCd(torihikisakiCd, shimeDate.ToString("yyyy/MM/dd"));
            if (latestGetsujiData != null && latestGetsujiData.Rows.Count > 0)
            {
                // 過去の月次データがある場合、過去の最新年月 + 1ヶ月 が締める年月のFrom
                latestGetsujiShoriYear = int.Parse(latestGetsujiData.Rows[0]["YEAR"].ToString());
                latestGetsujiShoriMonth = int.Parse(latestGetsujiData.Rows[0]["MONTH"].ToString());
                DateTime tmpDate = new DateTime(latestGetsujiShoriYear, latestGetsujiShoriMonth, 1);
                tmpDate = tmpDate.AddMonths(1);
                fromDate = tmpDate.ToString("yyyy/MM/dd");
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                /* 過去月次データが有る場合は、本来締める年月より前を全て合算締めて残高を算出 */

                // 最新月次処理データの残高
                decimal zandaka = 0;
                this.GetShiharaiKurikosigaku(torihikisakiCd, latestGetsujiShoriYear, latestGetsujiShoriMonth, ref zandaka);

                // 月次処理用DTO作成
                GetsujiShoriDTOClass dto = new GetsujiShoriDTOClass();
                dto.TORIHIKISAKI_CD = torihikisakiCd;
                dto.PREVIOUS_MONTH_BALANCE = zandaka;
                dto.FROM_DATE = fromDate;
                dto.TO_DATE = toDate;

                // 月次処理開始
                this.ExecShiharaiGetsujiShori(dto);

                // 月次処理結果から残高を取得
                string key = DateTime.Parse(toDate).ToString("yyyy/MM");
                List<T_MONTHLY_LOCK_SH> dataList = this.MonthlyLockShDatas[key];
                T_MONTHLY_LOCK_SH data = dataList.Find(delegate(T_MONTHLY_LOCK_SH x) { return x.TORIHIKISAKI_CD.Equals(torihikisakiCd); });
                decimal kurikosiZandaka = decimal.Parse(data.ZANDAKA.ToString());
                return kurikosiZandaka;
            }
            else
            {
                /* 過去月次データが無い場合は取引先の開始残高を使用して過去分の伝票を全て合算した締め処理を行い本来の残高を算出する */

                // 取引先_支払取得
                //M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
                // 取引先_支払情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
                M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData;
                if (this.TorihikisakiShiharaiList != null && this.TorihikisakiShiharaiList.Count > 0)
                {
                    torihikisakiShiharaiData = this.TorihikisakiShiharaiList.Find(delegate(M_TORIHIKISAKI_SHIHARAI m) { return m.TORIHIKISAKI_CD.Equals(torihikisakiCd); });

                    // Listに無いというのはありえないが念のため回避用
                    if (torihikisakiShiharaiData == null)
                    {
                        torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
                    }
                }
                else
                {
                    torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
                }
                decimal zandaka = 0;
                if (!torihikisakiShiharaiData.KAISHI_KAIKAKE_ZANDAKA.IsNull)
                {
                    zandaka = torihikisakiShiharaiData.KAISHI_KAIKAKE_ZANDAKA.Value;
                }

                // 月次処理用DTO作成
                GetsujiShoriDTOClass dto = new GetsujiShoriDTOClass();
                dto.TORIHIKISAKI_CD = torihikisakiCd;
                dto.PREVIOUS_MONTH_BALANCE = zandaka;
                dto.TO_DATE = toDate;

                // 月次処理開始
                this.ExecShiharaiGetsujiShori(dto);

                // 月次処理結果から残高を取得
                string key = DateTime.Parse(toDate).ToString("yyyy/MM");
                List<T_MONTHLY_LOCK_SH> dataList = this.MonthlyLockShDatas[key];
                T_MONTHLY_LOCK_SH data = dataList.Find(delegate(T_MONTHLY_LOCK_SH x) { return x.TORIHIKISAKI_CD.Equals(torihikisakiCd); });

                decimal kurikosiZandaka = 0;
                if (data != null && !data.ZANDAKA.IsNull)
                {
                    kurikosiZandaka = decimal.Parse(data.ZANDAKA.ToString());
                }
                return kurikosiZandaka;
            }
        }

        #endregion

        #endregion

        #region 月次処理開始

        #region 売上月次処理

        /// <summary>
        /// 月次処理(売上)を実行します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="zandaka"></param>
        private void ExecUriageGetsujiShori(GetsujiShoriDTOClass uriageDto)
        {
            // 売上データ取得
            DataTable uriageData = this.GetUriageData(uriageDto.TORIHIKISAKI_CD, uriageDto.FROM_DATE, uriageDto.TO_DATE);
            // 入金データ取得
            DataTable nyuukinData = this.GetNyuukinData(uriageDto.TORIHIKISAKI_CD, uriageDto.FROM_DATE, uriageDto.TO_DATE);
            // 取引先請求情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
            M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData;
            if (this.TorihikisakiSeikyuList != null && this.TorihikisakiSeikyuList.Count > 0)
            {
                torihikisakiSeikyuuData = this.TorihikisakiSeikyuList.Find(delegate(M_TORIHIKISAKI_SEIKYUU data) { return data.TORIHIKISAKI_CD.Equals(uriageDto.TORIHIKISAKI_CD); });

                // Listに無いというのはありえないが念のため回避用
                if (torihikisakiSeikyuuData == null)
                {
                    torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(uriageDto.TORIHIKISAKI_CD);
                }
            }
            else
            {
                torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(uriageDto.TORIHIKISAKI_CD);
            }

            // 売上データ作成開始
            CreateUriageGetsujiData(uriageDto, uriageData, nyuukinData, torihikisakiSeikyuuData);
        }

        #endregion

        #region 支払月次処理開始

        /// <summary>
        /// 月次処理(支払)を実行します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="zandaka"></param>
        private void ExecShiharaiGetsujiShori(GetsujiShoriDTOClass shiharaiDto)
        {
            // 支払データ取得
            DataTable shiharaiData = this.GetShiharaiData(shiharaiDto.TORIHIKISAKI_CD, shiharaiDto.FROM_DATE, shiharaiDto.TO_DATE);
            // 出金データ取得
            DataTable shukkinData = this.GetShukkinData(shiharaiDto.TORIHIKISAKI_CD, shiharaiDto.FROM_DATE, shiharaiDto.TO_DATE);
            // 取引先支払情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
            M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData;
            if (this.TorihikisakiShiharaiList != null && this.TorihikisakiShiharaiList.Count > 0)
            {
                torihikisakiShiharaiData = this.TorihikisakiShiharaiList.Find(delegate(M_TORIHIKISAKI_SHIHARAI data) { return data.TORIHIKISAKI_CD.Equals(shiharaiDto.TORIHIKISAKI_CD); });

                // Listに無いというのはありえないが念のため回避用
                if (torihikisakiShiharaiData == null)
                {
                    torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(shiharaiDto.TORIHIKISAKI_CD);
                }
            }
            else
            {
                torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(shiharaiDto.TORIHIKISAKI_CD);
            }

            // 支払月次データ作成開始
            CreateShiharaiGetsujiData(shiharaiDto, shiharaiData, shukkinData, torihikisakiShiharaiData);
        }

        #endregion

        #endregion

        #region 取引先マスタ取得

        /// <summary>
        /// 取引先マスタから削除されているものを含む全てのデータを取得します
        /// </summary>
        /// <returns>取引先マスタEntityの配列</returns>
        private M_TORIHIKISAKI[] GetTorihikisaki()
        {
            M_TORIHIKISAKI[] datas = this.torihikisakiDao.GetAllData();
            return datas;
        }

        #endregion

        #region 伝票(受入、出荷、売上/支払)データ取得

        #region 売上

        /// <summary>
        /// 受入、出荷、売上/支払伝票の売上データを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="fromDate">[任意]取得する期間FROM</param>
        /// <param name="toDate">取得する期間TO</param>
        /// <returns></returns>
        private DataTable GetUriageData(string torihikisakiCd, string fromDate, string toDate)
        {
            DataTable uriageData;

            // 受入データ取得
            DataTable ukeireData = this.dao.GetUriageData(2, torihikisakiCd, fromDate, toDate);
            ukeireData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in ukeireData.Rows)
            {
                row["DENPYOUSHURUI"] = 1;
            }

            // 出荷データ取得
            DataTable shukkaData = this.dao.GetUriageData(3, torihikisakiCd, fromDate, toDate);
            shukkaData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in shukkaData.Rows)
            {
                row["DENPYOUSHURUI"] = 2;
            }

            // 売上/支払データ取得
            DataTable uriageShiharaiData = this.dao.GetUriageData(4, torihikisakiCd, fromDate, toDate);
            uriageShiharaiData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in uriageShiharaiData.Rows)
            {
                row["DENPYOUSHURUI"] = 3;
            }

            // ソート用DataTable
            DataTable sorturiageData = new DataTable();

            //データコピー
            if (ukeireData != null)
            {
                sorturiageData = ukeireData.Clone();
            }
            else if (shukkaData != null)
            {
                sorturiageData = shukkaData.Clone();
            }
            else if (uriageShiharaiData != null)
            {
                sorturiageData = uriageShiharaiData.Clone();
            }

            if (ukeireData != null)
            {
                foreach (DataRow r in ukeireData.Rows)
                {
                    sorturiageData.ImportRow(r);
                }
            }

            if (shukkaData != null)
            {
                foreach (DataRow r in shukkaData.Rows)
                {
                    sorturiageData.ImportRow(r);
                }
            }

            if (uriageShiharaiData != null)
            {
                foreach (DataRow r in uriageShiharaiData.Rows)
                {
                    sorturiageData.ImportRow(r);
                }
            }

            DataView dv = new DataView(sorturiageData);
            uriageData = sorturiageData.Clone();
            dv.Sort = "TORIHIKISAKI_CD,GYOUSHA_CD,GENBA_CD";
            foreach (DataRowView drv in dv)
            {
                uriageData.ImportRow(drv.Row);
            }

            return uriageData;
        }

        #endregion

        #region 支払

        /// <summary>
        /// 受入、出荷、売上/支払伝票の支払データを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="fromDate">[任意]取得する期間FROM</param>
        /// <param name="toDate">取得する期間TO</param>
        /// <returns></returns>
        private DataTable GetShiharaiData(string torihikisakiCd, string fromDate, string toDate)
        {
            DataTable shiharaiData;

            // 受入データ取得
            DataTable ukeireData = this.dao.GetShiharaiData(2, torihikisakiCd, fromDate, toDate);
            ukeireData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in ukeireData.Rows)
            {
                row["DENPYOUSHURUI"] = 1;
            }

            // 出荷データ取得
            DataTable shukkaData = this.dao.GetShiharaiData(3, torihikisakiCd, fromDate, toDate);
            shukkaData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in shukkaData.Rows)
            {
                row["DENPYOUSHURUI"] = 2;
            }

            // 売上/支払データ取得
            DataTable uriageShiharaiData = this.dao.GetShiharaiData(4, torihikisakiCd, fromDate, toDate);
            uriageShiharaiData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in uriageShiharaiData.Rows)
            {
                row["DENPYOUSHURUI"] = 3;
            }

            // ソート用DataTable
            DataTable sortShiharaiData = new DataTable();

            //データコピー
            if (ukeireData != null)
            {
                sortShiharaiData = ukeireData.Clone();
            }
            else if (shukkaData != null)
            {
                sortShiharaiData = shukkaData.Clone();
            }
            else if (uriageShiharaiData != null)
            {
                sortShiharaiData = uriageShiharaiData.Clone();
            }

            if (ukeireData != null)
            {
                foreach (DataRow r in ukeireData.Rows)
                {
                    sortShiharaiData.ImportRow(r);
                }
            }

            if (shukkaData != null)
            {
                foreach (DataRow r in shukkaData.Rows)
                {
                    sortShiharaiData.ImportRow(r);
                }
            }

            if (uriageShiharaiData != null)
            {
                foreach (DataRow r in uriageShiharaiData.Rows)
                {
                    sortShiharaiData.ImportRow(r);
                }
            }

            DataView dv = new DataView(sortShiharaiData);
            shiharaiData = sortShiharaiData.Clone();
            dv.Sort = "TORIHIKISAKI_CD,GYOUSHA_CD,GENBA_CD";
            foreach (DataRowView drv in dv)
            {
                shiharaiData.ImportRow(drv.Row);
            }

            return shiharaiData;
        }

        #endregion

        #endregion

        #region 入・出金データ取得

        #region 入金

        /// <summary>
        /// 出金データを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="fromDate">[任意]取得する期間FROM</param>
        /// <param name="toDate">取得する期間TO</param>
        /// <returns></returns>
        private DataTable GetNyuukinData(string torihikisakiCd, string fromDate, string toDate)
        {
            // 出金データ取得
            DataTable nyuukinData = this.dao.GetNyuukinData(torihikisakiCd, fromDate, toDate);
            return nyuukinData;
        }

        #endregion

        #region 出金

        /// <summary>
        /// 出金データを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="fromDate">[任意]取得する期間FROM</param>
        /// <param name="toDate">取得する期間TO</param>
        /// <returns></returns>
        private DataTable GetShukkinData(string torihikisakiCd, string fromDate, string toDate)
        {
            // 出金データ取得
            DataTable shukkinData = this.dao.GetShukkinData(torihikisakiCd, fromDate, toDate);
            return shukkinData;
        }

        #endregion

        #endregion

        #region 売上月次データ作成

        /// <summary>
        /// 売上月次データ作成
        /// </summary>
        /// <param name="uriageDto"></param>
        /// <param name="uriageData"></param>
        /// <param name="nyuukinData"></param>
        /// <param name="torihikisakiSeikyuuData"></param>
        /// <returns></returns>
        private void CreateUriageGetsujiData(GetsujiShoriDTOClass uriageDto, DataTable uriageData,
                                             DataTable nyuukinData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData)
        {
            switch (torihikisakiSeikyuuData.SHOSHIKI_KBN.ToString())
            {
                case "1":
                    /* 取引先別 */

                    //集計金額を初期化
                    //今回入金額
                    konkaiNyuukingaku = 0;
                    //今回調整額	
                    konkaiChouseigaku = 0;
                    //今回売上額	
                    konkaiUriagegaku = 0;
                    //今回請内税額	
                    konkaiSeiUtizeigaku = 0;
                    //今回請外税額	
                    konkaiSeisotozeigaku = 0;
                    //今回伝内税額	
                    konkaiDenUtizeigaku = 0;
                    //今回伝外税額	
                    konkaiDensotozeigaku = 0;
                    //今回明内税額	
                    konkaiMeiUtizeigaku = 0;
                    //今回明外税額	
                    konkaiMeisotozeigaku = 0;
                    //今回御請求額	
                    konkaiSeikyuugaku = 0;

                    // 売上月次データ作成
                    this.CreateUriageData(uriageDto, uriageData, nyuukinData, torihikisakiSeikyuuData);

                    break;

                case "2":
                    /* 業者別 */

                    //集計金額を初期化
                    //今回入金額
                    konkaiNyuukingaku = 0;
                    //今回調整額	
                    konkaiChouseigaku = 0;
                    //今回売上額	
                    konkaiUriagegaku = 0;
                    //今回請内税額	
                    konkaiSeiUtizeigaku = 0;
                    //今回請外税額	
                    konkaiSeisotozeigaku = 0;
                    //今回伝内税額	
                    konkaiDenUtizeigaku = 0;
                    //今回伝外税額	
                    konkaiDensotozeigaku = 0;
                    //今回明内税額	
                    konkaiMeiUtizeigaku = 0;
                    //今回明外税額	
                    konkaiMeisotozeigaku = 0;
                    //今回御請求額	
                    konkaiSeikyuugaku = 0;

                    // 売上月次データ作成
                    this.CreateUriageData(uriageDto, uriageData, nyuukinData, torihikisakiSeikyuuData);

                    break;

                case "3":
                    /* 現場別 */

                    //集計金額を初期化
                    //今回入金額
                    konkaiNyuukingaku = 0;
                    //今回調整額	
                    konkaiChouseigaku = 0;
                    //今回売上額	
                    konkaiUriagegaku = 0;
                    //今回請内税額	
                    konkaiSeiUtizeigaku = 0;
                    //今回請外税額	
                    konkaiSeisotozeigaku = 0;
                    //今回伝内税額	
                    konkaiDenUtizeigaku = 0;
                    //今回伝外税額	
                    konkaiDensotozeigaku = 0;
                    //今回明内税額	
                    konkaiMeiUtizeigaku = 0;
                    //今回明外税額	
                    konkaiMeisotozeigaku = 0;
                    //今回御請求額	
                    konkaiSeikyuugaku = 0;

                    // 売上月次データ作成
                    this.CreateUriageData(uriageDto, uriageData, nyuukinData, torihikisakiSeikyuuData);

                    break;
            }
        }

        /// <summary>
        /// 売上月次処理データ作成
        /// </summary>
        /// <param name="uriageDto"></param>
        /// <param name="uriageData"></param>
        /// <param name="nyuukinData"></param>
        /// <param name="torihikisakiSeikyuuData"></param>
        private void CreateUriageData(GetsujiShoriDTOClass uriageDto, DataTable uriageData, DataTable nyuukinData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData)
        {
            //前回繰越額
            decimal zenkaigaku = uriageDto.PREVIOUS_MONTH_BALANCE;

            // 足しこみ用TMP金額変数
            decimal tmpKonkaiUriagegaku = 0;
            decimal tmpKonkaiSeiUtizeigaku = 0;
            decimal tmpKonkaiSeisotozeigaku = 0;
            decimal tmpKonkaiDenUtizeigaku = 0;
            decimal tmpKonkaiDensotozeigaku = 0;
            decimal tmpKonkaiMeiUtizeigaku = 0;
            decimal tmpKonkaiMeisotozeigaku = 0;
            decimal tmpKonkaiSeikyuugaku = 0;
            string gyoushaCd = "";
            string genbaCd = "";

            // 書式区分別に売上データから集計
            switch (torihikisakiSeikyuuData.SHOSHIKI_KBN.ToString())
            {
                case "1":
                    // 請求先別
                    SumNyuukinUriageData(zenkaigaku, uriageData, nyuukinData, torihikisakiSeikyuuData);

                    break;

                case "2":
                    // 業者別
                    SumNyuukinData(nyuukinData);

                    for (int i = 0; i < uriageData.Rows.Count; i++)
                    {
                        if (gyoushaCd != uriageData.Rows[i]["GYOUSHA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = uriageData.Rows[i]["GYOUSHA_CD"].ToString();

                            SumUriageData("2", gyoushaCd, "", uriageData, torihikisakiSeikyuuData);

                            tmpKonkaiUriagegaku += konkaiUriagegaku;
                            tmpKonkaiSeiUtizeigaku += konkaiSeiUtizeigaku;
                            tmpKonkaiSeisotozeigaku += konkaiSeisotozeigaku;
                            tmpKonkaiDenUtizeigaku += konkaiDenUtizeigaku;
                            tmpKonkaiDensotozeigaku += konkaiDensotozeigaku;
                            tmpKonkaiMeiUtizeigaku += konkaiMeiUtizeigaku;
                            tmpKonkaiMeisotozeigaku += konkaiMeisotozeigaku;
                            tmpKonkaiSeikyuugaku += konkaiSeikyuugaku;

                            konkaiUriagegaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                            konkaiSeikyuugaku = 0;
                        }
                    }

                    konkaiUriagegaku = tmpKonkaiUriagegaku;
                    konkaiSeiUtizeigaku = tmpKonkaiSeiUtizeigaku;
                    konkaiSeisotozeigaku = tmpKonkaiSeisotozeigaku;
                    konkaiDenUtizeigaku = tmpKonkaiDenUtizeigaku;
                    konkaiDensotozeigaku = tmpKonkaiDensotozeigaku;
                    konkaiMeiUtizeigaku = tmpKonkaiMeiUtizeigaku;
                    konkaiMeisotozeigaku = tmpKonkaiMeisotozeigaku;
                    konkaiSeikyuugaku = zenkaigaku + tmpKonkaiSeikyuugaku - konkaiNyuukingaku - konkaiChouseigaku;

                    break;

                case "3":
                    // 現場別
                    SumNyuukinData(nyuukinData);

                    for (int i = 0; i < uriageData.Rows.Count; i++)
                    {
                        if (gyoushaCd != uriageData.Rows[i]["GYOUSHA_CD"].ToString() || genbaCd != uriageData.Rows[i]["GENBA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = uriageData.Rows[i]["GYOUSHA_CD"].ToString();
                            genbaCd = uriageData.Rows[i]["GENBA_CD"].ToString();

                            SumUriageData("3", gyoushaCd, genbaCd, uriageData, torihikisakiSeikyuuData);

                            tmpKonkaiUriagegaku += konkaiUriagegaku;
                            tmpKonkaiSeiUtizeigaku += konkaiSeiUtizeigaku;
                            tmpKonkaiSeisotozeigaku += konkaiSeisotozeigaku;
                            tmpKonkaiDenUtizeigaku += konkaiDenUtizeigaku;
                            tmpKonkaiDensotozeigaku += konkaiDensotozeigaku;
                            tmpKonkaiMeiUtizeigaku += konkaiMeiUtizeigaku;
                            tmpKonkaiMeisotozeigaku += konkaiMeisotozeigaku;
                            tmpKonkaiSeikyuugaku += konkaiSeikyuugaku;

                            konkaiUriagegaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                            konkaiSeikyuugaku = 0;
                        }
                    }
                    konkaiUriagegaku = tmpKonkaiUriagegaku;
                    konkaiSeiUtizeigaku = tmpKonkaiSeiUtizeigaku;
                    konkaiSeisotozeigaku = tmpKonkaiSeisotozeigaku;
                    konkaiDenUtizeigaku = tmpKonkaiDenUtizeigaku;
                    konkaiDensotozeigaku = tmpKonkaiDensotozeigaku;
                    konkaiMeiUtizeigaku = tmpKonkaiMeiUtizeigaku;
                    konkaiMeisotozeigaku = tmpKonkaiMeisotozeigaku;
                    konkaiSeikyuugaku = zenkaigaku + tmpKonkaiSeikyuugaku - konkaiNyuukingaku - konkaiChouseigaku;

                    break;
                default:
                    break;
            }

            T_MONTHLY_LOCK_UR monthlyLockUrData = new T_MONTHLY_LOCK_UR();
            //取引先
            monthlyLockUrData.TORIHIKISAKI_CD = uriageDto.TORIHIKISAKI_CD;
            DateTime date = DateTime.Parse(uriageDto.TO_DATE);
            //年
            monthlyLockUrData.YEAR = short.Parse(date.Year.ToString());
            //月
            monthlyLockUrData.MONTH = short.Parse(date.Month.ToString());
            //繰越残高
            monthlyLockUrData.PREVIOUS_MONTH_BALANCE = zenkaigaku;
            //入金額
            monthlyLockUrData.NYUUKIN_KINGAKU = SqlDecimal.Parse((konkaiNyuukingaku + konkaiChouseigaku).ToString());
            //税抜金額
            monthlyLockUrData.KINGAKU = SqlDecimal.Parse(konkaiUriagegaku.ToString());
            //消費税額
            monthlyLockUrData.TAX = SqlDecimal.Parse((konkaiSeiUtizeigaku + konkaiSeisotozeigaku + konkaiDenUtizeigaku
                                                        + konkaiDensotozeigaku + konkaiMeiUtizeigaku + konkaiMeisotozeigaku).ToString());
            //締内税額	
            monthlyLockUrData.SHIME_UTIZEI_GAKU = SqlDecimal.Parse(konkaiSeiUtizeigaku.ToString());
            //締外税額	
            monthlyLockUrData.SHIME_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiSeisotozeigaku.ToString());
            //伝内税額	
            monthlyLockUrData.DEN_UTIZEI_GAKU = SqlDecimal.Parse(konkaiDenUtizeigaku.ToString());
            //伝外税額	
            monthlyLockUrData.DEN_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiDensotozeigaku.ToString());
            //明内税額	
            monthlyLockUrData.MEI_UTIZEI_GAKU = SqlDecimal.Parse(konkaiMeiUtizeigaku.ToString());
            //明外税額	
            monthlyLockUrData.MEI_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiMeisotozeigaku.ToString());
            //税込金額
            monthlyLockUrData.TOTAL_KINGAKU = SqlDecimal.Parse((monthlyLockUrData.KINGAKU + monthlyLockUrData.TAX).ToString());
            //調整前差引残高
            monthlyLockUrData.ZANDAKA = monthlyLockUrData.PREVIOUS_MONTH_BALANCE + monthlyLockUrData.TOTAL_KINGAKU - monthlyLockUrData.NYUUKIN_KINGAKU;
            //適格区分(1.旧 2.適格請求)
            monthlyLockUrData.INVOICE_KBN = LInvoceKBN;

            // 月次処理データを格納
            DateTime keyDate = DateTime.Parse(uriageDto.TO_DATE);
            string keyString = keyDate.ToString("yyyy/MM");
            if (this.MonthlyLockUrDatas.ContainsKey(keyString))
            {
                List<T_MONTHLY_LOCK_UR> dataList = this.MonthlyLockUrDatas[keyString];
                dataList.Add(monthlyLockUrData);
            }
            else
            {
                List<T_MONTHLY_LOCK_UR> dataList = new List<T_MONTHLY_LOCK_UR>();
                dataList.Add(monthlyLockUrData);
                this.MonthlyLockUrDatas.Add(keyString, dataList);
            }
        }

        #region 入金、売上データ集計値作成

        /// <summary>
        /// 入金・売上データ集計値を作成します。
        /// </summary>
        /// <param name="zenkaiGaku"></param>
        /// <param name="uriageData"></param>
        /// <param name="nyuukinData"></param>
        /// <param name="torihikisakiSeikyuuData"></param>
        private void SumNyuukinUriageData(decimal zenkaiGaku, DataTable uriageData, DataTable nyuukinData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData)
        {
            SumNyuukinUriageData(zenkaiGaku, "1", string.Empty, string.Empty, uriageData, nyuukinData, torihikisakiSeikyuuData);
        }

        /// <summary>
        /// 入金・売上データ集計値を作成します。
        /// </summary>
        /// <param name="zenkaiGaku">前回繰越額</param>
        /// <param name="shoshikiKbn">取引先_請求情報マスタ.書式区分</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        private void SumNyuukinUriageData(decimal zenkaiGaku, string shoshikiKbn, string gyoushaCd, string genbaCd,
                                          DataTable uriageData, DataTable nyuukinData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData)
        {
            //保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            //入金データから集計
            for (int i = 0; i < nyuukinData.Rows.Count; i++)
            {
                if (saveSysID != nyuukinData.Rows[i]["SYSTEM_ID"].ToString())
                {
                    //今回入金額
                    if (!string.IsNullOrEmpty(Convert.ToString(nyuukinData.Rows[i]["NYUUKIN_AMOUNT_TOTAL"])))
                    {
                        konkaiNyuukingaku = konkaiNyuukingaku + Decimal.Parse(nyuukinData.Rows[i]["NYUUKIN_AMOUNT_TOTAL"].ToString());
                    }

                    //今回調整額
                    if (!string.IsNullOrEmpty(Convert.ToString(nyuukinData.Rows[i]["CHOUSEI_AMOUNT_TOTAL"])))
                    {
                        konkaiChouseigaku = konkaiChouseigaku + Decimal.Parse(nyuukinData.Rows[i]["CHOUSEI_AMOUNT_TOTAL"].ToString());
                    }

                    //システムＩＤの保存
                    saveSysID = nyuukinData.Rows[i]["SYSTEM_ID"].ToString();
                }
            }

            //保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            //売上データから集計
            if (LInvoceKBN == 2)
            {
                switch (shoshikiKbn)
                {
                    case "1":
                        // 請求先別
                        CreatetSeikyuuUriageData(uriageData, torihikisakiSeikyuuData);
                        break;
                    case "2":
                        // 業者別
                        CreatetGyoushaUriageData(uriageData, torihikisakiSeikyuuData, gyoushaCd);
                        break;
                    case "3":
                        // 現場別
                        CreatetGenbaUriageData(uriageData, torihikisakiSeikyuuData, gyoushaCd, genbaCd);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (shoshikiKbn)
                {
                    case "1":
                        // 請求先別
                        CreatetSeikyuuUriageData_OldInvoice(uriageData, torihikisakiSeikyuuData);
                        break;
                    case "2":
                        // 業者別
                        CreatetGyoushaUriageData_OldInvoice(uriageData, torihikisakiSeikyuuData, gyoushaCd);
                        break;
                    case "3":
                        // 現場別
                        CreatetGenbaUriageData_OldInvoice(uriageData, torihikisakiSeikyuuData, gyoushaCd, genbaCd);
                        break;
                    default:
                        break;
                }
            }

            //今回御請求額
            konkaiSeikyuugaku = zenkaiGaku + konkaiUriagegaku
                              + konkaiSeiUtizeigaku + konkaiSeisotozeigaku
                              + konkaiDenUtizeigaku + konkaiDensotozeigaku
                              + konkaiMeiUtizeigaku + konkaiMeisotozeigaku
                              - konkaiNyuukingaku - konkaiChouseigaku;
        }

        /// <summary>
        /// 入金データ集計値を作成します。
        /// </summary>
        private void SumNyuukinData(DataTable nyuukinData)
        {
            //保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            //入金データから集計
            for (int i = 0; i < nyuukinData.Rows.Count; i++)
            {
                if (saveSysID != nyuukinData.Rows[i]["SYSTEM_ID"].ToString())
                {
                    //今回入金額
                    if (!string.IsNullOrEmpty(Convert.ToString(nyuukinData.Rows[i]["NYUUKIN_AMOUNT_TOTAL"])))
                    {
                        konkaiNyuukingaku = konkaiNyuukingaku + Decimal.Parse(nyuukinData.Rows[i]["NYUUKIN_AMOUNT_TOTAL"].ToString());
                    }

                    //今回調整額
                    if (!string.IsNullOrEmpty(Convert.ToString(nyuukinData.Rows[i]["CHOUSEI_AMOUNT_TOTAL"])))
                    {
                        konkaiChouseigaku = konkaiChouseigaku + Decimal.Parse(nyuukinData.Rows[i]["CHOUSEI_AMOUNT_TOTAL"].ToString());
                    }

                    //システムＩＤの保存
                    saveSysID = nyuukinData.Rows[i]["SYSTEM_ID"].ToString();
                }
            }
        }

        /// <summary>
        /// 売上データ集計値を作成します。(for 業者別、現場別請求)
        /// </summary>
        /// <param name="shoshikiKbn">取引先_請求情報マスタ.書式区分</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        private void SumUriageData(string shoshikiKbn, string gyoushaCd, string genbaCd,
                                   DataTable uriageData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData)
        {
            //保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            //売上データから集計
            if (LInvoceKBN == 2)
            {
                switch (shoshikiKbn)
                {
                    case "1":
                        // 請求先別
                        CreatetSeikyuuUriageData(uriageData, torihikisakiSeikyuuData);
                        break;
                    case "2":
                        // 業者別
                        CreatetGyoushaUriageData(uriageData, torihikisakiSeikyuuData, gyoushaCd);
                        break;
                    case "3":
                        // 現場別
                        CreatetGenbaUriageData(uriageData, torihikisakiSeikyuuData, gyoushaCd, genbaCd);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (shoshikiKbn)
                {
                    case "1":
                        // 請求先別
                        CreatetSeikyuuUriageData_OldInvoice(uriageData, torihikisakiSeikyuuData);
                        break;
                    case "2":
                        // 業者別
                        CreatetGyoushaUriageData_OldInvoice(uriageData, torihikisakiSeikyuuData, gyoushaCd);
                        break;
                    case "3":
                        // 現場別
                        CreatetGenbaUriageData_OldInvoice(uriageData, torihikisakiSeikyuuData, gyoushaCd, genbaCd);
                        break;
                    default:
                        break;
                }
            }

            //今回御請求額(業者別、現場別のため入金系は加算しない)
            konkaiSeikyuugaku = konkaiUriagegaku
                              + konkaiSeiUtizeigaku + konkaiSeisotozeigaku
                              + konkaiDenUtizeigaku + konkaiDensotozeigaku
                              + konkaiMeiUtizeigaku + konkaiMeisotozeigaku;
        }

        #endregion

        #region 支払データ算出

        #region 売上データ算出(請求先別)

        /// <summary>
        /// 売上データ算出(請求先別)
        /// </summary>
        /// <param name="uriageData"></param>
        /// <param name="torihikisakiSeikyuuData"></param>
        private void CreatetSeikyuuUriageData(DataTable uriageData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData)
        {
            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            decimal shohizeirate = 0;
            int count = 0;
            decimal uriageTotalzei = 0;

            // 請求毎税の計算
            DataView dv = new DataView(uriageData);
            DataTable table = uriageData.Clone();
            dv.Sort = "URIAGE_SHOUHIZEI_RATE,URIAGE_ZEI_KEISAN_KBN_CD,URIAGE_ZEI_KBN_CD,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                CreatetUriageData(table.Rows[i]);
            }

            if (uriageData.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //初回
                    if (count == 0)
                    {
                        shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());
                    }

                    if ((decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString()) != shohizeirate))
                    {
                        //消費税計算・変数初期化
                        uriageTotalzei = uriageTotalzei + CreateSeikyuuTaxSoto(uriageTotal, shohizeirate, torihikisakiSeikyuuData);
                        uriageTotal = 0;
                    }

                    if (string.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() == table.Rows[i]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                 Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString())
                        {
                            //請求毎外税
                            uriageTotal = uriageTotal
                                        + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()));
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString())
                        {
                            //明細毎内税
                            uriageTotal = uriageTotal
                                        + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()))
                                        - Decimal.Parse(BlankToZero(table.Rows[i]["TAX_UCHI"].ToString()));
                        }
                    }
                    else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())
                    {
                        //品名内税
                        uriageTotal = uriageTotal
                                        + Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_KINGAKU"].ToString()))
                                        - Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_TAX_UCHI"].ToString()));
                    }

                    //消費税率の保存
                    shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());

                    //カウントアップ
                    count++;
                }
                //消費税計算
                uriageTotalzei = uriageTotalzei + CreateSeikyuuTaxSoto(uriageTotal, shohizeirate, torihikisakiSeikyuuData);
            }

            //今回売上額
            konkaiUriagegaku = konkaiUriagegaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
            //請求毎外税
            konkaiSeisotozeigaku = uriageTotalzei;
            //今回明内税額
            konkaiMeiUtizeigaku = 0;
        }

        #endregion

        #region 売上データ算出(業者別)

        /// <summary>
        /// 売上データ算出(業者別)
        /// </summary>
        private void CreatetGyoushaUriageData(DataTable uriageData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData, string gyoushaCd)
        {
            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            decimal shohizeirate = 0;
            int count = 0;
            decimal uriageTotalzei = 0;

            // 請求毎税の計算
            DataView dv = new DataView(uriageData);
            DataTable table = uriageData.Clone();
            dv.Sort = "URIAGE_SHOUHIZEI_RATE,GYOUSHA_CD,URIAGE_ZEI_KEISAN_KBN_CD,URIAGE_ZEI_KBN_CD,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
                {
                    CreatetUriageData(table.Rows[i]);
                }
            }

            if (uriageData.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
                    {
                        //初回
                        if (count == 0)
                        {
                            shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());
                        }

                        if (decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString()) != shohizeirate)
                        {
                            //消費税計算・変数初期化
                            uriageTotalzei = uriageTotalzei + CreateSeikyuuTaxSoto(uriageTotal, shohizeirate, torihikisakiSeikyuuData);
                            uriageTotal = 0;
                        }

                        if (string.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() == table.Rows[i]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                 Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString())
                            {
                                //請求毎外税
                                uriageTotal = uriageTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()));
                            }
                            else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString())
                            {
                                //明細毎内税
                                uriageTotal = uriageTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()))
                                            - Decimal.Parse(BlankToZero(table.Rows[i]["TAX_UCHI"].ToString()));
                            }
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())
                        {
                            //品名内税
                            uriageTotal = uriageTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_KINGAKU"].ToString()))
                                            - Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_TAX_UCHI"].ToString()));
                        }
                            
                        //消費税率の保存
                        shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());

                        //カウントアップ
                        count++;
                    }
                }
                //消費税計算
                uriageTotalzei = uriageTotalzei + CreateSeikyuuTaxSoto(uriageTotal, shohizeirate, torihikisakiSeikyuuData);
            }

            //今回売上額
            konkaiUriagegaku = konkaiUriagegaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
            //請求毎外税
            konkaiSeisotozeigaku = uriageTotalzei;
            //今回明内税額
            konkaiMeiUtizeigaku = 0;
        }

        #endregion

        #region 売上データ算出(現場別)

        /// <summary>
        /// 売上データ算出(現場別)
        /// </summary>
        private void CreatetGenbaUriageData(DataTable uriageData, M_TORIHIKISAKI_SEIKYUU torihikisakiSEikyuuData, string gyoushaCd, string genbaCd)
        {
            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            decimal shohizeirate = 0;
            int count = 0;
            decimal uriageTotalzei = 0;

            // 請求毎税の計算
            DataView dv = new DataView(uriageData);
            DataTable table = uriageData.Clone();
            dv.Sort = "URIAGE_SHOUHIZEI_RATE,GYOUSHA_CD,GENBA_CD,URIAGE_ZEI_KEISAN_KBN_CD,URIAGE_ZEI_KBN_CD,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && table.Rows[i]["GENBA_CD"].ToString() == genbaCd)
                {
                    CreatetUriageData(table.Rows[i]);
                }
            }

            if (uriageData.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && table.Rows[i]["GENBA_CD"].ToString() == genbaCd)
                    {
                        //初回
                        if (count == 0)
                        {
                            shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());
                        }

                        if (decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString()) != shohizeirate)
                        {
                            //消費税計算・変数初期化
                            uriageTotalzei = uriageTotalzei + CreateSeikyuuTaxSoto(uriageTotal, shohizeirate, torihikisakiSEikyuuData);
                            uriageTotal = 0;
                        }

                        if (string.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() == table.Rows[i]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                 Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString())
                            {
                                //請求毎外税
                                uriageTotal = uriageTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()));
                            }
                            else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString())
                            {
                                //明細毎内税
                                uriageTotal = uriageTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()))
                                            - Decimal.Parse(BlankToZero(table.Rows[i]["TAX_UCHI"].ToString()));
                            }
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())
                        {
                            //品名内税
                            uriageTotal = uriageTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_KINGAKU"].ToString()))
                                            - Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_TAX_UCHI"].ToString()));
                        }

                        //消費税率の保存
                        shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());

                        //カウントアップ
                        count++;
                    }
                }
                //消費税計算
                uriageTotalzei = uriageTotalzei + CreateSeikyuuTaxSoto(uriageTotal, shohizeirate, torihikisakiSEikyuuData);
            }

            //今回売上額
            konkaiUriagegaku = konkaiUriagegaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
            //請求毎外税
            konkaiSeisotozeigaku = uriageTotalzei;
            //今回明内税額
            konkaiMeiUtizeigaku = 0;
        }

        #endregion

        #region 売上データ算出

        /// <summary>
        /// 売上データ算出
        /// </summary>
        private void CreatetUriageData(DataRow row)
        {
            //売上金額合計
            decimal w_UriageTotal = 0;
            //品名別売上金額合計
            decimal w_HinmeiUriageTotal = 0;
            //売上伝票毎内税
            decimal w_UriageDenUti = 0;
            //売上伝票毎外税	
            decimal w_UriageDenSoto = 0;
            //売上明細毎内税
            decimal w_UriageMeiUti = 0;
            //売上明細毎外税
            decimal w_UriageMeiSoto = 0;
            //売上請求毎内税
            decimal w_UriageSeiUti = 0;
            //売上請求毎外税
            decimal w_UriageSeiSoto = 0;
            //品名別売上消費税内税計(請求)
            decimal w_HinmeiUriageTaxUti_Sei = 0;
            //品名別売上消費税外税計(請求)
            decimal w_HinmeiUriageTaxSoto_Sei = 0;
            //品名別売上消費税内税計(明細)
            decimal w_HinmeiUriageTaxUti_Mei = 0;
            //品名別売上消費税外税計(明細)
            decimal w_HinmeiUriageTaxSoto_Mei = 0;

            if (!(saveSysID.Equals(row["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(row["DENPYOUSHURUI"].ToString())))
            {
                //売上金額合計
                if (row["URIAGE_AMOUNT_TOTAL"] != null)
                {
                    w_UriageTotal = w_UriageTotal + Decimal.Parse(BlankToZero(row["URIAGE_AMOUNT_TOTAL"].ToString()));
                }

                //品名別売上金額合計	
                if (row["HINMEI_URIAGE_KINGAKU_TOTAL"] != null)
                {
                    w_HinmeiUriageTotal = w_HinmeiUriageTotal + Decimal.Parse(BlankToZero(row["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()));
                }

                //売上税計算区分が伝票毎:1
                if (row["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == "1")
                {
                    //売上伝票毎内税	
                    if (row["URIAGE_TAX_UCHI"] != null)
                    {
                        w_UriageDenUti = w_UriageDenUti + Decimal.Parse(BlankToZero(row["URIAGE_TAX_UCHI"].ToString()));
                    }

                    //売上伝票毎外税	
                    if (row["URIAGE_TAX_SOTO"] != null)
                    {
                        w_UriageDenSoto = w_UriageDenSoto + Decimal.Parse(BlankToZero(row["URIAGE_TAX_SOTO"].ToString()));
                    }
                }

                //売上税計算区分が明細毎:3
                if (row["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == "3")
                {
                    //売上明細毎内税	
                    if (row["URIAGE_TAX_UCHI_TOTAL"] != null)
                    {
                        w_UriageMeiUti = w_UriageMeiUti + Decimal.Parse(BlankToZero(row["URIAGE_TAX_UCHI_TOTAL"].ToString()));
                    }

                    //売上明細毎外税	
                    if (row["URIAGE_TAX_SOTO_TOTAL"] != null)
                    {
                        w_UriageMeiSoto = w_UriageMeiSoto + Decimal.Parse(BlankToZero(row["URIAGE_TAX_SOTO_TOTAL"].ToString()));
                    }
                }

                //品名別売上消費税内税計	
                if (row["HINMEI_URIAGE_TAX_UCHI_TOTAL"] != null)
                {
                    w_HinmeiUriageTaxUti_Mei = w_HinmeiUriageTaxUti_Mei + Decimal.Parse(BlankToZero(row["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()));
                }

                //品名別売上消費税外税計	
                if (row["HINMEI_URIAGE_TAX_SOTO_TOTAL"] != null)
                {
                    w_HinmeiUriageTaxSoto_Mei = w_HinmeiUriageTaxSoto_Mei + Decimal.Parse(BlankToZero(row["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()));
                }

                //システムID、SEQの保存
                saveSysID = row["SYSTEM_ID"].ToString();
                saveDenshuKbn = row["DENPYOUSHURUI"].ToString();
            }

            //登録用売上データ算出
            CreateInsertUriageData(w_UriageTotal,
                                  w_HinmeiUriageTotal,
                                  w_UriageDenUti,
                                  w_UriageDenSoto,
                                  w_UriageMeiUti,
                                  w_UriageMeiSoto,
                                  w_UriageSeiUti,
                                  w_UriageSeiSoto,
                                  w_HinmeiUriageTaxUti_Sei,
                                  w_HinmeiUriageTaxSoto_Sei,
                                  w_HinmeiUriageTaxUti_Mei,
                                  w_HinmeiUriageTaxSoto_Mei);
        }

        #endregion

        #region 登録用売上データ算出

        /// <summary>
        /// 登録用売上データ算出
        /// </summary>
        /// <param name="w_uke_UriageTotal">売上金額合計</param>
        /// <param name="w_uke_HinmeiUriageTotal">品名別売上金額合計</param>
        /// <param name="w_uke_UriageDenUti">売上伝票毎内税</param>
        /// <param name="w_uke_UriageDenSoto">売上伝票毎外税</param>
        /// <param name="w_uke_UriageMeiUti">売上明細毎内税</param>
        /// <param name="w_uke_UriageMeiSoto">売上明細毎外税</param>
        /// <param name="w_uke_UriageSeiUti">売上請求毎内税</param>
        /// <param name="w_uke_UriageSeiSoto">売上請求毎外税</param>
        /// <param name="w_uke_HinmeiUriageTaxUti_Sei">品名別売上消費税内税計(請求)</param>
        /// <param name="w_uke_HinmeiUriageTaxSoto_Sei">品名別売上消費税外税計(請求)</param>
        /// <param name="w_uke_HinmeiUriageTaxUti_Mei">品名別売上消費税内税計(明細)</param>
        /// <param name="w_uke_HinmeiUriageTaxSoto_Mei">品名別売上消費税外税計(明細)</param>
        private void CreateInsertUriageData(decimal w_uke_UriageTotal,
                                           decimal w_uke_HinmeiUriageTotal,
                                           decimal w_uke_UriageDenUti,
                                           decimal w_uke_UriageDenSoto,
                                           decimal w_uke_UriageMeiUti,
                                           decimal w_uke_UriageMeiSoto,
                                           decimal w_uke_UriageSeiUti,
                                           decimal w_uke_UriageSeiSoto,
                                           decimal w_uke_HinmeiUriageTaxUti_Sei,
                                           decimal w_uke_HinmeiUriageTaxSoto_Sei,
                                           decimal w_uke_HinmeiUriageTaxUti_Mei,
                                           decimal w_uke_HinmeiUriageTaxSoto_Mei)
        {
            //今回伝内税額
            konkaiDenUtizeigaku = konkaiDenUtizeigaku + w_uke_UriageDenUti;

            //今回伝外税額
            konkaiDensotozeigaku = konkaiDensotozeigaku + w_uke_UriageDenSoto;

            //今回明内税額
            konkaiMeiUtizeigaku = konkaiMeiUtizeigaku + w_uke_UriageMeiUti + w_uke_HinmeiUriageTaxUti_Mei;

            //今回明外税額
            konkaiMeisotozeigaku = konkaiMeisotozeigaku + w_uke_UriageMeiSoto + w_uke_HinmeiUriageTaxSoto_Mei;

            //請求内税計
            konkaiSeiUtizeigaku = konkaiSeiUtizeigaku + w_uke_UriageSeiUti + w_uke_HinmeiUriageTaxUti_Sei;

            //請求外税計
            konkaiSeisotozeigaku = konkaiSeisotozeigaku + w_uke_UriageSeiSoto + w_uke_HinmeiUriageTaxSoto_Sei;

            //今回売上額
            konkaiUriagegaku = konkaiUriagegaku + w_uke_UriageTotal + w_uke_HinmeiUriageTotal;
        }

        #endregion

        #endregion

        #region 請求毎の消費税額算出

        /// <summary>
        /// 請求毎の消費税額算出(外税)
        /// </summary>
        /// <param name="uriageTotal">売上合計</param>
        /// <param name="tax">税率</param>
        /// <returns>算出消費税</returns>
        private decimal CreateSeikyuuTaxSoto(decimal uriageTotal, decimal tax, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData)
        {
            string hasuuCD = "";
            decimal shohizei = 0;
            decimal sign = 1;
            if (uriageTotal < 0)
            {
                // 売上合計が負の場合
                sign = -1;
            }

            hasuuCD = torihikisakiSeikyuuData.TAX_HASUU_CD.ToString();

            if (hasuuCD == "1")
            {
                //切り上げ
                shohizei = Math.Ceiling(Math.Abs(uriageTotal) * tax) * sign;
            }
            else if (hasuuCD == "2")
            {
                //切り捨て
                shohizei = Math.Floor(Math.Abs(uriageTotal) * tax) * sign;
            }
            else if (hasuuCD == "3")
            {
                //四捨五入
                shohizei = Math.Round(Math.Abs(uriageTotal * tax), 0, MidpointRounding.AwayFromZero) * sign;
            }

            return shohizei;
        }

        /// <summary>
        /// 請求毎の消費税額算出(内税)
        /// </summary>
        /// <param name="uriageTotal">売上合計</param>
        /// <param name="tax">税率</param>
        /// <returns>算出消費税</returns>
        private decimal CreateSeikyuuTaxUti(decimal uriageTotal, decimal tax, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData)
        {
            string hasuuCD = "";
            decimal shohizei = 0;
            decimal sign = 1;
            if (uriageTotal < 0)
            {
                // 売上合計が負の場合
                sign = -1;
            }

            hasuuCD = torihikisakiSeikyuuData.TAX_HASUU_CD.ToString();

            if (hasuuCD == "1")
            {
                //切り上げ
                shohizei = Math.Ceiling(Math.Abs(uriageTotal) - (Math.Abs(uriageTotal) / (1 + tax))) * sign;
            }
            else if (hasuuCD == "2")
            {
                //切り捨て
                shohizei = Math.Floor(Math.Abs(uriageTotal) - (Math.Abs(uriageTotal) / (1 + tax))) * sign;
            }
            else if (hasuuCD == "3")
            {
                //四捨五入
                shohizei = Math.Round((Math.Abs(uriageTotal) - (Math.Abs(uriageTotal) / (1 + tax))), 0, MidpointRounding.AwayFromZero) * sign;
            }

            return shohizei;
        }

        #endregion

        #endregion

        #region 支払月次データ作成

        /// <summary>
        /// 支払月次データ作成
        /// </summary>
        /// <param name="shiharaiDto"></param>
        /// <param name="shiharaiData"></param>
        /// <param name="shukkinData"></param>
        /// <param name="torihikisakiShiharaiData"></param>
        private void CreateShiharaiGetsujiData(GetsujiShoriDTOClass shiharaiDto, DataTable shiharaiData,
                                               DataTable shukkinData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData)
        {
            switch (torihikisakiShiharaiData.SHOSHIKI_KBN.ToString())
            {
                case "1":
                    /* 取引先別 */

                    //集計金額を初期化
                    //今回出金額
                    konkaiShukingaku = 0;
                    //今回調整額	
                    konkaiChouseigaku = 0;
                    //今回支払額	
                    konkaiShiharaigaku = 0;
                    //今回請内税額	
                    konkaiSeiUtizeigaku = 0;
                    //今回請外税額	
                    konkaiSeisotozeigaku = 0;
                    //今回伝内税額	
                    konkaiDenUtizeigaku = 0;
                    //今回伝外税額	
                    konkaiDensotozeigaku = 0;
                    //今回明内税額	
                    konkaiMeiUtizeigaku = 0;
                    //今回明外税額	
                    konkaiMeisotozeigaku = 0;
                    //今回御請求額	
                    konkaiSeikyuugaku = 0;

                    // 支払月次データ作成
                    this.CreateShiharaiData(shiharaiDto, shiharaiData, shukkinData, torihikisakiShiharaiData);

                    break;

                case "2":
                    /* 業者別 */

                    //集計金額を初期化
                    //今回出金額
                    konkaiShukingaku = 0;
                    //今回調整額	
                    konkaiChouseigaku = 0;
                    //今回支払額	
                    konkaiShiharaigaku = 0;
                    //今回請内税額	
                    konkaiSeiUtizeigaku = 0;
                    //今回請外税額	
                    konkaiSeisotozeigaku = 0;
                    //今回伝内税額	
                    konkaiDenUtizeigaku = 0;
                    //今回伝外税額	
                    konkaiDensotozeigaku = 0;
                    //今回明内税額	
                    konkaiMeiUtizeigaku = 0;
                    //今回明外税額	
                    konkaiMeisotozeigaku = 0;
                    //今回御請求額	
                    konkaiSeikyuugaku = 0;

                    // 支払月次データ作成
                    this.CreateShiharaiData(shiharaiDto, shiharaiData, shukkinData, torihikisakiShiharaiData);

                    break;

                case "3":
                    /* 現場別 */

                    //集計金額を初期化
                    //今回出金額
                    konkaiShukingaku = 0;
                    //今回調整額	
                    konkaiChouseigaku = 0;
                    //今回支払額	
                    konkaiShiharaigaku = 0;
                    //今回請内税額	
                    konkaiSeiUtizeigaku = 0;
                    //今回請外税額	
                    konkaiSeisotozeigaku = 0;
                    //今回伝内税額	
                    konkaiDenUtizeigaku = 0;
                    //今回伝外税額	
                    konkaiDensotozeigaku = 0;
                    //今回明内税額	
                    konkaiMeiUtizeigaku = 0;
                    //今回明外税額	
                    konkaiMeisotozeigaku = 0;
                    //今回御請求額	
                    konkaiSeikyuugaku = 0;

                    // 支払月次データ作成
                    this.CreateShiharaiData(shiharaiDto, shiharaiData, shukkinData, torihikisakiShiharaiData);

                    break;
            }
        }

        /// <summary>
        /// 支払月次処理データ作成
        /// </summary>
        /// <param name="shiharaiDto"></param>
        /// <param name="shiharaiData"></param>
        /// <param name="shukkinData"></param>
        /// <param name="torihikisakiShiharaiData"></param>
        private void CreateShiharaiData(GetsujiShoriDTOClass shiharaiDto, DataTable shiharaiData, DataTable shukkinData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData)
        {
            //前回繰越額
            decimal zenkaigaku = shiharaiDto.PREVIOUS_MONTH_BALANCE;

            // 足しこみ用TMP金額変数
            decimal tmpkonkaiShiharaigaku = 0;
            decimal tmpKonkaiSeiUtizeigaku = 0;
            decimal tmpKonkaiSeisotozeigaku = 0;
            decimal tmpKonkaiDenUtizeigaku = 0;
            decimal tmpKonkaiDensotozeigaku = 0;
            decimal tmpKonkaiMeiUtizeigaku = 0;
            decimal tmpKonkaiMeisotozeigaku = 0;
            decimal tmpKonkaiSeikyuugaku = 0;
            string gyoushaCd = "";
            string genbaCd = "";

            // 書式区分別に支払データから集計
            switch (torihikisakiShiharaiData.SHOSHIKI_KBN.ToString())
            {
                case "1":
                    // 請求先別
                    SumSyukkinShiharaiData(zenkaigaku, shiharaiData, shukkinData, torihikisakiShiharaiData);

                    break;

                case "2":
                    // 業者別
                    SumShukkinData(shukkinData);

                    for (int i = 0; i < shiharaiData.Rows.Count; i++)
                    {
                        if (gyoushaCd != shiharaiData.Rows[i]["GYOUSHA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = shiharaiData.Rows[i]["GYOUSHA_CD"].ToString();

                            SumShiharaiData("2", gyoushaCd, "", shiharaiData, torihikisakiShiharaiData);

                            tmpkonkaiShiharaigaku += konkaiShiharaigaku;
                            tmpKonkaiSeiUtizeigaku += konkaiSeiUtizeigaku;
                            tmpKonkaiSeisotozeigaku += konkaiSeisotozeigaku;
                            tmpKonkaiDenUtizeigaku += konkaiDenUtizeigaku;
                            tmpKonkaiDensotozeigaku += konkaiDensotozeigaku;
                            tmpKonkaiMeiUtizeigaku += konkaiMeiUtizeigaku;
                            tmpKonkaiMeisotozeigaku += konkaiMeisotozeigaku;
                            tmpKonkaiSeikyuugaku += konkaiSeikyuugaku;

                            konkaiShiharaigaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                            konkaiSeikyuugaku = 0;
                        }
                    }

                    konkaiShiharaigaku = tmpkonkaiShiharaigaku;
                    konkaiSeiUtizeigaku = tmpKonkaiSeiUtizeigaku;
                    konkaiSeisotozeigaku = tmpKonkaiSeisotozeigaku;
                    konkaiDenUtizeigaku = tmpKonkaiDenUtizeigaku;
                    konkaiDensotozeigaku = tmpKonkaiDensotozeigaku;
                    konkaiMeiUtizeigaku = tmpKonkaiMeiUtizeigaku;
                    konkaiMeisotozeigaku = tmpKonkaiMeisotozeigaku;
                    konkaiSeikyuugaku = zenkaigaku + tmpKonkaiSeikyuugaku - konkaiShukingaku - konkaiChouseigaku;

                    break;

                case "3":
                    // 現場別
                    SumShukkinData(shukkinData);

                    for (int i = 0; i < shiharaiData.Rows.Count; i++)
                    {
                        if (gyoushaCd != shiharaiData.Rows[i]["GYOUSHA_CD"].ToString() || genbaCd != shiharaiData.Rows[i]["GENBA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = shiharaiData.Rows[i]["GYOUSHA_CD"].ToString();
                            genbaCd = shiharaiData.Rows[i]["GENBA_CD"].ToString();

                            SumShiharaiData("3", gyoushaCd, genbaCd, shiharaiData, torihikisakiShiharaiData);

                            tmpkonkaiShiharaigaku += konkaiShiharaigaku;
                            tmpKonkaiSeiUtizeigaku += konkaiSeiUtizeigaku;
                            tmpKonkaiSeisotozeigaku += konkaiSeisotozeigaku;
                            tmpKonkaiDenUtizeigaku += konkaiDenUtizeigaku;
                            tmpKonkaiDensotozeigaku += konkaiDensotozeigaku;
                            tmpKonkaiMeiUtizeigaku += konkaiMeiUtizeigaku;
                            tmpKonkaiMeisotozeigaku += konkaiMeisotozeigaku;
                            tmpKonkaiSeikyuugaku += konkaiSeikyuugaku;

                            konkaiShiharaigaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                            konkaiSeikyuugaku = 0;
                        }
                    }

                    konkaiShiharaigaku = tmpkonkaiShiharaigaku;
                    konkaiSeiUtizeigaku = tmpKonkaiSeiUtizeigaku;
                    konkaiSeisotozeigaku = tmpKonkaiSeisotozeigaku;
                    konkaiDenUtizeigaku = tmpKonkaiDenUtizeigaku;
                    konkaiDensotozeigaku = tmpKonkaiDensotozeigaku;
                    konkaiMeiUtizeigaku = tmpKonkaiMeiUtizeigaku;
                    konkaiMeisotozeigaku = tmpKonkaiMeisotozeigaku;
                    konkaiSeikyuugaku = zenkaigaku + tmpKonkaiSeikyuugaku - konkaiShukingaku - konkaiChouseigaku;

                    break;

                default:
                    break;
            }

            T_MONTHLY_LOCK_SH monthlyLockShData = new T_MONTHLY_LOCK_SH();
            //取引先
            monthlyLockShData.TORIHIKISAKI_CD = shiharaiDto.TORIHIKISAKI_CD;
            DateTime date = DateTime.Parse(shiharaiDto.TO_DATE);
            //年
            monthlyLockShData.YEAR = short.Parse(date.Year.ToString());
            //月
            monthlyLockShData.MONTH = short.Parse(date.Month.ToString());
            //繰越残高
            monthlyLockShData.PREVIOUS_MONTH_BALANCE = zenkaigaku;
            //出金額
            monthlyLockShData.SHUKKIN_KINGAKU = SqlDecimal.Parse((konkaiShukingaku + konkaiChouseigaku).ToString());
            //税抜金額
            monthlyLockShData.KINGAKU = SqlDecimal.Parse(konkaiShiharaigaku.ToString());
            //消費税額
            monthlyLockShData.TAX = SqlDecimal.Parse((konkaiSeiUtizeigaku + konkaiSeisotozeigaku + konkaiDenUtizeigaku
                                                         + konkaiDensotozeigaku + konkaiMeiUtizeigaku + konkaiMeisotozeigaku).ToString());
            //締内税額	
            monthlyLockShData.SHIME_UTIZEI_GAKU = SqlDecimal.Parse(konkaiSeiUtizeigaku.ToString());
            //締外税額	
            monthlyLockShData.SHIME_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiSeisotozeigaku.ToString());
            //伝内税額	
            monthlyLockShData.DEN_UTIZEI_GAKU = SqlDecimal.Parse(konkaiDenUtizeigaku.ToString());
            //伝外税額	
            monthlyLockShData.DEN_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiDensotozeigaku.ToString());
            //明内税額	
            monthlyLockShData.MEI_UTIZEI_GAKU = SqlDecimal.Parse(konkaiMeiUtizeigaku.ToString());
            //明外税額	
            monthlyLockShData.MEI_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiMeisotozeigaku.ToString());
            //税込金額
            monthlyLockShData.TOTAL_KINGAKU = SqlDecimal.Parse((monthlyLockShData.KINGAKU + monthlyLockShData.TAX).ToString());
            //調整前差引残高
            monthlyLockShData.ZANDAKA = monthlyLockShData.PREVIOUS_MONTH_BALANCE + monthlyLockShData.TOTAL_KINGAKU - monthlyLockShData.SHUKKIN_KINGAKU;
            //適格区分(1.旧処理、2.適格対応)
            monthlyLockShData.INVOICE_KBN = LInvoceKBN;

            // 月次処理データを格納
            DateTime keyDate = DateTime.Parse(shiharaiDto.TO_DATE);
            string keyString = keyDate.ToString("yyyy/MM");
            if (this.MonthlyLockShDatas.ContainsKey(keyString))
            {
                List<T_MONTHLY_LOCK_SH> dataList = this.MonthlyLockShDatas[keyString];
                dataList.Add(monthlyLockShData);
            }
            else
            {
                List<T_MONTHLY_LOCK_SH> dataList = new List<T_MONTHLY_LOCK_SH>();
                dataList.Add(monthlyLockShData);
                this.MonthlyLockShDatas.Add(keyString, dataList);
            }
        }

        #region 出金、支払データ集計値作成

        private void SumSyukkinShiharaiData(decimal zenkaiGaku, DataTable shiharaiData, DataTable shukkinData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData)
        {
            SumSyukkinShiharaiData(zenkaiGaku, "1", string.Empty, string.Empty, shiharaiData, shukkinData, torihikisakiShiharaiData);
        }

        private void SumSyukkinShiharaiData(decimal zenkaiGaku, string shoshikiKbn, string gyoushaCd, string genbaCd,
                                            DataTable shiharaiData, DataTable shukkinData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData)
        {
            // 保存用システムIDの初期化
            saveSysID = "";

            //出金データから集計
            for (int i = 0; i < shukkinData.Rows.Count; i++)
            {
                if (saveSysID != shukkinData.Rows[i]["SYSTEM_ID"].ToString())
                {
                    //今回出金額
                    if (shukkinData.Rows[i]["SHUKKIN_AMOUNT_TOTAL"] != null)
                    {
                        konkaiShukingaku = konkaiShukingaku + Decimal.Parse(shukkinData.Rows[i]["SHUKKIN_AMOUNT_TOTAL"].ToString());
                    }

                    //今回調整額
                    if (shukkinData.Rows[i]["CHOUSEI_AMOUNT_TOTAL"] != null)
                    {
                        konkaiChouseigaku = konkaiChouseigaku + Decimal.Parse(shukkinData.Rows[i]["CHOUSEI_AMOUNT_TOTAL"].ToString());
                    }

                    //システムＩＤの保存
                    saveSysID = shukkinData.Rows[i]["SYSTEM_ID"].ToString();
                }
            }

            //保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            //支払データから集計
            if (LInvoceKBN == 2)
            {
                switch (shoshikiKbn)
                {
                    case "1":
                        //支払先別
                        CreatetSeikyuuShiharaiData(shiharaiData, torihikisakiShiharaiData);
                        break;
                    case "2":
                        //業者別
                        CreatetGyoushaShiharaiData(shiharaiData, torihikisakiShiharaiData, gyoushaCd);
                        break;
                    case "3":
                        //現場別
                        CreatetGenbaShiharaiData(shiharaiData, torihikisakiShiharaiData, gyoushaCd, genbaCd);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (shoshikiKbn)
                {
                    case "1":
                        //支払先別
                        CreatetSeikyuuShiharaiData_OldInvoice(shiharaiData, torihikisakiShiharaiData);
                        break;
                    case "2":
                        //業者別
                        CreatetGyoushaShiharaiData_OldInvoice(shiharaiData, torihikisakiShiharaiData, gyoushaCd);
                        break;
                    case "3":
                        //現場別
                        CreatetGenbaShiharaiData_OldInvoice(shiharaiData, torihikisakiShiharaiData, gyoushaCd, genbaCd);
                        break;
                    default:
                        break;
                }
            }

            //今回御請求額
            konkaiSeikyuugaku = zenkaiGaku + konkaiShiharaigaku
                              + konkaiSeiUtizeigaku + konkaiSeisotozeigaku
                              + konkaiDenUtizeigaku + konkaiDensotozeigaku
                              + konkaiMeiUtizeigaku + konkaiMeisotozeigaku
                              - konkaiShukingaku - konkaiChouseigaku;
        }

        /// <summary>
        /// 出金データ集計値を作成します。
        /// </summary>
        private void SumShukkinData(DataTable shukkinData)
        {
            // 保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            // 出金データから集計
            for (int i = 0; i < shukkinData.Rows.Count; i++)
            {
                if (saveSysID != shukkinData.Rows[i]["SYSTEM_ID"].ToString())
                {
                    // 今回出金額
                    if (shukkinData.Rows[i]["SHUKKIN_AMOUNT_TOTAL"] != null)
                    {
                        konkaiShukingaku = konkaiShukingaku + Decimal.Parse(shukkinData.Rows[i]["SHUKKIN_AMOUNT_TOTAL"].ToString());
                    }

                    // 今回調整額
                    if (shukkinData.Rows[i]["CHOUSEI_AMOUNT_TOTAL"] != null)
                    {
                        konkaiChouseigaku = konkaiChouseigaku + Decimal.Parse(shukkinData.Rows[i]["CHOUSEI_AMOUNT_TOTAL"].ToString());
                    }

                    // システムＩＤの保存
                    saveSysID = shukkinData.Rows[i]["SYSTEM_ID"].ToString();
                }
            }
        }

        /// <summary>
        /// 支払データ集計値を作成します。(for 業者別、現場別精算)
        /// </summary>
        /// <param name="shoshikiKbn">取引先_精算情報マスタ.書式区分</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="shiharaiData">支払データ(受入・出荷・売上/支払)</param>
        /// <param name="torihikisakiShiharaiData">取引先支払情報マスタデータ</param>
        private void SumShiharaiData(string shoshikiKbn, string gyoushaCd, string genbaCd,
                                     DataTable shiharaiData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData)
        {
            // 保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            // 支払データから集計
            if (LInvoceKBN == 2)
            {
                switch (shoshikiKbn)
                {
                    case "1":
                        // 請求別
                        CreatetSeikyuuShiharaiData(shiharaiData, torihikisakiShiharaiData);
                        break;
                    case "2":
                        // 業者別
                        CreatetGyoushaShiharaiData(shiharaiData, torihikisakiShiharaiData, gyoushaCd);
                        break;
                    case "3":
                        // 現場別
                        CreatetGenbaShiharaiData(shiharaiData, torihikisakiShiharaiData, gyoushaCd, genbaCd);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (shoshikiKbn)
                {
                    case "1":
                        // 請求別
                        CreatetSeikyuuShiharaiData_OldInvoice(shiharaiData, torihikisakiShiharaiData);
                        break;
                    case "2":
                        // 業者別
                        CreatetGyoushaShiharaiData_OldInvoice(shiharaiData, torihikisakiShiharaiData, gyoushaCd);
                        break;
                    case "3":
                        // 現場別
                        CreatetGenbaShiharaiData_OldInvoice(shiharaiData, torihikisakiShiharaiData, gyoushaCd, genbaCd);
                        break;
                    default:
                        break;
                }
            }

            //今回御精算額(業者別、現場別のため入金系は加算しない)
            konkaiSeikyuugaku = konkaiShiharaigaku
                              + konkaiSeiUtizeigaku + konkaiSeisotozeigaku
                              + konkaiDenUtizeigaku + konkaiDensotozeigaku
                              + konkaiMeiUtizeigaku + konkaiMeisotozeigaku;
        }

        #endregion

        #region 支払データ算出

        #region 支払データ算出(支払先別)

        /// <summary>
        /// 支払データ算出(支払先別)
        /// </summary>
        /// <param name="shiharaiData"></param>
        /// <param name="torihikisakiShiharaiData"></param>
        private void CreatetSeikyuuShiharaiData(DataTable shiharaiData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData)
        {
            // 精算毎税の計算用項目の初期化
            decimal shiharaiTotal = 0;
            decimal shohizeirate = 0;
            int count = 0;
            decimal shiharaiTotalzei = 0;

            // 精算毎税の計算
            DataView dv = new DataView(shiharaiData);
            DataTable table = shiharaiData.Clone();
            dv.Sort = "SHIHARAI_SHOUHIZEI_RATE,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            // 精算毎税以外の計算
            for (int i = 0; i < table.Rows.Count; i++)
            {
                CreatetShiharaiData(table.Rows[i]);
            }

            if (shiharaiData.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //初回
                    if (count == 0)
                    {
                        shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                    }

                    if (decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()) != shohizeirate)
                    {
                        //消費税計算・変数初期化
                        shiharaiTotalzei = shiharaiTotalzei + CreateSeisanTaxSoto(shiharaiTotal, shohizeirate, torihikisakiShiharaiData);
                        shiharaiTotal = 0;
                    }

                    if (string.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() == table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                 Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString())
                        {
                            //精算毎外税
                            shiharaiTotal = shiharaiTotal
                                        + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()));
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString())
                        {
                            //明細毎内税
                            shiharaiTotal = shiharaiTotal
                                        + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()))
                                        - Decimal.Parse(BlankToZero(table.Rows[i]["TAX_UCHI"].ToString()));
                        }
                    }
                    else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())
                    {
                        //品名内税
                        shiharaiTotal = shiharaiTotal
                                        + Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_KINGAKU"].ToString()))
                                        - Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_TAX_UCHI"].ToString()));
                    }

                    //消費税率の保存
                    shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                    //カウントアップ
                    count++;
                }
                //消費税率の保存
                shiharaiTotalzei = shiharaiTotalzei + CreateSeisanTaxSoto(shiharaiTotal, shohizeirate, torihikisakiShiharaiData);
            }

            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
            //精算毎外税
            konkaiSeisotozeigaku = shiharaiTotalzei;
            //今回明細内税
            konkaiMeiUtizeigaku = 0;
        }

        #endregion

        #region 支払データ算出(業者別)

        /// <summary>
        /// 支払データ算出(業者別)
        /// </summary>
        private void CreatetGyoushaShiharaiData(DataTable shiharaiData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData, string gyoushaCd)
        {
            //請求毎税の計算用項目の初期化
            decimal shiharaiTotal = 0;
            decimal shohizeirate = 0;
            int count = 0;
            decimal shiharaiTotalzei = 0;

            // 精算毎税の計算
            DataView dv = new DataView(shiharaiData);
            DataTable table = shiharaiData.Clone();
            dv.Sort = "SHIHARAI_SHOUHIZEI_RATE,GYOUSHA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
                {
                    CreatetShiharaiData(table.Rows[i]);
                }
            }

            if (shiharaiData.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
                    {
                        //初回
                        if (count == 0)
                        {
                            shohizeirate = Decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                        }

                        if (Decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()) != shohizeirate)
                        {
                            //消費税計算・変数初期化
                            shiharaiTotalzei = shiharaiTotalzei + CreateSeisanTaxSoto(shiharaiTotal, shohizeirate, torihikisakiShiharaiData);
                            shiharaiTotal = 0;
                        }

                        if (string.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() == table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                 Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString())
                            {
                                //精算毎外税
                                shiharaiTotal = shiharaiTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()));
                            }
                            else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString())
                            {
                                //明細毎内税
                                shiharaiTotal = shiharaiTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()))
                                            - Decimal.Parse(BlankToZero(table.Rows[i]["TAX_UCHI"].ToString()));
                            }
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())
                        {
                            //品名内税
                            shiharaiTotal = shiharaiTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_KINGAKU"].ToString()))
                                            - Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_TAX_UCHI"].ToString()));
                        }

                        //消費税率の保存、システムID、SEQの保存
                        shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                        //カウントアップ
                        count++;
                    }
                }
                //消費税率の保存
                shiharaiTotalzei = shiharaiTotalzei + CreateSeisanTaxSoto(shiharaiTotal, shohizeirate, torihikisakiShiharaiData);
            }

            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
            //精算毎外税
            konkaiSeisotozeigaku = shiharaiTotalzei;
            //今回明細内税
            konkaiMeiUtizeigaku = 0;
        }

        #endregion

        #region 支払データ算出(現場別)

        /// <summary>
        /// 支払データ算出(受入)現場別
        /// </summary>
        private void CreatetGenbaShiharaiData(DataTable shiharaiData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData,
                                              string gyoushaCd, string genbaCd)
        {
            //精算毎税の計算用項目の初期化
            decimal shiharaiTotal = 0;
            decimal shohizeirate = 0;
            int count = 0;
            decimal shiharaiTotalzei = 0;

            // 精算毎税の計算
            DataView dv = new DataView(shiharaiData);
            DataTable table = shiharaiData.Clone();
            dv.Sort = "SHIHARAI_SHOUHIZEI_RATE,GYOUSHA_CD,GENBA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && table.Rows[i]["GENBA_CD"].ToString() == genbaCd)
                {
                    CreatetShiharaiData(table.Rows[i]);
                }
            }

            if (shiharaiData.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && table.Rows[i]["GENBA_CD"].ToString() == genbaCd)
                    {
                        //初回
                        if (count == 0)
                        {
                            shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                        }

                        if (decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()) != shohizeirate)
                        {
                            //消費税計算・変数初期化
                            shiharaiTotalzei = shiharaiTotalzei + CreateSeisanTaxSoto(shiharaiTotal, shohizeirate, torihikisakiShiharaiData);
                            shiharaiTotal = 0;
                        }

                        if (string.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() == table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                 Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString())
                            {
                                //精算毎外税
                                shiharaiTotal = shiharaiTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()));
                            }
                            else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString())
                            {
                                //明細毎内税
                                shiharaiTotal = shiharaiTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()))
                                            - Decimal.Parse(BlankToZero(table.Rows[i]["TAX_UCHI"].ToString()));
                            }
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())
                        {
                            //品名内税
                            shiharaiTotal = shiharaiTotal
                                            + Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_KINGAKU"].ToString()))
                                            - Decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_TAX_UCHI"].ToString()));
                        }

                        //消費税率の保存
                        shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                        //カウントアップ
                        count++;
                    }
                }
                //消費税率の保存
                shiharaiTotalzei = shiharaiTotalzei + CreateSeisanTaxSoto(shiharaiTotal, shohizeirate, torihikisakiShiharaiData);
            }

            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
            //精算毎外税
            konkaiSeisotozeigaku = shiharaiTotalzei;
            //今回明細内税
            konkaiMeiUtizeigaku = 0;
        }

        #endregion

        #region 支払データ算出

        /// <summary>
        /// 支払データ算出
        /// </summary>
        /// <param name="row"></param>
        private void CreatetShiharaiData(DataRow row)
        {
            //支払金額合計
            decimal w_ShiharaiTotal = 0;
            //品名別支払金額合計
            decimal w_HinmeiShiharaiTotal = 0;
            //支払伝票毎内税
            decimal w_ShiharaiDenUti = 0;
            //支払伝票毎外税	
            decimal w_ShiharaiDenSoto = 0;
            //支払明細毎内税
            decimal w_ShiharaiMeiUti = 0;
            //支払明細毎外税
            decimal w_ShiharaiMeiSoto = 0;
            //支払請求毎内税
            decimal w_ShiharaiSeiUti = 0;
            //支払請求毎外税
            decimal w_ShiharaiSeiSoto = 0;
            //品名別支払消費税内税計(請求)
            decimal w_HinmeiShiharaiTaxUti_Sei = 0;
            //品名別支払消費税外税計(請求)
            decimal w_HinmeiShiharaiTaxSoto_Sei = 0;
            //品名別支払消費税内税計(明細)
            decimal w_HinmeiShiharaiTaxUti_Mei = 0;
            //品名別支払消費税外税計(明細)
            decimal w_HinmeiShiharaiTaxSoto_Mei = 0;

            if (!(saveSysID.Equals(row["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(row["DENPYOUSHURUI"].ToString())))
            {
                //支払金額合計
                if (row["SHIHARAI_KINGAKU_TOTAL"] != null)
                {
                    w_ShiharaiTotal = w_ShiharaiTotal + Decimal.Parse(BlankToZero(row["SHIHARAI_KINGAKU_TOTAL"].ToString()));
                }

                //品名別支払金額合計	
                if (row["HINMEI_SHIHARAI_KINGAKU_TOTAL"] != null)
                {
                    w_HinmeiShiharaiTotal = w_HinmeiShiharaiTotal + Decimal.Parse(BlankToZero(row["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()));
                }

                //支払税計算区分が伝票毎:1
                if (row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == "1")
                {
                    //支払伝票毎内税	
                    if (row["SHIHARAI_TAX_UCHI"] != null)
                    {
                        w_ShiharaiDenUti = w_ShiharaiDenUti + Decimal.Parse(BlankToZero(row["SHIHARAI_TAX_UCHI"].ToString()));
                    }

                    //支払伝票毎外税	
                    if (row["SHIHARAI_TAX_SOTO"] != null)
                    {
                        w_ShiharaiDenSoto = w_ShiharaiDenSoto + Decimal.Parse(BlankToZero(row["SHIHARAI_TAX_SOTO"].ToString()));
                    }
                }

                //支払税計算区分が明細毎:3
                if (row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == "3")
                {
                    //支払明細毎内税	
                    if (row["SHIHARAI_TAX_UCHI_TOTAL"] != null)
                    {
                        w_ShiharaiMeiUti = w_ShiharaiMeiUti + Decimal.Parse(BlankToZero(row["SHIHARAI_TAX_UCHI_TOTAL"].ToString()));
                    }

                    //支払明細毎外税	
                    if (row["SHIHARAI_TAX_SOTO_TOTAL"] != null)
                    {
                        w_ShiharaiMeiSoto = w_ShiharaiMeiSoto + Decimal.Parse(BlankToZero(row["SHIHARAI_TAX_SOTO_TOTAL"].ToString()));
                    }
                }

                //品名別支払消費税内税計	
                if (row["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"] != null)
                {
                    w_HinmeiShiharaiTaxUti_Mei = w_HinmeiShiharaiTaxUti_Mei + Decimal.Parse(BlankToZero(row["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()));
                }

                //品名別支払消費税外税計	
                if (row["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"] != null)
                {
                    w_HinmeiShiharaiTaxSoto_Mei = w_HinmeiShiharaiTaxSoto_Mei + Decimal.Parse(BlankToZero(row["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()));
                }

                //システムID、SEQの保存
                saveSysID = row["SYSTEM_ID"].ToString();
                saveDenshuKbn = row["DENPYOUSHURUI"].ToString();
            }

            //登録用支払データ算出
            CreateInsertShiharaiData(w_ShiharaiTotal,
                                  w_HinmeiShiharaiTotal,
                                  w_ShiharaiDenUti,
                                  w_ShiharaiDenSoto,
                                  w_ShiharaiMeiUti,
                                  w_ShiharaiMeiSoto,
                                  w_ShiharaiSeiUti,
                                  w_ShiharaiSeiSoto,
                                  w_HinmeiShiharaiTaxUti_Sei,
                                  w_HinmeiShiharaiTaxSoto_Sei,
                                  w_HinmeiShiharaiTaxUti_Mei,
                                  w_HinmeiShiharaiTaxSoto_Mei);

        }

        #endregion

        #region 登録用支払データ算出

        /// <summary>
        /// 登録用支払データ算出
        /// </summary>
        /// <param name="w_uke_UriageTotal">売上金額合計</param>
        /// <param name="w_uke_HinmeiUriageTotal">品名別売上金額合計</param>
        /// <param name="w_uke_UriageDenUti">売上伝票毎内税</param>
        /// <param name="w_uke_UriageDenSoto">売上伝票毎外税</param>
        /// <param name="w_uke_UriageMeiUti">売上明細毎内税</param>
        /// <param name="w_uke_UriageMeiSoto">売上明細毎外税</param>
        /// <param name="w_uke_UriageSeiUti">売上請求毎内税</param>
        /// <param name="w_uke_UriageSeiSoto">売上請求毎外税</param>
        /// <param name="w_uke_HinmeiUriageTaxUti_Sei">品名別売上消費税内税計(請求)</param>
        /// <param name="w_uke_HinmeiUriageTaxSoto_Sei">品名別売上消費税外税計(請求)</param>
        /// <param name="w_uke_HinmeiUriageTaxUti_Mei">品名別売上消費税内税計(明細)</param>
        /// <param name="w_uke_HinmeiUriageTaxSoto_Mei">品名別売上消費税外税計(明細)</param>
        private void CreateInsertShiharaiData(decimal w_uke_UriageTotal,
                                           decimal w_uke_HinmeiUriageTotal,
                                           decimal w_uke_UriageDenUti,
                                           decimal w_uke_UriageDenSoto,
                                           decimal w_uke_UriageMeiUti,
                                           decimal w_uke_UriageMeiSoto,
                                           decimal w_uke_UriageSeiUti,
                                           decimal w_uke_UriageSeiSoto,
                                           decimal w_uke_HinmeiUriageTaxUti_Sei,
                                           decimal w_uke_HinmeiUriageTaxSoto_Sei,
                                           decimal w_uke_HinmeiUriageTaxUti_Mei,
                                           decimal w_uke_HinmeiUriageTaxSoto_Mei)
        {
            //今回伝内税額
            konkaiDenUtizeigaku = konkaiDenUtizeigaku + w_uke_UriageDenUti;

            //今回伝外税額
            konkaiDensotozeigaku = konkaiDensotozeigaku + w_uke_UriageDenSoto;

            //今回明内税額
            konkaiMeiUtizeigaku = konkaiMeiUtizeigaku + w_uke_UriageMeiUti + w_uke_HinmeiUriageTaxUti_Mei;

            //今回明外税額
            konkaiMeisotozeigaku = konkaiMeisotozeigaku + w_uke_UriageMeiSoto + w_uke_HinmeiUriageTaxSoto_Mei;

            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku + w_uke_UriageTotal + w_uke_HinmeiUriageTotal;
        }

        #endregion

        #endregion

        #region 精算毎の消費税額算出

        /// <summary>
        /// 精算毎の消費税額算出(外税)
        /// </summary>
        /// <param name="siharaiTotal">支払合計</param>
        /// <param name="tax">税率</param>
        /// <returns>算出消費税</returns>
        private decimal CreateSeisanTaxSoto(decimal siharaiTotal,
                                             decimal tax,
                                             M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData)
        {
            string hasuuCD = "";
            decimal shohizei = 0;
            decimal sign = 1;
            if (siharaiTotal < 0)
            {
                // 売上合計が負の場合
                sign = -1;
            }

            hasuuCD = torihikisakiShiharaiData.TAX_HASUU_CD.ToString();

            if (hasuuCD.Equals("1"))
            {
                //切り上げ
                shohizei = Math.Ceiling(Math.Abs(siharaiTotal) * tax) * sign;
            }
            else if (hasuuCD.Equals("2"))
            {
                //切り捨て
                shohizei = Math.Floor(Math.Abs(siharaiTotal) * tax) * sign;
            }
            else if (hasuuCD.Equals("3"))
            {
                //四捨五入
                shohizei = Math.Round(Math.Abs(siharaiTotal * tax), 0, MidpointRounding.AwayFromZero) * sign;
            }

            return shohizei;

        }

        /// <summary>
        /// 精算毎の消費税額算出(内税)
        /// </summary>
        /// <param name="uriageTotal">売上合計</param>
        /// <param name="tax">税率</param>
        /// <returns>算出消費税</returns>
        private decimal CreateSeisanTaxUti(decimal uriageTotal,
                                            decimal tax,
                                            M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData)
        {
            string hasuuCD = "";
            decimal shohizei = 0;
            decimal sign = 1;
            if (uriageTotal < 0)
            {
                // 売上合計が負の場合
                sign = -1;
            }

            hasuuCD = torihikisakiShiharaiData.TAX_HASUU_CD.ToString();

            if (hasuuCD.Equals("1"))
            {
                //切り上げ
                shohizei = Math.Ceiling(Math.Abs(uriageTotal) - (Math.Abs(uriageTotal) / (1 + tax))) * sign;
            }
            else if (hasuuCD.Equals("2"))
            {
                //切り捨て
                shohizei = Math.Floor(Math.Abs(uriageTotal) - (Math.Abs(uriageTotal) / (1 + tax))) * sign;
            }
            else if (hasuuCD.Equals("3"))
            {
                //四捨五入
                shohizei = Math.Round((Math.Abs(uriageTotal) - (Math.Abs(uriageTotal) / (1 + tax))), 0, MidpointRounding.AwayFromZero) * sign;
            }

            return shohizei;
        }

        #endregion

        #endregion

        #region プログレスバー表示関連処理

        /// <summary>
        /// プログレスバーの初期化
        /// </summary>
        /// <param name="min">プログレスバーに反映する最小の値</param>
        /// <param name="max">プログレスバーに反映する最大の値</param>
        private void InitProgressBar(int min, int max)
        {
            this.progressBar.Maximum = max;
            this.progressBar.Minimum = min;
            this.progressBar.Value = 0;
        }

        /// <summary>
        /// プログレスバーの値を設定します
        /// </summary>
        /// <param name="val">プログレスバーの値</param>
        private void SetProgressBarValue(int val)
        {
            if (this.progressBar.Maximum >= val)
            {
                this.progressBar.Value = val;
            }
        }

        /// <summary>
        /// プログレスバーをリセット
        /// </summary>
        private void ResetProgBar()
        {
            this.progressBar.Value = 0;
        }

        #endregion

        #region Utility

        /// <summary>
        /// 空白をゼロに置き換える
        /// </summary>
        /// <param name="chkText">チェック対象</param>
        /// <returns>置き換え後文字列</returns>
        private String BlankToZero(string chkText)
        {

            if (chkText == "")
            {
                return "0";
            }
            else
            {
                return chkText;
            }
        }

        #endregion

        /// <summary>
        /// 月次（日付指定）
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="getujiCheck"></param>
        /// <param name="continues"></param>
        /// <param name="ZandakaStartDay"></param>
        /// <param name="ZandakaEndDay"></param>
        /// <param name="zandaka"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <param name="windowId"></param>
        /// <param name="shoriType"></param>
        /// <returns></returns>
        public bool GetsujiShoriHituke(string torihikisakiCd, bool continues, DateTime ZandakaStartDay, DateTime ZandakaEndDay, decimal zandaka, DateTime startDay, DateTime endDay, int InvoiceKbn,
            r_framework.Const.WINDOW_ID windowId = r_framework.Const.WINDOW_ID.NONE, GETSUJI_SHORI_TYPE shoriType = GETSUJI_SHORI_TYPE.BOTH)
        {
            LogUtility.DebugMethodStart(torihikisakiCd, continues, ZandakaStartDay, ZandakaEndDay, zandaka, startDay, endDay, InvoiceKbn, windowId, shoriType);

            #region 売上月次処理

            LInvoceKBN = InvoiceKbn;

            if ((shoriType & GETSUJI_SHORI_TYPE.SEIKYUU) == GETSUJI_SHORI_TYPE.SEIKYUU)
            {
                // 取引先_請求情報取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
                // ※月次処理実行時の速度問題によりListを優先で使用。（Listが空の場合は月次処理画面以外からの呼び出し）
                M_TORIHIKISAKI_SEIKYUU mtse;
                if (this.TorihikisakiSeikyuList != null && this.TorihikisakiSeikyuList.Count > 0)
                {
                    mtse = this.TorihikisakiSeikyuList.Find(delegate (M_TORIHIKISAKI_SEIKYUU data) { return data.TORIHIKISAKI_CD.Equals(torihikisakiCd); });

                    // Listに無いというのはありえないが念のため回避用
                    if (mtse == null)
                    {
                        mtse = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                    }
                }
                else
                {
                    mtse = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                }

                if (mtse != null && mtse.TORIHIKI_KBN_CD == 2)
                {
                    /* 取引先の設定が掛けの場合、伝票データをから当月の金額を算出する */

                    // 売上用のDTO作成
                    GetsujiShoriDTOClass uriageDto = new GetsujiShoriDTOClass();
                    uriageDto.TORIHIKISAKI_CD = torihikisakiCd;

                    if (continues)
                    {
                        if (ZandakaStartDay == DateTime.MinValue)
                        {
                            // 売上データ作成開始
                            uriageDto.PREVIOUS_MONTH_BALANCE = this.ExecUriageGetsujiShoriHituke(uriageDto, string.Empty, ZandakaEndDay.ToString(), zandaka);
                        }
                        else
                        {
                            // 売上データ作成開始
                            uriageDto.PREVIOUS_MONTH_BALANCE = this.ExecUriageGetsujiShoriHituke(uriageDto, ZandakaStartDay.ToString(), ZandakaEndDay.ToString(), zandaka);
                        }
                    }
                    else
                    {
                        uriageDto.PREVIOUS_MONTH_BALANCE = zandaka;
                    }

                    uriageDto.FROM_DATE = startDay.ToString("yyyy/MM/dd");
                    uriageDto.TO_DATE = endDay.ToString("yyyy/MM/dd");

                    // 売上データ作成開始
                    this.ExecUriageGetsujiShori(uriageDto);
                }
                else
                {
                    /* 取引先の設定が現金の場合、当月の金額は0円とし過去データがある場合は繰越残高だけ引き継いだデータを作成する */

                    /* 過去データがあるか調べ、ある場合は残高を取得 */
                    decimal zandakas = 0;
                    DateTime shimeDate = new DateTime(startDay.Year, startDay.Month, 1);
                    DataTable latestGetsujiData = this.dao.GetLatestGetsujiDateByTorihikisakiCd(torihikisakiCd, shimeDate.ToString("yyyy/MM/dd"));
                    if (latestGetsujiData != null && latestGetsujiData.Rows.Count > 0)
                    {
                        int latestGetsujiShoriYear = int.Parse(latestGetsujiData.Rows[0]["YEAR"].ToString());
                        int latestGetsujiShoriMonth = int.Parse(latestGetsujiData.Rows[0]["MONTH"].ToString());
                        this.GetUriageKurikosigaku(torihikisakiCd, latestGetsujiShoriYear, latestGetsujiShoriMonth, ref zandakas);
                    }

                    /* 売上月次処理データ作成 */
                    T_MONTHLY_LOCK_UR monthlyLockUrData = new T_MONTHLY_LOCK_UR();
                    //取引先
                    monthlyLockUrData.TORIHIKISAKI_CD = torihikisakiCd;
                    //年
                    monthlyLockUrData.YEAR = short.Parse(startDay.Year.ToString());
                    //月
                    monthlyLockUrData.MONTH = short.Parse(startDay.Month.ToString());
                    //繰越残高
                    monthlyLockUrData.PREVIOUS_MONTH_BALANCE = zandakas;
                    //入金額
                    monthlyLockUrData.NYUUKIN_KINGAKU = 0;
                    //税抜金額
                    monthlyLockUrData.KINGAKU = 0;
                    //消費税額
                    monthlyLockUrData.TAX = 0;
                    //締内税額	
                    monthlyLockUrData.SHIME_UTIZEI_GAKU = 0;
                    //締外税額	
                    monthlyLockUrData.SHIME_SOTOZEI_GAKU = 0;
                    //伝内税額	
                    monthlyLockUrData.DEN_UTIZEI_GAKU = 0;
                    //伝外税額	
                    monthlyLockUrData.DEN_SOTOZEI_GAKU = 0;
                    //明内税額	
                    monthlyLockUrData.MEI_UTIZEI_GAKU = 0;
                    //明外税額	
                    monthlyLockUrData.MEI_SOTOZEI_GAKU = 0;
                    //税込金額
                    monthlyLockUrData.TOTAL_KINGAKU = 0;
                    //調整前差引残高
                    monthlyLockUrData.ZANDAKA = zandakas;
                    //適格区分(1.旧処理、2.適格対応)
                    monthlyLockUrData.INVOICE_KBN = LInvoceKBN;

                    // 月次処理データを格納
                    DateTime keyDate = new DateTime(startDay.Year, startDay.Month, 1);
                    string keyString = keyDate.ToString("yyyy/MM");
                    if (this.MonthlyLockUrDatas.ContainsKey(keyString))
                    {
                        List<T_MONTHLY_LOCK_UR> dataList = this.MonthlyLockUrDatas[keyString];
                        dataList.Add(monthlyLockUrData);
                    }
                    else
                    {
                        List<T_MONTHLY_LOCK_UR> dataList = new List<T_MONTHLY_LOCK_UR>();
                        dataList.Add(monthlyLockUrData);
                        this.MonthlyLockUrDatas.Add(keyString, dataList);
                    }
                }
            }

            #endregion

            #region 支払月次処理

            if ((shoriType & GETSUJI_SHORI_TYPE.SHIHARAI) == GETSUJI_SHORI_TYPE.SHIHARAI)
            {
                // 取引先_支払情報取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
                // ※月次処理実行時の速度問題によりListを優先で使用。（Listが空の場合は月次処理画面以外からの呼び出し）
                M_TORIHIKISAKI_SHIHARAI mtsh;
                if (this.TorihikisakiShiharaiList != null && this.TorihikisakiShiharaiList.Count > 0)
                {
                    mtsh = this.TorihikisakiShiharaiList.Find(delegate(M_TORIHIKISAKI_SHIHARAI data) { return data.TORIHIKISAKI_CD.Equals(torihikisakiCd); });

                    // Listに無いというのはありえないが念のため回避用
                    if (mtsh == null)
                    {
                        mtsh = this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
                    }
                }
                else
                {
                    mtsh = this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
                }

                if (mtsh != null && mtsh.TORIHIKI_KBN_CD == 2)
                {
                    /* 取引先の設定が掛けの場合、伝票データをから当月の金額を算出する */

                    GetsujiShoriDTOClass shiharaiDto = new GetsujiShoriDTOClass();
                    shiharaiDto.TORIHIKISAKI_CD = torihikisakiCd;

                    if (continues)
                    {
                        if (ZandakaStartDay == DateTime.MinValue)
                        {
                            // 売上データ作成開始
                            shiharaiDto.PREVIOUS_MONTH_BALANCE = this.ExecShiharaiGetsujiShoriHituke(shiharaiDto, string.Empty, ZandakaEndDay.ToString(), zandaka);
                        }
                        else
                        {
                            // 売上データ作成開始
                            shiharaiDto.PREVIOUS_MONTH_BALANCE = this.ExecShiharaiGetsujiShoriHituke(shiharaiDto, ZandakaStartDay.ToString(), ZandakaEndDay.ToString(), zandaka);
                        }
                    }
                    else
                    {
                        shiharaiDto.PREVIOUS_MONTH_BALANCE = zandaka;
                    }

                    shiharaiDto.FROM_DATE = startDay.ToString("yyyy/MM/dd");
                    shiharaiDto.TO_DATE = endDay.ToString("yyyy/MM/dd");

                    // 支払月次データ作成開始
                    this.ExecShiharaiGetsujiShori(shiharaiDto);
                }
                else
                {
                    /* 取引先の設定が現金の場合、当月の金額は0円とし過去データがある場合は繰越残高だけ引き継いだデータを作成する */

                    /* 過去データがあるか調べ、ある場合は残高を取得 */
                    decimal zandakas = 0;
                    DateTime shimeDate = new DateTime(startDay.Year, startDay.Month, 1);
                    DataTable latestGetsujiData = this.dao.GetLatestGetsujiDateByTorihikisakiCd(torihikisakiCd, shimeDate.ToString("yyyy/MM/dd"));
                    if (latestGetsujiData != null && latestGetsujiData.Rows.Count > 0)
                    {
                        int latestGetsujiShoriYear = int.Parse(latestGetsujiData.Rows[0]["YEAR"].ToString());
                        int latestGetsujiShoriMonth = int.Parse(latestGetsujiData.Rows[0]["MONTH"].ToString());
                        this.GetShiharaiKurikosigaku(torihikisakiCd, latestGetsujiShoriYear, latestGetsujiShoriMonth, ref zandakas);
                    }

                    /* 売上月次処理データ作成 */
                    T_MONTHLY_LOCK_SH monthlyLockShData = new T_MONTHLY_LOCK_SH();
                    //取引先
                    monthlyLockShData.TORIHIKISAKI_CD = torihikisakiCd;
                    //年
                    monthlyLockShData.YEAR = short.Parse(startDay.Year.ToString());
                    //月
                    monthlyLockShData.MONTH = short.Parse(startDay.Month.ToString());
                    //繰越残高
                    monthlyLockShData.PREVIOUS_MONTH_BALANCE = zandakas;
                    //出金額
                    monthlyLockShData.SHUKKIN_KINGAKU = 0;
                    //税抜金額
                    monthlyLockShData.KINGAKU = 0;
                    //消費税額
                    monthlyLockShData.TAX = 0;
                    //締内税額	
                    monthlyLockShData.SHIME_UTIZEI_GAKU = 0;
                    //締外税額	
                    monthlyLockShData.SHIME_SOTOZEI_GAKU = 0;
                    //伝内税額	
                    monthlyLockShData.DEN_UTIZEI_GAKU = 0;
                    //伝外税額	
                    monthlyLockShData.DEN_SOTOZEI_GAKU = 0;
                    //明内税額	
                    monthlyLockShData.MEI_UTIZEI_GAKU = 0;
                    //明外税額	
                    monthlyLockShData.MEI_SOTOZEI_GAKU = 0;
                    //税込金額
                    monthlyLockShData.TOTAL_KINGAKU = 0;
                    //調整前差引残高
                    monthlyLockShData.ZANDAKA = zandakas;
                    //適格区分(1.旧処理、2.適格対応)
                    monthlyLockShData.INVOICE_KBN = LInvoceKBN;

                    // 月次処理データを格納
                    DateTime keyDate = new DateTime(startDay.Year, startDay.Month, 1);
                    string keyString = keyDate.ToString("yyyy/MM");
                    if (this.MonthlyLockShDatas.ContainsKey(keyString))
                    {
                        List<T_MONTHLY_LOCK_SH> dataList = this.MonthlyLockShDatas[keyString];
                        dataList.Add(monthlyLockShData);
                    }
                    else
                    {
                        List<T_MONTHLY_LOCK_SH> dataList = new List<T_MONTHLY_LOCK_SH>();
                        dataList.Add(monthlyLockShData);
                        this.MonthlyLockShDatas.Add(keyString, dataList);
                    }
                }
            }

            #endregion

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 月次処理(売上)を実行します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="zandaka"></param>
        private decimal ExecUriageGetsujiShoriHituke(GetsujiShoriDTOClass uriageDto, string ZandakaStartDay, string ZandakaEndDay, decimal zandaka)
        {
            // 売上データ取得
            DataTable uriageData = this.GetUriageData(uriageDto.TORIHIKISAKI_CD, ZandakaStartDay, ZandakaEndDay);
            // 入金データ取得
            DataTable nyuukinData = this.GetNyuukinData(uriageDto.TORIHIKISAKI_CD, ZandakaStartDay, ZandakaEndDay);
            // 取引先請求情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
            M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData;
            if (this.TorihikisakiSeikyuList != null && this.TorihikisakiSeikyuList.Count > 0)
            {
                torihikisakiSeikyuuData = this.TorihikisakiSeikyuList.Find(delegate (M_TORIHIKISAKI_SEIKYUU data) { return data.TORIHIKISAKI_CD.Equals(uriageDto.TORIHIKISAKI_CD); });

                // Listに無いというのはありえないが念のため回避用
                if (torihikisakiSeikyuuData == null)
                {
                    torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(uriageDto.TORIHIKISAKI_CD);
                }
            }
            else
            {
                torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(uriageDto.TORIHIKISAKI_CD);
            }

            // 売上データ作成開始
            return CreateUriageDataHituke(uriageDto, uriageData, nyuukinData, torihikisakiSeikyuuData, zandaka);
        }

        /// <summary>
        /// 売上月次処理データ作成
        /// </summary>
        /// <param name="uriageDto"></param>
        /// <param name="uriageData"></param>
        /// <param name="nyuukinData"></param>
        /// <param name="torihikisakiSeikyuuData"></param>
        private decimal CreateUriageDataHituke(GetsujiShoriDTOClass uriageDto, DataTable uriageData, DataTable nyuukinData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData, decimal zandaka)
        {
            //前回繰越額
            decimal zenkaigaku = zandaka;

            // 足しこみ用TMP金額変数
            decimal tmpKonkaiUriagegaku = 0;
            decimal tmpKonkaiSeiUtizeigaku = 0;
            decimal tmpKonkaiSeisotozeigaku = 0;
            decimal tmpKonkaiDenUtizeigaku = 0;
            decimal tmpKonkaiDensotozeigaku = 0;
            decimal tmpKonkaiMeiUtizeigaku = 0;
            decimal tmpKonkaiMeisotozeigaku = 0;
            decimal tmpKonkaiSeikyuugaku = 0;
            string gyoushaCd = "";
            string genbaCd = "";
            //集計金額を初期化
            //今回入金額
            konkaiNyuukingaku = 0;
            //今回調整額	
            konkaiChouseigaku = 0;
            //今回売上額	
            konkaiUriagegaku = 0;
            //今回請内税額	
            konkaiSeiUtizeigaku = 0;
            //今回請外税額	
            konkaiSeisotozeigaku = 0;
            //今回伝内税額	
            konkaiDenUtizeigaku = 0;
            //今回伝外税額	
            konkaiDensotozeigaku = 0;
            //今回明内税額	
            konkaiMeiUtizeigaku = 0;
            //今回明外税額	
            konkaiMeisotozeigaku = 0;
            //今回御請求額	
            konkaiSeikyuugaku = 0;

            // 書式区分別に売上データから集計
            switch (torihikisakiSeikyuuData.SHOSHIKI_KBN.ToString())
            {
                case "1":
                    // 請求先別
                    SumNyuukinUriageData(zenkaigaku, uriageData, nyuukinData, torihikisakiSeikyuuData);

                    break;

                case "2":
                    // 業者別
                    SumNyuukinData(nyuukinData);

                    for (int i = 0; i < uriageData.Rows.Count; i++)
                    {
                        if (gyoushaCd != uriageData.Rows[i]["GYOUSHA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = uriageData.Rows[i]["GYOUSHA_CD"].ToString();

                            SumUriageData("2", gyoushaCd, "", uriageData, torihikisakiSeikyuuData);

                            tmpKonkaiUriagegaku += konkaiUriagegaku;
                            tmpKonkaiSeiUtizeigaku += konkaiSeiUtizeigaku;
                            tmpKonkaiSeisotozeigaku += konkaiSeisotozeigaku;
                            tmpKonkaiDenUtizeigaku += konkaiDenUtizeigaku;
                            tmpKonkaiDensotozeigaku += konkaiDensotozeigaku;
                            tmpKonkaiMeiUtizeigaku += konkaiMeiUtizeigaku;
                            tmpKonkaiMeisotozeigaku += konkaiMeisotozeigaku;
                            tmpKonkaiSeikyuugaku += konkaiSeikyuugaku;

                            konkaiUriagegaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                            konkaiSeikyuugaku = 0;
                        }
                    }

                    konkaiUriagegaku = tmpKonkaiUriagegaku;
                    konkaiSeiUtizeigaku = tmpKonkaiSeiUtizeigaku;
                    konkaiSeisotozeigaku = tmpKonkaiSeisotozeigaku;
                    konkaiDenUtizeigaku = tmpKonkaiDenUtizeigaku;
                    konkaiDensotozeigaku = tmpKonkaiDensotozeigaku;
                    konkaiMeiUtizeigaku = tmpKonkaiMeiUtizeigaku;
                    konkaiMeisotozeigaku = tmpKonkaiMeisotozeigaku;
                    konkaiSeikyuugaku = zenkaigaku + tmpKonkaiSeikyuugaku - konkaiNyuukingaku - konkaiChouseigaku;

                    break;

                case "3":
                    // 現場別
                    SumNyuukinData(nyuukinData);

                    for (int i = 0; i < uriageData.Rows.Count; i++)
                    {
                        if (gyoushaCd != uriageData.Rows[i]["GYOUSHA_CD"].ToString() || genbaCd != uriageData.Rows[i]["GENBA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = uriageData.Rows[i]["GYOUSHA_CD"].ToString();
                            genbaCd = uriageData.Rows[i]["GENBA_CD"].ToString();

                            SumUriageData("3", gyoushaCd, genbaCd, uriageData, torihikisakiSeikyuuData);

                            tmpKonkaiUriagegaku += konkaiUriagegaku;
                            tmpKonkaiSeiUtizeigaku += konkaiSeiUtizeigaku;
                            tmpKonkaiSeisotozeigaku += konkaiSeisotozeigaku;
                            tmpKonkaiDenUtizeigaku += konkaiDenUtizeigaku;
                            tmpKonkaiDensotozeigaku += konkaiDensotozeigaku;
                            tmpKonkaiMeiUtizeigaku += konkaiMeiUtizeigaku;
                            tmpKonkaiMeisotozeigaku += konkaiMeisotozeigaku;
                            tmpKonkaiSeikyuugaku += konkaiSeikyuugaku;

                            konkaiUriagegaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                            konkaiSeikyuugaku = 0;
                        }
                    }
                    konkaiUriagegaku = tmpKonkaiUriagegaku;
                    konkaiSeiUtizeigaku = tmpKonkaiSeiUtizeigaku;
                    konkaiSeisotozeigaku = tmpKonkaiSeisotozeigaku;
                    konkaiDenUtizeigaku = tmpKonkaiDenUtizeigaku;
                    konkaiDensotozeigaku = tmpKonkaiDensotozeigaku;
                    konkaiMeiUtizeigaku = tmpKonkaiMeiUtizeigaku;
                    konkaiMeisotozeigaku = tmpKonkaiMeisotozeigaku;
                    konkaiSeikyuugaku = zenkaigaku + tmpKonkaiSeikyuugaku - konkaiNyuukingaku - konkaiChouseigaku;

                    break;
                default:
                    break;
            }

            zenkaigaku = konkaiSeikyuugaku;

            return zenkaigaku;
        }

        /// <summary>
        /// 月次処理(支払)を実行します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="zandaka"></param>
        private decimal ExecShiharaiGetsujiShoriHituke(GetsujiShoriDTOClass shiharaiDto, string ZandakaStartDay, string ZandakaEndDay, decimal zandaka)
        {
            // 支払データ取得
            DataTable shiharaiData = this.GetShiharaiData(shiharaiDto.TORIHIKISAKI_CD, ZandakaStartDay, ZandakaEndDay);
            // 出金データ取得
            DataTable shukkinData = this.GetShukkinData(shiharaiDto.TORIHIKISAKI_CD, ZandakaStartDay, ZandakaEndDay);
            // 取引先支払情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
            M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData;
            if (this.TorihikisakiShiharaiList != null && this.TorihikisakiShiharaiList.Count > 0)
            {
                torihikisakiShiharaiData = this.TorihikisakiShiharaiList.Find(delegate(M_TORIHIKISAKI_SHIHARAI data) { return data.TORIHIKISAKI_CD.Equals(shiharaiDto.TORIHIKISAKI_CD); });

                // Listに無いというのはありえないが念のため回避用
                if (torihikisakiShiharaiData == null)
                {
                    torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(shiharaiDto.TORIHIKISAKI_CD);
                }
            }
            else
            {
                torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(shiharaiDto.TORIHIKISAKI_CD);
            }

            // 支払月次データ作成開始
            return CreateShiharaiDataHituke(shiharaiDto, shiharaiData, shukkinData, torihikisakiShiharaiData, zandaka);
        }

        /// <summary>
        /// 支払月次処理データ作成
        /// </summary>
        /// <param name="uriageDto"></param>
        /// <param name="uriageData"></param>
        /// <param name="nyuukinData"></param>
        /// <param name="torihikisakiSeikyuuData"></param>
        private decimal CreateShiharaiDataHituke(GetsujiShoriDTOClass shiharaiDto, DataTable shiharaiData, DataTable shukkinData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData, decimal zandaka)
        {
            //前回繰越額
            decimal zenkaigaku = zandaka;

            // 足しこみ用TMP金額変数
            decimal tmpkonkaiShiharaigaku = 0;
            decimal tmpKonkaiSeiUtizeigaku = 0;
            decimal tmpKonkaiSeisotozeigaku = 0;
            decimal tmpKonkaiDenUtizeigaku = 0;
            decimal tmpKonkaiDensotozeigaku = 0;
            decimal tmpKonkaiMeiUtizeigaku = 0;
            decimal tmpKonkaiMeisotozeigaku = 0;
            decimal tmpKonkaiSeikyuugaku = 0;
            string gyoushaCd = "";
            string genbaCd = "";

            // 書式区分別に支払データから集計
            switch (torihikisakiShiharaiData.SHOSHIKI_KBN.ToString())
            {
                case "1":
                    // 請求先別
                    SumSyukkinShiharaiData(zenkaigaku, shiharaiData, shukkinData, torihikisakiShiharaiData);

                    break;

                case "2":
                    // 業者別
                    SumShukkinData(shukkinData);

                    for (int i = 0; i < shiharaiData.Rows.Count; i++)
                    {
                        if (gyoushaCd != shiharaiData.Rows[i]["GYOUSHA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = shiharaiData.Rows[i]["GYOUSHA_CD"].ToString();

                            SumShiharaiData("2", gyoushaCd, "", shiharaiData, torihikisakiShiharaiData);

                            tmpkonkaiShiharaigaku += konkaiShiharaigaku;
                            tmpKonkaiSeiUtizeigaku += konkaiSeiUtizeigaku;
                            tmpKonkaiSeisotozeigaku += konkaiSeisotozeigaku;
                            tmpKonkaiDenUtizeigaku += konkaiDenUtizeigaku;
                            tmpKonkaiDensotozeigaku += konkaiDensotozeigaku;
                            tmpKonkaiMeiUtizeigaku += konkaiMeiUtizeigaku;
                            tmpKonkaiMeisotozeigaku += konkaiMeisotozeigaku;
                            tmpKonkaiSeikyuugaku += konkaiSeikyuugaku;

                            konkaiShiharaigaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                            konkaiSeikyuugaku = 0;
                        }
                    }

                    konkaiShiharaigaku = tmpkonkaiShiharaigaku;
                    konkaiSeiUtizeigaku = tmpKonkaiSeiUtizeigaku;
                    konkaiSeisotozeigaku = tmpKonkaiSeisotozeigaku;
                    konkaiDenUtizeigaku = tmpKonkaiDenUtizeigaku;
                    konkaiDensotozeigaku = tmpKonkaiDensotozeigaku;
                    konkaiMeiUtizeigaku = tmpKonkaiMeiUtizeigaku;
                    konkaiMeisotozeigaku = tmpKonkaiMeisotozeigaku;
                    konkaiSeikyuugaku = zenkaigaku + tmpKonkaiSeikyuugaku - konkaiShukingaku - konkaiChouseigaku;

                    break;

                case "3":
                    // 現場別
                    SumShukkinData(shukkinData);

                    for (int i = 0; i < shiharaiData.Rows.Count; i++)
                    {
                        if (gyoushaCd != shiharaiData.Rows[i]["GYOUSHA_CD"].ToString() || genbaCd != shiharaiData.Rows[i]["GENBA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = shiharaiData.Rows[i]["GYOUSHA_CD"].ToString();
                            genbaCd = shiharaiData.Rows[i]["GENBA_CD"].ToString();

                            SumShiharaiData("3", gyoushaCd, genbaCd, shiharaiData, torihikisakiShiharaiData);

                            tmpkonkaiShiharaigaku += konkaiShiharaigaku;
                            tmpKonkaiSeiUtizeigaku += konkaiSeiUtizeigaku;
                            tmpKonkaiSeisotozeigaku += konkaiSeisotozeigaku;
                            tmpKonkaiDenUtizeigaku += konkaiDenUtizeigaku;
                            tmpKonkaiDensotozeigaku += konkaiDensotozeigaku;
                            tmpKonkaiMeiUtizeigaku += konkaiMeiUtizeigaku;
                            tmpKonkaiMeisotozeigaku += konkaiMeisotozeigaku;
                            tmpKonkaiSeikyuugaku += konkaiSeikyuugaku;

                            konkaiShiharaigaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                            konkaiSeikyuugaku = 0;
                        }
                    }

                    konkaiShiharaigaku = tmpkonkaiShiharaigaku;
                    konkaiSeiUtizeigaku = tmpKonkaiSeiUtizeigaku;
                    konkaiSeisotozeigaku = tmpKonkaiSeisotozeigaku;
                    konkaiDenUtizeigaku = tmpKonkaiDenUtizeigaku;
                    konkaiDensotozeigaku = tmpKonkaiDensotozeigaku;
                    konkaiMeiUtizeigaku = tmpKonkaiMeiUtizeigaku;
                    konkaiMeisotozeigaku = tmpKonkaiMeisotozeigaku;
                    konkaiSeikyuugaku = zenkaigaku + tmpKonkaiSeikyuugaku - konkaiShukingaku - konkaiChouseigaku;

                    break;

                default:
                    break;
            }

            zenkaigaku = konkaiSeikyuugaku;

            return zenkaigaku;
        }

        /// <summary>
        /// 締
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="getujiCheck"></param>
        /// <param name="continues"></param>
        /// <param name="ZandakaStartDay"></param>
        /// <param name="ZandakaEndDay"></param>
        /// <param name="zandaka"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <param name="windowId"></param>
        /// <param name="shoriType"></param>
        /// <returns></returns>
        public bool GetsujiShoriSeikyuu(string torihikisakiCd, bool continues, DateTime ZandakaStartDay, DateTime ZandakaEndDay, decimal zandaka, DateTime startDay, DateTime endDay, int Invoicekbn,
            r_framework.Const.WINDOW_ID windowId = r_framework.Const.WINDOW_ID.NONE, GETSUJI_SHORI_TYPE shoriType = GETSUJI_SHORI_TYPE.BOTH)
        {
            LogUtility.DebugMethodStart(torihikisakiCd, continues, ZandakaStartDay, ZandakaEndDay, zandaka, startDay, endDay, Invoicekbn, windowId, shoriType);

            LInvoceKBN = Invoicekbn;

            #region 売上月次処理

            if ((shoriType & GETSUJI_SHORI_TYPE.SEIKYUU) == GETSUJI_SHORI_TYPE.SEIKYUU)
            {
                // 取引先_請求情報取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
                // ※月次処理実行時の速度問題によりListを優先で使用。（Listが空の場合は月次処理画面以外からの呼び出し）
                M_TORIHIKISAKI_SEIKYUU mtse;
                if (this.TorihikisakiSeikyuList != null && this.TorihikisakiSeikyuList.Count > 0)
                {
                    mtse = this.TorihikisakiSeikyuList.Find(delegate (M_TORIHIKISAKI_SEIKYUU data) { return data.TORIHIKISAKI_CD.Equals(torihikisakiCd); });

                    // Listに無いというのはありえないが念のため回避用
                    if (mtse == null)
                    {
                        mtse = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                    }
                }
                else
                {
                    mtse = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                }

                if (mtse != null && mtse.TORIHIKI_KBN_CD == 2)
                {
                    /* 取引先の設定が掛けの場合、伝票データをから当月の金額を算出する */

                    // 売上用のDTO作成
                    GetsujiShoriDTOClass uriageDto = new GetsujiShoriDTOClass();
                    uriageDto.TORIHIKISAKI_CD = torihikisakiCd;

                    if (continues)
                    {
                        if (ZandakaStartDay == DateTime.MinValue)
                        {
                            // 売上データ作成開始
                            uriageDto.PREVIOUS_MONTH_BALANCE = this.ExecUriageGetsujiShoriSeikyuu(uriageDto, string.Empty, ZandakaEndDay.ToString(), zandaka);
                        }
                        else
                        {
                            // 売上データ作成開始
                            uriageDto.PREVIOUS_MONTH_BALANCE = this.ExecUriageGetsujiShoriSeikyuu(uriageDto, ZandakaStartDay.ToString(), ZandakaEndDay.ToString(), zandaka);
                        }
                    }
                    else
                    {
                        uriageDto.PREVIOUS_MONTH_BALANCE = zandaka;
                    }

                    uriageDto.FROM_DATE = startDay.ToString("yyyy/MM/dd");
                    uriageDto.TO_DATE = endDay.ToString("yyyy/MM/dd");

                    // 売上データ作成開始
                    this.ExecUriageGetsujiShoriSeikyuu(uriageDto);
                }
                else
                {
                    /* 取引先の設定が現金の場合、当月の金額は0円とし過去データがある場合は繰越残高だけ引き継いだデータを作成する */

                    /* 過去データがあるか調べ、ある場合は残高を取得 */
                    decimal zandakas = 0;
                    DateTime shimeDate = new DateTime(startDay.Year, startDay.Month, 1);
                    DataTable latestGetsujiData = this.dao.GetLatestGetsujiDateByTorihikisakiCd(torihikisakiCd, shimeDate.ToString("yyyy/MM/dd"));
                    if (latestGetsujiData != null && latestGetsujiData.Rows.Count > 0)
                    {
                        int latestGetsujiShoriYear = int.Parse(latestGetsujiData.Rows[0]["YEAR"].ToString());
                        int latestGetsujiShoriMonth = int.Parse(latestGetsujiData.Rows[0]["MONTH"].ToString());
                        this.GetUriageKurikosigaku(torihikisakiCd, latestGetsujiShoriYear, latestGetsujiShoriMonth, ref zandakas);
                    }

                    /* 売上月次処理データ作成 */
                    T_MONTHLY_LOCK_UR monthlyLockUrData = new T_MONTHLY_LOCK_UR();
                    //取引先
                    monthlyLockUrData.TORIHIKISAKI_CD = torihikisakiCd;
                    //年
                    monthlyLockUrData.YEAR = short.Parse(startDay.Year.ToString());
                    //月
                    monthlyLockUrData.MONTH = short.Parse(startDay.Month.ToString());
                    //繰越残高
                    monthlyLockUrData.PREVIOUS_MONTH_BALANCE = zandakas;
                    //入金額
                    monthlyLockUrData.NYUUKIN_KINGAKU = 0;
                    //税抜金額
                    monthlyLockUrData.KINGAKU = 0;
                    //消費税額
                    monthlyLockUrData.TAX = 0;
                    //締内税額	
                    monthlyLockUrData.SHIME_UTIZEI_GAKU = 0;
                    //締外税額	
                    monthlyLockUrData.SHIME_SOTOZEI_GAKU = 0;
                    //伝内税額	
                    monthlyLockUrData.DEN_UTIZEI_GAKU = 0;
                    //伝外税額	
                    monthlyLockUrData.DEN_SOTOZEI_GAKU = 0;
                    //明内税額	
                    monthlyLockUrData.MEI_UTIZEI_GAKU = 0;
                    //明外税額	
                    monthlyLockUrData.MEI_SOTOZEI_GAKU = 0;
                    //税込金額
                    monthlyLockUrData.TOTAL_KINGAKU = 0;
                    //調整前差引残高
                    monthlyLockUrData.ZANDAKA = zandakas;
                    //適格区分(1.旧 2.適格請求)
                    monthlyLockUrData.INVOICE_KBN = LInvoceKBN;

                    // 月次処理データを格納
                    DateTime keyDate = new DateTime(startDay.Year, startDay.Month, 1);
                    string keyString = keyDate.ToString("yyyy/MM");
                    if (this.MonthlyLockUrDatas.ContainsKey(keyString))
                    {
                        List<T_MONTHLY_LOCK_UR> dataList = this.MonthlyLockUrDatas[keyString];
                        dataList.Add(monthlyLockUrData);
                    }
                    else
                    {
                        List<T_MONTHLY_LOCK_UR> dataList = new List<T_MONTHLY_LOCK_UR>();
                        dataList.Add(monthlyLockUrData);
                        this.MonthlyLockUrDatas.Add(keyString, dataList);
                    }
                }
            }

            #endregion

            #region 支払月次処理

            if ((shoriType & GETSUJI_SHORI_TYPE.SHIHARAI) == GETSUJI_SHORI_TYPE.SHIHARAI)
            {
                // 取引先_請求情報取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
                // ※月次処理実行時の速度問題によりListを優先で使用。（Listが空の場合は月次処理画面以外からの呼び出し）
                M_TORIHIKISAKI_SEIKYUU mtse;
                if (this.TorihikisakiSeikyuList != null && this.TorihikisakiSeikyuList.Count > 0)
                {
                    mtse = this.TorihikisakiSeikyuList.Find(delegate(M_TORIHIKISAKI_SEIKYUU data) { return data.TORIHIKISAKI_CD.Equals(torihikisakiCd); });

                    // Listに無いというのはありえないが念のため回避用
                    if (mtse == null)
                    {
                        mtse = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                    }
                }
                else
                {
                    mtse = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                }

                if (mtse != null && mtse.TORIHIKI_KBN_CD == 2)
                {
                    /* 取引先の設定が掛けの場合、伝票データをから当月の金額を算出する */

                    GetsujiShoriDTOClass shiharaiDto = new GetsujiShoriDTOClass();
                    shiharaiDto.TORIHIKISAKI_CD = torihikisakiCd;

                    if (continues)
                    {
                        if (ZandakaStartDay == DateTime.MinValue)
                        {
                            // 支払データ作成開始
                            shiharaiDto.PREVIOUS_MONTH_BALANCE = this.ExecShiharaiGetsujiShoriShiharai(shiharaiDto, string.Empty, ZandakaEndDay.ToString(), zandaka);
                        }
                        else
                        {
                            // 支払データ作成開始
                            shiharaiDto.PREVIOUS_MONTH_BALANCE = this.ExecShiharaiGetsujiShoriShiharai(shiharaiDto, ZandakaStartDay.ToString(), ZandakaEndDay.ToString(), zandaka);
                        }
                    }
                    else
                    {
                        shiharaiDto.PREVIOUS_MONTH_BALANCE = zandaka;
                    }

                    shiharaiDto.FROM_DATE = startDay.ToString("yyyy/MM/dd");
                    shiharaiDto.TO_DATE = endDay.ToString("yyyy/MM/dd");

                    // 支払データ作成開始
                    this.ExecUriageGetsujiShoriShiharai(shiharaiDto);
                }
                else
                {
                    /* 取引先の設定が現金の場合、当月の金額は0円とし過去データがある場合は繰越残高だけ引き継いだデータを作成する */

                    /* 過去データがあるか調べ、ある場合は残高を取得 */
                    decimal zandakas = 0;
                    DateTime shimeDate = new DateTime(startDay.Year, startDay.Month, 1);
                    DataTable latestGetsujiData = this.dao.GetLatestGetsujiDateByTorihikisakiCd(torihikisakiCd, shimeDate.ToString("yyyy/MM/dd"));
                    if (latestGetsujiData != null && latestGetsujiData.Rows.Count > 0)
                    {
                        int latestGetsujiShoriYear = int.Parse(latestGetsujiData.Rows[0]["YEAR"].ToString());
                        int latestGetsujiShoriMonth = int.Parse(latestGetsujiData.Rows[0]["MONTH"].ToString());
                        this.GetUriageKurikosigaku(torihikisakiCd, latestGetsujiShoriYear, latestGetsujiShoriMonth, ref zandakas);
                    }

                    /* 売上月次処理データ作成 */
                    T_MONTHLY_LOCK_SH monthlyLockShData = new T_MONTHLY_LOCK_SH();
                    //取引先
                    monthlyLockShData.TORIHIKISAKI_CD = torihikisakiCd;
                    //年
                    monthlyLockShData.YEAR = short.Parse(startDay.Year.ToString());
                    //月
                    monthlyLockShData.MONTH = short.Parse(startDay.Month.ToString());
                    //繰越残高
                    monthlyLockShData.PREVIOUS_MONTH_BALANCE = zandakas;
                    //出金額
                    monthlyLockShData.SHUKKIN_KINGAKU = 0;
                    //税抜金額
                    monthlyLockShData.KINGAKU = 0;
                    //消費税額
                    monthlyLockShData.TAX = 0;
                    //締内税額	
                    monthlyLockShData.SHIME_UTIZEI_GAKU = 0;
                    //締外税額	
                    monthlyLockShData.SHIME_SOTOZEI_GAKU = 0;
                    //伝内税額	
                    monthlyLockShData.DEN_UTIZEI_GAKU = 0;
                    //伝外税額	
                    monthlyLockShData.DEN_SOTOZEI_GAKU = 0;
                    //明内税額	
                    monthlyLockShData.MEI_UTIZEI_GAKU = 0;
                    //明外税額	
                    monthlyLockShData.MEI_SOTOZEI_GAKU = 0;
                    //税込金額
                    monthlyLockShData.TOTAL_KINGAKU = 0;
                    //調整前差引残高
                    monthlyLockShData.ZANDAKA = zandakas;
                    //適格区分(1.旧処理、2.適格対応)
                    monthlyLockShData.INVOICE_KBN = LInvoceKBN;

                    // 月次処理データを格納
                    DateTime keyDate = new DateTime(startDay.Year, startDay.Month, 1);
                    string keyString = keyDate.ToString("yyyy/MM");
                    if (this.MonthlyLockShDatas.ContainsKey(keyString))
                    {
                        List<T_MONTHLY_LOCK_SH> dataList = this.MonthlyLockShDatas[keyString];
                        dataList.Add(monthlyLockShData);
                    }
                    else
                    {
                        List<T_MONTHLY_LOCK_SH> dataList = new List<T_MONTHLY_LOCK_SH>();
                        dataList.Add(monthlyLockShData);
                        this.MonthlyLockShDatas.Add(keyString, dataList);
                    }
                }
            }

            #endregion

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 締処理(売上)を実行します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="zandaka"></param>
        private decimal ExecUriageGetsujiShoriSeikyuu(GetsujiShoriDTOClass uriageDto, string ZandakaStartDay, string ZandakaEndDay, decimal zandaka)
        {
            // 売上データ取得
            DataTable uriageData = this.GetUriageDataSeikyuu(uriageDto.TORIHIKISAKI_CD, ZandakaStartDay, ZandakaEndDay);
            // 入金データ取得
            DataTable nyuukinData = this.GetNyuukinDataSeikyuu(uriageDto.TORIHIKISAKI_CD, ZandakaStartDay, ZandakaEndDay);
            // 取引先請求情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
            M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData;
            if (this.TorihikisakiSeikyuList != null && this.TorihikisakiSeikyuList.Count > 0)
            {
                torihikisakiSeikyuuData = this.TorihikisakiSeikyuList.Find(delegate (M_TORIHIKISAKI_SEIKYUU data) { return data.TORIHIKISAKI_CD.Equals(uriageDto.TORIHIKISAKI_CD); });

                // Listに無いというのはありえないが念のため回避用
                if (torihikisakiSeikyuuData == null)
                {
                    torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(uriageDto.TORIHIKISAKI_CD);
                }
            }
            else
            {
                torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(uriageDto.TORIHIKISAKI_CD);
            }

            // 売上データ作成開始
            return CreateUriageDataHituke(uriageDto, uriageData, nyuukinData, torihikisakiSeikyuuData, zandaka);
        }

        /// <summary>
        /// 受入、出荷、売上/支払伝票の売上データを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="fromDate">[任意]取得する期間FROM</param>
        /// <param name="toDate">取得する期間TO</param>
        /// <returns></returns>
        private DataTable GetUriageDataSeikyuu(string torihikisakiCd, string fromDate, string toDate)
        {
            DataTable uriageData;

            // 受入データ取得
            DataTable ukeireData = this.dao.GetUriageSeikyuuData(2, torihikisakiCd, fromDate, toDate);
            ukeireData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in ukeireData.Rows)
            {
                row["DENPYOUSHURUI"] = 1;
            }

            // 出荷データ取得
            DataTable shukkaData = this.dao.GetUriageSeikyuuData(3, torihikisakiCd, fromDate, toDate);
            shukkaData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in shukkaData.Rows)
            {
                row["DENPYOUSHURUI"] = 2;
            }

            // 売上/支払データ取得
            DataTable uriageShiharaiData = this.dao.GetUriageSeikyuuData(4, torihikisakiCd, fromDate, toDate);
            uriageShiharaiData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in uriageShiharaiData.Rows)
            {
                row["DENPYOUSHURUI"] = 3;
            }

            // ソート用DataTable
            DataTable sorturiageData = new DataTable();

            //データコピー
            if (ukeireData != null)
            {
                sorturiageData = ukeireData.Clone();
            }
            else if (shukkaData != null)
            {
                sorturiageData = shukkaData.Clone();
            }
            else if (uriageShiharaiData != null)
            {
                sorturiageData = uriageShiharaiData.Clone();
            }

            if (ukeireData != null)
            {
                foreach (DataRow r in ukeireData.Rows)
                {
                    sorturiageData.ImportRow(r);
                }
            }

            if (shukkaData != null)
            {
                foreach (DataRow r in shukkaData.Rows)
                {
                    sorturiageData.ImportRow(r);
                }
            }

            if (uriageShiharaiData != null)
            {
                foreach (DataRow r in uriageShiharaiData.Rows)
                {
                    sorturiageData.ImportRow(r);
                }
            }

            DataView dv = new DataView(sorturiageData);
            uriageData = sorturiageData.Clone();
            dv.Sort = "TORIHIKISAKI_CD,GYOUSHA_CD,GENBA_CD";
            foreach (DataRowView drv in dv)
            {
                uriageData.ImportRow(drv.Row);
            }

            return uriageData;
        }

        /// <summary>
        /// 入金データを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="fromDate">[任意]取得する期間FROM</param>
        /// <param name="toDate">取得する期間TO</param>
        /// <returns></returns>
        private DataTable GetNyuukinDataSeikyuu(string torihikisakiCd, string fromDate, string toDate)
        {
            // 入金データ取得
            DataTable nyuukinData = this.dao.GetNyuukinSeikyuuData(torihikisakiCd, fromDate, toDate);
            return nyuukinData;
        }

        /// <summary>
        /// 月次処理(売上)を実行します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="zandaka"></param>
        private void ExecUriageGetsujiShoriSeikyuu(GetsujiShoriDTOClass uriageDto)
        {
            // 売上データ取得
            DataTable uriageData = this.GetUriageDataSeikyuu(uriageDto.TORIHIKISAKI_CD, uriageDto.FROM_DATE, uriageDto.TO_DATE);
            // 入金データ取得
            DataTable nyuukinData = this.GetNyuukinDataSeikyuu(uriageDto.TORIHIKISAKI_CD, uriageDto.FROM_DATE, uriageDto.TO_DATE);
            // 取引先請求情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
            M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData;
            if (this.TorihikisakiSeikyuList != null && this.TorihikisakiSeikyuList.Count > 0)
            {
                torihikisakiSeikyuuData = this.TorihikisakiSeikyuList.Find(delegate (M_TORIHIKISAKI_SEIKYUU data) { return data.TORIHIKISAKI_CD.Equals(uriageDto.TORIHIKISAKI_CD); });

                // Listに無いというのはありえないが念のため回避用
                if (torihikisakiSeikyuuData == null)
                {
                    torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(uriageDto.TORIHIKISAKI_CD);
                }
            }
            else
            {
                torihikisakiSeikyuuData = this.torihikisakiSeikyuuDao.GetDataByCd(uriageDto.TORIHIKISAKI_CD);
            }

            // 売上データ作成開始
            CreateUriageGetsujiData(uriageDto, uriageData, nyuukinData, torihikisakiSeikyuuData);
        }

        /// <summary>
        /// 締処理(支払)を実行します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="zandaka"></param>
        private decimal ExecShiharaiGetsujiShoriShiharai(GetsujiShoriDTOClass shiharaiDto, string ZandakaStartDay, string ZandakaEndDay, decimal zandaka)
        {
            // 支払データ取得
            DataTable shiharaiData = this.GetShiharaiDataShiharai(shiharaiDto.TORIHIKISAKI_CD, ZandakaStartDay, ZandakaEndDay);
            // 出金データ取得
            DataTable shukkinData = this.GetShukkinDataShiharai(shiharaiDto.TORIHIKISAKI_CD, ZandakaStartDay, ZandakaEndDay);
            // 取引先支払情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
            M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData;
            if (this.TorihikisakiShiharaiList != null && this.TorihikisakiShiharaiList.Count > 0)
            {
                torihikisakiShiharaiData = this.TorihikisakiShiharaiList.Find(delegate(M_TORIHIKISAKI_SHIHARAI data) { return data.TORIHIKISAKI_CD.Equals(shiharaiDto.TORIHIKISAKI_CD); });

                // Listに無いというのはありえないが念のため回避用
                if (torihikisakiShiharaiData == null)
                {
                    torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(shiharaiDto.TORIHIKISAKI_CD);
                }
            }
            else
            {
                torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(shiharaiDto.TORIHIKISAKI_CD);
            }

            // 支払月次データ作成開始
            return CreateShiharaiDataHituke(shiharaiDto, shiharaiData, shukkinData, torihikisakiShiharaiData, zandaka);
        }

        /// <summary>
        /// 受入、出荷、売上/支払伝票の支払データを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="fromDate">[任意]取得する期間FROM</param>
        /// <param name="toDate">取得する期間TO</param>
        /// <returns></returns>
        private DataTable GetShiharaiDataShiharai(string torihikisakiCd, string fromDate, string toDate)
        {
            DataTable shiharaiData;

            // 受入データ取得
            DataTable ukeireData = this.dao.GetShiharaiSeikyuuData(2, torihikisakiCd, fromDate, toDate);
            ukeireData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in ukeireData.Rows)
            {
                row["DENPYOUSHURUI"] = 1;
            }

            // 出荷データ取得
            DataTable shukkaData = this.dao.GetShiharaiSeikyuuData(3, torihikisakiCd, fromDate, toDate);
            shukkaData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in shukkaData.Rows)
            {
                row["DENPYOUSHURUI"] = 2;
            }

            // 売上/支払データ取得
            DataTable uriageShiharaiData = this.dao.GetShiharaiSeikyuuData(4, torihikisakiCd, fromDate, toDate);
            uriageShiharaiData.Columns.Add("DENPYOUSHURUI", typeof(int));
            foreach (DataRow row in uriageShiharaiData.Rows)
            {
                row["DENPYOUSHURUI"] = 3;
            }

            // ソート用DataTable
            DataTable sorturiageData = new DataTable();

            //データコピー
            if (ukeireData != null)
            {
                sorturiageData = ukeireData.Clone();
            }
            else if (shukkaData != null)
            {
                sorturiageData = shukkaData.Clone();
            }
            else if (uriageShiharaiData != null)
            {
                sorturiageData = uriageShiharaiData.Clone();
            }

            if (ukeireData != null)
            {
                foreach (DataRow r in ukeireData.Rows)
                {
                    sorturiageData.ImportRow(r);
                }
            }

            if (shukkaData != null)
            {
                foreach (DataRow r in shukkaData.Rows)
                {
                    sorturiageData.ImportRow(r);
                }
            }

            if (uriageShiharaiData != null)
            {
                foreach (DataRow r in uriageShiharaiData.Rows)
                {
                    sorturiageData.ImportRow(r);
                }
            }

            DataView dv = new DataView(sorturiageData);
            shiharaiData = sorturiageData.Clone();
            dv.Sort = "TORIHIKISAKI_CD,GYOUSHA_CD,GENBA_CD";
            foreach (DataRowView drv in dv)
            {
                shiharaiData.ImportRow(drv.Row);
            }

            return shiharaiData;
        }

        /// <summary>
        /// 入金データを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="fromDate">[任意]取得する期間FROM</param>
        /// <param name="toDate">取得する期間TO</param>
        /// <returns></returns>
        private DataTable GetShukkinDataShiharai(string torihikisakiCd, string fromDate, string toDate)
        {
            // 入金データ取得
            DataTable shukkinData = this.dao.GetShukkinShiharaiData(torihikisakiCd, fromDate, toDate);
            return shukkinData;
        }

        /// <summary>
        /// 月次処理(支払)を実行します
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="zandaka"></param>
        private void ExecUriageGetsujiShoriShiharai(GetsujiShoriDTOClass shiharaiDto)
        {
            // 支払データ取得
            DataTable shiharaiData = this.GetShiharaiDataShiharai(shiharaiDto.TORIHIKISAKI_CD, shiharaiDto.FROM_DATE, shiharaiDto.TO_DATE);
            // 出金データ取得
            DataTable shukkinData = this.GetShukkinDataShiharai(shiharaiDto.TORIHIKISAKI_CD, shiharaiDto.FROM_DATE, shiharaiDto.TO_DATE);
            // 取引先支払情報マスタ取得(FieldのListがある場合はそちらを使用し、無い場合はDBから取得)
            M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData;
            if (this.TorihikisakiShiharaiList != null && this.TorihikisakiShiharaiList.Count > 0)
            {
                torihikisakiShiharaiData = this.TorihikisakiShiharaiList.Find(delegate(M_TORIHIKISAKI_SHIHARAI data) { return data.TORIHIKISAKI_CD.Equals(shiharaiDto.TORIHIKISAKI_CD); });

                // Listに無いというのはありえないが念のため回避用
                if (torihikisakiShiharaiData == null)
                {
                    torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(shiharaiDto.TORIHIKISAKI_CD);
                }
            }
            else
            {
                torihikisakiShiharaiData = this.torihikisakiShiharaiDao.GetDataByCd(shiharaiDto.TORIHIKISAKI_CD);
            }

            // 支払月次データ作成開始
            CreateShiharaiGetsujiData(shiharaiDto, shiharaiData, shukkinData, torihikisakiShiharaiData);
        }


        #region 売上データ算出(請求先別)_月次既存処理用

        /// <summary>
        /// 売上データ算出(請求先別)_月次既存処理用
        /// ※月次処理用に既存の処理を別関数名で退避
        /// </summary>
        /// <param name="uriageData"></param>
        /// <param name="torihikisakiSeikyuuData"></param>
        private void CreatetSeikyuuUriageData_OldInvoice(DataTable uriageData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData)
        {
            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            int zikubun = 0;
            decimal shohizeirate = 0;
            int count = 0;
            string torihikicd = "";

            // 請求毎税の計算
            DataView dv = new DataView(uriageData);
            DataTable table = uriageData.Clone();
            dv.Sort = "URIAGE_ZEI_KEISAN_KBN_CD,URIAGE_ZEI_KBN_CD,URIAGE_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                CreatetUriageData(table.Rows[i]);
            }

            if (uriageData.Rows.Count != 0)
            {
                //保存用システムＩＤ、ＳＥＱの初期化
                saveSysID = "";
                saveDenshuKbn = "";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == "2")
                    {
                        //初回
                        if (count == 0)
                        {
                            zikubun = int.Parse(table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString());
                            shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());
                            torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                        }


                        if ((int.Parse(table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString()) == zikubun) &&
                            (decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString()) == shohizeirate))
                        {
                            if (!(saveSysID.Equals(table.Rows[i]["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(table.Rows[i]["DENPYOUSHURUI"].ToString())))
                            {
                                uriageTotal = uriageTotal +
                                                Decimal.Parse(BlankToZero(table.Rows[i]["URIAGE_AMOUNT_TOTAL"].ToString()));

                                //システムID、SEQの保存
                                saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                            }
                        }
                        else
                        {
                            //税区分が外税:1
                            if (zikubun == 1)
                            {
                                //請求毎の消費税額算出
                                konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                            CreateSeikyuuTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSeikyuuData);

                                uriageTotal = 0;

                            }
                            //税区分が内税
                            else if (zikubun == 2)
                            {
                                //請求毎の消費税額算出
                                konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                            CreateSeikyuuTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSeikyuuData);

                                uriageTotal = 0;
                            }

                            uriageTotal = uriageTotal +
                                            Decimal.Parse(BlankToZero(table.Rows[i]["URIAGE_AMOUNT_TOTAL"].ToString()));

                            //システムID、SEQの保存
                            saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                            saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();

                            //税区分と消費税の保存
                            zikubun = int.Parse(table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString());
                            shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());
                            torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                        }

                        //カウントアップ
                        count++;
                    }
                }

                if (uriageTotal != 0)
                {
                    //税区分が外税:1
                    if (zikubun == 1)
                    {
                        //請求毎の消費税額算出
                        konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                 CreateSeikyuuTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSeikyuuData);
                    }

                    //税区分が内税:2
                    if (zikubun == 2)
                    {
                        //請求毎の消費税額算出
                        konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                 CreateSeikyuuTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSeikyuuData);
                    }
                }
            }

            //今回売上額
            konkaiUriagegaku = konkaiUriagegaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
        }

        #endregion

        #region 売上データ算出(業者別)_月次既存処理用

        /// <summary>
        /// 売上データ算出(業者別)_月次既存処理用
        /// ※月次処理用に既存の処理を別関数名で退避
        /// </summary>
        private void CreatetGyoushaUriageData_OldInvoice(DataTable uriageData, M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuuData, string gyoushaCd)
        {
            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            int zikubun = 0;
            decimal shohizeirate = 0;
            int count = 0;
            string torihikicd = "";

            // 請求毎税の計算
            DataView dv = new DataView(uriageData);
            DataTable table = uriageData.Clone();
            dv.Sort = "GYOUSHA_CD,URIAGE_ZEI_KEISAN_KBN_CD,URIAGE_ZEI_KBN_CD,URIAGE_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
                {
                    CreatetUriageData(table.Rows[i]);
                }
            }

            if (uriageData.Rows.Count != 0)
            {
                //保存用システムＩＤ、ＳＥＱの初期化
                saveSysID = "";
                saveDenshuKbn = "";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (uriageData.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
                    {
                        if (table.Rows[i]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == "2")
                        {
                            //初回
                            if (count == 0)
                            {
                                zikubun = int.Parse(table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString());
                                shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());
                                torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            if ((uriageData.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd) &&
                                    (int.Parse(table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString()) == zikubun) &&
                                   (decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString()) == shohizeirate))
                            {
                                if (!(saveSysID.Equals(table.Rows[i]["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(table.Rows[i]["DENPYOUSHURUI"].ToString())))
                                {
                                    uriageTotal = uriageTotal +
                                                    Decimal.Parse(BlankToZero(table.Rows[i]["URIAGE_AMOUNT_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }
                            }
                            else
                            {
                                //税区分が外税:1
                                if (zikubun == 1)
                                {
                                    //請求毎の消費税額算出
                                    konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                             CreateSeikyuuTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSeikyuuData);

                                    uriageTotal = 0;
                                }
                                //税区分が内税
                                else if (zikubun == 2)
                                {
                                    //請求毎の消費税額算出
                                    konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                             CreateSeikyuuTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSeikyuuData);

                                    uriageTotal = 0;
                                }

                                uriageTotal = uriageTotal +
                                                Decimal.Parse(BlankToZero(table.Rows[i]["URIAGE_AMOUNT_TOTAL"].ToString()));

                                //システムID、SEQの保存
                                saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();

                                //税区分と消費税の保存
                                zikubun = int.Parse(table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString());
                                shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());
                                torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            //カウントアップ
                            count++;
                        }
                    }
                }

                if (uriageTotal != 0)
                {
                    //税区分が外税:1
                    if (zikubun == 1)
                    {
                        //請求毎の消費税額算出
                        konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                 CreateSeikyuuTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSeikyuuData);
                    }

                    //税区分が内税:2
                    if (zikubun == 2)
                    {
                        //請求毎の消費税額算出
                        konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                 CreateSeikyuuTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSeikyuuData);
                    }
                }
            }
            //今回売上額
            konkaiUriagegaku = konkaiUriagegaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
        }

        #endregion

        #region 売上データ算出(現場別)_月次既存処理用

        /// <summary>
        /// 売上データ算出(現場別)_月次既存処理用
        /// ※月次処理用に既存の処理を別関数名で退避
        /// </summary>
        private void CreatetGenbaUriageData_OldInvoice(DataTable uriageData, M_TORIHIKISAKI_SEIKYUU torihikisakiSEikyuuData, string gyoushaCd, string genbaCd)
        {
            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            int zikubun = 0;
            decimal shohizeirate = 0;
            int count = 0;
            string torihikicd = "";

            // 請求毎税の計算
            DataView dv = new DataView(uriageData);
            DataTable table = uriageData.Clone();
            dv.Sort = "GYOUSHA_CD,GENBA_CD,URIAGE_ZEI_KEISAN_KBN_CD,URIAGE_ZEI_KBN_CD,URIAGE_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && table.Rows[i]["GENBA_CD"].ToString() == genbaCd)
                {
                    CreatetUriageData(table.Rows[i]);
                }
            }

            if (uriageData.Rows.Count != 0)
            {
                //保存用システムＩＤ、ＳＥＱの初期化
                saveSysID = "";
                saveDenshuKbn = "";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (uriageData.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && uriageData.Rows[i]["GENBA_CD"].ToString() == genbaCd)
                    {
                        if (table.Rows[i]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == "2")
                        {
                            //初回
                            if (count == 0)
                            {
                                zikubun = int.Parse(table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString());
                                shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());
                                torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            if ((uriageData.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd) &&
                                    (int.Parse(table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString()) == zikubun) &&
                                   (decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString()) == shohizeirate))
                            {
                                if (!(saveSysID.Equals(table.Rows[i]["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(table.Rows[i]["DENPYOUSHURUI"].ToString())))
                                {
                                    uriageTotal = uriageTotal +
                                                    Decimal.Parse(BlankToZero(table.Rows[i]["URIAGE_AMOUNT_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }
                            }
                            else
                            {
                                //税区分が外税:1
                                if (zikubun == 1)
                                {
                                    //請求毎の消費税額算出
                                    konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                             CreateSeikyuuTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSEikyuuData);

                                    uriageTotal = 0;
                                }
                                //税区分が内税
                                else if (zikubun == 2)
                                {
                                    //請求毎の消費税額算出
                                    konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                             CreateSeikyuuTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSEikyuuData);

                                    uriageTotal = 0;
                                }

                                uriageTotal = uriageTotal +
                                                Decimal.Parse(BlankToZero(table.Rows[i]["URIAGE_AMOUNT_TOTAL"].ToString()));

                                //システムID、SEQの保存
                                saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();

                                //税区分と消費税の保存
                                zikubun = int.Parse(table.Rows[i]["URIAGE_ZEI_KBN_CD"].ToString());
                                shohizeirate = decimal.Parse(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString());
                                torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            //カウントアップ
                            count++;
                        }
                    }
                }

                if (uriageTotal != 0)
                {
                    //税区分が外税:1
                    if (zikubun == 1)
                    {
                        //請求毎の消費税額算出
                        konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                 CreateSeikyuuTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSEikyuuData);
                    }

                    //税区分が内税:2
                    if (zikubun == 2)
                    {
                        //請求毎の消費税額算出
                        konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                 CreateSeikyuuTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiSEikyuuData);
                    }
                }
            }
            //今回売上額
            konkaiUriagegaku = konkaiUriagegaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

        }

        #endregion

        #region 支払データ算出(支払先別)_月次既存処理用

        /// <summary>
        /// 支払データ算出(支払先別)_月次既存処理用
        /// ※月次処理用に既存の処理を別関数名で退避
        /// </summary>
        /// <param name="shiharaiData"></param>
        /// <param name="torihikisakiShiharaiData"></param>
        private void CreatetSeikyuuShiharaiData_OldInvoice(DataTable shiharaiData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData)
        {
            // 精算毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            int zikubun = 0;
            decimal shohizeirate = 0;
            int count = 0;
            string torihikicd = "";

            // 精算毎税の計算
            DataView dv = new DataView(shiharaiData);
            DataTable table = shiharaiData.Clone();
            dv.Sort = "SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SHIHARAI_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            // 精算毎税以外の計算
            for (int i = 0; i < table.Rows.Count; i++)
            {
                CreatetShiharaiData(table.Rows[i]);
            }

            if (shiharaiData.Rows.Count != 0)
            {
                // 保存用システムID、伝種区分の初期化
                saveSysID = "";
                saveDenshuKbn = "";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == "2")
                    {
                        //初回
                        if (count == 0)
                        {
                            zikubun = int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString());
                            shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                            torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                        }


                        if ((int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()) == zikubun) &&
                           (decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()) == shohizeirate))
                        {
                            if (!(saveSysID.Equals(table.Rows[i]["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(table.Rows[i]["DENPYOUSHURUI"].ToString())))
                            {
                                uriageTotal = uriageTotal +
                                                 Decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                //システムID、SEQの保存
                                saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                            }
                        }
                        else
                        {
                            //税区分が外税:1
                            if (zikubun == 1)
                            {
                                //請求毎の消費税額算出
                                konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                         CreateSeisanTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);

                                //クリア
                                uriageTotal = 0;

                                //集計
                                uriageTotal = uriageTotal +
                                             Decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                //システムID、SEQの保存
                                saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                            }

                            //税区分が内税:2
                            if (zikubun == 2)
                            {
                                //請求毎の消費税額算出
                                konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                         CreateSeisanTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);

                                //クリア
                                uriageTotal = 0;

                                //集計
                                uriageTotal = uriageTotal +
                                             Decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                //システムID、SEQの保存
                                saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                            }

                            //税区分と消費税の保存
                            zikubun = int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString());
                            shohizeirate = Decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                            torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                        }

                        //カウントアップ
                        count++;
                    }
                }

                if (uriageTotal != 0)
                {
                    //税区分が外税:1
                    if (zikubun == 1)
                    {
                        //精算毎の消費税額算出
                        konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                 CreateSeisanTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);
                    }

                    //税区分が内税:2
                    if (zikubun == 2)
                    {
                        //精算毎の消費税額算出
                        konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                 CreateSeisanTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);
                    }
                }
            }
            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
        }

        #endregion

        #region 支払データ算出(業者別)_月次既存処理用

        /// <summary>
        /// 支払データ算出(業者別)_月次既存処理用
        /// ※月次処理用に既存の処理を別関数名で退避
        /// </summary>
        private void CreatetGyoushaShiharaiData_OldInvoice(DataTable shiharaiData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData, string gyoushaCd)
        {
            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            int zikubun = 0;
            decimal shohizeirate = 0;
            int count = 0;
            string torihikicd = "";

            // 精算毎税の計算
            DataView dv = new DataView(shiharaiData);
            DataTable table = shiharaiData.Clone();
            dv.Sort = "GYOUSHA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SHIHARAI_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
                {
                    CreatetShiharaiData(table.Rows[i]);
                }
            }

            if (shiharaiData.Rows.Count != 0)
            {
                //保存用システムＩＤ、ＳＥＱの初期化
                saveSysID = "";
                saveDenshuKbn = "";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (shiharaiData.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
                    {
                        if (table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == "2")
                        {
                            //初回
                            if (count == 0)
                            {
                                zikubun = int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString());
                                shohizeirate = Decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            if ((shiharaiData.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd) &&
                                (int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()) == zikubun) &&
                               (Decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()) == shohizeirate))
                            {
                                if (!(saveSysID.Equals(table.Rows[i]["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(table.Rows[i]["DENPYOUSHURUI"].ToString())))
                                {
                                    uriageTotal = uriageTotal +
                                                    Decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }
                            }
                            else
                            {
                                //税区分が外税:1
                                if (zikubun == 1)
                                {
                                    //精算毎の消費税額算出
                                    konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                             CreateSeisanTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);

                                    //クリア
                                    uriageTotal = 0;

                                    //集計
                                    uriageTotal = uriageTotal +
                                                Decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }
                                //税区分が内税
                                else if (zikubun == 2)
                                {
                                    //精算毎の消費税額算出
                                    konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                             CreateSeisanTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);

                                    //クリア
                                    uriageTotal = 0;

                                    //集計
                                    uriageTotal = uriageTotal +
                                                Decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }

                                //税区分と消費税の保存
                                zikubun = int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString());
                                shohizeirate = Decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            //カウントアップ
                            count++;
                        }
                    }
                }

                if (uriageTotal != 0)
                {
                    //税区分が外税:1
                    if (zikubun == 1)
                    {
                        //精算毎の消費税額算出
                        konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                 CreateSeisanTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);
                    }

                    //税区分が内税:2
                    if (zikubun == 2)
                    {
                        //精算毎の消費税額算出
                        konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                 CreateSeisanTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);
                    }
                }
            }
            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
        }

        #endregion

        #region 支払データ算出(現場別)_月次既存処理用

        /// <summary>
        /// 支払データ算出(受入)現場別_月次既存処理用
        /// ※月次処理用に既存の処理を別関数名で退避
        /// </summary>
        private void CreatetGenbaShiharaiData_OldInvoice(DataTable shiharaiData, M_TORIHIKISAKI_SHIHARAI torihikisakiShiharaiData,
                                              string gyoushaCd, string genbaCd)
        {
            //精算毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            int zikubun = 0;
            decimal shohizeirate = 0;
            int count = 0;
            string torihikicd = "";

            // 精算毎税の計算
            DataView dv = new DataView(shiharaiData);
            DataTable table = shiharaiData.Clone();
            dv.Sort = "GYOUSHA_CD,GENBA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SHIHARAI_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && table.Rows[i]["GENBA_CD"].ToString() == genbaCd)
                {
                    CreatetShiharaiData(table.Rows[i]);
                }
            }

            if (shiharaiData.Rows.Count != 0)
            {
                //保存用システムＩＤ、ＳＥＱの初期化
                saveSysID = "";
                saveDenshuKbn = "";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (shiharaiData.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && shiharaiData.Rows[i]["GENBA_CD"].ToString() == genbaCd)
                    {
                        if (table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == "2")
                        {
                            //初回
                            if (count == 0)
                            {
                                zikubun = int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString());
                                shohizeirate = Decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            if ((shiharaiData.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd) &&
                                (int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()) == zikubun) &&
                               (decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()) == shohizeirate))
                            {
                                if (!(saveSysID.Equals(table.Rows[i]["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(table.Rows[i]["DENPYOUSHURUI"].ToString())))
                                {
                                    uriageTotal = uriageTotal +
                                                    Decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }
                            }
                            else
                            {
                                //税区分が外税:1
                                if (zikubun == 1)
                                {
                                    //精算毎の消費税額算出
                                    konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                             CreateSeisanTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);

                                    //クリア
                                    uriageTotal = 0;

                                    //集計
                                    uriageTotal = uriageTotal +
                                                Decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }
                                //税区分が内税
                                else if (zikubun == 2)
                                {
                                    //精算毎の消費税額算出
                                    konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                             CreateSeisanTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);

                                    //クリア
                                    uriageTotal = 0;

                                    //集計
                                    uriageTotal = uriageTotal +
                                                Decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }

                                //税区分と消費税の保存
                                zikubun = int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString());
                                shohizeirate = Decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            //カウントアップ
                            count++;
                        }
                    }
                }

                if (uriageTotal != 0)
                {
                    //税区分が外税:1
                    if (zikubun == 1)
                    {
                        //精算毎の消費税額算出
                        konkaiSeisotozeigaku = konkaiSeisotozeigaku +
                                                 CreateSeisanTaxSoto(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);
                    }

                    //税区分が内税:2
                    if (zikubun == 2)
                    {
                        //精算毎の消費税額算出
                        konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                 CreateSeisanTaxUti(uriageTotal, Decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikisakiShiharaiData);
                    }
                }
            }

            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
        }

        #endregion

        #endregion


    }
}
