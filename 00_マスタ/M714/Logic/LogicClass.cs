using System;
using r_framework.Logic;
using r_framework.APP.Base;
using r_framework.Utility;
using r_framework.Setting;
using System.Reflection;
using r_framework.Const;
using System.Data;
using System.Collections.Generic;
using r_framework.Dao;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Master.ChiikiIkkatsu
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>ボタン設定ファイル</summary>
        public static readonly string BUTTON_SETTING_XML = "Shougun.Core.Master.ChiikiIkkatsu.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm
        /// </summary>
        private HeaderForm headerForm;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm parentForm;

        /// <summary>
        /// 検索用DTO
        /// </summary>
        public DTOClass dto;

        /// <summary>
        /// 業者マスタDao
        /// </summary>
        public DAO_M_GYOUSHA GyoushaDao;

        /// <summary>
        /// 現場マスタDao
        /// </summary>
        public DAO_M_GENBA GenbaDao;

        /// <summary>
        /// 地域マスタDao
        /// </summary>
        public DAO_M_CHIIKI ChiikiDao;

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.parentForm = (BusinessBaseForm)this.form.Parent;
            this.headerForm = (HeaderForm)parentForm.headerForm;
            this.dto = new DTOClass();

            this.GyoushaDao = DaoInitUtility.GetComponent<DAO_M_GYOUSHA>();
            this.GenbaDao = DaoInitUtility.GetComponent<DAO_M_GENBA>();
            this.ChiikiDao = DaoInitUtility.GetComponent<DAO_M_CHIIKI>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, BUTTON_SETTING_XML);
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

            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

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

            this.headerForm.lb_title.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
            this.form.CHIIKI_HENKOU_HOUHOU.Text = "1";
            this.form.CHIIKI_MASTER_MANE.Text = "1";

            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M714", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // FunctionButton
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.bt_func9.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.CHIIKI_HENKOU_HOUHOU.Enabled = false;
            this.form.CHIIKI_MASTER_MANE.Enabled = false;
            this.form.CHIIKI_CD_OLD.Enabled = false;
            this.form.JYUUSHO_OLD.Enabled = false;
            this.form.CHIIKI_CD_NEW.Enabled = false;
            this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Enabled = false;

            // FunctionButton
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func9.Enabled = false;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void CHIIKI_HENKOU_HOUHOU_TextChanged()
        {
            if (!string.IsNullOrEmpty(this.form.CHIIKI_HENKOU_HOUHOU.Text))
            {
                if (this.form.CHIIKI_HENKOU_HOUHOU.Text == "1")
                {
                    this.form.CHIIKI_MASTER_MANE.Enabled = true;
                    this.form.customPanel2.Enabled = true;
                    this.form.JYUUSHO_OLD.Enabled = true;
                    this.parentForm.bt_func8.Enabled = true;
                    this.form.customDataGridView1.Visible = true;
                    this.form.CHECK_ALL.Visible = true;
                    this.headerForm.lblText.Text = "（例）　124:福島市　が増えた場合、　0000007：福島県と登録している地域CDを福島市に変更したい";
                }
                else
                {
                    this.form.CHIIKI_MASTER_MANE.Enabled = false;
                    this.form.customPanel2.Enabled = false;
                    this.form.JYUUSHO_OLD.Enabled = false;
                    this.parentForm.bt_func8.Enabled = false;
                    this.headerForm.lblText.Text = "（例）　間違って登録している地域CD＝100:つくば市を、正しい地域CD＝101：つくば市に変更したい";

                    int k = this.form.customDataGridView1.Rows.Count;
                    for (int i = k; i >= 1; i--)
                    {
                        this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
                    }
                    this.form.customDataGridView1.Refresh();
                    this.form.customDataGridView1.Visible = false;
                    this.form.CHECK_ALL.Visible = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ChiikiCd"></param>
        public void GetChiikiInfo(string ChiikiCd)
        {
            DataTable dt = this.ChiikiDao.GetDataForEntity(ChiikiCd);

            if (dt != null && dt.Rows.Count > 0)
            {
                this.form.CHIIKI_NAME_NEW.Text = dt.Rows[0]["CHIIKI_NAME_RYAKU"].ToString();

                if (ChiikiCd.Equals(this.form.ChiikiCdNewPre))
                {
                    return;
                }
                string ChiikiCdNew = dt.Rows[0]["TODOUFUKEN_CD"].ToString().PadLeft(6, '0');

                var ChiikiEntity = this.ChiikiDao.GetDataByCd(ChiikiCdNew);

                if (ChiikiEntity != null)
                {
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Text = dt.Rows[0]["TODOUFUKEN_CD"].ToString().PadLeft(6, '0');
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_NEW.Text = dt.Rows[0]["TODOUFUKEN_NAME"].ToString();
                }
                else
                {
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Text = string.Empty;
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_NEW.Text = string.Empty;
                }
            }
            else
            {
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Text = string.Empty;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_NEW.Text = string.Empty;
                this.form.CHIIKI_NAME_NEW.Text = string.Empty;
                this.form.errmessage.MessageBoxShow("E020", "地域");
                this.form.CHIIKI_CD_NEW.Focus();
                return;
            }

            this.form.ChiikiCdNewPre = ChiikiCd;
        }

        /// <summary>
        /// 検索データ
        /// </summary>
        /// <param name="dto"></param>
        public void SearchData(DTOClass dto)
        {
            switch (dto.ChiikiMasterName)
            {
                case "1":
                case "3":
                case "5":
                    this.form.customDataGridView1.Columns["GENBA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["GENBA_NAME"].Visible = false;

                    DataTable GyoushaDt = this.GyoushaDao.GetDataForEntity(dto);

                    if (GyoushaDt != null && GyoushaDt.Rows.Count > 0)
                    {
                        this.form.customDataGridView1.DataSource = GyoushaDt;
                    }
                    else
                    {
                        int k = this.form.customDataGridView1.Rows.Count;
                        for (int i = k; i >= 1; i--)
                        {
                            this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
                        }
                        this.form.errmessage.MessageBoxShow("C001");
                        return;
                    }

                    break;
                case "2":
                case "4":
                case "6":
                    this.form.customDataGridView1.Columns["GENBA_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["GENBA_NAME"].Visible = true;

                    DataTable GenbaDt = this.GenbaDao.GetDataForEntity(dto);

                    if (GenbaDt != null && GenbaDt.Rows.Count > 0)
                    {
                        this.form.customDataGridView1.DataSource = GenbaDt;
                    }
                    else
                    {
                        int k = this.form.customDataGridView1.Rows.Count;
                        for (int i = k; i >= 1; i--)
                        {
                            this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
                        }
                        this.form.errmessage.MessageBoxShow("C001");
                        return;
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 更新データ
        /// </summary>
        /// <param name="SentakuData"></param>
        public void CreateEntitys(Dictionary<int, List<string>> SentakuData)
        {
            try
            {
                using (var tran = new Transaction())
                {
                    foreach (var data in SentakuData)
                    {
                        switch (this.dto.ChiikiMasterName)
                        {
                            case "1":
                            case "3":
                            case "5":

                                this.GyoushaDao.UpdateGyoushaChiiki(data.Value[0], this.form.CHIIKI_CD_NEW.Text, this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Text, this.dto.ChiikiMasterName);

                                break;
                            case "2":
                            case "4":
                            case "6":

                                this.GenbaDao.UpdateGenbaChiiki(data.Value[0], data.Value[1], this.form.CHIIKI_CD_NEW.Text, this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Text, this.dto.ChiikiMasterName);

                                break;
                        }
                    }

                    tran.Commit();
                }
                this.form.errmessage.MessageBoxShow("I001", "登録");
                this.SearchData(this.dto);
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Warn(ex); //排他は警告
                this.form.errmessage.MessageBoxShow("E080");
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex); //その他はエラー
                this.form.errmessage.MessageBoxShow("E093");
            }
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

        #region イベント処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
