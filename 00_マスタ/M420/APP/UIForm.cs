using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.Logic;

namespace Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP
{
    /// <summary>
    /// M420：最終処分場所パターン一覧（最終・中間）
    /// </summary>
    [Implementation]
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region 内部変数

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass ShobunBasyoLogic;

        /// <summary>
        /// 画面初期化(true:ロード済み、false：未実施)
        /// </summary>
        private bool IsLoaded;
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public List<string> NotSearchColumn { get; set; }

        #endregion

        #region 画面返却値

        /// <summary>
        /// 画面選択したパターン名
        /// </summary>
        public string OutSelectedPatternName { set; get; }

        #endregion

        #region プロパティ

        /// <summary>
        /// 親フォーム
        /// </summary>
        internal BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal UIHeader HeaderForm { get; private set; }

        /// <summary>
        /// 全てのコントロール
        /// </summary>
        internal Control[] AllControl { get; private set; }

        public string selectSql { get; set; }

        public string joinSql { get; set; }

        public string orderSql { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="kbn"></param>
        public UIForm(DENSHU_KBN kbn)
            : base(kbn, false)
        {
            LogUtility.DebugMethodStart(kbn);

            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.ShobunBasyoLogic = new LogicClass(this);

            // 伝種区分によって画面表示を切り替える
            base.DenshuKbn = kbn;

            // 社員CDをIchiranSuperFormに保存
            base.ShainCd = SystemProperty.Shain.CD;

            // ロードフラグ
            this.IsLoaded = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面Load処理

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                if (!this.DesignMode)
                {
                    // パターンボタン押下で毎回このOnLoadが呼ばれるためIsLoadedプロパティ参照
                    if (!this.IsLoaded)
                    {
                        // 他フォームを保存
                        this.ParentBaseForm = (BusinessBaseForm)this.Parent;
                        this.HeaderForm = (UIHeader)this.ParentBaseForm.headerForm;

                        // 画面初期化
                        if (!this.ShobunBasyoLogic.WindowInit())
                        {
                            return;
                        }

                        this.customSearchHeader1.Visible = true;
                        this.customSearchHeader1.Location = new System.Drawing.Point(4, 120);
                        this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                        this.customSortHeader1.Location = new System.Drawing.Point(4, 142);
                        this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                        this.customDataGridView1.Location = new System.Drawing.Point(3, 169);
                        this.customDataGridView1.Size = new System.Drawing.Size(997, 285);

                        this.PatternReload(true);

                        // 画面初期化完了
                        this.IsLoaded = true;

                        // Anchorの設定は必ずOnLoadで行うこと
                        if (this.customDataGridView1 != null)
                        {
                            this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                        }
                    }

                    // パターンボタン押下時はソート条件初期化を行う
                    this.customSortHeader1.ClearCustomSortSetting();

                    // パターンボタン押下時はソート条件初期化を行う
                    this.customSortHeader1.ClearCustomSortSetting();

                    this.SetDataGridViewColumns();

                    this.SetDataGridViewSql();

                    // パターンボタン押下時は0件初期化を行う
                    this.ShobunBasyoLogic.SetZeroGridData();

                    // ヘッダの読み込み件数更新
                    this.HeaderForm.readDataNumber.Text = "0";

                    // 一覧グリッド更新
                    this.ShowIchiranData();
                    // ------------------------------------------

                    this.NotSearchColumn = new List<string>();
                    this.NotSearchColumn.Add("削除");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(e);
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }

        #endregion

        #region 一覧データグリッド表示

        /// <summary>
        /// 一覧データグリッド表示
        /// </summary>
        public virtual void ShowIchiranData()
        {
            LogUtility.DebugMethodStart();

            // IchiranSuperFormに表示データを渡す
            base.Table = this.ShobunBasyoLogic.SearchResult;

            if (!this.DesignMode)
            {
                // IchiranSuperFormのグリッド一覧表示メソッドを呼び出す
                base.logic.AlertCount = this.HeaderForm.AlertCount;
                base.logic.CreateDataGridView(this.Table);

                // グリッド情報再設定
                this.ShobunBasyoLogic.RefreshIchiranGrid();
            }


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// データグリッドのヘッダ設定
        /// </summary>
        public virtual void SetDataGridViewColumns()
        {
            this.Table = new System.Data.DataTable();
            this.Table.Columns.Add("パターン名");
            this.Table.Columns.Add("パターンフリガナ");
            this.Table.Columns.Add("処分受託者CD");
            this.Table.Columns.Add("処分受託者名");
            this.Table.Columns.Add("処分受託者都道府県");
            this.Table.Columns.Add("処分受託者住所１");
            this.Table.Columns.Add("処分受託者住所２");

            switch (this.DenshuKbn)
            {
                // 中間処分場所パターン一覧(産廃・建廃)
                case r_framework.Const.DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                case r_framework.Const.DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                    this.Table.Columns.Add("処分事業場CD");
                    this.Table.Columns.Add("処分事業場名");
                    this.Table.Columns.Add("処分事業場都道府県");
                    this.Table.Columns.Add("処分事業場住所１");
                    this.Table.Columns.Add("処分事業場住所２");
                    this.Table.Columns.Add("処分方法CD");
                    this.Table.Columns.Add("処分方法名");
                    this.Table.Columns.Add("施設の処理能力");
                    break;
                // 最終処分場所パターン一覧(産廃)
                case r_framework.Const.DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                    this.Table.Columns.Add("最終処分場CD");
                    this.Table.Columns.Add("最終処分場名");
                    this.Table.Columns.Add("最終処分場都道府県");
                    this.Table.Columns.Add("最終処分場住所１");
                    this.Table.Columns.Add("最終処分場住所２");
                    this.Table.Columns.Add("処分方法CD");
                    this.Table.Columns.Add("処分方法名");
                    this.Table.Columns.Add("施設の処理能力");
                    break;
                // 最終処分場所パターン一覧(建廃)
                case r_framework.Const.DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                    this.Table.Columns.Add("最終処分場CD");
                    this.Table.Columns.Add("最終処分場名");
                    this.Table.Columns.Add("最終処分場都道府県");
                    this.Table.Columns.Add("最終処分場住所１");
                    this.Table.Columns.Add("最終処分場住所２");
                    this.Table.Columns.Add("報告書分類CD");
                    this.Table.Columns.Add("報告書分類名");
                    this.Table.Columns.Add("処分方法CD");
                    this.Table.Columns.Add("処分方法名");
                    this.Table.Columns.Add("施設の処理能力");
                    this.Table.Columns.Add("備考処理後の廃棄物");
                    this.Table.Columns.Add("分類");
                    this.Table.Columns.Add("中間・最終の区分");
                    this.Table.Columns.Add("処分先№");
                    break;
            }
        }

        /// <summary>
        /// データグリッド用なSQLの設定
        /// </summary>
        public virtual void SetDataGridViewSql()
        {
            var sql = new StringBuilder();
            sql.Append(" M_SBNB_PATTERN.PATTERN_NAME AS \"パターン名\", ");
            sql.Append(" M_SBNB_PATTERN.PATTERN_FURIGANA AS \"パターンフリガナ\", ");
            sql.Append(" M_SBNB_PATTERN.GYOUSHA_CD AS \"処分受託者CD\", ");
            sql.Append(" M_SBNB_PATTERN.GYOUSHA_NAME AS \"処分受託者名\", ");
            sql.Append(" MT1.TODOUFUKEN_NAME_RYAKU AS \"処分受託者都道府県\", ");
            sql.Append(" M_SBNB_PATTERN.GYOUSHA_ADDRESS1 AS \"処分受託者住所１\", ");
            sql.Append(" M_SBNB_PATTERN.GYOUSHA_ADDRESS2 AS \"処分受託者住所２\", ");

            switch (this.DenshuKbn)
            {
                // 中間処分場所パターン一覧(産廃・建廃)
                case r_framework.Const.DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                case r_framework.Const.DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                    sql.Append(" M_SBNB_PATTERN.GENBA_CD AS \"処分事業場CD\", ");
                    sql.Append(" M_SBNB_PATTERN.GENBA_NAME AS \"処分事業場名\", ");
                    sql.Append(" MT2.TODOUFUKEN_NAME_RYAKU AS \"処分事業場都道府県\", ");
                    sql.Append(" M_SBNB_PATTERN.GENBA_ADDRESS1 AS \"処分事業場住所１\", ");
                    sql.Append(" M_SBNB_PATTERN.GENBA_ADDRESS2 AS \"処分事業場住所２\", ");
                    sql.Append(" M_SBNB_PATTERN.SHOBUN_HOUHOU_CD AS \"処分方法CD\", ");
                    sql.Append(" M_SHOBUN_HOUHOU1.SHOBUN_HOUHOU_NAME_RYAKU AS \"処分方法名\", ");
                    sql.Append(" M_SBNB_PATTERN.SHORI_SPEC AS \"施設の処理能力\" ");
                    break;
                // 最終処分場所パターン一覧(産廃)
                case r_framework.Const.DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                    sql.Append(" M_SBNB_PATTERN.GENBA_CD AS \"最終処分場CD\", ");
                    sql.Append(" M_SBNB_PATTERN.GENBA_NAME AS \"最終処分場名\", ");
                    sql.Append(" MT2.TODOUFUKEN_NAME_RYAKU AS \"最終処分場都道府県\", ");
                    sql.Append(" M_SBNB_PATTERN.GENBA_ADDRESS1 AS \"最終処分場住所１\", ");
                    sql.Append(" M_SBNB_PATTERN.GENBA_ADDRESS2 AS \"最終処分場住所２\", ");
                    sql.Append(" M_SBNB_PATTERN.SHOBUN_HOUHOU_CD AS \"処分方法CD\", ");
                    sql.Append(" M_SHOBUN_HOUHOU1.SHOBUN_HOUHOU_NAME_RYAKU AS \"処分方法名\", ");
                    sql.Append(" M_SBNB_PATTERN.SHORI_SPEC AS \"施設の処理能力\" ");
                    break;
                // 最終処分場所パターン一覧(建廃)
                case r_framework.Const.DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                    sql.Append(" M_SBNB_PATTERN.GENBA_CD AS \"最終処分場CD\", ");
                    sql.Append(" M_SBNB_PATTERN.GENBA_NAME AS \"最終処分場名\", ");
                    sql.Append(" MT2.TODOUFUKEN_NAME_RYAKU AS \"最終処分場都道府県\", ");
                    sql.Append(" M_SBNB_PATTERN.GENBA_ADDRESS1 AS \"最終処分場住所１\", ");
                    sql.Append(" M_SBNB_PATTERN.GENBA_ADDRESS2 AS \"最終処分場住所２\", ");
                    sql.Append(" M_SBNB_PATTERN.HOUKOKUSHO_BUNRUI_CD AS \"報告書分類CD\", ");
                    sql.Append(" M_SBNB_PATTERN.HOUKOKUSHO_BUNRUI_NAME AS \"報告書分類名\", ");
                    sql.Append(" M_SBNB_PATTERN.SHOBUN_HOUHOU_CD AS \"処分方法CD\", ");
                    sql.Append(" M_SHOBUN_HOUHOU1.SHOBUN_HOUHOU_NAME_RYAKU AS \"処分方法名\", ");
                    sql.Append(" M_SBNB_PATTERN.SHORI_SPEC AS \"施設の処理能力\", ");
                    sql.Append(" M_SBNB_PATTERN.OTHER AS \"備考処理後の廃棄物\", ");
                    sql.Append(" M_SBNB_PATTERN.BUNRUI AS \"分類\", ");
                    sql.Append(" M_SBNB_PATTERN.END_KUBUN AS \"中間・最終の区分\", ");
                    sql.Append(" M_SBNB_PATTERN.SHOBUNSAKI_NO AS \"処分先№\" ");
                    break;
            }

            this.selectSql = sql.ToString();

            this.joinSql = " LEFT JOIN M_GYOUSHA ON M_SBNB_PATTERN.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ";
            this.joinSql += " LEFT JOIN M_TODOUFUKEN MT1 ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = MT1.TODOUFUKEN_CD ";
            this.joinSql += " LEFT JOIN M_GENBA ON M_SBNB_PATTERN.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_SBNB_PATTERN.GENBA_CD = M_GENBA.GENBA_CD ";
            this.joinSql += " LEFT JOIN M_TODOUFUKEN MT2 ON M_GENBA.GENBA_TODOUFUKEN_CD = MT2.TODOUFUKEN_CD ";
            this.joinSql += " LEFT JOIN M_SHOBUN_HOUHOU M_SHOBUN_HOUHOU1 ON M_SBNB_PATTERN.SHOBUN_HOUHOU_CD = M_SHOBUN_HOUHOU1.SHOBUN_HOUHOU_CD ";

            var odSql = new StringBuilder();
            odSql.Append(" \"パターン名\" ASC ");
            odSql.Append(",\"パターンフリガナ\" ASC ");
            odSql.Append(",\"処分受託者CD\" ASC ");
            odSql.Append(",\"処分受託者名\" ASC ");
            odSql.Append(",\"処分受託者都道府県\" ASC ");
            odSql.Append(",\"処分受託者住所１\" ASC ");
            odSql.Append(",\"処分受託者住所２\" ASC ");

            switch (this.DenshuKbn)
            {
                // 中間処分場所パターン一覧(産廃・建廃)
                case r_framework.Const.DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                case r_framework.Const.DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                    odSql.Append(",\"処分事業場CD\" ASC");
                    odSql.Append(",\"処分事業場名\" ASC");
                    odSql.Append(",\"処分事業場都道府県\" ASC");
                    odSql.Append(",\"処分事業場住所１\" ASC");
                    odSql.Append(",\"処分事業場住所２\" ASC");
                    odSql.Append(",\"処分方法CD\" ASC");
                    odSql.Append(",\"処分方法名\" ASC");
                    odSql.Append(",\"施設の処理能力\" ASC");
                    break;
                // 最終処分場所パターン一覧(産廃)
                case r_framework.Const.DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                    odSql.Append(",\"最終処分場CD\" ASC");
                    odSql.Append(",\"最終処分場名\" ASC");
                    odSql.Append(",\"最終処分場都道府県\" ASC");
                    odSql.Append(",\"最終処分場住所１\" ASC");
                    odSql.Append(",\"最終処分場住所２\" ASC");
                    odSql.Append(",\"処分方法CD\" ASC");
                    odSql.Append(",\"処分方法名\" ASC");
                    odSql.Append(",\"施設の処理能力\" ASC");
                    break;
                // 最終処分場所パターン一覧(建廃)
                case r_framework.Const.DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                    odSql.Append(",\"最終処分場CD\" ASC");
                    odSql.Append(",\"最終処分場名\" ASC");
                    odSql.Append(",\"最終処分場都道府県\" ASC");
                    odSql.Append(",\"最終処分場住所１\" ASC");
                    odSql.Append(",\"最終処分場住所２\" ASC");
                    odSql.Append(",\"報告書分類CD\" ASC");
                    odSql.Append(",\"報告書分類名\" ASC");
                    odSql.Append(",\"処分方法CD\" ASC");
                    odSql.Append(",\"処分方法名\" ASC");
                    odSql.Append(",\"施設の処理能力\" ASC");
                    odSql.Append(",\"備考処理後の廃棄物\" ASC");
                    odSql.Append(",\"分類\" ASC");
                    odSql.Append(",\"中間・最終の区分\" ASC");
                    odSql.Append(",\"処分先№\" ASC");
                    break;
            }
            this.orderSql = odSql.ToString();
        }

        #endregion
    }
}
