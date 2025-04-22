using System;

namespace Shougun.Core.Common.Login.CommonDefine
{
    /// <summary>
    /// デフォルトログイン情報
    /// </summary>
    public struct LoginInfo
    {
        /// <summary>
        /// ログインID
        /// </summary>
        public string Code;

        /// <summary>
        /// デフォルトパスワード
        /// </summary>
        public string Pwd;

        /// <summary>
        /// パスワード保存フラグ
        /// </summary>
        public bool PwdSaved;

        /// <summary>
        /// 社員名略称
        /// </summary>
        public string Name;

        /// <summary>
        /// 文字列化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Code = {0}, Pwd = {1}, PwdSaved = {2}",
                                  this.Code, this.Pwd, this.PwdSaved);
        }
    }
}