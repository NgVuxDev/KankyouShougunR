using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.OriginalException;

namespace r_framework.Utility
{
    /// <summary>
    /// SQL文生成処理
    /// </summary>
    public class SqlCreateUtility
    {
        /// <summary>
        /// Join句のSQL文を格納する変数
        /// </summary>
        public static StringBuilder joinSql { get; set; }

        /// <summary>
        /// Where句のSQLを格納する変数
        /// </summary>
        public static StringBuilder whereSql { get; set; }

        /// <summary>
        /// Where句のSQL文字列
        /// </summary>
        private static string WHERE_SQL = string.Empty;

        /// <summary>
        /// ポップアップにて利用する条件式を生成する
        /// </summary>
        /// <param name="popupDto">ポップアップのjoin句を記述したコレクション</param>
        /// <param name="controls">画面上に表示されているコントロールの配列</param>
        /// <returns></returns>
        public static string CreatePopupSql(Collection<JoinMethodDto> popupDto,WINDOW_ID strwindowid, object[] controls)
        {
            joinSql = null;
            whereSql = null;
            WHERE_SQL = string.Empty;
            if (popupDto != null)
            {
                int whereCount = 0;
                foreach (var joinDate in popupDto)
                {
                    CreateJoinStr(ref whereCount, joinDate, controls);
                    CreateWhereStr(joinDate, controls);
                }
            }
            var returnSql = string.Empty;

            if (joinSql != null)
            {
                returnSql += joinSql.ToString();
                returnSql += " ";
            }

            //createWhereSql(popupDto, strwindowid);
            if (whereSql != null)
            {
                returnSql += whereSql.ToString();
                if (!string.IsNullOrEmpty(WHERE_SQL))
                {
                    returnSql += " and " + WHERE_SQL;
                }
                returnSql += " ";
            }
            else
            {
                if (!string.IsNullOrEmpty(WHERE_SQL))
                {
                    returnSql += " WHERE " + WHERE_SQL;
                    returnSql += " ";
                }
            }

            return returnSql;
        }

        /// <summary>
        /// ポップアップにて利用する条件式を生成する
        /// </summary>
        /// <param name="popupDto">ポップアップのjoin句を記述したコレクション</param>
        /// <param name="controls">画面上に表示されているコントロールの配列</param>
        /// <returns></returns>
        public static string CreatePopupSql2(Collection<JoinMethodDto> popupDto, object[] controls)
        {
            joinSql = null;
            whereSql = new StringBuilder();
            whereSql.Append("WHERE 1 = 1 ");
            WHERE_SQL = string.Empty;
            if (popupDto != null)
            {
                // join句の追加
                foreach (var joinDate in popupDto)
                {
                    CreateJoinStr(joinDate, controls);
                }
                // where句の追加
                foreach (var joinDate in popupDto)
                {
                    CreateWhereStr2(joinDate, controls);
                }
            }
            var returnSql = string.Empty;

            if (joinSql != null)
            {
                returnSql += joinSql.ToString();
                returnSql += " ";
            }
            returnSql += whereSql.ToString();
            //有効データをチェックする
            createVariableWhereSql(popupDto);
            if (!string.IsNullOrEmpty(WHERE_SQL))
            {
                returnSql += " AND " + WHERE_SQL;
            }
            returnSql += " ";

            return returnSql;
        }

        /// <summary>
        /// Where句の生成を行う
        /// </summary>
        /// <param name="joinDate"></param>
        private static void createWhereSql(Collection<JoinMethodDto> joinDate, WINDOW_ID strwindowid)
        {
            string tableName = string.Empty;
            if (joinDate != null && joinDate.Count != 0)
            {
                tableName = joinDate[0].LeftTable + ".";
            }

            if (strwindowid == WINDOW_ID.M_KIHON_HINMEI_TANKA
                || strwindowid == WINDOW_ID.M_KOBETSU_HINMEI_TANKA
                || strwindowid == WINDOW_ID.M_TORIHIKISAKI
                || strwindowid == WINDOW_ID.M_GYOUSHA
                || strwindowid == WINDOW_ID.M_GENBA
                || strwindowid == WINDOW_ID.M_SHOUHIZEI
                || strwindowid == WINDOW_ID.M_COURSE)
            {
                SqlCreateUtility.WHERE_SQL += String.Format("CONVERT(DATE, ISNULL({0}TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL({0}TEKIYOU_END, DATEADD(day,1,GETDATE()))) AND {0}DELETE_FLG = 0", tableName);
            }
            else
            {
                SqlCreateUtility.WHERE_SQL += String.Format("{0}DELETE_FLG = 0", tableName);
            }
        }

        /// <summary>
        /// 動的にWhere句の生成を行う
        /// </summary>
        /// <param name="joinDate"></param>
        private static void createVariableWhereSql(Collection<JoinMethodDto> joinDate)
        {
            // 既にチェック済のテーブルを保存しておくリスト
            var cl = new List<string>();
            if (joinDate != null && joinDate.Count != 0)
            {
                for (int i = 0; i < joinDate.Count; i++)
                {
                    // LeftTableの有効チェック
                    if (joinDate[i].LeftTable != null && joinDate[i].IsCheckLeftTable == true)
                    {
                        // チェック済テーブルはチェックしない
                        if (!cl.Contains(joinDate[i].LeftTable))
                        {
                            createWhereSQLInChecked(joinDate[i].LeftTable);
                            cl.Add(joinDate[i].LeftTable);
                        }
                    }
                    // RightTableの有効チェック
                    if (joinDate[i].RightTable != null && joinDate[i].IsCheckRightTable == true)
                    {
                        // チェック済テーブルはチェックしない
                        if (!cl.Contains(joinDate[i].RightTable))
                        {
                            createWhereSQLInChecked(joinDate[i].RightTable);
                            cl.Add(joinDate[i].RightTable);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// カラムの有無をチェックしてからWhere句の生成を行う
        /// </summary>
        /// <param name="tableName"></param>
        private static void createWhereSQLInChecked(string tableName)
        {
            var type = Type.GetType("r_framework.Entity." + tableName);
            if (type != null)
            {
                tableName += ".";
                // 対象のテーブルに必要なカラムがあるかチェックし、SQLを追加する
                var pNames = type.GetProperties().Select(p => p.Name);
                if (pNames.Contains("TEKIYOU_BEGIN") && pNames.Contains("TEKIYOU_END"))
                {
                    createTekiyouSQL(tableName);
                }
                if (pNames.Contains("DELETE_FLG"))
                {
                    createDeleteFlgSQL(tableName);
                }
            }
        }

        /// <summary>
        /// 適用期間についてのWhere句の生成を行う
        /// </summary>
        /// <param name="tableName"></param>
        private static void createTekiyouSQL(string tableName)
        {
            if (!string.IsNullOrEmpty(WHERE_SQL))
            {
                WHERE_SQL += " AND ";
            }

            if (tableName.Contains("M_KIHON_HINMEI_TANKA") ||
                tableName.Contains("M_KOBETSU_HINMEI_TANKA") ||
                tableName.Contains("M_TORIHIKISAKI") ||
                tableName.Contains("M_GYOUSHA") ||
                tableName.Contains("M_GENBA") ||
                tableName.Contains("M_SHOUHIZEI") ||
                tableName.Contains("M_COURSE"))
            {
                SqlCreateUtility.WHERE_SQL += String.Format("CONVERT(DATE, ISNULL({0}TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL({0}TEKIYOU_END, DATEADD(day,1,GETDATE())))", tableName);
            }

        }

        /// <summary>
        /// 削除フラグについてのWhere句の生成を行う
        /// </summary>
        /// <param name="tableName"></param>
        private static void createDeleteFlgSQL(string tableName)
        {
            if (!string.IsNullOrEmpty(WHERE_SQL))
            {
                WHERE_SQL += " AND ";
            }
            WHERE_SQL += tableName + "DELETE_FLG = 0 ";
        }

        /// <summary>
        /// 検索条件を作成する
        /// 対象のコントロールが見つけれた場合については、コントロールの値とする
        /// コントロールが見つからない場合は、Valuesの値を直接設定する
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static string createValues(object[] controls, SearchConditionsDto dto)
        {
            var field = ControlUtility.CreateFields(controls, dto.Value);

            if (field[0] != null)
            {
                var control = field[0] as ICustomControl;

                if (control != null)
                {
                    return dto.ValueColumnType.ToConvertString(control.GetResultText());
                }
                throw new Exception();

            }
            return dto.ValueColumnType.ToConvertString(dto.Value.ToString());
        }

        /// <summary>
        /// JoinのSQL文を生成する
        /// </summary>
        /// <param name="whereCount">where使用回数</param>
        /// <param name="joinDate">Join設定情報クラス</param>
        /// <param name="controls">画面上に表示されているコントロール</param>
        private static void CreateJoinStr(ref int whereCount, JoinMethodDto joinDate, object[] controls)
        {
            if (joinDate.Join == JOIN_METHOD.WHERE)
            {
                return;
            }
            if (joinSql == null)
            {
                joinSql = new StringBuilder();
            }

            //Join条件設定
            joinSql.Append(" ");
            joinSql.Append(JOIN_METHODExt.ToString(joinDate.Join));
            joinSql.Append(" ");
            joinSql.Append(joinDate.LeftTable);
            joinSql.Append(" ");
            joinSql.Append(" ON ");
            joinSql.Append(joinDate.LeftTable);
            joinSql.Append(".");
            joinSql.Append(joinDate.LeftKeyColumn);
            joinSql.Append(" = ");
            joinSql.Append(joinDate.RightTable);
            joinSql.Append(".");
            joinSql.Append(joinDate.RightKeyColumn);

            foreach (var searchDate in joinDate.SearchCondition)
            {
                // 初回だけWHERE区を追加する
                if (whereCount < 1)
                {
                    joinSql.Append(" WHERE");
                }
                //検索条件設定
                if (string.IsNullOrEmpty(searchDate.Value))
                {
                    //value値がnullのため、テーブル同士のカラム結合を行う
                    joinSql.Append(" ");
                    if (0 < whereCount)
                    {
                        joinSql.Append(searchDate.And_Or.ToString());
                    }
                    joinSql.Append(" ");
                    joinSql.Append(joinDate.LeftTable);
                    joinSql.Append(".");
                    joinSql.Append(searchDate.LeftColumn);
                    var date = joinDate.RightTable + "." + searchDate.RightColumn;
                    joinSql.Append(searchDate.Condition.ToConditionString(date));
                }
                else
                {
                    var date = createValues(controls, searchDate);

                    if (!string.IsNullOrEmpty(date))
                    {
                        joinSql.Append(" ");
                        if (0 < whereCount)
                        {
                            joinSql.Append(searchDate.And_Or.ToString());
                        }
                        joinSql.Append(" ");
                        joinSql.Append(joinDate.LeftTable);
                        joinSql.Append(".");
                        joinSql.Append(searchDate.LeftColumn);
                        joinSql.Append(searchDate.Condition.ToConditionString(date));
                    }
                }

                whereCount++;
            }
        }

        /// <summary>
        /// Where句を生成する
        /// </summary>
        /// <param name="joinDate">Join設定情報クラス</param>
        /// <param name="controls">画面上に表示されているコントロール</param>
        private static void CreateWhereStr(JoinMethodDto joinDate, object[] controls)
        {
            if (joinDate.Join != JOIN_METHOD.WHERE)
            {
                return;
            }
            foreach (var searchDate in joinDate.SearchCondition)
            {
                if ("TEKIYOU_FLG".Equals(searchDate.LeftColumn) && !string.IsNullOrEmpty(searchDate.Value))
                {
                    if ("TRUE".Equals(searchDate.Value.ToUpper()))
                    {
                        if (whereSql == null)
                        {
                            whereSql = new StringBuilder();
                            whereSql.Append("WHERE 1 = 1 ");
                        }
                        string tekiyouSql = " {0} (({1}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {1}.TEKIYOU_END) OR ({1}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND {1}.TEKIYOU_END IS NULL) OR ({1}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {1}.TEKIYOU_END) OR ({1}.TEKIYOU_BEGIN IS NULL AND {1}.TEKIYOU_END IS NULL)) AND {1}.DELETE_FLG = 0  ";
                        whereSql.AppendFormat(tekiyouSql, searchDate.And_Or.ToString(), joinDate.LeftTable);
                    }
                    else if ("FALSE".Equals(searchDate.Value.ToUpper()))
                    {
                        if (whereSql == null)
                        {
                            whereSql = new StringBuilder();
                            whereSql.Append("WHERE 1 = 1 ");
                        }
                        string tekiyouSql = " {0} {1}.DELETE_FLG = 0 ";
                        whereSql.AppendFormat(tekiyouSql, searchDate.And_Or.ToString(), joinDate.LeftTable);
                    }
                    continue;
                }
                var date = createValues(controls, searchDate);

                if (!string.IsNullOrEmpty(date))
                {
                    if (whereSql == null)
                    {
                        whereSql = new StringBuilder();

                        //where句を追加
                        whereSql.Append(" ");
                        whereSql.Append(joinDate.Join.ToString());
                        whereSql.Append(" ");
                    }
                    else
                    {
                        whereSql.Append(" ");
                        whereSql.Append(searchDate.And_Or.ToString());
                        whereSql.Append(" ");
                    }
                    whereSql.Append(joinDate.LeftTable);
                    whereSql.Append(".");
                    whereSql.Append(searchDate.LeftColumn);
                    whereSql.Append(searchDate.Condition.ToConditionString(date));
                }
            }
        }

        private static void CreateJoinStr(JoinMethodDto joinDate, object[] controls)
        {
            if (joinDate.Join == JOIN_METHOD.WHERE)
            {
                return;
            }
            if (joinSql == null)
            {
                joinSql = new StringBuilder();
            }

            //Join条件設定
            joinSql.Append(" ");
            joinSql.Append(JOIN_METHODExt.ToString(joinDate.Join));
            joinSql.Append(" ");
            joinSql.Append(joinDate.LeftTable);
            joinSql.Append(" ");
            joinSql.Append(" ON ");
            joinSql.Append(joinDate.LeftTable);
            joinSql.Append(".");
            joinSql.Append(joinDate.LeftKeyColumn);
            joinSql.Append(" = ");
            joinSql.Append(joinDate.RightTable);
            joinSql.Append(".");
            joinSql.Append(joinDate.RightKeyColumn);
        }

        private static void CreateWhereStr2(JoinMethodDto joinDate, object[] controls)
        {
            if (joinDate.Join != JOIN_METHOD.WHERE)
            {
                return;
            }
            foreach (var searchDate in joinDate.SearchCondition)
            {
                if ("TEKIYOU_FLG".Equals(searchDate.LeftColumn) && !string.IsNullOrEmpty(searchDate.Value))
                {
                    if ("TRUE".Equals(searchDate.Value.ToUpper()))
                    {
                        string tekiyouSql = " {0} (({1}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {1}.TEKIYOU_END) OR ({1}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND {1}.TEKIYOU_END IS NULL) OR ({1}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {1}.TEKIYOU_END) OR ({1}.TEKIYOU_BEGIN IS NULL AND {1}.TEKIYOU_END IS NULL)) AND {1}.DELETE_FLG = 0  ";
                        whereSql.AppendFormat(tekiyouSql, searchDate.And_Or.ToString(), joinDate.LeftTable);
                    }
                    else if ("FALSE".Equals(searchDate.Value.ToUpper()))
                    {
                        string tekiyouSql = " {0} {1}.DELETE_FLG = 0 ";
                        whereSql.AppendFormat(tekiyouSql, searchDate.And_Or.ToString(), joinDate.LeftTable);
                    }
                    continue;
                }
                //検索条件設定
                if (string.IsNullOrEmpty(searchDate.Value))
                {
                    whereSql.Append(" ");
                    whereSql.Append(searchDate.And_Or.ToString());
                    whereSql.Append(" ");
                    //value値がnullのため、テーブル同士のカラム結合を行う
                    whereSql.Append(" ");
                    whereSql.Append(joinDate.LeftTable);
                    whereSql.Append(".");
                    whereSql.Append(searchDate.LeftColumn);
                    var date = joinDate.RightTable + "." + searchDate.RightColumn;
                    whereSql.Append(searchDate.Condition.ToConditionString(date));
                }
                else
                {
                    var date = createValues(controls, searchDate);

                    if (!string.IsNullOrEmpty(date))
                    {
                        whereSql.Append(" ");
                        whereSql.Append(searchDate.And_Or.ToString());
                        whereSql.Append(" ");
                        whereSql.Append(joinDate.LeftTable);
                        whereSql.Append(".");
                        whereSql.Append(searchDate.LeftColumn);
                        whereSql.Append(searchDate.Condition.ToConditionString(date));
                    }
                }
            }
        }

        /// <summary>
        /// Where句の文字列に対してエスケープシーケンス対策を行う(like用)
        /// </summary>
        /// <param name="str">検索文字列</param>
        public static string CounterplanEscapeSequence(string str)
        {
            return str.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]").Replace("'", "''");
        }

        /// <summary>
        /// Where句の文字列に対してエスケープシーケンス対策を行う(=用)
        /// </summary>
        /// <param name="str">検索文字列</param>
        public static string CounterplanEscapeSequence2(string str)
        {
            return str.Replace("'", "''");
        }
    }
}
