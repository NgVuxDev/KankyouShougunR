using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using r_framework.APP.Base;
using System.IO;
using r_framework.Const;
using r_framework.Logic;

namespace Shougun.Core.Common.TruckScaleWeight
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        public UIForm()
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        private void btnSetWeight_Click(object sender, EventArgs e)
        {
            var configXml = XElement.Load(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
            var scaleElem = configXml.XPathSelectElement("./Settings/TScaleSettings/Scale");
            var scaleFilePath = scaleElem.Attribute("FilePath").Value;
            File.WriteAllText(scaleFilePath, customTextBox1.Text);
        }

        private void btnProcessWeight_Click(object sender, EventArgs e)
        {
            truckScaleWeight1.ProcessWeight();
        }

        private void UIForm_Load(object sender, EventArgs e)
        {
            this.logic = new LogicClass(this);
            this.logic.WindowInit();
        }
    }
}
