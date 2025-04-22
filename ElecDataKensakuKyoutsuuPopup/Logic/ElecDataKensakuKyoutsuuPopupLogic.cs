// $Id: ElecDataKensakuKyoutsuuPopupLogic.cs 30005 2014-09-12 06:06:32Z takeda $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ElecDataKensakuKyoutsuuPopup.APP;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace ElecDataKensakuKyoutsuuPopup.Logic
{
    /// <summary>
    /// 検索共通ポップアップロジック
    /// </summary>
    public class ElecDataKensakuKyoutsuuPopupLogic
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
        private static readonly string ButtonInfoXmlPath = "ElecDataKensakuKyoutsuuPopup.Setting.ButtonSetting.xml";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private ElecDataKensakuKyoutsuuPopupForm form;

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
        /// DENSHI_JIGYOUSHAの表示項目
        /// </summary>
        private enum DENSHI_JIGYOUSHA_COLUMNS
        {
            GYOUSHA_CD,
            JIGYOUSHA_NAME,
            EDI_MEMBER_ID,
            JIGYOUSHA_POST,
            TODOFUKEN_NAME,
            DISP_JIGYOUSHA_ADDRESS,
            JIGYOUSHA_TEL
        }

        #endregion フィールド

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal ElecDataKensakuKyoutsuuPopupLogic(ElecDataKensakuKyoutsuuPopupForm targetForm)
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
        /// カラムヘッダーとこの画面で必須のカラムのチェックをする
        /// </summary>
        /// <returns>true:問題なし、false:問題あり</returns>
        internal bool CheckColumn(out bool catchErr)
        {
            try
            {
                bool returnVal = true;
                catchErr = false;

                List<string> columnList = new List<string>();
                foreach (DataColumn col in this.form.table.Columns)
                {
                    columnList.Add(col.ColumnName);
                }

                foreach (var col in Enum.GetNames(typeof(DENSHI_JIGYOUSHA_COLUMNS)))
                {
                    if (!columnList.Contains(col))
                    {
                        returnVal = false;
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CheckColumn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
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
        internal bool WindowInit()
        {
            try
            {
                //ファンクションキー対応
                this.form.KeyPreview = true;
                //リサイズ対応
                this.CopeResize();

                //ボタンの初期化
                this.ButtonInit();
                // 画面タイトルやDaoを初期化
                this.DisplayInit();

                this.form.customDataGridView1.AllowUserToAddRows = false;

                this.form.customDataGridView1.AllowUserToResizeColumns = false; //行サイズは固定

                EventInit();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 画面のタイトルなどを初期化を行う
        /// </summary>
        private void DisplayInit()
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

                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    this.form.lb_title.Text = "電子事業者検索";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(DENSHI_JIGYOUSHA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    if (this.form.PopupDataHeaderTitle != null
                        && this.form.PopupDataHeaderTitle.Count() > 0)
                    {
                        this.displayColumnNames = this.form.PopupDataHeaderTitle;
                    }
                    else
                    {
                        this.displayColumnNames = new string[] { "加入者番号", "電子事業者名", "郵便番号", "都道府県", "住所", "電話番号", "業者CD", "業者名", "非表示１", "非表示２", "非表示３", "非表示４", "非表示５", "非表示６", "非表示７", "非表示８" };
                        this.hideColumnNames = new string[] { "JIGYOUSHA_ADDRESS1", "JIGYOUSHA_ADDRESS2", "JIGYOUSHA_ADDRESS3", "JIGYOUSHA_ADDRESS4", "GYOUSHA_NAME1", "GYOUSHA_NAME2", "GYOUSHA_ADDRESS1", "GYOUSHA_ADDRESS2" }; //画面へ戻せるように隠し
                    }

                    //フリガナがないため利用不可に
                    this.form.plBOIN.Enabled = false;
                    this.form.panel2.Enabled = false;

                    this.form.CONDITION2.Text = "2.名称";
                    this.form.CONDITION2.Tag = "事業者名が対象の場合チェックを付けてください";

                    this.form.CONDITION3.Enabled = false;

                    this.form.CONDITION_ITEM.Tag = "【1、2、4～7】のいずれかで入力してください";
                    this.form.CONDITION_ITEM.Text = "2"; //ふりがながないので事業者名を初期にする

                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;

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
            int i = 0;
            for (i = 0; i < displayColumnNames.Length; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = this.form.table.Columns[i].ColumnName;
                column.Name = this.form.table.Columns[i].ColumnName;
                column.HeaderText = displayColumnNames[i];

                column.ReadOnly = true;

                this.form.customDataGridView1.Columns.Add(column);
            }

            // 非表示カラムをセット
            while (i < this.form.table.Columns.Count)
            {
                var col = this.form.table.Columns[i];
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = col.ColumnName;
                column.Name = col.ColumnName;
                column.ReadOnly = true;
                column.Visible = false;
                this.form.customDataGridView1.Columns.Add(column);
                i++;
            }

            //ダミーカラム EMPTY　：空文字 を画面反映したい場合に利用
            this.form.customDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "EMPTY",
                HeaderText = "EMPTY",
                Visible = false
            });

            //列リサイズ(ここでの処理の場合は、一度だけでは反映されず、2回呼ぶと反映された)
            ResizeColumns(this.form.customDataGridView1);
            ResizeColumns(this.form.customDataGridView1);
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

        #endregion 初期化処理

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

                if (this.SearchResult != null)
                {
                    this.SearchResult.Clear();
                }

                // 検索条件生成
                this.SetSearchString();

                DataTable dt = new DataTable();
                if (this.form.table != null
                    && this.form.table.Rows.Count > 0)
                {
                    dt = this.form.table;
                }

                var tempData = dt.Select(this.whereStr);
                if (tempData != null && tempData.Count() > 0)
                {
                    this.SearchResult = tempData.CopyToDataTable();
                }

                // 頭文字絞込み
                this.form.customSortHeader1.SortDataTable(this.SearchResult);
                this.form.customDataGridView1.DataSource = this.SearchResult;
                this.form.customDataGridView1.ReadOnly = true;
                ResizeColumns(this.form.customDataGridView1);

                //PT498 検索0件時のフォーカス移動不正
                int returnCount = this.SearchResult == null ? 0 : this.SearchResult.Rows.Count;

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
        /// 選択データ決定処理
        /// </summary>
        internal bool ElementDecision()
        {
            try
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
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElementDecision", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ImeControlCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion イベント用処理

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
                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    this.SearchInfo = new M_DENSHI_JIGYOUSHA();
                    tableName = typeof(M_DENSHI_JIGYOUSHA).Name;
                    break;

                default:
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
                        this.whereStr = "GYOUSHA_CD LIKE '%" + condition + "%' ";
                        break;

                    case "2":
                        // 略称名
                        this.whereStr = "JIGYOUSHA_NAME LIKE '%" + condition + "%' ";
                        break;

                    case "3":
                        // ﾌﾘｶﾞﾅ
                        break;

                    case "4":
                        // 都道府県
                        this.whereStr = "JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%' ";
                        break;

                    case "5":
                        // 住所
                        this.whereStr = "(DISP_JIGYOUSHA_ADDRESS LIKE '%" + condition + "%') ";
                        break;

                    case "6":
                        // 電話
                        this.whereStr = "JIGYOUSHA_TEL LIKE '%" + condition + "%' ";
                        break;

                    case "7":
                        // ﾌﾘｰ
                        // ﾌﾘｰでは1～6のすべてに対して検索をかける
                        this.whereStr = "(GYOUSHA_CD LIKE '%" + condition + "%' ";
                        this.whereStr = this.whereStr + " OR JIGYOUSHA_NAME LIKE '%" + condition + "%' ";
                        //フリガナ無
                        this.whereStr = this.whereStr + " OR JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%' ";
                        this.whereStr = this.whereStr + " OR (DISP_JIGYOUSHA_ADDRESS LIKE '%" + condition + "%' ) ";
                        this.whereStr = this.whereStr + " OR JIGYOUSHA_TEL LIKE '%" + condition + "%')";
                        break;

                    default:
                        break;
                }
            }
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
                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    colName = DENSHI_JIGYOUSHA_COLUMNS.JIGYOUSHA_NAME.ToString();
                    break;

                default:
                    break;
            }

            return colName;
        }

        /// <summary>
        /// 一部の列で自動調整がうまくいかないので 補正つきリサイズ
        /// </summary>
        /// <param name="dgv"></param>
        static public void ResizeColumns(DataGridView dgv)
        {
            if (dgv == null || dgv.RowCount < 1 || dgv.ColumnCount < 1)
            {
                return;
            }

            //自動整列
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            //自動調整を解除しつつ、すべての横幅を1ドットプラス
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                c.Width += 1;
            }
        }

        #endregion
    }
}