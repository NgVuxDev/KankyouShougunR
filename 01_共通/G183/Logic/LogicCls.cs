using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Data;
using r_framework.APP.PopUp.Base;
using System.Reflection;
using ContenaPopup.APP;
using System.Windows.Forms;
using r_framework.Dto;
using r_framework.CustomControl;
using ContenaPopup.DAO;
using r_framework.CustomControl.DataGridCustomControl;

namespace ContenaPopup.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region プロパティ

        /// <summary>
        /// バインドするカラム名一覧
        /// </summary>
        internal string[] bindColumnNames = new string[] { "" };

        internal string[] displayColumnNames = new string[] { };
        
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 起動元への戻り値(カラム名)
        /// </summary>
        public string[] returnParamNames = new string[] { };

        #endregion

        #region フィールド
        /// <summary>
        /// コンテナ検索のDao
        /// </summary>
        private MCTDaoCls MCTDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;
       
        /// <summary>
        /// DTO
        /// </summary>
        private DTOCls dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// M_GYOUSHAの表示項目
        /// </summary>
        private enum CONTENA_COLUMNS
        {
            CONTENA_SHURUI_CD,
            CONTENA_SHURUI_NAME_RYAKU,
            CONTENA_CD,
            CONTENA_NAME_RYAKU,
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU,
            GENBA_CD,
            GENBA_NAME_RYAKU,
            CONTENA_SHURUI_FURIGANA
        }

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "ContenaPopup.Setting.ButtonSetting.xml";

        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.MCTDao = DaoInitUtility.GetComponent<MCTDaoCls>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.dto = new DTOCls();

            LogUtility.DebugMethodEnd();
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
            this.form.CHILD_CONDITION_ITEM.Text = "2";
            this.form.CHILD_CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
            this.form.customDataGridView1.AllowUserToAddRows = false;
            // イベントの初期化
            EventInit();

            // 条件によって初期化する
            this.ChangeDisplayFilter();

        }

        #region 検索条件の入力項目制御処理
        /// <summary>
        /// 検索条件の入力項目制御処理
        /// 起動ポイントでpopupSearchSendParamが指定されている場合、
        /// Enabledにする検索条件があるため、ここでコントロール。
        /// </summary>
        private void ChangeDisplayFilter()
        {

            // 検索パラメタを取得
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
                if (popupSearchSendParam == null || string.IsNullOrEmpty(popupSearchSendParam.KeyName))
                {
                    continue;
                }

                switch (popupSearchSendParam.KeyName)
                {
                    case "GYOUSHA_CD":
                        this.form.GYOUSYA_CD.Text = this.GetControlOrValue(popupSearchSendParam);
                        if (!string.IsNullOrEmpty(this.form.GYOUSYA_CD.Text))
                        {
                            //this.form.GYOUSYA_CD.Enabled = false;
                            //this.form.GYOUSYA_NAME_RYAKU.Enabled = false;
                            //this.form.customPopupOpenButton1.Enabled = false;
                            // 業者名を取得
                            var gyoushaEntity = GetGyousha(this.form.GYOUSYA_CD.Text);
                            this.form.GYOUSYA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        break;

                    case "GENBA_CD":
                        this.form.GENNBA_CD.Text = this.GetControlOrValue(popupSearchSendParam);
                        if (!string.IsNullOrEmpty(this.form.GENNBA_CD.Text))
                        {
                            //this.form.GENNBA_CD.Enabled = false;
                            //this.form.GENNBA_NAME_RYAKU.Enabled = false;
                            //this.form.customPopupOpenButton2.Enabled = false;
                            // 現場名を取得
                            var genba = GetGenba(this.form.GYOUSYA_CD.Text, this.form.GENNBA_CD.Text);
                            this.form.GENNBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                        }
                        break;

                    case "CONTENA_SHURUI_CD":
                        this.form.PARENT_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                        if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                        {
                            //this.form.PARENT_CONDITION_ITEM.Enabled = false;
                            this.form.PARENT_CONDITION_ITEM.Text = "1";
                            //this.form.PARENT_CONDITION1.Enabled = false;
                            //this.form.PARENT_CONDITION2.Enabled = false;
                            //this.form.PARENT_CONDITION3.Enabled = false;
                            //this.form.PARENT_CONDITION4.Enabled = false;
                            //this.form.PARENT_CONDITION_VALUE.Enabled = false;
                        }
                        break;

                    case "CONTENA_CD":
                        this.form.CHILD_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                        if (!string.IsNullOrEmpty(this.form.CHILD_CONDITION_VALUE.Text))
                        {
                            this.form.CHILD_CONDITION_ITEM.Text = "1";
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion

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
        #endregion

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
                case WINDOW_ID.M_CONTENA:
                    //this.form.lb_title.Text = "コンテナ検索";
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(CONTENA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "コンテナ種類CD", "コンテナ種類名", "コンテナCD", "コンテナ名", "業者CD", "業者名", "現場CD", "現場名", "コンテナ種類ふりがな" };
                    break;

                default:
                    break;
            }

            // Formタイトルの初期化
            this.form.Text = this.form.lb_title.Text;

            // カラム設定(画面ごとに表示カラムは変わらないはず)
            DgvCustomAlphaNumTextBoxColumn alphaNumcolumn;
            DgvCustomTextBoxColumn column;
            for (int i = 0; i < bindColumnNames.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        alphaNumcolumn = new DgvCustomAlphaNumTextBoxColumn();
                        alphaNumcolumn.DataPropertyName = bindColumnNames[i];
                        alphaNumcolumn.Name = bindColumnNames[i];
                        alphaNumcolumn.HeaderText = displayColumnNames[i];
                        alphaNumcolumn.CharactersNumber = 0;
                        this.form.customDataGridView1.Columns.Add(alphaNumcolumn);
                        break;
                    
                    case 1:
                        column = new DgvCustomTextBoxColumn();
                        column.DataPropertyName = bindColumnNames[i];
                        column.Name = bindColumnNames[i];
                        column.HeaderText = displayColumnNames[i];
                        column.CharactersNumber = 0;
                        this.form.customDataGridView1.Columns.Add(column);
                        break;
                    
                    case 2:
                        alphaNumcolumn = new DgvCustomAlphaNumTextBoxColumn();
                        alphaNumcolumn.DataPropertyName = bindColumnNames[i];
                        alphaNumcolumn.Name = bindColumnNames[i];
                        alphaNumcolumn.HeaderText = displayColumnNames[i];
                        alphaNumcolumn.CharactersNumber = 0;
                        this.form.customDataGridView1.Columns.Add(alphaNumcolumn);
                        break;
                    
                    case 3:
                        column = new DgvCustomTextBoxColumn();
                        column.DataPropertyName = bindColumnNames[i];
                        column.Name = bindColumnNames[i];
                        column.HeaderText = displayColumnNames[i];
                        column.CharactersNumber = 0;
                        this.form.customDataGridView1.Columns.Add(column);
                        break;
                   
                    case 4:
                        alphaNumcolumn = new DgvCustomAlphaNumTextBoxColumn();
                        alphaNumcolumn.DataPropertyName = bindColumnNames[i];
                        alphaNumcolumn.Name = bindColumnNames[i];
                        alphaNumcolumn.HeaderText = displayColumnNames[i];
                        alphaNumcolumn.CharactersNumber = 0;
                        alphaNumcolumn.ZeroPaddengFlag = true;
                        this.form.customDataGridView1.Columns.Add(alphaNumcolumn);
                        break;
                   
                    case 5:
                        column = new DgvCustomTextBoxColumn();
                        column.DataPropertyName = bindColumnNames[i];
                        column.Name = bindColumnNames[i];
                        column.HeaderText = displayColumnNames[i];
                        column.CharactersNumber = 0;
                        this.form.customDataGridView1.Columns.Add(column);
                        break;
                    
                    case 6:
                        alphaNumcolumn = new DgvCustomAlphaNumTextBoxColumn();
                        alphaNumcolumn.DataPropertyName = bindColumnNames[i];
                        alphaNumcolumn.Name = bindColumnNames[i];
                        alphaNumcolumn.HeaderText = displayColumnNames[i];
                        alphaNumcolumn.CharactersNumber = 0;
                        this.form.customDataGridView1.Columns.Add(alphaNumcolumn);
                        break;
                   
                    case 7:
                    case 8:
                        column = new DgvCustomTextBoxColumn();
                        column.DataPropertyName = bindColumnNames[i];
                        column.Name = bindColumnNames[i];
                        column.HeaderText = displayColumnNames[i];
                        column.CharactersNumber = 0;
                        this.form.customDataGridView1.Columns.Add(column);
                        break;
                    default:
                        break;       

                }
            }
            // 幅設定
            this.form.customDataGridView1.Columns[0].Width = 130;
            this.form.customDataGridView1.Columns[1].Width = 150;
            this.form.customDataGridView1.Columns[2].Width = 120;
            this.form.customDataGridView1.Columns[3].Width = 150;
            this.form.customDataGridView1.Columns[4].Width = 80;
            this.form.customDataGridView1.Columns[5].Width = 150;
            this.form.customDataGridView1.Columns[6].Width = 80;
            this.form.customDataGridView1.Columns[7].Width = 150;
            this.form.customDataGridView1.Columns[8].Width = 0;

            // 非表示設定
            this.form.customDataGridView1.Columns[8].Visible = false;
           
        }

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {

            //クリアボタン(F7)イベント生成
            this.form.bt_func7.Click += new EventHandler(this.form.Clear);

            //検索ボタン(F8)イベント生成
            this.form.bt_func8.Click += new EventHandler(this.form.Search);

            //確定ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.Selected);

            //並べ替えボタン(F10)イベント生成
            this.form.bt_func10.Click += new EventHandler(this.form.SortSetting);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.Close);
        }
        #endregion


        #region イベント用処理

        #region コンテナ種類検索条件入力欄のIME制御
        /// <summary>
        /// コンテナ種類検索条件入力欄のIME制御
        /// </summary>
        internal void ImeControlCondition()
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

                default:
                    break;
            }
        }
        #endregion

        #region コンテナ検索条件入力欄のIME制御
        /// <summary>
        /// コンテナ検索条件入力欄のIME制御
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
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region 検索処理
        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            this.SearchResult = new DataTable();

            this.dto.Gyousya_Cd = this.form.GYOUSYA_CD.Text;
            this.dto.Gennba_Cd = this.form.GENNBA_CD.Text;
            this.dto.Parent_Condition_Item = this.form.PARENT_CONDITION_ITEM.Text;
            this.dto.Parent_Condition_Value = this.form.PARENT_CONDITION_VALUE.Text;
            this.dto.Child_Condition_Item = this.form.CHILD_CONDITION_ITEM.Text;
            this.dto.Child_Condition_Value = this.form.CHILD_CONDITION_VALUE.Text;


            this.SearchResult = MCTDao.GetDataForEntity(this.dto);
            int cnt = this.SearchResult.Rows.Count;

            this.form.customSortHeader1.SortDataTable(this.SearchResult);
            this.form.customDataGridView1.DataSource = this.SearchResult;
            this.form.customDataGridView1.ReadOnly = true;

            LogUtility.DebugMethodEnd(cnt);
            return cnt;
        }
        #endregion

        #region 選択データ決定処理
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
        #endregion

        #region 業者チェック
        /// <summary>
        /// 業者チェック
        /// </summary>
        internal void CheckGyousha()
        {
            LogUtility.DebugMethodStart();
            // 初期化
            this.form.GYOUSYA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSYA_NAME_RYAKU.ReadOnly = true;

            if (string.IsNullOrEmpty(this.form.GYOUSYA_CD.Text))
            {
                // 業者名、現場CD、現場名をクリアする
                this.form.GYOUSYA_NAME_RYAKU.Text = String.Empty;
                this.form.GENNBA_CD.Text = String.Empty;
                this.form.GENNBA_NAME_RYAKU.Text = String.Empty;
                return;
            }

            var gyoushaEntity = GetGyousha(this.form.GYOUSYA_CD.Text);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (gyoushaEntity == null)
            {
                msgLogic.MessageBoxShow("E020", "業者");
                // 業者CD、業者名、現場CD、現場名をクリアする
                this.form.GYOUSYA_CD.Text = String.Empty;
                this.form.GYOUSYA_NAME_RYAKU.Text = String.Empty;
                this.form.GENNBA_CD.Text = String.Empty;
                this.form.GENNBA_NAME_RYAKU.Text = String.Empty;
                this.form.GYOUSYA_CD.Focus();
                return;
            }
            else
            {
                // 業者名
                this.form.GYOUSYA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                // 現場CD、現場名をクリアする
                this.form.GENNBA_CD.Text = String.Empty;
                this.form.GENNBA_NAME_RYAKU.Text = String.Empty;

            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 現場チェック
        /// <summary>
        /// 現場チェック
        /// </summary>
        internal void CheckGenba()
        {
            LogUtility.DebugMethodStart();
            // 初期化
            this.form.GENNBA_NAME_RYAKU.Text = string.Empty;
            this.form.GENNBA_NAME_RYAKU.ReadOnly = true;

            if (string.IsNullOrEmpty(this.form.GENNBA_CD.Text))
            {
                // 現場CD、現場名をクリアする
                this.form.GENNBA_CD.Text = String.Empty;
                this.form.GENNBA_NAME_RYAKU.Text = String.Empty;
                return;
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            M_GENBA genba = new M_GENBA();
            genba = GetGenba(this.form.GYOUSYA_CD.Text, this.form.GENNBA_CD.Text);
            if (genba == null)
            {
                msgLogic.MessageBoxShow("E020", "現場");
                // 現場CD、現場名をクリアする
                this.form.GENNBA_CD.Text = String.Empty;
                this.form.GENNBA_NAME_RYAKU.Text = String.Empty;
                this.form.GENNBA_CD.Focus();
                return;
            }
            else
            {
                // 業者CD
                this.form.GYOUSYA_CD.Text = genba.GYOUSHA_CD;

                // 業者名
                var gyoushaEntity = GetGyousha(this.form.GYOUSYA_CD.Text);
                this.form.GYOUSYA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                // 現場CD
                this.form.GENNBA_CD.Text = genba.GENBA_CD;
                // 現場名
                this.form.GENNBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;

            }


            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 業者取得
        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return null;
            }

            M_GYOUSHA keyEntity = new M_GYOUSHA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

            if (gyousha == null || gyousha.Length < 1)
            {
                return null;
            }
            else
            {
                return gyousha[0];
            }
        }
        #endregion

        #region 現場取得(複数)
        /// <summary>
        /// 現場取得(複数)
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string genbaCd)
        {
            if (string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GENBA_CD = genbaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            return genba;
        }
        #endregion

        #region 現場取得
        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            // PK指定のため1件
            return genba[0];
        }
        #endregion
        
        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region その他(使わない)

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
