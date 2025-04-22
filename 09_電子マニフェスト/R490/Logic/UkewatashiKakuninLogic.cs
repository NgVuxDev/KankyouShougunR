using System;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Logic;
using Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Dao;
using r_framework.Utility;
using Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Dto;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Collections.Generic;
using Report;

namespace Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Logic
{
    /// <summary>
    /// 受渡確認票
    /// </summary>
    public class UkewatashiKakuninHyouLogic
    {
        #region フィールド
        /// <summary>
        /// レポートデータ
        /// </summary>
        ReportInfoR490[] report_r490;

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 印刷ポップアップを表すクラス・コントロール
        /// </summary>
        CommonChouhyouPopup.App.FormReport formReport = null;

        /// <summary>
        /// 受渡確認票DAO
        /// </summary>
        private ManifestInfoDaoCls ManifestInfoDao;

        /// <summary>
        /// 1次マニフェスト情報フラグ
        /// データ存在フラグ
        /// false :データが存在しない
        /// true  :データが存在する
        /// </summary>
        private bool IsFirstManifestDataExistsFlg = true;

        /// <summary>
        /// 最終処分終了日・事業場情報フラグ
        /// データ存在フラグ
        /// false :データが存在しない
        /// true  :データが存在する
        /// </summary>
        private bool IsLastSbnJouDataExistsFlg = true;

        /// <summary>
        /// 最終処分事業場（予定）情報フラグ
        /// データ存在フラグ
        /// false :データが存在しない
        /// true  :データが存在する
        /// </summary>
        private bool IsLastSbnPlanDataExistsFlg = true;
        #endregion

        #region 受渡確認票プリンタ
        /// <summary>
        /// 受渡確認票プリンタ
        /// </summary>
        /// <param name="kanriId"></param>
        /// <returns>
        /// false:データが存在しない場合、メッセージを表示する
        /// true :データが存在する
        /// </returns>
        public bool UkewatashiKakuninHyouPrint(string kanriId)
        {
            LogUtility.DebugMethodStart(kanriId);

            try
            {
                this.ManifestInfoDao = DaoInitUtility.GetComponent<ManifestInfoDaoCls>();

                msgLogic = new MessageBoxShowLogic();

                int dataCnt = 0;

                //印刷データの作成
                DataTable dtPrint = this.GetPrintData(kanriId);

                DataTable dt = new DataTable();

                DataColumn dc = new DataColumn();
            
                dc = new DataColumn();
                dc.ColumnName = "Item";
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                if (dtPrint == null || dtPrint.Rows.Count == 0 )
                {
                    //検索結果が存在しませんでした
                    msgLogic.MessageBoxShow("E044");
                    LogUtility.DebugMethodEnd(kanriId);
                    return false;
                }

                dataCnt = dtPrint.Rows.Count;
                this.report_r490 = new ReportInfoR490[dataCnt];

                StringBuilder sbInfo;

                for (int i = 0; i < dataCnt; i++)
                {
                    dt.Clear();
                    DataRow dr = dt.NewRow();
                    //①－１
                    sbInfo = new StringBuilder();
                    sbInfo.Append("\"1-1");
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["MANIFEST_ID"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["REGIST_STATUS"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HIKIWATASHI_DATE"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HIKIWATASHI_TAN_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["RENRAKU_ID1"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["RENRAKU_ID2"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["RENRAKU_ID3"]);
                    sbInfo.Append("\"");
                
                    dr[0] = sbInfo.ToString();
                    dt.Rows.Add(dr);
                    //①－２
                    dr = dt.NewRow();

                    sbInfo = new StringBuilder();
                    sbInfo.Append("\"1-2");
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HST_SHA_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HST_SHA_POST"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HST_SHA_ADDRESS"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HST_SHA_TEL"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HST_SHA_EDI_MEMBER_ID"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HST_JOU_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HST_JOU_POST_NO"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HST_JOU_ADDRESS"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HST_JOU_TEL"]);
                    sbInfo.Append("\"");

                    dr[0] = sbInfo.ToString();
                    dt.Rows.Add(dr);

                    //①－３
                    dr = dt.NewRow();

                    sbInfo = new StringBuilder();
                    sbInfo.Append("\"1-3");
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_CODE"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_SHURUI"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_BUNRUI"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["YUUGAI_CODE1"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["YUUGAI_NAME1"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["YUUGAI_CODE2"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["YUUGAI_NAME2"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["YUUGAI_CODE3"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["YUUGAI_NAME3"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SONOTA_CNT"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_SUU"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_UNIT_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["NISUGATA_SUU"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["NISUGATA_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_KAKUTEI_SUU"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_KAKUTEI_UNIT_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_KAKUTEI_UNIT_NM"]);
                    sbInfo.Append("\"");

                    dr[0] = sbInfo.ToString();
                    dt.Rows.Add(dr);
                   
                    //1次マニフェスト情報が存在する場合
                    if (this.IsFirstManifestDataExistsFlg)
                    {
                        //①－４
                        dr = dt.NewRow();

                        sbInfo = new StringBuilder();
                        sbInfo.Append("\"1-4");
                        sbInfo.Append("\",\"");
                        sbInfo.Append(dtPrint.Rows[i]["FIRST_MANIFEST"]);
                        sbInfo.Append("\"");

                        dr[0] = sbInfo.ToString();
                        dt.Rows.Add(dr);

                        //①－９
                        dr = dt.NewRow();

                        sbInfo = new StringBuilder();
                        sbInfo.Append("\"1-9");
                        sbInfo.Append("\",\"");
                        sbInfo.Append(dtPrint.Rows[i]["FIRST_MANIFEST_2"]);
                        sbInfo.Append("\"");

                        dr[0] = sbInfo.ToString();
                        dt.Rows.Add(dr);
                    }

                    //最終処分事業場（予定）情報が存在する場合
                    if (this.IsLastSbnPlanDataExistsFlg)
                    {
                        //①－５
                        dr = dt.NewRow();

                        sbInfo = new StringBuilder();
                        sbInfo.Append("\"1-5");
                        sbInfo.Append("\",\"");
                        sbInfo.Append(dtPrint.Rows[i]["LAST_SBN_JOU_YOTEI"]);
                        sbInfo.Append("\"");

                        dr[0] = sbInfo.ToString();
                        dt.Rows.Add(dr);
                    }

                    //①－６
                    dr = dt.NewRow();

                    sbInfo = new StringBuilder();
                    sbInfo.Append("\"1-6");
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_ROUTE_NM"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_SHA_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_SHA_POST"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_SHA_ADDRESS"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_SHA_TEL"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_SHA_EDI_MEMBER_ID"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_SHA_KYOKA_ID"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["BIKOU"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPNSAKI_JOU_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPNSAKI_JOU_POST"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPNSAKI_JOU_ADDRESS"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPNSAKI_JOU_TEL"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_WAY_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["CAR_NO"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_SUU"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_UNIT_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["YUUKA_SUU"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["YUUKA_UNIT_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_TAN_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["UPN_END_DATE"]);
                    sbInfo.Append("\"");

                    dr[0] = sbInfo.ToString();
                    dt.Rows.Add(dr);
                    //①－７
                    dr = dt.NewRow();

                    sbInfo = new StringBuilder();
                    sbInfo.Append("\"1-7");
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_SHA_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_SHA_POST"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_SHA_ADDRESS"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_SHA_TEL"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_SHA_MEMBER_ID"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_SHA_KYOKA_ID"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_REP_BIKOU"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["MAX_UPNSAKI_JOU_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["MAX_UPNSAKI_JOU_POST"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["MAX_UPNSAKI_JOU_ADDRESS"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["MAX_UPNSAKI_JOU_TEL"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_WAY_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_ENDREP_KBN_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_END_DATE"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["HAIKI_IN_DATE"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["SBN_TAN_NAME"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["RECEPT_SUU"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["RECEPT_UNIT_NAME"]);
                    sbInfo.Append("\"");

                    dr[0] = sbInfo.ToString();
                    dt.Rows.Add(dr);

                    //①－８
                    dr = dt.NewRow();

                    sbInfo = new StringBuilder();
                    sbInfo.Append("\"1-8");
                    sbInfo.Append("\",\"");
                    if (this.IsLastSbnJouDataExistsFlg)
                        sbInfo.Append(dtPrint.Rows[i]["LAST_SBN_JOU_JISSEKI"]);
                    else
                        sbInfo.Append(string.Empty);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["LAST_SBN_END_DATE"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["BIKOU1"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["BIKOU2"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["BIKOU3"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["BIKOU4"]);
                    sbInfo.Append("\",\"");
                    sbInfo.Append(dtPrint.Rows[i]["BIKOU5"]);
                    sbInfo.Append("\"");

                    dr[0] = sbInfo.ToString();
                    dt.Rows.Add(dr);

                    this.report_r490[i] = new ReportInfoR490();
                    this.report_r490[i].R490_Report(dt);
                }

                //印刷ポップアップ
                formReport = new CommonChouhyouPopup.App.FormReport(this.report_r490);
                formReport.Text = Const.ConstCls.REPORT_TITLE;
                formReport.Show();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd(kanriId);

            return true;

        }
        #endregion

        #region 印刷データを作成
        /// <summary>
        /// 印刷データを作成
        /// </summary>
        /// <param name="kanriId"></param>
        /// <returns></returns>
        private DataTable GetPrintData(string kanriId)
        {
            LogUtility.DebugMethodStart(kanriId);

            DataTable dtPrint = new DataTable();
            DataTable dtLastSbnPlan = new DataTable();
            DataTable dtLastSbnJou = new DataTable();
            DataTable dtFirstManifest = new DataTable();

            try
            {
                UkewatashiKakuninDTOCls UkewatashiKakuninDto = new UkewatashiKakuninDTOCls();
                UkewatashiKakuninDto.kanriId = kanriId;
            
                //マニフェスト目次情報を取得
                dtPrint = this.ManifestInfoDao.GetManifestTocInfo(UkewatashiKakuninDto);
                dtLastSbnPlan = this.ManifestInfoDao.GetLastSbnPlanInfo(UkewatashiKakuninDto);
                dtLastSbnJou = this.ManifestInfoDao.GetLastSbnJouInfo(UkewatashiKakuninDto);
                dtFirstManifest = this.ManifestInfoDao.GetFirstManifestInfo(UkewatashiKakuninDto);

                //データが存在する場合
                if (dtPrint != null && dtPrint.Rows.Count > 0)
                {
                    //最終処分事業場（予定）情報の取得
                    this.SetLastSbnPlanInfo(ref dtPrint, dtLastSbnPlan);

                    //最終処分終了日・事業場情報の取得
                    this.SetLastSbnJouInfo(ref dtPrint, dtLastSbnJou);

                    //1次マニフェスト情報の取得
                    this.SetFirstManifestInfo(ref dtPrint, dtFirstManifest);

                }

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd(kanriId);

            return dtPrint;
        }

        /// <summary>
        /// 最終処分事業場（予定）情報の設定する
        /// </summary>
        /// <param name="dtPrint"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SetLastSbnPlanInfo(ref DataTable dtPrint,DataTable dt)
        {
            LogUtility.DebugMethodStart(dtPrint, dt);

            //最終処分場所（予定）
            string lastSbnJouYotei = string.Empty;

            //最終処分場所
            string lastSbnJou = string.Empty;

            StringBuilder sblastSbnJouYotei = new StringBuilder();

            try
            {
                //データが存在する場合
                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        lastSbnJouYotei = dr["LAST_SBN_JOU_YOTEI"].ToString();

                        if (!string.IsNullOrEmpty(lastSbnJouYotei))
                        {
                            sblastSbnJouYotei.Append(lastSbnJouYotei);
                            sblastSbnJouYotei.Append("\",");
                        }
                    }
                }
                else
                {
                    //データが存在しない場合
                    this.IsLastSbnPlanDataExistsFlg = false;
                    LogUtility.DebugMethodEnd(dtPrint, dt);
                    return;
                }

                lastSbnJou = this.subString(sblastSbnJouYotei.ToString(), Const.ConstCls.LAST_SBN_PLAN_LENGTH);

                dtPrint.Columns["LAST_SBN_JOU_YOTEI"].ReadOnly = false;
                dtPrint.Columns["LAST_SBN_JOU_YOTEI"].MaxLength = Const.ConstCls.LAST_SBN_PLAN_LENGTH;

                foreach (DataRow dr in dtPrint.Rows)
                {
                    if (Const.ConstCls.LAST_SBN_JOU_TYPE.Equals(dr["LAST_SBN_JOU_KISAI_FLAG"].ToString()))
                    {
                        dr["LAST_SBN_JOU_YOTEI"] = lastSbnJou;
                    }
                    else if (Const.ConstCls.LAST_SBN_JOU_KISAI_TYPE.Equals(dr["LAST_SBN_JOU_KISAI_FLAG"].ToString()))
                    {
                        dr["LAST_SBN_JOU_YOTEI"] = Const.ConstCls.LAST_SBN_JOU_KISAI;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd(dtPrint, dt);
        }

        /// <summary>
        /// 最終処分終了日・事業場情報の設定する
        /// </summary>
        /// <param name="dtPrint"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SetLastSbnJouInfo(ref DataTable dtPrint, DataTable dt)
        {
            LogUtility.DebugMethodStart(dtPrint, dt);

            //最終処分場所（実績）
            string lastSbnJouJisseki = string.Empty;

            //最終処分場所
            string lastSbnJou = string.Empty;

            StringBuilder sblastSbnJouYotei = new StringBuilder();

            try
            {
                //データが存在する場合
                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        lastSbnJouJisseki = dr["LAST_SBN_JOU_JISSEKI"].ToString();

                        if (!string.IsNullOrEmpty(lastSbnJouJisseki))
                        {
                            sblastSbnJouYotei.Append(lastSbnJouJisseki);
                            sblastSbnJouYotei.Append(",");
                        }
                    }
                }
                else
                {
                    //データが存在しない場合
                    this.IsLastSbnJouDataExistsFlg = false;
                    LogUtility.DebugMethodEnd(dtPrint, dt);
                    return;
                }

                lastSbnJou = this.subString(sblastSbnJouYotei.ToString(), Const.ConstCls.LAST_SBN_JOU_LENGTH);

                dtPrint.Columns["LAST_SBN_JOU_JISSEKI"].ReadOnly = false;
                dtPrint.Columns["LAST_SBN_JOU_JISSEKI"].MaxLength = Const.ConstCls.LAST_SBN_JOU_LENGTH;

                foreach (DataRow dr in dtPrint.Rows)
                {
                    dr["LAST_SBN_JOU_JISSEKI"] = lastSbnJou;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd(dtPrint, dt);
        }

        /// <summary>
        /// 1次マニフェスト情報の設定する
        /// </summary>
        /// <param name="dtPrint"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SetFirstManifestInfo(ref DataTable dtPrint, DataTable dt)
        {
            LogUtility.DebugMethodStart(dtPrint, dt);

            //電子/紙区分
            string mediaType = string.Empty;

            //1次交付番号／マニフェスト番号
            string firstManifestId = string.Empty;

            //中間処理産業廃棄物
            string firstManifest = string.Empty;
            string firstManifest_2 = string.Empty;

            StringBuilder sbFirstManifest= new StringBuilder();
            StringBuilder sbFirstManifest_D = new StringBuilder();
            StringBuilder sbFirstManifest_K = new StringBuilder();

            try
            {
                //データが存在する場合
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        firstManifestId = dr["FIRST_MANIFEST_ID"].ToString();

                        if (dr["MEDIA_TYPE"].ToString().Equals("紙"))
                        {
                            if (!string.IsNullOrEmpty(firstManifestId))
                            {
                                sbFirstManifest_K.Append(firstManifestId);
                            }
                            else
                            {
                                sbFirstManifest_K.Append(string.Empty);
                            }
                            sbFirstManifest_K.Append(",");
                        }
                        else if (dr["MEDIA_TYPE"].ToString().Equals("電子"))
                        {
                            if (!string.IsNullOrEmpty(firstManifestId))
                            {
                                sbFirstManifest_D.Append(firstManifestId);
                            }
                            else
                            {
                                sbFirstManifest_D.Append(string.Empty);
                            }
                            sbFirstManifest_D.Append(",");
                        }
                    }
                }
                else
                {
                    //データが存在しない場合
                    this.IsFirstManifestDataExistsFlg = false;
                    LogUtility.DebugMethodEnd(dtPrint, dt);
                    return;
                }

                if (sbFirstManifest_D.Length == 0 && sbFirstManifest_K.Length == 0)
                {
                    //データが存在しない場合
                    this.IsFirstManifestDataExistsFlg = false;
                    LogUtility.DebugMethodEnd(dtPrint, dt);
                    return;
                }

                dtPrint.Columns["FIRST_MANIFEST"].ReadOnly = false;
                dtPrint.Columns["FIRST_MANIFEST"].MaxLength = Const.ConstCls.FIRST_MANIFEST_LENGTH / 2;
                dtPrint.Columns["FIRST_MANIFEST_2"].ReadOnly = false;
                dtPrint.Columns["FIRST_MANIFEST_2"].MaxLength = Const.ConstCls.FIRST_MANIFEST_LENGTH / 2;

                if (sbFirstManifest_D.ToString() != "" && sbFirstManifest_K.ToString() != "")
                {
                    sbFirstManifest.Append("電子\",\"");
                    sbFirstManifest.Append(sbFirstManifest_D);
                    firstManifest = this.subString(sbFirstManifest.ToString(), Const.ConstCls.FIRST_MANIFEST_LENGTH / 2);

                    sbFirstManifest.Clear();
                    sbFirstManifest.Append("紙\",\"");
                    sbFirstManifest.Append(sbFirstManifest_K);
                    firstManifest_2 = this.subString(sbFirstManifest.ToString(), Const.ConstCls.FIRST_MANIFEST_LENGTH / 2);
                }
                else
                {
                    if (sbFirstManifest_D.ToString() != "")
                    {
                        sbFirstManifest.Append("電子\",\"");
                        sbFirstManifest.Append(sbFirstManifest_D);
                        firstManifest = this.subString(sbFirstManifest.ToString(), Const.ConstCls.FIRST_MANIFEST_LENGTH / 2);
                    }
                    else if (sbFirstManifest_K.ToString() != "")
                    {
                        sbFirstManifest.Append("紙\",\"");
                        sbFirstManifest.Append(sbFirstManifest_K);
                        firstManifest = this.subString(sbFirstManifest.ToString(), Const.ConstCls.FIRST_MANIFEST_LENGTH / 2);
                    }
                    sbFirstManifest.Clear();
                    sbFirstManifest.Append("\",\",");
                    firstManifest_2 = this.subString(sbFirstManifest.ToString(), Const.ConstCls.FIRST_MANIFEST_LENGTH / 2);
                }

                foreach (DataRow dr in dtPrint.Rows)
                {
                    dr["FIRST_MANIFEST"] = firstManifest;
                    dr["FIRST_MANIFEST_2"] = firstManifest_2;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd(dtPrint, dt);

        }
        #endregion

        #region インスタンスから部分文字列を取得
        /// <summary>
        /// インスタンスから部分文字列を取得します。
        /// この部分文字列は、指定した文字位置から開始し、指定した文字数の文字列です。
        /// </summary>
        /// <param name="strVal">文字列</param>
        /// <param name="length">文字数</param>
        /// <returns></returns>
        private string subString(string strVal ,int length)
        {
            LogUtility.DebugMethodStart(strVal, length);

            string retVal = string.Empty;
            try
            {
                if (strVal.Length > length)
                {
                    retVal = strVal.Substring(0, length);
                }
                else
                {
                    //最終文字列が特定の文字の場合は削除を行う
                    if (strVal.Length > 1) retVal = strVal.Substring(0, strVal.Length - 1);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd(strVal, length);

            return retVal;
        }
        #endregion
    }
}