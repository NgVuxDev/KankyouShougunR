using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Master.HikiaiGenbaHoshu.Const;
using Shougun.Core.Master.HikiaiGenbaHoshu.Entity;
using Shougun.Core.Master.HikiaiGenbaHoshu.Logic;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.APP
{
    /// <summary>
    /// 引合現場保守画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        // 20140709 syunrei #947 №19 start
        //メッセージ
        public MessageBoxShowLogic messBSL = new MessageBoxShowLogic();
        // 20140709 syunrei #947 №19 end

        /// <summary>
        /// 現場保守画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// ポップアップ動作チェック用変数(Detial用)
        /// </summary>
        internal bool FlgDenpyouKbn;

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        /// <summary>
        /// 明細でエラーが起きたかどうか判断するためのフラグ
        /// </summary>
        internal bool bDetailErrorFlag = false;

        /// <summary>
        /// コンストラクタ(【新規】モード起動時)
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();

                InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コンストラクタ(【修正】【削除】【複写】モード起動時)
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="hikiaiGyoushaUseFlg">True：引合業者を参照 False：通常の業者マスタを参照</param>
        /// <param name="gyoushaCd">選択されたデータの業者CD</param>
        /// <param name="genbaCd">選択されたデータの現場CD</param>
        /// <param name="denshiShinseiFlg">True：電子申請で使用 False：電子申請で使用せず</param>
        public UIForm(WINDOW_TYPE windowType, bool hikiaiGyoushaUseFlg, string gyoushaCd, string genbaCd, bool denshiShinseiFlg)
            : base(WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, hikiaiGyoushaUseFlg, gyoushaCd, genbaCd, denshiShinseiFlg);

                InitializeComponent();

                // この２つのプロパティは、デザイナー上でNoneに設定しても、
                // フレームワーク側で上書きされてしまうためここで再設定します。
                this.GenbaKeishou1.AutoCompleteMode = AutoCompleteMode.None;
                this.GenbaKeishou2.AutoCompleteMode = AutoCompleteMode.None;
                this.SeikyuuSouhuKeishou1.AutoCompleteMode = AutoCompleteMode.None;
                this.SeikyuuSouhuKeishou2.AutoCompleteMode = AutoCompleteMode.None;
                this.ShiharaiSoufuKeishou1.AutoCompleteMode = AutoCompleteMode.None;
                this.ShiharaiSoufuKeishou2.AutoCompleteMode = AutoCompleteMode.None;
                this.ManiHensousakiKeishou1.AutoCompleteMode = AutoCompleteMode.None;
                this.ManiHensousakiKeishou2.AutoCompleteMode = AutoCompleteMode.None;
                this.GenbaKeishou1.AutoCompleteSource = AutoCompleteSource.None;
                this.GenbaKeishou2.AutoCompleteSource = AutoCompleteSource.None;
                this.SeikyuuSouhuKeishou1.AutoCompleteSource = AutoCompleteSource.None;
                this.SeikyuuSouhuKeishou2.AutoCompleteSource = AutoCompleteSource.None;
                this.ShiharaiSoufuKeishou1.AutoCompleteSource = AutoCompleteSource.None;
                this.ShiharaiSoufuKeishou2.AutoCompleteSource = AutoCompleteSource.None;
                this.ManiHensousakiKeishou1.AutoCompleteSource = AutoCompleteSource.None;
                this.ManiHensousakiKeishou2.AutoCompleteSource = AutoCompleteSource.None;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);
                this.logic.HikiaiGyoushaUseFlg = hikiaiGyoushaUseFlg;
                this.logic.GyoushaCd = gyoushaCd;
                this.logic.GenbaCd = genbaCd;
                this.logic.denshiShinseiFlg = denshiShinseiFlg;

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

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

                if (!this.logic.WindowInit(base.WindowType))
                {
                    return;
                }

                // サブファンクションの活性・非活性を切り替えます
                this.logic.SubFunctionEnabledChenge();
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // 20140709 syunrei #947 №19 start
        /// <summary>
        /// 【移行】ボタンを押下する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ikou(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                if (messBSL.MessageBoxShow("C074") == DialogResult.Yes)
                {
                    //画面の業者コード
                    string gyoshaCd = this.GyoushaCode.Text;
                    //画面の現場コード
                    string genba = this.GenbaCode.Text;
                    //画面の引合業者利用フラグ
                    string useflg = this.HIKIAI_GYOUSHA_USE_FLG.Text;

                    //登録処理を行う
                    var parentForm = (BusinessBaseForm)this.Parent;
                    //登録ボタン(F9)イベント呼出
                    parentForm.bt_func9.PerformClick();

                    // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする start
                    //検索条件設定
                    M_HIKIAI_GENBA genbaData = new M_HIKIAI_GENBA();
                    genbaData.GYOUSHA_CD = gyoshaCd;
                    genbaData.GENBA_CD = genba;
                    genbaData.HIKIAI_GYOUSHA_USE_FLG = useflg.Equals("1") ? true : false;

                    //移行前のデータ取得
                    M_HIKIAI_GENBA ikouBeforeData = new M_HIKIAI_GENBA();
                    bool catchErr = true;
                    ikouBeforeData = this.logic.ikouBeforeData(genbaData, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    // 20140716 EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する start
                    if (ikouBeforeData.HIKIAI_GYOUSHA_USE_FLG)
                    {
                        messBSL.MessageBoxShow("E185", "引合業者");
                        return;
                    }
                    else
                    {
                        string gyousha = string.Empty;
                        gyousha = this.logic.getGyoushaAFTER(ikouBeforeData.GYOUSHA_CD, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (gyousha == "" || string.IsNullOrEmpty(gyousha))
                        {
                            messBSL.MessageBoxShow("E185", "引合業者");
                            return;
                        }
                        else
                        {
                            if (ikouBeforeData.HIKIAI_TORIHIKISAKI_USE_FLG)
                            {
                                messBSL.MessageBoxShow("E185", "引合取引先");
                                return;
                            }
                            else
                            {
                                if (ikouBeforeData.TORIHIKISAKI_CD != "" && !string.IsNullOrEmpty(ikouBeforeData.TORIHIKISAKI_CD))
                                {
                                    string torihikisaki = string.Empty;
                                    torihikisaki = this.logic.getTorihikisakiAFTER(ikouBeforeData.TORIHIKISAKI_CD, out catchErr);
                                    if (!catchErr)
                                    {
                                        return;
                                    }
                                    if (torihikisaki == "" || string.IsNullOrEmpty(torihikisaki))
                                    {
                                        messBSL.MessageBoxShow("E185", "引合取引先");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    // トランザクション開始
                    // using (Transaction tran = new Transaction())
                    //{
                    if (!base.RegistErrorFlag)
                    {
                        //Transaction tran = new Transaction();
                        try
                        {
                            using (Transaction tran = new Transaction())
                            {
                                //20150930 hoanghm #1932 start
                                //引合先マスタから通常マスタへ移行する
                                //M_HIKIAI_GENBA ikouBefore = this.logic.CreateIkouData(ikouBeforeData);
                                //if (ikouBefore == null)
                                //{
                                //    return;
                                //}

                                //if (ikouBefore != null && !string.IsNullOrEmpty(ikouBefore.GYOUSHA_CD)
                                //    && !string.IsNullOrEmpty(ikouBefore.GENBA_CD))
                                //{
                                //M_GENBA ikouData = new M_GENBA();
                                ////引合マスタのデータを通常マスタに変更する
                                //ikouData = this.logic.HikiToTsujou(ikouBefore);

                                //// 20140716 EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する end
                                ////引合マスタのデータを通常マスタに移行する
                                //this.logic.InsertIkouData(ikouData, ikouBefore);
                                //}
                                M_GENBA ikouData = new M_GENBA();
                                //引合マスタのデータを通常マスタに変更する
                                ikouData = this.logic.HikiToTsujou(ikouBeforeData);

                                // 20140716 EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する end
                                //引合マスタのデータを通常マスタに移行する
                                this.logic.InsertIkouData(ikouData, ikouBeforeData);
                                //20150930 hoanghm #1932 end

                                // トランザクション終了
                                tran.Commit();
                            }

                            if (!this.logic.SaibanErrorFlg)
                            {
                                messBSL.MessageBoxShow("I001", "通常マスタへの移行");
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする end
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ikou", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        // 20140709 syunrei #947 №19 end

        /// <summary>
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (!Manager.CheckAuthority("M463", WINDOW_TYPE.NEW_WINDOW_FLAG))
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
                if (!this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent))
                {
                    return;
                }

                // バリデーションをセットする
                this.logic.SetDynamicEvent();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (Manager.CheckAuthority("M463", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 処理モード変更
                    base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else if (Manager.CheckAuthority("M463", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    if (!Manager.CheckAuthority("M463", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "修正");
                        return;
                    }

                    // 処理モード変更
                    base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                else
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                // バリデーションを解除する
                this.logic.RemoveDynamicEvent();

                // 画面タイトル変更
                base.HeaderFormInit();

                // 画面初期化
                if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    if (!this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent))
                    {
                        return;
                    }
                }
                else if (base.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    if (!this.logic.WindowInitReference((BusinessBaseForm)this.Parent))
                    {
                        return;
                    }
                }

                // バリデーションをセットする
                this.logic.SetDynamicEvent();
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateMode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //FWで対応できない必須チェックを書く

                // No2267-->
                // アラートの内容の重複確認クリア
                this.logic.errorMessagesClear();
                // No2267<--

                #region A票～E票返送先区分処理必須チェック

                //【修正】他区分のチェック有り/無しに関わらず
                //マニ返送先区分にチェックがない場合
                //返送先区分によって必須チェックをつける

                #region マージ前削除ソース

                //var messageShowLogic = new MessageBoxShowLogic();
                //string errMsg = "";

                ////マニありのチェックが1つでもついている場合必須チェックする。
                //if (!IsManiAriAllNoChecked())
                //{
                //    if (!this.logic.FlgManiHensousakiKbn)
                //    {
                //        // [諸口フラグ]が[OFF]のときのみ
                //        if (!this.ShokuchiKbn.Checked)
                //        {
                //            if (this.HensousakiKbn.Text == "1")
                //            {
                //                if (string.IsNullOrEmpty(this.ManiHensousakiTorihikisakiCode.Text))
                //                {
                //                    errMsg = "返送先取引先CD";
                //                }

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
                //                // No3521-->
                //                //if (string.IsNullOrEmpty(this.ManiHensousakiGyoushaCode.Text))
                //                //{
                //                //    errMsg = "返送先業者CD";
                //                //}
                //                // No3521<--
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
                var messageShowLogic = new MessageBoxShowLogic();
                string errMsg = "";
                string errAllMsg = "";
                bool catchErr = true;
                //マニありのチェックが1つでもついている場合必須チェックする。
                if (!IsManiAriAllNoChecked())
                {
                    if (!(!this.logic.FlgManiHensousakiKbn && !this.HaishutsuKbn.Checked))
                    {
                        // [諸口フラグ]が[OFF]のときのみ
                        if (!this.ShokuchiKbn.Checked)
                        {
                            //A票
                            errMsg = RegistCheck("_AHyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_B1Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_B2Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_B4Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_B6Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_C1Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_C2Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_DHyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_EHyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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

                #region 電子申請チェック

                bool isOk = this.logic.CheckDenshiShinseiData(out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if ((base.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        || base.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                    && !isOk)
                {
                    messageShowLogic.MessageBoxShow("E192", WINDOW_TYPEExt.ToTypeString(base.WindowType));
                    return;
                }

                #endregion

                #region 本登録済みデータチェック

                if (base.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    var existGyousha = this.logic.ExistedGyousha(this.HIKIAI_GYOUSHA_USE_FLG.Text, this.GyoushaCode.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    var existGenba = this.logic.ExistedGenba(this.HIKIAI_GYOUSHA_USE_FLG.Text, this.GyoushaCode.Text, this.GenbaCode.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (existGyousha)
                    {
                        if (existGenba)
                        {
                            // 本登録前(業者のみ本登録)
                            messageShowLogic.MessageBoxShow("E204", "既に業者が", "業者");
                        }
                        else
                        {
                            // 本登録後(業者と現場も本登録)
                            messageShowLogic.MessageBoxShow("E202", "既に");
                        }
                        return;
                    }
                    else
                    {
                        // 既存業者
                        if (existGenba)
                        {
                            // 本登録後(既存業者で現場が本登録)
                            messageShowLogic.MessageBoxShow("E202", "既に");
                            return;
                        }
                    }
                }

                #endregion

                if (!base.RegistErrorFlag)
                {
                    /// 20141203 Houkakou 「引合現場入力」の日付チェックを追加する start
                    if (this.logic.DateCheck())
                    {
                        return;
                    }
                    /// 20141203 Houkakou 「引合現場入力」の日付チェックを追加する end

                    if (this.logic.CheckRegistData())
                    {
                        return;
                    }

                    if (!this.logic.CreateEntity(false))
                    {
                        return;
                    }

                    string sHikiaiGyoushaCd = this.logic.genbaEntity.GYOUSHA_CD;
                    string sHikiaiGenbaCd = this.logic.genbaEntity.GENBA_CD;

                    switch (base.WindowType)
                    {
                        // 新規追加
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            // 重複チェック
                            bool result = this.DupliUpdateViewCheck(e, true);
                            if (result)
                            {
                                // 重複していなければ登録を行う
                                this.logic.Regist(base.RegistErrorFlag);
                            }
                            break;

                        // 更新
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.logic.Update(base.RegistErrorFlag);
                            break;

                        // 論理削除
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            this.logic.LogicalDelete();
                            break;

                        default:
                            break;
                    }

                    if (this.logic.isRegist)
                    {
                        if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            if (Manager.CheckAuthority("M463", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                            {
                                // バリデーションを解除する
                                this.logic.RemoveDynamicEvent();

                                // DB更新後、新規モードで表示
                                base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                base.HeaderFormInit();
                                this.logic.GyoushaCd = string.Empty;
                                this.logic.GenbaCd = string.Empty;
                                this.logic.isRegist = false;
                                if (!this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent))
                                {
                                    return;
                                }

                                // バリデーションをセットする
                                this.logic.SetDynamicEvent();
                            }
                            else
                            {
                                this.FormClose(sender, e);
                            }
                        }
                        else//open mode 参照 after regist
                        {
                            if (Manager.CheckAuthority("M463", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                // バリデーションを解除する
                                this.logic.RemoveDynamicEvent();

                                // DB更新後、新規モードで表示
                                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                base.HeaderFormInit();
                                this.logic.GyoushaCd = sHikiaiGyoushaCd;
                                this.logic.GenbaCd = sHikiaiGenbaCd;
                                this.logic.isRegist = false;
                                if (!this.logic.WindowInitReference((BusinessBaseForm)this.Parent))
                                {
                                    return;
                                }

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
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 申請処理

        /// <summary>
        /// 申請処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Shinsei(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //FWで対応できない必須チェックを書く
                // アラートの内容の重複確認クリア
                this.logic.errorMessagesClear();

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
                        bool catchErr = true;
                        // [諸口フラグ]が[OFF]のときのみ
                        if (!this.ShokuchiKbn.Checked)
                        {
                            //A票
                            errMsg = RegistCheck("_AHyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_B1Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_B2Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_B4Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_B6Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_C1Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_C2Hyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_DHyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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
                            errMsg = RegistCheck("_EHyo", out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
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

                if (!base.RegistErrorFlag)
                {
                    #region 電子申請入力起動

                    var initDto = this.logic.CreateDenshiShinseiInitDto();

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

                    #region 更新処理

                    #region 電子申請チェック

                    bool catchErr = true;
                    bool isOk = this.logic.CheckDenshiShinseiData(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOk)
                    {
                        messageShowLogic.MessageBoxShow("E189");
                        return;
                    }

                    #endregion

                    /// 20141203 Houkakou 「引合現場入力」の日付チェックを追加する start
                    if (this.logic.DateCheck())
                    {
                        return;
                    }
                    /// 20141203 Houkakou 「引合現場入力」の日付チェックを追加する end

                    if (!this.logic.CreateEntity(false))
                    {
                        return;
                    }
                    this.logic.Shinsei(base.RegistErrorFlag, denshiShinseiEntry, denshiShinseiDetailList);

                    if (this.logic.isRegist)
                    {
                        // 成功
                        this.FormClose(sender, e);
                        FormManager.UpdateForm("G560");
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Shinsei", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        /// <summary>
        /// 現場CD重複チェック and 修正モード起動要否チェック
        /// </summary>
        /// <param name="e">イベント</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        private bool DupliUpdateViewCheck(EventArgs e, bool isRegister)
        {
            bool result = false;

            try
            {
                LogUtility.DebugMethodStart(e, isRegister);

                // バリデーションを解除する
                this.logic.RemoveDynamicEvent();

                // 現場CDの入力値をゼロパディング
                string zeroPadCd = this.logic.ZeroPadding(this.GenbaCode.Text);

                // 重複チェック
                ConstCls.GenbaCdLeaveResult isUpdate = this.logic.DupliCheckGenbaCd(zeroPadCd, isRegister);

                if (isUpdate == ConstCls.GenbaCdLeaveResult.FALSE_ON)
                {
                    if (Manager.CheckAuthority("M463", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    }
                    else if (Manager.CheckAuthority("M463", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }
                    else
                    {
                        return result;
                    }

                    // 修正モードで表示する
                    this.logic.GyoushaCd = this.GyoushaCode.Text;
                    this.logic.GenbaCd = zeroPadCd;

                    // 画面タイトル変更
                    base.HeaderFormInit();

                    if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        // 修正モードで画面初期化
                        this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                    }
                    else if (base.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        // 参照モードで画面初期化
                        this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                    }

                    this.GenbaName1.Focus();

                    // バリデーションを設定する
                    this.logic.SetDynamicEvent();

                    result = true;
                }
                else if (isUpdate != ConstCls.GenbaCdLeaveResult.TURE_NONE)
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("DupliUpdateViewCheck", ex);
                this.messBSL.MessageBoxShow("E245", "");
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!base.RegistErrorFlag)
                {
                    this.logic.LogicalDelete();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取消ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 本登録済みデータチェック
                if (base.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    bool catchErr = true;
                    var existGyousha = this.logic.ExistedGyousha(this.HIKIAI_GYOUSHA_USE_FLG.Text, this.GyoushaCode.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    var existGenba = this.logic.ExistedGenba(this.HIKIAI_GYOUSHA_USE_FLG.Text, this.GyoushaCode.Text, this.GenbaCode.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    if (existGyousha || existGenba)
                    {
                        MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E209");
                        return;
                    }
                }

                this.logic.Cancel((BusinessBaseForm)this.Parent);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.TeikiHinmeiIchiran.CellValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.TeikiHinmeiIchiran_CellValidating);
                this.TeikiHinmeiIchiran.RowValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.TeikiHinmeiIchiran_RowValidating);
                this.TsukiHinmeiIchiran.CellValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.TsukiHinmeiIchiran_CellValidating);
                this.TsukiHinmeiIchiran.RowValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.TsukiHinmeiIchiran_RowValidating);

                var parentForm = (BusinessBaseForm)this.Parent;

                this.Close();
                if (parentForm != null)
                {
                    parentForm.Close();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 一覧ボタン押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormSearch(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //// 一覧画面を表示する
                this.logic.ShowIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormSearch", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
            var copyMotoHensousaki = new MANIFESUTO_HENSOUSAKI();
            copyMotoHensousaki = this.logic.HensousakiCopy();

            // タブ全てをループ
            foreach (TabPage page in this.ManiHensousakiKeishou2B1.TabPages)
            {
                // 全ての返送先へコピー元を反映
                this.logic.HensousakiPaste(copyMotoHensousaki, page.Name);
            }

            // 完了メッセージ
            messageShowLogic.MessageBoxShow("I001", "コピー");
        }

        /// <summary>
        /// 現場CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 【新規】モードの場合のみチェック処理を行う
                if (base.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    bool catchErr = true;
                    var existGyousha = this.logic.ExistedGyousha(this.HIKIAI_GYOUSHA_USE_FLG.Text, this.GyoushaCode.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    var existGenba = this.logic.ExistedGenba(this.HIKIAI_GYOUSHA_USE_FLG.Text, this.GyoushaCode.Text, this.GenbaCode.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    if (existGyousha)
                    {
                        // 引合業者
                        MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                        if (existGenba)
                        {
                            // 本登録前(業者のみ本登録)
                            messageShowLogic.MessageBoxShow("E204", "既に業者が", "業者");
                        }
                        else
                        {
                            // 本登録後(業者と現場も本登録)
                            messageShowLogic.MessageBoxShow("E202", "既に");
                        }
                        e.Cancel = true;

                        return;
                    }
                    else
                    {
                        // 既存業者
                        MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                        if (existGenba)
                        {
                            // 本登録後(既存業者で現場が本登録)
                            messageShowLogic.MessageBoxShow("E202", "既に");
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaCode_Validating", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 現場CDフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaCode_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

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
                this.logic.HikiaiGyoushaUseFlg = this.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1") ? true : false;
                this.logic.GenbaCd = this.GenbaCode.Text;
                this.logic.GyoushaCd = this.GyoushaCode.Text;
                int count = this.logic.SearchGenba();
                if (count == -1)
                {
                    return;
                }
                if (count > 0)
                {
                    // 重複チェック
                    this.DupliUpdateViewCheck(e, false);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaCode_Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CD検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterGyoushaCode()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //業者チェックボックスセット
                this.logic.IsSetDataFromPopup = true;
                if (!this.logic.SearchchkGyousha(true, false))
                {
                    return;
                }
                if (this.ActiveControl != null)
                {
                    base.PreviousValue = this.ActiveControl.Text;
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 発行先チェック処理
                this.logic.HakkousakuCheck();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                //取引先CDが登録されているとGyoushaCDValidatedイベントが実行されない（？）為、ここで呼び出しています。
                this.logic.ManiCheckOffCheck(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterGyoushaCode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CD検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteGyoushaCode(object sender, DialogResult rlt)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, rlt);

                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //業者チェックボックスセット
                this.logic.IsSetDataFromPopup = true;
                if (!this.logic.SearchchkGyousha(true, false))
                {
                    return;
                }
                if (this.ActiveControl != null)
                {
                    base.PreviousValue = this.ActiveControl.Text;
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 発行先チェック処理
                this.logic.HakkousakuCheck();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                //取引先CDが登録されているとGyoushaCDValidatedイベントが実行されない（？）為、ここで呼び出しています。
                this.logic.ManiCheckOffCheck(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterExecuteGyoushaCode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GyoushaCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 【新規】モードの場合のみチェック処理を行う
                if (base.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 本登録済みデータチェック
                    bool catchErr = true;
                    bool isOk = this.logic.ExistedGyousha(this.HIKIAI_GYOUSHA_USE_FLG.Text, this.GyoushaCode.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isOk)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E204", "既に業者が", "業者");

                        e.Cancel = true;

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaCode_Validating", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CD TextChangeイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GyoushaCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //取引先拠点セット
                this.logic.ChangeGyousha();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaCode_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CD Validatedイベント
        /// </summary>
        internal virtual void GyoushaCDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.isError && this.GyoushaCode.Text.Equals(this.PreviousValue))
                {
                    return;
                }
                this.logic.isError = false;
                if (!this.logic.SearchchkGyousha(true, false))
                {
                    return;
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 発行先チェック処理
                this.logic.HakkousakuCheck();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                this.logic.ManiCheckOffCheck(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaCDValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterTorihikisakiCode()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //取引先拠点セット
                this.logic.IsSetDataFromPopup = true;
                if (this.logic.SearchsetTorihikisaki() == -1)
                {
                    return;
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                if (!this.TorihikisakiCode.Text.Equals(this.PreviousValue))
                {
                    this.logic.HakkousakuCheck();
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                if (this.ActiveControl != null)
                {
                    base.PreviousValue = this.ActiveControl.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterTorihikisakiCode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteTorihikisakiCode(object sender, DialogResult rlt)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, rlt);

                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //取引先拠点セット
                this.logic.IsSetDataFromPopup = true;
                if (this.logic.SearchsetTorihikisaki() == -1)
                {
                    return;
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                if (!this.TorihikisakiCode.Text.Equals(this.PreviousValue))
                {
                    this.logic.HakkousakuCheck();
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                if (this.ActiveControl != null)
                {
                    base.PreviousValue = this.ActiveControl.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterExecuteTorihikisakiCode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD TextChangeイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TorihikisakiCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //取引先拠点セット
                this.logic.ChangeTorihikisai();
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKISAKI_CD_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD Leaveイベント
        /// </summary>
        internal virtual void TorihikisakiCDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

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
                    this.logic.HakkousakuCheck();
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiCDValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送先取引先コード検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterManiHensousakiTorihikisakiCode(object sender, DialogResult rlt)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, rlt);

                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                this.logic.isError = false;
                this.logic.IsSetDataFromPopup = true;

                //アクティブコントロールを取得
                Control ctl = this.ActiveControl;

                //返送先(票)判定
                string hensousaki = string.Empty;

                hensousaki = this.logic.ChkTabEvent(ctl.Name);

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
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterManiHensousakiTorihikisakiCode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送先取引先CD Leaveイベント
        /// </summary>
        internal virtual void ManiTorihikisakiCDValidating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomAlphaNumTextBox ctlName = ((CustomAlphaNumTextBox)(sender));

                //返送先(票)判定
                string hensousaki = string.Empty;

                hensousaki = this.logic.ChkTabEvent(ctlName.Name);

                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.FindForm().Controls;

                //テキストボックス
                this.logic.ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensousaki);
                this.logic.ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensousaki);
                this.logic.ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensousaki);
                this.logic.ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensousaki);

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
            catch (Exception ex)
            {
                LogUtility.Error("ManiTorihikisakiCDValidating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送先業者コード検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterManiHensousakiGyoushaCode(object sender, DialogResult rlt)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, rlt);

                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                this.logic.isError = false;
                this.logic.IsSetDataFromPopup = true;

                //アクティブコントロールを取得
                Control ctl = this.ActiveControl;

                //返送先(票)判定
                string hensousaki = string.Empty;

                hensousaki = this.logic.ChkTabEvent(ctl.Name);

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
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterManiHensousakiGyoushaCode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送先業者CD Leaveイベント
        /// </summary>
        internal virtual void ManiGyoushaCDValidating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomAlphaNumTextBox ctlName = ((CustomAlphaNumTextBox)(sender));

                //返送先(票)判定
                string hensousaki = string.Empty;

                hensousaki = this.logic.ChkTabEvent(ctlName.Name);

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
                    //現場CDクリア
                    this.logic.ManiHensousakiGenbaCode.Text = string.Empty;
                    this.logic.ManiHensousakiGenbaName.Text = string.Empty;
                    return;
                }
                if (!this.logic.isError && cd.Equals(this.PreviousValue))
                {
                    return;
                }
                //現場CDクリア
                this.logic.ManiHensousakiGenbaCode.Text = string.Empty;
                this.logic.ManiHensousakiGenbaName.Text = string.Empty;
                //データを設定
                this.logic.isError = false;
                if (!string.IsNullOrWhiteSpace(cd))
                {
                    this.logic.SetManiHensousakiGyousha(cd, e, hensousaki);
                }
                else
                {
                    this.logic.ManiHensousakiGyoushaName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterManiHensousakiGyoushaCode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送先現場コード検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterManiHensousakiGenbaCode(object sender, DialogResult rlt)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, rlt);

                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                this.logic.isError = false;
                this.logic.IsSetDataFromPopup = true;
               
                //アクティブコントロールを取得
                Control ctl = this.ActiveControl;

                //返送先(票)判定
                string hensousaki = string.Empty;

                hensousaki = this.logic.ChkTabEvent(ctl.Name);

                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.FindForm().Controls;

                //テキストボックス
                this.logic.ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensousaki);
                this.logic.ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensousaki);// No3521
                this.logic.ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensousaki);
                this.logic.ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensousaki);

                string cd = this.logic.ManiHensousakiGenbaCode.Text;
                // No3521-->
                if (string.IsNullOrWhiteSpace(this.logic.ManiHensousakiGyoushaCode.Text))
                {
                    this.logic.ManiHensousakiGenbaName.Text = string.Empty;
                    return;
                }
                // No3521--<
                this.logic.ManiGyoushaCd = this.logic.ManiHensousakiGyoushaCode.Text;

                if (!string.IsNullOrWhiteSpace(cd))
                {
                    this.logic.SetManiHensousakiGenba(cd, null, hensousaki);
                }
                else
                {
                    this.logic.ManiHensousakiGenbaName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterManiHensousakiGenbaCode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送先現場CD Leaveイベント
        /// </summary>
        internal virtual void ManiGenbaCDValidating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomAlphaNumTextBox ctlName = ((CustomAlphaNumTextBox)(sender));

                //返送先(票)判定
                string hensousaki = string.Empty;

                hensousaki = this.logic.ChkTabEvent(ctlName.Name);

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
                if (string.IsNullOrWhiteSpace(cd))
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
                if (!this.logic.SetManiHensousakiGenba(cd, e, hensousaki))
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiGenbaCDValidating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TorihikisakiCd = this.TorihikisakiCode.Text;
                this.logic.TorihikisakiCopy();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_torihikisaki_copy_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先入力画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TorihikisakiNew_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 取引先画面を表示する
                this.logic.ShowTorihikisakiCreate();
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiNew_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 採番ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_genbacd_saiban_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

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
            catch (Exception ex)
            {
                LogUtility.Error("bt_genbacd_saiban_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopySeikyuButtonClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CopyToSeikyu();
                this.logic.CheckTextBoxLength(this.SeikyuuSoufuAddress1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopySeikyuButtonClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopySiharaiButtonClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CopyToSiharai();
                this.logic.CheckTextBoxLength(this.ShiharaiSoufuAddress1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopySiharaiButtonClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 分類取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopyManiButtonClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomButton ctlName = ((CustomButton)(sender));

                //返送先(票)判定
                string hensousaki = string.Empty;

                hensousaki = this.logic.ChkTabEvent(ctlName.Name);

                this.logic.CopyToMani();
                //this.logic.CopyToMani();
                //this.logic.CheckTextBoxLength(this.ManiHensousakiAddress1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyManiButtonClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 営業担当部署変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EigyouCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.isError && this.EigyouCode.Text.Equals(this.PreviousValue) && !this.EigyouCode.IsInputErrorOccured)
                {
                    return;
                }

                this.logic.isError = false;
                string cd = this.EigyouCode.Text;
                if (!string.IsNullOrWhiteSpace(cd))
                {
                    if (!this.logic.SetBushoData(cd))
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
            catch (Exception ex)
            {
                LogUtility.Error("EigyouCode_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ポップアップ用営業担当部署取得メソッド
        /// </summary>
        public void PopupAfterEigyouCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string cd = this.EigyouCode.Text;
                if (!string.IsNullOrWhiteSpace(cd))
                {
                    this.logic.SetBushoData(cd);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterEigyouCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ポップアップ用営業担当部署取得メソッド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteEigyouCd(object sender, DialogResult rlt)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, rlt);

                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                string cd = this.EigyouCode.Text;
                if (!string.IsNullOrWhiteSpace(cd))
                {
                    this.logic.SetBushoData(cd);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterExecuteEigyouCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 営業担当部署変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EigyouTantouBushoCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 【営業担当者CD】【営業担当者名】をクリアする。
                this.EigyouCode.Text = string.Empty;
                this.EigyouName.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EigyouTantouBushoCode_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ポップアップ用営業担当部署変更メソッド
        /// </summary>
        public void SetEigyouData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.EigyouCode.Text = string.Empty;
                this.EigyouName.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetEigyouData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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
            var useKbnTextBox = (CustomNumericTextBox2)sender;
            // 使用区分
            var useKbn = useKbnTextBox.Text;
            //返送先(票)判定
            var hyoName = this.logic.ChkTabEvent(useKbnTextBox.Name);

            this.logic.SetEnabledManifestHensousakiKbnRendou(hyoName);
        }

        // チェックボックス判定
        private void HaishutsuKbn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.FlgHaishutsuKbn = this.HaishutsuKbn.Checked;
                this.logic.ManiCheckOffCheck(true);
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
            catch (Exception ex)
            {
                LogUtility.Error("HaishutsuKbn_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニ返送先区分チェック変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKbn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.ManiHensousakiKbn_CheckedChanged();
                this.logic.ManiCheckOffCheck(true);
                this.logic.SettingHensouSakiKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiHensousakiKbn_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_gyousya_copy_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.GyoushaCd = this.GyoushaCode.Text;
                this.logic.GyousyaCopy();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_gyousya_copy_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者入力画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GyouhsaNew_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 業者画面を表示する
                this.logic.ShowGyoushaCreate();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyouhsaNew_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// セル表示形式変換処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItakuKeiyakuIchiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ChangeItakuStatus(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ItakuKeiyakuIchiran_CellFormatting", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 必須区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DummyKbnChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

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
                if (!this.logic.BunruiKbnChanged())
                {
                    return;
                }
                this.logic.ChangeManiKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("DummyKbnChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 都道府県コードテキスト変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaTodoufukenCode_TextChanged(object sender, EventArgs e)
        {
            //20150812 hoanghm edit #11919
            //// 都道府県チェック
            //this.CheckTodoufuken();
            //20150812 hoanghm end edit #11919
        }

        /// <summary>
        /// 都道府県チェック
        /// </summary>
        internal bool CheckTodoufuken()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                //20150812 hoanghm edit #11919
                //// ０埋め処理
                //if (!string.IsNullOrEmpty(this.GenbaTodoufukenCode.Text))
                //{
                //    var todoufukenCd = Convert.ToInt16(this.GenbaTodoufukenCode.Text);
                //    this.GenbaTodoufukenCode.Text = string.Format("{0:D2}", todoufukenCd);
                //}
                //20150812 hoanghm end edit #11919

                if (!this.logic.ChechChiiki(true))
                {
                    ret = false;
                    return false;
                }
                if (string.IsNullOrWhiteSpace(this.GenbaTodoufukenCode.Text))
                {
                    this.GenbaTodoufukenNameRyaku.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTodoufuken", ex);
                this.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 住所1フォーカス乖離イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaAddress1_Leave(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ChechChiiki(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaAddress1_Leave", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求拠点印字区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SEIKYUU_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ChangeSeikyuuKyotenPrintKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SEIKYUU_KYOTEN_PRINT_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払拠点印字区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ChangeShiharaiKyotenPrintKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_KYOTEN_PRINT_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.SetIchiranTeikiRowControl();

                this.logic.SetIchiranTsukiRowControl();
                // バリデーションをセットする
                this.logic.SetDynamicEvent();
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm_Shown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_UserRegistCheck(object sender, r_framework.Event.RegistCheckEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CheckRegist(sender, e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm_UserRegistCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期品名一覧セルフォーカスエンター処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CellEnter(object sender, CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TeikiHinmeiCellEnter(e);
                this.logic.TeikiHinmeiCellSwitchCdName(e, ConstCls.FocusSwitch.IN);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiIchiran_CellEnter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期品名一覧セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                this.logic.TeikiHinmeiCellFormatting(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiIchiran_CellFormatting", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期品名一覧値確定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CellValidated(object sender, CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TeikiHinmeiCellValidated(e);
                this.logic.TeikiHinmeiCellSwitchCdName(e, ConstCls.FocusSwitch.OUT);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiIchiran_CellValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期品名一覧値チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TeikiHinmeiIchiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TeikiHinmeiCellValidating(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiIchiran_CellValidating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期品名一覧値変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CellValueChanged(object sender, CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TeikiHinmeiCellValueChanged(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiIchiran_CellValueChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期品名一覧未確定データ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeikiHinmeiIchiran_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TeikiHinmeiDirtyStateChanged(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiIchiran_CurrentCellDirtyStateChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期品名一覧行値チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TeikiHinmeiIchiran_RowValidating(object sender, CellCancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TeikiHinmeiRowValidating(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiIchiran_RowValidating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 月極品名一覧セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsukiHinmeiIchiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                this.logic.TsukiHinmeiCellFormatting(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiIchiran_CellFormatting", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 月極品名一覧値チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TsukiHinmeiIchiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TsukiHinmeiCellValidating(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiIchiran_CellValidating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsukiHinmeiIchiran_CellEnter(object sender, CellEventArgs e)
        {
            this.logic.TsukiHinmeiCellSwitchCdName(e, ConstCls.FocusSwitch.IN);
        }

        /// <summary>
        /// 月極品名一覧値確定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsukiHinmeiIchiran_CellValidated(object sender, CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TsukiHinmeiCellValidated(e);
                this.logic.TsukiHinmeiCellSwitchCdName(e, ConstCls.FocusSwitch.OUT);
                this.logic.preCellIndex = e.CellIndex;
                this.logic.preRowIndex = e.RowIndex;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiIchiran_CellValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 月極品名一覧行値チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TsukiHinmeiIchiran_RowValidating(object sender, CellCancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TsukiHinmeiRowValidating(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiIchiran_RowValidating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送用取引先コード値変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ManiHensousakiTorihikisakiCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //アクティブコントロールを取得
                CustomTextBox ctl = ((CustomTextBox)(sender));
                //返送先(票)判定
                string hensousaki = string.Empty;
                hensousaki = this.logic.ChkTabEvent(ctl.Name);
                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.FindForm().Controls;

                //タブ内(A票～E票)のコントロールに紐付け
                //引合ユーザフラグ
                this.logic.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG" + hensousaki);
                this.logic.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG" + hensousaki);
                this.logic.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_GENBA_HIKIAI_FLG" + hensousaki);

                if (!this.logic.IsSetDataFromPopup)
                {
                    this.logic.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiHensousakiTorihikisakiCode_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送用業者コード値変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ManiHensousakiGyoushaCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //アクティブコントロールを取得
                CustomTextBox ctl = ((CustomTextBox)(sender));
                //返送先(票)判定
                string hensousaki = string.Empty;
                hensousaki = this.logic.ChkTabEvent(ctl.Name);
                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.FindForm().Controls;

                //タブ内(A票～E票)のコントロールに紐付け
                //引合ユーザフラグ
                this.logic.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG" + hensousaki);
                this.logic.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG" + hensousaki);
                this.logic.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_GENBA_HIKIAI_FLG" + hensousaki);

                if (!this.logic.IsSetDataFromPopup)
                {
                    this.logic.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiHensousakiGyoushaCode_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送用現場コード値変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ManiHensousakiGenbaCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //アクティブコントロールを取得
                CustomTextBox ctl = ((CustomTextBox)(sender));
                //返送先(票)判定
                string hensousaki = string.Empty;
                hensousaki = this.logic.ChkTabEvent(ctl.Name);
                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.FindForm().Controls;

                //タブ内(A票～E票)のコントロールに紐付け
                //引合ユーザフラグ
                this.logic.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG" + hensousaki);
                this.logic.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG" + hensousaki);
                this.logic.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_GENBA_HIKIAI_FLG" + hensousaki);

                if (!this.logic.IsSetDataFromPopup)
                {
                    this.logic.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiHensousakiGenbaCode_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.isError = false;
                if (!this.logic.SeikyuuKyotenCdValidated())
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SEIKYUU_KYOTEN_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KYOTEN_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.isError = false;
                if (!this.logic.ShiharaiKyotenCdValidated())
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_KYOTEN_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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

            //伝票ポップアップキャンセル時もHINMEI_CDに対してENTERイベントが発生していまうため、値が再保存されないように制御する。
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
                    if (beforeValuesForDetail.ContainsKey(Const.ConstCls.TEIKI_HINMEI_NAME_RYAKU))
                    {
                        beforeValuesForDetail[Const.ConstCls.TEIKI_HINMEI_NAME_RYAKU] = Convert.ToString(row.Cells[Const.ConstCls.TEIKI_HINMEI_NAME_RYAKU].Value);
                    }
                    else
                    {
                        beforeValuesForDetail.Add(Const.ConstCls.TEIKI_HINMEI_NAME_RYAKU, Convert.ToString(row.Cells[Const.ConstCls.TEIKI_HINMEI_NAME_RYAKU].Value));
                    }
                }
                // 伝票区分ポップアップ起動抑制用
                FlgDenpyouKbn = false;
            }

            //月極情報タブ
            if (this.ActiveControl.Parent.Text.Equals("月極情報"))
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

            //定期回収情報
            //新規モードまたは、新行の場合、削除フラグチェック不可設定
            if (this.ActiveControl.Parent.Text.Equals("定期回収情報"))
            {
                //新行の場合、削除フラグチェック不可設定
                if (this.TeikiHinmeiIchiran.Rows[e.RowIndex].IsNewRow)
                {
                    this.TeikiHinmeiIchiran.Rows[e.RowIndex][0].Selectable = false;
                }
                else
                {
                    this.TeikiHinmeiIchiran.Rows[e.RowIndex][0].Selectable = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 社員部署の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EigyouTantouBushoCode_Validating(object sender, CancelEventArgs e)
        {
            this.logic.isError = false;
            if (!this.logic.BushoCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 登録時の必須チェック
        /// </summary>
        /// <param name="hensouCd">返送先CD</param>
        /// <returns>エラーメッセージ</returns>
        private string RegistCheck(string hensouCd, out bool catchErr)
        {
            string errMsg = "";
            catchErr = true;
            try
            {
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
                    if (string.Empty.Equals(this.logic.HensousakiKbn.Text))
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
                        if (string.IsNullOrEmpty(this.logic.ManiHensousakiGenbaCode.Text))
                        {
                            errMsg = "返送先現場CD";
                        }
                    }
                }

                string tabName = hensouCd.Substring(1, hensouCd.Length - 4);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    errMsg = tabName + "票" + errMsg;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
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
            var hyoName = this.logic.ChkTabEvent(useKbnTextBox.Name);

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
        /// タブページ変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKeishou2B1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // サブファンクションの活性・非活性を切り替えます
            this.logic.SubFunctionEnabledChenge();
        }

        // 20141209 katen #2927 実績報告書　フィードバック対応 start
        internal void UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.isError = false;
            // 運搬報告書提出先拠点セット
            if (!this.logic.SearchsetUpanHoukokushoTeishutsu())
            {
                e.Cancel = true;
            }
        }

        // 20141209 katen #2927 実績報告書　フィードバック対応 end

        /// <summary>
        /// 都道府県ポップアップから戻ってきた後に実行されます
        /// </summary>
        public void TodoufukenPopupAfter()
        {
            // 都道府県チェック
            this.CheckTodoufuken();
        }

        #region 20150812 hoanghm edit #11919

        private void GenbaTodoufukenCode_Validating(object sender, CancelEventArgs e)
        {
            // 都道府県チェック
            this.CheckTodoufuken();
        }

        #endregion

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
            var hyoName = this.logic.ChkTabEvent(useKbnTextBox.Name);

            // マニフェスト返送先のチェック状態に応じて状態変更
            this.logic.HENSOUSAKI_PLACE_KBN_TextChanged(hyoName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            this.beforeValuesForDetail[ConstCls.TSUKI_HINMEI_CD] = string.Empty;
        }

        private void TsukiHinmeiIchiran_CellContentDoubleClick(object sender, CellEventArgs e)
        {
            this.logic.TsukiHinmeiIchiran_CellContentClick(e);
        }

        private void TsukiHinmeiIchiran_CurrentCellChanged(object sender, EventArgs e)
        {
            this.logic.TsukiHinmeiIchiran_CurrentCellChanged(e);
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
            this.logic.OpenHikiaiGyoushaFormReference(this.GyoushaCode.Text);
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
            this.logic.OpenHikiaiTorihikisakiFormReference(this.TorihikisakiCode.Text);
        } 
    }
}