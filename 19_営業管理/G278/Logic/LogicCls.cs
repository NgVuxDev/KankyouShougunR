// $Id: LogicCls.cs 38461 2014-12-29 07:59:08Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class JuchuuYojitsuKanrihyouLogic : IBuisinessLogic
    {
        /// <summary>
        /// DTO
        /// </summary>
        private JuchuuYojitsuDto dto;

        /// <summary>
        /// DAO
        /// </summary>
        private JuchuuYojitsuDao dao;

        /// <summary>
        /// Form
        /// </summary>
        private JuchuuYojitsuKanrihyouForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 部署マスタデータ取得テーブル
        /// </summary>
        private DataTable bushoResult { get; set; }

        /// <summary>
        /// 自社情報マスタデータ取得テーブル
        /// </summary>
        private DataTable corpResult { get; set; }

        /// <summary>
        /// 自社情報から取得した期首月
        /// </summary>
        private int kishuMonth { get; set; }

        /// <summary>
        /// 期首月から作成した月リスト
        /// </summary>
        private string[] kishuMonthArray = new string[12];

        /// <summary>
        /// 期首月から作成した開始月
        /// </summary>
        private string startMonth { get; set; }

        /// <summary>
        /// 期首月から作成した終了月
        /// </summary>
        private string endMonth { get; set; }

        /// <summary>
        /// 期首月から算出した年度開始年月日
        /// </summary>
        private DateTime nendoStartYMD { get; set; }

        /// <summary>
        /// 期首月から算出した年度終了年月日
        /// </summary>
        private DateTime nendoEndYMD { get; set; }

        /// <summary>
        /// DataTable(仮)
        /// </summary>
        private DataTable table;

        /// <summary>
        /// DataTable
        /// </summary>
        private DataTable ResultTable { get; set; }

        private String bushoCd;
        private String eigyouCd;

        /// <summary>
        /// Dao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou.Setting.ButtonSetting.xml";

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        private HeaderForm headerForm;

        /// <summary>
        /// アラート件数超過時
        /// </summary>
        private DialogResult showBool;

        /// <summary>
        /// 指定年度から作成したデータ取得テーブル
        /// </summary>
        private DataTable nendoTable = new DataTable();

        /// <summary>
        /// 指定年度の次の年度から作成したデータ取得テーブル
        /// </summary>
        private DataTable jinendoTable = new DataTable();

        /// <summary>
        /// 次年度分になる月のリストを格納します
        /// 例：期首月５月の場合１月～４月
        /// </summary>
        private List<string> jinendoMonthArray = new List<string>();

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public JuchuuYojitsuKanrihyouLogic(JuchuuYojitsuKanrihyouForm targetForm)
        {
            LogUtility.DebugMethodStart();

            this.form = targetForm;
            this.dto = new JuchuuYojitsuDto();
            this.dao = DaoInitUtility.GetComponent<JuchuuYojitsuDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                //プロセスボタンを非表示設定
                parentForm.ProcessButtonPanel.Visible = false;
                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                //期首月の取得・設定
                GetKishutuki();
                //DGVヘッダ用名称作成
                CreateNendoMonth();
                //年度選択カレンダの初期設定
                SetCalendar();
                //部署CDにFocus当てる
                this.form.tb_busho_cd.Focus();

                //初期化を行い表示用空白1レコードの追加
                MultiRowInit(2);
                this.form.grdIchiran.Rows.Add();

                // アラート件数
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
                if (sysInfo != null)
                {
                    this.headerForm.alertNumber.Text = ((int)sysInfo.ICHIRAN_ALERT_KENSUU).ToString("#,0");
                }
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
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion

        #region 初期表示イベント

        /// <summary>
        /// 表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // フォーカス設定
            this.form.header.Select();
            this.form.header.Focus();
            this.form.header.tb_nendo.Select();
            this.form.header.tb_nendo.Focus();
        }

        #endregion

        #region MultiRowの初期化処理

        /// <summary>
        /// MultiRowの初期化を行う
        /// </summary>
        /// <param name="keisiki">1=年度、2=月次</param>
        internal void MultiRowInit(int keisiki)
        {
            LogUtility.DebugMethodStart(keisiki);
            try
            {
                this.form.grdIchiran.Rows.Clear();
                if (keisiki == 1)
                {
                    //年次用のテンプレートを設定
                    this.form.grdIchiran.Template = this.form.detailItilanNendo;

                    //検索条件（年度）に対応したヘッダーを設定
                    this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_01"].Value = this.dto.NENDO_01 + "年度";
                    this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_02"].Value = this.dto.NENDO_02 + "年度";
                    this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_03"].Value = this.dto.NENDO_03 + "年度";
                    this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_04"].Value = this.dto.NENDO_04 + "年度";
                    this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_05"].Value = this.dto.NENDO_05 + "年度";
                    this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_06"].Value = this.dto.NENDO_06 + "年度";
                    this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_07"].Value = this.dto.NENDO_07 + "年度";
                    this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_08"].Value = this.dto.NENDO_08 + "年度";
                    this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_09"].Value = this.dto.NENDO_09 + "年度";
                }
                else
                {
                    //月次用のテンプレートを設定
                    this.form.grdIchiran.Template = this.form.detailItilanGetsuji;

                    //ヘッダーの値を設定(今年度の開始月から終了月)
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_01"].Value = this.kishuMonthArray[0];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_02"].Value = this.kishuMonthArray[1];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_03"].Value = this.kishuMonthArray[2];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_04"].Value = this.kishuMonthArray[3];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_05"].Value = this.kishuMonthArray[4];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_06"].Value = this.kishuMonthArray[5];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_07"].Value = this.kishuMonthArray[6];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_08"].Value = this.kishuMonthArray[7];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_09"].Value = this.kishuMonthArray[8];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_10"].Value = this.kishuMonthArray[9];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_11"].Value = this.kishuMonthArray[10];
                    this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_12"].Value = this.kishuMonthArray[11];
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // ボタンの設定情報をファイルから読み込む
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();

                LogUtility.DebugMethodEnd();

                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //Functionボタンのイベント生成
                parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);            //[F6]CSV出力イベント
                parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);            //[F7]条件ｸﾘｱイベント
                parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);            //[F8]検索イベント
                parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);          //[F12]閉じるイベント
                //年度変更イベント
                //this.headerForm.dt_nendo.TextChanged += new EventHandler(dt_nendo_TextChanged); //カレンダー変更イベント
                // 画面表示イベント
                parentForm.Shown += new EventHandler(UIForm_Shown);
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 自社情報の期首月を取得・設定

        /// <summary>
        /// 自社情報の期首月を取得・設定する
        /// </summary>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        private void GetKishutuki()
        {
            LogUtility.DebugMethodStart();
            try
            {
                M_CORP_INFO corpEntity = new M_CORP_INFO();
                corpEntity.SYS_ID = 0;
                this.corpResult = dao.GetCorpDataForEntity(corpEntity);
                this.kishuMonth = int.Parse(this.corpResult.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F6]CSV出力ボタンイベント

        /// <summary>
        /// [F6]CSV出力ボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                int tableCnt;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (this.form.grdIchiran.Template == this.form.detailItilanGetsuji)
                {
                    tableCnt = this.form.grdGetsuji.Rows.Count;
                }
                else
                {
                    tableCnt = this.form.grdNendo.Rows.Count;
                }
                if (tableCnt == 0)
                {
                    msgLogic.MessageBoxShow("E044");
                    return;
                }

                CSVExport exp = new CSVExport();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)			// CSV出力しますか？
                {
                    if (this.form.grdIchiran.Template == this.form.detailItilanGetsuji)
                    {
                        exp.ConvertCustomDataGridViewToCsv(this.form.grdGetsuji, true, true, this.headerForm.lb_title.Text, this.form);
                    }
                    else
                    {
                        exp.ConvertCustomDataGridViewToCsv(this.form.grdNendo, true, true, this.headerForm.lb_title.Text, this.form);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region [F7]条件クリアボタンイベント

        /// <summary>
        /// [F7]条件クリアボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //検索条件をクリア
                //string getDate = DateTime.Today.ToString("yyyy/MM/dd");
                //SetNendo(getDate);
                this.headerForm.tb_nendo.Text = this.getNendo(this.parentForm.sysDate).ToString();

                this.form.tb_busho_cd.Text = string.Empty;
                this.form.tb_busho_name.Text = string.Empty;
                this.headerForm.ReadDataNumber.Text = "0";
                //年度選択カレンダの初期設定
                SetCalendar();
                //部署CDにFocus当てる
                this.form.tb_busho_cd.Focus();

                //月次に修正し、MultiRowを初期化
                this.headerForm.rdbGetuji.Checked = true;
                MultiRowInit(2);
                this.form.grdIchiran.Rows.Add();
                this.form.grdGetsuji.Rows.Clear();
                this.form.grdNendo.Rows.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region [F8]検索ボタンイベント

        /// <summary>
        /// [F8]検索ボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                #region 入力値チェック

                //年度の必須チェック

                if (string.IsNullOrEmpty(this.headerForm.tb_nendo.Text))
                {
                    //未入力チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "年度");
                    this.headerForm.tb_nendo.Focus();
                    return;
                }
                else if (1753 > int.Parse(this.headerForm.tb_nendo.Text))
                {
                    //DateTime下限値チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E032", "1753", "年度");
                    this.headerForm.tb_nendo.Focus();
                    return;
                }
                //年度の必須チェック
                if (string.IsNullOrEmpty(this.headerForm.txtKeisiki.Text))
                {
                    //未入力チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "出力形式");
                    this.headerForm.txtKeisiki.Focus();
                    return;
                }
                // No2661-->
                //部署の必須チェック
                if (string.IsNullOrEmpty(this.form.tb_busho_cd.Text))
                {
                    //未入力チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "部署");
                    this.form.tb_busho_cd.Focus();
                    return;
                }
                // No2661<--

                #endregion

                //検索条件取得
                this.dto = new JuchuuYojitsuDto();

                if (!string.IsNullOrEmpty(this.form.tb_busho_cd.Text))
                {
                    // No2661-->
                    if (!this.form.tb_busho_cd.Text.Equals("999"))
                    {
                        this.dto.BUSHO_CD = this.form.tb_busho_cd.Text;
                    }
                    // No2661<--
                }

                this.dto.NENDO_01 = this.headerForm.tb_nendo.Text;
                this.dto.JINEN_01 = (Convert.ToInt16(this.headerForm.tb_nendo.Text) + 1).ToString();
                //選択された年度の開始～終了年月日を算出
                //共通用DTO設定
                String nendoStrat = this.headerForm.tb_nendo.Text + "/" + startMonth + "/" + "01";
                String nendoEnd = DateTime.Parse(nendoStrat).AddYears(1).AddMonths(-1).ToString("yyyyMMdd");
                this.dto.STARTNENDO = this.headerForm.tb_nendo.Text + startMonth;
                this.dto.ENDNENDO = nendoEnd.Substring(0, 6);

                if (this.headerForm.txtKeisiki.Text.Equals("1"))
                {
                    //年次用DTO設定

                    #region DBから明細部データ取得

                    this.dto.STARTMONTH = startMonth;
                    this.dto.ENDMONTH = endMonth;
                    String nendo = nendoStrat;

                    //設定年度
                    nendo = DateTime.Parse(nendo).AddYears(1).AddMonths(-1).ToString("yyyy/MM/dd");
                    this.dto.ENDNENDO_01 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    nendo = DateTime.Parse(nendo).AddMonths(-11).ToString("yyyy/MM/dd");
                    this.dto.STARTNENDO_01 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    //設定年度-1
                    nendo = DateTime.Parse(nendo).AddMonths(-1).ToString("yyyy/MM/dd");
                    this.dto.ENDNENDO_02 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    nendo = DateTime.Parse(nendo).AddMonths(-11).ToString("yyyy/MM/dd");
                    this.dto.STARTNENDO_02 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    //設定年度-2
                    nendo = DateTime.Parse(nendo).AddMonths(-1).ToString("yyyy/MM/dd");
                    this.dto.ENDNENDO_03 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    nendo = DateTime.Parse(nendo).AddMonths(-11).ToString("yyyy/MM/dd");
                    this.dto.STARTNENDO_03 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    //設定年度-3
                    nendo = DateTime.Parse(nendo).AddMonths(-1).ToString("yyyy/MM/dd");
                    this.dto.ENDNENDO_04 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    nendo = DateTime.Parse(nendo).AddMonths(-11).ToString("yyyy/MM/dd");
                    this.dto.STARTNENDO_04 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    //設定年度-4
                    nendo = DateTime.Parse(nendo).AddMonths(-1).ToString("yyyy/MM/dd");
                    this.dto.ENDNENDO_05 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    nendo = DateTime.Parse(nendo).AddMonths(-11).ToString("yyyy/MM/dd");
                    this.dto.STARTNENDO_05 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    //設定年度-5
                    nendo = DateTime.Parse(nendo).AddMonths(-1).ToString("yyyy/MM/dd");
                    this.dto.ENDNENDO_06 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    nendo = DateTime.Parse(nendo).AddMonths(-11).ToString("yyyy/MM/dd");
                    this.dto.STARTNENDO_06 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    //設定年度-6
                    nendo = DateTime.Parse(nendo).AddMonths(-1).ToString("yyyy/MM/dd");
                    this.dto.ENDNENDO_07 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    nendo = DateTime.Parse(nendo).AddMonths(-11).ToString("yyyy/MM/dd");
                    this.dto.STARTNENDO_07 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    //設定年度-7
                    nendo = DateTime.Parse(nendo).AddMonths(-1).ToString("yyyy/MM/dd");
                    this.dto.ENDNENDO_08 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    nendo = DateTime.Parse(nendo).AddMonths(-11).ToString("yyyy/MM/dd");
                    this.dto.STARTNENDO_08 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    //設定年度-8
                    nendo = DateTime.Parse(nendo).AddMonths(-1).ToString("yyyy/MM/dd");
                    this.dto.ENDNENDO_09 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);
                    nendo = DateTime.Parse(nendo).AddMonths(-11).ToString("yyyy/MM/dd");
                    this.dto.STARTNENDO_09 = DateTime.Parse(nendo).ToString("yyyyMMdd").Substring(0, 6);

                    //設定年度-1から設定年度-8
                    this.dto.NENDO_02 = (int.Parse(this.dto.NENDO_01) - 1).ToString();
                    this.dto.NENDO_03 = (int.Parse(this.dto.NENDO_01) - 2).ToString();
                    this.dto.NENDO_04 = (int.Parse(this.dto.NENDO_01) - 3).ToString();
                    this.dto.NENDO_05 = (int.Parse(this.dto.NENDO_01) - 4).ToString();
                    this.dto.NENDO_06 = (int.Parse(this.dto.NENDO_01) - 5).ToString();
                    this.dto.NENDO_07 = (int.Parse(this.dto.NENDO_01) - 6).ToString();
                    this.dto.NENDO_08 = (int.Parse(this.dto.NENDO_01) - 7).ToString();
                    this.dto.NENDO_09 = (int.Parse(this.dto.NENDO_01) - 8).ToString();

                    this.dto.JINEN_02 = (int.Parse(this.dto.JINEN_01) - 1).ToString();
                    this.dto.JINEN_03 = (int.Parse(this.dto.JINEN_01) - 2).ToString();
                    this.dto.JINEN_04 = (int.Parse(this.dto.JINEN_01) - 3).ToString();
                    this.dto.JINEN_05 = (int.Parse(this.dto.JINEN_01) - 4).ToString();
                    this.dto.JINEN_06 = (int.Parse(this.dto.JINEN_01) - 5).ToString();
                    this.dto.JINEN_07 = (int.Parse(this.dto.JINEN_01) - 6).ToString();
                    this.dto.JINEN_08 = (int.Parse(this.dto.JINEN_01) - 7).ToString();
                    this.dto.JINEN_09 = (int.Parse(this.dto.JINEN_01) - 8).ToString();

                    // 自社情報の期首月を基準に次年になる月を設定
                    bool[] jinenFlgArray = new bool[12];
                    // 初期化
                    for (int i = 0; i < jinenFlgArray.Length; i++)
                    {
                        jinenFlgArray[i] = false;
                    }

                    // 自社情報の期首月が一月以外の場合
                    if (this.kishuMonth != 1)
                    {
                        for (int i = 0; i <= (this.kishuMonth - 2); i++)
                        {
                            jinenFlgArray[i] = true;
                        }
                    }
                    this.dto.JINEN_FLG_01 = jinenFlgArray[0];
                    this.dto.JINEN_FLG_02 = jinenFlgArray[1];
                    this.dto.JINEN_FLG_03 = jinenFlgArray[2];
                    this.dto.JINEN_FLG_04 = jinenFlgArray[3];
                    this.dto.JINEN_FLG_05 = jinenFlgArray[4];
                    this.dto.JINEN_FLG_06 = jinenFlgArray[5];
                    this.dto.JINEN_FLG_07 = jinenFlgArray[6];
                    this.dto.JINEN_FLG_08 = jinenFlgArray[7];
                    this.dto.JINEN_FLG_09 = jinenFlgArray[8];
                    this.dto.JINEN_FLG_10 = jinenFlgArray[9];
                    this.dto.JINEN_FLG_11 = jinenFlgArray[10];
                    this.dto.JINEN_FLG_12 = jinenFlgArray[11];

                    //検索処理
                    this.table = this.dao.GetDispDataNendoForEntity(this.dto);
                    if (0 == table.Rows.Count)
                    {
                        //健作結果が0件の場合、メッセージを出力
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                        MultiRowInit(1);
                        this.form.grdNendo.Rows.Clear();
                        this.form.grdIchiran.Rows.Add();
                        return;
                    }
                    showBool = DialogResult.Yes;

                    if (decimal.Parse(this.headerForm.alertNumber.Text) != 0 && decimal.Parse(this.headerForm.alertNumber.Text) < table.Rows.Count)
                    {
                        MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                        showBool = showLogic.MessageBoxShow("C025");
                    }
                    if (showBool == DialogResult.Yes)
                    {
                        SetDataNendo(this.table);
                        this.headerForm.ReadDataNumber.Text = this.table.Rows.Count.ToString("#,0");
                    }

                    #endregion
                }
                else
                {
                    //月次用DTO設定

                    #region DBから明細部データ取得

                    //選択された年度の開始から終了までの年月を算出
                    String month = nendoStrat;

                    this.dto.MONTH_01 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_02 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_03 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_04 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_05 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_06 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_07 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_08 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_09 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_10 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_11 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);
                    month = DateTime.Parse(month).AddMonths(1).ToString("yyyy/MM/dd");
                    this.dto.MONTH_12 = DateTime.Parse(month).ToString("yyyyMMdd").Substring(0, 6);

                    ////検索処理
                    // 表示用テーブル
                    this.table = this.dao.GetDispDataGetsujiForEntity(this.dto);
                    // 年度テーブル
                    this.nendoTable = this.dao.GetDispDataGetsujiForEntity(this.dto);

                    // 期首月が１月以外の場合
                    if (this.kishuMonth != 1)
                    {
                        // 次年度のデータを取得
                        this.dto.NENDO_01 = (Convert.ToInt16(this.dto.NENDO_01) + 1).ToString();
                        this.jinendoTable = this.dao.GetDispDataGetsujiForEntity(this.dto);
                        this.dto.NENDO_01 = (Convert.ToInt16(this.dto.NENDO_01) - 1).ToString();

                        // 上書き対象のカラムのReadOnlyを一時的に外す
                        foreach (var s in this.jinendoMonthArray)
                        {
                            this.table.Columns[s].ReadOnly = false;
                            this.table.Columns["YOTEI_GOUKEI"].ReadOnly = false;
                        }

                        // 次年度テーブルを表示用テーブルに上書き
                        for (int i = 0; i < jinendoTable.Rows.Count; i++)
                        {
                            foreach (var s in this.jinendoMonthArray)
                            {
                                this.table.Rows[i][s] = this.jinendoTable.Rows[i][s];
                            }

                            // 合計を再計算
                            long goukei = 0;
                            for (int j = 1; j <= 12; j++)
                            {
                                goukei = goukei + Convert.ToInt32(this.table.Rows[i]["YOTEI_MONTH" + j.ToString().PadLeft(2, '0')]);
                            }
                            this.table.Rows[i]["YOTEI_GOUKEI"] = goukei;
                        }

                        // ReadOnlyを戻す
                        foreach (var s in this.jinendoMonthArray)
                        {
                            this.table.Columns[s].ReadOnly = true;
                            this.table.Columns["YOTEI_GOUKEI"].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.table = this.nendoTable;
                    }

                    if (0 == this.table.Rows.Count)
                    {
                        //健作結果が0件の場合、メッセージを出力
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                        MultiRowInit(2);
                        this.form.grdGetsuji.Rows.Clear();
                        this.form.grdIchiran.Rows.Add();
                        return;
                    }
                    showBool = DialogResult.Yes;

                    if (decimal.Parse(this.headerForm.alertNumber.Text) != 0 && decimal.Parse(this.headerForm.alertNumber.Text) < this.table.Rows.Count)
                    {
                        MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                        showBool = showLogic.MessageBoxShow("C025");
                    }
                    if (showBool == DialogResult.Yes)
                    {
                        SetDataGetsuji(this.table);
                        this.headerForm.ReadDataNumber.Text = this.table.Rows.Count.ToString("#,0");
                    }

                    #endregion
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region [F12]閉じるボタンイベント

        /// <summary>
        /// [F12]閉じるボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region 年度選択カレンダの初期設定

        /// <summary>
        /// 年度選択カレンダの初期設定
        /// </summary>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        private void SetCalendar()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //今日日付を取得
                //string today = DateTime.Today.ToString("yyyy/MM/dd");
                //カレンダーに設定
                //this.headerForm.tb_nendo.DateTimeNowYear = DateTime.Today.ToString("yyyy"); ;
                this.headerForm.tb_nendo.Text = this.getNendo(this.parentForm.sysDate).ToString();
                //yyy/MM/dd形式の文字列を渡して年度(yyyy)取得・設定
                //SetNendo(today);
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 年度を算出

        ///// <summary>
        ///// 年度を算出して値を設定する
        ///// </summary>
        ///// <param name="date">yyyy/MM/dd形式の年月日を表す文字列</param>
        //void SetNendo(string date)
        //{
        //    LogUtility.DebugMethodStart(date);
        //    try
        //    {
        //        //パラメータ
        //        DateTime selectDate = DateTime.Parse(date);

        //        string year = DateTime.Parse(date).ToString("yyyy");
        //        string nendoBegin = year + "/" + this.startMonth + "/" + "01";
        //        //年度開始年月日
        //        this.nendoStartYMD = DateTime.Parse(DateTime.Parse(nendoBegin).ToString("yyyy/MM/dd"));
        //        //年度終了年月日
        //        this.nendoEndYMD = DateTime.Parse(DateTime.Parse(nendoBegin).AddYears(1).AddDays(-1).ToString("yyyy/MM/dd"));

        //        if (selectDate < this.nendoStartYMD)
        //        {
        //            //前年度
        //            this.headerForm.tb_nendo_BK.Text = selectDate.AddYears(-1).ToString("yyyy");
        //        }
        //        else if (selectDate > this.nendoEndYMD)
        //        {
        //            //次年度
        //            this.headerForm.tb_nendo_BK.Text = selectDate.AddYears(1).ToString("yyyy");
        //        }
        //        else
        //        {
        //            //今年度
        //            this.headerForm.tb_nendo_BK.Text = selectDate.ToString("yyyy");
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    LogUtility.DebugMethodEnd(date);
        //}

        /// <summary>
        /// 指定日付より年度を算出
        /// </summary>
        /// <param name="p_day">指定日付</param>
        /// <returns>年度</returns>
        private int getNendo(DateTime p_day)
        {
            LogUtility.DebugMethodStart(p_day);

            DateTime nendoStart =
                new DateTime(p_day.Year, this.kishuMonth, 1, 0, 0, 0, 0);

            int ret = 0;

            if (p_day.CompareTo(nendoStart) < 0)
            {
                // 指定日は算出年度開始日以前の場合
                ret = p_day.Year - 1;
            }
            else
            {
                // その他場合
                ret = p_day.Year;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region カレンダー変更イベント

        ///// <summary>
        ///// カレンダー変更イベント
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //void dt_nendo_TextChanged(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(this.headerForm.dt_nendo.Text))
        //        {
        //            string getDate = DateTime.Parse(this.headerForm.dt_nendo.Text).ToString("yyyy/MM/dd");
        //            SetNendo(getDate);
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    LogUtility.DebugMethodEnd(sender, e);
        //}

        #endregion

        #region 取得した期首月から12か月分のヘッダ用名称作成

        /// <summary>
        /// 取得した期首月から12か月分のヘッダ用名称を作成する
        /// </summary>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        private void CreateNendoMonth()
        {
            LogUtility.DebugMethodStart();
            try
            {
                for (int i = 0; i < this.kishuMonthArray.Length; i++)
                {
                    int sum = this.kishuMonth + i;
                    if (12 >= sum)
                    {
                        this.kishuMonthArray[i] = sum.ToString() + "月";

                        if (0 == i)
                        {
                            this.startMonth = sum.ToString().PadLeft(2, '0');
                        }
                        else if (i == this.kishuMonthArray.Length - 1)
                        {
                            this.endMonth = sum.ToString().PadLeft(2, '0');
                        }
                    }
                    else
                    {
                        this.kishuMonthArray[i] = (sum - 12).ToString() + "月";

                        if (0 == i)
                        {
                            this.startMonth = (sum - 12).ToString().PadLeft(2, '0');
                        }
                        else if (i == this.kishuMonthArray.Length - 1)
                        {
                            this.endMonth = (sum - 12).ToString().PadLeft(2, '0');
                        }
                    }
                }

                // 次年度分の月のリストを取得
                for (int i = 1; i <= (this.kishuMonth - 1); i++)
                {
                    this.jinendoMonthArray.Add("YOTEI_MONTH" + i.ToString().PadLeft(2, '0'));
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region データセット（年度）

        /// <summary>
        /// データセット（年度）
        /// </summary>
        /// <param name="DataTable">取得データ格納テーブル</param>
        /// <param name="table">table</param>
        private void SetDataNendo(DataTable table)
        {
            LogUtility.DebugMethodStart(table);
            try
            {
                #region データセット初期設定

                MultiRowInit(1);
                this.form.grdNendo.Rows.Clear();

                //検索条件（年度）に対応したヘッダーを設定
                this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_01"].Value = this.dto.NENDO_01 + "年度";
                this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_02"].Value = this.dto.NENDO_02 + "年度";
                this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_03"].Value = this.dto.NENDO_03 + "年度";
                this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_04"].Value = this.dto.NENDO_04 + "年度";
                this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_05"].Value = this.dto.NENDO_05 + "年度";
                this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_06"].Value = this.dto.NENDO_06 + "年度";
                this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_07"].Value = this.dto.NENDO_07 + "年度";
                this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_08"].Value = this.dto.NENDO_08 + "年度";
                this.form.grdIchiran.ColumnHeaders[0].Cells["NENDO_09"].Value = this.dto.NENDO_09 + "年度";

                this.form.grdNendo.Columns[5].HeaderText = this.dto.NENDO_09 + "年度";
                this.form.grdNendo.Columns[6].HeaderText = this.dto.NENDO_08 + "年度";
                this.form.grdNendo.Columns[7].HeaderText = this.dto.NENDO_07 + "年度";
                this.form.grdNendo.Columns[8].HeaderText = this.dto.NENDO_06 + "年度";
                this.form.grdNendo.Columns[9].HeaderText = this.dto.NENDO_05 + "年度";
                this.form.grdNendo.Columns[10].HeaderText = this.dto.NENDO_04 + "年度";
                this.form.grdNendo.Columns[11].HeaderText = this.dto.NENDO_03 + "年度";
                this.form.grdNendo.Columns[12].HeaderText = this.dto.NENDO_02 + "年度";
                this.form.grdNendo.Columns[13].HeaderText = this.dto.NENDO_01 + "年度";

                // テーブル作成(受注目標件数)
                this.ResultTable = new DataTable();
                ResultTable.Columns.Add("EIGYOU_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("EIGYOU_NAME", Type.GetType("System.String"));
                ResultTable.Columns.Add("BUSHO_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("BUSHO_NAME", Type.GetType("System.String"));
                ResultTable.Columns.Add("YOTEI_NENDO01", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_NENDO02", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_NENDO03", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_NENDO04", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_NENDO05", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_NENDO06", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_NENDO07", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_NENDO08", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_NENDO09", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_GOUKEI", Type.GetType("System.Int64"));
                ResultTable.Columns.Add("MITSUMORI_NENDO01", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_NENDO02", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_NENDO03", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_NENDO04", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_NENDO05", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_NENDO06", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_NENDO07", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_NENDO08", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_NENDO09", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_GOUKEI", Type.GetType("System.Int64"));

                //テーブルの要素名作成（予定件数）
                string[] yoteiName = new string[10];
                yoteiName[0] = "YOTEI_NENDO01";
                yoteiName[1] = "YOTEI_NENDO02";
                yoteiName[2] = "YOTEI_NENDO03";
                yoteiName[3] = "YOTEI_NENDO04";
                yoteiName[4] = "YOTEI_NENDO05";
                yoteiName[5] = "YOTEI_NENDO06";
                yoteiName[6] = "YOTEI_NENDO07";
                yoteiName[7] = "YOTEI_NENDO08";
                yoteiName[8] = "YOTEI_NENDO09";
                yoteiName[9] = "YOTEI_GOUKEI";

                //テーブルの要素名作成（見積(受注)件数）
                string[] mitsumoriName = new string[10];
                mitsumoriName[0] = "MITSUMORI_NENDO01";
                mitsumoriName[1] = "MITSUMORI_NENDO02";
                mitsumoriName[2] = "MITSUMORI_NENDO03";
                mitsumoriName[3] = "MITSUMORI_NENDO04";
                mitsumoriName[4] = "MITSUMORI_NENDO05";
                mitsumoriName[5] = "MITSUMORI_NENDO06";
                mitsumoriName[6] = "MITSUMORI_NENDO07";
                mitsumoriName[7] = "MITSUMORI_NENDO08";
                mitsumoriName[8] = "MITSUMORI_NENDO09";
                mitsumoriName[9] = "MITSUMORI_GOUKEI";

                //テーブルの要素名作成（達成率）
                string[] tasseiName = new string[10];
                tasseiName[0] = "NENDO_TASSEI_RITSU_01";
                tasseiName[1] = "NENDO_TASSEI_RITSU_02";
                tasseiName[2] = "NENDO_TASSEI_RITSU_03";
                tasseiName[3] = "NENDO_TASSEI_RITSU_04";
                tasseiName[4] = "NENDO_TASSEI_RITSU_05";
                tasseiName[5] = "NENDO_TASSEI_RITSU_06";
                tasseiName[6] = "NENDO_TASSEI_RITSU_07";
                tasseiName[7] = "NENDO_TASSEI_RITSU_08";
                tasseiName[8] = "NENDO_TASSEI_RITSU_09";
                tasseiName[9] = "TASSEI_GOKEI";

                string[] csvNENDO = new string[10];
                csvNENDO[0] = "NENDO_01";
                csvNENDO[1] = "NENDO_02";
                csvNENDO[2] = "NENDO_03";
                csvNENDO[3] = "NENDO_04";
                csvNENDO[4] = "NENDO_05";
                csvNENDO[5] = "NENDO_06";
                csvNENDO[6] = "NENDO_07";
                csvNENDO[7] = "NENDO_08";
                csvNENDO[8] = "NENDO_09";
                csvNENDO[9] = "NENDO_GOUKEI";

                #endregion

                //MultiRow設定用のDataTableに設定

                #region DataTableに設定

                foreach (DataRow r in table.Rows)
                {
                    // 値をRowへ設定後、DataTableに追加
                    DataRow newRow = this.ResultTable.NewRow();
                    // 部署、営業担当
                    newRow["EIGYOU_CD"] = r["EIGYOU_CD"];
                    newRow["EIGYOU_NAME"] = r["EIGYOU_NAME"];
                    newRow["BUSHO_CD"] = r["BUSHO_CD"];
                    newRow["BUSHO_NAME"] = r["BUSHO_NAME"];

                    // 受注目標件数
                    newRow["YOTEI_NENDO01"] = r["YOTEI_NENDO01"];
                    newRow["YOTEI_NENDO02"] = r["YOTEI_NENDO02"];
                    newRow["YOTEI_NENDO03"] = r["YOTEI_NENDO03"];
                    newRow["YOTEI_NENDO04"] = r["YOTEI_NENDO04"];
                    newRow["YOTEI_NENDO05"] = r["YOTEI_NENDO05"];
                    newRow["YOTEI_NENDO06"] = r["YOTEI_NENDO06"];
                    newRow["YOTEI_NENDO07"] = r["YOTEI_NENDO07"];
                    newRow["YOTEI_NENDO08"] = r["YOTEI_NENDO08"];
                    newRow["YOTEI_NENDO09"] = r["YOTEI_NENDO09"];
                    newRow["YOTEI_GOUKEI"] = r["YOTEI_GOUKEI"];

                    // 受注目標件数
                    newRow["MITSUMORI_NENDO01"] = r["MITSUMORI_NENDO01"];
                    newRow["MITSUMORI_NENDO02"] = r["MITSUMORI_NENDO02"];
                    newRow["MITSUMORI_NENDO03"] = r["MITSUMORI_NENDO03"];
                    newRow["MITSUMORI_NENDO04"] = r["MITSUMORI_NENDO04"];
                    newRow["MITSUMORI_NENDO05"] = r["MITSUMORI_NENDO05"];
                    newRow["MITSUMORI_NENDO06"] = r["MITSUMORI_NENDO06"];
                    newRow["MITSUMORI_NENDO07"] = r["MITSUMORI_NENDO07"];
                    newRow["MITSUMORI_NENDO08"] = r["MITSUMORI_NENDO08"];
                    newRow["MITSUMORI_NENDO09"] = r["MITSUMORI_NENDO09"];
                    newRow["MITSUMORI_GOUKEI"] = r["MITSUMORI_GOUKEI"];

                    this.ResultTable.Rows.Add(newRow);
                }

                #endregion

                //達成率計算用変数
                decimal yoteiWork;
                decimal mitsumoriWork;
                int intCsvCount;

                #region MultiRow,DataGridView作成処理(年度)

                this.form.grdIchiran.IsBrowsePurpose = false;

                //検索結果設定
                for (int i = 0; i < this.ResultTable.Rows.Count; i++)
                {
                    this.form.grdIchiran.Rows.Add();

                    this.form.grdNendo.Rows.Add();
                    this.form.grdNendo.Rows.Add();
                    this.form.grdNendo.Rows.Add();

                    bushoCd = castPadding(table.Rows[i]["BUSHO_CD"].ToString(), 3);
                    eigyouCd = castPadding(table.Rows[i]["EIGYOU_CD"].ToString(), 6);

                    intCsvCount = i * 3;
                    // DGVへ設定(部署、営業担当)
                    this.form.grdIchiran.Rows[i].Cells["BUSHO_CD"].Value = bushoCd;
                    this.form.grdIchiran.Rows[i].Cells["BUSHO_NAME"].Value = table.Rows[i]["BUSHO_NAME"].ToString();
                    this.form.grdIchiran.Rows[i].Cells["EIGYOU_CD"].Value = eigyouCd;
                    this.form.grdIchiran.Rows[i].Cells["EIGYOU_NAME"].Value = table.Rows[i]["EIGYOU_NAME"].ToString();

                    this.form.grdNendo.Rows[intCsvCount].Cells["NENDO_BUSHO_CD"].Value = bushoCd;
                    this.form.grdNendo.Rows[intCsvCount].Cells["NENDO_BUSHO_NAME"].Value = table.Rows[i]["BUSHO_NAME"].ToString();
                    this.form.grdNendo.Rows[intCsvCount].Cells["NENDO_EIGYOU_CD"].Value = eigyouCd;
                    this.form.grdNendo.Rows[intCsvCount].Cells["NENDO_EIGYOU_NAME"].Value = table.Rows[i]["EIGYOU_NAME"].ToString();
                    this.form.grdNendo.Rows[intCsvCount].Cells["NENDO_JYOUKYOU"].Value = "予定件数";
                    this.form.grdNendo.Rows[intCsvCount + 1].Cells["NENDO_BUSHO_CD"].Value = bushoCd;
                    this.form.grdNendo.Rows[intCsvCount + 1].Cells["NENDO_BUSHO_NAME"].Value = table.Rows[i]["BUSHO_NAME"].ToString();
                    this.form.grdNendo.Rows[intCsvCount + 1].Cells["NENDO_EIGYOU_CD"].Value = eigyouCd;
                    this.form.grdNendo.Rows[intCsvCount + 1].Cells["NENDO_EIGYOU_NAME"].Value = table.Rows[i]["EIGYOU_NAME"].ToString();
                    this.form.grdNendo.Rows[intCsvCount + 1].Cells["NENDO_JYOUKYOU"].Value = "受注件数";
                    this.form.grdNendo.Rows[intCsvCount + 2].Cells["NENDO_BUSHO_CD"].Value = bushoCd;
                    this.form.grdNendo.Rows[intCsvCount + 2].Cells["NENDO_BUSHO_NAME"].Value = table.Rows[i]["BUSHO_NAME"].ToString();
                    this.form.grdNendo.Rows[intCsvCount + 2].Cells["NENDO_EIGYOU_CD"].Value = eigyouCd;
                    this.form.grdNendo.Rows[intCsvCount + 2].Cells["NENDO_EIGYOU_NAME"].Value = table.Rows[i]["EIGYOU_NAME"].ToString();
                    this.form.grdNendo.Rows[intCsvCount + 2].Cells["NENDO_JYOUKYOU"].Value = "達成率(%)";

                    // 設定年度から八年度前まで＋合計分
                    for (int j = 0; j <= 9; j++)
                    {
                        this.form.grdIchiran.Rows[i].Cells[yoteiName[j]].Value = ((int)table.Rows[i][yoteiName[j]]).ToString("#,0");
                        this.form.grdNendo.Rows[intCsvCount].Cells[csvNENDO[j]].Value = table.Rows[i][yoteiName[j]].ToString();

                        this.form.grdIchiran.Rows[i].Cells[mitsumoriName[j]].Value = ((int)table.Rows[i][mitsumoriName[j]]).ToString("#,0");
                        this.form.grdNendo.Rows[intCsvCount + 1].Cells[csvNENDO[j]].Value = table.Rows[i][mitsumoriName[j]].ToString();

                        yoteiWork = int.Parse(table.Rows[i][yoteiName[j]].ToString());
                        mitsumoriWork = int.Parse(table.Rows[i][mitsumoriName[j]].ToString());
                        if (yoteiWork == 0 && mitsumoriWork == 0)
                        {
                            this.form.grdIchiran.Rows[i].Cells[tasseiName[j]].Value = 0.ToString("#0.00");
                            this.form.grdNendo.Rows[intCsvCount + 2].Cells[csvNENDO[j]].Value = 0.ToString("#0.00");
                        }
                        else if (yoteiWork == 0 && mitsumoriWork > 0)
                        {
                            this.form.grdIchiran.Rows[i].Cells[tasseiName[j]].Value = 100.ToString("#0.00");
                            this.form.grdNendo.Rows[intCsvCount + 2].Cells[csvNENDO[j]].Value = 100.ToString("#0.00");
                        }
                        else
                        {
                            this.form.grdIchiran.Rows[i].Cells[tasseiName[j]].Value = (mitsumoriWork / yoteiWork * 100).ToString("#0.00");
                            this.form.grdNendo.Rows[intCsvCount + 2].Cells[csvNENDO[j]].Value = (mitsumoriWork / yoteiWork * 100).ToString("#0.00"); ;
                        }
                    }
                }

                this.form.grdIchiran.IsBrowsePurpose = false;

                #endregion
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region データセット（月次）

        /// <summary>
        /// データセット（月次）
        /// </summary>
        /// <param name="DataTable">取得データ格納テーブル</param>
        /// <param name="table">table</param>
        private void SetDataGetsuji(DataTable table)
        {
            LogUtility.DebugMethodStart(table);
            try
            {
                MultiRowInit(2);
                string[] MonthArray = new string[13];
                int count = 0;

                //CSV出力用DGVを削除
                this.form.grdGetsuji.Rows.Clear();

                #region データセット初期設定

                //検索条件（今年度）に対応したヘッダーを設定
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_01"].Value = this.kishuMonthArray[0];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_02"].Value = this.kishuMonthArray[1];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_03"].Value = this.kishuMonthArray[2];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_04"].Value = this.kishuMonthArray[3];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_05"].Value = this.kishuMonthArray[4];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_06"].Value = this.kishuMonthArray[5];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_07"].Value = this.kishuMonthArray[6];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_08"].Value = this.kishuMonthArray[7];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_09"].Value = this.kishuMonthArray[8];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_10"].Value = this.kishuMonthArray[9];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_11"].Value = this.kishuMonthArray[10];
                this.form.grdIchiran.ColumnHeaders[0].Cells["MONTH_12"].Value = this.kishuMonthArray[11];

                // テーブル作成(受注目標件数)
                this.ResultTable = new DataTable();
                ResultTable.Columns.Add("EIGYOU_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("EIGYOU_NAME", Type.GetType("System.String"));
                ResultTable.Columns.Add("BUSHO_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("BUSHO_NAME", Type.GetType("System.String"));
                ResultTable.Columns.Add("YOTEI_MONTH01", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH02", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH03", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH04", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH05", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH06", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH07", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH08", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH09", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH10", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH11", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_MONTH12", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("YOTEI_GOUKEI", Type.GetType("System.Int64"));
                ResultTable.Columns.Add("MITSUMORI_MONTH01", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH02", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH03", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH04", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH05", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH06", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH07", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH08", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH09", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH10", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH11", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_MONTH12", Type.GetType("System.Int32"));
                ResultTable.Columns.Add("MITSUMORI_GOUKEI", Type.GetType("System.Int64"));
                ResultTable.Columns.Add("SYSTEM_ID", Type.GetType("System.Int64"));
                ResultTable.Columns.Add("SEQ", Type.GetType("System.Int32"));

                //代入先テーブルの要素名作成（予定件数）
                string[] yoteiName = new string[13];
                yoteiName[0] = "YOTEI_MONTH01";
                yoteiName[1] = "YOTEI_MONTH02";
                yoteiName[2] = "YOTEI_MONTH03";
                yoteiName[3] = "YOTEI_MONTH04";
                yoteiName[4] = "YOTEI_MONTH05";
                yoteiName[5] = "YOTEI_MONTH06";
                yoteiName[6] = "YOTEI_MONTH07";
                yoteiName[7] = "YOTEI_MONTH08";
                yoteiName[8] = "YOTEI_MONTH09";
                yoteiName[9] = "YOTEI_MONTH10";
                yoteiName[10] = "YOTEI_MONTH11";
                yoteiName[11] = "YOTEI_MONTH12";
                yoteiName[12] = "YOTEI_GOUKEI";

                //代入元テーブルの要素名作成（予定件数）
                // 代入する要素を機種月から順に並び替える
                foreach (string Month in this.kishuMonthArray)
                {
                    MonthArray[count] = "YOTEI_MONTH" + int.Parse(Month.Replace("月", string.Empty)).ToString().PadLeft(2, '0');
                    count++;
                }
                MonthArray[12] = "YOTEI_GOUKEI";

                //テーブルの要素名作成（見積（受注）件数）
                string[] mitsumoriName = new string[13];
                mitsumoriName[0] = "MITSUMORI_MONTH01";
                mitsumoriName[1] = "MITSUMORI_MONTH02";
                mitsumoriName[2] = "MITSUMORI_MONTH03";
                mitsumoriName[3] = "MITSUMORI_MONTH04";
                mitsumoriName[4] = "MITSUMORI_MONTH05";
                mitsumoriName[5] = "MITSUMORI_MONTH06";
                mitsumoriName[6] = "MITSUMORI_MONTH07";
                mitsumoriName[7] = "MITSUMORI_MONTH08";
                mitsumoriName[8] = "MITSUMORI_MONTH09";
                mitsumoriName[9] = "MITSUMORI_MONTH10";
                mitsumoriName[10] = "MITSUMORI_MONTH11";
                mitsumoriName[11] = "MITSUMORI_MONTH12";
                mitsumoriName[12] = "MITSUMORI_GOUKEI";

                //テーブルの要素名作成（達成率）
                string[] tasseiName = new string[13];
                tasseiName[0] = "GETSUJI_TASSEI_RITSU_01";
                tasseiName[1] = "GETSUJI_TASSEI_RITSU_02";
                tasseiName[2] = "GETSUJI_TASSEI_RITSU_03";
                tasseiName[3] = "GETSUJI_TASSEI_RITSU_04";
                tasseiName[4] = "GETSUJI_TASSEI_RITSU_05";
                tasseiName[5] = "GETSUJI_TASSEI_RITSU_06";
                tasseiName[6] = "GETSUJI_TASSEI_RITSU_07";
                tasseiName[7] = "GETSUJI_TASSEI_RITSU_08";
                tasseiName[8] = "GETSUJI_TASSEI_RITSU_09";
                tasseiName[9] = "GETSUJI_TASSEI_RITSU_10";
                tasseiName[10] = "GETSUJI_TASSEI_RITSU_11";
                tasseiName[11] = "GETSUJI_TASSEI_RITSU_12";
                tasseiName[12] = "TASSEI_GOKEI";

                string[] csvMONTH = new string[13];
                csvMONTH[0] = "MONTH_01";
                csvMONTH[1] = "MONTH_02";
                csvMONTH[2] = "MONTH_03";
                csvMONTH[3] = "MONTH_04";
                csvMONTH[4] = "MONTH_05";
                csvMONTH[5] = "MONTH_06";
                csvMONTH[6] = "MONTH_07";
                csvMONTH[7] = "MONTH_08";
                csvMONTH[8] = "MONTH_09";
                csvMONTH[9] = "MONTH_10";
                csvMONTH[10] = "MONTH_11";
                csvMONTH[11] = "MONTH_12";
                csvMONTH[12] = "GOUKEI";

                #endregion

                //MultiRow設定用のDataTableに設定

                #region DataTableに設定

                foreach (DataRow r in table.Rows)
                {
                    // 値をRowへ設定後、DataTableに追加
                    DataRow newRow = this.ResultTable.NewRow();
                    // 部署、営業担当
                    newRow["EIGYOU_CD"] = r["EIGYOU_CD"];
                    newRow["EIGYOU_NAME"] = r["EIGYOU_NAME"];
                    newRow["BUSHO_CD"] = r["BUSHO_CD"];
                    newRow["BUSHO_NAME"] = r["BUSHO_NAME"];

                    // 受注目標件数
                    newRow["YOTEI_MONTH01"] = r["YOTEI_MONTH01"];
                    newRow["YOTEI_MONTH02"] = r["YOTEI_MONTH02"];
                    newRow["YOTEI_MONTH03"] = r["YOTEI_MONTH03"];
                    newRow["YOTEI_MONTH04"] = r["YOTEI_MONTH04"];
                    newRow["YOTEI_MONTH05"] = r["YOTEI_MONTH05"];
                    newRow["YOTEI_MONTH06"] = r["YOTEI_MONTH06"];
                    newRow["YOTEI_MONTH07"] = r["YOTEI_MONTH07"];
                    newRow["YOTEI_MONTH08"] = r["YOTEI_MONTH08"];
                    newRow["YOTEI_MONTH09"] = r["YOTEI_MONTH09"];
                    newRow["YOTEI_MONTH10"] = r["YOTEI_MONTH10"];
                    newRow["YOTEI_MONTH11"] = r["YOTEI_MONTH11"];
                    newRow["YOTEI_MONTH12"] = r["YOTEI_MONTH12"];
                    newRow["YOTEI_GOUKEI"] = r["YOTEI_GOUKEI"];

                    // 受注目標件数
                    newRow["MITSUMORI_MONTH01"] = r["MITSUMORI_MONTH01"];
                    newRow["MITSUMORI_MONTH02"] = r["MITSUMORI_MONTH02"];
                    newRow["MITSUMORI_MONTH03"] = r["MITSUMORI_MONTH03"];
                    newRow["MITSUMORI_MONTH04"] = r["MITSUMORI_MONTH04"];
                    newRow["MITSUMORI_MONTH05"] = r["MITSUMORI_MONTH05"];
                    newRow["MITSUMORI_MONTH06"] = r["MITSUMORI_MONTH06"];
                    newRow["MITSUMORI_MONTH07"] = r["MITSUMORI_MONTH07"];
                    newRow["MITSUMORI_MONTH08"] = r["MITSUMORI_MONTH08"];
                    newRow["MITSUMORI_MONTH09"] = r["MITSUMORI_MONTH09"];
                    newRow["MITSUMORI_MONTH10"] = r["MITSUMORI_MONTH10"];
                    newRow["MITSUMORI_MONTH11"] = r["MITSUMORI_MONTH11"];
                    newRow["MITSUMORI_MONTH12"] = r["MITSUMORI_MONTH12"];
                    newRow["MITSUMORI_GOUKEI"] = r["MITSUMORI_GOUKEI"];

                    this.ResultTable.Rows.Add(newRow);
                }

                #endregion

                //達成率計算用格納変数
                decimal yoteiWork;
                decimal mitsumoriWork;
                int intCsvCount;

                for (int i = 0; i < this.kishuMonthArray.Length; i++)
                {
                    this.form.grdGetsuji.Columns[i + 5].HeaderText = this.kishuMonthArray[i];
                }

                #region MultiRow,DataGridView作成処理(年度)

                this.form.grdIchiran.IsBrowsePurpose = false;

                //検索結果設定
                for (int i = 0; i < this.ResultTable.Rows.Count; i++)
                {
                    intCsvCount = i * 3;
                    this.form.grdIchiran.Rows.Add();
                    //MultiRow1行に対してDGVは3行作成
                    this.form.grdGetsuji.Rows.Add();
                    this.form.grdGetsuji.Rows.Add();
                    this.form.grdGetsuji.Rows.Add();

                    bushoCd = castPadding(table.Rows[i]["BUSHO_CD"].ToString(), 3);
                    eigyouCd = castPadding(table.Rows[i]["EIGYOU_CD"].ToString(), 6);

                    //  MultRowへ設定(部署、営業担当)
                    this.form.grdIchiran.Rows[i].Cells["BUSHO_CD"].Value = bushoCd;
                    this.form.grdIchiran.Rows[i].Cells["BUSHO_NAME"].Value = table.Rows[i]["BUSHO_NAME"].ToString();
                    this.form.grdIchiran.Rows[i].Cells["EIGYOU_CD"].Value = eigyouCd;
                    this.form.grdIchiran.Rows[i].Cells["EIGYOU_NAME"].Value = table.Rows[i]["EIGYOU_NAME"].ToString();

                    this.form.grdGetsuji.Rows[intCsvCount].Cells["BUSHO_CD"].Value = bushoCd;
                    this.form.grdGetsuji.Rows[intCsvCount].Cells["BUSHO_NAME"].Value = table.Rows[i]["BUSHO_NAME"].ToString();
                    this.form.grdGetsuji.Rows[intCsvCount].Cells["EIGYOU_CD"].Value = eigyouCd;
                    this.form.grdGetsuji.Rows[intCsvCount].Cells["EIGYOU_NAME"].Value = table.Rows[i]["EIGYOU_NAME"].ToString();
                    this.form.grdGetsuji.Rows[intCsvCount].Cells["JYOUKYOU"].Value = "予定件数";
                    this.form.grdGetsuji.Rows[intCsvCount + 1].Cells["BUSHO_CD"].Value = bushoCd;
                    this.form.grdGetsuji.Rows[intCsvCount + 1].Cells["BUSHO_NAME"].Value = table.Rows[i]["BUSHO_NAME"].ToString();
                    this.form.grdGetsuji.Rows[intCsvCount + 1].Cells["EIGYOU_CD"].Value = eigyouCd;
                    this.form.grdGetsuji.Rows[intCsvCount + 1].Cells["EIGYOU_NAME"].Value = table.Rows[i]["EIGYOU_NAME"].ToString();
                    this.form.grdGetsuji.Rows[intCsvCount + 1].Cells["JYOUKYOU"].Value = "受注件数";
                    this.form.grdGetsuji.Rows[intCsvCount + 2].Cells["BUSHO_CD"].Value = bushoCd;
                    this.form.grdGetsuji.Rows[intCsvCount + 2].Cells["BUSHO_NAME"].Value = table.Rows[i]["BUSHO_NAME"].ToString();
                    this.form.grdGetsuji.Rows[intCsvCount + 2].Cells["EIGYOU_CD"].Value = eigyouCd;
                    this.form.grdGetsuji.Rows[intCsvCount + 2].Cells["EIGYOU_NAME"].Value = table.Rows[i]["EIGYOU_NAME"].ToString();
                    this.form.grdGetsuji.Rows[intCsvCount + 2].Cells["JYOUKYOU"].Value = "達成率(%)";

                    // 年度分(年度開始月から終了月まで)＋合計分
                    for (int j = 0; j <= 12; j++)
                    {
                        this.form.grdIchiran.Rows[i][yoteiName[j]].Value = ((int)table.Rows[i][MonthArray[j]]).ToString("#,0");
                        this.form.grdGetsuji.Rows[intCsvCount].Cells[csvMONTH[j]].Value = table.Rows[i][MonthArray[j]].ToString();

                        this.form.grdIchiran.Rows[i][mitsumoriName[j]].Value = ((int)table.Rows[i][mitsumoriName[j]]).ToString("#,0");
                        this.form.grdGetsuji.Rows[intCsvCount + 1].Cells[csvMONTH[j]].Value = ((int)table.Rows[i][mitsumoriName[j]]).ToString("#,0");

                        yoteiWork = int.Parse(table.Rows[i][MonthArray[j]].ToString());
                        mitsumoriWork = int.Parse(table.Rows[i][mitsumoriName[j]].ToString());
                        if (yoteiWork == 0 && mitsumoriWork == 0)
                        {
                            this.form.grdIchiran.Rows[i][tasseiName[j]].Value = 0.ToString("#0.00");
                            this.form.grdGetsuji.Rows[intCsvCount + 2].Cells[csvMONTH[j]].Value = 0.ToString("#0.00");
                        }
                        else if (yoteiWork == 0 && mitsumoriWork > 0)
                        {
                            this.form.grdIchiran.Rows[i][tasseiName[j]].Value = 100.ToString("#0.00");
                            this.form.grdGetsuji.Rows[intCsvCount + 2].Cells[csvMONTH[j]].Value = 100.ToString("#0.00");
                        }
                        else
                        {
                            this.form.grdIchiran.Rows[i][tasseiName[j]].Value = (mitsumoriWork / yoteiWork * 100).ToString("#0.00");
                            this.form.grdGetsuji.Rows[intCsvCount + 2].Cells[csvMONTH[j]].Value = (mitsumoriWork / yoteiWork * 100).ToString("#0.00");
                        }
                    }
                }

                this.form.grdIchiran.IsBrowsePurpose = true;

                #endregion
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ゼロパティング

        /// <summary>
        /// ゼロパティング
        /// </summary>
        /// <param name="tage">変換文字列</param>
        /// <param name="keta">変更後桁数</param>
        private string castPadding(string tage, int keta)
        {
            try
            {
                string res = tage;
                if (string.IsNullOrWhiteSpace(res))
                {
                    res = "0".PadLeft(keta, '0');
                }
                else
                {
                    int tmp;
                    if (!int.TryParse(res, out tmp))
                    {
                        tmp = 0;
                    }
                    res = tmp.ToString().PadLeft(keta, '0');
                }
                return res;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region １～４カラム+最終カラム(１７)目を編集不可にする

        /// <summary>
        /// １～４カラム目を編集不可にする
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void customDataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (e.ColumnIndex <= 3 || e.ColumnIndex > 15)
                {
                    e.Cancel = true;
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

        #endregion

        #region HeaderForm取得

        /// <summary>
        /// HeaderForm取得
        /// </summary>
        /// <param name="hs">hs</param>
        public void SetHeaderInfo(HeaderForm hs)
        {
            LogUtility.DebugMethodStart(hs);

            this.headerForm = hs;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region デフォルトメソッド

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

        #region デフォルトメソッド

        /// <summary>
        /// popup用部署情報取得
        /// </summary>
        /// <returns>部署情報</returns>
        public DataTable getPopupBusyoInfo(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            DataTable popupData = null;
            catchErr = false;
            try
            {
                M_BUSHO condition = new M_BUSHO();
                popupData = this.dao.GetBushoDataForEntity(condition);

                // ポップアップのタイトル
                popupData.TableName = "部署検索";
            }
            catch (Exception ex)
            {
                LogUtility.Error("getPopupBusyoInfo", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                popupData = null;
                catchErr = true;
            }
            LogUtility.DebugMethodEnd(popupData, catchErr);
            return popupData;
        }

        /// <summary>
        /// 部署コードより、部署情報取得
        /// </summary>
        /// <param name="p_busyoCD">部署コード</param>
        /// <returns>部署情報</returns>
        public DataTable getInfoByBusyoCD(string p_busyoCD)
        {
            LogUtility.DebugMethodStart(p_busyoCD);

            M_BUSHO condition = new M_BUSHO();
            condition.BUSHO_CD = p_busyoCD;
            DataTable busyoInfo = this.dao.GetBushoDataForEntity(condition);

            LogUtility.DebugMethodEnd(busyoInfo);
            return busyoInfo;
        }

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
    }
}