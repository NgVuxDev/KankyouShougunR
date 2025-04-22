// $Id: ManifestKansanHoshuValidator.cs 16092 2014-02-17 15:13:00Z ishibashi $
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

namespace ManifestKansanHoshu.Validator
{
    /// <summary>
    /// マニフェスト換算保守検証ロジック
    /// </summary>
    public class ManifestKansanHoshuValidator
    {
        /// <summary>
        /// 最大日付（適用終了日が未入力時に使用）
        /// </summary>
        private const string MAX_DATE = "9999/12/31";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ManifestKansanHoshuValidator()
        {
        }

        /// <summary>
        /// マニフェスト換算データの重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool PrimaryCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            // 重複チェック
            {
                // カレント行以外の廃棄物名称CD、単位CD、荷姿CD、適用開始日を保持するリスト
                var isChange = new List<bool>();
                var listHaikiCd = new List<object>();
                var listUnitCd = new List<object>();
                var listNisugataCd = new List<object>();

                {
                    foreach (DataRow row in ((DataTable)gcMultiRow.DataSource).Rows)
                    {
                        object cellHaikiCd = row[Const.ManifestKansanHoshuConstans.HAIKI_NAME_CD];
                        object cellUnitCd = row[Const.ManifestKansanHoshuConstans.UNIT_CD];
                        object cellNisugataCd = row[Const.ManifestKansanHoshuConstans.NISUGATA_CD];

                        if (row[Const.ManifestKansanHoshuConstans.DELETE_FLG] != null && row[Const.ManifestKansanHoshuConstans.DELETE_FLG].ToString() == "True"
                            && (row["CREATE_USER"] == null || string.IsNullOrEmpty(row["CREATE_USER"].ToString())))
                        {
                            continue;
                        }

                        // 修正対象が本当に修正されているかチェックする
                        if (row[Const.ManifestKansanHoshuConstans.UK_HOUKOKUSHO_BUNRUI_CD] != null && !row[Const.ManifestKansanHoshuConstans.UK_HOUKOKUSHO_BUNRUI_CD].ToString().Equals(string.Empty)
                            && row[Const.ManifestKansanHoshuConstans.UK_HAIKI_NAME_CD] != null && !row[Const.ManifestKansanHoshuConstans.UK_HAIKI_NAME_CD].ToString().Equals(string.Empty)
                            && row[Const.ManifestKansanHoshuConstans.UK_UNIT_CD] != null && !row[Const.ManifestKansanHoshuConstans.UK_UNIT_CD].ToString().Equals(string.Empty))
                        {
                            DataRow[] dr = dtAll.Select(String.Format("HOUKOKUSHO_BUNRUI_CD = '{0}' AND HAIKI_NAME_CD = '{1}' AND UNIT_CD = '{2}' AND NISUGATA_CD = '{3}'"
                                , row[Const.ManifestKansanHoshuConstans.UK_HOUKOKUSHO_BUNRUI_CD].ToString()
                                , row[Const.ManifestKansanHoshuConstans.UK_HAIKI_NAME_CD].ToString()
                                , row[Const.ManifestKansanHoshuConstans.UK_UNIT_CD].ToString()
                                , row[Const.ManifestKansanHoshuConstans.UK_NISUGATA_CD].ToString()));
                            if (dr.Length > 0
                                && ((bool)dr[0][Const.ManifestKansanHoshuConstans.DELETE_FLG]).Equals(((bool)row[Const.ManifestKansanHoshuConstans.DELETE_FLG]))
                                && dr[0][Const.ManifestKansanHoshuConstans.HOUKOKUSHO_BUNRUI_CD].ToString().Equals(row[Const.ManifestKansanHoshuConstans.HOUKOKUSHO_BUNRUI_CD].ToString())
                                && dr[0][Const.ManifestKansanHoshuConstans.HAIKI_NAME_CD].ToString().Equals(row[Const.ManifestKansanHoshuConstans.HAIKI_NAME_CD].ToString())
                                && dr[0][Const.ManifestKansanHoshuConstans.UNIT_CD].ToString().Equals(row[Const.ManifestKansanHoshuConstans.UNIT_CD].ToString())
                                && dr[0][Const.ManifestKansanHoshuConstans.NISUGATA_CD].ToString().Equals(row[Const.ManifestKansanHoshuConstans.NISUGATA_CD].ToString())
                                && dr[0][Const.ManifestKansanHoshuConstans.KANSANSHIKI].ToString().Equals(row[Const.ManifestKansanHoshuConstans.KANSANSHIKI].ToString())
                                && dr[0][Const.ManifestKansanHoshuConstans.KANSANCHI].ToString().Equals(row[Const.ManifestKansanHoshuConstans.KANSANCHI].ToString())
                                && dr[0][Const.ManifestKansanHoshuConstans.MANIFEST_KANSAN_BIKOU].ToString().Equals(row[Const.ManifestKansanHoshuConstans.MANIFEST_KANSAN_BIKOU].ToString()))
                            {
                                row.RejectChanges();
                            }
                        }

                        isChange.Add(row.RowState != DataRowState.Unchanged);
                        listHaikiCd.Add(cellHaikiCd);
                        listUnitCd.Add(cellUnitCd);
                        listNisugataCd.Add(cellNisugataCd);
                    }
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                for (int i = 0; i < listHaikiCd.Count; i++)
                {
                    if (!isChange[i]) continue;

                    for (int j = 0; j < listHaikiCd.Count; j++)
                    {
                        if (i == j) continue;

                        //キーが同一の数
                        int cellCount = 0;

                        //廃棄物名称CDが同一
                        if ((listHaikiCd[i] == null || listHaikiCd[i].ToString() == "") &&
                            (listHaikiCd[j] == null || listHaikiCd[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listHaikiCd[i].ToString() != "" && listHaikiCd[j].ToString() != "")
                        {
                            if (listHaikiCd[i].ToString() == listHaikiCd[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //単位CDが同一
                        if ((listUnitCd[i] == null || listUnitCd[i].ToString() == "") &&
                            (listUnitCd[j] == null || listUnitCd[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listUnitCd[i].ToString() != "" && listUnitCd[j].ToString() != "")
                        {
                            if (listUnitCd[i].ToString() == listUnitCd[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //荷姿CDが同一
                        if ((listNisugataCd[i] == null || listNisugataCd[i].ToString() == "") &&
                            (listNisugataCd[j] == null || listNisugataCd[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listNisugataCd[i].ToString() != "" && listNisugataCd[j].ToString() != "")
                        {
                            if (listNisugataCd[i].ToString() == listNisugataCd[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //廃棄物名称CD、単位、荷姿CDが同一の場合
                        if (cellCount == 3)
                        {
                            msgLogic.MessageBoxShow("E013");
                                return false;
                        }
                    }
                }

                #region DBデータチェック
                // DBのデータチェック
                var listDbHaikiCd = new List<object>();
                var listDbUnitCd = new List<object>();
                var listDbNisugataCd = new List<object>();
                // 重複エラー時用のメッセージで使用
                string dispHaikiNameCd = string.Empty;
                string dispUnitKbn = string.Empty;
                string dispNisugataCd = string.Empty;

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowManifestKansanCompare());
                    foreach (DataRow row in rows)
                    {
                        // 廃棄物名称CD
                        listDbHaikiCd.Add(row.Field<string>(Const.ManifestKansanHoshuConstans.HAIKI_NAME_CD));

                        // 単位CD
                        if (row.Field<Object>(Const.ManifestKansanHoshuConstans.UNIT_CD) == null)
                        {
                            listDbUnitCd.Add(null);
                        }
                        else
                        {
                            listDbUnitCd.Add(row.Field<Int16>(Const.ManifestKansanHoshuConstans.UNIT_CD));
                        }

                        // 荷姿CD
                        listDbNisugataCd.Add(row.Field<string>(Const.ManifestKansanHoshuConstans.NISUGATA_CD));
                    }
                }

                for (int i = 0; i < listHaikiCd.Count; i++)
                {
                    if (!isChange[i]) continue;

                    for (int j = 0; j < listDbHaikiCd.Count; j++)
                    {
                        //キーが同一の数
                        int cellCount = 0;
                        dispHaikiNameCd = string.Empty;
                        dispUnitKbn = string.Empty;
                        dispNisugataCd = string.Empty;

                        //廃棄物名称CDが同一
                        if ((listHaikiCd[i] == null || listHaikiCd[i].ToString() == "") &&
                            (listDbHaikiCd[j] == null || listDbHaikiCd[j].ToString() == ""))
                        {
                            cellCount += 1;
                            dispHaikiNameCd = listHaikiCd[i].ToString();
                        }
                        else if (listHaikiCd[i].ToString() != "" && listDbHaikiCd[j].ToString() != "")
                        {
                            if (listHaikiCd[i].ToString() == listDbHaikiCd[j].ToString())
                            {
                                cellCount += 1;
                                dispHaikiNameCd = listHaikiCd[i].ToString();
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //単位CDが同一
                        if ((listUnitCd[i] == null || listUnitCd[i].ToString() == "") &&
                            (listDbUnitCd[j] == null || listDbUnitCd[j].ToString() == ""))
                        {
                            cellCount += 1;
                            dispUnitKbn = listUnitCd[i].ToString();
                        }
                        else if (listUnitCd[i].ToString() != "" && listDbUnitCd[j].ToString() != "")
                        {
                            if (listUnitCd[i].ToString() == listDbUnitCd[j].ToString())
                            {
                                cellCount += 1;
                                dispUnitKbn = listUnitCd[i].ToString();
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //荷姿CDが同一
                        if ((listNisugataCd[i] == null || listNisugataCd[i].ToString() == "") &&
                            (listDbNisugataCd[j] == null || listDbNisugataCd[j].ToString() == ""))
                        {
                            cellCount += 1;
                            dispNisugataCd = listNisugataCd[j].ToString();
                        }
                        else if (listNisugataCd[i].ToString() != "" && listDbNisugataCd[j].ToString() != "")
                        {
                            if (listNisugataCd[i].ToString() == listDbNisugataCd[j].ToString())
                            {
                                cellCount += 1;
                                dispNisugataCd = listNisugataCd[j].ToString();
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //廃棄物名称CD、単位、荷姿CDが同一の場合
                        if (cellCount == 3)
                        {
                            msgLogic.MessageBoxShow("E259", "換算値または備考", "・廃棄物名称CD：" + dispHaikiNameCd + "、単位区分CD：" + dispUnitKbn.ToString() + "、荷姿CD：" + dispNisugataCd);
                            return false;
                        }
                    }
                }
                #endregion
            }
            return true;
        }
    }
}
