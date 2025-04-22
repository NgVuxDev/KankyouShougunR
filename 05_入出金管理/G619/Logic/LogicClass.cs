using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace Shougun.Core.ReceiptPayManagement.NyukinNyuryoku3
{
    /// <summary>
    /// G619 入金入力（取引先） ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// 読み込みデータDTO
        /// </summary>
        internal DTOClass dto;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 読み込んだ伝票の入金番号
        /// </summary>
        internal SqlInt64 nyuukinNumber;

        /// <summary>
        /// 読み込んだ伝票のSEQ
        /// </summary>
        internal SqlInt32 seq;

        /// <summary>
        /// 入金額合計
        /// </summary>
        internal decimal nyuukinTotal;

        /// <summary>
        /// 消込額合計
        /// </summary>
        internal decimal keshikomiTotal;

        /// <summary>
        /// 入金―消込差額
        /// </summary>
        internal decimal sagaku;

        /// <summary>
        /// 調整額合計
        /// </summary>
        internal decimal chouseiTotal;

        /// <summary>
        /// 調整額合計
        /// </summary>
        internal bool keshikomiFlg;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// ソート設定情報
        /// </summary>
        private SortSettingInfo sortSettingInfo = null;

        /// <summary>
        /// 銀行Dao
        /// </summary>
        internal IM_BANKDao bankDao;

        /// <summary>
        /// 銀行支店Dao
        /// </summary>
        internal IM_BANK_SHITENDao bankShitenDao;

        /// <summary>
        /// 会社設定Dao
        /// </summary>
        internal IM_CORP_INFODao corpInfoDao;

        /// <summary>
        /// システム設定Dao
        /// </summary>
        internal IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 拠点Dao
        /// </summary>
        internal IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 入出金区分Dao
        /// </summary>
        internal IM_NYUUSHUKKIN_KBNDao nyuushukkinKbnDao;

        /// <summary>
        /// 入金先Dao
        /// </summary>
        internal IM_NYUUKINSAKIDao nyuukinsakiDao;

        /// <summary>
        /// 取引先Dao
        /// </summary>
        internal IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// 取引先請求Dao
        /// </summary>
        internal IM_TORIHIKISAKI_SEIKYUUDao torihikisakiSeikyuuDao;

        /// <summary>
        /// 入金一括Dao
        /// </summary>
        internal IT_NYUUKIN_SUM_ENTRYDao sumEntryDao;

        /// <summary>
        /// 入金一括明細Dao
        /// </summary>
        internal IT_NYUUKIN_SUM_DETAILDao sumDetailDao;

        /// <summary>
        /// 入金Dao
        /// </summary>
        internal IT_NYUUKIN_ENTRYDao entryDao;

        /// <summary>
        /// 入金明細Dao
        /// </summary>
        internal IT_NYUUKIN_DETAILDao detailDao;

        /// <summary>
        /// 入金消込Dao
        /// </summary>
        internal IT_NYUUKIN_KESHIKOMIDao keshikomiDao;

        /// <summary>
        /// 締処理中Dao
        /// </summary>
        internal IT_SHIME_SHORI_CHUUDao shimeiShoriChuuDao;

        /// <summary>
        /// 請求伝票Dao
        /// </summary>
        internal IT_SEIKYUU_DENPYOUDao seikyuuDenpyouDao;

        /// <summary>
        /// 請求明細Dao
        /// </summary>
        internal IT_SEIKYUU_DETAILDao seikyuuDetailDao;

        /// <summary>
        /// 前回値 - 入金区分
        /// </summary>
        private string beforeNyuukinKbn = string.Empty;

        /// <summary>
        /// 入金区分の入力でエラーが発生中かのフラグ
        /// </summary>
        private bool isNyuukinKbnInputError = false;

        #endregion

        #region プロパティ

        /// <summary>
        /// システム設定エンティティを取得・設定します
        /// </summary>
        internal M_SYS_INFO SysInfo { get; private set; }

        /// <summary>
        /// 読み込んだ伝票の入金先CDを取得・設定します
        /// </summary>
        internal string NyuukinsakiCd { get; set; }

        /// <summary>
        /// 追加権限があるかどうかを取得・設定します
        /// </summary>
        internal bool AuthCanAdd { get; private set; }

        /// <summary>
        /// 修正権限があるかどうかを取得・設定します
        /// </summary>
        internal bool AuthCanUpdate { get; private set; }

        /// <summary>
        /// 削除権限があるかどうかを取得・設定します
        /// </summary>
        internal bool AuthCanDelete { get; private set; }

        /// <summary>
        /// 参照権限があるかどうかを取得・設定します
        /// </summary>
        internal bool AuthCanView { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォームインスタンス</param>
        /// <param name="targetForm">メインフォームインスタンス</param>
        public LogicClass(UIHeader headerForm, UIForm targetForm)
        {
            LogUtility.DebugMethodStart(headerForm, targetForm);

            this.headerForm = headerForm;
            this.form = targetForm;

            // 権限をセット
            var formId = FormManager.GetFormID(Assembly.GetExecutingAssembly());
            this.AuthCanAdd = Manager.CheckAuthority(formId, WINDOW_TYPE.NEW_WINDOW_FLAG, false);
            this.AuthCanUpdate = Manager.CheckAuthority(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
            this.AuthCanDelete = Manager.CheckAuthority(formId, WINDOW_TYPE.DELETE_WINDOW_FLAG, false);
            this.AuthCanView = Manager.CheckAuthority(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);

            this.bankDao = DaoInitUtility.GetComponent<IM_BANKDao>();
            this.bankShitenDao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
            this.corpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.nyuushukkinKbnDao = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();
            this.nyuukinsakiDao = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.sumEntryDao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_ENTRYDao>();
            this.sumDetailDao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_DETAILDao>();
            this.entryDao = DaoInitUtility.GetComponent<IT_NYUUKIN_ENTRYDao>();
            this.detailDao = DaoInitUtility.GetComponent<IT_NYUUKIN_DETAILDao>();
            this.keshikomiDao = DaoInitUtility.GetComponent<IT_NYUUKIN_KESHIKOMIDao>();
            this.shimeiShoriChuuDao = DaoInitUtility.GetComponent<IT_SHIME_SHORI_CHUUDao>();
            this.seikyuuDenpyouDao = DaoInitUtility.GetComponent<IT_SEIKYUU_DENPYOUDao>();
            this.seikyuuDetailDao = DaoInitUtility.GetComponent<IT_SEIKYUU_DETAILDao>();

            this.SysInfo = this.sysInfoDao.GetAllData().FirstOrDefault();

            this.sortSettingInfo = SortSettingHelper.LoadSortSettingInfo("UIForm.KESHIKOMI_Ichiran");

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 初期化処理

        /// <summary>
        /// モードに応じて画面を初期化します
        /// </summary>
        /// <param name="isClearDenpyouDate">伝票日付をクリアするかどうかのフラグ</param>
        public bool WindowInit(bool isClearDate)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                this.form.HeaderFormInit();

                this.ClearFormData(isClearDate);

                this.form.TORIHIKISAKI_CD.Text = this.form.MoveDataNyuukinsakiCd;
                var torihikisaki = this.torihikisakiDao.GetDataByCd(this.form.MoveDataNyuukinsakiCd);
                if (null != torihikisaki)
                {
                    this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    var seikyuu = this.torihikisakiSeikyuuDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    if (seikyuu == null || seikyuu.TORIHIKI_KBN_CD.IsNull || seikyuu.TORIHIKI_KBN_CD.Value != 2)
                    {
                        this.form.MoveDataNyuukinsakiCd = null;
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E226");
                        if (!WindowInit(true))
                        {
                            ret = false;
                        }
                        return ret;
                    }

                    this.form.NYUUKINSAKI_CD.Text = seikyuu.NYUUKINSAKI_CD;
                    var nyuukinsaki = this.nyuukinsakiDao.GetDataByCd(seikyuu.NYUUKINSAKI_CD);
                    if (nyuukinsaki != null)
                    {
                        this.form.NYUUKINSAKI_NAME.Text = nyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                    }
                }

                // 一旦無効にし、キャッシャ連携初期値セット
                this.form.CASHER_LINK_KBN.Text = CommonConst.CASHER_LINK_KBN_UNUSED;
                this.form.CASHER_LINK_KBN.Enabled = false;
                this.form.CASHER_LINK_KBN_USE.Enabled = false;
                this.form.CASHER_LINK_KBN_UNUSED.Enabled = false;

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.dto = new DTOClass();
                        this.SetKyoten();
                        this.SetCorpBank();

                        // キャッシャ連携有効化
                        this.form.CASHER_LINK_KBN.Enabled = true;
                        this.form.CASHER_LINK_KBN_USE.Enabled = true;
                        this.form.CASHER_LINK_KBN_UNUSED.Enabled = true;
                        const string CASHER_LINK = "キャッシャ連動";
                        Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile userProfile = Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile.Load();
                        this.form.CASHER_LINK_KBN.Text = this.GetUserProfileValue(userProfile, CASHER_LINK);
                        if (this.form.CASHER_LINK_KBN.Text.Length == 0)
                        {
                            this.form.CASHER_LINK_KBN.Text = CommonConst.CASHER_LINK_KBN_UNUSED;
                        }
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.SetNyuukinData();
                        break;
                }

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.form.NYUUKIN_NUMBER.ReadOnly = false;
                        this.form.NYUUKIN_NUMBER.TabStop = true;
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.form.NYUUKIN_NUMBER.ReadOnly = true;
                        this.form.NYUUKIN_NUMBER.TabStop = false;
                        break;
                }
                //160013 S
                this.ButtonInit();
                this.EventInit();

                #region 伝票請求⇒入金入力(取引先)
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.InitBySeikyuuDto();
                        break;
                }
                #endregion
                //160013 E
                if (!this.SetBankCheck())
                {
                    ret = false;
                    return ret;
                }

                this.ButtonInit();
                this.EventInit();

                var isError = false;

                // 締処理中チェック
                // 修正モードで修正権限あり or 削除モードで削除権限あり 時のみチェック
                if (!isError && ((WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && this.AuthCanUpdate) || (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType && this.AuthCanDelete)))
                {
                    if (this.dto.NyuukinEntry != null)
                    {
                        var shimeShoriChuuList = this.shimeiShoriChuuDao.GetShimeShoriChuuList(this.dto.NyuukinEntry.DENPYOU_DATE.Value, this.dto.NyuukinEntry.TORIHIKISAKI_CD);
                        if (shimeShoriChuuList.Count() > 0)
                        {
                            var messageLogic = new MessageBoxShowLogic();
                            messageLogic.MessageBoxShow("E046", "締処理実行中", "現在締処理実行中の範囲に含まれる為、");

                            this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            this.form.HeaderFormInit();

                            isError = true;
                        }
                    }
                }

                // 月次処理中 or 月次締め済チェック
                // 修正モードで修正権限あり or 削除モードで削除権限あり 時のみチェック
                if (!isError && ((WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && this.AuthCanUpdate) || (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType && this.AuthCanDelete)))
                {
                    GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                    DateTime getsujiShoriCheckDate = DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());

                    // 月次処理中チェック
                    if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string messageArg = string.Empty;
                        // メッセージ生成
                        if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                        {
                            messageArg = "修正";
                        }
                        else if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                        {
                            messageArg = "削除";
                        }
                        msgLogic.MessageBoxShow("E224", messageArg);

                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();
                        isError = true;
                    }
                    // 月次締め済チェック
                    else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string messageArg = string.Empty;
                        // メッセージ生成
                        if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                        {
                            messageArg = "修正";
                        }
                        else if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                        {
                            messageArg = "削除";
                        }
                        msgLogic.MessageBoxShow("E222", messageArg);

                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();
                        isError = true;
                    }
                }

                // 請求・精算締済チェック
                // 修正モードで修正権限あり or 削除モードで削除権限あり 時のみチェック
                if (!isError && ((WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && this.AuthCanUpdate) || (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType && this.AuthCanDelete)))
                {
                    var torihikisakiCdList = new List<string>();
                    var seikyuuDetailList = this.seikyuuDetailDao.GetDataByNyuukinNumber(this.dto.NyuukinEntry.NYUUKIN_NUMBER);
                    if (seikyuuDetailList.Count() > 0)
                    {
                        var seikyuuTorihikisakiList = seikyuuDetailList.Select(s => s.TORIHIKISAKI_CD).Distinct();
                        var seikyuuDetail = seikyuuDetailList.Where(s => s.TORIHIKISAKI_CD == this.form.TORIHIKISAKI_CD.Text.ToString()).FirstOrDefault();
                        if (null != seikyuuDetail)
                        {
                            torihikisakiCdList.Add(seikyuuDetail.TORIHIKISAKI_CD);
                        }

                        // 取引先が1件でも締処理されている場合は、入金伝票を編集できない
                        var messageLogic = new MessageBoxShowLogic();
                        if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType)
                        {
                            messageLogic.MessageBoxShow("I011", "修正");
                        }
                        else
                        {
                            messageLogic.MessageBoxShow("I011", "削除");
                        }

                        // すべて締済の取引先
                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();

                        isError = true;
                    }
                }

                // 精算相殺作成チェック
                // 修正モードで修正権限あり 時のみチェック
                if (!isError && WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && this.AuthCanUpdate)
                {
                    if (this.dto.NyuukinSumEntry.SEISAN_SOUSAI_CREATE_KBN.IsTrue)
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("I015");

                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();

                        isError = true;
                    }
                }

                // エラーがなくても
                // 修正モードで修正権限なし or 削除モードで削除権限なし 時は参照モードにする
                if (!isError && ((WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && !this.AuthCanUpdate) || (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType && !this.AuthCanDelete)))
                {
                    this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    this.form.HeaderFormInit();
                }

                this.ChangeControlState();
                this.ChangeButtonState();

                //switch (this.form.WindowType)
                //{
                //    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                //        this.form.NYUUKIN_NUMBER.Focus();
                //        break;
                //    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                //    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                //    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                //        this.form.DENPYOU_DATE.Focus();
                //        break;
                //}
                this.form.DENPYOU_DATE.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 新規ボタン(F2)イベント
            parentForm.bt_func2.Click -= new EventHandler(this.form.bt_func2_Click);
            parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);

            // 修正ボタン(F3)イベント
            parentForm.bt_func3.Click -= new EventHandler(this.form.bt_func3_Click);
            parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);

            // 消込修正ボタン(F5)イベント
            parentForm.bt_func5.Click -= new EventHandler(this.form.bt_func5_Click);
            parentForm.bt_func5.Click += new EventHandler(this.form.bt_func5_Click);

            // 消込履歴ボタン(F6)イベント
            parentForm.bt_func6.Click -= new EventHandler(this.form.bt_func6_Click);
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            // 入出一覧ボタン(F7)イベント
            parentForm.bt_func7.Click -= new EventHandler(this.form.bt_func7_Click);
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            // 検索ボタン(F8)イベント
            parentForm.bt_func8.Click -= new EventHandler(this.form.bt_func8_Click);
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            // 登録ボタン(F9)イベント
            parentForm.bt_func9.Click -= new EventHandler(this.form.bt_func9_Click);
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            //// 並び替えボタン(F10)イベント
            //parentForm.bt_func10.Click -= new EventHandler(this.form.bt_func10_Click);
            //parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            // 閉じるボタン(F12)イベント
            parentForm.bt_func12.Click -= new EventHandler(this.form.bt_func12_Click);
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // 一括消込ボタンイベント
            parentForm.bt_process1.Click -= new EventHandler(this.form.bt_process1_Click);
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            // 手数料消込ボタンイベント
            parentForm.bt_process2.Click -= new EventHandler(this.form.bt_process2_Click);
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

            // 行削除ボタンイベント
            parentForm.bt_process3.Click -= new EventHandler(this.form.bt_process3_Click);
            parentForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);

            // 行挿入ボタンイベント
            parentForm.bt_process4.Click -= new EventHandler(this.form.bt_process4_Click);
            parentForm.bt_process4.Click += new EventHandler(this.form.bt_process4_Click);

            // 前ボタンイベント
            this.form.PREV_BUTTON.Click -= new EventHandler(this.form.PrevButton_Click);
            this.form.PREV_BUTTON.Click += new EventHandler(this.form.PrevButton_Click);

            // 次ボタンイベント
            this.form.NEXT_BUTTON.Click -= new EventHandler(this.form.NextButton_Click);
            this.form.NEXT_BUTTON.Click += new EventHandler(this.form.NextButton_Click);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ユーザ設定から拠点を画面に設定します
        /// </summary>
        internal void SetKyoten()
        {
            LogUtility.DebugMethodStart();

            var fileAccess = new XMLAccessor();
            var config = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

            var kyotenCd = config.ItemSetVal1;

            if (!string.IsNullOrEmpty(kyotenCd))
            {
                this.headerForm.txtKyotenCd.Text = config.ItemSetVal1.PadLeft(2, '0');
            }

            this.headerForm.txtKyotenName.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.headerForm.txtKyotenCd.Text))
            {
                var kyoten = this.kyotenDao.GetDataByCd(this.headerForm.txtKyotenCd.Text);
                if (null != kyoten)
                {
                    this.headerForm.txtKyotenName.Text = kyoten.KYOTEN_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 自社情報に設定されている銀行情報を画面に設定します
        /// </summary>
        internal void SetCorpBank()
        {
            LogUtility.DebugMethodStart();

            // ユーザー毎の前回値を設定
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SET_BANK_CD))
            {
                // 銀行
                var bank = new M_BANK();
                bank.BANK_CD = Properties.Settings.Default.SET_BANK_CD;

                var banks = this.bankDao.GetAllValidData(bank);
                if (banks != null && banks.Count() > 0)
                {
                    this.form.BANK_CD.Text = banks[0].BANK_CD;
                    this.form.BANK_NAME_RYAKU.Text = banks[0].BANK_NAME_RYAKU;

                    // 銀行支店
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.SET_BANK_SHITEN_CD))
                    {
                        var bankShiten = new M_BANK_SHITEN();
                        bankShiten.BANK_CD = Properties.Settings.Default.SET_BANK_CD;
                        bankShiten.BANK_SHITEN_CD = Properties.Settings.Default.SET_BANK_SHITEN_CD;
                        bankShiten.KOUZA_NO = Properties.Settings.Default.SET_KOUZA_NO;

                        var bankShitens = this.bankShitenDao.GetAllValidData(bankShiten);
                        if (bankShitens != null && bankShitens.Count() > 0)
                        {
                            this.form.BANK_SHITEN_CD.Text = bankShitens[0].BANK_SHITEN_CD;
                            this.form.BANK_SHIETN_NAME_RYAKU.Text = bankShitens[0].BANK_SHIETN_NAME_RYAKU;
                            this.form.KOUZA_SHURUI.Text = bankShitens[0].KOUZA_SHURUI;
                            this.form.KOUZA_NO.Text = bankShitens[0].KOUZA_NO;
                            this.form.KOUZA_NAME.Text = bankShitens[0].KOUZA_NAME;
                        }
                        else
                        {
                            this.form.BANK_SHITEN_CD.Text = string.Empty;
                            this.form.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;
                            this.form.KOUZA_SHURUI.Text = string.Empty;
                            this.form.KOUZA_NO.Text = string.Empty;
                            this.form.KOUZA_NAME.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    this.form.BANK_CD.Text = string.Empty;
                    this.form.BANK_NAME_RYAKU.Text = string.Empty;
                    this.form.BANK_SHITEN_CD.Text = string.Empty;
                    this.form.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;
                    this.form.KOUZA_SHURUI.Text = string.Empty;
                    this.form.KOUZA_NO.Text = string.Empty;
                    this.form.KOUZA_NAME.Text = string.Empty;
                }
            }

            M_CORP_INFO corpInfo = this.corpInfoDao.GetAllDataMinCols().FirstOrDefault();
            if (null != corpInfo
                && (string.IsNullOrEmpty(Properties.Settings.Default.SET_BANK_CD)))
            {
                this.form.BANK_CD.Text = corpInfo.BANK_CD;
                this.form.BANK_NAME_RYAKU.Text = string.Empty;
                this.form.BANK_SHITEN_CD.Text = corpInfo.BANK_SHITEN_CD;
                this.form.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;

                if (!string.IsNullOrEmpty(corpInfo.BANK_CD))
                {
                    var bank = this.bankDao.GetDataByCd(corpInfo.BANK_CD);
                    if (null != bank)
                    {
                        this.form.BANK_NAME_RYAKU.Text = bank.BANK_NAME_RYAKU;
                    }
                }
                if (!string.IsNullOrEmpty(corpInfo.BANK_CD) && !string.IsNullOrEmpty(corpInfo.BANK_SHITEN_CD) && !string.IsNullOrEmpty(corpInfo.KOUZA_NO))
                {
                    var bankShiten = new M_BANK_SHITEN();
                    bankShiten.BANK_CD = corpInfo.BANK_CD;
                    bankShiten.BANK_SHITEN_CD = corpInfo.BANK_SHITEN_CD;
                    bankShiten.KOUZA_NO = corpInfo.KOUZA_NO;
                    bankShiten = this.bankShitenDao.GetDataByCd(bankShiten);

                    if (null != bankShiten)
                    {
                        this.form.BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                        this.form.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                        this.form.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                        this.form.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンコントロールを初期化します
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(this.CreateButtonInfo(), parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定情報を作成します
        /// </summary>
        /// <returns>ボタン設定情報</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            string ButtonInfoXmlPath = this.GetType().Namespace;
            ButtonInfoXmlPath = ButtonInfoXmlPath + ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        #endregion

        #region 共通メソッド

        /// <summary>
        /// 読み込んだ伝票の入金番号を設定します
        /// </summary>
        /// <param name="nyuukinNumber">入金番号</param>
        internal void SetNyuukinNumber(SqlInt64 nyuukinNumber)
        {
            LogUtility.DebugMethodStart(nyuukinNumber);

            this.SetNyuukinNumberAndSeq(nyuukinNumber, SqlInt32.Null);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 読み込んだ伝票の入金番号とSEQを設定します
        /// </summary>
        /// <param name="nyuukinNumber">入金番号</param>
        /// <param name="seq">SEQ</param>
        internal void SetNyuukinNumberAndSeq(SqlInt64 nyuukinNumber, SqlInt32 seq)
        {
            LogUtility.DebugMethodStart(nyuukinNumber, seq);

            this.nyuukinNumber = nyuukinNumber;
            this.seq = seq;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 指定されたオブジェクトが null または Empty文字列であるかどうかを示します
        /// </summary>
        /// <param name="value">テストする文字列</param>
        /// <returns>null または Empty文字列の場合は true それ以外の場合は false</returns>
        internal bool IsNullOrEmpty(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = false;
            if (null == value)
            {
                ret = true;
            }
            else if (string.IsNullOrEmpty(value.ToString()))
            {
                ret = true;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region 型変換メソッド

        /// <summary>
        /// オブジェクトを Decimal に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal Decimal ConvertToDecimal(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = 0m;
            if (!this.IsNullOrEmpty(value))
            {
                if (value is TextBox)
                {
                    ret = Decimal.Parse(((TextBox)value).Text);
                }
                else
                {
                    ret = Decimal.Parse(value.ToString());
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlDecimal に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlDecimal ConvertToSqlDecimal(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = SqlDecimal.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlDecimal.Parse(value.ToString().Replace(",", ""));
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlInt64 に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlInt64 ConvertToSqlInt64(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = SqlInt64.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlInt64.Parse(value.ToString());
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlInt32 に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlInt32 ConvertToSqlInt32(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = SqlInt32.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlInt32.Parse(value.ToString());
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlInt16 に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlInt16 ConvertToSqlInt16(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = SqlInt16.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlInt16.Parse(value.ToString());
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region データ取得メソッド

        /// <summary>
        /// 基準の入金番号より前で最大の入金番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の入金番号</param>
        /// <returns>入金番号</returns>
        internal SqlInt64 GetPrevNyuukinNumber(SqlInt64 nyuukinNumber)
        {
            LogUtility.DebugMethodStart(nyuukinNumber);

            var ret = SqlInt64.Null;

            var number = string.Empty;
            if (!nyuukinNumber.IsNull)
            {
                number = nyuukinNumber.Value.ToString();
            }
            string kyotenCd = this.headerForm.txtKyotenCd.Text;
            var nyuukinEntry = this.entryDao.GetPrevNyuukinNumber(number, kyotenCd);
            if (null != nyuukinEntry)
            {
                ret = nyuukinEntry.NYUUKIN_NUMBER;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 基準の入金番号より後で最小の入金番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の入金番号</param>
        /// <returns>入金番号</returns>
        internal SqlInt64 GetNextNyuukinNumber(SqlInt64 nyuukinNumber)
        {
            LogUtility.DebugMethodStart(nyuukinNumber);

            var ret = SqlInt64.Null;

            var number = string.Empty;
            if (!nyuukinNumber.IsNull)
            {
                number = nyuukinNumber.Value.ToString();
            }
            string kyotenCd = this.headerForm.txtKyotenCd.Text;
            var nyuukinEntry = this.entryDao.GetNextNyuukinNumber(number, kyotenCd);
            if (null != nyuukinEntry)
            {
                ret = nyuukinEntry.NYUUKIN_NUMBER;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金伝票を取得します
        /// </summary>
        /// <param name="nyuukinNumber">入金番号</param>
        /// <param name="seq">SEQ</param>
        internal void GetNyuukinData(SqlInt64 nyuukinNumber, SqlInt32 seq)
        {
            LogUtility.DebugMethodStart(nyuukinNumber, seq);

            this.dto = new DTOClass();

            // 入金
            var entry = new T_NYUUKIN_ENTRY();
            entry.NYUUKIN_NUMBER = nyuukinNumber;
            if (!seq.IsNull)
            {
                entry.SEQ = seq;
            }
            else
            {
                entry.DELETE_FLG = false;
            }
            this.dto.NyuukinEntry = this.entryDao.GetNyuukinEntry(entry);
            if (this.dto.NyuukinEntry == null)
            {
                return;
            }

            // 入金一括
            var sumEntry = new T_NYUUKIN_SUM_ENTRY();
            sumEntry.SYSTEM_ID = this.dto.NyuukinEntry.NYUUKIN_SUM_SYSTEM_ID;
            if (!seq.IsNull)
            {
                sumEntry.SEQ = this.dto.NyuukinEntry.SEQ;
            }
            else
            {
                sumEntry.DELETE_FLG = false;
            }
            this.dto.NyuukinSumEntry = this.sumEntryDao.GetNyuukinSumEntry(sumEntry);

            // 入金明細
            var detail = new T_NYUUKIN_DETAIL();
            detail.SYSTEM_ID = this.dto.NyuukinEntry.SYSTEM_ID;
            detail.SEQ = this.dto.NyuukinEntry.SEQ;
            this.dto.NyuukinDetailList = this.detailDao.GetNyuukinDetailList(detail);

            // 入金一括明細
            var sumDetail = new T_NYUUKIN_SUM_DETAIL();
            sumDetail.SYSTEM_ID = this.dto.NyuukinEntry.NYUUKIN_SUM_SYSTEM_ID;
            sumDetail.SEQ = this.dto.NyuukinEntry.SEQ;
            this.dto.NyuukinSumDetailList = this.sumDetailDao.GetNyuukinSumDetailList(sumDetail);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタンイベント

        #region 前ボタン処理

        /// <summary>
        /// 前ボタン処理
        /// </summary>
        public bool Prev()
        {
            bool ret = true;
            try
            {
                var nyuukinNumber = SqlInt64.Null;
                if (!string.IsNullOrEmpty(this.form.NYUUKIN_NUMBER.Text))
                {
                    nyuukinNumber = this.ConvertToSqlInt64(this.form.NYUUKIN_NUMBER.Text);
                }

                // 読み込む対象の入金番号を取得
                nyuukinNumber = this.GetPrevNyuukinNumber(nyuukinNumber);

                if (nyuukinNumber.IsNull)
                {
                    //try to get maximun nyuukin number
                    nyuukinNumber = this.GetPrevNyuukinNumber(nyuukinNumber);
                    //after try to get maximun nyuukin number, if nyuukin number is null then show message
                    if (nyuukinNumber.IsNull)
                    {
                        // 読み込む対象の入金番号を取得
                        //ThangNguyen [Update] 20150814 #11409 Start
                        //nyuukinNumber = this.GetPrevNyuukinNumber(nyuukinNumber);
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E045");
                        return ret;
                        //ThangNguyen [Update] 20150814 #11409 End
                    }
                }

                var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());

                this.SetNyuukinNumber(nyuukinNumber);

                if (nyuukinNumber.IsNull)
                {
                    if (!Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
                    {
                        return ret;
                    }

                    // 入金データがない
                    this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                }
                else
                {
                    if (!Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                        && !Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.UPDATE_WINDOW_FLAG));
                        return ret;
                    }

                    this.form.NYUUKIN_NUMBER.Text = nyuukinNumber.Value.ToString();
                    this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }

                if (!this.WindowInit(true))
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Prev", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Prev", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 次ボタン処理

        /// <summary>
        /// 次ボタン処理
        /// </summary>
        public bool Next()
        {
            bool ret = true;
            try
            {
                var nyuukinNumber = SqlInt64.Null;
                if (!string.IsNullOrEmpty(this.form.NYUUKIN_NUMBER.Text))
                {
                    nyuukinNumber = this.ConvertToSqlInt64(this.form.NYUUKIN_NUMBER.Text);
                }

                // 読み込む対象の入金番号を取得
                nyuukinNumber = this.GetNextNyuukinNumber(nyuukinNumber);

                if (nyuukinNumber.IsNull)
                {
                    //try to get minimum number if nyuukin number is maximum
                    nyuukinNumber = this.GetNextNyuukinNumber(0);
                    //after try to get minimum number, if nyuukin number is null then show message
                    if (nyuukinNumber.IsNull)
                    {
                        // 読み込む対象の入金番号を取得
                        //ThangNguyen [Update] 20150814 #11409 Start
                        //nyuukinNumber = this.GetNextNyuukinNumber(nyuukinNumber);
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E045");
                        return ret;
                        //ThangNguyen [Update] 20150814 #11409 End
                    }
                }

                var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());

                this.SetNyuukinNumber(nyuukinNumber);

                if (nyuukinNumber.IsNull)
                {
                    if (!Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
                    {
                        return ret;
                    }

                    // 入金データがない
                    this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                }
                else
                {
                    if (!Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                        && !Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.UPDATE_WINDOW_FLAG));
                        return ret;
                    }

                    this.form.NYUUKIN_NUMBER.Text = nyuukinNumber.Value.ToString();
                    this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }

                if (!this.WindowInit(true))
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Next", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Next", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            int count = 0;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                this.form.searchDate = "";
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    msgLogic.MessageBoxShow("E012", "取引先");
                    this.form.TORIHIKISAKI_CD.Focus();
                    return 0;
                }
                if (string.IsNullOrEmpty(this.form.DENPYOU_DATE.Text))
                {
                    msgLogic.MessageBoxShow("E012", "伝票日付");
                    this.form.DENPYOU_DATE.Focus();
                    return 0;
                }

                this.form.KESHIKOMI_Ichiran.Columns["GYOUSHA_CD"].Visible = true;
                this.form.KESHIKOMI_Ichiran.Columns["GYOUSHA_NAME"].Visible = true;
                this.form.KESHIKOMI_Ichiran.Columns["GENBA_CD"].Visible = true;
                this.form.KESHIKOMI_Ichiran.Columns["GENBA_NAME"].Visible = true;

                T_NYUUKIN_ENTRY entry = new T_NYUUKIN_ENTRY();
                entry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                DateTime time;
                if (DateTime.TryParse(this.form.DENPYOU_DATE.Text, out time))
                {
                    entry.DENPYOU_DATE = time;
                }
                DataTable dt = this.entryDao.GetKeshikomiData(entry);
                if (dt == null || dt.Rows.Count == 0)
                {
                    this.form.KESHIKOMI_Ichiran.Rows.Clear();
                    msgLogic.MessageBoxShow("C001");
                    return 0;
                }

                count = dt.Rows.Count;

                DataTable table = dt.Clone();
                table.Columns.Add("KESHIKOMI_KINGAKU_ZEN");
                table.Columns.Add("KESHIKOMI_KINGAKU_TOTAL");

                string select = "1=1";
                bool matchNyuukinNumFlg = false;
                if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    select += " AND NYUUKIN_NUM = " + this.form.NYUUKIN_NUMBER.Text;
                    // 新規モード以外で、該当の入金番号に合致する入金消込が無い場合は、
                    // 入力された取引先が等しい入金消込を再表示
                    if (dt.Select(select).Length <= 0)
                    {
                        select = "1=1";
                        matchNyuukinNumFlg = false;
                    }
                    else
                    {
                        matchNyuukinNumFlg = true;
                    }
                }

                table = DispKeshikomiIchiran(dt, table, matchNyuukinNumFlg, select);
                if (matchNyuukinNumFlg)
                {
                    // 該当の入金番号以外の明細を追加
                    matchNyuukinNumFlg = false;
                    select = "NYUUKIN_NUM <> " + this.form.NYUUKIN_NUMBER.Text + " OR NYUUKIN_NUM IS NULL";
                    table = DispKeshikomiIchiran(dt, table, matchNyuukinNumFlg, select);
                }

                DataRow dr;
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    dr = table.Rows[i];
                    if (this.ConvertToDecimal(dr["SEIKYUU_KINGAKU"]).CompareTo(0) == 0)
                    {
                        table.Rows.Remove(dr);
                    }
                }
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    dr = table.Rows[i];
                    if (this.ConvertToDecimal(dr["MIKESHIKOMI_KINGAKU"]).CompareTo(0) == 0 && dr["NYUUKIN_NUM"].ToString() != this.form.NYUUKIN_NUMBER.Text)
                    {
                        table.Rows.Remove(dr);
                    }
                }

                string gyoushaCd = "";
                string genba = "";
                bool gyoushaFlg = true;
                bool genbaFlg = true;
                foreach (DataRow row in table.Rows)
                {
                    gyoushaCd = Convert.ToString(row["GYOUSHA_CD"]);
                    genba = Convert.ToString(row["GENBA_CD"]);
                    if (!string.IsNullOrEmpty(gyoushaCd))
                    {
                        gyoushaFlg = false;
                    }
                    if (!string.IsNullOrEmpty(genba))
                    {
                        genbaFlg = false;
                    }
                }

                if (gyoushaFlg)
                {
                    this.form.KESHIKOMI_Ichiran.Columns["GYOUSHA_CD"].Visible = false;
                    this.form.KESHIKOMI_Ichiran.Columns["GYOUSHA_NAME"].Visible = false;
                }
                if (genbaFlg)
                {
                    this.form.KESHIKOMI_Ichiran.Columns["GENBA_CD"].Visible = false;
                    this.form.KESHIKOMI_Ichiran.Columns["GENBA_NAME"].Visible = false;
                }

                // 請求日付、請求番号、鑑番号で並べ替え
                DataRow[] DataRowsSort = table.Select("1=1", "SORT_SEIKYUU_DATE, SEIKYUU_NUMBER, KAGAMI_NUMBER, SEIKYUU_KINGAKU DESC");
                DataTable tableSort = table.Clone();

                string seikyuu_numOld = string.Empty;
                string kagami_numOld = string.Empty;
                string seikyuu_num = string.Empty;
                string kagami_num = string.Empty;
                foreach (DataRow row in DataRowsSort)
                {
                    DataRow newRow = tableSort.NewRow();
                    newRow["SYSTEM_ID"] = row["SYSTEM_ID"];
                    newRow["KESHIKOMI_SEQ"] = row["KESHIKOMI_SEQ"];
                    newRow["SORT_SEIKYUU_DATE"] = row["SORT_SEIKYUU_DATE"];
                    newRow["NYUUKIN_NUM"] = row["NYUUKIN_NUM"];
                    newRow["SEIKYUU_NUMBER"] = row["SEIKYUU_NUMBER"];
                    newRow["KAGAMI_NUMBER"] = row["KAGAMI_NUMBER"];
                    newRow["GYOUSHA_CD"] = row["GYOUSHA_CD"];
                    newRow["GYOUSHA_NAME"] = row["GYOUSHA_NAME"];
                    newRow["GENBA_CD"] = row["GENBA_CD"];
                    newRow["GENBA_NAME"] = row["GENBA_NAME"];
                    newRow["SEIKYUU_DATE"] = row["SEIKYUU_DATE"];
                    newRow["SEIKYUU_KINGAKU"] = row["SEIKYUU_KINGAKU"];
                    newRow["KESHIKOMI_KINGAKU"] = row["KESHIKOMI_KINGAKU"];
                    newRow["KESHIKOMI_BIKOU"] = row["KESHIKOMI_BIKOU"];
                    newRow["MIKESHIKOMI_KINGAKU"] = row["MIKESHIKOMI_KINGAKU"];
                    newRow["KESHIKOMI_KINGAKU_ZEN"] = row["KESHIKOMI_KINGAKU_ZEN"];
                    newRow["KESHIKOMI_KINGAKU_TOTAL"] = row["KESHIKOMI_KINGAKU_TOTAL"];

                    seikyuu_num = Convert.ToString(row["SEIKYUU_NUMBER"]);
                    kagami_num = Convert.ToString(row["KAGAMI_NUMBER"]);
                    if (seikyuu_numOld != seikyuu_num || kagami_numOld != kagami_num)
                    {
                        tableSort.Rows.Add(newRow);
                        seikyuu_numOld = seikyuu_num;
                        kagami_numOld = kagami_num;
                    }
                }
                table = tableSort;

                this.form.KESHIKOMI_Ichiran.Rows.Clear();
                if (table.Rows.Count > 0)
                {
                    this.form.KESHIKOMI_Ichiran.Rows.Add(table.Rows.Count);
                    DateTime date = this.parentForm.sysDate;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataGridViewRow row = this.form.KESHIKOMI_Ichiran.Rows[i];
                        dr = table.Rows[i];
                        row.Cells["DELETE_FLG"].Value = false;
                        row.Cells["SYSTEM_ID"].Value = dr["SYSTEM_ID"];
                        row.Cells["KESHIKOMI_SEQ"].Value = dr["KESHIKOMI_SEQ"];
                        row.Cells["SORT_SEIKYUU_DATE"].Value = dr["SORT_SEIKYUU_DATE"];
                        row.Cells["NYUUKIN_NUM"].Value = dr["NYUUKIN_NUM"];
                        row.Cells["SEIKYUU_NUMBER"].Value = dr["SEIKYUU_NUMBER"];
                        row.Cells["KAGAMI_NUMBER"].Value = dr["KAGAMI_NUMBER"];
                        row.Cells["GYOUSHA_CD"].Value = dr["GYOUSHA_CD"];
                        row.Cells["GYOUSHA_NAME"].Value = dr["GYOUSHA_NAME"];
                        row.Cells["GENBA_CD"].Value = dr["GENBA_CD"];
                        row.Cells["GENBA_NAME"].Value = dr["GENBA_NAME"];
                        row.Cells["SEIKYUU_DATE"].Value = dr["SEIKYUU_DATE"];
                        if (DateTime.TryParse(Convert.ToString(row.Cells["SEIKYUU_DATE"].Value), out date))
                        {
                            switch (date.DayOfWeek)
                            {
                                case DayOfWeek.Sunday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(日)";
                                    break;

                                case DayOfWeek.Monday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(月)";
                                    break;

                                case DayOfWeek.Tuesday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(火)";
                                    break;

                                case DayOfWeek.Wednesday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(水)";
                                    break;

                                case DayOfWeek.Thursday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(木)";
                                    break;

                                case DayOfWeek.Friday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(金)";
                                    break;

                                case DayOfWeek.Saturday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(土)";
                                    break;
                            }
                        }
                        row.Cells["SEIKYUU_KINGAKU"].Value = dr["SEIKYUU_KINGAKU"];
                        row.Cells["KESHIKOMI_KINGAKU"].Value = dr["KESHIKOMI_KINGAKU"];
                        row.Cells["MIKESHIKOMI_KINGAKU"].Value = dr["MIKESHIKOMI_KINGAKU"];
                        row.Cells["KESHIKOMI_BIKOU"].Value = dr["KESHIKOMI_BIKOU"];
                        row.Cells["KESHIKOMI_KINGAKU_ZEN"].Value = dr["KESHIKOMI_KINGAKU_ZEN"];
                        row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = dr["KESHIKOMI_KINGAKU_TOTAL"];
                    }
                    this.form.searchDate = this.form.DENPYOU_DATE.Text;
                }
                if (this.form.DETAIL_Ichiran.Rows.Count >= this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value
                    && !this.form.DETAIL_Ichiran.Rows[this.form.DETAIL_Ichiran.Rows.Count - 1].IsNewRow)
                {
                    this.form.DETAIL_Ichiran.AllowUserToAddRows = false;
                }
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    this.form.KESHIKOMI_Ichiran.ReadOnly = true;
                }
                else
                {
                    this.form.KESHIKOMI_Ichiran.ReadOnly = false;
                    foreach (DataGridViewColumn col in this.form.KESHIKOMI_Ichiran.Columns)
                    {
                        if (!col.Visible)
                        {
                            continue;
                        }

                        if (col.Name != "DELETE_FLG" && col.Name != "KESHIKOMI_KINGAKU" && col.Name != "KESHIKOMI_BIKOU")
                        {
                            col.ReadOnly = true;
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }
            return count;
        }

        /// <summary>
        /// 入金消込明細元データ編集
        /// </summary>
        /// <param name="dt">元データ</param>
        /// <param name="table">出力データ</param>
        /// <param name="matchNyuukinNumFlg">入金番号が一致するものがあるか</param>
        /// <param name="select">検索条件</param>
        /// <returns>入金消込明細元データ</returns>
        private DataTable DispKeshikomiIchiran(DataTable dt, DataTable table, bool matchNyuukinNumFlg, string select)
        {
            string seikyuu_num = "";
            string kagami_num = "";
            string format = "SEIKYUU_NUMBER = {0} AND KAGAMI_NUMBER = '{1}'";
            SqlInt64 nyuukinNum;
            decimal keshikomiGaku = 0;
            decimal keshikomiZen = 0;
            List<string> list = new List<string>();
            string key = string.Empty;

            foreach (DataRow row in dt.Select(select))
            {
                seikyuu_num = Convert.ToString(row["SEIKYUU_NUMBER"]);
                kagami_num = Convert.ToString(row["KAGAMI_NUMBER"]);

                key = seikyuu_num + "-" + kagami_num;

                if (list.Contains(key))
                {
                    continue;
                }

                list.Add(key);
                DataRow[] rows = dt.Select(string.Format(format, seikyuu_num, kagami_num));

                DataRow newRow = table.NewRow();
                keshikomiGaku = 0;
                keshikomiZen = 0;
                foreach (DataRow tmpRow in rows)
                {
                    nyuukinNum = this.ConvertToSqlInt64(tmpRow["NYUUKIN_NUM"]);
                    if (!nyuukinNum.IsNull && Convert.ToString(nyuukinNum.Value) != this.form.NYUUKIN_NUMBER.Text)
                    {
                        keshikomiZen += this.ConvertToDecimal(tmpRow["KESHIKOMI_KINGAKU"]);
                    }
                    else if (!nyuukinNum.IsNull && Convert.ToString(nyuukinNum.Value) == this.form.NYUUKIN_NUMBER.Text)
                    {
                        keshikomiGaku = this.ConvertToDecimal(tmpRow["KESHIKOMI_KINGAKU"]);
                    }
                }
                newRow["SYSTEM_ID"] = row["SYSTEM_ID"];
                newRow["KESHIKOMI_SEQ"] = row["KESHIKOMI_SEQ"];
                newRow["SORT_SEIKYUU_DATE"] = row["SORT_SEIKYUU_DATE"];
                newRow["NYUUKIN_NUM"] = row["NYUUKIN_NUM"];
                newRow["SEIKYUU_NUMBER"] = row["SEIKYUU_NUMBER"];
                newRow["KAGAMI_NUMBER"] = row["KAGAMI_NUMBER"];
                newRow["GYOUSHA_CD"] = row["GYOUSHA_CD"];
                newRow["GYOUSHA_NAME"] = row["GYOUSHA_NAME"];
                newRow["GENBA_CD"] = row["GENBA_CD"];
                newRow["GENBA_NAME"] = row["GENBA_NAME"];
                newRow["SEIKYUU_DATE"] = row["SEIKYUU_DATE"];
                newRow["SEIKYUU_KINGAKU"] = row["SEIKYUU_KINGAKU"];
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    keshikomiZen += keshikomiGaku;
                    keshikomiGaku = 0;
                    newRow["KESHIKOMI_KINGAKU"] = DBNull.Value;
                    newRow["KESHIKOMI_BIKOU"] = string.Empty;
                }
                else
                {
                    if (matchNyuukinNumFlg)
                    {
                        newRow["KESHIKOMI_KINGAKU"] = row["KESHIKOMI_KINGAKU"];
                        newRow["KESHIKOMI_BIKOU"] = row["KESHIKOMI_BIKOU"];
                    }
                }
                newRow["MIKESHIKOMI_KINGAKU"] = this.ConvertToDecimal(row["SEIKYUU_KINGAKU"]) - keshikomiZen - keshikomiGaku;
                newRow["KESHIKOMI_KINGAKU_ZEN"] = keshikomiZen;
                newRow["KESHIKOMI_KINGAKU_TOTAL"] = keshikomiGaku + keshikomiZen;
                table.Rows.Add(newRow);
            }

            return table;
        }

        #endregion

        #region 登録データ作成処理

        /// <summary>
        /// 登録データ作成処理
        /// </summary>
        /// <returns></returns>
        internal DTOClass CreateEntity()
        {
            LogUtility.DebugMethodStart();

            var ret = this.dto.CloneDto(this.dto);
            ret.NyuukinSumEntry = new T_NYUUKIN_SUM_ENTRY();
            ret.NyuukinSumDetailList.Clear();
            ret.NyuukinEntry = new T_NYUUKIN_ENTRY();
            ret.NyuukinDetailList.Clear();
            ret.NyuukinKeshikomiList.Clear();

            // 入金入力エンティティ作成
            var newNyuukinEntryBinderLogic = new DataBinderLogic<T_NYUUKIN_ENTRY>(ret.NyuukinEntry);
            newNyuukinEntryBinderLogic.SetSystemProperty(ret.NyuukinEntry, false);

            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                ret.NyuukinEntry.SYSTEM_ID = this.CreateSystemId();
                ret.NyuukinEntry.SEQ = 1;
                ret.NyuukinEntry.NYUUKIN_SUM_SYSTEM_ID = this.CreateSystemId();
                ret.NyuukinEntry.NYUUKIN_NUMBER = this.CreateNyuukinNumber();
            }
            else
            {
                ret.NyuukinEntry.SYSTEM_ID = this.dto.NyuukinEntry.SYSTEM_ID;
                ret.NyuukinEntry.SEQ = this.dto.NyuukinEntry.SEQ + 1;
                ret.NyuukinEntry.NYUUKIN_SUM_SYSTEM_ID = this.dto.NyuukinEntry.NYUUKIN_SUM_SYSTEM_ID;
                ret.NyuukinEntry.NYUUKIN_NUMBER = this.dto.NyuukinEntry.NYUUKIN_NUMBER;
                ret.NyuukinEntry.CREATE_DATE = this.dto.NyuukinEntry.CREATE_DATE;
                ret.NyuukinEntry.CREATE_PC = this.dto.NyuukinEntry.CREATE_PC;
                ret.NyuukinEntry.CREATE_USER = this.dto.NyuukinEntry.CREATE_USER;
            }
            ret.NyuukinEntry.KYOTEN_CD = this.ConvertToSqlInt16(this.headerForm.txtKyotenCd.Text);
            ret.NyuukinEntry.DENPYOU_DATE = (DateTime)this.form.DENPYOU_DATE.Value;
            ret.NyuukinEntry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            ret.NyuukinEntry.NYUUKINSAKI_CD = this.form.NYUUKINSAKI_CD.Text;
            if (!string.IsNullOrEmpty(this.form.BANK_CD.Text))
            {
                ret.NyuukinEntry.BANK_CD = this.form.BANK_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.BANK_SHITEN_CD.Text))
            {
                ret.NyuukinEntry.BANK_SHITEN_CD = this.form.BANK_SHITEN_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.KOUZA_SHURUI.Text))
            {
                ret.NyuukinEntry.KOUZA_SHURUI = this.form.KOUZA_SHURUI.Text;
            }
            if (!string.IsNullOrEmpty(this.form.KOUZA_NO.Text))
            {
                ret.NyuukinEntry.KOUZA_NO = this.form.KOUZA_NO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.KOUZA_NAME.Text))
            {
                ret.NyuukinEntry.KOUZA_NAME = this.form.KOUZA_NAME.Text;
            }
            ret.NyuukinEntry.NYUUKIN_AMOUNT_TOTAL = this.nyuukinTotal;
            ret.NyuukinEntry.CHOUSEI_AMOUNT_TOTAL = this.chouseiTotal;
            ret.NyuukinEntry.TOK_INPUT_KBN = false;
            ret.NyuukinEntry.DELETE_FLG = false;

            // 入金一括エンティティ作成
            var newNyuukinSumEntryBinderLogic = new DataBinderLogic<T_NYUUKIN_SUM_ENTRY>(ret.NyuukinSumEntry);
            newNyuukinSumEntryBinderLogic.SetSystemProperty(ret.NyuukinSumEntry, false);

            ret.NyuukinSumEntry.SYSTEM_ID = ret.NyuukinEntry.NYUUKIN_SUM_SYSTEM_ID;
            ret.NyuukinSumEntry.SEQ = ret.NyuukinEntry.SEQ;
            ret.NyuukinSumEntry.KYOTEN_CD = ret.NyuukinEntry.KYOTEN_CD;
            ret.NyuukinSumEntry.NYUUKIN_NUMBER = ret.NyuukinEntry.NYUUKIN_NUMBER;
            ret.NyuukinSumEntry.DENPYOU_DATE = ret.NyuukinEntry.DENPYOU_DATE;
            ret.NyuukinSumEntry.NYUUKINSAKI_CD = ret.NyuukinEntry.NYUUKINSAKI_CD;
            ret.NyuukinSumEntry.BANK_CD = ret.NyuukinEntry.BANK_CD;
            ret.NyuukinSumEntry.BANK_SHITEN_CD = ret.NyuukinEntry.BANK_SHITEN_CD;
            ret.NyuukinSumEntry.KOUZA_SHURUI = ret.NyuukinEntry.KOUZA_SHURUI;
            ret.NyuukinSumEntry.KOUZA_NO = ret.NyuukinEntry.KOUZA_NO;
            ret.NyuukinSumEntry.KOUZA_NAME = ret.NyuukinEntry.KOUZA_NAME;
            ret.NyuukinSumEntry.NYUUKIN_AMOUNT_TOTAL = ret.NyuukinEntry.NYUUKIN_AMOUNT_TOTAL;
            ret.NyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL = ret.NyuukinEntry.CHOUSEI_AMOUNT_TOTAL;
            ret.NyuukinSumEntry.TORI_KOMI_KBN = false;
            ret.NyuukinSumEntry.JIDO_KESHIKOMI_KBN = false;
            ret.NyuukinSumEntry.DELETE_FLG = false;
            
            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                ret.NyuukinSumEntry.CREATE_DATE = this.dto.NyuukinEntry.CREATE_DATE;
                ret.NyuukinSumEntry.CREATE_PC = this.dto.NyuukinEntry.CREATE_PC;
                ret.NyuukinSumEntry.CREATE_USER = this.dto.NyuukinEntry.CREATE_USER;
                ret.NyuukinSumEntry.JIDO_KESHIKOMI_KBN = this.dto.NyuukinSumEntry.JIDO_KESHIKOMI_KBN;
                ret.NyuukinSumEntry.TORI_KOMI_KBN = this.dto.NyuukinSumEntry.TORI_KOMI_KBN;
            }

            Int16 rowNum = 1;
            foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                // 入金明細エンティティ作成
                var detail = new T_NYUUKIN_DETAIL();
                detail.SYSTEM_ID = ret.NyuukinEntry.SYSTEM_ID;
                detail.SEQ = ret.NyuukinEntry.SEQ;
                detail.DETAIL_SYSTEM_ID = this.CreateSystemId();
                detail.ROW_NUMBER = rowNum;
                detail.NYUUSHUKKIN_KBN_CD = this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD"].Value);
                detail.KINGAKU = this.ConvertToSqlDecimal(row.Cells["KINGAKU"].Value);
                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["MEISAI_BIKOU"].Value)))
                {
                    detail.MEISAI_BIKOU = Convert.ToString(row.Cells["MEISAI_BIKOU"].Value);
                }

                // 入金一括明細エンティティ作成
                var sumDetail = new T_NYUUKIN_SUM_DETAIL();
                sumDetail.SYSTEM_ID = ret.NyuukinEntry.NYUUKIN_SUM_SYSTEM_ID;
                sumDetail.SEQ = ret.NyuukinEntry.SEQ;
                sumDetail.DETAIL_SYSTEM_ID = this.CreateSystemId();
                sumDetail.ROW_NUMBER = rowNum;
                sumDetail.NYUUSHUKKIN_KBN_CD = this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD"].Value);
                sumDetail.KINGAKU = this.ConvertToSqlDecimal(row.Cells["KINGAKU"].Value);
                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["MEISAI_BIKOU"].Value)))
                {
                    sumDetail.MEISAI_BIKOU = Convert.ToString(row.Cells["MEISAI_BIKOU"].Value);
                }

                ret.NyuukinDetailList.Add(detail);
                ret.NyuukinSumDetailList.Add(sumDetail);

                rowNum++;
            }

            var data = new T_NYUUKIN_KESHIKOMI();
            foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
            {
                //if (string.IsNullOrEmpty(Convert.ToString(row.Cells["KESHIKOMI_KINGAKU"].Value)))
                //{
                //    continue;
                //}
                var keshikomi = new T_NYUUKIN_KESHIKOMI();
                keshikomi.SYSTEM_ID = ret.NyuukinEntry.SYSTEM_ID;
                keshikomi.SEIKYUU_NUMBER = this.ConvertToSqlInt64(row.Cells["SEIKYUU_NUMBER"].Value);
                keshikomi.KAGAMI_NUMBER = this.ConvertToSqlInt32(row.Cells["KAGAMI_NUMBER"].Value);

                data = new T_NYUUKIN_KESHIKOMI();
                data.SYSTEM_ID = keshikomi.SYSTEM_ID;
                data.SEIKYUU_NUMBER = keshikomi.SEIKYUU_NUMBER;
                data.KAGAMI_NUMBER = keshikomi.KAGAMI_NUMBER;
                //data.DELETE_FLG = false;
                data = this.keshikomiDao.GetKeshikomi(data).LastOrDefault();
                if (data != null)
                {
                    keshikomi.KESHIKOMI_SEQ = data.KESHIKOMI_SEQ + 1;
                }
                else
                {
                    keshikomi.KESHIKOMI_SEQ = 1;
                }

                keshikomi.NYUUKIN_NUMBER = ret.NyuukinEntry.NYUUKIN_NUMBER;
                keshikomi.TORIHIKISAKI_CD = ret.NyuukinEntry.TORIHIKISAKI_CD;
                keshikomi.KESHIKOMI_GAKU = this.ConvertToSqlDecimal(row.Cells["KESHIKOMI_KINGAKU"].Value);
                keshikomi.KESHIKOMI_BIKOU = Convert.ToString(row.Cells["KESHIKOMI_BIKOU"].Value);
                keshikomi.NYUUKIN_SEQ = 0;
                keshikomi.DELETE_FLG = Convert.ToBoolean(row.Cells["DELETE_FLG"].Value);
                if (!keshikomi.KESHIKOMI_GAKU.IsNull && !keshikomi.DELETE_FLG)
                {
                    ret.NyuukinKeshikomiList.Add(keshikomi);
                }
            }

            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                this.dto.NyuukinEntry.DELETE_FLG = true;
                this.dto.NyuukinSumEntry.DELETE_FLG = true;
                foreach (var keshikomi in this.dto.NyuukinKeshikomiList)
                {
                    keshikomi.DELETE_FLG = true;
                }
                if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    ret.NyuukinEntry.DELETE_FLG = true;
                    ret.NyuukinSumEntry.DELETE_FLG = true;
                    foreach (var keshikomi in ret.NyuukinKeshikomiList)
                    {
                        keshikomi.DELETE_FLG = true;
                    }
                }
            }
            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        internal void Regist()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 必須チェック
                if (this.form.DETAIL_Ichiran.Rows.Count > 0 &&
                    this.form.DETAIL_Ichiran.Rows.Count == this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
                {
                    DataGridViewRow row = this.form.DETAIL_Ichiran.Rows[this.form.DETAIL_Ichiran.Rows.Count - 1];
                    if (!row.IsNewRow && string.IsNullOrEmpty(Convert.ToString(row.Cells["NYUUSHUKKIN_KBN_CD"].Value))
                        && string.IsNullOrEmpty(Convert.ToString(row.Cells["KINGAKU"].Value))
                        && string.IsNullOrEmpty(Convert.ToString(row.Cells["MEISAI_BIKOU"].Value)))
                    {
                        this.form.DETAIL_Ichiran.AllowUserToAddRows = true;
                        this.form.DETAIL_Ichiran.Rows.Remove(row);
                    }
                }

                var autoCheckLogic = new AutoRegistCheckLogic(this.form.allControl, this.form.allControl);
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                // 入金一覧行に入力が無い場合はエラー
                if (!this.form.RegistErrorFlag && this.form.DETAIL_Ichiran.Rows.Count == 1)
                {
                    DataGridViewRow row = this.form.DETAIL_Ichiran.Rows[this.form.DETAIL_Ichiran.Rows.Count - 1];
                    if (row.IsNewRow
                        && (string.IsNullOrEmpty(Convert.ToString(row.Cells["NYUUSHUKKIN_KBN_CD"].Value)) || string.IsNullOrEmpty(Convert.ToString(row.Cells["KINGAKU"].Value))))
                    {
                        msgLogic.MessageBoxShow("E001", "入金区分および入金額");
                        this.form.RegistErrorFlag = true;
                    }
                }

                if (this.form.RegistErrorFlag)
                {
                    var focusControl = this.form.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                    if (null != focusControl)
                    {
                        ((Control)focusControl).Focus();
                    }
                    else
                    {
                        foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                        {
                            bool flg = false;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                var CheckControl = cell as ICustomAutoChangeBackColor;
                                if (CheckControl.IsInputErrorOccured)
                                {
                                    this.form.DETAIL_Ichiran.CurrentCell = cell;
                                    this.form.DETAIL_Ichiran.Focus();
                                    flg = true;
                                    break;
                                }
                            }
                            if (flg)
                            {
                                break;
                            }
                        }
                    }
                    return;
                }

                decimal keshikomiGaku = 0;
                decimal total = 0;
                decimal seikyuu = 0;
                // [入金―消込差額]チェックフラッグ
                bool keshikomiFlg = true;
                foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                {
                    keshikomiGaku = 0;
                    total = 0;
                    seikyuu = 0;
                    string keshikomi_value = Convert.ToString(row.Cells["KESHIKOMI_KINGAKU"].Value);
                    string zenkai_value = Convert.ToString(row.Cells["KESHIKOMI_KINGAKU_ZEN"].Value);
                    string seikyuu_value = Convert.ToString(row.Cells["SEIKYUU_KINGAKU"].Value);
                    if (!string.IsNullOrEmpty(keshikomi_value) && !Convert.ToBoolean(row.Cells["DELETE_FLG"].Value))
                    {
                        keshikomiGaku = Convert.ToDecimal(keshikomi_value.Replace(",", ""));
                    }
                    if (!string.IsNullOrEmpty(zenkai_value))
                    {
                        total = Convert.ToDecimal(zenkai_value.Replace(",", "")) + keshikomiGaku;
                    }
                    if (!string.IsNullOrEmpty(seikyuu_value))
                    {
                        seikyuu = Convert.ToDecimal(seikyuu_value.Replace(",", ""));
                    }
                    // 明細行―消込額　＜　０　が存在する場合、チェック処理無し
                    if (keshikomiGaku < 0)
                    {
                        keshikomiFlg = false;
                    }
                }

                if (this.form.DENPYOU_DATE.Text != this.form.searchDate
                    && this.form.KESHIKOMI_Ichiran.Rows.Count != 0)
                {
                    msgLogic.MessageBoxShow("E249", "伝票日付");
                    return;
                }

                // 金額合計
                if(!this.CalcAll())
                {
                    return;
                }

                // [入金―消込差額]チェック
                if (keshikomiFlg)
                {
                    DialogResult dialogResult = new DialogResult();
                    if (this.sagaku > 0)
                    {
                        dialogResult = msgLogic.MessageBoxShow("C106");
                        if (!dialogResult.Equals(DialogResult.OK)
                            && !dialogResult.Equals(DialogResult.Yes))
                        {
                            return;
                        }
                    }
                    else if (this.sagaku < 0)
                    {
                        dialogResult = msgLogic.MessageBoxShow("C107");
                        if (!dialogResult.Equals(DialogResult.OK)
                            && !dialogResult.Equals(DialogResult.Yes))
                        {
                            return;
                        }
                    }
                }
                //if (keshikomiFlg && this.keshikomiTotal.CompareTo(this.nyuukinTotal + this.chouseiTotal) > 0)
                //{
                //    msgLogic.MessageBoxShow("E228");
                //    this.form.RegistErrorFlag = true;
                //}

                if (!this.form.RegistErrorFlag)
                {
                    // 月次処理チェック
                    GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                    if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        // 修正は伝票日付が変更されている可能性があるため変更前データと違う場合は画面起動から
                        // 登録までの間に月次処理が行われていないか確認する。
                        // 上記が問題なければ現在表示されている変更後の日付が月次処理期間内かをチェックする
                        DateTime beforeDate = DateTime.Parse(this.dto.NyuukinEntry.DENPYOU_DATE.ToString());
                        DateTime updateDate = DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                        // 月次処理中チェック
                        if ((beforeDate.CompareTo(updateDate) != 0) &&
                            getsujiShoriCheckLogic.CheckGetsujiShoriChu(beforeDate))
                        {
                            msgLogic.MessageBoxShow("E224", "修正");
                            this.form.RegistErrorFlag = true;
                        }
                        // 月次処理中チェック
                        else if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(updateDate))
                        {
                            msgLogic.MessageBoxShow("E224", "修正");
                            this.form.RegistErrorFlag = true;
                        }
                        // 月次処理ロックチェック
                        else if ((beforeDate.CompareTo(updateDate) != 0) &&
                            getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(beforeDate.Year.ToString()), short.Parse(beforeDate.Month.ToString())))
                        {
                            msgLogic.MessageBoxShow("E223", "修正");
                            this.form.RegistErrorFlag = true;
                        }
                        // 月次処理ロックチェック
                        else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(updateDate.Year.ToString()), short.Parse(updateDate.Month.ToString())))
                        {
                            msgLogic.MessageBoxShow("E223", "修正");
                            this.form.RegistErrorFlag = true;
                        }
                    }
                    else
                    {
                        // 新規、削除は画面に表示されている伝票日付を使用
                        DateTime getsujiShoriCheckDate = DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                        if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                        {
                            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                            {
                                msgLogic.MessageBoxShow("E224", "登録");
                            }
                            else
                            {
                                msgLogic.MessageBoxShow("E224", "削除");
                            }
                            this.form.RegistErrorFlag = true;
                        }
                        else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                        {
                            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                            {
                                msgLogic.MessageBoxShow("E223", "登録");
                            }
                            else
                            {
                                msgLogic.MessageBoxShow("E222", "削除");
                            }
                            this.form.RegistErrorFlag = true;
                        }
                    }
                }

                // 締済チェック
                if (!this.form.RegistErrorFlag && this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    var seikyuuDetailList = this.seikyuuDetailDao.GetDataByNyuukinNumber(this.dto.NyuukinSumEntry.NYUUKIN_NUMBER);
                    if (seikyuuDetailList.Count() > 0)
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        {
                            messageLogic.MessageBoxShow("I011", "修正");
                        }
                        else
                        {
                            messageLogic.MessageBoxShow("I011", "削除");
                        }
                        this.form.RegistErrorFlag = true;
                    }
                }

                // 請求日付チェック
                if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType && false == this.form.RegistErrorFlag)
                {
                    if (!this.SeikyuuDateCheck())
                    {
                        this.form.RegistErrorFlag = true;
                    }
                }

                DialogResult result = new DialogResult();
                if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType && false == this.form.RegistErrorFlag)
                {
                    result = msgLogic.MessageBoxShow("C026");
                    if (result == DialogResult.Yes)
                    {
                        this.form.RegistErrorFlag = false;
                    }
                    else
                    {
                        this.form.RegistErrorFlag = true;
                    }
                }

                if (!this.form.RegistErrorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        var registDto = this.CreateEntity();

                        if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                        {
                            // 入金
                            this.entryDao.Insert(registDto.NyuukinEntry);

                            // 入金一括
                            this.sumEntryDao.Insert(registDto.NyuukinSumEntry);

                            // 入金明細
                            foreach (var detail in registDto.NyuukinDetailList)
                            {
                                this.detailDao.Insert(detail);
                            }

                            // 入金一括明細
                            foreach (var sumDetail in registDto.NyuukinSumDetailList)
                            {
                                this.sumDetailDao.Insert(sumDetail);
                            }

                            var data = new T_NYUUKIN_KESHIKOMI();
                            // 入金消込
                            foreach (var keshikomi in registDto.NyuukinKeshikomiList)
                            {
                                this.keshikomiDao.Insert(keshikomi);
                            }

                            if (this.form.CASHER_LINK_KBN.Text == CommonConst.CASHER_LINK_KBN_USE)
                            {
                                // キャッシャ連動「1.する」の場合キャッシャ情報送信
                                this.sendCasher(registDto.NyuukinSumEntry, registDto.NyuukinSumDetailList);
                            }
                        }
                        else
                        {
                            // 入金
                            this.entryDao.Update(this.dto.NyuukinEntry);
                            this.entryDao.Insert(registDto.NyuukinEntry);

                            // 入金一括
                            this.sumEntryDao.Update(this.dto.NyuukinSumEntry);
                            this.sumEntryDao.Insert(registDto.NyuukinSumEntry);

                            // 入金明細
                            foreach (var detail in this.dto.NyuukinDetailList)
                            {
                                this.detailDao.Update(detail);
                            }
                            foreach (var detail in registDto.NyuukinDetailList)
                            {
                                this.detailDao.Insert(detail);
                            }

                            // 入金一括明細
                            foreach (var sumDetail in this.dto.NyuukinSumDetailList)
                            {
                                this.sumDetailDao.Update(sumDetail);
                            }
                            foreach (var sumDetail in registDto.NyuukinSumDetailList)
                            {
                                this.sumDetailDao.Insert(sumDetail);
                            }

                            // 入金消込
                            var data = new T_NYUUKIN_KESHIKOMI();
                            data = new T_NYUUKIN_KESHIKOMI();
                            data.SYSTEM_ID = this.dto.NyuukinEntry.SYSTEM_ID;
                            data.NYUUKIN_NUMBER = this.dto.NyuukinEntry.NYUUKIN_NUMBER;
                            data.DELETE_FLG = false;
                            var datas = this.keshikomiDao.GetKeshikomi(data);

                            foreach (var keshikomi in datas)
                            {
                                keshikomi.DELETE_FLG = true;
                                this.keshikomiDao.Update(keshikomi);
                            }

                            foreach (var keshikomi in registDto.NyuukinKeshikomiList)
                            {
                                this.keshikomiDao.Insert(keshikomi);
                            }
                        }

                        tran.Commit();
                    }

                    var messageLogic = new MessageBoxShowLogic();
                    if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.form.WindowType || WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType)
                    {
                        messageLogic.MessageBoxShow("I001", "登録");
                        this.SetPrevStatus();
                    }
                    else
                    {
                        messageLogic.MessageBoxShow("I001", "削除");
                    }

                    var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());
                    if (Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.SetNyuukinNumber(SqlInt64.Null);
                        this.WindowInit(false);
                        this.form.DENPYOU_DATE.Focus();
                    }
                    else
                    {
                        // 追加権限が無い場合は画面を閉じる
                        var parentForm = (BusinessBaseForm)this.form.Parent;
                        this.form.Close();
                        parentForm.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex);
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex);
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex);
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 並べ替え処理

        ///// <summary>
        ///// 並べ替え処理
        ///// </summary>
        //internal void DateSort()
        //{
        //    if (this.form.KESHIKOMI_Ichiran.Rows.Count < 1)
        //    {
        //        return;
        //    }

        //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //    DialogResult ret = msgLogic.MessageBoxShow("C055", "入力されている消込額をクリアして、並べ替え");
        //    if (ret != DialogResult.Yes)
        //    {
        //        return;
        //    }

        //    this.Search(true);
        //}

        #endregion

        #region 一括消込処理

        /// <summary>
        /// 一括消込処理
        /// </summary>
        internal bool IkkatuKeshikomi()
        {
            bool retVal = true;
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                DialogResult ret = msgLogic.MessageBoxShow("C086");
                if (ret != DialogResult.Yes)
                {
                    return retVal;
                }

                // 入金消込データの検索
                if (this.Search() == -1)
                {
                    retVal = false;
                    return retVal;
                }

                // 入金額合計の取得
                decimal kingakuTotal = 0;
                foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    kingakuTotal += this.ConvertToDecimal(row.Cells["KINGAKU"].Value);
                }

                // 未消込額合計の初期値設定
                decimal mikeshikomiTotal = 0;
                foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                {
                    mikeshikomiTotal += this.ConvertToDecimal(row.Cells["MIKESHIKOMI_KINGAKU"].Value) + this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU"].Value);
                }

                decimal seikyuu = 0;
                decimal zenkai = 0;
                decimal keshikomi = 0;
                decimal mikeshikomi = 0;
                if (mikeshikomiTotal.CompareTo(0) >= 0)
                {
                    // 未消込額合計≧0　の場合
                    foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                    {
                        seikyuu = this.ConvertToDecimal(row.Cells["SEIKYUU_KINGAKU"].Value);
                        zenkai = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU_ZEN"].Value);
                        keshikomi = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU"].Value);
                        mikeshikomi = this.ConvertToDecimal(row.Cells["MIKESHIKOMI_KINGAKU"].Value);

                        if (seikyuu > 0)
                        {
                            if ((kingakuTotal + zenkai) > seikyuu)
                            {
                                if ((seikyuu - zenkai) > 0)
                                {
                                    // 未消込額がまだある場合
                                    if (seikyuu - zenkai >= 1)
                                    {
                                        row.Cells["KESHIKOMI_KINGAKU"].Value = seikyuu - zenkai;
                                    }
                                    else
                                    {
                                        row.Cells["KESHIKOMI_KINGAKU"].Value = null;
                                    }
                                    row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = seikyuu;
                                    row.Cells["MIKESHIKOMI_KINGAKU"].Value = 0;
                                }
                                else
                                {
                                    // 前回時点で満額消込されている場合
                                    row.Cells["KESHIKOMI_KINGAKU"].Value = null;
                                    row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = zenkai;
                                    row.Cells["MIKESHIKOMI_KINGAKU"].Value = seikyuu - zenkai;
                                }

                                decimal kingakuWk = kingakuTotal;
                                kingakuWk -= seikyuu;
                                kingakuWk += zenkai;
                                if (kingakuWk <= kingakuTotal)
                                {
                                    kingakuTotal = kingakuWk;
                                }
                            }
                            else
                            {
                                decimal keshikomi_kingaku = ((seikyuu - kingakuTotal - zenkai) > 0) ? kingakuTotal : seikyuu - zenkai;
                                if (keshikomi_kingaku >= 1)
                                {
                                    row.Cells["KESHIKOMI_KINGAKU"].Value = keshikomi_kingaku;
                                }
                                else
                                {
                                    row.Cells["KESHIKOMI_KINGAKU"].Value = null;
                                }
                                row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = ((seikyuu - kingakuTotal - zenkai) > 0) ? kingakuTotal + zenkai : seikyuu;
                                row.Cells["MIKESHIKOMI_KINGAKU"].Value = ((seikyuu - kingakuTotal - zenkai) > 0) ? seikyuu - kingakuTotal - zenkai : 0;
                                kingakuTotal = 0;
                            }
                        }
                    }
                }
                else
                {
                    // 未消込額合計＜0　の場合（請求金額がマイナスの場合など）
                    foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                    {
                        seikyuu = this.ConvertToDecimal(row.Cells["SEIKYUU_KINGAKU"].Value);
                        zenkai = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU_ZEN"].Value);
                        keshikomi = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU"].Value);
                        mikeshikomi = this.ConvertToDecimal(row.Cells["MIKESHIKOMI_KINGAKU"].Value);

                        if (seikyuu < 0)
                        {
                            if ((kingakuTotal + zenkai) <= seikyuu)
                            {
                                if ((seikyuu - zenkai) <= 0)
                                {
                                    // 未消込額がまだある場合
                                    if (seikyuu - zenkai >= 1)
                                    {
                                        row.Cells["KESHIKOMI_KINGAKU"].Value = seikyuu - zenkai;
                                    }
                                    else
                                    {
                                        row.Cells["KESHIKOMI_KINGAKU"].Value = null;
                                    }
                                    row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = seikyuu;
                                    row.Cells["MIKESHIKOMI_KINGAKU"].Value = 0;
                                }
                                else
                                {
                                    // 前回時点で満額消込されている場合
                                    row.Cells["KESHIKOMI_KINGAKU"].Value = null;
                                    row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = zenkai;
                                    row.Cells["MIKESHIKOMI_KINGAKU"].Value = seikyuu - zenkai;
                                }

                                decimal kingakuWk = kingakuTotal;
                                kingakuWk -= seikyuu;
                                kingakuWk += zenkai;
                                if (kingakuWk > kingakuTotal)
                                {
                                    kingakuTotal = kingakuWk;
                                }
                            }
                            else
                            {
                                decimal keshikomi_kingaku = ((seikyuu - kingakuTotal - zenkai) <= 0) ? kingakuTotal : seikyuu - zenkai;
                                if (keshikomi_kingaku >= 1)
                                {
                                    row.Cells["KESHIKOMI_KINGAKU"].Value = keshikomi_kingaku;
                                }
                                else
                                {
                                    row.Cells["KESHIKOMI_KINGAKU"].Value = null;
                                }
                                row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = ((seikyuu - kingakuTotal - zenkai) <= 0) ? kingakuTotal + zenkai : seikyuu;
                                row.Cells["MIKESHIKOMI_KINGAKU"].Value = ((seikyuu - kingakuTotal - zenkai) <= 0) ? seikyuu - kingakuTotal - zenkai : 0;
                                kingakuTotal = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IkkatuKeshikomi", ex);
                this.errmessage.MessageBoxShow("E245", "");
                retVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retVal);
            }
            return retVal;
        }

        #endregion

        #region 手数料消込処理

        /// <summary>
        /// 手数料消込処理
        /// </summary>
        internal bool TesuuryouKeshikomi()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                decimal seikyuu = 0;
                decimal keshikomiT = 0;
                decimal mikeshikomi = 0;
                decimal tesuuryou = 0;

                foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                {
                    // 未消込額合計を算出する
                    seikyuu = this.ConvertToDecimal(row.Cells["SEIKYUU_KINGAKU"].Value);
                    keshikomiT = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value);
                    mikeshikomi = seikyuu - keshikomiT;
                    if (mikeshikomi.CompareTo(0) != 0)
                    {
                        tesuuryou += mikeshikomi;
                    }
                }

                if (tesuuryou.CompareTo(0) == 0)
                {
                    // 未消込額合計=0の場合は、処理を抜ける
                    return ret;
                }

                bool tesuuryouFlg = false;
                SqlInt16 kbn = 0;
                decimal kingaku = 0;
                foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                {
                    // 入金区分行に21:手数料の行が存在する場合は、その金額に未消込額合計を加算し、21:手数料の行は追加しない
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    kbn = this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD"].Value);
                    if (!kbn.IsNull && kbn == 21)
                    {
                        kingaku = this.ConvertToDecimal(row.Cells["KINGAKU"].Value);
                        row.Cells["KINGAKU"].Value = kingaku + tesuuryou;
                        tesuuryouFlg = true;
                        break;
                    }
                }

                if (!tesuuryouFlg)
                {
                    // 入金区分行に21:手数料の行が存在しない場合
                    M_NYUUSHUKKIN_KBN data = new M_NYUUSHUKKIN_KBN();
                    DataGridViewRow row = this.form.DETAIL_Ichiran.Rows[0];
                    int count = this.form.DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
                    if (count == this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
                    {
                        // 入金区分表示数の最大値＝入金区分行行数、となる場合はアラート
                        // ただし、最後の行がブランクの場合を除く
                        row = this.form.DETAIL_Ichiran.Rows[this.form.DETAIL_Ichiran.Rows.Count - 1];
                        if (string.IsNullOrEmpty(Convert.ToString(row.Cells["NYUUSHUKKIN_KBN_CD"].Value))
                            && string.IsNullOrEmpty(Convert.ToString(row.Cells["KINGAKU"].Value))
                            && string.IsNullOrEmpty(Convert.ToString(row.Cells["MEISAI_BIKOU"].Value)))
                        {
                            row.Cells["NYUUSHUKKIN_KBN_CD"].Value = 21;
                            data = this.nyuushukkinKbnDao.GetDataByCd(21);
                            if (data != null)
                            {
                                row.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = data.NYUUSHUKKIN_KBN_NAME_RYAKU;
                            }
                            row.Cells["KINGAKU"].Value = tesuuryou;
                        }
                        else
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShowError("表示数が最大値を超えてしまうため、手数料消込を実行できません。");
                            return ret;
                        }
                    }
                    else
                    {
                        // 21:手数料の入金区分行を追加準備
                        this.form.DETAIL_Ichiran.Rows.Add();
                        row = this.form.DETAIL_Ichiran.Rows[this.form.DETAIL_Ichiran.Rows.Count - 2];
                    }

                    // 21:手数料の入金区分行を追加
                    row.Cells["NYUUSHUKKIN_KBN_CD"].Value = 21;
                    data = this.nyuushukkinKbnDao.GetDataByCd(21);
                    if (data != null)
                    {
                        row.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = data.NYUUSHUKKIN_KBN_NAME_RYAKU;
                    }
                    row.Cells["KINGAKU"].Value = tesuuryou;
                    if (this.form.DETAIL_Ichiran.Rows.Count > this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
                    {
                        this.form.DETAIL_Ichiran.AllowUserToAddRows = false;
                    }
                }

                // 消込額行の更新
                UpdateKeshikomiRow();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("TesuuryouKeshikomi", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TesuuryouKeshikomi", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 消込額行の更新
        /// </summary>
        private void UpdateKeshikomiRow()
        {
            decimal seikyuu = 0;
            decimal keshikomi = 0;
            decimal keshikomiT = 0;
            decimal mikeshikomi = 0;

            foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
            {
                // 未消込額を消込額、総消込額に加算し、未消込額を0とする
                seikyuu = this.ConvertToDecimal(row.Cells["SEIKYUU_KINGAKU"].Value);
                keshikomi = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU"].Value);
                keshikomiT = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value);
                mikeshikomi = seikyuu - keshikomiT;

                if (mikeshikomi.CompareTo(0) != 0)
                {
                    row.Cells["KESHIKOMI_KINGAKU"].Value = keshikomi + mikeshikomi;
                    row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = keshikomiT + mikeshikomi;
                    row.Cells["MIKESHIKOMI_KINGAKU"].Value = 0;
                }
            }
        }

        #endregion

        #region 行削除処理

        /// <summary>
        /// 行削除処理
        /// </summary>
        internal bool RowRemove()
        {
            bool ret = true;
            try
            {
                if (this.form.DETAIL_Ichiran.CurrentRow != null)
                {
                    if (!this.form.DETAIL_Ichiran.CurrentRow.IsNewRow)
                    {
                        this.form.DETAIL_Ichiran.Rows.RemoveAt(this.form.DETAIL_Ichiran.CurrentRow.Index);
                    }
                }

                if (this.form.DETAIL_Ichiran.Rows.Count < this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
                {
                    this.form.DETAIL_Ichiran.AllowUserToAddRows = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RowRemove", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 行挿入

        /// <summary>
        /// 行挿入処理
        /// </summary>
        internal bool RowAdd()
        {
            bool ret = true;
            try
            {
                if (this.form.DETAIL_Ichiran.Rows.Count < this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
                {
                    if (this.form.DETAIL_Ichiran.CurrentRow == null)
                    {
                        this.form.DETAIL_Ichiran.Rows.Add();
                    }
                    else
                    {
                        this.form.DETAIL_Ichiran.Rows.Insert(this.form.DETAIL_Ichiran.CurrentRow.Index, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RowAdd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region システムID採番

        /// <summary>
        /// 新規にシステムIDを取得します
        /// </summary>
        /// <returns>システムID</returns>
        private SqlInt64 CreateSystemId()
        {
            LogUtility.DebugMethodStart();

            var ret = SqlInt64.Null;

            var accessor = new DBAccessor();
            ret = accessor.createSystemId((int)DENSHU_KBN.NYUUKIN);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region 入金番号採番

        /// <summary>
        /// 新規に入金番号を取得します
        /// </summary>
        /// <returns></returns>
        public SqlInt64 CreateNyuukinNumber()
        {
            LogUtility.DebugMethodStart();

            var ret = SqlInt64.Null;

            var accessor = new DBAccessor();
            ret = accessor.createDenshuNumber((int)DENSHU_KBN.NYUUKIN);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region 前回値保存

        /// <summary>
        /// 前回値保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SetPrevStatus()
        {
            Properties.Settings.Default.SET_BANK_CD = this.form.BANK_CD.Text;
            Properties.Settings.Default.SET_BANK_SHITEN_CD = this.form.BANK_SHITEN_CD.Text;
            Properties.Settings.Default.SET_KOUZA_NO = this.form.KOUZA_NO.Text;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region ソート列取得

        public void SetDataGridViewColumns(DataGridView grid)
        {
            // ソート用に表示カラムのみコピー
            var gridColumns = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn gridColumn in grid.Columns)
            {
                if (gridColumn.Visible)
                {
                    gridColumns.Add(gridColumn);
                }
            }

            // 表示インデックスでソート
            gridColumns.Sort(
                delegate(DataGridViewColumn x, DataGridViewColumn y)
                {
                    return x.DisplayIndex - y.DisplayIndex;
                }
            );

            // グリッドの表示列タイトルでリスト作成
            this.sortSettingInfo.ViewColumns.Clear();// = new List<CustomSortColumn>();
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                var viewColumn = new CustomSortColumn(gridColumn.Name, gridColumn.HeaderText, true);
                this.sortSettingInfo.ViewColumns.Add(viewColumn);
            }

            // 存在しているものだけをソート項目に残す
            CustomSortColumn[] tempColumns = new CustomSortColumn[8];
            this.sortSettingInfo.SortColumns.CopyTo(tempColumns);
            this.sortSettingInfo.SortColumns.Clear();
            foreach (var sortColumn in tempColumns)
            {
                if (sortColumn == null)
                {
                    continue;
                }
                foreach (var viewColumn in this.sortSettingInfo.ViewColumns)
                {
                    if (viewColumn.Name.Equals(sortColumn.Name))
                    {
                        this.sortSettingInfo.SortColumns.Add(sortColumn);
                        break;
                    }
                }
            }
        }

        #endregion

        #region ソート

        /// <summary>
        /// ソート
        /// </summary>
        public DataTable SortDataTable(DataTable dataTable)
        {
            if (dataTable == null)
            {
                return new DataTable();
            }

            if (sortSettingInfo == null)
            {
                return dataTable;
            }

            sortSettingInfo.SetDataTableColumns(dataTable);
            var sb = new StringBuilder();
            string name = "";

            foreach (var item in sortSettingInfo.SortColumns)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                name = item.Name;
                if (name == "SEIKYUU_DATE")
                {
                    name = "SORT_SEIKYUU_DATE";
                }
                sb.AppendFormat("{0} {1}", name, item.IsAsc ? "ASC" : "DESC");
            }

            if (sb.Length == 0)
            {
                sb.Append("SORT_SEIKYUU_DATE ASC,GYOUSHA_CD ASC,GENBA_CD ASC");
            }
            DataTable dt = dataTable.Select("1=1", sb.ToString()).CopyToDataTable();
            return dt;
        }

        #endregion

        #endregion

        #region 金額合計

        /// <summary>
        /// 金額合計
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool CalcAll()
        {
            bool ret = true;
            try
            {
                this.nyuukinTotal = 0;
                this.keshikomiTotal = 0;
                this.chouseiTotal = 0;
                SqlInt16 kbnCd;
                decimal kingaku = 0;
                foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                {
                    kbnCd = this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD"].Value);
                    if (kbnCd.IsNull)
                    {
                        continue;
                    }
                    kingaku = this.ConvertToDecimal(row.Cells["KINGAKU"].Value);
                    if (kbnCd.Value >= 21 && kbnCd.Value <= 50)
                    {
                        this.chouseiTotal += kingaku;
                    }
                    else
                    {
                        this.nyuukinTotal += kingaku;
                    }
                }
                this.keshikomiFlg = false;
                foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                {
                    if (!Convert.ToBoolean(row.Cells["DELETE_FLG"].Value))
                    {
                        kingaku = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU"].Value);
                        this.keshikomiTotal += kingaku;
                        if (kingaku != 0)
                        {
                            this.keshikomiFlg = true;
                        }
                    }
                }
                this.form.NYUUKIN_AMOUNT_TOTAL.Text = this.FormatKingaku(nyuukinTotal);
                this.form.CHOUSEI_AMOUNT_TOTAL.Text = this.FormatKingaku(chouseiTotal);
                this.form.TOTAL.Text = this.FormatKingaku(chouseiTotal + nyuukinTotal);
                this.form.KESHIKOMIGAKU.Text = this.FormatKingaku(keshikomiTotal);
                // 入金―消込差額
                this.sagaku = chouseiTotal + nyuukinTotal - keshikomiTotal;
                this.form.SAGAKU.Text = this.FormatKingaku(this.sagaku);
                if (this.sagaku == 0)
                {
                    this.form.SAGAKU.ForeColor = Color.Black;
                }
                else
                {
                    this.form.SAGAKU.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcAll", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region IBuisinessLogicの実装

        //public int Search()
        //{
        //    throw new NotImplementedException();
        //}

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 請求日付チェック

        /// <summary>
        /// 請求日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool SeikyuuDateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
            List<ReturnDate> returnDate = new List<ReturnDate>();
            List<CheckDate> checkDate = new List<CheckDate>();
            CheckDate cd = new CheckDate();
            ReturnDate rd = new ReturnDate();

            string strKyotenCd = this.headerForm.txtKyotenCd.Text;
            string strTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
            string strDenpyouDate = this.form.DENPYOU_DATE.Text;

            if (string.IsNullOrEmpty(strDenpyouDate))
            {
                return true;
            }
            if (string.IsNullOrEmpty(strTorihikisakiCd))
            {
                return true;
            }
            if (string.IsNullOrEmpty(strKyotenCd))
            {
                return true;
            }

            DateTime DenpyouDate = Convert.ToDateTime(strDenpyouDate);

            cd.TORIHIKISAKI_CD = strTorihikisakiCd;
            cd.CHECK_DATE = DenpyouDate;
            cd.KYOTEN_CD = strKyotenCd;
            checkDate.Add(cd);

            returnDate = CheckShimeDate.GetNearShimeDate(checkDate, 1);

            if (returnDate.Count == 0)
            {
                return true;
            }
            else
            {
                //例外日付が含まれる
                foreach (ReturnDate rdDate in returnDate)
                {
                    if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                }
                if (msgLogic.MessageBoxShow("C084", returnDate[0].dtDATE.ToString("yyyy/MM/dd"), "請求") == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region 画面設定

        /// <summary>
        /// 取得した入金伝票を画面にセットします
        /// </summary>
        internal void SetNyuukinData()
        {
            LogUtility.DebugMethodStart();

            this.ClearFormData(true);

            this.GetNyuukinData(this.nyuukinNumber, this.seq);
            if (null == this.dto.NyuukinEntry)
            {
                // 該当データなし
                var messageLogic = new MessageBoxShowLogic();
                messageLogic.MessageBoxShow("E045");
                this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                this.SetNyuukinNumber(SqlInt64.Null);
                if (!this.WindowInit(true))
                {
                    return;
                }
            }
            else
            {
                // 画面にデータセット
                // 拠点
                this.headerForm.txtKyotenCd.Text = this.dto.NyuukinEntry.KYOTEN_CD.Value.ToString("00");
                var kyoten = this.kyotenDao.GetDataByCd(this.dto.NyuukinEntry.KYOTEN_CD.Value.ToString());
                if (null != kyoten)
                {
                    this.headerForm.txtKyotenName.Text = kyoten.KYOTEN_NAME_RYAKU;
                }
                // 入金番号
                this.form.NYUUKIN_NUMBER.Text = this.dto.NyuukinEntry.NYUUKIN_NUMBER.Value.ToString();
                // 伝票日付
                this.form.DENPYOU_DATE.Value = this.dto.NyuukinEntry.DENPYOU_DATE;
                // 取引先CD
                this.form.TORIHIKISAKI_CD.Text = this.dto.NyuukinEntry.TORIHIKISAKI_CD;

                M_TORIHIKISAKI torihikisaki = new M_TORIHIKISAKI();
                torihikisaki.TORIHIKISAKI_CD = this.dto.NyuukinEntry.TORIHIKISAKI_CD;
                torihikisaki.ISNOT_NEED_DELETE_FLG = true;
                torihikisaki = torihikisakiDao.GetAllValidData(torihikisaki).FirstOrDefault();

                if (null != torihikisaki)
                {
                    this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                }
                // 入金先CD
                this.form.NYUUKINSAKI_CD.Text = this.dto.NyuukinEntry.NYUUKINSAKI_CD;
                var nyuukinsaki = this.nyuukinsakiDao.GetDataByCd(this.dto.NyuukinEntry.NYUUKINSAKI_CD);
                if (null != nyuukinsaki)
                {
                    this.form.NYUUKINSAKI_NAME.Text = nyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                }
                // 銀行CD
                this.form.BANK_CD.Text = this.dto.NyuukinEntry.BANK_CD;
                var bank = this.bankDao.GetDataByCd(this.dto.NyuukinEntry.BANK_CD);
                if (null != bank)
                {
                    this.form.BANK_NAME_RYAKU.Text = bank.BANK_NAME_RYAKU;
                }
                else
                {
                    this.form.BANK_NAME_RYAKU.Text = "";
                }
                // 銀行支店CD
                this.form.BANK_SHITEN_CD.Text = this.dto.NyuukinEntry.BANK_SHITEN_CD;
                var bankShiten = new M_BANK_SHITEN();
                bankShiten.BANK_CD = this.dto.NyuukinEntry.BANK_CD;
                bankShiten.BANK_SHITEN_CD = this.dto.NyuukinEntry.BANK_SHITEN_CD;
                bankShiten.KOUZA_NO = this.dto.NyuukinEntry.KOUZA_NO;
                bankShiten = this.bankShitenDao.GetDataByCd(bankShiten);
                if (null != bankShiten)
                {
                    this.form.BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                }
                else
                {
                    this.form.BANK_SHIETN_NAME_RYAKU.Text = "";
                }
                // 口座種類
                this.form.KOUZA_SHURUI.Text = this.dto.NyuukinEntry.KOUZA_SHURUI;
                // 口座番号
                this.form.KOUZA_NO.Text = this.dto.NyuukinEntry.KOUZA_NO;
                this.form.KOUZA_NAME.Text = this.dto.NyuukinEntry.KOUZA_NAME;

                var index = 0;
                if (this.dto.NyuukinDetailList.Count > 0)
                {
                    this.form.DETAIL_Ichiran.Rows.Add(this.dto.NyuukinDetailList.Count);
                    index = 0;
                    foreach (var entity in this.dto.NyuukinDetailList)
                    {
                        var row = this.form.DETAIL_Ichiran.Rows[index];
                        row.Cells["NYUUSHUKKIN_KBN_CD"].Value = entity.NYUUSHUKKIN_KBN_CD.Value;
                        var nyuushukkinKbn = this.nyuushukkinKbnDao.GetDataByCd(entity.NYUUSHUKKIN_KBN_CD.Value);
                        if (null != nyuushukkinKbn)
                        {
                            row.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = nyuushukkinKbn.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                        if (!entity.KINGAKU.IsNull)
                        {
                            row.Cells["KINGAKU"].Value = entity.KINGAKU.Value;
                        }
                        row.Cells["MEISAI_BIKOU"].Value = entity.MEISAI_BIKOU;

                        index++;
                    }
                }

                // 入金消込
                if (this.Search() == -1)
                {
                    return;
                }
            }

            this.CalcAll();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面のコントロールを初期化

        /// <summary>
        /// 画面のコントロールを初期化します
        /// </summary>
        /// <param name="isClearDenpyouDate">伝票日付をクリアするかどうかのフラグ</param>
        internal void ClearFormData(bool isClearDate)
        {
            LogUtility.DebugMethodStart();

            // 画面上のデータをクリア
            this.form.DETAIL_Ichiran.AllowUserToAddRows = false;

            // 修正⇒新規モード時などで削除済みデータがある場合、イベントが走ってエラーが出るので一旦手動で入金区分を削除する
            if (this.form.DETAIL_Ichiran != null && this.form.DETAIL_Ichiran.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                {
                    row.Cells["NYUUSHUKKIN_KBN_CD"].Value = null;
                }
            }

            this.form.DETAIL_Ichiran.Rows.Clear();
            this.form.KESHIKOMI_Ichiran.Rows.Clear();
            this.form.KESHIKOMI_Ichiran.Columns["GYOUSHA_CD"].Visible = true;
            this.form.KESHIKOMI_Ichiran.Columns["GYOUSHA_NAME"].Visible = true;
            this.form.KESHIKOMI_Ichiran.Columns["GENBA_CD"].Visible = true;
            this.form.KESHIKOMI_Ichiran.Columns["GENBA_NAME"].Visible = true;

            this.SetKyoten();

            if (!this.nyuukinNumber.IsNull)
            {
                this.form.NYUUKIN_NUMBER.Text = this.nyuukinNumber.Value.ToString();
            }
            else
            {
                this.form.NYUUKIN_NUMBER.Text = string.Empty;
            }
            if (isClearDate)
            {
                this.form.DENPYOU_DATE.Value = this.parentForm.sysDate;
            }
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_CD.BackColor = Constans.NOMAL_COLOR;
            this.form.TORIHIKISAKI_NAME.Text = string.Empty;
            this.form.NYUUKINSAKI_CD.Text = string.Empty;
            this.form.NYUUKINSAKI_NAME.Text = string.Empty;

            this.SetCorpBank();

            this.form.KOUZA_SHURUI.Text = string.Empty;
            this.form.KOUZA_NO.Text = string.Empty;
            this.form.KOUZA_NAME.Text = string.Empty;

            //並び順をクリア
            this.sortSettingInfo = SortSettingHelper.LoadSortSettingInfo("UIForm.KESHIKOMI_Ichiran");

            this.form.isError = false;
            this.form.beforeTorihikisakiCd = "";
            this.form.beforeBankCd = "";
            this.form.searchDate = "";

            this.form.NYUUKIN_AMOUNT_TOTAL.Text = "0";
            this.form.CHOUSEI_AMOUNT_TOTAL.Text = "0";
            this.form.KESHIKOMIGAKU.Text = "0";
            this.form.TOTAL.Text = "0";
            // 入金―消込差額
            this.form.SAGAKU.Text = "0";
            this.form.SAGAKU.ForeColor = Color.Black;//160013
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面イベント

        #region 入金番号Validated

        /// <summary>
        /// 入金番号Validated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal bool NYUUKIN_NUMBER_Validated(object sender, EventArgs e)
        {
            bool ret = true;
            try
            {
                // 再読み込みするかを示します
                bool isReload = false;

                // 入金蛮行空じゃない場合
                if (!string.IsNullOrEmpty(this.form.NYUUKIN_NUMBER.Text))
                {
                    // 前回値がNULLじゃない場合
                    if (!this.nyuukinNumber.IsNull)
                    {
                        // 入金番号と前回値が同じじゃない場合
                        if (this.ConvertToSqlInt64(this.form.NYUUKIN_NUMBER.Text).Value != this.nyuukinNumber.Value)
                        {
                            isReload = true;
                        }
                    }
                    else
                    {
                        isReload = true;
                    }
                }
                else
                {
                    // 入金番号の前回値をクリア
                    this.SetNyuukinNumber(SqlInt64.Null);
                }

                // 上記条件に合致する場合は読み込みを行う
                if (isReload)
                {
                    var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());

                    if (!Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                        && !Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.UPDATE_WINDOW_FLAG));

                        this.form.NYUUKIN_NUMBER.Focus();
                        return ret;
                    }

                    var entry = new T_NYUUKIN_ENTRY();
                    entry.NYUUKIN_NUMBER = this.ConvertToSqlInt64(this.form.NYUUKIN_NUMBER.Text);
                    entry.DELETE_FLG = false;
                    entry = this.entryDao.GetNyuukinEntry(entry);
                    if (entry != null && entry.TOK_INPUT_KBN.IsTrue)
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E225");
                        this.form.NYUUKIN_NUMBER.Focus();
                        return ret;
                    }
                    else if (null == entry)
                    {
                        // 該当データなし
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E045");
                        this.form.NYUUKIN_NUMBER.Focus();
                        return ret;
                    }

                    this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.SetNyuukinNumber(this.ConvertToSqlInt64(this.form.NYUUKIN_NUMBER.Text));
                    if (!this.WindowInit(true))
                    {
                        ret = false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NYUUKIN_NUMBER_Validated", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("NYUUKIN_NUMBER_Validated", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 伝票日付Validated

        /// <summary>
        /// 伝票日付Validated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void DENPYOU_DATE_Validated(object sender, EventArgs e)
        {
            //var messageLogic = new MessageBoxShowLogic();

            //if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            //{
            //    this.form.KESHIKOMI_Ichiran.Rows.Clear();
            //    return;
            //}

            //if (this.form.beforeDenpyouDate != this.form.DENPYOU_DATE.Text || this.form.isError)
            //{
            //    if (this.form.isSearch)
            //    {
            //        DialogResult ret = messageLogic.MessageBoxShow("C087", "伝票日付");

            //        if (ret != DialogResult.Yes)
            //        {
            //            this.form.DENPYOU_DATE.Text = this.form.beforeDenpyouDate;
            //            this.form.DENPYOU_DATE.Focus();
            //            return;
            //        }
            //    }

            //    this.form.isError = false;
            //    this.form.isSearch = false;
            //    this.form.KESHIKOMI_Ichiran.Rows.Clear();
            //}

            // 入金消込データがある場合は、伝票日付変更時にアラート表示
            if (this.form.beforeDenpyouDate != this.form.DENPYOU_DATE.Text)
            {
                if (KeshikomiUmuCheck())
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E233", "伝票日付");
                    this.form.DENPYOU_DATE.Text = this.form.beforeDenpyouDate;
                    this.form.DENPYOU_DATE.Focus();
                    return;
                }
            }
        }

        #endregion

        #region 取引先CDValidated

        /// <summary>
        /// 取引先CDValidated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal bool TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            bool ret = true;
            try
            {
                var messageLogic = new MessageBoxShowLogic();

                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    if (KeshikomiUmuCheck())
                    {
                        this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                        messageLogic.MessageBoxShow("E233", "取引先");
                        this.form.TORIHIKISAKI_CD.Text = this.form.beforeTorihikisakiCd;
                        this.form.TORIHIKISAKI_CD.Focus();
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME.Text = "";
                        this.form.NYUUKINSAKI_CD.Text = "";
                        this.form.NYUUKINSAKI_NAME.Text = "";
                        this.form.beforeTorihikisakiCd = "";
                        this.form.KESHIKOMI_Ichiran.Rows.Clear();
                    }
                    return ret;
                }

                if (this.form.beforeTorihikisakiCd != this.form.TORIHIKISAKI_CD.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                    {
                        this.form.KESHIKOMI_Ichiran.Rows.Clear();
                    }
                    M_TORIHIKISAKI torihikisaki = new M_TORIHIKISAKI();
                    torihikisaki.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    torihikisaki = this.torihikisakiDao.GetAllValidData(torihikisaki).FirstOrDefault();

                    if (torihikisaki == null || torihikisaki.TORIHIKISAKI_KYOTEN_CD.IsNull
                        || (torihikisaki.TORIHIKISAKI_KYOTEN_CD.Value != 99 && torihikisaki.TORIHIKISAKI_KYOTEN_CD.Value.ToString("00") != headerForm.txtKyotenCd.Text))
                    {
                        this.form.isError = true;
                        this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                        messageLogic.MessageBoxShow("E020", "取引先");
                        this.form.TORIHIKISAKI_CD.Focus();
                        return ret;
                    }
                    else
                    {
                        SqlDateTime tekiyouDate = this.parentForm.sysDate;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.DENPYOU_DATE.Text) && DateTime.TryParse(this.form.DENPYOU_DATE.Text, out date))
                        {
                            tekiyouDate = date;
                        }
                        if (torihikisaki.TEKIYOU_BEGIN.IsNull && torihikisaki.TEKIYOU_END.IsNull)
                        {
                            this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (torihikisaki.TEKIYOU_BEGIN.IsNull && !torihikisaki.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(torihikisaki.TEKIYOU_END) <= 0)
                        {
                            this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!torihikisaki.TEKIYOU_BEGIN.IsNull && torihikisaki.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(torihikisaki.TEKIYOU_BEGIN) >= 0)
                        {
                            this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!torihikisaki.TEKIYOU_BEGIN.IsNull && !torihikisaki.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(torihikisaki.TEKIYOU_BEGIN) >= 0
                                && tekiyouDate.CompareTo(torihikisaki.TEKIYOU_END) <= 0)
                        {
                            this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.isError = true;
                            this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                            messageLogic.MessageBoxShow("E020", "取引先");
                            this.form.TORIHIKISAKI_CD.Focus();
                            return ret;
                        }
                    }

                    if (KeshikomiUmuCheck())
                    {
                        // 変更前の取引先CDの入金消込データが存在する場合
                        this.form.TORIHIKISAKI_NAME.Text = this.form.beforeTorihikisakiName;
                        messageLogic.MessageBoxShow("E233", "取引先");
                        this.form.TORIHIKISAKI_CD.Text = this.form.beforeTorihikisakiCd;
                        this.form.TORIHIKISAKI_CD.Focus();
                        return ret;
                    }

                    var seikyuu = this.torihikisakiSeikyuuDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    if (seikyuu == null || seikyuu.TORIHIKI_KBN_CD.IsNull || seikyuu.TORIHIKI_KBN_CD.Value != 2)
                    {
                        this.form.TORIHIKISAKI_NAME.Text = "";
                        messageLogic.MessageBoxShow("E226");
                        this.form.TORIHIKISAKI_CD.Focus();
                        this.form.isError = true;
                        return ret;
                    }

                    this.form.NYUUKINSAKI_CD.Text = seikyuu.NYUUKINSAKI_CD;
                    var nyuukinsaki = this.nyuukinsakiDao.GetDataByCd(seikyuu.NYUUKINSAKI_CD);
                    if (nyuukinsaki != null)
                    {
                        this.form.NYUUKINSAKI_NAME.Text = nyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                    }
                    this.form.beforeTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("TORIHIKISAKI_CD_Validated", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKISAKI_CD_Validated", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 取引先CDに関連する情報をセット

        /// <summary>
        /// 取引先CDに関連する情報をセット
        /// </summary>
        internal void SetTorihikisaki()
        {
            if (this.form.beforeTorihikisakiCd != this.form.TORIHIKISAKI_CD.Text)
            {
                if (KeshikomiUmuCheck())
                {
                    this.form.TORIHIKISAKI_NAME.Text = this.form.beforeTorihikisakiName;
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E233", "取引先");
                    this.form.TORIHIKISAKI_CD.Text = this.form.beforeTorihikisakiCd;
                    this.form.TORIHIKISAKI_CD.Focus();
                    return;
                }

                this.form.NYUUKINSAKI_CD.Text = "";
                this.form.NYUUKINSAKI_NAME.Text = "";
                this.form.KESHIKOMI_Ichiran.Rows.Clear();
                var seikyuu = this.torihikisakiSeikyuuDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                if (seikyuu == null)
                {
                    this.form.TORIHIKISAKI_CD.Focus();
                    return;
                }

                this.form.NYUUKINSAKI_CD.Text = seikyuu.NYUUKINSAKI_CD;
                var nyuukinsaki = this.nyuukinsakiDao.GetDataByCd(seikyuu.NYUUKINSAKI_CD);
                if (nyuukinsaki != null)
                {
                    this.form.NYUUKINSAKI_NAME.Text = nyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                }
            }
            this.form.TORIHIKISAKI_CD.Focus();
            return;
        }

        #endregion

        #region 入金消込データの有無チェック

        private bool KeshikomiUmuCheck()
        {
            bool retVal = false;

            T_NYUUKIN_ENTRY entry = new T_NYUUKIN_ENTRY();
            entry.NYUUKIN_NUMBER = this.ConvertToSqlInt64(this.form.NYUUKIN_NUMBER.Text);
            if (!string.IsNullOrEmpty(this.form.beforeTorihikisakiCd))
            {
                entry.TORIHIKISAKI_CD = this.form.beforeTorihikisakiCd;
            }
            else
            {
                entry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.beforeDenpyouDate))
            {
                entry.DENPYOU_DATE = Convert.ToDateTime(this.form.beforeDenpyouDate);
            }
            entry.DELETE_FLG = false;
            entry = this.entryDao.GetNyuukinEntry(entry);
            if (entry != null && this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                T_NYUUKIN_KESHIKOMI data = new T_NYUUKIN_KESHIKOMI();
                data.SYSTEM_ID = entry.SYSTEM_ID;
                data.NYUUKIN_NUMBER = this.ConvertToSqlInt64(this.form.NYUUKIN_NUMBER.Text);
                data.DELETE_FLG = false;
                T_NYUUKIN_KESHIKOMI[] datas = this.keshikomiDao.GetKeshikomi(data);
                if (datas != null && datas.Length > 0)
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        #endregion

        #region 入金明細一覧の描画

        /// <summary>
        /// 入金明細一覧の描画時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal bool DETAIL_Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            bool ret = true;
            try
            {
                var dgv = (DataGridView)sender;

                if (e.RowIndex == -1)
                {
                    var columnName = dgv.Columns[e.ColumnIndex].Name;
                    if ("NYUUSHUKKIN_KBN_CD" == columnName || "NYUUSHUKKIN_KBN_NAME_RYAKU" == columnName)
                    {
                        var rect = e.CellBounds;

                        if ("NYUUSHUKKIN_KBN_CD" == columnName)
                        {
                            rect.Width += dgv.Columns["NYUUSHUKKIN_KBN_NAME_RYAKU"].Width;
                            rect.Y = e.CellBounds.Y + 1;
                        }
                        else
                        {
                            rect.Width += dgv.Columns["NYUUSHUKKIN_KBN_CD"].Width;
                            rect.Y = e.CellBounds.Y + 1;

                            rect.X -= dgv.Columns["NYUUSHUKKIN_KBN_CD"].Width;
                        }

                        using (var brush = new SolidBrush(dgv.ColumnHeadersDefaultCellStyle.BackColor))
                        {
                            e.Graphics.FillRectangle(brush, rect);
                        }
                        using (var pen = new Pen(dgv.GridColor))
                        {
                            e.Graphics.DrawRectangle(pen, rect);
                        }
                        using (var pen = new Pen(Color.DarkGray))
                        {
                            e.Graphics.DrawLine(pen, rect.X, rect.Y - 1, rect.X + rect.Width, rect.Y - 1);
                            e.Graphics.DrawLine(pen, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                        }

                        TextRenderer.DrawText(e.Graphics, "入金区分", e.CellStyle.Font, rect, e.CellStyle.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
                    }
                    else
                    {
                        e.Paint(e.ClipBounds, e.PaintParts);
                    }

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DETAIL_Ichiran_CellPainting", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 入金明細一覧CellEnter

        /// <summary>
        /// 入金明細一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DETAIL_Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string columnName = this.form.DETAIL_Ichiran.Columns[e.ColumnIndex].Name;
                switch (columnName)
                {
                    case "NYUUSHUKKIN_KBN_CD":
                        if (!isNyuukinKbnInputError)
                        {
                            this.beforeNyuukinKbn = Convert.ToString(this.form.DETAIL_Ichiran.CurrentRow.Cells["NYUUSHUKKIN_KBN_CD"].Value);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DETAIL_Ichiran_CellEnter", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }
        }

        #endregion

        #region 入金明細一覧CellValidating

        /// <summary>
        /// 入金消込一覧のバリデート実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal bool DETAIL_Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                var columnName = this.form.DETAIL_Ichiran.Columns[e.ColumnIndex].Name;

                switch (columnName)
                {
                    case "NYUUSHUKKIN_KBN_CD":
                        isNyuukinKbnInputError = false;
                        string nyuushukkinKbnCd = Convert.ToString(this.form.DETAIL_Ichiran.CurrentRow.Cells["NYUUSHUKKIN_KBN_CD"].Value);
                        if (nyuushukkinKbnCd == "51")
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E042", "「51 仮受金」以外");
                            e.Cancel = true;
                        }
                        else if (!string.IsNullOrEmpty(nyuushukkinKbnCd))
                        {
                            // CellEnterで取得した前回値と値が変わっている場合のみ処理
                            if (!nyuushukkinKbnCd.Equals(beforeNyuukinKbn))
                            {
                                short cd = 0;
                                if (short.TryParse(nyuushukkinKbnCd, out cd))
                                {
                                    // 入出金区分マスタ検索：検索結果はMAXで1件
                                    M_NYUUSHUKKIN_KBN searchEntity = new M_NYUUSHUKKIN_KBN();
                                    searchEntity.NYUUSHUKKIN_KBN_CD = cd;
                                    M_NYUUSHUKKIN_KBN[] resultEntitys = this.nyuushukkinKbnDao.GetAllValidData(searchEntity);

                                    if (resultEntitys == null || resultEntitys.Length < 1)
                                    {
                                        // 存在しないデータの場合はエラー)
                                        new MessageBoxShowLogic().MessageBoxShow("E020", "入出金区分");
                                        this.form.DETAIL_Ichiran.Rows[e.RowIndex].Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = null;
                                        isNyuukinKbnInputError = true;
                                        e.Cancel = true;
                                    }
                                    else
                                    {
                                        // 存在するデータの場合は名称設定
                                        this.form.DETAIL_Ichiran.Rows[e.RowIndex].Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = resultEntitys[0].NYUUSHUKKIN_KBN_NAME_RYAKU;
                                    }
                                }
                                else
                                {
                                    // コントロールに不適切な値が入った場合は名称クリア(コントロールの入力制限上ここにくることはありえない)
                                    new MessageBoxShowLogic().MessageBoxShow("E058");
                                    this.form.DETAIL_Ichiran.Rows[e.RowIndex].Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = null;
                                    isNyuukinKbnInputError = true;
                                    e.Cancel = true;
                                }
                            }
                        }
                        else
                        {
                            // 値が無い場合は名称クリア
                            this.form.DETAIL_Ichiran.Rows[e.RowIndex].Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = null;
                        }
                        break;

                    default:
                        break;
                }
                if (!this.CalcAll())
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DETAIL_Ichiran_CellValidating", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 入金消込一覧CellValidating

        /// <summary>
        /// 入金消込一覧のバリデート実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal bool KESHIKOMI_Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                var columnName = this.form.KESHIKOMI_Ichiran.Columns[e.ColumnIndex].Name;

                switch (columnName)
                {
                    case "KESHIKOMI_KINGAKU":
                        decimal keshikomi = 0;
                        decimal total = 0;
                        decimal seikyuu = 0;
                        string keshikomi_value = Convert.ToString(this.form.KESHIKOMI_Ichiran.CurrentRow.Cells["KESHIKOMI_KINGAKU"].Value);
                        string zenkai_value = Convert.ToString(this.form.KESHIKOMI_Ichiran.CurrentRow.Cells["KESHIKOMI_KINGAKU_ZEN"].Value);
                        string seikyuu_value = Convert.ToString(this.form.KESHIKOMI_Ichiran.CurrentRow.Cells["SEIKYUU_KINGAKU"].Value);
                        if (!string.IsNullOrEmpty(keshikomi_value))
                        {
                            keshikomi = Convert.ToDecimal(keshikomi_value.Replace(",", ""));
                        }
                        if (!string.IsNullOrEmpty(zenkai_value))
                        {
                            total = Convert.ToDecimal(zenkai_value.Replace(",", "")) + keshikomi;
                        }
                        if (!string.IsNullOrEmpty(seikyuu_value))
                        {
                            seikyuu = Convert.ToDecimal(seikyuu_value.Replace(",", ""));
                        }

                        if (seikyuu.CompareTo(0) > 0)
                        {
                            //if (total.CompareTo(seikyuu) > 0)
                            //{
                            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            //    msgLogic.MessageBoxShow("E227");
                            //    e.Cancel = true;
                            //    return;
                            //}
                            if (keshikomi.CompareTo(0) < 0)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("C103", "0円以下");
                            }
                        }

                        if (seikyuu.CompareTo(0) < 0)
                        {
                            //if (total.CompareTo(seikyuu) < 0)
                            //{
                            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            //    msgLogic.MessageBoxShow("E227");
                            //    e.Cancel = true;
                            //    return;
                            //}
                            if (keshikomi.CompareTo(0) > 0)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("C103", "0円以上");
                            }
                        }
                        this.form.KESHIKOMI_Ichiran.CurrentRow.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = total;
                        this.form.KESHIKOMI_Ichiran.CurrentRow.Cells["MIKESHIKOMI_KINGAKU"].Value = seikyuu - total;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("KESHIKOMI_Ichiran_CellValidating", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region コントロールの状態変更

        /// <summary>
        /// 画面上のコントロールの状態を変更します
        /// </summary>
        internal void ChangeControlState()
        {
            LogUtility.DebugMethodStart();

            var readOnly = false;
            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    readOnly = false;
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                default:
                    readOnly = true;
                    break;
            }

            this.headerForm.txtKyotenCd.ReadOnly = readOnly;
            this.form.DENPYOU_DATE.ReadOnly = readOnly;
            this.form.DENPYOU_DATE.BackColor = SystemColors.Window;
            this.form.TORIHIKISAKI_CD.ReadOnly = readOnly;
            this.form.TORIHIKISAKI_POPUP.Enabled = !readOnly;
            this.form.BANK_CD.ReadOnly = readOnly;
            this.form.BANK_SHITEN_CD.ReadOnly = readOnly;
            foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
            {
                row.ReadOnly = readOnly;
                row.Cells.OfType<DgvCustomTextBoxCell>().ToList().ForEach(c => c.UpdateBackColor());
            }
            foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
            {
                row.ReadOnly = readOnly;
                row.Cells.OfType<DgvCustomTextBoxCell>().ToList().ForEach(c => c.UpdateBackColor());
            }

            if (!readOnly && this.form.DETAIL_Ichiran.Rows.Count < this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value)
            {
                this.form.DETAIL_Ichiran.AllowUserToAddRows = true;
            }

            // 先頭の列までスクロールする
            this.form.KESHIKOMI_Ichiran.FirstDisplayedScrollingColumnIndex = this.form.KESHIKOMI_Ichiran.Columns["DELETE_FLG"].Index;
            this.form.DETAIL_Ichiran.FirstDisplayedScrollingColumnIndex = this.form.DETAIL_Ichiran.Columns["NYUUSHUKKIN_KBN_CD"].Index;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタンの活性状態変更

        /// <summary>
        /// ボタンの活性状態を変更します
        /// </summary>
        internal void ChangeButtonState()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    parentForm.bt_func2.Enabled = true;
                    parentForm.bt_func3.Enabled = true;
                    parentForm.bt_func5.Enabled = true;
                    parentForm.bt_func6.Enabled = true;
                    parentForm.bt_func7.Enabled = true;
                    parentForm.bt_func8.Enabled = true;
                    parentForm.bt_func9.Enabled = true;
                    //parentForm.bt_func10.Enabled = true;
                    parentForm.bt_func12.Enabled = true;
                    parentForm.bt_process1.Enabled = true;
                    parentForm.bt_process2.Enabled = true;
                    parentForm.bt_process3.Enabled = true;
                    parentForm.bt_process4.Enabled = true;
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    parentForm.bt_func2.Enabled = false;
                    parentForm.bt_func3.Enabled = false;
                    parentForm.bt_func8.Enabled = false;
                    //parentForm.bt_func10.Enabled = false;
                    parentForm.bt_process1.Enabled = false;
                    parentForm.bt_process2.Enabled = false;
                    parentForm.bt_process3.Enabled = false;
                    parentForm.bt_process4.Enabled = false;

                    parentForm.bt_func5.Enabled = true;
                    parentForm.bt_func6.Enabled = true;
                    parentForm.bt_func7.Enabled = true;
                    parentForm.bt_func9.Enabled = true;
                    parentForm.bt_func12.Enabled = true;
                    break;

                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    parentForm.bt_func3.Enabled = false;
                    //parentForm.bt_func5.Enabled = false;
                    parentForm.bt_func9.Enabled = false;
                    //parentForm.bt_func10.Enabled = false;
                    parentForm.bt_process1.Enabled = false;
                    parentForm.bt_process2.Enabled = false;
                    parentForm.bt_process3.Enabled = false;
                    parentForm.bt_process4.Enabled = false;

                    parentForm.bt_func2.Enabled = true;
                    parentForm.bt_func6.Enabled = true;
                    parentForm.bt_func7.Enabled = true;
                    parentForm.bt_func8.Enabled = true;
                    parentForm.bt_func12.Enabled = true;
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 銀行・銀行支店設定

        /// <summary>
        /// 銀行・銀行支店を設定します
        /// </summary>
        internal bool SetBankCheck()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 「2:振込」の行が存在する場合は、「銀行CD」「銀行支店CD」の入力を必須とする
                var count = this.form.DETAIL_Ichiran.Rows.Cast<DataGridViewRow>()
                                                                .Where(r => !r.IsNewRow && ConstInfo.NYUUKIN_KBN_FURIKOMI == Convert.ToString(r.Cells["NYUUSHUKKIN_KBN_CD"].Value))
                                                                .Count();

                var bank = this.form.BANK_CD.RegistCheckMethod.Where(r => r.CheckMethodName == "必須チェック").FirstOrDefault();
                var bankShiten = this.form.BANK_SHITEN_CD.RegistCheckMethod.Where(r => r.CheckMethodName == "必須チェック").FirstOrDefault();

                if (count > 0)
                {
                    if (null == bank)
                    {
                        this.form.BANK_CD.RegistCheckMethod.Add(this.form.bankCdRegistCheckMethod);
                    }
                    if (null == bankShiten)
                    {
                        this.form.BANK_SHITEN_CD.RegistCheckMethod.Add(this.form.bankShitenCdRegistCheckMethod);
                    }
                }
                else
                {
                    if (null != bank)
                    {
                        this.form.bankCdRegistCheckMethod = bank;
                        this.form.BANK_CD.RegistCheckMethod.Clear();
                    }
                    if (null != bankShiten)
                    {
                        this.form.bankShitenCdRegistCheckMethod = bankShiten;
                        this.form.BANK_SHITEN_CD.RegistCheckMethod.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetBankCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region キャッシャ連動

        /// <summary>
        /// キャッシャ情報送信
        /// </summary>
        /// <param name="entity">入金一括伝票</param>
        /// <param name="detailList">入金一括明細List</param>
        private void sendCasher(T_NYUUKIN_SUM_ENTRY entity, List<T_NYUUKIN_SUM_DETAIL> detailList)
        {
            // 入金額合計算出※現金のもののみ
            decimal kingaku = 0;
            foreach (var detail in detailList)
            {
                // 入出金区分が「現金」の明細金額を積算
                if (detail.NYUUSHUKKIN_KBN_CD == CommonConst.NYUUSHUKKIN_KBN_GENKIN)
                {
                    kingaku += detail.KINGAKU.Value;
                }
            }

            // 差引０円の場合はキャッシャ情報の送信を行わない
            if (kingaku != 0)
            {
                // キャッシャ用DTO生成
                var casherDto = new CasherDTOClass();
                casherDto.DENPYOU_DATE = entity.DENPYOU_DATE.Value;
                casherDto.NYUURYOKU_TANTOUSHA_CD = SystemProperty.Shain.CD;
                casherDto.DENPYOU_NUMBER = entity.NYUUKIN_NUMBER.Value;
                casherDto.KINGAKU = kingaku;
                casherDto.BIKOU = (string.IsNullOrEmpty(entity.DENPYOU_BIKOU) ? string.Empty : entity.DENPYOU_BIKOU);
                casherDto.DENSHU_KBN_CD = CommonConst.DENSHU_KBN_NYUUKIN;
                casherDto.KYOTEN_CD = entity.KYOTEN_CD.Value;

                // キャッシャ共通処理に情報セット
                var casherAccessor = new CasherAccessor();
                casherAccessor.setCasherData(casherDto);
            }
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        #endregion キャッシャ連動

        //thongh 2015/08/07 #12106 start

        /// <summary>
        /// 指定されたオブジェクトをカンマ区切りの文字列にフォーマットします
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>返還後の文字列</returns>
        internal String FormatKingaku(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = String.Empty;

            ret = this.ConvertToDecimal(value).ToString("#,##0");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlBoolean に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト（変換するオブジェクトが null の場合は False）</returns>
        internal SqlBoolean ConvertToSqlBooleanDefaultFalse(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = SqlBoolean.False;
            if (false == this.IsNullOrEmpty(value))
            {
                ret = SqlBoolean.Parse(value.ToString());
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        //thongh 2015/08/07 #12106 end

        #endregion

        /// <summary>
        /// 160013
        /// </summary>
        private void InitBySeikyuuDto()
        {
            if (this.form.SeikyuuDto != null)
            {
                //取引先
                var torihikisakiEntity = this.torihikisakiDao.GetDataByCd(this.form.SeikyuuDto.TorihikisakiCd);
                if (null != torihikisakiEntity)
                {
                    this.form.TORIHIKISAKI_CD.Text = torihikisakiEntity.TORIHIKISAKI_CD;
                    this.form.TORIHIKISAKI_NAME.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                    var seikyuu = this.torihikisakiSeikyuuDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    if (seikyuu != null)
                    {
                        this.form.NYUUKINSAKI_CD.Text = seikyuu.NYUUKINSAKI_CD;
                        var nyuukinsaki = this.nyuukinsakiDao.GetDataByCd(seikyuu.NYUUKINSAKI_CD);
                        if (nyuukinsaki != null)
                        {
                            this.form.NYUUKINSAKI_NAME.Text = nyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                        }
                    }
                }
                //伝票日付
                this.form.DENPYOU_DATE.Value = this.form.SeikyuuDto.SeikyuuDate;
                //キャッシャ連動
                if (this.form.SeikyuuDto.NyuushukinCd == CommonConst.NYUUSHUKKIN_KBN_GENKIN)
                {
                    this.form.CASHER_LINK_KBN.Text = CommonConst.CASHER_LINK_KBN_USE;
                }
                else
                {
                    this.form.CASHER_LINK_KBN.Text = CommonConst.CASHER_LINK_KBN_UNUSED;
                }
                //銀行
                if (this.form.SeikyuuDto.NyuushukinCd == CommonConst.NYUUSHUKKIN_KBN_GENKIN)
                {
                    this.form.BANK_CD.Text = string.Empty;
                    this.form.BANK_NAME_RYAKU.Text = string.Empty;
                    this.form.BANK_SHITEN_CD.Text = string.Empty;
                    this.form.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;
                    this.form.KOUZA_SHURUI.Text = string.Empty;
                    this.form.KOUZA_NO.Text = string.Empty;
                    this.form.KOUZA_NAME.Text = string.Empty;
                }
                else
                {
                    bool setBankFlg = true;
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.SET_BANK_CD))
                    {
                        // 銀行
                        var bank = new M_BANK();
                        bank.BANK_CD = Properties.Settings.Default.SET_BANK_CD;
                        bank = this.bankDao.GetAllValidData(bank).FirstOrDefault();
                        if (bank != null)
                        {
                            setBankFlg = false;
                        }
                    }
                    if (setBankFlg)
                    {
                        M_CORP_INFO corpInfo = this.corpInfoDao.GetAllDataMinCols().FirstOrDefault();
                        if (null != corpInfo)
                        {
                            this.form.BANK_CD.Text = corpInfo.BANK_CD;
                            this.form.BANK_NAME_RYAKU.Text = string.Empty;
                            this.form.BANK_SHITEN_CD.Text = corpInfo.BANK_SHITEN_CD;
                            this.form.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;

                            if (!string.IsNullOrEmpty(corpInfo.BANK_CD))
                            {
                                var bank = this.bankDao.GetDataByCd(corpInfo.BANK_CD);
                                if (null != bank)
                                {
                                    this.form.BANK_NAME_RYAKU.Text = bank.BANK_NAME_RYAKU;
                                }
                            }
                            if (!string.IsNullOrEmpty(corpInfo.BANK_CD) && !string.IsNullOrEmpty(corpInfo.BANK_SHITEN_CD) && !string.IsNullOrEmpty(corpInfo.KOUZA_NO))
                            {
                                var bankShiten = new M_BANK_SHITEN();
                                bankShiten.BANK_CD = corpInfo.BANK_CD;
                                bankShiten.BANK_SHITEN_CD = corpInfo.BANK_SHITEN_CD;
                                bankShiten.KOUZA_NO = corpInfo.KOUZA_NO;
                                bankShiten = this.bankShitenDao.GetDataByCd(bankShiten);

                                if (null != bankShiten)
                                {
                                    this.form.BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                                    this.form.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                                    this.form.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                                    this.form.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;
                                }
                            }
                        }
                    }
                }
                //請求書データ検索 
                this.parentForm.bt_func8.PerformClick();
                //入金額&消込額
                if (this.form.KESHIKOMI_Ichiran.Rows.Count > 0)
                {
                    decimal nyuukingaku = 0;
                    foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                    {
                        if (this.form.SeikyuuDto.SeikyuuNumbers.Contains((Int64)row.Cells["SEIKYUU_NUMBER"].Value))
                        {
                            nyuukingaku += this.ConvertToDecimal(row.Cells["MIKESHIKOMI_KINGAKU"].Value);
                            row.Cells["KESHIKOMI_KINGAKU"].Value = row.Cells["MIKESHIKOMI_KINGAKU"].Value;
                            row.Cells["MIKESHIKOMI_KINGAKU"].Value = 0;
                            row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value) + this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU"].Value);
                        }
                    }
                    var nyuukinIndex = this.form.DETAIL_Ichiran.Rows.Add();
                    var nyuukinRow = this.form.DETAIL_Ichiran.Rows[nyuukinIndex];
                    var nyuushukkinEntity = this.nyuushukkinKbnDao.GetDataByCd(this.form.SeikyuuDto.NyuushukinCd);
                    if (nyuushukkinEntity != null)
                    {
                        nyuukinRow.Cells["NYUUSHUKKIN_KBN_CD"].Value = nyuushukkinEntity.NYUUSHUKKIN_KBN_CD;
                        nyuukinRow.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = nyuushukkinEntity.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        nyuukinRow.Cells["KINGAKU"].Value = nyuukingaku;
                    }
                    this.CalcAll();
                }
                //DTOをクリアする 
                this.form.SeikyuuDto = null;
            }
        }
    }
}