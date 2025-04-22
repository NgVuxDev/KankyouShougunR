// $Id: ElecDataKensakuKyoutsuuPopupForMultiKeyLogic.cs 29965 2014-09-11 11:01:32Z takeda $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ElecDataKensakuKyoutsuuPopupForMultiKey.APP;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace ElecDataKensakuKyoutsuuPopupForMultiKey.Logic
{
    /// <summary>
    /// 複数キー用検索共通ポップアップロジック
    /// </summary>
    public class ElecDataKensakuKyoutsuuPopupForMultiKeyLogic
    {
        #region フィールド

        /// <summary>
        /// 表示カラム名
        /// </summary>
        internal string[] displayColumnNames = new string[] { };

        /// <summary>
        /// 表示Tag
        /// </summary>
        internal string[] displayTags = new string[] { };

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "ElecDataKensakuKyoutsuuPopupForMultiKey.Setting.ButtonSetting.xml";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private ElecDataKensakuKyoutsuuPopupForMultiKeyForm form;

        /// <summary>
        /// where句
        /// </summary>
        private string whereStr = string.Empty;

        /// <summary>
        /// PopupSearchSendParamDtoの最大深度を保持する
        /// </summary>
        private int depthCnt = 0;

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
        /// M_DENSHI_JIGYOUJOUの表示項目
        /// </summary>
        private enum DENSHI_JIGYOUJOU_COLUMNS
        {
            GENBA_CD,
            JIGYOUJOU_NAME,
            JIGYOUJOU_POST,
            TODOFUKEN_NAME,
            DISP_JIGYOUJOU_ADDRESS,
            JIGYOUJOU_TEL,
            EDI_MEMBER_ID,
        }

        #endregion フィールド

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal ElecDataKensakuKyoutsuuPopupForMultiKeyLogic(ElecDataKensakuKyoutsuuPopupForMultiKeyForm targetForm)
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

            //ソートボタン(F10)イベント生成
            this.form.bt_func10.Click += new EventHandler(this.form.MoveToSort);

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
                catchErr = false;
                bool returnVal = true;

                List<string> columnList = new List<string>();
                foreach (DataColumn col in this.form.table.Columns)
                {
                    columnList.Add(col.ColumnName);
                }

                foreach (var col in Enum.GetNames(typeof(DENSHI_JIGYOUJOU_COLUMNS)))
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
                return true;
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

                this.form.customDataGridView1.AllowUserToResizeColumns = false; //行サイズは固定

                // 条件によってPARENT_CONDITION_ITEMの初期かもするので初期化の一番最後に実行
                this.ChangeDisplayFilter();
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
            this.form.PARENT_CONDITION_ITEM.Text = "3";
            this.form.PARENT_CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.form.CHILD_CONDITION_ITEM.Text = "3";
            this.form.CHILD_CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.form.FILTER_SHIIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.FILTER_BOIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.customDataGridView1.AllowUserToAddRows = false;

            string parentLabel = string.Empty;
            string childLabel = string.Empty;
            string hintTextConditon = string.Empty;

            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                // 画面IDごとに生成を行うDaoを変更する

                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    parentLabel = "電事業者";
                    childLabel = "電子事業場";
                    hintTextConditon = "1、2、3～7";
                    this.returnParamNames = popupGetMasterField;
                    if (this.form.PopupDataHeaderTitle != null
                        && this.form.PopupDataHeaderTitle.Count() > 0)
                    {
                        this.displayColumnNames = this.form.PopupDataHeaderTitle;
                    }
                    else
                    {
                        this.displayColumnNames = new string[] { "加入者番号", "事業者名", "事業場CD", "事業場名", "郵便番号", "都道府県", "事業場住所", "事業場電話番号", "非表示１", "非表示２", "非表示３", "非表示４", "非表示５", "非表示６", "非表示７", "非表示８", "非表示９", "非表示１０" };
                        this.displayTags = new string[] { "加入者番号が表示されます", "事業名が表示されます", "事業場CDが表示されます",
                            "事業場名が表示されます", "郵便番号が表示されます",
                            "都道府県が表示されます", "事業場住所が表示されます", "事業場電話番号が表示されます" , "非表示１", "非表示２", "非表示３", "非表示４", "非表示５", "非表示６", "非表示７", "非表示８", "非表示９", "非表示１０"  };
                    }

                    // レイアウト調整

                    //フリガナ利用不可
                    this.form.panel3.Enabled = false;
                    this.form.plBOIN.Enabled = false;

                    this.form.PARENT_CONDITION3.Enabled = false;
                    this.form.CHILD_CONDITION3.Enabled = false;

                    this.form.PARENT_CONDITION2.Text = "2.名称";
                    this.form.CHILD_CONDITION2.Text = "2.名称";

                    this.form.PARENT_CONDITION_ITEM.Text = "2"; //ふりがながないので名前を初期にする
                    this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;

                    this.form.CHILD_CONDITION_ITEM.Text = "2"; //ふりがながないので名前を初期にする
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                default:
                    break;
            }

            // 画面のラベル表示名を更新
            this.form.lb_title.Text = childLabel + "検索";
            this.form.label16.Text = parentLabel + "検索条件";

            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    this.form.lb_title.Text = "電子事業場検索";
                    this.form.label16.Text = "事業者検索条件";

                    this.form.label3.Text = "事業場検索条件";
                    this.form.label1.Text = "ﾌﾘｶﾞﾅ頭文字(子音)";
                    this.form.label2.Text = "ﾌﾘｶﾞﾅ頭文字(母音)";
                    break;

                default:
                    this.form.label3.Text = childLabel + "検索条件";
                    this.form.label1.Text = childLabel + "ﾌﾘｶﾞﾅ頭文字(子音)";
                    this.form.label2.Text = childLabel + "ﾌﾘｶﾞﾅ頭文字(母音)";
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

            // ヒントテキストを更新
            this.form.PARENT_CONDITION_ITEM.Tag = string.Format("【{0}】のいずれかで入力してください", hintTextConditon);
            this.form.PARENT_CONDITION_VALUE.Tag = string.Format("{0}について検索条件を入力してください", parentLabel);
            this.form.CHILD_CONDITION_ITEM.Tag = string.Format("【{0}】のいずれかで入力してください", hintTextConditon);
            this.form.CHILD_CONDITION_VALUE.Tag = string.Format("{0}について検索条件を入力してください", childLabel);

            // カラム設定(画面ごとに表示カラムは変わらないはず)
            int i = 0;
            for (i = 0; i < displayColumnNames.Length; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = this.form.table.Columns[i].ColumnName;
                column.Name = this.form.table.Columns[i].ColumnName;
                column.HeaderText = displayColumnNames[i];
                if (displayTags.Length > 0)
                {
                    column.CellTemplate.Tag = displayTags[i];
                }
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

            EventInit();

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
        /// 検索条件の入力項目制御処理（画面に業者が入力されていると、検索固定して現場だけの検索にする機能）
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

                //todo:ポップアップ対象追加時修正箇所
                switch (popupSearchSendParam.KeyName)
                {
                    case "GYOUSHA_CD":
                        this.form.PARENT_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                        if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                        {
                            this.form.panel1.Enabled = false;
                            this.ParentFilterDispFlag = false;
                            this.form.PARENT_CONDITION_ITEM.Text = "1";
                        }

                        break;

                    default:
                        break;
                }
            }
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

                // TODO:dataSourceを画面の検索条件で絞り込むようにする
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

                this.form.customDataGridView1.DataSource = this.SearchResult;

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
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool InvokeInitialSort()
        {
            try
            {
                if (this.SearchResult != null)
                {
                    // 頭文字絞込み
                    this.SearchResult.DefaultView.RowFilter = string.Empty;
                    this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();
                    ResizeColumns(this.form.customDataGridView1);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InvokeInitialSort", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
                Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
                List<PopupReturnParam> setParam = new List<PopupReturnParam>();
                if (!this.form.PopupMultiSelect)
                {
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
        internal bool ImeControlParentCondition()
        {
            try
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

        #endregion イベント用処理

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
            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                // TODO:.Nemeでちゃんとクラス名取れているか確認
                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    tableName = typeof(M_DENSHI_JIGYOUJOU).Name;
                    parentTableName = typeof(M_DENSHI_JIGYOUSHA).Name;
                    break;

                default:
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
                    var condition = this.form.PARENT_CONDITION_VALUE.Text.Replace("'", "''");
                    switch (this.form.PARENT_CONDITION_ITEM.Text)
                    {
                        case "1":
                            // ｺｰﾄﾞ
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = "EDI_MEMBER_ID LIKE '%" + condition + "%' ";
                                    break;

                                default:
                                    this.whereStr = parentColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "2":
                            // 略称名
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = "JIGYOUSHA_NAME LIKE '%" + condition + "%' ";
                                    break;

                                default:
                                    this.whereStr = parentColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "3":
                            // ﾌﾘｶﾞﾅ
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = "1=1 ";  //フリガナ無
                                    break;

                                default:
                                    this.whereStr = parentColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "4":
                            // 都道府県
                            // もし数値変換できない場合は設定しない
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = "JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%'";
                                    break;

                                default:
                                    this.whereStr = " TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "5":
                            // 住所
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = "JIGYOUSHA_ADDRESS LIKE '%" + condition + "%' ";
                                    break;

                                default:
                                    this.whereStr = parentColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "6":
                            // 電話
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = "JIGYOUSHA_TEL LIKE '%" + condition + "%' ";
                                    break;

                                default:
                                    this.whereStr = parentColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                                    break;
                            }

                            break;

                        case "7":
                            // ﾌﾘｰ
                            // ﾌﾘｰでは1～6のすべてに対して検索をかける

                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU: //電子は列名が独特
                                    this.whereStr = " (" + "EDI_MEMBER_ID LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + "JIGYOUSHA_NAME LIKE '%" + condition + "%'";
                                    //フリガナ無
                                    this.whereStr = this.whereStr + " OR " + "JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + "JIGYOUSHA_ADDRESS LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + "JIGYOUSHA_TEL LIKE '%" + condition + "%') ";
                                    break;

                                default:
                                    this.whereStr = " (" + parentColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentColumnHeaderName + "_TEL LIKE '%" + condition + "%')";
                                    break;
                            }

                            break;

                        default:
                            break;
                    }
                }
            }

            // 子の検索条件
            if (!string.IsNullOrEmpty(this.form.CHILD_CONDITION_VALUE.Text))
            {
                var condition = this.form.CHILD_CONDITION_VALUE.Text.Replace("'", "''");
                string strAnd = string.Empty;
                strAnd = !string.IsNullOrEmpty(this.whereStr) ? "AND " : string.Empty;

                switch (this.form.CHILD_CONDITION_ITEM.Text)
                {
                    case "1":
                        // ｺｰﾄﾞ
                        this.whereStr = this.whereStr + strAnd + "GENBA_CD LIKE '%" + condition + "%' ";
                        break;

                    case "2":
                        // 略称名
                        this.whereStr = this.whereStr + strAnd + "JIGYOUJOU_NAME LIKE '%" + condition + "%' ";
                        break;

                    case "3":
                        // ﾌﾘｶﾞﾅ
                        break;

                    case "4":
                        // 都道府県
                        this.whereStr = this.whereStr + strAnd + "TODOFUKEN_NAME LIKE '%" + condition + "%' ";
                        break;

                    case "5":
                        // 住所
                        this.whereStr = this.whereStr + strAnd + " ( DISP_JIGYOUJOU_ADDRESS LIKE '%" + condition + "%' ) "; //1と2がある
                        break;

                    case "6":
                        // 電話
                        this.whereStr = this.whereStr + strAnd + "JIGYOUJOU_TEL LIKE '%" + condition + "%' ";
                        break;

                    case "7":
                        // ﾌﾘｰ
                        // ﾌﾘｰでは1～6のすべてに対して検索をかける

                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUJOU: //電子は列名が独特
                                this.whereStr = this.whereStr + strAnd + " ( GENBA_CD LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + "JIGYOUJOU_NAME LIKE '%" + condition + "%'";
                                //フリガナ無
                                this.whereStr = this.whereStr + " OR " + "TODOFUKEN_NAME LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR (" + "DISP_JIGYOUJOU_ADDRESS LIKE '%" + condition + "%' ) ";
                                this.whereStr = this.whereStr + " OR " + "JIGYOUJOU_TEL LIKE '%" + condition + "%') ";
                                break;
                        }

                        break;

                    default:
                        break;
                }
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

            if (string.IsNullOrEmpty(this.form.FILTER_SHIIN_VALUE.Text))
            {
                return string.Empty;
            }

            string furiganaCol = GetFuriganaColName();
            if (string.IsNullOrEmpty(this.form.FILTER_BOIN_VALUE.Text))
            {
                string filterInitialStr = string.Empty;
                // 母音が選択されてなければ選択されている母音のをすべてを表示
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
                    // 母音があれば母音で絞込み

                    //濁点等対応
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
                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    colName = DENSHI_JIGYOUJOU_COLUMNS.JIGYOUJOU_NAME.ToString();
                    break;

                default:
                    break;
            }

            return colName;
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
        /// 一部の列で自動調整がうまくいかないので 補正つきリサイズ
        /// </summary>
        /// <param name="dgv"></param>
        static public void ResizeColumns(DataGridView dgv)
        {
            //自動整列
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            //自動調整を解除しつつ、すべての横幅を1ドットプラス
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                c.Width += 1;
            }
        }

        #endregion Utility
    }
}