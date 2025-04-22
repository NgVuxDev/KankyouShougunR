using System;
using System.Data;
using System.Data.SqlTypes;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Message;
using System.Windows.Forms;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.ShouninzumiDenshiShinseiIchiran
{
    /// <summary>
    /// G561 承認済申請一覧画面クラス
    /// </summary>
    public partial class ShouninzumiDenshiShinseiIchiranUIForm : SuperForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private ShouninzumiDenshiShinseiIchiranLogic logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">ウィンドウID</param>
        public ShouninzumiDenshiShinseiIchiranUIForm(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new ShouninzumiDenshiShinseiIchiranLogic(this);

            this.DENSHI_SHINSEI_ICHIRAN.AutoGenerateColumns = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面内の項目を初期化します
        /// </summary>
        public void Initialize()
        {
            LogUtility.DebugMethodStart();

            this.SetFormData(true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.logic.WindowInit())
            {
                return;
            }

            this.Initialize();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.DENSHI_SHINSEI_ICHIRAN != null)
            {
                int GRID_HEIGHT_MIN_VALUE = 365;
                int GRID_WIDTH_MIN_VALUE = 370;
                int h = this.Height - 81;
                int w = this.Width;

                if (h < GRID_HEIGHT_MIN_VALUE)
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Height = GRID_HEIGHT_MIN_VALUE;
                }
                else
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Height = h;
                }
                if (w < GRID_WIDTH_MIN_VALUE)
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Width = GRID_WIDTH_MIN_VALUE;
                }
                else
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Width = w;
                }

                if (this.DENSHI_SHINSEI_ICHIRAN.Height <= GRID_HEIGHT_MIN_VALUE
                    || this.DENSHI_SHINSEI_ICHIRAN.Width <= GRID_WIDTH_MIN_VALUE)
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                }
                else
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            if (!isShown)
            {
                this.Height -= 15;
                isShown = true;
            }

            base.OnShown(e);
        }

        /// <summary>
        /// FormのOnSizeChangedイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// 条件クリアボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Dto = new ShouninzumiDenshiShinseiIchiranDto();

            //var ribbon = (RibbonMainMenu)((BusinessBaseForm)this.Parent).ribbonForm;
            //this.logic.Dto.ShainCd = ribbon.GlobalCommonInformation.CurrentShain.SHAIN_CD;
            //this.logic.Dto.ShainName = ribbon.GlobalCommonInformation.CurrentShain.SHAIN_NAME_RYAKU;

            this.SetFormData(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc8_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var autoCheckLogic = new AutoRegistCheckLogic(this.allControl, this.allControl);
            this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (false == this.RegistErrorFlag)
            {
                /// 20141203 Houkakou 「承認済申請一覧」の日付チェックを追加する　start
                if (this.logic.DateCheck())
                {
                    return;
                }
                /// 20141203 Houkakou 「承認済申請一覧」の日付チェックを追加する　end
                
                int count = Search();
                if (count < 0)
                {
                    return;
                }
                else if (count == 0)
                {
                    MessageBoxUtility.MessageBoxShow("C001");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索処理を行います
        /// </summary>
        /// <returns></returns>
        internal int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.GetFormData();
                var count = this.logic.Search();

                var header = (ShouninzumiDenshiShinseiIchiranHeaderForm)((BusinessBaseForm)this.Parent).headerForm;
                header.readDataNumber.Text = count.ToString();

                LogUtility.DebugMethodEnd(count);

                return count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// 本登録ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc9_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var row = this.DENSHI_SHINSEI_ICHIRAN.CurrentRow;
            if (null != row)
            {
                var isError = false;

                var systemId = row.Cells["SYSTEM_ID"].Value;
                var seq = row.Cells["SEQ"].Value;
                var hikiaiTorihikisakiCd = row.Cells["HIKIAI_TORIHIKISAKI_CD"].Value;
                var hikiaiGyoushaCd = row.Cells["HIKIAI_GYOUSHA_CD"].Value;
                var hikiaiGenbaCd = row.Cells["HIKIAI_GENBA_CD"].Value;
                var torihikisakiCd = row.Cells["TORIHIKISAKI_CD"].Value;
                var gyoushaCd = row.Cells["GYOUSHA_CD"].Value;
                var genbaCd = row.Cells["GENBA_CD"].Value;
                var hikiaiGyoushaUseFlg = row.Cells["HIKIAI_GYOUSHA_USE_FLG"].Value;

                if (null != hikiaiGenbaCd && !String.IsNullOrEmpty(hikiaiGenbaCd.ToString()))
                {
                    // 引合現場CDがある場合

                    // 業者CDがない場合は、本登録不可
                    if (null == gyoushaCd || String.IsNullOrEmpty(gyoushaCd.ToString()))
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E190", "引合業者");
                        isError = true;
                    }
                    // 引合取引先CDがあって、取引先CDがない場合は、本登録不可
                    if (null != hikiaiTorihikisakiCd && !String.IsNullOrEmpty(hikiaiTorihikisakiCd.ToString()) && (null == torihikisakiCd || String.IsNullOrEmpty(torihikisakiCd.ToString())))
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E190", "引合取引先");
                        isError = true;
                    }
                }
                else if (null != hikiaiGyoushaCd && !String.IsNullOrEmpty(hikiaiGyoushaCd.ToString()))
                {
                    // 引合業者CDがある場合

                    // 引合取引先CDがあって、取引先CDがない場合は、本登録不可
                    if (null != hikiaiTorihikisakiCd && !String.IsNullOrEmpty(hikiaiTorihikisakiCd.ToString()) && (null == torihikisakiCd || String.IsNullOrEmpty(torihikisakiCd.ToString())))
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E190", "引合取引先");
                        isError = true;
                    }
                }

                // 申請のデータ存在チェック
                bool catchErr = true;
                bool checkRet = this.logic.IsRegistDenshiShinsei(systemId, seq, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (checkRet)
                {
                    new MessageBoxShowLogic().MessageBoxShow("E191");
                    isError = true;
                }


                if (false == isError)
                {
                    if (null != genbaCd && !String.IsNullOrEmpty(genbaCd.ToString()))
                    {
                        // 現場マスタ（修正）
                        FormManager.OpenFormWithAuth("M217", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, string.Empty, string.Empty, false, true, torihikisakiCd, gyoushaCd, genbaCd, hikiaiTorihikisakiCd, hikiaiGyoushaCd, hikiaiGenbaCd, systemId, seq);
                    }
                    else if (null != hikiaiGenbaCd && !String.IsNullOrEmpty(hikiaiGenbaCd.ToString()))
                    {
                        // 現場マスタ（新規）
                        FormManager.OpenFormWithAuth("M217", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, string.Empty, string.Empty, false, true, torihikisakiCd, gyoushaCd, genbaCd, hikiaiTorihikisakiCd, hikiaiGyoushaCd, hikiaiGenbaCd, systemId, seq, hikiaiGyoushaUseFlg);
                    }
                    else if (null != gyoushaCd && !String.IsNullOrEmpty(gyoushaCd.ToString()))
                    {
                        // 業者マスタ（修正）
                        FormManager.OpenFormWithAuth("M215", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, string.Empty, false, true, gyoushaCd, hikiaiGyoushaCd, systemId, seq);
                    }
                    else if (null != hikiaiGyoushaCd && !String.IsNullOrEmpty(hikiaiGyoushaCd.ToString()))
                    {
                        // 業者マスタ（新規）
                        FormManager.OpenFormWithAuth("M215", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, string.Empty, false, true, gyoushaCd, hikiaiGyoushaCd, systemId, seq);
                    }
                    else if (null != torihikisakiCd && !String.IsNullOrEmpty(torihikisakiCd.ToString()))
                    {
                        // 取引先マスタ（修正）
                        FormManager.OpenFormWithAuth("M213", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, string.Empty, false, true, torihikisakiCd, hikiaiTorihikisakiCd, systemId, seq);
                    }
                    else if (null != hikiaiTorihikisakiCd && !String.IsNullOrEmpty(hikiaiTorihikisakiCd.ToString()))
                    {
                        // 取引先マスタ（新規）
                        FormManager.OpenFormWithAuth("M213", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, string.Empty, false, true, torihikisakiCd, hikiaiTorihikisakiCd, systemId, seq);
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じるボタンがクリックされたときに処理します
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
        /// DTOに画面からデータをセットします
        /// </summary>
        private void GetFormData()
        {
            LogUtility.DebugMethodStart();

            var header = (ShouninzumiDenshiShinseiIchiranHeaderForm)((BusinessBaseForm)this.Parent).headerForm;
            this.logic.Dto.KyotenCd = header.KYOTEN_CD.Text;
            this.logic.Dto.KyotenName = header.KYOTEN_NAME_RYAKU.Text;

            this.logic.Dto.ShainCd = this.SHAIN_CD.Text;
            this.logic.Dto.ShainName = this.SHAIN_NAME.Text;
            this.logic.Dto.ShinseiDateFrom = this.SHINSEI_DATE_FROM.Value == null ? SqlDateTime.Null : SqlDateTime.Parse(this.SHINSEI_DATE_FROM.Value.ToString());
            this.logic.Dto.ShinseiDateTo = this.SHINSEI_DATE_TO.Value == null ? SqlDateTime.Null : SqlDateTime.Parse(this.SHINSEI_DATE_TO.Value.ToString());

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// DTOのデータを画面にセットします
        /// </summary>
        /// <param name="isSetKyoten">拠点CDを設定するかのフラグ</param>
        private void SetFormData(bool isSetKyoten)
        {
            LogUtility.DebugMethodStart();

            var header = (ShouninzumiDenshiShinseiIchiranHeaderForm)((BusinessBaseForm)this.Parent).headerForm;
            if (isSetKyoten)
            {
                // F7条件クリア押下時は拠点を初期化しない
                header.KYOTEN_CD.Text = this.logic.Dto.KyotenCd;
                header.KYOTEN_NAME_RYAKU.Text = this.logic.Dto.KyotenName;
            }
            header.readDataNumber.Text = "0";

            this.SHAIN_CD.Text = this.logic.Dto.ShainCd;
            this.SHAIN_NAME.Text = this.logic.Dto.ShainName;
            this.SHINSEI_DATE_FROM.Text = this.logic.Dto.ShinseiDateFrom.IsNull ? string.Empty : this.logic.Dto.ShinseiDateFrom.ToString();
            this.SHINSEI_DATE_TO.Text = this.logic.Dto.ShinseiDateTo.IsNull ? string.Empty : this.logic.Dto.ShinseiDateTo.ToString();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 申請一覧のDataSourceにデータをセットします
        /// </summary>
        /// <param name="dataTable"></param>
        internal void SetDenshiShinseiIchiranDataSource(DataTable dataTable)
        {
            LogUtility.DebugMethodStart();

            this.DENSHI_SHINSEI_ICHIRAN.IsBrowsePurpose = false;
            this.DENSHI_SHINSEI_ICHIRAN.DataSource = dataTable;
            this.DENSHI_SHINSEI_ICHIRAN.IsBrowsePurpose = true;

            LogUtility.DebugMethodEnd();
        }
    }
}
