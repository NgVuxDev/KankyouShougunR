// $Id: KeiryouChouseiHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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

namespace KeiryouChouseiHoshu.Validator
{
    /// <summary>
    /// 計量調整保守検証ロジック
    /// </summary>
    public class KeiryouChouseiHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KeiryouChouseiHoshuValidator()
        {
        }

        /// <summary>
        /// 計量調整CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool KeiryouChouseiCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;

            GcCustomAlphaNumTextBoxCell controlHimei = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.KeiryouChouseiHoshuConstans.HINMEI_CD] as GcCustomAlphaNumTextBoxCell;
            GcCustomNumericTextBox2Cell controlUnit = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.KeiryouChouseiHoshuConstans.UNIT_CD] as GcCustomNumericTextBox2Cell;

            if (controlHimei == null
                || controlHimei.Value == null
                || string.IsNullOrEmpty(controlHimei.Value.ToString())
                || controlUnit == null
                || controlUnit.Value == null
                || string.IsNullOrEmpty(controlUnit.Value.ToString()))
                return result;

            // 重複チェック
            {
                var cellsHimei = new List<Cell>();
                var cellsUnit = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の計量調整CDを保持するリスト
                    var listHimei = new List<Cell>();
                    var listUnit = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cellHimei = row.Cells[Const.KeiryouChouseiHoshuConstans.HINMEI_CD];
                        if (cellHimei.Selected)
                        {
                            continue;
                        }

                        Cell cellUnit = row.Cells[Const.KeiryouChouseiHoshuConstans.UNIT_CD];
                        Cell cellUnitName = row.Cells[Const.KeiryouChouseiHoshuConstans.UNIT_NAME_RYAKU];
                        if (cellUnit.Selected || cellUnitName.Selected)
                        {
                            continue;
                        }

                        listHimei.Add(cellHimei);
                        listUnit.Add(cellUnit);
                    }
                    cellsHimei.AddRange(listHimei);
                    cellsUnit.AddRange(listUnit);
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowKeiryouChouseiCompare());

                    var listHimei = new List<GcCustomAlphaNumTextBoxCell>();
                    var listUnit = new List<GcCustomNumericTextBox2Cell>();

                    foreach (DataRow row in rows)
                    {
                        string himeiCd = row.Field<string>(Const.KeiryouChouseiHoshuConstans.HINMEI_CD);

                        GcCustomAlphaNumTextBoxCell cellHimei = new GcCustomAlphaNumTextBoxCell();
                        cellHimei.Value = himeiCd;
                        listHimei.Add(cellHimei);

                        Int16 unitCd = row.Field<Int16>(Const.KeiryouChouseiHoshuConstans.UNIT_CD);
                        GcCustomNumericTextBox2Cell cellUnit = new GcCustomNumericTextBox2Cell();
                        cellUnit.Value = unitCd;
                        listUnit.Add(cellUnit);
                    }
                    cellsHimei.AddRange(listHimei);
                    cellsUnit.AddRange(listUnit);
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string msg01 = "計量調整「品名CD,単位区分」";
                string msg02 = String.Empty;

                for (int i = 0; i <= cellsHimei.Count - 1; i++)
                {
                    if (controlHimei.Value.ToString() == cellsHimei[i].Value.ToString() &&
                        controlUnit.Value.ToString() == cellsUnit[i].Value.ToString())
                    {
                        msg02 = controlHimei.Value.ToString() + "," + controlUnit.Value.ToString();

                        msgLogic.MessageBoxShow("E003", msg01, msg02);
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
