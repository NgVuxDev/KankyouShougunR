// $Id: ShobunHouhouHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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
using System.Text.RegularExpressions;

namespace ShobunHouhouHoshu.Validator
{
    /// <summary>
    /// 処分方法保守検証ロジック
    /// </summary>
    public class ShobunHouhouHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShobunHouhouHoshuValidator()
        {
        }

        /// <summary>
        /// 処分方法CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool ShobunHouhouCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            bool result = true;

            GcCustomAlphaNumTextBoxCell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.ShobunHouhouHoshuConstans.SHOBUN_HOUHOU_CD] as GcCustomAlphaNumTextBoxCell;

            if (control == null ||
                control.Value == null ||
                string.IsNullOrEmpty(control.Value.ToString()))                                  
            {
                return result;
            }

            if (!IsNumber(control.Value.ToString()) || Convert.ToInt16(control.Value) < 100 || Convert.ToInt16(control.Value) > 399)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShowError("再生処分の場合は100～199、中間処理の場合は200～299、最終処分の場合は300～399を入力してください");
                return false;
            }

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の処分方法CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.ShobunHouhouHoshuConstans.SHOBUN_HOUHOU_CD];
                        if (cell.Selected)
                        {
                            continue;
                        }

                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowShobunHouhouCompare());

                    var list = new List<GcCustomAlphaNumTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        string shobunhouhouCd = row.Field<string>(Const.ShobunHouhouHoshuConstans.SHOBUN_HOUHOU_CD);

                        GcCustomAlphaNumTextBoxCell cell = new GcCustomAlphaNumTextBoxCell();
                        cell.Value = shobunhouhouCd;
                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }

                FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
                string str = vali.DuplicationCheck();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E022", "入力された処分方法CD");
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// 文字列が数字であるかどうかを判断する
        /// </summary>
        public static bool IsNumber(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            const string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }
    }
}
