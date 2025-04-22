
using System;
using System.Data;
using System.Reflection;
using KyoutsuuIchiran.APP;
using r_framework;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Container.Factory;
using Seasar.Quill.Attrs;
using Seasar.Framework.Exceptions;

namespace KyoutsuuIchiran.Logic
{
    /// <summary>
    /// 共通一覧画面のロジッククラス
    /// </summary>
    public class KyoutsuuIchiranLogic : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "KyoutsuuIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private KyoutsuuIchiranForm form;

        /// <summary>
        /// 共通一覧画面にて利用されるDao
        /// </summary>
        private IchiranBaseDao dao;
        #endregion

        #region プロパティ
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public string SearchString { get; set; }
        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal KyoutsuuIchiranLogic(KyoutsuuIchiranForm targetForm)
        {
            this.form = targetForm;
            switch (form.WindowId)
            {
                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    // Daoの生成
                    this.dao = (IM_GENBADao)SingletonS2ContainerFactory.Container.GetComponent(typeof(IM_GENBADao));
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                //親の初期化
                this.ButtonInit();
                this.SetIchiranTemplate();
                this.EventInit();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CcreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.ICHIRAN_WINDOW_FLAG:
                        cont.Text = button.IchiranButtonName;
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();
            var parentForm = (BusinessBaseForm)this.form.Parent;

            //処理番号ボタンのイベント生成
            parentForm.bt_process1.Click += (object sender, EventArgs e) => this.ProcessButton1();
            parentForm.bt_process2.Click += (object sender, EventArgs e) => this.ProcessButton2();
            parentForm.bt_process3.Click += (object sender, EventArgs e) => this.ProcessButton3();
            parentForm.bt_process4.Click += (object sender, EventArgs e) => this.ProcessButton4();

            //パターンボタンのイベント生成
            this.form.PatternButton1.Click += (object sender, EventArgs e) => this.PatterButton1();
            this.form.PatternButton2.Click += (object sender, EventArgs e) => this.PatterButton2();
            this.form.PatternButton3.Click += (object sender, EventArgs e) => this.PatterButton3();
            this.form.PatternButton4.Click += (object sender, EventArgs e) => this.PatterButton4();
            this.form.PatternButton5.Click += (object sender, EventArgs e) => this.PatterButton5();

            //閉じるボタンのイベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //テンプレートのセルをダブルクリックした場合のイベント生成
            this.form.Ichiran.CellDoubleClick += (object sender, GrapeCity.Win.MultiRow.CellEventArgs e) => this.DetailDoubleClick();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一覧のテンプレートを設定する
        /// </summary>
        private void SetIchiranTemplate()
        {
            LogUtility.DebugMethodStart();
            switch (this.form.WindowId)
            {
                //ケースで分けるのは多くなるから別途対策を考える
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    // パターン一覧
                    this.form.Ichiran.Template = new KyoutsuuIchiran.MultiRowTemplate.KokyakuIchiran();
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターンボタンについて名称の設定を行う
        /// 未実装
        /// </summary>
        private void SetPatterButtonName()
        {
            LogUtility.DebugMethodStart();
            // 検索パターンについてDBからボタン名を取得する

            // 検索パターンボタンの名称を設定する
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CcreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }
        #endregion

        #region イベント処理
        /// <summary>
        /// 一覧の行をダブルクリックした時の処理
        /// </summary>
        private void DetailDoubleClick()
        {
            LogUtility.DebugMethodStart();
            this.OpenSubWindow(this.form.Ichiran.SelectedRows[0].Index);
            LogUtility.DebugMethodEnd();
        }

        #region 処理ボタン押下処理
        /// <summary>
        /// 処理１ボタン押下処理
        /// </summary>
        private void ProcessButton1()
        {
            // 個別にクラス化したほうがいいような
            // 共通検索一覧のため、switchで分けると大量の分岐が必要となる
            // 要検討
            LogUtility.DebugMethodStart();

            switch (this.form.WindowId)
            {
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    // 並び替え
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 処理２ボタン押下処理
        /// パターン一覧画面を表示する
        /// </summary>
        private void ProcessButton2()
        {
            LogUtility.DebugMethodStart();

            switch (this.form.WindowId)
            {
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    // パターン一覧
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();

        }
        /// <summary>
        /// 処理3ボタン押下処理
        /// 一覧のソートを行う
        /// 実施場所不明のため、仮実装
        /// </summary>
        private void ProcessButton3()
        {
            LogUtility.DebugMethodStart();
            // 並び替え
            this.Sort();
            LogUtility.DebugMethodEnd();

        }


        /// <summary>
        /// 処理3ボタン押下処理
        /// 一覧のソートを行う
        /// 実施場所不明のため、仮実装
        /// </summary>
        private void ProcessButton4()
        {
            M_GENBA entity = new M_GENBA();

            var entityList = new M_GENBA[10];

            for (int i = 0; i < entityList.Length; i++)
            {
                entityList[i] = new M_GENBA();
            }

            var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_GENBA>(entityList);
            var entitys = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);
            dataBinderLogic.CreateDataTableForEntity(this.form.Ichiran, entitys);
        }

        #endregion

        #region パターンボタン押下処理
        /// <summary>
        /// パターン1ボタン押下処理
        /// </summary>
        private void PatterButton1()
        {
            LogUtility.DebugMethodStart();
            switch (this.form.WindowId)
            {
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// パターン2ボタン押下処理
        /// </summary>
        private void PatterButton2()
        {
            LogUtility.DebugMethodStart();
            switch (this.form.WindowId)
            {
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターン3ボタン押下処理
        /// </summary>
        private void PatterButton3()
        {
            LogUtility.DebugMethodStart();
            switch (this.form.WindowId)
            {
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターン4ボタン押下処理
        /// </summary>
        private void PatterButton4()
        {
            LogUtility.DebugMethodStart();
            switch (this.form.WindowId)
            {
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターン5ボタン押下処理
        /// </summary>
        private void PatterButton5()
        {
            LogUtility.DebugMethodStart();
            switch (this.form.WindowId)
            {
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #endregion

        #region privateLogic
        /// <summary>
        /// 一覧のソートを行う
        /// </summary>
        private void Sort()
        {
            LogUtility.DebugMethodStart();

            SortUtility.Cells = this.form.Ichiran.ColumnHeaders[0].Cells;
            var sortItemList = SortUtility.DoSort();
            if (sortItemList.Length != 0)
            {
                this.form.Ichiran.Sort(sortItemList);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// キー情報を基に、対応する画面の表示を行う
        /// </summary>
        /// <param name="rowNumber">一覧の選択された行番号</param>
        private void OpenSubWindow(int rowNumber)
        {
            LogUtility.DebugMethodStart();
            switch (this.form.WindowId)
            {
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region Equals/GetHashCode/ToString
        public bool Equals(KyoutsuuIchiranLogic other)
        {
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        /// <summary>
        /// 検索処理を行う
        /// </summary>
        [Transaction]
        public virtual int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResult = this.dao.GetIchiranData(this.SearchString);
                LogUtility.DebugMethodEnd();
                return SearchResult.Rows.Count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// ユーザが設定を行った条件にて検索を行う
        /// </summary>
        [Transaction]
        public virtual int UserSettingSearch(string sql)
        {
            //SQL文とパラメータを別々で送ってS2Daoで組み当ててもらえるように
            //SQLインジェクション要注意
            LogUtility.DebugMethodStart(sql);
            this.SearchResult = this.dao.GetUserSettingData(sql);
            LogUtility.DebugMethodEnd();
            return SearchResult.Rows.Count;
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
    }
}
