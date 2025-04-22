// $Id: TorihikisakiHoshuForm.cs 54798 2015-07-07 02:14:18Z minhhoang@e-mall.co.jp $
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic; // LANDUONG - 20220209
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using TorihikisakiHoshu.Const;
using TorihikisakiHoshu.Dao;
using TorihikisakiHoshu.Logic;
using r_framework.CustomControl;

namespace TorihikisakiHoshu.APP
{
    /// <summary>
    /// 車種保守画面
    /// </summary>
    [Implementation]
    public partial class TorihikisakiHoshuForm : SuperForm
    {
        /// <summary>
        /// 車種保守画面ロジック
        /// </summary>
        private TorihikisakiHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 銀行支店ポップアップ表示フラグ
        /// </summary>
        private bool isBankShitenPopup;

        /// <summary>
        /// 銀行支店2ポップアップ表示フラグ
        /// </summary>
        private bool isBankShitenPopup_2;

        /// <summary>
        /// 銀行支店3ポップアップ表示フラグ
        /// </summary>
        private bool isBankShitenPopup_3;

        /// <summary>
        /// 銀行支店CD前回入力値
        /// </summary>
        internal string previousBankShitenCd;

        /// <summary>
        /// 銀行支店CD2前回入力値
        /// </summary>
        internal string previousBankShitenCd_2;

        /// <summary>
        /// 銀行支店CD3前回入力値
        /// </summary>
        internal string previousBankShitenCd_3;
        internal string previousBankShitenMotoCd;//160026
        private bool isBankShitenPopup_moto;//160026
        private string beforeFURIKOMI_BANK_MOTO_CD = string.Empty;//160026


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TorihikisakiHoshuForm()
            : base(WINDOW_ID.M_TORIHIKISAKI, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new TorihikisakiHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="denshiShinseiFlg">True：電子申請で使用 False：電子申請で使用せず</param>
        public TorihikisakiHoshuForm(WINDOW_TYPE type, string torihikisakiCd, bool denshiShinseiFlg)
            : base(WINDOW_ID.M_TORIHIKISAKI, type)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new TorihikisakiHoshuLogic(this);
            this.logic.TorihikisakiCd = torihikisakiCd;
            this.logic.denshiShinseiFlg = denshiShinseiFlg;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        public TorihikisakiHoshuForm(WINDOW_TYPE windowType, string torihikisakiCd, bool denshiShinseiFlg, bool isFromShouninzumiDenshiShinseiIchiran)
            : base(WINDOW_ID.M_TORIHIKISAKI, windowType)
        {
            InitializeComponent();

            this.logic = new TorihikisakiHoshuLogic(this);

            this.logic.TorihikisakiCd = torihikisakiCd;
            this.logic.denshiShinseiFlg = denshiShinseiFlg;
            this.logic.IsFromShouninzumiDenshiShinseiIchiran = isFromShouninzumiDenshiShinseiIchiran;
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //※※※　何故か、タブが一度選択されないと修正時に敬称がうまくセットされないので
            //※※※　強制的に一度全タブを選択して戻すようにすることで一旦解決
            TabPage now = this.tabData.SelectedTab;
            foreach (TabPage page in this.tabData.TabPages)
            {
                this.tabData.SelectedTab = page;
            }
            this.tabData.SelectedTab = now;
            //※※※　強引な対応ここまで

            bool catchErr = this.logic.WindowInit(WindowType);
            if (catchErr)
            {
                return;
            }

            this.logic.Search();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.GYOUSHA_ICHIRAN != null)
            {
                this.GYOUSHA_ICHIRAN.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
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
            base.OnShown(e);
        }

        /// <summary>
        /// 取引先一覧からの再描画処理
        /// </summary>
        public virtual void ReloadWindow(object[] param)
        {

            WINDOW_TYPE windowType = (WINDOW_TYPE)param[0];
            string torihikisakiCd = param[1].ToString();

            //処理モード設定
            base.WindowType = windowType;

            // 画面タイトル変更
            base.HeaderFormInit();

            this.logic.TorihikisakiCd = torihikisakiCd;

            this.logic.ModeInit(WindowType, (BusinessBaseForm)this.ParentForm);
        }

        /// <summary>
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M213", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            // 処理モード変更
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // 画面タイトル変更
            base.HeaderFormInit();

            // 画面初期化
            this.logic.TorihikisakiCd = string.Empty;
            this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
        }

        /// <summary>
        /// 【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            // 権限降格チェック
            if (r_framework.Authority.Manager.CheckAuthority("M213", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                // 画面タイトル変更
                base.HeaderFormInit();
                // 画面初期化
                this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
            }
            else if (r_framework.Authority.Manager.CheckAuthority("M213", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                if (!r_framework.Authority.Manager.CheckAuthority("M213", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                // 処理モード変更
                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                // 画面タイトル変更
                base.HeaderFormInit();
                // 画面初期化
                this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
            }
        }

        /// <summary>
        /// 取消ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel((BusinessBaseForm)this.Parent);
        }

        /// <summary>
        /// 一覧画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            this.logic.ShowIchiran();
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
                /// 20141217 Houkakou 「取引先入力」の日付チェックを追加する　start
                if (this.logic.DateCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「取引先入力」の日付チェックを追加する　end

                // Begin: LANDUONG - 20220209 - refs#160050
                if ((this.logic.denshiSeikyusho && this.logic.denshiSeikyuRaku) || (this.logic.denshiSeikyusho && !this.logic.denshiSeikyuRaku)
                    || (!this.logic.denshiSeikyusho && this.logic.denshiSeikyuRaku))
                {
                    if (this.OUTPUT_KBN.Text.Equals("2") && !this.logic.denshiSeikyusho)
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E319"); // MessageB
                        return;
                    }
                    else if (this.OUTPUT_KBN.Text.Equals("3") && !this.logic.denshiSeikyuRaku)
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E320"); // MessageC
                        return;
                    }
                    else if (this.OUTPUT_KBN.Text.Equals("3") && this.SHOSHIKI_KBN.Text.Equals("1"))
                    {
                        if (base.RegistErrorFlag)
                        {
                            return;
                        }

                        // 楽楽顧客コード = BLANK かつ システム設定.楽楽顧客コード採番方法 = 1.自動採番の場合
                        if (string.IsNullOrWhiteSpace(this.RAKURAKU_CUSTOMER_CD.Text)
                            && (!this.logic.entitysM_SYS_INFO.RAKURAKU_CODE_NUMBERING_KBN.IsNull && this.logic.entitysM_SYS_INFO.RAKURAKU_CODE_NUMBERING_KBN == 1))
                        {
                            // 自動採番を実施（取引先CD＋"000000"＋"000000"を設定）
                            this.RAKURAKU_CUSTOMER_CD.Text = this.TORIHIKISAKI_CD.Text + "000000" + "000000";
                        }
                        // 楽楽顧客コード = BLANK かつ システム設定.楽楽顧客コード採番方法 = 2.手動採番の場合
                        else if (string.IsNullOrWhiteSpace(this.RAKURAKU_CUSTOMER_CD.Text)
                            && (!this.logic.entitysM_SYS_INFO.RAKURAKU_CODE_NUMBERING_KBN.IsNull && this.logic.entitysM_SYS_INFO.RAKURAKU_CODE_NUMBERING_KBN == 2))
                        {
                            // 確認メッセージを表示
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            if (msgLogic.MessageBoxShow("C122", "楽楽顧客コード") == System.Windows.Forms.DialogResult.No)
                            {
                                return;
                            }
                        }
                    }

                    if (this.OUTPUT_KBN.Text.Equals("3"))
                    {
                        string beforeCode = this.logic.GetBeforeRakurakuCode();
                        if (!this.RAKURAKU_CUSTOMER_CD.Text.Equals(beforeCode))
                        {
                            if (!MasterCommonLogic.CheckRakurakuCustomerCode(this.RAKURAKU_CUSTOMER_CD.Text))
                            {
                                var msgLogic = new MessageBoxShowLogic();
                                this.RAKURAKU_CUSTOMER_CD.BackColor = Constans.ERROR_COLOR;
                                msgLogic.MessageBoxShow("E312"); // MessageA
                                this.RAKURAKU_CUSTOMER_CD.Focus();
                                return;
                            }
                        }
                    }
                }
                // End: LANDUONG - 20220209 - refs#160050

                var message = String.Empty;

                if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.WindowType)
                {
                    // 削除時は確認メッセージを表示（トランザクション内でメッセージを出力したくないので最初に確認する）
                    var result = new MessageBoxShowLogic().MessageBoxShow("C026");
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                // VUNGUYEN 20150525 #1294 START
                if (this.logic.CheckRegistData())
                {
                    return;
                }
                // VUNGUYEN 20150525 #1294 END

                // 緯度経度の入力チェック
                if (this.logic.CheckLocation())
                {
                    return;
                }

                // 登録番号が入力されている場合、入力文字数をチェック
                if (this.TOUROKU_NO.Text != "" && this.TOUROKU_NO.Text.Length != 14)
                {
                    DialogResult result = MessageBox.Show("登録番号は14桁で入力してください。\r登録を継続しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                bool catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }

                string sTorihikisakiCd = this.logic.entitysTORIHIKISAKI.TORIHIKISAKI_CD;

                // TODO トランザクション部分をロジックに移動する。20140808 おがわ

                using (var tran = new Transaction())
                {
                    switch (base.WindowType)
                    {
                        // 新規追加
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            // 重複チェック
                            bool result = this.DupliUpdateViewCheck(e, true, out catchErr);
                            if (catchErr)
                            {
                                return;
                            }
                            if (result)
                            {
                                // 重複していなければ登録を行う
                                this.logic.Regist(base.RegistErrorFlag);

                                message = "登録";
                            }
                            break;

                        // 更新
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.logic.Update(base.RegistErrorFlag);

                            message = "更新";
                            break;

                        // 論理削除
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            this.logic.LogicalDelete();

                            message = "削除";
                            break;

                        default:
                            break;
                    }
                    if (base.RegistErrorFlag)
                    {
                        return;
                    }

                    if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.WindowType && null != this.logic.LoadHikiaiTorihikisaki)
                    {
                        // 引合取引先をもとに本登録した場合は、引合取引先のDELETE_FLGをTrueにする
                        this.logic.LoadHikiaiTorihikisaki.TORIHIKISAKI_CD_AFTER = this.logic.entitysTORIHIKISAKI.TORIHIKISAKI_CD;
                        this.logic.LoadHikiaiTorihikisaki.DELETE_FLG = true;
                        this.logic.UpdateHikiaiTorihikisaki(this.logic.LoadHikiaiTorihikisaki);

                        // 引合取引先をもとに本登録した場合は、引合業者の取引先CDを変更する
                        var hikiaiGyoushaDao = DaoInitUtility.GetComponent<IM_HIKIAI_GYOUSHADao>();
                        hikiaiGyoushaDao.UpdateHikiaiTorihikisakiCd(this.logic.LoadHikiaiTorihikisaki.TORIHIKISAKI_CD, this.logic.entitysTORIHIKISAKI.TORIHIKISAKI_CD);

                        // 引合取引先をもとに本登録した場合は、引合現場の返送先を変更する
                        var genbaDao = DaoInitUtility.GetComponent<GenbaDao>();
                        genbaDao.UpdateGenba(this.logic.LoadHikiaiTorihikisaki.TORIHIKISAKI_CD, this.logic.entitysTORIHIKISAKI.TORIHIKISAKI_CD);

                        // 引合取引先をもとに本登録した場合は、引合現場の取引先CDを変更する
                        var hikiaiGenbaDao = DaoInitUtility.GetComponent<IM_HIKIAI_GENBADao>();
                        hikiaiGenbaDao.UpdateHikiaiTorihikisakiCd(this.logic.LoadHikiaiTorihikisaki.TORIHIKISAKI_CD, this.logic.entitysTORIHIKISAKI.TORIHIKISAKI_CD);

                        // 引合取引先をもとに本登録した場合は、見積データの取引先CDを変更する
                        var mitsumoriEntryDao = DaoInitUtility.GetComponent<IMitsumoriEntryDao>();
                        var mitsumoriEntryKeyEntity = new r_framework.Entity.T_MITSUMORI_ENTRY();
                        mitsumoriEntryKeyEntity.HIKIAI_TORIHIKISAKI_FLG = true;
                        mitsumoriEntryKeyEntity.TORIHIKISAKI_CD = this.logic.LoadHikiaiTorihikisaki.TORIHIKISAKI_CD;
                        mitsumoriEntryKeyEntity.DELETE_FLG = false;
                        var mitsumoriEntryList = mitsumoriEntryDao.GetMitsumoriEntryList(mitsumoriEntryKeyEntity);
                        foreach (var entryEntity in mitsumoriEntryList)
                        {
                            entryEntity.DELETE_FLG = true;
                            mitsumoriEntryDao.Update(entryEntity);

                            var newMitsumoriEntryBinderLogic = new DataBinderLogic<r_framework.Entity.T_MITSUMORI_ENTRY>(entryEntity);
                            newMitsumoriEntryBinderLogic.SetSystemProperty(entryEntity, false);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this), entryEntity);
                            entryEntity.SEQ = entryEntity.SEQ.Value + 1;
                            entryEntity.TORIHIKISAKI_CD = this.logic.entitysTORIHIKISAKI.TORIHIKISAKI_CD;
                            entryEntity.HIKIAI_TORIHIKISAKI_FLG = false;
                            entryEntity.DELETE_FLG = false;
                            mitsumoriEntryDao.Insert(entryEntity);

                            var mitsumoriDetailDao = DaoInitUtility.GetComponent<IMitsumoriDetailDao>();
                            var mitsumoriDetailKeyEntity = new r_framework.Entity.T_MITSUMORI_DETAIL();
                            mitsumoriDetailKeyEntity.SYSTEM_ID = entryEntity.SYSTEM_ID;
                            mitsumoriDetailKeyEntity.SEQ = entryEntity.SEQ - 1;
                            var mitsumoriDetailList = mitsumoriDetailDao.GetMitsumoriDetailList(mitsumoriDetailKeyEntity);
                            foreach (var detailEntity in mitsumoriDetailList)
                            {
                                detailEntity.SEQ = entryEntity.SEQ;
                                mitsumoriDetailDao.Insert(detailEntity);
                            }
                        }
                    }

                    // 電子申請から登録した場合は、電子申請のステータスを変更する
                    if (this.logic.IsFromShouninzumiDenshiShinseiIchiran)
                    {
                        var denshiShinseiEntryDao = DaoInitUtility.GetComponent<IDenshiShinseiEntryDao>();
                        var denshiShinseiEntryKeyEntity = new r_framework.Entity.T_DENSHI_SHINSEI_ENTRY();
                        if (!String.IsNullOrEmpty(this.ShinseiHikiaiTorihikisakiCd))
                        {
                            denshiShinseiEntryKeyEntity.HIKIAI_TORIHIKISAKI_CD = this.ShinseiHikiaiTorihikisakiCd;
                        }
                        else
                        {
                            denshiShinseiEntryKeyEntity.TORIHIKISAKI_CD = this.ShinseiTorihikisakiCd;
                        }

                        denshiShinseiEntryKeyEntity.DELETE_FLG = false;
                        denshiShinseiEntryKeyEntity.SYSTEM_ID = this.DenshiShinseiSystemId;
                        denshiShinseiEntryKeyEntity.SEQ = this.DenshiShinseiSeq;
                        var denshiShinseiEntry = denshiShinseiEntryDao.GetShouninzumiDenshiShinseiEntryList(denshiShinseiEntryKeyEntity);
                        if (0 != denshiShinseiEntry.Count())
                        {
                            var denshiShinseiStatusDao = DaoInitUtility.GetComponent<IDenshiShinseiStatusDao>();
                            var denshiShinseiStatusKeyEntity = new r_framework.Entity.T_DENSHI_SHINSEI_STATUS();
                            denshiShinseiStatusKeyEntity.SYSTEM_ID = denshiShinseiEntry.FirstOrDefault().SYSTEM_ID;
                            denshiShinseiStatusKeyEntity.SEQ = denshiShinseiEntry.FirstOrDefault().SEQ;
                            denshiShinseiStatusKeyEntity.DELETE_FLG = false;
                            var denshiShinseiStatusList = denshiShinseiStatusDao.GetDenshiShinseiStatusList(denshiShinseiStatusKeyEntity);
                            foreach (var denshiShinseiStatus in denshiShinseiStatusList)
                            {
                                denshiShinseiStatus.DELETE_FLG = true;
                                denshiShinseiStatusDao.Update(denshiShinseiStatus);

                                var newDenshiShinseiStatusBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_SHINSEI_STATUS>(denshiShinseiStatus);
                                newDenshiShinseiStatusBinderLogic.SetSystemProperty(denshiShinseiStatus, false);
                                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this), denshiShinseiStatus);
                                denshiShinseiStatus.UPDATE_NUM = denshiShinseiStatus.UPDATE_NUM + 1;
                                denshiShinseiStatus.SHINSEI_STATUS_CD = (int)DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.COMPLETE;
                                denshiShinseiStatus.SHINSEI_STATUS = DenshiShinseiUtility.STR_COMPLETE;
                                denshiShinseiStatus.DELETE_FLG = false;
                                denshiShinseiStatusDao.Insert(denshiShinseiStatus);
                            }
                        }
                    }

                    if (this.logic.isRegist)
                    {
                        tran.Commit();
                    }
                }

                if (this.logic.isRegist)
                {
                    new MessageBoxShowLogic().MessageBoxShow("I001", message);
                    string torihikisakiCd = this.TORIHIKISAKI_CD.Text;
                    WINDOW_TYPE beforeWindowType = this.WindowType;

                    if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.WindowType && null != this.logic.LoadHikiaiTorihikisaki)
                    {
                        // 本登録時は承認済申請一覧を更新
                        FormManager.UpdateForm("G561");
                    }

                    // 権限チェック
                    if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    {
                        if (r_framework.Authority.Manager.CheckAuthority("M213", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // DB更新後、新規モードで表示
                            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            base.HeaderFormInit();
                            this.logic.TorihikisakiCd = string.Empty;
                            this.logic.isRegist = false;
                            catchErr = this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
                            if (catchErr)
                            {
                                return;
                            }
                        }
                        else
                        {
                            // 権限が無い場合は画面を閉じる
                            var parentForm = (BusinessBaseForm)this.Parent;
                            this.Close();
                            if (parentForm != null)
                            {
                                parentForm.Close();
                            }
                        }
                    }
                    else//open mode 参照 after regist
                    {
                        if (r_framework.Authority.Manager.CheckAuthority("M213", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            base.HeaderFormInit();
                            this.logic.TorihikisakiCd = sTorihikisakiCd;
                            this.logic.isRegist = false;
                            catchErr = this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
                            parentForm.bt_func3.Enabled = true;
                            if (catchErr)
                            {
                                return;
                            }
                        }
                        else
                        {
                            // 権限が無い場合は画面を閉じる
                            var parentForm = (BusinessBaseForm)this.Parent;
                            this.Close();
                            if (parentForm != null)
                            {
                                parentForm.Close();
                            }
                        }

                        if (beforeWindowType == WINDOW_TYPE.NEW_WINDOW_FLAG
                            && !this.logic.IsFromShouninzumiDenshiShinseiIchiran)
                        {
                            // ワークフローから本登録する場合は、業者、現場の登録は不要
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            DialogResult dialogResult = msgLogic.MessageBoxShowConfirm("同じ情報を業者マスタへ連携しますか？");
                            if (dialogResult == DialogResult.Yes)
                            {
                                if (r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                                {
                                    r_framework.FormManager.FormManager.OpenFormWithAuth("M215", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, string.Empty, false, false, string.Empty, string.Empty, -1, -1, torihikisakiCd);
                                }
                                else
                                {
                                    msgLogic.MessageBoxShow("E158", "新規");
                                }
                            }
                        }
                    }
                    this.logic.IsFromShouninzumiDenshiShinseiIchiran = false;
                }
            }
        }

        /// <summary>
        /// 入金先CD重複チェック and 修正モード起動要否チェック
        /// </summary>
        /// <param name="e">イベント</param>
        /// <param name="isRegister">登録中か判断します</param>
        private bool DupliUpdateViewCheck(EventArgs e, bool isRegister, out bool catchErr)
        {
            try
            {
                bool result = false;
                catchErr = false;

                // 入金先CDの入力値をゼロパディング
                string zeroPadCd = this.logic.ZeroPadding(this.TORIHIKISAKI_CD.Text);

                // 重複チェック
                TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult isUpdate = this.logic.DupliCheckTorihikisakiCd(zeroPadCd, isRegister);

                if (isUpdate == TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult.FALSE_ON)
                {
                    // 権限降格チェック
                    if (r_framework.Authority.Manager.CheckAuthority("M213", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正モードで表示する
                        this.logic.TorihikisakiCd = zeroPadCd;
                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                        // ヘッダー情報
                        base.HeaderFormInit();

                        this.NYUUKINSAKI_KBN.Focus();

                        // 修正モードで画面初期化
                        this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority("M213", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        // 参照モードで表示する
                        this.logic.TorihikisakiCd = zeroPadCd;
                        base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;

                        // ヘッダー情報
                        base.HeaderFormInit();

                        this.NYUUKINSAKI_KBN.Focus();

                        // 参照モードで画面初期化
                        this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                        this.TORIHIKISAKI_CD.Text = string.Empty;
                        this.TORIHIKISAKI_CD.Focus();
                    }

                    result = true;
                }
                else if (isUpdate != TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult.TURE_NONE)
                {
                    // 入力した取引先CDが重複した かつ 修正モード未起動の場合
                    this.TORIHIKISAKI_CD.Text = string.Empty;
                    this.TORIHIKISAKI_CD.Focus();
                }
                else
                {
                    // 重複しなければINSERT処理を行うフラグON
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("DupliUpdateViewCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        /// <summary>
        /// 取引先CD変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void TorihikisakiCdValidated(object sender, EventArgs e)
        {
            //this.logic.TorihikisakiCdValidated();
            // 【新規】モードの場合のみチェック処理を行う
            if (base.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                return;
            }

            // 入力された取引先CD取得
            string inputCd = this.TORIHIKISAKI_CD.Text;
            if (string.IsNullOrWhiteSpace(inputCd))
            {
                return;
            }

            bool catchErr = false;
            if (this.logic.IsFromShouninzumiDenshiShinseiIchiran)
            {
                if (this.logic.IsDupliTorihikisakiCd(inputCd, out catchErr) && !catchErr)
                {
                    new MessageBoxShowLogic().MessageBoxShow("E025", "取引先", "他");
                    this.TORIHIKISAKI_CD.Focus();
                }
            }
            else
            {
                // 重複チェック
                this.logic.TorihikisakiCd = inputCd;
                this.DupliUpdateViewCheck(e, false, out catchErr);
            }
        }

        /// <summary>
        /// 採番ボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void SaibanButtonClick(object sender, EventArgs e)
        {
            this.logic.Saiban();
        }

        /// <summary>
        /// 請求取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopySeikyuButtonClick(object sender, EventArgs e)
        {
            bool catchErr = this.logic.CopyToSeikyu();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.SEIKYUU_SOUFU_ADDRESS1);
        }

        /// <summary>
        /// 支払取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopySiharaiButtonClick(object sender, EventArgs e)
        {
            bool catchErr = this.logic.CopyToSiharai();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.SHIHARAI_SOUFU_ADDRESS1);
        }

        /// <summary>
        /// 分類取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopyManiButtonClick(object sender, EventArgs e)
        {
            bool catchErr = this.logic.CopyToMani();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.MANI_HENSOUSAKI_ADDRESS1);
        }

        /// <summary>
        /// 営業担当部署CD変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void EigyouTantouBushoCdValidated(object sender, EventArgs e)
        {
            this.logic.EigyouTantouBushoCdValidated();
        }

        /// <summary>
        /// 営業担当者CD変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void EigyouTantouCdValidated(object sender, EventArgs e)
        {
            this.logic.EigyouTantouCdValidated();
        }

        /// <summary>
        /// 営業担当部署変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EigyouTantouBushoCode_TextChanged(object sender, EventArgs e)
        {
            // 【営業担当者CD】【営業担当者名】をクリアする。
            this.EIGYOU_TANTOU_CD.Text = string.Empty;
            this.EIGYOU_TANTOU_NAME.Text = string.Empty;
        }

        /// <summary>
        /// 営業担当者CDポップアップ後処理
        /// </summary>
        public virtual void EigyouTantouCdAfterPopup()
        {
            this.logic.EigyouTantouCdValidated();
        }

        /// <summary>
        /// 振込銀行CD変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FurikomiBankCdValidated(object sender, EventArgs e)
        {
            this.logic.FurikomiBankCdValidated();
        }

        /// <summary>
        /// 振込銀行CD2変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FurikomiBankCd2Validated(object sender, EventArgs e)
        {
            this.logic.FurikomiBankCd2Validated();
        }

        /// <summary>
        /// 振込銀行CD3変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FurikomiBankCd3Validated(object sender, EventArgs e)
        {
            this.logic.FurikomiBankCd3Validated();
        }


        /// <summary>
        /// 入金先CDポップアップ後処理
        /// </summary>
        public virtual void NyuukinsakiCdAfterPopup()
        {
            this.logic.NyuukinsakiCdValidated();
        }

        /// <summary>
        /// 開始売掛残高の入力終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KAISHI_URIKAKE_ZANDAKA_Validated(object sender, EventArgs e)
        {
            this.logic.SetZandakaFormat(this.KAISHI_URIKAKE_ZANDAKA.Text, this.KAISHI_URIKAKE_ZANDAKA);
        }

        /// <summary>
        /// 開始買掛残高の入力終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KAISHI_KAIKAKE_ZANDAKA_Validated(object sender, EventArgs e)
        {
            this.logic.SetZandakaFormat(this.KAISHI_KAIKAKE_ZANDAKA.Text, this.KAISHI_KAIKAKE_ZANDAKA);
        }

        /// <summary>
        /// ウインドウタイプ設定処理
        /// </summary>
        /// <param name="type"></param>
        public void SetWindowType(WINDOW_TYPE type)
        {
            base.WindowType = type;
            base.HeaderFormInit();
        }

        /// <summary>
        /// 請求先締日1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.SHIMEBI1.Text))
            {
                this.SHIMEBI2.Enabled = false;
                this.SHIMEBI2.Text = string.Empty;
                this.SHIMEBI3.Enabled = false;
                this.SHIMEBI3.Text = string.Empty;
            }
            else
            {
                this.SHIMEBI2.Enabled = true;
            }
        }

        /// <summary>
        /// 請求先締日1変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SHIMEBI2.Text) && this.SHIMEBI1.Text.Equals(this.SHIMEBI2.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日2");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SHIMEBI3.Text) && this.SHIMEBI1.Text.Equals(this.SHIMEBI3.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日3");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 請求先締日2変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.SHIMEBI2.Text))
            {
                this.SHIMEBI3.Enabled = false;
                this.SHIMEBI3.Text = string.Empty;
            }
            else
            {
                this.SHIMEBI3.Enabled = true;
            }
        }

        /// <summary>
        /// 請求先締日2変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SHIMEBI1.Text) && this.SHIMEBI2.Text.Equals(this.SHIMEBI1.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日1");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SHIMEBI3.Text) && this.SHIMEBI2.Text.Equals(this.SHIMEBI3.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日3");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 請求先締日3変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SHIMEBI1.Text) && this.SHIMEBI3.Text.Equals(this.SHIMEBI1.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日1");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SHIMEBI2.Text) && this.SHIMEBI3.Text.Equals(this.SHIMEBI2.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日2");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払先締日1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI1.Text))
            {
                this.SHIHARAI_SHIMEBI2.Enabled = false;
                this.SHIHARAI_SHIMEBI2.Text = string.Empty;
                this.SHIHARAI_SHIMEBI3.Enabled = false;
                this.SHIHARAI_SHIMEBI3.Text = string.Empty;
            }
            else
            {
                this.SHIHARAI_SHIMEBI2.Enabled = true;
            }
        }

        /// <summary>
        /// 支払先締日1変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI2.Text) && this.SHIHARAI_SHIMEBI1.Text.Equals(this.SHIHARAI_SHIMEBI2.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日2");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI3.Text) && this.SHIHARAI_SHIMEBI1.Text.Equals(this.SHIHARAI_SHIMEBI3.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日3");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払先締日2変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI2.Text))
            {
                this.SHIHARAI_SHIMEBI3.Enabled = false;
                this.SHIHARAI_SHIMEBI3.Text = string.Empty;
            }
            else
            {
                this.SHIHARAI_SHIMEBI3.Enabled = true;
            }
        }

        /// <summary>
        /// 支払先締日2変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI1.Text) && this.SHIHARAI_SHIMEBI2.Text.Equals(this.SHIHARAI_SHIMEBI1.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日1");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI3.Text) && this.SHIHARAI_SHIMEBI2.Text.Equals(this.SHIHARAI_SHIMEBI3.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日3");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払先締日3変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI1.Text) && this.SHIHARAI_SHIMEBI3.Text.Equals(this.SHIHARAI_SHIMEBI1.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日1");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI2.Text) && this.SHIHARAI_SHIMEBI3.Text.Equals(this.SHIHARAI_SHIMEBI2.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日2");
                e.Cancel = true;
            }
        }

        private string beforeFURIKOMI_BANK_CD = string.Empty;
        private string beforeFURIKOMI_BANK_CD_2 = string.Empty;
        private string beforeFURIKOMI_BANK_CD_3 = string.Empty;


        /// <summary>
        /// 銀行検索ポップアップ実行後処理
        /// </summary>
        public void FURIKOMI_BANK_CD_PopupAfterExecuteMethod()
        {
            // 検索ポップアップからCDを設定すると紐付く項目がクリアされないのでここで対策
            if (!this.beforeFURIKOMI_BANK_CD.Equals(this.FURIKOMI_BANK_CD.Text))
            {
                this.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                this.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                this.KOUZA_SHURUI.Text = string.Empty;
                this.KOUZA_NO.Text = string.Empty;
                this.KOUZA_NAME.Text = string.Empty;
                this.previousBankShitenCd = string.Empty;
            }
        }

        /// <summary>
        /// 銀行2検索ポップアップ実行後処理
        /// </summary>
        public void FURIKOMI_BANK_CD_2_PopupAfterExecuteMethod()
        {
            // 検索ポップアップからCDを設定すると紐付く項目がクリアされないのでここで対策
            if (!this.beforeFURIKOMI_BANK_CD_2.Equals(this.FURIKOMI_BANK_CD_2.Text))
            {
                this.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                this.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;
                this.KOUZA_SHURUI_2.Text = string.Empty;
                this.KOUZA_NO_2.Text = string.Empty;
                this.KOUZA_NAME_2.Text = string.Empty;
                this.previousBankShitenCd_2 = string.Empty;
            }
        }

        /// <summary>
        /// 銀行3検索ポップアップ実行後処理
        /// </summary>
        public void FURIKOMI_BANK_CD_3_PopupAfterExecuteMethod()
        {
            // 検索ポップアップからCDを設定すると紐付く項目がクリアされないのでここで対策
            if (!this.beforeFURIKOMI_BANK_CD_3.Equals(this.FURIKOMI_BANK_CD_3.Text))
            {
                this.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                this.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;
                this.KOUZA_SHURUI_3.Text = string.Empty;
                this.KOUZA_NO_3.Text = string.Empty;
                this.KOUZA_NAME_3.Text = string.Empty;
                this.previousBankShitenCd_3 = string.Empty;
            }
        }

        /// <summary>
        /// 銀行検索ポップアップ実行前処理
        /// </summary>
        public void FURIKOMI_BANK_CD_PopupBeforeExecuteMethod()
        {
            this.beforeFURIKOMI_BANK_CD = this.FURIKOMI_BANK_CD.Text;
        }

        /// <summary>
        /// 銀行2検索ポップアップ実行前処理
        /// </summary>
        public void FURIKOMI_BANK_CD_2_PopupBeforeExecuteMethod()
        {
            this.beforeFURIKOMI_BANK_CD_2 = this.FURIKOMI_BANK_CD_2.Text;
        }

        /// <summary>
        /// 銀行3検索ポップアップ実行前処理
        /// </summary>
        public void FURIKOMI_BANK_CD_3_PopupBeforeExecuteMethod()
        {
            this.beforeFURIKOMI_BANK_CD_3 = this.FURIKOMI_BANK_CD_3.Text;
        }

        /// <summary>
        /// 入金先区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NYUUKINSAKI_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.NYUUKINSAKI_KBN.Text.Equals("1"))
            {
                this.NYUUKINSAKI_CD.Enabled = false;
                this.NYUUKINSAKI_SEARCH.Enabled = false;
                this.NYUUKINSAKI_CD.Text = string.Empty;
                this.NYUUKINSAKI_NAME1.Text = string.Empty;
                this.NYUUKINSAKI_NAME2.Text = string.Empty;
            }
            else
            {
                this.NYUUKINSAKI_CD.Enabled = true;
                this.NYUUKINSAKI_SEARCH.Enabled = true;
            }
        }

        /// <summary>
        /// 請求情報1取引区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeTorihikiKbn(true);

            // Begin: LANDUONG - 20220209 - refs#160050
            logic.ChangeOutputDensiSeikyushoAndRakurakuKbn();
            // End: LANDUONG - 20220209 - refs#160050
        }

        /// <summary>
        /// 支払情報1取引区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SIHARAI_TORIHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeSiharaiTorihikiKbn();
        }

        /// <summary>
        /// マニ返送先区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKbn_CheckedChanged(object sender, EventArgs e)
        {

            if (!this.MANI_HENSOUSAKI_KBN.Checked)
            {
                this.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                this.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                this.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                this.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                this.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                this.MANI_HENSOUSAKI_POST.Text = string.Empty;
                this.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                this.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                this.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                this.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
            }
            this.logic.ManiCheckOffCheck();
        }

        /// <summary>
        /// 営業担当部署変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.isError && this.EIGYOU_TANTOU_CD.Text.Equals(this.PreviousValue))
            {
                return;
            }

            this.logic.isError = false;
            if (!this.logic.EigyouTantouCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 取引中止業者指定変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKI_STOP_TextChanged(object sender, EventArgs e)
        {
            if (base.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.logic.TorihikiStopIchiran();
            }
        }

        /// <summary>
        /// 業者一覧ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ICHIRAN_CellDoubleClick(object sender, CellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.logic.ShowGyoushaWindow();
        }

        /// <summary>
        /// 業者一覧キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ICHIRAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.logic.ShowGyoushaWindow();
            }
        }

        /// <summary>
        /// 業者一覧フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ICHIRAN_Enter(object sender, EventArgs e)
        {
            this.KeyPreview = false;
        }

        /// <summary>
        /// 業者一覧フォーカス乖離処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ICHIRAN_Leave(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        /// <summary>
        /// 請求書式1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOSHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            // 請求書式明細区分の制限処理
            this.logic.LimitSeikyuuShoshikiMeisaiKbn();
        }

        /// <summary>
        /// 支払書式1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHOSHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            // 支払書式明細区分の制限処理
            this.logic.LimitShiharaiShoshikiMeisaiKbn();
        }

        /// <summary>
        /// 請求拠点印字区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SEIKYUU_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeSeikyuuKyotenPrintKbn();
        }

        /// <summary>
        /// 支払拠点印字区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeShiharaiKyotenPrintKbn();
        }

        /// <summary>
        /// 請求税計算区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            // 請求税区分の制限処理
            this.logic.LimitSeikyuuZeiKbn();
        }

        /// <summary>
        /// 支払税計算区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            // 支払税区分の制限処理
            this.logic.LimitShiharaiZeiKbn();
        }

        /// <summary>
        /// 請求書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.isError = false;
            bool catchErr = false;
            if (!this.logic.SeikyuuKyotenCdValidated(out catchErr))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.isError = false;
            bool catchErr = false;
            if (!this.logic.ShiharaiKyotenCdValidated(out catchErr))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 営業担当部署の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOU_BUSHO_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.isError = false;
            if (!this.logic.BushoCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 振込銀行支店CDのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void FURIKOMI_BANK_SHITEN_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURIKOMI_BANK_CD.Text;
            var bankShitenCd = this.FURIKOMI_BANK_SHITEN_CD.Text;

            //20150706 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm start
            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURIKOMI_BANK_SHITEN_CD.Focus();
                return;
            }
            if (this.previousBankShitenCd != bankShitenCd)
            {
                this.isBankShitenPopup = false;
            }
            //20150706 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm end

            bool catchErr = false;
            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.FURIKOMI_BANK_SHITEN_NAME.Text = String.Empty;
                    this.KOUZA_SHURUI.Text = String.Empty;
                    this.KOUZA_NO.Text = String.Empty;
                    this.KOUZA_NAME.Text = String.Empty;

                    this.previousBankShitenCd = String.Empty;

                    this.isBankShitenPopup = false;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURIKOMI_BANK_SHITEN_NAME.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd = bankShitenCd;

                    this.isBankShitenPopup = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup && this.previousBankShitenCd != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.FURIKOMI_BANK_SHITEN_CD);
                        this.isBankShitenPopup = true;

                        e.Cancel = true;
                    }
                    else
                    {
                        this.isBankShitenPopup = false;
                    }

                    this.previousBankShitenCd = bankShitenCd;
                }
            }
            else
            {
                this.FURIKOMI_BANK_SHITEN_NAME.Text = String.Empty;
                this.KOUZA_SHURUI.Text = String.Empty;
                this.KOUZA_NO.Text = String.Empty;
                this.KOUZA_NAME.Text = String.Empty;

                this.previousBankShitenCd = String.Empty;

                this.isBankShitenPopup = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 振込銀行支店CD2のバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void FURIKOMI_BANK_SHITEN_CD_2_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURIKOMI_BANK_CD_2.Text;
            var bankShitenCd = this.FURIKOMI_BANK_SHITEN_CD_2.Text;

            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURIKOMI_BANK_SHITEN_CD_2.Focus();
                return;
            }
            if (this.previousBankShitenCd_2 != bankShitenCd)
            {
                this.isBankShitenPopup_2 = false;
            }

            bool catchErr = false;
            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.FURIKOMI_BANK_SHITEN_NAME_2.Text = String.Empty;
                    this.KOUZA_SHURUI_2.Text = String.Empty;
                    this.KOUZA_NO_2.Text = String.Empty;
                    this.KOUZA_NAME_2.Text = String.Empty;

                    this.previousBankShitenCd_2 = String.Empty;

                    this.isBankShitenPopup_2 = false;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURIKOMI_BANK_SHITEN_NAME_2.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI_2.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO_2.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME_2.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd_2 = bankShitenCd;

                    this.isBankShitenPopup_2 = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup_2 && this.previousBankShitenCd_2 != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.FURIKOMI_BANK_SHITEN_CD_2);
                        this.isBankShitenPopup_2 = true;

                        e.Cancel = true;
                    }
                    else
                    {
                        this.isBankShitenPopup_2 = false;
                    }

                    this.previousBankShitenCd_2 = bankShitenCd;
                }
            }
            else
            {
                this.FURIKOMI_BANK_SHITEN_NAME_2.Text = String.Empty;
                this.KOUZA_SHURUI_2.Text = String.Empty;
                this.KOUZA_NO_2.Text = String.Empty;
                this.KOUZA_NAME_2.Text = String.Empty;

                this.previousBankShitenCd_2 = String.Empty;

                this.isBankShitenPopup_2 = false;
            }

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 振込銀行支店CD3のバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void FURIKOMI_BANK_SHITEN_CD_3_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURIKOMI_BANK_CD_3.Text;
            var bankShitenCd = this.FURIKOMI_BANK_SHITEN_CD_3.Text;

            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURIKOMI_BANK_SHITEN_CD_3.Focus();
                return;
            }
            if (this.previousBankShitenCd_3 != bankShitenCd)
            {
                this.isBankShitenPopup_3 = false;
            }

            bool catchErr = false;
            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.FURIKOMI_BANK_SHITEN_NAME_3.Text = String.Empty;
                    this.KOUZA_SHURUI_3.Text = String.Empty;
                    this.KOUZA_NO_3.Text = String.Empty;
                    this.KOUZA_NAME_3.Text = String.Empty;

                    this.previousBankShitenCd_3 = String.Empty;

                    this.isBankShitenPopup_3 = false;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURIKOMI_BANK_SHITEN_NAME_3.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI_3.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO_3.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME_3.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd_3 = bankShitenCd;

                    this.isBankShitenPopup_3 = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup_3 && this.previousBankShitenCd_3 != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.FURIKOMI_BANK_SHITEN_CD_3);
                        this.isBankShitenPopup_3 = true;

                        e.Cancel = true;
                    }
                    else
                    {
                        this.isBankShitenPopup_3 = false;
                    }

                    this.previousBankShitenCd_3 = bankShitenCd;
                }
            }
            else
            {
                this.FURIKOMI_BANK_SHITEN_NAME_3.Text = String.Empty;
                this.KOUZA_SHURUI_3.Text = String.Empty;
                this.KOUZA_NO_3.Text = String.Empty;
                this.KOUZA_NAME_3.Text = String.Empty;

                this.previousBankShitenCd_3 = String.Empty;

                this.isBankShitenPopup_3 = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 振込銀行支店選択ポップアップボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void FURIKOMI_BANK_SHITEN_SEARCH_MouseClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.isBankShitenPopup = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 振込銀行支店選択ポップアップを閉じたときに処理します
        /// </summary>
        public void FURIKOMI_BANK_SHITEN_CD_PopupAfter()
        {
            LogUtility.DebugMethodStart();

            this.isBankShitenPopup = true;
            this.previousBankShitenCd = this.FURIKOMI_BANK_SHITEN_CD.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 振込銀行支店2選択ポップアップを閉じたときに処理します
        /// </summary>
        public void FURIKOMI_BANK_SHITEN_CD_2_PopupAfter()
        {
            LogUtility.DebugMethodStart();

            this.isBankShitenPopup_2 = true;
            this.previousBankShitenCd_2 = this.FURIKOMI_BANK_SHITEN_CD_2.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 振込銀行支店3選択ポップアップを閉じたときに処理します
        /// </summary>
        public void FURIKOMI_BANK_SHITEN_CD_3_PopupAfter()
        {
            LogUtility.DebugMethodStart();

            this.isBankShitenPopup_3 = true;
            this.previousBankShitenCd_3 = this.FURIKOMI_BANK_SHITEN_CD_3.Text;

            LogUtility.DebugMethodEnd();
        }

        internal string ShinseiTorihikisakiCd { get; set; }

        internal string ShinseiHikiaiTorihikisakiCd { get; set; }

        /// <summary>電子申請システムID</summary>
        internal long DenshiShinseiSystemId { get; set; }

        /// <summary>電子申請SEQ</summary>
        internal int DenshiShinseiSeq { get; set; }

        // VUNGUYEN 20150525 #1294 START
        private void TEKIYOU_BEGIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                EIGYOU_TANTOU_CD.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                CHUUSHI_RIYUU1.Focus();
            }
        }
        // VUNGUYEN 20150525 #1294 END

        private void MANI_HENSOUSAKI_THIS_ADDRESS_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeManiHensousakiAddKbn();
        }

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 請求情報1出力区分CD変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OUTPUT_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeOutputKbn();
        }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        //thongh 2016/03/31 #16765 start
        /// <summary>
        /// 振込人名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FURIKOMI_NAME_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox textbox = (CustomTextBox)sender;
            if (!this.HanKatakanaCheck(textbox))
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 半角カタカナ形式かチェックする
        /// </summary>
        public bool HanKatakanaCheck(CustomTextBox CheckControl)
        {
            bool IsKatakanaFlagr = true;

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return IsKatakanaFlagr;
            }

            //半角カタカナか調べる 160026
            //スペース、数字、英小文字、英大文字、記号「()」「,」「.」「/」「-」、カタカナ
            bool IsKatakanaFlag = System.Text.RegularExpressions.Regex.IsMatch(
                CheckControl.GetResultText(),
                 @"^[ 0-9a-zA-Z()\\,./｢｣\-\uFF66-\uFF9F]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!IsKatakanaFlag)
            {
                var itemName = CheckControl.DisplayItemName;
                MessageBoxShowLogic message = new MessageBoxShowLogic();
                message.MessageBoxShow("E014", itemName);
            }
            return IsKatakanaFlag;
        }
        //thongh 2016/03/31 #16765 end

        /// <summary>
        /// 取引先敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 取引先敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_SOUFU_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.SEIKYUU_SOUFU_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_SOUFU_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.SEIKYUU_SOUFU_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SOUFU_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.SHIHARAI_SOUFU_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SOUFU_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.SHIHARAI_SOUFU_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANI_HENSOUSAKI_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.MANI_HENSOUSAKI_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANI_HENSOUSAKI_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.MANI_HENSOUSAKI_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 取引先敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 取引先敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_SOUFU_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SEIKYUU_SOUFU_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_SOUFU_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SEIKYUU_SOUFU_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SOUFU_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SHIHARAI_SOUFU_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SOUFU_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SHIHARAI_SOUFU_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANI_HENSOUSAKI_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.MANI_HENSOUSAKI_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANI_HENSOUSAKI_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.MANI_HENSOUSAKI_KEISHOU2.DroppedDown = false;
        }

        //Begin: LANDUONG - 20220209 - refs#160050
        internal void RAKURAKU_SAIBAN_BUTTON_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.SaibanRakurakuCode();

            LogUtility.DebugMethodEnd();
        }
        internal Control[] GetControl_Seikyuu()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);

            return allControl.ToArray();
        }
        //End: LANDUONG - 20220209 - refs#160050

        //160026 S
        internal void KAISHUU_BETSU_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.KAISHUU_BETSU_KBN.Text == "2")
            {
                this.KAISHUU_MONTH.Text = string.Empty;
                this.KAISHUU_MONTH.Enabled = false;
                this.KAISHUU_MONTH_1.Enabled = false;
                this.KAISHUU_MONTH_2.Enabled = false;
                this.KAISHUU_MONTH_3.Enabled = false;
                this.KAISHUU_MONTH_4.Enabled = false;
                this.KAISHUU_MONTH_5.Enabled = false;
                this.KAISHUU_MONTH_6.Enabled = false;
                this.KAISHUU_MONTH_7.Enabled = false;
                this.KAISHUU_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.KAISHUU_BETSU_NICHIGO.Enabled = true;
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_NICHIGO.IsNull)
                {
                    this.KAISHUU_BETSU_NICHIGO.Text = this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_NICHIGO.ToString();
                }
                this.KAISHUU_DAY.Text = string.Empty;//DAT 2022/04/29 #162931
                this.KAISHUU_DAY.Enabled = false;//DAT 2022/04/29 #162931
            }
            if (this.KAISHUU_BETSU_KBN.Text == "1")
            {
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_MONTH.IsNull)
                {
                    this.KAISHUU_MONTH.Text = this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_MONTH.ToString();
                }
                this.KAISHUU_MONTH.Enabled = true;
                this.KAISHUU_MONTH_1.Enabled = true;
                this.KAISHUU_MONTH_2.Enabled = true;
                this.KAISHUU_MONTH_3.Enabled = true;
                this.KAISHUU_MONTH_4.Enabled = true;
                this.KAISHUU_MONTH_5.Enabled = true;
                this.KAISHUU_MONTH_6.Enabled = true;
                this.KAISHUU_MONTH_7.Enabled = true;
                this.KAISHUU_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.KAISHUU_BETSU_NICHIGO.Text = string.Empty;
                this.KAISHUU_BETSU_NICHIGO.Enabled = false;
                //DAT 2022/04/29 #162931 S
                this.KAISHUU_DAY.Enabled = true;
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_DAY.IsNull)
                {
                    this.KAISHUU_DAY.Text = this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_DAY.ToString();
                }
                //DAT 2022/04/29 #162931 E
            }
        }

        internal void SHIHARAI_BETSU_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.SHIHARAI_BETSU_KBN.Text == "2")
            {
                this.SHIHARAI_MONTH.Text = string.Empty;
                this.SHIHARAI_MONTH.Enabled = false;
                this.SHIHARAI_MONTH_1.Enabled = false;
                this.SHIHARAI_MONTH_2.Enabled = false;
                this.SHIHARAI_MONTH_3.Enabled = false;
                this.SHIHARAI_MONTH_4.Enabled = false;
                this.SHIHARAI_MONTH_5.Enabled = false;
                this.SHIHARAI_MONTH_6.Enabled = false;
                this.SHIHARAI_MONTH_7.Enabled = false;
                this.SHIHARAI_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.SHIHARAI_BETSU_NICHIGO.Enabled = true;
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_NICHIGO.IsNull)
                {
                    this.SHIHARAI_BETSU_NICHIGO.Text = this.logic.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_NICHIGO.ToString();
                }
                this.SHIHARAI_DAY.Text = string.Empty;//DAT 2022/04/29 #162931
                this.SHIHARAI_DAY.Enabled = false;//DAT 2022/04/29 #162931
            }
            if (this.SHIHARAI_BETSU_KBN.Text == "1")
            {
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SHIHARAI_MONTH.IsNull)
                {
                    this.SHIHARAI_MONTH.Text = this.logic.entitysM_SYS_INFO.SHIHARAI_MONTH.ToString();
                }
                this.SHIHARAI_MONTH.Enabled = true;
                this.SHIHARAI_MONTH_1.Enabled = true;
                this.SHIHARAI_MONTH_2.Enabled = true;
                this.SHIHARAI_MONTH_3.Enabled = true;
                this.SHIHARAI_MONTH_4.Enabled = true;
                this.SHIHARAI_MONTH_5.Enabled = true;
                this.SHIHARAI_MONTH_6.Enabled = true;
                this.SHIHARAI_MONTH_7.Enabled = true;
                this.SHIHARAI_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.SHIHARAI_BETSU_NICHIGO.Text = string.Empty;
                this.SHIHARAI_BETSU_NICHIGO.Enabled = false;
                //DAT 2022/04/29 #162931 S
                this.SHIHARAI_DAY.Enabled = true;
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SHIHARAI_DAY.IsNull)
                {
                    this.SHIHARAI_DAY.Text = this.logic.entitysM_SYS_INFO.SHIHARAI_DAY.ToString();
                }
                //DAT 2022/04/29 #162931 E
            }
        }
        /// <summary>
        /// 振込元銀行検索ポップアップ実行後処理
        /// </summary>
        public void FURI_KOMI_MOTO_BANK_CD_PopupAfterExecuteMethod()
        {
            // 検索ポップアップからCDを設定すると紐付く項目がクリアされないのでここで対策
            if (!this.beforeFURIKOMI_BANK_MOTO_CD.Equals(this.FURI_KOMI_MOTO_BANK_CD.Text))
            {
                this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
                this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                this.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                this.FURI_KOMI_MOTO_NO.Text = string.Empty;
                this.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                this.previousBankShitenMotoCd = string.Empty;
            }
        }
        /// <summary>
        /// 振込元銀行検索ポップアップ実行前処理
        /// </summary>
        public void FURI_KOMI_MOTO_BANK_CD_PopupBeforeExecuteMethod()
        {
            this.beforeFURIKOMI_BANK_MOTO_CD = this.FURI_KOMI_MOTO_BANK_CD.Text;
        }
        /// <summary>
        /// 振込元銀行のバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void FURI_KOMI_MOTO_BANK_SHITTEN_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURI_KOMI_MOTO_BANK_CD.Text;
            var bankShitenCd = this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text;

            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Focus();
                return;
            }
            if (this.previousBankShitenMotoCd != bankShitenCd)
            {
                this.isBankShitenPopup_moto = false;
            }

            bool catchErr = false;
            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                    this.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                    this.FURI_KOMI_MOTO_NO.Text = string.Empty;
                    this.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                    this.previousBankShitenMotoCd = string.Empty;

                    this.isBankShitenPopup_moto = false;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.FURI_KOMI_MOTO_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                    this.FURI_KOMI_MOTO_NO.Text = bankShiten.KOUZA_NO;
                    this.FURI_KOMI_MOTO_NAME.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenMotoCd = bankShitenCd;

                    this.isBankShitenPopup_moto = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup_moto && this.previousBankShitenMotoCd != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.FURI_KOMI_MOTO_BANK_SHITTEN_CD);
                        this.isBankShitenPopup_moto = true;

                        e.Cancel = true;
                    }
                    else
                    {
                        this.isBankShitenPopup_moto = false;
                    }

                    this.previousBankShitenMotoCd = bankShitenCd;
                }
            }
            else
            {
                this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                this.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                this.FURI_KOMI_MOTO_NO.Text = string.Empty;
                this.FURI_KOMI_MOTO_NAME.Text = string.Empty;

                this.previousBankShitenMotoCd = String.Empty;

                this.isBankShitenPopup_moto = false;
            }

            LogUtility.DebugMethodEnd();
        }
        internal virtual void FurikomiMotoBankCdValidated(object sender, EventArgs e)
        {
            this.logic.FurikomiMotoBankCdValidated();
        }
        internal void FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD_Validated(object sender, EventArgs e)
        {
            string shuruiCd = this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text;
            if (string.IsNullOrEmpty(shuruiCd))
            {
                this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = string.Empty;
            }
            else
            {
                if (shuruiCd.Equals("1"))
                {
                    this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = "普通預金";
                }
                else if (shuruiCd.Equals("2"))
                {
                    this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = "当座預金";
                }
                else if (shuruiCd.Equals("9"))
                {
                    this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = "その他";
                }
                else
                {
                    this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = string.Empty;
                }
            }
        }

        private void FURIKOMI_SAKI_BANK_NAME_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox textbox = (CustomTextBox)sender;
            if (!this.HanKatakanaFurisakiBankCheck(textbox))
            {
                e.Cancel = true;
            }
        }

        private void FURIKOMI_SAKI_BANK_SHITEN_NAME_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox textbox = (CustomTextBox)sender;
            if (!this.HanKatakanaFurisakiBankShitenCheck(textbox))
            {
                e.Cancel = true;
            }
        }

        private void FURIKOMI_SAKI_BANK_KOUZA_NAME_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox textbox = (CustomTextBox)sender;
            if (!this.HanKatakanaCheck(textbox))
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 半角カタカナ形式かチェックする
        /// </summary>
        public bool HanKatakanaFurisakiBankCheck(CustomTextBox CheckControl)
        {
            bool IsKatakanaFlagr = true;

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return IsKatakanaFlagr;
            }

            //半角カタカナか調べる
            //スペース、数字、英小文字、英大文字、記号「()」「.」「/」「-」、カタカナ
            bool IsKatakanaFlag = System.Text.RegularExpressions.Regex.IsMatch(
                CheckControl.GetResultText(),
                @"^[ 0-9a-zA-Z()\./\-\uFF67-\uFF9F]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!IsKatakanaFlag)
            {
                var itemName = CheckControl.DisplayItemName;
                MessageBoxShowLogic message = new MessageBoxShowLogic();
                message.MessageBoxShow("E014", itemName);
            }
            return IsKatakanaFlag;
        }
        /// <summary>
        /// 半角カタカナ形式かチェックする
        /// </summary>
        public bool HanKatakanaFurisakiBankShitenCheck(CustomTextBox CheckControl)
        {
            bool IsKatakanaFlagr = true;

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return IsKatakanaFlagr;
            }

            //半角カタカナか調べる
            //スペース、数字、英小文字、英大文字、記号「-」、カタカナ
            bool IsKatakanaFlag = System.Text.RegularExpressions.Regex.IsMatch(
                CheckControl.GetResultText(),
                @"^[ 0-9a-zA-Z()\-\uFF67-\uFF9F]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!IsKatakanaFlag)
            {
                var itemName = CheckControl.DisplayItemName;
                MessageBoxShowLogic message = new MessageBoxShowLogic();
                message.MessageBoxShow("E014", itemName);
            }
            return IsKatakanaFlag;
        }

        internal void FURIKOMI_EXPORT_KBN_TextChanged(object sender, EventArgs e)
        {
            string furikomiKbn = this.FURIKOMI_EXPORT_KBN.Text;
            this.FURIKOMI_SAKI_BANK_CD.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_NAME.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_SHITEN_CD.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_SHITEN_NAME.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_KOUZA_NO.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_KOUZA_NAME.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_CD.Enabled = false;
            this.FURIKOMI_SAKI_BANK_NAME.Enabled = false;
            this.FURIKOMI_SAKI_BANK_SHITEN_CD.Enabled = false;
            this.FURIKOMI_SAKI_BANK_SHITEN_NAME.Enabled = false;
            this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Enabled = false;
            this.FURIKOMI_SAKI_BANK_KOUZA_NO.Enabled = false;
            this.FURIKOMI_SAKI_BANK_KOUZA_NAME.Enabled = false;

            this.TEI_SUU_RYOU_KBN.Text = string.Empty;
            this.FURI_KOMI_MOTO_BANK_CD.Text = string.Empty;
            this.FURI_KOMI_MOTO_BANK_NAME.Text = string.Empty;
            this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
            this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
            this.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
            this.FURI_KOMI_MOTO_NO.Text = string.Empty;
            this.FURI_KOMI_MOTO_NAME.Text = string.Empty;

            this.TEI_SUU_RYOU_KBN.Enabled = false;
            this.TEI_SUU_RYOU_KBN_1.Enabled = false;
            this.TEI_SUU_RYOU_KBN_2.Enabled = false;
            this.FURI_KOMI_MOTO_BANK_CD.Enabled = false;
            this.FURI_KOMI_MOTO_BANK_POPUP.Enabled = false;
            this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Enabled = false;
            this.FURI_KOMI_MOTO_BANK_SHITTEN_POPUP.Enabled = false;
            if (furikomiKbn.Equals("1"))
            {
                this.FURIKOMI_SAKI_BANK_CD.Enabled = true;
                this.FURIKOMI_SAKI_BANK_NAME.Enabled = true;
                this.FURIKOMI_SAKI_BANK_SHITEN_CD.Enabled = true;
                this.FURIKOMI_SAKI_BANK_SHITEN_NAME.Enabled = true;
                this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Enabled = true;
                this.FURIKOMI_SAKI_BANK_KOUZA_NO.Enabled = true;
                this.FURIKOMI_SAKI_BANK_KOUZA_NAME.Enabled = true;

                this.TEI_SUU_RYOU_KBN.Text = "1";
                this.TEI_SUU_RYOU_KBN.Enabled = true;
                this.TEI_SUU_RYOU_KBN_1.Enabled = true;
                this.TEI_SUU_RYOU_KBN_2.Enabled = true;
                this.FURI_KOMI_MOTO_BANK_CD.Enabled = true;
                this.FURI_KOMI_MOTO_BANK_POPUP.Enabled = true;
                this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Enabled = true;
                this.FURI_KOMI_MOTO_BANK_SHITTEN_POPUP.Enabled = true;
                this.logic.SetDefaultValueFromFURIKOMI_BANK_MOTO();
            }
        }
        //160026 E
    }
}
