using System.Collections.Generic;
using System;

namespace Shougun.Core.Common.IchiranCommon.Dto
{
    internal class OutputGroup : IComparable
    {
        /// <summary>
        /// 出力区分
        /// </summary>
        internal int OutputKbn { get; private set; }

        /// <summary>
        /// 項目IDに紐付く出力項目のリスト
        /// </summary>
        internal Dictionary<int, OutputColumn> OutputColumns;

        internal OutputGroup(int outputKbn)
        {
            this.OutputKbn = outputKbn;
            this.OutputColumns = new Dictionary<int, OutputColumn>();
        }

        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal OutputColumn this[int outputKbn]
        {
            get
            {
                return this.OutputColumns[outputKbn];
            }

            set
            {
                this.OutputColumns[outputKbn] = value;
            }
        }

        /// <summary>
        /// 項目IDと項目を追加します。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="outputColumn"></param>
        internal void Add(int columnID, OutputColumn outputColumn)
        {
            this.OutputColumns.Add(columnID, outputColumn);
        }

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

            var dto = obj as OutputGroup;

            if (dto == null)
            {
                throw new ArgumentNullException();
            }

            return this.OutputKbn < dto.OutputKbn ? 1 : -1;
        }
    }
}
