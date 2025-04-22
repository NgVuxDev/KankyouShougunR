// $Id: UIForm.cs 50276 2015-05-21 06:30:50Z sanbongi $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.Logic;
using Shougun.Core.Common.IchiranCommon.APP;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.APP
{
    [Implementation]
    public partial class UIForm : IchiranSuperForm
    {
        #region フィールド

        /// <summary>
        /// ロジック
        /// </summary>
        private LogicCls IchiranLogic;

        ///// <summary>
        ///// UIHeader
        ///// </summary>
        //UIHeader header_new;

        /// <summary>
        /// 画面情報初期化フラグ
        /// </summary>
        private Boolean isLoaded;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="paramIn_DenshuKb"></param>
        /// <param name="paramIn_ShaInCd"></param>
        public UIForm(DENSHU_KBN paramIn_DenshuKb, string paramIn_searchString, string paramIn_ShaInCd)
            : base(paramIn_DenshuKb, false)
        {
            try
            {
                LogUtility.DebugMethodStart(paramIn_DenshuKb, paramIn_searchString, paramIn_ShaInCd);

                this.InitializeComponent();

                //パラメータ設定
                //Properties.Settings.Default.ShainCd = paramIn_ShaInCd;
                //Properties.Settings.Default.Save();

                ////Header部の項目を初期化
                //this.header_new = headerForm;

                //ロジックに、Header部情報を設定する            
                this.IchiranLogic = new LogicCls(this);
               
                //IchiranLogic.SetHeader(header_new);
                if (!string.IsNullOrEmpty(paramIn_searchString))
                {
                    string getSearchString = paramIn_searchString.Replace("\r", "").Replace("\n", "");
                    //検索対象文字列取得
                    this.IchiranLogic.searchString = getSearchString;
                }
                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);

                //社員コードを取得すること
                this.ShainCd = SystemProperty.Shain.CD;
                //Main画面で社員コード値を取得すること
                //this.IchiranLogic.syainCode = paramIn_ShaInCd;
                this.IchiranLogic.syainCode = SystemProperty.Shain.CD;
                //伝種区分を取得すること
                DENSHU_KBN time = (DENSHU_KBN)Enum.Parse(typeof(DENSHU_KBN), paramIn_DenshuKb.ToString(), true);
                this.IchiranLogic.denShu_Kbn = (int)time;

                isLoaded = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
            
                base.OnLoad(e);

                //画面情報の初期化
                if (isLoaded == false)
                {
                    //initFrom();
                    if (!this.IchiranLogic.WindowInit()) { return; }

                    this.customSearchHeader1.Visible = true;
                    this.customSearchHeader1.Location = new System.Drawing.Point(4, 91);
                    this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customSortHeader1.Location = new System.Drawing.Point(4, 113);
                    this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customDataGridView1.Location = new System.Drawing.Point(3, 140);
                    this.customDataGridView1.Size = new System.Drawing.Size(997, 290);
                }

                this.PatternReload();

                ////共通からSQL文でDataGridViewの列名とソート順を取得する
                //this.IchiranLogic.selectQuery = this.logic.SelectQeury;
                //this.IchiranLogic.orderByQuery = this.logic.OrderByQuery;

                isLoaded = true;

                // ソート条件の初期化
                this.customSortHeader1.ClearCustomSortSetting();

                // フィルタの初期化
                this.customSearchHeader1.ClearCustomSearchSetting();

                //パターン1～5をクリックする時、再検索処理を行う
                //base.OnLoad時にthis.Tableに設定されたヘッダー情報をグリッドに表示する
                if (!this.DesignMode)
                {
                    this.customDataGridView1.DataSource = null;
                    if (this.Table != null)
                    {
                        this.logic.CreateDataGridView(this.Table);
                    }
                }

                // すべてのコントロールを返す
                this.allControl = this.GetAllControl();

                //if (isLoaded == true)
                //{
                //    //明細部をクリアする
                //    this.customDataGridView1.DataSource = null;
                //    //再検索処理を行う
                //    this.IchiranLogic.Search();
                //}

                //isLoaded = true;

                //thongh 2015/10/16 #13526 start
                //読込データ件数の設定
                if (this.customDataGridView1 != null)
                {
                    this.IchiranLogic.headForm.readDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.IchiranLogic.headForm.readDataNumber.Text = "0";
                }
                //thongh 2015/10/16 #13526 end
                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
                }

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }

        #region DataGridViewの列名とソート順を取得する

        /// <summary>
        /// 共通からSQL文でDataGridViewの列名とソート順を取得する
        /// </summary>
        public void SetLogicSelect()
        {
            //共通からSQL文でDataGridViewの列名とソート順を取得する
            this.IchiranLogic.selectQuery = this.logic.SelectQeury;
            this.IchiranLogic.orderByQuery = this.logic.OrderByQuery;
            this.IchiranLogic.joinQuery = this.logic.JoinQuery;
        }
        #endregion

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData(DataTable searchResult)
        {
            try
            {
                LogUtility.DebugMethodStart(searchResult);
            
                this.Table = searchResult;

                if (!this.DesignMode)
                {
                    //アラート件数を設定する（カンマを除く）
                    int alertCount = 0;
                    if (!string.IsNullOrEmpty(this.IchiranLogic.headForm.alertNumber.Text))
                    {
                        alertCount = int.Parse(this.IchiranLogic.headForm.alertNumber.Text.Replace(",", ""));
                    }
                    this.logic.AlertCount = alertCount;
                    //DataGridViewに値の設定を行う
                    this.logic.CreateDataGridView(this.Table);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 一覧画面再表示
        /// </summary>
        public void IchiranUpdate()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 一覧画面再表示
                this.OnLoad(null);
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranUpdate", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// チェック。自前でチェック。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCourseCd_Validating(object sender, CancelEventArgs e)
        {
            // キャンセルボタン時は何もしない
            if (this.ActiveControl == this.CancelButton)
            {
                return;
            }

            //未入力時は名称を初期化する
            if (this.txtCourseCd.Text.Trim().Length == 0)
            {
                this.txtCourseNm.Text = string.Empty;
                return;
            }

            var shortName = this.IchiranLogic.mCourseNameAll.Where(s => s.COURSE_NAME_CD == this.txtCourseCd.Text.Trim().ToUpper()).Select(s => s.COURSE_NAME_RYAKU).FirstOrDefault();
            if (shortName == null)
            {
                this.txtCourseCd.IsInputErrorOccured = true;
                e.Cancel = true;

                MessageBox.Show("コース名称マスタに存在しないコードが入力されました。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.txtCourseCd.SelectAll();
                this.txtCourseNm.Clear();
            }
            else
            {
                this.txtCourseNm.Text = shortName;
            }
        }

        private void txtCourseCd_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // ｺｰｽ情報 ポップアップ初期化
                this.IchiranLogic.PopUpDataInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("COURSE_NAME_CD_Enter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        //#region 車輌有効性チェック
        ///// <summary>
        ///// 車輌有効性チェック  
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SHARYOU_CD_Validating(object sender, CancelEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    var pCd = this.SHARYOU_CD.Text;
        //    var pName = this.SHARYOU_NAME_RYAKU.Text;
        //    var pGosyaCd = this.txtGosyaCDHidden.Text;
        //    if (pCd.Trim() != "")
        //    {
        //        var kv = this.IchiranLogic.GetSharyouInfo(pGosyaCd, pCd);
        //        var messageShowLogic = new MessageBoxShowLogic();

        //        switch (kv.Key)
        //        {
        //            case 0:
        //                this.SHARYOU_NAME_RYAKU.Text = "";
        //                messageShowLogic.MessageBoxShow("E020", "車輌");
        //                e.Cancel = true;
        //                this.SHARYOU_CD.IsInputErrorOccured = true;
        //                break;
        //            case 1:
        //                var dr = kv.Value;
        //                this.SHARYOU_CD.Text = dr.Field<string>("SHARYOU_CD");
        //                this.SHARYOU_NAME_RYAKU.Text = dr.Field<string>("SHARYOU_NAME_RYAKU");
        //                this.txtGosyaCDHidden.Text = dr.Field<string>("GYOUSHA_CD");
        //                break;
        //            default:
        //                SendKeys.Send(" ");
        //                e.Cancel = true;
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        this.SHARYOU_NAME_RYAKU.Text = "";
        //    }

        //    LogUtility.DebugMethodEnd();
        //}
        //#endregion

        /// <summary>
        /// UIForm, ヘッダフォームのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls(this.IchiranLogic.headForm));

            return allControl.ToArray();
        }   

        #endregion

        /// <summary>
        /// パターンボタン更新処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        /// <param name="e">イベントクラス</param>
        /// <param name="ptnNo">パターンNo(0はデフォルトパターンを表示)</param>
        public void PatternButtonUpdate(object sender, System.EventArgs e, int ptnNo = -1)
        {
            if (ptnNo != -1) this.PatternNo = ptnNo;
            this.OnLoad(e);
        }

        #region 車輌有効性チェック
        /// <summary>
        /// 車輌有効性チェック 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.IchiranLogic.CheckSharyouCd())
                {
                    // フォーカス設定
                    this.SHARYOU_CD.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHARYOU_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運転者CD（FocusOutCheckMethodと併用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void UNTENSHA_CDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if(!this.txtSHAIN_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if(string.IsNullOrEmpty(this.txtSHAIN_CD.Text))
                {
                    return;
                }

                this.IchiranLogic.UNTENSHA_CDValidated();

            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 補助員CD（FocusOutCheckMethodと併用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void HOJOIN_CDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.txtSHoujyosyaCd.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.txtSHoujyosyaCd.Text))
                {
                    return;
                }

                this.IchiranLogic.HOJOIN_CDValidated();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運搬業者CD（FocusOutCheckMethodと併用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void UNPAN_GYOUSHA_CDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if(!this.UNPAN_GYOUSHA_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if(string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
                {
                    this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    return;
                }

                this.IchiranLogic.UNPAN_GYOUSHA_CDValidated();

            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 車輌CDEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            // 車輌CDEnter処理
            this.IchiranLogic.sharyouCdEnter(sender, e);
        }
        #endregion
    }
}
