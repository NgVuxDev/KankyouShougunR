using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.BusinessManagement.Const.Common;
using Shougun.Core.BusinessManagement.MitumoriIchiran.DAO;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Message;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.MitumoriIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region プロパティ

        /// <summary>
        /// 見積一覧検索条件
        /// </summary>
        public MitumoriItiranKsjkDTO MitumoriKsjk { get; set; }

        /// <summary>
        /// 見積一覧検索結果
        /// </summary>
        public DataTable MitumoriKskk { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        #endregion

        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.MitumoriIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        private HeaderForm headForm;

        /// <summary>
        /// MitumoriIchiranForm myForm
        /// </summary>
        private MitumoriIchiranForm myForm;

        /// <summary>
        /// 見積一覧のDao
        /// </summary>
        private TMITSUMORIENTRYDao MitumoriItiranDao;

        /// <summary>
        /// IM_TORIHIKISAKIDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// IM_HIKIAI_TORIHIKISAKIDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_TORIHIKISAKIDao hikiaiTorihikisakiDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_HIKIAI_GYOUSHADao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_GYOUSHADao hikiaiGyousyaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// IM_HIKIAI_GENBADao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_GENBADao hikiaiGenbaDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　start
        /// <summary>
        /// DBアクセッサー
        /// </summary>
        private Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Accessor.DBAccessor accessor;

        // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　end

        #endregion

        #region 定数

        /// <summary>
        /// システムID
        /// </summary>
        internal readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>
        /// 見積番号（伝票番号）
        /// </summary>
        internal readonly string HIDDEN_MITUSMORI_NUMBER = "HIDDEN_MITUSMORI_NUMBER";

        /// <summary>
        /// 見積書書式区分
        /// </summary>
        internal readonly string HIDDEN_MITSUMORI_SHOSHIKI_KBN = "HIDDEN_MITSUMORI_SHOSHIKI_KBN";

        /// <summary>
        /// 明細システムID
        /// </summary>
        internal readonly string HIDDEN_DETAIL_SYSTEM_ID = "HIDDEN_DETAIL_SYSTEM_ID";

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(MitumoriIchiranForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.myForm = targetForm;
                this.MitumoriItiranDao = DaoInitUtility.GetComponent<TMITSUMORIENTRYDao>();
                this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.hikiaiTorihikisakiDao = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_TORIHIKISAKIDao>();
                this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.hikiaiGyousyaDao = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_GYOUSHADao>();
                this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.hikiaiGenbaDao = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_GENBADao>();
                this.MitumoriKsjk = new MitumoriItiranKsjkDTO();
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();

                // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　start
                // Accessor
                this.accessor = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Accessor.DBAccessor();
                // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　end
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("LogicClass", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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

                // 画面タイトル設定
                this.headForm.lb_title.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.myForm.WindowId);
                var parentForm = (BusinessBaseForm)this.myForm.Parent;
                parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.headForm.lb_title.Text);

                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                // 伝票日付
                this.headForm.radbtnDenpyouHiduke.Checked = true;

                // アラート件数
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
                if (sysInfo != null)
                {
                    // システム情報からアラート件数を取得
                    this.headForm.alertNumber.Text = sysInfo.ICHIRAN_ALERT_KENSUU.ToString();
                }

                // DataGridView設定
                this.myForm.customDataGridView1.AllowUserToAddRows = false;
                this.myForm.customDataGridView1.Size = new System.Drawing.Size(997, 328);
                this.myForm.customDataGridView1.Location = new System.Drawing.Point(3, 120);
                this.myForm.customDataGridView1.TabIndex = 22;

                //this.myForm.SetGridViewTitle();

                //DgvCustomTextBoxColumn columeType = new DgvCustomTextBoxColumn();
                //this.myForm.customDataGridView1.Columns.Add(columeType);

                //this.myForm.txtBox_Eigyotantosya.Select();
                this.myForm.txtBox_Eigyotantosya.Focus();
                // 201400708 syunrei ＃947　№15　　start
                //・見積入力画面内、状況項目内、進行中項目は削除する。
                this.DeleteJoukyo();
                // 201400708 syunrei ＃947　№15　　end

                // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　start
                // 拠点
                headForm.KYOTEN_CD.Text = string.Empty;
                headForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                const string KYOTEN_CD = "拠点CD";
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                this.headForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, KYOTEN_CD);
                if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text.ToString()))
                {
                    this.headForm.KYOTEN_CD.Text = this.headForm.KYOTEN_CD.Text.ToString().PadLeft(this.headForm.KYOTEN_CD.MaxLength, '0');
                    CheckKyotenCd();
                }
                // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　end
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret;
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.myForm.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
                parentForm.bt_func1.Enabled = false;
                parentForm.bt_func2.Enabled = true;
                parentForm.bt_func3.Enabled = false;
                parentForm.bt_func4.Enabled = false;
                parentForm.bt_func5.Enabled = false;
                parentForm.bt_func6.Enabled = false;
                parentForm.bt_func7.Enabled = true;
                parentForm.bt_func7.CausesValidation = false;
                parentForm.bt_func8.Enabled = true;
                parentForm.bt_func9.Enabled = false;
                parentForm.bt_func10.Enabled = true;
                parentForm.bt_func11.Enabled = true;
                parentForm.bt_func12.Enabled = true;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("ButtonInit", ex);
                throw ex;
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

                var parentForm = (BusinessBaseForm)this.myForm.Parent;
                //customTextBoxでのエンターキー押下イベント生成
                this.myForm.searchString.KeyDown += new KeyEventHandler(SearchStringKeyDown);    //汎用検索(SearchString)
                parentForm.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);      //処理No(ESC)

                //Functionボタンのイベント生成
                parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);              //汎用検索
                parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);              //新規
                parentForm.bt_func3.Click += new EventHandler(this.bt_func3_Click);              //修正
                parentForm.bt_func4.Click += new EventHandler(this.bt_func4_Click);              //削除
                parentForm.bt_func5.Click += new EventHandler(this.bt_func5_Click);              //複写
                parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);              //CSV出力
                parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //検索条件クリア
                parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //検索
                parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     //並替移動
                parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる
                parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //検索条件設定画面へ遷移

                //画面上でESCキー押下時のイベント生成
                //this.myForm.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown); //myForm上でのESCキー押下でFocus移動
                //  明細画面上でダブルクリック時のイベント生成
                this.myForm.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);
                this.myForm.customDataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(customDataGridView1_CellContentClick);
                //this.myForm.customSortHeader1.txboxSortSettingInfo.KeyPress += new KeyPressEventHandler(bt_func10_Click);

                // 20141201 teikyou ダブルクリックを追加する　start
                this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
                // 20141201 teikyou ダブルクリックを追加する　end
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("EventInit", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region Functionボタン 押下処理

        /// <summary>
        /// F1 簡易検索／汎用検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.myForm.Parent;

                if (parentForm.bt_func1.Text == "[F1]\r\n簡易検索")
                {
                    parentForm.bt_func1.Text = "[F1]\r\n汎用検索";
                }
                else if (parentForm.bt_func1.Text == "[F1]\r\n汎用検索")
                {
                    parentForm.bt_func1.Text = "[F1]\r\n簡易検索";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func1_Click", ex);
                throw ex;
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

                // 新規権限がある場合入力画面表示
                FormManager.OpenFormWithAuth("G276", WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func2_Click", ex);
                throw ex;
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

                //FormShowParamDao prmDto = new FormShowParamDao();
                //DataGridViewCell datagridviewcell = this.myForm.customDataGridView1.CurrentCell;
                //if (datagridviewcell.RowIndex >= 0)
                //{
                //    prmDto.windowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                //    prmDto.systemID =
                //        this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["システムID"].Value.ToString();
                //    prmDto.detailSystemID =
                //        this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["枝番"].Value.ToString();
                //    prmDto.mitsumoriNumber =
                //        this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["伝票番号"].Value.ToString();
                //    prmDto.cyohyoType =
                //        int.Parse(this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["見積書式区分"].Value.ToString());
                //}

                //var headerForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.HeaderForm();
                ////var callForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.MitsumoriNyuryoku(prmDto);
                //var callForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.MitsumoriNyuryoku(WINDOW_TYPE.NEW_WINDOW_FLAG, 11);
                //var businessForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.UIFooterForm(callForm, WINDOW_TYPE.NEW_WINDOW_FLAG);

                //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                //if (!isExistForm)
                //{
                //    businessForm.Show();
                //}

                DataGridViewCell datagridviewcell = this.myForm.customDataGridView1.CurrentCell;

                // 引数遷移先パラメータ設定dto
                FormShowParamDao formParem = new FormShowParamDao();

                if (datagridviewcell.RowIndex >= 0)
                {
                    // 見積番号設定
                    formParem.mitsumoriNumber = this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_MITUSMORI_NUMBER].Value.ToString();
                    // 帳票種別設定
                    formParem.cyohyoType = int.Parse(this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_MITSUMORI_SHOSHIKI_KBN].Value.ToString());
                }

                // 修正権限チェック
                if (Manager.CheckAuthority("G276", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    //フォーム起動
                    FormManager.OpenForm("G276", WINDOW_TYPE.UPDATE_WINDOW_FLAG, formParem);
                }
                else if (Manager.CheckAuthority("G276", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 修正権限が無く、参照権限がある場合は参照モードで起動する
                    FormManager.OpenForm("G276", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, formParem);
                }
                else
                {
                    // 修正・参照権限が無い場合は、修正権限なしのエラーを表示する
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func3_Click", ex);
                throw ex;
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

                DataGridViewCell datagridviewcell = this.myForm.customDataGridView1.CurrentCell;

                // 引数遷移先パラメータ設定dto
                FormShowParamDao formParem = new FormShowParamDao();

                if (datagridviewcell.RowIndex >= 0)
                {
                    // 見積番号設定
                    formParem.mitsumoriNumber = this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_MITUSMORI_NUMBER].Value.ToString();
                    // 帳票種別設定
                    formParem.cyohyoType = int.Parse(this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_MITSUMORI_SHOSHIKI_KBN].Value.ToString());
                }

                // 削除権限がある場合入力画面表示
                FormManager.OpenFormWithAuth("G276", WINDOW_TYPE.DELETE_WINDOW_FLAG, WINDOW_TYPE.DELETE_WINDOW_FLAG, formParem);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func4_Click", ex);
                throw ex;
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

                //FormShowParamDao prmDto = new FormShowParamDao();
                //DataGridViewCell datagridviewcell = this.myForm.customDataGridView1.CurrentCell;
                //if (datagridviewcell.RowIndex >= 0)
                //{
                //    prmDto.windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                //    prmDto.systemID =
                //        this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["システムID"].Value.ToString();
                //    prmDto.detailSystemID =
                //        this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["枝番"].Value.ToString();
                //    prmDto.mitsumoriNumber =
                //        this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["伝票番号"].Value.ToString();
                //    prmDto.cyohyoType =
                //        int.Parse(this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["見積書式区分"].Value.ToString());
                //}

                //var headerForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.HeaderForm();
                ////var callForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.MitsumoriNyuryoku(prmDto);
                //var callForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.MitsumoriNyuryoku(WINDOW_TYPE.NEW_WINDOW_FLAG, 11);
                //var businessForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.UIFooterForm(callForm, WINDOW_TYPE.NEW_WINDOW_FLAG);

                //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                //if (!isExistForm)
                //{
                //    businessForm.Show();
                //}

                DataGridViewCell datagridviewcell = this.myForm.customDataGridView1.CurrentCell;

                // 引数遷移先パラメータ設定dto
                FormShowParamDao formParem = new FormShowParamDao();

                if (datagridviewcell.RowIndex >= 0)
                {
                    // 見積番号設定
                    formParem.mitsumoriNumber = this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_MITUSMORI_NUMBER].Value.ToString();
                    // 帳票種別設定
                    formParem.cyohyoType = int.Parse(this.myForm.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_MITSUMORI_SHOSHIKI_KBN].Value.ToString());
                }

                // 新規権限がある場合入力画面表示
                FormManager.OpenFormWithAuth("G276", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, formParem);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func5_Click", ex);
                throw ex;
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

                if (this.myForm.customDataGridView1.RowCount == 0)
                {
                    // アラートを表示し、CSV出力処理はしない
                    MessageBoxUtility.MessageBoxShow("E044");
                }
                else
                {
                    if (MessageBoxUtility.MessageBoxShow("C012") == DialogResult.Yes)			// CSV出力しますか？
                    {
                        CSVExport csvExp = new CSVExport();
                        csvExp.ConvertCustomDataGridViewToCsv(this.myForm.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_MITSUMORI_ICHRAN), this.myForm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.headForm.KYOTEN_CD.IsInputErrorOccured = false;
                this.headForm.HIDUKE_FROM.IsInputErrorOccured = false;
                this.headForm.HIDUKE_TO.IsInputErrorOccured = false;
                this.headForm.alertNumber.IsInputErrorOccured = false;
                this.myForm.txtBox_Eigyotantosya.IsInputErrorOccured = false;
                this.myForm.txtNum_Jyoukyou.IsInputErrorOccured = false;
                this.myForm.numTxtbox_TrhkskCD.IsInputErrorOccured = false;
                this.myForm.numTxtBox_GyousyaCD.IsInputErrorOccured = false;
                this.myForm.numTxtBox_GbCD.IsInputErrorOccured = false;

                //this.headForm.KYOTEN_CD.Clear();          // No.2292
                //this.headForm.KYOTEN_NAME_RYAKU.Clear();  // No.2292
                //this.headForm.radbtnDenpyouHiduke.Checked = true;  // No.2292
                var parentForm = (BusinessBaseForm)this.myForm.Parent;
                this.headForm.HIDUKE_FROM.Value = parentForm.sysDate.Date;
                this.headForm.HIDUKE_TO.Value = parentForm.sysDate.Date;
                //this.headForm.HIDUKE_FROM.Value = null;  // No.2292
                //this.headForm.HIDUKE_TO.Value = null;  // No.2292
                this.headForm.ReadDataNumber.Text = "0";
                this.myForm.customSortHeader1.ClearCustomSortSetting();
                this.myForm.customSearchHeader1.ClearCustomSearchSetting();
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.myForm.SystemId.ToString());
                if (sysInfo != null)
                {
                    // システム情報からアラート件数を取得
                    this.headForm.alertNumber.Text = sysInfo.ICHIRAN_ALERT_KENSUU.ToString();
                }

                this.myForm.txtBox_Eigyotantosya.Clear();
                this.myForm.txtBox_Eigyosyamei.Clear();
                //this.myForm.txtNum_Jyoukyou.Text = "1";  // No.2292
                //this.myForm.radbtn_Subete.Checked = true;  // No.2292
                this.myForm.numTxtbox_TrhkskCD.Clear();
                this.myForm.txtBox_TrhkskName.Clear();
                this.myForm.numTxtBox_GyousyaCD.Clear();
                this.myForm.txtBox_GyousyaName.Clear();
                this.myForm.numTxtBox_GbCD.Clear();
                this.myForm.txtBox_GbName.Clear();
                this.myForm.customSortHeader1.ClearCustomSortSetting();
                //this.myForm.customDataGridView1.DataSource = null;

                //this.myForm.SetGridViewTitle();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func7_Click", ex);
                throw ex;
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

                if (this.myForm.PatternNo == 0 || string.IsNullOrEmpty(this.myForm.SelectQuery))
                {
                    MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                var parentForm = (BusinessBaseForm)this.myForm.Parent;
                bool catchErr = false;
                bool isErr = this.KensakuJyoukenCheck(out catchErr);
                if (catchErr || isErr)
                {
                    return;
                }

                //int cnt = MitumoriItiranKensaku();
                int cnt = this.Search();
                if (cnt == -1) { return; }

                if (cnt > 0)
                {
                    parentForm.bt_func1.Enabled = false;
                    //parentForm.bt_func2.Enabled = true;
                    parentForm.bt_func3.Enabled = true;
                    parentForm.bt_func4.Enabled = true;
                    parentForm.bt_func5.Enabled = true;
                    parentForm.bt_func6.Enabled = true;
                }
                else
                {
                    parentForm.bt_func1.Enabled = false;
                    //parentForm.bt_func2.Enabled = true;
                    parentForm.bt_func3.Enabled = false;
                    parentForm.bt_func4.Enabled = false;
                    parentForm.bt_func5.Enabled = false;
                    parentForm.bt_func6.Enabled = false;
                }

                //読込データ件数を取得
                if (this.myForm.customDataGridView1 != null)
                {
                    this.headForm.ReadDataNumber.Text = this.myForm.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headForm.ReadDataNumber.Text = "0";
                }

                if (this.headForm.ReadDataNumber.Text == "0")
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func8_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.myForm.customSortHeader1.ShowCustomSortSettingDialog();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func10_Click", ex);
                throw ex;
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
            this.myForm.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.myForm.customDataGridView1 != null)
            {
                this.headForm.ReadDataNumber.Text = this.myForm.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headForm.ReadDataNumber.Text = "0";
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

                //拠点、伝票日付From、伝票日付To、確定区分、伝票種類の項目をセッティングファイルに保存する。
                //Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;
                //Properties.Settings.Default.SET_KYOTEN_NAME_RYAKU = this.headForm.KYOTEN_NAME_RYAKU.Text;

                //if (!String.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text.Trim()))
                //{
                //    Properties.Settings.Default.SET_HIDUKE_FROM = this.headForm.HIDUKE_FROM.Text;
                //}
                //if (!String.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text.Trim()))
                //{
                //    Properties.Settings.Default.SET_HIDUKE_TO = this.headForm.HIDUKE_TO.Text;
                //}

                var parentForm = (BusinessBaseForm)this.myForm.Parent;
                this.myForm.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func12_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region プロセスボタン押下処理

        /// <summary>
        /// 検索条件設定画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var sysID = this.myForm.OpenPatternIchiran();

                if (!string.IsNullOrEmpty(sysID))
                {
                    this.myForm.SetPatternBySysId(sysID);
                    this.myForm.ShowData();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
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
            ////LogUtility.DebugMethodStart();

            //if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            //{
            //    if (!string.IsNullOrEmpty(this.myForm.searchString.Text))
            //    {
            //        string getSearchString = this.myForm.searchString.Text.Replace("\r", "").Replace("\n", "");
            //        this.searchString = getSearchString;
            //        this.Search();

            //    }
            //    else
            //    {
            //        this.myForm.searchString.Text = "";
            //        this.myForm.searchString.SelectionLength = this.myForm.searchString.Text.Length;
            //        this.myForm.searchString.Focus();
            //    }

            //}

            ////LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 処理No(ESC)でのエンターキー押下イベント(※遷移先画面が存在しない為、未実装)

        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void txb_process_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.myForm.Parent;

                if ("1".Equals(parentForm.txb_process.Text))
                {
                    // 検索条件設定画面へ遷移
                    //var us = new KensakujoukenSetteiForm(this.myForm.DenshuKbn);
                    //us.Show();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("txb_process_KeyDown", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 取引先チェック

        /// <summary>
        /// 取引先チェック
        /// </summary>
        /// <param name="torihikisakiCode"></param>
        public bool TorihikisakiCheck(string torihikisakiCode)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCode);

                M_TORIHIKISAKI keyEntity = new M_TORIHIKISAKI();
                keyEntity.TORIHIKISAKI_CD = torihikisakiCode;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity);
                if (torihikisaki == null || torihikisaki.Length == 0)
                {
                    M_HIKIAI_TORIHIKISAKI keyEntity1 = new M_HIKIAI_TORIHIKISAKI();
                    keyEntity1.TORIHIKISAKI_CD = torihikisakiCode;
                    keyEntity1.ISNOT_NEED_DELETE_FLG = true;
                    var torihikisaki1 = this.hikiaiTorihikisakiDao.GetAllValidData(keyEntity1);
                    if (torihikisaki1 != null && torihikisaki1.Length > 0)
                    {
                        this.myForm.chkBox_Trhksk.Text = "1";
                        this.myForm.chkBox_Trhksk.Checked = true;
                    }
                }
                else
                {
                    this.myForm.chkBox_Trhksk.Text = "0";
                    this.myForm.chkBox_Trhksk.Checked = false;
                }

                // 取引先●
                if (!string.IsNullOrEmpty(torihikisakiCode) && (this.myForm.chkBox_Trhksk.Text.Equals("0") || string.IsNullOrEmpty(this.myForm.chkBox_Trhksk.Text)))
                {
                    // 取引先マスタ検索
                    this.seachTorihikisaki(torihikisakiCode);
                    this.myForm.chkBox_Trhksk.Text = "0";
                    this.myForm.chkBox_Trhksk.Checked = false;
                }
                else if (!string.IsNullOrEmpty(torihikisakiCode) && (this.myForm.chkBox_Trhksk.Text.Equals("1")))
                {
                    // 引合取引先マスタ検索
                    this.seachHikiaiTorihikisaki(torihikisakiCode);
                    this.myForm.chkBox_Trhksk.Text = "1";
                    this.myForm.chkBox_Trhksk.Checked = true;
                }
                else if (string.IsNullOrEmpty(torihikisakiCode))
                {
                    // 空に設定
                    this.myForm.chkBox_Trhksk.Text = "0";
                    this.myForm.chkBox_Trhksk.Checked = false;
                    this.myForm.txtBox_TrhkskName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiCheck", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret;
        }

        #endregion

        #region 取引先マスタ検索

        /// <summary>
        /// 取引先マスタ検索
        /// </summary>
        /// <param name="torihikisakiCode"></param>
        public void seachTorihikisaki(string torihikisakiCode)
        {
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCode);

                M_TORIHIKISAKI keyEntity = new M_TORIHIKISAKI();
                keyEntity.TORIHIKISAKI_CD = torihikisakiCode;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity);

                if (torihikisaki.Length == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "取引先");

                    this.myForm.txtBox_TrhkskName.Clear();
                    this.myForm.numTxtBox_GyousyaCD.Clear();
                    this.myForm.txtBox_GyousyaName.Clear();
                    this.myForm.numTxtBox_GbCD.Clear();
                    this.myForm.txtBox_GbName.Clear();
                    this.myForm.numTxtbox_TrhkskCD.IsInputErrorOccured = true;
                    this.myForm.numTxtbox_TrhkskCD.Focus();
                }
                else
                {
                    this.myForm.txtBox_TrhkskName.Text = torihikisaki[0].TORIHIKISAKI_NAME_RYAKU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("seachTorihikisaki", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 引合取引先マスタ検索

        /// <summary>
        /// 引合取引先マスタ検索
        /// </summary>
        /// <param name="torihikisakiCode"></param>
        public void seachHikiaiTorihikisaki(string torihikisakiCode)
        {
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCode);

                M_HIKIAI_TORIHIKISAKI keyEntity = new M_HIKIAI_TORIHIKISAKI();
                keyEntity.TORIHIKISAKI_CD = torihikisakiCode;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var torihikisaki = this.hikiaiTorihikisakiDao.GetAllValidData(keyEntity);

                if (torihikisaki.Length == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "引合取引先");

                    this.myForm.txtBox_TrhkskName.Clear();
                    this.myForm.numTxtBox_GyousyaCD.Clear();
                    this.myForm.txtBox_GyousyaName.Clear();
                    this.myForm.numTxtBox_GbCD.Clear();
                    this.myForm.txtBox_GbName.Clear();
                    this.myForm.numTxtbox_TrhkskCD.IsInputErrorOccured = true;
                    this.myForm.numTxtbox_TrhkskCD.Focus();
                }
                else
                {
                    this.myForm.txtBox_TrhkskName.Text = torihikisaki[0].TORIHIKISAKI_NAME_RYAKU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("seachHikiaiTorihikisaki", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 業者チェック

        /// <summary>
        /// 業者チェック
        /// </summary>
        /// <param name="gyousyaCode"></param>
        /// <param name="strOldGyousyaCD"></param>
        public bool GyousyaCheck(string gyousyaCode, string strOldGyousyaCD)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(gyousyaCode, strOldGyousyaCD);

                if (this.myForm.ManualInputFlg)
                {
                    M_GYOUSHA keyEntity = new M_GYOUSHA();
                    keyEntity.GYOUSHA_CD = gyousyaCode;
                    keyEntity.ISNOT_NEED_DELETE_FLG = true;
                    var gyousya = this.gyoushaDao.GetAllValidData(keyEntity);
                    if (gyousya == null || gyousya.Length == 0)
                    {
                        M_HIKIAI_GYOUSHA keyEntity1 = new M_HIKIAI_GYOUSHA();
                        keyEntity1.GYOUSHA_CD = gyousyaCode;
                        keyEntity1.ISNOT_NEED_DELETE_FLG = true;
                        var gyousya1 = this.hikiaiGyousyaDao.GetAllValidData(keyEntity1);
                        if (gyousya1 != null && gyousya1.Length > 0)
                        {
                            this.myForm.chkBox_Gyosya.Text = "1";
                        }
                    }
                    else
                    {
                        this.myForm.chkBox_Gyosya.Text = "0";
                    }
                }

                // 業者●
                if (!string.IsNullOrEmpty(gyousyaCode) && (this.myForm.chkBox_Gyosya.Text.Equals("0") || string.IsNullOrEmpty(this.myForm.chkBox_Gyosya.Text) || this.myForm.chkBox_Gyosya.Text.Equals("False")))
                {
                    // 業者マスタ検索
                    this.seachGyousya(gyousyaCode, strOldGyousyaCD);
                }
                else if (!string.IsNullOrEmpty(gyousyaCode) && (this.myForm.chkBox_Gyosya.Text.Equals("1") || this.myForm.chkBox_Gyosya.Text.Equals("True")))
                {
                    // 引合業者マスタ検索
                    this.seachHikiaiGyousya(gyousyaCode, strOldGyousyaCD);
                }
                else if (string.IsNullOrEmpty(gyousyaCode))
                {
                    // 業者及び現場を空に設定
                    this.myForm.chkBox_Gyosya.Text = "0";
                    this.myForm.chkBox_Gyosya.Checked = false;
                    this.myForm.txtBox_GyousyaName.Text = string.Empty;
                    this.myForm.numTxtBox_GbCD.Text = string.Empty;
                    this.myForm.chkBox_Gb.Text = "0";
                    this.myForm.chkBox_Gb.Checked = false;
                    this.myForm.txtBox_GbName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyousyaCheck", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret;
        }

        #endregion

        #region 業者マスタ検索

        /// <summary>
        /// 業者マスタ検索
        /// </summary>
        /// <param name="gyousyaCode"></param>
        /// <param name="strOldGyousyaCD"></param>
        public void seachGyousya(string gyousyaCode, string strOldGyousyaCD)
        {
            try
            {
                LogUtility.DebugMethodStart(gyousyaCode, strOldGyousyaCD);

                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyousyaCode;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var gyousya = this.gyoushaDao.GetAllValidData(keyEntity);

                if (gyousya.Length == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "業者");

                    this.myForm.txtBox_GyousyaName.Clear();
                    this.myForm.numTxtBox_GbCD.Clear();
                    this.myForm.txtBox_GbName.Clear();
                    this.myForm.numTxtBox_GyousyaCD.IsInputErrorOccured = true;
                    this.myForm.numTxtBox_GyousyaCD.Focus();
                }
                else
                {
                    this.myForm.txtBox_GyousyaName.Text = gyousya[0].GYOUSHA_NAME_RYAKU;
                    if (!this.myForm.numTxtBox_GyousyaCD.Text.Equals(strOldGyousyaCD))
                    {
                        this.myForm.numTxtBox_GbCD.Clear();
                        this.myForm.txtBox_GbName.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("seachGyousya", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 引合業者マスタ検索

        /// <summary>
        /// 引合業者マスタ検索
        /// </summary>
        /// <param name="gyousyaCode"></param>
        /// <param name="strOldGyousyaCD"></param>
        public void seachHikiaiGyousya(string gyousyaCode, string strOldGyousyaCD)
        {
            try
            {
                LogUtility.DebugMethodStart(gyousyaCode, strOldGyousyaCD);

                M_HIKIAI_GYOUSHA keyEntity = new M_HIKIAI_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyousyaCode;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var gyousya = this.hikiaiGyousyaDao.GetAllValidData(keyEntity);

                if (gyousya.Length == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "引合業者");

                    this.myForm.txtBox_GyousyaName.Clear();
                    this.myForm.numTxtBox_GbCD.Clear();
                    this.myForm.txtBox_GbName.Clear();
                    this.myForm.numTxtBox_GyousyaCD.IsInputErrorOccured = true;
                    this.myForm.numTxtBox_GyousyaCD.Focus();
                }
                else
                {
                    this.myForm.txtBox_GyousyaName.Text = gyousya[0].GYOUSHA_NAME_RYAKU;
                    if (this.myForm.numTxtBox_GyousyaCD.Text.Equals(strOldGyousyaCD))
                    {
                        this.myForm.numTxtBox_GbCD.Clear();
                        this.myForm.txtBox_GbName.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("seachHikiaiGyousya", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 現場チェック

        /// <summary>
        /// 現場チェック
        /// </summary>
        /// <param name="gyousyaCode"></param>
        /// <param name="genbaCode"></param>
        public bool GenbaCheck(string gyousyaCode, string genbaCode)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(gyousyaCode, genbaCode);

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyousyaCode;
                keyEntity.GENBA_CD = genbaCode;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.genbaDao.GetAllValidData(keyEntity);
                if (genba == null || genba.Length == 0)
                {
                    M_HIKIAI_GENBA keyEntity1 = new M_HIKIAI_GENBA();
                    keyEntity1.GYOUSHA_CD = gyousyaCode;
                    keyEntity1.GENBA_CD = genbaCode;
                    keyEntity1.HIKIAI_GYOUSHA_USE_FLG = this.myForm.chkBox_Gyosya.Text == "1";
                    keyEntity1.ISNOT_NEED_DELETE_FLG = true;
                    var genba1 = this.hikiaiGenbaDao.GetAllValidData(keyEntity1);
                    if (genba1 != null && genba1.Length != 0)
                    {
                        this.myForm.chkBox_Gb.Text = "1";
                    }
                }
                else
                {
                    this.myForm.chkBox_Gb.Text = "0";
                }

                // 現場●
                if (!string.IsNullOrEmpty(genbaCode) && (this.myForm.chkBox_Gb.Text.Equals("0") || string.IsNullOrEmpty(this.myForm.chkBox_Gb.Text)))
                {
                    // 現場マスタ検索
                    this.seachGenba(gyousyaCode, genbaCode);
                }
                else if (!string.IsNullOrEmpty(genbaCode) && (this.myForm.chkBox_Gb.Text.Equals("1")))
                {
                    // 引合現場マスタ検索
                    this.seachHikiaiGenba(gyousyaCode, genbaCode);
                }
                else if (string.IsNullOrEmpty(genbaCode))
                {
                    // 空に設定
                    this.myForm.chkBox_Gb.Text = "0";
                    this.myForm.chkBox_Gb.Checked = false;
                    this.myForm.txtBox_GbName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaCheck", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret;
        }

        #endregion

        #region 現場マスタ検索

        /// <summary>
        /// 現場マスタ検索
        /// </summary>
        /// <param name="gyousyaCode"></param>
        /// <param name="genbaCode"></param>
        public void seachGenba(string gyousyaCode, string genbaCode)
        {
            try
            {
                LogUtility.DebugMethodStart(gyousyaCode, genbaCode);

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyousyaCode;
                keyEntity.GENBA_CD = genbaCode;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba.Length == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "現場");

                    this.myForm.txtBox_GbName.Clear();
                    this.myForm.numTxtBox_GbCD.IsInputErrorOccured = true;
                    this.myForm.numTxtBox_GbCD.Focus();
                }
                else
                {
                    this.myForm.txtBox_GbName.Text = genba[0].GENBA_NAME_RYAKU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("seachGenba", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 引合現場マスタ検索

        /// <summary>
        /// 引合現場マスタ検索
        /// </summary>
        /// <param name="gyousyaCode"></param>
        public void seachHikiaiGenba(string gyousyaCode, string genbaCode)
        {
            try
            {
                LogUtility.DebugMethodStart(gyousyaCode, genbaCode);

                M_HIKIAI_GENBA keyEntity = new M_HIKIAI_GENBA();
                keyEntity.GYOUSHA_CD = gyousyaCode;
                keyEntity.GENBA_CD = genbaCode;
                keyEntity.HIKIAI_GYOUSHA_USE_FLG = this.myForm.chkBox_Gyosya.Text == "1";
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.hikiaiGenbaDao.GetAllValidData(keyEntity);

                if (genba.Length == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "引合現場");

                    this.myForm.txtBox_GbName.Clear();
                    this.myForm.numTxtBox_GbCD.IsInputErrorOccured = true;
                    this.myForm.numTxtBox_GbCD.Focus();
                }
                else
                {
                    this.myForm.txtBox_GbName.Text = genba[0].GENBA_NAME_RYAKU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("seachHikiaiGenba", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 明細データダブルクリックイベント

        private void customDataGridView1_MouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0)
                {
                    return;
                }

                // 引数遷移先パラメータ設定dto
                FormShowParamDao formParem = new FormShowParamDao();

                // 見積番号設定
                formParem.mitsumoriNumber = this.myForm.customDataGridView1.Rows[e.RowIndex].Cells[this.HIDDEN_MITUSMORI_NUMBER].Value.ToString();
                // 帳票種別設定
                formParem.cyohyoType = int.Parse(this.myForm.customDataGridView1.Rows[e.RowIndex].Cells[this.HIDDEN_MITSUMORI_SHOSHIKI_KBN].Value.ToString());

                // 修正権限チェック
                if (Manager.CheckAuthority("G276", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    //フォーム起動
                    FormManager.OpenForm("G276", WINDOW_TYPE.UPDATE_WINDOW_FLAG, formParem);
                }
                else if (Manager.CheckAuthority("G276", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 修正権限が無く、参照権限がある場合は参照モードで起動する
                    FormManager.OpenForm("G276", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, formParem);
                }
                else
                {
                    // 修正・参照権限が無い場合は、修正権限なしのエラーを表示する
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("customDataGridView1_MouseDoubleClick", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void customDataGridView1_CellContentClick(object sender, MouseEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.myForm.Parent;
                parentForm.bt_func1.Enabled = false;
                parentForm.bt_func2.Enabled = true;
                parentForm.bt_func3.Enabled = true;
                parentForm.bt_func4.Enabled = true;
                parentForm.bt_func5.Enabled = true;
                parentForm.bt_func6.Enabled = true;
                parentForm.bt_func7.Enabled = true;
                parentForm.bt_func8.Enabled = true;
                //parentForm.bt_func9.Enabled = false;
                parentForm.bt_func10.Enabled = true;
                //parentForm.bt_func11.Enabled = false;
                parentForm.bt_func12.Enabled = true;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("customDataGridView1_CellContentClick", ex);
                throw ex;
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

                LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath));
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("CreateButtonInfo", ex);
                throw ex;
            }
        }

        #endregion

        #region 全角の英数字の文字列を半角に変換

        /// <summary>
        /// 全角スペースを半角スペースに変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankakuSpace(string param)
        {
            try
            {
                LogUtility.DebugMethodStart(param);

                Regex re = new Regex("[　]+");
                string output = re.Replace(param, MyReplacer);

                LogUtility.DebugMethodEnd(output);
                return output;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("ToHankakuSpace", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankaku(string param)
        {
            try
            {
                LogUtility.DebugMethodStart(param);

                Regex re = new Regex("[０-９Ａ-Ｚａ-ｚ　]+");
                string output = re.Replace(param, MyReplacer);

                LogUtility.DebugMethodEnd(output);
                return output;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("ToHankaku", ex);
                throw ex;
            }
        }

        private static string MyReplacer(Match m)
        {
            try
            {
                LogUtility.DebugMethodStart(m);

                LogUtility.DebugMethodEnd(Strings.StrConv(m.Value, VbStrConv.Narrow, 0));
                return Strings.StrConv(m.Value, VbStrConv.Narrow, 0);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("MyReplacer", ex);
                throw ex;
            }
        }

        #endregion

        #region 見積一覧取得処理

        /// <summary>
        /// 見積一覧取得処理
        /// </summary>
        /// <returns>count</returns>
        public virtual int MitumoriItiranKensaku()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SetSearchCondition();

                DataTable dtKskk = MitumoriItiranDao.GetMitumoriIchiran(this.MitumoriKsjk);

                DataTable dtEdit = new DataTable();

                // 列名の設定
                for (int i = 0; i < dtKskk.Columns.Count; i++)
                {
                    //#受入試験指摘修正(数値表示項目のソート)Start
                    if (i == 2)
                    {
                        dtEdit.Columns.Add(dtKskk.Columns[i].ColumnName, typeof(long));
                    }
                    else if (i >= 6 && i <= 10)
                    {
                        dtEdit.Columns.Add(dtKskk.Columns[i].ColumnName, typeof(decimal));
                    }
                    else
                    {
                        dtEdit.Columns.Add(dtKskk.Columns[i].ColumnName, typeof(string));
                    }
                    //dtEdit.Columns.Add(dtKskk.Columns[i].ColumnName, typeof(string));
                    //#受入試験指摘修正(数値表示項目のソート)End
                }

                for (int i = 0; i < dtKskk.Rows.Count; i++)
                {
                    DataRow dtEditRow = dtEdit.NewRow();
                    dtEditRow[0] = dtKskk.Rows[i][0].ToString();
                    dtEditRow[1] = dtKskk.Rows[i][1].ToString();
                    dtEditRow[2] = Int32.Parse(dtKskk.Rows[i][2].ToString());
                    dtEditRow[3] = ((DateTime)dtKskk.Rows[i][3]).ToString("yyyy/MM/dd");
                    dtEditRow[4] = dtKskk.Rows[i][4].ToString();
                    dtEditRow[5] = dtKskk.Rows[i][5].ToString();
                    //#受入試験指摘修正(数値表示項目のソート)Start
                    //dtEditRow[6] = SetComma(dtKskk.Rows[i][6].ToString());
                    //dtEditRow[7] = SetComma(dtKskk.Rows[i][7].ToString());
                    //dtEditRow[8] = SetComma(dtKskk.Rows[i][8].ToString());
                    //dtEditRow[9] = SetComma(dtKskk.Rows[i][9].ToString());
                    //dtEditRow[10] = SetComma(dtKskk.Rows[i][10].ToString());

                    dtEditRow[6] = SetComma(dtKskk.Rows[i][6]);
                    dtEditRow[7] = SetComma(dtKskk.Rows[i][7]);
                    dtEditRow[8] = SetComma(dtKskk.Rows[i][8]);
                    dtEditRow[9] = SetComma(dtKskk.Rows[i][9]);
                    dtEditRow[10] = SetComma(dtKskk.Rows[i][10]);
                    //#受入試験指摘修正(数値表示項目のソート)End
                    dtEditRow[11] = dtKskk.Rows[i][11].ToString();
                    dtEditRow[12] = ((DateTime)dtKskk.Rows[i][12]).ToString("yyyy/MM/dd");
                    dtEditRow[13] = dtKskk.Rows[i][13].ToString();

                    dtEdit.Rows.Add(dtEditRow);
                }

                this.MitumoriKskk = dtEdit;

                int cnt = this.MitumoriKskk.Rows.Count;

                this.alertCount = int.Parse(this.headForm.alertNumber.Text.Replace(",", ""));
                this.headForm.ReadDataNumber.Text = cnt.ToString();
                LogUtility.DebugMethodEnd(cnt);
                return cnt;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("MitumoriItiranKensaku", ex);
                throw ex;
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
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.myForm.SelectQuery))
                {
                    return count;
                }

                // 検索条件をセットします
                this.SetSearchCondition();

                // 検索文字列を作成して取得します
                var sql = this.GetSearchString();

                DialogResult result = DialogResult.Yes;

                // 検索実行
                this.myForm.Table = daoIchiran.getdateforstringsql(sql);

                // アラート件数チェック
                int alertNumber = 0;
                if (!string.IsNullOrEmpty(this.headForm.alertNumber.Text))
                {
                    alertNumber = int.Parse(this.headForm.alertNumber.Text.Replace(",", ""));
                }
                if (alertNumber != 0 && alertNumber < this.myForm.Table.Rows.Count)
                {
                    // 件数アラート
                    result = MessageBoxUtility.MessageBoxShow("C025");
                }

                //読込データ件数を取得
                count = this.myForm.Table.Rows.Count;
                this.headForm.ReadDataNumber.Text = count.ToString();

                if (result == DialogResult.Yes)
                {
                    //検索結果表示
                    this.myForm.ShowData();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
            return count;
        }

        /// <summary>
        /// 検索文字列を作成して取得します
        /// </summary>
        /// <returns></returns>
        private string GetSearchString()
        {
            var sql = new StringBuilder();

            #region SELECT句

            sql.Append("SELECT DISTINCT ");
            sql.Append(this.myForm.SelectQuery);
            // システムID
            sql.AppendFormat(", T_MITSUMORI_ENTRY.SYSTEM_ID AS {0} ", this.HIDDEN_SYSTEM_ID);
            // 見積番号（伝票番号）
            sql.AppendFormat(", T_MITSUMORI_ENTRY.MITSUMORI_NUMBER AS {0} ", this.HIDDEN_MITUSMORI_NUMBER);
            // 見積書書式区分
            sql.AppendFormat(", T_MITSUMORI_ENTRY.MITSUMORI_SHOSHIKI_KBN AS {0} ", this.HIDDEN_MITSUMORI_SHOSHIKI_KBN);
            if (this.myForm.CurrentPatternOutputKbn == (int)OUTPUT_KBN.MEISAI)
            {
                // 明細システムID
                sql.AppendFormat(", T_MITSUMORI_DETAIL.DETAIL_SYSTEM_ID AS {0} ", this.HIDDEN_DETAIL_SYSTEM_ID);
            }

            #endregion

            #region FROM句

            // 見積入力
            sql.Append(" FROM T_MITSUMORI_ENTRY ");
            if (this.myForm.CurrentPatternOutputKbn == (int)OUTPUT_KBN.MEISAI)
            {
                // 見積明細
                sql.Append(" LEFT JOIN T_MITSUMORI_DETAIL ");
                sql.Append(" ON T_MITSUMORI_ENTRY.SYSTEM_ID = T_MITSUMORI_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_MITSUMORI_ENTRY.SEQ = T_MITSUMORI_DETAIL.SEQ ");
            }

            sql.Append(this.myForm.JoinQuery);

            #endregion

            #region WHERE句

            // 削除フラグ
            sql.Append(" WHERE T_MITSUMORI_ENTRY.DELETE_FLG = 0 ");

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Jokyo_flg))
            {
                // 状況CD
                sql.AppendFormat(" AND T_MITSUMORI_ENTRY.JOKYO_FLG = {0} ", this.MitumoriKsjk.Jokyo_flg);
            }

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Kyoten_cd) && this.MitumoriKsjk.Kyoten_cd != "99")
            {
                // 拠点CD
                sql.AppendFormat(" AND T_MITSUMORI_ENTRY.KYOTEN_CD = {0} ", this.MitumoriKsjk.Kyoten_cd);
            }

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Mitsumori_Fdate))
            {
                // 見積日付（From）
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, T_MITSUMORI_ENTRY.MITSUMORI_DATE, 111), 120) >= ");
                sql.AppendFormat("CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) ", this.MitumoriKsjk.Mitsumori_Fdate);
            }

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Mitsumori_Tdate))
            {
                // 見積日付（To）
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, T_MITSUMORI_ENTRY.MITSUMORI_DATE, 111), 120) <= ");
                sql.AppendFormat("CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) ", this.MitumoriKsjk.Mitsumori_Tdate);
            }

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Update_Fdate))
            {
                // 最終更新日付（From）
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, T_MITSUMORI_ENTRY.UPDATE_DATE, 111), 120) >= ");
                sql.AppendFormat("CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) ", this.MitumoriKsjk.Update_Fdate);
            }

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Update_Tdate))
            {
                // 最終更新日付（To）
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, T_MITSUMORI_ENTRY.UPDATE_DATE, 111), 120) <= ");
                sql.AppendFormat("CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) ", this.MitumoriKsjk.Update_Tdate);
            }

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Shain_cd))
            {
                // 社員CD
                sql.AppendFormat(" AND T_MITSUMORI_ENTRY.SHAIN_CD = '{0}' ", this.MitumoriKsjk.Shain_cd);
            }

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Torihikisaki_cd))
            {
                // 取引先CD
                sql.AppendFormat(" AND T_MITSUMORI_ENTRY.TORIHIKISAKI_CD = '{0}' ", this.MitumoriKsjk.Torihikisaki_cd);
                if (!string.IsNullOrEmpty(this.MitumoriKsjk.Hikiai_torihikisaki_flg))
                {
                    // 引合取引先フラグ
                    sql.AppendFormat(" AND T_MITSUMORI_ENTRY.HIKIAI_TORIHIKISAKI_FLG = {0} ", this.MitumoriKsjk.Hikiai_torihikisaki_flg);
                }
            }

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Gyousha_cd))
            {
                // 業者CD
                sql.AppendFormat(" AND T_MITSUMORI_ENTRY.GYOUSHA_CD = '{0}' ", this.MitumoriKsjk.Gyousha_cd);
                if (!string.IsNullOrEmpty(this.MitumoriKsjk.Hikiai_gyousha_flg))
                {
                    // 引合業者フラグ
                    sql.AppendFormat(" AND T_MITSUMORI_ENTRY.HIKIAI_GYOUSHA_FLG = {0} ", this.MitumoriKsjk.Hikiai_gyousha_flg);
                }
            }

            if (!string.IsNullOrEmpty(this.MitumoriKsjk.Genba_cd))
            {
                // 現場CD
                sql.AppendFormat(" AND T_MITSUMORI_ENTRY.GENBA_CD = '{0}' ", this.MitumoriKsjk.Genba_cd);
                if (!string.IsNullOrEmpty(this.MitumoriKsjk.Hikiai_genba_flg))
                {
                    // 引合現場フラグ
                    sql.AppendFormat(" AND T_MITSUMORI_ENTRY.HIKIAI_GENBA_FLG = {0} ", this.MitumoriKsjk.Hikiai_genba_flg);
                }
            }

            #endregion

            #region ORDER BY句

            if (!string.IsNullOrEmpty(this.myForm.OrderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.myForm.OrderByQuery);
            }

            #endregion

            return sql.ToString();
        }

        /// <summary>
        /// 検索条件設定
        /// </summary>
        private void SetSearchCondition()
        {
            this.MitumoriKsjk = new MitumoriItiranKsjkDTO();

            // 検索条件設定
            if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
            {
                this.MitumoriKsjk.Kyoten_cd = this.headForm.KYOTEN_CD.Text;
            }

            if (this.headForm.radbtnDenpyouHiduke.Checked == true)
            {
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
                {
                    this.MitumoriKsjk.Mitsumori_Fdate = this.headForm.HIDUKE_FROM.Text.Substring(0, 10);
                }
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text))
                {
                    this.MitumoriKsjk.Mitsumori_Tdate = this.headForm.HIDUKE_TO.Text.Substring(0, 10);
                }
            }

            if (this.headForm.radbtnNyuuryokuHiduke.Checked == true)
            {
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
                {
                    this.MitumoriKsjk.Update_Fdate = this.headForm.HIDUKE_FROM.Text.Substring(0, 10);
                }
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text))
                {
                    this.MitumoriKsjk.Update_Tdate = this.headForm.HIDUKE_TO.Text.Substring(0, 10);
                }
            }

            if (!string.IsNullOrEmpty(this.myForm.txtBox_Eigyotantosya.Text))
            {
                this.MitumoriKsjk.Shain_cd = this.myForm.txtBox_Eigyotantosya.Text;
            }
            if (this.myForm.txtNum_Jyoukyou.Text == "1" ||
                // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
                //this.myForm.txtNum_Jyoukyou.Text == "2" ||
                //this.myForm.txtNum_Jyoukyou.Text == "3")
                this.myForm.txtNum_Jyoukyou.Text == "2")
            // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end
            {
                this.MitumoriKsjk.Jokyo_flg = this.myForm.txtNum_Jyoukyou.Text;
            }
            if (!string.IsNullOrEmpty(this.myForm.numTxtbox_TrhkskCD.Text))
            {
                this.MitumoriKsjk.Torihikisaki_cd = this.myForm.numTxtbox_TrhkskCD.Text;
            }
            if (!string.IsNullOrEmpty(this.myForm.numTxtBox_GyousyaCD.Text))
            {
                this.MitumoriKsjk.Gyousha_cd = this.myForm.numTxtBox_GyousyaCD.Text;
            }
            if (!string.IsNullOrEmpty(this.myForm.numTxtBox_GbCD.Text))
            {
                this.MitumoriKsjk.Genba_cd = this.myForm.numTxtBox_GbCD.Text;
            }
            if (this.myForm.chkBox_Trhksk.Text == "0")
            {
                this.MitumoriKsjk.Hikiai_torihikisaki_flg = "0";
            }
            else
            {
                this.MitumoriKsjk.Hikiai_torihikisaki_flg = "1";
            }
            if (this.myForm.chkBox_Gyosya.Text == "0")
            {
                this.MitumoriKsjk.Hikiai_gyousha_flg = "0";
            }
            else
            {
                this.MitumoriKsjk.Hikiai_gyousha_flg = "1";
            }
            if (this.myForm.chkBox_Gb.Text == "0")
            {
                this.MitumoriKsjk.Hikiai_genba_flg = "0";
            }
            else
            {
                this.MitumoriKsjk.Hikiai_genba_flg = "1";
            }
        }

        #endregion

        #region チェック処理

        /// <summary>
        /// 検索条件チェック
        /// </summary>
        internal bool KensakuJyoukenCheck(out bool catchErr)
        {
            catchErr = false;
            bool isErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 日付FROM > 日付TO 場合
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text.Trim()) &&
                    !string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text.Trim()))
                {
                    if (this.headForm.HIDUKE_TO.Text.Trim().CompareTo(this.headForm.HIDUKE_FROM.Text.Trim()) < 0)
                    {
                        // koukouei 20141020 バグ 「From　>　To」のアラート表示タイミング変更 start
                        this.headForm.HIDUKE_FROM.IsInputErrorOccured = true;
                        this.headForm.HIDUKE_TO.IsInputErrorOccured = true;
                        this.headForm.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                        this.headForm.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                        string errorMsg = "日付範囲の設定を見直してください。";
                        MessageBoxUtility.MessageBoxShowError(errorMsg);
                        this.headForm.HIDUKE_FROM.Focus();
                        LogUtility.DebugMethodEnd(isErr, catchErr);
                        return isErr;
                        // koukouei 20141020 バグ 「From　>　To」のアラート表示タイミング変更 end
                    }
                }

                // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
                //// 状況「1」、「2」、「3」、「0」以外場合
                //if (!"1".Equals(this.myForm.txtNum_Jyoukyou.Text) && !"2".Equals(this.myForm.txtNum_Jyoukyou.Text) &&
                //    !"3".Equals(this.myForm.txtNum_Jyoukyou.Text) && !"0".Equals(this.myForm.txtNum_Jyoukyou.Text))
                //{
                //    MessageBoxUtility.MessageBoxShow("E042", "0～3");
                // 状況「1」、「2」、「0」以外場合
                //20151007 hoanghm #2315 start
                //if (!"1".Equals(this.myForm.txtNum_Jyoukyou.Text) && !"2".Equals(this.myForm.txtNum_Jyoukyou.Text) &&
                //    !"0".Equals(this.myForm.txtNum_Jyoukyou.Text))
                //{
                //    MessageBoxUtility.MessageBoxShow("E042", "0～2");
                //    LogUtility.DebugMethodEnd(true);
                //    return true;
                //}

                var allControlAndHeaderControls = this.myForm.allControl.ToList();
                allControlAndHeaderControls.AddRange(this.myForm.controlUtil.GetAllControls(this.headForm));
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.myForm.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                if (this.myForm.RegistErrorFlag)
                {
                    LogUtility.DebugMethodEnd(isErr, catchErr);
                    return isErr;
                }
                //20151007 hoanghm #2315 end

                // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end

                // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　start
                isErr = this.HissuCheck();
                // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　end
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("KensakuJyoukenCheck", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr, catchErr);
            }
            return isErr;
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal void CheckGenba()
        {
            //LogUtility.DebugMethodStart();
            //// 初期化
            //this.myForm.txtBox_GbName.Text = string.Empty;
            //this.myForm.txtBox_GbName.ReadOnly = true;

            //if (string.IsNullOrEmpty(this.myForm.numTxtBox_GbCD.Text))
            //{
            //    return;
            //}

            //var genbaEntityList = this.accessor.GetGenba(this.myForm.numTxtBox_GbCD.Text);
            //if (genbaEntityList == null || genbaEntityList.Length < 1)
            //{
            //    return;
            //}

            //bool isContinue = false;
            //M_GENBA genba = new M_GENBA();
            //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //if (string.IsNullOrEmpty(this.control.TORIHIKISAKI_NAME_RYAKU.Text))
            //{
            //    if (string.IsNullOrEmpty(this.control.GYOUSYA_NAME_RYAKU.Text))
            //    {
            //        // エラーメッセージ
            //        msgLogic.MessageBoxShow("E051", "業者");
            //        this.myForm.numTxtBox_GbCD.Text = string.Empty;
            //        return;
            //    }

            //    foreach (M_GENBA genbaEntity in genbaEntityList)
            //    {
            //        if (this.control.GYOUSYA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
            //        {
            //            isContinue = true;
            //            genba = genbaEntity;
            //            this.control.GENNBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
            //            break;
            //        }
            //    }

            //    if (!isContinue)
            //    {
            //        // 一致するものがないのでエラー
            //        msgLogic.MessageBoxShow("E062", "業者");
            //        this.myForm.numTxtBox_GbCD.Focus();
            //        return;
            //    }
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(this.control.GYOUSYA_NAME_RYAKU.Text))
            //    {
            //        // エラーメッセージ
            //        msgLogic.MessageBoxShow("E051", "業者");
            //        this.myForm.numTxtBox_GbCD.Text = string.Empty;
            //        return;
            //    }

            //    foreach (M_GENBA genbaEntity in genbaEntityList)
            //    {
            //        if (this.control.TORIHIKISAKI_CD.Text.Equals(genbaEntity.TORIHIKISAKI_CD) &&
            //            this.control.GYOUSYA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
            //        {
            //            isContinue = true;
            //            genba = genbaEntity;
            //            this.control.GENNBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
            //            break;
            //        }
            //    }

            //    if (!isContinue)
            //    {
            //        // 一致するものがないのでエラー
            //        msgLogic.MessageBoxShow("E062", "取引先、業者");
            //        this.myForm.numTxtBox_GbCD.Focus();
            //        return;
            //    }
            //}
            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal void CheckGyousha()
        {
            //LogUtility.DebugMethodStart();
            //// 初期化
            //this.control.GYOUSYA_NAME_RYAKU.Text = string.Empty;
            //this.control.GYOUSYA_NAME_RYAKU.ReadOnly = true;

            //if (string.IsNullOrEmpty(this.control.GYOUSYA_CD.Text))
            //{
            //    return;
            //}

            //var gyoushaEntity = this.accessor.GetGyousha((this.control.GYOUSYA_CD.Text));
            //if (gyoushaEntity == null)
            //{
            //    return;
            //}
            //else if (gyoushaEntity.GYOUSHAKBN_UKEIRE == false)
            //{
            //    // エラーメッセージ
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    msgLogic.MessageBoxShow("E058", "");
            //    this.control.GYOUSYA_CD.Focus();
            //    return;
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(this.control.TORIHIKISAKI_NAME_RYAKU.Text))
            //    {
            //        var torihikisaki = this.accessor.GetTorihikisaki(gyoushaEntity.TORIHIKISAKI_CD);
            //        this.control.GYOUSYA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
            //        this.control.TORIHIKISAKI_CD.Text = gyoushaEntity.TORIHIKISAKI_CD;
            //        this.control.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
            //        //// 締日更新
            //        //this.CheckSeikyuuShimebi();
            //        //this.CheckShiharaiShimebi();
            //    }
            //    else
            //    {
            //        if (this.control.TORIHIKISAKI_CD.Text.Equals(gyoushaEntity.TORIHIKISAKI_CD))
            //        {
            //            this.control.GYOUSYA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
            //        }
            //        else
            //        {
            //            // 一致するものがないのでエラー
            //            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //            msgLogic.MessageBoxShow("E062", "取引先");
            //            this.control.GYOUSYA_CD.Focus();
            //        }
            //    }
            //}
            //LogUtility.DebugMethodEnd();
        }

        #endregion

        #region インターフェース処理

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

        #region ヘッダ設定

        /// <summary>
        /// ヘッダ設定
        /// </summary>
        /// /// <returns></returns>
        public void SetHeader(HeaderForm hs)
        {
            try
            {
                this.headForm = hs;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("SetHeader", ex);
                throw ex;
            }
        }

        #endregion

        #region 共通関数

        ///// <summary>
        ///// カンマ編集(ヘッダ部アラート件数用)
        ///// </summary>
        ///// <returns></returns>
        //private string SetComma(string value)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(value);

        //        if (string.IsNullOrEmpty(value) == true)
        //        {
        //            LogUtility.DebugMethodEnd("0");
        //            return "0";
        //        }
        //        else
        //        {
        //            LogUtility.DebugMethodEnd(string.Format("{0:#,0}", Convert.ToDecimal(value)));
        //            return string.Format("{0:#,0}", Convert.ToDecimal(value));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Fatal("SetComma", ex);
        //        throw ex;
        //    }
        //}

        #region 受入試験指摘修正(数値表示項目のソート)

        /// <summary>
        /// カンマ編集とNULLチェック(Grid明細部用)
        /// </summary>
        /// <returns></returns>
        private decimal SetComma(object value)
        {
            try
            {
                LogUtility.DebugMethodStart(value);

                if (this.IsDBNull(value))
                {
                    LogUtility.DebugMethodEnd("0");
                    return 0;
                }
                else
                {
                    LogUtility.DebugMethodEnd(string.Format("{0:#,0}", Convert.ToDecimal(value)));
                    //string aaa = ((decimal)value).ToString("#,0");
                    return (decimal)value;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("SetComma", ex);
                throw ex;
            }
        }

        /// <summary>データがDbNullかどうか取得する</summary>
        /// <param name="data">確認データオブジェクト</param>
        /// <returns>DBNullの場合は真</returns>
        private bool IsDBNull(object data)
        {
            return DBNull.Value.Equals(data);
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

        /// <summary>
        /// 営業担当者チェック
        /// </summary>
        internal bool CheckEigyoTantousha()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // 初期化
                this.myForm.txtBox_Eigyosyamei.Text = string.Empty;

                if (string.IsNullOrEmpty(this.myForm.txtBox_Eigyotantosya.Text))
                {
                    // 入力担当者CDがなければ既にエラーが表示されているはずなので何もしない
                    return ret;
                }

                var shainEntity = this.accessor.GetShain(this.myForm.txtBox_Eigyotantosya.Text);
                if (shainEntity == null)
                {
                    return ret;
                }

                if (shainEntity.EIGYOU_TANTOU_KBN.IsFalse)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.myForm.txtBox_Eigyotantosya.Focus();
                }
                else
                {
                    this.myForm.txtBox_Eigyosyamei.Text = shainEntity.SHAIN_NAME_RYAKU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEigyoTantousha", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        // 201400708 syunrei ＃947　№15　　start
        /// <summary>
        /// ・見積入力画面内、状況項目内、進行中項目は削除する。
        /// </summary>
        private void DeleteJoukyo()
        {
            //進行中表示しない
            this.myForm.radbtn_Sinkoutyuu.Visible = false;
            //２、３の項目上に移動
            this.myForm.radbtn_Jyutyuu.Location = new System.Drawing.Point(67, 0);
            this.myForm.radbtn_Situtyuu.Location = new System.Drawing.Point(148, 0);
            //テキスト入力範囲設定
            //this.myForm.txtNum_Jyoukyou.CharacterLimitList = new char[] {
            //// 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
            //        //'0',
            //        //'2',
            //        //'3',
            //        //'\0'};
            //        '0',
            //        '1',
            //        '2',
            //        '\0'};
            //// 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end

            this.myForm.txtNum_Jyoukyou.RangeSetting.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});

            this.myForm.txtNum_Jyoukyou.RangeSetting.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
        }

        // 201400708 syunrei ＃947　№15　　end
        // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　start
        public bool HissuCheck()
        {
            bool res = false;
            //拠点必須項目とする
            if (string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
            {
                MessageBoxUtility.MessageBoxShow("E001", "拠点");
                this.headForm.KYOTEN_CD.Focus();
                this.headForm.KYOTEN_CD.BackColor = System.Drawing.Color.Red;

                return res = true;
            }
            //伝票日付必須項目とする
            if ((this.headForm.radbtnDenpyouHiduke.Checked || this.headForm.radbtnNyuuryokuHiduke.Checked)
                && (string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text) || string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text)))
            {
                if (string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
                {
                    MessageBoxUtility.MessageBoxShow("E001", "開始日");
                    this.headForm.HIDUKE_FROM.Focus();
                    this.headForm.HIDUKE_FROM.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    MessageBoxUtility.MessageBoxShow("E001", "終了日");
                    this.headForm.HIDUKE_TO.Focus();
                    this.headForm.HIDUKE_TO.BackColor = System.Drawing.Color.Red;
                }

                LogUtility.DebugMethodEnd(true);
                return res = true;
            }
            return res;
        }

        #region ユーザー定義情報取得処理

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

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            LogUtility.DebugMethodStart();

            // 初期化
            this.headForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

            if (string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
            {
                this.headForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                return;
            }

            short kyoteCd = -1;
            if (!short.TryParse(string.Format("{0:#,0}", this.headForm.KYOTEN_CD.Text), out kyoteCd))
            {
                return;
            }

            var kyotens = this.accessor.GetDataByCodeForKyoten(kyoteCd);

            // 存在チェック
            if (kyotens == null || kyotens.Length < 1)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "拠点");
                this.headForm.KYOTEN_CD.Focus();
                return;
            }
            else
            {
                // キーが１つなので複数はヒットしないはず
                M_KYOTEN kyoten = kyotens[0];
                this.headForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // 20140716 syunrei EV005277_拠点CD、伝票日付、状況を必須項目とする　end

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141201 teikyou ダブルクリックを追加する　start
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hidukeFromTextBox = this.headForm.HIDUKE_FROM;
            var hidukeToTextBox = this.headForm.HIDUKE_TO;
            hidukeToTextBox.Text = hidukeFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        // 20141201 teikyou ダブルクリックを追加する　end

        #endregion
    }
}