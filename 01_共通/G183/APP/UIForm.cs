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
using r_framework.Utility;
using r_framework.APP.PopUp.Base;
using ContenaPopup.Logic;
using Seasar.Quill;

namespace ContenaPopup.APP
{
    public partial class UIForm : SuperPopupForm
    {

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls Logic;

        public bool keyPressFlag = false;

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        public String GyousyaText;

        public UIForm()
            : base(WINDOW_ID.M_CONTENA)
        {
           try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.Logic = new LogicCls(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
           try
            {
                LogUtility.DebugMethodStart(e);
                base.OnLoad(e);
                this.Logic.WindowInit();

                // イベントバンディング
                var allControl = controlUtil.GetAllControls(this);
                foreach (Control c in allControl)
                {
                    Control_Enter(c);
                }
           }
           catch (Exception ex)
           {
               // 例外エラー
               LogUtility.Error(ex);
               throw;
           }
           finally
           {
               LogUtility.DebugMethodEnd();
           }
        }

        /// <summary>
        /// フォーカスイン時に実行されるメソッドの追加を行う
        /// </summary>
        /// <param name="c">追加を行う対象のコントロール</param>
        /// <returns></returns>
        private void Control_Enter(Control c)
        {
            try
            {
                LogUtility.DebugMethodStart();
                c.Enter -= c_GotFocus;
                c.Enter += c_GotFocus;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
        protected void c_GotFocus(object sender, EventArgs e)
        {
            
            try
            {
                LogUtility.DebugMethodStart(sender,e);
                var activ = this.ActiveControl as SuperPopupForm;

                if (activ == null)
                {
                    if (ActiveControl != null)
                    {
                        if (ActiveControl is DataGridView)
                        {
                            if (this.customDataGridView1.Rows.Count <= 0)
                            {
                                if (this.keyPressFlag)
                                {
                                    var ctrl = this.GetNextControl(ActiveControl, false);

                                    this.SelectNextControl(ctrl, false, true, true, true);
                                }
                                else
                                {
                                    this.SelectNextControl(this, true, true, true, true);
                                }
                            }
                        }
                        this.lb_hint.Text = (string)ActiveControl.Tag;


                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
       
        /// <summary>
        /// 業者フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.GyousyaText = this.GYOUSYA_CD.Text;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者ポップアップ後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GYOUSHA_CD_PopupAfterMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (this.GYOUSYA_CD.Text != this.GyousyaText)
                {
                    // 現場CD、現場名をクリアする
                    this.GENNBA_CD.Text = String.Empty;
                    this.GENNBA_NAME_RYAKU.Text = String.Empty;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        /// <summary>
        /// 業者ロストフォーカス処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.GYOUSYA_CD.Text != this.GyousyaText)
                {
                    this.Logic.CheckGyousha();
                }
                GyousyaText = this.GYOUSYA_CD.Text;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }


        }

        /// <summary>
        /// 現場ロストフォーカス処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.Logic.CheckGenba();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コンテナ種類検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PARENT_CONDITION_ITEM_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (string.IsNullOrEmpty(this.PARENT_CONDITION_ITEM.Text))
                {
                    this.PARENT_CONDITION_ITEM.Text = "3";
                }
                this.Logic.ImeControlCondition();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コンテナ検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CHILD_CONDITION_ITEM_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (string.IsNullOrEmpty(this.CHILD_CONDITION_ITEM.Text))
                {
                    this.CHILD_CONDITION_ITEM.Text = "2";
                }
                this.Logic.ImeControlChildCondition();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PARENT_CONDITION_VALUE_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                //{
                //    if (this.Logic.Search() == 0)
                //    {
                //        this.PARENT_CONDITION_VALUE.Focus();
                //    }
                //    else
                //    {
                //        this.customDataGridView1.Focus();
                //    }
                //}
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CHILD_CONDITION_VALUE_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                //{
                //    if (this.Logic.Search() == 0)
                //    {
                //        this.CHILD_CONDITION_VALUE.Focus();
                //    }
                //    else
                //    {
                //        this.customDataGridView1.Focus();
                //    }
                //}
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
       
        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DetailKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
           try
            {
                LogUtility.DebugMethodStart(sender, e);
                //this.ContenaPopupForm_KeyUp(sender, e);
            }
           catch (Exception ex)
           {
               // 例外エラー
               LogUtility.Error(ex);
               throw;
           }
           finally
           {
               LogUtility.DebugMethodEnd();
           }
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.customDataGridView1.Rows.Count > 0)
                    {
                        this.Logic.ElementDecision();
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.keyPressFlag = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                var activ = ActiveControl as SuperPopupForm;

                string a = Convert.ToString(e.KeyData);
                if (e.KeyData.Equals(Keys.LButton | Keys.Back | Keys.Shift)
                  || e.KeyData.Equals(Keys.LButton | Keys.MButton | Keys.Back | Keys.Shift))
                {
                    this.keyPressFlag = true;
                }
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            var activ = ActiveControl as SuperPopupForm;
            base.OnKeyPress(e);
        }


        /// <summary>
        /// キー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContenaPopupForm_KeyUp(object sender, KeyEventArgs e)
        {
           try
            {
                LogUtility.DebugMethodStart(sender, e);            
                //switch (e.KeyCode)
                //{
                //    case Keys.F7:
                //        ControlUtility.ClickButton(this, "bt_func7");
                //        break;
                //    case Keys.F8:
                //        ControlUtility.ClickButton(this, "bt_func8");
                //        break;
                //    case Keys.F9:
                //        ControlUtility.ClickButton(this, "bt_func9");
                //        break;
                //    case Keys.F10:
                //        ControlUtility.ClickButton(this, "bt_func10");
                //        break;
                //    case Keys.F12:
                //        ControlUtility.ClickButton(this, "bt_func12");
                //        break;
                //}
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (e.RowIndex < 0)
                {
                    return;
                }
                this.Logic.ElementDecision();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Clear(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.GYOUSYA_CD.Enabled)
                {
                    this.GYOUSYA_CD.Text = string.Empty;
                    this.GYOUSYA_NAME_RYAKU.Text = string.Empty;
                }

                if (this.GENNBA_CD.Enabled)
                {
                    this.GENNBA_CD.Text = string.Empty;
                    this.GENNBA_NAME_RYAKU.Text = string.Empty;
                }

                if (this.PARENT_CONDITION_ITEM.Enabled)
                {
                    this.PARENT_CONDITION_ITEM.Text = string.Empty;
                    this.PARENT_CONDITION_VALUE.Text = string.Empty;
                }
                this.CHILD_CONDITION_ITEM.Text = string.Empty;
                this.CHILD_CONDITION_VALUE.Text = string.Empty;
                this.GYOUSYA_CD.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.Logic.Search() != 0)
                {
                    this.customDataGridView1.Focus();
                }
                else
                { 
                    //メッセージ
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    this.GYOUSYA_CD.Focus();
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 確定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Selected(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.customDataGridView1 != null && 0 < this.customDataGridView1.RowCount)
                {
                    this.Logic.ElementDecision();
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 並替移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SortSetting(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.customSortHeader1.ShowCustomSortSettingDialog();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Close(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                base.ReturnParams = null;
                this.Close();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
    }
}
