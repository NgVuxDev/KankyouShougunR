using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using System.Data;
using Shougun.Function.ShougunCSCommon.Utility;
using CommonChouhyouPopup.App;
using Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Drawing;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PayByProxy.DainoMeisaihyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class DainoMeisaihyoLogic : IBuisinessLogic
    {
        #region Variable
        private GcCustomMultiRow GridMultiRowResult;
        /// <summary>
        /// xml setting for config footer button
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PayByProxy.DainoMeisaihyo.Setting.ButtonSetting.xml";
        /// <summary>
        /// DTO
        /// </summary>
        private DainoMeisaihyoDTO dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        //private DainoMeisaihyoDAO dao;
        private UIHeader headerForm;
        private DBAccessor CommonDBAccessor;
        //for popup return
        private ShukeiHyoJokenDTO entityDTO = new ShukeiHyoJokenDTO();
        private DataTable tableResult = new DataTable();
        private Dictionary<string, DataTable> DicR403;
        internal MessageBoxShowLogic errmessage;
        //end for popup return

        /// <summary>
        /// Array name of cell in grid
        /// </summary>
        private string[] sHeadEntry = new string[]{"GYOUSHA_CD_IN","GYOUSHA_NAME_IN","GYOUSHA_CD_EX","GYOUSHA_NAME_EX","TORIHIKISAKI_CD_IN",
                "TORIHIKISAKI_NAME_IN","TORIHIKISAKI_CD_EX","TORIHIKISAKI_NAME_EX","GENBA_CD_IN","GENBA_NAME_IN","GENBA_CD_EX","GENBA_NAME_EX",
                "UNPAN_GYOUSHA_CD","UNPAN_GYOUSHA_NAME","KINGAKU_TRAN","entry_temp1","entry_temp2","entry_temp3","entry_temp4","entry_temp5"};

        private string[] sDetail = new string[] { "DENPYOU_DATE","ROW_NO","HINMEI_CD_IN","HINMEI_NAME_IN","NET_JYUURYOU_IN","SUURYOU_IN","UNIT_NAME_RYAKU_IN",
                "TANKA_IN","KINGAKU_IN","MEISAI_BIKOU_IN","DAINOU_NUMBER","HINMEI_CD_EX","HINMEI_NAME_EX","NET_JYUURYOU_EX","SUURYOU_EX","UNIT_NAME_RYAKU_EX",
                "TANKA_EX","KINGAKU_EX","SAEKI_KINGAKU","MEISAI_BIKOU_EX","detail_temp1","detail_temp2","detail_temp3","detail_temp4" };

        private string[] sTotalOne = new string[] {"lblTotal_ONE_IN", "TOTAL_NET_JYUURYOU_IN","TOTAL_KINGAKU_IN","lblTotal_ONE_EX",
            "TOTAL_NET_JYUURYOU_EX","TOTAL_KINGAKU_EX","TOTAL_SAEKI_KINGAKU","TOTAL_KINGAKU_TRAN_ONE","TOTAL_ONE_TEMP1","TOTAL_ONE_TEMP2","TOTAL_ONE_TEMP3"
            ,"TOTAL_ONE_TEMP4","TOTAL_ONE_TEMP5","TOTAL_ONE_TEMP6","TOTAL_ONE_TEMP7","TOTAL_ONE_TEMP8"};

        string[] sTotalAll = new string[] { "lblTotal_IN_ALL", "TOTAL_NET_JYUURYOU_IN_ALL", "TOTAL_KINGAKU_IN_ALL", "lblTotal_EX_ALL",
            "TOTAL_NET_JYUURYOU_EX_ALL","TOTAL_KINGAKU_EX_ALL","TOTAL_SAEKI_KINGAKU_ALL","TOTAL_KINGAKU_TRAN_ALL", "Total_ALL_TEMP1", "Total_ALL_TEMP2", "Total_ALL_TEMP3", "Total_ALL_TEMP4", 
            "Total_ALL_TEMP5", "Total_ALL_TEMP6", "Total_ALL_TEMP7", "Total_ALL_TEMP8"};
        #endregion
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DainoMeisaihyoLogic(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DainoMeisaihyoDTO();
            //this.dao = DaoInitUtility.GetComponent<DainoMeisaihyoDAO>();
            this.GridMultiRowResult = new GcCustomMultiRow();
            this.GridMultiRowResult = this.form.gcCustomMultiRowResult;
            this.CommonDBAccessor = new DBAccessor();
            LogUtility.DebugMethodEnd();
        }
        #region Auto Create Method
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
        #endregion
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();
                this.ButtonInit();
                this.EventInit();
                this.SetDefaultSearchConditionValues();
                this.GetSysInfo();
                this.SetEnabled();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #region ボタン活性/非活性
        /// <summary>
        //ボタン活性/非活性
        /// </summary>
        /// <returns></returns>
        private void SetEnabled()
        {
            // 検索実行が行われている？
            if (this.DicR403 != null)
            {
                // 印刷ボタン(F5)活性化
                this.parentForm.bt_func5.Enabled = true;
                // CSVボタン(F6)活性化
                this.parentForm.bt_func6.Enabled = true;

            }
            else
            {
                // 印刷ボタン(F5)非活性化
                this.parentForm.bt_func5.Enabled = false;
                // CSVボタン(F6)非活性化
                this.parentForm.bt_func6.Enabled = false;
            }

        }
        #endregion
        ///<summary>
        ///load InitializeComponent
        /// </summary>
        private void InitializeComponent()
        {
            parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.ProcessButtonPanel.Visible = false;
            this.headerForm = (UIHeader)parentForm.headerForm;

        }

        /// <summary>
        /// 
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            ////Functionボタンのイベント生成
            parentForm.bt_func5.Click += new EventHandler(bt_func5_Click);
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);
            //GridMultiRowResult.CellDoubleClick += new EventHandler<CellEventArgs>(gcCustomMultiRowResult_CellDoubleClick);
            //GridMultiRowResult.CellClick += new EventHandler<CellEventArgs>(gcCustomMultiRowResult_CellClick);
            this.headerForm.ctxt_AlertNumber.Leave += new EventHandler(ctxt_AlertNumber_Leave);
            ////年度変更イベント           
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// Get alert number from sysinfo
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO sysInfo = new M_SYS_INFO();
            sysInfo = CommonDBAccessor.GetSysInfo();
            if (sysInfo != null)
                this.headerForm.ctxt_AlertNumber.Text = CommonCalc.DecimalFormat(Convert.ToDecimal(sysInfo.ICHIRAN_ALERT_KENSUU.ToString()));
            else
                this.headerForm.ctxt_AlertNumber.Text = "1";
        }
        private void GetReturnResult(ShukeiHyoJokenDTO rentityDTO, DataTable rtableResult, Dictionary<string, DataTable> rDicR308)
        {
            this.entityDTO = rentityDTO;
            this.tableResult = rtableResult;
            this.DicR403 = rDicR308;
        }
        public bool CallPopup()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                Decimal alertNumber = this.headerForm.ctxt_AlertNumber.Text == string.Empty ? 0 : Convert.ToDecimal(this.headerForm.ctxt_AlertNumber.Text.Replace(",", ""));

                var callHeader = new ShukeiHyoJokenShiteiPoppup.UIHeader();
                var callForm = new ShukeiHyoJokenShiteiPoppupForm(callHeader, this.form.WindowId, alertNumber, this.GetReturnResult);
                var businessForm = new BasePopForm(callForm, callHeader);
                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    // 画面表示位置を設定（親フォーム中央）
                    businessForm.StartPosition = FormStartPosition.Manual;
                    int parentFormHeight, parentFormWidth;
                    // 親フォームの高さ
                    parentFormHeight = parentForm.Height;
                    // 親フォームの幅
                    parentFormWidth = parentForm.Width;

                    //フォーム位置を中央に設定
                    businessForm.Location = new Point(parentForm.Left + (parentFormWidth - 720) / 2, parentForm.Top + (parentFormHeight - 460) / 2);

                    //businessForm.StartPosition = FormStartPosition.CenterScreen;
                    var dr = businessForm.ShowDialog();
                    //reset
                    this.SetDefaultSearchConditionValues();
                    this.GridMultiRowResult.Rows.Clear();
                    //new data
                    if (tableResult.Rows.Count > 0)
                    {
                        this.BindData(this.tableResult);
                        if (entityDTO != null)
                            this.SetSearchCondition(entityDTO);
                    }
                    //end new data
                }
                this.SetEnabled();
                businessForm.Dispose();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;

        }
        private void SetDefaultSearchConditionValues()
        {
            LogUtility.DebugMethodStart();
            this.SetReadOnlyHeaderControl();
            headerForm.dtpDENPYOU_DATE_FROM.Value = DateTime.Now.ToShortDateString();
            headerForm.dtpDENPYOU_DATE_TO.Value = DateTime.Now.ToShortDateString();

            LogUtility.DebugMethodEnd();
        }
        private void SetReadOnlyHeaderControl()
        {
            LogUtility.DebugMethodStart();
            //set enable
            foreach (Control ctr in headerForm.Controls)
            {
                Type ctype = ctr.GetType();
                if (ctype == typeof(CustomDateTimePicker))
                {
                    CustomDateTimePicker cCustomDateTimePicker = (CustomDateTimePicker)ctr;
                    cCustomDateTimePicker.ReadOnly = true;
                }
                else if (ctype == typeof(CustomTextBox))
                {
                    CustomTextBox cCustomTextBox = (CustomTextBox)ctr;
                    cCustomTextBox.ReadOnly = true;
                }
                else if (ctype == typeof(CustomNumericTextBox2))
                {
                    CustomNumericTextBox2 cCustomNumericTextBox2 = (CustomNumericTextBox2)ctr;
                    cCustomNumericTextBox2.ReadOnly = true;
                }
                else if (ctype == typeof(CustomAlphaNumTextBox))
                {
                    CustomAlphaNumTextBox cCustomAlphaNumTextBox = (CustomAlphaNumTextBox)ctr;
                    cCustomAlphaNumTextBox.ReadOnly = true;
                }

            }
            //txtAler_number can input
            headerForm.ctxt_AlertNumber.ReadOnly = false;
            headerForm.ctxt_AlertNumber.Enabled = true;
            headerForm.ctxt_AlertNumber.MaxLength = 5;
            LogUtility.DebugMethodEnd();
        }
        private void SetSearchCondition(ShukeiHyoJokenDTO entityDTO)
        {
            LogUtility.DebugMethodStart();

            headerForm.cnTxtKYOTEN_CD.Text = entityDTO.M_KYOTEN_CD;
            headerForm.cTxtKYOTEN_NAME_RYAKU.Text = entityDTO.M_KYOTEN_NAME;

            //get sysinfo again to ctxt_AlertNumber
            //this.GetSysInfo();
            if (this.tableResult != null)
                headerForm.ctxt_ReadDataNumber.Text = this.tableResult.Rows.Count.ToString();
            else
                headerForm.ctxt_ReadDataNumber.Text = "0";
            ///////date/////////////////////////
            headerForm.dtpDENPYOU_DATE_FROM.Value = entityDTO.DAINOU_ENTRY_DENPYOU_DATE_FROM;//DateTime.Parse(entityDTO.Shukka_Entry_Denpyou_Date_Begin.ToString()).ToString();
            headerForm.dtpDENPYOU_DATE_TO.Value = entityDTO.DAINOU_ENTRY_DENPYOU_DATE_TO;
            ///////////////////////////////////
            headerForm.cTxtTORIHIKISAKI_CD.Text = entityDTO.UKEIRE_ENTRY_TORIHIKISAKI_CD_FROM;
            headerForm.cTxtTORIHIKISAKI_NAME.Text = entityDTO.UKEIRE_ENTRY_TORIHIKISAKI_NAME_FROM;
            headerForm.cTxtTORIHIKISAKI_CD_TO.Text = entityDTO.UKEIRE_ENTRY_TORIHIKISAKI_CD_TO;
            headerForm.cTxtTORIHIKISAKI_NAME_TO.Text = entityDTO.UKEIRE_ENTRY_TORIHIKISAKI_NAME_TO;

            headerForm.cTxtTORIHIKISAKI_CD_EX.Text = entityDTO.SHUKKA_ENTRY_TORIHIKISAKI_CD_FROM;
            headerForm.cTxtTORIHIKISAKI_NAME_EX.Text = entityDTO.SHUKKA_ENTRY_TORIHIKISAKI_NAME_FROM;
            headerForm.cTxtTORIHIKISAKI_CD_EX_TO.Text = entityDTO.SHUKKA_ENTRY_TORIHIKISAKI_CD_TO;
            headerForm.cTxtTORIHIKISAKI_NAME_EX_TO.Text = entityDTO.SHUKKA_ENTRY_TORIHIKISAKI_NAME_TO;

            headerForm.cTxtGYOUSHA_CD.Text = entityDTO.UKEIRE_ENTRY_GYOUSHA_CD_FROM;
            headerForm.cTxtGYOUSHA_NAME.Text = entityDTO.UKEIRE_ENTRY_GYOUSHA_NAME_FROM;
            headerForm.cTxtGYOUSHA_CD_TO.Text = entityDTO.UKEIRE_ENTRY_GYOUSHA_CD_TO;
            headerForm.cTxtGYOUSHA_NAME_TO.Text = entityDTO.UKEIRE_ENTRY_GYOUSHA_NAME_TO;

            headerForm.cTxtGYOUSHA_CD_EX.Text = entityDTO.SHUKKA_ENTRY_GYOUSHA_CD_FROM;
            headerForm.cTxtGYOUSHA_NAME_EX.Text = entityDTO.SHUKKA_ENTRY_GYOUSHA_NAME_FROM;
            headerForm.cTxtGYOUSHA_CD_EX_TO.Text = entityDTO.SHUKKA_ENTRY_GYOUSHA_CD_TO;
            headerForm.cTxtGYOUSHA_NAME_EX_TO.Text = entityDTO.SHUKKA_ENTRY_GYOUSHA_NAME_TO;

            headerForm.cTxtGENBA_CD.Text = entityDTO.UKEIRE_ENTRY_GENBA_CD_FROM;
            headerForm.cTxtGENBA_NAME.Text = entityDTO.UKEIRE_ENTRY_GENBA_NAME_FROM;
            headerForm.cTxtGENBA_CD_TO.Text = entityDTO.UKEIRE_ENTRY_GENBA_CD_TO;
            headerForm.cTxtGENBA_NAME_TO.Text = entityDTO.UKEIRE_ENTRY_GENBA_NAME_TO;

            headerForm.cTxtGENBA_CD_EX.Text = entityDTO.SHUKKA_ENTRY_GENBA_CD_FROM;
            headerForm.cTxtGENBA_NAME_EX.Text = entityDTO.SHUKKA_ENTRY_GENBA_NAME_FROM;
            headerForm.cTxtGENBA_CD_EX_TO.Text = entityDTO.SHUKKA_ENTRY_GENBA_CD_TO;
            headerForm.cTxtGENBA_NAME_EX_TO.Text = entityDTO.SHUKKA_ENTRY_GENBA_NAME_TO;

            headerForm.cTxtHINMEI_CD.Text = entityDTO.UKEIRE_DETAIL_HINMEI_CD_FROM;
            headerForm.cTxtHINMEI_NAME.Text = entityDTO.UKEIRE_DETAIL_HINMEI_NAME_FROM;
            headerForm.cTxtHINMEI_CD_TO.Text = entityDTO.UKEIRE_DETAIL_HINMEI_CD_TO;
            headerForm.cTxtHINMEI_NAME_TO.Text = entityDTO.UKEIRE_DETAIL_HINMEI_NAME_TO;

            headerForm.cTxtHINMEI_CD_EX.Text = entityDTO.SHUKKA_DETAIL_HINMEI_CD_FROM;
            headerForm.cTxtHINMEI_NAME_EX.Text = entityDTO.SHUKKA_DETAIL_HINMEI_NAME_FROM;
            headerForm.cTxtHINMEI_CD_EX_TO.Text = entityDTO.SHUKKA_DETAIL_HINMEI_CD_TO;
            headerForm.cTxtHINMEI_NAME_EX_TO.Text = entityDTO.SHUKKA_DETAIL_HINMEI_NAME_TO;

            headerForm.cTxtUNPAN_GYOUSHA_CD.Text = entityDTO.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_FROM;
            headerForm.cTxtUNPAN_GYOUSHA_NAME.Text = entityDTO.UNCHIN_ENTRY_UNPAN_GYOUSHA_NAME_FROM;
            headerForm.cTxtUNPAN_GYOUSHA_CD_TO.Text = entityDTO.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_TO;
            headerForm.cTxtUNPAN_GYOUSHA_NAME_TO.Text = entityDTO.UNCHIN_ENTRY_UNPAN_GYOUSHA_NAME_TO;

            LogUtility.DebugMethodEnd();
        }
        #region Process For New Row
        private enum NewRowType
        {
            HeadEntry = 1,
            ContentDetail = 2,
            TotalPerOne = 3,
            TotalPerList = 4
        }
        private int AddNewRow(NewRowType rowType)
        {
            int irowIndex = -1;
            irowIndex = GridMultiRowResult.Rows.Add();
            switch (rowType)
            {
                case NewRowType.HeadEntry:
                    VisibleGridControl(sHeadEntry, true, irowIndex);
                    VisibleGridControl(sDetail, false, irowIndex);
                    VisibleGridControl(sTotalOne, false, irowIndex);
                    VisibleGridControl(sTotalAll, false, irowIndex);
                    break;
                case NewRowType.ContentDetail:
                    VisibleGridControl(sDetail, true, irowIndex);
                    VisibleGridControl(sHeadEntry, false, irowIndex);
                    VisibleGridControl(sTotalOne, false, irowIndex);
                    VisibleGridControl(sTotalAll, false, irowIndex);
                    break;
                case NewRowType.TotalPerOne:
                    VisibleGridControl(sTotalOne, true, irowIndex);
                    VisibleGridControl(sHeadEntry, false, irowIndex);
                    VisibleGridControl(sDetail, false, irowIndex);
                    VisibleGridControl(sTotalAll, false, irowIndex);
                    break;
                case NewRowType.TotalPerList:
                    VisibleGridControl(sTotalAll, true, irowIndex);
                    VisibleGridControl(sHeadEntry, false, irowIndex);
                    VisibleGridControl(sDetail, false, irowIndex);
                    VisibleGridControl(sTotalOne, false, irowIndex);
                    break;
            }
            return irowIndex;
        }
        private void VisibleGridControl(string[] ColName, bool bVisible, int irowIndex)
        {
            foreach (string CellName in ColName)
            {
                GridMultiRowResult.Rows[irowIndex].Cells[CellName].Visible = bVisible;
            }
        }
        private void AddHeadEntryContent(int irowIndex, DataRow dtrow)
        {
            //add head entry
            GridMultiRowResult.Rows[irowIndex].Cells["GYOUSHA_CD_IN"].Value = dtrow["UKEIRE_GYOUSHA_CD"];
            GridMultiRowResult.Rows[irowIndex].Cells["GYOUSHA_NAME_IN"].Value = dtrow["UKEIRE_GYOUSHA_NAME"];

            GridMultiRowResult.Rows[irowIndex].Cells["GYOUSHA_CD_EX"].Value = dtrow["SHUKKA_GYOUSHA_CD"];
            GridMultiRowResult.Rows[irowIndex].Cells["GYOUSHA_NAME_EX"].Value = dtrow["SHUKKA_GYOUSHA_NAME"];

            GridMultiRowResult.Rows[irowIndex].Cells["TORIHIKISAKI_CD_IN"].Value = dtrow["UKEIRE_TORIHIKISAKI_CD"];
            GridMultiRowResult.Rows[irowIndex].Cells["TORIHIKISAKI_NAME_IN"].Value = dtrow["UKEIRE_TORIHIKISAKI_NAME"];

            GridMultiRowResult.Rows[irowIndex].Cells["TORIHIKISAKI_CD_EX"].Value = dtrow["SHUKKA_TORIHIKISAKI_CD"];
            GridMultiRowResult.Rows[irowIndex].Cells["TORIHIKISAKI_NAME_EX"].Value = dtrow["SHUKKA_TORIHIKISAKI_NAME"];

            GridMultiRowResult.Rows[irowIndex].Cells["GENBA_CD_IN"].Value = dtrow["UKEIRE_GENBA_CD"];
            GridMultiRowResult.Rows[irowIndex].Cells["GENBA_NAME_IN"].Value = dtrow["UKEIRE_GENBA_NAME"];

            GridMultiRowResult.Rows[irowIndex].Cells["GENBA_CD_EX"].Value = dtrow["SHUKKA_GENBA_CD"];
            GridMultiRowResult.Rows[irowIndex].Cells["GENBA_NAME_EX"].Value = dtrow["SHUKKA_GENBA_NAME"];

            GridMultiRowResult.Rows[irowIndex].Cells["UNPAN_GYOUSHA_CD"].Value = dtrow["UPN_GYOUSHA_CD"];
            GridMultiRowResult.Rows[irowIndex].Cells["UNPAN_GYOUSHA_NAME"].Value = dtrow["UPN_GYOUSHA_NAME"];

            ////total unchin kingaku
            //if (!String.IsNullOrEmpty(Convert.ToString(dtrow["UNCHIN_KINGAKU_GOUKEI"])))
            //    GridMultiRowResult.Rows[irowIndex].Cells["KINGAKU_TRAN"].Value = CommonCalc.DecimalFormat(decimal.Parse(Convert.ToString(dtrow["UNCHIN_KINGAKU_GOUKEI"])));//CommonCalc.DecimalFormat(decimal.Parse(Convert.ToString(dtrow["UNCHIN_KINGAKU"])));
            //end add head entry
        }
        private void AddDetailContent(int irowIndex, DataRow dtrow, bool bAddHeaderRow)
        {
            //add flag for process selected row
            GridMultiRowResult.Rows[irowIndex].Tag = dtrow["DENPYOU_NUMBER"];
            //add detail conten
            //decimal dKINGAKU_IN = 0;            
            if (bAddHeaderRow)
            {
                GridMultiRowResult.Rows[irowIndex].Cells["DENPYOU_DATE"].Value = ((DateTime)dtrow["DENPYOU_DATE"]).ToString("yyyy/MM/dd");
                GridMultiRowResult.Rows[irowIndex].Cells["DAINOU_NUMBER"].Value = dtrow["DENPYOU_NUMBER"];
            }
            else
            {
                GridMultiRowResult.Rows[irowIndex].Cells["DENPYOU_DATE"].Value = null;
                GridMultiRowResult.Rows[irowIndex].Cells["DAINOU_NUMBER"].Value = null;
            }
            GridMultiRowResult.Rows[irowIndex].Cells["ROW_NO"].Value = dtrow["ROW_NO"];
            GridMultiRowResult.Rows[irowIndex].Cells["HINMEI_CD_IN"].Value = dtrow["UKEIRE_HINMEI_CD"];
            GridMultiRowResult.Rows[irowIndex].Cells["HINMEI_NAME_IN"].Value = dtrow["UKEIRE_HINMEI_NAME"];

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["UKEIRE_SYOUMI"])))
                GridMultiRowResult.Rows[irowIndex].Cells["NET_JYUURYOU_IN"].Value = CommonCalc.DecimalFormat((decimal.Parse(dtrow["UKEIRE_SYOUMI"].ToString())));

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["UKEIRE_SUURYOU"])))
                GridMultiRowResult.Rows[irowIndex].Cells["SUURYOU_IN"].Value = CommonCalc.DecimalFormat((decimal.Parse(dtrow["UKEIRE_SUURYOU"].ToString())));

            GridMultiRowResult.Rows[irowIndex].Cells["UNIT_NAME_RYAKU_IN"].Value = dtrow["UKEIRE_UNIT_NAME"];

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["UKEIRE_TANKA"])))
                GridMultiRowResult.Rows[irowIndex].Cells["TANKA_IN"].Value = CommonCalc.DecimalFormat((decimal.Parse(dtrow["UKEIRE_TANKA"].ToString())));

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["UKEIRE_KINGAKU"])))
                GridMultiRowResult.Rows[irowIndex].Cells["KINGAKU_IN"].Value = CommonCalc.DecimalFormat((decimal.Parse(dtrow["UKEIRE_KINGAKU"].ToString())));

            GridMultiRowResult.Rows[irowIndex].Cells["MEISAI_BIKOU_IN"].Value = dtrow["UKEIRE_BIKOU"];

            GridMultiRowResult.Rows[irowIndex].Cells["HINMEI_CD_EX"].Value = dtrow["SHUKKA_HINMEI_CD"];
            GridMultiRowResult.Rows[irowIndex].Cells["HINMEI_NAME_EX"].Value = dtrow["SHUKKA_HINMEI_NAME"];

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["SHUKKA_SYOUMI"])))
                GridMultiRowResult.Rows[irowIndex].Cells["NET_JYUURYOU_EX"].Value = CommonCalc.DecimalFormat((decimal.Parse(dtrow["SHUKKA_SYOUMI"].ToString())));

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["SHUKKA_SUURYOU"])))
                GridMultiRowResult.Rows[irowIndex].Cells["SUURYOU_EX"].Value = CommonCalc.DecimalFormat((decimal.Parse(dtrow["SHUKKA_SUURYOU"].ToString())));

            GridMultiRowResult.Rows[irowIndex].Cells["UNIT_NAME_RYAKU_EX"].Value = dtrow["SHUKKA_UNIT_NAME"];

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["SHUKKA_TANKA"])))
                GridMultiRowResult.Rows[irowIndex].Cells["TANKA_EX"].Value = CommonCalc.DecimalFormat((decimal.Parse(dtrow["SHUKKA_TANKA"].ToString())));

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["SHUKKA_KINGAKU"])))
                GridMultiRowResult.Rows[irowIndex].Cells["KINGAKU_EX"].Value = CommonCalc.DecimalFormat((decimal.Parse(dtrow["SHUKKA_KINGAKU"].ToString())));

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["SAEKI_KINGAKU"])))
                GridMultiRowResult.Rows[irowIndex].Cells["SAEKI_KINGAKU"].Value = CommonCalc.DecimalFormat((decimal.Parse(dtrow["SAEKI_KINGAKU"].ToString())));//CommonCalc.DecimalFormat(dKINGAKU_IN);

            GridMultiRowResult.Rows[irowIndex].Cells["MEISAI_BIKOU_EX"].Value = dtrow["SHUKKA_BIKOU"];
            //end detail conten
        }
        private decimal SumTotalByField(int irowBegin, int irowEnd, string FiledName)
        {
            decimal dResult = 0;
            for (int i = irowBegin; i < irowEnd; i++)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(GridMultiRowResult.Rows[i].Cells[FiledName].Value)))
                    dResult += decimal.Parse(GridMultiRowResult.Rows[i].Cells[FiledName].Value.ToString());
            }
            return dResult;
        }
        private void SetTotalOne(int irowIndex, DataRow dtrow)
        {
            //set value

            // ヘッダスタイル設定
            this.SetTitleCellStyle(GridMultiRowResult.Rows[irowIndex].Cells["lblTotal_ONE_IN"]);
            this.SetTitleCellStyle(GridMultiRowResult.Rows[irowIndex].Cells["lblTotal_ONE_EX"]);

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["UKEIRE_SYOUMI_GOUKEI"])))
                GridMultiRowResult.Rows[irowIndex].Cells["TOTAL_NET_JYUURYOU_IN"].Value = CommonCalc.DecimalFormat(Decimal.Parse(dtrow["UKEIRE_SYOUMI_GOUKEI"].ToString()));
            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["UKEIRE_KINGAKU_GOUKEI"])))
                GridMultiRowResult.Rows[irowIndex].Cells["TOTAL_KINGAKU_IN"].Value = CommonCalc.DecimalFormat(Decimal.Parse(dtrow["UKEIRE_KINGAKU_GOUKEI"].ToString()));

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["SHUKKA_SYOUMI_GOUKEI"])))
                GridMultiRowResult.Rows[irowIndex].Cells["TOTAL_NET_JYUURYOU_EX"].Value = CommonCalc.DecimalFormat(Decimal.Parse(dtrow["SHUKKA_SYOUMI_GOUKEI"].ToString()));
            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["SHUKKA_KINGAKU_GOUKEI"])))
                GridMultiRowResult.Rows[irowIndex].Cells["TOTAL_KINGAKU_EX"].Value = CommonCalc.DecimalFormat(Decimal.Parse(dtrow["SHUKKA_KINGAKU_GOUKEI"].ToString()));

            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["SAEKI_KINGAKU_GOUKEI"])))
                GridMultiRowResult.Rows[irowIndex].Cells["TOTAL_SAEKI_KINGAKU"].Value = CommonCalc.DecimalFormat(Decimal.Parse(dtrow["SAEKI_KINGAKU_GOUKEI"].ToString()));
            if (!String.IsNullOrEmpty(Convert.ToString(dtrow["UNCHIN_KINGAKU_GOUKEI"])))
                GridMultiRowResult.Rows[irowIndex].Cells["TOTAL_KINGAKU_TRAN_ONE"].Value = CommonCalc.DecimalFormat(Decimal.Parse(dtrow["UNCHIN_KINGAKU_GOUKEI"].ToString()));
        }
        /// <summary>
        /// Sum total all of list and set value
        /// </summary>
        private void SetTotalAll()
        {

            int irowEnd = GridMultiRowResult.Rows.Count - 1;

            DataTable dtTotal = DicR403["Footer"];
            DataRow dtrow = dtTotal.Rows[0];
            // ヘッダスタイル設定
            this.SetTitleCellStyle(GridMultiRowResult.Rows[irowEnd].Cells["lblTotal_IN_ALL"]);
            this.SetTitleCellStyle(GridMultiRowResult.Rows[irowEnd].Cells["lblTotal_EX_ALL"]);

            GridMultiRowResult.Rows[irowEnd].Cells["TOTAL_NET_JYUURYOU_IN_ALL"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(dtrow["UKEIRE_SYOUMI_SOUGOUKEI"]));
            GridMultiRowResult.Rows[irowEnd].Cells["TOTAL_KINGAKU_IN_ALL"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(dtrow["UKEIRE_KINGAKU_SOUGOUKEI"]));

            GridMultiRowResult.Rows[irowEnd].Cells["TOTAL_NET_JYUURYOU_EX_ALL"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(dtrow["SHUKKA_SYOUMI_SOUGOUKEI"]));
            GridMultiRowResult.Rows[irowEnd].Cells["TOTAL_KINGAKU_EX_ALL"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(dtrow["SHUKKA_KINGAKU_SOUGOUKEI"]));

            GridMultiRowResult.Rows[irowEnd].Cells["TOTAL_SAEKI_KINGAKU_ALL"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(dtrow["SAEKI_KINGAKU_SOUGOUKEI"]));
            GridMultiRowResult.Rows[irowEnd].Cells["TOTAL_KINGAKU_TRAN_ALL"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(dtrow["UNCHIN_KINGAKU_SOUGOUKEI"]));
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        public void BindData(DataTable dtResult)
        {
            LogUtility.DebugMethodStart();
            try
            {

                //current row index of gridview
                int irowIndex = -1;
                //index for total/dainou
                int irowBegin = -1;

                string Current_DENPYOU_NUMBER = string.Empty;

                string Current_UKEIRE_GYOUSHA_CD = string.Empty;
                string Current_SHUKKA_GYOUSHA_CD = string.Empty;
                string Current_UKEIRE_TORIHIKISAKI_CD = string.Empty;
                string Current_SHUKKA_TORIHIKISAKI_C = string.Empty;
                string Current_UKEIRE_GENBA_CD = string.Empty;
                string Current_SHUKKA_GENBA_CD = string.Empty;
                if (dtResult.Rows.Count > 0)
                {
                    irowBegin = 0;
                    Current_DENPYOU_NUMBER = dtResult.Rows[0]["DENPYOU_NUMBER"].ToString();

                    Current_UKEIRE_GYOUSHA_CD = dtResult.Rows[0]["UKEIRE_GYOUSHA_CD"].ToString();
                    Current_SHUKKA_GYOUSHA_CD = dtResult.Rows[0]["SHUKKA_GYOUSHA_CD"].ToString();
                    Current_UKEIRE_TORIHIKISAKI_CD = dtResult.Rows[0]["UKEIRE_TORIHIKISAKI_CD"].ToString();
                    Current_SHUKKA_TORIHIKISAKI_C = dtResult.Rows[0]["SHUKKA_TORIHIKISAKI_CD"].ToString();
                    Current_UKEIRE_GENBA_CD = dtResult.Rows[0]["UKEIRE_GENBA_CD"].ToString();
                    Current_SHUKKA_GENBA_CD = dtResult.Rows[0]["SHUKKA_GENBA_CD"].ToString();

                    //first row                
                    irowIndex = this.AddNewRow(NewRowType.HeadEntry);
                    this.AddHeadEntryContent(irowIndex, dtResult.Rows[0]);
                    irowIndex = this.AddNewRow(NewRowType.ContentDetail);
                    this.AddDetailContent(irowIndex, dtResult.Rows[0], true);

                    for (int irow = 1; irow < dtResult.Rows.Count; irow++)
                    {
                        DataRow dtrow = dtResult.Rows[irow];
                        if (!Convert.ToString(dtrow["DENPYOU_NUMBER"]).Equals(Current_DENPYOU_NUMBER))
                        {
                            Current_DENPYOU_NUMBER = Convert.ToString(dtrow["DENPYOU_NUMBER"]);
                            irowIndex = this.AddNewRow(NewRowType.TotalPerOne);
                            this.SetTotalOne(irowIndex, dtResult.Rows[irow - 1]);
                            if (!Convert.ToString(dtrow["UKEIRE_GYOUSHA_CD"]).Equals(Current_UKEIRE_GYOUSHA_CD)
                                || !Convert.ToString(dtrow["SHUKKA_GYOUSHA_CD"]).Equals(Current_SHUKKA_GYOUSHA_CD)
                                || !Convert.ToString(dtrow["UKEIRE_TORIHIKISAKI_CD"]).Equals(Current_UKEIRE_TORIHIKISAKI_CD)
                                || !Convert.ToString(dtrow["SHUKKA_TORIHIKISAKI_CD"]).Equals(Current_SHUKKA_TORIHIKISAKI_C)
                                || !Convert.ToString(dtrow["UKEIRE_GENBA_CD"]).Equals(Current_UKEIRE_GENBA_CD)
                                || !Convert.ToString(dtrow["SHUKKA_GENBA_CD"]).Equals(Current_SHUKKA_GENBA_CD))
                            {
                                Current_UKEIRE_GYOUSHA_CD = dtrow["UKEIRE_GYOUSHA_CD"].ToString();
                                Current_SHUKKA_GYOUSHA_CD = dtrow["SHUKKA_GYOUSHA_CD"].ToString();
                                Current_UKEIRE_TORIHIKISAKI_CD = dtrow["UKEIRE_TORIHIKISAKI_CD"].ToString();
                                Current_SHUKKA_TORIHIKISAKI_C = dtrow["SHUKKA_TORIHIKISAKI_CD"].ToString();
                                Current_UKEIRE_GENBA_CD = dtrow["UKEIRE_GENBA_CD"].ToString();
                                Current_SHUKKA_GENBA_CD = dtrow["SHUKKA_GENBA_CD"].ToString();

                                //add header entry for new detail
                                irowIndex = this.AddNewRow(NewRowType.HeadEntry);
                                this.AddHeadEntryContent(irowIndex, dtrow);
                            }
                            //reset begin index
                            irowBegin = irow;//irowIndex;                        
                            //add detail
                            irowIndex = this.AddNewRow(NewRowType.ContentDetail);
                            this.AddDetailContent(irowIndex, dtrow, true);

                        }
                        else
                        {
                            irowIndex = this.AddNewRow(NewRowType.ContentDetail);
                            this.AddDetailContent(irowIndex, dtrow, false);
                        }
                    }
                    //add total for end list
                    irowIndex = this.AddNewRow(NewRowType.TotalPerOne);
                    this.SetTotalOne(irowIndex, dtResult.Rows[irowBegin]);
                    //add total for footer
                    irowIndex = this.AddNewRow(NewRowType.TotalPerList);
                    this.SetTotalAll();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #region 明細中のヘッダスタイル設定
        /// <summary>
        /// 明細中のヘッダスタイル設定
        /// </summary>
        /// <param name="cell">セール</param>
        private void SetTitleCellStyle(GrapeCity.Win.MultiRow.Cell cell)
        {
            try
            {

                cell.Style.BackColor = Color.FromArgb(0, 105, 51);
                cell.Style.SelectionBackColor = Color.FromArgb(0, 105, 51);
                cell.Style.ForeColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.FromArgb(255, 255, 255);
                cell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;

                GcCustomTextBoxCell cellKikan = (GcCustomTextBoxCell)cell;
                // 自動背景色変更モード
                cellKikan.AutoChangeBackColorEnabled = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
            }
        }
        #endregion
        #region gcCustomMultiRowResult event
        protected void gcCustomMultiRowResult_CellDoubleClick(object sender, CellEventArgs e)
        {
            //画面に表示中の受付明細表を印刷する。
            //hien thi man hinh nhap chi tiet (chi tra thay the)
            //waiting
            int iIndex = e.RowIndex;
            if (iIndex >= 0)
            {
                this.GridMultiRowResult.Rows[iIndex].Selected = true;
                object objDainouNumber = GridMultiRowResult.Rows[iIndex].Tag;
                if (objDainouNumber != null)
                {
                    string sDainouNumber = objDainouNumber.ToString();
                    //call form
                    MessageBox.Show("Waiting for 画面に表示中の受付明細表を印刷する。", string.Format("[Parameter] Dainou Number: {0}", sDainouNumber), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //end call form                    
                }
            }
        }
        protected void gcCustomMultiRowResult_CellClick(object sender, CellEventArgs e)
        {
            //hien thi tinh trang chon cac cell lien quan cua 1 phieu
            int iIndex = e.RowIndex;
            if (iIndex >= 0)
                GridMultiRowResult.Rows[iIndex].Selected = true;
        }
        private void ctxt_AlertNumber_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.headerForm.ctxt_AlertNumber.Text.Trim()))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E002", "アラート件数", "1～99999");
                this.headerForm.ctxt_AlertNumber.Focus();
            }
            else if (Convert.ToDecimal(this.headerForm.ctxt_AlertNumber.Text.Replace(",", "")) < 1 || Convert.ToDecimal(this.headerForm.ctxt_AlertNumber.Text.Replace(",", "")) > 99999)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E002", "アラート件数", "1～99999");
                this.headerForm.ctxt_AlertNumber.Focus();
            }
            else
            {
                this.headerForm.ctxt_AlertNumber.Text = CommonCalc.DecimalFormat(Convert.ToDecimal(this.headerForm.ctxt_AlertNumber.Text.Replace(",", "")));
            }

        }
        #endregion
        #region button event
        public void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            PrintData();
            LogUtility.DebugMethodEnd(sender, e);
        }
        public void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            CSVOutput();
            LogUtility.DebugMethodEnd(sender, e);
        }
        public void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            CallPopup();
            LogUtility.DebugMethodEnd(sender, e);
        }
        public void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion
        //////////////////////////

        #region OutPutData
        private void AddHeaderGridExport()
        {
            this.form.gridExport.AutoGenerateColumns = false;
            this.form.gridExport.Columns.Clear();
            string[] headerGrid = new string[] { 
                "受入業者ＣＤ","受入業者名","受入取引先ＣＤ","受入取引先名","受入現場ＣＤ",
                "受入現場名","運搬業者ＣＤ","運搬業者名","出荷業者ＣＤ","出荷業者名",
                "出荷取引先ＣＤ","出荷取引先名","出荷現場ＣＤ","出荷現場名",//"運賃金額",
                "伝票日付","明細行番","受入品名CD","受入品名","受入正味(kg)",
                "受入数量","受入単位","受入単価","受入金額","受入備考",
                "伝票番号","出荷品名CD","出荷品名","出荷正味(kg)","出荷数量",
                "出荷単位","出荷単価","出荷金額","差益金額","出荷備考"};

            string[] fieldBound = new string[] { 
                "UKEIRE_GYOUSHA_CD","UKEIRE_GYOUSHA_NAME","UKEIRE_TORIHIKISAKI_CD","UKEIRE_TORIHIKISAKI_NAME","UKEIRE_GENBA_CD",
                "UKEIRE_GENBA_NAME","UPN_GYOUSHA_CD","UPN_GYOUSHA_NAME","SHUKKA_GYOUSHA_CD","SHUKKA_GYOUSHA_NAME",
                "SHUKKA_TORIHIKISAKI_CD","SHUKKA_TORIHIKISAKI_NAME","SHUKKA_GENBA_CD","SHUKKA_GENBA_NAME",//"UNCHIN_KINGAKU",
                "DENPYOU_DATE","ROW_NO","UKEIRE_HINMEI_CD","UKEIRE_HINMEI_NAME","UKEIRE_SYOUMI",
                "UKEIRE_SUURYOU","UKEIRE_UNIT_NAME","UKEIRE_TANKA","UKEIRE_KINGAKU","UKEIRE_BIKOU",
                "DENPYOU_NUMBER","SHUKKA_HINMEI_CD","SHUKKA_HINMEI_NAME","SHUKKA_SYOUMI","SHUKKA_SUURYOU",
                "SHUKKA_UNIT_NAME","SHUKKA_TANKA","SHUKKA_KINGAKU","SAEKI_KINGAKU","SHUKKA_BIKOU"};

            for (int i = 0; i < headerGrid.Length; i++)
            {
                this.form.gridExport.Columns.Add(fieldBound[i], headerGrid[i]);
                this.form.gridExport.Columns[i].DataPropertyName = fieldBound[i];
            }
        }
        private void CSVOutput()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (DicR403 != null)
                {
                    this.ConvertDicnary();
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (DicR403["Detail"].Rows.Count > 0)
                    {
                        this.AddHeaderGridExport();
                        this.form.gridExport.DataSource = this.DicR403["Detail"];

                        CSVExport exp = new CSVExport();
                        if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)			// CSV出力しますか？
                        {
                            exp.ConvertCustomDataGridViewToCsv(this.form.gridExport, true, true, this.headerForm.lb_title.Text, this.form);
                        }
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E044");
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        private void ConvertDicnary()
        {
            if (DicR403.Keys.Count == 3)
            {
                //change data type to string for reportInfo
                DataTable dtDetail = CopyToStringFormated(DicR403["Detail"]);
                DicR403.Remove("Detail");
                DicR403.Add("Detail", dtDetail);
                //add flag Chaged
                DicR403.Add("Flag", new DataTable());
            }
        }
        private void PrintData()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //check dictionary (3 datatable: Head, Detail, Footer)
                if (DicR403 != null)
                {
                    this.ConvertDicnary();
                    if (DicR403.Keys.Count == 4)
                    {
                        ReportInfoR403 reportInfo = new ReportInfoR403(WINDOW_ID.R_DAINOU_ICHIRANHYOU);
                        reportInfo.DataTableList = DicR403;
                        if (reportInfo != null)
                        {
                            //reportInfo.CreateSampleData();
                            reportInfo.Create(@".\Template\R403-Form.xml", "LAYOUT1", new DataTable());
                            using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                            {
                                //popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                                popup.ReportCaption = this.form.WindowId.ToTitleString();
                                popup.Text = this.form.WindowId.ToTitleString();
                                popup.ShowDialog();
                                popup.Dispose();
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        private DataTable CopyToStringFormated(DataTable dtDetail)
        {
            DataTable dtResult = new DataTable(dtDetail.TableName);
            foreach (DataColumn col in dtDetail.Columns)
            {
                dtResult.Columns.Add(col.ColumnName, typeof(System.String));
            }
            foreach (DataRow row in dtDetail.Rows)
            {
                List<string> list = new List<string>();
                foreach (DataColumn col in dtDetail.Columns)
                {
                    Type colType = row[col.ColumnName].GetType();
                    if (colType == typeof(double) || colType == typeof(decimal) || colType == typeof(float))
                    {
                        list.Add(CommonCalc.DecimalFormat(Convert.ToDecimal(row[col.ColumnName])));
                    }
                    else if (colType == typeof(DateTime))
                    {
                        list.Add(((DateTime)row[col.ColumnName]).ToString("yyyy/MM/dd"));
                    }
                    else
                    {
                        list.Add(row[col.ColumnName].ToString());
                    }
                }
                dtResult.Rows.Add(list.ToArray());
            }

            return dtResult;

        }
        #endregion
    }

}
