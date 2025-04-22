
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Setting;
namespace r_framework.Utility
{
    /// <summary>
    /// ボタンの操作を行うコントロールユーティリティ
    /// </summary>
    public static class ButtonControlUtility
    {
        /// <summary>
        /// ボタンの名称とヒントテキストの設定を行う
        /// </summary>
        /// <param name="buttonSetting">ファイルから読込んだボタン設定</param>
        /// <param name="parentForm">親のForm情報</param>
        /// <param name="WindowType">画面のタイプ情報</param>
        public static void SetButtonInfo(ButtonSetting[] buttonSetting, Form parentForm, WINDOW_TYPE WindowType)
        {
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                switch (WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        cont.Text = button.NewButtonName;

                        if (string.IsNullOrEmpty(button.NewButtonHintText))
                        {
                            cont.Tag = button.DefaultHintText;
                        }
                        else
                        {
                            cont.Tag = button.NewButtonHintText;
                        }
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        cont.Text = button.ReferButtonName;
                        if (string.IsNullOrEmpty(button.ReferButtonHintText))
                        {
                            cont.Tag = button.DefaultHintText;
                        }
                        else
                        {
                            cont.Tag = button.ReferButtonHintText;
                        }

                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        cont.Text = button.UpdateButtonName;
                        if (string.IsNullOrEmpty(button.UpdateButtonHintText))
                        {
                            cont.Tag = button.DefaultHintText;
                        }
                        else
                        {
                            cont.Tag = button.UpdateButtonHintText;
                        }

                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        cont.Text = button.DeleteButtonName;
                        if (string.IsNullOrEmpty(button.DeleteButtonHintText))
                        {
                            cont.Tag = button.DefaultHintText;
                        }
                        else
                        {
                            cont.Tag = button.DeleteButtonHintText;
                        }

                        break;

                    case WINDOW_TYPE.ICHIRAN_WINDOW_FLAG:
                        cont.Text = button.IchiranButtonName;
                        if (string.IsNullOrEmpty(button.IchiranButtonHintText))
                        {
                            cont.Tag = button.DefaultHintText;
                        }
                        else
                        {
                            cont.Tag = button.IchiranButtonHintText;
                        }

                        break;
                    case WINDOW_TYPE.NONE:
                        cont.Text = button.PopupButtonName;
                        if (string.IsNullOrEmpty(button.PopupButtonHintText))
                        {
                            cont.Tag = button.DefaultHintText;
                        }
                        else
                        {
                            cont.Tag = button.PopupButtonHintText;
                        }

                        break;
                }
            }
        }
    }
}
