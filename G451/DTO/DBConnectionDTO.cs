using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.Login
{
    /// <summary>
    /// DB接続定義
    /// </summary>
    internal class DBConnectionDTO
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dispName">画面表示名</param>
        /// <param name="connectionString">接続文字列</param>
        /// <param name="isSelected">True:選択, False:未選択</param>
        public DBConnectionDTO(string dispName, string connectionString)
        {
            this.DispName = dispName;
            this.ConnectionString = connectionString;
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
        /// DB接続可能チェック
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
                r_framework.Utility.LogUtility.Error("DB接続に失敗しました。", ex);
                return false;
            }

            return true;
        }

        public bool Equals(DBConnectionDTO other)
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
            return string.Format("DispName = {0}, ConnectionString = {1}",
                                  this.DispName, this.ConnectionString);
        }
    }
}
