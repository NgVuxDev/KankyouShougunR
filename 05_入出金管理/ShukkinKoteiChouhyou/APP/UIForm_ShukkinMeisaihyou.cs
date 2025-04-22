using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Data;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKoteiChouhyou
{
    /// <summary>
    /// 出金明細表出力画面クラス
    /// </summary>
    public partial class UIForm_ShukkinMeisaihyou : SuperForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private ShukkinMeisaihyouLogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">ウィンドウID</param>
        public UIForm_ShukkinMeisaihyou(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new ShukkinMeisaihyouLogicClass(this);

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
            // 日付CD
            this.HIDUKE_SHURUI.Text = "1";
            // 日付範囲
            this.HIDUKE.Text = "1";
            // 日付From
            this.HIDUKE_FROM.Value = this.logic.parentForm.sysDate;
            // 日付To
            this.HIDUKE_TO.Value = this.logic.parentForm.sysDate;
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
            // 集計単位（伝票）
            this.GROUP_DENPYOU.Checked = true;
            // 集計単位（取引先）
            this.GROUP_TORIHIKISAKI.Checked = true;
            // 集計単位（出金区分）
            this.GROUP_NYUUSHUKKIN_KBN.Checked = false;

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

            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;
            }
            else
            {
                if (this.logic.DateCheck())
                {
                    Cursor.Current = Cursors.Arrow;

                    LogUtility.DebugMethodEnd();
                    return;
                }
                var dto = new ShukkinMeisaihyouDtoClass();
                dto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                dto.KyotenName = this.KYOTEN_NAME.Text;
                dto.DateShuruiCd = Int32.Parse(this.HIDUKE_SHURUI.Text);
                dto.DateFrom = ((DateTime)this.HIDUKE_FROM.Value).ToString("yyyy/MM/dd");
                dto.DateTo = ((DateTime)this.HIDUKE_TO.Value).ToString("yyyy/MM/dd");
                dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                dto.Sort1 = Int32.Parse(this.SORT_1_KOUMOKU.Text);
                dto.IsGroupDenpyouNumber = this.GROUP_DENPYOU.Checked;
                dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;
                dto.IsGroupNyuushukkinKbn = this.GROUP_NYUUSHUKKIN_KBN.Checked;

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
                csvExport.ConvertDataTableToCsv(dt, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_SYUKKINN_MEISAIHYOU), this);
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

            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;
            }
            else
            {
                /// 20141024 Houkakou 「出金明細表」の日付チェックを追加する　start
                if (this.logic.DateCheck())
                {
                    Cursor.Current = Cursors.Arrow;

                    LogUtility.DebugMethodEnd();
                    return;
                }
                /// 20141024 Houkakou 「出金明細表」の日付チェックを追加する　end
                var dto = new ShukkinMeisaihyouDtoClass();
                dto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                dto.KyotenName = this.KYOTEN_NAME.Text;
                dto.DateShuruiCd = Int32.Parse(this.HIDUKE_SHURUI.Text);
                dto.DateFrom = ((DateTime)this.HIDUKE_FROM.Value).ToString("yyyy/MM/dd");
                dto.DateTo = ((DateTime)this.HIDUKE_TO.Value).ToString("yyyy/MM/dd");
                dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                dto.Sort1 = Int32.Parse(this.SORT_1_KOUMOKU.Text);
                dto.IsGroupDenpyouNumber = this.GROUP_DENPYOU.Checked;
                dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;
                dto.IsGroupNyuushukkinKbn = this.GROUP_NYUUSHUKKIN_KBN.Checked;

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
        /// 日付範囲テキストボックスのテキストが変更された時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HIDUKE_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hiduke = this.HIDUKE.Text;
            if (!String.IsNullOrEmpty(hiduke))
            {
                var fromTextBox = this.HIDUKE_FROM;
                var toTextBox = this.HIDUKE_TO;

                // 期間指定の時だけ入力可なので、入力不可に初期化しておく
                fromTextBox.Enabled = false;
                toTextBox.Enabled = false;

                switch (hiduke)
                {
                    case "1":
                        // 当日
                        fromTextBox.Value = this.logic.parentForm.sysDate;
                        toTextBox.Value = this.logic.parentForm.sysDate;
                        break;
                    case "2":
                        // 当月
                        fromTextBox.Value = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, 1);
                        toTextBox.Value = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, 1).AddMonths(1).AddDays(-1);
                        break;
                    case "3":
                        // 期間指定
                        fromTextBox.Text = String.Empty;
                        toTextBox.Text = String.Empty;
                        fromTextBox.Enabled = true;
                        toTextBox.Enabled = true;
                        break;
                    default:
                        fromTextBox.Text = String.Empty;
                        toTextBox.Text = String.Empty;
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付Toテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hidukeFromTextBox = this.HIDUKE_FROM;
            var hidukeToTextBox = this.HIDUKE_TO;
            //if (!String.IsNullOrEmpty(hidukeFromTextBox.Text) && String.IsNullOrEmpty(hidukeToTextBox.Text))
            //{
                hidukeToTextBox.Text = hidukeFromTextBox.Text;
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

            //if (!String.IsNullOrEmpty(torihikisakiCdFromTextBox.Text))
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

        /// <summary>
        /// 並び順のテキストボックスのテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SORT_1_KOUMOKU_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var sort1Koumoku = this.SORT_1_KOUMOKU.Text;
            if (this.SORT_1_KOUMOKU_5.Checked)
            {
                this.GROUP_NYUUSHUKKIN_KBN.Enabled = true;
                this.GROUP_NYUUSHUKKIN_KBN.Checked = true;
            }
            else
            {
                this.GROUP_NYUUSHUKKIN_KBN.Enabled = false;
                this.GROUP_NYUUSHUKKIN_KBN.Checked = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// 20141023 Houkakou 「出金明細表」の日付チェックを追加する　start
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIDUKE_TO.Text))
            {
                this.HIDUKE_TO.IsInputErrorOccured = false;
                this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIDUKE_FROM.Text))
            {
                this.HIDUKE_FROM.IsInputErrorOccured = false;
                this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141023 Houkakou 「出金明細表」の日付チェックを追加する　end
    }
}
