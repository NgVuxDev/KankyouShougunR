using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data;
using r_framework.Utility;
using r_framework.CustomControl;
using r_framework.Logic;

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

        /// <summary>
        /// DataTable→Entityに変換
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
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

                    entity.SetValue(column.ToString(), row[column.ToString()].ToString(), pi.PropertyType.Name);
                }
                returnEntitys[i] = entity;
            }

            return returnEntitys;
        }

		/// <summary>
		/// MultiRowの表示データをDataTable化
		/// </summary>
		/// <param name="multiRow">抜き出す対象のMultiRow</param>
		/// <returns name="DataTable[]">抜き出したDataTable</returns>
		public DataTable GetDataTableForMultiRow(GcCustomMultiRow multiRow)
		{
			LogUtility.DebugMethodStart();

			// MultiRowのColumn名からテーブル定義作成
			DataTable table = new DataTable();
			string name = string.Empty;

			// CSV出力用にMultiRowIndexを生成
			MultiRowIndexCreateLogic MLLogic = new MultiRowIndexCreateLogic();
			MLLogic.multiRow = multiRow;
			MLLogic.CreateLocations();

			foreach(var dto in MLLogic.sortEndList)
			{
				// Header名の取得
				name = dto.Cells.Name.ToString();

				// フィールド名に置換
				name = name.Replace("_LABEL", "");
				
				if(false == string.IsNullOrEmpty(name))
			    {
			        // 表示しているものをTableとして登録
			        table.Columns.Add(name, typeof(string));
			    }
			}

			// MultiRowから値を取得
			foreach(GrapeCity.Win.MultiRow.Row mRow in multiRow.Rows)
			{
				DataRow row = table.NewRow();
				foreach(DataColumn Column in table.Columns)
				{
					row[Column.ColumnName] = mRow[Column.ColumnName].FormattedValue;
				}
				table.Rows.Add(row);
			}

			// Column名をHeaderTxtに変換
			int i = 0;
			foreach(DataColumn Column in table.Columns)
			{
				name = MLLogic.sortEndList[i].Cells.Value.ToString();
				Column.ColumnName = name.Trim();
				i++;
			}

			LogUtility.DebugMethodEnd(table);

			return table;
		}
	}
}
