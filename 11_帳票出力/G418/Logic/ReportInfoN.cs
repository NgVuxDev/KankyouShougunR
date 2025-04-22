using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Classes -

    #region - ReportInfoCommon -

    /// <summary>帳票情報共通を表すクラス・コントロール</summary>
    internal class ReportInfoCommon : ReportInfoBase
    {
        #region - Fields -

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoCommon" /> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <param name="dataTable">データーテーブル</param>
        /// <param name="commonChouhyouBase">共通帳票</param>
        public ReportInfoCommon(WINDOW_ID windowID, DataTable dataTable, CommonChouhyouBase commonChouhyouBase)
            : base(dataTable)
        {
            this.WindowID = windowID;
            this.ComChouhyouBase = commonChouhyouBase;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>画面ＩＤを保持するプロパティ</summary>
        public WINDOW_ID WindowID { get; set; }

        /// <summary>共通帳票ベースクラスを保持するプロパティ</summary>
        protected CommonChouhyouBase ComChouhyouBase { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            try
            {
                string tmp;
                string tmpBegin;
                string tmpEnd;
                int index;
                int indexBegin;
                int indexEnd;

                // 受け渡し用データーテーブル(ヘッダ)フィールド名の設定処理
                this.SetHeaderFieldNameForUkewatashi();

                // 会社名
                tmp = this.ComChouhyouBase.CorpRyakuName;
                this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", tmp);
                index = this.DataTableList["Header"].Columns.IndexOf("CORP_RYAKU_NAME");
                this.DataTableList["Header"].Rows[0][index] = tmp;

                // 印刷日時
                tmp = this.ComChouhyouBase.DateTimePrint + " 発行";
                this.SetFieldName("FH_PRINT_DATE_VLB", tmp);
                index = this.DataTableList["Header"].Columns.IndexOf("PRINT_DATE");
                this.DataTableList["Header"].Rows[0][index] = tmp;

                // タイトル
                int item = this.ComChouhyouBase.SelectSyuukeiKoumokuList[0];
                tmp = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.WindowID) + " ( " + this.ComChouhyouBase.Name + " )";
                this.SetFieldName("FH_TITLE_VLB", tmp);
                index = this.DataTableList["Header"].Columns.IndexOf("TITLE");
                this.DataTableList["Header"].Rows[0][index] = tmp;

                // 日付範囲
                indexBegin = this.DataTableList["Header"].Columns.IndexOf("DENPYOU_DATE_BEGIN");
                indexEnd = this.DataTableList["Header"].Columns.IndexOf("DENPYOU_DATE_END");
                switch (this.ComChouhyouBase.KikanShiteiType)
                {
                    case CommonChouhyouBase.KIKAN_SHITEI_TYPE.Tougets:
                        // 今日の0時0分0秒000を取得
                        DateTime today = DateTime.Today;

                        // todayから今日の日付を引くと先月末なのでその次の日
                        DateTime firstDay = today.AddDays(-today.Day + 1);

                        // 来月1日の1日前
                        DateTime endDay = firstDay.AddMonths(1).AddDays(-1);

                        tmpBegin = firstDay.ToString("yyyy/MM/dd");
                        tmpEnd = endDay.ToString("yyyy/MM/dd");
                        tmp = tmpBegin + " ～ " + tmpEnd;
                        this.SetFieldName("FH_DENPYOU_DATE_CTL", tmp);
                        this.DataTableList["Header"].Rows[0][indexBegin] = tmpBegin;
                        this.DataTableList["Header"].Rows[0][indexEnd] = tmpEnd;

                        break;
                    case CommonChouhyouBase.KIKAN_SHITEI_TYPE.Shitei:
                        if (this.ComChouhyouBase.DateTimeStart.Equals(new DateTime(0)) && this.ComChouhyouBase.DateTimeEnd.Equals(new DateTime(0)))
                        {   // 期間が指定されていない
                            this.SetFieldName("FH_DENPYOU_DATE_CTL", "全期間");
                            this.DataTableList["Header"].Rows[0][indexBegin] = string.Empty;
                            this.DataTableList["Header"].Rows[0][indexEnd] = string.Empty;
                        }
                        else if (!this.ComChouhyouBase.DateTimeStart.Equals(new DateTime(0)) && this.ComChouhyouBase.DateTimeEnd.Equals(new DateTime(0)))
                        {   // 終了日付が指定されていない

                            tmpBegin = this.ComChouhyouBase.DateTimeStart.ToString("yyyy/MM/dd");
                            this.SetFieldName("FH_DENPYOU_DATE_CTL", tmpBegin + " ～ ");
                            this.DataTableList["Header"].Rows[0][indexBegin] = tmpBegin;
                            this.DataTableList["Header"].Rows[0][indexEnd] = string.Empty;
                        }
                        else if (this.ComChouhyouBase.DateTimeStart.Equals(new DateTime(0)) && !this.ComChouhyouBase.DateTimeEnd.Equals(new DateTime(0)))
                        {   // 開始日付が指定されていない

                            tmpEnd = this.ComChouhyouBase.DateTimeEnd.ToString("yyyy/MM/dd");
                            this.SetFieldName("FH_DENPYOU_DATE_CTL", " ～ " + tmpEnd);
                            this.DataTableList["Header"].Rows[0][indexBegin] = string.Empty;
                            this.DataTableList["Header"].Rows[0][indexEnd] = tmpEnd;
                        }
                        else
                        {   // 両方指定されている

                            tmpBegin = this.ComChouhyouBase.DateTimeStart.ToString("yyyy/MM/dd");
                            tmpEnd = this.ComChouhyouBase.DateTimeEnd.ToString("yyyy/MM/dd");
                            this.SetFieldName("FH_DENPYOU_DATE_CTL", tmpBegin + " ～ " + tmpEnd);
                            this.DataTableList["Header"].Rows[0][indexBegin] = tmpBegin;
                            this.DataTableList["Header"].Rows[0][indexEnd] = tmpEnd;
                        }

                        break;
                    default:
                    case CommonChouhyouBase.KIKAN_SHITEI_TYPE.Toujitsu:

                        DateTime dateTime = DateTime.Now;

                        tmpBegin = dateTime.ToString("yyyy/MM/dd");
                        tmpEnd = dateTime.ToString("yyyy/MM/dd");
                        this.SetFieldName("FH_DENPYOU_DATE_CTL", tmpBegin + " ～ " + tmpEnd);
                        this.DataTableList["Header"].Rows[0][indexBegin] = tmpBegin;
                        this.DataTableList["Header"].Rows[0][indexEnd] = tmpEnd;

                        break;
                }

                // 拠点コード
                index = this.DataTableList["Header"].Columns.IndexOf("KYOTEN_CD");
                tmp = this.ComChouhyouBase.KyotenCode;
                this.DataTableList["Header"].Rows[0][index] = tmp;

                // 拠点名
                index = this.DataTableList["Header"].Columns.IndexOf("KYOTEN_NAME_RYAKU");
                if (this.ComChouhyouBase.KyotenCode == "99")
                {
                    this.SetFieldName("FH_KYOTEN_NAME_VLB", "全社");
                    this.DataTableList["Header"].Rows[0][index] = "全社";
                }
                else
                {
                    tmp = this.ComChouhyouBase.KyotenCodeName;
                    this.SetFieldName("FH_KYOTEN_NAME_VLB", tmp);
                    this.DataTableList["Header"].Rows[0][index] = tmp;
                }

                if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU || this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU)
                {   // R355(売上／支払明細票)・R356(売上／支払集計表)

                    #region - R355(売上／支払明細票)・R356(売上／支払集計表) -

                    // 伝票種類
                    index = this.DataTableList["Header"].Columns.IndexOf("DENPYOU_SHURUI");
                    switch (this.ComChouhyouBase.DenpyouSyurui)
                    {
                        case CommonChouhyouBase.DENPYOU_SYURUI.Ukeire:          // 受入

                            this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "受入");
                            this.DataTableList["Header"].Rows[0][index] = "受入";

                            break;
                        case CommonChouhyouBase.DENPYOU_SYURUI.Syutsuka:        // 出荷

                            this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "出荷");
                            this.DataTableList["Header"].Rows[0][index] = "出荷";

                            break;
                        case CommonChouhyouBase.DENPYOU_SYURUI.UriageShiharai:  // 売上／支払

                            this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "売上／支払");
                            this.DataTableList["Header"].Rows[0][index] = "売上／支払";

                            break;
                        case CommonChouhyouBase.DENPYOU_SYURUI.Subete:          // 全て

                            this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "全て");
                            this.DataTableList["Header"].Rows[0][index] = "全て";

                            break;
                        default:    // ブランク

                            this.SetFieldVisible("FH_DENPYOU_SHURUI_FLB", false);
                            this.SetFieldVisible("FH_DENPYOU_SHURUI_CTL", false);
                            this.DataTableList["Header"].Rows[0][index] = string.Empty;

                            break;
                    }

                    // 伝票区分
                    index = this.DataTableList["Header"].Columns.IndexOf("DENPYOU_KUBUN");
                    switch (this.ComChouhyouBase.DenpyouKubun)
                    {
                        case CommonChouhyouBase.DENPYOU_KUBUN.Uriage:   // 売上

                            this.SetFieldName("FH_DENPYOU_KBN_CTL", "売上");
                            this.DataTableList["Header"].Rows[0][index] = "売上";

                            break;
                        case CommonChouhyouBase.DENPYOU_KUBUN.Shiharai: // 支払

                            this.SetFieldName("FH_DENPYOU_KBN_CTL", "支払");
                            this.DataTableList["Header"].Rows[0][index] = "支払";

                            break;
                        case CommonChouhyouBase.DENPYOU_KUBUN.Subete:   // 全て

                            this.SetFieldName("FH_DENPYOU_KBN_CTL", "全て");
                            this.DataTableList["Header"].Rows[0][index] = "全て";

                            break;
                        default:    // ブランク

                            this.SetFieldVisible("FH_DENPYOU_KBN_FLB", false);
                            this.SetFieldVisible("FH_DENPYOU_KBN_CTL", false);
                            this.DataTableList["Header"].Rows[0][index] = string.Empty;

                            break;
                    }

                    #endregion - R355(売上／支払明細票)・R356(売上／支払集計表) -
                }
                else if (this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU)
                {   // R342(受付明細票)

                    #region - R342(受付明細票) -

                    // 伝票種類
                    index = this.DataTableList["Header"].Columns.IndexOf("DENPYOU_SHURUI");
                    switch (this.ComChouhyouBase.DenpyouSyurui)
                    {
                        case CommonChouhyouBase.DENPYOU_SYURUI.Syuusyuu:        // 収集

                            this.SetFieldName("FH_DENPYOU_SHURUI_VLB", "収集");
                            this.DataTableList["Header"].Rows[0][index] = "収集";

                            break;
                        case CommonChouhyouBase.DENPYOU_SYURUI.Syutsuka:        // 出荷

                            this.SetFieldName("FH_DENPYOU_SHURUI_VLB", "出荷");
                            this.DataTableList["Header"].Rows[0][index] = "出荷";

                            break;
                        case CommonChouhyouBase.DENPYOU_SYURUI.Mochikomi:       // 持込

                            this.SetFieldName("FH_DENPYOU_SHURUI_VLB", "持込");
                            this.DataTableList["Header"].Rows[0][index] = "持込";

                            break;
                        case CommonChouhyouBase.DENPYOU_SYURUI.Butsupan:        // 物販

                            this.SetFieldName("FH_DENPYOU_SHURUI_VLB", "物販");
                            this.DataTableList["Header"].Rows[0][index] = "物販";

                            break;
                        default:    // ブランク

                            this.SetFieldVisible("FH_DENPYOU_SHURUI_FLB", false);
                            this.SetFieldVisible("FH_DENPYOU_SHURUI_CTL", false);
                            this.SetFieldVisible("FH_DENPYOU_SHURUI_VLB", false);
                            this.DataTableList["Header"].Rows[0][index] = string.Empty;

                            break;
                    }

                    // 伝票区分

                    #endregion - R342(受付明細票) -
                }
                else if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU ||
                         this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU || this.WindowID == WINDOW_ID.R_KEIRYOU_SUIIHYOU ||
                         this.WindowID == WINDOW_ID.R_URIAGE_JYUNNIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_JYUNNIHYOU ||
                         this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU || this.WindowID == WINDOW_ID.R_KEIRYOU_JYUNNIHYOU ||
                         this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU ||
                         this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                {   // R432(売上推移表・支払推移表・売上/支払推移表・計量推移表)
                    // R433(売上順位表・支払順位表・売上/支払順位表・計量順位表)
                    // R434(売上前年対比表・支払前年対比表・売上/支払前年対比表・計量前年対比表)

                    #region - R432(売上推移表・支払推移表・売上/支払推移表・計量推移表)・R433(売上順位表・支払順位表・売上/支払順位表・計量順位表)・R434(売上前年対比表・支払前年対比表・売上/支払前年対比表・計量前年対比表) -

                    // 伝票種類
                    index = this.DataTableList["Header"].Columns.IndexOf("DENPYOU_SHURUI");
                    if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU ||
                        this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU ||
                        this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU)
                    {   // 売上/支払推移表・売上/支払順位表・売上/支払前年対比表

                        switch (this.ComChouhyouBase.DenpyouSyurui)
                        {
                            case CommonChouhyouBase.DENPYOU_SYURUI.Ukeire:          // 受入

                                if (this.ComChouhyouBase.IsDenpyouSyuruiGroupKubun)
                                {   // グループ区分有
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "受入");
                                    this.DataTableList["Header"].Rows[0][index] = "受入";
                                }
                                else
                                {   // グループ区分無
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "受入");
                                    this.DataTableList["Header"].Rows[0][index] = "受入";
                                }

                                break;
                            case CommonChouhyouBase.DENPYOU_SYURUI.Syutsuka:        // 出荷

                                if (this.ComChouhyouBase.IsDenpyouSyuruiGroupKubun)
                                {   // グループ区分有
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "出荷");
                                    this.DataTableList["Header"].Rows[0][index] = "出荷";
                                }
                                else
                                {   // グループ区分無
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "出荷");
                                    this.DataTableList["Header"].Rows[0][index] = "出荷";
                                }

                                break;
                            case CommonChouhyouBase.DENPYOU_SYURUI.UriageShiharai:  // 売上／支払

                                if (this.ComChouhyouBase.IsDenpyouSyuruiGroupKubun)
                                {   // グループ区分有
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "売上／支払");
                                    this.DataTableList["Header"].Rows[0][index] = "売上／支払";
                                }
                                else
                                {   // グループ区分無
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "売上／支払");
                                    this.DataTableList["Header"].Rows[0][index] = "売上／支払";
                                }

                                break;
                            case CommonChouhyouBase.DENPYOU_SYURUI.Subete:          // 全て

                                if (this.ComChouhyouBase.IsDenpyouSyuruiGroupKubun)
                                {   // グループ区分有
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "全て");
                                    this.DataTableList["Header"].Rows[0][index] = "全て";
                                }
                                else
                                {   // グループ区分無
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "全て");
                                    this.DataTableList["Header"].Rows[0][index] = "全て";
                                }

                                break;
                            default:    // ブランク

                                this.SetFieldVisible("FH_DENPYOU_SHURUI_FLB", false);
                                this.SetFieldVisible("FH_DENPYOU_SHURUI_CTL", false);
                                this.DataTableList["Header"].Rows[0][index] = string.Empty;

                                break;
                        }
                    }
                    else
                    {   // 売上推移表・支払推移表・計量推移表・売上順位表・支払順位表・計量順位表・売上前年対比表・支払前年対比表・計量前年対比表
                        switch (this.ComChouhyouBase.DenpyouSyurui)
                        {
                            case CommonChouhyouBase.DENPYOU_SYURUI.Ukeire:          // 受入

                                this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "受入");
                                this.DataTableList["Header"].Rows[0][index] = "受入";

                                break;
                            case CommonChouhyouBase.DENPYOU_SYURUI.Syutsuka:        // 出荷

                                this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "出荷");
                                this.DataTableList["Header"].Rows[0][index] = "出荷";

                                break;
                            case CommonChouhyouBase.DENPYOU_SYURUI.UriageShiharai:  // 売上／支払

                                this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "売上／支払");
                                this.DataTableList["Header"].Rows[0][index] = "売上／支払";

                                break;
                            case CommonChouhyouBase.DENPYOU_SYURUI.Subete:          // 全て

                                if (this.ComChouhyouBase.IsDenpyouSyuruiGroupKubun)
                                {   // グループ区分有
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "全て");
                                    this.DataTableList["Header"].Rows[0][index] = "全て";
                                }
                                else
                                {   // グループ区分無
                                    this.SetFieldName("FH_DENPYOU_SHURUI_CTL", "全て");
                                    this.DataTableList["Header"].Rows[0][index] = "全て";
                                }

                                break;
                            default:    // ブランク

                                this.SetFieldVisible("FH_DENPYOU_SHURUI_FLB", false);
                                this.SetFieldVisible("FH_DENPYOU_SHURUI_CTL", false);
                                this.DataTableList["Header"].Rows[0][index] = string.Empty;

                                break;
                        }
                    }

                    // 伝票区分
                    index = this.DataTableList["Header"].Columns.IndexOf("DENPYOU_KUBUN");
                    switch (this.ComChouhyouBase.DenpyouKubun)
                    {
                        case CommonChouhyouBase.DENPYOU_KUBUN.Uriage:   // 売上

                            this.SetFieldName("FH_DENPYOU_KBN_CTL", "売上");
                            this.DataTableList["Header"].Rows[0][index] = "売上";

                            break;
                        case CommonChouhyouBase.DENPYOU_KUBUN.Shiharai: // 支払

                            this.SetFieldName("FH_DENPYOU_KBN_CTL", "支払");
                            this.DataTableList["Header"].Rows[0][index] = "支払";

                            break;
                        case CommonChouhyouBase.DENPYOU_KUBUN.Subete:   // 全て

                            this.SetFieldName("FH_DENPYOU_KBN_CTL", "全て");
                            this.DataTableList["Header"].Rows[0][index] = "全て";

                            break;
                        default:    // ブランク

                            this.SetFieldVisible("FH_DENPYOU_KBN_FLB", false);
                            this.SetFieldVisible("FH_DENPYOU_KBN_CTL", false);
                            this.DataTableList["Header"].Rows[0][index] = string.Empty;

                            break;
                    }

                    // 単位ラベル
                    if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                    {   // R432(売上推移表・支払推移表)
                        tmp = "単位：(千円)";

                        this.SetFieldName("FH_FILL_TANNI_FLB", tmp);
                        index = this.DataTableList["Header"].Columns.IndexOf("UNIT_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = tmp;
                    }
                    else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU || this.WindowID == WINDOW_ID.R_KEIRYOU_SUIIHYOU)
                    {   // R432(売上/支払推移表・計量推移表)
                        tmp = "単位：(ｔ)";

                        this.SetFieldName("FH_FILL_TANNI_FLB", tmp);
                        index = this.DataTableList["Header"].Columns.IndexOf("UNIT_LABEL");
                        this.DataTableList["Header"].Rows[0][index] = tmp;
                    }

                    #endregion - R432(売上推移表・支払推移表・売上/支払推移表・計量推移表)・R433(売上順位表・支払順位表・売上/支払順位表・計量順位表)・R434(売上前年対比表・支払前年対比表・売上/支払前年対比表・計量前年対比表) -
                }
                else if (this.WindowID == WINDOW_ID.R_KEIRYOU_MEISAIHYOU)
                {   // R351(計量明細)

                    #region - R351(計量明細) -

                    // 伝票種類
                    this.SetFieldVisible("FH_DENPYOU_SHURUI_FLB", false);
                    this.SetFieldVisible("FH_DENPYOU_SHURUI_CTL", false);
                    this.SetFieldVisible("FH_DENPYOU_SHURUI_VLB", false);

                    // 伝票区分
                    this.SetFieldVisible("FH_DENPYOU_KBN_FLB", false);
                    this.SetFieldVisible("FH_DENPYOU_KBN_CTL", false);

                    #endregion - R351(計量明細) -
                }
                else if (this.WindowID == WINDOW_ID.R_UNNCHIN_MEISAIHYOU)
                {   // R398(運賃明細)

                    #region - R398(運賃明細) -

                    // 伝票種類
                    this.SetFieldVisible("FH_DENPYOU_SHURUI_FLB", false);
                    this.SetFieldVisible("FH_DENPYOU_SHURUI_CTL", false);
                    this.SetFieldVisible("FH_DENPYOU_SHURUI_VLB", false);

                    // 伝票区分
                    this.SetFieldVisible("FH_DENPYOU_KBN_FLB", false);
                    this.SetFieldVisible("FH_DENPYOU_KBN_CTL", false);

                    #endregion - R398(運賃明細) -
                }
                else
                {
                    #region - その他 -

                    // 伝票種類
                    this.SetFieldVisible("FH_DENPYOU_SHURUI_FLB", false);
                    this.SetFieldVisible("FH_DENPYOU_SHURUI_CTL", false);

                    // 伝票区分
                    this.SetFieldVisible("FH_DENPYOU_KBN_FLB", false);
                    this.SetFieldVisible("FH_DENPYOU_KBN_CTL", false);

                    #endregion - その他 -
                }

                #region - 集計項目 -

                int count = this.ComChouhyouBase.SelectEnableSyuukeiKoumokuGroupCount;

                #region - 集計項目１ -

                // 集計項目１
                int itemNo = this.ComChouhyouBase.SelectSyuukeiKoumokuList[0];
                if (count >= 1 && itemNo != 0)
                {
                    SyuukeiKoumoku syuukeiKoumoku = this.ComChouhyouBase.SyuukeiKomokuList[itemNo];

                    tmp = syuukeiKoumoku.Name;
                    this.SetFieldName("FH_FILL_COND_ID_1_VLB", "[" + tmp + "]");
                    index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_1_NAME");
                    this.DataTableList["Header"].Rows[0][index] = tmp;

                    if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart == string.Empty && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd == string.Empty)
                    {
                        this.SetFieldName("FH_FILL_COND_CD_1_CTL", "全て");
                    }
                    else
                    {
                        tmpBegin = syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart;
                        tmpEnd = syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd;
                        this.SetFieldName("FH_FILL_COND_CD_1_CTL", tmpBegin + " ～ " + tmpEnd);

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_1_CD_BEGIN");
                        this.DataTableList["Header"].Rows[0][index] = tmpBegin;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_1_VALUE_BEGIN");
                        this.DataTableList["Header"].Rows[0][index] = syuukeiKoumoku.SyuukeiKoumokuHani.CodeStartName;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_1_CD_END");
                        this.DataTableList["Header"].Rows[0][index] = tmpEnd;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_1_VALUE_END");
                        this.DataTableList["Header"].Rows[0][index] = syuukeiKoumoku.SyuukeiKoumokuHani.CodeEndName;
                    }
                }
                else
                {
                    this.SetFieldVisible("FH_FILL_COND_ID_1_VLB", false);
                    this.SetFieldVisible("FH_FILL_COND_CD_1_CTL", false);
                }

                #endregion - 集計項目１ -

                #region - 集計項目２ -

                // 集計項目２
                itemNo = this.ComChouhyouBase.SelectSyuukeiKoumokuList[1];
                if (count >= 2 && itemNo != 0)
                {
                    SyuukeiKoumoku syuukeiKoumoku = this.ComChouhyouBase.SyuukeiKomokuList[itemNo];

                    tmp = syuukeiKoumoku.Name;
                    this.SetFieldName("FH_FILL_COND_ID_2_VLB", "[" + tmp + "]");
                    index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_2_NAME");
                    this.DataTableList["Header"].Rows[0][index] = tmp;

                    if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart == string.Empty && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd == string.Empty)
                    {
                        this.SetFieldName("FH_FILL_COND_CD_2_CTL", "全て");
                    }
                    else
                    {
                        tmpBegin = syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart;
                        tmpEnd = syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd;
                        this.SetFieldName("FH_FILL_COND_CD_2_CTL", tmpBegin + " ～ " + tmpEnd);

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_2_CD_BEGIN");
                        this.DataTableList["Header"].Rows[0][index] = tmpBegin;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_2_VALUE_BEGIN");
                        this.DataTableList["Header"].Rows[0][index] = syuukeiKoumoku.SyuukeiKoumokuHani.CodeStartName;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_2_CD_END");
                        this.DataTableList["Header"].Rows[0][index] = tmpEnd;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_2_VALUE_END");
                        this.DataTableList["Header"].Rows[0][index] = syuukeiKoumoku.SyuukeiKoumokuHani.CodeEndName;
                    }
                }
                else
                {
                    this.SetFieldVisible("FH_FILL_COND_ID_2_VLB", false);
                    this.SetFieldVisible("FH_FILL_COND_CD_2_CTL", false);
                }

                #endregion - 集計項目２ -

                #region - 集計項目３ -

                // 集計項目３
                itemNo = this.ComChouhyouBase.SelectSyuukeiKoumokuList[2];
                if (count >= 3 && itemNo != 0)
                {
                    SyuukeiKoumoku syuukeiKoumoku = this.ComChouhyouBase.SyuukeiKomokuList[itemNo];

                    tmp = syuukeiKoumoku.Name;

                    this.SetFieldName("FH_FILL_COND_ID_3_VLB", "[" + tmp + "]");
                    index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_3_NAME");
                    this.DataTableList["Header"].Rows[0][index] = tmp;

                    if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart == string.Empty && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd == string.Empty)
                    {
                        this.SetFieldName("FH_FILL_COND_CD_3_CTL", "全て");
                    }
                    else
                    {
                        tmpBegin = syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart;
                        tmpEnd = syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd;
                        this.SetFieldName("FH_FILL_COND_CD_3_CTL", tmpBegin + " ～ " + tmpEnd);

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_3_CD_BEGIN");
                        this.DataTableList["Header"].Rows[0][index] = tmpBegin;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_3_VALUE_BEGIN");
                        this.DataTableList["Header"].Rows[0][index] = syuukeiKoumoku.SyuukeiKoumokuHani.CodeStartName;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_3_CD_END");
                        this.DataTableList["Header"].Rows[0][index] = tmpEnd;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_3_VALUE_END");
                        this.DataTableList["Header"].Rows[0][index] = syuukeiKoumoku.SyuukeiKoumokuHani.CodeEndName;
                    }
                }
                else
                {
                    this.SetFieldVisible("FH_FILL_COND_ID_3_VLB", false);
                    this.SetFieldVisible("FH_FILL_COND_CD_3_CTL", false);
                }

                #endregion - 集計項目３ -

                #region - 集計項目４ -

                // 集計項目４
                itemNo = this.ComChouhyouBase.SelectSyuukeiKoumokuList[3];
                if (count >= 4 && itemNo != 0)
                {
                    SyuukeiKoumoku syuukeiKoumoku = this.ComChouhyouBase.SyuukeiKomokuList[itemNo];

                    tmp = syuukeiKoumoku.Name;
                    this.SetFieldName("FH_FILL_COND_ID_4_VLB", "[" + tmp + "]");
                    index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_4_NAME");
                    this.DataTableList["Header"].Rows[0][index] = tmp;

                    if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart == string.Empty && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd == string.Empty)
                    {
                        this.SetFieldName("FH_FILL_COND_CD_4_CTL", "全て");
                    }
                    else
                    {
                        tmpBegin = syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart;
                        tmpEnd = syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd;
                        this.SetFieldName("FH_FILL_COND_CD_4_CTL", tmpBegin + " ～ " + tmpEnd);

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_4_CD_BEGIN");
                        this.DataTableList["Header"].Rows[0][index] = tmpBegin;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_4_VALUE_BEGIN");
                        this.DataTableList["Header"].Rows[0][index] = syuukeiKoumoku.SyuukeiKoumokuHani.CodeStartName;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_4_CD_END");
                        this.DataTableList["Header"].Rows[0][index] = tmpEnd;

                        index = this.DataTableList["Header"].Columns.IndexOf("FILL_COND_4_VALUE_END");
                        this.DataTableList["Header"].Rows[0][index] = syuukeiKoumoku.SyuukeiKoumokuHani.CodeEndName;
                    }
                }
                else
                {
                    this.SetFieldVisible("FH_FILL_COND_ID_4_VLB", false);
                    this.SetFieldVisible("FH_FILL_COND_CD_4_CTL", false);
                }

                #endregion - 集計項目４ -

                #endregion - 集計項目 -

                // アラート件数
                index = this.DataTableList["Header"].Columns.IndexOf("ALERT_NUMBER");
                this.DataTableList["Header"].Rows[0][index] = this.ComChouhyouBase.IchiranAlertCount.ToString();

                // 読込データ件数
                index = this.DataTableList["Header"].Columns.IndexOf("READ_DATA_NUMBER");
                this.DataTableList["Header"].Rows[0][index] = this.ComChouhyouBase.MaxRowCount;
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>受け渡し用データーテーブル（ヘッダ）フィールド名の設定処理を実行する</summary>
        private void SetHeaderFieldNameForUkewatashi()
        {
            try
            {
                // データテーブル生成
                DataTable datatable = new DataTable();

                #region - Field Name Array -

                string[] fieldNameArray =
            {
                "CORP_RYAKU_NAME",                  // 会社名
                "PRINT_DATE",                       // 発行日時
                "TITLE",                            // タイトル
                "KYOTEN_CD",                        // 拠点CD
                "KYOTEN_NAME_RYAKU",                // 拠点名
                "ALERT_NUMBER",                     // アラート件数
                "READ_DATA_NUMBER",                 // 読込データ件数
                "DENPYOU_DATE_BEGIN",               // 日付範囲指定 開始
                "DENPYOU_DATE_END",                 // 日付範囲指定 終了
                "DENPYOU_SHURUI",                   // 伝票種類
                "DENPYOU_KUBUN",                    // 伝票区分

                "FILL_COND_1_NAME",                 // 集計項目名1
                "FILL_COND_1_CD_BEGIN",             // 集計項目CD1 開始
                "FILL_COND_1_VALUE_BEGIN",          // 集計項目値1 開始
                "FILL_COND_1_CD_END",               // 集計項目CD1 終了
                "FILL_COND_1_VALUE_END",            // 集計項目値1 終了
                "FILL_COND_2_NAME",                 // 集計項目名2
                "FILL_COND_2_CD_BEGIN",             // 集計項目CD2 開始
                "FILL_COND_2_VALUE_BEGIN",          // 集計項目値2 開始
                "FILL_COND_2_CD_END",               // 集計項目CD2 終了
                "FILL_COND_2_VALUE_END",            // 集計項目値2 終了
                "FILL_COND_3_NAME",                 // 集計項目名3
                "FILL_COND_3_CD_BEGIN",             // 集計項目CD3 開始
                "FILL_COND_3_VALUE_BEGIN",          // 集計項目値3 開始
                "FILL_COND_3_CD_END",               // 集計項目CD3 終了
                "FILL_COND_3_VALUE_END",            // 集計項目値3 終了
                "FILL_COND_4_NAME",                 // 集計項目名4
                "FILL_COND_4_CD_BEGIN",             // 集計項目CD4 開始
                "FILL_COND_4_VALUE_BEGIN",          // 集計項目値4 開始
                "FILL_COND_4_CD_END",               // 集計項目CD4 終了
                "FILL_COND_4_VALUE_END",            // 集計項目値4 終了

                "UNIT_LABEL",                       // 単位ラベル
                "ROW_NO_LABEL",                     // 行Noラベル

                "FILL_COND_1_CD_LABEL",             // 集計項目CD1ラベル
                "FILL_COND_1_NAME_LABEL",           // 集計項目名1ラベル
                "FILL_COND_2_CD_LABEL",             // 集計項目CD2ラベル
                "FILL_COND_2_NAME_LABEL",           // 集計項目名2ラベル
                "FILL_COND_3_CD_LABEL",             // 集計項目CD3ラベル
                "FILL_COND_3_NAME_LABEL",           // 集計項目名3ラベル
                "FILL_COND_4_CD_LABEL",             // 集計項目CD4ラベル
                "FILL_COND_4_NAME_LABEL",           // 集計項目名4ラベル

                "DENPYOU_NUMBER_LABEL",             // 伝票番号ラベル
                "DENPYOU_DATE_LABEL",               // 伝票日付ラベル
                
                "OUTPUT_DENPYOU_1_LABEL",           // 出力（伝票）項目1ラベル
                "OUTPUT_DENPYOU_1_ALIGN",           // 出力（伝票）項目1配置区分
                "OUTPUT_DENPYOU_2_LABEL",           // 出力（伝票）項目2ラベル
                "OUTPUT_DENPYOU_2_ALIGN",           // 出力（伝票）項目2配置区分
                "OUTPUT_DENPYOU_3_LABEL",           // 出力（伝票）項目3ラベル
                "OUTPUT_DENPYOU_3_ALIGN",           // 出力（伝票）項目3配置区分
                "OUTPUT_DENPYOU_4_LABEL",           // 出力（伝票）項目4ラベル
                "OUTPUT_DENPYOU_4_ALIGN",           // 出力（伝票）項目4配置区分
                "OUTPUT_DENPYOU_5_LABEL",           // 出力（伝票）項目5ラベル
                "OUTPUT_DENPYOU_5_ALIGN",           // 出力（伝票）項目5配置区分
                "OUTPUT_DENPYOU_6_LABEL",           // 出力（伝票）項目6ラベル
                "OUTPUT_DENPYOU_6_ALIGN",           // 出力（伝票）項目6配置区分
                "OUTPUT_DENPYOU_7_LABEL",           // 出力（伝票）項目7ラベル
                "OUTPUT_DENPYOU_7_ALIGN",           // 出力（伝票）項目7配置区分
                "OUTPUT_DENPYOU_8_LABEL",           // 出力（伝票）項目8ラベル
                "OUTPUT_DENPYOU_8_ALIGN",           // 出力（伝票）項目8配置区分
               
                "OUTPUT_MEISAI_1_LABEL",            // 出力（明細）項目1ラベル
                "OUTPUT_MEISAI_1_ALIGN",            // 出力（明細）項目1配置区分
                "OUTPUT_MEISAI_2_LABEL",            // 出力（明細）項目2ラベル
                "OUTPUT_MEISAI_2_ALIGN",            // 出力（明細）項目2配置区分
                "OUTPUT_MEISAI_3_LABEL",            // 出力（明細）項目3ラベル
                "OUTPUT_MEISAI_3_ALIGN",            // 出力（明細）項目3配置区分
                "OUTPUT_MEISAI_4_LABEL",            // 出力（明細）項目4ラベル
                "OUTPUT_MEISAI_4_ALIGN",            // 出力（明細）項目4配置区分
                "OUTPUT_MEISAI_5_LABEL",            // 出力（明細）項目5ラベル
                "OUTPUT_MEISAI_5_ALIGN",            // 出力（明細）項目5配置区分
                "OUTPUT_MEISAI_6_LABEL",            // 出力（明細）項目6ラベル
                "OUTPUT_MEISAI_6_ALIGN",            // 出力（明細）項目6配置区分
                "OUTPUT_MEISAI_7_LABEL",            // 出力（明細）項目7ラベル
                "OUTPUT_MEISAI_7_ALIGN",            // 出力（明細）項目7配置区分
                "OUTPUT_MEISAI_8_LABEL",            // 出力（明細）項目8ラベル
                "OUTPUT_MEISAI_8_ALIGN",            // 出力（明細）項目8配置区分

                "OUTPUT_YEAR_MONTH_1_LABEL",        // 出力年月1ラベル
                "OUTPUT_YEAR_MONTH_2_LABEL",        // 出力年月2ラベル
                "OUTPUT_YEAR_MONTH_3_LABEL",        // 出力年月3ラベル
                "OUTPUT_YEAR_MONTH_4_LABEL",        // 出力年月4ラベル
                "OUTPUT_YEAR_MONTH_5_LABEL",        // 出力年月5ラベル
                "OUTPUT_YEAR_MONTH_6_LABEL",        // 出力年月6ラベル
                "OUTPUT_YEAR_MONTH_7_LABEL",        // 出力年月7ラベル
                "OUTPUT_YEAR_MONTH_8_LABEL",        // 出力年月8ラベル
                "OUTPUT_YEAR_MONTH_9_LABEL",        // 出力年月9ラベル
                "OUTPUT_YEAR_MONTH_10_LABEL",       // 出力年月10ラベル
                "OUTPUT_YEAR_MONTH_11_LABEL",       // 出力年月11ラベル
                "OUTPUT_YEAR_MONTH_12_LABEL",       // 出力年月12ラベル

                "OUTPUT_YEAR_MONTH_TOTAL_LABEL",    // 出力年月合計ラベル
                
                "OUTPUT_ITEM_1_LABEL",              // 出力項目1ラベル
                "OUTPUT_ITEM_2_LABEL",              // 出力項目2ラベル
                "OUTPUT_ITEM_3_LABEL",              // 出力項目3ラベル
                "OUTPUT_ITEM_4_LABEL",              // 出力項目4ラベル
                
                "KINGAKU_TOTAL_LABEL",              // 金額計ラベル
                "TAX_TOTAL_LABEL",                  // 消費税ラベル
                "TOTAL_LABEL",                      // 総合計ラベル
                "DENPYOU_TOTAL_LABEL",              // 伝票合計ラベル
                
                "FILL_COND_ID_1_TOTAL_LABEL",       // 集計項目1合計ラベル
                "FILL_COND_ID_2_TOTAL_LABEL",       // 集計項目2合計ラベル
                "FILL_COND_ID_3_TOTAL_LABEL",       // 集計項目3合計ラベル
                "FILL_COND_ID_4_TOTAL_LABEL",       // 集計項目4合計ラベル
                
                "ALL_TOTAL_LABEL",                  // 総合計ラベル
            };

                #endregion - Field Name Array -

                // フィールド名設定
                foreach (string field in fieldNameArray)
                {
                    datatable.Columns.Add(field);
                }

                DataRow row = datatable.NewRow();

                foreach (string field in fieldNameArray)
                {
                    row[field] = string.Empty;
                }

                datatable.Rows.Add(row);

                // データテーブルリストに追加
                this.DataTableList.Add("Header", datatable);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - ReportInfoCommon -

    #endregion - Classes -
}
