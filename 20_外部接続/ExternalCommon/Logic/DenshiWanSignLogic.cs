using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Logic
{
    /// <summary>
    /// 電子契約（WAN-Sign）
    /// </summary>
    public class DenshiWanSignLogic
    {
        #region プロパティ
        /// <summary>システム設定</summary>
        private M_SYS_INFO sysInfo = null;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenshiWanSignLogic()
        {
            this.sysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData().FirstOrDefault();
        }
        #endregion

        #region アクセストークン生成API
        /// <summary>
        /// アクセストークン取得
        /// </summary>
        /// <returns></returns>
        public RESULT_ACCESS_TOKEN_WAN_SIGN GetAccessTokenWanSign()
        {
            if (this.sysInfo == null)
            {
                return null;
            }

            var requestDto = new RequestBase()
            {
                Secret_Key = this.sysInfo.SECRET_KEY,
                Cus_Id = this.sysInfo.CUSTOMER_ID,
            };

            var httpRequest = new HttpRequestApiWanSign(DenshiWanSignConst.ROUTE_ACCESS_TOKEN);
            var response = httpRequest.ExecutePost<RequestBase, RESULT_ACCESS_TOKEN_WAN_SIGN>(requestDto);

            if (response == null ||
                response.Result == null)
            {
                return null;
            }

            //APIレスポンス-satus≠０（0以外）
            if (!"0".Equals(response.Status))
            {
                //メッセージA表示
                MessageBox.Show(string.Format(DenshiWanSignConst.MsgA, response.Status), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return response;
        }
        #endregion

        #region 関連コード取得API
        /// <summary>
        /// 関連コード取得
        /// </summary>
        /// <param name="accessToken">アクセストークン</param>
        /// <returns></returns>
        public RESULT_CONTROL_NUMBER_WAN_SIGN GetControlNumberWanSign(string accessToken)
        {
            if (this.sysInfo == null)
            {
                return null;
            }

            var requestDto = new RequestControlNumber()
            {
                Secret_Key = this.sysInfo.SECRET_KEY,
                Cus_Id = this.sysInfo.CUSTOMER_ID,
                Access_Token = accessToken

            };

            var httpRequest = new HttpRequestApiWanSign(DenshiWanSignConst.ROUTE_CONTROL_NUMBER);
            var response = httpRequest.ExecutePost<RequestControlNumber, RESULT_CONTROL_NUMBER_WAN_SIGN>(requestDto);

            return response;
        }
        #endregion

        #region 文書詳細情報取得API
        /// <summary>
        /// 文書詳細情報取得
        /// </summary>
        /// <param name="accessToken">アクセストークン</param>
        /// <param name="controlNumber">関連コード</param>
        /// <param name="xid">トランザクションID</param>
        /// <returns></returns>
        public RESULT_KEIYAKU_INFO_WAN_SIGN GetKeyakuInfoWanSign(string accessToken, string controlNumber, string xid = null)
        {
            if (this.sysInfo == null)
            {
                return null;
            }

            var requestDto = new RequestKeiyakuInfo()
            {
                Secret_Key = this.sysInfo.SECRET_KEY,
                Cus_Id = this.sysInfo.CUSTOMER_ID,
                Access_Token = accessToken,
                Xid = xid,
                Control_Number = controlNumber

            };

            var httpRequest = new HttpRequestApiWanSign(DenshiWanSignConst.ROUTE_KEIYAKU_INFO);
            var response = httpRequest.ExecutePost<RequestKeiyakuInfo, RESULT_KEIYAKU_INFO_WAN_SIGN>(requestDto);

            return response;
        }
        #endregion

        #region 文書取得API

        /// <summary>
        /// 文書取得
        /// </summary>
        /// <param name="dir">ファイルの出力ダイアログ</param>
        /// <param name="accessToken">アクセストークン</param>
        /// <param name="controlNumber">関連コード</param>
        /// <param name="xid">トランザクションID</param>
        /// <returns></returns>
        public bool DownLoadKeyakuWanSign(string dir, string accessToken, string controlNumber, string xid = null)
        {
            if (this.sysInfo == null)
            {
                return false;
            }

            var requestDto = new RequestDownLoadKeiyaku()
            {
                Secret_Key = this.sysInfo.SECRET_KEY,
                Cus_Id = this.sysInfo.CUSTOMER_ID,
                Access_Token = accessToken,
                Xid = xid,
                Control_Number = controlNumber,
                Operation = "2",
                Download_Type = "2"
            };

            var httpRequest = new HttpRequestApiWanSign(DenshiWanSignConst.ROUTE_KEIYAKU_DOWNLOAD);
            var response = httpRequest.ExecutePost<RequestDownLoadKeiyaku, RESULT_KEIYAKU_DOWNLOAD_WAN_SIGN>(requestDto);

            if (response == null ||
                response.Result == null)
            {
                return false;
            }

            //APIレスポンス-satus≠０（0以外）
            if (!"0".Equals(response.Status))
            {
                //メッセージL表示
                MessageBox.Show(string.Format(DenshiWanSignConst.MsgL, response.Status), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //レスポンスーステータス＝0（取得成功）
            if (!string.IsNullOrEmpty(response.Result.Documents_Url))
            {
                WebRequest request = WebRequest.Create(response.Result.Documents_Url);
                using (WebResponse getResponse = request.GetResponse())
                {
                    var files = getResponse.Headers["Content-disposition"];
                    if (!string.IsNullOrEmpty(response.Result.Documents_Url))
                    {
                        var arrs = files.Split(new string[] { "''" }, StringSplitOptions.None);
                        if (arrs.Length == 2)
                        {
                            var fileName = Path.Combine(dir, arrs[1]);
                            var count = 1;
                            var fileNameTmp = fileName;
                            while (File.Exists(fileNameTmp))
                            {
                                fileNameTmp = fileName.Replace(".pdf", "") + "(" + (count++).ToString() + ").pdf";
                            }
                            fileName = fileNameTmp;
                            using (var reader = getResponse.GetResponseStream())
                            {
                                using (var output = File.OpenWrite(fileName))
                                {
                                    reader.CopyTo(output);
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }
        #endregion

        #region 文書詳細情報編集API
        /// <summary>
        /// 文書詳細情報編集
        /// </summary>
        /// <param name="accessToken">アクセストークン</param>
        /// <param name="xid">トランザクションID</param>
        /// <returns></returns>
        public RESULT_KEIYAKU_INFO_WAN_SIGN WanSignDocumentDetailUpdate(string accessToken, M_WANSIGN_KEIYAKU_INFO entity)
        {
            if (this.sysInfo == null)
            {
                return null;
            }

            string ContractDate = string.Empty;
            if (!entity.CONTRACT_DATE.IsNull)
            {
                ContractDate = entity.CONTRACT_DATE.Value.ToString("yyyyMMdd");
            }
            string ContractExpirationDate = string.Empty;
            if (!entity.CONTRACT_EXPIRATION_DATE.IsNull)
            {
                ContractExpirationDate = entity.CONTRACT_EXPIRATION_DATE.Value.ToString("yyyyMMdd");
            }
            string IsAutoUpdating = string.Empty;
            if (!entity.IS_AUTO_UPDATING.IsNull)
            {
                IsAutoUpdating = entity.IS_AUTO_UPDATING.Value.ToString();
            }
            string IsReminder = string.Empty;
            if (!entity.IS_REMINDER.IsNull)
            {
                IsReminder = entity.IS_REMINDER.Value.ToString();
            }
            string ContractDecimal = string.Empty;
            if (!entity.CONTRACT_DECIMAL.IsNull)
            {
                ContractDecimal = entity.CONTRACT_DECIMAL.Value.ToString();
            }
            string IsValid = string.Empty;
            if (!entity.IS_VALID.IsNull)
            {
                IsValid = entity.IS_VALID.Value.ToString();
            }

            List<PARTNER_ORGANIZE_NAME> ListPartner = new List<PARTNER_ORGANIZE_NAME>();
            PARTNER_ORGANIZE_NAME Partner = new PARTNER_ORGANIZE_NAME();
            if(!string.IsNullOrEmpty(entity.PARTNER_ORGANIZE_NM))
            {
                Partner.Partner_Organize_Name = entity.PARTNER_ORGANIZE_NM;
            }
            ListPartner.Add(Partner);

			//PhuocLoc 2022/03/09 #161245, #161246 -Start
            string Storage_Location = Regex.Replace(entity.STORAGE_LOCATION, @"[\x00\x1a\\""]", @"\$0");
            Storage_Location = Storage_Location.Replace(System.Environment.NewLine, @"\r\n");

            string Comment_1 = Regex.Replace(entity.COMMENT_1, @"[\x00\x1a\\""]", @"\$0");
            Comment_1 = Comment_1.Replace(System.Environment.NewLine, @"\r\n");

            string Comment_2 = Regex.Replace(entity.COMMENT_2, @"[\x00\x1a\\""]", @"\$0");
            Comment_2 = Comment_2.Replace(System.Environment.NewLine, @"\r\n");

            string Comment_3 = Regex.Replace(entity.COMMENT_3, @"[\x00\x1a\\""]", @"\$0");
            Comment_3 = Comment_3.Replace(System.Environment.NewLine, @"\r\n");

            string Field_1 = Regex.Replace(entity.FIELD_1, @"[\x00\x1a\\""]", @"\$0");
            Field_1 = Field_1.Replace(System.Environment.NewLine, @"\r\n");

            string Field_2 = Regex.Replace(entity.FIELD_2, @"[\x00\x1a\\""]", @"\$0");
            Field_2 = Field_2.Replace(System.Environment.NewLine, @"\r\n");

            string Field_3 = Regex.Replace(entity.FIELD_3, @"[\x00\x1a\\""]", @"\$0");
            Field_3 = Field_3.Replace(System.Environment.NewLine, @"\r\n");

            string Field_4 = Regex.Replace(entity.FIELD_4, @"[\x00\x1a\\""]", @"\$0");
            Field_4 = Field_4.Replace(System.Environment.NewLine, @"\r\n");

            string Field_5 = Regex.Replace(entity.FIELD_5, @"[\x00\x1a\\""]", @"\$0");
            Field_5 = Field_5.Replace(System.Environment.NewLine, @"\r\n");

            var requestDto = new RequestKeiyakuInfo()
            {
                Secret_Key = this.sysInfo.SECRET_KEY,
                Cus_Id = this.sysInfo.CUSTOMER_ID,
                Access_Token = accessToken,
                Control_Number = entity.CONTROL_NUMBER,
                Document_Name = entity.DOCUMENT_NAME,
                Contract_Date = ContractDate,
                Contract_Expiration_Date = ContractExpirationDate,
                Is_Auto_Updating = IsAutoUpdating,
                Renewal_Period = entity.RENEWWAL_PERIOD,
                Renewal_Period_Unit = entity.RENEWWAL_PERIOD_UNIT,
                Cancel_Period = entity.CANCEL_PERIOD,
                Cancel_Period_Unit = entity.CANCEL_PERIOD_UNIT,
                Is_Reminder = IsReminder,
                Reminder_Period = entity.REMINDER_PERIOD,
                Reminder_Period_Unit = entity.REMINDER_PERIOD_UNIT,
                Post_nm = entity.POST_NM,
                Name_nm = entity.NAME_NM,
                Contract_Decimal = ContractDecimal,
                Storage_Location = Storage_Location,
                Comment_1 = Comment_1,
                Comment_2 = Comment_2,
                Comment_3 = Comment_3,
                Field_1 = Field_1,
                Field_2 = Field_2,
                Field_3 = Field_3,
                Field_4 = Field_4,
                Field_5 = Field_5,
                Is_Valid = IsValid,
                Partner = ListPartner,
                //Xid = entity.XID,
                Original_Control_Number = entity.ORIGINAL_CONTROL_NUMBER
            };
            //PhuocLoc 2022/03/09 #161245, #161246 -End

            var httpRequest = new HttpRequestApiWanSign(DenshiWanSignConst.ROUTE_DOCUMENT_DETAIL_UPDATE);
            var response = httpRequest.ExecutePost<RequestKeiyakuInfo, RESULT_KEIYAKU_INFO_WAN_SIGN>(requestDto);

            return response;
        }
        #endregion
    }
}
