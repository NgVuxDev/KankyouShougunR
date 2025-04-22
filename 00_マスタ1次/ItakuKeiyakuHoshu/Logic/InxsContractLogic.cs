using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ItakuKeiyakuHoshu.Logic
{
    public class InxsContractLogic
    {
        private readonly IM_FILE_LINK_ITAKU_KEIYAKU_KIHONDao _dao;
        private readonly MessageBoxShowLogic _msgBox;

        public InxsContractLogic()
        {
            _dao = DaoInitUtility.GetComponent<IM_FILE_LINK_ITAKU_KEIYAKU_KIHONDao>(); ;
            _msgBox = new MessageBoxShowLogic();
        }

        /// <summary>
        /// Check is uploaded contract to INXS
        /// </summary>
        /// <returns>true is uploaded</returns>
        public bool CheckIsUploadContractToInxs(string deleteSystemId)
        {
            bool result = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM T_CONTRACT_FILE_UPLOAD_INXS ");
                sql.AppendFormat(" WHERE CONTRACT_SYSTEM_ID = '{0}'", deleteSystemId);

                DataTable dt = this._dao.GetDateForStringSql(sql.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckIsUploadLicenseToInxs", ex);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Delete inxs data
        /// </summary>
        /// <param name="deleteSystemId"></param>
        /// <param name="transactionId"></param>
        /// <param name="formText"></param>
        public void DeleteInxsData(string deleteSystemId, string transactionId, string formText)
        {
            List<CommonKeyGenericDto<string>> commandArgs = new List<CommonKeyGenericDto<string>>()
            {
                new CommonKeyGenericDto<string>(){Id = deleteSystemId}
            };

            var requestDto = new
            {
                CommandName = 7, //DeleteInxsContractBySystemIds
                ShougunParentWindowName = formText,
                CommandArgs = commandArgs
            };

            RemoteAppCls remoteAppCls = new RemoteAppCls();
            var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
            {
                TransactionId = transactionId,
                ReferenceID = -1
            });
            var execCommandDto = new ExecuteCommandDto()
            {
                Token = token,
                Type = Shougun.Core.ExternalConnection.CommunicateLib.Enums.NotificationType.ExecuteCommand,
                Args = new object[] { JsonUtility.SerializeObject(requestDto) }
            };
            remoteAppCls.ExecuteCommand(Constans.StartFormText, execCommandDto);
        }

        /// <summary>
        /// Handle response message from subapp
        /// </summary>
        /// <param name="responseMsg"></param>
        /// <param name="transactionId"></param>
        public void HandleResponse(string responseMsg, string transactionId)
        {
            if (!string.IsNullOrEmpty(responseMsg))
            {
                var arg = JsonUtility.DeserializeObject<CommunicateAppDto>(responseMsg);
                if (arg != null)
                {
                    var msgDto = (CommunicateAppDto)arg;
                    var token = JsonUtility.DeserializeObject<CommunicateTokenDto>(msgDto.Token);
                    if (token != null)
                    {
                        if (token.TransactionId == transactionId)
                        {
                            if (msgDto.Args.Length > 0 && msgDto.Args[0] != null)
                            {
                                var responeDto = JsonUtility.DeserializeObject<InxsExecuteResponseDto>(msgDto.Args[0].ToString());
                                if (responeDto != null && responeDto.MessageType == Shougun.Core.Common.BusinessCommon.Enums.EnumMessageType.ERROR)
                                {
                                    this._msgBox.MessageBoxShowError(responeDto.ResponseMessage);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
