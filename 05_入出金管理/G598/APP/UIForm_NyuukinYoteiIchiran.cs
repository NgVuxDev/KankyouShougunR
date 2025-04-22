using System;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.CustomControl;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Data;

namespace Shougun.Core.ReceiptPayManagement.NyuukinYoteiIchiran
{
    /// <summary>
    /// 入金予定一覧表出力画面クラス
    /// </summary>
    public partial class UIForm_NyuukinYoteiIchiran : SuperForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private NyuukinYoteiIchiranLogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">ウィンドウID</param>
        public UIForm_NyuukinYoteiIchiran(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new NyuukinYoteiIchiranLogicClass(this);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面内の項目を初期化します
        /// </summary>
        public void Initialize()
        {
            LogUtility.DebugMethodStart();

            // 拠点CD
            this.KYOTEN_CD.Text = "99";
            // 拠点
            this.KYOTEN_NAME.Text = "全社";
            // 入金予定日From
            this.NYUUKIN_YOTEI_FROM.Text = String.Empty;
            // 入金予定日To
            this.NYUUKIN_YOTEI_TO.Text = String.Empty;
            // 請求日From
            this.SEIKYUU_DATE_FROM.Text = String.Empty;
            // 請求日To
            this.SEIKYUU_DATE_TO.Text = String.Empty;
            // 営業者CDFrom
            this.EIGYOUSHA_CD_FROM.Text = String.Empty;
            // 営業者From
            this.EIGYOUSHA_NAME_FROM.Text = String.Empty;
            // 営業者CDTo
            this.EIGYOUSHA_CD_TO.Text = String.Empty;
            // 営業者To
            this.EIGYOUSHA_NAME_TO.Text = String.Empty;
            // 取引先CDFrom
            this.TORIHIKISAKI_CD_FROM.Text = String.Empty;
            // 取引先From
            this.TORIHIKISAKI_NAME_FROM.Text = String.Empty;
            // 取引先CDTo
            this.TORIHIKISAKI_CD_TO.Text = String.Empty;
            // 取引先To
            this.TORIHIKISAKI_NAME_TO.Text = String.Empty;
            // 並び順１
            this.SORT_1_KOUMOKU.Text = "1";
            // 並び順２
            this.SORT_2_KOUMOKU.Text = "1";
            // 集計単位（伝票）
            this.GROUP_EIGYOUSHA.Checked = true;
            // 集計単位（取引先）
            this.GROUP_TORIHIKISAKI.Checked = true;
            // 集計単位（入金予定日）
            this.GROUP_NYUUKIN_YOTEI.Checked = false;

            // 請求書書式
            this.SHOSHIKI_NUM.Text = ConstClass.SHOSHIKI_T.ToString();
            // 入金消込状況を反映
            this.NYUUKIN_KESHIGOMU_JOUKYOU.Text = ConstClass.NYUUKIN_KESHIGOMU_JOUKYOU_SURU.ToString();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Cursor.Current = Cursors.WaitCursor;

            if (!this.SetTorihikisakiCd())
            {
                return;
            }

            // 登録時チェックロジックを実行
            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();

            // 入金予定日・請求日のいずれかを必須項目とする
            var isEmptyYotei = false;
            var isEmptySeikyu = false;
            var yoteiFrom = this.NYUUKIN_YOTEI_FROM.Text;
            var yoteiTo = this.NYUUKIN_YOTEI_TO.Text;
            var seikyuFrom = this.SEIKYUU_DATE_FROM.Text;
            var seikyuTo = this.SEIKYUU_DATE_TO.Text;
            if (String.IsNullOrEmpty(yoteiFrom) || String.IsNullOrEmpty(yoteiTo))
            {
                isEmptyYotei = true;
            }
            if (String.IsNullOrEmpty(seikyuFrom) || String.IsNullOrEmpty(seikyuTo))
            {
                isEmptySeikyu = true;
            }
            if (isEmptyYotei && isEmptySeikyu)
            {
                if (String.IsNullOrEmpty(yoteiFrom))
                {
                    this.NYUUKIN_YOTEI_FROM.IsInputErrorOccured = true;
                }
                if (String.IsNullOrEmpty(yoteiTo))
                {
                    this.NYUUKIN_YOTEI_TO.IsInputErrorOccured = true;
                }
                if (String.IsNullOrEmpty(seikyuFrom))
                {
                    this.SEIKYUU_DATE_FROM.IsInputErrorOccured = true;
                }
                if (String.IsNullOrEmpty(seikyuTo))
                {
                    this.SEIKYUU_DATE_TO.IsInputErrorOccured = true;
                }

                new MessageBoxShowLogic().MessageBoxShow("E001", "入金予定日、請求日のいずれか");

                this.RegistErrorFlag = true;
            }

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.EIGYOUSHA_CD_FROM.CausesValidation = true;
                this.EIGYOUSHA_CD_TO.CausesValidation = true;
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;

                this.NYUUKIN_YOTEI_FROM.CausesValidation = true;
                this.NYUUKIN_YOTEI_TO.CausesValidation = true;
                this.SEIKYUU_DATE_FROM.CausesValidation = true;
                this.SEIKYUU_DATE_TO.CausesValidation = true;

                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
            }
            else
            {
                var dto = new NyuukinYoteiIchiranDtoClass();
                dto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                dto.KyotenName = this.KYOTEN_NAME.Text;
                if (!String.IsNullOrEmpty(this.NYUUKIN_YOTEI_FROM.Text))
                {
                    dto.NyuukinYoteiDateFrom = ((DateTime)this.NYUUKIN_YOTEI_FROM.Value).ToString("yyyy/MM/dd");
                }
                if (!String.IsNullOrEmpty(this.NYUUKIN_YOTEI_TO.Text))
                {
                    dto.NyuukinYoteiDateTo = ((DateTime)this.NYUUKIN_YOTEI_TO.Value).ToString("yyyy/MM/dd");
                }
                if (!String.IsNullOrEmpty(this.SEIKYUU_DATE_FROM.Text))
                {
                    dto.SeikyuuDateFrom = ((DateTime)this.SEIKYUU_DATE_FROM.Value).ToString("yyyy/MM/dd");
                }
                if (!String.IsNullOrEmpty(this.SEIKYUU_DATE_TO.Text))
                {
                    dto.SeikyuuDateTo = ((DateTime)this.SEIKYUU_DATE_TO.Value).ToString("yyyy/MM/dd");
                }
                // 入金予定日・請求日はFromおよびToが両方入力されている項目を条件に使用する
                if (isEmptyYotei)
                {
                    dto.NyuukinYoteiDateFrom = null;
                    dto.NyuukinYoteiDateTo = null;
                }
                if (isEmptySeikyu)
                {
                    dto.SeikyuuDateFrom = null;
                    dto.SeikyuuDateTo = null;
                }
                dto.NyuukinKeshigomuJoukyou = Int32.Parse(this.NYUUKIN_KESHIGOMU_JOUKYOU.Text);
                dto.EigyoushaCdFrom = this.EIGYOUSHA_CD_FROM.Text;
                dto.EigyoushaFrom = this.EIGYOUSHA_NAME_FROM.Text;
                dto.EigyoushaCdTo = this.EIGYOUSHA_CD_TO.Text;
                dto.EigyoushaTo = this.EIGYOUSHA_NAME_TO.Text;
                dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                dto.Sort1 = Int32.Parse(this.SORT_1_KOUMOKU.Text);
                if (string.IsNullOrEmpty(this.SORT_2_KOUMOKU.Text))
                {
                    dto.Sort2 = 0;
                }
                else
                {
                    dto.Sort2 = Int32.Parse(this.SORT_2_KOUMOKU.Text);
                }
                dto.IsGroupEigyousha = this.GROUP_EIGYOUSHA.Checked;
                dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;
                dto.IsGroupNyuukinYoteiBi = this.GROUP_NYUUKIN_YOTEI.Checked;

                this.logic.CSV(dto);
            }

            Cursor.Current = Cursors.Arrow;

            LogUtility.DebugMethodEnd();
        }

        public void CsvReport(DataTable dt)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertDataTableToCsv(dt, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU), this);
            }
        }

        /// <summary>
        /// 表示ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Cursor.Current = Cursors.WaitCursor;

            if (!this.SetTorihikisakiCd())
            {
                return;
            }

            // 登録時チェックロジックを実行
            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();

            // 入金予定日・請求日のいずれかを必須項目とする
            var isEmptyYotei = false;
            var isEmptySeikyu = false;
            var yoteiFrom = this.NYUUKIN_YOTEI_FROM.Text;
            var yoteiTo = this.NYUUKIN_YOTEI_TO.Text;
            var seikyuFrom = this.SEIKYUU_DATE_FROM.Text;
            var seikyuTo = this.SEIKYUU_DATE_TO.Text;
            if (String.IsNullOrEmpty(yoteiFrom) || String.IsNullOrEmpty(yoteiTo))
            {
                isEmptyYotei = true;
            }
            if (String.IsNullOrEmpty(seikyuFrom) || String.IsNullOrEmpty(seikyuTo))
            {
                isEmptySeikyu = true;
            }
            if (isEmptyYotei && isEmptySeikyu)
            {
                if (String.IsNullOrEmpty(yoteiFrom))
                {
                    this.NYUUKIN_YOTEI_FROM.IsInputErrorOccured = true;
                }
                if (String.IsNullOrEmpty(yoteiTo))
                {
                    this.NYUUKIN_YOTEI_TO.IsInputErrorOccured = true;
                }
                if (String.IsNullOrEmpty(seikyuFrom))
                {
                    this.SEIKYUU_DATE_FROM.IsInputErrorOccured = true;
                }
                if (String.IsNullOrEmpty(seikyuTo))
                {
                    this.SEIKYUU_DATE_TO.IsInputErrorOccured = true;
                }

                new MessageBoxShowLogic().MessageBoxShow("E001", "入金予定日、請求日のいずれか");

                this.RegistErrorFlag = true;
            }

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.EIGYOUSHA_CD_FROM.CausesValidation = true;
                this.EIGYOUSHA_CD_TO.CausesValidation = true;
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;

                this.NYUUKIN_YOTEI_FROM.CausesValidation = true;
                this.NYUUKIN_YOTEI_TO.CausesValidation = true;
                this.SEIKYUU_DATE_FROM.CausesValidation = true;
                this.SEIKYUU_DATE_TO.CausesValidation = true;

                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
            }
            else
            {
                var dto = new NyuukinYoteiIchiranDtoClass();
                dto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                dto.KyotenName = this.KYOTEN_NAME.Text;
                if (!String.IsNullOrEmpty(this.NYUUKIN_YOTEI_FROM.Text))
                {
                    dto.NyuukinYoteiDateFrom = ((DateTime)this.NYUUKIN_YOTEI_FROM.Value).ToString("yyyy/MM/dd");
                }
                if (!String.IsNullOrEmpty(this.NYUUKIN_YOTEI_TO.Text))
                {
                    dto.NyuukinYoteiDateTo = ((DateTime)this.NYUUKIN_YOTEI_TO.Value).ToString("yyyy/MM/dd");
                }
                if (!String.IsNullOrEmpty(this.SEIKYUU_DATE_FROM.Text))
                {
                    dto.SeikyuuDateFrom = ((DateTime)this.SEIKYUU_DATE_FROM.Value).ToString("yyyy/MM/dd");
                }
                if (!String.IsNullOrEmpty(this.SEIKYUU_DATE_TO.Text))
                {
                    dto.SeikyuuDateTo = ((DateTime)this.SEIKYUU_DATE_TO.Value).ToString("yyyy/MM/dd");
                }
                // 入金予定日・請求日はFromおよびToが両方入力されている項目を条件に使用する
                if (isEmptyYotei)
                {
                    dto.NyuukinYoteiDateFrom = null;
                    dto.NyuukinYoteiDateTo = null;
                }
                if (isEmptySeikyu)
                {
                    dto.SeikyuuDateFrom = null;
                    dto.SeikyuuDateTo = null;
                }
                dto.NyuukinKeshigomuJoukyou = Int32.Parse(this.NYUUKIN_KESHIGOMU_JOUKYOU.Text);
                dto.EigyoushaCdFrom = this.EIGYOUSHA_CD_FROM.Text;
                dto.EigyoushaFrom = this.EIGYOUSHA_NAME_FROM.Text;
                dto.EigyoushaCdTo = this.EIGYOUSHA_CD_TO.Text;
                dto.EigyoushaTo = this.EIGYOUSHA_NAME_TO.Text;
                dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                dto.Sort1 = Int32.Parse(this.SORT_1_KOUMOKU.Text);
                if (string.IsNullOrEmpty(this.SORT_2_KOUMOKU.Text))
                {
                    dto.Sort2 = 0;
                }
                else
                {
                    dto.Sort2 = Int32.Parse(this.SORT_2_KOUMOKU.Text);
                }
                dto.IsGroupEigyousha = this.GROUP_EIGYOUSHA.Checked;
                dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;
                dto.IsGroupNyuukinYoteiBi = this.GROUP_NYUUKIN_YOTEI.Checked;

                this.logic.Search(dto);
            }

            Cursor.Current = Cursors.Arrow;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じるボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.logic.WindowInit())
            {
                return;
            }

            this.Initialize();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 並び順１のテキストボックスのテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SORT_1_KOUMOKU_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (ConstClass.SORT_1_EIGYOUSHA.ToString() == this.SORT_1_KOUMOKU.Text)
            {
                this.GROUP_EIGYOUSHA.Checked = true;
                this.GROUP_EIGYOUSHA.Enabled = true;
                this.GROUP_NYUUKIN_YOTEI.Checked = false;
                this.GROUP_NYUUKIN_YOTEI.Enabled = false;
                if (!this.SORT_2_KOUMOKU.Enabled)
                {
                    this.SORT_2_KOUMOKU.Text = "1";
                    this.SORT_2_KOUMOKU.Enabled = true;
                    this.SORT_2_KOUMOKU_1.Enabled = true;
                    this.SORT_2_KOUMOKU_2.Enabled = true;
                    this.GROUP_TORIHIKISAKI.Checked = true;
                    this.GROUP_TORIHIKISAKI.Enabled = true;
                }
            }
            else if (ConstClass.SORT_1_NYUUKIN_YOTEI_BI.ToString() == this.SORT_1_KOUMOKU.Text)
            {
                this.SORT_2_KOUMOKU.Text = string.Empty;
                this.SORT_2_KOUMOKU.Enabled = false;
                this.SORT_2_KOUMOKU_1.Checked = false;
                this.SORT_2_KOUMOKU_1.Enabled = false;
                this.SORT_2_KOUMOKU_2.Checked = false;
                this.SORT_2_KOUMOKU_2.Enabled = false;
                if (!this.SHOSHIKI_G.Checked)
                {
                    this.GROUP_EIGYOUSHA.Checked = false;
                    this.GROUP_EIGYOUSHA.Enabled = true;
                }
                else
                {
                    this.GROUP_EIGYOUSHA.Checked = false;
                    this.GROUP_EIGYOUSHA.Enabled = false;
                }
                this.GROUP_TORIHIKISAKI.Checked = true;
                this.GROUP_TORIHIKISAKI.Enabled = true;
                this.GROUP_NYUUKIN_YOTEI.Checked = true;
                this.GROUP_NYUUKIN_YOTEI.Enabled = true;
            }
            else if (ConstClass.SORT_1_TORIHIKISAKI.ToString() == this.SORT_1_KOUMOKU.Text)
            {
                this.GROUP_EIGYOUSHA.Checked = false;
                this.GROUP_EIGYOUSHA.Enabled = false;
                this.GROUP_NYUUKIN_YOTEI.Checked = false;
                this.GROUP_NYUUKIN_YOTEI.Enabled = false;
                if (!this.SORT_2_KOUMOKU.Enabled)
                {
                    this.SORT_2_KOUMOKU.Text = "1";
                    this.SORT_2_KOUMOKU.Enabled = true;
                    this.SORT_2_KOUMOKU_1.Enabled = true;
                    this.SORT_2_KOUMOKU_2.Enabled = true;
                    this.GROUP_TORIHIKISAKI.Checked = true;
                    this.GROUP_TORIHIKISAKI.Enabled = true;
                }
            }
            else
            {
                this.GROUP_EIGYOUSHA.Enabled = true;
                this.GROUP_NYUUKIN_YOTEI.Enabled = true;
                this.GROUP_TORIHIKISAKI.Enabled = true;
                if (!this.SORT_2_KOUMOKU.Enabled)
                {
                    this.SORT_2_KOUMOKU.Text = "1";
                    this.SORT_2_KOUMOKU.Enabled = true;
                    this.SORT_2_KOUMOKU_1.Enabled = true;
                    this.SORT_2_KOUMOKU_2.Enabled = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 営業者CDToのテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void EIGYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var eigyoushaCdFromTextBox = this.EIGYOUSHA_CD_FROM;
            var eigyoushaNameFromTextBox = this.EIGYOUSHA_NAME_FROM;
            var eigyoushaCdToTextBox = this.EIGYOUSHA_CD_TO;
            var eigyoushaNameToTextBox = this.EIGYOUSHA_NAME_TO;

            //if (!String.IsNullOrEmpty(eigyoushaCdFromTextBox.Text) && String.IsNullOrEmpty(eigyoushaCdToTextBox.Text))
            //{
                eigyoushaCdToTextBox.Text = eigyoushaCdFromTextBox.Text;
                eigyoushaNameToTextBox.Text = eigyoushaNameFromTextBox.Text;
            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDToのテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var torihikisakiCdFromTextBox = this.TORIHIKISAKI_CD_FROM;
            var torihikisakiNameFromTextBox = this.TORIHIKISAKI_NAME_FROM;
            var torihikisakiCdToTextBox = this.TORIHIKISAKI_CD_TO;
            var torihikisakiNameToTextBox = this.TORIHIKISAKI_NAME_TO;

            //if (!String.IsNullOrEmpty(torihikisakiCdFromTextBox.Text) && String.IsNullOrEmpty(torihikisakiCdToTextBox.Text))
            //{
                torihikisakiCdToTextBox.Text = torihikisakiCdFromTextBox.Text;
                torihikisakiNameToTextBox.Text = torihikisakiNameFromTextBox.Text;
            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDをセットします（取引先CDが入力されていないとき）
        /// </summary>
        private bool SetTorihikisakiCd()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 取引先が入力可で、入力されていなければ自動入力する
                if (this.TORIHIKISAKI_CD_FROM.Enabled && this.TORIHIKISAKI_CD_TO.Enabled)
                {
                    var torihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                    var torihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                    if (String.IsNullOrEmpty(torihikisakiCdFrom) && String.IsNullOrEmpty(torihikisakiCdTo))
                    {
                        var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                        var mTorihikisakiList = dao.GetAllValidData(new M_TORIHIKISAKI());
                        var firstTorihikisaki = mTorihikisakiList.OrderBy(t => t.TORIHIKISAKI_CD).FirstOrDefault();
                        var lastTorihikisaki = mTorihikisakiList.OrderBy(t => t.TORIHIKISAKI_CD).LastOrDefault();
                        if (null != firstTorihikisaki)
                        {
                            this.TORIHIKISAKI_CD_FROM.Text = firstTorihikisaki.TORIHIKISAKI_CD;
                            this.TORIHIKISAKI_NAME_FROM.Text = firstTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                        if (null != lastTorihikisaki)
                        {
                            this.TORIHIKISAKI_CD_TO.Text = lastTorihikisaki.TORIHIKISAKI_CD;
                            this.TORIHIKISAKI_NAME_TO.Text = lastTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetTorihikisakiCd", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisakiCd", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        private void SHOSHIKI_T_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SHOSHIKI_T.Checked)
            {
                this.SORT_1_KOUMOKU.RangeSetting.Max = 3;
                this.SORT_1_KOUMOKU.RangeSetting.Min = 1;
                this.SORT_1_KOUMOKU.Tag = "【1～3】のいずれかで入力してください";
                this.SORT_1_KOUMOKU_1.Enabled = true;
                this.SORT_1_KOUMOKU_2.Enabled = true;
                this.SORT_1_KOUMOKU_3.Enabled = true;
                this.SORT_1_KOUMOKU_1.Checked = true;
                this.SORT_1_KOUMOKU_2.Checked = false;
                this.SORT_1_KOUMOKU_3.Checked = false;
            }
        }

        private void SHOSHIKI_G_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SHOSHIKI_G.Checked)
            {
                this.SORT_1_KOUMOKU.RangeSetting.Max = 3;
                this.SORT_1_KOUMOKU.RangeSetting.Min = 2;
                this.SORT_1_KOUMOKU.Tag = "【2～3】のいずれかで入力してください";
                this.SORT_1_KOUMOKU_1.Enabled = false;
                this.SORT_1_KOUMOKU_2.Enabled = true;
                this.SORT_1_KOUMOKU_3.Enabled = true;
                this.SORT_1_KOUMOKU_1.Checked = false;
                this.SORT_1_KOUMOKU_2.Checked = true;
                this.SORT_1_KOUMOKU_3.Checked = false;
            }
        }

        private void SHOSHIKI_NUM_TextChanged(object sender, EventArgs e)
        {
            if (ConstClass.SHOSHIKI_T.ToString() == this.SHOSHIKI_NUM.Text)
            {
                this.SHOSHIKI_T.Checked = true;
            }
            else if (ConstClass.SHOSHIKI_G.ToString() == this.SHOSHIKI_NUM.Text)
            {
                this.SHOSHIKI_G.Checked = true;
            }
            else
            {
                this.SHOSHIKI_T.Checked = false;
                this.SHOSHIKI_G.Checked = false;
                this.SORT_1_KOUMOKU.RangeSetting.Max = 3;
                this.SORT_1_KOUMOKU.RangeSetting.Min = 1;
                this.SORT_1_KOUMOKU.Tag = "【1～3】のいずれかで入力してください";
                this.SORT_1_KOUMOKU_1.Enabled = true;
            }
        }

        private void GROUP_EIGYOUSHA_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GROUP_EIGYOUSHA.Checked && this.SORT_1_KOUMOKU_3.Checked)
            {
                this.GROUP_TORIHIKISAKI.Checked = false;
            }
        }

        private void GROUP_TORIHIKISAKI_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GROUP_TORIHIKISAKI.Checked && this.SORT_1_KOUMOKU_3.Checked)
            {
                this.GROUP_EIGYOUSHA.Checked = false;
            }
        }
    }
}
