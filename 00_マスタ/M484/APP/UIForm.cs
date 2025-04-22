// $Id: UIForm.cs 32785 2014-10-20 07:21:09Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Message;
using Shougun.Core.Master.HikiaiTorihikisakiIchiran.Logic;

namespace Shougun.Core.Master.HikiaiTorihikisakiIchiran.APP
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        /// <summary>
        /// ビジネスロジック
        /// </summary>
        private LogicClass businessLogic;

        /// <summary>
        /// イベントフラグ
        /// </summary>
        internal bool EventSetFlg = false;

        /// <summary>
        /// ロード済かどうかを示す
        /// </summary>
        internal bool isLoaded = false;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn"></param>
        public UIForm(DENSHU_KBN denshuKbn)
            : base(denshuKbn, false)
        {
            try
            {
                LogUtility.DebugMethodStart(denshuKbn);

                InitializeComponent();

                // 社員CDを取得
                this.ShainCd = SystemProperty.Shain.CD;
                this.logic.SettingAssembly = Assembly.GetExecutingAssembly();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("UIForm", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

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
                if (!this.isLoaded)
                {
                    // ビジネスロジックの初期化
                    this.businessLogic = new LogicClass(this);
                    this.businessLogic.WindowInit();

                    this.customSearchHeader1.Visible = true;
                    this.customSearchHeader1.Location = new System.Drawing.Point(4, 120);
                    this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customSortHeader1.Location = new System.Drawing.Point(4, 142);
                    this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customDataGridView1.Location = new System.Drawing.Point(3, 169);
                    this.customDataGridView1.Size = new System.Drawing.Size(997, 275);

                    this.bt_ptn1.Location = new System.Drawing.Point(this.bt_ptn1.Location.X, 450);
                    this.bt_ptn2.Location = new System.Drawing.Point(this.bt_ptn2.Location.X, 450);
                    this.bt_ptn3.Location = new System.Drawing.Point(this.bt_ptn3.Location.X, 450);
                    this.bt_ptn4.Location = new System.Drawing.Point(this.bt_ptn4.Location.X, 450);
                    this.bt_ptn5.Location = new System.Drawing.Point(this.bt_ptn5.Location.X, 450);

                    // 汎用検索機能が未実装の為、汎用検索は非表示
                    this.searchString.Visible = false;

                    //表示条件設定
                    this.businessLogic.RemoveIchiranHyoujiJoukenEvent();
                    this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                    this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
                    this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
                    this.businessLogic.AddIchiranHyoujiJoukenEvent();
                    if (!this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                    {
                        this.businessLogic.SetHyoujiJoukenInit();
                    }

                    // Anchorの設定は必ずOnLoadで行うこと
                    if (this.customDataGridView1 != null)
                    {
                        this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    }
                }

                // パターン読み込み（初回のみデフォルトパターン選択）
                this.PatternReload(!this.isLoaded);

                // フィルタの初期化
                this.customSearchHeader1.ClearCustomSearchSetting();

                if (!this.DesignMode)
                {
                    this.logic.CreateDataGridView(this.Table);
                }

                this.isLoaded = true;

                //thongh 2015/10/16 #13526 start
                //読込データ件数の設定
                if (this.customDataGridView1 != null)
                {
                    this.businessLogic.headerForm.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.businessLogic.headerForm.ReadDataNumber.Text = "0";
                }
                //thongh 2015/10/16 #13526 end
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("OnLoad", ex);
                throw ex;
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
        /// 一覧表示条件チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ICHIRAN_HYOUJI_JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = (CheckBox)sender;
            if (!item.Checked)
            {
                if (!this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    MessageBoxUtility.MessageBoxShow("E001", "表示条件");
                    item.Checked = true;
                }
            }
        }

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!this.DesignMode)
                {
                    // 表示件数を取得
                    var rowCount = 0;
                    if (this.Table != null)
                    {
                        rowCount = this.Table.Rows.Count;
                    }

                    // 明細に表示
                    this.logic.AlertCount = this.businessLogic.GetAlertCount();
                    this.logic.CreateDataGridView(this.Table);
                    this.HideKeyColumn();

                    // 検索件数を設定し、画面に表示
                    var parentForm = base.Parent;
                    var readDataNumber = (TextBox)controlUtil.FindControl(parentForm, "ReadDataNumber");
                    if (this.Table != null && readDataNumber != null)
                    {
                        readDataNumber.Text = rowCount.ToString();
                    }

                    // ソート用ヘッダーの表示・非表示
                    if (this.customSortHeader1 != null)
                    {
                        if (this.Table != null)
                        {
                            this.customSortHeader1.Visible = true;
                        }
                        else
                        {
                            this.customSortHeader1.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("ShowData", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// キーカラムを非表示にします
        /// </summary>
        public void HideKeyColumn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.customDataGridView1.Columns.Contains(this.businessLogic.KEY_ID1))
                {
                    this.customDataGridView1.Columns[this.businessLogic.KEY_ID1].Visible = false;
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
        /// 検索処理
        /// </summary>
        /// <param name="clearFlg"></param>
        public virtual void GetPattern(bool isClear)
        {
            try
            {
                LogUtility.DebugMethodStart(isClear);

                if (!this.DesignMode)
                {
                    if (isClear)
                    {
                        this.SimpleSearchSettings = string.Empty;
                    }
                    else
                    {
                        this.SimpleSearchSettings = this.businessLogic.GetSearchString();
                    }

                    //this.logic.GetPattern();
                    string sql = string.Empty;
                    if (isClear)
                    {
                        sql = "select " + this.logic.SelectQeury +
                                " from M_HIKIAI_TORIHIKISAKI" +
                                " LEFT JOIN M_SHAIN ON M_SHAIN.SHAIN_CD = M_HIKIAI_TORIHIKISAKI.EIGYOU_TANTOU_CD" +
                                " where 1 = 0" +
                                " order by " + this.logic.OrderByQuery;
                    }
                    else if (!string.IsNullOrWhiteSpace(this.SimpleSearchSettings))
                    {
                        sql = "select " + this.logic.SelectQeury +
                                " from M_HIKIAI_TORIHIKISAKI"+
                                " LEFT JOIN M_SHAIN ON M_SHAIN.SHAIN_CD = M_HIKIAI_TORIHIKISAKI.EIGYOU_TANTOU_CD" +
                                " where " + this.SimpleSearchSettings +
                                " order by " + this.logic.OrderByQuery;
                    }
                    else
                    {
                        sql = "select " + this.logic.SelectQeury +
                                " from M_HIKIAI_TORIHIKISAKI" +
                                " LEFT JOIN M_SHAIN ON M_SHAIN.SHAIN_CD = M_HIKIAI_TORIHIKISAKI.EIGYOU_TANTOU_CD" +
                                " order by " + this.logic.OrderByQuery;
                    }
                    this.Table = this.businessLogic.daoTorihikisaki.GetDateForStringSql(sql);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("GetPattern", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CDの入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TORIHIKISAKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.businessLogic.TORIHIKISAKI_CD_Validating(sender, e);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("TORIHIKISAKI_CD_Validating", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 引合取引先一覧のShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // 初期フォーカス位置を設定します
            this.TORIHIKISAKI_CD.Focus();
        }
    }
}

