using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using r_framework.Entity;
using r_framework.Dao;
using r_framework.Utility;

namespace r_framework.Logic
{
    public class FractionLogic
    {
        /// <summary>
        /// 端数コード種別
        /// </summary>
        public enum FractionSetting
        {
            CUSTOM = 0,

            // 取引先別請求消費税
            [Description("M_TORIHIKISAKI_SEIKYUU.TAX_HASUU_CD")]
            TORIHIKISAKI_SEIKYUU_TAX,

            // 取引先別請求金額
            [Description("M_TORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD")]
            TORIHIKISAKI_SEIKYUU_KINGAKU,
            
            // 取引先別支払消費税
            [Description("M_TORIHIKISAKI_SHIHARAI.TAX_HASUU_CD")]
            TORIHIKISAKI_SHIHARAI_TAX,
            
            // 取引先別支払金額
            [Description("M_TORIHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD")]
            TORIHIKISAKI_SHIHARAI_KINGAKU,

            // 引合取引先別請求消費税
            [Description("M_HIKIAI_TORIHIKISAKI_SEIKYUU.TAX_HASUU_CD")]
            HIKIAI_TORIHIKISAKI_SEIKYUU_TAX,

            // 引合取引先別請求金額
            [Description("M_HIKIAI_TORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD")]
            HIKIAI_TORIHIKISAKI_SEIKYUU_KINGAKU,
            
            // 引合取引先別支払消費税
            [Description("M_HIKIAI_TORIHIKISAKI_SHIHARAI.TAX_HASUU_CD")]
            HIKIAI_TORIHIKISAKI_SHIHARAI_TAX,

            // 引合取引先別支払金額
            [Description("M_HIKIAI_TORIHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD")]
            HIKIAI_TORIHIKISAKI_SHIHARAI_KINGAKU,

            // 受入情報割振値
            [Description("M_SYS_INFO.UKEIRE_WARIFURI_HASU_CD, M_SYS_INFO.UKEIRE_WARIFURI_HASU_KETA")]
            UKEIRE_WARFURI,

            // 受入情報割振割合
            [Description("M_SYS_INFO.UKEIRE_WARIFURI_WARIAI_HASU_CD, M_SYS_INFO.UKEIRE_WARIFURI_WARIAI_HASU_KETA")]
            UKEIRE_WARIFURI_WARIAI,

            // 受入情報調整値
            [Description("M_SYS_INFO.UKEIRE_CHOUSEI_HASU_CD, M_SYS_INFO.UKEIRE_CHOUSEI_HASU_KETA")]
            UKEIRE_CHOUSEI,

            // 受入情報調整割合
            [Description("M_SYS_INFO.UKEIRE_CHOUSEI_WARIAI_HASU_CD, M_SYS_INFO.UKEIRE_CHOUSEI_WARIAI_HASU_KETA")]
            UKEIRE_CHOUSEI_WARIAI,

            // 出荷情報割振値
            [Description("M_SYS_INFO.SHUKKA_WARIFURI_HASU_CD, M_SYS_INFO.SHUKKA_WARIFURI_HASU_KETA")]
            SHUKKA_WARIFURI,

            // 出荷情報割振割合
            [Description("M_SYS_INFO.SHUKKA_WARIFURI_WARIAI_HASU_CD, M_SYS_INFO.SHUKKA_WARIFURI_WARIAI_HASU_KETA")]
            SHUKKA_WARIFURI_WARIAI,

            // 出荷情報調整値
            [Description("M_SYS_INFO.SHUKKA_CHOUSEI_HASU_CD, M_SYS_INFO.SHUKKA_CHOUSEI_HASU_KETA")]
            SHUKKA_CHOUSEI,
            // 出荷情報調整割合
            [Description("M_SYS_INFO.SHUKKA_CHOUSEI_WARIAI_HASU_CD, M_SYS_INFO.SHUKKA_CHOUSEI_WARIAI_HASU_KETA")]
            SHUKKA_CHOUSEI_WARIAI
        }

        /// <summary>
        /// 端数処理種別
        /// </summary>
        public enum FractionType : int
        {
            CEILING = 1,  // 切り上げ
            FLOOR,		  // 切り捨て
            ROUND,		  // 四捨五入
        }

        /// <summary>
        /// 端数処理メインメソッド
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="fracType">端数処理種別</param>
        /// <param name="hasuuKeta">端数を表示する桁</param>
        /// <returns>端数処理値</returns>
        public static decimal FractionCalc(decimal value, FractionType fracType, int hasuuKeta = 0)
        {
            decimal retVal = 0;

            retVal = FractionLogic.Calc(value, fracType, hasuuKeta);

            return retVal;
        }

        /// <summary>
        /// 端数処理
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="fracSetting">端数コード種別</param>
        /// <param name="torihikisakiCD">取引先コード</param>
        /// <returns>端数処理済みの値</returns>
        public static decimal FractionCalc(decimal value, FractionSetting fracSetting, string torihikisakiCD)
        {
            decimal retVal = 0;

            FractionLogic instance = new FractionLogic();

            FractionType fracType;
            int hasuuketa;
            instance.GetFractionType(fracSetting, out fracType, out hasuuketa);

            retVal = FractionCalc(value, fracType, hasuuketa);

            return retVal;
        }

        /// <summary>
        /// プロパティに設定(FractionType、端数桁)
        /// </summary>
        /// <param name="fracType">端数処理種別</param>
        /// <param name="hasuuKeta">端数を表示する桁</param>
        public void SetFractionSetting(FractionType fracType, int hasuuKeta = 0)
        {
            this.fractionType = fracType;
            this.hasuuKeta = hasuuKeta;
        }

        /// <summary>
        /// プロパティに設定(FractionSetting、取引先CD、FractionType、端数桁)
        /// </summary>
        /// <param name="fracSetting">端数コード種別</param>
        /// <param name="torihikisakiCD">取引先CD</param>
        public void SetFractionSetting(FractionSetting fracSetting, string torihikisakiCD)
        {
            this.fractionSetting = fracSetting;
            this.torihikisakiCode = torihikisakiCD;
            
            // fractionType, hasuuKetaを取得し、設定
            FractionType fracType;
            int hasuuketa;
            GetFractionType(fracSetting, out fracType, out hasuuketa);

            this.fractionType = fracType;
            this.hasuuKeta = hasuuketa;
        }

        /// <summary>
        /// 端数処理
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>端数処理済みの値</returns>
        public decimal FractionCalc(decimal value)
        {
            decimal retVal = 0;

            retVal = FractionLogic.FractionCalc(value, this.fractionType, this.hasuuKeta);

            return retVal;
        }

        /// <summary>
        /// FractionSettingより検索対象のテーブル、カラム名を取得しGetFractionCdを呼ぶ
        /// </summary>
        /// <param name="fracSetting">端数コード種別</param>
        /// <param name="fracType">端数処理種別</param>
        /// <param name="hasuuKeta">端数を表示する桁</param>
        private void GetFractionType(FractionSetting fracSetting, out FractionType fracType, out int hasuuKeta)
        {
            fracType = 0;
            hasuuKeta = 0;

            // Descriptionを取得
            FieldInfo fi = fracSetting.GetType().GetField(fracSetting.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var desciptionString = attributes.Select(n => n.Description).FirstOrDefault();
            
            string description = desciptionString;
            string tableName = description.Split(",".ToCharArray())[0].Split(".".ToCharArray())[0];

            FractionType ft;
            int hasuuketa;
            GetFractionCd(tableName, description, out ft, out hasuuketa);

            fracType = ft;
            hasuuKeta = hasuuketa;
        }

        /// <summary>
        /// DBより端数CD、端数桁を取得
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="fracType">端数処理種別</param>
        /// <param name="hasuuketa">端数を表示する桁</param>
        private void GetFractionCd(string tableName, string columnName, out FractionType fracType, out int hasuuketa)
        {
            fracType = 0;
            hasuuketa = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT");
            sb.AppendFormat(" {0} ", columnName);
            sb.Append("FROM ");
            sb.AppendFormat("{0} ", tableName);
            
            if (!string.IsNullOrEmpty(this.torihikisakiCode))
            {
                sb.Append("WHERE ");
                sb.AppendFormat("{0}.TORIHIKISAKI_CD = '{1}'", tableName, this.torihikisakiCode);
            }

            DataTable dt = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>().GetDateForStringSql(sb.ToString());

            if (dt.Rows.Count != 0)
            {
                fracType = (FractionType)int.Parse(dt.Rows[0][0].ToString());
                
                if (columnName.Split(",".ToCharArray()).Length > 1)
                {
                    hasuuketa = int.Parse(dt.Rows[0][1].ToString()) - 1;
                }
            }
        }

        /// <summary>
        /// 指定された端数CD、端数桁数に従い、金額の端数処理を行う
        /// </summary>
        /// <param name="value">端数処理対象値</param>
        /// <param name="fracType">端数CD</param>
        /// <param name="hasuuKeta">端数を表示する桁</param>
        /// <returns name="decimal">端数処理後の値</returns>
        private static decimal Calc(decimal value, FractionType fracType, int hasuuKeta = 0)
        {
            decimal returnVal = 0;		// 戻り値
            double hasuKetaCoefficient = 1;
            decimal sign = 1;
            if (value < 0)
            {
                sign = -1;
            }

            // 10の0乗は1
            hasuKetaCoefficient = Math.Pow(10, hasuuKeta);

            value = Math.Abs(value);

            switch ((FractionLogic.FractionType)fracType)
            {
                case FractionLogic.FractionType.CEILING:
                    returnVal = Math.Ceiling(value * (decimal)hasuKetaCoefficient) / (decimal)hasuKetaCoefficient;
                    break;
                case FractionLogic.FractionType.FLOOR:
                    returnVal = Math.Floor(value * (decimal)hasuKetaCoefficient) / (decimal)hasuKetaCoefficient;
                    break;
                case FractionLogic.FractionType.ROUND:
                    returnVal = Math.Round(value * (decimal)hasuKetaCoefficient, 0, MidpointRounding.AwayFromZero) / (decimal)hasuKetaCoefficient;
                    break;
                default:
                    returnVal = value;
                    break;
            }

            returnVal = returnVal * sign;
            return returnVal;
        }

        public FractionSetting fractionSetting { get; set; }
        public FractionType fractionType { get; set; }
        public int hasuuKeta { get; set; }
        public string torihikisakiCode { get; set; }
    }
}
