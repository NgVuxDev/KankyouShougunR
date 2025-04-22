using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Const;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Logic;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.APP
{
    /// <summary>
    /// ○○入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド・プロパティ

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        ///
        /// </summary>
        internal MessageBoxShowLogic MsgLogic { get { return this.msglogic; } }
        private MessageBoxShowLogic msglogic;

        /// <summary>
        /// 共通情報(SysInfoなど)
        /// </summary>
        internal CommonInformation CommInfo { get; private set; }

        /// <summary>
        /// 親フォーム
        /// </summary>
        internal BusinessBaseForm BaseForm { get; private set; }

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        internal UIHeaderForm HeaderForm { get; private set; }

        /// <summary>
        ///
        /// </summary>
        internal long DenpyouNo { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private bool errorFlag = false;

        private int sharyouCheck = -1;

        private bool clickLock = false;

        private string oldDenshuKbn = "";
        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        private Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>新規用</remarks>
        public UIForm()
            : base(WINDOW_ID.T_DENPYOU_IKKATU_UPDATE, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.InitializeComponent();

                CommonShogunData.Create(SystemProperty.Shain.CD);
                // メッセージロジック
                this.msglogic = new MessageBoxShowLogic();
                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

                // 完全に固定
                // ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region イベント

        #region オーバーライド

        #region 画面ロード

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            try
            {
                this.BaseForm = this.Parent as BusinessBaseForm;
                this.HeaderForm = this.BaseForm.headerForm as UIHeaderForm;
                this.CommInfo = (this.BaseForm.ribbonForm as RibbonMainMenu).GlobalCommonInformation;

                // 画面情報の初期化
                this.logic.WindowInit();
                base.HeaderFormInit();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.mrDetail != null)
                {
                    this.mrDetail.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面表示時の制御
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);

            if (!isShown)
            {
                this.Height -= 7;
                this.isShown = true;
            }
        }

        #endregion

        #endregion

        #region ファンクションボタン

        /// <summary>
        /// [F1]一括入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.mrDetail.RowCount < 1)
            {
                this.msglogic.MessageBoxShow("E051", "一括入力する伝票データ");
                return;
            }

            bool checkFlg = false;
            foreach (Row dgvRow in this.mrDetail.Rows)
            {
                if (dgvRow.Cells[ConstCls.COLUMN_CHECK].Value != null && (bool)dgvRow.Cells[ConstCls.COLUMN_CHECK].Value == true)
                {
                    checkFlg = true;
                    break;
                }
            }
            if (!checkFlg)
            {
                this.msglogic.MessageBoxShow("E051", "一括入力する伝票データ");
                return;
            }

            this.logic.ShowPopUp();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F7]条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ControlInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F8]検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_TO.IsInputErrorOccured = false;

            Control[] allControlControls = { this.HIDUKE_FROM, this.HIDUKE_TO, this.KYOTEN_CD, this.txtKakuteiKbn, this.txtDenshuKbn };
            var autoCheckLogic = new AutoRegistCheckLogic(allControlControls, allControlControls);
            this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (this.RegistErrorFlag)
            {
                //必須チェックエラーフォーカス処理
                this.logic.SetErrorFocus();
                return;
            }

            DateTime dtpFrom = DateTime.Parse(this.HIDUKE_FROM.GetResultText());
            DateTime dtpTo = DateTime.Parse(this.HIDUKE_TO.GetResultText());
            DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
            DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

            int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

            if (0 < diff)
            {
                //対象期間内でないならエラーメッセージ表示
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                this.HIDUKE_FROM.IsInputErrorOccured = true;
                this.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.HIDUKE_TO.IsInputErrorOccured = true;
                this.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                this.msglogic.MessageBoxShow("E030", "伝票日付From", "伝票日付To");
                this.HIDUKE_FROM.Select();
                this.HIDUKE_FROM.Focus();
                return;
            }

            this.logic.map.Clear();
            // 明細クリア
            this.mrDetail.Rows.Clear();

            if (this.txtDenshuKbn.Text == "1")
            {
                this.mrDetail.Template = this.mrDetailUkeireTemplate;
            }
            else if (this.txtDenshuKbn.Text == "2")
            {
                this.mrDetail.Template = this.mrDetailShukkaTemplate;
            }
            else if (this.txtDenshuKbn.Text == "3")
            {
                this.mrDetail.Template = this.mrDetailUrshTemplate;
            }

            int Count = this.logic.Search();

            if (Count == 0)
            {
                this.msglogic.MessageBoxShow("C001");
                return;
            }

            this.logic.DisplayEntitesToForm();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F9]登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// 表示中のデータの新規登録、修正又は削除を行い、DBに反映する
        /// 登録完了後、データを再検索し表示する
        /// </remarks>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var allControlAndHeaderControls = this.controlUtil.GetAllControls(this.HeaderForm).ToList();
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                if (this.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.logic.SetErrorFocus();
                    return;
                }

                if (this.HeaderForm.txtCsvOutputKbn.Text == "1")
                {
                    if (string.IsNullOrEmpty(this.logic.csvPath))
                    {
                        this.msglogic.MessageBoxShow("E274");
                        return;
                    }
                    else
                    {
                        if (!Directory.Exists(this.logic.csvPath))
                        {
                            this.msglogic.MessageBoxShow("E274");
                            return;
                        }
                    }
                }

                if (this.mrDetail.RowCount < 1)
                {
                    this.msglogic.MessageBoxShow("E051", "更新する伝票データ");
                    return;
                }

                bool checkFlg = false;
                List<string> messageList = new List<string>();
                foreach (Row dgvRow in this.mrDetail.Rows)
                {
                    foreach (Cell cell in dgvRow.Cells)
                    {
                        cell.UpdateBackColor(cell.Selected);
                    }
                    if (dgvRow.Cells[ConstCls.COLUMN_CHECK].Value != null && (bool)dgvRow.Cells[ConstCls.COLUMN_CHECK].Value == true)
                    {
                        checkFlg = true;
                        if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_KYOTEN_CD].Value)))
                        {
                            dgvRow.Cells[ConstCls.COLUMN_KYOTEN_CD].Style.BackColor = Constans.ERROR_COLOR;
                            var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                            var msgStr = msg.Text.Replace("{0}", "拠点");
                            if (!messageList.Contains(msgStr))
                            {
                                messageList.Add(msgStr);
                            }
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value)))
                        {
                            dgvRow.Cells[ConstCls.COLUMN_DENPYOU_DATE].Style.BackColor = Constans.ERROR_COLOR;
                            var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                            var msgStr = msg.Text.Replace("{0}", "伝票日付");
                            if (!messageList.Contains(msgStr))
                            {
                                messageList.Add(msgStr);
                            }
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_URIAGE_DATE].Value)))
                        {
                            bool detailFlg = false;
                            switch (this.txtDenshuKbn.Text)
                            {
                                case "1":
                                    detailFlg = this.logic.tuResult[dgvRow.Index].detailList.Where(x => x.DENPYOU_KBN_CD.Value == 1).ToList().Count > 0;
                                    break;
                                case "2":
                                    detailFlg = this.logic.tsResult[dgvRow.Index].detailList.Where(x => x.DENPYOU_KBN_CD.Value == 1).ToList().Count > 0;
                                    break;
                                case "3":
                                    break;
                            }
                            if (detailFlg)
                            {
                                dgvRow.Cells[ConstCls.COLUMN_URIAGE_DATE].Style.BackColor = Constans.ERROR_COLOR;
                                var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                                var msgStr = msg.Text.Replace("{0}", "売上日付");
                                if (!messageList.Contains(msgStr))
                                {
                                    messageList.Add(msgStr);
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value)))
                        {
                            bool detailFlg = false;
                            switch (this.txtDenshuKbn.Text)
                            {
                                case "1":
                                    detailFlg = this.logic.tuResult[dgvRow.Index].detailList.Where(x => x.DENPYOU_KBN_CD.Value == 2).ToList().Count > 0;
                                    break;
                                case "2":
                                    detailFlg = this.logic.tsResult[dgvRow.Index].detailList.Where(x => x.DENPYOU_KBN_CD.Value == 2).ToList().Count > 0;
                                    break;
                                case "3":
                                    break;
                            }
                            if (detailFlg)
                            {
                                dgvRow.Cells[ConstCls.COLUMN_SHIHARAI_DATE].Style.BackColor = Constans.ERROR_COLOR;
                                var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                                var msgStr = msg.Text.Replace("{0}", "支払日付");
                                if (!messageList.Contains(msgStr))
                                {
                                    messageList.Add(msgStr);
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value)))
                        {
                            dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Style.BackColor = Constans.ERROR_COLOR;
                            var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                            var msgStr = msg.Text.Replace("{0}", "取引先");
                            if (!messageList.Contains(msgStr))
                            {
                                messageList.Add(msgStr);
                            }
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value)))
                        {
                            dgvRow.Cells[ConstCls.COLUMN_GYOUSHA_CD].Style.BackColor = Constans.ERROR_COLOR;
                            var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                            var msgStr = msg.Text.Replace("{0}", "業者");
                            if (!messageList.Contains(msgStr))
                            {
                                messageList.Add(msgStr);
                            }
                        }

                        if (dgvRow.Cells[ConstCls.COLUMN_GENBA_CD].Value == null
                            || string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_GENBA_CD].Value)))
                        {
                            switch (this.txtDenshuKbn.Text)
                            {
                                case "1":
                                    T_UKEIRE_ENTRY UkeireEntity = this.logic.accessor.getUkeireDataByNumber(Convert.ToInt64(dgvRow.Cells[ConstCls.COLUMN_DENPYOU_NO].Value));

                                    if (UkeireEntity != null)
                                    {
                                        T_CONTENA_RESULT[] UkeireContena = this.logic.accessor.GetContenaData(UkeireEntity.SYSTEM_ID, 1);

                                        if (UkeireContena != null && UkeireContena.Length > 0)
                                        {
                                            dgvRow.Cells[ConstCls.COLUMN_GENBA_CD].Style.BackColor = Constans.ERROR_COLOR;
                                            var msg = Shougun.Core.Message.MessageUtility.GetMessage("E180");
                                            if (!messageList.Contains(msg.Text))
                                            {
                                                messageList.Add(msg.Text);
                                            }
                                        }
                                    }
                                    break;
                                case "2":
                                    break;
                                case "3":
                                    T_UR_SH_ENTRY UrshEntity = this.logic.accessor.getUrshDataByNumber(Convert.ToInt64(dgvRow.Cells[ConstCls.COLUMN_DENPYOU_NO].Value));

                                    if (UrshEntity != null)
                                    {
                                        T_CONTENA_RESULT[] UrshContena = this.logic.accessor.GetContenaData(UrshEntity.SYSTEM_ID, 3);

                                        if (UrshContena != null && UrshContena.Length > 0)
                                        {
                                            dgvRow.Cells[ConstCls.COLUMN_GENBA_CD].Style.BackColor = Constans.ERROR_COLOR;
                                            var msg = Shougun.Core.Message.MessageUtility.GetMessage("E180");
                                            if (!messageList.Contains(msg.Text))
                                            {
                                                messageList.Add(msg.Text);
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                if (messageList.Count > 0)
                {
                    var result = new StringBuilder(256);
                    for (int i = 0; i < messageList.Count; i++)
                    {
                        result.AppendLine(messageList[i]);
                    }
                    MessageBox.Show(result.ToString(), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!checkFlg)
                {
                    this.msglogic.MessageBoxShow("E051", "更新する伝票データ");
                    return;
                }

                switch (this.txtDenshuKbn.Text)
                {
                    case "1":
                        #region 受入

                        for (int i = 0; i < this.mrDetail.Rows.Count; i++)
                        {
                            Row dgvRow = this.mrDetail.Rows[i];
                            foreach (Cell cell in dgvRow.Cells)
                            {
                                cell.UpdateBackColor(cell.Selected);
                            }
                            if (dgvRow.Cells[ConstCls.COLUMN_CHECK].Value != null && (bool)dgvRow.Cells[ConstCls.COLUMN_CHECK].Value == true)
                            {
                                ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> dto = this.logic.tuResult[i];

                                List<int> uriageDetail = new List<int>();
                                List<int> shiharaiDetail = new List<int>();
                                if (dto.detailList.Count > 0)
                                {
                                    for (int k = 0; k < dto.detailList.Count; k++)
                                    {
                                        if (dto.detailList[k].DENPYOU_KBN_CD == 1)
                                        {
                                            uriageDetail.Add(k);
                                        }
                                        else if (dto.detailList[k].DENPYOU_KBN_CD == 2)
                                        {
                                            shiharaiDetail.Add(k);
                                        }
                                    }
                                }
                                // 取引区分
                                if (dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value != null
                                    && !string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value)))
                                {
                                    string torihikisakiCd = Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value);

                                    if (dto.entry.TORIHIKISAKI_CD.Equals(torihikisakiCd))
                                        continue;

                                    M_TORIHIKISAKI_SEIKYUU SeikyuuTorihikiKbn = this.logic.accessor.GetTorihikisakiSeikyuuDataByCode(torihikisakiCd);
                                    M_TORIHIKISAKI_SHIHARAI ShiharaiTorihikiKbn = this.logic.accessor.GetTorihikisakiShiharaiDataByCode(torihikisakiCd);

                                    if (SeikyuuTorihikiKbn != null
                                        && !SeikyuuTorihikiKbn.TORIHIKI_KBN_CD.IsNull
                                        && !dto.entry.URIAGE_TORIHIKI_KBN_CD.IsNull
                                        && SeikyuuTorihikiKbn.TORIHIKI_KBN_CD != dto.entry.URIAGE_TORIHIKI_KBN_CD
                                        && uriageDetail != null
                                        && uriageDetail.Count > 0)
                                    {
                                        dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Style.BackColor = Constans.ERROR_COLOR;

                                        if (SeikyuuTorihikiKbn.TORIHIKI_KBN_CD == 1)
                                            this.msglogic.MessageBoxShow("E277", "掛け", "現金");
                                        else
                                            this.msglogic.MessageBoxShow("E277", "現金", "掛け");

                                        return;
                                    }

                                    if (ShiharaiTorihikiKbn != null
                                        && !ShiharaiTorihikiKbn.TORIHIKI_KBN_CD.IsNull
                                        && !dto.entry.SHIHARAI_TORIHIKI_KBN_CD.IsNull
                                        && ShiharaiTorihikiKbn.TORIHIKI_KBN_CD != dto.entry.SHIHARAI_TORIHIKI_KBN_CD
                                        && shiharaiDetail != null
                                        && shiharaiDetail.Count > 0)
                                    {
                                        dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Style.BackColor = Constans.ERROR_COLOR;

                                        if (SeikyuuTorihikiKbn.TORIHIKI_KBN_CD == 1)
                                            this.msglogic.MessageBoxShow("E277", "掛け", "現金");
                                        else
                                            this.msglogic.MessageBoxShow("E277", "現金", "掛け");

                                        return;
                                    }
                                }
                            }
                        }

                        #endregion
                        break;
                    case "2":
                        #region 出荷

                        for (int i = 0; i < this.mrDetail.Rows.Count; i++)
                        {
                            Row dgvRow = this.mrDetail.Rows[i];
                            foreach (Cell cell in dgvRow.Cells)
                            {
                                cell.UpdateBackColor(cell.Selected);
                            }
                            if (dgvRow.Cells[ConstCls.COLUMN_CHECK].Value != null && (bool)dgvRow.Cells[ConstCls.COLUMN_CHECK].Value == true)
                            {
                                ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> dto = this.logic.tsResult[i];

                                List<int> uriageDetail = new List<int>();
                                List<int> shiharaiDetail = new List<int>();
                                if (dto.detailList.Count > 0)
                                {
                                    for (int k = 0; k < dto.detailList.Count; k++)
                                    {
                                        if (dto.detailList[k].DENPYOU_KBN_CD == 1)
                                        {
                                            uriageDetail.Add(k);
                                        }
                                        else if (dto.detailList[k].DENPYOU_KBN_CD == 2)
                                        {
                                            shiharaiDetail.Add(k);
                                        }
                                    }
                                }
                                // 取引区分
                                if (dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value != null
                                    && !string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value)))
                                {
                                    string torihikisakiCd = Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value);

                                    if (dto.entry.TORIHIKISAKI_CD.Equals(torihikisakiCd))
                                        continue;

                                    M_TORIHIKISAKI_SEIKYUU SeikyuuTorihikiKbn = this.logic.accessor.GetTorihikisakiSeikyuuDataByCode(torihikisakiCd);
                                    M_TORIHIKISAKI_SHIHARAI ShiharaiTorihikiKbn = this.logic.accessor.GetTorihikisakiShiharaiDataByCode(torihikisakiCd);

                                    if (SeikyuuTorihikiKbn != null
                                        && !SeikyuuTorihikiKbn.TORIHIKI_KBN_CD.IsNull
                                        && !dto.entry.URIAGE_TORIHIKI_KBN_CD.IsNull
                                        && SeikyuuTorihikiKbn.TORIHIKI_KBN_CD != dto.entry.URIAGE_TORIHIKI_KBN_CD
                                        && uriageDetail != null
                                        && uriageDetail.Count > 0)
                                    {
                                        dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Style.BackColor = Constans.ERROR_COLOR;

                                        if (SeikyuuTorihikiKbn.TORIHIKI_KBN_CD == 1)
                                            this.msglogic.MessageBoxShow("E277", "掛け", "現金");
                                        else
                                            this.msglogic.MessageBoxShow("E277", "現金", "掛け");

                                        return;
                                    }

                                    if (ShiharaiTorihikiKbn != null
                                        && !ShiharaiTorihikiKbn.TORIHIKI_KBN_CD.IsNull
                                        && !dto.entry.SHIHARAI_TORIHIKI_KBN_CD.IsNull
                                        && ShiharaiTorihikiKbn.TORIHIKI_KBN_CD != dto.entry.SHIHARAI_TORIHIKI_KBN_CD
                                        && shiharaiDetail != null
                                        && shiharaiDetail.Count > 0)
                                    {
                                        dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Style.BackColor = Constans.ERROR_COLOR;

                                        if (SeikyuuTorihikiKbn.TORIHIKI_KBN_CD == 1)
                                            this.msglogic.MessageBoxShow("E277", "掛け", "現金");
                                        else
                                            this.msglogic.MessageBoxShow("E277", "現金", "掛け");

                                        return;
                                    }
                                }
                            }
                        }

                        #endregion
                        break;
                    case "3":
                        #region 売上支払

                        for (int i = 0; i < this.mrDetail.Rows.Count; i++)
                        {
                            Row dgvRow = this.mrDetail.Rows[i];
                            foreach (Cell cell in dgvRow.Cells)
                            {
                                cell.UpdateBackColor(cell.Selected);
                            }
                            if (dgvRow.Cells[ConstCls.COLUMN_CHECK].Value != null && (bool)dgvRow.Cells[ConstCls.COLUMN_CHECK].Value == true)
                            {
                                ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> dto = this.logic.tusResult[i];

                                List<int> uriageDetail = new List<int>();
                                List<int> shiharaiDetail = new List<int>();
                                if (dto.detailList.Count > 0)
                                {
                                    for (int k = 0; k < dto.detailList.Count; k++)
                                    {
                                        if (dto.detailList[k].DENPYOU_KBN_CD == 1)
                                        {
                                            uriageDetail.Add(k);
                                        }
                                        else if (dto.detailList[k].DENPYOU_KBN_CD == 2)
                                        {
                                            shiharaiDetail.Add(k);
                                        }
                                    }
                                }
                                // 取引区分
                                if (dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value != null
                                    && !string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value)))
                                {
                                    string torihikisakiCd = Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value);

                                    if (dto.entry.TORIHIKISAKI_CD.Equals(torihikisakiCd))
                                        continue;

                                    M_TORIHIKISAKI_SEIKYUU SeikyuuTorihikiKbn = this.logic.accessor.GetTorihikisakiSeikyuuDataByCode(torihikisakiCd);
                                    M_TORIHIKISAKI_SHIHARAI ShiharaiTorihikiKbn = this.logic.accessor.GetTorihikisakiShiharaiDataByCode(torihikisakiCd);

                                    if (SeikyuuTorihikiKbn != null
                                        && !SeikyuuTorihikiKbn.TORIHIKI_KBN_CD.IsNull
                                        && !dto.entry.URIAGE_TORIHIKI_KBN_CD.IsNull
                                        && SeikyuuTorihikiKbn.TORIHIKI_KBN_CD != dto.entry.URIAGE_TORIHIKI_KBN_CD
                                        && uriageDetail != null
                                        && uriageDetail.Count > 0)
                                    {
                                        dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Style.BackColor = Constans.ERROR_COLOR;

                                        if (SeikyuuTorihikiKbn.TORIHIKI_KBN_CD == 1)
                                            this.msglogic.MessageBoxShow("E277", "掛け", "現金");
                                        else
                                            this.msglogic.MessageBoxShow("E277", "現金", "掛け");

                                        return;
                                    }

                                    if (ShiharaiTorihikiKbn != null
                                        && !ShiharaiTorihikiKbn.TORIHIKI_KBN_CD.IsNull
                                        && !dto.entry.SHIHARAI_TORIHIKI_KBN_CD.IsNull
                                        && ShiharaiTorihikiKbn.TORIHIKI_KBN_CD != dto.entry.SHIHARAI_TORIHIKI_KBN_CD
                                        && shiharaiDetail != null
                                        && shiharaiDetail.Count > 0)
                                    {
                                        dgvRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Style.BackColor = Constans.ERROR_COLOR;

                                        if (SeikyuuTorihikiKbn.TORIHIKI_KBN_CD == 1)
                                            this.msglogic.MessageBoxShow("E277", "掛け", "現金");
                                        else
                                            this.msglogic.MessageBoxShow("E277", "現金", "掛け");

                                        return;
                                    }
                                }
                            }
                        }
                        
                        #endregion
                        break;
                }

                this.setFunctionEnabled(false);
                if (this.logic.CreateEntites())
                {
                    this.logic.Regist(errorFlag);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                this.setFunctionEnabled(true);
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>表示中のデータに対する修正を全て取消し、画面を閉じる</remarks>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.Close();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [1]伝票明細一括更新ボタンクリック
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            FormManager.OpenFormWithAuth("G685", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        private void setFunctionEnabled(bool enabled)
        {
            this.BaseForm.bt_func1.Enabled = enabled;
            this.BaseForm.bt_func7.Enabled = enabled;
            this.BaseForm.bt_func8.Enabled = enabled;
            this.BaseForm.bt_func9.Enabled = enabled;
            this.BaseForm.bt_func12.Enabled = enabled;
            this.BaseForm.bt_process1.Enabled = enabled;
        }
        #endregion

        #region 取引先イベント

        /// <summary>
        /// 取引先プップでデータを選択後処理
        /// </summary>
        public void TORIHIKISAKI_CD_PopupAfter()
        {
            this.txtControl_Enter(this.TORIHIKISAKI_CD, null);
        }

        /// <summary>
        /// 取引先更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TORIHIKISAKI_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // チェック処理
                if (!this.logic.CheckTorihikisaki())
                {
                    e.Cancel = true;
                    this.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    return;
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

        #region 業者イベント
        /// <summary>
        /// 業者プップ表示前処理
        /// </summary>
        public void GYOUSHA_CD_PopupBefore()
        {
            this.txtControl_Enter(this.GYOUSHA_CD, null);
        }

        /// <summary>
        /// 業者プップでデータを選択後処理
        /// </summary>
        public void GYOUSHA_CD_PopupAfter()
        {
            if (this.GYOUSHA_CD.isChanged())
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                this.txtControl_Enter(this.GYOUSHA_CD, null);
            }
        }

        /// <summary>
        /// 業者検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.GYOUSHA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckGyousha())
                {
                    e.Cancel = true;
                    this.GYOUSHA_CD.IsInputErrorOccured = true;
                    return;
                }

                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 現場イベント

        /// <summary>
        /// 現場プップ表示後処理
        /// </summary>
        public void GENBA_CD_PopupAfter()
        {
            this.txtControl_Enter(this.GENBA_CD, null);
        }

        /// <summary>
        /// 現場検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.GENBA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckGenba())
                {
                    e.Cancel = true;
                    this.GENBA_CD.IsInputErrorOccured = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 運搬業者イベント

        /// <summary>
        /// 運搬業者プップ表示後処理
        /// </summary>
        public void UPN_GYOUSHA_CD_PopupAfter()
        {
            this.txtControl_Enter(this.UPN_GYOUSHA_CD, null);
        }

        /// <summary>
        /// 運搬業者検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UPN_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.UPN_GYOUSHA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckUnpanGyousha())
                {
                    e.Cancel = true;
                    this.UPN_GYOUSHA_CD.IsInputErrorOccured = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 荷積業者イベント

        /// <summary>
        /// 荷積業者プップ表示前処理
        /// </summary>
        public void NIZUMI_GYOUSHA_CD_PopupBefore()
        {
            this.txtControl_Enter(this.NIZUMI_GYOUSHA_CD, null);
        }

        /// <summary>
        /// 荷積業者プップ表示後処理
        /// </summary>
        public void NIZUMI_GYOUSHA_CD_PopupAfter()
        {
            if (this.NIZUMI_GYOUSHA_CD.isChanged())
            {
                this.NIZUMI_GENBA_CD.Text = string.Empty;
                this.NIZUMI_GENBA_NAME_RYAKU.Text = string.Empty;
                this.txtControl_Enter(this.NIZUMI_GYOUSHA_CD, null);
            }
        }

        /// <summary>
        /// 荷積業者検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIZUMI_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.NIZUMI_GYOUSHA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckNizumiGyousha())
                {
                    e.Cancel = true;
                    this.NIZUMI_GYOUSHA_CD.IsInputErrorOccured = true;
                    return;
                }
                this.NIZUMI_GENBA_CD.Text = string.Empty;
                this.NIZUMI_GENBA_NAME_RYAKU.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 荷積現場イベント

        /// <summary>
        /// 荷積現場プップ表示後処理
        /// </summary>
        public void NIZUMI_GENBA_CD_PopupAfter()
        {
            this.txtControl_Enter(this.NIZUMI_GENBA_CD, null);
        }

        /// <summary>
        /// 荷積現場検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIZUMI_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.NIZUMI_GENBA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckNizumiGenba())
                {
                    e.Cancel = true;
                    this.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 荷降業者イベント

        /// <summary>
        /// 荷降業者プップ表示前処理
        /// </summary>
        public void NIOROSHI_GYOUSHA_CD_PopupBefore()
        {
            this.txtControl_Enter(this.NIOROSHI_GYOUSHA_CD, null);
        }

        /// <summary>
        /// 荷降業者プップ表示後処理
        /// </summary>
        public void NIOROSHI_GYOUSHA_CD_PopupAfter()
        {
            if (this.NIOROSHI_GYOUSHA_CD.isChanged())
            {
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;
                this.txtControl_Enter(this.NIOROSHI_GYOUSHA_CD, null);
            }
        }

        /// <summary>
        /// 荷降業者検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIOROSHI_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.NIOROSHI_GYOUSHA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckNioroshiGyousha())
                {
                    e.Cancel = true;
                    this.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = true;
                    return;
                }
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 荷降現場イベント

        /// <summary>
        /// 荷降現場プップ表示後処理
        /// </summary>
        public void NIOROSHI_GENBA_CD_PopupAfter()
        {
            this.txtControl_Enter(this.NIOROSHI_GENBA_CD, null);
        }

        /// <summary>
        /// 荷降現場検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIOROSHI_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.NIOROSHI_GENBA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckNioroshiGenba())
                {
                    e.Cancel = true;
                    this.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 営業担当者イベント

        /// <summary>
        /// 営業担当者プップでデータを選択後処理
        /// </summary>
        public void EIGYOUTANTOU_CD_PopupAfter()
        {
            this.txtControl_Enter(this.EIGYOUTANTOU_CD, null);
        }

        /// <summary>
        /// 営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void EIGYOUTANTOU_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // チェック処理
                if (!this.logic.CheckEigyoutantousha())
                {
                    e.Cancel = true;
                    this.EIGYOUTANTOU_CD.IsInputErrorOccured = true;
                    return;
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

        #region 伝種イベント
        /// <summary>
        /// 伝種TextChange後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtDenshuKbn_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.txtDenshuKbn.TextChanged -= this.txtDenshuKbn_TextChanged;
            try
            {
                if (!this.CheckAuth(this.txtDenshuKbn.Text))
                {
                    this.txtDenshuKbn.Text = this.oldDenshuKbn;
                    return;
                }
                switch (this.txtDenshuKbn.Text)
                {
                    case "1":
                        this.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                        this.NIZUMI_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                        this.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.NIZUMI_GENBA_NAME_RYAKU.Text = string.Empty;

                        this.NIZUMI_GYOUSHA_CD.Enabled = false;
                        this.NIZUMI_GENBA_CD.Enabled = false;
                        this.btnNizumiGyoushaSrch.Enabled = false;
                        this.btnNizumiGenbaSrch.Enabled = false;

                        this.NIOROSHI_GYOUSHA_CD.Enabled = true;
                        this.NIOROSHI_GENBA_CD.Enabled = true;
                        this.btnNioroshiGyoushaSrch.Enabled = true;
                        this.btnNioroshiGenbaSrch.Enabled = true;
                        break;
                    case "2":
                        this.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                        this.NIOROSHI_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                        this.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;

                        this.NIZUMI_GYOUSHA_CD.Enabled = true;
                        this.NIZUMI_GENBA_CD.Enabled = true;
                        this.btnNizumiGyoushaSrch.Enabled = true;
                        this.btnNizumiGenbaSrch.Enabled = true;

                        this.NIOROSHI_GYOUSHA_CD.Enabled = false;
                        this.NIOROSHI_GENBA_CD.Enabled = false;
                        this.btnNioroshiGyoushaSrch.Enabled = false;
                        this.btnNioroshiGenbaSrch.Enabled = false;
                        break;
                    case "3":

                        this.NIZUMI_GYOUSHA_CD.Enabled = true;
                        this.NIZUMI_GENBA_CD.Enabled = true;
                        this.btnNizumiGyoushaSrch.Enabled = true;
                        this.btnNizumiGenbaSrch.Enabled = true;

                        this.NIOROSHI_GYOUSHA_CD.Enabled = true;
                        this.NIOROSHI_GENBA_CD.Enabled = true;
                        this.btnNioroshiGyoushaSrch.Enabled = true;
                        this.btnNioroshiGenbaSrch.Enabled = true;
                        break;
                }
                this.oldDenshuKbn = this.txtDenshuKbn.Text;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                this.txtDenshuKbn.TextChanged += this.txtDenshuKbn_TextChanged;
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region グリッドイベント

        #region 各CELLのフォーカス取得時処理

        /// <summary>
        /// 各CELLのフォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrDetail_CellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (this.mrDetail.CurrentCell == null)
                    return;

                if (this.sharyouCheck >= 0)
                {
                    int rowIndex = this.sharyouCheck;
                    this.sharyouCheck = -1;
                    this.mrDetail.CellValidating -= this.mrDetail_CellValidating;
                    this.mrDetail.CurrentCell = this.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD];
                    this.mrDetail.CurrentCell.Selected = true;
                    this.mrDetail.CellValidating += this.mrDetail_CellValidating;
                }
                else
                {
                    this.txtMr_Enter(this.mrDetail.CurrentCell);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public void Detail_PopAfter()
        {
            if (this.mrDetail.CurrentCell == null)
                return;

            if (this.mrDetail.CurrentCell is GcCustomTextBoxCell)
            {
                if (!(this.mrDetail.CurrentCell as GcCustomTextBoxCell).TextBoxChanged)
                {
                    return;
                }
            }
            string cellName = this.mrDetail.CurrentCell.Name;
            string value = Convert.ToString(this.mrDetail.CurrentCell.Value);
            Row row = this.mrDetail.CurrentRow;
            if (this.beforeValuesForDetail.ContainsKey(cellName) && this.beforeValuesForDetail[cellName] == value)
            {
                return;
            }
            string denpyouDate = Convert.ToString(row.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value);
            switch (cellName)
            {
                case ConstCls.COLUMN_TORIHIKISAKI_CD:
                    break;
                case ConstCls.COLUMN_GYOUSHA_CD:
                    row.Cells[ConstCls.COLUMN_GENBA_CD].Value = string.Empty;
                    row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = string.Empty;
                    break;
                case ConstCls.COLUMN_GENBA_CD:
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value))
                        && string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value)))
                    {
                        string gyoushaCd = Convert.ToString(row.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value);
                        M_GYOUSHA gyousha = this.logic.accessor.GetGyoushaValidDataByCode(gyoushaCd, denpyouDate);
                        if (gyousha != null)
                        {
                            row.Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = gyousha.GYOUSHA_NAME_RYAKU;
                        }
                        M_GENBA genba = this.logic.accessor.GetGenbaValidDataByCode(gyoushaCd, value, denpyouDate);
                        if (genba != null && !string.IsNullOrEmpty(genba.TORIHIKISAKI_CD))
                        {
                            row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = genba.TORIHIKISAKI_CD;
                            M_TORIHIKISAKI torihikisaki = this.logic.accessor.GetTorihikisakiValidDataByCode(genba.TORIHIKISAKI_CD, denpyouDate);
                            if (torihikisaki != null)
                            {
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                            }
                        }
                    }
                    break;
                case ConstCls.COLUMN_NIZUMI_GYOUSHA_CD:
                    row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value = string.Empty;
                    row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = string.Empty;
                    break;
                case ConstCls.COLUMN_NIZUMI_GENBA_CD:
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value))
                        && string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value)))
                    {
                        string gyoushaCd = Convert.ToString(row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value);
                        M_GYOUSHA nizumiGyousha = this.logic.accessor.GetGyoushaValidDataByCode(gyoushaCd, denpyouDate);
                        if (nizumiGyousha != null)
                        {
                            row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value = nizumiGyousha.GYOUSHA_NAME_RYAKU;
                        }
                    }
                    break;
                case ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD:
                    row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = string.Empty;
                    row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = string.Empty;
                    break;
                case ConstCls.COLUMN_NIOROSHI_GENBA_CD:
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value))
                        && string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value)))
                    {
                        string gyoushaCd = Convert.ToString(row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value);
                        M_GYOUSHA nioroshiGyousha = this.logic.accessor.GetGyoushaValidDataByCode(gyoushaCd, denpyouDate);
                        if (nioroshiGyousha != null)
                        {
                            row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = nioroshiGyousha.GYOUSHA_NAME_RYAKU;
                        }
                    }
                    break;
                case ConstCls.COLUMN_SHARYOU_CD:
                    break;
                case ConstCls.COLUMN_SHASHU_CD:
                    break;
                case ConstCls.COLUMN_UNPAN_GYOUSHA_CD:
                    M_SHARYOU upnSharyou = this.logic.accessor.GetSharyouDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value), value);
                    if (upnSharyou == null)
                    {
                        row.Cells[ConstCls.COLUMN_SHARYOU_CD].Value = string.Empty;
                        row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = string.Empty;
                    }
                    else
                    {
                        row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = upnSharyou.SHARYOU_NAME_RYAKU;
                        row.Cells[ConstCls.COLUMN_SHASHU_CD].Value = upnSharyou.SHASYU_CD;
                        row.Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = upnSharyou.SHAIN_CD;
                        M_SHASHU sharyouShu = this.logic.accessor.GetShashuDataByCode(upnSharyou.SHASYU_CD);
                        if (sharyouShu == null)
                        {
                            row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = string.Empty;
                        }
                        else
                        {
                            row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = sharyouShu.SHASHU_NAME_RYAKU;
                        }
                        M_SHAIN sharyouUnten = this.logic.accessor.GetShainDataByCode(upnSharyou.SHAIN_CD);
                        if (sharyouUnten == null || sharyouUnten.DELETE_FLG.IsTrue)
                        {
                            row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = string.Empty;
                        }
                        else
                        {
                            row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = sharyouUnten.SHAIN_NAME_RYAKU;
                        }
                    }
                    break;
                case ConstCls.COLUMN_UNTENSHA_CD:
                    break;
                case ConstCls.COLUMN_KEITAI_KBN_CD:
                    break;
                case ConstCls.COLUMN_DAIKAN_KBN_CD:
                    break;
                case ConstCls.COLUMN_MANIFEST_SHURUI_CD:
                    break;
                case ConstCls.COLUMN_MANIFEST_TEHAI_CD:
                    break;
                default:
                    break;
            }
            this.txtMr_Enter(this.mrDetail.CurrentCell);
        }
        #endregion

        #region 各CELLの更新処理

        /// <summary>
        /// 各CELLの更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrDetail_CellValidating(object sender, CellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool changed = !this.beforeValuesForDetail.ContainsKey(this.mrDetail.CurrentCell.Name) || this.beforeValuesForDetail[this.mrDetail.CurrentCell.Name] != Convert.ToString(this.mrDetail.CurrentCell.Value);
            try
            {
                string cellName = this.mrDetail.CurrentCell.Name;
                string value = Convert.ToString(this.mrDetail.CurrentCell.Value);
                Row row = this.mrDetail.CurrentRow;
                if (this.beforeValuesForDetail.ContainsKey(cellName) && this.beforeValuesForDetail[cellName] == value)
                {
                    return;
                }
                string denpyouDate = Convert.ToString(row.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value);
                bool catchErr = true;
                bool retCheck = true;
                #region 受入
                if (this.logic.dto.denshuKbnCd == "1")
                {
                    switch (e.CellName)
                    {
                        case ConstCls.COLUMN_TORIHIKISAKI_CD:
                            row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_TORIHIKISAKI tori = this.logic.accessor.GetTorihikisakiValidDataByCode(value, denpyouDate);
                            if (tori == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "取引先");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else if (!CheckTorihikisakiKyoten(Convert.ToString(row.Cells[ConstCls.COLUMN_KYOTEN_CD].Value), tori))
                            {
                                this.logic.errmessage.MessageBoxShow("E146");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = tori.TORIHIKISAKI_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_GENBA_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA gyou = this.logic.accessor.GetGyoushaValidDataByCode(value, denpyouDate);
                            if (gyou == null || !gyou.GYOUSHAKBN_UKEIRE.IsTrue)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = gyou.GYOUSHA_NAME_RYAKU;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = gyou.TORIHIKISAKI_CD;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = string.Empty;
                                M_TORIHIKISAKI gyouTori = this.logic.accessor.GetTorihikisakiValidDataByCode(gyou.TORIHIKISAKI_CD, denpyouDate);
                                if (gyouTori == null)
                                {
                                    return;
                                }
                                else if (!CheckTorihikisakiKyoten(Convert.ToString(row.Cells[ConstCls.COLUMN_KYOTEN_CD].Value), gyouTori))
                                {
                                    this.logic.errmessage.MessageBoxShow("E146");
                                    return;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = gyouTori.TORIHIKISAKI_NAME_RYAKU;
                                }
                            }
                            break;
                        case ConstCls.COLUMN_GENBA_CD:
                            row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_GYOUSHA_CD].Value)))
                            {
                                this.logic.errmessage.MessageBoxShow("E051", "業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_GENBA gen = this.logic.accessor.GetGenbaValidDataByCode(Convert.ToString(row[ConstCls.COLUMN_GYOUSHA_CD].Value), value, denpyouDate);
                            if (gen == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "現場");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = gen.GENBA_NAME_RYAKU;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = gen.TORIHIKISAKI_CD;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = string.Empty;
                                M_TORIHIKISAKI genTori = this.logic.accessor.GetTorihikisakiValidDataByCode(gen.TORIHIKISAKI_CD, denpyouDate);
                                if (genTori == null)
                                {
                                    return;
                                }
                                else if (!CheckTorihikisakiKyoten(Convert.ToString(row.Cells[ConstCls.COLUMN_KYOTEN_CD].Value), genTori))
                                {
                                    this.logic.errmessage.MessageBoxShow("E146");
                                    return;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = genTori.TORIHIKISAKI_NAME_RYAKU;
                                }
                            }
                            break;
                        case ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA nioGyou = this.logic.accessor.GetGyoushaValidDataByCode(value, denpyouDate);
                            if (nioGyou == null || !nioGyou.GYOUSHAKBN_UKEIRE.IsTrue || (!nioGyou.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue && !nioGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "荷降業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = nioGyou.GYOUSHA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_NIOROSHI_GENBA_CD:
                            row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value)))
                            {
                                this.logic.errmessage.MessageBoxShow("E051", "荷降業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }

                            M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = this.logic.accessor.GetAllValidHannyuuClosedData(Convert.ToString(row[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value), value, denpyouDate);

                            //取得テータ
                            if (workclosedhannyuusakiList.Count() >= 1)
                            {
                               row[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Style.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                               row[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Style.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                               this.logic.errmessage.MessageBoxShow("E206", "荷降現場", "伝票日付：" + Convert.ToDateTime(denpyouDate).ToString("yyyy/MM/dd"));
                               e.Cancel = true;
                               this.errorFlag = true;
                               return;
                            }
                            M_GENBA nioGen = this.logic.accessor.GetGenbaValidDataByCode(Convert.ToString(row[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value), value, denpyouDate);
                            if (nioGen == null || (!nioGen.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue && !nioGen.SAISHUU_SHOBUNJOU_KBN.IsTrue && !nioGen.TSUMIKAEHOKAN_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "荷降現場");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = nioGen.GENBA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_SHAIN eigyou = this.logic.accessor.GetShainDataByCode(value);
                            if (eigyou == null || eigyou.DELETE_FLG.IsTrue || !eigyou.EIGYOU_TANTOU_KBN.IsTrue)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "営業担当者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = eigyou.SHAIN_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_SHARYOU_CD:
                            row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            retCheck = this.logic.SharyouDateCheck(Convert.ToString(row[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value), value, denpyouDate, out catchErr);
                            if (!catchErr || !retCheck)
                            {
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_SHARYOU[] sharyou = this.logic.accessor.GetAllValidSharyouData(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value), value);
                            if (sharyou == null || sharyou.Length == 0)
                            {
                                row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = ZeroSuppress(row.Cells[ConstCls.COLUMN_SHARYOU_CD]);
                                return;
                            }
                            else if (sharyou.Length == 1)
                            {
                                row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = sharyou[0].SHARYOU_NAME_RYAKU;
                                row.Cells[ConstCls.COLUMN_SHASHU_CD].Value = sharyou[0].SHASYU_CD;
                                row.Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = sharyou[0].SHAIN_CD;
                                M_SHASHU sharyouShu = this.logic.accessor.GetShashuDataByCode(sharyou[0].SHASYU_CD);
                                if (sharyouShu == null)
                                {
                                    row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = string.Empty;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = sharyouShu.SHASHU_NAME_RYAKU;
                                }
                                M_SHAIN sharyouUnten = this.logic.accessor.GetShainDataByCode(sharyou[0].SHAIN_CD);
                                if (sharyouUnten == null || sharyouUnten.DELETE_FLG.IsTrue)
                                {
                                    row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = string.Empty;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = sharyouUnten.SHAIN_NAME_RYAKU;
                                }
                                if (sharyou[0].GYOUSHA_CD != Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value))
                                {
                                    row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = sharyou[0].GYOUSHA_CD;
                                    M_GYOUSHA upnGyou = this.logic.accessor.GetGyoushaDataByCode(sharyou[0].GYOUSHA_CD);
                                    if (upnGyou == null || upnGyou.DELETE_FLG.IsTrue || (!upnGyou.GYOUSHAKBN_UKEIRE.IsTrue || !upnGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.logic.errmessage.MessageBoxShow("E020", "運搬業者");
                                        row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = string.Empty;
                                        this.errorFlag = true;
                                        this.sharyouCheck = e.RowIndex;
                                        return;
                                    }
                                    else
                                    {
                                        row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = upnGyou.GYOUSHA_NAME_RYAKU;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value)))
                                {
                                    M_GYOUSHA upnGyou = this.logic.accessor.GetGyoushaDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value));
                                    if (upnGyou == null || upnGyou.DELETE_FLG.IsTrue || (!upnGyou.GYOUSHAKBN_UKEIRE.IsTrue || !upnGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.logic.errmessage.MessageBoxShow("E020", "運搬業者");
                                        row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = string.Empty;
                                        this.errorFlag = true;
                                        this.sharyouCheck = e.RowIndex;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                // この時は車輌CDを検索条件に含める
                                this.PopUpConditionsSharyouSwitch((row.Cells[ConstCls.COLUMN_SHARYOU_CD]) as GcCustomAlphaNumTextBoxCell, true);

                                // 検索ポップアップ起動
                                CustomControlExtLogic.PopUp((row.Cells[ConstCls.COLUMN_SHARYOU_CD]) as GcCustomAlphaNumTextBoxCell);

                                this.PopUpConditionsSharyouSwitch((row.Cells[ConstCls.COLUMN_SHARYOU_CD]) as GcCustomAlphaNumTextBoxCell, false);

                                // PopUpでF12押下された場合
                                //（戻り値でF12が押下されたか判断できない為、運搬業者の有無で判断）
                                if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value)))
                                {
                                    row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = ZeroSuppress(row.Cells[ConstCls.COLUMN_SHARYOU_CD]);
                                }
                            }
                            break;
                        case ConstCls.COLUMN_SHASHU_CD:
                            row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_SHASHU shashu = this.logic.accessor.GetShashuDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_SHASHU_CD].Value));
                            if (shashu == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "車種");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = shashu.SHASHU_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_UNPAN_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_SHARYOU_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA upnGyousha = this.logic.accessor.GetGyoushaDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value));
                            if (upnGyousha == null || upnGyousha.DELETE_FLG.IsTrue || (!upnGyousha.GYOUSHAKBN_UKEIRE.IsTrue || !upnGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "運搬業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = upnGyousha.GYOUSHA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_UNTENSHA_CD:
                            row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            retCheck = this.logic.UntenshaDateCheck(value, denpyouDate, out catchErr);
                            if (!catchErr || !retCheck)
                            {
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_SHAIN untensha = this.logic.accessor.GetShainDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNTENSHA_CD].Value));
                            if (untensha == null || untensha.DELETE_FLG.IsTrue || !untensha.UNTEN_KBN.IsTrue)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "社員");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = untensha.SHAIN_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_KEITAI_KBN_CD:
                            row.Cells[ConstCls.COLUMN_KEITAI_KBN].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_KEITAI_KBN keitai = this.logic.accessor.GetKeitaiDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value));
                            if (keitai == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "形態区分");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_KEITAI_KBN].Value = keitai.KEITAI_KBN_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_DAIKAN_KBN_CD:
                            row.Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            string daikanKbnName = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(value));
                            if (string.IsNullOrEmpty(daikanKbnName))
                            {
                                this.logic.errmessage.MessageBoxShow("E058", "");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = daikanKbnName;
                            }
                            break;
                        case ConstCls.COLUMN_MANIFEST_SHURUI_CD:
                            row.Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_MANIFEST_SHURUI maniShurui = this.logic.accessor.GetManiShuruiDataByCode(value);
                            if (maniShurui == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "マニフェスト種類");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = maniShurui.MANIFEST_SHURUI_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_MANIFEST_TEHAI_CD:
                            row.Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_MANIFEST_TEHAI maniTehai = this.logic.accessor.GetManiTehaiDataByCode(value);
                            if (maniTehai == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "マニフェスト手配");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = maniTehai.MANIFEST_TEHAI_NAME_RYAKU;
                            }
                            break;
                        default:
                            break;
                    }
                }
                #endregion
                #region 出荷
                else if (this.txtDenshuKbn.Text == "2")
                {
                    switch (e.CellName)
                    {
                        case ConstCls.COLUMN_TORIHIKISAKI_CD:
                            row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_TORIHIKISAKI tori = this.logic.accessor.GetTorihikisakiValidDataByCode(value, denpyouDate);
                            if (tori == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "取引先");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else if (!CheckTorihikisakiKyoten(Convert.ToString(row.Cells[ConstCls.COLUMN_KYOTEN_CD].Value), tori))
                            {
                                this.logic.errmessage.MessageBoxShow("E146");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = tori.TORIHIKISAKI_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_GENBA_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA gyou = this.logic.accessor.GetGyoushaValidDataByCode(value, denpyouDate);
                            if (gyou == null || !gyou.GYOUSHAKBN_SHUKKA.IsTrue)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = gyou.GYOUSHA_NAME_RYAKU;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = gyou.TORIHIKISAKI_CD;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = string.Empty;
                                M_TORIHIKISAKI gyouTori = this.logic.accessor.GetTorihikisakiValidDataByCode(gyou.TORIHIKISAKI_CD, denpyouDate);
                                if (gyouTori == null)
                                {
                                    return;
                                }
                                else if (!CheckTorihikisakiKyoten(Convert.ToString(row.Cells[ConstCls.COLUMN_KYOTEN_CD].Value), gyouTori))
                                {
                                    this.logic.errmessage.MessageBoxShow("E146");
                                    return;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = gyouTori.TORIHIKISAKI_NAME_RYAKU;
                                }
                            }
                            break;
                        case ConstCls.COLUMN_GENBA_CD:
                            row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_GYOUSHA_CD].Value)))
                            {
                                this.logic.errmessage.MessageBoxShow("E051", "業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_GENBA gen = this.logic.accessor.GetGenbaValidDataByCode(Convert.ToString(row[ConstCls.COLUMN_GYOUSHA_CD].Value), value, denpyouDate);
                            if (gen == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "現場");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = gen.GENBA_NAME_RYAKU;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = gen.TORIHIKISAKI_CD;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = string.Empty;
                                M_TORIHIKISAKI genTori = this.logic.accessor.GetTorihikisakiValidDataByCode(gen.TORIHIKISAKI_CD, denpyouDate);
                                if (genTori == null)
                                {
                                    return;
                                }
                                else if (!CheckTorihikisakiKyoten(Convert.ToString(row.Cells[ConstCls.COLUMN_KYOTEN_CD].Value), genTori))
                                {
                                    this.logic.errmessage.MessageBoxShow("E146");
                                    return;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = genTori.TORIHIKISAKI_NAME_RYAKU;
                                }
                            }
                            break;
                        case ConstCls.COLUMN_NIZUMI_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA nioGyou = this.logic.accessor.GetGyoushaValidDataByCode(value, denpyouDate);
                            if (nioGyou == null || !nioGyou.GYOUSHAKBN_SHUKKA.IsTrue || (!nioGyou.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue && !nioGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "荷積業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value = nioGyou.GYOUSHA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_NIZUMI_GENBA_CD:
                            row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value)))
                            {
                                this.logic.errmessage.MessageBoxShow("E051", "荷積業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_GENBA nioGen = this.logic.accessor.GetGenbaValidDataByCode(Convert.ToString(row[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value), value, denpyouDate);
                            if (nioGen == null || (!nioGen.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue && !nioGen.SAISHUU_SHOBUNJOU_KBN.IsTrue && !nioGen.TSUMIKAEHOKAN_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "荷積現場");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = nioGen.GENBA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_SHAIN eigyou = this.logic.accessor.GetShainDataByCode(value);
                            if (eigyou == null || eigyou.DELETE_FLG.IsTrue || !eigyou.EIGYOU_TANTOU_KBN.IsTrue)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "営業担当者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = eigyou.SHAIN_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_SHARYOU_CD:
                            row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            retCheck = this.logic.SharyouDateCheck(Convert.ToString(row[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value), value, denpyouDate, out catchErr);
                            if (!catchErr || !retCheck)
                            {
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_SHARYOU[] sharyou = this.logic.accessor.GetAllValidSharyouData(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value), value);
                            if (sharyou == null || sharyou.Length == 0)
                            {
                                row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = ZeroSuppress(row.Cells[ConstCls.COLUMN_SHARYOU_CD]);
                                return;
                            }
                            else if (sharyou.Length == 1)
                            {
                                row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = sharyou[0].SHARYOU_NAME_RYAKU;
                                row.Cells[ConstCls.COLUMN_SHASHU_CD].Value = sharyou[0].SHASYU_CD;
                                row.Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = sharyou[0].SHAIN_CD;
                                M_SHASHU sharyouShu = this.logic.accessor.GetShashuDataByCode(sharyou[0].SHASYU_CD);
                                if (sharyouShu == null)
                                {
                                    row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = string.Empty;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = sharyouShu.SHASHU_NAME_RYAKU;
                                }
                                M_SHAIN sharyouUnten = this.logic.accessor.GetShainDataByCode(sharyou[0].SHAIN_CD);
                                if (sharyouUnten == null || sharyouUnten.DELETE_FLG.IsTrue)
                                {
                                    row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = string.Empty;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = sharyouUnten.SHAIN_NAME_RYAKU;
                                }
                                if (sharyou[0].GYOUSHA_CD != Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value))
                                {
                                    row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = sharyou[0].GYOUSHA_CD;
                                    M_GYOUSHA upnGyou = this.logic.accessor.GetGyoushaDataByCode(sharyou[0].GYOUSHA_CD);
                                    if (upnGyou == null || upnGyou.DELETE_FLG.IsTrue || (!upnGyou.GYOUSHAKBN_UKEIRE.IsTrue || !upnGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.logic.errmessage.MessageBoxShow("E020", "運搬業者");
                                        row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = string.Empty;
                                        this.errorFlag = true;
                                        this.sharyouCheck = e.RowIndex;
                                        return;
                                    }
                                    else
                                    {
                                        row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = upnGyou.GYOUSHA_NAME_RYAKU;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value)))
                                {
                                    M_GYOUSHA upnGyou = this.logic.accessor.GetGyoushaDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value));
                                    if (upnGyou == null || upnGyou.DELETE_FLG.IsTrue || (!upnGyou.GYOUSHAKBN_UKEIRE.IsTrue || !upnGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.logic.errmessage.MessageBoxShow("E020", "運搬業者");
                                        row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = string.Empty;
                                        this.errorFlag = true;
                                        this.sharyouCheck = e.RowIndex;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                // この時は車輌CDを検索条件に含める
                                this.PopUpConditionsSharyouSwitch((row.Cells[ConstCls.COLUMN_SHARYOU_CD]) as GcCustomAlphaNumTextBoxCell, true);

                                // 検索ポップアップ起動
                                CustomControlExtLogic.PopUp((row.Cells[ConstCls.COLUMN_SHARYOU_CD]) as GcCustomAlphaNumTextBoxCell);

                                this.PopUpConditionsSharyouSwitch((row.Cells[ConstCls.COLUMN_SHARYOU_CD]) as GcCustomAlphaNumTextBoxCell, false);

                                // PopUpでF12押下された場合
                                //（戻り値でF12が押下されたか判断できない為、運搬業者の有無で判断）
                                if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value)))
                                {
                                    row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = ZeroSuppress(row.Cells[ConstCls.COLUMN_SHARYOU_CD]);
                                }
                            }
                            break;
                        case ConstCls.COLUMN_SHASHU_CD:
                            row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_SHASHU shashu = this.logic.accessor.GetShashuDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_SHASHU_CD].Value));
                            if (shashu == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "車種");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = shashu.SHASHU_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_UNPAN_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_SHARYOU_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA upnGyousha = this.logic.accessor.GetGyoushaDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value));
                            if (upnGyousha == null || upnGyousha.DELETE_FLG.IsTrue || (!upnGyousha.GYOUSHAKBN_SHUKKA.IsTrue || !upnGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "運搬業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = upnGyousha.GYOUSHA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_UNTENSHA_CD:
                            row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            retCheck = this.logic.UntenshaDateCheck(value, denpyouDate, out catchErr);
                            if (!catchErr || !retCheck)
                            {
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_SHAIN untensha = this.logic.accessor.GetShainDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNTENSHA_CD].Value));
                            if (untensha == null || untensha.DELETE_FLG.IsTrue || !untensha.UNTEN_KBN.IsTrue)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "社員");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = untensha.SHAIN_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_KEITAI_KBN_CD:
                            row.Cells[ConstCls.COLUMN_KEITAI_KBN].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_KEITAI_KBN keitai = this.logic.accessor.GetKeitaiDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value));
                            if (keitai == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "形態区分");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_KEITAI_KBN].Value = keitai.KEITAI_KBN_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_DAIKAN_KBN_CD:
                            row.Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            string daikanKbnName = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(value));
                            if (string.IsNullOrEmpty(daikanKbnName))
                            {
                                this.logic.errmessage.MessageBoxShow("E058", "");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = daikanKbnName;
                            }
                            break;
                        case ConstCls.COLUMN_MANIFEST_SHURUI_CD:
                            row.Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_MANIFEST_SHURUI maniShurui = this.logic.accessor.GetManiShuruiDataByCode(value);
                            if (maniShurui == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "マニフェスト種類");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = maniShurui.MANIFEST_SHURUI_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_MANIFEST_TEHAI_CD:
                            row.Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_MANIFEST_TEHAI maniTehai = this.logic.accessor.GetManiTehaiDataByCode(value);
                            if (maniTehai == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "マニフェスト手配");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = maniTehai.MANIFEST_TEHAI_NAME_RYAKU;
                            }
                            break;
                        default:
                            break;
                    }
                }
                #endregion
                #region 売上支払
                else
                {
                    switch (e.CellName)
                    {
                        case ConstCls.COLUMN_TORIHIKISAKI_CD:
                            row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_TORIHIKISAKI tori = this.logic.accessor.GetTorihikisakiValidDataByCode(value, denpyouDate);
                            if (tori == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "取引先");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else if (!CheckTorihikisakiKyoten(Convert.ToString(row.Cells[ConstCls.COLUMN_KYOTEN_CD].Value), tori))
                            {
                                this.logic.errmessage.MessageBoxShow("E146");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = tori.TORIHIKISAKI_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_GENBA_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA gyou = this.logic.accessor.GetGyoushaValidDataByCode(value, denpyouDate);
                            if (gyou == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = gyou.GYOUSHA_NAME_RYAKU;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = gyou.TORIHIKISAKI_CD;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = string.Empty;
                                M_TORIHIKISAKI gyouTori = this.logic.accessor.GetTorihikisakiValidDataByCode(gyou.TORIHIKISAKI_CD, denpyouDate);
                                if (gyouTori == null)
                                {
                                    return;
                                }
                                else if (!CheckTorihikisakiKyoten(Convert.ToString(row.Cells[ConstCls.COLUMN_KYOTEN_CD].Value), gyouTori))
                                {
                                    this.logic.errmessage.MessageBoxShow("E146");
                                    return;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = gyouTori.TORIHIKISAKI_NAME_RYAKU;
                                }
                            }
                            break;
                        case ConstCls.COLUMN_GENBA_CD:
                            row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_GYOUSHA_CD].Value)))
                            {
                                this.logic.errmessage.MessageBoxShow("E051", "業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_GENBA gen = this.logic.accessor.GetGenbaValidDataByCode(Convert.ToString(row[ConstCls.COLUMN_GYOUSHA_CD].Value), value, denpyouDate);
                            if (gen == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "現場");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_GENBA_NAME].Value = gen.GENBA_NAME_RYAKU;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = gen.TORIHIKISAKI_CD;
                                row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = string.Empty;
                                M_TORIHIKISAKI genTori = this.logic.accessor.GetTorihikisakiValidDataByCode(gen.TORIHIKISAKI_CD, denpyouDate);
                                if (genTori == null)
                                {
                                    return;
                                }
                                else if (!CheckTorihikisakiKyoten(Convert.ToString(row.Cells[ConstCls.COLUMN_KYOTEN_CD].Value), genTori))
                                {
                                    this.logic.errmessage.MessageBoxShow("E146");
                                    return;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = genTori.TORIHIKISAKI_NAME_RYAKU;
                                }
                            }
                            break;
                        case ConstCls.COLUMN_NIZUMI_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA nizuGyou = this.logic.accessor.GetGyoushaValidDataByCode(value, denpyouDate);
                            if (nizuGyou == null || (!nizuGyou.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue && !nizuGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "荷積業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value = nizuGyou.GYOUSHA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_NIZUMI_GENBA_CD:
                            row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value)))
                            {
                                this.logic.errmessage.MessageBoxShow("E051", "荷積業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            if (!catchErr || !retCheck)
                            {
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_GENBA nizuGen = this.logic.accessor.GetGenbaValidDataByCode(Convert.ToString(row[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value), value, denpyouDate);
                            if (nizuGen == null || (!nizuGen.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue && !nizuGen.SAISHUU_SHOBUNJOU_KBN.IsTrue && !nizuGen.TSUMIKAEHOKAN_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "荷積現場");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = nizuGen.GENBA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA nioGyou = this.logic.accessor.GetGyoushaValidDataByCode(value, denpyouDate);
                            if (nioGyou == null || !nioGyou.GYOUSHAKBN_UKEIRE.IsTrue || (!nioGyou.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue && !nioGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "荷降業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = nioGyou.GYOUSHA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_NIOROSHI_GENBA_CD:
                            row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value)))
                            {
                                this.logic.errmessage.MessageBoxShow("E051", "荷降業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }

                            M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = this.logic.accessor.GetAllValidHannyuuClosedData(Convert.ToString(row[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value), value, denpyouDate);

                            //取得テータ
                            if (workclosedhannyuusakiList.Count() >= 1)
                            {
                               row[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Style.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                               row[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Style.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                               this.logic.errmessage.MessageBoxShow("E206", "荷降現場", "伝票日付：" + Convert.ToDateTime(denpyouDate).ToString("yyyy/MM/dd"));
                               e.Cancel = true;
                               this.errorFlag = true;
                               return;
                            }
                            M_GENBA nioGen = this.logic.accessor.GetGenbaValidDataByCode(Convert.ToString(row[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value), value, denpyouDate);
                            if (nioGen == null || (!nioGen.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue && !nioGen.SAISHUU_SHOBUNJOU_KBN.IsTrue && !nioGen.TSUMIKAEHOKAN_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "荷降現場");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = nioGen.GENBA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_SHAIN eigyou = this.logic.accessor.GetShainDataByCode(value);
                            if (eigyou == null || eigyou.DELETE_FLG.IsTrue || !eigyou.EIGYOU_TANTOU_KBN.IsTrue)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "営業担当者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = eigyou.SHAIN_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_SHARYOU_CD:
                            row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            retCheck = this.logic.SharyouDateCheck(Convert.ToString(row[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value), value, denpyouDate, out catchErr);
                            if (!catchErr || !retCheck)
                            {
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_SHARYOU[] sharyou = this.logic.accessor.GetAllValidSharyouData(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value), value);
                            if (sharyou == null || sharyou.Length == 0)
                            {
                                row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = ZeroSuppress(row.Cells[ConstCls.COLUMN_SHARYOU_CD]);
                                return;
                            }
                            else if (sharyou.Length == 1)
                            {
                                row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = sharyou[0].SHARYOU_NAME_RYAKU;
                                row.Cells[ConstCls.COLUMN_SHASHU_CD].Value = sharyou[0].SHASYU_CD;
                                row.Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = sharyou[0].SHAIN_CD;
                                M_SHASHU sharyouShu = this.logic.accessor.GetShashuDataByCode(sharyou[0].SHASYU_CD);
                                if (sharyouShu == null)
                                {
                                    row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = string.Empty;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = sharyouShu.SHASHU_NAME_RYAKU;
                                }
                                M_SHAIN sharyouUnten = this.logic.accessor.GetShainDataByCode(sharyou[0].SHAIN_CD);
                                if (sharyouUnten == null || sharyouUnten.DELETE_FLG.IsTrue)
                                {
                                    row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = string.Empty;
                                }
                                else
                                {
                                    row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = sharyouUnten.SHAIN_NAME_RYAKU;
                                }
                                if (sharyou[0].GYOUSHA_CD != Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value))
                                {
                                    row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = sharyou[0].GYOUSHA_CD;
                                    M_GYOUSHA upnGyou = this.logic.accessor.GetGyoushaDataByCode(sharyou[0].GYOUSHA_CD);
                                    if (upnGyou == null || upnGyou.DELETE_FLG.IsTrue || (!upnGyou.GYOUSHAKBN_UKEIRE.IsTrue || !upnGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.logic.errmessage.MessageBoxShow("E020", "運搬業者");
                                        row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = string.Empty;
                                        this.errorFlag = true;
                                        this.sharyouCheck = e.RowIndex;
                                        return;
                                    }
                                    else
                                    {
                                        row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = upnGyou.GYOUSHA_NAME_RYAKU;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value)))
                                {
                                    M_GYOUSHA upnGyou = this.logic.accessor.GetGyoushaDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value));
                                    if (upnGyou == null || upnGyou.DELETE_FLG.IsTrue || (!upnGyou.GYOUSHAKBN_UKEIRE.IsTrue || !upnGyou.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.logic.errmessage.MessageBoxShow("E020", "運搬業者");
                                        row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = string.Empty;
                                        this.errorFlag = true;
                                        this.sharyouCheck = e.RowIndex;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                // この時は車輌CDを検索条件に含める
                                this.PopUpConditionsSharyouSwitch((row.Cells[ConstCls.COLUMN_SHARYOU_CD]) as GcCustomAlphaNumTextBoxCell, true);

                                // 検索ポップアップ起動
                                CustomControlExtLogic.PopUp((row.Cells[ConstCls.COLUMN_SHARYOU_CD]) as GcCustomAlphaNumTextBoxCell);

                                this.PopUpConditionsSharyouSwitch((row.Cells[ConstCls.COLUMN_SHARYOU_CD]) as GcCustomAlphaNumTextBoxCell, false);

                                // PopUpでF12押下された場合
                                //（戻り値でF12が押下されたか判断できない為、運搬業者の有無で判断）
                                if (string.IsNullOrEmpty(Convert.ToString(row[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value)))
                                {
                                    row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = ZeroSuppress(row.Cells[ConstCls.COLUMN_SHARYOU_CD]);
                                }
                            }
                            break;
                        case ConstCls.COLUMN_SHASHU_CD:
                            row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_SHASHU shashu = this.logic.accessor.GetShashuDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_SHASHU_CD].Value));
                            if (shashu == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "車種");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_SHASHU_NAME].Value = shashu.SHASHU_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_UNPAN_GYOUSHA_CD:
                            row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_SHARYOU_CD].Value = string.Empty;
                            row.Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_GYOUSHA upnGyousha = this.logic.accessor.GetGyoushaDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value));
                            if (upnGyousha == null || upnGyousha.DELETE_FLG.IsTrue || (!upnGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "運搬業者");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = upnGyousha.GYOUSHA_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_UNTENSHA_CD:
                            row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            retCheck = this.logic.UntenshaDateCheck(value, denpyouDate, out catchErr);
                            if (!catchErr || !retCheck)
                            {
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            M_SHAIN untensha = this.logic.accessor.GetShainDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_UNTENSHA_CD].Value));
                            if (untensha == null || untensha.DELETE_FLG.IsTrue || !untensha.UNTEN_KBN.IsTrue)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "社員");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = untensha.SHAIN_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_KEITAI_KBN_CD:
                            row.Cells[ConstCls.COLUMN_KEITAI_KBN].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_KEITAI_KBN keitai = this.logic.accessor.GetKeitaiDataByCode(Convert.ToString(row.Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value));
                            if (keitai == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "形態区分");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_KEITAI_KBN].Value = keitai.KEITAI_KBN_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_DAIKAN_KBN_CD:
                            row.Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            string daikanKbnName = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(value));
                            if (string.IsNullOrEmpty(daikanKbnName))
                            {
                                this.logic.errmessage.MessageBoxShow("E058", "");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = daikanKbnName;
                            }
                            break;
                        case ConstCls.COLUMN_MANIFEST_SHURUI_CD:
                            row.Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_MANIFEST_SHURUI maniShurui = this.logic.accessor.GetManiShuruiDataByCode(value);
                            if (maniShurui == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "マニフェスト種類");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = maniShurui.MANIFEST_SHURUI_NAME_RYAKU;
                            }
                            break;
                        case ConstCls.COLUMN_MANIFEST_TEHAI_CD:
                            row.Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = string.Empty;
                            if (string.IsNullOrEmpty(value))
                            {
                                return;
                            }
                            M_MANIFEST_TEHAI maniTehai = this.logic.accessor.GetManiTehaiDataByCode(value);
                            if (maniTehai == null)
                            {
                                this.logic.errmessage.MessageBoxShow("E020", "マニフェスト手配");
                                e.Cancel = true;
                                this.errorFlag = true;
                                return;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = maniTehai.MANIFEST_TEHAI_NAME_RYAKU;
                            }
                            break;
                        default:
                            break;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                if (changed)
                {
                    if (e.CellName != ConstCls.COLUMN_CHECK && !e.Cancel)
                    {
                        this.mrDetail.CurrentRow.Cells[ConstCls.COLUMN_CHECK].Value = true;
                    }
                }

                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// 各CELLの更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrDetail_CellValidated(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                string value = Convert.ToString(this.mrDetail.CurrentCell.Value);
                Row row = this.mrDetail.CurrentRow;
                switch (e.CellName)
                {
                    case ConstCls.COLUMN_TORIHIKISAKI_CD:
                        bool shokuchi = this.logic.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = value });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD], row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME], shokuchi, row.Index);
                        break;
                    case ConstCls.COLUMN_GYOUSHA_CD:
                        shokuchi = this.logic.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = value });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_GYOUSHA_CD], row.Cells[ConstCls.COLUMN_GYOUSHA_NAME], shokuchi, row.Index);
                        shokuchi = this.logic.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = value, GENBA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_GENBA_CD].Value) });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_GENBA_CD], row.Cells[ConstCls.COLUMN_GENBA_NAME], shokuchi, row.Index);
                        break;
                    case ConstCls.COLUMN_GENBA_CD:
                        shokuchi = this.logic.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value) });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD], row.Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME], shokuchi, row.Index);
                        shokuchi = this.logic.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value) });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_GYOUSHA_CD], row.Cells[ConstCls.COLUMN_GYOUSHA_NAME], shokuchi, row.Index);
                        shokuchi = this.logic.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value), GENBA_CD = value });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_GENBA_CD], row.Cells[ConstCls.COLUMN_GENBA_NAME], shokuchi, row.Index);
                        break;
                    case ConstCls.COLUMN_NIZUMI_GYOUSHA_CD:
                    case ConstCls.COLUMN_NIZUMI_GENBA_CD:
                        shokuchi = this.logic.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value) });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD], row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME], shokuchi, row.Index);
                        shokuchi = this.logic.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value), GENBA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value) });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD], row.Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME], shokuchi, row.Index);
                        break;
                    case ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD:
                    case ConstCls.COLUMN_NIOROSHI_GENBA_CD:
                        shokuchi = this.logic.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value) });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD], row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME], shokuchi, row.Index);
                        shokuchi = this.logic.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value), GENBA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value) });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD], row.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME], shokuchi, row.Index);
                        break;
                    case ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD:
                        break;
                    case ConstCls.COLUMN_UNPAN_GYOUSHA_CD:
                    case ConstCls.COLUMN_SHARYOU_CD:
                        shokuchi = this.logic.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value) });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD], row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME], shokuchi, row.Index);
                        shokuchi = this.logic.accessor.IsShokuchi(new M_SHARYOU() { GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value), SHARYOU_CD = Convert.ToString(row.Cells[ConstCls.COLUMN_SHARYOU_CD].Value) });
                        this.logic.SetShokuchi(row.Cells[ConstCls.COLUMN_SHARYOU_CD], row.Cells[ConstCls.COLUMN_SHARYOU_NAME], shokuchi, row.Index);
                        break;
                    case ConstCls.COLUMN_SHASHU_CD:
                        break;
                    case ConstCls.COLUMN_UNTENSHA_CD:
                        break;
                    case ConstCls.COLUMN_KEITAI_KBN_CD:
                        break;
                    case ConstCls.COLUMN_DAIKAN_KBN_CD:
                        break;
                    case ConstCls.COLUMN_MANIFEST_SHURUI_CD:
                        break;
                    case ConstCls.COLUMN_MANIFEST_TEHAI_CD:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region へーダーチェックをクリックイベント
        public void mrDetail_CellClick(object sender, CellEventArgs e)
        {
            if (this.clickLock)
            {
                return;
            }
            try
            {
                this.clickLock = true;
                if (this.mrDetail.Rows.Count > 0)
                {
                    if (e.RowIndex == -1)
                    {
                        int currentRowIndex = this.mrDetail.CurrentCell.RowIndex;
                        int currentCellIndex = this.mrDetail.CurrentCell.CellIndex;
                        this.mrDetail.CurrentCell = null;
                        switch (e.CellIndex)
                        {
                            case 0:
                                CheckBoxCell delete = this.mrDetail.ColumnHeaders[0].Cells["lbl_CHECK"] as CheckBoxCell;
                                if (this.logic.ToNBoolean(delete.Value) == true)
                                {
                                    delete.Value = false;
                                }
                                else
                                {
                                    delete.Value = true;
                                }
                                foreach (Row row in this.mrDetail.Rows)
                                {
                                    GcCustomCheckBoxCell cell = (row.Cells[ConstCls.COLUMN_CHECK] as GcCustomCheckBoxCell);
                                    cell.Value = delete.Value;
                                }
                                break;
                            default:
                                break;
                        }
                        this.mrDetail.CurrentCell = this.mrDetail.Rows[currentRowIndex].Cells[currentCellIndex];
                    }
                }
            }
            finally
            {
                this.clickLock = false;
            }
        }
        #endregion

        #endregion

        #region フォーカス取得イベント

        /// <summary>
        /// フォーカス取得イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtControl_Enter(object sender, EventArgs e)
        {
            // 前回内容保存
            var txtCtrl = sender as CustomTextBox;
            if (txtCtrl != null)
                txtCtrl.PrevText = txtCtrl.Text;
        }

        /// <summary>
        /// フォーカス取得イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtMr_Enter(object sender)
        {
            // 前回内容保存
            var txtCtrl = sender as GcCustomTextBoxCell;
            if (txtCtrl != null)
            {
                string cellName = txtCtrl.Name;
                if (this.errorFlag)
                {
                    if (this.beforeValuesForDetail.ContainsKey(cellName))
                    {
                        this.beforeValuesForDetail[cellName] = "";
                    }
                    this.errorFlag = false;
                }
                else
                {
                    if (this.beforeValuesForDetail.ContainsKey(cellName))
                    {
                        this.beforeValuesForDetail[cellName] = Convert.ToString(txtCtrl.Value);
                    }
                    else
                    {
                        this.beforeValuesForDetail.Add(cellName, Convert.ToString(txtCtrl.Value));
                    }
                }
            }
        }

        #endregion

        #region 日付イベント
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }

        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.HIDUKE_FROM;
            var ToTextBox = this.HIDUKE_TO;

            ToTextBox.Text = FromTextBox.Text;
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #endregion

        #region メソッド

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal bool CheckAuth(string kbn = "")
        {
            return this.logic.CheckAuth(kbn);
        }

        internal bool CheckTorihikisakiKyoten(string kyoten, M_TORIHIKISAKI tori)
        {
            if (!string.IsNullOrEmpty(kyoten))
            {
                if (Int16.Parse(kyoten) != tori.TORIHIKISAKI_KYOTEN_CD && !tori.TORIHIKISAKI_KYOTEN_CD.ToString().Equals(SalesPaymentConstans.KYOTEN_ZENSHA))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 車輌PopUpの検索条件に車輌CDを含めるかを引数によって設定します
        /// </summary>
        /// <param name="isPopupConditionsSharyouCD"></param>
        internal void PopUpConditionsSharyouSwitch(GcCustomAlphaNumTextBoxCell cell, bool isPopupConditionsSharyouCD)
        {
            PopupSearchSendParamDto sharyouParam = new PopupSearchSendParamDto();
            sharyouParam.And_Or = CONDITION_OPERATOR.AND;
            sharyouParam.Control = "mr_SHARYOU_CD";
            sharyouParam.KeyName = "key002";

            if (isPopupConditionsSharyouCD)
            {
                if (!cell.PopupSearchSendParams.Contains(sharyouParam))
                {
                    cell.PopupSearchSendParams.Add(sharyouParam);
                }
            }
            else
            {
                var paramsCount = cell.PopupSearchSendParams.Count;
                for (int i = 0; i < paramsCount; i++)
                {
                    if (cell.PopupSearchSendParams[i].Control == "SHARYOU_CD" &&
                        cell.PopupSearchSendParams[i].KeyName == "key002")
                    {
                        cell.PopupSearchSendParams.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// ゼロサプレス処理
        /// </summary>
        /// <param name="source">入力コントロール</param>
        /// <returns>ゼロサプレス後の文字列</returns>
        private string ZeroSuppress(object source)
        {
            string result = string.Empty;

            // 該当コントロールの最大桁数を取得
            object obj;
            decimal charactersNumber;
            string text = PropertyUtility.GetTextOrValue(source);
            if (!PropertyUtility.GetValue(source, Constans.CHARACTERS_NUMBER, out obj))
                // 最大桁数が取得できない場合はそのまま
                return text;

            charactersNumber = (decimal)obj;
            if (charactersNumber == 0 || source == null || string.IsNullOrEmpty(text))
                // 最大桁数が0または入力値が空の場合はそのまま
                return text;

            var strCharactersUmber = text;
            if (strCharactersUmber.Contains("."))
                // 小数点を含む場合はそのまま
                return text;

            // ゼロサプレスした値を返す
            StringBuilder sb = new StringBuilder((int)charactersNumber);
            string format = sb.Append('#', (int)charactersNumber).ToString();
            long val = 0;
            if (long.TryParse(text, out val))
                result = val == 0 ? "0" : val.ToString(format);
            else
                // 入力値が数値ではない場合はそのまま
                result = text;

            return result;
        }
        #endregion

        #region 伝種イベント
        /// <summary>
        /// 伝種TextChange後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrDetail_SizeChanged(object sender, EventArgs e)
        {
            int height = this.mrDetail.Height;
            int width = this.mrDetail.Width;
        }
        #endregion
    }
}