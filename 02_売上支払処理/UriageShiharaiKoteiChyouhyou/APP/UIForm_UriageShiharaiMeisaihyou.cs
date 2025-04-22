// $Id:
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou.DAO;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    /// <summary>
    /// 売上/支払明細表出力指定画面を表すクラス・コントロール
    /// </summary>
    public partial class UIForm_UriageShiharaiMeisaihyou : SuperForm
    {
        #region - Fields -

        /// <summary>画面ロジック</summary>
        private UriageShiharaiMeisaihyouLogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="UIForm_UriageShiharaiMeisaihyou" /> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public UIForm_UriageShiharaiMeisaihyou(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new UriageShiharaiMeisaihyouLogicClass(this);

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool Initialize()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 拠点CD
                this.KYOTEN_CD.Text = "99";
                // 拠点
                this.KYOTEN_NAME_RYAKU.Text = "全社";
                // 入力担当者CD
                this.NYUURYOKU_TANTOUSHA_CD.Text = String.Empty;
                // 入力担当者
                this.NYUURYOKU_TANTOUSHA_NAME.Text = String.Empty;
                // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) Start
                // 伝票種類CD
                this.DENPYOU_SHURUI.Text = "5";
                // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) End
                // 日付CD
                this.HIDUKE_SHURUI.Text = "1";
                // 日付範囲
                this.HIDUKE.Text = "1";
                // 日付From
                this.HIDUKE_FROM.Text = String.Empty;
                // 日付To
                this.HIDUKE_TO.Text = String.Empty;
                // 締処理状況CD
                this.SHIME.Text = "3";
                // 確定区分CD
                this.KAKUTEI_KBN.Text = "3";
                // 取引区分CD
                this.TORIHIKI_KBN.Text = "3";
                // 並び順
                this.ORDER.Text = "1";

                if (this.WindowId == WINDOW_ID.T_URIAGE_MEISAIHYOU_KOTEI)
                {
                    this.customRadioButtonHiduke2.Text = "2.売上日付";
                    // 20150513 タグ修正 Start
                    this.customRadioButtonHiduke2.Tag = "売上日付を選択する場合にチェックを付けてください";
                    // 20150513 タグ修正 End
                }
                else
                {
                    this.customRadioButtonHiduke2.Text = "2.支払日付";
                    // 20150513 タグ修正 Start
                    this.customRadioButtonHiduke2.Tag = "支払日付を選択する場合にチェックを付けてください";
                    // 20150513 タグ修正 End
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Initialize", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #region - キー処理 -
        
        /// <summary>
        /// 「F5 CSV出力ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.logic.mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                Cursor.Current = Cursors.WaitCursor;

                if (this.RegistErrorFlag)
                {
                    Cursor.Current = Cursors.Arrow;

                    return;
                }

                if (this.customRadioButtonHidukeHaniShitei3.Checked)
                {
                    if (this.logic.CheckDate())
                    {
                        return;
                    }
                }

                if (!this.SetTorihikisakiCdFromTo())
                {
                    return;
                }
                if (!this.SetGyoushaCdFromTo())
                {
                    return;
                }

                if (!this.ChangeGenbaCdTextBoxEnabled())
                {
                    return;
                }

                // FromToのチェックがうまくいかないので自前でチェックする
                var errMsg = String.Empty;
                bool catchErr = true;
                bool isOk1 = this.CheckTorihikisakiCdFromTo(out catchErr);
                if (!catchErr)
                {
                    return;
                }
                bool isOk2 = this.CheckGyoushaCdFromTo(out catchErr);
                if (!catchErr)
                {
                    return;
                }
                bool isOk3 = this.CheckGenbaCdFromTo(out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (!isOk1)
                {
                    errMsg = "取引先";
                }
                else if (!isOk2)
                {
                    errMsg = "業者";
                }
                else if (!isOk3)
                {
                    errMsg = "現場";
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    this.logic.errmessage.MessageBoxShow("E032", errMsg + "From", errMsg + "To");

                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                // CSV出力へ
                var dto = CreateUriageShiharaiMeisaihyouDto();
                var dao = DaoInitUtility.GetComponent<IUriageShiharaiMeisaihyouDao>();
                var SearchDetailResult = dao.GetUriageShiharaiMeisaiData(dto); ;

                this.logic.CSVPrint(SearchDetailResult, dto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func5_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>Ｆ７キー（表示）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Cursor.Current = Cursors.WaitCursor;

            if (this.RegistErrorFlag)
            {
                Cursor.Current = Cursors.Arrow;

                return;
            }

            // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
            if (this.customRadioButtonHidukeHaniShitei3.Checked)
            {
                if (this.logic.CheckDate())
                {
                    return;
                }
            }
            // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

            // 20140703 syunrei EV004143_売上支払明細表(固定帳票)で取引先、業者、現場のFromまたはToのどちらかだけを入れて[F7]表示を行った場合、アラートが表示される　start

            // 取引先CD（From、Toともに）に入力が無い場合は、Fromに取引先CDの最小値、Toに最大値をセット
            //// 取引先CDのFromまたはToのどちらか一方に入力が無い場合はエラー
            //if (!this.SetTorihikisakiCdFromTo())
            //{
            //    Cursor.Current = Cursors.Arrow;
            //    return;
            //}

            // 業者CD（From、Toともに）に入力が無い場合は、Fromに業者CDの最小値、Toに最大値をセット
            //// 業者CDのFromまたはToのどちらか一方に入力が無い場合はエラー
            //if (!this.SetGyoushaCdFromTo())
            //{
            //    Cursor.Current = Cursors.Arrow;
            //    return;
            //}

            if (!this.SetTorihikisakiCdFromTo())
            {
                return;
            }
            if (!this.SetGyoushaCdFromTo())
            {
                return;
            }
            // 20140703 syunrei EV004143_売上支払明細表(固定帳票)で取引先、業者、現場のFromまたはToのどちらかだけを入れて[F7]表示を行った場合、アラートが表示される　end

            if (!this.ChangeGenbaCdTextBoxEnabled())
            {
                return;
            }

            // FromToのチェックがうまくいかないので自前でチェックする
            var errMsg = String.Empty;
            bool catchErr = true;
            bool isOk1 = this.CheckTorihikisakiCdFromTo(out catchErr);
            if (!catchErr)
            {
                return;
            }
            bool isOk2 = this.CheckGyoushaCdFromTo(out catchErr);
            if (!catchErr)
            {
                return;
            }
            bool isOk3 = this.CheckGenbaCdFromTo(out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (!isOk1)
            {
                errMsg = "取引先";
            }
            else if (!isOk2)
            {
                errMsg = "業者";
            }
            else if (!isOk3)
            {
                errMsg = "現場";
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E032", errMsg + "From", errMsg + "To");

                Cursor.Current = Cursors.Arrow;
                return;
            }

            Cursor.Current = Cursors.Arrow;
            var dto = CreateUriageShiharaiMeisaihyouDto();
            this.logic.Search(dto);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>DTO作成</summary>
        /// <param name=""></param>
        private UriageShiharaiMeisaihyouDtoClass CreateUriageShiharaiMeisaihyouDto()
        {
            var dto = new UriageShiharaiMeisaihyouDtoClass();
            dto.KyotenCd = int.Parse(this.KYOTEN_CD.Text);
            dto.KyotenName = this.KYOTEN_NAME_RYAKU.Text;
            dto.NyuuryokuTantoushaCd = this.NYUURYOKU_TANTOUSHA_CD.Text;
            dto.NyuuryokuTantousyaName = this.NYUURYOKU_TANTOUSHA_NAME.Text;

            if (this.WindowId == WINDOW_ID.T_URIAGE_MEISAIHYOU_KOTEI)
            {
                dto.DenpyouKbnCd = 1;
            }
            else
            {
                dto.DenpyouKbnCd = 2;
            }

            dto.DenpyouShuruiCd = int.Parse(this.DENPYOU_SHURUI.Text);
            dto.DateShuruiCd = int.Parse(this.HIDUKE_SHURUI.Text);

            // 日付範囲の選択状態で日付条件を設定
            if (this.customRadioButtonHidukeHaniShitei1.Checked)
            {
                dto.DateFrom = this.logic.parentForm.sysDate.ToString("yyyy/MM/dd");
                dto.DateTo = this.logic.parentForm.sysDate.ToString("yyyy/MM/dd");
            }
            else if (this.customRadioButtonHidukeHaniShitei2.Checked)
            {
                dto.DateFrom = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, 1).ToString("yyyy/MM/dd");
                dto.DateTo = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd"); ;
            }
            else if (this.customRadioButtonHidukeHaniShitei3.Checked)
            {
                dto.DateFrom = Convert.ToDateTime(this.HIDUKE_FROM.Value).ToString("yyyy/MM/dd");
                dto.DateTo = Convert.ToDateTime(this.HIDUKE_TO.Value).ToString("yyyy/MM/dd");
            }

            dto.ShimeJoukyouCd = int.Parse(this.SHIME.Text);
            dto.KakuteiKbnCd = int.Parse(this.KAKUTEI_KBN.Text);
            dto.TorihikiKbnCd = int.Parse(this.TORIHIKI_KBN.Text);
            dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
            dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
            dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
            dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
            dto.GyoushaCdFrom = this.GYOUSHA_CD_FROM.Text;
            dto.GyoushaCdTo = this.GYOUSHA_CD_TO.Text;
            dto.GyoushaFrom = this.GYOUSHA_NAME_FROM.Text;
            dto.GyoushaTo = this.GYOUSHA_NAME_TO.Text;

            // 現場はテキストボックスが使用不可の場合、条件なしとする
            if (this.GENBA_CD_FROM.Enabled)
            {
                dto.GenbaCdFrom = this.GENBA_CD_FROM.Text;
                dto.GenbaCdTo = this.GENBA_CD_TO.Text;
                dto.GenbaFrom = this.GENBA_NAME_FROM.Text;
                dto.GenbaTo = this.GENBA_NAME_TO.Text;
            }

            dto.Order = int.Parse(this.ORDER.Text);
            dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;
            dto.IsGroupGyousha = this.GROUP_GYOUSHA.Checked;
            dto.IsGroupGenba = this.GROUP_GENBA.Checked;
            dto.IsGroupDenpyouNumber = this.GROUP_DENPYOU_NUMBER.Checked;

            return dto;
        }

        /// <summary>
        /// 取引先CDのFromToの関係をチェックします
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool CheckTorihikisakiCdFromTo(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            catchErr = true;

            try
            {
                var cdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                var cdTo = this.TORIHIKISAKI_CD_TO.Text;
                if (!String.IsNullOrEmpty(cdFrom) && !String.IsNullOrEmpty(cdTo))
                {
                    if (0 < cdFrom.CompareTo(cdTo))
                    {
                        this.TORIHIKISAKI_CD_FROM.IsInputErrorOccured = true;
                        this.TORIHIKISAKI_CD_TO.IsInputErrorOccured = true;

                        this.TORIHIKISAKI_CD_FROM.Focus();

                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiCdFromTo", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 業者CDのFromToの関係をチェックします
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool CheckGyoushaCdFromTo(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            catchErr = true;

            try
            {
                var cdFrom = this.GYOUSHA_CD_FROM.Text;
                var cdTo = this.GYOUSHA_CD_TO.Text;
                if (!String.IsNullOrEmpty(cdFrom) && !String.IsNullOrEmpty(cdTo))
                {
                    if (0 < cdFrom.CompareTo(cdTo))
                    {
                        this.GYOUSHA_CD_FROM.IsInputErrorOccured = true;
                        this.GYOUSHA_CD_TO.IsInputErrorOccured = true;

                        this.GYOUSHA_CD_FROM.Focus();

                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaCdFromTo", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 現場CDのFromToの関係をチェックします
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool CheckGenbaCdFromTo(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            catchErr = true;

            try
            {
                var cdFrom = this.GENBA_CD_FROM.Text;
                var cdTo = this.GENBA_CD_TO.Text;
                if (!String.IsNullOrEmpty(cdFrom) && !String.IsNullOrEmpty(cdTo))
                {
                    if (0 < cdFrom.CompareTo(cdTo))
                    {
                        this.GENBA_CD_FROM.IsInputErrorOccured = true;
                        this.GENBA_CD_TO.IsInputErrorOccured = true;

                        this.GENBA_CD_FROM.Focus();

                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenbaCdFromTo", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>Ｆ１２キー（閉じる）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion - キー処理 -

        /// <summary>画面Load処理</summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.logic.WindowInit())
            {
                return;
            }

            // 初期化処理
            this.Initialize();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 期間指定ラジオボタンのチェック状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void RadioButtonHidukeHaniShitei_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.HIDUKE_FROM.Text = String.Empty;
            this.HIDUKE_TO.Text = String.Empty;

            if (this.customRadioButtonHidukeHaniShitei3.Checked)
            {
                // 日付範囲指定テキストボックスに（登録時）必須チェックを追加する
                this.HIDUKE_FROM.RegistCheckMethod = new Collection<SelectCheckDto>() { new SelectCheckDto("必須チェック") };
                this.HIDUKE_TO.RegistCheckMethod = new Collection<SelectCheckDto>() { new SelectCheckDto("必須チェック") };
                this.customPanelHidukeHaniShitei.Enabled = true;
            }
            else
            {
                // 日付範囲指定テキストボックスの必須チェックをはずす
                this.HIDUKE_FROM.RegistCheckMethod = new Collection<SelectCheckDto>();
                this.HIDUKE_TO.RegistCheckMethod = new Collection<SelectCheckDto>();

                this.customPanelHidukeHaniShitei.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion - Methods -

        /// <summary>
        /// 現場CDFromのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCd = this.GYOUSHA_CD_FROM.Text;
            var genbaCd = this.GENBA_CD_FROM.Text;

            if (String.IsNullOrEmpty(genbaCd))
            {
                this.GENBA_NAME_FROM.Text = String.Empty;
            }
            else if (!String.IsNullOrEmpty(genbaCd))
            {
                if (!this.CheckGyoushaCd())
                {
                    e.Cancel = true;
                }
                else if (!this.CheckGenba(gyoushaCd, genbaCd, true))
                {
                    e.Cancel = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDToのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCd = this.GYOUSHA_CD_TO.Text;
            var genbaCd = this.GENBA_CD_TO.Text;

            if (String.IsNullOrEmpty(genbaCd))
            {
                this.GENBA_NAME_TO.Text = String.Empty;
            }
            else if (!String.IsNullOrEmpty(genbaCd))
            {
                if (!this.CheckGyoushaCd())
                {
                    e.Cancel = true;
                }
                else if (!this.CheckGenba(gyoushaCd, genbaCd, false))
                {
                    e.Cancel = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力された現場CDに対応する現場が存在するかチェックします
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="isFromCd">From側の現場CDをチェックする場合はTrue</param>
        /// <returns>チェック結果</returns>
        private bool CheckGenba(string gyoushaCd, string genbaCd, bool isFromCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd, isFromCd);

            bool ret = true;

            try
            {
                var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                var mGenbaList = genbaDao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd, ISNOT_NEED_DELETE_FLG = true });
                if (0 == mGenbaList.Count())
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");

                    if (isFromCd)
                    {
                        this.GENBA_NAME_FROM.Text = String.Empty;
                    }
                    else
                    {
                        this.GENBA_NAME_TO.Text = String.Empty;
                    }

                    ret = false;
                }
                else
                {
                    if (isFromCd)
                    {
                        this.GENBA_NAME_FROM.Text = mGenbaList.FirstOrDefault().GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.GENBA_NAME_TO.Text = mGenbaList.FirstOrDefault().GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGenba", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        // 201400703 syunrei EV004143_売上支払明細表(固定帳票)で取引先、業者、現場のFromまたはToのどちらかだけを入れて[F7]表示を行った場合、アラートが表示される　start

        ///// <summary>
        ///// 取引先CDの最大値と最小値をセット（入力が無い場合）
        ///// </summary>
        //private bool SetTorihikisakiCdFromTo()
        //{
        //    LogUtility.DebugMethodStart();

        //    bool ret = false;
        //    var torihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM;
        //    var torihikisakiCdTo = this.TORIHIKISAKI_CD_TO;
        //    var msgLogic = new MessageBoxShowLogic();

        //    if (String.IsNullOrEmpty(torihikisakiCdFrom.Text) && String.IsNullOrEmpty(torihikisakiCdTo.Text))
        //    {
        //        // From、Toの両方に入力が無い場合は、CDの最大値と最小値をセット
        //        var dt = this.logic.GetMinMaxKeyTorihikisaki();
        //        var row = dt.Rows[0];
        //        torihikisakiCdFrom.Text = row["RETURN_VALUE_MIN"].ToString();
        //        this.CheckTorihikisaki(torihikisakiCdFrom.Text, true);
        //        torihikisakiCdTo.Text = row["RETURN_VALUE_MAX"].ToString();
        //        this.CheckTorihikisaki(torihikisakiCdTo.Text, false);
        //        ret = true;
        //    }
        //    else if (String.IsNullOrEmpty(torihikisakiCdFrom.Text))
        //    {
        //        // From入力なし
        //        torihikisakiCdFrom.IsInputErrorOccured = true;
        //        msgLogic.MessageBoxShow("E012", "取引先From");
        //        torihikisakiCdTo.Focus();
        //    }
        //    else if (String.IsNullOrEmpty(torihikisakiCdTo.Text))
        //    {
        //        // To入力なし
        //        torihikisakiCdTo.IsInputErrorOccured = true;
        //        msgLogic.MessageBoxShow("E012", "取引先To");
        //        torihikisakiCdTo.Focus();
        //    }
        //    else
        //    {
        //        ret = true;
        //    }

        //    LogUtility.DebugMethodEnd();
        //    return ret;
        //}

        ///// <summary>
        ///// 業者CDの最大値と最小値をセット（入力が無い場合）
        ///// </summary>
        //private bool SetGyoushaCdFromTo()
        //{
        //    LogUtility.DebugMethodStart();

        //    bool ret = false;
        //    var gyoushaCdFrom = this.GYOUSHA_CD_FROM;
        //    var gyoushaCdTo = this.GYOUSHA_CD_TO;
        //    var msgLogic = new MessageBoxShowLogic();

        //    if (String.IsNullOrEmpty(gyoushaCdFrom.Text) && String.IsNullOrEmpty(gyoushaCdTo.Text))
        //    {
        //        // From、Toの両方に入力が無い場合は、CDの最大値と最小値をセット
        //        var dt = this.logic.GetMinMaxKeyGyousha();
        //        var row = dt.Rows[0];
        //        gyoushaCdFrom.Text = row["RETURN_VALUE_MIN"].ToString();
        //        this.CheckGyousha(gyoushaCdFrom.Text, true);
        //        gyoushaCdTo.Text = row["RETURN_VALUE_MAX"].ToString();
        //        this.CheckGyousha(gyoushaCdTo.Text, false);
        //        ret = true;
        //    }
        //    else if (String.IsNullOrEmpty(gyoushaCdFrom.Text))
        //    {
        //        // From入力なし
        //        gyoushaCdFrom.IsInputErrorOccured = true;
        //        msgLogic.MessageBoxShow("E012", "業者From");
        //        gyoushaCdFrom.Focus();
        //    }
        //    else if (String.IsNullOrEmpty(gyoushaCdTo.Text))
        //    {
        //        // To入力なし
        //        gyoushaCdTo.IsInputErrorOccured = true;
        //        msgLogic.MessageBoxShow("E012", "業者To");
        //        gyoushaCdTo.Focus();
        //    }
        //    else
        //    {
        //        ret = true;
        //    }

        //    LogUtility.DebugMethodEnd();
        //    return ret;
        //}

        /// <summary>
        /// 業者CDの最大値と最小値をセット（入力が無い場合）
        /// </summary>
        private bool SetGyoushaCdFromTo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                IEnumerable<M_GYOUSHA> mGyoushaList = dao.GetAllValidData(new M_GYOUSHA());

                if (mGyoushaList.Count() > 0)
                {
                    var minGyousha = mGyoushaList.Where(g => g.GYOUSHA_CD == mGyoushaList.Min(gy => gy.GYOUSHA_CD)).FirstOrDefault();
                    var maxGyousha = mGyoushaList.OrderByDescending(m => m.GYOUSHA_CD).Where(g => g.GYOUSHA_CD == mGyoushaList.Max(gy => gy.GYOUSHA_CD)).FirstOrDefault();

                    if (String.IsNullOrEmpty(this.GYOUSHA_CD_FROM.Text))
                    {
                        this.GYOUSHA_CD_FROM.Text = minGyousha.GYOUSHA_CD;
                        this.GYOUSHA_NAME_FROM.Text = minGyousha.GYOUSHA_NAME_RYAKU;
                    }

                    if (String.IsNullOrEmpty(this.GYOUSHA_CD_TO.Text))
                    {
                        this.GYOUSHA_CD_TO.Text = maxGyousha.GYOUSHA_CD;
                        this.GYOUSHA_NAME_TO.Text = maxGyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGyoushaCdFromTo", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyoushaCdFromTo", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 取引先のFromとToに最小値と最大値をセットします
        /// </summary>
        private bool SetTorihikisakiCdFromTo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                var mTorihikisakiArray = dao.GetAllValidData(new M_TORIHIKISAKI());

                IEnumerable<M_TORIHIKISAKI> mTorihikisakiList = new List<M_TORIHIKISAKI>();
                mTorihikisakiList = mTorihikisakiArray;
                if (mTorihikisakiList.Count() > 0)
                {
                    var minTorihikisaki = mTorihikisakiList.Where(g => g.TORIHIKISAKI_CD == mTorihikisakiList.Min(gy => gy.TORIHIKISAKI_CD)).FirstOrDefault();
                    var maxTorihikisaki = mTorihikisakiList.OrderByDescending(m => m.TORIHIKISAKI_CD).Where(g => g.TORIHIKISAKI_CD == mTorihikisakiList.Max(gy => gy.TORIHIKISAKI_CD)).FirstOrDefault();

                    if (String.IsNullOrEmpty(this.TORIHIKISAKI_CD_FROM.Text))
                    {
                        this.TORIHIKISAKI_CD_FROM.Text = minTorihikisaki.TORIHIKISAKI_CD;
                        this.TORIHIKISAKI_NAME_FROM.Text = minTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }

                    if (String.IsNullOrEmpty(this.TORIHIKISAKI_CD_TO.Text))
                    {
                        this.TORIHIKISAKI_CD_TO.Text = maxTorihikisaki.TORIHIKISAKI_CD;
                        this.TORIHIKISAKI_NAME_TO.Text = maxTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetTorihikisakiCdFromTo", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisakiCdFromTo", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        // 201400703 syunrei EV004143_売上支払明細表(固定帳票)で取引先、業者、現場のFromまたはToのどちらかだけを入れて[F7]表示を行った場合、アラートが表示される　end

        /// <summary>
        /// 業者CDが入力されているかをチェックします
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool CheckGyoushaCd()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;

            try
            {
                var gyoushaCdFrom = this.GYOUSHA_CD_FROM.Text;
                var gyoushaCdTo = this.GYOUSHA_CD_TO.Text;

                if (String.IsNullOrEmpty(gyoushaCdFrom) && String.IsNullOrEmpty(gyoushaCdTo))
                {
                    ret = false;

                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E012", "業者CD");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaCd", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 取引先CD入力チェック
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="isStartCd"></param>
        /// <returns></returns>
        private bool CheckTorihikisaki(string torihikisakiCd, bool isStartCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd, isStartCd);

            bool ret = true;

            var torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            var mTorihikisakiList = torihikisakiDao.GetAllValidData(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = torihikisakiCd });
            if (0 == mTorihikisakiList.Count())
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "取引先");

                if (isStartCd)
                {
                    this.TORIHIKISAKI_NAME_FROM.Text = String.Empty;
                }
                else
                {
                    this.TORIHIKISAKI_NAME_TO.Text = String.Empty;
                }

                ret = false;
            }
            else
            {
                if (isStartCd)
                {
                    this.TORIHIKISAKI_NAME_FROM.Text = mTorihikisakiList.FirstOrDefault().TORIHIKISAKI_NAME_RYAKU;
                }
                else
                {
                    this.TORIHIKISAKI_NAME_TO.Text = mTorihikisakiList.FirstOrDefault().TORIHIKISAKI_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 業者CD入力チェック
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="isStartCd"></param>
        /// <returns></returns>
        private bool CheckGyousha(string gyoushaCd, bool isStartCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, isStartCd);

            bool ret = true;

            var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            var mGyoushaList = gyoushaDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = gyoushaCd });
            if (0 == mGyoushaList.Count())
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");

                if (isStartCd)
                {
                    this.GYOUSHA_NAME_FROM.Text = String.Empty;
                }
                else
                {
                    this.GYOUSHA_NAME_TO.Text = String.Empty;
                }

                ret = false;
            }
            else
            {
                if (isStartCd)
                {
                    this.GYOUSHA_NAME_FROM.Text = mGyoushaList.FirstOrDefault().GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.GYOUSHA_NAME_TO.Text = mGyoushaList.FirstOrDefault().GYOUSHA_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 業者CDFromのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDToのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_TO_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDの入力状態に応じて現場CDテキストボックスの活性状態を変更します
        /// </summary>
        private bool ChangeGenbaCdTextBoxEnabled()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var gyoushaCdFrom = this.GYOUSHA_CD_FROM.Text;
                var gyoushaCdTo = this.GYOUSHA_CD_TO.Text;

                if (String.IsNullOrEmpty(gyoushaCdFrom) == false && String.IsNullOrEmpty(gyoushaCdTo) == false && this.ZeroSuppressGenbaCd(gyoushaCdFrom) == this.ZeroSuppressGenbaCd(gyoushaCdTo))
                {
                    // 現場CDテキストボックスの活性状態を初期化
                    this.GENBA_CD_FROM.Enabled = true;
                    this.GENBA_NAME_FROM.Enabled = true;
                    this.customPopupOpenButtonSyukeiKomoku3StartMeishoSearch.Enabled = true;
                    this.GENBA_CD_TO.Enabled = true;
                    this.GENBA_NAME_TO.Enabled = true;
                    this.customPopupOpenButtonSyukeiKomoku3EndMeishoSearch.Enabled = true;
                }
                else
                {
                    this.GENBA_CD_FROM.Enabled = false;
                    this.GENBA_NAME_FROM.Enabled = false;
                    this.customPopupOpenButtonSyukeiKomoku3StartMeishoSearch.Enabled = false;
                    this.GENBA_CD_TO.Enabled = false;
                    this.GENBA_NAME_TO.Enabled = false;
                    this.customPopupOpenButtonSyukeiKomoku3EndMeishoSearch.Enabled = false;

                    this.GENBA_CD_FROM.Text = String.Empty;
                    this.GENBA_NAME_FROM.Text = String.Empty;
                    this.GENBA_CD_TO.Text = String.Empty;
                    this.GENBA_NAME_TO.Text = String.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeGenbaCdTextBoxEnabled", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 現場CDのゼロ埋め処理を行います
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>ゼロ埋めした現場CD</returns>
        private String ZeroSuppressGenbaCd(String genbaCd)
        {
            LogUtility.DebugMethodStart(genbaCd);

            var ret = String.Empty;
            if (String.IsNullOrEmpty(genbaCd) == false)
            {
                ret = genbaCd.ToUpper().PadLeft(6, '0');
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 取引先CD順のラジオボタンのチェック状態が切り替わったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonSort1_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGroupCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先フリガナ順のラジオボタンのチェック状態が切り替わったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonSort2_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGroupCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票日付順のラジオボタンのチェック状態が切り替わったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonSort3_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGroupCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票番号順のラジオボタンのチェック状態が切り替わったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonSort4_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGroupCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 並び順の選択によって、集計単位のチェックボックスの活性or不活性を切り替えます
        /// </summary>
        private void ChangeGroupCheckBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            if (this.customRadioButtonSort1.Checked || this.customRadioButtonSort2.Checked)
            {
                this.GROUP_TORIHIKISAKI.Enabled = true;
                this.GROUP_GYOUSHA.Enabled = true;
                this.GROUP_GENBA.Enabled = true;
                this.GROUP_DENPYOU_NUMBER.Enabled = true;

                this.GROUP_TORIHIKISAKI.Checked = true;
                this.GROUP_GYOUSHA.Checked = true;
                this.GROUP_GENBA.Checked = true;
                this.GROUP_DENPYOU_NUMBER.Checked = true;
            }
            else if (this.customRadioButtonSort3.Checked || this.customRadioButtonSort4.Checked)
            {
                this.GROUP_TORIHIKISAKI.Enabled = false;
                this.GROUP_GYOUSHA.Enabled = false;
                this.GROUP_GENBA.Enabled = false;
                this.GROUP_DENPYOU_NUMBER.Enabled = true;

                this.GROUP_TORIHIKISAKI.Checked = false;
                this.GROUP_GYOUSHA.Checked = false;
                this.GROUP_GENBA.Checked = false;
                this.GROUP_DENPYOU_NUMBER.Checked = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者検索ボタン（From）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonSyukeiKomoku2StartMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.ChangeGenbaCdTextBoxEnabled();
        }

        /// <summary>
        /// 業者検索ボタン（To）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonSyukeiKomoku2EndMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.ChangeGenbaCdTextBoxEnabled();
        }

        /// <summary>
        /// 日付Toテキストボックスでダブルクリックしたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HIDUKE_TO_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (String.IsNullOrEmpty(this.HIDUKE_TO.Text))
            //{
            this.HIDUKE_TO.Text = this.HIDUKE_FROM.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
            LogUtility.DebugMethodEnd();
        }

        private void TORIHIKISAKI_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD_TO.Text))
            //{
            this.TORIHIKISAKI_CD_TO.Text = this.TORIHIKISAKI_CD_FROM.Text;
            this.TORIHIKISAKI_NAME_TO.Text = this.TORIHIKISAKI_NAME_FROM.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        private void GYOUSHA_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.GYOUSHA_CD_TO.Text))
            //{
            this.GYOUSHA_CD_TO.Text = this.GYOUSHA_CD_FROM.Text;
            this.GYOUSHA_NAME_TO.Text = this.GYOUSHA_NAME_FROM.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        private void GENBA_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.GENBA_CD_TO.Text))
            //{
            this.GENBA_CD_TO.Text = this.GENBA_CD_FROM.Text;
            this.GENBA_NAME_TO.Text = this.GENBA_NAME_FROM.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        /// <summary>
        /// 取引区分（現金）のラジオボタンのチェック状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonTorihikiKubunShitei1_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeShimeShoriJokyoCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引区分（掛け）のラジオボタンのチェック状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonTorihikiKubunShitei2_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeShimeShoriJokyoCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引区分（全て）のラジオボタンのチェック状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonTorihikiKubunShitei3_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeShimeShoriJokyoCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引区分の選択状態に応じて、締め処理状況のラジオボタンの状態を変更します
        /// </summary>
        private bool ChangeShimeShoriJokyoCheckBoxEnabled()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.customRadioButtonTorihikiKubunShitei1.Checked)
                {
                    this.SHIME.Enabled = false;

                    this.customRadioButtonShime1.Enabled = false;
                    this.customRadioButtonShime2.Enabled = false;
                    this.customRadioButtonShime3.Enabled = true;

                    this.customRadioButtonShime1.Checked = false;
                    this.customRadioButtonShime2.Checked = false;
                    this.customRadioButtonShime3.Checked = true;
                }
                else if (this.customRadioButtonTorihikiKubunShitei2.Checked)
                {
                    this.SHIME.Enabled = true;

                    this.customRadioButtonShime1.Enabled = true;
                    this.customRadioButtonShime2.Enabled = true;
                    this.customRadioButtonShime3.Enabled = true;

                    this.customRadioButtonShime1.Checked = false;
                    this.customRadioButtonShime2.Checked = false;
                    this.customRadioButtonShime3.Checked = true;
                }
                else if (this.customRadioButtonTorihikiKubunShitei3.Checked)
                {
                    this.SHIME.Enabled = false;

                    this.customRadioButtonShime1.Enabled = false;
                    this.customRadioButtonShime2.Enabled = false;
                    this.customRadioButtonShime3.Enabled = true;

                    this.customRadioButtonShime1.Checked = false;
                    this.customRadioButtonShime2.Checked = false;
                    this.customRadioButtonShime3.Checked = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShimeShoriJokyoCheckBoxEnabled", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end
    }
}
