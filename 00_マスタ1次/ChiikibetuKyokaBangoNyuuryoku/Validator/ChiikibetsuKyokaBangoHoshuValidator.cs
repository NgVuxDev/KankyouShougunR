// $Id: ChiikibetsuKyokaBangoHoshuValidator.cs 47708 2015-04-20 03:01:28Z j-kikuchi $
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

namespace ChiikibetsuKyokaBangoHoshu.Validator
{         
    /// <summary>
    /// 地域別許可番号保守検証ロジック
    /// </summary>
    public class ChiikibetsuKyokaBangoHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChiikibetsuKyokaBangoHoshuValidator()
        {
        }

        /// <summary>
        /// 地域CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool ChiikiCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;

            GcCustomAlphaNumTextBoxCell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD] as GcCustomAlphaNumTextBoxCell;
            if (control == null
                || control.Value == null
                || string.IsNullOrEmpty(control.Value.ToString()))
                return result;

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の地域CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD];
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

                    var rows = enumRowAll.Except(enumRow, new ChiikibetsuKyokaBangouNyuuryokuCompare());

                    var list = new List<GcCustomAlphaNumTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        string shainCd = row.Field<string>(Const.ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD);

                        GcCustomAlphaNumTextBoxCell cell = new GcCustomAlphaNumTextBoxCell();
                        cell.Value = shainCd;
                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }

                FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
                string str = vali.DuplicationCheck();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E022", "入力された地域CD");
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// 報告書分類CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow">チェック対象一覧</param>
        /// <returns name ="bool">TRUE:重複なし, FALSE:重複あり</returns>
        internal bool bunruiCDValidator(GcMultiRow gcMultiRow)
        {
            bool result = true;

            // 各行の普通, 特管の分類項目を比較
            foreach(var row in gcMultiRow.Rows)
            {
                // 新規行以外
                if(false == row.IsNewRow)
                {
                    var err = false;

                    // 一旦背景色を通常に初期化
                    row.Cells["HOUKOKUSHO_BUNRUI_NAME_RYAKU_FUTSUU"].Style.BackColor = Constans.NOMAL_COLOR;
                    row.Cells["HOUKOKUSHO_BUNRUI_NAME_RYAKU_TOKUBETSU"].Style.BackColor = Constans.NOMAL_COLOR;

                    // 普通、特管の分類項目を取得
                    var futsuuCDStr = row["HOUKOKUSHO_BUNRUI_CD_FUTSUU"].Value.ToString();
                    var tokubetsuCDStr = row["HOUKOKUSHO_BUNRUI_CD_TOKUBETSU"].Value.ToString();

                    if((false == string.IsNullOrEmpty(futsuuCDStr)) && (false == string.IsNullOrEmpty(tokubetsuCDStr)))
                    {
                        // 分類CDリストを生成
                        var futsuuList = futsuuCDStr.Split(new Char[] { ',' });
                        var tokubetsuList = tokubetsuCDStr.Split(new Char[] { ',' });

                        foreach(var futsuuCD in futsuuList)
                        {
                            foreach(var tokubetsuCD in tokubetsuList)
                            {
                                if(futsuuCD == tokubetsuCD)
                                {
                                    // 重複あり
                                    err = true;
                                    break;
                                }
                            }

                            // 重複があった場合即座に抜ける
                            if(err == true)
                            {
                                break;
                            }
                        }
                    }

                    // 重複があった場合
                    if(err == true)
                    {
                        // 分類名の背景色を赤
                        row.Cells["HOUKOKUSHO_BUNRUI_NAME_RYAKU_FUTSUU"].Style.BackColor = Constans.ERROR_COLOR;
                        row.Cells["HOUKOKUSHO_BUNRUI_NAME_RYAKU_TOKUBETSU"].Style.BackColor = Constans.ERROR_COLOR;

                        // 「重複あり」を返却
                        result = false;
                    }
                }
            }

            if(result == false)
            {
                // 重複エラー表示
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShowError("普通と特管で同じ報告書分類は選択できません。");
            }

            return result;
        }
    }
}
