using System;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Dao;
using r_framework.Utility;

using System.Collections.Generic;


namespace r_framework.Logic
{
    /// <summary>
    /// 各種要素から別要素へデータを変換・設定を行うクラス
    /// </summary>
    public sealed class DataBinderLogic<T> where T : SuperEntity
    {
        /// <summary>
        /// Entity
        /// </summary>
        public T[] Entitys { get; set; }

        /// <summary>
        /// Form上のすべてのコントロール
        /// </summary>
        public Control[] AllControl { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataBinderLogic(T entity)
        {
            Entitys = new T[1];
            Entitys[0] = entity;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataBinderLogic(T[] entity)
        {
            Entitys = entity;
        }

        /// <summary>
        /// コントロールの内容をEntityへ設定するメソッド
        /// </summary>
        public void CreateEntityForControl()
        {
            foreach (var entity in Entitys)
            {
                foreach (var cont in AllControl.Where(s => s.Visible
                                                        && s as ICustomControl != null)
                                                        .OfType<ICustomControl>())
                {
                    if (!string.IsNullOrEmpty(cont.DBFieldsName)
                        && entity.GetType().GetProperty(cont.DBFieldsName) != null)
                    {
                        // 削除フラグにNULLが入ってしまう場合がある為
                        // 削除フラグがNULLの場合は強制的に設定（暫定対応:TODO）
                        if (cont.DBFieldsName == "DELETE_FLG" && string.IsNullOrWhiteSpace(cont.GetResultText()))
                        {
                            cont.SetResultText(SqlBoolean.False.ToString());
                            entity.SetValue(cont);
                        }
                        else
                        {
                            entity.SetValue(cont);
                        }
                    }
                }
                SetSystemProperty(entity, false);
            }
        }

        /// <summary>
        /// EntityからControlへ値の設定を行う
        /// </summary>
        public void SetControlItemForEntity()
        {
            foreach (var entity in Entitys)
            {
                foreach (var cont in AllControl.Where(s => s.Visible
                                                        && s as ICustomControl != null)
                                                        .OfType<ICustomControl>())
                {
                    if (!string.IsNullOrEmpty(cont.DBFieldsName))
                    {
                        if (entity.GetType().GetProperty(cont.DBFieldsName) == null)
                        {
                            continue;
                        }

                        object obj = entity.GetType()
                                         .InvokeMember(cont.DBFieldsName, BindingFlags.GetProperty, null, entity,
                                                       new object[] { });
                        if (obj == null)
                        {
                            continue;
                        }
                        Type type = obj.GetType();
                        string data = null;

                        if (typeof(SqlBoolean) == type)
                        {
                            // CheckBoxコントロール対応
                            data = obj.ToString() == DB_FLAG.TRUE.ToEntityDataTypeString() ? DB_FLAG.TRUE.ToString() : DB_FLAG.FALSE.ToString();
                        }
                        else
                        {
                            data = obj.ToString();
                        }

                        int output;
                        bool result = Int32.TryParse(data, out output);
                        if (result)
                        {
                            data = string.Format("{0:#,0}", output);
                        }

                        cont.SetResultText(Convert.ToString(data));
                    }
                }
            }
        }

        /// <summary>
        /// システム自動設定のプロパティを設定する
        /// </summary>
        /// <param name="entity">設定を行うEntity</param>
        /// <param name="isDelete">論理削除を行うかどうかのフラグ</param>
        public void SetSystemProperty(SuperEntity entity, bool isDelete)
        {
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            string computerName = SystemInformation.ComputerName;

            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //entity.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
            entity.CREATE_DATE = SqlDateTime.Parse(now.ToString());
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            entity.CREATE_USER = SystemProperty.UserName;
            entity.CREATE_PC = computerName;

            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
            entity.UPDATE_DATE = SqlDateTime.Parse(now.ToString());
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            entity.UPDATE_USER = SystemProperty.UserName;
            entity.UPDATE_PC = computerName;

            //entity.DELETE_FLG = Convert.ToInt32(isDelete ? Constans.LOGICAL_DELETE_FLAG.DELETE : Constans.LOGICAL_DELETE_FLAG.NOT_DELETE);
        }

        /// <summary>
        /// MultiRowからEntityへ値の設定を行う
        /// </summary>
        /// <param name="multiRow">multiRowの明細情報</param>
        public T[] CreateEntityForDataTable(GcMultiRow multiRow)
        {
            var entityList = new T[multiRow.RowCount - 1];

            for (int i = 0; i < multiRow.RowCount - 1; i++)
            {
                var t = Entitys[i].GetType();
                for (int j = 0; j < multiRow.Template.Row.Cells.Count; j++)
                {
                    var pi = t.GetProperty(multiRow[i, j].DataField);
                    if (pi == null)
                    {
                        continue;
                    }

                    // 削除フラグにNULLが入ってしまう場合がある為
                    // 削除フラグがNULLの場合は強制的に設定（暫定対応:TODO）
                    if (multiRow[i, j].DataField == "DELETE_FLG" && string.IsNullOrWhiteSpace(multiRow[i, j].Value.ToString()))
                    {
                        Entitys[i].SetValue(multiRow[i, j].DataField, SqlBoolean.False.ToString(), pi.PropertyType.Name);
                    }
                    else
                    {
                        Entitys[i].SetValue(multiRow[i, j].DataField, multiRow[i, j].Value.ToString(), pi.PropertyType.Name);
                    }
                }

                SetSystemProperty(Entitys[i], false);

                // 20150817 TIME_STAMP自動設定の暫定対応 Start
                if (multiRow.Rows[i].DataBoundItem != null && multiRow.Rows[i].DataBoundItem is DataRowView && (multiRow.Rows[i].DataBoundItem as DataRowView).Row != null &&
                    (multiRow.Rows[i].DataBoundItem as DataRowView).Row.Table.Columns.Contains("TIME_STAMP"))
                {
                    var timestamp = (multiRow.Rows[i].DataBoundItem as DataRowView).Row["TIME_STAMP"];
                    if (!Convert.IsDBNull(timestamp) && timestamp is byte[] && ((byte[])timestamp).Length > 0)
                    {
                        Entitys[i].TIME_STAMP = (byte[])timestamp;
                    }
                }
                // 20150817 TIME_STAMP自動設定の暫定対応 End

                entityList[i] = Entitys[i];
            }

            return entityList;
        }

        //20250411
        public T[] CreateEntityForDataTable(DataTable table)
        {
            var entityList = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                T entity = Activator.CreateInstance<T>();
                var t = entity.GetType();

                foreach (DataColumn column in table.Columns)
                {
                    var pi = t.GetProperty(column.ColumnName);
                    if (pi == null)
                    {
                        continue;
                    }

                    object value = row[column];
                    if (column.ColumnName == "DELETE_FLG" && (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString())))
                    {
                        value = SqlBoolean.False.ToString();
                    }

                    if (value != DBNull.Value)
                    {
                        entity.SetValue(column.ColumnName, value.ToString(), pi.PropertyType.Name);
                    }
                }

                SetSystemProperty(entity, false);

                if (table.Columns.Contains("TIME_STAMP"))
                {
                    var timestamp = row["TIME_STAMP"];
                    if (timestamp is byte[] ts && ts.Length > 0)
                    {
                        entity.TIME_STAMP = ts;
                    }
                }

                entityList.Add(entity);
            }

            return entityList.ToArray();
        }


        /// <summary>
        /// EntityからMultiRowへ値の設定を行う
        /// </summary>
        /// <param name="multiRow">multiRowの明細情報</param>
        /// <param name="entitys">明細のEntityの配列</param>
        public void CreateDataTableForEntity(GcMultiRow multiRow, SuperEntity[] entitys)
        {
            for (int i = 0; i < multiRow.RowCount; i++)
            {
                if (entitys.Length - 1 < i)
                {
                    // Eintityの範囲外が指定されるのを防ぐ
                    continue;
                }
                for (int j = 0; j < multiRow.Template.Row.Cells.Count; j++)
                {
                    var t = entitys[i].GetType();
                    var pi = t.GetProperty(multiRow[i, j].DataField);
                    if (pi == null)
                    {
                        continue;
                    }
                    var data = entitys[i].GetType()
                                     .InvokeMember(multiRow[i, j].DataField, BindingFlags.GetProperty, null, entitys[i],
                                                   new object[] { });

                    var control = multiRow[i, j];

                    var cont = control as ICustomControl;
                    if (cont != null && data != null)
                    {
                        bool existValue = false;
                        var property = data.GetType().GetProperty("IsNull");
                        if (property != null)
                        {
                            existValue = (bool)property.GetValue(data, null);
                        }
                        else
                        {
                            existValue = (data == null);
                        }

                        if (!existValue)
                        {
                            cont.SetResultText(Convert.ToString(data));
                        }
                    }
                    else
                    {
                        multiRow[i, j].Value = data;
                    }
                }
            }
        }

        /// <summary>
        /// EntityからDataGridViewへ値の設定を行う
        /// </summary>
        /// <param name="multiRow">DataGridViewの明細情報</param>
        /// <param name="entitys">明細のEntityの配列</param>
        public void CreateDataGridViewForEntity(CustomDataGridView gridView, SuperEntity[] entitys)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                for (int j = 0; j < gridView.Rows[i].Cells.Count; j++)
                {

                    var cell = gridView.Columns[j].CellTemplate as ICustomDataGridControl;


                    var t = entitys[i].GetType();
                    var pi = t.GetProperty(cell.DBFieldsName);
                    if (pi == null)
                    {
                        continue;
                    }
                    var data = entitys[i].GetType()
                                     .InvokeMember(cell.DBFieldsName, BindingFlags.GetProperty, null, entitys[i],
                                                   new object[] { });

                    if (cell != null)
                    {
                        cell.SetResultText(Convert.ToString(data));
                    }
                    else
                    {
                        gridView.Rows[i].Cells[j].Value = data;
                    }
                }
            }
        }

        /// <summary>
        /// DataGridViewからEntityへ値の設定を行う
        /// </summary>
        /// <param name="gridView">DataGridViewの明細情報</param>
        public T[] CreateEntityForDataGrid(CustomDataGridView gridView)
        {
            var entityList = new T[gridView.RowCount - 1];

            for (int i = 0; i < gridView.RowCount - 1; i++)
            {
                var t = Entitys[i].GetType();
                for (int j = 0; j < gridView.Rows[i].Cells.Count; j++)
                {
                    var cell = gridView.Columns[j].CellTemplate as ICustomDataGridControl;
                    var pi = t.GetProperty(cell.DBFieldsName);
                    if (pi == null)
                    {
                        continue;
                    }

                    // 削除フラグにNULLが入ってしまう場合がある為
                    // 削除フラグがNULLの場合は強制的に設定（暫定対応:TODO）
                    if (cell.DBFieldsName == "DELETE_FLG" && string.IsNullOrWhiteSpace(cell.GetResultText()))
                    {
                        Entitys[i].SetValue(cell.DBFieldsName, SqlBoolean.False.ToString(), pi.PropertyType.Name);
                    }
                    else
                    {
                        Entitys[i].SetValue(cell.DBFieldsName, cell.GetResultText(), pi.PropertyType.Name);
                    }
                }

                SetSystemProperty(Entitys[i], false);

                // 20150817 TIME_STAMP自動設定の暫定対応 Start
                if (gridView.Rows[i].DataBoundItem != null && gridView.Rows[i].DataBoundItem is DataRowView && (gridView.Rows[i].DataBoundItem as DataRowView).Row != null &&
                    (gridView.Rows[i].DataBoundItem as DataRowView).Row.Table.Columns.Contains("TIME_STAMP"))
                {
                    var timestamp = (gridView.Rows[i].DataBoundItem as DataRowView).Row["TIME_STAMP"];
                    if (!Convert.IsDBNull(timestamp) && timestamp is byte[] && ((byte[])timestamp).Length > 0)
                    {
                        Entitys[i].TIME_STAMP = (byte[])timestamp;
                    }
                }
                // 20150817 TIME_STAMP自動設定の暫定対応 End

                entityList[i] = Entitys[i];
            }

            return entityList;
        }
    }
}
