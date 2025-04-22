// $Id: LogicCls.cs 48256 2015-04-24 07:49:36Z wuq@oec-h.com $
using System;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Master.ContenaQrHakkou.APP;
using Shougun.Core.Master.ContenaQrHakkou.DAO;
using System.Data;
using System.Windows.Forms;

namespace Shougun.Core.Master.ContenaQrHakkou.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public MasterBaseForm parentForm;

        /// <summary>
        /// コンテナDAO
        /// </summary>
        public IM_CONTENADao contenaDao;

        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// デザインdao
        /// </summary>
        private DaoCls daoCls;

        #endregion

        #region プロパティ
        /// <summary>
        /// 数量管理の場合
        /// </summary>
        internal bool isSuuryoukanRi = true;

        /// <summary>
        /// 明細データ
        /// </summary>
        internal DataTable SearchResult = null;
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                this.contenaDao = DaoInitUtility.GetComponent<IM_CONTENADao>();
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.daoCls = DaoInitUtility.GetComponent<DaoCls>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                this.parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //画面初期値設定
                this.DefaultInit();

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #region データ設定
        /// <summary>
        ///  画面初期値設定
        /// </summary>
        private void DefaultInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // ・発行数/ページ：4(4.6つ)が入力されていること
                this.form.PAGE_KBN.Text = "4";
                this.form.PAGE_KBN_4.Checked = true;
                // ・コンテナ種類：ブランク
                this.form.CONTENA_CD.Text = string.Empty;
                this.form.CONTENA_NAME_RYAKU.Text = string.Empty;
                // ・コンテナ：ブランク
                this.form.CONTENA_SHURUI_CD.Text = string.Empty;
                this.form.CONTENA_SHURUI_NAME_RYAKU.Text = string.Empty;

                // [表示条件]システム設定(M_SYS_INFO)より値を取得、初期値としてセット。
                // コンテナ(ラベル)、コンテナ、コンテナ(名称)、コンテナCD、コンテナ名
                // 数量管理の場合：非表示
                // 個体管理の場合：表示
                this.GetSysInfoInit();

                // 明細Row追加
                this.form.Ichiran.Rows.Clear();
                this.form.Ichiran.AllowUserToAddRows = false;
                this.form.ALL_CHECKED.Checked = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DefaultInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();

                ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();

                return buttonSetting.LoadButtonSetting(thisAssembly, Const.Constans.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //プレビューボタン(F5)イベント生成
                this.form.C_Regist(this.parentForm.bt_func5);
                this.parentForm.bt_func5.Click -= this.form.PreView;
                this.parentForm.bt_func5.Click += this.form.PreView;
                //検索ボタン(F8)イベント生成
                this.parentForm.bt_func8.Click -= this.form.Search;
                this.parentForm.bt_func8.Click += this.form.Search;
                //閉じるボタン(F12)イベント生成
                this.parentForm.bt_func12.Click -= this.form.FormClose;
                this.parentForm.bt_func12.Click += this.form.FormClose;
                
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region Func処理
        /// <summary>
        /// 表示条件の初期値設定
        /// </summary>
        private void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                // 個体管理
                if (sysInfo != null && sysInfo[0].CONTENA_KANRI_HOUHOU == 2)
                {
                    this.isSuuryoukanRi = false;
                    // コンテナ
                    this.form.LBL_CONTENA.Visible = true;
                    this.form.CONTENA_CD.Visible = true;
                    this.form.CONTENA_NAME_RYAKU.Visible = true;
                    // 明細
                    this.form.Ichiran.Columns["DET_CONTENA_CD"].Visible = true;
                    this.form.Ichiran.Columns["DET_CONTENA_NAME_RYAKU"].Visible = true;
                }
                // 数量管理
                else
                {
                    this.isSuuryoukanRi = true;
                    // コンテナ
                    this.form.LBL_CONTENA.Visible = false;
                    this.form.CONTENA_CD.Visible = false;
                    this.form.CONTENA_NAME_RYAKU.Visible = false;
                    // 明細
                    this.form.Ichiran.Columns["DET_CONTENA_CD"].Visible = false;
                    this.form.Ichiran.Columns["DET_CONTENA_NAME_RYAKU"].Visible = false;
                    // コンテナ管理方法が数量管理の場合UIを変更
                    this.form.PAGE_KBN.Width = 30;
                    this.form.PNL_PAGE.Location = new System.Drawing.Point(155, 9);
                    this.form.CONTENA_SHURUI_CD.Width = 30;
                    this.form.CONTENA_SHURUI_NAME_RYAKU.Location = new System.Drawing.Point(155, 39);
                    this.form.Ichiran.Location = new System.Drawing.Point(1, 69);
                    this.form.Ichiran.Size = new System.Drawing.Size(990, 385);
 
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面データ検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();
                // 画面条件取得処理を行う
                M_CONTENA data = new M_CONTENA();
                if (!string.IsNullOrEmpty(this.form.CONTENA_SHURUI_CD.Text))
                {
                    data.CONTENA_SHURUI_CD = this.form.CONTENA_SHURUI_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.CONTENA_CD.Text))
                {
                    data.CONTENA_CD = this.form.CONTENA_CD.Text;
                }
                // 検索処理を行う
                this.SearchResult = null;
                if (this.isSuuryoukanRi)
                {
                    this.SearchResult = this.daoCls.GetIchiranDataSqlForSuuryoukanRi(data);
                }
                else
                {
                    this.SearchResult = this.daoCls.GetIchiranDataSqlForKotaikanRi(data);
                }
                
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
            return count;
        }

        /// <summary>
        /// コンテナチェック処理
        /// </summary>
        /// <returns></returns>
        internal bool CheckContena()
        {
            try
            {
                M_CONTENA entity = new M_CONTENA();
                entity.CONTENA_CD = this.form.CONTENA_CD.Text;
                entity.CONTENA_SHURUI_CD = this.form.CONTENA_SHURUI_CD.Text;
                entity.DELETE_FLG = false;
                var contena = this.contenaDao.GetAllValidData(entity);
                if (contena != null && contena.Length > 0)
                {
                    this.form.CONTENA_NAME_RYAKU.Text = contena[0].CONTENA_NAME_RYAKU;
                }
                else
                {
                    this.form.CONTENA_NAME_RYAKU.Text = string.Empty;
                    this.form.errmessage.MessageBoxShow("E020", "コンテナ");
                    this.form.CONTENA_CD.Focus();
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckContena", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckContena", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #region [F5]プレビューボタン押下
        /// <summary>
        /// QRコードを発行処理
        /// </summary>
        /// <returns></returns>
        public virtual void PreView()
        {
            try
            {
                // サンプルデータの作成処理を実行する
                DataTable data = this.CreateSampleData();
                if (data == null && data.Rows.Count <= 0)
                {
                    this.form.errmessage.MessageBoxShow("E044");
                    return;
                }

                // 帳票テンプレートで使用するレイアウト
                string LAYOUT = Const.Constans.LAYOUT4;
                switch(this.form.PAGE_KBN.Text)
                {
                    // 発行数/ページ - 1つ
                     case "1":
                        LAYOUT = Const.Constans.LAYOUT1;
                        break;
                     // 発行数/ページ - 2つ
                     case "2":
                        LAYOUT = Const.Constans.LAYOUT2;
                        break;
                     // 発行数/ページ - 4つ
                     case "3":
                        LAYOUT = Const.Constans.LAYOUT3;
                        data = this.CreateSampleDataForLayout4and6(data);
                        break;
                     // 発行数/ページ - 6つ
                     case "4":
                        LAYOUT = Const.Constans.LAYOUT4;
                        data = this.CreateSampleDataForLayout4and6(data);
                        break;
                }

                var reportLogic = new ReportInfoR670();
                reportLogic.CreateReport(data, LAYOUT);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("PreView", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("PreView", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
        }


        /// <summary>
        /// サンプルデータの作成処理を実行する
        /// </summary>
        /// <returns>DataTable</returns>
        internal DataTable CreateSampleData()
        {
            DataTable dataTableTmp;
            DataRow rowTmp;

            #region - Detail -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";

            // KBN
            dataTableTmp.Columns.Add("CONTENA_KANRI_HOUHOU");
            // CONTENA_SHURUI_CD
            dataTableTmp.Columns.Add("CONTENA_SHURUI_CD");
            // CONTENA_CD
            dataTableTmp.Columns.Add("CONTENA_CD");
            // QRコード
            dataTableTmp.Columns.Add("QR_CD");
           

            foreach (DataGridViewRow row in this.form.Ichiran.Rows)
            {
                if ((bool)row.Cells["DET_CHECKED"].Value)
                {
                    // 印刷数
                    int printCount = 1;
                    if (row.Cells["DET_SHOYUU_DAISUU"].Value != null && !string.IsNullOrEmpty(row.Cells["DET_SHOYUU_DAISUU"].Value.ToString()))
                    {
                        printCount = int.Parse(row.Cells["DET_SHOYUU_DAISUU"].Value.ToString());
                    }

                    // 印刷数設定
                    for (int i = 0; i < printCount; i++)
                    {
                        if (this.isSuuryoukanRi)
                        {
                            #region 数量管理
                            rowTmp = dataTableTmp.NewRow();
                            // KBN
                            rowTmp["CONTENA_KANRI_HOUHOU"] = "KBN=1";
                            // CONTENA_SHURUI_CD
                            rowTmp["CONTENA_SHURUI_CD"] = "CONTENA_SHURUI_CD=" + row.Cells["DET_CONTENA_SHURUI_CD"].Value.ToString();
                            // CONTENA_CD
                            rowTmp["CONTENA_CD"] = "";
                            // QRコード
                            rowTmp["QR_CD"] = "KBN=1" + ";" +
                                              "CONTENA_SHURUI_CD=" + row.Cells["DET_CONTENA_SHURUI_CD"].Value.ToString();

                            dataTableTmp.Rows.Add(rowTmp);
                            #endregion
                        }
                        else
                        {
                            #region 個体管理
                            rowTmp = dataTableTmp.NewRow();
                            // KBN
                            rowTmp["CONTENA_KANRI_HOUHOU"] = "KBN=2";
                            // CONTENA_SHURUI_CD
                            rowTmp["CONTENA_SHURUI_CD"] = "CONTENA_SHURUI_CD=" + row.Cells["DET_CONTENA_SHURUI_CD"].Value.ToString();
                            // CONTENA_CD
                            rowTmp["CONTENA_CD"] = "CONTENA_CD=" + row.Cells["DET_CONTENA_CD"].Value.ToString();
                            // QRコード
                            rowTmp["QR_CD"] = "KBN=2" + ";" +
                                              "CONTENA_SHURUI_CD=" + row.Cells["DET_CONTENA_SHURUI_CD"].Value.ToString() + ";" +
                                              "CONTENA_CD=" + row.Cells["DET_CONTENA_CD"].Value.ToString();

                            dataTableTmp.Rows.Add(rowTmp);
                            #endregion
                        }
                    }
                }
            }

            #endregion - Detail -

            return dataTableTmp;
        }

        /// <summary>
        /// サンプルデータの作成処理を実行する
        /// </summary>
        /// <returns>DataTable</returns>
        internal DataTable CreateSampleDataForLayout4and6(DataTable table)
        {
            DataTable dataTableTmp;
            DataRow rowTmp;

            #region - Detail -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";

            // KBN
            dataTableTmp.Columns.Add("CONTENA_KANRI_HOUHOU");
            // CONTENA_SHURUI_CD
            dataTableTmp.Columns.Add("CONTENA_SHURUI_CD");
            // CONTENA_CD
            dataTableTmp.Columns.Add("CONTENA_CD");
            // QRコード
            dataTableTmp.Columns.Add("QR_CD");
            // KBN2
            dataTableTmp.Columns.Add("CONTENA_KANRI_HOUHOU2");
            // CONTENA_SHURUI_CD2
            dataTableTmp.Columns.Add("CONTENA_SHURUI_CD2");
            // CONTENA_CD2
            dataTableTmp.Columns.Add("CONTENA_CD2");
            // QRコード2
            dataTableTmp.Columns.Add("QR_CD2");

            int rowCount = 1;
            foreach (DataRow row in table.Rows)
            {
                if (rowCount % 2 == 1)
                {
                    rowTmp = dataTableTmp.NewRow();
                    // KBN
                    rowTmp["CONTENA_KANRI_HOUHOU"] = row["CONTENA_KANRI_HOUHOU"];
                    // CONTENA_SHURUI_CD
                    rowTmp["CONTENA_SHURUI_CD"] = row["CONTENA_SHURUI_CD"];
                    // CONTENA_CD
                    rowTmp["CONTENA_CD"] = row["CONTENA_CD"];
                    // QRコード
                    rowTmp["QR_CD"] = row["QR_CD"];

                    dataTableTmp.Rows.Add(rowTmp);
                }

                if (rowCount % 2 == 0)
                {
                    // KBN
                    dataTableTmp.Rows[dataTableTmp.Rows.Count - 1]["CONTENA_KANRI_HOUHOU2"] = row["CONTENA_KANRI_HOUHOU"];
                    // CONTENA_SHURUI_CD
                    dataTableTmp.Rows[dataTableTmp.Rows.Count - 1]["CONTENA_SHURUI_CD2"] = row["CONTENA_SHURUI_CD"];
                    // CONTENA_CD
                    dataTableTmp.Rows[dataTableTmp.Rows.Count - 1]["CONTENA_CD2"] = row["CONTENA_CD"];
                    // QRコード
                    dataTableTmp.Rows[dataTableTmp.Rows.Count - 1]["QR_CD2"] = row["QR_CD"];
                }

                rowCount++;
            }

            #endregion - Detail -

            return dataTableTmp;
        }
        #endregion

        #endregion

        #region 必須
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

    }
}