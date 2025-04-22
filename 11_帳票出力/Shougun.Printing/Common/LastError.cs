using System;
using System.Text;
using System.Diagnostics;

namespace Shougun.Printing.Common
{
    // TODO:スレッドセーフにしたいが当面はシングルスレッド前提で
    /// <summary>
    /// ラストエラー設定/取得クラス
    /// </summary>
    public static class LastError
    {
        /// <summary>
        /// メッセージプロパティを保持するプライベートデータメンバ
        /// </summary>
        private static string message;

        /// <summary>
        /// メッセージを取得/設定する。
        /// 発生場所(Sourceプロパティ）も自動的に設定される。
        /// </summary>
        public static string Message 
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var caller = (new StackTrace()).GetFrames()[1].GetMethod();
                    LastError.Source = string.Format("{0}.{1}", caller.DeclaringType.FullName, caller.Name);
                }
                message = value;
            }
            get
            {
                return message;
            }
        }

        /// <summary>
        /// 発生場所(LastError.Messageを設定した呼び出し元)を取得する。
        /// </summary>
        public static string Source { get; private set; }

        /// <summary>
        /// 例外プロパティを保持するプライベートデータメンバ
        /// </summary>
        private static Exception exception;
        
        /// <summary>
        /// 例外情報を取得/設定する。
        /// メッセージプロパティが空の場合はメッセージプロパティも自動的に設定される。
        /// </summary>
        public static Exception Exception
        {
            set
            {
                if (value !=  null && string.IsNullOrEmpty(LastError.message))
                {
                    LastError.message = value.Message;
                    var frame = (new StackTrace()).GetFrames()[1];
                    var caller = frame.GetMethod();
                    LastError.Source = string.Format("{0}.{1}, {2}({3}行目)", caller.DeclaringType.FullName, caller.Name, frame.GetFileName(), frame.GetFileLineNumber());
                }
                exception = value;
            }
            get
            {
                return exception;
            }
        }

        /// <summary>
        /// エラー情報をもっているかどうかを判定する。
        /// </summary>
        public static bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(LastError.message);
            }
        }

        /// <summary>
        /// エラー情報をクリアする。
        /// </summary>
        public static void Clear()
        {
            Message = "";
            Source = "";
            Exception = null;
        }

        /// <summary>
        /// 例外と発生箇所も含めた長いメッセージを取得する。
        /// </summary>
        public static string FullMessage
        {
            get
            {
                var buf = new StringBuilder();
                if (LastError.HasError)
                {
                    buf.AppendLine(LastError.Message);
                    if (LastError.Exception != null)
                    {
                        if (LastError.Exception.Message != LastError.Message)
                        {
                            buf.AppendLine(LastError.Exception.Message);
                        }
                    }

                    if (!string.IsNullOrEmpty(LastError.Source))
                    {
                        buf.AppendLine(LastError.Source);
                    }
                }
                return buf.ToString();
            }
        }
    }
}
