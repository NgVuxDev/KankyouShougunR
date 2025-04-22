using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using C1.C1Report;

namespace CommonChouhyouPopup.App
{
    /// <summary>レコードを表すクラス・コントロール</summary>
    internal class Record : IC1ReportRecordset
    {
        #region - Fields -

        /// <summary>カーソル位置を保持するフィールド</summary>
        private int position = 0;

        /// <summary>レコード情報を保持するフィールド</summary>
        private ArrayList arrayListRecord = new ArrayList();

        /// <summary>データーテーブルを保持するフィールド</summary>
        private DataTable dataTable = null;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="Record" /> class.</summary>
        /// <param name="dataTable">データーテーブル</param>
        public Record(DataTable dataTable)
        {
            this.dataTable = dataTable;

            // フィールド名
            this.FieldNames = new ArrayList();

            // フィールドタイプ
            this.FieldTypes = new ArrayList();
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>フィールド名を保持するプロパティ</summary>
        public ArrayList FieldNames { get; internal set; }

        /// <summary>フィールドタイプを保持するプロパティ</summary>
        public ArrayList FieldTypes { get; internal set; }

        #endregion - Properties -

        #region - Methods -

        #region - ComponentOne固有 -

        /// <summary>フィルタ文字列を適用します。詳細については、<see cref="P:System.Data.DataView.RowFilter" /> プロパティを参照してください。</summary>
        /// <param name="filter">フィルタ文字列。</param>
        void IC1ReportRecordset.ApplyFilter(string filter)
        {
        }

        /// <summary>ソート文字列を適用します。詳細については、<see cref="P:System.Data.DataView.Sort" /> プロパティを参照してください。</summary>
        /// <param name="sort">ソート文字列。</param>
        void IC1ReportRecordset.ApplySort(string sort)
        {
        }

        /// <summary>カーソルがデータソースの最初のレコードの位置にある否か</summary>
        /// <returns>カーソルがデータソースの最初のレコードの位置にあるか否か場合は true を返します。</returns>
        bool IC1ReportRecordset.BOF()
        {
            return this.position == 0;
        }

        /// <summary>カーソルがデータソースの最後のレコードを過ぎた位置にあるか否か</summary>
        /// <returns>カーソルがデータソースの最後のレコードを過ぎた位置にある場合は true を返します。</returns>
        bool IC1ReportRecordset.EOF()
        {
            return this.position >= this.arrayListRecord.Count;
        }

        /// <summary>現在のカーソル位置を返します。</summary>
        /// <returns>
        /// 現在のレコードのインデックス。
        /// </returns>
        int IC1ReportRecordset.GetBookmark()
        {
            return this.position;
        }

        /// <summary>データソース内の各フィールドの名前を表す文字列ベクターを取得します。</summary>
        /// <returns>
        /// データソース内の各フィールドの名前を表す文字列ベクター。
        /// </returns>
        string[] IC1ReportRecordset.GetFieldNames()
        {
            if (this.FieldNames == null)
            {
                return null;
            }

            return (string[])this.FieldNames.ToArray(typeof(string));
        }

        /// <summary>データソース内の各フィールドのタイプを表すベクターを取得します。</summary>
        /// <returns>
        /// データソース内の各フィールドのタイプを表すベクター。
        /// </returns>
        Type[] IC1ReportRecordset.GetFieldTypes()
        {
            if (this.FieldTypes == null)
            {
                return null;
            }

            return (Type[])this.FieldTypes.ToArray(typeof(Type));
        }

        /// <summary>カーソル位置にある特定のフィールドの値を取得します。</summary>
        /// <param name="fieldIndex">フィールドのインデックス。</param>
        /// <returns>フィールドの値。</returns>
        /// <remarks>
        /// カーソルがデータの終わり（EOF 条件）を過ぎると、このメソッドは null を返し、
        /// 例外をスローしません。
        /// </remarks>
        object IC1ReportRecordset.GetFieldValue(int fieldIndex)
        {
            if (this.position >= this.arrayListRecord.Count)
            {
                return null;
            }

            // リスト中の現在選択されている位置を取得します。
            string fields = this.arrayListRecord[this.position].ToString();

            string[] fieldsArray = fields.Split('\t');

            // ファイル情報を生成して戻り値として返します。
            return fieldsArray[fieldIndex];
        }

        /// <summary>カーソルをデータソースの最初のレコードに移動します。</summary>
        void IC1ReportRecordset.MoveFirst()
        {
            this.position = 0;
        }

        /// <summary>カーソルをデータソースの最後のレコードに移動します。</summary>
        void IC1ReportRecordset.MoveLast()
        {
            this.position = this.arrayListRecord.Count - 1;
        }

        /// <summary>カーソルをデータソースの次のレコードに移動します。</summary>
        void IC1ReportRecordset.MoveNext()
        {
            if (this.position < this.arrayListRecord.Count)
            {
                this.position++;
            }
        }

        /// <summary>カーソルをデータソースの前のレコードに移動します。</summary>
        void IC1ReportRecordset.MovePrevious()
        {
            if (this.position > 0)
            {
                this.position--;
            }
        }

        /// <summary>現在のカーソル位置を指定された値に設定します。</summary>
        /// <param name="bkmk">レコードのインデックス。</param>
        void IC1ReportRecordset.SetBookmark(int bkmk)
        {
            this.position = bkmk;
        }

        #endregion - ComponentOne固有 -

        /// <summary>データテーブルから表示用レコード情報作成処理を実行する</summary>
        public void CreateRecordInfo()
        {
            if (this.dataTable == null)
            {
                return;
            }

            int count = this.dataTable.Columns.Count;
            if (count == 0)
            {
                return;
            }

            string fieldName;

            this.arrayListRecord.Clear();
            foreach (DataColumn column in this.dataTable.Columns)
            {
                fieldName = column.Caption;

                this.FieldNames.Add(fieldName);
                this.FieldTypes.Add("a".GetType());
            }

            int i = 0;
            foreach (DataRow row in this.dataTable.Rows)
            {
                string record = row.ItemArray[0].ToString();

                for (i = 1; i < count; i++)
                {
                    record += "\t" + row.ItemArray[i].ToString();
                }

                this.arrayListRecord.Add(record);
            }
        }

        #endregion - Methods -
    }
}
