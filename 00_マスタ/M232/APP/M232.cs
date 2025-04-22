using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.Master.CourseNyuryoku.APP;

namespace Shougun.Core.Master.CourseNyuryoku
{
    /// <summary>
    /// M232 コース入力画面
    /// </summary>
    class M232 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name="args">SgFormManager.OpenForm()の可変引数</param>
        /// <returns>作成したフォーム。失敗時はNull</returns>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0 && args[0] != null)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // DAY_CD
            string dayCd = string.Empty;
            if (args.Length > 1 && args[1] != null)
            {
                dayCd = args[1].ToString();
            }

            // COURSE_NAME_CD
            string courseNameCd = string.Empty;
            if (args.Length > 2 && args[2] != null)
            {
                courseNameCd = args[2].ToString();
            }

            // COURSE_NAME
            string courseName = string.Empty;
            if (args.Length > 3 && args[3] != null)
            {
                courseName = args[3].ToString();
            }

            // KYOTEN_CD
            string kyotenCd = string.Empty;
            if (args.Length > 4 && args[4] != null)
            {
                kyotenCd = args[4].ToString();
            }

            // KYOTEN_NAME
            string kyotenName = string.Empty;
            if (args.Length > 5 && args[5] != null)
            {
                kyotenName = args[5].ToString();
            }

            // DAY_CD複写用
            string dayCdF = string.Empty;
            if (args.Length > 6 && args[6] != null)
            {
                dayCdF = args[6].ToString(); ;
            }

            // COURSE_NAME_CD複写用
            string courseNameCdF = string.Empty;
            if (args.Length > 7 && args[7] != null)
            {
                courseNameCdF = args[7].ToString(); ;
            }

            // COURSE_NAME複写用
            string courseNameF = string.Empty;
            if (args.Length > 8 && args[8] != null)
            {
                courseNameF = args[8].ToString(); ;
            }

            var callForm = new UIForm(dayCd, courseNameCd, courseName, kyotenCd, kyotenName, dayCdF, courseNameCdF, courseNameF);
            return new BusinessBaseForm(callForm, new HeaderForm());
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <returns>true：同じ false:異なる</returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 一覧画面は無条件にtrue(最前面表示)
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
