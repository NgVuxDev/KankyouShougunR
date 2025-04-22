using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - ChouhyouOutKoumoku -

    /// <summary>帳票出力項目を表すクラス・コントロール</summary>
    public class ChouhyouOutKoumoku
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ChouhyouOutKoumoku" /> class.</summary>
        /// <param name="id">出力帳票ＩＤを表す数値</param>
        /// <param name="name">出力帳票名を表す文字列</param>
        /// <param name="tableName">テーブル名を表す文字列</param>
        /// <param name="fieldName">フィールド名を表す文字列</param>
        /// <param name="type">データータイプを表す文字列</param>
        /// <param name="outputFormat">出力フォーマットを表す文字列</param>
        /// <param name="outputAlignment">出力アライメントを表す文字列</param>
        public ChouhyouOutKoumoku(int id, string name, string tableName, string fieldName, Type type, string outputFormat, int outputAlignment, bool isTotalKubun)
        {
            // 出力帳票ＩＤ
            this.ID = id;

            // 出力帳票名
            this.Name = name;

            // テーブル名
            this.TableName = tableName;

            // フィールド名
            this.FieldName = fieldName;

            // データータイプ
            this.DataType = type;

            // 出力フォーマット
            this.OutputFormat = outputFormat;

            // 出力アライメント
            this.OutputAlignment = outputAlignment;

            // 集計時に合計するか否か
            this.IsTotalKubun = isTotalKubun;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>出力帳票ＩＤを保持するプロパティ</summary>
        public int ID { get; set; }

        /// <summary>出力帳票名を保持するプロパティ</summary>
        public string Name { get; set; }

        /// <summary>テーブル名を保持するプロパティ</summary>
        public string TableName { get; set; }

        /// <summary>フィールド名を保持するプロパティ</summary>
        public string FieldName { get; set; }

        /// <summary>データータイプを保持するプロパティ</summary>
        public Type DataType { get; set; }

        /// <summary>出力フォーマットを保持するプロパティ</summary>
        public string OutputFormat { get; set; }

        /// <summary>出力アライメントを保持するプロパティ</summary>
        public int OutputAlignment { get; set; }

        /// <summary>集計時に合計するか否かを保持するプロパティ</summary>
        public bool IsTotalKubun { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>参照すべきテーブル・フィールド名の取得処理を実行する</summary>
        /// <param name="tableName">基準となるテーブル名を表す文字列</param>
        /// <param name="tableNameRef">参照すべきテーブル名を表す文字列</param>
        /// <param name="fieldName">参照すべきフィールド名を表す文字列</param>
        /// <param name="fieldNameRyaku">参照すべきフィールド略名を表す文字列</param>
        public void GetTableAndFieldNameRef(string tableName, ref string tableNameRef, ref string fieldName, ref string fieldNameRyaku)
        {
            try
            {
                tableNameRef = this.TableName;
                fieldName = this.FieldName;

                switch (tableName)
                {
                    case "T_UKEIRE_ENTRY":          // 受入入力

                        #region - 受入入力 -

                        if (this.ID == 4)
                        {   // 拠点CD
                            tableNameRef = "M_KYOTEN";
                            fieldName = "KYOTEN_CD";
                            fieldNameRyaku = "KYOTEN_NAME_RYAKU";
                        }
                        else if (this.ID == 6)
                        {   // 受入番号
                            tableNameRef = tableName;
                            fieldName = "UKEIRE_NUMBER";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 40)
                        {   // 形態区分CD
                            tableNameRef = "M_KEITAI_KBN";
                            fieldName = "KEITAI_KBN_CD";
                            fieldNameRyaku = "KEITAI_KBN_NAME_RYAKU";
                        }
                        else if (this.ID == 42)
                        {   // コンテナ操作CD
                            tableNameRef = "M_CONTENA_SOUSA";
                            fieldName = "CONTENA_SOUSA_CD";
                            fieldNameRyaku = "CONTENA_SOUSA_NAME_RYAKU";
                        }
                        else if (this.ID == 43)
                        {   // マニフェスト種類CD
                            tableNameRef = "M_MANIFEST_SHURUI";
                            fieldName = "MANIFEST_SHURUI_CD";
                            fieldNameRyaku = "MANIFEST_SHURUI_NAME_RYAKU";
                        }
                        else if (this.ID == 44)
                        {   // マニフェスト手配CD
                            tableNameRef = "M_MANIFEST_TEHAI";
                            fieldName = "MANIFEST_TEHAI_CD";
                            fieldNameRyaku = "MANIFEST_TEHAI_NAME_RYAKU";
                        }
                        else if (this.ID == 52)
                        {   // 売上金額合計
                            tableNameRef = tableName;
                            fieldName = "URIAGE_KINGAKU_TOTAL";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 61)
                        {   // 支払金額合計
                            tableNameRef = tableName;
                            fieldName = "SHIHARAI_KINGAKU_TOTAL";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 69)
                        {   // 売上税計算区分CD
                            //tableNameRef = "M_URIAGE_ZEI_KEISAN_KBN";
                            //fieldName = "URIAGE_ZEI_KEISAN_KBN_CD";
                            //fieldNameRyaku = "MANIFEST_TEHAI_NAME_RYAKU";
                            //fieldNameRyaku = "";
                        }
                        else if (this.ID == 70)
                        {   // 売上税区分CD
                            tableNameRef = "M_URIAGE_ZEI_KBN";
                            fieldName = "URIAGE_ZEI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 71)
                        {   // 売上税計算区分CD
                            tableNameRef = "M_URIAGE_TORIHIKI_KBN";
                            fieldName = "URIAGE_TORIHIKI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 72)
                        {   // 支払税計算区分CD
                            tableNameRef = "M_SHIHARAI_ZEI_KEISAN_KBN";
                            fieldName = "SHIHARAI_ZEI_KEISAN_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 73)
                        {   // 支払税区分CD
                            tableNameRef = "M_SHIHARAI_ZEI_KBN";
                            fieldName = "SHIHARAI_ZEI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 74)
                        {   // 支払取引区分CD
                            tableNameRef = "M_SHIHARAI_TORIHIKI_KBN";
                            fieldName = "SHIHARAI_TORIHIKI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 104)
                        {   // 容器CD
                            tableNameRef = "M_YOUKI";
                            fieldName = "YOUKI_CD";
                            fieldNameRyaku = "YOUKI_NAME_RYAKU";
                        }
                        else if (this.ID == 125)
                        {   // 荷姿単位CD
                            tableNameRef = "M_NISUGATA";
                            fieldName = "NISUGATA_UNIT_CD";
                            fieldNameRyaku = "NISUGATA_NAME_RYAKU";
                        }

                        #endregion - 受入入力 -

                        break;
                    case "T_UKEIRE_DETAIL":         // 受入明細

                        #region - 受入明細 -

                        if (this.ID == 94)
                        {   // 受入番号
                            tableNameRef = tableName;
                            fieldName = "UKEIRE_NUMBER";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 104)
                        {   // 容器CD
                            tableNameRef = "M_YOUKI";
                            fieldName = "YOUKI_CD";
                            fieldNameRyaku = "YOUKI_NAME_RYAKU";
                        }
                        else if (this.ID == 107)
                        {   // 伝票区分CD
                            tableNameRef = "M_DENPYOU_KBN";
                            fieldName = "DENPYOU_KBN_CD";
                            fieldNameRyaku = "DENPYOU_KBN_NAME_RYAKU";
                        }
                        else if (this.ID == 114)
                        {   // 単位CD
                            tableNameRef = "M_UNIT";
                            fieldName = "UNIT_CD";
                            fieldNameRyaku = "UNIT_NAME_RYAKU";
                        }
                        else if (this.ID == 119)
                        {   // 品名別税区分CD
                            tableNameRef = "M_HINMEI_ZEI_KBN";
                            fieldName = "HINMEI_ZEI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 125)
                        {   // 荷姿単位CD
                            tableNameRef = "M_NISUGATA";
                            fieldName = "NISUGATA_UNIT_CD";
                            fieldNameRyaku = "NISUGATA_NAME_RYAKU";
                        }

                        #endregion - 受入明細 -

                        break;
                    case "T_SHUKKA_ENTRY":          // 出荷入力

                        #region - 出荷入力 -

                        if (this.ID == 4)
                        {   // 拠点CD
                            tableNameRef = "M_KYOTEN";
                            fieldName = "KYOTEN_CD";
                            fieldNameRyaku = "KYOTEN_NAME_RYAKU";
                        }
                        else if (this.ID == 6)
                        {   // 出荷番号
                            tableNameRef = tableName;
                            fieldName = "SHUKKA_NUMBER";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 40)
                        {   // 形態区分CD
                            tableNameRef = "M_KEITAI_KBN";
                            fieldName = "KEITAI_KBN_CD";
                            fieldNameRyaku = "KEITAI_KBN_NAME_RYAKU";
                        }
                        else if (this.ID == 42)
                        {   // コンテナ操作CD
                            tableNameRef = "M_CONTENA_SOUSA";
                            fieldName = "CONTENA_SOUSA_CD";
                            fieldNameRyaku = "CONTENA_SOUSA_NAME_RYAKU";
                        }
                        else if (this.ID == 43)
                        {   // マニフェスト種類CD
                            tableNameRef = "M_MANIFEST_SHURUI";
                            fieldName = "MANIFEST_SHURUI_CD";
                            fieldNameRyaku = "MANIFEST_SHURUI_NAME_RYAKU";
                        }
                        else if (this.ID == 44)
                        {   // マニフェスト手配CD
                            tableNameRef = "M_MANIFEST_TEHAI";
                            fieldName = "MANIFEST_TEHAI_CD";
                            fieldNameRyaku = "MANIFEST_TEHAI_NAME_RYAKU";
                        }
                        else if (this.ID == 52)
                        {   // 売上金額合計
                            tableNameRef = tableName;
                            fieldName = "URIAGE_AMOUNT_TOTAL";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 61)
                        {   // 支払金額合計
                            tableNameRef = tableName;
                            fieldName = "SHIHARAI_AMOUNT_TOTAL";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 69)
                        {   // 売上税計算区分CD
                            tableNameRef = "M_URIAGE_ZEI_KEISAN_KBN";
                            fieldName = "URIAGE_ZEI_KEISAN_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 70)
                        {   // 売上税区分CD
                            tableNameRef = "M_URIAGE_ZEI_KBN";
                            fieldName = "URIAGE_ZEI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 71)
                        {   // 売上税計算区分CD
                            tableNameRef = "M_URIAGE_TORIHIKI_KBN";
                            fieldName = "URIAGE_TORIHIKI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 72)
                        {   // 支払税計算区分CD
                            tableNameRef = "M_SHIHARAI_ZEI_KEISAN_KBN";
                            fieldName = "SHIHARAI_ZEI_KEISAN_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 73)
                        {   // 支払税区分CD
                            tableNameRef = "M_SHIHARAI_ZEI_KBN";
                            fieldName = "SHIHARAI_ZEI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 74)
                        {   // 支払取引区分CD
                            tableNameRef = "M_SHIHARAI_TORIHIKI_KBN";
                            fieldName = "SHIHARAI_TORIHIKI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 104)
                        {   // 容器CD
                            tableNameRef = "M_YOUKI";
                            fieldName = "YOUKI_CD";
                            fieldNameRyaku = "YOUKI_NAME_RYAKU";
                        }
                        else if (this.ID == 125)
                        {   // 荷姿単位CD
                            tableNameRef = "M_NISUGATA";
                            fieldName = "NISUGATA_UNIT_CD";
                            fieldNameRyaku = "NISUGATA_NAME_RYAKU";
                        }

                        #endregion - 出荷入力 -

                        break;
                    case "T_SHUKKA_DETAIL":         // 出荷明細

                        #region - 出荷明細 -

                        if (this.ID == 94)
                        {   // 出荷番号
                            tableNameRef = tableName;
                            fieldName = "SHUKKA_NUMBER";
                        }
                        else if (this.ID == 104)
                        {   // 容器CD
                            tableNameRef = "M_YOUKI";
                            fieldName = "YOUKI_CD";
                        }
                        else if (this.ID == 107)
                        {   // 伝票区分CD
                            tableNameRef = "M_DENPYOU_KBN";
                            fieldName = "DENPYOU_KBN_CD";
                        }
                        else if (this.ID == 114)
                        {   // 単位CD
                            tableNameRef = "M_UNIT";
                            fieldName = "UNIT_CD";
                        }
                        else if (this.ID == 119)
                        {   // 品名別税区分CD
                            tableNameRef = "M_HINMEI_ZEI_KBN";
                            fieldName = "HINMEI_ZEI_KBN_CD";
                        }
                        else if (this.ID == 125)
                        {   // 荷姿単位CD
                            tableNameRef = "M_NISUGATA";
                            fieldName = "NISUGATA_UNIT_CD";
                            fieldNameRyaku = "NISUGATA_NAME_RYAKU";
                        }

                        #endregion - 出荷明細 -

                        break;
                    case "T_NYUUKIN_ENTRY":         // 入金入力

                        #region - 入金入力 -

                        if (this.ID == 3)
                        {   // 拠点CD
                            tableNameRef = "M_KYOTEN";
                            fieldName = "KYOTEN_CD";
                            fieldNameRyaku = "KYOTEN_NAME_RYAKU";
                        }
                        else if (this.ID == 7)
                        {   // 取引先CD
                            tableNameRef = "M_TORIHIKISAKI";
                            fieldName = "TORIHIKISAKI_CD";
                            fieldNameRyaku = "TORIHIKISAKI_NAME_RYAKU";
                        }
                        else if (this.ID == 8)
                        {   // 入金先CD
                            tableNameRef = "M_NYUUKINSAKI";
                            fieldName = "NYUUKINSAKI_CD";
                            fieldNameRyaku = "NYUUKINSAKI_NAME_RYAKU";
                        }
                        else if (this.ID == 9)
                        {   // 銀行CD
                            tableNameRef = "M_BANK";
                            fieldName = "BANK_CD";
                            fieldNameRyaku = "BANK_NAME_RYAKU";
                        }
                        else if (this.ID == 10)
                        {   // 銀行支店CD
                            tableNameRef = "M_BANK_SHITEN";
                            fieldName = "BANK_SHITEN_CD";
                            fieldNameRyaku = "BANK_SHIETN_NAME_RYAKU";
                        }
                        //else if (this.ID == 14)
                        //{   // 営業担当者(社員)CD
                        //    tableNameRef = "M_SHAIN";
                        //    fieldName = "SHAIN_CD";
                        //    fieldNameRyaku = "SHAIN_NAME_RYAKU";
                        //}

                        #endregion - 入金入力 -

                        break;
                    case "T_NYUUKIN_DETAIL":        // 入金明細

                        #region - 入金明細 -

                        if (this.ID == 33)
                        {   // 入出金区分CD
                            tableNameRef = "M_NYUUSHUKKIN_KBN";
                            fieldName = "NYUUSHUKKIN_KBN_CD";
                            fieldNameRyaku = "NYUUSHUKKIN_KBN_NAME_RYAKU";
                        }

                        #endregion - 入金明細 -

                        break;
                    case "T_SHUKKIN_ENTRY":         // 出金入力

                        #region - 出金入力 -

                        if (this.ID == 3)
                        {   // 拠点CD
                            tableNameRef = "M_KYOTEN";
                            fieldName = "KYOTEN_CD";
                            fieldNameRyaku = "KYOTEN_NAME_RYAKU";
                        }
                        else if (this.ID == 7)
                        {   // 取引先CD
                            tableNameRef = "M_TORIHIKISAKI";
                            fieldName = "TORIHIKISAKI_CD";
                            fieldNameRyaku = "TORIHIKISAKI_NAME_RYAKU";
                        }
                        else if (this.ID == 8)
                        {   // 出金先CD
                            tableNameRef = "M_SHUKKINSAKI";
                            fieldName = "SHUKKINSAKI_CD";
                            fieldNameRyaku = "SHUKKINSAKI_NAME_RYAKU";
                        }
                        //else if (this.ID == 9)
                        //{   // 営業担当者(社員)CD
                        //    tableNameRef = "M_SHAIN";
                        //    fieldName = "SHAIN_CD";
                        //    fieldNameRyaku = "SHAIN_NAME_RYAKU";
                        //}

                        #endregion - 出金入力 -

                        break;
                    case "T_SHUKKIN_DETAIL":        // 出金明細

                        #region - 出金明細 -

                        if (this.ID == 25)
                        {   // 入出金区分CD
                            tableNameRef = "M_NYUUSHUKKIN_KBN";
                            fieldName = "NYUUSHUKKIN_KBN_CD";
                            fieldNameRyaku = "NYUUSHUKKIN_KBN_NAME_RYAKU";
                        }
                        else if (this.ID == 33)
                        {   // 入出金区分CD
                            tableNameRef = "M_NYUUSHUKKIN_KBN";
                            fieldName = "NYUUSHUKKIN_KBN_CD";
                            fieldNameRyaku = "NYUUSHUKKIN_KBN_NAME_RYAKU";
                        }

                        #endregion - 出金明細 -

                        break;
                    case "T_SEIKYUU_DENPYOU":       // 請求伝票

                        #region - 請求伝票 -

                        if (this.ID == 3)
                        {   // 拠点CD
                            tableNameRef = "M_KYOTEN";
                            fieldName = "KYOTEN_CD";
                            fieldNameRyaku = "KYOTEN_NAME_RYAKU";
                        }
                        else if (this.ID == 5)
                        {   // 取引先CD
                            tableNameRef = "M_TORIHIKISAKI";
                            fieldName = "TORIHIKISAKI_CD";
                            fieldNameRyaku = "TORIHIKISAKI_NAME_RYAKU";
                        }

                        #endregion - 請求伝票 -

                        break;
                    case "T_SEISAN_DENPYOU":        // 精算伝票

                        #region - 精算伝票 -

                        if (this.ID == 3)
                        {   // 拠点CD
                            tableNameRef = "M_KYOTEN";
                            fieldName = "KYOTEN_CD";
                            fieldNameRyaku = "KYOTEN_NAME_RYAKU";
                        }
                        else if (this.ID == 4)
                        {   // 取引先CD
                            tableNameRef = "M_TORIHIKISAKI";
                            fieldName = "TORIHIKISAKI_CD";
                            fieldNameRyaku = "TORIHIKISAKI_NAME_RYAKU";
                        }

                        #endregion - 精算伝票 -

                        break;
                    case "T_UR_SH_ENTRY":           // 売上／支払入力

                        #region - 売上／支払入力 -

                        if (this.ID == 4)
                        {   // 拠点CD
                            tableNameRef = "M_KYOTEN";
                            fieldName = "KYOTEN_CD";
                            fieldNameRyaku = "KYOTEN_NAME_RYAKU";
                        }
                        else if (this.ID == 6)
                        {   // 売上／支払番号
                            tableNameRef = tableName;
                            fieldName = "UR_SH_NUMBER";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 40)
                        {   // 形態区分CD
                            tableNameRef = "M_KEITAI_KBN";
                            fieldName = "KEITAI_KBN_CD";
                            fieldNameRyaku = "KEITAI_KBN_NAME_RYAKU";
                        }
                        else if (this.ID == 42)
                        {   // コンテナ操作CD
                            tableNameRef = "M_CONTENA_SOUSA";
                            fieldName = "CONTENA_SOUSA_CD";
                            fieldNameRyaku = "CONTENA_SOUSA_NAME_RYAKU";
                        }
                        else if (this.ID == 43)
                        {   // マニフェスト種類CD
                            tableNameRef = "M_MANIFEST_SHURUI";
                            fieldName = "MANIFEST_SHURUI_CD";
                            fieldNameRyaku = "MANIFEST_SHURUI_NAME_RYAKU";
                        }
                        else if (this.ID == 44)
                        {   // マニフェスト手配CD
                            tableNameRef = "M_MANIFEST_TEHAI";
                            fieldName = "MANIFEST_TEHAI_CD";
                            fieldNameRyaku = "MANIFEST_TEHAI_NAME_RYAKU";
                        }
                        else if (this.ID == 52)
                        {   // 売上金額合計
                            tableNameRef = tableName;
                            fieldName = "URIAGE_AMOUNT_TOTAL";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 61)
                        {   // 支払金額合計
                            tableNameRef = tableName;
                            fieldName = "SHIHARAI_AMOUNT_TOTAL";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 69)
                        {   // 売上税計算区分CD
                            tableNameRef = "M_URIAGE_ZEI_KEISAN_KBN";
                            fieldName = "URIAGE_ZEI_KEISAN_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 70)
                        {   // 売上税区分CD
                            tableNameRef = "M_URIAGE_ZEI_KBN";
                            fieldName = "URIAGE_ZEI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 71)
                        {   // 売上税計算区分CD
                            tableNameRef = "M_URIAGE_TORIHIKI_KBN";
                            fieldName = "URIAGE_TORIHIKI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 72)
                        {   // 支払税計算区分CD
                            tableNameRef = "M_SHIHARAI_ZEI_KEISAN_KBN";
                            fieldName = "SHIHARAI_ZEI_KEISAN_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 73)
                        {   // 支払税区分CD
                            tableNameRef = "M_SHIHARAI_ZEI_KBN";
                            fieldName = "SHIHARAI_ZEI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 74)
                        {   // 支払取引区分CD
                            tableNameRef = "M_SHIHARAI_TORIHIKI_KBN";
                            fieldName = "SHIHARAI_TORIHIKI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 104)
                        {   // 容器CD
                            tableNameRef = "M_YOUKI";
                            fieldName = "YOUKI_CD";
                            fieldNameRyaku = "YOUKI_NAME_RYAKU";
                        }
                        else if (this.ID == 125)
                        {   // 荷姿単位CD
                            tableNameRef = "M_NISUGATA";
                            fieldName = "NISUGATA_UNIT_CD";
                            fieldNameRyaku = "NISUGATA_NAME_RYAKU";
                        }

                        #endregion - 売上／支払入力 -

                        break;
                    case "T_UR_SH_DETAIL":          // 売上／支払明細

                        #region - 売上／支払明細 -

                        if (this.ID == 94)
                        {   // 売上／支払番号
                            tableNameRef = tableName;
                            fieldName = "UR_SH_NUMBER";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 107)
                        {   // 伝票区分CD
                            tableNameRef = "M_DENPYOU_KBN";
                            fieldName = "DENPYOU_KBN_CD";
                            fieldNameRyaku = "DENPYOU_KBN_NAME_RYAKU";
                        }
                        else if (this.ID == 114)
                        {   // 単位CD
                            tableNameRef = "M_UNIT";
                            fieldName = "UNIT_CD";
                            fieldNameRyaku = "UNIT_NAME_RYAKU";
                        }
                        else if (this.ID == 119)
                        {   // 品名別税区分CD
                            tableNameRef = "M_HINMEI_ZEI_KBN";
                            fieldName = "HINMEI_ZEI_KBN_CD";
                            fieldNameRyaku = string.Empty;
                        }
                        else if (this.ID == 125)
                        {   // 荷姿単位CD
                            tableNameRef = "M_NISUGATA_UNIT";
                            fieldName = "NISUGATA_UNIT_CD";
                            fieldNameRyaku = "NISUGATA_UNIT_RYAKU";
                        }

                        #endregion - 売上／支払明細 -

                        break;

                    case "T_UKETSUKE_SS_ENTRY":     // 受付収集

                        #region - 受付収集 -

                        #endregion - 受付収集 -

                        break;

                    case "T_UKETSUKE_SS_DETAIL":    // 受付収集明細

                        #region - 受付収集明細 -

                        #endregion - 受付収集明細 -

                        break;

                    case "T_UKETSUKE_SK_ENTRY":     // 受付出荷

                        #region - 受付出荷 -

                        #endregion - 受付出荷 -

                        break;

                    case "T_UKETSUKE_SK_DETAIL":    // 受付出荷明細

                        #region - 受付出荷明細 -

                        #endregion - 受付出荷明細 -

                        break;
                    case "T_UKETSUKE_MK_ENTRY":     // 受付持込

                        #region - 受付持込 -

                        #endregion - 受付持込 -

                        break;

                    case "T_UKETSUKE_MK_DETAIL":    // 受付持込明細

                        #region - 受付持込明細 -

                        #endregion - 受付持込明細 -

                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - ChouhyouOutKoumoku -
}
