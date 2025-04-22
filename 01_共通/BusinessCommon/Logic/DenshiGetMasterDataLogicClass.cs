using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;
using System.Windows.Forms;
using Seasar.Quill.Attrs;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using r_framework.Dto;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class DenshiMasterDataLogic
    {
        #region フィールド
        private DenshiMasterDataSearchDao MasterDao;

        public static string befGyoushaCd = string.Empty;
        #endregion フィールド

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenshiMasterDataLogic()
        {
            LogUtility.DebugMethodStart();

            this.MasterDao = DaoInitUtility.GetComponent<DenshiMasterDataSearchDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion コンストラクタ

        #region 各種マスタ情報取得メソッド
        /// <summary>
        /// 電子廃棄物種類検索
        /// </summary>
        /// <param name="Dspd"></param>
        /// <returns></returns>
        public DataTable GetDenshiHaikiShuruiData(DenshiSearchParameterDtoCls Dspd)
        {
            LogUtility.DebugMethodStart(Dspd);

            DataTable dt = MasterDao.GetDenshiHaikiShuruiForEntity(Dspd);

            LogUtility.DebugMethodEnd(Dspd);

            return dt;
        }

        /// <summary>
        /// 電子廃棄物名称コードと名称検索
        /// </summary>
        /// <param name="Dspd"></param>
        /// <returns></returns>
        public DataTable GetDenshiHaikiNameData(DenshiSearchParameterDtoCls Dspd)
        {
            LogUtility.DebugMethodStart(Dspd);

            DataTable dt = MasterDao.GetDenshiHaikiNameForEntity(Dspd);

            LogUtility.DebugMethodEnd(Dspd);

            return dt;
        }

        /// <summary>
        /// 電子業者検索
        /// </summary>
        /// <param name="Dspd"></param>
        /// <returns></returns>
        public DataTable GetDenshiGyoushaData(DenshiSearchParameterDtoCls Dspd)
        {
            LogUtility.DebugMethodStart(Dspd);

            DataTable dt = MasterDao.GetDenshiGyoushaForEntity(Dspd);

            LogUtility.DebugMethodEnd(Dspd);

            return dt;
        }

        /// <summary>
        /// 電子事業場検索
        /// </summary>
        /// <param name="Dspd"></param>
        /// <returns></returns>
        public DataTable GetDenshiGenbaData(DenshiSearchParameterDtoCls Dspd)
        {
            LogUtility.DebugMethodStart(Dspd);

            DataTable dt = MasterDao.GetDenshiGenbaForEntity(Dspd);

            LogUtility.DebugMethodEnd(Dspd);

            return dt;
        }

        /// <summary>
        /// 有害物質検索
        /// </summary>
        /// <param name="Dspd"></param>
        /// <returns></returns>
        public DataTable GetYougaibutujituData(DenshiSearchParameterDtoCls Dspd)
        {
            LogUtility.DebugMethodStart(Dspd);

            DataTable dt = MasterDao.GetYougaibutujituForEntity(Dspd);

            LogUtility.DebugMethodEnd(Dspd);

            return dt;
        }

        /// <summary>
        /// 電子担当者検索
        /// </summary>
        /// <param name="Dspd"></param>
        /// <returns></returns>
        public DataTable GetTantoushaData(DenshiSearchParameterDtoCls Dspd)
        {
            LogUtility.DebugMethodStart(Dspd);

            DataTable dt = MasterDao.GetTantoushaForEntity(Dspd);

            LogUtility.DebugMethodEnd(Dspd);

            return dt;
        }

        /// <summary>
        /// 共通マスタのコードより名称取得メッソド
        /// </summary>
        /// <param name="targetTable">目的テーブル</param>
        /// <param name="CD">コード</param>
        /// <param name="GyoushaCd">車輌検索場合業者CD</param>
        /// <returns></returns>
        public string GetCodeNameFromMasterInfo(WINDOW_ID targetTable, string CD, string GyoushaCd = null, bool isNotNeedDeleteFlg = false)
        {
            string Name = string.Empty;
            //MasterDao
            //単位
            if (targetTable == WINDOW_ID.M_UNIT)
            {
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                dto.UNIT_CD = CD;
                DataTable dt = MasterDao.GetDenshiUnitInfo(dto);
                if (dt.Rows.Count == 0) return null;
                if (dt.Rows.Count == 1) Name = dt.Rows[0]["NAME"].ToString();
            }
            //荷姿
            if (targetTable == WINDOW_ID.M_NISUGATA)
            {
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                dto.NISUGATA_CD = CD;
                DataTable dt = MasterDao.GetDenshiNISUGATAInfo(dto);
                if (dt.Rows.Count == 0) return null;
                if (dt.Rows.Count == 1) Name = dt.Rows[0]["NAME"].ToString();
            }
            //処分方法
            if (targetTable == WINDOW_ID.M_SHOBUN_HOUHOU)
            {
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                dto.SHOBUN_HOUHOU_CD = CD;
                dto.ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg;
                DataTable dt = MasterDao.GetDenshiSBN_HOUHOUInfo(dto);
                if (dt.Rows.Count == 0) return null;
                if (dt.Rows.Count == 1) Name = dt.Rows[0]["NAME"].ToString();
            }
            //運搬方法
            if (targetTable == WINDOW_ID.M_UNPAN_HOUHOU)
            {
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                dto.UNPAN_HOUHOU_CD = CD;
                dto.ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg;
                DataTable dt = MasterDao.GetDenshiUNPAN_HOUHOUInfo(dto);
                if (dt.Rows.Count == 0) return null;
                if (dt.Rows.Count == 1) Name = dt.Rows[0]["NAME"].ToString();
            }
            //車輌
            if (targetTable == WINDOW_ID.M_SHARYOU && GyoushaCd != null)
            {
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                dto.GYOUSHA_CD = GyoushaCd;
                dto.SHARYOU_CD = CD;
                DataTable dt = MasterDao.GetDenshiCARInfo(dto);
                if (dt.Rows.Count == 0) return null;
                if (dt.Rows.Count == 1) Name = dt.Rows[0]["NAME"].ToString();
            }

            return Name;
        }

        /// <summary>
        /// 車輌検索
        /// </summary>
        /// <param name="Dspd"></param>
        /// <returns></returns>
        public DataTable GetSharyouData(DenshiSearchParameterDtoCls Dspd)
        {
            LogUtility.DebugMethodStart(Dspd);
            DataTable dt = MasterDao.GetDenshiSharyouInfo(Dspd);

            LogUtility.DebugMethodEnd(Dspd);

            return dt;
        }
        /// <summary>
        /// 交付番号検索チェック用
        /// </summary>
        /// <param name="Dspd"></param>
        /// <returns></returns>
        public DataTable SearchDenshiManifestNo(DenshiSearchParameterDtoCls Dspd)
        {
            LogUtility.DebugMethodStart(Dspd);
            DataTable dt = MasterDao.SearchDenshiManifestNo(Dspd);

            LogUtility.DebugMethodEnd(Dspd);

            return dt;
        }

        /// <summary>
        /// 換算値取得
        /// </summary>
        /// <param name="Dspd"></param>
        /// <returns></returns>
        public DataTable GetDenmaniKansanData(DenshiSearchParameterDtoCls Dspd)
        {
            DenshiSearchParameterDtoCls searchDto = new DenshiSearchParameterDtoCls();

            DataTable tbl1 = MasterDao.GetDenshiKansanInfo(Dspd);
            if (tbl1.Rows.Count >= 1)
            {
                return tbl1;
            }

            // 廃棄物種類
            if (string.IsNullOrEmpty(Dspd.HAIKI_SHURUI_CD))
            {
                searchDto.HAIKI_SHURUI_CD = "";
            }
            else
            {
                searchDto.HAIKI_SHURUI_CD = Dspd.HAIKI_SHURUI_CD;
            }

            // 単位
            searchDto.UNIT_CD = Dspd.UNIT_CD;

            // 荷姿以外で検索
            if (string.IsNullOrEmpty(Dspd.HAIKI_NAME_CD))
            {
                searchDto.HAIKI_NAME_CD = "";
            }
            else
            {
                searchDto.HAIKI_NAME_CD = Dspd.HAIKI_NAME_CD;
            }
            searchDto.NISUGATA_CD = "";

            DataTable tbl2 = MasterDao.GetDenshiKansanInfo(searchDto);
            if (tbl2.Rows.Count >= 1)
            {
                return tbl2;
            }

            // 廃棄物の名称以外で検索
            searchDto.HAIKI_NAME_CD = "";
            if (string.IsNullOrEmpty(Dspd.NISUGATA_CD))
            {
                searchDto.NISUGATA_CD = "";
            }
            else
            {
                searchDto.NISUGATA_CD = Dspd.NISUGATA_CD;
            }

            DataTable tbl3 = MasterDao.GetDenshiKansanInfo(searchDto);
            if (tbl3.Rows.Count >= 1)
            {
                return tbl3;
            }

            // 廃棄物種類と単位だけで検索
            searchDto.HAIKI_NAME_CD = "";
            searchDto.NISUGATA_CD = "";

            DataTable tbl4 = MasterDao.GetDenshiKansanInfo(searchDto);
            if (tbl4.Rows.Count >= 1)
            {
                return tbl4;
            }

            // 単位だけで検索
            searchDto.HAIKI_SHURUI_CD = "";
            searchDto.HAIKI_NAME_CD = "";
            searchDto.NISUGATA_CD = "";

            DataTable tbl5 = MasterDao.GetDenshiKansanInfo(searchDto);
            if (tbl5.Rows.Count >= 1)
            {
                return tbl5;
            }

            return tbl1;
        }

        #endregion 各種マスタ情報取得メソッド



        #region 紙マニ/電マニ　共通　ポップアップ設定自動実装ロジック群

        /// <summary>
        /// 業者の区分
        /// </summary>
        [Flags]
        public enum JIGYOUSYA_KBN
        {
            /// <summary>自社</summary>
            JISHA = 1,

            // 20151023 BUNN #12040 STR
            /// <summary>排出事業者(電子の排出）/荷積業者区分</summary>
            HAISHUTSU_NIZUMI_GYOUSHA = 1 << 1,
            /// <summary>運搬受託者(電子の運搬）/運搬会社区分</summary>
            UNPAN_JUTAKUSHA_KAISHA = 1 << 2,
            /// <summary>処分受託者(電子の処分)/荷降業者区分</summary>
            SHOBUN_NIOROSHI_GYOUSHA = 1 << 3,
            /// <summary>マニフェスト返送先</summary>
            MANI_HENSOUSAKI = 1 << 4,
            /// <summary>運搬会社</summary>
            //UNPAN_KAISHA = 1 << 5,
            /// <summary>荷降業者</summary>
            //NIOROSHI_GHOUSHA = 1 << 6,
            // 20151023 BUNN #12040 END

            /// <summary>諸口</summary>
            SHOKUCHI = 1 << 7
        }


        //電子の事業場区分：1.排出事業者　2.運搬業者　3.処分業者								
        /// <summary>
        /// 現場の区分
        /// </summary>
        [Flags]
        public enum JIGYOUJOU_KBN
        {
            /// <summary>無</summary>            
            NONE = 0,
            /// <summary>自社</summary>
            JISHA = 1,

            // 20151023 BUNN #12040 STR
            /// <summary>排出事業場/荷積現場区分</summary>
            HAISHUTSU_NIZUMI_GENBA = 1 << 1,
            /// <summary>積替保管</summary>
            TSUMIKAEHOKAN = 1 << 2,
            /// <summary>処分事業場/荷降現場区分</summary>
            SHOBUN_NIOROSHI_GENBA = 1 << 3,
            /// <summary>最終処分場</summary>
            SAISHUU_SHOBUNJOU = 1 << 4,
            /// <summary>マニフェスト返送先</summary>
            MANI_HENSOUSAKI = 1 << 5,
            /// <summary>荷降現場</summary>
            //NIOROSHI_GENBA = 1 << 6
            // 20151023 BUNN #12040 END
        }
        /// <summary>
        /// 紙か電子か
        /// </summary>
        public enum MANI_KBN
        {
            /// <summary>紙</summary>
            KAMI,
            /// <summary>電子</summary>
            DENSHI
        }

        /// <summary>
        /// 業者＋現場の設定を行う（Load等のWindowInitで呼ばれる想定）
        /// </summary>
        /// <param name="GYOUSHA_CD"></param>
        /// <param name="GYOUSYA_NAME"></param>
        /// <param name="GYOUSYA_BTN"></param>
        /// <param name="GENBA_CD"></param>
        /// <param name="GENBA_NAME"></param>
        /// <param name="GENBA_BTN"></param>
        /// <param name="mKbn"></param>
        /// <param name="jKbn"></param>
        /// <param name="needEDI_PASSWORD">電子専用 trueだと EDI_PASSWORD <> '' の条件追加</param>
        /// <param name="needGENBA_CD">電子専用 trueだと GENBA_CD <> ''  の条件追加(紐付必須)</param>
        /// <param name="gKbn"></param>
        /// <param name="useDenshiJigyoujou">true:電子事業場を使う、false:将軍の事業場を使う</param>
        /// <param name="bindGyoushaValidating">業者のvalidatingを自動で紐づける場合true</param>
        /// <param name="bindGenbaValidating">現場のvalidatingを自動で紐づける場合true</param>
        public static void SetPopupSetting(
            CustomTextBox GYOUSHA_CD,
            CustomTextBox GYOUSYA_NAME,
            CustomPopupOpenButton GYOUSYA_BTN,
            CustomTextBox GENBA_CD,
            CustomTextBox GENBA_NAME,
            CustomPopupOpenButton GENBA_BTN,
            MANI_KBN mKbn,
            JIGYOUSYA_KBN jKbn,
            bool needEDI_PASSWORD,
            bool needGENBA_CD,
            JIGYOUJOU_KBN gKbn,
            bool useDenshiJigyoujou,
            bool bindGyoushaValidating,
            bool bindGenbaValidating
            )
        {

            //業者
            GYOUSHA_CD.ZeroPaddengFlag = true;
            GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            GYOUSHA_CD.popupWindowSetting.Clear();
            GYOUSHA_CD.PopupSearchSendParams.Clear();


            //現場
            if (GENBA_CD != null)
            {
                GENBA_CD.ZeroPaddengFlag = true;
                GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
                GENBA_CD.popupWindowSetting.Clear();
                GENBA_CD.PopupSearchSendParams.Clear();
            }


            switch (mKbn)
            {
                case MANI_KBN.KAMI:
                    //業者
                    GYOUSHA_CD.CharactersNumber = 6;
                    GYOUSHA_CD.MaxLength = 6;
                    GYOUSHA_CD.Tag = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                    GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;

                    //現場
                    if (GENBA_CD != null)
                    {
                        GENBA_CD.CharactersNumber = 6;
                        GENBA_CD.MaxLength = 6;
                        GENBA_CD.Tag = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                        GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
                    }


                    //コントロールと、ポップアップの列との対応
                    var gyoushaCtls = new[]
                    {
                        new { Key = GYOUSHA_CD, value = "GYOUSHA_CD" },
                        new { Key = GYOUSYA_NAME, value = "GYOUSHA_NAME_RYAKU" }
                    };

                    // 業者変更があれば現場クリアをするように変更して各画面でやります
                    //if (GENBA_CD != null) //業者ポップアップ選択時、現場クリアする
                    //{
                    //    gyoushaCtls = new[]
                    //    {
                    //        new { Key = GYOUSHA_CD, value = "GYOUSHA_CD" },
                    //        new { Key = GYOUSYA_NAME, value = "GYOUSHA_NAME_RYAKU" },
                    //        new { Key = GENBA_CD, value = "EMPTY" },
                    //        new { Key = GENBA_NAME, value = "EMPTY" }
                    //    };
                    //}

                    //カンマ区切り
                    GYOUSHA_CD.PopupGetMasterField = string.Join(",", gyoushaCtls.Where(x => x.Key != null).Select(x => x.value));
                    GYOUSHA_CD.PopupSetFormField = string.Join(",", gyoushaCtls.Where(x => x.Key != null).Select(x => x.Key.Name));

                    if (GENBA_CD != null)
                    {
                            //コントロールと、ポップアップの列との対応
                            var genbaCtls = new[]
                            {
                                new { Key = GYOUSHA_CD, value = "GYOUSHA_CD" },
                                new { Key = GYOUSYA_NAME, value = "GYOUSHA_NAME_RYAKU" },
                                new { Key = GENBA_CD, value = "GENBA_CD" },
                                new { Key = GENBA_NAME, value = "GENBA_NAME_RYAKU" }
                            };

                            GENBA_CD.PopupGetMasterField = string.Join(",", genbaCtls.Where(x => x.Key != null).Select(x => x.value));
                            GENBA_CD.PopupSetFormField = string.Join(",", genbaCtls.Where(x => x.Key != null).Select(x => x.Key.Name));
                    }



                    var searchSendParamParent = new r_framework.Dto.PopupSearchSendParamDto();
                    searchSendParamParent.And_Or = CONDITION_OPERATOR.AND;
                    searchSendParamParent.SubCondition = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();

                    //業者
                    foreach (JIGYOUSYA_KBN kbn in Enum.GetValues(typeof(JIGYOUSYA_KBN)))
                    {
                        if ((kbn & jKbn) == kbn)
                        {

                            if (kbn == JIGYOUSYA_KBN.JISHA)
                            {
                                //自社だけはAND条件
                                var searchSendParam = new r_framework.Dto.PopupSearchSendParamDto();
                                var sub = new r_framework.Dto.PopupSearchSendParamDto();
                                searchSendParam.And_Or = CONDITION_OPERATOR.AND;

                                sub.KeyName = "M_GYOUSHA." + kbn.ToString() + "_KBN";
                                sub.Value = "1";
                                sub.And_Or = CONDITION_OPERATOR.AND;

                                searchSendParam.SubCondition = new System.Collections.ObjectModel.Collection<PopupSearchSendParamDto>();
                                searchSendParam.SubCondition.Add(sub);

                                GYOUSHA_CD.PopupSearchSendParams.Add(searchSendParam);
                                //現場 //業者の同じものセット
                                if (GENBA_CD != null)
                                {
                                    GENBA_CD.PopupSearchSendParams.Add(searchSendParam);
                                }
                            }
                            else
                            {
                                //自社以外はORで追加
                                var searchSendParam = new r_framework.Dto.PopupSearchSendParamDto();
                                searchSendParam.KeyName = "M_GYOUSHA." + kbn.ToString() + "_KBN";
                                if (searchSendParamParent.SubCondition.Count == 0)
                                {
                                    searchSendParam.And_Or = CONDITION_OPERATOR.AND; //1つ目だけはANDにする必要あり
                                }
                                else
                                {
                                    searchSendParam.And_Or = CONDITION_OPERATOR.OR;
                                }

                                searchSendParam.Value = "1";

                                searchSendParamParent.SubCondition.Add(searchSendParam);
                            }
                        }
                    }

                    if (searchSendParamParent.SubCondition.Count != 0)
                    {
                        GYOUSHA_CD.PopupSearchSendParams.Add(searchSendParamParent);

                        //現場 //業者の同じものセット
                        if (GENBA_CD != null)
                        {
                            GENBA_CD.PopupSearchSendParams.Add(searchSendParamParent);
                        }
                    }

                    // 20151023 BUNN #12040 STR
                    // 業者区分マニを追加
                    if (GYOUSHA_CD.PopupSearchSendParams.Count > 0)
                    {
                        //自社だけはAND条件
                        var searchSendParam = new r_framework.Dto.PopupSearchSendParamDto();
                        var sub = new r_framework.Dto.PopupSearchSendParamDto();
                        searchSendParam.And_Or = CONDITION_OPERATOR.AND;

                        sub.KeyName = "M_GYOUSHA.GYOUSHAKBN_MANI";
                        sub.Value = "1";
                        sub.And_Or = CONDITION_OPERATOR.AND;

                        searchSendParam.SubCondition = new System.Collections.ObjectModel.Collection<PopupSearchSendParamDto>();
                        searchSendParam.SubCondition.Add(sub);

                        GYOUSHA_CD.PopupSearchSendParams.Add(searchSendParam);
                        //現場 //業者の同じものセット
                        if (GENBA_CD != null)
                        {
                            GENBA_CD.PopupSearchSendParams.Add(searchSendParam);
                        }
                    }
                    // 20151023 BUNN #12040 END

                    if (GENBA_CD != null)
                    {

                        var searchSendParamParentGenba = new r_framework.Dto.PopupSearchSendParamDto();
                        searchSendParamParentGenba.And_Or = CONDITION_OPERATOR.AND;
                        searchSendParamParentGenba.SubCondition = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();


                        //業者連携(キー１つ目の連動設定)
                        var dto = new r_framework.Dto.PopupSearchSendParamDto();
                        dto.And_Or = CONDITION_OPERATOR.AND;
                        dto.KeyName = "M_GYOUSHA.GYOUSHA_CD";
                        dto.Control = GYOUSHA_CD.Name;
                        GENBA_CD.PopupSearchSendParams.Add(dto);


                        //現場
                        foreach (JIGYOUJOU_KBN kbn in Enum.GetValues(typeof(JIGYOUJOU_KBN)))
                        {
                            if ((kbn & gKbn) == kbn)
                            {
                                if (kbn == JIGYOUJOU_KBN.JISHA)
                                {

                                    //自社だけはAND条件
                                    var searchSendParam = new r_framework.Dto.PopupSearchSendParamDto();
                                    var sub = new r_framework.Dto.PopupSearchSendParamDto();
                                    searchSendParam.And_Or = CONDITION_OPERATOR.AND;

                                    sub.KeyName = "M_GENBA." + kbn.ToString() + "_KBN";
                                    sub.Value = "1";
                                    sub.And_Or = CONDITION_OPERATOR.AND;

                                    searchSendParam.SubCondition = new System.Collections.ObjectModel.Collection<PopupSearchSendParamDto>();
                                    searchSendParam.SubCondition.Add(sub);

                                    GENBA_CD.PopupSearchSendParams.Add(searchSendParam);
                                }
                                else if(kbn != JIGYOUJOU_KBN.NONE)
                                {
                                    var searchSendParam = new r_framework.Dto.PopupSearchSendParamDto();
                                    searchSendParam.KeyName = "M_GENBA." + kbn.ToString() + "_KBN";
                                    if (searchSendParamParentGenba.SubCondition.Count == 0)
                                    {
                                        searchSendParam.And_Or = CONDITION_OPERATOR.AND;
                                    }
                                    else
                                    {
                                        searchSendParam.And_Or = CONDITION_OPERATOR.OR;
                                    }
                                    searchSendParam.Value = "1";
                                    searchSendParamParentGenba.SubCondition.Add(searchSendParam);
                                }


                            }
                        }
                        if (searchSendParamParentGenba.SubCondition.Count != 0)
                        {
                            GENBA_CD.PopupSearchSendParams.Add(searchSendParamParentGenba);
                        }
                    }

                    break;


                //電子の場合
                case MANI_KBN.DENSHI:
                    //業者
                    GYOUSHA_CD.CharactersNumber = 7;
                    GYOUSHA_CD.MaxLength = 7;
                    GYOUSHA_CD.Tag = "7 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                    GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_JIGYOUSHA;


                    //コントロールと、ポップアップの列との対応
                    gyoushaCtls = new[]
                    {
                        new { Key = GYOUSHA_CD, value = "EDI_MEMBER_ID" },
                        new { Key = GYOUSYA_NAME, value = "JIGYOUSHA_NAME" }
                    };

                    // 業者変更があれば現場クリアをするように変更して各画面でやります
                    //if (GENBA_CD != null) //業者ポップアップ選択時、現場クリアする
                    //{
                    //    gyoushaCtls = new[]
                    //    {
                    //        new { Key = GYOUSHA_CD, value = "EDI_MEMBER_ID" },
                    //        new { Key = GYOUSYA_NAME, value = "JIGYOUSHA_NAME" },
                    //        new { Key = GENBA_CD, value = "EMPTY" },
                    //        new { Key = GENBA_NAME, value = "EMPTY" }
                    //    };
                    //}

                    //カンマ区切り
                    GYOUSHA_CD.PopupGetMasterField = string.Join(",", gyoushaCtls.Where(x => x.Key != null).Select(x => x.value));
                    GYOUSHA_CD.PopupSetFormField = string.Join(",", gyoushaCtls.Where(x => x.Key != null).Select(x => x.Key.Name));

                    if (GENBA_CD != null)
                    {

                        if (useDenshiJigyoujou)
                        {
                            //電子の現場を使う
                            GENBA_CD.CharactersNumber = 10;
                            GENBA_CD.MaxLength = 10;
                            GENBA_CD.Tag = "10 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                            GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_JIGYOUJOU;

                            //コントロールと、ポップアップの列との対応
                            var genbaCtls = new[]
                            {
                                new { Key = GYOUSHA_CD, value = "EDI_MEMBER_ID" },
                                new { Key = GYOUSYA_NAME, value = "JIGYOUSHA_NAME" },
                                new { Key = GENBA_CD, value = "JIGYOUJOU_CD" },
                                new { Key = GENBA_NAME, value = "JIGYOUJOU_NAME" }
                            };

                            GENBA_CD.PopupGetMasterField = string.Join(",", genbaCtls.Where(x => x.Key != null).Select(x => x.value));
                            GENBA_CD.PopupSetFormField = string.Join(",", genbaCtls.Where(x => x.Key != null).Select(x => x.Key.Name));

                            //業者連携(キー１つ目の連動設定)
                            var dto = new r_framework.Dto.PopupSearchSendParamDto();
                            dto.And_Or = CONDITION_OPERATOR.AND;
                            dto.KeyName = "M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID";
                            dto.Control = GYOUSHA_CD.Name;
                            GENBA_CD.PopupSearchSendParams.Add(dto);
                        }
                        else
                        {
                            //将軍の現場を使う
                            GENBA_CD.CharactersNumber = 6;
                            GENBA_CD.MaxLength = 6;
                            GENBA_CD.Tag = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                            GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;

                            //コントロールと、ポップアップの列との対応
                            var genbaCtls = new[]
                            {
                                new { Key = GYOUSHA_CD, value = "EDI_MEMBER_ID" },
                                new { Key = GYOUSYA_NAME, value = "JIGYOUSHA_NAME" },
                                new { Key = GENBA_CD, value = "GENBA_CD" },
                                new { Key = GENBA_NAME, value = "GENBA_NAME_RYAKU" }
                            };

                            GENBA_CD.PopupGetMasterField = string.Join(",", genbaCtls.Where(x => x.Key != null).Select(x => x.value));
                            GENBA_CD.PopupSetFormField = string.Join(",", genbaCtls.Where(x => x.Key != null).Select(x => x.Key.Name));

                            //業者連携(キー１つ目の連動設定)
                            var dto = new r_framework.Dto.PopupSearchSendParamDto();
                            dto.And_Or = CONDITION_OPERATOR.AND;
                            dto.KeyName = "M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID";
                            dto.Control = GYOUSHA_CD.Name;
                            GENBA_CD.PopupSearchSendParams.Add(dto);
                        }
                    }

                    //電子事業者条件-------------

                    searchSendParamParent = new r_framework.Dto.PopupSearchSendParamDto();
                    searchSendParamParent.And_Or = CONDITION_OPERATOR.AND;
                    searchSendParamParent.SubCondition = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();


                    var searchSendParamParentDGenba = new r_framework.Dto.PopupSearchSendParamDto();
                    searchSendParamParentDGenba.And_Or = CONDITION_OPERATOR.AND;
                    searchSendParamParentDGenba.SubCondition = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();

                    //排出
                    if ((JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA & jKbn) != 0)
                    {
                        var dto = new r_framework.Dto.PopupSearchSendParamDto();
                        if (searchSendParamParent.SubCondition.Count == 0)
                        {
                            dto.And_Or = CONDITION_OPERATOR.AND;
                        }
                        else
                        {
                            dto.And_Or = CONDITION_OPERATOR.OR;
                        }
                        dto.KeyName = "M_DENSHI_JIGYOUSHA.HST_KBN";
                        dto.Value = "1";
                        searchSendParamParent.SubCondition.Add(dto);


                        dto = new r_framework.Dto.PopupSearchSendParamDto();
                        if (searchSendParamParentDGenba.SubCondition.Count == 0)
                        {
                            dto.And_Or = CONDITION_OPERATOR.AND;
                        }
                        else
                        {
                            dto.And_Or = CONDITION_OPERATOR.OR;
                        }
                        dto.KeyName = "M_DENSHI_JIGYOUJOU.JIGYOUJOU_KBN";
                        dto.Value = "1"; //排出は1
                        searchSendParamParentDGenba.SubCondition.Add(dto);
                    }

                    //運搬
                    if ((JIGYOUSYA_KBN.UNPAN_JUTAKUSHA_KAISHA & jKbn) != 0)
                    {
                        var dto = new r_framework.Dto.PopupSearchSendParamDto();
                        if (searchSendParamParent.SubCondition.Count == 0)
                        {
                            dto.And_Or = CONDITION_OPERATOR.AND;
                        }
                        else
                        {
                            dto.And_Or = CONDITION_OPERATOR.OR;
                        }
                        dto.KeyName = "M_DENSHI_JIGYOUSHA.UPN_KBN";
                        dto.Value = "1";
                        searchSendParamParent.SubCondition.Add(dto);

                        dto = new r_framework.Dto.PopupSearchSendParamDto();
                        if (searchSendParamParentDGenba.SubCondition.Count == 0)
                        {
                            dto.And_Or = CONDITION_OPERATOR.AND;
                        }
                        else
                        {
                            dto.And_Or = CONDITION_OPERATOR.OR;
                        }
                        dto.KeyName = "M_DENSHI_JIGYOUJOU.JIGYOUJOU_KBN";
                        dto.Value = "2"; //運搬は2
                        searchSendParamParentDGenba.SubCondition.Add(dto);
                    }

                    //処分
                    if ((JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA & jKbn) != 0)
                    {
                        var dto = new r_framework.Dto.PopupSearchSendParamDto();
                        if (searchSendParamParent.SubCondition.Count == 0)
                        {
                            dto.And_Or = CONDITION_OPERATOR.AND;
                        }
                        else
                        {
                            dto.And_Or = CONDITION_OPERATOR.OR;
                        }
                        dto.KeyName = "M_DENSHI_JIGYOUSHA.SBN_KBN";
                        dto.Value = "1";
                        searchSendParamParent.SubCondition.Add(dto);

                        dto = new r_framework.Dto.PopupSearchSendParamDto();
                        if (searchSendParamParentDGenba.SubCondition.Count == 0)
                        {
                            dto.And_Or = CONDITION_OPERATOR.AND;
                        }
                        else
                        {
                            dto.And_Or = CONDITION_OPERATOR.OR;
                        }
                        dto.KeyName = "M_DENSHI_JIGYOUJOU.JIGYOUJOU_KBN";
                        dto.Value = "3"; //処分は3
                        searchSendParamParentDGenba.SubCondition.Add(dto);
                    }


                    if (searchSendParamParent.SubCondition.Count != 0)
                    {
                        GYOUSHA_CD.PopupSearchSendParams.Add(searchSendParamParent);

                        //現場 //業者の同じものセット
                        if (GENBA_CD != null)
                        {
                            GENBA_CD.PopupSearchSendParams.Add(searchSendParamParent);
                            GENBA_CD.PopupSearchSendParams.Add(searchSendParamParentDGenba);
                        }
                    }



                    if (needEDI_PASSWORD)
                    {

                        var dtowhere = new r_framework.Dto.JoinMethodDto();
                        dtowhere.Join = JOIN_METHOD.WHERE;

                        var serdto = new r_framework.Dto.SearchConditionsDto();
                        serdto.And_Or = CONDITION_OPERATOR.AND;
                        serdto.Condition = JUGGMENT_CONDITION.NOT_EQUALS;
                        serdto.LeftColumn = "M_DENSHI_JIGYOUSHA.EDI_PASSWORD";
                        serdto.Value = "'+'"; //インジェクション利用で無理やり空文字化
                        serdto.ValueColumnType = DB_TYPE.VARCHAR;
                        dtowhere.SearchCondition.Add(serdto);

                        GYOUSHA_CD.popupWindowSetting.Add(dtowhere);

                        if (GENBA_CD != null)
                        {
                            GENBA_CD.popupWindowSetting.Add(dtowhere);
                        }
                    }

                    if (needGENBA_CD)
                    {

                        var dtowhere = new r_framework.Dto.JoinMethodDto();
                        dtowhere.Join = JOIN_METHOD.WHERE;

                        var serdto = new r_framework.Dto.SearchConditionsDto();
                        serdto.And_Or = CONDITION_OPERATOR.AND;
                        serdto.Condition = JUGGMENT_CONDITION.NOT_EQUALS;
                        serdto.LeftColumn = "M_DENSHI_JIGYOUJOU.GENBA_CD";
                        serdto.Value = "'+'";
                        serdto.ValueColumnType = DB_TYPE.VARCHAR;
                        dtowhere.SearchCondition.Add(serdto);

                        if (GENBA_CD != null)
                        {
                            GENBA_CD.popupWindowSetting.Add(dtowhere);
                        }
                    }


                    if (!useDenshiJigyoujou)
                    {
                        var dtowhere = new r_framework.Dto.JoinMethodDto();
                        dtowhere.Join = JOIN_METHOD.WHERE;

                        var serdto = new r_framework.Dto.SearchConditionsDto();
                        serdto.And_Or = CONDITION_OPERATOR.AND;
                        serdto.Condition = JUGGMENT_CONDITION.NOT_EQUALS;
                        serdto.LeftColumn = "M_DENSHI_JIGYOUSHA.GYOUSHA_CD"; //業者と紐付されていること
                        serdto.Value = "'+'";
                        serdto.ValueColumnType = DB_TYPE.VARCHAR;
                        dtowhere.SearchCondition.Add(serdto);

                        if (GENBA_CD != null)
                        {
                            GENBA_CD.popupWindowSetting.Add(dtowhere);
                        }
                    }
                    //電子事業場条件は無い-----



                    break;
            }


            //validatingイベントを登録
            if (bindGyoushaValidating)
            {
                //業者の場合
                ManifestoLogic.ClearEvent(GYOUSHA_CD);
                GYOUSHA_CD.Enter +=
                    (sender, e) =>
                    {
                        GYOUSHA_CD_EnterBase(GYOUSHA_CD);
                    };

                GYOUSHA_CD.Validating +=
                    (sender, e) =>
                    {
                        if (GYOUSHA_CD_VlidatingBase(GYOUSHA_CD, GYOUSYA_NAME, GENBA_CD, GENBA_NAME, mKbn))
                        {
                            e.Cancel = true;
                        }
                    };

            }
            if (bindGenbaValidating && GENBA_CD != null)
            {
                //現場の場合
                ManifestoLogic.ClearEvent(GENBA_CD);
                GENBA_CD.Validating +=
                    (sender, e) =>
                    {
                        if (GENBA_CD_VlidatingBase(GYOUSHA_CD, GYOUSYA_NAME, GENBA_CD, GENBA_NAME, mKbn, useDenshiJigyoujou))
                        {
                            e.Cancel = true;
                        }
                    };

            }

            //ポップアップボタン（あるとき）
            if (GYOUSYA_BTN != null)
            {
                GYOUSYA_BTN.PopupSetFormField = GYOUSHA_CD.PopupSetFormField;
                GYOUSYA_BTN.PopupGetMasterField = GYOUSHA_CD.PopupGetMasterField;
                GYOUSYA_BTN.SetFormField = GYOUSHA_CD.SetFormField;
                GYOUSYA_BTN.GetCodeMasterField = GYOUSHA_CD.GetCodeMasterField;

                GYOUSYA_BTN.PopupWindowName = GYOUSHA_CD.PopupWindowName;
                GYOUSYA_BTN.PopupWindowId = GYOUSHA_CD.PopupWindowId;
                GYOUSYA_BTN.popupWindowSetting = GYOUSHA_CD.popupWindowSetting;
                GYOUSYA_BTN.PopupSearchSendParams = GYOUSHA_CD.PopupSearchSendParams;

                GYOUSYA_BTN.PopupAfterExecuteMethod = GYOUSHA_CD.PopupAfterExecuteMethod;
                GYOUSYA_BTN.PopupBeforeExecuteMethod = GYOUSHA_CD.PopupBeforeExecuteMethod;

            }
            if (GENBA_BTN != null)
            {
                GENBA_BTN.PopupSetFormField = GENBA_CD.PopupSetFormField;
                GENBA_BTN.PopupGetMasterField = GENBA_CD.PopupGetMasterField;
                GENBA_BTN.SetFormField = GENBA_CD.SetFormField;
                GENBA_BTN.GetCodeMasterField = GENBA_CD.GetCodeMasterField;

                GENBA_BTN.PopupWindowName = GENBA_CD.PopupWindowName;
                GENBA_BTN.PopupWindowId = GENBA_CD.PopupWindowId;
                GENBA_BTN.popupWindowSetting = GENBA_CD.popupWindowSetting;
                GENBA_BTN.PopupSearchSendParams = GENBA_CD.PopupSearchSendParams;

                GENBA_BTN.PopupAfterExecuteMethod = GENBA_CD.PopupAfterExecuteMethod;
                GENBA_BTN.PopupBeforeExecuteMethod = GENBA_CD.PopupBeforeExecuteMethod;
            }



        }

        /// <summary>
        /// popupWindowSettingの内容からJOIN句を作成します。
        /// </summary>
        static public void CreateJoinStr(System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto> popupWindowSetting, Control[] controls, out string joinStr, out string whereStr)
        {
            var join = new StringBuilder();
            var where = new StringBuilder();
            var isChecked = new List<string>();
            foreach (r_framework.Dto.JoinMethodDto joinData in popupWindowSetting)
            {
                if (joinData.Join != JOIN_METHOD.WHERE)
                {
                    if (!string.IsNullOrEmpty(joinData.LeftTable) && !string.IsNullOrEmpty(joinData.LeftKeyColumn) &&
                        !string.IsNullOrEmpty(joinData.RightTable) && !string.IsNullOrEmpty(joinData.RightKeyColumn))
                    {
                        join.Append(" " + JOIN_METHODExt.ToString(joinData.Join) + " ");
                        join.Append(joinData.LeftTable + " ON ");
                        join.Append(joinData.LeftTable + "." + joinData.LeftKeyColumn + " = ");
                        join.Append(joinData.RightTable + "." + joinData.RightKeyColumn + " ");
                    }
                }
                else if (joinData.Join == JOIN_METHOD.WHERE)
                {
                    var searchStr = new StringBuilder();
                    foreach (var searchData in joinData.SearchCondition)
                    {
                        //検索条件設定
                        if (string.IsNullOrEmpty(searchData.Value))
                        {
                            //value値がnullのため、テーブル同士のカラム結合を行う
                            if (searchStr.Length == 0)
                            {
                                searchStr.Append(" AND (");
                            }
                            else
                            {
                                searchStr.Append(" ");
                                searchStr.Append(searchData.And_Or.ToString());
                                searchStr.Append(" ");
                            }
                            searchStr.Append(joinData.LeftTable);
                            searchStr.Append(".");
                            searchStr.Append(searchData.LeftColumn);
                            var data = joinData.RightTable + "." + searchData.RightColumn;
                            searchStr.Append(searchData.Condition.ToConditionString(data));
                        }
                        else
                        {
                            // コントロールの値が有効な場合WHERE句を作成する
                            var data = SqlCreateUtility.CounterplanEscapeSequence(createValues(controls, searchData));

                            if (!string.IsNullOrEmpty(data))
                            {
                                if (searchStr.Length == 0)
                                {
                                    searchStr.Append(" AND (");
                                }
                                else
                                {
                                    searchStr.Append(" ");
                                    searchStr.Append(searchData.And_Or.ToString());
                                    searchStr.Append(" ");
                                }
                                searchStr.Append(joinData.LeftTable);
                                searchStr.Append(".");
                                searchStr.Append(searchData.LeftColumn);
                                searchStr.Append(searchData.Condition.ToConditionString(data));
                                searchStr.Append(" ");
                            }
                        }
                    }
                    if (searchStr.Length > 0)
                    {
                        // 閉じる
                        searchStr.Append(") ");
                    }
                    where.Append(searchStr);
                }
                // 有効レコードをチェックする
                if (joinData.IsCheckLeftTable == true && !isChecked.Contains(joinData.LeftTable))
                {
                    var type = Type.GetType("r_framework.Entity." + joinData.LeftTable);
                    if (type != null)
                    {
                        var pNames = type.GetProperties().Select(p => p.Name);
                        if (pNames.Contains("TEKIYOU_BEGIN") && pNames.Contains("TEKIYOU_END") && pNames.Contains("DELETE_FLG"))
                        {
                            where.Append(" AND ");
                            where.Append(String.Format("CONVERT(DATE, ISNULL({0}.TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL({0}.TEKIYOU_END, DATEADD(day,1,GETDATE()))) AND {0}.DELETE_FLG = 0", joinData.LeftTable));
                            where.Append(" ");
                        }
                    }
                    isChecked.Add(joinData.LeftTable);
                }
            }

            joinStr = join.ToString();
            whereStr = where.ToString();
        }

        /// <summary>
        /// 検索条件を作成する
        /// 対象のコントロールが見つけれた場合については、コントロールの値とする
        /// コントロールが見つからない場合は、Valuesの値を直接設定する
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static string createValues(object[] controls, SearchConditionsDto dto)
        {
            var field = ControlUtility.CreateFields(controls, dto.Value);

            if (field[0] != null)
            {
                var control = field[0] as ICustomControl;

                if (control != null)
                {
                    return dto.ValueColumnType.ToConvertString(control.GetResultText());
                }

            }
            return dto.ValueColumnType.ToConvertString(dto.Value.ToString());
        }



        /// <summary>
        /// PopupSearchSendParamDtoからWHERE句を作成します。(サブコンディション用)
        /// </summary>
        static public string CreateWhere(PopupSearchSendParamDto dto, Control[] controls, int depth)
        {

            string returnStr = string.Empty;

            if (dto == null)
            {
                return returnStr;
            }

            if (dto.SubCondition != null && dto.SubCondition.Count > 0)
            {
                //サブ

                returnStr += " " + dto.And_Or.ToString() + " ( ";

                int cnt = 0;
                foreach (var sub in dto.SubCondition)
                {
                    if (cnt != 0) //先頭以外は結合する
                    {
                        returnStr += " " + sub.And_Or.ToString() + " ";
                    }
                    returnStr += CreateWhere(sub, controls, depth + 1);
                    cnt++;
                }
                returnStr += " ) ";

            }
            if (dto.Control == null || string.IsNullOrEmpty(dto.Control))
            {
                if (dto.Value != null && !string.IsNullOrEmpty(dto.Value))
                {
                    if (dto.Value.Contains(","))
                    {
                        // IN結合の場合
                        // 使用側で"'"を意識しないで使わせたいので、FW側で"'"をつける
                        string[] valueList = dto.Value.Replace(" ", "").Split(',');
                        string values = "";
                        foreach (string tempValue in valueList)
                        {
                            if (string.IsNullOrEmpty(returnStr))
                            {
                                values = "'" + tempValue + "'";
                            }
                            else
                            {
                                values = values + ", '" + tempValue + "'";
                            }
                        }
                        values = "(" + values + ")";
                        returnStr += " ( " + dto.KeyName + " IN " + values + " ) ";
                    }
                    else
                    {
                        //統合で結合の場合
                        returnStr += " ( " + dto.KeyName + " = '" + dto.Value + "' ) ";
                    }
                }
            }
            else
            {
                var target = controls.Where(x => string.Equals(x.Name, dto.Control)).First();
                string controlText = PropertyUtility.GetTextOrValue(target);

                if (!string.IsNullOrEmpty(controlText)) //空の時は条件付けない
                {
                    // 複数同じ名前のコントロールは存在しないはず
                    returnStr += dto.And_Or.ToString() + " ( " + dto.KeyName + " = '" + controlText + "' ) ";
                }
            }

            return returnStr;
        }

        static private void GYOUSHA_CD_EnterBase(CustomTextBox GYOUSYA_CD)
        {
            //befGyoushaCd = GYOUSYA_CD.Text;
            befGyoushaCd = GYOUSYA_CD.prevText;
        }

        static private bool GYOUSHA_CD_VlidatingBase(CustomTextBox GYOUSYA_CD, CustomTextBox GYOUSYA_NAME, CustomTextBox GENBA_CD, CustomTextBox GENBA_NAME, MANI_KBN kbn)
        {
            //変更なしの場合何もしない
            //if (!GYOUSYA_CD.isChanged())
            //{
            //    return false;
            //}

            if (befGyoushaCd == GYOUSYA_CD.Text)
            {
                return false;
            }

            if (GENBA_CD != null)
            {
                GENBA_CD.Text = "";
                GENBA_NAME.Text = "";
            }

            //未入力時はクリア
            if (string.IsNullOrEmpty(GYOUSYA_CD.Text))
            {
                GYOUSYA_NAME.Text = "";
                return false;
            }

            //入力有の場合


            string gyousyaCd = "";
            string gyousyaName = "";
            string gyousyaMaster = "";
            string genbaCd = "";
            string genbaName = "";
            string genbaMaster = "";

            switch (kbn)
            {
                case MANI_KBN.DENSHI:
                    gyousyaMaster = "M_DENSHI_JIGYOUSHA";
                    gyousyaCd = "EDI_MEMBER_ID";
                    gyousyaName = "JIGYOUSHA_NAME";
                    genbaMaster = "M_DENSHI_JIGYOUJOU";
                    genbaCd = "JIGYOUJOU_CD";
                    genbaName = "JIGYOUJOU_NAME";
                    break;
                case MANI_KBN.KAMI:
                    gyousyaMaster = "M_GYOUSHA";
                    gyousyaCd = "GYOUSHA_CD";
                    gyousyaName = "GYOUSHA_NAME_RYAKU";
                    genbaMaster = "M_GENBA";
                    genbaCd = "GENBA_CD";
                    genbaName = "GENBA_NAME_RYAKU";
                    break;
            }

            string whereStr;
            string joinStr; //対応不完全

            //条件作成
            CreateJoinStr(GYOUSYA_CD.popupWindowSetting, new[] { GYOUSYA_CD }, out joinStr, out whereStr);

            //条件作成
            foreach (var p in GYOUSYA_CD.PopupSearchSendParams)
            {
                whereStr += CreateWhere(p, new[] { GYOUSYA_CD }, 0);
            }

            string sql = "SELECT * FROM " + gyousyaMaster + " " + joinStr + " WHERE 1=1 " + whereStr + " AND " + gyousyaMaster + "." + gyousyaCd + " = '" + GYOUSYA_CD.Text + "' ";
            //string sql = "SELECT * FROM " + gyousyaMaster + " " + joinStr + " WHERE " + whereStr + " AND " + gyousyaMaster + "." + gyousyaCd + " = '" + GYOUSYA_CD.Text + "' ";

            var dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
            var dt = dao.GetDateForStringSql(sql);

            if (dt.Rows.Count == 0)
            {
                //対象データ無
                GYOUSYA_NAME.Text = "";
                if (GENBA_CD != null)
                {
                    GENBA_CD.Text = "";
                    GENBA_NAME.Text = "";
                }
                GYOUSYA_CD.IsInputErrorOccured = true;
                GYOUSYA_CD.prevText = "";
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E020", GYOUSYA_CD.DisplayItemName);
                return true;
            }

            //データあり
            GYOUSYA_NAME.Text = dt.Rows[0][gyousyaName].ToString();

            //変更時、現場は消す
            if (GENBA_CD != null)
            {
                GENBA_CD.Text = "";
                GENBA_NAME.Text = "";
            }
            return false;
        }

        static private bool GENBA_CD_VlidatingBase(CustomTextBox GYOUSYA_CD, CustomTextBox GYOUSYA_NAME, CustomTextBox GENBA_CD, CustomTextBox GENBA_NAME, MANI_KBN kbn, bool useDenshiJigyoujou)
        {

            //変更なしの場合何もしない
            if (!GENBA_CD.isChanged())
            {
                return false;
            }


            //未入力時はクリア
            if (string.IsNullOrEmpty(GENBA_CD.Text))
            {
                GENBA_NAME.Text = "";
                return false;
            }

            if (string.IsNullOrEmpty(GYOUSYA_CD.Text))
            {
                //対象データ無
                GENBA_CD.IsInputErrorOccured = true;
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E051", GYOUSYA_CD.DisplayItemName);
                GENBA_CD.Text = string.Empty;
                return true;
            }

            //入力有の場合
            string gyousyaCd = "";
            string gyousyaName = "";
            string gyousyaMaster = "";
            string genbaCd = "";
            string genbaName = "";
            string genbaMaster = "";

            switch (kbn)
            {
                case MANI_KBN.DENSHI:
                    gyousyaMaster = "M_DENSHI_JIGYOUSHA";
                    gyousyaCd = "EDI_MEMBER_ID";
                    gyousyaName = "JIGYOUSHA_NAME";
                    genbaMaster = "M_DENSHI_JIGYOUJOU";

                    if (useDenshiJigyoujou)
                    {
                        genbaCd = "JIGYOUJOU_CD";
                        genbaName = "JIGYOUJOU_NAME";
                    }
                    else
                    {
                        genbaCd = "GENBA_CD";
                        genbaName = "GENBA_NAME_RYAKU";
                    }
                    break;
                case MANI_KBN.KAMI:
                    gyousyaMaster = "M_GYOUSHA";
                    gyousyaCd = "GYOUSHA_CD";
                    gyousyaName = "GYOUSHA_NAME_RYAKU";
                    genbaMaster = "M_GENBA";
                    genbaCd = "GENBA_CD";
                    genbaName = "GENBA_NAME_RYAKU";
                    break;
            }

            string whereStr;
            string joinStr; 

            //条件作成
            CreateJoinStr(GENBA_CD.popupWindowSetting, new[] { GYOUSYA_CD, GENBA_CD }, out joinStr, out whereStr);

            //条件作成
            foreach (var p in GENBA_CD.PopupSearchSendParams)
            {
                whereStr += CreateWhere(p, new[] { GYOUSYA_CD, GENBA_CD }, 0);
            }
            string sql;

            if (kbn== MANI_KBN.DENSHI && !useDenshiJigyoujou)
            {
                //電子で業者マスタを使っている場合 
                sql = " SELECT M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID ,M_DENSHI_JIGYOUSHA.JIGYOUSHA_NAME , M_GENBA.GENBA_CD , M_GENBA.GENBA_NAME_RYAKU ";
                sql += " FROM M_GENBA LEFT JOIN M_DENSHI_JIGYOUJOU ON M_GENBA.GYOUSHA_CD=M_DENSHI_JIGYOUJOU.GYOUSHA_CD AND M_GENBA.GENBA_CD=M_DENSHI_JIGYOUJOU.GENBA_CD ";
                sql += " LEFT JOIN M_DENSHI_JIGYOUSHA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID = M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID";
                sql += joinStr + " WHERE 1=1 " + whereStr + " AND M_GENBA.GENBA_CD = '" + GENBA_CD.Text + "' ";
            }
            else
            {
                sql = "SELECT * FROM " + genbaMaster + " LEFT JOIN " + gyousyaMaster + " ON " + gyousyaMaster + "." + gyousyaCd + " = " + genbaMaster + "." + gyousyaCd + " " + joinStr + " WHERE 1=1 " + whereStr + " AND " + genbaMaster + "." + genbaCd + " = '" + GENBA_CD.Text + "' ";
            }
            

            var dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
            DataTable dt =null;
            try
            {
                dt= dao.GetDateForStringSql(sql);
            }
            catch(System.Data.ConstraintException ex)
            {
                //例外無視：タイムスタンプを複数列同時にselectすると例外が発生するため。 ※将軍1に対して電子が複数紐づく可能性があるため、電子業者を絞れない場合もあり
            }
            
            if (dt.Rows.Count == 0)
            {
                //対象データ無
                GENBA_NAME.Text = "";
                GENBA_CD.IsInputErrorOccured = true;
                GENBA_CD.prevText = "";
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E020", GENBA_CD.DisplayItemName);
                return true;
            }
            else if (dt.Rows.Count > 1) //2件以上ある場合
            {
                GENBA_NAME.Text = "";
                GYOUSYA_CD.IsInputErrorOccured = true;
                GENBA_CD.IsInputErrorOccured = true;
                GENBA_CD.prevText = "";
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E034", GYOUSYA_CD.DisplayItemName);
                return true;
            }

            //データあり
            GYOUSYA_CD.Text = dt.Rows[0][gyousyaCd].ToString();
            GYOUSYA_NAME.Text = dt.Rows[0][gyousyaName].ToString();
            GENBA_CD.Text = dt.Rows[0][genbaCd].ToString();
            GENBA_NAME.Text = dt.Rows[0][genbaName].ToString();

            return false;
        }

        # endregion


    }
}