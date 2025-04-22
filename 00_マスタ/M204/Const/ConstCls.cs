// $Id: ConstCls.cs 14774 2014-01-22 04:56:51Z sys_dev_23 $

using System.Data;
using System;
namespace Shougun.Core.Master.ContenaShuruiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>M_CONTENA_SHURUIのCONTENA_SHURUI_CD</summary>
        public static readonly string CONTENA_SHURUI_CD = "CONTENA_SHURUI_CD";

        /// <summary>M_CONTENA_SHURUIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_CONTENA_SHURUIのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public class ExceptionErrMsg
        {
            public const string HAITA = "排他エラーが発生しました。";
            public const string REIGAI = "例外エラーが発生しました。";
        }

        //#region グリッドカラム名定義
        //internal const string DEF_COLNAME_CHB_DELETE_FLG = "chb_delete";  // DELETE_FLG データとバインドさせるカラム
        //internal const string DEF_COLNAME_CONTENA_SHURUI_CD = "CONTENA_SHURUI_CD";
        //internal const string DEF_COLNAME_CONTENA_SHURUI_NAME = "CONTENA_SHURUI_NAME";
        //internal const string DEF_COLNAME_CONTENA_SHURUI_NAME_RYAKU = "CONTENA_SHURUI_NAME_RYAKU";
        //internal const string DEF_COLNAME_CONTENA_SHURUI_FURIGANA = "CONTENA_SHURUI_FURIGANA";
        //internal const string DEF_COLNAME_CONTENA_SHURUI_BIKOU = "CONTENA_SHURUI_BIKOU";
        //internal const string DEF_COLNAME_TEKIYOU_BEGIN = "TEKIYOU_BEGIN";
        //internal const string DEF_COLNAME_TEKIYOU_END = "TEKIYOU_END";
        //internal const string DEF_COLNAME_UPDATE_USER = "UPDATE_USER";
        //internal const string DEF_COLNAME_UPDATE_DATE = "UPDATE_DATE";
        //internal const string DEF_COLNAME_CREATE_USER = "CREATE_USER";
        //internal const string DEF_COLNAME_CREATE_DATE = "CREATE_DATE";
        //internal const string DEF_COLNAME_CREATE_PC = "CREATE_PC";
        //internal const string DEF_COLNAME_DELETE_FLG = "DELETE_FLG";
        //internal const string DEF_COLNAME_UPDATE_PC = "UPDATE_PC";
        //internal const string DEF_COLNAME_TIME_STAMP = "TIME_STAMP";
        //#endregion

        //#region グリッドカラム順と同じDataTableを作成
        ///// <summary>
        ///// グリッドカラムと同じDataTable作成(カラムによりデータ型が変わる場合)
        ///// </summary>
        ///// <param name="dataTbl"></param>
        ///// <returns></returns>
        //internal static DataTable CreateGridDataTable()
        //{
        //    DataTable tbl = new DataTable();

        //    // グリッド表示順に登録すること
        //    //tbl.Columns.Add(DEF_COLNAME_CHB_DELETE_FLG, typeof(bool));   DELETE_FLGとバインドさせているためCHB_DELETE_FLGは不要
        //    tbl.Columns.Add(DEF_COLNAME_CONTENA_SHURUI_CD, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_CONTENA_SHURUI_NAME, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_CONTENA_SHURUI_NAME_RYAKU, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_CONTENA_SHURUI_FURIGANA, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_CONTENA_SHURUI_BIKOU, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_TEKIYOU_BEGIN, typeof(DateTime));
        //    tbl.Columns.Add(DEF_COLNAME_TEKIYOU_END, typeof(DateTime));
        //    tbl.Columns.Add(DEF_COLNAME_UPDATE_USER, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_UPDATE_DATE, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_CREATE_USER, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_CREATE_DATE, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_CREATE_PC, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_DELETE_FLG, typeof(bool));
        //    tbl.Columns.Add(DEF_COLNAME_UPDATE_PC, typeof(string));
        //    tbl.Columns.Add(DEF_COLNAME_TIME_STAMP, typeof(byte));

        //    // 新規行時は入力不可
        //    foreach (DataColumn col in tbl.Columns)
        //    {
        //        col.ReadOnly = true;
        //    }

        //    return tbl;
        //}
        //#endregion

        //#region SQL実行結果をグリッド用データテーブルへ以降
        ///// <summary>
        ///// SQL実行結果をGridView表示順に合わせたDataTableへ変換する
        ///// </summary>
        ///// <param name="tbl"></param>
        ///// <returns></returns>
        //internal static DataTable ConvertGridViewDataTbl(DataTable tbl)
        //{
        //    // グリッドと同じカラム順のDataTableを作成
        //    DataTable gridTbl = CreateGridDataTable();

        //    foreach (DataRow row in tbl.Rows)
        //    {
        //        // 列データのスライド
        //        DataRow addRow = gridTbl.NewRow();
        //        // addRow[DEF_COLNAME_CHB_DELETE_FLG] = false;  DELETE_FLGとバインドさせているためCHB_DELETE_FLGは不要
        //        addRow[DEF_COLNAME_CONTENA_SHURUI_CD] = row[DEF_COLNAME_CONTENA_SHURUI_CD];
        //        addRow[DEF_COLNAME_CONTENA_SHURUI_NAME] = row[DEF_COLNAME_CONTENA_SHURUI_NAME];
        //        addRow[DEF_COLNAME_CONTENA_SHURUI_NAME_RYAKU] = row[DEF_COLNAME_CONTENA_SHURUI_NAME_RYAKU];
        //        addRow[DEF_COLNAME_CONTENA_SHURUI_FURIGANA] = row[DEF_COLNAME_CONTENA_SHURUI_FURIGANA];
        //        addRow[DEF_COLNAME_CONTENA_SHURUI_BIKOU] = row[DEF_COLNAME_CONTENA_SHURUI_BIKOU];
        //        addRow[DEF_COLNAME_TEKIYOU_BEGIN] = row[DEF_COLNAME_TEKIYOU_BEGIN];
        //        addRow[DEF_COLNAME_TEKIYOU_END] = row[DEF_COLNAME_TEKIYOU_END];
        //        addRow[DEF_COLNAME_UPDATE_USER] = row[DEF_COLNAME_UPDATE_USER];
        //        addRow[DEF_COLNAME_UPDATE_DATE] = row[DEF_COLNAME_UPDATE_DATE];
        //        addRow[DEF_COLNAME_CREATE_USER] = row[DEF_COLNAME_CREATE_USER];
        //        addRow[DEF_COLNAME_CREATE_DATE] = row[DEF_COLNAME_CREATE_DATE];
        //        addRow[DEF_COLNAME_CREATE_PC] = row[DEF_COLNAME_CREATE_PC];
        //        addRow[DEF_COLNAME_DELETE_FLG] = row[DEF_COLNAME_DELETE_FLG];
        //        addRow[DEF_COLNAME_UPDATE_PC] = row[DEF_COLNAME_UPDATE_PC];
        //        addRow[DEF_COLNAME_TIME_STAMP] = row[DEF_COLNAME_TIME_STAMP];

        //        // 列データ登録
        //        gridTbl.Rows.Add(addRow);
        //    }

        //    return gridTbl;
        //}
        //#endregion
    }
}
