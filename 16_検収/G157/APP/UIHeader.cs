// $Id: UIForm.cs 4357 2013-10-22 00:18:55Z sys_dev_12 $
using System;
using r_framework.APP.Base;

namespace Shougun.Core.Inspection.KenshuMeisaiNyuryoku
{
	/// <summary>
	/// 検収入力画面Header
	/// </summary>
	public partial class UIHeader : HeaderBaseForm
    {
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public UIHeader()
        {
			// コンポーネントの初期化
			InitializeComponent();
        }

		/// <summary>
		/// 画面ロード
		/// </summary>
		/// <param name="e">イベント</param>
		protected override void OnLoad(EventArgs e)
        {
			// 親クラスのロード
			base.OnLoad(e);
        }
    }
}
