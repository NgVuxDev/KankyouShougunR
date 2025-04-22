using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Carriage.UntinSyuusyuuhyoPopup;
using CommonChouhyouPopup.App;

namespace Shougun.Core.Carriage.UntinSyuusyuuhyoPopup
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Carriage.UntinSyuusyuuhyoPopup.Setting.ButtonSetting.xml";

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// DAOClass
        /// </summary>
        private DAOClass mDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;
        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        /// <summary>
        /// Header
        /// </summary>
        private UIHeader header;
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;
        /// <summary>
        /// 帳票情報データ
        /// </summary>
        private DataTable mReportInfoData;
        /// <summary>
        /// 帳票情報Header部データ
        /// </summary>
        private DataTable mReportHeaderInfo;
        /// <summary>
        ///  帳票情報Detail部データ
        /// </summary>
        private DataTable mReportDetailInfo;
        /// <summary>
        ///  帳票情報Footer部データ
        /// </summary>
        private DataTable mReportFooterInfo;
        /// <summary>
        /// 
        /// </summary>
        private DtoCls SearchCon;
        #endregion

        #region プロパティ

        /// <summary>帳票情報格納クラス</summary>
        public ReportInfoBase ReportInfo { get;set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                //DAO
                this.mDao = DaoInitUtility.GetComponent<DAOClass>();
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();    

                // 共通Dao               
                this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
                this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();              

                msgLogic = new MessageBoxShowLogic();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
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
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;              

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                //　ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
                //伝票日付範囲指定※
                this.form.HIDUKE_FROM.Text = parentForm.sysDate.ToShortDateString();
                this.form.HIDUKE_TO.Text = parentForm.sysDate.ToShortDateString();
                parentForm.Height = this.form.formHeight;

                parentForm.Width = this.form.formWidth;

                parentForm.bt_func1.Visible = false;
                parentForm.bt_func2.Visible = false;
                parentForm.bt_func3.Visible = false;
                parentForm.bt_func4.Visible = false;
                parentForm.bt_func5.Visible = false;
                parentForm.bt_func6.Visible = false;
                parentForm.bt_func7.Visible = false;
                parentForm.bt_func8.Visible = false;
                parentForm.bt_func9.Visible = true;
                parentForm.bt_func10.Visible = false;
                parentForm.bt_func11.Visible = false;
                parentForm.bt_func12.Visible = true;
                parentForm.lb_hint.Visible = false;

                //parentForm.pn_foot.Visible = false;
                //parentForm.pn_foot.Location = new Point(0, 700);

                //int h = this.form.btnClosed.Height;
                //int w = this.form.btnClosed.Width;
                //this.form.btnClosed.Visible = false;
                //this.form.btnSearch.Visible = false; 
                //parentForm.ProcessButtonPanel.Location = new Point(0, 0);
                //parentForm.ProcessButtonPanel.Visible = false;
                //parentForm.ProcessButtonPanel.Height = 0;
                //parentForm.ProcessButtonPanel.Width = 0;

                var p = parentForm.bt_func9.Location;
                parentForm.bt_func9.Location = new System.Drawing.Point(497, p.Y - 20);
                parentForm.bt_func12.Location = new System.Drawing.Point(604, p.Y - 20);
                parentForm.Name = "運賃集計表条件指定ポップアップ";
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BasePopForm)this.form.Parent;

                //ヘッダーの初期化
                UIHeader targetHeader = (UIHeader)parentForm.headerForm;
                this.header = targetHeader;
                //
                this.header.lb_title.Text = "運賃集計表条件指定ポップアップ";
                this.header.windowTypeLabel.Visible = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }     
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (BasePopForm)this.form.Parent;

                //実行ボタン(F9)イベント生成                  
                parentForm.bt_func9.Click += new EventHandler(this.form.btnSearch_Click);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;
                //クローズ処理(F12)イベント生成              
                parentForm.bt_func12.Click += new EventHandler(this.form.btnClosed_Click);

                /// 20141023 Houkakou 「運賃集計表」のダブルクリックを追加する　start
                // 「To」のイベント生成
                this.form.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
                this.form.UNPAN_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(UNPAN_GYOUSHA_CD_TO_MouseDoubleClick);
                this.form.NIZUMI_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIZUMI_GYOUSHA_CD_TO_MouseDoubleClick);
                this.form.NIZUMI_GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIZUMI_GENBA_CD_TO_MouseDoubleClick);
                this.form.NIOROSHI_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIOROSHI_GYOUSHA_CD_TO_MouseDoubleClick);
                this.form.NIOROSHI_GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIOROSHI_GENBA_CD_TO_MouseDoubleClick);
                /// 20141023 Houkakou 「運賃集計表」のダブルクリックを追加する　end

                /// 20141209 teikyou 日付チェックを追加する　start
                this.form.HIDUKE_FROM.Leave += new System.EventHandler(HIDUKE_FROM_Leave);
                this.form.HIDUKE_TO.Leave += new System.EventHandler(HIDUKE_TO_Leave);
                /// 20141209 teikyou 日付チェックを追加する　end

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();              
                var parentForm = (BasePopForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
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
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 実行処理（F9）

        /// <summary>
        /// 実行処理
        /// </summary>
        [Transaction]
        public void Jikou()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var messageShowLogic = new MessageBoxShowLogic();
                 SearchCon = new DtoCls();
                //伝票日付
                SearchCon.DENPYOU_DATE_FROM = this.form.HIDUKE_FROM.Value.ToString();
                SearchCon.DENPYOU_DATE_TO = this.form.HIDUKE_TO.Value.ToString();
                //拠点CD
                SearchCon.KYOTEN_CD = this.form.KYOTEN_CD.Text.Trim();
                //運搬情報
                SearchCon.UNPAN_GYOUSHA_CD_FROM = this.form.UNPAN_GYOUSHA_CD.Text.Trim();
                SearchCon.UNPAN_GYOUSHA_CD_TO = this.form.UNPAN_GYOUSHA_CD_TO.Text.Trim();

                //荷積業者CD
                SearchCon.NIZUMI_GYOUSHA_CD_FROM = this.form.NIZUMI_GYOUSHA_CD.Text.Trim();
                SearchCon.NIZUMI_GYOUSHA_CD_TO = this.form.NIZUMI_GYOUSHA_CD_TO.Text.Trim();
                SearchCon.NIZUMI_GENBA_CD_FROM = this.form.NIZUMI_GENBA_CD.Text.Trim();
                SearchCon.NIZUMI_GENBA_CD_TO = this.form.NIZUMI_GENBA_CD_TO.Text.Trim();

                //荷降
                SearchCon.NIOROSHI_GYOUSHA_CD_FROM = this.form.NIOROSHI_GYOUSHA_CD.Text.Trim();
                SearchCon.NIOROSHI_GYOUSHA_CD_TO = this.form.NIOROSHI_GYOUSHA_CD_TO.Text.Trim();
                SearchCon.NIOROSHI_GENBA_CD_FROM = this.form.NIOROSHI_GENBA_CD.Text.Trim();
                SearchCon.NIOROSHI_GENBA_CD_TO = this.form.NIOROSHI_GENBA_CD_TO.Text.Trim();


               this.mReportInfoData = this.mDao.GetReportData(SearchCon);

               //読込み件数チェック
               if (this.mReportInfoData == null || this.mReportInfoData.Rows.Count <= 0)
               {  
                   //読込データ件数を0にする                 
                   messageShowLogic.MessageBoxShow("W002", "運賃集計表");
                   return;
               }
               //読込データ件数
               SearchCon.readDataNumber = this.mReportInfoData.Rows.Count.ToString();
              
                //システム情報を取得する
               M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
               if (sysInfo != null)
               {
                   //Headarのアラート件数を処理する。
                   DialogResult result = DialogResult.Yes;
                   //システム情報からアラート件数を取得
                   SearchCon.alertNumber = sysInfo[0].ICHIRAN_ALERT_KENSUU.ToString();
                   if (!string.IsNullOrEmpty(SearchCon.alertNumber) && !SearchCon.alertNumber.Equals("0") && int.Parse(SearchCon.alertNumber) <int.Parse( SearchCon.readDataNumber))
                   {                     
                       result = messageShowLogic.MessageBoxShow("C025");
                   }
                   if (result != DialogResult.Yes)
                   {
                       return;
                   }
               }

                //レポートＩＮＦＯ（DataTableList）作成
               this.ReportInfo = new ReportInfoR483(WINDOW_ID.R_UNNCHIN_SYUUKEIHYOU);
               
                //帳票Header情報取得
               this.CreateHeaderDataTable();
               // //帳票詳細情報取得
               this.CreateDetailDataTable();
               // //帳票Footer情報取得
               this.CreateFooterDataTable();  

                //ダイアログClose処理
               var parentForm = (BasePopForm)this.form.Parent;
               this.form.Close();
               parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 終了処理（F12）

        /// <summary>
        /// 終了する。
        /// </summary>
        public void FormClose()
        {
            try
            { 
                var parentForm = (BasePopForm)this.form.Parent;             

                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion             

        #region 運搬業者チェック
        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        internal bool CheckUnpanGyoushaCd(CustomTextBox Cd, CustomTextBox name)
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart(Cd, name);
                //名称クリア
                name.Text = string.Empty;
                // 入力されてない場合
                if (String.IsNullOrEmpty(Cd.Text))
                {
                    returnVal = true;
                    // 処理終了
                    return true;
                }

                // 業者情報取得
                var gyousha = this.GetGyousha(Cd.Text);
                // 20151023 BUNN #12040 STR
                if (!gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151023 BUNN #12040 END
                {
                    this.msgLogic.MessageBoxShow("E062", "運搬業者");
                    return returnVal;
                }
                else
                {
                    // 運搬業者名を設定
                    name.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }

                returnVal = true;
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

        #region 業者チェック
        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha(CustomTextBox Cd, CustomTextBox name)
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart(Cd, name);

                name.Text = string.Empty;
                // 入力されてない場合
                if (String.IsNullOrEmpty(Cd.Text))
                {
                    returnVal = true;
                    // 処理終了
                    return returnVal;
                }

                // 業者を取得
                var gyoushaEntity = this.GetGyousha((Cd.Text));
                // 取得できない場合
                if (gyoushaEntity == null)
                {
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    // 処理終了
                    return returnVal;
                }
                else
                {
                    // 業者名を設定
                    name.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                }
               
                returnVal = true;
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

        #region 現場チェック
        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba(CustomTextBox GyoushaCd, CustomTextBox Cd, CustomTextBox name)
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                // 現場の関連情報をクリア
                name.Text = string.Empty;               

                // 入力されてない場合
                if (String.IsNullOrEmpty(Cd.Text))
                {
                   
                    returnVal = true;
                    // 処理終了
                    return returnVal;
                }

                // 現場CDで現場情報取得（複数）
                var genbaEntityList = this.GetGenba(Cd.Text);
                if (genbaEntityList == null)
                {
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    // 処理終了
                    return returnVal;
                }

                // 業者入力されてない場合
                if (string.IsNullOrEmpty(GyoushaCd.Text))
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E051", "業者");
                    Cd.Text = string.Empty;
                    return returnVal;
                }

                // 現場情報を取得
                M_GENBA genbaEntity = new M_GENBA();
                genbaEntity = GetGenba(GyoushaCd.Text, Cd.Text);
                // 取得できない場合
                if (genbaEntity == null)
                {
                    this.msgLogic.MessageBoxShow("E062", "業者");
                    // 処理終了
                    return returnVal;
                }

                

                // 現場情報設定
                name.Text = genbaEntity.GENBA_NAME_RYAKU;
               

                returnVal = true;
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

        #region 荷済業者チェック
        /// <summary>
        /// 荷済業者チェック
        /// </summary>
        internal bool CheckNizumiGyoushaCd(CustomTextBox Cd, CustomTextBox name)
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart(Cd, name);
                //名称クリア
                name.Text = string.Empty;
                // 入力されてない場合
                if (String.IsNullOrEmpty(Cd.Text))
                {
                    returnVal = true;
                    // 処理終了
                    return true;
                }

                // 業者情報取得
                var gyousha = this.GetGyousha(Cd.Text);
                if (!gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue && !gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    this.msgLogic.MessageBoxShow("E062", "荷済業者");
                    return returnVal;
                }
                else
                {
                    // 荷降業者名を設定
                    name.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }

                returnVal = true;
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

        #region 荷済場チェック
        /// <summary>
        /// 荷済場チェック
        /// </summary>
        internal bool ChechNizumiGenbaCd(CustomTextBox GyoushaCd, CustomTextBox Cd, CustomTextBox name)
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart(GyoushaCd, Cd, name);

                // 荷降場の関連情報をクリア
                name.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(Cd.Text))
                {
                    returnVal = true;
                    // 処理終了
                    return true;
                }

                // 荷降場CDで現場情報取得（複数）
                var genbaEntityList = this.GetGenba(Cd.Text);
                if (genbaEntityList == null)
                {
                    this.msgLogic.MessageBoxShow("E020", "荷済場");
                    // 処理終了
                    return returnVal;
                }

                // 荷降業者入力されてない場合
                if (string.IsNullOrEmpty(GyoushaCd.Text))
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E051", "荷済業者");
                    Cd.Text = string.Empty;
                    return returnVal;
                }

                // 荷降場情報を取得
                M_GENBA genbaEntity = new M_GENBA();
                genbaEntity = GetGenba(GyoushaCd.Text, Cd.Text);
                // 取得できない場合
                if (genbaEntity == null)
                {
                    // 一致するデータがないのでエラー
                    this.msgLogic.MessageBoxShow("E062", "荷済業者");
                    // 処理終了
                    return returnVal;
                }
                // 排出事業場/荷積現場区分チェック
                if (!genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue &&
                    !genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue)
                {
                    // 一致するデータがないのでエラー
                    this.msgLogic.MessageBoxShow("E062", "荷済場");
                    // 処理終了
                    return returnVal;
                }

                // 荷降場情報設定
                name.Text = genbaEntity.GENBA_NAME_RYAKU;

                returnVal = true;
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

        #region 荷降業者チェック
        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyoushaCd(CustomTextBox Cd, CustomTextBox name)
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart(Cd, name);
                //名称クリア
                name.Text = string.Empty;
                // 入力されてない場合
                if (String.IsNullOrEmpty(Cd.Text))
                {
                    returnVal = true;
                    // 処理終了
                    return true;
                }

                // 業者情報取得
                var gyousha = this.GetGyousha(Cd.Text);
                if (!gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN && !gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN)
                {
                    this.msgLogic.MessageBoxShow("E062", "荷降業者");
                    return returnVal;
                }
                else
                {
                    // 荷降業者名を設定
                    name.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }

                returnVal = true;
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

        #region 荷降場チェック
        /// <summary>
        /// 荷降場チェック
        /// </summary>
        internal bool ChechNioroshiGenbaCd(CustomTextBox GyoushaCd, CustomTextBox Cd, CustomTextBox name)
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart(GyoushaCd,Cd, name);

                // 荷降場の関連情報をクリア
                name.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(Cd.Text))
                {
                    returnVal = true;
                    // 処理終了
                    return true;
                }

                // 荷降場CDで現場情報取得（複数）
                var genbaEntityList = this.GetGenba(Cd.Text);
                if (genbaEntityList == null)
                {
                    this.msgLogic.MessageBoxShow("E020", "荷降場");
                    // 処理終了
                    return returnVal;
                }

                // 荷降業者入力されてない場合
                if (string.IsNullOrEmpty(GyoushaCd.Text))
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E051", "荷降業者");
                    Cd.Text = string.Empty;
                    return returnVal;
                }

                // 荷降場情報を取得
                M_GENBA genbaEntity = new M_GENBA();
                genbaEntity = GetGenba(GyoushaCd.Text, Cd.Text);
                // 取得できない場合
                if (genbaEntity == null)
                {
                    // 一致するデータがないのでエラー
                    this.msgLogic.MessageBoxShow("E062", "荷降業者");
                    // 処理終了
                    return returnVal;
                }
                // 処分事業場区分、荷降場区分チェック
                if (!genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue &&
                    !genbaEntity.SAISHUU_SHOBUNJOU_KBN.IsTrue &&
                    !genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue)
                {
                    // 一致するデータがないのでエラー
                    this.msgLogic.MessageBoxShow("E062", "荷降場");
                    // 処理終了
                    return returnVal;
                }

                // 荷降場情報設定
                name.Text = genbaEntity.GENBA_NAME_RYAKU;

                returnVal = true;
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

        #region 各マスター情報を取得
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

                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

                if (gyousha != null && gyousha.Length > 0)
                {
                    returnVal = gyousha[0];
                }

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

        /// <summary>
        /// 現場取得(複数)
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string genbaCd)
        {
            M_GENBA[] returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(genbaCd);

                if (string.IsNullOrEmpty(genbaCd))
                {
                    return returnVal;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba != null && genba.Length > 0)
                {
                    returnVal = genba;
                }

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

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd)
        {
            M_GENBA returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
                {
                    return returnVal;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba != null && genba.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = genba[0];
                }

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

        #region uitility

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            try
            {
                //LogUtility.DebugMethodStart(obj, value);
                if (obj is DBNull)
                {
                    return value;
                }
                else if (obj.GetType().Namespace.Equals("System.Data.SqlTypes"))
                {
                    INullable objChk = (INullable)obj;
                    if (objChk.IsNull)
                        return value;
                    else
                        return obj;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
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

        #endregion

        #region ベースフォームのメッソドの実例化(ロジックは実装しない)

        /// <summary>
        ///データ検索処理
        /// </summary>
        public int Search()
        {
            return 0;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {

        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {

        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update(bool errorFlag)
        {

        }
        #endregion

        #region 帳票情報取得
        /// <summary>//帳票Header情報取得</summary>
        public void CreateHeaderDataTable()
        {           
         
            #region - Header -

            mReportHeaderInfo = new DataTable();
            mReportHeaderInfo.TableName = "Header";

            // 会社略称
            mReportHeaderInfo.Columns.Add("CORP_RYAKU_NAME");
            // 発行日時
            mReportHeaderInfo.Columns.Add("PRINT_DATE");
            // 拠点名
            mReportHeaderInfo.Columns.Add("KYOTEN_NAME");
            // 伝票日付
            mReportHeaderInfo.Columns.Add("DENPYOU_DATE");
            mReportHeaderInfo.Columns.Add("DENPYOU_DATE_BEGIN");
            mReportHeaderInfo.Columns.Add("DENPYOU_DATE_END");
            // 運搬業者
            mReportHeaderInfo.Columns.Add("FILL_COND_CD_1");
            mReportHeaderInfo.Columns.Add("FILL_COND_1_CD_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_1_VALUE_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_1_CD_END");
            mReportHeaderInfo.Columns.Add("FILL_COND_1_VALUE_END");
            // 荷積業者
            mReportHeaderInfo.Columns.Add("FILL_COND_CD_2");
            mReportHeaderInfo.Columns.Add("FILL_COND_2_CD_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_2_VALUE_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_2_CD_END");
            mReportHeaderInfo.Columns.Add("FILL_COND_2_VALUE_END");
            // 荷積現場
            mReportHeaderInfo.Columns.Add("FILL_COND_CD_3");
            mReportHeaderInfo.Columns.Add("FILL_COND_3_CD_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_3_VALUE_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_3_CD_END");
            mReportHeaderInfo.Columns.Add("FILL_COND_3_VALUE_END");
            // 荷降業者
            mReportHeaderInfo.Columns.Add("FILL_COND_CD_4");
            mReportHeaderInfo.Columns.Add("FILL_COND_4_CD_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_4_VALUE_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_4_CD_END");
            mReportHeaderInfo.Columns.Add("FILL_COND_4_VALUE_END");
            // 荷降現場
            mReportHeaderInfo.Columns.Add("FILL_COND_CD_5");
            mReportHeaderInfo.Columns.Add("FILL_COND_5_CD_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_5_VALUE_BEGIN");
            mReportHeaderInfo.Columns.Add("FILL_COND_5_CD_END");
            mReportHeaderInfo.Columns.Add("FILL_COND_5_VALUE_END");
            // アラート件数
            mReportHeaderInfo.Columns.Add("ALERT_NUMBER");
            // 読込データ件数
            mReportHeaderInfo.Columns.Add("READ_DATA_NUMBER");           

            DataRow rowHeader = mReportHeaderInfo.NewRow();

            // 会社略称情報
            DataTable dataTable = mDao.GetDateForStringSql("SELECT M_CORP_INFO.CORP_RYAKU_NAME from M_CORP_INFO");
            // 会社略称
            rowHeader["CORP_RYAKU_NAME"] = (string)dataTable.Rows[0].ItemArray[0]; ;
            // 発行日時
            rowHeader["PRINT_DATE"] =DateTime.Now;
            // 拠点名
            rowHeader["KYOTEN_NAME"] = this.form.KYOTEN_NAME_RYAKU.Text;
            // 伝票日付
            rowHeader["DENPYOU_DATE"] = DateTime.Parse(this.form.HIDUKE_FROM.Text).ToShortDateString() + " ～ " + DateTime.Parse(this.form.HIDUKE_TO.Text).ToShortDateString();
            rowHeader["DENPYOU_DATE_BEGIN"] = this.form.HIDUKE_FROM.Text.ToString() ;
            rowHeader["DENPYOU_DATE_END"] = this.form.HIDUKE_TO.Text.ToString();
            // 運搬業者
            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_TO.Text))
            {
                rowHeader["FILL_COND_CD_1"] = "全て";
            }
            else
            {
                rowHeader["FILL_COND_CD_1"] = this.form.UNPAN_GYOUSHA_CD.Text.ToString() + " ～ " + this.form.UNPAN_GYOUSHA_CD_TO.Text.ToString();
            }
            rowHeader["FILL_COND_1_CD_BEGIN"] = this.form.UNPAN_GYOUSHA_CD.Text.ToString();
            rowHeader["FILL_COND_1_VALUE_BEGIN"] = this.form.UNPAN_GYOUSHA_NAME.Text.ToString();
            rowHeader["FILL_COND_1_CD_END"] = this.form.UNPAN_GYOUSHA_CD_TO.Text.ToString();
            rowHeader["FILL_COND_1_VALUE_END"] =  this.form.UNPAN_GYOUSHA_NAME_TO.Text.ToString();
 
            // 荷積業者
            if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD_TO.Text))
            {
                rowHeader["FILL_COND_CD_2"] = "全て";
            }
            else
            {
                rowHeader["FILL_COND_CD_2"] = this.form.NIZUMI_GYOUSHA_CD.Text.ToString() + " ～ " + this.form.NIZUMI_GYOUSHA_CD_TO.Text.ToString();
            }
            rowHeader["FILL_COND_2_CD_BEGIN"] = this.form.NIZUMI_GYOUSHA_CD.Text.ToString();
            rowHeader["FILL_COND_2_VALUE_BEGIN"] = this.form.NIZUMI_GYOUSHA_NAME.Text.ToString();
            rowHeader["FILL_COND_2_CD_END"] = this.form.NIZUMI_GYOUSHA_CD_TO.Text.ToString();
            rowHeader["FILL_COND_2_VALUE_END"] = this.form.NIZUMI_GYOUSHA_NAME_TO.Text.ToString();
            // 荷積現場
            if (string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text) && string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD_TO.Text))
            {
                rowHeader["FILL_COND_CD_3"] = "全て";
            }
            else
            {
                rowHeader["FILL_COND_CD_3"] = this.form.NIZUMI_GENBA_CD.Text.ToString() + " ～ " + this.form.NIZUMI_GENBA_CD_TO.Text.ToString();
            }
            rowHeader["FILL_COND_3_CD_BEGIN"] = this.form.NIZUMI_GENBA_CD.Text.ToString();
            rowHeader["FILL_COND_3_VALUE_BEGIN"] = this.form.NIZUMI_GENBA_NAME.Text.ToString();
            rowHeader["FILL_COND_3_CD_END"] = this.form.NIZUMI_GENBA_CD_TO.Text.ToString();
            rowHeader["FILL_COND_3_VALUE_END"] = this.form.NIZUMI_GENBA_NAME_TO.Text.ToString();
            // 荷降業者
            if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD_TO.Text))
            {
                rowHeader["FILL_COND_CD_4"] = "全て";
            }
            else
            {
                rowHeader["FILL_COND_CD_4"] = this.form.NIOROSHI_GYOUSHA_CD.Text.ToString() + " ～ " + this.form.NIOROSHI_GYOUSHA_CD_TO.Text.ToString();

            }
            rowHeader["FILL_COND_4_CD_BEGIN"] = this.form.NIOROSHI_GYOUSHA_CD.Text.ToString();
            rowHeader["FILL_COND_4_VALUE_BEGIN"] = this.form.NIOROSHI_GYOUSHA_NAME.Text.ToString();
            rowHeader["FILL_COND_4_CD_END"] = this.form.NIOROSHI_GYOUSHA_CD_TO.Text.ToString();
            rowHeader["FILL_COND_4_VALUE_END"] = this.form.NIOROSHI_GYOUSHA_NAME_TO.Text.ToString();
            // 荷降現場
            if (string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text) && string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD_TO.Text))
            {
                rowHeader["FILL_COND_CD_5"] = "全て";
            }
            else
            {
                rowHeader["FILL_COND_CD_5"] = this.form.NIOROSHI_GENBA_CD.Text.ToString() + " ～ " + this.form.NIOROSHI_GENBA_CD_TO.Text.ToString();
            }
            rowHeader["FILL_COND_5_CD_BEGIN"] = this.form.NIOROSHI_GENBA_CD.Text.ToString();
            rowHeader["FILL_COND_5_VALUE_BEGIN"] = this.form.NIOROSHI_GENBA_NAME.Text.ToString();
            rowHeader["FILL_COND_5_CD_END"] = this.form.NIOROSHI_GENBA_CD_TO.Text.ToString();
            rowHeader["FILL_COND_5_VALUE_END"] = this.form.NIOROSHI_GENBA_NAME_TO.Text.ToString();
            // アラート件数
            rowHeader["ALERT_NUMBER"] = this.SearchCon.alertNumber;
            // 読込データ件数
            rowHeader["READ_DATA_NUMBER"] = this.SearchCon.readDataNumber;

            mReportHeaderInfo.Rows.Add(rowHeader);          

            // データを設定する処理を入れる
            this.ReportInfo.DataTableList.Add("Header", this.mReportHeaderInfo);

            #endregion - Header -         
        }
        /// <summary>帳票詳細情報取得 </summary>
        public void CreateDetailDataTable()
        {   

            #region - Detail -

            mReportDetailInfo = new DataTable();
            DataRow rowDetail;
            mReportDetailInfo.TableName = "Detail";

            // 運搬業者コード
            mReportDetailInfo.Columns.Add("UNPAN_GYOUSHA_CD");
            // 運搬業者名
            mReportDetailInfo.Columns.Add("UNPAN_GYOUSHA_NAME");
            // 荷積業者コード
            mReportDetailInfo.Columns.Add("NZM_GYOUSHA_CD");
            // 荷積業者名
            mReportDetailInfo.Columns.Add("NZM_GYOUSHA_NAME");
            // 荷積現場コード
            mReportDetailInfo.Columns.Add("NZM_GENBA_CD");
            // 荷積現場名
            mReportDetailInfo.Columns.Add("NZM_GENBA_NAME");
            // 荷降業者コード
            mReportDetailInfo.Columns.Add("NOS_GYOUSHA_CD");
            // 荷降業者名
            mReportDetailInfo.Columns.Add("NOS_GYOUSHA_NAME");
            // 荷降現場コード
            mReportDetailInfo.Columns.Add("NOS_GENBA_CD");
            // 荷降現場名
            mReportDetailInfo.Columns.Add("NOS_GENBA_NAME");
            // 伝票種類
            mReportDetailInfo.Columns.Add("DENPYOU_SHURUI");
            // 車種コード
            mReportDetailInfo.Columns.Add("SHASHU_CD");
            // 車輌コード
            mReportDetailInfo.Columns.Add("SHARYOU_CD");
            // 運転者コード
            mReportDetailInfo.Columns.Add("UNTENSHA_CD");
            // 車種名
            mReportDetailInfo.Columns.Add("SHASHU_NAME");
            // 車輌名
            mReportDetailInfo.Columns.Add("SHARYOU_NAME");
            // 運転者名
            mReportDetailInfo.Columns.Add("UNTENSHA_NAME");
            // 品名コード
            mReportDetailInfo.Columns.Add("HINMEI_CD");
            // 品名
            mReportDetailInfo.Columns.Add("HINMEI_NAME");
            // 正味(kg)
            mReportDetailInfo.Columns.Add("SYOUMI");
            // 数量
            mReportDetailInfo.Columns.Add("SUURYOU");
            // 単位
            mReportDetailInfo.Columns.Add("UNIT_NAME");
            // 運賃金額
            mReportDetailInfo.Columns.Add("UNCHIN");
            // 運賃消費税
            mReportDetailInfo.Columns.Add("TAX");
            // 運賃合計額
            mReportDetailInfo.Columns.Add("KINGAKU");
            // 正味合計
            mReportDetailInfo.Columns.Add("SHOUMI_KEI");
            // 運賃金額計
            mReportDetailInfo.Columns.Add("KINGAKU_KEI");
            // 運賃消費税
            mReportDetailInfo.Columns.Add("SHOHIZEI_TOTAL");
            // 運賃合計金額
            mReportDetailInfo.Columns.Add("KINGAKU_TOTAL");

            foreach (DataRow dtRow in  this.mReportInfoData.Rows )
            {
                rowDetail = mReportDetailInfo.NewRow();
                // 運搬業者コード
                rowDetail["UNPAN_GYOUSHA_CD"] = dtRow["UNPAN_GYOUSHA_CD"].ToString();
                // 運搬業者名
                rowDetail["UNPAN_GYOUSHA_NAME"] = dtRow["UNPAN_GYOUSHA_NAME"].ToString();
                // 荷積業者コード
                rowDetail["NZM_GYOUSHA_CD"] = dtRow["NIZUMI_GYOUSHA_CD"].ToString();
                // 荷積業者名
                rowDetail["NZM_GYOUSHA_NAME"] = dtRow["NIZUMI_GYOUSHA_NAME"].ToString();
                // 荷積現場コード
                rowDetail["NZM_GENBA_CD"] = dtRow["NIZUMI_GENBA_CD"].ToString();
                // 荷積現場名
                rowDetail["NZM_GENBA_NAME"] = dtRow["NIZUMI_GENBA_NAME"].ToString();
                // 荷降業者コード
                rowDetail["NOS_GYOUSHA_CD"] = dtRow["NIOROSHI_GYOUSHA_CD"].ToString();
                // 荷降業者名
                rowDetail["NOS_GYOUSHA_NAME"] = dtRow["NIOROSHI_GYOUSHA_NAME"].ToString();
                // 荷降現場コード
                rowDetail["NOS_GENBA_CD"] = dtRow["NIOROSHI_GENBA_CD"].ToString();
                // 荷降現場名
                rowDetail["NOS_GENBA_NAME"] = dtRow["NIOROSHI_GENBA_NAME"].ToString();
                // 伝票種類
                rowDetail["DENPYOU_SHURUI"] = dtRow["DENSHU_KBN_NAME_RYAKU"].ToString();
                // 車種コード
                rowDetail["SHASHU_CD"] = dtRow["SHASHU_CD"].ToString();
                // 車輌コード
                rowDetail["SHARYOU_CD"] = dtRow["SHARYOU_CD"].ToString();
                // 運転者コード
                rowDetail["UNTENSHA_CD"] = dtRow["UNTENSHA_CD"].ToString();
                // 車種名
                rowDetail["SHASHU_NAME"] = dtRow["SHASHU_NAME"].ToString();
                // 車輌名
                rowDetail["SHARYOU_NAME"] = dtRow["SHARYOU_NAME"].ToString();
                // 運転者名
                rowDetail["UNTENSHA_NAME"] = dtRow["UNTENSHA_NAME"].ToString();
                // 品名コード
                rowDetail["HINMEI_CD"] = dtRow["HINMEI_CD"].ToString();
                // 品名
                rowDetail["HINMEI_NAME"] = dtRow["HINMEI_NAME"].ToString();
                // 正味(kg)
                rowDetail["SYOUMI"] = dtRow["NET_JYUURYOU"].ToString();
                // 数量
                rowDetail["SUURYOU"] = dtRow["SUURYOU"].ToString();               
                // 単位
                rowDetail["UNIT_NAME"] = dtRow["UNIT_NAME_RYAKU"].ToString();
                // 運賃金額
                rowDetail["UNCHIN"] = dtRow["TANKA"].ToString();
                // 運賃消費税
                rowDetail["TAX"] = dtRow["TAX"].ToString();
                // 運賃合計額
                rowDetail["KINGAKU"] = dtRow["KINGAKU"].ToString();
                // 正味合計
                rowDetail["SHOUMI_KEI"] = this.mReportInfoData.AsEnumerable()
                    .Where(y => y.Field<string>("UNPAN_GYOUSHA_CD") == dtRow["UNPAN_GYOUSHA_CD"].ToString())
                    .Sum(r => r.Field<decimal>("NET_JYUURYOU"));
                // 運賃金額計
                rowDetail["KINGAKU_KEI"] = this.mReportInfoData.AsEnumerable()
                    .Where(y => y.Field<string>("UNPAN_GYOUSHA_CD") == dtRow["UNPAN_GYOUSHA_CD"].ToString())
                    .Sum(r => r.Field<decimal>("TANKA"));
                // 運賃消費税
                rowDetail["SHOHIZEI_TOTAL"] = this.mReportInfoData.AsEnumerable()
                    .Where(y => y.Field<string>("UNPAN_GYOUSHA_CD") == dtRow["UNPAN_GYOUSHA_CD"].ToString())
                    .Sum(r => r.Field<decimal>("TAX"));
                // 運賃合計金額
                rowDetail["KINGAKU_TOTAL"] = this.mReportInfoData.AsEnumerable()
                    .Where(y => y.Field<string>("UNPAN_GYOUSHA_CD") == dtRow["UNPAN_GYOUSHA_CD"].ToString())
                    .Sum(r => r.Field<decimal>("KINGAKU"));

                mReportDetailInfo.Rows.Add(rowDetail);
            }
            // データを設定する処理を入れる
            this.ReportInfo.DataTableList.Add("Detail", this.mReportDetailInfo);
            #endregion - Detail -          
        }
        /// <summary>/帳票Footer情報取得</summary>
        public void CreateFooterDataTable()
        {
           
          
            #region - Footer -
            mReportFooterInfo = new DataTable();
            DataRow rowFooter;
            mReportFooterInfo.TableName = "Footer";
            // 正味計
            mReportFooterInfo.Columns.Add("SHOUMI_SOU_KEI");
            // 金額計
            mReportFooterInfo.Columns.Add("KINGAKU_SOU_KEI");
            // 消費税
            mReportFooterInfo.Columns.Add("SHOHIZEI_SOU_TOTAL");
            // 総合計
            mReportFooterInfo.Columns.Add("KINGAKU_SOU_TOTAL");

            rowFooter = mReportFooterInfo.NewRow();           
       
            // 正味計
            rowFooter["SHOUMI_SOU_KEI"] = this.mReportInfoData.AsEnumerable().Sum(r => r.Field<decimal>("NET_JYUURYOU")); 
            // 金額計
            rowFooter["KINGAKU_SOU_KEI"] = this.mReportInfoData.AsEnumerable().Sum(r => r.Field<decimal>("TANKA")); 
            // 消費税
            rowFooter["SHOHIZEI_SOU_TOTAL"] = this.mReportInfoData.AsEnumerable().Sum(r => r.Field<decimal>("TAX")); 
            // 総合計
            rowFooter["KINGAKU_SOU_TOTAL"] = this.mReportInfoData.AsEnumerable().Sum(r => r.Field<decimal>("KINGAKU"));

            mReportFooterInfo.Rows.Add(rowFooter);          

            this.ReportInfo.DataTableList.Add("Footer", mReportFooterInfo);

            #endregion - Footer -
        }
        #endregion

        /// 20141128 Houkakou 「運賃集計表」のダブルクリックを追加する　start
        #region HIDUKE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HIDUKE_FROM;
            var ToTextBox = this.form.HIDUKE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region UNPAN_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.UNPAN_GYOUSHA_CD;
            var ToTextBox = this.form.UNPAN_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.UNPAN_GYOUSHA_NAME_TO.Text = this.form.UNPAN_GYOUSHA_NAME.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIZUMI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIZUMI_GYOUSHA_CD;
            var ToTextBox = this.form.NIZUMI_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIZUMI_GYOUSHA_NAME_TO.Text = this.form.NIZUMI_GYOUSHA_NAME.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIZUMI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIZUMI_GENBA_CD;
            var ToTextBox = this.form.NIZUMI_GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIZUMI_GENBA_NAME_TO.Text = this.form.NIZUMI_GENBA_NAME.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIOROSHI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIOROSHI_GYOUSHA_CD;
            var ToTextBox = this.form.NIOROSHI_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIOROSHI_GYOUSHA_NAME_TO.Text = this.form.NIOROSHI_GYOUSHA_NAME.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIOROSHI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIOROSHI_GENBA_CD;
            var ToTextBox = this.form.NIOROSHI_GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIOROSHI_GENBA_NAME_TO.Text = this.form.NIOROSHI_GENBA_NAME.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「運賃集計表」のダブルクリックを追加する　end

        /// 20141209 teikyou 日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.form.HIDUKE_FROM.Value);
            DateTime date_to = Convert.ToDateTime(this.form.HIDUKE_TO.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                this.form.HIDUKE_TO.IsInputErrorOccured = true;
                this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "伝票日付From", "伝票日付To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.HIDUKE_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region HIDUKE_FROM_Leaveイベント
        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
            {
                this.form.HIDUKE_TO.IsInputErrorOccured = false;
                this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region HIDUKE_TO_Leaveイベント
        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
            {
                this.form.HIDUKE_FROM.IsInputErrorOccured = false;
                this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141209 teikyou 日付チェックを追加する　end
    }
}