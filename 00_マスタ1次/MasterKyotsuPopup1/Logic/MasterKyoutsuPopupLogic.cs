using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MasterKyoutsuPopup1.APP;
using MasterKyoutsuPopup1.Const;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;

namespace MasterKyoutsuPopup1.Logic
{
    public class MasterKyoutsuPopupLogic
    {
        internal SuperEntity[] entity { get; set; }

        private MasterKyoutsuPopupForm form;

        private IS2Dao dao;

        private List<string> headerList { get; set; }

        internal Control[] popupViewControls { get; set; }

        /// <summary>
        /// 適用開始/終了、削除フラグを持っていないテーブルかどうか
        /// </summary>
        private bool hasNotTekiyouTable = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MasterKyoutsuPopupLogic(MasterKyoutsuPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            headerList = new List<string>();
            this.form = targetForm;
            this.DaoSetting();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal void WindowInit()
        {
            this.SetHeaderText();
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);
        }

        /// <summary>
        /// マスタデータの検索を行い加工する
        /// </summary>
        internal void MasterSearch()
        {
            var createLogic = new MultiRowCreateLogic();
            DataTable dt = new DataTable();
            if (this.form.table == null)
            {
                if (!this.form.IsMasterAccessStartUp)
                {
                    var whereSql = SqlCreateUtility.CreatePopupSql(this.form.popupWindowSetting, this.form.WindowId,this.form.ParentControls);
                    dt = this.dao.GetAllMasterDataForPopup(whereSql);
                }
                else
                {
                    dt = this.dao.GetAllValidDataForPopUp(this.form.Entity);
                }
                DataTable table = GetStringDataTable(dt);

                this.form.masterDetail.Template = createLogic.CreateMultiRow(headerList, table);

                //先頭にブランク行の追加を行う
                DataRow row = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {

                    var colmun = table.Columns[i];
                    //先頭にブランク行の追加を行うためにEmptyにて初期化する
                    row[i] = string.Empty;
                }
                table.Rows.InsertAt(row, 0);

                // カラム非表示設定
                foreach (var colHeaderCell in this.form.masterDetail.ColumnHeaders[0].Cells)
                {
                    colHeaderCell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
                }

                //MultiRowへデータの設定
                this.form.masterDetail.DataSource = table;

                foreach (var Rows in this.form.masterDetail.Rows)
                {
                    Rows.Cells[0].Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
                    foreach (var cell in Rows.Cells)
                    {
                        cell.ReadOnly = true;
                    }
                }
            }
            else
            {
                this.form.masterDetail.Template = createLogic.CreateMultiRow(headerList, this.form.table);

                // MultiRowへの取得済データの設定
                this.form.masterDetail.DataSource = this.form.table;

                // カラム非表示設定
                foreach (var colHeaderCell in this.form.masterDetail.ColumnHeaders[0].Cells)
                {
                    colHeaderCell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
                    if (colHeaderCell.Name.Contains(ConstCls.STR_VISIBLE_FALSE))
                    {
                        colHeaderCell.Visible = false;
                    }
                }

                // 読取専用設定
                foreach (var Rows in this.form.masterDetail.Rows)
                {
                    Rows.Cells[0].Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
                    foreach (var cell in Rows.Cells)
                    {
                        cell.ReadOnly = true;

                        // セル非表示設定
                        if (cell.Name.Contains(ConstCls.STR_VISIBLE_FALSE))
                        {
                            cell.Visible = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 先頭カラムが文字列のDataTableの取得
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetStringDataTable(DataTable dt)
        {
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            return table;
        }

        /// <summary>
        /// Daoの初期化を行う
        /// </summary>
        private void DaoSetting()
        {
            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_BUSHO:
                    this.dao = DaoInitUtility.GetComponent<IM_BUSHODao>();
                    break;
                case WINDOW_ID.M_UNIT:
                    this.dao = DaoInitUtility.GetComponent<IM_UNITDao>();
                    break;
                case WINDOW_ID.M_BANK:
                    this.dao = DaoInitUtility.GetComponent<IM_BANKDao>();
                    break;
                case WINDOW_ID.M_GYOUSHU:
                    this.dao = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
                    break;
                case WINDOW_ID.M_CONTENA_SHURUI:
                    this.dao = DaoInitUtility.GetComponent<IM_CONTENA_SHURUIDao>();
                    break;
                case WINDOW_ID.M_KYOTEN:
                    this.dao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                    break;
                case WINDOW_ID.M_NYUUSHUKKIN_KBN:
                    this.dao = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();
                    break;
                case WINDOW_ID.M_TODOUFUKEN:
                    this.dao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
                    break;
                case WINDOW_ID.M_SHASHU:
                    this.dao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
                    break;
                case WINDOW_ID.M_BUNRUI:
                    this.dao = DaoInitUtility.GetComponent<IM_BUNRUIDao>();
                    break;
                case WINDOW_ID.M_CHIIKI:
                    this.dao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
                    break;
                case WINDOW_ID.M_SHOBUN_HOUHOU:
                    this.dao = DaoInitUtility.GetComponent<IM_SHOBUN_HOUHOUDao>();
                    break;
                case WINDOW_ID.M_SHOBUN_MOKUTEKI:
                    this.dao = DaoInitUtility.GetComponent<IM_SHOBUN_MOKUTEKIDao>();
                    break;
                case WINDOW_ID.M_UNPAN_HOUHOU:
                    this.dao = DaoInitUtility.GetComponent<IM_UNPAN_HOUHOUDao>();
                    break;
                case WINDOW_ID.M_YOUKI:
                    this.dao = DaoInitUtility.GetComponent<IM_YOUKIDao>();
                    break;
                case WINDOW_ID.M_KEITAI_KBN:
                    this.dao = DaoInitUtility.GetComponent<IM_KEITAI_KBNDao>();
                    break;
                case WINDOW_ID.M_SHAIN:
                case WINDOW_ID.M_EIGYOU_TANTOUSHA:
                case WINDOW_ID.M_NYUURYOKU_TANTOUSHA:
                case WINDOW_ID.M_SHOBUN_TANTOUSHA:
                case WINDOW_ID.M_TEGATA_HOKANSHA:
                case WINDOW_ID.M_UNTENSHA:
                    this.dao = DaoInitUtility.GetComponent<IM_SHAINDao>();
                    break;
                case WINDOW_ID.M_DENPYOU_KBN:
                    this.dao = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>();
                    break;
                case WINDOW_ID.M_MANIFEST_SHURUI:
                    this.dao = DaoInitUtility.GetComponent<IM_MANIFEST_SHURUIDao>();
                    break;
                case WINDOW_ID.M_MANIFEST_TEHAI:
                    this.dao = DaoInitUtility.GetComponent<IM_MANIFEST_TEHAIDao>();
                    break;
                case WINDOW_ID.M_GENCHAKU_TIME:
                    this.dao = DaoInitUtility.GetComponent<IM_GENCHAKU_TIMEDao>();
                    break;
                case WINDOW_ID.M_CONTENA_JOUKYOU:
                    this.dao = DaoInitUtility.GetComponent<IM_CONTENA_JOUKYOUDao>();
                    break;
                case WINDOW_ID.M_CONTENA_SOUSA:
                    this.dao = DaoInitUtility.GetComponent<IM_CONTENA_SOUSADao>();
                    break;
                case WINDOW_ID.M_KEIJOU:
                    this.dao = DaoInitUtility.GetComponent<IM_KEIJOUDao>();
                    break;
                case WINDOW_ID.M_NISUGATA:
                    this.dao = DaoInitUtility.GetComponent<IM_NISUGATADao>();
                    break;
                case WINDOW_ID.M_HOUKOKUSHO_BUNRUI:
                    this.dao = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>();
                    break;
                case WINDOW_ID.M_HAIKI_NAME:
                    this.dao = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>();
                    break;
                case WINDOW_ID.M_HAIKI_KBN:
                    this.dao = DaoInitUtility.GetComponent<IM_HAIKI_KBNDao>();
                    break;
                case WINDOW_ID.M_SHURUI:
                    this.dao = DaoInitUtility.GetComponent<IM_SHURUIDao>();
                    break;
                case WINDOW_ID.M_SHIKUCHOUSON:
                    this.dao = DaoInitUtility.GetComponent<IM_SHIKUCHOUSONDao>();
                    break;
                case WINDOW_ID.M_YUUGAI_BUSSHITSU:
                    this.dao = DaoInitUtility.GetComponent<IM_YUUGAI_BUSSHITSUDao>();
                    break;
                case WINDOW_ID.M_TORIHIKI_KBN:
                    this.dao = DaoInitUtility.GetComponent<IM_TORIHIKI_KBNDao>();
                    break;
                case WINDOW_ID.M_SHUUKEI_KOUMOKU:
                    this.dao = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();
                    break;
                case WINDOW_ID.M_GENNYOURITSU:
                    this.dao = DaoInitUtility.GetComponent<IM_GENNYOURITSUDao>();
                    break;
                case WINDOW_ID.M_HINMEI:
                    this.dao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
                    break;
                case WINDOW_ID.M_DENSHU_KBN:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHU_KBNDao>();
                    break;
                case WINDOW_ID.M_BANK_SHITEN:
                    this.dao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
                    break;
                case WINDOW_ID.M_CONTENA:
                    this.dao = DaoInitUtility.GetComponent<IM_CONTENADao>();
                    break;
                case WINDOW_ID.M_GENBA:
                    this.dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                    break;
                case WINDOW_ID.M_HAIKI_SHURUI:
                    this.dao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();
                    break;
                case WINDOW_ID.M_KANSAN:
                    this.dao = DaoInitUtility.GetComponent<IM_KANSANDao>();
                    break;
                case WINDOW_ID.M_KEIRYOU_CHOUSEI:
                    this.dao = DaoInitUtility.GetComponent<IM_KEIRYOU_CHOUSEIDao>();
                    break;
                case WINDOW_ID.M_KONGOU_SHURUI:
                    this.dao = DaoInitUtility.GetComponent<IM_KONGOU_SHURUIDao>();
                    break;
                case WINDOW_ID.M_MANIFEST_KANSAN:
                    this.dao = DaoInitUtility.GetComponent<IM_MANIFEST_KANSANDao>();
                    break;
                case WINDOW_ID.M_SHARYOU:
                    this.dao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
                    break;
                case WINDOW_ID.M_TORIHIKISAKI:
                    this.dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                    break;
                case WINDOW_ID.M_GYOUSHA:
                    this.dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                    break;
                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
                    this.hasNotTekiyouTable = true;
                    break;
                case WINDOW_ID.M_DENSHI_HAIKI_SHURUI:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUIDao>();
                    break;
                case WINDOW_ID.M_MENU_AUTH_EACH_MENU:
                    this.dao = null;
                    break;
                #region 一般廃用報告書分類
                case WINDOW_ID.M_JISSEKI_BUNRUI:
                    this.dao = DaoInitUtility.GetComponent<IM_JISSEKI_BUNRUIDao>();
                    break;
                #endregion

            }
        }

        /// <summary>
        /// multiRowHeaderの初期化を行う
        /// </summary>
        private void SetHeaderText()
        {
            string title = string.Empty;

            if (this.form.title.Equals(string.Empty))
            {
                switch (this.form.WindowId)
                {
                    case WINDOW_ID.M_BUSHO:
                        headerList.Add("部署CD");
                        headerList.Add("部署名");
                        title = "部署検索";
                        break;
                    case WINDOW_ID.M_UNIT:
                        headerList.Add("単位CD");
                        headerList.Add("単位名");
                        title = "単位検索";
                        break;
                    case WINDOW_ID.M_BANK:
                        headerList.Add("銀行CD");
                        headerList.Add("銀行名");
                        title = "銀行検索";
                        break;
                    case WINDOW_ID.M_GYOUSHU:
                        headerList.Add("業種CD");
                        headerList.Add("業種名");
                        title = "業種検索";
                        break;
                    case WINDOW_ID.M_CONTENA_SHURUI:
                        headerList.Add("コンテナ種類CD");
                        headerList.Add("コンテナ種類名");
                        title = "コンテナ種類検索";
                        break;
                    case WINDOW_ID.M_KYOTEN:
                        headerList.Add("拠点CD");
                        headerList.Add("拠点名");
                        title = "拠点検索";
                        break;
                    case WINDOW_ID.M_NYUUSHUKKIN_KBN:
                        headerList.Add("入出金区分CD");
                        headerList.Add("入出金区分名");
                        title = "入出金区分検索";
                        break;
                    case WINDOW_ID.M_TODOUFUKEN:
                        headerList.Add("都道府県CD");
                        headerList.Add("都道府県名");
                        title = "都道府県検索";
                        break;
                    case WINDOW_ID.M_SHASHU:
                        headerList.Add("車種CD");
                        headerList.Add("車種名");
                        title = "車種検索";

                        break;
                    case WINDOW_ID.M_BUNRUI:
                        headerList.Add("分類CD");
                        headerList.Add("分類名");
                        title = "分類検索";
                        break;
                    case WINDOW_ID.M_CHIIKI:
                        headerList.Add("地域CD");
                        headerList.Add("地域名");
                        title = "地域検索";
                        break;
                    case WINDOW_ID.M_SHOBUN_HOUHOU:
                        headerList.Add("処分方法CD");
                        headerList.Add("処分方法名");
                        title = "処分方法検索";
                        break;
                    case WINDOW_ID.M_SHOBUN_MOKUTEKI:
                        headerList.Add("処分目的CD");
                        headerList.Add("処分目的名");
                        title = "処分目的検索";
                        break;
                    case WINDOW_ID.M_UNPAN_HOUHOU:
                        headerList.Add("運搬方法CD");
                        headerList.Add("運搬方法名");
                        title = "運搬方法検索";
                        break;
                    case WINDOW_ID.M_YOUKI:
                        headerList.Add("容器CD");
                        headerList.Add("容器名");
                        title = "容器検索";
                        break;
                    case WINDOW_ID.M_KEITAI_KBN:
                        headerList.Add("形態区分CD");
                        headerList.Add("形態区分名");
                        title = "形態区分検索";
                        break;
                    case WINDOW_ID.M_SHAIN:
                        headerList.Add("社員CD");
                        headerList.Add("社員名");
                        title = "社員検索";
                        break;
                    case WINDOW_ID.M_EIGYOU_TANTOUSHA:
                        headerList.Add("営業担当者CD");
                        headerList.Add("営業担当者名");
                        title = "営業担当者検索";
                        break;
                    case WINDOW_ID.M_UNTENSHA:
                        headerList.Add("運転者CD");
                        headerList.Add("運転者名");
                        title = "運転者検索";
                        break;
                    case WINDOW_ID.M_SHOBUN_TANTOUSHA:
                        headerList.Add("処分担当者CD");
                        headerList.Add("処分担当者名");
                        title = "処分担当者検索";
                        break;
                    case WINDOW_ID.M_TEGATA_HOKANSHA:
                        headerList.Add("手形保管者CD");
                        headerList.Add("手形保管者名");
                        title = "手形保管者検索";
                        break;
                    case WINDOW_ID.M_NYUURYOKU_TANTOUSHA:
                        headerList.Add("入力担当者CD");
                        headerList.Add("入力担当者名");
                        title = "入力担当者検索";
                        break;
                    case WINDOW_ID.M_DENPYOU_KBN:
                        headerList.Add("伝票区分CD");
                        headerList.Add("伝票区分名");
                        title = "伝票区分検索";
                        break;
                    case WINDOW_ID.M_MANIFEST_SHURUI:
                        headerList.Add("マニフェスト種類CD");
                        headerList.Add("マニフェスト種類名");
                        title = "マニフェスト種類検索";
                        break;
                    case WINDOW_ID.M_MANIFEST_TEHAI:
                        headerList.Add("マニフェスト手配CD");
                        headerList.Add("マニフェスト手配名");
                        title = "マニフェスト手配検索";
                        break;
                    case WINDOW_ID.M_GENCHAKU_TIME:
                        headerList.Add("現着時間CD");
                        headerList.Add("現着時間名");
                        title = "現着時間検索";
                        break;
                    case WINDOW_ID.M_CONTENA_JOUKYOU:
                        headerList.Add("コンテナ状況CD");
                        headerList.Add("コンテナ状況名");
                        title = "コンテナ状況検索";
                        break;
                    case WINDOW_ID.M_CONTENA_SOUSA:
                        headerList.Add("コンテナ操作CD");
                        headerList.Add("コンテナ操作名");
                        title = "コンテナ操作検索";
                        break;
                    case WINDOW_ID.M_KEIJOU:
                        headerList.Add("形状CD");
                        headerList.Add("形状名");
                        title = "形状検索";
                        break;
                    case WINDOW_ID.M_NISUGATA:
                        headerList.Add("荷姿CD");
                        headerList.Add("荷姿名");
                        title = "荷姿検索";
                        break;
                    case WINDOW_ID.M_HOUKOKUSHO_BUNRUI:
                        headerList.Add("報告書分類CD");
                        headerList.Add("報告書分類名");
                        title = "報告書分類検索";
                        break;
                    case WINDOW_ID.M_HAIKI_NAME:
                        headerList.Add("廃棄物名称CD");
                        headerList.Add("廃棄物名");
                        title = "廃棄物名称検索";
                        break;
                    case WINDOW_ID.M_HAIKI_KBN:
                        headerList.Add("廃棄物区分CD");
                        headerList.Add("廃棄物区分名");
                        title = "廃棄物区分検索";
                        break;
                    case WINDOW_ID.M_SHURUI:
                        headerList.Add("種類CD");
                        headerList.Add("種類名");
                        title = "種類検索";
                        break;
                    case WINDOW_ID.M_SHIKUCHOUSON:
                        headerList.Add("市区町村CD");
                        headerList.Add("市区町村名");
                        title = "市区町村検索";
                        break;
                    case WINDOW_ID.M_YUUGAI_BUSSHITSU:
                        headerList.Add("有害物質CD");
                        headerList.Add("有害物質名");
                        title = "有害物質検索";
                        break;
                    case WINDOW_ID.M_TORIHIKI_KBN:
                        headerList.Add("取引区分CD");
                        headerList.Add("取引区分名");
                        title = "取引区分検索";
                        break;
                    case WINDOW_ID.M_SHUUKEI_KOUMOKU:
                        headerList.Add("集計項目CD");
                        headerList.Add("集計項目名");
                        title = "集計項目検索";
                        break;
                    case WINDOW_ID.M_GENNYOURITSU:
                        headerList.Add("報告書分類CD");
                        headerList.Add("廃棄物名称CD");
                        headerList.Add("処分方法CD");
                        headerList.Add("減容率");
                        title = "減溶率検索";
                        break;
                    case WINDOW_ID.M_HINMEI:
                        headerList.Add("品名CD");
                        headerList.Add("品名名");
                        title = "集計項目検索";
                        break;
                    case WINDOW_ID.M_DENSHU_KBN:
                        headerList.Add("伝種区分CD");
                        headerList.Add("伝種区分名");
                        title = "伝種区分検索";
                        break;
                    case WINDOW_ID.M_BANK_SHITEN:
                        if (this.form.IsMasterAccessStartUp)
                        {
                            headerList.Add("銀行CD");
                            headerList.Add("銀行名");
                            headerList.Add("銀行支店CD");
                            headerList.Add("銀行支店名");
                            headerList.Add("口座種類");
                            headerList.Add("口座番号");
                            headerList.Add("口座名義");
                            if (this.form.Entity == null)
                            {
                                this.form.Entity = new M_BANK_SHITEN();
                            }
                        }
                        else
                        {
                            headerList.Add("銀行支店CD");
                            headerList.Add("銀行支店名");
                        }
                        title = "銀行支店検索";
                        break;

                    case WINDOW_ID.M_CONTENA:
                        if (this.form.IsMasterAccessStartUp)
                        {
                            headerList.Add("コンテナ種類CD");
                            headerList.Add("コンテナ種類名");
                            headerList.Add("コンテナCD");
                            headerList.Add("コンテナ名");

                            if (this.form.Entity == null)
                            {
                                this.form.Entity = new M_CONTENA();
                            }
                        }
                        else
                        {
                            headerList.Add("コンテナCD");
                            headerList.Add("コンテナ名");
                        }
                        title = "コンテナ検索";
                        break;

                    case WINDOW_ID.M_GENBA:
                        if (this.form.IsMasterAccessStartUp)
                        {
                            headerList.Add("業者CD");
                            headerList.Add("業者名");
                            headerList.Add("現場CD");
                            headerList.Add("現場名");
                            this.form.IsMasterAccessStartUp = true;
                            if (this.form.Entity == null)
                            {
                                this.form.Entity = new M_GENBA();
                            }
                        }
                        else
                        {
                            headerList.Add("現場CD");
                            headerList.Add("現場名");
                        }
                        title = "現場検索";
                        break;

                    case WINDOW_ID.M_HAIKI_SHURUI:
                        if (this.form.IsMasterAccessStartUp)
                        {
                            headerList.Add("廃棄物区分CD");
                            headerList.Add("廃棄物区分名");
                            headerList.Add("廃棄物種類CD");
                            headerList.Add("廃棄物種類名");

                            this.form.IsMasterAccessStartUp = true;
                            if (this.form.Entity == null)
                            {
                                this.form.Entity = new M_HAIKI_SHURUI();
                            }
                        }
                        else
                        {
                            headerList.Add("廃棄物種類CD");
                            headerList.Add("廃棄物種類名");
                            // 20140602 kayo 不具合#4134 報告書分類表示追加 start
                            headerList.Add("報告書分類CD");
                            headerList.Add("報告書分類名");
                            // 20140602 kayo 不具合#4134 報告書分類表示追加 end
                        }
                        title = "廃棄物種類検索";
                        break;

                    case WINDOW_ID.M_KANSAN:
                        if (this.form.IsMasterAccessStartUp)
                        {
                            headerList.Add("伝票区分CD");
                            headerList.Add("伝票区分名");
                            headerList.Add("品名CD");
                            headerList.Add("品名");
                            headerList.Add("単位CD");
                            headerList.Add("単位名");
                            headerList.Add("換算式");
                            headerList.Add("換算値");

                            this.form.IsMasterAccessStartUp = true;
                            if (this.form.Entity == null)
                            {
                                this.form.Entity = new M_KANSAN();
                            }
                        }
                        else
                        {
                            headerList.Add("伝票区分CD");
                            headerList.Add("品名CD");
                        }
                        title = "換算値検索";
                        break;

                    case WINDOW_ID.M_KEIRYOU_CHOUSEI:
                        if (this.form.IsMasterAccessStartUp)
                        {
                            headerList.Add("取引先CD");
                            headerList.Add("取引先名");
                            headerList.Add("業者CD");
                            headerList.Add("業者名");
                            headerList.Add("現場CD");
                            headerList.Add("現場名");
                            headerList.Add("品名CD");
                            headerList.Add("品名");
                            headerList.Add("単位CD");
                            headerList.Add("単位名");
                            headerList.Add("調整値");
                            headerList.Add("調整割合");

                            this.form.IsMasterAccessStartUp = true;
                            if (this.form.Entity == null)
                            {
                                this.form.Entity = new M_KEIRYOU_CHOUSEI();
                            }
                        }
                        else
                        {
                            headerList.Add("取引先CD");
                            headerList.Add("業者CD");
                        }
                        title = "計量調整検索";
                        break;

                    case WINDOW_ID.M_KONGOU_SHURUI:
                        if (this.form.IsMasterAccessStartUp)
                        {
                            headerList.Add("廃棄物区分CD");
                            headerList.Add("廃棄物区分名");
                            headerList.Add("混合種類CD");
                            headerList.Add("混合種類名");

                            this.form.IsMasterAccessStartUp = true;
                            if (this.form.Entity == null)
                            {
                                this.form.Entity = new M_KONGOU_SHURUI();
                            }
                        }
                        else
                        {
                            headerList.Add("混合種類CD");
                            headerList.Add("混合種類名");
                        }
                        title = "混合種類検索";
                        break;

                    case WINDOW_ID.M_MANIFEST_KANSAN:
                        if (this.form.IsMasterAccessStartUp)
                        {
                            headerList.Add("報告書分類CD");
                            headerList.Add("報告書分類名");
                            headerList.Add("廃棄物名称CD");
                            headerList.Add("廃棄物名称");
                            headerList.Add("単位CD");
                            headerList.Add("単位名");
                            headerList.Add("荷姿CD");
                            headerList.Add("荷姿名");
                            headerList.Add("換算式");
                            headerList.Add("換算値");

                            this.form.IsMasterAccessStartUp = true;
                            if (this.form.Entity == null)
                            {
                                this.form.Entity = new M_MANIFEST_KANSAN();
                            }
                        }
                        else
                        {
                            headerList.Add("報告書分類CD");
                            headerList.Add("廃棄物名称CD");
                        }
                        title = "マニフェスト換算値検索";
                        break;

                    case WINDOW_ID.M_SHARYOU:
                        if (this.form.IsMasterAccessStartUp)
                        {
                            headerList.Add("業者CD");
                            headerList.Add("業者名");
                            headerList.Add("車輌CD");
                            headerList.Add("車輌名");

                            this.form.IsMasterAccessStartUp = true;
                            if (this.form.Entity == null)
                            {
                                this.form.Entity = new M_SHARYOU();
                            }
                        }
                        else
                        {
                            headerList.Add("車輌CD");
                            headerList.Add("車輌名");
                        }
                        title = "車輌検索";
                        break;

                    case WINDOW_ID.M_TORIHIKISAKI:
                        headerList.Add("取引先CD");
                        headerList.Add("取引先名");
                        title = "取引先検索";
                        break;

                    case WINDOW_ID.M_GYOUSHA:
                        headerList.Add("業者CD");
                        headerList.Add("業者名");
                        title = "業者検索";
                        break;

                    case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                        headerList.Add("加入者番号");
                        headerList.Add("事業者名");
                        title = "電子事業者検索";
                        break;

                    case WINDOW_ID.M_DENSHI_HAIKI_SHURUI:
                        headerList.Add("廃棄物種類CD");
                        headerList.Add("廃棄物種類名");
                        title = "電子廃棄物種類検索";
                        break;

                    case WINDOW_ID.M_MENU_AUTH_EACH_MENU:
                        headerList.Add("タブ名");
                        headerList.Add("機能名");
                        headerList.Add("メニューID");
                        headerList.Add("メニュー名");
                        headerList.Add("ウィンドウID" + ConstCls.STR_VISIBLE_FALSE);
                        title = "メニュー検索";
                        break;

                    case WINDOW_ID.M_JISSEKI_BUNRUI:
                        headerList.Add("実績分類CD");
                        headerList.Add("実績分類名");
                        title = "実績分類検索";
                        break;

                }
            }
            else
            {
                title = this.form.title;
                this.headerList.AddRange(this.form.headerList);
            }
            this.form.lb_title.Text = title;

			// Formタイトルの初期化
			this.form.Text = this.form.lb_title.Text;
		}

        internal void ElementDecision()
        {

            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            List<PopupReturnParam> setParam;
            for (int i = 0; i < this.form.masterDetail.Columns.Count; i++)
            {
                PopupReturnParam popupParam = new PopupReturnParam();
                var setDate = this.form.masterDetail[this.form.masterDetail.CurrentRow.Index, i];

                var control = setDate as ICustomControl;

                popupParam.Key = "Value";

                popupParam.Value = setDate.Value.ToString();

                if (setParamList.ContainsKey(int.Parse(control.ShortItemName)))
                {
                    setParam = setParamList[int.Parse(control.ShortItemName)];
                }
                else
                {
                    setParam = new List<PopupReturnParam>();
                }

                setParam.Add(popupParam);


                setParamList.Add(int.Parse(control.ShortItemName), setParam);
            }

            this.form.ReturnParams = setParamList;
            this.form.Close();
        }

        internal void CreateMultiRowTemplate(DataTable setDatas)
        {

        }
    }
}
