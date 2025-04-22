// $Id: LogicCls.cs 54491 2015-07-03 03:56:01Z quocthang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.ElectronicManifest.CustomControls_Ex;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.Allocation.TeikiHaisyaIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.TeikiHaisyaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeader headForm;

        /// <summary>
        /// 検索用SQL
        /// </summary>
        public string searchSql { get; set; }

        /// <summary>
        /// 車輌CD前回値
        /// </summary>
        private string oldSharyouCD;

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 一覧検索用のDao
        /// </summary>
        private DAOClass mDetailDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 車輌マスタのDao
        /// </summary>
        private IM_SHARYOUDao sharyouDao;
        /// <summary>
        /// 車種マスタのDao
        /// </summary>
        private IM_SHASHUDao shashuDao;
        /// <summary>
        /// 社員マスタのDao
        /// </summary>
        private IM_SHAINDao shainDao;
        /// <summary>
        /// 業者マスタDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;
        /// <summary>
        /// モバイル連携Dao
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// チェックボックスのスペースキー対応用
        /// </summary>
        private bool SpaceChk = false;
        private bool SpaceON = false;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者連携現場のDao
        /// </summary>
        private IM_SMS_RECEIVER_LINK_GENBADao smsReceiverLinkGenbaDao;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }
        /// <summary>
        /// 検索条件
        /// </summary>
        public string searchString { get; set; }
        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        public string joinQuery { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public int denShu_Kbn { get; set; }

        /// <summary>
        /// コース名称データ
        /// </summary>
        public M_COURSE_NAME[] mCourseNameAll { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                this.oldSharyouCD = string.Empty;
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.mDetailDao = DaoInitUtility.GetComponent<DAOClass>();

                this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();             // 車輌マスタのDao
                this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();               // 車種マスタのDao
                this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();                 // 社員マスタのDao
                this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();             // 業者マスタDao
                this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();                         // モバイル連携Dao
                this.MsgBox = new MessageBoxShowLogic();
                this.smsReceiverLinkGenbaDao = DaoInitUtility.GetComponent<IM_SMS_RECEIVER_LINK_GENBADao>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // 親フォームオブジェクト取得
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダフォームオブジェクト取得
                headForm = (UIHeader)this.parentForm.headerForm;
                //システム情報を取得する
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.form.SystemId.ToString());
                if (sysInfo != null)
                {
                    //システム情報からアラート件数を取得
                    this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
                    this.headForm.alertNumber.Text = this.alertCount.ToString();
                }

                //ボタンのテキストを初期化
                this.ButtonInit();
                //イベントの初期化処理
                this.EventInit();

                // オプション非対応
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    var parentForm = (BusinessBaseForm)this.form.Parent;
                    // mapbox用ボタン無効化
                    parentForm.bt_process3.Text = string.Empty;
                    parentForm.bt_process3.Enabled = false;
                }
                else
                {
                    // 一覧内のチェックボックスの設定
                    this.HeaderCheckBoxSupportMapbox();
                }
                // ｼｮｰﾄﾒｯｾｰｼﾞオプション
                if(!AppConfig.AppOptions.IsSMS())
                {
                    var parentForm = (BusinessBaseForm)this.form.Parent;
                    // ｼｮｰﾄﾒｯｾｰｼﾞボタン無効化
                    parentForm.bt_process4.Text = string.Empty;
                    parentForm.bt_process4.Enabled = false;
                }

                this.allControl = this.form.allControl;

                //並び替えと明細の設定
                this.form.customSortHeader1.Size = new Size(992, 26);
                this.form.customDataGridView1.Location = new Point(0, 175);
                this.form.customDataGridView1.Size = new Size(992, 248);

                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();
                PopUpDataInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                //customTextBoxでのエンターキー押下イベント生成
                this.form.searchString.KeyDown += new KeyEventHandler(SearchStringKeyDown);       //汎用検索(SearchString)

                //Functionボタンのイベント生成
                parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);              // F1 簡易検索／汎用検索
                parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);              // 新規
                parentForm.bt_func3.Click += new EventHandler(this.bt_func3_Click);              // 修正
                parentForm.bt_func4.Click += new EventHandler(this.bt_func4_Click);              // 削除
                //parentForm.bt_func5.Click += new EventHandler(this.bt_func5_Click);            // 複写
                parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);       // 配車表
                parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);              // CSV出力
                parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       // 条件クリア
                parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       // 検索
                parentForm.bt_func9.Click += new System.EventHandler(this.bt_func9_Click);       // 現場メモ登録
                parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     // 並び替え
                parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     // フィルタ
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     // 閉じる
                parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             // パターン一覧画面へ遷移
                parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             // 現場メモ一覧画面へ遷移
                parentForm.bt_process3.Click += new EventHandler(bt_process3_Click);             // 地図を表示
                parentForm.bt_process4.Click += new EventHandler(bt_process4_Click);             // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面へ遷移

                //明細画面上でダブルクリック時のイベント生成
                this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(customDataGridView1_MouseDoubleClick);

                this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(customDataGridView1_CellClick);
                this.form.customDataGridView1.PreviewKeyDown += new PreviewKeyDownEventHandler(this.DetailPreviewKeyDown);

                this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(customDataGridView1_ColumnHeaderMouseClick);

                // Validatedイベント
                this.form.txtSHAIN_CD.Validated += new EventHandler(this.form.UNTENSHA_CDValidated);
                this.form.txtSHoujyosyaCd.Validated += new EventHandler(this.form.HOJOIN_CDValidated);
                this.form.UNPAN_GYOUSHA_CD.Validated += new EventHandler(this.form.UNPAN_GYOUSHA_CDValidated);

                // 20141128 teikyou ダブルクリックを追加する　start
                this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
                // 20141128 teikyou ダブルクリックを追加する　end

                // Enterイベント

            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonSetting", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// ファンクションボタンの制御
        /// </summary>
        public void ButtonEnabledControl()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
            {
                parentForm.bt_func9.Enabled = true;
            }
            else
            {
                parentForm.bt_func9.Enabled = false;
            }
        }

        #region 検索処理

        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return 0;
                }
                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

                this.HeaderCheckBoxFalse();

                //検索用SQLの作成
                MakeSearchCondition();

                //検索実行
                this.searchResult = new DataTable();
                if (!string.IsNullOrEmpty(this.searchSql))
                {
                    this.searchResult = mDetailDao.GetDateForStringSql(this.searchSql);
                    count = searchResult.Rows.Count;

                    //検索結果が存在しませんの場合、メッセージを表示する
                    if (count == 0)
                    {
                        //読込データ件数を0にする
                        this.headForm.readDataNumber.Text = count.ToString();
                        //検索結果を表示する
                        this.form.ShowData(searchResult);
                        //DataGridViewのプロパティ再設定
                        setDataGridView();
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("C001");

                        if (this.form.searchString.Visible)
                        {
                            this.form.searchString.Focus();
                        }
                        else
                        {
                            this.form.txtSHAIN_CD.Focus();
                        }

                        return 0;
                    }
                    else
                    {
                        //2013/12/16 修正（製造課題一覧68） START
                        //this.form.customDataGridView1.DataSource = null;
                        //this.form.customDataGridView1.Columns.Clear();

                        //品名情報列がある場合、明細毎にまとめる
                        if (searchResult.Columns.Contains("品名情報"))
                        {
                            //searchResult = this.CorrectHinmeiInfo(searchResult);
                        }

                        //検索結果を表示する
                        this.form.ShowData(searchResult);

                        //DataGridView更新の場合
                        if (searchResult.Rows.Count == this.form.customDataGridView1.RowCount)
                        {
                            //読込データ件数の設定
                            if (this.form.customDataGridView1 != null)
                            {
                                this.headForm.readDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                            }
                            else
                            {
                                this.headForm.readDataNumber.Text = "0";
                            }

                            //DataGridViewのプロパティ再設定
                            setDataGridView();
                        }
                        //2013/12/16 修正（製造課題一覧68） END
                        return searchResult.Rows.Count;
                    }
                }
                else
                {
                    this.form.customDataGridView1.DataSource = null;
                    this.form.customDataGridView1.Columns.Clear();
                    //読込データ件数：０ [件]
                    this.headForm.readDataNumber.Text = "0";
                }

                return 0;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
        }

        /// <summary>
        /// 同じ明細の品名情報を結合して一つにまとめる
        /// </summary>
        /// <param name="baseDt">対象のデータテーブル</param>
        /// <returns>処理後のデータテーブル</returns>
        private DataTable CorrectHinmeiInfo(DataTable baseDt)
        {
            // 明細を特定するためのカラムの列インデックスを取得する
            int indexHinmei = baseDt.Columns.IndexOf(ConstCls.COLUMN_HINMEI_INFO_NAME);
            int indexSysID = baseDt.Columns.IndexOf(ConstCls.HIDDEN_COLUMN_SYSTEM_ID);
            int indexSeq = baseDt.Columns.IndexOf(ConstCls.HIDDEN_COLUMN_SEQ);
            int indexDetID = baseDt.Columns.IndexOf(ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID);
            if (new[] { indexHinmei, indexSysID, indexSeq, indexDetID }.Contains(-1))
            {
                // いずれかが含まれていない場合は何もせず返す
                return baseDt;
            }

            // 変換後に返すクローンを作成してループ開始
            DataTable newDt = baseDt.Clone();
            int indexZero = 0;
            while (0 < baseDt.Rows.Count)
            {
                // 元データテーブルから同じ明細のデータを抽出
                DataRow[] rows = baseDt.Rows.Cast<DataRow>().Where(n =>
                    n[indexSysID].ToString() == baseDt.Rows[indexZero][indexSysID].ToString() &&
                    n[indexSeq].ToString() == baseDt.Rows[indexZero][indexSeq].ToString() &&
                    n[indexDetID].ToString() == baseDt.Rows[indexZero][indexDetID].ToString()).ToArray();

                // 同じ明細の品名情報を一つの列にまとめる
                string hinmei = baseDt.Rows[indexZero][indexHinmei].ToString();
                for (int i = 1; i < rows.Length; i++)
                {
                    hinmei += "/" + rows[i][indexHinmei];
                }

                // 品名情報を結合した新しい行データを作成し、新データテーブルに追加する
                DataRow row = newDt.NewRow();
                for (int j = 0; j < row.ItemArray.Length; j++)
                {
                    if (j == indexHinmei)
                    {
                        // 格納する値に合わせて列の最大長を調整
                        if (newDt.Columns[indexHinmei].MaxLength < hinmei.Length)
                        {
                            newDt.Columns[indexHinmei].MaxLength = hinmei.Length;
                        }
                        row.SetField(indexHinmei, hinmei);
                    }
                    else
                    {
                        row.SetField(j, baseDt.Rows[indexZero][j]);
                    }
                }
                newDt.Rows.Add(row);

                // 処理を終えた列を元データテーブルから削除する
                for (int k = 0; k < rows.Length; k++)
                {
                    baseDt.Rows.Remove(rows[k]);
                }
            }

            return newDt;
        }

        /// <summary>
        /// 検索用SQLの作成
        /// </summary>
        private void MakeSearchCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //SQL文格納StringBuilder
                var sql = new StringBuilder();

                //SELECT句未取得なら検索できない
                if (string.IsNullOrEmpty(this.selectQuery))
                {
                    this.headForm.readDataNumber.Text = "0";
                    this.searchSql = string.Empty;
                    return;
                }

                #region SELECT句
                sql.Append(" SELECT DISTINCT ");
                // 出力パターンよりのSQL
                sql.Append(this.selectQuery);
                // システムID
                sql.AppendFormat(" ,T_TEIKI_HAISHA_ENTRY.SYSTEM_ID AS {0} ", ConstCls.HIDDEN_COLUMN_SYSTEM_ID);
                // 枝番
                sql.AppendFormat(" ,T_TEIKI_HAISHA_ENTRY.SEQ AS {0} ", ConstCls.HIDDEN_COLUMN_SEQ);
                // 定期配車番号
                sql.AppendFormat(" ,T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER AS {0} ", ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER);
                // 作業日付
                sql.AppendFormat(" ,T_TEIKI_HAISHA_ENTRY.SAGYOU_DATE AS {0}", ConstCls.HIDDEN_COLUMN_SAGYOU_DATE);

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    // 明細システムID
                    sql.AppendFormat(" ,T_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID AS {0} ", ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID);
                    // 行番号
                    sql.AppendFormat(" ,T_TEIKI_HAISHA_DETAIL.ROW_NUMBER AS {0} ", ConstCls.HIDDEN_COLUMN_DETAIL_ROW_NUMBER);
                }
                // mapbox連携
                sql.Append(", LOCATION_IS_NULL.LOCATION AS HIDDEN_LOCATION");

                #endregion

                #region FROM,JOIN句

                sql.Append(" FROM T_TEIKI_HAISHA_ENTRY ");

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.DENPYOU)
                {
                    // ろじコン連携
                    sql.Append(" LEFT JOIN");
                    sql.Append(" (SELECT DENPYOU_ATTR, REF_SYSTEM_ID, DELIVERY_NO FROM T_LOGI_DELIVERY_DETAIL GROUP BY DENPYOU_ATTR, REF_SYSTEM_ID, DELIVERY_NO) T_LOGI_DELIVERY_DETAIL");
                    sql.Append(" ON T_TEIKI_HAISHA_ENTRY.SYSTEM_ID = T_LOGI_DELIVERY_DETAIL.REF_SYSTEM_ID");
                    sql.Append(" AND T_LOGI_DELIVERY_DETAIL.DENPYOU_ATTR = 3");
                }

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    // 明細テーブル
                    sql.Append(" LEFT JOIN T_TEIKI_HAISHA_DETAIL ");
                    sql.Append(" ON T_TEIKI_HAISHA_ENTRY.SYSTEM_ID = T_TEIKI_HAISHA_DETAIL.SYSTEM_ID ");
                    sql.Append(" AND T_TEIKI_HAISHA_ENTRY.SEQ = T_TEIKI_HAISHA_DETAIL.SEQ ");

                    // 詳細テーブル
                    sql.Append(" LEFT JOIN T_TEIKI_HAISHA_SHOUSAI ");
                    sql.Append(" ON T_TEIKI_HAISHA_DETAIL.SYSTEM_ID = T_TEIKI_HAISHA_SHOUSAI.SYSTEM_ID ");
                    sql.Append(" AND T_TEIKI_HAISHA_DETAIL.SEQ = T_TEIKI_HAISHA_SHOUSAI.SEQ ");
                    sql.Append(" AND T_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID = T_TEIKI_HAISHA_SHOUSAI.DETAIL_SYSTEM_ID ");

                    // 荷降テーブル
                    sql.Append(" LEFT JOIN T_TEIKI_HAISHA_NIOROSHI ");
                    sql.Append(" ON T_TEIKI_HAISHA_DETAIL.SYSTEM_ID = T_TEIKI_HAISHA_NIOROSHI.SYSTEM_ID ");
                    sql.Append(" AND T_TEIKI_HAISHA_DETAIL.SEQ = T_TEIKI_HAISHA_NIOROSHI.SEQ ");
                    sql.Append(" AND T_TEIKI_HAISHA_SHOUSAI.NIOROSHI_NUMBER = T_TEIKI_HAISHA_NIOROSHI.NIOROSHI_NUMBER ");

                    // 必須項目（品名情報の加工）
                    sql.Append("LEFT JOIN M_HINMEI ON T_TEIKI_HAISHA_SHOUSAI.HINMEI_CD = M_HINMEI.HINMEI_CD ");
                    sql.Append("LEFT JOIN M_UNIT ON T_TEIKI_HAISHA_SHOUSAI.UNIT_CD = M_UNIT.UNIT_CD ");

                    // ろじコン連携
                    sql.Append(" LEFT JOIN T_LOGI_DELIVERY_DETAIL ON T_TEIKI_HAISHA_DETAIL.SYSTEM_ID = T_LOGI_DELIVERY_DETAIL.REF_SYSTEM_ID");
                    sql.Append(" AND T_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID = T_LOGI_DELIVERY_DETAIL.REF_DETAIL_SYSTEM_ID");
                    sql.Append(" AND T_LOGI_DELIVERY_DETAIL.DENPYOU_ATTR = 3");

                    // 現場メモEntry
                    sql.Append(" LEFT JOIN T_GENBAMEMO_ENTRY ");
                    sql.Append(" ON T_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID = T_GENBAMEMO_ENTRY.HASSEIMOTO_DETAIL_SYSTEM_ID ");
                    sql.Append(" AND T_GENBAMEMO_ENTRY.DELETE_FLG = 0");

                    // ｼｮｰﾄﾒｯｾｰｼﾞEntry
                    if (this.selectQuery.Contains("SMS送信"))
                    {
                        sql.Append(" LEFT JOIN T_SMS ");
                        sql.Append(" ON T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER = T_SMS.DENPYOU_NUMBER ");
                        sql.Append(" AND T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD = T_SMS.GYOUSHA_CD  ");
                        sql.Append(" AND T_TEIKI_HAISHA_DETAIL.GENBA_CD = T_SMS.GENBA_CD ");
                    }
                }

                // mapbox連携
                sql.Append(" LEFT JOIN (SELECT SYSTEM_ID, SEQ, 1 AS 'LOCATION' FROM T_TEIKI_HAISHA_DETAIL LEFT JOIN M_GENBA ON T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND T_TEIKI_HAISHA_DETAIL.GENBA_CD = M_GENBA.GENBA_CD WHERE M_GENBA.GENBA_LATITUDE IS NOT NULL AND M_GENBA.GENBA_LATITUDE != '') AS LOCATION_IS_NULL ON LOCATION_IS_NULL.SYSTEM_ID = T_TEIKI_HAISHA_ENTRY.SYSTEM_ID AND LOCATION_IS_NULL.SEQ = T_TEIKI_HAISHA_ENTRY.SEQ ");

                sql.Append(this.joinQuery);

                #endregion

                #region WHERE句

                sql.Append(" WHERE ");
                //削除フラグ
                sql.Append(" T_TEIKI_HAISHA_ENTRY.DELETE_FLG = 0 ");

                //画面で在拠点CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text)
                    && this.headForm.KYOTEN_CD.Text != ConstCls.KyouTenZenSya)
                {
                    sql.Append(" AND T_TEIKI_HAISHA_ENTRY.KYOTEN_CD = " + int.Parse(this.headForm.KYOTEN_CD.Text) + " ");
                }

                ////画面で日付選択が作業日付の場合
                //if (ConstCls.HidukeCD_DenPyou.Equals(this.headForm.txtNum_HidukeSentaku.Text))
                //{
                //    if (this.headForm.HIDUKE_FROM.Value != null)
                //    {
                //        sql.Append(" AND ENTRY.DENPYOU_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
                //    }
                //    if (this.headForm.HIDUKE_TO.Value != null)
                //    {
                //        sql.Append(" AND ENTRY.DENPYOU_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
                //    }
                //}

                //画面で日付選択が作業日付の場合
                if (ConstCls.HidukeCD_DenPyou.Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.SAGYOU_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
                    }
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.SAGYOU_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
                    }
                }

                //画面で日付選択が入力日付の場合
                if (ConstCls.HidukeCD_NyuuRyoku.Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.UPDATE_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
                    }
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.UPDATE_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
                    }
                }

                //画面条件
                if (this.form.pnlSearchString.Visible)
                {
                    //画面で在コースCDがnull無いの場合
                    if (!string.IsNullOrEmpty(this.form.txtCourseCd.Text))
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.COURSE_NAME_CD = '" + this.form.txtCourseCd.Text + "' ");
                    }
                    //画面で運転者CDがnull無いの場合
                    if (!string.IsNullOrEmpty(this.form.txtSHAIN_CD.Text))
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.UNTENSHA_CD = '" + this.form.txtSHAIN_CD.Text + "' ");
                    }
                    //画面で補助員CDがnull無いの場合
                    if (!string.IsNullOrEmpty(this.form.txtSHoujyosyaCd.Text))
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.HOJOIN_CD = '" + this.form.txtSHoujyosyaCd.Text + "' ");
                    }
                    //画面で車種CDがnull無いの場合
                    if (!string.IsNullOrEmpty(this.form.txtSHASHU_CD.Text))
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.SHASHU_CD = '" + this.form.txtSHASHU_CD.Text + "' ");
                    }
                    //画面で車輌CDがnull無いの場合
                    if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.SHARYOU_CD = '" + this.form.SHARYOU_CD.Text + "' ");
                    }
                    //画面で運搬業者CDがnullではない場合
                    if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                    {
                        sql.Append(" AND T_TEIKI_HAISHA_ENTRY.UNPAN_GYOUSHA_CD = '" + this.form.UNPAN_GYOUSHA_CD.Text + "' ");
                    }
                }

                #endregion

                #region ORDERBY句

                var template = " ,\"{0}\" ASC ";

                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
                //システムID
                sql.AppendFormat(template, ConstCls.HIDDEN_COLUMN_SYSTEM_ID);
                //枝番
                sql.AppendFormat(template, ConstCls.HIDDEN_COLUMN_SEQ);
                //定期配車番号
                sql.AppendFormat(template, ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER);
                //作業日付
                sql.AppendFormat(template, ConstCls.HIDDEN_COLUMN_SAGYOU_DATE);

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    // 明細システムID
                    sql.AppendFormat(template, ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID);
                }

                #endregion

                this.searchSql = sql.ToString();
                sql.Append("");
            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeSearchCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// DataGridViewのプロパティ再設定
        /// </summary>
        private void setDataGridView()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                setDataGridViewColumn(this.form.customDataGridView1);
                setDataGridViewColumn(this.form.customDataGridViewCSV);
                this.form.customDataGridView1.AllowUserToResizeRows = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDataGridView", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        //ThangNguyen Add 2015030 #11101 Start
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        public void HeaderCheckBoxSupport()
        {
            LogUtility.DebugMethodStart();

            DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            newColumn.Name = ConstCls.ADD_COLUMN_INSATSU_NAME;
            newColumn.Width = 80;
            newColumn.MinimumWidth = 80;

            DatagridViewCheckBoxHeaderCell newheader = new DatagridViewCheckBoxHeaderCell(0, 2);
            newColumn.HeaderCell = newheader;
            newColumn.ReadOnly = false;

            if (this.form.customDataGridView1.Columns.Count > 0)
            {
                this.form.customDataGridView1.Columns.Insert(0, newColumn);
            }
            else
            {
                this.form.customDataGridView1.Columns.Add(newColumn);
            }
            this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";

            LogUtility.DebugMethodEnd();
        }
        //ThangNguyen Add 2015030 #11101 End

        /// <summary>
        /// Column非表示設定
        /// </summary>
        /// <param name="dtgv"></param>
        private void setDataGridViewColumn(CustomDataGridView dtgv)
        {
            try
            {
                LogUtility.DebugMethodStart(dtgv);
                //ThangNguyen Delete 2015030 #11101 Start
                if (dtgv.Columns.Contains(ConstCls.ADD_COLUMN_INSATSU_NAME))
                {
                    dtgv.Columns[ConstCls.ADD_COLUMN_INSATSU_NAME].Width = 80;
                }
                //ThangNguyen Delete 2015030 #11101 End
                //入力画面へ遷移用Column（システムID）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_COLUMN_SYSTEM_ID))
                {
                    dtgv.Columns[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Visible = false;
                }

                //入力画面へ遷移用Column（枝番）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_COLUMN_SEQ))
                {
                    dtgv.Columns[ConstCls.HIDDEN_COLUMN_SEQ].Visible = false;
                }
                //入力画面へ遷移用Column（定期配車番号）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER))
                {
                    dtgv.Columns[ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER].Visible = false;
                }
                //入力画面へ遷移用Column（作業日）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_COLUMN_SAGYOU_DATE))
                {
                    dtgv.Columns[ConstCls.HIDDEN_COLUMN_SAGYOU_DATE].Visible = false;
                }
                //品名情報結合用Column（明細システムID）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID))
                {
                    dtgv.Columns[ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID].Visible = false;
                }
                //MAPBOX用Column（緯度経度有無）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_LOCATION))
                {
                    dtgv.Columns[ConstCls.HIDDEN_LOCATION].Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDataGridViewColumn", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// SQLを発行し取得されたDataTableをFormに設定する
        /// </summary>
        public DataTable GetColumnHeaderOnlyDataTable()
        {
            var dataTable = new DataTable();
            //検索文再設定
            //MakeSearchCondition();
            if (this.selectQuery != null)
            {
                string[] header = this.selectQuery.Split('"');
                //dataTable.Columns.Add(new DataColumn(ConstCls.ADD_COLUMN_INSATSU_NAME));  //ThangNguyen Delete 2015030 #11101
                for (int i = 1; i < header.Length; i = i + 2)
                //foreach (var pattern in header)
                {
                    dataTable.Columns.Add(new DataColumn(header[i]));
                }
            }

            return dataTable;
        }

        #endregion

        #region Functionボタン 押下処理
        /// <summary>
        /// F1 簡易検索／汎用検索→仕様変更：F1 定期実績
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //var parentForm = (BusinessBaseForm)this.form.Parent;

                //if (this.form.pnlSearchString.Visible == true)
                //{
                //    this.form.pnlSearchString.Visible = false;
                //    parentForm.bt_process2.Text = "[2]検索条件設定";
                //    parentForm.bt_process2.Enabled = true;
                //    this.form.searchString.Visible = true;
                //    parentForm.bt_func1.Text = "[F1]\r\n簡易検索";

                //}
                //else if (this.form.searchString.Visible == true)
                //{
                //    this.form.searchString.Visible = false;
                //    this.form.pnlSearchString.Visible = true;
                //    parentForm.bt_process2.Text = "";
                //    parentForm.bt_process2.Enabled = false;
                //    parentForm.bt_func1.Text = "[F1]\r\n汎用検索";
                //}

                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E076");
                }
                else
                {
                    //選択されたレコードを取得する
                    DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
                    //定期配車番号に設定
                    string chouseiNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER].Value.ToString();

                    //ＤＢ存在チックを行う
                    if (!IsExistData(chouseiNumber))
                    {
                        //定期配車番号が登録されていない場合
                        return;
                    }

                    if (!RenkeiCheck(chouseiNumber))
                    {
                        return;
                    }

                    //ＤＢ存在チックを行う
                    if (!IsExistDataInJiseki(chouseiNumber))
                    {
                        //定期配車番号がすでに定期配車実績データに紐づけられているかをチェック
                        return;
                    }

                    //定期配車実績入力画面へ遷移する（新規モード）
                    FormManager.OpenFormWithAuth("G289", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, chouseiNumber, true);
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        /// <summary>
        /// F2 新規
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //入力画面へ遷移する（新規モード）
                forwardNyuuryoku(WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func2_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E076");
                }
                else
                {
                    //入力画面へ遷移する（修正モード）
                    forwardNyuuryoku(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func3_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E076");
                }
                else
                {
                    //入力画面へ遷移する（削除モード）
                    forwardNyuuryoku(WINDOW_TYPE.DELETE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func4_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        ///// <summary>
        ///// F5 複写
        ///// </summary>
        ///// <param name="sender">object</param>
        ///// <param name="e">Syste.EventArgs</param>
        //private void bt_func5_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        //一覧に明細行がない場合
        //        if (this.form.customDataGridView1.RowCount == 0)
        //        {
        //            //アラートを表示し、画面遷移しない
        //            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //            msgLogic.MessageBoxShow("E076");
        //        }
        //        else
        //        {
        //            //入力画面へ遷移する（新規モード）
        //            forwardNyuuryoku(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("bt_func5_Click", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = null;

                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、CSV出力処理はしない
                    msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");
                }
                else
                {
                    //CSV出力確認メッセージを表示する
                    msgLogic = new MessageBoxShowLogic();
                    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        //共通部品を利用して、画面に表示されているデータをCSVに出力する
                        CSVExport CSVExport = new CSVExport();
                        CSVExport.ConvertCustomDataGridViewToCsv(this.form.customDataGridViewCSV, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN), this.form);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F7 条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                ////拠点CD、拠点 : ブランク
                //this.headForm.KYOTEN_CD.Text = string.Empty;
                //this.headForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

                ////伝票日付RadioButton選択状態
                //this.headForm.txtNum_HidukeSentaku.Text = ConstCls.HidukeCD_NyuuRyoku;
                //this.headForm.radbtnDenpyouHiduke.Checked = true;

                ////日付(from) ：当月の1日
                ////DateTime hidukeStart = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                ////this.headForm.HIDUKE_FROM.Value = hidukeStart;
                this.headForm.HIDUKE_FROM.Value = this.parentForm.sysDate;

                ////日付(to): 当月の末日
                ////DateTime hidukeEnd = hidukeStart.AddMonths(1).AddDays(-1);
                ////this.headForm.HIDUKE_TO.Value = hidukeEnd;
                this.headForm.HIDUKE_TO.Value = this.parentForm.sysDate;

                //コースCD、コース ： ブランク                
                this.form.txtCourseCd.Text = string.Empty;
                this.form.txtCourseNm.Text = string.Empty;

                //運転者CD、運転者 : ブランク
                this.form.txtSHAIN_CD.Text = string.Empty;
                this.form.txtSHAIN_NAME.Text = string.Empty;

                //補助員CD、補助員 : ブランク               
                this.form.txtSHoujyosyaCd.Text = string.Empty;
                this.form.txtSHoujyosyaNm.Text = string.Empty;

                //車種CD、車種 : ブランク
                this.form.txtSHASHU_CD.Text = string.Empty;
                this.form.txtSHASHU_NAME.Text = string.Empty;

                //車輌CD、車輌 : ブランク               
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.form.preSyaryouCD = this.form.SHARYOU_CD.Text;
                //this.form.txtGosyaCDHidden.Text = string.Empty;                //汎用検索 : ブランク  
                this.form.searchString.Text = string.Empty;

                //運搬業者CD、運搬業者名 : ブランク
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;

                //並び順をクリア
                this.form.customSortHeader1.ClearCustomSortSetting(); // No.2292
                //フィルタをクリア
                this.form.customSearchHeader1.ClearCustomSearchSetting();

                //フォーカス移動
                if (this.form.searchString.Visible == true)
                {
                    this.form.searchString.Focus();
                }
                else
                {
                    this.form.txtSHAIN_CD.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func7_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F8検索
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.form.PatternNo == 0)
                {
                    var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                var autoCheckLogic = new AutoRegistCheckLogic(this.form.GetAllControl(), this.form.GetAllControl());
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                // エラーの場合
                if (this.form.RegistErrorFlag)
                {
                    // 処理中止
                    return;
                }
                //検索処理を行う
                this.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 現場メモ登録
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            DataTable dt = null;

            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row != null)
            {
                // パラメータを設定する。
                T_GENBAMEMO_ENTRY paramEntry = new T_GENBAMEMO_ENTRY();
                paramEntry.HASSEIMOTO_CD = "5";
                paramEntry.HASSEIMOTO_NAME = "定期配車";
                paramEntry.HASSEIMOTO_SYSTEM_ID = System.Data.SqlTypes.SqlInt64.Parse(row.Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString());
                var isDetail = this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI;
                if (isDetail)
                {
                    paramEntry.HASSEIMOTO_DETAIL_SYSTEM_ID = System.Data.SqlTypes.SqlInt64.Parse(row.Cells[ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID].Value.ToString());
                }

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    string systemId = row.Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString();
                    string SEQ = row.Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value.ToString();
                    string detailSystemId = row.Cells[ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID].Value.ToString();

                    var chkSql = new StringBuilder();
                    chkSql.Append(" SELECT T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD, M_GYOUSHA.GYOUSHA_NAME_RYAKU, T_TEIKI_HAISHA_DETAIL.GENBA_CD, M_GENBA.GENBA_NAME_RYAKU FROM T_TEIKI_HAISHA_DETAIL ");
                    chkSql.Append(" LEFT JOIN M_GYOUSHA ON T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                    chkSql.Append(" LEFT JOIN M_GENBA ON T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD = M_GENBA.GYOUSHA_CD ");
                    chkSql.Append("                  AND T_TEIKI_HAISHA_DETAIL.GENBA_CD = M_GENBA.GENBA_CD ");
                    chkSql.Append(" WHERE T_TEIKI_HAISHA_DETAIL.SYSTEM_ID = '" + systemId + "' AND T_TEIKI_HAISHA_DETAIL.SEQ = " + SEQ + " AND T_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID = '" + detailSystemId + "' ");

                    dt = this.mDetailDao.GetDateForStringSql(chkSql.ToString());

                    if (dt.Rows.Count != 0)
                    {
                        paramEntry.GYOUSHA_CD = dt.Rows[0]["GYOUSHA_CD"].ToString();
                        paramEntry.GYOUSHA_NAME = dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                        paramEntry.GENBA_CD = dt.Rows[0]["GENBA_CD"].ToString();
                        paramEntry.GENBA_NAME = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                    }
                }

                WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                FormManager.OpenFormWithAuth("G741", windowType, windowType, string.Empty, string.Empty, paramEntry, WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN.ToString());
            }
            else
            {
                // 明細が未選択の場合は、エラーとする。
                msgLogic.MessageBoxShow("E051", "対象データ");
                return;
            }

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// F10並び替え
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func10_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.form.customDataGridView1 != null)
            {
                this.headForm.readDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headForm.readDataNumber.Text = "0";
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F5配車表
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                parentForm.bt_func5.Click -= new System.EventHandler(this.bt_func5_Click);

                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、帳票出力しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");
                    return;
                }

                List<string> lstTeikiHaishaNo = new List<string>();
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    // Row取得
                    DataGridViewRow row = this.form.customDataGridView1.Rows[i];
                    if (row.Cells[ConstCls.ADD_COLUMN_INSATSU_NAME].Value != null
                        && row.Cells[ConstCls.ADD_COLUMN_INSATSU_NAME].Value.ToString() == "True")
                    {
                        //重複データ除く
                        if (!lstTeikiHaishaNo.Contains(row.Cells[ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER].Value.ToString()))
                        {
                            lstTeikiHaishaNo.Add(row.Cells[ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER].Value.ToString());
                        }
                    }
                }

                //一覧に明細行が選択ない場合
                if (lstTeikiHaishaNo.Count == 0)
                {
                    //アラートを表示し、帳票出力しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");
                    return;
                }

                //bool isLayoutV = reportInfo.IsLayoutV();
                //帳票出力処理
                for (int j = 0; j < lstTeikiHaishaNo.Count; j++)
                {
                    ReportInfoR450_R551 reportInfo = new ReportInfoR450_R551(WINDOW_ID.R_UNTEN_NIPPOU, this.mDetailDao);
                    reportInfo.TeikiHaishaNo = lstTeikiHaishaNo[j];
                    reportInfo.Title = WINDOW_ID.R_UNTEN_NIPPOU.ToTitleString();

                    // 運転日報の縦か否か
                    string projectId = string.Empty;
                    if (reportInfo.IsLayoutV())
                    {   // 縦
                        reportInfo.Create(@".\Template\R450_R551-Form.xml", "LAYOUT1", new DataTable());
                        projectId = "R450";
                    }
                    else
                    {   // 横
                        reportInfo.Create(@".\Template\R450_R551-Form.xml", "LAYOUT2", new DataTable());
                        projectId = "R551";
                    }

                    using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(reportInfo, projectId, WINDOW_ID.R_UNTEN_NIPPOU))
                    {
                        formReportPrintPopup.PrintInitAction = 3;
                        formReportPrintPopup.PrintXPS();
                        //formReportPrintPopup.ShowDialog();
                        //formReportPrintPopup.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func11_Click", ex);
                throw;
            }
            finally
            {
                parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 明細データダブルクリックイベント

        private void customDataGridView1_MouseDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                CustomDataGridView customDataGridView = (CustomDataGridView)sender;

                if (customDataGridView.RowCount != 0 && e.RowIndex > -1)
                {
                    customDataGridView.CurrentCell = customDataGridView.Rows[e.RowIndex].Cells[0];

                    //入力画面へ遷移する（修正モード）
                    forwardNyuuryoku(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("customDataGridView1_MouseDoubleClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 入力画面へ遷移する
        /// </summary>
        private void forwardNyuuryoku(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                //システムIDを初期化
                String systemID = String.Empty;
                //枝番を初期化
                String seq = String.Empty;
                //伝票番号を初期化
                String chouseiNumber = String.Empty;
                //作業日付を初期化
                String sagyoDate = String.Empty;

                //修正、削除、複写の場合
                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(windowType)
                    || WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(windowType)
                     || WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(windowType))
                {
                    //選択されたレコードを取得する
                    DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
                    //システムIDに設定
                    systemID = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString();
                    //枝番Dに設定
                    seq = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value.ToString();
                    //定期配車番号に設定
                    chouseiNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER].Value.ToString();
                    //作業日付に設定
                    sagyoDate = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_COLUMN_SAGYOU_DATE].Value.ToString();

                    //ＤＢ存在チックを行う
                    if (!IsExistData(chouseiNumber))
                    {
                        //定期配車番号が登録されていない場合
                        return;
                    }
                }

                if (WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(windowType))
                {
                    //複写の場合、新規モードを設定する
                    windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                }

                // 権限チェック
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                   !r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    if (!r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                        return;
                    }

                    windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }

                FormManager.OpenFormWithAuth("G030", windowType, windowType, chouseiNumber);

            }
            catch (Exception ex)
            {
                LogUtility.Error("forwardNyuuryoku", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        //2013/12/04 仕様変更対応 追加 start
        #region 指定した定期配車番号のデータが存在するか返す
        /// <summary>
        /// 指定した定期配車番号のデータが存在するか返す
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistData(string uketsukeNumber)
        {
            // 戻り値初期化
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart(uketsukeNumber);

                if (!string.IsNullOrEmpty(uketsukeNumber))
                {
                    //SQL文格納StringBuilder
                    var chkSql = new StringBuilder();
                    chkSql.Append(" SELECT * FROM ");

                    //受付（収集）入力
                    chkSql.Append(" T_TEIKI_HAISHA_ENTRY AS ENTRY ");
                    chkSql.Append(" WHERE ");
                    chkSql.Append(" ENTRY.TEIKI_HAISHA_NUMBER = " + uketsukeNumber);
                    chkSql.Append(" AND ENTRY.DELETE_FLG = 0 ");

                    // データを検索
                    var dtReturn = this.mDetailDao.GetDateForStringSql(chkSql.ToString());

                    // 0件の場合
                    if (dtReturn.Rows.Count > 0)
                    {
                        // 戻り値
                        returnVal = true;
                    }
                    else
                    {
                        //アラートを表示し、画面遷移しない
                        returnVal = false;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E045");
                    }
                }
                LogUtility.DebugMethodEnd(returnVal);

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion
        //2013/12/04 仕様変更対応 追加 end

        //2014/01/29 仕様変更対応 追加 start
        #region 定期実績入力テーブルのデータが存在するか返す
        /// <summary>
        /// 定期実績入力テーブルのデータが存在するか返す
        /// </summary>
        /// <param name="uketsukeNumber"></param>
        /// <returns></returns>
        internal bool IsExistDataInJiseki(string uketsukeNumber)
        {
            // 戻り値初期化
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart(uketsukeNumber);

                if (!string.IsNullOrEmpty(uketsukeNumber))
                {
                    //SQL文格納StringBuilder
                    var chkSql = new StringBuilder();
                    chkSql.Append(" SELECT * FROM ");

                    //受付（収集）入力
                    chkSql.Append(" T_TEIKI_JISSEKI_ENTRY AS ENTRY ");
                    chkSql.Append(" WHERE ");
                    chkSql.Append(" ENTRY.TEIKI_HAISHA_NUMBER = " + uketsukeNumber);
                    chkSql.Append(" AND ENTRY.DELETE_FLG = 0 ");

                    // データを検索
                    var dtReturn = this.mDetailDao.GetDateForStringSql(chkSql.ToString());

                    // 0件の場合
                    if (dtReturn.Rows.Count == 0)
                    {
                        // 該当データなし
                        returnVal = true;
                    }
                    else
                    {
                        // 該当データあり
                        returnVal = false;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        //E022 「入力された定期配車番号はすでに登録されています。」
                        msgLogic.MessageBoxShow("E022", "入力された定期配車番号");
                    }
                }
                LogUtility.DebugMethodEnd(returnVal);

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion
        //2014/01/29 仕様変更対応 追加 end
        #endregion

        #region 明細データクリックイベント

        /// <summary>
        /// 明細データクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                DataGridViewCheckBoxCell cbxType = new DataGridViewCheckBoxCell();
                //if (e.RowIndex >= 0 && this.form.customDataGridView1.Columns[e.ColumnIndex].ValueType == cbxType.ValueType)
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    //checkbox選択
                    cbxType = (DataGridViewCheckBoxCell)this.form.customDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cbxType.Value == null)
                    {
                        cbxType.Value = true;
                    }
                    else
                    {
                        cbxType.Value = !(bool)cbxType.Value;
                    }
                }

                if (e.RowIndex < 0 || e.ColumnIndex != 1)
                {
                    return;
                }

                // チェックした行の曜日とコース名CDを取得
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                string systemId = Convert.ToString(row.Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value);
                string seq = Convert.ToString(row.Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value);

                //スペースで、OFFの場合は抜ける
                if (this.SpaceChk && !this.SpaceON)
                {
                    // チェックNULL(OFF)⇒ONにする時の処理
                    foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                    {
                        // 選択行は変更しない
                        if (dgvRow.Index.Equals(e.RowIndex)) { continue; }

                        // チェックした行と同一コースにチェックをつける
                        if (dgvRow.Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString() == systemId &&
                            dgvRow.Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value.ToString() == seq)
                        {
                            if (AppConfig.AppOptions.IsMAPBOX())
                            {
                                dgvRow.Cells[ConstCls.DATA_TAISHO].Value = false;
                            }
                        }
                    }
                    this.form.customDataGridView1.RefreshEdit();
                    this.form.customDataGridView1.Refresh();
                    return;
                }
                this.SpaceON = false;

                if (this.form.customDataGridView1.CurrentCell.Value == null)
                {
                    if (Convert.ToString(row.Cells[ConstCls.HIDDEN_LOCATION].Value) != "1")
                    {
                        this.MsgBox.MessageBoxShowError("緯度経度が登録されているデータがないため、地図を表示できません。");
                        if (!this.SpaceChk)
                        {
                            if (AppConfig.AppOptions.IsMAPBOX())
                            {
                                row.Cells[ConstCls.DATA_TAISHO].Value = true;
                            }
                        }
                        this.SpaceChk = false;
                        return;
                    }
                    if (this.SpaceChk)
                    {
                        if (this.form.customDataGridView1[1, e.RowIndex].Value == null)
                        {
                            this.form.customDataGridView1[1, e.RowIndex].Value = true;
                        }
                        else
                        {
                            this.form.customDataGridView1[1, e.RowIndex].Value = !(bool)this.form.customDataGridView1[1, e.RowIndex].Value;
                        }
                        this.SpaceChk = false;
                    }

                    // チェックNULL(OFF)⇒ONにする時の処理
                    foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                    {
                        // 選択行は変更しない
                        if (dgvRow.Index.Equals(e.RowIndex)) { continue; }

                        // チェックした行と同一コースにチェックをつける
                        if (dgvRow.Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString() == systemId &&
                            dgvRow.Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value.ToString() == seq)
                        {
                            if (AppConfig.AppOptions.IsMAPBOX())
                            {
                                dgvRow.Cells[ConstCls.DATA_TAISHO].Value = true;
                            }
                        }
                    }
                }
                else if (this.form.customDataGridView1.CurrentCell.Value.Equals(false))
                {
                    if (Convert.ToString(row.Cells[ConstCls.HIDDEN_LOCATION].Value) != "1")
                    {
                        this.MsgBox.MessageBoxShowError("緯度経度が登録されているデータがないため、地図を表示できません。");
                        if (!this.SpaceChk)
                        {
                            if (AppConfig.AppOptions.IsMAPBOX())
                            {
                                row.Cells[ConstCls.DATA_TAISHO].Value = true;
                            }
                        }
                        this.SpaceChk = false;
                        return;
                    }
                    if (this.SpaceChk)
                    {
                        if (this.form.customDataGridView1[1, e.RowIndex].Value == null)
                        {
                            this.form.customDataGridView1[1, e.RowIndex].Value = true;
                        }
                        else
                        {
                            this.form.customDataGridView1[1, e.RowIndex].Value = !(bool)this.form.customDataGridView1[1, e.RowIndex].Value;
                        }
                        this.SpaceChk = false;
                    }

                    // チェックOFF⇒ONにする時の処理
                    foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                    {
                        // 選択行は変更しない
                        if (dgvRow.Index.Equals(e.RowIndex)) { continue; }

                        // チェックした行と同一コースにチェックをつける
                        if (dgvRow.Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString() == systemId &&
                            dgvRow.Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value.ToString() == seq)
                        {
                            if (AppConfig.AppOptions.IsMAPBOX())
                            {
                                dgvRow.Cells[ConstCls.DATA_TAISHO].Value = true;
                            }
                        }
                    }
                }
                else
                {
                    // チェックON⇒OFFにする時の処理
                    foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                    {
                        // 選択行は変更しない
                        if (dgvRow.Index.Equals(e.RowIndex)) { continue; }

                        // チェックした行と同一コースにチェックをつける
                        if (dgvRow.Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString() == systemId &&
                            dgvRow.Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value.ToString() == seq)
                        {
                            if (AppConfig.AppOptions.IsMAPBOX())
                            {
                                dgvRow.Cells[ConstCls.DATA_TAISHO].Value = false;
                            }
                        }
                    }
                }

                this.form.customDataGridView1.RefreshEdit();
                this.form.customDataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                LogUtility.Error("customDataGridView1_CellClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        private void customDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.form.customDataGridView1.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                DatagridViewCheckBoxHeaderCell header = col.HeaderCell as DatagridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                    this.form.customDataGridView1.Refresh();
                }
            }
        }

        #region プロセスボタン押下処理（※処理未実装）
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var sysID = this.form.OpenPatternIchiran();

                this.selectQuery = this.form.SelectQuery;
                this.orderByQuery = this.form.OrderByQuery;
                this.joinQuery = this.form.JoinQuery;

                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.searchResult = this.form.Table;
                    this.form.ShowData(this.searchResult);
                }
                //20151023 hoanghm #13638 start
                if (!this.form.customDataGridView1.Columns.Contains(ConstCls.ADD_COLUMN_INSATSU_NAME))
                {
                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.HeaderCheckBoxSupport();
                    }
                }
                //20151023 hoanghm #13638 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 現場メモ一覧画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            DataTable dt = null;

            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row != null)
            {
                // パラメータを設定する。
                T_GENBAMEMO_ENTRY paramEntry = new T_GENBAMEMO_ENTRY();
                paramEntry.HASSEIMOTO_CD = "5";
                paramEntry.HASSEIMOTO_NAME = "定期配車";
                paramEntry.HASSEIMOTO_SYSTEM_ID = System.Data.SqlTypes.SqlInt64.Parse(row.Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString());
                var isDetail = this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI;
                if (isDetail)
                {
                    paramEntry.HASSEIMOTO_DETAIL_SYSTEM_ID = System.Data.SqlTypes.SqlInt64.Parse(row.Cells[ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID].Value.ToString());
                }

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    string systemId = row.Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString();
                    string SEQ = row.Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value.ToString();
                    string detailSystemId = row.Cells[ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID].Value.ToString();

                    var chkSql = new StringBuilder();
                    chkSql.Append(" SELECT T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD, M_GYOUSHA.GYOUSHA_NAME_RYAKU, T_TEIKI_HAISHA_DETAIL.GENBA_CD, M_GENBA.GENBA_NAME_RYAKU FROM T_TEIKI_HAISHA_DETAIL ");
                    chkSql.Append(" LEFT JOIN M_GYOUSHA ON T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                    chkSql.Append(" LEFT JOIN M_GENBA ON T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD = M_GENBA.GYOUSHA_CD ");
                    chkSql.Append("                  AND T_TEIKI_HAISHA_DETAIL.GENBA_CD = M_GENBA.GENBA_CD ");
                    chkSql.Append(" WHERE T_TEIKI_HAISHA_DETAIL.SYSTEM_ID = '" + systemId + "' AND T_TEIKI_HAISHA_DETAIL.SEQ = " + SEQ + " AND T_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID = '" + detailSystemId + "' ");

                    dt = this.mDetailDao.GetDateForStringSql(chkSql.ToString());

                    if (dt.Rows.Count != 0)
                    {
                        paramEntry.GYOUSHA_CD = dt.Rows[0]["GYOUSHA_CD"].ToString();
                        paramEntry.GYOUSHA_NAME = dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                        paramEntry.GENBA_CD = dt.Rows[0]["GENBA_CD"].ToString();
                        paramEntry.GENBA_NAME = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                    }
                }

                FormManager.OpenFormWithAuth("G742", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, paramEntry, WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN.ToString());

            }
            else
            {
                // 明細が未選択の場合は、画面遷移のみとする。
                FormManager.OpenFormWithAuth("G742", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }

            LogUtility.DebugMethodEnd();
        }
        
        /// <summary>
        /// 検索条件設定画面へ遷移
        /// </summary>
        private void bt_process3_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 件数チェック
                if (!this.CheckForCheckBox())
                {
                    return;
                }

                if (this.MsgBox.MessageBoxShowConfirm("地図を表示しますか？" +
                    Environment.NewLine + "※緯度/経度が登録されていない現場は表示されません。") == DialogResult.No)
                {
                    return;
                }

                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                // 地図に渡すDTO作成
                List<mapDtoList> dtos = new List<mapDtoList>();
                dtos = this.createMapboxDto();
                if (dtos.Count == 0)
                {
                    this.MsgBox.MessageBoxShowError("表示する対象がありません。");
                    return;
                }

                // 地図表示
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process2_Click", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ入力画面へ遷移
        /// </summary>
        private void bt_process4_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す伝票情報
                string[] smsparamList = new string[7];
                // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す受信者リスト
                List<int> smsReceiverList = new List<int>();

                DataTable getDataDt = new DataTable();

                var row = this.form.customDataGridView1.CurrentRow;
                if (null != row)
                {
                    if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN != (int)OUTPUT_KBN.MEISAI)
                    {
                        this.MsgBox.MessageBoxShowError("ショートメッセージ機能を使用できません。\r\nパターン出力区分＝2.明細　のパターンで検索を行ってください。");
                        return;
                    }

                    // 伝票種類は「4．定期」固定
                    smsparamList[0] = "4";

                    // SEQ、受付番号、行番号は先に設定
                    smsparamList[1] = Convert.ToString(row.Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value);
                    smsparamList[2] = Convert.ToString(row.Cells[ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER].Value);
                    smsparamList[6] = Convert.ToString(row.Cells[ConstCls.HIDDEN_COLUMN_DETAIL_ROW_NUMBER].Value);

                    // 必要情報を取得するために明細システムIDを取得
                    string detailSysId = Convert.ToString(row.Cells[ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID].Value);

                    string getDataSql = "SELECT * FROM T_TEIKI_HAISHA_ENTRY HE ";
                    getDataSql += "LEFT JOIN T_TEIKI_HAISHA_DETAIL HD ";
                    getDataSql += "ON HE.TEIKI_HAISHA_NUMBER = HD.TEIKI_HAISHA_NUMBER ";
                    getDataSql += string.Format("WHERE HE.SEQ = '{0}' AND HD.SEQ = '{0}' ", smsparamList[1]);
                    getDataSql += string.Format("AND HD.DETAIL_SYSTEM_ID = '{0}' ", detailSysId);
                    getDataSql += string.Format("AND HE.TEIKI_HAISHA_NUMBER = '{0}' ", smsparamList[2]);
                    getDataSql += string.Format("AND HD.ROW_NUMBER = '{0}' ", smsparamList[6]);

                    // SEQ、明細システムID、定期配車番号を元に情報取得
                    getDataDt = this.mDetailDao.GetDateForStringSql(getDataSql);
                    if (getDataDt.Rows.Count == 0)
                    {
                        this.MsgBox.MessageBoxShow("E045");
                        return;
                    }

                    // ｼｮｰﾄﾒｯｾｰｼﾞ受信者チェック
                    var dao = this.smsReceiverLinkGenbaDao.CheckDataForSmsNyuuryoku(getDataDt.Rows[0]["GYOUSHA_CD"].ToString(), getDataDt.Rows[0]["GENBA_CD"].ToString());
                    if(dao == null)
                    {
                        this.MsgBox.MessageBoxShowError("現場入力（マスタ）に受信者情報が登録されていません。\r\n受信者情報を登録してください。");
                        return;
                    }
                
                    smsparamList[3] = getDataDt.Rows[0]["GYOUSHA_CD"].ToString();
                    smsparamList[4] = getDataDt.Rows[0]["GENBA_CD"].ToString();
                    smsparamList[5] = ((DateTime)getDataDt.Rows[0]["SAGYOU_DATE"]).ToString("yyyy/MM/dd(ddd)");
                
                    smsReceiverList = this.SmsReceiverListSetting(smsparamList[3], smsparamList[4]);

                    // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面を起動
                    FormManager.OpenForm("G767", smsReceiverList, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN, smsparamList);
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E076");
                }
            }
            catch(Exception ex)
            {
                LogUtility.Error("bt_process4_Click", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト取得
        /// </summary>
        /// <returns></returns>
        private List<int> SmsReceiverListSetting(string gyoushaCd, string genbaCd)
        {
            List<int> list = null;
            List<M_SMS_RECEIVER_LINK_GENBA> smsReceiverLink = null;

            // 選択行の値を参照
            smsReceiverLink = this.smsReceiverLinkGenbaDao.GetDataForSmsNyuuryoku(gyoushaCd, genbaCd);
            
            if (smsReceiverLink != null)
            {
                list = smsReceiverLink.Select(n => n.SYSTEM_ID.Value).ToList();
            }

            return list;
        }
        
        #endregion

        #region 汎用検索(SearchString)内でのエンターキー押下イベント
        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void SearchStringKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(this.form.searchString.Text))
                    {
                        string getSearchString = this.form.searchString.Text.Replace("\r", "").Replace("\n", "");
                        this.searchString = getSearchString;
                        this.Search();

                    }
                    else
                    {
                        this.form.searchString.Text = "";
                        this.form.searchString.SelectionLength = this.form.searchString.Text.Length;
                        this.form.searchString.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchStringKeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        #endregion

        #region ESCキー押下イベント
        //2013/12/05 削除　start
        //void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //2013/12/05 削除 製造課題一覧(全体)No28 start
        //try
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    var parentForm = (BusinessBaseForm)this.form.Parent;

        //    if (e.KeyCode == Keys.Escape)
        //    {
        //        //処理No(ESC)へカーソル移動
        //        parentForm.txb_process.Focus();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    LogUtility.Error("form_PreviewKeyDown", ex);
        //    throw;
        //}
        //finally
        //{
        //    LogUtility.DebugMethodEnd();
        //}
        //}
        //2013/12/05 削除 製造課題一覧(全体)No28 end
        #endregion

        #region ｺｰｽポップアップ

        /// <summary>
        /// ｺｰｽ情報 ポップアップ初期化
        /// </summary>
        public void PopUpDataInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // ｺｰｽ情報 ポップアップ取得
                // 表示用データ取得＆加工
                var ShainDataTable = this.GetPopUpData(this.form.txtCourseCd.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
                // TableNameを設定すれば、ポップアップのタイトルになる
                ShainDataTable.TableName = "ｺｰｽ名称情報";

                // 列名とデータソース設定
                this.form.txtCourseCd.PopupDataHeaderTitle = new string[] { "ｺｰｽ名称CD", "ｺｰｽ名称" };
                this.form.txtCourseCd.PopupDataSource = ShainDataTable;
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopUpDataInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public DataTable GetPopUpData(IEnumerable<string> displayCol)
        {
            DataTable ret = new DataTable();
            try
            {
                LogUtility.DebugMethodStart(displayCol);
                M_COURSE_NAME[] CourseNameAll;
                M_COURSE_NAME entity = new M_COURSE_NAME();
                entity.ISNOT_NEED_DELETE_FLG = true;
                CourseNameAll = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>().GetAllValidData(entity);
                this.mCourseNameAll = CourseNameAll;
                if (displayCol.Any(s => s.Length == 0))
                {
                    return new DataTable();
                }
                var dt = EntityUtility.EntityToDataTable(CourseNameAll);
                if (dt.Rows.Count == 0)
                {
                    ret = dt;
                    return dt;
                }
                var sortedDt = new DataTable();
                foreach (var col in displayCol)
                {
                    // 表示対象の列だけを順番に追加
                    sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
                }

                foreach (DataRow r in dt.Rows)
                {
                    sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }
                ret = sortedDt;
                return sortedDt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPopUpData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
        }
        #endregion

        #region 車輌チェック
        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChechSharyouCd()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 車輌名をクリア
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    return returnVal;
                }

                // 車輌情報取得
                var sharyou = this.GetSharyou(this.form.SHARYOU_CD.Text);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (sharyou == null)
                {
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E020", "車輌");
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                // 車輌名設定
                this.form.SHARYOU_NAME_RYAKU.Text = sharyou.SHARYOU_NAME_RYAKU;

                // 車種入力されてない場合
                if (string.IsNullOrEmpty(this.form.txtSHASHU_CD.Text))
                {
                    // 車種情報取得
                    var shashu = this.GetSharshu(sharyou.SHASYU_CD);
                    if (shashu != null)
                    {
                        // 車種情報設定
                        this.form.txtSHASHU_CD.Text = shashu.SHASHU_CD;
                        this.form.txtSHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
                    }
                }

                // 運転者入力されてない場合
                if (string.IsNullOrEmpty(this.form.txtSHAIN_CD.Text))
                {
                    // 社員情報取得
                    var shain = this.GetShain(sharyou.SHAIN_CD);
                    if (shain != null)
                    {
                        // 運転者情報設定
                        this.form.txtSHAIN_CD.Text = shain.SHAIN_CD;
                        this.form.txtSHAIN_NAME.Text = shain.SHAIN_NAME_RYAKU;
                    }
                }

                // 運搬業者が入力されてない場合
                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // 業者情報取得
                    var gyousha = this.GetGyousha(sharyou.GYOUSHA_CD);
                    if (gyousha != null)
                    {
                        // 業者情報設定
                        this.form.UNPAN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 処理終了
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechSharyouCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// 車輌情報取得
        /// </summary>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns></returns>
        public M_SHARYOU GetSharyou(string sharyouCd)
        {
            M_SHARYOU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(sharyouCd);

                if (string.IsNullOrEmpty(sharyouCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHARYOU keyEntity = new M_SHARYOU();
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    keyEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                keyEntity.SHARYOU_CD = sharyouCd;
                // 車種入力されている場合
                if (!string.IsNullOrEmpty(this.form.txtSHASHU_CD.Text))
                {
                    keyEntity.SHASYU_CD = this.form.txtSHASHU_CD.Text;
                }
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                // [運搬業者CD,車輌CD,車種CD]でM_SHARYOUを検索する
                var returnEntitys = sharyouDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    if (returnEntitys.Length == 1)
                    {
                        // 1件
                        returnVal = returnEntitys[0];
                    }
                    else
                    {
                        // ヒット数が複数件の場合、ポップアップ表示
                        this.form.SHARYOU_CD.Focus();
                        SendKeys.Send(" ");

                        // 返却値は空白をセット
                        returnVal = new M_SHARYOU();
                        returnVal.SHARYOU_NAME_RYAKU = "";
                        returnVal.SHASYU_CD = "";
                        returnVal.SHAIN_CD = "";
                        returnVal.GYOUSHA_CD = "";
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharyou", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 車種情報取得
        /// </summary>
        /// <param name="shashuCd">車種CD</param>
        /// <returns></returns>
        public M_SHASHU GetSharshu(string shashuCd)
        {
            M_SHASHU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shashuCd);

                if (string.IsNullOrEmpty(shashuCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHASHU keyEntity = new M_SHASHU();
                keyEntity.SHASHU_CD = shashuCd;

                // [車種CD]でM_SHASHUを検索する
                var returnEntitys = this.shashuDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = returnEntitys[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharshu", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 社員情報取得
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <returns></returns>
        public M_SHAIN GetShain(string shainCd)
        {
            M_SHAIN returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shainCd);

                if (string.IsNullOrEmpty(shainCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHAIN keyEntity = new M_SHAIN();
                keyEntity.SHAIN_CD = shainCd;
                keyEntity.UNTEN_KBN = true;

                // [社員CD,運転者フラグ=true]でM_SHAINを検索する
                var returnEntitys = this.shainDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = returnEntitys[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetShain", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            M_GYOUSHA returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

                if (gyousha != null && gyousha.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = gyousha[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        /// <summary>
        /// 運転者CDバリデート
        /// </summary>
        public void UNTENSHA_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.txtSHAIN_NAME.Text = "";
                M_SHAIN entity = new M_SHAIN();
                entity.SHAIN_CD = this.form.txtSHAIN_CD.Text;
                entity.UNTEN_KBN = true;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var untenShain = this.shainDao.GetAllValidData(entity).FirstOrDefault();
                if (untenShain == null)
                {
                    // エラーメッセージ
                    this.form.txtSHAIN_CD.IsInputErrorOccured = true;
                    this.form.txtSHAIN_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");
                    this.form.txtSHAIN_CD.Focus();
                    return;
                }

                this.form.txtSHAIN_NAME.Text = untenShain.SHAIN_NAME_RYAKU;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNTENSHA_CDValidated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 補助員CDバリデート
        /// </summary>
        public void HOJOIN_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.txtSHoujyosyaNm.Text = "";
                M_SHAIN entity = new M_SHAIN();
                entity.SHAIN_CD = this.form.txtSHoujyosyaCd.Text;
                entity.UNTEN_KBN = true;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var untenShain = this.shainDao.GetAllValidData(entity).FirstOrDefault();
                if (untenShain == null)
                {
                    // エラーメッセージ
                    this.form.txtSHoujyosyaCd.IsInputErrorOccured = true;
                    this.form.txtSHoujyosyaCd.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "社員");
                    this.form.txtSHoujyosyaCd.Focus();
                    return;
                }

                this.form.txtSHoujyosyaNm.Text = untenShain.SHAIN_NAME_RYAKU;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HOJOIN_CDValidated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運搬業者CDバリデート
        /// </summary>
        public void UNPAN_GYOUSHA_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.UNPAN_GYOUSHA_NAME.Text = "";

                M_GYOUSHA entity = new M_GYOUSHA();
                entity.ISNOT_NEED_DELETE_FLG = true;
                var unpanGyousha = this.gyoushaDao.GetAllValidData(entity).FirstOrDefault(s => s.GYOUSHA_CD == this.form.UNPAN_GYOUSHA_CD.Text);
                if (unpanGyousha == null || !unpanGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    // エラーメッセージ
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.form.UNPAN_GYOUSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    return;
                }

                // 名称セット
                this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CDValidated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 車輌CDEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void sharyouCdEnter(object sender, EventArgs e)
        {
            try
            {
                // 前回値を保持する
                var ctrl = (CustomAlphaNumTextBox)sender;
                this.oldSharyouCD = ctrl.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("sharyouCdEnter", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }
        #region 車輌情報取得
        /// <summary>
        /// 現場情報取得
        /// </summary>
        /// <param name="genbrCd"></param>
        /// <param name="gyoushaCd"></param>
        //public KeyValuePair<int, DataRow> GetSharyouInfo(string gosyaCd, string sharyouCd)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(gosyaCd, sharyouCd);
        //        var dt = this.mDetailDao.GetSharyouDataSql(gosyaCd, sharyouCd);
        //        int cnt = dt.Rows.Count;
        //        var dr = cnt == 1 ? dt.Rows[0] : null;
        //        var kv = new KeyValuePair<int, DataRow>(cnt, dr);

        //        return kv;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("GetSharyouInfo", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}
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

        #region 使うない

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

        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.headForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.headForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.headForm.HIDUKE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.headForm.HIDUKE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.headForm.HIDUKE_FROM.IsInputErrorOccured = true;
                this.headForm.HIDUKE_TO.IsInputErrorOccured = true;
                this.headForm.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.headForm.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                string errorMsg = "日付範囲の設定を見直してください。";
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                msglogic.MessageBoxShowError(errorMsg);
                this.headForm.HIDUKE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141128 teikyou ダブルクリックを追加する　start
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hidukeFromTextBox = this.headForm.HIDUKE_FROM;
            var hidukeToTextBox = this.headForm.HIDUKE_TO;
            hidukeToTextBox.Text = hidukeFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141128 teikyou ダブルクリックを追加する　end
        #endregion

        #region 連携チェック
        internal bool RenkeiCheck(string haishaNum)
        {
            bool ret = true;
            try
            {
                if (string.IsNullOrEmpty(haishaNum))
                {
                    return true;
                }

                DataTable dt = this.mobisyoRtDao.GetRenkeiData("0", haishaNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E262", "現在配車中", "完了後、実績取込にて、定期配車実績データを作成");
                    return false;
                }

                // ロジこんぱす連携済みであるかをチェックする。
                string selectStr;
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = 3"  // 3：定期
                    + " and LDD.REF_DENPYOU_NO = " + haishaNum
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";

                // データ取得
                dt = this.mDetailDao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.MsgBox.MessageBoxShow("E261", "ロジこんぱす連携中", "定期配車実績データを作成");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                ret = false;
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return true;
        }
        #endregion

        #region mapbox連携

        #region 明細ヘッダーにチェックボックスを追加

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        internal void HeaderCheckBoxSupportMapbox()
        {

            LogUtility.DebugMethodStart();

            if (!this.form.customDataGridView1.Columns.Contains(ConstCls.DATA_TAISHO))
            {
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                    newColumn.Name = ConstCls.DATA_TAISHO;
                    newColumn.HeaderText = "地図";
                    newColumn.DataPropertyName = "TAISHO";
                    newColumn.Width = 70;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "地図   ";
                    newColumn.HeaderCell = newheader;
                    newColumn.Resizable = DataGridViewTriState.False;
                    newColumn.ReadOnly = false;

                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.form.customDataGridView1.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns.Add(newColumn);
                    }
                    this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 明細ヘッダーのチェックボックス解除

        /// <summary>
        /// 明細ヘッダーチェックボックスを解除する
        /// </summary>
        private void HeaderCheckBoxFalse()
        {
            if (this.form.customDataGridView1.Columns.Contains(ConstCls.DATA_TAISHO))
            {
                DataGridViewCheckBoxHeaderCell header = this.form.customDataGridView1.Columns[ConstCls.DATA_TAISHO].HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header._checked = false;
                }
            }
        }

        #endregion

        #region 地図表示件数チェック

        /// <summary>
        /// 一覧で選択がチェックされているか確認する。
        /// また、チェックされた行で地図表示できるデータがない場合もアラートを出す。
        /// </summary>
        /// <returns></returns>
        internal bool CheckForCheckBox()
        {
            bool ret = false;

            // チェックが1件もない場合のチェック
            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                // 選択のチェックボックスの値を取得する。
                if (row.Cells[ConstCls.DATA_TAISHO].Value != null)
                {
                    ret = bool.Parse(Convert.ToString(row.Cells[ConstCls.DATA_TAISHO].Value));
                    if (ret)
                    {
                        break;
                    }
                }
            }
            if (!ret)
            {
                this.MsgBox.MessageBoxShowError("地図表示対象の明細がありません。");
                return ret;
            }

            return ret;
        }

        #endregion

        #region 明細チェックボックスのスペースキー押下時の制御

        /// <summary>
        /// [地図]で、スペースキーでチェック処理が走るように下準備
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DataGridViewCell curCell = this.form.customDataGridView1.CurrentCell;

                if (curCell.RowIndex < 0 || curCell.ColumnIndex != 1)
                {
                    return;
                }

                this.SpaceChk = true;
                this.SpaceON = false;
                //[地図]OFFにする場合は、何もしない。
                //[地図]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                if (this.form.customDataGridView1[1, curCell.RowIndex].Value == null)
                {
                    this.SpaceON = true;
                    this.form.customDataGridView1[1, curCell.RowIndex].Value = true;
                }
                else
                {
                    if (!(bool)this.form.customDataGridView1[1, curCell.RowIndex].Value)
                    {
                        this.SpaceON = true;
                        this.form.customDataGridView1[1, curCell.RowIndex].Value = !(bool)this.form.customDataGridView1[1, curCell.RowIndex].Value;
                    }
                }
                this.form.customDataGridView1.Refresh();
            }
        }

        #endregion

        #region 連携処理

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        private List<mapDtoList> createMapboxDto()
        {
            try
            {
                int layerId = 0;

                List<SummaryKeyCode> summaryKeyCodeList = new List<SummaryKeyCode>();

                // 出力対象となるコース情報のPK情報を取得
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].IsNewRow)
                    {
                        continue;
                    }

                    // チェックなしデータを排除する
                    if (this.form.customDataGridView1.Rows[i].Cells[ConstCls.DATA_TAISHO].Value == null) continue;
                    if ((bool)this.form.customDataGridView1.Rows[i].Cells[ConstCls.DATA_TAISHO].Value == false) continue;

                    SummaryKeyCode summaryKeyCode = new SummaryKeyCode();
                    summaryKeyCode.HIDDEN_COLUMN_HAISHA_NUMBER = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER].Value);
                    summaryKeyCodeList.Add(summaryKeyCode);
                }

                List<mapDtoList> dtoLists = new List<mapDtoList>();

                // LINQでグループ化する
                var roopList = summaryKeyCodeList.GroupBy(a => new { HIDDEN_COLUMN_HAISHA_NUMBER = a.HIDDEN_COLUMN_HAISHA_NUMBER });
                foreach (var group in roopList)
                {
                    layerId++;

                    // レイヤー追加
                    mapDtoList dtoList = new mapDtoList();
                    dtoList.layerId = layerId;

                    List<mapDto> dtos = new List<mapDto>();

                    // 地図出力に必要な情報を収集
                    #region 明細1件の定期配車の内容を取得する

                    string sql = string.Empty;
                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT ");
                    sb.AppendFormat(" DET.ROW_NUMBER AS {0} ", ConstCls.ROW_NO);
                    sb.AppendFormat(",DET.ROUND_NO AS {0} ", ConstCls.ROUND_NO);
                    sb.AppendFormat(",CON.COURSE_NAME AS {0} ", ConstCls.COURSE_NAME);
                    sb.AppendFormat(",DET.GYOUSHA_CD AS {0} ", ConstCls.GYOUSHA_CD);
                    sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstCls.GYOUSHA_NAME_RYAKU);
                    sb.AppendFormat(",DET.GENBA_CD AS {0} ", ConstCls.GENBA_CD);
                    sb.AppendFormat(",GEN.GENBA_NAME_RYAKU AS {0} ", ConstCls.GENBA_NAME_RYAKU);
                    sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstCls.TODOUFUKEN_NAME);
                    sb.AppendFormat(",GEN.GENBA_ADDRESS1 AS {0} ", ConstCls.GENBA_ADDRESS1);
                    sb.AppendFormat(",GEN.GENBA_ADDRESS2 AS {0} ", ConstCls.GENBA_ADDRESS2);
                    sb.AppendFormat(",GEN.GENBA_LATITUDE AS {0} ", ConstCls.GENBA_LATITUDE);
                    sb.AppendFormat(",GEN.GENBA_LONGITUDE AS {0} ", ConstCls.GENBA_LONGITUDE);
                    sb.AppendFormat(",GEN.GENBA_POST AS {0} ", ConstCls.GENBA_POST);
                    sb.AppendFormat(",GEN.GENBA_TEL AS {0} ", ConstCls.GENBA_TEL);
                    sb.AppendFormat(",GEN.BIKOU1 AS {0} ", ConstCls.BIKOU1);
                    sb.AppendFormat(",GEN.BIKOU2 AS {0} ", ConstCls.BIKOU2);
                    sb.AppendFormat(",ENT.TEIKI_HAISHA_NUMBER");
                    sb.AppendFormat(",ENT.SAGYOU_DATE");
                    sb.AppendFormat(",ENT.DAY_CD");
                    sb.AppendFormat(",ENT.SHUPPATSU_GYOUSHA_CD");
                    sb.AppendFormat(",ENT.SHUPPATSU_GENBA_CD");
                    sb.AppendFormat(",DET.KIBOU_TIME ");
                    sb.AppendFormat(",DET.SYSTEM_ID ");
                    sb.AppendFormat(",DET.SEQ ");
                    sb.AppendFormat(",DET.DETAIL_SYSTEM_ID ");
                    sb.AppendFormat(" FROM T_TEIKI_HAISHA_ENTRY AS ENT ");
                    sb.AppendFormat(" LEFT JOIN M_COURSE_NAME CON ON ENT.COURSE_NAME_CD = CON.COURSE_NAME_CD ");
                    sb.AppendFormat(" LEFT JOIN T_TEIKI_HAISHA_DETAIL DET ON ENT.SYSTEM_ID = DET.SYSTEM_ID AND ENT.SEQ = DET.SEQ ");
                    sb.AppendFormat(" LEFT JOIN M_GYOUSHA GYO ON DET.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_GENBA GEN ON DET.GYOUSHA_CD = GEN.GYOUSHA_CD AND DET.GENBA_CD = GEN.GENBA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                    sb.AppendFormat(" WHERE ENT.DELETE_FLG = 0 ");
                    sb.AppendFormat(" AND ENT.TEIKI_HAISHA_NUMBER = {0}", group.Key.HIDDEN_COLUMN_HAISHA_NUMBER);
                    sb.AppendFormat(" ORDER BY DET.ROW_NUMBER ");

                    DataTable dt = this.mDetailDao.GetDateForStringSql(sb.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        // 出発業者のみ、または出発業者と出発現場が設定されている場合、コースの先頭とする。
                        string gyoushaCd = dt.Rows[0]["SHUPPATSU_GYOUSHA_CD"].ToString();
                        string genbaCd = dt.Rows[0]["SHUPPATSU_GENBA_CD"].ToString();
                        if (!string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd))
                        {
                            sql = string.Empty;
                            sb = new StringBuilder();

                            sb.AppendFormat(" SELECT ");
                            sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", ConstCls.GYOUSHA_CD);
                            sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstCls.GYOUSHA_NAME_RYAKU);
                            sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstCls.TODOUFUKEN_NAME);
                            sb.AppendFormat(",GYO.GYOUSHA_ADDRESS1 AS {0} ", ConstCls.GYOUSHA_ADDRESS1);
                            sb.AppendFormat(",GYO.GYOUSHA_ADDRESS2 AS {0} ", ConstCls.GYOUSHA_ADDRESS2);
                            sb.AppendFormat(",GYO.GYOUSHA_LATITUDE AS {0} ", ConstCls.GYOUSHA_LATITUDE);
                            sb.AppendFormat(",GYO.GYOUSHA_LONGITUDE AS {0} ", ConstCls.GYOUSHA_LONGITUDE);
                            sb.AppendFormat(",GYO.GYOUSHA_POST AS {0} ", ConstCls.GYOUSHA_POST);
                            sb.AppendFormat(",GYO.GYOUSHA_TEL AS {0} ", ConstCls.GYOUSHA_TEL);
                            sb.AppendFormat(",GYO.BIKOU1 AS {0} ", ConstCls.BIKOU1);
                            sb.AppendFormat(",GYO.BIKOU2 AS {0} ", ConstCls.BIKOU2);
                            sb.AppendFormat(" FROM M_GYOUSHA AS GYO ");
                            sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GYO.GYOUSHA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                            sb.AppendFormat(" WHERE GYO.DELETE_FLG = 0 ");
                            sb.AppendFormat(" AND GYO.GYOUSHA_CD = '{0}'", gyoushaCd);

                            DataTable dtShuppatsu = this.mDetailDao.GetDateForStringSql(sb.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                mapDto dto = new mapDto();
                                dto.id = layerId;
                                dto.layerNo = layerId;
                                dto.courseName = Convert.ToString(dt.Rows[0][ConstCls.COURSE_NAME]);
                                if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["DAY_CD"])))
                                {
                                    dto.dayName = mapLogic.SetDayNameByDate(Convert.ToString(dt.Rows[0]["SAGYOU_DATE"]));
                                }
                                else
                                {
                                    dto.dayName = mapLogic.SetDayNameByCd(Convert.ToString(dt.Rows[0]["DAY_CD"]));
                                }
                                dto.teikiHaishaNo = Convert.ToString(dt.Rows[0]["TEIKI_HAISHA_NUMBER"]);
                                dto.torihikisakiCd = string.Empty;
                                dto.torihikisakiName = string.Empty;
                                dto.gyoushaCd = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_CD]);
                                dto.gyoushaName = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_NAME_RYAKU]);
                                dto.genbaCd = string.Empty;
                                dto.genbaName = string.Empty;
                                dto.post = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_POST]);
                                dto.address = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.TODOUFUKEN_NAME])
                                            + Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_ADDRESS1])
                                            + Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_ADDRESS2]);
                                dto.tel = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_TEL]);
                                dto.bikou1 = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.BIKOU1]);
                                dto.bikou2 = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.BIKOU2]);
                                dto.latitude = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_LATITUDE]);
                                dto.longitude = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_LONGITUDE]);
                                dto.rowNo = 0;
                                dto.roundNo = 0;
                                dto.genbaChaku = string.Empty;
                                dto.hinmei = string.Empty;
                                dto.shuppatsuFlag = true;
                                dtos.Add(dto);
                            }
                        }
                        else if (!string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
                        {
                            sql = string.Empty;
                            sb = new StringBuilder();

                            sb.AppendFormat(" SELECT ");
                            sb.AppendFormat(" GEN.GYOUSHA_CD AS {0} ", ConstCls.GYOUSHA_CD);
                            sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstCls.GYOUSHA_NAME_RYAKU);
                            sb.AppendFormat(",GEN.GENBA_CD AS {0} ", ConstCls.GENBA_CD);
                            sb.AppendFormat(",GEN.GENBA_NAME_RYAKU AS {0} ", ConstCls.GENBA_NAME_RYAKU);
                            sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstCls.TODOUFUKEN_NAME);
                            sb.AppendFormat(",GEN.GENBA_ADDRESS1 AS {0} ", ConstCls.GENBA_ADDRESS1);
                            sb.AppendFormat(",GEN.GENBA_ADDRESS2 AS {0} ", ConstCls.GENBA_ADDRESS2);
                            sb.AppendFormat(",GEN.GENBA_LATITUDE AS {0} ", ConstCls.GENBA_LATITUDE);
                            sb.AppendFormat(",GEN.GENBA_LONGITUDE AS {0} ", ConstCls.GENBA_LONGITUDE);
                            sb.AppendFormat(",GEN.GENBA_POST AS {0} ", ConstCls.GENBA_POST);
                            sb.AppendFormat(",GEN.GENBA_TEL AS {0} ", ConstCls.GENBA_TEL);
                            sb.AppendFormat(",GEN.BIKOU1 AS {0} ", ConstCls.BIKOU1);
                            sb.AppendFormat(",GEN.BIKOU2 AS {0} ", ConstCls.BIKOU2);
                            sb.AppendFormat(" FROM M_GENBA AS GEN ");
                            sb.AppendFormat(" LEFT JOIN M_GYOUSHA GYO ON GEN.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                            sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                            sb.AppendFormat(" WHERE GEN.DELETE_FLG = 0 ");
                            sb.AppendFormat(" AND GEN.GYOUSHA_CD = '{0}'", gyoushaCd);
                            sb.AppendFormat(" AND GEN.GENBA_CD = '{0}'", genbaCd);

                            DataTable dtShuppatsu = this.mDetailDao.GetDateForStringSql(sb.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                mapDto dto = new mapDto();
                                dto.id = layerId;
                                dto.layerNo = layerId;
                                dto.courseName = Convert.ToString(dt.Rows[0][ConstCls.COURSE_NAME]);
                                if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["DAY_CD"])))
                                {
                                    dto.dayName = mapLogic.SetDayNameByDate(Convert.ToString(dt.Rows[0]["SAGYOU_DATE"]));
                                }
                                else
                                {
                                    dto.dayName = mapLogic.SetDayNameByCd(Convert.ToString(dt.Rows[0]["DAY_CD"]));
                                }
                                dto.teikiHaishaNo = Convert.ToString(dt.Rows[0]["TEIKI_HAISHA_NUMBER"]);
                                dto.torihikisakiCd = string.Empty;
                                dto.torihikisakiName = string.Empty;
                                dto.gyoushaCd = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_CD]);
                                dto.gyoushaName = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GYOUSHA_NAME_RYAKU]);
                                dto.genbaCd = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GENBA_CD]);
                                dto.genbaName = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GENBA_NAME_RYAKU]);
                                dto.post = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GENBA_POST]);
                                dto.address = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.TODOUFUKEN_NAME])
                                            + Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GENBA_ADDRESS1])
                                            + Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GENBA_ADDRESS2]);
                                dto.tel = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GENBA_TEL]);
                                dto.bikou1 = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.BIKOU1]);
                                dto.bikou2 = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.BIKOU2]);
                                dto.latitude = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GENBA_LATITUDE]);
                                dto.longitude = Convert.ToString(dtShuppatsu.Rows[0][ConstCls.GENBA_LONGITUDE]);
                                dto.rowNo = 0;
                                dto.roundNo = 0;
                                dto.genbaChaku = string.Empty;
                                dto.hinmei = string.Empty;
                                dto.shuppatsuFlag = true;
                                dtos.Add(dto);
                            }
                        }

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = Convert.ToString(dt.Rows[j][ConstCls.COURSE_NAME]);
                            if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[j]["DAY_CD"])))
                            {
                                dto.dayName = mapLogic.SetDayNameByDate(Convert.ToString(dt.Rows[j]["SAGYOU_DATE"]));
                            }
                            else
                            {
                                dto.dayName = mapLogic.SetDayNameByCd(Convert.ToString(dt.Rows[j]["DAY_CD"]));
                            }
                            dto.teikiHaishaNo = Convert.ToString(dt.Rows[j]["TEIKI_HAISHA_NUMBER"]);
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_CD]);
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = Convert.ToString(dt.Rows[j][ConstCls.GENBA_CD]);
                            dto.genbaName = Convert.ToString(dt.Rows[j][ConstCls.GENBA_NAME_RYAKU]);
                            dto.post = Convert.ToString(dt.Rows[j][ConstCls.GENBA_POST]);
                            dto.address = Convert.ToString(dt.Rows[j][ConstCls.TODOUFUKEN_NAME]) + Convert.ToString(dt.Rows[j][ConstCls.GENBA_ADDRESS1]) + Convert.ToString(dt.Rows[j][ConstCls.GENBA_ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][ConstCls.GENBA_TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU2]);
                            dto.rowNo = Convert.ToInt32(dt.Rows[j][ConstCls.ROW_NO]);
                            dto.roundNo = Convert.ToInt32(dt.Rows[j][ConstCls.ROUND_NO]);
                            string time = string.Empty;
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j]["KIBOU_TIME"])))
                                time = Convert.ToDateTime(Convert.ToString(dt.Rows[j]["KIBOU_TIME"])).ToString("HH:mm");
                            dto.genbaChaku = time;

                            sql = " SELECT H.HINMEI_NAME_RYAKU, U.UNIT_NAME_RYAKU FROM T_TEIKI_HAISHA_SHOUSAI HS "
                                + " LEFT JOIN M_HINMEI H ON HS.HINMEI_CD = H.HINMEI_CD "
                                + " LEFT JOIN M_UNIT U ON HS.UNIT_CD = U.UNIT_CD"
                                + " WHERE SYSTEM_ID = " + Convert.ToInt64(dt.Rows[j]["SYSTEM_ID"])
                                + "   AND SEQ = " + Convert.ToInt32(dt.Rows[j]["SEQ"])
                                + "   AND DETAIL_SYSTEM_ID = " + Convert.ToInt64(dt.Rows[j]["DETAIL_SYSTEM_ID"]);
                            DataTable hinmeiDt = this.sysInfoDao.GetDateForStringSql(sql);
                            string hinmei = string.Empty;
                            foreach (DataRow dr in hinmeiDt.Rows)
                            {
                                if (string.IsNullOrEmpty(hinmei))
                                {
                                    hinmei += dr["HINMEI_NAME_RYAKU"] + " " + dr["UNIT_NAME_RYAKU"];
                                }
                                else
                                {
                                    hinmei += "/" + dr["HINMEI_NAME_RYAKU"] + " " + dr["UNIT_NAME_RYAKU"];
                                }
                            }
                            dto.hinmei = hinmei;
                            dto.latitude = Convert.ToString(dt.Rows[j][ConstCls.GENBA_LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][ConstCls.GENBA_LONGITUDE]);
                            dto.shuppatsuFlag = false;
                            dtos.Add(dto);
                        }
                        // 1コース終わったらリストにセット
                        dtoList.dtos = dtos;
                    }

                    #endregion

                    if (dtoList.dtos != null)
                    {
                        if (dtoList.dtos.Count != 0)
                        {
                            dtoLists.Add(dtoList);
                        }
                    }
                }
                return dtoLists;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createMapboxDto", ex);
                this.MsgBox.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        #endregion

        #endregion
    }
}