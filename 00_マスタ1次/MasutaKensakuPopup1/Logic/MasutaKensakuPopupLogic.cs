// $Id: MasutaKensakuPopupLogic.cs 12989 2013-12-26 09:22:04Z ishibashi $
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasutaKensakuPopup1.APP;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Logic;

namespace MasutaKensakuPopup1.Logic
{
    /// <summary>
    /// 検索項目ポップアップ画面ロジック
    /// </summary>
    public class MasutaKensakuPopupLogic
    {
        #region フィールド

        private MasutaKensakuPopupForm form;

        private GcCustomMultiRow gcMultiRow;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MasutaKensakuPopupLogic()
            : this(null, null)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        /// <param name="gcMultiRow"></param>
        public MasutaKensakuPopupLogic(MasutaKensakuPopupForm targetForm, GcCustomMultiRow gcMultiRow)
        {
            this.form = targetForm;
            this.gcMultiRow = gcMultiRow;
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal void WindowInit()
        {
        }

        /// <summary>
        /// 一覧データ作成
        /// </summary>
        internal void CreateDetailList()
        {

            MultiRowIndexCreateLogic multirowLocationLogic = new MultiRowIndexCreateLogic();
            multirowLocationLogic.multiRow = this.gcMultiRow;

            multirowLocationLogic.CreateLocations();

            DataTable dt = CreateDataTable();

            foreach (var dto in multirowLocationLogic.sortEndList)
            {
                var index = dto.Cells.CellIndex;
                var customHeaderCell = gcMultiRow.ColumnHeaders[0].Cells[index] as GcCustomColumnHeader;

                if (customHeaderCell != null)
                {
                    if (!customHeaderCell.ViewSearchItem)
                    {
                        continue;
                    }
                }

                if (this.gcMultiRow.Rows.Count > 0)
                {
                    Cell cell = this.gcMultiRow.Rows[0].Cells[index];

                    var customCont = cell as ICustomControl;

                    string name = gcMultiRow.ColumnHeaders[0].Cells[index].Value.ToString();
                    ImeMode ime = GetIME(name, cell);
                    string data = cell.DataField;
                    string itemDef = customCont.ItemDefinedTypes;
                    dt.Rows.Add(name, ime, data, itemDef);
                }
                else
                {
                    Cell cell = this.gcMultiRow.ColumnHeaders[0].Cells[index];
                    string name = gcMultiRow.ColumnHeaders[0].Cells[index].Value.ToString();
                    ImeMode ime = GetIME(name, cell);
                    string data = cell.DataField;
                    //ヘッダー固定なのでこれで固定
                    string itemDef = "varchar";
                    dt.Rows.Add(name, ime, data, itemDef);
                }
                


            }

            this.form.MasterItem.DataSource = dt;
        }

        /// <summary>
        /// 項目決定
        /// </summary>
        internal void ElementDecision()
        {
            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            List<PopupReturnParam> setParam;
            for (int i = 0; i < this.form.MasterItem.Columns.Count; i++)
            {
                PopupReturnParam popupParam = new PopupReturnParam();
                var setDate = this.form.MasterItem[this.form.MasterItem.CurrentRow.Index, i];
                var control = setDate as ICustomControl;

                popupParam.Key = setDate.Name;

                if ("ImeMode" == setDate.Name)
                {
                    popupParam.Value = (ImeMode)Enum.ToObject(typeof(ImeMode), int.Parse(setDate.Value.ToString()));
                }
                else
                {
                    popupParam.Value = setDate.Value.ToString();
                }
                if (setParamList.ContainsKey(int.Parse(control.ShortItemName)))
                {
                    setParam = setParamList[int.Parse(control.ShortItemName)];
                    setParamList.Remove(int.Parse(control.ShortItemName));
                }
                else
                {
                    setParam = new List<PopupReturnParam>();
                }


                setParam.Add(popupParam);
                setParamList.Add(int.Parse(control.ShortItemName), setParam);
            }

            var staticPopupParam = new PopupReturnParam();

            //初期化のため、ベタ書きにて実装
            staticPopupParam.Key = "Text";
            staticPopupParam.Value = string.Empty;
            setParam = new List<PopupReturnParam>();
            setParam = setParamList[2];
            setParamList.Remove(2);

            setParam.Add(staticPopupParam);
            setParamList.Add(2, setParam);


            this.form.ReturnParams = setParamList;
            this.form.Close();
        }

        /// <summary>
        /// データテーブル作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            var dt = new DataTable();

            DataColumn dcItem = new DataColumn("Item", typeof(string));
            DataColumn dcImeMode = new DataColumn("ImeMode", typeof(ImeMode));
            DataColumn dcDataField = new DataColumn("DBFieldsName", typeof(string));
            DataColumn dcItemTypes = new DataColumn("ItemDefinedTypes", typeof(string));

            dt.Columns.Add(dcItem);
            dt.Columns.Add(dcImeMode);
            dt.Columns.Add(dcDataField);
            dt.Columns.Add(dcItemTypes);

            return dt;
        }

        /// <summary>
        /// IMEの取得
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        private ImeMode GetIME(string name, Cell cell)
        {
            if (!cell.ReadOnly)
            {
                return cell.Style.ImeMode;
            }

            // 表示専用であればヘッダ名称から推測して判定
            if (name.Contains("CD")
                || name.Equals("更新日")
                || name.Equals("作成日"))
            {
                return ImeMode.Disable;
            }
            else if (name.Contains("フリガナ"))
            {
                return ImeMode.Katakana;
            }
            else if (cell is GcCustomDataTimePicker)
            {
                return cell.Style.ImeMode;
            }
            else
            {
                return ImeMode.Hiragana;
            }
        }
    }
}
