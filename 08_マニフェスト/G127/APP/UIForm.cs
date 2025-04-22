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
using r_framework.Logic;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Seasar.Quill;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.Dao;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PaperManifest.Manifestmeisaihyo
{
    /// <summary>
    /// マニフェスト明細表出力画面クラス
    /// </summary>
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">画面ID</param>
        /// <param name="window_type">画面区分</param>
        public UIForm(WINDOW_ID windowID, WINDOW_TYPE window_type)
            : base(windowID, window_type)
        {
            LogUtility.DebugMethodStart(windowID, window_type);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面がロードされたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            this.logic.WindowInit();

            // 最終の空行を表示させない
            this.customDataGridView1.AllowUserToAddRows = false;

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e">イベントが発生したオブジェクト</param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);

            this.logic.ManifestMeisaihyouDto = new ManifestMeisaihyouDto();
            this.SearchManifest();
        }

        /// <summary>
        /// 印刷ボタンを押下したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.CreateMeisaihyou();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV出力ボタンを押下したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc6_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            CSVExport csvExport = new CSVExport();
            csvExport.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, ConstClass.MANIFEST_MEISAIHYOU_TITLE, this);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索ボタンを押下したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc8_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SearchManifest();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じるボタンを押下したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索ポップアップを表示します
        /// </summary>
        private void SearchManifest()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件入力ポップアップを表示
                var popup = new JoukenPopupForm();
                popup.ManifestMeisaihyouDto = this.logic.ManifestMeisaihyouDto;
                popup.ShowDialog();

                if (DialogResult.OK == popup.DialogResult)
                {
                    var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                    var mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

                    this.logic.ManifestMeisaihyouDto = popup.ManifestMeisaihyouDto;

                    var dao = DaoInitUtility.GetComponent<IManifestMeisaihyouDao>();
                    this.logic.ManifestDataTable = dao.GetManifestData(this.logic.ManifestMeisaihyouDto);

                    var dt = this.logic.ManifestDataTable.Copy();
                    dt.Columns.Add("FORMAT_KOUFU_DATE");
                    dt.Columns.Add("FORMAT_HAIKI_SUU");
                    dt.Columns.Add("FORMAT_SUURYOU_NO_GOUKEI");
                    dt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);

                    dt.Rows.Cast<DataRow>().Where(r => null != r["KOUFU_DATE"] && !String.IsNullOrEmpty(r["KOUFU_DATE"].ToString()))
                                           .ToList()
                                           .ForEach(r => r["FORMAT_KOUFU_DATE"] = DateTime.ParseExact(r["KOUFU_DATE"].ToString(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("yyyy/MM/dd"));
                    dt.Rows.Cast<DataRow>().Where(r => null != r["HAIKI_SUU"] && !String.IsNullOrEmpty(r["HAIKI_SUU"].ToString()))
                                           .ToList()
                                           .ForEach(r => r["FORMAT_HAIKI_SUU"] = Convert.ToDecimal(r["HAIKI_SUU"]).ToString(mSysInfo.MANIFEST_SUURYO_FORMAT));
                    dt.Rows.Cast<DataRow>().Where(r => null != r["SUURYOU_NO_GOUKEI"] && !String.IsNullOrEmpty(r["SUURYOU_NO_GOUKEI"].ToString()))
                                           .ToList()
                                           .ForEach(r => r["FORMAT_SUURYOU_NO_GOUKEI"] = Convert.ToDecimal(r["SUURYOU_NO_GOUKEI"]).ToString(mSysInfo.MANIFEST_SUURYO_FORMAT));

                    this.customDataGridView1.AutoGenerateColumns = false;
                    this.customDataGridView1.IsBrowsePurpose = false;
                    this.customDataGridView1.DataSource = dt;
                    // 20140621 syunrei EV004915_マニフェスト明細表で検索時に1件もヒットし無い場合も何も表示されない　start
                    if (dt.Rows.Count <= 0)
                    {
                        //this.customDataGridView1.DataSource = null;
                        //this.customDataGridView1.Rows.Add();
                        MessageBoxShowLogic mess = new MessageBoxShowLogic();
                        if (mess.MessageBoxShow("C001") == DialogResult.OK)
                        {
                            this.SearchManifest();
                        }
                    }
                    // 20140621 syunrei EV004915_マニフェスト明細表で検索時に1件もヒットし無い場合も何も表示されない　end
                    this.customDataGridView1.IsBrowsePurpose = true;
                }

                popup.Dispose();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchManifest", ex);
                MessageBoxShowLogic message = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    message.MessageBoxShow("E093", "");
                }
                else
                {
                    message.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}
