using System;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.Dto;
using r_framework.Const;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using r_framework.OriginalException;
using System.Data;
using r_framework.Logic;
using System.Reflection;
using System.IO;


namespace r_framework.MasterAccess
{
    public enum CNNECTOR_SIGNS
    {
        EQUALS = 0,
        IN = 1
    }
    /// <summary>
    /// CNNECTOR_SIGNSを文字列に変換する
    /// </summary>
    public static class CNNECTOR_SIGNSExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToTypeString(CNNECTOR_SIGNS e)
        {
            switch (e)
            {
                case CNNECTOR_SIGNS.EQUALS:
                    return "=";

                case CNNECTOR_SIGNS.IN:
                    return "IN";
            }
            return String.Empty;
        }
    }
    /// <summary>
    /// 取引先マスタをチェックするクラス
    /// </summary>
    [Obsolete("処理共通化のため廃止。このクラスには削除不ラブを見ていないバグもある。Implにあるクラスを使ってください。",true)]
    public class PopupSettingMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// 取引先マスタのDao
        /// </summary>
        private IS2Dao Dao;

        /// <summary>
        /// Entity
        /// </summary>
        public SuperEntity Entity { get; set; }
        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }
        /// <summary>
        /// メッセージユーティリティ
        /// </summary>
        private MessageUtility Message { get; set; }

        public object[] Param { get; set; }

        public object[] SendParam { get; set; }

        //Ten khoa chinh cua bang
        public string KeyName;
        //Gia tri khoa chinh cua bang
        public object KeyValue;
        //Ten bang
        public string TableName;
        //Sql select
        public string SelectStr;
        //Sql Join
        public string JoinStr;
        //Sql Where
        public string WhereStr;

        public int depthCnt;
        //Table Result
        public DataTable EntityTable;
        /// <summary>
        /// CDのMax桁数
        /// </summary>
        public readonly int CdMaxLength = 6;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PopupSettingMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            Dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
        }

        /// <summary>
        /// Ham chinh duoc goi tu phuong thuc khai bao cho FocusOutCheckMethod
        /// </summary>
        public string CheckValidDataByPopupSetting()
        {
            //Reset cac Control thuoc property SetFormField
            this.SettingFieldInit();
           
            //Kiem tra Control co gia tri moi xu ly
            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            string errorMessage = string.Empty;
            if (CheckRequireControl(ref errorMessage))
            {
            //Goi ham Check du lieu , neu co du lieu thi set vao Control tren man hinh, khong co thi thong bao loi
            var checkResultFlag = this.CheckValidData();
            if (checkResultFlag)
            {
                this.CodeDataSetting();
            }
            else
            {
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("E020").MESSAGE;
                string displayName = this.CheckControl.DisplayItemName.Replace("CD","");
                errorMessage = String.Format(errorMessage, displayName);
            }
            }
            //Tra ve thong bao loi
            return errorMessage;
        }

        /// <summary>
        /// Ham reset cac Control thuoc property SetFormField
        /// </summary>
        public void SettingFieldInit()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            controlUtil.InitCheckDateField();
        }

        /// <summary>
        /// Ham chinh thuc hien lay du lieu tuong ung voi cac Propery cua Control
        /// </summary>
        public bool CheckValidData()
        {
            //Kiem tra khai bao thuoc tinh cho Control hop le
            if (!CheckValidSearchString())
            {
                return false;
            }
            //Khoi tao cac gia tri ban dau
            this.SetInitSearchString();

            //Khoi tao SQL tuong ung 
            this.SetSearchString();

            //Gan ket cac SQL
            string sql = this.SelectStr + this.JoinStr + this.WhereStr;

            int idx = sql.IndexOf("SELECT ", StringComparison.InvariantCultureIgnoreCase);
            int idxDis = sql.IndexOf("DISTINCT ", StringComparison.InvariantCultureIgnoreCase);

            if (idx > -1 && idxDis < 0)
            {
                var newsql = sql.Substring(0, idx) + "SELECT DISTINCT " + sql.Substring(idx + 7);
                sql = newsql; //ステップ実行用に変数を経由
            }

            //Lay du lieu tuong ung
            this.EntityTable = this.Dao.GetDateForStringSql(sql);

            //Ton tai du lieu neu co ket qua va tuong ung voi 1 dong du lieu duy nhat
            return EntityTable != null && EntityTable.Rows.Count == 1;
        }
        /// <summary>
        ///Ham gan du lieu duong tim thay vao Control tuong ung cua property SetFormField
        /// </summary>
        public virtual void CodeDataSetting()
        {

            // 取得フィールドがない場合、処理を中止する
            if (string.IsNullOrEmpty(CheckControl.GetCodeMasterField))
            {
                return;
            }
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);

            var getFieldNames = this.CheckControl.GetCodeMasterField.Split(',').Select(s => s.Trim().ToUpper()).ToArray();

            for (int i = 0; i < getFieldNames.Length; i++)
            {
                var control = setField[i] as ICustomControl;
                object obj = EntityTable.Rows[0][getFieldNames[i]];

                if (obj != null && control != null)
                {
                    control.SetResultText(obj.ToString());

                    // ゼロ埋め処理
                    ICustomTextBox textBox = this.CheckControl as ICustomTextBox;
                    if (textBox.ZeroPaddengFlag)
                    {
                        var textLogic = new CustomTextBoxLogic(textBox);
                        textLogic.ZeroPadding(this.CheckControl);
                    }
                }

            }
        }

      


        #region Utility

        /// <summary>
        /// Ham kiem tra khai bao cac thuoc tinh cua Control hop le
        /// </summary>
        private bool CheckValidSearchString()
        {
            bool valid = true;
            switch (this.CheckControl.PopupWindowId)
            {
                //Kiem tra Validating Genba phai duoc gan voi Control Gyousha
                case WINDOW_ID.M_GENBA:
                case WINDOW_ID.M_GENBA_CLOSED:
                    valid = false;
                    if (this.CheckControl.PopupSearchSendParams != null)
                    {
                        foreach (var dto in this.CheckControl.PopupSearchSendParams)
                        {
                            if (dto.KeyName == "GYOUSHA_CD")
                            {
                                if (String.IsNullOrEmpty(dto.Control))
                                {
                                    if (!String.IsNullOrEmpty(dto.Value))
                                    {
                                        valid = true;
                                    }
                                }
                                else
                                {
                                    object[] control = new ControlUtility().FindControl(this.Param, new string[] { dto.Control });
                                    string controlText = PropertyUtility.GetTextOrValue(control[0]);
                                    if (!String.IsNullOrEmpty(controlText))
                                    {
                                        valid = true;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    break;
            }
            return valid;
        }

        /// <summary>
        /// Ham kiem tra khai bao cac thuoc tinh cua Control hop le
        /// </summary>
        private bool CheckRequireControl(ref string messageError)
        {
            bool valid = true;
            switch (this.CheckControl.PopupWindowId)
            {
                //Kiem tra Validating Genba phai duoc gan voi Control Gyousha
                case WINDOW_ID.M_GENBA:
                case WINDOW_ID.M_GENBA_CLOSED:
                    valid = false;
                    if (this.CheckControl.PopupSearchSendParams != null)
                    {
                        foreach (var dto in this.CheckControl.PopupSearchSendParams)
                        {
                            if (dto.KeyName == "GYOUSHA_CD")
                            {
                                if (String.IsNullOrEmpty(dto.Control))
                                {
                                    if (!String.IsNullOrEmpty(dto.Value))
                                    {
                                        valid = true;
                                    }
                                }
                                else
                                {
                                    object[] control = new ControlUtility().FindControl(this.Param, new string[] { dto.Control });
                                    string controlText = PropertyUtility.GetTextOrValue(control[0]);
                                    if (!String.IsNullOrEmpty(controlText))
                                    {
                                        valid = true;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    if (valid == false)
                    {
                        var messageUtil = new MessageUtility();
                        string  message = messageUtil.GetMessage("E051").MESSAGE;
                        messageError = String.Format(message, "業者");
                    }
                    break;
            }
            return valid;
        }
        /// <summary>
        ///Ham khoi tao cac gia tri ban dau de thuc hien tao SQL
        /// </summary>
        public void SetInitSearchString()
        {
            this.SelectStr = string.Empty;
            this.JoinStr = string.Empty;
            this.WhereStr = string.Empty;
            this.KeyValue = this.CheckControl.GetResultText();
            this.KeyName = this.CheckControl.DBFieldsName;
            this.TableName = this.CheckControl.PopupWindowId.ToString();

            switch (this.CheckControl.PopupWindowId)
            {
                case WINDOW_ID.M_GYOUSHA:
                    this.KeyName = "GYOUSHA_CD";
                    this.TableName = "M_GYOUSHA";
                    break;
                case WINDOW_ID.M_GENBA:
                case WINDOW_ID.M_GENBA_CLOSED:
                    this.KeyName = "GENBA_CD";
                    this.TableName = "M_GENBA";
                    break;
                case WINDOW_ID.M_HINMEI:
                    this.KeyName = "HINMEI_CD";
                    this.TableName = "M_HINMEI";
                    break;
                case WINDOW_ID.M_SHARYOU:
                case WINDOW_ID.M_SHARYOU_CLOSED:
                    this.KeyName = "SHARYOU_CD";
                    this.TableName = "M_SHARYOU";
                    break;
                case WINDOW_ID.M_SHAIN:
                case WINDOW_ID.M_SHAIN_CLOSED:
                    this.KeyName = "SHAIN_CD";
                    this.TableName = "M_SHAIN";
                    break;
                default:

                    break;
            }
        }
   
        /// <summary>
        /// Ham chinh thuc hien tao du lieu cau SQL lay du lieu hop le
        /// </summary>
        private void SetSearchString()
        {
            //Tao cau SELECT cua SQL
            this.CreateSelectStr();

            //Tao cau WHERE cua SQL
            this.CreateWhereStr();

            //Tao cau JOIN cua SQL
            this.CreateJoinStr();
        }
        /// <summary>
        /// Ham thuc hien tao cau SELECT cua SQL, tuy truong hop se lay SQL phu hop tuong ung
        /// </summary>
        private void CreateSelectStr()
        {
            this.SelectStr = String.Format("SELECT * FROM {0} ", this.TableName);
            switch (this.CheckControl.PopupWindowId)
            {
                case WINDOW_ID.M_GYOUSHA:
                    this.SelectStr = this.ReadContentFileSql("r_framework.MasterAccess.Sql.GetGyoushaDataSql.sql");
                    break;
                case WINDOW_ID.M_GENBA:
                case WINDOW_ID.M_GENBA_CLOSED:
                    this.SelectStr = this.ReadContentFileSql("r_framework.MasterAccess.Sql.GetGenbaDataSql.sql");
                    break;
                case WINDOW_ID.M_HINMEI:
                    this.SelectStr = this.ReadContentFileSql("r_framework.MasterAccess.Sql.GetHinmeiDataSql.sql");
                    break;
                case WINDOW_ID.M_SHARYOU:
                case WINDOW_ID.M_SHARYOU_CLOSED:
                    this.SelectStr = this.ReadContentFileSql("r_framework.MasterAccess.Sql.GetSharyouData.sql");
                    break;
                default:

                    break;
            }
        }
        /// <summary>
        /// Ham thuc hien tao cau WHERE cua SQL
        /// </summary>
        private void CreateWhereStr()
        {
            //Tao cau WHERE cua khoa chinh
            this.WhereStr = " WHERE " + this.TableName + "." + this.KeyName + " = '" + this.KeyValue + "'";

            //Tao cau WHERE cho du lieu hop le (TEKIYOU_BEGIN,TEKIYOU_END,DELETE_FLG)
            var type = Type.GetType("r_framework.Entity." + this.TableName);
            if (type != null)
            {
                bool tekiyouFlg = false;
                bool deleteFlg = false;
                foreach (PopupSearchSendParamDto popupSearchSendParam in this.CheckControl.PopupSearchSendParams)
                {
                    if (popupSearchSendParam.KeyName != null && popupSearchSendParam.KeyName.Equals("TEKIYOU_FLG")
                        && !string.IsNullOrEmpty(popupSearchSendParam.Value))
                    {
                        if ("TRUE".Equals(popupSearchSendParam.Value.ToUpper()))
                        {
                            tekiyouFlg = true;
                            deleteFlg = true;
                        }
                        else if ("FALSE".Equals(popupSearchSendParam.Value.ToUpper()))
                        {
                            deleteFlg = true;
                        }
                    }
                }
                //var pNames = type.GetProperties().Select(p => p.Name);
                //if (pNames.Contains("TEKIYOU_BEGIN") && pNames.Contains("TEKIYOU_END"))
                //{
                //    tekiyouFlg = true;
                //}
                //if (pNames.Contains("DELETE_FLG"))
                //{
                //    deleteFlg = true;
                //}
                this.WhereStr = WhereStr + CreateWhereValidStr(this.TableName, tekiyouFlg, deleteFlg);

            }

            //Tao cau WHERE cho cac truong hop rieng them neu co
            string whereAdd = this.CreateWhereAddStr();

            this.WhereStr += whereAdd;

            //Tao cau WHERE cho property PopupSearchSendParamDto khai bao them 
            bool existSearchParam = false;  // popupSearchSendParamからWHEREが生成されたかどうかのフラグ
            StringBuilder sb = new StringBuilder(" ");
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.CheckControl.PopupSearchSendParams)
            {
                if (popupSearchSendParam.KeyName != null && popupSearchSendParam.KeyName.Equals("TEKIYOU_BEGIN"))
                {
                   
                    continue;
                }

                if (popupSearchSendParam.KeyName != null && popupSearchSendParam.KeyName.Equals("TEKIYOU_FLG"))
                {
                    
                    continue;
                }
                this.depthCnt = 1;
                existSearchParam = false;
                string where = string.Empty;
                if ((this.CheckControl.PopupWindowId == WINDOW_ID.M_GENBA_CLOSED 
                    || this.CheckControl.PopupWindowId == WINDOW_ID.M_SHAIN_CLOSED
                    || this.CheckControl.PopupWindowId == WINDOW_ID.M_SHARYOU_CLOSED)
                    && (popupSearchSendParam.KeyName == "SAGYOU_DATE" || popupSearchSendParam.KeyName == "DENPYOU_DATE"))
                {
                    where = this.CreateWhereAddClosedStr(popupSearchSendParam);
                }
                else
                {
                     where = this.CreateWhereStrFromScreen(popupSearchSendParam, this.TableName, ref existSearchParam);

                }
                sb.Append(where);
                if (sb.Length > 0)
                {
                    if (!existSearchParam)
                    {
                        this.depthCnt--;
                    }
                    for (int i = 0; i < this.depthCnt; i++)
                    {
                        sb.Append(") ");
                    }
                }
            }

            //Tra ve ket qua
            this.WhereStr += sb.ToString();
        }

        /// <summary>
        /// Ham thuc hien tao cau WHERE cho cac truong hop rieng them neu co
        /// </summary>
        private string CreateWhereAddStr( )
        {
            string where = string.Empty;
            
            return where;
        }

        /// <summary>
        /// Ham thuc hien tao cau WHERE cho cac truong hop rieng boi M_GENBA_CLOSED/M_SHAIN_CLOSED/M_SHARYOU_CLOSED
        /// </summary>
        private string CreateWhereAddClosedStr(PopupSearchSendParamDto popupSearchSendParamDto)
        {
            string where = string.Empty; ;
            object[] control = new ControlUtility().FindControl(this.Param, new string[] { popupSearchSendParamDto.Control });
            string controlValue = PropertyUtility.GetTextOrValue(control[0]);
            switch (this.CheckControl.PopupWindowId)
            {
                case WINDOW_ID.M_GENBA_CLOSED:
                    if (!String.IsNullOrEmpty(controlValue))
                    {
                        where += " AND NOT EXISTS( ";
                        where += "SELECT 1 FROM M_WORK_CLOSED_HANNYUUSAKI T2 ";
                        where += "WHERE M_GENBA.GYOUSHA_CD = T2.GYOUSHA_CD ";
                        where += "AND M_GENBA.GENBA_CD = T2.GENBA_CD ";
                        where += string.Format("AND CONVERT(CHAR(10), T2.CLOSED_DATE, 111) = CONVERT(CHAR(10), '{0}', 111) ", controlValue );
                        where += "AND T2.DELETE_FLG = 0) ";
                    }
                    break;
                case WINDOW_ID.M_SHAIN_CLOSED:
                    if (!String.IsNullOrEmpty(controlValue))
                    {
                        where += " AND NOT EXISTS( ";
                        where += "SELECT 1 FROM M_WORK_CLOSED_UNTENSHA T2 ";
                        where += "WHERE M_SHAIN.SHAIN_CD = T2.SHAIN_CD ";
                        where += string.Format("AND CONVERT(CHAR(10), T2.CLOSED_DATE, 111) = CONVERT(CHAR(10), '{0}', 111) ", controlValue );
                        where += "AND T2.DELETE_FLG = 0) ";
                    }
                    break;
                case WINDOW_ID.M_SHARYOU_CLOSED:
                    if (!String.IsNullOrEmpty(controlValue))
                    {
                        where += " AND NOT EXISTS( SELECT 1 FROM M_WORK_CLOSED_SHARYOU T2 ";
                        where += "WHERE M_SHARYOU.GYOUSHA_CD = T2.GYOUSHA_CD ";
                        where += "AND M_SHARYOU.SHARYOU_CD = T2.SHARYOU_CD ";
                        where += string.Format("AND CONVERT(CHAR(10), T2.CLOSED_DATE, 111) = CONVERT(CHAR(10), '{0}', 111) ", controlValue );
                        where += "AND T2.DELETE_FLG = 0) ";
                    }
                    break;
                default:
                    break;
            }
            return where;
        }

        /// <summary>
        /// Ham thuc hien tao cau WHERE cho property PopupSearchSendParamDto
        /// </summary>
        /// <param name="dto">PopupSearchSendParamDto</param>
        /// <param name="tableName">tableName</param>
        /// <param name="existSearchParam">existSearchParam</param>
        /// <returns></returns>
        private string CreateWhereStrFromScreen(PopupSearchSendParamDto dto, string tableName, ref bool existSearchParam)
        {
            StringBuilder sb = new StringBuilder();

            bool subExistSearchParam = false;
            int thisDepth = this.depthCnt;

            // 括弧付きの条件対応
            if (dto.SubCondition != null && 0 < dto.SubCondition.Count)
            {
                this.depthCnt++;
                foreach (PopupSearchSendParamDto popupSearchSendParam in dto.SubCondition)
                {
                    string where = this.CreateWhereStrFromScreen(popupSearchSendParam, tableName, ref subExistSearchParam);
                    sb.Append(where);
                }

                // 条件をまとめるため
                if (subExistSearchParam)
                {
                    for (int i = 0; i < thisDepth; i++)
                    {
                        sb.Append(") ");
                    }
                }
                else
                {
                    this.depthCnt--;
                }
            }

            // 通常のWHERE句を生成
            if (string.IsNullOrEmpty(dto.KeyName))
            {
                return sb.ToString();
            }

            // 絞込み条件にControlが指定されていればそれを使い、無ければValueを使用する
            // 両方無ければ条件句の生成はしない
            string whereValue = this.CreateWhere(dto);

            if (string.IsNullOrEmpty(whereValue))
            {
                return sb.ToString();
            }

            sb.Append(dto.And_Or.ToString());

            if (!existSearchParam)
            {
                for (int i = 0; i < thisDepth; i++)
                {
                    sb.Append(" (");
                }
            }

            if (dto.KeyName.Contains("."))
            {
                sb.Append(" (")
                  .Append(dto.KeyName)
                  .Append(" ")
                  .Append(whereValue)
                  .Append(") ");
            }
            else
            {
                sb.Append(" (")
                  .Append(tableName)
                  .Append(".")
                  .Append(dto.KeyName)
                  .Append(" ")
                  .Append(whereValue)
                  .Append(" ) ");
            }

            existSearchParam = true;

            return sb.ToString();
        }

        /// <summary>
        /// Ham thuc hien tao dieu kien WHERE cho property PopupSearchSendParamDto
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="whereValue"></param>
        private string CreateWhere(PopupSearchSendParamDto dto)
        {
            CNNECTOR_SIGNS sqlConnectorSign = CNNECTOR_SIGNS.EQUALS;  // KeyとValueをつなぐ符号
            string returnStr = string.Empty;

            if (dto == null)
            {
                return returnStr;
            }
            if (dto.Control == null || string.IsNullOrEmpty(dto.Control))
            {
                if (dto.Value != null && !string.IsNullOrEmpty(dto.Value))
                {
                    if (dto.Value.Contains(","))
                    {
                        sqlConnectorSign = CNNECTOR_SIGNS.IN;
                        // 使用側で"'"を意識しないで使わせたいので、FW側で"'"をつける
                        string[] valueList = dto.Value.Replace(" ", "").Split(',');
                        foreach (string tempValue in valueList)
                        {
                            if (string.IsNullOrEmpty(returnStr))
                            {
                                returnStr = "'" + tempValue + "'";
                            }
                            else
                            {
                                returnStr = returnStr + ", '" + tempValue + "'";
                            }
                        }
                        returnStr = "(" + returnStr + ")";
                    }
                    else
                    {
                        sqlConnectorSign = CNNECTOR_SIGNS.EQUALS;
                        returnStr = "'" + dto.Value + "'";
                    }
                }
            }
            else
            {
                object[] control = new ControlUtility().FindControl(this.Param, new string[] { dto.Control });
                string controlText = PropertyUtility.GetTextOrValue(control[0]);
                if (control != null && !string.IsNullOrEmpty(controlText))
                {
                    // 複数同じ名前のコントロールは存在しないはず
                    returnStr = "'" + controlText + "'";
                }
            }

            if (!string.IsNullOrEmpty(returnStr))
            {
                return CNNECTOR_SIGNSExt.ToTypeString(sqlConnectorSign) + " " + returnStr;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Ham thuc hien tao dieu kien WHERE valid
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <returns></returns>
        private static string CreateWhereValidStr(string tableName,bool tekiyouFlg,bool deleteFlg)
        {
            string strWhereValid = string.Empty;
            if (tekiyouFlg)
            {
                strWhereValid += String.Format(" AND CONVERT(DATE, ISNULL({0}.TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL({0}.TEKIYOU_END, DATEADD(day,1,GETDATE()))) ", tableName);
            }
            if (deleteFlg)
            {
                strWhereValid += String.Format(" AND {0}.DELETE_FLG = 0 ", tableName);
            }
            return strWhereValid;
        }

        /// <summary>
        /// Ham thuc hien tao JOIN table va WHERE tuong ung cho property popupWindowSetting
        /// </summary>
        private void CreateJoinStr()
        {
            var join = new StringBuilder();
            var where = new StringBuilder();
            var isChecked = new List<string>();
            foreach (JoinMethodDto joinData in this.CheckControl.popupWindowSetting)
            {
                if (joinData.Join != JOIN_METHOD.WHERE)
                {
                    if (!string.IsNullOrEmpty(joinData.LeftTable) && !string.IsNullOrEmpty(joinData.LeftKeyColumn) &&
                        !string.IsNullOrEmpty(joinData.RightTable) && !string.IsNullOrEmpty(joinData.RightKeyColumn))
                    {
                        join.Append(" " + JOIN_METHODExt.ToString(joinData.Join) + " ");
                        join.Append(joinData.LeftTable + " ON ");
                        join.Append(joinData.LeftTable + "." + joinData.LeftKeyColumn + " = ");
                        join.Append(joinData.RightTable + "." + joinData.RightKeyColumn + " ");
                    }
                }
                else if (joinData.Join == JOIN_METHOD.WHERE)
                {
                   
                    var searchStr = new StringBuilder();
                    foreach (var searchData in joinData.SearchCondition)
                    {
                        if (searchData.LeftColumn != null && searchData.LeftColumn.Equals("TEKIYOU_BEGIN"))
                        {

                            continue;
                        }

                        if (searchData.LeftColumn != null && searchData.LeftColumn.Equals("TEKIYOU_FLG"))
                        {

                            continue;
                        }
                        //検索条件設定
                        if (string.IsNullOrEmpty(searchData.Value))
                        {
                            //value値がnullのため、テーブル同士のカラム結合を行う
                            if (searchStr.Length == 0)
                            {
                                searchStr.Append(" AND (");
                            }
                            else
                            {
                                searchStr.Append(" ");
                                searchStr.Append(searchData.And_Or.ToString());
                                searchStr.Append(" ");
                            }
                            searchStr.Append(joinData.LeftTable);
                            searchStr.Append(".");
                            searchStr.Append(searchData.LeftColumn);
                            var data = joinData.RightTable + "." + searchData.RightColumn;
                            searchStr.Append(searchData.Condition.ToConditionString(data));
                        }
                        else
                        {
                            // コントロールの値が有効な場合WHERE句を作成する
                            var data = createValues(this.Param, searchData);

                            if (!string.IsNullOrEmpty(data))
                            {
                                if (searchStr.Length == 0)
                                {
                                    searchStr.Append(" AND (");
                                }
                                else
                                {
                                    searchStr.Append(" ");
                                    searchStr.Append(searchData.And_Or.ToString());
                                    searchStr.Append(" ");
                                }
                                searchStr.Append(joinData.LeftTable);
                                searchStr.Append(".");
                                searchStr.Append(searchData.LeftColumn);
                                searchStr.Append(searchData.Condition.ToConditionString(data));
                                searchStr.Append(" ");
                            }
                        }
                    }
                    if (searchStr.Length > 0)
                    {
                        // 閉じる
                        searchStr.Append(") ");
                    }
                    where.Append(searchStr);
                }
                // 有効レコードをチェックする
                if (joinData.IsCheckLeftTable == true && !isChecked.Contains(joinData.LeftTable))
                {
                    var type = Type.GetType("r_framework.Entity." + joinData.LeftTable);
                    if (type != null)
                    {
                        bool tekiyouFlg = false;
                        bool deleteFlg = false;
                        var pNames = type.GetProperties().Select(p => p.Name);
                        if (pNames.Contains("TEKIYOU_BEGIN") && pNames.Contains("TEKIYOU_END"))
                        {
                            tekiyouFlg = true;
                        }
                        if (pNames.Contains("DELETE_FLG"))
                        {
                            deleteFlg = true;
                        }
                        where.Append(CreateWhereValidStr(joinData.LeftTable, tekiyouFlg, deleteFlg));

                    }
                    isChecked.Add(joinData.LeftTable);
                }
            }
            this.JoinStr += join.ToString();
            this.WhereStr += where.ToString();
        }

        /// <summary>
        /// 検索条件を作成する
        /// 対象のコントロールが見つけれた場合については、コントロールの値とする
        /// コントロールが見つからない場合は、Valuesの値を直接設定する
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string createValues(object[] controls, SearchConditionsDto dto)
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
        /// Ham doc noi dung cua file SQL dua vao
        /// </summary>
        private string ReadContentFileSql(string executeSqlFilePath)
        {
            string sqlContent = string.Empty;
            var thisAssembly = Assembly.GetExecutingAssembly();
            using (var resourceStream = thisAssembly.GetManifestResourceStream(executeSqlFilePath))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    sqlContent = sqlStr.ReadToEnd(); ;
                }
            }
            return sqlContent;
        }

        #endregion

    }
}
