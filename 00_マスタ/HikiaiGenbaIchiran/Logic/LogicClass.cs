// $Id: LogicClass.cs 15601 2014-02-04 06:08:35Z ogawa@takumi-sys.co.jp $
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using r_framework;
using r_framework.APP.Base;
using r_framework.APP.Base.IchiranHeader;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using HikiaiGenbaIchiran.APP;

namespace HikiaiGenbaIchiran.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "HikiaiGenbaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// リンク画面設定ファイル
        /// </summary>
        private readonly string LinkWindowInfoXmlPath = "HikiaiGenbaIchiran.Setting.LinkWindowSetting.xml";

        /// <summary>
        /// 画面連携に使用するキー取得項目名1
        /// </summary>
        private readonly string KEY_ID1 = "業者CD";

        /// <summary>
        /// 画面連携に使用するキー取得項目名2
        /// </summary>
        private readonly string KEY_ID2 = "現場CD";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private HikiaiGenbaIchiranForm form;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        private IchiranHeaderForm1 headerForm;

        /// <summary>
        /// 画面連携設定
        /// </summary>
        private LinkWindowSetting[] linkWindowSetting;

        /// <summary>
        /// 全コントロール一覧
        /// </summary>
        private Control[] allControl;

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(HikiaiGenbaIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.headerForm = (IchiranHeaderForm1)((IchiranBaseForm)targetForm.ParentForm).headerForm;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンのテキストを初期化
            this.ButtonInit();
            // イベントの初期化処理
            this.EventInit();
            this.allControl = this.form.allControl;
            this.form.customDataGridView1.AllowUserToAddRows = false;                             //行の追加オプション(false)
            this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro; //ヘッダの背景色
            this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;  //ヘッダの文字色
            // リンク画面情報の初期化
            var thisAssembly = Assembly.GetExecutingAssembly();
            this.linkWindowSetting = LinkWindowSetting.LoadLinkWindowSetting(thisAssembly, this.LinkWindowInfoXmlPath);
            // 拠点CD入力欄の初期化
            this.InitHeaderArea();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタンの初期化
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            //// ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (IchiranBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (IchiranBaseForm)this.form.Parent;
            //Functionボタンのイベント生成
            parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);            //新規
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);            //修正
            parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);            //削除
            parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);            //複写
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);            //CSV
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);            //検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);            //検索
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる
            parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);             //パターン一覧画面へ遷移
            parentForm.bt_process2.Click += new EventHandler(this.bt_process2_Click);             //検索条件設定画面へ遷移

            //画面上でESCキー押下時のイベント生成
            this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(this.form_PreviewKeyDown); //form上でのESCキー押下でFocus移動

            LogUtility.DebugMethodEnd();
        }



        #endregion


        #region Functionボタン 押下処理

        /// <summary>
        /// F2 新規
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG, true);

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.DELETE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F5 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG);

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F6 CSV
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {
                CSVFileLogic csvLogic = new CSVFileLogic();

                csvLogic.DataGridVew = this.form.customDataGridView1;

                DENSHU_KBN id = this.form.DenshuKbn;

                csvLogic.FileName = id.ToTitleString();
                csvLogic.headerOutputFlag = true;

                csvLogic.CreateCSVFileForDataGrid(this.form);

                msgLogic.MessageBoxShow("I000");
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.searchString.Clear();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.ShowData();

            HikiaiGenbaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
            HikiaiGenbaIchiran.Properties.Settings.Default.Save();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            HikiaiGenbaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
            HikiaiGenbaIchiran.Properties.Settings.Default.Save();

            var parentForm = (IchiranBaseForm)this.form.Parent;
            parentForm.Close();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region プロセスボタン押下処理（※処理未実装）
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //var us = new PatternIchiranForm(this.form.ShainCd,this.form.DenshuKbn);
            //us.Show();

            LogUtility.DebugMethodEnd(sender, e);

        }
        /// <summary>
        /// 検索条件設定画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //var us = new KensakuJoukenSetteiForm(this.form.DenshuKbn);
            //us.Show();

            LogUtility.DebugMethodEnd(sender, e);

        }
        #endregion

        #region 処理No(ESC)でのエンターキー押下イベント
        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void txb_process_KeyDown(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.form.Parent;

            if ("1".Equals(parentForm.txb_process.Text))
            {
                //パターン一覧画面へ遷移
                this.bt_process1_Click(sender, e);
            }
            else if ("2".Equals(parentForm.txb_process.Text))
            {
                //検索条件設定画面へ遷移
                this.bt_process2_Click(sender, e);
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region ESCキー押下イベント
        void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.form.Parent;

            if (e.KeyCode == Keys.Escape)
            {
                //処理No(ESC)へカーソル移動
                parentForm.txb_process.Focus();
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }
        #endregion

        #region ヘッダーの初期化

        private void InitHeaderArea()
        {
            // チェックメソッド設定
            this.headerForm.HEADER_KYOTEN_CD.PopupGetMasterField = this.headerForm.HEADER_KYOTEN_CD.PopupGetMasterField;
            this.headerForm.HEADER_KYOTEN_CD.SetFormField = this.headerForm.HEADER_KYOTEN_CD.PopupSetFormField;
            Collection<SelectCheckDto> dtoCollection = new Collection<SelectCheckDto>();
            SelectCheckDto dto = new SelectCheckDto();
            dto.CheckMethodName = "拠点マスタコードチェックandセッティング";
            dtoCollection.Add(dto);
            this.headerForm.HEADER_KYOTEN_CD.FocusOutCheckMethod = dtoCollection;

            // ポップアップ設定
            this.headerForm.HEADER_KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";

            // 初期値設定
            this.headerForm.HEADER_KYOTEN_CD.Text = HikiaiGenbaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT;
        }

        #endregion

        #region 現場入力画面起動処理

        /// <summary>
        /// 現場入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        private void OpenWindow(WINDOW_TYPE windowType, bool newFlg = false)
        {
            LogUtility.DebugMethodStart(windowType, newFlg);

            if (this.linkWindowSetting.Length <= 0)
            {
                Console.WriteLine("画面定義が読み込めていない。");
                LogUtility.DebugMethodEnd(windowType, newFlg);
            }

            // 引数へのオブジェクトを作成する
            // 新規の場合は引数なし、ただし参照の場合は引数あり
            object[] sendParams;
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG && newFlg)
            {
                sendParams = new object[] { };
            }
            else
            {
                // 表示されている行の取引先CDを取得する
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                string cd1 = row.Cells[this.KEY_ID1].Value.ToString();
                string cd2 = row.Cells[this.KEY_ID2].Value.ToString();

                sendParams = new object[] { windowType, cd1, cd2 };
            }

            //各アセンブリの読み込みを同一メソッドで行えるように
            // XMLにて読み込みを行うように
            var assembly = Assembly.LoadFrom(this.linkWindowSetting[0].AssemblyName);
            SuperForm superForm = (SuperForm)assembly.CreateInstance(
                    this.linkWindowSetting[0].FormName, // 名前空間を含めたクラス名
                    false, // 大文字小文字を無視するかどうか
                    BindingFlags.CreateInstance, // インスタンスを生成
                    null,
                    sendParams, // コンストラクタの引数
                    null,
                    null
                  );
            if (superForm.IsDisposed)
            {
                return;
            }
            BusinessBaseForm baseForm = new BusinessBaseForm(superForm, WINDOW_TYPE.NEW_WINDOW_FLAG);
            FormControlLogic formLogic = new FormControlLogic();
            var flag = formLogic.ScreenPresenceCheck(superForm);
            if (!flag)
            {
                baseForm.Show();
            }

            LogUtility.DebugMethodEnd(windowType, newFlg);
        }

        #endregion

        #region IBuisinessLogicで必須実装(未使用)

        public int Search()
        {
            throw new NotImplementedException();
        }

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

        #region Equals/GetHashCode/ToString
        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            LogicClass localLogic = other as LogicClass;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion
    }
}
