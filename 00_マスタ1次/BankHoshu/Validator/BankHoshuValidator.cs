// $Id: BankHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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

namespace BankHoshu.Validator
{
    /// <summary>
    /// 銀行保守検証ロジック
    /// </summary>
    public class BankHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BankHoshuValidator()
        {
        }

        /// <summary>
        /// 銀行CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool BankCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。
            bool result = true;

            GcCustomNumericTextBox2Cell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.BankHoshuConstans.BANK_CD] as GcCustomNumericTextBox2Cell;
            if (control == null
                || control.Value == null
                || string.IsNullOrEmpty(control.Value.ToString()))
                return result;

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の銀行CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.BankHoshuConstans.BANK_CD];
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

                    var rows = enumRowAll.Except(enumRow, new DataRowBankCompare());

                    var list = new List<GcCustomTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        string bankCd = row.Field<string>(Const.BankHoshuConstans.BANK_CD);

                        GcCustomTextBoxCell cell = new GcCustomTextBoxCell();
                        cell.Value = bankCd;
                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }

                FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
                string str = vali.DuplicationCheck();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E022", "入力された銀行CD", control.Value.ToString());
                    result = false;
                }
            }

            return result;
        }
        /// <summary>
        /// 連携用CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool RenkeiCDValidator(GcMultiRow gcMultiRow, Row currentRow, DataTable dt, DataTable dtAll)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;
            int iError = 0;
            if (string.IsNullOrEmpty(Convert.ToString(currentRow.Cells[Const.BankHoshuConstans.RENKEI_CD].Value))
                || string.IsNullOrEmpty(Convert.ToString(currentRow.Cells[Const.BankHoshuConstans.BANK_CD].Value)))
            {
                return result;
            }

            foreach (Row row in gcMultiRow.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[Const.BankHoshuConstans.RENKEI_CD].Value))
                    && row.Cells[Const.BankHoshuConstans.RENKEI_CD].Value.ToString().Equals(currentRow.Cells[Const.BankHoshuConstans.RENKEI_CD].Value.ToString())
                    && !row.Cells[Const.BankHoshuConstans.BANK_CD].Value.ToString().Equals(currentRow.Cells[Const.BankHoshuConstans.BANK_CD].Value.ToString()))
                {
                    iError++;
                }
            }

            IEnumerable<DataRow> enumRow = dt.AsEnumerable();
            IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

            var rows = enumRowAll.Except(enumRow, new DataRowBankCompare());

            foreach (DataRow dr in rows)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dr[Const.BankHoshuConstans.RENKEI_CD].ToString()))
                    && dr[Const.BankHoshuConstans.RENKEI_CD].ToString().Equals(currentRow.Cells[Const.BankHoshuConstans.RENKEI_CD].Value.ToString())
                    && !dr[Const.BankHoshuConstans.BANK_CD].ToString().Equals(currentRow.Cells[Const.BankHoshuConstans.BANK_CD].Value.ToString()))
                {
                    iError++;
                }
            }

            if (iError > 0)
            {
                result = false;
            }
            return result;

        }
    }
}
