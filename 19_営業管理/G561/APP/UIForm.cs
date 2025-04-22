using System;
using System.Reflection;
using System.Windows.Forms;
using ShouninzumiDenshiShinseiIchiran.Const;
using ShouninzumiDenshiShinseiIchiran.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.Message;
using r_framework.Dto;

namespace ShouninzumiDenshiShinseiIchiran.APP
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {

    //    /// <summary>
    //    /// ビジネスロジック
    //    /// </summary>
    //    private LogicClass businessLogic;

    //    /// <summary>
    //    /// イベントフラグ
    //    /// </summary>
    //    internal bool IsLoaded = false;

    //    /// <summary>
    //    /// ヘッダーオブジェクト
    //    /// </summary>
    //    internal HeaderForm headerForm;

    //    /// <summary>
    //    /// コンストラクタ
    //    /// </summary>
    //    /// <param name="denshuKbn"></param>
    //    public ShouninzumiDenshiShinseiIchiranForm(DENSHU_KBN denshuKbn)
    //        : base(denshuKbn)
    //    {
    //        InitializeComponent();
    //        this.logic.SettingAssembly = Assembly.GetExecutingAssembly();

    //        // 社員CDを取得すること
    //        this.ShainCd = SystemProperty.Shain.CD;

    //        // 伝種区分
    //        this.DenshuKbn = denshuKbn;
    //    }

    //    /// <summary>
    //    /// 画面Load処理
    //    /// </summary>
    //    /// <param name="e"></param>
    //    protected override void OnLoad(EventArgs e)
    //    {
    //        base.OnLoad(e);
    //        if (!this.IsLoaded)
    //        {
    //            // ヘッダフォームを取得
    //            this.headerForm = (HeaderForm)((BusinessBaseForm)this.ParentForm).headerForm;

    //            // ビジネスロジックの初期化
    //            this.businessLogic = new LogicClass(this);
    //            this.businessLogic.WindowInit();

    //            // 汎用検索機能が未実装の為、汎用検索は非表示
    //            this.searchString.Visible = false;

    //            // 非表示項目の登録
    //            this.SetHiddenColumns(this.businessLogic.KEY_ID1, this.businessLogic.KEY_ID2);
    //        }

    //        this.PatternReload(!this.IsLoaded);

    //        this.IsLoaded = true;

    //        if (!this.DesignMode)
    //        {
    //            this.logic.CreateDataGridView(this.Table);
    //        }
    //    }

    //    /// <summary>
    //    /// 検索結果表示処理
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    public virtual void ShowData()
    //    {
    //        if (!this.DesignMode)
    //        {
    //            DialogResult dlgResult = System.Windows.Forms.DialogResult.Yes;
    //            var rowCount = 0;

    //            // 表示件数を取得
    //            if (this.Table != null)
    //            {
    //                rowCount = this.Table.Rows.Count;
    //            }

    //            // アラート件数を設定し、検索実行
    //            this.logic.AlertCount = this.businessLogic.GetAlertCount();

    //            if (dlgResult == DialogResult.Yes)
    //            {
    //                // 明細に表示
    //                this.logic.CreateDataGridView(this.Table);

    //                // 検索件数を設定し、画面に表示
    //                this.headerForm.ReadDataNumber.Text = rowCount.ToString();

    //                // ソート用ヘッダーの表示・非表示
    //                if (this.customSortHeader1 != null)
    //                {
    //                    if (this.Table != null)
    //                    {
    //                        this.customSortHeader1.Visible = true;
    //                    }
    //                    else
    //                    {
    //                        this.customSortHeader1.Visible = false;
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 一覧表示条件チェック処理
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    public virtual void ICHIRAN_HYOUJI_JOUKEN_CheckedChanged(object sender, EventArgs e)
    //    {
    //        CheckBox item = (CheckBox)sender;
    //        if (!item.Checked)
    //        {
    //            if (!this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
    //            {
    //                MessageBoxUtility.MessageBoxShow("E001", "表示条件");
    //                item.Checked = true;
    //            }
    //        }
    //    }
    }
}

