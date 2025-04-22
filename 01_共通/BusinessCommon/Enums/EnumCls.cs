using System.ComponentModel;
namespace Shougun.Core.Common.BusinessCommon.Enums
{
    public enum EnumUploadSatus
    {
        /// <summary>アップロード状況：1.未</summary>
        [Description("未")]
        MI = 1,
        /// <summary>アップロード状況：2.済</summary>
        [Description("済")]
        SUMI = 2,
        /// <summary>アップロード状況：3.取消</summary>
        [Description("取消")]
        CANCEL = 3,
        /// <summary>アップロード状況：4.エラー</summary>
        [Description("エラー")]
        ERROR = 4,
        /// <summary>アップロード状況：5.中</summary>
        [Description("中")]
        CHUU = 5,
        /// <summary>アップロード状況：6.全て</summary>
        [Description("全て")]
        SUBETE = 6
    }

    public enum EnumDownloadSatus
    {
        /// <summary>ダウンロード状況：1.未</summary>
        [Description("未")]
        MI = 1,
        /// <summary>ダウンロード状況：2.済</summary>
        [Description("済")]
        SUMI = 2
    }

    public enum EnumExecuteAction : int
    {
        /// <summary>DELETE</summary>
        DELETE = 1
    }

    public enum EnumMessageType : int
    {
        /// <summary>DELETE</summary>
        CONFIRM = 1,
        /// <summary>WARN</summary>
        WARN = 2,
        /// <summary>ERROR</summary>
        ERROR = 3,
        /// <summary>INFO</summary>
        INFO = 4
    }

    public enum EnumManifestType : int
    {
        KAMI = 1,
        DENSHI = 2,
    }
}
