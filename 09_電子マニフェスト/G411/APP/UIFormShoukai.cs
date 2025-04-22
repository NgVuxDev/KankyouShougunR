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
using Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Logic;

namespace Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai
{
    /// <summary>
    /// 通知情報照会
    /// </summary>
    public partial class TuuchiJouhouShoukai : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private TuuchiJouhouShoukaiLogic logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 通知開始日
        /// </summary>
        public string TuuchiBiFrom { get; private set; }

        /// <summary>
        /// 通知終了日
        /// </summary>
        public string TuuchiBiTo { get; private set; }

        /// <summary>
        /// 確認
        /// </summary>
        public string ReadFlag { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TuuchiJouhouShoukai()
            : base(WINDOW_ID.T_TUCHI_RIREKI_SHOKAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new TuuchiJouhouShoukaiLogic(this);
        }

        #endregion

        #region 画面読み込み処理
        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            //初期化
            if (!this.logic.WindowInit())
            {
                return;
            }

            //フォーカスを設定する
            this.cDtPicker_TuuchiBiFrom.Focus();

        }
        #endregion

        #region Formクローズ処理
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }
        #endregion

        #region 検索処理
        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Kensaku(object sender, EventArgs e)
        {
            /// 20141021 Houkakou 「通知照会」の日付チェックを追加する　start
            bool catchErr = false;
            bool retCheck = this.logic.DateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (retCheck)
            {
                return;
            }
            /// 20141021 Houkakou 「通知照会」の日付チェックを追加する　end

            //検索条件を設定する
            if (!string.IsNullOrEmpty(this.cDtPicker_TuuchiBiFrom.Text.Trim()))
                this.TuuchiBiFrom = this.cDtPicker_TuuchiBiFrom.Text.Substring(0, 10).ToString().Replace("/", "");
            if (!string.IsNullOrEmpty(this.cDtPicker_TuuchiBiTo.Text.Trim()))
                this.TuuchiBiTo = this.cDtPicker_TuuchiBiTo.Text.Substring(0, 10).ToString().Replace("/", "");
            this.ReadFlag = this.txt_Kakunin.Text;

            this.logic.Kensaku(this.TuuchiBiFrom,
                                this.TuuchiBiTo,
                                this.txt_Kakunin.Text);
        }
        #endregion

        #region 明細ダブルクリック
        /// <summary>
        /// 重要な通知ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Jyuuyou_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            /// 20141021 Houkakou 「通知照会」の日付チェックを追加する　start
            bool catchErr = false;
            bool retCheck = this.logic.DateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (retCheck)
            {
                return;
            }
            /// 20141021 Houkakou 「通知照会」の日付チェックを追加する　end

            string tuuchiCd = string.Empty;

            if (this.cdgv_Jyuuyou.CurrentRow != null)
            {
                tuuchiCd = this.cdgv_Jyuuyou.CurrentRow.Cells["jyuuyouTuuchiCd"].Value.ToString();
            }

            this.logic.TsuuchiJouhouMeisaiShow(this.TuuchiBiFrom, this.TuuchiBiTo, this.ReadFlag, tuuchiCd);
        }

        /// <summary>
        /// お知らせ通知ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_Oshirase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            /// 20141021 Houkakou 「通知照会」の日付チェックを追加する　start
            bool catchErr = false;
            bool retCheck = this.logic.DateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (retCheck)
            {
                return;
            }
            /// 20141021 Houkakou 「通知照会」の日付チェックを追加する　end

            string tuuchiCd = string.Empty;

            if (this.cdgv_Oshirase.CurrentRow != null)
            {
                tuuchiCd = this.cdgv_Oshirase.CurrentRow.Cells["OshiraseTuuchiCd"].Value.ToString();
            }

            this.logic.TsuuchiJouhouMeisaiShow(this.TuuchiBiFrom, this.TuuchiBiTo, this.ReadFlag, tuuchiCd);
        }
        #endregion

        /// 20141021 Houkakou 「通知照会」の日付チェックを追加する　start
        private void cDtPicker_TuuchiBiFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cDtPicker_TuuchiBiTo.Text))
            {
                this.cDtPicker_TuuchiBiTo.IsInputErrorOccured = false;
                this.cDtPicker_TuuchiBiTo.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void cDtPicker_TuuchiBiTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cDtPicker_TuuchiBiFrom.Text))
            {
                this.cDtPicker_TuuchiBiFrom.IsInputErrorOccured = false;
                this.cDtPicker_TuuchiBiFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }

        /// 20141021 Houkakou 「通知照会」の日付チェックを追加する　end
    }
}
