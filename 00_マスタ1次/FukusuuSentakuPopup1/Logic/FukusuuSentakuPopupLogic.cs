using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FukusuuSentakuPopup1.APP;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.CustomControl;
using System.Text;
using System.Linq;

namespace FukusuuSentakuPopup1.Logic
{
    internal class FukusuuSentakuPopupLogic
    {
        protected IS2Dao dao;
        protected IM_CHIIKIBETSU_KYOKA_MEIGARADao Chiikibetsudao;

        protected SuperEntity entity;

        protected FukusuuSentakuPopupForm form;

        internal bool isReturnValueInit = true;

        private List<string> HeaderList;

        private List<string> CellList;

        private string[] cdList;

        private string[] tsumikaeList;

        public DataTable dtOld;

        public FukusuuSentakuPopupLogic(FukusuuSentakuPopupForm target)
        {
            this.form = target;

        }

        /// <summary>
        /// 画面表示情報初期化
        /// </summary>
        internal void WindowInit()
        {
            this.EventInit();
            this.FieldInit();
            this.CreateHeader();
        }

        /// <summary>
        /// Dao/Entityを初期化
        /// </summary>
        internal void FieldInit()
        {
            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_HAIKI_SHURUI:
                    this.dao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();
                    this.entity = new M_HAIKI_SHURUI();
                    break;

                case WINDOW_ID.M_HOUKOKUSHO_BUNRUI:
                case WINDOW_ID.M_HOUKOKUSHO_BUNRUI_UNPAN:
                    this.dao = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>();
                    this.entity = new M_HOUKOKUSHO_BUNRUI();
                    this.Chiikibetsudao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_KYOKA_MEIGARADao>();
                    break;
            }
            dtOld = new DataTable();
        }

        /// <summary>
        /// MultiRowのヘッダー部分に表示する文字列を指定する
        /// </summary>
        internal void CreateHeader()
        {
            HeaderList = new List<string>();

            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_HAIKI_SHURUI:
                    this.form.lb_title.Text = "廃棄物種類選択";
                    //HeaderList.Add("選");
                    HeaderList.Add("廃棄物区分CD");
                    HeaderList.Add("廃棄物区分名");
                    HeaderList.Add("廃棄物種類CD");
                    HeaderList.Add("廃棄物種類名");
                    this.form.IsMasterAccessStartUp = true;
                    break;

                case WINDOW_ID.M_HOUKOKUSHO_BUNRUI:
                case WINDOW_ID.M_HOUKOKUSHO_BUNRUI_UNPAN:
                    this.form.lb_title.Text = "報告書分類選択";
                    //HeaderList.Add("選");
                    HeaderList.Add("報告書分類CD");
                    HeaderList.Add("報告書分類名");
                    HeaderList.Add("許可KBN");
                    HeaderList.Add("業者CD");
                    HeaderList.Add("現場CD");
                    HeaderList.Add("地域CD");
                    HeaderList.Add("特別管理KBN");
                    this.form.IsMasterAccessStartUp = true;
                    break;
            }

            this.form.Text = this.form.lb_title.Text;
        }

        /// <summary>
        /// 値の選択処理
        /// </summary>
        internal void ElementDecision()
        {
            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            List<PopupReturnParam> setParam = null;

            var fields = this.form.PopupGetMasterField.Split(',');
            for (int i = 0; i < fields.Length; i++)
            {
                List<string> list = new List<string>();
                for (int j = 0; j < this.form.masterDetail.Rows.Count; j++)
                {
                    var select = this.form.masterDetail.Rows[j].Cells[0].Value;
                    if (select is bool && (bool)select)
                    {
                        var setData = this.form.masterDetail.Rows[j].Cells[fields[i]];
                        if (!"TSUMIKAE".Equals(fields[i]))
                        {
                            list.Add(setData.Value.ToString());
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(setData.Value.ToString()))
                            {
                                list.Add("False");
                            }
                            else
                            {
                                list.Add(setData.Value.ToString());
                            }
                        }
                    }
                }
                PopupReturnParam popupParam = new PopupReturnParam();
                popupParam.Key = "Value";
                popupParam.Value = string.Join(",", list.ToArray());
                if (setParamList.ContainsKey(i))
                {
                    setParam = setParamList[i];
                }
                else
                {
                    setParam = new List<PopupReturnParam>();
                }
                setParam.Add(popupParam);
                setParamList.Add(i, setParam);
            }

            this.form.ReturnParams = setParamList;

            this.isReturnValueInit = false;
            this.form.Close();
        }

        /// <summary>
        /// チェックボックスのセット処理
        /// </summary>
        internal void SetCheck()
        {
            // ポップアップから呼ぶ側にPopupSetFormFieldが設定されていた場合
            if (this.form.ReturnParams != null)
            {
                var fields = this.form.ReturnParams[0][0].Value.ToString().Split(',');
                if (fields.Count() > 0 && !string.IsNullOrEmpty(fields[0]))
                {
                    // チェック対象のCDを配列で取得
                     var selectedCds = this.form.ParentControls.OfType<GrapeCity.Win.MultiRow.Cell>().Where(c => c.Name == fields[0].Trim()).FirstOrDefault().Value.ToString().Split(',');
                    foreach (var cd in selectedCds)
                    {
                        var row = this.form.masterDetail.Rows.Where(r => r.Cells["CD"].Value != null && r.Cells["CD"].Value.ToString() == cd).FirstOrDefault();
                        if (row != null)
                        {
                            row.Cells[0].Value = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            // 選択ボタン(F1)イベント生成
            this.form.bt_func1.Click -= new EventHandler(this.form.DataSelection);
            this.form.bt_func1.Click += new EventHandler(this.form.DataSelection);

            // 閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click -= new EventHandler(this.form.FormClose);
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);

            // キー押下処理イベント生成
            this.form.KeyUp -= new KeyEventHandler(this.form.DetailKeyUp);
            this.form.KeyUp += new KeyEventHandler(this.form.DetailKeyUp);
        }

        /// <summary>
        /// マスタデータの検索を行い加工する
        /// </summary>
        internal void MasterSearch()
        {
            var dt = this.GetDataTable();
            dtOld = dt.Copy();

            // 読取専用
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = false;
            }

            // MultiRowへデータの設定
            DataTable table = this.GetStringDataTable(dt);
            var createLogic = new r_framework.Logic.MultiRowCreateLogic();
            if (this.form.WindowId == WINDOW_ID.M_HOUKOKUSHO_BUNRUI_UNPAN)
            {
                this.form.masterDetail.Template = createLogic.CreateMultiRowUnpan(this.HeaderList, table, true);
            }
            else 
            {
                this.form.masterDetail.Template = createLogic.CreateMultiRow(this.HeaderList, table);
            }
            this.form.masterDetail.DataSource = dt;

            //for (int i = 0; i < this.form.masterDetail.ColumnHeaders[0].Cells.Count; i++)
            //{
            //    ColumnHeaderCell HeadedrCell = (ColumnHeaderCell)this.form.masterDetail.ColumnHeaders[0].Cells[i];
            //    HeadedrCell.Value = HeaderList[i];
            //}

            foreach (var Rows in this.form.masterDetail.Rows)
            {
                Rows.Cells[0].Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
                foreach (var cell in Rows.Cells)
                {
                    if (cell.Name != "CHECKED" && cell.Name != "TSUMIKAE")
                    {
                        cell.ReadOnly = true;
                    }

                    if (cell.Name == "CHECKED")
                    {
                        cell.ReadOnly = false;
                    }

                    if (cell.Name == "TSUMIKAE")
                    {
                        cell.ReadOnly = false;
                    }
                }
                /*if (Rows["KYOKA_KBN"] != null && !string.IsNullOrEmpty(Rows["KYOKA_KBN"].Value.ToString()))
                {
                    Rows["CHECKED"].Value = true;
                }*/

                if (cdList != null)
                {
                    for (int i = 0; i < cdList.Length; i++)
                    {
                        var cd = cdList[i];
                        if (cd.Equals(Rows["CD"].Value))
                        {
                            Rows["CHECKED"].Value = true;
                            if (this.form.WindowId == WINDOW_ID.M_HOUKOKUSHO_BUNRUI_UNPAN)
                            {
                                var tsumikae = tsumikaeList[i];
                                if (!string.IsNullOrEmpty(tsumikae))
                                {
                                    Rows["TSUMIKAE"].Value = Convert.ToBoolean(tsumikae);
                                }
                            }
                        }
                    }
                    //foreach (var item in cdList)
                    //{
                    //    if (item.Equals(Rows["CD"].Value))
                    //    {
                    //        Rows["CHECKED"].Value = true;
                    //    }
                    //}
                }
                //if (this.form.WindowId == WINDOW_ID.M_HOUKOKUSHO_BUNRUI_UNPAN)
                //{
                //    if (Rows["TSUMIKAE"] != null && !string.IsNullOrEmpty(Rows["TSUMIKAE"].Value.ToString()))
                //    {
                //        Rows["TSUMIKAE"].Value = (bool)Rows["TSUMIKAE"].Value;
                //    }
                //}
            }

            this.form.masterDetail.EndEdit();
        }

        /// <summary>
        /// DataTableの取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            var whereSql = string.Empty;

            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_HAIKI_SHURUI:
                    whereSql = SqlCreateUtility.CreatePopupSql(this.form.popupWindowSetting,this.form.WindowId, this.form.ParentControls);
                    var haikiShuruiDao = dao as IM_HAIKI_SHURUIDao;
                    if (haikiShuruiDao != null)
                    {
                        dt = haikiShuruiDao.GetHaikiShuruiDataSql(whereSql);
                    }
                    break;

                case WINDOW_ID.M_HOUKOKUSHO_BUNRUI:
                    whereSql = SqlCreateUtility.CreatePopupSql(this.form.popupWindowSetting, this.form.WindowId, this.form.ParentControls);

                    // 画面から来た絞込み情報で条件句を作成
                    bool existSearchParam = false;  // popupSearchSendParamからWHEREが生成されたかどうかのフラグ
                    StringBuilder sb = new StringBuilder(" ");
                    foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
                    {
                        string where = CreateWhereStrFromScreen(popupSearchSendParam, "M_CHIIKIBETSU_KYOKA_MEIGARA", ref existSearchParam);
                        //sb.Append(where);
                    }

                    var houkokushoBunruiDao = dao as IM_HOUKOKUSHO_BUNRUIDao;
                    if (houkokushoBunruiDao != null)
                    {
                        dt = houkokushoBunruiDao.GetAllMasterDataForPopup(whereSql);
                    }
                    this.form.ReturnParams = null;
                    break;

                case WINDOW_ID.M_HOUKOKUSHO_BUNRUI_UNPAN:
                    if (this.form.popupWindowSetting.Count > 0)
                    {
                        whereSql = SqlCreateUtility.CreatePopupSql(this.form.popupWindowSetting, this.form.WindowId, this.form.ParentControls);
                    }
                    else
                    {
                        whereSql = string.Empty;
                        // 画面から来た絞込み情報で条件句を作成
                        existSearchParam = false;  // popupSearchSendParamからWHEREが生成されたかどうかのフラグ
                        sb = new StringBuilder(" ");
                        foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
                        {
                            string where = CreateWhereStrFromScreen(popupSearchSendParam, "M_CHIIKIBETSU_KYOKA_MEIGARA", ref existSearchParam);
                            sb.Append(where);
                        }

                        // popupSearchSendParam分のWHERE句をまとめるため
                        if (existSearchParam)
                        {
                            sb.Append(" )");
                        }
                        if (sb.Length > 0)
                        {
                            whereSql += sb.ToString();
                        }
                    }
                    string whereSql1 = "WHERE M_HOUKOKUSHO_BUNRUI.DELETE_FLG = 0) ";
                    var houkokushoBunruiDao1 = dao as IM_HOUKOKUSHO_BUNRUIDao;
                    if (houkokushoBunruiDao1 != null)
                    {
                        dt = houkokushoBunruiDao1.GetAllMasterDataForPopup1(whereSql, whereSql1);
                    }
                    this.form.ReturnParams = null;
                    break;
            }

            return dt;
        }

        /// <summary>
        /// 先頭カラムが文字列のDataTableの取得
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetStringDataTable(DataTable dt)
        {
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            return table;
        }

        /// <summary>
        /// Data Update
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        /*public void UpdateData()
        {
            int nIndex = 0;

            foreach (Row row in this.form.masterDetail.Rows)
            {
                string strAfter = Convert.ToString(row.Cells["TSUMIKAE"].Value);
                string strBefore = Convert.ToString(this.dtOld.Rows[nIndex]["TSUMIKAE"]);
                if (strAfter.Equals(strBefore))
                {
                    nIndex++;
                    continue;
                }
                else
                {
                    M_CHIIKIBETSU_KYOKA_MEIGARA ChikibetsuKyokaMeigara = new M_CHIIKIBETSU_KYOKA_MEIGARA();
                    ChikibetsuKyokaMeigara.KYOKA_KBN = Convert.ToInt16(row.Cells["KYOKA_KBN"].Value);
                    ChikibetsuKyokaMeigara.GYOUSHA_CD = Convert.ToString(row.Cells["GYOUSHA_CD"].Value);
                    ChikibetsuKyokaMeigara.GENBA_CD = Convert.ToString(row.Cells["GENBA_CD"].Value);
                    ChikibetsuKyokaMeigara.CHIIKI_CD = Convert.ToString(row.Cells["CHIIKI_CD"].Value);
                    ChikibetsuKyokaMeigara.TOKUBETSU_KANRI_KBN = Convert.ToBoolean(row.Cells["TOKUBETSU_KANRI_KBN"].Value);
                    ChikibetsuKyokaMeigara.HOUKOKUSHO_BUNRUI_CD = Convert.ToString(row.Cells["CD"].Value);
                    ChikibetsuKyokaMeigara.TSUMIKAE = Convert.ToBoolean(row.Cells["TSUMIKAE"].Value);
                    this.Chiikibetsudao.UpdateData(ChikibetsuKyokaMeigara);
                    nIndex++;
                }
            }
        }*/
        /// <summary>
        /// 画面から来た絞込み情報による条件句を生成
        /// </summary>
        /// <param name="dto">PopupSearchSendParamDto</param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="existSearchParam">条件が生成されたかどうかのフラグ</param>
        /// <returns></returns>
        private string CreateWhereStrFromScreen(PopupSearchSendParamDto dto, string tableName, ref bool existSearchParam)
        {
            StringBuilder sb = new StringBuilder();

            // 括弧付きの条件対応
            if (dto.SubCondition != null && 0 < dto.SubCondition.Count)
            {
                bool subExistSearchParam = false;

                foreach (PopupSearchSendParamDto popupSearchSendParam in dto.SubCondition)
                {
                    string where = CreateWhereStrFromScreen(popupSearchSendParam, tableName, ref subExistSearchParam);
                    sb.Append(where);
                }

                // 条件をまとめるため
                if (subExistSearchParam)
                {
                    sb.Append(")");
                }
                return sb.ToString();
            }
            // 通常のWHERE句を生成
            else
            {
                if (string.IsNullOrEmpty(dto.KeyName))
                {
                    return sb.ToString();
                }


                // 絞込み条件にControlが指定されていればそれを使い、無ければValueを使用する
                // 両方無ければ条件句の生成はしない
                string whereValue = this.CreateWhere(dto);

                if (string.IsNullOrEmpty(whereValue))
                {
                    return sb.ToString();
                }

                if (dto.KeyName.Equals("HOUKOKUSHO_BUNRUI_CD"))
                {
                    whereValue = whereValue.Substring(3, whereValue.Length - 4);
                    cdList = whereValue.Split(',');
                    return sb.ToString();
                }

                if (dto.KeyName.Equals("TSUMIKAE"))
                {
                    whereValue = whereValue.Substring(3, whereValue.Length - 4);
                    tsumikaeList = whereValue.Split(',');
                    return sb.ToString();
                }

                sb.Append(dto.And_Or.ToString());

                if (!existSearchParam)
                {
                    // 先頭配列は「(」で括る
                    sb.Append(" (");
                }

                if (dto.KeyName.Contains("."))
                {
                    sb.Append(" (")
                        .Append(dto.KeyName)
                        .Append(" ")
                        .Append(whereValue)
                        .Append(") ");
                }
                else
                {
                    sb.Append(" (")
                        .Append(tableName)
                        .Append(".")
                        .Append(dto.KeyName)
                        .Append(" ")
                        .Append(whereValue)
                        .Append(" ) ");
                }

                existSearchParam = true;

                return sb.ToString();
            }
        }

        /// <summary>
        /// PopupSearchSendParamDtoからWHERE句を作成します。
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="whereValue"></param>
        private string CreateWhere(PopupSearchSendParamDto dto)
        {
            CNNECTOR_SIGNS sqlConnectorSign = CNNECTOR_SIGNS.EQUALS;  // KeyとValueをつなぐ符号
            string returnStr = string.Empty;

            if (dto == null)
            {
                return returnStr;
            }
            if (dto.Control == null || string.IsNullOrEmpty(dto.Control))
            {
                if (dto.Value != null && !string.IsNullOrEmpty(dto.Value))
                {
                    if (dto.Value.Contains(","))
                    {
                        sqlConnectorSign = CNNECTOR_SIGNS.IN;
                        // 使用側で"'"を意識しないで使わせたいので、FW側で"'"をつける
                        string[] valueList = dto.Value.Replace(" ", "").Split(',');
                        foreach (string tempValue in valueList)
                        {
                            if (string.IsNullOrEmpty(returnStr))
                            {
                                returnStr = "'" + tempValue + "'";
                            }
                            else
                            {
                                returnStr = returnStr + ", '" + tempValue + "'";
                            }
                        }
                        returnStr = "(" + returnStr + ")";
                    }
                    else
                    {
                        sqlConnectorSign = CNNECTOR_SIGNS.EQUALS;
                        returnStr = "'" + dto.Value + "'";
                    }
                }
            }
            else
            {
                object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { dto.Control });
                string controlText = PropertyUtility.GetTextOrValue(control[0]);
                if (control != null && !string.IsNullOrEmpty(controlText))
                {
                    // 複数同じ名前のコントロールは存在しないはず
                    returnStr = "'" + controlText + "'";
                }
            }

            if (!string.IsNullOrEmpty(returnStr))
            {
                return CNNECTOR_SIGNSExt.ToTypeString(sqlConnectorSign) + " " + returnStr;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// CNNECTOR_SIGNSを文字列に変換する
        /// </summary>
        public static class CNNECTOR_SIGNSExt
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            public static string ToTypeString(CNNECTOR_SIGNS e)
            {
                switch (e)
                {
                    case CNNECTOR_SIGNS.EQUALS:
                        return "=";

                    case CNNECTOR_SIGNS.IN:
                        return "IN";
                }
                return String.Empty;
            }
        }

        /// <summary>
        /// 絞り込み条件で使用する符号
        /// </summary>
        public enum CNNECTOR_SIGNS
        {
            EQUALS = 0,
            IN = 1
        }
    }
}
