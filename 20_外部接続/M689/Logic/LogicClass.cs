using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
//ロジこん連携用
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.DigitachoMasterRenkei
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        private DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        private string SearchString { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            //DAO(一覧用)
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();

            //Dao(出力済タイムスタンプテーブル用)
            this.daoShashuTS = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_SHASHUDao>();
            this.daoSharyouTS = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_SHARYOUDao>();
            this.daoShuruiTS = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_SHURUIDao>();
            this.daoUnitTS = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_UNITDao>();
            this.daoHinmeiTS = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_HINMEIDao>();
            this.daoShainTS = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_SHAINDao>();

            //ロジこん
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.logiConnectDao = DaoInitUtility.GetComponent<IS_LOGI_CONNECTDao>();

            //エラーメッセージ
            this.errmessage = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.DigitachoMasterRenkei.Setting.ButtonSetting.xml";

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        internal MessageBoxShowLogic errmessage;

        #region 画面コントロール系
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// DGV用
        /// </summary>
        internal DataGridView meisaiForm { get; set; }

        /// <summary>
        /// 一覧GRID：X＝4
        /// </summary>
        private int mIchiranLocationX = 0;

        /// <summary>
        /// 一覧GRID：Y＝214
        /// </summary>
        private int mIchiranLocationY = 46;

        /// <summary>
        /// チェックボックス宣言用
        /// </summary>
        private string chkRenkei { get; set; }
        #endregion

        #region 必須チェック用
        /// <summary>
        /// 必須項目チェックに使用1
        /// </summary>
        internal string chkColumn { get; set; }
        /// <summary>
        /// 必須項目チェックに使用2
        /// </summary>
        internal string chkColumnName { get; set; }
        /// <summary>
        /// 必須項目チェックに使用3
        /// </summary>
        internal string chkColmnPK1 { get; set; }
        /// <summary>
        /// 必須項目チェックに使用4
        /// </summary>
        internal string chkColmnPK2 { get; set; }

        #endregion

        #region 抽出条件
        /// <summary>
        /// 連携マスタ(1.車種)
        /// </summary>
        const int RENKEI_SHASHU = 1;

        /// <summary>
        /// 連携マスタ(2.車輌)
        /// </summary>
        const int RENKEI_SHARYOU = 2;

        /// <summary>
        /// 連携マスタ(3.品名)
        /// </summary>
        const int RENKEI_HINMEI = 3;

        /// <summary>
        /// 連携マスタ(4.単位)
        /// </summary>
        const int RENKEI_UNIT = 4;

        /// <summary>
        /// 連携マスタ(5.種類)
        /// </summary>
        const int RENKEI_SHURUI = 5;

        /// <summary>
        /// 連携マスタ(6.社員)
        /// </summary>
        const int RENKEI_SHAIN = 6;

        /// <summary>
        /// 連携マスタフラグ
        /// </summary>
        internal int renkeimaster_flg { get; set; }

        /// <summary>
        /// 未連携
        /// </summary>
        const int RENKEI_NOT = 1;

        /// <summary>
        /// 連携済み
        /// </summary>
        const int RENKEI_ALREADY = 2;

        /// <summary>
        /// 再連携候補
        /// </summary>
        const int RENKEI_AGAIN = 3;

        /// <summary>
        /// 連携候補フラグ
        /// </summary>
        internal int renkeikouho_flg { get; set; }

        /// <summary>
        /// 連携対象
        /// </summary>
        const int RENKEI_TAISHOU = 1;

        /// <summary>
        /// 連携除外
        /// </summary>
        const int RENKEI_JOGAI = 2;

        /// <summary>
        /// 検索条件フラグ
        /// </summary>
        internal int kensaku_Flg { get; set; }
        /// <summary>

        /// 作成したSQL
        /// </summary>
        private string mcreateSql { get; set; }

        /// <summary>
        /// 抽出に使用するテーブル名
        /// </summary>
        private string tblName { get; set; }
        #endregion

        #region Dao
        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;
        /// <summary>
        /// 車種出力済みDao
        /// </summary>
        private IM_DIGI_OUTPUT_SHASHUDao daoShashuTS;
        /// <summary>
        /// 車輌出力済みDao
        /// </summary>
        private IM_DIGI_OUTPUT_SHARYOUDao daoSharyouTS;
        /// <summary>
        /// 種類出力済みDao
        /// </summary>
        private IM_DIGI_OUTPUT_SHURUIDao daoShuruiTS;
        /// <summary>
        /// 単位出力済みDao
        /// </summary>
        private IM_DIGI_OUTPUT_UNITDao daoUnitTS;
        /// <summary>
        /// 品名出力済みDao
        /// </summary>
        private IM_DIGI_OUTPUT_HINMEIDao daoHinmeiTS;
        /// <summary>
        /// 社員出力済みDao
        /// </summary>
        private IM_DIGI_OUTPUT_SHAINDao daoShainTS;
        /// <summary>
        /// ロジこん連携用Dao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        private IS_LOGI_CONNECTDao logiConnectDao;

        #endregion

        #endregion

        #region ボタンの初期化
        /// <summary>
        /// ボタン設定の読み込み
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonsetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonsetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();

            //親フォームのボタン表示
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベント初期化
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //除外登録(F1)イベント作成
            parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);
            //除外解除登録(F2)イベント作成
            parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);
            //削除連携(F4)イベント作成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);
            //検索(F8)イベント作成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            //登録連携(F9)イベント作成
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            //閉じる(F12)イベント作成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //連携マスタイベント作成
            this.form.txtNum_RenkeiMaster.TextChanged += new EventHandler(this.txtNum_RenkeiMaster_TextChanged);
            this.form.txtNum_RenkeiMaster.Leave += new EventHandler(this.txtNum_RenkeiMaster_Leave);
            //連携候補イベント作成
            this.form.txtNum_RenkeiKouho.TextChanged += new EventHandler(this.txtNum_RenkeiKouho_TextChanged);
            this.form.txtNum_RenkeiKouho.Leave += new EventHandler(this.txtNum_RenkeiKouho_Leave);
            //検索条件イベント作成
            this.form.txtNum_KensakuJoken.TextChanged += new EventHandler(this.txtNum_KensakuJoken_TextChanged);
            this.form.txtNum_KensakuJoken.Leave += new EventHandler(this.txtNum_KensakuJoken_Leave);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 抽出条件初期化
        /// <summary>
        /// 抽出条件の初期化
        /// </summary>
        private void SetInitialRenkeiCondition()
        {
            //初期値は車種
            this.form.txtNum_RenkeiMaster.Text = RENKEI_SHASHU.ToString();
            this.renkeimaster_flg = int.Parse(this.form.txtNum_RenkeiMaster.Text);

            //初期値は未連携
            this.form.txtNum_RenkeiKouho.Text = RENKEI_NOT.ToString();
            this.renkeikouho_flg = int.Parse(this.form.txtNum_RenkeiKouho.Text);

            //初期値は連携対象
            this.form.txtNum_KensakuJoken.Text = RENKEI_TAISHOU.ToString();
            this.kensaku_Flg = int.Parse(this.form.txtNum_KensakuJoken.Text);

            //FunctionのEnabled変更
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

                //ボタンのテキストを初期化
                this.ButtonInit();

                //一覧内のチェックボックスの設定
                this.HeaderCheckBoxSupport();

                //検索条件の初期化処理
                this.SetInitialRenkeiCondition();

                //イベントの初期化処理
                this.EventInit();

                //表示明細の初期化
                this.DetailChange();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret;
        }

        #endregion

        #region 各項目のイベント処理

        #region 連携マスタ変更時の処理
        /// <summary>
        /// 連携マスタが変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txtNum_RenkeiMaster_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //連携マスタの値を変数にセット
            if (this.form.txtNum_RenkeiMaster.Text == string.Empty)
            {
                this.renkeimaster_flg = 0;
            }
            else
            {
                this.renkeimaster_flg = int.Parse(this.form.txtNum_RenkeiMaster.Text);
            }
            //明細の表示切替
            this.DetailChange();

            LogUtility.DebugMethodEnd(sender, e);
        }
        /// <summary>
        /// 連携マスタ未入力不許可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_RenkeiMaster_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (string.IsNullOrEmpty(this.form.txtNum_RenkeiMaster.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.form.txtNum_RenkeiMaster.Focus();
                this.form.txtNum_RenkeiMaster.Text = "1";
                this.errmessage.MessageBoxShow("W001", "1", "6");

            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 連携候補変更時の処理
        /// <summary>
        /// 連携候補が変更された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_RenkeiKouho_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //連携候補の値を変数にセット
            if (this.form.txtNum_RenkeiKouho.Text == string.Empty)
            {
                this.renkeikouho_flg = 0;
            }
            else
            {
                this.renkeikouho_flg = int.Parse(this.form.txtNum_RenkeiKouho.Text);
            }

            //FunctionのEnabled変更
            this.FunctionControl();

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 連携候補未入力不許可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_RenkeiKouho_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.form.txtNum_RenkeiKouho.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.form.txtNum_RenkeiKouho.Focus();
                this.form.txtNum_RenkeiKouho.Text = "1";
                this.errmessage.MessageBoxShow("W001", "1", "3");

            }

            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion

        #region 検索条件変更時の処理
        /// <summary>
        /// 検索条件が変更された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_KensakuJoken_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //検索条件の値を変数にセット
            if (this.form.txtNum_KensakuJoken.Text == string.Empty)
            {
                this.kensaku_Flg = 0;
            }
            else
            {
                this.kensaku_Flg = int.Parse(this.form.txtNum_KensakuJoken.Text);
            }

            if (this.kensaku_Flg == RENKEI_TAISHOU)
            {
                //連携対象選択時、連携候補には「2.連携済み」をセット
                this.form.txtNum_RenkeiKouho.Text = RENKEI_ALREADY.ToString();
            }
            else
            {
                //連携除外選択時、連携候補には「1.未連携」をセット
                this.form.txtNum_RenkeiKouho.Text = RENKEI_NOT.ToString();
            }

            //FunctionのEnabled変更
            this.FunctionControl();

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 検索条件未入力不許可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_KensakuJoken_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.form.txtNum_KensakuJoken.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.form.txtNum_KensakuJoken.Focus();
                this.form.txtNum_KensakuJoken.Text = "1";
                this.errmessage.MessageBoxShow("W001", "1", "3");

            }

            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion

        #endregion

        #region ファンクションEnabled切替
        /// <summary>
        /// ファンクションボタンのEnabledを切り替える
        /// </summary>
        private void FunctionControl()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            parentForm.bt_func1.Enabled = true;
            parentForm.bt_func2.Enabled = true;
            parentForm.bt_func4.Enabled = true;
            parentForm.bt_func9.Enabled = true;

            if (kensaku_Flg == 1)
            {
                //連携対象
                parentForm.bt_func2.Enabled = false;
                if (this.renkeikouho_flg == 1)
                {
                    //未連携は削除連携不可に
                    parentForm.bt_func4.Enabled = false;
                }
                else
                {
                    //連携済は除外登録不可に
                    parentForm.bt_func1.Enabled = false;
                }
            }
            else
            {
                //連携除外
                parentForm.bt_func1.Enabled = false;
                parentForm.bt_func2.Enabled = true;
                parentForm.bt_func4.Enabled = false;
                parentForm.bt_func9.Enabled = false;
            }
        }
        #endregion

        #region 明細クリア
        /// <summary>
        /// 各DGVをクリアする
        /// </summary>
        private void DetailClear()
        {
            //全明細クリア
            this.form.Ichiran1.Rows.Clear();
            this.form.Ichiran2.Rows.Clear();
            this.form.Ichiran3.Rows.Clear();
            this.form.Ichiran4.Rows.Clear();
            this.form.Ichiran5.Rows.Clear();
            this.form.Ichiran6.Rows.Clear();
        }
        #endregion

        #region 明細切替処理
        /// <summary>
        /// 明細切替処理
        /// 必須項目チェックに使用する変数もここで一緒にセットしておく
        /// </summary>
        internal void DetailChange()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //明細をクリアする
                this.DetailClear();

                //全明細非表示
                this.form.Ichiran1.Visible = false;
                this.form.Ichiran2.Visible = false;
                this.form.Ichiran3.Visible = false;
                this.form.Ichiran4.Visible = false;
                this.form.Ichiran5.Visible = false;
                this.form.Ichiran6.Visible = false;

                //対象の確定
                switch (this.renkeimaster_flg)
                {
                    case RENKEI_SHASHU:
                        this.meisaiForm = this.form.Ichiran1;
                        this.chkColumn = "SHASHU_DIGI_SHASHU_CD";
                        this.chkColumnName = "デジタコ車輌グループID";
                        this.chkColmnPK1 = "SHASHU_CD";
                        this.chkColmnPK2 = string.Empty;
                        break;
                    case RENKEI_SHARYOU:
                        this.meisaiForm = this.form.Ichiran2;
                        this.chkColumn = "SHARYOU_DIGI_SHARYOU_CD";
                        this.chkColumnName = "デジタコ車輌ID";
                        this.chkColmnPK1 = "SHARYOU_UPN_GYOUSHA_CD";
                        this.chkColmnPK2 = "SHARYOU_CD";
                        break;
                    case RENKEI_HINMEI:
                        this.meisaiForm = this.form.Ichiran3;
                        this.chkColumn = "HINMEI_DIGI_HINMEI_CD";
                        this.chkColumnName = "デジタコ品名ID";
                        this.chkColmnPK1 = "HINMEI_CD";
                        this.chkColmnPK2 = string.Empty;
                        break;
                    case RENKEI_UNIT:
                        this.meisaiForm = this.form.Ichiran4;
                        this.chkColumn = "UNIT_DIGI_UNIT_CD";
                        this.chkColumnName = "デジタコ品名単位ID";
                        this.chkColmnPK1 = "UNIT_CD";
                        this.chkColmnPK2 = string.Empty;
                        break;
                    case RENKEI_SHURUI:
                        this.meisaiForm = this.form.Ichiran5;
                        this.chkColumn = "SHURUI_DIGI_SHURUI_CD";
                        this.chkColumnName = "デジタコ品名分類ID";
                        this.chkColmnPK1 = "SHURUI_CD";
                        this.chkColmnPK2 = string.Empty;
                        break;
                    case RENKEI_SHAIN:
                        this.meisaiForm = this.form.Ichiran6;
                        this.chkColumn = "SHAIN_DIGI_SHAIN_CD";
                        this.chkColumnName = "デジタコ運転手ID";
                        this.chkColmnPK1 = "SHAIN_CD";
                        this.chkColmnPK2 = string.Empty;
                        break;
                }

                //DGVの位置を設定
                this.meisaiForm.Location = new Point(this.mIchiranLocationX, this.mIchiranLocationY);

                //選択したマスタに対応するDGVのみ表示
                this.meisaiForm.Visible = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailChange", ex);
                this.errmessage.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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

                var sql = new StringBuilder();

                //SQL生成
                //SELECT
                this.CreateSQLSelect(sql);
                //JOIN
                this.CreateSQLJoin(sql);
                //WHERE
                this.CreateSQLWhere(sql);
                //ORDER BY
                this.CreateSQLOrderBy(sql);

                this.mcreateSql = sql.ToString();

                //検索実行
                this.SearchResult = new DataTable();
                if (!string.IsNullOrEmpty(this.mcreateSql))
                {
                    this.SearchResult = this.daoIchiran.getdateforstringsql(this.mcreateSql);
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
                this.errmessage.MessageBoxShow("E093");
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245");
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
            switch (this.renkeimaster_flg)
            {
                case RENKEI_SHASHU:
                    tblName = "M_SHASHU";
                    sql.AppendFormat(" SELECT DISTINCT {0}.SHASHU_CD, {0}.SHASHU_NAME_RYAKU AS SHASHU_NAME, DTO.DIGI_SHASHU_CD, ", tblName);
                    break;
                case RENKEI_SHARYOU:
                    tblName = "M_SHARYOU";
                    sql.AppendFormat(" SELECT DISTINCT {0}.GYOUSHA_CD, M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME1, {0}.SHARYOU_CD, {0}.SHARYOU_NAME_RYAKU AS SHARYOU_NAME, {0}.SHASYU_CD, DTO2.DIGI_SHASHU_CD , DTO.DIGI_SHARYOU_CD, ", tblName);
                    break;
                case RENKEI_HINMEI:
                    tblName = "M_HINMEI";
                    sql.AppendFormat(" SELECT DISTINCT {0}.HINMEI_CD, {0}.HINMEI_NAME_RYAKU AS HINMEI_NAME, {0}.HINMEI_FURIGANA, {0}.UNIT_CD, M_UNIT.UNIT_NAME_RYAKU AS UNIT_NAME, {0}.SHURUI_CD, DTOU.DIGI_UNIT_CD, DTOS.DIGI_SHURUI_CD, DTO.DIGI_HINMEI_CD, ", tblName);
                    break;
                case RENKEI_UNIT:
                    tblName = "M_UNIT";
                    sql.AppendFormat(" SELECT DISTINCT {0}.UNIT_CD, {0}.UNIT_NAME_RYAKU AS UNIT_NAME, DTO.DIGI_UNIT_CD, ", tblName);
                    break;
                case RENKEI_SHURUI:
                    tblName = "M_SHURUI";
                    sql.AppendFormat(" SELECT DISTINCT {0}.SHURUI_CD, {0}.SHURUI_NAME_RYAKU AS SHURUI_NAME, DTO.DIGI_SHURUI_CD, ", tblName);
                    break;
                case RENKEI_SHAIN:
                    tblName = "M_SHAIN";
                    sql.AppendFormat(" SELECT DISTINCT {0}.SHAIN_CD, {0}.SHAIN_NAME_RYAKU AS SHAIN_NAME, {0}.SHAIN_FURIGANA, {0}.PASSWORD, {0}.SHAIN_BIKOU, DTO.DIGI_SHAIN_CD, ", tblName);
                    break;
            }

            //共通(DTO：M_DIGI_OUTPUT_○○)
            sql.AppendFormat(" CASE ");
            sql.AppendFormat("     WHEN DTO.OUTPUT_DATE IS NULL THEN ");
            sql.AppendFormat("         CASE ");
            sql.AppendFormat("             WHEN {0}.DELETE_FLG = 1 THEN 'NO_ACTION' ", tblName);
            sql.AppendFormat("             ELSE 'PUT' ");
            sql.AppendFormat("         END ");
            sql.AppendFormat("     WHEN {0}.UPDATE_DATE > DTO.OUTPUT_DATE THEN ", tblName);
            sql.AppendFormat("         CASE ");
            sql.AppendFormat("             WHEN {0}.DELETE_FLG = 1 THEN 'DELETE' ", tblName);
            sql.AppendFormat("             ELSE 'PUT' ");
            sql.AppendFormat("         END ");
            sql.AppendFormat("     ELSE 'NO_MODIFY' ");
            sql.AppendFormat(" END ACTION_CASE");

            //ACTION_FLG目視確認用
            sql.AppendFormat(", {0}.DELETE_FLG, {0}.UPDATE_DATE ", tblName);
            sql.AppendFormat(", DTO.OUTPUT_DATE ");

            sql.AppendFormat(" FROM {0} ", tblName);
        }
        #endregion
        #region JOIN
        /// <summary>
        /// JOIN句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLJoin(StringBuilder sql)
        {
            //JOIN句
            switch (this.renkeimaster_flg)
            {
                case RENKEI_SHASHU:
                    sql.AppendFormat(" LEFT JOIN M_DIGI_OUTPUT_SHASHU DTO ON {0}.SHASHU_CD = DTO.SHASHU_CD ", tblName);
                    break;
                case RENKEI_SHARYOU:
                    sql.Append(" LEFT JOIN M_GYOUSHA ON M_SHARYOU.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                    sql.AppendFormat(" LEFT JOIN M_DIGI_OUTPUT_SHARYOU DTO ON {0}.GYOUSHA_CD = DTO.GYOUSHA_CD AND {0}.SHARYOU_CD = DTO.SHARYOU_CD ", tblName);
                    sql.AppendFormat(" LEFT JOIN M_DIGI_OUTPUT_SHASHU DTO2 ON {0}.SHASYU_CD = DTO2.SHASHU_CD ", tblName);
                    break;
                case RENKEI_HINMEI:
                    sql.Append(" LEFT JOIN M_UNIT ON M_HINMEI.UNIT_CD = M_UNIT.UNIT_CD ");
                    sql.AppendFormat(" LEFT JOIN M_DIGI_OUTPUT_HINMEI DTO ON {0}.HINMEI_CD = DTO.HINMEI_CD ", tblName);
                    sql.AppendFormat(" LEFT JOIN M_DIGI_OUTPUT_UNIT DTOU ON {0}.UNIT_CD = DTOU.UNIT_CD ", tblName);
                    sql.AppendFormat(" LEFT JOIN M_DIGI_OUTPUT_SHURUI DTOS ON {0}.SHURUI_CD = DTOS.SHURUI_CD ", tblName);
                    break;
                case RENKEI_UNIT:
                    sql.AppendFormat(" LEFT JOIN M_DIGI_OUTPUT_UNIT DTO ON {0}.UNIT_CD = DTO.UNIT_CD ", tblName);
                    break;
                case RENKEI_SHURUI:
                    sql.AppendFormat(" LEFT JOIN M_DIGI_OUTPUT_SHURUI DTO ON {0}.SHURUI_CD = DTO.SHURUI_CD ", tblName);
                    break;
                case RENKEI_SHAIN:
                    sql.AppendFormat(" LEFT JOIN M_DIGI_OUTPUT_SHAIN DTO ON {0}.SHAIN_CD = DTO.SHAIN_CD ", tblName);
                    sql.AppendFormat(" LEFT JOIN M_SHAIN SHA ON {0}.SHAIN_CD = SHA.SHAIN_CD ", tblName);
                    break;
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
            //WHERE句
            sql.Append(" WHERE ");

            //連携候補・検索条件により分岐
            switch (this.renkeikouho_flg)
            {
                case RENKEI_NOT:
                    if (this.kensaku_Flg == 1)
                    {
                        //未連携・連携対象
                        //タイムスタンプテーブルが存在しないか、存在していても除外フラグが立っていないこと
                        sql.Append(" (DTO.JYOGAI_FLG IS NULL OR (DTO.OUTPUT_DATE IS NULL AND DTO.JYOGAI_FLG = 0)) ");
                        //車輌の場合のみ、車種連携済みの情報も条件に含める
                        if (this.renkeimaster_flg == RENKEI_SHARYOU)
                        {
                            sql.Append(" AND (DTO2.OUTPUT_DATE IS NOT NULL) ");
                        }
                        //品名検索の場合、伝種区分(3or9)伝票区分(1or2)も条件に加える
                        if (this.renkeimaster_flg == RENKEI_HINMEI)
                        {
                            sql.Append(" AND M_HINMEI.DENSHU_KBN_CD IN (3, 9) AND M_HINMEI.DENPYOU_KBN_CD IN (1, 2)");
                        }
                    }
                    else
                    {
                        //未連携・連携除外
                        //除外登録済みなのでタイムスタンプテーブルは存在する前提で
                        //出力日がNull、除外フラグが1のもの
                        sql.Append(" DTO.OUTPUT_DATE IS NULL AND DTO.JYOGAI_FLG = 1 ");
                        //車輌の場合のみ、車種の情報も条件に含める
                        if (this.renkeimaster_flg == RENKEI_SHARYOU)
                        {
                            sql.Append(" AND (DTO2.OUTPUT_DATE IS NOT NULL) ");
                        }
                    }
                    break;
                case RENKEI_ALREADY:
                    if (this.kensaku_Flg == 1)
                    {
                        //連携済み・連携対象
                        //出力日有り、除外フラグが0
                        sql.AppendFormat(" DTO.OUTPUT_DATE IS NOT NULL AND {0}.UPDATE_DATE <= DTO.OUTPUT_DATE AND DTO.JYOGAI_FLG = 0 ", tblName);
                        //車輌の場合のみ、車種の情報も条件に含める
                        if (this.renkeimaster_flg == RENKEI_SHARYOU)
                        {
                            sql.Append(" AND (DTO2.OUTPUT_DATE IS NOT NULL) ");
                        }
                    }
                    else
                    {
                        //連携済み・連携除外
                        //出力後に除外登録した、というデータ
                        //基本的にはほぼないはず
                        sql.AppendFormat(" DTO.OUTPUT_DATE IS NOT NULL AND {0}.UPDATE_DATE <= DTO.OUTPUT_DATE AND DTO.JYOGAI_FLG = 1 ", tblName);
                        //車輌の場合のみ、車種の情報も条件に含める
                        if (this.renkeimaster_flg == RENKEI_SHARYOU)
                        {
                            sql.Append(" AND (DTO2.OUTPUT_DATE IS NOT NULL) ");
                        }
                    }
                    break;
                case RENKEI_AGAIN:
                    //検索条件
                    if (this.kensaku_Flg == 1)
                    {
                        //再連携対象・連携対象
                        sql.AppendFormat(" DTO.OUTPUT_DATE IS NOT NULL AND {0}.UPDATE_DATE > DTO.OUTPUT_DATE AND DTO.JYOGAI_FLG = 0 ", tblName);
                        //車輌の場合のみ、車種の情報も条件に含める
                        if (this.renkeimaster_flg == RENKEI_SHARYOU)
                        {
                            sql.Append(" AND (DTO2.OUTPUT_DATE IS NOT NULL) ");
                        }
                    }
                    else
                    {
                        //再連携対象・連携除外
                        sql.AppendFormat(" DTO.OUTPUT_DATE IS NOT NULL AND {0}.UPDATE_DATE > DTO.OUTPUT_DATE AND DTO.JYOGAI_FLG = 1 ", tblName);
                        //車輌の場合のみ、車種の情報も条件に含める
                        if (this.renkeimaster_flg == RENKEI_SHARYOU)
                        {
                            sql.Append(" AND (DTO2.OUTPUT_DATE IS NOT NULL) ");
                        }
                    }
                    break;
            }

            //社員の場合は運転者のみ抽出
            if (this.renkeimaster_flg == RENKEI_SHAIN)
            {
                sql.Append(" AND SHA.UNTEN_KBN = 1");
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

                //明細をクリアする
                this.DetailClear();

                //抽出結果をDGVにセット
                if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
                {
                    meisaiForm.Rows.Add(this.SearchResult.Rows.Count);
                    for (int i = 0; i < meisaiForm.Rows.Count; i++)
                    {
                        DataGridViewRow row = meisaiForm.Rows[i];
                        DataRow dr = this.SearchResult.Rows[i];

                        switch (this.renkeimaster_flg)
                        {
                            case RENKEI_SHASHU:
                                row.Cells["SHASHU_RENKEI"].Value = false;
                                row.Cells["SHASHU_CD"].Value = dr["SHASHU_CD"];
                                row.Cells["SHASHU_NAME"].Value = dr["SHASHU_NAME"];
                                row.Cells["SHASHU_ACTION_CASE"].Value = dr["ACTION_CASE"];
                                row.Cells["SHASHU_DIGI_SHASHU_CD"].Value = dr["DIGI_SHASHU_CD"];
                                break;
                            case RENKEI_SHARYOU:
                                row.Cells["SHARYOU_RENKEI"].Value = false;
                                row.Cells["SHARYOU_UPN_GYOUSHA_CD"].Value = dr["GYOUSHA_CD"];
                                row.Cells["SHARYOU_UPN_GYOUSHA_NAME"].Value = dr["GYOUSHA_NAME1"];
                                row.Cells["SHARYOU_CD"].Value = dr["SHARYOU_CD"];
                                row.Cells["SHARYOU_NAME"].Value = dr["SHARYOU_NAME"];
                                row.Cells["SHARYOU_SHASHU_CD"].Value = dr["SHASYU_CD"];
                                row.Cells["SHARYOU_ACTION_CASE"].Value = dr["ACTION_CASE"];
                                row.Cells["SHARYOU_DIGI_SHASHU_CD"].Value = dr["DIGI_SHASHU_CD"];
                                row.Cells["SHARYOU_DIGI_SHARYOU_CD"].Value = dr["DIGI_SHARYOU_CD"];
                                break;
                            case RENKEI_HINMEI:
                                row.Cells["HINMEI_RENKEI"].Value = false;
                                row.Cells["HINMEI_CD"].Value = dr["HINMEI_CD"];
                                row.Cells["HINMEI_NAME"].Value = dr["HINMEI_NAME"];
                                row.Cells["HINMEI_FURIGANA"].Value = Strings.StrConv(dr["HINMEI_FURIGANA"].ToString(), VbStrConv.Wide, 0);  //全角に変換する
                                row.Cells["HINMEI_UNIT_CD"].Value = dr["UNIT_CD"];
                                row.Cells["HINMEI_UNIT_NAME"].Value = dr["UNIT_NAME"];
                                row.Cells["HINMEI_SHURUI_CD"].Value = dr["SHURUI_CD"];
                                row.Cells["HINMEI_ACTION_CASE"].Value = dr["ACTION_CASE"];
                                row.Cells["HINMEI_DIGI_UNIT_CD"].Value = dr["DIGI_UNIT_CD"];
                                row.Cells["HINMEI_DIGI_SHURUI_CD"].Value = dr["DIGI_SHURUI_CD"];
                                row.Cells["HINMEI_DIGI_HINMEI_CD"].Value = dr["DIGI_HINMEI_CD"];
                                break;
                            case RENKEI_UNIT:
                                row.Cells["UNIT_RENKEI"].Value = false;
                                row.Cells["UNIT_CD"].Value = dr["UNIT_CD"];
                                row.Cells["UNIT_NAME"].Value = dr["UNIT_NAME"];
                                row.Cells["UNIT_ACTION_CASE"].Value = dr["ACTION_CASE"];
                                row.Cells["UNIT_DIGI_UNIT_CD"].Value = dr["DIGI_UNIT_CD"];
                                break;
                            case RENKEI_SHURUI:
                                row.Cells["SHURUI_RENKEI"].Value = false;
                                row.Cells["SHURUI_CD"].Value = dr["SHURUI_CD"];
                                row.Cells["SHURUI_NAME"].Value = dr["SHURUI_NAME"];
                                row.Cells["SHURUI_ACTION_CASE"].Value = dr["ACTION_CASE"];
                                row.Cells["SHURUI_DIGI_SHURUI_CD"].Value = dr["DIGI_SHURUI_CD"];
                                break;
                            case RENKEI_SHAIN:
                                row.Cells["SHAIN_RENKEI"].Value = false;
                                row.Cells["SHAIN_CD"].Value = dr["SHAIN_CD"];
                                row.Cells["SHAIN_NAME"].Value = Strings.StrConv(dr["SHAIN_NAME"].ToString(), VbStrConv.Wide, 0);    //全角に変換する
                                row.Cells["SHAIN_FURIGANA"].Value = dr["SHAIN_FURIGANA"];
                                row.Cells["SHAIN_PASSWORD"].Value = dr["PASSWORD"];
                                row.Cells["SHAIN_TEL_NO"].Value = string.Empty;                 //マスタに項目ないのでEmpty
                                row.Cells["SHAIN_LICENSE_RENEWAL_DATE"].Value = string.Empty;   //マスタに項目ないのでEmpty
                                row.Cells["SHAIN_BIKOU"].Value = dr["SHAIN_BIKOU"];
                                row.Cells["SHAIN_ACTION_CASE"].Value = dr["ACTION_CASE"];
                                row.Cells["SHAIN_DIGI_SHAIN_CD"].Value = dr["DIGI_SHAIN_CD"];
                                break;
                        }
                    }
                }

                //入力制限をかける
                this.LimitationsColumn();

            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.errmessage.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 除外・除外解除登録
        /// <summary>
        /// 除外・除外解除登録
        /// </summary>
        /// <returns></returns>
        public int ExclusionRegist()
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                //DGVに表示されているデータ
                foreach (DataGridViewRow dgvRow in meisaiForm.Rows)
                {
                    //DVG内最初のカラム(連携)がONになっているものが対象
                    if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].Value == true)
                    {
                        this.TimeStampTableRegist(dgvRow);
                        ret_cnt++;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExclusionRegist", ex);
                this.errmessage.MessageBoxShow("E245");
                ret_cnt = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret_cnt;
        }
        #endregion

        #region データ送信
        /// <summary>
        /// データの送信
        /// JSON絡みの処理
        /// </summary>
        /// <returns></returns>
        internal int JsonConnection(HTTP_METHOD ReqMethod)
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                // 事前に必要情報を取得
                var sysInfo = this.sysInfoDao.GetAllDataForCode("0");

                var logic = new LogiLogic();

                //DELETEで使用するURL用変数初期化
                var apiURL = string.Empty;

                //DGVに表示されているデータ
                foreach (DataGridViewRow dgvRow in meisaiForm.Rows)
                {
                    //DVG内最初のカラム(連携)がONになっているものが対象
                    if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].Value == true)
                    {
                        //URLとRequestBodyを各々作る
                        switch (this.renkeimaster_flg)
                        {
                            //車種マスタ
                            case RENKEI_SHASHU:
                                var shashuConnect = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_CAR_GROUPS);
                                if (shashuConnect == null)
                                {
                                    this.errmessage.MessageBoxShow("S_LOGI_CONNECTのCarGroups情報なし！！");
                                    ret_cnt = -1;
                                    return ret_cnt;
                                }

                                if (ReqMethod == HTTP_METHOD.PUT)
                                {
                                    //車種PUT
                                    var shashuDto = new ExternalCommon.DTO.Logicompass.INFO_CAR_GROUPS();
                                    shashuDto.Company_Id = sysInfo.DIGI_CORP_ID;
                                    shashuDto.Car_Group_Id = dgvRow.Cells["SHASHU_DIGI_SHASHU_CD"].Value.ToString();
                                    shashuDto.Car_Group_Name = dgvRow.Cells["SHASHU_NAME"].Value.ToString();

                                    logic.HttpPut(shashuConnect.URL, shashuConnect.CONTENT_TYPE, shashuDto);
                                }
                                else
                                {
                                    //車種DELETE
                                    apiURL = string.Format(shashuConnect.URL + LogiConst.API_PARAMETER_URL_CAR_GROUPS, sysInfo.DIGI_CORP_ID, dgvRow.Cells["SHASHU_DIGI_SHASHU_CD"].Value);
                                    logic.HttpDelete(apiURL, shashuConnect.CONTENT_TYPE);
                                }
                                break;
                            //車輌マスタ
                            case RENKEI_SHARYOU:
                                var sharyouConnect = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_CAR_RELATIONS);
                                if (sharyouConnect == null)
                                {
                                    this.errmessage.MessageBoxShow("S_LOGI_CONNECTのCarRelations情報なし！！");
                                    ret_cnt = -1;
                                    return ret_cnt;
                                }

                                if (ReqMethod == HTTP_METHOD.PUT)
                                {
                                    //車輌PUT
                                    var sharyouDto = new ExternalCommon.DTO.Logicompass.INFO_CAR_RELATIONS();
                                    sharyouDto.Company_Id = sysInfo.DIGI_CORP_ID;
                                    sharyouDto.Car_Group_Id = dgvRow.Cells["SHARYOU_DIGI_SHASHU_CD"].Value.ToString();
                                    sharyouDto.Car_Id = dgvRow.Cells["SHARYOU_DIGI_SHARYOU_CD"].Value.ToString();

                                    logic.HttpPut(sharyouConnect.URL, sharyouConnect.CONTENT_TYPE, sharyouDto);
                                }
                                else
                                {
                                    //車輌DELETE
                                    apiURL = string.Format(sharyouConnect.URL + LogiConst.API_PARAMETER_URL_CAR_RELATIONS, sysInfo.DIGI_CORP_ID, dgvRow.Cells["SHARYOU_DIGI_SHASHU_CD"].Value, dgvRow.Cells["SHARYOU_DIGI_SHARYOU_CD"].Value);
                                    logic.HttpDelete(apiURL, sharyouConnect.CONTENT_TYPE);
                                }
                                break;
                            //品名マスタ
                            case RENKEI_HINMEI:
                                var hinmeiConnect = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_GOODS);
                                if (hinmeiConnect == null)
                                {
                                    this.errmessage.MessageBoxShow("S_LOGI_CONNECTのGoods情報なし！！");
                                    ret_cnt = -1;
                                    return ret_cnt;
                                }

                                if (ReqMethod == HTTP_METHOD.PUT)
                                {
                                    //品名PUT
                                    var hinmeiDto = new ExternalCommon.DTO.Logicompass.INFO_GOODS();
                                    hinmeiDto.Company_Id = sysInfo.DIGI_CORP_ID;
                                    hinmeiDto.Goods_Id = dgvRow.Cells["HINMEI_DIGI_HINMEI_CD"].Value.ToString();
                                    hinmeiDto.Goods_Name = dgvRow.Cells["HINMEI_NAME"].Value.ToString();
                                    hinmeiDto.Goods_Kana_Name = dgvRow.Cells["HINMEI_FURIGANA"].Value.ToString();
                                    hinmeiDto.Goods_Unit_Id = dgvRow.Cells["HINMEI_DIGI_UNIT_CD"].Value.ToString();
                                    hinmeiDto.Goods_Kind_Id = dgvRow.Cells["HINMEI_DIGI_SHURUI_CD"].Value.ToString();

                                    logic.HttpPut(hinmeiConnect.URL, hinmeiConnect.CONTENT_TYPE, hinmeiDto);
                                }
                                else
                                {
                                    //品名DELETE
                                    apiURL = string.Format(hinmeiConnect.URL + LogiConst.API_PARAMETER_URL_GOODS, sysInfo.DIGI_CORP_ID, dgvRow.Cells["HINMEI_DIGI_HINMEI_CD"].Value);
                                    logic.HttpDelete(apiURL, hinmeiConnect.CONTENT_TYPE);
                                }
                                break;
                            //単位マスタ
                            case RENKEI_UNIT:
                                var unitsConnect = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_GOODS_UNITS);
                                if (unitsConnect == null)
                                {
                                    this.errmessage.MessageBoxShow("S_LOGI_CONNECTのGoodsUnits情報なし！！");
                                    ret_cnt = -1;
                                    return ret_cnt;
                                }

                                if (ReqMethod == HTTP_METHOD.PUT)
                                {
                                    //単位PUT
                                    var unitsDto = new ExternalCommon.DTO.Logicompass.INFO_GOODS_UNITS();
                                    unitsDto.Company_Id = sysInfo.DIGI_CORP_ID;
                                    unitsDto.Goods_Unit_Id = dgvRow.Cells["UNIT_DIGI_UNIT_CD"].Value.ToString();
                                    unitsDto.Goods_Unit_Name = dgvRow.Cells["UNIT_NAME"].Value.ToString();

                                    logic.HttpPut(unitsConnect.URL, unitsConnect.CONTENT_TYPE, unitsDto);
                                }
                                else
                                {
                                    //単位DELETE
                                    apiURL = string.Format(unitsConnect.URL + LogiConst.API_PARAMETER_URL_GOODS_UNITS, sysInfo.DIGI_CORP_ID, dgvRow.Cells["UNIT_DIGI_UNIT_CD"].Value.ToString());
                                    logic.HttpDelete(apiURL, unitsConnect.CONTENT_TYPE);
                                }
                                break;
                            //種類マスタ
                            case RENKEI_SHURUI:
                                var shuruiConnect = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_GOODS_KINDS);
                                if (shuruiConnect == null)
                                {
                                    this.errmessage.MessageBoxShow("S_LOGI_CONNECTのGoodsKinds情報なし！！");
                                    ret_cnt = -1;
                                    return ret_cnt;
                                }

                                if (ReqMethod == HTTP_METHOD.PUT)
                                {
                                    //種類PUT
                                    var hinmeiDto = new ExternalCommon.DTO.Logicompass.INFO_GOODS_KINDS();
                                    hinmeiDto.Company_Id = sysInfo.DIGI_CORP_ID;
                                    hinmeiDto.Goods_Kind_Id = dgvRow.Cells["SHURUI_DIGI_SHURUI_CD"].Value.ToString();
                                    hinmeiDto.Goods_Kind_Name = dgvRow.Cells["SHURUI_NAME"].Value.ToString();

                                    logic.HttpPut(shuruiConnect.URL, shuruiConnect.CONTENT_TYPE, hinmeiDto);
                                }
                                else
                                {
                                    //種類DELETE
                                    apiURL = string.Format(shuruiConnect.URL + LogiConst.API_PARAMETER_URL_GOODS_KINDS, sysInfo.DIGI_CORP_ID, dgvRow.Cells["SHURUI_DIGI_SHURUI_CD"].Value);
                                    logic.HttpDelete(apiURL, shuruiConnect.CONTENT_TYPE);
                                }
                                break;
                            //社員マスタ
                            case RENKEI_SHAIN:
                                var shainConnect = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_DRIVERS);
                                if (shainConnect == null)
                                {
                                    this.errmessage.MessageBoxShow("S_LOGI_CONNECTのDrivers情報なし！！");
                                    ret_cnt = -1;
                                    return ret_cnt;
                                }

                                if (ReqMethod == HTTP_METHOD.PUT)
                                {
                                    //社員PUT
                                    var shainDto = new ExternalCommon.DTO.Logicompass.INFO_DRIVERS();
                                    shainDto.Company_Id = sysInfo.DIGI_CORP_ID;
                                    shainDto.Driver_Id = dgvRow.Cells["SHAIN_DIGI_SHAIN_CD"].Value.ToString();
                                    shainDto.Password = dgvRow.Cells["SHAIN_PASSWORD"].Value.ToString();
                                    shainDto.Driver_Name = dgvRow.Cells["SHAIN_NAME"].Value.ToString();
                                    shainDto.Driver_Kana_Name = dgvRow.Cells["SHAIN_FURIGANA"].Value.ToString();
                                    shainDto.Tel_No = dgvRow.Cells["SHAIN_TEL_NO"].Value.ToString();
                                    shainDto.License_Renewal_Date = dgvRow.Cells["SHAIN_LICENSE_RENEWAL_DATE"].Value.ToString();
                                    shainDto.Remarks = dgvRow.Cells["SHAIN_BIKOU"].Value.ToString();

                                    logic.HttpPut(shainConnect.URL, shainConnect.CONTENT_TYPE, shainDto);
                                }
                                else
                                {
                                    //社員DELETE
                                    apiURL = string.Format(shainConnect.URL + LogiConst.API_PARAMETER_URL_DRIVERS, sysInfo.DIGI_CORP_ID, dgvRow.Cells["SHAIN_DIGI_SHAIN_CD"].Value);
                                    logic.HttpDelete(apiURL, shainConnect.CONTENT_TYPE);
                                }
                                break;
                        }

                        if (ReqMethod == HTTP_METHOD.PUT)
                        {
                            this.form.renkei_cd = dgvRow.Cells[this.chkColumn].Value.ToString();
                        }
                        else
                        {
                            this.form.renkei_cd = string.Empty;
                        }

                        //タイムスタンプテーブルに出力データを保存する
                        ret_cnt = this.TimeStampTableRegist(dgvRow);
                        if (ret_cnt < 0)
                        {
                            return ret_cnt;
                        }

                        //完了
                        ret_cnt++;
                    }
                }
            }
            catch (WebException)
            {
                //LogiLogic.cs側でエラー表示しているので何もしない
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                //WebException以外はアラート表示
                this.errmessage.MessageBoxShow("E245");
                LogUtility.Error("JsonConnection", ex);
                ret_cnt = -1;
            }

            LogUtility.DebugMethodEnd();

            return ret_cnt;

        }
        #endregion

        #region タイムスタンプテーブルにデータを保存
        /// <summary>
        /// タイムスタンプテーブルに登録
        /// </summary>
        /// <param name="dgvRow"></param>
        /// <returns></returns>
        [Transaction]
        private int TimeStampTableRegist(DataGridViewRow dgvRow)
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                using (var tran = new TransactionUtility())
                {
                    string sql = "";
                    int dbCount = 0;
                    switch (this.renkeimaster_flg)
                    {
                        case RENKEI_SHASHU:
                            dbCount = this.daoShashuTS.GetDataByCd(dgvRow.Cells["SHASHU_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_DIGI_OUTPUT_SHASHU VALUES ('{0}', {1}, {2}, '{3}')", dgvRow.Cells["SHASHU_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoShashuTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_DIGI_OUTPUT_SHASHU SET OUTPUT_DATE = {1}, JYOGAI_FLG = {2}, DIGI_SHASHU_CD = '{3}' WHERE SHASHU_CD = '{0}'", dgvRow.Cells["SHASHU_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoShashuTS.ExecuteForStringSql(sql);
                            }
                            break;
                        case RENKEI_SHARYOU:
                            dbCount = this.daoSharyouTS.GetDataByCd(dgvRow.Cells["SHARYOU_UPN_GYOUSHA_CD"].Value.ToString(), dgvRow.Cells["SHARYOU_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_DIGI_OUTPUT_SHARYOU VALUES ('{0}', '{1}', {2}, {3}, '{4}')", dgvRow.Cells["SHARYOU_UPN_GYOUSHA_CD"].Value, dgvRow.Cells["SHARYOU_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoSharyouTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_DIGI_OUTPUT_SHARYOU SET OUTPUT_DATE = {2}, JYOGAI_FLG = {3}, DIGI_SHARYOU_CD = '{4}' WHERE  GYOUSHA_CD = '{0}' AND SHARYOU_CD = '{1}'", dgvRow.Cells["SHARYOU_UPN_GYOUSHA_CD"].Value, dgvRow.Cells["SHARYOU_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoSharyouTS.ExecuteForStringSql(sql);
                            }
                            break;
                        case RENKEI_HINMEI:
                            dbCount = this.daoHinmeiTS.GetDataByCd(dgvRow.Cells["HINMEI_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_DIGI_OUTPUT_HINMEI VALUES ('{0}', {1}, {2}, '{3}')", dgvRow.Cells["HINMEI_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoHinmeiTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_DIGI_OUTPUT_HINMEI SET OUTPUT_DATE = {1}, JYOGAI_FLG = {2}, DIGI_HINMEI_CD = '{3}' WHERE HINMEI_CD = '{0}'", dgvRow.Cells["HINMEI_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoHinmeiTS.ExecuteForStringSql(sql);
                            }
                            break;
                        case RENKEI_UNIT:
                            dbCount = this.daoUnitTS.GetDataByCd(dgvRow.Cells["UNIT_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_DIGI_OUTPUT_UNIT VALUES ('{0}', {1}, {2}, '{3}')", dgvRow.Cells["UNIT_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoUnitTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_DIGI_OUTPUT_UNIT SET OUTPUT_DATE = {1}, JYOGAI_FLG = {2}, DIGI_UNIT_CD = '{3}' WHERE UNIT_CD = '{0}'", dgvRow.Cells["UNIT_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoUnitTS.ExecuteForStringSql(sql);
                            }
                            break;
                        case RENKEI_SHURUI:
                            dbCount = this.daoShuruiTS.GetDataByCd(dgvRow.Cells["SHURUI_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_DIGI_OUTPUT_SHURUI VALUES ('{0}', {1}, {2}, '{3}')", dgvRow.Cells["SHURUI_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoShuruiTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_DIGI_OUTPUT_SHURUI SET OUTPUT_DATE = {1}, JYOGAI_FLG = {2}, DIGI_SHURUI_CD = '{3}' WHERE SHURUI_CD = '{0}'", dgvRow.Cells["SHURUI_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoShuruiTS.ExecuteForStringSql(sql);
                            }
                            break;
                        case RENKEI_SHAIN:
                            dbCount = this.daoShainTS.GetDataByCd(dgvRow.Cells["SHAIN_CD"].Value.ToString());
                            if (dbCount == 0)
                            {
                                sql = string.Format("INSERT INTO M_DIGI_OUTPUT_SHAIN VALUES ('{0}', {1}, {2}, '{3}')", dgvRow.Cells["SHAIN_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoShainTS.ExecuteForStringSql(sql);
                            }
                            else
                            {
                                sql = string.Format("UPDATE M_DIGI_OUTPUT_SHAIN SET OUTPUT_DATE = {1}, JYOGAI_FLG = {2}, DIGI_SHAIN_CD = '{3}' WHERE SHAIN_CD = '{0}'", dgvRow.Cells["SHAIN_CD"].Value, this.form.output_date, this.form.exclusionFlg, this.form.renkei_cd);
                                this.daoShainTS.ExecuteForStringSql(sql);
                            }
                            break;
                    }
                    tran.Commit();
                    ret_cnt = 1;
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex1)
            {
                //排他は警告
                LogUtility.Warn(ex1); //排他は警告
                this.errmessage.MessageBoxShow("E080");
                ret_cnt = -1;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error(ex2); //その他SQLエラー
                this.errmessage.MessageBoxShow("E093");
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex); //その他エラー
                this.errmessage.MessageBoxShow("E245");
                ret_cnt = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret_cnt;
        }
        #endregion

        #region 必須チェック
        /// <summary>
        /// 必須項目チェック
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
                for (int i = 0; i < this.meisaiForm.RowCount; i++)
                {
                    if (this.meisaiForm.Rows[i].IsNewRow)
                    {
                        continue;
                    }
                    if ((null == this.meisaiForm.Rows[i].Cells[chkColumn].Value ||
                        "".Equals(this.meisaiForm.Rows[i].Cells[chkColumn].Value.ToString().Trim())) &&
                        true.Equals(this.meisaiForm.Rows[i].Cells[0].Value))
                    {
                        if (false == isErr)
                        {
                            errColName.Add(chkColumnName);
                            isErr = true;
                        }
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.meisaiForm.Rows[i].Cells[chkColumn], true);
                    }
                }

                this.meisaiForm.Refresh();
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
                    this.errmessage.MessageBoxShow("E001", errMsg);
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckBeforeUpdate", ex);
                this.errmessage.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 種類チェック(品名更新時のみ)
        /// <summary>
        /// 種類チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean CheckShurui()
        {
            LogUtility.DebugMethodStart();

            //品名以外は引っかけたくないのでtrueで抜ける
            if (this.renkeimaster_flg != RENKEI_HINMEI)
            {
                return true;
            }

            try
            {
                Boolean isErr;

                // 種類未入力チェック
                isErr = false;
                for (int i = 0; i < this.meisaiForm.RowCount; i++)
                {
                    if ((null == this.meisaiForm.Rows[i].Cells["HINMEI_DIGI_SHURUI_CD"].Value ||
                        "".Equals(this.meisaiForm.Rows[i].Cells["HINMEI_DIGI_SHURUI_CD"].Value.ToString().Trim())) &&
                        true.Equals(this.meisaiForm.Rows[i].Cells[0].Value))
                    {
                        if (!isErr)
                        {
                            isErr = true;
                        }
                    }
                }

                if (isErr)
                {
                    if (this.errmessage.MessageBoxShowConfirm("デジタコ品名分類IDが未入力の品名が含まれています。\r\nデジタコ側システムへマスタ連携してよろしいですか？") != DialogResult.Yes)
                    {
                        return false;
                    }
                    LogUtility.DebugMethodEnd(true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShurui", ex);
                this.errmessage.MessageBoxShow("E245");
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

                // 画面で種類CD重複チェック
                // 未登録データで、同じデジタコ連携CDを手入力したものを弾く
                int recCount = 0;
                for (int i = 0; i < this.meisaiForm.Rows.Count; i++)
                {
                    string strCD = string.Empty;
                    if (this.meisaiForm.Rows[i].Cells[this.chkColumn].Value != null)
                    {
                        strCD = this.meisaiForm.Rows[i].Cells[this.chkColumn].Value.ToString();
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

                // DBで種類CD重複チェック
                // 登録済みデータで、PK違いでデジタコ連携CDが存在したら弾く
                int dbCount = 0;
                switch (this.renkeimaster_flg)
                {
                    case RENKEI_SHASHU:
                        dbCount = this.daoShashuTS.GetDuplicationCountByCd(ChkCDPK1, ChkCD);
                        break;
                    case RENKEI_SHARYOU:
                        dbCount = this.daoSharyouTS.GetDuplicationCountByCd(ChkCDPK1, ChkCDPK2, ChkCD);
                        break;
                    case RENKEI_HINMEI:
                        dbCount = this.daoHinmeiTS.GetDuplicationCountByCd(ChkCDPK1, ChkCD);
                        break;
                    case RENKEI_UNIT:
                        dbCount = this.daoUnitTS.GetDuplicationCountByCd(ChkCDPK1, ChkCD);
                        break;
                    case RENKEI_SHURUI:
                        dbCount = this.daoShuruiTS.GetDuplicationCountByCd(ChkCDPK1, ChkCD);
                        break;
                    case RENKEI_SHAIN:
                        dbCount = this.daoShainTS.GetDuplicationCountByCd(ChkCDPK1, ChkCD);
                        break;
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
                this.errmessage.MessageBoxShow("E093");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.errmessage.MessageBoxShow("E245");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(false, catchErr);
            return false;
        }
        #endregion

        #region デジタコ連携項目入力制限
        private void LimitationsColumn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 未連携のみ入力可
                // 連携済み、再連携候補は入力不可
                if (renkeikouho_flg != RENKEI_NOT || kensaku_Flg == RENKEI_JOGAI)
                {
                    for (int i = 0; i < this.meisaiForm.Rows.Count; i++)
                    {
                        this.meisaiForm.Rows[i].Cells[this.chkColumn].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.errmessage.MessageBoxShow("E245");
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 明細ヘッダーにチェックボックスを追加
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        private void HeaderCheckBoxSupport()
        {

            LogUtility.DebugMethodStart();

            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        this.meisaiForm = this.form.Ichiran1;
                        this.chkRenkei = "SHASHU_RENKEI";
                        break;
                    case 1:
                        this.meisaiForm = this.form.Ichiran2;
                        this.chkRenkei = "SHARYOU_RENKEI";

                        break;
                    case 2:
                        this.meisaiForm = this.form.Ichiran3;
                        this.chkRenkei = "HINMEI_RENKEI";
                        break;
                    case 3:
                        this.meisaiForm = this.form.Ichiran4;
                        this.chkRenkei = "UNIT_RENKEI";
                        break;
                    case 4:
                        this.meisaiForm = this.form.Ichiran5;
                        this.chkRenkei = "SHURUI_RENKEI";
                        break;
                    case 5:
                        this.meisaiForm = this.form.Ichiran6;
                        this.chkRenkei = "SHAIN_RENKEI";
                        break;
                }

                if (!this.meisaiForm.Columns.Contains(this.chkRenkei))
                {
                    {
                        DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                        newColumn.Name = this.chkRenkei;
                        newColumn.HeaderText = "連携";
                        newColumn.Width = 70;
                        DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                        newheader.Value = "連携   ";
                        newColumn.HeaderCell = newheader;
                        newColumn.Resizable = DataGridViewTriState.False;

                        if (this.meisaiForm.Columns.Count > 0)
                        {
                            this.meisaiForm.Columns.Insert(0, newColumn);
                        }
                        else
                        {
                            this.meisaiForm.Columns.Add(newColumn);
                        }
                        this.meisaiForm.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
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
            string clmname = string.Empty;

            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        dgv = this.form.Ichiran1;
                        clmname = "SHASHU_RENKEI";
                        break;
                    case 1:
                        dgv = this.form.Ichiran2;
                        clmname = "SHARYOU_RENKEI";
                        break;
                    case 2:
                        dgv = this.form.Ichiran3;
                        clmname = "HINMEI_RENKEI";
                        break;
                    case 3:
                        dgv = this.form.Ichiran4;
                        clmname = "UNIT_RENKEI";
                        break;
                    case 4:
                        dgv = this.form.Ichiran5;
                        clmname = "SHURUI_RENKEI";
                        break;
                    case 5:
                        dgv = this.form.Ichiran6;
                        clmname = "SHAIN_RENKEI";
                        break;
                }
                if (dgv.Columns.Contains(clmname))
                {
                    DataGridViewCheckBoxHeaderCell header = dgv.Columns[clmname].HeaderCell as DataGridViewCheckBoxHeaderCell;
                    if (header != null)
                    {
                        header._checked = false;
                    }
                }
            }
        }
        #endregion

        #region 未使用
        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 論理削除
        /// <summary>
        /// 論理削除
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 物理削除
        /// <summary>
        /// 物理削除
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
