using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Const;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    public class InxsClass
    {
        public RequestStatusInxsDto GetRequestStatusInfo(int requestStatus)
        {
            RequestStatusInxsDto result = new RequestStatusInxsDto();
            result.Id = requestStatus;
            switch (requestStatus)
            {
                case CommonConst.RequestStatusInxs.WAITING_NEW_CONFIRM_VALUE:
                    result.DisplayText = CommonConst.RequestStatusInxs.WAITING_NEW_CONFIRM_TEXT;
                    result.BackColor = CommonConst.RequestStatusInxs.WAITING_NEW_CONFIRM_BACK_COLOR;
                    result.ForeColor = CommonConst.RequestStatusInxs.WAITING_NEW_CONFIRM_FORE_COLOR;
                    break;
                case CommonConst.RequestStatusInxs.CONFIRMED_VALUE:
                    result.DisplayText = CommonConst.RequestStatusInxs.CONFIRMED_TEXT;
                    result.BackColor = CommonConst.RequestStatusInxs.CONFIRMED_BACK_COLOR;
                    result.ForeColor = CommonConst.RequestStatusInxs.CONFIRMED_FORE_COLOR;
                    break;
                case CommonConst.RequestStatusInxs.WAITING_CHANGE_CONFIRM_VALUE:
                    result.DisplayText = CommonConst.RequestStatusInxs.WAITING_CHANGE_CONFIRM_TEXT;
                    result.BackColor = CommonConst.RequestStatusInxs.WAITING_CHANGE_CONFIRM_BACK_COLOR;
                    result.ForeColor = CommonConst.RequestStatusInxs.WAITING_CHANGE_CONFIRM_FORE_COLOR;
                    break;
                case CommonConst.RequestStatusInxs.WAITING_CANCE_CONFIRM_VALUE:
                    result.DisplayText = CommonConst.RequestStatusInxs.WAITING_CANCE_CONFIRM_TEXT;
                    result.BackColor = CommonConst.RequestStatusInxs.WAITING_CANCE_CONFIRM_BACK_COLOR;
                    result.ForeColor = CommonConst.RequestStatusInxs.WAITING_CANCE_CONFIRM_FORE_COLOR;
                    break;
                case CommonConst.RequestStatusInxs.WAITING_ADJUST_CONFIRM_VALUE:
                    result.DisplayText = CommonConst.RequestStatusInxs.WAITING_ADJUST_CONFIRM_TEXT;
                    result.BackColor = CommonConst.RequestStatusInxs.WAITING_ADJUST_CONFIRM_BACK_COLOR;
                    result.ForeColor = CommonConst.RequestStatusInxs.WAITING_ADJUST_CONFIRM_FORE_COLOR;
                    break;
                case CommonConst.RequestStatusInxs.ADJUSTING_VALUE:
                    result.DisplayText = CommonConst.RequestStatusInxs.ADJUSTING_TEXT;
                    result.BackColor = CommonConst.RequestStatusInxs.ADJUSTING_BACK_COLOR;
                    result.ForeColor = CommonConst.RequestStatusInxs.ADJUSTING_FORE_COLOR;
                    break;
                case CommonConst.RequestStatusInxs.CANCELED_VALUE:
                    result.DisplayText = CommonConst.RequestStatusInxs.CANCELED_TEXT;
                    result.BackColor = CommonConst.RequestStatusInxs.CANCELED_BACK_COLOR;
                    result.ForeColor = CommonConst.RequestStatusInxs.CANCELED_FORE_COLOR;
                    break;
                case CommonConst.RequestStatusInxs.CANCEL_ADJUSTMENT_VALUE:
                    result.DisplayText = CommonConst.RequestStatusInxs.CANCEL_ADJUSTMENT_TEXT;
                    result.BackColor = CommonConst.RequestStatusInxs.CANCEL_ADJUSTMENT_BACK_COLOR;
                    result.ForeColor = CommonConst.RequestStatusInxs.CANCEL_ADJUSTMENT_FORE_COLOR;
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }
    }
}
