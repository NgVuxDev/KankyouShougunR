using System;
using System.Reflection;
using System.Windows.Forms;
using GyoushuHoshu.APP;
using KyoutsuuIchiran.APP;
using ManifestKansanHoshu.APP;
using NyuukinsakiNyuuryokuHoshu.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Setting;
using ShainHoshu.APP;
using TorihikisakiIchiran.APP;
using MenuList.APP;

namespace kankyouShogunMainMenu.APP
{
    /// <summary>
    /// メインメニュークラス
    /// </summary>
    public partial class MainMenu : SuperForm
    {
        private SuperForm superForm = new SuperForm();

        private BusinessBaseForm BusinessBaseForm;

        private MasterBaseForm masterBaseForm;

        private FormControlLogic formLogic;

        private ShainHoshuForm shainHoshu;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();

            this.formLogic = new FormControlLogic();

            shainHoshu = new ShainHoshuForm();

            //QuillInjector.GetInstance().Inject(this);
        }

        public void MainMenu_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }


            //MenuSetting menuSetting = new MenuSetting();
            //var RibbonTabSettingDtoList = menuSetting.LoadMenuSetting();


            //foreach (var ribbonSetting in RibbonTabSettingDtoList)
            //{
            //    foreach (var menuSettingDto in ribbonSetting.MenuSettingDto)
            //    {

            //    }
            //}

            //プロパティにて使用可能となっている場合は対象のパネルを表示し、クリック時のイベントを生成する
            this.受付収集.Visible = kankyouShogunMainMenu.Properties.Settings.Default.UketsukeShushu;
            if (this.受付収集.Visible)
            {
                this.受付収集ボタン.Click += new System.EventHandler(this.UketsukeShushuWindowView);
            }

            //ログイン前に環境のチェックを行う
            //XMLから設定値を読み込みメニューの生成

            // 各画面の検索条件をクリア
            //ShainHoshu.Properties.Settings.Default.Reset();
        }
        /// <summary>
        /// ボタン１押下時処理
        /// </summary>
        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            var form = new GyoushuHoshuForm();
            if (form.IsDisposed)
            {
                return;
            }
            masterBaseForm = new MasterBaseForm(form, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
            masterBaseForm.Show();
        }

        /// <summary>
        /// 受付収集画面表示イベント
        /// </summary>
        private void UketsukeShushuWindowView(object sender, EventArgs e)
        {
            //各アセンブリの読み込みを同一メソッドで行えるように
            // XMLにて読み込みを行うように
            var assembly = Assembly.LoadFrom(MenuConstants.UKETSUKE_SHUSHU_ASSEMBLY_NAME);
            superForm = (SuperForm)assembly.CreateInstance(
                    MenuConstants.UKETSUKE_SHUSHU_FORM_NAME, // 名前空間を含めたクラス名
                    false, // 大文字小文字を無視するかどうか
                    BindingFlags.CreateInstance, // インスタンスを生成
                    null,
                    new object[] { WINDOW_TYPE.NEW_WINDOW_FLAG, null }, // コンストラクタの引数
                    null,
                    null
                  );
            if (superForm.IsDisposed)
            {
                return;
            }
            BusinessBaseForm = new BusinessBaseForm(superForm, WINDOW_TYPE.NEW_WINDOW_FLAG);
            var flag = formLogic.ScreenPresenceCheck(superForm);
            if (!flag)
            {
                BusinessBaseForm.Show();
            }
        }
        /// <summary>
        /// ボタン3押下時処理
        /// </summary>
        private void ribbonButton3_Click(object sender, EventArgs e)
        {
            var form = new TorihikisakiIchiranForm(DENSHU_KBN.TORIHIKISAKI);

            if (form.IsDisposed)
            {
                return;
            }
            var BusinessBaseForm = new BusinessBaseForm(form, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            var flag = formLogic.ScreenPresenceCheck(form);
            if (!flag)
            {
                BusinessBaseForm.Show();
            }


            form.Show();

        }
        /// <summary>
        /// ボタン4押下時処理
        /// </summary>
        private void ribbonButton4_Click(object sender, EventArgs e)
        {
            var assembly = Assembly.LoadFrom(MenuConstants.UKETSUKE_SHUSHU_ASSEMBLY_NAME);
            var form = new ManifestKansanHoshuForm();
            if (form.IsDisposed)
            {
                return;
            }
            var BusinessBaseForm = new MasterBaseForm(form, WINDOW_TYPE.UPDATE_WINDOW_FLAG, true);
            var flag = formLogic.ScreenPresenceCheck(form);
            if (!flag)
            {
                BusinessBaseForm.Show();
            }

        }
        /// <summary>
        /// ボタン5押下時処理
        /// </summary>
        private void ribbonButton5_Click(object sender, EventArgs e)
        {
            var form = new NyuukinsakiNyuuryokuHoshuForm();
            if (form.IsDisposed)
            {
                return;
            }
            var masterBaseForm = new BusinessBaseForm(form, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            var flag = formLogic.ScreenPresenceCheck(form);
            if (!flag)
            {
                masterBaseForm.Show();
            }
        }
        /// <summary>
        /// ボタン11押下時処理
        /// </summary>
        private void ribbonButton11_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(@"環境将軍を終了してもよろしいですか？",
            @"終了",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        /// <summary>
        /// 検索ボタン押下処理
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Search();
        }
        /// <summary>
        /// クイック検索処理
        /// </summary>
        private void Search()
        {

            var cl = new KyoutsuuIchiranForm(WINDOW_ID.KOKYAKU_ITIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG, txb_quickSearch.Text);
            if (cl.IsDisposed)
            {
                return;
            }
            var form = new BusinessBaseForm(cl, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            var flag = formLogic.ScreenPresenceCheck(cl);
            if (form.IsDisposed)
            {
                return;
            }
            if (!flag)
            {
                form.Show();
            }
        }
        /// <summary>
        /// ファンクションキー押下処理
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            RibbonTab tab = null;
            if (e.KeyCode == Keys.F1)
            {
                tab = ribbonTab1;
            }
            if (e.KeyCode == Keys.F2)
            {
                tab = ribbonTab2;
            }

            if (e.KeyCode == Keys.F3)
            {
                tab = ribbonTab3;
            }

            if (e.KeyCode == Keys.F4)
            {
                tab = ribbonTab4;
            }

            if (e.KeyCode == Keys.F5)
            {
                tab = ribbonTab5;
            }

            if (e.KeyCode == Keys.F6)
            {
                tab = ribbonTab6;
            }

            if (tab != null)
            {
                ribbon1.ActiveTab = tab;
            }
        }
        /// <summary>
        /// 受付収集削除ボタン押下
        /// </summary>
        private void ribbonButton1_Click_1(object sender, EventArgs e)
        {
            var assembly = Assembly.LoadFrom(MenuConstants.UKETSUKE_SHUSHU_ASSEMBLY_NAME);
            var form = (SuperForm)assembly.CreateInstance(
                    MenuConstants.UKETSUKE_SHUSHU_FORM_NAME, // 名前空間を含めたクラス名
                    false, // 大文字小文字を無視するかどうか
                    BindingFlags.CreateInstance, // インスタンスを生成
                    null,
                    new object[] { WINDOW_TYPE.DELETE_WINDOW_FLAG, 3 }, // コンストラクタの引数
                    null,
                    null
                  );
            if (form.IsDisposed)
            {
                return;
            }
            var BusinessBaseForm = new BusinessBaseForm(form, WINDOW_TYPE.DELETE_WINDOW_FLAG);
            var flag = formLogic.ScreenPresenceCheck(form);
            if (!flag)
            {
                BusinessBaseForm.Show();
            }

        }

        /// <summary>
        /// 受付出荷（登録）画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 受付出荷ボタン_Click(object sender, EventArgs e)
        {
            ShowShukkaUketsuke(MenuConstants.UKETSUKE_SHUKKA_ASSEMBLY_NAME, MenuConstants.UKETSUKE_SHUKKA_FORM_NAME, WINDOW_TYPE.NEW_WINDOW_FLAG, null);
        }

        /// <summary>
        /// 受付出荷（参照）画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 受付出荷参照ボタン_Click(object sender, EventArgs e)
        {



            if (shainHoshu.IsDisposed)
            {
                return;
            }

            masterBaseForm = new MasterBaseForm(shainHoshu, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);

            masterBaseForm.Show();
            //ShowShukkaUketsuke(MenuConstants.UKETSUKE_SHUKKA_ASSEMBLY_NAME, MenuConstants.UKETSUKE_SHUKKA_FORM_NAME, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, GetMaxKey());
        }

        /// <summary>
        /// 受付出荷（更新）画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 受付出荷更新ボタン_Click(object sender, EventArgs e)
        {
            ShowShukkaUketsuke(MenuConstants.UKETSUKE_SHUKKA_ASSEMBLY_NAME, MenuConstants.UKETSUKE_SHUKKA_FORM_NAME, WINDOW_TYPE.UPDATE_WINDOW_FLAG, GetMaxKey());
        }

        /// <summary>
        /// 受付出荷（削除）画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 受付出荷削除ボタン_Click(object sender, EventArgs e)
        {
            ShowShukkaUketsuke(MenuConstants.UKETSUKE_SHUKKA_ASSEMBLY_NAME, MenuConstants.UKETSUKE_SHUKKA_FORM_NAME, WINDOW_TYPE.DELETE_WINDOW_FLAG, GetMaxKey());
        }

        /// <summary>
        /// 指定されたモードで出荷受付画面を表示
        /// </summary>
        /// <param name="menuConstAsmb"></param>
        /// <param name="menuConstForm"></param>
        /// <param name="windowType"></param>
        /// <param name="obj"></param>
        private void ShowShukkaUketsuke(string menuConstAsmb, string menuConstForm, WINDOW_TYPE windowType, object obj)
        {
            //各アセンブリの読み込みを同一メソッドで行えるように
            // XMLにて読み込みを行うように
            var assembly = Assembly.LoadFrom(menuConstAsmb);
            superForm = (SuperForm)assembly.CreateInstance(
                    menuConstForm, // 名前空間を含めたクラス名
                    false, // 大文字小文字を無視するかどうか
                    BindingFlags.CreateInstance, // インスタンスを生成
                    null,
                    new object[] { windowType, obj }, // コンストラクタの引数
                    null,
                    null
                  );

            if (superForm.IsDisposed)
            {
                return;
            }
            BusinessBaseForm = new BusinessBaseForm(superForm, windowType);
            BusinessBaseForm.Show();
        }

        /// <summary>
        /// 受付番号の最大値取得（テスト用）
        /// </summary>
        /// <returns></returns>
        private int GetMaxKey()
        {
            var dao = (r_framework.Dao.IT_UKETSUKE_SLIPDao)Seasar.Framework.Container.Factory.SingletonS2ContainerFactory.Container.GetComponent(typeof(r_framework.Dao.IT_UKETSUKE_SLIPDao));

            return dao.GetMaxKey();
        }

		/// <summary>
		/// 全メニュー表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ribbonOrbMenuItem7_Click(object sender, EventArgs e)
		{
			//各アセンブリの読み込みを同一メソッドで行えるように
			// XMLにて読み込みを行うように
			var assembly = Assembly.LoadFrom(MenuConstants.MENU_LIST_ASSEMBLY_NAME);
			superForm = (SuperForm)assembly.CreateInstance(
					MenuConstants.MENU_LIST_FORM_NAME, // 名前空間を含めたクラス名
					false, // 大文字小文字を無視するかどうか
					BindingFlags.CreateInstance, // インスタンスを生成
					null,
					null,
					null,
					null
				  );

			if(superForm.IsDisposed)
			{
				return;
			}
			masterBaseForm = new MasterBaseForm(superForm, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
			masterBaseForm.Show();
		}

		/// <summary>
        /// 社員保守
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonOrbMenuItem8_Click(object sender, EventArgs e)
        {
            //各アセンブリの読み込みを同一メソッドで行えるように
            // XMLにて読み込みを行うように
            var assembly = Assembly.LoadFrom(MenuConstants.SHAIN_HOSHU_ASSEMBLY_NAME);
            superForm = (SuperForm)assembly.CreateInstance(
                    MenuConstants.SHAIN_HOSHU_FORM_NAME, // 名前空間を含めたクラス名
                    false, // 大文字小文字を無視するかどうか
                    BindingFlags.CreateInstance, // インスタンスを生成
                    null,
                    null,
                    null,
                    null
                  );

            if (superForm.IsDisposed)
            {
                return;
            }
            masterBaseForm = new MasterBaseForm(superForm, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
            masterBaseForm.Show();
        }

        private void txb_quickSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                Search();
            }
        }

    }
}
