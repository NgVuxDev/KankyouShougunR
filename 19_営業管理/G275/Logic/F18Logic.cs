// $Id: LogicCls.cs 8436 2013-11-27 01:47:57Z sys_dev_27 $
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.APP;
using Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Const;
using r_framework.CustomControl;
using Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Dto;
using Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Dao;
using Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Entity;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class F18Logic : IBuisinessLogic
    {

        #region fields
        /// <summary>
        /// M_CORP_INFO検索用Dao
        /// </summary>
        IM_CORP_INFODao iM_CORP_INFODao;

        /// <summary>
        /// M_SYS_INFODao検索用Dao
        /// </summary>
        IM_SYS_INFODao iM_SYS_INFODao;

        /// <summary>
        /// 月次検索用Dao
        /// </summary>
        GetujiInfoDAO getujiInfoDAO;

        /// <summary>
        /// 年度検索用Dao
        /// </summary>
        NendoInfoDAO nendoInfoDAO;

        /// <summary>
        /// 部署情報検索用Dao
        /// </summary>
        BusyoInfoDAO busyoInfoDAO;

        F18_G275UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public F18Logic()
        {
            LogUtility.DebugMethodStart();

            this.iM_CORP_INFODao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.iM_SYS_INFODao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.getujiInfoDAO = DaoInitUtility.GetComponent<GetujiInfoDAO>();
            this.nendoInfoDAO = DaoInitUtility.GetComponent<NendoInfoDAO>();
            this.busyoInfoDAO = DaoInitUtility.GetComponent<BusyoInfoDAO>();

            LogUtility.DebugMethodEnd();
        }

        public F18Logic(F18_G275UIForm form)
        {
            LogUtility.DebugMethodStart();

            this.iM_CORP_INFODao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.iM_SYS_INFODao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.getujiInfoDAO = DaoInitUtility.GetComponent<GetujiInfoDAO>();
            this.nendoInfoDAO = DaoInitUtility.GetComponent<NendoInfoDAO>();
            this.busyoInfoDAO = DaoInitUtility.GetComponent<BusyoInfoDAO>();
            this.form = form;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region マスタ系情報取得
        /// <summary>
        /// 自社の期首月を取得
        /// </summary>
        /// <returns>自社の期首月</returns>
        public int getSelfCorpKishuMonth()
        {
            LogUtility.DebugMethodStart();

            M_CORP_INFO condition = new M_CORP_INFO();
            condition.SYS_ID = F18_G275ConstCls.SYS_ID;
            int ret = this.iM_CORP_INFODao.GetAllValidData(condition)[0].KISHU_MONTH.Value;

            LogUtility.DebugMethodEnd(ret);
            return ret;

        }

        /// <summary>
        /// アラート件数を取得
        /// </summary>
        /// <returns>アラート件数</returns>
        public int getIchiranAlertKensuu()
        {
            LogUtility.DebugMethodStart();

            int ret = this.iM_SYS_INFODao.GetAllDataForCode(F18_G275ConstCls.SYS_ID.ToString()).ICHIRAN_ALERT_KENSUU.Value;

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 請求情報金額端数CDを取得
        /// </summary>
        /// <returns>請求情報金額端数CD</returns>
        public int getSeikyuuKingakuHasuuCD()
        {
            LogUtility.DebugMethodStart();

            int ret = this.iM_SYS_INFODao.GetAllDataForCode(F18_G275ConstCls.SYS_ID.ToString()).SEIKYUU_KINGAKU_HASUU_CD.Value;

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region CSVファイル出力処理
        // 2014/01/24 oonaka delete CSVファイル名が不正 start
        ///// <summary>
        ///// CSVファイル出力
        ///// </summary>
        ///// <param name="p_customDataGridView">出力対象</param>
        ///// <param name="p_defaultFilename">デフォールトファイル名</param>
        //public void outputCSV(CustomDataGridView p_customDataGridView, string p_defaultFilename)
        //{
        //    LogUtility.DebugMethodStart(p_customDataGridView, p_defaultFilename);

        //    // 画面一覧内容を出力
        //    (new CSVExport()).ConvertCustomDataGridViewToCsv(p_customDataGridView, true, true, p_defaultFilename);

        //    LogUtility.DebugMethodEnd();
        //}
        // 2014/01/24 oonaka delete CSVファイル名が不正 start

        // 2014/01/24 oonaka add CSVファイル名が不正 start
        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="p_customDataGridView">出力対象</param>
        /// <param name="windowId">画面ID</param>
        public bool outputCSV(CustomDataGridView p_customDataGridView, WINDOW_ID windowId)
        {
            bool ret = true;

            LogUtility.DebugMethodStart(p_customDataGridView, windowId);
            try
            {


                // 画面一覧内容を出力
                (new CSVExport()).ConvertCustomDataGridViewToCsv(p_customDataGridView, true, true, WINDOW_TITLEExt.ToTitleString(windowId), this.form);

            }
            catch (Exception ex)
            {
                LogUtility.Error("outputCSV", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }
        // 2014/01/24 oonaka add CSVファイル名が不正 end
        #endregion

        #region 検索処理
        /// <summary>
        /// 年度データを取得
        /// </summary>
        /// <param name="p_nendo">年度</param>
        /// <param name="p_kishuMonth">自社期首月</param>
        /// <param name="p_busyo">部署</param>
        public NendoInfo[] searchNendoData(string p_nendo, int p_kishuMonth, string p_busyo, string p_denpyouKbn, out bool catchErr)
        {
            LogUtility.DebugMethodStart(p_nendo, p_kishuMonth, p_busyo, p_denpyouKbn);
            NendoInfo[] ret;
            catchErr = false;
            try
            {
                // 検索条件設定
                SearchNendoCondition condition = new SearchNendoCondition();
                condition.busyouCD = p_busyo;
                condition.denpyouKbn = p_denpyouKbn;
                // 年度9
                condition.nendo9FirstDay = new DateTime(int.Parse(p_nendo), p_kishuMonth, 1, 0, 0, 0);
                condition.nendo9LastDay = condition.nendo9FirstDay.AddYears(1).AddSeconds(-1);
                condition.nendo9 = condition.nendo9FirstDay.ToString("yyyy");
                condition.jinen9 = condition.nendo9FirstDay.AddYears(+1).ToString("yyyy");
                // 年度8
                condition.nendo8FirstDay = condition.nendo9FirstDay.AddYears(-1);
                condition.nendo8LastDay = condition.nendo9LastDay.AddYears(-1);
                condition.nendo8 = condition.nendo8FirstDay.ToString("yyyy");
                condition.jinen8 = condition.nendo8FirstDay.AddYears(+1).ToString("yyyy");
                // 年度7
                condition.nendo7FirstDay = condition.nendo8FirstDay.AddYears(-1);
                condition.nendo7LastDay = condition.nendo8LastDay.AddYears(-1);
                condition.nendo7 = condition.nendo7FirstDay.ToString("yyyy");
                condition.jinen7 = condition.nendo7FirstDay.AddYears(+1).ToString("yyyy");
                // 年度6
                condition.nendo6FirstDay = condition.nendo7FirstDay.AddYears(-1);
                condition.nendo6LastDay = condition.nendo7LastDay.AddYears(-1);
                condition.nendo6 = condition.nendo6FirstDay.ToString("yyyy");
                condition.jinen6 = condition.nendo6FirstDay.AddYears(+1).ToString("yyyy");
                // 年度5
                condition.nendo5FirstDay = condition.nendo6FirstDay.AddYears(-1);
                condition.nendo5LastDay = condition.nendo6LastDay.AddYears(-1);
                condition.nendo5 = condition.nendo5FirstDay.ToString("yyyy");
                condition.jinen5 = condition.nendo5FirstDay.AddYears(+1).ToString("yyyy");
                // 年度4
                condition.nendo4FirstDay = condition.nendo5FirstDay.AddYears(-1);
                condition.nendo4LastDay = condition.nendo5LastDay.AddYears(-1);
                condition.nendo4 = condition.nendo4FirstDay.ToString("yyyy");
                condition.jinen4 = condition.nendo4FirstDay.AddYears(+1).ToString("yyyy");
                // 年度3
                condition.nendo3FirstDay = condition.nendo4FirstDay.AddYears(-1);
                condition.nendo3LastDay = condition.nendo4LastDay.AddYears(-1);
                condition.nendo3 = condition.nendo3FirstDay.ToString("yyyy");
                condition.jinen3 = condition.nendo3FirstDay.AddYears(+1).ToString("yyyy");
                // 年度2
                condition.nendo2FirstDay = condition.nendo3FirstDay.AddYears(-1);
                condition.nendo2LastDay = condition.nendo3LastDay.AddYears(-1);
                condition.nendo2 = condition.nendo2FirstDay.ToString("yyyy");
                condition.jinen2 = condition.nendo2FirstDay.AddYears(+1).ToString("yyyy");
                // 年度1
                condition.nendo1FirstDay = condition.nendo2FirstDay.AddYears(-1);
                condition.nendo1LastDay = condition.nendo2LastDay.AddYears(-1);
                condition.nendo1 = condition.nendo1FirstDay.ToString("yyyy");
                condition.jinen1 = condition.nendo1FirstDay.AddYears(+1).ToString("yyyy");

                // 期首月に基づき指定年度の次の年になる月を設定
                // 自社情報の期首月が一月以外の場合
                if (p_kishuMonth != 1)
                {
                    // 初期化
                    condition.JINEN_FLG_01 = false;
                    condition.JINEN_FLG_02 = false;
                    condition.JINEN_FLG_03 = false;
                    condition.JINEN_FLG_04 = false;
                    condition.JINEN_FLG_05 = false;
                    condition.JINEN_FLG_06 = false;
                    condition.JINEN_FLG_07 = false;
                    condition.JINEN_FLG_08 = false;
                    condition.JINEN_FLG_09 = false;
                    condition.JINEN_FLG_10 = false;
                    condition.JINEN_FLG_11 = false;
                    condition.JINEN_FLG_12 = false;
                    for (int i = 1; i <= (p_kishuMonth - 1); i++)
                    {
                        switch (i)
                        {
                            case (1): condition.JINEN_FLG_01 = true; break;
                            case (2): condition.JINEN_FLG_02 = true; break;
                            case (3): condition.JINEN_FLG_03 = true; break;
                            case (4): condition.JINEN_FLG_04 = true; break;
                            case (5): condition.JINEN_FLG_05 = true; break;
                            case (6): condition.JINEN_FLG_06 = true; break;
                            case (7): condition.JINEN_FLG_07 = true; break;
                            case (8): condition.JINEN_FLG_08 = true; break;
                            case (9): condition.JINEN_FLG_09 = true; break;
                            case (10): condition.JINEN_FLG_10 = true; break;
                            case (11): condition.JINEN_FLG_11 = true; break;
                            case (12): condition.JINEN_FLG_12 = true; break;
                        }
                    }
                }

                ret = this.nendoInfoDAO.getNendoInfo(condition);
                this.editNendoInfo(ret);
            }
            catch (Exception ex)
            {
                LogUtility.Error("searchNendoData", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = null;
                catchErr = true;
            }
            LogUtility.DebugMethodEnd(ret, catchErr);
            return ret;
        }

        /// <summary>
        /// 年度データ編集(達成率計算)
        /// </summary>
        /// <param name="g_nendoInfo">年度データ</param>
        private void editNendoInfo(NendoInfo[] g_nendoInfo)
        {
            LogUtility.DebugMethodStart(g_nendoInfo);

            if (g_nendoInfo != null && g_nendoInfo.Length > 0)
            {
                for (int i = 0; i < g_nendoInfo.Length; i++)
                {
                    // 達成率計算
                    g_nendoInfo[i].TASSEI_RITSU_1 = this.getTasseritu(g_nendoInfo[i].YOSAN_1, Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_1)));
                    g_nendoInfo[i].TASSEI_RITSU_2 = this.getTasseritu(g_nendoInfo[i].YOSAN_2, Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_2)));
                    g_nendoInfo[i].TASSEI_RITSU_3 = this.getTasseritu(g_nendoInfo[i].YOSAN_3, Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_3)));
                    g_nendoInfo[i].TASSEI_RITSU_4 = this.getTasseritu(g_nendoInfo[i].YOSAN_4, Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_4)));
                    g_nendoInfo[i].TASSEI_RITSU_5 = this.getTasseritu(g_nendoInfo[i].YOSAN_5, Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_5)));
                    g_nendoInfo[i].TASSEI_RITSU_6 = this.getTasseritu(g_nendoInfo[i].YOSAN_6, Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_6)));
                    g_nendoInfo[i].TASSEI_RITSU_7 = this.getTasseritu(g_nendoInfo[i].YOSAN_7, Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_7)));
                    g_nendoInfo[i].TASSEI_RITSU_8 = this.getTasseritu(g_nendoInfo[i].YOSAN_8, Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_8)));
                    g_nendoInfo[i].TASSEI_RITSU_9 = this.getTasseritu(g_nendoInfo[i].YOSAN_9, Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_9)));

                    // 端数処理をした状態で合計値を再計算
                    g_nendoInfo[i].JISSEKI_GOUKEI = Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_1)) +
                                                    Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_2)) +
                                                    Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_3)) +
                                                    Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_4)) +
                                                    Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_5)) +
                                                    Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_6)) +
                                                    Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_7)) +
                                                    Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_8)) +
                                                    Convert.ToDecimal(this.form.editYenToYen1000(g_nendoInfo[i].JISSEKI_9));

                    g_nendoInfo[i].TASSEI_GOKEI = this.getTasseritu(g_nendoInfo[i].YOSAN_GOUKEI, Convert.ToDecimal(this.form.editYenToYen(g_nendoInfo[i].JISSEKI_GOUKEI)));
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 月次データを取得
        /// </summary>
        /// <param name="p_nendo">年度</param>
        /// <param name="p_kishuMonth">自社期首月</param>
        /// <param name="p_busyo">部署</param>
        public GetujiInfo[] searchGetujiData(string p_nendo, int p_kishuMonth, string p_busyo, string p_denpyouKbn, out bool catchErr)
        {
            LogUtility.DebugMethodStart(p_nendo, p_kishuMonth, p_busyo, p_denpyouKbn);
            GetujiInfo[] ret = null;
            catchErr = false;
            try
            {

                // 検索条件設定
                SearchGetujiCondition condition = new SearchGetujiCondition();
                condition.busyouCD = p_busyo;
                condition.nendo = p_nendo;
                condition.denpyouKbn = p_denpyouKbn;
                condition.nendoFirstDay = new DateTime(int.Parse(p_nendo), p_kishuMonth, 1, 0, 0, 0);
                condition.nendoLastDay = condition.nendoFirstDay.AddYears(1).AddSeconds(-1);
                condition.month1 = condition.nendoFirstDay.ToString("yyyyMM");
                condition.month2 = condition.nendoFirstDay.AddMonths(1).ToString("yyyyMM");
                condition.month3 = condition.nendoFirstDay.AddMonths(2).ToString("yyyyMM");
                condition.month4 = condition.nendoFirstDay.AddMonths(3).ToString("yyyyMM");
                condition.month5 = condition.nendoFirstDay.AddMonths(4).ToString("yyyyMM");
                condition.month6 = condition.nendoFirstDay.AddMonths(5).ToString("yyyyMM");
                condition.month7 = condition.nendoFirstDay.AddMonths(6).ToString("yyyyMM");
                condition.month8 = condition.nendoFirstDay.AddMonths(7).ToString("yyyyMM");
                condition.month9 = condition.nendoFirstDay.AddMonths(8).ToString("yyyyMM");
                condition.month10 = condition.nendoFirstDay.AddMonths(9).ToString("yyyyMM");
                condition.month11 = condition.nendoFirstDay.AddMonths(10).ToString("yyyyMM");
                condition.month12 = condition.nendoFirstDay.AddMonths(11).ToString("yyyyMM");

                ////検索処理
                // 表示用
                ret = this.getujiInfoDAO.getGetujiInfo(condition);

                // 期首月が１月以外の場合
                if (p_kishuMonth != 1)
                {
                    // 次年度のデータを取得
                    condition.nendo = (Convert.ToInt16(condition.nendo) + 1).ToString();
                    var jinen = this.getujiInfoDAO.getGetujiInfo(condition);
                    condition.nendo = (Convert.ToInt16(condition.nendo) - 1).ToString();

                    // 次年度テーブルを表示用テーブルに上書き
                    for (int i = 0; i < jinen.Length; i++)
                    {
                        for (int j = 1; j <= (p_kishuMonth - 1); j++)
                        {
                            switch (j)
                            {
                                case (1): ret[i].YOSAN_1 = jinen[i].YOSAN_1; break;
                                case (2): ret[i].YOSAN_2 = jinen[i].YOSAN_2; break;
                                case (3): ret[i].YOSAN_3 = jinen[i].YOSAN_3; break;
                                case (4): ret[i].YOSAN_4 = jinen[i].YOSAN_4; break;
                                case (5): ret[i].YOSAN_5 = jinen[i].YOSAN_5; break;
                                case (6): ret[i].YOSAN_6 = jinen[i].YOSAN_6; break;
                                case (7): ret[i].YOSAN_7 = jinen[i].YOSAN_7; break;
                                case (8): ret[i].YOSAN_8 = jinen[i].YOSAN_8; break;
                                case (9): ret[i].YOSAN_9 = jinen[i].YOSAN_9; break;
                                case (10): ret[i].YOSAN_10 = jinen[i].YOSAN_10; break;
                                case (11): ret[i].YOSAN_11 = jinen[i].YOSAN_11; break;
                                case (12): ret[i].YOSAN_12 = jinen[i].YOSAN_12; break;
                            }
                        }
                        // 合計を再計算
                        ret[i].YOSAN_GOUKEI =
                            ret[i].YOSAN_1 +
                            ret[i].YOSAN_2 +
                            ret[i].YOSAN_3 +
                            ret[i].YOSAN_4 +
                            ret[i].YOSAN_5 +
                            ret[i].YOSAN_6 +
                            ret[i].YOSAN_7 +
                            ret[i].YOSAN_8 +
                            ret[i].YOSAN_9 +
                            ret[i].YOSAN_10 +
                            ret[i].YOSAN_11 +
                            ret[i].YOSAN_12;
                    }
                }

                this.editGetujiInfo(ret, p_kishuMonth);

            }
            catch (Exception ex)
            {
                LogUtility.Error("searchGetujiData", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = null;
                catchErr = true;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);
            return ret;
        }

        /// <summary>
        /// 月次データ編集(達成率計算)
        /// </summary>
        /// <param name="g_getujiInfo">月次データ</param>
        private void editGetujiInfo(GetujiInfo[] g_getujiInfo, int p_kishuMonth)
        {
            // 当functionはデータ件数の倍数毎、重複呼出され、ログ出力すると動作は大変遅くなる
            //LogUtility.DebugMethodStart(g_getujiInfo);

            // GetujiInfoクラスのメンバを動的に呼び出すためTypeを取得
            Type t = typeof(GetujiInfo);

            // 設定される側の項目を期首月に応じて並び替える
            string[] MonthArray = new string[12];
            DateTime nendoStart = new DateTime(this.parentForm.sysDate.Year, p_kishuMonth, 1, 0, 0, 0, 0);
            for (int i = 0; i < F18_G275ConstCls.COUNT_MONTH; i++)
            {
                // 月次一覧ヘッダ部の月項目の表示文字設定
                MonthArray[i] = "YOSAN_" + nendoStart.AddMonths(i).Month.ToString();
            }

            if (g_getujiInfo != null && g_getujiInfo.Length > 0)
            {
                for (int i = 0; i < g_getujiInfo.Length; i++)
                {
                    // 達成率計算
                    g_getujiInfo[i].TASSEI_RITSU_1 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[0], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_1)));
                    g_getujiInfo[i].TASSEI_RITSU_2 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[1], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_2)));
                    g_getujiInfo[i].TASSEI_RITSU_3 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[2], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_3)));
                    g_getujiInfo[i].TASSEI_RITSU_4 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[3], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_4)));
                    g_getujiInfo[i].TASSEI_RITSU_5 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[4], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_5)));
                    g_getujiInfo[i].TASSEI_RITSU_6 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[5], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_6)));
                    g_getujiInfo[i].TASSEI_RITSU_7 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[6], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_7)));
                    g_getujiInfo[i].TASSEI_RITSU_8 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[7], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_8)));
                    g_getujiInfo[i].TASSEI_RITSU_9 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[8], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_9)));
                    g_getujiInfo[i].TASSEI_RITSU_10 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[9], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_10)));
                    g_getujiInfo[i].TASSEI_RITSU_11 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[10], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_11)));
                    g_getujiInfo[i].TASSEI_RITSU_12 = this.getTasseritu((decimal)t.InvokeMember(MonthArray[11], BindingFlags.GetProperty, null, g_getujiInfo[i], null), Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_12)));

                    // 端数処理をした状態で合計値を再計算
                    g_getujiInfo[i].JISSEKI_GOUKEI = Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_1)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_2)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_3)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_4)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_5)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_6)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_7)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_8)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_9)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_10)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_11)) +
                                                     Convert.ToDecimal(this.form.editYenToYen1000(g_getujiInfo[i].JISSEKI_12));

                    g_getujiInfo[i].TASSEI_GOKEI = this.getTasseritu(g_getujiInfo[i].YOSAN_GOUKEI, Convert.ToDecimal(this.form.editYenToYen(g_getujiInfo[i].JISSEKI_GOUKEI)));
                }
            }

            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 達成率計算
        /// </summary>
        /// <param name="p_yosan">予算(千円)</param>
        /// <param name="p_jisseki">実績</param>
        /// <returns>達成率(%)</returns>
        private decimal getTasseritu(decimal p_yosan, decimal p_jisseki)
        {
            // 当functionはデータ件数の倍数毎、重複呼出され、ログ出力すると動作は大変遅くなる
            //LogUtility.DebugMethodStart(p_yosan, p_jisseki);

            decimal ret = 0m;
            if ((p_yosan != 0m) && (p_jisseki != 0m))
            {
                ret = p_jisseki / p_yosan * 100;
            }
            else if ((p_yosan == 0m) && (p_jisseki >= 1m))
            {
                ret = 100;
            }
            //LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// popup用部署情報取得
        /// </summary>
        /// <returns>部署情報</returns>
        public DataTable getPopupBusyoInfo(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            DataTable popupData = null;
            catchErr = false;
            try
            {
                popupData = this.busyoInfoDAO.getBusyoInfo(null);

                // ポップアップのタイトル
                popupData.TableName = "部署検索";

            }
            catch (Exception ex)
            {
                LogUtility.Error("getPopupBusyoInfo", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                popupData = null;
                catchErr = true;
            }
            LogUtility.DebugMethodEnd(popupData, catchErr);
            return popupData;
        }

        /// <summary>
        /// 部署コードより、部署情報取得
        /// </summary>
        /// <param name="p_busyoCD">部署コード</param>
        /// <returns>部署情報</returns>
        public DataTable getInfoByBusyoCD(string p_busyoCD)
        {
            LogUtility.DebugMethodStart(p_busyoCD);

            SearchBusyoCondition condition = new SearchBusyoCondition();
            condition.busyouCD = p_busyoCD;
            DataTable busyoInfo = this.busyoInfoDAO.getBusyoInfo(condition);

            LogUtility.DebugMethodEnd(busyoInfo);
            return busyoInfo;
        }

        #endregion


        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion


        #region デフォルトメソッド
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

