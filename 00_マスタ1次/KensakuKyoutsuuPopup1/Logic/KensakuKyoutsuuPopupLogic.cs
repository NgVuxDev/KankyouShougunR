// $Id: KensakuKyoutsuuPopupLogic.cs 43381 2015-03-02 00:24:25Z nagata $
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using KensakuKyoutsuuPopup1.APP;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Setting;
using r_framework.Utility;

namespace KensakuKyoutsuuPopup1.Logic
{
    /// <summary>
    /// 検索共通ポップアップロジック
    /// </summary>
    public class KensakuKyoutsuuPopupLogic
    {
        #region フィールド

        /// <summary>
        /// バインドするカラム名一覧
        /// </summary>
        internal string[] bindColumnNames = new string[] { "" };

        internal string[] displayColumnNames = new string[] { };

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "KensakuKyoutsuuPopup1.Setting.ButtonSetting.xml";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private KensakuKyoutsuuPopupForm form;

        /// <summary>
        /// 共通一覧画面にて利用されるDao
        /// </summary>
        private IS2Dao dao;

        /// <summary>
        /// Daoに引き渡すSQLファイルのパス
        /// </summary>
        private string executeSqlFilePath = string.Empty;

        private string whereStr = string.Empty;

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
        /// M_TORIHIKISAKIの表示項目
        /// </summary>
        private enum TORIHIKISAKI_COLUMNS
        {
            TORIHIKISAKI_CD,
            TORIHIKISAKI_NAME_RYAKU,
            TORIHIKISAKI_FURIGANA,
            TODOUFUKEN_NAME_RYAKU,
            TORIHIKISAKI_ADDRESS1,
            TORIHIKISAKI_TEL
        }

        /// <summary>
        /// M_GYOUSHAの表示項目
        /// </summary>
        private enum GYOUSHA_COLUMNS
        {
            TORIHIKISAKI_CD,
            TORIHIKISAKI_NAME_RYAKU,
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU,
            GYOUSHA_FURIGANA,
            TODOUFUKEN_NAME_RYAKU,
            GYOUSHA_ADDRESS1,
            GYOUSHA_TEL
        }

        /// <summary>
        /// DENSHI_JIGYOUSHAの表示項目
        /// </summary>
        private enum DENSHI_JIGYOUSHA_COLUMNS
        {
            EDI_MEMBER_ID,
            JIGYOUSHA_NAME,
            JIGYOUSHA_POST,
            TODOFUKEN_NAME,
            JIGYOUSHA_ADDRESS,
            JIGYOUSHA_TEL,
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU
        }

        /// <summary>
        /// M_NYUUKINSAKIの表示項目
        /// </summary>
        private enum NYUUKINSAKI_COLUMNS
        {
            NYUUKINSAKI_CD,
            NYUUKINSAKI_NAME_RYAKU,
            NYUUKINSAKI_FURIGANA,
            TODOUFUKEN_NAME_RYAKU,
            NYUUKINSAKI_ADDRESS1,
            NYUUKINSAKI_TEL
        }

        /// <summary>
        /// M_SYUKKINSAKIの表示項目
        /// </summary>
        private enum SYUKKINSAKI_COLUMNS
        {
            SYUKKINSAKI_CD,
            SYUKKINSAKI_NAME_RYAKU,
            SYUKKINSAKI_FURIGANA,
            TODOUFUKEN_NAME_RYAKU,
            SYUKKINSAKI_ADDRESS1,
            SYUKKINSAKI_TEL
        }

        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal KensakuKyoutsuuPopupLogic(KensakuKyoutsuuPopupForm targetForm)
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


            //初期値 個別対応する場合は以下のDisplyInit()で上書きすること
            this.form.CONDITION_ITEM.Text = "3";
            this.form.CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.form.FILTER_BOIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.FILTER_SHIIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.customDataGridView1.AllowUserToAddRows = false;

            // 画面タイトルやDaoを初期化
            this.DisplyInit();

            // カラムの幅を調整する
            for (int i = 0; this.form.customDataGridView1.Columns.Count > i; i++)
            {
                this.form.customDataGridView1.Columns[i].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            }

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

            switch (this.form.WindowId)
            {
                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.M_TORIHIKISAKI:
                    // TODO:SQLファイルのパス設定を忘れずに
                    this.dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                    this.form.lb_title.Text = "取引先検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup1.Sql.GetTorihikisakiDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(TORIHIKISAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "フリガナ", "都道府県", "住所", "電話番号" };
                    break;

                case WINDOW_ID.M_GYOUSHA:
                    this.dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                    this.form.lb_title.Text = "業者検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup1.Sql.GetGyoushaDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(GYOUSHA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "業者CD", "業者名", "フリガナ", "都道府県", "住所", "電話番号" };
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
                    this.form.lb_title.Text = "電子事業者検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup1.Sql.GetDenshiGyoushaDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(DENSHI_JIGYOUSHA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "加入者番号", "電子事業者名", "郵便番号", "都道府県", "住所", "電話番号", "業者CD", "業者名" };

                    //フリガナがないため利用不可に
                    this.form.panel2.Enabled = false;
                    this.form.panel3.Enabled = false;

                    this.form.CONDITION2.Text = "事業者名";
                    this.form.CONDITION2.Tag = "事業者名が対象の場合チェックを付けて下さい";
                    this.form.CONDITION3.Enabled = false;
                    this.form.CONDITION_ITEM.Tag = "【１～２、４～7】のいずれかで入力して下さい";
                    this.form.CONDITION_ITEM.CharacterLimitList = new char[] { '1', '2', '4', '5', '6', '7' };

                    // "2.略称"を"2.事業者名"にするため２文字増えた分位置調整
                    this.form.CONDITION3.Location = new System.Drawing.Point(189, 5);
                    this.form.CONDITION4.Location = new System.Drawing.Point(261, 5);
                    this.form.CONDITION5.Location = new System.Drawing.Point(348, 5);
                    this.form.CONDITION6.Location = new System.Drawing.Point(413, 5);
                    this.form.CONDITION7.Location = new System.Drawing.Point(478, 5);
                    this.form.CONDITION_VALUE.Location = new System.Drawing.Point(539, 5);



                    this.form.CONDITION_ITEM.Text = "2"; //ふりがながないので事業者名を初期にする
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;

                    break;

                case WINDOW_ID.M_NYUUKINSAKI:
                    this.dao = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
                    this.form.lb_title.Text = "入金先検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup1.Sql.GetNyuukinsakiDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(NYUUKINSAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "入金先CD", "入金先名", "フリガナ", "都道府県", "住所", "電話番号" };
                    break;

                case WINDOW_ID.M_SYUKKINSAKI:
                    this.dao = DaoInitUtility.GetComponent<IM_SYUKKINSAKIDao>();
                    this.form.lb_title.Text = "出金先検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup1.Sql.GetSyukkinsakiDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(SYUKKINSAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "出金先CD", "出金先名", "フリガナ", "都道府県", "住所", "電話番号" };
                    break;

                default:
                    break;
            }

			// Formタイトルの初期化
			this.form.Text = this.form.lb_title.Text;

            // カラム設定(画面ごとに表示カラムは変わらないはず)
            for (int i = 0; i < bindColumnNames.Length; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = bindColumnNames[i];
                column.Name = bindColumnNames[i];
                column.HeaderText = displayColumnNames[i];

                this.form.customDataGridView1.Columns.Add(column);
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
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
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
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
                    dt = this.dao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + this.whereStr);
                    sqlStr.Close();
                }
            }

            // DataTable table = GetStringDataTable(dt);
            this.SearchResult = dt;

            // 頭文字絞込み
            this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();

            this.form.customDataGridView1.DataSource = this.SearchResult;
            this.form.customDataGridView1.ReadOnly = true;
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

            this.form.ReturnParams = setParamList;
            this.form.Close();
        }

        /// <summary>
        /// 検索条件入力欄のIME制御
        /// </summary>
        internal void ImeControlCondition()
        {
            switch (this.form.CONDITION_ITEM.Text)
            {
                case "1":
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                    break;

                case "2":
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "3":
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Katakana;
                    break;

                case "4":
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "5":
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "6":
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                    break;

                case "7":
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
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
            this.form.CONDITION_ITEM.Text = "3";
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.FILTER_BOIN_VALUE.Text = string.Empty;
            this.form.FILTER_SHIIN_VALUE.Text = string.Empty;

            DataTable dt = (DataTable)this.form.customDataGridView1.DataSource;

            if (dt != null)
            {
                dt.Rows.Clear();
                //this.form.customDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }

            this.form.CONDITION_ITEM.Focus();
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
            switch (this.form.WindowId)
            {
                // TODO:.Nemeでちゃんとクラス名取れているか確認
                case WINDOW_ID.M_TORIHIKISAKI:
                    this.SearchInfo = new M_TORIHIKISAKI();
                    tableName = typeof(M_TORIHIKISAKI).Name;
                    break;

                case WINDOW_ID.M_GYOUSHA:
                    this.SearchInfo = new M_GYOUSHA();
                    tableName = typeof(M_GYOUSHA).Name;
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    this.SearchInfo = new M_DENSHI_JIGYOUSHA();
                    tableName = typeof(M_DENSHI_JIGYOUSHA).Name;
                    break;

                case WINDOW_ID.M_NYUUKINSAKI:
                    this.SearchInfo = new M_NYUUKINSAKI();
                    tableName = typeof(M_NYUUKINSAKI).Name;
                    break;

                case WINDOW_ID.M_SYUKKINSAKI:
                    this.SearchInfo = new M_SYUKKINSAKI();
                    tableName = typeof(M_SYUKKINSAKI).Name;
                    break;

                default:
                    break;
            }

            // カラム名を動的に指定するために必要
            var ColumnHeaderName = tableName.Substring(2, tableName.Length - 2);

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                // シングルクォートは2つ重ねる
                var condition = SqlCreateUtility.CounterplanEscapeSequence(this.form.CONDITION_VALUE.Text);
                switch (this.form.CONDITION_ITEM.Text)
                {
                    case "1":
                        // ｺｰﾄﾞ
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND " + tableName + ".EDI_MEMBER_ID LIKE '%" + condition + "%' ";
                                break;
                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%' ";
                                break;
                        }
                        break;

                    case "2":
                        // 略称名
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND " + tableName + ".JIGYOUSHA_NAME LIKE '%" + condition + "%' "; //略名なし
                                break;
                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%' ";
                                break;
                        }
                        break;

                    case "3":
                        // ﾌﾘｶﾞﾅ
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND 1=1 "; //フリガナ無
                                break;
                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "4":
                        // 都道府県
                        // もし数値変換できない場合は設定しない
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND " + tableName + ".JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%' "; //結合無
                                break;
                            default:
                                this.whereStr = " AND M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "5":
                        // 住所
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND (" + tableName + ".JIGYOUSHA_ADDRESS2 LIKE '%" + condition + "%' OR " + tableName + ".JIGYOUSHA_ADDRESS3 LIKE '%" + condition + "%' ) "; //1と2がある
                                break;
                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "6":
                        // 電話
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND " + tableName + ".JIGYOUSHA_TEL LIKE '%" + condition + "%' ";
                                break;
                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "7":
                        // ﾌﾘｰ
                        // ﾌﾘｰでは1～6のすべてに対して検索をかける
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND (" + tableName + ".EDI_MEMBER_ID LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".JIGYOUSHA_NAME LIKE '%" + condition + "%' ";
                                //フリガナ無
                                this.whereStr = this.whereStr + " OR " + tableName + ".JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR (" + tableName + ".JIGYOUSHA_ADDRESS2 LIKE '%" + condition + "%' OR " + tableName + ".JIGYOUSHA_ADDRESS3 LIKE '%" + condition + "%' ) ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".JIGYOUSHA_TEL LIKE '%" + condition + "%')";
                                break;
                            default:
                                this.whereStr = " AND (" + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%')";
                                break;
                        }
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
                case WINDOW_ID.M_TORIHIKISAKI:
                    colName = TORIHIKISAKI_COLUMNS.TORIHIKISAKI_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_GYOUSHA:
                    colName = GYOUSHA_COLUMNS.GYOUSHA_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    colName = DENSHI_JIGYOUSHA_COLUMNS.JIGYOUSHA_NAME.ToString();
                    break;

                case WINDOW_ID.M_NYUUKINSAKI:
                    colName = NYUUKINSAKI_COLUMNS.NYUUKINSAKI_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_SYUKKINSAKI:
                    colName = SYUKKINSAKI_COLUMNS.SYUKKINSAKI_FURIGANA.ToString();
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

        #endregion
    }
}
