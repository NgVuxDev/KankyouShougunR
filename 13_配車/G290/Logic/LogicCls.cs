// $Id: LogicCls.cs 36305 2014-12-02 03:54:15Z diq@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.APP;
using Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.Const;
using Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.DAO;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.IchiranCommon.Const;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.Logic
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeader headForm;

        /// <summary>
        /// 検索用SQL
        /// </summary>
        public string searchSql { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;
        /// <summary>
        /// 一覧検索用のDao
        /// </summary>
        private T_TeikiJisekiDao mDetailDao;

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
        ///   ｺｰｽ名称CD
        /// </summary>
        public M_COURSE_NAME[] mCourseNameAll;

        /// <summary>
        /// 車輌CD前回値
        /// </summary>
        private string oldSharyouCD;

        private MessageBoxShowLogic MsgBox;
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
                this.mDetailDao = DaoInitUtility.GetComponent<T_TeikiJisekiDao>();
                this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();
                this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();
                this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
                this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
                this.MsgBox = new MessageBoxShowLogic();
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

                //システム情報を取得する
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.form.SystemId.ToString());
                // ヘッダー項目
                this.headForm = (UIHeader)((BusinessBaseForm)this.form.ParentForm).headerForm;
                //Hearder情報初期化
                this.initHeaderFrom();
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
                // ｺｰｽ情報 ポップアップ初期化
                this.PopUpDataInit();


                this.allControl = this.form.allControl;
                //並び替えと明細の設定             
                this.form.customDataGridView1.Location = new Point(0, 100);
                this.form.customDataGridView1.Size = new Size(997, 340);
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.txtCourseCd.Focus();

                //2014/12/25 追加（拠点初期表示設定）喬 start
                //拠点CD
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                string KYOTEN_CD = this.GetUserProfileValue(userProfile, "拠点CD");
                IM_KYOTENDao kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                var kyotenP = kyotenDao.GetDataByCd(KYOTEN_CD);
                //拠点名称
                if (kyotenP != null && KYOTEN_CD != string.Empty)
                {
                    this.headForm.KYOTEN_CD.Text = KYOTEN_CD.PadLeft(this.headForm.KYOTEN_CD.MaxLength, '0'); ;
                    this.headForm.KYOTEN_NAME_RYAKU.Text = kyotenP.KYOTEN_NAME_RYAKU;
                }
                else
                {
                    //拠点CD、拠点 : ブランク
                    this.headForm.KYOTEN_CD.Text = string.Empty;
                    this.headForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                }
                //2014/12/25 追加（拠点初期表示設定）喬 end

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
        /// <summary>
        /// header項目の初期表示処理
        /// </summary>
        private void initHeaderFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                //日付(from) ：当日
                this.headForm.HIDUKE_FROM.Value = parentForm.sysDate;

                //日付(to): 当日
                this.headForm.HIDUKE_TO.Value = parentForm.sysDate;

                //伝票日付RadioButton選択状態
                this.headForm.txtNum_HidukeSentaku.Text = ConstCls.HidukeCD_DenPyou;
                this.headForm.radbtnDenpyouHiduke.Checked = true;

                //読込データ件数：０ [件]
                this.headForm.readDataNumber.Text = "0";
                //明細部：　ブランク
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.TabIndex = 70;
            }
            catch (Exception ex)
            {
                LogUtility.Error("initFrom", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ｺｰｽ情報 ポップアップ初期化
        /// </summary>
        public void PopUpDataInit()
        {
            // ｺｰｽ情報 ポップアップ取得
            // 表示用データ取得＆加工
            var ShainDataTable = this.GetPopUpData(this.form.txtCourseCd.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
            // TableNameを設定すれば、ポップアップのタイトルになる
            ShainDataTable.TableName = "ｺｰｽ名称情報";

            // 列名とデータソース設定
            this.form.txtCourseCd.PopupDataHeaderTitle = new string[] { "ｺｰｽ名称CD", "ｺｰｽ名称" };
            this.form.txtCourseCd.PopupDataSource = ShainDataTable;
        }
        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public DataTable GetPopUpData(IEnumerable<string> displayCol)
        {
            M_COURSE_NAME entity = new M_COURSE_NAME();
            entity.ISNOT_NEED_DELETE_FLG = true;
            mCourseNameAll = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>().GetAllValidData(entity);

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = EntityUtility.EntityToDataTable(mCourseNameAll);
            if (dt.Rows.Count == 0)
            {
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

            return sortedDt;
        }

        //2014/12/25 追加（拠点初期表示設定）喬 start
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }
        //2014/12/25 追加（拠点初期表示設定）喬 end
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
                //parentForm.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);      //処理No(ESC)               
                parentForm.txb_process.PreviewKeyDown += new PreviewKeyDownEventHandler(txb_process_PreviewKeyDown);      //処理No(ESC) 

                //Functionボタンのイベント生成
                parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);              //F1 簡易検索／汎用検索
                parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);              //新規
                parentForm.bt_func3.Click += new EventHandler(this.bt_func3_Click);              //修正
                parentForm.bt_func4.Click += new EventHandler(this.bt_func4_Click);              //削除
                parentForm.bt_func5.Click += new EventHandler(this.bt_func5_Click);              //複写
                parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);              //CSV出力
                parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //条件クリア

                this.form.C_Regist(parentForm.bt_func8);
                parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //検索
                parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     //並び替え
                parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる
                parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移
                parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //検索条件設定へ遷移

                //明細画面上でダブルクリック時のイベント生成
                this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(customDataGridView1_MouseDoubleClick);

                // Validatedイベント
                this.form.txtSHAIN_CD.Validated += new EventHandler(this.form.UNTENSHA_CDValidated);
                this.form.txtSHoujyosyaCd.Validated += new EventHandler(this.form.HOJOIN_CDValidated);
                this.form.UNPAN_GYOUSHA_CD.Validated += new EventHandler(this.form.UNPAN_GYOUSHA_CDValidated);

                // 20141128 teikyou ダブルクリックを追加する　start
                this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
                // 20141128 teikyou ダブルクリックを追加する　end
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

        #region 検索処理

        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return 0;
                }
                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

                //共通からSQL文でDataGridViewの列名とソート順を取得する
                this.form.SetLogicSelect();

                //検索用SQLの作成
                MakeSearchCondition();

                //検索実行
                this.searchResult = new DataTable();
                if (!string.IsNullOrEmpty(this.searchSql))
                {
                    if (!string.IsNullOrEmpty(this.syainCode))
                    {
                        this.searchResult = mDetailDao.getdateforstringsql(this.searchSql);
                        int count = searchResult.Rows.Count;
                        //読込データ件数を0にする
                        this.headForm.readDataNumber.Text = count.ToString();
                        //検索結果を表示する
                        this.form.ShowData(searchResult);

                        if (count == 0)
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("C001");
                        }
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
                        return searchResult.Rows.Count;
                    }
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
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索用SQLの作成
        /// </summary>
        private void MakeSearchCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.selectQuery))
                {
                    this.searchSql = string.Empty;
                    return;
                }
                // テーブル名称
                string TableName = this.selectQuery.Split(',')[0].Split('.')[0];

                // SQL文格納StringBuilder
                var sql = new StringBuilder();

                #region SELECT句

                sql.Append(" SELECT  DISTINCT ");
                // 出力パターンよりのSQL
                sql.Append(this.selectQuery);

                // 定期実績番号               
                sql.AppendFormat(", T_TEIKI_JISSEKI_ENTRY.TEIKI_JISSEKI_NUMBER AS {0} ", ConstCls.HIDDEN_COLUMN_JISSEKI_NUMBER);
                sql.AppendFormat(", T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID AS {0} ", ConstCls.HIDDEN_COLUMN_SYSTEM_ID);
                sql.AppendFormat(", T_TEIKI_JISSEKI_ENTRY.SEQ AS {0} ", ConstCls.HIDDEN_COLUMN_SEQ);
                sql.AppendFormat(", T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE AS {0} ", ConstCls.HIDDEN_COLUMN_SAGYOU_DATE);
                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    sql.AppendFormat(", T_TEIKI_JISSEKI_DETAIL.DETAIL_SYSTEM_ID AS {0} ", ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID);
                }

                #endregion

                #region FROM句

                sql.Append(" FROM   ");
                sql.Append(" T_TEIKI_JISSEKI_ENTRY ");

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    // 明細
                    sql.Append(" LEFT JOIN T_TEIKI_JISSEKI_DETAIL ");
                    sql.Append(" ON T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID = T_TEIKI_JISSEKI_DETAIL.SYSTEM_ID ");
                    sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.SEQ = T_TEIKI_JISSEKI_DETAIL.SEQ ");

                    // 荷降
                    sql.Append(" LEFT JOIN T_TEIKI_JISSEKI_NIOROSHI ");
                    sql.Append(" ON T_TEIKI_JISSEKI_DETAIL.SYSTEM_ID = T_TEIKI_JISSEKI_NIOROSHI.SYSTEM_ID ");
                    sql.Append(" AND T_TEIKI_JISSEKI_DETAIL.SEQ = T_TEIKI_JISSEKI_NIOROSHI.SEQ ");
                    sql.Append(" AND T_TEIKI_JISSEKI_DETAIL.NIOROSHI_NUMBER = T_TEIKI_JISSEKI_NIOROSHI.NIOROSHI_NUMBER  ");
                }

                sql.Append(this.joinQuery);

                #endregion

                #region WHERE句

                sql.Append(" WHERE ");
                //削除フラグ
                sql.Append(" T_TEIKI_JISSEKI_ENTRY.DELETE_FLG = 0 ");

                //画面で在拠点CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text)
                       && this.headForm.KYOTEN_CD.Text != ConstCls.KyouTenZenSya)
                {
                    sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.KYOTEN_CD = " + int.Parse(this.headForm.KYOTEN_CD.Text) + " ");
                }

                //画面で日付選択が作業日付の場合
                if (ConstCls.HidukeCD_DenPyou.Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
                    }
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
                    }
                }
                //画面で日付選択が入力日付の場合
                if (ConstCls.HidukeCD_NyuuRyoku.Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.UPDATE_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
                    }
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.UPDATE_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
                    }
                }
                //画面条件
                //画面で在コースCDがnull無いの場合
                if (!string.IsNullOrEmpty(this.form.txtCourseCd.Text))
                {
                    sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.COURSE_NAME_CD = '" + this.form.txtCourseCd.Text + "' ");
                }
                //画面で運転者CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.form.txtSHAIN_CD.Text))
                {
                    sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.UNTENSHA_CD = '" + this.form.txtSHAIN_CD.Text + "' ");
                }
                //画面で補助員CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.form.txtSHoujyosyaCd.Text))
                {
                    sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.HOJOIN_CD = '" + this.form.txtSHoujyosyaCd.Text + "' ");
                }
                //画面で車種CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.form.txtSHASHU_CD.Text))
                {
                    sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.SHASHU_CD = '" + this.form.txtSHASHU_CD.Text + "' ");
                }
                //画面で車輌CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.SHARYOU_CD = '" + this.form.SHARYOU_CD.Text + "' ");
                }
                //画面で運搬業者CDがnullではない場合
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    sql.Append(" AND T_TEIKI_JISSEKI_ENTRY.UNPAN_GYOUSHA_CD = '" + this.form.UNPAN_GYOUSHA_CD.Text + "' ");
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
                sql.AppendFormat(template, ConstCls.HIDDEN_COLUMN_JISSEKI_NUMBER);
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

                //入力画面へ遷移用Column（定期実績番号）を非表示にする
                if (this.form.customDataGridView1.Columns.Contains(ConstCls.HIDDEN_COLUMN_JISSEKI_NUMBER))
                {
                    this.form.customDataGridView1.Columns[ConstCls.HIDDEN_COLUMN_JISSEKI_NUMBER].Visible = false;
                }

                if (this.form.customDataGridView1.Columns.Contains(ConstCls.HIDDEN_COLUMN_SYSTEM_ID))
                {
                    this.form.customDataGridView1.Columns[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Visible = false;
                }

                if (this.form.customDataGridView1.Columns.Contains(ConstCls.HIDDEN_COLUMN_SEQ))
                {
                    this.form.customDataGridView1.Columns[ConstCls.HIDDEN_COLUMN_SEQ].Visible = false;
                }

                if (this.form.customDataGridView1.Columns.Contains(ConstCls.HIDDEN_COLUMN_SAGYOU_DATE))
                {
                    this.form.customDataGridView1.Columns[ConstCls.HIDDEN_COLUMN_SAGYOU_DATE].Visible = false;
                }

                if (this.form.customDataGridView1.Columns.Contains(ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID))
                {
                    this.form.customDataGridView1.Columns[ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID].Visible = false;
                }

                // TextAlignの変更(伝票区分CD) ※IchranBaseLogicで決定されているのでここで変更
                if (this.form.customDataGridView1.Columns.Contains(ConstCls.ALIGN_COLUMN_DENPYOU_KBN_CD))
                {
                    // 右寄せ⇒左寄せ
                    this.form.customDataGridView1.Columns[ConstCls.ALIGN_COLUMN_DENPYOU_KBN_CD].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
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

        #endregion

        #region 車輌チェック
        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckSharyouCd()
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
                    LogUtility.DebugMethodEnd(returnVal);
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
                LogUtility.Error("CheckSharyouCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
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
                keyEntity.ISNOT_NEED_DELETE_FLG = true;

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
                keyEntity.ISNOT_NEED_DELETE_FLG = true;

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
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
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
                M_SHAIN keyEntity = new M_SHAIN();
                keyEntity.UNTEN_KBN = true;
                keyEntity.SHAIN_CD = this.form.txtSHAIN_CD.Text;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var untenShain = this.shainDao.GetAllValidData(keyEntity).FirstOrDefault();
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
                this.MsgBox.MessageBoxShow("E245", "");
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
                M_SHAIN keyEntity = new M_SHAIN();
                keyEntity.UNTEN_KBN = true;
                keyEntity.SHAIN_CD = this.form.txtSHoujyosyaCd.Text;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var untenShain = this.shainDao.GetAllValidData(keyEntity).FirstOrDefault();
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
                this.MsgBox.MessageBoxShow("E245", "");
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
                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var unpanGyousha = this.gyoushaDao.GetAllValidData(keyEntity).FirstOrDefault();
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
                this.MsgBox.MessageBoxShow("E245", "");
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
            // 前回値を保持する
            var ctrl = (CustomAlphaNumTextBox)sender;
            this.oldSharyouCD = ctrl.Text;
        }

        #region Functionボタン 押下処理
        /// <summary>
        /// F1 簡易検索／汎用検索
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            if (this.form.pnlSearchString.Visible == true)
            {
                this.form.pnlSearchString.Visible = false;
                parentForm.bt_process2.Text = "[2]検索条件設定";
                parentForm.bt_process2.Enabled = true;

                this.form.searchString.Visible = true;


                parentForm.bt_func1.Text = "[F1]\r\n簡易検索";
            }
            else if (this.form.searchString.Visible == true)
            {
                this.form.searchString.Visible = false;
                this.form.pnlSearchString.Visible = true;
                parentForm.bt_process2.Text = "";
                parentForm.bt_process2.Enabled = false;
                parentForm.bt_func1.Text = "[F1]\r\n汎用検索";
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
                //入力画面対象を生成する               
                FormManager.OpenFormWithAuth("G289", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, string.Empty);

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

        /// <summary>
        /// F5 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
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
                    //入力画面へ遷移する（複写モード）
                    forwardNyuuryoku(WINDOW_TYPE.NEW_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func5_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

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
                        CSVExport.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_TEIKIHAISHA_ZISSEKI_ICHIRAN), this.form);
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

                var parentForm = (BusinessBaseForm)this.form.Parent;
                ////日付(from) ：当日
                this.headForm.HIDUKE_FROM.Value = parentForm.sysDate;

                ////日付(to): 当日
                this.headForm.HIDUKE_TO.Value = parentForm.sysDate;

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
                //汎用検索条件：ブランク
                this.form.searchString.Text = string.Empty;

                //運搬業者CD、運搬業者名 : ブランク
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;

                //並び順をクリア
                this.form.customSortHeader1.ClearCustomSortSetting(); // No.2292
                this.form.customSearchHeader1.ClearCustomSearchSetting();

                //フォーカス移動
                if (this.form.searchString.Visible == true)
                {
                    this.form.searchString.Focus();
                }
                else
                {
                    this.form.txtCourseCd.Focus();
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

                //必須チェック
                if (this.form.RegistErrorFlag)
                {
                    return;
                }
                //検索処理を行う
                this.Search();
                if (!string.IsNullOrEmpty(this.form.searchString.Text))
                {
                    string getSearchString = this.form.searchString.Text.Replace("\r", "").Replace("\n", "");
                    this.searchString = getSearchString;
                    this.Search();
                }
                else
                {
                    this.form.searchString.Clear();
                    this.form.searchString.Focus();
                }
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
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 1.以下の項目をセッティングファイルに保存する
                //【拠点】
                Properties.Settings.Default.KyotenCd = headForm.KYOTEN_CD.Text;
                //【日付CD】
                Properties.Settings.Default.NumHidukeSentaku = headForm.txtNum_HidukeSentaku.Text;
                //【伝票日付From】【伝票日付To】
                DateTime resultDt;
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text) && DateTime.TryParse(this.headForm.HIDUKE_FROM.Text, out resultDt))
                {
                    Properties.Settings.Default.HidukeFrom = this.headForm.HIDUKE_FROM.Value.ToString();
                }
                else
                {
                    Properties.Settings.Default.HidukeFrom = string.Empty;
                    // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                    this.headForm.HIDUKE_FROM.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text) && DateTime.TryParse(this.headForm.HIDUKE_TO.Text, out resultDt))
                {
                    Properties.Settings.Default.HidukeTo = this.headForm.HIDUKE_TO.Value.ToString();
                }
                else
                {
                    Properties.Settings.Default.HidukeTo = string.Empty;
                    // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                    this.headForm.HIDUKE_TO.Text = string.Empty;
                }

                //Properties.Settings.Default.HidukeFrom = this.headForm.HIDUKE_FROM.Value == null ? string.Empty : this.headForm.HIDUKE_FROM.Value.ToString();
                //Properties.Settings.Default.HidukeTo = this.headForm.HIDUKE_TO.Value == null ? string.Empty : this.headForm.HIDUKE_TO.Value.ToString();
                //【コース】【運転者】【補助員】【車種】【車輌】
                Properties.Settings.Default.CourseCd = this.form.txtCourseCd.Text;
                Properties.Settings.Default.UntensyaCd = this.form.txtSHAIN_CD.Text;
                Properties.Settings.Default.SHoujyosyaCd = this.form.txtSHoujyosyaCd.Text;
                Properties.Settings.Default.ShashuCd = this.form.txtSHASHU_CD.Text;
                Properties.Settings.Default.SharyouCd = this.form.SHARYOU_CD.Text;
                Properties.Settings.Default.unpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;


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
                //定期実績番号を初期化
                String jisekiNumber = String.Empty;
                //作業日付を初期化
                String sagyoDate = String.Empty;
                //修正、削除、複写の場合
                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(windowType)
                    || WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(windowType)
                     || WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(windowType))
                {
                    //選択されたレコードを取得する
                    DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
                    ////システムIDに設定
                    //systemID = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_COLUMN_SYSTEM_ID].Value.ToString();
                    ////枝番Dに設定
                    //seq = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_COLUMN_SEQ].Value.ToString();
                    ////定期実績番号に設定
                    jisekiNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_COLUMN_JISSEKI_NUMBER].Value.ToString();

                    // 権限チェック
                    if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && !r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        if (!r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                            msglogic.MessageBoxShow("E158", "修正");
                            return;
                        }

                        windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }

                    //入力画面対象を生成する               
                    FormManager.OpenFormWithAuth("G289", windowType, windowType, jisekiNumber);

                }



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

        #endregion

        #region プロセスボタン押下処理
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
        ///検索条件設定へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                UIHeader headeForm = new UIHeader();

                //戻り値
                String rtnSysID = String.Empty;
                rtnSysID = this.syainCode;

                ////社員コード、伝種区分を共通画面に渡す
                //var callForm1 = new Shougun.Core.Common.PatternIchiran.UIForm(rtnSysID, denShu_Kbn.ToString());
                //var BusinessBaseForm1 = new BusinessBaseForm(callForm1, headeForm);

                ////共通画面を起動する
                //var isExistForm1 = new FormControlLogic().ScreenPresenceCheck(callForm1);
                //if (!isExistForm1)
                //{
                //    BusinessBaseForm1.ShowDialog();
                //}

                ////戻り値
                //rtnSysID = callForm1.ParamOut_SysID;
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
        #endregion

        #region 汎用検索(SearchString)内でのエンターキー押下イベント
        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void SearchStringKeyDown(object sender, KeyEventArgs e)
        {
            //LogUtility.DebugMethodStart();

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

            //LogUtility.DebugMethodEnd();
        }


        #endregion

        #region 処理No(ESC)でのエンターキー押下イベント
        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void txb_process_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.form.Parent;

                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    return;
                }

                if ("1".Equals(parentForm.txb_process.Text))
                {

                    //戻り値
                    String rtnSysID = String.Empty;
                    rtnSysID = this.syainCode;

                    //社員コード、伝種区分を共通画面に渡す
                    var callForm = new Shougun.Core.Common.PatternIchiran.UIForm(rtnSysID, denShu_Kbn.ToString());
                    var headerForm = new Shougun.Core.Common.PatternIchiran.APP.UIHeader();

                    var businessForm = new BasePopForm(callForm, headerForm);
                    var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                    if (!isExistForm)
                    {
                        businessForm.ShowDialog();
                        if (callForm.ParamOut_UpdateFlag)
                        {
                            this.form.PatternButtonUpdate(sender, e);
                        }
                    }

                    //戻り値
                    rtnSysID = callForm.ParamOut_SysID;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txb_process_KeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                if (this.headForm.radbtnNyuuryokuHiduke.Checked)
                {
                    string[] errorMsg = { "入力日付From", "入力日付To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else
                {
                    string[] errorMsg = { "作業日From", "作業日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
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
    }
}