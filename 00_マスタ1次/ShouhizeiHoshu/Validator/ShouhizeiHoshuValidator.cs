// $Id: ShouhizeiHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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

namespace ShouhizeiHoshu.Validator
{
    /// <summary>
    /// 消費税証ロジック
    /// </summary>
    public class ShouhizeiHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShouhizeiHoshuValidator()
        {
        }

        /// <summary>
        /// 重複・整合性チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <returns>true:エラー無し、false:エラー有り</returns>
        public bool ShouhizeiValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            bool result = true;

            List<bool> isChange = new List<bool>();
            List<decimal> shouhizeiRateList = new List<decimal>();
            List<object> tekiyouBeginList = new List<object>();
            List<object> tekiyouEndList = new List<object>();
            List<bool> deleteFlgList = new List<bool>();

            // 重複チェック
            {
                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の社員CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.ShouhizeiHoshuConstans.SYS_ID];
                        if (cell.Selected)
                        {
                            continue;
                        }

                        list.Add(cell);

                        Cell shouhizeiRateCell = row.Cells[Const.ShouhizeiHoshuConstans.SHOUHIZEI_RATE];
                        decimal tempShouhizei = -1;
                        if (!string.IsNullOrEmpty(Convert.ToString(shouhizeiRateCell.Value))
                            && decimal.TryParse(shouhizeiRateCell.Value.ToString(), out tempShouhizei))
                        {
                            isChange.Add(row.Modified);
                            shouhizeiRateList.Add(tempShouhizei);
                            tekiyouBeginList.Add(row.Cells[Const.ShouhizeiHoshuConstans.TEKIYOU_BEGIN].Value);
                            tekiyouEndList.Add(row.Cells[Const.ShouhizeiHoshuConstans.TEKIYOU_END].Value);
                            if (row.Cells[Const.ShouhizeiHoshuConstans.DELETE_FLG].Value != null
                                && !string.IsNullOrEmpty(row.Cells[Const.ShouhizeiHoshuConstans.DELETE_FLG].Value.ToString()))
                            {
                                deleteFlgList.Add((bool)row.Cells[Const.ShouhizeiHoshuConstans.DELETE_FLG].Value);
                            }
                            else
                            {
                                deleteFlgList.Add(false);
                            }
                        }
                    }
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowShouhizeiCompare());

                    var list = new List<GcCustomAlphaNumTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        decimal tempShouhizei = row.Field<decimal>(Const.ShouhizeiHoshuConstans.SHOUHIZEI_RATE);
                        isChange.Add(false);
                        shouhizeiRateList.Add(tempShouhizei);

                        if (row.Field<object>(Const.ShouhizeiHoshuConstans.TEKIYOU_BEGIN) == null)
                        {
                            tekiyouBeginList.Add(null);
                        }
                        else
                        {
                            tekiyouBeginList.Add(row.Field<object>(Const.ShouhizeiHoshuConstans.TEKIYOU_BEGIN));
                        }

                        if (row.Field<object>(Const.ShouhizeiHoshuConstans.TEKIYOU_END) == null)
                        {
                            tekiyouEndList.Add(null);
                        }
                        else
                        {
                            tekiyouEndList.Add(row.Field<object>(Const.ShouhizeiHoshuConstans.TEKIYOU_END));
                        }
                        deleteFlgList.Add(row.Field<bool>(Const.ShouhizeiHoshuConstans.DELETE_FLG));
                    }
                }
            }

            // 日付の整合性チェック
            if (result) {
                for (int i = 0; i < shouhizeiRateList.Count; i++)
                {
                    if (!result) break;
                    if (!isChange[i]) continue;

                    for (int j = 0; j < shouhizeiRateList.Count; j++)
                    {
                        if (!result) break;
                        if (i == j) continue;

                        //キーが同一の数
                        int cellCount = 0;

                        // 削除チェックがついている かつ 未登録行の場合
                        if (deleteFlgList[i] || deleteFlgList[j])
                        {
                            continue;
                        }

                        bool checkDate = true;

                        //検証データを変数に格納しておく
                        //※置換データが実データに反映されないように
                        DateTime srcBegin = (DateTime)tekiyouBeginList[i];
                        DateTime srcEnd = DateTime.MaxValue;
                        if (tekiyouEndList[i] != null && !string.IsNullOrWhiteSpace(tekiyouEndList[i].ToString()))
                        {
                            srcEnd = (DateTime)tekiyouEndList[i];
                        }
                        DateTime targetBegin = (DateTime)tekiyouBeginList[j];
                        DateTime targetEnd = DateTime.MaxValue;
                        if (tekiyouEndList[j] != null && !string.IsNullOrWhiteSpace(tekiyouEndList[j].ToString()))
                        {
                            targetEnd = (DateTime)tekiyouEndList[j];
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
                            result = false;
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
