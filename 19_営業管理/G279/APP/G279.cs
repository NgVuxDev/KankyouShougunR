using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Dto;
using r_framework.Entity;
using System.Collections.Generic;

namespace Shougun.Core.BusinessManagement.DenshiShinseiNyuuryoku
{
    /// <summary>
    /// G279 申請入力
    /// </summary>
    class G279 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数
        /// [0] : WINDOW_TYPE
        /// [1] : T_DENSHI_SHINSEI_ENTRY.SYSTEM_ID
        /// [2] : T_DENSHI_SHINSEI_ENTRY.SEQ
        /// [3] : 初期化用DTO
        /// [4] : 登録用T_DENSHI_SHINSEI_ENTRY
        /// [5] : 登録用T_DENSHI_SHINSEI_DETAILのList
        /// </param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0 && args[0] != null)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            long systemId = -1L;
            if (args.Length > 1 && args[1] != null)
            {
                systemId = long.TryParse(args[1].ToString(), out systemId) ? systemId : -1;
            }

            int seq = -1;
            if (args.Length > 2 && args[2] != null)
            {
                seq = int.TryParse(args[2].ToString(), out seq) ? seq : -1;
            }

            // 初期化用DTO
            var initDto = new DenshiShinseiNyuuryokuInitDTO();
            if (args.Length > 3 && args[3] != null)
            {
                initDto = args[3] as DenshiShinseiNyuuryokuInitDTO;
            }

            var entry = new T_DENSHI_SHINSEI_ENTRY();
            if (args.Length > 4 && args[4] != null)
            {
                entry = args[4] as T_DENSHI_SHINSEI_ENTRY;
            }

            var detailList = new List<T_DENSHI_SHINSEI_DETAIL>();
            if (args.Length > 5 && args[5] != null)
            {
                detailList = args[5] as List<T_DENSHI_SHINSEI_DETAIL>;
            }

            var HeaderForm = new UIHeader();
            var callForm = new UIForm(HeaderForm, windowType, systemId, seq, initDto, entry, detailList);
            var popupForm = new BasePopForm(callForm, HeaderForm);

            return popupForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="form">表示を更新するフォーム</param>
        public void UpdateForm(Form form)
        {
        }
    }
}
