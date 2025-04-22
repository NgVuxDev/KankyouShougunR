// $Id: HikiaiKizonKensakuPopupLogic.cs 3657 2013-10-15 07:03:01Z ishibashi $
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using HikiaiKizonKensakuPopup.APP;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.OriginalException;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace HikiaiKizonKensakuPopup.Logic
{
    /// <summary>
    /// 検索共通ポップアップロジック
    /// </summary>
    public class HikiaiKizonKensakuPopupLogic
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
        private static readonly string ButtonInfoXmlPath = "HikiaiKizonKensakuPopup.Setting.ButtonSetting.xml";

        private static readonly string tekiyou1 = " AND (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND {0}.DELETE_FLG = 0  ";

        private static readonly string tekiyou2 = " AND (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND {0}.DELETE_FLG = 0  ";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private HikiaiKizonKensakuPopupForm form;

        /// <summary>
        /// 共通一覧画面にて利用されるDao
        /// </summary>
        private IS2Dao dao;

        /// <summary>
        /// Daoに引き渡すSQLファイルのパス
        /// </summary>
        private string executeSqlFilePath = string.Empty;

        /// <summary>
        /// join句
        /// </summary>
        private string joinStr = string.Empty;

        /// <summary>
        /// where句
        /// </summary>
        private string whereStr = string.Empty;

        /// <summary>
        /// PopupSearchSendParamDtoの最大深度を保持する
        /// </summary>
        private int depthCnt = 0;

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
        //NvNhat #158998 #158999
        public bool _flagChangedJouken = false;

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
                LogUtility.DebugMethodStart();

                switch (e)
                {
                    case CNNECTOR_SIGNS.EQUALS:
                        return "=";

                    case CNNECTOR_SIGNS.IN:
                        return "IN";
                }

                LogUtility.DebugMethodEnd();
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
            TORIHIKISAKI_POST,
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
            GYOUSHA_POST,
            TODOUFUKEN_NAME_RYAKU,
            GYOUSHA_ADDRESS1,
            GYOUSHA_TEL
        }

        /// <summary>
        /// M_GENBAの表示項目
        /// </summary>
        private enum GENBA_COLUMNS
        {
            GENBA_CD,
            GENBA_NAME_RYAKU,
            GENBA_FURIGANA,
            GENBA_POST,
            TODOUFUKEN_NAME_RYAKU,
            GENBA_ADDRESS1,
            GENBA_TEL,
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU
        }

        #endregion

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal HikiaiKizonKensakuPopupLogic(HikiaiKizonKensakuPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            //クリアボタン(F5)イベント生成
            this.form.bt_func5.Click += new EventHandler(this.form.Clear);

            //検索ボタン(F8)イベント生成
            this.form.bt_func8.Click += new EventHandler(this.form.Search);

            //確定ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.Selected);

            //取消ボタン(F10)イベント生成
            this.form.bt_func10.Click += new EventHandler(this.form.SortSetting);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.Close);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //ボタンの初期化
                this.ButtonInit();
                // 画面タイトルやDaoを初期化
                this.DisplyInit();

                this.form.CONDITION_ITEM.Text = "3";
                this.form.CONDITION_ITEM.ImeMode = ImeMode.Alpha;
                this.form.CONDITION_VALUE.ImeMode = ImeMode.Katakana;
                this.form.FILTER_BOIN_VALUE.ImeMode = ImeMode.Alpha;
                this.form.FILTER_SHIIN_VALUE.ImeMode = ImeMode.Alpha;
                this.form.customDataGridView1.AllowUserToAddRows = false;

                // イベント初期化
                EventInit();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 画面のタイトルなどを初期化を行う
        /// </summary>
        private void DisplyInit()
        {
            LogUtility.DebugMethodStart();

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
                    this.dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                    this.form.lb_title.Text = "取引先検索";
                    //this.executeSqlFilePath = "HikiaiKizonKensakuPopup.Sql.GetTorihikisakiDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(TORIHIKISAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "フリガナ", "郵便番号", "都道府県", "住所", "電話番号" };
                    break;

                case WINDOW_ID.M_GYOUSHA:
                    this.dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                    this.form.lb_title.Text = "業者検索";
                    //this.executeSqlFilePath = "HikiaiKizonKensakuPopup.Sql.GetGyoushaDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(GYOUSHA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "業者CD", "業者名", "フリガナ", "郵便番号", "都道府県", "住所", "電話番号" };
                    break;

                case WINDOW_ID.M_GENBA:
                    //this.dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                    //this.form.lb_title.Text = "現場検索(引合)";
                    //this.executeSqlFilePath = "HikiaiKizonKensakuPopup.Sql.GetGenbaDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    //this.bindColumnNames = Enum.GetNames(typeof(GENBA_COLUMNS));
                    //this.returnParamNames = popupGetMasterField;
                    //this.displayColumnNames = new string[] { "現場CD", "現場名", "フリガナ", "郵便番号", "都道府県", "住所", "電話番号", "業者CD", "業者名" };
                    break;

                default:
                    break;
            }
            this.SettingDisplayKensakuJouken();//NvNhat #158998 #158999
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

            LogUtility.DebugMethodEnd();
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

            LogUtility.DebugMethodEnd();
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
            try
            {
                LogUtility.DebugMethodStart();

                // 引合が選択されたかどうか
                Boolean isHikiai = this.form.rdlHikiai.Checked;
                String tableName = String.Empty;
                String columnHeaderName = String.Empty;

                // テーブル名、sqlパス設定
                switch (this.form.WindowId)
                {
                    // 画面IDごとに生成を行うDaoを変更する
                    case WINDOW_ID.M_TORIHIKISAKI:
                        if (isHikiai)
                        {
                            tableName = typeof(M_HIKIAI_TORIHIKISAKI).Name;
                            this.executeSqlFilePath = "HikiaiKizonKensakuPopup.Sql.GetTorihikisakiHikiaiDataSql.sql";
                        }
                        else
                        {
                            tableName = typeof(M_TORIHIKISAKI).Name;
                            this.executeSqlFilePath = "HikiaiKizonKensakuPopup.Sql.GetTorihikisakiDataSql.sql";
                        }
                        columnHeaderName = "TORIHIKISAKI";
                        break;

                    case WINDOW_ID.M_GYOUSHA:
                        if (isHikiai)
                        {
                            tableName = typeof(M_HIKIAI_GYOUSHA).Name;
                            this.executeSqlFilePath = "HikiaiKizonKensakuPopup.Sql.GetGyoushaHikiaiDataSql.sql";
                        }
                        else
                        {
                            tableName = typeof(M_GYOUSHA).Name;
                            this.executeSqlFilePath = "HikiaiKizonKensakuPopup.Sql.GetGyoushaDataSql.sql";
                        }
                        columnHeaderName = "GYOUSHA";
                        break;

                    case WINDOW_ID.M_GENBA:
                        if (isHikiai)
                        {
                            tableName = typeof(M_HIKIAI_GENBA).Name;
                            this.executeSqlFilePath = "HikiaiKizonKensakuPopup.Sql.GetGenbaHikiaiDataSql.sql";
                        }
                        else
                        {
                            tableName = typeof(M_GENBA).Name;
                            this.executeSqlFilePath = "HikiaiKizonKensakuPopup.Sql.GetGenbaDataSql.sql";
                        }
                        columnHeaderName = "GENBA";
                        break;

                    default:
                        break;
                }

                // 検索条件生成
                this.SetSearchString(tableName, columnHeaderName);

                DataTable dt = new DataTable();
                // 基本的なスクリプトを取得
                var thisAssembly = Assembly.GetExecutingAssembly();
                using (var resourceStream = thisAssembly.GetManifestResourceStream(this.executeSqlFilePath))
                {
                    using (var sqlStr = new StreamReader(resourceStream))
                    {
                        String s1 = sqlStr.ReadToEnd().Replace(Environment.NewLine, "");
                        string sql = s1 + this.joinStr + this.whereStr;

                        dt = this.dao.GetDateForStringSql(sql);
                        sqlStr.Close();
                    }
                }

                this.SearchResult = dt;

                // 頭文字絞込み
                this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();
                this.form.customSortHeader1.SortDataTable(this.SearchResult);
                this.form.customDataGridView1.DataSource = this.SearchResult;
                this.form.customDataGridView1.ReadOnly = true;
                this.form.customDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // 一部結果を非表示設定
                InVisibleSeting();

                int returnCount = this.SearchResult.Rows == null ? 0 : this.SearchResult.Rows.Count;

                LogUtility.DebugMethodEnd(returnCount);
                return returnCount;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// 検索結果を非表示設定
        /// </summary>
        public void InVisibleSeting()
        {
            LogUtility.DebugMethodStart();

            // 引合が選択されたかどうか
            Boolean isHikiai = this.form.rdlHikiai.Checked;
            switch (this.form.WindowId)
            {
                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.M_TORIHIKISAKI:
                    this.form.customDataGridView1.Columns["TORIHIKISAKI_HIKIAI_FLG"].Visible = false;
                    this.form.customDataGridView1.Columns["TORIHIKISAKI_SHOKUCHI_KBN"].Visible = false;
                    break;

                case WINDOW_ID.M_GYOUSHA:
                    this.form.customDataGridView1.Columns["GYOUSHA_HIKIAI_FLG"].Visible = false;
                    this.form.customDataGridView1.Columns["GYOUSHA_SHOKUCHI_KBN"].Visible = false;
                    if (isHikiai)
                    {
                        this.form.customDataGridView1.Columns["TORIHIKISAKI_HIKIAI_FLG"].Visible = true;
                        this.form.customDataGridView1.Columns["TORIHIKISAKI_HIKIAI_FLG"].HeaderText = "引合取引先";
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns["TORIHIKISAKI_HIKIAI_FLG"].Visible = false;
                    }
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool InvokeInitialSort()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.SearchResult != null)
                {
                    // 頭文字絞込み
                    this.SearchResult.DefaultView.RowFilter = string.Empty;
                    this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();
                    this.form.customDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InvokeInitialSort", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 選択データ決定処理
        /// </summary>
        internal bool ElementDecision()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // レコードはない場合、リターンする
                if (this.form.customDataGridView1.CurrentRow == null) return false;

                Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
                List<PopupReturnParam> setParam = new List<PopupReturnParam>();
                for (int i = 0; i < this.returnParamNames.Length; i++)
                {
                    PopupReturnParam popupParam = new PopupReturnParam();
                    var setDate = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[this.returnParamNames[i]];

                    var control = setDate as ICustomControl;

                    popupParam.Key = "Value";

                    if (setDate.Value.GetType() == typeof(Boolean))
                    {
                        // データタイプがBooleanの場合は０or１で値を返す
                        popupParam.Value = (Boolean)setDate.Value ? "1" : "0";
                    }
                    else
                    {
                        popupParam.Value = setDate.Value.ToString();
                    }

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

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElementDecision", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 検索条件入力欄のIME制御
        /// </summary>
        internal bool ImeControlCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

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

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ImeControlCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #endregion

        #region Utility

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString(String inTableName, String inColumnHeaderName)
        {
            LogUtility.DebugMethodStart(inTableName, inColumnHeaderName);

            // 初期化
            this.joinStr = string.Empty;
            this.whereStr = string.Empty;

            // 検索画面が増えたら以下に設定を追加していく
            string tableName = inTableName;
            switch (this.form.WindowId)
            {
                // TODO:.Nemeでちゃんとクラス名取れているか確認
                case WINDOW_ID.M_TORIHIKISAKI:
                    this.SearchInfo = new M_TORIHIKISAKI();
                    break;

                case WINDOW_ID.M_GYOUSHA:
                    this.SearchInfo = new M_GYOUSHA();
                    break;

                case WINDOW_ID.M_GENBA:
                    this.SearchInfo = new M_GENBA();
                    break;

                default:
                    break;
            }

            // カラム名を動的に指定するために必要
            //var ColumnHeaderName = tableName.Substring(2, tableName.Length - 2);
            var ColumnHeaderName = inColumnHeaderName;

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                // シングルクォートは2つ重ねる
                var condition = SqlCreateUtility.CounterplanEscapeSequence(this.form.CONDITION_VALUE.Text);
                switch (this.form.CONDITION_ITEM.Text)
                {
                    case "1":
                        // ｺｰﾄﾞ
                        this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%' ";
                        break;

                    case "2":
                        // 略称名
                        this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%' ";
                        break;

                    case "3":
                        // ﾌﾘｶﾞﾅ
                        this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                        break;

                    case "4":
                        // 都道府県
                        // もし数値変換できない場合は設定しない
                        this.whereStr = " AND M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                        break;

                    case "5":
                        // 住所
                        this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                        break;

                    case "6":
                        // 電話
                        this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                        break;

                    case "7":
                        // ﾌﾘｰ
                        // ﾌﾘｰでは1～6のすべてに対して検索をかける
                        this.whereStr = " AND (" + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%' ";
                        this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%' ";
                        this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                        this.whereStr = this.whereStr + " OR M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                        this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                        this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%')";
                        break;

                    default:
                        break;
                }
            }

            //this.whereStr = " WHERE " + CreateWhereStr(tableName) + this.whereStr;
            //this.whereStr = " WHERE " + tableName + ".DELETE_FLG = 0 " + this.whereStr;
            //Add Query //NvNhat #158998 #158999
            if (this._flagChangedJouken)
            {
                // チェックボックスからくる条件句
                if (this.form.HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.HYOUJI_JOUKEN_DELETED.Checked || this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    string queryJouken = "";
                    if (!string.IsNullOrEmpty(tableName))
                    {
                        queryJouken += "  AND  (1 = 0";

                        // 適用
                        if (this.form.HYOUJI_JOUKEN_TEKIYOU.Checked)
                        {
                            queryJouken += " OR ((({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) or ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and {0}.TEKIYOU_END IS NULL) or ({0}.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) or ({0}.TEKIYOU_BEGIN IS NULL and {0}.TEKIYOU_END IS NULL)) and {0}.DELETE_FLG = 0)";
                        }

                        // 削除
                        if (this.form.HYOUJI_JOUKEN_DELETED.Checked)
                        {
                            queryJouken += " OR {0}.DELETE_FLG = 1";
                        }

                        // 適用外
                        if (this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                        {
                            queryJouken += " OR (({0}.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > {0}.TEKIYOU_END) and {0}.DELETE_FLG = 0)";
                        }
                        queryJouken += ")";
                    }
                    this.whereStr += string.Format(queryJouken.ToString(), tableName);
                }
                //Save Properties
                this.SettingProperties(false);
            }

            this.whereStr = " WHERE 1 = 1 " + this.whereStr;

            // 画面から来た絞込み情報で条件句を作成
            bool existSearchParam = false;  // popupSearchSendParamからWHEREが生成されたかどうかのフラグ
            StringBuilder sb = new StringBuilder(" ");
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
                //Check if Delete, Tekiyou
                if (this.CheckParamsIsValid(popupSearchSendParam.KeyName))
                {
                    continue;
                }

                if (popupSearchSendParam.KeyName != null && popupSearchSendParam.KeyName.Equals("TEKIYOU_BEGIN"))
                {
                    object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { popupSearchSendParam.Control });
                    string controlText = PropertyUtility.GetTextOrValue(control[0]);
                    var ctr = control[0] as CustomDateTimePicker;
                    var ctr2 = control[0] as DataGridViewTextBoxCell;
                    string tekiyouSql = string.Empty;
                    if (ctr != null && ctr.Value != null)
                    {
                        tekiyouSql = string.Format(tekiyou1, tableName, ctr.Value);
                        sb.Append(tekiyouSql);
                    }
                    else if (ctr2 != null && ctr2.Value != null)
                    {
                        tekiyouSql = string.Format(tekiyou1, tableName, ctr2.Value);
                        sb.Append(tekiyouSql);
                    }
                    else
                    {
                        tekiyouSql = string.Format(tekiyou2, tableName);
                        sb.Append(tekiyouSql);
                    }
                    continue;
                }

                if (popupSearchSendParam.KeyName != null && popupSearchSendParam.KeyName.Equals("TEKIYOU_FLG")
                    && !string.IsNullOrEmpty(popupSearchSendParam.Value))
                {
                    if ("TRUE".Equals(popupSearchSendParam.Value.ToUpper()))
                    {
                        string tekiyouSql = string.Format(tekiyou2, tableName);
                        sb.Append(tekiyouSql);
                    }
                    else if ("FALSE".Equals(popupSearchSendParam.Value.ToUpper()))
                    {
                        string tekiyouSql = string.Format(" AND {0}.DELETE_FLG = 0 ", tableName);
                        sb.Append(tekiyouSql);
                    }
                    continue;
                }

                this.depthCnt = 1;
                existSearchParam = false;
                string where = this.CreateWhereStrFromScreen(popupSearchSendParam, tableName, ref existSearchParam);
                sb.Append(where);
                if (sb.Length > 0)
                {
                    if (!existSearchParam)
                    {
                        this.depthCnt--;
                    }
                    for (int i = 0; i < this.depthCnt; i++)
                    {
                        sb.Append(") ");
                    }
                }
            }

            this.whereStr += sb.ToString();
            this.CreateJoinStr();

            LogUtility.DebugMethodEnd();
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

            bool subExistSearchParam = false;
            int thisDepth = this.depthCnt;

            // 括弧付きの条件対応
            if (dto.SubCondition != null && 0 < dto.SubCondition.Count)
            {
                this.depthCnt++;
                foreach (PopupSearchSendParamDto popupSearchSendParam in dto.SubCondition)
                {
                    //Check if Delete, Tekiyou
                    if (this.CheckParamsIsValid(popupSearchSendParam.KeyName)) //NvNhat #158998 #158999
                    {
                        continue;
                    }
                    string where = this.CreateWhereStrFromScreen(popupSearchSendParam, tableName, ref subExistSearchParam);
                    sb.Append(where);
                }

                // 条件をまとめるため
                if (subExistSearchParam)
                {
                    for (int i = 0; i < thisDepth; i++)
                    {
                        sb.Append(") ");
                    }
                }
                else
                {
                    this.depthCnt--;
                }
            }

            // 通常のWHERE句を生成
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
                for (int i = 0; i < thisDepth; i++)
                {
                    sb.Append(" (");
                }
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

        /// <summary>
        /// 頭文字条件の生成
        /// DataGridViewのフィルタ条件に使用すること
        /// </summary>
        /// <returns>フィルタ条件</returns>
        private String SetInitialSearchString()
        {
            LogUtility.DebugMethodStart();

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

            LogUtility.DebugMethodEnd();
            return filterStr;
        }

        /// <summary>
        /// 該当テーブルからフリガナのカラム名を取得
        /// </summary>
        /// <returns></returns>
        private string GetFuriganaColName()
        {
            LogUtility.DebugMethodStart();

            string colName = string.Empty;

            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_TORIHIKISAKI:
                    colName = TORIHIKISAKI_COLUMNS.TORIHIKISAKI_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_GYOUSHA:
                    colName = GYOUSHA_COLUMNS.GYOUSHA_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_GENBA:
                    colName = GENBA_COLUMNS.GENBA_FURIGANA.ToString();
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
            return colName;
        }

        /// <summary>
        /// 先頭カラムが文字列のDataTableの取得
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetStringDataTable(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

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

            LogUtility.DebugMethodEnd();
            return table;
        }

        /// <summary>
        /// PopupSearchSendParamDtoからWHERE句を作成します。
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="whereValue"></param>
        private string CreateWhere(PopupSearchSendParamDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            CNNECTOR_SIGNS sqlConnectorSign = CNNECTOR_SIGNS.EQUALS;  // KeyとValueをつなぐ符号
            string returnStr = string.Empty;

            if (dto == null)
            {
                return returnStr;
            }
            string sqlValue = string.Empty;
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
                            // Where句の文字列に対してエスケープシーケンス対策を行う
                            sqlValue = SqlCreateUtility.CounterplanEscapeSequence(tempValue);
                            if (string.IsNullOrEmpty(returnStr))
                            {
                                returnStr = "'" + sqlValue + "'";
                            }
                            else
                            {
                                returnStr = returnStr + ", '" + sqlValue + "'";
                            }
                        }
                        returnStr = "(" + returnStr + ")";
                    }
                    else
                    {
                        sqlValue = SqlCreateUtility.CounterplanEscapeSequence(dto.Value);
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
                    sqlValue = SqlCreateUtility.CounterplanEscapeSequence(controlText);
                    // 複数同じ名前のコントロールは存在しないはず
                    returnStr = "'" + sqlValue + "'";
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
        /// 有効レコードをチェックするSQLを作成します。
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <returns></returns>
        private static string CreateWhereStr(string tableName)
        {
            LogUtility.DebugMethodStart(tableName);

            tableName += ".";
            string result = string.Empty;
            result += tableName + "DELETE_FLG = 0  ";

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// popupWindowSettingの内容からJOIN句を作成します。
        /// </summary>
        private void CreateJoinStr()
        {
            LogUtility.DebugMethodStart();

            var join = new StringBuilder();
            var where = new StringBuilder();
            var isChecked = new List<string>();
            foreach (JoinMethodDto joinData in this.form.popupWindowSetting)
            {
                if (joinData.Join != JOIN_METHOD.WHERE)
                {
                    if (!string.IsNullOrEmpty(joinData.LeftTable) && !string.IsNullOrEmpty(joinData.LeftKeyColumn) &&
                        !string.IsNullOrEmpty(joinData.RightTable) && !string.IsNullOrEmpty(joinData.RightKeyColumn))
                    {
                        join.Append(" " + JOIN_METHODExt.ToString(joinData.Join) + " ");
                        join.Append(joinData.LeftTable + " ON ");
                        join.Append(joinData.LeftTable + "." + joinData.LeftKeyColumn + " = ");
                        join.Append(joinData.RightTable + "." + joinData.RightKeyColumn + " ");
                    }
                }
                else if (joinData.Join == JOIN_METHOD.WHERE)
                {
                    var searchStr = new StringBuilder();
                    foreach (var searchData in joinData.SearchCondition)
                    {
                        //Check if Delete, Tekiyou
                        if (this.CheckParamsIsValid(searchData.LeftColumn)) //NvNhat #158998 #158999
                        {
                            continue;
                        }    
                        //検索条件設定
                        if (string.IsNullOrEmpty(searchData.Value))
                        {
                            //value値がnullのため、テーブル同士のカラム結合を行う
                            if (searchStr.Length == 0)
                            {
                                searchStr.Append(" AND (");
                            }
                            else
                            {
                                searchStr.Append(" ");
                                searchStr.Append(searchData.And_Or.ToString());
                                searchStr.Append(" ");
                            }
                            searchStr.Append(joinData.LeftTable);
                            searchStr.Append(".");
                            searchStr.Append(searchData.LeftColumn);
                            var data = joinData.RightTable + "." + searchData.RightColumn;
                            searchStr.Append(searchData.Condition.ToConditionString(data));
                        }
                        else
                        {
                            // コントロールの値が有効な場合WHERE句を作成する
                            var data = createValues(this.form.ParentControls, searchData);

                            if (!string.IsNullOrEmpty(data))
                            {
                                if (searchStr.Length == 0)
                                {
                                    searchStr.Append(" AND (");
                                }
                                else
                                {
                                    searchStr.Append(" ");
                                    searchStr.Append(searchData.And_Or.ToString());
                                    searchStr.Append(" ");
                                }
                                searchStr.Append(joinData.LeftTable);
                                searchStr.Append(".");
                                searchStr.Append(searchData.LeftColumn);
                                searchStr.Append(searchData.Condition.ToConditionString(data));
                                searchStr.Append(" ");
                            }
                        }
                    }
                    if (searchStr.Length > 0)
                    {
                        // 閉じる
                        searchStr.Append(") ");
                    }
                    where.Append(searchStr);
                }
                // 有効レコードをチェックする
                if (joinData.IsCheckLeftTable == true && !isChecked.Contains(joinData.LeftTable))
                {
                    var type = Type.GetType("r_framework.Entity." + joinData.LeftTable);
                    if (type != null)
                    {
                        var pNames = type.GetProperties().Select(p => p.Name);
                        if (pNames.Contains("TEKIYOU_BEGIN") && pNames.Contains("TEKIYOU_END") && pNames.Contains("DELETE_FLG"))
                        {
                            where.Append(" AND ");
                            where.Append(CreateWhereStr(joinData.LeftTable));
                            where.Append(" ");
                        }
                    }
                    isChecked.Add(joinData.LeftTable);
                }
            }
            this.joinStr += join.ToString();
            this.whereStr += where.ToString();

            LogUtility.DebugMethodEnd(joinStr, whereStr);
        }

        /// <summary>
        /// 検索条件を作成する
        /// 対象のコントロールが見つけれた場合については、コントロールの値とする
        /// コントロールが見つからない場合は、Valuesの値を直接設定する
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static string createValues(object[] controls, SearchConditionsDto dto)
        {
            LogUtility.DebugMethodStart(controls, dto);

            var field = ControlUtility.CreateFields(controls, dto.Value);

            if (field[0] != null)
            {
                var control = field[0] as ICustomControl;

                if (control != null)
                {
                    return dto.ValueColumnType.ToConvertString(control.GetResultText());
                }
                throw new Exception();
            }

            LogUtility.DebugMethodEnd();
            return dto.ValueColumnType.ToConvertString(dto.Value.ToString());
        }

        /// <summary>
        /// SetDisplay will use Checkbox
        /// NvNhat #158998 #158999 BEGIN
        /// </summary>
        private void SettingDisplayKensakuJouken()
        {
            #region Setting Layout
            Control itemChild = this.form.ParentControls[0] as Control;
            if (itemChild == null)
            {
                _flagChangedJouken = false;
                return;
            }
            BaseBaseForm parentForm = itemChild.Parent as BaseBaseForm;
            if (parentForm == null)
            {
                _flagChangedJouken = false;
                return;
            }

            var ichiranForm = parentForm.inForm as Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm;
            if (!_flagChangedJouken && ichiranForm == null)
            {
                _flagChangedJouken = false;
                return;
            }

            if (!_flagChangedJouken && ichiranForm.DenshuKbn.Equals(DENSHU_KBN.NONE))
            {
                _flagChangedJouken = false;
                return;
            }
            _flagChangedJouken = true;

            this.form.HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.form.HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.form.label4 = new System.Windows.Forms.Label();
            this.form.HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();

            // 
            // HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(287, 156);
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Name = "HYOUJI_JOUKEN_TEKIYOUGAI";
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(84, 16);
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 518;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.TabStop = false;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Visible = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);
            // 
            // HYOUJI_JOUKEN_DELETED
            // 
            this.form.HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.form.HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(233, 156);
            this.form.HYOUJI_JOUKEN_DELETED.Name = "HYOUJI_JOUKEN_DELETED";
            this.form.HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(48, 16);
            this.form.HYOUJI_JOUKEN_DELETED.TabIndex = 517;
            this.form.HYOUJI_JOUKEN_DELETED.TabStop = false;
            this.form.HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.form.HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_DELETED.Visible = true;
            this.form.HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);
            // 
            // label4
            // 
            this.form.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.form.label4.ForeColor = System.Drawing.Color.White;
            this.form.label4.Location = new System.Drawing.Point(12, 154);
            this.form.label4.Name = "label4";
            this.form.label4.Size = new System.Drawing.Size(148, 20);
            this.form.label4.TabIndex = 519;
            this.form.label4.Text = "表示条件";
            this.form.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.form.label4.Visible = true;
            // 
            // HYOUJI_JOUKEN_TEKIYOU
            // 
            this.form.HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(167, 156);
            this.form.HYOUJI_JOUKEN_TEKIYOU.Name = "HYOUJI_JOUKEN_TEKIYOU";
            this.form.HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(60, 16);
            this.form.HYOUJI_JOUKEN_TEKIYOU.TabIndex = 516;
            this.form.HYOUJI_JOUKEN_TEKIYOU.TabStop = false;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.form.HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Visible = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);

            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_TEKIYOUGAI);
            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_DELETED);
            this.form.Controls.Add(this.form.label4);
            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_TEKIYOU);
            #endregion


            this.form.label1.Location = new System.Drawing.Point(12, 79);
            this.form.panel2.Location = new System.Drawing.Point(165, 76);

            this.form.label2.Location = new System.Drawing.Point(12, 104);
            this.form.panel3.Location = new System.Drawing.Point(165, 101);

            this.form.label3.Location = new System.Drawing.Point(12, 129);
            this.form.SearchKindPnl.Location = new System.Drawing.Point(162, 130);

            this.SettingProperties(true);
        }
        /// <summary>
        /// Load And Save setting
        /// </summary>
        /// <param name="isLoad"></param>
        public void SettingProperties(bool isLoad = false)
        {
            try
            {
                if (this._flagChangedJouken)
                {
                    if (isLoad)
                    {
                        this.form.HYOUJI_JOUKEN_TEKIYOU.Checked = Properties.Settings.Default.HYOUJI_JOUKEN_TEKIYOU;
                        this.form.HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.HYOUJI_JOUKEN_DELETED;
                        this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked = Properties.Settings.Default.HYOUJI_JOUKEN_TEKIYOUGAI;
                    }
                    else
                    {
                        //Save Properties
                        Properties.Settings.Default.HYOUJI_JOUKEN_TEKIYOU = this.form.HYOUJI_JOUKEN_TEKIYOU.Checked;
                        Properties.Settings.Default.HYOUJI_JOUKEN_DELETED = this.form.HYOUJI_JOUKEN_DELETED.Checked;
                        Properties.Settings.Default.HYOUJI_JOUKEN_TEKIYOUGAI = this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }
        }
        /// <summary>
        /// Check One in Three Checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var CheckCurrent = sender as CheckBox;
                if (!CheckCurrent.Checked)
                {
                    if (!this.form.HYOUJI_JOUKEN_TEKIYOU.Checked && !this.form.HYOUJI_JOUKEN_DELETED.Checked && !this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                    {
                        this.form.errmessage.MessageBoxShow("E001", "表示条件");
                        CheckCurrent.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }
        }
        /// <summary>
        /// Check value will skip
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool CheckParamsIsValid(string param)
        {
            try
            {
                bool isContinue = false;
                if (this._flagChangedJouken && param != null &&
                   (param.Equals("TEKIYOU_BEGIN") || param.Equals("TEKIYOU_FLG") || param.Equals("DELETE_FLG") || param.Contains("DELETE_FLG")))
                {
                    isContinue = true;
                }
                return isContinue;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                return false;
            }
        }
        //NvNhat #158998 #158999 END
        #endregion
    }
}