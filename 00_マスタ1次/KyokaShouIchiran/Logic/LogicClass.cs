using System;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
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

namespace KyokaShouIchiran
{
   /// <summary>
   /// 
   /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        private readonly string ButtonInfoXmlPath = "KyokaShouIchiran.Setting.ButtonSetting.xml";
        internal HeaderForm headForm;
        private KyokaShouIchiranForm form;
        private IM_GYOUSHADao gyoushaDao;
        private IM_GENBADao genbaDao;
        private MessageBoxShowLogic msgLogic;
        internal readonly string HIDDEN_KYOKA_KBN = "HIDDEN_KYOKA_KBN";
        internal readonly string HIDDEN_GYOUSHA_CD = "HIDDEN_GYOUSHA_CD";
        internal readonly string HIDDEN_GENBA_CD = "HIDDEN_GENBA_CD";
        internal readonly string HIDDEN_CHIIKI_CD = "HIDDEN_CHIIKI_CD";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetForm"></param>
        public LogicClass(KyokaShouIchiranForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);
                this.form = targetForm;
                this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                this.msgLogic = new MessageBoxShowLogic();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("LogicClass", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                this.ButtonInit();
                this.EventInit();
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.Size = new System.Drawing.Size(997, 290);
                this.form.customDataGridView1.Location = new System.Drawing.Point(1, 160);
                this.form.customDataGridView1.TabIndex = 73;
                this.SetInit();
                this.LoadProperties();
            }
            catch (Exception ex)
            {
                ret = false;
                LogUtility.Error("WindowInit", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
                parentForm.bt_func1.Enabled = false;
                parentForm.bt_func2.Enabled = true;
                parentForm.bt_func3.Enabled = true;
                parentForm.bt_func4.Enabled = false;
                parentForm.bt_func5.Enabled = false;
                parentForm.bt_func6.Enabled = true;
                parentForm.bt_func7.Enabled = true;
                parentForm.bt_func8.Enabled = true;
                parentForm.bt_func9.Enabled = false;
                parentForm.bt_func10.Enabled = true;
                parentForm.bt_func11.Enabled = true;
                parentForm.bt_func12.Enabled = true;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("ButtonInit", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);
                parentForm.bt_func3.Click += new EventHandler(this.bt_func3_Click);           
                parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);             
                parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);     
                parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);      
                parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);   
                parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);   
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);   
                parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);          
                parentForm.FormClosing += new FormClosingEventHandler(parentForm_FormClosing);
                this.form.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("EventInit", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                int kyokaKbn = 0;
                LogUtility.DebugMethodStart(sender, e);
                switch (this.form.KYOKA_KBN.Text)
                {
                    case "2":
                        kyokaKbn = 2;
                        break;
                    case "3":
                        kyokaKbn = 3;
                        break;
                    default:
                        kyokaKbn = 1;
                        break;
                }
                r_framework.FormManager.FormManager.OpenFormWithAuth("M237", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kyokaKbn, string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func2_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.form.customDataGridView1 == null || this.form.customDataGridView1.Rows.Count == 0)
                {
                    this.msgLogic.MessageBoxShowError("対象データを選択してください");
                    return;
                }
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (row != null)
                {
                    SqlInt16 KYOKA_KBN = SqlInt16.Parse(row.Cells[HIDDEN_KYOKA_KBN].Value.ToString());
                    string GYOUSHA_CD = row.Cells[HIDDEN_GYOUSHA_CD].Value.ToString();
                    string GENBA_CD = row.Cells[HIDDEN_GENBA_CD].Value.ToString();
                    if (!r_framework.Authority.Manager.CheckAuthority("M237", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        if (!r_framework.Authority.Manager.CheckAuthority("M237", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            this.msgLogic.MessageBoxShow("E158", "修正");
                            return;
                        }
                    }
                    r_framework.FormManager.FormManager.OpenFormWithAuth("M237", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, KYOKA_KBN, GYOUSHA_CD, GENBA_CD);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func3_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    this.msgLogic.MessageBoxShowError("該当するデータが1件もありません。");
                }
                else
                {
                    if (this.msgLogic.MessageBoxShowConfirm("画面表示内容をCSV出力しますか。", MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        CSVExport csvExp = new CSVExport();
                        csvExp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.KYOKASHOU_ICHIRAN), this.form);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.SetInit();
                this.form.customSortHeader1.ClearCustomSortSetting();
                this.form.customSearchHeader1.ClearCustomSearchSetting();
                DataTable dtTmp = (DataTable)this.form.customDataGridView1.DataSource;
                if (dtTmp == null)
                {
                    return;
                }
                dtTmp.Clear();
                this.form.customDataGridView1.DataSource = dtTmp;
                this.form.KYOKA_KBN.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func7_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.form.PatternNo == 0 || string.IsNullOrEmpty(this.form.SelectQuery))
                {
                    this.msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }
                var parentForm = (BusinessBaseForm)this.form.Parent;
                bool catchErr = false;
                bool isErr = this.CheckDate(out catchErr);
                if (catchErr || isErr)
                {
                    return;
                }
                int cnt = this.Search();
                if (cnt == -1)
                {
                    return;
                }
                if (this.form.customDataGridView1 != null)
                {
                    this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headForm.ReadDataNumber.Text = "0";
                }
                if (this.headForm.ReadDataNumber.Text == "0")
                {
                    this.msgLogic.MessageBoxShow("C001");
                }
                this.SaveProperties();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func8_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.form.customSortHeader1.ShowCustomSortSettingDialog();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func10_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();
            if (this.form.customDataGridView1 != null)
            {
                this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headForm.ReadDataNumber.Text = "0";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func12_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var sysId = this.form.OpenPatternIchiran();
                if (!string.IsNullOrEmpty(sysId))
                {
                    this.form.SetPatternBySysId(sysId);
                    this.form.ShowData();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_MouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.form.customDataGridView1 == null || this.form.customDataGridView1.Rows.Count == 0)
                {
                    return;
                }
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (row != null)
                {
                    short kyokaKbn = short.Parse(row.Cells[HIDDEN_KYOKA_KBN].Value.ToString());
                    string gyoushaCd = row.Cells[HIDDEN_GYOUSHA_CD].Value.ToString();
                    string genbaCd = row.Cells[HIDDEN_GENBA_CD].Value.ToString();
                    if (!r_framework.Authority.Manager.CheckAuthority("M237", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        if (!r_framework.Authority.Manager.CheckAuthority("M237", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            this.msgLogic.MessageBoxShow("E158", "修正");
                            return;
                        }
                    }
                    r_framework.FormManager.FormManager.OpenFormWithAuth("M237", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kyokaKbn, gyoushaCd, genbaCd);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func3_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();
                LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath));
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("CreateButtonInfo", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.form.SelectQuery))
                {
                    return count;
                }
                var sql = this.GetSearchString();
                DialogResult result = DialogResult.Yes;
                this.form.Table = this.gyoushaDao.GetDateForStringSql(sql);
                int alertNumber = 0;
                if (!string.IsNullOrEmpty(this.headForm.alertNumber.Text))
                {
                    alertNumber = int.Parse(this.headForm.alertNumber.Text.Replace(",", ""));
                }
                if (alertNumber != 0 && alertNumber < this.form.Table.Rows.Count)
                {
                    result = this.msgLogic.MessageBoxShow("C025");
                }
                count = this.form.Table.Rows.Count;
                this.headForm.ReadDataNumber.Text = count.ToString();
                if (result == DialogResult.Yes)
                {
                    this.form.ShowData();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetSearchString()
        {
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT DISTINCT ");//CongBinh 20210621 #151966
            sql.AppendLine(this.form.SelectQuery);
            sql.AppendLine(" ,M.KYOKA_KBN AS HIDDEN_KYOKA_KBN ");
            sql.AppendLine(" ,M.GYOUSHA_CD AS HIDDEN_GYOUSHA_CD ");
            sql.AppendLine(" ,M.GENBA_CD AS HIDDEN_GENBA_CD ");
            sql.AppendLine(" ,M.CHIIKI_CD AS HIDDEN_CHIIKI_CD ");
            sql.AppendLine(" FROM M_CHIIKIBETSU_KYOKA M ");
            //CongBinh 20210628 #152181 S
            //sql.AppendLine(" LEFT JOIN (");
            //if ("9".Equals(this.form.FUTSU_KBN.Text) || "1".Equals(this.form.FUTSU_KBN.Text))
            //{
            //    sql.AppendLine(" SELECT * FROM M_CHIIKIBETSU_KYOKA_MEIGARA WHERE TOKUBETSU_KANRI_KBN = 0 ");
            //}
            //if ("9".Equals(this.form.FUTSU_KBN.Text))
            //{
            //    sql.AppendLine(" UNION ALL");
            //}
            //if ("9".Equals(this.form.FUTSU_KBN.Text) || "2".Equals(this.form.FUTSU_KBN.Text))
            //{
            //    sql.AppendLine(" SELECT * FROM M_CHIIKIBETSU_KYOKA_MEIGARA WHERE TOKUBETSU_KANRI_KBN = 1 ");
            //}
            //sql.AppendLine(") MOD  ");
            //sql.AppendLine(" ON M.KYOKA_KBN = MOD.KYOKA_KBN ");
            //sql.AppendLine(" AND M.GYOUSHA_CD = MOD.GYOUSHA_CD ");
            //sql.AppendLine(" AND M.GENBA_CD = MOD.GENBA_CD ");
            //sql.AppendLine(" AND M.CHIIKI_CD = MOD.CHIIKI_CD ");
            //sql.AppendLine(" LEFT JOIN M_HOUKOKUSHO_BUNRUI FUTSU_BUNRUI ON MOD.TOKUBETSU_KANRI_KBN = 0 AND MOD.HOUKOKUSHO_BUNRUI_CD = FUTSU_BUNRUI.HOUKOKUSHO_BUNRUI_CD ");
            //sql.AppendLine(" LEFT JOIN M_HOUKOKUSHO_BUNRUI TOKU_BUNRUI ON MOD.TOKUBETSU_KANRI_KBN = 1 AND MOD.HOUKOKUSHO_BUNRUI_CD = TOKU_BUNRUI.HOUKOKUSHO_BUNRUI_CD ");
            sql.AppendLine(" LEFT JOIN ");
            sql.AppendLine(" (SELECT DISTINCT ");
            sql.AppendLine(" TMP_AA.KYOKA_KBN, ");
            sql.AppendLine(" TMP_AA.GYOUSHA_CD, ");
            sql.AppendLine(" TMP_AA.GENBA_CD, ");
            sql.AppendLine(" TMP_AA.CHIIKI_CD, ");
            sql.AppendLine(" MC.FUTSUU_KYOKA_NO, ");
            sql.AppendLine(" HOUKOKUSHO_BUNRUI_NAME_RYAKU = STUFF( ");
            sql.AppendLine(" (SELECT ',' + TMP_B.HOUKOKUSHO_BUNRUI_NAME_RYAKU ");
            sql.AppendLine("    FROM M_CHIIKIBETSU_KYOKA M ");
            sql.AppendLine(" 	LEFT JOIN M_CHIIKIBETSU_KYOKA_MEIGARA TMP_A ");
            sql.AppendLine(" 	ON M.KYOKA_KBN = TMP_A.KYOKA_KBN ");
            sql.AppendLine(" 	AND M.GYOUSHA_CD = TMP_A.GYOUSHA_CD ");
            sql.AppendLine(" 	AND ISNULL( M.GENBA_CD,'') = ISNULL( TMP_A.GENBA_CD,'') ");
            sql.AppendLine(" 	AND M.CHIIKI_CD = TMP_A.CHIIKI_CD   ");
            sql.AppendLine(" 	LEFT JOIN M_HOUKOKUSHO_BUNRUI TMP_B  ");
            sql.AppendLine(" 	ON TMP_A.HOUKOKUSHO_BUNRUI_CD = TMP_B.HOUKOKUSHO_BUNRUI_CD ");
            sql.AppendLine(" 	WHERE TMP_B.DELETE_FLG = 0   ");
            sql.AppendLine(" 	AND TMP_A.TOKUBETSU_KANRI_KBN = TMP_AA.TOKUBETSU_KANRI_KBN 	 ");
            sql.AppendLine(" 	AND M.FUTSUU_KYOKA_NO = MC.FUTSUU_KYOKA_NO 				 ");
            sql.AppendLine(" 	ORDER BY  ");
            sql.AppendLine(" 	TMP_A.KYOKA_KBN,  ");
            sql.AppendLine(" 	TMP_A.GYOUSHA_CD,  ");
            sql.AppendLine(" 	TMP_A.GENBA_CD,  ");
            sql.AppendLine(" 	TMP_A.CHIIKI_CD ");
            sql.AppendLine(" 	FOR XML PATH ('')), 1, 1, '')  ");
            sql.AppendLine(" FROM ");
            sql.AppendLine(" M_CHIIKIBETSU_KYOKA MC ");
            sql.AppendLine(" LEFT JOIN M_CHIIKIBETSU_KYOKA_MEIGARA TMP_AA ");
            sql.AppendLine(" ON MC.KYOKA_KBN = TMP_AA.KYOKA_KBN ");
            sql.AppendLine(" AND MC.GYOUSHA_CD = TMP_AA.GYOUSHA_CD ");
            sql.AppendLine(" AND MC.GENBA_CD = TMP_AA.GENBA_CD ");
            sql.AppendLine(" AND MC.CHIIKI_CD = TMP_AA.CHIIKI_CD ");
            sql.AppendLine(" LEFT JOIN M_HOUKOKUSHO_BUNRUI TMP_BB ");
            sql.AppendLine(" ON TMP_AA.HOUKOKUSHO_BUNRUI_CD = TMP_BB.HOUKOKUSHO_BUNRUI_CD ");
            sql.AppendLine(" WHERE MC.DELETE_FLG = 0 ");
            sql.AppendLine(" AND TMP_AA.TOKUBETSU_KANRI_KBN = 0) FUTSUU ");
            sql.AppendLine(" ON M.KYOKA_KBN = FUTSUU.KYOKA_KBN ");
            sql.AppendLine(" AND M.GYOUSHA_CD = FUTSUU.GYOUSHA_CD  ");
            sql.AppendLine(" AND M.GENBA_CD = FUTSUU.GENBA_CD ");
            sql.AppendLine(" AND M.CHIIKI_CD = FUTSUU.CHIIKI_CD ");
            sql.AppendLine(" AND M.FUTSUU_KYOKA_NO = FUTSUU.FUTSUU_KYOKA_NO ");

            sql.AppendLine(" LEFT JOIN ");
            sql.AppendLine(" (SELECT DISTINCT ");
            sql.AppendLine(" TMP_AA.KYOKA_KBN, ");
            sql.AppendLine(" TMP_AA.GYOUSHA_CD, ");
            sql.AppendLine(" TMP_AA.GENBA_CD, ");
            sql.AppendLine(" TMP_AA.CHIIKI_CD, ");
            sql.AppendLine(" MC.TOKUBETSU_KYOKA_NO, ");
            sql.AppendLine(" HOUKOKUSHO_BUNRUI_NAME_RYAKU = STUFF( ");
            sql.AppendLine(" (SELECT ',' + TMP_B.HOUKOKUSHO_BUNRUI_NAME_RYAKU ");
            sql.AppendLine("    FROM M_CHIIKIBETSU_KYOKA M ");
            sql.AppendLine(" 	LEFT JOIN M_CHIIKIBETSU_KYOKA_MEIGARA TMP_A ");
            sql.AppendLine(" 	ON M.KYOKA_KBN = TMP_A.KYOKA_KBN ");
            sql.AppendLine(" 	AND M.GYOUSHA_CD = TMP_A.GYOUSHA_CD ");
            sql.AppendLine(" 	AND ISNULL( M.GENBA_CD,'') = ISNULL( TMP_A.GENBA_CD,'') ");
            sql.AppendLine(" 	AND M.CHIIKI_CD = TMP_A.CHIIKI_CD   ");
            sql.AppendLine(" 	LEFT JOIN M_HOUKOKUSHO_BUNRUI TMP_B  ");
            sql.AppendLine(" 	ON TMP_A.HOUKOKUSHO_BUNRUI_CD = TMP_B.HOUKOKUSHO_BUNRUI_CD ");
            sql.AppendLine(" 	WHERE TMP_B.DELETE_FLG = 0   ");
            sql.AppendLine(" 	AND TMP_A.TOKUBETSU_KANRI_KBN = TMP_AA.TOKUBETSU_KANRI_KBN 	 ");
            sql.AppendLine(" 	AND M.TOKUBETSU_KYOKA_NO = MC.TOKUBETSU_KYOKA_NO ");
            sql.AppendLine(" 	ORDER BY  ");
            sql.AppendLine(" 	TMP_A.KYOKA_KBN,  ");
            sql.AppendLine(" 	TMP_A.GYOUSHA_CD,  ");
            sql.AppendLine(" 	TMP_A.GENBA_CD,  ");
            sql.AppendLine(" 	TMP_A.CHIIKI_CD ");
            sql.AppendLine(" 	FOR XML PATH ('')), 1, 1, '')  ");
            sql.AppendLine(" FROM ");
            sql.AppendLine(" M_CHIIKIBETSU_KYOKA MC ");
            sql.AppendLine(" LEFT JOIN M_CHIIKIBETSU_KYOKA_MEIGARA TMP_AA ");
            sql.AppendLine(" ON MC.KYOKA_KBN = TMP_AA.KYOKA_KBN ");
            sql.AppendLine(" AND MC.GYOUSHA_CD = TMP_AA.GYOUSHA_CD ");
            sql.AppendLine(" AND MC.GENBA_CD = TMP_AA.GENBA_CD ");
            sql.AppendLine(" AND MC.CHIIKI_CD = TMP_AA.CHIIKI_CD ");
            sql.AppendLine(" LEFT JOIN M_HOUKOKUSHO_BUNRUI TMP_BB ");
            sql.AppendLine(" ON TMP_AA.HOUKOKUSHO_BUNRUI_CD = TMP_BB.HOUKOKUSHO_BUNRUI_CD ");
            sql.AppendLine(" WHERE MC.DELETE_FLG = 0 ");
            sql.AppendLine(" AND TMP_AA.TOKUBETSU_KANRI_KBN = 1) TOKUBETSU ");
            sql.AppendLine(" ON M.KYOKA_KBN = TOKUBETSU.KYOKA_KBN ");
            sql.AppendLine(" AND M.GYOUSHA_CD = TOKUBETSU.GYOUSHA_CD  ");
            sql.AppendLine(" AND M.GENBA_CD = TOKUBETSU.GENBA_CD ");
            sql.AppendLine(" AND M.CHIIKI_CD = TOKUBETSU.CHIIKI_CD ");
            sql.AppendLine(" AND M.TOKUBETSU_KYOKA_NO = TOKUBETSU.TOKUBETSU_KYOKA_NO ");
            if (!string.IsNullOrEmpty(this.form.HOUKOKU_SHO_BUNRUI_CD.Text))
            {
                sql.AppendLine(" LEFT JOIN ");
                sql.AppendLine(" (SELECT DISTINCT ");
                sql.AppendLine(" TMP_AA.KYOKA_KBN, ");
                sql.AppendLine(" TMP_AA.GYOUSHA_CD, ");
                sql.AppendLine(" TMP_AA.GENBA_CD, ");
                sql.AppendLine(" TMP_AA.CHIIKI_CD, ");
                sql.AppendLine(" MC.FUTSUU_KYOKA_NO, ");
                sql.AppendLine(" HOUKOKUSHO_BUNRUI_CD = STUFF( ");
                sql.AppendLine(" (SELECT ',' + TMP_A.HOUKOKUSHO_BUNRUI_CD ");
                sql.AppendLine("    FROM M_CHIIKIBETSU_KYOKA M ");
                sql.AppendLine(" 	LEFT JOIN M_CHIIKIBETSU_KYOKA_MEIGARA TMP_A ");
                sql.AppendLine(" 	ON M.KYOKA_KBN = TMP_A.KYOKA_KBN ");
                sql.AppendLine(" 	AND M.GYOUSHA_CD = TMP_A.GYOUSHA_CD ");
                sql.AppendLine(" 	AND ISNULL( M.GENBA_CD,'') = ISNULL( TMP_A.GENBA_CD,'') ");
                sql.AppendLine(" 	AND M.CHIIKI_CD = TMP_A.CHIIKI_CD   ");
                sql.AppendLine(" 	LEFT JOIN M_HOUKOKUSHO_BUNRUI TMP_B  ");
                sql.AppendLine(" 	ON TMP_A.HOUKOKUSHO_BUNRUI_CD = TMP_B.HOUKOKUSHO_BUNRUI_CD ");
                sql.AppendLine(" 	WHERE TMP_B.DELETE_FLG = 0   ");
                sql.AppendLine(" 	AND TMP_A.TOKUBETSU_KANRI_KBN = TMP_AA.TOKUBETSU_KANRI_KBN 	 ");
                sql.AppendLine(" 	AND M.FUTSUU_KYOKA_NO = MC.FUTSUU_KYOKA_NO 				 ");
                sql.AppendLine(" 	ORDER BY  ");
                sql.AppendLine(" 	TMP_A.KYOKA_KBN,  ");
                sql.AppendLine(" 	TMP_A.GYOUSHA_CD,  ");
                sql.AppendLine(" 	TMP_A.GENBA_CD,  ");
                sql.AppendLine(" 	TMP_A.CHIIKI_CD ");
                sql.AppendLine(" 	FOR XML PATH ('')), 1, 1, '')  ");
                sql.AppendLine(" FROM ");
                sql.AppendLine(" M_CHIIKIBETSU_KYOKA MC ");
                sql.AppendLine(" LEFT JOIN M_CHIIKIBETSU_KYOKA_MEIGARA TMP_AA ");
                sql.AppendLine(" ON MC.KYOKA_KBN = TMP_AA.KYOKA_KBN ");
                sql.AppendLine(" AND MC.GYOUSHA_CD = TMP_AA.GYOUSHA_CD ");
                sql.AppendLine(" AND MC.GENBA_CD = TMP_AA.GENBA_CD ");
                sql.AppendLine(" AND MC.CHIIKI_CD = TMP_AA.CHIIKI_CD ");
                sql.AppendLine(" LEFT JOIN M_HOUKOKUSHO_BUNRUI TMP_BB ");
                sql.AppendLine(" ON TMP_AA.HOUKOKUSHO_BUNRUI_CD = TMP_BB.HOUKOKUSHO_BUNRUI_CD ");
                sql.AppendLine(" WHERE MC.DELETE_FLG = 0 ");
                sql.AppendLine(" AND TMP_AA.TOKUBETSU_KANRI_KBN = 0) FUTSUU_CD ");
                sql.AppendLine(" ON M.KYOKA_KBN = FUTSUU_CD.KYOKA_KBN ");
                sql.AppendLine(" AND M.GYOUSHA_CD = FUTSUU_CD.GYOUSHA_CD  ");
                sql.AppendLine(" AND M.GENBA_CD = FUTSUU_CD.GENBA_CD ");
                sql.AppendLine(" AND M.CHIIKI_CD = FUTSUU_CD.CHIIKI_CD ");
                sql.AppendLine(" AND M.FUTSUU_KYOKA_NO = FUTSUU_CD.FUTSUU_KYOKA_NO ");


                sql.AppendLine(" LEFT JOIN ");
                sql.AppendLine(" (SELECT DISTINCT ");
                sql.AppendLine(" TMP_AA.KYOKA_KBN, ");
                sql.AppendLine(" TMP_AA.GYOUSHA_CD, ");
                sql.AppendLine(" TMP_AA.GENBA_CD, ");
                sql.AppendLine(" TMP_AA.CHIIKI_CD, ");
                sql.AppendLine(" MC.TOKUBETSU_KYOKA_NO, ");
                sql.AppendLine(" HOUKOKUSHO_BUNRUI_CD = STUFF( ");
                sql.AppendLine(" (SELECT ',' + TMP_A.HOUKOKUSHO_BUNRUI_CD ");
                sql.AppendLine("    FROM M_CHIIKIBETSU_KYOKA M ");
                sql.AppendLine(" 	LEFT JOIN M_CHIIKIBETSU_KYOKA_MEIGARA TMP_A ");
                sql.AppendLine(" 	ON M.KYOKA_KBN = TMP_A.KYOKA_KBN ");
                sql.AppendLine(" 	AND M.GYOUSHA_CD = TMP_A.GYOUSHA_CD ");
                sql.AppendLine(" 	AND ISNULL( M.GENBA_CD,'') = ISNULL( TMP_A.GENBA_CD,'') ");
                sql.AppendLine(" 	AND M.CHIIKI_CD = TMP_A.CHIIKI_CD   ");
                sql.AppendLine(" 	LEFT JOIN M_HOUKOKUSHO_BUNRUI TMP_B  ");
                sql.AppendLine(" 	ON TMP_A.HOUKOKUSHO_BUNRUI_CD = TMP_B.HOUKOKUSHO_BUNRUI_CD ");
                sql.AppendLine(" 	WHERE TMP_B.DELETE_FLG = 0   ");
                sql.AppendLine(" 	AND TMP_A.TOKUBETSU_KANRI_KBN = TMP_AA.TOKUBETSU_KANRI_KBN 	 ");
                sql.AppendLine(" 	AND M.TOKUBETSU_KYOKA_NO = MC.TOKUBETSU_KYOKA_NO ");
                sql.AppendLine(" 	ORDER BY  ");
                sql.AppendLine(" 	TMP_A.KYOKA_KBN,  ");
                sql.AppendLine(" 	TMP_A.GYOUSHA_CD,  ");
                sql.AppendLine(" 	TMP_A.GENBA_CD,  ");
                sql.AppendLine(" 	TMP_A.CHIIKI_CD ");
                sql.AppendLine(" 	FOR XML PATH ('')), 1, 1, '')  ");
                sql.AppendLine(" FROM ");
                sql.AppendLine(" M_CHIIKIBETSU_KYOKA MC ");
                sql.AppendLine(" LEFT JOIN M_CHIIKIBETSU_KYOKA_MEIGARA TMP_AA ");
                sql.AppendLine(" ON MC.KYOKA_KBN = TMP_AA.KYOKA_KBN ");
                sql.AppendLine(" AND MC.GYOUSHA_CD = TMP_AA.GYOUSHA_CD ");
                sql.AppendLine(" AND MC.GENBA_CD = TMP_AA.GENBA_CD ");
                sql.AppendLine(" AND MC.CHIIKI_CD = TMP_AA.CHIIKI_CD ");
                sql.AppendLine(" LEFT JOIN M_HOUKOKUSHO_BUNRUI TMP_BB ");
                sql.AppendLine(" ON TMP_AA.HOUKOKUSHO_BUNRUI_CD = TMP_BB.HOUKOKUSHO_BUNRUI_CD ");
                sql.AppendLine(" WHERE MC.DELETE_FLG = 0 ");
                sql.AppendLine(" AND TMP_AA.TOKUBETSU_KANRI_KBN = 1) TOKUBETSU_CD ");
                sql.AppendLine(" ON M.KYOKA_KBN = TOKUBETSU_CD.KYOKA_KBN ");
                sql.AppendLine(" AND M.GYOUSHA_CD = TOKUBETSU_CD.GYOUSHA_CD  ");
                sql.AppendLine(" AND M.GENBA_CD = TOKUBETSU_CD.GENBA_CD ");
                sql.AppendLine(" AND M.CHIIKI_CD = TOKUBETSU_CD.CHIIKI_CD ");
                sql.AppendLine(" AND M.TOKUBETSU_KYOKA_NO = TOKUBETSU_CD.TOKUBETSU_KYOKA_NO ");
            }
            //CongBinh 20210628 #152181 E
            sql.AppendLine(" LEFT JOIN M_GYOUSHA ON M_GYOUSHA.GYOUSHA_CD = M.GYOUSHA_CD ");
            sql.AppendLine(" LEFT JOIN M_GENBA ON M_GENBA.GENBA_CD = M.GENBA_CD AND M_GENBA.GYOUSHA_CD = M.GYOUSHA_CD ");
            sql.AppendLine(" LEFT JOIN M_CHIIKI ON M_CHIIKI.CHIIKI_CD = M.CHIIKI_CD ");
            sql.AppendLine(" WHERE 1 = 1 ");

            #region  KYOKA_KBN
            if ("1".Equals(this.form.KYOKA_KBN.Text))
            {
                sql.AppendLine(" AND M.KYOKA_KBN = 1 ");
            }
            else if ("2".Equals(this.form.KYOKA_KBN.Text))
            {
                sql.AppendLine(" AND M.KYOKA_KBN = 2 ");
            }
            else if ("3".Equals(this.form.KYOKA_KBN.Text))
            {
                sql.AppendLine(" AND M.KYOKA_KBN = 3 ");
            }
            #endregion

            #region  FUTSU_KBN
            if ("9".Equals(this.form.FUTSU_KBN.Text))
            {
                if ("1".Equals(this.form.KIKAN_KBN.Text))
                {
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                    {
                        if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                        {
                            sql.AppendLine(" AND (( M.FUTSUU_KYOKA_BEGIN >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                            sql.AppendLine(" AND M.FUTSUU_KYOKA_BEGIN <= '" + this.form.HIDUKE_TO.Value.ToString() + "')");
                            sql.AppendLine(" OR ( M.TOKUBETSU_KYOKA_BEGIN >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                            sql.AppendLine(" AND M.TOKUBETSU_KYOKA_BEGIN <= '" + this.form.HIDUKE_TO.Value.ToString() + "'))");
                        }
                        else
                        {
                            sql.AppendLine(" AND ( M.FUTSUU_KYOKA_BEGIN >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                            sql.AppendLine(" OR  M.TOKUBETSU_KYOKA_BEGIN >= '" + this.form.HIDUKE_FROM.Value.ToString() + "')");
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                        {
                            sql.AppendLine(" AND ( M.FUTSUU_KYOKA_BEGIN <= '" + this.form.HIDUKE_TO.Value.ToString() + "'");
                            sql.AppendLine(" OR M.TOKUBETSU_KYOKA_BEGIN <= '" + this.form.HIDUKE_TO.Value.ToString() + "')");
                        }
                    }
                }
                else if ("2".Equals(this.form.KIKAN_KBN.Text))
                {
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                    {
                        if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                        {
                            sql.AppendLine(" AND (( M.FUTSUU_KYOKA_END >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                            sql.AppendLine(" AND M.FUTSUU_KYOKA_END <= '" + this.form.HIDUKE_TO.Value.ToString() + "')");
                            sql.AppendLine(" OR ( M.TOKUBETSU_KYOKA_END >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                            sql.AppendLine(" AND M.TOKUBETSU_KYOKA_END <= '" + this.form.HIDUKE_TO.Value.ToString() + "'))");
                        }
                        else
                        {
                            sql.AppendLine(" AND ( M.FUTSUU_KYOKA_END >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                            sql.AppendLine(" OR  M.TOKUBETSU_KYOKA_END >= '" + this.form.HIDUKE_FROM.Value.ToString() + "')");
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                        {
                            sql.AppendLine(" AND ( M.FUTSUU_KYOKA_END <= '" + this.form.HIDUKE_TO.Value.ToString() + "'");
                            sql.AppendLine(" OR M.TOKUBETSU_KYOKA_END <= '" + this.form.HIDUKE_TO.Value.ToString() + "')");
                        }
                    }
                }
                if (!string.IsNullOrEmpty(this.form.KYOKA_NO.Text))
                {
                    sql.AppendLine(" AND (M.FUTSUU_KYOKA_NO = '" + this.form.KYOKA_NO.Text + "'");
                    sql.AppendLine(" OR M.TOKUBETSU_KYOKA_NO = '" + this.form.KYOKA_NO.Text + "')");
                }
                if (!string.IsNullOrEmpty(this.form.HOUKOKU_SHO_BUNRUI_CD.Text))
                {
                    //CongBinh 20210628 #152181 S
                    //sql.AppendLine(" AND ((MOD.HOUKOKUSHO_BUNRUI_CD = '" + this.form.HOUKOKU_SHO_BUNRUI_CD.Text + "'");
                    //sql.AppendLine(" AND MOD.TOKUBETSU_KANRI_KBN = 0 )");
                    //sql.AppendLine(" OR (MOD.HOUKOKUSHO_BUNRUI_CD = '" + this.form.HOUKOKU_SHO_BUNRUI_CD.Text + "'");
                    //sql.AppendLine(" AND MOD.TOKUBETSU_KANRI_KBN = 1 ))");
                    sql.AppendLine(" AND (FUTSUU_CD.HOUKOKUSHO_BUNRUI_CD like '%" + this.form.HOUKOKU_SHO_BUNRUI_CD.Text + "%' ");
                    sql.AppendLine("  OR  TOKUBETSU_CD.HOUKOKUSHO_BUNRUI_CD like '%" + this.form.HOUKOKU_SHO_BUNRUI_CD.Text + "%' )");
                    //CongBinh 20210628 #152181 S
                }
            }
            else if ("1".Equals(this.form.FUTSU_KBN.Text))
            {
                if ("1".Equals(this.form.KIKAN_KBN.Text))
                {
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                    {
                        sql.AppendLine(" AND M.FUTSUU_KYOKA_BEGIN >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                    }
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                    {
                        sql.AppendLine(" AND M.FUTSUU_KYOKA_BEGIN <= '" + this.form.HIDUKE_TO.Value.ToString() + "'");
                    }
                }
                else if ("2".Equals(this.form.KIKAN_KBN.Text))
                {
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                    {
                        sql.AppendLine(" AND M.FUTSUU_KYOKA_END >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                    }
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                    {
                        sql.AppendLine(" AND M.FUTSUU_KYOKA_END <= '" + this.form.HIDUKE_TO.Value.ToString() + "'");
                    }
                }
                if (!string.IsNullOrEmpty(this.form.KYOKA_NO.Text))
                {
                    sql.AppendLine(" AND M.FUTSUU_KYOKA_NO = '" + this.form.KYOKA_NO.Text + "'");
                }
                if (!string.IsNullOrEmpty(this.form.HOUKOKU_SHO_BUNRUI_CD.Text))
                {
                    sql.AppendLine(" AND FUTSUU_CD.HOUKOKUSHO_BUNRUI_CD like '%" + this.form.HOUKOKU_SHO_BUNRUI_CD.Text + "%' ");
                }
            }
            else if ("2".Equals(this.form.FUTSU_KBN.Text))
            {
                if ("1".Equals(this.form.KIKAN_KBN.Text))
                {
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                    {
                        sql.AppendLine(" AND M.TOKUBETSU_KYOKA_BEGIN >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                    }
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                    {
                        sql.AppendLine(" AND M.TOKUBETSU_KYOKA_BEGIN <= '" + this.form.HIDUKE_TO.Value.ToString() + "'");
                    }
                }
                else if ("2".Equals(this.form.KIKAN_KBN.Text))
                {
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                    {
                        sql.AppendLine(" AND M.TOKUBETSU_KYOKA_END >= '" + this.form.HIDUKE_FROM.Value.ToString() + "'");
                    }
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                    {
                        sql.AppendLine(" AND M.TOKUBETSU_KYOKA_END <= '" + this.form.HIDUKE_TO.Value.ToString() + "'");
                    }
                }
                if (!string.IsNullOrEmpty(this.form.KYOKA_NO.Text))
                {
                    sql.AppendLine(" AND M.TOKUBETSU_KYOKA_NO = '" + this.form.KYOKA_NO.Text + "'");
                }
                if (!string.IsNullOrEmpty(this.form.HOUKOKU_SHO_BUNRUI_CD.Text))
                {
                    sql.AppendLine(" AND TOKUBETSU_CD.HOUKOKUSHO_BUNRUI_CD like '%" + this.form.HOUKOKU_SHO_BUNRUI_CD.Text + "%' ");
                }
            }
            #endregion

            #region GYOUSHA_CD GENBA_CD CHIIKI_CD
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                sql.AppendLine(" AND M.GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "'");
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                if ("9".Equals(this.form.KYOKA_KBN.Text))
                {
                    sql.AppendLine(" AND (M.GENBA_CD = '" + this.form.GENBA_CD.Text + "'");
                    sql.AppendLine(" OR (ISNULL(M.GENBA_CD, '') = '' AND M.KYOKA_KBN = 1) )");
                }
                else
                {
                    sql.AppendLine(" AND M.GENBA_CD = '" + this.form.GENBA_CD.Text + "'");
                }
            }
            if (!string.IsNullOrEmpty(this.form.CHIIKI_CD.Text))
            {
                sql.AppendLine(" AND M.CHIIKI_CD = '" + this.form.CHIIKI_CD.Text + "'");
            }
            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                sql.AppendLine(" AND M.DELETE_FLG = 0 ");
            }
            #endregion
            //CongBinh 20210621 #151966 S
            if ("1".Equals(this.form.FUTSU_KBN.Text))
            {
                sql.AppendLine(" AND ISNULL(M.FUTSUU_KYOKA_NO , '') <> '' ");
            }
            if ("2".Equals(this.form.FUTSU_KBN.Text))
            {
                sql.AppendLine(" AND ISNULL(M.TOKUBETSU_KYOKA_NO , '') <> '' ");
            }
            //CongBinh 20210621 #151966 E
            #region ORDER BY
            if (!string.IsNullOrEmpty(this.form.OrderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.form.OrderByQuery);
            }
            #endregion
            return sql.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal bool CheckDate(out bool catchErr)
        {
            catchErr = false;
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();
                var allControlAndHeaderControls = this.form.allControl.ToList();
                allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.headForm));
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                if (this.form.RegistErrorFlag)
                {
                    LogUtility.DebugMethodEnd(isErr, catchErr);
                    return true;
                }
                if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text.Trim()) && !string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text.Trim()))
                {
                    if (this.form.HIDUKE_TO.Text.Trim().CompareTo(this.form.HIDUKE_FROM.Text.Trim()) < 0)
                    {
                        this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                        this.form.HIDUKE_TO.IsInputErrorOccured = true;
                        this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                        this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                        string[] errorMsg = { "有効期限From", "有効期限To" };
                        this.msgLogic.MessageBoxShow("E030", errorMsg);
                        this.form.HIDUKE_FROM.Focus();
                        LogUtility.DebugMethodEnd(isErr, catchErr);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("CheckDate", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr, catchErr);
            }
            return isErr;
        }
        /// <summary>
        /// 
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hs"></param>
        public void SetHeader(HeaderForm hs)
        {
            try
            {
                this.headForm = hs;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("SetHeader", ex);
                throw ex;
            }
        }

        #region Equals/GetHashCode/ToString
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        void LoadProperties()
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.KYOKA_KBN))
            {
                this.form.KYOKA_KBN.Text = Properties.Settings.Default.KYOKA_KBN;
            }
            if (!String.IsNullOrEmpty(Properties.Settings.Default.FUTSU_KBN))
            {
                this.form.FUTSU_KBN.Text = Properties.Settings.Default.FUTSU_KBN;
            }
            if (!String.IsNullOrEmpty(Properties.Settings.Default.KIKAN_KBN))
            {
                this.form.KIKAN_KBN.Text = Properties.Settings.Default.KIKAN_KBN;
            }
            if (!String.IsNullOrEmpty(Properties.Settings.Default.HIDUKE_FROM))
            {
                this.form.HIDUKE_FROM.Text = Properties.Settings.Default.HIDUKE_FROM;
            }
            if (!String.IsNullOrEmpty(Properties.Settings.Default.HIDUKE_TO))
            {
                this.form.HIDUKE_TO.Text = Properties.Settings.Default.HIDUKE_TO;
            }
            if (!String.IsNullOrEmpty(Properties.Settings.Default.KYOKA_NO))
            {
                this.form.KYOKA_NO.Text = Properties.Settings.Default.KYOKA_NO;
            }
            if (!String.IsNullOrEmpty(Properties.Settings.Default.GYOUSHA_CD))
            {
                this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GYOUSHA_CD;
                var gyousha = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                if (gyousha != null)
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
            }
            if (!String.IsNullOrEmpty(Properties.Settings.Default.GENBA_CD))
            {
                this.form.GENBA_CD.Text = Properties.Settings.Default.GENBA_CD;
                var genba = this.genbaDao.GetDataByCd(new M_GENBA() { GYOUSHA_CD = this.form.GYOUSHA_CD.Text, GENBA_CD = this.form.GENBA_CD.Text });
                if (genba != null)
                {
                    this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                }
            }
            if (!String.IsNullOrEmpty(Properties.Settings.Default.CHIIKI_CD))
            {
                this.form.CHIIKI_CD.Text = Properties.Settings.Default.CHIIKI_CD;
                var chiiki = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetDataByCd(this.form.CHIIKI_CD.Text);
                if (chiiki != null)
                {
                    this.form.CHIIKI_NAME.Text = chiiki.CHIIKI_NAME_RYAKU;
                }
            }
            if (!String.IsNullOrEmpty(Properties.Settings.Default.HOUKOKU_SHOU_BUNRUI_CD))
            {
                this.form.HOUKOKU_SHO_BUNRUI_CD.Text = Properties.Settings.Default.HOUKOKU_SHOU_BUNRUI_CD;
                var houkou = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>().GetDataByCd(this.form.HOUKOKU_SHO_BUNRUI_CD.Text);
                if (houkou != null)
                {
                    this.form.HOUKOKU_SHO_BUNRUI_NAME.Text = houkou.HOUKOKUSHO_BUNRUI_NAME_RYAKU;
                }
            }
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = bool.Parse(Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void parentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveProperties();
        }
        /// <summary>
        /// 
        /// </summary>
        internal void SaveProperties()
        {
            Properties.Settings.Default.KYOKA_KBN = this.form.KYOKA_KBN.Text;
            Properties.Settings.Default.FUTSU_KBN = this.form.FUTSU_KBN.Text;
            Properties.Settings.Default.KIKAN_KBN = this.form.KIKAN_KBN.Text;
            Properties.Settings.Default.HIDUKE_FROM = this.form.HIDUKE_FROM.Text;
            Properties.Settings.Default.HIDUKE_TO = this.form.HIDUKE_TO.Text;
            Properties.Settings.Default.KYOKA_NO = this.form.KYOKA_NO.Text;
            Properties.Settings.Default.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            Properties.Settings.Default.GENBA_CD = this.form.GENBA_CD.Text;
            Properties.Settings.Default.CHIIKI_CD = this.form.CHIIKI_CD.Text;
            Properties.Settings.Default.HOUKOKU_SHOU_BUNRUI_CD = this.form.HOUKOKU_SHO_BUNRUI_CD.Text;
            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked.ToString();
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// 
        /// </summary>
        internal void SetInit()
        {
            this.headForm.ReadDataNumber.Text = "0";
            this.headForm.alertNumber.Text = "0";
            this.form.KYOKA_KBN.Text = "9";
            this.form.FUTSU_KBN.Text = "9";
            this.form.KIKAN_KBN.Text = "2";
            this.form.HIDUKE_FROM.Text = string.Empty;
            this.form.HIDUKE_TO.Text = string.Empty;
            this.form.KYOKA_NO.Text = string.Empty;
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.CHIIKI_CD.Text = string.Empty;
            this.form.CHIIKI_NAME.Text = string.Empty;
            this.form.HOUKOKU_SHO_BUNRUI_CD.Text = string.Empty;
            this.form.HOUKOKU_SHO_BUNRUI_NAME.Text = string.Empty;
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool CheckGyoushaCd()
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    return false;
                }
                var gyousha = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                if (gyousha != null)
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    return true;
                }
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGyoushaCd", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool CheckGenbaCd()
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.msgLogic.MessageBoxShow("E051", "業者");
                    return true;
                }
                else
                {
                    var genba = this.genbaDao.GetDataByCd(new M_GENBA() { GYOUSHA_CD = this.form.GYOUSHA_CD.Text, GENBA_CD = this.form.GENBA_CD.Text });
                    if (genba == null)
                    {
                        this.msgLogic.MessageBoxShow("E020", "現場");
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        return true;
                    }
                    else
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                    }
                }
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenbaCd", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenbaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
    }
}