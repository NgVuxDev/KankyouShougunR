using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data;

namespace Shougun.Function.ShougunCSCommon.Utility
{
    /// <summary>
    /// 各種要素から別要素へデータを変換・設定を行うクラス
    /// FW側で用意されていないものを独自実装する。
    /// </summary>
    public class DataBinderUtility<T> where T : SuperEntity, new()
    {
        /// <summary>
        /// Entity
        /// </summary>
        public T[] Entitys { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataBinderUtility()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataBinderUtility(T entity)
        {
            Entitys = new T[1];
            Entitys[0] = entity;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataBinderUtility(T[] entity)
        {
            Entitys = entity;
        }

        public T[] CreateEntityForDataTable(DataTable dt)
        {
            var returnEntitys = new T[dt.Rows.Count];
            var dtColumns = dt.Columns;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T entity = new T();
                var t = entity.GetType();
                var row = dt.Rows[i];
                foreach (var column in dtColumns)
                {
                    var pi = t.GetProperty(column.ToString());

                    if (pi == null)
                    {
                        continue;
                    }
                    // TODO: 以下で値が設定できるのか確認
                    entity.SetValue(column.ToString(), row[column.ToString()].ToString(), pi.PropertyType.Name);
                }
                returnEntitys[i] = entity;
            }

            return returnEntitys;
        }
    }
}
