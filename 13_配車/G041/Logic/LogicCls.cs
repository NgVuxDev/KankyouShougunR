// $Id: LogicCls.cs 43707 2015-03-04 06:51:15Z nagata $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.ContenaShitei.DAO;
using Shougun.Core.Common.ContenaShitei.DTO;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.Allocation.ContenaIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {

        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.ContenaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 設置コンテナ一覧画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeader headForm;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// システム情報Dao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報エンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 設置コンテナ一覧Dao
        /// </summary>
        private DAOCls dao;

        /// <summary>
        /// 設置コンテナ一覧Dto
        /// </summary>
        private DTOCls dto;

        /// <summary>コンテナ種類用Dao</summary>
        private IM_CONTENA_SHURUIDao contenaShuruiDao;

        /// <summary>コンテナ数量管理時用Dao</summary>
        private SuuryouKanriDAOClass suuryouKanriDao;

        /// <summary>
        /// コンテナマスタDao
        /// </summary>
        private IM_CONTENADao contenaDao;

        /// <summary>
        /// 変更前業者CD
        /// </summary>
        internal string beforeGyoushaCd;

        /// <summary>
        /// 変更前現場CD
        /// </summary>
        internal string beforeGenbaCd;

        /// <summary>
        /// 変更前コンテナ種類CD
        /// </summary>
        internal string beforeContenaShuruiCd;

        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 取得した現場エンティティを保持する
        /// </summary>
        private List<M_GENBA> genbaList = new List<M_GENBA>();

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;
        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        #endregion

        #region プロパティ
        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOCls SearchString { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// アラート件数
        /// </summary>
        public int AlertCount { get; set; }

        private List<string> listColumnn = new List<string>() { "SecchiChouhuku", "CONTENA_SHURUI_CD", "CONTENA_SHURUI_NAME_RYAKU", "SECCHI_DATE", "CONTENA_CD", "CONTENA_NAME_RYAKU", "GYOUSHA_CD", "GYOUSHA_NAME_RYAKU", "GENBA_CD", "GENBA_NAME_RYAKU", "DAISUU", "EIGYOU_TANTOU_CD", "SHAIN_NAME_RYAKU", "DAYSCOUNT", "GRAPH" };

        #endregion

        #region 初期化処理

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOCls();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.dao = DaoInitUtility.GetComponent<DAOCls>();
            this.contenaDao = DaoInitUtility.GetComponent<IM_CONTENADao>();
            this.suuryouKanriDao = DaoInitUtility.GetComponent<SuuryouKanriDAOClass>();
            this.contenaShuruiDao = DaoInitUtility.GetComponent<IM_CONTENA_SHURUIDao>();
            // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            // メッセージ表示オブジェクト
            msgLogic = new MessageBoxShowLogic();
            // 現場Dao
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                // システム情報を取得し、初期値をセットする
                this.GetSysInfoInit();

                // コンテナ管理方法によってデザインを変更する
                this.ChangeLabelAndLayout();

                // 画面情報を設定する
                this.setLoadPage();

                // オプション非対応
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    // mapbox用ボタン無効化
                    parentForm.bt_process1.Text = string.Empty;
                    parentForm.bt_process1.Enabled = false;
                }

                this.form.Ichiran.AutoGenerateColumns = false;
                this.form.Ichiran.DataSource = CreateDataTable();
                this.form.IchiranForCSVOutput.AutoGenerateColumns = false;
                this.parentForm.txb_process.Enabled = false;

                this.headForm.ReadDataNumber.Text = "0";
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);

            //取消ボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(bt_func10_Click);

            //フィルタ(F11)イベント生成
            parentForm.bt_func11.Click += new System.EventHandler(bt_func11_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            //地図表示ボタンイベント生成
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);
        }
        #endregion

        #region システム情報を取得し、初期値をセットする
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }
        #endregion

        #region on コンテナ管理方法によるラベルとレイアウトの変更
        /// <summary>
        /// コンテナ管理方法によりラベルとレイアウトを変更する
        /// </summary>
        private void ChangeLabelAndLayout()
        {
            int kontenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;

            if (kontenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                // 個体管理

                // ラベル名変更
                this.form.label4.Text = "経過日数";

                // レイアウト調整
                this.form.label3.Visible = true;
                this.form.CONTENA_CD_HEADER.Visible = true;
                this.form.CONTENA_NAME_RYAKU_HEADER.Visible = true;
                this.form.customPanel1.Visible = true;

                // ついでに初期化
                this.form.ChouhukuSecchiNomi.Text = "2";

                // コンテナ種類
                //this.form.label2.Size = new Size(100, 20);
                //int contenaShuruiCdLeft = this.form.label2.Left + this.form.label2.Width + 5;
                //this.form.CONTENA_SHURUI_CD_HEADER.Location = new Point(contenaShuruiCdLeft, 33);
                //this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Size = new Size(80, 20);
                //int contenaShuruiNameLeft = this.form.CONTENA_SHURUI_CD_HEADER.Left + this.form.CONTENA_SHURUI_CD_HEADER.Width - 1;
                //this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Location = new Point(contenaShuruiNameLeft, 33);

                // コンテナ名
                int label3Left = this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Left + this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Width + 6;
                this.form.label3.Location = new Point(label3Left, 33);
                this.form.label3.Size = new Size(80, 20);
                int contenaCdLeft = this.form.label3.Left + this.form.label3.Width + 5;
                this.form.CONTENA_CD_HEADER.Location = new Point(contenaCdLeft, 33);
                int contenaNameLeft = this.form.CONTENA_CD_HEADER.Left + this.form.CONTENA_CD_HEADER.Width - 1;
                this.form.CONTENA_NAME_RYAKU_HEADER.Location = new Point(contenaNameLeft, 33);

                // 経過日数
                //int label4Left = this.form.CONTENA_NAME_RYAKU_HEADER.Left + this.form.CONTENA_NAME_RYAKU_HEADER.Width + 6;
                //this.form.label4.Location = new Point(label4Left, 33);
                //this.form.label4.Size = new Size(80, 20);
                //int elapsedLeft = this.form.label4.Left + this.form.label4.Width + 5;
                //this.form.ELAPSED_DAYS.Location = new Point(elapsedLeft, 33);
                //this.form.ELAPSED_DAYS.Size = new Size(30, 19);
                //int label5Left = this.form.ELAPSED_DAYS.Left + this.form.ELAPSED_DAYS.Width + 2;
                //this.form.label5.Location = new Point(label5Left, 38);

                // 重複設置検索
                int panel1Left = this.form.label5.Left + this.form.label5.Width + 5;
                this.form.customPanel1.Location = new Point(panel1Left, 55);

                // 明細行の調整
                this.form.Ichiran.Columns[COLUMN_NAME_CONTENA_NAME_RYAKU].Visible = true;
                this.form.Ichiran.Columns[COLUMN_NAME_DAISUU].Visible = false;
                this.form.Ichiran.Columns[COLUMN_NAME_SECCHICHOUUHUKU].Visible = true;
                // 初期表示時に表示内容を全てみせたいため、コンテナ種類名を非表示にする
                this.form.Ichiran.Columns[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU].Visible = false;

                this.form.Ichiran.Columns[COLUMN_NAME_SECCHI_DATE].HeaderText = "設置日";
                this.form.Ichiran.Columns[COLUMN_NAME_SECCHI_DATE].Width = 80;
                this.form.Ichiran.Columns[COLUMN_NAME_DAYSCOUNT].HeaderText = "経過日数";
                this.form.Ichiran.Columns[COLUMN_NAME_DAYSCOUNT].Width = 70;

                // ラジオボタンの値を変更してしまうと、そこにフォーカスされてしまうので最後に初期フォーカスを設定
                this.form.GYOUSHA_CD_HEADER.Focus();
            }
            else
            {
                // 数量管理
                // デザイナのほうは数量管理用のデザインになっているためここでは何もしない
            }
        }
        #endregion

        #region 初期画面設定
        /// <summary>
        /// 初期画面設定
        /// </summary>
        private void setLoadPage()
        {
            // アラート件数設定
            this.headForm.alertNumber.Text = this.sysInfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
            // グラフ（ｎ日迄）
            this.form.Ichiran.Columns["graph"].HeaderText = "グラフ（" + this.sysInfoEntity.CONTENA_MAX_SET_KEIKA_DATE + "日まで）";
            // フォーカス設定
            this.form.GYOUSHA_CD_HEADER.Focus();
        }
        #endregion

        #endregion

        #region 業務処理

        #region CSV出力
        /// <summary>
        /// 「F6 CSV出力ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
                if (this.form.Ichiran.Rows.Count == 0)
                {
                    msgLogic.MessageBoxShow("E044");
                    // フォーカス設定
                    this.form.GYOUSHA_CD_HEADER.Focus();
                    return;
                }
                // 出力先指定のポップアップを表示させる。
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    // =========================================================================================
                    // IchiranForCSVOutputを使用していたが、固体、数量で項目が変化する＆項目表示順序も異なるため
                    // 独自にCustomDataGridViewを作成して出力する。
                    // IchiranForCSVOutputは念のため残してある
                    // =========================================================================================
                    CustomDataGridView dgv = this.CreateCSVCustomDataGridView();
                    dgv.Visible = false;
                    this.form.Controls.Add(dgv);
                    dgv.Refresh();

                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(dgv, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_CONTENA_ICHIRAN), this.form);

                    this.form.Controls.Remove(dgv);
                    dgv.Dispose();
                }
                // フォーカス設定
                this.form.GYOUSHA_CD_HEADER.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// CSV出力用のCustomDataGridViewを作成します
        /// </summary>
        /// <returns>CSV出力用のCostomDataGridView</returns>
        private CustomDataGridView CreateCSVCustomDataGridView()
        {
            CustomDataGridView dgv = new CustomDataGridView();

            // 数量管理、固体管理で出力する項目が違う
            int kontenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;
            if (kontenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                /* 固体管理 */

                #region DataGrieViewの列定義

                // 重複設置
                DgvCustomTextBoxColumn chouhukuSecchiCell = new DgvCustomTextBoxColumn();
                chouhukuSecchiCell.Name = "CSV_SecchiChouhuku";
                chouhukuSecchiCell.HeaderText = "重複設置";
                dgv.Columns.Add(chouhukuSecchiCell);
                // 設置日
                DgvCustomTextBoxColumn secchiDateCell = new DgvCustomTextBoxColumn();
                secchiDateCell.Name = "CSV_SECCHI_DATE";
                secchiDateCell.HeaderText = "設置日";
                dgv.Columns.Add(secchiDateCell);
                // コンテナ種類CD
                DgvCustomTextBoxColumn contenaShuruiCdCell = new DgvCustomTextBoxColumn();
                contenaShuruiCdCell.Name = "CSV_CONTENA_SHURUI_CD";
                contenaShuruiCdCell.HeaderText = "コンテナ種類CD";
                dgv.Columns.Add(contenaShuruiCdCell);
                // コンテナ種類名
                DgvCustomTextBoxColumn contenaShuruiNameRyakuCell = new DgvCustomTextBoxColumn();
                contenaShuruiNameRyakuCell.Name = "CSV_CONTENA_SHURUI_NAME_RYAKU";
                contenaShuruiNameRyakuCell.HeaderText = "コンテナ種類名";
                dgv.Columns.Add(contenaShuruiNameRyakuCell);
                // コンテナCD
                DgvCustomTextBoxColumn contenaCdCell = new DgvCustomTextBoxColumn();
                contenaCdCell.Name = "CSV_CONTENA_CD";
                contenaCdCell.HeaderText = "コンテナCD";
                dgv.Columns.Add(contenaCdCell);
                // コンテナ名
                DgvCustomTextBoxColumn contenaNameRyakuCell = new DgvCustomTextBoxColumn();
                contenaNameRyakuCell.Name = "CSV_CONTENA_NAME_RYAKU";
                contenaNameRyakuCell.HeaderText = "コンテナ名";
                dgv.Columns.Add(contenaNameRyakuCell);
                // 業者CD
                DgvCustomTextBoxColumn gyoushaCdCell = new DgvCustomTextBoxColumn();
                gyoushaCdCell.Name = "CSV_GYOUSHA_CD";
                gyoushaCdCell.HeaderText = "業者CD";
                dgv.Columns.Add(gyoushaCdCell);
                // 業者名
                DgvCustomTextBoxColumn gyoushaNameRyakuCell = new DgvCustomTextBoxColumn();
                gyoushaNameRyakuCell.Name = "CSV_GYOUSHA_NAME_RYAKU";
                gyoushaNameRyakuCell.HeaderText = "業者名";
                dgv.Columns.Add(gyoushaNameRyakuCell);
                // 現場CD
                DgvCustomTextBoxColumn genbaCdCell = new DgvCustomTextBoxColumn();
                genbaCdCell.Name = "CSV_GENBA_CD";
                genbaCdCell.HeaderText = "現場CD";
                dgv.Columns.Add(genbaCdCell);
                // 現場名
                DgvCustomTextBoxColumn genbaNameRyakuCell = new DgvCustomTextBoxColumn();
                genbaNameRyakuCell.Name = "CSV_GENBA_NAME_RYAKU";
                genbaNameRyakuCell.HeaderText = "現場名";
                dgv.Columns.Add(genbaNameRyakuCell);
                // 営業担当者CD
                DgvCustomTextBoxColumn eigyouTantouCdCell = new DgvCustomTextBoxColumn();
                eigyouTantouCdCell.Name = "CSV_EIGYOU_TANTOU_CD";
                eigyouTantouCdCell.HeaderText = "営業担当者CD";
                dgv.Columns.Add(eigyouTantouCdCell);
                // 営業担当者名
                DgvCustomTextBoxColumn shainNameRyakuCell = new DgvCustomTextBoxColumn();
                shainNameRyakuCell.Name = "CSV_SHAIN_NAME_RYAKU";
                shainNameRyakuCell.HeaderText = "営業担当者名";
                dgv.Columns.Add(shainNameRyakuCell);
                // 経過日数
                DgvCustomTextBoxColumn daysCountCell = new DgvCustomTextBoxColumn();
                daysCountCell.Name = "CSV_DAYSCOUNT";
                daysCountCell.HeaderText = "経過日数";
                dgv.Columns.Add(daysCountCell);

                #endregion

                #region データ設定

                int i = 0;
                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    dgv.Rows.Add();
                    // 重複設置
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_SECCHICHOUUHUKU].Value = row.Cells[COLUMN_NAME_SECCHICHOUUHUKU].Value;
                    // 設置日
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_SECCHI_DATE].Value = row.Cells[COLUMN_NAME_SECCHI_DATE].Value;
                    // コンテナ種類CD
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_CONTENA_SHURUI_CD].Value = row.Cells[COLUMN_NAME_CONTENA_SHURUI_CD].Value;
                    // コンテナ種類名
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU].Value = row.Cells[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU].Value;
                    // コンテナCD
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_CONTENA_CD].Value = row.Cells[COLUMN_NAME_CONTENA_CD].Value;
                    // コンテナ名
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_CONTENA_NAME_RYAKU].Value = row.Cells[COLUMN_NAME_CONTENA_NAME_RYAKU].Value;
                    // 業者CD
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_GYOUSHA_CD].Value = row.Cells[COLUMN_NAME_GYOUSHA_CD].Value;
                    // 業者名
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_GYOUSHA_NAME_RYAKU].Value = row.Cells[COLUMN_NAME_GYOUSHA_NAME_RYAKU].Value;
                    // 現場CD
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_GENBA_CD].Value = row.Cells[COLUMN_NAME_GENBA_CD].Value;
                    // 現場名
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_GENBA_NAME_RYAKU].Value = row.Cells[COLUMN_NAME_GENBA_NAME_RYAKU].Value;
                    // 営業担当者CD
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_EIGYOU_TANTOU_CD].Value = row.Cells[COLUMN_NAME_EIGYOU_TANTOU_CD].Value;
                    // 営業担当者名
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_SHAIN_NAME_RYAKU].Value = row.Cells[COLUMN_NAME_SHAIN_NAME_RYAKU].Value;
                    // 経過日数
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_DAYSCOUNT].Value = row.Cells[COLUMN_NAME_DAYSCOUNT].Value;

                    i++;
                }

                #endregion
            }
            else
            {
                /* 数量管理 */
                #region DataGrieViewの列定義

                // コンテナ種類CD
                DgvCustomTextBoxColumn contenaShuruiCdCell = new DgvCustomTextBoxColumn();
                contenaShuruiCdCell.Name = "CSV_CONTENA_SHURUI_CD";
                contenaShuruiCdCell.HeaderText = "コンテナ種類CD";
                dgv.Columns.Add(contenaShuruiCdCell);
                // コンテナ種類名
                DgvCustomTextBoxColumn contenaShuruiNameRyakuCell = new DgvCustomTextBoxColumn();
                contenaShuruiNameRyakuCell.Name = "CSV_CONTENA_SHURUI_NAME_RYAKU";
                contenaShuruiNameRyakuCell.HeaderText = "コンテナ種類名";
                dgv.Columns.Add(contenaShuruiNameRyakuCell);
                // 最終更新日
                DgvCustomTextBoxColumn secchiDateCell = new DgvCustomTextBoxColumn();
                secchiDateCell.Name = "CSV_SECCHI_DATE";
                secchiDateCell.HeaderText = "最終更新日";
                dgv.Columns.Add(secchiDateCell);
                // 業者CD
                DgvCustomTextBoxColumn gyoushaCdCell = new DgvCustomTextBoxColumn();
                gyoushaCdCell.Name = "CSV_GYOUSHA_CD";
                gyoushaCdCell.HeaderText = "業者CD";
                dgv.Columns.Add(gyoushaCdCell);
                // 業者名
                DgvCustomTextBoxColumn gyoushaNameRyakuCell = new DgvCustomTextBoxColumn();
                gyoushaNameRyakuCell.Name = "CSV_GYOUSHA_NAME_RYAKU";
                gyoushaNameRyakuCell.HeaderText = "業者名";
                dgv.Columns.Add(gyoushaNameRyakuCell);
                // 現場CD
                DgvCustomTextBoxColumn genbaCdCell = new DgvCustomTextBoxColumn();
                genbaCdCell.Name = "CSV_GENBA_CD";
                genbaCdCell.HeaderText = "現場CD";
                dgv.Columns.Add(genbaCdCell);
                // 現場名
                DgvCustomTextBoxColumn genbaNameRyakuCell = new DgvCustomTextBoxColumn();
                genbaNameRyakuCell.Name = "CSV_GENBA_NAME_RYAKU";
                genbaNameRyakuCell.HeaderText = "現場名";
                dgv.Columns.Add(genbaNameRyakuCell);
                // 台数
                DgvCustomTextBoxColumn daisuuCell = new DgvCustomTextBoxColumn();
                daisuuCell.Name = "CSV_DAISUU";
                daisuuCell.HeaderText = "台数";
                dgv.Columns.Add(daisuuCell);
                // 営業担当者CD
                DgvCustomTextBoxColumn eigyouTantouCdCell = new DgvCustomTextBoxColumn();
                eigyouTantouCdCell.Name = "CSV_EIGYOU_TANTOU_CD";
                eigyouTantouCdCell.HeaderText = "営業担当者CD";
                dgv.Columns.Add(eigyouTantouCdCell);
                // 営業担当者名
                DgvCustomTextBoxColumn shainNameRyakuCell = new DgvCustomTextBoxColumn();
                shainNameRyakuCell.Name = "CSV_SHAIN_NAME_RYAKU";
                shainNameRyakuCell.HeaderText = "営業担当者名";
                dgv.Columns.Add(shainNameRyakuCell);
                // 無回転日数
                DgvCustomTextBoxColumn daysCountCell = new DgvCustomTextBoxColumn();
                daysCountCell.Name = "CSV_DAYSCOUNT";
                daysCountCell.HeaderText = "無回転日数";
                dgv.Columns.Add(daysCountCell);

                #endregion

                #region データ設定

                int i = 0;
                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    dgv.Rows.Add();
                    // コンテナ種類CD
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_CONTENA_SHURUI_CD].Value = row.Cells[COLUMN_NAME_CONTENA_SHURUI_CD].Value;
                    // コンテナ種類名
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU].Value = row.Cells[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU].Value;
                    // 最終更新日
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_SECCHI_DATE].Value = row.Cells[COLUMN_NAME_SECCHI_DATE].Value;
                    // 業者CD
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_GYOUSHA_CD].Value = row.Cells[COLUMN_NAME_GYOUSHA_CD].Value;
                    // 業者名
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_GYOUSHA_NAME_RYAKU].Value = row.Cells[COLUMN_NAME_GYOUSHA_NAME_RYAKU].Value;
                    // 現場CD
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_GENBA_CD].Value = row.Cells[COLUMN_NAME_GENBA_CD].Value;
                    // 現場名
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_GENBA_NAME_RYAKU].Value = row.Cells[COLUMN_NAME_GENBA_NAME_RYAKU].Value;
                    // 台数
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_DAISUU].Value = row.Cells[COLUMN_NAME_DAISUU].Value;
                    // 営業担当者CD
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_EIGYOU_TANTOU_CD].Value = row.Cells[COLUMN_NAME_EIGYOU_TANTOU_CD].Value;
                    // 営業担当者名
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_SHAIN_NAME_RYAKU].Value = row.Cells[COLUMN_NAME_SHAIN_NAME_RYAKU].Value;
                    // 無回転日数
                    dgv.Rows[i].Cells["CSV_" + COLUMN_NAME_DAYSCOUNT].Value = row.Cells[COLUMN_NAME_DAYSCOUNT].Value;

                    i++;
                }

                #endregion
            }

            return dgv;
        }
        #endregion

        #region 検索条件初期化
        /// <summary>
        /// 「F7 条件クリアボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 検索条件クリア
                // 業者
                this.form.GYOUSHA_CD_HEADER.Text = "";
                this.form.GYOUSHA_NAME_RYAKU_HEADER.Text = "";
                // 現場
                this.form.GENBA_CD_HEADER.Text = "";
                this.form.GENBA_NAME_RYAKU_HEADER.Text = "";
                // 営業担当者
                this.form.EIGYOU_TANTOU_CD_HEADER.Text = "";
                this.form.SHAIN_NAME_RYAKU_HEADER.Text = "";
                // コンテナ種類
                this.form.CONTENA_SHURUI_CD_HEADER.Text = "";
                this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Text = "";
                // コンテナ
                this.form.CONTENA_CD_HEADER.Text = "";
                this.form.CONTENA_NAME_RYAKU_HEADER.Text = "";
                // 経過日数
                this.form.ELAPSED_DAYS.Text = "";

                // 重複設置絞込
                this.form.ChouhukuSecchiNomi.Text = "2";

                // フォーカス設定
                this.form.GYOUSHA_CD_HEADER.Focus();
                this.form.customSortHeader1.ClearCustomSortSetting();
                this.form.customSearchHeader1.ClearCustomSearchSetting();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func7_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region 検索

        #region 検索で使用する固定値

        // 検索結果のカラム名(数量管理)
        private string[] columnNames = { "CONTENA_SHURUI_CD", "CONTENA_SHURUI_NAME_RYAKU", "CONTENA_CD", "CONTENA_NAME_RYAKU", "GYOUSHA_CD", "GYOUSHA_NAME_RYAKU"
                                       , "GENBA_CD", "GENBA_NAME_RYAKU", "EIGYOU_TANTOU_CD", "SHAIN_NAME_RYAKU", "SECCHI_DATE", "DAYSCOUNT", "GRAPH", "DAISUU"};

        // 検索結果のカラム名(個体管理)
        private string[] columnNamesForKotaiknri = { "SecchiChouhuku", "CONTENA_SHURUI_CD", "CONTENA_SHURUI_NAME_RYAKU", "CONTENA_CD", "CONTENA_NAME_RYAKU", "GYOUSHA_CD", "GYOUSHA_NAME_RYAKU"
                                                    , "GENBA_CD", "GENBA_NAME_RYAKU", "EIGYOU_TANTOU_CD", "SHAIN_NAME_RYAKU", "SECCHI_DATE", "DAYSCOUNT", "GRAPH"};

        // 検索結果のタイプ名(検索結果のカラム名に対応)
        private string[] columnTyepes = { "System.String","System.String","System.String","System.String","System.String","System.String"
                                        ,"System.String","System.String","System.String","System.String","System.DateTime","System.Int16","System.Double","System.Int16"};

        private string[] columnTyepesForKotaiKanri = { "System.String","System.String","System.String","System.String","System.String","System.String","System.String"
                                        ,"System.String","System.String","System.String","System.String","System.DateTime","System.Int32","System.Double"};

        private string COLUMN_NAME_CONTENA_SHURUI_CD = "CONTENA_SHURUI_CD";
        private string COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU = "CONTENA_SHURUI_NAME_RYAKU";
        private string COLUMN_NAME_CONTENA_CD = "CONTENA_CD";
        private string COLUMN_NAME_CONTENA_NAME_RYAKU = "CONTENA_NAME_RYAKU";
        private string COLUMN_NAME_GYOUSHA_CD = "GYOUSHA_CD";
        private string COLUMN_NAME_GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
        private string COLUMN_NAME_GENBA_CD = "GENBA_CD";
        private string COLUMN_NAME_GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
        private string COLUMN_NAME_EIGYOU_TANTOU_CD = "EIGYOU_TANTOU_CD";
        private string COLUMN_NAME_SHAIN_NAME_RYAKU = "SHAIN_NAME_RYAKU";
        private string COLUMN_NAME_SECCHI_DATE = "SECCHI_DATE";
        private string COLUMN_NAME_DAYSCOUNT = "DAYSCOUNT";
        private string COLUMN_NAME_GRAPH = "GRAPH";
        private string COLUMN_NAME_DAISUU = "DAISUU";
        private string COLUMN_NAME_SECCHICHOUUHUKU = "SecchiChouhuku";

        // 重複設置カラムに表示する文字列
        private string CHOUHUKU_SECCHI_VALUE = "○";

        #endregion

        /// <summary>
        /// 「F8 検索ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 検索条件を設定する
                SetSearchString();

                // 検索結果を取得する
                int kontenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                    ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;

                if (kontenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU)
                {
                    if (this.SearchResult != null)
                    {
                        this.SearchResult.Clear();
                    }

                    DataTable displayData = new DataTable();
                    for (int i = 0; i < this.columnNames.Length; i++)
                    {
                        displayData.Columns.Add(columnNames[i].ToString(), System.Type.GetType(columnTyepes[i]));
                    }

                    var queryResult = this.suuryouKanriDao.GetIchiranDataSqlForSuuryouKanri(this.SearchString);
                    // 数量計算
                    // その現場にいくつコンテナが設定されていて、何日動きが無いかを計算
                    var groupList = queryResult.Select(s => new { s.CONTENA_SHURUI_CD, s.GYOUSHA_CD, s.GENBA_CD }).GroupBy(g => new { g.CONTENA_SHURUI_CD, g.GYOUSHA_CD, g.GENBA_CD });

                    foreach (var tempGroup in groupList)
                    {
                        var tempResultData = queryResult.Where(w => w.CONTENA_SHURUI_CD.Equals(tempGroup.Key.CONTENA_SHURUI_CD)
                                                                && w.GYOUSHA_CD.Equals(tempGroup.Key.GYOUSHA_CD)
                                                                && w.GENBA_CD.Equals(tempGroup.Key.GENBA_CD)
                                                                && w.DAYSCOUNT >= 0).OrderBy(o => o.SECCHI_DATE);

                        short daisuuCntTotal = 0;

                        foreach (var calcTarget in tempResultData)
                        {
                            if (calcTarget.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                            {
                                // 加算
                                daisuuCntTotal += (short)calcTarget.DAISUU_CNT;
                            }
                            else if (calcTarget.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                            {
                                // 減算
                                daisuuCntTotal -= calcTarget.DAISUU_CNT;
                            }
                        }

                        // 最後の要素を画面に表示する
                        var lastData = tempResultData.LastOrDefault();
                        // 引揚のほうが多い場合、現場にはコンテナは設定されていないはずなので画面には表示しない
                        if (daisuuCntTotal != 0 && lastData != null
                            && lastData.DAYSCOUNT >= this.SearchString.ELAPSED_DAYS)
                        {
                            DataRow row = displayData.NewRow();
                            row[COLUMN_NAME_CONTENA_SHURUI_CD] = lastData.CONTENA_SHURUI_CD;
                            row[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU] = lastData.CONTENA_SHURUI_NAME_RYAKU;
                            row[COLUMN_NAME_CONTENA_CD] = lastData.CONTENA_CD;
                            row[COLUMN_NAME_CONTENA_NAME_RYAKU] = lastData.CONTENA_NAME_RYAKU;
                            row[COLUMN_NAME_GYOUSHA_CD] = lastData.GYOUSHA_CD;
                            row[COLUMN_NAME_GYOUSHA_NAME_RYAKU] = lastData.GYOUSHA_NAME_RYAKU;
                            row[COLUMN_NAME_GENBA_CD] = lastData.GENBA_CD;
                            row[COLUMN_NAME_GENBA_NAME_RYAKU] = lastData.GENBA_NAME_RYAKU;
                            row[COLUMN_NAME_EIGYOU_TANTOU_CD] = lastData.EIGYOU_TANTOU_CD;
                            row[COLUMN_NAME_SHAIN_NAME_RYAKU] = lastData.SHAIN_NAME_RYAKU;
                            row[COLUMN_NAME_SECCHI_DATE] = lastData.SECCHI_DATE;
                            row[COLUMN_NAME_DAYSCOUNT] = lastData.DAYSCOUNT;
                            row[COLUMN_NAME_GRAPH] = lastData.GRAPH;
                            row[COLUMN_NAME_DAISUU] = daisuuCntTotal;
                            displayData.Rows.Add(row);
                        }
                    }

                    this.SearchResult = displayData;
                }
                else
                {
                    // 個体管理

                    // 実績データ(収集受付、受入、売上支払)から取得
                    var resulutList = this.dao.GetIchiranJissekiDataSql(this.SearchString);

                    if (resulutList != null
                        && resulutList.Count > 0)
                    {
                        // 引揚がされていないデータを抽出
                        var genbas = resulutList.AsEnumerable().Select(s => new { s.CONTENA_SHURUI_CD, s.CONTENA_CD, s.GYOUSHA_CD, s.GENBA_CD })
                            .GroupBy(g => new { g.CONTENA_SHURUI_CD, g.CONTENA_CD, g.GYOUSHA_CD, g.GENBA_CD });

                        foreach (var genba in genbas)
                        {
                            var rows = resulutList.AsEnumerable()
                                .Where(w => w.CONTENA_SHURUI_CD.Equals(genba.Key.CONTENA_SHURUI_CD)
                                    && w.CONTENA_CD.Equals(genba.Key.CONTENA_CD)
                                    && w.GYOUSHA_CD.Equals(genba.Key.GYOUSHA_CD)
                                    && w.GENBA_CD.Equals(genba.Key.GENBA_CD)
                                    && w.CONTENA_SET_KBN == 2).ToArray();

                            foreach (var row in rows)
                            {
                                var secchiRow = resulutList.AsEnumerable().Where(w => w.SECCHI_DATE != null
                                    && Convert.ToDateTime(w.SECCHI_DATE) <= (Convert.ToDateTime(row.SECCHI_DATE))
                                    && w.CONTENA_SHURUI_CD.Equals(genba.Key.CONTENA_SHURUI_CD)
                                    && w.CONTENA_CD.Equals(genba.Key.CONTENA_CD)
                                    && w.GYOUSHA_CD.Equals(genba.Key.GYOUSHA_CD.ToString())
                                    && w.GENBA_CD.Equals(genba.Key.GENBA_CD)
                                    && w.CONTENA_SET_KBN == 1)
                                    .OrderByDescending(o => o.SECCHI_DATE).ToArray();

                                // 設置 -> 引揚の操作のセットがあった場合はリストから除外
                                if (secchiRow != null && secchiRow.Count() > 0)
                                {
                                    // 設置分
                                    resulutList.Remove(secchiRow.First());
                                }

                                // 引揚分を除外
                                resulutList.Remove(row);
                            }

                        }
                    }

                    DataTable displayData = new DataTable();
                    for (int i = 0; i < this.columnNamesForKotaiknri.Length; i++)
                    {
                        displayData.Columns.Add(columnNamesForKotaiknri[i].ToString(), System.Type.GetType(columnTyepesForKotaiKanri[i]));
                    }

                    foreach (SearchResult data in resulutList)
                    {
                        DataRow row = displayData.NewRow();
                        row[COLUMN_NAME_SECCHICHOUUHUKU] = data.SecchiChouhuku;
                        row[COLUMN_NAME_CONTENA_SHURUI_CD] = data.CONTENA_SHURUI_CD;
                        row[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU] = data.CONTENA_SHURUI_NAME_RYAKU;
                        row[COLUMN_NAME_CONTENA_CD] = data.CONTENA_CD;
                        row[COLUMN_NAME_CONTENA_NAME_RYAKU] = data.CONTENA_NAME_RYAKU;
                        row[COLUMN_NAME_GYOUSHA_CD] = data.GYOUSHA_CD;
                        row[COLUMN_NAME_GYOUSHA_NAME_RYAKU] = data.GYOUSHA_NAME_RYAKU;
                        row[COLUMN_NAME_GENBA_CD] = data.GENBA_CD;
                        row[COLUMN_NAME_GENBA_NAME_RYAKU] = data.GENBA_NAME_RYAKU;
                        row[COLUMN_NAME_EIGYOU_TANTOU_CD] = data.EIGYOU_TANTOU_CD;
                        row[COLUMN_NAME_SHAIN_NAME_RYAKU] = data.SHAIN_NAME_RYAKU;
                        row[COLUMN_NAME_SECCHI_DATE] = data.SECCHI_DATE;
                        row[COLUMN_NAME_DAYSCOUNT] = data.DAYSCOUNT;
                        row[COLUMN_NAME_GRAPH] = data.GRAPH;
                        displayData.Rows.Add(row);
                    }

                    this.SearchResult = displayData;

                    this.SearchResult.DefaultView.RowFilter = string.Format("{0} >= '{1}'", COLUMN_NAME_DAYSCOUNT, this.SearchString.ELAPSED_DAYS);
                    // [F11]フィルタ機能でRowFilterが上書かれるため、その対策
                    this.SearchResult = this.SearchResult.DefaultView.ToTable();

                    // 重複するコンテナが存在したら、画面上わかりやすいように重複設置カラムに記号を設定
                    // 取得した状態のDataTableは読み取り専用になっているため、ReadOnlyを一度はずす
                    foreach (DataColumn col in this.SearchResult.Columns)
                    {
                        col.ReadOnly = false;
                    }

                    foreach (DataRow row in this.SearchResult.Rows)
                    {
                        var filterRow = this.SearchResult.AsEnumerable().Where(w => w[COLUMN_NAME_CONTENA_SHURUI_CD].ToString().Equals(row[COLUMN_NAME_CONTENA_SHURUI_CD].ToString())
                                                                                    && w[COLUMN_NAME_CONTENA_CD].ToString().Equals(row[COLUMN_NAME_CONTENA_CD].ToString()));

                        if (filterRow != null && filterRow.Count() > 1)
                        {
                            foreach (DataRow tempRow in filterRow)
                            {
                                tempRow[COLUMN_NAME_SECCHICHOUUHUKU] = this.CHOUHUKU_SECCHI_VALUE;
                            }
                        }
                    }

                    // ReadOnlyがTrueの状態だと、画面に表示したときにCell色の制御がおかしくなるため、
                    // ReadOnlyをFalseに戻す
                    foreach (DataColumn col in this.SearchResult.Columns)
                    {
                        col.ReadOnly = true;
                    }

                }

                // 検索結果を画面に設定する
                var dataRow = this.SearchResult.AsEnumerable().Where(w => int.Parse(w[COLUMN_NAME_DAYSCOUNT].ToString()) >= this.SearchString.ELAPSED_DAYS);
                int count = dataRow.Count();
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");

                    //読込データ件数を0にする
                    this.headForm.ReadDataNumber.Text = count.ToString();
                    //明細をクリア
                    this.form.Ichiran.DataSource = null;
                    this.form.IchiranForCSVOutput.DataSource = null;
                    this.form.GYOUSHA_CD_HEADER.Focus();
                    return;
                }
                else
                {
                    // 重複設置はSQL取得ではなく、LogicClassで自前で設定しているため
                    // 重複設置の絞込はこのタイミングで実行
                    if ("1".Equals(this.form.ChouhukuSecchiNomi.Text))
                    {
                        this.SearchResult.DefaultView.RowFilter = string.Format("{0} = '{1}' AND {2} >= '{3}'", COLUMN_NAME_SECCHICHOUUHUKU, CHOUHUKU_SECCHI_VALUE, COLUMN_NAME_DAYSCOUNT, this.SearchString.ELAPSED_DAYS);
                        dataRow = this.SearchResult.AsEnumerable().Where(w => w[COLUMN_NAME_SECCHICHOUUHUKU].ToString().Equals(CHOUHUKU_SECCHI_VALUE)
                                                    && int.Parse(w[COLUMN_NAME_DAYSCOUNT].ToString()) >= this.SearchString.ELAPSED_DAYS);
                        count = dataRow.Count();

                        // [F11]フィルタ機能でRowFilterが上書かれるため、その対策
                        this.SearchResult = this.SearchResult.DefaultView.ToTable();

                        if (count == 0)
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("C001");

                            //読込データ件数を0にする
                            this.headForm.ReadDataNumber.Text = count.ToString();
                            //明細をクリア
                            this.form.Ichiran.DataSource = null;
                            this.form.IchiranForCSVOutput.DataSource = null;
                            this.form.GYOUSHA_CD_HEADER.Focus();
                            return;
                        }
                    }

                    // 検索結果を表示する
                    this.ShowData(this.SearchResult);
                }

                // 検索件数を画面に設定する
                this.headForm.ReadDataNumber.Text = this.form.Ichiran.RowCount.ToString();

                // グラフ（ｎ月迄）を一覧に設定する
                this.form.Ichiran.Columns["graph"].HeaderText = "グラフ（" + this.SearchString.SYS_DAYS_COUNT + "日まで）";
                // フォーカス設定
                this.form.GYOUSHA_CD_HEADER.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region 並び替え
        /// <summary>
        /// 「F10 並び替えボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //一覧に明細行がない場合
                if (this.form.Ichiran.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E076");
                }
                else
                {
                    //ソート設定ダイアログを呼び出す
                    this.form.customSortHeader1.ShowCustomSortSettingDialog();
                }

                // フォーカス設定
                this.form.GYOUSHA_CD_HEADER.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func10_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region フィルタ
        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.form.Ichiran != null)
            {
                this.headForm.ReadDataNumber.Text = this.form.Ichiran.Rows.Count.ToString();
            }
            else
            {
                this.headForm.ReadDataNumber.Text = "0";
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 閉じる
        /// <summary>
        /// 「F12 閉じるボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }
        #endregion

        #region 地図表示

        /// <summary>
        /// 「SubF12 地図表示ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_process1_Click(object sender, EventArgs e)
        {
            if (this.msgLogic.MessageBoxShowConfirm("設置コンテナ一覧の内容を地図表示しますか？" +
                Environment.NewLine + "※緯度及び経度が未入力の場合は、表示されません。") == DialogResult.No)
            {
                return;
            }

            MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

            // DTO作成前にDataTableに一覧の内容をセットする
            DataTable dt = this.createIchiranDto();

            // 作成したDataTableから地図に渡すDTO作成
            List<mapDtoList> dtos = new List<mapDtoList>();
            dtos = this.createMapboxDto(dt);

            int cnt = 0;
            foreach (mapDtoList item in dtos)
            {
                if (item.dtos.Where(a => a.latitude != "").Count() > 0)
                {
                    cnt++;
                }
            }

            if (cnt == 0)
            {
                this.msgLogic.MessageBoxShowError("表示する対象がありません。");
                return;
            }

            // 地図表示
            gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.T_CONTENA_ICHIRAN);
        }

        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public void SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();
                DTOCls searchCondition = new DTOCls();

                // アラート件数

                // 業者
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_HEADER.Text))
                {
                    searchCondition.GYOUSHA_CD = this.form.GYOUSHA_CD_HEADER.Text;
                }

                // コンテナ種類
                if (!string.IsNullOrEmpty(this.form.CONTENA_SHURUI_CD_HEADER.Text))
                {
                    searchCondition.CONTENA_SHURUI_CD = this.form.CONTENA_SHURUI_CD_HEADER.Text;
                }

                // 現場
                if (!string.IsNullOrEmpty(this.form.GENBA_CD_HEADER.Text))
                {
                    searchCondition.GENBA_CD = this.form.GENBA_CD_HEADER.Text;
                }

                // コンテナ名
                if (!string.IsNullOrEmpty(this.form.CONTENA_CD_HEADER.Text))
                {
                    searchCondition.CONTENA_CD = this.form.CONTENA_CD_HEADER.Text;
                }

                // 営業担当者
                if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_CD_HEADER.Text))
                {
                    searchCondition.EIGYOU_TANTOU_CD = this.form.EIGYOU_TANTOU_CD_HEADER.Text;
                }

                // 経過日数
                if (!string.IsNullOrEmpty(this.form.ELAPSED_DAYS.Text))
                {
                    searchCondition.ELAPSED_DAYS = Int16.Parse(this.form.ELAPSED_DAYS.Text);
                }

                // グラフ（ｎ日迄）
                if (!this.sysInfoEntity.CONTENA_MAX_SET_KEIKA_DATE.IsNull)
                {
                    searchCondition.SYS_DAYS_COUNT = this.sysInfoEntity.CONTENA_MAX_SET_KEIKA_DATE;
                }
                else
                {
                    searchCondition.SYS_DAYS_COUNT = 999;
                }

                this.SearchString = searchCondition;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSearchString", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region header設定
        /// <summary>
        /// header設定
        /// </summary>
        public void SetHeader(UIHeader headForm)
        {
            try
            {
                LogUtility.DebugMethodStart(headForm);
                this.headForm = headForm;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(headForm);
            }
        }
        #endregion

        #region 検索結果表示処理
        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData(DataTable searchResult)
        {
            try
            {
                LogUtility.DebugMethodStart(searchResult);

                DataTable dt = searchResult;

                // アラート件数を設定する（カンマを除く）
                if (!string.IsNullOrEmpty(this.headForm.alertNumber.Text))
                {
                    this.AlertCount = int.Parse(this.headForm.alertNumber.Text.Replace(",", ""));
                }
                else
                {
                    this.AlertCount = 0;
                }
                // DataGridViewに値の設定を行う
                this.CreateDataGridView(dt);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(searchResult);
            }
        }
        #endregion

        #region DataGridViewに値の設定
        /// <summary>
        /// DataGridViewに値の設定を行う
        /// </summary>
        /// <param name="table"></param>
        public void CreateDataGridView(DataTable table)
        {
            try
            {
                LogUtility.DebugMethodStart(table);
                DialogResult result = DialogResult.Yes;

                if (this.AlertCount != 0 && this.AlertCount < table.Rows.Count)
                {
                    MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                    result = showLogic.MessageBoxShow("C025");
                }
                if (result == DialogResult.Yes)
                {
                    this.form.Ichiran.IsBrowsePurpose = false;

                    this.form.customSortHeader1.SortDataTable(table);
                    this.form.customSearchHeader1.SearchDataTable(table);

                    this.form.Ichiran.DataSource = table;
                    this.form.IchiranForCSVOutput.DataSource = table;

                    foreach (DataGridViewColumn column in this.form.Ichiran.Columns)
                    {
                        column.ReadOnly = true;
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    this.form.Ichiran.IsBrowsePurpose = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateDataGridView", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(table);
            }
        }
        #endregion

        #region 業者有効性チェック
        /// <summary>
        /// 業者有効性チェック
        /// </summary>
        /// <param name="tbGenbrCd"></param>
        /// <param name="e"></param>
        public void GYOUSHA_CD_HEADER_Validating(CustomAlphaNumTextBox tbGyousyaCd, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(tbGyousyaCd, e);

                // 値に変更が無ければ何もしない
                if (this.form.GYOUSHA_CD_HEADER.Text.Equals(this.beforeGyoushaCd))
                {
                    return;
                }

                var gyoushaCd = tbGyousyaCd.Text;
                var gyoushaName = this.form.GYOUSHA_NAME_RYAKU_HEADER.Text;
                var genbrCd = this.form.GENBA_CD_HEADER.Text;
                var genbrName = this.form.GENBA_NAME_RYAKU_HEADER.Text;

                if (gyoushaCd.Trim() != "")
                {
                    this.form.GENBA_NAME_RYAKU_HEADER.Text = "";
                    this.form.GENBA_CD_HEADER.Text = "";
                }
                else
                {
                    this.form.GENBA_NAME_RYAKU_HEADER.Text = "";
                    this.form.GENBA_CD_HEADER.Text = "";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_CD_HEADER_Validating", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(tbGyousyaCd, e);
            }
        }
        #endregion

        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        #region 現場有効性チェック
        ///// <summary>
        ///// 現場有効性チェック
        ///// </summary>
        ///// <param name="tbGenbrCd"></param>
        ///// <param name="e"></param>
        //public void GENBA_CD_HEADER_Validating(CustomAlphaNumTextBox tbGenbrCd, CancelEventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(tbGenbrCd, e);

        //        // 変更が無ければ何もしない
        //        if (this.form.GENBA_CD_HEADER.Text.Equals(this.beforeGenbaCd))
        //        {
        //            return;
        //        }

        //        var gyoushaCd = this.form.GYOUSHA_CD_HEADER.Text;
        //        var gyoushaName = this.form.GYOUSHA_NAME_RYAKU_HEADER.Text;
        //        var genbrCd = tbGenbrCd.Text;
        //        var genbrName = this.form.GENBA_NAME_RYAKU_HEADER.Text;

        //        if (genbrCd.Trim() != "")
        //        {
        //            var kv = GetGenbrInfo(genbrCd, gyoushaCd);
        //            var messageShowLogic = new MessageBoxShowLogic();

        //            switch (kv.Key)
        //            {
        //                case 0:
        //                    this.form.GENBA_NAME_RYAKU_HEADER.Text = "";
        //                    this.form.GENBA_CD_HEADER.IsInputErrorOccured = true;
        //                    messageShowLogic.MessageBoxShow("E020", "現場");
        //                    e.Cancel = true;
        //                    this.form.GENBA_CD_HEADER.SelectAll();
        //                    break;
        //                case 1:
        //                    var dr = kv.Value;
        //                    this.form.GYOUSHA_CD_HEADER.Text = dr.Field<string>("GYOUSHA_CD");
        //                    this.form.GYOUSHA_NAME_RYAKU_HEADER.Text = dr.Field<string>("GYOUSHA_NAME_RYAKU");
        //                    this.form.GENBA_CD_HEADER.Text = dr.Field<string>("GENBA_CD");
        //                    this.form.GENBA_NAME_RYAKU_HEADER.Text = dr.Field<string>("GENBA_NAME_RYAKU");
        //                    break;
        //                default:
        //                    SendKeys.Send(" ");
        //                    e.Cancel = true;
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            this.form.GENBA_NAME_RYAKU_HEADER.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("GENBA_CD_HEADER_Validating", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd(tbGenbrCd, e);
        //    }
        //}


        /// <summary>
        /// 現場CDエラーチェック
        /// </summary>
        /// <returns></returns>
        internal bool ErrorCheckGenba(string gyoushaCd, string genbaCd)
        {
            bool ren = true;
            try
            {
                // 業者入力されてない場合
                if (String.IsNullOrEmpty(gyoushaCd) && !String.IsNullOrEmpty(genbaCd))
                {
                    this.msgLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD_HEADER.Text = String.Empty;
                    this.form.GENBA_NAME_RYAKU_HEADER.Text = String.Empty;
                    return false;
                }

                if (!String.IsNullOrEmpty(gyoushaCd))
                {
                    // 現場情報を取得
                    if (!string.IsNullOrEmpty(genbaCd))
                    {
                        var genab = this.GetGenba(gyoushaCd, genbaCd);
                        if (genab.Count() == 0)
                        {
                            // マスタに現場が存在しない場合
                            // 現場の関連情報をクリア
                            this.form.GENBA_CD_HEADER.Text = String.Empty;
                            this.form.GENBA_NAME_RYAKU_HEADER.Text = String.Empty;
                            this.msgLogic.MessageBoxShow("E020", "現場");
                            return false;

                        }
                        else
                        {
                            // 現場名を取得
                            this.form.GENBA_NAME_RYAKU_HEADER.Text = genab[0].GENBA_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ErrorCheckGenba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ren = false;
            }
            return ren;
        }

        /// <summary>
        /// 現場CDで現場リストを取得します
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <returns>現場エンティティリスト</returns>
        public M_GENBA[] GetGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            // 取得済みの現場リストから取得
            var gList = this.genbaList.Where(g => g.GYOUSHA_CD == gyoushaCd && g.GENBA_CD == genbaCd);
            if (gList.Count() == 0)
            {
                // なければDBから取得
                var keyEntity = new M_GENBA();
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;

                var genbaEntities = this.genbaDao.GetAllValidData(keyEntity);
                if (null != genbaEntities)
                {
                    this.genbaList.AddRange(genbaEntities);
                    gList = genbaEntities;
                }
            }

            LogUtility.DebugMethodEnd();

            return gList.ToArray();
        }
        #endregion
        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        #region 現場情報取得
        /// <summary>
        /// 現場情報取得
        /// </summary>
        /// <param name="genbrCd"></param>
        /// <param name="gyoushaCd"></param>
        public KeyValuePair<int, DataRow> GetGenbrInfo(string genbrCd, string gyoushaCd)
        {
            try
            {
                LogUtility.DebugMethodStart(genbrCd, gyoushaCd);
                DTOCls searchConditionEntity = new DTOCls();
                searchConditionEntity.GENBA_CD = genbrCd;
                searchConditionEntity.GYOUSHA_CD = gyoushaCd;

                var dt = this.dao.GetGenbrDataSql(searchConditionEntity);

                int cnt = dt.Rows.Count;
                var dr = cnt == 1 ? dt.Rows[0] : null;
                var kv = new KeyValuePair<int, DataRow>(cnt, dr);

                return kv;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbrInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(genbrCd, gyoushaCd);
            }
        }
        #endregion

        #region コンテナ有効性チェック
        /// <summary>
        /// コンテナ有効性チェック
        /// </summary>
        /// <param name="tbContenaCd"></param>
        /// <param name="e"></param>
        public void CONTENA_CD_HEADER_Validating(CustomAlphaNumTextBox tbContenaCd, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(tbContenaCd, e);

                var messageShowLogic = new MessageBoxShowLogic();

                // 必須チェック
                if (string.IsNullOrEmpty(this.form.CONTENA_SHURUI_CD_HEADER.Text)
                    && !string.IsNullOrEmpty(tbContenaCd.Text))
                {
                    this.form.CONTENA_NAME_RYAKU_HEADER.Text = "";
                    this.form.CONTENA_CD_HEADER.IsInputErrorOccured = true;
                    messageShowLogic.MessageBoxShow("E051", "コンテナ種類");
                    e.Cancel = true;
                    this.form.CONTENA_CD_HEADER.SelectAll();
                    return;
                }

                // コンテナ種類CD
                var contenaShuiruiCd = this.form.CONTENA_SHURUI_CD_HEADER.Text;
                // コンテナCD
                var contenaCd = tbContenaCd.Text;

                if (contenaCd.Trim() != "")
                {

                    // コンテナCD、コンテナ種類CDにより、コンテナ情報取得
                    var kvContenaCD = GetContenaInfo(contenaShuiruiCd, contenaCd);
                    // コンテナ情報ない場合、処理を中止して、メッセージを表示する
                    if (kvContenaCD.Key == 0)
                    {
                        this.form.CONTENA_NAME_RYAKU_HEADER.Text = "";
                        this.form.CONTENA_CD_HEADER.IsInputErrorOccured = true;
                        messageShowLogic.MessageBoxShow("E020", "コンテナ");
                        e.Cancel = true;
                        this.form.CONTENA_CD_HEADER.SelectAll();
                        return;
                    }

                    // コンテナCD、コンテナ種類CD、現場CD、業者CDにより、コンテナ情報取得
                    var kv = GetContenaInfoByCD(contenaShuiruiCd, contenaCd);

                    switch (kv.Key)
                    {
                        case 0:
                            this.form.CONTENA_NAME_RYAKU_HEADER.Text = "";
                            this.form.CONTENA_CD_HEADER.IsInputErrorOccured = true;
                            messageShowLogic.MessageBoxShow("E062", "コンテナ種類");
                            e.Cancel = true;
                            this.form.CONTENA_CD_HEADER.SelectAll();
                            break;
                        case 1:
                            var dr = kv.Value;
                            this.form.CONTENA_NAME_RYAKU_HEADER.Text = dr.Field<string>("CONTENA_NAME_RYAKU");
                            //this.form.CONTENA_SHURUI_CD_HEADER.Text = dr.Field<string>("CONTENA_SHURUI_CD");
                            this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Text = dr.Field<string>("CONTENA_SHURUI_NAME_RYAKU");
                            break;
                        default:
                            SendKeys.Send(" ");
                            e.Cancel = true;
                            break;
                    }
                }
                else
                {
                    this.form.CONTENA_NAME_RYAKU_HEADER.Text = "";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CONTENA_CD_HEADER_Validating", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(tbContenaCd, e);
            }
        }
        #endregion

        #region コンテナ種類存在チェック
        internal void CheckContenaShuruiMaster(CustomAlphaNumTextBox tbContenaShuruiCd, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.form.CONTENA_SHURUI_CD_HEADER.Text))
                {
                    this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Text = string.Empty;
                    if (this.form.CONTENA_CD_HEADER.Visible)
                    {
                        this.form.CONTENA_CD_HEADER.Text = string.Empty;
                    }
                    if (this.form.CONTENA_NAME_RYAKU_HEADER.Visible)
                    {
                        this.form.CONTENA_NAME_RYAKU_HEADER.Text = string.Empty;
                    }
                    return;
                }
                else if (!this.form.CONTENA_CD_HEADER.Text.Equals(this.beforeContenaShuruiCd))
                {
                    if (this.form.CONTENA_CD_HEADER.Visible)
                    {
                        this.form.CONTENA_CD_HEADER.Text = string.Empty;
                    }
                    if (this.form.CONTENA_NAME_RYAKU_HEADER.Visible)
                    {
                        this.form.CONTENA_NAME_RYAKU_HEADER.Text = string.Empty;
                    }
                }

                var messageShowLogic = new MessageBoxShowLogic();

                var contenaShurui = contenaShuruiDao.GetDataByCd(this.form.CONTENA_SHURUI_CD_HEADER.Text);
                if (contenaShurui == null
                    || string.IsNullOrEmpty(contenaShurui.CONTENA_SHURUI_CD))
                {
                    this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Text = string.Empty;
                    this.form.CONTENA_SHURUI_CD_HEADER.IsInputErrorOccured = true;
                    messageShowLogic.MessageBoxShow("E020", "コンテナ");
                    e.Cancel = true;
                    this.form.CONTENA_SHURUI_CD_HEADER.SelectAll();
                    return;
                }
                else
                {
                    this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Text = contenaShurui.CONTENA_SHURUI_NAME_RYAKU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckContenaShuruiMaster", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }
        }
        #endregion

        #region コンテナ種類有効性チェック
        /// <summary>
        /// コンテナ種類有効性チェック
        /// </summary>
        /// <param name="tbContenaShuruiCd"></param>
        /// <param name="e"></param>
        public void CONTENA_SHURUI_CD_HEADER_Validating(CustomAlphaNumTextBox tbContenaShuruiCd, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(tbContenaShuruiCd, e);

                // コンテナCD
                var contenaCd = this.form.CONTENA_CD_HEADER.Text;
                // コンテナ種類CD
                var contenaShuiruiCd = tbContenaShuruiCd.Text;

                if (contenaShuiruiCd.Trim() != "")
                {
                    var messageShowLogic = new MessageBoxShowLogic();

                    // コンテナCD、コンテナ種類CDにより、コンテナ情報取得
                    var kvContenaCD = GetContenaInfo(contenaShuiruiCd, contenaCd);
                    // コンテナ情報ない場合、処理を中止して、メッセージを表示する
                    if (kvContenaCD.Key == 0)
                    {
                        this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Text = "";
                        this.form.CONTENA_SHURUI_CD_HEADER.IsInputErrorOccured = true;
                        messageShowLogic.MessageBoxShow("E020", "コンテナ");
                        e.Cancel = true;
                        this.form.CONTENA_SHURUI_CD_HEADER.SelectAll();
                        return;
                    }

                    // コンテナCD、コンテナ種類CD、現場CD、業者CDにより、コンテナ情報取得
                    var kv = GetContenaInfoByCD(contenaShuiruiCd, contenaCd);

                    switch (kv.Key)
                    {
                        case 0:
                            this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Text = "";
                            this.form.CONTENA_SHURUI_CD_HEADER.IsInputErrorOccured = true;
                            messageShowLogic.MessageBoxShow("E062", "業者CD、現場CD");
                            e.Cancel = true;
                            this.form.CONTENA_SHURUI_CD_HEADER.SelectAll();
                            break;
                        case 1:
                            var dr = kv.Value;
                            this.form.CONTENA_CD_HEADER.Text = dr.Field<string>("CONTENA_CD");
                            this.form.CONTENA_NAME_RYAKU_HEADER.Text = dr.Field<string>("CONTENA_NAME_RYAKU");
                            //this.form.CONTENA_SHURUI_CD_HEADER.Text = dr.Field<string>("CONTENA_SHURUI_CD");
                            this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Text = dr.Field<string>("CONTENA_SHURUI_NAME_RYAKU");
                            break;
                        default:
                            SendKeys.Send(" ");
                            e.Cancel = true;
                            break;
                    }
                }
                else
                {
                    this.form.CONTENA_SHURUI_NAME_RYAKU_HEADER.Text = "";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CONTENA_SHURUI_CD_HEADER_Validating", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd(tbContenaShuruiCd, e);
            }
        }
        #endregion

        #region コンテナ情報取得
        /// <summary>
        /// コンテナ情報取得
        /// </summary>
        /// <param name="genbrCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="contenaShuiruiCd"></param>
        /// <param name="contenaCd"></param>
        public KeyValuePair<int, DataRow> GetContenaInfo(string contenaShuiruiCd, string contenaCd)
        {
            try
            {
                LogUtility.DebugMethodStart(contenaShuiruiCd, contenaCd);
                DTOCls mContena = new DTOCls();

                // コンテナ種類CD
                if (!string.IsNullOrEmpty(contenaShuiruiCd))
                {
                    mContena.CONTENA_SHURUI_CD = contenaShuiruiCd;
                }
                // コンテナCD
                if (!string.IsNullOrEmpty(contenaCd))
                {
                    mContena.CONTENA_CD = contenaCd;
                }

                var dt = this.dao.GetContenaDataSql(mContena);

                int cnt = dt.Rows.Count;
                var dr = cnt == 1 ? dt.Rows[0] : null;
                var kv = new KeyValuePair<int, DataRow>(cnt, dr);

                return kv;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbrInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region コンテナ情報取得
        /// <summary>
        /// コンテナ情報取得
        /// </summary>
        /// <param name="contenaShuiruiCd"></param>
        /// <param name="contenaCd"></param>
        public KeyValuePair<int, DataRow> GetContenaInfoByCD(string contenaShuiruiCd, string contenaCd)
        {
            try
            {
                LogUtility.DebugMethodStart(contenaShuiruiCd, contenaCd);
                DTOCls mContena = new DTOCls();

                // コンテナ種類CD
                if (!string.IsNullOrEmpty(contenaShuiruiCd))
                {
                    mContena.CONTENA_SHURUI_CD = contenaShuiruiCd;
                }
                // コンテナCD
                if (!string.IsNullOrEmpty(contenaCd))
                {
                    mContena.CONTENA_CD = contenaCd;
                }

                var dt = this.dao.GetContenaDataSqlByCD(mContena);

                int cnt = dt.Rows.Count;
                var dr = cnt == 1 ? dt.Rows[0] : null;
                var kv = new KeyValuePair<int, DataRow>(cnt, dr);

                return kv;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbrInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region コンテナCD(検索条件)のポップアップ情報設定
        /// <summary>
        /// コンテナCD(検索条件)のポップアップ情報をセット
        /// </summary>
        internal void SetPopupInfoForContenaCdHeader()
        {
            try
            {
                this.form.CONTENA_CD_HEADER.PopupGetMasterField = "CONTENA_CD, CONTENA_NAME_RYAKU, CONTENA_SHURUI_CD, CONTENA_SHURUI_NAME_RYAKU";
                this.form.CONTENA_CD_HEADER.PopupDataHeaderTitle = new string[] { "ｺﾝﾃﾅCD", "ｺﾝﾃﾅ名", "ｺﾝﾃﾅ種類CD", "ｺﾝﾃﾅ種類名" };
                this.form.CONTENA_CD_HEADER.PopupSetFormField = "CONTENA_CD_HEADER,CONTENA_NAME_RYAKU_HEADER,CONTENA_SHURUI_CD_HEADER,CONTENA_SHURUI_NAME_RYAKU_HEADER";

                // DataSourceをセット
                // コンテナ指定画面の設置コンテナCD検索と同等の条件を使用。
                // もしコンテナ指定画面の仕様が変更になった場合は、G041で独自に実装すること。
                SearchConditionDto condition = new SearchConditionDto();
                condition.CONTENA_SHURUI_CD = string.IsNullOrEmpty(this.form.CONTENA_SHURUI_CD_HEADER.Text) ? null : this.form.CONTENA_SHURUI_CD_HEADER.Text;
                condition.DENPYOU_DATE = DateTime.Now.Date.ToString();
                condition.ISNOT_NEED_DELETE_FLG = true;
                var dao = DaoInitUtility.GetComponent<CheckCONRETDaoCls>();
                var ret = dao.GetContenaData(condition);

                var dataTable = new DataTable();
                dataTable.Columns.Add("CONTENA_CD");
                dataTable.Columns.Add("CONTENA_NAME_RYAKU");
                dataTable.Columns.Add("CONTENA_SHURUI_CD");
                dataTable.Columns.Add("CONTENA_SHURUI_NAME_RYAKU");

                foreach (var data in ret)
                {
                    var dispData = dataTable.NewRow();
                    dispData["CONTENA_CD"] = data.CONTENA_CD;
                    dispData["CONTENA_NAME_RYAKU"] = data.CONTENA_NAME_RYAKU;
                    dispData["CONTENA_SHURUI_CD"] = data.CONTENA_SHURUI_CD;
                    dispData["CONTENA_SHURUI_NAME_RYAKU"] = data.CONTENA_SHURUI_NAME_RYAKU;
                    dataTable.Rows.Add(dispData);
                }

                this.form.CONTENA_CD_HEADER.PopupDataSource = dataTable;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetPopupInfoForContenaCdHeader", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }
        #endregion

        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region 自動生成（実装なし）
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Create Datatable

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();

            foreach (string str in listColumnn)
            {
                dt.Columns.Add(str);
            }

            return dt;
        }

        #endregion

        #region 連携処理

        // 一覧の内容を配列にセット
        private DataTable createIchiranDto()
        {
            DataTable dt = new DataTable();

            try
            {
                dt.Columns.Add("CONTENA_SHURUI_CD");
                dt.Columns.Add("CONTENA_SHURUI_NAME_RYAKU");
                dt.Columns.Add("CONTENA_NAME_RYAKU");
                dt.Columns.Add("GYOUSHA_CD");
                dt.Columns.Add("GENBA_CD");
                dt.Columns.Add("DAYSCOUNT");
                dt.Columns.Add("SECCHI_DATE");
                dt.Columns.Add("DAISUU");
                dt.Columns.Add("SecchiChouhuku");

                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    DataRow datarow;
                    datarow = dt.NewRow();
                    datarow["CONTENA_SHURUI_CD"] = row.Cells["CONTENA_SHURUI_CD"].Value;
                    datarow["CONTENA_SHURUI_NAME_RYAKU"] = row.Cells["CONTENA_SHURUI_NAME_RYAKU"].Value;
                    datarow["CONTENA_NAME_RYAKU"] = row.Cells["CONTENA_NAME_RYAKU"].Value;
                    datarow["GYOUSHA_CD"] = row.Cells["GYOUSHA_CD"].Value;
                    datarow["GENBA_CD"] = row.Cells["GENBA_CD"].Value;
                    datarow["DAYSCOUNT"] = row.Cells["DAYSCOUNT"].Value;
                    datarow["SECCHI_DATE"] = row.Cells["SECCHI_DATE"].Value;
                    datarow["DAISUU"] = row.Cells["DAISUU"].Value;
                    datarow["SecchiChouhuku"] = row.Cells["SecchiChouhuku"].Value;
                    dt.Rows.Add(datarow);
                }

                // DataViewを使って並び替える
                DataView dv = new DataView(dt);
                dv.Sort = "CONTENA_SHURUI_CD";

                // DataViewをDataTableに戻す
                dt = dv.ToTable();
            }
            catch (Exception ex)
            {
                LogUtility.Error("createIchiranDto", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                throw;
            }
            return dt;
        }


        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        private List<mapDtoList> createMapboxDto(DataTable data)
        {
            try
            {
                // 出力用データの整形
                int layerId = 1;
                List<mapDtoList> dtoLists = new List<mapDtoList>();

                string contenaShuruiCd = string.Empty;

                int i = 0;

                // コース明細をDTOにセット
                foreach (DataRow row in data.Rows)
                {
                    // コンテナ種類が切り替わったら表示切替用カラムをインクリメントする
                    if (Convert.ToString(row["CONTENA_SHURUI_CD"]) != contenaShuruiCd && i > 0)
                    {
                        layerId++;
                    }
                    mapDtoList dtoList = new mapDtoList();
                    dtoList.layerId = i + 1;
                    List<mapDto> dtos = new List<mapDto>();

                    string sql = string.Empty;
                    StringBuilder sb = new StringBuilder();

                    sb.AppendFormat(" SELECT ");
                    sb.AppendFormat(" GEN.GYOUSHA_CD AS {0} ", ConstCls.GYOUSHA_CD);
                    sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstCls.GYOUSHA_NAME_RYAKU);
                    sb.AppendFormat(",GEN.GENBA_CD AS {0} ", ConstCls.GENBA_CD);
                    sb.AppendFormat(",GEN.GENBA_NAME_RYAKU AS {0} ", ConstCls.GENBA_NAME_RYAKU);
                    sb.AppendFormat(",GEN.GENBA_LATITUDE AS {0} ", ConstCls.GENBA_LATITUDE);
                    sb.AppendFormat(",GEN.GENBA_LONGITUDE AS {0} ", ConstCls.GENBA_LONGITUDE);
                    sb.AppendFormat(" FROM M_GENBA AS GEN ");
                    sb.AppendFormat(" LEFT JOIN M_GYOUSHA GYO ON GEN.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                    sb.AppendFormat(" WHERE GEN.DELETE_FLG = 0 ");
                    sb.AppendFormat(" AND GEN.GYOUSHA_CD = '{0}'", row["GYOUSHA_CD"]);
                    sb.AppendFormat(" AND GEN.GENBA_CD = '{0}'", row["GENBA_CD"]);

                    DataTable dt = this.genbaDao.GetDateForStringSql(sb.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        mapDto dto = new mapDto();
                        dto.id = i + 1;
                        dto.layerNo = layerId;
                        dto.torihikisakiCd = string.Empty;
                        dto.torihikisakiName = string.Empty;
                        dto.gyoushaCd = string.Empty;
                        dto.gyoushaName = Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_NAME_RYAKU]);
                        dto.genbaCd = string.Empty;
                        dto.genbaName = Convert.ToString(dt.Rows[0][ConstCls.GENBA_NAME_RYAKU]);
                        dto.post = string.Empty;
                        dto.address = string.Empty;
                        dto.tel = string.Empty;
                        dto.bikou1 = string.Empty;
                        dto.bikou2 = string.Empty;
                        dto.courseName = Convert.ToString(row["CONTENA_SHURUI_CD"]) + "－" + Convert.ToString(row["CONTENA_SHURUI_NAME_RYAKU"]);
                        dto.latitude = Convert.ToString(dt.Rows[0][ConstCls.GENBA_LATITUDE]);
                        dto.longitude = Convert.ToString(dt.Rows[0][ConstCls.GENBA_LONGITUDE]);
                        dto.rowNo = Convert.ToInt32(row["DAYSCOUNT"]);                                        // マーカー下の数字として表示される
                        dto.contenaShuruiName = Convert.ToString(row["CONTENA_SHURUI_NAME_RYAKU"]);           // コンテナ種類名
                        dto.contenaName = Convert.ToString(row["CONTENA_NAME_RYAKU"]);                        // 数量管理：なし　個体管理：コンテナ名
                        string secchiDate = string.Empty;
                        if (!string.IsNullOrEmpty(Convert.ToString(row["SECCHI_DATE"])))
                            secchiDate = Convert.ToDateTime(row["SECCHI_DATE"]).ToString("yyyy/MM/dd(ddd)");  // 設置日
                        dto.secchiDate = secchiDate;
                        dto.daisuu = Convert.ToString(row["DAISUU"]);                                         // 設置台数
                        dto.daysCount = Convert.ToString(row["DAYSCOUNT"]);                                   // 数量管理：無回転日数　個体管理：経過日数
                        dto.secchiChouhuku = Convert.ToString(row["SecchiChouhuku"]);                         // 設置重複
                        dtos.Add(dto);
                        dtoList.dtos = dtos;
                        if (dtoList.dtos.Count != 0)
                        {
                            dtoLists.Add(dtoList);
                        }
                    }

                    contenaShuruiCd = Convert.ToString(row["CONTENA_SHURUI_CD"]);
                    i++;
                }
                return dtoLists;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createMapboxDto", ex);
                this.msgLogic.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
