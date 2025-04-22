using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Stock.ZaikoShimeSyori.Const;
using Shougun.Core.Stock.ZaikoShimeSyori.DAO;
using Shougun.Core.Stock.ZaikoShimeSyori.DTO;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;
using r_framework.Entity;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon;
using r_framework.Const;
using r_framework.Dao;
using CommonChouhyouPopup.App;
using r_framework.Dto;
using Shougun.Core.Stock.ZaikoShimeSyori.APP;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Stock.ZaikoShimeSyori
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class F18Logic : IBuisinessLogic
    {

        #region フィールド

        /// <summary>
        /// 現場情報取得Dao
        /// </summary>
        private GenbaInfoDao genbaInfoDao;

        /// <summary>
        /// 締め情報取得Dao
        /// </summary>
        private ShimeInfoDao shimeInfoDao;

        /// <summary>
        /// 締めの元情報取得Dao
        /// </summary>
        private ShimeTargetInfoDao shimeTargetInfoDao;

        /// <summary>
        /// 評価方法取得Dao
        /// </summary>
        private ZaikoHyoukaHouhouDao zaikoHyoukaHouhouDao;

        /// <summary>
        /// 在庫品名マスタ情報取得Dao
        /// </summary>
        private MZaikoHinmeiDao mZaikoHinmeiDao;

        /// <summary>
        /// T_ZAIKO_TANK処理Dao
        /// </summary>
        private IT_ZAIKO_TANKDao iT_ZAIKO_TANKDao;

        /// <summary>
        /// T_ZAIKO_UKEIRE_DETAIL処理Dao
        /// </summary>
        private IT_ZAIKO_TANK_DETAILDao iT_ZAIKO_TANK_DETAILDao;

        /// <summary>
        /// システムID採番用Dao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// M_CORP_INFO検索用Dao
        /// </summary>
        private IM_CORP_INFODao iM_CORP_INFODao;

        ///// <summary>
        ///// systemId
        ///// </summary>
        //private int systemId;

        private MessageBoxShowLogic MsgBox;

        private F18_G170UIForm form;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public F18Logic(F18_G170UIForm targetForm)
        {
            LogUtility.DebugMethodStart();

            // DAOの初期化
            this.genbaInfoDao = DaoInitUtility.GetComponent<GenbaInfoDao>();
            this.shimeInfoDao = DaoInitUtility.GetComponent<ShimeInfoDao>();
            this.shimeTargetInfoDao = DaoInitUtility.GetComponent<ShimeTargetInfoDao>();
            this.zaikoHyoukaHouhouDao = DaoInitUtility.GetComponent<ZaikoHyoukaHouhouDao>();
            this.mZaikoHinmeiDao = DaoInitUtility.GetComponent<MZaikoHinmeiDao>();
            this.iT_ZAIKO_TANKDao = DaoInitUtility.GetComponent<IT_ZAIKO_TANKDao>();
            this.iT_ZAIKO_TANK_DETAILDao = DaoInitUtility.GetComponent<IT_ZAIKO_TANK_DETAILDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();
            this.iM_CORP_INFODao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.MsgBox = new MessageBoxShowLogic();
            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }
        #endregion


        #region 業務処理

        /// <summary>
        /// 評価方法取得
        /// </summary>
        /// <returns>評価方法(マスタ上可能値1～5)</returns>
        public int getZaikoHyoukaHouhou()
        {
            // 戻り値の初期設定
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();

                // ≪システム設定 M_SYS_INFO≫を検索
                ZaikoHyoukaHouhou result = this.zaikoHyoukaHouhouDao.getZaikoHyoukaHouhou(null);
                if (result != null)
                {
                    ret = int.Parse(result.RET_ZAIKO_HYOUKA_HOUHOU);
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("getZaikoHyoukaHouhou", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 現場情報を取得
        /// </summary>
        /// <param name="p_genbaCD">現場CD</param>
        /// <param name="p_gyousyaCD">業者CD</param>
        /// <returns>現場情報</returns>
        public GenbaInfo[] getGenbaInfo(string p_gyousyaCD, string p_genbaCD, out bool catchErr)
        {
            catchErr = false;
            GenbaInfo[] ret = null;
            try
            {
                LogUtility.DebugMethodStart(p_gyousyaCD, p_genbaCD);

                ret = this.genbaInfoDao.getGenbaInfo(p_gyousyaCD, p_genbaCD);
            }
            catch (Exception ex)
            {
                LogUtility.Error("getGenbaInfo", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 指定条件より締情報を取得
        /// </summary>
        /// <param name="p_gyouShaCD">業者コード</param>
        /// <param name="p_genbaCD">現場コード</param>
        /// <param name="p_dayFrom">対象日from</param>
        /// <param name="p_dayTo">対象日to</param>
        /// <returns>締情報</returns>
        public DataTable searchSimeInfo(string p_gyouShaCD, string p_genbaCD, DateTime p_dayFrom, DateTime p_dayTo, out bool catchErr)
        {
            catchErr = false;
            DataTable ret = null;
            try
            {
                LogUtility.DebugMethodStart(p_gyouShaCD, p_genbaCD, p_dayFrom, p_dayTo);

                // 検索条件作成
                F18_G170Dto condition = new F18_G170Dto();
                condition.gyoushaCD = p_gyouShaCD;
                condition.genbaCD = p_genbaCD;
                condition.simeTaisyouKikanFrom = p_dayFrom;
                condition.simeTaisyouKikanTo = p_dayTo;

                // 検索
                ret = this.shimeInfoDao.searchShimeInfo(condition);
                // 最後の計算結果のreadonlyを解除
                ret.Columns[ret.Columns.Count - 1].ReadOnly = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("searchSimeInfo", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 指定条件より締情報を作成
        /// </summary>
        /// <param name="p_gyouShaCD">業者コード</param>
        /// <param name="p_genbaCD">現場コード</param>
        /// <param name="p_dayFrom">対象日from</param>
        /// <param name="p_dayTo">対象日to</param>
        /// <param name="p_zaikoHyoukaHouhou">評価方法</param>
        /// <returns>発生したエラーメッセージID</returns>
        public string insertSimeInfo(String p_gyouShaCD, String p_genbaCD, DateTime p_dayFrom, DateTime p_dayTo, int p_zaikoHyoukaHouhou)
        {
            LogUtility.DebugMethodStart(p_gyouShaCD, p_genbaCD, p_dayFrom, p_dayTo, p_zaikoHyoukaHouhou);

            this.form.isRegistErr = false;

            // 該当月締め状態チェック
            bool catchErr = false;
            DataTable simeDataOfThisMonth = this.searchSimeInfo(p_gyouShaCD, p_genbaCD, p_dayFrom, p_dayTo, out catchErr);
            if (catchErr)
            {
                this.form.isRegistErr = true;
                return string.Empty;
            }
            if ((simeDataOfThisMonth != null) && (simeDataOfThisMonth.Rows.Count > 0))
            {
                LogUtility.DebugMethodEnd(F18_G170ConstCls.E_MSGID_INSERT_DATA_EXIST);

                // 締め済み場合
                return F18_G170ConstCls.E_MSGID_INSERT_DATA_EXIST;
            }

            // 先月締め状態チェック
            DataTable prewSimeInfo = this.getPrewSimeInfo(p_gyouShaCD, p_genbaCD, p_dayFrom, p_dayTo);
            Dictionary<String, ShimeInfo> prewInfoDictionary = this.getPrewSimeInfoDictionary(prewSimeInfo);

            // 該当締めデータ作成
            // ①該当締め対象データを取得
            F18_G170Dto condition = new F18_G170Dto();
            condition.gyoushaCD = p_gyouShaCD;
            condition.genbaCD = p_genbaCD;
            condition.simeTaisyouKikanFrom = p_dayFrom;
            condition.simeTaisyouKikanTo = p_dayTo;
            ShimeTargetInfo[] targetData = this.shimeTargetInfoDao.searchShimeTargetInfo(condition);
            // ②登録用締めデータを編集
            List<T_ZAIKO_TANK> zaikoTankList = new List<T_ZAIKO_TANK>();
            List<T_ZAIKO_TANK_DETAIL> zaikoTankDetailList = new List<T_ZAIKO_TANK_DETAIL>();
            // ②≪在庫締データ　T_ZAIKO_TANK≫と≪在庫締明細　T_ZAIKO_TANK_DETAIL≫の登録データ作成
            this.cearteInsertData(
                prewInfoDictionary,
                prewSimeInfo, targetData,
                zaikoTankList, zaikoTankDetailList, p_zaikoHyoukaHouhou, p_dayTo);

            // 締情報が存在しない場合
            if (zaikoTankDetailList.Count == 0 && zaikoTankList.Count == 0)
            {
                // アラートを表示する
                return F18_G170ConstCls.E_MSGID_INSERT_DATA_NOTEXIST;
            }

            // ③締めデータを登録データ作成
            try
            {
                using (Transaction tran = new Transaction())
                {
                    // ③-1.≪在庫締明細　T_ZAIKO_TANK_DETAIL≫データ登録
                    foreach (T_ZAIKO_TANK_DETAIL zaikoTankDetail in zaikoTankDetailList)
                    {
                        this.iT_ZAIKO_TANK_DETAILDao.Insert(zaikoTankDetail);
                    }
                    // ③-2.≪在庫締データ　T_ZAIKO_TANK≫データ登録
                    foreach (T_ZAIKO_TANK tmpZaikoTank in zaikoTankList)
                    {
                        this.iT_ZAIKO_TANKDao.Insert(tmpZaikoTank);
                    }

                    tran.Commit();
                }
            }
            catch (Exception e)
            {
                LogUtility.Error("insertSimeInfo", e);
                return F18_G170ConstCls.E_MSG_INSERT;
            }

            LogUtility.DebugMethodEnd();
            return "";
        }

        /// <summary>
        /// 指定した条件より、先月データを取得
        /// </summary>
        /// <param name="p_gyouShaCD">業者コード</param>
        /// <param name="p_genbaCD">現場コード</param>
        /// <param name="p_dayFrom">対象日from</param>
        /// <param name="p_dayTo">対象日to</param>
        /// <returns>先月締残数</returns>
        private DataTable getPrewSimeInfo(String p_gyouShaCD, String p_genbaCD, DateTime p_dayFrom, DateTime p_dayTo)
        {
            LogUtility.DebugMethodStart(p_gyouShaCD, p_genbaCD, p_dayFrom, p_dayTo);

            bool catchErr = false;
            DateTime prewFirstDay = p_dayFrom.AddMonths(-1);// 先月の初日 = 当月01日 - 1ヶ月
            DateTime prewLastDay = p_dayFrom.AddSeconds(-1);// 先月の末 = 当月初日 - 1 Seconds
            DataTable ret = this.searchSimeInfo(p_gyouShaCD, p_genbaCD, prewFirstDay, prewLastDay, out catchErr);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 先月データより、検索用Dictionaryを作成(key：業者CD+現場CD+在庫品名CD)
        /// </summary>
        /// <param name="p_prewSimeInfo">先月データ</param>
        /// <returns>検索用Dictionary</returns>
        private Dictionary<string, ShimeInfo> getPrewSimeInfoDictionary(DataTable p_prewSimeInfo)
        {
            LogUtility.DebugMethodStart(p_prewSimeInfo);

            Dictionary<string, ShimeInfo> ret = new Dictionary<string, ShimeInfo>();

            if (p_prewSimeInfo != null && p_prewSimeInfo.Rows.Count > 0)
            {
                for (int i = 0; i < p_prewSimeInfo.Rows.Count; i++)
                {
                    // [業者コード]+[現場コード]+[在庫品名コード]をkeyとして、先月の締めデータを作成
                    string key =
                        p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD].ToString()
                        + p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD].ToString()
                        + p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_CD].ToString();
                    ShimeInfo prewMonthData = new ShimeInfo();
                    prewMonthData.RET_GYOUSHA_CD = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD].ToString();//業者CD
                    prewMonthData.RET_GENBA_CD = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD].ToString();//現場CD
                    prewMonthData.RET_GENBA_NAME_RYAKU = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_NAME_RYAKU].ToString();//現場名
                    prewMonthData.RET_ZAIKO_HINMEI_CD = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_CD].ToString();//在庫CD
                    prewMonthData.RET_ZAIKO_HINMEI_RYAKU = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_RYAKU].ToString();//在庫品名
                    prewMonthData.RET_REMAIN_SUU = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_REMAIN_SUU].ToString();//前月残数
                    prewMonthData.RET_ENTER_SUU = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ENTER_SUU].ToString();//当月受入数
                    prewMonthData.RET_OUT_SUU = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_OUT_SUU].ToString();//当月出荷量
                    prewMonthData.RET_ADJUST_SUU = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ADJUST_SUU].ToString();//調整量
                    prewMonthData.RET_TOTAL_SUU = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_TOTAL_SUU].ToString();//当月在庫残
                    prewMonthData.RET_TANKA = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_TANKA].ToString();//評価単価
                    prewMonthData.RET_MULT = p_prewSimeInfo.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_MULT].ToString();//在庫金額
                    ret.Add(key, prewMonthData);
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 登録用データを作成
        /// <param name="p_prewDataDictionary">先月締めデータのDictionary</param>
        /// <param name="p_prewSimeInfo">先月締めデータ</param>
        /// <param name="p_targetData">該当登録データ</param>
        /// <param name="p_zaikoTankList">作成対象の≪在庫締データ　T_ZAIKO_TANK≫</param>
        /// <param name="p_zaikoTankDetailList">作成対象の≪在庫締明細　T_ZAIKO_TANK_DETAIL≫</param>
        /// <param name="p_zaikoHyoukaHouhou">評価方法</param>
        /// <param name="p_simeDate">締め日</param>
        /// </summary>
        private void cearteInsertData(
            Dictionary<string, ShimeInfo> p_prewDataDictionary,
            DataTable p_prewSimeInfo, ShimeTargetInfo[] p_targetData,
            List<T_ZAIKO_TANK> p_zaikoTankList, List<T_ZAIKO_TANK_DETAIL> p_zaikoTankDetailList, int p_zaikoHyoukaHouhou, DateTime p_simeDate)
        {
            LogUtility.DebugMethodStart(p_prewDataDictionary, p_prewSimeInfo, p_targetData, p_zaikoTankList, p_zaikoTankDetailList, p_zaikoHyoukaHouhou, p_simeDate);

            // 一時変数定義
            string tmpGyoushaCD = "";
            string tmpGenbaCD = "";
            string tmpZaikoHinmeiCD = "";
            SqlInt64 systemID = -1;// システムID
            SqlInt32 rowNo = 0;// 行番号

            // 「業者」と「現場」より採番したシステムIDを管理
            Dictionary<string, SqlInt64> systemIDDictionary = new Dictionary<string, SqlInt64>();
            // システムIDの最大rowNoを管理
            Dictionary<SqlInt64, SqlInt32> maxRowNoOfsystemIDDictionary = new Dictionary<SqlInt64, SqlInt32>();

            // ●当月入庫/出荷/在庫調整情報が存在する商品の締めデータ作成
            if (p_targetData != null && p_targetData.Length > 0)
            {

                List<F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO> tmpDetailForEditList = new List<F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO>();//
                F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO tmpDetailForEdit = null;
                for (int i = 0; i < p_targetData.Length; i++ )
                {
                    // 同じシステムID内、「在庫商品」毎、行番号を採番し、≪在庫締明細　T_ZAIKO_TANK_DETAIL≫データを作成
                    if (!tmpGyoushaCD.Equals(p_targetData[i].RET_GYOUSHA_CD) || !tmpGenbaCD.Equals(p_targetData[i].RET_GENBA_CD))
                    {
                        // 新しい「業者」と「現場」の組合せ時（別システムID、行番号を0から）

                        // keyの一時変数最新化
                        tmpGyoushaCD = p_targetData[i].RET_GYOUSHA_CD;// 業者コード
                        tmpGenbaCD = p_targetData[i].RET_GENBA_CD;// 現場コード
                        tmpZaikoHinmeiCD = p_targetData[i].RET_ZAIKO_HINMEI_CD;// 在庫商品コード

                        // 「業者」「現場」毎、システムIDを採番
                        systemID = this.saibanSystemId();
                        systemIDDictionary.Add(tmpGyoushaCD + tmpGenbaCD, systemID);

                        // １．≪在庫締データ　T_ZAIKO_TANK≫データ作成
                        p_zaikoTankList.Add(this.getZaikoTank(systemID, tmpGyoushaCD, tmpGenbaCD, p_zaikoHyoukaHouhou, p_simeDate));


                        // ２．≪在庫締明細　T_ZAIKO_TANK_DETAIL≫データ作成
                        // ・明細データインスタンス生成
                        tmpDetailForEdit = new F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO();
                        tmpDetailForEdit.calculationList = new List<ShimeTargetInfo>();

                        tmpDetailForEdit.SYSTEM_ID = systemID;
                        rowNo = 0;// 行番号0にリセット
                        tmpDetailForEdit.ROW_NO = rowNo;
                        maxRowNoOfsystemIDDictionary.Add(systemID, rowNo);

                        tmpDetailForEdit.ZAIKO_HINMEI_CD = tmpZaikoHinmeiCD;
                        // ・先月データを設定
                        string keyOfPrewMonthData = tmpGyoushaCD + tmpGenbaCD + tmpZaikoHinmeiCD;
                        if (p_prewDataDictionary.ContainsKey(keyOfPrewMonthData))
                        {
                            tmpDetailForEdit.prewMonthInfo = p_prewDataDictionary[keyOfPrewMonthData];
                        }
                        // ・計算用情報部追加
                        tmpDetailForEdit.calculationList.Add(p_targetData[i]);
                        // ・明細を明細リストに追加
                        tmpDetailForEditList.Add(tmpDetailForEdit);

                        // 今月処理ある商品データを先月データリストから除外
                        if (tmpDetailForEdit.prewMonthInfo != null)
                        {
                            this.removeTargetRow(p_prewSimeInfo, keyOfPrewMonthData);
                        }
                    }
                    else
                    {
                        // 同じ「業者」と「現場」の組合せ時(システムIDは同じ)

                        if (!tmpZaikoHinmeiCD.Equals(p_targetData[i].RET_ZAIKO_HINMEI_CD))
                        {
                            // 新しい「在庫商品」の場合

                            // keyの一時変数最新化
                            tmpZaikoHinmeiCD = p_targetData[i].RET_ZAIKO_HINMEI_CD;// 在庫商品コード

                            // １．≪在庫締明細　T_ZAIKO_TANK_DETAIL≫データ作成
                            // ・明細データインスタンス生成
                            tmpDetailForEdit = new F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO();
                            tmpDetailForEdit.calculationList = new List<ShimeTargetInfo>();

                            tmpDetailForEdit.SYSTEM_ID = systemID;
                            rowNo += 1;// 行番号+1
                            tmpDetailForEdit.ROW_NO = rowNo;
                            maxRowNoOfsystemIDDictionary.Remove(systemID);
                            maxRowNoOfsystemIDDictionary.Add(systemID, rowNo);

                            tmpDetailForEdit.ZAIKO_HINMEI_CD = tmpZaikoHinmeiCD;

                            // ・先月データを設定
                            string keyOfPrewMonthData = tmpGyoushaCD + tmpGenbaCD + tmpZaikoHinmeiCD;
                            if (p_prewDataDictionary.ContainsKey(keyOfPrewMonthData))
                            {
                                tmpDetailForEdit.prewMonthInfo = p_prewDataDictionary[keyOfPrewMonthData];
                            }
                            // ・計算用情報部追加
                            tmpDetailForEdit.calculationList.Add(p_targetData[i]);
                            // ・明細を明細リストに追加
                            tmpDetailForEditList.Add(tmpDetailForEdit);

                            // 今月処理ある商品データを先月データリストから除外
                            if (tmpDetailForEdit.prewMonthInfo != null)
                            {
                                this.removeTargetRow(p_prewSimeInfo, keyOfPrewMonthData);
                            }
                        }
                        else
                        {
                            // 同じ「在庫商品」の場合
                            // 明細データの計算用情報部追加
                            tmpDetailForEdit.calculationList.Add(p_targetData[i]);
                        }
                    }
                }

                // 当月操作ある商品データより明細データを編集
                this.createZaikoUkeireDetail(p_zaikoTankDetailList, tmpDetailForEditList, p_zaikoHyoukaHouhou);

            }


            // ●先月の締め情報のみが存在する商品の締めデータ作成
            tmpGyoushaCD = "";
            tmpGenbaCD = "";
            tmpZaikoHinmeiCD = "";
            systemID = -1;// システムID
            rowNo = 0;// 行番号
            for (int j = 0; j < p_prewSimeInfo.Rows.Count; j++)
            {
                // １．≪在庫締データ　T_ZAIKO_TANK≫データ作成
                if (!tmpGyoushaCD.Equals(p_prewSimeInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD].ToString())
                    || !tmpGenbaCD.Equals(p_prewSimeInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD].ToString()))
                {
                    // 新しい「業者」と「現場」の組合せ時

                    // keyの一時変数最新化
                    tmpGyoushaCD = p_prewSimeInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD].ToString();
                    tmpGenbaCD = p_prewSimeInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD].ToString();


                    // 「業者」「現場」毎、システムIDを採番
                    if (!systemIDDictionary.ContainsKey(tmpGyoushaCD + tmpGenbaCD))
                    {
                        // 「業者」と「現場」より、未採番
                        systemID = this.saibanSystemId();
                        // 行番号0にリセット
                        rowNo = 0;

                        // データ作成
                        p_zaikoTankList.Add(this.getZaikoTank(systemID, tmpGyoushaCD, tmpGenbaCD, p_zaikoHyoukaHouhou, p_simeDate));
                    }
                    else
                    {
                        // 業者」と「現場」より、採番済み
                        systemID = systemIDDictionary[tmpGyoushaCD + tmpGenbaCD];
                        rowNo = maxRowNoOfsystemIDDictionary[systemID] + 1;
                        maxRowNoOfsystemIDDictionary.Remove(systemID);
                        maxRowNoOfsystemIDDictionary.Add(systemID, rowNo);
                    }
                }

                tmpZaikoHinmeiCD = p_prewSimeInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_CD].ToString();

                // ２．≪在庫締明細　T_ZAIKO_TANK_DETAIL≫データ作成
                T_ZAIKO_TANK_DETAIL tmpDetail = new T_ZAIKO_TANK_DETAIL();
                tmpDetail.SYSTEM_ID = systemID;
                tmpDetail.ROW_NO = rowNo;
                tmpDetail.ZAIKO_HINMEI_CD = tmpZaikoHinmeiCD;
                tmpDetail.REMAIN_SUU = new SqlDecimal(decimal.Parse(p_prewSimeInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_TOTAL_SUU].ToString()));// [前月繰越残]は先月の[当月在庫残]
                tmpDetail.ENTER_SUU = new SqlDecimal(0f);// 入荷総量
                tmpDetail.OUT_SUU = new SqlDecimal(0f);// 出荷総量
                tmpDetail.ADJUST_SUU = new SqlDecimal(0f);// 調整総量
                tmpDetail.TOTAL_SUU = tmpDetail.REMAIN_SUU;// 当月在庫残
                tmpDetail.TANKA = new SqlDecimal(decimal.Parse(p_prewSimeInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_TANKA].ToString()));
                p_zaikoTankDetailList.Add(tmpDetail);

                // 行番号+1
                rowNo += 1;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 指定したkeyより、行を対象DataTableから削除
        /// </summary>
        /// <pparam name="p_target">対象DataTable</param>
        /// <pparam name="p_key">対象DataTable</param>
        private void removeTargetRow(DataTable p_target, string p_key)
        {
            if (p_target != null)
            {
                for (int i = 0; (p_target != null) && (i < p_target.Rows.Count); i++)
                {
                    string rowKey =
                        p_target.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD].ToString()
                        + p_target.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD].ToString()
                        + p_target.Rows[i][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_CD].ToString();
                    if (rowKey.Equals(p_key))
                    {
                        p_target.Rows.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// システムID採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        private SqlInt64 saibanSystemId()
        {
            LogUtility.DebugMethodStart();

            SqlInt64 ret = 1;
            // 在庫の最大値+1を取得    
            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = (Int16)DENSHU_KBN.ZAIKO;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            ret = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (Int16)DENSHU_KBN.ZAIKO;
                updateEntity.CURRENT_NUMBER = ret;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = ret;
                this.numberSystemDao.Update(updateEntity);
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ≪在庫締データ　T_ZAIKO_TANK≫登録用データ作成
        /// </summary>
        /// <param name="p_systemID">システムID</param>
        /// <param name="p_gyouShaCD">業者CD</param>
        /// <param name="p_genbaCD">現場CD</param>
        /// <param name="p_zaikoHyoukaHouhou">評価方法</param>
        /// <param name="p_simeDate">締め日</param>
        /// <returns>≪在庫締データ　T_ZAIKO_TANK≫データ</returns>
        private T_ZAIKO_TANK getZaikoTank(SqlInt64 p_systemID, string p_gyouShaCD, string p_genbaCD, int p_zaikoHyoukaHouhou, DateTime p_simeDate)
        {
            LogUtility.DebugMethodStart(p_systemID, p_gyouShaCD, p_genbaCD, p_zaikoHyoukaHouhou, p_simeDate);

            T_ZAIKO_TANK ret = new T_ZAIKO_TANK();
            // 業務項目設定
            ret.SYSTEM_ID = p_systemID; ;//システムID
            ret.ZAIKO_SHIME_DATE = p_simeDate;//在庫締実行日
            ret.GYOUSHA_CD = p_gyouShaCD;//業者CD
            ret.GENBA_CD = p_genbaCD;//現場コード
            ret.ZAIKO_ASSESSMENT_KBN = SqlInt16.Parse(p_zaikoHyoukaHouhou.ToString());//在庫評価方法区分
            ret.DELETE_FLG = SqlBoolean.False;// 削除区分
            // システム共通項目設定
            (new DataBinderLogic<T_ZAIKO_TANK>(ret)).SetSystemProperty(ret, false);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 在庫締明細データの編集
        /// </summary>
        /// <param name="p_zaikoTankDetailList">編集結果明細リスト</param>
        /// <param name="p_detailForEditList">編集元データ</param>
        /// <param name="p_zaikoHyoukaHouhou">評価方法</param>
        private void createZaikoUkeireDetail(List<T_ZAIKO_TANK_DETAIL> p_zaikoTankDetailList, List<F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO> p_detailForEditList, int p_zaikoHyoukaHouhou)
        {
            LogUtility.DebugMethodStart(p_zaikoTankDetailList, p_detailForEditList, p_zaikoHyoukaHouhou);

            foreach (F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO tmpDetailForEdit in p_detailForEditList)
            {
                // 明細データ作成（評価単価以外計算完了）
                T_ZAIKO_TANK_DETAIL tmpZaikoTankDetail = this.createDetailWithoutTanka(tmpDetailForEdit);
                // 明細データのkey設定
                tmpZaikoTankDetail.SYSTEM_ID = tmpDetailForEdit.SYSTEM_ID;
                tmpZaikoTankDetail.ROW_NO = tmpDetailForEdit.ROW_NO;
                tmpZaikoTankDetail.ZAIKO_HINMEI_CD = tmpDetailForEdit.ZAIKO_HINMEI_CD;

                // 評価単価算出
                decimal tmpTanka = (decimal)0f;
                switch (p_zaikoHyoukaHouhou)
                {
                    case F18_G170ConstCls.ZAIKO_HYOUKA_HOUHOU_1:
                        // 評価方法：1.総平均 の場合
                        tmpTanka = this.getTanka1(tmpDetailForEdit);
                        break;
                    case F18_G170ConstCls.ZAIKO_HYOUKA_HOUHOU_2:
                        // 2.移動平均 の場合
                        tmpTanka = this.getTanka2(tmpDetailForEdit);
                        break;
                    case F18_G170ConstCls.ZAIKO_HYOUKA_HOUHOU_3:
                        // 評価方法：3.FIFO の場合
                        tmpTanka = this.getTanka3(tmpDetailForEdit);
                        break;
                    case F18_G170ConstCls.ZAIKO_HYOUKA_HOUHOU_4:
                        // 評価方法：4.最終仕入 の場合
                        tmpTanka = this.getTanka4(tmpDetailForEdit);
                        break;
                    case F18_G170ConstCls.ZAIKO_HYOUKA_HOUHOU_5:
                        // 評価方法：5.在庫基準単価 の場合
                        tmpTanka = this.getTanka5(tmpDetailForEdit.ZAIKO_HINMEI_CD);
                        break;
                    default:
                        break;
                }
                tmpZaikoTankDetail.TANKA = new SqlDecimal(tmpTanka);

                p_zaikoTankDetailList.Add(tmpZaikoTankDetail);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 在庫締明細データの生成（単価以外は計算済み）
        /// </summary>
        /// <param name="p_detailForEdit">編集元データ</param>
        /// <returns>単価以外は計算済みの在庫締明細データ</returns>
        private T_ZAIKO_TANK_DETAIL createDetailWithoutTanka(F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO p_detailForEdit)
        {
            LogUtility.DebugMethodStart(p_detailForEdit);

            // 項目初期化
            decimal tmpRemainSuu = (decimal)0f; //前月繰越残
            decimal tmpEnterSuu = (decimal)0f;  //入荷総量
            decimal tmpOutSuu = (decimal)0f;    //出荷総量
            decimal tmpAdjustSuu = (decimal)0f; //調整総量
            decimal tmpTotalSuu = (decimal)0f;  //当月在庫残

            // 前月データの処理
            if (p_detailForEdit.prewMonthInfo != null)
            {
                tmpRemainSuu = decimal.Parse(p_detailForEdit.prewMonthInfo.RET_TOTAL_SUU);//前月繰越残
            }

            // 当月データ処理
            foreach (ShimeTargetInfo tmpShimeTargetInfo in p_detailForEdit.calculationList)
            {
                switch (tmpShimeTargetInfo.RET_TARGET_FLG)
                {
                    case F18_G170ConstCls.TARGET_FLG_1:
                        // 対象データ1(受入)
                        tmpEnterSuu += tmpShimeTargetInfo.RET_JYUURYOU;//入荷総量
                        break;
                    case F18_G170ConstCls.TARGET_FLG_2:
                        // 対象データ2(出荷)
                        tmpOutSuu += tmpShimeTargetInfo.RET_JYUURYOU;//出荷総量
                        break;
                    case F18_G170ConstCls.TARGET_FLG_3:
                        // 対象データ3(在庫調整)
                        tmpAdjustSuu += tmpShimeTargetInfo.RET_JYUURYOU;//出荷総量
                        break;
                    default:
                        break;
                }

            }
            tmpTotalSuu = tmpRemainSuu + tmpEnterSuu - tmpOutSuu + tmpAdjustSuu;//当月在庫残(前月繰越残 + 入荷総量 - 出荷総量 + 調整総量)

            T_ZAIKO_TANK_DETAIL ret = new T_ZAIKO_TANK_DETAIL();
            ret.REMAIN_SUU = new SqlDecimal(tmpRemainSuu);
            ret.ENTER_SUU = new SqlDecimal(tmpEnterSuu);
            ret.OUT_SUU = new SqlDecimal(tmpOutSuu);
            ret.ADJUST_SUU = new SqlDecimal(tmpAdjustSuu);
            ret.TOTAL_SUU = new SqlDecimal(tmpTotalSuu);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #region 単価計算関連
        /// <summary>
        /// 評価方法：1.総平均 の場合、単価計算を行う
        /// </summary>
        /// <param name="p_detailForEdit">編集元データ</param>
        /// <returns>単価</returns>
        private decimal getTanka1(F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO p_detailForEdit)
        {
            LogUtility.DebugMethodStart(p_detailForEdit);

            decimal ret = (decimal)0f;

            decimal sumKingaku = (decimal)0f;
            decimal sumJyuryou = (decimal)0f;

            if (p_detailForEdit.prewMonthInfo != null)
            {
                // 前月データ存在の場合
                sumKingaku = decimal.Parse(p_detailForEdit.prewMonthInfo.RET_MULT);
                sumJyuryou = decimal.Parse(p_detailForEdit.prewMonthInfo.RET_TOTAL_SUU);
            }

            foreach (ShimeTargetInfo tmpCalculation in p_detailForEdit.calculationList)
            {
                switch (tmpCalculation.RET_TARGET_FLG)
                {
                    case F18_G170ConstCls.TARGET_FLG_1:
                        // 仕入時
                        sumJyuryou += tmpCalculation.RET_JYUURYOU;
                        sumKingaku += tmpCalculation.RET_KINGAKU;
                        break;
                    case F18_G170ConstCls.TARGET_FLG_3:
                        // 在庫調整時
                        if (tmpCalculation.RET_JYUURYOU > (decimal)0f)
                        {
                            // 在庫調は追加時(仕入と同じ扱い)
                            sumJyuryou += tmpCalculation.RET_JYUURYOU;
                            sumKingaku += tmpCalculation.RET_KINGAKU;
                        }
                        break;
                    default:
                        break;
                }
            }

            if (sumJyuryou != (decimal)0f)
            {
                ret = sumKingaku / sumJyuryou;// 総仕入合計金額／総仕入数量
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 評価方法：2.移動平均 の場合、単価計算を行う
        /// </summary>
        /// <param name="p_detailForEdit">編集元データ</param>
        /// <returns>単価</returns>
        private decimal getTanka2(F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO p_detailForEdit)
        {
            LogUtility.DebugMethodStart(p_detailForEdit);

            // 一時変数
            decimal tanka = (decimal)0f;// 単価
            decimal zaikosuu = (decimal)0f;// 在庫数
            decimal kingaku = (decimal)0f;// 金額

            // 先月データからデータ処理
            if (p_detailForEdit.prewMonthInfo != null)
            {
                // 先月情報が存在
                tanka = decimal.Parse(p_detailForEdit.prewMonthInfo.RET_TANKA);
                zaikosuu = decimal.Parse(p_detailForEdit.prewMonthInfo.RET_TOTAL_SUU);
                kingaku = tanka * zaikosuu;
            }

            // 当月データからデータ処理
            foreach (ShimeTargetInfo tmpCalculation in p_detailForEdit.calculationList)
            {
                switch (tmpCalculation.RET_TARGET_FLG)
                {
                    case F18_G170ConstCls.TARGET_FLG_1:
                        // 仕入時

                        // 金額と在庫数計算
                        kingaku += tmpCalculation.RET_KINGAKU;//金額
                        zaikosuu += tmpCalculation.RET_JYUURYOU;// 在庫数

                        // 単価を算出
                        if (zaikosuu != (decimal)0f)
                        {
                            tanka = kingaku / zaikosuu;
                        }
                        else
                        {
                            tanka = (decimal)0f;
                        }
                        break;
                    case F18_G170ConstCls.TARGET_FLG_2:
                        // 出荷時
                        // 在庫数と金額を計算(単価は前回算出結果)
                        zaikosuu -= tmpCalculation.RET_JYUURYOU;// 在庫数
                        kingaku = tanka * zaikosuu;//金額
                        break;
                    case F18_G170ConstCls.TARGET_FLG_3:
                        // 在庫調整時

                        if (tmpCalculation.RET_JYUURYOU > (decimal)0f)
                        {
                            // 在庫調整は増加時(仕入と同じ扱い)

                            // 金額と在庫数計算
                            kingaku += tmpCalculation.RET_KINGAKU;//金額
                            zaikosuu += tmpCalculation.RET_JYUURYOU;// 在庫数

                            // 単価を算出
                            if (zaikosuu != (decimal)0f)
                            {
                                tanka = kingaku / zaikosuu;
                            }
                            else
                            {
                                tanka = (decimal)0f;
                            }
                        }
                        else
                        {
                            // 在庫調整は減少時(出荷と同じ扱い)
                            zaikosuu += tmpCalculation.RET_JYUURYOU;// 在庫数 ※当数字は"-"になるので、"+"算を使う
                            kingaku = tanka * zaikosuu;//金額
                        }

                        break;
                    default:
                        break;
                }
            }

            LogUtility.DebugMethodEnd(tanka);
            return tanka;
        }

        /// <summary>
        /// 評価方法：3.FIFO の場合、単価計算を行う
        /// </summary>
        /// <param name="p_detailForEdit">編集元データ</param>
        /// <returns>単価</returns>
        private decimal getTanka3(F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO p_detailForEdit)
        {
            LogUtility.DebugMethodStart(p_detailForEdit);

            List<ShimeTargetInfo> tmpList = new List<ShimeTargetInfo>();// 計算用仕入データリスト

            // 先月データを仕入データリストに追加
            if (p_detailForEdit.prewMonthInfo != null)
            {
                ShimeTargetInfo prewData = new ShimeTargetInfo();
                prewData.RET_JYUURYOU = decimal.Parse(p_detailForEdit.prewMonthInfo.RET_TOTAL_SUU);
                prewData.RET_TANKA = decimal.Parse(p_detailForEdit.prewMonthInfo.RET_TANKA);
                prewData.RET_KINGAKU = decimal.Parse(p_detailForEdit.prewMonthInfo.RET_MULT);
                tmpList.Add(prewData);
            }

            // 当月データからデータ処理
            foreach (ShimeTargetInfo tmpCalculation in p_detailForEdit.calculationList)
            {
                switch (tmpCalculation.RET_TARGET_FLG)
                {
                    case F18_G170ConstCls.TARGET_FLG_1:
                        // 仕入時、データを仕入データリストに追加
                        tmpList.Add(tmpCalculation);
                        break;
                    case F18_G170ConstCls.TARGET_FLG_2:
                        // 出荷時、出荷データより、仕入データリストをFIFO処理
                        this.processFIFO(tmpList, tmpCalculation);
                        break;
                    case F18_G170ConstCls.TARGET_FLG_3:
                        // 在庫調整時
                        if (tmpCalculation.RET_JYUURYOU > (decimal)0f)
                        {
                            // 在庫調整は増加時(仕入と同じ扱い)、データを仕入データリストに追加
                            tmpList.Add(tmpCalculation);
                        }
                        else
                        {
                            // 在庫調整は減少時(出荷と同じ扱い)、出荷データより、仕入データリストをFIFO処理
                            this.processFIFO(tmpList, tmpCalculation);
                        }
                        break;
                    default:
                        break;
                }
            }

            decimal ret = (decimal)0f;
            decimal kingaku = (decimal)0f;
            decimal zaikoSuu = (decimal)0f;
            foreach (ShimeTargetInfo tmpShimeTargetInfo in tmpList)
            {
                kingaku += tmpShimeTargetInfo.RET_KINGAKU;
                zaikoSuu += tmpShimeTargetInfo.RET_JYUURYOU;
            }
            if (zaikoSuu != (decimal)0f)
            {
                ret = kingaku / zaikoSuu;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        /// <summary>
        /// 仕入データリストのFIFO処理を行う
        /// </summary>
        /// <param name="pList">仕入データリスト</param>
        /// <param name="syukkaData">出荷データ</param>
        private void processFIFO(List<ShimeTargetInfo> pList, ShimeTargetInfo syukkaData)
        {
            LogUtility.DebugMethodStart(pList, syukkaData);

            decimal outSuu = syukkaData.RET_JYUURYOU;
            if (outSuu < 0)
            {
                // 数量減少の在庫調整時、DB上は負数のため、正数に変換
                outSuu *= -1m;
                
            }
            bool doLoop = true;
            while (doLoop)
            {
                if (pList.Count == 0)
                {
                    break;
                }

                foreach (ShimeTargetInfo tmpInfo in pList)
                {
                    decimal minusResult = outSuu - tmpInfo.RET_JYUURYOU;
                    if (minusResult > 0)
                    {
                        // 当商品データは完全に出荷後、出荷残数はまだ0より大きいの場合
                        // データをリストから外す
                        pList.Remove(tmpInfo);
                        // 出荷数残数設定
                        outSuu = minusResult;

                        // foreachをbreakし、リストの最初から実行
                        break;
                    }
                    else
                    {
                        if (minusResult == 0)
                        {
                            // 当商品データの在庫数は出荷数と同じ場合
                            // データをリストから外す
                            pList.Remove(tmpInfo);

                        }
                        else
                        {
                            // 当商品データの在庫数は出荷数より大きい場合
                            // 当商品データの在庫数を更新
                            tmpInfo.RET_JYUURYOU = -1m * minusResult;
                            tmpInfo.RET_KINGAKU = tmpInfo.RET_JYUURYOU * tmpInfo.RET_TANKA;
                        }

                        // ループを終了
                        doLoop = false;
                        break;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 評価方法：4.最終仕入 の場合、単価計算を行う
        /// </summary>
        /// <param name="p_calculationList">編集元データ</param>
        /// <returns>単価</returns>
        private decimal getTanka4(F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO p_detailForEdit)
        {
            LogUtility.DebugMethodStart(p_detailForEdit);

            decimal ret = (decimal)0f;

            if (p_detailForEdit.prewMonthInfo != null)
            {
                // 先月データ存在の場合
                ret = decimal.Parse(p_detailForEdit.prewMonthInfo.RET_TANKA);
            }

            foreach (ShimeTargetInfo tmpCalculation in p_detailForEdit.calculationList)
            {
                switch (tmpCalculation.RET_TARGET_FLG)
                {
                    case F18_G170ConstCls.TARGET_FLG_1:
                        // 仕入時
                        ret = tmpCalculation.RET_TANKA;
                        break;
                    case F18_G170ConstCls.TARGET_FLG_3:
                        // 在庫調整時
                        if (tmpCalculation.RET_JYUURYOU > (decimal)0f)
                        {
                            // 在庫調は増加時、仕入と同じ見る
                            ret = tmpCalculation.RET_TANKA;
                        }
                        break;
                    default:
                        break;
                }
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 評価方法：5.在庫基準単価 の場合、単価計算を行う
        /// </summary>
        /// <param name="p_ZaikoHinmeiCd">在庫品名コード</param>
        /// <returns>単価</returns>
        private decimal getTanka5(string p_ZaikoHinmeiCd)
        {
            LogUtility.DebugMethodStart(p_ZaikoHinmeiCd);

            decimal ret = this.mZaikoHinmeiDao.getZaikoBaseTanka(p_ZaikoHinmeiCd).RET_ZAIKO_BASE_TANKA;

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion


        /// <summary>
        /// 指定条件より締情報を削除
        /// </summary>
        /// <param name="p_gyouShaCD">業者コード</param>
        /// <param name="p_genbaCD">現場コード</param>
        /// <param name="p_dayFrom">対象日from</param>
        /// <param name="p_dayTo">対象日to</param>
        /// <returns>締情報</returns>
        public void deleteSimeInfo(String p_gyouShaCD, String p_genbaCD, DateTime p_dayFrom, DateTime p_dayTo)
        {
            try
            {
                LogUtility.DebugMethodStart(p_gyouShaCD, p_genbaCD, p_dayFrom, p_dayTo);
                this.form.isRegistErr = false;

                // 削除条件作成
                F18_G170Dto condition = new F18_G170Dto();
                condition.gyoushaCD = p_gyouShaCD;
                condition.genbaCD = p_genbaCD;
                condition.simeTaisyouKikanFrom = p_dayFrom;
                condition.simeTaisyouKikanTo = p_dayTo;

                using (Transaction tran = new Transaction())
                {
                    // 削除　※削除順番は必ず下記となる
                    // ①≪在庫締明細 T_ZAIKO_TANK_DETAIL≫データ削除
                    // ②≪在庫締データ T_ZAIKO_TANK≫データ削除
                    this.shimeInfoDao.deleteTZaikoTankDetail(condition);
                    this.shimeInfoDao.deleteTZaikoTank(condition);

                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("deleteSimeInfo", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.form.isRegistErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// CSVファイルを出力
        /// </summary>
        /// <param name="p_simeiInfo">取得した一覧締データ</param>
        /// <param name="p_filePath">保存ファイルパス</param>
        internal bool outputCsv(DataTable p_simeiInfo, string p_filePath)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(p_simeiInfo, p_filePath);

                // 出力用データ情報を作成
                ArrayList editedData = this.editDataForOutput(p_simeiInfo, false);

                // CSVファイル出力
                // ※ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                using (StreamWriter streamWriter = new StreamWriter(p_filePath, false, System.Text.Encoding.GetEncoding(0)))
                {
                    // 行毎で出力
                    foreach (Dictionary<String, String> tmpEditedRowData in editedData)
                    {
                        StringBuilder tmpLineData = new StringBuilder("");
                        tmpLineData.Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_NAME_RYAKU])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_CD])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_RYAKU])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_REMAIN_SUU])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_ENTER_SUU])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_OUT_SUU])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_ADJUST_SUU])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_TOTAL_SUU])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_TANKA])
                                    .Append(F18_G170ConstCls.COMMA)
                                    .Append(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_MULT]);

                        // ファイルの書き込み
                        streamWriter.WriteLine(tmpLineData.ToString());
                    }

                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("outputCsv", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 帳票を出力
        /// </summary>
        /// <param name="p_slipCondition">帳票出力の条件部情報</param>
        /// <param name="p_slipDetail">帳票出力の一覧部情報</param>
        public void printSlip(F18_G170_SLIP_CONDITION_Dto p_slipCondition, DataTable p_slipDetail)
        {
            try
            {
                LogUtility.DebugMethodStart(p_slipCondition, p_slipDetail);

                ReportInfoR405 reportInfo = this.createReportInfo(p_slipCondition, p_slipDetail);
                reportInfo.Create(@".\Template\R405-Form.xml", "LAYOUT1", new DataTable());

                using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(reportInfo, WINDOW_ID.R_ZAIKO_KANNRIHYOU))
                {
                    formReportPrintPopup.ShowDialog();
                    formReportPrintPopup.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("printSlip", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 帳票を出力情報インスタンス作成
        /// </summary>
        /// <param name="p_slipCondition">帳票出力の条件部情報</param>
        /// <param name="p_slipDetail">帳票出力の一覧部情報</param>
        private ReportInfoR405 createReportInfo(F18_G170_SLIP_CONDITION_Dto p_slipCondition, DataTable p_slipDetail)
        {

            ReportInfoR405 reportInfo = new ReportInfoR405(WINDOW_ID.R_ZAIKO_KANNRIHYOU);

            // 画面のデータをReportInfoのthis.DataTablePageListに設定

            // 出力データを作成
            string strKey = "G170_R405";
            #region １．帳票ヘッダ部処理
            DataTable head_DataTable = new DataTable();
            // 会社名
            head_DataTable.Columns.Add("CORP_RYAKU_NAME");
            // 発行日
            head_DataTable.Columns.Add("PRINT_DATE");
            // 対象期間範囲
            head_DataTable.Columns.Add("UKETSUKE_DATE");
            // 業者コード
            head_DataTable.Columns.Add("GYOUSHA_CD");
            // 現場コード
            head_DataTable.Columns.Add("GENBA_CD");
            // 評価方法
            head_DataTable.Columns.Add("ZAIKO_ASSESSMENT");

            DataRow head_DataRow = head_DataTable.NewRow();
            // 会社名
            M_CORP_INFO condition = new M_CORP_INFO();
            condition.SYS_ID = 0;
            head_DataRow["CORP_RYAKU_NAME"] = this.iM_CORP_INFODao.GetAllValidData(condition)[0].CORP_RYAKU_NAME;
            // 発行日
            head_DataRow["PRINT_DATE"] = DateTime.Today.ToString("yyyy/MM/dd HH:mm:ss");
            // 対象期間範囲
            head_DataRow["UKETSUKE_DATE"] = p_slipCondition.simeTaisyouKikanFrom.ToString("yyyy/MM/dd") + "～" + p_slipCondition.simeTaisyouKikanTo.ToString("yyyy/MM/dd");
            // 業者コード
            head_DataRow["GYOUSHA_CD"] = p_slipCondition.gyoushaCD;
            // 現場コード
            head_DataRow["GENBA_CD"] = p_slipCondition.genbaCD;
            // 評価方法
            head_DataRow["ZAIKO_ASSESSMENT"] = p_slipCondition.hyoukaHouhou;

            head_DataTable.Rows.Add(head_DataRow);
            reportInfo.DataTablePageList[strKey] = new Dictionary<string, DataTable>();
            reportInfo.DataTablePageList[strKey].Add("Header", head_DataTable);
            #endregion

            #region ２．帳票明細部処理
            // 明細一覧を編集
            DataTable detail_DataTable = new DataTable();
            detail_DataTable.TableName = "Detail";
            // 現場コード
            detail_DataTable.Columns.Add("D_GENBA_CD");
            // 現場名
            detail_DataTable.Columns.Add("D_GENBA_MEI");
            // 在庫品名CD
            detail_DataTable.Columns.Add("ZAIKO_HINMEI_CD");
            // 在庫品名
            detail_DataTable.Columns.Add("ZAIKO_HINMEI");
            // 前月残量
            detail_DataTable.Columns.Add("REMAIN_SUU");
            // 当月受入量
            detail_DataTable.Columns.Add("ENTER_SUU");
            // 当月出荷量
            detail_DataTable.Columns.Add("OUT_SUU");
            // 調整量
            detail_DataTable.Columns.Add("ADJUST_SUU");
            // 当月在庫残
            detail_DataTable.Columns.Add("TOTAL_SUU");
            // 評価単価
            detail_DataTable.Columns.Add("TANKA");
            // 在庫金額
            detail_DataTable.Columns.Add("KINGAKU");

            DataRow detail_DataRow = null;
            ArrayList editedDetailData = this.editDataForOutput(p_slipDetail, true);
            decimal REMAIN_SUU = 0m;
            decimal ENTER_SUU = 0m;
            decimal OUT_SUU = 0m;
            decimal ADJUST_SUU = 0m;
            decimal TOTAL_SUU = 0m;
            //float TANKA = 0f;
            decimal KINGAKU = 0m;
            foreach (Dictionary<String, String> tmpEditedRowData in editedDetailData)
            {
                detail_DataRow = detail_DataTable.NewRow();
                // 現場コード
                detail_DataRow["D_GENBA_CD"] = tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD];
                // 現場名
                detail_DataRow["D_GENBA_MEI"] = tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_NAME_RYAKU];
                // 在庫品名CD
                detail_DataRow["ZAIKO_HINMEI_CD"] = tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_CD];
                // 在庫品名
                detail_DataRow["ZAIKO_HINMEI"] = tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_RYAKU];
                // 前月残量
                decimal tmp_REMAIN_SUU = decimal.Parse(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_REMAIN_SUU]);
                detail_DataRow["REMAIN_SUU"] = tmp_REMAIN_SUU.ToString("n0");
                REMAIN_SUU += tmp_REMAIN_SUU;
                // 当月受入量
                decimal tmp_ENTER_SUU = decimal.Parse(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_ENTER_SUU]);
                detail_DataRow["ENTER_SUU"] = tmp_ENTER_SUU.ToString("n0");
                ENTER_SUU += tmp_ENTER_SUU;
                // 当月出荷量
                decimal tmp_OUT_SUU = decimal.Parse(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_OUT_SUU]);
                detail_DataRow["OUT_SUU"] = tmp_OUT_SUU.ToString("n0");
                OUT_SUU += tmp_OUT_SUU;
                // 調整量
                decimal tmp_ADJUST_SUU = decimal.Parse(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_ADJUST_SUU]);
                detail_DataRow["ADJUST_SUU"] = tmp_ADJUST_SUU.ToString("n0");
                ADJUST_SUU += tmp_ADJUST_SUU;
                // 当月在庫残
                decimal tmp_TOTAL_SUU = decimal.Parse(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_TOTAL_SUU]);
                detail_DataRow["TOTAL_SUU"] = tmp_TOTAL_SUU.ToString("n0");
                TOTAL_SUU += tmp_TOTAL_SUU;
                // 評価単価
                decimal tmp_TANKA = decimal.Parse(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_TANKA]);
                detail_DataRow["TANKA"] = tmp_TANKA.ToString("n2");
                //TANKA += tmp_TANKA;
                // 在庫金額
                decimal tmp_KINGAKU = decimal.Parse(tmpEditedRowData[F18_G170ConstCls.OUTPUT_COLUMN_NAME_MULT]);
                detail_DataRow["KINGAKU"] = tmp_KINGAKU.ToString("n2");
                KINGAKU += tmp_KINGAKU;

                detail_DataTable.Rows.Add(detail_DataRow);
            }
            reportInfo.DataTablePageList[strKey].Add("Detail", detail_DataTable);
            #endregion

            // 帳票Footer部処理
            #region ３．帳票Footer部処理
            DataTable footer_DataTable = new DataTable();
            footer_DataTable.TableName = "GroupFooter";
            // 前月残量合計値
            footer_DataTable.Columns.Add("ALL_REMAIN_SUU");
            // 当月受入量計値
            footer_DataTable.Columns.Add("ALL_ENTER_SUU");
            // 当月出荷量計値
            footer_DataTable.Columns.Add("ALL_OUT_SUU");
            // 調整量計値
            footer_DataTable.Columns.Add("ALL_ADJUST_SUU");
            // 当月在庫残計値
            footer_DataTable.Columns.Add("ALL_TOTAL_SUU");
            // 在庫金額計値
            footer_DataTable.Columns.Add("ALL_KINGAKU");

            DataRow footer_DataRow = footer_DataTable.NewRow();
            // 前月残量合計値
            footer_DataRow["ALL_REMAIN_SUU"] = REMAIN_SUU.ToString("n3");
            // 当月受入量計値
            footer_DataRow["ALL_ENTER_SUU"] = ENTER_SUU.ToString("n3");
            // 当月出荷量計値
            footer_DataRow["ALL_OUT_SUU"] = OUT_SUU.ToString("n3");
            // 調整量計値
            footer_DataRow["ALL_ADJUST_SUU"] = ADJUST_SUU.ToString("n3");
            // 当月在庫残計値
            footer_DataRow["ALL_TOTAL_SUU"] = TOTAL_SUU.ToString("n3");
            // 在庫金額計値
            footer_DataRow["ALL_KINGAKU"] = KINGAKU.ToString("n3");

            footer_DataTable.Rows.Add(footer_DataRow);
            reportInfo.DataTablePageList[strKey].Add("GroupFooter", footer_DataTable);
            #endregion

            return reportInfo;
        }

        /// <summary>
        /// 取得した一覧締データより、出力用データ(CSVファイルと帳票)を取得
        /// </summary>
        /// <param name="p_simeiInfo">取得した一覧締データ</param>
        /// <param name="omitFlg">省略フラグ（trueの場合、同じ業者と現場情報を出力しない）</param>
        private ArrayList editDataForOutput(DataTable p_simeiInfo, bool omitFlg)
        {
            LogUtility.DebugMethodStart(p_simeiInfo, omitFlg);

            // 出力用データ情報を作成
            ArrayList editedData = new ArrayList();
            String tmp_gyoushaCD = "";
            String tmp_genbaCD = "";
            for (int j = 0; (p_simeiInfo != null) && (j < p_simeiInfo.Rows.Count) && (p_simeiInfo.Rows[j] != null); j++)
            {
                // データ取得
                Dictionary<String, String> editedRowData = new Dictionary<String, String>();
                if (tmp_gyoushaCD.Equals(p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD].ToString())
                    && tmp_genbaCD.Equals(p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD].ToString())
                    && omitFlg)
                {
                    // omitFlgはtrueの場合、同じ業者と現場情報を出力しない
                    editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD, "");
                    editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD, "");
                    editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_NAME_RYAKU, "");
                }
                else
                {
                    tmp_gyoushaCD = p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD].ToString();
                    tmp_genbaCD = p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD].ToString();
                    editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_GYOUSHA_CD, tmp_gyoushaCD);
                    editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_CD, tmp_genbaCD);
                    editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_NAME_RYAKU, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_GENBA_NAME_RYAKU].ToString());
                }
                editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_CD, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_CD].ToString());
                editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_RYAKU, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ZAIKO_HINMEI_RYAKU].ToString());
                editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_REMAIN_SUU, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_REMAIN_SUU].ToString());
                editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_ENTER_SUU, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ENTER_SUU].ToString());
                editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_OUT_SUU, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_OUT_SUU].ToString());
                editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_ADJUST_SUU, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_ADJUST_SUU].ToString());
                editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_TOTAL_SUU, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_TOTAL_SUU].ToString());
                editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_TANKA, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_TANKA].ToString());
                editedRowData.Add(F18_G170ConstCls.OUTPUT_COLUMN_NAME_MULT, p_simeiInfo.Rows[j][F18_G170ConstCls.OUTPUT_COLUMN_NAME_MULT].ToString());
                editedData.Add(editedRowData);
            }

            LogUtility.DebugMethodEnd(editedData);
            return editedData;
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

        #region 利用なしのoverrideメソッド
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        int IBuisinessLogic.Search()
        {
            throw new NotImplementedException();
        }

        void IBuisinessLogic.Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        void IBuisinessLogic.Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        void IBuisinessLogic.LogicalDelete()
        {
            throw new NotImplementedException();
        }

        void IBuisinessLogic.PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
