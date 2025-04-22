using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Logic;
using Seasar.Quill;
using r_framework.Utility;
using r_framework.CustomControl;

namespace Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 初期処理フラグ
        /// </summary>
        private bool bInitialize = true;

        /// <summary>
        /// 業者CD
        /// </summary>
        private string preGyoushaCd = string.Empty;

        // thongh 20150803 #11932 start
        /// <summary>
        /// 前回値チェック用変数(拠点CD用)
        /// </summary>
        private Dictionary<string, string> beforeValuesForKyoten = new Dictionary<string, string>();
        // thongh 20150803 #11932 end
        #endregion フィールド

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        public UIForm(UIHeader headerForm)
            : base(WINDOW_ID.T_TSUKIGIME_URIAGE_DENPYOU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this, headerForm);

            chkSakusei.Visible = true;
            chkSakusei.SendToBack();
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

        }
        #endregion コンストラクタ

        #region イベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.logic.WindowInit())
            {
                return;
            }
            txtKyotenCd.Focus();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.mrwTukigimeUriageDenpyo != null)
            {
                this.mrwTukigimeUriageDenpyo.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            bInitialize = false;
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

            base.OnShown(e);
        }

        /// <summary>
        /// 締日変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbShimebi_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.logic.ChangeShimebiProcess();
        }

        /// <summary>
        /// 取引先CDのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txtTorihikisakiCd_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool catchErr = true;
            bool isErr = this.logic.CheckTorihikisakiShimebi(this.txtTorihikisakiCd.Text, this.cmbShimebi.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (!String.IsNullOrEmpty(this.txtTorihikisakiCd.Text) && !isErr)
            {
                MessageBoxShowLogic logic = new MessageBoxShowLogic();
                logic.MessageBoxShow("E058");

                this.txtTorihikisakiName.Text = String.Empty;
                this.txtTorihikisakiCd.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDフォーカスアウト時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGenbaCd_Validated(object sender, EventArgs e)
        {
            this.logic.CheckGenba();
        }

        #endregion イベント

        #region "ボタン押下イベント"
        /// <summary>
        /// 対象期間を１ヶ月前にします
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function3Click(object sender, EventArgs e)
        {
            this.logic.MinusMonth();
            this.mrwTukigimeUriageDenpyo.Rows.Clear();
        }

        /// <summary>
        /// 対象期間を１ヶ月後にします
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function4Click(object sender, EventArgs e)
        {
            this.logic.PlusMonth();
            this.mrwTukigimeUriageDenpyo.Rows.Clear();
        }

        /// <summary>
        /// 一覧画面に遷移する。
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function7Click(object sender, EventArgs e)
        {
            this.logic.StartWindow();
        }

        /// <summary>
        /// 検索を実行する。
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function8Click(object sender, EventArgs e)
        {
            // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
            if (this.logic.CheckDate())
            {
                return;
            }
            // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end
            // thongh 20150803 #11932 拠点必須のアラート表示 start
            if (this.logic.CheckKyotenToSearch())
            {
                return;
            }
            // thongh 20150803 #11932 拠点必須のアラート表示 end
            if (this.logic.CheckSeikyuDateToSearch())
            {
                return;
            }

            this.logic.ExecuteSearch();
        }

        /// <summary>
        /// エラー理由を登録する。
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function9Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var isRegistError = base.RegistErrorFlag;
            if (!isRegistError)
            {
                //チェックボックス未入力のチェック処理
                int checkCount = this.logic.chkboxCheck();
                if (checkCount == -1)
                {
                    return;
                }
                else if (checkCount == 0)
                {
                    //チェック状態の行が１件も存在しない場合、エラーメッセージ
                    var messageBoxShowLogic = new MessageBoxShowLogic();
                    messageBoxShowLogic.MessageBoxShow(ConstInfo.ERR_MSG_CD_E027, ConstInfo.ERR_ARGS_CUREATE_DATA);
                    LogUtility.DebugMethodEnd();
                    return;
                }

                /* 月次処理中チェック */
                DateTime getsujiShoriCheckDate = DateTime.Parse(this.dtpSeikyuDate.Value.ToString());
                GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E224", "実行");
                    LogUtility.DebugMethodEnd();
                    return;
                }

                // 月次処理ロックチェック
                if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E223", "実行");
                    LogUtility.DebugMethodEnd();
                    return;
                }

                this.logic.ProcessStart();
            }
            else
            {
                // 先頭のエラー項目にフォーカスを設定
                this.SetFocusErrorControl();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を閉じる。
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function12Click(object sender, EventArgs e)
        {
            this.logic.EndOfProcess();
        }

        #endregion "ボタン押下イベント"

        #region グリッド発行列のチェックボックス設定

        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrwTukigimeUriageDenpyo_CellPainting(object sender, GrapeCity.Win.MultiRow.CellPaintingEventArgs e)
        {
            this.logic.CellPaintingLogic(e);
        }

        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrwTukigimeUriageDenpyo_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            this.logic.CellClickLogic(e);
        }

        /// <summary>
        /// すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.checkBoxAllCheckedChangedLogic();
        }

        #endregion グリッド発行列のチェックボックス設定

        /// <summary>
        /// 画面に表示されているコントロールのリストを取得します
        /// </summary>
        /// <returns>コントロールのリスト</returns>
        private List<Control> GetAllControlList()
        {
            LogUtility.DebugMethodStart();

            var controlList = new List<Control>();
            controlList.AddRange(controlUtil.GetAllControls(this.logic.GetHeader()));
            controlList.AddRange(controlUtil.GetAllControls(this));

            LogUtility.DebugMethodEnd(controlList);
            return controlList;
        }

        /// <summary>
        /// エラーが発生しているコントロールのうち、TabIndexが先頭のコントロールにフォーカスをセットします
        /// </summary>
        private void SetFocusErrorControl()
        {
            LogUtility.DebugMethodStart();

            var controlList = this.GetAllControlList();
            // エラーが発生しているコントロールで先頭のコントロールを取得
            var errorControl = controlList.Where(c => c is ICustomAutoChangeBackColor && ((ICustomAutoChangeBackColor)c).IsInputErrorOccured == true).OrderBy(c => ((Control)c).TabIndex).FirstOrDefault();
            if (null != errorControl)
            {
                errorControl.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
        private void dtpTaishoKikanFrom_Leave(object sender, EventArgs e)
        {
            this.dtpTaishoKikanTo.IsInputErrorOccured = false;
            this.dtpSeikyuDate.IsInputErrorOccured = false;
            this.dtpTaishoKikanTo.BackColor = Constans.NOMAL_COLOR;
            this.dtpSeikyuDate.BackColor = Constans.NOMAL_COLOR;
        }

        private void dtpTaishoKikanTo_Leave(object sender, EventArgs e)
        {
            this.dtpTaishoKikanFrom.IsInputErrorOccured = false;
            this.dtpSeikyuDate.IsInputErrorOccured = false;
            this.dtpTaishoKikanFrom.BackColor = Constans.NOMAL_COLOR;
            this.dtpSeikyuDate.BackColor = Constans.NOMAL_COLOR;
        }

        private void dtpSeikyuDate_Leave(object sender, EventArgs e)
        {
            this.dtpTaishoKikanFrom.IsInputErrorOccured = false;
            this.dtpTaishoKikanTo.IsInputErrorOccured = false;
            this.dtpTaishoKikanFrom.BackColor = Constans.NOMAL_COLOR;
            this.dtpTaishoKikanTo.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

        //20150703 #11161 hoanghm 業者をクリアしたら現場もクリアすること start
        private void txtGyosyaCd_Validated(object sender, EventArgs e)
        {
            AfterPopupGyousha();
        }

        private void txtGyosyaCd_Enter(object sender, EventArgs e)
        {
            this.preGyoushaCd = this.txtGyosyaCd.Text;
        }

        public void AfterPopupGyousha()
        {
            if (!this.txtGyosyaCd.Text.Equals(this.preGyoushaCd)) //if clear or change gyousha then clear genba
            {
                this.preGyoushaCd = this.txtGyosyaCd.Text;
                txtGenbaCd.Text = string.Empty;
                txtGenbaName.Text = string.Empty;
            }
        }

        public void BeforePopupGyousha()
        {
            this.preGyoushaCd = this.txtGyosyaCd.Text;
        }
        //20150703 #11161 hoanghm 業者をクリアしたら現場もクリアすること end

        // thongh 20150803 #11932 start
        private void txtKyotenCd_TextChanged(object sender, EventArgs e)
        {
            bool catchErr = true;
            string txtKyotrnCd = this.ChangeValue(this.txtKyotenCd.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (beforeValuesForKyoten.ContainsKey(this.txtKyotenCd.Name) && !txtKyotrnCd.Equals(this.beforeValuesForKyoten[this.txtKyotenCd.Name]))
            {
                if (this.mrwTukigimeUriageDenpyo.RowCount != 0)
                {
                    this.mrwTukigimeUriageDenpyo.Rows.Remove(0, this.mrwTukigimeUriageDenpyo.Rows.Count);
                }
                this.logic.SearchResult = null;
            }
            // 前回値チェック用データをセット
            if (beforeValuesForKyoten.ContainsKey(this.txtKyotenCd.Name))
            {
                beforeValuesForKyoten[this.txtKyotenCd.Name] = txtKyotrnCd;
            }
            else
            {
                beforeValuesForKyoten.Add(this.txtKyotenCd.Name, txtKyotrnCd);
            }
        }

        /// <summary> 指定Valueに変換した文字列を取得する </summary>
        /// <param name="value">変換対象を表す文字列</param>
        /// <returns>指定Valueに変換後文字列</returns>
        private string ChangeValue(string value, out bool catchErr)
        {
            catchErr = true;

            // フォーマット変換後文字列
            string ret = string.Empty;

            try
            {
                // 引数がブランクの場合はブランクを返す
                if (value.Trim() != string.Empty)
                {
                    ret = Convert.ToInt16(value).ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeValue", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            return ret;
        }
        // thongh 20150803 #11932 end

        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 現場フォカスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGenbaCd_Leave(object sender, EventArgs e)
        {
            this.preGyoushaCd = this.txtGyosyaCd.Text;
        }
        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
    }
}
