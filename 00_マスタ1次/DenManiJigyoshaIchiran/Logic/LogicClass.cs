// $Id: LogicClass.cs 46576 2015-04-06 04:18:28Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DenManiJigyoushaIchiran.APP;
using r_framework.APP.Base;
using r_framework.APP.Base.IchiranHeader;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;

namespace DenManiJigyoushaIchiran.Logic
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
        private readonly string ButtonInfoXmlPath = "DenManiJigyoushaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// SQLファイル
        /// </summary>
        private readonly string GET_DATA_BY_CD_SQL = "DenManiJigyoushaIchiran.Sql.GetDataByCdSql.sql";

        /// <summary>
        /// 画面連携に使用するキー取得項目名1
        /// </summary>
        internal readonly string KEY_ID1 = "HIDDEN_EDI_MEMBER_ID";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private DenManiJigyoushaIchiranForm form;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        private IchiranHeaderForm2 headerForm;

        /// <summary>
        /// 全コントロール一覧
        /// </summary>
        private Control[] allControl;

        private M_SYS_INFO sysinfoEntity;

        /// <summary>
        /// 電子事業者のDao
        /// </summary>
        private IM_DENSHI_JIGYOUSHADao daoJigyousha;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(DenManiJigyoushaIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.headerForm = (IchiranHeaderForm2)((IchiranBaseForm)targetForm.ParentForm).headerForm;
            this.daoJigyousha = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();
                if (!this.form.EventSetFlg)
                {
                    // イベントの初期化処理
                    this.EventInit();
                    this.form.EventSetFlg = true;
                }
                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;                             //行の追加オプション(false)
                this.form.customDataGridView1.Size = new Size(997, 268);

                //システム設定情報読み込み
                this.GetSysInfo();
                // ヘッダーの初期化
                this.InitHeaderArea();
                // コントロールの非活性化

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
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
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);          //並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);        //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる
            parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);             //パターン一覧画面へ遷移
            parentForm.bt_process2.Click += new EventHandler(this.bt_process2_Click);             //検索条件設定画面へ遷移

            //明細ダブルクリック時のイベント
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DetailCellDoubleClick);
            //明細エンター時のイベント
            this.form.customDataGridView1.KeyDown += new KeyEventHandler(this.DetailKeyDown);

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

            CSVExport csvLogic = new CSVExport();
            DENSHU_KBN id = this.form.DenshuKbn;
            csvLogic.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, id.ToTitleString(), this.form);

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

            // 条件初期化
            this.form.JIGYOUSHA_KBN.Text = "1";
            this.form.JIGYOUSHA_NAME.Text = string.Empty;
            this.form.JIGYOUSHA_ADDRESS.Text = string.Empty;
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            this.form.customSortHeader1.ClearCustomSortSetting();

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

            try
            {
                // パターン未登録の場合検索処理を行わない
                if (this.form.PatternNo == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }
                //読込データ件数を取得
                this.headerForm.ReadDataNumber.Text = this.Search().ToString();

                if (this.form.customDataGridView1 != null)
                {
                    this.headerForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headerForm.ReadDataNumber.Text = "0";
                }

                if (this.headerForm.ReadDataNumber.Text == "0")
                {
                    MessageBoxUtility.MessageBoxShow("C001");
                }

                this.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func8_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.form.customDataGridView1 != null)
            {
                this.headerForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headerForm.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (IchiranBaseForm)this.form.Parent;
            if (parentForm != null)
            {
                parentForm.Close();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
        }

        #endregion

        #region プロセスボタン押下処理（※処理未実装）
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var sysID = this.form.OpenPatternIchiran();

                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_process1_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
            if (string.IsNullOrWhiteSpace(DenManiJigyoushaIchiran.Properties.Settings.Default.JIGYOUSHA_KBN_TEXT))
            {
                this.form.JIGYOUSHA_KBN.Text = "1";
            }
            else
            {
                this.form.JIGYOUSHA_KBN.Text = DenManiJigyoushaIchiran.Properties.Settings.Default.JIGYOUSHA_KBN_TEXT;
            }

            //アラート件数の初期値セット
            if (!sysinfoEntity.ICHIRAN_ALERT_KENSUU.IsNull)
            {
                this.headerForm.alertNumber.Text = this.sysinfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
            }

            //アラートの保存データがあればそちらを表示
            if (DenManiJigyoushaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU != "")
            {
                this.headerForm.alertNumber.Text = DenManiJigyoushaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU;
            }

            // 検索条件の初期化
            this.form.JIGYOUSHA_NAME.Text = DenManiJigyoushaIchiran.Properties.Settings.Default.JIGYOUSHA_NAME_TEXT;
            this.form.JIGYOUSHA_ADDRESS.Text = DenManiJigyoushaIchiran.Properties.Settings.Default.JIGYOUSHA_ADDRESS_TEXT;
        }

        #endregion

        #region 事業者入力画面起動処理

        /// <summary>
        /// 電子事業者入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        private void OpenWindow(WINDOW_TYPE windowType, bool newFlg = false)
        {
            LogUtility.DebugMethodStart(windowType, newFlg);

            // 引数へのオブジェクトを作成する
            // 新規の場合は引数なし、ただし参照の場合は引数あり
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG && newFlg)
            {
                r_framework.FormManager.FormManager.OpenFormWithAuth("M309", WINDOW_TYPE.NEW_WINDOW_FLAG, windowType);
            }
            else
            {
                // 表示されている行の取引先CDを取得する
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (row == null)
                {
                    MessageBoxUtility.MessageBoxShow("E051", "対象データ");
                    LogUtility.DebugMethodEnd(windowType, newFlg);
                    return;
                }
                string cd1 = row.Cells[this.KEY_ID1].Value.ToString();

                // データ削除チェックを行う
                //if (windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                //{
                //    M_DENSHI_JIGYOUSHA cond = new M_DENSHI_JIGYOUSHA();
                //    cond.EDI_MEMBER_ID = cd1;
                //    DataTable data = this.daoJigyousha.GetDataBySqlFile(GET_DATA_BY_CD_SQL, cond);
                //    if ((bool)data.Rows[0]["DELETE_FLG"])
                //    {
                //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //        msgLogic.MessageBoxShow("E026", "コード");
                //        LogUtility.DebugMethodEnd();
                //        return;
                //    }
                //}

                // 権限チェック
                // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                    !r_framework.Authority.Manager.CheckAuthority("M309", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    if(!r_framework.Authority.Manager.CheckAuthority("M309", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        MessageBoxUtility.MessageBoxShow("E158", "修正");
                        return;
                    }

                    windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }

                r_framework.FormManager.FormManager.OpenFormWithAuth("M309", windowType, windowType, cd1);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        #endregion

        #region IBuisinessLogicで必須実装(未使用)

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

        #region アラート件数取得処理

        /// <summary>
        /// アラート件数取得処理
        /// </summary>
        /// <returns></returns>
        public int GetAlertCount()
        {
            int result = 0;
            int.TryParse(this.headerForm.alertNumber.Text,System.Globalization.NumberStyles.AllowThousands,null, out result);
            return result;
        }

        #endregion

        #region 検索条件作成

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            this.form.Table = null;

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                // 検索文字列の作成
                var sql = this.MakeSearchCondition();
                this.form.Table = this.daoJigyousha.GetDateForStringSql(sql);
            }

            this.form.ShowData();

            return this.form.Table != null ? this.form.Table.Rows.Count : 0;
        }

        /// <summary>
        /// 検索条件作成
        /// </summary>
        /// <returns></returns>
        private string MakeSearchCondition()
        {
            LogUtility.DebugMethodStart();

            var res = string.Empty;

            try
            {
                var sb = new StringBuilder();

                #region SELECT句

                sb.Append("SELECT ");
                sb.Append(this.form.SelectQuery);
                sb.AppendFormat(" , M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID AS {0} ", this.KEY_ID1);

                #endregion

                #region FROM句

                // 電子事業者
                sb.Append(" FROM M_DENSHI_JIGYOUSHA ");


                // パターンから作成したJOIN句
                sb.Append(this.form.JoinQuery);

                #endregion

                #region WHERE句

                var strTemp = string.Empty;

                sb.Append(" WHERE 1=1 ");

                // 固定検索項目
                switch (this.form.JIGYOUSHA_KBN.Text)
                {
                    case "1":
                        strTemp = "M_DENSHI_JIGYOUSHA.HST_KBN = 1";
                        break;
                    case "2":
                        strTemp = "M_DENSHI_JIGYOUSHA.UPN_KBN = 1";
                        break;
                    case "3":
                        strTemp = "M_DENSHI_JIGYOUSHA.SBN_KBN = 1";
                        break;
                }
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND {0} ", strTemp);
                }

                // 事業者名
                strTemp = SqlCreateUtility.CounterplanEscapeSequence(this.form.JIGYOUSHA_NAME.Text);
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_DENSHI_JIGYOUSHA.JIGYOUSHA_NAME LIKE '%{0}%' ", strTemp);
                }

                // 住所
                strTemp = SqlCreateUtility.CounterplanEscapeSequence(this.form.JIGYOUSHA_ADDRESS.Text);
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.Append(" AND (")
                        .Append("M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS1").Append(" + ")
                        .Append("M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS2").Append(" + ")
                        .Append("M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS3").Append(" + ")
                        .Append("M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS4").Append(")")
                        .AppendFormat(" LIKE '%{0}%' ", strTemp);
                }

                #endregion

                #region ORDER BY句

                if (!string.IsNullOrWhiteSpace(this.form.OrderByQuery))
                {
                    sb.AppendFormat(" ORDER BY {0} ", this.form.OrderByQuery);
                }

                #endregion

                res = sb.ToString();

                return res;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res);
            }
        }

        /// <summary>
        /// 検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        public string GetSearchString()
        {
            var result = new StringBuilder(256);
            string strTemp = string.Empty;

            // 固定検索項目
            switch (this.form.JIGYOUSHA_KBN.Text)
            {
                case "1":
                    strTemp = "M_DENSHI_JIGYOUSHA.HST_KBN = 1";
                    break;
                case "2":
                    strTemp = "M_DENSHI_JIGYOUSHA.UPN_KBN = 1";
                    break;
                case "3":
                    strTemp = "M_DENSHI_JIGYOUSHA.SBN_KBN = 1";
                    break;
            }
            if (!string.IsNullOrWhiteSpace(result.ToString()))
            {
                result.Append(" AND ");
            }
            result.Append(strTemp);

            // 事業者名
            strTemp = this.form.JIGYOUSHA_NAME.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_DENSHI_JIGYOUSHA.JIGYOUSHA_NAME LIKE '%{0}%'", strTemp);
            }

            // 住所
            strTemp = this.form.JIGYOUSHA_ADDRESS.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" (")
                    .Append("M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS1").Append(" + ")
                    .Append("M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS2").Append(" + ")
                    .Append("M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS3").Append(" + ")
                    .Append("M_DENSHI_JIGYOUSHA.JIGYOUSHA_ADDRESS4").Append(")")
                    .AppendFormat(" LIKE '%{0}%'", strTemp);
            }

            return result.ToString();
        }

        #endregion

        /// <summary>
        /// 表示条件を次回呼出時のデフォルト値として保存します
        /// </summary>
        public bool SaveHyoujiJoukenDefault()
        {
            LogUtility.DebugMethodStart();

            try
            {
                DenManiJigyoushaIchiran.Properties.Settings.Default.JIGYOUSHA_KBN_TEXT = this.form.JIGYOUSHA_KBN.Text;
                DenManiJigyoushaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                DenManiJigyoushaIchiran.Properties.Settings.Default.JIGYOUSHA_NAME_TEXT = this.form.JIGYOUSHA_NAME.Text;
                DenManiJigyoushaIchiran.Properties.Settings.Default.JIGYOUSHA_ADDRESS_TEXT = this.form.JIGYOUSHA_ADDRESS.Text;
                DenManiJigyoushaIchiran.Properties.Settings.Default.Save();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}
