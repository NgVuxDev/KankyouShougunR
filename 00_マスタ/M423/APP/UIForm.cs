// $Id: UIForm.cs 16250 2014-02-20 01:52:00Z y-sato $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Logic;
using r_framework.Utility;


namespace Shougun.Core.Master.KobestuHinmeiTankaIkkatsu
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        //private r_framework.Logic.IBuisinessLogic logic;

        private LogicClass logic;
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #region コンストラクタ

        public UIForm()
            : base(WINDOW_ID.M_KOBETSU_HINMEI_TANKA_IKKATSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
            
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            
        }

        #endregion

        #region 画面Load処理

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {

            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic.WindowInit();

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

        #endregion

        #region 印刷ボタン押下イベント

        /// <summary>
        /// 印刷ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Print(object sender, EventArgs e)
        {

            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.Print();
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

        #endregion

        #region CSV出力ボタン押下イベント

        /// <summary>
        /// CSV出力ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.ActionBeforeCheck())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");
                    return;
                }

                this.logic.OutputCsvFile();

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

        #endregion

        #region 検索ボタン押下イベント

        /// <summary>
        /// 検索ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                int result = this.logic.Search();
                if (result == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
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

        #endregion

        #region 登録ボタン押下イベント

        /// <summary>
        /// 登録ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!base.RegistErrorFlag)
                {
                    // 件数チェック
                    if (!this.logic.ActionBeforeCheck())
                    {
                        msgLogic.MessageBoxShow("E057", "検索", "一括登録");
                        return;
                    }

                    //Boolean isCheckOK = this.logic.CheckBeforeUpdate();

                    if (!this.logic.CheckBeforeUpdate())
                    {
                        return;
                    }

                    bool catchErr = true;
                    bool isErr = this.logic.CheckIchiranData(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isErr)
                    {
                        msgLogic.MessageBoxShow("E063", "明細", "一括登録");
                        return;
                    }

                    this.logic.Regist(base.RegistErrorFlag);
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

        #endregion

        #region 取消ボタン押下

        /// <summary>
        /// 取消ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {

            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.Cancel();

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

        #endregion

        #region 閉じるボタン押下イベント

        /// <summary>
        /// 閉じるボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {

                var parentForm = (MasterBaseForm)this.Parent;

                this.Close();
                parentForm.Close();
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

        #endregion

        //#region 現場CDロストフォーカスイベント

        ///// <summary>
        ///// 現場CDロストフォーカスイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void GENBA_CD_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        this.logic.GenbaLostFocus();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        //#endregion

        //#region 運搬業者CDロストフォーカスイベント

        ///// <summary>
        ///// 運搬業者CDロストフォーカスイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void UNPAN_KAISHA_CD_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        this.logic.UnpanKaishaLostFocus();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        //#endregion

        //#region 荷降業者CDロストフォーカスイベント

        ///// <summary>
        ///// 荷降業者CDロストフォーカスイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void NIOROSHI_KAISHA_CD_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        this.logic.NioroshiKaishaLostFocus();

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        //#endregion

        //#region 荷降現場CDロストフォーカスイベント

        ///// <summary>
        ///// 荷降現場CDロストフォーカスイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void NIOROSHI_GENBA_CD_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);
                
        //        this.logic.NioroshiGenbaLostFocus();
                
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        //#endregion

        #region 業者ポップアップ後処理
        /// <summary>
        /// 業者ポップアップ後処理
        /// </summary>
        public void AfterGyoushaPopup()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // [現場CD]、[現場]を空にする
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_NAME_RYAKU.Text = String.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("AfterGyoushaPopup", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 荷降業者ポップアップ後処理
        /// <summary>
        /// 荷降業者ポップアップ後処理
        /// </summary>
        public void AfterNioroshiGyoushaPopup()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // [荷降現場CD]、[荷降現場]を空にする
                this.NIOROSHI_GENBA_CD.Text = String.Empty;
                this.NIOROSHI_GENBA_NAME_RYAKU.Text = String.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("AfterNioroshiGyoushaPopup", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
                
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.SetGyoushaInfo();
        }

        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.SetGenbaInfo();
        }
        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
    }
}
