using System;
using System.Data.SqlTypes;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;

namespace Shougun.Core.Carriage.UnchinNyuuRyoku
{
    class G153 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : 受付番号/システムID
            // 引数 arg[2] : 伝種
            SqlInt64 renkei_number = SqlInt64.Null;
            SqlInt16 renkei_denshu_kbn_cd = SqlInt16.Null;
            SuperEntity[] entitys = null;
            if (args.Length > 1 && args[1] != null)
            {
                renkei_number = Int64.Parse(args[1].ToString());
            }

            if (args.Length > 2 && args[2] != null)
            {
                renkei_denshu_kbn_cd = Int16.Parse(args[2].ToString());
            }

            if (args.Length > 3 && args[3] != null)
            {
                if (args[3] is SuperEntity[])
                {
                    entitys = args[3] as SuperEntity[];
                }
                else if (args[3] is SuperEntity)
                {
                    entitys = new SuperEntity[1];
                    entitys[0] = args[3] as SuperEntity;
                }
            }

            var callHeader = new Shougun.Core.Carriage.UnchinNyuuRyoku.UIHeaderForm();
            var callForm = new Shougun.Core.Carriage.UnchinNyuuRyoku.UnchinNyuuryokuForm(windowType, callHeader, renkei_denshu_kbn_cd, renkei_number, entitys);
            if (renkei_denshu_kbn_cd.IsNull && !renkei_number.IsNull)
            {
                // 受入番号が使えるかチェック
                var isExistUkeireData = callForm.IsExistData();
                if (!isExistUkeireData)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    callForm.Dispose();
                    return null;
                }
            }
            if (!callForm.CheckAuth())
            {
                callForm.Dispose();
                return null;
            }

            var businessForm = new BusinessBaseForm(callForm, callHeader);
            return businessForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            string renkei_number = "";
            string renkei_denshu_kbn_cd = "";
            if (args.Length <= 1)
            {
                return false;
            }
            if (args.Length > 1)
            {
                renkei_number = Convert.ToString(args[1]);
            }
            if (args.Length > 2)
            {
                renkei_denshu_kbn_cd = Convert.ToString(args[2]);
            }
            var baseForm = form as BusinessBaseForm;
            var uiForm = baseForm.inForm as UnchinNyuuryokuForm;
            if (string.IsNullOrEmpty(renkei_denshu_kbn_cd))
            {
                return (!string.IsNullOrEmpty(renkei_number) && uiForm.txt_DenpyouBango.Text == renkei_number);
            }
            else
            {
                return (uiForm.txt_Denpyousyurui.Text == renkei_denshu_kbn_cd && uiForm.txt_RenkeiNumber.Text == renkei_number);
            }
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name=form>表示を更新するフォーム</param>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること。
        public void UpdateForm(Form form)
        {
        }
    }
}
