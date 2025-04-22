using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

// MEMO:
// 実際の連携機能があるのは1.社員、2.現場のみ
// 3.出発現場、荷降現場、4.車種については連携しない
// ※APIが用意されていないのでそもそも連携が不可
//   NAVITIMEで作成したものと同じコードのデータをこの画面でも作ってもらう運用になる

// また、連携機能に関して2.現場のみ「連携確認」を後から追加した都合上、かなり煩雑になってしまっている
// 時間があるならリファクタリングしたいところ…

namespace Shougun.Core.ExternalConnection.NaviTimeMasterRenkei
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.NaviTimeMasterRenkei.Setting.ButtonSetting.xml";

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        /// <summary>
        /// 社員出力済みDao
        /// </summary>
        private IM_NAVI_OUTPUT_SHAINDao daoShainTS;
        /// <summary>
        /// 現場出力済みDao
        /// </summary>
        private IM_NAVI_OUTPUT_GENBADao daoGenbaTS;
        /// <summary>
        /// 出発現場、荷降現場出力済みDao
        /// </summary>
        private IM_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBADao daoShuppatsuGenbaTS;
        /// <summary>
        /// 車種出力済みDao
        /// </summary>
        private IM_NAVI_OUTPUT_SHASHUDao daoShashuTS;
        /// <summary>
        /// NAVITIME接続管理Dao
        /// </summary>
        private IS_NAVI_CONNECTDao naviConnectDao;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        #endregion

        #region プロパティ

        /// <summary>
        /// 表示用のDGV
        /// </summary>
        internal DataGridView DispGridView { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        private DataTable SearchResult { get; set; }

        /// <summary>
        /// 抽出に使用するテーブル名
        /// </summary>
        private string TblName { get; set; }

        internal string chkColumn1 { get; set; }
        private string chkColumn2 { get; set; }
        internal string chkColumn3 { get; set; }
        internal string chkColumnName1 { get; set; }
        private string chkColumnName2 { get; set; }
        internal string chkColmnPK1 { get; set; }
        internal string chkColmnPK2 { get; set; }

        /// <summary>
        /// 抽出条件保存用
        /// </summary>
        internal int renkeiMaster { get; set; }
        internal int renkeiKouho { get; set; }
        internal int kensakuJoken { get; set; }

        /// <summary>
        /// 訪問先連携のみ使用する
        /// </summary>
        private int processingId { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            // DAO(一覧用)
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();
            // 登録に使用するDAO
            this.daoShainTS = DaoInitUtility.GetComponent<IM_NAVI_OUTPUT_SHAINDao>();
            this.daoGenbaTS = DaoInitUtility.GetComponent<IM_NAVI_OUTPUT_GENBADao>();
            this.daoShuppatsuGenbaTS = DaoInitUtility.GetComponent<IM_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBADao>();
            this.daoShashuTS = DaoInitUtility.GetComponent<IM_NAVI_OUTPUT_SHASHUDao>();
            this.naviConnectDao = DaoInitUtility.GetComponent<IS_NAVI_CONNECTDao>();

            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 初期化

        #region ボタンの初期化
        /// <summary>
        /// ボタン設定の読み込み
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonsetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonsetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();

            // 親フォームのボタン表示
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }
        #endregion

        #region イベント初期化
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            // 除外登録(F1)イベント作成
            parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);
            // 除外解除登録(F2)イベント作成
            parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);
            // 削除連携(F4)イベント作成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);
            // 検索(F8)イベント作成
            this.form.C_Regist(parentForm.bt_func8);
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            parentForm.bt_func8.ProcessKbn = PROCESS_KBN.NEW;
            // 登録連携(F9)イベント作成
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            // 閉じる(F12)イベント作成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // 連携マスタイベント作成
            this.form.txtNum_RenkeiMaster.TextChanged += new EventHandler(this.txtNum_RenkeiMaster_TextChanged);
            // 連携候補イベント作成
            this.form.txtNum_RenkeiKouho.TextChanged += new EventHandler(this.txtNum_RenkeiKouho_TextChanged);
        }
        #endregion

        #region 抽出条件初期化
        /// <summary>
        /// 抽出条件の初期化
        /// </summary>
        private void SetInitialRenkeiCondition()
        {
            // 初期値は社員
            this.form.txtNum_RenkeiMaster.Text = ConstCls.RENKEI_MASTER_SHAIN.ToString();

            // 初期値は未連携
            this.form.txtNum_RenkeiKouho.Text = ConstCls.RENKEI_KOUHO_MIRENKEI.ToString();

            // 初期値は連携対象
            this.form.txtNum_KensakuJoken.Text = ConstCls.SEARCH_RENKEI_TAISHO.ToString();

            this.renkeiMaster = int.Parse(this.form.txtNum_RenkeiMaster.Text);
            this.renkeiKouho = int.Parse(this.form.txtNum_RenkeiKouho.Text);
            this.kensakuJoken = int.Parse(this.form.txtNum_KensakuJoken.Text);

            // FunctionのEnabled変更
            this.FunctionControl();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // 一覧内のチェックボックスの設定
                this.HeaderCheckBoxSupport();

                // 検索条件の初期化処理
                this.SetInitialRenkeiCondition();

                // イベントの初期化処理
                this.EventInit();

                // 表示明細の初期化
                this.DetailChange();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret;
        }

        #endregion

        #endregion

        #region 連携マスタ変更時の処理
        /// <summary>
        /// 連携マスタが変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txtNum_RenkeiMaster_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.txtNum_RenkeiMaster.Text))
            {
                this.renkeiMaster = int.Parse(this.form.txtNum_RenkeiMaster.Text);

                // 明細の表示切替
                this.DetailChange();
            }
        }
        #endregion

        #region 連携候補変更時の処理
        /// <summary>
        /// 連携候補が変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txtNum_RenkeiKouho_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.txtNum_RenkeiKouho.Text))
            {
                this.renkeiKouho = int.Parse(this.form.txtNum_RenkeiKouho.Text);

                // 明細の表示切替
                this.DetailChange();
            }
        }
        #endregion

        #region ファンクションEnabled切替
        /// <summary>
        /// ファンクションボタンのEnabledを切り替える
        /// </summary>
        internal void FunctionControl()
        {
            parentForm.bt_func1.Enabled = true;
            parentForm.bt_func2.Enabled = true;
            parentForm.bt_func4.Enabled = true;
            parentForm.bt_func8.Enabled = true;
            parentForm.bt_func9.Enabled = true;
            parentForm.bt_func12.Enabled = true;

            if (this.kensakuJoken == ConstCls.SEARCH_RENKEI_TAISHO)
            {
                // 連携対象
                parentForm.bt_func2.Enabled = false;
                if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_MIRENKEI)
                {
                    // 未連携は削除連携不可に
                    parentForm.bt_func4.Enabled = false;
                }
                else
                {
                    // 連携済は除外登録不可に
                    parentForm.bt_func1.Enabled = false;

                    if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_RENKEIZUMI
                    && this.renkeiMaster != ConstCls.RENKEI_MASTER_SHAIN)
                    {
                        parentForm.bt_func9.Enabled = false;
                    }
                }
            }
            else
            {
                // 連携除外
                parentForm.bt_func1.Enabled = false;
                parentForm.bt_func4.Enabled = false;
                parentForm.bt_func9.Enabled = false;
            }
        }

        /// <summary>
        /// 全ファンクションを入力不可にする
        /// </summary>
        internal void ButtonEnabledFalse()
        {
            parentForm.bt_func1.Enabled = false;
            parentForm.bt_func2.Enabled = false;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func8.Enabled = false;
            parentForm.bt_func9.Enabled = false;
            parentForm.bt_func12.Enabled = false;
        }
        #endregion

        #region 明細切替処理
        /// <summary>
        /// 明細切替処理
        /// 必須項目チェックに使用する変数もここで一緒にセットしておく
        /// </summary>
        private void DetailChange()
        {
            try
            {
                // 明細をクリアする
                this.form.Ichiran1.Rows.Clear();
                this.form.Ichiran2.Rows.Clear();
                this.form.Ichiran3.Rows.Clear();
                this.form.Ichiran4.Rows.Clear();

                // 全明細非表示
                this.form.Ichiran1.Visible = false;
                this.form.Ichiran2.Visible = false;
                this.form.Ichiran3.Visible = false;
                this.form.Ichiran4.Visible = false;

                this.form.STATUS_CD.Enabled = false;

                // 対象の確定
                if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                {
                    // 社員
                    this.DispGridView = this.form.Ichiran1;
                    this.chkColumn1 = "SHAIN_NAVI_SHAIN_CD";
                    this.chkColumn2 = "SHAIN_NAVI_ROLE";
                    this.chkColumn3 = "SHAIN_NAVI_PASSWORD";
                    this.chkColumnName1 = "作業者CD";
                    this.chkColumnName2 = "業務";
                    this.chkColmnPK1 = "SHAIN_CD";
                    this.chkColmnPK2 = string.Empty;
                }
                else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
                {
                    // 現場
                    this.DispGridView = this.form.Ichiran2;
                    this.chkColumn1 = string.Empty;
                    this.chkColumn2 = string.Empty;
                    this.chkColumn3 = string.Empty;
                    this.chkColumnName1 = string.Empty;
                    this.chkColumnName2 = string.Empty;
                    this.chkColmnPK1 = string.Empty;
                    this.chkColmnPK2 = string.Empty;
                    // 再連携はステータスが1つしかなく絞り込む意味がないためFalseのまま
                    if (this.renkeiKouho != ConstCls.RENKEI_KOUHO_SAIRENKEI)
                    {
                        this.form.STATUS_CD.Enabled = true;
                    }
                    if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_MIRENKEI)
                    {
                        this.DispGridView.Columns["GENBA_ERROR_INFO"].Visible = true;
                    }
                    else
                    {
                        this.DispGridView.Columns["GENBA_ERROR_INFO"].Visible = false;
                    }
                }
                else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHUPPATSU_GENBA)
                {
                    // 出発現場、荷卸現場
                    this.DispGridView = this.form.Ichiran3;
                    this.chkColumn1 = "EIGYOUSHO_NAVI_EIGYOUSHO_CD";
                    this.chkColumn2 = string.Empty;
                    this.chkColumn3 = string.Empty;
                    this.chkColumnName1 = "Navi営業所CD";
                    this.chkColumnName2 = string.Empty;
                    this.chkColmnPK1 = "EIGYOUSHO_GYOUSHA_CD";
                    this.chkColmnPK2 = "EIGYOUSHO_GENBA_CD";
                }
                else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHASHU)
                {
                    // 車種
                    this.DispGridView = this.form.Ichiran4;
                    this.chkColumn1 = "SHASHU_NAVI_SHASHU_CD";
                    this.chkColumn2 = string.Empty;
                    this.chkColumn3 = string.Empty;
                    this.chkColumnName1 = "Navi車輌タイプCD";
                    this.chkColumnName2 = string.Empty;
                    this.chkColmnPK1 = string.Empty;
                    this.chkColmnPK2 = string.Empty;
                }

                // DGVの位置を設定
                this.DispGridView.Location = new Point(ConstCls.ICHIRAN_LOCATION_X, ConstCls.ICHIRAN_LOCATION_Y);

                // 選択したマスタに対応するDGVのみ表示
                this.DispGridView.Visible = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailChange", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
        }
        #endregion

        #region 検索
        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.renkeiMaster = int.Parse(this.form.txtNum_RenkeiMaster.Text);
                this.renkeiKouho = int.Parse(this.form.txtNum_RenkeiKouho.Text);
                this.kensakuJoken = int.Parse(this.form.txtNum_KensakuJoken.Text);

                var sb = new StringBuilder();

                //SQL生成
                //SELECT
                this.CreateSQLSelect(sb);
                //JOIN
                this.CreateSQLJoin(sb);
                //WHERE
                this.CreateSQLWhere(sb);
                //ORDER BY
                this.CreateSQLOrderBy(sb);

                var sql = sb.ToString();

                //検索実行
                this.SearchResult = new DataTable();
                if (!string.IsNullOrEmpty(sql))
                {
                    this.SearchResult = this.daoIchiran.getdateforstringsql(sql);
                }

                ret_cnt = SearchResult.Rows.Count;

                //検索結果表示
                this.setIchiran();

                //FunctionのEnabled変更
                this.FunctionControl();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093");
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245");
                ret_cnt = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret_cnt;
        }
        #endregion

        #region SQL生成

        #region SELECT
        /// <summary>
        /// SELECT句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLSelect(StringBuilder sql)
        {
            if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
            {
                // 社員
                TblName = "M_SHAIN";
                sql.AppendFormat(" SELECT DISTINCT {0}.SHAIN_CD, {0}.SHAIN_NAME_RYAKU, {0}.SHAIN_FURIGANA, {0}.MAIL_ADDRESS AS MAIL_ADDRESS, ", TblName);
                sql.Append(" ISNULL(DTO.NAVI_PASSWORD, M_SHAIN.PASSWORD) AS NAVI_PASSWORD, ");
                sql.Append(" ISNULL(DTO.NAVI_SHAIN_CD, M_SHAIN.SHAIN_CD) AS NAVI_SHAIN_CD, ISNULL(DTO.NAVI_ROLE, 'master') AS NAVI_ROLE, ");
                sql.Append(" CASE ISNULL(DTO.NAVI_ROLE, 'master') WHEN 'mastersp'  THEN 'システム管理者＋スマホ操作'" +
                                                                " WHEN 'master'    THEN 'システム管理者'" +
                                                                " WHEN 'managersp' THEN '受付・配車業務＋スマホ操作'" +
                                                                " WHEN 'manager'   THEN '受付・配車業務'" +
                                                                " WHEN 'workerpc'  THEN '作業者情報＋スマホ操作'" +
                                                                " WHEN 'worker'    THEN 'スマホ操作'" +
                                                                " ELSE '' END AS BUSINESS_CONTENT ");
            }
            else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
            {
                // 現場
                TblName = "M_GENBA";
                sql.AppendFormat(" SELECT DISTINCT {0}.GYOUSHA_CD + {0}.GENBA_CD AS HOUMONSAKI_CD, {0}.GENBA_NAME_RYAKU, {0}.GENBA_FURIGANA, {0}.GENBA_ADDRESS1 + {0}.GENBA_ADDRESS2 AS GENBA_ADDRESS, {0}.GENBA_TEL, {0}.GENBA_POST, {0}.GYOUSHA_CD, {0}.GENBA_CD, {0}.UNTENSHA_SHIJI_JIKOU1 + {0}.UNTENSHA_SHIJI_JIKOU2 + {0}.UNTENSHA_SHIJI_JIKOU3 AS UNTENSHA_SHIJI_JIKOU, M_GYOUSHA.GYOUSHA_NAME_RYAKU, M_GYOUSHA.GYOUSHA_FURIGANA, M_TODOUFUKEN.TODOUFUKEN_NAME, DTO.PROCESSING_ID, DTO.LINE_NO, ISNULL(DTO.ERROR_INFO, '') AS ERROR_INFO, ", TblName);
                // 定義はしてあるものの、データとしてSTATUS=3になることはあり得ないため、再連携候補抽出時の条件を含めている
                sql.AppendFormat(" CASE WHEN ISNULL(DTO.STATUS, 0) = 0 THEN '未連携'" +
                                      " WHEN DTO.STATUS = 3 OR (DTO.OUTPUT_DATE IS NOT NULL AND {0}.UPDATE_DATE > DTO.OUTPUT_DATE) THEN '再連携候補'" +
                                      " WHEN DTO.STATUS = 1            THEN '連携確認要'" +
                                      " WHEN DTO.STATUS = 2            THEN '連携完了'" +
                                      " WHEN DTO.STATUS = 9            THEN '差し戻し'" +
                                      " ELSE '' END AS STATUS ", TblName);
            }
            else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHUPPATSU_GENBA)
            {
                // 出発現場、荷降現場
                TblName = "M_GENBA";
                sql.Append(" SELECT DISTINCT M_GYOUSHA.GYOUSHA_CD, M_GYOUSHA.GYOUSHA_NAME_RYAKU, M_GENBA.GENBA_CD, M_GENBA.GENBA_NAME_RYAKU ");
                sql.Append(" , DTO.NAVI_EIGYOUSHO_CD ");
            }
            else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHASHU)
            {
                // 車種
                TblName = "M_SHASHU";
                sql.AppendFormat(" SELECT DISTINCT {0}.SHASHU_CD, {0}.SHASHU_NAME_RYAKU, DTO.NAVI_SHASHU_CD ", TblName);
            }

            sql.AppendFormat(" FROM {0} ", TblName);
        }
        #endregion
        #region JOIN
        /// <summary>
        /// JOIN句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLJoin(StringBuilder sql)
        {
            // JOIN句
            if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
            {
                sql.AppendFormat(" LEFT JOIN M_NAVI_OUTPUT_SHAIN AS DTO ON {0}.SHAIN_CD = DTO.SHAIN_CD ", TblName);
            }
            else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
            {
                sql.Append(" LEFT JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                sql.Append(" LEFT JOIN M_TODOUFUKEN ON M_GENBA.GENBA_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD");
                sql.AppendFormat(" LEFT JOIN M_NAVI_OUTPUT_GENBA AS DTO ON {0}.GYOUSHA_CD = DTO.GYOUSHA_CD AND {0}.GENBA_CD = DTO.GENBA_CD ", TblName);
            }
            else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHUPPATSU_GENBA)
            {
                sql.Append(" LEFT JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                sql.AppendFormat(" LEFT JOIN M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA AS DTO ON {0}.GYOUSHA_CD = DTO.GYOUSHA_CD AND {0}.GENBA_CD = DTO.GENBA_CD ", TblName);
            }
            else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHASHU)
            {
                sql.AppendFormat(" LEFT JOIN M_NAVI_OUTPUT_SHASHU AS DTO ON {0}.SHASHU_CD = DTO.SHASHU_CD ", TblName);
            }
        }
        #endregion
        #region WHERE
        /// <summary>
        /// WHERE句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLWhere(StringBuilder sql)
        {
            // WHERE句
            sql.Append(" WHERE ");

            // 削除区分加味
            sql.AppendFormat(" {0}.DELETE_FLG = 0 ", TblName);

            // 連携マスタ毎の条件設定
            if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
            {
                // 社員の場合は運転者のみ抽出
                sql.AppendFormat(" AND {0}.UNTEN_KBN = 1", TblName);
            }
            else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
            {
                // 現場
                sql.AppendFormat(" AND ISNULL({0}.GENBA_ADDRESS1, '') != '' ", TblName);
                sql.AppendFormat(" AND ISNULL({0}.GENBA_TODOUFUKEN_CD, '') != '' ", TblName);
                sql.AppendFormat(" AND ({0}.HAISHUTSU_NIZUMI_GENBA_KBN != 0 ", TblName);
                sql.AppendFormat(" OR {0}.TSUMIKAEHOKAN_KBN != 0 ", TblName);
                sql.AppendFormat(" OR {0}.SHOBUN_NIOROSHI_GENBA_KBN != 0 ", TblName);
                sql.AppendFormat(" OR {0}.SAISHUU_SHOBUNJOU_KBN != 0) ", TblName);
                if (!string.IsNullOrEmpty(this.form.STATUS_CD.Text))
                {
                    sql.Append(" AND DTO.STATUS = " + this.form.STATUS_CD.Text);
                }
            }
            else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHUPPATSU_GENBA)
            {
                // 出発現場、荷降現場
                sql.AppendFormat(" AND ({0}.HAISHUTSU_NIZUMI_GENBA_KBN != 0 ", TblName);
                sql.AppendFormat(" OR {0}.TSUMIKAEHOKAN_KBN != 0 ", TblName);
                sql.AppendFormat(" OR {0}.SHOBUN_NIOROSHI_GENBA_KBN != 0 ", TblName);
                sql.AppendFormat(" OR {0}.SAISHUU_SHOBUNJOU_KBN != 0) ", TblName);
            }

            // 連携候補・検索条件により分岐
            if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_MIRENKEI)
            {
                if (this.kensakuJoken == ConstCls.SEARCH_RENKEI_TAISHO)
                {
                    // 未連携・連携対象
                    // タイムスタンプテーブルが存在しないか、存在していても除外フラグが立っていないこと
                    sql.Append(" AND (DTO.JYOGAI_FLG IS NULL OR (DTO.OUTPUT_DATE IS NULL AND DTO.JYOGAI_FLG = 0)) ");
                }
                else
                {
                    // 未連携・連携除外
                    // 除外登録済みなのでタイムスタンプテーブルは存在する前提で
                    // 出力日がNull、除外フラグが1のもの
                    sql.Append(" AND DTO.OUTPUT_DATE IS NULL AND DTO.JYOGAI_FLG = 1 ");
                }
            }
            if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_RENKEIZUMI)
            {
                if (this.kensakuJoken == ConstCls.SEARCH_RENKEI_TAISHO)
                {
                    // 連携済み・連携対象
                    // 出力日有り、除外フラグが0
                    sql.AppendFormat(" AND DTO.OUTPUT_DATE IS NOT NULL AND {0}.UPDATE_DATE <= DTO.OUTPUT_DATE AND DTO.JYOGAI_FLG = 0 ", TblName);
                }
                else
                {
                    // 連携済み・連携除外
                    // 出力後に除外登録した、というデータ
                    // 基本的にはほぼないはず
                    sql.AppendFormat(" AND DTO.OUTPUT_DATE IS NOT NULL AND {0}.UPDATE_DATE <= DTO.OUTPUT_DATE AND DTO.JYOGAI_FLG = 1 ", TblName);
                }
            }
            if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_SAIRENKEI)
            {
                // 検索条件
                if (this.kensakuJoken == ConstCls.SEARCH_RENKEI_TAISHO)
                {
                    // 再連携対象・連携対象
                    sql.AppendFormat(" AND DTO.OUTPUT_DATE IS NOT NULL AND {0}.UPDATE_DATE > DTO.OUTPUT_DATE AND DTO.JYOGAI_FLG = 0 ", TblName);
                }
                else
                {
                    // 再連携対象・連携除外
                    sql.AppendFormat(" AND DTO.OUTPUT_DATE IS NOT NULL AND {0}.UPDATE_DATE > DTO.OUTPUT_DATE AND DTO.JYOGAI_FLG = 1 ", TblName);
                }
            }
        }
        #endregion
        #region ORDER BY
        /// <summary>
        /// ORDERBY句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLOrderBy(StringBuilder sql)
        {
        }
        #endregion

        #endregion

        #region 検索結果表示
        /// <summary>
        /// 一覧にセット
        /// </summary>
        /// <returns></returns>
        private void setIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 全明細クリア
                this.form.Ichiran1.Rows.Clear();
                this.form.Ichiran2.Rows.Clear();
                this.form.Ichiran3.Rows.Clear();
                this.form.Ichiran4.Rows.Clear();

                // 抽出結果をDGVにセット
                if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
                {
                    this.DispGridView.Rows.Add(this.SearchResult.Rows.Count);
                    for (int i = 0; i < this.DispGridView.Rows.Count; i++)
                    {
                        DataGridViewRow row = this.DispGridView.Rows[i];
                        DataRow dr = this.SearchResult.Rows[i];

                        if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                        {
                            // 社員
                            row.Cells["DATA_RENKEI"].Value = false;
                            row.Cells["SHAIN_CD"].Value = dr["SHAIN_CD"];
                            row.Cells["SHAIN_NAVI_SHAIN_CD"].Value = dr["NAVI_SHAIN_CD"];
                            row.Cells["SHAIN_NAME_RYAKU"].Value = dr["SHAIN_NAME_RYAKU"];
                            row.Cells["SHAIN_FURIGANA"].Value = dr["SHAIN_FURIGANA"];
                            row.Cells["SHAIN_MAIL_ADDRESS"].Value = dr["MAIL_ADDRESS"];
                            row.Cells["SHAIN_NAVI_PASSWORD"].Value = dr["NAVI_PASSWORD"];
                            row.Cells["SHAIN_NAVI_ROLE"].Value = dr["NAVI_ROLE"];
                            row.Cells["SHAIN_BUSINESS_CONTENT"].Value = dr["BUSINESS_CONTENT"];
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
                        {
                            // 現場
                            row.Cells["DATA_RENKEI"].Value = false;
                            row.Cells["GENBA_HOUMONSAKI_CD"].Value = dr["HOUMONSAKI_CD"];
                            row.Cells["GENBA_NAME_RYAKU"].Value = dr["GENBA_NAME_RYAKU"];
                            row.Cells["GENBA_FURIGANA"].Value = dr["GENBA_FURIGANA"];
                            row.Cells["GENBA_ADDRESS"].Value = dr["GENBA_ADDRESS"];
                            row.Cells["GENBA_TEL"].Value = dr["GENBA_TEL"];
                            row.Cells["GENBA_POST"].Value = dr["GENBA_POST"];
                            row.Cells["GENBA_GYOUSHA_CD"].Value = dr["GYOUSHA_CD"];
                            row.Cells["GENBA_CD"].Value = dr["GENBA_CD"];
                            row.Cells["GENBA_GYOUSHA_NAME_RYAKU"].Value = dr["GYOUSHA_NAME_RYAKU"];
                            row.Cells["GENBA_GYOUSHA_NAME_FURIGANA"].Value = dr["GYOUSHA_FURIGANA"];
                            row.Cells["GENBA_UNTENSHA_SHIJI_JIKOU"].Value = dr["UNTENSHA_SHIJI_JIKOU"];
                            row.Cells["GENBA_TODOUFUKEN_NAME"].Value = dr["TODOUFUKEN_NAME"];
                            row.Cells["GENBA_PROCESSING_ID"].Value = dr["PROCESSING_ID"];
                            if (dr["LINE_NO"].Equals(DBNull.Value))
                            {
                                row.Cells["GENBA_LINE_NO"].Value = 0;
                            }
                            else
                            {
                                row.Cells["GENBA_LINE_NO"].Value = dr["LINE_NO"];
                            }
                            row.Cells["GENBA_STATUS"].Value = dr["STATUS"];
                            row.Cells["GENBA_ERROR_INFO"].Value = dr["ERROR_INFO"];
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHUPPATSU_GENBA)
                        {
                            // 出発現場、荷降現場
                            row.Cells["DATA_RENKEI"].Value = false;
                            row.Cells["EIGYOUSHO_GYOUSHA_CD"].Value = dr["GYOUSHA_CD"];
                            row.Cells["EIGYOUSHO_GYOUSHA_NAME_RYAKU"].Value = dr["GYOUSHA_NAME_RYAKU"];
                            row.Cells["EIGYOUSHO_GENBA_CD"].Value = dr["GENBA_CD"];
                            row.Cells["EIGYOUSHO_GENBA_NAME_RYAKU"].Value = dr["GENBA_NAME_RYAKU"];
                            row.Cells["EIGYOUSHO_NAVI_EIGYOUSHO_CD"].Value = dr["NAVI_EIGYOUSHO_CD"];
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHASHU)
                        {
                            // 車種
                            row.Cells["DATA_RENKEI"].Value = false;
                            row.Cells["SHASHU_CD"].Value = dr["SHASHU_CD"];
                            row.Cells["SHASHU_NAME_RYAKU"].Value = dr["SHASHU_NAME_RYAKU"];
                            row.Cells["SHASHU_NAVI_SHASHU_CD"].Value = dr["NAVI_SHASHU_CD"];
                        }
                    }
                }

                // 入力制限をかける
                this.LimitationsColumn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region API絡み

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        internal string OutputCSV(string Mode)
        {
            string filePath = string.Empty;

            try
            {
                // 出力データを整形する。
                var dataList = new List<string>();

                // LINE_NO(現場用)
                var line_no = 0;

                StringBuilder sb = new StringBuilder();
                int updMode = 0;
                if (this.renkeiKouho != ConstCls.RENKEI_KOUHO_MIRENKEI)
                {
                    updMode = 2;
                }

                // ヘッダ情報(空行で構わない)
                sb.Append("");
                dataList.Add(sb.ToString());

                // チェックONのデータ読み込み
                var dtos = this.DispGridView.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_RENKEI"].Value.Equals(true));

                foreach (var dto in dtos)
                {
                    if (Mode == "INS")
                    {
                        #region 追加用

                        if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                        {
                            // 名称⇒姓・名分割
                            string surname; // 姓
                            string name;    // 名
                            SplitShainName(dto.Cells["SHAIN_NAME_RYAKU"].Value.ToString(), out surname, out name);

                            string surnamef;
                            string namef;
                            SplitShainName(dto.Cells["SHAIN_FURIGANA"].Value.ToString(), out surnamef, out namef);

                            string adr1;
                            string adr2;
                            this.SplitMailAddress(dto.Cells["SHAIN_MAIL_ADDRESS"].Value.ToString(), out adr1, out adr2);

                            sb.Clear();
                            sb.Append(dto.Cells["SHAIN_NAVI_SHAIN_CD"].Value)                   // ユーザーコード
                                .AppendFormat(",{0}", surname)                                  // 姓
                                .AppendFormat(",{0}", name)                                     // 名
                                .AppendFormat(",{0}", surnamef)                                 // 姓(かな)
                                .AppendFormat(",{0}", namef)                                    // 名(かな)
                                .AppendFormat(",{0}", adr1)                                     // PCメールアドレス
                                .AppendFormat(",{0}", adr2)                                     // SPメールアドレス
                                .Append(",")                                                    // 電話番号
                                .Append(",")                                                    // 画像URL
                                .AppendFormat(",{0}", dto.Cells["SHAIN_NAVI_PASSWORD"].Value)   // パスワード
                                .AppendFormat(",{0}", "ROOT")                                   // グループ
                                .AppendFormat(",{0}", dto.Cells["SHAIN_NAVI_ROLE"].Value)       // ロール種別コード
                                .Append(",")                                                    // 顧客コード
                                .Append(",0")                                                   // 更新モード
                                .Append(",");                                                   // エラー事由
                            dataList.Add(sb.ToString());
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
                        {
                            line_no++;
                            // DGVにLINE_NOをセットする
                            dto.Cells["GENBA_LINE_NO"].Value = line_no;

                            string address = Convert.ToString(dto.Cells["GENBA_TODOUFUKEN_NAME"].Value) + Convert.ToString(dto.Cells["GENBA_ADDRESS"].Value);
                            //string address = Convert.ToString(dto.Cells["GENBA_ADDRESS"].Value);
                            sb.Clear();
                            sb.Append(dto.Cells["GENBA_HOUMONSAKI_CD"].Value)                           // 訪問先コード
                                .AppendFormat(",{0}", dto.Cells["GENBA_NAME_RYAKU"].Value)              // 訪問先名称
                                .AppendFormat(",{0}", dto.Cells["GENBA_FURIGANA"].Value)                // 訪問先名称(かな)
                                .Append(",")                                                            // 訪問先緯度
                                .Append(",")                                                            // 訪問先経度
                                .AppendFormat(",{0}", dto.Cells["GENBA_TEL"].Value)                     // 電話番号
                                .AppendFormat(",{0}", dto.Cells["GENBA_POST"].Value)                    // 郵便番号
                                .AppendFormat(",{0}", address)                                          // 住所
                                .AppendFormat(",{0}", dto.Cells["GENBA_GYOUSHA_CD"].Value)              // 顧客コード
                                .AppendFormat(",{0}", dto.Cells["GENBA_GYOUSHA_NAME_RYAKU"].Value)      // 顧客名称
                                .AppendFormat(",{0}", dto.Cells["GENBA_GYOUSHA_NAME_FURIGANA"].Value)   // 顧客名称かな
                                .Append(",")                                                            // 駐車場登録フラグ
                                .Append(",")                                                            // 駐車場名称
                                .Append(",")                                                            // 駐車場名称(かな)
                                .Append(",")                                                            // 駐車場住所
                                .Append(",")                                                            // 駐車場緯度
                                .Append(",")                                                            // 駐車場経度
                                .AppendFormat(",{0}", dto.Cells["GENBA_UNTENSHA_SHIJI_JIKOU"].Value)    // 特記事項
                                .AppendFormat(",{0}", updMode)                                          // 更新モード
                                .Append(",")                                                            // メールアドレス
                                .Append(",0")                                                           // 横付けフラグ
                                .Append(",")                                                            // 到着希望日
                                .Append(",")                                                            // 到着希望時刻
                                .Append(",")                                                            // 作業時間(分)
                                .Append(",")                                                            // 案件コード
                                .Append(",")                                                            // 案件名称
                                .Append(",")                                                            // 案件詳細
                                .Append(",")                                                            // 関連情報
                                .Append(",")                                                            // グループCD
                                .Append(",")                                                            // 距離
                                .Append(",")                                                            // 時間
                                .Append(",")                                                            // 種別
                                .Append(",")                                                            // 遅延通知メール制限
                                .Append(",");                                                           // 荷物量
                            dataList.Add(sb.ToString());
                        }
                        #endregion
                    }
                    else
                    {
                        #region 削除用
                        if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                        {
                            sb.Clear();
                            sb.Append(dto.Cells["SHAIN_NAVI_SHAIN_CD"].Value)   // ユーザーコード
                                .Append(",")                                    // 姓
                                .Append(",")                                    // 名
                                .Append(",")                                    // 姓(かな)
                                .Append(",")                                    // 名(かな)
                                .Append(",")                                    // PCメールアドレス
                                .Append(",")                                    // SPメールアドレス
                                .Append(",")                                    // 電話番号
                                .Append(",")                                    // 画像URL
                                .Append(",")                                    // パスワード
                                .Append(",")                                    // グループ
                                .Append(",")                                    // ロール種別コード
                                .Append(",")                                    // 顧客コード
                                .Append(",1")                                   // 更新モード
                                .Append(",");                                   // エラー事由
                            dataList.Add(sb.ToString());
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
                        {
                            sb.Clear();
                            sb.Append(dto.Cells["GENBA_HOUMONSAKI_CD"].Value)   // 訪問先コード
                                .Append(",")                                    // 訪問先名称
                                .Append(",")                                    // 訪問先名称(かな)
                                .Append(",")                                    // 訪問先緯度
                                .Append(",")                                    // 訪問先経度
                                .Append(",")                                    // 電話番号
                                .Append(",")                                    // 郵便番号
                                .Append(",")                                    // 住所
                                .Append(",")                                    // 顧客コード
                                .Append(",")                                    // 顧客名称
                                .Append(",")                                    // 顧客名称かな
                                .Append(",")                                    // 駐車場登録フラグ
                                .Append(",")                                    // 駐車場名称
                                .Append(",")                                    // 駐車場名称(かな)
                                .Append(",")                                    // 駐車場住所
                                .Append(",")                                    // 駐車場緯度
                                .Append(",")                                    // 駐車場経度
                                .Append(",")                                    // 特記事項
                                .Append(",1")                                   // 更新モード
                                .Append(",")                                    // メールアドレス
                                .Append(",")                                    // 横付けフラグ
                                .Append(",")                                    // 到着希望日
                                .Append(",")                                    // 到着希望時刻
                                .Append(",")                                    // 作業時間(分)
                                .Append(",")                                    // 案件コード
                                .Append(",")                                    // 案件名称
                                .Append(",")                                    // 案件詳細
                                .Append(",")                                    // 関連情報
                                .Append(",")                                    // グループCD
                                .Append(",")                                    // 距離
                                .Append(",")                                    // 時間
                                .Append(",")                                    // 種別
                                .Append(",")                                    // 遅延通知メール制限
                                .Append(",");                                   // 荷物量
                            dataList.Add(sb.ToString());
                        }
                        #endregion
                    }
                }

                // 件数が1=ヘッダしかない=データなし
                if (!dataList.Count.Equals(1))
                {
                    string fileName = string.Empty;

                    if (Mode == "INS")
                    {
                        if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                        {
                            fileName = "ユーザー送信";
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
                        {
                            fileName = "訪問先送信";
                        }
                    }
                    else
                    {
                        if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                        {
                            fileName = "ユーザー削除";
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
                        {
                            fileName = "訪問先削除";
                        }
                    }

                    var navilogic = new NaviLogic();
                    filePath = navilogic.OutputCSV(fileName, dataList);

                    if (filePath == string.Empty)
                    {
                        return filePath;
                    }
                }
                return filePath;
            }
            catch (Exception ex)
            {
                LogUtility.Error("OutputCSV", ex);
                this.msgLogic.MessageBoxShow("E245");
                return filePath;
            }
        }
        #endregion

        #region データ送信
        /// <summary>
        /// データの送信
        /// JSON絡みの処理
        /// </summary>
        /// <returns></returns>
        internal int JsonConnection(string filePath)
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                var logic = new LogiLogic();

                //DELETEで使用するURL用変数初期化
                var apiURL = string.Empty;

                // テスト用ロジック
                var naviLogic = new NaviLogic();

                var postDto = new NaviRequestDto();
                postDto.filePath = filePath;


                if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                {
                    // 社員マスタ
                    var connectDto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_UPLOAD_USER);
                    if (connectDto == null)
                    {
                        return -1;
                    }

                    RES_UPLOAD_USER dto = new RES_UPLOAD_USER();
                    //var dto = naviLogic.HttpPOST<RES_UPLOAD_USER>(NaviConst.naviUPLOAD_USER, WebAPI_ContentType.MULTIPART_FORM_DATA, postDto);
                    var result = naviLogic.HttpPOST<RES_UPLOAD_USER>(connectDto.URL, connectDto.CONTENT_TYPE, postDto, out dto);
                    if (!result || dto == null)
                    {
                        // 例外が発生時はNULLなので処理終了
                        return -1;
                    }

                    string resultStatus = dto.Results.ResultStatus;
                    List<string> errList = new List<string>() { "1", "9" };
                    if (resultStatus.Equals("0"))
                    {
                        ret_cnt++;
                    }
                    else if (errList.Contains(resultStatus))
                    {
                        this.msgLogic.MessageBoxShowError("NAVITIMEマスタ連携で失敗しました。" + System.Environment.NewLine + "NAVITIMEの「ユーザの設定」画面からエラー内容を確認し連携情報を修正してください。");
                        ret_cnt = -1;
                    }
                }
                else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
                {
                    // 現場マスタ
                    var connectDto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_UPLOAD_VISIT);
                    if (connectDto == null)
                    {
                        return -1;
                    }

                    RES_UPLOAD_VISIT dto = new RES_UPLOAD_VISIT();
                    //var dto = naviLogic.HttpPOST<RES_UPLOAD_VISIT>(NaviConst.naviUPLOAD_VISIT, WebAPI_ContentType.MULTIPART_FORM_DATA, postDto);
                    var result = naviLogic.HttpPOST<RES_UPLOAD_VISIT>(connectDto.URL, connectDto.CONTENT_TYPE, postDto, out dto);
                    if (!result || dto == null)
                    {
                        // 例外が発生時はNULLなので処理終了
                        return -1;
                    }

                    if (dto.Success)
                    {
                        // 訪問先登録成功時のみPROCESSING_IDを保持する
                        this.processingId = int.Parse(dto.ProcessingId);
                        ret_cnt++;
                    }
                    else
                    {
                        this.msgLogic.MessageBoxShowError("NAVITIMEマスタ連携で失敗しました。" + System.Environment.NewLine + "NAVITIMEの「訪問先の設定」画面からエラー内容を確認し連携情報を修正してください。");
                        ret_cnt = -1;
                    }
                }
            }
            catch (WebException)
            {
                // LogiLogic.cs側でエラー表示しているので何もしない
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                // WebException以外はアラート表示
                LogUtility.Error("JsonConnection", ex);
                this.msgLogic.MessageBoxShow("E245");
                ret_cnt = -1;
            }

            LogUtility.DebugMethodEnd();

            return ret_cnt;

        }
        #endregion

        #region 訪問先一括登録の結果確認絡み

        #region API処理(現状未使用 先のバージョンで使用するため残す)
        /// <summary>
        /// 訪問先一括登録の結果確認のAPI連携
        /// </summary>
        /// <returns>true: false: </returns>
        internal bool CheckAPI()
        {
            bool err_flg = false;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var dtos = this.DispGridView.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_RENKEI"].Value.Equals(true));
                foreach (var result in dtos)
                {
                    // DtoにプロセスIDをセット
                    var postDto = new NaviRequestDto();
                    postDto.processingId = Convert.ToString(this.processingId);

                    // チェックがついてるデータがない場合抜ける
                    if (postDto.processingId == null)
                    {
                        return false;
                    }

                    var navilogic = new NaviLogic();

                    var connectDto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_CHECK_UPLOAD_VISIT);
                    if (connectDto == null)
                    {
                        return false;
                    }

                    // チェックのリクエストを送信
                    RES_CHECK_UPLOAD_VISIT dto = new RES_CHECK_UPLOAD_VISIT();
                    //var dto = navilogic.HttpPOST<RES_CHECK_UPLOAD_VISIT>(NaviConst.naviCHECK_UPLOAD_VISIT, WebAPI_ContentType.MULTIPART_FORM_DATA, postDto);
                    var results = navilogic.HttpPOST<RES_CHECK_UPLOAD_VISIT>(connectDto.URL, connectDto.CONTENT_TYPE, postDto, out dto);
                    if (!results || dto == null)
                    {
                        // 例外が発生時はNULLなので処理終了
                        return false;
                    }

                    // リクエストのエラー
                    if (dto.ErrorMessage != null)
                    {
                        string msg = string.Empty;
                        foreach (var err in dto.ErrorMessage)
                        {
                            msg += err.ToString() + System.Environment.NewLine;
                            this.msgLogic.MessageBoxShowError(msg);
                        }
                        return false;
                    }

                    if (dto.Results.UploadingStatus != "0")
                    {
                        // このアラートが極力出ないようにしたい
                        //this.msgLogic.MessageBoxShowInformation("まだナビタイムへ反映されておりません。" + Environment.NewLine + "もう暫しお待ちください。");
                        return false;
                    }

                    // 結果
                    if (dto.Results.ResultStatus == "0")
                    {
                        this.msgLogic.MessageBoxShow("I001", "連携");
                        //this.msgLogic.MessageBoxShowInformation("ナビタイムへ反映されました。" + Environment.NewLine + "ステータスを更新します。");
                        this.form.status = ConstCls.STATUS_RENKEIKANRYOU;
                        ret = this.GenbaStatusUpdate(this.processingId, 0, string.Empty);
                        if (!ret)
                        {
                            return false;
                        }

                        return true;
                    }
                    else
                    {
                        if (err_flg == false)
                        {
                            //this.msgLogic.MessageBoxShowWarn("エラー情報が存在します。" + Environment.NewLine + "関連データを未連携状態に差し戻します。");
                            this.msgLogic.MessageBoxShowError("NAVITIMEマスタ連携で失敗しました。" + System.Environment.NewLine + "NAVITIMEの「ユーザの設定」画面からエラー内容を確認し連携情報を修正してください。");
                            err_flg = true;
                        }
                    }

                    // エラー理由の抽出
                    if (dto.Results.ContentError == null)
                    {
                        // 送信API直後に確認APIが流れるのでここに入ることはないはず
                        //this.msgLogic.MessageBoxShowError("送信から24時間以上経過したため、エラー内容の取得ができません。");
                        return false;
                    }

                    // ポ詳細エラー内容を文字列に格納
                    if (dto.Results.ContentError.Count != 0)
                    {
                        this.form.status = ConstCls.STATUS_SASHIMODOSHI;
                        // 配送計画
                        foreach (RES_CHECK_UPLOAD_VISIT_ERROR_CONTENT errDto in dto.Results.ContentError)
                        {
                            //var dtos2 = this.DispGridView.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["GENBA_PROCESSING_ID"].Value.Equals(result.Cells["GENBA_PROCESSING_ID"].Value));
                            var dtos2 = this.DispGridView.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_RENKEI"].Value.Equals(true));
                            foreach (var result2 in dtos2)
                            {
                                if (Convert.ToInt32(result2.Cells["GENBA_LINE_NO"].Value) == errDto.LineNo)
                                {
                                    ret = this.GenbaStatusUpdate(this.processingId, errDto.LineNo, errDto.ErrorReason);
                                    if (!ret)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        ret = this.GenbaStatusUpdate(this.processingId, 0, string.Empty);
                        if (!ret)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (WebException)
            {
                // NaviLogic.cs側でエラー表示しているのでここでは表示しない
                return false;
            }
            catch (Exception ex)
            {
                // WebException以外はアラート表示
                LogUtility.Error("CheckAPI", ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }
        }
        #endregion

        #region 訪問先のステータス更新
        /// <summary>
        /// M_NAVI_OUTPUT_GENBAのステータス更新
        /// </summary>
        /// <param name="dgvRow"></param>
        /// <returns></returns>
        [Transaction]
        internal bool GenbaStatusUpdate(int pid, int line_no, string ErrorReason)
        {
            try
            {
                using (var tran = new TransactionUtility())
                {
                    string sql = string.Empty;
                    // 現場
                    if (line_no != 0)
                    {
                        sql = string.Format("UPDATE M_NAVI_OUTPUT_GENBA SET OUTPUT_DATE = {2}, STATUS = {3}, ERROR_INFO = '{4}' WHERE PROCESSING_ID = {0} AND LINE_NO = {1} ", pid, line_no, this.form.output_date, this.form.status, ErrorReason);
                    }
                    else
                    {
                        // こちらはPROCESSING_IDのみで絞り、エラーがセットされていない行のステータスだけを変える
                        if (this.form.status == ConstCls.STATUS_SASHIMODOSHI)
                        {
                            sql = string.Format("UPDATE M_NAVI_OUTPUT_GENBA SET OUTPUT_DATE = {1}, STATUS = {2}, ERROR_INFO = '' WHERE PROCESSING_ID = {0} AND ISNULL(ERROR_INFO, '') = '' ", pid, this.form.output_date, this.form.status);
                        }
                        else if (this.form.status == ConstCls.STATUS_RENKEIKANRYOU)
                        {
                            sql = string.Format("UPDATE M_NAVI_OUTPUT_GENBA SET STATUS = {1}, ERROR_INFO = '' WHERE PROCESSING_ID = {0} ", pid, this.form.status);
                        }
                    }
                    this.daoGenbaTS.ExecuteForStringSql(sql);

                    tran.Commit();
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex1)
            {
                // 排他は警告
                LogUtility.Warn(ex1);
                this.msgLogic.MessageBoxShow("E080");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                //その他SQLエラー
                LogUtility.Error(ex2);
                this.msgLogic.MessageBoxShow("E093");
                return false;
            }
            catch (Exception ex)
            {
                //その他エラー
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }

            return true;
        }
        #endregion

        #endregion

        #endregion

        #region テーブルにデータを保存
        /// <summary>
        /// データ登録
        /// </summary>
        /// <param name="dgvRow"></param>
        /// <returns></returns>
        [Transaction]
        internal int TimeStampTableRegist()
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                using (var tran = new TransactionUtility())
                {

                    var dtos = this.DispGridView.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_RENKEI"].Value.Equals(true));
                    foreach (var dto in dtos)
                    {

                        string sql = "";
                        int dbCount = 0;
                        if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                        {
                            // 社員
                            dbCount = this.daoShainTS.GetDataByCd(dto.Cells["SHAIN_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_NAVI_OUTPUT_SHAIN VALUES ('{0}', {1}, {2}, '{3}', '{4}', '{5}')", dto.Cells["SHAIN_CD"].Value, this.form.output_date, this.form.exclusionFlg, dto.Cells["SHAIN_NAVI_SHAIN_CD"].Value, dto.Cells["SHAIN_NAVI_ROLE"].Value, dto.Cells["SHAIN_NAVI_PASSWORD"].Value);
                                this.daoShainTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_NAVI_OUTPUT_SHAIN SET OUTPUT_DATE = {1}, JYOGAI_FLG = {2}, NAVI_SHAIN_CD = '{3}', NAVI_ROLE = '{4}', NAVI_PASSWORD = '{5}' WHERE SHAIN_CD = '{0}'", dto.Cells["SHAIN_CD"].Value, this.form.output_date, this.form.exclusionFlg, dto.Cells["SHAIN_NAVI_SHAIN_CD"].Value, dto.Cells["SHAIN_NAVI_ROLE"].Value, dto.Cells["SHAIN_NAVI_PASSWORD"].Value);
                                this.daoShainTS.ExecuteForStringSql(sql);
                            }
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
                        {
                            // 現場
                            dbCount = this.daoGenbaTS.GetDataByCd(dto.Cells["GENBA_GYOUSHA_CD"].Value.ToString(), dto.Cells["GENBA_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_NAVI_OUTPUT_GENBA VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, '{7}')", dto.Cells["GENBA_GYOUSHA_CD"].Value, dto.Cells["GENBA_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.processingId, Convert.ToInt32(dto.Cells["GENBA_LINE_NO"].Value), this.form.status, string.Empty);
                                this.daoGenbaTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_NAVI_OUTPUT_GENBA SET OUTPUT_DATE = {2}, JYOGAI_FLG = {3}, PROCESSING_ID = {4}, LINE_NO = {5}, STATUS = {6}, ERROR_INFO = '{7}' WHERE  GYOUSHA_CD = '{0}' AND GENBA_CD = '{1}'", dto.Cells["GENBA_GYOUSHA_CD"].Value, dto.Cells["GENBA_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.processingId, Convert.ToInt32(dto.Cells["GENBA_LINE_NO"].Value), this.form.status, string.Empty);
                                this.daoGenbaTS.ExecuteForStringSql(sql);
                            }
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHUPPATSU_GENBA)
                        {
                            // 出発現場、荷降現場
                            dbCount = this.daoShuppatsuGenbaTS.GetDataByCd(dto.Cells["EIGYOUSHO_GYOUSHA_CD"].Value.ToString(), dto.Cells["EIGYOUSHO_GENBA_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA VALUES ('{0}', '{1}', {2}, {3}, '{4}')", dto.Cells["EIGYOUSHO_GYOUSHA_CD"].Value, dto.Cells["EIGYOUSHO_GENBA_CD"].Value, this.form.output_date, this.form.exclusionFlg, dto.Cells["EIGYOUSHO_NAVI_EIGYOUSHO_CD"].Value);
                                this.daoShuppatsuGenbaTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA SET OUTPUT_DATE = {2}, JYOGAI_FLG = {3}, NAVI_EIGYOUSHO_CD = '{4}' WHERE GYOUSHA_CD = '{0}' AND GENBA_CD = '{1}'", dto.Cells["EIGYOUSHO_GYOUSHA_CD"].Value, dto.Cells["EIGYOUSHO_GENBA_CD"].Value, this.form.output_date, this.form.exclusionFlg, dto.Cells["EIGYOUSHO_NAVI_EIGYOUSHO_CD"].Value);
                                this.daoShuppatsuGenbaTS.ExecuteForStringSql(sql);
                            }
                        }
                        else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHASHU)
                        {
                            // 車種
                            dbCount = this.daoShashuTS.GetDataByCd(dto.Cells["SHASHU_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_NAVI_OUTPUT_SHASHU VALUES ('{0}', {1}, {2}, '{3}')", dto.Cells["SHASHU_CD"].Value, this.form.output_date, this.form.exclusionFlg, dto.Cells["SHASHU_NAVI_SHASHU_CD"].Value);
                                this.daoShashuTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_NAVI_OUTPUT_SHASHU SET OUTPUT_DATE = {1}, JYOGAI_FLG = {2}, NAVI_SHASHU_CD = '{3}' WHERE SHASHU_CD = '{0}'", dto.Cells["SHASHU_CD"].Value, this.form.output_date, this.form.exclusionFlg, dto.Cells["SHASHU_NAVI_SHASHU_CD"].Value);
                                this.daoShashuTS.ExecuteForStringSql(sql);
                            }
                        }
                        ret_cnt++;
                    }
                    tran.Commit();
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex1)
            {
                // 排他は警告
                LogUtility.Warn(ex1);
                this.msgLogic.MessageBoxShow("E080");
                ret_cnt = -1;
            }
            catch (SQLRuntimeException ex2)
            {
                //その他SQLエラー
                LogUtility.Error(ex2);
                this.msgLogic.MessageBoxShow("E093");
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                //その他エラー
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245");
                ret_cnt = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret_cnt;
        }
        #endregion

        #region 各種チェック系

        #region 登録チェック
        internal bool CheckCount()
        {
            // チェック0件は弾く
            var count = this.DispGridView.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_RENKEI"].Value.Equals(true)).Count();
            if (count == 0)
            {
                this.msgLogic.MessageBoxShowError("処理対象となる明細行を選択してください。");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 登録チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean CheckBeforeUpdate()
        {
            LogUtility.DebugMethodStart();

            try
            {
                ArrayList errColName = new ArrayList();

                Boolean rtn = true;

                Boolean isErr;

                // 必須入力チェック
                isErr = false;

                if (this.chkColumn1 != string.Empty)
                {
                    for (int i = 0; i < this.DispGridView.RowCount; i++)
                    {
                        if (this.DispGridView.Rows[i].IsNewRow)
                        {
                            continue;
                        }
                        if ((null == this.DispGridView.Rows[i].Cells[this.chkColumn1].Value ||
                            "".Equals(this.DispGridView.Rows[i].Cells[this.chkColumn1].Value.ToString().Trim())) &&
                            true.Equals(this.DispGridView.Rows[i].Cells[0].Value))
                        {
                            if (false == isErr)
                            {
                                errColName.Add(this.chkColumnName1);
                                isErr = true;
                            }
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.DispGridView.Rows[i].Cells[this.chkColumn1], true);
                        }
                    }
                }
                isErr = false;
                if (this.chkColumn2 != string.Empty)
                {
                    for (int i = 0; i < this.DispGridView.RowCount; i++)
                    {
                        if (this.DispGridView.Rows[i].IsNewRow)
                        {
                            continue;
                        }
                        if ((null == this.DispGridView.Rows[i].Cells[this.chkColumn2].Value ||
                            "".Equals(this.DispGridView.Rows[i].Cells[this.chkColumn2].Value.ToString().Trim())) &&
                            true.Equals(this.DispGridView.Rows[i].Cells[0].Value))
                        {
                            if (false == isErr)
                            {
                                errColName.Add(this.chkColumnName2);
                                isErr = true;
                            }
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.DispGridView.Rows[i].Cells[this.chkColumn2], true);
                        }
                    }
                }

                this.DispGridView.Refresh();
                rtn = (errColName.Count == 0);

                string errMsg = "";
                if (!rtn)
                {
                    foreach (string colName in errColName)
                    {
                        if (errMsg.Length > 0)
                        {
                            errMsg += "\n";
                        }
                        errMsg += colName;
                    }
                    this.msgLogic.MessageBoxShow("E001", errMsg);
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // パスワードのセキュリティレベルチェックは社員マスタのみ
                if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                {
                    // パスワードチェック
                    List<DataGridViewRow> selectedRows = this.DispGridView.Rows.Cast<DataGridViewRow>().Where(row => (bool)row.Cells["DATA_RENKEI"].Value == true).ToList();
                    foreach (var selectedRow in selectedRows)
                    {
                        // ロールが企業管理(システム管理者)の場合はメールアドレスを必須
                        if (selectedRow.Cells["SHAIN_NAVI_ROLE"].Value.ToString() == "mastersp" || selectedRow.Cells["SHAIN_NAVI_ROLE"].Value.ToString() == "master")
                        {
                            if (selectedRow.Cells["SHAIN_MAIL_ADDRESS"].Value.ToString() == string.Empty)
                            {
                                var str = "ロールが\"システム管理者\"　または　\"システム管理者＋スマホ操作\"の場合、メールアドレスを社員入力で登録してください。";
                                this.msgLogic.MessageBoxShowInformation(str);
                                return false;
                            }
                        }

                        // パスワードのセキュリティレベルチェック
                        if (!CheckPassword(selectedRow.Cells["SHAIN_NAVI_PASSWORD"].Value.ToString(), true))
                        {
                            var str = "パスワードは、半角英数字(大文字/小文字を区別)の二つ以上の組み合わせで8～32文字で入力してください。";
                            this.msgLogic.MessageBoxShowInformation(str);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckBeforeUpdate", ex);
                this.msgLogic.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 重複チェック
        /// <summary>
        /// 重複チェックメインロジック
        /// </summary>
        /// <param name="ChkCD"></param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal bool DuplicationCheck(string ChkCDPK1, string ChkCDPK2, string ChkCD, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(ChkCDPK1, ChkCDPK2, ChkCD);

                // 画面で重複チェック
                int recCount = 0;
                for (int i = 0; i < this.DispGridView.Rows.Count; i++)
                {
                    string strCD = string.Empty;
                    if (this.DispGridView.Rows[i].Cells[this.chkColumn1].Value != null)
                    {
                        strCD = this.DispGridView.Rows[i].Cells[this.chkColumn1].Value.ToString();
                    }
                    if (strCD.Equals(Convert.ToString(ChkCD)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // DBで重複チェック
                int dbCount = 0;
                if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHAIN)
                {
                    // 社員重複チェック
                    dbCount = this.daoShainTS.GetDuplicationCountByCd(ChkCDPK1, ChkCD);
                }
                else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_GENBA)
                {
                    // 現場は重複チェックする項目なし
                    dbCount = 0;
                }
                else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHUPPATSU_GENBA)
                {
                    // 出発現場、荷降現場重複チェック
                    dbCount = this.daoShuppatsuGenbaTS.GetDuplicationCountByCd(ChkCDPK1, ChkCDPK2, ChkCD);
                }
                else if (this.renkeiMaster == ConstCls.RENKEI_MASTER_SHASHU)
                {
                    // 車種も重複チェックする項目なし
                    dbCount = 0;
                }

                if (dbCount > 0)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.msgLogic.MessageBoxShow("E093");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.msgLogic.MessageBoxShow("E245");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(false, catchErr);
            return false;
        }
        #endregion

        #region 削除チェック
        /// <summary>
        /// 社員削除時のチェック
        /// NAVITIMEのユーザー情報が全て消えてしまわないようにチェックする
        /// </summary>
        /// <returns></returns>
        internal bool DeleteCheck()
        {
            int dgvCount = this.DispGridView.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_RENKEI"].Value.Equals(true)).Count();
            int dbCount = this.daoShainTS.ExecuteForStringSql("SELECT COUNT(SHAIN_CD) CNT FROM M_NAVI_OUTPUT_SHAIN WHERE OUTPUT_DATE IS NOT NULL");
            if (dbCount - dgvCount < 1)
            {
                // DBに連携状態として登録されている件数-削除しようとしている件数が0件になってしまう場合、アラート
                this.msgLogic.MessageBoxShowWarn("社員の連携情報を全て削除することはできません。");
                return false;
            }
            return true;
        }
        #endregion

        #endregion

        #region NAVITIME連携項目入力制限
        /// <summary>
        /// 明細の項目制限
        /// </summary>
        private void LimitationsColumn()
        {
            try
            {
                // 未連携のみ入力可
                // 連携済み、社員の場合は入力可
                // 連携済み、再連携候補は入力不可
                if (this.renkeiKouho != ConstCls.RENKEI_KOUHO_MIRENKEI || this.kensakuJoken == ConstCls.SEARCH_RENKEI_JOGAI)
                {
                    for (int i = 0; i < this.DispGridView.Rows.Count; i++)
                    {
                        if (this.chkColumn1 != string.Empty)
                        {
                            this.DispGridView.Rows[i].Cells[this.chkColumn1].ReadOnly = true;
                        }
                        if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_SAIRENKEI)
                        {
                            if (this.chkColumn2 != string.Empty)
                            {
                                this.DispGridView.Rows[i].Cells[this.chkColumn2].ReadOnly = true;
                            }
                            if (this.chkColumn3 != string.Empty)
                            {
                                this.DispGridView.Rows[i].Cells[this.chkColumn3].ReadOnly = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitationsColumn", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
        }
        #endregion

        #region 明細ヘッダーにチェックボックスを追加
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        private void HeaderCheckBoxSupport()
        {

            LogUtility.DebugMethodStart();

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        this.DispGridView = this.form.Ichiran1;
                        break;
                    case 1:
                        this.DispGridView = this.form.Ichiran2;
                        break;
                    case 2:
                        this.DispGridView = this.form.Ichiran3;
                        break;
                    case 3:
                        this.DispGridView = this.form.Ichiran4;
                        break;
                }

                // 出発現場、荷降現場・車種も、連携は行わないがつけるらしい
                if (!this.DispGridView.Columns.Contains("DATA_RENKEI"))
                {
                    {
                        DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                        newColumn.Name = "DATA_RENKEI";
                        newColumn.HeaderText = "選択";
                        newColumn.Width = 70;
                        DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                        newheader.Value = "選択   ";
                        newColumn.HeaderCell = newheader;
                        newColumn.Resizable = DataGridViewTriState.False;

                        if (this.DispGridView.Columns.Count > 0)
                        {
                            this.DispGridView.Columns.Insert(0, newColumn);
                        }
                        else
                        {
                            this.DispGridView.Columns.Add(newColumn);
                        }
                        this.DispGridView.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                    }
                }
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 明細ヘッダーのチェックボックス解除
        /// <summary>
        /// 明細ヘッダーチェックボックスを解除する
        /// </summary>
        internal void HeaderCheckBoxFalse()
        {
            DataGridView dgv = null;

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        dgv = this.form.Ichiran1;
                        break;
                    case 1:
                        dgv = this.form.Ichiran2;
                        break;
                    case 2:
                        dgv = this.form.Ichiran3;
                        break;
                    case 3:
                        dgv = this.form.Ichiran4;
                        break;
                }
                if (dgv.Columns.Contains("DATA_RENKEI"))
                {
                    DataGridViewCheckBoxHeaderCell header = dgv.Columns["DATA_RENKEI"].HeaderCell as DataGridViewCheckBoxHeaderCell;
                    if (header != null)
                    {
                        header._checked = false;
                    }
                }
            }
        }
        #endregion

        #region 名称を姓名に分割
        /// <summary>
        /// 名称を姓・名に分割するメソッド
        /// </summary>
        /// <param name="shainName"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool SplitShainName(string shainName, out string surname, out string name)
        {
            surname = string.Empty;
            name = string.Empty;

            if (string.IsNullOrEmpty(shainName))
            {
                return false;
            }

            // スペースで分割させるが、分割不可能な場合に名には全角スペースを暫定で設定
            var splitNames = shainName.Split(new string[] { " ", "　" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var splitName in splitNames)
            {
                if (string.IsNullOrEmpty(surname))
                {
                    // 姓 に最初で分割された名前を設定
                    surname = splitName;
                }
                else
                {
                    // 名 は残り分割された名前を設定
                    name += splitName;
                }
            }

            if (string.IsNullOrEmpty(name))
            {
                // ブランクだとエラーとなるため、暫定で全角スペースで退避
                name = "　";
            }

            return true;
        }
        #endregion

        #region メールアドレスを分割
        /// <summary>
        /// メールアドレスををPCメールアドレス・SPメールアドレスに分割するメソッド
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <param name="mailAdress1"></param>
        /// <param name="mailAddress2"></param>
        /// <returns></returns>
        private bool SplitMailAddress(string mailAddress, out string mailAdress1, out string mailAddress2)
        {
            mailAdress1 = string.Empty;
            mailAddress2 = string.Empty;

            if (string.IsNullOrEmpty(mailAddress))
            {
                return false;
            }

            // カンマで分割させるが、分割不可能な場合に名には全角スペースを暫定で設定
            var splits = mailAddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var address in splits)
            {
                if (string.IsNullOrEmpty(mailAdress1))
                {
                    // PCメールアドレス に最初で分割された名前を設定
                    mailAdress1 = address;
                }
                else
                {
                    // SPメールアドレス は残り分割された名前を設定
                    mailAddress2 += address;
                }
            }
            return true;
        }
        #endregion

        #region パスワードのセキュリティレベル判定
        /// <summary>
        /// 社員マスタのパスワードがナビタイムのセキュリティレベルをクリアしたパスワードか判定
        /// </summary>
        /// <param name="shainPassword">パスワード</param>
        /// <param name="highSecurity">true:セキュリティレベル高, false:セキュリティレベル低</param>
        /// <returns>true:正常, false:エラー</returns>
        internal bool CheckPassword(string shainPassword, bool highSecurity)
        {
            if (string.IsNullOrEmpty(shainPassword))
            {
                return true;
            }

            var length = shainPassword.Length;
            // セキュリティレベル：高(標準)
            if (highSecurity)
            {
                // 半角英数字(大文字/小文字を区別)の2つ以上の組み合せ8～32文字
                if (length < 8 || 32 < length)
                {
                    return false;
                }

                // 半角英数字(大文字/小文字を区別)の2つ以上の組み合せか
                // 英数字で構成されているか
                var reg1 = new Regex("^[0-9A-z]+$").IsMatch(shainPassword);
                // 数字のみ、もしくは英字のみで構成されているか
                var reg2 = new Regex("^([0-9]*|[A-Z]*|[a-z]*)$").IsMatch(shainPassword);

                if (reg1 && !reg2)
                {
                }
                else
                {
                    return false;
                }
            }
            // セキュリティレベル：低
            else
            {
                // 半角英数字(大文字/小文字を区別)の4～32文字
                if (length < 4 || 32 < length)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ポップアップ系

        #region ロールのポップアップ
        /// <summary>
        /// 車種グループポップアップ設定(固定値)
        /// </summary>
        /// <param name="e"></param>
        public void CheckPopup1(KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (e.KeyCode == Keys.Space)
            {
                if (this.form.Ichiran1.Columns[this.form.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("SHAIN_NAVI_ROLE"))
                {
                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;
                    row = dt.NewRow();
                    row["CD"] = "mastersp";
                    row["VALUE"] = "システム管理者＋スマホ操作";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "master";
                    row["VALUE"] = "システム管理者";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "managersp";
                    row["VALUE"] = "受付・配車業務＋スマホ操作";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "manager";
                    row["VALUE"] = "受付・配車業務";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "workerpc";
                    row["VALUE"] = "作業者情報＋スマホ操作";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "worker";
                    row["VALUE"] = "スマホ操作";
                    dt.Rows.Add(row);
                    dt.TableName = "業務(ロール)";
                    form.table = dt;
                    form.PopupTitleLabel = "業務(ロール)";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "ロール", "業務内容" };

                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.Ichiran1.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.Ichiran1.Rows[this.form.Ichiran1.CurrentCell.RowIndex].Cells["SHAIN_BUSINESS_CONTENT"].Value = form.ReturnParams[1][0].Value.ToString();
                    }
                }
            }
        }
        #endregion

        #region ステータスのポップアップ
        /// <summary>
        /// ステータス
        /// </summary>
        /// <param name="e"></param>
        internal void StatusPopup(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_MIRENKEI)
                {
                    row = dt.NewRow();
                    row["CD"] = ConstCls.STATUS_MIRENKEI;
                    row["VALUE"] = ConstCls.STATUS_MIRENKEI_STRING;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = ConstCls.STATUS_SASHIMODOSHI;
                    row["VALUE"] = ConstCls.STATUS_SASHIMODOSHI_STRING;
                    dt.Rows.Add(row);
                }
                if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_RENKEIZUMI)
                {
                    row = dt.NewRow();
                    row["CD"] = ConstCls.STATUS_RENKEIKAKUNINNYOU;
                    row["VALUE"] = ConstCls.STATUS_RENKEIKAKUNINNYOU_STRING;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = ConstCls.STATUS_RENKEIKANRYOU;
                    row["VALUE"] = ConstCls.STATUS_RENKEIKANRYOU_STRING;
                    dt.Rows.Add(row);
                }
                if (this.renkeiKouho == ConstCls.RENKEI_KOUHO_SAIRENKEI)
                {
                    row = dt.NewRow();
                    row["CD"] = ConstCls.STATUS_SAIRENKEIKOUHO;
                    row["VALUE"] = ConstCls.STATUS_SAIRENKEIKOUHO_STRING;
                    dt.Rows.Add(row);
                }

                dt.TableName = "ステータス";
                form.table = dt;
                form.PopupTitleLabel = "ステータス";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "ステータスCD", "ステータス" };

                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.form.STATUS_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.form.STATUS_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }
        #endregion

        #endregion

        #region その他

        internal string GetCellValue(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }

        #endregion

        #region 未使用
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
