// $Id: LogicCls.cs 36232 2014-12-01 07:19:02Z nagata $
using System;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Master.TabOrderSettei.APP;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.TabOrderSettei.Logic
{
    /// <summary>
    /// 入力項目設定画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.TabOrderSettei.Setting.ButtonSetting.xml";

        /// <summary>
        /// コンテナ種類画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public MasterBaseForm parentForm;

        private StringCollection UkeireDenpyouCtrl = null;
        private StringCollection UkeireDetailCtrl = null;
        
        private StringCollection SyukkaDenpyouCtrl = null;
        private StringCollection SyukkaDetailCtrl = null;
        
        private StringCollection UriageDenpyouCtrl = null;
        private StringCollection UriageDetailCtrl = null;
        
        private StringCollection KeiryouDenpyouCtrl = null;
        private StringCollection KeiryouDetailCtrl = null;

        private IM_SYS_INFODao sysInfoDao;
        private M_SYS_INFO sysInfoEntity;

        #endregion

        #region プロパティ

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

            LogUtility.DebugMethodEnd(targetForm);
        }
        # endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 処理No（ESC)を入力不可にする
                this.parentForm.txb_process.Enabled = false;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M590", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }

                // システム情報を取得し、初期値をセットする
                this.GetSysInfoInit();
                // Ver 2.0では計量入力は未リリースのため非表示
                this.form.JOHOU.TabPages.RemoveAt(this.form.JOHOU.Controls[this.form.tab_GENBA_ICHIRAN.Name].TabIndex);

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (MasterBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
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

        #region ボタン設定の読込
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
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (MasterBaseForm)this.form.Parent;

                //登録ボタン(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                //受入出荷画面サイズ選択取得
                HearerSysInfoInit();

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

        #region システム情報を取得し、初期値をセットする
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 設定データ取得
                GetStatus();

                // 表示リストに格納

            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 参照モード表示

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            foreach (Control ctr in this.form.JOHOU.Controls)
            {
                this.SetReference(ctr);
            }

            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func9.Enabled = false;
        }

        /// <summary>
        /// 画面の各コントロールを参照モード用に設定します
        /// </summary>
        /// <param name="control"></param>
        private void SetReference(Control control)
        {
            if (control is System.Windows.Forms.Panel ||
                control is System.Windows.Forms.TabControl ||
                control is System.Windows.Forms.GroupBox)
            {
                // 再帰
                foreach (Control ctr in control.Controls)
                {
                    this.SetReference(ctr);
                }
            }
            else
            {
                // ラベルは設定しない
                if (control is System.Windows.Forms.Label) return;

                // ReadOnly優先で設定
                PropertyInfo ctl_property_readonly = control.GetType().GetProperty("ReadOnly");
                PropertyInfo ctl_property_enabled = control.GetType().GetProperty("Enabled");
                if (ctl_property_readonly != null)
                {
                    ctl_property_readonly.SetValue(control, true, null);
                }
                else if (ctl_property_enabled != null)
                {
                    ctl_property_enabled.SetValue(control, false, null);
                }
            }
        }

        #endregion

        #endregion

        #region 業務処理

        #region 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 論理削除処理
        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I001", "削除");
            }
            catch (Exception ex)
            {

                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 物理削除処理
        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region データ取得処理
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                int count = 0;
                return count;
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
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual bool RegistData(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            //独自チェックの記述例を書く
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    SetStatus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 取消処理
        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Cancel", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void HearerSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }

        /// <summary>
        /// ステータス取得
        /// </summary>
        public void GetStatus()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //受入出荷画面サイズ選択によって受入入力と出荷入力内容は違います
                if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                {
                    // 受入入力
                    this.UkeireDenpyouCtrl = this.getStatus(this.form.Ukeire_Denpyou,
                        Shougun.Core.SalesPayment.UkeireNyuuryoku.Properties.Settings.Default.ApDenpyouCtrl,
                        Shougun.Core.SalesPayment.UkeireNyuuryoku.Properties.Settings.Default.DenpyouCtrl);

                    this.UkeireDetailCtrl = this.getStatus(this.form.Ukeire_Syousai,
                        Shougun.Core.SalesPayment.UkeireNyuuryoku.Properties.Settings.Default.ApDetailCtrl,
                        Shougun.Core.SalesPayment.UkeireNyuuryoku.Properties.Settings.Default.DetailCtrl);

                    // 出荷入力
                    this.SyukkaDenpyouCtrl = this.getStatus(this.form.Shukka_Denpyou,
                        Shougun.Core.SalesPayment.SyukkaNyuuryoku.Properties.Settings.Default.ApDenpyouCtrl,
                        Shougun.Core.SalesPayment.SyukkaNyuuryoku.Properties.Settings.Default.DenpyouCtrl);

                    this.SyukkaDetailCtrl = this.getStatus(this.form.Shukka_Syousai,
                        Shougun.Core.SalesPayment.SyukkaNyuuryoku.Properties.Settings.Default.ApDetailCtrl,
                        Shougun.Core.SalesPayment.SyukkaNyuuryoku.Properties.Settings.Default.DetailCtrl);
                }
                else
                {
                    // 受入入力
                    this.UkeireDenpyouCtrl = this.getStatus(this.form.Ukeire_Denpyou,
                        Shougun.Core.SalesPayment.UkeireNyuuryoku2.Properties.Settings.Default.ApDenpyouCtrl,
                        Shougun.Core.SalesPayment.UkeireNyuuryoku2.Properties.Settings.Default.DenpyouCtrl);

                    this.UkeireDetailCtrl = this.getStatus(this.form.Ukeire_Syousai,
                        Shougun.Core.SalesPayment.UkeireNyuuryoku2.Properties.Settings.Default.ApDetailCtrl,
                        Shougun.Core.SalesPayment.UkeireNyuuryoku2.Properties.Settings.Default.DetailCtrl);

                    // 出荷入力
                    this.SyukkaDenpyouCtrl = this.getStatus(this.form.Shukka_Denpyou,
                        Shougun.Core.SalesPayment.SyukkaNyuuryoku2.Properties.Settings.Default.ApDenpyouCtrl,
                        Shougun.Core.SalesPayment.SyukkaNyuuryoku2.Properties.Settings.Default.DenpyouCtrl);

                    this.SyukkaDetailCtrl = this.getStatus(this.form.Shukka_Syousai,
                        Shougun.Core.SalesPayment.SyukkaNyuuryoku2.Properties.Settings.Default.ApDetailCtrl,
                        Shougun.Core.SalesPayment.SyukkaNyuuryoku2.Properties.Settings.Default.DetailCtrl);
                }

                // 売上／支払入力
                this.UriageDenpyouCtrl = this.getStatus(this.form.Uriage_Denpyou,
                    Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Properties.Settings.Default.ApDenpyouCtrl,
                    Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Properties.Settings.Default.DenpyouCtrl);

                this.UriageDetailCtrl = this.getStatus(this.form.Uriage_Syousai,
                    Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Properties.Settings.Default.ApDetailCtrl,
                    Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Properties.Settings.Default.DetailCtrl);

                // 計量入力
                this.KeiryouDenpyouCtrl = this.getStatus(this.form.Keiryou_Denpyou,
                    Shougun.Core.Scale.Keiryou.Properties.Settings.Default.ApDenpyouCtrl,
                    Shougun.Core.Scale.Keiryou.Properties.Settings.Default.DenpyouCtrl);

                this.KeiryouDetailCtrl = this.getStatus(this.form.Keiryou_Syousai,
                    Shougun.Core.Scale.Keiryou.Properties.Settings.Default.ApDetailCtrl,
                    Shougun.Core.Scale.Keiryou.Properties.Settings.Default.DetailCtrl);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetStatus", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 設定の読み込みとMultiRowコントロールへの設定を行う
        /// </summary>
        /// <param name="multiRow">設定対象のMultiRowコントロール</param>
        /// <param name="appSettings">APPスコープのSettings</param>
        /// <param name="userSettings">USERスコープのSettings</param>
        /// <returns>APPスコープの定義とUSERスコープの値を合成したリスト</returns>
        private StringCollection getStatus(
                    GcCustomMultiRow multiRow,
                    StringCollection appSettings,
                    StringCollection userSettings)
        {
            // まずAPPスコープから各行を4要素に分解した配列を作成する
            // こんなのがたくさんある <string>UNPAN_GYOUSHA_NAME:運搬業者名:True:28</string>
            // （VerUpでAPPスコープに定義項目が増えているかもしれない)
            var appTable = new List<string[]>();
            if (appSettings != null)
            {
                foreach (string appSetting in appSettings)
                {
                    appTable.Add(appSetting.Split(':'));
                }
            }

            // USERスコープから同じコントロール名の行を探しTabStopの値だけ参照する
            if (userSettings != null)
            {
                foreach (string userSetting in userSettings)
                {
                    var userRow = userSetting.Split(':');
                    foreach (string[] appRow in appTable)
                    {
                        if (appRow[0].Equals(userRow[0]))
                        {
                            appRow[2] = userRow[2];
                            break;
                        }
                    }
                }
            }

            // MultiRowコントロールの行を設定
            // 同時に返却リストも作成
            multiRow.Rows.Clear();
            var result = new StringCollection();
            foreach (string[] appRow in appTable)
            {
                int index = multiRow.Rows.Add();
                var cells = multiRow.Rows[index].Cells;
                cells["DENPYOU_NUMBER"].Value = (index + 1).ToString();
                cells["DENPYOU_KOUMOKU"].Value = appRow[1];
                cells["DENPYOU_SENNI"].Value = bool.Parse(appRow[2]);

                result.Add(string.Format("{0}:{1}:{2}:{3}", appRow[0], appRow[1], appRow[2], appRow[3]));
            }
            return result;
        }

        /// <summary>
        /// /// ステータス保存
        /// </summary>
        public void SetStatus()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 受入入力
                if (this.sysInfoEntity != null && this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE.ToString() == "1")
                {
                    Shougun.Core.SalesPayment.UkeireNyuuryoku2.Properties.Settings.Default.DenpyouCtrl
                    = this.setStatus(this.form.Ukeire_Denpyou, this.UkeireDenpyouCtrl);

                    Shougun.Core.SalesPayment.UkeireNyuuryoku2.Properties.Settings.Default.DetailCtrl
                        = this.setStatus(this.form.Ukeire_Syousai, this.UkeireDetailCtrl);
                    Shougun.Core.SalesPayment.UkeireNyuuryoku2.Properties.Settings.Default.Save(); 
                }
                else if (this.sysInfoEntity != null && this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE.ToString() == "2")
                {
                    Shougun.Core.SalesPayment.UkeireNyuuryoku.Properties.Settings.Default.DenpyouCtrl
                    = this.setStatus(this.form.Ukeire_Denpyou, this.UkeireDenpyouCtrl);

                    Shougun.Core.SalesPayment.UkeireNyuuryoku.Properties.Settings.Default.DetailCtrl
                        = this.setStatus(this.form.Ukeire_Syousai, this.UkeireDetailCtrl);
                    Shougun.Core.SalesPayment.UkeireNyuuryoku.Properties.Settings.Default.Save(); 
                }

                // 出荷入力
                if (this.sysInfoEntity != null && this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE.ToString() == "1")
                {
                    Shougun.Core.SalesPayment.SyukkaNyuuryoku2.Properties.Settings.Default.DenpyouCtrl
                    = this.setStatus(this.form.Shukka_Denpyou, this.SyukkaDenpyouCtrl);

                    Shougun.Core.SalesPayment.SyukkaNyuuryoku2.Properties.Settings.Default.DetailCtrl
                        = this.setStatus(this.form.Shukka_Syousai, this.SyukkaDetailCtrl);
                    Shougun.Core.SalesPayment.SyukkaNyuuryoku2.Properties.Settings.Default.Save();
                }
                else if (this.sysInfoEntity != null && this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE.ToString() == "2")
                {
                    Shougun.Core.SalesPayment.SyukkaNyuuryoku.Properties.Settings.Default.DenpyouCtrl
                    = this.setStatus(this.form.Shukka_Denpyou, this.SyukkaDenpyouCtrl);

                    Shougun.Core.SalesPayment.SyukkaNyuuryoku.Properties.Settings.Default.DetailCtrl
                        = this.setStatus(this.form.Shukka_Syousai, this.SyukkaDetailCtrl);
                    Shougun.Core.SalesPayment.SyukkaNyuuryoku.Properties.Settings.Default.Save();
                }

                // 売上／支払入力
                Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Properties.Settings.Default.DenpyouCtrl
                    = this.setStatus(this.form.Uriage_Denpyou, this.UriageDenpyouCtrl);

                Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Properties.Settings.Default.DetailCtrl
                    = this.setStatus(this.form.Uriage_Syousai, this.UriageDetailCtrl);

                Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Properties.Settings.Default.Save();

                // 計量入力
                Shougun.Core.Scale.Keiryou.Properties.Settings.Default.DenpyouCtrl
                    = this.setStatus(this.form.Keiryou_Denpyou, this.KeiryouDenpyouCtrl);

                Shougun.Core.Scale.Keiryou.Properties.Settings.Default.DetailCtrl
                    = this.setStatus(this.form.Keiryou_Syousai, this.KeiryouDetailCtrl);

                Shougun.Core.Scale.Keiryou.Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetStatus", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// MultiRowコントロールから値を取得しuser.config用の保存データを作成
        /// </summary>
        /// <param name="multiRow"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private StringCollection setStatus(
                    GcCustomMultiRow multiRow,
                    StringCollection settings)
        {
            var userSettings = new StringCollection();

            for (int i = 0; i < settings.Count; i++)
            {
                var row = settings[i].Split(':');
                var tabStop = multiRow.Rows[i].Cells["DENPYOU_SENNI"].Value.ToString();
                settings[i] = string.Format("{0}:{1}:{2}:{3}", row[0], row[1], tabStop, row[3]);
                userSettings.Add(settings[i]);
            }
            return userSettings;
        }

        public virtual void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
