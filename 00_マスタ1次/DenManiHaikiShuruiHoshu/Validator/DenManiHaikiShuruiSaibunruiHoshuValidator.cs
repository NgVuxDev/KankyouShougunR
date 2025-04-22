// $Id: DenManiHaikiShuruiSaibunruiHoshuValidator.cs 14165 2014-01-15 13:13:30Z sugioka $
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

namespace DenManiHaikiShuruiHoshu.Validator
{
    /// <summary>
    /// 廃棄物種類保守検証ロジック
    /// </summary>
    public class DenManiHaikiShuruiSaibunruiHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenManiHaikiShuruiSaibunruiHoshuValidator()
        {
        }

        /// <summary>
        /// 廃棄物種類CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool HaikiShuruiCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;

            GcCustomTextBoxCell control1 = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD] as GcCustomTextBoxCell;
            if (control1 == null
                || control1.Value == null
                || string.IsNullOrEmpty(control1.Value.ToString()))
                return result;

            GcCustomTextBoxCell control2 = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_SAIBUNRUI_CD] as GcCustomTextBoxCell;
            if (control2 == null
                || control2.Value == null
                || string.IsNullOrEmpty(control2.Value.ToString()))
                return result;

            // 重複チェック
            {
                var cellsShu = new List<Cell>();
                var cellsSai = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の廃棄物種類CDを保持するリスト
                    var listShu = new List<Cell>();
                    var listSai = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cellShu = row.Cells[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD];
                        if (cellShu.Selected)
                        {
                            continue;
                        }

                        Cell cellSai = row.Cells[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_SAIBUNRUI_CD];
                        if (cellSai.Selected)
                        {
                            continue;
                        }

                        listShu.Add(cellShu);
                        listSai.Add(cellSai);
                    }
                    cellsShu.AddRange(listShu);
                    cellsSai.AddRange(listSai);
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt == null ? new DataTable().AsEnumerable() : dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll == null ? new DataTable().AsEnumerable() : dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DenManiHaikiShuruiCompare());

                    var listShu = new List<GcCustomAlphaNumTextBoxCell>();
                    var listSai = new List<GcCustomAlphaNumTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        string shuruiCd = row.Field<string>(Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD);
                        string saibunruiCd = row.Field<string>(Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_SAIBUNRUI_CD);

                        GcCustomAlphaNumTextBoxCell cellShu = new GcCustomAlphaNumTextBoxCell();
                        cellShu.Value = shuruiCd;
                        listShu.Add(cellShu);

                        GcCustomAlphaNumTextBoxCell cellSai = new GcCustomAlphaNumTextBoxCell();
                        cellSai.Value = saibunruiCd;
                        listSai.Add(cellSai);
                    }
                    cellsShu.AddRange(listShu);
                    cellsSai.AddRange(listSai);
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                for (int i = 0; i < cellsShu.Count; i++)
                {
                    // 同じ行の比較の場合は飛ばす
                    if (i == gcMultiRow.CurrentRow.Index)
                    {
                        continue;
                    }

                    if (control1.Value.ToString() == cellsShu[i].Value.ToString())
                    {
                        if (control2.Value.ToString() == cellsSai[i].Value.ToString())
                        {
                            msgLogic.MessageBoxShow("E037", "廃棄物種類CD,細分類CD");
                            result = false;
                        }
                    }
                }
            }

            return result;
        }
    }
}
