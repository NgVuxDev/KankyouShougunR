using System;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.APP;
using System.ComponentModel;
using r_framework.Entity;
using Seasar.Framework.Exceptions;
using r_framework.APP.Base;
using System.Data.SqlTypes;
using Shougun.Function.ShougunCSCommon.Const;
using System.Collections.Generic;
using r_framework.Dao;
using System.Drawing;
using System.Text;
using r_framework.CustomControl;
using r_framework.Dto;
using Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.DTO;
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.APP;
using Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.APP;
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.DAO;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.Logic
{
    /// <summary> ビジネスロジック </summary>
    internal class DenpyouiTankakkatsuLogic : IBuisinessLogic
    {
        /// <summary> Form </summary>

        private DenpyouiTankakkatsuPopupForm form;

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// 品名のDao
        /// </summary>
        private HinmeiDAO hinmeiDAO;
      
        /// <summary> 検索条件 </summary>
        public NyuuryokuParamDto JoukenParam { get; set; }

        /// <summary> コンストラクタ </summary>
        public DenpyouiTankakkatsuLogic(DenpyouiTankakkatsuPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フィールドの初期化
            this.form = targetForm;
            this.MsgBox = new MessageBoxShowLogic();
            this.hinmeiDAO = DaoInitUtility.GetComponent<HinmeiDAO>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary> 論理削除処理 </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary> 物理削除処理 </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary> 登録処理 </summary>
        /// <param name="errorFlag"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary> 検索処理 </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary> 更新処理 </summary>
        /// <param name="errorFlag"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary> window初期化 </summary>
        /// <param name="joken">joken</param>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // イベントの初期化処理
                this.EventInit();

                // 画面の初期化
                this.PopupInit();

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary> イベント初期化 </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // 一括入力(F8)イベント生成
            this.form.bt_func8.Click -= new EventHandler(this.form.Nyuuryoku);
            this.form.bt_func8.Click += new EventHandler(this.form.Nyuuryoku);

            // 検索条件クリア(F11)イベント生成
            this.form.bt_func11.Click -= new EventHandler(this.form.kuria);
            this.form.bt_func11.Click += new EventHandler(this.form.kuria);

            // キャンセルボタン(F12)イベント生成
            this.form.bt_func12.DialogResult = DialogResult.Cancel;
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索条件クリア
        /// </summary>
        internal void Kuria()
        {
            if (this.form.cbxTanka)
            {
                this.form.TANNKA.Text = string.Empty;
                this.form.KINNGAKU.Text = string.Empty;
                this.form.TANNKA_ZOUGENN.Text = string.Empty;
                this.form.TANNKA.ReadOnly = false;
                this.form.KINNGAKU.ReadOnly = false;
                this.form.TANNKA_ZOUGENN.ReadOnly = false;
            }

            if(this.form.denshuKbnCd == "1")
            {
                this.form.HINMEI_CD_UKEIRE.Text = string.Empty;
                this.form.HINMEI_NAME_UKEIRE.Text = string.Empty;
            }
            else if (this.form.denshuKbnCd == "2")
            {
                this.form.HINMEI_CD_SHUKKA.Text = string.Empty;
                this.form.HINMEI_NAME_SHUKKA.Text = string.Empty;
            }
            else
            {
                this.form.HINMEI_CD_URSH.Text = string.Empty;
                this.form.HINMEI_NAME_URSH.Text = string.Empty;
            }
            this.form.DENPYOU_KBN_CD.Text = string.Empty;
            this.form.DENPYOU_KBN_NAME.Text = string.Empty;
            this.form.SUURYOU.Text = string.Empty;
            this.form.UNIT_CD.Text = string.Empty;
            this.form.UNIT_NAME_RYAKU.Text = string.Empty;
            this.form.MEISAI_BIKOU.Text = string.Empty;

        }

        /// <summary> Popup初期化 </summary>
        private void PopupInit()
        {
            if (this.form.denshuKbnCd == "2")
            {
                this.form.HINMEI_CD_UKEIRE.Visible = false;
                this.form.HINMEI_NAME_UKEIRE.Visible = false;

                this.form.HINMEI_CD_SHUKKA.Visible = true;
                this.form.HINMEI_NAME_SHUKKA.Visible = true;

                this.form.HINMEI_CD_SHUKKA.Location = new System.Drawing.Point(105, 157);
                this.form.HINMEI_NAME_SHUKKA.Location = new System.Drawing.Point(159, 157);
            }
            else if (this.form.denshuKbnCd == "3")
            {
                this.form.HINMEI_CD_UKEIRE.Visible = false;
                this.form.HINMEI_NAME_UKEIRE.Visible = false;

                this.form.HINMEI_CD_URSH.Visible = true;
                this.form.HINMEI_NAME_URSH.Visible = true;

                this.form.HINMEI_CD_URSH.Location = new System.Drawing.Point(105, 157);
                this.form.HINMEI_NAME_URSH.Location = new System.Drawing.Point(159, 157);
            }

            if (this.form.cbxTanka)
            {
                this.form.TANNKA.Enabled = true;
                this.form.KINNGAKU.Enabled = true;
                this.form.TANNKA_ZOUGENN.Enabled = true;
            }
            else
            {
                this.form.TANNKA.Enabled = false;
                this.form.KINNGAKU.Enabled = false;
                this.form.TANNKA_ZOUGENN.Enabled = false;
            }

            if (this.form.cbxHinmei)
            {
                this.form.HINMEI_CD_UKEIRE.Enabled = true;
                this.form.HINMEI_NAME_UKEIRE.Enabled = true;
                this.form.HINMEI_CD_SHUKKA.Enabled = true;
                this.form.HINMEI_NAME_SHUKKA.Enabled = true;
                this.form.HINMEI_CD_URSH.Enabled = true;
                this.form.HINMEI_NAME_URSH.Enabled = true;
            }
            else
            {
                this.form.HINMEI_CD_UKEIRE.Enabled = false;
                this.form.HINMEI_NAME_UKEIRE.Enabled = false;
                this.form.HINMEI_CD_SHUKKA.Enabled = false;
                this.form.HINMEI_NAME_SHUKKA.Enabled = false;
                this.form.HINMEI_CD_URSH.Enabled = false;
                this.form.HINMEI_NAME_URSH.Enabled = false;
            }

            if (this.form.cbxSuuryou)
            {
                this.form.SUURYOU.Enabled = true;
            }
            else
            {
                this.form.SUURYOU.Enabled = false;
            }

            if (this.form.cbxUnit)
            {
                this.form.UNIT_CD.Enabled = true;
            }
            else
            {
                this.form.UNIT_CD.Enabled = false;
            }

            if (this.form.cbxMeisaiBikou)
            {
                this.form.MEISAI_BIKOU.Enabled = true;
            }
            else
            {
                this.form.MEISAI_BIKOU.Enabled = false;
            }
        }


        internal bool SaveParams()
        {
            bool ret = true;
            try
            {
                var nyuuryokuParam = new NyuuryokuParamDto();

                if(!string.IsNullOrEmpty(this.form.TANNKA.Text))
                {
                    nyuuryokuParam.tanka = Convert.ToDecimal(this.form.TANNKA.Text);
                }
                if(!string.IsNullOrEmpty(this.form.KINNGAKU.Text))
                {
                    nyuuryokuParam.kingkaku = Convert.ToDecimal(this.form.KINNGAKU.Text);
                }
                if (!string.IsNullOrEmpty(this.form.TANNKA_ZOUGENN.Text))
                {
                    nyuuryokuParam.tankaZougenn = Convert.ToDecimal(this.form.TANNKA_ZOUGENN.Text);
                }

                if (this.form.denshuKbnCd == "2")
                {
                    if(!string.IsNullOrEmpty(this.form.HINMEI_CD_SHUKKA.Text))
                    {
                        nyuuryokuParam.hinmeiCd = this.form.HINMEI_CD_SHUKKA.Text;
                    }
                    if(!string.IsNullOrEmpty(this.form.HINMEI_NAME_SHUKKA.Text))
                    {
                        nyuuryokuParam.hinmeiName = this.form.HINMEI_NAME_SHUKKA.Text;
                    }
                }
                else if(this.form.denshuKbnCd == "3")
                {
                    if (!string.IsNullOrEmpty(this.form.HINMEI_CD_URSH.Text))
                    {
                        nyuuryokuParam.hinmeiCd = this.form.HINMEI_CD_URSH.Text;
                    }
                    if (!string.IsNullOrEmpty(this.form.HINMEI_NAME_URSH.Text))
                    {
                        nyuuryokuParam.hinmeiName = this.form.HINMEI_NAME_URSH.Text;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.form.HINMEI_CD_UKEIRE.Text))
                    {
                        nyuuryokuParam.hinmeiCd = this.form.HINMEI_CD_UKEIRE.Text;
                    }
                    if (!string.IsNullOrEmpty(this.form.HINMEI_NAME_UKEIRE.Text))
                    {
                        nyuuryokuParam.hinmeiName = this.form.HINMEI_NAME_UKEIRE.Text;
                    }
                }

                if (!string.IsNullOrEmpty(this.form.DENPYOU_KBN_NAME.Text))
                {
                    nyuuryokuParam.denpyouKbnName = this.form.DENPYOU_KBN_NAME.Text;
                }

                if (!string.IsNullOrEmpty(this.form.DENPYOU_KBN_CD.Text))
                {
                    nyuuryokuParam.denpyouKbnCd = this.form.DENPYOU_KBN_CD.Text;
                }

                if (!string.IsNullOrEmpty(this.form.SUURYOU.Text))
                {
                    nyuuryokuParam.suuryou = Convert.ToDecimal(this.form.SUURYOU.Text);
                }

                if (!string.IsNullOrEmpty(this.form.UNIT_CD.Text))
                {
                    nyuuryokuParam.unitCd = this.form.UNIT_CD.Text;
                }

                if (!string.IsNullOrEmpty(this.form.UNIT_NAME_RYAKU.Text))
                {
                    nyuuryokuParam.unitName = this.form.UNIT_NAME_RYAKU.Text;
                }

                if (!string.IsNullOrEmpty(this.form.MEISAI_BIKOU.Text))
                {
                    nyuuryokuParam.meisaiBikou = this.form.MEISAI_BIKOU.Text;
                }

                this.form.NyuuryokuParam = nyuuryokuParam;

            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveParams", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 品名名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchHinmeiNameUkeire(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.form.HINMEI_CD_UKEIRE.Text))
                {
                    // マスタ存在チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    M_HINMEI hinmei = new M_HINMEI();
                    hinmei.HINMEI_CD = this.form.HINMEI_CD_UKEIRE.Text;
                    DataTable dt = this.hinmeiDAO.GetHinmeiData(hinmei);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.HINMEI_NAME_UKEIRE.Text = dt.Rows[0]["HINMEI_NAME_RYAKU"].ToString();
                    }
                    else
                    {
                        this.form.HINMEI_NAME_UKEIRE.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "品名");
                        e.Cancel = true;
                        return true;
                    }
                }
                else
                {
                    this.form.HINMEI_NAME_UKEIRE.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchHinmeiNameUkeire", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHinmeiNameUkeire", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 品名名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchHinmeiNameShukka(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.form.HINMEI_CD_SHUKKA.Text))
                {
                    // マスタ存在チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    M_HINMEI hinmei = new M_HINMEI();
                    hinmei.HINMEI_CD = this.form.HINMEI_CD_SHUKKA.Text;
                    DataTable dt = this.hinmeiDAO.GetHinmeiData(hinmei);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.HINMEI_NAME_SHUKKA.Text = dt.Rows[0]["HINMEI_NAME_RYAKU"].ToString();
                    }
                    else
                    {
                        this.form.HINMEI_NAME_SHUKKA.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "品名");
                        e.Cancel = true;
                        return true;
                    }
                }
                else
                {
                    this.form.HINMEI_NAME_SHUKKA.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchHinmeiNameShukka", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHinmeiNameShukka", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 品名名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchHinmeiNameUrsh(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.form.HINMEI_CD_URSH.Text))
                {
                    // マスタ存在チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    M_HINMEI hinmei = new M_HINMEI();
                    hinmei.HINMEI_CD = this.form.HINMEI_CD_URSH.Text;
                    DataTable dt = this.hinmeiDAO.GetHinmeiData(hinmei);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.HINMEI_NAME_URSH.Text = dt.Rows[0]["HINMEI_NAME_RYAKU"].ToString();
                    }
                    else
                    {
                        this.form.HINMEI_NAME_URSH.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "品名");
                        e.Cancel = true;
                        return true;
                    }
                }
                else
                {
                    this.form.HINMEI_NAME_URSH.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchHinmeiNameUrsh", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHinmeiNameUrsh", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 伝票区分設定
        /// </summary>
        internal bool SetDenpyouKbn()
        {
            CustomControlExtLogic.PopUp((ICustomControl)this.form.DENPYOU_KBN_CD);

            if(string.IsNullOrEmpty(this.form.DENPYOU_KBN_CD.Text))
            {
                this.form.DENPYOU_KBN_NAME.Text = string.Empty;
                return false;
            }

            return true;
        }
    }
}
