using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace r_framework.Utility
{
    /// <summary>
    /// Entity関連ユーティリティクラス
    /// </summary>
    public static class EntityUtility
    {
        /// <summary>
        /// EntityをDataTableに変換(型無視)
        /// </summary>
        /// <param name="entity">SuperEntityを継承したEntityクラス配列</param>
        /// <returns></returns>
        public static DataTable EntityToDataTable(SuperEntity[] entity)
        {
            var dt = new DataTable();

            if (entity == null || entity.Length == 0)
            {
                return dt;
            }

            var dictEntity = entity.Select(s => s.ConvertToDictionary());

            dt.Columns.AddRange(dictEntity.First().Select(s => new DataColumn(s.Key)).ToArray());

            foreach (var r in dictEntity)
            {
                dt.Rows.Add(r.Select(s => s.Value).ToArray());
            }

            return dt;
        }

        /// <summary>
        /// データテーブルからエンティティの配列を作成します。
        /// ※データテーブルの列名とエンティティのプロパティ名が同じであること
        /// </summary>
        /// <typeparam name="T">SuperEntityを継承した、引数なしのコンストラクタを持つ型</typeparam>
        /// <param name="dt">データテーブル</param>
        /// <returns>エンティティの配列</returns>
        public static T[] DataTableToEntity<T>(DataTable dt) where T : SuperEntity, new()
        {
            // リスト作成
            var entities = new List<T>();

            // プロパティ名を鍵、型名を値とするディクショナリを作成
            var propDic = new T().GetType().GetProperties().ToDictionary(k => k.Name, v => v.PropertyType.Name);

            foreach (DataRow row in dt.Rows)
            {
                var entity = new T();
                foreach (DataColumn col in dt.Columns)
                {
                    if (!(row[col.ColumnName] is DBNull || row[col.ColumnName] == null) &&
                        propDic.ContainsKey(col.ColumnName))
                    {
                        // Nullでなければプロパティに値をセット
                        entity.SetValue(col.ColumnName, row[col.ColumnName].ToString(), propDic[col.ColumnName]);
                    }
                }

                entities.Add(entity);
            }

            // 配列にして返す
            return entities.ToArray();
        }
    }
}
