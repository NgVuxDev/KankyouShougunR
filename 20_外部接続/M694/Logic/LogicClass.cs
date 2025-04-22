// $Id: LogicClass.cs 54103 2015-06-30 08:35:46Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
//ロジこん連携用
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.GaibuRenkeiGenbaIchiran
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.GaibuRenkeiGenbaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private GaibuRenkeiGenbaIchiranForm form;

        /// <summary>
        /// フォームオブジェクト
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        private HeaderForm headerForm;

        /// <summary>
        /// メッセージクラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// システム設定マスタのエンティティ
        /// </summary>
        private M_SYS_INFO sysinfoEntity;

        /// <summary>
        /// 現場マスタのエンティティ
        /// </summary>
        private M_GENBA genbaEntity;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// 外部連携現場のDao
        /// </summary>
        private IM_GENBA_DIGIDao daoGenbaDigi;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 都道府県のDao
        /// </summary>
        private IM_TODOUFUKENDao daoTodoufuken;

        /// <summary>
        /// 現場出力済みDao
        /// </summary>
        private IM_DIGI_OUTPUT_GENBADao daoGenbaTS;

        /// <summary>
        /// ロジこん連携用Dao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        private IS_LOGI_CONNECTDao logiConnectDao;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(GaibuRenkeiGenbaIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            //Form
            this.form = targetForm;

            //Dao
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.daoGenbaTS = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_GENBADao>();
            this.daoGenbaDigi = DaoInitUtility.GetComponent<IM_GENBA_DIGIDao>();

            //ロジこん用Dao
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.logiConnectDao = DaoInitUtility.GetComponent<IS_LOGI_CONNECTDao>();

            //メッセージ
            this.msgLogic = new MessageBoxShowLogic();

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

                //フォーム初期化
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (HeaderForm)parentForm.headerForm;

                //システム設定情報読み込み
                this.GetSysInfo();

                // ヘッダーの初期化
                this.InitHeaderArea();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                // 表示条件初期化
                this.RemoveIchiranHyoujiJoukenEvent();
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
                this.AddIchiranHyoujiJoukenEvent();
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }

                this.form.txtNum_RenkeiJyoukyou.Text = "3";

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
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
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
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
            this.parentForm.bt_func2.Click += new System.EventHandler(this.form.bt_func2_Click);      // 新規
            this.parentForm.bt_func3.Click += new System.EventHandler(this.form.bt_func3_Click);      // 修正
            this.parentForm.bt_func6.Click += new System.EventHandler(this.form.bt_func6_Click);      // CSV
            this.parentForm.bt_func7.Click += new System.EventHandler(this.form.bt_func7_Click);      // 検索条件クリア
            this.parentForm.bt_func8.Click += new System.EventHandler(this.form.bt_func8_Click);      // 検索
            this.parentForm.bt_func9.Click += new System.EventHandler(this.form.bt_func9_Click);      // 連携登録
            this.parentForm.bt_func10.Click += new System.EventHandler(this.form.bt_func10_Click);    // 並び替え
            this.parentForm.bt_func11.Click += new System.EventHandler(this.form.bt_func11_Click);    // フィルタ
            this.parentForm.bt_func12.Click += new System.EventHandler(this.form.bt_func12_Click);    // 閉じる
            this.parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);       // パターン一覧画面へ遷移
            this.parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);       // 外部連携CSV

            //明細ダブルクリック時のイベント
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DetailCellDoubleClick);

            //明細チェックボックスクリックの設定
            this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(customDataGridView1_ColumnHeaderMouseClick);

            this.form.TEKIYOU_END.MouseDoubleClick += new MouseEventHandler(this.form.TEKIYOU_END_MouseDoubleClick);

            this.form.TEKIYOU_BEGIN.Leave += new System.EventHandler(this.form.TEKIYOU_BEGIN_Leave);
            this.form.TEKIYOU_END.Leave += new System.EventHandler(this.form.TEKIYOU_END_Leave);

            //表示条件イベント生成
            this.AddIchiranHyoujiJoukenEvent();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ヘッダーの初期化

        private void InitHeaderArea()
        {
            //初期値設定
            this.headerForm.txtNum_HidukeSentaku.Text = "2";

            //日付の初期値(2018/01/01～今日)
            this.headerForm.HIDUKE_FROM.Text = "2018/01/01";
            this.headerForm.HIDUKE_TO.Text = DateTime.Now.ToString();

            // 初期値設定
            this.form.GYOUSHA_CD.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT;
            this.form.GYOUSHA_RNAME.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT;
            this.form.GENBA_CD.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_CD_TEXT;
            this.form.GENBA_FURIGANA.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_FURIGANA_TEXT;
            this.form.GENBA_NAME1.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_NAME1_TEXT;
            this.form.GENBA_NAME2.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_NAME2_TEXT;
            this.form.GENBA_NAME_RYAKU.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_NAME_RYAKU_TEXT;

            this.form.DIGI_POINT_KANA_NAME.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.POINT_KANA_NAME_TEXT;
            this.form.DIGI_POINT_NAME.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.POINT_NAME_TEXT;
            this.form.DIGI_MAP_NAME.Text = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.MAP_NAME_TEXT;

            if (GaibuRenkeiGenbaIchiran.Properties.Settings.Default["TEKIYOU_BEGIN_VALUE"] == null)
            {
                this.form.TEKIYOU_BEGIN.Value = null;
            }
            else if ((DateTime)GaibuRenkeiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE != new DateTime(0))
            {
                this.form.TEKIYOU_BEGIN.Value = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE;
            }
            if (GaibuRenkeiGenbaIchiran.Properties.Settings.Default["TEKIYOU_END_VALUE"] == null)
            {
                this.form.TEKIYOU_END.Value = null;
            }
            else if ((DateTime)GaibuRenkeiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE != new DateTime(0))
            {
                this.form.TEKIYOU_END.Value = GaibuRenkeiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE;
            }

            //現場名をセット
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                M_GENBA condition = new M_GENBA();

                condition.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                condition.GENBA_CD = this.form.GENBA_CD.Text;

                this.genbaEntity = daoGenba.GetDataByCd(condition);
                this.form.GENBA_RNAME.Text = this.genbaEntity == null ? string.Empty : genbaEntity.GENBA_NAME_RYAKU;
            }

            //前回業者CDを設定
            this.form.beforGyoushaCD = this.form.GYOUSHA_CD.Text;
        }

        #endregion

        #region 明細ダブルクリック
        /// <summary>
        /// ダブルクリック処理
        /// 明細にイベントが貼れなかったのでここに設置
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

        #region 外部連携現場入力画面起動処理

        /// <summary>
        /// 外部連携現場入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        internal void OpenWindow(WINDOW_TYPE windowType, bool newFlg = false)
        {
            LogUtility.DebugMethodStart(windowType, newFlg);

            // 表示されている行のPKを取得する
            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row == null)
            {
                //データ無しの場合は全くの新規画面を開く
                r_framework.FormManager.FormManager.OpenFormWithAuth("M693", WINDOW_TYPE.NEW_WINDOW_FLAG, windowType);
            }
            else
            {
                // 引数へのオブジェクトを作成する
                // 新規の場合は引数なし、ただし参照の場合は引数あり
                string cd1 = row.Cells[ConstCls.KEY_ID1].Value.ToString();
                string cd2 = row.Cells[ConstCls.KEY_ID2].Value.ToString();

                if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG && newFlg)
                {
                    //新規
                    if (row.Cells[ConstCls.HIDDEN_POINT_ID].Value.ToString() == string.Empty)
                    {
                        //外部連携現場未入力の場合は業者CDと現場CDを引数として飛ばす
                        r_framework.FormManager.FormManager.OpenFormWithAuth("M693", WINDOW_TYPE.NEW_WINDOW_FLAG, windowType, cd1, cd2);
                    }
                    else
                    {
                        //既に外部連携現場入力済み(連携有無ではなく入力済みかどうか)の場合
                        //業者現場のコードは渡さず全くの新規画面を開く
                        r_framework.FormManager.FormManager.OpenFormWithAuth("M693", WINDOW_TYPE.NEW_WINDOW_FLAG, windowType);
                    }
                }
                else
                {
                    if (row.Cells[ConstCls.HIDDEN_POINT_ID].Value.ToString() == string.Empty)
                    {
                        //外部連携現場未入力の場合は業者CDと現場CDを引数として飛ばす
                        r_framework.FormManager.FormManager.OpenFormWithAuth("M693", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, cd1, cd2);
                        return;
                    }

                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                        !r_framework.Authority.Manager.CheckAuthority("M693", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        if (!r_framework.Authority.Manager.CheckAuthority("M693", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            this.msgLogic.MessageBoxShow("E158", "修正");
                            return;
                        }

                        windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }

                    r_framework.FormManager.FormManager.OpenFormWithAuth("M693", windowType, windowType, cd1, cd2);
                }
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

        #region 検索処理

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
                this.form.Table = this.daoGenba.GetDateForStringSql(sql);
            }

            this.form.ShowData();

            return this.form.Table != null ? this.form.Table.Rows.Count : 0;
        }
        #endregion

        #region SQL生成
        /// <summary>
        /// 検索文字列を作成
        /// </summary>
        private string MakeSearchCondition()
        {
            var selectQuery = this.CreateSelectQuery();
            var fromQuery = this.CreateFromQuery();
            var whereQuery = this.CreateWhereQuery();
            var orderByQuery = this.CreateOrderByQuery();

            return selectQuery + fromQuery + whereQuery + orderByQuery;
        }

        #region SELECT
        /// <summary>
        /// Select句作成
        /// </summary>
        /// <returns></returns>
        private string CreateSelectQuery()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                //表示用項目
                sb.Append("SELECT DISTINCT ");
                sb.AppendFormat(" MG.GYOUSHA_CD AS {0}, ", ConstCls.KEY_ID1);
                sb.AppendFormat(" MG.GENBA_CD AS {0}, ", ConstCls.KEY_ID2);

                // 連携状況
                sb.Append(" CASE WHEN MDOG.OUTPUT_DATE IS NULL THEN '未' ELSE '済' END 連携状況, ");

                //マスタ差分
                sb.Append(" CASE WHEN MDOG.OUTPUT_DATE IS NULL THEN '―'");
                sb.Append("      WHEN MG.UPDATE_DATE > MDOG.OUTPUT_DATE THEN '有'");
                sb.Append("      ELSE '―' END マスタ差分, ");

                //パターン一覧からの表示列
                sb.Append(this.form.SelectQuery);

                //以下、非表示項目
                //RequestBody作成用に、固定で非表示抽出させたい
                sb.AppendFormat(" ,MGD.POINT_ID AS {0}", ConstCls.HIDDEN_POINT_ID);
                sb.AppendFormat(" ,MGD.POINT_NAME AS {0}", ConstCls.HIDDEN_POINT_NAME);
                sb.AppendFormat(" ,MGD.POINT_KANA_NAME AS {0}", ConstCls.HIDDEN_POINT_KANA_NAME);
                sb.AppendFormat(" ,MGD.MAP_NAME AS {0}", ConstCls.HIDDEN_MAP_NAME);
                sb.AppendFormat(" ,MGD.POST_CODE AS {0}", ConstCls.HIDDEN_POST_CODE);
                sb.AppendFormat(" ,MGTO.TODOUFUKEN_NAME AS {0}", ConstCls.HIDDEN_PREFECTURES);
                sb.AppendFormat(" ,MGD.ADDRESS1 AS {0}", ConstCls.HIDDEN_ADDRESS1);
                sb.AppendFormat(" ,MGD.ADDRESS2 AS {0}", ConstCls.HIDDEN_ADDRESS2);
                sb.AppendFormat(" ,MGD.TEL_NO AS {0}", ConstCls.HIDDEN_TEL_NO);
                sb.AppendFormat(" ,MGD.FAX_NO AS {0}", ConstCls.HIDDEN_FAX_NO);
                sb.AppendFormat(" ,MGD.CONTACT_NAME AS {0}", ConstCls.HIDDEN_CONTACT_NAME);
                sb.AppendFormat(" ,MGD.MAIL_ADDRESS AS {0}", ConstCls.HIDDEN_MAIL_ADDRESS);
                sb.AppendFormat(" ,MGD.RANGE_RADIUS AS {0}", ConstCls.HIDDEN_RANGE_RADIUS);
                sb.AppendFormat(" ,MGD.REMARKS AS {0}", ConstCls.HIDDEN_REMARKS);

            }
            return sb.ToString();
        }
        #endregion
        #region FROM
        /// <summary>
        /// From句作成
        /// </summary>
        /// <returns></returns>
        private string CreateFromQuery()
        {
            var sb = new StringBuilder();

            // 現場マスタ
            sb.Append(" FROM M_GENBA MG ");

            // 都道府県マスタ(現場)
            sb.Append(" LEFT JOIN M_TODOUFUKEN MTO ");
            sb.Append(" ON MG.GENBA_TODOUFUKEN_CD = MTO.TODOUFUKEN_CD ");

            // 取引先マスタ
            sb.Append(" LEFT JOIN M_TORIHIKISAKI MT ");
            sb.Append(" ON MG.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD ");

            // 現場マスタ(外部連携用)
            sb.Append(" LEFT JOIN M_GENBA_DIGI MGD ");
            sb.Append(" ON MG.GYOUSHA_CD = MGD.GYOUSHA_CD ");
            sb.Append(" AND MG.GENBA_CD = MGD.GENBA_CD ");

            // 現場タイムスタンプテーブル
            sb.Append(" LEFT JOIN M_DIGI_OUTPUT_GENBA MDOG ");
            sb.Append(" ON MGD.GYOUSHA_CD = MDOG.GYOUSHA_CD ");
            sb.Append(" AND MGD.GENBA_CD = MDOG.GENBA_CD ");

            // 業者マスタ
            sb.Append(" LEFT JOIN M_GYOUSHA MGYO ");
            sb.Append(" ON MG.GYOUSHA_CD = MGYO.GYOUSHA_CD ");

            // 都道府県マスタ(外部連携現場)
            sb.Append(" LEFT JOIN M_TODOUFUKEN MGTO ");
            sb.Append(" ON MGD.GENBA_TODOUFUKEN_CD = MGTO.TODOUFUKEN_CD ");

            // パターンから作成したJOIN句
            sb.Append(this.form.JoinQuery);

            return sb.ToString();
        }
        #endregion
        #region WHERE
        /// <summary>
        /// 検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        private string CreateWhereQuery()
        {
            var result = new StringBuilder();
            string strTemp;

            result.Append(" 0 = 0 ");

            //日付
            if (this.headerForm.radbtnDenpyouHiduke.Checked)
            {
                if (this.headerForm.HIDUKE_FROM.Value != null || this.headerForm.HIDUKE_TO.Value != null)
                {
                    result.Append(" AND ");
                }

                // 日時は日付のみにしてから変換
                if (this.headerForm.HIDUKE_FROM.Value != null)
                {
                    result.Append(" CONVERT(DATETIME, CONVERT(nvarchar, MG.CREATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(nvarchar, '");
                    result.Append(DateTime.Parse(this.headerForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }

                if (this.headerForm.HIDUKE_FROM.Value != null && this.headerForm.HIDUKE_TO.Value != null)
                {
                    result.Append(" AND ");
                }

                if (this.headerForm.HIDUKE_TO.Value != null)
                {
                    result.Append(" CONVERT(DATETIME, CONVERT(nvarchar, MG.CREATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(nvarchar, '");
                    result.Append(DateTime.Parse(this.headerForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
            }
            else if (this.headerForm.radbtnNyuuryokuHiduke.Checked)
            {
                if (this.headerForm.HIDUKE_FROM.Value != null || this.headerForm.HIDUKE_TO.Value != null)
                {
                    result.Append(" AND ");
                }

                if (this.headerForm.HIDUKE_FROM.Value != null)
                {
                    result.Append(" CONVERT(DATETIME, CONVERT(nvarchar, MG.UPDATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(nvarchar, '");
                    result.Append(DateTime.Parse(this.headerForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }

                if (this.headerForm.HIDUKE_FROM.Value != null && this.headerForm.HIDUKE_TO.Value != null)
                {
                    result.Append(" AND ");
                }

                if (this.headerForm.HIDUKE_TO.Value != null)
                {
                    result.Append(" CONVERT(DATETIME, CONVERT(nvarchar, MG.UPDATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(nvarchar, '");
                    result.Append(DateTime.Parse(this.headerForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
            }
            else if (this.headerForm.radbtnKenshuHiduke.Checked)
            {
                if (this.headerForm.HIDUKE_FROM.Value != null || this.headerForm.HIDUKE_TO.Value != null)
                {
                    result.Append(" AND ");
                }

                if (this.headerForm.HIDUKE_FROM.Value != null)
                {
                    result.Append(" CONVERT(DATETIME, CONVERT(nvarchar, MDOG.OUTPUT_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(nvarchar, '");
                    result.Append(DateTime.Parse(this.headerForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }

                if (this.headerForm.HIDUKE_FROM.Value != null && this.headerForm.HIDUKE_TO.Value != null)
                {
                    result.Append(" AND ");
                }

                if (this.headerForm.HIDUKE_TO.Value != null)
                {
                    result.Append(" CONVERT(DATETIME, CONVERT(nvarchar, MDOG.OUTPUT_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(nvarchar, '");
                    result.Append(DateTime.Parse(this.headerForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
            }

            // 連携状況
            strTemp = this.form.txtNum_RenkeiJyoukyou.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                // 4.全て は条件指定無し
                if (strTemp != "3")
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    switch (strTemp)
                    {
                        case "1":
                            // タイムスタンプテーブルにデータなし
                            result.Append(" MDOG.OUTPUT_DATE IS NULL ");
                            break;
                        case "2":
                            // タイムスタンプテーブルにデータあり
                            result.Append(" MDOG.OUTPUT_DATE IS NOT NULL ");
                            break;
                        case "3":
                            break;
                    }
                }
            }
            // 業者CD
            strTemp = this.form.GYOUSHA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.GYOUSHA_CD = '{0}'", strTemp);
            }
            // 現場CD
            strTemp = this.form.GENBA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.GENBA_CD = '{0}'", strTemp);
            }

            // フリガナ
            strTemp = this.form.GENBA_FURIGANA.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.GENBA_FURIGANA LIKE '%{0}%'", strTemp);
            }

            // 現場名1
            strTemp = this.form.GENBA_NAME1.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.GENBA_NAME1 LIKE '%{0}%'", strTemp);
            }

            // 現場名2
            strTemp = this.form.GENBA_NAME2.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.GENBA_NAME2 LIKE '%{0}%'", strTemp);
            }

            // 略称名
            strTemp = this.form.GENBA_NAME_RYAKU.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.GENBA_NAME_RYAKU LIKE '%{0}%'", strTemp);
            }
            // 外部地点カナ
            strTemp = this.form.DIGI_POINT_KANA_NAME.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MGD.POINT_KANA_NAME LIKE '%{0}%'", strTemp);
            }
            // 外部地点名
            strTemp = this.form.DIGI_POINT_NAME.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MGD.POINT_NAME LIKE '%{0}%'", strTemp);
            }
            // 外部地図表示名
            strTemp = this.form.DIGI_MAP_NAME.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MGD.MAP_NAME LIKE '%{0}%'", strTemp);
            }
            // 適用開始
            if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_BEGIN.Text))
            {
                strTemp = this.form.TEKIYOU_BEGIN.Value.ToString();
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.TEKIYOU_BEGIN >= CONVERT(date,'{0}')", strTemp);
                result.Append(" AND ");
                result.AppendFormat(" (MG.TEKIYOU_END >= CONVERT(date,'{0}') OR MG.TEKIYOU_END IS NULL)", strTemp);
            }

            // 適用終了
            if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_END.Text))
            {
                strTemp = this.form.TEKIYOU_END.Value.ToString();
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.TEKIYOU_BEGIN <= CONVERT(date,'{0}')", strTemp);
                result.Append(" AND ");
                result.AppendFormat(" (MG.TEKIYOU_END <= CONVERT(date,'{0}') OR MG.TEKIYOU_END IS NULL)", strTemp);
            }

            // 表示条件
            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" MG.DELETE_FLG = 0");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" (1 = 0");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked)
            {
                result.Append(" OR (((MG.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MG.TEKIYOU_END) or (MG.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and MG.TEKIYOU_END IS NULL) or (MG.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MG.TEKIYOU_END) or (MG.TEKIYOU_BEGIN IS NULL and MG.TEKIYOU_END IS NULL)) and MG.DELETE_FLG = 0)");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                result.Append(" OR MG.DELETE_FLG = 1");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                result.Append(" OR ((MG.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > MG.TEKIYOU_END) and MG.DELETE_FLG = 0)");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                result.Append(")");
            }

            return result.Length > 0 ? result.Insert(0, " WHERE ").ToString() : string.Empty;
        }
        #endregion
        #region ORDER BY
        /// <summary>
        /// OrderBy句作成
        /// </summary>
        /// <returns></returns>
        private string CreateOrderByQuery()
        {
            var query = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.form.OrderByQuery))
            {
                query += " ORDER BY " + this.form.OrderByQuery;
            }

            return query;
        }
        #endregion

        #endregion

        #region 表示条件
        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        internal void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();

            // 一覧表示イベントの削除
            this.RemoveIchiranHyoujiJoukenEvent();

            if (this.sysinfoEntity != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Value;
            }
            else
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = true;
            }

            // 一覧表示イベントの追加
            this.AddIchiranHyoujiJoukenEvent();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 表示条件を次回呼出時のデフォルト値として保存します
        /// </summary>
        internal bool SaveHyoujiJoukenDefault()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //追加項目はどうやって設定する…？
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT = this.form.GYOUSHA_CD.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT = this.form.GYOUSHA_RNAME.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_CD_TEXT = this.form.GENBA_CD.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_FURIGANA_TEXT = this.form.GENBA_FURIGANA.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_NAME1_TEXT = this.form.GENBA_NAME1.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_NAME2_TEXT = this.form.GENBA_NAME2.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.GENBA_NAME_RYAKU_TEXT = this.form.GENBA_NAME_RYAKU.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.POINT_KANA_NAME_TEXT = this.form.DIGI_POINT_KANA_NAME.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.POINT_NAME_TEXT = this.form.DIGI_POINT_NAME.Text;
                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.MAP_NAME_TEXT = this.form.DIGI_MAP_NAME.Text;

                DateTime dtTemp;
                if (DateTime.TryParse(this.form.TEKIYOU_BEGIN.Text, out dtTemp))
                {
                    GaibuRenkeiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = dtTemp;
                }
                else
                {
                    GaibuRenkeiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = new DateTime(0);
                }

                if (DateTime.TryParse(this.form.TEKIYOU_END.Text, out dtTemp))
                {
                    GaibuRenkeiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = dtTemp;
                }
                else
                {
                    GaibuRenkeiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = new DateTime(0);
                }
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

                GaibuRenkeiGenbaIchiran.Properties.Settings.Default.Save();
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveHyoujiJoukenDefault", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 一覧表示イベントの削除
        /// </summary>
        private void RemoveIchiranHyoujiJoukenEvent()
        {
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
        }

        /// <summary>
        /// 一覧表示イベントの追加
        /// </summary>
        private void AddIchiranHyoujiJoukenEvent()
        {
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
        }

        #endregion

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            this.form.TEKIYOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
            this.form.TEKIYOU_END.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.TEKIYOU_BEGIN.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.TEKIYOU_END.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.TEKIYOU_BEGIN.Text);
            DateTime date_to = DateTime.Parse(this.form.TEKIYOU_END.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.TEKIYOU_BEGIN.IsInputErrorOccured = true;
                this.form.TEKIYOU_END.IsInputErrorOccured = true;
                this.form.TEKIYOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                this.form.TEKIYOU_END.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "適用期間From", "適用期間To" };
                this.msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.TEKIYOU_BEGIN.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region 現場のチェック
        internal bool CheckGenba()
        {
            try
            {
                M_GENBA entity = new M_GENBA();
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                entity.GENBA_CD = this.form.GENBA_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.daoGenba.GetAllValidData(entity);
                if (genba != null && genba.Length > 0)
                {
                    this.form.GENBA_RNAME.Text = genba[0].GENBA_NAME_RYAKU;
                }
                else
                {
                    this.form.GENBA_RNAME.Text = string.Empty;
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }
        #endregion

        #region 明細内のチェックボックス関連の処理(特殊なのでここにまとめる)
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        internal void HeaderCheckBoxSupport()
        {

            LogUtility.DebugMethodStart();
            if (!this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
            {
                if (this.form.PatternNo != 0)
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                    newColumn.Name = ConstCls.CELL_CHECKBOX;
                    newColumn.HeaderText = "連携";
                    newColumn.Width = 70;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "連携   ";
                    newColumn.HeaderCell = newheader;
                    newColumn.MinimumWidth = 70;
                    newColumn.Resizable = DataGridViewTriState.False;

                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.form.customDataGridView1.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns.Add(newColumn);
                    }
                    this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// ヘッダーのチェックボックスクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.form.customDataGridView1.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                DataGridViewCheckBoxHeaderCell header = col.HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                }
            }
        }
        #endregion

        #region データ送信
        /// <summary>
        /// データの送信
        /// JSON絡みの処理
        /// </summary>
        /// <returns></returns>
        internal int JsonConnection(HTTP_METHOD ReqMethod)
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                // 事前に必要情報を取得
                var sysInfo = this.sysInfoDao.GetAllDataForCode("0");

                var logic = new LogiLogic();

                //DELETEで使用するURL用変数初期化
                var apiURL = string.Empty;

                //リクエスト関連情報取得
                var genbaConnect = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_POINTS);
                if (genbaConnect == null)
                {
                    this.msgLogic.MessageBoxShowError("S_LOGI_CONNECTのPoints情報なし！！");
                    ret_cnt = -1;
                    return ret_cnt;
                }

                //DGVに表示されているデータ
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    //DVG内最初のカラム(連携)がONになっているものが対象
                    if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].Value == true)
                    {

                        if (ReqMethod == HTTP_METHOD.PUT)
                        {
                            //現場PUT
                            var genbaDto = new ExternalCommon.DTO.Logicompass.INFO_POINT();
                            genbaDto.Company_Id = sysInfo.DIGI_CORP_ID;
                            genbaDto.Point_Id = dgvRow.Cells["POINT_ID_HIDDEN"].Value.ToString();
                            genbaDto.Point_Name = dgvRow.Cells["POINT_NAME_HIDDEN"].Value.ToString();
                            genbaDto.Point_Kana_Name = dgvRow.Cells["POINT_KANA_NAME_HIDDEN"].Value.ToString();
                            genbaDto.Map_Name = dgvRow.Cells["MAP_NAME_HIDDEN"].Value.ToString();
                            genbaDto.Post_Code = dgvRow.Cells["POST_CODE_HIDDEN"].Value.ToString();
                            genbaDto.Prefectures = dgvRow.Cells["PREFECTURES_HIDDEN"].Value.ToString();
                            genbaDto.Address1 = dgvRow.Cells["ADDRESS1_HIDDEN"].Value.ToString() + dgvRow.Cells["ADDRESS2_HIDDEN"].Value.ToString();
//                            genbaDto.Address2 = dgvRow.Cells["ADDRESS2_HIDDEN"].Value.ToString();
                            genbaDto.Tel_No = dgvRow.Cells["TEL_NO_HIDDEN"].Value.ToString();
                            genbaDto.Fax_No = dgvRow.Cells["FAX_NO_HIDDEN"].Value.ToString();
                            genbaDto.Contact_Name = dgvRow.Cells["CONTACT_NAME_HIDDEN"].Value.ToString();
                            genbaDto.Mail_Address = dgvRow.Cells["MAIL_ADDRESS_HIDDEN"].Value.ToString();
                            genbaDto.Range_Radius = int.Parse(dgvRow.Cells["RANGE_RADIUS_HIDDEN"].Value.ToString());
                            genbaDto.Remarks = dgvRow.Cells["REMARKS_HIDDEN"].Value.ToString();

                            logic.HttpPut(genbaConnect.URL, genbaConnect.CONTENT_TYPE, genbaDto);
                        }
                        else
                        {
                            //現場DELETE
                            apiURL = string.Format(genbaConnect.URL + LogiConst.API_PARAMETER_URL_POINTS, sysInfo.DIGI_CORP_ID, dgvRow.Cells["POINT_ID_HIDDEN"].Value);
                            logic.HttpDelete(apiURL, genbaConnect.CONTENT_TYPE);
                        }

                        //タイムスタンプテーブルに出力データを保存する
                        ret_cnt = this.TimeStampTableRegist(dgvRow);
                        if (ret_cnt < 0)
                        {
                            return ret_cnt;
                        }

                        //完了
                        ret_cnt++;
                    }
                }
            }
            catch (WebException)
            {
                //LogiLogic.cs側でエラー表示しているので何もしない
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                //エラーメッセージは各々出してるのでここでは表示しない
                LogUtility.Error("JsonConnection", ex);
                ret_cnt = -1;
            }

            LogUtility.DebugMethodEnd();

            return ret_cnt;

        }
        #endregion

        #region タイムスタンプテーブルにデータを保存

        [Transaction]
        private int TimeStampTableRegist(DataGridViewRow dgvRow)
        {
            int ret_cnt = 0;

            try
            {
                using (var tran = new TransactionUtility())
                {
                    string sql = "";
                    if (dgvRow.Cells["連携状況"].Value.ToString() == "未")
                    {
                        sql = string.Format("INSERT INTO M_DIGI_OUTPUT_GENBA VALUES ('{0}', '{1}', '{2}', GETDATE(), '{3}', {4})", dgvRow.Cells[ConstCls.KEY_ID1].Value, dgvRow.Cells[ConstCls.KEY_ID2].Value, SystemProperty.UserName, SystemInformation.ComputerName, 0);
                        this.daoGenbaTS.ExecuteForStringSql(sql);
                    }
                    else
                    {
                        sql = string.Format("UPDATE M_DIGI_OUTPUT_GENBA SET OUTPUT_USER = '{0}', OUTPUT_DATE = GETDATE(), OUTPUT_PC = '{1}' WHERE GYOUSHA_CD = '{2}' AND GENBA_CD = '{3}'", SystemProperty.UserName, SystemInformation.ComputerName, dgvRow.Cells[ConstCls.KEY_ID1].Value, dgvRow.Cells[ConstCls.KEY_ID2].Value);
                        this.daoGenbaTS.ExecuteForStringSql(sql);
                    }

                    tran.Commit();
                    ret_cnt = 1;
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex1)
            {
                //排他は警告
                LogUtility.Warn(ex1); //排他は警告
                this.msgLogic.MessageBoxShow("E080");
                ret_cnt = -1;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error(ex2); //その他SQLエラー
                this.msgLogic.MessageBoxShow("E093");
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex); //その他エラー
                this.msgLogic.MessageBoxShow("E245");
                ret_cnt = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret_cnt;
        }
        #endregion

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        internal void OutputCSV()
        {
            // M_SYS_INFOから会社IDを取得
            string corpId = this.sysinfoEntity.DIGI_CORP_ID;
            // M_GENBA_DIGIから全データ取得
            M_GENBA_DIGI[] genbaDataList = this.daoGenbaDigi.GetAllData();

            // 出力データを整形する。
            var dataList = new List<string>();
            List<M_TODOUFUKEN> todofukenDataList = this.daoTodoufuken.GetAllData().ToList();
            foreach (var genbaDigi in genbaDataList.ToList())
            {
                // M_GENBA_DIGIの都道府県CDをもとに、M_TODOUFUKENから名称を取得する。
                var todoufuken = todofukenDataList.Where(n => !n.TODOUFUKEN_CD.IsNull
                                                           && !genbaDigi.GENBA_TODOUFUKEN_CD.IsNull
                                                           && n.TODOUFUKEN_CD.Value == genbaDigi.GENBA_TODOUFUKEN_CD.Value)
                                                  .FirstOrDefault();
                string todoufukenName = "";
                if (todoufuken != null && !string.IsNullOrEmpty(todoufuken.TODOUFUKEN_NAME))
                {
                    todoufukenName = todoufuken.TODOUFUKEN_NAME;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(corpId)                                       // 会社ID
                    .AppendFormat(",{0}", genbaDigi.POINT_ID)           // 地点ID
                    .AppendFormat(",{0}", genbaDigi.POINT_NAME)         // 地点名
                    .AppendFormat(",{0}", genbaDigi.POINT_KANA_NAME)    // 地点カナ名
                    .AppendFormat(",{0}", genbaDigi.MAP_NAME)           // 地図表示名
                    .AppendFormat(",{0}", genbaDigi.POST_CODE)          // 郵便番号
                    .AppendFormat(",{0}", todoufukenName)               // 都道府県
                    .AppendFormat(",{0}", genbaDigi.ADDRESS1 + genbaDigi.ADDRESS2)           // 住所
                    .AppendFormat(",{0}", "")           // 住所その他
                    .AppendFormat(",{0}", genbaDigi.TEL_NO)             // TEL番号
                    .AppendFormat(",{0}", genbaDigi.FAX_NO)             // FAX番号
                    .AppendFormat(",{0}", genbaDigi.CONTACT_NAME)       // 担当者名
                    .AppendFormat(",{0}", genbaDigi.MAIL_ADDRESS)       // メールアドレス
                    .Append(",0")                                       // 緯度(0固定)
                    .Append(",0")                                       // 経度(0固定)
                    .Append(",0")                                       // 子会社公開フラグ(0固定)
                    .AppendFormat(",{0}", genbaDigi.RANGE_RADIUS)       // 地点範囲半径
                    .Append(",0")                                       // アイコン表示フラグ(0固定)
                    .Append(",0")                                       // アイコン積フラグ(0固定)
                    .Append(",0")                                       // ラベル表示フラグ(0固定)
                    .Append(",0")                                       // アイコンNo(0固定)
                    .AppendFormat(",{0}{1}{2}", 
                                    genbaDigi.UNTENSHA_SHIJI_JIKOU1,
                                    genbaDigi.UNTENSHA_SHIJI_JIKOU2,
                                    genbaDigi.UNTENSHA_SHIJI_JIKOU3)    // 備考(運転者指示事項1～3を結合したもの)
                    .Append(",0");                                      // 削除フラグ(0固定)

                dataList.Add(sb.ToString());
            }

            if (this.msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
            {
                if (new CsvUtility(dataList, this.form, ConstCls.RENKEI_CSV_NAME).Output())
                {
                    this.msgLogic.MessageBoxShowInformation("出力が完了しました。");
                }
            }
        }
        #endregion
    }
}
