using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;

namespace Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku
{
    /// <summary>
    /// G618 月次消費税調整入力
    /// </summary>
    class G618 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name="args">FormManager.OpenForm()の可変引数。
        /// メニューから呼び出す場合はパラメータなし。</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数のエラー有無判定
            bool hasError = false;

            // 引数 arg[0] : DateTime 月次年月
            DateTime dt = new DateTime();
            if (0 < args.Length && args[0] != null && DateTime.TryParse(args[0].ToString(), out dt))
            {
            }
            else
            {
                hasError = true;
            }

            // 引数 arg[1] : WINDOW_ID　画面ID
            WINDOW_ID windowId = WINDOW_ID.NONE;
            if (1 < args.Length && args[1] != null)
            {
                windowId = (WINDOW_ID)args[1];
            }

            if (WINDOW_ID.T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_UR != windowId
                && WINDOW_ID.T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_SH != windowId)
            {
                // G618 月次消費税調整入力 以外はエラー
                hasError = true;
            }

            if (hasError)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E076");
                return null;
            }

            var headerForm = new UIHeader();
            var callForm = new UIForm(headerForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG, dt, windowId);

            // モードレス呼出専用
            var form = new Shougun.Core.Common.BusinessCommon.Base.BaseForm.BasePopForm(callForm, headerForm);
            return form;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">FormManager.OpenForm()の可変引数</param>
        /// <return>
        /// true：同じ
        ///    表示中の画面と可変引数は同じ内容のものであることを示す。
        ///    trueを返却するとformで指定された画面が前面表示される。
        ///    一覧系や帳票出力系などの複数画面が必要ない場合は無条件でtrueを返却すること。
        ///    入力系画面で新規入力以外のモードの場合、argsが表示中の内容と同一のEntryの場合はtrueを返却すること。
        /// false:異なる。
        ///    表示中の画面とパラメータは異なる内容のものであることを示す。
        ///    argsでパラメータ指定された新規画面が作成される。
        ///    入力系画面で新規入力モードの場合はfalseを返却すること。
        ///    入力系画面で新規入力以外のモードの場合、argsが表示中の内容と異なるEntryの場合はfalseを返却すること。
        /// </return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="form">表示を更新するフォーム</param>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること。
        public void UpdateForm(Form form)
        {

        }
    }
}
