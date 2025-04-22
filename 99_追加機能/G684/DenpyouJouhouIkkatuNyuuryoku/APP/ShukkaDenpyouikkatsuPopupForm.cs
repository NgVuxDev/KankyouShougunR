using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Logic;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.APP
{
    public partial class ShukkaDenpyouikkatsuPopupForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 範囲条件指定ロジッククラス
        /// </summary>
        private LogicClassShukka logic;

        /// <summary>
        /// エラー発生状態(True:エラー発生)
        /// </summary>
        private bool error;

        internal bool isInputError = false;

        /// <summary>
        /// 検索ボタンから入力されたか判断するフラグ
        /// </summary>
        internal bool isFromSearchButton;

        // <summary>
        /// クリックのフォーカス移動か判断するフラグ
        /// </summary>
        internal bool isNotMoveFocus;

        /// <summary>
        /// 諸口区分によるフォーカス移動用
        /// 諸口区分設定によってフォーカスを設定した場合に入力項目設定によるフォーカス移動処理を行いたくない場合にTrueを設定
        /// 入力項目設定によるフォーカス移動処理時にTrueだった場合にFalseにし、処理を中断させている
        /// </summary>
        internal bool isSetShokuchiForcus = false;

        /// <summary>
        /// KeyDownイベントで押されたキーを保存します
        /// </summary>
        internal KeyEventArgs keyEventArgs;

        /// <summary> 入力パラメータ </summary>
        public NyuuryokuParamDto NyuuryokuParam { get; set; }

        /// <summary>
        /// 前回フォーカスのあったコントロール名を保持します
        /// </summary>
        internal string beforControlName = string.Empty;

        /// <summary>
        /// 前々回フォーカスのあったコントロール名を保持します
        /// </summary>
        internal string beforbeforControlName = string.Empty;

        /// <summary>
        /// EnterかTabボタンが押下されたかどうかの判定フラグ
        /// </summary>
        internal bool pressedEnterOrTab = false;

        /// <summary>
        /// 諸口区分(車輌名用)の前回値を保持する
        /// </summary>
        internal bool oldSharyouShokuchiKbn = false;

        /// <summary>
        /// 車輌選択ポップアップ選択中フラグ
        /// </summary>
        internal bool isSelectingSharyouCd = false;

        /// <summary>
        /// 車輌CDが編集中かどうかのフラグ
        /// </summary>
        private bool editingSharyouCdFlag = false;

        /// <summary>
        /// 変更前荷済業者
        /// </summary>
        public string beforNizumiGyousha { get; set; }

        /// <summary>
        /// 諸口区分の前回値を保持する
        /// </summary>
        internal bool oldShokuchiKbn;

        #endregion

        public ShukkaDenpyouikkatsuPopupForm()
        {
            this.InitializeComponent();

            // ロジッククラス作成
            this.logic = new LogicClassShukka(this);

        }

        #region KeyDownイベントを発生させます
        /// <summary>
        /// KeyDownイベントを発生させます
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.keyEventArgs = e;
            if (e != null)
            {
                base.OnKeyDown(e);
            }
        }
        #endregion KeyDownイベントを発生させます

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            // エラーキャンセル
            this.error = false;

            // Formクローズ
            this.Close();
        }

        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);

            // 画面の初期化
            this.logic.WindowInit();
        }

        /// <summary>
        /// Formクリア処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void kuria(object sender, EventArgs e)
        {
            this.logic.Kuria();
        }

        /// <summary>
        /// 一括入力処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void Nyuuryoku(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            if (!this.logic.SaveParams()) { return; }

            // Formクローズ
            this.FormClose(sender, null);

        }

        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.error == true)
            {
                // エラーが発生している場合は閉じない
                // ※DialogResult設定を行っている場合はFormCloseしてしまうため
                e.Cancel = true;
            }
        }

        #region 取引先イベント
        /// <summary>
        /// 取引先フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isInputError)
            {
                bool catchErr = false;
                var torihikisaki = this.logic.accessor.GetTorihikisaki(this.TORIHIKISAKI_CD.Text, this.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (torihikisaki != null)
                {
                    // 諸口区分の前回値を取得
                    this.oldShokuchiKbn = (bool)torihikisaki.SHOKUCHI_KBN;
                }
                this.logic.TorihikisakiCdSet(); // 比較用取引先CDをセット
            }
        }

        /// <summary>
        /// 取引先更新イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            this.isNotMoveFocus = this.SetTorihikisaki();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }
        #endregion 取引先イベント

        #region 業者イベント
        /// <summary>
        /// 業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isFromSearchButton && !this.isInputError)
            {
                bool catchErr = false;
                var gyousha = this.logic.accessor.GetGyousha(this.GYOUSHA_CD.Text, this.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (gyousha != null)
                {
                    // 諸口区分の前回値を取得
                    this.oldShokuchiKbn = (bool)gyousha.SHOKUCHI_KBN;
                }
                this.logic.GyousyaCdSet();  //比較用業者CDをセット
            }
        }

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.isNotMoveFocus = this.SetGyousha();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }
            this.oldShokuchiKbn = false;
        }
        #endregion　業者イベント

        #region 現場イベント
        /// <summary>
        /// 現場フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isInputError)
            {
                bool catchErr = false;
                var genba = this.logic.accessor.GetGenba(this.GYOUSHA_CD.Text, this.GENBA_CD.Text, this.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (genba != null)
                {
                    // 諸口区分の前回値を取得
                    this.oldShokuchiKbn = (bool)genba.SHOKUCHI_KBN;
                }
                this.logic.GenbaCdSet();   // 比較用現場CDをセット
            }
        }

        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.isNotMoveFocus = this.SetGenba();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }
            this.oldShokuchiKbn = false;
        }

        /// <summary>
        /// 現場CDへフォーカス移動する
        /// 現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.GYOUSHA_CD.Text != this.logic.tmpGyousyaCd || this.GENBA_CD.Text != this.logic.tmpGenbaCd)
            {
                this.SetGenba();
                this.GENBA_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        #endregion 現場イベント

        #region 荷積業者イベント
        /// <summary>
        /// 荷積業者CDフォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            // 荷積業者を取得
            bool catchErr = false;
            var nizumiGyousha = this.logic.accessor.GetGyousha(this.NIZUMI_GYOUSHA_CD.Text, this.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
            if (catchErr)
            {
                return;
            }
            if (nizumiGyousha != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nizumiGyousha.SHOKUCHI_KBN;
            }

            if (!this.isFromSearchButton)
            {
                this.logic.NizumiGyoushaCdSet();  //比較用業者CDをセット
            }
        }

        /// <summary>
        /// 荷積業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            this.isNotMoveFocus = this.SetNizumiGyousha();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }
        #endregion 荷積業者イベント


        #region 荷積現場イベント
        /// <summary>
        /// 荷積現場CDフォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            // 荷降現場を取得
            bool catchErr = false;
            var nioroshiGenba = this.logic.accessor.GetGenba(this.NIZUMI_GYOUSHA_CD.Text, this.NIZUMI_GENBA_CD.Text, this.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
            if (catchErr)
            {
                return;
            }

            if (nioroshiGenba != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nioroshiGenba.SHOKUCHI_KBN;
            }

            this.logic.NizumiGenbaCdSet();  //比較用業者CDをセット
        }

        /// <summary>
        /// 荷積現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.isNotMoveFocus = this.SetNizumiGenba();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;

        }    

        /// <summary>
        /// 荷積現場CDへフォーカス移動する
        /// 荷積現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNiZumiGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.SetNizumiGenba();
            this.NIZUMI_GENBA_CD.Focus();

            // 初期化
            this.isFromSearchButton = false;

        }

        #endregion 荷積現場イベント

        #region 取引先CD・業者CD・現場CDの関連情報セット処理

        /// <summary>
        /// 取引先CDに関連する情報をセット
        /// </summary>
        public bool SetTorihikisaki()
        {
            // 初期化
            bool ret = false;
            bool catchErr = false;

            ret = this.logic.CheckTorihikisaki(out catchErr);
            if (catchErr)
            {
                return false;
            }

            this.logic.TorihikisakiCdSet();

            return ret;
        }

        /// <summary>
        /// 業者CDに関連する情報をセット
        /// </summary>
        public bool SetGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = false;

            ret = this.logic.CheckGyousha(out catchErr);
            if (catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 現場に関連する情報をセット
        /// </summary>
        public bool SetGenba()
        {
            // 初期化
            bool ret = false;
            bool catchErr = false;

            if ((String.IsNullOrEmpty(this.GYOUSHA_CD.Text) || !this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text)
                    || (this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.GYOUSHA_NAME_RYAKU.Text)))
                    || (String.IsNullOrEmpty(this.GENBA_CD.Text) || !this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text)
                    || (this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text) && string.IsNullOrEmpty(this.GENBA_NAME_RYAKU.Text))))
            {
                ret = this.logic.CheckGenba(out catchErr);
                if (catchErr || !ret)
                {
                    return false;
                }
            }

            return ret;
        }

        /// <summary>
        /// 荷積業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNizumiGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = false;

            ret = this.logic.CheckNizumiGyoushaCd(out catchErr);

            if (catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 荷積現場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNizumiGenba()
        {
            // 初期化
            bool ret = false;
            bool catchErr = false;

            ret = this.logic.CheckNizumiGenbaCd(out catchErr);

            if (catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 運搬業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetUnpanGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = false;

            ret = this.logic.CheckUnpanGyoushaCd(out catchErr);

            if (catchErr)
            {
                return false;
            }

            this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット

            return ret;
        }

        #endregion 取引先CD・業者CD・現場CDの関連情報セット処理

        #region POPUP_BEF
        /// <summary>
        /// 取引先POPUP_BEF
        /// </summary>
        public void TorihikisakiPopupBefore()
        {
            this.logic.TorihikisakiCdSet();
        }

        /// <summary>
        /// 業者POPUP_BEF
        /// </summary>
        public void GyoushaPopupBefore()
        {
            this.logic.GyousyaCdSet();
        }

        /// <summary>
        /// 現場POPUP_BEF
        /// </summary>
        public void GenbaPopupBefore()
        {
            this.logic.GyousyaCdSet();
            this.logic.GenbaCdSet();
        }

        /// <summary>
        /// 運搬業者POPUP_BEF
        /// </summary>
        public void UpanGyoushaPopupBefore()
        {
            this.logic.UnpanGyoushaCdSet();
        }

        /// <summary>
        /// 荷済業者POPUP_BEF
        /// </summary>
        public void SetNizumiGyoushaPopupBef()
        {
            this.beforNizumiGyousha = this.NIZUMI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積現場POPUP_BEF
        /// </summary>
        public void NizumiGyoushaPopupBefore()
        {
            this.logic.NizumiGyoushaCdSet();
            this.logic.NizumiGenbaCdSet();
        }
        #endregion POPUP_BEF

        /// <summary>
        /// 荷済業者POPUP_AFT
        /// </summary>
        public void SetNizumiGyoushaPopupAft()
        {
            this.SetNizumiGyousha();
        }

        /// <summary>
        /// 車輌フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHASHU_CD_Enter(object sender, EventArgs e)
        {
            // 比較用車種CDをセット
            this.logic.ShashuCdSet();
        }

        /// <summary>
        /// 車輌フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            if (!this.editingSharyouCdFlag)
            {
                this.logic.ShayouCdSet();   // 比較用車輌CDをセット
            }
        }

        /// <summary>
        /// 車輌検証後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            this.editingSharyouCdFlag = true;

            this.logic.CheckSharyou();

            this.editingSharyouCdFlag = false;
        }

        #region 車輌更新Validating
        /// <summary>
        /// 車輌CDのバリデートが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = false;
            bool retCheck = this.logic.SharyouDateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (!retCheck)
            {
                e.Cancel = true;
            }
        }
        #endregion

        /// <summary>
        /// 運搬業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット

            // 運搬業者を取得
            bool catchErr = false;
            var unpanGyousha = this.logic.accessor.GetGyousha(this.UNPAN_GYOUSHA_CD.Text, this.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
            if (catchErr)
            {
                return;
            }
            if (unpanGyousha != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)unpanGyousha.SHOKUCHI_KBN;
            }
        }

        /// <summary>
        /// 運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UNPAN_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            this.isNotMoveFocus = this.SetUnpanGyousha();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        /// <summary>
        /// 運転者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isFromSearchButton)
            {
                // 比較用運転者CDをセット
                this.logic.UntenshaCdSet();
            }
        }

        /// <summary>
        /// 運転者CDロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckUntensha();
        }

        #region 運転者Validating
        /// <summary>
        /// 運転者Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = false;
            bool retCheck = this.logic.UntenshaDateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (!retCheck)
            {
                e.Cancel = true;
            }
        }
        #endregion

        /// <summary>
        /// 形態区分フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEITAI_KBN_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isFromSearchButton)
            {
                // 比較用形態区分CDをセット
                this.logic.KeitaiKbnCdSet();
            }
        }

        /// <summary>
        /// 形態区分更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEITAI_KBN_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckKeitaiKbn();
        }

        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (ActiveControl != null)
                {
                    var forward = (Control.ModifierKeys & Keys.Shift) != Keys.Shift;

                    if ("TAIRYUU_BIKOU".Equals(this.beforbeforControlName) && (Control.ModifierKeys & Keys.Shift) != Keys.Shift)
                    {
                    }
                    else if (this.isSetShokuchiForcus)
                    {
                        // 諸口区分によるフォーカス移動の場合、ここで判定用のフラグを戻す
                        this.isSetShokuchiForcus = false;

                        if (!forward)
                        {
                            // Shiftの場合は諸口のCD⇒CDの前の項目の移動なので入力項目設定に従ってフォーカス移動を行う

                            // ActiveControlをCD項目に戻す
                            string activeControlName = this.ActiveControl.Name;
                            if (activeControlName.Equals(GYOUSHA_NAME_RYAKU.Name))
                            {
                                // 業者名⇒業者CD
                                this.ActiveControl = this.GYOUSHA_CD;
                            }
                            else if (activeControlName.Equals(GENBA_NAME_RYAKU.Name))
                            {
                                // 現場名⇒現場CD
                                this.ActiveControl = this.GENBA_CD;
                            }
                            else
                            {
                                this.ActiveControl = this.allControl.Where(c => c.Name == this.beforbeforControlName).FirstOrDefault();
                            }

                            this.logic.GotoNextControl(forward);
                        }
                    }
                    else
                    {
                        this.ActiveControl = this.allControl.Where(c => c.Name == this.beforbeforControlName).FirstOrDefault();

                        this.logic.GotoNextControl(forward);
                    }
                }
            }
        }

        public void DAIKAN_KBN_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DAIKAN_KBN.Text))
            {
                if (this.DAIKAN_KBN.Text == "1")
                {
                    this.DAIKAN_KBN_NAME.Text = "自社";
                }
                else if (this.DAIKAN_KBN.Text == "2")
                {
                    this.DAIKAN_KBN_NAME.Text = "他社";
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E058");
                    this.DAIKAN_KBN_NAME.Text = string.Empty;
                    this.DAIKAN_KBN.Focus();
                    return;
                }
            }
            else
            {
                this.DAIKAN_KBN_NAME.Text = string.Empty;
            }
        }
    }
}
