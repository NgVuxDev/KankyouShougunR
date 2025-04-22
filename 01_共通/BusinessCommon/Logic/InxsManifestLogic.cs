using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Enums;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    public class InxsManifestLogic
    {
        private readonly CommonEntryDaoCls _dao;
        private readonly MessageBoxShowLogic _msgBox;
        private EnumManifestType _manifestType;

        public InxsManifestLogic(EnumManifestType manifestType)
        {
            _dao = DaoInitUtility.GetComponent<CommonEntryDaoCls>(); ;
            _msgBox = new MessageBoxShowLogic();
            _manifestType = manifestType;
        }

        public bool IsUploadManifestToInxs(string systemId)
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(systemId))
                {
                    return result;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM T_MANIFEST_ENTRY_SUB ");
                sql.AppendFormat(" WHERE MANIFEST_TYPE = 1 AND MANIFEST_SYS_ID = {0} ", systemId);
                sql.Append(" AND DELETE_FLG = 0 ");

                DataTable dt = this._dao.GetDataForStringSql(sql.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsUploadManifestToInxs", ex);
                result = false;
            }

            return result;
        }

        public bool IsUploadDenshiManifestToInxs(string kanriId)
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(kanriId))
                {
                    return result;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM T_MANIFEST_ENTRY_SUB ");
                sql.AppendFormat(" WHERE  MANIFEST_TYPE = 2 AND KANRI_ID = '{0}' ", kanriId);
                sql.Append(" AND DELETE_FLG = 0 ");

                DataTable dt = this._dao.GetDataForStringSql(sql.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsUploadDenshiManifestToInxs", ex);
                result = false;
            }

            return result;
        }

        public void DeleteInxsData(string deleteId, string transactionId, string formText)
        {
            List<CommonKeyGenericDto<string>> commandArgs = new List<CommonKeyGenericDto<string>>()
            {
                new CommonKeyGenericDto<string>(){Id = deleteId}
            };

            var requestDto = new
            {
                CommandName = _manifestType == EnumManifestType.KAMI ? 9 : 10, //DeleteInxsManifestKami
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
                                if (responeDto != null && responeDto.MessageType == Enums.EnumMessageType.ERROR)
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
