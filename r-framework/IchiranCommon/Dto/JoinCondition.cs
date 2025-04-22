using System;

namespace Shougun.Core.Common.IchiranCommon.Dto
{
    class JoinCondition : IComparable
    {
        /// <summary>
        /// テーブルID
        /// </summary>
        internal int TableID;

        /// <summary>
        /// JOINクエリ
        /// </summary>
        internal string Query;

        /// <summary>
        /// 有効チェック
        /// </summary>
        internal int ApplyCheckType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tableID">テーブルID</param>
        /// <param name="query">JOINクエリ</param>
        /// <param name="applyCheckType">有効チェックタイプ</param>
        internal JoinCondition(int tableID, string query)
        {
            this.TableID = tableID;
            this.Query = query;
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

            var dto = obj as JoinCondition;

            if (dto == null)
            {
                throw new ArgumentNullException();
            }

            return this.TableID < dto.TableID ? 1 : -1;
        }
    }
}
