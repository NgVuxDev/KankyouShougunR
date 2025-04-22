using System;

namespace Shougun.Core.ExternalConnection.CommunicateLib.Dtos
{
    /// <summary>
    /// ファイルアップロードDB接続定義
    /// </summary>
    public class InxsSubappConnectionDto
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dispName">画面表示名</param>
        /// <param name="connectionString">接続文字列</param>
        public InxsSubappConnectionDto(string dispName, string connectionString, string shougunRConnectionString)
        {
            this.DispName = dispName;
            this.ConnectionString = connectionString;
            this.ShougunRConnectionString = shougunRConnectionString;
        }

        /// <summary>
        /// 表示名
        /// </summary>
        public string DispName { get; set; }

        /// <summary>
        /// SQLServer接続文字列
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Shougun SQLServer接続文字列
        /// </summary>
        public string ShougunRConnectionString { get; set; }

        /// <summary>
        /// ファイルアップロード用DB接続可能チェック
        /// </summary>
        /// <returns>True : 接続成功, false : 接続失敗</returns>
        public bool CanConnect()
        {
            // 実際にConnectionした結果を返す
            try
            {
                using (var cn = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    cn.Open();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Equals(InxsSubappConnectionDto other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return this.DispName == other.DispName && this.ConnectionString == other.ConnectionString;
        }

        public override int GetHashCode()
        {
            return this.DispName.GetHashCode() ^ this.ConnectionString.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("DispName = {0}, ConnectionString = {1}}, ShougunRConnectionString = {2}",
                                  this.DispName, this.ConnectionString, this.ShougunRConnectionString);
        }
    }
}
