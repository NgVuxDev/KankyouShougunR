using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.BankIkkatsu
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// 銀行DTO
        /// </summary>
        private DTO_Bank dtoBank = new DTO_Bank();
        /// <summary>
        /// 取引先DTO
        /// </summary>
        private DTO_Torihikisaki dtoTorihikisaki = new DTO_Torihikisaki();

        /// <summary>
        /// 取引先請求DAO
        /// </summary>
        private DAO_M_TORIHIKISAKI_SEIKYUU daoM_TORIHIKISAKI_SEIKYUU = DaoInitUtility.GetComponent<DAO_M_TORIHIKISAKI_SEIKYUU>();
        /// <summary>
        /// 取引先DAO
        /// </summary>
        private DAO_M_TORIHIKISAKI daoM_TORIHIKISAKI = DaoInitUtility.GetComponent<DAO_M_TORIHIKISAKI>();
        /// <summary>
        /// 取引先DAO
        /// </summary>
        private DAO_M_BANK_SHITEN daoM_BANK_SHITEN = DaoInitUtility.GetComponent<DAO_M_BANK_SHITEN>();

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 親フォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 検索結果DataTable
        /// </summary>
        private DataTable searchResult = new DataTable();

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// DataGridView
        /// </summary>
        private DataGridView dgv;

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

            this.parentForm.bt_func1.Click += this.bt_func1_Click;
            this.parentForm.bt_func8.Click += this.bt_func8_Click;
            this.parentForm.bt_func9.Click += this.bt_func9_Click;
            this.parentForm.bt_func11.Click += this.bt_func11_Click;
            this.parentForm.bt_func12.Click += this.bt_func12_Click;

            this.parentForm.txb_process.Enabled = false;

            LogUtility.DebugMethodEnd();
        }

        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            //　ボタンのテキストを初期化
            this.ButtonInit();
            // イベントの初期化処理
            this.EventInit();

            this.dgv = this.form.dgvBank;
            this.dgv.AutoGenerateColumns = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F1 一括設定
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.SetAll();
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
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.SearchAndDisplay();
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
        /// F9 登録
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.Regist(false);
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
        /// F11 取消
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.form.BANK_CD.Text = "";
                this.form.BANK_NAME.Text = "";
                this.form.BANK_SHITEN_CD.Text = "";
                this.form.BANK_SHITEN_NAME.Text = "";
                this.form.BANK_SHITEN_CD_OLD.Text = "";
                this.form.KOUZA_SHURUI.Text = "";
                this.form.KOUZA_NO.Text = "";
                this.form.KOUZA_NAME.Text = "";
                this.form.BANK_CD_AFTER.Text = "";
                this.form.BANK_NAME_AFTER.Text = "";
                this.form.BANK_SHITEN_CD_AFTER.Text = "";
                this.form.BANK_SHITEN_NAME_AFTER.Text = "";
                this.form.BANK_SHITEN_CD_AFTER_OLD.Text = "";
                this.form.KOUZA_SHURUI_AFTER.Text = "";
                this.form.KOUZA_NO_AFTER.Text = "";
                this.form.KOUZA_NAME_AFTER.Text = "";
                this.form.BANK_SHITEN_CD_GRID.Text = "";
                this.form.KOUZA_SHURUI_GRID.Text = "";
                this.form.KOUZA_NO_GRID.Text = "";
                this.form.KOUZA_NAME_GRID.Text = "";

                this.searchResult.Rows.Clear();

                this.form.BANK_CD.Focus();
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
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.dgv.CurrentCell = null;
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

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            this.dtoBank.BankCd = this.form.BANK_CD.Text;
            this.dtoBank.BankShitenCd = this.form.BANK_SHITEN_CD.Text;
            this.dtoBank.KouzaShurui = this.form.KOUZA_SHURUI.Text;
            this.dtoBank.KouzaNo = this.form.KOUZA_NO.Text;

            this.searchResult = this.daoM_TORIHIKISAKI_SEIKYUU.GetDataForEntity(this.dtoBank);
            int cnt = this.searchResult.Rows.Count;
            if (cnt == 0)
            {
                this.msgLogic.MessageBoxShow("C001");
            }

            LogUtility.DebugMethodEnd(cnt);

            return cnt;
        }

        /// <summary>
        /// 検索結果を表示
        /// </summary>
        public void SearchAndDisplay()
        {
            LogUtility.DebugMethodStart();

            this.Search();
            this.dgv.DataSource = this.searchResult;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一括設定
        /// </summary>
        public void SetAll()
        {
            LogUtility.DebugMethodStart();

            var bankCd = this.form.BANK_CD_AFTER.Text;
            var bankShitenCd = this.form.BANK_SHITEN_CD_AFTER.Text;
            if (bankCd == "")
            {
                this.form.BANK_CD_AFTER.Focus();
                this.msgLogic.MessageBoxShow("E012", this.form.labelBANK_AFTER.Text);
            }
            else if (bankShitenCd == "")
            {
                this.form.BANK_SHITEN_CD_AFTER.Focus();
                this.msgLogic.MessageBoxShow("E012", this.form.labelBANK_SHITEN_AFTER.Text);
            }
            else
            {
                var bankName = this.form.BANK_NAME_AFTER.Text;
                var bankShitenName = this.form.BANK_SHITEN_NAME_AFTER.Text;
                var kouzaShurui = this.form.KOUZA_SHURUI_AFTER.Text;
                var kouzaNo = this.form.KOUZA_NO_AFTER.Text;
                var kouzaName = this.form.KOUZA_NAME_AFTER.Text;
                foreach (var dr in this.searchResult.AsEnumerable())
                {
                    dr.SetField<string>("FURIKOMI_BANK_CD_AFTER", bankCd);
                    dr.SetField<string>("BANK_NAME_AFTER", bankName);
                    dr.SetField<string>("FURIKOMI_BANK_SHITEN_CD_AFTER", bankShitenCd);
                    dr.SetField<string>("BANK_SHITEN_NAME_AFTER", bankShitenName);
                    dr.SetField<string>("KOUZA_SHURUI_AFTER", kouzaShurui);
                    dr.SetField<string>("KOUZA_NO_AFTER", kouzaNo);
                    dr.SetField<string>("KOUZA_NAME_AFTER", kouzaName);
                }
                this.msgLogic.MessageBoxShow("I010");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            if (this.searchResult.Rows.Count == 0)
            {
                this.msgLogic.MessageBoxShow("E061");
            }
            else
            {
                bool ok = true;
                foreach (DataGridViewRow row in this.dgv.Rows)
                {
                    var cellBankCd = row.Cells["FURIKOMI_BANK_CD_AFTERColumn"];
                    if (cellBankCd.Value.ToString() == "")
                    {
                        this.dgv.CurrentCell = cellBankCd;
                        this.dgv.BeginEdit(true);
                        this.msgLogic.MessageBoxShow("E001", this.form.labelBANK_AFTER.Text);
                        ok = false;
                        break;
                    }
                    var cellBankShitenCd = row.Cells["FURIKOMI_BANK_SHITEN_CD_AFTERColumn"];
                    if (cellBankShitenCd.Value.ToString() == "")
                    {
                        this.dgv.CurrentCell = cellBankShitenCd;
                        this.dgv.BeginEdit(true);
                        this.msgLogic.MessageBoxShow("E001", this.form.labelBANK_SHITEN_AFTER.Text);
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    //更新時間、更新者、更新PCを設定
                    new DataBinderLogic<DTO_Torihikisaki>(this.dtoTorihikisaki).SetSystemProperty(this.dtoTorihikisaki, false);

                    try
                    {
                        using (var tran = new Transaction())
                        {
                            foreach (var dr in this.searchResult.AsEnumerable())
                            {
                                this.dtoTorihikisaki.TorihiksakiCd = dr.Field<string>("TORIHIKISAKI_CD");
                                this.dtoTorihikisaki.BankCd = dr.Field<string>("FURIKOMI_BANK_CD_AFTER");
                                this.dtoTorihikisaki.BankShitenCd = dr.Field<string>("FURIKOMI_BANK_SHITEN_CD_AFTER");
                                this.dtoTorihikisaki.KouzaShurui = dr.Field<string>("KOUZA_SHURUI_AFTER");
                                this.dtoTorihikisaki.KouzaNo = dr.Field<string>("KOUZA_NO_AFTER");
                                this.dtoTorihikisaki.KouzaName = dr.Field<string>("KOUZA_NAME_AFTER");
                                this.daoM_TORIHIKISAKI_SEIKYUU.UpdateBank(this.dtoTorihikisaki);
                                this.daoM_TORIHIKISAKI.UpdateBank(this.dtoTorihikisaki);
                            }
                            tran.Commit();
                        }
                        this.msgLogic.MessageBoxShow("I001", "登録");
                        this.SearchAndDisplay();
                    }
                    catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
                    {
                        LogUtility.Warn(ex); //排他は警告
                        this.msgLogic.MessageBoxShow("E080");
                    }
                    catch (Exception ex)
                    {
                        LogUtility.Error(ex); //その他はエラー
                        this.msgLogic.MessageBoxShow("E093");
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店（明細部）変更後
        /// </summary>
        /// <param name="e">イベントデータ</param>
        public bool dgvBank_CellValidating(DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                var col = this.dgv.Columns[e.ColumnIndex];

                if (col.Name.StartsWith("FURIKOMI_BANK_SHITEN_CD_AFTER"))
                {
                    var bankShitenCd = e.FormattedValue.ToString();
                    if (bankShitenCd != "")
                    {
                        var wd = (int)((DgvCustomTextBoxColumn)col).CharactersNumber;
                        bankShitenCd = bankShitenCd.PadLeft(wd, '0');
                        this.dgv.CurrentCell.Value = bankShitenCd;
                    }

                    var row = this.dgv.Rows[e.RowIndex];
                    var bankCd = row.Cells["FURIKOMI_BANK_CD_AFTERColumn"].Value.ToString();
                    Action<string, string, string, string> SetKouza = (sName, kShurui, kNo, kName) =>
                    {
                        row.Cells["BANK_SHITEN_NAME_AFTERColumn"].Value = sName;
                        row.Cells["KOUZA_SHURUI_AFTERColumn"].Value = this.form.KOUZA_SHURUI_GRID.Text = kShurui;
                        row.Cells["KOUZA_NO_AFTERColumn"].Value = this.form.KOUZA_NO_GRID.Text = kNo;
                        row.Cells["KOUZA_NAME_AFTERColumn"].Value = this.form.KOUZA_NAME_GRID.Text = kName;
                    };
                    Action ClearKouza = () => SetKouza("", "", "", "");
                    if (bankShitenCd != this.form.BANK_SHITEN_CD_GRID.Text)
                    {
                        ClearKouza();
                    }

                    var kouzaShurui = row.Cells["KOUZA_SHURUI_AFTERColumn"].Value.ToString();
                    var kouzaNo = row.Cells["KOUZA_NO_AFTERColumn"].Value.ToString();

                    if (bankShitenCd != "")
                    {
                        var kv = GetBankShiten(bankCd, bankShitenCd, kouzaShurui, kouzaNo);
                        switch (kv.Key)
                        {
                            case 0:
                                this.msgLogic.MessageBoxShow("E020", "銀行支店");
                                ControlUtility.SetInputErrorOccuredForDgvCell(this.dgv[e.ColumnIndex, e.RowIndex], true);
                                e.Cancel = true;
                                ClearKouza();
                                break;
                            case 1:
                                this.form.BANK_SHITEN_CD_GRID.Text = bankShitenCd;
                                var drBankShiten = kv.Value;
                                row.Cells["FURIKOMI_BANK_CD_AFTERColumn"].Value = drBankShiten.Field<string>("BANK_CD");
                                row.Cells["FURIKOMI_BANK_SHITEN_CD_AFTERColumn"].Value = drBankShiten.Field<string>("BANK_SHITEN_CD");
                                row.Cells["BANK_NAME_AFTERColumn"].Value = drBankShiten.Field<string>("BANK_NAME");
                                SetKouza(drBankShiten.Field<string>("BANK_SHITEN_NAME"),
                                    drBankShiten.Field<string>("KOUZA_SHURUI"),
                                    drBankShiten.Field<string>("KOUZA_NO"),
                                    drBankShiten.Field<string>("KOUZA_NAME"));
                                break;
                            default:
                                SendKeys.Send(" ");
                                e.Cancel = true;
                                break;
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("dgvBank_CellValidating", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvBank_CellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 銀行支店、銀行支店（変更後）変更後
        /// </summary>
        /// <param name="tbBankShitenCd">銀行支店CD</param>
        /// <param name="e">イベントデータ</param>
        public bool BANK_SHITEN_CD_Validating(CustomAlphaNumTextBox tbBankShitenCd, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(tbBankShitenCd, e);

                var ctrlNames = tbBankShitenCd.SetFormField.Split(',');
                var tbBankCd = (CustomTextBox)this.form.Controls[ctrlNames[0]];
                var tbBankName = (CustomTextBox)this.form.Controls[ctrlNames[1]];
                var tbBankShitenName = (CustomTextBox)this.form.Controls[ctrlNames[3]];
                var tbBankShitenCdOld = (CustomTextBox)this.form.Controls[ctrlNames[4]];
                var tbKouzaShurui = (CustomTextBox)this.form.Controls[ctrlNames[5]];
                var tbKouzaNo = (CustomTextBox)this.form.Controls[ctrlNames[6]];
                var tbKouzaName = (CustomTextBox)this.form.Controls[ctrlNames[7]];

                var bankCd = tbBankCd.Text;
                var bankShitenCd = tbBankShitenCd.Text;
                Action<string, string, string, string> SetKouza = (sName, kShurui, kNo, kName) =>
                {
                    tbBankShitenName.Text = sName;
                    tbKouzaShurui.Text = kShurui;
                    tbKouzaNo.Text = kNo;
                    tbKouzaName.Text = kName;
                };
                Action ClearKouza = () => SetKouza("", "", "", "");
                if (bankShitenCd != tbBankShitenCdOld.Text)
                {
                    ClearKouza();
                }
                var kouzaShurui = tbKouzaShurui.Text;
                var kouzaNo = tbKouzaNo.Text;

                if (bankShitenCd != "")
                {
                    var kv = GetBankShiten(bankCd, bankShitenCd, kouzaShurui, kouzaNo);
                    switch (kv.Key)
                    {
                        case 0:
                            tbBankShitenCd.IsInputErrorOccured = true;
                            this.msgLogic.MessageBoxShow("E020", "銀行支店");
                            e.Cancel = true;
                            ClearKouza();
                            break;
                        case 1:
                            tbBankShitenCdOld.Text = bankShitenCd;
                            var drBankShiten = kv.Value;
                            tbBankCd.Text = drBankShiten.Field<string>("BANK_CD");
                            tbBankShitenCd.Text = drBankShiten.Field<string>("BANK_SHITEN_CD");
                            if (tbBankShitenName == this.form.BANK_SHITEN_NAME)
                            {
                                tbBankName.Text = drBankShiten.Field<string>("BANK_NAME_RYAKU");
                                SetKouza(drBankShiten.Field<string>("BANK_SHITEN_INFO"),
                                    drBankShiten.Field<string>("KOUZA_SHURUI"),
                                    drBankShiten.Field<string>("KOUZA_NO"),
                                    drBankShiten.Field<string>("KOUZA_NAME"));
                            }
                            else
                            {
                                tbBankName.Text = drBankShiten.Field<string>("BANK_NAME");
                                SetKouza(drBankShiten.Field<string>("BANK_SHITEN_NAME"),
                                    drBankShiten.Field<string>("KOUZA_SHURUI"),
                                    drBankShiten.Field<string>("KOUZA_NO"),
                                    drBankShiten.Field<string>("KOUZA_NAME"));
                            }
                            break;
                        default:
                            SendKeys.Send(" ");
                            e.Cancel = true;
                            break;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("BANK_SHITEN_CD_Validating", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BANK_SHITEN_CD_Validating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 銀行支店DataRow取得
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <returns>銀行支店DataRow</returns>
        private KeyValuePair<int, DataRow> GetBankShiten(string bankCd, string bankShitenCd, string kouzaShurui, string kouzaNo)
        {
            LogUtility.DebugMethodStart(bankCd, bankShitenCd, kouzaShurui, kouzaNo);

            this.dtoBank.BankCd = bankCd;
            this.dtoBank.BankShitenCd = bankShitenCd;
            this.dtoBank.KouzaShurui = kouzaShurui;
            this.dtoBank.KouzaNo = kouzaNo;
            var dt = this.daoM_BANK_SHITEN.GetDataForEntity(this.dtoBank);
            int cnt = dt.Rows.Count;
            var dr = cnt == 1 ? dt.Rows[0] : null;
            var kv = new KeyValuePair<int, DataRow>(cnt, dr);

            LogUtility.DebugMethodEnd(kv);

            return kv;
        }

        /// <summary>
        /// 列の結合描写
        /// </summary>
        /// <param name="e"></param>
        public void dgvBank_CellPainting(DataGridViewCellPaintingEventArgs e)
        {
            //var col1 = this.dgv.Columns[e.ColumnIndex];
            //if (col1.HeaderText.StartsWith("変更"))
            //{

            //}
            //else if (col1.HeaderText.StartsWith("銀行"))
            //{
            //    var col2 = this.dgv.Columns[e.ColumnIndex + 1];

            //    // セルの矩形を取得
            //    var rect = e.CellBounds;

            //    rect.Width += col2.Width;
            //    rect.Y = e.CellBounds.Y + 1;

            //    // 背景、枠線、セルの値を描画
            //    using (var brush = new SolidBrush(this.dgv.ColumnHeadersDefaultCellStyle.BackColor))
            //    {
            //        // 背景の描画
            //        e.Graphics.FillRectangle(brush, rect);

            //        using (var pen = new Pen(dgv.GridColor))
            //        {
            //            // 枠線の描画
            //            e.Graphics.DrawRectangle(pen, rect);
            //        }
            //    }

            //    // セルに表示するテキストを描画
            //    TextRenderer.DrawText(e.Graphics,
            //                    string.Format("{0}（{1}）", col1.HeaderText, col2.HeaderText),
            //                    e.CellStyle.Font,
            //                    rect,
            //                    e.CellStyle.ForeColor,
            //                    TextFormatFlags.HorizontalCenter
            //                    | TextFormatFlags.VerticalCenter);
            //}
            //else
            //{
            //    e.Paint(e.ClipBounds, e.PaintParts);
            //}
            //// イベントハンドラ内で処理を行ったことを通知
            //e.Handled = true;

            var col1 = this.dgv.Columns[e.ColumnIndex];
            if (col1.HeaderText.StartsWith("変更"))
            {

            }
            else if (col1.HeaderText.StartsWith("銀行"))
            {
                var col2 = this.dgv.Columns[e.ColumnIndex + 1];
                int colIndex = e.ColumnIndex;

                // セルの矩形を取得
                var rect = e.CellBounds;

                // 1列目の場合
                if (e.ColumnIndex == colIndex)
                {
                    // 2列目の幅を取得して、1列目の幅に足す
                    rect.Width += dgv.Columns[colIndex + 1].Width;
                    rect.Y = e.CellBounds.Y + 1;
                }
                else
                {
                    // 1列目の幅を取得して、2列目の幅に足す
                    rect.Width += dgv.Columns[colIndex].Width;
                    rect.Y = e.CellBounds.Y + 1;

                    // Leftを1列目に合わせる
                    rect.X -= dgv.Columns[colIndex].Width;
                }

                // 背景、枠線、セルの値を描画
                using (var brush = new SolidBrush(this.dgv.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    // 背景の描画
                    e.Graphics.FillRectangle(brush, rect);

                    using (var pen = new Pen(dgv.GridColor))
                    {
                        // 枠線の描画
                        e.Graphics.DrawRectangle(pen, rect);
                    }

                    using (Pen pen1 = new Pen(Color.DarkGray))
                    {
                        // 直線を描画(ヘッダ上部)
                        e.Graphics.DrawLine(pen1, rect.X, rect.Y - 1, rect.X + rect.Width, rect.Y - 1);

                        // 直線を描画(ヘッダ下部)
                        e.Graphics.DrawLine(pen1, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                    }
                }

                // セルに表示するテキストを描画
                TextRenderer.DrawText(e.Graphics,
                                string.Format("{0}（{1}）", col1.HeaderText, col2.HeaderText),
                                e.CellStyle.Font,
                                rect,
                                e.CellStyle.ForeColor,
                                TextFormatFlags.HorizontalCenter
                                | TextFormatFlags.VerticalCenter);
            }
            else
            {
                e.Paint(e.ClipBounds, e.PaintParts);
            }
            // イベントハンドラ内で処理を行ったことを通知
            e.Handled = true;
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
    }
}
