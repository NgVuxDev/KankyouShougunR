using System;
using System.IO;
using System.Globalization;

namespace Shougun.Core.Common
{
    /// <summary>
    /// ログファイルの情報を保持するDto
    /// </summary>
    class LogFileDto : IComparable
    {
        /// <summary>
        ///  コンストラクタ
        /// </summary>
        /// <param name="file"></param>
        public LogFileDto(FileInfo file)
        {
            this.FileName = file.Name;
            if (this.FileName.Length > 8)
            {
                DateTime dt;
                if (DateTime.TryParseExact(file.Name.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    System.Globalization.DateTimeStyles.None, out dt))
                {
                    this.CreateDate = dt;
                }
                else
                {
                    this.CreateDate = file.LastWriteTime;
                }
            }
            else
            {
                this.CreateDate = file.LastWriteTime;
            }
            this.IsChecked = false;
            this.FilePath = file.FullName;
        }

        /// <summary>
        /// ファイル名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 作成日付
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// チェックされているかどうか
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// ファイルパス
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// IComparable実装の為の必須メソッド
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            // 引数がnullの場合はArgumentNullExceptionをスローする
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var dto = obj as LogFileDto;

            if (dto == null)
            {
                throw new ArgumentNullException();
            }

            return this.CreateDate.Date < dto.CreateDate.Date ? 1 : -1;
        }
    }
}
