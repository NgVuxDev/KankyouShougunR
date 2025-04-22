using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Data;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using OboeGakiIkktuImputIchiran;
using Seasar.Quill.Attrs;
using r_framework.CustomControl;
//using OboegakiIkkatuHoshu.APP;



namespace OboeGakiIkktuImputIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class M421Logic : IBuisinessLogic
    {
    
        /// <summary>
        /// Form
        /// </summary>
        private M421Form form;

        private M421HeaderForm headerForm;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string buttonInfoXmlPath = "OboeGakiIkktuImputIchiran.Setting.ButtonSetting.xml";
        /// <summary>
        /// システムID
        /// </summary>
        private long systemId ;

        /// <summary>
        /// 処理タイプ
        /// </summary>
        public WINDOW_TYPE winType ;

        /// <summary>
        /// 画面間のパラメータ
        /// </summary>
        private T_ITAKU_MEMO_IKKATSU_ENTRY sendDto; 

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }

          /// <summary>
        /// 検索条件
        /// </summary>
        public T_ITAKU_MEMO_IKKATSU_ENTRY searchString { get; set; }
        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// Dao
        /// </summary>
        private IM_ITAKUMEMOIKKATSUDao dao;

        private IM_SYS_INFODao sysInfoDao;

        #endregion   
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal M421Logic(M421Form targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.headerForm = new M421HeaderForm();

            this.dao = DaoInitUtility.GetComponent<IM_ITAKUMEMOIKKATSUDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            this.sendDto = new T_ITAKU_MEMO_IKKATSU_ENTRY();

            this.systemId=0;

            LogUtility.DebugMethodEnd();
        }
   
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンのテキストを初期化
            this.ButtonInit();
            
            //検索データ表示
            this.ItiranDatahyouji();

            // イベントの初期化処理
            this.EventInit();

            //初期表示のコントロール活性化
           // this.ControlKasseika();

            //1行目に初期選択される
            if (this.form.customDataGridView1.RowCount>0)
            {
                 this.form.customDataGridView1.Rows[0].Selected = true;
            }
           

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
      
            // 新規ボタン(F2)イベント生成
            this.form.C_Regist(parentForm.bt_func2);
            parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);
            parentForm.bt_func2.ProcessKbn = PROCESS_KBN.NEW;

            // 修正ボタン(F3)イベント生成
            this.form.C_Regist(parentForm.bt_func3);
            parentForm.bt_func3.Click += new EventHandler(this.bt_func3_Click);
            parentForm.bt_func3.ProcessKbn = PROCESS_KBN.UPDATE;

            // 削除ボタン(F4)イベント生成
            this.form.C_Regist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.bt_func4_Click);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            // CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);
 
            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.bt_func12_Click);

            //画面上でESCキー押下時のイベント生成
            this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(this.form_PreviewKeyDown); //form上でのESCキー押下でFocus移動

   
            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }


        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);
           
        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            T_ITAKU_MEMO_IKKATSU_ENTRY entity = new T_ITAKU_MEMO_IKKATSU_ENTRY();

            this.searchString = entity;
        }
         /// <summary>
        /// 検索データ画面表示
        /// </summary>
        private void ItiranDatahyouji()
        {
            LogUtility.DebugMethodStart();
            //検索データ
            this.Search();

            for (int i = 0; i < searchResult.Rows.Count; i++)
            {
                int row = this.form.customDataGridView1.Rows.Count;

                //行追加
                this.form.customDataGridView1.Rows.Add();

                this.form.customDataGridView1.Rows[i].Cells["DENPYOU_NUMBER"].Value = this.searchResult.Rows[i]["DENPYOU_NUMBER"];
                this.form.customDataGridView1.Rows[i].Cells["MEMO_UPDATE_DATE"].Value = this.searchResult.Rows[i]["MEMO_UPDATE_DATE"];
                this.form.customDataGridView1.Rows[i].Cells["MEMO"].Value = this.searchResult.Rows[i]["MEMO"];
                this.form.customDataGridView1.Rows[i].Cells["SHOBUN_PATTERN_NAME"].Value = this.searchResult.Rows[i]["SHOBUN_PATTERN_NAME"];
                this.form.customDataGridView1.Rows[i].Cells["LAST_SHOBUN_PATTERN_NAME"].Value = this.searchResult.Rows[i]["LAST_SHOBUN_PATTERN_NAME"];
                this.form.customDataGridView1.Rows[i].Cells["SHOBUN_PATTERN_SYSTEM_ID"].Value = this.searchResult.Rows[i]["SHOBUN_PATTERN_SYSTEM_ID"];
                this.form.customDataGridView1.Rows[i].Cells["SHOBUN_PATTERN_SEQ"].Value = this.searchResult.Rows[i]["SHOBUN_PATTERN_SEQ"];
                this.form.customDataGridView1.Rows[i].Cells["LAST_SHOBUN_PATTERN_SYSTEM_ID"].Value = this.searchResult.Rows[i]["LAST_SHOBUN_PATTERN_SYSTEM_ID"];
                this.form.customDataGridView1.Rows[i].Cells["LAST_SHOBUN_PATTERN_SEQ"].Value = this.searchResult.Rows[i]["LAST_SHOBUN_PATTERN_SEQ"];

                this.form.customDataGridView1.Rows[i].Cells["SEQ"].Value = this.searchResult.Rows[i]["SEQ"];
                this.form.customDataGridView1.Rows[i].Cells["SYSTEM_ID"].Value = this.searchResult.Rows[i]["SYSTEM_ID"];

            }
            
            //選択できる属性設定
            this.form.customDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー部
            this.headerForm = (M421HeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;

            //読込み件数
          
            this.headerForm.customNumericTextBox_YOMIKOMI_KENSU.Text = this.searchResult.Rows.Count.ToString();
         
            //// アラート件数
            //M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.systemId.ToString());
            
            //if (sysInfo != null)
            //{
            //    // システム情報からアラート件数を取得
            //    int cnt = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
            //    string str = String.Format("{0:#,000} ", cnt);
            //    this.headerForm.customNumericTextBox_ARAT_KENSU.Text = str;
   
            //}


            LogUtility.DebugMethodEnd();
            
        }
        /// <summary>
        ///初期表示のコントロール活性化
        /// </summary>      
        private void ControlKasseika()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            //件数
            int cnt = this.form.customDataGridView1.Rows.Count;
            if (cnt <= 0)
            {
                parentForm.bt_func3.Enabled = false;

                parentForm.bt_func4.Enabled = false;
            }
            

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///遷移先画面呼び出し
        /// </summary>      
        /// <param name="sendDto">T_ITAKU_MEMO_IKKATSU_ENTRY</param>
        /// <param name="gamenKbn">WINDOW_TYPE</param>
        private void GamenYobidasi(T_ITAKU_MEMO_IKKATSU_ENTRY sendDto, WINDOW_TYPE gamenKbn)
        {
            ////遷移先画面を呼び出し
            //var headerForm = new OboegakiIkkatuHoshuHeader();
            //var callForm = new OboegakiIkkatuHoshuForm(gamenKbn, sendDto);
            //var businessForm = new BusinessBaseForm(callForm, headerForm);
            //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            ////遷移先画面を呼び出し
            //if (!isExistForm)
            //{
            //    businessForm.Show();
            //}


            #region XMLにて読み込み
            LinkWindowSetting linkWindowDto = new LinkWindowSetting();

            string LinkWindowInfoXmlPath = "OboeGakiIkktuImputIchiran.Setting.LinkWindowSetting.xml";
            var thisAssembly = Assembly.GetExecutingAssembly();
            LinkWindowSetting[] linkWindowDtoArray = LinkWindowSetting.LoadLinkWindowSetting(thisAssembly, LinkWindowInfoXmlPath);

            //各アセンブリの読み込みを同一メソッドで行えるように
            // XMLにて読み込みを行うように
            var assembly = Assembly.LoadFrom(linkWindowDtoArray[0].AssemblyName);
            SuperForm superForm = (SuperForm)assembly.CreateInstance(
                    linkWindowDtoArray[0].FormName, // 名前空間を含めたクラス名
                    false, // 大文字小文字を無視するかどうか
                    BindingFlags.CreateInstance, // インスタンスを生成
                    null,
                    new object[] { gamenKbn, sendDto }, // コンストラクタの引数//this.form.ItilanWindowInitUpdate 
                    null,
                    null
                  );

            HeaderBaseForm hearForm = (HeaderBaseForm)assembly.CreateInstance(
                    linkWindowDtoArray[1].FormName, // 名前空間を含めたクラス名
                    false, // 大文字小文字を無視するかどうか
                    BindingFlags.CreateInstance, // インスタンスを生成
                    null,
                    new object[] { }, // コンストラクタの引数//this.form.ItilanWindowInitUpdate 
                    null,
                    null
                  );
            if (superForm.IsDisposed)
            {
                return;
            }
            if (hearForm.IsDisposed)
            {
                return;
            }
            BusinessBaseForm baseForm = new BusinessBaseForm(superForm, hearForm);
            FormControlLogic formLogic = new FormControlLogic();
            var flag = formLogic.ScreenPresenceCheck(superForm);
            if (!flag)
            {
                baseForm.Show();
            }
            #endregion





        }

        /// <summary>
        /// F2 新規
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);
            ////メッセージボックス
            //DialogResult dr = MessageBox.Show("新規登録画面へ", "MSG", System.Windows.Forms.MessageBoxButtons.OKCancel);
            //if (dr.Equals(DialogResult.Cancel)) return;

             //システムID
            this.sendDto.SYSTEM_ID = 0;
            //伝票番号
            this.sendDto.DENPYOU_NUMBER = 0;
            //画面ID
            //int gamenId = (int)WINDOW_ID.M_OBOE_IKKATSU_ICHIRAN;
             //SEQ
            this.sendDto.SEQ = 0;
            //処理タイプ
            this.winType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            //遷移先画面を呼び出し
            if (this.form.seniMotoFlg == 1)
            {
                //自画面クローズ,遷移元に戻る
                bt_func12_Click(sender, e);
            }
            else
            {
                //遷移先画面を呼び出し
                GamenYobidasi(this.sendDto, this.winType);
            }
           
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);
            ////メッセージボックス
            //DialogResult dr = MessageBox.Show("修正登録画面へ", "MSG", System.Windows.Forms.MessageBoxButtons.OKCancel);
            //if (dr.Equals(DialogResult.Cancel)) return;
           
            int selectRowCnt = this.form.customDataGridView1.SelectedRows.Count;

             //選択されない場合
             if (selectRowCnt <= 0 || selectRowCnt > 1)
             {
                 MessageBox.Show("一行を選択してください");

             }
             else //一行選択した場合
             {    
                 //空行を外す
                 int  rowIndex = this.form.customDataGridView1.CurrentRow.Index;
                 if (rowIndex < this.form.customDataGridView1.Rows.Count)
                 {
  
                     //システムID
                     this.sendDto.SYSTEM_ID = (long)this.form.customDataGridView1.CurrentRow.Cells["SYSTEM_ID"].Value;
                     //伝票番号
                     this.sendDto.DENPYOU_NUMBER = (long)this.form.customDataGridView1.CurrentRow.Cells["DENPYOU_NUMBER"].Value;
                     //画面ID
                    // int gamenId = (int)WINDOW_ID.M_OBOE_IKKATSU_ICHIRAN;
                     
                     //SEQ
                     this.sendDto.SEQ = (int)this.form.customDataGridView1.CurrentRow.Cells["SEQ"].Value;

                     //処理タイプ
                     this.winType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                     //遷移先画面を呼び出し
                     if (this.form.seniMotoFlg == 1)
                     {
                         //自画面クローズ,遷移元に戻る
                         bt_func12_Click(sender, e);
                     }
                     else
                     {
                         //遷移先画面を呼び出し
                         GamenYobidasi(this.sendDto, this.winType);
                     }
                     
                 }

             }
             LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);
            ////メッセージボックス
            //DialogResult dr = MessageBox.Show("削除登録画面へ", "MSG", System.Windows.Forms.MessageBoxButtons.OKCancel);
            //if (dr.Equals(DialogResult.Cancel)) return;

            int selectRowCnt = this.form.customDataGridView1.SelectedRows.Count;
            //選択されない場合
            if (selectRowCnt <= 0 || selectRowCnt > 1)
            {
                MessageBox.Show("一行を選択してください");

            }
            else //一行選択した場合
            {
                //空行を外す
                int rowIndex = this.form.customDataGridView1.CurrentRow.Index;
                if (rowIndex < this.form.customDataGridView1.Rows.Count)
                {
 
                    //システムID
                    this.sendDto.SYSTEM_ID = (long)this.form.customDataGridView1.CurrentRow.Cells["SYSTEM_ID"].Value;
                    //伝票番号
                    this.sendDto.DENPYOU_NUMBER = (long)this.form.customDataGridView1.CurrentRow.Cells["DENPYOU_NUMBER"].Value;
                    //画面ID
                    //int gamenId = (int)WINDOW_ID.M_OBOE_IKKATSU_ICHIRAN;

                    //SEQ
                    this.sendDto.SEQ = (int)this.form.customDataGridView1.CurrentRow.Cells["SEQ"].Value;

                    //処理タイプ
                    this.winType = WINDOW_TYPE.DELETE_WINDOW_FLAG;

                    //遷移先画面を呼び出し
                    if (this.form.seniMotoFlg == 1)
                    {
                        //自画面クローズ,遷移元に戻る
                        bt_func12_Click(sender, e);
                    }
                    else
                    {
                        //遷移先画面を呼び出し
                        GamenYobidasi(this.sendDto, this.winType);
                    }
                }
            }
            LogUtility.DebugMethodEnd();
        }

      
        /// <summary>
        /// F6 CSV出力
        /// </summary>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {
                CSVFileLogic csvLogic = new CSVFileLogic();

                csvLogic.DataGridVew = this.form.customDataGridView1;

                WINDOW_ID id = this.form.WindowId;

                csvLogic.FileName = id.ToTitleString();
                csvLogic.headerOutputFlag = true;

                csvLogic.CreateCSVFileForDataGrid(this.form);

                msgLogic.MessageBoxShow("I000");
            }
 
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);

            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();
            //覚書一括入力画面から遷移の場合
            if (this.form.closeMethod != null)
            {
                this.form.closeMethod(this.winType, this.sendDto);
            }

            LogUtility.DebugMethodEnd();
        }

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

        /// <summary>
        /// 一覧データ検索
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            SetSearchString();

            this.searchResult = dao.GetDataForEntity(this.searchString);

            int count = this.searchResult.Rows == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);

            return count;

        }

        #endregion

      
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Insert(bool flg)
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
    }
}
