using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Utility;
using r_framework.Dao;
using r_framework.Entity;
using Shougun.Core.Common.ContenaShitei.Const;
using Shougun.Core.Common.BusinessCommon.Const;
using r_framework.Logic;
using System.Windows.Forms;
using Shougun.Core.Common.ContenaShitei.DAO;
using Shougun.Core.Common.ContenaShitei.DTO;
using r_framework.Const;

namespace Shougun.Core.Common.ContenaShitei.Utility
{
    /// <summary>
    /// コンテナ指定で使用するUtility
    /// 他画面からの呼び出し可。
    /// 2014/09/03現在、想定している呼び出し元は
    /// コンテナ指定ポップアップ、受付収集入力、受入入力、売上支払入力
    /// </summary>
    public class ContenaShiteiUtility
    {
        /// <summary>
        /// 指定された日付を元に既に設置されているコンテナか判定。
        /// 実績情報の取得はコンテナ履歴のSQLと同等にしておく必要がある。
        /// </summary>
        /// <param name="contenaShuruiCd">コンテナ種類CD</param>
        /// <param name="contenaCd">コンテナCD</param>
        /// <param name="denpyouInfoList"></param>
        /// <returns>true:設置済、false:未設置</returns>
        public bool IsPutContena(string contenaShuruiCd, string contenaCd, List<UtilityDto> denpyouInfoList)
        {
            bool returnVal = false;

            if (string.IsNullOrEmpty(contenaShuruiCd)
                || string.IsNullOrEmpty(contenaCd))
            {
                return returnVal;
            }

            // 実績情報を取得
            var checkContenaDao = DaoInitUtility.GetComponent<CheckCONRETDaoCls>();
            var condition = new SearchConditionDto();
            condition.CONTENA_SHURUI_CD = contenaShuruiCd;
            condition.CONTENA_CD = contenaCd;
            var jissekiDataList = checkContenaDao.GetJissekiContenaDataSql(condition);

            // ヒットしなければ未設置
            if (jissekiDataList == null || jissekiDataList.Count < 1)
            {
                return false;
            }

            /**
             * 設置 -> 引揚の情報を打ち消して、設置のデータだけを抽出
             */
            var hikiageDataList =
                jissekiDataList.AsEnumerable().Where(w => w.CONTENA_SET_KBN == 2 && w.SECCHI_DATE != null)
                .OrderBy(o => o.SECCHI_DATE).ToArray();

            foreach (var hikiageData in hikiageDataList)
            {
                var secchiDataList =
                    jissekiDataList.AsEnumerable().Where(w => w.CONTENA_SET_KBN == 1
                    && w.SECCHI_DATE != null
                    && Convert.ToDateTime(w.SECCHI_DATE) <= (Convert.ToDateTime(hikiageData.SECCHI_DATE)))
                    .OrderByDescending(o => o.SECCHI_DATE);

                if (secchiDataList != null
                    && secchiDataList.Count() > 0)
                {
                    jissekiDataList.Remove(secchiDataList.First());
                }

                jissekiDataList.Remove(hikiageData);
            }

            /**
             * 自伝票のデータを除外
             */
            if (jissekiDataList.Count > 0
                && denpyouInfoList != null)
            {
                var tempSecchiDataList =
                    jissekiDataList.AsEnumerable().Where(w => w.CONTENA_SET_KBN == 1).ToArray();

                if (tempSecchiDataList != null
                    && tempSecchiDataList.Count() > 0)
                {
                    foreach(var checkTarget in tempSecchiDataList)
                    {
                        foreach (var denpyouInfo in denpyouInfoList)
                        {
                            if (checkTarget.SYSTEM_ID == denpyouInfo.SysId
                                && checkTarget.SEQ == denpyouInfo.Seq
                                && (int)checkTarget.DENSHU_KBN_CD == denpyouInfo.DenshuKbn)
                            {
                                jissekiDataList.Remove(checkTarget);
                            }
                        }
                    }
                }
            }

            /**
             * 設置が残ってるかチェック
             */
            if (jissekiDataList.Count > 0)
            {
                var secchiDataList =
                    jissekiDataList.AsEnumerable().Where(w => w.CONTENA_SET_KBN == 1);

                if (secchiDataList != null
                    && secchiDataList.Count() > 0)
                {
                    returnVal = true;
                }
            }

            return returnVal;
        }

        /// <summary>
        /// 指定された日付を元に設置(または引揚)可能なコンテナか判定。
        /// dateを指定しなかった場合は操作可能と判断する。
        /// コンテナマスタのデータがおかしな状態にある場合は
        /// 「操作可能」と判断します。
        /// </summary>
        /// <param name="contenaShuruiCd">コンテナ種類CD</param>
        /// <param name="contenaCd">コンテナCD</param>
        /// <param name="conteaSetKbn">コンテナの操作(2:設置、3:引揚)</param>
        /// <param name="date">操作したい日付</param>
        /// <returns>true:操作不可、false:操作可能</returns>
        public bool IsCannotPutAndTakeContena(string contenaShuruiCd, string contenaCd, int conteaSetKbn, DateTime date)
        {
            bool returnVal = false;

            if (date == null)
            {
                // 日付が無いと判定が出来ないため、一律、「設置可能」と判断
                return returnVal;
            }

            var contenaDao = DaoInitUtility.GetComponent<IM_CONTENADao>();
            var conditionEntry = new M_CONTENA();
            conditionEntry.CONTENA_SHURUI_CD = contenaShuruiCd;
            conditionEntry.CONTENA_CD = contenaCd;
            var targetContena = contenaDao.GetDataByCd(conditionEntry);

            if (CommonConst.CONTENA_SET_KBN_SECCHI == conteaSetKbn)
                    // コンテナを設置する場合
                    if (targetContena.HIKIAGE_DATE.IsNull)
                    {
                        // 引揚日が無い場合、設置状態。
                        // 設置状態の場合は再設置可能とするので「設置可能」と判断する。
                        returnVal = false;
                    }
                    else
                    {
                        if (targetContena.SECCHI_DATE.IsNull)
                        {
                            // 設置日が指定されていなければ、もちろん「設置可能」
                            returnVal = false;
                        }
                        else if ((targetContena.SECCHI_DATE <= date)
                            && (date < targetContena.HIKIAGE_DATE))
                        {
                            // 設置日 ≦ 設置したい日付 < 引揚日はおかしな状態なので「設置不可」
                            returnVal = true;
                        }
                        else
                        {
                            returnVal = false;
                        }
                    }
            else if (CommonConst.CONTENA_SET_KBN_HIKIAGE == conteaSetKbn)
            {
                    // コンテナを引き上げる場合
                    if (!targetContena.SECCHI_DATE.IsNull
                        && !targetContena.HIKIAGE_DATE.IsNull)
                    {
                        // 引揚の場合は設置日 ≦ 操作する日付 ≦ 引揚日のチェックだけをする
                        if ((targetContena.SECCHI_DATE <= date)
                            && (date <= targetContena.HIKIAGE_DATE))
                        {
                            returnVal = true;
                        }
                        else
                        {
                            returnVal = false;
                        }
                    }

            }

            return returnVal;
        }

        #region 登録時チェック用ロジック
        /// <summary>
        /// コンテナマスタの情報をチェックする
        /// エラーメッセージは当ロジック内で表示する。
        /// </summary>
        /// <param name="contenaResultList">チェック対象のT_CONTENA_RESULT</param>
        /// <param name="gyousyaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="denpyouInfoList"></param>
        /// <returns>true:正常、false:異常</returns>
        public bool CheckContenaInfo(List<T_CONTENA_RESULT> contenaResultList, string gyousyaCd, string genbaCd, List<UtilityDto> denpyouInfoList)
        {
            // 設置してよいか確認用情報
            string putContenaInfo = string.Empty;

            foreach (T_CONTENA_RESULT contenaRes in contenaResultList)
            {
                if (string.IsNullOrEmpty(contenaRes.CONTENA_SHURUI_CD)
                    || string.IsNullOrEmpty(contenaRes.CONTENA_CD))
                {
                    // 上記項目が無いとチェックができないため何もしない
                    continue;
                }

                if (CommonConst.CONTENA_SET_KBN_SECCHI == (int)contenaRes.CONTENA_SET_KBN
                    && this.IsPutContena(contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD, denpyouInfoList))
                {
                    putContenaInfo += string.Format("\nコンテナ種類CD:{0}, コンテナCD:{1}", contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD);
                    continue;
                }

            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrEmpty(putContenaInfo))
            {
                // 他の現場で設置されているコンテナ
                if (DialogResult.Yes == msgLogic.MessageBoxShow("C078", "以下のコンテナ", putContenaInfo))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;

        }

        /// <summary>
        /// コンテナマスタの情報をチェックする
        /// エラーメッセージは当ロジック内で表示する。
        /// </summary>
        /// <param name="contenaResultList">チェック対象のT_CONTENA_RESULT</param>
        /// <param name="gyousyaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="denpyouInfoList"></param>
        /// <returns>true:正常、false:異常</returns>
        public bool CheckContenaInfo(List<T_CONTENA_RESERVE> contenaResultList, string gyousyaCd, string genbaCd, List<UtilityDto> denpyouInfoList)
        {
            // 設置してよいか確認用情報
            string putContenaInfo = string.Empty;

            foreach (T_CONTENA_RESERVE contenaRes in contenaResultList)
            {
                if (string.IsNullOrEmpty(contenaRes.CONTENA_SHURUI_CD)
                    || string.IsNullOrEmpty(contenaRes.CONTENA_CD))
                {
                    // 上記項目が無いとチェックができないため何もしない
                    return true;
                }

                if (CommonConst.CONTENA_SET_KBN_SECCHI == (int)contenaRes.CONTENA_SET_KBN
                    && this.IsPutContena(contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD, denpyouInfoList))
                {
                    putContenaInfo += string.Format("\nコンテナ種類CD:{0}, コンテナCD:{1}", contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD);
                    continue;
                }

            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            
            if (!string.IsNullOrEmpty(putContenaInfo))
            {
                // 他の現場で設置されているコンテナ
                if (DialogResult.Yes == msgLogic.MessageBoxShow("C078", "以下のコンテナ", putContenaInfo))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;

        }

        /// <summary>
        /// 指定された日付、業者CD、現場CDを元に既にその現場に設置されているか判定。
        /// 指定された日付より前の設置はOKとし、指定された日付以降の場合はNGとする。
        /// </summary>
        /// <param name="contenaShuruiCd">コンテナ種類CD</param>
        /// <param name="contenaCd">コンテナCD</param>
        /// <param name="secchiDate">設置したい日付</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>true:設置済、false:未設置</returns>
        public bool IsInstalledGenba(string contenaShuruiCd, string contenaCd, DateTime secchiDate, string gyoushaCd, string genbaCd, long sysId, int seq, int denshuKbn)
        {
            bool returnVal = false;

            // 必要な情報がない場合、判定できないのでfalse(未設置)で返す
            if (string.IsNullOrEmpty(contenaShuruiCd)
                || string.IsNullOrEmpty(contenaCd)
                || string.IsNullOrEmpty(gyoushaCd)
                || string.IsNullOrEmpty(genbaCd))
            {
                return false;
            }

            // データ取得
            var contenaDao = DaoInitUtility.GetComponent<CheckCONRETDaoCls>();
            var condition = new SearchConditionDto();
            condition.CONTENA_SHURUI_CD = contenaShuruiCd;
            condition.CONTENA_CD = contenaCd;
            condition.GYOUSHA_CD = gyoushaCd;
            condition.GENBA_CD = genbaCd;
            var jissekiDataList = contenaDao.GetJissekiContenaDataSql(condition);

            // ヒットしなければ設置OK
            if (jissekiDataList == null || jissekiDataList.Count < 1)
            {
                return false;
            }

            // 設置 -> 引揚の情報を打ち消して、設置のデータだけを抽出
            var hikiageDataList =
                jissekiDataList.AsEnumerable().Where(w => w.CONTENA_SET_KBN == 2 && w.SECCHI_DATE != null)
                .OrderBy(o => o.SECCHI_DATE).ToArray();

            foreach (var hikiageData in hikiageDataList)
            {
                var secchiDataList =
                    jissekiDataList.AsEnumerable().Where(w => w.CONTENA_SET_KBN == 1
                    && w.SECCHI_DATE != null
                    && Convert.ToDateTime(w.SECCHI_DATE) <= (Convert.ToDateTime(hikiageData.SECCHI_DATE)))
                    .OrderByDescending(o => o.SECCHI_DATE);

                if (secchiDataList != null
                    && secchiDataList.Count() > 0)
                {
                    jissekiDataList.Remove(secchiDataList.First());
                }

                jissekiDataList.Remove(hikiageData);
            }

            if (jissekiDataList.Count > 0)
            {
                // 一件以上設置データが存在し、設置しようとしている前の日付だった場合は設置済みと判断する
                foreach(var jissekiData in jissekiDataList)
                {
                    DateTime tempSecchiDate = DateTime.Now;
                    if (DateTime.TryParse(jissekiData.SECCHI_DATE, out tempSecchiDate))
                    {
                        if (tempSecchiDate.Date <= secchiDate.Date)
                        {
                            if (sysId != jissekiData.SYSTEM_ID
                                || seq != jissekiData.SEQ
                                || denshuKbn != (int)jissekiData.DENSHU_KBN_CD)
                            {
                                returnVal = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return returnVal;
        }
        #endregion

        #region 設置データ削除用チェックロジック
        /// <summary>
        /// 指定された日付、業者CD、現場CDを元に既にその現場に設置→引揚がされているか判定。
        /// エラーメッセージは当ロジック内で表示する。
        /// </summary>
        /// <param name="contenaResultList">チェック対象のT_CONTENA_RESULT</param>
        /// <param name="denpyouDate">設置または引揚しようとしている伝票日付</param>
        /// <param name="gyousyaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="message">メッセージ</param>
        /// <returns>true:正常、false:異常</returns>
        public bool CheckFixedContena(List<T_CONTENA_RESULT> contenaResultList, string denpyouDate, string gyousyaCd, string genbaCd, string message)
        {
            // 設置解除が出来ないコンテナ
            string cannotDeleteContenaInfo = string.Empty;

            foreach (T_CONTENA_RESULT contenaRes in contenaResultList)
            {
                if (string.IsNullOrEmpty(contenaRes.CONTENA_SHURUI_CD)
                    || string.IsNullOrEmpty(contenaRes.CONTENA_CD)
                    || string.IsNullOrEmpty(denpyouDate))
                {
                    // 上記項目が無いとチェックができないため何もしない
                    continue;
                }

                DateTime date = Convert.ToDateTime(denpyouDate);
                if (CommonConst.CONTENA_SET_KBN_SECCHI == (int)contenaRes.CONTENA_SET_KBN
                    && this.IsFixedContenaData(contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD, date.Date, gyousyaCd, genbaCd))
                {
                    cannotDeleteContenaInfo += string.Format("\nコンテナ種類CD:{0}, コンテナCD:{1}", contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD);
                    continue;
                }

            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrEmpty(cannotDeleteContenaInfo))
            {
                // 既に設置～引揚が完了しているコンテナ
                msgLogic.MessageBoxShow("E198", "以下のコンテナ", message, cannotDeleteContenaInfo);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 指定された日付、業者CD、現場CDを元に既にその現場に設置→引揚がされているか判定。
        /// エラーメッセージは当ロジック内で表示する。
        /// </summary>
        /// <param name="contenaResultList">チェック対象のT_CONTENA_RESULT</param>
        /// <param name="denpyouDate">設置または引揚しようとしている伝票日付</param>
        /// <param name="gyousyaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// /// <param name="message">メッセージ</param>
        /// <returns>true:正常、false:異常</returns>
        public bool CheckFixedContena(List<T_CONTENA_RESERVE> contenaResultList, string denpyouDate, string gyousyaCd, string genbaCd, string message)
        {
            // 設置解除が出来ないコンテナ
            string cannotDeleteContenaInfo = string.Empty;

            foreach (T_CONTENA_RESERVE contenaRes in contenaResultList)
            {
                if (string.IsNullOrEmpty(contenaRes.CONTENA_SHURUI_CD)
                    || string.IsNullOrEmpty(contenaRes.CONTENA_CD)
                    || string.IsNullOrEmpty(denpyouDate))
                {
                    // 上記項目が無いとチェックができないため何もしない
                    continue;
                }

                DateTime date = Convert.ToDateTime(denpyouDate);
                if (CommonConst.CONTENA_SET_KBN_SECCHI == (int)contenaRes.CONTENA_SET_KBN
                    && this.IsFixedContenaData(contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD, date.Date, gyousyaCd, genbaCd))
                {
                    cannotDeleteContenaInfo += string.Format("\nコンテナ種類CD:{0}, コンテナCD:{1}", contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD);
                    continue;
                }

            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrEmpty(cannotDeleteContenaInfo))
            {
                // 既に設置～引揚が完了しているコンテナ
                msgLogic.MessageBoxShow("E198", "以下のコンテナ", message, cannotDeleteContenaInfo);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 指定された日付、業者CD、現場CDを元に既にその現場に設置→引揚がされているか判定。
        /// </summary>
        /// <param name="contenaShuruiCd">コンテナ種類CD</param>
        /// <param name="contenaCd">コンテナCD</param>
        /// <param name="secchiDate">設置したい日付</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>true:設置→引揚済、false:それ以外</returns>
        public bool IsFixedContenaData(string contenaShuruiCd, string contenaCd, DateTime secchiDate, string gyoushaCd, string genbaCd)
        {
            bool returnVal = false;

            // 必要な情報がない場合、判定できないのでfalseで返す
            if (string.IsNullOrEmpty(contenaShuruiCd)
                || string.IsNullOrEmpty(contenaCd)
                || string.IsNullOrEmpty(gyoushaCd)
                || string.IsNullOrEmpty(genbaCd))
            {
                return false;
            }

            /**
             * コンテナマスタ取得し、設置→引揚期間の操作かをチェック
             */
            var contenaMasterDao = DaoInitUtility.GetComponent<IM_CONTENADao>();
            var conditionEntry = new M_CONTENA();
            conditionEntry.CONTENA_SHURUI_CD = contenaShuruiCd;
            conditionEntry.CONTENA_CD = contenaCd;
            var targetContena = contenaMasterDao.GetDataByCd(conditionEntry);

            if (targetContena.SECCHI_DATE.IsNull
                || targetContena.HIKIAGE_DATE.IsNull)
            {
                // 設置か引揚がない状態であればおかしな状態なのでretrun
                return false;
            }

            if (!(targetContena.SECCHI_DATE <= secchiDate
                && secchiDate <= targetContena.HIKIAGE_DATE))
            {
                // コンテナマスタの設置→引揚の期間外の場合はチェックが不可能と判断されるため
                // falseで返す
                return false;
            }

            /**
             * コンテナマスタと実績データを使ってチェック
             */
            var contenaDao = DaoInitUtility.GetComponent<CheckCONRETDaoCls>();
            var condition = new SearchConditionDto();
            condition.CONTENA_SHURUI_CD = contenaShuruiCd;
            condition.CONTENA_CD = contenaCd;
            condition.GYOUSHA_CD = gyoushaCd;
            condition.GENBA_CD = genbaCd;
            var jissekiDataList = contenaDao.GetJissekiContenaDataSql(condition);

            // ヒットしなければ設置→引揚されていない
            if (jissekiDataList == null || jissekiDataList.Count < 1)
            {
                return false;
            }

            // ペアになる設置→引揚のデータを抽出
            var hikiageDataList =
                jissekiDataList.AsEnumerable().Where(w => w.CONTENA_SET_KBN == 2 && w.SECCHI_DATE != null && Convert.ToDateTime(w.SECCHI_DATE).Date == (DateTime)targetContena.HIKIAGE_DATE)
                .OrderBy(o => o.SECCHI_DATE).ToArray();

            var secchiDataList =
                jissekiDataList.AsEnumerable().Where(w => w.CONTENA_SET_KBN == 1
                && w.SECCHI_DATE != null
                && Convert.ToDateTime(w.SECCHI_DATE) == (DateTime)targetContena.SECCHI_DATE)
                .OrderByDescending(o => o.SECCHI_DATE);

            if ((hikiageDataList != null && hikiageDataList.Count() > 0)
                && (secchiDataList != null && secchiDataList.Count() > 0))
            {
                // 設置→引揚の実績データが存在している
                returnVal = true;
            }

            return returnVal;
        }
        #endregion

        #region コンテナマスタに登録されていた可能性があるデータかチェック
        /// <summary>
        /// 指定された日付、業者CD、現場CDを元にコンテナマスタに登録されている日付のデータかチェック。
        /// </summary>
        /// <param name="contenaResultList">チェック対象のT_CONTENA_RESULT</param>
        /// <param name="denpyouDate">設置または引揚しようとしている伝票日付</param>
        /// <param name="checkTarget">ユーザーに確認を促すコンテナリスト</param>
        /// <returns>true:正常、false:異常</returns>
        public bool CheckLatestContena(List<T_CONTENA_RESULT> contenaResultList, string denpyouDate, out List<T_CONTENA_RESULT> checkTarget)
        {
            checkTarget = new List<T_CONTENA_RESULT>();
            // 設置解除が出来ないコンテナ
            string dispContenaInfo = string.Empty;

            foreach (T_CONTENA_RESULT contenaRes in contenaResultList)
            {
                if (string.IsNullOrEmpty(contenaRes.CONTENA_SHURUI_CD)
                    || string.IsNullOrEmpty(contenaRes.CONTENA_CD)
                    || string.IsNullOrEmpty(denpyouDate))
                {
                    // 上記項目が無いとチェックができないため何もしない
                    continue;
                }

                DateTime date = Convert.ToDateTime(denpyouDate);
                if (this.IsLatestContenaData(contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD, date.Date, (int)contenaRes.CONTENA_SET_KBN))
                {
                    checkTarget.Add(contenaRes);
                    continue;
                }

            }

            if (checkTarget.Count > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 指定された日付、業者CD、現場CDを元にコンテナマスタに登録されている日付のデータかチェック。
        /// </summary>
        /// <param name="contenaResultList">チェック対象のT_CONTENA_RESULT</param>
        /// <param name="denpyouDate">設置または引揚しようとしている伝票日付</param>
        /// <param name="checkTarget">ユーザーに確認を促すコンテナリスト</param>
        /// <returns>true:正常、false:異常</returns>
        public bool CheckLatestContena(List<T_CONTENA_RESERVE> contenaResultList, string denpyouDate, out List<T_CONTENA_RESERVE> checkTarget)
        {
            checkTarget = new List<T_CONTENA_RESERVE>();
            // 設置解除が出来ないコンテナ
            string dispContenaInfo = string.Empty;

            foreach (T_CONTENA_RESERVE contenaRes in contenaResultList)
            {
                if (string.IsNullOrEmpty(contenaRes.CONTENA_SHURUI_CD)
                    || string.IsNullOrEmpty(contenaRes.CONTENA_CD)
                    || string.IsNullOrEmpty(denpyouDate))
                {
                    // 上記項目が無いとチェックができないため何もしない
                    continue;
                }

                DateTime date = Convert.ToDateTime(denpyouDate);
                if (this.IsLatestContenaData(contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD, date.Date, (int)contenaRes.CONTENA_SET_KBN))
                {
                    checkTarget.Add(contenaRes);
                    continue;
                }

            }

            if (checkTarget.Count > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 指定された日付、業者CD、現場CDを元にコンテナマスタに登録されている日付のデータかチェック。
        /// </summary>
        /// <param name="contenaShuruiCd">コンテナ種類CD</param>
        /// <param name="contenaCd">コンテナCD</param>
        /// <param name="secchiDate">設置したい日付</param>
        /// <param name="setKbn">設置区分</param>
        /// <returns>true:コンテナマスタに登録されているデータ、false:それ以外</returns>
        public bool IsLatestContenaData(string contenaShuruiCd, string contenaCd, DateTime secchiDate, int setKbn)
        {
            bool returnVal = false;

            // 必要な情報がない場合、判定できないのでfalseで返す
            if (string.IsNullOrEmpty(contenaShuruiCd)
                || string.IsNullOrEmpty(contenaCd))
            {
                return false;
            }

            /**
             * コンテナマスタ取得し、コンテナマスタに登録されているデータか確認
             * ※正確なチェックは出来ず、日付に頼ったチェックになるため、正確には「コンテナマスタに登録されている可能性があるデータ」
             */
            var contenaMasterDao = DaoInitUtility.GetComponent<IM_CONTENADao>();
            var conditionEntry = new M_CONTENA();
            conditionEntry.CONTENA_SHURUI_CD = contenaShuruiCd;
            conditionEntry.CONTENA_CD = contenaCd;
            var targetContena = contenaMasterDao.GetDataByCd(conditionEntry);

            // 日付が一緒かチェック
            if (CommonConst.CONTENA_SET_KBN_SECCHI == setKbn
                && !targetContena.SECCHI_DATE.IsNull)
            {
                returnVal = targetContena.SECCHI_DATE.Value.Date == secchiDate.Date ? true : false;

            }
            else if (CommonConst.CONTENA_SET_KBN_HIKIAGE == setKbn
                && !targetContena.HIKIAGE_DATE.IsNull)
            {
                returnVal = targetContena.HIKIAGE_DATE.Value.Date == secchiDate.Date ? true : false;
            }

            return returnVal;
        }
        #endregion
    }
}
