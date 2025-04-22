// $Id: KongouShuruiHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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

namespace KongouShuruiHoshu.Validator
{
    /// <summary>
    /// 混合種類保守検証ロジック
    /// </summary>
    public class KongouShuruiHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KongouShuruiHoshuValidator()
        {
        }

        /// <summary>
        /// 混合種類CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <param name="haikiCd"></param>
        /// <returns></returns>
        public bool KongouShuruiCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch, string haikiCd)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;

            GcCustomAlphaNumTextBoxCell shuruiControl = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.KongouShuruiHoshuConstans.KONGOU_SHURUI_CD] as GcCustomAlphaNumTextBoxCell;
            if (shuruiControl == null
                || shuruiControl.Value == null
                || string.IsNullOrEmpty(shuruiControl.Value.ToString()))
                return result;

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の混合種類CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.KongouShuruiHoshuConstans.KONGOU_SHURUI_CD];
                        Cell haikiKbnCd = row.Cells[Const.KongouShuruiHoshuConstans.HAIKI_KBN_CD];
                        if (cell.Selected)
                        {
                            continue;
                        }
                        //数値同士で比較する。
                        //if (Convert.ToString(haikiKbnCd.Value) == haikiCd)
                        if (string.IsNullOrEmpty(haikiKbnCd.Value.ToString()) || Convert.ToInt16(haikiKbnCd.Value) == Convert.ToInt16(haikiCd))
                        {
                            list.Add(cell);
                        }
                    }
                    cells.AddRange(list);
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowKongouCompare());

                    var list = new List<GcCustomAlphaNumTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        string kongouCd = row.Field<string>(Const.KongouShuruiHoshuConstans.KONGOU_SHURUI_CD);
                        Int16 haikiKbnCd = row.Field<Int16>(Const.KongouShuruiHoshuConstans.HAIKI_KBN_CD);
                        GcCustomAlphaNumTextBoxCell cell = new GcCustomAlphaNumTextBoxCell();
                        if (!String.IsNullOrEmpty(haikiCd))
                        {
                            if (haikiKbnCd == Convert.ToInt16(haikiCd))
                            {
                                cell.Value = kongouCd;
                                list.Add(cell);
                            }
                        }
                    }
                    cells.AddRange(list);
                }

                FWK.Validator vali = new FWK.Validator(shuruiControl, cells.ToArray());

                string str = vali.DuplicationCheck();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E022", "入力された混合種類CD");
                    result = false;
                }
            }

            return result;
        }
    }
}
