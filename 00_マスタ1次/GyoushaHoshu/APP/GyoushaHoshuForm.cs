using System;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using GyoushaHoshu.Const;
using GyoushaHoshu.Dao;
using GyoushaHoshu.Logic;
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

namespace GyoushaHoshu.APP
{
    /// <summary>
    /// 業者保守画面
    /// </summary>
    [Implementation]
    public partial class GyoushaHoshuForm : SuperForm
    {
        /// <summary>
        /// 業者保守画面ロジック
        /// </summary>
        private GyoushaHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ(【新規】モード起動時)
        /// </summary>
        public GyoushaHoshuForm()
            : base(WINDOW_ID.M_GYOUSHA, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new GyoushaHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ(【修正】【削除】【複写】モード起動時)
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="gyoushaCd">選択されたデータの入金先CD</param>
        /// <param name="denshiShinseiFlg">True：電子申請で使用 False：電子申請で使用せず</param>
        public GyoushaHoshuForm(WINDOW_TYPE windowType, string gyoushaCd, bool denshiShinseiFlg)
            : base(WINDOW_ID.M_GYOUSHA, windowType)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new GyoushaHoshuLogic(this);

            this.logic.GyoushaCd = gyoushaCd;
            this.logic.denshiShinseiFlg = denshiShinseiFlg;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ（承認済み電子申請一覧からの画面起動時）
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="denshiShinseiFlg">電子申請フラグ</param>
        /// <param name="isFromShouninzumiDenshiShinseiIchiran">承認済み電子申請一覧から起動されたかのフラグ</param>
        public GyoushaHoshuForm(WINDOW_TYPE windowType, string gyoushaCd, bool denshiShinseiFlg, bool isFromShouninzumiDenshiShinseiIchiran, string torihikisakiCd)
            : base(WINDOW_ID.M_GYOUSHA, windowType)
        {
            InitializeComponent();

            this.logic = new GyoushaHoshuLogic(this);

            this.logic.GyoushaCd = gyoushaCd;
            this.logic.denshiShinseiFlg = denshiShinseiFlg;
            this.logic.IsFromShouninzumiDenshiShinseiIchiran = isFromShouninzumiDenshiShinseiIchiran;
            this.logic.TorihikisakiCd = torihikisakiCd;
            if (!string.IsNullOrEmpty(torihikisakiCd))
            {
                this.logic.RenkeiFlg = true;
            }
            else
            {
                this.logic.RenkeiFlg = false;
            }

            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //※※※　何故か、現場分類タブが一度選択されないと修正時に敬称がうまくセットされないので
            //※※※　強制的に一度全タブを選択して戻すようにすることで一旦解決
            TabPage now = this.JOHOU.SelectedTab;
            foreach (TabPage page in this.JOHOU.TabPages)
            {
                this.JOHOU.SelectedTab = page;
            }
            this.JOHOU.SelectedTab = now;
            //※※※　強引な対応ここまで

            this.logic.WindowInit(base.WindowType);

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.JOHOU != null)
            {
                this.JOHOU.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                if (this.GENBA_ICHIRAN != null)
                {
                    this.GENBA_ICHIRAN.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
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
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            // 処理モード変更
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // 取引先連携時に取引先情報コピーを防ぐためフラグを設定
            this.logic.isNotTorihikisakiCopy = true;

            // 画面タイトル変更
            base.HeaderFormInit();

            // 画面初期化
            this.logic.GyoushaCd = string.Empty;
            bool catchErr = this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);

            this.logic.isNotTorihikisakiCopy = false;
        }

        /// <summary>
        /// 【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            // 権限チェック
            // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
            if (r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                // 画面タイトル変更
                base.HeaderFormInit();
                // 画面初期化
                this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
            }
            else if (r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                if (!r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
        /// 一覧画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            this.logic.ShowIchiran();
        }

        /// <summary>
        /// 検索処理(現場)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SearchGenba(object sender, EventArgs e)
        {
            this.logic.TorihikiStopIchiran();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                // 重複チェック
                if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.WindowType)
                {
                    if (!this.DupliUpdateViewCheck(e, true))
                    {
                        return;
                    }
                }

                /// 20141217 Houkakou 「業者入力」の日付チェックを追加する　start
                if (this.logic.DateCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「業者入力」の日付チェックを追加する　end

                // Begin: LANDUONG - 20220214 - refs#160052
                if ((this.logic.denshiSeikyusho && this.logic.denshiSeikyuRaku) || (this.logic.denshiSeikyusho && !this.logic.denshiSeikyuRaku)
                    || (!this.logic.denshiSeikyusho && this.logic.denshiSeikyuRaku))
                {
                    M_TORIHIKISAKI_SEIKYUU queryParam = new M_TORIHIKISAKI_SEIKYUU();
                    queryParam.TORIHIKISAKI_CD = this.TORIHIKISAKI_CD.Text;
                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>().GetDataByCd(queryParam.TORIHIKISAKI_CD);
                    if (seikyuuEntity != null)
                    {
                        if (!seikyuuEntity.SHOSHIKI_KBN.IsNull)
                        {
                            if (seikyuuEntity.OUTPUT_KBN.Value == 2 && seikyuuEntity.SHOSHIKI_KBN.Value == 2)
                            {                                
                                if (base.RegistErrorFlag)
                                {
                                    return;
                                }
                                if (string.IsNullOrWhiteSpace(this.HAKKOUSAKI_CD.Text))
                                {
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    if (msgLogic.MessageBoxShow("C122", "発行先コード") == System.Windows.Forms.DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                            }
                            else if (seikyuuEntity.OUTPUT_KBN.Value == 3 && seikyuuEntity.SHOSHIKI_KBN.Value == 2)
                            {                                
                                if (base.RegistErrorFlag)
                                {
                                    return;
                                }

                                // 楽楽顧客コード = BLANK かつ システム設定.楽楽顧客コード採番方法 = 1.自動採番の場合
                                if (string.IsNullOrWhiteSpace(this.RAKURAKU_CUSTOMER_CD.Text)
                                    && (!this.logic.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN.IsNull && this.logic.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN == 1))
                                {
                                    // 自動採番を実施（取引先CD＋業者CD＋"000000"を設定）
                                    this.RAKURAKU_CUSTOMER_CD.Text = this.TORIHIKISAKI_CD.Text + this.GYOUSHA_CD.Text + "000000";
                                }
                                // 楽楽顧客コード = BLANK かつ システム設定.楽楽顧客コード採番方法 = 2.手動採番の場合
                                else if (string.IsNullOrWhiteSpace(this.RAKURAKU_CUSTOMER_CD.Text)
                                    && (!this.logic.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN.IsNull && this.logic.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN == 2))
                                {
                                    // 確認メッセージを表示
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    if (msgLogic.MessageBoxShow("C122", "楽楽顧客コード") == System.Windows.Forms.DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                            }

                            if (seikyuuEntity.OUTPUT_KBN.Value == 3)
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
                    }
                }
                // End: LANDUONG - 20220214 - refs#160052

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

                bool catchErr = this.logic.CreateGyoushaEntity(false);
                if (catchErr)
                {
                    return;
                }

                string sGyoushaCd = this.logic.GyoushaEntity.GYOUSHA_CD;
                string sGenbaCd = string.Empty;

                if (base.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    // ワークフローから本登録される場合、現場の登録は不要
                    if (!this.logic.IsFromShouninzumiDenshiShinseiIchiran)
                    {
                        if (null == this.logic.genbaEntity || (null != this.logic.genbaEntity && SqlBoolean.False == this.logic.genbaEntity.DELETE_FLG))
                        {
                            // 現場マスタ登録確認
                            var messageLogic = new MessageBoxShowLogic();
                            var dialogResult = DialogResult.No;
                            if (null == this.logic.genbaEntity)
                            {
                                dialogResult = messageLogic.MessageBoxShow("C071");
                                this.logic.IsGenbaAdd = true;
                            }
                            else
                            {
                                dialogResult = messageLogic.MessageBoxShow("C072");
                                this.logic.IsGenbaAdd = false;
                            }
                            if (DialogResult.Yes == dialogResult)
                            {
                                catchErr = this.logic.CreateGenbaEntity();
                                if (catchErr)
                                {
                                    return;
                                }
                                sGenbaCd = this.logic.genbaEntity.GENBA_CD;
                            }
                            else
                            {
                                this.logic.genbaEntity = null;
                            }
                        }
                    }
                }

                if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.WindowType)
                {
                    // 他マスタで使用されているかのチェック
                    if (!this.logic.CheckDelete()) return;

                    // 削除時は確認メッセージを表示（トランザクション内でメッセージを出力したくないので最初に確認する）
                    var result = new MessageBoxShowLogic().MessageBoxShow("C026");
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                // TODO トランザクション部分をロジックに移動する。20140807 おがわ

                var message = String.Empty;
                using (var tran = new Transaction())
                {
                    switch (base.WindowType)
                    {
                        // 新規追加
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            this.logic.Regist(base.RegistErrorFlag);

                            message = "登録";

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
                    if (this.RegistErrorFlag)
                    {
                        return;
                    }

                    if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.WindowType && null != this.logic.LoadHikiaiGyoushaEntity)
                    {
                        // 引合業者をもとに本登録した場合は、引合業者のDELETE_FLGをTrueにする
                        this.logic.LoadHikiaiGyoushaEntity.GYOUSHA_CD_AFTER = this.logic.GyoushaEntity.GYOUSHA_CD;
                        this.logic.LoadHikiaiGyoushaEntity.DELETE_FLG = true;
                        catchErr = this.logic.UpdateHikiaiGyousha(this.logic.LoadHikiaiGyoushaEntity);
                        if (catchErr)
                        {
                            return;
                        }

                        // 引合取引先をもとに本登録した場合は、引合現場の取引先CDを変更する
                        var genbaDao = DaoInitUtility.GetComponent<GenbaDao>();
                        genbaDao.UpdateGenba(this.logic.LoadHikiaiGyoushaEntity.GYOUSHA_CD, this.logic.GyoushaEntity.GYOUSHA_CD);

                        // 引合業者をもとに本登録した場合は、引合現場の業者CDを変更する
                        var hikiaiGenbaDao = DaoInitUtility.GetComponent<IM_HIKIAI_GENBADao>();
                        hikiaiGenbaDao.UpdateHikiaiGyoushaCd(this.logic.LoadHikiaiGyoushaEntity.GYOUSHA_CD, this.logic.GyoushaEntity.GYOUSHA_CD);

                        // 引合業者をもとに本登録した場合は、見積データの業者CDを変更する
                        var mitsumoriEntryDao = DaoInitUtility.GetComponent<IMitsumoriEntryDao>();
                        var mitsumoriEntryKeyEntity = new r_framework.Entity.T_MITSUMORI_ENTRY();
                        mitsumoriEntryKeyEntity.HIKIAI_GYOUSHA_FLG = true;
                        mitsumoriEntryKeyEntity.GYOUSHA_CD = this.logic.LoadHikiaiGyoushaEntity.GYOUSHA_CD;
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
                            entryEntity.GYOUSHA_CD = this.logic.GyoushaEntity.GYOUSHA_CD;
                            entryEntity.HIKIAI_GYOUSHA_FLG = false;
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
                        if (!String.IsNullOrEmpty(this.ShinseiHikiaiGyoushaCd))
                        {
                            denshiShinseiEntryKeyEntity.HIKIAI_GYOUSHA_CD = this.ShinseiHikiaiGyoushaCd;
                        }
                        else
                        {
                            denshiShinseiEntryKeyEntity.GYOUSHA_CD = this.ShinseiGyoushaCd;
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

                    tran.Commit();
                }

                if (this.logic.isRegist)
                {
                    // メッセージ表示
                    new MessageBoxShowLogic().MessageBoxShow("I001", message);
                    this.logic.IsFromShouninzumiDenshiShinseiIchiran = false;

                    if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.WindowType && null != this.logic.LoadHikiaiGyoushaEntity)
                    {
                        // 本登録時は承認済申請一覧を更新
                        FormManager.UpdateForm("G561");
                    }


                    //open genba master
                    if (!string.IsNullOrEmpty(sGenbaCd))
                    {
                        switch (base.WindowType)
                        {
                            // 新規追加
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                this.logic.OpenGenbaForm(WINDOW_TYPE.UPDATE_WINDOW_FLAG, sGyoushaCd, sGenbaCd);
                                break;
                            default:
                                break;
                        }
                    }

                    // 権限チェック
                    if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    {
                        if (r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // 取引先連携時に取引先情報コピーを防ぐためフラグを設定
                            this.logic.isNotTorihikisakiCopy = true;

                            // DB更新後、新規モードで表示
                            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            base.HeaderFormInit();
                            this.logic.TorihikisakiCd = string.Empty;
                            this.logic.RenkeiFlg = false;
                            this.logic.GyoushaCd = string.Empty;
                            this.logic.isRegist = false;
                            this.logic.IsGenbaAdd = false;
                            this.logic.genbaEntity = null;
                            this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);

                            this.logic.isNotTorihikisakiCopy = false;
                        }
                        else
                        {
                            // 新規権限がない場合は画面Close
                            this.FormClose(sender, e);
                        }
                    }
                    else//open mode 参照 after regist
                    {
                        if (r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            // 取引先連携時に取引先情報コピーを防ぐためフラグを設定
                            this.logic.isNotTorihikisakiCopy = true;

                            base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            base.HeaderFormInit();
                            this.logic.TorihikisakiCd = string.Empty;
                            this.logic.RenkeiFlg = false;
                            this.logic.GyoushaCd = sGyoushaCd;
                            this.logic.isRegist = false;
                            this.logic.IsGenbaAdd = false;
                            this.logic.genbaEntity = null;
                            this.logic.WindowInitReference((BusinessBaseForm)this.Parent);

                            //active button F3
                            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
                            parentForm.bt_func3.Enabled = true;
                            this.logic.isNotTorihikisakiCopy = false;
                        }
                        else
                        {
                            this.FormClose(sender, e);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 業者CD重複チェック and 修正モード起動要否チェック
        /// </summary>
        /// <param name="e">イベント</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        private bool DupliUpdateViewCheck(EventArgs e, bool isRegister)
        {
            try
            {
                bool result = false;

                // 業者CDの入力値をゼロパディング
                bool catchErr = false;
                string zeroPadCd = this.logic.ZeroPadding(this.GYOUSHA_CD.Text, out catchErr);
                if (catchErr)
                {
                    return result;
                }

                // 重複チェック
                GyoushaHoshuConstans.GyoushaCdLeaveResult isUpdate = this.logic.DupliCheckGyoushaCd(zeroPadCd, isRegister);

                if (isUpdate == GyoushaHoshuConstans.GyoushaCdLeaveResult.FALSE_ON)
                {
                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正モードで表示する
                        this.logic.GyoushaCd = zeroPadCd;

                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                        // 画面タイトル変更
                        base.HeaderFormInit();

                        this.GYOUSHA_NAME1.Focus();

                        // 修正モードで画面初期化
                        catchErr = this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                        if (catchErr)
                        {
                            return result;
                        }

                        // 画面表示時のエントリを保持
                        this.logic.gyousha = this.logic.daoGyousha.GetDataByCd(zeroPadCd);
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        // 参照モードで表示する
                        this.logic.GyoushaCd = zeroPadCd;

                        base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;

                        // 画面タイトル変更
                        base.HeaderFormInit();

                        this.GYOUSHA_NAME1.Focus();

                        // 参照モードで画面初期化
                        catchErr = this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                        if (catchErr)
                        {
                            return result;
                        }

                        // 画面表示時のエントリを保持
                        this.logic.gyousha = this.logic.daoGyousha.GetDataByCd(zeroPadCd);
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                        this.GYOUSHA_CD.Text = string.Empty;
                        this.GYOUSHA_CD.Focus();
                    }

                    result = true;
                }
                else if (isUpdate != GyoushaHoshuConstans.GyoushaCdLeaveResult.TURE_NONE)
                {
                    // 入力した業者CDが重複した かつ 修正モード未起動の場合
                    this.GYOUSHA_CD.Text = string.Empty;
                    this.logic.genbaEntity = null;
                    this.GYOUSHA_CD.Focus();
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
                LogUtility.Error("DupliUpdateViewCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                this.logic.LogicalDelete();
            }
        }

        /// <summary>
        /// 取引中止の表示を切り替えたら現場一覧を再検索
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
        /// 取消ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel((BusinessBaseForm)this.Parent);
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
        /// ユーザーコードチェック処理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void FormUserRegistCheck(object source, r_framework.Event.RegistCheckEventArgs e)
        {
            this.logic.FormUserRegistCheck(source, e);
        }

        /// <summary>
        /// 取引先有無区分変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_UMU_KBN_TextChanged(object sender, EventArgs e)
        {
            //修正モードの時に取引先の有無が変更されたかをチェック
            if (this.logic.TorihikisakiUmuCheck())
            {
                this.TORIHIKISAKI_UMU_KBN.Text = "1";
                return;
            }
            this.logic.TabDispControl();
        }

        /// <summary>
        /// 営業担当部署変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOU_BUSHO_CD_TextChanged(object sender, EventArgs e)
        {
            // 【営業担当者CD】【営業担当者名】をクリアする。
            this.EIGYOU_TANTOU_CD.Text = string.Empty;
            this.SHAIN_NAME.Text = string.Empty;
        }

        /// <summary>
        /// 業者CDフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Leave(object sender, EventArgs e)
        {
            // 【新規】モードの場合のみチェック処理を行う
            if (base.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                return;
            }

            // 入力された業者CD取得
            string inputCd = this.GYOUSHA_CD.Text;
            if (string.IsNullOrWhiteSpace(inputCd))
            {
                return;
            }

            // 重複チェック
            if (this.logic.IsFromShouninzumiDenshiShinseiIchiran)
            {
                bool catchErr = false;
                string gyoushaCd = this.logic.ZeroPadding(this.GYOUSHA_CD.Text, out catchErr);
                if (catchErr)
                {
                    return;
                }
                var gyoushaData = this.logic.daoGyousha.GetDataByCd(gyoushaCd);
                if (gyoushaData != null && !string.IsNullOrEmpty(gyoushaData.GYOUSHA_CD))
                {
                    new MessageBoxShowLogic().MessageBoxShow("E025", "業者", "他");
                    this.GYOUSHA_CD.Focus();
                    return;
                }
            }
            else
            {
                this.DupliUpdateViewCheck(e, false);
            }
        }

        /// <summary>
        /// 取引先情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            this.logic.TorihikisakiCopy();
        }

        /// <summary>
        /// チェックボックス判定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JISHA_KBN_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.JISHA_KBN.Checked && this.logic.SearchGyousha() > 0)
            {
                // 紐づく現場をチェックする際に、
                // 取引中止区分を条件に含めたくないため一時的に空に設定
                int count = this.logic.SearchGenbaIchiran();
                if (count < 0)
                {
                    return;
                }
                bool errorFlg = false;

                // 紐づく現場いずれかの自社区分がTRUEの場合
                foreach (DataRow rows in this.logic.SearchResultGenba.Rows)
                {
                    if (Convert.ToBoolean(rows["JISHA_KBN"].ToString()))
                    {
                        errorFlg = true;
                    }
                }

                // 不整合エラー
                if (errorFlg)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E098", "自社区分");
                    this.JISHA_KBN.Checked = true;
                }
                else
                {
                    // 紐づく引合現場を検索（自社区分がTRUE）
                    count = this.logic.SearchHikiaiGenbaIchiran();
                    if (count > 0)
                    {
                        errorFlg = true;
                    }
                    else if (count < 0)
                    {
                        return;
                    }
                    if (errorFlg)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E188", "自社区分");
                        this.JISHA_KBN.Checked = true;
                    }
                }
            }
        }

        private void HAISHUTSU_JIGYOUSHA_KBN_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked)
            {
                // 現場マスタデータを取得する
                M_GENBA queryParam = new M_GENBA();
                queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
                queryParam.HAISHUTSU_NIZUMI_GENBA_KBN = true;
                bool catchErr = false;
                if (!this.logic.ManiCheckMsg(queryParam, out catchErr) && !catchErr)
                {
                    this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = true;
                    // 不整合エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E098", "排出事業場/荷積現場");
                    return;
                }
                else if (catchErr)
                {
                    return;
                }
            }

            this.logic.FlgHaishutsuJigyoushaKbn = this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked;
            this.logic.ManiCheckOffCheck();

            if (this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked)
            {
                this.TASYA_EDI.ReadOnly = false;
                this.TASYA_EDI.Text = "2";
            }
            else 
            { 
                this.TASYA_EDI.ReadOnly = true;
                this.TASYA_EDI.Text = string.Empty;
                this.TASYA_EDI_KANJI.Text = string.Empty;
            }
        }

        private void UNPAN_JUTAKUSHA_KBN_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked)
            {
                // 現場マスタデータを取得する
                M_GENBA queryParam = new M_GENBA();
                queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
                queryParam.TSUMIKAEHOKAN_KBN = true;
                bool catchErr = false;
                if (!this.logic.ManiCheckMsg(queryParam, out catchErr) && !catchErr)
                {
                    this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = true;
                    // 不整合エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E098", "積み替え保管");
                    return;
                }
                else if (catchErr)
                {
                    return;
                }
            }

            this.logic.FlgUnpanJutakushaKbn = this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked;
            this.logic.ManiCheckOffCheck();
        }

        private void SHOBUN_JUTAKUSHA_KBN_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked)
            {
                // 現場マスタデータを取得する
                M_GENBA queryParam = new M_GENBA();
                queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
                queryParam.SHOBUN_NIOROSHI_GENBA_KBN = true;
                bool catchErr = false;
                if (!this.logic.ManiCheckMsg(queryParam, out catchErr) && !catchErr)
                {
                    this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = true;
                    // 不整合エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E098", "処分事業場/荷降現場");
                    return;
                }
                else if (catchErr)
                {
                    return;
                }
            }

            this.logic.FlgShobunJutakushaKbn = this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked;
            this.logic.ManiCheckOffCheck();
        }

        private void MANI_HENSOUSAKI_KBN_CheckedChanged(object sender, EventArgs e)
        {
            /*if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.MANI_HENSOUSAKI_KBN.Checked)
            {
                // 現場マスタデータを取得する
                M_GENBA queryParam = new M_GENBA();
                queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
                queryParam.MANI_HENSOUSAKI_KBN = true;
                if (!this.logic.ManiCheckMsg(queryParam))
                {
                    this.MANI_HENSOUSAKI_KBN.Checked = true;
                    // 不整合エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E098", this.MANI_HENSOUSAKI_KBN.DisplayItemName);
                    return;
                }
            }*/

            this.logic.FlgManiHensousakiKbn = this.MANI_HENSOUSAKI_KBN.Checked;
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

        //private void NIOROSHI_GYOUSHA_KBN_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.NIOROSHI_GYOUSHA_KBN.Checked)
        //    {
        //        // 現場マスタデータを取得する
        //        M_GENBA queryParam = new M_GENBA();
        //        queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
        //        queryParam.NIOROSHI_GENBA_KBN = true;
        //        if (!this.logic.ManiCheckMsg(queryParam))
        //        {
        //            this.NIOROSHI_GYOUSHA_KBN.Checked = true;
        //            // 不整合エラー
        //            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //            msgLogic.MessageBoxShow("E098", "荷卸現場");
        //            return;
        //        }
        //    }
        //}

        /// <summary>
        /// 採番ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_gyoushacd_saiban_Click(object sender, EventArgs e)
        {
            // 採番値取得
            this.logic.Saiban();
        }

        /// <summary>
        /// ポップアップ用営業担当部署取得メソッド
        /// </summary>
        public void PopupAfterEigyouTantouCd()
        {
            string cd = this.EIGYOU_TANTOU_CD.Text;
            if (!string.IsNullOrWhiteSpace(cd))
            {
                this.logic.SetBushoData(cd);
            }
        }

        /// <summary>
        /// ポップアップ用営業担当部署取得メソッド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteEigyouTantouCd(object sender, DialogResult rlt)
        {
            if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                return;

            string cd = this.EIGYOU_TANTOU_CD.Text;
            if (!string.IsNullOrWhiteSpace(cd))
            {
                this.logic.SetBushoData(cd);
            }
        }

        /// <summary>
        /// 取引先入力画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_torihikisaki_create_Click(object sender, EventArgs e)
        {
            //TODO：未実装
            // 取引先画面を表示する
            this.logic.ShowTorihikisakiCreate();
        }

        /// <summary>
        /// 現場入力画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_ICHIRAN_CellDoubleClick(object sender, CellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.logic.ShowWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
        }

        private void GENBA_ICHIRAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.logic.ShowWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
        }

        /// <summary>
        /// 請求情報タブ：業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_souhusaki_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            bool catchErr = this.logic.GyoushaInfoCopyFromSeikyuuInfo();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.SEIKYUU_SOUFU_ADDRESS1);
        }

        /// <summary>
        /// 支払情報タブ：業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_shiharai_souhusaki_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            bool catchErr = this.logic.GyoushaInfoCopyFromShiharaiInfo();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.SHIHARAI_SOUFU_ADDRESS1);
        }

        /// <summary>
        /// 業者分類：業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_hensousaki_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            bool catchErr = this.logic.GyoushaInfoCopyFromGyoushaBunrui();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.MANI_HENSOUSAKI_ADDRESS1);
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
            string cd = this.EIGYOU_TANTOU_CD.Text;
            if (!string.IsNullOrWhiteSpace(cd))
            {
                if (!this.logic.SetBushoData(cd))
                {
                    e.Cancel = true;
                }
            }
            else
            {
                this.EIGYOU_TANTOU_CD.Text = string.Empty;
                this.SHAIN_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 都道府県コードテキスト変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_TODOUFUKEN_CD_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.GYOUSHA_TODOUFUKEN_CD.Text) || this.GYOUSHA_TODOUFUKEN_CD.Text.Length >= this.GYOUSHA_TODOUFUKEN_CD.CharactersNumber)
            {
                bool catchErr = this.logic.ChechChiiki(true);
                if (catchErr)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.GYOUSHA_TODOUFUKEN_CD.Text))
                {
                    this.TODOUFUKEN_NAME.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 住所1フォーカス乖離イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ADDRESS1_Leave(object sender, EventArgs e)
        {
            this.logic.ChechChiiki(false);
        }

        /// <summary>
        /// 委託契約入力画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ITAKU_ICHIRAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.logic.ShowWindowItaku(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
        }

        /// <summary>
        /// 委託契約一覧ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ITAKU_ICHIRAN_CellDoubleClick(object sender, CellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.logic.ShowWindowItaku(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
        }

        /// <summary>
        /// 取引先CD検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterTorihikisakiCd()
        {
            //取引先拠点セット
            int count = this.logic.SearchsetTorihikisaki();
            if (count < 0)
            {
                return;
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            // 発行先チェック処理
            if (!this.TORIHIKISAKI_CD.Text.Equals(this.PreviousValue))
            {
                this.logic.HakkousakuAndRakurakuCDCheck();
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            if (this.ActiveControl != null)
            {
                base.PreviousValue = this.ActiveControl.Text;
            }
        }

        /// <summary>
        /// 取引先CD検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteTorihikisakiCd(object sender, DialogResult rlt)
        {
            if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                return;

            //取引先拠点セット
            int count = this.logic.SearchsetTorihikisaki();
            if (count < 0)
            {
                return;
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            // 発行先チェック処理
            if (!this.TORIHIKISAKI_CD.Text.Equals(this.PreviousValue))
            {
                this.logic.HakkousakuAndRakurakuCDCheck();
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            if (this.ActiveControl != null)
            {
                base.PreviousValue = this.ActiveControl.Text;
            }
        }

        /// <summary>
        /// 取引先CDValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            if (!this.logic.isError && this.TORIHIKISAKI_CD.Text.Equals(this.PreviousValue))
            {
                return;
            }
            this.logic.isError = false;
            //取引先拠点セット
            this.logic.SearchsetTorihikisaki();
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            // 発行先チェック処理
            if (!this.logic.isError)
            {
                this.logic.HakkousakuAndRakurakuCDCheck();
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
        }

        /// <summary>
        /// 現場一覧フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_ICHIRAN_Enter(object sender, EventArgs e)
        {
            this.KeyPreview = false;
        }

        /// <summary>
        /// 現場一覧フォーカス乖離処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_ICHIRAN_Leave(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        /// <summary>
        /// 委託契約一覧フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ITAKU_ICHIRAN_Enter(object sender, EventArgs e)
        {
            this.KeyPreview = false;
        }

        /// <summary>
        /// 委託契約一覧フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ITAKU_ICHIRAN_Leave(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        /// <summary>
        /// マニ記載業者チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Gyousha_KBN_3_CheckedChanged(object sender, EventArgs e)
        //{
        //    //新規画面
        //    if (base.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
        //    {
        //        this.GyousyaKbnEnableControl(this.Gyousha_KBN_3.Checked);
        //    }
        //    //修正画面
        //    else if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
        //    {
        //        if (!this.Gyousha_KBN_3.Checked)
        //        {
        //            if (!this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked
        //                && !this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked
        //                && !this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked)
        //            {
        //                this.GyousyaKbnEnableControl(false);
        //            }
        //            else
        //            {
        //                this.Gyousha_KBN_3.Checked = true;
        //                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //                msgLogic.MessageBoxShow("E105");

        //                return;
        //            }
        //        }
        //        else
        //        {
        //            this.GyousyaKbnEnableControl(true);
        //        }
        //    }
        //}

        /// <summary>
        /// マニ記載業者のチェックを判別し、
        /// [マニあり側] もしくは [マニなし側]のどちらか一方を有効にする。
        /// True＝[マニあり側]有効、False＝[マニなし側]有効
        /// </summary>
        /// <param name="gyoushaKbn3Check"></param>
        //private void GyousyaKbnEnableControl(bool gyoushaKbn3Check)
        //{
        //    if (gyoushaKbn3Check)
        //    {
        //        //マニあり側
        //        //this.JISHA_KBN.Enabled = true;
        //        this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = true;
        //        this.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = true;
        //        this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = true;

        //        //マニなし側
        //        this.UNPAN_KAISHA_KBN.Enabled = false;
        //        this.UNPAN_KAISHA_KBN.Checked = false;
        //        this.NIOROSHI_GYOUSHA_KBN.Enabled = false;
        //        this.NIOROSHI_GYOUSHA_KBN.Checked = false;
        //    }
        //    else
        //    {
        //        //マニあり側
        //        //this.JISHA_KBN.Enabled = false;
        //        this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = false;
        //        this.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = false;
        //        this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = false;
        //        //this.JISHA_KBN.Checked = false;
        //        this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
        //        this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
        //        this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;

        //        //マニなし側
        //        this.UNPAN_KAISHA_KBN.Enabled = true;
        //        this.NIOROSHI_GYOUSHA_KBN.Enabled = true;
        //    }
        //}

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
        /// 請求書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.isError = false;
            if (!this.logic.SeikyuuKyotenCdValidated())
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
            if (!this.logic.ShiharaiKyotenCdValidated())
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

        internal string ShinseiGyoushaCd { get; set; }
        internal string ShinseiHikiaiGyoushaCd { get; set; }

        /// <summary>電子申請システムID</summary>
        internal long DenshiShinseiSystemId { get; set; }

        /// <summary>電子申請SEQ</summary>
        internal int DenshiShinseiSeq { get; set; }

        // 20141208 ブン 運搬報告書提出先を追加する start
        private void UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_Validated(object sender, EventArgs e)
        {
            this.logic.isError = false;
            // 運搬報告書提出先拠点セット
            this.logic.SearchsetUpanHoukokushoTeishutsu();
        }

        // 20141208 ブン 運搬報告書提出先を追加する end

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

        private void MANI_HENSOUSAKI_JYOUHOU_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeManiHensousakiJyouhouKbn();
        }

        private void UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.TeishutsuChiikiCdValidating(e);
        }

        /// <summary>
        /// 業者敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.GYOUSHA_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 業者敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.GYOUSHA_KEISHOU2.DroppedDown = false;
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
        /// 業者敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GYOUSHA_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 業者敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GYOUSHA_KEISHOU2.DroppedDown = false;
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

        /// <summary>
        /// 請求情報タブ：取引先情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_seikyuu_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                return;
            }

            this.logic.TorihikisakiCd = this.TORIHIKISAKI_CD.Text;
            bool catchErr = this.logic.TorihikisakiInfoCopyFromSeikyuuInfo();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.SEIKYUU_SOUFU_ADDRESS1);
        }

        /// <summary>
        /// 支払情報タブ：取引先情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_shiharai_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                return;
            }

            this.logic.TorihikisakiCd = this.TORIHIKISAKI_CD.Text;
            bool catchErr = this.logic.TorihikisakiInfoCopyFromShiharaiInfo();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.SHIHARAI_SOUFU_ADDRESS1);
        }

        /// <summary>
        /// 他社EDIの使用有無区分変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TASYA_EDI_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TASYA_EDI.Text)) 
            {
                this.TASYA_EDI_KANJI.Text = string.Empty;
            }
            else if ("1".Equals(this.TASYA_EDI.Text))
            {
                this.TASYA_EDI_KANJI.Text = "使用している";
            }
            else if ("2".Equals(this.TASYA_EDI.Text))
            {
                this.TASYA_EDI_KANJI.Text = "使用していない";
            }
        }

        /// <summary>
        /// BT_TORIHIKISAKI_REFERENCE_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_TORIHIKISAKI_REFERENCE_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E290", "取引先CD");
                return;
            }
            this.logic.OpenTorihikisakiFormReference(this.TORIHIKISAKI_CD.Text);
        }


        // Begin: LANDUONG - 20220214 - refs#160052
        internal void RAKURAKU_SAIBAN_BUTTON_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.SaibanRakurakuCode();

            LogUtility.DebugMethodEnd();
        }
        // End: LANDUONG - 20220214 - refs#160052

        private void URIAGE_GURUPU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.UriageGurupuCdValidating(e);
        }

        private void SHIHARAI_GURUPU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.ShiharaiGurupuCdValidating(e);
        }
    }
}