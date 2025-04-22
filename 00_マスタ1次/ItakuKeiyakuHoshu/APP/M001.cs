// $Id: M001.cs 21063 2014-05-21 02:17:02Z gai $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.Dao;

namespace ItakuKeiyakuHoshu.APP
{
    /// <summary>
    /// M001 委託契約入力
    /// </summary>
    class M001 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            string systemId = string.Empty;
            if (args.Length > 1)
            {
                systemId = args[1].ToString();
                string dispTourokuHouhou = args[2].ToString(); ;

                M_ITAKU_KEIYAKU_KIHON cond = new M_ITAKU_KEIYAKU_KIHON();
                cond.SYSTEM_ID = systemId;
                M_ITAKU_KEIYAKU_KIHON entity = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>().GetDataBySystemId(cond);
                if (entity != null)
                {
                    if (entity.ITAKU_KEIYAKU_TYPE.IsNull || entity.ITAKU_KEIYAKU_TYPE == 1)
                    {
                        var sanpaiForm = new ItakuKeiyakuHoshuForm(windowType, systemId, short.Parse(dispTourokuHouhou));
                        return new BusinessBaseForm(sanpaiForm, windowType);
                    }
                    else
                    {
                        var kenpaiForm = new ItakuKeiyakuKenpaiHoshuForm(windowType, systemId, short.Parse(dispTourokuHouhou));
                        return new BusinessBaseForm(kenpaiForm, windowType);
                    }
                }
            }

            var callForm = new SelectForm();
            callForm.ShowDialog();
            callForm.Dispose();
            return null;
        }

        /// <summary>
        /// 同一情報存在問合せ処理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            if (args.Length > 1)
            {
                string systemId = args[1].ToString();
                M_ITAKU_KEIYAKU_KIHON cond = new M_ITAKU_KEIYAKU_KIHON();
                cond.SYSTEM_ID = systemId;
                M_ITAKU_KEIYAKU_KIHON entity = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>().GetDataBySystemId(cond);
                if (entity != null)
                {
                    if (entity.ITAKU_KEIYAKU_TYPE.IsNull || entity.ITAKU_KEIYAKU_TYPE == 1)
                    {
                        var businessForm = form as BusinessBaseForm;
                        var uiForm = businessForm.inForm as ItakuKeiyakuHoshuForm;
                        if (uiForm != null &&
                            (uiForm.WindowType == (WINDOW_TYPE)args[0]
                            || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG)))
                        {
                            return (uiForm.SYSTEM_ID.Text.Equals(systemId));
                        }
                    }
                    else
                    {
                        var businessForm = form as BusinessBaseForm;
                        var uiForm = businessForm.inForm as ItakuKeiyakuKenpaiHoshuForm;
                        if (uiForm != null && 
                            (uiForm.WindowType == (WINDOW_TYPE)args[0]
                            || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG)))
                        {
                            return (uiForm.SYSTEM_ID.Text.Equals(systemId));
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// フォーム更新処理
        /// </summary>
        /// <param name="form"></param>
        public void UpdateForm(Form form)
        {
        }
    }
}
