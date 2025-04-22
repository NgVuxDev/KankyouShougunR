using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Allocation.Sharyoukyuudounyuryoku
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
        private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.Sharyoukyuudounyuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// 検索条件
        /// </summary>
        public SearchDTOClass SearchString { get; set; }

        /// <summary>
        /// 車輌DAO
        /// </summary>
        private M_SHARYOUDao sharyouDao;

        /// <summary>
        /// 車輌休動DAO
        /// </summary>
        private M_WORK_CLOSED_SHARYOUDao kyudouDao;

        // 20141008 koukouei 休動管理機能 start

        /// <summary>
        /// 収集受付DAO
        /// </summary>
        private T_UKETSUKE_SS_ENTRYDao uketsukeSsDao;

        /// <summary>
        /// 出荷受付DAO
        /// </summary>
        private T_UKETSUKE_SK_ENTRYDao uketsukeSkDao;

        /// <summary>
        /// 持込受付DAO
        /// </summary>
        private T_UKETSUKE_MK_ENTRYDao uketsukeMkDao;

        /// <summary>
        /// 定期配車入力DAO
        /// </summary>
        private T_TEIKI_HAISHA_ENTRYDao teikiHaishaDao;

        /// <summary>
        /// 定期配車実績入力DAO
        /// </summary>
        private T_TEIKI_JISSEKI_ENTRYDao teikiJissekiDao;

        /// <summary>
        /// 受入入力DAO
        /// </summary>
        private T_UKEIRE_ENTRYDao ukeireDao;

        /// <summary>
        /// 出荷入力DAO
        /// </summary>
        private T_SHUKKA_ENTRYDao shukkaDao;

        /// <summary>
        /// 売上支払入力DAO
        /// </summary>
        private T_UR_SH_ENTRYDao urShDao;

        // 20141008 koukouei 休動管理機能 end

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;

        ///// <summary>
        ///// 車輌マスタ検索結果
        ///// </summary>
        //private DataTable SearchSharyouResult;

        /// <summary>
        /// 車輌休動マスタ検索結果
        /// </summary>
        private DataTable SearchSharyoukyudouResult;

        /// <summary>
        /// カレンダ表示されている日付
        /// </summary>
        private DateTime calendarDate;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 車輌DataGridView
        /// </summary>
        private CustomDataGridView leftGridView;

        /// <summary>
        /// 車輌休動DataGridView(01～16)
        /// </summary>
        private CustomDataGridView rightGridView1;

        /// <summary>
        /// 車輌休動DataGridView(17～月末)
        /// </summary>
        private CustomDataGridView rightGridView2;

        /// <summary>
        /// 新規リスト
        /// </summary>
        private List<M_WORK_CLOSED_SHARYOU> insertList;

        /// <summary>
        /// 更新リスト
        /// </summary>
        private List<M_WORK_CLOSED_SHARYOU> updateList;

        /// <summary>
        /// 削除リスト
        /// </summary>
        private List<M_WORK_CLOSED_SHARYOU> deleteList;

        // 20141112 koukouei 休動管理機能 start
        /// <summary>
        /// 検索フラグ
        /// </summary>
        private bool searchFlg = false;

        /// <summary>
        /// 変更前行
        /// </summary>
        internal int rowIndex;

        // 20141112 koukouei 休動管理機能 end

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);
                // メインフォーム
                this.form = targetForm;
                // メッセージ表示オブジェクト
                this.msgLogic = new MessageBoxShowLogic();
                // DTO
                this.dto = new DTOClass();
                // 車輌DAO
                this.sharyouDao = DaoInitUtility.GetComponent<M_SHARYOUDao>();
                // 車輌休動DAO
                this.kyudouDao = DaoInitUtility.GetComponent<M_WORK_CLOSED_SHARYOUDao>();
                // 20141008 koukouei 休動管理機能 start
                // 収集受付DAO
                this.uketsukeSsDao = DaoInitUtility.GetComponent<T_UKETSUKE_SS_ENTRYDao>();
                // 出荷受付DAO
                this.uketsukeSkDao = DaoInitUtility.GetComponent<T_UKETSUKE_SK_ENTRYDao>();
                // 持込受付DAO
                this.uketsukeMkDao = DaoInitUtility.GetComponent<T_UKETSUKE_MK_ENTRYDao>();
                // 定期配車入力DAO
                this.teikiHaishaDao = DaoInitUtility.GetComponent<T_TEIKI_HAISHA_ENTRYDao>();
                // 定期配車実績入力DAO
                this.teikiJissekiDao = DaoInitUtility.GetComponent<T_TEIKI_JISSEKI_ENTRYDao>();
                // 受入入力DAO
                this.ukeireDao = DaoInitUtility.GetComponent<T_UKEIRE_ENTRYDao>();
                // 出荷入力DAO
                this.shukkaDao = DaoInitUtility.GetComponent<T_SHUKKA_ENTRYDao>();
                // 売上支払入力DAO
                this.urShDao = DaoInitUtility.GetComponent<T_UR_SH_ENTRYDao>();
                // 20141008 koukouei 休動管理機能 end
                // DataGridViewコントロール
                this.leftGridView = this.form.dgvSharyou;
                this.rightGridView1 = this.form.dgvSharyouKyudou1;
                this.rightGridView2 = this.form.dgvSharyouKyudou2;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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

        #region 画面初期化

        #region 画面情報の初期化

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                // 親フォームオブジェクト取得
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダフォームオブジェクト取得
                this.headerForm = (UIHeader)this.parentForm.headerForm;

                // 画面初期表示設定
                this.initializeScreen();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 車輌休動DataGridView初期化処理
                this.dataGridViewInit();

                // 権限チェック
                if (!Manager.CheckAuthority("G208", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        #endregion

        #region 初期表示

        /// <summary>
        /// 画面初期表示設定
        /// </summary>
        private void initializeScreen()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件をクリア
                this.form.CONDITION_ITEM.Text = string.Empty;
                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                // カレンダ日付を設定する
                this.form.monthCalendar.TodayDate = this.parentForm.sysDate;
                this.calendarDate = this.form.monthCalendar.TodayDate;
                // ヘッダフォーム年月日を設定する
                this.headerForm.txt_nengappi.Text = this.form.monthCalendar.TodayDate.ToString("yyyy年MM月");
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        public void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        #endregion

        #region

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.checkBox1.Enabled = false;
            this.form.dgvSharyouKyudou1.ReadOnly = true;
            this.form.checkBox2.Enabled = false;
            this.form.dgvSharyouKyudou2.ReadOnly = true;

            // FunctionButton
            this.parentForm.bt_func4.Enabled = false;
            this.parentForm.bt_func9.Enabled = false;
        }

        #endregion

        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 「Functionボタン」のイベント生成
                // 前月ボタン
                parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);
                // 次月ボタン
                parentForm.bt_func2.Click += new EventHandler(bt_func2_Click);
                // 削除ボタン
                parentForm.bt_func4.Click += new EventHandler(bt_func4_Click);
                // 条件クリアボタン
                parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);
                // 検索ボタン
                parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);
                // 登録ボタン
                parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);
                // 取消ボタン
                parentForm.bt_func11.Click += new EventHandler(bt_func11_Click);
                // 閉じるボタン
                parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

                this.parentForm.ProcessButtonPanel.Visible = false;
                parentForm.bt_process1.Visible = false;
                parentForm.bt_process2.Visible = false;
                parentForm.bt_process3.Visible = false;
                parentForm.bt_process4.Visible = false;
                parentForm.bt_process5.Visible = false;
                parentForm.txb_process.Visible = false;
                parentForm.lb_process.Visible = false;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 業務処理

        #region 車輌休動DataGridViewに空白を設定処理

        /// <summary>
        /// 車輌休動DataGridViewに空白を設定する
        /// </summary>
        private void dataGridViewInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.dgvSharyouKyudou1.CurrentCellChanged -= new System.EventHandler(this.form.dgvSharyouKyudou1_CurrentCellChanged);
                this.form.dgvSharyouKyudou2.CurrentCellChanged -= new System.EventHandler(this.form.dgvSharyouKyudou2_CurrentCellChanged);
                // 20141112 koukouei 休動管理機能 start
                this.form.dgvSharyouKyudou1.CellValueChanged -= this.form.dgvSharyouKyudou1_CellValueChanged;
                this.form.dgvSharyouKyudou2.CellValueChanged -= this.form.dgvSharyouKyudou2_CellValueChanged;
                // 20141112 koukouei 休動管理機能 end

                // 年を取得
                int year = this.form.monthCalendar.SelectionStart.Year;
                // 月を取得
                int month = this.form.monthCalendar.SelectionStart.Month;
                // 当月の日数
                int dayCount = DateTime.DaysInMonth(year, month);

                // 明細クリア
                this.rightGridView1.Rows.Clear();
                this.rightGridView2.Rows.Clear();

                // 明細行を追加
                this.rightGridView1.Rows.Add(16);
                this.rightGridView2.Rows.Add(dayCount - 16);

                // dataGird1を設定する
                for (int i = 0; i < 16; i++)
                {
                    // dataGirdの日付の日を取得する
                    String day = (i + 1).ToString("D2");
                    // 日付
                    this.rightGridView1["CLOSED_DATE", i].Value = day;
                    // 曜日
                    DateTime date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                    this.rightGridView1["WEEK", i].Value = ("日月火水木金土").Substring(int.Parse(date.Date.DayOfWeek.ToString("d")), 1);
                    // チェックボックスを設定する
                    this.rightGridView1["CLOSED_DATE_FLG", i].Value = false;
                    // 備考
                    this.rightGridView1["BIKOU", i].Value = string.Empty;
                }
                // dataGird2を設定する
                for (int i = 0; i < dayCount - 16; i++)
                {
                    // dataGirdの日付の日を取得する
                    String day = (i + 17).ToString("D2");
                    // 日付
                    this.rightGridView2["CLOSED_DATE2", i].Value = day;
                    // 曜日
                    DateTime date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                    this.rightGridView2["WEEK2", i].Value = ("日月火水木金土").Substring(int.Parse(date.Date.DayOfWeek.ToString("d")), 1);
                    // チェックボックスを設定する
                    this.rightGridView2["CLOSED_DATE_FLG2", i].Value = false;
                    // 備考
                    this.rightGridView2["BIKOU2", i].Value = string.Empty;
                }
                this.form.dgvSharyouKyudou1.CurrentCellChanged += new System.EventHandler(this.form.dgvSharyouKyudou1_CurrentCellChanged);
                this.form.dgvSharyouKyudou2.CurrentCellChanged += new System.EventHandler(this.form.dgvSharyouKyudou2_CurrentCellChanged);
                // 20141112 koukouei 休動管理機能 start
                this.form.dgvSharyouKyudou1.CellValueChanged += this.form.dgvSharyouKyudou1_CellValueChanged;
                this.form.dgvSharyouKyudou2.CellValueChanged += this.form.dgvSharyouKyudou2_CellValueChanged;
                // 20141112 koukouei 休動管理機能 end

                // 赤枠非表示する
                this.form.clearCurrentCell();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 20141112 koukouei 休動管理機能 start
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool catchErr = false;
                bool henkouFlg = workCloseHenkouCheck(out catchErr);
                if (catchErr) { return; }
                if (searchFlg && henkouFlg && msgLogic.MessageBoxShowConfirm("選択した内容が破棄されますがよろしいですか。") != DialogResult.Yes)
                {
                    return;
                }
                // 20141112 koukouei 休動管理機能 end

                this.form.dgvSharyou.CurrentCellChanged -= new System.EventHandler(this.form.dgvSharyou_CurrentCellChanged);

                // 検索条件以外項目をクリアする
                this.form.TXT_SHARYOU_CD.Text = string.Empty;
                this.form.TXT_SHARYOU_NAME.Text = string.Empty;
                this.form.TXT_GYOUSHA_CD.Text = string.Empty;
                // 20141008 koukouei 休動管理機能 start
                this.form.TXT_UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.TXT_UNPAN_GYOUSHA_NAME.Text = string.Empty;
                // 20141008 koukouei 休動管理機能 end
                // 20141120 koukouei 休動管理機能 start
                this.form.TXT_SHASYU_CD.Text = string.Empty;
                this.form.TXT_SHASYU_NAME.Text = string.Empty;
                // 20141120 koukouei 休動管理機能 end
                this.form.dgvSharyou.Rows.Clear();
                this.dataGridViewInit();

                //検索条件設定
                SetSearchString();

                // 検索条件によって、車輌マストテーブルを検索
                DataTable searchData = sharyouDao.GetSharyouData(this.SearchString);

                //var searchData = this.SearchSharyouResult;
                //// 業者CD未設定については、業者ﾏｽﾀの自社区分チェックは行わない
                //for (int j = 0; j < this.SearchSharyouResult.Rows.Count; j++)
                //{
                //    String gyoushaCd = this.SearchSharyouResult.Rows[j]["GYOUSHA_CD"].ToString().Trim();
                //    bool jishaBkn = bool.Parse(this.SearchSharyouResult.Rows[j]["JISHA_KBN"].ToString());

                //    if (!gyoushaCd.Equals(string.Empty) && !jishaBkn)
                //    {
                //        searchData.Rows[j].Delete();
                //    }
                //}
                //searchData.AcceptChanges();

                //　取得したデータが存在しない場合
                if (searchData.Rows.Count == 0)
                {
                    // メッセージ通知
                    msgLogic.MessageBoxShow("C001");
                    // 20141112 koukouei 休動管理機能 start
                    searchFlg = false;
                    // 20141112 koukouei 休動管理機能 end
                    return;
                }

                this.leftGridView.IsBrowsePurpose = false;
                // 明細クリア
                this.leftGridView.Rows.Clear();
                // 明細行を追加
                this.leftGridView.Rows.Add(searchData.Rows.Count);
                searchData.BeginLoadData();

                // 検索結果設定
                for (int i = 0; i < searchData.Rows.Count; i++)
                {
                    // 車輌CD
                    this.leftGridView["SHARYOU_CD", i].Value = searchData.Rows[i]["SHARYOU_CD"].ToString();
                    // 車輌略称名
                    if (DBNull.Value.Equals(searchData.Rows[i]["SHARYOU_NAME_RYAKU"]))
                    {
                        this.leftGridView["SHARYOU_NAME_RYAKU", i].Value = string.Empty;
                    }
                    else
                    {
                        this.leftGridView["SHARYOU_NAME_RYAKU", i].Value = searchData.Rows[i]["SHARYOU_NAME_RYAKU"].ToString();
                    }
                    // 車種CD
                    this.leftGridView["SHASYU_CD", i].Value = searchData.Rows[i]["SHASYU_CD"].ToString();
                    // 車種略称名
                    if (DBNull.Value.Equals(searchData.Rows[i]["SHASHU_NAME_RYAKU"]))
                    {
                        this.leftGridView["SHASHU_NAME_RYAKU", i].Value = string.Empty;
                    }
                    else
                    {
                        this.leftGridView["SHASHU_NAME_RYAKU", i].Value = searchData.Rows[i]["SHASHU_NAME_RYAKU"].ToString();
                    }
                    // 業者CD
                    this.leftGridView["GYOUSHA_CD", i].Value = searchData.Rows[i]["GYOUSHA_CD"].ToString();
                    // 20141008 koukouei 休動管理機能 start
                    // 業者名
                    if (DBNull.Value.Equals(searchData.Rows[i]["GYOUSHA_NAME_RYAKU"]))
                    {
                        this.leftGridView["GYOUSHA_NAME_RYAKU", i].Value = string.Empty;
                    }
                    else
                    {
                        this.leftGridView["GYOUSHA_NAME_RYAKU", i].Value = searchData.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                    }
                    // 20141008 koukouei 休動管理機能 end
                }
                this.leftGridView.IsBrowsePurpose = true;

                // 検索した後に、車輌１行目のデータによって、車輌休動のデータをセットする。
                // 業者CDを設定する
                this.form.TXT_GYOUSHA_CD.Text = this.leftGridView["GYOUSHA_CD", 0].Value.ToString();
                // 20141008 koukouei 休動管理機能 start
                // 運搬業者CDを設定する
                this.form.TXT_UNPAN_GYOUSHA_CD.Text = Convert.ToString(this.leftGridView["GYOUSHA_CD", 0].Value);
                // 運搬業者名を設定する
                this.form.TXT_UNPAN_GYOUSHA_NAME.Text = Convert.ToString(this.leftGridView["GYOUSHA_NAME_RYAKU", 0].Value);
                // 20141008 koukouei 休動管理機能 end
                // 車輌CDを設定する
                this.form.TXT_SHARYOU_CD.Text = this.leftGridView["SHARYOU_CD", 0].Value.ToString();
                // 車輌名を設定する
                this.form.TXT_SHARYOU_NAME.Text = this.leftGridView["SHARYOU_NAME_RYAKU", 0].Value.ToString();
                // 20141120 koukouei 休動管理機能 start
                // 車種CDを設定する
                this.form.TXT_SHASYU_CD.Text = this.leftGridView["SHASYU_CD", 0].Value.ToString();
                // 車種名を設定する
                this.form.TXT_SHASYU_NAME.Text = this.leftGridView["SHASHU_NAME_RYAKU", 0].Value.ToString();
                // 20141120 koukouei 休動管理機能 end

                // 検索日付を取得する
                String searchDate = this.form.monthCalendar.SelectionStart.ToString("yyyy/MM");

                if (!this.SearchWorkClosedSharyouData(this.form.TXT_GYOUSHA_CD.Text, this.form.TXT_SHARYOU_CD.Text, searchDate)) { return; }

                this.form.dgvSharyou.CurrentCellChanged += new System.EventHandler(this.form.dgvSharyou_CurrentCellChanged);

                // 20141112 koukouei 休動管理機能 start
                this.form.checkBox1.CheckedChanged -= this.form.checkBox1_CheckedChanged;
                this.form.checkBox1.Checked = false;
                this.form.checkBox1.CheckedChanged += this.form.checkBox1_CheckedChanged;
                searchFlg = true;
                rowIndex = 0;
                // 20141112 koukouei 休動管理機能 end
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            LogUtility.DebugMethodStart();

            SearchDTOClass entity = new SearchDTOClass();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    //検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }

            this.SearchString = entity;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 車輌に値を設定処理

        /// <summary>
        /// 車輌CDと車輌名に値を設定処理
        /// </summary>
        /// <returns></returns>
        public void dgvSharyou_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 当前行目を取得する
                int i = e.RowIndex;

                // 車輌CDを設定する
                this.form.TXT_SHARYOU_CD.Text = this.leftGridView["SHARYOU_CD", i].Value.ToString();

                // 車輌名を設定する
                this.form.TXT_SHARYOU_NAME.Text = this.leftGridView["SHARYOU_NAME_RYAKU", i].Value.ToString();

                // 20141008 koukouei 休動管理機能 start
                // 運搬業者CDを設定する
                this.form.TXT_UNPAN_GYOUSHA_CD.Text = Convert.ToString(this.leftGridView["GYOUSHA_CD", i].Value);

                // 運搬業者名を設定する
                this.form.TXT_UNPAN_GYOUSHA_NAME.Text = Convert.ToString(this.leftGridView["GYOUSHA_NAME_RYAKU", i].Value);
                // 20141008 koukouei 休動管理機能 end

                // 20141120 koukouei 休動管理機能 start
                // 車種CDを設定する
                this.form.TXT_SHASYU_CD.Text = this.leftGridView["SHASYU_CD", i].Value.ToString();

                // 車種名を設定する
                this.form.TXT_SHASYU_NAME.Text = this.leftGridView["SHASHU_NAME_RYAKU", i].Value.ToString();
                // 20141120 koukouei 休動管理機能 end

                // 隠し業者CDを設定する
                this.form.TXT_GYOUSHA_CD.Text = this.leftGridView["GYOUSHA_CD", i].Value.ToString();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 車輌休動データを取得処理

        /// <summary>
        /// 車輌休動データを取得する
        /// </summary>
        /// <returns></returns>
        public bool SearchWorkClosedSharyouData(String gyoushaCd, String sharyouCd, String searchDate)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, sharyouCd, searchDate);

                // 検索条件によって、車輌休動マスタテーブルを検索
                this.SearchSharyoukyudouResult = kyudouDao.GetSharyoukyudouData(gyoushaCd, sharyouCd, searchDate);

                // 取得できない場合、車輌休動DataGridViewに空白を設定する
                if (this.SearchSharyoukyudouResult.Rows.Count == 0)
                {
                    dataGridViewInit();
                }
                // 取得できる場合、車輌休動DataGridViewに取得したデータを設定する
                else
                {
                    setDataGridView();
                }

                // 20141112 koukouei 休動管理機能 start
                this.form.checkBox1.Checked = false;
                this.form.checkBox2.Checked = false;
                // 20141112 koukouei 休動管理機能 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchWorkClosedSharyouData", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 車輌休動DataGridViewを設定処理

        /// <summary>
        /// 車輌休動DataGridViewを設定する
        /// </summary>
        /// <returns></returns>
        private void setDataGridView()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.form.dgvSharyouKyudou1.CurrentCellChanged -= new System.EventHandler(this.form.dgvSharyouKyudou1_CurrentCellChanged);
                this.form.dgvSharyouKyudou2.CurrentCellChanged -= new System.EventHandler(this.form.dgvSharyouKyudou2_CurrentCellChanged);
                // 20141112 koukouei 休動管理機能 start
                this.form.dgvSharyouKyudou1.CellValueChanged -= this.form.dgvSharyouKyudou1_CellValueChanged;
                this.form.dgvSharyouKyudou2.CellValueChanged -= this.form.dgvSharyouKyudou2_CellValueChanged;
                // 20141112 koukouei 休動管理機能 end
                // 年を取得
                int year = this.form.monthCalendar.SelectionStart.Year;
                // 月を取得
                int month = this.form.monthCalendar.SelectionStart.Month;
                // 当月の日数
                int dayCount = DateTime.DaysInMonth(year, month);

                // 明細クリア
                this.rightGridView1.Rows.Clear();
                this.rightGridView2.Rows.Clear();

                // 明細行を追加
                this.rightGridView1.Rows.Add(16);
                this.rightGridView2.Rows.Add(dayCount - 16);

                this.SearchSharyoukyudouResult.BeginLoadData();

                // dataGird1検索結果設定
                for (int i = 0; i < 16; i++)
                {
                    // dataGirdの日付の日を取得する
                    String day = (i + 1).ToString("D2");
                    // 日付
                    this.rightGridView1["CLOSED_DATE", i].Value = day;
                    // 曜日
                    DateTime date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                    this.rightGridView1["WEEK", i].Value = ("日月火水木金土").Substring(int.Parse(date.Date.DayOfWeek.ToString("d")), 1);

                    for (int j = 0; j < this.SearchSharyoukyudouResult.Rows.Count; j++)
                    {
                        // 休動日
                        String closedDate = DateTime.Parse(this.SearchSharyoukyudouResult.Rows[j]["CLOSED_DATE"].ToString()).ToShortDateString();
                        // 休動日の日を取得する
                        String closedDateDay = closedDate.Substring(8, 2);

                        if (closedDateDay.Equals(day))
                        {
                            // チェックボックスを設定する
                            this.rightGridView1["CLOSED_DATE_FLG", i].Value = true;
                            // 備考
                            this.rightGridView1["BIKOU", i].Value = this.SearchSharyoukyudouResult.Rows[j]["BIKOU"].ToString();
                            // タイムスタンプ
                            this.rightGridView1["TIME_STAMP", i].Value = this.SearchSharyoukyudouResult.Rows[j]["TIME_STAMP"];
                            break;
                        }
                        else
                        {
                            // チェックボックスを設定する
                            this.rightGridView1["CLOSED_DATE_FLG", i].Value = false;
                            // 備考
                            this.rightGridView1["BIKOU", i].Value = string.Empty;
                        }
                    }
                }
                // dataGird2検索結果設定
                for (int i = 0; i < dayCount - 16; i++)
                {
                    // dataGird2の日付の日を取得する
                    String day = (i + 17).ToString("D2");
                    // 日付
                    this.rightGridView2["CLOSED_DATE2", i].Value = day;
                    // 曜日
                    DateTime date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                    this.rightGridView2["WEEK2", i].Value = ("日月火水木金土").Substring(int.Parse(date.Date.DayOfWeek.ToString("d")), 1);

                    for (int j = 0; j < this.SearchSharyoukyudouResult.Rows.Count; j++)
                    {
                        // 休動日
                        String closedDate = DateTime.Parse(this.SearchSharyoukyudouResult.Rows[j]["CLOSED_DATE"].ToString()).ToShortDateString();
                        // 休動日の日を取得する
                        String closedDateDay = closedDate.Substring(8, 2);

                        if (closedDateDay.Equals(day))
                        {
                            // チェックボックスを設定する
                            this.rightGridView2["CLOSED_DATE_FLG2", i].Value = true;
                            // 備考
                            this.rightGridView2["BIKOU2", i].Value = this.SearchSharyoukyudouResult.Rows[j]["BIKOU"].ToString();
                            // TIME_STAMP
                            this.rightGridView2["TIME_STAMP2", i].Value = this.SearchSharyoukyudouResult.Rows[j]["TIME_STAMP"];
                            break;
                        }
                        else
                        {
                            // チェックボックスを設定する
                            this.rightGridView2["CLOSED_DATE_FLG2", i].Value = false;
                            // 備考
                            this.rightGridView2["BIKOU2", i].Value = string.Empty;
                        }
                    }
                }

                this.form.dgvSharyouKyudou1.CurrentCellChanged += new System.EventHandler(this.form.dgvSharyouKyudou1_CurrentCellChanged);
                this.form.dgvSharyouKyudou2.CurrentCellChanged += new System.EventHandler(this.form.dgvSharyouKyudou2_CurrentCellChanged);
                // 20141112 koukouei 休動管理機能 start
                this.form.dgvSharyouKyudou1.CellValueChanged += this.form.dgvSharyouKyudou1_CellValueChanged;
                this.form.dgvSharyouKyudou2.CellValueChanged += this.form.dgvSharyouKyudou2_CellValueChanged;
                // 20141112 koukouei 休動管理機能 end
                // 赤枠非表示する
                this.form.clearCurrentCell();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region カレンダ日付変更処理

        /// <summary>
        /// カレンダ日付変更処理
        /// </summary>
        /// <returns></returns>
        public bool calendarDateChanged(bool calendarFlg)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(calendarFlg);

                if (calendarFlg)
                {
                    // 変更前の年月を取得する
                    String nowDate = this.calendarDate.ToString("yyyy/MM");

                    // 変更後年月を取得する
                    String seleteDate = this.form.monthCalendar.SelectionStart.ToString("yyyy/MM");

                    // 変更前の年月と変更後年月は同一月ではないの場合
                    if (!nowDate.Equals(seleteDate))
                    {
                        // 20141112 koukouei 休動管理機能 start
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        bool catchErr = false;
                        bool henkouFlg = this.workCloseHenkouCheck(out catchErr);
                        if (catchErr)
                        {
                            ret = false;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        if (searchFlg && henkouFlg && msgLogic.MessageBoxShowConfirm("選択した内容が破棄されますがよろしいですか。") != DialogResult.Yes)
                        {
                            this.form.monthCalendar.SetSelectionRange(this.calendarDate, this.calendarDate);
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        // 20141112 koukouei 休動管理機能 end

                        // ヘッダフォーム年月日を設定する
                        this.headerForm.txt_nengappi.Text = this.form.monthCalendar.SelectionStart.ToString("yyyy年MM月");

                        // 検索業者CDを取得する
                        String gyoushaCd = this.form.TXT_GYOUSHA_CD.Text;

                        // 検索車輌CDを取得する
                        String sharyouCd = this.form.TXT_SHARYOU_CD.Text;

                        // 変更後年月によって、車輌休動データを再取得する
                        if (!SearchWorkClosedSharyouData(gyoushaCd, sharyouCd, seleteDate))
                        {
                            ret = false;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                    }
                    else
                    {
                        // 変更後日によって、車輌休動dataGridの行目を選択する
                        setDgvSharyouKyudou();
                    }
                }

                // カレンダ表示されている日付に変更後日付を設定する
                this.calendarDate = this.form.monthCalendar.SelectionStart;
            }
            catch (Exception ex)
            {
                LogUtility.Error("calendarDateChanged", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 車輌休動DataGridの行目を選択処理

        /// <summary>
        /// 変更後日によって、車輌休動DataGridの行目を選択する
        /// </summary>
        /// <returns></returns>
        public void setDgvSharyouKyudou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 変更後日を取得する
                int day = this.form.monthCalendar.SelectionStart.Day;
                if (day < 17)
                {
                    this.rightGridView1.Rows[day - 1].Cells[0].Selected = true;
                    // 赤枠非表示する
                    this.form.dgvSharyouKyudou2.CurrentCell = null;
                }
                else
                {
                    this.rightGridView2.Rows[day - 17].Cells[0].Selected = true;
                    // 赤枠非表示する
                    this.form.dgvSharyouKyudou1.CurrentCell = null;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 車輌休動DataGridViewの行クリック処理

        /// <summary>
        /// 車輌休動DataGridViewの行クリック処理
        /// </summary>
        /// <returns></returns>
        public void dgvSharyouKyudouCellClick(int index, String dataGridViewFlg)
        {
            try
            {
                LogUtility.DebugMethodStart(index, dataGridViewFlg);

                String selectDay;

                if (dataGridViewFlg.Equals("1"))
                {
                    // 選択した行目の日付を取得する
                    selectDay = this.rightGridView1.Rows[index].Cells["CLOSED_DATE"].Value.ToString();
                }
                else
                {
                    // 選択した行目の日付を取得する
                    selectDay = this.rightGridView2.Rows[index].Cells["CLOSED_DATE2"].Value.ToString();
                }

                // 選択した行目の日付によって、カレンダを設定する
                int day = this.calendarDate.Day;
                int intSelectDay = int.Parse(selectDay);
                DateTime setDate = this.calendarDate.AddDays(-(day - intSelectDay));
                // カレンダに選択した行目の日付を設定する
                this.form.monthCalendar.SetDate(setDate);
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvSharyouKyudouCellClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 前月ボタン押下処理

        /// <summary>
        /// 前月ボタン押下処理
        /// </summary>
        /// <returns></returns>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DateTime calendarBefor = this.parentForm.sysDate.Date;
                // カレンダ表示されているの前月の日付を取得する
                calendarBefor = this.form.monthCalendar.SelectionStart.AddMonths(-1);
                // 前月の一日を設定する
                int day = calendarBefor.Day;
                calendarBefor = calendarBefor.AddDays(-(day - 1));
                // カレンダに前月の一日を設定する
                this.form.monthCalendar.SetDate(calendarBefor);
                // フォーカスを設定する
                this.form.monthCalendar.Focus();

                // 検索車輌CDを取得する
                String sharyouCd = this.form.TXT_SHARYOU_CD.Text;

                //検索日付(前月)を取得する
                string searchDate = calendarBefor.ToString("yyyy/MM");

                // ヘッダの年月日を設定する
                this.headerForm.txt_nengappi.Text = calendarBefor.ToString("yyyy年MM月");
                if (sharyouCd.Equals(string.Empty))
                {
                    this.dataGridViewInit();
                    return;
                }
                else
                {
                    // 検索業者CDを取得する
                    String gyoushaCd = this.form.TXT_GYOUSHA_CD.Text;

                    // 車輌休動データを取得
                    this.SearchWorkClosedSharyouData(gyoushaCd, sharyouCd, searchDate);
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 次月ボタン押下処理

        /// <summary>
        /// 次月ボタン押下処理
        /// </summary>
        /// <returns></returns>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                DateTime calendarBefor = this.parentForm.sysDate.Date;
                // カレンダ表示されているの前月の日付を取得する
                calendarBefor = this.form.monthCalendar.SelectionStart.AddMonths(1);
                // 前月の一日を設定する
                int day = calendarBefor.Day;
                calendarBefor = calendarBefor.AddDays(-(day - 1));
                // カレンダに前月の一日を設定する
                this.form.monthCalendar.SetDate(calendarBefor);
                // フォーカスを設定する
                this.form.monthCalendar.Focus();

                // 検索車輌CDを取得する
                String sharyouCd = this.form.TXT_SHARYOU_CD.Text;

                //検索日付(前月)を取得する
                string searchDate = calendarBefor.ToString("yyyy/MM");

                // ヘッダの年月日を設定する
                this.headerForm.txt_nengappi.Text = calendarBefor.ToString("yyyy年MM月");
                if (sharyouCd.Equals(string.Empty))
                {
                    this.dataGridViewInit();
                    return;
                }
                else
                {
                    // 検索業者CDを取得する
                    String gyoushaCd = this.form.TXT_GYOUSHA_CD.Text;

                    // 車輌休動データを取得
                    this.SearchWorkClosedSharyouData(gyoushaCd, sharyouCd, searchDate);
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 条件取消ボタン押下処理

        /// <summary>
        /// 条件取消ボタン押下処理
        /// </summary>
        /// <returns></returns>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 検索条件をクリアする
                this.form.CONDITION_ITEM.Text = string.Empty;
                this.form.CONDITION_VALUE.Text = string.Empty;
                // フォーカスを設定する
                this.form.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 取消ボタン押下処理

        /// <summary>
        /// 取消ボタン押下処理
        /// </summary>
        /// <returns></returns>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 検索業者CDを取得する
                String gyoushaCd = this.form.TXT_GYOUSHA_CD.Text;

                // 検索車輌CDを取得する
                String sharyouCd = this.form.TXT_SHARYOU_CD.Text;

                // 検索日付を取得する
                String searchDate = this.calendarDate.ToString("yyyy/MM");

                if (sharyouCd.Equals(string.Empty))
                {
                    this.dataGridViewInit();
                    return;
                }
                else
                {
                    // 車輌休動データを取得
                    this.SearchWorkClosedSharyouData(gyoushaCd, sharyouCd, searchDate);
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 削除ボタン押下処理

        /// <summary>
        /// 削除ボタン押下処理
        /// </summary>
        /// <returns></returns>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 車輌が選択しない場合
                if (this.form.TXT_SHARYOU_CD.Text.Equals(string.Empty))
                {
                    // メッセージ通知
                    msgLogic.MessageBoxShow("E051", "車輌");
                    return;
                }
                // 日付設定DataGridViewのデータを取得する（チェックボックスがチェックオフのレコード）
                List<M_WORK_CLOSED_SHARYOU> entityList = getDataGridViewList(false);

                // 削除用エンティティリストを作成する
                CreateEntityList(entityList, true);

                // [チェックボックス]のチェック有⇒チェック無に遷移した、該当日付が無の場合
                if (this.deleteList.Count == 0)
                {
                    // アラート表示し、処理しない
                    msgLogic.MessageBoxShow("E075");
                    return;
                }

                // 確認メッセージを表示する
                var result = msgLogic.MessageBoxShow("C026");
                if (result == DialogResult.Yes)
                {
                    using (Transaction tran = new Transaction())
                    {
                        // レコード削除を行う
                        foreach (M_WORK_CLOSED_SHARYOU untensyaEntity in this.deleteList)
                        {
                            this.kyudouDao.Delete(untensyaEntity);
                        }

                        // コミット
                        tran.Commit();
                    }

                    // 正常終了メッセージ
                    msgLogic.MessageBoxShow("I001", "選択データの削除");
                    // 画面再検索
                    this.SearchWorkClosedSharyouData(this.form.TXT_GYOUSHA_CD.Text, this.form.TXT_SHARYOU_CD.Text, this.calendarDate.ToString("yyyy/MM"));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

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

        #region 日付設定DataGridViewのデータ取得

        /// <summary>
        /// 日付設定DataGridViewのデータを取得する
        /// </summary>
        /// <param name="isChecked">true:チェックボックスがチェックオン、false:チェックボックスがチェックオフ</param>
        /// <returns>車輌休動エンティティリスト</returns>
        internal List<M_WORK_CLOSED_SHARYOU> getDataGridViewList(bool isChecked)
        {
            // 車輌休動エンティティリスト
            List<M_WORK_CLOSED_SHARYOU> entityList = new List<M_WORK_CLOSED_SHARYOU>();

            try
            {
                LogUtility.DebugMethodStart(isChecked);

                // 車輌休動エンティティ
                M_WORK_CLOSED_SHARYOU entity = null;

                // 日付設定DataGridView1のデータを取得する
                foreach (DataGridViewRow row in this.form.dgvSharyouKyudou1.Rows)
                {
                    // チェックボックスがチェックオンの場合
                    if (isChecked.Equals(row.Cells["CLOSED_DATE_FLG"].Value))
                    {
                        entity = new M_WORK_CLOSED_SHARYOU();
                        // 業者CD
                        entity.GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
                        // 車輌CD
                        entity.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
                        // 休動日
                        entity.CLOSED_DATE = Convert.ToDateTime(this.calendarDate.ToString("yyyy/MM") + "/" + row.Cells["CLOSED_DATE"].Value.ToString());
                        // 備考
                        entity.BIKOU = row.Cells["BIKOU"].Value != null ? row.Cells["BIKOU"].Value.ToString() : "";
                        // 削除フラグ
                        entity.DELETE_FLG = false;
                        //更新時間、更新者、更新PCを設定
                        var dataBinder1 = new DataBinderLogic<M_WORK_CLOSED_SHARYOU>(entity);
                        dataBinder1.SetSystemProperty(entity, false);
                        // TIME_STAMP
                        if (!string.IsNullOrEmpty(row.Cells["TIME_STAMP"].FormattedValue.ToString()))
                        {
                            entity.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
                        }

                        entityList.Add(entity);
                    }
                }

                // 日付設定DataGridView2のデータを取得する
                foreach (DataGridViewRow row in this.form.dgvSharyouKyudou2.Rows)
                {
                    // チェックボックスがチェックオンの場合
                    if (isChecked.Equals(row.Cells["CLOSED_DATE_FLG2"].Value))
                    {
                        entity = new M_WORK_CLOSED_SHARYOU();
                        // 業者CD
                        entity.GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
                        // 車輌CD
                        entity.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
                        // 休動日
                        entity.CLOSED_DATE = Convert.ToDateTime(this.calendarDate.ToString("yyyy/MM") + "/" + row.Cells["CLOSED_DATE2"].Value.ToString());
                        // 備考
                        entity.BIKOU = row.Cells["BIKOU2"].Value != null ? row.Cells["BIKOU2"].Value.ToString() : "";
                        // 削除フラグ
                        entity.DELETE_FLG = false;
                        //更新時間、更新者、更新PCを設定
                        var dataBinder1 = new DataBinderLogic<M_WORK_CLOSED_SHARYOU>(entity);
                        dataBinder1.SetSystemProperty(entity, false);

                        // TIME_STAMP
                        if (!string.IsNullOrEmpty(row.Cells["TIME_STAMP2"].FormattedValue.ToString()))
                        {
                            entity.TIME_STAMP = (byte[])row.Cells["TIME_STAMP2"].Value;
                        }

                        entityList.Add(entity);
                    }
                }

                return entityList;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(entityList);
            }
        }

        #endregion

        #region DB登録用エンティティリスト生成処理

        /// <summary>
        /// DB登録用エンティティリストを生成する
        /// </summary>
        /// <param name="entityList">車輌休動エンティティリスト</param>
        /// <param name="deleteFlg">削除フラグ</param>
        internal void CreateEntityList(List<M_WORK_CLOSED_SHARYOU> entityList, bool deleteFlg)
        {
            try
            {
                LogUtility.DebugMethodStart(entityList, deleteFlg);

                // 更新用エンティティリスト
                this.updateList = new List<M_WORK_CLOSED_SHARYOU>();
                // 登録用エンティティリスト
                this.insertList = new List<M_WORK_CLOSED_SHARYOU>();
                // 削除用エンティティリスト
                this.deleteList = new List<M_WORK_CLOSED_SHARYOU>();
                // 画面上の日付
                String entityClosedDate = string.Empty;
                // DB側の日付
                String dbClosedDate = string.Empty;
                // 存在フラグ
                bool isExsit = false;

                // 更新用エンティティリストを繰り返す
                foreach (M_WORK_CLOSED_SHARYOU entity in entityList)
                {
                    // 画面上の日付を取得する
                    entityClosedDate = DateTime.Parse(entity.CLOSED_DATE.ToString()).ToShortDateString();

                    // 存在フラグを初期化
                    isExsit = false;
                    // 車輌休動マスタ検索結果を繰り返す
                    for (int i = 0; i < this.SearchSharyoukyudouResult.Rows.Count; i++)
                    {
                        // DB側の日付を取得する
                        dbClosedDate = DateTime.Parse(this.SearchSharyoukyudouResult.Rows[i]["CLOSED_DATE"].ToString()).ToShortDateString();

                        // 画面上の日付と比較し、同じの場合
                        if (dbClosedDate.Equals(entityClosedDate))
                        {
                            if (deleteFlg)
                            {
                                deleteList.Add(entity);
                            }
                            else
                            {
                                updateList.Add(entity);
                            }
                            isExsit = true;
                            break;
                        }
                    }

                    // DBに存在しない場合、登録対象になります
                    if (!isExsit)
                    {
                        this.insertList.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityList", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 閉じるボタン押下処理

        /// <summary>
        /// 閉じるボタン押下処理
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 登録ボタン押下処理

        /// <summary>
        /// 登録ボタン押下処理
        /// </summary>
        /// <returns></returns>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 車輌が選択しない場合
                if (this.form.TXT_SHARYOU_CD.Text.Equals(string.Empty))
                {
                    // メッセージ通知
                    msgLogic.MessageBoxShow("E051", "車輌");
                    return;
                }

                // 日付設定DataGridViewのデータを取得する（チェックボックスがチェックオンのレコード）
                List<M_WORK_CLOSED_SHARYOU> entityList = getDataGridViewList(true);
                // チェックボックスに1件もチェックされていない場合
                if (entityList.Count == 0)
                {
                    // アラート表示し、処理しない
                    msgLogic.MessageBoxShow("E051", "日付");
                    return;
                }

                // 20141008 koukouei 休動管理機能 start
                if (!this.workCloseCheck())
                {
                    // アラート表示し、処理しない
                    msgLogic.MessageBoxShow("E205", "車輌");
                    return;
                }
                // 20141008 koukouei 休動管理機能 end

                // 確認メッセージを表示する
                var result = msgLogic.MessageBoxShow("C046", "登録");
                if (result == DialogResult.Yes)
                {
                    // 更新、登録用エンティティリストを作成する
                    CreateEntityList(entityList, false);

                    bool isError = false;
                    using (Transaction tran = new Transaction())
                    {
                        // レコード登録を行う
                        foreach (M_WORK_CLOSED_SHARYOU insertEntity in this.insertList)
                        {
                            M_WORK_CLOSED_SHARYOU checkData = this.kyudouDao.GetSharyoukyudouWithTableLock(insertEntity);

                            if (checkData == null)
                            {
                                this.kyudouDao.Insert(insertEntity);
                            }
                            else
                            {
                                isError = true;
                                break;
                            }
                        }

                        // レコード更新を行う
                        if (!isError)
                        {
                            foreach (M_WORK_CLOSED_SHARYOU updateEntity in this.updateList)
                            {
                                this.kyudouDao.Update(updateEntity);
                            }
                        }

                        if (!isError)
                        {
                            // コミット
                            tran.Commit();
                        }
                    }

                    if (!isError)
                    {
                        // 正常終了メッセージ
                        msgLogic.MessageBoxShow("I001", "登録");
                    }
                    else
                    {
                        // Insert時に既にデータがあった場合は排他エラーとする
                        msgLogic.MessageBoxShow("E080");
                    }

                    // 画面再検索
                    this.SearchWorkClosedSharyouData(this.form.TXT_GYOUSHA_CD.Text, this.form.TXT_SHARYOU_CD.Text, this.calendarDate.ToString("yyyy/MM"));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

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

        #region 新規、更新データを取得処理

        /// <summary>
        /// 新規、更新データを取得する
        /// </summary>
        /// <returns></returns>
        internal void getEntityData(List<M_WORK_CLOSED_SHARYOU> entityList, String modFlg)
        {
            try
            {
                LogUtility.DebugMethodStart(entityList, modFlg);

                this.updateList = new List<M_WORK_CLOSED_SHARYOU>();
                this.insertList = new List<M_WORK_CLOSED_SHARYOU>();
                this.deleteList = new List<M_WORK_CLOSED_SHARYOU>();

                foreach (M_WORK_CLOSED_SHARYOU sharyoukyudouEntity in entityList)
                {
                    // 画面の日付
                    String entityClosedDate = DateTime.Parse(sharyoukyudouEntity.CLOSED_DATE.ToString()).ToShortDateString();

                    bool insetFlg = true;

                    for (int i = 0; i < this.SearchSharyoukyudouResult.Rows.Count; i++)
                    {
                        // DBから取得したデータ
                        String dbClosedDate = DateTime.Parse(this.SearchSharyoukyudouResult.Rows[i]["CLOSED_DATE"].ToString()).ToShortDateString();

                        if (modFlg.Equals("1") && entityClosedDate.Equals(dbClosedDate))
                        {
                            this.updateList.Add(sharyoukyudouEntity);
                            insetFlg = false;
                            break;
                        }
                        else if (modFlg.Equals("0") && entityClosedDate.Equals(dbClosedDate))
                        {
                            this.deleteList.Add(sharyoukyudouEntity);
                        }
                    }
                    if (modFlg.Equals("1"))
                    {
                        if (insetFlg)
                        {
                            this.insertList.Add(sharyoukyudouEntity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        // 20141008 koukouei 休動管理機能 start
        #region 休動チェック処理

        /// <summary>
        /// 休動チェック処理
        /// </summary>
        /// <returns></returns>
        private bool workCloseCheck()
        {
            LogUtility.DebugMethodStart();

            bool result = true;
            DateTime date = this.form.monthCalendar.SelectionStart;
            // 収集受付
            T_UKETSUKE_SS_ENTRY uketsukeSS = new T_UKETSUKE_SS_ENTRY();
            uketsukeSS.UNPAN_GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
            uketsukeSS.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
            uketsukeSS.SAGYOU_DATE = date.ToString("yyyy-MM");
            DataTable dtUketsukeSs = this.uketsukeSsDao.GetUketsukeSSData(uketsukeSS);
            // 出荷受付
            T_UKETSUKE_SK_ENTRY uketsukeSK = new T_UKETSUKE_SK_ENTRY();
            uketsukeSK.UNPAN_GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
            uketsukeSK.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
            uketsukeSK.SAGYOU_DATE = date.ToString("yyyy-MM");
            DataTable dtUketsukeSk = this.uketsukeSkDao.GetUketsukeSKData(uketsukeSK);
            // 持込受付
            T_UKETSUKE_MK_ENTRY uketsukeMK = new T_UKETSUKE_MK_ENTRY();
            uketsukeMK.UNPAN_GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
            uketsukeMK.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
            uketsukeMK.SAGYOU_DATE = date.ToString("yyyy-MM");
            DataTable dtUketsukeMk = this.uketsukeMkDao.GetUketsukeMKData(uketsukeMK);
            // 定期配車入力
            T_TEIKI_HAISHA_ENTRY teikiHaisha = new T_TEIKI_HAISHA_ENTRY();
            teikiHaisha.UNPAN_GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
            teikiHaisha.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
            teikiHaisha.SAGYOU_DATE = date;
            DataTable dtTeikiHaisha = this.teikiHaishaDao.GetTeikiHaishaData(teikiHaisha);
            // 定期配車実績入力
            T_TEIKI_JISSEKI_ENTRY teikiJisseki = new T_TEIKI_JISSEKI_ENTRY();
            teikiJisseki.UNPAN_GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
            teikiJisseki.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
            teikiJisseki.SAGYOU_DATE = date;
            DataTable dtTeikiJisseki = this.teikiJissekiDao.GetTeikiJissekiData(teikiJisseki);
            // 受入入力
            T_UKEIRE_ENTRY ukeire = new T_UKEIRE_ENTRY();
            ukeire.UNPAN_GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
            ukeire.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
            ukeire.DENPYOU_DATE = date;
            DataTable dtUkeire = this.ukeireDao.GetUkeireData(ukeire);
            // 出荷受付
            T_SHUKKA_ENTRY shukka = new T_SHUKKA_ENTRY();
            shukka.UNPAN_GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
            shukka.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
            shukka.DENPYOU_DATE = date;
            DataTable dtShukka = this.shukkaDao.GetShukkaData(shukka);
            // 売上支払入力
            T_UR_SH_ENTRY urSh = new T_UR_SH_ENTRY();
            urSh.UNPAN_GYOUSHA_CD = this.form.TXT_GYOUSHA_CD.Text;
            urSh.SHARYOU_CD = this.form.TXT_SHARYOU_CD.Text;
            urSh.DENPYOU_DATE = date;
            DataTable dtURSH = this.urShDao.GetURSHData(urSh);

            DataRow[] rows;
            // dataGird1を設定する
            foreach (DataGridViewRow row in this.form.dgvSharyouKyudou1.Rows)
            {
                if (!(bool)row.Cells["CLOSED_DATE_FLG"].Value)
                {
                    continue;
                }
                string day = row.Cells["CLOSED_DATE"].Value.ToString();
                // 日付
                date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                // 収集受付
                rows = dtUketsukeSs.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 出荷受付
                rows = dtUketsukeSk.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 持込受付
                rows = dtUketsukeMk.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 定期配車入力
                rows = dtTeikiHaisha.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 定期配車実績入力
                rows = dtTeikiJisseki.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 受入入力
                rows = dtUkeire.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 出荷入力
                rows = dtShukka.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 売上支払入力
                rows = dtURSH.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
            }

            // dataGird2を設定する
            foreach (DataGridViewRow row in this.form.dgvSharyouKyudou2.Rows)
            {
                if (!(bool)row.Cells["CLOSED_DATE_FLG2"].Value)
                {
                    continue;
                }
                string day = row.Cells["CLOSED_DATE2"].Value.ToString();
                // 日付
                date = Convert.ToDateTime(this.form.monthCalendar.SelectionStart.ToString("yyyy/MM") + "/" + day);
                // 収集受付
                rows = dtUketsukeSs.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 出荷受付
                rows = dtUketsukeSk.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 持込受付
                rows = dtUketsukeMk.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 定期配車入力
                rows = dtTeikiHaisha.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 定期配車実績入力
                rows = dtTeikiJisseki.Select(string.Format("SAGYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 受入入力
                rows = dtUkeire.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 出荷入力
                rows = dtShukka.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
                // 売上支払入力
                rows = dtURSH.Select(string.Format("DENPYOU_DATE = '{0}'", date));
                if (rows != null && rows.Length > 0)
                {
                    row.Cells["CLOSED_DATE2"].Style.BackColor = Constans.ERROR_COLOR;
                    result = false;
                    continue;
                }
            }

            LogUtility.DebugMethodEnd();
            return result;
        }

        #endregion
        // 20141008 koukouei 休動管理機能 end

        // 20141112 koukouei 休動管理機能 start
        #region 休動変更チェック処理

        /// <summary>
        /// 休動変更チェック処理
        /// </summary>
        /// <returns></returns>
        internal bool workCloseHenkouCheck(out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                // dataGird1を設定する
                foreach (DataGridViewRow row in this.form.dgvSharyouKyudou1.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["HENKOU_FLG1"].Value))
                    {
                        LogUtility.DebugMethodEnd(ret, catchErr);
                        return ret;
                    }
                }

                // dataGird2を設定する
                foreach (DataGridViewRow row in this.form.dgvSharyouKyudou2.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["HENKOU_FLG2"].Value))
                    {
                        LogUtility.DebugMethodEnd(ret, catchErr);
                        return ret;
                    }
                }
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("workCloseHenkouCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        #endregion
        // 20141112 koukouei 休動管理機能 end

        #endregion
    }
}