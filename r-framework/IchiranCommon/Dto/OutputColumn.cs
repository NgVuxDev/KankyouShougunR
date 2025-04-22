using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.IchiranCommon.Dto
{
    class OutputColumn : IComparable
    {
        /// <summary>
        /// 出力区分
        /// </summary>
        internal int OutputKbn { get; private set; }

        /// <summary>
        /// 項目ID
        /// </summary>
        internal int ID { get; private set; }

        /// <summary>
        /// 一覧出力項目選択表示順
        /// </summary>
        internal int DispNum { get; private set; }

        /// <summary>
        /// 項目表示名
        /// </summary>
        internal string DispName { get; private set; }

        /// <summary>
        /// 項目名
        /// </summary>
        internal string Name { get; private set; }

        /// <summary>
        /// テーブルID
        /// </summary>
        internal int TableID { get; private set; }

        /// <summary>
        /// 書式
        /// </summary>
        internal string Format { get; private set; }

        /// <summary>
        /// 必須フラグ
        /// </summary>
        internal bool Needs { get; private set; }

        /// <summary>
        /// Is Inxs SubApplication column
        /// </summary>
        internal bool IsColumnInxs { get; private set; }

        /// <summary>
        /// テーブル名が空かどうか
        /// </summary>
        internal bool IsTableEmpty
        {
            get
            {
                return this.TableID == 0;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dispName">項目表示名</param>
        /// <param name="name">項目名</param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="format">書式（0:なし, 1:数量, 2:重量, 3:金額, それ以外:string.Format()に使用可能な書式指定文字列</param>
        /// <param name="needs">必須フラグ</param>
        internal OutputColumn(int outputKbn, int id, int dispNum, string dispName, string name, int tableID, string format, bool needs = false, bool isColumnInxs = false)
        {
            this.OutputKbn = outputKbn;
            this.ID = id;
            this.DispNum = dispNum;
            this.DispName = dispName;
            this.Name = name;
            this.TableID = tableID;
            this.Format = format;
            this.Needs = needs;
            //Communicate InxsSubApplication Start
            this.IsColumnInxs = isColumnInxs;
            //Communicate InxsSubApplication End
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

            var dto = obj as OutputColumn;

            if (dto == null)
            {
                throw new ArgumentNullException();
            }

            return this.DispNum < dto.DispNum ? 1 : -1;
        }
    }
}
