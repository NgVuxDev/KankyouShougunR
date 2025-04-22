// $Id: BankShitenHoshuValidator.cs 31014 2014-09-26 09:57:20Z y-hosokawa@takumi-sys.co.jp $
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

namespace BankShitenHoshu.Validator
{
    /// <summary>
    /// 銀行支店保守検証ロジック
    /// </summary>
    public class BankShitenHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BankShitenHoshuValidator()
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
        public bool BankShitenCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch, Row checkRow)
        {
            // 要実装方法検討。現段階は仮実装。
            bool result = true;
            string msg01 = "入力された銀行支店「銀行支店CD,口座番号」";
            string msg02 = String.Empty;

            GcCustomNumericTextBox2Cell cotlBankShitenCd = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.BankShitenHoshuConstans.BANK_SHITEN_CD] as GcCustomNumericTextBox2Cell;
            GcCustomAlphaNumTextBoxCell cotlKouzaNo = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.BankShitenHoshuConstans.KOUZA_NO] as GcCustomAlphaNumTextBoxCell;



            if (cotlBankShitenCd == null || cotlBankShitenCd.Value == null || string.IsNullOrEmpty(cotlBankShitenCd.Value.ToString()))
            {
                return result;
            }

            if (cotlKouzaNo == null || cotlKouzaNo.Value == null || string.IsNullOrEmpty(cotlKouzaNo.Value.ToString()))
            {
                return result;
            }

            // 重複チェック
            {
                // カレント行以外の廃棄物名称CDを保持するリスト
                var listBankShiten = new List<Cell>();
                var listKouzaNo = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        if (row.Equals(checkRow))
                        {
                            continue;
                        }

                        Cell cellBankShitenCd = row.Cells[Const.BankShitenHoshuConstans.BANK_SHITEN_CD];
                        if (cellBankShitenCd.Selected)
                        {
                            continue;
                        }

                        Cell cellKouzaNo = row.Cells[Const.BankShitenHoshuConstans.KOUZA_NO];
                        if (cellKouzaNo.Selected)
                        {
                            continue;
                        }

                        listBankShiten.Add(cellBankShitenCd);
                        listKouzaNo.Add(cellKouzaNo);
                    }
                }



                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();


                    var rows = enumRowAll.Except(enumRow, new DataRowBankShitenCompare());

                    foreach (DataRow row in rows)
                    {
                        string BankShitenCd = row.Field<string>(Const.BankShitenHoshuConstans.BANK_SHITEN_CD);

                        GcCustomNumericTextBox2Cell cellbankshiten = new GcCustomNumericTextBox2Cell();
                        cellbankshiten.Value = BankShitenCd;
                        listBankShiten.Add(cellbankshiten);

                        string shobunCd = row.Field<string>(Const.BankShitenHoshuConstans.KOUZA_NO);

                        GcCustomAlphaNumTextBoxCell cellkouzano = new GcCustomAlphaNumTextBoxCell();
                        cellkouzano.Value = shobunCd;
                        listKouzaNo.Add(cellkouzano);
                    }
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                for (int i = 0; i < listBankShiten.Count; i++)
                {
                    //キーが同一の桁
                    int cellCount = 0;
                    msg02 = "";

                    //銀行支店CDが同一
                    if ((checkRow[Const.BankShitenHoshuConstans.BANK_SHITEN_CD].Value == null || checkRow[Const.BankShitenHoshuConstans.BANK_SHITEN_CD].Value.ToString() == "") &&
                        (listBankShiten[i] == null || Convert.ToString(listBankShiten[i].Value) == ""))
                    {
                        cellCount += 1;
                        msg02 += "";
                    }
                    else if (checkRow[Const.BankShitenHoshuConstans.BANK_SHITEN_CD].Value.ToString() != "" && listBankShiten[i].Value.ToString() != "")
                    {
                        if (checkRow[Const.BankShitenHoshuConstans.BANK_SHITEN_CD].Value.ToString() == listBankShiten[i].Value.ToString())
                        {
                            cellCount += 1;
                            msg02 += listBankShiten[i].Value.ToString();
                        }
                    }
                    else
                    {
                        cellCount = 0;
                        msg02 = string.Empty;
                        continue;
                    }

                    //口座番号が同一
                    if ((checkRow[Const.BankShitenHoshuConstans.KOUZA_NO].Value == null || checkRow[Const.BankShitenHoshuConstans.KOUZA_NO].Value.ToString() == "") &&
                        (listKouzaNo[i] == null || Convert.ToString(listKouzaNo[i].Value) == ""))
                    {
                        cellCount += 1;
                        msg02 += "," + "";
                    }
                    else if (checkRow[Const.BankShitenHoshuConstans.KOUZA_NO].Value.ToString() != "" && listKouzaNo[i].Value.ToString() != "")
                    {
                        if (checkRow[Const.BankShitenHoshuConstans.KOUZA_NO].Value.ToString() == listKouzaNo[i].Value.ToString())
                        {
                            cellCount += 1;
                            msg02 += "," + listKouzaNo[i].Value.ToString();
                        }
                    }
                    else
                    {
                        cellCount = 0;
                        msg02 = string.Empty;
                        continue;
                    }

                    //同一の場合
                    if (cellCount == 2)
                    {
                        msgLogic.MessageBoxShow("E022", msg01);
                        return false;
                    }
                }
            }

            return result;
        }
    }
}

