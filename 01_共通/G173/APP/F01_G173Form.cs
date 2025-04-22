using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.KokyakuKarute.Logic;

namespace Shougun.Core.Common.KokyakuKarute.APP
{
    public partial class G173Form : SuperForm
    {
        //取引先CD
        private string TorihikisakiCd = "";

        //業者CD
        private string GyoushaCd = "";

        //現場CD
        private string GenbaCd = "";

        //前取引先CD 比較用
        private string maeTorihikisakiCd = "";

        //前業者CD 比較用
        private string maeGyoushaCd = "";

        //前現場CD 比較用
        private string maeGenbaCd = "";

        //初期表示フラグ
        private bool FormShowFlg = false;

        //条件空のフラグ
        private bool JyoukenNullFlg = false;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private G173Logic logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public G173Form()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G173Logic(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <param name="gyoushaCD">業者CD</param>
        /// <param name="genbaCD">現場CD</param>
        public G173Form(string torihikisakiCD, string gyoushaCD, string genbaCD)
        {
            LogUtility.DebugMethodStart(torihikisakiCD, gyoushaCD, genbaCD);
            try
            {
                this.InitializeComponent();

                this.TorihikisakiCd = torihikisakiCD;
                this.GyoushaCd = gyoushaCD;
                this.GenbaCd = genbaCD;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G173Logic(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnLoad(e);

                //VAN 20210921 #154849 S
                //列を自動作成が要らないため、falseにする
                this.Gyousha_ItakuKeiyaku_Ichiran.AutoGenerateColumns = false;
                this.Genba_ItakuKeiyaku_Ichiran.AutoGenerateColumns = false;
                this.Genba_TsukiHinmei_Ichiran.AutoGenerateColumns = false;

                this.Uketsuke_Denpyou.AutoGenerateColumns = false;
                this.Uketsuke_Meisai.AutoGenerateColumns = false;
                this.Uketsuke_Kuremu_Denpyou.AutoGenerateColumns = false;

                this.Ukeire_Denpyou.AutoGenerateColumns = false;
                this.Shukka_Denpyou.AutoGenerateColumns = false;
                this.ShukkaKenshuu_Denpyou.AutoGenerateColumns = false;
                this.UriageShiharai_Denpyou.AutoGenerateColumns = false;
                this.UriageShiharai_Meisai.AutoGenerateColumns = false;

                this.Nyuukin_Denpyou.AutoGenerateColumns = false;
                this.Nyuukin_Meisai.AutoGenerateColumns = false;
                this.Shukkin_Denpyou.AutoGenerateColumns = false;
                this.Shukkin_Meisai.AutoGenerateColumns = false;

                this.Tanka_Shiharai_Ichiran.AutoGenerateColumns = false;
                this.Tanka_Uriage_Ichiran.AutoGenerateColumns = false;
                //VAN 20210921 #154849 E

                //初期処理
                this.logic.WindowInit();

                if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                {
                    this.MultiRow_UkeireMeisai.Template = this.ukeireDetail1;
                }
                else
                {
                    this.MultiRow_UkeireMeisai.Template = this.ukeireDetail2;
                }

                if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                {
                    this.MultiRow_ShukkaMeisai.Template = this.shukkaDetail1;
                }
                else
                {
                    this.MultiRow_ShukkaMeisai.Template = this.shukkaDetail2;
                }

                //取引先CD
                if (!"".Equals(this.TorihikisakiCd) && null != this.TorihikisakiCd)
                {
                    this.TORIHIKISAKI_CD.Text = this.TorihikisakiCd;
                    this.TorihikisakiFORTorihikisakiCd_Select();
                }
                //業者CD
                if (!"".Equals(this.GyoushaCd) && null != this.GyoushaCd)
                {
                    this.GYOUSHA_CD.Text = this.GyoushaCd;
                    this.GyoushaFORGyoushaCD_Select();
                }
                //現場CD
                if (!"".Equals(this.GenbaCd) && null != this.GenbaCd)
                {
                    this.GENBA_CD.Text = this.GenbaCd;
                    this.GenbaFORGenbaCD_Select();
                }
                //初期の条件を保存
                this.logic.TorihikisakiCD = this.TorihikisakiCd;
                this.logic.GyoushaCD = this.GyoushaCd;
                this.logic.GenbaCD = this.GenbaCd;

                //内容を記憶,比較用
                this.maeTorihikisakiCd = this.TORIHIKISAKI_CD.Text;
                this.maeGyoushaCd = this.GYOUSHA_CD.Text;
                this.maeGenbaCd = this.GENBA_CD.Text;

                //初期表示検索(伝票ー受付)
                this.UketsukeSearch(null, e);

                //初期表示フラグ
                this.FormShowFlg = true;

                // 単価タブの売上・支払の単価でシステム設定のフォーマットを反映させるために一度タブを選択し画面を表示状態とする。
                // IsBrowsePurposeプロパティの影響で、初期表示時のみフォーマットが反映されない対策
                this.tabControl1.SuspendLayout();
                this.tabControl1.SelectedTab = this.tabPage_tanka;
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.tabControl1.ResumeLayout();

                //取引先CDにフォーカス
                this.TORIHIKISAKI_CD.Focus();

                if (!this.logic.IsUserFullAuth())
                {
                    this.logic.GetUserAuthRead();
                }
                this.logic.SetEnabledAuthRead();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.tabControl1 != null)
                {
                    this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                var parentForm = (BusinessBaseForm)this.Parent;

                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                throw ex;
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
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }

        /// <summary>
        /// 伝票ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Tab_Denpyou_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //初期表示検索(伝票ー受付)
                if (!this.JyoukenNullFlg)
                {
                    //受付情報取得
                    //this.UketsukeDate_Select();

                    if (this.tabControl1.TabPages.Count > 0 && this.tabControl1.TabPages[0].Name == "TabPage_Denpyou")
                    {
                        this.logic.AddTabComeBack();
                    }

                    if (this.TabControl_Denpyou.TabPages.Count > 0)
                    {
                        var page = this.TabControl_Denpyou.TabPages[0];
                        switch (page.Name)
                        {
                            case "Denpyou_TabPage_Uketsuke":
                                //受付
                                this.Denpyou_TabPage_Uketsuke_Click();
                                //set denpyou date label text
                                this.logic.header.label3.Text = "作業日※";
                                break;
                            case "Denpyou_TabPage_Uketsuke_Kuremu":
                                //受付クレーム
                                this.Denpyou_TabPage_Uketsuke_Kuremu_Click();
                                //set denpyou date label text
                                this.logic.header.label3.Text = "受付日※";
                                break;
                            case "Denpyou_TabPage_Ukeire":
                                //受入
                                Denpyou_TabPage_Ukeire_Click();
                                //set denpyou date label text
                                this.logic.header.label3.Text = "伝票日付※";
                                break;
                            case "Denpyou_TabPage_Shukka":
                                //出荷
                                Denpyou_TabPage_Shukka_Click();
                                //set denpyou date label text
                                this.logic.header.label3.Text = "伝票日付※";
                                break;
                            case "Denpyou_TabPage_UriageShiharai":
                                //売上/支払
                                Denpyou_TabPage_UriageShiharai_Click();
                                //set denpyou date label text
                                this.logic.header.label3.Text = "伝票日付※";
                                break;
                            case "Denpyou_TabPage_Dainou":
                                //代納
                                this.Denpyou_TabPage_Dainou_Click();
                                this.logic.header.label3.Text = "伝票日付※";
                                break;
                            case "Denpyou_TabPage_Nyuukin":
                                //入金
                                Denpyou_TabPage_Nyuukin_Click();
                                //set denpyou date label text
                                this.logic.header.label3.Text = "伝票日付※";
                                break;
                            case "Denpyou_TabPage_Shukkin":
                                //出金
                                Denpyou_TabPage_Shukkin_Click();
                                //set denpyou date label text
                                this.logic.header.label3.Text = "伝票日付※";
                                break;
                            case "Denpyou_TabPage_ShukkaKenshuu":
                                //検収
                                Denpyou_TabPage_ShukkaKenshuu_Click();
                                this.logic.header.label3.Text = "伝票日付※";
                                break;
                            case "Denpyou_TabPage_Contena":
                                //コンテナ
                                this.logic.header.label3.Text = "最終更新日※";
                                this.Denpyou_TabPage_Contena_Click();
                                break;
                        }
                    }
                }
                else
                {
                    if (this.tabControl1.TabPages.Count > 0 && this.tabControl1.TabPages[0].Name == "TabPage_Denpyou")
                    {
                        this.logic.AddTabComeBack();
                    }
                }

                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                //this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Uketsuke;
                if (this.TabControl_Denpyou.TabPages.Count > 0)
                {
                    this.TabControl_Denpyou.SelectedTab = this.TabControl_Denpyou.TabPages[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Tab_TorihikisakiMaster_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //取引先CD
                string torihikisaki_cd = this.logic.TorihikisakiCD;
                if (!this.JyoukenNullFlg)
                {
                    //すでに検索したら、再検索をしない
                    //if (!this.logic.TorihikisakiFlg)
                    //{
                    if (null != torihikisaki_cd && !"".Equals(torihikisaki_cd) && !DBNull.Value.Equals(torihikisaki_cd))
                    {
                        //取引先マスタのヘッダ情報取得
                        this.logic.TorihikisakiHeaderSearch();
                        //取引先マスタの基本情報取得
                        this.logic.TorihikisakiKihonSearch();
                        //取引先請求情報取得
                        this.logic.TorihikisakiSeikyuuSearch();
                        //取引先支払情報取得
                        this.logic.TorihikisakiShiharaiSearch();
                        //取引先業者一覧取得
                        this.logic.TorihikisakiGyoushaIchiranSearch();
                    }
                    //}

                    //取引先マスタのヘッダ情報表示
                    this.logic.TorihikisakiHeaderHyouji();
                }

                this.logic.AddTabComeBack();

                //タブー活性化
                this.tabControl1.SelectedTab = this.tabPage_torihikisaki;
                this.TabControl_Torihikisaki.SelectedTab = this.Torihikisaki_TabPage_HeaderInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Tab_GyoushaMaster_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //取引先CD
                string torihikisaki_cd = this.logic.TorihikisakiCD;
                //業者CD
                string gyousha_cd = this.logic.GyoushaCD;
                if (!this.JyoukenNullFlg)
                {
                    //すでに検索したら、再検索をしない
                    //if (!this.logic.GyoushaFlg)
                    //{
                    if (null != gyousha_cd && !"".Equals(gyousha_cd) && !DBNull.Value.Equals(gyousha_cd))
                    {
                        //業者マスタの基本情報,請求情報、支払情報、取得
                        this.logic.GyoushaInfoSearch();
                        //業者現場一覧取得
                        this.logic.GyoushaGenbaIchiranSearch();
                    }
                    //}

                    //業者マスタのヘッダ情報表示
                    this.logic.GyoushaHeaderHyouji();
                }

                this.logic.AddTabComeBack();

                //タブー活性化
                this.tabControl1.SelectedTab = this.tabPage_gyousha;
                this.TabControl_Gyousha.SelectedTab = this.Gyousha_TabPage_HeaderInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Tab_GenbaMaster_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //取引先CD
                string trihikisaki_cd = this.logic.TorihikisakiCD;
                //業者CD
                string gyousha_cd = this.logic.GyoushaCD;
                //現場CD
                string genba_cd = this.logic.GenbaCD;
                if (!this.JyoukenNullFlg)
                {
                    //すでに検索したら、再検索をしない
                    //if (!this.logic.GenbaFlg)
                    //{
                    if (null != genba_cd && !"".Equals(genba_cd) && !DBNull.Value.Equals(genba_cd))
                    {
                        //現場マスタの基本情報,請求情報、支払情報取得
                        this.logic.GenbaInfoSearch();
                    }
                    //}

                    //現場マスタのヘッダ情報表示
                    this.logic.GenbaHeaderHyouji();
                }

                this.logic.AddTabComeBack();

                //タブー活性化
                this.tabControl1.SelectedTab = this.tabPage_genba;
                this.TabControl_Genba.SelectedTab = this.Genba_TabPage_HeaderInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 単価ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Tab_Tanka_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //売上
                    int uriageResult = this.logic.KobetsuHinmeiTankaSearch(1);
                    //支払
                    int siharaiResult = this.logic.KobetsuHinmeiTankaSearch(2);

                    if (uriageResult == -1 && siharaiResult == -1)
                    {
                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    this.Tanka_Uriage_Ichiran.IsBrowsePurpose = false;
                    this.Tanka_Shiharai_Ichiran.IsBrowsePurpose = false;

                    //売上
                    var uriageTable = this.logic.KobetsuHinmeiTankaUriageSearchResult;
                    //支払
                    var siharaiTable = this.logic.KobetsuHinmeiTankaShiharaiSearchResult;

                    uriageTable.BeginLoadData();
                    siharaiTable.BeginLoadData();

                    this.Tanka_Uriage_Ichiran.DataSource = uriageTable;
                    this.Tanka_Shiharai_Ichiran.DataSource = siharaiTable;

                    this.Tanka_Uriage_Ichiran.IsBrowsePurpose = true;
                    this.Tanka_Shiharai_Ichiran.IsBrowsePurpose = true;
                }
                else
                {
                    this.Tanka_Uriage_Ichiran.IsBrowsePurpose = true;
                    this.Tanka_Shiharai_Ichiran.IsBrowsePurpose = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            this.logic.AddTabComeBack();
            this.tabControl1.SelectedTab = this.tabPage_tanka;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件ｸﾘｱボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Conditions_Clear_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // 検索条件をクリア
                this.logic.searchConfirionsClear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UketsukeSearch(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                var allControlAndHeaderControls = allControl.ToList();
                allControlAndHeaderControls.AddRange(this.controlUtil.GetAllControls(this.logic.header));
                allControlAndHeaderControls = allControlAndHeaderControls.Where(c => !(c is CustomDataGridView)).ToList();
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                if (this.RegistErrorFlag)
                {
                    this.logic.SetErrorFocus();
                    return;
                }
                //Check denpyou date
                if (!this.logic.DateCheck())
                {
                    return;
                }
                //条件の取引先CD,業者CD,現場CD 保存
                //取引先CD
                this.logic.TorihikisakiCD = this.TORIHIKISAKI_CD.Text;
                //業者CD
                this.logic.GyoushaCD = this.GYOUSHA_CD.Text;
                //現場CD
                this.logic.GenbaCD = this.GENBA_CD.Text;
                //拠点CD
                if (!string.IsNullOrEmpty(this.logic.header.KYOTEN_CD.Text))
                {
                    this.logic.KyotenCD = int.Parse(this.logic.header.KYOTEN_CD.Text).ToString();
                }
                //From date
                if (this.logic.header.DATE_FROM.Value != null)
                {
                    this.logic.FromDate = ((DateTime)this.logic.header.DATE_FROM.Value).ToString("yyyy-MM-dd");
                }
                //To date
                if (this.logic.header.DATE_TO.Value != null)
                {
                    this.logic.ToDate = ((DateTime)this.logic.header.DATE_TO.Value).ToString("yyyy-MM-dd");
                }

                //検索フラグを初期化にする
                this.logic.FlgInit();

                //条件NULLか判定
                if ((null == this.logic.TorihikisakiCD || "".Equals(this.logic.TorihikisakiCD))
                        && (null == this.logic.GyoushaCD || "".Equals(this.logic.GyoushaCD))
                        && (null == this.logic.GenbaCD || "".Equals(this.logic.GenbaCD)))
                {
                    this.JyoukenNullFlg = true;
                }
                else
                {
                    this.JyoukenNullFlg = false;
                }

                if (!this.logic.IsUserFullAuth())
                {
                    this.logic.GetUserAuthRead();
                }
                this.logic.SetEnabledAuthRead();

                //フォーム起動するか
                if (this.FormShowFlg)
                {
                    //再検索時、各一覧の明細を初期化
                    this.logic.MeisaiInit();

                    //一つの条件が必須です
                    if (this.JyoukenNullFlg)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E012", "取引先CD、業者CD、現場CDから一つ以上のコード値");
                        //赤色にする
                        //this.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        //フォーカスにする
                        this.TORIHIKISAKI_CD.Focus();

                        LogUtility.DebugMethodEnd();
                        return;
                    }
                    else
                    {
                        //受付情報取得
                        //this.UketsukeDate_Select();
                        this.GetTabControl1Data();
                    }
                }
                else
                {
                    if (!this.JyoukenNullFlg)
                    {
                        //受付情報取得
                        //this.UketsukeDate_Select();
                        this.GetTabControl1Data();
                    }
                }

                //タブー活性化
                //this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                //this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Uketsuke;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F9 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal void Copy_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            string copyNo = "";
            string tabName = "";

            //呼び出し画面判別
            tabName = tabControl1.SelectedTab.Text;

            #region 複写モードで画面を開く(伝票)

            if ("伝票" == tabName)
            {
                //伝票Tab内の伝票名を取得
                string denName = this.TabControl_Denpyou.SelectedTab.Text;

                if ("受付" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Uketsuke_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    //一覧で選択中の一行を取得
                    string UketsukeKubun = this.Uketsuke_Denpyou.CurrentRow.Cells[0].Value.ToString();//受付区分
                    copyNo = this.Uketsuke_Denpyou.CurrentRow.Cells[4].Value.ToString();//受付番号

                    if ("収集" == UketsukeKubun)
                    {
                        //G015	収集受付入力
                        FormManager.OpenFormWithAuth("G015", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                    }
                    else if ("出荷" == UketsukeKubun)
                    {
                        //G016	出荷受付入力
                        FormManager.OpenFormWithAuth("G016", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                    }
                    else if ("持込" == UketsukeKubun)
                    {
                        //G018	持込受付入力
                        FormManager.OpenFormWithAuth("G018", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                    }
                }
                else if ("受付（クレーム）" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Uketsuke_Kuremu_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    //一覧で選択中の一行を取得
                    copyNo = this.Uketsuke_Kuremu_Denpyou.CurrentRow.Cells[1].Value.ToString();//受付番号
                    FormManager.OpenFormWithAuth("G020", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                }
                else if ("受入" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Ukeire_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    copyNo = this.Ukeire_Denpyou.CurrentRow.Cells[1].Value.ToString();//受入番号
                    if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        FormManager.OpenFormWithAuth("G051", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                    }
                    else
                    {
                        FormManager.OpenFormWithAuth("G721", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                    }
                }
                else if ("出荷" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Shukka_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    copyNo = this.Shukka_Denpyou.CurrentRow.Cells[1].Value.ToString();//出荷番号
                    if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        FormManager.OpenFormWithAuth("G053", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                    }
                    else
                    {
                        FormManager.OpenFormWithAuth("G722", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                    }
                }
                else if ("売上/支払" == denName)
                {
                    //一覧で選択していない場合
                    if (this.UriageShiharai_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    copyNo = this.UriageShiharai_Denpyou.CurrentRow.Cells[0].Value.ToString();//売上/支払番号
                    FormManager.OpenFormWithAuth("G054", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                }
                else if ("入金" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Nyuukin_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    copyNo = this.Nyuukin_Denpyou.CurrentRow.Cells[9].Value.ToString();//入金番号

                    T_NYUUKIN_ENTRY entry = new T_NYUUKIN_ENTRY();
                    entry.NYUUKIN_NUMBER = Convert.ToInt64(copyNo);
                    entry.DELETE_FLG = false;
                    entry = this.logic.NyuukinDao.GetDataForEntity(entry).FirstOrDefault();
                    if (entry != null && entry.TOK_INPUT_KBN.IsFalse)
                    {
                        FormManager.OpenFormWithAuth("G619", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                    }
                    else
                    {
                        FormManager.OpenFormWithAuth("G459", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                    }
                }
                else if ("出金" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Shukkin_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    copyNo = this.Shukkin_Denpyou.CurrentRow.Cells[5].Value.ToString();//出金番号
                    FormManager.OpenFormWithAuth("G090", WINDOW_TYPE.NEW_WINDOW_FLAG, copyNo);//複写モードで起動（新規モード）
                }
            }

            #endregion

            #region 複写モードで画面を開く(取引先マスタ)

            if ("取引先マスタ" == tabName)
            {
                //取引先が入力されていない場合
                if (this.TORIHIKISAKI_CD.Text == "")
                {
                    return;
                }
                FormManager.OpenFormWithAuth("M213", WINDOW_TYPE.NEW_WINDOW_FLAG, TORIHIKISAKI_CD.Text);//複写モードで起動（新規モード）
            }

            #endregion

            #region 複写モードで画面を開く(業者マスタ)

            if ("業者マスタ" == tabName)
            {
                //業者が入力されていない場合
                if (this.GYOUSHA_CD.Text == "")
                {
                    return;
                }
                FormManager.OpenFormWithAuth("M215", WINDOW_TYPE.NEW_WINDOW_FLAG, GYOUSHA_CD.Text);//複写モードで起動（新規モード）
            }

            #endregion

            #region 複写モードで画面を開く(現場マスタ)

            if ("現場マスタ" == tabName)
            {
                //業者が入力されていない場合
                if (this.GYOUSHA_CD.Text == "")
                {
                    return;
                }
                //現場が入力されていない場合
                if (this.GENBA_CD.Text == "")
                {
                    return;
                }
                FormManager.OpenFormWithAuth("M217", WINDOW_TYPE.NEW_WINDOW_FLAG, GYOUSHA_CD.Text, GENBA_CD.Text);//複写モードで起動（新規モード）
            }

            #endregion

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// データ移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void MoveData_Click(object sender, EventArgs e)
        {
            try
            {
                // 拠点取得
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                var kyotenCd = this.GetUserProfileValue(userProfile, "拠点CD");

                // 取引先拠点チェック
                var chkTK = this.logic.CheckTorihikisakiKyoten(kyotenCd, this.TORIHIKISAKI_CD.Text);
                if (!chkTK && string.Empty != this.TORIHIKISAKI_CD.Text)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShowWarn("選択されている取引先の拠点が異なるためデータ移動できません。");
                    return;
                }

                // 業者拠点チェック
                var chkGK = this.logic.CheckGyosyaKyoten(kyotenCd, this.TORIHIKISAKI_CD.Text, this.GYOUSHA_CD.Text);
                if (!chkGK && string.Empty != this.GYOUSHA_CD.Text)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShowWarn("選択されている業者の拠点が異なるためデータ移動できません。");
                    return;
                }

                int ukeireShukaGamenSizeKbn = this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE.IsNull ? 1 : (int)this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE;
                F01_G173MoveData moveData = new F01_G173MoveData(this.TORIHIKISAKI_CD.Text, this.GYOUSHA_CD.Text, this.GENBA_CD.Text, ukeireShukaGamenSizeKbn);
                moveData.ShowDialog();
                moveData.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// 現在選択しているタブ(tabControl1)のデータを取得
        /// </summary>
        private void GetTabControl1Data()
        {
            int tabindex = this.tabControl1.SelectedIndex;
            if (tabindex < 0) return;
            var page = this.tabControl1.TabPages[tabindex];
            //取引先CD
            string torihikisaki_cd = this.logic.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.logic.GyoushaCD;
            //現場CD
            string genba_cd = this.logic.GenbaCD;

            switch (page.Name)
            {
                case "TabPage_Denpyou":
                    //伝票
                    //this.UketsukeDate_Select();

                    this.GetTabControlDenpyouData();
                    break;

                case "tabPage_torihikisaki":
                    //取引先
                    if (null != torihikisaki_cd && !"".Equals(torihikisaki_cd) && !DBNull.Value.Equals(torihikisaki_cd))
                    {
                        //取引先マスタのヘッダ情報取得
                        this.logic.TorihikisakiHeaderSearch();
                        //取引先マスタの基本情報取得
                        this.logic.TorihikisakiKihonSearch();
                        //取引先請求情報取得
                        this.logic.TorihikisakiSeikyuuSearch();
                        //取引先支払情報取得
                        this.logic.TorihikisakiShiharaiSearch();
                        //取引先業者一覧取得
                        this.logic.TorihikisakiGyoushaIchiranSearch();
                    }

                    this.GetTabControlTorihikisakiData();
                    break;

                case "tabPage_gyousha":
                    //業者
                    if (null != gyousha_cd && !"".Equals(gyousha_cd) && !DBNull.Value.Equals(gyousha_cd))
                    {
                        //業者マスタのヘッダ情報、基本情報、請求情報、支払情報、取得
                        this.logic.GyoushaInfoSearch();
                        //業者現場一覧取得
                        this.logic.GyoushaGenbaIchiranSearch();
                    }

                    this.GetTabControlGyoushaData();
                    break;

                case "tabPage_genba":
                    //現場
                    if (null != genba_cd && !"".Equals(genba_cd) && !DBNull.Value.Equals(genba_cd))
                    {
                        //現場マスタの基本情報,請求情報、支払情報取得
                        this.logic.GenbaInfoSearch();
                    }

                    this.GetTabControlGenbaData();
                    break;

                case "tabPage_tanka":
                    //単価
                    //売上
                    int uriageResult = this.logic.KobetsuHinmeiTankaSearch(1);
                    //支払
                    int siharaiResult = this.logic.KobetsuHinmeiTankaSearch(2);

                    if (uriageResult == -1 && siharaiResult == -1)
                    {
                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    this.Tanka_Uriage_Ichiran.IsBrowsePurpose = false;
                    this.Tanka_Shiharai_Ichiran.IsBrowsePurpose = false;

                    //売上
                    var uriageTable = this.logic.KobetsuHinmeiTankaUriageSearchResult;
                    //支払
                    var siharaiTable = this.logic.KobetsuHinmeiTankaShiharaiSearchResult;

                    uriageTable.BeginLoadData();
                    siharaiTable.BeginLoadData();

                    this.Tanka_Uriage_Ichiran.DataSource = uriageTable;
                    this.Tanka_Shiharai_Ichiran.DataSource = siharaiTable;

                    this.Tanka_Uriage_Ichiran.IsBrowsePurpose = true;
                    this.Tanka_Shiharai_Ichiran.IsBrowsePurpose = true;

                    break;
            }
        }

        /// <summary>
        /// 現在選択しているタブ(TabControl_Denpyou)のデータを取得
        /// </summary>
        private void GetTabControlDenpyouData()
        {
            //タブIndex
            int tabindex = this.TabControl_Denpyou.SelectedIndex;
            if (tabindex < 0) return;
            var page = this.TabControl_Denpyou.TabPages[tabindex];

            switch (page.Name)
            {
                case "Denpyou_TabPage_Uketsuke":
                    //受付
                    this.Denpyou_TabPage_Uketsuke_Click();
                    break;
                case "Denpyou_TabPage_Uketsuke_Kuremu":
                    //受付クレーム
                    this.Denpyou_TabPage_Uketsuke_Kuremu_Click();
                    break;
                case "Denpyou_TabPage_Ukeire":
                    //受入
                    Denpyou_TabPage_Ukeire_Click();
                    break;
                case "Denpyou_TabPage_Shukka":
                    //出荷
                    Denpyou_TabPage_Shukka_Click();
                    break;
                case "Denpyou_TabPage_UriageShiharai":
                    //売上/支払
                    Denpyou_TabPage_UriageShiharai_Click();
                    break;
                case "Denpyou_TabPage_Dainou":
                    //代納
                    this.Denpyou_TabPage_Dainou_Click();
                    break;
                case "Denpyou_TabPage_Nyuukin":
                    //入金
                    Denpyou_TabPage_Nyuukin_Click();
                    break;
                case "Denpyou_TabPage_Shukkin":
                    //出金
                    Denpyou_TabPage_Shukkin_Click();
                    break;
                case "Denpyou_TabPage_ShukkaKenshuu":
                    //検収
                    Denpyou_TabPage_ShukkaKenshuu_Click();
                    break;
                case "Denpyou_TabPage_Contena":
                    //コンテナ
                    this.Denpyou_TabPage_Contena_Click();
                    break;
            }
        }

        /// <summary>
        /// 現在選択しているタブ(TabControl_Torihikisaki)のデータを取得
        /// </summary>
        private void GetTabControlTorihikisakiData()
        {
            //タブIndex
            int tabindex = this.TabControl_Torihikisaki.SelectedIndex;

            switch (tabindex)
            {
                case 0:
                    //ヘッダ
                    if (null != this.logic.TorihikisakiHeaderSearchResult && this.logic.TorihikisakiHeaderSearchResult.Rows.Count > 0)
                    {
                        this.logic.TorihikisakiHeaderHyouji();
                    }

                    this.TabControl_Torihikisaki.Focus();
                    break;

                case 1:
                    //基本情報
                    if (null != this.logic.TorihikisakiKihonSearchResult && this.logic.TorihikisakiKihonSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.TorihikisakiKihonHyouji();
                    }

                    this.TabControl_Torihikisaki.Focus();
                    break;

                case 2:
                    //請求情報1
                    if (null != this.logic.TorihikisakiSeikyuuSearchResult && this.logic.TorihikisakiSeikyuuSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.TorihikisakiSeikyuuHyouji();
                    }

                    this.TabControl_Torihikisaki.Focus();
                    break;

                case 3:
                    //請求情報2
                    if (null != this.logic.TorihikisakiSeikyuuSearchResult && this.logic.TorihikisakiSeikyuuSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.TorihikisakiSeikyuuHyouji2();
                    }

                    this.TabControl_Torihikisaki.Focus();
                    break;

                case 4:
                    //支払情報1
                    if (null != this.logic.TorihikisakiShiharaiSearchResult && this.logic.TorihikisakiShiharaiSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.TorihikisakiShiharaiHyouji();
                    }

                    this.TabControl_Torihikisaki.Focus();
                    break;

                case 5:
                    //支払情報2
                    if (null != this.logic.TorihikisakiShiharaiSearchResult && this.logic.TorihikisakiShiharaiSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.TorihikisakiShiharaiHyouji2();
                    }

                    this.TabControl_Torihikisaki.Focus();
                    break;

                case 6:
                    //取引先分類
                    if (null != this.logic.TorihikisakiKihonSearchResult && this.logic.TorihikisakiKihonSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.TorihikisakiBunruiHyouji();
                    }

                    this.TabControl_Torihikisaki.Focus();
                    break;

                case 7:
                    //業者一覧
                    this.logic.TorihikisakiGyoushaIchiranHyouji();

                    break;
            }
        }

        /// <summary>
        /// 現在選択しているタブ(TabControl_Gyousha)のデータを取得
        /// </summary>
        private void GetTabControlGyoushaData()
        {
            //タブIndex
            int tabindex = this.TabControl_Gyousha.SelectedIndex;

            switch (tabindex)
            {
                case 0:
                    //ヘッダ情報
                    if (null != this.logic.GyoushaInfoSearchResult && this.logic.GyoushaInfoSearchResult.Rows.Count > 0)
                    {
                        this.logic.GyoushaHeaderHyouji();
                    }

                    this.TabControl_Gyousha.Focus();
                    break;

                case 1:
                    //基本情報
                    if (null != this.logic.GyoushaInfoSearchResult && this.logic.GyoushaInfoSearchResult.Rows.Count > 0)
                    {
                        this.logic.GyoushaKihonHyouji();
                    }

                    this.TabControl_Gyousha.Focus();
                    break;

                case 2:
                    //請求情報
                    if (null != this.logic.GyoushaInfoSearchResult && this.logic.GyoushaInfoSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.GyoushaSeikyuuHyouji();
                    }

                    this.TabControl_Gyousha.Focus();
                    break;

                case 3:
                    //支払情報
                    if (null != this.logic.GyoushaInfoSearchResult && this.logic.GyoushaInfoSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.GyoushaShiharaiHyouji();
                    }

                    this.TabControl_Gyousha.Focus();
                    break;

                case 4:
                    //現場一覧
                    this.logic.GyoushaGenbaIchiranHyouji();

                    break;

                case 5:
                    //分類情報
                    if (null != this.logic.GyoushaInfoSearchResult && this.logic.GyoushaInfoSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.GyoushaBunruiHyouji();
                    }

                    this.TabControl_Gyousha.Focus();
                    break;

                case 6:
                    //委託契約(業者)情報
                    this.GyoushaItakuKeiyaku_Select();

                    this.TabControl_Gyousha.Focus();
                    break;
            }
        }

        /// <summary>
        /// 現在選択しているタブ(TabControl_Genba)のデータを取得
        /// </summary>
        private void GetTabControlGenbaData()
        {
            //タブIndex
            int tabindex = this.TabControl_Genba.SelectedIndex;

            switch (tabindex)
            {
                case 0:
                    //ヘッダ情報
                    if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                    {
                        this.logic.GenbaHeaderHyouji();
                    }

                    this.TabControl_Genba.Focus();
                    break;

                case 1:
                    //基本情報
                    if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                    {
                        this.logic.GenbaKihonHyouji();
                    }

                    this.TabControl_Genba.Focus();
                    break;

                case 2:
                    //請求情報
                    if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.GenbaSeikyuuHyouji();
                    }

                    this.TabControl_Genba.Focus();
                    break;

                case 3:
                    //支払情報
                    if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.GenbaShiharaiHyouji();
                    }

                    this.TabControl_Genba.Focus();
                    break;

                case 4:
                    //分類情報
                    if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                    {
                        //表示
                        this.logic.GenbaBunruiHyouji();
                    }

                    this.TabControl_Genba.Focus();
                    break;

                case 5:
                    //委託契約(現場)情報
                    this.ItakuKeiyakuUseKbn.Text = string.Empty;
                    if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                    {
                        //委託契約区分
                        if (!this.logic.GenbaInfoSearchResult.Rows[0].IsNull("ITAKU_KEIYAKU_USE_KBN"))
                        {
                            this.ItakuKeiyakuUseKbn.Text = this.logic.GenbaInfoSearchResult.Rows[0].Field<Int16>("ITAKU_KEIYAKU_USE_KBN").ToString();
                        }
                    }
                    this.GenbaItakuKeiyaku_Select();

                    this.TabControl_Genba.Focus();
                    break;

                case 7:
                    if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                    {
                        //定期回収情報
                        this.logic.GenbaTeikiHinmei_Select();
                    }

                    this.TabControl_Genba.Focus();
                    break;

                case 6:
                    if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                    {
                        //月極情報
                        this.logic.GenbaTsukiHinmei_Select();
                    }

                    this.TabControl_Genba.Focus();
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                    {
                        // A票～E票
                        this.logic.SetAToEWindowsData();
                    }
                    break;
            }
        }

        /// <summary>
        /// 取引先CDテキストボックスのフォーカスが失い時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ゼロ埋め処理
                if (null != this.TORIHIKISAKI_CD.Text && !"".Equals(this.TORIHIKISAKI_CD.Text))
                {
                    this.TORIHIKISAKI_CD.Text = this.TORIHIKISAKI_CD.Text.PadLeft(6, '0');
                }

                //検索
                this.TorihikisakiFORTorihikisakiCd_Select();

                //取引先CDテキストボックスの内容を記憶,比較用
                this.maeTorihikisakiCd = this.TORIHIKISAKI_CD.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDにより取引先情報を取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TorihikisakiFORTorihikisakiCd_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                string torihikisaki_cd = this.TORIHIKISAKI_CD.Text;

                if (null == torihikisaki_cd || "".Equals(torihikisaki_cd) || DBNull.Value.Equals(torihikisaki_cd))
                {
                    //取引先、業者、現場が空にする
                    this.logic.TorihikisakiCrear();
                }
                else
                {
                    //取引先マスタ検索
                    if (this.logic.TorihikisakiSearch() == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E020", "取引先");
                        //業者、現場を空欄にする
                        this.logic.TorihikisakiCrear();
                        //フォーカス
                        this.TORIHIKISAKI_CD.Focus();

                        LogUtility.DebugMethodEnd();
                        return;
                    }
                    //表示
                    this.logic.TorihikisakiSet();

                    ////フッタの[F2取引先],[F3業者],[F4現場]を利用可にする
                    //this.logic.F2F3F4_Enabled();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CD_PopupAfterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteTorihikisakiCd(object sender, DialogResult rlt)
        {
            LogUtility.DebugMethodStart(sender, rlt);
            try
            {
                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //取引先マスタ検索
                if (this.logic.TorihikisakiSearch() == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "取引先");
                    //業者、現場を空欄にする
                    this.logic.TorihikisakiCrear();
                    //フォーカス
                    this.TORIHIKISAKI_CD.Focus();

                    LogUtility.DebugMethodEnd();
                    return;
                }
                //表示
                this.logic.TorihikisakiSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDテキストボックスのフォーカスが失い時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ゼロ埋め処理
                if (null != this.GYOUSHA_CD.Text && !"".Equals(this.GYOUSHA_CD.Text))
                {
                    this.GYOUSHA_CD.Text = this.GYOUSHA_CD.Text.PadLeft(6, '0');
                }
                //検索
                this.GyoushaFORGyoushaCD_Select();

                //業者CDテキストボックスの内容を記憶,比較用
                this.maeGyoushaCd = this.GYOUSHA_CD.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDにより業者情報を取得
        /// </summary>
        public void GyoushaFORGyoushaCD_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                //string torihikisaki_cd = this.TORIHIKISAKI_CD.Text;
                //業者CD
                string gyousha_cd = this.GYOUSHA_CD.Text;

                if (null == gyousha_cd || "".Equals(gyousha_cd) || DBNull.Value.Equals(gyousha_cd))
                {
                    //業者、現場が空にする
                    this.logic.GyoushaClear();
                    this.logic.GenbaClear();
                }
                else
                {
                    //業者マスタ検索
                    if (this.logic.GyoushaSearch() == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E020", "業者");
                        //業者、現場を空欄にする
                        this.logic.GyoushaClear();
                        this.logic.GenbaClear();
                        //フォーカス
                        this.GYOUSHA_CD.Focus();

                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    //表示
                    this.logic.GyoushaSet();

                    ////業者が変わったら、現場のクリア
                    if (!this.maeGyoushaCd.Equals(this.GYOUSHA_CD.Text))
                    {
                        this.logic.GenbaClear();
                    }

                    ////フッタの[F2取引先],[F3業者],[F4現場]を利用可にする
                    //this.logic.F2F3F4_Enabled();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CD_PopupAfterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteGyoushaCd(object sender, DialogResult rlt)
        {
            LogUtility.DebugMethodStart(sender, rlt);
            try
            {
                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //業者マスタ検索
                if (this.logic.GyoushaSearch() == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "業者");
                    //業者、現場を空欄にする
                    this.logic.GyoushaClear();
                    this.logic.GenbaClear();
                    //フォーカス
                    this.GYOUSHA_CD.Focus();

                    LogUtility.DebugMethodEnd();
                    return;
                }

                //表示
                this.logic.GyoushaSet();

                ////業者が変わったら、現場のクリア
                if (!this.maeGyoushaCd.Equals(this.GYOUSHA_CD.Text))
                {
                    this.logic.GenbaClear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDテキストボックスのフォーカスが失い時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ゼロ埋め処理
                if (null != this.GENBA_CD.Text && !"".Equals(this.GENBA_CD.Text))
                {
                    this.GENBA_CD.Text = this.GENBA_CD.Text.PadLeft(6, '0').ToUpper();
                }

                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) STR
                if (string.IsNullOrEmpty(this.GENBA_CD.Text))
                {
                    this.logic.GenbaClear();
                    return;
                }
                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E051", "業者");
                    this.GENBA_CD.Text = string.Empty;
                    this.GENBA_CD.Focus();
                    return;
                }
                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) END
                //検索

                this.GenbaFORGenbaCD_Select();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDにより現場情報を取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GenbaFORGenbaCD_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                //string torihikisaki_cd = this.TORIHIKISAKI_CD.Text;
                //業者CD
                //string gyousha_cd = this.GYOUSHA_CD.Text;
                //現場CD
                string genba_cd = this.GENBA_CD.Text;
                //if (null != gyousha_cd && !"".Equals(gyousha_cd) && !DBNull.Value.Equals(gyousha_cd))
                //{
                if (null == genba_cd || "".Equals(genba_cd) || DBNull.Value.Equals(genba_cd))
                {
                    //現場が空にする
                    this.logic.GenbaClear();
                }
                else
                {
                    //現場マスタ検索
                    if (this.logic.GenbaSearch() == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E020", "現場");
                        //業者、現場を空欄にする
                        this.logic.GenbaClear();
                        //フォーカス
                        this.GENBA_CD.Focus();

                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    //表示
                    this.logic.GenbaSet();

                    ////フッタの[F2取引先],[F3業者],[F4現場]を利用可にする
                    //this.logic.F2F3F4_Enabled();
                }
                ///}
                this.maeGyoushaCd = this.GYOUSHA_CD.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CD_PopupAfterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteGenbaCd(object sender, DialogResult rlt)
        {
            LogUtility.DebugMethodStart(sender, rlt);
            try
            {
                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //現場マスタ検索
                if (this.logic.GenbaSearch() == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "現場");
                    //業者、現場を空欄にする
                    this.logic.GenbaClear();
                    //フォーカス
                    this.GENBA_CD.Focus();

                    LogUtility.DebugMethodEnd();
                    return;
                }

                //表示
                this.logic.GenbaSet();

                this.maeGyoushaCd = this.GYOUSHA_CD.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受付タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Uketsuke_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //受付情報取得
                    if (this.UketsukeDate_Select() > 0)
                    {
                        //受付明細の初期表示
                        //受付区分
                        string uketsukeKbn = this.logic.UketsukeSearchResult.Rows[0].Field<string>("UKETSUKE_KBN");
                        //システムID
                        long systemId = this.logic.UketsukeSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.logic.UketsukeSearchResult.Rows[0].Field<int>("SEQ");
                        //受付明細
                        this.UketsukeDetailDate_Select(uketsukeKbn, systemId, seq);
                    }
                }
                //タブ活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Uketsuke;
                ////20211228 Thanh 158919 s
                //this.logic.adjustColumnSize(this.Uketsuke_Denpyou);
                //this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_SAGYOU_DATE"].Width = 110;
                //this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_UKETUKE_DATE"].Width = 110;
                ////20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受付タブ情報を取得
        /// </summary>
        private int UketsukeDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.logic.UketsukeFlg)
                //{
                //DB検索 :受付
                this.Uketsuke_Denpyou.IsBrowsePurpose = false;
                if (this.logic.UketsukeSearch() == 0)
                {
                    //var messageShowLogic = new MessageBoxShowLogic();
                    //messageShowLogic.MessageBoxShow("E076");
                    this.Uketsuke_Denpyou.DataSource = this.logic.UketsukeSearchResult;

                    this.Uketsuke_Denpyou.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //}
                var table = this.logic.UketsukeSearchResult;
                //バインド
                table.BeginLoadData();
                //行の編集可
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }
                this.Uketsuke_Denpyou.DataSource = null;
                this.Uketsuke_Denpyou.DataSource = table;
                this.Uketsuke_Denpyou.IsBrowsePurpose = true;
                ////20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Uketsuke_Denpyou);
                this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_UKETSUKE_KBN"].Width = this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_UKETSUKE_KBN"].Width + 10;
                this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_SAGYOU_DATE"].Width = 110;
                this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_UKETUKE_DATE"].Width = 110;
                this.logic.adjustColumnSize(this.Uketsuke_Meisai);
                ////20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
            return 1;
        }

        /// <summary>
        /// 受付明細情報を取得
        /// </summary>
        /// <param name="uketsukeKbn">受付区分</param>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int UketsukeDetailDate_Select(string uketsukeKbn, long systemId, int seq)
        {
            LogUtility.DebugMethodStart(uketsukeKbn, systemId, seq);
            try
            {
                DataTable table;

                //DB検索 :受付明細
                this.Uketsuke_Meisai.IsBrowsePurpose = false;
                switch (uketsukeKbn)
                {
                    case "収集":
                        //受付(収集)明細
                        if (this.logic.UketsukeSSDetailSearch(systemId, seq) == 0)
                        {
                            this.Uketsuke_Meisai.DataSource = this.logic.UketsukeSSDetailSearchResult;

                            this.Uketsuke_Meisai.IsBrowsePurpose = true;
                            LogUtility.DebugMethodEnd(0);
                            return 0;
                        }
                        //バインド
                        table = this.logic.UketsukeSSDetailSearchResult;
                        table.BeginLoadData();
                        this.Uketsuke_Meisai.DataSource = table;

                        this.Uketsuke_Meisai.IsBrowsePurpose = true;
                        break;

                    case "出荷":
                        //受付(出荷)明細
                        if (this.logic.UketsukeSKDetailSearch(systemId, seq) == 0)
                        {
                            this.Uketsuke_Meisai.DataSource = this.logic.UketsukeSKDetailSearchResult;

                            this.Uketsuke_Meisai.IsBrowsePurpose = true;
                            LogUtility.DebugMethodEnd(0);
                            return 0;
                        }
                        //バインド
                        table = this.logic.UketsukeSKDetailSearchResult;
                        table.BeginLoadData();
                        this.Uketsuke_Meisai.DataSource = table;

                        this.Uketsuke_Meisai.IsBrowsePurpose = true;
                        break;

                    case "持込":
                        //受付(持込)明細
                        if (this.logic.UketsukeMKDetailSearch(systemId, seq) == 0)
                        {
                            this.Uketsuke_Meisai.DataSource = this.logic.UketsukeMKDetailSearchResult;

                            this.Uketsuke_Meisai.IsBrowsePurpose = true;
                            LogUtility.DebugMethodEnd(0);
                            return 0;
                        }
                        //バインド
                        table = this.logic.UketsukeMKDetailSearchResult;
                        table.BeginLoadData();
                        this.Uketsuke_Meisai.DataSource = table;

                        this.Uketsuke_Meisai.IsBrowsePurpose = true;
                        break;
                }

                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Uketsuke_Meisai);
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();

            return 1;
        }

        /// <summary>
        /// 受付クレームタブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Uketsuke_Kuremu_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //受付クレーム情報取得
                    if (this.UketsukeCMDate_Select() > 0)
                    {
                        //内容の初期表示
                        this.Uketsuke_Kuremu_Denpyou_NaiyouHyouji(0);
                    }
                }

                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Uketsuke_Kuremu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受付クレームタブー情報を取得
        /// </summary>
        private int UketsukeCMDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.logic.UketsukeCMFlg)
                //{
                //DB検索 :受付(クレーム）
                this.Uketsuke_Kuremu_Denpyou.IsBrowsePurpose = false;
                if (this.logic.UketsukeCMSearch() == 0)
                {
                    this.Uketsuke_Kuremu_Denpyou.DataSource = this.logic.UketsukeCMSearchResult;

                    this.Uketsuke_Kuremu_Denpyou.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //}
                //バインド
                var table = this.logic.UketsukeCMSearchResult;
                table.BeginLoadData();
                this.Uketsuke_Kuremu_Denpyou.DataSource = table;
                this.Uketsuke_Kuremu_Denpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Uketsuke_Kuremu_Denpyou);
                this.Uketsuke_Kuremu_Denpyou.Columns["UKETUKE_KUREMU_DENPYOU_UKETUKE_DATE"].Width = 110;
                this.Uketsuke_Kuremu_Denpyou.Columns["UKETUKE_KUREMU_DENPYOU_TAIOKANRYOU_DATE"].Width = 110;
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 受入タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Ukeire_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //受入情報取得
                    if (this.UkeireDate_Select() > 0)
                    {
                        //受入明細の初期表示
                        //システムID
                        long systemId = this.logic.UkeireSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.logic.UkeireSearchResult.Rows[0].Field<int>("SEQ");
                        //受入明細
                        this.UkeireDetailDate_Select(systemId, seq);
                    }
                }
                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Ukeire;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受入タブー情報を取得
        /// </summary>
        private int UkeireDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                this.Ukeire_Denpyou.IsBrowsePurpose = false;
                if (!this.logic.UkeireFlg)
                {
                    //DB検索 :受入
                    if (this.logic.UkeireSearch() == 0)
                    {
                        this.Ukeire_Denpyou.DataSource = this.logic.UkeireSearchResult;

                        this.Ukeire_Denpyou.IsBrowsePurpose = true;
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                //バインド
                var table = this.logic.UkeireSearchResult;
                table.BeginLoadData();
                this.Ukeire_Denpyou.DataSource = table;
                this.Ukeire_Denpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Ukeire_Denpyou);
                this.Ukeire_Denpyou.Columns["UKEIRE_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 受入明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int UkeireDetailDate_Select(long systemId, int seq)
        {
            //LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :受入明細
                this.MultiRow_UkeireMeisai.IsBrowsePurpose = false;
                if (this.logic.UkeireDetailSearch(systemId, seq) == 0)
                {
                    this.MultiRow_UkeireMeisai.DataSource = this.logic.UkeireDetailSearchResult;

                    this.MultiRow_UkeireMeisai.IsBrowsePurpose = true;
                    return 0;
                }
                //バインド
                var table = this.logic.UkeireDetailSearchResult;
                table.BeginLoadData();
                this.MultiRow_UkeireMeisai.DataSource = table;
                this.MultiRow_UkeireMeisai.IsBrowsePurpose = true;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出荷タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Shukka_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //出荷情報取得
                    if (this.ShukkaDate_Select() > 0)
                    {
                        //出荷明細の初期表示
                        //システムID
                        long systemId = this.logic.ShukkaSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.logic.ShukkaSearchResult.Rows[0].Field<int>("SEQ");
                        //出荷明細
                        this.ShukkaDetailDate_Select(systemId, seq);
                    }
                }
                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Shukka;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷タブー情報を取得
        /// </summary>
        private int ShukkaDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.logic.ShukkaFlg)
                //{
                //DB検索 :出荷
                this.Shukka_Denpyou.IsBrowsePurpose = false;
                if (this.logic.ShukkaSearch() == 0)
                {
                    this.Shukka_Denpyou.DataSource = this.logic.ShukkaSearchResult;

                    this.Shukka_Denpyou.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //}
                //バインド
                var table = this.logic.ShukkaSearchResult;
                table.BeginLoadData();
                this.Shukka_Denpyou.DataSource = table;
                this.Shukka_Denpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Shukka_Denpyou);
                this.Shukka_Denpyou.Columns["SHUKKA_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出荷明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int ShukkaDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :受入明細
                this.MultiRow_ShukkaMeisai.IsBrowsePurpose = false;
                if (this.logic.ShukkaDetailSearch(systemId, seq) == 0)
                {
                    this.MultiRow_ShukkaMeisai.DataSource = this.logic.ShukkaDetailSearchResult;

                    this.MultiRow_ShukkaMeisai.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.ShukkaDetailSearchResult;
                table.BeginLoadData();
                this.MultiRow_ShukkaMeisai.DataSource = table;
                this.MultiRow_ShukkaMeisai.IsBrowsePurpose = true;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 売上/支払タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_UriageShiharai_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //売上/支払情報取得
                    if (this.UrShDate_Select() > 0)
                    {
                        //売上/支払明細の初期表示
                        //システムID
                        long systemId = this.logic.UrShSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.logic.UrShSearchResult.Rows[0].Field<int>("SEQ");
                        //売上/支払明細
                        this.UrShDetailDate_Select(systemId, seq);
                    }
                }
                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_UriageShiharai;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 売上/支払タブー情報を取得
        /// </summary>
        private int UrShDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                this.UriageShiharai_Denpyou.IsBrowsePurpose = false;
                if (!this.logic.UrShFlg)
                {
                    //DB検索 :売上/支払
                    if (this.logic.UrShSearch() == 0)
                    {
                        this.UriageShiharai_Denpyou.DataSource = this.logic.UrShSearchResult;

                        this.UriageShiharai_Denpyou.IsBrowsePurpose = true;
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                //バインド
                var table = this.logic.UrShSearchResult;
                table.BeginLoadData();
                this.UriageShiharai_Denpyou.DataSource = table;
                this.UriageShiharai_Denpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.UriageShiharai_Denpyou);
                this.UriageShiharai_Denpyou.Columns["URIAGESIHARAI_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.logic.adjustColumnSize(this.UriageShiharai_Meisai);
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 売上/支払明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int UrShDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :売上/支払明細
                this.UriageShiharai_Meisai.IsBrowsePurpose = false;
                if (this.logic.UrShDetailSearch(systemId, seq) == 0)
                {
                    this.UriageShiharai_Meisai.DataSource = this.logic.UrShDetailSearchResult;

                    this.UriageShiharai_Meisai.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.UrShDetailSearchResult;
                table.BeginLoadData();
                this.UriageShiharai_Meisai.DataSource = table;
                this.UriageShiharai_Meisai.IsBrowsePurpose = true;
                this.logic.adjustColumnSize(this.UriageShiharai_Meisai);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 代納
        /// </summary>
        internal void Denpyou_TabPage_Dainou_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    if (this.DainouDate_Select() > 0)
                    {
                        //支払
                        //システムID
                        long systemIdUkeire = !DBNull.Value.Equals(this.logic.DainouSearchResult.Rows[0].ItemArray[10]) ? this.logic.DainouSearchResult.Rows[0].Field<long>("UKEIRE_SYSTEM_ID") : 0;
                        //SEQ
                        int seqUkeire = !DBNull.Value.Equals(this.logic.DainouSearchResult.Rows[0].ItemArray[11]) ? this.logic.DainouSearchResult.Rows[0].Field<int>("UKEIRE_SEQ") : 0;
                        //システムID
                        //売上
                        long systemIdShukka = !DBNull.Value.Equals(this.logic.DainouSearchResult.Rows[0].ItemArray[27]) ? this.logic.DainouSearchResult.Rows[0].Field<long>("SHUKKA_SYSTEM_ID") : 0;
                        //SEQ
                        int seqShukka = !DBNull.Value.Equals(this.logic.DainouSearchResult.Rows[0].ItemArray[28]) ? this.logic.DainouSearchResult.Rows[0].Field<int>("SHUKKA_SEQ") : 0;
                        //売上/支払明細
                        this.DainouDetailDate_Select(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka);
                    }
                }
                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Dainou;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 代納
        /// </summary>
        private int DainouDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                this.MultiRow_DaiNouDenpyou.IsBrowsePurpose = false;
                if (!this.logic.DainouFlg)
                {
                    //DB検索 :売上/支払
                    if (this.logic.DainouSearch() == 0)
                    {
                        this.MultiRow_DaiNouDenpyou.DataSource = this.logic.DainouSearchResult;
                        this.MultiRow_DaiNouMeissai.DataSource = this.logic.DainouDetailSearchResult;

                        this.MultiRow_DaiNouDenpyou.IsBrowsePurpose = true;
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                //バインド
                var table = this.logic.DainouSearchResult;
                table.BeginLoadData();
                this.MultiRow_DaiNouDenpyou.DataSource = table;
                this.MultiRow_DaiNouDenpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.MultiRow_DaiNouDenpyou.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_DaiNouDenpyou.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                this.MultiRow_DaiNouMeissai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_DaiNouMeissai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 代納明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int DainouDetailDate_Select(long systemIdUkeire, int seqUkeire, long systemIdShukka, int seqShukka)
        {
            LogUtility.DebugMethodStart(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka);
            try
            {
                //DB検索 :売上/支払明細
                this.MultiRow_DaiNouMeissai.IsBrowsePurpose = false;
                if (this.logic.DainouDetailSearch(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka) == 0)
                {
                    this.MultiRow_DaiNouMeissai.DataSource = this.logic.DainouDetailSearchResult;

                    this.MultiRow_DaiNouMeissai.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.DainouDetailSearchResult;
                table.BeginLoadData();
                this.MultiRow_DaiNouMeissai.DataSource = table;
                this.MultiRow_DaiNouMeissai.IsBrowsePurpose = true;
                this.MultiRow_DaiNouMeissai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_DaiNouMeissai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 入金タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Nyuukin_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //入金情報取得
                    if (this.NyuukinDate_Select() > 0)
                    {
                        //入金明細の初期表示
                        //システムID
                        long systemId = this.logic.NyuukinSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.logic.NyuukinSearchResult.Rows[0].Field<int>("SEQ");
                        //入金明細
                        this.NyuukinDetailDate_Select(systemId, seq);
                    }
                }
                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Nyuukin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金タブー情報を取得
        /// </summary>
        private int NyuukinDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.logic.NyuukinFlg)
                //{
                //DB検索 :入金
                this.Nyuukin_Denpyou.IsBrowsePurpose = false;
                if (this.logic.NyuukinSearch() == 0)
                {
                    this.Nyuukin_Denpyou.DataSource = this.logic.NyuukinSearchResult;

                    this.Nyuukin_Denpyou.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //}
                //バインド
                var table = this.logic.NyuukinSearchResult;
                table.BeginLoadData();
                this.Nyuukin_Denpyou.DataSource = table;
                this.Nyuukin_Denpyou.IsBrowsePurpose = true;

                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Nyuukin_Denpyou);
                this.Nyuukin_Denpyou.Columns["NYUKIN_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.logic.adjustColumnSize(this.Nyuukin_Meisai);
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 入金明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int NyuukinDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :入金明細
                this.Nyuukin_Meisai.IsBrowsePurpose = false;
                if (this.logic.NyuukinDetailSearch(systemId, seq) == 0)
                {
                    this.Nyuukin_Meisai.DataSource = this.logic.NyuukinDetailSearchResult;

                    this.Nyuukin_Meisai.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.NyuukinDetailSearchResult;
                table.BeginLoadData();
                this.Nyuukin_Meisai.DataSource = table;
                this.Nyuukin_Meisai.IsBrowsePurpose = true;

                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Nyuukin_Meisai);
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出金タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Shukkin_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //出金情報取得
                    if (this.ShukkinDate_Select() > 0)
                    {
                        //出金明細の初期表示
                        //システムID
                        long systemId = this.logic.ShukkinSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.logic.ShukkinSearchResult.Rows[0].Field<int>("SEQ");
                        //出金明細
                        this.ShukkinDetailDate_Select(systemId, seq);
                    }
                }
                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Shukkin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出金タブー情報を取得
        /// </summary>
        private int ShukkinDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.logic.ShukkinFlg)
                //{
                //DB検索 :出金
                this.Shukkin_Denpyou.IsBrowsePurpose = false;
                if (this.logic.ShukkinSearch() == 0)
                {
                    this.Shukkin_Denpyou.DataSource = this.logic.ShukkinSearchResult;

                    this.Shukkin_Denpyou.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //}
                //バインド
                var table = this.logic.ShukkinSearchResult;
                table.BeginLoadData();
                this.Shukkin_Denpyou.DataSource = table;
                this.Shukkin_Denpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Shukkin_Denpyou);
                this.Shukkin_Denpyou.Columns["SHUKIN_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.logic.adjustColumnSize(this.Shukkin_Meisai);
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出金明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int ShukkinDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :入金明細
                this.Shukkin_Meisai.IsBrowsePurpose = false;
                if (this.logic.ShukkinDetailSearch(systemId, seq) == 0)
                {
                    this.Shukkin_Meisai.DataSource = this.logic.ShukkinDetailSearchResult;

                    this.Shukkin_Meisai.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.ShukkinDetailSearchResult;
                table.BeginLoadData();
                this.Shukkin_Meisai.DataSource = table;
                this.Shukkin_Meisai.IsBrowsePurpose = true;

                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Shukkin_Meisai);
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 検収タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_ShukkaKenshuu_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //出荷情報取得
                    if (this.ShukkaKenshuu_Select() > 0)
                    {
                        //システムID
                        long systemId = this.logic.ShukkaKenshuuSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.logic.ShukkaKenshuuSearchResult.Rows[0].Field<int>("SEQ");
                        //出荷検収明細
                        this.ShukkaKenshuuDetail_Select(systemId, seq);
                    }
                }
                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_ShukkaKenshuu;
                ////20211228 Thanh 158919 s
                //this.logic.adjustColumnSize(this.ShukkaKenshuu_Denpyou);
                //this.ShukkaKenshuu_Denpyou.Columns["SHUKKA_KENSHUU_DENPYOU_DATE"].Width = 110;
                ////20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検収タブー情報を取得
        /// </summary>
        private int ShukkaKenshuu_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //DB検索 :出荷
                this.ShukkaKenshuu_Denpyou.IsBrowsePurpose = false;

                if (this.logic.ShukkaKenshuuDataSearch() == 0)
                {
                    this.ShukkaKenshuu_Denpyou.DataSource = this.logic.ShukkaKenshuuSearchResult;

                    this.ShukkaKenshuu_Denpyou.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.ShukkaKenshuuSearchResult;
                table.BeginLoadData();
                this.ShukkaKenshuu_Denpyou.DataSource = table;
                this.ShukkaKenshuu_Denpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.ShukkaKenshuu_Denpyou);
                this.ShukkaKenshuu_Denpyou.Columns["SHUKKA_KENSHUU_DENPYOU_DATE"].Width = 110;
                this.MultiRow_ShukkaKenshuu.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_ShukkaKenshuu.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出荷検収明細
        /// </summary>
        private int ShukkaKenshuuDetail_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                this.MultiRow_ShukkaKenshuu.IsBrowsePurpose = false;

                if (this.logic.ShukkaKenshuuDetailSearch(systemId, seq) == 0)
                {
                    this.MultiRow_ShukkaKenshuu.DataSource = this.logic.ShukkaKenshuuDetailSearchResult;

                    this.MultiRow_ShukkaMeisai.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }

                //バインド
                var table = this.logic.ShukkaKenshuuDetailSearchResult;
                table.BeginLoadData();
                this.MultiRow_ShukkaKenshuu.DataSource = table;
                this.MultiRow_ShukkaKenshuu.IsBrowsePurpose = true;
                this.MultiRow_ShukkaKenshuu.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_ShukkaKenshuu.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 委託契約(業者)情報を取得
        /// </summary>
        private int GyoushaItakuKeiyaku_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //業者CD、空欄の場合は検索せず
                if (null != this.logic.GyoushaCD && !"".Equals(this.logic.GyoushaCD) && !DBNull.Value.Equals(this.logic.GyoushaCD))
                {
                    //DB検索 :委託契約(業者)
                    this.Gyousha_ItakuKeiyaku_Ichiran.IsBrowsePurpose = false;
                    if (this.logic.ItakuKeiyakuSearch(1) == 0)
                    {
                        this.Gyousha_ItakuKeiyaku_Ichiran.DataSource = this.logic.GyoushaItakuKeiyakuSearchResult;

                        this.Gyousha_ItakuKeiyaku_Ichiran.IsBrowsePurpose = true;
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                    //バインド
                    var table = this.logic.GyoushaItakuKeiyakuSearchResult;
                    table.BeginLoadData();
                    //行の編集可
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                    }
                    this.Gyousha_ItakuKeiyaku_Ichiran.DataSource = table;
                    this.Gyousha_ItakuKeiyaku_Ichiran.IsBrowsePurpose = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 委託契約(現場)情報を取得
        /// </summary>
        private int GenbaItakuKeiyaku_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //業者CD、現場CD,いずれか空欄の場合は検索せず
                if ((null != this.logic.GyoushaCD && !"".Equals(this.logic.GyoushaCD) && !DBNull.Value.Equals(this.logic.GyoushaCD))
                    && (null != this.logic.GenbaCD && !"".Equals(this.logic.GenbaCD) && !DBNull.Value.Equals(this.logic.GenbaCD)))
                {
                    //DB検索 :委託契約(現場)
                    this.Genba_ItakuKeiyaku_Ichiran.IsBrowsePurpose = false;
                    if (this.logic.ItakuKeiyakuSearch(2) == 0)
                    {
                        this.Genba_ItakuKeiyaku_Ichiran.DataSource = this.logic.GenbaItakuKeiyakuSearchResult;

                        this.Genba_ItakuKeiyaku_Ichiran.IsBrowsePurpose = true;
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                    //バインド
                    var table = this.logic.GenbaItakuKeiyakuSearchResult;
                    table.BeginLoadData();
                    //行の編集可
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                    }
                    this.Genba_ItakuKeiyaku_Ichiran.DataSource = table;
                    this.Genba_ItakuKeiyaku_Ichiran.IsBrowsePurpose = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// コンテナ
        /// </summary>
        internal void Denpyou_TabPage_Contena_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.logic.ChangeLabelAndLayout();
                this.logic.setLoadPage();
                if (!this.JyoukenNullFlg)
                {
                    this.ContenaDate_Select();
                }
                //タブー活性化
                this.tabControl1.SelectedTab = this.TabPage_Denpyou;
                this.TabControl_Denpyou.SelectedTab = this.Denpyou_TabPage_Contena;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コンテナ
        /// </summary>
        private int ContenaDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                this.Contena_Denpyou.IsBrowsePurpose = false;
                if (!this.logic.ContenaFlg)
                {
                    //DB検索 :コンテナ
                    if (this.logic.ContenaSearch() == 0)
                    {
                        this.Contena_Denpyou.DataSource = this.logic.ContenaSearchResult;

                        this.Contena_Denpyou.IsBrowsePurpose = true;
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                //バインド
                var table = this.logic.ContenaSearchResult;
                table.BeginLoadData();
                this.Contena_Denpyou.DataSource = table;
                // グラフ（ｎ月迄）を一覧に設定する
                this.Contena_Denpyou.Columns["graph".ToUpper()].HeaderText = "グラフ（" + this.logic.SearchString.SYS_DAYS_COUNT + "日まで）";
                this.Contena_Denpyou.IsBrowsePurpose = true;
                this.logic.adjustColumnSize(this.Contena_Denpyou);
                if (this.Contena_Denpyou.Columns["SecchiChouhuku"].Visible)
                {
                    this.Contena_Denpyou.Columns["SecchiChouhuku"].Width = this.Contena_Denpyou.Columns["SecchiChouhuku"].Width + 5;
                }
                if (this.Contena_Denpyou.Columns["CONTENA_SHURUI_NAME_RYAKU"].Visible)
                {
                    this.Contena_Denpyou.Columns["CONTENA_SHURUI_NAME_RYAKU"].Width = this.Contena_Denpyou.Columns["CONTENA_SHURUI_NAME_RYAKU"].Width + 5;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// タブー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //タブーIndex
                int tabindex = e.TabPageIndex;
                var page = this.tabControl1.TabPages[tabindex];

                switch (page.Name)
                {
                    case "TabPage_Denpyou":
                        //伝票
                        Tab_Denpyou_Click(sender, e);
                        break;

                    case "tabPage_torihikisaki":
                        //取引先
                        Tab_TorihikisakiMaster_Click(sender, e);
                        break;

                    case "tabPage_gyousha":
                        //業者
                        Tab_GyoushaMaster_Click(sender, e);
                        break;

                    case "tabPage_genba":
                        //現場
                        Tab_GenbaMaster_Click(sender, e);
                        break;

                    case "tabPage_tanka":
                        //単価
                        Tab_Tanka_Click(sender, e);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票のtabControlタブー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Denpyou_Selected(object sender, TabControlEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //タブーIndex
                int tabindex = e.TabPageIndex;
                var page = this.TabControl_Denpyou.TabPages[tabindex];
                switch (page.Name)
                {
                    case "Denpyou_TabPage_Uketsuke":
                        //受付
                        this.Denpyou_TabPage_Uketsuke_Click();
                        //set denpyou date label text
                        this.logic.header.label3.Text = "作業日※";
                        break;
                    case "Denpyou_TabPage_Uketsuke_Kuremu":
                        //受付クレーム
                        this.Denpyou_TabPage_Uketsuke_Kuremu_Click();
                        //set denpyou date label text
                        this.logic.header.label3.Text = "受付日※";
                        break;
                    case "Denpyou_TabPage_Ukeire":
                        //受入
                        Denpyou_TabPage_Ukeire_Click();
                        //set denpyou date label text
                        this.logic.header.label3.Text = "伝票日付※";
                        break;
                    case "Denpyou_TabPage_Shukka":
                        //出荷
                        Denpyou_TabPage_Shukka_Click();
                        //set denpyou date label text
                        this.logic.header.label3.Text = "伝票日付※";
                        break;
                    case "Denpyou_TabPage_UriageShiharai":
                        //売上/支払
                        Denpyou_TabPage_UriageShiharai_Click();
                        //set denpyou date label text
                        this.logic.header.label3.Text = "伝票日付※";
                        break;
                    case "Denpyou_TabPage_Dainou":
                        //代納
                        this.Denpyou_TabPage_Dainou_Click();
                        this.logic.header.label3.Text = "伝票日付※";
                        break;
                    case "Denpyou_TabPage_Nyuukin":
                        //入金
                        Denpyou_TabPage_Nyuukin_Click();
                        //set denpyou date label text
                        this.logic.header.label3.Text = "伝票日付※";
                        break;
                    case "Denpyou_TabPage_Shukkin":
                        //出金
                        Denpyou_TabPage_Shukkin_Click();
                        //set denpyou date label text
                        this.logic.header.label3.Text = "伝票日付※";
                        break;
                    case "Denpyou_TabPage_ShukkaKenshuu":
                        //検収
                        Denpyou_TabPage_ShukkaKenshuu_Click();
                        this.logic.header.label3.Text = "伝票日付※";
                        break;
                    case "Denpyou_TabPage_Contena":
                        //コンテナ
                        this.logic.header.label3.Text = "最終更新日※";
                        this.Denpyou_TabPage_Contena_Click();
                        break;
                }
                ////20211228 Thanh 158919 s
                //this.logic.adjustColumnSize(this.Ukeire_Denpyou);
                //this.Ukeire_Denpyou.Columns["UKEIRE_DENPYOU_DENPYOU_DATE"].Width = 110;
                ////20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先のtabControlタブー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Torihikisaki_Selected(object sender, TabControlEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //取引先CD
                string torihikisaki_cd = this.logic.TorihikisakiCD;
                //タブーIndex
                int tabindex = e.TabPageIndex;
                switch (tabindex)
                {
                    case 0:
                        //ヘッダ情報
                        Tab_TorihikisakiMaster_Click(sender, e);

                        break;

                    case 1:
                        //
                        //基本情報
                        if (null != this.logic.TorihikisakiKihonSearchResult && this.logic.TorihikisakiKihonSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.TorihikisakiKihonHyouji();
                        }

                        break;

                    case 2:
                        //請求情報1
                        if (null != this.logic.TorihikisakiSeikyuuSearchResult && this.logic.TorihikisakiSeikyuuSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.TorihikisakiSeikyuuHyouji();
                        }

                        break;

                    case 3:
                        //請求情報2
                        if (null != this.logic.TorihikisakiSeikyuuSearchResult && this.logic.TorihikisakiSeikyuuSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.TorihikisakiSeikyuuHyouji2();
                        }

                        break;

                    case 4:
                        //支払情報1
                        if (null != this.logic.TorihikisakiShiharaiSearchResult && this.logic.TorihikisakiShiharaiSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.TorihikisakiShiharaiHyouji();
                        }

                        break;

                    case 5:
                        //支払情報2
                        if (null != this.logic.TorihikisakiShiharaiSearchResult && this.logic.TorihikisakiShiharaiSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.TorihikisakiShiharaiHyouji2();
                        }

                        break;

                    case 6:
                        //取引先分類
                        if (null != this.logic.TorihikisakiKihonSearchResult && this.logic.TorihikisakiKihonSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.TorihikisakiBunruiHyouji();
                        }

                        break;

                    case 7:
                        //業者一覧
                        this.logic.TorihikisakiGyoushaIchiranHyouji();

                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者のtabControlタブー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Gyousha_Selected(object sender, TabControlEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //取引先CD
                string trihikisaki_cd = this.logic.TorihikisakiCD;
                //業者CD
                string gyousha_cd = this.logic.GyoushaCD;

                //タブーIndex
                int tabindex = e.TabPageIndex;
                switch (tabindex)
                {
                    case 0:
                        //ヘッダ情報
                        if (null != gyousha_cd && !"".Equals(gyousha_cd) && !DBNull.Value.Equals(gyousha_cd))
                        {
                            Tab_GyoushaMaster_Click(sender, e);
                        }

                        break;

                    case 1:
                        //基本情報
                        if (null != this.logic.GyoushaInfoSearchResult && this.logic.GyoushaInfoSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.GyoushaKihonHyouji();
                        }

                        break;

                    case 2:
                        //請求情報
                        if (null != this.logic.GyoushaInfoSearchResult && this.logic.GyoushaInfoSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.GyoushaSeikyuuHyouji();
                        }

                        break;

                    case 3:
                        //支払情報
                        if (null != this.logic.GyoushaInfoSearchResult && this.logic.GyoushaInfoSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.GyoushaShiharaiHyouji();
                        }

                        break;

                    case 4:
                        //現場一覧
                        this.logic.GyoushaGenbaIchiranHyouji();

                        break;

                    case 5:
                        //分類情報
                        if (null != this.logic.GyoushaInfoSearchResult && this.logic.GyoushaInfoSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.GyoushaBunruiHyouji();
                        }

                        break;

                    case 6:
                        //委託契約(業者)情報
                        this.GyoushaItakuKeiyaku_Select();

                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場のtabControlタブー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Genba_Selected(object sender, TabControlEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //取引先CD
                string trihikisaki_cd = this.logic.TorihikisakiCD;
                //業者CD
                string gyousha_cd = this.logic.GyoushaCD;
                //現場CD
                string genba_cd = this.logic.GenbaCD;

                //タブーIndex
                int tabindex = e.TabPageIndex;
                switch (tabindex)
                {
                    case 0:
                        //ヘッダ情報
                        if (null != genba_cd && !"".Equals(genba_cd) && !DBNull.Value.Equals(genba_cd))
                        {
                            Tab_GenbaMaster_Click(sender, e);
                        }
                        break;

                    case 1:
                        //基本情報
                        if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.GenbaKihonHyouji();
                        }
                        break;

                    case 2:
                        //請求情報
                        if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.GenbaSeikyuuHyouji();
                        }
                        break;

                    case 3:
                        //支払情報
                        if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.GenbaShiharaiHyouji();
                        }
                        break;

                    case 4:
                        //分類情報
                        if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                        {
                            //表示
                            this.logic.GenbaBunruiHyouji();
                        }
                        break;

                    case 5:
                        //委託契約(現場)情報
                        this.ItakuKeiyakuUseKbn.Text = string.Empty;
                        if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                        {
                            //委託契約区分
                            if (!this.logic.GenbaInfoSearchResult.Rows[0].IsNull("ITAKU_KEIYAKU_USE_KBN"))
                            {
                                this.ItakuKeiyakuUseKbn.Text = this.logic.GenbaInfoSearchResult.Rows[0].Field<Int16>("ITAKU_KEIYAKU_USE_KBN").ToString();
                            }
                        }
                        this.GenbaItakuKeiyaku_Select();
                        break;

                    case 7:
                        if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                        {
                            //定期回収情報
                            this.logic.GenbaTeikiHinmei_Select();
                        }
                        break;

                    case 6:
                        if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                        {
                            //月極情報
                            this.logic.GenbaTsukiHinmei_Select();
                        }
                        break;
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        if (null != this.logic.GenbaInfoSearchResult && this.logic.GenbaInfoSearchResult.Rows.Count > 0)
                        {
                            // A票～E票
                            this.logic.SetAToEWindowsData();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受付一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uketsuke_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //受付区分
                string uketsukeKbn = this.logic.UketsukeSearchResult.Rows[rowIndex].Field<string>("UKETSUKE_KBN");
                //システムID
                long systemId = this.logic.UketsukeSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.UketsukeSearchResult.Rows[rowIndex].Field<int>("SEQ");

                //明細
                this.UketsukeDetailDate_Select(uketsukeKbn, systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受付クレーム一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uketuke_Kuremu_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                this.Uketsuke_Kuremu_Denpyou_NaiyouHyouji(rowIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受付クレーム一覧の行クリック内容表示
        /// </summary>
        /// <param name="rowindex">行番号</param>
        private void Uketsuke_Kuremu_Denpyou_NaiyouHyouji(int rowIndex)
        {
            LogUtility.DebugMethodStart(rowIndex);
            try
            {
                //20220114 INS ST nakayama #159312
                this.logic.UketsukeCMMeisaiCrear();
                //20220114 INS ED nakayama #159312
                //内容１
                if (!this.logic.UketsukeCMSearchResult.Rows[rowIndex].IsNull("NAIYOU1"))
                {
                    this.UKETSUKE_CM_NAIYOU1.Text = this.logic.UketsukeCMSearchResult.Rows[rowIndex].Field<string>("NAIYOU1");
                }
                //内容2
                if (!this.logic.UketsukeCMSearchResult.Rows[rowIndex].IsNull("NAIYOU2"))
                {
                    this.UKETSUKE_CM_NAIYOU2.Text = this.logic.UketsukeCMSearchResult.Rows[rowIndex].Field<string>("NAIYOU2");
                }
                //内容3
                if (!this.logic.UketsukeCMSearchResult.Rows[rowIndex].IsNull("NAIYOU3"))
                {
                    this.UKETSUKE_CM_NAIYOU3.Text = this.logic.UketsukeCMSearchResult.Rows[rowIndex].Field<string>("NAIYOU3");
                }
                //内容4
                if (!this.logic.UketsukeCMSearchResult.Rows[rowIndex].IsNull("NAIYOU4"))
                {
                    this.UKETSUKE_CM_NAIYOU4.Text = this.logic.UketsukeCMSearchResult.Rows[rowIndex].Field<string>("NAIYOU4");
                }
                //内容5
                if (!this.logic.UketsukeCMSearchResult.Rows[rowIndex].IsNull("NAIYOU5"))
                {
                    this.UKETSUKE_CM_NAIYOU5.Text = this.logic.UketsukeCMSearchResult.Rows[rowIndex].Field<string>("NAIYOU5");
                }
                //内容6
                if (!this.logic.UketsukeCMSearchResult.Rows[rowIndex].IsNull("NAIYOU6"))
                {
                    this.UKETSUKE_CM_NAIYOU6.Text = this.logic.UketsukeCMSearchResult.Rows[rowIndex].Field<string>("NAIYOU6");
                }
                //内容7
                if (!this.logic.UketsukeCMSearchResult.Rows[rowIndex].IsNull("NAIYOU7"))
                {
                    this.UKETSUKE_CM_NAIYOU7.Text = this.logic.UketsukeCMSearchResult.Rows[rowIndex].Field<string>("NAIYOU7");
                }
                //内容8
                if (!this.logic.UketsukeCMSearchResult.Rows[rowIndex].IsNull("NAIYOU8"))
                {
                    this.UKETSUKE_CM_NAIYOU8.Text = this.logic.UketsukeCMSearchResult.Rows[rowIndex].Field<string>("NAIYOU8");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受入伝票一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ukeire_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = this.logic.UkeireSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.UkeireSearchResult.Rows[rowIndex].Field<int>("SEQ");
                //明細
                this.UkeireDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷伝票一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shukka_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = this.logic.ShukkaSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.ShukkaSearchResult.Rows[rowIndex].Field<int>("SEQ");
                //明細
                this.ShukkaDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 売上/支払伝票一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UriageShiharai_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = this.logic.UrShSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.UrShSearchResult.Rows[rowIndex].Field<int>("SEQ");
                //明細
                this.UrShDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金伝票一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nyuukin_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = this.logic.NyuukinSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.NyuukinSearchResult.Rows[rowIndex].Field<int>("SEQ");
                //明細
                this.NyuukinDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出金伝票一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shukkin_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = this.logic.ShukkinSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.ShukkinSearchResult.Rows[rowIndex].Field<int>("SEQ");
                //明細
                this.ShukkinDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷伝票一覧の行選択時(検収)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShukkaKenshuu_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = this.logic.ShukkaKenshuuSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.ShukkaKenshuuSearchResult.Rows[rowIndex].Field<int>("SEQ");
                //明細
                this.ShukkaKenshuuDetail_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 委託契約（業者、現場）のDataGridViewのセル結合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItakuKeiyaku_Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ヘッダのみ行う
                if (e.RowIndex > -1)
                {
                    return;
                }
                // セルの矩形を取得
                Rectangle rect = e.CellBounds;
                CustomDataGridView dgv = (CustomDataGridView)sender;
                if (e.ColumnIndex == 2)
                {
                    //3セル結合
                    rect.Width += dgv.Columns[e.ColumnIndex + 1].Width + dgv.Columns[e.ColumnIndex + 2].Width;

                    // 矩形の位置を補正
                    //rect.X -= 1;
                    rect.Y += 1;
                    // 背景、枠線、セルの値を描画

                    using (SolidBrush brush = new SolidBrush(e.CellStyle.BackColor))
                    {
                        // 背景の描画
                        e.Graphics.FillRectangle(brush, rect);
                        using (Pen pen = new Pen(dgv.GridColor))
                        {
                            // 枠線の描画
                            e.Graphics.DrawRectangle(pen, rect);
                        }
                    }

                    // テキストの位置を調整
                    // rect.Y += 1;

                    // セルに表示するテキストを描画
                    TextRenderer.DrawText(e.Graphics,
                                                   dgv.Columns[e.ColumnIndex].HeaderText,
                                                   e.CellStyle.Font,
                                                   rect,
                                                   e.CellStyle.ForeColor,
                                                   TextFormatFlags.HorizontalCenter
                                                   | TextFormatFlags.VerticalCenter);

                    //================================
                    // DataGridViewヘッダー結合セルの枠線の設定
                    // Graphics オブジェクトを取得
                    Graphics g = e.Graphics;

                    // グレー，太さ 2 のペンを定義
                    // 直線を描画(ヘッダ上部)
                    int startX = dgv.Columns[0].Width + dgv.Columns[1].Width;
                    int endX = startX + dgv.Columns[2].Width + dgv.Columns[3].Width + dgv.Columns[4].Width;
                    g.DrawLine(new Pen(Color.DarkGray, 1), startX, rect.Y - 1, endX, rect.Y - 1);
                    //// 直線を描画(ヘッダ下部)
                    g.DrawLine(new Pen(Color.DarkGray, 1), startX + 1, rect.Y - 2 + rect.Height, endX + 1, rect.Y - 2 + rect.Height);
                    //================================
                }
                else if (e.ColumnIndex != 2 && e.ColumnIndex != 3 && e.ColumnIndex != 4)
                {
                    //結合しなくて通常描画
                    e.Paint(e.ClipBounds, e.PaintParts);
                }

                e.Handled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //LogUtility.DebugMethodEnd();
        }

        private void Gyousha_ItakuKeiyaku_Ichiran_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.Gyousha_ItakuKeiyaku_Ichiran.Refresh();
        }

        private void Genba_ItakuKeiyaku_Ichiran_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.Genba_ItakuKeiyaku_Ichiran.Refresh();
        }

        #region 伝票を参照で開く

        /// <summary>
        /// 受付伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uketsuke_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        private void Uketsuke_Meisai_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        /// <summary>
        /// 受付（クレーム）伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uketsuke_Kuremu_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        /// <summary>
        /// 計量伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Keiryou_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        private void MultiRow_KeiryouMeisai_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        /// <summary>
        /// 受入伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ukeire_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        private void MultiRow_UkeireMeisai_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        /// <summary>
        /// 出荷伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shukka_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        private void MultiRow_ShukkaMeisai_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        /// <summary>
        /// 売上/支払伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UriageShiharai_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        private void UriageShiharai_Meisai_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        /// <summary>
        /// 入金伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nyuukin_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        private void Nyuukin_Meisai_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        /// <summary>
        /// 出金伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shukkin_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        private void Shukkin_Meisai_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        /// <summary>
        /// 出荷伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShukkaKenshuu_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        private void MultiRow_ShukkaKenshuu_DoubleClick(object sender, EventArgs e)
        {
            this.OpenSlipByReferenceMode();
        }

        /// <summary>
        /// 伝票を参照モードで開く
        /// </summary>
        private void OpenSlipByReferenceMode()
        {
            string refNo = "";
            string tabName = "";

            //呼び出し画面判別
            tabName = tabControl1.SelectedTab.Text;

            #region 参照モードで画面を開く(伝票)

            if ("伝票" == tabName)
            {
                //伝票Tab内の伝票名を取得
                string denName = this.TabControl_Denpyou.SelectedTab.Text;

                if ("受付" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Uketsuke_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    //一覧で選択中の一行を取得
                    string UketsukeKubun = this.Uketsuke_Denpyou.CurrentRow.Cells["UKETUKE_DENPYOU_UKETSUKE_KBN"].Value.ToString();//受付区分
                    refNo = this.Uketsuke_Denpyou.CurrentRow.Cells["UKETUKE_DENPYOU_UKETUKE_NO"].Value.ToString();//受付番号

                    if ("収集" == UketsukeKubun)
                    {
                        //G015	収集受付入力
                        FormManager.OpenFormWithAuth("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                    else if ("出荷" == UketsukeKubun)
                    {
                        //G016	出荷受付入力
                        FormManager.OpenFormWithAuth("G016", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                    else if ("持込" == UketsukeKubun)
                    {
                        //G018	持込受付入力
                        FormManager.OpenFormWithAuth("G018", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                }
                else if ("受付（クレーム）" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Uketsuke_Kuremu_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    //一覧で選択中の一行を取得
                    refNo = this.Uketsuke_Kuremu_Denpyou.CurrentRow.Cells["UKETUKE_KUREMU_DENPYOU_UKETUKE_NO"].Value.ToString();//受付番号
                    FormManager.OpenFormWithAuth("G020", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                }
                else if ("受入" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Ukeire_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.Ukeire_Denpyou.CurrentRow.Cells["UKEIRE_DENPYOU_UKEIRE_NO"].Value.ToString();//受入番号
                    if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        FormManager.OpenFormWithAuth("G051", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                    else
                    {
                        FormManager.OpenFormWithAuth("G721", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                }
                else if ("出荷" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Shukka_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.Shukka_Denpyou.CurrentRow.Cells["SHUKKA_DENPYOU_SHUKKA_NO"].Value.ToString();//出荷番号
                    if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        FormManager.OpenFormWithAuth("G053", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                    else
                    {
                        FormManager.OpenFormWithAuth("G722", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                }
                else if ("売上/支払" == denName)
                {
                    //一覧で選択していない場合
                    if (this.UriageShiharai_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.UriageShiharai_Denpyou.CurrentRow.Cells["URIAGESIHARAI_NO"].Value.ToString();//売上/支払番号

                    // 20150602 代納伝票対応(代納不具合一覧52) Start
                    T_UR_SH_ENTRY entry = new T_UR_SH_ENTRY();
                    entry.UR_SH_NUMBER = Convert.ToInt64(refNo);
                    entry.DELETE_FLG = false;
                    entry = this.logic.UrShDao.GetDataForEntity(entry).FirstOrDefault();
                    if (entry != null)
                    {
                        if (entry.DAINOU_FLG.IsNull || entry.DAINOU_FLG.IsFalse)
                        {
                            FormManager.OpenFormWithAuth("G054", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                        }
                        else
                        {
                            FormManager.OpenFormWithAuth("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                        }
                    }
                    // 20150602 代納伝票対応(代納不具合一覧52) End
                }
                else if ("代納" == denName)
                {
                    //一覧で選択していない場合
                    if (this.MultiRow_DaiNouDenpyou.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.MultiRow_DaiNouDenpyou.CurrentRow.Cells["DENPYOU_NUMBER"].Value.ToString();//代納番号

                    T_UR_SH_ENTRY entry = new T_UR_SH_ENTRY();
                    entry.UR_SH_NUMBER = Convert.ToInt64(refNo);
                    entry.DELETE_FLG = false;
                    entry = this.logic.UrShDao.GetDataForEntity(entry).FirstOrDefault();
                    if (entry != null)
                    {
                        if (!entry.DAINOU_FLG.IsNull || entry.DAINOU_FLG.IsTrue)
                        {
                            FormManager.OpenFormWithAuth("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                        }
                    }
                }
                else if ("入金" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Nyuukin_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.Nyuukin_Denpyou.CurrentRow.Cells["NYUKIN_DENPYOU_NYUUKIN_NUMBER"].Value.ToString();//入金番号

                    T_NYUUKIN_ENTRY entry = new T_NYUUKIN_ENTRY();
                    entry.NYUUKIN_NUMBER = Convert.ToInt64(refNo);
                    entry.DELETE_FLG = false;
                    entry = this.logic.NyuukinDao.GetDataForEntity(entry).FirstOrDefault();
                    if (entry != null && entry.TOK_INPUT_KBN.IsFalse)
                    {
                        FormManager.OpenFormWithAuth("G619", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                    else
                    {
                        FormManager.OpenFormWithAuth("G459", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                }
                else if ("出金" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Shukkin_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.Shukkin_Denpyou.CurrentRow.Cells["SHUKIN_DENPYOU_SHUKKIN_NUMBER"].Value.ToString();//出金番号
                    FormManager.OpenFormWithAuth("G090", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                }
                else if ("検収" == denName)
                {
                    //一覧で選択していない場合
                    if (this.MultiRow_ShukkaKenshuu.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.ShukkaKenshuu_Denpyou.CurrentRow.Cells["SHUKKA_KENSHUU_SHUKKA_NUMBER"].Value.ToString();//出荷番号
                    FormManager.OpenFormWithAuth("G053", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                }
            }

            #endregion
        }

        #endregion

        /// <summary>
        /// 取引中止業者指定変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKI_STOP_TextChanged(object sender, EventArgs e)
        {
            //取引先業者一覧取得
            this.logic.TorihikisakiGyoushaIchiranSearch();

            //表示
            this.logic.TorihikisakiGyoushaIchiranHyouji();
        }

        /// <summary>
        /// 取引中止現場指定変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_TORIHIKI_STOP_TextChanged(object sender, EventArgs e)
        {
            //業者現場一覧取得
            this.logic.GyoushaGenbaIchiranSearch();

            //表示
            this.logic.GyoushaGenbaIchiranHyouji();
        }

        private void MultiRow_DaiNouDenpyou_RowEnter(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //支払
                //システムID
                long systemIdUkeire = !DBNull.Value.Equals(this.logic.DainouSearchResult.Rows[rowIndex].ItemArray[10]) ? this.logic.DainouSearchResult.Rows[rowIndex].Field<long>("UKEIRE_SYSTEM_ID") : 0;
                //SEQ
                int seqUkeire = !DBNull.Value.Equals(this.logic.DainouSearchResult.Rows[rowIndex].ItemArray[11]) ? this.logic.DainouSearchResult.Rows[rowIndex].Field<int>("UKEIRE_SEQ") : 0;
                //システムID
                //売上
                long systemIdShukka = !DBNull.Value.Equals(this.logic.DainouSearchResult.Rows[rowIndex].ItemArray[27]) ? this.logic.DainouSearchResult.Rows[rowIndex].Field<long>("SHUKKA_SYSTEM_ID") : 0;
                //SEQ
                int seqShukka = !DBNull.Value.Equals(this.logic.DainouSearchResult.Rows[rowIndex].ItemArray[28]) ? this.logic.DainouSearchResult.Rows[rowIndex].Field<int>("SHUKKA_SEQ") : 0;
                //売上/支払明細
                this.DainouDetailDate_Select(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void MultiRow_DaiNouDenpyou_DoubleClick(object sender, EventArgs e)
        {
            OpenSlipByReferenceMode();
        }

        //20211228 Thanh 158919 s
        /// <summary>
        /// TabControl_Denpyou_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Denpyou_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tabindex = TabControl_Denpyou.SelectedIndex;
            if (tabindex < 0) return;
            var page = this.TabControl_Denpyou.TabPages[tabindex];
            switch (page.Name)
            {
                case "Denpyou_TabPage_Uketsuke":
                    //受付
                    this.logic.adjustColumnSize(this.Uketsuke_Denpyou);
                    this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_UKETSUKE_KBN"].Width = this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_UKETSUKE_KBN"].Width + 10;
                    this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_SAGYOU_DATE"].Width = 110;
                    this.Uketsuke_Denpyou.Columns["UKETUKE_DENPYOU_UKETUKE_DATE"].Width = 110;
                    this.logic.adjustColumnSize(this.Uketsuke_Meisai);
                    break;

                case "Denpyou_TabPage_Uketsuke_Kuremu":
                    //受付クレーム
                    this.logic.adjustColumnSize(this.Uketsuke_Kuremu_Denpyou);
                    this.Uketsuke_Kuremu_Denpyou.Columns["UKETUKE_KUREMU_DENPYOU_UKETUKE_DATE"].Width = 110;
                    this.Uketsuke_Kuremu_Denpyou.Columns["UKETUKE_KUREMU_DENPYOU_TAIOKANRYOU_DATE"].Width = 110;
                    break;

                case "Denpyou_TabPage_Ukeire":
                    //受入
                    this.logic.adjustColumnSize(this.Ukeire_Denpyou);
                    this.Ukeire_Denpyou.Columns["UKEIRE_DENPYOU_DENPYOU_DATE"].Width = 110;
                    this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                    this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;

                    break;

                case "Denpyou_TabPage_Shukka":
                    //出荷
                    this.logic.adjustColumnSize(this.Shukka_Denpyou);
                    this.Shukka_Denpyou.Columns["SHUKKA_DENPYOU_DENPYOU_DATE"].Width = 110;
                    this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                    this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                    break;

                case "Denpyou_TabPage_UriageShiharai":
                    //売上/支払
                    this.logic.adjustColumnSize(this.UriageShiharai_Denpyou);
                    this.UriageShiharai_Denpyou.Columns["URIAGESIHARAI_DENPYOU_DENPYOU_DATE"].Width = 110;
                    this.logic.adjustColumnSize(this.UriageShiharai_Meisai);
                    break;

                case "Denpyou_TabPage_Dainou":
                    //代納
                    this.MultiRow_DaiNouDenpyou.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                    this.MultiRow_DaiNouDenpyou.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                    this.MultiRow_DaiNouMeissai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                    this.MultiRow_DaiNouMeissai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                    break;

                case "Denpyou_TabPage_Nyuukin":
                    //入金
                    this.logic.adjustColumnSize(this.Nyuukin_Denpyou);
                    this.Nyuukin_Denpyou.Columns["NYUKIN_DENPYOU_DENPYOU_DATE"].Width = 110;
                    this.logic.adjustColumnSize(this.Nyuukin_Meisai);
                    break;

                case "Denpyou_TabPage_Shukkin":
                    //出金
                    this.logic.adjustColumnSize(this.Shukkin_Denpyou);
                    this.Shukkin_Denpyou.Columns["SHUKIN_DENPYOU_DENPYOU_DATE"].Width = 110;
                    this.logic.adjustColumnSize(this.Shukkin_Meisai);
                    break;
                case "Denpyou_TabPage_ShukkaKenshuu":
                    //検収
                    this.logic.adjustColumnSize(this.ShukkaKenshuu_Denpyou);
                    this.ShukkaKenshuu_Denpyou.Columns["SHUKKA_KENSHUU_DENPYOU_DATE"].Width = 110;

                    this.MultiRow_ShukkaKenshuu.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                    this.MultiRow_ShukkaKenshuu.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                    break;
                case "Denpyou_TabPage_Contena":
                    //コンテナ
                    this.logic.adjustColumnSize(this.Contena_Denpyou);
                    if (this.Contena_Denpyou.Columns["SecchiChouhuku"].Visible)
                    {
                        this.Contena_Denpyou.Columns["SecchiChouhuku"].Width = this.Contena_Denpyou.Columns["SecchiChouhuku"].Width + 5;
                    }
                    if (this.Contena_Denpyou.Columns["CONTENA_SHURUI_NAME_RYAKU"].Visible)
                    {
                        this.Contena_Denpyou.Columns["CONTENA_SHURUI_NAME_RYAKU"].Width = this.Contena_Denpyou.Columns["CONTENA_SHURUI_NAME_RYAKU"].Width + 5;
                    }
                    break;
            }
        }
        //20211228 Thanh 158919 e

        private void Contena_Denpyou_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.Contena_Denpyou.Columns[e.ColumnIndex].Name == "SecchiChouhuku")
            {
                // 目立つ色に変更
                e.CellStyle.ForeColor = Color.Red;
            }
        }
    }
}