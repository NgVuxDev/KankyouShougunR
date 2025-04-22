// $Id: KensakuKyoutsuuPopupForMultiKeyLogic.cs 43381 2015-03-02 00:24:25Z nagata $
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using KensakuKyoutsuuPopupForMultiKey1.APP;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Setting;
using r_framework.Utility;

namespace KensakuKyoutsuuPopupForMultiKey1.Logic
{
    /// <summary>
    /// 複数キー用検索共通ポップアップロジック
    /// </summary>
    public class KensakuKyoutsuuPopupForMultiKeyLogic
    {

        #region フィールド

        /// <summary>
        /// バインドするカラム名一覧
        /// </summary>
        internal string[] bindColumnNames = new string[] { "" };

        /// <summary>
        /// 表示カラム名
        /// </summary>
        internal string[] displayColumnNames = new string[] { };

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "KensakuKyoutsuuPopupForMultiKey1.Setting.ButtonSetting.xml";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private KensakuKyoutsuuPopupForMultiKeyForm form;

        /// <summary>
        /// 共通一覧画面にて利用されるDao
        /// </summary>
        private IS2Dao dao;

        /// <summary>
        /// Daoに引き渡すSQLファイルのパス
        /// </summary>
        private string executeSqlFilePath = string.Empty;

        /// <summary>
        /// WHERE句
        /// </summary>
        private string whereStr = string.Empty;

        /// <summary>
        /// ORDER BY句
        /// </summary>
        private string orderStr = string.Empty;

        /// <summary>
        /// 検索条件
        /// </summary>
        public SuperEntity SearchInfo { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 起動元への戻り値(カラム名)
        /// </summary>
        public string[] returnParamNames = new string[] { };

        private bool ParentFilterDispFlag = true;

        /// <summary>
        /// 絞り込み条件で使用する符号
        /// </summary>
        public enum CNNECTOR_SIGNS
        {
            EQUALS = 0,
            IN = 1
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
        /// 頭文字フィルタのための文字列
        /// ※時間があれば、Unicodeから自動で以下の文字列を生成するのを作りたい
        /// なお、DataGridViewのRowFilterには正規表現が使用できないようなので、以下のようにベタで文字を定義する
        /// </summary>
        private string Agyou = "'ア', 'イ', 'ウ', 'エ', 'オ', 'ァ', 'ィ', 'ゥ', 'ェ', 'ォ'";
        private string KAgyou = "'カ', 'キ', 'ク', 'ケ', 'コ', 'ガ', 'ギ', 'グ', 'ゲ', 'ゴ'";
        private string SAgyou = "'サ', 'シ', 'ス', 'セ', 'ソ', 'ザ', 'ジ', 'ズ', 'ゼ', 'ゾ'";
        private string TAgyou = "'タ', 'チ', 'ツ', 'テ', 'ト', 'ダ', 'ヂ', 'ヅ', 'デ', 'ド', 'ッ'";
        private string NAgyou = "'ナ', 'ニ', 'ヌ', 'ネ', 'ノ'";
        private string HAgyou = "'ハ', 'ヒ', 'フ', 'ヘ', 'ホ', 'バ', 'ビ', 'ブ', 'ベ', 'ボ', 'パ', 'ピ', 'プ', 'ペ', 'ポ'";
        private string MAgyou = "'マ', 'ミ', 'ム', 'メ', 'モ'";
        private string YAgyou = "'ヤ', 'ユ', 'ヨ', 'ャ', 'ュ', 'ョ'";
        private string RAgyou = "'ラ', 'リ', 'ル', 'レ', 'ロ'";
        private string WAgyou = "'ワ', 'ヮ', 'ヰ', 'ヱ', 'ヲ', 'ン', 'ヴ', 'ヵ', 'ヶ'";
        private string alphanumeric = "'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', "
            + "'０', '１', '２', '３', '４', '５', '６', '７', '８', '９', "
            + "'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', "
            + "'Ａ', 'Ｂ', 'Ｃ', 'Ｄ', 'Ｅ', 'Ｆ', 'Ｇ', 'Ｈ', 'Ｉ', 'Ｊ', 'Ｋ', 'Ｌ', 'Ｍ', 'Ｎ', 'Ｏ', 'Ｐ', 'Ｑ', 'Ｒ', 'Ｓ', 'Ｔ', 'Ｕ', 'Ｖ', 'Ｗ', 'Ｘ', 'Ｙ', 'Ｚ', "
            + "'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', "
            + "'ａ', 'ｂ', 'ｃ', 'ｄ', 'ｅ', 'ｆ', 'ｇ', 'ｈ', 'ｉ', 'ｊ', 'ｋ', 'ｌ', 'ｍ', 'ｎ', 'ｏ', 'ｐ', 'ｑ', 'ｒ', 'ｓ', 'ｔ', 'ｕ', 'ｖ', 'ｗ', 'ｘ', 'ｙ', 'ｚ'";

        /// <summary>
        /// M_GENBA_SHITENの表示項目
        /// </summary>
        private enum BANK_SHITEN_COLUMNS
        {
            BANK_CD,
            BANK_NAME_RYAKU,
            BANK_SHITEN_CD,
            BANK_SHIETN_NAME_RYAKU,
            BANK_SHITEN_FURIGANA,
            KOUZA_SHURUI,
            KOUZA_NO,
            KOUZA_NAME
        }

        /// <summary>
        /// M_GENBAの表示項目
        /// </summary>
        private enum GENBA_COLUMNS
        {
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU,
            GENBA_CD,
            GENBA_NAME_RYAKU,
            GENBA_FURIGANA,
            TODOUFUKEN_NAME_RYAKU,
            GENBA_ADDRESS1,
            GENBA_TEL
        }

        /// <summary>
        /// M_HINMEIの表示項目
        /// </summary>
        private enum HINMEI_COLUMNS
        {
            HINMEI_CD,
            HINMEI_NAME_RYAKU,
            HINMEI_FURIGANA,
            SHURUI_CD,
            SHURUI_NAME_RYAKU
        }

        /// <summary>
        /// M_HINMEIの表示項目
        /// </summary>
        private enum HINMEI_COLUMNS_FOR_MULTISELECT
        {
            SELECT_CHECK,
            HINMEI_CD,
            HINMEI_NAME_RYAKU,
            HINMEI_FURIGANA,
            SHURUI_CD,
            SHURUI_NAME_RYAKU
        }

        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal KensakuKyoutsuuPopupForMultiKeyLogic(KensakuKyoutsuuPopupForMultiKeyForm targetForm)
        {
            this.form = targetForm;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {

            //クリアボタン(F5)イベント生成
            this.form.bt_func5.Click += new EventHandler(this.form.Clear);

            //検索ボタン(F8)イベント生成
            this.form.bt_func8.Click += new EventHandler(this.form.Search);

            //確定ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.Selected);

            //取消ボタン(F11)イベント生成
            this.form.bt_func11.Click += new EventHandler(this.form.MoveToSort);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.Close);
        }

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            //ボタンの初期化
            this.ButtonInit();
            // 画面タイトルやDaoを初期化
            this.DisplyInit();

            this.form.PARENT_CONDITION_ITEM.Text = "3";
            this.form.PARENT_CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.form.CHILD_CONDITION_ITEM.Text = "3";
            this.form.CHILD_CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.form.FILTER_BOIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.FILTER_SHIIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.customDataGridView1.AllowUserToAddRows = false;

            // 条件によってPARENT_CONDITION_ITEMの初期かもするので初期化の一番最後に実行
            this.ChangeDisplayFilter();
        }

        /// <summary>
        /// 画面のタイトルなどを初期化を行う
        /// </summary>
        private void DisplyInit()
        {
            // PopupGetMasterFieldプロパティから返却値を設定
            string[] popupGetMasterField = new string[] { };
            if (!string.IsNullOrEmpty(this.form.PopupGetMasterField))
            {
                string str = this.form.PopupGetMasterField.Replace(" ", "");
                str = str.Replace("　", "");
                if (!string.IsNullOrEmpty(str))
                {
                    popupGetMasterField = str.Split(',');
                }
            }

            string parentLabel = string.Empty;
            string childLabel = string.Empty;
            string hintTextConditon = string.Empty;
            bool setCheckBoxFirst = false;

            switch (this.form.WindowId)
            {
                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.M_BANK_SHITEN:
                    this.dao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
                    parentLabel = "銀行";
                    childLabel = "銀行支店";
                    hintTextConditon = "1～3,7";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey1.Sql.GetBankShitenDataSql.sql";
                    this.bindColumnNames = Enum.GetNames(typeof(BANK_SHITEN_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "銀行CD", "銀行名", "銀行支店CD", "銀行支店名", "銀行支店ﾌﾘｶﾞﾅ", "口座種類", "口座番号", "口座名義" };

                    // レイアウト調整
                    this.form.PARENT_CONDITION4.Visible = false;
                    this.form.PARENT_CONDITION5.Visible = false;
                    this.form.PARENT_CONDITION6.Visible = false;
                    this.form.CHILD_CONDITION4.Visible = false;
                    this.form.CHILD_CONDITION5.Visible = false;
                    this.form.CHILD_CONDITION6.Visible = false;

                    this.form.PARENT_CONDITION7.Location = new Point(239, 6);
                    this.form.PARENT_CONDITION_VALUE.Location = new Point(326, 6);
                    this.form.CHILD_CONDITION7.Location = new Point(242, 8);
                    this.form.CHILD_CONDITION_VALUE.Location = new Point(329, 8);

                    this.form.PARENT_CONDITION_ITEM.CharacterLimitList = new char[] { '1', '2', '3', '7' };
                    this.form.CHILD_CONDITION_ITEM.CharacterLimitList = new char[] { '1', '2', '3', '7' };

                    break;

                case WINDOW_ID.M_GENBA:
                    this.dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                    parentLabel = "業者";
                    childLabel = "現場";
                    hintTextConditon = "1～7";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey1.Sql.GetGenbaDataSql.sql";
                    this.bindColumnNames = Enum.GetNames(typeof(GENBA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "業者CD", "業者名", "現場CD", "現場名", "現場ﾌﾘｶﾞﾅ", "都道府県", "現場住所", "現場電話番号" };
                    break;

                case WINDOW_ID.M_HINMEI:
                    this.dao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
                    parentLabel = "種類";
                    childLabel = "品名";
                    hintTextConditon = "1～3,7";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey1.Sql.GetHinmeiDataSql.sql";
                    this.bindColumnNames = Enum.GetNames(typeof(HINMEI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "品名CD", "品名", "ﾌﾘｶﾞﾅ", "種類CD", "種類名" };
                    if (this.form.PopupMultiSelect)
                    {
                        setCheckBoxFirst = true;
                        this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey1.Sql.GetHinmeiDataForMultiSelectSql.sql";
                        this.bindColumnNames = Enum.GetNames(typeof(HINMEI_COLUMNS_FOR_MULTISELECT));
                        this.displayColumnNames = new string[] { string.Empty, "品名CD", "品名", "ﾌﾘｶﾞﾅ", "種類CD", "種類名" };
                    }

                    // レイアウト調整
                    this.form.PARENT_CONDITION4.Visible = false;
                    this.form.PARENT_CONDITION5.Visible = false;
                    this.form.PARENT_CONDITION6.Visible = false;
                    this.form.CHILD_CONDITION4.Visible = false;
                    this.form.CHILD_CONDITION5.Visible = false;
                    this.form.CHILD_CONDITION6.Visible = false;

                    this.form.PARENT_CONDITION7.Location = new Point(239, 6);
                    this.form.PARENT_CONDITION_VALUE.Location = new Point(326, 6);
                    this.form.CHILD_CONDITION7.Location = new Point(242, 8);
                    this.form.CHILD_CONDITION_VALUE.Location = new Point(329, 8);

                    this.form.PARENT_CONDITION_ITEM.CharacterLimitList = new char[] { '1', '2', '3', '7' };
                    this.form.CHILD_CONDITION_ITEM.CharacterLimitList = new char[] { '1', '2', '3', '7' };

                    break;

                default:
                    break;
            }

            // 画面のラベル表示名を更新
            this.form.lb_title.Text = childLabel + "検索";
            this.form.label16.Text = parentLabel + "検索条件";
            this.form.label3.Text = childLabel + "検索条件";
            this.form.label1.Text = childLabel + "ﾌﾘｶﾞﾅ頭文字(母音)";
            this.form.label2.Text = childLabel + "ﾌﾘｶﾞﾅ頭文字(子音)";

            // Formタイトルの初期化
            this.form.Text = this.form.lb_title.Text;

            // ヒントテキストを更新
            this.form.PARENT_CONDITION_ITEM.Tag = string.Format("【{0}】のいずれかで入力して下さい", hintTextConditon);
            this.form.PARENT_CONDITION_VALUE.Tag = string.Format("{0}について検索条件を入力して下さい", parentLabel);
            this.form.CHILD_CONDITION_ITEM.Tag = string.Format("【{0}】のいずれかで入力して下さい", hintTextConditon);
            this.form.CHILD_CONDITION_VALUE.Tag = string.Format("{0}について検索条件を入力して下さい", childLabel);

            // カラム設定(画面ごとに表示カラムは変わらないはず)
            for (int i = 0; i < displayColumnNames.Length; i++)
            {
                if (i == 0 && setCheckBoxFirst)
                {
                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                    column.DataPropertyName = bindColumnNames[i];
                    column.Name = bindColumnNames[i];
                    column.HeaderText = displayColumnNames[i];
                    //column.CellTemplate = new DataGridViewCheckBoxCell();
                    column.ReadOnly = false;
                    this.form.customDataGridView1.Columns.Add(column);
                }
                else
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.DataPropertyName = bindColumnNames[i];
                    column.Name = bindColumnNames[i];
                    column.HeaderText = displayColumnNames[i];
                    column.ReadOnly = true;
                    this.form.customDataGridView1.Columns.Add(column);
                }
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CcreateButtonInfo();
            var parentForm = (SuperPopupForm)this.form;
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CcreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// 検索条件の入力項目制御処理
        /// 起動ポイントでpopupSearchSendParamが指定されている場合、
        /// Enabledにする検索条件があるため、ここでコントロール。
        /// </summary>
        private void ChangeDisplayFilter()
        {
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
                if (popupSearchSendParam == null || string.IsNullOrEmpty(popupSearchSendParam.KeyName))
                {
                    continue;
                }

                switch (popupSearchSendParam.KeyName)
                {
                    case "BANK_CD":
                        if (this.form.WindowId.Equals(WINDOW_ID.M_BANK_SHITEN))
                        {
                            this.form.PARENT_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                            if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                            {
                                this.form.panel1.Enabled = false;
                                this.ParentFilterDispFlag = false;
                                this.form.PARENT_CONDITION_ITEM.Text = "1";
                            }
                        }
                        break;

                    case "GYOUSHA_CD":
                        if (this.form.WindowId.Equals(WINDOW_ID.M_GENBA))
                        {
                            this.form.PARENT_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                            if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                            {
                                this.form.panel1.Enabled = false;
                                this.ParentFilterDispFlag = false;
                                this.form.PARENT_CONDITION_ITEM.Text = "1";
                            }
                        }
                        break;

                    case "SHURUI_CD":
                        if (this.form.WindowId.Equals(WINDOW_ID.M_HINMEI))
                        {
                            this.form.PARENT_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                            if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                            {
                                this.form.panel1.Enabled = false;
                                this.ParentFilterDispFlag = false;
                                this.form.PARENT_CONDITION_ITEM.Text = "1";
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion

        #region イベント用処理

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            // 検索条件生成
            this.SetSearchString();

            DataTable dt = new DataTable();
            // 基本的なスクリプトを取得
            var thisAssembly = Assembly.GetExecutingAssembly();
            using (var resourceStream = thisAssembly.GetManifestResourceStream(this.executeSqlFilePath))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    dt = this.dao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + this.whereStr + this.orderStr);
                    sqlStr.Close();
                }
            }

            // DataTable table = GetStringDataTable(dt);
            this.SearchResult = dt;

            // 頭文字絞込み
            this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();

            foreach (DataColumn col in this.SearchResult.Columns)
            {
                if (col.ColumnName.Equals("SELECT_CHECK"))
                {
                    col.ReadOnly = false;
                }
            }

            this.form.customDataGridView1.DataSource = this.SearchResult;
            //this.form.customDataGridView1.ReadOnly = true;
            this.form.customDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            int returnCount = this.SearchResult.Rows == null ? 0 : 1;

            LogUtility.DebugMethodEnd(returnCount);
            return returnCount;
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void InvokeInitialSort()
        {
            if (this.SearchResult != null)
            {
                // 頭文字絞込み
                this.SearchResult.DefaultView.RowFilter = string.Empty;
                this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();
                this.form.customDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
        }

        /// <summary>
        /// 選択データ決定処理
        /// </summary>
        internal void ElementDecision()
        {
            if (this.form.customDataGridView1.CurrentRow == null)
            {
                return;
            }

            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            List<PopupReturnParam> setParam = new List<PopupReturnParam>();
            if (!this.form.PopupMultiSelect)
            {
                for (int i = 0; i < this.returnParamNames.Length; i++)
                {
                    PopupReturnParam popupParam = new PopupReturnParam();
                    var setDate = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[this.returnParamNames[i]];

                    var control = setDate as ICustomControl;

                    popupParam.Key = "Value";

                    popupParam.Value = setDate.Value.ToString();

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
            }
            else
            {
                for (int i = 0; i < this.returnParamNames.Length; i++)
                {
                    List<string> list = new List<string>();
                    for (int j = 0; j < this.form.customDataGridView1.Rows.Count; j++)
                    {
                        if (((bool)this.form.customDataGridView1.Rows[j].Cells[0].Value))
                        {
                            var setData = this.form.customDataGridView1.Rows[j].Cells[this.returnParamNames[i]];
                            list.Add(setData.Value.ToString());
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
            }

            this.form.ReturnParams = setParamList;
            this.form.Close();
        }

        /// <summary>
        /// 検索条件入力欄のIME制御
        /// </summary>
        internal void ImeControlParentCondition()
        {
            switch (this.form.PARENT_CONDITION_ITEM.Text)
            {
                case "1":
                    this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                    break;

                case "2":
                    this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "3":
                    this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
                    break;

                case "4":
                    this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "5":
                    this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "6":
                    this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                    break;

                case "7":
                    this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 検索条件入力欄のIME制御
        /// </summary>
        internal void ImeControlChildCondition()
        {
            switch (this.form.CHILD_CONDITION_ITEM.Text)
            {
                case "1":
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                    break;

                case "2":
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "3":
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
                    break;

                case "4":
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "5":
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "6":
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                    break;

                case "7":
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// クリア処理
        /// </summary>
        internal void Clear()
        {
            this.form.PARENT_CONDITION_ITEM.Text = "3";
            this.form.PARENT_CONDITION_VALUE.Text = string.Empty;
            this.form.CHILD_CONDITION_ITEM.Text = "3";
            this.form.CHILD_CONDITION_VALUE.Text = string.Empty;
            this.form.FILTER_BOIN_VALUE.Text = string.Empty;
            this.form.FILTER_SHIIN_VALUE.Text = string.Empty;

            DataTable dt = (DataTable)this.form.customDataGridView1.DataSource;
            if (dt != null)
            {
                dt.Rows.Clear();
                //this.form.customDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            
            this.form.PARENT_CONDITION_ITEM.Focus();
        }

        #endregion

        #region Utility

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            // 初期化
            this.whereStr = string.Empty;

            // 検索画面が増えたら以下に設定を追加していく
            string tableName = string.Empty;
            string parentTableName = string.Empty;
            switch (this.form.WindowId)
            {
                // TODO:.Nemeでちゃんとクラス名取れているか確認
                case WINDOW_ID.M_BANK_SHITEN:
                    this.SearchInfo = new M_BANK_SHITEN();
                    tableName = typeof(M_BANK_SHITEN).Name;
                    parentTableName = typeof(M_BANK).Name;
                    break;

                case WINDOW_ID.M_GENBA:
                    this.SearchInfo = new M_GENBA();
                    tableName = typeof(M_GENBA).Name;
                    parentTableName = typeof(M_GYOUSHA).Name;
                    break;

                case WINDOW_ID.M_HINMEI:
                    this.SearchInfo = new M_HINMEI();
                    tableName = typeof(M_HINMEI).Name;
                    parentTableName = typeof(M_SHURUI).Name;
                    break;

                default:
                    break;
            }

            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_GENBA:
                    this.orderStr = " ORDER BY " + tableName + ".GYOUSHA_CD , " + tableName + ".GENBA_CD ";
                    break;
                case WINDOW_ID.M_BANK_SHITEN:
                    this.orderStr = " ORDER BY " + tableName + ".BANK_CD , " + tableName + ".BANK_SHITEN_CD ";
                    break;
                case WINDOW_ID.M_HINMEI:
                    this.orderStr = " ORDER BY " + tableName + ".HINMEI_CD ";
                    break;
            }

            // カラム名を動的に指定するために必要
            var ColumnHeaderName = tableName.Substring(2, tableName.Length - 2);
            var parentColumnHeaderName = parentTableName.Substring(2, parentTableName.Length - 2);

            // 親の検索条件
            // 起動時にParentの条件が指定されている場合はPearent用条件句を生成してはいけない
            if (ParentFilterDispFlag)
            {
                if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                {
                    // シングルクォートは2つ重ねる
                    var condition = SqlCreateUtility.CounterplanEscapeSequence(this.form.PARENT_CONDITION_VALUE.Text);
                    switch (this.form.PARENT_CONDITION_ITEM.Text)
                    {
                        case "1":
                            // ｺｰﾄﾞ
                            this.whereStr = " AND " + parentTableName + "." + parentColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                            break;

                        case "2":
                            // 略称名
                            this.whereStr = " AND " + parentTableName + "." + parentColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                            break;

                        case "3":
                            // ﾌﾘｶﾞﾅ
                            this.whereStr = " AND " + parentTableName + "." + parentColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                            break;

                        case "4":
                            // 都道府県
                            // もし数値変換できない場合は設定しない
                            this.whereStr = " AND M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                            break;

                        case "5":
                            // 住所
                            this.whereStr = " AND " + parentTableName + "." + parentColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                            break;

                        case "6":
                            // 電話
                            this.whereStr = "AND " + parentTableName + "." + parentColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                            break;

                        case "7":
                            // ﾌﾘｰ
                            // ﾌﾘｰでは1～6のすべてに対して検索をかける
                            this.whereStr = " AND (" + parentTableName + "." + parentColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                            this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                            this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                            if (this.form.WindowId == WINDOW_ID.M_GENBA)
                            {
                                this.whereStr = this.whereStr + " OR M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                            }
                            this.whereStr = this.whereStr + ")";
                            break;

                        default:
                            break;
                    }
                }
            }

            // 子の検索条件
            if (!string.IsNullOrEmpty(this.form.CHILD_CONDITION_VALUE.Text))
            {
                // シングルクォートは2つ重ねる
                var condition = SqlCreateUtility.CounterplanEscapeSequence(this.form.CHILD_CONDITION_VALUE.Text);
                switch (this.form.CHILD_CONDITION_ITEM.Text)
                {
                    case "1":
                        // ｺｰﾄﾞ
                        this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                        break;

                    case "2":
                        // 略称名
                        if (this.form.WindowId == WINDOW_ID.M_BANK_SHITEN)
                        {
                            this.whereStr = this.whereStr + " AND " + tableName + ".BANK_SHIETN_NAME_RYAKU LIKE '%" + condition + "%'";
                        }
                        else
                        {
                            this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                        }
                        break;

                    case "3":
                        // ﾌﾘｶﾞﾅ
                        this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                        break;

                    case "4":
                        // 都道府県
                        // もし数値変換できない場合は設定しない
                        this.whereStr = this.whereStr + " AND M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                        break;

                    case "5":
                        // 住所
                        this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                        break;

                    case "6":
                        // 電話
                        this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                        break;

                    case "7":
                        // ﾌﾘｰ
                        // ﾌﾘｰでは1～6のすべてに対して検索をかける
                        this.whereStr = this.whereStr + " AND (" + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                        if (this.form.WindowId == WINDOW_ID.M_BANK_SHITEN)
                        {
                            this.whereStr = this.whereStr + " OR " + tableName + ".BANK_SHIETN_NAME_RYAKU LIKE '%" + condition + "%'";
                        }
                        else
                        {
                            this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                        }
                        this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                        if (this.form.WindowId == WINDOW_ID.M_GENBA)
                        {
                            this.whereStr = this.whereStr + " OR M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                            this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                            this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                        }
                        this.whereStr = this.whereStr + ")";
                        break;

                    default:
                        break;
                }
            }

            // 画面から来た絞込み情報で条件句を作成
            bool existSearchParam = false;  // popupSearchSendParamからWHEREが生成されたかどうかのフラグ
            StringBuilder sb = new StringBuilder(" ");
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
                string where = CreateWhereStrFromScreen(popupSearchSendParam, tableName, ref existSearchParam);
                sb.Append(where);
            }

            // popupSearchSendParam分のWHERE句をまとめるため
            if (existSearchParam)
            {
                sb.Append(" )");
            }
            if (sb.Length > 0)
            {
                this.whereStr += sb.ToString();
            }
        }

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
        /// 頭文字条件の生成
        /// DataGridViewのフィルタ条件に使用すること
        /// </summary>
        /// <returns>フィルタ条件</returns>
        private String SetInitialSearchString()
        {
            string filterStr = string.Empty;

            // DBアクセスを発生させないためDataGridView用の条件を作成する

            if (string.IsNullOrEmpty(this.form.FILTER_BOIN_VALUE.Text))
            {
                return string.Empty;
            }

            string furiganaCol = GetFuriganaColName();
            if (string.IsNullOrEmpty(this.form.FILTER_SHIIN_VALUE.Text))
            {
                string filterInitialStr = string.Empty;
                // 子音が選択されてなければ選択されている母音のをすべてを表示
                switch (this.form.FILTER_BOIN_VALUE.Text)
                {
                    case "1":
                        filterInitialStr = Agyou;
                        break;

                    case "2":
                        filterInitialStr = KAgyou;
                        break;

                    case "3":
                        filterInitialStr = SAgyou;
                        break;

                    case "4":
                        filterInitialStr = TAgyou;
                        break;

                    case "5":
                        filterInitialStr = NAgyou;
                        break;

                    case "6":
                        filterInitialStr = HAgyou;
                        break;

                    case "7":
                        filterInitialStr = MAgyou;
                        break;

                    case "8":
                        filterInitialStr = YAgyou;
                        break;

                    case "9":
                        filterInitialStr = RAgyou;
                        break;

                    case "10":
                        filterInitialStr = WAgyou;
                        break;

                    case "11":
                        filterInitialStr = alphanumeric;
                        break;

                    case "12":
                        // 以下、うまく動かない
                        filterInitialStr = Agyou + ", " + KAgyou + ", " + SAgyou + ", " + TAgyou + ", " + NAgyou
                            + ", " + HAgyou + ", " + MAgyou + ", " + YAgyou + ", " + RAgyou + ", " + WAgyou + ", " + alphanumeric;
                        break;

                    default:
                        return string.Empty;
                }

                if ("12".Equals(this.form.FILTER_BOIN_VALUE.Text))
                {
                    filterStr = string.Format("substring({0}, 1, 1) not in ({1})", furiganaCol, filterInitialStr);
                }
                else
                {
                    filterStr = string.Format("substring({0}, 1, 1) in ({1})", furiganaCol, filterInitialStr);
                }
            }
            else
            {
                int shiinIndex = -1;
                if (int.TryParse(this.form.FILTER_SHIIN_VALUE.Text, out shiinIndex)
                    && shiinIndex <= this.form.shiinList.Length)
                {
                    var boinIndex = Int32.Parse(this.form.FILTER_BOIN_VALUE.Text);

                    var filterList = new List<List<String>>();

                    var aGyoList = new List<String>() { "'ア','ァ'", "'イ','ィ'", "'ウ','ゥ'", "'エ','ェ'", "'オ','オ'" };
                    var kaGyoList = new List<String>() { "'カ','ガ'", "'キ','ギ'", "'ク','グ'", "'ケ','ゲ'", "'コ','ゴ'" };
                    var saGyoList = new List<String>() { "'サ','ザ'", "'シ','ジ'", "'ス','ズ'", "'セ','ゼ'", "'ソ','ゾ'" };
                    var taGyoList = new List<String>() { "'タ','ダ'", "'チ','ヂ'", "'ツ','ヅ','ッ'", "'テ','デ'", "'ト','ド'" };
                    var naGyoList = new List<String>() { "'ナ'", "'ニ'", "'ヌ'", "'ネ'", "'ノ'" };
                    var haGyoList = new List<String>() { "'ハ','バ','パ'", "'ヒ','ビ','ピ'", "'フ','ブ','プ'", "'ヘ','ベ','ペ'", "'ホ','ボ','ポ'" };
                    var maGyoList = new List<String>() { "'マ'", "'ミ'", "'ム'", "'メ'", "'モ'" };
                    var yaGyoList = new List<String>() { "'ヤ'", "", "'ユ'", "", "'ヨ'" };
                    var raGyoList = new List<String>() { "'ラ'", "'リ'", "'ル'", "'レ'", "'ロ'" };
                    var waGyoList = new List<String>() { "'ワ'", "", "", "", "" };

                    filterList.Add(aGyoList);
                    filterList.Add(kaGyoList);
                    filterList.Add(saGyoList);
                    filterList.Add(taGyoList);
                    filterList.Add(naGyoList);
                    filterList.Add(haGyoList);
                    filterList.Add(maGyoList);
                    filterList.Add(yaGyoList);
                    filterList.Add(raGyoList);
                    filterList.Add(waGyoList);

                    // 子音があれば子音で絞込み
                    filterStr = "substring(" + furiganaCol + ", 1, 1) in (" + filterList[boinIndex - 1][shiinIndex - 1] + ")";
                }
            }

            return filterStr;
        }

        /// <summary>
        /// 該当テーブルからフリガナのカラム名を取得
        /// </summary>
        /// <returns></returns>
        private string GetFuriganaColName()
        {
            string colName = string.Empty;

            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_BANK_SHITEN:
                    colName = BANK_SHITEN_COLUMNS.BANK_SHITEN_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_GENBA:
                    colName = GENBA_COLUMNS.GENBA_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_HINMEI:
                    colName = HINMEI_COLUMNS.HINMEI_FURIGANA.ToString();
                    break;

                default:
                    break;
            }

            return colName;
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
        /// PopupSearchSendParamDtoのControlの値か、Valueを返す
        /// 先にControlをチェックし、存在しなければValueを返す。
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string GetControlOrValue(PopupSearchSendParamDto dto)
        {
            if (dto == null)
            {
                return string.Empty;
            }

            // 絞込み条件にControlが指定されていればそれを使い、無ければValueを使用する
            // 両方無ければ空を返す
            if (dto.Control == null || string.IsNullOrEmpty(dto.Control))
            {
                if (dto.Value != null && !string.IsNullOrEmpty(dto.Value))
                {
                    return dto.Value;
                }
            }
            else
            {
                object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { dto.Control });
                if (control != null)
                {
                    // 複数同じ名前のコントロールは存在しないはず
                    return PropertyUtility.GetTextOrValue(control[0]);
                }
            }

            return string.Empty;
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
                            string sqlTempValue = SqlCreateUtility.CounterplanEscapeSequence(tempValue);
                            if (string.IsNullOrEmpty(returnStr))
                            {
                                returnStr = "'" + sqlTempValue + "'";
                            }
                            else
                            {
                                returnStr = returnStr + ", '" + sqlTempValue + "'";
                            }
                        }
                        returnStr = "(" + returnStr + ")";
                    }
                    else
                    {
                        string sqlValue = SqlCreateUtility.CounterplanEscapeSequence(dto.Value);
                        sqlConnectorSign = CNNECTOR_SIGNS.EQUALS;
                        returnStr = "'" + sqlValue + "'";
                    }
                }
            }
            else
            {
                object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { dto.Control });
                string controlText = PropertyUtility.GetTextOrValue(control[0]);
                if (control != null && !string.IsNullOrEmpty(controlText))
                {
                    string sqlControl = SqlCreateUtility.CounterplanEscapeSequence(controlText);
                    // 複数同じ名前のコントロールは存在しないはず
                    returnStr = "'" + sqlControl + "'";
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

        #endregion

    }
}
