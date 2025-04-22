using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Shougun.Core.PaperManifest.HensoSakiAnnaisho
{
    /// <summary>
    /// 返送案内書画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 一覧GRID：X＝4
        /// </summary>
        private int mIchiranLocationX = 4;

        /// <summary>
        /// 一覧GRID：Y＝214
        /// </summary>
        private int mIchiranLocationY = 214;

        private MessageBoxShowLogic myMessageBox = new MessageBoxShowLogic();

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string txtHaishutsuJigyoushaBef { get; set; }
        private string txtHaishutsuJigyoushaAft { get; set; }
        private string txtHaishutsuJigyoushaTOBef { get; set; }
        private string txtHaishutsuJigyoushaTOAft { get; set; }
        private string txtGYOUSHABef { get; set; }
        private string txtGYOUSHAAft { get; set; }
        private string txtGYOUSHAToBef { get; set; }
        private string txtGYOUSHAToAft { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_HENSO_ANNAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面Load処理

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic.WindowInit();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.KohuBangoGRD != null)
                {
                    this.KohuBangoGRD.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
                if (this.HenkyakusakiShukeiGRD != null)
                {
                    this.HenkyakusakiShukeiGRD.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
                if (this.GenbaGRD != null)
                {
                    this.GenbaGRD.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region Functionボタン 押下処理

        #region [F1] 番号「前」

        /// <summary>
        /// 番号「前」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void previousNumber_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 前の番号を取得
                this.logic.GetPreviousNumber();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F2] 番号「次」

        /// <summary>
        /// 番号「次」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void nextNumber_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 次の番号を取得
                this.logic.GetNextNumber();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F5]印刷

        /// <summary>
        /// [F5]印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Print(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var msgLogic = new MessageBoxShowLogic();
                //発行拠点
                if (string.IsNullOrEmpty(this.logic.headForm.HAKKOU_KYOTEN_CD.Text))
                {
                    this.logic.headForm.HAKKOU_KYOTEN_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E001", "発行拠点CD");
                    this.logic.headForm.HAKKOU_KYOTEN_CD.Focus();
                    return;
                }
                //印刷区分
                if (string.IsNullOrEmpty(this.txt_InsatuKubunCD.Text))
                {
                    msgLogic.MessageBoxShow("E034", "印刷区分");
                    return;
                }
                //出力内容
                if (string.IsNullOrEmpty(this.txt_ShuturyokuNaiyoCD.Text))
                {
                    msgLogic.MessageBoxShow("E034", "出力内容");
                    return;
                }

                this.logic.Print();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F6]CSV出力

        /// <summary>
        /// [F6]CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //CSV出力
                this.logic.CsvOutput();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F8]検索

        /// <summary>
        /// [F8]検索
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.logic.SearchCheck())//155769
                {
                    LogUtility.DebugMethodEnd();
                    return;
                }
                // 20141022 Houkakou 「返送案内書」の日付チェックを追加する start
                if (this.logic.DateCheck())
                {
                    return;
                }
                // 20141022 Houkakou 「返送案内書」の日付チェックを追加する end
                // FromToのチェックがうまくいかないので自前でチェックする
                var errMsg = String.Empty;
                bool catchErr = true;
                errMsg = this.logic.CheckErr(out catchErr);
                if (catchErr)
                {
                    return;
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E032", errMsg + "From", errMsg + "To");
                    Cursor.Current = Cursors.Arrow;
                    return;
                }
                //クリアー詳細データ情報
                this.logic.ClearDetailData();
                // 画面表示
                this.logic.Search();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F12]閉じる

        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CloseForm();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 出力内容設定

        private void txt_ShuturyokuNaiyoCD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;
                // 画面表示
                CustomTextBox obj = (CustomTextBox)sender;
                string strShuturyokuNaiyoCD = obj.Text;
                //返送先の数
                int count = string.IsNullOrEmpty(this.txtManiHensousakiCount.Tag.ToString()) ? 0 : int.Parse(this.txtManiHensousakiCount.Tag.ToString());
                string maniHensousakiKbnName;
                switch (strShuturyokuNaiyoCD)
                {
                    case "1":
                        //交付番号毎表示
                        this.KohuBangoGRD.Visible = true;
                        this.KohuBangoGRD.Location = new System.Drawing.Point(this.mIchiranLocationX, this.mIchiranLocationY);
                        //返却先集計非表示
                        this.HenkyakusakiShukeiGRD.Visible = false;
                        //現場毎非表示
                        this.GenbaGRD.Visible = false;
                        this.ParentBaseForm.bt_func1.Enabled = true;
                        this.ParentBaseForm.bt_func2.Enabled = true;
                        //第一の返送先名称設定
                        this.logic.SetManiHensousakiName(count, out maniHensousakiKbnName);
                        //返送先総件数
                        this.txtAllManiHensousakiCount.Text = this.txtAllManiHensousakiCount.Tag.ToString();
                        //今表示の返送先数設定
                        this.txtManiHensousakiCount.Text = this.txtAllManiHensousakiCount.Tag.ToString().Equals("0") ? "0" : (count + 1).ToString();

                        //読込データ件数設定
                        this.logic.headForm.readDataNumber.Text = KohuBangoGRD.Rows.Count == 0 ? "0" : KohuBangoGRD.Rows.Count.ToString("#,###");
                        // 20140623 kayo 不具合#4972 アラート件数判断不正 start
                        if (!this.logic.SetDataForKohuBangoGRD()) { return; }
                        // 20140623 kayo 不具合#4972 アラート件数判断不正 end
                        break;

                    case "2":
                        //現場毎表示
                        this.GenbaGRD.Visible = true;
                        this.GenbaGRD.Location = new System.Drawing.Point(this.mIchiranLocationX, this.mIchiranLocationY);
                        //交付番号毎非表示
                        this.KohuBangoGRD.Visible = false;
                        //返却先集計非表示
                        this.HenkyakusakiShukeiGRD.Visible = false;
                        this.ParentBaseForm.bt_func1.Enabled = true;
                        this.ParentBaseForm.bt_func2.Enabled = true;
                        //第一の返送先名称設定
                        this.logic.SetManiHensousakiName(count, out maniHensousakiKbnName);
                        //返送先総件数
                        this.txtAllManiHensousakiCount.Text = this.txtAllManiHensousakiCount.Tag.ToString();
                        //今表示の返送先数設定
                        this.txtManiHensousakiCount.Text = this.txtAllManiHensousakiCount.Tag.ToString().Equals("0") ? "0" : (count + 1).ToString();
                        //読込データ件数設定
                        this.logic.headForm.readDataNumber.Text = GenbaGRD.Rows.Count == 0 ? "0" : GenbaGRD.Rows.Count.ToString("#,###");
                        // 20140623 kayo 不具合#4972 アラート件数判断不正 start
                        if (!this.logic.SetDataForGenbaGRD()) { return; }
                        // 20140623 kayo 不具合#4972 アラート件数判断不正 end
                        break;

                    case "3":
                        //返却先集計
                        this.HenkyakusakiShukeiGRD.Visible = true;
                        this.HenkyakusakiShukeiGRD.Location = new System.Drawing.Point(this.mIchiranLocationX, this.mIchiranLocationY);
                        //交付番号毎非表示
                        this.KohuBangoGRD.Visible = false;
                        //現場毎非表示
                        this.GenbaGRD.Visible = false;
                        this.ParentBaseForm.bt_func1.Enabled = false;
                        this.ParentBaseForm.bt_func2.Enabled = false;
                        //第一の返送先名称設定
                        this.txtManiHensousakiName.Text = string.Empty;
                        //返送先総件数
                        this.txtAllManiHensousakiCount.Text = "0";
                        //今表示の返送先数設定
                        this.txtManiHensousakiCount.Text = "0";
                        //読込データ件数設定
                        this.logic.headForm.readDataNumber.Text = HenkyakusakiShukeiGRD.Rows.Count == 0 ? "0" : HenkyakusakiShukeiGRD.Rows.Count.ToString("#,###");
                        // 20140623 kayo 不具合#4972 アラート件数判断不正 start
                        if (!this.logic.SetDataForHenkyakusakiShukeiGRD()) { return; }
                        // 20140623 kayo 不具合#4972 アラート件数判断不正 end
                        break;

                    default:
                        //交付番号毎表示
                        this.KohuBangoGRD.Visible = true;
                        this.KohuBangoGRD.Location = new System.Drawing.Point(this.mIchiranLocationX, this.mIchiranLocationY);
                        //返却先集計非表示
                        this.HenkyakusakiShukeiGRD.Visible = false;
                        //現場毎非表示
                        this.GenbaGRD.Visible = false;
                        this.ParentBaseForm.bt_func1.Enabled = true;
                        this.ParentBaseForm.bt_func2.Enabled = true;
                        //第一の返送先名称設定
                        if (!string.IsNullOrEmpty(this.txtManiHensousakiName.Tag.ToString()))
                        {
                            string[] nameS = this.txtManiHensousakiName.Tag.ToString().Split(' ');
                            this.txtManiHensousakiName.Text = Strings.StrConv(nameS[0], VbStrConv.Wide, 0) + " " + nameS[1];
                        }
                        else
                        {
                            this.txtManiHensousakiName.Text = this.txtManiHensousakiName.Tag.ToString();
                        }
                        //返送先総件数
                        this.txtAllManiHensousakiCount.Text = this.txtAllManiHensousakiCount.Tag.ToString();
                        //今表示の返送先数設定
                        this.txtManiHensousakiCount.Text = (count + 1).ToString();
                        //読込データ件数設定
                        this.logic.headForm.readDataNumber.Text = KohuBangoGRD.Rows.Count == 0 ? "0" : KohuBangoGRD.Rows.Count.ToString("#,###");
                        // 20140623 kayo 不具合#4972 アラート件数判断不正 start
                        if (!this.logic.SetDataForKohuBangoGRD()) { return; }
                        // 20140623 kayo 不具合#4972 アラート件数判断不正 end
                        break;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 印刷区分設定

        private void txt_InsatuKubunCD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 印刷区分
                CustomTextBox obj = (CustomTextBox)sender;
                string strInsatuKubunCD = obj.Text;
                switch (strInsatuKubunCD)
                {
                    case "1":
                        //交付番号毎
                        this.KohuBangoGRD.Columns[ConstCls.KohuBangoGRDColName.SEND_B1].Visible = true;
                        this.KohuBangoGRD.Columns[ConstCls.KohuBangoGRDColName.SEND_B4].Visible = false;
                        this.KohuBangoGRD.Columns[ConstCls.KohuBangoGRDColName.SEND_B6].Visible = false;
                        //返却先集計
                        this.HenkyakusakiShukeiGRD.Columns[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B1].Visible = true;
                        this.HenkyakusakiShukeiGRD.Columns[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B4].Visible = false;
                        this.HenkyakusakiShukeiGRD.Columns[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B6].Visible = false;
                        //現場毎
                        this.GenbaGRD.Template = new RefGenbaInfo();
                        if (!this.logic.ExecuteAlignmentForDetail(false, strInsatuKubunCD)) { return; }
                        break;

                    case "2":
                        //交付番号毎
                        this.KohuBangoGRD.Columns[ConstCls.KohuBangoGRDColName.SEND_B1].Visible = false;
                        this.KohuBangoGRD.Columns[ConstCls.KohuBangoGRDColName.SEND_B4].Visible = true;
                        this.KohuBangoGRD.Columns[ConstCls.KohuBangoGRDColName.SEND_B6].Visible = true;
                        //返却先集計
                        this.HenkyakusakiShukeiGRD.Columns[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B1].Visible = false;
                        this.HenkyakusakiShukeiGRD.Columns[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B4].Visible = true;
                        this.HenkyakusakiShukeiGRD.Columns[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B6].Visible = true;
                        //現場毎
                        this.GenbaGRD.Template = new RefGenbaInfo();
                        if (!this.logic.ExecuteAlignmentForDetail(true, strInsatuKubunCD)) { return; }
                        break;

                    default:
                        break;
                }
                this.logic.ClearDetailData();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #endregion

        #region 現場チェック

        #region 排出事業現場チェック

        private void txtHaishutsuJigyoushaCd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //「範囲条件」に現場を指定する場合は、業者の開始/終了CDを同じにしてください。
                this.ChecktHaishutsuGenba();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void txtHaishutsuGenbaCd_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //「範囲条件」に現場を指定する場合は、業者の開始/終了CDを同じにしてください。
                this.ChecktHaishutsuGenba();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        internal void ChecktHaishutsuGenba()
        {
            //「範囲条件」に現場を指定する場合は、業者の開始/終了CDを同じにしてください。
            if ((string.IsNullOrEmpty(this.txtHaishutsuJigyoushaCd.Text)
                && string.IsNullOrEmpty(this.txtHaishutsuJigyoushaCdTO.Text))
                || !this.txtHaishutsuJigyoushaCd.Text.Equals(this.txtHaishutsuJigyoushaCdTO.Text))
            {
                //myMessageBox.MessageBoxShow("E102", "排出事業場", "排出事業者");
                this.txtHaishutsuGenbaCd.ReadOnly = true;
                this.txtHaishutsuGenbaCdTO.ReadOnly = true;
                return;
            }
            this.txtHaishutsuGenbaCd.ReadOnly = false;
            this.txtHaishutsuGenbaCdTO.ReadOnly = false;
        }

        #endregion

        #region 返送先現場チェック

        private void txtGYOUSHA_CD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //「範囲条件」に現場を指定する場合は、業者の開始/終了CDを同じにしてください。
                this.CheckGenba();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void txtGENBA_CD_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //「範囲条件」に現場を指定する場合は、業者の開始/終了CDを同じにしてください。
                this.CheckGenba();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        internal void CheckGenba()
        {
            //「範囲条件」に現場を指定する場合は、業者の開始/終了CDを同じにしてください。
            if ((string.IsNullOrEmpty(this.txtGYOUSHA_CD.Text)
                && string.IsNullOrEmpty(this.txtGYOUSHA_CD_TO.Text))
                || !this.txtGYOUSHA_CD.Text.Equals(this.txtGYOUSHA_CD_TO.Text))
            {
                //myMessageBox.MessageBoxShow("E102", "返送先(現場)", "返送先(業者)");
                this.txtGENBA_CD.ReadOnly = true;
                this.txtGENBA_CD_TO.ReadOnly = true;
                return;
            }
            this.txtGENBA_CD.ReadOnly = false;
            this.txtGENBA_CD_TO.ReadOnly = false;
        }

        #endregion

        #endregion

        // 20141022 Houkakou 「返送案内書」の日付チェックを追加する start
        private void txtHinnkyakuDateFROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtHinnkyakuDateTO.Text))
            {
                this.txtHinnkyakuDateTO.IsInputErrorOccured = false;
                this.txtHinnkyakuDateTO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void txtHinnkyakuDateTO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtHinnkyakuDateFROM.Text))
            {
                this.txtHinnkyakuDateFROM.IsInputErrorOccured = false;
                this.txtHinnkyakuDateFROM.BackColor = Constans.NOMAL_COLOR;
            }
        }

        // 20141022 Houkakou 「返送案内書」の日付チェックを追加する end

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
        /// 排出事業者_POPUP_BEFイベント
        /// </summary>
        public void txtHaishutsuJigyousha_POPUP_BEF()
        {
            txtHaishutsuJigyoushaBef = this.txtHaishutsuJigyoushaCd.Text;
        }

        /// <summary>
        /// 排出事業者_POPUP_AFTイベント
        /// </summary>
        public void txtHaishutsuJigyousha_POPUP_AFT()
        {
            txtHaishutsuJigyoushaAft = this.txtHaishutsuJigyoushaCd.Text;
            if (txtHaishutsuJigyoushaBef != txtHaishutsuJigyoushaAft)
            {
                this.txtHaishutsuGenbaCd.Text = string.Empty;
                this.txtHaishutsuGenbaNm.Text = string.Empty;
                this.txtHaishutsuGenbaCdTO.Text = string.Empty;
                this.txtHaishutsuGenbaNmTO.Text = string.Empty;
            }
        }

        /// <summary>
        /// 排出事業者TO_POPUP_BEFイベント
        /// </summary>
        public void txtHaishutsuJigyoushaTO_POPUP_BEF()
        {
            txtHaishutsuJigyoushaTOBef = this.txtHaishutsuJigyoushaCdTO.Text;
        }

        /// <summary>
        /// 排出事業者TO_POPUP_AFTイベント
        /// </summary>
        public void txtHaishutsuJigyoushaTO_POPUP_AFT()
        {
            txtHaishutsuJigyoushaTOAft = this.txtHaishutsuJigyoushaCdTO.Text;
            if (txtHaishutsuJigyoushaTOBef != txtHaishutsuJigyoushaTOAft)
            {
                this.txtHaishutsuGenbaCd.Text = string.Empty;
                this.txtHaishutsuGenbaNm.Text = string.Empty;
                this.txtHaishutsuGenbaCdTO.Text = string.Empty;
                this.txtHaishutsuGenbaNmTO.Text = string.Empty;
            }
        }

        /// <summary>
        /// （返送先）業者_POPUP_BEFイベント
        /// </summary>
        public void txtGYOUSHA_POPUP_BEF()
        {
            txtGYOUSHABef = this.txtGYOUSHA_CD.Text;
        }

        /// <summary>
        /// （返送先）業者_POPUP_AFTイベント
        /// </summary>
        public void txtGYOUSHA_POPUP_AFT()
        {
            txtGYOUSHAAft = this.txtGYOUSHA_CD.Text;
            if (txtGYOUSHABef != txtGYOUSHAAft)
            {
                this.txtGENBA_CD.Text = string.Empty;
                this.txtGENBA_NAME.Text = string.Empty;
                this.txtGENBA_CD_TO.Text = string.Empty;
                this.txtGENBA_NAME_TO.Text = string.Empty;
            }
        }

        /// <summary>
        /// （返送先）業者TO_POPUP_BEFイベント
        /// </summary>
        public void txtGYOUSHA_TO_POPUP_BEF()
        {
            txtGYOUSHAToBef = this.txtGYOUSHA_CD_TO.Text;
        }

        /// <summary>
        /// （返送先）業者TO_POPUP_AFTイベント
        /// </summary>
        public void txtGYOUSHA_TO_POPUP_AFT()
        {
            txtGYOUSHAToAft = this.txtGYOUSHA_CD_TO.Text;
            if (txtGYOUSHAToBef != txtGYOUSHAToAft)
            {
                this.txtGENBA_CD.Text = string.Empty;
                this.txtGENBA_NAME.Text = string.Empty;
                this.txtGENBA_CD_TO.Text = string.Empty;
                this.txtGENBA_NAME_TO.Text = string.Empty;
            }
        }
    }
}