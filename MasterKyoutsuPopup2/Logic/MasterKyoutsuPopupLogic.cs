
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using MasterKyoutsuPopup2.APP;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.CustomControl;
using r_framework.OriginalException;
using System.Text;
using Seasar.Framework.Exceptions;

namespace MasterKyoutsuPopup2.Logic
{
    public class MasterKyoutsuPopupLogic
    {
        internal SuperEntity[] entity { get; set; }

        private MasterKyoutsuPopupForm form;

        private IS2Dao dao;

        private List<string> headerList { get; set; }

        internal Control[] popupViewControls { get; set; }

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
        internal bool WindowInit()
        {
            try
            {
                this.form.KeyPreview = true;

                this.SetHeaderText();

                this.EventInit();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            // 条件入力ボタン(F5)イベント生成
            this.form.bt_func5.Click += new EventHandler(this.form.InputCondition);

            // 検索ボタン(F8)イベント生成
            this.form.bt_func8.Click += new EventHandler(this.form.Filter);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);
        }

        /// <summary>
        /// DataGridViewにデータをセット
        /// </summary>
        /// <param name="sourceDT">マスタデータを格納したデータテーブル</param>
        /// <param name="columnsHeaderTitle">Gridのタイトル列テキスト</param>
        internal bool DataGridViewLoad(DataTable sourceDT, string[] columnsHeaderTitle)
        {
            try
            {
                LogUtility.DebugMethodStart(sourceDT, columnsHeaderTitle);

                // データテーブルが空で渡された場合、空行を追加する
                if (columnsHeaderTitle != null && sourceDT.Columns.Count == 0)
                {
                    for (int i = 0; i < columnsHeaderTitle.Length; i++)
                    {
                        sourceDT.Columns.Add(new DataColumn());
                    }
                }

                DataTable stringDT = GetStringDataTable(sourceDT);
                //先頭にブランク行の追加を行う
                DataRow row = stringDT.NewRow();
                for (int i = 0; i < stringDT.Columns.Count; i++)
                {
                    //先頭にブランク行の追加を行うためにEmptyにて初期化する
                    row[i] = string.Empty;
                }
                stringDT.Rows.InsertAt(row, 0);

                //DataGridViewへデータの設定
                this.form.customDataGridView1.DataSource = stringDT;

                // 列タイトル設定
                if (columnsHeaderTitle != null)
                {
                    for (var i = 0; i < columnsHeaderTitle.Length; i++)
                    {
                        this.form.customDataGridView1.Columns[i].HeaderText = columnsHeaderTitle[i];
                    }
                }

                foreach (DataGridViewRow Rows in this.form.customDataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in Rows.Cells)
                    {
                        cell.ReadOnly = true;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DataGridViewLoad", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// マスタデータの検索を行い加工する
        /// </summary>
        internal bool MasterSearch()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = new DataTable();
                if (!this.form.IsMasterAccessStartUp)
                {
                    if (this.form.popupWindowSetting != null && this.form.popupWindowSetting.Count > 0)
                    {
                        if (this.form.popupWindowSetting[0].IsCheckLeftTable != null || this.form.popupWindowSetting[0].IsCheckRightTable != null)
                        {
                            // JoinMethodDtoが一つでもセットされており、IsCheckプロパティのどちらかがnull以外のとき
                            var whereSql = SqlCreateUtility.CreatePopupSql2(this.form.popupWindowSetting, this.form.ParentControls);
                            // 運転者休動マスタと一致しないRecordを検索結果として表示する。
                            if (this.form.WindowId == WINDOW_ID.M_SHAIN_CLOSED && this.form.PopupSearchSendParams != null)
                            {
                                whereSql += AddSql();
                            }
                            dt = this.dao.GetAllMasterDataForPopup(whereSql);
                        }
                        else
                        {
                            // SqlCreateUtility.CreatePopupSql2が実行されなかった場合
                            var whereSql = SqlCreateUtility.CreatePopupSql(this.form.popupWindowSetting, this.form.WindowId, this.form.ParentControls);
                            if (this.form.WindowId == WINDOW_ID.M_SHAIN_CLOSED && this.form.PopupSearchSendParams != null)
                            {
                                whereSql += AddSql();
                            }
                            dt = this.dao.GetAllMasterDataForPopup(whereSql);
                        }
                    }
                    else
                    {
                        // SqlCreateUtility.CreatePopupSql2が実行されなかった場合
                        var whereSql = SqlCreateUtility.CreatePopupSql(this.form.popupWindowSetting, this.form.WindowId, this.form.ParentControls);
                        if (this.form.WindowId == WINDOW_ID.M_SHAIN_CLOSED && this.form.PopupSearchSendParams != null)
                        {
                            whereSql += AddSql();
                        }
                        dt = this.dao.GetAllMasterDataForPopup(whereSql);
                    }
                }
                else
                {
                    dt = this.dao.GetAllValidDataForPopUp(this.form.Entity);
                }

                // 拠点マスタの場合、拠点コードの0埋めを行う
                if (this.form.WindowId == WINDOW_ID.M_KYOTEN)
                {
                    dt = this.CodeZeroPadding(dt);
                }

                this.DataGridViewLoad(dt, this.headerList.ToArray());

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #region 絞込み
        /// <summary>
        /// F8検索処理
        /// </summary>
        internal bool Filter()
        {
            try
            {
                if (this.form.customDataGridView1 == null
                    || this.form.customDataGridView1.DataSource == null)
                {
                    return false;
                }

                var dataSource = this.form.customDataGridView1.DataSource as DataTable;

                if (dataSource == null)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.form.CONDITION_TEXT.Text))
                {
                    // フィルタを解除
                    dataSource.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    if (dataSource.Columns.Count < 1)
                    {
                        return false;
                    }
                    //20150807 hoanghm edit #11762
                    string condition = this.ReplaceSpecialCharacers(this.form.CONDITION_TEXT.Text);//this.form.CONDITION_TEXT.Text.ToString().Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]");
                    //20150807 hoanghm end edit #11762
                    if (dataSource.Columns.Count >= 2)
                    {
                        // 二列以降ある場合は、CD + NAMEの形なので、2列目を絞り込みする
                        dataSource.DefaultView.RowFilter = string.Format("{0} LIKE ('%{1}%')", dataSource.Columns[1].ToString(), condition);
                        this.form.customDataGridView1.Focus();
                    }
                    else
                    {
                        // それ以外は一列目を絞込み。もし列を呼び出し元から動的に変えたい場合はFWの修正が必要
                        dataSource.DefaultView.RowFilter = string.Format("{0} LIKE ('%{1}%')", dataSource.Columns[0].ToString(), condition);
                        this.form.customDataGridView1.Focus();
                    }
                }

                this.form.customDataGridView1.DataSource = dataSource;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Filter", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        #endregion

        /// <summary>
        /// 先頭カラムが文字列のDataTableの取得
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetStringDataTable(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

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

            LogUtility.DebugMethodEnd(table);

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
                case WINDOW_ID.M_SHAIN_CLOSED:
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
                    break;
                case WINDOW_ID.M_ZAIKO_HINMEI:
                    this.dao = DaoInitUtility.GetComponent<IM_ZAIKO_HINMEIDao>();
                    break;
                case WINDOW_ID.M_DENSHI_SHINSEI_JYUYOUDO:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_SHINSEI_JYUYOUDODao>();
                    break;
                case WINDOW_ID.M_DENSHI_SHINSEI_NAIYOU:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_SHINSEI_NAIYOUDao>();
                    break;
                case WINDOW_ID.M_DENSHI_SHINSEI_ROUTE_NAME:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_SHINSEI_ROUTE_NAMEDao>();
                    break;
                case WINDOW_ID.M_DENSHI_HAIKI_SHURUI:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUIDao>();
                    break;
                case WINDOW_ID.M_DENSHI_HAIKI_SHURUI_SAIBUNRUI:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUI_SAIBUNRUIDao>();
                    break;
                case WINDOW_ID.M_CHIIKIBETSU_BUNRUI:
                    this.dao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_BUNRUIDao>();
                    break;
                case WINDOW_ID.M_CHIIKIBETSU_SHOBUN:
                    this.dao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_SHOBUNDao>();
                    break;
                case WINDOW_ID.M_UNCHIN_HINMEI:
                    this.dao = DaoInitUtility.GetComponent<IM_UNCHIN_HINMEIDao>();
                    break;
                case WINDOW_ID.M_JISSEKI_BUNRUI:
                    this.dao = DaoInitUtility.GetComponent<IM_JISSEKI_BUNRUIDao>();
                    break;
                case WINDOW_ID.M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_KEIYAKU_SHANAI_KEIRO_NAMEDao>();
                    break;
                case WINDOW_ID.M_GENBAMEMO_BUNRUI:
                    this.dao = DaoInitUtility.GetComponent<IM_GENBAMEMO_BUNRUIDao>();
                    break;
                case WINDOW_ID.M_GENBAMEMO_HYOUDAI:
                    this.dao = DaoInitUtility.GetComponent<IM_GENBAMEMO_HYOUDAIDao>();
                    break;

                //20250319
                case WINDOW_ID.M_GURUPU_NYURYOKU:
                    this.dao = DaoInitUtility.GetComponent<IM_GURUPU_NYURYOKUDao>();
                    break;

                //20250408
                case WINDOW_ID.M_BIKO_PATAN_NYURYOKU:
                    this.dao = DaoInitUtility.GetComponent<IM_BIKO_PATAN_NYURYOKUDao>();
                    break;
            }
        }

        /// <summary>
        /// multiRowHeaderの初期化を行う
        /// </summary>
        private void SetHeaderText()
        {
            string title = string.Empty;

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
                    headerList.Add("容器重量");
                    title = "容器検索";
                    break;
                case WINDOW_ID.M_KEITAI_KBN:
                    headerList.Add("形態区分CD");
                    headerList.Add("形態区分名");
                    title = "形態区分検索";
                    break;
                case WINDOW_ID.M_SHAIN:
                case WINDOW_ID.M_SHAIN_CLOSED:
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
                    headerList.Add("品名");
                    title = "品名検索";
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

                case WINDOW_ID.M_ZAIKO_HINMEI:
                    headerList.Add("在庫品名CD");
                    headerList.Add("在庫品名");
                    headerList.Add("単価");
                    title = "在庫品名検索";
                    break;

                case WINDOW_ID.M_DENSHI_SHINSEI_JYUYOUDO:
                    headerList.Add("重要度CD");
                    headerList.Add("重要度名");
                    title = "重要度検索";
                    break;

                case WINDOW_ID.M_DENSHI_SHINSEI_NAIYOU:
                    headerList.Add("内容名CD");
                    headerList.Add("内容名");
                    title = "内容名検索";
                    break;

                case WINDOW_ID.M_DENSHI_SHINSEI_ROUTE_NAME:
                    headerList.Add("経路CD");
                    headerList.Add("経路名");
                    title = "経路検索";
                    break;

                case WINDOW_ID.M_DENSHI_HAIKI_SHURUI:
                    headerList.Add("廃棄物種類CD");
                    headerList.Add("廃棄物種類名");
                    title = "電子廃棄物種類検索";
                    break;

                case WINDOW_ID.M_DENSHI_HAIKI_SHURUI_SAIBUNRUI:
                    headerList.Add("廃棄物種類細分類CD");
                    headerList.Add("廃棄物種類名");
                    title = "電子廃棄物種類細分類検索";
                    break;

                case WINDOW_ID.M_CHIIKIBETSU_BUNRUI:
                    headerList.Add("報告書分類CD");
                    headerList.Add("報告分類名");
                    title = "地域別分類検索";
                    break;

                case WINDOW_ID.M_CHIIKIBETSU_SHOBUN:
                    headerList.Add("処分方法CD");
                    headerList.Add("報告処分方法名");
                    title = "地域別処分検索";
                    break;

                case WINDOW_ID.M_UNCHIN_HINMEI:
                    headerList.Add("運賃品名CD");
                    headerList.Add("運賃品名");
                    title = "運賃品名検索";
                    break;

                case WINDOW_ID.M_JISSEKI_BUNRUI:
                    headerList.Add("実績分類CD");
                    headerList.Add("実績分類名");
                    title = "実績分類検索";
                    break;

                case WINDOW_ID.M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME:
                    headerList.Add("社内経路名CD");
                    headerList.Add("社内経路名");
                    title = "社内経路名（電子）検索";
                    break;

                case WINDOW_ID.M_GENBAMEMO_BUNRUI:
                    headerList.Add("現場メモ分類CD");
                    headerList.Add("現場メモ分類名");
                    title = "現場メモ分類検索";
                    break;
                
                case WINDOW_ID.M_GENBAMEMO_HYOUDAI:
                    headerList.Add("現場メモ表題CD");
                    headerList.Add("現場メモ表題名");
                    title = "現場メモ表題検索";
                    break;
                
                //20250319
                case WINDOW_ID.M_GURUPU_NYURYOKU:
                    headerList.Add("グループCD");
                    headerList.Add("グループ名");
                    title = "グループ検索";
                    break;

                //20250408
                case WINDOW_ID.M_BIKO_PATAN_NYURYOKU:
                    headerList.Add("備考パターンCD");
                    headerList.Add("備考パターン名");
                    title = "備考パターン検索";
                    break;

            }

            //タイトルラベルの強制変更対応
            if (!string.IsNullOrEmpty(this.form.PopupTitleLabel))
            {
                title = this.form.PopupTitleLabel;
                this.form.lb_title.Text = title;
                this.form.Text = title;

                ControlUtility.AdjustTitleSize(this.form.lb_title, this.TitleMaxWidth);
            }
            else
            {
                this.form.lb_title.Text = title;
                this.form.Text = title;
            }

        }

        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 658;

        internal bool ElementDecision()
        {
            try
            {
                LogUtility.DebugMethodStart();

                Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
                List<PopupReturnParam> setParam;

                int i = 0;

                // M_GENBAMEMO_HYOUDAIの場合、2つ目の要素のみ返却する
                if (this.form.WindowId.Equals(WINDOW_ID.M_GENBAMEMO_HYOUDAI))
                {
                    i = 1;
                }                

                for ( ; i < this.form.customDataGridView1.Columns.Count; i++)
                {
                    PopupReturnParam popupParam = new PopupReturnParam();
                    var setDate = this.form.customDataGridView1[i, this.form.customDataGridView1.CurrentRow.Index];

                    //var control = setDate as ICustomControl;
                    var control = setDate as DataGridViewTextBoxCell;

                    popupParam.Key = "Value";

                    popupParam.Value = setDate.Value.ToString();

                    if (setParamList.ContainsKey(i))
                    {
                        setParam = setParamList[i];
                    }
                    else
                    {
                        setParam = new List<PopupReturnParam>();
                    }

                    setParam.Add(popupParam);


                    setParamList.Add(i, setParam);
                }

                this.form.ReturnParams = setParamList;
                this.form.Close();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElementDecision", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// Int型のコードを0埋めします。
        /// （今のところ拠点CDのみ）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable CodeZeroPadding(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            int cdIndex = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "CD" || dt.Columns[i].ColumnName == "KYOTEN_CD" || dt.Columns[i].ColumnName == "M_KYOTEN.KYOTEN_CD")
                {
                    cdIndex += i + 1;
                    break;
                }
            }
            if (cdIndex == -1)
            {
                return dt;
            }
            DataTable resultDt = dt.Clone();
            resultDt.Columns[cdIndex].DataType = typeof(string);
            string paddingCd = null;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = resultDt.NewRow();
                paddingCd = int.Parse(dt.Rows[i][cdIndex].ToString()).ToString("00");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j != cdIndex)
                    {
                        row[j] = dt.Rows[i][j];
                    }
                    else
                    {
                        row[j] = paddingCd;
                    }
                }
                resultDt.Rows.Add(row);
            }

            LogUtility.DebugMethodEnd(resultDt);
            return resultDt;
        }
        
        /// <summary>
        /// グリッドビューの表示を整えます
        /// （セル内折り返し、セル縦横幅調整）
        /// </summary>
        internal bool CordinateGridSize()
        {
            try
            {
                LogUtility.DebugMethodStart();

                int columnCount = this.form.customDataGridView1.ColumnCount;
                // 列数0の場合は何もしない
                if (columnCount == 0)
                {
                    return false;
                }

                // 垂直スクロールバーのプロパティを取得
                var pi = this.form.customDataGridView1.GetType().GetProperty("VerticalScrollBar", BindingFlags.NonPublic | BindingFlags.Instance);
                var vsb = (VScrollBar)pi.GetValue(this.form.customDataGridView1, null);

                // セルを表示可能な領域の横幅を計算する
                int dgvWidth = this.form.customDataGridView1.Width;
                if (vsb.Visible)
                {
                    dgvWidth -= vsb.Width;
                }

                // 各カラムの横幅を指定する
                int columnWidth = (dgvWidth / columnCount) - SystemInformation.BorderSize.Width; // 線の太さを考慮
                for (int i = 0; i < columnCount; i++)
                {
                    this.form.customDataGridView1.Columns[i].Width = columnWidth; // 横幅指定
                    this.form.customDataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // ヘッダ中央揃え
                }
                // 余りを1列目に足す
                this.form.customDataGridView1.Columns[0].Width += dgvWidth % columnCount;

                // セル内折り返し(半角英数字の羅列は折り返されない仕様)
                this.form.customDataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // 縦幅は自動調節
                this.form.customDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                // 画面サイズを調整
                this.SettingFormSize();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CordinateGridSize", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 休動検索条件を作成する
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string AddSql()
        {
            string sql = string.Empty;
            foreach (var dto in this.form.PopupSearchSendParams)
            {
                object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { dto.Control });
                if (dto.KeyName == "SAGYOU_DATE" || dto.KeyName == "DENPYOU_DATE")
                {
                    sql += " AND NOT EXISTS( ";
                    sql += "SELECT 1 FROM M_WORK_CLOSED_UNTENSHA T2 ";
                    sql += "WHERE M_SHAIN.SHAIN_CD = T2.SHAIN_CD ";
                    sql += string.Format("AND CONVERT(CHAR(10), T2.CLOSED_DATE, 111) = CONVERT(CHAR(10), '{0}', 111) ", PropertyUtility.GetTextOrValue(control[0]));
                    sql += "AND M_SHAIN.DELETE_FLG = 0 ";
                    sql += "AND T2.DELETE_FLG = 0) ";
                }
            }
            return sql;
        }

        internal void CreateMultiRowTemplate(DataTable setDatas)
        {

        }

        private string ReplaceSpecialCharacers(string value)
        {
            StringBuilder sb = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                switch (c)
                {
                    case ']':
                    case '[':
                    case '%':
                    case '*':
                        sb.Append("[").Append(c).Append("]");
                        break;
                    case '\'':
                        sb.Append("''");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 画面のサイズを調整する。
        /// </summary>
        private void SettingFormSize()
        {
            // 現場メモ表題マスタの場合
            if (this.form.WindowId.Equals(WINDOW_ID.M_GENBAMEMO_HYOUDAI))
            {
                this.form.customDataGridView1.Columns[0].Width = 120;
                this.form.customDataGridView1.Columns[1].Width = 300;
                this.form.Size = new System.Drawing.Size(480, 490);
                this.form.bt_func5.Location = new System.Drawing.Point(12, 405);
                this.form.bt_func8.Location = new System.Drawing.Point(98, 405);
            }
        }
    }
}
