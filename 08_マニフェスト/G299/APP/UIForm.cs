using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Logic;
using System.Collections.Generic;

namespace Shougun.Core.PaperManifest.ManifestPattern
{
    public partial class UIForm : IchiranSuperForm
    {
        /// <summary>
        /// 伝種区分
        /// </summary>
        public DENSHU_KBN DenshuKbn = DENSHU_KBN.MANI_PATTERN_ICHIRAN;

        /// <summary>
        /// 一括登録区分
        /// </summary>
        public String ListRegistKbn = "false";

        /// <summary>
        /// 廃棄物区分CD（1：産廃（直行）、2：建廃、3：産廃（積替）、4：電子）
        /// </summary>
        public String HaikiKbnCD = String.Empty;

        /// <summary>
        /// 一次マニフェスト区分（一次：false、二次：true）
        /// </summary>
        public String FirstManifestKbn = "";

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private ManifestPattern.LogicClass MPLogic;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        private Boolean isLoaded = false;

        /// <summary>
        /// システムID
        /// </summary>
        public String ParamOut_SysID { get; set; }

        /// <summary>
        /// 枝番
        /// </summary>
        public String ParamOut_Seq { get; set; }

        // 20140529 syunrei No.730 マニフェストパターン一覧 start

        public String strSelectQeury = "T_MANIFEST_PT_ENTRY.PATTERN_NAME AS 'パターン名',CASE T_MANIFEST_PT_ENTRY.USE_DEFAULT_KBN		WHEN 1 THEN '規定値'		WHEN 0 THEN '' ELSE '' END AS '規定値',M_KYOTEN.KYOTEN_NAME_RYAKU AS '拠点名',T_MANIFEST_PT_ENTRY.HST_GYOUSHA_NAME AS '排出事業者名'      , T_MANIFEST_PT_ENTRY.HST_GENBA_NAME AS '排出事業場名'      , T_MANIFEST_PT_UPN1.UPN_GYOUSHA_NAME AS '運搬受託者名'      , T_MANIFEST_PT_ENTRY.SBN_GYOUSHA_NAME AS '処分受託者名'      , T_MANIFEST_PT_UPN1.UPN_SAKI_GENBA_NAME AS '処分事業場名'      , M_HAIKI_SHURUI.HAIKI_SHURUI_NAME AS '廃棄物種類名'      , M_HAIKI_NAME.HAIKI_NAME AS '廃棄物名称'      , M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_NAME AS '処分方法'      , M_NISUGATA.NISUGATA_NAME AS '荷姿'      ,T_MANIFEST_PT_DETAIL_PRT.HAIKI_SHURUI_NAME AS '廃棄物種類(原本)'  ,PRT.PRT_HAIKI_NAME AS '廃棄物の名称(原本)'    ,PRT.PRT_NISUGATA_NAME AS '荷姿(原本)'      ,PRT.PRT_SBN_HOUHOU_NAME AS '処分方法(原本)'      ,T_MANIFEST_PT_ENTRY.BIKOU AS '備考(原本)',T_MANIFEST_PT_ENTRY.LAST_SBN_GENBA_NAME AS '最終処分場所(原本)'";
        public String strSelectQeuryTsumikae = "T_MANIFEST_PT_ENTRY.PATTERN_NAME AS 'パターン名',CASE T_MANIFEST_PT_ENTRY.USE_DEFAULT_KBN		WHEN 1 THEN '規定値'		WHEN 0 THEN '' ELSE '' END AS '規定値',M_KYOTEN.KYOTEN_NAME_RYAKU AS '拠点名',T_MANIFEST_PT_ENTRY.HST_GYOUSHA_NAME AS '排出事業者名'      , T_MANIFEST_PT_ENTRY.HST_GENBA_NAME AS '排出事業場名'      , T_MANIFEST_PT_UPN1.UPN_GYOUSHA_NAME AS '運搬受託者名'      , T_MANIFEST_PT_ENTRY.SBN_GYOUSHA_NAME AS '処分受託者名'      , CASE	WHEN T_MANIFEST_PT_UPN1.UPN_SAKI_KBN = 1 THEN T_MANIFEST_PT_UPN1.UPN_SAKI_GENBA_NAME	WHEN T_MANIFEST_PT_UPN2.UPN_SAKI_KBN = 1 THEN T_MANIFEST_PT_UPN2.UPN_SAKI_GENBA_NAME	WHEN T_MANIFEST_PT_UPN3.UPN_SAKI_KBN = 1 THEN T_MANIFEST_PT_UPN3.UPN_SAKI_GENBA_NAME	ELSE ''	END AS '処分事業場名'      , M_HAIKI_SHURUI.HAIKI_SHURUI_NAME AS '廃棄物種類名'      , M_HAIKI_NAME.HAIKI_NAME AS '廃棄物名称'      , M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_NAME AS '処分方法'      , M_NISUGATA.NISUGATA_NAME AS '荷姿'      ,T_MANIFEST_PT_DETAIL_PRT.HAIKI_SHURUI_NAME AS '廃棄物種類(原本)'  ,PRT.PRT_HAIKI_NAME AS '廃棄物の名称(原本)'    ,PRT.PRT_NISUGATA_NAME AS '荷姿(原本)'      ,PRT.PRT_SBN_HOUHOU_NAME AS '処分方法(原本)'      ,T_MANIFEST_PT_ENTRY.BIKOU AS '備考(原本)',T_MANIFEST_PT_ENTRY.LAST_SBN_GENBA_NAME AS '最終処分場所(原本)'";
        public String strOrderby = "'パターン名' ASC,'規定値' ASC,'拠点名' ASC,'排出事業者名' ASC,'排出事業場名' ASC,'運搬受託者名' ASC,'処分受託者名' ASC,'処分事業場名' ASC,'廃棄物種類名' ASC,'廃棄物名称' ASC,'処分方法' ASC,'荷姿' ASC,'廃棄物種類(原本)' ASC,'廃棄物の名称(原本)' ASC,'荷姿(原本)' ASC,'処分方法(原本)' ASC,'備考(原本)' ASC,'最終処分場所(原本)' ASC";
        
        //電子マニフェスト
        //20151103 hoanghm #2166 start
        //public String strSelectQeuryDT = "DT_PT_R18.PATTERN_NAME AS 'パターン名',DT_PT_R18.HST_SHA_NAME AS '排出事業者名',	DT_PT_R18.HST_JOU_NAME AS '排出事業場名',	DT_PT_R19_1.UPN_SHA_NAME AS '運搬受託者名',	DT_PT_R18.SBN_SHA_NAME AS '処分受託者名',	DT_PT_R19_1.UPNSAKI_JOU_NAME AS '処分事業場名',	M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME AS '廃棄物種類名',	M_DENSHI_HAIKI_NAME.HAIKI_NAME AS '廃棄物名称',	M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_NAME AS '処分方法',	DT_PT_R18.NISUGATA_NAME AS '荷姿',	T_MANIFEST_PT_DETAIL_PRT.HAIKI_SHURUI_NAME AS '廃棄物名称(原本)',	PRT.PRT_NISUGATA_NAME AS '荷姿(原本)',	PRT.PRT_SBN_HOUHOU_NAME AS '処分方法(原本)',	DT_PT_R06_1.BIKOU AS '備考(原本)',	DT_PT_R04_1.LAST_SBN_JOU_NAME AS '最終処分場所(原本)'";
        //public String strOrderbyDT = "'パターン名' ASC,'排出事業者名' ASC,'排出事業場名' ASC,'運搬受託者名' ASC,'処分受託者名' ASC,'処分事業場名' ASC,'廃棄物種類名' ASC,'廃棄物名称' ASC,'処分方法' ASC,'荷姿' ASC,'廃棄物名称(原本)' ASC,'荷姿(原本)' ASC,'処分方法(原本)' ASC,'備考(原本)' ASC,'最終処分場所(原本)' ASC";
        public String strSelectQeuryDT = "DT_PT_R18.PATTERN_NAME AS 'パターン名',DT_PT_R18.HST_SHA_NAME AS '排出事業者名',	DT_PT_R18.HST_JOU_NAME AS '排出事業場名',	DT_PT_R19_1.UPN_SHA_NAME AS '運搬受託者名',	DT_PT_R18.SBN_SHA_NAME AS '処分受託者名',	DT_PT_R19_1.UPNSAKI_JOU_NAME AS '処分事業場名',	M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME AS '廃棄物種類名',	M_DENSHI_HAIKI_NAME.HAIKI_NAME AS '廃棄物名称',	M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_NAME AS '処分方法',	DT_PT_R18.NISUGATA_NAME AS '荷姿', DT_PT_R06_1.BIKOU AS '備考(原本)',	DT_PT_R04_1.LAST_SBN_JOU_NAME AS '最終処分場所(原本)'";
        public String strOrderbyDT = "'パターン名' ASC,'排出事業者名' ASC,'排出事業場名' ASC,'運搬受託者名' ASC,'処分受託者名' ASC,'処分事業場名' ASC,'廃棄物種類名' ASC,'廃棄物名称' ASC,'処分方法' ASC,'荷姿' ASC,'備考(原本)' ASC,'最終処分場所(原本)' ASC";
        //20151103 hoanghm #2166 end

        //初期時一覧の明細列
        private string[] strColumns = { "パターン名","規定値", "拠点名", "排出事業者名", "排出事業場名", "運搬受託者名", "処分受託者名", "処分事業場名", "廃棄物種類名", "廃棄物名称", "処分方法", "荷姿", "廃棄物種類(原本)", "廃棄物の名称(原本)", "荷姿(原本)", "処分方法(原本)", "備考(原本)", "最終処分場所(原本)" };
      
        //電子マニフェスト
        //20151103 hoanghm #2166 start
        //private string[] strColumnsDT = { "パターン名", "排出事業者名", "排出事業場名", "運搬受託者名", "処分受託者名", "処分事業場名", "廃棄物種類名", "廃棄物名称", "処分方法", "荷姿", "廃棄物名称(原本)", "荷姿(原本)", "処分方法(原本)", "備考(原本)", "最終処分場所(原本)" };
        private string[] strColumnsDT = { "パターン名","排出事業者名", "排出事業場名", "運搬受託者名", "処分受託者名", "処分事業場名", "廃棄物種類名", "廃棄物名称", "処分方法", "荷姿", "備考(原本)", "最終処分場所(原本)" };
        //20151103 hoanghm #2166 end

        // 20140529 syunrei No.730 マニフェストパターン一覧 end

        // 20140618 syunrei EV004876_マニフェストパターン一覧のアラート件数が効いていない start
        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        public UIHeader HeaderForm { get; private set; }
        // 20140618 syunrei EV004876_マニフェストパターン一覧のアラート件数が効いていない end

        internal bool isRegistErr { get; set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string befHstGyousha = string.Empty;
        private string befSbnGyousha = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm(DENSHU_KBN denshuKbn, String paramIn_ListRegistKbn, String paramIn_HaikiKbnCD, String paramIn_ManiFlag)
            : base(denshuKbn, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MPLogic = new LogicClass(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
            // 3/14 暫定パターン固定化対応
            this.ShainCd = "000001";
            this.bt_ptn1.Visible = false;
            this.bt_ptn2.Visible = false;
            this.bt_ptn3.Visible = false;
            this.bt_ptn4.Visible = false;
            this.bt_ptn5.Visible = false;

            //伝種区分
            DenshuKbn = denshuKbn;

            //一括登録区分
            switch (paramIn_ListRegistKbn)
            {
                case "1":
                case "true":
                    ListRegistKbn = "true";
                    break;

                case "0":
                case "false":
                default:
                    ListRegistKbn = "false";
                    break;
            }

            //廃棄物区分CD
            HaikiKbnCD = paramIn_HaikiKbnCD;

            //システムID
            ParamOut_SysID = String.Empty;

            //枝番
            ParamOut_Seq = String.Empty;

            // 汎用検索は一旦廃止
            this.searchString.Visible = false;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            // 20140529 syunrei No.730 マニフェストパターン一覧 start

            //元画面からマニフェスト（一次：false、二次：true）
            if (!string.IsNullOrEmpty(paramIn_ManiFlag))
            {
                switch (paramIn_ManiFlag)
                {
                    case "2":
                    case "true":
                        this.FirstManifestKbn = "true";
                        break;

                    case "1":
                    case "false":
                    default:
                        this.FirstManifestKbn = "false";
                        break;
                }
            }

            // 20140529 syunrei No.730 マニフェストパターン一覧 end
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.PreOnLoad(e);

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        /// OnLoad処理を分割
        /// </summary>
        /// <param name="e"></param>
        private void PreOnLoad(EventArgs e)
        {
            if (isLoaded == false)
            {
                //初期化、初期表示
                if (!this.MPLogic.WindowInit()) { return; }

                //キー入力設定
                var parentForm = (BusinessBaseForm)this.Parent;

                // ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)parentForm.headerForm;

                //画面全体
                parentForm.KeyDown += new KeyEventHandler(UIForm_KeyDown);

                //一覧
                this.customDataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridView1_CellDoubleClick);

                //処理No（ESC）
                parentForm.txb_process.KeyDown += new KeyEventHandler(TXB_PROCESS_KeyDown);
            }

            //画面の初期化
            if (!this.MPLogic.ClearScreen("Initial")) { return; }

            switch (this.HaikiKbnCD)
            {
                case "4":
                    this.MPLogic.selectQuery = this.strSelectQeuryDT;
                    this.MPLogic.orderByQuery = this.strOrderbyDT;
                    break;
                case "3":
                    this.MPLogic.selectQuery = this.strSelectQeuryTsumikae;
                    this.MPLogic.orderByQuery = this.strOrderby;
                    break;
                case "1":
                case "2":
                case "5":
                default:
                    this.MPLogic.selectQuery = this.strSelectQeury;
                    this.MPLogic.orderByQuery = this.strOrderby;
                    break;
            }

            isLoaded = true;

            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                //初期時、固定の明細列を表示させるために
                if (this.MPLogic.SearchResult.Columns.Count <= 0)
                {
                    //電子マニフェスト以外の場合
                    if (!this.HaikiKbnCD.Equals("4"))
                    {
                        foreach (string s in strColumns)
                        {
                            this.MPLogic.SearchResult.Columns.Add(s);
                        }
                    }
                    //電子マニフェストの場合
                    else
                    {
                        foreach (string s in strColumnsDT)
                        {
                            this.MPLogic.SearchResult.Columns.Add(s);
                        }
                    }
                    this.logic.CreateDataGridView(this.MPLogic.SearchResult);
                }
                else
                {
                    this.logic.CreateDataGridView(this.MPLogic.SearchResult);
                }
            }
            this.MPLogic.ChangeTitle();
            this.SetBackColor(this.FirstManifestKbn);
        }

        /// <summary>
        /// 画面表示時の制御
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;


            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }

        #region 画面コントロールイベント

        /// <summary>
        /// フォーム
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape://ESCキー
                    this.MPLogic.SetFocusTxbProcess();
                    break;
            }
        }

        /// <summary>
        /// 廃棄物区分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void cntHaikiKbnCd_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        //{

        //    //タイトル更新
        //    this.MPLogic.ChangeTitle();

        //    if (this.cntHaikiKbnCd.Text == null || this.cntHaikiKbnCd.Text == String.Empty)
        //    {
        //        MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
        //        var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E001");
        //        msg1 = string.Format(msg1, "廃棄物区分");
        //        Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
        //        e.Cancel = true;
        //        return;
        //    }

        //    switch (this.HaikiKbnCD)
        //    {
        //        case "1"://産廃（直行）
        //            //一括入力
        //            if (this.ListRegistKbn == "true")
        //            {
        //                if (this.cntHaikiKbnCd.Text == "4")
        //                {
        //                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
        //                    var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E002");
        //                    msg1 = string.Format(msg1, "廃棄物区分", "【1～3、5】");
        //                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
        //                    e.Cancel = true;
        //                    return;
        //                }
        //            }
        //            //単票入力
        //            else if (this.ListRegistKbn == "false")
        //            {
        //                if (this.cntHaikiKbnCd.Text != "1")
        //                {
        //                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
        //                    var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E002");
        //                    msg1 = string.Format(msg1, "廃棄物区分", "【1】");
        //                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
        //                    e.Cancel = true;
        //                    return;
        //                }
        //            }
        //            break;

        //        case "2"://建廃
        //            //一括入力
        //            if (this.ListRegistKbn == "true")
        //            {
        //                if (this.cntHaikiKbnCd.Text == "4")
        //                {
        //                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
        //                    var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E002");
        //                    msg1 = string.Format(msg1, "廃棄物区分", "【1～3、5】");
        //                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
        //                    e.Cancel = true;
        //                    return;
        //                }
        //            }
        //            //単票入力
        //            else if (this.ListRegistKbn == "false")
        //            {
        //                if (this.cntHaikiKbnCd.Text != "3")
        //                {
        //                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
        //                    var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E002");
        //                    msg1 = string.Format(msg1, "廃棄物区分", "【3】");
        //                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
        //                    e.Cancel = true;
        //                    return;
        //                }
        //            }
        //            break;

        //        case "3"://産廃（積替）
        //            //一括入力
        //            if (this.ListRegistKbn == "true")
        //            {
        //                if (this.cntHaikiKbnCd.Text == "4")
        //                {
        //                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
        //                    var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E002");
        //                    msg1 = string.Format(msg1, "廃棄物区分", "【1～3、5】");
        //                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
        //                    e.Cancel = true;
        //                    return;
        //                }
        //            }
        //            //単票入力
        //            else if (this.ListRegistKbn == "false")
        //            {
        //                if (this.cntHaikiKbnCd.Text != "2")
        //                {
        //                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
        //                    var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E002");
        //                    msg1 = string.Format(msg1, "廃棄物区分", "【2】");
        //                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
        //                    e.Cancel = true;
        //                    return;
        //                }
        //            }
        //            break;

        //        case "4"://電子
        //            if (this.cntHaikiKbnCd.Text != "4")
        //            {
        //                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
        //                var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E002");
        //                msg1 = string.Format(msg1, "廃棄物区分", "【4】");
        //                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
        //                e.Cancel = true;
        //                return;
        //            }
        //            break;

        //        default://未指定
        //            if (this.cntHaikiKbnCd.Text == "4")
        //            {
        //                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
        //                var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E002");
        //                msg1 = string.Format(msg1, "廃棄物区分", "【1～3、5】");
        //                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
        //                e.Cancel = true;
        //                return;
        //            }
        //            break;
        //    }
        //}

        private void cntHaikiKbnCd_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 一次二次区分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIRST_MANIFEST_KBN_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        //private void FIRST_MANIFEST_KBN_TextChanged(object sender, EventArgs e)
        //{
        //    //タイトル更新
        //    this.MPLogic.ChangeTitle();

        //    //出力区分
        //    switch (this.FIRST_MANIFEST_KBN.Text)
        //    {
        //        case "1"://一次
        //            this.FirstManifestKbn = "false";
        //            Properties.Settings.Default.FIRST_MANIFEST_KBN = "1";

        //            this.ChangeLabelColor(true);

        //            break;

        //        case "2"://二次
        //            this.FirstManifestKbn = "true";
        //            Properties.Settings.Default.FIRST_MANIFEST_KBN = "2";

        //            this.ChangeLabelColor(false);

        //            break;

        //        default:
        //            this.FirstManifestKbn = "";
        //            Properties.Settings.Default.FIRST_MANIFEST_KBN = "";
        //            break;
        //    }
        //    this.FIRST_MANIFEST_KBN.SelectAll();
        //}

        private void ChangeLabelColor(bool isFirst)
        {
            var backColor = Color.FromArgb(255, 0, 105, 51);
            if (!isFirst)
            {
                backColor = Color.FromArgb(255, 0, 51, 160);
            }

            // ヘッダ部分のラベル背景色変更
            var header = (UIHeader)((BusinessBaseForm)this.Parent).headerForm;
            header.Controls.OfType<Label>().ToList().ForEach(c => c.BackColor = backColor);

            // フッタ部分のラベル背景色変更
            ((BusinessBaseForm)this.Parent).lb_process.BackColor = backColor;

            // フォーム部分のラベル背景色変更
            this.Controls.OfType<Label>().ToList().ForEach(c => c.BackColor = backColor);

            // ソートヘッダ部分のラベル背景色変更
            this.customSortHeader1.Controls.OfType<Label>().ToList().ForEach(c => c.BackColor = backColor);

            // データグリッドのカラムヘッダ背景色変更
            this.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = backColor;

            // 20140529 syunrei No.730 マニフェストパターン一覧 start
            this.label13.BackColor = Color.Empty;
            // 20140529 syunrei No.730 マニフェストパターン一覧 end
        }

        /// <summary>
        /// パターン名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PATTERN_NAME_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void PATTERN_NAME_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PATTERN_NAME = this.PATTERN_NAME.Text;
        }

        /// <summary>
        /// 排出事業者名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HST_GYOUSHA_NAME_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void HST_GYOUSHA_NAME_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HST_GYOUSHA_NAME = this.HST_GYOUSHA_NAME.Text;
        }

        /// <summary>
        /// 排出事業場名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HST_GENBA_NAME_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void HST_GENBA_NAME_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HST_GENBA_NAME = this.HST_GENBA_NAME.Text;
        }

        /// <summary>
        /// 一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //例外操作対策
            if (this.customDataGridView1.SelectedCells.Count <= 0)
            {
                return;
            }

            if (e.RowIndex < 0)
            {
                return;
            }
            this.MPLogic.SelectData();
        }

        /// <summary>
        //パターン１
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン２
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン３
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン４
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        //パターン５
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn5_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// パターンデータ選択処理(F3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func3_Click(object sender, EventArgs e)
        {
            if (this.customDataGridView1.Enabled == false)
            {
                return;
            }
            if (this.MPLogic.SearchResult.Rows.Count > 0)
            {
                if (this.customDataGridView1.SelectedCells[0].RowIndex >= 0)
                {
                    this.MPLogic.SelectData();
                }
            }
        }

        /// <summary>
        /// 削除処理(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void bt_func4_Click(object sender, EventArgs e)
        {
            //2013-12-13 Add ogawamut PT 電マニ No.606
            //例外操作対策
            if (this.customDataGridView1.SelectedCells.Count <= 0)
            {
                return;
            }

            if (this.customDataGridView1.Enabled == false)
            {
                return;
            }
            if (this.customDataGridView1.SelectedCells[0].RowIndex >= 0)
            {
                this.MPLogic.LogicalDelete();
            }
        }

        /// <summary>
        /// 検索条件をクリアする(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            this.MPLogic.ClearScreen("ClsSearchCondition");
        }

        /// <summary>
        /// 検索処理(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            switch (this.MPLogic.Search())
            {
                case 0:
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    break;
            }
        }
        #region MOD NHU 20211005 #155786
        /// <summary>
        /// 規定登録処理(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            //2013-12-13 Add ogawamut PT 電マニ No.607
            //例外操作対策
            if (this.customDataGridView1.SelectedCells.Count <= 0)
            {
                return;
            }
            if (this.customDataGridView1.Enabled == false)
            {
                return;
            }                    
            if (this.customDataGridView1.SelectedCells[0].RowIndex >= 0)
            {
                switch (this.HaikiKbnCD)
                {
                    case "1"://産廃（直行）
                    case "2"://建廃
                    case "3"://産廃（積替）
                    default:
                        // 20140529 syunrei No.730 マニフェストパターン一覧 start
                        //this.MPLogic.Regist(base.RegistErrorFlag);
                        this.MPLogic.RegistUseDefaultKbn();
                        // 20140529 syunrei No.730 マニフェストパターン一覧 end
                        break;
                    case "4"://電子
                        //var messageShowLogic = new MessageBoxShowLogic();
                        //messageShowLogic.MessageBoxShow("E051", "電子マニフェスト以外のパターン");
                        this.MPLogic.RegistDenshiUseDefaultKbn();//155789
                        return;
                } 
            }
        }
        public virtual void bt_func2_Click(object sender, EventArgs e)
        {
            if (this.customDataGridView1.SelectedCells.Count <= 0)
            {
                return;
            }
            if (this.customDataGridView1.Enabled == false)
            {
                return;
            }
            this.MPLogic.DeleteUseDefaultKbn();
        }
        #endregion
        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            ////仕様不明なため、未実装。確認用
            //MessageBox.Show("並列移動", "フォーカス移動");
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// 画面を閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            this.MPLogic.ClearScreen("ClsSearchCondition");
            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// パターン一覧画面へ遷移(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            //社員コード
            string shainCD = this.ShainCd;

            //伝種区分
            int denshu = 0;
            denshu = (int)DenshuKbn;

            //戻り値
            String rtnSysID = String.Empty;

            var callHeader = new Shougun.Core.Common.PatternIchiran.APP.UIHeader();
            //社員コード、伝種区分を共通画面に渡す
            var callForm = new Shougun.Core.Common.PatternIchiran.UIForm(shainCD, denshu.ToString());
            var popForm = new BasePopForm(callForm, callHeader);

            //共通画面を起動する
            var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            if (!isExistForm)
            {
                popForm.ShowDialog();

                if (callForm.ParamOut_UpdateFlag)
                {
                    this.PatternButtonUpdate(sender, e);
                }
            }

            //戻り値
            rtnSysID = callForm.ParamOut_SysID;
        }

        /// <summary>
        /// 検索条件設定画面へ遷移(2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {
            //仕様不明なため、未実装。確認用
            //MessageBox.Show("検索条件設定画面", "画面遷移");
        }

        /// <summary>
        //ESCキー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXB_PROCESS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.MPLogic.SelectButton();
            }
        }

        /// <summary>
        /// パターンボタン更新処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        /// <param name="e">イベントクラス</param>
        /// <param name="ptnNo">パターンNo(0はデフォルトパターンを表示)</param>
        public void PatternButtonUpdate(object sender, System.EventArgs e, int ptnNo = -1)
        {
            if (ptnNo != -1) this.PatternNo = ptnNo;
            //this.OnLoad(e);
            this.PreOnLoad(e);
        }

        #endregion

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData()
        {
            r_framework.Utility.LogUtility.DebugMethodStart();
            //MOD NHU 20211005 #155786 S
            var parentForm = (BusinessBaseForm)this.Parent;
            //MOD NHU 20211005 #155786 E
            // 20140618 syunrei EV004876_マニフェストパターン一覧のアラート件数が効いていない start
            //this.logic.CreateDataGridView(this.MPLogic.SearchResult);
            if (this.MPLogic.SearchResult != null && this.MPLogic.SearchResult.Rows.Count>0)
            {
                int alertNum;
                int.TryParse(this.HeaderForm.AlertNumber.Text, System.Globalization.NumberStyles.AllowThousands, null, out alertNum);
                this.logic.AlertCount = alertNum;
                this.logic.CreateDataGridView(this.MPLogic.SearchResult);

                //MOD NHU 20211005 #155786 S
                var selectDefaultKbn = this.MPLogic.SearchResult.Select(" USE_DEFAULT_KBN = 1 ");
                if (selectDefaultKbn != null && selectDefaultKbn.Length > 0)
                {
                    if (!this.HaikiKbnCD.Equals("4"))//159144
                    {
                        parentForm.bt_func2.Enabled = true;
                    }
                }
                //MOD NHU 20211005 #155786 E

            }
            // 20140621 syunrei EV004937_マニフェストパターン一覧で検索時に1件もヒットしないと項目名がDBのカラム名になる start
            else
            {
                this.logic.CreateDataGridView(this.MPLogic.SearchResult);
                //非表示列を隠す
                this.MPLogic.SetColumnsVisible(this.HaikiKbnCD, false);
            }
            // 20140621 syunrei EV004937_マニフェストパターン一覧で検索時に1件もヒットしないと項目名がDBのカラム名になる start
            // 20140618 syunrei EV004876_マニフェストパターン一覧のアラート件数が効いていない end
           
            r_framework.Utility.LogUtility.DebugMethodEnd();
        }

        // 20140529 syunrei No.730 マニフェストパターン一覧 start
        /// <summary>
        /// マニフェストより背景色変更処理を行う
        /// </summary>
        public void SetBackColor(string imanifestKbn)
        {
            //タイトル更新
            this.MPLogic.ChangeTitle();

            //出力区分
            switch (imanifestKbn)
            {
                case "false"://一次
                    
                    this.ChangeLabelColor(true);
                    break;

                case "true"://二次
                   
                    this.ChangeLabelColor(false);
                    break;

                default:
                
                    break;
            }

        }

        private void cantxt_HaisyutugyoshaCD_TextChanged(object sender, EventArgs e)
        {
            cantxt_HaisyutugenbaCD.Text = string.Empty;
            ctxt_HaisyutugenbaName.Text = string.Empty;

        }

        private void cantxt_ShobungyoshaCD_TextChanged(object sender, EventArgs e)
        {
            if (cantxt_ShobungyoshaCD.Text != string.Empty)
            {
                cantxt_ShobunGenbaCD.Text = string.Empty;
                ctxt_ShobunGenbaName.Text = string.Empty;
            }
        }
        // 20140529 syunrei No.730 マニフェストパターン一覧 end 

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupBeforeExecuteMethod()
        {
            this.befHstGyousha = this.cantxt_HaisyutugyoshaCD.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod()
        {
            if (this.befHstGyousha != this.cantxt_HaisyutugyoshaCD.Text)
            {
                this.cantxt_HaisyutugenbaCD.Text = string.Empty;
                this.ctxt_HaisyutugenbaName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupBeforeExecuteMethod()
        {
            this.befSbnGyousha = this.cantxt_ShobungyoshaCD.Text;
        }

        /// <summary>
        /// 処分受託者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod()
        {
            if (this.befSbnGyousha != this.cantxt_ShobungyoshaCD.Text)
            {
                this.cantxt_ShobunGenbaCD.Text = string.Empty;
                this.ctxt_ShobunGenbaName.Text = string.Empty;
            }
        }
    }
}
