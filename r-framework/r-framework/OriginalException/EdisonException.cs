using System;

namespace r_framework.OriginalException
{
    /// <summary>
    /// Edisonオリジナル例外クラス
    /// </summary>
    public class EdisonException : Exception
    {
        public EdisonException()
        {
        }

        public EdisonException(string msg)
        {
            this.ErrorMessage = msg;
        }

        public EdisonException(Exception ex, string msg)
        {
            this.ErrorMessage = msg;
            this.Exception = ex;
        }

        /// <summary>
        /// ラップする例外
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// エラーコード
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage { get; set; }

    }
}
