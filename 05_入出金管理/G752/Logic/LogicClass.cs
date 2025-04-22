using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKeshikomi
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.ShukkinKeshikomi.Setting.ButtonSetting.xml";

        /// <summary>
        /// システム上の必須項目
        /// </summary>
        internal readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>
        /// システム上の必須項目
        /// </summary>
        internal readonly string HIDDEN_SHUKKIN_NUMBER = "HIDDEN_SHUKKIN_NUMBER";

        /// <summary>
        /// システム上の必須項目
        /// </summary>
        internal readonly string HIDDEN_KESHIKOMI_SEQ = "HIDDEN_KESHIKOMI_SEQ";    

        /// <summary>
        /// 汎用、簡易検索フラグ
        /// </summary>
        private string Hanyou_Kenyi;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        private UIHeader headForm;
        private BusinessBaseForm footer;

        /// <summary>	
        /// 拠点マスタ	
        /// </summary>	
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;
        
        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// テーブル「M_OUTPUT_PATTERN_KOBETSU」のディフォール区分
        /// </summary>
        public DataTable setDefaultKbn { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        public string joinQuery { get; set; }

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        private Control[] allControl;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.dto = new DTOClass();
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //2013.12.23 naitou upd start
                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                //　ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.Location = new System.Drawing.Point(3, 141);
                this.form.customDataGridView1.Size = new System.Drawing.Size(997, 278);
                //2013.12.23 naitou upd end
                //2013.12.23 naitou upd start
                //アラート件数
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                this.headForm.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                this.headForm.NumberAlert = this.headForm.InitialNumberAlert;
                //2013.12.23 naitou upd end
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;

        }

        //2013.12.23 naitou upd start
        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.headForm = targetHeader;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        }
        //2013.12.23 naitou upd end

        //2013.12.23 naitou upd start
        /// <summary>
        /// 画面クリア
        /// </summary>
        public bool ClearScreen(String Kbn)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                switch (Kbn)
                {
                    case "Initial"://初期表示
                    case "ClsSearchCondition"://検索条件をクリア

                        //タイトル
                        this.headForm.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_SHUKKIN_KESHIKOMI_RIEKI_ICHIRAN);

                        //アラート件数
                        this.headForm.NumberAlert = this.headForm.InitialNumberAlert;

                        //常にシステム設定ファイルから拠点CDを設定する
                        XMLAccessor fileAccess = new XMLAccessor();
                        CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
                        this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));

                        // 拠点名称
                        if (this.headForm.KYOTEN_CD.Text != "")
                        {
                            M_KYOTEN mKyoten = new M_KYOTEN();
                            mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                            if (mKyoten == null)
                            {
                                this.headForm.KYOTEN_NAME_RYAKU.Text = "";
                            }
                            else
                            {
                                this.headForm.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
                            }
                        }

                        //検索条件
                        this.form.searchString.Clear();

                        break;

                }

                //読込データ件数
                this.headForm.ReadDataNumber.Text = "0";

                //アラート件数
                this.headForm.alertNumber.Text = this.headForm.NumberAlert.ToString();

                //一覧明細をクリア
                this.form.customDataGridView1.DataSource = null;
                this.form.ShowHeader();

                //並び順をクリア
                this.form.customSortHeader1.ClearCustomSortSetting();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ClearScreen", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearScreen", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        //2013.12.23 naitou upd end

        /// <summary>
        /// 起動元画面で条件が指定されている場合に、検索条件をセットする
        /// </summary>
        internal bool SetStartUpCondition()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 出金入力画面から遷移した場合
                if (!string.IsNullOrEmpty(this.form.kyotenCdForStartUpPoint))
                {
                    this.headForm.KYOTEN_CD.Text = this.form.kyotenCdForStartUpPoint;
                    M_KYOTEN mKyoten = new M_KYOTEN();
                    mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                    if (mKyoten == null)
                    {
                        this.headForm.KYOTEN_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        this.headForm.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetStartUpCondition", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetStartUpCondition", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion        

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        public void EventInit()
        {
            LogUtility.DebugMethodStart();
            //Functionボタンのイベント生成
            footer.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);          //CSV出力
            footer.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);          //検索条件クリア
            footer.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);          //検索
            footer.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);        //閉じる
            footer.bt_func10.Click += new EventHandler(this.bt_func10_Click);               //並替移動ボタン(F10)イベント生成
            footer.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);        //F11 フィルタ
            footer.bt_process1.Click += new EventHandler(bt_process1_Click);                //パターン一覧画面へ遷移         

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Functionボタン 押下処理     

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            // 一覧にデータ行がない場合
            if (this.form.customDataGridView1.RowCount == 0)
            {
                // アラートを表示し、CSV出力処理はしない
                msgLogic.MessageBoxShow("E044");
            }
            else
            {
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport exp = new CSVExport();
                    exp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, DENSHU_KBNExt.ToTitleString(DENSHU_KBN.SHUKKIN_KESHIKOMI_RIEKI_ICHIRAN), this.form);
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            //並び順をクリア
            this.form.customSortHeader1.ClearCustomSortSetting();
            //フィルタをクリア
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            this.ClearScreen("ClsSearchCondition");
            //2013.12.23 naitou upd end

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

            // パターンチェック
            if (this.form.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }
            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text) &&
                string.IsNullOrEmpty(this.form.Shukkin_CD.Text) &&
                string.IsNullOrEmpty(this.form.SEISAN_NUMBER.Text))
            {
                MessageBoxUtility.MessageBoxShow("E001", "検索時には、取引先、出金番号、精算番号のいずれか");
                return;
            }

            this.Search();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F10 並替移動ボタン
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.form.customDataGridView1.Rows.Count < 1)
            {
                return;
            }
            this.form.customSortHeader1.ShowCustomSortSettingDialog();

            LogUtility.DebugMethodEnd();
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
                this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headForm.ReadDataNumber.Text = "0";
            }
        }


        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.setSetting();
            //2013.12.23 naitou upd end

            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Settingsの値の保存

        //2013.12.23 naitou upd start
        /// <summary>
        /// Settingsの値の保存
        /// </summary>
        public void setSetting()
        {
            LogUtility.DebugMethodStart();

            Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;     

            Properties.Settings.Default.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;              //取引先

            Properties.Settings.Default.Save();

            LogUtility.DebugMethodEnd();
        }
        //2013.12.23 naitou upd end

        #endregion

        #region プロセスボタン押下処理

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var sysID = this.form.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.form.SetPatternBySysId(sysID);
                this.SearchResult = this.form.Table;
                this.form.ShowData();
            }
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
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            LogUtility.DebugMethodEnd();

        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        public void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region HeaderForm

        /// <summary>
        /// HeaderForm.cs設定
        /// </summary>
        /// /// <returns>hs</returns>
        public void SetHeader(UIHeader hs)
        {
            LogUtility.DebugMethodStart(hs);
            this.headForm = hs;
            LogUtility.DebugMethodEnd(hs);
        }

        #endregion

        #region 出金消込一覧

        /// <summary>
        /// 出金消込一覧
        /// </summary>
        public void MakeSearchCondition()
        {
            LogUtility.DebugMethodStart();
            //SQL文格納StringBuilder
            var sql = new StringBuilder(); 
            sql.Append(" SELECT DISTINCT ");
            sql.Append(this.selectQuery);
            sql.AppendFormat(" , T_SHUKKIN_KESHIKOMI.SHUKKIN_NUMBER AS {0} ", this.HIDDEN_SHUKKIN_NUMBER);
            sql.AppendFormat(" , T_SHUKKIN_KESHIKOMI.SYSTEM_ID AS {0} ", this.HIDDEN_SYSTEM_ID);

            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
            {
                sql.AppendFormat(" , T_SHUKKIN_KESHIKOMI.KESHIKOMI_SEQ AS {0} ", this.HIDDEN_KESHIKOMI_SEQ);
            }


            #region FROM句

            //FROM句作成
            sql.Append(" FROM ");
            // 出金消込テーブル
            sql.Append(" T_SHUKKIN_KESHIKOMI ");

            // 出金テーブル
            sql.Append(" LEFT JOIN T_SHUKKIN_ENTRY ");
            sql.Append(" ON T_SHUKKIN_ENTRY.SYSTEM_ID = T_SHUKKIN_KESHIKOMI.SYSTEM_ID ");
            // 取引先CD
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                sql.Append(" AND T_SHUKKIN_ENTRY.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
            }

            // 精算伝票テーブル
            sql.Append(" LEFT JOIN T_SEISAN_DENPYOU ");
            sql.Append(" ON T_SEISAN_DENPYOU.SEISAN_NUMBER = T_SHUKKIN_KESHIKOMI.SEISAN_NUMBER ");
            sql.Append(" LEFT JOIN T_SEISAN_DENPYOU_KAGAMI ");
            sql.Append(" ON T_SEISAN_DENPYOU.SEISAN_NUMBER = T_SEISAN_DENPYOU_KAGAMI.SEISAN_NUMBER ");
            sql.Append(" AND T_SHUKKIN_KESHIKOMI.KAGAMI_NUMBER = T_SEISAN_DENPYOU_KAGAMI.KAGAMI_NUMBER ");

            // 取引先精算マスタ
            sql.Append(" LEFT JOIN M_TORIHIKISAKI_SHIHARAI ");
            sql.Append(" ON T_SHUKKIN_KESHIKOMI.TORIHIKISAKI_CD = M_TORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD ");

            // 同一の精算についての出金消込の合計金額を取得
            sql.Append(" LEFT JOIN ");
            sql.Append(" (SELECT SEISAN_NUMBER,SUM(KESHIKOMI_GAKU) AS KESHIKOMI_GAKU_TOTAL FROM T_SHUKKIN_KESHIKOMI WHERE DELETE_FLG = 0 GROUP BY SEISAN_NUMBER) T_SHUKKIN_KESHIKOMI2 ");
            sql.Append(" ON T_SEISAN_DENPYOU.SEISAN_NUMBER = T_SHUKKIN_KESHIKOMI2.SEISAN_NUMBER ");

            // 取引先開始残高についての出金消込の合計金額を取得
            sql.Append(" LEFT JOIN ");
            sql.Append(" (SELECT TORIHIKISAKI_CD,SEISAN_NUMBER,SUM(KESHIKOMI_GAKU) AS KESHIKOMI_GAKU_TOTAL FROM T_SHUKKIN_KESHIKOMI WHERE DELETE_FLG = 0 GROUP BY TORIHIKISAKI_CD,SEISAN_NUMBER) T_SHUKKIN_KESHIKOMI3 ");
            sql.Append(" ON T_SHUKKIN_KESHIKOMI.TORIHIKISAKI_CD = T_SHUKKIN_KESHIKOMI3.TORIHIKISAKI_CD AND T_SHUKKIN_KESHIKOMI.SEISAN_NUMBER = T_SHUKKIN_KESHIKOMI3.SEISAN_NUMBER ");

            sql.Append(this.joinQuery);

            #endregion

            #region WHERE句

            sql.Append(" WHERE ");
            
            sql.Append(" T_SHUKKIN_KESHIKOMI.KESHIKOMI_GAKU IS NOT NULL ");
            if ("".Equals(this.form.Shukkin_CD.Text))
            {
                sql.Append(" AND T_SHUKKIN_KESHIKOMI.DELETE_FLG = 0 ");
            }
            else
            {
                sql.Append(" AND T_SHUKKIN_KESHIKOMI.SHUKKIN_NUMBER = '" + long.Parse(this.form.Shukkin_CD.Text) + "' ");
                sql.Append(" AND T_SHUKKIN_KESHIKOMI.DELETE_FLG = 0 ");
            }
            // 拠点CD
            if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text) && !"99".Equals(this.headForm.KYOTEN_CD.Text))
            {
                sql.Append(" AND T_SHUKKIN_ENTRY.KYOTEN_CD = " + int.Parse(this.headForm.KYOTEN_CD.Text) + " ");
            }
            // 取引先CD
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                sql.Append(" AND T_SHUKKIN_KESHIKOMI.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
            }
            // 精算番号
            if (!string.IsNullOrEmpty(this.form.SEISAN_NUMBER.Text))
            {
                sql.Append(" AND T_SEISAN_DENPYOU.SEISAN_NUMBER = '" + this.form.SEISAN_NUMBER.Text + "' ");
                sql.Append(" AND T_SHUKKIN_KESHIKOMI.SEISAN_NUMBER = '" + this.form.SEISAN_NUMBER.Text + "' ");
            }
            sql.Append(" AND (T_SEISAN_DENPYOU.DELETE_FLG = 0 OR T_SEISAN_DENPYOU.DELETE_FLG IS NULL) ");

            sql.Append(" AND ISNULL(T_SHUKKIN_ENTRY.DELETE_FLG,0) = 0 ");

            #region ORDERBY句

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            #endregion

            #endregion

            this.createSql = sql.ToString();
            sql.Append("");
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string errorStr = ""; //2013.12.23 naitou upd

                //SQLの作成 出金消込一覧
                this.form.setLogicSelect();
                this.MakeSearchCondition();

                //検索実行
                this.SearchResult = new DataTable();

                //必須チェックエラーメッセージ表示
                if (!String.IsNullOrEmpty(errorStr))
                {
                    MessageBoxUtility.MessageBoxShowError(errorStr);
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }

                if (!string.IsNullOrEmpty(this.syainCode))
                {
                    this.SearchResult = daoIchiran.getdateforstringsql(this.createSql);

                    int count = SearchResult.Rows.Count;

                    //2013.12.15 naitou upd start
                    
                    this.form.ShowData();

                    //読込データ件数を取得
                    if (this.form.customDataGridView1 != null)
                    {
                        this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                    }
                    else
                    {
                        this.headForm.ReadDataNumber.Text = "0";
                    }

                    if (count == 0)
                    {
                        MessageBoxUtility.MessageBoxShow("C001");
                    }
                    LogUtility.DebugMethodEnd(SearchResult.Rows.Count);
                    if (this.form.customDataGridView1.Rows.Count == 0)
                    {
                        this.form.customDataGridView1.TabStop = false;
                    }
                    else
                    {
                        this.form.customDataGridView1.TabStop = true;
                    }
                    return SearchResult.Rows.Count;
                }

                this.form.SetSearch(); //2013.12.23 naitou upd
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }

            LogUtility.DebugMethodEnd(0);

            return 0;
        }

        #endregion

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
    }
}
