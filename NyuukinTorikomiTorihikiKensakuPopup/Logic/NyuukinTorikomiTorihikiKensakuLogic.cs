// $Id: NyuukinTorikomiTorihikiKensakuPopupLogic.cs 55781 2015-07-15 08:58:53Z huangxy@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using NyuukinTorikomiTorihikiKensakuPopup.APP;
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

namespace NyuukinTorikomiTorihikiKensakuPopup.Logic
{
    /// <summary>
    /// 検索共通ポップアップロジック
    /// </summary>
    public class NyuukinTorikomiTorihikiKensakuLogic
    {
        #region フィールド

        /// <summary>
        /// バインドするカラム名一覧
        /// </summary>
        internal string[] bindColumnNames = new string[] { "" };

        internal string[] displayColumnNames = new string[] { };

        internal string[] hideColumnNames = new string[] { };

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "NyuukinTorikomiTorihikiKensakuPopup.Setting.ButtonSetting.xml";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private NyuukinTorikomiTorihikiKensakuForm form;

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
        /// orderby句
        /// </summary>
        private string orderStr = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private string popupType = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string furikomiJinmei = string.Empty;
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
            TORIHIKISAKI_TEL,
            TORIHIKISAKI_NAME1,
            TORIHIKISAKI_NAME2,
            TORIHIKISAKI_ADDRESS2
        }


        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal NyuukinTorikomiTorihikiKensakuLogic(NyuukinTorikomiTorihikiKensakuForm targetForm)
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

            //並び替えボタン(F10)イベント生成
            this.form.bt_func10.Click += new EventHandler(this.form.SortSetting);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.Close);
        }

        /// <summary>
        /// アンカーを設定して、フォームサイズの変更に自動対応
        /// </summary>
        private void CopeResize()
        {
            //リサイズ対応
            this.form.customDataGridView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            this.form.bt_func1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func11.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func12.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            this.form.lb_hint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.form.customSortHeader1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        }
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            //ファンクションキー対応
            this.form.KeyPreview = true;
            //リサイズ対応
            this.CopeResize();

            //ボタンの初期化
            this.ButtonInit();
            // 画面タイトルやDaoを初期化
            this.DisplyInit();

            this.form.customDataGridView1.AllowUserToAddRows = false;

            this.form.customDataGridView1.AllowUserToResizeColumns = false; //行サイズは固定

            EventInit();

            if (this.form.PopupSearchSendParams != null)
            {
                for (int i = this.form.PopupSearchSendParams.Count - 1; i >= 0; i--)
                {
                    if (this.form.PopupSearchSendParams[i].KeyName == "POPUP_TYPE")
                    {
                        popupType = this.form.PopupSearchSendParams[i].Value;
                        this.form.PopupSearchSendParams.RemoveAt(i);
                    }
                    else if (this.form.PopupSearchSendParams[i].KeyName == "FURIKOMI_JINMEI")
                    {
                        furikomiJinmei = this.form.PopupSearchSendParams[i].Value;
                        this.form.PopupSearchSendParams.RemoveAt(i);
                    }
                }
            }
            if (popupType == "3")
            {
                this.form.plFurikomiJinmei.Enabled = false;
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

            //初期値()個別対応する場合は以下のswithで上書きすること
            this.form.CONDITION_ITEM.Text = "3";
            this.form.CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.form.FILTER_SHIIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.FILTER_BOIN_VALUE.ImeMode = ImeMode.Alpha;


            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.M_TORIHIKISAKI:
                case WINDOW_ID.M_TORIHIKISAKI_ALL:
                    // TODO:SQLファイルのパス設定を忘れずに
                    this.dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                    this.form.lb_title.Text = "取引先検索";
                    this.executeSqlFilePath = "NyuukinTorikomiTorihikiKensakuPopup.Sql.GetTorihikisakiDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(TORIHIKISAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "フリガナ", "郵便番号", "都道府県", "住所", "電話番号", "非表示１", "非表示２", "非表示３" };
                    this.hideColumnNames = new string[] { "TORIHIKISAKI_NAME1","TORIHIKISAKI_NAME2","TORIHIKISAKI_ADDRESS2"}; //画面へ戻せるように隠し
                    break;
                default:
                    break;
            }

            //タイトルラベルの強制変更対応
            if (!string.IsNullOrEmpty(this.form.PopupTitleLabel))
            {
                this.form.lb_title.Text = this.form.PopupTitleLabel;
                ControlUtility.AdjustTitleSize(this.form.lb_title, this.TitleMaxWidth);
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

                if (this.hideColumnNames.Contains(column.Name)) //非表示
                {
                    column.Visible = false;
                }

                this.form.customDataGridView1.Columns.Add(column);
            }

            //ダミーカラム EMPTY　：空文字 を画面反映したい場合に利用
            this.form.customDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "EMPTY",
                HeaderText="EMPTY",
                Visible = false
            });


            //列リサイズ(ここでの処理の場合は、一度だけでは反映されず、2回呼ぶと反映された)
            ResizeColumns(this.form.customDataGridView1);
            ResizeColumns(this.form.customDataGridView1);

            this.form.FURIKOMI_JINMEI_SHIBORIKOMI.Text = "1";
        }

        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 658;

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
                    string sql = sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + this.joinStr + this.whereStr + this.orderStr;

                    //distinct対応 結合条件によっては列が増えるため
                    int idx = sql.IndexOf("SELECT ", StringComparison.InvariantCultureIgnoreCase);
                    int idxDis = sql.IndexOf("DISTINCT ", StringComparison.InvariantCultureIgnoreCase);

                    if (idx > -1 && idxDis < 0)
                    {
                        var newsql = sql.Substring(0, idx) + "SELECT DISTINCT " + sql.Substring(idx + 7);
                        sql = newsql; //ステップ実行用に変数を経由
                    }

                    dt = this.dao.GetDateForStringSql(sql);
                    sqlStr.Close();
                }
            }

            // DataTable table = GetStringDataTable(dt);
            this.SearchResult = dt;

            // 頭文字絞込み
            this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();
            this.form.customSortHeader1.SortDataTable(this.SearchResult);
            this.form.customDataGridView1.DataSource = this.SearchResult;
            this.form.customDataGridView1.ReadOnly = true;
            ResizeColumns(this.form.customDataGridView1);

            //PT498 検索0件時のフォーカス移動不正
            //int returnCount = this.SearchResult.Rows == null ? 0 : 1;
            int returnCount = this.SearchResult == null ? 0 : this.SearchResult.Rows.Count;

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
                ResizeColumns(this.form.customDataGridView1);
            }
        }

        /// <summary>
        /// 選択データ決定処理
        /// </summary>
        internal void ElementDecision()
        {
            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            List<PopupReturnParam> setParam = new List<PopupReturnParam>();
            for (int i = 0; i < this.returnParamNames.Length; i++)
            {
                PopupReturnParam popupParam = new PopupReturnParam();

                //列名に+をつけると結合する
                var setDate = string.Concat(this.returnParamNames[i].Split('+').Select(x => x.Trim()).Select(x => (this.form.customDataGridView1.CurrentRow.Cells[x].Value ?? "").ToString()));

                popupParam.Key = "Value";

                popupParam.Value = setDate;

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
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Disable;
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
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Disable;
                    break;

                case "7":
                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Utility

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            // 初期化
            this.joinStr = string.Empty;
            this.whereStr = string.Empty;
            this.orderStr = string.Empty;

            // 検索画面が増えたら以下に設定を追加していく
            string tableName = string.Empty;
            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                // TODO:.Nemeでちゃんとクラス名取れているか確認
                case WINDOW_ID.M_TORIHIKISAKI:
                case WINDOW_ID.M_TORIHIKISAKI_ALL:
                    this.SearchInfo = new M_TORIHIKISAKI();
                    tableName = typeof(M_TORIHIKISAKI).Name;
                    break;
                default:
                    break;
            }

            //hack:SQLインジェクション対策必要
            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                default:
                    this.orderStr = " ORDER BY " + tableName + "." + tableName.Substring(2) + "_CD ";
                    break;
            }


            // カラム名を動的に指定するために必要
            var ColumnHeaderName = tableName.Substring(2, tableName.Length - 2);

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                // シングルクォートは2つ重ねる
                var condition = this.form.CONDITION_VALUE.Text.Replace("'", "''");
                switch (this.form.CONDITION_ITEM.Text)
                {
                    case "1":
                        // ｺｰﾄﾞ
                        switch (this.form.WindowId)
                        {
                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%' ";
                                break;
                        }
                        break;

                    case "2":
                        // 略称名
                        switch (this.form.WindowId)
                        {
                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%' ";
                                break;
                        }
                        break;

                    case "3":
                        // ﾌﾘｶﾞﾅ
                        switch (this.form.WindowId)
                        {
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
                            default:
                                this.whereStr = " AND M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "5":
                        // 住所
                        switch (this.form.WindowId)
                        {
                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "6":
                        // 電話
                        switch (this.form.WindowId)
                        {
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

            if (this.form.WindowId == WINDOW_ID.M_TORIHIKISAKI_ALL)
            {
                this.whereStr = " WHERE 1 = 1 " + this.whereStr;
            }
            else
            {
                this.whereStr = " WHERE " + CreateWhereStr(tableName) + this.whereStr;
            }

            // 画面から来た絞込み情報で条件句を作成
            bool existSearchParam = false;  // popupSearchSendParamからWHEREが生成されたかどうかのフラグ
            StringBuilder sb = new StringBuilder(" ");
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
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
            if (this.form.plFurikomiJinmei.Enabled)
            {
                if (this.form.WindowId == WINDOW_ID.M_TORIHIKISAKI)
                {
                    if (this.form.FURIKOMI_JINMEI_SHIBORIKOMI.Text == "1")
                    {
                        if (!String.IsNullOrEmpty(furikomiJinmei))
                        {
                            this.whereStr += " AND (M_TORIHIKISAKI_SEIKYUU.FURIKOMI_NAME1 ='" + furikomiJinmei + "' OR M_TORIHIKISAKI_SEIKYUU.FURIKOMI_NAME2 ='" + furikomiJinmei + "')";
                        }

                    }
                }
            }
            this.CreateJoinStr();
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
            string filterStr = string.Empty;

            // DBアクセスを発生させないためDataGridView用の条件を作成する

            if (string.IsNullOrEmpty(this.form.FILTER_SHIIN_VALUE.Text))
            {
                return string.Empty;
            }

            string furiganaCol = GetFuriganaColName();
            if (string.IsNullOrEmpty(this.form.FILTER_BOIN_VALUE.Text))
            {
                string filterInitialStr = string.Empty;
                // 母音(段)が選択されてなければ選択されている子音(行)のをすべてを表示
                switch (this.form.FILTER_SHIIN_VALUE.Text)
                {
                    case "1":
                        filterInitialStr = Constans.Agyou;
                        break;

                    case "2":
                        filterInitialStr = Constans.KAgyou;
                        break;

                    case "3":
                        filterInitialStr = Constans.SAgyou;
                        break;

                    case "4":
                        filterInitialStr = Constans.TAgyou;
                        break;

                    case "5":
                        filterInitialStr = Constans.NAgyou;
                        break;

                    case "6":
                        filterInitialStr = Constans.HAgyou;
                        break;

                    case "7":
                        filterInitialStr = Constans.MAgyou;
                        break;

                    case "8":
                        filterInitialStr = Constans.YAgyou;
                        break;

                    case "9":
                        filterInitialStr = Constans.RAgyou;
                        break;

                    case "10":
                        filterInitialStr = Constans.WAgyou;
                        break;

                    case "11":
                        filterInitialStr = Constans.alphanumeric;
                        break;

                    case "12":
                        // 以下、うまく動かない
                        filterInitialStr = string.Join(",", Constans.Agyou, Constans.KAgyou, Constans.SAgyou, Constans.TAgyou, Constans.NAgyou,
                            Constans.HAgyou, Constans.MAgyou, Constans.YAgyou, Constans.RAgyou, Constans.WAgyou, Constans.alphanumeric);
                        break;

                    default:
                        return string.Empty;
                }

                if ("12".Equals(this.form.FILTER_SHIIN_VALUE.Text))
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
                int BOINIndex = -1;
                if (int.TryParse(this.form.FILTER_BOIN_VALUE.Text, out BOINIndex)
                    && BOINIndex <= this.form.BOINListFilter.Length)
                {
                    // 母音(段)があれば母音(段)で絞込み
                    //濁点等対応
                    //filterStr = furiganaCol + " LIKE '" + this.form.BOINList[BOINIndex - 1] + "%'";
                    filterStr = string.Format("substring({0}, 1, 1) in ({1})", furiganaCol, this.form.BOINListFilter[BOINIndex - 1]);
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
            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_TORIHIKISAKI:
                case WINDOW_ID.M_TORIHIKISAKI_ALL:
                    colName = TORIHIKISAKI_COLUMNS.TORIHIKISAKI_FURIGANA.ToString();
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

        /// <summary>
        /// 有効レコードをチェックするSQLを作成します。
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <returns></returns>
        private static string CreateWhereStr(string tableName)
        {
            return String.Format("CONVERT(DATE, ISNULL({0}.TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL({0}.TEKIYOU_END, DATEADD(day,1,GETDATE()))) AND {0}.DELETE_FLG = 0", tableName);
        }

        /// <summary>
        /// popupWindowSettingの内容からJOIN句を作成します。
        /// </summary>
        private void CreateJoinStr()
        {
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
                    var type = Type.GetType("r_framework.Entity." + joinData.LeftTable+",r_framework");
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
            var field = ControlUtility.CreateFields(controls, dto.Value);

            if (field[0] != null)
            {
                var control = field[0] as ICustomControl;

                if (control != null)
                {
                    return dto.ValueColumnType.ToConvertString(control.GetResultText());
                }
                throw new EdisonException();

            }
            return dto.ValueColumnType.ToConvertString(dto.Value.ToString());
        }

        /// <summary>
        /// 一部の列で自動調整がうまくいかないので 補正つきリサイズ
        /// </summary>
        /// <param name="dgv"></param>
        static public void ResizeColumns(DataGridView dgv)
        {
            //自動整列
            //dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            //自動調整を解除しつつ、すべての横幅を1ドットプラス
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                //c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                c.Width += 1;
            }
        }


        public virtual bool CheckInput()
        {
            LogUtility.DebugMethodStart();
            bool result = true;
            if (string.IsNullOrEmpty(this.form.FURIKOMI_JINMEI_SHIBORIKOMI.Text))
            {
                this.form.FURIKOMI_JINMEI_SHIBORIKOMI.IsInputErrorOccured = true;
                this.form.FURIKOMI_JINMEI_SHIBORIKOMI.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "振込人名絞り込み");
                result = false;
                //フォーカス設定
                this.form.FURIKOMI_JINMEI_SHIBORIKOMI.Focus();
            }
            LogUtility.DebugMethodEnd();
            return result;
        }
        #endregion
    }
}
