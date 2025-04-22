using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib.Enums;
using Shougun.Core.ExternalConnection.CommunicateLib.Helpers;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using System;
using System.Web.Script.Serialization;

namespace Shougun.Core.ExternalConnection.CommunicateLib
{
    public class RemoteAppCls
    {
        public bool OpenForm(string windowName, INotificationModel model)
        {
            RemoteAppHelper msg = new RemoteAppHelper();
            var hWnd = msg.GetWindowId(null, windowName);
            if (hWnd > 0)
            {
                model.Type = NotificationType.OpenForm;
                string message = new JavaScriptSerializer().Serialize(model);
                msg.SendWindowsStringMessage(hWnd, 0, EncryptionUtility.Encrypt(message));
                return true;
            }

            return false;
        }

        public bool OpenDialog(string windowName, INotificationModel model)
        {
            RemoteAppHelper msg = new RemoteAppHelper();
            var hWnd = msg.GetWindowId(null, windowName);
            if (hWnd > 0)
            {
                model.Type = NotificationType.OpenDialog;
                string message = new JavaScriptSerializer().Serialize(model);
                msg.SendWindowsStringMessage(hWnd, 0, EncryptionUtility.Encrypt(message));
                return true;
            }

            return false;
        }

        public bool ExecuteCommand(string windowName, INotificationModel model)
        {
            RemoteAppHelper msg = new RemoteAppHelper();
            var hWnd = msg.GetWindowId(null, windowName);
            if (hWnd > 0)
            {
                model.Type = NotificationType.ExecuteCommand;
                string message = new JavaScriptSerializer().Serialize(model);
                msg.SendWindowsStringMessage(hWnd, 0, EncryptionUtility.Encrypt(message));
                return true;
            }

            return false;
        }

        public bool CloseForm(string windowName, INotificationModel model)
        {
            RemoteAppHelper msg = new RemoteAppHelper();
            var hWnd = msg.GetWindowId(null, windowName);
            if (hWnd > 0)
            {
                model.Type = NotificationType.CloseForm;
                string message = new JavaScriptSerializer().Serialize(model);
                msg.SendWindowsStringMessage(hWnd, 0, EncryptionUtility.Encrypt(message));
                return true;
            }

            return false;
        }

        public string GenerateToken(ITokenModel tokenModel)
        {
            return new JavaScriptSerializer().Serialize(tokenModel);
        }
    }
}