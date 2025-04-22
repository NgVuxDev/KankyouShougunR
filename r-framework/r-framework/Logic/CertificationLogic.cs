using r_framework.Const;
using r_framework.Entity;
using r_framework.Setting;

namespace r_framework.Logic
{
    /// <summary>
    /// ユーザ認証クラス
    /// </summary>
    public class CertificationLogic
    {
        /// <summary>
        /// ユーザ情報
        /// </summary>
        public M_LOGIN_USER_INFO LoginUserInfo { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ユーザID</param>
        /// <param name="pass">パス</param>
        public CertificationLogic(string id, string pass)
        {
            this.LoginAuthentication(id, pass);
        }

        /// <summary>
        /// ログイン認証処理
        /// </summary>
        private void LoginAuthentication(string id, string pass)
        {

            var userInfo = new M_LOGIN_USER_INFO();

            userInfo.USER_ID = id;
            userInfo.PASSWORD = pass;

            LoginUserInfo = userInfo;

            SettingFileReadLogic.SettingFilePath = "settingfile.xml";

            var checkLogin = SettingFileReadLogic.LoadUserSettingFile();

            if (checkLogin.USER_ID == LoginUserInfo.USER_ID)
            {
                if (checkLogin.PASSWORD == LoginUserInfo.PASSWORD)
                {
                    LoginStatus = LoginStatus.Login;
                    return;
                }
            }
            LoginStatus = LoginStatus.Logout;
        }

        /// <summary>
        /// ログイン状態
        /// </summary>
        public LoginStatus LoginStatus { get; private set; }

        /// <summary>
        /// アカウント情報チェック
        /// </summary>
        public static bool CheckAccountInformation()
        {
            return false;
        }

        /// <summary>
        /// 重複ログインチェック
        /// </summary>
        public static bool LoginDuplicationCheck()
        {
            return false;
        }

        /// <summary>
        /// 体験版使用期限チェック
        /// </summary>
        public static bool TrialExpirationDateCheck()
        {
            return false;
        }

        /// <summary>
        /// イコール図メソッド
        /// </summary>
        /// <param name="other">対象クラス</param>
        /// <returns>結果</returns>
        public bool Equals(CertificationLogic other)
        {
            return this.Equals(other);
        }

        /// <summary>
        /// ハッシュコード取得メソッド
        /// </summary>
        /// <returns>取得したハッシュコード</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 文字列取得メソッド
        /// </summary>
        /// <returns>取得したクラス文字列</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

