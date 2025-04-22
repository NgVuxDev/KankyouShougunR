using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Windows.Forms;
using System.ComponentModel.Design;
using r_framework.CustomControl;
using r_framework.APP.Base;
using r_framework.Utility;

namespace Shougun.Core.Common.TruckScaleWeight
{
    public partial class TruckScaleWeight : Component
    {
        private bool scaleUse = false;
        private bool scaletxtWeightDisplaySwitch = false;
        private string scaleFilePath = string.Empty;
        private int scaleFileNoReactTime = 0;
        private int scaleFileDetectTime = 100;
        private int scaleDetectAllowWeight = 0;
        private int scaleSTWeightCount = 0;
        private int? lastWeight = null;

        private int cntStable = 1;
        private Label lbl_Weight;
        private CustomButton btn_func1;
        private CustomButton bt_process;

        //同じエラーを繰り返し検出した場合のログ制御用フラグ
        private bool detectIoError = false;
        private bool detectAuthError = false;

        [Category("重量値関連画面設定")]
        public ContainerControl ContainerControl { get; set; }

        public override ISite Site
        {
            get { return base.Site; }
            set
            {
                base.Site = value;
                if (value == null)
                {
                    return;
                }
                IDesignerHost host = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host == null)
                {
                    return;
                }
                IComponent componentHost = host.RootComponent;
                if (componentHost is ContainerControl)
                {
                    ContainerControl = componentHost as ContainerControl;
                }
            }
        }

        [Category("重量値関連画面設定")]
        [Description("ヘッダフォームの重量値コントロール名を設定してください。")]
        public string WeightControl { get; set; }

        [Category("重量値関連画面設定")]
        [Description("F1重量取り込みコントロール名です。")]
        [DefaultValue("bt_func1")]
        public string Func1Control { get; set; }

        [Category("重量値関連画面設定")]
        [Description("処理手入力コントロール名です。")]
        [DefaultValue("bt_process3")]
        public string ProcessControl { get; set; }

        [Category("重量値関連画面設定")]
        [Description("重量値が不安定とみなされた時の色です。")]
        [DefaultValue(typeof(Color), "Aqua")]
        public Color UnstableColor { get; set; }

        [Category("重量値関連画面設定")]
        [Description("重量値が安定とみなされた時の色です。")]
        [DefaultValue(typeof(Color), "Orange")]
        public Color StableColor { get; set; }

        private void InitControls()
        {
            this.Func1Control = "bt_func1";
            this.ProcessControl = "bt_process3";
            this.UnstableColor = Color.Aqua;
            this.StableColor = Color.Orange;
        }

        public TruckScaleWeight()
        {
            InitializeComponent();
            InitControls();
        }

        public TruckScaleWeight(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            InitControls();
        }

        /// <summary>
        /// 自動手動重量表示の値を読み込んで返す
        /// </summary>
        /// <returns></returns>
        public bool WeightDisplaySwitch()
        {
            bool ret = false;

            var configXml = XElement.Load(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
            XElement scaleElem = null;
            if (configXml != null)
            {
                scaleElem = configXml.XPathSelectElement("./Settings/TScaleSettings/Scale");
            }

            if (scaleElem != null)
            {
                if (scaleElem.Attribute("WeightDisplaySwitch") == null)
                {
                    ret = true;
                }
                else
                {
                    ret = scaleElem.Attribute("WeightDisplaySwitch").Value == "1";
                }

            }

            return ret;
        }
        /// <summary>
        /// 重量値読込み
        /// </summary>
        public void ProcessWeight()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var configXml = XElement.Load(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
                XElement scaleElem = null;
                if (configXml != null)
                {
                    scaleElem = configXml.XPathSelectElement("./Settings/TScaleSettings/Scale");
                }

                if (scaleElem != null)
                {
                    this.scaleUse = scaleElem.Attribute("Use").Value == "1";
                    this.scaleFilePath = scaleElem.Attribute("FilePath").Value;
                    this.scaleFileNoReactTime = int.Parse(scaleElem.Attribute("FileNoReactTime").Value);

                    decimal fileDetectTime = 0;
                    if (decimal.TryParse(scaleElem.Attribute("FileDetectTime").Value, out fileDetectTime))
                    {
                        this.scaleFileDetectTime = (int)fileDetectTime;
                    }

                    decimal detectAllowWeight = 0;
                    if (decimal.TryParse(scaleElem.Attribute("DetectAllowWeight").Value, out detectAllowWeight))
                    {
                        this.scaleDetectAllowWeight = (int)detectAllowWeight;
                    }
                    this.scaleSTWeightCount = int.Parse(scaleElem.Attribute("STWeightCount").Value);
                    
                    // 追加項目
                    // この項目がないバージョンから、あるバージョンにアップする時
                    // (○○).valueを読み込むとエラーしてしまうため回避する
                    if (scaleElem.Attribute("WeightDisplaySwitch") == null)
                    {
                        this.scaletxtWeightDisplaySwitch = true;
                    }
                    else
                    {
                        this.scaletxtWeightDisplaySwitch = scaleElem.Attribute("WeightDisplaySwitch").Value == "1";
                    }
                }

                this.timerProcess.Interval = this.scaleFileDetectTime;
                this.lastWeight = null;
                this.cntStable = 1;

                var fmParent = this.ContainerControl.Parent as BusinessBaseForm;
                this.lbl_Weight = fmParent.headerForm.Controls[this.WeightControl] as Label;
                this.btn_func1 = fmParent.pn_foot.Controls[this.Func1Control] as CustomButton;
                this.bt_process = fmParent.ProcessButtonPanel.Controls[this.ProcessControl] as CustomButton;

                var use = this.scaleUse && File.Exists(this.scaleFilePath);

                this.lbl_Weight.Visible = use;
                this.btn_func1.Enabled = use;
                this.bt_process.Enabled = use;

                if (use)
                {
                    if (scaletxtWeightDisplaySwitch)
                    {
                        this.lbl_Weight.Visible = true;
                        this.timerProcess.Enabled = true;
                    }
                    else
                    {
                        this.lbl_Weight.Visible = false;
                        this.timerProcess.Enabled = false;
                    }
                    timerProcess_Tick(null, null);
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
        /// 重量値表示タイマー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerProcess_Tick(object sender, EventArgs e)
        {
            try
            {
                // 現在時刻 - ファイルの最終更新時間 < FileNoReactTimeの場合
                var tm = File.GetLastWriteTime(this.scaleFilePath);
                var ts = DateTime.Now - tm;
                if (ts.TotalSeconds < this.scaleFileNoReactTime)
                {
                    string weightStr;

                    // 読み込みモードで開く
                    using (var fs = new FileStream(this.scaleFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var sr = new StreamReader(fs))
                        {
                            weightStr = sr.ReadLine();
                        }
                    }
                    int weightNum;
                    // 取得した内容が数値、かつ >= DetectAllowWeightの場合
                    if (int.TryParse(weightStr, out weightNum))
                    {
                        // 取得した内容を重量テキストに入力（カンマ編集）
                        this.lbl_Weight.Text = string.Format("{0:N0}", weightNum);

                        // 取得した内容を前回取得した内容と比較して、
                        // 同じではなかった場合、カウンターを１でクリアする
                        // 同じだった場合、カウンターをインクリメントする
                        this.cntStable = weightNum == this.lastWeight ? this.cntStable + 1 : 1;

                        // カウンターをSTWeightCountと比較して、
                        // カウンターがSTWeightCountに達していない場合、不安定とみなす
                        // カウンターがSTWeightCountに達した場合、安定とみなす
                        var stable = this.cntStable >= this.scaleSTWeightCount;

                        // 安定とみなしたときの色：水色
                        // 不安定とみなしたときの色：オレンジ系の色
                        this.lbl_Weight.ForeColor = stable ? StableColor : UnstableColor;

                        this.lastWeight = weightNum;
                    }
                }

                //検出フラグはここまで来たら毎回リセット
                detectAuthError = false;
                detectIoError = false;

            }
            catch (UnauthorizedAccessException ex)
            {
                if (!detectAuthError)
                {
                    //クラウド利用時など、サーバー側でアプリは生きていてファイルリンク・ファイルマウントが切れる場合があるため、エラーは通知しない
                    LogUtility.Error("UnauthorizedAccessError", ex);

                    //連続検出を除外するため検出フラグを設定
                    //同種のExceptionで別のエラーの場合は改めて考える必要あり。とりあえず今はここまで。
                    detectAuthError = true;
                }
            }
            catch (IOException ex)
            {
                if (!detectIoError)
                {
                    //クラウド利用時など、サーバー側でアプリは生きていてファイルリンク・ファイルマウントが切れる場合があるため、エラーは通知しない
                    LogUtility.Error("IOError", ex);

                    //連続検出を除外するため検出フラグを設定
                    //同種のExceptionで別のエラーの場合は改めて考える必要あり。とりあえず今はここまで。
                    detectIoError = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                this.timerProcess.Enabled = false;
                throw;
            }
        }
    }
}
