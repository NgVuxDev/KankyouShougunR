
using System.Windows.Forms;
using r_framework.APP.Base;
namespace r_framework.Logic
{
    /// <summary>
    /// Formのコントロールを行うロジック
    /// 同一画面チェックなどの処理を記述
    /// </summary>
    public class FormControlLogic
    {
        /// <summary>
        ///  同一画面存在チェック
        /// </summary>
        /// <param name="mainForm">起動する予定のForm</param>
        /// <returns>起動可否フラグ</returns>
        public bool ScreenPresenceCheck(SuperForm mainForm)
        {
            var exists = false;

            foreach (Form openForm in Application.OpenForms)
            {
                if (mainForm.GetType() == openForm.GetType())
                {

                    var superForm = openForm as SuperForm;

                    if (superForm != null)
                    {
                        if (superForm.WindowType == mainForm.WindowType)
                        {
                            exists = true;
                            var parentForm = openForm.ParentForm;
                            if (parentForm != null)
                            {
                                parentForm.BringToFront();
                            }
                            return exists;
                        }
                    }
                }
            }
            return exists;
        }
    }
}
