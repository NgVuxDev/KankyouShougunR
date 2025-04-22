using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;

namespace Shougun.Core.Common.TruckScaleWeight
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
        private BusinessBaseForm parentForm;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.parentForm = (BusinessBaseForm)this.form.Parent;

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

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            //　ボタンのテキストを初期化
            this.ButtonInit();
            // イベントの初期化処理
            this.EventInit();

            LogUtility.DebugMethodEnd();
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
