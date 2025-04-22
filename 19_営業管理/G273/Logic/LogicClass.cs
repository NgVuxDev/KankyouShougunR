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
using System.Reflection;
using System.Windows.Forms;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.DTO;
using Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.DAO;
using r_framework.CustomControl;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 内部定義

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private const string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.Setting.ButtonSetting.xml";

        private const int DEF_GRID_COL_IDX_MONTH_09 = 4;
        private const int DEF_GRID_COL_IDX_MONTH_10 = 5;
        private const int DEF_GRID_COL_IDX_MONTH_11 = 6;
        private const int DEF_GRID_COL_IDX_MONTH_12 = 7;
        private const int DEF_GRID_COL_IDX_MONTH_01 = 8;
        private const int DEF_GRID_COL_IDX_MONTH_02 = 9;
        private const int DEF_GRID_COL_IDX_MONTH_03 = 10;
        private const int DEF_GRID_COL_IDX_MONTH_04 = 11;
        private const int DEF_GRID_COL_IDX_MONTH_05 = 12;
        private const int DEF_GRID_COL_IDX_MONTH_06 = 13;
        private const int DEF_GRID_COL_IDX_MONTH_07 = 14;
        private const int DEF_GRID_COL_IDX_MONTH_08 = 15;
        private const int DEF_GRID_COL_IDX_MONTH_TOTAL = 16;

        #endregion

        #region 内部変数

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        HeaderForm headerForm;

        /// <summary>
        /// 更新データリスト
        /// </summary>
        List<T_EIGYO_YOSAN> updateList;

        /// <summary>
        /// 挿入データリスト
        /// </summary>
        List<T_EIGYO_YOSAN> insertList;

        /// <summary>
        /// メッセージ表示ロジック
        /// </summary>
        MessageBoxShowLogic msgLogic;

        #endregion

        #region プロパティ

        /// <summary>
        /// DTO
        /// </summary>
        private EigyoYosanDto dto;

        /// <summary>
        /// DAO
        /// </summary>
        private EigyoYosanNyuuryokuDao dao;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 部署マスタデータ取得テーブル
        /// </summary>
        DataTable bushoResult { get; set; }

        /// <summary>
        /// 自社情報マスタデータ取得テーブル
        /// </summary>
        DataTable corpResult { get; set; }

        /// <summary>
        /// 自社情報から取得した期首月
        /// </summary>
        int kishuMonth { get; set; }

        /// <summary>
        /// 期首月から作成した月リスト
        /// </summary>
        string[] kishuMonthArray = new string[12];

        /// <summary>
        /// 期首月から作成した開始月
        /// </summary>
        string startMonth { get; set; }

        /// <summary>
        /// 期首月から作成した終了月
        /// </summary>
        string endMonth { get; set; }

        /// <summary>
        /// 期首月から算出した年度開始年月日
        /// </summary>
        DateTime nendoStartYMD { get; set; }

        /// <summary>
        /// 期首月から算出した年度終了年月日
        /// </summary>
        DateTime nendoEndYMD { get; set; }

        /// <summary>
        /// DataTable
        /// </summary>
        DataTable ResultTable { get; set; }

        /// <summary>
        /// DGV表示データ有無
        /// </summary>
        bool dispDataRecord = false;

        /// <summary>
        /// 期首月をもとに表示順で項目名の配列を格納します
        /// </summary>
        string[] MonthArray = new string[12];

        /// <summary>
        /// １月～１２月の項目名の配列を格納します
        /// </summary>
        string[] nenkanMonthArray = new string[12];

        /// <summary>
        /// 指定年度から作成したデータ取得テーブル
        /// </summary>
        DataTable nendoTable = new DataTable();

        /// <summary>
        /// 指定年度の次の年度から作成したデータ取得テーブル
        /// </summary>
        DataTable jinenTable = new DataTable();

        /// <summary>
        /// 指定年度になる月のリストを格納します
        /// 例：期首月５月の場合５月～１２月
        /// </summary>
        List<string> nendoMonthArray = new List<string>();

        /// <summary>
        /// 次年分になる月のリストを格納します
        /// 例：期首月５月の場合１月～４月
        /// </summary>
        List<string> jinenMonthArray = new List<string>();

        /// <summary>
        /// 検索時の伝票区分を保持します
        /// </summary>
        internal string saveDenpyouKbn = string.Empty;


        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new EigyoYosanDto();
            this.dao = DaoInitUtility.GetComponent<EigyoYosanNyuuryokuDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                //プロセスボタンを非表示設定
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.ProcessButtonPanel.Visible = false;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //期首月の取得・設定
                this.GetKishutuki();

                //DGVヘッダ用名称作成
                this.CreateNendoMonth();

                // 指定年度及び次年分のリストを取得・設定
                this.SetMonthArray();

                //DGVにヘッダタイトル設定
                this.SetHeaderName();

                //年度選択カレンダの初期設定
                this.SetCalendar();

                //初期表示用空白1レコードの追加
                this.CreateNoDataRecord();

                //DGVヘッダの高さ調整
                this.form.customDataGridView1.ColumnHeadersHeight = 20;

                // 伝票区分（１．売上）
                this.form.DENPYOU_KBN_CD.Text = "1";

                //修正箇所[FormとLogic2箇所]
                //※[罫線を非表示設定の場合のみ使用の為コメントアウト]罫線を非表示にした項目名称を中央に寄せておく
                //this.form.customDataGridView1.Columns["BUSHO_NAME"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;
                //this.form.customDataGridView1.Columns["EIGYOU_NAME"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G273", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.SetReferenceMode();
                }
         
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
           
        }
        #endregion

        #region 参照モード表示

        /// <summary>
        /// 参照モード用項目制御処理を行います
        /// </summary>
        private void SetReferenceMode()
        {
            // MainForm
            this.form.customDataGridView1.ReadOnly = true;

            // FunctionButton
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func9.Enabled = false;
        }

        #endregion

        #region HeaderForm取得

        /// <summary>
        /// HeaderForm取得
        /// </summary>
        /// <param name="hs">hs</param>
        public void SetHeaderInfo(HeaderForm hs)
        {
            LogUtility.DebugMethodStart(hs);

            this.headerForm = hs;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 空白1レコードを追加
        /// <summary>
        /// 空白1レコードを追加する
        /// </summary>
        void CreateNoDataRecord()
        {
            LogUtility.DebugMethodStart();

            #region テーブル作成
            this.ResultTable = new DataTable();
            ResultTable.Columns.Add("EIGYOU_CD", Type.GetType("System.String"));
            ResultTable.Columns.Add("EIGYOU_NAME", Type.GetType("System.String"));
            ResultTable.Columns.Add("BUSHO_CD", Type.GetType("System.String"));
            ResultTable.Columns.Add("BUSHO_NAME", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH1", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH2", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH3", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH4", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH5", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH6", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH7", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH8", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH9", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH10", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH11", Type.GetType("System.String"));
            ResultTable.Columns.Add("MONTH12", Type.GetType("System.String"));
            ResultTable.Columns.Add("GOUKEI", Type.GetType("System.String"));
            ResultTable.Columns.Add("SYSTEM_ID", Type.GetType("System.String"));
            ResultTable.Columns.Add("SEQ", Type.GetType("System.String"));
            #endregion

            // 空白レコードの追加はしない
            this.form.customDataGridView1.Rows.Clear();
            this.ResultTable.Rows.Clear();

            #region 削除：空白レコードの追加はしない(空白レコードの追加）
            //this.form.customDataGridView1.Rows.Add();
            //DataRow newRow = this.ResultTable.NewRow();
            //newRow["EIGYOU_CD"] = Type.GetType("System.DBNull");
            //newRow["EIGYOU_NAME"] = Type.GetType("System.DBNull");
            //newRow["BUSHO_CD"] = Type.GetType("System.DBNull");
            //newRow["BUSHO_NAME"] = Type.GetType("System.DBNull");

            //newRow["MONTH1"] = Type.GetType("System.DBNull");
            //newRow["MONTH2"] = Type.GetType("System.DBNull");
            //newRow["MONTH3"] = Type.GetType("System.DBNull");
            //newRow["MONTH4"] = Type.GetType("System.DBNull");
            //newRow["MONTH5"] = Type.GetType("System.DBNull");
            //newRow["MONTH6"] = Type.GetType("System.DBNull");
            //newRow["MONTH7"] = Type.GetType("System.DBNull");
            //newRow["MONTH8"] = Type.GetType("System.DBNull");
            //newRow["MONTH9"] = Type.GetType("System.DBNull");
            //newRow["MONTH10"] = Type.GetType("System.DBNull");
            //newRow["MONTH11"] = Type.GetType("System.DBNull");
            //newRow["MONTH12"] = Type.GetType("System.DBNull");

            //newRow["GOUKEI"] = Type.GetType("System.DBNull");
            //newRow["SYSTEM_ID"] = Type.GetType("System.DBNull");
            //newRow["SEQ"] = Type.GetType("System.DBNull");

            //this.ResultTable.Rows.Add(newRow);
            #endregion

            //表示データなし
            this.dispDataRecord = false;

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

            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);

        }
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            //Functionボタンのイベント生成
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func5.Click += new EventHandler(bt_func5_Click);            //[F5]条件再設定イベント
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);            //[F6]CSV出力イベント
            parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);            //[F7]条件ｸﾘｱイベント
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);            //[F8]検索イベント
            parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);            //[F9]登録イベント
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);          //[F12]閉じるイベント

            // 初期表示用
            parentForm.Shown += new EventHandler(form_Shown);

            #region (コメントアウト：カレンダーからテキストへ変更対応)
            ////年度変更イベント
            //this.form._dt_nendo.TextChanged += new EventHandler(dt_nendo_TextChanged); //カレンダー変更イベント
            #endregion

            // グリッドイベント
            this.form.customDataGridView1.CellBeginEdit += new DataGridViewCellCancelEventHandler(customDataGridView1_CellBeginEdit);
            this.form.customDataGridView1.CellEndEdit += new DataGridViewCellEventHandler(customDataGridView1_CellEndEdit);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 表示完了イベント
        /// <summary>
        /// 初期フォーカス用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_Shown(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // CustomDateTimePicker.OnValidating()実行のため
                this.form.dtp_nendo.Select();
                this.form.dtp_nendo.Focus();

                this.form.tb_busho_cd.Select();
                this.form.tb_busho_cd.Focus();
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 年度選択カレンダの初期設定(カレンダーからテキストへ変更対応)

        /// <summary>
        /// 年度選択カレンダの初期設定
        /// </summary>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void SetCalendar()
        {
            LogUtility.DebugMethodStart();
            try
            {

                var parentForm = (BusinessBaseForm)this.form.Parent;
                //カレンダーに設定
                this.form.dtp_nendo.Value = CorpInfoUtility.GetCurrentYear(parentForm.sysDate, (short)this.kishuMonth);

                #region 日付コントロール変更対応(コメント化)
                ////yyy/MM/dd形式の文字列を渡して年度(yyyy)取得・設定
                //this.SetNendo(today);
                #endregion
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 年度を算出(コメントアウト：カレンダーからテキストへ変更対応)
        ///// <summary>
        ///// 年度を算出して値を設定する
        ///// </summary>
        ///// <param name="date">yyyy/MM/dd形式の年月日を表す文字列</param>
        //void SetNendo(string date)
        //{
        //    LogUtility.DebugMethodStart(date);
        //    try
        //    {
        //        //パラメータ
        //        DateTime selectDate = DateTime.Parse(date);
        //        //違う値でテスト
        //        //selectDate = DateTime.Parse(date).AddYears(3).AddMonths(2).AddDays(20);

        //        string year = DateTime.Parse(date).ToString("yyyy");
        //        string nendoBegin = year + "/" + this.startMonth + "/" + "01";
        //        //年度開始年月日
        //        this.nendoStartYMD = DateTime.Parse(DateTime.Parse(nendoBegin).ToString("yyyy/MM/dd"));
        //        //年度終了年月日
        //        this.nendoEndYMD = DateTime.Parse(DateTime.Parse(nendoBegin).AddYears(1).AddDays(-1).ToString("yyyy/MM/dd"));

        //        if (selectDate < this.nendoStartYMD)
        //        {
        //            //前年度
        //            this.form._tb_nendo.Text = selectDate.AddYears(-1).ToString("yyyy");
        //        }
        //        else if (selectDate > this.nendoEndYMD)
        //        {
        //            //次年
        //            this.form._tb_nendo.Text = selectDate.AddYears(1).ToString("yyyy");
        //        }
        //        else
        //        {
        //            //今年度
        //            this.form._tb_nendo.Text = selectDate.ToString("yyyy");
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion

        #region 自社情報の期首月を取得・設定
        /// <summary>
        /// 自社情報の期首月を取得・設定する
        /// </summary>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void GetKishutuki()
        {
            LogUtility.DebugMethodStart();

            try
            {
                M_CORP_INFO corpEntity = new M_CORP_INFO();
                corpEntity.SYS_ID = 0;
                this.corpResult = dao.GetCorpDataForEntity(corpEntity);
                this.kishuMonth = int.Parse(this.corpResult.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 指定年度及び次年分のリストを取得・設定します
        /// <summary>
        /// 指定年度及び次年分のリストを取得・設定します
        /// </summary>
        internal void SetMonthArray()
        {
            int count = 0;
            // DGVへ設定する項目を機種月から順に並び替える
            foreach (string Month in this.kishuMonthArray)
            {
                this.MonthArray[count] = "MONTH" + Month.Replace("月", string.Empty);
                count++;
            }

            // １月から１２月までの項目名配列を取得
            count = 0;
            for (int i = 1; i <= 12; i++)
            {
                this.nenkanMonthArray[count] = "MONTH" + i.ToString();
                count++;
            }

            // 指定年度分の月のリストを取得
            for (int i = this.kishuMonth; i <= 12; i++)
            {
                this.nendoMonthArray.Add("MONTH" + i.ToString());
            }

            // 次年分の月のリストを取得
            for (int i = 1; i <= (this.kishuMonth - 1); i++)
            {
                this.jinenMonthArray.Add("MONTH" + i.ToString());
            }
        }
        #endregion


        #region 取得した期首月から12か月分のヘッダ用名称作成
        /// <summary>
        /// 取得した期首月から12か月分のヘッダ用名称を作成する
        /// </summary>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void CreateNendoMonth()
        {
            LogUtility.DebugMethodStart();

            try
            {
                for (int i = 0; i < this.kishuMonthArray.Length; i++)
                {
                    int sum = this.kishuMonth + i;
                    if (12 >= sum)
                    {
                        this.kishuMonthArray[i] = sum.ToString() + "月";

                        if (0 == i)
                        {
                            this.startMonth = sum.ToString().PadLeft(2, '0');
                        }
                        else if (i == this.kishuMonthArray.Length - 1)
                        {
                            this.endMonth = sum.ToString().PadLeft(2, '0');
                        }
                    }
                    else
                    {
                        this.kishuMonthArray[i] = (sum - 12).ToString() + "月";

                        if (0 == i)
                        {
                            this.startMonth = (sum - 12).ToString().PadLeft(2, '0');
                        }
                        else if (i == this.kishuMonthArray.Length - 1)
                        {
                            this.endMonth = (sum - 12).ToString().PadLeft(2, '0');
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ヘッダ名称をDGVのHeaderTextへ設定する

        /// <summary>
        /// ヘッダ名称をDGVのHeaderTextへ設定する
        /// </summary>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void SetHeaderName()
        {
            LogUtility.DebugMethodStart();

            try
            {
                for (int i = 0; i < this.kishuMonthArray.Length; i++)
                {
                    this.form.customDataGridView1.Columns[i + 4].HeaderText = this.kishuMonthArray[i];
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region カレンダー変更イベント(コメントアウト：カレンダーからテキストへ変更対応)

        ///// <summary>
        ///// カレンダー変更イベント
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //void dt_nendo_TextChanged(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(this.form._dt_nendo.Text))
        //        {
        //            string getDate = DateTime.Parse(this.form._dt_nendo.Text).ToString("yyyy/MM/dd");
        //            this.SetNendo(getDate);
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion

        #region 条件設定入力箇所の編集可否変更(カレンダーからテキストへ変更対応)
        /// <summary>
        /// 条件設定入力箇所の編集可否を変更する
        /// </summary>
        /// <param name="param">true:編集可/false:編集不可</param>
        void SearchConditionChange(bool param)
        {
            LogUtility.DebugMethodStart(param);

            try
            {
                #region (コメントアウト：カレンダーからテキストへ変更対応)
                //this.form._dt_nendo.Enabled = param;
                //this.form._tb_nendo.Enabled = param;
                #endregion
                //条件設定箇所の操作可否を変更
                this.form.dtp_nendo.Enabled = param;
                this.form.tb_busho_cd.Enabled = param;
                this.form.DENPYOU_KBN_CD.Enabled = param;
                this.form.DENPYOU_KBN_URIAGE.Enabled = param;
                this.form.DENPYOU_KBN_SHIHARAI.Enabled = param;
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [F5]条件再設定ボタンイベント

        /// <summary>
        /// [F5]条件再設定ボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //条件設定箇所を可にする
                SearchConditionChange(true);

                //前の結果をクリア
                int k = this.form.customDataGridView1.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
                }

                //初期表示用空白1レコードの追加
                CreateNoDataRecord();

                //ヘッダ部表示項目もクリア
                this.headerForm.CREATE_USER.Text = string.Empty;
                this.headerForm.CREATE_DATE.Text = string.Empty;
                this.headerForm.UPDATE_DATE.Text = string.Empty;
                this.headerForm.UPDATE_USER.Text = string.Empty;
            }
            catch
            {
                throw;
            }


            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [F6]CSV出力ボタンイベント

        /// <summary>
        /// [F6]CSV出力ボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (this.dispDataRecord)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        CSVExport exp = new CSVExport();
                        exp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(this.form.WindowId), this.form);
                    }
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [F7]条件クリアボタンイベント(カレンダーからテキストへ変更対応)

        /// <summary>
        /// [F7]条件クリアボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //条件設定箇所を可にする
                SearchConditionChange(true);

                //検索条件をクリア
                #region 日付コントロール変更対応(コメント化)
                //this.form._dt_nendo.Text = DateTime.Today.ToString("yyyy/MM/dd");
                //this.form._tb_nendo.Text = string.Empty;
                #endregion
                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.dtp_nendo.Value = CorpInfoUtility.GetCurrentYear(parentForm.sysDate, (short)this.kishuMonth);

                this.form.tb_busho_cd.Text = string.Empty;
                this.form.tb_busho_name.Text = string.Empty;
                this.form.DENPYOU_KBN_CD.Text = "1";

                //DGVの明細もクリア
                this.form.customDataGridView1.Rows.Clear();
                //ヘッダ部表示項目もクリア
                this.headerForm.CREATE_USER.Text = string.Empty;
                this.headerForm.CREATE_DATE.Text = string.Empty;
                this.headerForm.UPDATE_DATE.Text = string.Empty;
                this.headerForm.UPDATE_USER.Text = string.Empty;

                //前の結果をクリア
                int k = this.form.customDataGridView1.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
                }
                //初期表示用空白1レコードの追加
                CreateNoDataRecord();

                // カレンダー初期設定
                this.SetCalendar();
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [F8]検索ボタンイベント(カレンダーからテキストへ変更対応)

        /// <summary>
        /// [F8]検索ボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //条件設定箇所をロックする
                SearchConditionChange(false);

                //検索条件取得
                this.dto = new EigyoYosanDto();

                // 年度取得
                string nendo = this.form.dtp_nendo.DateTimeNowYear;

                // 伝票区分取得
                this.saveDenpyouKbn = this.form.DENPYOU_KBN_CD.Text;

                #region 入力値チェック
                //年度の必須チェック
                if (string.IsNullOrEmpty(nendo))
                {
                    //未入力チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "年度");
                    SearchConditionChange(true);
                    this.form.dtp_nendo.Focus();
                    return;
                }
                else if (1753 > int.Parse(nendo))
                {
                    //DateTime下限値チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E032", "1753", "年度");
                    SearchConditionChange(true);
                    this.form.dtp_nendo.Focus();
                    return;
                }
                else if (9998 < int.Parse(nendo))
                {
                    //上限値チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E082", "年度");
                    SearchConditionChange(true);
                    this.form.dtp_nendo.Focus();
                    return;
                }
                // No2661-->
                //部署の必須チェック
                if (string.IsNullOrEmpty(this.form.tb_busho_cd.Text))
                {
                    //未入力チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "部署");
                    SearchConditionChange(true);
                    this.form.tb_busho_cd.Focus();
                    return;
                }
                // No2661<--
                if (string.IsNullOrEmpty(this.form.DENPYOU_KBN_CD.Text))
                {
                    //未入力チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "伝票区分");
                    SearchConditionChange(true);
                    this.form.DENPYOU_KBN_CD.Focus();
                    return;
                }


                #endregion

                // 前の結果をクリア
                int k = this.form.customDataGridView1.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
                }

                // DBから明細部データ取得
                this.dto.NENDO = nendo;
                this.dto.DENPYOU_KBN_CD = this.saveDenpyouKbn;
                //選択された年度の開始～終了年月日を算出
                this.dto.STARTNENDO = nendo + "/" + startMonth + "/" + "01";
                this.dto.ENDNENDO = DateTime.Parse(this.dto.STARTNENDO).AddYears(1).AddDays(-1).ToString("yyyy/MM/dd");
                if (!string.IsNullOrEmpty(this.form.tb_busho_cd.Text))
                {
                    // No2661-->
                    if (!this.form.tb_busho_cd.Text.Equals("999"))
                    {
                        this.dto.BUSHO_CD = this.form.tb_busho_cd.Text;
                    }
                    // No2661<--
                }
                // 表示用テーブル
                DataTable table = this.dao.GetDispDataForEntity(this.dto);
                // 年度テーブル
                this.nendoTable = this.dao.GetDispDataForEntity(this.dto);

                // 期首月が１月以外の場合
                if (this.kishuMonth != 1)
                {
                    // 次年のデータを取得
                    this.dto.NENDO = (Convert.ToInt16(this.dto.NENDO) + 1).ToString();
                    this.jinenTable = this.dao.GetDispDataForEntity(this.dto);
                    this.dto.NENDO = (Convert.ToInt16(this.dto.NENDO) - 1).ToString();

                    // 上書き対象のカラムのReadOnlyを一時的に外す
                    foreach (var s in this.jinenMonthArray)
                    {
                        table.Columns[s].ReadOnly = false;
                    }

                    // 次年テーブルを表示用テーブルに上書き
                    for (int i = 0; i < jinenTable.Rows.Count; i++)
                    {
                        foreach (var s in this.jinenMonthArray)
                        {
                            table.Rows[i][s] = this.jinenTable.Rows[i][s];
                        }
                    }

                    // ReadOnlyを戻す
                    foreach (var s in this.jinenMonthArray)
                    {
                        table.Columns[s].ReadOnly = true;
                    }
                }
                else
                {
                    table = this.nendoTable;
                }

                // DGVへ値をセット
                SetDataGridView(table, this.form.customDataGridView1, true);

                if (0 == table.Rows.Count)
                {
                    //健作結果が0件の場合、メッセージを出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                    return;
                }

                //更新後、初回登録者/日時＆最終更新者/日時を更新する。
                SetHeaderArea(table);

            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ヘッダ部に初回登録者・日時と最終更新者・日時を設定
        /// <summary>
        /// ヘッダ部に初回登録者・日時と最終更新者・日時を設定
        /// </summary>
        /// <param name="dataTbl">dataTbl</param>
        private void SetHeaderArea(DataTable dataTbl)
        {
            LogUtility.DebugMethodStart(dataTbl);

            // 作成日付が最新の行
            DataRow createRow = this.GetMaxDateTimeRow("CREATE_DATE", dataTbl);
            this.headerForm.CREATE_USER.Text = createRow["CREATE_USER"] == null ? string.Empty : createRow["CREATE_USER"].ToString();
            this.headerForm.CREATE_DATE.Text = createRow["CREATE_DATE"] == null ? string.Empty : createRow["CREATE_DATE"].ToString();

            // 更新日付が最新の行
            DataRow updateRow = this.GetMaxDateTimeRow("UPDATE_DATE", dataTbl);
            this.headerForm.UPDATE_USER.Text = updateRow["UPDATE_USER"] == null ? string.Empty : updateRow["UPDATE_USER"].ToString();
            this.headerForm.UPDATE_DATE.Text = updateRow["UPDATE_DATE"] == null ? string.Empty : updateRow["UPDATE_DATE"].ToString();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 指定されたフィールドの最大値の行を返却
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="dataTbl"></param>
        /// <returns></returns>
        private DataRow GetMaxDateTimeRow(string fieldName, DataTable dataTbl)
        {
            LogUtility.DebugMethodStart(fieldName, dataTbl);

            DateTime? maxCreateDay = dataTbl.AsEnumerable().Max(t => t.Field<DateTime?>(fieldName));
            DataRow createRow = dataTbl.AsEnumerable().
                Where<DataRow>(t => t.Field<DateTime?>(fieldName) == maxCreateDay).FirstOrDefault();

            LogUtility.DebugMethodEnd(createRow);
            return createRow;
        }
        #endregion

        #region [F9]登録ボタンイベント

        /// <summary>
        /// [F9]登録ボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        [Transaction]
        void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 登録データ有り確認
                if (!this.dispDataRecord || this.form.customDataGridView1.RowCount <= 0)
                {
                    //登録対象なしのメッセージ
                    msgLogic.MessageBoxShow("E061");
                    return;
                }

                //登録確認メッセージ
                if (msgLogic.MessageBoxShow("C046", "登録") == DialogResult.Yes)
                {
                    // 期首月が一月以外の場合
                    if (this.kishuMonth != 1)
                    {
                        int monthArrayCount = 0;
                        // 上書き対象のカラムのReadOnlyを一時的に外す
                        foreach (var s in this.MonthArray)
                        {
                            nendoTable.Columns[s].ReadOnly = false;
                            jinenTable.Columns[s].ReadOnly = false;
                        }

                        // 次年テーブルを登録用に上書き
                        for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                        {
                            // 指定年度
                            monthArrayCount = 0;
                            for (int j = 1; j <= 12 - (this.kishuMonth - 1); j++)
                            {
                                this.nendoTable.Rows[i][nendoMonthArray[monthArrayCount]] = SqlInt32.Parse(this.form.customDataGridView1.Rows[i].Cells["MONTH" + j.ToString()].Value.ToString().Replace(",", "")).Value;
                                monthArrayCount++;
                            }

                            // 次年
                            monthArrayCount = 0;
                            for (int j = 12 - (this.kishuMonth - 2); j <= 12; j++)
                            {
                                this.jinenTable.Rows[i][jinenMonthArray[monthArrayCount]] = SqlInt32.Parse(this.form.customDataGridView1.Rows[i].Cells["MONTH" + j.ToString()].Value.ToString().Replace(",", "")).Value;
                                monthArrayCount++;
                            }
                        }

                        // 上書き対象のカラムのReadOnlyを一時的に外す
                        foreach (var s in this.MonthArray)
                        {
                            nendoTable.Columns[s].ReadOnly = true;
                            jinenTable.Columns[s].ReadOnly = true;
                        }

                        //データベースに登録および更新を行う。
                        var jinenDGV = this.form.customDataGridView1;

                        // 指定年度を登録および更新
                        SetDataGridView(this.nendoTable, this.form.customDataGridView1, false);
                        bool ret = this.Data_InsertUpdate(this.form.customDataGridView1, this.form.dtp_nendo.Text);
                        if (!ret)
                        {
                            msgLogic.MessageBoxShow("E080");
                            return;
                        }

                        // 次年を登録および更新
                        var jinenDGV2 = SetDataGridView(this.jinenTable, jinenDGV, false);
                        ret = this.Data_InsertUpdate(jinenDGV2, (Convert.ToInt16(this.form.dtp_nendo.Text) + 1).ToString());

                        if (ret)
                        {
                            // ポップアップが上がっている時に表示が次年
                            this.bt_func8_Click(null, null);
                            msgLogic.MessageBoxShow("I001", "登録");
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E080");
                            return;
                        }
                    }
                    else
                    {
                        //データベースに登録および更新を行う。
                        bool ret = this.Data_InsertUpdate(this.form.customDataGridView1, this.form.dtp_nendo.DateTimeNowYear);
                        if (ret)
                        {
                            msgLogic.MessageBoxShow("I001", "登録");
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E080");
                            return;
                        }
                    }

                    //明細部再表示
                    //前の結果をクリア
                    int k = this.form.customDataGridView1.Rows.Count;
                    for (int i = k; i >= 1; i--)
                    {
                        this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
                    }
                    //検索処理
                    DataTable table = this.dao.GetDispDataForEntity(this.dto);
                    this.bt_func8_Click(null, null);

                    //更新後、初回登録者/日時＆最終更新者/日時を更新する。
                    SetHeaderArea(table);
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 登録/更新処理

        /// <summary>
        /// 登録/更新処理
        /// </summary>
        /// <param name="CDGV">DataGridView</param>
        /// <returns></returns>
        private bool Data_InsertUpdate(CustomDataGridView CDGV, string tourokuNendo)
        {
            LogUtility.DebugMethodStart(CDGV, tourokuNendo);

            //明細部データ取得
            this.GetMeisaiIchiranData(CDGV, tourokuNendo);
            try
            {
                using (Transaction tran = new Transaction()) //トランザクション処理
                {
                    //updata
                    if (updateList != null && updateList.Count() > 0)
                    {
                        foreach (T_EIGYO_YOSAN updateData in updateList)
                        {
                            int updateCount = this.dao.Update(updateData);
                        }
                    }
                    //insert
                    if (insertList != null && insertList.Count() > 0)
                    {
                        foreach (T_EIGYO_YOSAN insertData in insertList)
                        {
                            var oldData = this.dao.GetDataByKey(insertData);
                            if (oldData != null)
                            {
                                tran.Dispose();
                                return false;
                            }
                            int insertCount = this.dao.Insert(insertData);
                        }
                    }
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                //LogUtility.Debug(ex);//例外はここで処理

                //if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                //{
                //    LogUtility.Warn(ex); //排他は警告
                //    var messageShowLogic = new MessageBoxShowLogic();
                //    messageShowLogic.MessageBoxShow("E080");
                //}
                //else
                //{
                //    LogUtility.Error(ex); //その他はエラー
                //    var messageShowLogic = new MessageBoxShowLogic();
                //    messageShowLogic.MessageBoxShow("E093");
                //}
                //LogUtility.DebugMethodEnd(false);
                //return false;
                throw;
            }
            LogUtility.DebugMethodEnd(true);
            return true;

        }

        #endregion

        #region 明細部のデータを取得
        /// <summary>
        /// 明細部データ取得
        /// </summary>
        /// /// <returns></returns>
        private void GetMeisaiIchiranData(CustomDataGridView CDGV, string tourokuNendo)
        {
            LogUtility.DebugMethodStart(CDGV, tourokuNendo);

            updateList = new List<T_EIGYO_YOSAN>();
            insertList = new List<T_EIGYO_YOSAN>();
            //■■■■■■■■■■■■■■■■■■■■■■■■■
            //※ユーザ情報(WHOカラム設定が正常に動作したら削除)
            //■■■■■■■■■■■■■■■■■■■■■■■■■
            //String UsrName = System.Environment.UserName;
            //UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;
            //DateTime datatime = DateTime.Now;
            //string pcname = System.Environment.MachineName;
            //■■■■■■■■■■■■■■■■■■■■■

            foreach (DataGridViewRow dgvRow in CDGV.Rows)
            {
                T_EIGYO_YOSAN dispDataUpdate = new T_EIGYO_YOSAN();
                T_EIGYO_YOSAN dispDataInsert = new T_EIGYO_YOSAN();

                // WHOカラム設定共通ロジック呼び出し用
                //var dataBind_juChuuKensuuEntityUpdate = new DataBinderLogic<T_JUCHU_M_KENSU>(dispDataUpdate);
                //var dataBind_juChuuKensuuEntityInsert = new DataBinderLogic<T_JUCHU_M_KENSU>(dispDataInsert);

                //systemIdがあればUpdate対象
                if (!string.IsNullOrEmpty(dgvRow.Cells[17].Value.ToString()))
                {
                    //■■■■■■■■■■■■■■■■■■■■■■■
                    //update対象データなのでDELETE_FLG = trueに設定
                    //■■■■■■■■■■■■■■■■■■■■■■■
                    // WHOカラム設定
                    //OLD
                    //dataBind_juChuuKensuuEntityUpdate.SetSystemProperty(dispDataUpdate, true);
                    //NEW
                    var WHO = new DataBinderLogic<T_EIGYO_YOSAN>(dispDataUpdate);
                    WHO.SetSystemProperty(dispDataUpdate, true);

                    //画面表示項目------------------
                    dispDataUpdate.NUMBERED_YEAR = SqlInt32.Parse(tourokuNendo);
                    dispDataUpdate.DENPYOU_KBN_CD = this.saveDenpyouKbn;
                    dispDataUpdate.BUSHO_CD = dgvRow.Cells[0].Value.ToString();
                    dispDataUpdate.SHAIN_CD = dgvRow.Cells[2].Value.ToString();
                    dispDataUpdate.SHAIN_NAME = dgvRow.Cells[3].Value.ToString();
                    dispDataUpdate.MONTH_YOSAN_01 = SqlInt32.Parse(dgvRow.Cells[4].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_02 = SqlInt32.Parse(dgvRow.Cells[5].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_03 = SqlInt32.Parse(dgvRow.Cells[6].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_04 = SqlInt32.Parse(dgvRow.Cells[7].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_05 = SqlInt32.Parse(dgvRow.Cells[8].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_06 = SqlInt32.Parse(dgvRow.Cells[9].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_07 = SqlInt32.Parse(dgvRow.Cells[10].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_08 = SqlInt32.Parse(dgvRow.Cells[11].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_09 = SqlInt32.Parse(dgvRow.Cells[12].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_10 = SqlInt32.Parse(dgvRow.Cells[13].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_11 = SqlInt32.Parse(dgvRow.Cells[14].Value.ToString().Replace(",", ""));
                    dispDataUpdate.MONTH_YOSAN_12 = SqlInt32.Parse(dgvRow.Cells[15].Value.ToString().Replace(",", ""));
                    //設定項目------------------
                    dispDataUpdate.SYSTEM_ID = SqlInt64.Parse(dgvRow.Cells[17].Value.ToString());
                    dispDataUpdate.SEQ = SqlInt32.Parse(dgvRow.Cells[18].Value.ToString());
                    dispDataUpdate.TIME_STAMP = ConvertStrByte.In32ToByteArray((int)dgvRow.Cells["TIME_STAMP"].Value);
                    //■■■■■■■■■■■■■■■■■■■■■■■■■■
                    //※WHOカラム設定((WHOカラム設定が正常に動作したら削除))
                    dispDataUpdate.DELETE_FLG = SqlBoolean.True;
                    //dispDataUpdate.UPDATE_DATE = SqlDateTime.Parse(datatime.ToString());
                    //dispDataUpdate.UPDATE_USER = UsrName;
                    //dispDataUpdate.UPDATE_PC = pcname;
                    //■■■■■■■■■■■■■■■■■■■■■■■■■■
                    updateList.Add(dispDataUpdate);
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells[17].Value.ToString()))
                {
                    //■■■■■■■■■■■■■■■■
                    //insert対象データ(既存データ更新)
                    //■■■■■■■■■■■■■■■■
                    // WHOカラム設定
                    //OLD
                    //dataBind_juChuuKensuuEntityInsert.SetSystemProperty(dispDataInsert, false);
                    //NEW
                    var WHO = new DataBinderLogic<T_EIGYO_YOSAN>(dispDataInsert);
                    WHO.SetSystemProperty(dispDataInsert, false);

                    //画面表示項目------------------
                    dispDataInsert.NUMBERED_YEAR = SqlInt32.Parse(tourokuNendo);
                    dispDataInsert.DENPYOU_KBN_CD = this.saveDenpyouKbn;
                    dispDataInsert.BUSHO_CD = dgvRow.Cells[0].Value.ToString();
                    dispDataInsert.SHAIN_CD = dgvRow.Cells[2].Value.ToString();
                    dispDataInsert.SHAIN_NAME = dgvRow.Cells[3].Value.ToString();
                    dispDataInsert.MONTH_YOSAN_01 = SqlInt32.Parse(dgvRow.Cells[4].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_02 = SqlInt32.Parse(dgvRow.Cells[5].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_03 = SqlInt32.Parse(dgvRow.Cells[6].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_04 = SqlInt32.Parse(dgvRow.Cells[7].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_05 = SqlInt32.Parse(dgvRow.Cells[8].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_06 = SqlInt32.Parse(dgvRow.Cells[9].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_07 = SqlInt32.Parse(dgvRow.Cells[10].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_08 = SqlInt32.Parse(dgvRow.Cells[11].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_09 = SqlInt32.Parse(dgvRow.Cells[12].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_10 = SqlInt32.Parse(dgvRow.Cells[13].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_11 = SqlInt32.Parse(dgvRow.Cells[14].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_12 = SqlInt32.Parse(dgvRow.Cells[15].Value.ToString().Replace(",", ""));
                    //設定項目------------------
                    dispDataInsert.SYSTEM_ID = SqlInt64.Parse(dgvRow.Cells["SYSTEM_ID"].Value.ToString());//Cells[17]
                    dispDataInsert.SEQ = SqlInt32.Parse(dgvRow.Cells["SEQ"].Value.ToString()) + 1;//Cells[18]
                    //既存データの場合、以下を更新しない(既存のデータを設定)
                    dispDataInsert.CREATE_USER = dgvRow.Cells[20].Value.ToString();
                    dispDataInsert.CREATE_DATE = SqlDateTime.Parse(dgvRow.Cells[21].Value.ToString());
                    dispDataInsert.CREATE_PC = dgvRow.Cells[22].Value.ToString();

                    //■■■■■■■■■■■■■■■■■■■■■■■■■■
                    //※WHOカラム設定((WHOカラム設定が正常に動作したら削除))
                    dispDataInsert.DELETE_FLG = SqlBoolean.False;
                    //dispDataInsert.UPDATE_DATE = SqlDateTime.Parse(datatime.ToString());
                    //dispDataInsert.UPDATE_USER = UsrName;
                    //dispDataInsert.UPDATE_PC = pcname;
                    //■■■■■■■■■■■■■■■■■■■■■■■■■■


                    insertList.Add(dispDataInsert);
                }
                else
                {
                    //■■■■■■■■
                    //insert(新規登録)
                    //■■■■■■■■
                    // WHOカラム設定
                    //dataBind_juChuuKensuuEntityInsert.SetSystemProperty(dispDataInsert, false);
                    //NEW
                    var WHO = new DataBinderLogic<T_EIGYO_YOSAN>(dispDataInsert);
                    WHO.SetSystemProperty(dispDataInsert, false);

                    //画面表示項目------------------
                    dispDataInsert.NUMBERED_YEAR = SqlInt32.Parse(tourokuNendo);
                    dispDataInsert.DENPYOU_KBN_CD = this.saveDenpyouKbn;
                    dispDataInsert.BUSHO_CD = dgvRow.Cells[0].Value.ToString();
                    dispDataInsert.SHAIN_CD = dgvRow.Cells[2].Value.ToString();
                    dispDataInsert.SHAIN_NAME = dgvRow.Cells[3].Value.ToString();
                    dispDataInsert.MONTH_YOSAN_01 = Int32.Parse(dgvRow.Cells[4].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_02 = Int32.Parse(dgvRow.Cells[5].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_03 = Int32.Parse(dgvRow.Cells[6].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_04 = Int32.Parse(dgvRow.Cells[7].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_05 = Int32.Parse(dgvRow.Cells[8].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_06 = Int32.Parse(dgvRow.Cells[9].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_07 = Int32.Parse(dgvRow.Cells[10].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_08 = Int32.Parse(dgvRow.Cells[11].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_09 = Int32.Parse(dgvRow.Cells[12].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_10 = Int32.Parse(dgvRow.Cells[13].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_11 = Int32.Parse(dgvRow.Cells[14].Value.ToString().Replace(",", ""));
                    dispDataInsert.MONTH_YOSAN_12 = Int32.Parse(dgvRow.Cells[15].Value.ToString().Replace(",", ""));
                    //設定項目------------------
                    //dispDataInsert.SYSTEM_ID = SqlInt64.Parse(dgvRow.Cells["SYSTEM_ID"].Value.ToString());
                    //dispDataInsert.SEQ = SqlInt32.Parse(dgvRow.Cells["SEQ"].Value.ToString()) + 1;

                    // 伝種区分 (営業予算)
                    DBAccessor clscmn = new DBAccessor();
                    //伝種採番テーブルから引数を元に番号を取得
                    Int16 densyuKbnCd = Int16.Parse(DENSHU_KBN.EIGYOU_YOSAN.GetHashCode().ToString());
                    dispDataInsert.SYSTEM_ID = clscmn.createSystemIdWithTableLock(densyuKbnCd);
                    dispDataInsert.SEQ = 1;
                    //■■■■■■■■■■■■■■■■■■■■■■■■■■
                    //※WHOカラム設定((WHOカラム設定が正常に動作したら削除))
                    dispDataInsert.DELETE_FLG = SqlBoolean.False;
                    //dispDataInsert.UPDATE_DATE = SqlDateTime.Parse(datatime.ToString());
                    //dispDataInsert.UPDATE_USER = UsrName;
                    //dispDataInsert.UPDATE_PC = pcname;
                    //dispDataInsert.CREATE_DATE = SqlDateTime.Parse(datatime.ToString());
                    //dispDataInsert.CREATE_USER = UsrName;
                    //dispDataInsert.CREATE_PC = pcname;
                    //■■■■■■■■■■■■■■■■■■■■■■■■■■


                    insertList.Add(dispDataInsert);
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region [F12]閉じるボタンイベント

        /// <summary>
        /// [F12]閉じるボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region DGVへ取得したデータを設定する

        /// <summary>
        ///  DGVへ取得したデータを設定する
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="DataGridView">DataGridView</param>
        /// <param name="kishuMonthSortFlg">期首月に応じて並べ替えを行うか判断するフラグ</param>
        /// <returns></returns>
        CustomDataGridView SetDataGridView(DataTable table, CustomDataGridView DataGridView, bool kishuMonthSortFlg)
        {
            LogUtility.DebugMethodStart(table, DataGridView, kishuMonthSortFlg);

            try
            {
                string[] MonthSort;
                if (kishuMonthSortFlg)
                {
                    MonthSort = this.MonthArray;
                }
                else
                {
                    MonthSort = this.nenkanMonthArray;
                }

                //初期表示の空白1レコード(もしくは前回表示分)を削除
                DataGridView.Rows.Clear();

                #region テーブル作成
                this.ResultTable = new DataTable();
                ResultTable.Columns.Add("EIGYOU_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("EIGYOU_NAME", Type.GetType("System.String"));
                ResultTable.Columns.Add("BUSHO_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("BUSHO_NAME", Type.GetType("System.String"));
                ResultTable.Columns.Add("MONTH1", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH2", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH3", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH4", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH5", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH6", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH7", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH8", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH9", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH10", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH11", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MONTH12", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("GOUKEI", Type.GetType("System.Int64"));
                ResultTable.Columns.Add("SYSTEM_ID", Type.GetType("System.Int64"));
                ResultTable.Columns.Add("SEQ", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("TIME_STAMP", Type.GetType("System.Int32"));

                ResultTable.Columns.Add("CREATE_USER", Type.GetType("System.String"));
                ResultTable.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
                ResultTable.Columns.Add("CREATE_PC", Type.GetType("System.String"));
                #endregion

                //DGV設定用のDataTableに設定
                #region DataTableに設定

                foreach (DataRow r in table.Rows)
                {
                    #region 値をRowへ設定後、DataTableに追加
                    long goukei = 0;
                    DataRow newRow = this.ResultTable.NewRow();
                    newRow["EIGYOU_CD"] = r["EIGYOU_CD"];
                    newRow["EIGYOU_NAME"] = r["EIGYOU_NAME"];
                    newRow["BUSHO_CD"] = r["BUSHO_CD"];
                    newRow["BUSHO_NAME"] = r["BUSHO_NAME"];

                    newRow["MONTH1"] = r["MONTH1"];
                    newRow["MONTH2"] = r["MONTH2"];
                    newRow["MONTH3"] = r["MONTH3"];
                    newRow["MONTH4"] = r["MONTH4"];
                    newRow["MONTH5"] = r["MONTH5"];
                    newRow["MONTH6"] = r["MONTH6"];
                    newRow["MONTH7"] = r["MONTH7"];
                    newRow["MONTH8"] = r["MONTH8"];
                    newRow["MONTH9"] = r["MONTH9"];
                    newRow["MONTH10"] = r["MONTH10"];
                    newRow["MONTH11"] = r["MONTH11"];
                    newRow["MONTH12"] = r["MONTH12"];

                    // 合計を計算
                    foreach (string Month in MonthSort)
                    {
                        goukei = goukei + Convert.ToInt32(r[Month]);
                    }
                    newRow["GOUKEI"] = goukei;

                    //非表示項目
                    newRow["SYSTEM_ID"] = r["SYSTEM_ID"];
                    newRow["SEQ"] = r["SEQ"];
                    newRow["TIME_STAMP"] = r["TIME_STAMP"];
                    newRow["CREATE_USER"] = r["CREATE_USER"];
                    newRow["CREATE_DATE"] = r["CREATE_DATE"];
                    newRow["CREATE_PC"] = r["CREATE_PC"];

                    this.ResultTable.Rows.Add(newRow);
                    #endregion
                }
                #endregion

                //検索結果設定
                for (int i = 0; i < this.ResultTable.Rows.Count; i++)
                {
                    #region DGVへ設定
                    DataGridView.Rows.Add();
                    DataGridView.Rows[i].Cells["EIGYOU_CD"].Value = this.ResultTable.Rows[i]["EIGYOU_CD"];
                    DataGridView.Rows[i].Cells["EIGYOU_NAME"].Value = this.ResultTable.Rows[i]["EIGYOU_NAME"];
                    DataGridView.Rows[i].Cells["BUSHO_CD"].Value = this.ResultTable.Rows[i]["BUSHO_CD"];
                    DataGridView.Rows[i].Cells["BUSHO_NAME"].Value = this.ResultTable.Rows[i]["BUSHO_NAME"];
                    DataGridView.Rows[i].Cells["MONTH1"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[0]]);
                    DataGridView.Rows[i].Cells["MONTH2"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[1]]);
                    DataGridView.Rows[i].Cells["MONTH3"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[2]]);
                    DataGridView.Rows[i].Cells["MONTH4"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[3]]);
                    DataGridView.Rows[i].Cells["MONTH5"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[4]]);
                    DataGridView.Rows[i].Cells["MONTH6"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[5]]);
                    DataGridView.Rows[i].Cells["MONTH7"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[6]]);
                    DataGridView.Rows[i].Cells["MONTH8"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[7]]);
                    DataGridView.Rows[i].Cells["MONTH9"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[8]]);
                    DataGridView.Rows[i].Cells["MONTH10"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[9]]);
                    DataGridView.Rows[i].Cells["MONTH11"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[10]]);
                    DataGridView.Rows[i].Cells["MONTH12"].Value = this.GetValueString(this.ResultTable.Rows[i][MonthSort[11]]);
                    DataGridView.Rows[i].Cells["GOUKEI"].Value = this.GetValueString(this.ResultTable.Rows[i]["GOUKEI"]);
                    DataGridView.Rows[i].Cells["SYSTEM_ID"].Value = this.ResultTable.Rows[i]["SYSTEM_ID"];
                    DataGridView.Rows[i].Cells["SEQ"].Value = this.ResultTable.Rows[i]["SEQ"];
                    DataGridView.Rows[i].Cells["TIME_STAMP"].Value = this.ResultTable.Rows[i]["TIME_STAMP"];
                    DataGridView.Rows[i].Cells["CREATE_USER"].Value = this.ResultTable.Rows[i]["CREATE_USER"];
                    DataGridView.Rows[i].Cells["CREATE_DATE"].Value = this.ResultTable.Rows[i]["CREATE_DATE"];
                    DataGridView.Rows[i].Cells["CREATE_PC"].Value = this.ResultTable.Rows[i]["CREATE_PC"];
                    #endregion
                }

                #region DGVに設定したレコードが0件の場合、空白1レコード作成
                if (0 != DataGridView.Rows.Count)
                {
                    //表示データ有り
                    this.dispDataRecord = true;
                }
                else
                {
                    CreateNoDataRecord();
                    //表示データなし
                    this.dispDataRecord = false;
                }
                #endregion

                return DataGridView;
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 数字の場合はカンマをつけて返却
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string GetValueString(object o)
        {
            decimal tmp;
            if (!decimal.TryParse(o.ToString(), out tmp))
            {
                tmp = 0;
            }

            return tmp.ToString("#,0");
        }

        #endregion

        #region グリッドイベント
        /// <summary>
        /// 編集開始イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void customDataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // 月カラム以外は編集不可
                if (!this.dispDataRecord || (e.ColumnIndex < DEF_GRID_COL_IDX_MONTH_09
                                                     || e.ColumnIndex > DEF_GRID_COL_IDX_MONTH_08))
                {
                    e.Cancel = true;
                }

                // カンマを除く
                else
                {
                    DataGridViewCell cell = this.form.customDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string cellValue = cell.Value.ToString();
                    cellValue = cellValue.Replace(",", string.Empty);
                    cell.Value = cellValue;
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customDataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (this.dispDataRecord && e.ColumnIndex >= DEF_GRID_COL_IDX_MONTH_09
                                                && e.ColumnIndex <= DEF_GRID_COL_IDX_MONTH_08)
                {
                    DataGridViewRow row = this.form.customDataGridView1.Rows[e.RowIndex];
                    DataGridViewCell cell = row.Cells[e.ColumnIndex];

                    // カンマを加える
                    string cellValue = cell.Value == null ? "0" : cell.Value.ToString();
                    decimal tmp;
                    if (!decimal.TryParse(cellValue, out tmp))
                    {
                        tmp = 0;
                    }
                    cellValue = tmp.ToString("#,0");
                    cell.Value = cellValue;


                    // 合計を再計算
                    decimal sum = 0;
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_09]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_10]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_11]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_12]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_01]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_02]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_03]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_04]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_05]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_06]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_07]);
                    sum += this.GetCellValue(row.Cells[DEF_GRID_COL_IDX_MONTH_08]);
                    row.Cells[DEF_GRID_COL_IDX_MONTH_TOTAL].Value = sum.ToString("#,0");
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// セルの値をdecimal型で取得
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private decimal GetCellValue(DataGridViewCell cell)
        {
            decimal tmp;
            if (decimal.TryParse(cell.Value.ToString(), out tmp))
            {
                return tmp;
            }
            return 0;
        }

        #endregion

        #region デフォルトメソッド
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
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
    }
}
