// $Id: KansanHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using FWK = r_framework.Logic;
using System.Data;
using System;

namespace KansanHoshu.Validator
{
    /// <summary>
    /// 換算値保守検証ロジック
    /// </summary>
    public class KansanHoshuValidator
    {
        /// <summary>
        /// 最大日付（適用終了日が未入力時に使用）
        /// </summary>
        private const string MAX_DATE = "9999/12/31";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KansanHoshuValidator()
        {
        }

        /// <summary>
        /// 換算値CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool KansanCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            // 重複チェック
            {
                // カレント行以外の計量調整CDを保持するリスト
                var isChange = new List<bool>();
                var listHinmei = new List<object>();
                var listUnit = new List<object>();
                var listBegin = new List<object>();
                var listEnd = new List<object>();

                // 表示分(検索条件による抽出分)
                {
                    foreach (DataRow row in ((DataTable)gcMultiRow.DataSource).Rows)
                    {
                        object cellHinmei = row[Const.KansanHoshuConstans.HINMEI_CD];
                        object cellUnit = row[Const.KansanHoshuConstans.UNIT_CD];

                        // 修正対象が本当に修正されているかチェックする
                        if (row[Const.KansanHoshuConstans.UK_DENPYOU_KBN_CD] != null && !row[Const.KansanHoshuConstans.UK_DENPYOU_KBN_CD].ToString().Equals(string.Empty)
                            && row[Const.KansanHoshuConstans.UK_HINMEI_CD] != null && !row[Const.KansanHoshuConstans.UK_HINMEI_CD].ToString().Equals(string.Empty)
                            && row[Const.KansanHoshuConstans.UK_UNIT_CD] != null && !row[Const.KansanHoshuConstans.UK_UNIT_CD].ToString().Equals(string.Empty)
                            )
                        {
                            DataRow[] dr = dtAll.Select(String.Format("DENPYOU_KBN_CD = '{0}' AND HINMEI_CD = '{1}' AND UNIT_CD = '{2}'"
                                , row[Const.KansanHoshuConstans.UK_DENPYOU_KBN_CD].ToString()
                                , row[Const.KansanHoshuConstans.UK_HINMEI_CD].ToString()
                                , row[Const.KansanHoshuConstans.UK_UNIT_CD].ToString()));
                            if (dr.Length > 0
                                && ((bool)dr[0][Const.KansanHoshuConstans.DELETE_FLG]).Equals(((bool)row[Const.KansanHoshuConstans.DELETE_FLG]))
                                && dr[0][Const.KansanHoshuConstans.DENPYOU_KBN_CD].ToString().Equals(row[Const.KansanHoshuConstans.DENPYOU_KBN_CD].ToString())
                                && dr[0][Const.KansanHoshuConstans.HINMEI_CD].ToString().Equals(row[Const.KansanHoshuConstans.HINMEI_CD].ToString())
                                && dr[0][Const.KansanHoshuConstans.UNIT_CD].ToString().Equals(row[Const.KansanHoshuConstans.UNIT_CD].ToString())
                                && dr[0][Const.KansanHoshuConstans.KANSANSHIKI].ToString().Equals(row[Const.KansanHoshuConstans.KANSANSHIKI].ToString())
                                && dr[0][Const.KansanHoshuConstans.KANSANCHI].ToString().Equals(row[Const.KansanHoshuConstans.KANSANCHI].ToString())
                                && dr[0][Const.KansanHoshuConstans.KANSAN_BIKOU].ToString().Equals(row[Const.KansanHoshuConstans.KANSAN_BIKOU].ToString())
                                )
                            {
                                row.RejectChanges();
                            }
                        }

                        isChange.Add(row.RowState != DataRowState.Unchanged);
                        listHinmei.Add(cellHinmei);
                        listUnit.Add(cellUnit);
                    }
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowKansanCompare());
                    foreach (DataRow row in rows)
                    {
                        isChange.Add(false);

                        //品名CD
                        listHinmei.Add(row.Field<string>(Const.KansanHoshuConstans.HINMEI_CD));

                        //単位CD
                        if (row.Field<Object>(Const.KansanHoshuConstans.UNIT_CD) == null)
                        {
                            listUnit.Add(null);
                        }
                        else
                        {
                            listUnit.Add(row.Field<Int16>(Const.KansanHoshuConstans.UNIT_CD));
                        }
                    }
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                for (int i = 0; i < listHinmei.Count; i++)
                {
                    if (!isChange[i]) continue;

                    for (int j = 0; j < listHinmei.Count; j++)
                    {
                        if (i == j) continue;

                        //キーが同一の数
                        int cellCount = 0;

                        //品名CDが同一
                        if ((listHinmei[i] == null || listHinmei[i].ToString() == "") &&
                            (listHinmei[j] == null || listHinmei[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listHinmei[i].ToString() != "" && listHinmei[j].ToString() != "")
                        {
                            if (listHinmei[i].ToString() == listHinmei[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //単位CDが同一
                        if ((listUnit[i] == null || listUnit[i].ToString() == "") &&
                            (listUnit[j] == null || listUnit[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listUnit[i].ToString() != "" && listUnit[j].ToString() != "")
                        {
                            if (listUnit[i].ToString() == listUnit[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //品名CD、単位CDが同一の場合
                        if (cellCount == 2)
                        {
                            bool checkDate = true;

                            //検証データを変数に格納しておく
                            //※置換データが実データに反映されないように
                            DateTime srcBegin = (DateTime)listBegin[i];
                            DateTime srcEnd = DateTime.Parse(MAX_DATE);
                            if (listEnd[i] != null && !string.IsNullOrWhiteSpace(listEnd[i].ToString()))
                            {
                                srcEnd = (DateTime)listEnd[i];
                            }
                            DateTime targetBegin = (DateTime)listBegin[j];
                            DateTime targetEnd = DateTime.Parse(MAX_DATE);
                            if (listEnd[j] != null && !string.IsNullOrWhiteSpace(listEnd[j].ToString()))
                            {
                                targetEnd = (DateTime)listEnd[j];
                            }

                            //チェックパターン①　開始日存在
                            if (srcBegin <= targetBegin && targetBegin <= srcEnd)
                            {
                                checkDate = false;
                            }

                            //チェックパターン②　終了日存在
                            if (srcBegin <= targetEnd && targetEnd <= srcEnd)
                            {
                                checkDate = false;
                            }

                            //チェックパターン③　開始終了内包
                            if (targetBegin <= srcBegin && srcEnd <= targetEnd)
                            {
                                checkDate = false;
                            }

                            //適用期間範囲のチェック結果、NGであれば重複エラーとする
                            if (!checkDate)
                            {
                                msgLogic.MessageBoxShow("E013");
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}

