// $Id: UIForm.cs 46639 2015-04-06 11:09:46Z takeda $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.FormManager;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.ElectronicManifest.MihimodukeIchiran.Const;
using Shougun.Core.ElectronicManifest.MihimodukeIchiran.Logic;
using r_framework.Utility;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.CustomControl;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ElectronicManifest.MihimodukeIchiran.APP
{
	public partial class UIForm : SuperForm
	{
		#region フィールド
		/// <summary>
		/// 未紐付一覧画面ロジック
		/// </summary>
		private LogicClass logic;

        /// <summary>
        /// 選択カラム名
        /// </summary>
        private string CellName { get; set; }

        /// <summary>
        /// 選択タイムスタンプ（業者）
        /// </summary>
        public int TimestampGyousha { get; set; }

        /// <summary>
        /// 選択タイムスタンプ（現場）
        /// </summary>
        public int TimestampGenba { get; set; }

        /// <summary>
        /// 業者エラーフラグ
        /// </summary>
        private bool isGyoushaError { get; set; }

        /// <summary>
        /// 現場エラーフラグ
        /// </summary>
        private bool isGenbaError { get; set; }

        /// <summary>
        /// 一覧紐付けデータ
        /// </summary>
        public DataTable IchiranData { get; set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string beforeGyoushaCd = string.Empty;
        private string beforeHstJigyoujouCd = string.Empty;
        private string beforeSbnJigyoushaCd = string.Empty;
        private string beforeLastSbnJigyoushaCd = string.Empty;

        #region コントロール
        /// <summary>検索条件：データ作成</summary>
        public Label DataConditionLabel { get; set; }
        public CustomNumericTextBox2 DataConditionValue { get; set; }
        public CustomRadioButton DataConditionKbn1 { get; set; }
        public CustomRadioButton DataConditionKbn2 { get; set; }
        public CustomRadioButton DataConditionKbn3 { get; set; }
        public CustomPanel DataCondPanel { get; set; }

        /// <summary>検索条件：排出事業者</summary>
        public Label HstJigyoushaLabel { get; set; }
        public CustomAlphaNumTextBox HstJigyoushaCd { get; set; }
        public CustomTextBox HstJigyoushaName { get; set; }
        public CustomPopupOpenButton HstJigyoushaSearch { get; set; }

        /// <summary>検索条件：排出事業場</summary>
        public Label HstJigyoujouLabel { get; set; }
        public CustomAlphaNumTextBox HstJigyoujouCd { get; set; }
        public CustomTextBox HstJigyoujouName { get; set; }
        public CustomPopupOpenButton HstJigyoujouSearch { get; set; }

        /// <summary>検索条件：収集運搬事業者</summary>
        public Label UpnJigyoushaLabel { get; set; }
        public CustomAlphaNumTextBox UpnJigyoushaCd { get; set; }
        public CustomTextBox UpnJigyoushaName { get; set; }
        public CustomPopupOpenButton UpnJigyoushaSearch { get; set; }

        /// <summary>検索条件：処分事業者</summary>
        public Label SbnJigyoushaLabel { get; set; }
        public CustomAlphaNumTextBox SbnJigyoushaCd { get; set; }
        public CustomTextBox SbnJigyoushaName { get; set; }
        public CustomPopupOpenButton SbnJigyoushaSearch { get; set; }

        /// <summary>検索条件：処分事業場</summary>
        public Label SbnJigyoujouLabel { get; set; }
        public CustomAlphaNumTextBox SbnJigyoujouCd { get; set; }
        public CustomTextBox SbnJigyoujouName { get; set; }
        public CustomPopupOpenButton SbnJigyoujouSearch { get; set; }

        /// <summary>検索条件：最終処分事業者</summary>
        public Label LastSbnJigyoushaLabel { get; set; }
        public CustomAlphaNumTextBox LastSbnJigyoushaCd { get; set; }
        public CustomTextBox LastSbnJigyoushaName { get; set; }
        public CustomPopupOpenButton LastSbnJigyoushaSearch { get; set; }

        /// <summary>検索条件：最終処分事業場</summary>
        public Label LastSbnJigyoujouLabel { get; set; }
        public CustomAlphaNumTextBox LastSbnJigyoujouCd { get; set; }
        public CustomTextBox LastSbnJigyoujouName { get; set; }
        public CustomPopupOpenButton LastSbnJigyoujouSearch { get; set; }

        /// <summary>検索条件：マスタ設定</summary>
        public Label MasterSettingConditionLabel { get; set; }
        public CustomNumericTextBox2 MasterSettingConditionValue { get; set; }
        public CustomRadioButton MasterSettingConditionKbn1 { get; set; }
        public CustomRadioButton MasterSettingConditionKbn2 { get; set; }
        public CustomRadioButton MasterSettingConditionKbn3 { get; set; }
        public CustomPanel MasterSettingCondPanel { get; set; }

        #endregion

        #endregion

        /// <summary>
		/// コンストラクタ
		/// </summary>
		public UIForm()
			: base(WINDOW_ID.T_MIHIMODUKE_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
		{
			// コンポーネントの初期化
			this.InitializeComponent();

			// 画面タイプなど引数値は変更となるが基本的にやることは変わらない
			this.logic = new LogicClass(this);

            // 画面レイアウト変更
            this.logic.ChangeLayout();

			// 完全に固定。ここには変更を入れない
			QuillInjector.GetInstance().Inject(this);
		}

		/// <summary>
		/// 画面Load処理
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{
			// 親クラスのロード
			base.OnLoad(e);

			// 画面の初期化
            if (!this.logic.WindowInit())
            {
                return;
            }
            // 業者コードの設定
            this.GyoushaCdSet(false);
            // 現場コードの設定
            this.GenbaCdSet();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }

		/// <summary>
		/// 検索
		/// </summary>
		public void Search(object sender, EventArgs e)
		{
            /// 20141021 Houkakou 「補助データ」の日付チェックを追加する　start
            if (this.logic.DateCheck() || !this.logic.maniIdIntegrityCheck()
                || !this.logic.DataConditionCheck() || !this.logic.MasterSettingCondCheck())
            {
                return;
            }
            /// 20141021 Houkakou 「補助データ」の日付チェックを追加する　end
			// 検索処理
            if (this.logic.Search() == -1)
            {
                return;
            }

            this.ClearDetailFooter();
            // 業者コードの設定
            this.GyoushaCdSet(false);
            // 現場コードの設定
            this.GenbaCdSet();
		}

		/// <summary>
		/// 登録
		/// </summary>
		public void Regist(object sender, EventArgs e)
		{
            MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

            if (string.Empty.Equals(this.CellName))
            {
                return;
            }

            // 選択先が｢排出事業者｣｢収集運搬業者N｣｢運搬先業者N｣｢処分業者｣の場合
            if (ConstCls.HAISHUTSU_JIGYOUSHA.Equals(this.CellName)
                || ConstCls.SHUSHU_UNPAN_GYOUSHAN.Equals(this.CellName)
                || ConstCls.UNPANSAKI_GYOUSHAN.Equals(this.CellName)
                || ConstCls.SHOBUN_GYOUSHA.Equals(this.CellName))
            {
                // 一覧のカーソル指定先が紐付けがある場合
                if (this.cantxt_GyoushaCd.ReadOnly == true
                    && !string.IsNullOrEmpty(this.cantxt_GyoushaCd.Text))
                {
                    // エラーメッセージ表示
                    messageShowLogic.MessageBoxShow("E022", "業者CD");
                    return;
                }
                // 一覧のカーソル指定先が未紐付けの場合
                else
                {
                    // 【事業者CD】が未設定の場合
                    if (string.IsNullOrEmpty(this.cantxt_JigyoushaCd.Text))
                    {
                        // エラーメッセージ表示
                        messageShowLogic.MessageBoxShow("E046", "マスタ情報の事業者CDが未登録", "将軍連携マスタの業者CD");
                        return;
                    }
                    // 【業者CD】が未設定の場合
                    else if (string.IsNullOrEmpty(this.cantxt_GyoushaCd.Text))
                    {
                        // エラーメッセージ表示
                        messageShowLogic.MessageBoxShow("E033", "業者CD");
                        this.cantxt_GyoushaCd.Focus();
                        return;
                    }
                    else
                    {
                        DialogResult result = messageShowLogic.MessageBoxShow("C046", "登録");
                        if (result == DialogResult.No)
                        {
                            return;
                        }

                        Cursor.Current = Cursors.WaitCursor;
                        // 登録処理
                        this.logic.Regist(false);
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            // 選択先が｢排出事業場｣｢運搬先事業場N｣｢処分事業場｣の場合
            else if (ConstCls.HAISHUTSU_JIGYOUJOU.Equals(this.CellName)
                || ConstCls.UNPANSAKI_JIGYOUJOUN.Equals(this.CellName)
                || ConstCls.SHOBUN_JIGYOUJOU.Equals(this.CellName))
            {
                // 一覧のカーソル指定先が紐付けがある場合
                if (this.cantxt_GenbaCd.ReadOnly == true
                    && !string.IsNullOrEmpty(this.cantxt_GenbaCd.Text))
                {
                    // エラーメッセージ表示
                    messageShowLogic.MessageBoxShow("E022", "現場CD");
                    return;
                }
                // 【事業者CD】が未設定の場合
                else if (string.IsNullOrEmpty(this.cantxt_JigyoushaCd.Text))
                {
                    // エラーメッセージ表示
                    messageShowLogic.MessageBoxShow("E046", "マスタ情報の事業者CDが未登録", "将軍連携マスタの業者CD");
                    return;
                }
                // 【事業場CD】が未設定の場合
                else if (string.IsNullOrEmpty(this.cantxt_JigyoujouCd.Text))
                {
                    // エラーメッセージ表示
                    messageShowLogic.MessageBoxShow("E046", "マスタ情報の事業場CDが未登録", "将軍連携マスタの現場CD");
                    return;
                }
                // 【業者CD】が未設定の場合
                else if (string.IsNullOrEmpty(this.cantxt_GyoushaCd.Text))
                {
                    // エラーメッセージ表示
                    messageShowLogic.MessageBoxShow("E033", "業者CD");
                    this.cantxt_GyoushaCd.Focus();
                    return;
                }
                // 【現場CD】が未設定の場合
                else if (string.IsNullOrEmpty(this.cantxt_GenbaCd.Text))
                {
                    // エラーメッセージ表示
                    messageShowLogic.MessageBoxShow("E033", "現場CD");
                    this.cantxt_GenbaCd.Focus();
                    return;
                }
                else
                {
                    DialogResult result = messageShowLogic.MessageBoxShow("C046", "登録");
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    Cursor.Current = Cursors.WaitCursor;
                    // 登録処理
                    this.logic.Regist(false);
                    Cursor.Current = Cursors.Default;
                }
            }
		}

		/// <summary>
		/// Formクローズ処理
		/// </summary>
        public void FormClose(object sender, EventArgs e)
		{
			// Formクローズ
			var parentForm = (BusinessBaseForm)this.Parent;

			this.Close();
			parentForm.Close();
		}

        /// <summary>
        /// 業者登録処理
        /// </summary>
        public void GyoushaTouroku(object sender, EventArgs e)
        {
            this.GyoushaTouroku();
        }

        /// <summary>
        /// 業者登録処理
        /// </summary>
        private void GyoushaTouroku()
        {
            FormManager.OpenFormWithAuth("M215", WINDOW_TYPE.NEW_WINDOW_FLAG);
        }

        /// <summary>
        /// 現場登録処理
        /// </summary>
        public void GenbaTouroku(object sender, EventArgs e)
        {
            this.GenbaTouroku();
        }

        /// <summary>
        /// 現場登録処理
        /// </summary>
        private void GenbaTouroku()
        {
            FormManager.OpenFormWithAuth("M217", WINDOW_TYPE.NEW_WINDOW_FLAG);
        }

        /// <summary>
        /// 事業者登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void JigyoushaToroku(object sender, EventArgs e)
        {
            this.logic.JigyoushaToroku();
        }

        /// <summary>
        /// 事業場登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void JigyoujouToroku(object sender, EventArgs e)
        {
            this.logic.JigyoujouToroku();
        }

        /// <summary>
        /// データ作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExDataSakusei(object sender, EventArgs e)
        {
            this.logic.ExDataSakusei();
        }

        /// <summary>
        /// 一覧のcellフォーカス
        /// </summary>
        private void Ichiran_CellEnter(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            // ヘッダが選択される場合
            if (rowIndex < 0)
            {
                // DetailFooterクリア
                ClearDetailFooter();
                // 業者コードの設定
                this.GyoushaCdSet(false);
                // 現場コードの設定
                this.GenbaCdSet();
                return;
            }

            string cellName = e.CellName;

            if (this.Ichiran.Rows[rowIndex].Cells[cellName].Value != null)
            {
                SetSentakuData(rowIndex, cellName);
            }
            else
            {
                // DetailFooterクリア
                ClearDetailFooter();
            }
        }

        /// <summary>
        /// 選択データの設定
        /// </summary>
        private bool SetSentakuData(int rowIndex, string cellName)
        {
            bool ret = true;
            try
            {
                bool isPossibleInput = false;

                // 引渡し日
                if ("HIKIWATASHI_DATE".Equals(cellName))
                {
                    this.CellName = string.Empty;
                    // DetailFooterクリア
                    ClearDetailFooter();
                }
                // 排出事業者
                else if ("HAISHUTSU_JIGYOUSHA".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "排出事業者";

                    // 排出事業者
                    this.CellName = ConstCls.HAISHUTSU_JIGYOUSHA;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_HS_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_HS_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 排出事業場
                else if ("HAISHUTSU_JIGYOUJOU".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "排出事業場";

                    // 排出事業場
                    this.CellName = ConstCls.HAISHUTSU_JIGYOUJOU;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_HS_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_HS_TIME_STAMP"]);
                    }
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_HS_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGenba = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_HS_TIME_STAMP"]);
                    }

                    #region 業者情報を取得
                    // 業者情報を取得(SQLファイルで取得しようとすると、メンテナンスが大変になるためUIForm内で取得する)
                    M_DENSHI_JIGYOUJOU denshiJigyoujou = new M_DENSHI_JIGYOUJOU();
                    M_GYOUSHA gyousha = null;

                    // 事業場情報取得
                    var denshiJigyoujouDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    var jigyoujouFilter = new M_DENSHI_JIGYOUJOU();
                    if (!string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["EDI_MEMBER_ID"].ToString())
                        && !string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_CD"].ToString()))
                    {
                        jigyoujouFilter.EDI_MEMBER_ID = this.IchiranData.Rows[rowIndex]["EDI_MEMBER_ID"].ToString();
                        jigyoujouFilter.JIGYOUJOU_CD = this.IchiranData.Rows[rowIndex]["JIGYOUJOU_CD"].ToString();
                        var denshiJigyoujous = denshiJigyoujouDao.GetAllValidData(jigyoujouFilter);
                        if (denshiJigyoujous != null && denshiJigyoujous.Count() > 0)
                        {
                            denshiJigyoujou = denshiJigyoujous[0];

                            // 業者情報取得
                            if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                            {
                                var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                                gyousha = gyoushaDao.GetDataByCd(denshiJigyoujou.GYOUSHA_CD);
                            }
                        }
                    }
                    #endregion

                    // 登録しいる電子事業場があればそちらを優先して表示
                    if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = denshiJigyoujou.GYOUSHA_CD;
                        this.ctxt_GyoushaName.Text = gyousha != null ? gyousha.GYOUSHA_NAME_RYAKU : string.Empty;
                    }
                    else
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["GYOUSHA_CD"].ToString();
                        // 業者名
                        this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["GYOUSHA_NAME_RYAKU"].ToString();

                        isPossibleInput = true;
                    }
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = this.IchiranData.Rows[rowIndex]["GENBA_CD"].ToString();
                    // 現場名
                    this.ctxt_GenbaName.Text = this.IchiranData.Rows[rowIndex]["GENBA_NAME_RYAKU"].ToString();
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = this.IchiranData.Rows[rowIndex]["JIGYOUJOU_CD"].ToString();
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = this.IchiranData.Rows[rowIndex]["JIGYOUJOU_NAME"].ToString();
                }
                // 収集運搬業者1
                else if ("SHUSHU_UNPAN_GYOUSHA1".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "収集運搬業者(区間1)";

                    // 収集運搬業者N
                    this.CellName = ConstCls.SHUSHU_UNPAN_GYOUSHAN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU1_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU1_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU1_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU1_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU1_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU1_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 運搬先業者1
                else if ("UNPANSAKI_GYOUSHA1".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "運搬先業者(区間1)";

                    // 運搬先業者N
                    this.CellName = ConstCls.UNPANSAKI_GYOUSHAN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US1_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US1_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US1_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["US1_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US1_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["US1_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 運搬先事業場1
                else if ("UNPANSAKI_JIGYOUJOU1".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "運搬先(区間1)";

                    // 運搬先事業場N
                    this.CellName = ConstCls.UNPANSAKI_JIGYOUJOUN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US1_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US1_TIME_STAMP"]);
                    }
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_US1_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGenba = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_US1_TIME_STAMP"]);
                    }

                    #region 業者情報を取得
                    // 業者情報を取得(SQLファイルで取得しようとすると、メンテナンスが大変になるためUIForm内で取得する)
                    M_DENSHI_JIGYOUJOU denshiJigyoujou = new M_DENSHI_JIGYOUJOU();
                    M_GYOUSHA gyousha = null;

                    // 事業場情報取得
                    var denshiJigyoujouDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    var jigyoujouFilter = new M_DENSHI_JIGYOUJOU();
                    if (!string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["US1_EDI_MEMBER_ID"].ToString())
                        && !string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["US1_JIGYOUJOU_CD"].ToString()))
                    {
                        jigyoujouFilter.EDI_MEMBER_ID = this.IchiranData.Rows[rowIndex]["US1_EDI_MEMBER_ID"].ToString();
                        jigyoujouFilter.JIGYOUJOU_CD = this.IchiranData.Rows[rowIndex]["US1_JIGYOUJOU_CD"].ToString();
                        var denshiJigyoujous = denshiJigyoujouDao.GetAllValidData(jigyoujouFilter);
                        if (denshiJigyoujous != null && denshiJigyoujous.Count() > 0)
                        {
                            denshiJigyoujou = denshiJigyoujous[0];

                            // 業者情報取得
                            if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                            {
                                var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                                gyousha = gyoushaDao.GetDataByCd(denshiJigyoujou.GYOUSHA_CD);
                            }
                        }
                    }
                    #endregion

                    // 登録しいる電子事業場があればそちらを優先して表示
                    if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = denshiJigyoujou.GYOUSHA_CD;
                        // 業者名
                        this.ctxt_GyoushaName.Text = gyousha != null ? gyousha.GYOUSHA_NAME_RYAKU : string.Empty;
                    }
                    else
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US1_GYOUSHA_CD"].ToString();
                        // 業者名
                        this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["US1_GYOUSHA_NAME_RYAKU"].ToString();

                        isPossibleInput = true;
                    }
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US1_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["US1_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = this.IchiranData.Rows[rowIndex]["US1_GENBA_CD"].ToString();
                    // 現場名
                    this.ctxt_GenbaName.Text = this.IchiranData.Rows[rowIndex]["US1_GENBA_NAME_RYAKU"].ToString();
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = this.IchiranData.Rows[rowIndex]["US1_JIGYOUJOU_CD"].ToString();
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = this.IchiranData.Rows[rowIndex]["US1_JIGYOUJOU_NAME"].ToString();
                }
                // 収集運搬業者2
                else if ("SHUSHU_UNPAN_GYOUSHA2".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "収集運搬業者(区間2)";

                    // 収集運搬業者N
                    this.CellName = ConstCls.SHUSHU_UNPAN_GYOUSHAN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU2_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU2_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU2_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU2_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU2_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU2_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 運搬先業者2
                else if ("UNPANSAKI_GYOUSHA2".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "運搬先業者(区間2)";

                    // 運搬先業者N
                    this.CellName = ConstCls.UNPANSAKI_GYOUSHAN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US2_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US2_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US2_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["US2_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US2_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["US2_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 運搬先事業場2
                else if ("UNPANSAKI_JIGYOUJOU2".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "運搬先(区間2)";

                    // 運搬先事業場N
                    this.CellName = ConstCls.UNPANSAKI_JIGYOUJOUN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US2_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US2_TIME_STAMP"]);
                    }
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_US2_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGenba = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_US2_TIME_STAMP"]);
                    }

                    #region 業者情報を取得
                    // 業者情報を取得(SQLファイルで取得しようとすると、メンテナンスが大変になるためUIForm内で取得する)
                    M_DENSHI_JIGYOUJOU denshiJigyoujou = new M_DENSHI_JIGYOUJOU();
                    M_GYOUSHA gyousha = null;

                    // 事業場情報取得
                    var denshiJigyoujouDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    var jigyoujouFilter = new M_DENSHI_JIGYOUJOU();
                    if (!string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["US2_EDI_MEMBER_ID"].ToString())
                        && !string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["US2_JIGYOUJOU_CD"].ToString()))
                    {
                        jigyoujouFilter.EDI_MEMBER_ID = this.IchiranData.Rows[rowIndex]["US2_EDI_MEMBER_ID"].ToString();
                        jigyoujouFilter.JIGYOUJOU_CD = this.IchiranData.Rows[rowIndex]["US2_JIGYOUJOU_CD"].ToString();
                        var denshiJigyoujous = denshiJigyoujouDao.GetAllValidData(jigyoujouFilter);
                        if (denshiJigyoujous != null && denshiJigyoujous.Count() > 0)
                        {
                            denshiJigyoujou = denshiJigyoujous[0];

                            // 業者情報取得
                            if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                            {
                                var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                                gyousha = gyoushaDao.GetDataByCd(denshiJigyoujou.GYOUSHA_CD);
                            }
                        }
                    }
                    #endregion

                    // 登録しいる電子事業場があればそちらを優先して表示
                    if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = denshiJigyoujou.GYOUSHA_CD;
                        // 業者名
                        this.ctxt_GyoushaName.Text = gyousha != null ? gyousha.GYOUSHA_NAME_RYAKU : string.Empty;
                    }
                    else
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US2_GYOUSHA_CD"].ToString();
                        // 業者名
                        this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["US2_GYOUSHA_NAME_RYAKU"].ToString();

                        isPossibleInput = true;
                    }
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US2_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["US2_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = this.IchiranData.Rows[rowIndex]["US2_GENBA_CD"].ToString();
                    // 現場名
                    this.ctxt_GenbaName.Text = this.IchiranData.Rows[rowIndex]["US2_GENBA_NAME_RYAKU"].ToString();
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = this.IchiranData.Rows[rowIndex]["US2_JIGYOUJOU_CD"].ToString();
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = this.IchiranData.Rows[rowIndex]["US2_JIGYOUJOU_NAME"].ToString();
                }
                // ブランク
                else if ("BLANK".Equals(cellName))
                {
                    this.CellName = string.Empty;
                    // DetailFooterクリア
                    this.ClearDetailFooter();
                }
                // マニフェスト番号
                else if ("MANIFEST_ID".Equals(cellName))
                {
                    this.CellName = string.Empty;
                    // DetailFooterクリア
                    this.ClearDetailFooter();
                }
                // 収集運搬業者3
                else if ("SHUSHU_UNPAN_GYOUSHA3".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "収集運搬業者(区間3)";

                    // 収集運搬業者N
                    this.CellName = ConstCls.SHUSHU_UNPAN_GYOUSHAN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU3_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU3_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU3_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU3_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU3_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU3_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 運搬先業者3
                else if ("UNPANSAKI_GYOUSHA3".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "運搬先業者(区間3)";

                    // 運搬先業者N
                    this.CellName = ConstCls.UNPANSAKI_GYOUSHAN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US3_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US3_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US3_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["US3_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US3_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["US3_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 運搬先事業場3
                else if ("UNPANSAKI_JIGYOUJOU3".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "運搬先(区間3)";

                    // 運搬先事業場N
                    this.CellName = ConstCls.UNPANSAKI_JIGYOUJOUN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US3_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US3_TIME_STAMP"]);
                    }
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_US3_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGenba = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_US3_TIME_STAMP"]);
                    }

                    #region 業者情報を取得
                    // 業者情報を取得(SQLファイルで取得しようとすると、メンテナンスが大変になるためUIForm内で取得する)
                    M_DENSHI_JIGYOUJOU denshiJigyoujou = new M_DENSHI_JIGYOUJOU();
                    M_GYOUSHA gyousha = null;

                    // 事業場情報取得
                    var denshiJigyoujouDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    var jigyoujouFilter = new M_DENSHI_JIGYOUJOU();
                    if (!string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["US3_EDI_MEMBER_ID"].ToString())
                        && !string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["US3_JIGYOUJOU_CD"].ToString()))
                    {
                        jigyoujouFilter.EDI_MEMBER_ID = this.IchiranData.Rows[rowIndex]["US3_EDI_MEMBER_ID"].ToString();
                        jigyoujouFilter.JIGYOUJOU_CD = this.IchiranData.Rows[rowIndex]["US3_JIGYOUJOU_CD"].ToString();
                        var denshiJigyoujous = denshiJigyoujouDao.GetAllValidData(jigyoujouFilter);
                        if (denshiJigyoujous != null && denshiJigyoujous.Count() > 0)
                        {
                            denshiJigyoujou = denshiJigyoujous[0];

                            // 業者情報取得
                            if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                            {
                                var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                                gyousha = gyoushaDao.GetDataByCd(denshiJigyoujou.GYOUSHA_CD);
                            }
                        }
                    }
                    #endregion

                    // 登録しいる電子事業場があればそちらを優先して表示
                    if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = denshiJigyoujou.GYOUSHA_CD;
                        // 業者名
                        this.ctxt_GyoushaName.Text = gyousha != null ? gyousha.GYOUSHA_NAME_RYAKU : string.Empty;
                    }
                    else
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US3_GYOUSHA_CD"].ToString();
                        // 業者名
                        this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["US3_GYOUSHA_NAME_RYAKU"].ToString();

                        isPossibleInput = true;
                    }
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US3_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["US3_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = this.IchiranData.Rows[rowIndex]["US3_GENBA_CD"].ToString();
                    // 現場名
                    this.ctxt_GenbaName.Text = this.IchiranData.Rows[rowIndex]["US3_GENBA_NAME_RYAKU"].ToString();
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = this.IchiranData.Rows[rowIndex]["US3_JIGYOUJOU_CD"].ToString();
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = this.IchiranData.Rows[rowIndex]["US3_JIGYOUJOU_NAME"].ToString();
                }
                // 収集運搬業者4
                else if ("SHUSHU_UNPAN_GYOUSHA4".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "収集運搬業者(区間4)";

                    // 収集運搬業者N
                    this.CellName = ConstCls.SHUSHU_UNPAN_GYOUSHAN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU4_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU4_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU4_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU4_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU4_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU4_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 運搬先業者4
                else if ("UNPANSAKI_GYOUSHA4".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "運搬先業者(区間4)";

                    // 運搬先業者N
                    this.CellName = ConstCls.UNPANSAKI_GYOUSHAN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US4_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US4_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US4_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["US4_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US4_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["US4_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 運搬先事業場4
                else if ("UNPANSAKI_JIGYOUJOU4".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "運搬先(区間4)";

                    // 運搬先事業場N
                    this.CellName = ConstCls.UNPANSAKI_JIGYOUJOUN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US4_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_US4_TIME_STAMP"]);
                    }
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_US4_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGenba = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_US4_TIME_STAMP"]);
                    }

                    #region 業者情報を取得
                    // 業者情報を取得(SQLファイルで取得しようとすると、メンテナンスが大変になるためUIForm内で取得する)
                    M_DENSHI_JIGYOUJOU denshiJigyoujou = new M_DENSHI_JIGYOUJOU();
                    M_GYOUSHA gyousha = null;

                    // 事業場情報取得
                    var denshiJigyoujouDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    var jigyoujouFilter = new M_DENSHI_JIGYOUJOU();
                    if (!string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["US4_EDI_MEMBER_ID"].ToString())
                        && !string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["US4_JIGYOUJOU_CD"].ToString()))
                    {
                        jigyoujouFilter.EDI_MEMBER_ID = this.IchiranData.Rows[rowIndex]["US4_EDI_MEMBER_ID"].ToString();
                        jigyoujouFilter.JIGYOUJOU_CD = this.IchiranData.Rows[rowIndex]["US4_JIGYOUJOU_CD"].ToString();
                        var denshiJigyoujous = denshiJigyoujouDao.GetAllValidData(jigyoujouFilter);
                        if (denshiJigyoujous != null && denshiJigyoujous.Count() > 0)
                        {
                            denshiJigyoujou = denshiJigyoujous[0];

                            // 業者情報取得
                            if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                            {
                                var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                                gyousha = gyoushaDao.GetDataByCd(denshiJigyoujou.GYOUSHA_CD);
                            }
                        }
                    }
                    #endregion

                    // 登録しいる電子事業場があればそちらを優先して表示
                    if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = denshiJigyoujou.GYOUSHA_CD;
                        // 業者名
                        this.ctxt_GyoushaName.Text = gyousha != null ? gyousha.GYOUSHA_NAME_RYAKU : string.Empty;
                    }
                    else
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US4_GYOUSHA_CD"].ToString();
                        // 業者名
                        this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["US4_GYOUSHA_NAME_RYAKU"].ToString();

                        isPossibleInput = true;
                    }
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["US4_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["US4_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = this.IchiranData.Rows[rowIndex]["US4_GENBA_CD"].ToString();
                    // 現場名
                    this.ctxt_GenbaName.Text = this.IchiranData.Rows[rowIndex]["US4_GENBA_NAME_RYAKU"].ToString();
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = this.IchiranData.Rows[rowIndex]["US4_JIGYOUJOU_CD"].ToString();
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = this.IchiranData.Rows[rowIndex]["US4_JIGYOUJOU_NAME"].ToString();
                }
                // 収集運搬業者5
                else if ("SHUSHU_UNPAN_GYOUSHA5".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "収集運搬業者(区間5)";

                    // 収集運搬業者N
                    this.CellName = ConstCls.SHUSHU_UNPAN_GYOUSHAN;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU5_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SU5_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU5_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU5_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SU5_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["SU5_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 処分業者
                else if ("SHOBUN_GYOUSHA".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "処分事業者";

                    // 処分業者
                    this.CellName = ConstCls.SHOBUN_GYOUSHA;
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SYOBUNN_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SYOBUNN_TIME_STAMP"]);
                    }

                    // 業者CD
                    this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SB_GYOUSHA_CD"].ToString();
                    // 業者名
                    this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["SB_GYOUSHA_NAME_RYAKU"].ToString();
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SB_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["SB_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = "";
                    // 現場名
                    this.ctxt_GenbaName.Text = "";
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = "";
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = "";
                }
                // 処分事業場
                else if ("SHOBUN_JIGYOUJOU".Equals(cellName))
                {
                    // 選択先
                    this.ctxt_Sentakusaki.Text = "処分事業場";

                    // 処分事業場
                    this.CellName = ConstCls.SHOBUN_JIGYOUJOU;

                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SYOBUNN_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGyousha = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUSHA_SYOBUNN_TIME_STAMP"]);
                    }
                    if (!this.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_SYOBUNN_TIME_STAMP"]))
                    {
                        // タイムスタンプ
                        this.TimestampGenba = Convert.ToInt32(this.IchiranData.Rows[rowIndex]["JIGYOUJOU_SYOBUNN_TIME_STAMP"]);
                    }

                    #region 業者情報を取得
                    // 業者情報を取得(SQLファイルで取得しようとすると、メンテナンスが大変になるためUIForm内で取得する)
                    M_DENSHI_JIGYOUJOU denshiJigyoujou = new M_DENSHI_JIGYOUJOU();
                    M_GYOUSHA gyousha = null;

                    // 事業場情報取得
                    var denshiJigyoujouDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    var jigyoujouFilter = new M_DENSHI_JIGYOUJOU();
                    if (!string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["SB_EDI_MEMBER_ID"].ToString())
                        && !string.IsNullOrEmpty(this.IchiranData.Rows[rowIndex]["SB_JIGYOUJOU_CD"].ToString()))
                    {
                        jigyoujouFilter.EDI_MEMBER_ID = this.IchiranData.Rows[rowIndex]["SB_EDI_MEMBER_ID"].ToString();
                        jigyoujouFilter.JIGYOUJOU_CD = this.IchiranData.Rows[rowIndex]["SB_JIGYOUJOU_CD"].ToString();
                        var denshiJigyoujous = denshiJigyoujouDao.GetAllValidData(jigyoujouFilter);
                        if (denshiJigyoujous != null && denshiJigyoujous.Count() > 0)
                        {
                            denshiJigyoujou = denshiJigyoujous[0];

                            // 業者情報取得
                            if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                            {
                                var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                                gyousha = gyoushaDao.GetDataByCd(denshiJigyoujou.GYOUSHA_CD);
                            }
                        }
                    }
                    #endregion

                    // 登録しいる電子事業場があればそちらを優先して表示
                    if (!string.IsNullOrEmpty(denshiJigyoujou.GYOUSHA_CD))
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = denshiJigyoujou.GYOUSHA_CD;
                        // 業者名
                        this.ctxt_GyoushaName.Text = gyousha != null ? gyousha.GYOUSHA_NAME_RYAKU : string.Empty;
                    }
                    else
                    {
                        // 業者CD
                        this.cantxt_GyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SB_GYOUSHA_CD"].ToString();
                        // 業者名
                        this.ctxt_GyoushaName.Text = this.IchiranData.Rows[rowIndex]["SB_GYOUSHA_NAME_RYAKU"].ToString();

                        isPossibleInput = true;
                    }
                    // 事業者CD
                    this.cantxt_JigyoushaCd.Text = this.IchiranData.Rows[rowIndex]["SB_EDI_MEMBER_ID"].ToString();
                    // 事業者名
                    this.ctxt_JigyoushaName.Text = this.IchiranData.Rows[rowIndex]["SB_JIGYOUSHA_NAME"].ToString();

                    // 現場CD
                    this.cantxt_GenbaCd.Text = this.IchiranData.Rows[rowIndex]["SB_GENBA_CD"].ToString();
                    // 現場名
                    this.ctxt_GenbaName.Text = this.IchiranData.Rows[rowIndex]["SB_GENBA_NAME_RYAKU"].ToString();
                    // 事業場CD
                    this.cantxt_JigyoujouCd.Text = this.IchiranData.Rows[rowIndex]["SB_JIGYOUJOU_CD"].ToString();
                    // 事業場名
                    this.ctxt_JigyoujouName.Text = this.IchiranData.Rows[rowIndex]["SB_JIGYOUJOU_NAME"].ToString();
                }

                // 業者コードの設定
                this.GyoushaCdSet(isPossibleInput);
                // 現場コードの設定
                this.GenbaCdSet();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetSentakuData", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSentakuData", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// null判定
        /// </summary>
        private bool IsNullOrEmpty(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 業者コードの設定
        /// </summary>
        /// <param name="isPossibleInput">入力可能フラグ。true:常にReadOnly=false, false:cantxt_JigyoushaCd, cantxt_GyoushaCdの値に依存</param>
        internal void GyoushaCdSet(bool isPossibleInput)
        {
            // 【事業者CD】がブランク以外、【業者CD】がブランクの場合
            // ただし、電子事業場で業者が未設定の場合には業者は入力できるようにする
            if ((!string.Empty.Equals(this.cantxt_JigyoushaCd.Text.Trim())
                    && string.Empty.Equals(this.cantxt_GyoushaCd.Text.Trim()))
                || (!string.Empty.Equals(this.cantxt_JigyoushaCd.Text.Trim())
                    && isPossibleInput))
            {
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "TEKIYOU_FLG";
                dto.Value = "TRUE";
                this.cantxt_GyoushaCd.PopupSearchSendParams = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();
                this.cantxt_GyoushaCd.PopupSearchSendParams.Add(dto);
                this.cantxt_GyoushaCd.PopupWindowId = WINDOW_ID.M_GYOUSHA;
                this.cantxt_GyoushaCd.PopupWindowName = "検索共通ポップアップ";
                this.cantxt_GyoushaCd.ReadOnly = false;
                this.cantxt_GyoushaCd.TabStop = true;
            }
            else
            {
                this.cantxt_GyoushaCd.PopupWindowId = WINDOW_ID.NONE;
                this.cantxt_GyoushaCd.PopupWindowName = "";
                this.cantxt_GyoushaCd.ReadOnly = true;
                this.cantxt_GyoushaCd.TabStop = false;
            }
        }

        /// <summary>
        /// 現場コードの設定
        /// </summary>
        internal void GenbaCdSet()
        {
            // 【事業場CD】がブランク以外、【現場CD】がブランクの場合
            if (!string.Empty.Equals(this.cantxt_JigyoujouCd.Text.Trim())
                && string.Empty.Equals(this.cantxt_GenbaCd.Text.Trim()))
            {
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                dto.KeyName = "TEKIYOU_FLG";
                dto.Value = "TRUE";
                this.cantxt_GyoushaCd.PopupSearchSendParams = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();
                this.cantxt_GyoushaCd.PopupSearchSendParams.Add(dto);
                this.cantxt_GenbaCd.PopupWindowId = WINDOW_ID.M_GENBA;
                this.cantxt_GenbaCd.PopupWindowName = "複数キー用検索共通ポップアップ";
                this.cantxt_GenbaCd.ReadOnly = false;
                this.cantxt_GenbaCd.TabStop = true;
            }
            else
            {
                this.cantxt_GenbaCd.PopupWindowId = WINDOW_ID.NONE;
                this.cantxt_GenbaCd.PopupWindowName = "";
                this.cantxt_GenbaCd.ReadOnly = true;
                this.cantxt_GenbaCd.TabStop = false;
            }
        }

        /// <summary>
        /// DetailFooterクリア
        /// </summary>
        internal void ClearDetailFooter()
        {
            // 選択先
            this.ctxt_Sentakusaki.Text = "";
            // 業者CD
            this.cantxt_GyoushaCd.Text = "";
            // 業者名
            this.ctxt_GyoushaName.Text = "";
            // 事業者CD
            this.cantxt_JigyoushaCd.Text = "";
            // 事業者名
            this.ctxt_JigyoushaName.Text = "";
            // 現場CD
            this.cantxt_GenbaCd.Text = "";
            // 現場名
            this.ctxt_GenbaName.Text = "";
            // 事業場CD
            this.cantxt_JigyoujouCd.Text = "";
            // 事業場名
            this.ctxt_JigyoujouName.Text = "";
        }

        /// <summary>
        /// 業者CDEnter
        /// </summary>
        private void cantxt_GyoushaCd_Enter(object sender, EventArgs e)
        {
            this.beforeGyoushaCd = this.cantxt_GyoushaCd.Text;
            if (this.cantxt_GyoushaCd.ReadOnly == true)
            {
                return;
            }
            // 排出事業者、収集運搬業者N、運搬先業者N、処分業者
            if (ConstCls.HAISHUTSU_JIGYOUSHA.Equals(this.CellName)
                || ConstCls.SHUSHU_UNPAN_GYOUSHAN.Equals(this.CellName)
                || ConstCls.UNPANSAKI_GYOUSHAN.Equals(this.CellName)
                || ConstCls.SHOBUN_GYOUSHA.Equals(this.CellName))
            {
                this.GyoushaEnter();
            }
            // 排出事業場、運搬先事業場N、処分事業場
            else if (ConstCls.HAISHUTSU_JIGYOUJOU.Equals(this.CellName)
                || ConstCls.UNPANSAKI_JIGYOUJOUN.Equals(this.CellName)
                || ConstCls.SHOBUN_JIGYOUJOU.Equals(this.CellName))
            {
                this.GyoujouEnter();
            }
        }

        /// <summary>
        /// 業者の場合
        /// </summary>
        private void GyoushaEnter()
        {
            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();

            methodDto.Join = JOIN_METHOD.WHERE;
            methodDto.LeftTable = "M_GYOUSHA";

            searchDto.And_Or = CONDITION_OPERATOR.AND;
            searchDto.Condition = JUGGMENT_CONDITION.EQUALS;

            // 20151022 BUNN #12040 STR
            // 「排出事業者」の場合、排出事業者区分=1
            if (ConstCls.HAISHUTSU_JIGYOUSHA.Equals(this.CellName))
            {
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
            }
            // 「収集運搬業者N」の場合、運搬受託者区分=1
            else if (ConstCls.SHUSHU_UNPAN_GYOUSHAN.Equals(this.CellName))
            {
                searchDto.LeftColumn = "UNPAN_JUTAKUSHA_KAISHA_KBN";
            }
            // 「運搬先業者N」の場合、運搬受託者区分=1
            else if (ConstCls.UNPANSAKI_GYOUSHAN.Equals(this.CellName))
            {
                searchDto.LeftColumn = "UNPAN_JUTAKUSHA_KAISHA_KBN";
            }
            // 「処分業者」の場合、処分受託者区分=1
            else if (ConstCls.SHOBUN_GYOUSHA.Equals(this.CellName))
            {
                searchDto.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
            }
            // 20151022 BUNN #12040 END

            searchDto.Value = "True";
            searchDto.ValueColumnType = DB_TYPE.BIT;

            methodDto.SearchCondition.Add(searchDto);

            // 20151022 BUNN #12040 STR
            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
            searchDto1.And_Or = CONDITION_OPERATOR.AND;
            searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
            searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
            searchDto1.Value = "True";
            searchDto1.ValueColumnType = DB_TYPE.BIT;
            methodDto.SearchCondition.Add(searchDto1);
            // 20151022 BUNN #12040 END

            this.cantxt_GyoushaCd.popupWindowSetting.Clear();
            this.cantxt_GyoushaCd.popupWindowSetting.Add(methodDto);
        }

        /// <summary>
        /// 業場の場合
        /// </summary>
        private void GyoujouEnter()
        {
            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
            //r_framework.Dto.JoinMethodDto methodDto1 = null;

            // 「排出事業場」の場合、M_GYOUSHA.排出事業者区分=1
            if (ConstCls.HAISHUTSU_JIGYOUJOU.Equals(this.CellName))
            {
                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GYOUSHA";

                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                methodDto.SearchCondition.Add(searchDto);
            }
            // 「運搬先事業場N」の場合、M_GENBA.積替保管区分=1
            else if (ConstCls.UNPANSAKI_JIGYOUJOUN.Equals(this.CellName))
            {
                //methodDto1 = new r_framework.Dto.JoinMethodDto();

                //methodDto1.Join = JOIN_METHOD.INNER_JOIN;
                //methodDto1.LeftTable = "M_GENBA";
                //methodDto1.LeftKeyColumn = "GYOUSHA_CD";
                //methodDto1.RightTable = "M_GYOUSHA";
                //methodDto1.RightKeyColumn = "GYOUSHA_CD";

                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GYOUSHA";

                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                methodDto.SearchCondition.Add(searchDto);
            }
            // 「処分事業場」の場合、M_GYOUSHA.処分受託者区分=1
            else if (ConstCls.SHOBUN_JIGYOUJOU.Equals(this.CellName))
            {
                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GYOUSHA";

                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                methodDto.SearchCondition.Add(searchDto);
            }

            // 20151022 BUNN #12040 STR
            searchDto1.And_Or = CONDITION_OPERATOR.AND;
            searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
            searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
            searchDto1.Value = "True";
            searchDto1.ValueColumnType = DB_TYPE.BIT;
            methodDto.SearchCondition.Add(searchDto1);
            // 20151022 BUNN #12040 END

            this.cantxt_GyoushaCd.popupWindowSetting.Clear();
            this.cantxt_GyoushaCd.popupWindowSetting.Add(methodDto);
            //if (methodDto1 != null)
            //{
            //    this.cantxt_GyoushaCd.popupWindowSetting.Add(methodDto1);
            //}
        }

        /// <summary>
        /// 現場CDEnter
        /// </summary>
        private void cantxt_GenbaCd_Enter(object sender, EventArgs e)
        {
            if (this.cantxt_GenbaCd.ReadOnly == true)
            {
                return;
            }
            this.isGenbaError = false;
            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.SearchConditionsDto searchDto1 = null;
            r_framework.Dto.SearchConditionsDto searchDtoOr = null;
            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
            r_framework.Dto.JoinMethodDto methodDto1 = null;

            methodDto.Join = JOIN_METHOD.WHERE;
            methodDto.LeftTable = "M_GENBA";

            searchDto.And_Or = CONDITION_OPERATOR.AND;
            searchDto.Condition = JUGGMENT_CONDITION.EQUALS;

            // 「排出事業場」の場合、排出事業場区分=1
            if (ConstCls.HAISHUTSU_JIGYOUJOU.Equals(this.CellName))
            {
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;
            }
            // 「運搬先事業場N」の場合、積替保管区分=1
            else if (ConstCls.UNPANSAKI_JIGYOUJOUN.Equals(this.CellName))
            {
                searchDto.LeftColumn = "TSUMIKAEHOKAN_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;
            }
            // 「処分事業場」の場合、処分事業場区分=1 OR 最終処分場区分=1
            else if (ConstCls.SHOBUN_JIGYOUJOU.Equals(this.CellName))
            {
                searchDto.LeftColumn = "SHOBUN_NIOROSHI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                //// 業者CDが入力される場合
                //if (!string.IsNullOrEmpty(this.cantxt_GyoushaCd.Text))
                //{
                //    searchDto1 = new r_framework.Dto.SearchConditionsDto();

                //    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                //    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                //    searchDto1.LeftColumn = "GYOUSHA_CD";
                //    searchDto1.Value = this.cantxt_GyoushaCd.Text;
                //    searchDto1.ValueColumnType = DB_TYPE.VARCHAR;
                //}

                searchDtoOr = new r_framework.Dto.SearchConditionsDto(); 
                searchDtoOr.And_Or = CONDITION_OPERATOR.OR;
                searchDtoOr.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDtoOr.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                searchDtoOr.Value = "True";
                searchDtoOr.ValueColumnType = DB_TYPE.BIT;
            }

            methodDto.SearchCondition.Add(searchDto);

            if (searchDto1 != null)
            {
                methodDto.SearchCondition.Add(searchDto1);
            }

            if (searchDtoOr != null)
            {
                methodDto.SearchCondition.Add(searchDtoOr);
            }

            // 業者CDが入力される場合
            if (!string.IsNullOrEmpty(this.cantxt_GyoushaCd.Text))
            {
                searchDto = new r_framework.Dto.SearchConditionsDto();

                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "GYOUSHA_CD";
                searchDto.Value = this.cantxt_GyoushaCd.Text;
                searchDto.ValueColumnType = DB_TYPE.VARCHAR;
                methodDto.SearchCondition.Add(searchDto);
            }
            else
            {
                searchDto = new r_framework.Dto.SearchConditionsDto();

                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;

                // 「排出事業場」の場合
                if (ConstCls.HAISHUTSU_JIGYOUJOU.Equals(this.CellName))
                {
                    searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                }
                // 「運搬先事業場N」の場合
                else if (ConstCls.UNPANSAKI_JIGYOUJOUN.Equals(this.CellName))
                {
                    searchDto.LeftColumn = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                }
                // 「処分事業場」の場合
                else if (ConstCls.SHOBUN_JIGYOUJOU.Equals(this.CellName))
                {
                    searchDto.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                }
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                methodDto1 = new r_framework.Dto.JoinMethodDto();
                methodDto1.Join = JOIN_METHOD.WHERE;
                methodDto1.LeftTable = "M_GYOUSHA";
                methodDto1.SearchCondition.Add(searchDto);

                // 20151022 BUNN #12040 STR
                searchDto1 = new r_framework.Dto.SearchConditionsDto();
                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
                searchDto1.Value = "True";
                searchDto1.ValueColumnType = DB_TYPE.BIT;
                methodDto1.SearchCondition.Add(searchDto1);
                // 20151022 BUNN #12040 END
            }
            this.cantxt_GenbaCd.popupWindowSetting.Clear();
            this.cantxt_GenbaCd.popupWindowSetting.Add(methodDto);
            if (methodDto1 != null)
            {
                this.cantxt_GenbaCd.popupWindowSetting.Add(methodDto1);
            }
        }

        /// <summary>
        /// フォームKeyDown
        /// </summary>
        private void UIForm_KeyDown(object sender, KeyEventArgs e)
        {

            // ESCキーの場合
            if (e.KeyCode == Keys.Escape)
            {
                ((BusinessBaseForm)this.Parent).txb_process.Focus();
            }

            // ENTERキーの場合
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cdate_HikiwatasiBiTo.Focused)
                {
                    this.cdate_HikiwatasiBiTo.Focus();
                }
                else if ("1".Equals(((BusinessBaseForm)this.Parent).txb_process.Text))
                {
                    this.GyoushaTouroku();
                }
                else if ("2".Equals(((BusinessBaseForm)this.Parent).txb_process.Text))
                {
                    this.GenbaTouroku();
                }
            }
        }

        /// <summary>
        /// 業者CD更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_GyoushaCd_Validated(object sender, EventArgs e)
        {
            if (this.beforeGyoushaCd == this.cantxt_GyoushaCd.Text && !this.isGyoushaError)
            {
                return;
            }

            this.isGyoushaError = false;
            this.cantxt_GenbaCd.Text = string.Empty;
            this.ctxt_GenbaName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.cantxt_GyoushaCd.Text))
            {
                this.ctxt_GyoushaName.Text = string.Empty;
                return;
            }

            if (this.cantxt_GyoushaCd.ReadOnly == true)
            {
                return;
            }

            // ゼロ埋め
            this.cantxt_GyoushaCd.Text = this.cantxt_GyoushaCd.Text.PadLeft(6, '0');

            bool catchErr = true;
            bool isOk = this.logic.GyoushaCdLeaveChk(this.CellName, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (!isOk)
            {
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                this.isGyoushaError = true;
                this.cantxt_GyoushaCd.Focus();

                // エラーメッセージ表示
                messageShowLogic.MessageBoxShow("E020", "業者");
            }
        }

        /// <summary>
        /// 現場CD更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_GenbaCd_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cantxt_GenbaCd.Text))
            {
                this.ctxt_GenbaName.Text = string.Empty;
                return;
            }

            if (this.cantxt_GenbaCd.ReadOnly == true)
            {
                return;
            }

            MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.cantxt_GyoushaCd.Text))
            {
                this.isGenbaError = true;
                this.cantxt_GenbaCd.Focus();
                // エラーメッセージ表示
                messageShowLogic.MessageBoxShow("E051", "業者");
                this.cantxt_GenbaCd.Text = string.Empty;
                return;
            }

            // ゼロ埋め
            this.cantxt_GenbaCd.Text = this.cantxt_GenbaCd.Text.PadLeft(6, '0');

            bool catchErr = true;
            bool isOk = this.logic.GenbaCdLeaveChk(this.CellName, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (!isOk)
            {
                this.isGenbaError = true;
                this.cantxt_GenbaCd.Focus();
                // エラーメッセージ表示
                messageShowLogic.MessageBoxShow("E020", "現場");
            }
            else
            {
                this.isGenbaError = false;
            }
        }

        /// <summary>
        /// 業者CD_BackColorChanged
        /// </summary>
        private void cantxt_GyoushaCd_BackColorChanged(object sender, EventArgs e)
        {
            if (isGyoushaError)
            {
                this.cantxt_GyoushaCd.BackColor = Constans.ERROR_COLOR;
            }
        }

        /// <summary>
        /// 現場CD_BackColorChanged
        /// </summary>
        private void cantxt_GenbaCd_BackColorChanged(object sender, EventArgs e)
        {
            if (isGenbaError)
            {
                this.cantxt_GenbaCd.BackColor = Constans.ERROR_COLOR;
            }
        }

        /// <summary>
        /// 一覧の値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEditedFormattedValueChanged(object sender, GrapeCity.Win.MultiRow.CellEditedFormattedValueChangedEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            
            var row = this.Ichiran.Rows[e.RowIndex];

            if (row == null)
            {
                return;
            }

            if (ConstCls.CELL_NAME_CHECK.Equals(e.CellName))
            {
                this.logic.CheckRenkeiMaster(row);
            }
        }

        /// <summary>
        /// ヘッダーチェックボックス変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellContentClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.Scope == CellScope.ColumnHeader && Ichiran.CurrentCell is CheckBoxCell)
            {
                //チェックボックス型セルの値を取得します
                this.logic.ChangeHeaderCheckBox();
            }
        }

        #region 一覧の背景色設定

        /// <summary>
        /// 一覧の背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellLeave(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            this.SetIchiranBackColor();
        }

        /// <summary>
        /// 一覧の背景色を設定
        /// 本メソッドを修正する場合、SelectMihimodukeIchiran.sqlの条件にも変更が必要かどうか確認。(検索条件：マスタ設定に影響があるかどうか確認)
        /// </summary>
        internal bool SetIchiranBackColor()
        {
            bool ret = true;
            try
            {
                var searchResult = this.IchiranData;
                for (int i = 0; i < Ichiran.Rows.Count; i++)
                {
                    if (!this.IsNullOrEmpty(searchResult.Rows[i]["KANRI_ID"]))
                    {
                        if (this.IsNullOrEmpty(searchResult.Rows[i]["EDI_MEMBER_ID"])
                            || this.IsNullOrEmpty(searchResult.Rows[i]["GYOUSHA_CD"]))
                        {
                            // 排出事業者
                            this.Ichiran.Rows[i].Cells["HAISHUTSU_JIGYOUSHA"].Style.BackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            this.Ichiran.Rows[i].Cells["HAISHUTSU_JIGYOUSHA"].Style.SelectionBackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        }

                        if (this.IsNullOrEmpty(searchResult.Rows[i]["JIGYOUJOU_CD"])
                            || this.IsNullOrEmpty(searchResult.Rows[i]["GENBA_CD"]))
                        {
                            // 排出事業場
                            this.Ichiran.Rows[i].Cells["HAISHUTSU_JIGYOUJOU"].Style.BackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            this.Ichiran.Rows[i].Cells["HAISHUTSU_JIGYOUJOU"].Style.SelectionBackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        }
                    }
                    if (!this.IsNullOrEmpty(searchResult.Rows[i]["SU1_KANRI_ID"]))
                    {
                        if (this.IsNullOrEmpty(searchResult.Rows[i]["SU1_EDI_MEMBER_ID"])
                            || this.IsNullOrEmpty(searchResult.Rows[i]["SU1_GYOUSHA_CD"]))
                        {
                            // 収集運搬業者1
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA1"].Style.BackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA1"].Style.SelectionBackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        }

                        if (Convert.ToInt32(searchResult.Rows[i]["UPN_ROUTE_CNT"]) > 1)
                        {
                            if (this.IsNullOrEmpty(searchResult.Rows[i]["US1_EDI_MEMBER_ID"])
                                || this.IsNullOrEmpty(searchResult.Rows[i]["US1_GYOUSHA_CD"]))
                            {
                                // 運搬先業者1
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA1"].Style.BackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA1"].Style.SelectionBackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            }

                            if (this.IsNullOrEmpty(searchResult.Rows[i]["US1_JIGYOUJOU_CD"])
                                || this.IsNullOrEmpty(searchResult.Rows[i]["US1_GENBA_CD"]))
                            {
                                // 運搬先事業場1
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU1"].Style.BackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU1"].Style.SelectionBackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            }
                        }
                    }

                    if (!this.IsNullOrEmpty(searchResult.Rows[i]["SU2_KANRI_ID"]))
                    {
                        if (this.IsNullOrEmpty(searchResult.Rows[i]["SU2_EDI_MEMBER_ID"])
                            || this.IsNullOrEmpty(searchResult.Rows[i]["SU2_GYOUSHA_CD"]))
                        {
                            // 収集運搬業者2
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA2"].Style.BackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA2"].Style.SelectionBackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        }

                        if (Convert.ToInt32(searchResult.Rows[i]["UPN_ROUTE_CNT"]) > 2)
                        {
                            if (this.IsNullOrEmpty(searchResult.Rows[i]["US2_EDI_MEMBER_ID"])
                                || this.IsNullOrEmpty(searchResult.Rows[i]["US2_GYOUSHA_CD"]))
                            {
                                // 運搬先業者2
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA2"].Style.BackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA2"].Style.SelectionBackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            }

                            if (this.IsNullOrEmpty(searchResult.Rows[i]["US2_JIGYOUJOU_CD"])
                                || this.IsNullOrEmpty(searchResult.Rows[i]["US2_GENBA_CD"]))
                            {
                                // 運搬先事業場2
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU2"].Style.BackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU2"].Style.SelectionBackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            }
                        }
                    }

                    if (!this.IsNullOrEmpty(searchResult.Rows[i]["SU3_KANRI_ID"]))
                    {
                        if (this.IsNullOrEmpty(searchResult.Rows[i]["SU3_EDI_MEMBER_ID"])
                            || this.IsNullOrEmpty(searchResult.Rows[i]["SU3_GYOUSHA_CD"]))
                        {
                            // 収集運搬業者3
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA3"].Style.BackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA3"].Style.SelectionBackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        }

                        if (Convert.ToInt32(searchResult.Rows[i]["UPN_ROUTE_CNT"]) > 3)
                        {
                            if (this.IsNullOrEmpty(searchResult.Rows[i]["US3_EDI_MEMBER_ID"])
                                || this.IsNullOrEmpty(searchResult.Rows[i]["US3_GYOUSHA_CD"]))
                            {
                                // 運搬先業者3
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA3"].Style.BackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA3"].Style.SelectionBackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            }

                            if (this.IsNullOrEmpty(searchResult.Rows[i]["US3_JIGYOUJOU_CD"])
                                || this.IsNullOrEmpty(searchResult.Rows[i]["US3_GENBA_CD"]))
                            {
                                // 運搬先事業場3
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU3"].Style.BackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU3"].Style.SelectionBackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            }
                        }
                    }

                    if (!this.IsNullOrEmpty(searchResult.Rows[i]["SU4_KANRI_ID"]))
                    {
                        if (this.IsNullOrEmpty(searchResult.Rows[i]["SU4_EDI_MEMBER_ID"])
                            || this.IsNullOrEmpty(searchResult.Rows[i]["SU4_GYOUSHA_CD"]))
                        {
                            // 収集運搬業者4
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA4"].Style.BackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA4"].Style.SelectionBackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        }

                        if (Convert.ToInt32(searchResult.Rows[i]["UPN_ROUTE_CNT"]) > 4)
                        {
                            if (this.IsNullOrEmpty(searchResult.Rows[i]["US4_EDI_MEMBER_ID"])
                                || this.IsNullOrEmpty(searchResult.Rows[i]["US4_GYOUSHA_CD"]))
                            {
                                // 運搬先業者4
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA4"].Style.BackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA4"].Style.SelectionBackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            }

                            if (this.IsNullOrEmpty(searchResult.Rows[i]["US4_JIGYOUJOU_CD"])
                                || this.IsNullOrEmpty(searchResult.Rows[i]["US4_GENBA_CD"]))
                            {
                                // 運搬先事業場4
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU4"].Style.BackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                                this.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU4"].Style.SelectionBackColor
                                    = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            }
                        }
                    }

                    if (!this.IsNullOrEmpty(searchResult.Rows[i]["SU5_KANRI_ID"]))
                    {
                        if (this.IsNullOrEmpty(searchResult.Rows[i]["SU5_EDI_MEMBER_ID"])
                            || this.IsNullOrEmpty(searchResult.Rows[i]["SU5_GYOUSHA_CD"]))
                        {
                            // 収集運搬業者5
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA5"].Style.BackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            this.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA5"].Style.SelectionBackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        }
                    }

                    if (!this.IsNullOrEmpty(searchResult.Rows[i]["SB_KANRI_ID"]))
                    {
                        if (this.IsNullOrEmpty(searchResult.Rows[i]["SB_EDI_MEMBER_ID"])
                            || this.IsNullOrEmpty(searchResult.Rows[i]["SB_GYOUSHA_CD"]))
                        {
                            // 処分業者
                            this.Ichiran.Rows[i].Cells["SHOBUN_GYOUSHA"].Style.BackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            this.Ichiran.Rows[i].Cells["SHOBUN_GYOUSHA"].Style.SelectionBackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        }

                        if (this.IsNullOrEmpty(searchResult.Rows[i]["SB_JIGYOUJOU_CD"])
                            || this.IsNullOrEmpty(searchResult.Rows[i]["SB_GENBA_CD"]))
                        {
                            // 処分事業場
                            this.Ichiran.Rows[i].Cells["SHOBUN_JIGYOUJOU"].Style.BackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                            this.Ichiran.Rows[i].Cells["SHOBUN_JIGYOUJOU"].Style.SelectionBackColor
                                = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranBackColor", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion

        /// 20141021 Houkakou 「補助データ」の日付チェックを追加する　start
        private void cdate_HikiwatasiBiFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cdate_HikiwatasiBiTo.Text))
            {
                this.cdate_HikiwatasiBiTo.IsInputErrorOccured = false;
                this.cdate_HikiwatasiBiTo.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void cdate_HikiwatasiBiTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cdate_HikiwatasiBiFrom.Text))
            {
                this.cdate_HikiwatasiBiFrom.IsInputErrorOccured = false;
                this.cdate_HikiwatasiBiFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141021 Houkakou 「補助データ」の日付チェックを追加する　end

        /// <summary>
        /// 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void Gyousha_PopupBeforeExecuteMethod()
        {
            this.beforeGyoushaCd = this.cantxt_GyoushaCd.Text;
        }

        /// <summary>
        /// 業者 PopupAfterExecuteMethod
        /// </summary>
        public void Gyousha_PopupAfterExecuteMethod()
        {
            if (this.beforeGyoushaCd != this.cantxt_GyoushaCd.Text)
            {
                this.cantxt_GenbaCd.Text = string.Empty;
                this.ctxt_GenbaName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void HstJigyoushaCd_PopupBeforeExecuteMethod()
        {
            this.beforeHstJigyoujouCd = this.HstJigyoushaCd.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        public void HstJigyoushaCd_PopupAfterExecuteMethod()
        {
            if (this.beforeHstJigyoujouCd != this.HstJigyoushaCd.Text)
            {
                this.HstJigyoujouCd.Text = string.Empty;
                this.HstJigyoujouName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分業者 PopupBeforeExecuteMethod
        /// </summary>
        public void SbnJigyoushaCd_PopupBeforeExecuteMethod()
        {
            this.beforeSbnJigyoushaCd = this.SbnJigyoushaCd.Text;
        }

        /// <summary>
        /// 処分業者 PopupAfterExecuteMethod
        /// </summary>
        public void SbnJigyoushaCd_PopupAfterExecuteMethod()
        {
            if (this.beforeSbnJigyoushaCd != this.SbnJigyoushaCd.Text)
            {
                this.SbnJigyoujouCd.Text = string.Empty;
                this.SbnJigyoujouName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 最終処分事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void LastSbnJigyoushaCd_PopupBeforeExecuteMethod()
        {
            this.beforeLastSbnJigyoushaCd = this.LastSbnJigyoushaCd.Text;
        }

        /// <summary>
        /// 最終処分事業者 PopupAfterExecuteMethod
        /// </summary>
        public void LastSbnJigyoushaCd_PopupAfterExecuteMethod()
        {
            if (this.beforeLastSbnJigyoushaCd != this.LastSbnJigyoushaCd.Text)
            {
                this.LastSbnJigyoujouCd.Text = string.Empty;
                this.LastSbnJigyoujouName.Text = string.Empty;
            }
        }
    }
}
