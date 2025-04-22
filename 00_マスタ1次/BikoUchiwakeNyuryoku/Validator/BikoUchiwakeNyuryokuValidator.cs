// $Id: BikoUchiwakeNyuryokuValidator.cs 31014 2014-09-26 09:57:20Z y-hosokawa@takumi-sys.co.jp $
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

namespace BikoUchiwakeNyuryoku.Validator
{
    /// <summary>
    /// 銀行支店保守検証ロジック
    /// </summary>
    public class BikoUchiwakeNyuryokuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BikoUchiwakeNyuryokuValidator()
        {
        }

        /// <summary>
        /// 銀行支店CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool BikoUchiwakeNyuryokuCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。
            bool result = true;

            GcCustomNumericTextBox2Cell cotlBikoCd = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.BikoUchiwakeNyuryokuConstans.BIKO_CD] as GcCustomNumericTextBox2Cell;


            if (cotlBikoCd == null || cotlBikoCd.Value == null || string.IsNullOrEmpty(cotlBikoCd.Value.ToString()))
            {
                return result;
            }

            // 重複チェック
            {
                // カレント行以外の廃棄物名称CDを保持するリスト
                var listBikos = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    var listBiko = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cellBikoCd = row.Cells[Const.BikoUchiwakeNyuryokuConstans.BIKO_CD];
                        if (cellBikoCd.Selected)
                        {
                            continue;
                        }

                        listBiko.Add(cellBikoCd);
                    }
                    listBikos.AddRange(listBiko);
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowBikoUchiwakeNyuryokuCompare());
                    
                    var listBiko = new List<GcCustomNumericTextBox2Cell>();
                    foreach (DataRow row in rows)
                    {
                        string BikoCd = row.Field<string>(Const.BikoUchiwakeNyuryokuConstans.BIKO_CD);

                        GcCustomNumericTextBox2Cell cellbiko = new GcCustomNumericTextBox2Cell();
                        cellbiko.Value = BikoCd;
                        listBiko.Add(cellbiko);
                    }
                    listBikos.AddRange(listBiko);
                }

                FWK.Validator vali = new FWK.Validator(cotlBikoCd, listBikos.ToArray());
                string str = vali.DuplicationCheck();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E022", "入力された備考CD");
                    result = false;
                }
            }

            return result;
        }
    }
}

