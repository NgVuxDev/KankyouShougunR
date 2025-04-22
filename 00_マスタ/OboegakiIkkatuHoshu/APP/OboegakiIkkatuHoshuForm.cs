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
using r_framework.Intercepter;
using r_framework.Logic;
using OboegakiIkkatuHoshu.Logic;
using r_framework.Utility;
using Seasar.Quill;
using r_framework.Entity;
namespace OboegakiIkkatuHoshu.APP
{
    public partial class OboegakiIkkatuHoshuForm : SuperForm
    {
        #region 
        /// <summary>
        /// 覚書一括入力画面ロジック
        /// </summary>
        private OboegakiIkkatuHoshuLogic logic;
        /// <summary>
        /// 伝票番号
        /// </summary>
        private String mDenpyouNumber;
        public bool mstartFlg = false;



        #endregion

        #region コンストラクタ
        /// <summary>
        ///  コンストラクタ(新規用)
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        public OboegakiIkkatuHoshuForm()
            : base(WINDOW_ID.M_OBOE_IKKATSU, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            this.InitializeComponent();
            //伝票番号
            this.mDenpyouNumber = string.Empty;
            this.mstartFlg = true;
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new OboegakiIkkatuHoshuLogic(this);
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

               /// <summary>
        /// コンストラクタ(修正削除複写用)
        /// </summary>
        public OboegakiIkkatuHoshuForm(WINDOW_TYPE windowType, T_ITAKU_MEMO_IKKATSU_ENTRY ItakuMemoIkkatsuEntryEntity)
            : base(WINDOW_ID.M_OBOE_IKKATSU, windowType)
        {
            InitializeComponent();
           
            //伝票番号
            this.mDenpyouNumber = ItakuMemoIkkatsuEntryEntity.DENPYOU_NUMBER.ToString();
            base.WindowType = windowType;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new OboegakiIkkatuHoshuLogic(this);
            this.logic.mDenpyouNumber = ItakuMemoIkkatsuEntryEntity.DENPYOU_NUMBER.ToString();
            this.txtSystemId.Text = ItakuMemoIkkatsuEntryEntity.SYSTEM_ID.ToString();
            this.txtSeq.Text = ItakuMemoIkkatsuEntryEntity.SEQ.ToString();

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }   
        #endregion      

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit(base.WindowType);           

        }

        /// <summary>
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            // 処理モード変更
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // 画面再描画
            //base.OnLoad(e);
            this.mDenpyouNumber = string.Empty;
            this.logic.mDenpyouNumber = string.Empty;
            // 画面初期化
           // this.logic.SystemId = string.Empty;
            this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
            this.PreviousValue = string.Empty;
        }

        /// <summary>
        /// 【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            // 処理モード変更
            base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

            // 画面再描画
            //base.OnLoad(e);
            this.txtDenpyouNumber.Text = this.mDenpyouNumber;
            // 画面初期化
            this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
            this.PreviousValue = string.Empty;
        }      
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                // 登録用データの作成
                this.logic.CreateEntity(base.WindowType);

                switch (base.WindowType)
                {
                    // 新規追加
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 重複チェック
                        bool result = true ;//this.DupliUpdateViewCheck(e);
                        if (result)
                        {
                            // 重複していなければ登録を行う
                            this.logic.Regist(base.RegistErrorFlag);
                        }

                        // DB登録後、修正モードで再表示
                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;                       
                        this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                        break;

                    // 更新
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        this.logic.Update(base.RegistErrorFlag);

                        // DB更新後、修正処理モード解除処理を実施
                       base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                       this.logic.mDenpyouNumber = this.txtDenpyouNumber.Text.ToString();
                       this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);                       
                        break;

                    // 論理削除
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.logic.LogicalDelete();
                       
                        // DB登録後、新規モードで再表示
                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        //base.OnLoad(e);
                        this.logic.mDenpyouNumber = string.Empty;
                        this.logic.isRegist = false;
                        this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
                        break;

                    default:
                        break;
                }
                this.txtDenpyouNumber.Focus();
                // 画面初期化
                this.PreviousValue = string.Empty;
            }
        }  

        ///// <summary>
        ///// 画面タイプ変更処理
        ///// </summary>
        ///// <param name="type"></param>
        //public virtual void SetWindowType(WINDOW_TYPE type)
        //{
        //    base.WindowType = type;
        //    base.OnLoad(new EventArgs());

        //    // 画面初期化
        //    this.PreviousValue = string.Empty;
        //}
        /// <summary>
        /// 一覧画面初期化画面設定
        /// </summary>
        /// <param name="type"></param>
        public void ItilanWindowInitUpdate(WINDOW_TYPE type, T_ITAKU_MEMO_IKKATSU_ENTRY ItakuMemoIkkatsuEntryEntity)
        {
            // 処理モード変更
            base.WindowType = type;

            this.txtDenpyouNumber.Text = ItakuMemoIkkatsuEntryEntity.DENPYOU_NUMBER.ToString();
            this.mDenpyouNumber = ItakuMemoIkkatsuEntryEntity.DENPYOU_NUMBER.ToString();
            this.logic.mDenpyouNumber = ItakuMemoIkkatsuEntryEntity.DENPYOU_NUMBER.ToString();
            this.txtSeq.Text = ItakuMemoIkkatsuEntryEntity.SEQ.ToString();
            this.txtSystemId.Text = ItakuMemoIkkatsuEntryEntity.SYSTEM_ID.ToString();

            //// 画面再描画
            //base.OnLoad(new EventArgs());

            // 画面初期化
            this.logic.ModeInit(type, (BusinessBaseForm)this.Parent);
            this.PreviousValue = string.Empty;
        }
        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            this.logic.mDenpyouNumber = string.Empty;
            if (this.logic.Search()==0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
                return;
            }

           // this.logic.SetEntry();
            this.logic.SetIchiran();
        }

        #region F7 一覧画面へ遷移
        /// <summary>
        /// 一覧画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            this.logic.ShowIchiran();
        }
        #endregion

        #region  F12 Formクローズ処理
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }
        #endregion

        #region  伝票番号変更時
        /// <summary>
        /// 伝票番号変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDenpyouNumber_LostFocus(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtDenpyouNumber.Text.ToString())
                || (this.txtDenpyouNumber.Text.ToString().Equals(this.mDenpyouNumber)))
            {
                return;
            }
          
            if (this.logic.Search(this.txtDenpyouNumber.Text.ToString()) == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E045");
                if (!string.IsNullOrEmpty(this.mDenpyouNumber))
                {
                    this.txtDenpyouNumber.Text = this.mDenpyouNumber;
                }
                else
                {
                    this.txtDenpyouNumber.Text = string.Empty;
                }
                this.txtDenpyouNumber.Focus();
                return;
            }
            this.mDenpyouNumber = this.txtDenpyouNumber.Text.ToString();
            this.logic.mDenpyouNumber = this.txtDenpyouNumber.Text.ToString();
             //処理モード変更
            base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            // 画面初期化
            this.logic.SetEntry("修正");
            this.logic.SetIchiran();
            this.logic.InitDenpyouNumberUpdate((BusinessBaseForm)this.Parent);
        }
        #endregion

        #region 処分パターン処理
        /// <summary>
        ///中間処分パターンシをセットする
        /// </summary>     
        private void txtShobun_TextChanged(object sender, EventArgs e)
        {
            //中間処分パターンシをセットする
            if (!this.txtShobun.Text.Equals("1"))
            {
                this.txtShobunPatternNm2.Text = string.Empty;
                this.txtShobunPatternSeq2.Text = string.Empty;
                this.txtShobunPatternSysId2.Text = string.Empty;
                this.txtShobunPatternNm2.ReadOnly = true;
                this.btnShobunPattern2.Enabled = false;
            }
            else
            {
                this.txtShobunPatternNm2.ReadOnly =false ;
                this.btnShobunPattern2.Enabled = true;
            }
           
        }
        /// <summary>
        /// 最終処分パターンシをセットする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLastShobun_TextChanged(object sender, EventArgs e)
        {
            //最終処分パターンシをセットする
            if (!this.txtLastShobun.Text.Equals("1"))
            {
                this.txtLastShobunPatternNm2.Text = string.Empty;
                this.txtLastShobunPatternSeq2.Text = string.Empty;
                this.txtLastShobunPatternSysId2.Text = string.Empty;
                this.txtLastShobunPatternNm2.ReadOnly = true;
                this.btnLastShobunPattern2.Enabled = false;

            }
            else
            {
                this.txtLastShobunPatternNm2.ReadOnly = false;
                this.btnLastShobunPattern2.Enabled = true;
            }
        }


        private void txtShobunPatternNm_Leave(object sender, EventArgs e)
        {

            //処分パターンシをセットする
            this.logic.SetPatternData(this.txtShobunPatternNm, this.txtShobunPatternSysId, this.txtShobunPatternSeq, 1);

        }

        private void txtLastShobunPatternNm_Leave(object sender, EventArgs e)
        {

            //処分パターンシをセットする
            this.logic.SetPatternData(this.txtLastShobunPatternNm, this.txtLastShobunPatternSysId, this.txtLastShobunPatternSeq, 2);

        }

        private void txtShobunPatternNm2_Leave(object sender, EventArgs e)
        {
            
                //処分パターンシをセットする
                this.logic.SetPatternData(this.txtShobunPatternNm2, this.txtShobunPatternSysId2, this.txtShobunPatternSeq2, 1);
            
        }

        private void txtLastShobunPatternNm2_Leave(object sender, EventArgs e)
        {
                //処分パターンシをセットする
                this.logic.SetPatternData(this.txtLastShobunPatternNm2, this.txtLastShobunPatternSysId2, this.txtLastShobunPatternSeq2, 2);
           
        }

        #endregion

        #region private メッソド
        private void UPDATE_SHUBETSU_Validated(object sender, EventArgs e)
        {
            r_framework.CustomControl.CustomNumericTextBox obj = (r_framework.CustomControl.CustomNumericTextBox)sender;
            if(string.IsNullOrEmpty (obj.Text .ToString ()))
            {
               obj.Text = "0";
            }
        }

        private void KEIYAKUSHO_SHURUI_Validated(object sender, EventArgs e)
        {
            r_framework.CustomControl.CustomNumericTextBox obj = (r_framework.CustomControl.CustomNumericTextBox)sender;
            if(string.IsNullOrEmpty (obj.Text .ToString ()))
            {
               obj.Text = "0";
            }
        }

        private void txtShobun_Validated(object sender, EventArgs e)
        {
            r_framework.CustomControl.CustomNumericTextBox obj = (r_framework.CustomControl.CustomNumericTextBox)sender;
            if(string.IsNullOrEmpty (obj.Text .ToString ()))
            {
               obj.Text = "2";
            }
        }

        private void txtLastShobun_Validated(object sender, EventArgs e)
        {
            r_framework.CustomControl.CustomNumericTextBox obj = (r_framework.CustomControl.CustomNumericTextBox)sender;
            if(string.IsNullOrEmpty (obj.Text .ToString ()))
            {
               obj.Text = "2";
            }
        }

        public void PopupAfterHaishutsuJigyoushaCD()
        {
            this.txtGenbaCd.Text = string.Empty;
            this.txtGenbaNm.Text = string.Empty;

        }

        #endregion
    }
}
