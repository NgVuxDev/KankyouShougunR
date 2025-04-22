using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using r_framework.Utility;
using r_framework.Logic;

/// <summary>
/// 画像、PDF表示ポップアップ画面
/// </summary>
namespace ItemViewPopup.App
{
    /// <summary>
    /// 画像、PDF表示ポップアップ画面
    /// </summary>
    public partial class Pdf_or_ImageViewForm : Form, IDisposable
    {
        #region 外部による設定
        private string filePath = "";
        public string pathSetting
        {
            set
            {
                filePath = value.ToString();
                this.txtFilePath.Text = filePath;
            }
        }
        #endregion

        public Pdf_or_ImageViewForm()
        {
            InitializeComponent();

            setExEventHandler();
        }
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #region 起動処理
        private void Pdf_or_ImageViewForm_Load(object sender, EventArgs e)
        {
            setDispSetting();
        }

        private bool setDispSetting()
        {
            try
            {
                setDispFormat();

                if (filePath != "")
                {
                    //拡張子をある程度判断してPDF表示かImage表示か切替
                    string stExtension = System.IO.Path.GetExtension(filePath);

                    switch (stExtension.ToLower())
                    {
                        case ".pdf":
                            setPDFViewModeSettings();

                            break;
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                        case ".bmp":
                            setImageViewModeSettings();

                            break;
                        default:
                            break;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDispSetting", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        #endregion

        #region コントロールの表示操作

        private void setDispFormat()
        {
            this.pctBox.Visible = false;
            this.axAcroPDF.Visible = false;

            this.bt_FirstPage.Visible = false;
            this.bt_PreviousPage.Visible = false;
            this.bt_NextPage.Visible = false;
            this.bt_LastPage.Visible = false;
            this.bt_SetScope.Visible = false;
            this.txtScopeRate.Visible = false;
            this.lblScopeRatePercent.Visible = false;
        }

        private void setPDFViewModeSettings()
        {
            //AxAcroPDFに設定可能なモード　参照：2014/03/15時点で有効：ttp://pdf-file.nnn2.com/?p=240
            //this.axAcroPDF.setPageMode("none");
            //this.axAcroPDF.setPageMode("bookmarks");
            //this.axAcroPDF.setPageMode("thumbs");

            //this.axAcroPDF.setLayoutMode("dontcare");
            //this.axAcroPDF.setLayoutMode("singlepage");
            //this.axAcroPDF.setLayoutMode("onecolumn");
            //this.axAcroPDF.setLayoutMode("twocolumnleft");
            //this.axAcroPDF.setLayoutMode("twocolumnright");

            this.axAcroPDF.src = filePath;
            this.axAcroPDF.Visible = true;
            this.axAcroPDF.Enabled = false;
            this.axAcroPDF.setShowScrollbars(false);

            //CreatePdfViewControl();
            //this.pnlPdfView.Visible = true;
            //this.pnlPdfView.Enabled = false;

            //ボタン類の開放
            this.bt_FirstPage.Visible = true;
            this.bt_PreviousPage.Visible = true;
            this.bt_NextPage.Visible = true;
            this.bt_LastPage.Visible = true;
            this.bt_SetScope.Visible = true;
            this.txtScopeRate.Visible = true;
            this.lblScopeRatePercent.Visible = true;

            //倍率の設定
            this.txtScopeRate.Text = "";
        }

        private void setImageViewModeSettings()
        {
            this.pctBox.ImageLocation = filePath;
            this.pctBox.Visible = true;
        }

        #endregion

        #region Event拡張

        private void setExEventHandler()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            this.txtScopeRate.KeyDown += new KeyEventHandler(this.txtScopeRate_KeyDown);
            this.txtScopeRate.Enter += new EventHandler(this.txtScopeRate_Enter);
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    bt_SetScope_Click(sender, e);
                    break;
                case Keys.F5:
                    bt_FirstPage_Click(this.bt_FirstPage, e);
                    break;
                case Keys.F6:
                    bt_PreviousPage_Click(this.bt_PreviousPage, e);
                    break;
                case Keys.F7:
                    bt_NextPage_Click(this.bt_NextPage, e);
                    break;
                case Keys.F8:
                    bt_LastPage_Click(this.bt_LastPage, e);
                    break;

                case Keys.F12:
                    bt_Close_Click(sender, e);
                    break;

                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.D0:
                case Keys.Delete:
                case Keys.Back:
                case Keys.Tab:
                case Keys.Shift:
                case Keys.Enter:
                    break;

                default:
                    e.Handled = true;
                    break;
            }
        }

        private void txtScopeRate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    bool catchErr = setScopeRate();
                    if (catchErr)
                    {
                        return;
                    }
                    this.txtScopeRate.SelectAll();
                    break;

                default:
                    break;
            }
        }

        private void txtScopeRate_Enter(object sender, EventArgs e)
        {
            this.txtScopeRate.SelectAll();
        }

        #endregion

        #region 表示倍率の変更

        private void bt_SetScope_Click(object sender, EventArgs e)
        {
            setScopeRate();
        }

        private bool setScopeRate()
        {
            try
            {
                //カーソル移動
                this.txtScopeRate.Select();

                //倍率取得
                float scopeRate;
                if (float.TryParse(this.txtScopeRate.Text, out scopeRate) == false)
                {
                    return false;
                }

                //倍率設定
                this.axAcroPDF.setZoom(scopeRate);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setScopeRate", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #endregion

        #region 先頭ページ、前ページ、次ページ、最終ページ押下時の動作

        private void bt_FirstPage_Click(object sender, EventArgs e)
        {
            pageAction(sender);
        }
        private void bt_LastPage_Click(object sender, EventArgs e)
        {
            pageAction(sender);
        }
        private void bt_PreviousPage_Click(object sender, EventArgs e)
        {
            pageAction(sender);
        }
        private void bt_NextPage_Click(object sender, EventArgs e)
        {
            pageAction(sender);
        }
        private bool pageAction(object sender)
        {
            try
            {
                r_framework.CustomControl.CustomButton btn = sender as r_framework.CustomControl.CustomButton;

                if (btn == null)
                {
                    return false;
                }

                switch (btn.Name)
                {
                    case "bt_FirstPage":
                        //最初の頁へ
                        this.axAcroPDF.gotoFirstPage();

                        break;

                    case "bt_LastPage":
                        //最後の頁へ
                        this.axAcroPDF.gotoLastPage();

                        break;

                    case "bt_PreviousPage":
                        //前の頁へ
                        this.axAcroPDF.gotoPreviousPage();

                        break;

                    case "bt_NextPage":
                        //次の頁へ
                        this.axAcroPDF.gotoNextPage();

                        break;

                    default:
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("pageAction", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #endregion

        #region 終了処理

        private void bt_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Dispose()
        {
        }

        #endregion



    }
}
