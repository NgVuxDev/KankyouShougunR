// $Id: LogicClass.cs 64702 2015-11-10 07:38:07Z yendiem@e-mall.co.jp $
using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.APP;
using Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.Const;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using Shougun.Core.Common.BusinessCommon;
using System.Data.SqlTypes;
using System.Collections;
using r_framework.Dao;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;

namespace Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private RakurakuMasutaIchiranForm form;

        /// <summary>
        /// 全コントロール一覧
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// システム設定マスタのエンティティ
        /// </summary>
        private M_SYS_INFO sysinfoEntity;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorihikisaki;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao daoToriSeikyuu;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        private IM_RAKU_RAKUDao daoRakuRaku;

        public BusinessBaseForm parentForm;

        public UIHeader headerForm;

        public bool newValue = false;

        private string _shoshiki = string.Empty;

        public MessageBoxShowLogic msgLogic;
        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(RakurakuMasutaIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.daoTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.daoToriSeikyuu = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoRakuRaku = DaoInitUtility.GetComponent<IM_RAKU_RAKUDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();
            this.parentForm = (BusinessBaseForm)this.form.Parent;
            this.headerForm = (UIHeader)this.parentForm.headerForm;
            // ボタンのテキストを初期化
            this.ButtonInit();
            if (!this.form.EventSetFlg)
            {
                // イベントの初期化処理
                this.EventInit();
                this.form.EventSetFlg = true;
            }
            this.allControl = this.form.allControl;
            this.form.customDataGridView1.AllowUserToAddRows = false;   // 行の追加オプション(false)
            this.form.customDataGridView1.TabStop = false;
            // システム設定情報読み込み
            this.GetSysInfo();

            // ヘッダーの初期化
            this.InitHeaderArea();

            LogUtility.DebugMethodEnd();
        }

        public void InitFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //並び替えと明細の設定
                this.form.customSearchHeader1.Visible = true;
                this.form.customSearchHeader1.Location = new Point(4, 115);
                this.form.customSearchHeader1.Size = new Size(992, 26);

                this.form.customSortHeader1.Location = new Point(4, 140);
                this.form.customSortHeader1.Size = new Size(992, 26);

                //明細部：　ブランク
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                this.form.customDataGridView1.TabIndex = 60;
                this.form.customDataGridView1.Location = new Point(4, 168);
                this.form.customDataGridView1.Size = new Size(992, 280);

                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();

            }
            catch (Exception ex)
            {
                LogUtility.Error("InitFrom", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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

            //// ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();
       
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func4.Click += new EventHandler(this.bt_func4_Click);      // 削除
            parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);      // CSV
            parentForm.bt_func7.Click += new EventHandler(this.bt_func7_Click);      // 検索条件クリア
            parentForm.bt_func8.Click += new EventHandler(this.bt_func8_Click);      // 検索
            parentForm.bt_func9.Click += new EventHandler(this.bt_func9_Click);      // 楽楽CSV
            parentForm.bt_func10.Click += new EventHandler(this.bt_func10_Click);    // 並び替え
            parentForm.bt_func11.Click += new EventHandler(this.bt_func11_Click);     //F11 フィルタ
            parentForm.bt_func12.Click += new EventHandler(this.bt_func12_Click);    // 閉じる
            parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);       // パターン一覧画面へ遷移

            this.headerForm.RAKURAKU_MEISAI_RENKEI.KeyDown += new KeyEventHandler(this.RAKURAKU_MEISAI_RENKEI_KeyDown);
            this.headerForm.RAKURAKU_MEISAI_RENKEI.TextChanged += new EventHandler(this.RAKURAKU_MEISAI_RENKEI_TextChanged);
            LogUtility.DebugMethodEnd();
        }

        public void RAKURAKU_MEISAI_RENKEI_TextChanged(object sender, EventArgs e)
        {
            if (newValue) { newValue = false; return; }
            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.msgLogic = new MessageBoxShowLogic();
            if (this.headerForm.RAKURAKU_MEISAI_RENKEI.Text == "1")
            {
                if (this.form.customDataGridView1.Rows.Count > 0)
                {
                    if (this.msgLogic.MessageBoxShow("C123") == DialogResult.Yes)
                    {
                        this.form.Table.Rows.Clear();
                        this.form.customDataGridView1.Refresh();
                        parentForm.bt_func9.Enabled = true;
                        newValue = false;
                    }
                    else
                    {
                        newValue = true;
                        this.headerForm.RAKURAKU_MEISAI_RENKEI.Text = "2";
                    }
                }
                else parentForm.bt_func9.Enabled = true;
            }
            else if (this.headerForm.RAKURAKU_MEISAI_RENKEI.Text == "2")
            {
                if (this.form.customDataGridView1.Rows.Count > 0)
                {
                    if (this.msgLogic.MessageBoxShow("C123") == DialogResult.Yes)
                    {
                        this.form.Table.Rows.Clear();
                        this.form.customDataGridView1.Refresh();
                        parentForm.bt_func9.Enabled = false;
                        newValue = false;
                    }
                    else
                    {
                        newValue = true;
                        this.headerForm.RAKURAKU_MEISAI_RENKEI.Text = "1";
                    }
                }
                else parentForm.bt_func9.Enabled = false;
            }
        }

        public void RAKURAKU_MEISAI_RENKEI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                this.form.RAKURAKU_CSV_KBN.Focus();
            }
        }
        #endregion

        #region Functionボタン 押下処理

        private void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.msgLogic = new MessageBoxShowLogic();
                var listdata = this.form.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(c => Convert.ToBoolean(c.Cells[ConstansIC.RENKEI].EditedFormattedValue.ToString()) == true).ToList();
                if (listdata.Count == 0)
                {
                    this.msgLogic.MessageBoxShow("E317");
                    return;
                }

                var result = this.msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {
                    using (Transaction tran = new Transaction())
                    {
                        foreach (DataGridViewRow row in listdata)
                        {
                            var rakuid = Convert.ToString(row.Cells[ConstansIC.RAKU_ID].EditedFormattedValue);
                            if (!string.IsNullOrEmpty(rakuid))
                            {
                                var raku = daoRakuRaku.GetDataByCd(rakuid);
                                raku.DELETE_FLG = true;
                                daoRakuRaku.Update(raku);
                            }
                        }
                        // コミット
                        tran.Commit();
                    }
                    this.msgLogic.MessageBoxShow("I001", "削除");
                    this.bt_func8_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func4_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// F6 CSV
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            CSVExport csvLogic = new CSVExport();
            DENSHU_KBN id = this.form.DenshuKbn;
            this.form.customDataGridView1.Columns["選択"].Visible = false;
            csvLogic.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, id.ToTitleString(), this.form);
            this.form.customDataGridView1.Columns["選択"].Visible = true;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.TORIHIKISAKI_CD.Clear();
            this.form.TORIHIKISAKI_NAME_RYAKU.Clear();
            this.form.GYOUSHA_CD.Clear();
            this.form.GYOUSHA_RNAME.Clear();
            this.form.GENBA_CD.Clear();
            this.form.GENBA_RNAME.Clear();
            this.form.RAKURAKU_CSV_KBN.Text = "2";
            this.form.SEIKYUU_SHO_SHOSHIKI_1.Text = "9";
            this.form.cb_shimebi.ResetText();
            this.form.cb_shimebi.SelectedIndex = -1;
            this.headerForm.RAKURAKU_MEISAI_RENKEI.Text = "1";
            var ds = (DataTable)this.form.customDataGridView1.DataSource;
            if (ds != null)
            {
                ds.Clear();
                this.form.customDataGridView1.DataSource = ds;
            }
            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();
            SaveHyoujiJoukenDefault();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // パターン未登録の場合検索処理を行わない
                if (this.form.PatternNo == 0)
                {
                    this.msgLogic = new MessageBoxShowLogic();
                    this.msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                if(!CheckDataRadioButton()) return;

                //読込データ件数を取得
                string resultCount = this.Search().ToString();
                if (resultCount == "0")
                {
                    this.msgLogic = new MessageBoxShowLogic();
                    this.msgLogic.MessageBoxShow("C001");
                }

                this.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 楽楽CSV
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                int _count = 0;
                var listdata = this.form.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(c => Convert.ToBoolean(c.Cells[ConstansIC.RENKEI].EditedFormattedValue.ToString()) == true).ToList();
                _count = listdata.Count;
                if (_count == 0)
                {
                    this.msgLogic = new MessageBoxShowLogic();
                    this.msgLogic.MessageBoxShow("E315");
                    return;
                }

                if (listdata.Where(r => r.Cells[ConstansIC.RAKURAKU_CUSTOMER_CD].Value.IsNullOrEmpty() == true).Count() > 0)
                {
                    this.msgLogic = new MessageBoxShowLogic();
                    this.msgLogic.MessageBoxShow("E318");
                    return;
                }    

                DataTable dt = new DataTable();
                foreach (DataGridViewColumn col in this.form.customDataGridView1.Columns) dt.Columns.Add(col.Name);
                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(c => Convert.ToBoolean(c.Cells[ConstansIC.RENKEI].EditedFormattedValue.ToString()) == true))
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells) dRow[cell.ColumnIndex] = cell.Value;
                    dt.Rows.Add(dRow);
                }

                var shoshiki = SqlInt16.Parse(this.form.SEIKYUU_SHO_SHOSHIKI_1.Text);
                var rakuCSV = SqlInt16.Parse(this.form.RAKURAKU_CSV_KBN.Text);
                var listgroup = dt.AsEnumerable().GroupBy(x => x.Field<string>("RAKURAKU_CUSTOMER_CD")).Select(g => new { RAKURAKU_CUSTOMER_CD = g.Key, COUNT = g.Count() });
                if (listgroup.Any(c => c.COUNT > 1))
                {
                    string rakukodo = listgroup.Where(c => c.COUNT > 1).FirstOrDefault().RAKURAKU_CUSTOMER_CD;
                    this.msgLogic = new MessageBoxShowLogic();
                    this.msgLogic.MessageBoxShow("E316", rakukodo);
                    return;
                }

                List<M_RAKU_RAKU> listRaku = new List<M_RAKU_RAKU>();
                if(rakuCSV == 1)
                {
                    foreach (DataGridViewRow row in listdata)
                    {
                        var shoshikiKbn = Convert.ToString(row.Cells[ConstansIC.SHOSHIKI_KBN].EditedFormattedValue);
                        var raku = AddListDataRaku(row, false);
                        if (CheckDataGridColumnName(ConstansIC.RAKU_ID))
                        {
                            var rankId = Convert.ToString(row.Cells[ConstansIC.RAKU_ID].EditedFormattedValue);
                            if (!string.IsNullOrEmpty(rankId)) raku.RAKU_ID = SqlInt64.Parse(rankId);
                        }
                        listRaku.Add(raku);
                    }    
                }   
                else
                {
                    foreach (DataGridViewRow row in listdata)
                    {
                        var raku = AddListDataRaku(row, false);
                        listRaku.Add(raku);
                    }
                }

                var cols = new string[] { "顧客コード", "社名", "部署", "担当者名", "敬称", "郵便番号", "住所1", "住所2", "メールアドレス", "サブメールアドレス1", "サブメールアドレス2", "サブメールアドレス3" };
                DataTable dataCSV = new DataTable();
                foreach (var col in cols) dataCSV.Columns.Add(col);

                foreach (var raku in listRaku)
                {
                    DataRow dr = dataCSV.NewRow();
                    dr[0] = raku.RAKURAKU_CUSTOMER_CD;
                    dr[1] = "\"" + raku.SEIKYUU_SHO_SOUFU_SAKI + "\"";
                    dr[2] = raku.SOUFU_SAKI_BUSHO;
                    dr[3] = raku.SOUFU_SAKI_TANTOUSHA;
                    dr[4] = raku.KEISHOU;
                    dr[5] = raku.SOUFU_SAKI_POST;
                    dr[6] = "\"" + raku.SOUFU_SAKI_ADDRESS1 + "\"";
                    dr[7] = "\"" + raku.SOUFU_SAKI_ADDRESS2 + "\"";
                    dr[8] = raku.EMAIL;
                    dr[9] = raku.EMAIL_ADDRESS1;
                    dr[10] = raku.EMAIL_ADDRESS2;
                    dr[11] = raku.EMAIL_ADDRESS3;
                    dataCSV.Rows.Add(dr);
                }

                CSVExport csvLogic = new CSVExport();
                var result = csvLogic.ConvertDataTableToCsvRaku(dataCSV, true, true, "楽楽CSVマスタ", this.form);

                var rakuKbn = SqlInt16.Parse(this.headerForm.RAKURAKU_MEISAI_RENKEI.Text);
                if (result)
                {
                    using (Transaction tran = new Transaction())
                    {
                        foreach (var entiry in listRaku)
                        {
                            if (rakuCSV == 1)
                            {
                                M_RAKU_RAKU raku = new M_RAKU_RAKU();
                                raku = daoRakuRaku.GetDataByCd(entiry.RAKU_ID.ToString());
                                if(raku != null)
                                {
                                    raku.DELETE_FLG = true;
                                    daoRakuRaku.Update(raku);
                                }

                                raku.RAKURAKU_MEISAI_KBN = rakuKbn;
                                raku.RAKURAKU_CUSTOMER_CD = entiry.RAKURAKU_CUSTOMER_CD;
                                raku.SHOSHIKI_KBN = entiry.SHOSHIKI_KBN;
                                raku.TORIHIKISAKI_CD = entiry.TORIHIKISAKI_CD;
                                raku.GYOUSHA_CD = entiry.GYOUSHA_CD;
                                raku.GENBA_CD = entiry.GENBA_CD;
                                raku.EMAIL = entiry.EMAIL;
                                raku.EMAIL_ADDRESS1 = entiry.EMAIL_ADDRESS1;
                                raku.EMAIL_ADDRESS2 = entiry.EMAIL_ADDRESS2;
                                raku.EMAIL_ADDRESS3 = entiry.EMAIL_ADDRESS3;
                                raku.SEIKYUU_SHO_SOUFU_SAKI = entiry.SEIKYUU_SHO_SOUFU_SAKI;
                                raku.SOUFU_SAKI_BUSHO = entiry.SOUFU_SAKI_BUSHO;
                                raku.SOUFU_SAKI_TANTOUSHA = entiry.SOUFU_SAKI_TANTOUSHA;
                                raku.KEISHOU = entiry.KEISHOU;
                                raku.SOUFU_SAKI_POST = entiry.SOUFU_SAKI_POST;
                                raku.SOUFU_SAKI_ADDRESS1 = entiry.SOUFU_SAKI_ADDRESS1;
                                raku.SOUFU_SAKI_ADDRESS2 = entiry.SOUFU_SAKI_ADDRESS2;
                                raku.DELETE_FLG = false;
                                string computerName = SystemInformation.ComputerName;
                                raku.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                raku.UPDATE_USER = SystemProperty.UserName;
                                raku.UPDATE_PC = computerName;
                                daoRakuRaku.Insert(raku);
                            }
                            else
                            {
                                var rakuNew = new M_RAKU_RAKU();
                                rakuNew.RAKURAKU_MEISAI_KBN = rakuKbn;
                                rakuNew.RAKURAKU_CUSTOMER_CD = entiry.RAKURAKU_CUSTOMER_CD;
                                rakuNew.SHOSHIKI_KBN = entiry.SHOSHIKI_KBN;
                                rakuNew.TORIHIKISAKI_CD = entiry.TORIHIKISAKI_CD;
                                rakuNew.GYOUSHA_CD = entiry.GYOUSHA_CD;
                                rakuNew.GENBA_CD = entiry.GENBA_CD;

                                rakuNew.EMAIL = entiry.EMAIL;
                                rakuNew.EMAIL_ADDRESS1 = entiry.EMAIL_ADDRESS1;
                                rakuNew.EMAIL_ADDRESS2 = entiry.EMAIL_ADDRESS2;
                                rakuNew.EMAIL_ADDRESS3 = entiry.EMAIL_ADDRESS3;

                                rakuNew.SEIKYUU_SHO_SOUFU_SAKI = entiry.SEIKYUU_SHO_SOUFU_SAKI;
                                rakuNew.SOUFU_SAKI_BUSHO = entiry.SOUFU_SAKI_BUSHO;
                                rakuNew.SOUFU_SAKI_TANTOUSHA = entiry.SOUFU_SAKI_TANTOUSHA;
                                rakuNew.KEISHOU = entiry.KEISHOU;
                                rakuNew.SOUFU_SAKI_POST = entiry.SOUFU_SAKI_POST;
                                rakuNew.SOUFU_SAKI_ADDRESS1 = entiry.SOUFU_SAKI_ADDRESS1;
                                rakuNew.SOUFU_SAKI_ADDRESS2 = entiry.SOUFU_SAKI_ADDRESS2;
                                rakuNew.DELETE_FLG = false;

                                string computerName = SystemInformation.ComputerName;
                                rakuNew.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                rakuNew.CREATE_USER = SystemProperty.UserName;
                                rakuNew.CREATE_PC = computerName;
                                rakuNew.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                rakuNew.UPDATE_USER = SystemProperty.UserName;
                                rakuNew.UPDATE_PC = computerName;
                                daoRakuRaku.Insert(rakuNew);
                            }    
                        }
                        // コミット
                        tran.Commit();
                    }
                
                    if (rakuCSV == 2) foreach (DataGridViewRow row in listdata) this.form.customDataGridView1.Rows.Remove(row);
                    if (rakuCSV == 1) { this.bt_func8_Click(null, null); }
                }    
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func9_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private M_RAKU_RAKU AddListDataRaku(DataGridViewRow row, bool del)
        {
            try
            {
                M_RAKU_RAKU raku = new M_RAKU_RAKU();
                var shoshikiKbn = Convert.ToString(row.Cells[ConstansIC.SHOSHIKI_KBN].EditedFormattedValue);
                raku.SHOSHIKI_KBN = SqlInt16.Parse(shoshikiKbn);
                if (!string.IsNullOrEmpty(row.Cells[ConstansIC.KEY_ID0].EditedFormattedValue.ToString()))
                {
                    raku.TORIHIKISAKI_CD = row.Cells[ConstansIC.KEY_ID0].EditedFormattedValue.ToString();
                }

                if (!string.IsNullOrEmpty(row.Cells[ConstansIC.KEY_ID1].EditedFormattedValue.ToString()))
                {
                    raku.GYOUSHA_CD = row.Cells[ConstansIC.KEY_ID1].EditedFormattedValue.ToString();
                }

                if (!string.IsNullOrEmpty(row.Cells[ConstansIC.KEY_ID2].EditedFormattedValue.ToString()))
                {
                    raku.GENBA_CD = row.Cells[ConstansIC.KEY_ID2].EditedFormattedValue.ToString();
                }

                string rakuraku_customer_cd = string.Empty, 
                       seikyuu_sho_soufu_saki = string.Empty, 
                       soufu_saki_busho = string.Empty,
                       soufu_saki_tantousha = string.Empty, 
                       keishou = string.Empty, 
                       soufu_saki_post = string.Empty,
                       soufu_saki_address1 = string.Empty, 
                       soufu_saki_address2 = string.Empty, 
                       email = string.Empty,
                       email_address1 = string.Empty, 
                       email_address2 = string.Empty, 
                       email_address3 = string.Empty;

                if (shoshikiKbn == "1")
                {
                    var tori = daoToriSeikyuu.GetDataByCd(row.Cells[ConstansIC.KEY_ID0].EditedFormattedValue.ToString());
                    if (tori != null)
                    {
                        rakuraku_customer_cd = tori.RAKURAKU_CUSTOMER_CD;
                        seikyuu_sho_soufu_saki = tori.SEIKYUU_SOUFU_NAME1 + tori.SEIKYUU_SOUFU_NAME2;
                        soufu_saki_busho = tori.SEIKYUU_SOUFU_BUSHO;
                        soufu_saki_tantousha = tori.SEIKYUU_SOUFU_TANTOU;
                        keishou = (tori.SEIKYUU_SOUFU_KEISHOU2 != string.Empty ? tori.SEIKYUU_SOUFU_KEISHOU2 : tori.SEIKYUU_SOUFU_KEISHOU1);
                        soufu_saki_post = tori.SEIKYUU_SOUFU_POST;
                        soufu_saki_address1 = tori.SEIKYUU_SOUFU_ADDRESS1;
                        soufu_saki_address2 = tori.SEIKYUU_SOUFU_ADDRESS2;
                    }
                }
                else if (shoshikiKbn == "2")
                {
                    var gyousha = daoGyousha.GetDataByCd(row.Cells[ConstansIC.KEY_ID1].EditedFormattedValue.ToString());
                    if (gyousha != null)
                    {
                        rakuraku_customer_cd = gyousha.RAKURAKU_CUSTOMER_CD;
                        seikyuu_sho_soufu_saki = gyousha.SEIKYUU_SOUFU_NAME1 + gyousha.SEIKYUU_SOUFU_NAME2;
                        soufu_saki_busho = gyousha.SEIKYUU_SOUFU_BUSHO;
                        soufu_saki_tantousha = gyousha.SEIKYUU_SOUFU_TANTOU;
                        keishou = (gyousha.SEIKYUU_SOUFU_KEISHOU2 != string.Empty ? gyousha.SEIKYUU_SOUFU_KEISHOU2 : gyousha.SEIKYUU_SOUFU_KEISHOU1);
                        soufu_saki_post = gyousha.SEIKYUU_SOUFU_POST;
                        soufu_saki_address1 = gyousha.SEIKYUU_SOUFU_ADDRESS1;
                        soufu_saki_address2 = gyousha.SEIKYUU_SOUFU_ADDRESS2;
                    }
                }
                else if (shoshikiKbn == "3")
                {
                    var genba = daoGenba.GetDataByCd(new M_GENBA { GYOUSHA_CD = row.Cells[ConstansIC.KEY_ID1].EditedFormattedValue.ToString(), GENBA_CD = row.Cells[ConstansIC.KEY_ID2].EditedFormattedValue.ToString() });
                    if (genba != null)
                    {
                        rakuraku_customer_cd = genba.RAKURAKU_CUSTOMER_CD;
                        seikyuu_sho_soufu_saki = genba.SEIKYUU_SOUFU_NAME1 + genba.SEIKYUU_SOUFU_NAME2;
                        soufu_saki_busho = genba.SEIKYUU_SOUFU_BUSHO;
                        soufu_saki_tantousha = genba.SEIKYUU_SOUFU_TANTOU;
                        keishou = (genba.SEIKYUU_SOUFU_KEISHOU2 != string.Empty ? genba.SEIKYUU_SOUFU_KEISHOU2 : genba.SEIKYUU_SOUFU_KEISHOU1);
                        soufu_saki_post = genba.SEIKYUU_SOUFU_POST;
                        soufu_saki_address1 = genba.SEIKYUU_SOUFU_ADDRESS1;
                        soufu_saki_address2 = genba.SEIKYUU_SOUFU_ADDRESS2;
                    }
                }

                if (CheckDataGridColumnName(ConstansIC.EMAIL)) raku.EMAIL = row.Cells[ConstansIC.EMAIL].EditedFormattedValue.ToString();
                if (CheckDataGridColumnName(ConstansIC.EMAIL_ADDRESS1)) raku.EMAIL_ADDRESS1 = row.Cells[ConstansIC.EMAIL_ADDRESS1].EditedFormattedValue.ToString();
                if (CheckDataGridColumnName(ConstansIC.EMAIL_ADDRESS2)) raku.EMAIL_ADDRESS2 = row.Cells[ConstansIC.EMAIL_ADDRESS2].EditedFormattedValue.ToString();
                if (CheckDataGridColumnName(ConstansIC.EMAIL_ADDRESS3)) raku.EMAIL_ADDRESS3 = row.Cells[ConstansIC.EMAIL_ADDRESS3].EditedFormattedValue.ToString();

                raku.RAKURAKU_CUSTOMER_CD = rakuraku_customer_cd;
                raku.SEIKYUU_SHO_SOUFU_SAKI = seikyuu_sho_soufu_saki;
                raku.SOUFU_SAKI_BUSHO = soufu_saki_busho;
                raku.SOUFU_SAKI_TANTOUSHA = soufu_saki_tantousha;
                raku.KEISHOU = keishou;
                raku.SOUFU_SAKI_POST = soufu_saki_post;
                raku.SOUFU_SAKI_ADDRESS1 = soufu_saki_address1;
                raku.SOUFU_SAKI_ADDRESS2 = soufu_saki_address2;
                raku.DELETE_FLG = del;
                string computerName = SystemInformation.ComputerName;
                raku.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                raku.CREATE_USER = SystemProperty.UserName;
                raku.CREATE_PC = computerName;
                raku.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                raku.UPDATE_USER = SystemProperty.UserName;
                raku.UPDATE_PC = computerName;

                return raku;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("AddListDataRaku", ex);
                throw;
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

                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E076");
                }
                else
                {
                    //ソート設定ダイアログを呼び出す
                    this.form.customSortHeader1.ShowCustomSortSettingDialog();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func10_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //フィルタ設定ダイアログを呼び出す
                this.form.customSearchHeader1.ShowCustomSearchSettingDialog();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func11_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SaveHyoujiJoukenDefault();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (parentForm != null)
            {
                parentForm.Close();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region プロセスボタン押下処理

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var sysID = this.form.OpenPatternIchiran();

                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_process1_Click", ex);
                throw;
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
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region ヘッダーの初期化
        public void InitHeaderShown()
        {
            this.headerForm.RAKURAKU_MEISAI_RENKEI.Focus();
        }

        public void InitHeaderArea()
        {
            this.headerForm.lb_title.Text = "楽楽明細マスタ一覧";            
            if (Properties.Settings.Default.RAKURAKU_MEISAI_RENKEI != string.Empty)
                this.headerForm.RAKURAKU_MEISAI_RENKEI.Text = Properties.Settings.Default.RAKURAKU_MEISAI_RENKEI;
            else
                this.headerForm.RAKURAKU_MEISAI_RENKEI.Text = "1";

            if (Properties.Settings.Default.RAKURAKU_CSV_KBN != string.Empty)
                this.form.RAKURAKU_CSV_KBN.Text = Properties.Settings.Default.RAKURAKU_CSV_KBN;
            else
                this.form.RAKURAKU_CSV_KBN.Text = "2";

            if (Properties.Settings.Default.SEIKYUU_SHO_SHOSHIKI_1 != string.Empty)
                this.form.SEIKYUU_SHO_SHOSHIKI_1.Text = Properties.Settings.Default.SEIKYUU_SHO_SHOSHIKI_1;
            else
                this.form.SEIKYUU_SHO_SHOSHIKI_1.Text = "9";

            this.form.TORIHIKISAKI_CD.Text = Properties.Settings.Default.TORIHIKISAKI_CD_TEXT;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT;

            this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GYOUSHA_CD_TEXT;
            this.form.GYOUSHA_RNAME.Text = Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT;

            this.form.GENBA_CD.Text = Properties.Settings.Default.GENBA_CD_TEXT;
            this.form.GENBA_RNAME.Text = Properties.Settings.Default.GENBA_NAME_RYAKU_TEXT;

            if (Properties.Settings.Default.CB_SHIMEBI != string.Empty)
            {
                this.form.cb_shimebi.Text = Properties.Settings.Default.CB_SHIMEBI;
            }   
            else
            {
                this.form.cb_shimebi.ResetText();
                this.form.cb_shimebi.SelectedIndex = -1;
            }    
        }

        #endregion

        #region 現場入力画面起動処理
        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        #endregion

        #region IBuisinessLogicで必須実装(未使用)

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

        #region Equals/GetHashCode/ToString
        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            LogicClass localLogic = other as LogicClass;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        public M_GENBA[] GetGenba(string genbaCd)
        {
            if (string.IsNullOrEmpty(genbaCd)) return null;

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GENBA_CD = genbaCd;
            var genba = this.daoGenba.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1) return null;

            return genba;
        }


        /// <summary>
        /// 現場チェック
        /// </summary>
        public void CheckGenba()
        {
            LogUtility.DebugMethodStart();
            // 初期化
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (string.IsNullOrEmpty(this.form.GENBA_CD.Text)) return;

            var genbaEntityList = this.GetGenba(this.form.GENBA_CD.Text);
            if (genbaEntityList == null || genbaEntityList.Length < 1)
            {
                // エラーメッセージ
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.GENBA_CD.Focus();
                return;
            }

            bool isContinue = false;
            M_GENBA genba = new M_GENBA();
            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text))
            {
                if (string.IsNullOrEmpty(this.form.GYOUSHA_RNAME.Text))
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_CD.Focus();
                    return;
                }

                foreach (M_GENBA genbaEntity in genbaEntityList)
                {
                    if (this.form.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                    {
                        isContinue = true;
                        genba = genbaEntity;
                        this.form.GENBA_RNAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                        break;
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    msgLogic.MessageBoxShow("E062", "業者");
                    this.form.GENBA_CD.Focus();
                    return;
                }

            }
            else
            {
                if (string.IsNullOrEmpty(this.form.GYOUSHA_RNAME.Text))
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_CD.Focus();
                    return;
                }

                foreach (M_GENBA genbaEntity in genbaEntityList)
                {
                    if (this.form.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                    {
                        isContinue = true;
                        genba = genbaEntity;
                        this.form.GENBA_RNAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                        break;
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    msgLogic.MessageBoxShow("E062", "業者");
                    this.form.GENBA_CD.Focus();
                    return;
                }

            }
            LogUtility.DebugMethodEnd();
        }
        #region 検索処理

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            this._shoshiki = this.form.SEIKYUU_SHO_SHOSHIKI_1.Text;
            this.form.Table = null;
            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                if (this._shoshiki == "9")
                {
                    this._shoshiki = "1"; 
                    var sql1 = MakeSearchConditionAll();

                    this._shoshiki = "2";
                    var sql2 = MakeSearchConditionAll();

                    this._shoshiki = "3";
                    var sql3 = MakeSearchConditionAll();

                    var orderByQuery = this.CreateOrderByQuery();
                    var sql = "SELECT * FROM ( " + sql1 + " UNION ALL " + sql2 + " UNION ALL " + sql3 + " ) M_RAKU ";

                    var result = new StringBuilder(256);
                    string strTemp = string.Empty, sqlwhere = string.Empty;
                    // 取引先CD
                    strTemp = this.form.TORIHIKISAKI_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        if (!string.IsNullOrWhiteSpace(result.ToString())) result.Append(" AND ");
                        result.AppendFormat(" M_RAKU.KEY_TORIHIKISAKI_CD = '{0}'", strTemp); //→　現場入力ー取引先CD　＝　図①取引先CD
                    }
                    // 業者CD
                    strTemp = this.form.GYOUSHA_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        if (!string.IsNullOrWhiteSpace(result.ToString())) result.Append(" AND ");
                        result.AppendFormat(" M_RAKU.KEY_GYOUSHA_CD = '{0}'", strTemp); //→  現場入力ー業者CD　＝　図①業者CD
                    }
                    // 現場CD
                    strTemp = this.form.GENBA_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        if (!string.IsNullOrWhiteSpace(result.ToString())) result.Append(" AND ");
                        result.AppendFormat(" M_RAKU.KEY_GENBA_CD = '{0}'", strTemp); //→　現場入力ー現場CD　＝　図①現場CD
                    }

                    if (result.Length > 0) sqlwhere = result.Insert(0, " WHERE 1 = 1 AND ").ToString();

                    this.form.Table = this.daoGenba.GetDateForStringSql(sql + " " + sqlwhere + " " + orderByQuery);
                    this._shoshiki = this.form.SEIKYUU_SHO_SHOSHIKI_1.Text;
                }   
                else
                {
                    // 検索文字列の作成
                    var sql = this.MakeSearchCondition();
                    this.form.Table = this.daoGenba.GetDateForStringSql(sql);
                }    
            }

            this.form.ShowData();
            return this.form.Table != null ? this.form.Table.Rows.Count : 0;
        }

        private string MakeSearchConditionAll()
        {
            var selectQuery = this.CreateSelectQuery();
            var fromQuery = this.CreateFromQuery();
            var whereQuery = this.CreateWhereQuery();

            return selectQuery + fromQuery + whereQuery;
        }

        /// <summary>
        /// 検索文字列を作成
        /// </summary>
        private string MakeSearchCondition()
        {
            var selectQuery = this.CreateSelectQuery();
            var fromQuery = this.CreateFromQuery();
            var whereQuery = this.CreateWhereQuery();
            var orderByQuery = this.CreateOrderByQuery();

            return selectQuery + fromQuery + whereQuery + orderByQuery;
        }

        /// <summary>
        /// Select句作成
        /// </summary>
        /// <returns></returns>
        private string CreateSelectQuery()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                sb.Append("SELECT DISTINCT ");

                string strTempCSV;
                strTempCSV = this.form.RAKURAKU_CSV_KBN.Text;
                if (strTempCSV == "1")
                {
                    if (!string.IsNullOrWhiteSpace(this._shoshiki))
                    {
                        switch (this._shoshiki)
                        {
                            case "1":
                                sb.AppendFormat(" M_RAKU.TORIHIKISAKI_CD AS {0}, ", ConstansIC.KEY_ID0);
                                sb.AppendFormat(" NULL AS {0}, ", ConstansIC.KEY_ID1);
                                sb.AppendFormat(" NULL AS {0}, ", ConstansIC.KEY_ID2);
                                sb.AppendFormat(" M_RAKU.SHOSHIKI_KBN AS {0}, ", ConstansIC.SHOSHIKI_KBN);
                                sb.AppendFormat(" M_RAKU.RAKURAKU_CUSTOMER_CD AS {0}, ", ConstansIC.RAKURAKU_CUSTOMER_CD);
                                break;

                            case "2":
                                sb.AppendFormat(" M_RAKU.TORIHIKISAKI_CD AS {0}, ", ConstansIC.KEY_ID0);
                                sb.AppendFormat(" M_RAKU.GYOUSHA_CD AS {0}, ", ConstansIC.KEY_ID1);
                                sb.AppendFormat(" NULL AS {0}, ", ConstansIC.KEY_ID2);
                                sb.AppendFormat(" M_RAKU.SHOSHIKI_KBN AS {0}, ", ConstansIC.SHOSHIKI_KBN);
                                sb.AppendFormat(" M_RAKU.RAKURAKU_CUSTOMER_CD AS {0}, ", ConstansIC.RAKURAKU_CUSTOMER_CD);
                                break;

                            case "3":
                                sb.AppendFormat(" M_RAKU.TORIHIKISAKI_CD AS {0}, ", ConstansIC.KEY_ID0);
                                sb.AppendFormat(" M_RAKU.GYOUSHA_CD AS {0}, ", ConstansIC.KEY_ID1);
                                sb.AppendFormat(" M_RAKU.GENBA_CD AS {0}, ", ConstansIC.KEY_ID2);
                                sb.AppendFormat(" M_RAKU.SHOSHIKI_KBN AS {0}, ", ConstansIC.SHOSHIKI_KBN);
                                sb.AppendFormat(" M_RAKU.RAKURAKU_CUSTOMER_CD AS {0}, ", ConstansIC.RAKURAKU_CUSTOMER_CD);
                                break;
                        }
                        sb.AppendFormat(" M_RAKU.RAKU_ID AS {0}, ", ConstansIC.RAKU_ID);
                    }
                }   
                else
                {
                    if (!string.IsNullOrWhiteSpace(this._shoshiki))
                    {
                        switch (this._shoshiki)
                        {
                            case "1":
                                sb.AppendFormat(" M_RAKU.TORIHIKISAKI_CD AS {0}, ", ConstansIC.KEY_ID0);
                                sb.AppendFormat(" NULL AS {0}, ", ConstansIC.KEY_ID1);
                                sb.AppendFormat(" NULL AS {0}, ", ConstansIC.KEY_ID2);
                                sb.AppendFormat(" M_RAKU.SHOSHIKI_KBN AS {0}, ", ConstansIC.SHOSHIKI_KBN);
                                sb.AppendFormat(" M_RAKU.RAKURAKU_CUSTOMER_CD AS {0}, ", ConstansIC.RAKURAKU_CUSTOMER_CD);
                                break;

                            case "2":
                                sb.AppendFormat(" M_RAKU.TORIHIKISAKI_CD AS {0}, ", ConstansIC.KEY_ID0);
                                sb.AppendFormat(" M_RAKU.GYOUSHA_CD AS {0}, ", ConstansIC.KEY_ID1);
                                sb.AppendFormat(" NULL AS {0}, ", ConstansIC.KEY_ID2);
                                sb.AppendFormat(" M_RAKU.SHOSHIKI_KBN AS {0}, ", ConstansIC.SHOSHIKI_KBN);
                                sb.AppendFormat(" M_RAKU.RAKURAKU_CUSTOMER_CD AS {0}, ", ConstansIC.RAKURAKU_CUSTOMER_CD);
                                break;

                            case "3":
                                sb.AppendFormat(" M_RAKU.TORIHIKISAKI_CD AS {0}, ", ConstansIC.KEY_ID0);
                                sb.AppendFormat(" M_RAKU.GYOUSHA_CD AS {0}, ", ConstansIC.KEY_ID1);
                                sb.AppendFormat(" M_RAKU.GENBA_CD AS {0}, ", ConstansIC.KEY_ID2);
                                sb.AppendFormat(" M_RAKU.SHOSHIKI_KBN AS {0}, ", ConstansIC.SHOSHIKI_KBN);
                                sb.AppendFormat(" M_RAKU.RAKURAKU_CUSTOMER_CD AS {0}, ", ConstansIC.RAKURAKU_CUSTOMER_CD);
                                break;
                        }
                        sb.AppendFormat(" NULL AS {0}, ", ConstansIC.RAKU_ID);
                    }
                }

                sb.Append(this.form.SelectQuery);
            }
            return sb.ToString();
        }

        /// <summary>
        /// From句作成
        /// </summary>
        /// <returns></returns>
        private string CreateFromQuery()
        {
            var sb = new StringBuilder();
            string strTempCSV, rakuKenkei;
            strTempCSV = this.form.RAKURAKU_CSV_KBN.Text;
            rakuKenkei = this.headerForm.RAKURAKU_MEISAI_RENKEI.Text;

            if (strTempCSV == "1")
            {
                if (!string.IsNullOrWhiteSpace(this._shoshiki))
                {
                    switch (this._shoshiki)
                    {
                        case "1":
                            {
                                sb.Append(" FROM ( ");
                                sb.Append(" SELECT C.TORIHIKISAKI_CD, C.SEIKYUU_SOUFU_NAME1, C.SEIKYUU_SOUFU_NAME2, C.SEIKYUU_SOUFU_KEISHOU1, C.SEIKYUU_SOUFU_KEISHOU2, C.SEIKYUU_SOUFU_POST, ");
                                sb.Append(" C.SEIKYUU_SOUFU_ADDRESS1, C.SEIKYUU_SOUFU_ADDRESS2, C.SEIKYUU_SOUFU_BUSHO, C.SEIKYUU_SOUFU_TANTOU, A.RAKURAKU_CUSTOMER_CD, C.SHIMEBI1, C.SHIMEBI2, C.SHIMEBI3, ");
                                sb.Append(" C.HICCHAKUBI, C.KAISHUU_MONTH, C.KAISHUU_DAY, C.KAISHUU_HOUHOU, C.SHOSHIKI_MEISAI_KBN, C.SEIKYUU_KEITAI_KBN, C.NYUUKIN_MEISAI_KBN, ");
                                sb.Append(" B.TORIHIKISAKI_NAME_RYAKU, A.GYOUSHA_CD, D.GYOUSHA_NAME_RYAKU, A.GENBA_CD, E.GENBA_NAME_RYAKU, ");
                                sb.Append(" A.SHOSHIKI_KBN, A.RAKU_ID, A.SEIKYUU_SHO_SOUFU_SAKI, A.SOUFU_SAKI_BUSHO, A.SOUFU_SAKI_TANTOUSHA, A.KEISHOU, A.SOUFU_SAKI_POST, A.SOUFU_SAKI_ADDRESS1, A.SOUFU_SAKI_ADDRESS2, A.EMAIL, ");
                                sb.Append(" A.EMAIL_ADDRESS1, A.EMAIL_ADDRESS2, A.EMAIL_ADDRESS3, A.CREATE_USER, A.CREATE_DATE, A.CREATE_PC, A.UPDATE_USER, A.UPDATE_DATE, A.UPDATE_PC ");
                                sb.Append(" FROM M_RAKU_RAKU A ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI B ON B.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU C ON C.TORIHIKISAKI_CD = B.TORIHIKISAKI_CD ");
                                sb.Append(" LEFT JOIN M_GYOUSHA D ON D.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD AND D.GYOUSHA_CD = A.GYOUSHA_CD ");
                                sb.Append(" LEFT JOIN M_GENBA E ON E.GYOUSHA_CD = A.GYOUSHA_CD AND E.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD AND E.GENBA_CD = A.GENBA_CD ");
                                sb.Append(" WHERE A.DELETE_FLG = 0 ");

                                if (rakuKenkei == "1") sb.Append(" AND C.OUTPUT_KBN = 3 ");
                                if (rakuKenkei == "2") sb.Append(" AND C.OUTPUT_KBN IN (1, 2) AND C.TORIHIKI_KBN_CD = 2 ");

                                sb.Append(" ) M_RAKU ");
                                break;
                            }
                        case "2":
                            {
                                sb.Append(" FROM ( ");
                                sb.Append(" SELECT A.GYOUSHA_CD, D.GYOUSHA_NAME_RYAKU, D.SEIKYUU_SOUFU_NAME1, D.SEIKYUU_SOUFU_NAME2, D.SEIKYUU_SOUFU_KEISHOU1, D.SEIKYUU_SOUFU_KEISHOU2, D.SEIKYUU_SOUFU_POST, D.SEIKYUU_SOUFU_ADDRESS1, ");
                                sb.Append(" D.SEIKYUU_SOUFU_ADDRESS2, D.SEIKYUU_SOUFU_BUSHO, D.SEIKYUU_SOUFU_TANTOU, A.RAKURAKU_CUSTOMER_CD, ");
                                sb.Append(" A.TORIHIKISAKI_CD, B.TORIHIKISAKI_NAME_RYAKU, A.GENBA_CD, E.GENBA_NAME_RYAKU, C.SHIMEBI1, C.SHIMEBI2, C.SHIMEBI3, ");
                                sb.Append(" C.HICCHAKUBI, C.KAISHUU_MONTH, C.KAISHUU_DAY, C.KAISHUU_HOUHOU, C.SHOSHIKI_MEISAI_KBN, C.SEIKYUU_KEITAI_KBN, C.NYUUKIN_MEISAI_KBN, ");
                                sb.Append(" A.SHOSHIKI_KBN, A.RAKU_ID, A.SEIKYUU_SHO_SOUFU_SAKI, A.SOUFU_SAKI_BUSHO, A.SOUFU_SAKI_TANTOUSHA, A.KEISHOU, A.SOUFU_SAKI_POST, A.SOUFU_SAKI_ADDRESS1, A.SOUFU_SAKI_ADDRESS2, A.EMAIL, ");
                                sb.Append(" A.EMAIL_ADDRESS1, A.EMAIL_ADDRESS2, A.EMAIL_ADDRESS3, A.CREATE_USER, A.CREATE_DATE, A.CREATE_PC, A.UPDATE_USER, A.UPDATE_DATE, A.UPDATE_PC ");
                                sb.Append(" FROM M_RAKU_RAKU A ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI B ON B.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU C ON C.TORIHIKISAKI_CD = B.TORIHIKISAKI_CD ");
                                sb.Append(" LEFT JOIN M_GYOUSHA D ON D.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD AND D.GYOUSHA_CD = A.GYOUSHA_CD ");
                                sb.Append(" LEFT JOIN M_GENBA E ON E.GYOUSHA_CD = A.GYOUSHA_CD AND E.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD AND E.GENBA_CD = A.GENBA_CD ");
                                sb.Append(" WHERE A.DELETE_FLG = 0 ");

                                if (rakuKenkei == "1") sb.Append(" AND C.OUTPUT_KBN = 3 ");
                                if (rakuKenkei == "2") sb.Append(" AND C.OUTPUT_KBN IN (1, 2) AND C.TORIHIKI_KBN_CD = 2 ");

                                sb.Append(" ) M_RAKU ");
                                break;
                            }
                        case "3":
                            {
                                sb.Append(" FROM ( ");
                                sb.Append(" SELECT A.GENBA_CD, F.GENBA_NAME_RYAKU, F.SEIKYUU_SOUFU_NAME1, F.SEIKYUU_SOUFU_NAME2, F.SEIKYUU_SOUFU_KEISHOU1, F.SEIKYUU_SOUFU_KEISHOU2, F.SEIKYUU_SOUFU_POST, F.SEIKYUU_SOUFU_ADDRESS1, ");
                                sb.Append(" F.SEIKYUU_SOUFU_ADDRESS2, F.SEIKYUU_SOUFU_BUSHO, F.SEIKYUU_SOUFU_TANTOU, A.RAKURAKU_CUSTOMER_CD, ");
                                sb.Append(" A.TORIHIKISAKI_CD, B.TORIHIKISAKI_NAME_RYAKU, A.GYOUSHA_CD, D.GYOUSHA_NAME_RYAKU, C.SHIMEBI1, C.SHIMEBI2, C.SHIMEBI3, ");
                                sb.Append(" C.HICCHAKUBI, C.KAISHUU_MONTH, C.KAISHUU_DAY, C.KAISHUU_HOUHOU, C.SHOSHIKI_MEISAI_KBN, C.SEIKYUU_KEITAI_KBN, C.NYUUKIN_MEISAI_KBN, ");
                                sb.Append(" A.SHOSHIKI_KBN, A.RAKU_ID, A.SEIKYUU_SHO_SOUFU_SAKI, A.SOUFU_SAKI_BUSHO, A.SOUFU_SAKI_TANTOUSHA, A.KEISHOU, A.SOUFU_SAKI_POST, A.SOUFU_SAKI_ADDRESS1, A.SOUFU_SAKI_ADDRESS2, A.EMAIL, ");
                                sb.Append(" A.EMAIL_ADDRESS1, A.EMAIL_ADDRESS2, A.EMAIL_ADDRESS3, A.CREATE_USER, A.CREATE_DATE, A.CREATE_PC, A.UPDATE_USER, A.UPDATE_DATE, A.UPDATE_PC ");
                                sb.Append(" FROM M_RAKU_RAKU A ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI B ON B.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU C ON C.TORIHIKISAKI_CD = B.TORIHIKISAKI_CD ");
                                sb.Append(" LEFT JOIN M_GYOUSHA D ON D.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD AND D.GYOUSHA_CD = A.GYOUSHA_CD ");
                                sb.Append(" LEFT JOIN M_GENBA F ON F.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD AND F.GYOUSHA_CD = A.GYOUSHA_CD AND F.GENBA_CD = A.GENBA_CD ");
                                sb.Append(" WHERE A.DELETE_FLG = 0 ");

                                if (rakuKenkei == "1") sb.Append(" AND C.OUTPUT_KBN = 3 ");
                                if (rakuKenkei == "2") sb.Append(" AND C.OUTPUT_KBN IN (1, 2) AND C.TORIHIKI_KBN_CD = 2 ");

                                sb.Append(" ) M_RAKU ");
                                break;
                            }
                    }
                }
            }
            else if (strTempCSV == "2")
            {
                if (!string.IsNullOrWhiteSpace(this._shoshiki))
                {
                    switch (this._shoshiki)
                    {
                        case "1":
                            {
                                sb.Append(" FROM ( ");
                                sb.Append(" SELECT B.*, A.TORIHIKISAKI_NAME_RYAKU, '' AS GYOUSHA_CD, '' AS GYOUSHA_NAME_RYAKU, '' AS GENBA_CD, '' AS GENBA_NAME_RYAKU, ");
                                sb.Append(" '' AS SEIKYUU_SHO_SOUFU_SAKI, '' AS SOUFU_SAKI_BUSHO, '' AS SOUFU_SAKI_TANTOUSHA, '' AS KEISHOU, '' AS SOUFU_SAKI_POST, '' AS SOUFU_SAKI_ADDRESS1, '' AS SOUFU_SAKI_ADDRESS2, ");
                                sb.Append(" '' AS EMAIL, '' AS EMAIL_ADDRESS1, '' AS EMAIL_ADDRESS2, '' AS EMAIL_ADDRESS3, '' AS CREATE_USER, NULL AS CREATE_DATE, '' AS CREATE_PC, '' AS UPDATE_USER, NULL AS UPDATE_DATE, '' AS UPDATE_PC ");
                                sb.Append(" FROM M_TORIHIKISAKI A ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU B ON B.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD ");
                                sb.Append(" WHERE A.DELETE_FLG = 0 ");

                                if (rakuKenkei == "1") sb.Append(" AND B.OUTPUT_KBN = 3 ");
                                if (rakuKenkei == "2") sb.Append(" AND B.OUTPUT_KBN IN (1, 2) AND B.TORIHIKI_KBN_CD = 2 ");

                                sb.Append(" AND NOT EXISTS (SELECT * FROM M_RAKU_RAKU WHERE DELETE_FLG = 0 AND SHOSHIKI_KBN = 1 AND ISNULL(TORIHIKISAKI_CD,'') = ISNULL(A.TORIHIKISAKI_CD,'')) ");
                                sb.Append(" ) M_RAKU ");
                                break;
                            }
                        case "2":
                            {
                                sb.Append(" FROM ( ");
                                sb.Append(" SELECT C.SHOSHIKI_KBN, C.SHIMEBI1, C.SHIMEBI2, C.SHIMEBI3, A.GYOUSHA_CD, A.GYOUSHA_NAME_RYAKU, A.SEIKYUU_SOUFU_NAME1, A.SEIKYUU_SOUFU_NAME2, A.SEIKYUU_SOUFU_KEISHOU1, A.SEIKYUU_SOUFU_KEISHOU2, A.SEIKYUU_SOUFU_POST, A.SEIKYUU_SOUFU_ADDRESS1, A.SEIKYUU_SOUFU_ADDRESS2, ");
                                sb.Append(" A.SEIKYUU_SOUFU_BUSHO, A.SEIKYUU_SOUFU_TANTOU, A.RAKURAKU_CUSTOMER_CD, B.TORIHIKISAKI_CD, B.TORIHIKISAKI_NAME_RYAKU, '' AS GENBA_CD, '' AS GENBA_NAME_RYAKU, ");
                                sb.Append(" '' AS SEIKYUU_SHO_SOUFU_SAKI, '' AS SOUFU_SAKI_BUSHO, '' AS SOUFU_SAKI_TANTOUSHA, '' AS KEISHOU, '' AS SOUFU_SAKI_POST, '' AS SOUFU_SAKI_ADDRESS1, '' AS SOUFU_SAKI_ADDRESS2, ");
                                sb.Append(" '' AS EMAIL, '' AS EMAIL_ADDRESS1, '' AS EMAIL_ADDRESS2, '' AS EMAIL_ADDRESS3, '' AS CREATE_USER, NULL AS CREATE_DATE, '' AS CREATE_PC, '' AS UPDATE_USER, NULL AS UPDATE_DATE, '' AS UPDATE_PC, ");
                                sb.Append(" C.HICCHAKUBI, C.KAISHUU_MONTH, C.KAISHUU_DAY, C.KAISHUU_HOUHOU, C.SHOSHIKI_MEISAI_KBN, C.SEIKYUU_KEITAI_KBN, C.NYUUKIN_MEISAI_KBN ");
                                sb.Append(" FROM M_GYOUSHA A ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI B ON B.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD AND B.DELETE_FLG = 0 ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU C ON C.TORIHIKISAKI_CD = B.TORIHIKISAKI_CD  ");
                                sb.Append(" WHERE A.DELETE_FLG = 0 ");

                                if (rakuKenkei == "1") sb.Append(" AND C.OUTPUT_KBN = 3 ");
                                if (rakuKenkei == "2") sb.Append(" AND C.OUTPUT_KBN IN (1, 2) AND C.TORIHIKI_KBN_CD = 2 ");

                                sb.Append(" AND NOT EXISTS (SELECT * FROM M_RAKU_RAKU WHERE DELETE_FLG = 0 AND SHOSHIKI_KBN = 2 AND ISNULL(TORIHIKISAKI_CD,'') = ISNULL(A.TORIHIKISAKI_CD,'') AND ISNULL(GYOUSHA_CD,'') = ISNULL(A.GYOUSHA_CD,'')) ");
                                sb.Append(" ) M_RAKU ");
                                break;
                            }
                        case "3":
                            {
                                sb.Append(" FROM ( ");
                                sb.Append(" SELECT D.SHOSHIKI_KBN, D.SHIMEBI1, D.SHIMEBI2, D.SHIMEBI3, A.GENBA_CD, A.GENBA_NAME_RYAKU, A.SEIKYUU_SOUFU_NAME1, A.SEIKYUU_SOUFU_NAME2, A.SEIKYUU_SOUFU_KEISHOU1, A.SEIKYUU_SOUFU_KEISHOU2, ");
                                sb.Append(" A.SEIKYUU_SOUFU_POST, A.SEIKYUU_SOUFU_ADDRESS1, A.SEIKYUU_SOUFU_ADDRESS2, A.SEIKYUU_SOUFU_BUSHO, A.SEIKYUU_SOUFU_TANTOU, A.RAKURAKU_CUSTOMER_CD,  ");
                                sb.Append(" C.TORIHIKISAKI_CD, C.TORIHIKISAKI_NAME_RYAKU, B.GYOUSHA_CD, B.GYOUSHA_NAME_RYAKU, ");
                                sb.Append(" '' AS SEIKYUU_SHO_SOUFU_SAKI, '' AS SOUFU_SAKI_BUSHO, '' AS SOUFU_SAKI_TANTOUSHA, '' AS KEISHOU, '' AS SOUFU_SAKI_POST, '' AS SOUFU_SAKI_ADDRESS1, '' AS SOUFU_SAKI_ADDRESS2, ");
                                sb.Append(" '' AS EMAIL, '' AS EMAIL_ADDRESS1, '' AS EMAIL_ADDRESS2, '' AS EMAIL_ADDRESS3, '' AS CREATE_USER, NULL AS CREATE_DATE, '' AS CREATE_PC, '' AS UPDATE_USER, NULL AS UPDATE_DATE, '' AS UPDATE_PC, ");
                                sb.Append(" D.HICCHAKUBI, D.KAISHUU_MONTH, D.KAISHUU_DAY, D.KAISHUU_HOUHOU, D.SHOSHIKI_MEISAI_KBN, D.SEIKYUU_KEITAI_KBN, D.NYUUKIN_MEISAI_KBN ");
                                sb.Append(" FROM M_GENBA A ");
                                sb.Append(" LEFT JOIN M_GYOUSHA B ON B.GYOUSHA_CD = A.GYOUSHA_CD AND B.DELETE_FLG = 0 ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI C ON C.TORIHIKISAKI_CD = A.TORIHIKISAKI_CD AND C.DELETE_FLG = 0 ");
                                sb.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU D ON D.TORIHIKISAKI_CD = C.TORIHIKISAKI_CD  ");
                                sb.Append(" WHERE A.DELETE_FLG = 0 ");

                                if (rakuKenkei == "1") sb.Append(" AND D.OUTPUT_KBN = 3 ");
                                if (rakuKenkei == "2") sb.Append(" AND D.OUTPUT_KBN IN (1, 2) AND D.TORIHIKI_KBN_CD = 2 ");

                                sb.Append(" AND NOT EXISTS (SELECT * FROM M_RAKU_RAKU WHERE DELETE_FLG = 0 AND SHOSHIKI_KBN = 3 AND ISNULL(TORIHIKISAKI_CD,'') = ISNULL(A.TORIHIKISAKI_CD,'') AND ISNULL(GYOUSHA_CD,'') = ISNULL(A.GYOUSHA_CD,'') AND ISNULL(GENBA_CD,'') = ISNULL(A.GENBA_CD,'')) ");
                                sb.Append(" ) M_RAKU ");
                                break;
                            }
                    }
                }
            }

            // パターンから作成したJOIN句
            sb.Append(this.form.JoinQuery);

            return sb.ToString();
        }

        /// <summary>
        /// 検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        public string CreateWhereQuery()
        {
            var result = new StringBuilder(256);
            string strTemp;
            string renkei = this.headerForm.RAKURAKU_MEISAI_RENKEI.Text;
            string shimebi = this.form.cb_shimebi.Text;

            if (!string.IsNullOrWhiteSpace(this._shoshiki))
            {
                switch (this._shoshiki)
                {
                    case "1":
                        {
                            result.Append(" AND ");
                            result.AppendFormat(" M_RAKU.SHOSHIKI_KBN = {0}", this._shoshiki); //→取引先入力ー請求書書式１ ＝　１

                            //if (renkei != string.Empty)
                            //{
                            //    result.Append(" AND ");
                            //    result.AppendFormat(" M_RAKU.RAKURAKU_MEISAI_KBN = {0}", renkei); //→取引先入力ー楽楽明細連携　＝　図①楽楽明細連携
                            //}

                            if (shimebi != string.Empty)
                            {
                                //→　取引先入力ー締日１＝図①締日 または 取引先入力ー締日2＝図①締日 または 取引先入力ー締日3＝図①締日
                                result.Append(" AND ");
                                result.Append("(");
                                result.AppendFormat(" M_RAKU.SHIMEBI1 = {0}", shimebi);
                                result.Append(" OR ");
                                result.AppendFormat(" M_RAKU.SHIMEBI2 = {0}", shimebi);
                                result.Append(" OR ");
                                result.AppendFormat(" M_RAKU.SHIMEBI3 = {0}", shimebi);
                                result.Append(")");
                            }

                            // 取引先CD
                            strTemp = this.form.TORIHIKISAKI_CD.Text;
                            if (!string.IsNullOrWhiteSpace(strTemp))
                            {
                                if (!string.IsNullOrWhiteSpace(result.ToString())) result.Append(" AND ");
                                result.AppendFormat(" M_RAKU.TORIHIKISAKI_CD = '{0}'", strTemp); //取引先入力ー取引先CD＝図①取引先CD
                            }
                            break;
                        }
                    case "2":
                        {
                            result.Append(" AND ");
                            result.AppendFormat(" M_RAKU.SHOSHIKI_KBN = {0}", this._shoshiki); //→取引先入力ー請求書書式１　＝　２

                            //if (renkei != string.Empty)
                            //{
                            //    result.Append(" AND ");
                            //    result.AppendFormat(" M_RAKU.RAKURAKU_MEISAI_KBN = {0}", renkei); //→業者入力ー楽楽明細連携　＝　図①楽楽明細連携
                            //}

                            if (shimebi != string.Empty)
                            {
                                //　　　→　取引先入力ー締日１　＝　図①締日　　　　または　　　取引先入力ー締日2　＝　図①締日　　　　または　　　取引先入力ー締日3　＝　図①締日
                                result.Append(" AND ");
                                result.Append("(");
                                result.AppendFormat(" M_RAKU.SHIMEBI1 = {0}", shimebi);
                                result.Append(" OR ");
                                result.AppendFormat(" M_RAKU.SHIMEBI2 = {0}", shimebi);
                                result.Append(" OR ");
                                result.AppendFormat(" M_RAKU.SHIMEBI3 = {0}", shimebi);
                                result.Append(")");
                            }

                            // 取引先CD
                            strTemp = this.form.TORIHIKISAKI_CD.Text;
                            if (!string.IsNullOrWhiteSpace(strTemp))
                            {
                                if (!string.IsNullOrWhiteSpace(result.ToString())) result.Append(" AND ");
                                result.AppendFormat(" M_RAKU.TORIHIKISAKI_CD = '{0}'", strTemp); // →　業者入力ー取引先CD　＝　図①取引先CD
                            }
                            // 業者CD
                            strTemp = this.form.GYOUSHA_CD.Text;
                            if (!string.IsNullOrWhiteSpace(strTemp))
                            {
                                if (!string.IsNullOrWhiteSpace(result.ToString())) result.Append(" AND ");
                                result.AppendFormat(" M_RAKU.GYOUSHA_CD = '{0}'", strTemp); //→　業者入力ー業者CD ＝　図①業者CD
                            }
                            break;
                        }

                    case "3":
                        {
                            result.Append(" AND ");
                            result.AppendFormat(" M_RAKU.SHOSHIKI_KBN = {0}", this._shoshiki); //→取引先入力ー請求書書式１　＝　3

                            //if (renkei != string.Empty)
                            //{
                            //    result.Append(" AND ");
                            //    result.AppendFormat(" M_RAKU.RAKURAKU_MEISAI_KBN = {0}", renkei); //→業者入力ー楽楽明細連携　＝　図①楽楽明細連携
                            //}

                            if (shimebi != string.Empty)
                            {
                                //→　取引先入力ー締日１　＝　図①締日　　　　または　　　取引先入力ー締日2　＝　図①締日　　　　または　　　取引先入力ー締日3　＝　図①締日
                                result.Append(" AND ");
                                result.Append("(");
                                result.AppendFormat(" M_RAKU.SHIMEBI1 = {0}", shimebi);
                                result.Append(" OR ");
                                result.AppendFormat(" M_RAKU.SHIMEBI2 = {0}", shimebi);
                                result.Append(" OR ");
                                result.AppendFormat(" M_RAKU.SHIMEBI3 = {0}", shimebi);
                                result.Append(")");
                            }

                            // 取引先CD
                            strTemp = this.form.TORIHIKISAKI_CD.Text;
                            if (!string.IsNullOrWhiteSpace(strTemp))
                            {
                                if (!string.IsNullOrWhiteSpace(result.ToString())) result.Append(" AND ");
                                result.AppendFormat(" M_RAKU.TORIHIKISAKI_CD = '{0}'", strTemp); //→　現場入力ー取引先CD　＝　図①取引先CD
                            }
                            // 業者CD
                            strTemp = this.form.GYOUSHA_CD.Text;
                            if (!string.IsNullOrWhiteSpace(strTemp))
                            {
                                if (!string.IsNullOrWhiteSpace(result.ToString())) result.Append(" AND ");
                                result.AppendFormat(" M_RAKU.GYOUSHA_CD = '{0}'", strTemp); //→現場入力ー業者CD　＝　図①業者CD
                            }
                            // 現場CD
                            strTemp = this.form.GENBA_CD.Text;
                            if (!string.IsNullOrWhiteSpace(strTemp))
                            {
                                if (!string.IsNullOrWhiteSpace(result.ToString())) result.Append(" AND ");
                                result.AppendFormat(" M_RAKU.GENBA_CD = '{0}'", strTemp); //→　現場入力ー現場CD　＝　図①現場CD
                            }
                            break;
                        }
                }
            }

            return result.Length > 0 ? result.Insert(0, " WHERE 1 = 1").ToString() : string.Empty;
        }

        /// <summary>
        /// OrderBy句作成
        /// </summary>
        /// <returns></returns>
        private string CreateOrderByQuery()
        {
            var query = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.form.OrderByQuery))
            {
                query += " ORDER BY " + this.form.OrderByQuery;
            }

            return query;
        }

        #endregion

        #region 表示条件
        /// <summary>
        /// 表示条件を次回呼出時のデフォルト値として保存します
        /// </summary>
        public void SaveHyoujiJoukenDefault()
        {
            LogUtility.DebugMethodStart();

            try
            {
                Properties.Settings.Default.TORIHIKISAKI_CD_TEXT = this.form.TORIHIKISAKI_CD.Text;
                Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                Properties.Settings.Default.GYOUSHA_CD_TEXT = this.form.GYOUSHA_CD.Text;
                this.form.beforGyoushaCD = this.form.GYOUSHA_CD.Text;
                Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT = this.form.GYOUSHA_RNAME.Text;
                Properties.Settings.Default.GENBA_CD_TEXT = this.form.GENBA_CD.Text;
                Properties.Settings.Default.GENBA_NAME_RYAKU_TEXT = this.form.GENBA_RNAME.Text;
                Properties.Settings.Default.CB_SHIMEBI = this.form.cb_shimebi.Text;
                Properties.Settings.Default.RAKURAKU_CSV_KBN = this.form.RAKURAKU_CSV_KBN.Text;
                Properties.Settings.Default.SEIKYUU_SHO_SHOSHIKI_1 = this.form.SEIKYUU_SHO_SHOSHIKI_1.Text;
                Properties.Settings.Default.RAKURAKU_MEISAI_RENKEI = this.headerForm.RAKURAKU_MEISAI_RENKEI.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("SaveHyoujiJouken", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        public void SetDataColumnsReadOnly()
        {
            List<string> listname = new List<string>();
            listname.Add(ConstansIC.RENKEI);
            listname.Add(ConstansIC.EMAIL);
            listname.Add(ConstansIC.EMAIL_ADDRESS1);
            listname.Add(ConstansIC.EMAIL_ADDRESS2);
            listname.Add(ConstansIC.EMAIL_ADDRESS3);
            foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
            {
                if (listname.Contains(column.Name)) column.ReadOnly = false;
                else column.ReadOnly = true;
            }
        }

        private bool CheckDataRadioButton()
        {
            ArrayList errColName = new ArrayList();
            bool isErr = false;
            // 必須入力チェック
            if (string.IsNullOrEmpty(this.headerForm.RAKURAKU_MEISAI_RENKEI.Text))
            {
                errColName.Add("楽楽明細連携");
                this.headerForm.RAKURAKU_MEISAI_RENKEI.BackColor = Constans.ERROR_COLOR;
                this.headerForm.RAKURAKU_MEISAI_RENKEI.ForeColor = Constans.ERROR_COLOR_FORE;
                isErr = true;
            }

            if (string.IsNullOrEmpty(this.form.RAKURAKU_CSV_KBN.Text))
            {
                errColName.Add("楽楽CSV");
                this.form.RAKURAKU_CSV_KBN.BackColor = Constans.ERROR_COLOR;
                this.form.RAKURAKU_CSV_KBN.ForeColor = Constans.ERROR_COLOR_FORE;
                isErr = true;
            }

            if (string.IsNullOrEmpty(this.form.SEIKYUU_SHO_SHOSHIKI_1.Text))
            {
                errColName.Add("請求書書式１");
                this.form.SEIKYUU_SHO_SHOSHIKI_1.BackColor = Constans.ERROR_COLOR;
                this.form.SEIKYUU_SHO_SHOSHIKI_1.ForeColor = Constans.ERROR_COLOR_FORE;
                isErr = true;
            }

            r_framework.Utility.MessageUtility messageUtility = new r_framework.Utility.MessageUtility();
            string message = messageUtility.GetMessage("E001").MESSAGE;

            string errMsg = "";
            if (isErr)
            {
                foreach (string colName in errColName)
                {
                    if (errMsg.Length > 0)
                    {
                        errMsg += "\n";
                    }
                    errMsg += message.Replace("{0}", colName);
                }
                MessageBox.Show(errMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.headerForm.RAKURAKU_MEISAI_RENKEI.BackColor = Constans.NOMAL_COLOR;
                this.form.RAKURAKU_CSV_KBN.BackColor = Constans.NOMAL_COLOR;
                this.form.SEIKYUU_SHO_SHOSHIKI_1.BackColor = Constans.NOMAL_COLOR;
                return false;
            }
            else
            {
                this.headerForm.RAKURAKU_MEISAI_RENKEI.BackColor = Constans.NOMAL_COLOR;
                this.form.RAKURAKU_CSV_KBN.BackColor = Constans.NOMAL_COLOR;
                this.form.SEIKYUU_SHO_SHOSHIKI_1.BackColor = Constans.NOMAL_COLOR;
                return true;
            }    
        }

        public bool CheckDataGridColumnName(string colname)
        {
            foreach(DataGridViewColumn col in this.form.customDataGridView1.Columns)
            {
                if (col.Name == colname) return true;
            }
            return false;
        }

        public bool CheckDataTableColumnName(string colname)
        {
            foreach (DataColumn column in this.form.Table.Columns)
            {
                if (column.ColumnName == colname) return true;
            }
            return false;
        }
    }
}
