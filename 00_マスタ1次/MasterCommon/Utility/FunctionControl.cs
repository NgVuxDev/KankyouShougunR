using r_framework.APP.Base;
using r_framework.Utility;

namespace MasterCommon.Utility
{
    /// <summary>
    /// ファンクションボタン制御
    /// </summary>
    public class FunctionControl
    {
        /// <summary>
        /// MasterBaseForm用ファンクションボタン制御処理
        /// </summary>
        /// <param name="isEnable"></param>
        public static void ControlFunctionButton(MasterBaseForm baseForm, bool isEnable)
        {
            LogUtility.DebugMethodStart(baseForm, isEnable);

            if (!string.IsNullOrWhiteSpace(baseForm.bt_func4.Text))
            {
                baseForm.bt_func4.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func6.Text))
            {
                baseForm.bt_func6.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func9.Text))
            {
                baseForm.bt_func9.Enabled = isEnable;
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// BusinessBaseForm用ファンクションボタン制御処理
        /// </summary>
        /// <param name="isEnable"></param>
        public static void ControlFunctionButton(BusinessBaseForm baseForm, bool isEnable)
        {
            LogUtility.DebugMethodStart(baseForm, isEnable);

            if (!string.IsNullOrWhiteSpace(baseForm.bt_func4.Text))
            {
                baseForm.bt_func4.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func6.Text))
            {
                baseForm.bt_func6.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func9.Text))
            {
                baseForm.bt_func9.Enabled = isEnable;
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// IchiranBaseForm用ファンクションボタン制御処理
        /// </summary>
        /// <param name="isEnable"></param>
        public static void ControlFunctionButton(IchiranBaseForm baseForm, bool isEnable)
        {
            LogUtility.DebugMethodStart(baseForm, isEnable);

            if (!string.IsNullOrWhiteSpace(baseForm.bt_func3.Text))
            {
                baseForm.bt_func3.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func4.Text))
            {
                baseForm.bt_func4.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func5.Text))
            {
                baseForm.bt_func5.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func6.Text))
            {
                baseForm.bt_func6.Enabled = isEnable;
            }

            LogUtility.DebugMethodEnd();
        }
    }
}
