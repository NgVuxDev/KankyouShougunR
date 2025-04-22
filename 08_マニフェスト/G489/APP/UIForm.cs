using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Seasar.Quill;
using Shougun.Core.PaperManifest.InsatsuBusuSettei.Logic;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon.Dto;
using r_framework.FormManager;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.PaperManifest.InsatsuBusuSettei
{
    public partial class InsatsuBusuSettei : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private InsatsuBusuSetteiLogic logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; set; }

        /// <summary>
        /// 交付区分
        /// </summary>
        public string KoufuKbn { get; private set; }

        /// <summary>
        /// マニフェスト
        /// </summary>
        public List<T_MANIFEST_ENTRY> Entrylist { get; set; }

        /// <summary>
        /// マニフェスト収集運搬
        /// </summary>
        public List<T_MANIFEST_UPN> Upnlist { get; private set; }

        /// <summary>
        /// マニフェスト印字
        /// </summary>
        public List<T_MANIFEST_PRT> Prtlist { get; private set; }

        /// <summary>
        /// マニフェスト印字明細
        /// </summary>
        public List<T_MANIFEST_DETAIL_PRT> Detailprtlist { get; private set; }

        /// <summary>
        /// マニフェスト明細
        /// </summary>
        public List<T_MANIFEST_DETAIL> Detaillist { get; private set; }

        /// <summary>
        /// マニフェスト返却日
        /// </summary>
        public List<T_MANIFEST_RET_DATE> Retdatelist { get; private set; }

        /// <summary>
        /// マニ印字_建廃_形状
        /// </summary>
        public List<T_MANIFEST_KP_KEIJYOU> Keijyoulist { get; private set; }

        /// <summary>
        /// マニ印字_建廃_荷姿
        /// </summary>
        public List<T_MANIFEST_KP_NISUGATA> Nisugatalist { get; private set; }

        /// <summary>
        /// マニ印字_建廃_処分方法
        /// </summary>
        public List<T_MANIFEST_KP_SBN_HOUHOU> Houhoulist { get; private set; }

        /// <summary>
        /// 登録できた場合のシステムID
        /// </summary>
        public ItakuErrorDTO retDto { get; internal set; }
        public int ManiFlag { get; internal set; }

        /// <summary>
        /// レイアウト区分
        /// </summary>
        public bool manifest_mercury_check { get; set; }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InsatsuBusuSettei()
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new InsatsuBusuSetteiLogic(this);

        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            this.logic = new InsatsuBusuSetteiLogic(this);

            if (!this.logic.WindowInit()) { return; }

            this.bt_func9.Click += this.Jikkou;
            this.bt_func12.Click += this.FormClose;
            this.bt_func1.Click += this.Print;
            this.bt_func4.Click += this.Toroku;
            this.bt_func11.Click += this.OpenPrintSettingGamen;
            this.txt_KoufuNo.Leave += this.txt_KoufuNo_Leave;
            this.txt_KoufuNo.KeyPress += this.txt_KoufuNo_KeyPress;
            this.txt_InsatsuBusu.Leave += this.txt_InsatsuBusu_Leave;
            this.txt_InsatsuBusu.KeyUp += this.ControlKeyUp;
            this.txt_KoufuNo.KeyUp += this.ControlKeyUp;

        }
   
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dentaneMode">伝種区分</param>
        /// <param name="syoriMode">処理モード</param>
        /// <param name="insatsuMode">印刷モード 1:単票 2:連票</param>
        /// <param name="entrylist">マニフェスト</param>
        /// <param name="upnlist">マニ収集運搬</param>
        /// <param name="prtlist">マニ印字</param>
        /// <param name="detailprtlist">マニ印字明細</param>
        /// <param name="detaillist">マニ明細</param>
        /// <param name="retdatelist">マニ返却日</param>
        public InsatsuBusuSettei(int dentaneMode, int syoriMode, int insatsuMode
            , List<T_MANIFEST_ENTRY> entrylist
            , List<T_MANIFEST_UPN> upnlist
            , List<T_MANIFEST_PRT> prtlist
            , List<T_MANIFEST_DETAIL_PRT> detailprtlist
            , List<T_MANIFEST_DETAIL> detaillist
            , List<T_MANIFEST_RET_DATE> retdatelist
            , int maniFlag)
           : base()
        {
            this.InitializeComponent();

            //パラメータ
            Properties.Settings.Default.dentaneMode = dentaneMode;
            Properties.Settings.Default.syoriMode = syoriMode;
            Properties.Settings.Default.insatsuMode = insatsuMode;
            Properties.Settings.Default.Save();

            Entrylist = entrylist;
            Upnlist = upnlist;
            Prtlist = prtlist;
            Detailprtlist = detailprtlist;
            Detaillist = detaillist;
            Retdatelist = retdatelist;
            ManiFlag = maniFlag;

            KoufuKbn = entrylist[0].KOUFU_KBN.ToString();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new InsatsuBusuSetteiLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dentaneMode">伝種区分</param>
        /// <param name="syoriMode">処理モード</param>
        /// <param name="insatsuMode">印刷モード</param>
        /// <param name="entrylist">マニフェスト</param>
        /// <param name="upnlist">マニ収集運搬</param>
        /// <param name="prtlist">マニ印字</param>
        /// <param name="detailprtlist">マニ印字明細</param>
        /// <param name="keijyoulist">マニ印字_建廃_形状</param>
        /// <param name="houhoulist">マニ印字_建廃_荷姿</param>
        /// <param name="niugatalist">マニ印字_建廃_処分方法</param>
        /// <param name="detaillist">マニ明細</param>
        /// <param name="retdatelist">マニ返却日</param>
        public InsatsuBusuSettei(int dentaneMode, int syoriMode, int insatsuMode
            , List<T_MANIFEST_ENTRY> entrylist
            , List<T_MANIFEST_UPN> upnlist
            , List<T_MANIFEST_PRT> prtlist
            , List<T_MANIFEST_DETAIL_PRT> detailprtlist
            , List<T_MANIFEST_KP_KEIJYOU> keijyoulist
            , List<T_MANIFEST_KP_NISUGATA> niugatalist
            , List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist
            , List<T_MANIFEST_DETAIL> detaillist
            , List<T_MANIFEST_RET_DATE> retdatelist
            , int maniFlag)
            : base()
        {
            this.InitializeComponent();

            //パラメータ
            Properties.Settings.Default.dentaneMode = dentaneMode;
            Properties.Settings.Default.syoriMode = syoriMode;
            Properties.Settings.Default.insatsuMode = insatsuMode;
            Properties.Settings.Default.Save();

            Entrylist = entrylist;
            Upnlist = upnlist;
            Prtlist = prtlist;
            Detailprtlist = detailprtlist;
            Detaillist = detaillist;
            Retdatelist = retdatelist;
            Keijyoulist =keijyoulist;
            Nisugatalist =niugatalist;
            Houhoulist = houhoulist;
            ManiFlag = maniFlag;

            KoufuKbn = entrylist[0].KOUFU_KBN.ToString();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new InsatsuBusuSetteiLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        
        #region イベント
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        internal void FormClose(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.dentaneMode = 0;
            Properties.Settings.Default.syoriMode = 0;
            Properties.Settings.Default.insatsuMode = 0;
            Properties.Settings.Default.Save();

            this.Close();
        }

        /// <summary>
        /// 登録できた場合のシステムID
        /// </summary>
        public long SystemId { get; internal set; }

        /// <summary>
        /// 実行処理
        /// </summary>
        internal void Jikkou(object sender, System.EventArgs e)
        {
            if (!this.logic.CheckInputItem(Const.ConstCls.enumChkKbn.All, Const.ConstCls.enumButtonKbn.All))
            {
                switch (this.logic.Jikkou())
                {
                    case  Const.ConstCls.JikkouResult.YES://正常
                        break;

                    case Const.ConstCls.JikkouResult.NO://「いいえ」を選択
                    case Const.ConstCls.JikkouResult.DUPLICATION://登録データ重複エラー
                    case Const.ConstCls.JikkouResult.ERROR://エラー
                        return;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.FormClose(sender,e);
            }
        } 

        /// <summary>
        /// 交付番号テキストフォーカスLeaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_KoufuNo_Leave(object sender, EventArgs e)
        {
            ////交付番号入力チェッ
            if (this.logic.CheckInputItem(Const.ConstCls.enumChkKbn.KoufuOnly, Const.ConstCls.enumButtonKbn.All))
            {
                return;
            }
        }

        private void txt_KoufuNo_Validated(object sender, EventArgs e)
        {
            //交付番号入力チェック
            //if (this.logic.CheckInputItem(Const.ConstCls.enumChkKbn.KoufuOnly))
            //{
            //    return;
            //}
        }

        /// <summary>
        /// キー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlKeyUp(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
               
                case Keys.F9:
                    ControlUtility.ClickButton(this, "bt_func9");
                    break;
                case Keys.F12:
                    ControlUtility.ClickButton(this, "bt_func12");
                    break;
                case Keys.F1:
                    ControlUtility.ClickButton(this, "bt_func1");
                    break;
                case Keys.F4:
                    ControlUtility.ClickButton(this, "bt_func4");
                    break;
                case Keys.F11:
                    ControlUtility.ClickButton(this, "bt_func11");
                    break;
                case Keys.Enter:
                    //SendKeys.Send("{TAB}");
                    break;
            }
    
        }

        /// <summary>
        /// 印刷部数テキストフォーカスLeaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_InsatsuBusu_Leave(object sender, EventArgs e)
        { 
            //印刷部数入力チェック
            if (this.logic.ChkInsatsuBusu())
            {
                return;
            }

        }

        /// <summary>
        /// 交付番号変換処理（小文字→大文字）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_KoufuNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar =Convert.ToChar(e.KeyChar.ToString().ToUpper());           

        }

        /// <summary>
        /// 印刷部数にフォーカスがあるときにキーが押されると発生します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_InsatsuBusu_KeyPress(object sender, KeyPressEventArgs e)
        {
            //「.」と「,」を除く
            if (e.KeyChar == (char)46 || e.KeyChar == (char)44) e.Handled = true;
        }
        #endregion

        /// <summary>
        /// 実行処理
        /// </summary>
        internal void Print(object sender, System.EventArgs e)
        {
            if (!this.logic.CheckInputItem(Const.ConstCls.enumChkKbn.All,Const.ConstCls.enumButtonKbn.Print))
            {
                switch (this.logic.Print())
                {
                    case Const.ConstCls.JikkouResult.YES://正常
                        break;

                    case Const.ConstCls.JikkouResult.NO://「いいえ」を選択
                    case Const.ConstCls.JikkouResult.ERROR://エラー
                        return;
                }
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        internal void Toroku(object sender, System.EventArgs e)
        {
            if (!this.logic.CheckInputItem(Const.ConstCls.enumChkKbn.All, Const.ConstCls.enumButtonKbn.Toroku))
            {
                switch (this.logic.Toroku())
                {
                    case Const.ConstCls.JikkouResult.YES://正常
                        break;

                    case Const.ConstCls.JikkouResult.NO://「いいえ」を選択
                    case Const.ConstCls.JikkouResult.DUPLICATION://登録データ重複エラー
                    case Const.ConstCls.JikkouResult.ERROR://エラー
                        return;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.FormClose(sender, e);
            }
        }

        /// <summary>
        /// 印刷設定
        /// </summary>
        internal void OpenPrintSettingGamen(object sender, System.EventArgs e)
        {
            FormManager.OpenDialog("G487");
        }
    }
}