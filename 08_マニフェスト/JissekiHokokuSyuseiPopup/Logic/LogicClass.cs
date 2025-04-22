using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.PaperManifest.JissekiHokokuSyuseiPopup.DAO;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PaperManifest.JissekiHokokuSyuseiPopup
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
        /// SYSTEM_ID
        /// </summary>
        string systemId;

        /// <summary>
        /// SEQ
        /// </summary>
        string seq;

        /// <summary>
        /// DETAIL_SYSTEM_ID
        /// </summary>
        string detailSystemId;

        /// <summary>
        /// マニ明細ポップアップDAO
        /// </summary>
        private JissekiHokokuSyuseiPopupDaoCls dao;

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm, object[] targetList)
        {
            LogUtility.DebugMethodStart(targetForm, targetList);

            // Formをlogicに設定する
            this.form = targetForm;
            // SYSTEM_ID
            this.systemId = targetList[0].ToString();
            // SEQ
            this.seq = targetList[1].ToString();
            // DETAIL_SYSTEM_ID
            this.detailSystemId = targetList[2].ToString();

            // Dao作成
            dao = DaoInitUtility.GetComponent<JissekiHokokuSyuseiPopupDaoCls>();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm, targetList);
        }

        /// <summary>
        /// 一覧に表示するデータをセットする
        /// jigyoubaListから必要なデータを取得し表示する
        /// </summary>
        internal void SetDisplayData()
        {
            try
            {
                this.form.customDataGridView1.Rows.Clear();

                DataTable data = this.dao.GetData(this.systemId, this.seq, this.detailSystemId);
                if (data.Rows.Count > 0)
                {
                    this.form.customDataGridView1.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDisplayData", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void cellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.form.customDataGridView1.Rows.Count < 0)
                {
                    return;
                }

                if (e.RowIndex >= 0)
                {
                    var kbn = this.form.customDataGridView1.Rows[e.RowIndex].Cells["HAIKI_KBN_CD"].Value.ToString();
                    // MANI_SYSTEM_ID
                    string systemid = this.form.customDataGridView1.Rows[e.RowIndex].Cells["MANI_SYSTEM_ID"].Value.ToString();
                    // MANI_SEQ
                    string seq = this.form.customDataGridView1.Rows[e.RowIndex].Cells["MANI_SEQ"].Value.ToString();
                    // DEN_MANI_KANRI_ID
                    string kanriID = this.form.customDataGridView1.Rows[e.RowIndex].Cells["DEN_MANI_KANRI_ID"].Value.ToString();

                    DataTable dt = dao.CheckData(systemid, kbn, kanriID);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E006");
                        return;
                    }

                    WINDOW_TYPE windowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    switch (kbn)
                    {
                        case "1":

                            r_framework.FormManager.FormManager.OpenForm("G119", windowType, null, systemid, null, (int)windowType);
                            break;
                        case "2":

                            r_framework.FormManager.FormManager.OpenForm("G121", windowType, null, systemid, null, (int)windowType);
                            break;
                        case "3":

                            r_framework.FormManager.FormManager.OpenForm("G120", windowType, null, systemid, null, (int)windowType);
                            break;
                        case "4":

                            r_framework.FormManager.FormManager.OpenForm("G141", windowType, kanriID, string.Empty);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cellDoubleClick", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// キャンセル処理
        /// </summary>
        internal void windowClose()
        {
            LogUtility.DebugMethodStart();

            form.Close();

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