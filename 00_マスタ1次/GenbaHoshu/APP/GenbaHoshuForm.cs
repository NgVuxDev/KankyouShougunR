using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using GenbaHoshu.Const;
using GenbaHoshu.Dao;
using GenbaHoshu.Entity;
using GenbaHoshu.Logic;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Dao;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;

using r_framework.Dto;

namespace GenbaHoshu.APP
{
    /// <summary>
    /// 現場保守画面
    /// </summary>
    [Implementation]
    public partial class GenbaHoshuForm : SuperForm
    {
        /// <summary>
        /// 現場保守画面ロジック
        /// </summary>
        private GenbaHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        /// <summary>
        /// 明細でエラーが起きたかどうか判断するためのフラグ
        /// </summary>
        internal bool bDetailErrorFlag = false;

        internal string beforeGyoushaCd = string.Empty;

        /// <summary>
        /// コンストラクタ(【新規】モード起動時)
        /// </summary>
        public GenbaHoshuForm()
            : base(WINDOW_ID.M_GENBA, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new GenbaHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ(【修正】【削除】【複写】モード起動時)
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="gyoushaCd">選択されたデータの業者CD</param>
        /// <param name="genbaCd">選択されたデータの現場CD</param>
        /// <param name="denshiShinseiFlg">True：電子申請で使用 False：電子申請で使用せず</param>
        public GenbaHoshuForm(WINDOW_TYPE windowType, string gyoushaCd, string genbaCd, bool denshiShinseiFlg)
            : base(WINDOW_ID.M_GENBA, windowType)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new GenbaHoshuLogic(this);

            this.logic.GyoushaCd = gyoushaCd;
            this.logic.GenbaCd = genbaCd;
            this.logic.denshiShinseiFlg = denshiShinseiFlg;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ（承認済電子申請一覧からの画面起動時）
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="denshiShinseiFlg">電子申請フラグ</param>
        /// <param name="isFromShouninzumiDenshiShinseiIchiran">承認済電子申請一覧から起動されたかのフラグ</param>
        public GenbaHoshuForm(WINDOW_TYPE windowType, string gyoushaCd, string genbaCd, bool denshiShinseiFlg, bool isFromShouninzumiDenshiShinseiIchiran)
            : base(WINDOW_ID.M_GENBA, windowType)
        {
            InitializeComponent();

            this.logic = new GenbaHoshuLogic(this);

            this.logic.GyoushaCd = gyoushaCd;
            this.logic.GenbaCd = genbaCd;
            this.logic.denshiShinseiFlg = denshiShinseiFlg;
            this.logic.IsFromShouninzumiDenshiShinseiIchiran = isFromShouninzumiDenshiShinseiIchiran;

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
            TabPage now = this.ManiHensousakiKeishou2B1.SelectedTab;
            foreach (TabPage page in this.ManiHensousakiKeishou2B1.TabPages)
            {
                this.ManiHensousakiKeishou2B1.SelectedTab = page;
            }
            this.ManiHensousakiKeishou2B1.SelectedTab = now;
            //※※※　強引な対応ここまで

            bool catchErr = this.logic.WindowInit(base.WindowType);
            if (catchErr)
            {
                return;
            }

            // サブファンクションの活性・非活性を切り替えます
            this.logic.SubFunctionEnabledChenge();
        }

        /// <summary>
        /// 画面表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaHoshuForm_Shown(object sender, EventArgs e)
        {
            this.logic.SetIchiranTeikiRowControl();

            this.logic.SetIchiranTsukiRowControl();

            this.logic.SetIchiranSmsRowControl();
            // バリデーションをセットする
            this.logic.SetDynamicEvent();
        }

        /// <summary>
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            // バリデーションを解除する
            this.logic.RemoveDynamicEvent();

            // 処理モード変更
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // 画面タイトル変更
            base.HeaderFormInit();

            // 画面初期化
            this.logic.GyoushaCd = string.Empty;
            this.logic.GenbaCd = string.Empty;
            this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);

            // バリデーションをセットする
            this.logic.SetDynamicEvent();
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
            if (r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // バリデーションを解除する
                this.logic.RemoveDynamicEvent();

                // 処理モード変更
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                // 画面タイトル変更
                base.HeaderFormInit();

                // 画面初期化
                this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);

                // バリデーションをセットする
                this.logic.SetDynamicEvent();
            }
            else if (r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                if (!r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }
                // バリデーションを解除する
                this.logic.RemoveDynamicEvent();

                // 処理モード変更
                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;

                // 画面タイトル変更
                base.HeaderFormInit();

                // 画面初期化
                this.logic.WindowInitReference((BusinessBaseForm)this.Parent);

                // バリデーションをセットする
                this.logic.SetDynamicEvent();
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            //FWで対応できない必須チェックを書く

            // No2267-->
            // アラートの内容の重複確認クリア
            bool catchErr = this.logic.errorMessagesClear();
            if (catchErr)
            {
                return;
            }
            // No2267<--

            // Begin: LANDUONG - 20220215 - refs#160054
            if ((this.logic.denshiSeikyusho && this.logic.denshiSeikyuRaku) || (this.logic.denshiSeikyusho && !this.logic.denshiSeikyuRaku)
                    || (!this.logic.denshiSeikyusho && this.logic.denshiSeikyuRaku))
            {
                M_TORIHIKISAKI_SEIKYUU queryParam = new M_TORIHIKISAKI_SEIKYUU();
                queryParam.TORIHIKISAKI_CD = this.TorihikisakiCode.Text;
                M_TORIHIKISAKI_SEIKYUU seikyuuEntity = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>().GetDataByCd(queryParam.TORIHIKISAKI_CD);
                if (seikyuuEntity != null)
                {
                    if (!seikyuuEntity.SHOSHIKI_KBN.IsNull)
                    {
                        if (seikyuuEntity.OUTPUT_KBN.Value == 2 && seikyuuEntity.SHOSHIKI_KBN.Value == 3)
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
                        else if (seikyuuEntity.OUTPUT_KBN.Value == 3 && seikyuuEntity.SHOSHIKI_KBN.Value == 3)
                        {                            
                            if (base.RegistErrorFlag)
                            {
                                return;
                            }

                            // 楽楽顧客コード = BLANK かつ システム設定.楽楽顧客コード採番方法 = 1.自動採番の場合
                            if (string.IsNullOrWhiteSpace(this.RAKURAKU_CUSTOMER_CD.Text)
                                && (!this.logic.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN.IsNull && this.logic.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN == 1))
                            {
                                // 自動採番を実施（取引先CD＋業者CD＋現場CDを設定）
                                this.RAKURAKU_CUSTOMER_CD.Text = this.TorihikisakiCode.Text + this.GyoushaCode.Text + this.GenbaCode.Text;
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
            // End: LANDUONG - 20220215 - refs#160054

            #region A票～E票返送先区分処理必須チェック

            //【修正】他区分のチェック有り/無しに関わらず
            //マニ返送先区分にチェックがない場合
            //返送先区分によって必須チェックをつける

            #region マージ前削除ソース

            //var messageShowLogic = new MessageBoxShowLogic();
            //string errMsg = "";
            //if (!this.logic.FlgManiHensousakiKbn)
            //{
            //    // [諸口フラグ]が[OFF]のときのみ
            //    if (!this.ShokuchiKbn.Checked)
            //    {
            //        if (this.HensousakiKbn.Text == "1")
            //        {
            //            if (string.IsNullOrEmpty(this.ManiHensousakiTorihikisakiCode.Text))
            //            {
            //                errMsg = "返送先取引先CD";
            //            }

            //            else if (this.HensousakiKbn.Text == "2")
            //            {
            //                if (string.IsNullOrEmpty(this.ManiHensousakiGyoushaCode.Text))
            //                {
            //                    errMsg = "返送先業者CD";
            //                }
            //            }
            //            else if (this.HensousakiKbn.Text == "3")
            //            {
            //                if (string.IsNullOrEmpty(this.ManiHensousakiGyoushaCode.Text))
            //                {
            //                    errMsg = "返送先業者CD";
            //                }
            //                if (string.IsNullOrEmpty(this.ManiHensousakiGenbaCode.Text))
            //                {
            //                    if (errMsg != "")
            //                    {
            //                        errMsg += ",";
            //                    }

            //                    errMsg += "返送先現場CD";
            //                }
            //            }
            //        }
            //    }
            //}

            #endregion

            var messageShowLogic = new MessageBoxShowLogic();
            string errMsg = "";
            string errAllMsg = "";
            //マニありのチェックが1つでもついている場合必須チェックする。
            if (!IsManiAriAllNoChecked())
            {
                if (!(!this.logic.FlgManiHensousakiKbn && !this.HaishutsuKbn.Checked))
                {
                    // [諸口フラグ]が[OFF]のときのみ
                    if (!this.ShokuchiKbn.Checked)
                    {
                        //A票
                        errMsg = RegistCheck("_AHyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }

                        //B1票
                        errMsg = RegistCheck("_B1Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //B2票
                        errMsg = RegistCheck("_B2Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //B4票
                        errMsg = RegistCheck("_B4Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //B6票
                        errMsg = RegistCheck("_B6Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //C1票
                        errMsg = RegistCheck("_C1Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //C2票
                        errMsg = RegistCheck("_C2Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //D票
                        errMsg = RegistCheck("_DHyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //E票
                        errMsg = RegistCheck("_EHyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }

                        errMsg = errAllMsg;
                    }
                }
            }

            #endregion

            //if (this.logic.FlgGyoushaHaishutuKbn)
            //{
            //    if (!this.HaishutsuKbn.Checked)
            //    {
            //        if (errMsg != "")
            //        {
            //            errMsg += ",";
            //        }
            //        errMsg += "排出事業場";
            //    }
            //}

            if (!base.RegistErrorFlag && errMsg != "")
            {
                messageShowLogic.MessageBoxShow("E001", errMsg);
                return;
            }

            // 事業場分類チェック処理（全てのエラーチェックを終えてから行う）
            if (!base.RegistErrorFlag && !this.logic.CheckBunruiKbn())
            {
                var result = messageShowLogic.MessageBoxShow("C061", "排出事業場/荷積現場、積み替え保管、処分事業場/荷降現場、最終処分場のいずれか");
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (!base.RegistErrorFlag)
            {
                /// 20141217 Houkakou 「現場入力」の日付チェックを追加する　start
                if (this.logic.DateCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「現場入力」の日付チェックを追加する　end

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

                catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }

                var message = String.Empty;

                if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.WindowType)
                {
                    // 他マスタで使用されているかのチェック
                    if (!this.logic.CheckDeleteForLogicalDel()) return;

                    // 削除時は確認メッセージを表示（トランザクション内でメッセージを出力したくないので最初に確認する）
                    var result = messageShowLogic.MessageBoxShow("C026");
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                string sGyoushaCd = this.logic.GenbaEntity.GYOUSHA_CD;
                string sGenbaCd = this.logic.GenbaEntity.GENBA_CD;

                // TODO トランザクション部分をロジックに移動する。20140806 おがわ

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
                            // 削除チェック
                            // QN Tue Anh #158986 START
                            this.logic.teikiHinmeiUpdate = false;
                            if (this.logic.IsTeikiHinmeiUpdateCheck())
                            {
                                if (this.errmessage.MessageBoxShowConfirm(Const.GenbaHoshuConstans.MSG_CONF_A_TEIKI_KAISHUU) == DialogResult.Yes)
                                {
                                    this.logic.teikiHinmeiUpdate = true;
                                    this.logic.Update(base.RegistErrorFlag);
                                    message = "更新";
                                }
                                else
                                {
                                    if (this.logic.CheckDelete())
                                    {
                                        this.logic.Update(base.RegistErrorFlag);
                                        message = "更新";
                                    }
                                }
                            }
                            else
                            {
                                if (this.logic.CheckDelete())
                                {
                                    this.logic.Update(base.RegistErrorFlag);
                                    message = "更新";
                                }
                            }
                            // QN Tue Anh #158986 END
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
                    if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.WindowType && null != this.logic.LoadHikiaiGenbaEntity)
                    {
                        // 引合現場をもとに本登録した場合は、引合現場のDELETE_FLGをTrueにする
                        this.logic.LoadHikiaiGenbaEntity.GENBA_CD_AFTER = this.logic.GenbaEntity.GENBA_CD;
                        this.logic.LoadHikiaiGenbaEntity.DELETE_FLG = true;
                        this.logic.UpdateHikiaiGenba(this.logic.LoadHikiaiGenbaEntity);

                        // 引合現場をもとに本登録した場合は、
                        // 同じ業者CD、同じ引合業者フラグ、同じ現場CD、引合現場フラグがTrueの見積データの現場CDを変更する
                        var mitsumoriEntryDao = DaoInitUtility.GetComponent<IMitsumoriEntryDao>();
                        var mitsumoriEntryKeyEntity = new r_framework.Entity.T_MITSUMORI_ENTRY();
                        mitsumoriEntryKeyEntity.HIKIAI_GYOUSHA_FLG = this.logic.LoadHikiaiGenbaEntity.HIKIAI_GYOUSHA_USE_FLG;
                        mitsumoriEntryKeyEntity.GYOUSHA_CD = this.logic.GenbaEntity.GYOUSHA_CD;
                        mitsumoriEntryKeyEntity.HIKIAI_GENBA_FLG = true;
                        mitsumoriEntryKeyEntity.GENBA_CD = this.logic.GenbaEntity.GENBA_CD;
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
                            entryEntity.GENBA_CD = this.logic.GenbaEntity.GENBA_CD;
                            entryEntity.HIKIAI_GENBA_FLG = false;
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

                    // 電子申請から本登録した場合は、電子申請のステータスを変更する
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
                        if (!String.IsNullOrEmpty(this.ShinseiHikiaiGenbaCd))
                        {
                            denshiShinseiEntryKeyEntity.HIKIAI_GENBA_CD = this.ShinseiHikiaiGenbaCd;
                        }
                        else
                        {
                            denshiShinseiEntryKeyEntity.GENBA_CD = this.ShinseiGenbaCd;
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
                    messageShowLogic.MessageBoxShow("I001", message);
                    this.logic.IsFromShouninzumiDenshiShinseiIchiran = false;

                    // バリデーションを解除する
                    this.logic.RemoveDynamicEvent();

                    if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.WindowType && null != this.logic.LoadHikiaiGenbaEntity)
                    {
                        // 本登録時は承認済申請一覧を更新
                        FormManager.UpdateForm("G561");
                    }

                    if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    {
                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // DB更新後、新規モードで表示
                            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            base.HeaderFormInit();
                            this.logic.GyoushaCd = string.Empty;
                            this.logic.GenbaCd = string.Empty;
                            this.logic.isRegist = false;
                            this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);

                            // バリデーションをセットする
                            this.logic.SetDynamicEvent();
                        }
                        else
                        {
                            // 新規権限がない場合は画面Close
                            this.FormClose(sender, e);
                        }
                    }
                    else//open mode 参照 after regist
                    {
                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            base.HeaderFormInit();
                            this.logic.GyoushaCd = sGyoushaCd;
                            this.logic.GenbaCd = sGenbaCd;
                            this.logic.isRegist = false;
                            this.logic.WindowInitReference((BusinessBaseForm)this.Parent);

                            //active button F3
                            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
                            parentForm.bt_func3.Enabled = true;
                            // バリデーションをセットする
                            this.logic.SetDynamicEvent();
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
        /// 現場CD重複チェック and 修正モード起動要否チェック
        /// </summary>
        /// <param name="e">イベント</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        private bool DupliUpdateViewCheck(EventArgs e, bool isRegister, out bool catchErr)
        {
            try
            {
                bool result = false;
                catchErr = false;

                // 現場CDの入力値をゼロパディング
                string zeroPadCd = this.logic.ZeroPadding(this.GenbaCode.Text);

                // 重複チェック
                GenbaHoshuConstans.GenbaCdLeaveResult isUpdate = this.logic.DupliCheckGenbaCd(zeroPadCd, isRegister);

                if (isUpdate == GenbaHoshuConstans.GenbaCdLeaveResult.FALSE_ON)
                {
                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // バリデーションを解除する
                        this.logic.RemoveDynamicEvent();

                        // 修正モードで表示する
                        this.logic.GyoushaCd = this.GyoushaCode.Text;
                        this.logic.GenbaCd = zeroPadCd;

                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                        // 画面タイトル変更
                        base.HeaderFormInit();

                        // 修正モードで画面初期化
                        catchErr = this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);

                        this.GenbaName1.Focus();

                        // バリデーションを設定する
                        this.logic.SetDynamicEvent();
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        // バリデーションを解除する
                        this.logic.RemoveDynamicEvent();

                        // 参照モードで表示する
                        this.logic.GyoushaCd = this.GyoushaCode.Text;
                        this.logic.GenbaCd = zeroPadCd;

                        base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;

                        // 画面タイトル変更
                        base.HeaderFormInit();

                        // 参照モードで画面初期化
                        catchErr = this.logic.WindowInitReference((BusinessBaseForm)this.Parent);

                        this.GenbaName1.Focus();

                        // バリデーションを設定する
                        this.logic.SetDynamicEvent();
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                        this.GenbaCode.Text = string.Empty;
                        this.GenbaCode.Focus();
                    }

                    result = true;
                }
                else if (isUpdate != GenbaHoshuConstans.GenbaCdLeaveResult.TURE_NONE)
                {
                    // 入力した現場CDが重複した かつ 修正モード未起動の場合
                    this.GenbaCode.Text = string.Empty;
                    this.GenbaCode.Focus();
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

        #region 電子申請

        /// <summary>
        /// 申請
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Shinsei(object sender, EventArgs e)
        {
            //FWで対応できない必須チェックを書く

            // アラートの内容の重複確認クリア
            bool catchErr = this.logic.errorMessagesClear();
            if (catchErr)
            {
                return;
            }

            #region A票～E票返送先区分処理必須チェック

            //【修正】他区分のチェック有り/無しに関わらず
            //マニ返送先区分にチェックがない場合
            //返送先区分によって必須チェックをつける

            var messageShowLogic = new MessageBoxShowLogic();
            string errMsg = "";
            string errAllMsg = "";
            //マニありのチェックが1つでもついている場合必須チェックする。
            if (!IsManiAriAllNoChecked())
            {
                if (!(!this.logic.FlgManiHensousakiKbn && !this.HaishutsuKbn.Checked))
                {
                    // [諸口フラグ]が[OFF]のときのみ
                    if (!this.ShokuchiKbn.Checked)
                    {
                        //A票
                        errMsg = RegistCheck("_AHyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }

                        //B1票
                        errMsg = RegistCheck("_B1Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //B2票
                        errMsg = RegistCheck("_B2Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //B4票
                        errMsg = RegistCheck("_B4Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //B6票
                        errMsg = RegistCheck("_B6Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //C1票
                        errMsg = RegistCheck("_C1Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //C2票
                        errMsg = RegistCheck("_C2Hyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //D票
                        errMsg = RegistCheck("_DHyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }
                        //E票
                        errMsg = RegistCheck("_EHyo");
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            if (string.IsNullOrEmpty(errAllMsg))
                            {
                                errAllMsg += errMsg;
                            }
                            else
                            {
                                errAllMsg += "," + errMsg;
                            }
                        }

                        errMsg = errAllMsg;
                    }
                }
            }

            #endregion

            // Begin: LANDUONG - 20220215 - refs#160054
            if ((this.logic.denshiSeikyusho && this.logic.denshiSeikyuRaku) || (this.logic.denshiSeikyusho && !this.logic.denshiSeikyuRaku)
                    || (!this.logic.denshiSeikyusho && this.logic.denshiSeikyuRaku))
            {
                M_TORIHIKISAKI_SEIKYUU queryParam = new M_TORIHIKISAKI_SEIKYUU();
                queryParam.TORIHIKISAKI_CD = this.TorihikisakiCode.Text;
                M_TORIHIKISAKI_SEIKYUU seikyuuEntity = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>().GetDataByCd(queryParam.TORIHIKISAKI_CD);
                if (seikyuuEntity != null)
                {
                    if (!seikyuuEntity.SHOSHIKI_KBN.IsNull)
                    {
                        if (seikyuuEntity.OUTPUT_KBN.Value == 2 && seikyuuEntity.SHOSHIKI_KBN.Value == 3)
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
                        else if (seikyuuEntity.OUTPUT_KBN.Value == 3 && seikyuuEntity.SHOSHIKI_KBN.Value == 3)
                        {                            
                            if (base.RegistErrorFlag)
                            {
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(this.RAKURAKU_CUSTOMER_CD.Text))
                            {
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
            // End: LANDUONG - 20220215 - refs#160054

            if (!base.RegistErrorFlag && errMsg != "")
            {
                messageShowLogic.MessageBoxShow("E001", errMsg);
                return;
            }

            // 事業場分類チェック処理（全てのエラーチェックを終えてから行う）
            if (!base.RegistErrorFlag && !this.logic.CheckBunruiKbn())
            {
                var result = messageShowLogic.MessageBoxShow("C061", "排出事業場、積み替え保管、処分事業場、最終処分場のいずれか");
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (this.logic.CheckRegistData())
            {
                return;
            }

            #region 電子申請チェック

            catchErr = false;
            bool ret = this.logic.CheckDenshiShinseiData(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (!ret)
            {
                messageShowLogic.MessageBoxShow("E189");
                return;
            }

            #endregion

            if (!base.RegistErrorFlag)
            {
                #region 電子申請入力起動

                catchErr = false;
                var initDto = this.logic.CreateDenshiShinseiInitDto(out catchErr);
                if (catchErr)
                {
                    return;
                }

                // 以下の変数は画面で設定されたデータが設定されてくる
                var denshiShinseiEntry = new T_DENSHI_SHINSEI_ENTRY();
                var denshiShinseiDetailList = new List<T_DENSHI_SHINSEI_DETAIL>();

                FormManager.OpenFormModal("G279", WINDOW_TYPE.NEW_WINDOW_FLAG, null, null
                                    , initDto, denshiShinseiEntry, denshiShinseiDetailList);

                // 必須項目が入力されていないのであれば何もしない
                if (denshiShinseiEntry.SHINSEI_DATE.IsNull)
                {
                    return;
                }

                #endregion

                #region 仮現場登録

                catchErr = this.logic.CreateKariEntity();
                if (catchErr)
                {
                    return;
                }

                this.logic.Shinsei(base.RegistErrorFlag, denshiShinseiEntry, denshiShinseiDetailList);

                #endregion

                if (this.logic.isRegist)
                {
                    // 成功
                    this.FormClose(sender, e);
                }
            }
        }

        #endregion

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
        /// 一覧ボタン押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormSearch(object sender, EventArgs e)
        {
            ////TODO：未実装
            //// 一覧画面を表示する
            //MessageBox.Show("未実装");
            this.logic.ShowIchiran();
        }

        /// <summary>
        /// アクティブになっている返送先情報を他の他票へコピーします
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ManifestoTabCopy(object sender, EventArgs e)
        {
            // メッセージ表示
            var messageShowLogic = new MessageBoxShowLogic();

            var result = messageShowLogic.MessageBoxShow("C055", "現在表示している返送先情報を他票にコピー");
            if (result == DialogResult.No)
            {
                return;
            }

            // 現在表示している返送先情報を変数にコピー
            bool catchErr = false;
            var copyMotoHensousaki = new MANIFESUTO_HENSOUSAKI();
            copyMotoHensousaki = this.logic.HensousakiCopy(out catchErr);
            if (catchErr)
            {
                return;
            }

            // タブ全てをループ
            foreach (TabPage page in this.ManiHensousakiKeishou2B1.TabPages)
            {
                // 全ての返送先へコピー元を反映
                catchErr = this.logic.HensousakiPaste(copyMotoHensousaki, page.Name);
                if (catchErr)
                {
                    return;
                }
            }

            // 完了メッセージ
            messageShowLogic.MessageBoxShow("I001", "コピー");
        }

        /// <summary>
        /// 現場CDフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaCode_Leave(object sender, EventArgs e)
        {
            // 【新規】モードの場合のみチェック処理を行う
            if (base.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                return;
            }

            //業者CDが入力されていなければエラー
            if (string.IsNullOrWhiteSpace(this.GyoushaCode.Text) && this.GenbaCode.Text != "")
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("E027", "業者CD");

                return;
            }

            // 入力された現場CD取得
            string inputCd = this.GenbaCode.Text;
            if (string.IsNullOrWhiteSpace(inputCd))
            {
                return;
            }

            //現場データがあるかどうか確認する
            this.logic.GenbaCd = this.GenbaCode.Text;
            this.logic.GyoushaCd = this.GyoushaCode.Text;
            int count = this.logic.SearchGenba();
            if (count > 0)
            {
                // 重複チェック
                if (this.logic.IsFromShouninzumiDenshiShinseiIchiran)
                {
                    new MessageBoxShowLogic().MessageBoxShow("E025", "現場", "他");
                    this.GenbaCode.Focus();
                    return;
                }
                else
                {
                    bool catchErr = false;
                    this.DupliUpdateViewCheck(e, false, out catchErr);
                }
            }
        }

        /// <summary>
        /// 業者CD検索ポップアップ前の処理を実施
        /// </summary>
        public void PopupBeforeGyoushaCode()
        {
            this.beforeGyoushaCd = this.GyoushaCode.Text;
        }

        /// <summary>
        /// 業者CD検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterGyoushaCode()
        {
            if (this.beforeGyoushaCd == this.GyoushaCode.Text)
            {
                return;
            }
            //業者チェックボックスセット
            bool catchErr = this.logic.SearchchkGyousha(true, false);
            if (catchErr)
            {
                return;
            }

            this.logic.HakkousakuAndRakurakuCDCheck();

            //取引先CDが登録されているとGyoushaCDValidatedイベントが実行されない（？）為、ここで呼び出しています。
            this.logic.ManiCheckOffCheck(false);
            this.logic.SetHensosaki();
        }

        /// <summary>
        /// 業者CD Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GyoushaCode_Enter(object sender, EventArgs e)
        {
            this.beforeGyoushaCd = this.GyoushaCode.Text;
        }

        /// <summary>
        /// 業者CD Validatedイベント
        /// </summary>
        internal virtual void GyoushaCDValidated(object sender, EventArgs e)
        {
            if (!this.logic.isError && this.GyoushaCode.Text.Equals(this.beforeGyoushaCd))
            {
                return;
            }
            this.logic.isError = false;
            bool catchErr = this.logic.SearchchkGyousha(true, false);
            if (catchErr)
            {
                return;
            }

            this.logic.HakkousakuAndRakurakuCDCheck();

            this.logic.ManiCheckOffCheck(false);
            this.logic.SetHensosaki();
        }

        /// <summary>
        /// 取引先CD検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterTorihikisakiCode()
        {
            //取引先拠点セット
            this.logic.SearchsetTorihikisaki();
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            if (!this.TorihikisakiCode.Text.Equals(this.PreviousValue))
            {
                this.logic.HakkousakuAndRakurakuCDCheck();
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
        }

        /// <summary>
        /// 取引先CD Leaveイベント
        /// </summary>
        internal virtual void TorihikisakiCDValidated(object sender, EventArgs e)
        {
            if (!this.logic.isError && this.TorihikisakiCode.Text.Equals(this.PreviousValue))
            {
                return;
            }
            this.logic.isError = false;
            this.logic.IsShowTorihikisakiError = true;
            this.logic.SearchsetTorihikisaki();
            this.logic.IsShowTorihikisakiError = false;

            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            // 発行先チェック処理
            if (!this.logic.isError)
            {
                this.logic.HakkousakuAndRakurakuCDCheck();
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
        }

        /// <summary>
        /// マニフェスト返送先取引先コード検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterManiHensousakiTorihikisakiCode(object sender, DialogResult rlt)
        {
            if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                return;

            this.logic.isError = false;

            //アクティブコントロールを取得
            Control ctl = this.ActiveControl;

            //返送先(票)判定
            string hensousaki = string.Empty;

            bool catchErr = false;
            hensousaki = this.logic.ChkTabEvent(ctl.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            //コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.FindForm().Controls;

            //テキストボックス
            this.logic.ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensousaki);
            this.logic.ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensousaki);

            string cd = this.logic.ManiHensousakiTorihikisakiCode.Text;

            if (!string.IsNullOrWhiteSpace(cd))
            {
                this.logic.SetManiHensousakiTorihikisaki(cd, null, hensousaki);
            }
            else
            {
                this.logic.ManiHensousakiTorihikisakiName.Text = string.Empty;
            }
        }

        /// <summary>
        /// マニフェスト返送先取引先CD Leaveイベント
        /// </summary>
        internal virtual void ManiTorihikisakiCDValidating(object sender, CancelEventArgs e)
        {
            CustomAlphaNumTextBox ctlName = ((CustomAlphaNumTextBox)(sender));

            //返送先(票)判定
            string hensousaki = string.Empty;
            bool catchErr = false;
            hensousaki = this.logic.ChkTabEvent(ctlName.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            //コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.FindForm().Controls;

            //テキストボックス
            this.logic.ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensousaki);
            this.logic.ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensousaki);
            this.logic.ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensousaki);
            this.logic.ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensousaki);
            this.logic.ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensousaki);
            this.logic.ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensousaki);

            string cd = this.logic.ManiHensousakiTorihikisakiCode.Text;
            if (string.IsNullOrWhiteSpace(cd))
            {
                this.logic.ManiHensousakiTorihikisakiName.Text = string.Empty;
                return;
            }
            if (!this.logic.isError && cd.Equals(this.PreviousValue))
            {
                return;
            }

            this.logic.isError = false;
            this.logic.SetManiHensousakiTorihikisaki(cd, e, hensousaki);
        }

        /// <summary>
        /// マニフェスト返送先業者コード検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterManiHensousakiGyoushaCode(object sender, DialogResult rlt)
        {
            if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                return;

            this.logic.isError = false;

            //アクティブコントロールを取得
            Control ctl = this.ActiveControl;

            //返送先(票)判定
            string hensousaki = string.Empty;

            bool catchErr = false;
            hensousaki = this.logic.ChkTabEvent(ctl.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            //コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.FindForm().Controls;
            //テキストボックス
            this.logic.HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensousaki);
            this.logic.ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensousaki);
            this.logic.ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensousaki);
            this.logic.ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensousaki);
            this.logic.ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensousaki);
            this.logic.ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensousaki);
            this.logic.ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensousaki);

            string cd = this.logic.ManiHensousakiGyoushaCode.Text;
            if (string.IsNullOrWhiteSpace(cd))
            {
                this.logic.ManiHensousakiGyoushaName.Text = string.Empty;
                this.logic.ManiHensousakiGenbaCode.Text = string.Empty;
                this.logic.ManiHensousakiGenbaName.Text = string.Empty;
                return;
            }
            if (!this.logic.isError && cd.Equals(this.PreviousValue))
            {
                return;
            }
            this.logic.SetManiHensousakiGyousha(cd, null, hensousaki);
        }

        /// <summary>
        /// マニフェスト返送先業者CD Leaveイベント
        /// </summary>
        internal virtual void ManiGyoushaCDValidating(object sender, CancelEventArgs e)
        {
            CustomAlphaNumTextBox ctlName = ((CustomAlphaNumTextBox)(sender));

            //返送先(票)判定
            string hensousaki = string.Empty;
            bool catchErr = false;
            hensousaki = this.logic.ChkTabEvent(ctlName.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            //コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.FindForm().Controls;

            //テキストボックス
            this.logic.HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensousaki);
            this.logic.ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensousaki);
            this.logic.ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensousaki);
            this.logic.ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensousaki);
            this.logic.ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensousaki);
            this.logic.ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensousaki);
            this.logic.ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensousaki);

            string cd = this.logic.ManiHensousakiGyoushaCode.Text;
            if (string.IsNullOrWhiteSpace(cd))
            {
                this.logic.ManiHensousakiGyoushaName.Text = string.Empty;
                this.logic.ManiHensousakiGenbaCode.Text = string.Empty;
                this.logic.ManiHensousakiGenbaName.Text = string.Empty;
                return;
            }
            if (!this.logic.isError && cd.Equals(this.PreviousValue))
            {
                return;
            }
            //データを設定
            this.logic.ManiHensousakiGenbaCode.Text = string.Empty;
            this.logic.ManiHensousakiGenbaName.Text = string.Empty;
            this.logic.isError = false;
            this.logic.SetManiHensousakiGyousha(cd, e, hensousaki);
        }

        /// <summary>
        /// マニフェスト返送先現場コード検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterManiHensousakiGenbaCode(object sender, DialogResult rlt)
        {
            if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                return;

            this.logic.isError = false;

            //アクティブコントロールを取得
            Control ctl = this.ActiveControl;

            //返送先(票)判定
            string hensousaki = string.Empty;
            bool catchErr = false;
            hensousaki = this.logic.ChkTabEvent(ctl.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            //コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.FindForm().Controls;

            //テキストボックス
            this.logic.ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensousaki);
            this.logic.ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensousaki);// No3521
            //this.logic.ManiHensousakiGyoushaCodeHidden = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCodeHidden" + hensousaki);

            this.logic.isError = false;

            string cd = this.logic.ManiHensousakiGenbaCode.Text;
            // No3521-->
            if (string.IsNullOrWhiteSpace(this.logic.ManiHensousakiGyoushaCode.Text))
            {
                return;
            }
            // No3521--<
            this.logic.ManiGyoushaCd = this.logic.ManiHensousakiGyoushaCode.Text;
            //this.logic.ManiHensousakiGyoushaCodeHidden.Text = this.logic.ManiHensousakiGyoushaCode.Text;

            catchErr = false;
            if (!string.IsNullOrWhiteSpace(cd))
            {
                this.logic.SetManiHensousakiGenba(cd, null, hensousaki, out catchErr);
            }
            else
            {
                this.logic.ManiHensousakiGenbaName.Text = string.Empty;
            }
        }

        /// <summary>
        /// マニフェスト返送先現場CD Leaveイベント
        /// </summary>
        internal virtual void ManiGenbaCDValidating(object sender, CancelEventArgs e)
        {
            CustomAlphaNumTextBox ctlName = ((CustomAlphaNumTextBox)(sender));

            //返送先(票)判定
            string hensousaki = string.Empty;

            bool catchErr = false;
            hensousaki = this.logic.ChkTabEvent(ctlName.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            //コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.FindForm().Controls;

            //テキストボックス
            this.logic.ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensousaki);
            this.logic.ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensousaki);
            this.logic.ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensousaki);
            this.logic.ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensousaki);
            this.logic.ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensousaki);
            this.logic.ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensousaki);

            string cd = this.logic.ManiHensousakiGenbaCode.Text;
            if (string.IsNullOrEmpty(cd))
            {
                this.logic.ManiHensousakiGenbaName.Text = string.Empty;
                return;
            }
            if (!this.logic.isError && cd.Equals(this.PreviousValue))
            {
                return;
            }

            this.logic.isError = false;

            this.logic.ManiGyoushaCd = this.logic.ManiHensousakiGyoushaCode.Text;
            if (!this.logic.SetManiHensousakiGenba(cd, e, hensousaki, out catchErr))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 取引先情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            this.logic.TorihikisakiCd = this.TorihikisakiCode.Text;
            this.logic.TorihikisakiCopy();
        }

        /// <summary>
        /// 取引先入力画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TorihikisakiNew_Click(object sender, EventArgs e)
        {
            // 取引先画面を表示する

            this.logic.ShowTorihikisakiCreate();
        }

        /// <summary>
        /// 採番ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_genbacd_saiban_Click(object sender, EventArgs e)
        {
            //業者CDが入力されていなければエラー
            if (string.IsNullOrWhiteSpace(this.GyoushaCode.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("E027", "業者CD");

                return;
            }
            this.logic.GyoushaCd = this.GyoushaCode.Text;
            // 採番値取得
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
            this.logic.CheckTextBoxLength(this.SeikyuuSoufuAddress1, out catchErr);
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
            this.logic.CheckTextBoxLength(this.ShiharaiSoufuAddress1, out catchErr);
        }

        /// <summary>
        /// 分類取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopyManiButtonClick(object sender, EventArgs e)
        {
            CustomButton ctlName = ((CustomButton)(sender));

            //返送先(票)判定
            string hensousaki = string.Empty;

            bool catchErr = false;
            hensousaki = this.logic.ChkTabEvent(ctlName.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            this.logic.CopyToMani();

            // this.logic.CopyToMani();
            //this.logic.CheckTextBoxLength(this.ManiHensousakiAddress1);
        }

        /// <summary>
        /// 営業担当部署変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EigyouCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.isError && this.EigyouCode.Text.Equals(this.PreviousValue))
            {
                return;
            }

            this.logic.isError = false;
            string cd = this.EigyouCode.Text;
            bool catchErr = false;
            if (!string.IsNullOrWhiteSpace(cd))
            {
                if (!this.logic.SetBushoData(cd, out catchErr))
                {
                    e.Cancel = true;
                }
            }
            else
            {
                this.EigyouCode.Text = string.Empty;
                this.EigyouName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 営業担当検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterEigyouCd()
        {
            string cd = this.EigyouCode.Text;
            bool catchErr = false;
            if (!string.IsNullOrWhiteSpace(cd))
            {
                this.logic.SetBushoData(cd, out catchErr);
            }
        }

        /// <summary>
        /// 営業担当検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteEigyouCd(object sender, DialogResult rlt)
        {
            if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                return;

            string cd = this.EigyouCode.Text;
            bool catchErr = false;
            if (!string.IsNullOrWhiteSpace(cd))
            {
                this.logic.SetBushoData(cd, out catchErr);
            }
        }

        /// <summary>
        /// 営業担当部署変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EigyouTantouBushoCode_TextChanged(object sender, EventArgs e)
        {
            // 【営業担当者CD】【営業担当者名】をクリアする。
            this.EigyouCode.Text = string.Empty;
            this.EigyouName.Text = string.Empty;
        }

        /// <summary>
        /// ポップアップ用営業担当部署変更メソッド
        /// </summary>
        public void SetEigyouData()
        {
            this.EigyouCode.Text = string.Empty;
            this.EigyouName.Text = string.Empty;
        }

        // チェックボックス判定
        private void HaishutsuKbn_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.FlgHaishutsuKbn = this.HaishutsuKbn.Checked;
            bool catchErr = this.logic.ManiCheckOffCheck(true);
            if (catchErr)
            {
                return;
            }
            this.logic.ChangeManiKbn();
            //if (this.GyoushaKbnMani.Checked == false)
            //{
            //    this.DUMMY_HISSU_KBN.Text = "1";
            //}
            //else
            //{
            //    if (this.HaishutsuKbn.Checked == false
            //         && this.TsumikaeHokanKbn.Checked == false
            //             && this.ShobunJigyoujouKbn.Checked == false
            //                 && this.SaishuuShobunjouKbn.Checked == false)
            //    {
            //        this.DUMMY_HISSU_KBN.Text = string.Empty;
            //    }
            //    else
            //    {
            //        this.DUMMY_HISSU_KBN.Text = "1";
            //    }
            //}
        }

        /// <summary>
        /// マニ返送先区分チェック変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKbn_CheckedChanged(object sender, EventArgs e)
        {
            bool catchErr = this.logic.ManiHensousakiKbn_CheckedChanged();
            if (catchErr)
            {
                return;
            }
            catchErr = this.logic.ManiCheckOffCheck(true);
            if (catchErr)
            {
                return;
            }
            this.logic.SettingHensouSakiKbn();
        }

        /// <summary>
        /// 業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_gyousya_copy_Click(object sender, EventArgs e)
        {
            this.logic.GyoushaCd = this.GyoushaCode.Text;
            this.logic.GyousyaCopy();
        }

        /// <summary>
        /// 業者入力画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GyoushaNew_Click(object sender, EventArgs e)
        {
            // 業者画面を表示する

            this.logic.ShowGyoushaCreate();
        }

        /// <summary>
        /// セル表示形式変換処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItakuKeiyakuIchiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            this.logic.ChangeItakuStatus(e);
        }

        /// <summary>
        /// 必須区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DummyKbnChanged(object sender, EventArgs e)
        {
            //if (!this.GyoushaKbnMani.Checked)
            //{
            //    this.DUMMY_HISSU_KBN.Text = "1";
            //}
            //else
            //{
            //    if (!this.HaishutsuKbn.Checked
            //        && !this.TsumikaeHokanKbn.Checked
            //        && !this.ShobunJigyoujouKbn.Checked
            //        && !this.SaishuuShobunjouKbn.Checked)
            //    {
            //        this.DUMMY_HISSU_KBN.Text = string.Empty;
            //    }
            //    else
            //    {
            //        this.DUMMY_HISSU_KBN.Text = "1";
            //    }
            //}
            bool catchErr = this.logic.ChangeManiKbn();
            if (catchErr)
            {
                return;
            }
            this.logic.BunruiKbnChanged();
        }

        /// <summary>
        /// 都道府県コードテキスト変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaTodoufukenCode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.GenbaTodoufukenCode.Text) || this.GenbaTodoufukenCode.Text.Length >= this.GenbaTodoufukenCode.CharactersNumber)
            {
                bool catchErr = this.logic.ChechChiiki(true);
                if (catchErr)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.GenbaTodoufukenCode.Text))
                {
                    this.GenbaTodoufukenNameRyaku.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 住所1フォーカス乖離イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaAddress1_Leave(object sender, EventArgs e)
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
            }

            // 明細が選択されている場合
            if (this.ItakuKeiyakuIchiran.CurrentCell != null)
            {
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
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaHoshuForm_UserRegistCheck(object sender, r_framework.Event.RegistCheckEventArgs e)
        {
            this.logic.CheckRegist(sender, e);
        }

        /// <summary>
        /// 定期品名一覧セルフォーカスエンター処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CellEnter(object sender, CellEventArgs e)
        {
            this.logic.TeikiHinmeiCellEnter(e);
            this.logic.TeikiHinmeiCellSwitchCdName(e, GenbaHoshuConstans.FocusSwitch.IN);
        }

        /// <summary>
        /// 定期品名一覧セルリーブ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CellLeave(object sender, CellEventArgs e)
        {
            this.logic.TeikiHinmeiCellLeave(e);
        }

        /// <summary>
        /// 定期品名一覧セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TeikiHinmeiIchiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            this.logic.TeikiHinmeiCellFormat(e);
        }

        /// <summary>
        /// 定期品名一覧値確定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CellValidated(object sender, CellEventArgs e)
        {
            this.logic.TeikiHinmeiCellValidated(e);
            this.logic.TeikiHinmeiCellSwitchCdName(e, GenbaHoshuConstans.FocusSwitch.OUT);
        }

        /// <summary>
        /// 定期品名一覧値チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TeikiHinmeiIchiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.TeikiHinmeiCellValidating(e);
        }

        /// <summary>
        /// 定期品名一覧値変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CellValueChanged(object sender, CellEventArgs e)
        {
            this.logic.TeikiHinmeiCellValueChanged(e);
        }

        /// <summary>
        /// 定期品名一覧未確定データ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            this.logic.TeikiHinmeiDirtyStateChanged(e);
        }

        /// <summary>
        /// 定期品名一覧編集ボックス表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            e.Control.KeyDown -= this.TeikiHinmeiIchiranEditingControl_KeyDown;
            e.Control.KeyDown += this.TeikiHinmeiIchiranEditingControl_KeyDown;
        }

        /// <summary>
        /// 定期品名一覧編集ボックスキーダウン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiranEditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            this.logic.CheckPopup(e);
        }

        /// <summary>
        /// 定期品名一覧行変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TeikiHinmeiIchiran_RowValidating(object sender, CellCancelEventArgs e)
        {
            this.logic.TeikiHinmeiRowValidating(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsukiHinmeiIchiran_CellEnter(object sender, CellEventArgs e)
        {
            this.logic.TsukiHinmeiCellSwitchCdName(e, GenbaHoshuConstans.FocusSwitch.IN);
        }

        /// <summary>
        /// 月極品名一覧セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TsukiHinmeiIchiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            this.logic.TsukiHinmeiCellFormat(e);
        }

        /// <summary>
        /// 月極品名一覧値チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TsukiHinmeiIchiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.TsukiHinmeiCellValidating(e);
        }

        /// <summary>
        /// 月極品名一覧値確定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsukiHinmeiIchiran_CellValidated(object sender, CellEventArgs e)
        {
            this.logic.TsukiHinmeiCellValidated(e);
            this.logic.TsukiHinmeiCellSwitchCdName(e, GenbaHoshuConstans.FocusSwitch.OUT);
            this.logic.preCellIndex = e.CellIndex;
            this.logic.preRowIndex = e.RowIndex;
        }

        /// <summary>
        /// 定期品名一覧行変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TsukiHinmeiIchiran_RowValidating(object sender, CellCancelEventArgs e)
        {
            this.logic.TsukiHinmeiRowValidating(e);
        }

        /// <summary>
        /// 定期回収情報タブ　市区町村コードチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIKUCHOUSON_CD_Validating(object sender, CancelEventArgs e)
        {
            this.logic.ShikuchousonValidating(e);
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
        private void SHIHARAI_KYOTEN_CD_Validating(object sender, CancelEventArgs e)
        {
            this.logic.isError = false;
            bool catchErr = false;
            if (!this.logic.ShiharaiKyotenCdValidated(out catchErr))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 定期品名一覧セルフォーカスエンター処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Row row = ((GcCustomMultiRow)sender).CurrentRow;

            if (row == null)
            {
                return;
            }

            // 伝票ポップアップキャンセル時もHINMEI_CDに対してENTERイベントが発生していまうため、値が再保存されないように制御する。
            if (!bDetailErrorFlag)
            {
                // 前回値チェック用データをセット
                if (beforeValuesForDetail.ContainsKey(e.CellName))
                {
                    beforeValuesForDetail[e.CellName] = Convert.ToString(row.Cells[e.CellName].Value);
                }
                else
                {
                    beforeValuesForDetail.Add(e.CellName, Convert.ToString(row.Cells[e.CellName].Value));
                }
                if (e.CellName.Equals("HINMEI_CD"))
                {
                    if (beforeValuesForDetail.ContainsKey(Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU))
                    {
                        beforeValuesForDetail[Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU] = Convert.ToString(row.Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU].Value);
                    }
                    else
                    {
                        beforeValuesForDetail.Add(Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU, Convert.ToString(row.Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU].Value));
                    }
                }
            }

            // 月極情報タブ
            if (this.ActiveControl != null && this.ActiveControl.Parent.Text.Equals("月極情報"))
            {
                //新行の場合、削除フラグチェック不可設定
                if (this.TsukiHinmeiIchiran.Rows[e.RowIndex].IsNewRow)
                {
                    this.TsukiHinmeiIchiran.Rows[e.RowIndex][0].Selectable = false;
                }
                else
                {
                    this.TsukiHinmeiIchiran.Rows[e.RowIndex][0].Selectable = true;
                }
            }

            // 定期回収情報
            // 新規モードまたは、新行の場合、削除フラグチェック不可設定
            if (this.ActiveControl != null && this.ActiveControl.Parent.Text.Equals("定期回収情報"))
            {
                //新行の場合、削除フラグチェック不可設定
                if (this.TeikiHinmeiIchiran.Rows[e.RowIndex].IsNewRow)
                {
                    this.TeikiHinmeiIchiran.Rows[e.RowIndex][0].Selectable = false;
                    this.TeikiHinmeiIchiran.Rows[e.RowIndex][1].Selectable = false;
                }
                else
                {
                    this.TeikiHinmeiIchiran.Rows[e.RowIndex][0].Selectable = true;
                    this.TeikiHinmeiIchiran.Rows[e.RowIndex][1].Selectable = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 営業担当部署の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EigyouTantouBushoCode_Validating(object sender, CancelEventArgs e)
        {
            this.logic.isError = false;
            bool catchErr = false;
            if (!this.logic.BushoCdValidated(out catchErr))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 返送先区分テキストボックスの値が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void HensousakiKbn_TextChanged(object sender, EventArgs e)
        {
            // 使用区分テキストボックス
            var useKbnTextBox = ((CustomNumericTextBox2)(sender));
            // 使用区分
            var useKbn = useKbnTextBox.Text;
            //返送先(票)判定
            bool catchErr = false;
            var hyoName = this.logic.ChkTabEvent(useKbnTextBox.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            this.logic.SetEnabledManifestHensousakiKbnRendou(hyoName);
        }

        /// <summary>
        /// 使用区分テキストボックスの値が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void MANI_HENSOUSAKI_USEKbn_TextChanged(object sender, EventArgs e)
        {
            // 使用区分テキストボックス
            var useKbnTextBox = ((CustomNumericTextBox2)(sender));
            // 使用区分
            var useKbn = useKbnTextBox.Text;
            // マニフェスト返送先
            var maniHensousaki = this.ManiHensousakiKbn.Checked;
            //返送先(票)判定
            bool catchErr = false;
            var hyoName = this.logic.ChkTabEvent(useKbnTextBox.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            if ("1" == useKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.logic.SetEnabledManifestHensousakiRendou(hyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.logic.SetEnabledFalseManifestHensousaki(hyoName);
            }
        }

        /// <summary>
        /// 登録時の必須チェック
        /// </summary>
        /// <param name="hensouCd">返送先CD</param>
        /// <returns>エラーメッセージ</returns>
        private string RegistCheck(string hensouCd)
        {
            string errMsg = "";

            //コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.FindForm().Controls;

            //テキストボックス
            this.logic.HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensouCd);

            this.logic.ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensouCd);
            this.logic.ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensouCd);
            this.logic.ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensouCd);

            if (this.logic.HensousakiKbn != null && this.logic.HensousakiKbn.Enabled)
            {
                if (this.logic.HensousakiKbn.Enabled && string.Empty.Equals(this.logic.HensousakiKbn.Text))
                {
                    // 各票の返送先区分が使用可であるにも関わらず、入力が無い場合
                    errMsg = "返送先区分";
                }
                if (this.logic.HensousakiKbn.Text == "1")
                {
                    if (string.IsNullOrEmpty(this.logic.ManiHensousakiTorihikisakiCode.Text))
                    {
                        errMsg = "返送先取引先CD";
                    }
                }
                else if (this.logic.HensousakiKbn.Text == "2")
                {
                    if (string.IsNullOrEmpty(this.logic.ManiHensousakiGyoushaCode.Text))
                    {
                        errMsg = "返送先業者CD";
                    }
                }
                else if (this.logic.HensousakiKbn.Text == "3")
                {
                    //if (string.IsNullOrEmpty(this.logic.ManiHensousakiGyoushaCode.Text))
                    //{
                    //    errMsg = "返送先業者CD";
                    //}
                    if (string.IsNullOrEmpty(this.logic.ManiHensousakiGenbaCode.Text))
                    {
                        //if (!string.IsNullOrEmpty(errMsg))
                        //{
                        //    errMsg += "と";
                        //}
                        errMsg = "返送先現場CD";
                    }
                }
            }

            string tabName = hensouCd.Substring(1, hensouCd.Length - 4);
            if (!string.IsNullOrEmpty(errMsg))
            {
                errMsg = tabName + "票" + errMsg;
            }

            return errMsg;
        }

        /// <summary>
        /// マニありのチェックがすべてついていないときTrueを返す。
        /// </summary>
        /// <returns></returns>
        public bool IsManiAriAllNoChecked()
        {
            if (this.HaishutsuKbn.Checked == false &&
                this.TsumikaeHokanKbn.Checked == false &&
                this.ShobunJigyoujouKbn.Checked == false &&
                this.SaishuuShobunjouKbn.Checked == false &&
                this.ManiHensousakiKbn.Checked == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// タブページ変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKeishou2B1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // サブファンクションの活性・非活性を切り替えます
            this.logic.SubFunctionEnabledChenge();
        }

        internal string ShinseiTorihikisakiCd { get; set; }
        internal string ShinseiGyoushaCd { get; set; }
        internal string ShinseiGenbaCd { get; set; }
        internal string ShinseiHikiaiTorihikisakiCd { get; set; }
        internal string ShinseiHikiaiGyoushaCd { get; set; }
        internal string ShinseiHikiaiGenbaCd { get; set; }
        internal bool ShinseiHikiaiGyoushaUseFlg { get; set; }

        /// <summary>電子申請システムID</summary>
        internal long DenshiShinseiSystemId { get; set; }

        /// <summary>電子申請SEQ</summary>
        internal int DenshiShinseiSeq { get; set; }

        // 20141208 ブン 運搬報告書提出先を追加する start
        internal void UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_Validated(object sender, EventArgs e)
        {
            this.logic.isError = false;
            // 運搬報告書提出先拠点セット
            this.logic.SearchsetUpanHoukokushoTeishutsu();
        }

        // 20141208 ブン 運搬報告書提出先を追加する end

        // VUNGUYEN 20150525 #1294 START
        private void TekiyouKikanForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                EigyouCode.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                ChuusiRiyuu1.Focus();
            }
        }

        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// 返送先場所区分テキストボックスの値が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void HENSOUSAKI_PLACE_KBN_TextChanged(object sender, EventArgs e)
        {
            // 使用区分テキストボックス
            var useKbnTextBox = ((CustomNumericTextBox2)(sender));
            //返送先(票)判定
            bool catchErr = false;
            var hyoName = this.logic.ChkTabEvent(useKbnTextBox.Name, out catchErr);
            if (catchErr)
            {
                return;
            }

            // マニフェスト返送先のチェック状態に応じて状態変更
            this.logic.HENSOUSAKI_PLACE_KBN_TextChanged(hyoName);
        }

        private void MANI_HENSOUSAKI_THIS_ADDRESS_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_TextChanged();
        }

        private void TsukiHinmeiIchiran_CellContentClick(object sender, CellEventArgs e)
        {
            this.logic.TsukiHinmeiIchiran_CellContentClick(e);
        }
        public void TsukiHinmeiPopupAfter()
        {
            this.beforeValuesForDetail[GenbaHoshuConstans.TSUKI_HINMEI_CD] = string.Empty;
        }

        private void TsukiHinmeiIchiran_CurrentCellChanged(object sender, EventArgs e)
        {
            this.logic.TsukiHinmeiIchiran_CurrentCellChanged(e);
        }

        private void TsukiHinmeiIchiran_CellValueChanged(object sender, CellEventArgs e)
        {
            this.logic.TsukiHinmeiIchiran_CellContentClick(e);
        }

        private void TsukiHinmeiIchiran_CellContentDoubleClick(object sender, CellEventArgs e)
        {
            this.logic.TsukiHinmeiIchiran_CellContentClick(e);
        }

        /// <summary>
        /// 現場敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaKeishou1_TextChanged(object sender, EventArgs e)
        {
            this.GenbaKeishou1.DroppedDown = false;
        }

        /// <summary>
        /// 現場敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaKeishou2_TextChanged(object sender, EventArgs e)
        {
            this.GenbaKeishou2.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeikyuuSouhuKeishou1_TextChanged(object sender, EventArgs e)
        {
            this.SeikyuuSouhuKeishou1.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeikyuuSouhuKeishou2_TextChanged(object sender, EventArgs e)
        {
            this.SeikyuuSouhuKeishou2.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiharaiSoufuKeishou1_TextChanged(object sender, EventArgs e)
        {
            this.ShiharaiSoufuKeishou1.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiharaiSoufuKeishou2_TextChanged(object sender, EventArgs e)
        {
            this.ShiharaiSoufuKeishou2.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKeishou1_TextChanged(object sender, EventArgs e)
        {
            this.ManiHensousakiKeishou1.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKeishou2_TextChanged(object sender, EventArgs e)
        {
            this.ManiHensousakiKeishou2.DroppedDown = false;
        }

        /// <summary>
        /// 現場敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaKeishou1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GenbaKeishou1.DroppedDown = false;
        }

        /// <summary>
        /// 現場敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaKeishou2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GenbaKeishou2.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeikyuuSouhuKeishou1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SeikyuuSouhuKeishou1.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeikyuuSouhuKeishou2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SeikyuuSouhuKeishou2.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiharaiSoufuKeishou1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.ShiharaiSoufuKeishou1.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiharaiSoufuKeishou2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.ShiharaiSoufuKeishou2.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKeishou1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.ManiHensousakiKeishou1.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKeishou2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.ManiHensousakiKeishou2.DroppedDown = false;
        }

        /// <summary>
        /// 請求情報タブ：取引先情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_seikyuu_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TorihikisakiCode.Text))
            {
                return;
            }

            this.logic.TorihikisakiCd = this.TorihikisakiCode.Text;
            bool catchErr = this.logic.TorihikisakiInfoCopyFromSeikyuuInfo();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.SeikyuuSoufuAddress1, out catchErr);
        }

        /// <summary>
        /// 請求情報タブ：業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_seikyuu_gyousha_copy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.GyoushaCode.Text))
            {
                return;
            }

            this.logic.GyoushaCd = this.GyoushaCode.Text;
            bool catchErr = this.logic.GyoushaInfoCopyFromSeikyuuInfo();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.SeikyuuSoufuAddress1, out catchErr);
        }

        /// <summary>
        /// 支払情報タブ：取引先情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_shiharai_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TorihikisakiCode.Text))
            {
                return;
            }

            this.logic.TorihikisakiCd = this.TorihikisakiCode.Text;
            bool catchErr = this.logic.TorihikisakiInfoCopyFromShiharaiInfo();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.ShiharaiSoufuAddress1, out catchErr);
        }

        /// <summary>
        /// 支払情報タブ：業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_shiharai_gyousha_copy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.GyoushaCode.Text))
            {
                return;
            }

            this.logic.GyoushaCd = this.GyoushaCode.Text;
            bool catchErr = this.logic.GyoushaInfoCopyFromShiharaiInfo();
            if (catchErr)
            {
                return;
            }
            this.logic.CheckTextBoxLength(this.ShiharaiSoufuAddress1, out catchErr);
        }

        /// <summary>
        /// コース一覧を呼び出す
        /// </summary>
        public virtual void MoveToCourseIchiran(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.WindowType)
            {
                FormManager.OpenFormWithAuth("M663", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
            else
            {
                FormManager.OpenFormWithAuth("M663", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.GyoushaCode.Text, this.GenbaCode.Text);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// BT_GYOUSHA_REFERENCE_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_GYOUSHA_REFERENCE_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.GyoushaCode.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E290", "業者CD");
                return;
            }
            this.logic.OpenGyoushaFormReference(this.GyoushaCode.Text);
        }

        /// <summary>
        /// BT_TORIHIKISAKI_REFERENCE_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_TORIHIKISAKI_REFERENCE_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TorihikisakiCode.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E290", "取引先CD");
                return;
            }
            this.logic.OpenTorihikisakiFormReference(this.TorihikisakiCode.Text);
        }

        private void GenbaHoshuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.logic.fileDelete();
        }
        
        //Begin: LANDUONG - 20220225 - refs#160796
        private void RAKURAKU_SAIBAN_BUTTON_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.SaibanRakurakuCode();

            LogUtility.DebugMethodEnd();
        }
        //End: LANDUONG - 20220225 - refs#160796

        /// <summary>
        /// [ｼｮｰﾄﾒｯｾｰｼﾞ]タブ携帯番号一覧の入力値チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SMSReceiverIchiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.SMSCellValidating(e);
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ可否切替イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SMS_USE_CheckedChanged(object sender, EventArgs e)
        {
            if (SMS_USE_1.Checked)
            {
                this.logic.SmsEnable(false);
            }
            else
            {
                this.logic.SmsEnable(true);
            }
        }

        /// <summary>
        /// セルボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SMSReceiverIchiran_CellContentButtonClick(object sender, CellEventArgs e)
        {
            if(e.CellName == "registButton")
            {
                // 登録ボタン押下処理
                this.logic.SMSReceiverInfoRegist(base.WindowType, e.RowIndex);
            }

            else if(e.CellName == "copyButton")
            {
                // 事前チェックで問題が無ければ継続
                if (!this.logic.SMSReceiverInfoCopyBeforeCheck())
                {
                    // 複写ボタン押下処理
                    this.logic.SMSReceiverInfoCopy(e.RowIndex);
                }
            }
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者一覧RowsAddedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SMSReceiverIchiran_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            var row = this.SMSReceiverIchiran.Rows[e.RowIndex];

            row.Cells["DELETE_FLG"].ReadOnly = false;
            row.Cells["MOBILE_PHONE_NUMBER"].ReadOnly = false;
            row.Cells["RECEIVER_NAME"].ReadOnly = false;
            row.Cells["DELETE_FLG"].Style.BackColor = Constans.NOMAL_COLOR;
            row.Cells["MOBILE_PHONE_NUMBER"].Style.BackColor = Constans.NOMAL_COLOR;
            row.Cells["RECEIVER_NAME"].Style.BackColor = Constans.NOMAL_COLOR;

            row.Cells["registButton"].Enabled = true;
            row.Cells["copyButton"].Enabled = true;
        }

        //20250320
        private void BT_SANSHO_Click(object sender, EventArgs e)
        {
            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var title = "取り込むファイルを選択してください";
            //var initialPath = @"C:\Temp";
            var initialPath = this.CHIZU.Text;
            var windowHandle = this.Handle;
            var isFileSelect = true;
            var isTerminalMode = SystemProperty.IsTerminalMode;
            var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

            browserForFolder = null;

            if (!String.IsNullOrEmpty(filePath))
            {
                this.CHIZU.Text = filePath;
            }
        }

        private void BT_ETSURAN_Click(object sender, EventArgs e)
        {
            var filePath = this.CHIZU.Text;

            if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    LogUtility.Error("OpenFile", ex);
                    this.errmessage.MessageBoxShowError("ファイルを開けません");
                }
            }
            else
            {
                this.errmessage.MessageBoxShowWarn("ファイルを選択してください");
            }
        }
    }
}