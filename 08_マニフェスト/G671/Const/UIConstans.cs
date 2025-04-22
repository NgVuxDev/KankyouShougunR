using System;
﻿using System.Collections.Generic;
namespace Shougun.Core.PaperManifest.ManifestImport.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class UIConstans
    {
        /// <summary>
        /// Button設定用XMLファイルパス
        /// </summary>
        internal static readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestImport.Setting.ButtonSetting.xml";

        /// <summary>必須項目（直行）</summary>
        public static readonly List<int> ListHissuColumnIndexChokko = new List<int>() { 0, 2, 3, 4, 5, 93, 94 };
        /// <summary>必須項目（積替）</summary>
        public static readonly List<int> ListHissuColumnIndexTsumikae = new List<int>() { 0, 2, 3, 4, 5, 158, 159 };
        /// <summary>必須項目（建廃）</summary>
        public static readonly List<int> ListHissuColumnIndexKenpai = new List<int>() { 0, 2, 3, 4, 5, 154, 155 };
        /// <summary>必須項目（紐付）</summary>
        public static readonly List<int> ListHissuColumnIndexHimoduke = new List<int>() { 0, 1, 2, 4, 5, 6 };

        /// <summary>処分業者</summary>
        internal const string SHOBUN_NIOROSHI_GYOUSHA_KBN = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
        /// <summary>最終処分場所</summary>
        internal const string SAISHUU_SHOBUNJOU_KBN = "SAISHUU_SHOBUNJOU_KBN";
        /// <summary>排出事業者</summary>
        internal const string HAISHUTSU_NIZUMI_GYOUSHA_KBN = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
        /// <summary>排出事業場</summary>
        internal const string HAISHUTSU_NIZUMI_GENBA_KBN = "HAISHUTSU_NIZUMI_GENBA_KBN";
        /// <summary>運搬の受託者</summary>
        internal const string UNPAN_JUTAKUSHA_KAISHA_KBN = "UNPAN_JUTAKUSHA_KAISHA_KBN";
        /// <summary>積替保管</summary>
        internal const string TSUMIKAEHOKAN_KBN = "TSUMIKAEHOKAN_KBN";

        /// <summary>廃棄物区分 1:直行</summary>
        internal const Int16 HAIKI_KBN_CHOKKOU = 1;
        /// <summary>廃棄物区分 3:積替</summary>
        internal const Int16 HAIKI_KBN_TUMIKAE = 3;
        /// <summary>廃棄物区分:2:建廃 </summary>
        internal const Int16 HAIKI_KBN_KENPAI = 2;
        /// <summary>廃棄物区分:4:電子 </summary>
        internal const Int16 HAIKI_KBN_DENSHI = 4;

        /// <summary>マニフェスト種別 1:直行</summary>
        internal const String MANI_SBT_CHOKKOU = "1";
        /// <summary>マニフェスト種別 2:積替</summary>
        internal const String MANI_SBT_TUMIKAE = "2";
        /// <summary>マニフェスト種別 3:建廃 </summary>
        internal const String MANI_SBT_KENPAI = "3";
        /// <summary>マニフェスト種別 4:紐付 </summary>
        internal const String MANI_SBT_HIMODUKE = "4";

        /// <summary>閾値</summary>
        public static readonly Int16 LIMIT_NUMBER = 120;

        /// <summary>全角スペース</summary>
        public static readonly string ZENKAKU_SPACE = "　";

        /// <summary>ヘッダ定義（直行）</summary>
        public static readonly string[] ListManiChokkoHeader = { "拠点CD", "拠点名", "一次マニフェスト区分", "交付番号区分", "交付番号", "行数", "交付年月日", "整理番号", "交付担当者", "排出事業者CD", "排出事業者名称", "排出事業者郵便番号", "排出事業者電話番号", "排出事業者住所", "排出事業場CD", "排出事業場名称", "排出事業場郵便番号", "排出事業場電話番号", "排出事業場住所", "中間処理産業廃棄物フラグ", "中間処理産業廃棄物", "最終処分の場所(予定)フラグ", "最終処分の場所(予定)業者CD", "最終処分の場所(予定)現場CD", "最終処分の場所(予定)現場名称", "最終処分の場所(予定)郵便番号", "最終処分の場所(予定)電話番号", "最終処分の場所(予定)住所", "運搬受託者CD", "運搬受託者名称", "運搬受託者郵便番号", "運搬受託者電話番号", "運搬受託者住所", "運搬方法CD", "運搬方法名", "車輌CD", "車輌名", "車種CD", "車種名", "運搬先の事業者CD(処分業者CD)", "運搬先の事業場CD", "運搬先の事業場名称", "運搬先の事業場郵便番号", "運搬先の事業場電話番号", "運搬先の事業場住所", "処分受託者CD", "処分受託者名称", "処分受託者郵便番号", "処分受託者電話番号", "処分受託者住所", "積替保管業者CD", "積替保管業者名称", "積替保管現場CD", "積替保管場名称", "積替保管場郵便番号", "積替保管場電話番号", "積替保管場住所", "有害物質CD", "有害物質名", "備考", "運搬の受託者CD", "運搬の受託者名称", "運転者CD", "運転者名", "運搬終了年月日", "有価物拾集量", "有価物拾集量単位CD", "有価物拾集量単位名", "処分の受託者CD", "処分の受託者名称", "処分担当者CD", "処分担当者名", "最終処分業者CD", "最終処分現場CD", "最終処分場名称", "最終処分場郵便番号", "最終処分場電話番号", "最終処分場住所", "処分先No", "照合確認B2票", "照合確認D票", "照合確認E票", "返却日A票", "返却日B1票", "返却日B2票", "返却日C1票", "返却日C2票", "返却日D票", "返却日E票", "斜線項目有害物質", "斜線項目備考", "斜線項目中間処理産業廃棄物", "斜線項目積替保管", "削除フラグ", "行番号", "廃棄物種類CD", "廃棄物種類名", "廃棄物の名称CD", "廃棄物名称", "荷姿CD", "荷姿名", "数量", "単位CD", "単位名", "換算後数量", "処分方法CD", "処分方法名", "処分終了年月日", "最終処分業者CD", "最終処分業者名", "最終処分場所CD", "最終処分場所名", "最終処分終了年月日" };
        /// <summary>ヘッダ定義（積替）</summary>
        public static readonly string[] ListManiTsumikaeHeader = { "拠点CD", "拠点名", "一次マニフェスト区分", "交付番号区分", "交付番号", "行数", "交付年月日", "整理番号", "交付担当者", "排出事業者CD", "排出事業者名称", "排出事業者郵便番号", "排出事業者電話番号", "排出事業者住所", "排出事業場CD", "排出事業場名称", "排出事業場郵便番号", "排出事業場電話番号", "排出事業場住所", "中間処理産業廃棄物フラグ", "中間処理産業廃棄物", "最終処分の場所(予定)フラグ", "最終処分の場所(予定)業者CD", "最終処分の場所(予定)現場CD", "最終処分の場所(予定)現場名称", "最終処分の場所(予定)郵便番号", "最終処分の場所(予定)電話番号", "最終処分の場所(予定)住所", "(区間1)運搬受託者CD", "(区間1)運搬受託者名称", "(区間1)運搬受託者郵便番号", "(区間1)運搬受託者電話番号", "(区間1)運搬受託者住所", "(区間1)車輌CD", "(区間1)車輌名", "(区間1)車種CD", "(区間1)車種名", "(区間1)運搬方法CD", "(区間1)運搬方法名", "(区間1)運搬先区分", "(区間1)運搬先の事業者CD", "(区間1)運搬先の事業場CD", "(区間1)運搬先の事業場名称", "(区間1)運搬先の事業場郵便番号", "(区間1)運搬先の事業場電話番号", "(区間1)運搬先の事業場住所", "(区間2)運搬受託者CD", "(区間2)運搬受託者名称", "(区間2)運搬受託者郵便番号", "(区間2)運搬受託者電話番号", "(区間2)運搬受託者住所", "(区間2)車輌CD", "(区間2)車輌名", "(区間2)車種CD", "(区間2)車種名", "(区間2)運搬方法CD", "(区間2)運搬方法名", "(区間2)運搬先区分", "(区間2)運搬先の事業者CD", "(区間2)運搬先の事業場CD", "(区間2)運搬先の事業場名称", "(区間2)運搬先の事業場郵便番号", "(区間2)運搬先の事業場電話番号", "(区間2)運搬先の事業場住所", "(区間3)運搬受託者CD", "(区間3)運搬受託者名称", "(区間3)運搬受託者郵便番号", "(区間3)運搬受託者電話番号", "(区間3)運搬受託者住所", "(区間3)車輌CD", "(区間3)車輌名", "(区間3)車種CD", "(区間3)車種名", "(区間3)運搬方法CD", "(区間3)運搬方法名", "(区間3)運搬先区分", "(区間3)運搬先の事業者CD", "(区間3)運搬先の事業場CD", "(区間3)運搬先の事業場名称", "(区間3)運搬先の事業場郵便番号", "(区間3)運搬先の事業場電話番号", "(区間3)運搬先の事業場住所", "処分受託者CD", "処分受託者名称", "処分受託者郵便番号", "処分受託者電話番号", "処分受託者住所", "積替保管業者CD", "積替保管業者名称", "積替保管現場CD", "積替保管場名称", "積替保管場郵便番号", "積替保管場電話番号", "積替保管場住所", "有害物質CD", "有害物質名", "(区間1)運搬の受託者CD", "(区間1)運搬の受託者名称", "(区間1)運転者CD", "(区間1)運転者名", "(区間1)運搬終了年月日", "(区間1)有価物拾集量", "(区間1)有価物拾集量単位CD", "(区間1)有価物拾集量単位名", "(区間2)運搬の受託者CD", "(区間2)運搬の受託者名称", "(区間2)運転者CD", "(区間2)運転者名", "(区間2)運搬終了年月日", "(区間2)有価物拾集量", "(区間2)有価物拾集量単位CD", "(区間2)有価物拾集量単位名", "(区間3)運搬の受託者CD", "(区間3)運搬の受託者名称", "(区間3)運転者CD", "(区間3)運転者名", "(区間3)運搬終了年月日", "(区間3)有価物拾集量", "(区間3)有価物拾集量単位CD", "(区間3)有価物拾集量単位名", "処分の受託者CD", "処分の受託者名称", "処分担当者CD", "処分担当者名", "最終処分業者CD", "最終処分現場CD", "最終処分場名称", "最終処分場郵便番号", "最終処分場電話番号", "最終処分場住所", "処分先No", "備考", "照合確認B2票", "照合確認B4票", "照合確認B6票", "照合確認D票", "照合確認E票", "返却日A票", "返却日B1票", "返却日B2票", "返却日B4票", "返却日B6票", "返却日C1票", "返却日C2票", "返却日D票", "返却日E票", "斜線項目有害物質", "斜線項目中間処理産業廃棄物", "斜線項目運搬受託者2", "斜線項目運搬の受託者2", "斜線項目運搬受託者3", "斜線項目運搬の受託者3", "斜線項目積替保管", "斜線項目運搬先事業場2", "斜線項目運搬先事業場3", "斜線項目備考", "斜線項目B4票", "斜線項目B6票", "削除フラグ", "行番号", "廃棄物種類CD", "廃棄物種類名", "廃棄物の名称CD", "廃棄物名称", "荷姿CD", "荷姿名", "数量", "単位CD", "単位名", "換算後数量", "処分方法CD", "処分方法名", "処分終了年月日", "最終処分業者CD", "最終処分業者名", "最終処分場所CD", "最終処分場所名", "最終処分終了年月日" };
        /// <summary>ヘッダ定義（建廃）</summary>
        public static readonly string[] ListManiKenpaiHeader = { "拠点CD", "拠点名", "一次マニフェスト区分", "交付番号区分", "交付番号", "行数", "交付年月日", "整理番号", "交付担当者所属", "交付担当者氏名", "事前協議番号", "事前協議年月日", "排出事業者CD", "排出事業者名称", "排出事業者郵便番号", "排出事業者電話番号", "排出事業者住所", "排出事業場CD", "排出事業場名称", "排出事業場郵便番号", "排出事業場電話番号", "排出事業場住所", "中間処理産業廃棄物フラグ", "中間処理産業廃棄物", "最終処分の場所(予定)フラグ", "最終処分の場所(予定)業者CD", "最終処分の場所(予定)現場CD", "最終処分の場所(予定)現場名称", "最終処分の場所(予定)住所", "形状1", "形状2", "形状3", "形状4", "形状4CD", "形状5", "形状5CD", "形状6", "形状6CD", "形状7", "形状7CD", "荷姿1", "荷姿2", "荷姿3", "荷姿4", "荷姿5", "荷姿5CD", "荷姿6", "荷姿6CD", "荷姿7", "荷姿7CD", "(区間1)運搬受託者CD", "(区間1)運搬受託者名称", "(区間1)運搬受託者郵便番号", "(区間1)運搬受託者電話番号", "(区間1)運搬受託者住所", "(区間1)車輌CD", "(区間1)車輌名称", "(区間1)車種CD", "(区間1)車種名称", "(区間1)積替・保管有無", "(区間1)運搬方法CD", "(区間2)運搬受託者CD", "(区間2)運搬受託者名称", "(区間2)運搬受託者郵便番号", "(区間2)運搬受託者電話番号", "(区間2)運搬受託者住所", "(区間2)車輌CD", "(区間2)車輌名称", "(区間2)車種CD", "(区間2)車種名称", "(区間2)積替・保管有無", "(区間2)運搬方法CD", "処分受託者CD", "処分受託者名称", "処分受託者郵便番号", "処分受託者電話番号", "処分受託者住所", "運搬先の事業場CD", "運搬先の事業場名称", "運搬先の事業場郵便番号", "運搬先の事業場電話番号", "運搬先の事業場住所", "積替保管業者CD", "積替保管現場CD", "積替保管現場郵便番号", "積替保管現場電話番号", "積替保管現場住所", "有価物拾集", "有価物拾集量", "有価物拾集単位CD", "処分方法中間処理1", "処分方法中間処理2", "処分方法中間処理3", "処分方法中間処理4", "処分方法中間処理4CD", "処分方法中間処理5", "処分方法中間処理5CD", "処分方法中間処理6", "処分方法中間処理6CD", "処分方法中間処理7", "処分方法中間処理7CD", "処分方法中間処理8", "処分方法中間処理8CD", "処分方法最終処分1", "処分方法最終処分2", "処分方法最終処分3", "追加特記事項", "(区間1)運搬の受託者CD", "(区間1)運搬の受託者名称", "(区間1)運転者CD", "(区間1)運転者名称", "(区間1)運搬終了年月日", "(区間2)運搬の受託者CD", "(区間2)運搬の受託者名称", "(区間2)運転者CD", "(区間2)運転者名称", "(区間2)運搬終了年月日", "処分(1)の受託者CD", "処分(1)の受託者名称", "処分(1)担当者CD", "処分(1)担当者名称", "処分(1)終了年月日", "処分(2)の受託者CD", "処分(2)の受託者名称", "処分(2)担当者CD", "処分(2)担当者名称", "最終処分確認者", "最終処分業者CD", "最終処分現場CD", "最終処分現場名称", "最終処分現場郵便番号", "最終処分現場所在地", "処分先No", "照合確認B1票", "照合確認B2票", "照合確認D票", "照合確認E票", "返却日A票", "返却日B1票", "返却日B2票", "返却日C1票", "返却日C2票", "返却日D票", "返却日E票", "斜線項目事前協議", "斜線項目照合確認B1票", "斜線項目照合確認B2票", "斜線項目照合確認D票", "斜線項目照合確認E票", "斜線項目中間処理産業廃棄物", "斜線項目運搬受託者2", "斜線項目積替保管", "斜線項目運搬の受託2", "斜線項目追加特記事項", "削除フラグ", "行番号", "廃棄物種類CD", "廃棄物種類名", "廃棄物の名称CD", "廃棄物名称", "荷姿CD", "荷姿名", "数量", "単位CD", "単位名", "換算後数量", "処分方法CD", "処分方法名", "処分終了年月日", "最終処分終了年月日", "最終処分業者CD", "最終処分業者名", "最終処分場所CD", "最終処分場所名" };
        /// <summary>ヘッダ定義（紐付）</summary>
        public static readonly string[] ListManiHimodukeHeader = { "二次交付番号", "二次廃棄物区分CD", "二次廃棄物種類CD", "二次廃棄物名称CD", "一次交付番号", "一次廃棄物区分CD", "一次廃棄物種類CD", "一次廃棄物名称CD" };

        /// <summary>書式定義（直行）</summary>
        public static readonly List<DataType> ListColumnDataTypeChokko = new List<DataType>
        {
            new DataType() { Type = "Number", Lenght = 2},              // 拠点CD
            new DataType() { Type = "None", Lenght = -1},               // 拠点名
            new DataType() { Type = "Boolean", Lenght = 1},             // 一次マニフェスト区分
            new DataType() { Type = "Number", Lenght = 1},              // 交付番号区分
            new DataType() { Type = "StringNumber", Lenght = 11},       // 交付番号
            new DataType() { Type = "Number", Lenght = -1},             // 行数
            new DataType() { Type = "DateTime", Lenght = -1},           // 交付年月日
            new DataType() { Type = "StringNumber", Lenght = 20},       // 整理番号
            new DataType() { Type = "String", Lenght = 8},              // 交付担当者
            new DataType() { Type = "StringNumber", Lenght = 6},        // 排出事業者CD
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者名称
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者電話番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 排出事業場CD
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場名称
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場住所
            new DataType() { Type = "Number", Lenght = -1},             // 中間処理産業廃棄物フラグ
            new DataType() { Type = "String", Lenght = 100},            // 中間処理産業廃棄物
            new DataType() { Type = "Number", Lenght = -1},             // 最終処分の場所(予定)フラグ
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分の場所(予定)業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分の場所(予定)現場CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)現場名称
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)電話番号
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 運搬受託者CD
            new DataType() { Type = "None", Lenght = -1},               // 運搬受託者名称
            new DataType() { Type = "None", Lenght = -1},               // 運搬受託者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 運搬受託者電話番号
            new DataType() { Type = "None", Lenght = -1},               // 運搬受託者住所
            new DataType() { Type = "StringNumber", Lenght = 1},        // 運搬方法CD
            new DataType() { Type = "None", Lenght = -1},               // 運搬方法名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 車輌CD
            new DataType() { Type = "String", Lenght = 10},             // 車輌名
            new DataType() { Type = "StringNumber", Lenght = 3},        // 車種CD
            new DataType() { Type = "None", Lenght = -1},               // 車種名
            new DataType() { Type = "None", Lenght = -1},               // 運搬先の事業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 運搬先の事業場CD
            new DataType() { Type = "None", Lenght = -1},               // 運搬先の事業場名称
            new DataType() { Type = "None", Lenght = -1},               // 運搬先の事業場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 運搬先の事業場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 運搬先の事業場住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分受託者CD
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者名称
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者電話番号
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 積替保管業者CD
            new DataType() { Type = "None", Lenght = -1},               // 積替保管業者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // 積替保管現場CD
            new DataType() { Type = "None", Lenght = -1},               // 積替保管場名称
            new DataType() { Type = "None", Lenght = -1},               // 積替保管場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 積替保管場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 積替保管場住所
            new DataType() { Type = "StringNumber", Lenght = 2},        // 有害物質CD
            new DataType() { Type = "String", Lenght = 20},             // 有害物質名
            new DataType() { Type = "String", Lenght = 62},             // 備考
            new DataType() { Type = "StringNumber", Lenght = 6},        // 運搬の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // 運搬の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // 運転者CD
            new DataType() { Type = "String", Lenght = 8},              // 運転者名
            new DataType() { Type = "DateTime", Lenght = -1},           // 運搬終了年月日
            new DataType() { Type = "Number", Lenght = -1},             // 有価物拾集量
            new DataType() { Type = "Number", Lenght = 2},              // 有価物拾集量単位CD
            new DataType() { Type = "None", Lenght = -1},               // 有価物拾集量単位名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // 処分の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分担当者CD
            new DataType() { Type = "String", Lenght = 8},              // 処分担当者名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分現場CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場名称
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場住所
            new DataType() { Type = "StringNumber", Lenght = 10},       // 処分先No
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認B2票
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認D票
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認E票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日A票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日B1票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日B2票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日C1票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日C2票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日D票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日E票
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目有害物質
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目備考
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目中間処理産業廃棄物
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目積替保管
            new DataType() { Type = "Boolean", Lenght = 1},             // 削除フラグ
            new DataType() { Type = "Number", Lenght = -1},             // 行番号
            new DataType() { Type = "Number", Lenght = 4},              // 廃棄物種類CD
            new DataType() { Type = "None", Lenght = -1},               // 廃棄物種類名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 廃棄物の名称CD
            new DataType() { Type = "None", Lenght = -1},               // 廃棄物名称
            new DataType() { Type = "StringNumber", Lenght = 2},        // 荷姿CD
            new DataType() { Type = "None", Lenght = -1},               // 荷姿名
            new DataType() { Type = "Number", Lenght = -1},             // 数量
            new DataType() { Type = "Number", Lenght = 2},              // 単位CD
            new DataType() { Type = "None", Lenght = -1},               // 単位名
            new DataType() { Type = "None", Lenght = -1},               // 換算後数量
            new DataType() { Type = "StringNumber", Lenght = 3},        // 処分方法CD
            new DataType() { Type = "None", Lenght = -1},               // 処分方法名
            new DataType() { Type = "DateTime", Lenght = -1},           // 処分終了年月日
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分業者CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分業者名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分場所CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場所名
            new DataType() { Type = "DateTime", Lenght = -1},           // 最終処分終了年月日

        };

        /// <summary>書式定義（積替）</summary>
        public static readonly List<DataType> ListColumnDataTypeTsumikae = new List<DataType>
        {
            new DataType() { Type = "Number", Lenght = 2},              // 拠点CD
            new DataType() { Type = "None", Lenght = -1},               // 拠点名
            new DataType() { Type = "Boolean", Lenght = 1},             // 一次マニフェスト区分
            new DataType() { Type = "Number", Lenght = 1},              // 交付番号区分
            new DataType() { Type = "StringNumber", Lenght = 11},       // 交付番号
            new DataType() { Type = "Number", Lenght = -1},             // 行数
            new DataType() { Type = "DateTime", Lenght = -1},           // 交付年月日
            new DataType() { Type = "StringNumber", Lenght = 20},       // 整理番号
            new DataType() { Type = "String", Lenght = 8},              // 交付担当者
            new DataType() { Type = "StringNumber", Lenght = 6},        // 排出事業者CD
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者名称
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者電話番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 排出事業場CD
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場名称
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場住所
            new DataType() { Type = "Number", Lenght = -1},             // 中間処理産業廃棄物フラグ
            new DataType() { Type = "String", Lenght = 100},            // 中間処理産業廃棄物
            new DataType() { Type = "Number", Lenght = -1},             // 最終処分の場所(予定)フラグ
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分の場所(予定)業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分の場所(予定)現場CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)現場名称
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)電話番号
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)運搬受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬受託者名称
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬受託者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬受託者電話番号
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬受託者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)車輌CD
            new DataType() { Type = "String", Lenght = 10},             // (区間1)車輌名
            new DataType() { Type = "StringNumber", Lenght = 3},        // (区間1)車種CD
            new DataType() { Type = "None", Lenght = -1},               // (区間1)車種名
            new DataType() { Type = "StringNumber", Lenght = 1},        // (区間1)運搬方法CD
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬方法名
            new DataType() { Type = "Number", Lenght = -1},             // (区間1)運搬先区分
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)運搬先の事業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)運搬先の事業場CD
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬先の事業場名称
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬先の事業場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬先の事業場電話番号
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬先の事業場住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)運搬受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬受託者名称
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬受託者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬受託者電話番号
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬受託者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)車輌CD
            new DataType() { Type = "String", Lenght = 10},             // (区間2)車輌名
            new DataType() { Type = "StringNumber", Lenght = 3},        // (区間2)車種CD
            new DataType() { Type = "None", Lenght = -1},               // (区間2)車種名
            new DataType() { Type = "StringNumber", Lenght = 1},        // (区間2)運搬方法CD
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬方法名
            new DataType() { Type = "Number", Lenght = -1},             // (区間2)運搬先区分
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)運搬先の事業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)運搬先の事業場CD
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬先の事業場名称
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬先の事業場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬先の事業場電話番号
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬先の事業場住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間3)運搬受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬受託者名称
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬受託者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬受託者電話番号
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬受託者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間3)車輌CD
            new DataType() { Type = "String", Lenght = 10},             // (区間3)車輌名
            new DataType() { Type = "StringNumber", Lenght = 3},        // (区間3)車種CD
            new DataType() { Type = "None", Lenght = -1},               // (区間3)車種名
            new DataType() { Type = "StringNumber", Lenght = 1},        // (区間3)運搬方法CD
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬方法名
            new DataType() { Type = "Number", Lenght = -1},             // (区間3)運搬先区分
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間3)運搬先の事業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間3)運搬先の事業場CD
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬先の事業場名称
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬先の事業場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬先の事業場電話番号
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬先の事業場住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分受託者CD
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者名称
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者電話番号
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 積替保管業者CD
            new DataType() { Type = "None", Lenght = -1},               // 積替保管業者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // 積替保管現場CD
            new DataType() { Type = "None", Lenght = -1},               // 積替保管場名称
            new DataType() { Type = "None", Lenght = -1},               // 積替保管場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 積替保管場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 積替保管場住所
            new DataType() { Type = "StringNumber", Lenght = 2},        // 有害物質CD
            new DataType() { Type = "String", Lenght = 20},             // 有害物質名
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)運搬の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)運転者CD
            new DataType() { Type = "String", Lenght = 8},              // (区間1)運転者名
            new DataType() { Type = "DateTime", Lenght = -1},           // (区間1)運搬終了年月日
            new DataType() { Type = "Number", Lenght = -1},             // (区間1)有価物拾集量
            new DataType() { Type = "Number", Lenght = 2},              // (区間1)有価物拾集量単位CD
            new DataType() { Type = "None", Lenght = -1},               // (区間1)有価物拾集量単位名
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)運搬の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)運転者CD
            new DataType() { Type = "String", Lenght = 8},              // (区間2)運転者名
            new DataType() { Type = "DateTime", Lenght = -1},           // (区間2)運搬終了年月日
            new DataType() { Type = "Number", Lenght = -1},             // (区間2)有価物拾集量
            new DataType() { Type = "Number", Lenght = 2},              // (区間2)有価物拾集量単位CD
            new DataType() { Type = "None", Lenght = -1},               // (区間2)有価物拾集量単位名
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間3)運搬の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間3)運搬の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間3)運転者CD
            new DataType() { Type = "String", Lenght = 8},              // (区間3)運転者名
            new DataType() { Type = "DateTime", Lenght = -1},           // (区間3)運搬終了年月日
            new DataType() { Type = "Number", Lenght = -1},             // (区間3)有価物拾集量
            new DataType() { Type = "Number", Lenght = 2},              // (区間3)有価物拾集量単位CD
            new DataType() { Type = "None", Lenght = -1},               // (区間3)有価物拾集量単位名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // 処分の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分担当者CD
            new DataType() { Type = "String", Lenght = 8},              // 処分担当者名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分現場CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場名称
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場住所
            new DataType() { Type = "StringNumber", Lenght = 10},       // 処分先No
            new DataType() { Type = "String", Lenght = 77},             // 備考
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認B2票
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認B4票
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認B6票
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認D票
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認E票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日A票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日B1票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日B2票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日B4票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日B6票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日C1票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日C2票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日D票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日E票
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目有害物質
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目中間処理産業廃棄物
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目運搬受託者2
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目運搬の受託者2
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目運搬受託者3
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目運搬の受託者3
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目積替保管
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目運搬先事業場2
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目運搬先事業場3
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目備考
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目B4票
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目B6票
            new DataType() { Type = "Boolean", Lenght = 1},             // 削除フラグ
            new DataType() { Type = "Number", Lenght = -1},             // 行番号
            new DataType() { Type = "Number", Lenght = 4},              // 廃棄物種類CD
            new DataType() { Type = "None", Lenght = -1},               // 廃棄物種類名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 廃棄物の名称CD
            new DataType() { Type = "None", Lenght = -1},               // 廃棄物名称
            new DataType() { Type = "StringNumber", Lenght = 2},        // 荷姿CD
            new DataType() { Type = "None", Lenght = -1},               // 荷姿名
            new DataType() { Type = "Number", Lenght = -1},             // 数量
            new DataType() { Type = "Number", Lenght = 2},              // 単位CD
            new DataType() { Type = "None", Lenght = -1},               // 単位名
            new DataType() { Type = "None", Lenght = -1},               // 換算後数量
            new DataType() { Type = "StringNumber", Lenght = 3},        // 処分方法CD
            new DataType() { Type = "None", Lenght = -1},               // 処分方法名
            new DataType() { Type = "DateTime", Lenght = -1},           // 処分終了年月日
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分業者CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分業者名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分場所CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場所名
            new DataType() { Type = "DateTime", Lenght = -1},           // 最終処分終了年月日

        };

        /// <summary>書式定義（建廃）</summary>
        public static readonly List<DataType> ListColumnDataTypeKenpai = new List<DataType>
        {
            new DataType() { Type = "Number", Lenght = 2},              // 拠点CD
            new DataType() { Type = "None", Lenght = -1},               // 拠点名
            new DataType() { Type = "Boolean", Lenght = 1},             // 一次マニフェスト区分
            new DataType() { Type = "Number", Lenght = 1},              // 交付番号区分
            new DataType() { Type = "StringNumber", Lenght = 11},       // 交付番号
            new DataType() { Type = "Number", Lenght = -1},             // 行数
            new DataType() { Type = "DateTime", Lenght = -1},           // 交付年月日
            new DataType() { Type = "StringNumber", Lenght = 20},       // 整理番号
            new DataType() { Type = "String", Lenght = 20},             // 交付担当者所属
            new DataType() { Type = "String", Lenght = 8},              // 交付担当者氏名
            new DataType() { Type = "String", Lenght = 30},             // 事前協議番号
            new DataType() { Type = "DateTime", Lenght = -1},           // 事前協議年月日
            new DataType() { Type = "StringNumber", Lenght = 6},        // 排出事業者CD
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者名称
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者電話番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 排出事業場CD
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場名称
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 排出事業場住所
            new DataType() { Type = "Number", Lenght = -1},             // 中間処理産業廃棄物フラグ
            new DataType() { Type = "String", Lenght = 100},            // 中間処理産業廃棄物
            new DataType() { Type = "Number", Lenght = -1},             // 最終処分の場所(予定)フラグ
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分の場所(予定)業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分の場所(予定)現場CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)現場名称
            new DataType() { Type = "None", Lenght = -1},               // 最終処分の場所(予定)住所
            new DataType() { Type = "Boolean", Lenght = 1},             // 形状1
            new DataType() { Type = "Boolean", Lenght = 1},             // 形状2
            new DataType() { Type = "Boolean", Lenght = 1},             // 形状3
            new DataType() { Type = "Boolean", Lenght = 1},             // 形状4
            new DataType() { Type = "StringNumber", Lenght = 2},        // 形状4CD
            new DataType() { Type = "Boolean", Lenght = 1},             // 形状5
            new DataType() { Type = "StringNumber", Lenght = 2},        // 形状5CD
            new DataType() { Type = "Boolean", Lenght = 1},             // 形状6
            new DataType() { Type = "StringNumber", Lenght = 2},        // 形状6CD
            new DataType() { Type = "Boolean", Lenght = 1},             // 形状7
            new DataType() { Type = "StringNumber", Lenght = 2},        // 形状7CD
            new DataType() { Type = "None", Lenght = -1},               // 荷姿1
            new DataType() { Type = "None", Lenght = -1},               // 荷姿2
            new DataType() { Type = "None", Lenght = -1},               // 荷姿3
            new DataType() { Type = "None", Lenght = -1},               // 荷姿4
            new DataType() { Type = "None", Lenght = -1},               // 荷姿5
            new DataType() { Type = "None", Lenght = -1},               // 荷姿5CD
            new DataType() { Type = "None", Lenght = -1},               // 荷姿6
            new DataType() { Type = "None", Lenght = -1},               // 荷姿6CD
            new DataType() { Type = "None", Lenght = -1},               // 荷姿7
            new DataType() { Type = "None", Lenght = -1},               // 荷姿7CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)運搬受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬受託者名称
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬受託者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬受託者電話番号
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬受託者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)車輌CD
            new DataType() { Type = "String", Lenght = 10},             // (区間1)車輌名称
            new DataType() { Type = "StringNumber", Lenght = 3},        // (区間1)車種CD
            new DataType() { Type = "String", Lenght = 10},             // (区間1)車種名称
            new DataType() { Type = "Number", Lenght = -1},             // (区間1)積替・保管有無
            new DataType() { Type = "StringNumber", Lenght = 1},        // (区間1)運搬方法CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)運搬受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬受託者名称
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬受託者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬受託者電話番号
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬受託者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)車輌CD
            new DataType() { Type = "String", Lenght = 10},             // (区間2)車輌名称
            new DataType() { Type = "StringNumber", Lenght = 3},        // (区間2)車種CD
            new DataType() { Type = "String", Lenght = 10},             // (区間2)車種名称
            new DataType() { Type = "Number", Lenght = -1},             // (区間2)積替・保管有無
            new DataType() { Type = "StringNumber", Lenght = 1},        // (区間2)運搬方法CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分受託者CD
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者名称
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者電話番号
            new DataType() { Type = "None", Lenght = -1},               // 処分受託者住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 運搬先の事業場CD
            new DataType() { Type = "None", Lenght = -1},               // 運搬先の事業場名称
            new DataType() { Type = "None", Lenght = -1},               // 運搬先の事業場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 運搬先の事業場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 運搬先の事業場住所
            new DataType() { Type = "StringNumber", Lenght = 6},        // 積替保管業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 積替保管現場CD
            new DataType() { Type = "None", Lenght = -1},               // 積替保管現場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 積替保管現場電話番号
            new DataType() { Type = "None", Lenght = -1},               // 積替保管現場住所
            new DataType() { Type = "Number", Lenght = -1},             // 有価物拾集
            new DataType() { Type = "Number", Lenght = -1},             // 有価物拾集量
            new DataType() { Type = "Number", Lenght = 1},              // 有価物拾集単位CD
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法中間処理1
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法中間処理2
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法中間処理3
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法中間処理4
            new DataType() { Type = "StringNumber", Lenght = 3},        // 処分方法中間処理4CD
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法中間処理5
            new DataType() { Type = "StringNumber", Lenght = 3},        // 処分方法中間処理5CD
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法中間処理6
            new DataType() { Type = "StringNumber", Lenght = 3},        // 処分方法中間処理6CD
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法中間処理7
            new DataType() { Type = "StringNumber", Lenght = 3},        // 処分方法中間処理7CD
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法中間処理8
            new DataType() { Type = "StringNumber", Lenght = 3},        // 処分方法中間処理8CD
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法最終処分1
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法最終処分2
            new DataType() { Type = "Boolean", Lenght = 1},             // 処分方法最終処分3
            new DataType() { Type = "String", Lenght = 374},            // 追加特記事項
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)運搬の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間1)運搬の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間1)運転者CD
            new DataType() { Type = "String", Lenght = 8},              // (区間1)運転者名称
            new DataType() { Type = "DateTime", Lenght = -1},           // (区間1)運搬終了年月日
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)運搬の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // (区間2)運搬の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // (区間2)運転者CD
            new DataType() { Type = "String", Lenght = 8},              // (区間2)運転者名称
            new DataType() { Type = "DateTime", Lenght = -1},           // (区間2)運搬終了年月日
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分(1)の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // 処分(1)の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分(1)担当者CD
            new DataType() { Type = "String", Lenght = 8},              // 処分(1)担当者名称
            new DataType() { Type = "DateTime", Lenght = -1},           // 処分(1)終了年月日
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分(2)の受託者CD
            new DataType() { Type = "None", Lenght = -1},               // 処分(2)の受託者名称
            new DataType() { Type = "StringNumber", Lenght = 6},        // 処分(2)担当者CD
            new DataType() { Type = "String", Lenght = 8},              // 処分(2)担当者名称
            new DataType() { Type = "String", Lenght = 8},              // 最終処分確認者
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分業者CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分現場CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分現場名称
            new DataType() { Type = "None", Lenght = -1},               // 最終処分現場郵便番号
            new DataType() { Type = "None", Lenght = -1},               // 最終処分現場所在地
            new DataType() { Type = "StringNumber", Lenght = 10},       // 処分先No
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認B1票
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認B2票
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認D票
            new DataType() { Type = "DateTime", Lenght = -1},           // 照合確認E票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日A票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日B1票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日B2票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日C1票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日C2票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日D票
            new DataType() { Type = "DateTime", Lenght = -1},           // 返却日E票
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目事前協議
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目照合確認B1票
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目照合確認B2票
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目照合確認D票
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目照合確認E票
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目中間処理産業廃棄物
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目運搬受託者2
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目積替保管
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目運搬の受託2
            new DataType() { Type = "Boolean", Lenght = 1},             // 斜線項目追加特記事項
            new DataType() { Type = "Boolean", Lenght = 1},             // 削除フラグ
            new DataType() { Type = "Number", Lenght = -1},             // 行番号
            new DataType() { Type = "Number", Lenght = 4},              // 廃棄物種類CD
            new DataType() { Type = "None", Lenght = -1},               // 廃棄物種類名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 廃棄物の名称CD
            new DataType() { Type = "None", Lenght = -1},               // 廃棄物名称
            new DataType() { Type = "StringNumber", Lenght = 2},        // 荷姿CD
            new DataType() { Type = "None", Lenght = -1},               // 荷姿名
            new DataType() { Type = "Number", Lenght = -1},             // 数量
            new DataType() { Type = "Number", Lenght = 2},              // 単位CD
            new DataType() { Type = "None", Lenght = -1},               // 単位名
            new DataType() { Type = "None", Lenght = -1},               // 換算後数量
            new DataType() { Type = "StringNumber", Lenght = 3},        // 処分方法CD
            new DataType() { Type = "None", Lenght = -1},               // 処分方法名
            new DataType() { Type = "DateTime", Lenght = -1},           // 処分終了年月日
            new DataType() { Type = "DateTime", Lenght = -1},           // 最終処分終了年月日
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分業者CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分業者名
            new DataType() { Type = "StringNumber", Lenght = 6},        // 最終処分場所CD
            new DataType() { Type = "None", Lenght = -1},               // 最終処分場所名

        };

        /// <summary>書式定義（紐付）</summary>
        public static readonly List<DataType> ListColumnDataTypeHimoduke = new List<DataType>
        {
            new DataType() { Type = "StringNumber", Lenght = 11},       // 二次交付番号
            new DataType() { Type = "Number", Lenght = 1},              // 二次廃棄物区分CD
            new DataType() { Type = "StringNumber", Lenght = 7},        // 二次廃棄物種類CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 二次廃棄物名称CD
            new DataType() { Type = "StringNumber", Lenght = 11},       // 一次交付番号
            new DataType() { Type = "Number", Lenght = 1},              // 一次廃棄物区分CD
            new DataType() { Type = "StringNumber", Lenght = 7},        // 一次廃棄物種類CD
            new DataType() { Type = "StringNumber", Lenght = 6},        // 一次廃棄物名称CD
        };
    }

    /// <summary>書式マッピング（データタイプ、長さ）</summary>
    public class DataType
    {
        public string Type { get; set; }
        public int Lenght { get; set; }
    }
}
