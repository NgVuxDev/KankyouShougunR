using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.Common.TruckScaleTsuushin
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 親フォーム
        /// </summary>
        private BasePopForm parentForm;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.parentForm = (BasePopForm)this.form.Parent;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.BUTTON_SETTING_XML);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンのイベント初期化処理
        /// </summary>
        /// <returns></returns>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            this.parentForm.bt_func9.Click += this.bt_func9_Click;
            this.parentForm.bt_func12.Click += this.bt_func12_Click;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // プロセスボタンを非表示
            this.parentForm.ProcessButtonPanel.Visible = false;
            // ポップアップモード
            this.parentForm.IsPopupType = true;

            //　ボタンのテキストを初期化
            this.ButtonInit();
            // イベントの初期化処理
            this.EventInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Form_Shown()
        {
            LogUtility.DebugMethodStart();

            this.parentForm.Size = new System.Drawing.Size(650, 600);
            this.parentForm.bt_func1.Visible = false;
            this.parentForm.bt_func2.Visible = false;
            this.parentForm.bt_func3.Visible = false;
            this.parentForm.bt_func4.Visible = false;
            this.parentForm.bt_func5.Visible = false;
            this.parentForm.bt_func6.Visible = false;
            this.parentForm.bt_func7.Visible = false;
            this.parentForm.bt_func8.Visible = false;
            this.parentForm.bt_func9.Visible = true;
            this.parentForm.bt_func10.Visible = false;
            this.parentForm.bt_func11.Visible = false;
            this.parentForm.bt_func12.Visible = true;
            this.parentForm.lb_hint.Visible = false;

            var p = this.parentForm.bt_func9.Location;
            this.parentForm.bt_func9.Location = new System.Drawing.Point(400, p.Y);
            this.parentForm.bt_func12.Location = new System.Drawing.Point(500, p.Y);

            this.parentForm.MinimizeBox = false;
            this.parentForm.MaximizeBox = false;
            this.parentForm.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.LoadSettings();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F9 保存
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (msgLogic.MessageBoxShow("C044") == DialogResult.Yes)
                {
                    if (this.SaveSettings())
                    {
                        msgLogic.MessageBoxShow("I001", "設定ファイル出力");
                    }
                    else
                    {
                        MessageBox.Show("設定ファイルの保存に失敗しました",  "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F12 キャンセル
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // フォームを閉じる
                this.form.Close();
                this.parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSettings()
        {
            LogUtility.DebugMethodStart();

            //デフォルト値
            this.form.txtUse.Text = "2";
            this.form.txtWeightDisplaySwitch.Text = "1";
            this.form.txtFilePath.Text = "TruckScale1.txt";
            this.form.txtFileNoReactTime.Text = "120";
            this.form.txtFileDetectTime.Text = "1200";
            this.form.txtDetectAllowWeight.Text = "300";
            this.form.txtSTWeightCount.Text = "2";
            
            try
            {
                var configXml = XElement.Load(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
                var scaleElem = configXml.XPathSelectElement("./Settings/TScaleSettings/Scale");
                if (scaleElem != null)
                {
                    this.form.txtUse.Text = scaleElem.Attribute("Use").Value;
                    this.form.txtFilePath.Text = scaleElem.Attribute("FilePath").Value;
                    this.form.txtFileNoReactTime.Text = scaleElem.Attribute("FileNoReactTime").Value;
                    this.form.txtFileDetectTime.Text = scaleElem.Attribute("FileDetectTime").Value;
                    this.form.txtDetectAllowWeight.Text = scaleElem.Attribute("DetectAllowWeight").Value;
                    this.form.txtSTWeightCount.Text = scaleElem.Attribute("STWeightCount").Value;

                    // 追加項目
                    // この項目がないバージョンからあるバージョンにアップする時
                    // ここでエラーしてしまうため回避する
                    if (scaleElem.Attribute("WeightDisplaySwitch")==null)
                    {
                        this.form.txtWeightDisplaySwitch.Text = "1";
                    }
                    else
                    {
                        this.form.txtWeightDisplaySwitch.Text = scaleElem.Attribute("WeightDisplaySwitch").Value;
                    }


                }
            }
            catch(Exception ex)
            {
                LogUtility.Error("Scale設定読み込み失敗", ex);
                msgLogic.MessageBoxShow("E076");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        private bool SaveSettings()
        {
            bool success = false;
            LogUtility.DebugMethodStart();
            try
            {
                var configXml = XElement.Load(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
                var scaleElem = configXml.XPathSelectElement("./Settings/TScaleSettings/Scale");
                if (scaleElem == null)
                {
                    scaleElem = new XElement("Scale");
                    configXml.XPathSelectElement("./Settings").Add(new XElement("TScaleSettings", scaleElem));
                }
                scaleElem.SetAttributeValue("Use", this.form.txtUse.Text);
                scaleElem.SetAttributeValue("WeightDisplaySwitch", this.form.txtWeightDisplaySwitch.Text);
                scaleElem.SetAttributeValue("FilePath", this.form.txtFilePath.Text);
                scaleElem.SetAttributeValue("FileNoReactTime", this.form.txtFileNoReactTime.Text);
                scaleElem.SetAttributeValue("FileDetectTime", this.form.txtFileDetectTime.Text);
                scaleElem.SetAttributeValue("DetectAllowWeight", this.form.txtDetectAllowWeight.Text);
                scaleElem.SetAttributeValue("STWeightCount", this.form.txtSTWeightCount.Text);
                configXml.Save(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
                success = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Scale設定書き込み失敗", ex);
            }

            LogUtility.DebugMethodEnd();
            return success;
        }

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
    }
}
