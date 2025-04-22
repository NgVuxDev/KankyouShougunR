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
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku.Logic;

namespace Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku
{
    public partial class DenshiManifestPatternTouroku : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private DenshiManifestPatternTourokuLogic logic;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// 電子マニフェストパターン
        /// </summary>
        public List<DT_PT_R18> EpEntrylist { get; set; }

        /// <summary>
        /// 電子マニフェストパターン収集運搬
        /// </summary>
        public List<DT_PT_R19> EpUpnlist { get; set; }

        /// <summary>
        /// 電子マニフェストパターン最終処分
        /// </summary>
        public List<DT_PT_R13> EpLastSbnlist { get; set; }

        /// <summary>
        /// 電子マニフェストパターン備考
        /// </summary>
        public List<DT_PT_R06> EpBikoulist { get; set; }

        /// <summary>
        /// 電子マニフェストパターン連絡番号
        /// </summary>
        public List<DT_PT_R05> EpRenrakulist { get; set; }

        /// <summary>
        /// 電子マニフェストパターン最終処分(予定)
        /// </summary>
        public List<DT_PT_R04> EpLastSbnYoteilist { get; set; }

        /// <summary>
        /// 電子マニフェストパターン有害物質
        /// </summary>
        public List<DT_PT_R02> EpYuugailist { get; set; }

        /// <summary>
        /// 電子マニフェストパターン拡張
        /// </summary>
        public List<DT_PT_R18_EX> EpEntryEXlist { get; set; }

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

        /// <summary>
        /// 更新モード、更新成功フラグ
        /// </summary>
        public bool UpdModeUpdSuccessFlg { get; internal set; }

        /// <summary>
        /// 一次二次区分
        /// </summary>
        internal int FirstManifestKbn { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenshiManifestPatternTouroku()
        {
            this.InitializeComponent();

            this.RegistedSystemId = System.Data.SqlTypes.SqlInt64.Null;
            this.RegistedSeq = System.Data.SqlTypes.SqlInt32.Null;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new DenshiManifestPatternTourokuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="firstManifestKbn">一次二次区分</param>
        /// <param name="entrylist">電子マニフェストパターンリスト</param>
        /// <param name="upnlist">電子マニフェストパターン収集運搬リスト</param>
        /// <param name="lastSbnlist">電子マニフェストパターン最終処分リスト</param>
        /// <param name="bikoulist">電子マニフェストパターン備考リスト</param>
        /// <param name="renrakulist">電子マニフェストパターン連絡番号リスト</param>
        /// <param name="lastSbnYoteilist">電子マニフェストパターン最終処分(予定)リスト</param>
        /// <param name="yuugailist">電子マニフェストパターン有害物質リスト</param>
        /// <param name="entryEXlist">電子マニフェストパターン拡張リスト</param>
        public DenshiManifestPatternTouroku(int firstManifestKbn
            , List<DT_PT_R18> entrylist
            , List<DT_PT_R19> upnlist
            , List<DT_PT_R13> lastSbnlist
            , List<DT_PT_R06> bikoulist
            , List<DT_PT_R05> renrakulist
            , List<DT_PT_R04> lastSbnYoteilist
            , List<DT_PT_R02> yuugailist
            , List<DT_PT_R18_EX> entryEXlist
            )
           : base()
        {
            this.InitializeComponent();

            this.RegistedSystemId = System.Data.SqlTypes.SqlInt64.Null;
            this.RegistedSeq = System.Data.SqlTypes.SqlInt32.Null;

            //パラメータ
            Properties.Settings.Default.firstManifestKbn = firstManifestKbn;
            Properties.Settings.Default.Save();
            this.FirstManifestKbn = firstManifestKbn;

            //新規または修正の判断
            if (entrylist[0].SYSTEM_ID.IsNull) processKbn = PROCESS_KBN.NEW;
            else processKbn = PROCESS_KBN.UPDATE;

            this.EpEntrylist = entrylist;
            this.EpUpnlist = upnlist;
            this.EpLastSbnlist = lastSbnlist;
            this.EpBikoulist = bikoulist;
            this.EpRenrakulist = renrakulist;
            this.EpLastSbnYoteilist = lastSbnYoteilist;
            this.EpYuugailist = yuugailist;
            this.EpEntryEXlist = entryEXlist;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new DenshiManifestPatternTourokuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// Load イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //表示の初期化
            this.logic.ClearScreen();

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
            Properties.Settings.Default.firstManifestKbn = 0;
            Properties.Settings.Default.Save();

            var parentForm = (BusinessBaseForm)this.Parent;
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
        public void ControlKeyUp(object sender, KeyEventArgs e)
        {

        }

        public void DenshiManifestPatternTouroku_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F9:
                    ControlUtility.ClickButton(this, "bt_func9");
                    break;

                case Keys.F12:
                    ControlUtility.ClickButton(this, "bt_func12");
                    break;
            }
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
    