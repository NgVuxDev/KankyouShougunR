using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.PaperManifest.HaikibutuTyoubo.Const;

namespace Shougun.Core.PaperManifest.HaikibutuTyoubo
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// 廃棄物画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 業者コード
        /// </summary>
        internal string GyoushaCD = String.Empty;

        /// <summary>
        /// 現場コード
        /// </summary>
        internal string GenbaCD = String.Empty;

        /// <summary>
        /// 現場名
        /// </summary>
        internal string GenbaName = String.Empty;

        /// <summary>
        /// 年月日(From)
        /// </summary>
        internal Boolean dtp_DateFrom_Flg = false;

        /// <summary>
        /// 年月日(To)
        /// </summary>
        internal Boolean dtp_DateTo_Flg = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        public UIForm(UIHeader header)
            : base(WINDOW_ID.T_HAIKIBUTU_CHOBO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            // コンポーネントの初期化
            this.InitializeComponent();
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.headerForm = header;
            this.logic.setHeaderForm(this.headerForm);
            
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);
            this.logic.WindowInit();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }

        private void UIForm_Load(object sender, EventArgs e)
        {

        }

        private void UIForm_Shown(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            // Formクローズ
            var parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 出力区分
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShuturyokuKbn_TextChanged(object sender, EventArgs e)
        {
            switch (this.txt_ShuturyokuKbn.Text)
            {
                case "2"://紙マニフェスト
                    this.label5.Text = Const.UIConstans.HaikibutsuKbn + "※";
                    this.panel5.Enabled = true;
                    break;

                case "1"://全て
                case "3"://電子マニフェスト
                    this.label5.Text = string.Empty;
                    this.panel5.Enabled = false;
                    break;

                default:
                    return;
            }
        }

        /// <summary>
        /// 出力区分
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShuturyokuKbn_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.txt_ShuturyokuKbn, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        /// <summary>
        /// 出力区分
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShuturyokuKbn_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.RadioButtonChk(sender, e);
        }

        /// <summary>
        /// 廃棄物区分
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void chk_HaikibutsuKbn1_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.chk_HaikibutsuKbn1, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        /// <summary>
        /// 廃棄物区分
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void chk_HaikibutsuKbn2_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.chk_HaikibutsuKbn2, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        /// <summary>
        /// 廃棄物区分
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void chk_HaikibutsuKbn3_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.chk_HaikibutsuKbn3, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        /// <summary>
        /// 出力内容
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShuturyokuNaiyo_TextChanged(object sender, EventArgs e)
        {
            //日付種類
            switch (txt_ShuturyokuNaiyo.Text)
            {
                case "4"://運搬（委託）
                case "5"://最終処分（委託）
                    this.txt_HitukeKbn.Text = "1";
                    break;

                case "1"://収集運搬（自社）
                case "3"://運搬（自社）
                    this.txt_HitukeKbn.Text = "2";
                    break;

                case "2"://中間処理
                case "6"://最終処分（処分）
                    this.txt_HitukeKbn.Text = "3";
                    break;

                default://未指定
                    break;
            }

            //業者･現場
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            switch (txt_ShuturyokuNaiyo.Text)
            {

                case "1"://収集運搬（自社）
                case "3"://運搬（自社）
                    //運搬受託者
                    this.lbl_ShobunJutakusha.Text = Const.UIConstans.UnpanJutakusha + "※";
                    this.txt_ShobunJutakushaCD.DisplayItemName = Const.UIConstans.UnpanJutakusha;
                    this.txt_ShobunJutakushaCD.Text = string.Empty;
                    this.txt_ShobunJutakushaName.Text = string.Empty;

                    //なし（事業場）
                    this.lbl_ShobunJigyojo.Text = string.Empty;
                    this.lbl_ShobunJigyojo.Enabled = false;

                    //事業場(From）
                    this.txt_ShobunJigyojoCD.Text = string.Empty;
                    this.txt_ShobunJigyojoCD.DisplayItemName = string.Empty;
                    this.txt_ShobunJigyojoCD.Enabled = false;

                    this.txt_ShobunJigyojoName.Text = string.Empty;
                    this.txt_ShobunJigyojoName.Enabled = false;

                    //検索ポップアップ
                    DenshiMasterDataLogic.SetPopupSetting(
                        this.txt_ShobunJutakushaCD, this.txt_ShobunJutakushaName, null
                        , null, null, null
                        , DenshiMasterDataLogic.MANI_KBN.KAMI
                        , DenshiMasterDataLogic.JIGYOUSYA_KBN.JISHA | DenshiMasterDataLogic.JIGYOUSYA_KBN.UNPAN_JUTAKUSHA_KAISHA
                        , false, false
                        , DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE
                        , false, true, true
                        );

                    //事業場(To）
                    this.txt_ShobunJigyojoCD_To.Text = string.Empty;
                    this.txt_ShobunJigyojoCD_To.DisplayItemName = string.Empty;
                    this.txt_ShobunJigyojoCD_To.Enabled = false;

                    this.txt_ShobunJigyojoName_To.Text = string.Empty;
                    this.txt_ShobunJigyojoName_To.Enabled = false;

                    //検索ポップアップ
                    DenshiMasterDataLogic.SetPopupSetting(
                        this.txt_ShobunJutakushaCD, this.txt_ShobunJutakushaName, null
                        , null, null, null
                        , DenshiMasterDataLogic.MANI_KBN.KAMI
                        , DenshiMasterDataLogic.JIGYOUSYA_KBN.JISHA | DenshiMasterDataLogic.JIGYOUSYA_KBN.UNPAN_JUTAKUSHA_KAISHA
                        , false, false
                        , DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE
                        , false, true, true
                        );

                    break;

                case "4"://運搬（委託）
                    //排出事業者
                    this.lbl_ShobunJutakusha.Text = Const.UIConstans.HaishutsuJutakusha + "※";
                    this.txt_ShobunJutakushaCD.DisplayItemName = Const.UIConstans.HaishutsuJutakusha;
                    this.txt_ShobunJutakushaCD.Text = string.Empty;
                    this.txt_ShobunJutakushaName.Text = string.Empty;

                    //排出事業場
                    this.lbl_ShobunJigyojo.Text = Const.UIConstans.HaishutsuJigyojo;
                    this.lbl_ShobunJigyojo.Enabled = true;

                    //事業場(From）
                    this.txt_ShobunJigyojoCD.Text = string.Empty;
                    this.txt_ShobunJigyojoCD.Enabled = true;
                    this.txt_ShobunJigyojoCD.DisplayItemName = Const.UIConstans.HaishutsuJigyojo;

                    this.txt_ShobunJigyojoName.Text = string.Empty;
                    this.txt_ShobunJigyojoName.Enabled = true;

                    //検索ポップアップ
                    DenshiMasterDataLogic.SetPopupSetting(
                        this.txt_ShobunJutakushaCD, this.txt_ShobunJutakushaName, null
                        , this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoName, null
                        , DenshiMasterDataLogic.MANI_KBN.KAMI
                        , DenshiMasterDataLogic.JIGYOUSYA_KBN.JISHA | DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA
                        , false, false
                        , DenshiMasterDataLogic.JIGYOUJOU_KBN.JISHA | DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA
                        , false, true, true
                        );

                    //事業場(To）
                    this.txt_ShobunJigyojoCD_To.Text = string.Empty;
                    this.txt_ShobunJigyojoCD_To.Enabled = true;
                    this.txt_ShobunJigyojoCD_To.DisplayItemName = Const.UIConstans.HaishutsuJigyojo;

                    this.txt_ShobunJigyojoName_To.Text = string.Empty;
                    this.txt_ShobunJigyojoName_To.Enabled = true;

                    //検索ポップアップ
                    DenshiMasterDataLogic.SetPopupSetting(
                        this.txt_ShobunJutakushaCD, this.txt_ShobunJutakushaName, null
                        , this.txt_ShobunJigyojoCD_To, this.txt_ShobunJigyojoName_To, null
                        , DenshiMasterDataLogic.MANI_KBN.KAMI
                        , DenshiMasterDataLogic.JIGYOUSYA_KBN.JISHA | DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA
                        , false, false
                        , DenshiMasterDataLogic.JIGYOUJOU_KBN.JISHA | DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA
                        , false, true, true
                        );
                    break;

                case "2"://中間処理
                case "6"://最終処分（処分）
                    //処分受託者※
                    this.lbl_ShobunJutakusha.Text = Const.UIConstans.ShobunJutakusha + "※";
                    this.txt_ShobunJutakushaCD.DisplayItemName = Const.UIConstans.ShobunJutakusha;
                    this.txt_ShobunJutakushaCD.Text = string.Empty;
                    this.txt_ShobunJutakushaName.Text = string.Empty;

                    //処分事業場※
                    this.lbl_ShobunJigyojo.Text = Const.UIConstans.ShobunJigyojo;
                    this.lbl_ShobunJigyojo.Enabled = true;

                    //事業場(From）
                    this.txt_ShobunJigyojoCD.Text = string.Empty;
                    this.txt_ShobunJigyojoCD.Enabled = true;
                    this.txt_ShobunJigyojoCD.DisplayItemName = Const.UIConstans.ShobunJigyojo;

                    this.txt_ShobunJigyojoName.Text = string.Empty;
                    this.txt_ShobunJigyojoName.Enabled = true;

                    //検索ポップアップ
                    DenshiMasterDataLogic.SetPopupSetting(
                        this.txt_ShobunJutakushaCD, this.txt_ShobunJutakushaName, null
                        , this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoName, null
                        , DenshiMasterDataLogic.MANI_KBN.KAMI
                        , DenshiMasterDataLogic.JIGYOUSYA_KBN.JISHA | DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA
                        , false, false
                        , DenshiMasterDataLogic.JIGYOUJOU_KBN.JISHA | DenshiMasterDataLogic.JIGYOUJOU_KBN.SHOBUN_NIOROSHI_GENBA
                        , false, true, true
                        );

                    //事業場(To）
                    this.txt_ShobunJigyojoCD_To.Text = string.Empty;
                    this.txt_ShobunJigyojoCD_To.Enabled = true;
                    this.txt_ShobunJigyojoCD_To.DisplayItemName = Const.UIConstans.ShobunJigyojo;

                    this.txt_ShobunJigyojoName_To.Text = string.Empty;
                    this.txt_ShobunJigyojoName_To.Enabled = true;

                    //検索ポップアップ
                    DenshiMasterDataLogic.SetPopupSetting(
                        this.txt_ShobunJutakushaCD, this.txt_ShobunJutakushaName, null
                        , this.txt_ShobunJigyojoCD_To, this.txt_ShobunJigyojoName_To, null
                        , DenshiMasterDataLogic.MANI_KBN.KAMI
                        , DenshiMasterDataLogic.JIGYOUSYA_KBN.JISHA | DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA
                        , false, false
                        , DenshiMasterDataLogic.JIGYOUJOU_KBN.JISHA | DenshiMasterDataLogic.JIGYOUJOU_KBN.SHOBUN_NIOROSHI_GENBA
                        , false, true, true
                        );

                    break;

                case "5"://最終処分（委託）
                    //排出事業者※
                    this.lbl_ShobunJutakusha.Text = Const.UIConstans.HaishutsuJutakusha + "※";
                    this.txt_ShobunJutakushaCD.DisplayItemName = Const.UIConstans.HaishutsuJutakusha;
                    this.txt_ShobunJutakushaCD.Text = string.Empty;
                    this.txt_ShobunJutakushaName.Text = string.Empty;

                    //排出事業場※
                    this.lbl_ShobunJigyojo.Text = Const.UIConstans.HaishutsuJigyojo;
                    this.lbl_ShobunJigyojo.Enabled = true;

                    //事業場(From）
                    this.txt_ShobunJigyojoCD.Text = string.Empty;
                    this.txt_ShobunJigyojoCD.Enabled = true;
                    this.txt_ShobunJigyojoCD.DisplayItemName = Const.UIConstans.HaishutsuJigyojo;

                    this.txt_ShobunJigyojoName.Text = string.Empty;
                    this.txt_ShobunJigyojoName.Enabled = true;

                    //検索ポップアップ
                    DenshiMasterDataLogic.SetPopupSetting(
                        this.txt_ShobunJutakushaCD, this.txt_ShobunJutakushaName, null
                        , this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoName, null
                        , DenshiMasterDataLogic.MANI_KBN.KAMI
                        , DenshiMasterDataLogic.JIGYOUSYA_KBN.JISHA | DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA
                        , false, false
                        , DenshiMasterDataLogic.JIGYOUJOU_KBN.JISHA | DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA
                        , false, true, true
                        );

                    //事業場(To）
                    this.txt_ShobunJigyojoCD_To.Text = string.Empty;
                    this.txt_ShobunJigyojoCD_To.Enabled = true;
                    this.txt_ShobunJigyojoCD_To.DisplayItemName = Const.UIConstans.ShobunJigyojo;

                    this.txt_ShobunJigyojoName_To.Text = string.Empty;
                    this.txt_ShobunJigyojoName_To.Enabled = true;

                    //検索ポップアップ
                    DenshiMasterDataLogic.SetPopupSetting(
                        this.txt_ShobunJutakushaCD, this.txt_ShobunJutakushaName, null
                        , this.txt_ShobunJigyojoCD_To, this.txt_ShobunJigyojoName_To, null
                        , DenshiMasterDataLogic.MANI_KBN.KAMI
                        , DenshiMasterDataLogic.JIGYOUSYA_KBN.JISHA | DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA
                        , false, false
                        , DenshiMasterDataLogic.JIGYOUJOU_KBN.JISHA | DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA
                        , false, true, true
                        );

                    break;

                default://未指定
                    break;
            }

            //運搬受託者
            this.txt_ShobunJutakushaCD.Enter += new EventHandler(txt_ShobunJutakushaCD_Enter);
            this.txt_ShobunJutakushaCD.Validated += new EventHandler(txt_ShobunJutakushaCD_Validated);
            this.txt_ShobunJigyojoCD_To.Validated += new EventHandler(txt_ShobunJigyojoCD_To_Validated);

            /// 20141023 Houkakou 「廃棄物帳簿」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.txt_ShobunJigyojoCD_To.MouseDoubleClick += new MouseEventHandler(this.logic.txt_ShobunJigyojoCD_To_MouseDoubleClick);
            /// 20141023 Houkakou 「廃棄物帳簿」のダブルクリックを追加する　end

            //中間処理
            switch (txt_ShuturyokuNaiyo.Text)
            {
                case "2"://中間処理
                    this.panel3.Enabled = true;
                    this.txt_Tyukansyori.Enabled = true;
                    break;

                case "1"://収集運搬（自社）
                case "3"://運搬（自社）
                case "4"://運搬（委託）
                case "5"://最終処分（委託）
                case "6"://最終処分（処分）
                    this.panel3.Enabled = false;
                    this.txt_Tyukansyori.Enabled = false;
                    break;

                default://未指定
                    break;
            }
        }

        /// <summary>
        /// 出力内容
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShuturyokuNaiyo_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.txt_ShuturyokuNaiyo, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        /// <summary>
        /// 出力内容
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShuturyokuNaiyo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.RadioButtonChk(sender, e);
        }

        /// <summary>
        /// 中間処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_Tyukansyori_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.txt_Tyukansyori, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        /// <summary>
        /// 中間処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_Tyukansyori_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.RadioButtonChk(sender, e);
        }

        /// <summary>
        /// 日付種類
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_HitukeKbn_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.txt_HitukeKbn, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        /// <summary>
        /// 日付種類
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_HitukeKbn_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            this.logic.RadioButtonChk(sender, e);
        }

        /// <summary>
        /// 拠点
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_KyotenCD_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.txt_KyotenCD, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        /// <summary>
        /// 処分受託者
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShobunJutakushaCD_Enter(object sender, EventArgs e)
        {
            this.GyoushaCD = this.txt_ShobunJutakushaCD.Text;
            this.logic.TextBoxFromToChk(this.txt_ShobunJutakushaCD, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        //処分受託者をクリアすると処分事業場をクリアする thongh 2015/08/26 #12538
        /// <summary>
        /// 処分受託者のポップアップ
        /// </summary>
        public void ShobunJutakushaCDPopupAfterExecuteMethod()
        {
            if (this.GyoushaCD != this.txt_ShobunJutakushaCD.Text)
            {
                if (!string.IsNullOrEmpty(this.txt_ShobunJigyojoCD.Text)
                && !string.IsNullOrEmpty(this.txt_ShobunJigyojoName.Text))
                {
                    this.txt_ShobunJigyojoCD.Text = string.Empty;
                    this.txt_ShobunJigyojoName.Text = string.Empty;
                }
                this.GenbaCD = string.Empty;
                this.GenbaName = string.Empty;
            }
            else if (string.IsNullOrEmpty(this.txt_ShobunJigyojoCD_To.Text)
                && string.IsNullOrEmpty(this.txt_ShobunJigyojoName_To.Text))
            {
                this.txt_ShobunJigyojoCD_To.Text = this.GenbaCD;
                this.txt_ShobunJigyojoName_To.Text = this.GenbaName;
            }
        }

        /// <summary>
        /// 処分受託者
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShobunJutakushaCD_Validated(object sender, EventArgs e)
        {
            ShobunJutakushaCDPopupAfterExecuteMethod();
        }

        /// <summary>
        /// 処分事業場(To)
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShobunJigyojoCD_To_Validated(object sender, EventArgs e)
        {
            this.GenbaCD = this.txt_ShobunJigyojoCD_To.Text;
            this.GenbaName = this.txt_ShobunJigyojoName_To.Text;
        }        
        //処分受託者をクリアすると処分事業場をクリアする thongh 2015/08/26 #12538

        /// <summary>
        /// 処分事業場(From)
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShobunJigyojoCD_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

        /// <summary>
        /// 処分事業場(To)
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void txt_ShobunJigyojoCD_To_Enter(object sender, EventArgs e)
        {
            this.logic.TextBoxFromToChk(this.txt_ShobunJigyojoCD,this.txt_ShobunJigyojoCD, this.txt_ShobunJigyojoCD_To);
        }

    }
}
