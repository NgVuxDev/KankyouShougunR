// $Id: KensakuKyoutsuuPopupForMultiKeyLogic.cs 3389 2013-10-10 08:58:06Z y-sato $
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using HikiaiKizonKensakuPopupForMultiKey.APP;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.OriginalException;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.Logic;
using Seasar.Framework.Exceptions;
using r_framework.APP.Base;

namespace HikiaiKizonKensakuPopupForMultiKey.Logic
{
    /// <summary>
    /// 複数キー用検索共通ポップアップロジック
    /// </summary>
    public class HikiaiKizonKensakuPopupForMultiKeyLogic
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
        private static readonly string ButtonInfoXmlPath = "HikiaiKizonKensakuPopupForMultiKey.Setting.ButtonSetting.xml";

        private static readonly string tekiyou1 = " AND (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND {0}.DELETE_FLG = 0  ";

        private static readonly string tekiyou1_Hikiai = " AND (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND GYOUSHA_DELETE_FLG = 0 AND GENBA_DELETE_FLG = 0  ";

        private static readonly string tekiyou2 = " AND (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND {0}.DELETE_FLG = 0  ";

        private static readonly string tekiyou2_Hikiai = " AND (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND GYOUSHA_DELETE_FLG = 0 AND GENBA_DELETE_FLG = 0  ";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private HikiaiKizonKensakuPopupForMultiKeyForm form;

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
        /// ORDER BY句
        /// </summary>
        private string orderStr = string.Empty;

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

        private bool ParentFilterDispFlag = true;

        /// <summary>
        /// 絞り込み条件で使用する符号
        /// </summary>
        public enum CNNECTOR_SIGNS
        {
            EQUALS = 0,
            IN = 1
        }
        // NvNhat #160897
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

        ///// <summary>
        ///// M_BANK_SHITENの表示項目
        ///// </summary>
        //private enum BANK_SHITEN_COLUMNS
        //{
        //    BANK_CD,
        //    BANK_NAME_RYAKU,
        //    BANK_SHITEN_CD,
        //    BANK_SHIETN_NAME_RYAKU,
        //    BANK_SHITEN_FURIGANA,
        //    KOUZA_SHURUI,
        //    KOUZA_NO,
        //    KOUZA_NAME
        //}

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
            GENBA_POST,
            TODOUFUKEN_NAME_RYAKU,
            GENBA_ADDRESS1,
            GENBA_TEL
        }

        ///// <summary>
        ///// M_HINMEIの表示項目
        ///// </summary>
        //private enum HINMEI_COLUMNS
        //{
        //    HINMEI_CD,
        //    HINMEI_NAME_RYAKU,
        //    HINMEI_FURIGANA,
        //    SHURUI_CD,
        //    SHURUI_NAME_RYAKU
        //}

        ///// <summary>
        ///// M_HINMEIの表示項目
        ///// </summary>
        //private enum HINMEI_COLUMNS_FOR_MULTISELECT
        //{
        //    SELECT_CHECK,
        //    HINMEI_CD,
        //    HINMEI_NAME_RYAKU,
        //    HINMEI_FURIGANA,
        //    SHURUI_CD,
        //    SHURUI_NAME_RYAKU
        //}

        /// <summary>
        /// 現場に紐づく業者が引合か判断します
        /// </summary>
        internal bool isHikiaiGyousha = false;

        /// <summary>
        /// PopupSearchSendParamsに業者の引合フラグが条件として入っているか判定します
        /// </summary>
        internal bool isPopupConditionHikiaiFlg = false;

        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal HikiaiKizonKensakuPopupForMultiKeyLogic(HikiaiKizonKensakuPopupForMultiKeyForm targetForm)
        {
            LogUtility.DebugMethodStart();

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
            this.form.bt_func5.Click -= new EventHandler(this.form.Clear);
            this.form.bt_func5.Click += new EventHandler(this.form.Clear);

            //検索ボタン(F8)イベント生成
            this.form.bt_func8.Click -= new EventHandler(this.form.Search);
            this.form.bt_func8.Click += new EventHandler(this.form.Search);

            //確定ボタン(F9)イベント生成
            this.form.bt_func9.Click -= new EventHandler(this.form.Selected);
            this.form.bt_func9.Click += new EventHandler(this.form.Selected);

            //取消ボタン(F10)イベント生成
            this.form.bt_func10.Click -= new EventHandler(this.form.MoveToSort);
            this.form.bt_func10.Click += new EventHandler(this.form.MoveToSort);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click -= new EventHandler(this.form.Close);
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

            string parentLabel = string.Empty;
            string childLabel = string.Empty;
            string hintTextConditon = string.Empty;
            bool setCheckBoxFirst = false;

            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_GENBA:
                    this.dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                    parentLabel = "業者";
                    childLabel = "現場";
                    hintTextConditon = "1～7";
                    this.executeSqlFilePath = "HikiaiKizonKensakuPopupForMultiKey.Sql.GetGenbaDataSql.sql";
                    this.bindColumnNames = Enum.GetNames(typeof(GENBA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "業者CD ", "業者名", "現場CD", "現場名", "現場ﾌﾘｶﾞﾅ", "郵便番号", "都道府県", "現場住所", "現場電話番号" };
                    break;

                default:
                    break;
            }
            this.SettingDisplayKensakuJouken();//NvNhat #160897

            // 画面のラベル表示名を更新
            this.form.lb_title.Text = childLabel + "検索";
            this.form.label16.Text = parentLabel + "検索条件";
            this.form.label3.Text = childLabel + "検索条件";
            this.form.label1.Text = childLabel + "ﾌﾘｶﾞﾅ頭文字(母音)";
            this.form.label2.Text = childLabel + "ﾌﾘｶﾞﾅ頭文字(子音)";

            // Formタイトルの初期化
            this.form.Text = this.form.lb_title.Text;

            // ヒントテキストを更新
            this.form.PARENT_CONDITION_ITEM.Tag = string.Format("【{0}】のいずれかで入力してください", hintTextConditon);
            this.form.PARENT_CONDITION_VALUE.Tag = string.Format("{0}について検索条件を入力してください", parentLabel);
            this.form.CHILD_CONDITION_ITEM.Tag = string.Format("【{0}】のいずれかで入力してください", hintTextConditon);
            this.form.CHILD_CONDITION_VALUE.Tag = string.Format("{0}について検索条件を入力してください", childLabel);

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
                    //column.ReadOnly = true;
                    this.form.customDataGridView1.Columns.Add(column);
                }
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

            //　イベントイニシャル
            this.EventInit();

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

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// 検索条件の入力項目制御処理
        /// 起動ポイントでpopupSearchSendParamが指定されている場合、
        /// Enabledにする検索条件があるため、ここでコントロール。
        /// </summary>
        private void ChangeDisplayFilter()
        {
            LogUtility.DebugMethodStart();

            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
                if (popupSearchSendParam == null || string.IsNullOrEmpty(popupSearchSendParam.KeyName))
                {
                    continue;
                }

                switch (popupSearchSendParam.KeyName)
                {
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

                    default:
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
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
                String tableName1 = String.Empty;
                String tableName2 = String.Empty;
                String columnHeaderName = String.Empty;
                String parentColumnHeaderName = String.Empty;

                // テーブル名、sqlパス設定
                switch (this.form.WindowId)
                {
                    // 画面IDごとに生成を行うDaoを変更する
                    case WINDOW_ID.M_GENBA:
                        if (isHikiai)
                        {
                            tableName1 = typeof(M_HIKIAI_GENBA).Name;
                            tableName2 = typeof(M_HIKIAI_GENBA).Name;
                            this.executeSqlFilePath = "HikiaiKizonKensakuPopupForMultiKey.Sql.GetGenbaHikiaiDataSql.sql";
                            this.orderStr = " ORDER BY M_HIKIAI_GENBA.GYOUSHA_CD, M_HIKIAI_GENBA.GENBA_CD ";
                        }
                        else
                        {
                            tableName1 = typeof(M_GYOUSHA).Name;
                            tableName2 = typeof(M_GENBA).Name;
                            this.executeSqlFilePath = "HikiaiKizonKensakuPopupForMultiKey.Sql.GetGenbaDataSql.sql";
                            this.orderStr = " ORDER BY M_GYOUSHA.GYOUSHA_CD, M_GENBA.GENBA_CD ";
                        }
                        columnHeaderName = "GENBA";
                        parentColumnHeaderName = "GYOUSHA";
                        break;

                    default:
                        break;
                }

                // 検索条件生成
                this.SetSearchString(tableName1, tableName2, columnHeaderName, parentColumnHeaderName);

                DataTable dt = new DataTable();
                // 基本的なスクリプトを取得
                var thisAssembly = Assembly.GetExecutingAssembly();
                using (var resourceStream = thisAssembly.GetManifestResourceStream(this.executeSqlFilePath))
                {
                    using (var sqlStr = new StreamReader(resourceStream))
                    {
                        String s1 = sqlStr.ReadToEnd().Replace(Environment.NewLine, "");
                        String s2 = this.whereStr;
                        dt = this.dao.GetDateForStringSql(s1 + s2 + this.orderStr);

                        // 既に入力されている業者引合フラグに応じて現場の出力にフィルターをかける
                        if (isPopupConditionHikiaiFlg)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (this.isHikiaiGyousha != Convert.ToBoolean(dt.Rows[i]["GYOUSHA_HIKIAI_FLG"].ToString()))
                                {
                                    dt.Rows[i].Delete();
                                }
                            }
                        }

                        sqlStr.Close();
                    }
                }

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
                this.form.customDataGridView1.ReadOnly = true;
                this.form.customDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                // 20140716 syunrei EV005278_現場の虫眼鏡ボタンから開いた現場検索ポップアップにて[F8]検索を押下するとシステムエラー(発生時間20時38分)　start  
                //背景色対応
                //読み取り専用の色を付ける
                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {

                        var ia = cell as ICustomAutoChangeBackColor;
                        if (ia != null)
                        {
                            ia.UpdateBackColor(); //色設定がない場合に対応させる
                        }
                        else
                        {
                            cell.UpdateBackColor(false); //読み取り専用だと最初に色を付ける
                        }
                    }
                }
                // 20140716 syunrei EV005278_現場の虫眼鏡ボタンから開いた現場検索ポップアップにて[F8]検索を押下するとシステムエラー(発生時間20時38分)　end

                // 一部結果を非表示列設定
                switch (this.form.WindowId)
                {
                    // 画面IDごとに生成を行うDaoを変更する
                    case WINDOW_ID.M_GENBA:
                        this.form.customDataGridView1.Columns["GENBA_HIKIAI_FLG"].Visible = false;
                        this.form.customDataGridView1.Columns["GENBA_SHOKUCHI_KBN"].Visible = false;
                        this.form.customDataGridView1.Columns["GYOUSHA_POST"].Visible = false;
                        this.form.customDataGridView1.Columns["GYOUSHA_FURIGANA"].Visible = false;
                        this.form.customDataGridView1.Columns["GYOUSHA_ADDRESS1"].Visible = false;
                        this.form.customDataGridView1.Columns["GYOUSHA_TEL"].Visible = false;
                        this.form.customDataGridView1.Columns["GYOUSHA_HIKIAI_FLG"].Visible = false;
                        this.form.customDataGridView1.Columns["GYOUSHA_SHOKUCHI_KBN"].Visible = false;
                        this.form.customDataGridView1.Columns["TEKIYOU_BEGIN"].Visible = false;
                        this.form.customDataGridView1.Columns["TEKIYOU_END"].Visible = false;
                        this.form.customDataGridView1.Columns["DELETE_FLG"].Visible = false;
                        if (isHikiai)
                        {
                            this.form.customDataGridView1.Columns["GYOUSHA_DELETE_FLG"].Visible = false;
                            this.form.customDataGridView1.Columns["GENBA_DELETE_FLG"].Visible = false;
                        }

                        // 検索条件用のため非表示
                        if (this.form.customDataGridView1.Columns["MANI_HENSOUSAKI_KBN"] != null)
                        {
                            this.form.customDataGridView1.Columns["MANI_HENSOUSAKI_KBN"].Visible = false;
                        }
                        if (this.form.customDataGridView1.Columns["TORIHIKISAKI_CD"] != null)
                        {
                            this.form.customDataGridView1.Columns["TORIHIKISAKI_CD"].Visible = false;
                        }
                        break;

                    default:
                        break;
                }

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
        /// 検索結果を一覧に設定
        /// </summary>
        internal void InvokeInitialSort()
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
        }

        /// <summary>
        /// 選択データ決定処理
        /// </summary>
        internal bool ElementDecision()
        {
            try
            {
                LogUtility.DebugMethodStart();

                Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
                List<PopupReturnParam> setParam = new List<PopupReturnParam>();
                if (!this.form.PopupMultiSelect)
                {
                    for (int i = 0; i < this.returnParamNames.Length; i++)
                    {
                        // 選択行はNull場合、リターンする
                        if (this.form.customDataGridView1.CurrentRow == null) return false;

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
        internal bool ImeControlParentCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

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

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ImeControlParentCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 検索条件入力欄のIME制御
        /// </summary>
        internal bool ImeControlChildCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

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

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ImeControlChildCondition", ex);
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
        private void SetSearchString(String inTableName1, String inTableName2, String inColumnHeaderName, String inParentColumnHeaderName)
        {
            LogUtility.DebugMethodStart(inTableName1, inTableName2, inColumnHeaderName, inParentColumnHeaderName);

            // 初期化
            this.joinStr = string.Empty;
            this.whereStr = string.Empty;
            Boolean isHikiai = this.form.rdlHikiai.Checked;

            // 検索画面が増えたら以下に設定を追加していく
            string tableName1 = inTableName1;
            string tableName2 = inTableName2;
            string parentTableName = string.Empty;
            switch (this.form.WindowId)
            {
                // TODO:.Nemeでちゃんとクラス名取れているか確認
                case WINDOW_ID.M_GENBA:
                    this.SearchInfo = new M_GENBA();
                    break;

                default:
                    break;
            }

            // カラム名を動的に指定するために必要
            //var ColumnHeaderName = tableName.Substring(2, tableName.Length - 2);
            //var parentColumnHeaderName = parentTableName.Substring(2, parentTableName.Length - 2);
            var ColumnHeaderName = inColumnHeaderName;
            var parentColumnHeaderName = inParentColumnHeaderName;

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
                            this.whereStr = " AND " + tableName1 + "." + parentColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                            break;

                        case "2":
                            // 略称名
                            this.whereStr = " AND " + tableName1 + "." + parentColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                            break;

                        case "3":
                            // ﾌﾘｶﾞﾅ
                            this.whereStr = " AND " + tableName1 + "." + parentColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                            break;

                        case "4":
                            // 都道府県
                            // もし数値変換できない場合は設定しない
                            if (isHikiai)
                            {
                                this.whereStr = " AND " + tableName1 + "." + "TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                            }
                            else
                            {
                                this.whereStr = " AND M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                            }
                            break;

                        case "5":
                            // 住所
                            this.whereStr = " AND " + tableName1 + "." + parentColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                            break;

                        case "6":
                            // 電話
                            this.whereStr = "AND " + tableName1 + "." + parentColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                            break;

                        case "7":
                            // ﾌﾘｰ
                            // ﾌﾘｰでは1～6のすべてに対して検索をかける
                            this.whereStr = " AND (" + tableName1 + "." + parentColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                            this.whereStr = this.whereStr + " OR " + tableName1 + "." + parentColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                            this.whereStr = this.whereStr + " OR " + tableName1 + "." + parentColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                            if (isHikiai)
                            {
                                this.whereStr = this.whereStr + " OR " + tableName1 + "." + "TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                            }
                            else
                            {
                                this.whereStr = this.whereStr + " OR M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                            }
                            this.whereStr = this.whereStr + " OR " + tableName1 + "." + parentColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                            this.whereStr = this.whereStr + " OR " + tableName1 + "." + parentColumnHeaderName + "_TEL LIKE '%" + condition + "%')";
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
                        this.whereStr = this.whereStr + " AND " + tableName2 + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                        break;

                    case "2":
                        // 略称名
                        this.whereStr = this.whereStr + " AND " + tableName2 + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                        break;

                    case "3":
                        // ﾌﾘｶﾞﾅ
                        this.whereStr = this.whereStr + " AND " + tableName2 + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                        break;

                    case "4":
                        // 都道府県
                        // もし数値変換できない場合は設定しない
                        if (isHikiai)
                        {
                            this.whereStr = " AND " + tableName1 + "." + "TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                        }
                        else
                        {
                            this.whereStr = " AND M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                        } break;

                    case "5":
                        // 住所
                        this.whereStr = this.whereStr + " AND " + tableName2 + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                        break;

                    case "6":
                        // 電話
                        this.whereStr = this.whereStr + " AND " + tableName2 + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                        break;

                    case "7":
                        // ﾌﾘｰ
                        // ﾌﾘｰでは1～6のすべてに対して検索をかける
                        this.whereStr = this.whereStr + " AND (" + tableName2 + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                        this.whereStr = this.whereStr + " OR " + tableName2 + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                        this.whereStr = this.whereStr + " OR " + tableName2 + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                        if (isHikiai)
                        {
                            this.whereStr = this.whereStr + " OR " + tableName1 + "." + "TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                        }
                        else
                        {
                            this.whereStr = this.whereStr + " OR M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                        }
                        this.whereStr = this.whereStr + " OR " + tableName2 + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                        this.whereStr = this.whereStr + " OR " + tableName2 + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%')";
                        break;

                    default:
                        break;
                }
            }

            //this.whereStr = " WHERE " + CreateWhereStr(tableName2) + this.whereStr;
            //this.whereStr = " WHERE " + tableName2 + ".DELETE_FLG = 0 " + this.whereStr;
            //Add Query //NvNhat #160897
            if (this._flagChangedJouken)
            {
                // チェックボックスからくる条件句
                if (this.form.HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.HYOUJI_JOUKEN_DELETED.Checked || this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    string queryJouken = "";
                    if (!string.IsNullOrEmpty(tableName2))
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
                    this.whereStr += string.Format(queryJouken.ToString(), tableName2);
                }
                //Save Properties
                this.SettingProperties(false);
            }

            this.whereStr = " WHERE 1 = 1 " + this.whereStr;

            // 画面から来た絞込み情報で条件句を作成
            bool existSearchParam = false;  // popupSearchSendParamからWHEREが生成されたかどうかのフラグ
            StringBuilder sb = new StringBuilder(" ");

            this.isPopupConditionHikiaiFlg = false;
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
                //Check if Delete, Tekiyou
                if (this.CheckParamsIsValid(popupSearchSendParam.KeyName))//NvNhat #160897
                {
                    continue;
                }
                // 業者引合フラグの場合は既存現場マスタに存在しないカラムなのでフラグで判定
                if (popupSearchSendParam.KeyName.Equals("GYOUSHA_HIKIAI_FLG"))
                {
                    this.isPopupConditionHikiaiFlg = true;
                    object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { popupSearchSendParam.Control });
                    string controlText = PropertyUtility.GetTextOrValue(control[0]);
                    if (control != null && !string.IsNullOrEmpty(controlText))
                    {
                        this.isHikiaiGyousha = false;
                        if (controlText.Equals("1"))
                        {
                            this.isHikiaiGyousha = true;
                        }
                    }
                    else
                    {
                        this.isPopupConditionHikiaiFlg = false;
                    }
                    continue;
                }
                else if (popupSearchSendParam.KeyName != null && popupSearchSendParam.KeyName.Equals("TEKIYOU_BEGIN"))
                {
                    object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { popupSearchSendParam.Control });
                    string controlText = PropertyUtility.GetTextOrValue(control[0]);
                    var ctr = control[0] as CustomDateTimePicker;
                    var ctr2 = control[0] as DataGridViewTextBoxCell;
                    string tekiyouSql = string.Empty;
                    if (ctr != null && ctr.Value != null)
                    {
                        if (this.form.rdlHikiai.Checked)
                        {
                            tekiyouSql = string.Format(tekiyou1_Hikiai, tableName2, ctr.Value);
                        }
                        else
                        {
                            tekiyouSql = string.Format(tekiyou1, tableName2, ctr.Value);
                        }
                        sb.Append(tekiyouSql);
                    }
                    else if (ctr2 != null && ctr2.Value != null)
                    {
                        if (this.form.rdlHikiai.Checked)
                        {
                            tekiyouSql = string.Format(tekiyou1_Hikiai, tableName2, ctr2.Value);
                        }
                        else
                        {
                            tekiyouSql = string.Format(tekiyou1, tableName2, ctr2.Value);
                        }
                        sb.Append(tekiyouSql);
                    }
                    else
                    {
                        if (this.form.rdlHikiai.Checked)
                        {
                            tekiyouSql = string.Format(tekiyou2_Hikiai, tableName2);
                        }
                        else
                        {
                            tekiyouSql = string.Format(tekiyou2, tableName2);
                        }
                        sb.Append(tekiyouSql);
                    }
                    continue;
                }
                else if (popupSearchSendParam.KeyName != null && popupSearchSendParam.KeyName.Equals("TEKIYOU_FLG")
                    && !string.IsNullOrEmpty(popupSearchSendParam.Value))
                {
                    if ("TRUE".Equals(popupSearchSendParam.Value.ToUpper()))
                    {
                        string tekiyouSql = string.Empty;
                        if (this.form.rdlHikiai.Checked)
                        {
                            tekiyouSql = string.Format(tekiyou2_Hikiai, tableName2);
                        }
                        else
                        {
                            tekiyouSql = string.Format(tekiyou2, tableName2);
                        }
                        sb.Append(tekiyouSql);
                    }
                    else if ("FALSE".Equals(popupSearchSendParam.Value.ToUpper()))
                    {
                        string tekiyouSql = string.Empty;
                        if (this.form.rdlHikiai.Checked)
                        {
                            tekiyouSql = " AND GOUSHA_DELETE_FLG = 0 AND GENBA_DELETE_FLG = 0 ";
                        }
                        else
                        {
                            tekiyouSql = string.Format(" AND {0}.DELETE_FLG = 0 ", tableName2);
                        }
                        sb.Append(tekiyouSql);
                    }
                    continue;
                }

                this.depthCnt = 1;
                existSearchParam = false;
                string where = this.CreateWhereStrFromScreen(popupSearchSendParam, tableName2, ref existSearchParam);
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
                    if (this.CheckParamsIsValid(popupSearchSendParam.KeyName))//NvNhat #160897
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

            LogUtility.DebugMethodEnd(filterStr);
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
                //case WINDOW_ID.M_BANK_SHITEN:
                //    colName = BANK_SHITEN_COLUMNS.BANK_SHITEN_FURIGANA.ToString();
                //    break;

                case WINDOW_ID.M_GENBA:
                    colName = GENBA_COLUMNS.GENBA_FURIGANA.ToString();
                    break;

                //case WINDOW_ID.M_HINMEI:
                //    colName = HINMEI_COLUMNS.HINMEI_FURIGANA.ToString();
                //    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd(colName);
            return colName;
        }

        /// <summary>
        /// 先頭カラムが文字列のDataTableの取得
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetStringDataTable(DataTable dt)
        {
            LogUtility.DebugMethodStart();

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

            LogUtility.DebugMethodEnd(table);
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
            LogUtility.DebugMethodStart();

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

            LogUtility.DebugMethodEnd(string.Empty);
            return string.Empty;
        }

        /// <summary>
        /// PopupSearchSendParamDtoからWHERE句を作成します。
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="whereValue"></param>
        private string CreateWhere(PopupSearchSendParamDto dto)
        {
            LogUtility.DebugMethodStart();

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
                LogUtility.DebugMethodEnd(CNNECTOR_SIGNSExt.ToTypeString(sqlConnectorSign) + " " + returnStr);
                return CNNECTOR_SIGNSExt.ToTypeString(sqlConnectorSign) + " " + returnStr;
            }
            else
            {
                LogUtility.DebugMethodEnd(string.Empty);
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
            LogUtility.DebugMethodStart();

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
            // 20140716 syunrei EV005278_現場の虫眼鏡ボタンから開いた現場検索ポップアップにて[F8]検索を押下するとシステムエラー(発生時間20時38分)　start           
            if (this.form.popupWindowSetting != null)
            {
                // 20140716 syunrei EV005278_現場の虫眼鏡ボタンから開いた現場検索ポップアップにて[F8]検索を押下するとシステムエラー(発生時間20時38分)　end
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
                            if (this.CheckParamsIsValid(searchData.LeftColumn))//NvNhat #160897
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
            LogUtility.DebugMethodStart();
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
        /// NvNhat #160897 BEGIN
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
            this.form.label5 = new System.Windows.Forms.Label();
            this.form.HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();

            // 
            // HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(333, 181);
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Name = "HYOUJI_JOUKEN_TEKIYOUGAI";
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(84, 16);
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 522;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.TabStop = false;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Visible = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);
            // 
            // HYOUJI_JOUKEN_DELETED
            // 
            this.form.HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.form.HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(279, 181);
            this.form.HYOUJI_JOUKEN_DELETED.Name = "HYOUJI_JOUKEN_DELETED";
            this.form.HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(48, 16);
            this.form.HYOUJI_JOUKEN_DELETED.TabIndex = 521;
            this.form.HYOUJI_JOUKEN_DELETED.TabStop = false;
            this.form.HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.form.HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_DELETED.Visible = true;
            this.form.HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);
            // 
            // label5
            // 
            this.form.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.form.label5.ForeColor = System.Drawing.Color.White;
            this.form.label5.Location = new System.Drawing.Point(24, 179);
            this.form.label5.Name = "label5";
            this.form.label5.Size = new System.Drawing.Size(182, 20);
            this.form.label5.TabIndex = 523;
            this.form.label5.Text = "表示条件";
            this.form.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.form.label5.Visible = true;
            // 
            // HYOUJI_JOUKEN_TEKIYOU
            // 
            this.form.HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(213, 181);
            this.form.HYOUJI_JOUKEN_TEKIYOU.Name = "HYOUJI_JOUKEN_TEKIYOU";
            this.form.HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(60, 16);
            this.form.HYOUJI_JOUKEN_TEKIYOU.TabIndex = 520;
            this.form.HYOUJI_JOUKEN_TEKIYOU.TabStop = false;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.form.HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Visible = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);

            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_TEKIYOUGAI);
            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_DELETED);
            this.form.Controls.Add(this.form.label5);
            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_TEKIYOU);
            #endregion

            this.form.label3.Location = new System.Drawing.Point(24, 79);
            this.form.panel2.Location = new System.Drawing.Point(140, 77);

            this.form.label1.Location = new System.Drawing.Point(24, 104);
            this.form.panel3.Location = new System.Drawing.Point(206, 102);

            this.form.label2.Location = new System.Drawing.Point(24, 129);
            this.form.panel4.Location = new System.Drawing.Point(210, 127);

            this.form.label4.Location = new System.Drawing.Point(24, 154);
            this.form.SearchKindPnl.Location = new System.Drawing.Point(209, 154);

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
        //NvNhat #160897 END
        #endregion

    }
}
