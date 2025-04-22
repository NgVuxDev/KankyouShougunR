using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Dao;
using r_framework.Utility;

namespace Shougun.Core.PayByProxy.DainoMeisaihyoOutput
{
    #region - Class -

    /// <summary> R403(代納明細表)帳票を表すクラス・コントロール </summary>
    public class ReportInfoR403 : ReportInfoBase
    {
        #region - Fields -

        private string FH_JISHA_NAME;
        private string FH_KYOTEN_NAME;
        private string FH_DATE_FR;
        private string FH_DATE_TO;
        private string FH_U_TORI_CD_FR;
        private string FH_U_TORI_NM_FR;
        private string FH_U_TORI_CD_TO;
        private string FH_U_TORI_NM_TO;
        private string FH_U_GYOUSHA_CD_FR;
        private string FH_U_GYOUSHA_NM_FR;
        private string FH_U_GYOUSHA_CD_TO;
        private string FH_U_GYOUSHA_NM_TO;
        private string FH_U_GENBA_CD_FR;
        private string FH_U_GENBA_NM_FR;
        private string FH_U_GENBA_CD_TO;
        private string FH_U_GENBA_NM_TO;
        private string FH_S_TORI_CD_FR;
        private string FH_S_TORI_NM_FR;
        private string FH_S_TORI_CD_TO;
        private string FH_S_TORI_NM_TO;
        private string FH_S_GYOUSHA_CD_FR;
        private string FH_S_GYOUSHA_NM_FR;
        private string FH_S_GYOUSHA_CD_TO;
        private string FH_S_GYOUSHA_NM_TO;
        private string FH_S_GENBA_CD_FR;
        private string FH_S_GENBA_NM_FR;
        private string FH_S_GENBA_CD_TO;
        private string FH_S_GENBA_NM_TO;

        #endregion

        #region - Constructors -

        /// <summary> Initializes a new instance of the <see cref="ReportInfoR403" /> class. </summary>
        public ReportInfoR403()
        {
            // パスとレイアウトのデフォルト値を設定しておく

            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = "./Template/R403-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";
        }

        #endregion

        #region - properties -
        /// <summary>帳票出力フルパスフォーム名を保持するフィールド</summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>帳票出力フォームレイアウト名を保持するフィールド</summary>
        public string OutputFormLayout { get; set; }
        #endregion

        #region - Methods -

        /// <summary> C1Reportの帳票データの作成を実行する </summary>
        /// <param name="chouhyouData">chouhyouData</param>
        /// <param name="nyuukinData">nyuukinData</param>
        public void R403_Report(DataTable data, DataTable headData)
        {
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(headData);
            Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
            dic.Add("LAYOUT1", data);
            // データテーブル情報から帳票情報作成処理を実行する
            this.Create(this.OutputFormFullPathName, this.OutputFormLayout, dic);
        }

        /// <summary> 文字列の帳票データを文字列配列データへの変換を実行する </summary>
        /// <param name="tmp">帳票データを表す文字列</param>
        /// <returns>文字列配列の帳票データ</returns>
        private string[] ReportSplit(string tmp)
        {
            // 値が空の項目を半角スペースに置き換える(""⇒" ")
            tmp = tmp.Replace("\"\"", "\" \"");

            // 先頭と末尾の"(ダブルコーテーション)を削除する
            // 先頭と末尾の空白を削除
            tmp = tmp.Trim();

            // 先頭と末尾以外を抽出(先頭と、末尾は"(ダブルコーテーション))
            tmp = tmp.Substring(1, tmp.Length - 2);

            // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
            string[] splt = { "\",\"" };
            string[] ret = tmp.Split(splt, StringSplitOptions.RemoveEmptyEntries);

            return ret;
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            this.SetFieldName("FH_JISHA_NAME", this.FH_JISHA_NAME);
            this.SetFieldName("FH_KYOTEN_NAME", this.FH_KYOTEN_NAME);
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //this.SetFieldName("FH_OUTPUT_DATE", DateTime.Now.ToString("yyyy/M/dd H:mm:ss") + "発行");
            this.SetFieldName("FH_OUTPUT_DATE", this.getDBDateTime().ToString("yyyy/MM/dd H:mm:ss") + "発行");
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.SetFieldName("FH_DATE_FR",this.FH_DATE_FR);
            this.SetFieldName("FH_DATE_TO", this.FH_DATE_TO);
            this.SetFieldName("FH_U_TORI_CD_FR", this.FH_U_TORI_CD_FR);
            this.SetFieldName("FH_U_TORI_NM_FR", this.FH_U_TORI_NM_FR);
            this.SetFieldName("FH_U_TORI_CD_TO", this.FH_U_TORI_CD_TO);
            this.SetFieldName("FH_U_TORI_NM_TO", this.FH_U_TORI_NM_TO);
            this.SetFieldName("FH_U_GYOUSHA_CD_FR", this.FH_U_GYOUSHA_CD_FR);
            this.SetFieldName("FH_U_GYOUSHA_NM_FR", this.FH_U_GYOUSHA_NM_FR);
            this.SetFieldName("FH_U_GYOUSHA_CD_TO", this.FH_U_GYOUSHA_CD_TO);
            this.SetFieldName("FH_U_GYOUSHA_NM_TO", this.FH_U_GYOUSHA_NM_TO);
            this.SetFieldName("FH_U_GENBA_CD_FR", this.FH_U_GENBA_CD_FR);
            this.SetFieldName("FH_U_GENBA_NM_FR", this.FH_U_GENBA_NM_FR);
            this.SetFieldName("FH_U_GENBA_CD_TO", this.FH_U_GENBA_CD_TO);
            this.SetFieldName("FH_U_GENBA_NM_TO", this.FH_U_GENBA_NM_TO);
            this.SetFieldName("FH_S_TORI_CD_FR", this.FH_S_TORI_CD_FR);
            this.SetFieldName("FH_S_TORI_NM_FR", this.FH_S_TORI_NM_FR);
            this.SetFieldName("FH_S_TORI_CD_TO", this.FH_S_TORI_CD_TO);
            this.SetFieldName("FH_S_TORI_NM_TO", this.FH_S_TORI_NM_TO);
            this.SetFieldName("FH_S_GYOUSHA_CD_FR", this.FH_S_GYOUSHA_CD_FR);
            this.SetFieldName("FH_S_GYOUSHA_NM_FR", this.FH_S_GYOUSHA_NM_FR);
            this.SetFieldName("FH_S_GYOUSHA_CD_TO", this.FH_S_GYOUSHA_CD_TO);
            this.SetFieldName("FH_S_GYOUSHA_NM_TO", this.FH_S_GYOUSHA_NM_TO);
            this.SetFieldName("FH_S_GENBA_CD_FR", this.FH_S_GENBA_CD_FR);
            this.SetFieldName("FH_S_GENBA_NM_FR", this.FH_S_GENBA_NM_FR);
            this.SetFieldName("FH_S_GENBA_CD_TO", this.FH_S_GENBA_CD_TO);
            this.SetFieldName("FH_S_GENBA_NM_TO", this.FH_S_GENBA_NM_TO);
        }

        /// <summary> 帳票データより、C1Reportに渡すデータを作成する </summary>
        /// <param name="dataTable">帳票データ</param>
        private void InputDataToMem(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                this.FH_JISHA_NAME = Convert.ToString(dataTable.Rows[0]["FH_JISHA_NAME"]);
                this.FH_KYOTEN_NAME = Convert.ToString(dataTable.Rows[0]["FH_KYOTEN_NAME"]);
                this.FH_DATE_FR = Convert.ToString(dataTable.Rows[0]["FH_DATE_FR"]);
                this.FH_DATE_TO = Convert.ToString(dataTable.Rows[0]["FH_DATE_TO"]);
                this.FH_U_TORI_CD_FR = Convert.ToString(dataTable.Rows[0]["FH_U_TORI_CD_FR"]);
                this.FH_U_TORI_NM_FR = Convert.ToString(dataTable.Rows[0]["FH_U_TORI_NM_FR"]);
                this.FH_U_TORI_CD_TO = Convert.ToString(dataTable.Rows[0]["FH_U_TORI_CD_TO"]);
                this.FH_U_TORI_NM_TO = Convert.ToString(dataTable.Rows[0]["FH_U_TORI_NM_TO"]);
                this.FH_U_GYOUSHA_CD_FR = Convert.ToString(dataTable.Rows[0]["FH_U_GYOUSHA_CD_FR"]);
                this.FH_U_GYOUSHA_NM_FR = Convert.ToString(dataTable.Rows[0]["FH_U_GYOUSHA_NM_FR"]);
                this.FH_U_GYOUSHA_CD_TO = Convert.ToString(dataTable.Rows[0]["FH_U_GYOUSHA_CD_TO"]);
                this.FH_U_GYOUSHA_NM_TO = Convert.ToString(dataTable.Rows[0]["FH_U_GYOUSHA_NM_TO"]);
                this.FH_U_GENBA_CD_FR = Convert.ToString(dataTable.Rows[0]["FH_U_GENBA_CD_FR"]);
                this.FH_U_GENBA_NM_FR = Convert.ToString(dataTable.Rows[0]["FH_U_GENBA_NM_FR"]);
                this.FH_U_GENBA_CD_TO = Convert.ToString(dataTable.Rows[0]["FH_U_GENBA_CD_TO"]);
                this.FH_U_GENBA_NM_TO = Convert.ToString(dataTable.Rows[0]["FH_U_GENBA_NM_TO"]);
                this.FH_S_TORI_CD_FR = Convert.ToString(dataTable.Rows[0]["FH_S_TORI_CD_FR"]);
                this.FH_S_TORI_NM_FR = Convert.ToString(dataTable.Rows[0]["FH_S_TORI_NM_FR"]);
                this.FH_S_TORI_CD_TO = Convert.ToString(dataTable.Rows[0]["FH_S_TORI_CD_TO"]);
                this.FH_S_TORI_NM_TO = Convert.ToString(dataTable.Rows[0]["FH_S_TORI_NM_TO"]);
                this.FH_S_GYOUSHA_CD_FR = Convert.ToString(dataTable.Rows[0]["FH_S_GYOUSHA_CD_FR"]);
                this.FH_S_GYOUSHA_NM_FR = Convert.ToString(dataTable.Rows[0]["FH_S_GYOUSHA_NM_FR"]);
                this.FH_S_GYOUSHA_CD_TO = Convert.ToString(dataTable.Rows[0]["FH_S_GYOUSHA_CD_TO"]);
                this.FH_S_GYOUSHA_NM_TO = Convert.ToString(dataTable.Rows[0]["FH_S_GYOUSHA_NM_TO"]);
                this.FH_S_GENBA_CD_FR = Convert.ToString(dataTable.Rows[0]["FH_S_GENBA_CD_FR"]);
                this.FH_S_GENBA_NM_FR = Convert.ToString(dataTable.Rows[0]["FH_S_GENBA_NM_FR"]);
                this.FH_S_GENBA_CD_TO = Convert.ToString(dataTable.Rows[0]["FH_S_GENBA_CD_TO"]);
                this.FH_S_GENBA_NM_TO = Convert.ToString(dataTable.Rows[0]["FH_S_GENBA_NM_TO"]);
            }
        }
        #endregion
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }

    #endregion
}
