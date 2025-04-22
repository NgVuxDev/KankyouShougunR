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

namespace Shougun.Core.ReceiptPayManagement.MinyuukinIchiran
{
    /// <summary>
    /// 未入金一覧表出力画面クラス
    /// </summary>
    public partial class UIForm_MinyuukinIchiran : SuperForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private MinyuukinIchiranLogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">ウィンドウID</param>
        public UIForm_MinyuukinIchiran(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new MinyuukinIchiranLogicClass(this);

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
            // 請求書書式
            this.Syosiki.Text = "1";

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV
        /// </summary>
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

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.EIGYOUSHA_CD_FROM.CausesValidation = true;
                this.EIGYOUSHA_CD_TO.CausesValidation = true;
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;

                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
            }
            else
            {
                var dto = new MinyuukinIchiranDtoClass();
                dto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                dto.KyotenName = this.KYOTEN_NAME.Text;
                dto.Syosiki = Int32.Parse(this.Syosiki.Text);
                dto.EigyoushaCdFrom = this.EIGYOUSHA_CD_FROM.Text;
                dto.EigyoushaFrom = this.EIGYOUSHA_NAME_FROM.Text;
                dto.EigyoushaCdTo = this.EIGYOUSHA_CD_TO.Text;
                dto.EigyoushaTo = this.EIGYOUSHA_NAME_TO.Text;
                dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                dto.Sort1 = Int32.Parse(this.SORT_1_KOUMOKU.Text);
                dto.Sort2 = Int32.Parse(this.SORT_2_KOUMOKU.Text);
                dto.IsGroupEigyousha = this.GROUP_EIGYOUSHA.Checked;
                dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;

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
                csvExport.ConvertDataTableToCsv(dt, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU), this);
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

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.EIGYOUSHA_CD_FROM.CausesValidation = true;
                this.EIGYOUSHA_CD_TO.CausesValidation = true;
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;

                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
            }
            else
            {
                var dto = new MinyuukinIchiranDtoClass();
                dto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                dto.KyotenName = this.KYOTEN_NAME.Text;
                dto.Syosiki = Int32.Parse(this.Syosiki.Text);
                dto.EigyoushaCdFrom = this.EIGYOUSHA_CD_FROM.Text;
                dto.EigyoushaFrom = this.EIGYOUSHA_NAME_FROM.Text;
                dto.EigyoushaCdTo = this.EIGYOUSHA_CD_TO.Text;
                dto.EigyoushaTo = this.EIGYOUSHA_NAME_TO.Text;
                dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                dto.Sort1 = Int32.Parse(this.SORT_1_KOUMOKU.Text);
                dto.Sort2 = Int32.Parse(this.SORT_2_KOUMOKU.Text);
                dto.IsGroupEigyousha = this.GROUP_EIGYOUSHA.Checked;
                dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;

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
            }
            else
            {
                this.GROUP_EIGYOUSHA.Checked = false;
                this.GROUP_EIGYOUSHA.Enabled = false;
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
                        var mTorihikisakiList = dao.GetAllValidData(new M_TORIHIKISAKI() { ISNOT_NEED_DELETE_FLG = true });
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

        private void txtSyosiki_TextChanged(object sender, EventArgs e)
        {
            this.logic.Syosiki_TextChanged();
        }
    }
}
