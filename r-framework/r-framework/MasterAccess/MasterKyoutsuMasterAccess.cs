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
    /// <summary>
    /// 取引先マスタをチェックするクラス
    /// </summary>
    [Obsolete("処理共通化のため廃止。このクラスには削除不ラブを見ていないバグもある。Implにあるクラスを使ってください。",true)]
    public class MasterKyoutsuMasterAccess : IMasterDataAccess
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
        //Sql Where
        public string JoinWhereStr;

        //Table Result
        public DataTable EntityTable;
        /// <summary>
        /// CDのMax桁数
        /// </summary>
        public readonly int CdMaxLength = 6;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MasterKyoutsuMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            Dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
        }

        /// <summary>
        /// Ham chinh duoc goi tu phuong thuc khai bao cho FocusOutCheckMethod
        /// </summary>
        public string CodeCheckAndSetting()
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
                    string displayName = this.CheckControl.DisplayItemName.Replace("CD", "");
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
            string sql = this.SelectStr + this.JoinWhereStr;

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
            string nameValid = string.Empty;
            switch (this.CheckControl.PopupWindowId)
            {
                //Kiem tra Validating Genba phai duoc gan voi Control Gyousha
                case WINDOW_ID.M_GENBA:
                case WINDOW_ID.M_GENBA_CLOSED:
                    valid = false;
                    nameValid = "業者";
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
                                    if (control.Length > 0 && control[0] is ICustomControl)
                                    {
                                        var customControl = control[0] as ICustomControl;
                                        if (!String.IsNullOrEmpty(customControl.DisplayItemName))
                                        {
                                            nameValid = customControl.DisplayItemName.Replace("CD", ""); ;
                                        }
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
                        messageError = String.Format(message, nameValid);
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
            this.JoinWhereStr = string.Empty;
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
                case WINDOW_ID.M_EIGYOU_TANTOUSHA:
                case WINDOW_ID.M_NYUURYOKU_TANTOUSHA:
                case WINDOW_ID.M_SHOBUN_TANTOUSHA:
                case WINDOW_ID.M_TEGATA_HOKANSHA:
                case WINDOW_ID.M_UNTENSHA:
                case WINDOW_ID.M_INXS_TANTOUSHA:
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

        }
        /// <summary>
        /// Ham thuc hien tao cau SELECT cua SQL, tuy truong hop se lay SQL phu hop tuong ung
        /// </summary>
        private void CreateSelectStr()
        {
            this.SelectStr = String.Format("SELECT * FROM {0} ", this.TableName);
        }
        /// <summary>
        /// Ham thuc hien tao cau WHERE cua SQL
        /// </summary>
        private void CreateWhereStr()
        {
            string whereSql = string.Empty;
            if (this.CheckControl.popupWindowSetting != null && this.CheckControl.popupWindowSetting.Count > 0)
            {
                if (this.CheckControl.popupWindowSetting[0].IsCheckLeftTable != null || this.CheckControl.popupWindowSetting[0].IsCheckRightTable != null)
                {
                    // JoinMethodDtoが一つでもセットされており、IsCheckプロパティのどちらかがnull以外のとき
                    whereSql = SqlCreateUtility.CreatePopupSql2(this.CheckControl.popupWindowSetting, this.Param);

                }
                else
                {
                    // SqlCreateUtility.CreatePopupSql2が実行されなかった場合
                     whereSql = SqlCreateUtility.CreatePopupSql(this.CheckControl.popupWindowSetting, this.CheckControl.PopupWindowId, this.Param);
                }
            }
            else
            {
                // SqlCreateUtility.CreatePopupSql2が実行されなかった場合
                 whereSql = SqlCreateUtility.CreatePopupSql(this.CheckControl.popupWindowSetting, this.CheckControl.PopupWindowId, this.Param);
                
            }
            this.JoinWhereStr = whereSql;

            //Tao cau WHERE cua khoa chinh
            if (this.JoinWhereStr.ToUpper().IndexOf("WHERE")>=0)
            {
                this.JoinWhereStr += " AND " + this.TableName + "." + this.KeyName + " = '" + this.KeyValue + "'";
            }
            else
            {
                this.JoinWhereStr += " WHERE " + this.TableName + "." + this.KeyName + " = '" + this.KeyValue + "'";
            }

            //Tao cau WHERE cho cac truong hop rieng them neu co
            string whereAdd = this.CreateWhereAddClosedStr();

            this.JoinWhereStr += whereAdd;

        }

        /// <summary>
        /// Ham thuc hien tao cau WHERE cho cac truong hop rieng boi M_GENBA_CLOSED/M_SHAIN_CLOSED/M_SHARYOU_CLOSED
        /// </summary>
        private string CreateWhereAddClosedStr()
        {
            string sql = string.Empty;
            if (this.CheckControl.PopupWindowId == WINDOW_ID.M_SHAIN_CLOSED && this.CheckControl.PopupSearchSendParams != null)
            {
                foreach (var dto in this.CheckControl.PopupSearchSendParams)
                {
                    object[] control = new ControlUtility().FindControl(this.Param, new string[] { dto.Control });
                    if (dto.KeyName == "SAGYOU_DATE" || dto.KeyName == "DENPYOU_DATE")
                    {
                        sql += " AND NOT EXISTS( ";
                        sql += "SELECT 1 FROM M_WORK_CLOSED_UNTENSHA T2 ";
                        sql += "WHERE M_SHAIN.SHAIN_CD = T2.SHAIN_CD ";
                        sql += string.Format("AND CONVERT(CHAR(10), T2.CLOSED_DATE, 111) = CONVERT(CHAR(10), '{0}', 111) ", PropertyUtility.GetTextOrValue(control[0]));
                        sql += "AND M_SHAIN.DELETE_FLG = 0 ";
                        sql += "AND T2.DELETE_FLG = 0) ";
                    }
                }
            }
            return sql;
        }


        #endregion

    }
}
