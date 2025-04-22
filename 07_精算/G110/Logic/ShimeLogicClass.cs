using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Dto;
using Seasar.Quill.Attrs;
using Shougun.Core.Adjustment.Shiharaishimesyori.DAO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using r_framework.Dao;
using System.Linq;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;

namespace Shougun.Core.Adjustment.Shiharaishimesyori.Logic
{
    /// <summary>
    /// ビジネスロジック(締め処理)
    /// </summary>
    internal class ShimeLogicClass : IBuisinessLogic
    {
        #region グローバル変数

        /// <summary>
        /// 請求締め処理(画面共通)のDao
        /// </summary>
        private ShiharaiShimeShoriDao shimeShoriDao;

        /// <summary>
        /// 精算伝票のDao
        /// </summary>
        private SeisanDenpyouDao seikyuuDenpyouDao;

        /// <summary>
        /// 精算伝票_鑑のDao
        /// </summary>
        private SeisanDenpyouKagamiDao seikyuuDenpyouKagamiDao;

        /// <summary>
        /// 精算明細のDao
        /// </summary>
        private SeisanDetailDao seikyuuDetailDao;

        /// <summary>
        /// 締処理中のDao
        /// </summary>
        private ShimeShoriChuuDao shimeshorichuuDao;

        /// <summary>
        /// 締実行履歴のDao
        /// </summary>
        private ShimeJikkouRirekiDao shimeJikkouRirekiDao;

        /// <summary>
        /// 締実行履歴_取引先のDao
        /// </summary>
        private ShimeJikkouRirekiTorihikiDao shimeJikkouRirekiTorihikiDao;

        /// <summary>
        /// 画面パラメータ
        /// </summary>
        private List<SeikyuShimeShoriDto> seikyuPramList;

        /// <summary>
        /// 回収月データ
        /// </summary>
        private DataTable kaisyuuResult { get; set; }

        /// <summary>
        /// 繰越額
        /// </summary>
        private DataTable kurikosiResult { get; set; }

        /// <summary>
        /// 繰越額(取引先支払情報マスタ)
        /// </summary>
        private DataTable kurikosiMasterResult { get; set; }

        /// <summary>
        /// 売上データ(受入)
        /// </summary>
        private DataTable uriageUkeireResult { get; set; }

        /// <summary>
        /// 売上データ(出荷)
        /// </summary>
        private DataTable uriageShukkaResult { get; set; }

        /// <summary>
        /// 売上データ(売上/支払)
        /// </summary>
        private DataTable uriageurShResult { get; set; }

        /// <summary>
        /// 登録されている行番号
        /// </summary>
        private DataTable rowNumberResult { get; set; }

        /// <summary>
        /// 出金データ
        /// </summary>
        private DataTable shukinResult { get; set; }

        /// <summary>
        /// 出金予定日
        /// </summary>
        private DateTime shukinYoteibi { get; set; }

        /// <summary>
        /// 鑑番号
        /// </summary>
        private SqlInt32 g_kagamiNo { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        private int g_rowNo { get; set; }

        #region 請求 作成金額

        /// <summary>
        /// 今回出金額
        /// </summary>
        private decimal konkaiShukingaku { get; set; }

        /// <summary>
        /// 今回調整額
        /// </summary>
        private decimal konkaiChouseigaku { get; set; }

        /// <summary>
        /// 今回支払額
        /// </summary>
        private decimal konkaiShiharaigaku { get; set; }

        /// <summary>
        /// 今回請内税額
        /// </summary>
        private decimal konkaiSeiUtizeigaku { get; set; }

        /// <summary>
        /// 今回請外税額
        /// </summary>
        private decimal konkaiSeisotozeigaku { get; set; }

        /// <summary>
        /// 今回伝内税額
        /// </summary>
        private decimal konkaiDenUtizeigaku { get; set; }

        /// <summary>
        /// 今回伝外税額
        /// </summary>
        private decimal konkaiDensotozeigaku { get; set; }

        /// <summary>
        /// 今回明内税額
        /// </summary>
        private decimal konkaiMeiUtizeigaku { get; set; }

        /// <summary>
        /// 今回明外税額
        /// </summary>
        private decimal konkaiMeisotozeigaku { get; set; }

        /// <summary>
        /// 今回御請求額
        /// </summary>
        private decimal konkaiSeikyuugaku { get; set; }

        #region 適格請求書用

        /// <summary>
        /// 請求書区分（1:旧請求書 2:適格請求書 3:合算請求書)
        /// </summary>
        private int InvoiceKBN { get; set; }

        /// <summary>
        /// 今回非課税有無
        /// </summary>
        private int konkaiHikazeiKbn { get; set; }

        /// <summary>
        /// 今回非課税額
        /// </summary>
        private decimal konkaiHikazeigaku { get; set; }

        /// <summary>
        /// 今回課税区分１
        /// </summary>
        private int konkaiKazeiKbn_1 { get; set; }

        /// <summary>
        /// 今回課税税率１
        /// </summary>
        private decimal konkaiKazeiZeiRate_1 { get; set; }

        /// <summary>
        /// 今回課税税抜金額１
        /// </summary>
        private decimal konkaiKazeiZeinukigaku_1 { get; set; }

        /// <summary>
        /// 今回課税税額１
        /// </summary>
        private decimal konkaiKazeiZeigaku_1 { get; set; }

        /// <summary>
        /// 今回課税区分２
        /// </summary>
        private int konkaiKazeiKbn_2 { get; set; }

        /// <summary>
        /// 今回課税税率２
        /// </summary>
        private decimal konkaiKazeiZeiRate_2 { get; set; }

        /// <summary>
        /// 今回課税税抜金額２
        /// </summary>
        private decimal konkaiKazeiZeinukigaku_2 { get; set; }

        /// <summary>
        /// 今回課税税額２
        /// </summary>
        private decimal konkaiKazeiZeigaku_2 { get; set; }

        /// <summary>
        /// 今回課税区分３
        /// </summary>
        private int konkaiKazeiKbn_3 { get; set; }

        /// <summary>
        /// 今回課税税率３
        /// </summary>
        private decimal konkaiKazeiZeiRate_3 { get; set; }

        /// <summary>
        /// 今回課税税抜金額３
        /// </summary>
        private decimal konkaiKazeiZeinukigaku_3 { get; set; }

        /// <summary>
        /// 今回課税税額３
        /// </summary>
        private decimal konkaiKazeiZeigaku_3 { get; set; }

        /// <summary>
        /// 今回課税区分４
        /// </summary>
        private int konkaiKazeiKbn_4 { get; set; }

        /// <summary>
        /// 今回課税税率４
        /// </summary>
        private decimal konkaiKazeiZeiRate_4 { get; set; }

        /// <summary>
        /// 今回課税税抜金額４
        /// </summary>
        private decimal konkaiKazeiZeinukigaku_4 { get; set; }

        /// <summary>
        /// 今回課税税額４
        /// </summary>
        private decimal konkaiKazeiZeigaku_4 { get; set; }

        #endregion 適格請求書用

        /// <summary>
        /// 精算伝票登録用リスト
        /// </summary>
        private List<T_SEISAN_DENPYOU> seisanDenpyouEntitylist;

        /// <summary>
        /// 精算伝票_鑑登録用リスト
        /// </summary>
        private List<T_SEISAN_DENPYOU_KAGAMI> seisanDenpyouKagamiEntitylist;

        /// <summary>
        /// 精算明細リスト
        /// </summary>
        private List<T_SEISAN_DETAIL> seisanDetailEntitylist;

        /// <summary>
        ///　取引先カウント
        /// </summary>
        private int torihikisakiCount;

        /// <summary>
        ///　統合売上データ(ソート用）
        /// </summary>
        private DataTable sorturiageData;

        /// <summary>
        ///　統合売上データ(統合用）
        /// </summary>
        private DataTable tougouuriageData;

        /// <summary>
        ///　精算番号リスト
        /// </summary>
        public List<SqlInt64> seisanBangoList;

        /// <summary>
        /// 再締精算番号リスト
        /// </summary>
        List<SqlInt64> seisanSaishimeBangoList;

        /// <summary>
        ///  保存用システムＩＤ
        /// </summary>
        private string saveSysID;

        /// <summary>
        /// 保存用伝種区分
        /// </summary>
        private string saveDenshuKbn;

        /// <summary>
        /// 仮伝票番号 :「-1] 
        /// テーブルの構造体を取得のため
        /// </summary>
        private readonly string KARI_DENPYOU_BANGOU = "-1";

        #endregion

        #region 列挙体

        /// <summary>
        /// 伝票種類
        /// </summary>
        private enum DenpyouSyurui
        {
            UKEIRE = 1,       //受入
            SYUKKA = 2,       //出荷
            URI_SHIHARAI = 3, //売上/支払い
            NYUUKIN = 4       //出金
        }

        /// <summary>
        /// 内税、外税
        /// </summary>
        private enum ZeiSyurui
        {
            UTI = 1, //内税
            SOTO = 2 //外税
        }

        #endregion

        #endregion
        //システム設定
        private M_SYS_INFO systemInfo;
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShimeLogicClass()
        {
            LogUtility.DebugMethodStart();

            //Daoを初期化
            this.shimeShoriDao = DaoInitUtility.GetComponent<ShiharaiShimeShoriDao>();
            this.seikyuuDenpyouDao = DaoInitUtility.GetComponent<SeisanDenpyouDao>();
            this.seikyuuDenpyouKagamiDao = DaoInitUtility.GetComponent<SeisanDenpyouKagamiDao>();
            this.seikyuuDetailDao = DaoInitUtility.GetComponent<SeisanDetailDao>();
            this.shimeshorichuuDao = DaoInitUtility.GetComponent<ShimeShoriChuuDao>();
            this.shimeJikkouRirekiDao = DaoInitUtility.GetComponent<ShimeJikkouRirekiDao>();
            this.shimeJikkouRirekiTorihikiDao = DaoInitUtility.GetComponent<ShimeJikkouRirekiTorihikiDao>();
            //システム設定
            this.systemInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData().FirstOrDefault();
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 締め処理

        /// <summary>
        /// 締め処理
        /// </summary>
        /// <param name="shimeDataList">画面パラメータ</param>
        /// <param name="GassanKBN">請求書区分（1:旧請求書 2:適格請求書 3:合算請求書)</param>
        /// <returns>処理結果　成功:True 失敗:False</returns>
        public Boolean Shime(List<SeikyuShimeShoriDto> shimeDataList, int GassanKBN = 1)
        {
            LogUtility.DebugMethodStart(shimeDataList, GassanKBN);

            try
            {
                //画面からのパラメータをグローバル変数に設定
                seikyuPramList = shimeDataList;
                InvoiceKBN = GassanKBN;
                
                //精算番号保存用リスト
                this.seisanBangoList = new List<SqlInt64>();
                //再締精算番号保存用リスト
                this.seisanSaishimeBangoList = new List<SqlInt64>();

                switch (seikyuPramList[0].SHIME_TANI)
                {
                    //期間締処理
                    case 1:
                        return ExeKikan();

                    //伝票締処理
                    case 2:
                        return ExeDenpyou();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                return false;
            }

            LogUtility.DebugMethodEnd(shimeDataList, GassanKBN);

            return true;
        }

        #endregion

        #region 期間締処理

        /// <summary>
        /// 期間締処理
        /// </summary>
        /// <returns>処理結果　成功:True 失敗:False</returns>
        private bool ExeKikan()
        {
            LogUtility.DebugMethodStart();

            SqlInt64 shimeNo = 0;

            try
            {
                //締実行番号を取得
                shimeNo = seikyuPramList[0].SHIME_JIKKOU_NO;

                //締め処理実行履歴レコード作成
                InsertTShimeShoriRireki(seikyuPramList[0], shimeNo);

                //取引先カウントの初期化
                torihikisakiCount = 0;

                //画面からのパラメータ件数分ループ
                foreach (SeikyuShimeShoriDto dto in seikyuPramList)
                {
                    //■■■■■精算データ再締■■■■■
                    if (dto.SAISHIME_FLG)
                    {
                        //精算データ再締削除
                        if (!DeleteSeisanSaishime(dto))
                        {
                            return false;
                        }
                    }
                    //■■■■■データ抽出■■■■■
                    //出金予定日算出
                    GetShukinYoteibi(dto);//160017

                    //前回からの繰越額の取得
                    GetKurikosigaku(dto);

                    //支払データの抽出
                    GetUriageData(dto);

                    //出金データの抽出
                    GetShukinDataKikan(dto);
                    //■■■■■■■■■■■■■■■

                    if (InvoiceKBN != 1)
                    {
                        if (!ChkResultData_invoice())
                        {
                            //取引先カウントのカウントアップ
                            torihikisakiCount++;
                            continue;
                        }
                    }

                    //データの存在チェック
                    if (!ChkResultData())
                    {
                        LogUtility.DebugMethodEnd();
                        return false;
                    }

                    //■■■■■請求データ作成■■■
                    //請求データ作成
                    CreateSeikyuuDate(shimeNo);

                    //締め処理実行履歴取引先レコード作成
                    InsertTShimeShoriRirekiTorihikisaki(dto, shimeNo);
                    //■■■■■■■■■■■■■■■

                    //取引先カウントのカウントアップ
                    torihikisakiCount++;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                //請求関連データ削除
                DeleteSeikyuData();

                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        #region 伝票締処理

        /// <summary>
        /// 伝票締処理
        /// </summary>
        /// <returns>処理結果　成功:True 失敗:False</returns>
        private bool ExeDenpyou()
        {
            LogUtility.DebugMethodStart();

            SqlInt64 shimeNo = 0;

            try
            {
                //締実行番号を取得
                shimeNo = seikyuPramList[0].SHIME_JIKKOU_NO;

                //締め処理実行履歴レコード作成
                InsertTShimeShoriRireki(seikyuPramList[0], shimeNo);

                //■■■■■データ抽出■■■■■
                //出金予定日算出
                GetShukinYoteibi(seikyuPramList[0]);//160017

                //前回からの繰越額の取得
                GetKurikosigaku(seikyuPramList[0]);

                if (seikyuPramList[0].KAIHI_FLG == false)
                {
                    //売上データの抽出
                    GetUriageDataDenpyou(seikyuPramList[0]);
                }
                else
                {
                    this.tougouuriageData = new DataTable();
                }

                //出金データの抽出
                GetShukinDataDenpyou(seikyuPramList[0]);
                //■■■■■■■■■■■■■■■

                if (InvoiceKBN != 1)
                {
                    if (!ChkResultData_invoice())
                    {
                        LogUtility.DebugMethodEnd();
                        return false;
                    }
                }

                //データの存在チェック
                if (!ChkResultData())
                {
                    LogUtility.DebugMethodEnd();
                    return false;
                }

                //■■■■■請求データ作成■■■
                //請求データ作成
                CreateSeikyuuDate(shimeNo);

                //締め処理実行履歴取引先レコード作成
                InsertTShimeShoriRirekiTorihikisaki(seikyuPramList[0], shimeNo);
                //■■■■■■■■■■■■■■■
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                //請求関連データ削除
                DeleteSeikyuData();

                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion
		#region 再締精算削除
        // エラーメッセージ表示フラグ（true：表示、false：非表示）
        internal bool DisplayErrorMsg;
        /// <summary>
        /// 再締精算データ削除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>継続:True 中断:False</returns>
        private Boolean DeleteSeisanSaishime(SeikyuShimeShoriDto dto)
        {
            SaishimeSearchDTO saishimeSearchdto = new SaishimeSearchDTO();

            saishimeSearchdto.DENPYOU_SHURUI_CD = Int16.Parse(dto.DENPYO_SHURUI.ToString());
            saishimeSearchdto.SHORI_KBN = Int16.Parse("2");
            saishimeSearchdto.KYOTEN_CD = Int16.Parse(dto.KYOTEN_CD.ToString());
            saishimeSearchdto.SHIMEBI = Int16.Parse(dto.SHIMEBI.ToString());
            if (saishimeSearchdto.SHIMEBI == 0)
            {
                saishimeSearchdto.SHIHARAISHIMEBI_FROM = dto.SHIHARAISHIMEBI_FROM;
            }
            saishimeSearchdto.SHIHARAISHIMEBI_TO = dto.SHIHARAISHIMEBI_TO;

            saishimeSearchdto.SHIHARAI_CD =  seikyuPramList[torihikisakiCount].SHIHARAI_CD;

            // 作成済み支払明細書の検索
            MessageBoxShowLogic msgLgc = new MessageBoxShowLogic();
            // 表示フラグの初期化
            DisplayErrorMsg = true;
            var result = seikyuuDenpyouDao.Search(saishimeSearchdto);
            if (result.Rows.Count > 1)
            {
                // 検索結果が1件より多い場合、中断
                msgLgc.MessageBoxShow("E301", "支払明細");
                DisplayErrorMsg = false;
            }
            else if (result.Rows.Count == 1)
            {
                // 検索結果が1件の場合、取得した請求書より未来日の請求書が存在するかチェック
                int searchCount = shimeShoriDao.CheckLatestSeisanData(seikyuPramList[torihikisakiCount].SHIHARAI_CD, Int64.Parse(result.Rows[0]["SEISAN_NUMBER"].ToString()));
                if (searchCount > 0)
                {
                    // 未来日の請求データが存在する場合、中断
                    msgLgc.MessageBoxShow("E047", "取引先", "精算");
                    DisplayErrorMsg = false;
                }
                else
                {
                    // 支払情報を削除更新
                    shimeShoriDao.UpdateSeisanSaishimeiDeleteData(Int64.Parse(result.Rows[0]["SEISAN_NUMBER"].ToString()), true);
                    this.seisanSaishimeBangoList.Add(Int64.Parse(result.Rows[0]["SEISAN_NUMBER"].ToString()));

                    bool flg = r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai();
                    if (flg && IsInxsAuthority())
                    {
                        // INXSデータ削除
                        CommonKeyDto number = new CommonKeyDto();
                        number.Id = long.Parse(result.Rows[0]["SEISAN_NUMBER"].ToString());
                        var requestDto = new
                        {
                            CommandName = 6,//delete seisan data
                            ShougunParentWindowName = "支払締処理",
                            CommandArgs = new List<CommonKeyDto>() { number }
                        };
                        ExecuteSubAppCommand(requestDto);
                    }   
                }
            }
            else
            {
                // 検索結果が0件の場合、通常の締処理継続するか判断
                DisplayErrorMsg = true;
                if (msgLgc.MessageBoxShowConfirm("再締の条件に合致する支払明細書がありませんでした。\n継続して支払明細書の作成を行いますか。")
                    == System.Windows.Forms.DialogResult.No)
                {
                    DisplayErrorMsg = false;
                }
            }

            // エラーメッセージの表示有無で処理継続か中断か判断（true：締処理継続、false：締処理中断）
            return DisplayErrorMsg;
        }

        /// <summary>
        /// INXSデータ削除
        /// </summary>
        /// <param name="requestDto"></param>
        private void ExecuteSubAppCommand(object requestDto)
        {
            RemoteAppCls remoteAppCls = new RemoteAppCls();
            var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
            {
                TransactionId = "1",
                ReferenceID = -1
            });
            var execCommandDto = new ExecuteCommandDto()
            {
                Token = token,
                Type = ExternalConnection.CommunicateLib.Enums.NotificationType.ExecuteCommand,
                Args = new object[] { JsonUtility.SerializeObject(requestDto) }
            };
            remoteAppCls.ExecuteCommand("将軍-INXS　サブアプリ", execCommandDto);

        }

        private bool IsInxsAuthority()
        {
            if (!SystemProperty.Shain.InxsTantouFlg)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 出金予定日算出

        #region 出金予定日取得

        /// <summary>
        /// 出金予定日算出処理
        /// </summary>
        /// <param name="seikyuSimebi">締め日</param>
        private void GetShukinYoteibi(SeikyuShimeShoriDto dto)//160017
        {
            LogUtility.DebugMethodStart(dto);

            //期間、明細、伝票で画面からのパラメータがちがう
            //160017 S
            //期間(伝票)締め処理
            if (dto.SHIME_TANI == 2 && !dto.SHUKKIN_YOTEI_BI.IsNull)
            {
                shukinYoteibi = dto.SHUKKIN_YOTEI_BI.Value;
            }
            else
            {
                var seikyuSimebi = DateTime.Parse(dto.SHIHARAISHIMEBI_TO);
                //160017 E
                //回収月データ取得
                M_TORIHIKISAKI torihikisakiEntity = new M_TORIHIKISAKI();
                torihikisakiEntity.TORIHIKISAKI_CD = seikyuPramList[torihikisakiCount].SHIHARAI_CD;
                this.kaisyuuResult = shimeShoriDao.GetKaisyuutukiDataForEntity(torihikisakiEntity);

                //1.日にち指定 (算出方法は標準の仕様に準拠する。)
                if (int.Parse(this.kaisyuuResult.Rows[0]["SHIHARAI_BETSU_KBN"].ToString()) == 1)
                {
                    //月を加算
                    DateTime dt = seikyuSimebi.AddMonths(GetKaisyuutuki(this.kaisyuuResult.Rows[0]["SHIHARAI_MONTH"].ToString()));

                    //末日取得
                    int day = DateTime.DaysInMonth(dt.Year, dt.Month);

                    //回収日が末日を超えていないか判定
                    DateTime rdt;
                    if (day < int.Parse(this.kaisyuuResult.Rows[0]["SHIHARAI_DAY"].ToString()))
                    {
                        rdt = new DateTime(dt.Year, dt.Month, day);
                    }
                    else
                    {
                        rdt = new DateTime(dt.Year, dt.Month, int.Parse(this.kaisyuuResult.Rows[0]["SHIHARAI_DAY"].ToString()));
                    }

                    shukinYoteibi = rdt;
                }
                //2.○日後指定
                else
                {
                    //計算：[検索期間(to)]＋[支払日種別(○日後)]
                    shukinYoteibi = seikyuSimebi.AddDays(int.Parse(this.kaisyuuResult.Rows[0]["SHIHARAI_BETSU_NICHIGO"].ToString()));
                }
            }
            LogUtility.DebugMethodEnd(shukinYoteibi);
        }

        #endregion

        #region 回収月取得

        /// <summary>
        /// 回収月取得処理
        /// </summary>
        /// <param name="kaisyuu">回収月</param>
        /// <returns>加算する月</returns>
        private int GetKaisyuutuki(string kaisyuu)
        {
            LogUtility.DebugMethodStart(kaisyuu);

            //加算する月を取得
            switch (kaisyuu)
            {
                case "1":
                    LogUtility.DebugMethodEnd(kaisyuu);
                    return 0;
                case "2":
                    LogUtility.DebugMethodEnd(kaisyuu);
                    return 1;
                case "3":
                    LogUtility.DebugMethodEnd(kaisyuu);
                    return 2;
                case "4":
                    LogUtility.DebugMethodEnd(kaisyuu);
                    return 3;
                case "5":
                    LogUtility.DebugMethodEnd(kaisyuu);
                    return 4;
                case "6":
                    LogUtility.DebugMethodEnd(kaisyuu);
                    return 5;
                case "7":
                    LogUtility.DebugMethodEnd(kaisyuu);
                    return 6;
                default:
                    LogUtility.DebugMethodEnd(kaisyuu);
                    return 6;
            }
        }

        #endregion

        #endregion

        #region 繰越額の取得

        /// <summary>
        /// 繰越額の取得
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetKurikosigaku(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //前回繰越額を取得
            SeikyuShimeShoriDto sisanDenpyouuiEntity = new SeikyuShimeShoriDto();
            sisanDenpyouuiEntity.SHIHARAI_CD = seikyuPramList[torihikisakiCount].SHIHARAI_CD;
            sisanDenpyouuiEntity.SHIHARAISHIMEBI_TO = dto.SHIHARAISHIMEBI_TO;
            this.kurikosiResult = shimeShoriDao.GetZenkaiKurikosigakuDataKikanForEntity(sisanDenpyouuiEntity);

            //取引先_支払情報マスタから取得
            SeikyuShimeShoriDto torihikisakiSeikyuuEntity = new SeikyuShimeShoriDto();
            torihikisakiSeikyuuEntity.SHIHARAI_CD = seikyuPramList[torihikisakiCount].SHIHARAI_CD;
            this.kurikosiMasterResult = shimeShoriDao.GetZenkaiKurikosigakuKaisiDataKikanForEntity(torihikisakiSeikyuuEntity);

            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        //#endregion

        #region 支払データ取得

        #region 売上データ取得(期間)

        /// <summary>
        /// 売上データ取得(期間)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetUriageData(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //伝票種類で取得データを判定
            switch (seikyuPramList[0].DENPYO_SHURUI)
            {
                case 2:
                    //受入データ
                    GetUkeireData(dto);
                    //データコピー
                    this.tougouuriageData = this.uriageUkeireResult.Copy();
                    break;

                case 3:
                    //出荷データ
                    GetSyukkaData(dto);
                    //データコピー
                    this.tougouuriageData = this.uriageShukkaResult.Copy();
                    break;

                case 4:
                    //売上/支払データ
                    GetUriageShiharaiData(dto);
                    //データコピー
                    this.tougouuriageData = this.uriageurShResult.Copy();
                    break;

                case 1:
                    //受入データ、出荷データ、売上/支払データ
                    GetUkeireData(dto);
                    GetSyukkaData(dto);
                    GetUriageShiharaiData(dto);

                    //データコピー
                    if (this.uriageUkeireResult != null)
                    {
                        this.sorturiageData = uriageUkeireResult.Clone();
                    }
                    else if (this.uriageShukkaResult != null)
                    {
                        this.sorturiageData = uriageShukkaResult.Clone();
                    }
                    else if (this.uriageurShResult != null)
                    {
                        this.sorturiageData = uriageurShResult.Clone();
                    }

                    if (this.uriageUkeireResult != null)
                    {
                        foreach (DataRow r in uriageUkeireResult.Rows)
                        {
                            this.sorturiageData.ImportRow(r);
                        }
                    }

                    if (this.uriageShukkaResult != null)
                    {
                        foreach (DataRow r in uriageShukkaResult.Rows)
                        {
                            this.sorturiageData.ImportRow(r);
                        }
                    }

                    if (this.uriageurShResult != null)
                    {
                        foreach (DataRow r in uriageurShResult.Rows)
                        {
                            this.sorturiageData.ImportRow(r);
                        }
                    }

                    DataView dv = new DataView(sorturiageData);
                    this.tougouuriageData = sorturiageData.Clone();
                    dv.Sort = "TORIHIKISAKI_CD,GYOUSHA_CD,GENBA_CD";
                    foreach (DataRowView drv in dv)
                    {
                        this.tougouuriageData.ImportRow(drv.Row);
                    }
                    break;
            }
            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #region 売上データ取得(伝票)

        /// <summary>
        /// 売上データ取得(伝票)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetUriageDataDenpyou(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //伝票番号取得用リスト
            List<string> ukeireList = new List<string>();
            List<string> shukaList = new List<string>();
            List<string> urishList = new List<string>();

            //画面からのパラメータ件数分ループ
            foreach (SeikyuShimeShoriDto dto1 in seikyuPramList)
            {
                //受入
                if (dto1.DATA_SHURUI == 2)
                {
                    ukeireList.Add(dto1.DENPYOU_NUMBER.ToString());
                }

                //出荷
                if (dto1.DATA_SHURUI == 3)
                {
                    shukaList.Add(dto1.DENPYOU_NUMBER.ToString());
                }

                //売上・支払
                if (dto1.DATA_SHURUI == 4)
                {
                    urishList.Add(dto1.DENPYOU_NUMBER.ToString());
                }
            }

            //伝票種類で取得データを判定
            switch (seikyuPramList[0].DENPYO_SHURUI)
            {
                case 2:
                    //受入データ

                    //伝票を選択しない場合、[仮伝票番号]でテーブルの構造体を取得
                    if (ukeireList.Count == 0)
                    {
                        ukeireList.Add(KARI_DENPYOU_BANGOU);
                    }

                    dto.DENPYOU_NUMBER_LIST = ukeireList;
                    GetUkeireDataDenpyou(dto);
                    //データコピー
                    this.tougouuriageData = this.uriageUkeireResult.Copy();
                    break;

                case 3:
                    //出荷データ

                    //伝票を選択しない場合、[仮伝票番号]でテーブルの構造体を取得
                    if (shukaList.Count == 0)
                    {
                        shukaList.Add(KARI_DENPYOU_BANGOU);
                    }

                    dto.DENPYOU_NUMBER_LIST = shukaList;
                    GetSyukkaDataDenpyou(dto);
                    //データコピー
                    this.tougouuriageData = this.uriageShukkaResult.Copy();
                    break;

                case 4:
                    //売上/支払データ

                    //伝票を選択しない場合、[仮伝票番号]でテーブルの構造体を取得
                    if (urishList.Count == 0)
                    {
                        urishList.Add(KARI_DENPYOU_BANGOU);
                    }

                    dto.DENPYOU_NUMBER_LIST = urishList;
                    GetUriageShiharaiDataDenpyou(dto);
                    //データコピー
                    this.tougouuriageData = this.uriageurShResult.Copy();
                    break;

                case 1:
                    //受入データ、出荷データ、売上/支払データ
                    if (ukeireList.Count != 0)
                    {
                        dto.DENPYOU_NUMBER_LIST = ukeireList;
                        GetUkeireDataDenpyou(dto);
                    }

                    if (shukaList.Count != 0)
                    {
                        dto.DENPYOU_NUMBER_LIST = shukaList;
                        GetSyukkaDataDenpyou(dto);
                    }

                    if (urishList.Count != 0)
                    {
                        dto.DENPYOU_NUMBER_LIST = urishList;
                        GetUriageShiharaiDataDenpyou(dto);
                    }

                    //伝票を選択しない場合、[仮伝票番号]でテーブルの構造体を取得
                    if (ukeireList.Count == 0 && shukaList.Count == 0 && urishList.Count == 0)
                    {
                        ukeireList.Add(KARI_DENPYOU_BANGOU);
                        dto.DENPYOU_NUMBER_LIST = ukeireList;
                        GetUkeireDataDenpyou(dto);
                    }

                    //データコピー
                    if (this.uriageUkeireResult != null)
                    {
                        this.sorturiageData = uriageUkeireResult.Clone();
                    }
                    else if (this.uriageShukkaResult != null)
                    {
                        this.sorturiageData = uriageShukkaResult.Clone();
                    }
                    else if (this.uriageurShResult != null)
                    {
                        this.sorturiageData = uriageurShResult.Clone();
                    }

                    if (this.uriageUkeireResult != null)
                    {
                        foreach (DataRow r in uriageUkeireResult.Rows)
                        {
                            this.sorturiageData.ImportRow(r);
                        }
                    }

                    if (this.uriageShukkaResult != null)
                    {
                        foreach (DataRow r in uriageShukkaResult.Rows)
                        {
                            this.sorturiageData.ImportRow(r);
                        }
                    }

                    if (this.uriageurShResult != null)
                    {
                        foreach (DataRow r in uriageurShResult.Rows)
                        {
                            this.sorturiageData.ImportRow(r);
                        }
                    }

                    DataView dv = new DataView(sorturiageData);
                    this.tougouuriageData = sorturiageData.Clone();
                    dv.Sort = "TORIHIKISAKI_CD,GYOUSHA_CD,GENBA_CD";
                    foreach (DataRowView drv in dv)
                    {
                        this.tougouuriageData.ImportRow(drv.Row);
                    }
                    break;
            }
            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #region 売上データ(受入　出荷　売上/支払)取得(期間)

        #region 受入データ取得(期間)

        /// <summary>
        /// 受入データ取得(期間)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetUkeireData(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //受入データ取得
            SeikyuShimeShoriDto sisanDenpyouEntity = new SeikyuShimeShoriDto();
            sisanDenpyouEntity.SHIHARAI_CD = dto.SHIHARAI_CD;
            sisanDenpyouEntity.KYOTEN_CD = dto.KYOTEN_CD;
            sisanDenpyouEntity.SHIHARAISHIMEBI_FROM = dto.SHIHARAISHIMEBI_FROM;
            sisanDenpyouEntity.SHIHARAISHIMEBI_TO = dto.SHIHARAISHIMEBI_TO;
            sisanDenpyouEntity.DENPYO_SHURUI = 2;
            sisanDenpyouEntity.INVOICE_KBN = InvoiceKBN;

            this.uriageUkeireResult = shimeShoriDao.GetUriageDataKikanForEntity(sisanDenpyouEntity);
            this.uriageUkeireResult.Columns.Add("DENPYOUSHURUI", typeof(int));

            foreach (DataRow row in uriageUkeireResult.Rows)
            {
                row["DENPYOUSHURUI"] = 1;
            }

            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #region 出荷データ取得(期間)

        /// <summary>
        /// 出荷データ取得(期間)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetSyukkaData(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //出荷データ取得
            SeikyuShimeShoriDto sisanDenpyouuiEntity = new SeikyuShimeShoriDto();
            sisanDenpyouuiEntity.SHIHARAI_CD = dto.SHIHARAI_CD;
            sisanDenpyouuiEntity.KYOTEN_CD = dto.KYOTEN_CD;
            sisanDenpyouuiEntity.SHIHARAISHIMEBI_FROM = dto.SHIHARAISHIMEBI_FROM;
            sisanDenpyouuiEntity.SHIHARAISHIMEBI_TO = dto.SHIHARAISHIMEBI_TO;
            sisanDenpyouuiEntity.DENPYO_SHURUI = 3;
            sisanDenpyouuiEntity.INVOICE_KBN = InvoiceKBN;

            this.uriageShukkaResult = shimeShoriDao.GetUriageDataKikanForEntity(sisanDenpyouuiEntity);
            this.uriageShukkaResult.Columns.Add("DENPYOUSHURUI", typeof(int));

            foreach (DataRow row in uriageShukkaResult.Rows)
            {
                row["DENPYOUSHURUI"] = 2;
            }

            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #region 売上/支払データ(期間)

        /// <summary>
        /// 売上/支払データ(期間)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetUriageShiharaiData(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //売上/支払データ取得
            SeikyuShimeShoriDto sisanDenpyouuiEntity = new SeikyuShimeShoriDto();
            sisanDenpyouuiEntity.SHIHARAI_CD = dto.SHIHARAI_CD;
            sisanDenpyouuiEntity.KYOTEN_CD = dto.KYOTEN_CD;
            sisanDenpyouuiEntity.SHIHARAISHIMEBI_FROM = dto.SHIHARAISHIMEBI_FROM;
            sisanDenpyouuiEntity.SHIHARAISHIMEBI_TO = dto.SHIHARAISHIMEBI_TO;
            sisanDenpyouuiEntity.DENPYO_SHURUI = 4;
            sisanDenpyouuiEntity.INVOICE_KBN = InvoiceKBN;

            this.uriageurShResult = shimeShoriDao.GetUriageDataKikanForEntity(sisanDenpyouuiEntity);
            this.uriageurShResult.Columns.Add("DENPYOUSHURUI", typeof(int));

            foreach (DataRow row in uriageurShResult.Rows)
            {
                row["DENPYOUSHURUI"] = 3;
            }

            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #endregion

        #region 売上データ(受入　出荷　売上/支払)取得(伝票)

        #region 受入データ取得(伝票)

        /// <summary>
        /// 受入データ取得(伝票)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetUkeireDataDenpyou(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //受入データ取得
            SeikyuShimeShoriDto sisanDenpyouEntity = new SeikyuShimeShoriDto();
            sisanDenpyouEntity.SHIHARAI_CD = dto.SHIHARAI_CD;
            sisanDenpyouEntity.DENPYOU_NUMBER_LIST = dto.DENPYOU_NUMBER_LIST;
            sisanDenpyouEntity.DENPYO_SHURUI = 2;
            sisanDenpyouEntity.INVOICE_KBN = InvoiceKBN;

            this.uriageUkeireResult = shimeShoriDao.GetUriageDataDenpyouForEntity(sisanDenpyouEntity);
            this.uriageUkeireResult.Columns.Add("DENPYOUSHURUI", typeof(int));

            foreach (DataRow row in uriageUkeireResult.Rows)
            {
                row["DENPYOUSHURUI"] = 1;
            }

            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #region 出荷データ取得(伝票)

        /// <summary>
        /// 出荷データ取得(伝票)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetSyukkaDataDenpyou(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //出荷データ取得
            SeikyuShimeShoriDto sisanDenpyouuiEntity = new SeikyuShimeShoriDto();
            sisanDenpyouuiEntity.SHIHARAI_CD = dto.SHIHARAI_CD;
            sisanDenpyouuiEntity.KYOTEN_CD = dto.KYOTEN_CD;
            sisanDenpyouuiEntity.DENPYOU_NUMBER_LIST = dto.DENPYOU_NUMBER_LIST;
            sisanDenpyouuiEntity.DENPYO_SHURUI = 3;
            sisanDenpyouuiEntity.INVOICE_KBN = InvoiceKBN;

            this.uriageShukkaResult = shimeShoriDao.GetUriageDataDenpyouForEntity(sisanDenpyouuiEntity);
            this.uriageShukkaResult.Columns.Add("DENPYOUSHURUI", typeof(int));

            foreach (DataRow row in uriageShukkaResult.Rows)
            {
                row["DENPYOUSHURUI"] = 2;
            }

            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #region 売上/支払データ(伝票)

        /// <summary>
        /// 売上/支払データ(伝票)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetUriageShiharaiDataDenpyou(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //売上/支払データ取得
            SeikyuShimeShoriDto sisanDenpyouuiEntity = new SeikyuShimeShoriDto();
            sisanDenpyouuiEntity.SHIHARAI_CD = dto.SHIHARAI_CD;
            sisanDenpyouuiEntity.KYOTEN_CD = dto.KYOTEN_CD;
            sisanDenpyouuiEntity.DENPYOU_NUMBER_LIST = dto.DENPYOU_NUMBER_LIST;
            sisanDenpyouuiEntity.DENPYO_SHURUI = 4;
            sisanDenpyouuiEntity.INVOICE_KBN = InvoiceKBN;

            this.uriageurShResult = shimeShoriDao.GetUriageDataDenpyouForEntity(sisanDenpyouuiEntity);
            this.uriageurShResult.Columns.Add("DENPYOUSHURUI", typeof(int));

            foreach (DataRow row in uriageurShResult.Rows)
            {
                row["DENPYOUSHURUI"] = 3;
            }

            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #endregion

        #endregion

        #region 出金データ取得

        #region 出金データ取得(期間)

        /// <summary>
        /// 出金データ取得(期間)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetShukinDataKikan(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //出金データ取得
            SeikyuShimeShoriDto sisanDenpyouuiEntity = new SeikyuShimeShoriDto();
            sisanDenpyouuiEntity.SHIHARAI_CD = dto.SHIHARAI_CD;
            sisanDenpyouuiEntity.KYOTEN_CD = dto.KYOTEN_CD;
            sisanDenpyouuiEntity.SHIHARAISHIMEBI_FROM = dto.SHIHARAISHIMEBI_FROM;
            sisanDenpyouuiEntity.SHIHARAISHIMEBI_TO = dto.SHIHARAISHIMEBI_TO;

            this.shukinResult = shimeShoriDao.GetShukkinDataKikanForEntity(sisanDenpyouuiEntity);

            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #region 出金データ取得(伝票)

        /// <summary>
        /// 出金データ取得(伝票)
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        private void GetShukinDataDenpyou(SeikyuShimeShoriDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            //伝票番号取得用リスト
            List<string> shukkinList = new List<string>();

            //画面からのパラメータ件数分ループ
            foreach (SeikyuShimeShoriDto dto1 in seikyuPramList)
            {
                //出金
                if (dto1.DATA_SHURUI == Shougun.Core.Common.BusinessCommon.Const.CommonConst.DENSHU_KBN_SHUKKINN)
                {
                    shukkinList.Add(dto1.DENPYOU_NUMBER.ToString());
                }
            }

            //伝票を選択しない場合、[仮伝票番号]でテーブルの構造体を取得
            if (shukkinList.Count == 0)
            {
                shukkinList.Add(KARI_DENPYOU_BANGOU);
            }

            //出金データ取得
            SeikyuShimeShoriDto sisanDenpyouuiEntity = new SeikyuShimeShoriDto();
            sisanDenpyouuiEntity.SHIHARAI_CD = dto.SHIHARAI_CD;
            //sisanDenpyouuiEntity.KYOTEN_CD = dto.KYOTEN_CD;
            //sisanDenpyouuiEntity.SHIHARAISHIMEBI_FROM = dto.SHIHARAISHIMEBI_FROM;
            //sisanDenpyouuiEntity.SHIHARAISHIMEBI_TO = dto.SHIHARAISHIMEBI_TO;
            sisanDenpyouuiEntity.DENPYOU_NUMBER_LIST = shukkinList;

            this.shukinResult = shimeShoriDao.GetShukkinDataDenpyouMeisaiForEntity(sisanDenpyouuiEntity);

            LogUtility.DebugMethodEnd(dto);
        }

        #endregion

        #endregion

        #region データの存在チェック

        /// <summary>
        /// データの存在チェック
        /// </summary>
        /// <returns>チェック結果　あり:True なし:False</returns>
        private bool ChkResultData()
        {
            LogUtility.DebugMethodStart();

            if (this.kurikosiResult.Rows.Count == 0 &&
                this.kurikosiMasterResult.Rows.Count == 0 &&
                this.uriageUkeireResult.Rows.Count == 0 &&
                this.uriageShukkaResult.Rows.Count == 0 &&
                this.uriageurShResult.Rows.Count == 0 &&
                this.shukinResult.Rows.Count == 0)
            {
                LogUtility.DebugMethodEnd();
                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        #region 請求データ作成

        /// <summary>
        /// 請求データ作成
        /// </summary>
        /// <param name="shimeNo">締実行番号</param>
        /// <returns></returns>
        private bool CreateSeikyuuDate(SqlInt64 shimeNo)
        {
            LogUtility.DebugMethodStart(shimeNo);

            //取引先請求情報マスタ：書式区分で処理を判定
            SqlInt64 seikyuNo = 0;

            //精算伝票_鑑リスト、精算明細リスト作成
            seisanDenpyouEntitylist = new List<T_SEISAN_DENPYOU>();
            seisanDenpyouKagamiEntitylist = new List<T_SEISAN_DENPYOU_KAGAMI>();
            seisanDetailEntitylist = new List<T_SEISAN_DETAIL>();

            switch (kurikosiMasterResult.Rows[0]["SHOSHIKI_KBN"].ToString())
            {
                //請求先別：1
                case "1":
                    //鑑番号の初期化
                    g_kagamiNo = 0;
                    g_rowNo = 0;

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
                    #region 適格請求書用
                    // 今回非課税区分
                    konkaiHikazeiKbn = 0;
                    // 今回非課税額
                    konkaiHikazeigaku = 0;
                    // 今回課税区分１
                    konkaiKazeiKbn_1 = 0;
                    // 今回課税税率１
                    konkaiKazeiZeiRate_1 = 0;
                    // 今回課税税抜金額１
                    konkaiKazeiZeinukigaku_1 = 0;
                    // 今回課税税額１
                    konkaiKazeiZeigaku_1 = 0;
                    // 今回課税区分２
                    konkaiKazeiKbn_2 = 0;
                    // 今回課税税率２
                    konkaiKazeiZeiRate_2 = 0;
                    // 今回課税税抜金額２
                    konkaiKazeiZeinukigaku_2 = 0;
                    // 今回課税税額２
                    konkaiKazeiZeigaku_2 = 0;
                    // 今回課税区分３
                    konkaiKazeiKbn_3 = 0;
                    // 今回課税税率３
                    konkaiKazeiZeiRate_3 = 0;
                    // 今回課税税抜金額３
                    konkaiKazeiZeinukigaku_3 = 0;
                    // 今回課税税額３
                    konkaiKazeiZeigaku_3 = 0;
                    // 今回課税区分４
                    konkaiKazeiKbn_4 = 0;
                    // 今回課税税率４
                    konkaiKazeiZeiRate_4 = 0;
                    // 今回課税税抜金額４
                    konkaiKazeiZeinukigaku_4 = 0;
                    // 今回課税税額４
                    konkaiKazeiZeigaku_4 = 0;
                    #endregion 適格請求書用

                    //請求伝票作成
                    CreateSeikyuuDenpyou(ref seikyuNo, shimeNo);

                    if (this.seisanDenpyouEntitylist.Count > 0)
                    {
                        //請求伝票_鑑 明細作成
                        CreateSeikyuuKagamiMeisaiDenpyouDetail(tougouuriageData, seikyuNo, false);
                    }

                    break;

                //業者別：2
                case "2":
                    //鑑番号の初期化
                    g_kagamiNo = 0;
                    g_rowNo = 0;

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
                    #region 適格請求書用
                    // 今回非課税区分
                    konkaiHikazeiKbn = 0;
                    // 今回非課税額
                    konkaiHikazeigaku = 0;
                    // 今回課税区分１
                    konkaiKazeiKbn_1 = 0;
                    // 今回課税税率１
                    konkaiKazeiZeiRate_1 = 0;
                    // 今回課税税抜金額１
                    konkaiKazeiZeinukigaku_1 = 0;
                    // 今回課税税額１
                    konkaiKazeiZeigaku_1 = 0;
                    // 今回課税区分２
                    konkaiKazeiKbn_2 = 0;
                    // 今回課税税率２
                    konkaiKazeiZeiRate_2 = 0;
                    // 今回課税税抜金額２
                    konkaiKazeiZeinukigaku_2 = 0;
                    // 今回課税税額２
                    konkaiKazeiZeigaku_2 = 0;
                    // 今回課税区分３
                    konkaiKazeiKbn_3 = 0;
                    // 今回課税税率３
                    konkaiKazeiZeiRate_3 = 0;
                    // 今回課税税抜金額３
                    konkaiKazeiZeinukigaku_3 = 0;
                    // 今回課税税額３
                    konkaiKazeiZeigaku_3 = 0;
                    // 今回課税区分４
                    konkaiKazeiKbn_4 = 0;
                    // 今回課税税率４
                    konkaiKazeiZeiRate_4 = 0;
                    // 今回課税税抜金額４
                    konkaiKazeiZeinukigaku_4 = 0;
                    // 今回課税税額４
                    konkaiKazeiZeigaku_4 = 0;
                    #endregion 適格請求書用

                    //請求伝票作成
                    CreateSeikyuuDenpyou(ref seikyuNo, shimeNo);

                    if (this.seisanDenpyouEntitylist.Count > 0)
                    {
                        //請求伝票_鑑 明細作成
                        CreateGyoushaKagamiMeisaiDenpyouDetail(tougouuriageData, seikyuNo, false);
                    }

                    break;

                //現場別：3
                case "3":
                    //鑑番号の初期化
                    g_kagamiNo = 0;
                    g_rowNo = 0;

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
                    #region 適格請求書用
                    // 今回非課税区分
                    konkaiHikazeiKbn = 0;
                    // 今回非課税額
                    konkaiHikazeigaku = 0;
                    // 今回課税区分１
                    konkaiKazeiKbn_1 = 0;
                    // 今回課税税率１
                    konkaiKazeiZeiRate_1 = 0;
                    // 今回課税税抜金額１
                    konkaiKazeiZeinukigaku_1 = 0;
                    // 今回課税税額１
                    konkaiKazeiZeigaku_1 = 0;
                    // 今回課税区分２
                    konkaiKazeiKbn_2 = 0;
                    // 今回課税税率２
                    konkaiKazeiZeiRate_2 = 0;
                    // 今回課税税抜金額２
                    konkaiKazeiZeinukigaku_2 = 0;
                    // 今回課税税額２
                    konkaiKazeiZeigaku_2 = 0;
                    // 今回課税区分３
                    konkaiKazeiKbn_3 = 0;
                    // 今回課税税率３
                    konkaiKazeiZeiRate_3 = 0;
                    // 今回課税税抜金額３
                    konkaiKazeiZeinukigaku_3 = 0;
                    // 今回課税税額３
                    konkaiKazeiZeigaku_3 = 0;
                    // 今回課税区分４
                    konkaiKazeiKbn_4 = 0;
                    // 今回課税税率４
                    konkaiKazeiZeiRate_4 = 0;
                    // 今回課税税抜金額４
                    konkaiKazeiZeinukigaku_4 = 0;
                    // 今回課税税額４
                    konkaiKazeiZeigaku_4 = 0;
                    #endregion 適格請求書用

                    //請求伝票作成
                    CreateSeikyuuDenpyou(ref seikyuNo, shimeNo);

                    if (this.seisanDenpyouEntitylist.Count > 0)
                    {
                        //請求伝票_鑑 明細作成
                        CreateGenbaKagamiMeisaiDenpyouDetail(tougouuriageData, seikyuNo, false);
                    }

                    break;
            }

            using (Transaction tran = new Transaction())
            {
                if (seisanDenpyouEntitylist != null && seisanDenpyouEntitylist.Count > 0)
                {
                    foreach (T_SEISAN_DENPYOU seisanDenpyou in seisanDenpyouEntitylist)
                    {
                        seikyuuDenpyouDao.Insert(seisanDenpyou);
                    }
                }

                if (seisanDenpyouKagamiEntitylist != null && seisanDenpyouKagamiEntitylist.Count > 0)
                {
                    foreach (T_SEISAN_DENPYOU_KAGAMI seisanDenpyouKagami in seisanDenpyouKagamiEntitylist)
                    {
                        //システム設定-支払タブ-支払明細書備考を参照し
                        seisanDenpyouKagami.BIKOU_1 = StringUtil.ConverToString(this.systemInfo.SHIHARAI_BIKOU_1);
                        seisanDenpyouKagami.BIKOU_2 = StringUtil.ConverToString(this.systemInfo.SHIHARAI_BIKOU_2);

                        seikyuuDenpyouKagamiDao.Insert(seisanDenpyouKagami);
                    }
                }

                if (seisanDetailEntitylist != null && seisanDetailEntitylist.Count > 0)
                {
                    int count = 0;
                    foreach (T_SEISAN_DETAIL seisanDetail in seisanDetailEntitylist)
                    {
                        count++;
                        int CntNyuukinIns = seikyuuDetailDao.Insert(seisanDetail);
                        string zenkaiseisanno = seisanDetail.SEISAN_NUMBER.ToString();
                        string zenkaikagamino = seisanDetail.KAGAMI_NUMBER.ToString();
                        string zenkairowno = seisanDetail.ROW_NUMBER.ToString();
                    }
                }

                // コミット
                tran.Commit();
            }

            LogUtility.DebugMethodEnd(shimeNo);

            return true;
        }

        #endregion

        #region 伝票作成

        #region 精算伝票作成

        /// <summary>
        /// 精算伝票作成
        /// </summary>
        /// <param name="seikyuNo">精算番号</param>
        /// <param name="shimeNo">締め実行番号</param>
        private void CreateSeikyuuDenpyou(ref SqlInt64 seikyuNo, SqlInt64 shimeNo)
        {
            LogUtility.DebugMethodStart(seikyuNo, shimeNo);

            T_SEISAN_DENPYOU seisanDenpyouEntity = new T_SEISAN_DENPYOU();
            // WHOカラム設定共通ロジック呼び出し用
            var dataBind_seisanDenpyouEntityAdd = new DataBinderLogic<T_SEISAN_DENPYOU>(seisanDenpyouEntity);

            //拠点CD
            seisanDenpyouEntity.KYOTEN_CD = SqlInt16.Parse(seikyuPramList[0].KYOTEN_CD.ToString());
            //締日
            //取引先CD
            if (seikyuPramList[0].SHIME_TANI == 1)
            {
                //期間締処理
                seisanDenpyouEntity.TORIHIKISAKI_CD = seikyuPramList[torihikisakiCount].SHIHARAI_CD.ToString();
                seisanDenpyouEntity.SHIMEBI = SqlInt16.Parse(seikyuPramList[0].SHIMEBI.ToString());
            }
            else
            {
                //伝票締処理、明細締処理
                seisanDenpyouEntity.TORIHIKISAKI_CD = seikyuPramList[0].SHIHARAI_CD.ToString();
                seisanDenpyouEntity.SHIMEBI = SqlInt16.Null;
            }

            //書式区分
            seisanDenpyouEntity.SHOSHIKI_KBN = SqlInt16.Parse(kurikosiMasterResult.Rows[0]["SHOSHIKI_KBN"].ToString());
            //書式明細区分
            seisanDenpyouEntity.SHOSHIKI_MEISAI_KBN = SqlInt16.Parse(kurikosiMasterResult.Rows[0]["SHOSHIKI_MEISAI_KBN"].ToString());
            //支払明細書書式3
            if (!kurikosiMasterResult.Rows[0]["SHOSHIKI_GENBA_KBN"].IsNull())
            {
                seisanDenpyouEntity.SHOSHIKI_GENBA_KBN = kurikosiMasterResult.Rows[0]["SHOSHIKI_GENBA_KBN"].ConvertToInt16();
            }
            //支払形態区分
            seisanDenpyouEntity.SHIHARAI_KEITAI_KBN = SqlInt16.Parse(kurikosiMasterResult.Rows[0]["SHIHARAI_KEITAI_KBN"].ToString());
            //出金明細区分
            seisanDenpyouEntity.SHUKKIN_MEISAI_KBN = SqlInt16.Parse(kurikosiMasterResult.Rows[0]["SHUKKIN_MEISAI_KBN"].ToString());
            //用紙区分
            seisanDenpyouEntity.YOUSHI_KBN = SqlInt16.Parse(kurikosiMasterResult.Rows[0]["YOUSHI_KBN"].ToString());
            //請求日付
            seisanDenpyouEntity.SEISAN_DATE = SqlDateTime.Parse(seikyuPramList[0].SHIHARAISHIMEBI_TO.ToString());
            //出金予定日
            seisanDenpyouEntity.SHUKKIN_YOTEI_BI = this.shukinYoteibi;

            //前回繰越額
            decimal zenkaigaku = GetZenkaikurikoshigaku();
            seisanDenpyouEntity.ZENKAI_KURIKOSI_GAKU = SqlDecimal.Parse(zenkaigaku.ToString());

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

            //支払データから集計
            switch (seisanDenpyouEntity.SHOSHIKI_KBN.ToString())
            {
                case "1":
                    // 請求先別
                    SumSyukkinShiharaiData(zenkaigaku);

                    break;

                case "2":
                    // 業者別
                    SumShukkinData();

                    for (int i = 0; i < tougouuriageData.Rows.Count; i++)
                    {
                        if (gyoushaCd != tougouuriageData.Rows[i]["GYOUSHA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = tougouuriageData.Rows[i]["GYOUSHA_CD"].ToString();

                            SumShiharaiData("2", gyoushaCd, "");

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
                            //適格
                            konkaiKazeiKbn_1 = 0;
                            konkaiKazeiZeiRate_1 = 0;
                            konkaiKazeiZeinukigaku_1 = 0;
                            konkaiKazeiZeigaku_1 = 0;
                            konkaiKazeiKbn_2 = 0;
                            konkaiKazeiZeiRate_2 = 0;
                            konkaiKazeiZeinukigaku_2 = 0;
                            konkaiKazeiZeigaku_2 = 0;
                            konkaiKazeiKbn_3 = 0;
                            konkaiKazeiZeiRate_3 = 0;
                            konkaiKazeiZeinukigaku_3 = 0;
                            konkaiKazeiZeigaku_3 = 0;
                            konkaiKazeiKbn_4 = 0;
                            konkaiKazeiZeiRate_4 = 0;
                            konkaiKazeiZeinukigaku_4 = 0;
                            konkaiKazeiZeigaku_4 = 0;
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
                    SumShukkinData();

                    for (int i = 0; i < tougouuriageData.Rows.Count; i++)
                    {
                        if (gyoushaCd != tougouuriageData.Rows[i]["GYOUSHA_CD"].ToString() || genbaCd != tougouuriageData.Rows[i]["GENBA_CD"].ToString())
                        {
                            //比較対象取得
                            gyoushaCd = tougouuriageData.Rows[i]["GYOUSHA_CD"].ToString();
                            genbaCd = tougouuriageData.Rows[i]["GENBA_CD"].ToString();

                            SumShiharaiData("3", gyoushaCd, genbaCd);

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
                            //適格
                            konkaiKazeiKbn_1 = 0;
                            konkaiKazeiZeiRate_1 = 0;
                            konkaiKazeiZeinukigaku_1 = 0;
                            konkaiKazeiZeigaku_1 = 0;
                            konkaiKazeiKbn_2 = 0;
                            konkaiKazeiZeiRate_2 = 0;
                            konkaiKazeiZeinukigaku_2 = 0;
                            konkaiKazeiZeigaku_2 = 0;
                            konkaiKazeiKbn_3 = 0;
                            konkaiKazeiZeiRate_3 = 0;
                            konkaiKazeiZeinukigaku_3 = 0;
                            konkaiKazeiZeigaku_3 = 0;
                            konkaiKazeiKbn_4 = 0;
                            konkaiKazeiZeiRate_4 = 0;
                            konkaiKazeiZeinukigaku_4 = 0;
                            konkaiKazeiZeigaku_4 = 0;
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

            //今回出金額
            seisanDenpyouEntity.KONKAI_SHUKKIN_GAKU = SqlDecimal.Parse(konkaiShukingaku.ToString());
            //今回調整額
            seisanDenpyouEntity.KONKAI_CHOUSEI_GAKU = SqlDecimal.Parse(konkaiChouseigaku.ToString());
            //今回支払額
            seisanDenpyouEntity.KONKAI_SHIHARAI_GAKU = SqlDecimal.Parse(konkaiShiharaigaku.ToString());
            //今回請内税額
            seisanDenpyouEntity.KONKAI_SEI_UTIZEI_GAKU = SqlDecimal.Parse(konkaiSeiUtizeigaku.ToString());
            //今回請外税額
            seisanDenpyouEntity.KONKAI_SEI_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiSeisotozeigaku.ToString());
            //今回伝内税額
            seisanDenpyouEntity.KONKAI_DEN_UTIZEI_GAKU = SqlDecimal.Parse(konkaiDenUtizeigaku.ToString());
            //今回伝外税額
            seisanDenpyouEntity.KONKAI_DEN_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiDensotozeigaku.ToString());
            //今回明内税額
            seisanDenpyouEntity.KONKAI_MEI_UTIZEI_GAKU = SqlDecimal.Parse(konkaiMeiUtizeigaku.ToString());
            //今回明外税額
            seisanDenpyouEntity.KONKAI_MEI_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiMeisotozeigaku.ToString());
            //今回御精算額
            seisanDenpyouEntity.KONKAI_SEISAN_GAKU = SqlDecimal.Parse(konkaiSeikyuugaku.ToString());

            //発行区分
            seisanDenpyouEntity.HAKKOU_KBN = false;

            //締実行番号
            seisanDenpyouEntity.SHIME_JIKKOU_NO = shimeNo;

            #region 適格請求書
            //登録番号
            seisanDenpyouEntity.TOUROKU_NO = seikyuPramList[0].TOUROKU_NO.ToString();

            //支払明細書区分(1:旧請求書、2:適格請求書、3:合算請求書)
            seisanDenpyouEntity.INVOICE_KBN = SqlInt16.Parse(Convert.ToString(InvoiceKBN));
            #endregion 適格請求書

            // WHOカラム設定
            dataBind_seisanDenpyouEntityAdd.SetSystemProperty(seisanDenpyouEntity, false);

            // 出金伝票有無
            var existShukinData = false;
            if (this.shukinResult != null && this.shukinResult.Rows != null && 0 < this.shukinResult.Rows.Count)
            {
                existShukinData = true;
            }

            // 支払データが0件、今回支払、前回繰越額が0の場合はデータを作成しない
            // 出金伝票が0件の場合はデータを作成しない
            // 前月繰越額>0でも、取引先支払マスタの「支払形態区分が1（単月精算）」の場合はデータを作成しない（№4970）
            if (this.tougouuriageData.Rows.Count > 0 || seisanDenpyouEntity.KONKAI_SHIHARAI_GAKU.Value != 0 || existShukinData
                || (seisanDenpyouEntity.ZENKAI_KURIKOSI_GAKU.Value != 0 && seisanDenpyouEntity.SHIHARAI_KEITAI_KBN == 2))
            {
                //伝種採番テーブルから請求番号を取得
                seikyuNo = GetDensyuSaibanNo("50");
                //請求番号を保存
                this.seisanBangoList.Add(seikyuNo);
                //精算番号
                seisanDenpyouEntity.SEISAN_NUMBER = seikyuNo;

                seisanDenpyouEntitylist.Add(seisanDenpyouEntity);
            }

            LogUtility.DebugMethodEnd(seikyuNo, shimeNo);
        }

        #endregion

        #region 精算伝票_鑑判定(請求先別)

        /// <summary>
        /// 精算伝票_鑑判定(請求先別)
        /// </summary>
        /// <param name="meisai">鑑作成用データ</param>
        /// <param name="seikyuNo">精算番号</param>
        /// <param name="allFlg">鑑作成フラグ</param>
        private void CreateSeikyuuKagamiMeisaiDenpyouDetail(DataTable meisai, SqlInt64 seikyuNo, bool allFlg = true)
        {
            LogUtility.DebugMethodStart(meisai, seikyuNo, allFlg);

            var sortedMeisaiDataTable = meisai.Clone();
            DataRow[] sortedMeisaiRows = (DataRow[])meisai.Select(String.Empty, "GYOUSHA_CD,GENBA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SHIHARAI_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO").Clone();
            foreach (var row in sortedMeisaiRows)
            {
                sortedMeisaiDataTable.ImportRow(row);
            }

            //行番号
            int l_RowNo = 0;

            //■売上
            //請求先別の鑑は1レコード
            //鑑作成
            //全てが選択されていた場合
            if (!allFlg)
            {
                if (sortedMeisaiDataTable.Rows.Count != 0)
                {
                    g_kagamiNo = g_kagamiNo + 1;

                    //登録用データ変数初期化
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
                    #region 適格請求書用
                    // 今回非課税区分
                    konkaiHikazeiKbn = 0;
                    // 今回非課税額
                    konkaiHikazeigaku = 0;
                    // 今回課税区分１
                    konkaiKazeiKbn_1 = 0;
                    // 今回課税税率１
                    konkaiKazeiZeiRate_1 = 0;
                    // 今回課税税抜金額１
                    konkaiKazeiZeinukigaku_1 = 0;
                    // 今回課税税額１
                    konkaiKazeiZeigaku_1 = 0;
                    // 今回課税区分２
                    konkaiKazeiKbn_2 = 0;
                    // 今回課税税率２
                    konkaiKazeiZeiRate_2 = 0;
                    // 今回課税税抜金額２
                    konkaiKazeiZeinukigaku_2 = 0;
                    // 今回課税税額２
                    konkaiKazeiZeigaku_2 = 0;
                    // 今回課税区分３
                    konkaiKazeiKbn_3 = 0;
                    // 今回課税税率３
                    konkaiKazeiZeiRate_3 = 0;
                    // 今回課税税抜金額３
                    konkaiKazeiZeinukigaku_3 = 0;
                    // 今回課税税額３
                    konkaiKazeiZeigaku_3 = 0;
                    // 今回課税区分４
                    konkaiKazeiKbn_4 = 0;
                    // 今回課税税率４
                    konkaiKazeiZeiRate_4 = 0;
                    // 今回課税税抜金額４
                    konkaiKazeiZeinukigaku_4 = 0;
                    // 今回課税税額４
                    konkaiKazeiZeigaku_4 = 0;
                    #endregion 適格請求書用

                    CreateKagamiDenpyouDetail(sortedMeisaiDataTable.Rows[0], seikyuNo, "", "");
                }
            }

            //売上データが無い場合は、前回繰越データ、出金データを使用して鑑を作成
            if ((seikyuPramList[0].KAIHI_FLG == true) || (sortedMeisaiDataTable.Rows.Count == 0))
            {
                g_kagamiNo = g_kagamiNo + 1;
                CreateKagamiDenpyouUrinashiDetail(seikyuNo);
            }

            //明細作成
            for (int i = 0; i < sortedMeisaiDataTable.Rows.Count; i++)
            {
                //明細作成
                CreateMeisaiDenpyouDetail(sortedMeisaiDataTable.Rows[i], seikyuNo, g_rowNo + i + 1);
                l_RowNo = i;
            }

            //行番号をグローバル変数に保存
            g_rowNo = g_rowNo + l_RowNo + 1;

            //明細作成
            l_RowNo = 0;
            //全てが選択されていた場合
            if (!allFlg)
            {
                for (int i = 0; i < shukinResult.Rows.Count; i++)
                {
                    //明細作成
                    CreateNyuukinDenpyouDetail(shukinResult.Rows[i], DenpyouSyurui.NYUUKIN, seikyuNo, g_rowNo + i + 1);
                    l_RowNo = i;
                }
                //行番号をグローバル変数に保存
                g_rowNo = g_rowNo + l_RowNo + 1;
            }

            LogUtility.DebugMethodEnd(meisai, seikyuNo, allFlg);
        }

        #endregion

        #region 精算伝票_鑑判定(業者別)

        /// <summary>
        /// 精算伝票_鑑判定(業者別)
        /// </summary>
        /// <param name="meisai">鑑作成用データ</param>
        /// <param name="seikyuNo">精算番号</param>
        /// /// <param name="allFlg">鑑作成フラグ</param>
        private void CreateGyoushaKagamiMeisaiDenpyouDetail(DataTable meisai, SqlInt64 seikyuNo, bool allFlg = true)
        {
            LogUtility.DebugMethodStart(meisai, seikyuNo, allFlg);

            var sortedMeisaiDataTable = meisai.Clone();
            DataRow[] sortedMeisaiRows = (DataRow[])meisai.Select(String.Empty, "GYOUSHA_CD,GENBA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SHIHARAI_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO").Clone();
            foreach (var row in sortedMeisaiRows)
            {
                sortedMeisaiDataTable.ImportRow(row);
            }

            //行番号
            int l_RowNo = 0;
            int cnt = 0;
            g_kagamiNo = 0;
            //■売上
            string gyoushaCd = string.Empty;

            for (int i = 0; i < sortedMeisaiDataTable.Rows.Count; i++)
            {
                if (gyoushaCd != sortedMeisaiDataTable.Rows[i]["GYOUSHA_CD"].ToString())
                {
                    //鑑作成
                    //鑑番号更新
                    g_kagamiNo = g_kagamiNo + 1;
                    //鑑番号毎の行番号
                    cnt = 0;

                    //登録用データ変数初期化
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
                    #region 適格請求書用
                    // 今回非課税区分
                    konkaiHikazeiKbn = 0;
                    // 今回非課税額
                    konkaiHikazeigaku = 0;
                    // 今回課税区分１
                    konkaiKazeiKbn_1 = 0;
                    // 今回課税税率１
                    konkaiKazeiZeiRate_1 = 0;
                    // 今回課税税抜金額１
                    konkaiKazeiZeinukigaku_1 = 0;
                    // 今回課税税額１
                    konkaiKazeiZeigaku_1 = 0;
                    // 今回課税区分２
                    konkaiKazeiKbn_2 = 0;
                    // 今回課税税率２
                    konkaiKazeiZeiRate_2 = 0;
                    // 今回課税税抜金額２
                    konkaiKazeiZeinukigaku_2 = 0;
                    // 今回課税税額２
                    konkaiKazeiZeigaku_2 = 0;
                    // 今回課税区分３
                    konkaiKazeiKbn_3 = 0;
                    // 今回課税税率３
                    konkaiKazeiZeiRate_3 = 0;
                    // 今回課税税抜金額３
                    konkaiKazeiZeinukigaku_3 = 0;
                    // 今回課税税額３
                    konkaiKazeiZeigaku_3 = 0;
                    // 今回課税区分４
                    konkaiKazeiKbn_4 = 0;
                    // 今回課税税率４
                    konkaiKazeiZeiRate_4 = 0;
                    // 今回課税税抜金額４
                    konkaiKazeiZeinukigaku_4 = 0;
                    // 今回課税税額４
                    konkaiKazeiZeigaku_4 = 0;
                    #endregion 適格請求書用

                    //全てが選択されていた場合
                    if (!allFlg)
                    {
                        CreateKagamiDenpyouDetail(sortedMeisaiDataTable.Rows[i], seikyuNo, sortedMeisaiDataTable.Rows[i]["GYOUSHA_CD"].ToString(), "");
                    }
                    else
                    {
                        //行番号取得
                        T_SEISAN_DETAIL rowNumberEntity = new T_SEISAN_DETAIL();
                        if (seikyuNo == 1)
                        {
                            rowNumberEntity.SEISAN_NUMBER = seikyuNo;
                        }
                        else
                        {
                            rowNumberEntity.SEISAN_NUMBER = seikyuNo - 1;
                        }

                        if (g_kagamiNo == 1)
                        {
                            rowNumberEntity.KAGAMI_NUMBER = g_kagamiNo;
                        }
                        else
                        {
                            rowNumberEntity.KAGAMI_NUMBER = g_kagamiNo - 1;
                        }
                        this.rowNumberResult = shimeShoriDao.GetRowNumberForEntity(rowNumberEntity);
                        l_RowNo = int.Parse(rowNumberResult.Rows[0]["CNT"].ToString());
                    }

                    //明細作成
                    CreateMeisaiDenpyouDetail(sortedMeisaiDataTable.Rows[i], seikyuNo, l_RowNo + cnt + 1);

                    gyoushaCd = sortedMeisaiDataTable.Rows[i]["GYOUSHA_CD"].ToString();
                }
                else
                {
                    //明細作成
                    CreateMeisaiDenpyouDetail(sortedMeisaiDataTable.Rows[i], seikyuNo, g_rowNo + cnt + 1);
                }
                cnt = cnt + 1;
            }
            //行番号をグローバル変数に保存
            g_rowNo = g_rowNo + l_RowNo + 1;

            //売上データが無い場合は、前回繰越データ、出金データを使用して鑑を作成
            if (seikyuPramList[0].KAIHI_FLG == true || (sortedMeisaiDataTable.Rows.Count == 0 && this.shukinResult.Rows.Count == 0))
            {
                //鑑番号更新
                g_kagamiNo = g_kagamiNo + 1;
                CreateKagamiDenpyouUrinashiDetail(seikyuNo);
            }

            //明細作成
            l_RowNo = 0;
            //全てが選択されていた場合
            if (!allFlg)
            {
                if (shukinResult.Rows.Count != 0)
                {
                    //出金用精算伝票_鑑作成
                    g_kagamiNo = g_kagamiNo + 1;
                    CreateKagamiDenpyouShuukinDetail(seikyuNo, this.shukinResult.Rows[0]);
                }

                for (int i = 0; i < shukinResult.Rows.Count; i++)
                {
                    //明細作成
                    CreateNyuukinDenpyouDetail(shukinResult.Rows[i], DenpyouSyurui.NYUUKIN, seikyuNo, i + 1);
                    l_RowNo = i;
                }
                //行番号をグローバル変数に保存
                g_rowNo = g_rowNo + l_RowNo + 1;
            }

            LogUtility.DebugMethodEnd(meisai, seikyuNo, allFlg);
        }

        #endregion

        #region 精算伝票_鑑判定(現場別)

        /// <summary>
        /// 精算伝票_鑑判定(現場別)
        /// </summary>
        /// <param name="meisai">鑑作成用データ</param>
        /// <param name="seikyuNo">精算番号</param>
        /// <param name="allFlg">鑑作成フラグ</param>
        private void CreateGenbaKagamiMeisaiDenpyouDetail(DataTable meisai, SqlInt64 seikyuNo, bool allFlg = true)
        {
            LogUtility.DebugMethodStart(meisai, seikyuNo, allFlg);

            var sortedMeisaiDataTable = meisai.Clone();
            DataRow[] sortedMeisaiRows = (DataRow[])meisai.Select(String.Empty, "GYOUSHA_CD,GENBA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SHIHARAI_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO").Clone();
            foreach (var row in sortedMeisaiRows)
            {
                sortedMeisaiDataTable.ImportRow(row);
            }

            //行番号
            int l_RowNo = 0;
            int cnt = 0;
            //■売上
            string gyoushaCd = string.Empty;
            string genbaCd = string.Empty;
            g_kagamiNo = 0;

            for (int i = 0; i < sortedMeisaiDataTable.Rows.Count; i++)
            {
                if (gyoushaCd != sortedMeisaiDataTable.Rows[i]["GYOUSHA_CD"].ToString() || genbaCd != sortedMeisaiDataTable.Rows[i]["GENBA_CD"].ToString())
                {
                    //鑑番号更新
                    g_kagamiNo = g_kagamiNo + 1;
                    //鑑番号毎の行番号
                    cnt = 0;

                    //登録用データ変数初期化
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
                    #region 適格請求書用
                    // 今回非課税区分
                    konkaiHikazeiKbn = 0;
                    // 今回非課税額
                    konkaiHikazeigaku = 0;
                    // 今回課税区分１
                    konkaiKazeiKbn_1 = 0;
                    // 今回課税税率１
                    konkaiKazeiZeiRate_1 = 0;
                    // 今回課税税抜金額１
                    konkaiKazeiZeinukigaku_1 = 0;
                    // 今回課税税額１
                    konkaiKazeiZeigaku_1 = 0;
                    // 今回課税区分２
                    konkaiKazeiKbn_2 = 0;
                    // 今回課税税率２
                    konkaiKazeiZeiRate_2 = 0;
                    // 今回課税税抜金額２
                    konkaiKazeiZeinukigaku_2 = 0;
                    // 今回課税税額２
                    konkaiKazeiZeigaku_2 = 0;
                    // 今回課税区分３
                    konkaiKazeiKbn_3 = 0;
                    // 今回課税税率３
                    konkaiKazeiZeiRate_3 = 0;
                    // 今回課税税抜金額３
                    konkaiKazeiZeinukigaku_3 = 0;
                    // 今回課税税額３
                    konkaiKazeiZeigaku_3 = 0;
                    // 今回課税区分４
                    konkaiKazeiKbn_4 = 0;
                    // 今回課税税率４
                    konkaiKazeiZeiRate_4 = 0;
                    // 今回課税税抜金額４
                    konkaiKazeiZeinukigaku_4 = 0;
                    // 今回課税税額４
                    konkaiKazeiZeigaku_4 = 0;
                    #endregion 適格請求書用

                    //鑑作成
                    if (!allFlg)
                    {
                        CreateKagamiDenpyouDetail(sortedMeisaiDataTable.Rows[i], seikyuNo, sortedMeisaiDataTable.Rows[i]["GYOUSHA_CD"].ToString(), sortedMeisaiDataTable.Rows[i]["GENBA_CD"].ToString());
                    }
                    else
                    {
                        //行番号取得
                        T_SEISAN_DETAIL rowNumberEntity = new T_SEISAN_DETAIL();
                        if (seikyuNo == 1)
                        {
                            rowNumberEntity.SEISAN_NUMBER = seikyuNo;
                        }
                        else
                        {
                            rowNumberEntity.SEISAN_NUMBER = seikyuNo - 1;
                        }

                        if (g_kagamiNo == 1)
                        {
                            rowNumberEntity.KAGAMI_NUMBER = g_kagamiNo;
                        }
                        else
                        {
                            rowNumberEntity.KAGAMI_NUMBER = g_kagamiNo - 1;
                        }

                        this.rowNumberResult = shimeShoriDao.GetRowNumberForEntity(rowNumberEntity);
                        l_RowNo = int.Parse(rowNumberResult.Rows[0]["CNT"].ToString());
                    }

                    //明細作成
                    CreateMeisaiDenpyouDetail(sortedMeisaiDataTable.Rows[i], seikyuNo, l_RowNo + cnt + 1);

                    gyoushaCd = sortedMeisaiDataTable.Rows[i]["GYOUSHA_CD"].ToString();
                    genbaCd = sortedMeisaiDataTable.Rows[i]["GENBA_CD"].ToString();
                }
                else
                {
                    //明細作成
                    CreateMeisaiDenpyouDetail(sortedMeisaiDataTable.Rows[i], seikyuNo, l_RowNo + cnt + 1);
                }
                cnt = cnt + 1;
            }

            //売上データが無い場合は、前回繰越データ、出金データを使用して鑑を作成
            if (seikyuPramList[0].KAIHI_FLG == true || (sortedMeisaiDataTable.Rows.Count == 0 && this.shukinResult.Rows.Count == 0))
            {
                //鑑番号更新
                g_kagamiNo = g_kagamiNo + 1;
                CreateKagamiDenpyouUrinashiDetail(seikyuNo);
            }

            //明細作成
            l_RowNo = 0;
            //全てが選択されていた場合
            if (!allFlg)
            {
                if (shukinResult.Rows.Count != 0)
                {
                    //出金用精算伝票_鑑作成
                    g_kagamiNo = g_kagamiNo + 1;
                    CreateKagamiDenpyouShuukinDetail(seikyuNo, this.shukinResult.Rows[0]);
                }

                for (int i = 0; i < shukinResult.Rows.Count; i++)
                {
                    //明細作成
                    CreateNyuukinDenpyouDetail(shukinResult.Rows[i], DenpyouSyurui.NYUUKIN, seikyuNo, i + 1);
                    l_RowNo = i;
                }
                //行番号をグローバル変数に保存
                g_rowNo = g_rowNo + l_RowNo + 1;
            }

            LogUtility.DebugMethodEnd(meisai, seikyuNo, allFlg);
        }

        #endregion

        #region 精算伝票_鑑作成

        /// <summary>
        /// 精算伝票_鑑作成
        /// </summary>
        /// <param name="meisaiRow">鑑作成用データ</param>
        /// <param name="seikyuNo">精算番号</param>
        /// <param name="gyoushaCd">業者コード</param>
        /// <param name="genbaCd">現場コード</param>
        private void CreateKagamiDenpyouDetail(DataRow meisaiRow, SqlInt64 seikyuNo, string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(meisaiRow, seikyuNo, gyoushaCd, genbaCd);

            T_SEISAN_DENPYOU_KAGAMI seisanDenpyouKagamiEntity = new T_SEISAN_DENPYOU_KAGAMI();

            //精算番号
            seisanDenpyouKagamiEntity.SEISAN_NUMBER = seikyuNo;
            //鑑番号
            seisanDenpyouKagamiEntity.KAGAMI_NUMBER = g_kagamiNo;
            //取引先CD
            //取引先CD
            if (seikyuPramList[0].SHIME_TANI == 1)
            {
                //期間締処理
                seisanDenpyouKagamiEntity.TORIHIKISAKI_CD = seikyuPramList[torihikisakiCount].SHIHARAI_CD.ToString();
            }
            else
            {
                //伝票締処理、明細締処理
                seisanDenpyouKagamiEntity.TORIHIKISAKI_CD = seikyuPramList[0].SHIHARAI_CD.ToString();
            }

            //会社名
            seisanDenpyouKagamiEntity.CORP_NAME = meisaiRow["CORP_NAME"].ToString();
            //代表者名
            seisanDenpyouKagamiEntity.CORP_DAIHYOU = meisaiRow["CORP_DAIHYOU"].ToString();

            //前回繰越額
            decimal zenkaigaku = GetZenkaikurikoshigaku();

            T_SEIKYUU_DENPYOU seikyuuDenpyouEntity = new T_SEIKYUU_DENPYOU();
            seikyuuDenpyouEntity.ZENKAI_KURIKOSI_GAKU = SqlDecimal.Parse(zenkaigaku.ToString());

            //出金、支払データ集計値作成
            SumSyukkinShiharaiData(zenkaigaku, meisaiRow["SHOSHIKI_KBN"].ToString(), gyoushaCd, genbaCd);

            //今回支払額
            seisanDenpyouKagamiEntity.KONKAI_SHIHARAI_GAKU = SqlDecimal.Parse(konkaiShiharaigaku.ToString());
            //今回請内税額
            seisanDenpyouKagamiEntity.KONKAI_SEI_UTIZEI_GAKU = SqlDecimal.Parse(konkaiSeiUtizeigaku.ToString());
            //今回請外税額
            seisanDenpyouKagamiEntity.KONKAI_SEI_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiSeisotozeigaku.ToString());
            //今回伝内税額
            seisanDenpyouKagamiEntity.KONKAI_DEN_UTIZEI_GAKU = SqlDecimal.Parse(konkaiDenUtizeigaku.ToString());
            //今回伝外税額
            seisanDenpyouKagamiEntity.KONKAI_DEN_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiDensotozeigaku.ToString());
            //今回明内税額
            seisanDenpyouKagamiEntity.KONKAI_MEI_UTIZEI_GAKU = SqlDecimal.Parse(konkaiMeiUtizeigaku.ToString());
            //今回明外税額
            seisanDenpyouKagamiEntity.KONKAI_MEI_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiMeisotozeigaku.ToString());
            //削除フラグ
            seisanDenpyouKagamiEntity.DELETE_FLG = false;
            #region 適格請求書
            // 今回課税区分①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_1 = SqlInt16.Parse(konkaiKazeiKbn_1.ToString());
            // 今回課税税率①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_1 = SqlDecimal.Parse(konkaiKazeiZeiRate_1.ToString());
            // 今回課税税抜金額①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_1 = SqlDecimal.Parse(konkaiKazeiZeinukigaku_1.ToString());
            // 今回課税税額①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_1 = SqlDecimal.Parse(konkaiKazeiZeigaku_1.ToString());
            // 今回課税区分②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_2 = SqlInt16.Parse(konkaiKazeiKbn_2.ToString());
            // 今回課税税率②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_2 = SqlDecimal.Parse(konkaiKazeiZeiRate_2.ToString());
            // 今回課税税抜金額②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_2 = SqlDecimal.Parse(konkaiKazeiZeinukigaku_2.ToString());
            // 今回課税税額②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_2 = SqlDecimal.Parse(konkaiKazeiZeigaku_2.ToString());
            // 今回課税区分③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_3 = SqlInt16.Parse(konkaiKazeiKbn_3.ToString());
            // 今回課税税率③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_3 = SqlDecimal.Parse(konkaiKazeiZeiRate_3.ToString());
            // 今回課税税抜金額③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_3 = SqlDecimal.Parse(konkaiKazeiZeinukigaku_3.ToString());
            // 今回課税税額③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_3 = SqlDecimal.Parse(konkaiKazeiZeigaku_3.ToString());
            // 今回課税区分④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_4 = SqlInt16.Parse(konkaiKazeiKbn_4.ToString());
            // 今回課税税率④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_4 = SqlDecimal.Parse(konkaiKazeiZeiRate_4.ToString());
            // 今回課税税抜金額④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_4 = SqlDecimal.Parse(konkaiKazeiZeinukigaku_4.ToString());
            // 今回課税税額④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_4 = SqlDecimal.Parse(konkaiKazeiZeigaku_4.ToString());
            //今回非課税区分
            seisanDenpyouKagamiEntity.KONKAI_HIKAZEI_KBN = SqlInt16.Parse(konkaiHikazeiKbn.ToString());
            //今回非課税税抜金額
            seisanDenpyouKagamiEntity.KONKAI_HIKAZEI_GAKU = SqlDecimal.Parse(konkaiHikazeigaku.ToString());
            #endregion 適格請求書           

            //支払先別、業者別、現場別の項目

            switch (meisaiRow["SHOSHIKI_KBN"].ToString())
            {
                case "1":
                    //■支払先別
                    //業者CD
                    seisanDenpyouKagamiEntity.GYOUSHA_CD = "";
                    //現場CD
                    seisanDenpyouKagamiEntity.GENBA_CD = "";
                    //代表者印字区分
                    seisanDenpyouKagamiEntity.DAIHYOU_PRINT_KBN = SqlInt16.Parse(BlankToZero(""));
                    //拠点名印字区分
                    seisanDenpyouKagamiEntity.KYOTEN_NAME_PRINT_KBN = SqlInt16.Parse(BlankToZero(meisaiRow["SHIHARAI_KYOTEN_PRINT_KBN"].ToString()));
                    //拠点CD
                    seisanDenpyouKagamiEntity.KYOTEN_CD = SqlInt16.Parse(BlankToZero(meisaiRow["SHIHARAI_KYOTEN_CD"].ToString()));
                    //拠点名
                    seisanDenpyouKagamiEntity.KYOTEN_NAME = meisaiRow["TR_KYOTEN_NAME"].ToString();
                    //拠点代表者名
                    seisanDenpyouKagamiEntity.KYOTEN_DAIHYOU = meisaiRow["TR_KYOTEN_DAIHYOU"].ToString();
                    //拠点郵便番号
                    seisanDenpyouKagamiEntity.KYOTEN_POST = meisaiRow["TR_KYOTEN_POST"].ToString();
                    //拠点住所1
                    seisanDenpyouKagamiEntity.KYOTEN_ADDRESS1 = meisaiRow["TR_KYOTEN_ADDRESS1"].ToString();
                    //拠点住所2
                    seisanDenpyouKagamiEntity.KYOTEN_ADDRESS2 = meisaiRow["TR_KYOTEN_ADDRESS2"].ToString();
                    //拠点TEL
                    seisanDenpyouKagamiEntity.KYOTEN_TEL = meisaiRow["TR_KYOTEN_TEL"].ToString();
                    //拠点FAX
                    seisanDenpyouKagamiEntity.KYOTEN_FAX = meisaiRow["TR_KYOTEN_FAX"].ToString();
                    //支払書送付先1
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME1 = meisaiRow["SHIHARAI_SOUFU_NAME1"].ToString();
                    //支払書送付先2
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME2 = meisaiRow["SHIHARAI_SOUFU_NAME2"].ToString();
                    //支払書送付先敬称1
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU1 = meisaiRow["SHIHARAI_SOUFU_KEISHOU1"].ToString();
                    //支払書送付先敬称2
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU2 = meisaiRow["SHIHARAI_SOUFU_KEISHOU2"].ToString();
                    //支払書送付先郵便番号
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_POST = meisaiRow["SHIHARAI_SOUFU_POST"].ToString();
                    //支払書送付先住所1
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS1 = meisaiRow["SHIHARAI_SOUFU_ADDRESS1"].ToString();
                    //支払書送付先住所2
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS2 = meisaiRow["SHIHARAI_SOUFU_ADDRESS2"].ToString();
                    //支払書送付先部署
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_BUSHO = meisaiRow["SHIHARAI_SOUFU_BUSHO"].ToString();
                    //支払書送付先担当者
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TANTOU = meisaiRow["SHIHARAI_SOUFU_TANTOU"].ToString();
                    //支払書送付先TEL
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TEL = meisaiRow["SHIHARAI_SOUFU_TEL"].ToString();
                    //支払書送付先FAX
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_FAX = meisaiRow["SHIHARAI_SOUFU_FAX"].ToString();

                    break;

                case "2":
                    //■業者別
                    //業者CD
                    seisanDenpyouKagamiEntity.GYOUSHA_CD = meisaiRow["GYOUSHA_CD"].ToString();
                    //現場CD
                    seisanDenpyouKagamiEntity.GENBA_CD = "";
                    //代表者印字区分
                    seisanDenpyouKagamiEntity.DAIHYOU_PRINT_KBN = SqlInt16.Parse(BlankToZero(""));
                    //拠点名印字区分
                    seisanDenpyouKagamiEntity.KYOTEN_NAME_PRINT_KBN = SqlInt16.Parse(BlankToZero(meisaiRow["GY_SHIHARAI_KYOTEN_PRINT_KBN"].ToString()));
                    //拠点CD
                    seisanDenpyouKagamiEntity.KYOTEN_CD = SqlInt16.Parse(BlankToZero(meisaiRow["GY_SHIHARAI_KYOTEN_CD"].ToString()));
                    //拠点名
                    seisanDenpyouKagamiEntity.KYOTEN_NAME = meisaiRow["GY_KYOTEN_NAME"].ToString();
                    //拠点代表者名
                    seisanDenpyouKagamiEntity.KYOTEN_DAIHYOU = meisaiRow["GY_KYOTEN_DAIHYOU"].ToString();
                    //拠点郵便番号
                    seisanDenpyouKagamiEntity.KYOTEN_POST = meisaiRow["GY_KYOTEN_POST"].ToString();
                    //拠点住所1
                    seisanDenpyouKagamiEntity.KYOTEN_ADDRESS1 = meisaiRow["GY_KYOTEN_ADDRESS1"].ToString();
                    //拠点住所2
                    seisanDenpyouKagamiEntity.KYOTEN_ADDRESS2 = meisaiRow["GY_KYOTEN_ADDRESS2"].ToString();
                    //拠点TEL
                    seisanDenpyouKagamiEntity.KYOTEN_TEL = meisaiRow["GY_KYOTEN_TEL"].ToString();
                    //拠点FAX
                    seisanDenpyouKagamiEntity.KYOTEN_FAX = meisaiRow["GY_KYOTEN_FAX"].ToString();
                    //支払書送付先1
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME1 = meisaiRow["GY_SHIHARAI_SOUFU_NAME1"].ToString();
                    //支払書送付先2
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME2 = meisaiRow["GY_SHIHARAI_SOUFU_NAME2"].ToString();
                    //支払書送付先敬称1
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU1 = meisaiRow["GY_SHIHARAI_SOUFU_KEISHOU1"].ToString();
                    //支払書送付先敬称2
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU2 = meisaiRow["GY_SHIHARAI_SOUFU_KEISHOU2"].ToString();
                    //支払書送付先郵便番号
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_POST = meisaiRow["GY_SHIHARAI_SOUFU_POST"].ToString();
                    //支払書送付先住所1
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS1 = meisaiRow["GY_SHIHARAI_SOUFU_ADDRESS1"].ToString();
                    //支払書送付先住所2
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS2 = meisaiRow["GY_SHIHARAI_SOUFU_ADDRESS2"].ToString();
                    //支払書送付先部署
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_BUSHO = meisaiRow["GY_SHIHARAI_SOUFU_BUSHO"].ToString();
                    //支払書送付先担当者
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TANTOU = meisaiRow["GY_SHIHARAI_SOUFU_TANTOU"].ToString();
                    //支払書送付先TEL
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TEL = meisaiRow["GY_SHIHARAI_SOUFU_TEL"].ToString();
                    //支払書送付先FAX
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_FAX = meisaiRow["GY_SHIHARAI_SOUFU_FAX"].ToString();

                    break;

                case "3":
                    //■現場別
                    //業者CD
                    seisanDenpyouKagamiEntity.GYOUSHA_CD = meisaiRow["GYOUSHA_CD"].ToString(); ;
                    //現場CD
                    seisanDenpyouKagamiEntity.GENBA_CD = meisaiRow["GENBA_CD"].ToString();
                    //代表者印字区分
                    seisanDenpyouKagamiEntity.DAIHYOU_PRINT_KBN = SqlInt16.Parse(BlankToZero(""));
                    //拠点名印字区分
                    seisanDenpyouKagamiEntity.KYOTEN_NAME_PRINT_KBN = SqlInt16.Parse(BlankToZero(meisaiRow["GE_SHIHARAI_KYOTEN_PRINT_KBN"].ToString()));
                    //拠点CD
                    seisanDenpyouKagamiEntity.KYOTEN_CD = SqlInt16.Parse(BlankToZero(meisaiRow["GE_SHIHARAI_KYOTEN_CD"].ToString()));
                    //拠点名
                    seisanDenpyouKagamiEntity.KYOTEN_NAME = meisaiRow["GE_KYOTEN_NAME"].ToString();
                    //拠点代表者名
                    seisanDenpyouKagamiEntity.KYOTEN_DAIHYOU = meisaiRow["GE_KYOTEN_DAIHYOU"].ToString();
                    //拠点郵便番号
                    seisanDenpyouKagamiEntity.KYOTEN_POST = meisaiRow["GE_KYOTEN_POST"].ToString();
                    //拠点住所1
                    seisanDenpyouKagamiEntity.KYOTEN_ADDRESS1 = meisaiRow["GE_KYOTEN_ADDRESS1"].ToString();
                    //拠点住所2
                    seisanDenpyouKagamiEntity.KYOTEN_ADDRESS2 = meisaiRow["GE_KYOTEN_ADDRESS2"].ToString();
                    //拠点TEL
                    seisanDenpyouKagamiEntity.KYOTEN_TEL = meisaiRow["GE_KYOTEN_TEL"].ToString();
                    //拠点FAX
                    seisanDenpyouKagamiEntity.KYOTEN_FAX = meisaiRow["GE_KYOTEN_FAX"].ToString();
                    //支払書送付先1
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME1 = meisaiRow["GE_SHIHARAI_SOUFU_NAME1"].ToString();
                    //支払書送付先2
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME2 = meisaiRow["GE_SHIHARAI_SOUFU_NAME2"].ToString();
                    //支払書送付先敬称1
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU1 = meisaiRow["GE_SHIHARAI_SOUFU_KEISHOU1"].ToString();
                    //支払書送付先敬称2
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU2 = meisaiRow["GE_SHIHARAI_SOUFU_KEISHOU2"].ToString();
                    //支払書送付先郵便番号
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_POST = meisaiRow["GE_SHIHARAI_SOUFU_POST"].ToString();
                    //支払書送付先住所1
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS1 = meisaiRow["GE_SHIHARAI_SOUFU_ADDRESS1"].ToString();
                    //支払書送付先住所2
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS2 = meisaiRow["GE_SHIHARAI_SOUFU_ADDRESS2"].ToString();
                    //支払書送付先部署
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_BUSHO = meisaiRow["GE_SHIHARAI_SOUFU_BUSHO"].ToString();
                    //支払書送付先担当者
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TANTOU = meisaiRow["GE_SHIHARAI_SOUFU_TANTOU"].ToString();
                    //支払書送付先TEL
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TEL = meisaiRow["GE_SHIHARAI_SOUFU_TEL"].ToString();
                    //支払書送付先FAX
                    seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_FAX = meisaiRow["GE_SHIHARAI_SOUFU_FAX"].ToString();

                    break;
            }

            //精算伝票_鑑を追加
            seisanDenpyouKagamiEntitylist.Add(seisanDenpyouKagamiEntity);

            LogUtility.DebugMethodEnd(meisaiRow, seikyuNo, gyoushaCd, genbaCd);
        }

        #endregion

        #region 精算伝票_鑑(出金)作成

        /// <summary>
        /// 精算伝票_鑑(出金)作成
        /// </summary>
        /// <param name="seikyuNo">精算番号</param>
        /// <param name="shukkinDataRow">出金データ</param>
        private void CreateKagamiDenpyouShuukinDetail(SqlInt64 seikyuNo, DataRow shukkinDataRow)
        {
            LogUtility.DebugMethodStart(seikyuNo, shukkinDataRow);

            T_SEISAN_DENPYOU_KAGAMI seisanDenpyouKagamiEntity = new T_SEISAN_DENPYOU_KAGAMI();

            //精算番号
            seisanDenpyouKagamiEntity.SEISAN_NUMBER = seikyuNo;
            //鑑番号
            seisanDenpyouKagamiEntity.KAGAMI_NUMBER = g_kagamiNo;
            //取引先CD
            //取引先CD
            if (seikyuPramList[0].SHIME_TANI == 1)
            {
                //期間締処理
                seisanDenpyouKagamiEntity.TORIHIKISAKI_CD = seikyuPramList[torihikisakiCount].SHIHARAI_CD.ToString();
            }
            else
            {
                //伝票締処理、明細締処理
                seisanDenpyouKagamiEntity.TORIHIKISAKI_CD = seikyuPramList[0].SHIHARAI_CD.ToString();
            }

            //会社名
            seisanDenpyouKagamiEntity.CORP_NAME = shukkinDataRow["CORP_NAME"] == null ? string.Empty : shukkinDataRow["CORP_NAME"].ToString();
            //代表者名
            seisanDenpyouKagamiEntity.CORP_DAIHYOU = shukkinDataRow["CORP_DAIHYOU"] == null ? string.Empty : shukkinDataRow["CORP_DAIHYOU"].ToString();
            //拠点名
            seisanDenpyouKagamiEntity.KYOTEN_NAME = shukkinDataRow["TR_KYOTEN_NAME"] == null ? string.Empty : shukkinDataRow["TR_KYOTEN_NAME"].ToString();
            //拠点代表者名
            seisanDenpyouKagamiEntity.KYOTEN_DAIHYOU = shukkinDataRow["TR_KYOTEN_DAIHYOU"] == null ? string.Empty : shukkinDataRow["TR_KYOTEN_DAIHYOU"].ToString();
            //拠点郵便番号
            seisanDenpyouKagamiEntity.KYOTEN_POST = shukkinDataRow["TR_KYOTEN_POST"] == null ? string.Empty : shukkinDataRow["TR_KYOTEN_POST"].ToString();
            //拠点住所1
            seisanDenpyouKagamiEntity.KYOTEN_ADDRESS1 = shukkinDataRow["TR_KYOTEN_ADDRESS1"] == null ? string.Empty : shukkinDataRow["TR_KYOTEN_ADDRESS1"].ToString();
            //拠点住所2
            seisanDenpyouKagamiEntity.KYOTEN_ADDRESS2 = shukkinDataRow["TR_KYOTEN_ADDRESS2"] == null ? string.Empty : shukkinDataRow["TR_KYOTEN_ADDRESS2"].ToString();
            //拠点TEL
            seisanDenpyouKagamiEntity.KYOTEN_TEL = shukkinDataRow["TR_KYOTEN_TEL"] == null ? string.Empty : shukkinDataRow["TR_KYOTEN_TEL"].ToString();
            //拠点FAX
            seisanDenpyouKagamiEntity.KYOTEN_FAX = shukkinDataRow["TR_KYOTEN_FAX"] == null ? string.Empty : shukkinDataRow["TR_KYOTEN_FAX"].ToString();

            //今回支払額
            seisanDenpyouKagamiEntity.KONKAI_SHIHARAI_GAKU = SqlDecimal.Parse("0");
            //今回請内税額
            seisanDenpyouKagamiEntity.KONKAI_SEI_UTIZEI_GAKU = SqlDecimal.Parse("0");
            //今回請外税額
            seisanDenpyouKagamiEntity.KONKAI_SEI_SOTOZEI_GAKU = SqlDecimal.Parse("0");
            //今回伝内税額
            seisanDenpyouKagamiEntity.KONKAI_DEN_UTIZEI_GAKU = SqlDecimal.Parse("0");
            //今回伝外税額
            seisanDenpyouKagamiEntity.KONKAI_DEN_SOTOZEI_GAKU = SqlDecimal.Parse("0");
            //今回明内税額
            seisanDenpyouKagamiEntity.KONKAI_MEI_UTIZEI_GAKU = SqlDecimal.Parse("0");
            //今回明外税額
            seisanDenpyouKagamiEntity.KONKAI_MEI_SOTOZEI_GAKU = SqlDecimal.Parse("0");
            //削除フラグ
            seisanDenpyouKagamiEntity.DELETE_FLG = false;

            //業者CD
            seisanDenpyouKagamiEntity.GYOUSHA_CD = "";
            //現場CD
            seisanDenpyouKagamiEntity.GENBA_CD = "";
            //代表者印字区分
            seisanDenpyouKagamiEntity.DAIHYOU_PRINT_KBN = SqlInt16.Parse(BlankToZero(""));
            //拠点名印字区分
            seisanDenpyouKagamiEntity.KYOTEN_NAME_PRINT_KBN = shukkinDataRow["SHIHARAI_KYOTEN_PRINT_KBN"] == null ? SqlInt16.Parse("0") : SqlInt16.Parse(BlankToZero(shukkinDataRow["SHIHARAI_KYOTEN_PRINT_KBN"].ToString()));
            //拠点CD
            seisanDenpyouKagamiEntity.KYOTEN_CD = shukkinDataRow["SHIHARAI_KYOTEN_CD"] == null ? SqlInt16.Parse("0") : SqlInt16.Parse(BlankToZero(shukkinDataRow["SHIHARAI_KYOTEN_CD"].ToString()));
            //支払書送付先1
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME1 = shukkinDataRow["SHIHARAI_SOUFU_NAME1"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_NAME1"].ToString();
            //支払書送付先2
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME2 = shukkinDataRow["SHIHARAI_SOUFU_NAME2"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_NAME2"].ToString();
            //支払書送付先敬称1
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU1 = shukkinDataRow["SHIHARAI_SOUFU_KEISHOU1"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_KEISHOU1"].ToString();
            //支払書送付先敬称2
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU2 = shukkinDataRow["SHIHARAI_SOUFU_KEISHOU2"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_KEISHOU2"].ToString();
            //支払書送付先郵便番号
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_POST = shukkinDataRow["SHIHARAI_SOUFU_POST"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_POST"].ToString();
            //支払書送付先住所1
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS1 = shukkinDataRow["SHIHARAI_SOUFU_ADDRESS1"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_ADDRESS1"].ToString();
            //支払書送付先住所2
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS2 = shukkinDataRow["SHIHARAI_SOUFU_ADDRESS2"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_ADDRESS2"].ToString();
            //支払書送付先部署
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_BUSHO = shukkinDataRow["SHIHARAI_SOUFU_BUSHO"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_BUSHO"].ToString();
            //支払書送付先担当者
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TANTOU = shukkinDataRow["SHIHARAI_SOUFU_TANTOU"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_TANTOU"].ToString();
            //支払書送付先TEL
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TEL = shukkinDataRow["SHIHARAI_SOUFU_TEL"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_TEL"].ToString();
            //支払書送付先FAX
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_FAX = shukkinDataRow["SHIHARAI_SOUFU_FAX"] == null ? string.Empty : shukkinDataRow["SHIHARAI_SOUFU_FAX"].ToString();
            #region 適格請求書
            // 今回課税区分①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_1 = SqlInt16.Parse("0");
            // 今回課税税率①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_1 = SqlDecimal.Parse("0");
            // 今回課税税抜金額①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_1 = SqlDecimal.Parse("0");
            // 今回課税税額①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_1 = SqlDecimal.Parse("0");
            // 今回課税区分②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_2 = SqlInt16.Parse("0");
            // 今回課税税率②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_2 = SqlDecimal.Parse("0");
            // 今回課税税抜金額②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_2 = SqlDecimal.Parse("0");
            // 今回課税税額②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_2 = SqlDecimal.Parse("0");
            // 今回課税区分③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_3 = SqlInt16.Parse("0");
            // 今回課税税率③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_3 = SqlDecimal.Parse("0");
            // 今回課税税抜金額③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_3 = SqlDecimal.Parse("0");
            // 今回課税税額③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_3 = SqlDecimal.Parse("0");
            // 今回課税区分④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_4 = SqlInt16.Parse("0");
            // 今回課税税率④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_4 = SqlDecimal.Parse("0");
            // 今回課税税抜金額④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_4 = SqlDecimal.Parse("0");
            // 今回課税税額④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_4 = SqlDecimal.Parse("0");
            //今回非課税区分
            seisanDenpyouKagamiEntity.KONKAI_HIKAZEI_KBN = SqlInt16.Parse("0");
            //今回非課税税抜金額
            seisanDenpyouKagamiEntity.KONKAI_HIKAZEI_GAKU = SqlDecimal.Parse("0");
            #endregion 適格請求書
            //精算伝票_鑑を追加
            seisanDenpyouKagamiEntitylist.Add(seisanDenpyouKagamiEntity);

            LogUtility.DebugMethodEnd(seikyuNo, shukkinDataRow);
        }

        #endregion

        #region 精算伝票_鑑(売上データなし用)作成

        /// <summary>
        /// 精算伝票_鑑(売上データなし用)作成
        /// </summary>
        /// <param name="seikyuNo">精算番号</param>
        private void CreateKagamiDenpyouUrinashiDetail(SqlInt64 seikyuNo)
        {
            LogUtility.DebugMethodStart(seikyuNo);

            T_SEISAN_DENPYOU_KAGAMI seisanDenpyouKagamiEntity = new T_SEISAN_DENPYOU_KAGAMI();

            //精算番号
            seisanDenpyouKagamiEntity.SEISAN_NUMBER = seikyuNo;
            //鑑番号
            seisanDenpyouKagamiEntity.KAGAMI_NUMBER = g_kagamiNo;
            //取引先CD
            //取引先CD
            if (seikyuPramList[0].SHIME_TANI == 1)
            {
                //期間締処理
                seisanDenpyouKagamiEntity.TORIHIKISAKI_CD = seikyuPramList[torihikisakiCount].SHIHARAI_CD.ToString();
            }
            else
            {
                //伝票締処理、明細締処理
                seisanDenpyouKagamiEntity.TORIHIKISAKI_CD = seikyuPramList[0].SHIHARAI_CD.ToString();
            }

            //自社情報取得
            M_CORP_INFO CorpEntity = new M_CORP_INFO();
            DataTable corpinfo = shimeShoriDao.GetCorpDataForEntity(CorpEntity);

            //会社名
            seisanDenpyouKagamiEntity.CORP_NAME = corpinfo.Rows[0]["CORP_NAME"].ToString();
            //代表者名
            seisanDenpyouKagamiEntity.CORP_DAIHYOU = corpinfo.Rows[0]["CORP_DAIHYOU"].ToString();

            //前回繰越額
            decimal zenkaigaku = GetZenkaikurikoshigaku();

            T_SEIKYUU_DENPYOU seikyuuDenpyouEntity = new T_SEIKYUU_DENPYOU();
            seikyuuDenpyouEntity.ZENKAI_KURIKOSI_GAKU = SqlDecimal.Parse(zenkaigaku.ToString());

            //出金、支払データ集計値作成
            SumSyukkinShiharaiData(zenkaigaku);

            //今回支払額
            seisanDenpyouKagamiEntity.KONKAI_SHIHARAI_GAKU = SqlDecimal.Parse(konkaiShiharaigaku.ToString());
            //今回請内税額
            seisanDenpyouKagamiEntity.KONKAI_SEI_UTIZEI_GAKU = SqlDecimal.Parse(konkaiSeiUtizeigaku.ToString());
            //今回請外税額
            seisanDenpyouKagamiEntity.KONKAI_SEI_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiSeisotozeigaku.ToString());
            //今回伝内税額
            seisanDenpyouKagamiEntity.KONKAI_DEN_UTIZEI_GAKU = SqlDecimal.Parse(konkaiDenUtizeigaku.ToString());
            //今回伝外税額
            seisanDenpyouKagamiEntity.KONKAI_DEN_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiDensotozeigaku.ToString());
            //今回明内税額
            seisanDenpyouKagamiEntity.KONKAI_MEI_UTIZEI_GAKU = SqlDecimal.Parse(konkaiMeiUtizeigaku.ToString());
            //今回明外税額
            seisanDenpyouKagamiEntity.KONKAI_MEI_SOTOZEI_GAKU = SqlDecimal.Parse(konkaiMeisotozeigaku.ToString());
            //削除フラグ
            seisanDenpyouKagamiEntity.DELETE_FLG = false;

            //業者CD
            seisanDenpyouKagamiEntity.GYOUSHA_CD = "";
            //現場CD
            seisanDenpyouKagamiEntity.GENBA_CD = "";
            //代表者印字区分
            seisanDenpyouKagamiEntity.DAIHYOU_PRINT_KBN = SqlInt16.Parse(BlankToZero(""));
            //拠点名印字区分
            seisanDenpyouKagamiEntity.KYOTEN_NAME_PRINT_KBN = SqlInt16.Parse(BlankToZero(this.kurikosiMasterResult.Rows[0]["SHIHARAI_KYOTEN_PRINT_KBN"].ToString()));
            //拠点CD
            seisanDenpyouKagamiEntity.KYOTEN_CD = SqlInt16.Parse(BlankToZero(this.kurikosiMasterResult.Rows[0]["SHIHARAI_KYOTEN_CD"].ToString()));
            //拠点名
            seisanDenpyouKagamiEntity.KYOTEN_NAME = this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_NAME"] == null ? string.Empty : this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_NAME"].ToString();
            //拠点代表者名
            seisanDenpyouKagamiEntity.KYOTEN_DAIHYOU = this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_DAIHYOU"] == null ? string.Empty : this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_DAIHYOU"].ToString();
            //拠点郵便番号
            seisanDenpyouKagamiEntity.KYOTEN_POST = this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_POST"] == null ? string.Empty : this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_POST"].ToString();
            //拠点住所1
            seisanDenpyouKagamiEntity.KYOTEN_ADDRESS1 = this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_ADDRESS1"] == null ? string.Empty : this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_ADDRESS1"].ToString();
            //拠点住所2
            seisanDenpyouKagamiEntity.KYOTEN_ADDRESS2 = this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_ADDRESS2"] == null ? string.Empty : this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_ADDRESS2"].ToString();
            //拠点TEL
            seisanDenpyouKagamiEntity.KYOTEN_TEL = this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_TEL"] == null ? string.Empty : this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_TEL"].ToString();
            //拠点FAX
            seisanDenpyouKagamiEntity.KYOTEN_FAX = this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_FAX"] == null ? string.Empty : this.kurikosiMasterResult.Rows[0]["SEIKYUU_KYOTEN_FAX"].ToString();
            //支払書送付先1
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME1 = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_NAME1"].ToString();
            //支払書送付先2
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_NAME2 = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_NAME2"].ToString();
            //支払書送付先敬称1
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU1 = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_KEISHOU1"].ToString();
            //支払書送付先敬称2
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_KEISHOU2 = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_KEISHOU2"].ToString();
            //支払書送付先郵便番号
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_POST = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_POST"].ToString();
            //支払書送付先住所1
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS1 = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_ADDRESS1"].ToString();
            //支払書送付先住所2
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_ADDRESS2 = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_ADDRESS2"].ToString();
            //支払書送付先部署
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_BUSHO = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_BUSHO"].ToString();
            //支払書送付先担当者
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TANTOU = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_TANTOU"].ToString();
            //支払書送付先TEL
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_TEL = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_TEL"].ToString();
            //支払書送付先FAX
            seisanDenpyouKagamiEntity.SHIHARAI_SOUFU_FAX = this.kurikosiMasterResult.Rows[0]["SHIHARAI_SOUFU_FAX"].ToString();
            #region 適格請求書
            // 今回課税区分①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_1 = SqlInt16.Parse("0");
            // 今回課税税率①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_1 = SqlDecimal.Parse("0");
            // 今回課税税抜金額①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_1 = SqlDecimal.Parse("0");
            // 今回課税税額①
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_1 = SqlDecimal.Parse("0");
            // 今回課税区分②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_2 = SqlInt16.Parse("0");
            // 今回課税税率②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_2 = SqlDecimal.Parse("0");
            // 今回課税税抜金額②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_2 = SqlDecimal.Parse("0");
            // 今回課税税額②
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_2 = SqlDecimal.Parse("0");
            // 今回課税区分③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_3 = SqlInt16.Parse("0");
            // 今回課税税率③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_3 = SqlDecimal.Parse("0");
            // 今回課税税抜金額③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_3 = SqlDecimal.Parse("0");
            // 今回課税税額③
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_3 = SqlDecimal.Parse("0");
            // 今回課税区分④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_KBN_4 = SqlInt16.Parse("0");
            // 今回課税税率④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_RATE_4 = SqlDecimal.Parse("0");
            // 今回課税税抜金額④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_GAKU_4 = SqlDecimal.Parse("0");
            // 今回課税税額④
            seisanDenpyouKagamiEntity.KONKAI_KAZEI_ZEIGAKU_4 = SqlDecimal.Parse("0");
            //今回非課税区分
            seisanDenpyouKagamiEntity.KONKAI_HIKAZEI_KBN = SqlInt16.Parse("0");
            //今回非課税税抜金額
            seisanDenpyouKagamiEntity.KONKAI_HIKAZEI_GAKU = SqlDecimal.Parse("0");
            #endregion 適格請求書

            //精算伝票_鑑を追加
            seisanDenpyouKagamiEntitylist.Add(seisanDenpyouKagamiEntity);

            LogUtility.DebugMethodEnd(seikyuNo);
        }

        #endregion

        #region 出金、支払データ集計値作成

        private void SumSyukkinShiharaiData(decimal zenkaiGaku)
        {
            SumSyukkinShiharaiData(zenkaiGaku, "1", string.Empty, string.Empty);
        }

        private void SumSyukkinShiharaiData(decimal zenkaiGaku, string shoshikiKbn, string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(zenkaiGaku, shoshikiKbn, gyoushaCd, genbaCd);

            //保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            //出金データから集計
            for (int i = 0; i < shukinResult.Rows.Count; i++)
            {
                if (saveSysID != shukinResult.Rows[i]["SYSTEM_ID"].ToString())
                {
                    //今回出金額
                    if (shukinResult.Rows[i]["SHUKKIN_AMOUNT_TOTAL"] != null)
                    {
                        konkaiShukingaku = konkaiShukingaku + decimal.Parse(shukinResult.Rows[i]["SHUKKIN_AMOUNT_TOTAL"].ToString());
                    }

                    //今回調整額
                    if (shukinResult.Rows[i]["CHOUSEI_AMOUNT_TOTAL"] != null)
                    {
                        konkaiChouseigaku = konkaiChouseigaku + decimal.Parse(shukinResult.Rows[i]["CHOUSEI_AMOUNT_TOTAL"].ToString());
                    }

                    //システムＩＤの保存
                    saveSysID = shukinResult.Rows[i]["SYSTEM_ID"].ToString();
                }
            }

            //保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            //支払データから集計
            if (InvoiceKBN == 2)
            {
                //適格請求書
                switch (shoshikiKbn)
                {
                    case "1":
                        //請求別
                        CreatetSeikyuuShiharaiData_invoice();
                        break;

                    case "2":
                        //業者別
                        CreatetGyoushaShiharaiData_invoice(gyoushaCd);
                        break;

                    case "3":
                        //現場別
                        CreatetGenbaShiharaiData_invoice(gyoushaCd, genbaCd);
                        break;

                    default:
                        break;
                }
            }
            else if (InvoiceKBN == 1)
            {
                //旧請求書
                switch (shoshikiKbn)
                {
                    case "1":
                        //請求別
                        CreatetSeikyuuShiharaiData();
                        break;

                    case "2":
                        //業者別
                        CreatetGyoushaShiharaiData(gyoushaCd);
                        break;

                    case "3":
                        //現場別
                        CreatetGenbaShiharaiData(gyoushaCd, genbaCd);
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

            LogUtility.DebugMethodEnd(zenkaiGaku, shoshikiKbn, gyoushaCd, genbaCd);
        }

        /// <summary>
        /// 出金データ集計地を作成します。
        /// </summary>
        private void SumShukkinData()
        {
            LogUtility.DebugMethodStart();

            // 保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            // 出金データから集計
            for (int i = 0; i < shukinResult.Rows.Count; i++)
            {
                if (saveSysID != shukinResult.Rows[i]["SYSTEM_ID"].ToString())
                {
                    // 今回出金額
                    if (shukinResult.Rows[i]["SHUKKIN_AMOUNT_TOTAL"] != null)
                    {
                        konkaiShukingaku = konkaiShukingaku + decimal.Parse(shukinResult.Rows[i]["SHUKKIN_AMOUNT_TOTAL"].ToString());
                    }

                    // 今回調整額
                    if (shukinResult.Rows[i]["CHOUSEI_AMOUNT_TOTAL"] != null)
                    {
                        konkaiChouseigaku = konkaiChouseigaku + decimal.Parse(shukinResult.Rows[i]["CHOUSEI_AMOUNT_TOTAL"].ToString());
                    }

                    // システムＩＤの保存
                    saveSysID = shukinResult.Rows[i]["SYSTEM_ID"].ToString();
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 支払データ集計値を作成します。(for 業者別、現場別請求)
        /// </summary>
        /// <param name="shoshikiKbn">取引先_精算情報マスタ.書式区分</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        private void SumShiharaiData(string shoshikiKbn, string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(shoshikiKbn, gyoushaCd, genbaCd);

            // 保存用システムＩＤ、ＳＥＱの初期化
            saveSysID = "";

            // 支払データから集計
            if (InvoiceKBN == 2)
            {
                //適格請求書
                switch (shoshikiKbn)
                {
                    case "1":
                        // 請求別
                        CreatetSeikyuuShiharaiData_invoice();
                        break;

                    case "2":
                        // 業者別
                        CreatetGyoushaShiharaiData_invoice(gyoushaCd);
                        break;

                    case "3":
                        // 現場別
                        CreatetGenbaShiharaiData_invoice(gyoushaCd, genbaCd);
                        break;

                    default:
                        break;
                }
            }
            else if (InvoiceKBN == 1)
            {
                //旧請求書
                switch (shoshikiKbn)
                {
                    case "1":
                        // 請求別
                        CreatetSeikyuuShiharaiData();
                        break;

                    case "2":
                        // 業者別
                        CreatetGyoushaShiharaiData(gyoushaCd);
                        break;

                    case "3":
                        // 現場別
                        CreatetGenbaShiharaiData(gyoushaCd, genbaCd);
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

            LogUtility.DebugMethodEnd(shoshikiKbn, gyoushaCd, genbaCd);
        }

        #endregion

        #region 売上データ算出

        #region 売上データ算出(請求)

        /// <summary>
        /// 売上データ算出(請求)
        /// </summary>
        private void CreatetSeikyuuShiharaiData()
        {
            LogUtility.DebugMethodStart();

            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            int zikubun = 0;
            decimal shohizeirate = 0;
            int count = 0;
            string torihikicd = "";

            // 請求毎税の計算
            DataView dv = new DataView(tougouuriageData);
            DataTable table = tougouuriageData.Clone();
            dv.Sort = "SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SHIHARAI_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            //請求毎税以外の計算
            for (int i = 0; i < table.Rows.Count; i++)
            {
                CreatetUriageData(table.Rows[i]);
            }

            if (table.Rows.Count != 0)
            {
                //保存用システムＩＤ、ＳＥＱの初期化
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
                                                 decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

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
                                                         CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), table.Rows[i]["TORIHIKISAKI_CD"].ToString());

                                //クリア
                                uriageTotal = 0;

                                //集計
                                uriageTotal = uriageTotal +
                                             decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                //システムID、SEQの保存
                                saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                            }

                            //税区分が内税:2
                            if (zikubun == 2)
                            {
                                //請求毎の消費税額算出
                                konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                         CreateSeikyuuTaxUti(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), table.Rows[i]["TORIHIKISAKI_CD"].ToString());

                                //クリア
                                uriageTotal = 0;

                                //集計
                                uriageTotal = uriageTotal +
                                             decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                //システムID、SEQの保存
                                saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                            }

                            //税区分と消費税の保存
                            zikubun = int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString());
                            shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
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
                                                 CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    }

                    //税区分が内税:2
                    if (zikubun == 2)
                    {
                        //請求毎の消費税額算出
                        konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                 CreateSeikyuuTaxUti(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    }
                }
            }
            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 売上データ算出(業者)

        /// <summary>
        /// 売上データ算出(業者)
        /// </summary>
        private void CreatetGyoushaShiharaiData(string gyoushaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd);

            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            int zikubun = 0;
            decimal shohizeirate = 0;
            int count = 0;
            string torihikicd = "";

            // 請求毎税の計算
            DataView dv = new DataView(tougouuriageData);
            DataTable table = tougouuriageData.Clone();
            dv.Sort = "GYOUSHA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SHIHARAI_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
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

            if (table.Rows.Count != 0)
            {
                //保存用システムＩＤ、ＳＥＱの初期化
                saveSysID = "";
                saveDenshuKbn = "";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
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

                            if ((table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd) &&
                                    (int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()) == zikubun) &&
                                   (decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()) == shohizeirate))
                            {
                                if (!(saveSysID.Equals(table.Rows[i]["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(table.Rows[i]["DENPYOUSHURUI"].ToString())))
                                {
                                    uriageTotal = uriageTotal +
                                                    decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

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
                                                             CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), table.Rows[i]["TORIHIKISAKI_CD"].ToString());

                                    //クリア
                                    uriageTotal = 0;

                                    //集計
                                    uriageTotal = uriageTotal +
                                                decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }
                                //税区分が内税
                                else if (zikubun == 2)
                                {
                                    //請求毎の消費税額算出
                                    konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                             CreateSeikyuuTaxUti(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), table.Rows[i]["TORIHIKISAKI_CD"].ToString());

                                    //クリア
                                    uriageTotal = 0;

                                    //集計
                                    uriageTotal = uriageTotal +
                                                decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }

                                //税区分と消費税の保存
                                zikubun = int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString());
                                shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
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
                                                 CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    }

                    //税区分が内税:2
                    if (zikubun == 2)
                    {
                        //請求毎の消費税額算出
                        konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                 CreateSeikyuuTaxUti(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    }
                }
            }
            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

            LogUtility.DebugMethodEnd(gyoushaCd);
        }

        #endregion

        #region 売上データ算出(現場)

        /// <summary>
        /// 売上データ算出(受入)現場
        /// </summary>
        private void CreatetGenbaShiharaiData(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            //請求毎税の計算用項目の初期化
            decimal uriageTotal = 0;
            int zikubun = 0;
            decimal shohizeirate = 0;
            int count = 0;
            string torihikicd = "";

            // 請求毎税の計算
            DataView dv = new DataView(tougouuriageData);
            DataTable table = tougouuriageData.Clone();
            dv.Sort = "GYOUSHA_CD,GENBA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SHIHARAI_SHOUHIZEI_RATE,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
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

            if (table.Rows.Count != 0)
            {
                //保存用システムＩＤ、ＳＥＱの初期化
                saveSysID = "";
                saveDenshuKbn = "";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && table.Rows[i]["GENBA_CD"].ToString() == genbaCd)
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

                            if ((table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd) &&
                                     (int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()) == zikubun) &&
                                    (decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()) == shohizeirate))
                            {
                                if (!(saveSysID.Equals(table.Rows[i]["SYSTEM_ID"].ToString()) && saveDenshuKbn.Equals(table.Rows[i]["DENPYOUSHURUI"].ToString())))
                                {
                                    uriageTotal = uriageTotal +
                                                    decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

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
                                                             CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), table.Rows[i]["TORIHIKISAKI_CD"].ToString());

                                    //クリア
                                    uriageTotal = 0;

                                    //集計
                                    uriageTotal = uriageTotal +
                                                decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }
                                //税区分が内税
                                else if (zikubun == 2)
                                {
                                    //請求毎の消費税額算出
                                    konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                             CreateSeikyuuTaxUti(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), table.Rows[i]["TORIHIKISAKI_CD"].ToString());

                                    //クリア
                                    uriageTotal = 0;

                                    //集計
                                    uriageTotal = uriageTotal +
                                                decimal.Parse(BlankToZero(table.Rows[i]["SHIHARAI_KINGAKU_TOTAL"].ToString()));

                                    //システムID、SEQの保存
                                    saveSysID = table.Rows[i]["SYSTEM_ID"].ToString();
                                    saveDenshuKbn = table.Rows[i]["DENPYOUSHURUI"].ToString();
                                }

                                //税区分と消費税の保存
                                zikubun = int.Parse(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString());
                                shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
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
                                                 CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    }

                    //税区分が内税:2
                    if (zikubun == 2)
                    {
                        //請求毎の消費税額算出
                        konkaiSeiUtizeigaku = konkaiSeiUtizeigaku +
                                                 CreateSeikyuuTaxUti(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    }
                }
            }

            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

            LogUtility.DebugMethodEnd(gyoushaCd, genbaCd);
        }

        #endregion

        #region 売上データ算出

        /// <summary>
        /// 売上データ算出
        /// </summary>
        private void CreatetUriageData(DataRow row)
        {
            LogUtility.DebugMethodStart(row);

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
                    w_ShiharaiTotal = w_ShiharaiTotal + decimal.Parse(BlankToZero(row["SHIHARAI_KINGAKU_TOTAL"].ToString()));
                }

                //品名別支払金額合計
                if (row["HINMEI_SHIHARAI_KINGAKU_TOTAL"] != null)
                {
                    w_HinmeiShiharaiTotal = w_HinmeiShiharaiTotal + decimal.Parse(BlankToZero(row["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()));
                }

                //支払税計算区分が伝票毎:1
                if (row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == "1")
                {
                    //支払伝票毎内税
                    if (row["SHIHARAI_TAX_UCHI"] != null)
                    {
                        w_ShiharaiDenUti = w_ShiharaiDenUti + decimal.Parse(BlankToZero(row["SHIHARAI_TAX_UCHI"].ToString()));
                    }

                    //支払伝票毎外税
                    if (row["SHIHARAI_TAX_SOTO"] != null)
                    {
                        w_ShiharaiDenSoto = w_ShiharaiDenSoto + decimal.Parse(BlankToZero(row["SHIHARAI_TAX_SOTO"].ToString()));
                    }
                }

                //支払税計算区分が明細毎:3
                if (row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == "3")
                {
                    //支払明細毎内税
                    if (row["SHIHARAI_TAX_UCHI_TOTAL"] != null)
                    {
                        w_ShiharaiMeiUti = w_ShiharaiMeiUti + decimal.Parse(BlankToZero(row["SHIHARAI_TAX_UCHI_TOTAL"].ToString()));
                    }

                    //支払明細毎外税
                    if (row["SHIHARAI_TAX_SOTO_TOTAL"] != null)
                    {
                        w_ShiharaiMeiSoto = w_ShiharaiMeiSoto + decimal.Parse(BlankToZero(row["SHIHARAI_TAX_SOTO_TOTAL"].ToString()));
                    }
                }

                //品名別支払消費税内税計
                if (row["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"] != null)
                {
                    w_HinmeiShiharaiTaxUti_Mei = w_HinmeiShiharaiTaxUti_Mei + decimal.Parse(BlankToZero(row["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()));
                }

                //品名別支払消費税外税計
                if (row["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"] != null)
                {
                    w_HinmeiShiharaiTaxSoto_Mei = w_HinmeiShiharaiTaxSoto_Mei + decimal.Parse(BlankToZero(row["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()));
                }

                //システムID、SEQの保存
                saveSysID = row["SYSTEM_ID"].ToString();
                saveDenshuKbn = row["DENPYOUSHURUI"].ToString();
            }
            //登録用売上データ算出
            CreateInsertUriageData(w_ShiharaiTotal,
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

            LogUtility.DebugMethodEnd(row);
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
            LogUtility.DebugMethodStart(w_uke_UriageTotal,
                                        w_uke_HinmeiUriageTotal,
                                        w_uke_UriageDenUti,
                                        w_uke_UriageDenSoto,
                                        w_uke_UriageMeiUti,
                                        w_uke_UriageMeiSoto,
                                        w_uke_UriageSeiUti,
                                        w_uke_UriageSeiSoto,
                                        w_uke_HinmeiUriageTaxUti_Sei,
                                        w_uke_HinmeiUriageTaxSoto_Sei,
                                        w_uke_HinmeiUriageTaxUti_Mei,
                                        w_uke_HinmeiUriageTaxSoto_Mei);

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

            LogUtility.DebugMethodStart(w_uke_UriageTotal,
                                        w_uke_HinmeiUriageTotal,
                                        w_uke_UriageDenUti,
                                        w_uke_UriageDenSoto,
                                        w_uke_UriageMeiUti,
                                        w_uke_UriageMeiSoto,
                                        w_uke_UriageSeiUti,
                                        w_uke_UriageSeiSoto,
                                        w_uke_HinmeiUriageTaxUti_Sei,
                                        w_uke_HinmeiUriageTaxSoto_Sei,
                                        w_uke_HinmeiUriageTaxUti_Mei,
                                        w_uke_HinmeiUriageTaxSoto_Mei);
        }

        #endregion

        #endregion

        #region 前回繰越額取得

        /// <summary>
        /// 前回繰越額取得
        /// </summary>
        /// <returns>前回繰越額</returns>
        private decimal GetZenkaikurikoshigaku()
        {
            LogUtility.DebugMethodStart();

            if (kurikosiResult.Rows.Count == 0)
            {
                LogUtility.DebugMethodEnd();
                return decimal.Parse(kurikosiMasterResult.Rows[0]["KAISHI_KAIKAKE_ZANDAKA"].ToString());
            }
            else
            {
                LogUtility.DebugMethodEnd();
                return decimal.Parse(kurikosiResult.Rows[0]["KONKAI_SEISAN_GAKU"].ToString());
            }
        }

        #endregion

        #region 請求毎の消費税額算出

        /// <summary>
        /// 請求毎の消費税額算出(外税)
        /// </summary>
        /// <param name="uriageTotal">売上合計</param>
        /// <param name="tax">税率</param>
        /// <returns>算出消費税</returns>
        private decimal CreateSeikyuuTaxSoto(decimal uriageTotal,
                                            decimal tax, string torihikisaki)
        {
            LogUtility.DebugMethodStart(uriageTotal, tax, torihikisaki);

            string hasuuCD = "";
            decimal shohizei = 0;
            decimal sign = 1;
            if (uriageTotal < 0)
            {
                // 売上合計が負の場合
                sign = -1;
            }

            foreach (DataRow row in kurikosiMasterResult.Rows)
            {
                if (row["TORIHIKISAKI_CD"].ToString() == torihikisaki)
                {
                    hasuuCD = row["TAX_HASUU_CD"].ToString();
                }
            }
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

            LogUtility.DebugMethodEnd(uriageTotal, tax, torihikisaki);

            return shohizei;
        }

        /// <summary>
        /// 請求毎の消費税額算出(内税)
        /// </summary>
        /// <param name="uriageTotal">売上合計</param>
        /// <param name="tax">税率</param>
        /// <returns>算出消費税</returns>
        private decimal CreateSeikyuuTaxUti(decimal uriageTotal,
                                           decimal tax, string torihikisaki)
        {
            LogUtility.DebugMethodStart(uriageTotal, tax, torihikisaki);

            string hasuuCD = "";
            decimal shohizei = 0;
            decimal sign = 1;
            if (uriageTotal < 0)
            {
                // 売上合計が負の場合
                sign = -1;
            }

            foreach (DataRow row in kurikosiMasterResult.Rows)
            {
                if (row["TORIHIKISAKI_CD"].ToString() == torihikisaki)
                {
                    hasuuCD = row["TAX_HASUU_CD"].ToString();
                }
            }
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

            LogUtility.DebugMethodEnd(uriageTotal, tax, torihikisaki);

            return shohizei;
        }

        #endregion

        #region 精算明細取得(出金明細以外)

        /// <summary>
        /// 精算明細取得(出金明細以外)
        /// </summary>
        /// <param name="meisaiRow">明細作成用データ</param>
        /// <param name="seikyuNo">精算番号</param>
        /// <param name="rowNo">対象行</param>
        private void CreateMeisaiDenpyouDetail(DataRow meisaiRow, SqlInt64 seikyuNo, SqlInt32 rowNo)
        {
            LogUtility.DebugMethodStart(meisaiRow, seikyuNo, rowNo);

            T_SEISAN_DETAIL seisanDetailEntity = new T_SEISAN_DETAIL();

            //精算番号
            seisanDetailEntity.SEISAN_NUMBER = seikyuNo;
            //鑑番号
            seisanDetailEntity.KAGAMI_NUMBER = g_kagamiNo;
            //行番号
            seisanDetailEntity.ROW_NUMBER = rowNo;
            //伝票種類CD
            seisanDetailEntity.DENPYOU_SHURUI_CD = GetDenpyouSyurui(int.Parse(meisaiRow["DENPYOUSHURUI"].ToString()));
            //伝票システムID
            seisanDetailEntity.DENPYOU_SYSTEM_ID = SqlInt64.Parse(BlankToZero(meisaiRow["SYSTEM_ID"].ToString()));
            //伝票枝番
            seisanDetailEntity.DENPYOU_SEQ = SqlInt32.Parse(BlankToZero(meisaiRow["SEQ"].ToString()));
            //明細システムID
            seisanDetailEntity.DETAIL_SYSTEM_ID = SqlInt64.Parse(BlankToZero(meisaiRow["DETAIL_SYSTEM_ID"].ToString()));
            //伝票番号
            //seisanDetailEntity.DENPYOU_NUMBER = GetDenpyouNo(int.Parse(meisaiRow["DENPYOUSHURUI"].ToString()), meisaiRow);
            seisanDetailEntity.DENPYOU_NUMBER = long.Parse(meisaiRow["DENPYONO"].ToString());
            //伝票日付
            //親伝票の【伝票日付】
            seisanDetailEntity.DENPYOU_DATE = SqlDateTime.Parse(meisaiRow["DENPYOU_DATE"].ToString());
            //取引先CD
            //親伝票の【取引先CD】
            seisanDetailEntity.TORIHIKISAKI_CD = meisaiRow["TORIHIKISAKI_CD"].ToString();
            //業者CD
            //親伝票の【業者CD】
            seisanDetailEntity.GYOUSHA_CD = meisaiRow["GYOUSHA_CD"].ToString();
            //マスタから取得
            //業者名1
            seisanDetailEntity.GYOUSHA_NAME1 = meisaiRow["GYOUSHA_NAME1"].ToString();
            //業者名2
            seisanDetailEntity.GYOUSHA_NAME2 = meisaiRow["GYOUSHA_NAME2"].ToString();
            //現場CD
            //親伝票の【現場CD】
            seisanDetailEntity.GENBA_CD = meisaiRow["GENBA_CD"].ToString();
            //現場名1
            seisanDetailEntity.GENBA_NAME1 = meisaiRow["GENBA_NAME1"].ToString();
            //現場名2
            seisanDetailEntity.GENBA_NAME2 = meisaiRow["GENBA_NAME2"].ToString();

            //品名CD
            seisanDetailEntity.HINMEI_CD = meisaiRow["HINMEI_CD"].ToString();
            //品名
            seisanDetailEntity.HINMEI_NAME = meisaiRow["HINMEI_NAME"].ToString();
            //数量
            seisanDetailEntity.SUURYOU = SqlDecimal.Parse(BlankToZero(meisaiRow["SUURYOU"].ToString()));
            //単位CD
            seisanDetailEntity.UNIT_CD = SqlInt16.Parse(BlankToZero(meisaiRow["UNIT_CD"].ToString()));
            //単位名
            seisanDetailEntity.UNIT_NAME = meisaiRow["UNIT_NAME"].ToString();
            //単価
            if (string.IsNullOrEmpty(meisaiRow["TANKA"].ToString()))
            {
                seisanDetailEntity.TANKA = SqlDecimal.Null;
            }
            else
            {
                seisanDetailEntity.TANKA = SqlDecimal.Parse(meisaiRow["TANKA"].ToString());
            }
            //金額
            seisanDetailEntity.KINGAKU = SqlDecimal.Parse(BlankToZero(meisaiRow["KINGAKU"].ToString())) + SqlDecimal.Parse(BlankToZero(meisaiRow["HINMEI_KINGAKU"].ToString()));

            //初期化＠0リセット
            //内税額
            seisanDetailEntity.UCHIZEI_GAKU = 0;
            //外税額
            seisanDetailEntity.SOTOZEI_GAKU = 0;
            //伝票内税額
            seisanDetailEntity.DENPYOU_UCHIZEI_GAKU = 0;
            //伝票外税額
            seisanDetailEntity.DENPYOU_SOTOZEI_GAKU = 0;

            //税計算区分・税区分によって計上する税額を切り替える
            Int16 hinmei_zei_kbn_cd = Int16.Parse(BlankToZero(meisaiRow["HINMEI_ZEI_KBN_CD"].ToString()));

            //品名毎税区分による格納先の分岐
            switch (hinmei_zei_kbn_cd)
            {
                //外税
                case 1:
                    seisanDetailEntity.SOTOZEI_GAKU = SqlDecimal.Parse(BlankToZero(meisaiRow["HINMEI_TAX_SOTO"].ToString()));
                    break;

                //内税
                case 2:
                    seisanDetailEntity.UCHIZEI_GAKU = SqlDecimal.Parse(BlankToZero(meisaiRow["HINMEI_TAX_UCHI"].ToString()));
                    break;

                //非課税
                case 3:
                    break;

                //品名に税区分の設定がない場合は何もしない
                default:
                    break;
            }

            Int16 zei_keisan_kbn_cd = Int16.Parse(BlankToZero(meisaiRow["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()));
            Int16 zei_kbn_cd = Int16.Parse(BlankToZero(meisaiRow["SHIHARAI_ZEI_KBN_CD"].ToString()));

            switch (zei_keisan_kbn_cd)
            {
                //伝票毎
                case 1:
                    //税区分による格納先の分岐
                    switch (zei_kbn_cd)
                    {
                        //外税
                        case 1:
                            seisanDetailEntity.DENPYOU_SOTOZEI_GAKU = SqlDecimal.Parse(BlankToZero(meisaiRow["SHIHARAI_TAX_SOTO"].ToString()));
                            break;

                        //内税
                        case 2:
                            seisanDetailEntity.DENPYOU_UCHIZEI_GAKU = SqlDecimal.Parse(BlankToZero(meisaiRow["SHIHARAI_TAX_UCHI"].ToString()));
                            break;

                        //非課税、他
                        default:
                            break;
                    };

                    break;

                //精算毎
                case 2:
                    //精算毎税はDETAILに税額の格納の必要なし
                    break;

                //明細毎
                case 3:
                    //品名税区分が計上されている場合はスルーする(こちら側の値は設定されないため)
                    if (hinmei_zei_kbn_cd != 0)
                    {
                        break;
                    }

                    //税区分による格納先の分岐
                    switch (zei_kbn_cd)
                    {
                        //外税
                        case 1:
                            seisanDetailEntity.SOTOZEI_GAKU = SqlDecimal.Parse(BlankToZero(meisaiRow["TAX_SOTO"].ToString()));
                            break;

                        //内税
                        case 2:
                            seisanDetailEntity.UCHIZEI_GAKU = SqlDecimal.Parse(BlankToZero(meisaiRow["TAX_UCHI"].ToString()));
                            break;

                        //非課税、他
                        default:
                            break;
                    };

                    break;

                //税計算区分がどれでもない
                default:
                    break;
            };

            //伝票税計算区分CD
            if (meisaiRow["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() != null)
            {
                seisanDetailEntity.DENPYOU_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(BlankToZero(meisaiRow["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()));
            }

            ////伝票税区分CD
            if (meisaiRow["SHIHARAI_ZEI_KBN_CD"].ToString() != null)
            {
                seisanDetailEntity.DENPYOU_ZEI_KBN_CD = SqlInt16.Parse(BlankToZero(meisaiRow["SHIHARAI_ZEI_KBN_CD"].ToString()));
            }

            //明細税区分CD
            if (meisaiRow["HINMEI_ZEI_KBN_CD"].ToString() != null)
            {
                seisanDetailEntity.MEISAI_ZEI_KBN_CD = SqlInt16.Parse(BlankToZero(meisaiRow["HINMEI_ZEI_KBN_CD"].ToString()));
            }
            //明細備考
            seisanDetailEntity.MEISAI_BIKOU = meisaiRow["MEISAI_BIKOU"].ToString();
            //削除フラグ
            seisanDetailEntity.DELETE_FLG = false;

            #region 適格請求書
            //消費税率
            seisanDetailEntity.SHOUHIZEI_RATE = SqlDecimal.Parse(meisaiRow["SHIHARAI_SHOUHIZEI_RATE"].ToString());
            #endregion 適格請求書

            //共通機能での登録
            ////タイムスタンプ
            //seisanDetailEntity.TIME_STAMP =

            //精算明細を追加
            seisanDetailEntitylist.Add(seisanDetailEntity);

            LogUtility.DebugMethodEnd(meisaiRow, seikyuNo, rowNo);
        }

        #endregion

        #region 精算明細取得(出金明細)

        /// <summary>
        /// 精算明細取得(出金明細)
        /// </summary>
        /// <param name="meisaiRow">明細作成用データ</param>
        /// <param name="densyurui">伝票種類</param>
        /// <param name="seikyuNo">精算番号</param>
        /// <param name="rowNo">対象行</param>
        private void CreateNyuukinDenpyouDetail(DataRow meisaiRow, DenpyouSyurui densyurui, SqlInt64 seikyuNo, SqlInt32 rowNo)
        {
            LogUtility.DebugMethodStart(meisaiRow, densyurui, seikyuNo, rowNo);

            T_SEISAN_DETAIL seisanDetailEntity = new T_SEISAN_DETAIL();

            //精算番号
            seisanDetailEntity.SEISAN_NUMBER = seikyuNo;
            //鑑番号
            seisanDetailEntity.KAGAMI_NUMBER = g_kagamiNo;
            //行番号
            seisanDetailEntity.ROW_NUMBER = rowNo;
            //伝票種類CD
            seisanDetailEntity.DENPYOU_SHURUI_CD = 20;
            //伝票システムID
            seisanDetailEntity.DENPYOU_SYSTEM_ID = SqlInt64.Parse(BlankToZero(meisaiRow["SYSTEM_ID"].ToString()));
            //伝票枝番
            seisanDetailEntity.DENPYOU_SEQ = SqlInt32.Parse(BlankToZero(meisaiRow["SEQ"].ToString()));
            //明細システムID
            seisanDetailEntity.DETAIL_SYSTEM_ID = SqlInt64.Parse(BlankToZero(meisaiRow["DETAIL_SYSTEM_ID"].ToString()));
            //伝票番号
            seisanDetailEntity.DENPYOU_NUMBER = SqlInt64.Parse(BlankToZero(meisaiRow["SHUKKIN_NUMBER"].ToString()));
            //伝票日付
            seisanDetailEntity.DENPYOU_DATE = SqlDateTime.Parse(meisaiRow["DENPYOU_DATE"].ToString());
            //取引先CD
            seisanDetailEntity.TORIHIKISAKI_CD = meisaiRow["TORIHIKISAKI_CD"].ToString();
            //品名CD
            seisanDetailEntity.HINMEI_CD = meisaiRow["NYUUSHUKKIN_KBN_CD"].ToString();
            //品名
            seisanDetailEntity.HINMEI_NAME = meisaiRow["NYUUSHUKKIN_KBN_NAME"].ToString();
            //数量
            seisanDetailEntity.SUURYOU = 0;
            //単位名
            seisanDetailEntity.UNIT_NAME = "";
            //単価
            seisanDetailEntity.TANKA = 0;
            //金額
            seisanDetailEntity.KINGAKU = SqlDecimal.Parse(BlankToZero(meisaiRow["KINGAKU"].ToString()));
            //内税額
            seisanDetailEntity.UCHIZEI_GAKU = 0;
            //外税額
            seisanDetailEntity.SOTOZEI_GAKU = 0;
            //伝票内税額
            seisanDetailEntity.DENPYOU_UCHIZEI_GAKU = 0;
            //伝票外税額
            seisanDetailEntity.DENPYOU_SOTOZEI_GAKU = 0;
            //明細備考
            seisanDetailEntity.MEISAI_BIKOU = meisaiRow["MEISAI_BIKOU"].ToString();
            //削除フラグ
            seisanDetailEntity.DELETE_FLG = false;

            //精算明細を追加
            seisanDetailEntitylist.Add(seisanDetailEntity);

            LogUtility.DebugMethodEnd(meisaiRow, densyurui, seikyuNo, rowNo);
        }

        #endregion

        #region 空白をゼロに置き換える

        /// <summary>
        /// 空白をゼロに置き換える
        /// </summary>
        /// <param name="chkText">チェック対象</param>
        /// <returns>置き換え後文字列</returns>
        private String BlankToZero(string chkText)
        {
            LogUtility.DebugMethodStart(chkText);

            if (chkText == "")
            {
                LogUtility.DebugMethodEnd(chkText);
                return "0";
            }
            else
            {
                LogUtility.DebugMethodEnd(chkText);
                return chkText;
            }
        }

        #endregion

        #region 締め処理実行履歴

        /// <summary>
        /// 締め処理実行履歴テーブルに登録
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        /// <param name="shimeNo">締め実行番号</param>
        [Transaction]
        private void InsertTShimeShoriRireki(SeikyuShimeShoriDto dto, SqlInt64 shimeNo)
        {
            LogUtility.DebugMethodStart(dto, shimeNo);

            T_SHIME_JIKKOU_RIREKI shimeJikkouRirekiEntity = new T_SHIME_JIKKOU_RIREKI();
            // WHOカラム設定共通ロジック呼び出し用
            var dataBind_shimeJikkouRirekiEntityAdd = new DataBinderLogic<T_SHIME_JIKKOU_RIREKI>(shimeJikkouRirekiEntity);

            //締実行番号
            shimeJikkouRirekiEntity.SHIME_JIKKOU_NO = shimeNo;
            //処理区分
            shimeJikkouRirekiEntity.SHORI_KBN = SqlInt16.Parse("2");
            //伝票種類
            shimeJikkouRirekiEntity.DENPYOU_SHURUI = SqlInt16.Parse(dto.DENPYO_SHURUI.ToString());
            //拠点CD
            shimeJikkouRirekiEntity.KYOTEN_CD = SqlInt16.Parse(dto.KYOTEN_CD.ToString());
            //締日（期間締の場合は締日を設定する、伝票締の場合は締日を設定しない）
            if (dto.SHIME_TANI == 1)
            {
                shimeJikkouRirekiEntity.SHIMEBI = SqlInt16.Parse(dto.SHIMEBI.ToString());
            }
            else
            {
                shimeJikkouRirekiEntity.SHIMEBI = SqlInt16.Null;
            }
            string hiduke = GetSeikyuuSimebi(dto.SHIHARAISHIMEBI_FROM, dto.SHIHARAISHIMEBI_TO);
            if (hiduke != "")
            {
                shimeJikkouRirekiEntity.HIDUKE_HANI_BEGIN = SqlDateTime.Parse(hiduke);
            }
            //日付範囲_終了
            shimeJikkouRirekiEntity.HIDUKE_HANI_END = SqlDateTime.Parse(dto.SHIHARAISHIMEBI_TO.ToString());

            // WHOカラム設定
            dataBind_shimeJikkouRirekiEntityAdd.SetSystemProperty(shimeJikkouRirekiEntity, false);

            using (Transaction tran = new Transaction())
            {
                int CntNyuukinIns = shimeJikkouRirekiDao.Insert(shimeJikkouRirekiEntity);
                tran.Commit();
            }

            LogUtility.DebugMethodEnd(dto, shimeNo);
        }

        #endregion

        #region 伝票種類CD取得

        /// <summary>
        /// 伝票種類CD取得
        /// </summary>
        /// <param name="denpyouSyurui">伝票種類</param>
        /// <returns></returns>
        private Int16 GetDenpyouSyurui(int denpyouSyurui)
        {
            LogUtility.DebugMethodStart(denpyouSyurui);

            switch (denpyouSyurui)
            {
                case 1:        //受入
                    LogUtility.DebugMethodEnd(denpyouSyurui);
                    return 1;

                case 2:        //出荷
                    LogUtility.DebugMethodEnd(denpyouSyurui);
                    return 2;

                case 3:  //売/支払
                    LogUtility.DebugMethodEnd(denpyouSyurui);
                    return 3;

                case 4:      //出金
                    LogUtility.DebugMethodEnd(denpyouSyurui);
                    return 10;

                default:
                    LogUtility.DebugMethodEnd(denpyouSyurui);
                    return 0;
            }
        }

        #endregion

        #region 伝票番号取得

        /// <summary>
        /// 伝票番号取得
        /// </summary>
        /// <param name="denpyouSyurui">伝票種類</param>
        /// <param name="meisai">対象行</param>
        /// <returns>伝票番号</returns>
        private long GetDenpyouNo(int denpyouSyurui, DataRow meisai)
        {
            LogUtility.DebugMethodStart(denpyouSyurui, meisai);

            switch (denpyouSyurui)
            {
                case 1:         //受入
                    LogUtility.DebugMethodEnd(denpyouSyurui, meisai);
                    return long.Parse(meisai["UKEIRE_NUMBER"].ToString());

                case 2:         //出荷
                    LogUtility.DebugMethodEnd(denpyouSyurui, meisai);
                    return long.Parse(meisai["SHUKKA_NUMBER"].ToString());

                case 3:   //売/支払
                    LogUtility.DebugMethodEnd(denpyouSyurui, meisai);
                    return long.Parse(meisai["UR_SH_NUMBER"].ToString());

                case 4:        //出金
                    LogUtility.DebugMethodEnd(denpyouSyurui, meisai);
                    return long.Parse(meisai["NYUUKIN_NUMBER"].ToString());

                default:
                    LogUtility.DebugMethodEnd(denpyouSyurui, meisai);
                    return 0;
            }
        }

        #endregion

        #region 締処理中テーブルに登録

        /// <summary>
        /// 締処理中テーブルに登録
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        /// <param name="shimeNo">締め実行番号</param>
        [Transaction]
        internal void InsertTShimeShoriChuu(SeikyuShimeShoriDto dto, SqlInt64 shimeNo)
        {
            LogUtility.DebugMethodStart(dto, shimeNo);

            T_SHIME_SHORI_CHUU seisanDetailEntity = new T_SHIME_SHORI_CHUU();
            // WHOカラム設定共通ロジック呼び出し用
            var dataBind_seisanDetailEntityAdd = new DataBinderLogic<T_SHIME_SHORI_CHUU>(seisanDetailEntity);

            //締実行番号
            seisanDetailEntity.SHIME_JIKKOU_NO = shimeNo;
            //取引先CD
            seisanDetailEntity.TORIHIKISAKI_CD = dto.SHIHARAI_CD;
            //処理区分
            seisanDetailEntity.SHORI_KBN = SqlInt16.Parse("2");
            //処理伝票種類
            seisanDetailEntity.SHORI_DENPYOU_SHURUI = SqlInt16.Parse(dto.DENPYO_SHURUI.ToString());
            //拠点CD
            seisanDetailEntity.KYOTEN_CD = SqlInt16.Parse(dto.KYOTEN_CD.ToString());
            //日付範囲_開始
            string hiduke = GetSeikyuuSimebi(dto.SHIHARAISHIMEBI_FROM, dto.SHIHARAISHIMEBI_TO);
            if (hiduke != "")
            {
                seisanDetailEntity.HIDUKE_HANI_BEGIN = SqlDateTime.Parse(hiduke);
            }
            //日付範囲_終了
            seisanDetailEntity.HIDUKE_HANI_END = SqlDateTime.Parse(dto.SHIHARAISHIMEBI_TO.ToString());

            // WHOカラム設定
            dataBind_seisanDetailEntityAdd.SetSystemProperty(seisanDetailEntity, false);

            GetEnvironmentInfoClass environment = new GetEnvironmentInfoClass();
            var name = environment.GetComputerAndUserName();
            // クライアントコンピュータ名
            seisanDetailEntity.CLIENT_COMPUTER_NAME = name.Item1;
            // クライアントユーザー名
            seisanDetailEntity.CLIENT_USER_NAME = name.Item2;

            int CntNyuukinIns = shimeshorichuuDao.Insert(seisanDetailEntity);

            LogUtility.DebugMethodEnd(dto, shimeNo);
        }

        #endregion

        #region 締め処理中テーブル削除

        /// <summary>
        /// 締め処理中テーブル削除
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        /// <param name="shimeNo">締め実行番号</param>
        [Transaction]
        internal void DeleteTShimeShoriChuu(SeikyuShimeShoriDto dto, SqlInt64 shimeNo)
        {
            LogUtility.DebugMethodStart(dto, shimeNo);

            T_SHIME_SHORI_CHUU shimeshorichuuEntity = new T_SHIME_SHORI_CHUU();
            // WHOカラム設定共通ロジック呼び出し用
            var dataBind_shimeshorichuuEntityAdd = new DataBinderLogic<T_SHIME_SHORI_CHUU>(shimeshorichuuEntity);

            //締実行番号
            shimeshorichuuEntity.SHIME_JIKKOU_NO = shimeNo;
            //取引先CD
            shimeshorichuuEntity.TORIHIKISAKI_CD = dto.SHIHARAI_CD;

            DataTable simeshorichuu = shimeShoriDao.SelectShimeShoriChuuEntity(shimeshorichuuEntity);
            if (simeshorichuu.Rows.Count > 0)
            {
                shimeshorichuuEntity.TIME_STAMP = (byte[])simeshorichuu.Rows[0]["TIME_STAMP"];

                using (Transaction tran = new Transaction())
                {
                    int CntNyuukinIns = shimeshorichuuDao.Delete(shimeshorichuuEntity);
                    tran.Commit();
                }
            }

            LogUtility.DebugMethodEnd(dto, shimeNo);
        }

        #endregion

        #region 締め処理実行履歴取引先テーブルに登録

        /// <summary>
        /// 締め処理実行履歴取引先テーブルに登録
        /// </summary>
        /// <param name="dto">画面からのパラメータ</param>
        /// <param name="shimeNo">締め実行番号</param>
        [Transaction]
        private void InsertTShimeShoriRirekiTorihikisaki(SeikyuShimeShoriDto dto, SqlInt64 shimeNo)
        {
            LogUtility.DebugMethodStart(dto, shimeNo);

            T_SHIME_JIKKOU_RIREKI_TORIHIKISAKI shimeJikkouRirekiTorihikisakiEntity = new T_SHIME_JIKKOU_RIREKI_TORIHIKISAKI();
            // WHOカラム設定共通ロジック呼び出し用
            var dataBind_shimeJikkouRirekiTorihikisakiEntityAdd = new DataBinderLogic<T_SHIME_JIKKOU_RIREKI_TORIHIKISAKI>(shimeJikkouRirekiTorihikisakiEntity);

            //締実行番号
            shimeJikkouRirekiTorihikisakiEntity.SHIME_JIKKOU_NO = shimeNo;
            //取引先CD
            shimeJikkouRirekiTorihikisakiEntity.TORIHIKISAKI_CD = dto.SHIHARAI_CD;

            // WHOカラム設定
            dataBind_shimeJikkouRirekiTorihikisakiEntityAdd.SetSystemProperty(shimeJikkouRirekiTorihikisakiEntity, false);

            using (Transaction tran = new Transaction())
            {
                int CntNyuukinIns = shimeJikkouRirekiTorihikiDao.Insert(shimeJikkouRirekiTorihikisakiEntity);
                tran.Commit();
            }

            LogUtility.DebugMethodEnd(dto, shimeNo);
        }

        #endregion

        #region 請求関連テーブル削除

        /// <summary>
        /// 請求関連テーブル削除
        /// </summary>
        [Transaction]
        private void DeleteSeikyuData()
        {
            LogUtility.DebugMethodStart();

            try
            {
                T_SEISAN_DENPYOU seisanDenPyouEntity = new T_SEISAN_DENPYOU();
                T_SEISAN_DENPYOU_KAGAMI seisanDenPyouKagamiEntity = new T_SEISAN_DENPYOU_KAGAMI();
                T_SEISAN_DETAIL seisanDetailEntity = new T_SEISAN_DETAIL();

                using (Transaction tran = new Transaction())
                {
                    foreach (SqlInt64 bango in this.seisanBangoList)
                    {
                        //請求番号を設定
                        seisanDenPyouEntity.SEISAN_NUMBER = bango;
                        seisanDenPyouKagamiEntity.SEISAN_NUMBER = bango;
                        seisanDetailEntity.SEISAN_NUMBER = bango;

                        //更新データ取得
                        int CntNyuukinIns1 = seikyuuDenpyouDao.Delete(seisanDenPyouEntity);
                        int CntNyuukinIns2 = seikyuuDenpyouKagamiDao.Delete(seisanDenPyouKagamiEntity);
                        int CntNyuukinIns3 = seikyuuDetailDao.Delete(seisanDetailEntity);

                    }

                    //再締精算番号を設定
                    foreach (SqlInt64 seisanSaishimeiBango in this.seisanSaishimeBangoList)
                    {
                        shimeShoriDao.UpdateSeisanSaishimeiDeleteData(seisanSaishimeiBango.Value, false);
                    }

                    tran.Commit();

                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    MessageBoxShowLogic MsgBox = new MessageBoxShowLogic();
                    MsgBox.MessageBoxShow("E080");
                }
                else
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 請求締め日取得

        /// <summary>
        /// 請求締め日取得
        /// </summary>
        /// <param name="from">請求締め日(From)</param>
        /// <param name="to">請求締め日(To)</param>
        /// <returns>請求締め日</returns>
        private string GetSeikyuuSimebi(string from, string to)
        {
            LogUtility.DebugMethodStart(from, to);

            if ((!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to)))
            {
                LogUtility.DebugMethodEnd(from, to);
                return from;
            }
            else
            {
                LogUtility.DebugMethodEnd(from, to);
                return "";
            }
        }

        #endregion

        #region 伝種採番テーブルから取得

        /// <summary>
        /// 伝種採番テーブルから取得
        /// </summary>
        /// <returns>伝種区分を元に取得した番号</returns>
        internal SqlInt64 GetDensyuSaibanNo(string densyuKbn)
        {
            LogUtility.DebugMethodStart();

            DBAccessor clscmn = new DBAccessor();

            LogUtility.DebugMethodEnd();
            //伝種採番テーブルから引数を元に番号を取得
            return clscmn.createDenshuNumber(SqlInt16.Parse(densyuKbn));
        }

        #endregion

        #endregion

        #region データの存在チェック

        /// <summary>
        /// データの存在チェック（適格請求書用）
        /// </summary>
        /// <returns>チェック結果　あり:True なし:False</returns>
        private bool ChkResultData_invoice()
        {
            LogUtility.DebugMethodStart();

            //適格請求書・合算請求書の場合対象データが存在しない場合、該当の取引先の処理を抜ける
            if ((this.uriageUkeireResult == null || this.uriageUkeireResult.Rows.Count == 0) &&
                (this.uriageShukkaResult == null || this.uriageShukkaResult.Rows.Count == 0) &&
                (this.uriageurShResult == null || this.uriageurShResult.Rows.Count == 0) &&
                (this.shukinResult == null || this.shukinResult.Rows.Count == 0))
            {
                LogUtility.DebugMethodEnd();
                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        /// <summary>
        /// 請求書鏡（適格請求書用）項目の税率毎の税抜金額、税額を変数にストック
        /// </summary>
        /// <param name="ZeiRatecount">税カウント</param>
        /// <param name="shohizeirate">税率</param>
        /// <param name="uriageTotal">税抜金額</param>
        /// <param name="torihikicd">取引先CD</param>
        private void Kazei_Save_invoice(int ZeiRatecount, decimal shohizeirate, decimal uriageTotal, string torihikicd)
        {
            //精算毎税計算
            switch (ZeiRatecount)
            {
                case 1:
                    konkaiKazeiKbn_1 = 1;
                    konkaiKazeiZeiRate_1 = shohizeirate;
                    konkaiKazeiZeinukigaku_1 = uriageTotal;
                    konkaiKazeiZeigaku_1 = CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    break;
                case 2:
                    konkaiKazeiKbn_2 = 1;
                    konkaiKazeiZeiRate_2 = shohizeirate;
                    konkaiKazeiZeinukigaku_2 = uriageTotal;
                    konkaiKazeiZeigaku_2 = CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    break;
                case 3:
                    konkaiKazeiKbn_3 = 1;
                    konkaiKazeiZeiRate_3 = shohizeirate;
                    konkaiKazeiZeinukigaku_3 = uriageTotal;
                    konkaiKazeiZeigaku_3 = CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    break;
                case 4:
                    konkaiKazeiKbn_4 = 1;
                    konkaiKazeiZeiRate_4 = shohizeirate;
                    konkaiKazeiZeinukigaku_4 = uriageTotal;
                    konkaiKazeiZeigaku_4 = CreateSeikyuuTaxSoto(uriageTotal, decimal.Parse(BlankToZero(shohizeirate.ToString())), torihikicd);
                    break;
                default:
                    break;
            }

        }
        #region 支払データ算出(請求)_適格請求書用

        /// <summary>
        /// 支払データ算出(請求)_適格請求書用
        /// >>税率毎に税抜金額を合算し、税計算を行う
        /// </summary>
        private void CreatetSeikyuuShiharaiData_invoice()
        {
            LogUtility.DebugMethodStart();

            //精算毎税の計算用項目の初期化
            int konkaiKazeiKbn = 0;
            decimal uriageTotal = 0;
            decimal shohizeirate = 0;
            int count = 0;
            int ZeiRatecount = 0;
            string torihikicd = "";

            // 精算毎税の計算
            DataView dv = new DataView(tougouuriageData);
            DataTable table = tougouuriageData.Clone();
            dv.Sort = "SHIHARAI_SHOUHIZEI_RATE,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
            foreach (DataRowView drv in dv)
            {
                table.ImportRow(drv.Row);
            }

            //精算毎税以外の計算
            for (int i = 0; i < table.Rows.Count; i++)
            {
                CreatetUriageData(table.Rows[i]);
            }

            if (table.Rows.Count != 0)
            {
                //保存用消費税率の初期化
                shohizeirate = 0;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (count == 0)
                    {
                        shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                        ZeiRatecount = 1;
                    }

                    //精算毎外税の計算
                    //税率毎、税抜金額ストック→一括課税
                    if (!(shohizeirate.Equals(decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))))
                    {
                        if (konkaiKazeiKbn == 1)
                        {
                            //精算毎税計算
                            Kazei_Save_invoice(ZeiRatecount, shohizeirate, uriageTotal, table.Rows[i]["TORIHIKISAKI_CD"].ToString());

                            ZeiRatecount = ZeiRatecount + 1;
                            uriageTotal = 0;
                            konkaiKazeiKbn = 0;
                        }
                    }

                    //非課税ストック（金額計算に影響しない）
                    if (((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_EXEMPTION.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString())
                        && (String.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())))
                        || (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_EXEMPTION.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //税区分：非課税且つ、品名毎税なし
                        //品名税区分：非課税
                        konkaiHikazeigaku = konkaiHikazeigaku
                                        + decimal.Parse(table.Rows[i]["KINGAKU"].ToString())
                                        + decimal.Parse(table.Rows[i]["HINMEI_KINGAKU"].ToString());
                        konkaiHikazeiKbn = 1;
                    }
                    else if (string.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() == table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()) &&
                            (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()))
                        {
                            //精算毎外税
                            uriageTotal = uriageTotal
                                        + decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()));

                            konkaiKazeiKbn = 1;
                        }
                        else if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() == table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()) &&
                            (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()))
                        {
                            //明細毎内税
                            uriageTotal = uriageTotal
                                        + decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()))
                                        - decimal.Parse(BlankToZero(table.Rows[i]["TAX_UCHI"].ToString()));
                            konkaiKazeiKbn = 1;
                        }
                    }
                    else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())
                    {
                        //品名内税
                        uriageTotal = uriageTotal
                                    + decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_KINGAKU"].ToString()))
                                    - decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_TAX_UCHI"].ToString()));
                        konkaiKazeiKbn = 1;
                    }

                    //消費税率の保存
                    shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                    torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                    //カウントアップ
                    count++;
                }
            }

            //精算毎の消費税額算出
            if (konkaiKazeiKbn == 1)
            {
                Kazei_Save_invoice(ZeiRatecount, shohizeirate, uriageTotal, torihikicd);
            }

            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
            //精算毎外税
            konkaiSeisotozeigaku = konkaiKazeiZeigaku_1 + konkaiKazeiZeigaku_2 + +konkaiKazeiZeigaku_3 + konkaiKazeiZeigaku_4;
            //今回明内税額
            konkaiMeiUtizeigaku = 0;


            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 支払データ算出(業者)_適格請求書用

        /// <summary>
        /// 支払データ算出(業者)_適格請求書用
        /// >>税率毎に税抜金額を合算し、税計算を行う
        /// </summary>
        private void CreatetGyoushaShiharaiData_invoice(string gyoushaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd);

            //精算毎税の計算用項目の初期化
            int konkaiKazeiKbn = 0;
            decimal uriageTotal = 0;
            decimal shohizeirate = 0;
            int count = 0;
            int ZeiRatecount = 0;
            string torihikicd = "";

            // 精算毎税の計算
            DataView dv = new DataView(tougouuriageData);
            DataTable table = tougouuriageData.Clone();
            dv.Sort = "SHIHARAI_SHOUHIZEI_RATE,GYOUSHA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";
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

            if (table.Rows.Count != 0)
            {
                //保存用消費税率の初期化
                shohizeirate = 0;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd)
                    {
                        if (count == 0)
                        {
                            shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                            ZeiRatecount = 1;
                        }

                        //精算毎外税の計算
                        //税率毎、税抜金額ストック→一括課税
                        if (!(shohizeirate.Equals(decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))))
                        {
                            if (konkaiKazeiKbn == 1)
                            {
                                //精算毎税計算
                                Kazei_Save_invoice(ZeiRatecount, shohizeirate, uriageTotal, table.Rows[i]["TORIHIKISAKI_CD"].ToString());

                                ZeiRatecount = ZeiRatecount + 1;
                                uriageTotal = 0;
                                konkaiKazeiKbn = 0;
                            }
                        }
                        //非課税ストック（金額計算に影響しない）
                        if (((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_EXEMPTION.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString())
                            && (String.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())))
                            || (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_EXEMPTION.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            //税区分：非課税且つ、品名毎税なし
                            //品名税区分：非課税
                            konkaiHikazeigaku = konkaiHikazeigaku
                                            + decimal.Parse(table.Rows[i]["KINGAKU"].ToString())
                                            + decimal.Parse(table.Rows[i]["HINMEI_KINGAKU"].ToString());
                            konkaiHikazeiKbn = 1;
                        }
                        else if (string.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() == table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                 (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()))
                            {
                                //精算毎外税
                                uriageTotal = uriageTotal
                                            + decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()));
                                konkaiKazeiKbn = 1;
                            }
                            else if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() == table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()))
                            {
                                //明細毎内税
                                uriageTotal = uriageTotal
                                            + decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()))
                                            - decimal.Parse(BlankToZero(table.Rows[i]["TAX_UCHI"].ToString()));
                                konkaiKazeiKbn = 1;
                            }
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())
                        {
                            //品名内税
                            uriageTotal = uriageTotal
                                        + decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_KINGAKU"].ToString()))
                                        - decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_TAX_UCHI"].ToString()));
                            konkaiKazeiKbn = 1;
                        }

                        //消費税率の保存
                        shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                        torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();

                        //カウントアップ
                        count++;
                    }
                }

                //精算毎税計算
                if (konkaiKazeiKbn == 1)
                {
                    Kazei_Save_invoice(ZeiRatecount, shohizeirate, uriageTotal, torihikicd);
                }

            }
            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
            //精算毎外税
            konkaiSeisotozeigaku = konkaiKazeiZeigaku_1 + konkaiKazeiZeigaku_2 + konkaiKazeiZeigaku_3 + konkaiKazeiZeigaku_4;
            //今回明内税額
            konkaiMeiUtizeigaku = 0;
            LogUtility.DebugMethodEnd(gyoushaCd);
        }

        #endregion

        #region 支払データ算出(現場)_適格請求書用

        /// <summary>
        /// 支払データ算出(現場)_適格請求書用
        /// >>税率毎に税抜金額を合算し、税計算を行う
        /// </summary>
        private void CreatetGenbaShiharaiData_invoice(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            //精算毎税の計算用項目の初期化
            int konkaiKazeiKbn = 0;
            decimal uriageTotal = 0;
            decimal shohizeirate = 0;
            int count = 0;
            int ZeiRatecount = 0;
            string torihikicd = "";

            // 精算毎税の計算
            DataView dv = new DataView(tougouuriageData);
            DataTable table = tougouuriageData.Clone();
            dv.Sort = "SHIHARAI_SHOUHIZEI_RATE,GYOUSHA_CD,GENBA_CD,SHIHARAI_ZEI_KEISAN_KBN_CD,SHIHARAI_ZEI_KBN_CD,SYSTEM_ID,DENPYOUSHURUI,ROW_NO";

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

            if (table.Rows.Count != 0)
            {
                //保存用消費税率の初期化
                shohizeirate = 0;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["GYOUSHA_CD"].ToString() == gyoushaCd && table.Rows[i]["GENBA_CD"].ToString() == genbaCd)
                    {
                        if (count == 0)
                        {
                            shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                            ZeiRatecount = 1;
                        }

                        //精算毎外税の計算
                        //税率毎、税抜金額ストック→一括課税
                        if (!(shohizeirate.Equals(decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))))
                        {
                            if (konkaiKazeiKbn == 1)
                            {
                                //精算毎税計算
                                Kazei_Save_invoice(ZeiRatecount, shohizeirate, uriageTotal, table.Rows[i]["TORIHIKISAKI_CD"].ToString());

                                ZeiRatecount = ZeiRatecount + 1;
                                uriageTotal = 0;
                                konkaiKazeiKbn = 0;
                            }
                        }

                        //非課税ストック（金額計算に影響しない）
                        if (((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_EXEMPTION.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString())
                            && (String.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())))
                            || (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_EXEMPTION.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            //税区分：非課税且つ、品名毎税なし
                            //品名税区分：非課税
                            konkaiHikazeigaku = konkaiHikazeigaku
                                            + decimal.Parse(table.Rows[i]["KINGAKU"].ToString())
                                            + decimal.Parse(table.Rows[i]["HINMEI_KINGAKU"].ToString());
                            konkaiHikazeiKbn = 1;
                        }
                        else if (string.IsNullOrEmpty(table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() == table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()))
                            {
                                //精算毎外税
                                uriageTotal = uriageTotal
                                            + decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()));
                                konkaiKazeiKbn = 1;
                            }
                            else if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() == table.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()) &&
                                (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString()))
                            {
                                //明細毎内税
                                uriageTotal = uriageTotal
                                            + decimal.Parse(BlankToZero(table.Rows[i]["KINGAKU"].ToString()))
                                            - decimal.Parse(BlankToZero(table.Rows[i]["TAX_UCHI"].ToString()));
                                konkaiKazeiKbn = 1;
                            }
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_UCHI.ToString() == table.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())
                        {
                            //品名外税
                            uriageTotal = uriageTotal
                                        + decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_KINGAKU"].ToString()))
                                        - decimal.Parse(BlankToZero(table.Rows[i]["HINMEI_TAX_UCHI"].ToString()));
                            konkaiKazeiKbn = 1;
                        }

                        //消費税率の保存
                        shohizeirate = decimal.Parse(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                        torihikicd = table.Rows[i]["TORIHIKISAKI_CD"].ToString();
                        //カウントアップ
                        count++;
                    }
                }

                //精算毎税計算
                if (konkaiKazeiKbn == 1)
                {
                    Kazei_Save_invoice(ZeiRatecount, shohizeirate, uriageTotal, torihikicd);
                }
            }

            //今回支払額
            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);
            //精算毎外税
            konkaiSeisotozeigaku = konkaiKazeiZeigaku_1 + konkaiKazeiZeigaku_2 + konkaiKazeiZeigaku_3 + konkaiKazeiZeigaku_4;
            //今回明内税額
            konkaiMeiUtizeigaku = 0;
            LogUtility.DebugMethodEnd(gyoushaCd, genbaCd);
        }

        #endregion

        #region デフォルトで作成されるコード

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}