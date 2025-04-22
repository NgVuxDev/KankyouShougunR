using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Logic;
using Seasar.Framework.Exceptions;
using r_framework.Utility;
using r_framework.Logic;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.APP
{
    public partial class UriageDenpyouikkatsuPopupForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 範囲条件指定ロジッククラス
        /// </summary>
        private LogicClassUriage logic;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 検索ボタンから入力されたか判断するフラグ
        /// </summary>
        internal bool isFromSearchButton;

        internal bool isInputError = false;

        /// <summary>
        /// 諸口区分の前回値を保持する
        /// </summary>
        internal bool oldShokuchiKbn;

        /// 車輌CDが編集中かどうかのフラグ
        /// </summary>
        private bool editingSharyouCdFlag = false;

        /// <summary>
        /// 諸口区分(車輌名用)の前回値を保持する
        /// </summary>
        internal bool oldSharyouShokuchiKbn = false;

        /// <summary>
        /// 車輌選択ポップアップ選択中フラグ
        /// </summary>
        internal bool isSelectingSharyouCd = false;

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
        /// クリックのフォーカス移動か判断するフラグ
        /// </summary>
        internal bool isNotMoveFocus;

        /// <summary>
        /// エラー発生状態(True:エラー発生)
        /// </summary>
        private bool error;

        #endregion

        public UriageDenpyouikkatsuPopupForm()
        {
            this.InitializeComponent();

            // ロジッククラス作成
            this.logic = new LogicClassUriage(this);

        }

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
        /// 取引先 PopupAfter
        /// </summary>
        public void PopupAfterTorihikisaki()
        {
            bool catchErr = false;
            this.SetTorihikisaki(out catchErr);
        }

        /// <summary>
        /// 取引先CDに関連する情報をセット
        /// </summary>
        public bool SetTorihikisaki(out bool catchErr)
        {
            // 初期化
            bool ret = false;
            catchErr = false;

            try
            {
                ret = this.logic.CheckTorihikisaki();
                this.logic.TorihikisakiCdSet();
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetTorihikisaki", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = true;
                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetTorihikisaki", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = true;
                ret = true;
            }
            return ret;
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
        /// 取引先更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void TORIHIKISAKI_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            bool catchErr = false;
            this.isNotMoveFocus = this.SetTorihikisaki(out catchErr);
            if (catchErr)
            {
                return;
            }
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
            if (!this.isInputError)
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
            bool catchErr = false;
            this.isNotMoveFocus = this.SetGyousha(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }
            this.oldShokuchiKbn = false;
        }

        #endregion 業者イベント

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
            bool catchErr = false;
            this.isNotMoveFocus = this.SetGenba(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }
            this.oldShokuchiKbn = false;
        }
        #endregion 現場イベント

        /// <summary>
        /// 荷積業者CDフォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 荷積業者を取得
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

            this.logic.NizumiGyoushaCdSet();  //比較用業者CDをセット
        }

        /// 荷積業者更新後処理
        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            bool catchErr = false;
            this.isNotMoveFocus = this.SetNizumiGyousha(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        /// <summary>
        /// 荷積現場CDフォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 荷降現場を取得
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

            bool catchErr = false;
            this.isNotMoveFocus = this.SetNizumiGenba(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        /// 荷降業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 荷降業者を取得
            var nioroshiGyousha = this.logic.accessor.GetGyousha(this.NIOROSHI_GYOUSHA_CD.Text, this.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
            if (catchErr)
            {
                return;
            }
            if (nioroshiGyousha != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nioroshiGyousha.SHOKUCHI_KBN;
            }

            this.logic.NioroshiGyoushaCdSet();   // 比較用現場CDをセット               
        }

        /// <summary>
        /// 荷降業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            bool catchErr = false;
            this.isNotMoveFocus = this.SetNioroshiGyousha(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        /// <summary>
        /// 荷降現場フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 荷降現場を取得
            var nioroshiGenba = this.logic.accessor.GetGenba(this.NIOROSHI_GYOUSHA_CD.Text, this.NIOROSHI_GENBA_CD.Text, this.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
            if (catchErr)
            {
                return;
            }
            if (nioroshiGenba != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nioroshiGenba.SHOKUCHI_KBN;
            }

            this.logic.NioroshiGenbaCdSet();   // 比較用現場CDをセット               
        }

        /// <summary>
        /// 荷降現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            bool catchErr = false;
            this.isNotMoveFocus = this.SetNioroshiGenba(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        #region 荷降現場Validating
        /// <summary>
        /// 荷降現場Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.HannyuusakiDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion

        /// <summary>
        /// 現場に関連する情報をセット
        /// </summary>
        public bool SetGenba(out bool catchErr)
        {
            // 初期化
            bool ret = false;
            catchErr = false;

            try
            {
                if (this.isInputError || (String.IsNullOrEmpty(this.GYOUSHA_CD.Text) || !this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text)
                        || (this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.GYOUSHA_NAME_RYAKU.Text)))
                        || (String.IsNullOrEmpty(this.GENBA_CD.Text) || !this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text)
                        || (this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text) && string.IsNullOrEmpty(this.GENBA_NAME_RYAKU.Text))))
                {
                    ret = this.logic.CheckGenba();
                }

            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetGenba", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = true;
                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetGenba", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = true;
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// 業者CDに関連する情報をセット
        /// </summary>
        public bool SetGyousha(out bool catchErr)
        {
            // 初期化
            bool ret = false;
            catchErr = false;

            try
            {
                ret = this.logic.CheckGyousha();

            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetGyousha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = true;
                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetGyousha", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = true;
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// 荷積業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNizumiGyousha(out bool catchErr)
        {
            try
            {
                // 初期化
                bool ret = false;
                catchErr = false;

                ret = this.logic.CheckNizumiGyoushaCd();

                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetNizumiGyousha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetNizumiGyousha", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 荷降現場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNioroshiGenba(out bool catchErr)
        {
            try
            {
                // 初期化
                bool ret = false;
                catchErr = false;

                ret = this.logic.CheckNioroshiGenbaCd();

                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetNioroshiGenba", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetNioroshiGenba", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 荷積現場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNizumiGenba(out bool catchErr)
        {
            try
            {
                // 初期化
                bool ret = false;
                catchErr = false;

                ret = this.logic.CheckNizumiGenbaCd();

                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetNizumiGenba", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetNizumiGenba", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 荷降業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNioroshiGyousha(out bool catchErr)
        {
            try
            {
                // 初期化
                bool ret = false;
                catchErr = false;

                ret = this.logic.CheckNioroshiGyoushaCd();

                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetNioroshiGyousha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetNioroshiGyousha", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 運搬業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetUnpanGyousha(out bool catchErr)
        {
            // 初期化
            bool ret = false;
            catchErr = false;

            try
            {
                ret = this.logic.CheckUnpanGyoushaCd();
                this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetUnpanGyousha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = true;
                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetUnpanGyousha", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = true;
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// 運搬業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット

            // 運搬業者を取得
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

            bool catchErr = false;
            this.isNotMoveFocus = this.SetUnpanGyousha(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
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

        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.logic.tmpGyousyaCd == this.GYOUSHA_CD.Text)
            {
                return;
            }

            bool catchErr = false;
            this.SetGyousha(out catchErr);
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

        #region 車輌更新Validating
        /// <summary>
        /// 車輌CDのバリデートが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.SharyouDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion

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

        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            this.logic.tmpGyousyaCd = this.GYOUSHA_CD.Text;
        }

        public void GENBA_PopupAfterExecuteMethod()
        {
            if (this.logic.tmpGyousyaCd == this.GYOUSHA_CD.Text && this.logic.tmpGenbaCd == this.GENBA_CD.Text)
            {
                return;
            }

            bool catchErr = false;
            this.SetGenba(out catchErr);
        }

        public void GENBA_PopupBeforeExecuteMethod()
        {
            this.logic.tmpGyousyaCd = this.GYOUSHA_CD.Text;
            this.logic.tmpGenbaCd = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 運転者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Enter(object sender, EventArgs e)
        {
            // 比較用運転者CDをセット
            this.logic.UntenshaCdSet();
        }

        /// <summary>
        /// 運転者CDチェック
        /// </summary>
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
            if (!this.logic.UntenshaDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion
        /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　end

        /// <summary>
        /// 営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckEigyouTantousha();
        }

        /// <summary>
        /// 荷積業者POPUP_AFT
        /// </summary>
        public void PopupAftZizumiGyousha()
        {
            bool catchErr = false;
            this.SetNizumiGyousha(out catchErr);
        }

        /// <summary>
        /// 荷積業者POPUP_BEF
        /// </summary>
        public void PopupBefZizumiGyousha()
        {
            this.logic.tmpNizumiGyoushaCd = this.NIZUMI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積現場POPUP_AFT
        /// </summary>
        public void PopupAftZizumiGenba()
        {
            bool catchErr = false;
            this.SetNizumiGenba(out catchErr);
        }

        /// <summary>
        /// 荷積現場POPUP_BEF
        /// </summary>
        public void PopupBefZizumiGenba()
        {
            this.logic.tmpNizumiGenbaCd = this.NIZUMI_GENBA_CD.Text;
        }

        /// <summary>
        /// 荷降業者POPUP_AFT
        /// </summary>
        public void PopupAftZioroshiGyousha()
        {
            bool catchErr = false;
            this.SetNioroshiGyousha(out catchErr);
        }

        /// <summary>
        /// 荷降業者POPUP_BEF
        /// </summary>
        public void PopupBefZioroshiGyousha()
        {
            this.logic.tmpNioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷降現場POPUP_AFT
        /// </summary>
        public void PopupAftZioroshiGenba()
        {
            bool catchErr = false;
            this.SetNioroshiGenba(out catchErr);
        }

        /// <summary>
        /// 荷降現場POPUP_BEF
        /// </summary>
        public void PopupBefZioroshiGenba()
        {
            this.logic.tmpNioroshiGenbaCd = this.NIOROSHI_GENBA_CD.Text;
        }

        /// <summary>
        /// 運搬業者 PopupAfter
        /// </summary>
        public void PopupAfterUnpanGyousha()
        {
            bool catchErr = false;
            this.SetUnpanGyousha(out catchErr);
        }

        /// <summary>
        /// 形態区分フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEITAI_KBN_CD_Enter(object sender, EventArgs e)
        {
            // 比較用形態区分CDをセット
            this.logic.KeitaiKbnCdSet();
        }

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
    }
}
