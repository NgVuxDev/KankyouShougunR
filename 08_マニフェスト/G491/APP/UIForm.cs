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
using r_framework.Entity;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.PaperManifest.ManifestPatternTouroku.Logic;
using r_framework.Utility;

namespace Shougun.Core.PaperManifest.ManifestPatternTouroku
{
    public partial class ManifestPatternTouroku : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private ManifestPatternRegistLogic logic;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// マニフェストパターン
        /// </summary>
        public List<T_MANIFEST_PT_ENTRY> PtEntrylist { get; set; }

        /// <summary>
        /// マニフェストパターン収集運搬
        /// </summary>
        public List<T_MANIFEST_PT_UPN> PtUpnlist { get; set; }

        /// <summary>
        /// マニフェストパターン印字
        /// </summary>
        public List<T_MANIFEST_PT_PRT> PtPrtlist { get; set; }

        /// <summary>
        /// マニフェストパターン印字明細
        /// </summary>
        public List<T_MANIFEST_PT_DETAIL_PRT> PtDetailprtlist { get; set; }

        /// <summary>
        /// マニフェストパターン明細
        /// </summary>
        public List<T_MANIFEST_PT_DETAIL> PtDetaillist { get; set; }

        /// <summary>
        /// マニフェストパターン印字_建廃_形状
        /// </summary>
        public List<T_MANIFEST_PT_KP_KEIJYOU> PtKeijyoulist { get; set; }

        /// <summary>
        /// マニフェストパターン印字_建廃_荷姿
        /// </summary>
        public List<T_MANIFEST_PT_KP_NISUGATA> PtNiugatalist { get; set; }

        /// <summary>
        /// マニフェストパターン印字_建廃_処分方法
        /// </summary>
        public List<T_MANIFEST_PT_KP_SBN_HOUHOU> PtHouhoulist { get; set; }

        /// <summary>
        /// 新規または更新モード
        /// </summary>
        public PROCESS_KBN processKbn { get; internal set; }

        /// <summary>
        /// 登録結果のシステムID nullの場合は登録なし
        /// </summary>
        public System.Data.SqlTypes.SqlInt64 RegistedSystemId { get; internal set; }

        /// <summary>
        /// 登録結果のSEQ nullの場合は登録なし
        /// </summary>
        public System.Data.SqlTypes.SqlInt32 RegistedSeq { get; internal set; }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        public ManifestPatternTouroku()
        {
            this.InitializeComponent();

            this.RegistedSystemId = System.Data.SqlTypes.SqlInt64.Null;
            this.RegistedSeq = System.Data.SqlTypes.SqlInt32.Null;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ManifestPatternRegistLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dentaneMode">伝種区分</param>
        /// <param name="firstManifestKbn">一次二次区分</param>
        /// <param name="listRegistKbn">一括登録区分</param>
        /// <param name="entrylist">マニフェスト</param>
        /// <param name="upnlist">マニ収集運搬</param>
        /// <param name="prtlist">マニ印字</param>
        /// <param name="detailprtlist">マニ印字明細</param>
        /// <param name="detaillist">マニ明細</param>
        /// <param name="keijyoulist">マニ印字_建廃_形状</param>
        /// <param name="houhoulist">マニ印字_建廃_荷姿</param>
        /// <param name="niugatalist">マニ印字_建廃_処分方法</param>
        public ManifestPatternTouroku(int dentaneMode, int firstManifestKbn, bool listRegistKbn
            , List<T_MANIFEST_PT_ENTRY> entrylist
            , List<T_MANIFEST_PT_UPN> upnlist
            , List<T_MANIFEST_PT_PRT> prtlist
            , List<T_MANIFEST_PT_DETAIL_PRT> detailprtlist
            , List<T_MANIFEST_PT_DETAIL> detaillist
            , List<T_MANIFEST_PT_KP_KEIJYOU> keijyoulist
            , List<T_MANIFEST_PT_KP_NISUGATA> niugatalist
            , List<T_MANIFEST_PT_KP_SBN_HOUHOU> houhoulist
            )
            : base()
        {
            this.InitializeComponent();

            this.RegistedSystemId = System.Data.SqlTypes.SqlInt64.Null;
            this.RegistedSeq = System.Data.SqlTypes.SqlInt32.Null;

            //パラメータ
            Properties.Settings.Default.dentaneMode = dentaneMode;
            Properties.Settings.Default.firstManifestKbn = firstManifestKbn;
            Properties.Settings.Default.listRegistKbn = listRegistKbn;
            Properties.Settings.Default.Save();

            if (entrylist[0].SYSTEM_ID.IsNull) processKbn = PROCESS_KBN.NEW;
            else processKbn = PROCESS_KBN.UPDATE;

            this.PtEntrylist = entrylist;
            this.PtUpnlist = upnlist;
            this.PtPrtlist = prtlist;
            this.PtDetailprtlist = detailprtlist;
            this.PtDetaillist = detaillist;
            this.PtKeijyoulist = keijyoulist;
            this.PtNiugatalist = niugatalist;
            this.PtHouhoulist = houhoulist;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ManifestPatternRegistLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //表示の初期化
            this.logic.ClearScreen("Initial");

            if (this.isLoaded == false)
            {
                //初期化、初期表示
                this.logic.WindowInit();
            }
        }

        /// <summary>
        /// フォーム閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FormClose(object sender, EventArgs e)
        {
            Properties.Settings.Default.dentaneMode = 0;
            Properties.Settings.Default.firstManifestKbn = 0;
            Properties.Settings.Default.listRegistKbn = false;
            Properties.Settings.Default.Save();

            this.Close();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        internal void Regist(object sender, System.EventArgs e)
        {
            this.logic.Regist(true);
        }

        /// <summary>
        /// キー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlKeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// システムメニューを設定する
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0xa1 && (int)m.WParam == 0x3)
            {
                return;
            }
            if (m.Msg == 0xa3 && ((int)m.WParam == 0x3 || (int)m.WParam == 0x2))
            {
                return;
            }
            if (m.Msg == 0xa4 && ((int)m.WParam == 0x2 || (int)m.WParam == 0x3))
            {
                return;
            }
            if (m.Msg == 0x112 && (int)m.WParam == 0xf100)
            {
                return;
            }
            base.WndProc(ref m);
        }

    }
}
