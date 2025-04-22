// $Id: UIForm.cs 49120 2015-05-08 04:10:25Z sanbongi $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.SalesManagement.ShiharaiMotocho.Logic;

namespace Shougun.Core.SalesManagement.ShiharaiMotocho.APP
{
	/// <summary>
	/// 支払元帳画面
	/// </summary>
	[Implementation]
	public partial class UIForm : SuperForm
    {
		#region フィールド
		/// <summary>
        /// 支払元帳画面ロジック
        /// </summary>
		private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public UIForm()
			: base(WINDOW_ID.T_SHIHARAI_MOTOCHO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
		{
			// コンポーネントの初期化
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
			this.logic = new ShiharaiMotocho.Logic.LogicClass(this);

			// 完全に固定。ここには変更を入れない
			QuillInjector.GetInstance().Inject(this);
        }

		/// <summary>
		/// 一覧画面再表示
		/// </summary>
		public void IchiranUpdate()
		{
			// 一覧画面再表示
			this.logic.SetIchiran();
		}

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);

            // 画面の初期化
            if (!this.logic.WindowInit())
            {
                return;
            }
            var parentForm = (IchiranBaseForm)this.Parent;
            parentForm.Shown += new EventHandler(this.OnShown);

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e">イベントデータ</param>
        internal virtual void OnShown(object sender, EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            // 範囲指定条件画面表示
            this.Search(null, e);
            var parentForm = (IchiranBaseForm)this.Parent;
            parentForm.Shown -= new EventHandler(this.OnShown);

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }

		/// <summary>
		/// 前
		/// </summary>
		/// <param name="sender">発生元オブジェクト</param>
		/// <param name="e">イベントデータ</param>
		internal virtual void Prev(object sender, EventArgs e)
		{
			// 前の取引先を表示
			this.logic.Prev();
		}

		/// <summary>
		/// 次
		/// </summary>
		/// <param name="sender">発生元オブジェクト</param>
		/// <param name="e">イベントデータ</param>
		internal virtual void Next(object sender, EventArgs e)
		{
			// 次の取引先を表示
			this.logic.Next();
		}

		/// <summary>
		/// 印刷
		/// </summary>
		/// <param name="sender">発生元オブジェクト</param>
		/// <param name="e">イベントデータ</param>
		internal virtual void Print(object sender, EventArgs e)
		{
			// 印刷出力
			this.logic.Print();
		}

		/// <summary>
		/// CSV
		/// </summary>
		/// <param name="sender">発生元オブジェクト</param>
		/// <param name="e">イベントデータ</param>
		internal virtual void CSV(object sender, EventArgs e)
		{
			// CSV出力
			this.logic.CSV();
		}

		/// <summary>
		/// 範囲条件指定
		/// </summary>
		/// <param name="sender">発生元オブジェクト</param>
		/// <param name="e">イベントデータ</param>
		internal virtual void Search(object sender, EventArgs e)
		{
			// 範囲条件指定画面表示
			this.logic.ShowPopUp(MotochoHaniJokenPopUp.Const.UIConstans.DispType.SHIHARAI);
		}

		/// <summary>
		/// Formクローズ処理
		/// </summary>
		/// <param name="sender">発生元オブジェクト</param>
		/// <param name="e">イベントデータ</param>
		internal virtual void FormClose(object sender, EventArgs e)
		{
			// Formクローズ
			var parentForm = (IchiranBaseForm)this.Parent;

			this.Close();
			parentForm.Close();
		}

		/// <summary>
		/// セルダブルクリック処理
		/// </summary>
		/// <param name="sender">発生元オブジェクト</param>
		/// <param name="e">イベントデータ</param>
		internal virtual void CellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
		{
			if(e.RowIndex >= 0)
			{
				// 選択したセルの伝票番号を用いて修正入力画面を開く
				this.logic.UpdateWindowShow();
			}
		}
    }
}
