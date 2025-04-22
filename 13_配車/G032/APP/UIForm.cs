// $Id: UIForm.cs 54491 2015-07-03 03:56:01Z quocthang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.IchiranCommon.APP;
using Shougun.Core.Common.IchiranCommon.Const;

namespace Shougun.Core.Allocation.TeikiHaisyaIchiran
{
    [Implementation]
    public partial class UIForm : IchiranSuperForm
    {
        #region フィールド

        /// <summary>
        /// ロジック
        /// </summary>
        private TeikiHaisyaIchiran.LogicCls IchiranLogic;

        /// <summary>
        /// 検索項目のSQL
        /// </summary>
        private string selectQuery = string.Empty;

        /// <summary>
        /// ソート分のSQL
        /// </summary>
        private string orderQuery = string.Empty;

        /// <summary>
        /// UIHeader
        /// </summary>
        private UIHeader header_new;

        /// <summary>
        /// 画面情報初期化フラグ
        /// </summary>
        private Boolean isLoaded;
        private DENSHU_KBN paramIn_DenshuKb;
        /// <summary>
        /// 車輌CD前
        /// </summary>
        public string preSyaryouCD = string.Empty;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.TEIKI_HAISHA, false)
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.InitializeComponent();
                paramIn_DenshuKb = DENSHU_KBN.TEIKI_HAISHA;

                //ロジックに、Header部情報を設定する            
                this.IchiranLogic = new LogicCls(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);

                //社員コードを取得すること
                this.ShainCd = SystemProperty.Shain.CD;
                //Main画面で社員コード値を取得すること
                //this.IchiranLogic.syainCode = Properties.Settings.Default.ShainCd;
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

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                //2012/12/18 追加 PTバグトラブル管理表_東北ICのNo1968対応 start
                this.customSortHeader1.ClearCustomSortSetting();
                //2012/12/18 追加 PTバグトラブル管理表_東北ICのNo1968対応 end

                base.OnLoad(e);

                //画面情報の初期化
                if (isLoaded == false)
                {
                    if (!this.IchiranLogic.WindowInit()) { return; }
                    this.header_new = this.IchiranLogic.headForm;
                    initFrom();

                    this.customSearchHeader1.Visible = true;
                    this.customSearchHeader1.Location = new System.Drawing.Point(0, 140);
                    this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customSortHeader1.Location = new System.Drawing.Point(0, 163);
                    this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customDataGridView1.Location = new System.Drawing.Point(0, 190);
                    this.customDataGridView1.Size = new System.Drawing.Size(997, 230);

                    // Anchorの設定は必ずOnLoadで行うこと
                    if (this.customDataGridView1 != null)
                    {
                        this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    }
                }

                // パターンロード
                this.PatternReload();

                //共通からSQL文でDataGridViewの列名とソート順を取得する
                this.IchiranLogic.selectQuery = this.logic.SelectQeury;
                this.IchiranLogic.orderByQuery = this.logic.OrderByQuery;
                this.IchiranLogic.joinQuery = this.logic.JoinQuery;

                //2013/12/16 追加 パターン更新 start
                ////パターン1～5をクリックする時、再検索処理を行う
                //if (isLoaded == true)
                //{
                //    //2013/12/16 削除（製造課題一覧68） START
                //    //明細部をクリアする
                //    //this.customDataGridView1.DataSource = null;
                //    //2013/12/16 削除（製造課題一覧68） END
                //    //再検索処理を行う
                //    this.IchiranLogic.Search();
                //}
                isLoaded = true;
                // ソート条件の初期化
                this.customSortHeader1.ClearCustomSortSetting();
                // フィルタの初期化
                this.customSearchHeader1.ClearCustomSearchSetting();
                //base.OnLoad時にthis.Tableに設定されたヘッダー情報をグリッドに表示する
                if (!this.DesignMode && !string.IsNullOrEmpty(this.IchiranLogic.selectQuery))
                {
                    this.Table = this.IchiranLogic.GetColumnHeaderOnlyDataTable();

                    if (this.Table != null)
                    {
                        this.customDataGridView1.DataSource = null;
                        this.customDataGridView1.Columns.Clear();
                        this.logic.CreateDataGridView(this.Table);
                        //CreateGridView();
                        if (AppConfig.AppOptions.IsMAPBOX())
                        {
                            this.IchiranLogic.HeaderCheckBoxSupportMapbox();
                        }
                        this.IchiranLogic.HeaderCheckBoxSupport();  //ThangNguyen Add 2015030 #11101
                    }
                }

                this.preSyaryouCD = this.SHARYOU_CD.Text;
                //2013/12/16 追加 パターン更新 end

                //thongh 2015/10/16 #13526 start
                //読込データ件数の設定
                if (this.customDataGridView1 != null)
                {
                    this.header_new.readDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.header_new.readDataNumber.Text = "0";
                }
                //thongh 2015/10/16 #13526 end

                // ボタン活性/非活性制御
                this.IchiranLogic.ButtonEnabledControl();

                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
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
        /// 画面項目の初期表示処理
        /// </summary>
        private void initFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (r_framework.APP.Base.BusinessBaseForm)this.Parent;
                ////日付(from) ：当月の1日
                //DateTime hidukeStart = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                //this.header_new.HIDUKE_FROM.Value = hidukeStart;
                //日付(from) ：ブランク
                //this.header_new.HIDUKE_FROM.Value = null;
                this.header_new.HIDUKE_FROM.Value = parentForm.sysDate;

                ////日付(to): 当月の末日
                //DateTime hidukeEnd = hidukeStart.AddMonths(1).AddDays(-1);
                //this.header_new.HIDUKE_TO.Value = hidukeEnd;
                //日付(to): ブランク
                //this.header_new.HIDUKE_TO.Value = null;
                this.header_new.HIDUKE_TO.Value = parentForm.sysDate;

                //伝票日付RadioButton選択状態
                this.header_new.txtNum_HidukeSentaku.Text = ConstCls.HidukeCD_DenPyou;
                this.header_new.radbtnDenpyouHiduke.Checked = true;

                //読込データ件数：０ [件]
                this.header_new.readDataNumber.Text = "0";
                //明細部：　ブランク
                this.customDataGridView1.DataSource = null;
                this.customDataGridView1.TabIndex = 60;

                //拠点CD
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                string KYOTEN_CD = this.GetUserProfileValue(userProfile, "拠点CD");
                IM_KYOTENDao kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                var kyotenP = kyotenDao.GetDataByCd(KYOTEN_CD);
                //拠点名称
                if (kyotenP != null && KYOTEN_CD != string.Empty)
                {
                    this.header_new.KYOTEN_CD.Text = KYOTEN_CD.PadLeft(this.header_new.KYOTEN_CD.MaxLength, '0'); ;
                    this.header_new.KYOTEN_NAME_RYAKU.Text = kyotenP.KYOTEN_NAME_RYAKU;
                }
                else
                {
                    //拠点CD、拠点 : ブランク
                    this.header_new.KYOTEN_CD.Text = string.Empty;
                    this.header_new.KYOTEN_NAME_RYAKU.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("initFrom", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

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
                    if (!string.IsNullOrEmpty(this.header_new.alertNumber.Text))
                    {
                        alertCount = int.Parse(this.header_new.alertNumber.Text.Replace(",", ""));
                    }
                    this.logic.AlertCount = alertCount;
                    //2013/12/16 追加 パターン更新 start
                    if (this.customDataGridView1.Columns.Contains(ConstCls.ADD_COLUMN_INSATSU_NAME))
                    {
                        //ThangNguyen Delete 2015030 #11101 Start
                        if (this.customDataGridView1.Columns.Count > 0)
                        {
                            this.customDataGridView1.Columns.Remove(ConstCls.ADD_COLUMN_INSATSU_NAME);
                            this.IchiranLogic.HeaderCheckBoxSupport();
                        }
                        //datagridにチェックボックスcolumnを追加
                        /*DgvCustomCheckBoxColumn newColumn = new DgvCustomCheckBoxColumn();
                        newColumn.HeaderText = ConstCls.ADD_COLUMN_INSATSU_NAME;
                        newColumn.Name = ConstCls.ADD_COLUMN_INSATSU;
                        newColumn.ValueType = typeof(System.Boolean);
                        if (this.customDataGridView1.Columns.Count > 0)
                        {
                            this.customDataGridView1.Columns.Insert(0, newColumn);
                        }*/
                        //ThangNguyen Delete 2015030 #11101 End
                        //else
                        //{
                        //    this.form.customDataGridView1.Columns.Add(newColumn);
                        //}
                    }
                    //2013/12/16 追加 パターン更新 end

                    //2013/12/16 修正（製造課題一覧68） START
                    //DataGridViewに値の設定を行う
                    this.logic.CreateDataGridView(this.Table);

                    // システム列を非表示にする
                    this.HideSystemColumn();
                    this.notReadOnlyColumns();

                    //CreateGridView();
                    //2013/12/16 修正（製造課題一覧68）END
                    this.customDataGridViewCSV.DataSource = this.customDataGridView1.DataSource;
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
        /// システム上で必須の列を非表示にします。
        /// </summary>
        internal void HideSystemColumn()
        {
            foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
            {
                if (col.Name == ConstCls.HIDDEN_COLUMN_SYSTEM_ID ||
                    col.Name == ConstCls.HIDDEN_COLUMN_SEQ ||
                    col.Name == ConstCls.HIDDEN_COLUMN_HAISHA_NUMBER ||
                    col.Name == ConstCls.HIDDEN_COLUMN_SAGYOU_DATE ||
                    col.Name == ConstCls.HIDDEN_COLUMN_DETAIL_SYSTEM_ID ||
                    col.Name == ConstCls.HIDDEN_LOCATION ||
                    col.Name == ConstCls.HIDDEN_COLUMN_DETAIL_ROW_NUMBER)
                {
                    col.Visible = false;
                }
            }
        }

        /// <summary>
        /// 地図表示のチェックボックスを使用可能にする
        /// </summary>
        internal void notReadOnlyColumns()
        {
            foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
            {
                // 現状「地図表示」のチェックのみ
                if (col.Name == ConstCls.DATA_TAISHO)
                {
                    col.ReadOnly = false;
                }
            }
        }

        //2013/12/16 追加 パターン更新 start
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
        //2013/12/16 追加 パターン更新 end

        #endregion

        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        public Control[] GetAllControl()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl);
                allControl.AddRange(controlUtil.GetAllControls(this.header_new));
                allControl.AddRange(controlUtil.GetAllControls(this.header_new));

                return allControl.ToArray();
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
        #endregion

        #region DataGridViewに値の設定を行う
        //2013/12/16 修正（製造課題一覧68） START
        /// <summary>
        /// DataGridViewに値の設定を行う
        /// </summary>
        //public void CreateGridView()
        //{
        //    DialogResult result = DialogResult.Yes;
        //    int alertCnt = int.Parse(this.header_new.alertNumber.Text.Replace(",", ""));
        //    if (alertCnt != 0 && alertCnt < this.Table.Rows.Count)
        //    {
        //        MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
        //        result = showLogic.MessageBoxShow("C025");
        //    }
        //    if (result == DialogResult.Yes)
        //    {
        //        // DataGridViewの値をクリア
        //        this.customDataGridView1.DataSource = null;
        //        this.customDataGridView1.Columns.Clear();

        //        this.customSortHeader1.SortDataTable(this.Table);
        //        this.customDataGridView1.DataSource = this.Table;
        //        //2013/12/16 追加 パターン更新 start
        //        if (!this.customDataGridView1.Columns.Contains(ConstCls.ADD_COLUMN_INSATSU))
        //        {
        //            //datagridにチェックボックスcolumnを追加
        //            DgvCustomCheckBoxColumn newColumn = new DgvCustomCheckBoxColumn();
        //            newColumn.HeaderText = ConstCls.ADD_COLUMN_INSATSU_NAME;
        //            newColumn.Name = ConstCls.ADD_COLUMN_INSATSU;
        //            newColumn.ValueType = typeof(System.Boolean);
        //            if (this.customDataGridView1.Columns.Count > 0)
        //            {
        //                this.customDataGridView1.Columns.Insert(0, newColumn);
        //            }
        //            //else
        //            //{
        //            //    this.form.customDataGridView1.Columns.Add(newColumn);
        //            //}
        //        }
        //        //2013/12/16 追加 パターン更新 end
        //        foreach (DataGridViewColumn column in this.customDataGridView1.Columns)
        //        {
        //            column.Width = (column.HeaderText.Length * 10) + 55;
        //            column.ReadOnly = true;
        //            column.SortMode = DataGridViewColumnSortMode.NotSortable;

        //            if (column.ValueType != null)
        //            {
        //                switch (column.ValueType.Name)
        //                {
        //                    case "Int32":
        //                    case "Int64":
        //                    case "UInt32":
        //                    case "UInt64":
        //                    case "Single":
        //                    case "Double":
        //                    case "Decimal":
        //                        // 数値型ならヘッダテキストもセル値も右寄せにする
        //                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //}
        //2013/12/16 修正（製造課題一覧68） END
        #endregion

        /// <summary>
        /// コースマスタの存在チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCourseCd_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // キャンセルボタン or 未入力時は何もしない
                if (null != this.CancelButton && this.ActiveControl == this.CancelButton || this.txtCourseCd.Text.Trim().Length == 0)
                {
                    this.txtCourseNm.Clear();
                    return;
                }

                var shortName = this.IchiranLogic.mCourseNameAll.Where(s => s.COURSE_NAME_CD == this.txtCourseCd.Text.Trim()).Select(s => s.COURSE_NAME_RYAKU).FirstOrDefault();
                if (shortName == null)
                {
                    this.txtCourseCd.IsInputErrorOccured = true;
                    e.Cancel = true;

                    //レコードが存在しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "コース");
                    this.txtCourseCd.SelectAll();
                    this.txtCourseNm.Clear();
                }
                else
                {
                    this.txtCourseNm.Text = shortName;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txtCourseCd_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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

                //var pCd = this.SHARYOU_CD.Text;
                //var pName = this.SHARYOU_NAME_RYAKU.Text;
                //if (this.preSyaryouCD != "" && pCd != this.preSyaryouCD)
                //{
                //    //車輌CD変更の場合
                //    this.txtGosyaCDHidden.Text = "";
                //}
                //var pGosyaCd = this.txtGosyaCDHidden.Text;
                //if (pCd.Trim() != "")
                //{
                //    var kv = this.IchiranLogic.GetSharyouInfo(pGosyaCd, pCd);
                //    var messageShowLogic = new MessageBoxShowLogic();

                //    switch (kv.Key)
                //    {
                //        case 0:
                //            this.SHARYOU_NAME_RYAKU.Text = "";
                //            messageShowLogic.MessageBoxShow("E020", "車輌");
                //            e.Cancel = true;
                //            this.SHARYOU_CD.IsInputErrorOccured = true;
                //            break;
                //        case 1:
                //            var dr = kv.Value;
                //            this.SHARYOU_CD.Text = dr.Field<string>("SHARYOU_CD");
                //            this.SHARYOU_NAME_RYAKU.Text = dr.Field<string>("SHARYOU_NAME_RYAKU");
                //            this.txtGosyaCDHidden.Text = dr.Field<string>("GYOUSHA_CD");
                //            break;
                //        default:
                //            SendKeys.Send(" ");
                //            e.Cancel = true;
                //            break;
                //    }
                //}
                //else
                //{
                //    this.SHARYOU_NAME_RYAKU.Text = "";
                //    this.txtGosyaCDHidden.Text = "";
                //}

                //this.preSyaryouCD = pCd;

                if (!this.IchiranLogic.ChechSharyouCd())
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

                if (!this.txtSHAIN_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.txtSHAIN_CD.Text))
                {
                    return;
                }

                this.IchiranLogic.UNTENSHA_CDValidated();

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

                if (!this.UNPAN_GYOUSHA_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
                {
                    this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    return;
                }

                this.IchiranLogic.UNPAN_GYOUSHA_CDValidated();

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
        /// 車輌CDEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            // 車輌CDEnter処理
            this.IchiranLogic.sharyouCdEnter(sender, e);
        }
    }
        #endregion
}
