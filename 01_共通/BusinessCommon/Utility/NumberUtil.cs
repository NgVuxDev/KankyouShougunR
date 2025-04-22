using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Utility;
using r_framework.Entity;
using r_framework.Dao;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    public class NumberUtil
    {
        /// <summary>
        /// 型：請求値または支払い値を丸める
        /// </summary>
        public enum RoundType
        { 
            SEIKYUU,
            SHIHARAI
        }

        /// <summary>
        /// 税金を丸める
        /// </summary>
        /// <param name="prmValue">丸める必要な値</param>
        /// <param name="TorihikisakiCD">取引先ＣＤ</param>
        /// <param name="prmType">請求または支払い</param>
        /// <returns>丸められた値</returns>
        public static decimal RoundTax(decimal prmValue, string TorihikisakiCD, RoundType prmType)
        {
            decimal returnValue = 0;
            switch (prmType)
            {
                case RoundType.SEIKYUU:
                    //請求
                    var objTorihikisakiSeikyuu = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>().GetDataByCd(TorihikisakiCD);
                    if (objTorihikisakiSeikyuu == null || objTorihikisakiSeikyuu.TAX_HASUU_CD.IsNull)
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = DoRound(prmValue, (int)objTorihikisakiSeikyuu.TAX_HASUU_CD);
                    }
                    break;
                case RoundType.SHIHARAI:
                    //支払
                    var objTorihikisakiShiharai = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>().GetDataByCd(TorihikisakiCD);
                    if (objTorihikisakiShiharai == null || objTorihikisakiShiharai.TAX_HASUU_CD.IsNull)
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = DoRound(prmValue, (int)objTorihikisakiShiharai.TAX_HASUU_CD);
                    }
                    break;
                default:
                    returnValue = 0;
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// 金額を丸める
        /// </summary>
        /// <param name="prmValue">丸める必要な値</param>
        /// <param name="TorihikisakiCD">取引先ＣＤ</param>
        /// <param name="prmType">請求または支払い</param>
        /// <returns>丸められた値</returns>
        public static decimal RoundKingaku(decimal prmValue, string TorihikisakiCD, RoundType prmType)
        {
            decimal returnValue = 0;
            switch (prmType)
            {
                case RoundType.SEIKYUU:
                    //請求
                    var objTorihikisakiSeikyuu = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>().GetDataByCd(TorihikisakiCD);
                    if (objTorihikisakiSeikyuu == null || objTorihikisakiSeikyuu.KINGAKU_HASUU_CD.IsNull)
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = DoRound(prmValue, (int)objTorihikisakiSeikyuu.KINGAKU_HASUU_CD);
                    }
                    break;
                case RoundType.SHIHARAI:
                    //支払
                    var objTorihikisakiShiharai = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>().GetDataByCd(TorihikisakiCD);
                    if (objTorihikisakiShiharai == null || objTorihikisakiShiharai.KINGAKU_HASUU_CD.IsNull)
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = DoRound(prmValue, (int)objTorihikisakiShiharai.KINGAKU_HASUU_CD);
                    }
                    break;
                default:
                    returnValue = 0;
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// 種類通り丸める
        /// </summary>
        /// <param name="prmValue">丸める必要な値</param>
        /// <param name="prmType">種類: 1. 切り上げ; 2. 切り捨て; 3. 四捨五入</param>
        /// <returns>丸められた値</returns>
        private static decimal DoRound(decimal prmValue, int prmType)
        {
            decimal returnValue = 0;

            decimal sign = 1;
            if (prmValue < 0)
            {
                sign = -1;
            }

            switch (prmType)
            {
                case 1:
                    returnValue = Math.Ceiling(Math.Abs(prmValue)) * sign;
                    break;
                case 2:
                    returnValue = Math.Floor(Math.Abs(prmValue)) * sign;
                    break;
                case 3:
                    returnValue = Math.Round(Math.Abs(prmValue), 0, MidpointRounding.AwayFromZero) * sign;
                    break;
                default:
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// Decimalタイプに交換する
        /// </summary>
        /// <param name="prmValue">Decimalタイプに交換する対象</param>
        /// <returns>Decimalタイプの値</returns>
        public static decimal ConvertToDecimal(object prmValue)
        {
            if (prmValue == null || string.IsNullOrEmpty(prmValue.ToString()))
            {
                return 0;
            }
            decimal returnValue = 0;
            if (decimal.TryParse(prmValue.ToString(), out returnValue))
            {
                return returnValue;
            }
            return 0;
        }

        /// <summary>
        /// Intタイプに交換する
        /// </summary>
        /// <param name="prmValue">Intタイプに交換する対象</param>
        /// <returns>Intタイプの値</returns>
        public static int ConvertToInt(object prmValue)
        {
            if (prmValue == null || string.IsNullOrEmpty(prmValue.ToString()))
            {
                return 0;
            }
            int returnValue = 0;
            if (int.TryParse(prmValue.ToString(), out returnValue))
            {
                return returnValue;
            }
            return 0;
        }

        /// <summary>
        /// 単価のフォーマット
        /// </summary>
        /// <param name="prmValue">フォーマット対象</param>
        /// <returns>フォーマットされた文字列</returns>
        public static string FormatTanka(object prmValue)
        {
            if (string.IsNullOrEmpty(prmValue.ToString()))
            {
                return string.Empty;
            }
            var objSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllDataForCode(Const.CommonConst.SYS_ID);
            string format = "#,##0";
            if (objSysInfo != null && !string.IsNullOrEmpty(objSysInfo.SYS_TANKA_FORMAT))
            {
                format = objSysInfo.SYS_TANKA_FORMAT;
            }
            return string.Format("{0:" + format + "}", ConvertToDecimal(prmValue));
        }

        /// <summary>
        /// 数量のフォーマット
        /// </summary>
        /// <param name="prmValue">フォーマット対象</param>
        /// <returns>フォーマットされた文字列</returns>
        public static string FormatSuuryou(object prmValue)
        {
            if (string.IsNullOrEmpty(prmValue.ToString()))
            {
                return string.Empty;
            }
            var objSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllDataForCode(Const.CommonConst.SYS_ID);
            string format = "#,##0";
            if (objSysInfo != null && !string.IsNullOrEmpty(objSysInfo.SYS_SUURYOU_FORMAT))
            {
                format = objSysInfo.SYS_SUURYOU_FORMAT;
            }
            return string.Format("{0:" + format + "}", ConvertToDecimal(prmValue));
        }

        /// <summary>
        /// 金額のフォーマット
        /// </summary>
        /// <param name="prmValue">フォーマット対象</param>
        /// <returns>フォーマットされた文字列</returns>
        public static string FormatKingaku(object prmValue)
        {
            string format = "#,##0";
            return string.Format("{0:" + format + "}", ConvertToDecimal(prmValue));
        }        
    }
}
