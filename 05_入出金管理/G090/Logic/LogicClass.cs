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

namespace Shougun.Core.ReceiptPayManagement.Syukinnyuryoku
{
    /// <summary>
    /// G090 出金入力 ビジネスロジック
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
        /// 読み込んだ伝票の出金番号
        /// </summary>
        internal SqlInt64 ShukkinNumber;

        /// <summary>
        /// 読み込んだ伝票のSEQ
        /// </summary>
        internal SqlInt32 seq;

        /// <summary>
        /// 出金額合計
        /// </summary>
        internal decimal ShukkinTotal;

        /// <summary>
        /// 消込額合計
        /// </summary>
        internal decimal keshikomiTotal;

        /// <summary>
        /// 出金―消込差額
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
        internal IM_NYUUSHUKKIN_KBNDao nyuuShukkinKbnDao;

        /// <summary>
        /// 出金先Dao
        /// </summary>
        internal IM_SYUKKINSAKIDao ShukkinsakiDao;

        /// <summary>
        /// 取引先Dao
        /// </summary>
        internal IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// 取引先支払Dao
        /// </summary>
        internal IM_TORIHIKISAKI_SHIHARAIDao torihikisakiShiharaiDao;

        /// <summary>
        /// 出金Dao
        /// </summary>
        internal IT_SHUKKIN_ENTRYDao entryDao;

        /// <summary>
        /// 出金明細Dao
        /// </summary>
        internal IT_SHUKKIN_DETAILDao detailDao;

        /// <summary>
        /// 出金消込Dao
        /// </summary>
        internal IT_SHUKKIN_KESHIKOMIDao keshikomiDao;

        /// <summary>
        /// 締処理中Dao
        /// </summary>
        internal IT_SHIME_SHORI_CHUUDao shimeiShoriChuuDao;

        /// <summary>
        /// 精算伝票Dao
        /// </summary>
        internal IT_SEISAN_DENPYOUDao seisanDenpyouDao;

        /// <summary>
        /// 精算明細Dao
        /// </summary>
        internal IT_SEISAN_DETAILDao seisanDetailDao;

        /// <summary>
        /// 前回値 - 出金区分
        /// </summary>
        private string beforeShukkinKbn = string.Empty;

        /// <summary>
        /// 出金区分の入力でエラーが発生中かのフラグ
        /// </summary>
        private bool isShukkinKbnInputError = false;

        #endregion

        #region プロパティ

        /// <summary>
        /// システム設定エンティティを取得・設定します
        /// </summary>
        internal M_SYS_INFO SysInfo { get; private set; }

        /// <summary>
        /// 読み込んだ伝票の出金先CDを取得・設定します
        /// </summary>
        internal string ShukkinsakiCd { get; set; }

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
            this.nyuuShukkinKbnDao = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();
            this.ShukkinsakiDao = DaoInitUtility.GetComponent<IM_SYUKKINSAKIDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torihikisakiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.entryDao = DaoInitUtility.GetComponent<IT_SHUKKIN_ENTRYDao>();
            this.detailDao = DaoInitUtility.GetComponent<IT_SHUKKIN_DETAILDao>();
            this.keshikomiDao = DaoInitUtility.GetComponent<IT_SHUKKIN_KESHIKOMIDao>();
            this.shimeiShoriChuuDao = DaoInitUtility.GetComponent<IT_SHIME_SHORI_CHUUDao>();
            this.seisanDenpyouDao = DaoInitUtility.GetComponent<IT_SEISAN_DENPYOUDao>();
            this.seisanDetailDao = DaoInitUtility.GetComponent<IT_SEISAN_DETAILDao>();

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

                this.form.TORIHIKISAKI_CD.Text = this.form.MoveDataShukkinsakiCd;
                var torihikisaki = this.torihikisakiDao.GetDataByCd(this.form.MoveDataShukkinsakiCd);
                if (null != torihikisaki)
                {
                    this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    var seikyuu = this.torihikisakiShiharaiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    if (seikyuu == null || seikyuu.TORIHIKI_KBN_CD.IsNull || seikyuu.TORIHIKI_KBN_CD.Value != 2)
                    {
                        this.form.MoveDataShukkinsakiCd = null;
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E226");
                        if (!WindowInit(true))
                        {
                            ret = false;
                        }
                        return ret;
                    }

                    this.form.SHUKKINSAKI_CD.Text = seikyuu.SYUUKINSAKI_CD;
                    var nyuukinsaki = this.ShukkinsakiDao.GetDataByCd(seikyuu.SYUUKINSAKI_CD);
                    if (nyuukinsaki != null)
                    {
                        this.form.SHUKKINSAKI_NAME.Text = nyuukinsaki.SYUKKINSAKI_NAME_RYAKU;
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
                        this.SetShukkinData();
                        break;
                }

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.form.SHUKKIN_NUMBER.ReadOnly = false;
                        this.form.SHUKKIN_NUMBER.TabStop = true;
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.form.SHUKKIN_NUMBER.ReadOnly = true;
                        this.form.SHUKKIN_NUMBER.TabStop = false;
                        break;
                }

                this.ButtonInit();
                this.EventInit();
                #region 伝票精算⇒出金入力
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.InitBySeisanDto();
                        break;
                }
                #endregion
                var isError = false;

                // 締処理中チェック
                // 修正モードで修正権限あり or 削除モードで削除権限あり 時のみチェック
                if (!isError && ((WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && this.AuthCanUpdate) || (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType && this.AuthCanDelete)))
                {
                    if (this.dto.ShukkinEntry != null)
                    {
                        var shimeShoriChuuList = this.shimeiShoriChuuDao.GetShimeShoriChuuList(this.dto.ShukkinEntry.DENPYOU_DATE.Value, this.dto.ShukkinEntry.TORIHIKISAKI_CD);
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

                // 精算・精算締済チェック
                // 修正モードで修正権限あり or 削除モードで削除権限あり 時のみチェック
                if (!isError && ((WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && this.AuthCanUpdate) || (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType && this.AuthCanDelete)))
                {
                    var torihikisakiCdList = new List<string>();
                    var seikyuuDetailList = this.seisanDetailDao.GetDataByShukkinNumber(this.dto.ShukkinEntry.SHUKKIN_NUMBER);
                    if (seikyuuDetailList.Count() > 0)
                    {
                        var seikyuuTorihikisakiList = seikyuuDetailList.Select(s => s.TORIHIKISAKI_CD).Distinct();
                        var seikyuuDetail = seikyuuDetailList.Where(s => s.TORIHIKISAKI_CD == this.form.TORIHIKISAKI_CD.Text.ToString()).FirstOrDefault();
                        if (null != seikyuuDetail)
                        {
                            torihikisakiCdList.Add(seikyuuDetail.TORIHIKISAKI_CD);
                        }

                        // 取引先が1件でも締処理されている場合は、出金伝票を編集できない
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
                    if (this.dto.ShukkinEntry.SEISAN_SOUSAI_CREATE_KBN.IsTrue)
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
                //        this.form.SHUKKIN_NUMBER.Focus();
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
        /// 
        /// </summary>
        private void InitBySeisanDto()
        {
            if (this.form.ShiharaiDto != null)
            {
                //取引先
                var torihikisakiEntity = this.torihikisakiDao.GetDataByCd(this.form.ShiharaiDto.TorihikisakiCd);
                if (null != torihikisakiEntity)
                {
                    this.form.TORIHIKISAKI_CD.Text = torihikisakiEntity.TORIHIKISAKI_CD;
                    this.form.TORIHIKISAKI_NAME.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                    var seikyuu = this.torihikisakiShiharaiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    if (seikyuu != null)
                    {
                        this.form.SHUKKINSAKI_CD.Text = seikyuu.SYUUKINSAKI_CD;
                        var nyuukinsaki = this.ShukkinsakiDao.GetDataByCd(seikyuu.SYUUKINSAKI_CD);
                        if (nyuukinsaki != null)
                        {
                            this.form.SHUKKINSAKI_NAME.Text = nyuukinsaki.SYUKKINSAKI_NAME_RYAKU;
                        }
                    }
                }
                //伝票日付
                this.form.DENPYOU_DATE.Value = this.form.ShiharaiDto.SeisanDate;
                //キャッシャ連動
                if (this.form.ShiharaiDto.NyuushukinCd == CommonConst.NYUUSHUKKIN_KBN_GENKIN)
                {
                    this.form.CASHER_LINK_KBN.Text = CommonConst.CASHER_LINK_KBN_USE;
                }
                else
                {
                    this.form.CASHER_LINK_KBN.Text = CommonConst.CASHER_LINK_KBN_UNUSED;
                }
                //精算書データ検索 
                this.parentForm.bt_func8.PerformClick();
                //出金額&消込額
                if (this.form.KESHIKOMI_Ichiran.Rows.Count > 0)
                {
                    decimal shukkingaku = 0;
                    foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                    {
                        if (this.form.ShiharaiDto.SeisanNumbers.Contains((Int64)row.Cells["SEISAN_NUMBER"].Value))
                        {
                            shukkingaku += this.ConvertToDecimal(row.Cells["MIKESHIKOMI_KINGAKU"].Value);
                            row.Cells["KESHIKOMI_KINGAKU"].Value = row.Cells["MIKESHIKOMI_KINGAKU"].Value;
                            row.Cells["MIKESHIKOMI_KINGAKU"].Value = 0;
                            row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value) + this.ConvertToDecimal(row.Cells["KESHIKOMI_KINGAKU"].Value);
                        }
                    }
                    var shukkinIndex = this.form.DETAIL_Ichiran.Rows.Add();
                    var shukkinRow = this.form.DETAIL_Ichiran.Rows[shukkinIndex];
                    var nyuushukkinEntity = this.nyuuShukkinKbnDao.GetDataByCd(this.form.ShiharaiDto.NyuushukinCd);
                    if (nyuushukkinEntity != null)
                    {
                        shukkinRow.Cells["NYUUSHUKKIN_KBN_CD"].Value = nyuushukkinEntity.NYUUSHUKKIN_KBN_CD;
                        shukkinRow.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = nyuushukkinEntity.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        shukkinRow.Cells["KINGAKU"].Value = shukkingaku;
                    }
                    this.CalcAll();
                }
                //DTOをクリアする 
                this.form.ShiharaiDto = null;
            }
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
            //振込出力ンイベント
            this.form.FURIKOMI_SHUTSURYOKU_KBN.TextChanged -= new EventHandler(FURIKOMI_SHUTSURYOKU_KBN_TextChanged);
            this.form.FURIKOMI_SHUTSURYOKU_KBN.TextChanged += new EventHandler(FURIKOMI_SHUTSURYOKU_KBN_TextChanged);
            //振込先口座種類ンイベント
            this.form.FURIKOMI_KOUZA_SHURUI_CD.Validating -= new System.ComponentModel.CancelEventHandler(FURIKOMI_KOUZA_SHURUI_CD_Validating);
            this.form.FURIKOMI_KOUZA_SHURUI_CD.Validating += new System.ComponentModel.CancelEventHandler(FURIKOMI_KOUZA_SHURUI_CD_Validating);
            
            this.form.FURIKOMI_BANK_NAME.Validating -= new System.ComponentModel.CancelEventHandler(FURIKOMI_BANK_NAME_Validating);
            this.form.FURIKOMI_BANK_NAME.Validating += new System.ComponentModel.CancelEventHandler(FURIKOMI_BANK_NAME_Validating);
            
            this.form.FURIKOMI_BANK_SHITEN_NAME.Validating -= new System.ComponentModel.CancelEventHandler(FURIKOMI_BANK_SHITEN_NAME_Validating);
            this.form.FURIKOMI_BANK_SHITEN_NAME.Validating += new System.ComponentModel.CancelEventHandler(FURIKOMI_BANK_SHITEN_NAME_Validating);
            
            this.form.FURIKOMI_KOUZA_NAME.Validating -= new System.ComponentModel.CancelEventHandler(FURIKOMI_KOUZA_NAME_Validating);
            this.form.FURIKOMI_KOUZA_NAME.Validating += new System.ComponentModel.CancelEventHandler(FURIKOMI_KOUZA_NAME_Validating);

            this.form.BANK_CD.Enter -= new EventHandler(BANK_CD_Enter);
            this.form.BANK_CD.Enter += new EventHandler(BANK_CD_Enter);
            
            this.form.BANK_CD.Validated -= new EventHandler(BANK_CD_Validated);
            this.form.BANK_CD.Validated += new EventHandler(BANK_CD_Validated);
            
            this.form.BANK_SHITEN_CD.Enter -= new EventHandler(BANK_SHITEN_CD_Enter);
            this.form.BANK_SHITEN_CD.Enter += new EventHandler(BANK_SHITEN_CD_Enter);
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
        /// 読み込んだ伝票の出金番号を設定します
        /// </summary>
        /// <param name="nyuukinNumber">出金番号</param>
        internal void SetShukkinNumber(SqlInt64 nyuukinNumber)
        {
            LogUtility.DebugMethodStart(nyuukinNumber);

            this.SetShukkinNumberAndSeq(nyuukinNumber, SqlInt32.Null);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 読み込んだ伝票の出金番号とSEQを設定します
        /// </summary>
        /// <param name="nyuukinNumber">出金番号</param>
        /// <param name="seq">SEQ</param>
        internal void SetShukkinNumberAndSeq(SqlInt64 nyuukinNumber, SqlInt32 seq)
        {
            LogUtility.DebugMethodStart(nyuukinNumber, seq);

            this.ShukkinNumber = nyuukinNumber;
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
            //LogUtility.DebugMethodStart(value);

            var ret = false;
            if (null == value)
            {
                ret = true;
            }
            else if (string.IsNullOrEmpty(value.ToString()))
            {
                ret = true;
            }

            //LogUtility.DebugMethodEnd(ret);

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
            //LogUtility.DebugMethodStart(value);

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

            //LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlDecimal に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlDecimal ConvertToSqlDecimal(object value)
        {
            //LogUtility.DebugMethodStart(value);

            var ret = SqlDecimal.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlDecimal.Parse(value.ToString().Replace(",", ""));
            }

            //LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlInt64 に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlInt64 ConvertToSqlInt64(object value)
        {
            //LogUtility.DebugMethodStart(value);

            var ret = SqlInt64.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlInt64.Parse(value.ToString());
            }

            //LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlInt32 に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlInt32 ConvertToSqlInt32(object value)
        {
            //LogUtility.DebugMethodStart(value);

            var ret = SqlInt32.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlInt32.Parse(value.ToString());
            }

            //LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlInt16 に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlInt16 ConvertToSqlInt16(object value)
        {
            //LogUtility.DebugMethodStart(value);

            var ret = SqlInt16.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlInt16.Parse(value.ToString());
            }

            //LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region データ取得メソッド

        /// <summary>
        /// 基準の出金番号より前で最大の出金番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の出金番号</param>
        /// <returns>出金番号</returns>
        internal SqlInt64 GetPrevShukkinNumber(SqlInt64 nyuukinNumber)
        {
            LogUtility.DebugMethodStart(nyuukinNumber);

            var ret = SqlInt64.Null;

            var number = string.Empty;
            if (!nyuukinNumber.IsNull)
            {
                number = nyuukinNumber.Value.ToString();
            }
            string kyotenCd = this.headerForm.txtKyotenCd.Text;
            var nyuukinEntry = this.entryDao.GetPrevShukkinNumber(number, kyotenCd);
            if (null != nyuukinEntry)
            {
                ret = nyuukinEntry.SHUKKIN_NUMBER;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 基準の出金番号より後で最小の出金番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の出金番号</param>
        /// <returns>出金番号</returns>
        internal SqlInt64 GetNextShukkinNumber(SqlInt64 nyuukinNumber)
        {
            LogUtility.DebugMethodStart(nyuukinNumber);

            var ret = SqlInt64.Null;

            var number = string.Empty;
            if (!nyuukinNumber.IsNull)
            {
                number = nyuukinNumber.Value.ToString();
            }
            string kyotenCd = this.headerForm.txtKyotenCd.Text;
            var nyuukinEntry = this.entryDao.GetNextShukkinNumber(number, kyotenCd);
            if (null != nyuukinEntry)
            {
                ret = nyuukinEntry.SHUKKIN_NUMBER;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 出金伝票を取得します
        /// </summary>
        /// <param name="nyuukinNumber">出金番号</param>
        /// <param name="seq">SEQ</param>
        internal void GetShukkinData(SqlInt64 nyuukinNumber, SqlInt32 seq)
        {
            LogUtility.DebugMethodStart(nyuukinNumber, seq);

            this.dto = new DTOClass();

            // 出金
            var entry = new T_SHUKKIN_ENTRY();
            entry.SHUKKIN_NUMBER = nyuukinNumber;
            if (!seq.IsNull)
            {
                entry.SEQ = seq;
            }
            else
            {
                entry.DELETE_FLG = false;
            }
            this.dto.ShukkinEntry = this.entryDao.GetShukkinEntry(entry);
            if (this.dto.ShukkinEntry == null)
            {
                return;
            }            

            // 出金明細
            var detail = new T_SHUKKIN_DETAIL();
            detail.SYSTEM_ID = this.dto.ShukkinEntry.SYSTEM_ID;
            detail.SEQ = this.dto.ShukkinEntry.SEQ;
            this.dto.ShukkinDetailList = this.detailDao.GetShukkinDetailList(detail);

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
                if (!string.IsNullOrEmpty(this.form.SHUKKIN_NUMBER.Text))
                {
                    nyuukinNumber = this.ConvertToSqlInt64(this.form.SHUKKIN_NUMBER.Text);
                }

                // 読み込む対象の出金番号を取得
                nyuukinNumber = this.GetPrevShukkinNumber(nyuukinNumber);

                if (nyuukinNumber.IsNull)
                {
                    //try to get maximun nyuukin number
                    nyuukinNumber = this.GetPrevShukkinNumber(nyuukinNumber);
                    //after try to get maximun nyuukin number, if nyuukin number is null then show message
                    if (nyuukinNumber.IsNull)
                    {
                        // 読み込む対象の出金番号を取得
                        //ThangNguyen [Update] 20150814 #11409 Start
                        //nyuukinNumber = this.GetPrevShukkinNumber(nyuukinNumber);
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E045");
                        return ret;
                        //ThangNguyen [Update] 20150814 #11409 End
                    }
                }

                var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());

                this.SetShukkinNumber(nyuukinNumber);

                if (nyuukinNumber.IsNull)
                {
                    if (!Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
                    {
                        return ret;
                    }

                    // 出金データがない
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

                    this.form.SHUKKIN_NUMBER.Text = nyuukinNumber.Value.ToString();
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
                if (!string.IsNullOrEmpty(this.form.SHUKKIN_NUMBER.Text))
                {
                    nyuukinNumber = this.ConvertToSqlInt64(this.form.SHUKKIN_NUMBER.Text);
                }

                // 読み込む対象の出金番号を取得
                nyuukinNumber = this.GetNextShukkinNumber(nyuukinNumber);

                if (nyuukinNumber.IsNull)
                {
                    //try to get minimum number if nyuukin number is maximum
                    nyuukinNumber = this.GetNextShukkinNumber(0);
                    //after try to get minimum number, if nyuukin number is null then show message
                    if (nyuukinNumber.IsNull)
                    {
                        // 読み込む対象の出金番号を取得
                        //ThangNguyen [Update] 20150814 #11409 Start
                        //nyuukinNumber = this.GetNextShukkinNumber(nyuukinNumber);
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E045");
                        return ret;
                        //ThangNguyen [Update] 20150814 #11409 End
                    }
                }

                var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());

                this.SetShukkinNumber(nyuukinNumber);

                if (nyuukinNumber.IsNull)
                {
                    if (!Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
                    {
                        return ret;
                    }

                    // 出金データがない
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

                    this.form.SHUKKIN_NUMBER.Text = nyuukinNumber.Value.ToString();
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

                T_SHUKKIN_ENTRY entry = new T_SHUKKIN_ENTRY();
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
                bool matchShukkinNumFlg = false;
                if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    select += " AND SHUKKIN_NUM = " + this.form.SHUKKIN_NUMBER.Text;
                    // 新規モード以外で、該当の出金番号に合致する出金消込が無い場合は、
                    // 入力された取引先が等しい出金消込を再表示
                    if (dt.Select(select).Length <= 0)
                    {
                        select = "1=1";
                        matchShukkinNumFlg = false;
                    }
                    else
                    {
                        matchShukkinNumFlg = true;
                    }
                }

                table = DispKeshikomiIchiran(dt, table, matchShukkinNumFlg, select);
                if (matchShukkinNumFlg)
                {
                    // 該当の出金番号以外の明細を追加
                    matchShukkinNumFlg = false;
                    select = "SHUKKIN_NUM <> " + this.form.SHUKKIN_NUMBER.Text + " OR SHUKKIN_NUM IS NULL";
                    table = DispKeshikomiIchiran(dt, table, matchShukkinNumFlg, select);
                }

                DataRow dr;
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    dr = table.Rows[i];
                    if (this.ConvertToDecimal(dr["SEISAN_KINGAKU"]).CompareTo(0) == 0)
                    {
                        table.Rows.Remove(dr);
                    }
                }
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    dr = table.Rows[i];
                    if (this.ConvertToDecimal(dr["MIKESHIKOMI_KINGAKU"]).CompareTo(0) == 0 && dr["SHUKKIN_NUM"].ToString() != this.form.SHUKKIN_NUMBER.Text)
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

                // 精算日付、精算番号、鑑番号で並べ替え
                DataRow[] DataRowsSort = table.Select("1=1", "SORT_SEISAN_DATE, SEISAN_NUMBER, KAGAMI_NUMBER, SEISAN_KINGAKU DESC");
                DataTable tableSort = table.Clone();

                string SEISAN_numOld = string.Empty;
                string kagami_numOld = string.Empty;
                string SEISAN_num = string.Empty;
                string kagami_num = string.Empty;
                foreach (DataRow row in DataRowsSort)
                {
                    DataRow newRow = tableSort.NewRow();
                    newRow["SYSTEM_ID"] = row["SYSTEM_ID"];
                    newRow["KESHIKOMI_SEQ"] = row["KESHIKOMI_SEQ"];
                    newRow["SORT_SEISAN_DATE"] = row["SORT_SEISAN_DATE"];
                    newRow["SHUKKIN_NUM"] = row["SHUKKIN_NUM"];
                    newRow["SEISAN_NUMBER"] = row["SEISAN_NUMBER"];
                    newRow["KAGAMI_NUMBER"] = row["KAGAMI_NUMBER"];
                    newRow["GYOUSHA_CD"] = row["GYOUSHA_CD"];
                    newRow["GYOUSHA_NAME"] = row["GYOUSHA_NAME"];
                    newRow["GENBA_CD"] = row["GENBA_CD"];
                    newRow["GENBA_NAME"] = row["GENBA_NAME"];
                    newRow["SEISAN_DATE"] = row["SEISAN_DATE"];
                    newRow["SEISAN_KINGAKU"] = row["SEISAN_KINGAKU"];
                    newRow["KESHIKOMI_KINGAKU"] = row["KESHIKOMI_KINGAKU"];
                    newRow["KESHIKOMI_BIKOU"] = row["KESHIKOMI_BIKOU"];
                    newRow["MIKESHIKOMI_KINGAKU"] = row["MIKESHIKOMI_KINGAKU"];
                    newRow["KESHIKOMI_KINGAKU_ZEN"] = row["KESHIKOMI_KINGAKU_ZEN"];
                    newRow["KESHIKOMI_KINGAKU_TOTAL"] = row["KESHIKOMI_KINGAKU_TOTAL"];

                    SEISAN_num = Convert.ToString(row["SEISAN_NUMBER"]);
                    kagami_num = Convert.ToString(row["KAGAMI_NUMBER"]);
                    if (SEISAN_numOld != SEISAN_num || kagami_numOld != kagami_num)
                    {
                        tableSort.Rows.Add(newRow);
                        SEISAN_numOld = SEISAN_num;
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
                        row.Cells["SORT_SEISAN_DATE"].Value = dr["SORT_SEISAN_DATE"];
                        row.Cells["SHUKKIN_NUM"].Value = dr["SHUKKIN_NUM"];
                        row.Cells["SEISAN_NUMBER"].Value = dr["SEISAN_NUMBER"];
                        row.Cells["KAGAMI_NUMBER"].Value = dr["KAGAMI_NUMBER"];
                        row.Cells["GYOUSHA_CD"].Value = dr["GYOUSHA_CD"];
                        row.Cells["GYOUSHA_NAME"].Value = dr["GYOUSHA_NAME"];
                        row.Cells["GENBA_CD"].Value = dr["GENBA_CD"];
                        row.Cells["GENBA_NAME"].Value = dr["GENBA_NAME"];
                        row.Cells["SEISAN_DATE"].Value = dr["SEISAN_DATE"];
                        if (DateTime.TryParse(Convert.ToString(row.Cells["SEISAN_DATE"].Value), out date))
                        {
                            switch (date.DayOfWeek)
                            {
                                case DayOfWeek.Sunday:
                                    row.Cells["SEISAN_DATE"].Value += "(日)";
                                    break;

                                case DayOfWeek.Monday:
                                    row.Cells["SEISAN_DATE"].Value += "(月)";
                                    break;

                                case DayOfWeek.Tuesday:
                                    row.Cells["SEISAN_DATE"].Value += "(火)";
                                    break;

                                case DayOfWeek.Wednesday:
                                    row.Cells["SEISAN_DATE"].Value += "(水)";
                                    break;

                                case DayOfWeek.Thursday:
                                    row.Cells["SEISAN_DATE"].Value += "(木)";
                                    break;

                                case DayOfWeek.Friday:
                                    row.Cells["SEISAN_DATE"].Value += "(金)";
                                    break;

                                case DayOfWeek.Saturday:
                                    row.Cells["SEISAN_DATE"].Value += "(土)";
                                    break;
                            }
                        }
                        row.Cells["SEISAN_KINGAKU"].Value = dr["SEISAN_KINGAKU"];
                        row.Cells["KESHIKOMI_KINGAKU"].Value = dr["KESHIKOMI_KINGAKU"];
                        row.Cells["MIKESHIKOMI_KINGAKU"].Value = dr["MIKESHIKOMI_KINGAKU"];
                        row.Cells["KESHIKOMI_BIKOU"].Value = dr["KESHIKOMI_BIKOU"];
                        row.Cells["KESHIKOMI_KINGAKU_ZEN"].Value = dr["KESHIKOMI_KINGAKU_ZEN"];
                        row.Cells["KESHIKOMI_KINGAKU_TOTAL"].Value = dr["KESHIKOMI_KINGAKU_TOTAL"];
                    }
                    this.form.searchDate = this.form.DENPYOU_DATE.Text;
                }
                //if (this.form.DETAIL_Ichiran.Rows.Count >= this.SysInfo.SHUKKIN_IKKATSU_KBN_DISP_SUU.Value
                //    && !this.form.DETAIL_Ichiran.Rows[this.form.DETAIL_Ichiran.Rows.Count - 1].IsNewRow)
                //{
                //    this.form.DETAIL_Ichiran.AllowUserToAddRows = false;
                //}
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
        /// 出金消込明細元データ編集
        /// </summary>
        /// <param name="dt">元データ</param>
        /// <param name="table">出力データ</param>
        /// <param name="matchShukkinNumFlg">出金番号が一致するものがあるか</param>
        /// <param name="select">検索条件</param>
        /// <returns>出金消込明細元データ</returns>
        private DataTable DispKeshikomiIchiran(DataTable dt, DataTable table, bool matchShukkinNumFlg, string select)
        {
            string SEISAN_num = "";
            string kagami_num = "";
            string format = "SEISAN_NUMBER = {0} AND KAGAMI_NUMBER = '{1}'";
            SqlInt64 nyuukinNum;
            decimal keshikomiGaku = 0;
            decimal keshikomiZen = 0;
            List<string> list = new List<string>();
            string key = string.Empty;

            foreach (DataRow row in dt.Select(select))
            {
                SEISAN_num = Convert.ToString(row["SEISAN_NUMBER"]);
                kagami_num = Convert.ToString(row["KAGAMI_NUMBER"]);

                key = SEISAN_num + "-" + kagami_num;

                if (list.Contains(key))
                {
                    continue;
                }

                list.Add(key);
                DataRow[] rows = dt.Select(string.Format(format, SEISAN_num, kagami_num));

                DataRow newRow = table.NewRow();
                keshikomiGaku = 0;
                keshikomiZen = 0;
                foreach (DataRow tmpRow in rows)
                {
                    nyuukinNum = this.ConvertToSqlInt64(tmpRow["SHUKKIN_NUM"]);
                    if (!nyuukinNum.IsNull && Convert.ToString(nyuukinNum.Value) != this.form.SHUKKIN_NUMBER.Text)
                    {
                        keshikomiZen += this.ConvertToDecimal(tmpRow["KESHIKOMI_KINGAKU"]);
                    }
                    else if (!nyuukinNum.IsNull && Convert.ToString(nyuukinNum.Value) == this.form.SHUKKIN_NUMBER.Text)
                    {
                        keshikomiGaku = this.ConvertToDecimal(tmpRow["KESHIKOMI_KINGAKU"]);
                    }
                }
                newRow["SYSTEM_ID"] = row["SYSTEM_ID"];
                newRow["KESHIKOMI_SEQ"] = row["KESHIKOMI_SEQ"];
                newRow["SORT_SEISAN_DATE"] = row["SORT_SEISAN_DATE"];
                newRow["SHUKKIN_NUM"] = row["SHUKKIN_NUM"];
                newRow["SEISAN_NUMBER"] = row["SEISAN_NUMBER"];
                newRow["KAGAMI_NUMBER"] = row["KAGAMI_NUMBER"];
                newRow["GYOUSHA_CD"] = row["GYOUSHA_CD"];
                newRow["GYOUSHA_NAME"] = row["GYOUSHA_NAME"];
                newRow["GENBA_CD"] = row["GENBA_CD"];
                newRow["GENBA_NAME"] = row["GENBA_NAME"];
                newRow["SEISAN_DATE"] = row["SEISAN_DATE"];
                newRow["SEISAN_KINGAKU"] = row["SEISAN_KINGAKU"];
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    keshikomiZen += keshikomiGaku;
                    keshikomiGaku = 0;
                    newRow["KESHIKOMI_KINGAKU"] = DBNull.Value;
                    newRow["KESHIKOMI_BIKOU"] = string.Empty;
                }
                else
                {
                    if (matchShukkinNumFlg)
                    {
                        newRow["KESHIKOMI_KINGAKU"] = row["KESHIKOMI_KINGAKU"];
                        newRow["KESHIKOMI_BIKOU"] = row["KESHIKOMI_BIKOU"];
                    }
                }
                newRow["MIKESHIKOMI_KINGAKU"] = this.ConvertToDecimal(row["SEISAN_KINGAKU"]) - keshikomiZen - keshikomiGaku;
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
            ret.ShukkinEntry = new T_SHUKKIN_ENTRY();
            ret.ShukkinDetailList.Clear();
            ret.ShukkinKeshikomiList.Clear();

            // 出金入力エンティティ作成
            var newShukkinEntryBinderLogic = new DataBinderLogic<T_SHUKKIN_ENTRY>(ret.ShukkinEntry);
            newShukkinEntryBinderLogic.SetSystemProperty(ret.ShukkinEntry, false);

            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                ret.ShukkinEntry.SYSTEM_ID = this.CreateSystemId();
                ret.ShukkinEntry.SEQ = 1;
                ret.ShukkinEntry.SHUKKIN_NUMBER = this.CreateShukkinNumber();
                //[バンキング出力フラグ]は隠し項目とすること。
                ret.ShukkinEntry.BANK_EXPORTED_FLG = false;
            }
            else
            {
                ret.ShukkinEntry.SYSTEM_ID = this.dto.ShukkinEntry.SYSTEM_ID;
                ret.ShukkinEntry.SEQ = this.dto.ShukkinEntry.SEQ + 1;
                ret.ShukkinEntry.SHUKKIN_NUMBER = this.dto.ShukkinEntry.SHUKKIN_NUMBER;
                ret.ShukkinEntry.BANK_EXPORTED_FLG = this.dto.ShukkinEntry.BANK_EXPORTED_FLG;
                ret.ShukkinEntry.CREATE_DATE = this.dto.ShukkinEntry.CREATE_DATE;
                ret.ShukkinEntry.CREATE_PC = this.dto.ShukkinEntry.CREATE_PC;
                ret.ShukkinEntry.CREATE_USER = this.dto.ShukkinEntry.CREATE_USER;
            }
            ret.ShukkinEntry.KYOTEN_CD = this.ConvertToSqlInt16(this.headerForm.txtKyotenCd.Text);
            ret.ShukkinEntry.DENPYOU_DATE = (DateTime)this.form.DENPYOU_DATE.Value;
            ret.ShukkinEntry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            ret.ShukkinEntry.SHUKKINSAKI_CD = this.form.SHUKKINSAKI_CD.Text;
            //銀行
            if (!string.IsNullOrEmpty(this.form.FURIKOMI_SHUTSURYOKU_KBN.Text))
            {
                ret.ShukkinEntry.FURIKOMI_SHUTSURYOKU_KBN = Int16.Parse(this.form.FURIKOMI_SHUTSURYOKU_KBN.Text);
            }
            if (!string.IsNullOrEmpty(this.form.BANK_CD.Text))
            {
                ret.ShukkinEntry.BANK_CD = this.form.BANK_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.BANK_SHITEN_CD.Text))
            {
                ret.ShukkinEntry.BANK_SHITEN_CD = this.form.BANK_SHITEN_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.KOUZA_SHURUI.Text))
            {
                ret.ShukkinEntry.KOUZA_SHURUI = this.form.KOUZA_SHURUI.Text;
            }
            if (!string.IsNullOrEmpty(this.form.KOUZA_NO.Text))
            {
                ret.ShukkinEntry.KOUZA_NO = this.form.KOUZA_NO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.KOUZA_NAME.Text))
            {
                ret.ShukkinEntry.KOUZA_NAME = this.form.KOUZA_NAME.Text;
            }
            //振込先銀行
            if (!string.IsNullOrEmpty(this.form.FURIKOMI_BANK_CD.Text))
            {
                ret.ShukkinEntry.FURIKOMI_BANK_CD = this.form.FURIKOMI_BANK_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.FURIKOMI_BANK_NAME.Text))
            {
                ret.ShukkinEntry.FURIKOMI_BANK_NAME = this.form.FURIKOMI_BANK_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.FURIKOMI_BANK_SHITEN_CD.Text))
            {
                ret.ShukkinEntry.FURIKOMI_BANK_SHITEN_CD = this.form.FURIKOMI_BANK_SHITEN_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.FURIKOMI_BANK_SHITEN_NAME.Text))
            {
                ret.ShukkinEntry.FURIKOMI_BANK_SHITEN_NAME = this.form.FURIKOMI_BANK_SHITEN_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.FURIKOMI_KOUZA_SHURUI_CD.Text))
            {
                ret.ShukkinEntry.FURIKOMI_KOUZA_SHURUI_CD = Int16.Parse(this.form.FURIKOMI_KOUZA_SHURUI_CD.Text);
                ret.ShukkinEntry.FURIKOMI_KOUZA_SHURUI_NAME = this.form.FURIKOMI_KOUZA_SHURUI_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.FURIKOMI_KOUZA_NO.Text))
            {
                ret.ShukkinEntry.FURIKOMI_KOUZA_NO = this.form.FURIKOMI_KOUZA_NO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.FURIKOMI_KOUZA_NAME.Text))
            {
                ret.ShukkinEntry.FURIKOMI_KOUZA_NAME = this.form.FURIKOMI_KOUZA_NAME.Text;
            }

            ret.ShukkinEntry.SHUKKIN_AMOUNT_TOTAL = this.ShukkinTotal;
            ret.ShukkinEntry.CHOUSEI_AMOUNT_TOTAL = this.chouseiTotal;
            ret.ShukkinEntry.DELETE_FLG = false;

            Int16 rowNum = 1;
            foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                // 出金明細エンティティ作成
                var detail = new T_SHUKKIN_DETAIL();
                detail.SYSTEM_ID = ret.ShukkinEntry.SYSTEM_ID;
                detail.SEQ = ret.ShukkinEntry.SEQ;
                detail.DETAIL_SYSTEM_ID = this.CreateSystemId();
                detail.ROW_NUMBER = rowNum;
                detail.NYUUSHUKKIN_KBN_CD = this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD"].Value);
                detail.KINGAKU = this.ConvertToSqlDecimal(row.Cells["KINGAKU"].Value);
                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["MEISAI_BIKOU"].Value)))
                {
                    detail.MEISAI_BIKOU = Convert.ToString(row.Cells["MEISAI_BIKOU"].Value);
                }

                ret.ShukkinDetailList.Add(detail);

                rowNum++;
            }

            var data = new T_SHUKKIN_KESHIKOMI();
            foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
            {
                //if (string.IsNullOrEmpty(Convert.ToString(row.Cells["KESHIKOMI_KINGAKU"].Value)))
                //{
                //    continue;
                //}
                var keshikomi = new T_SHUKKIN_KESHIKOMI();
                keshikomi.SYSTEM_ID = ret.ShukkinEntry.SYSTEM_ID;
                keshikomi.SEISAN_NUMBER = this.ConvertToSqlInt64(row.Cells["SEISAN_NUMBER"].Value);
                keshikomi.KAGAMI_NUMBER = this.ConvertToSqlInt32(row.Cells["KAGAMI_NUMBER"].Value);

                data = new T_SHUKKIN_KESHIKOMI();
                data.SYSTEM_ID = keshikomi.SYSTEM_ID;
                data.SEISAN_NUMBER = keshikomi.SEISAN_NUMBER;
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

                keshikomi.SHUKKIN_NUMBER = ret.ShukkinEntry.SHUKKIN_NUMBER;
                keshikomi.TORIHIKISAKI_CD = ret.ShukkinEntry.TORIHIKISAKI_CD;
                keshikomi.KESHIKOMI_GAKU = this.ConvertToSqlDecimal(row.Cells["KESHIKOMI_KINGAKU"].Value);
                keshikomi.KESHIKOMI_BIKOU = Convert.ToString(row.Cells["KESHIKOMI_BIKOU"].Value);
                keshikomi.SHUKKIN_SEQ = 0;
                keshikomi.DELETE_FLG = Convert.ToBoolean(row.Cells["DELETE_FLG"].Value);
                if (!keshikomi.KESHIKOMI_GAKU.IsNull && !keshikomi.DELETE_FLG)
                {
                    ret.ShukkinKeshikomiList.Add(keshikomi);
                }
            }

            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                this.dto.ShukkinEntry.DELETE_FLG = true;
                foreach (var keshikomi in this.dto.ShukkinKeshikomiList)
                {
                    keshikomi.DELETE_FLG = true;
                }
                if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    ret.ShukkinEntry.DELETE_FLG = true;
                    foreach (var keshikomi in ret.ShukkinKeshikomiList)
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
        /// [振込出力]の入力値により、下記の通り必須チェックを行う。
        /// </summary>
        private void SettingRequiredMethod()
        {
            if (this.form.FURIKOMI_SHUTSURYOKU_KBN.Text == ConstInfo.FURIKOMI_SHUTSURYOKU_ARI)
            {
                string methodName = "必須チェック";
                this.form.BANK_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>() { new SelectCheckDto() { CheckMethodName = methodName } };
                this.form.BANK_SHITEN_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>() { new SelectCheckDto() { CheckMethodName = methodName } };
                this.form.FURIKOMI_BANK_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>() { new SelectCheckDto() { CheckMethodName = methodName } };
                this.form.FURIKOMI_BANK_SHITEN_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>() { new SelectCheckDto() { CheckMethodName = methodName } };
                this.form.FURIKOMI_KOUZA_SHURUI_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>() { new SelectCheckDto() { CheckMethodName = methodName } };
                this.form.FURIKOMI_KOUZA_NO.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>() { new SelectCheckDto() { CheckMethodName = methodName } };
            }
            else
            {
                this.form.BANK_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>();
                this.form.BANK_SHITEN_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>();
                this.form.FURIKOMI_BANK_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>();
                this.form.FURIKOMI_BANK_SHITEN_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>();
                this.form.FURIKOMI_KOUZA_SHURUI_CD.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>();
                this.form.FURIKOMI_KOUZA_NO.RegistCheckMethod = new System.Collections.ObjectModel.Collection<SelectCheckDto>();
            }
        }
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
                if (this.form.DETAIL_Ichiran.Rows.Count > 0) //&&
                    //this.form.DETAIL_Ichiran.Rows.Count == this.SysInfo.SHUKKIN_IKKATSU_KBN_DISP_SUU.Value)
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
                this.SettingRequiredMethod();
                var autoCheckLogic = new AutoRegistCheckLogic(this.form.allControl, this.form.allControl);
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                // 出金一覧行に入力が無い場合はエラー
                if (!this.form.RegistErrorFlag && this.form.DETAIL_Ichiran.Rows.Count == 1)
                {
                    DataGridViewRow row = this.form.DETAIL_Ichiran.Rows[this.form.DETAIL_Ichiran.Rows.Count - 1];
                    if (row.IsNewRow
                        && (string.IsNullOrEmpty(Convert.ToString(row.Cells["NYUUSHUKKIN_KBN_CD"].Value)) || string.IsNullOrEmpty(Convert.ToString(row.Cells["KINGAKU"].Value))))
                    {
                        msgLogic.MessageBoxShow("E001", "出金区分および出金額");
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
                // [出金―消込差額]チェックフラッグ
                bool keshikomiFlg = true;
                foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                {
                    keshikomiGaku = 0;
                    total = 0;
                    seikyuu = 0;
                    string keshikomi_value = Convert.ToString(row.Cells["KESHIKOMI_KINGAKU"].Value);
                    string zenkai_value = Convert.ToString(row.Cells["KESHIKOMI_KINGAKU_ZEN"].Value);
                    string seisan_value = Convert.ToString(row.Cells["SEISAN_KINGAKU"].Value);
                    if (!string.IsNullOrEmpty(keshikomi_value) && !Convert.ToBoolean(row.Cells["DELETE_FLG"].Value))
                    {
                        keshikomiGaku = Convert.ToDecimal(keshikomi_value.Replace(",", ""));
                    }
                    if (!string.IsNullOrEmpty(zenkai_value))
                    {
                        total = Convert.ToDecimal(zenkai_value.Replace(",", "")) + keshikomiGaku;
                    }
                    if (!string.IsNullOrEmpty(seisan_value))
                    {
                        seikyuu = Convert.ToDecimal(seisan_value.Replace(",", ""));
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

                // [出金―消込差額]チェック
                if (keshikomiFlg)
                {
                    DialogResult dialogResult = new DialogResult();
                    if (this.sagaku > 0)
                    {
                        dialogResult = msgLogic.MessageBoxShow("C124");
                        if (!dialogResult.Equals(DialogResult.OK)
                            && !dialogResult.Equals(DialogResult.Yes))
                        {
                            return;
                        }
                    }
                    else if (this.sagaku < 0)
                    {
                        dialogResult = msgLogic.MessageBoxShow("C125");
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
                        DateTime beforeDate = DateTime.Parse(this.dto.ShukkinEntry.DENPYOU_DATE.ToString());
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
                    var seisanDetailList = this.seisanDetailDao.GetDataByShukkinNumber(this.dto.ShukkinEntry.SHUKKIN_NUMBER);
                    if (seisanDetailList.Count() > 0)
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

                // 精算日付チェック
                if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType && false == this.form.RegistErrorFlag)
                {
                    if (!this.SeisanDateCheck())
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
                            // 出金
                            this.entryDao.Insert(registDto.ShukkinEntry);

                            // 出金明細
                            foreach (var detail in registDto.ShukkinDetailList)
                            {
                                this.detailDao.Insert(detail);
                            }

                            var data = new T_SHUKKIN_KESHIKOMI();
                            // 出金消込
                            foreach (var keshikomi in registDto.ShukkinKeshikomiList)
                            {
                                this.keshikomiDao.Insert(keshikomi);
                            }

                            if (this.form.CASHER_LINK_KBN.Text == CommonConst.CASHER_LINK_KBN_USE)
                            {
                                // キャッシャ連動「1.する」の場合キャッシャ情報送信
                                this.sendCasher(registDto.ShukkinEntry, registDto.ShukkinDetailList);
                            }
                        }
                        else
                        {
                            // 出金
                            this.entryDao.Update(this.dto.ShukkinEntry);
                            this.entryDao.Insert(registDto.ShukkinEntry);                           
                            // 出金明細
                            foreach (var detail in this.dto.ShukkinDetailList)
                            {
                                this.detailDao.Update(detail);
                            }
                            foreach (var detail in registDto.ShukkinDetailList)
                            {
                                this.detailDao.Insert(detail);
                            }
                            // 出金消込
                            var data = new T_SHUKKIN_KESHIKOMI();
                            data = new T_SHUKKIN_KESHIKOMI();
                            data.SYSTEM_ID = this.dto.ShukkinEntry.SYSTEM_ID;
                            data.SHUKKIN_NUMBER = this.dto.ShukkinEntry.SHUKKIN_NUMBER;
                            data.DELETE_FLG = false;
                            var datas = this.keshikomiDao.GetKeshikomi(data);

                            foreach (var keshikomi in datas)
                            {
                                keshikomi.DELETE_FLG = true;
                                this.keshikomiDao.Update(keshikomi);
                            }

                            foreach (var keshikomi in registDto.ShukkinKeshikomiList)
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
                    }
                    else
                    {
                        messageLogic.MessageBoxShow("I001", "削除");
                    }

                    var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());
                    if (Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.SetShukkinNumber(SqlInt64.Null);
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

                // 出金消込データの検索
                if (this.Search() == -1)
                {
                    retVal = false;
                    return retVal;
                }

                // 出金額合計の取得
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
                        seikyuu = this.ConvertToDecimal(row.Cells["SEISAN_KINGAKU"].Value);
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
                    // 未消込額合計＜0　の場合（精算金額がマイナスの場合など）
                    foreach (DataGridViewRow row in this.form.KESHIKOMI_Ichiran.Rows)
                    {
                        seikyuu = this.ConvertToDecimal(row.Cells["SEISAN_KINGAKU"].Value);
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
                    seikyuu = this.ConvertToDecimal(row.Cells["SEISAN_KINGAKU"].Value);
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
                    // 出金区分行に21:手数料の行が存在する場合は、その金額に未消込額合計を加算し、21:手数料の行は追加しない
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
                    // 出金区分行に21:手数料の行が存在しない場合
                    M_NYUUSHUKKIN_KBN data = new M_NYUUSHUKKIN_KBN();
                    DataGridViewRow row = this.form.DETAIL_Ichiran.Rows[0];
                    //int count = this.form.DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
                    //if (count == this.SysInfo.SHUKKIN_IKKATSU_KBN_DISP_SUU.Value)
                    //{
                    //    // 出金区分表示数の最大値＝出金区分行行数、となる場合はアラート
                    //    // ただし、最後の行がブランクの場合を除く
                    //    row = this.form.DETAIL_Ichiran.Rows[this.form.DETAIL_Ichiran.Rows.Count - 1];
                    //    if (string.IsNullOrEmpty(Convert.ToString(row.Cells["NYUUSHUKKIN_KBN_CD"].Value))
                    //        && string.IsNullOrEmpty(Convert.ToString(row.Cells["KINGAKU"].Value))
                    //        && string.IsNullOrEmpty(Convert.ToString(row.Cells["MEISAI_BIKOU"].Value)))
                    //    {
                    //        row.Cells["NYUUSHUKKIN_KBN_CD"].Value = 21;
                    //        data = this.nyuuShukkinKbnDao.GetDataByCd(21);
                    //        if (data != null)
                    //        {
                    //            row.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = data.NYUUSHUKKIN_KBN_NAME_RYAKU;
                    //        }
                    //        row.Cells["KINGAKU"].Value = tesuuryou;
                    //    }
                    //    else
                    //    {
                    //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //        msgLogic.MessageBoxShowError("表示数が最大値を超えてしまうため、手数料消込を実行できません。");
                    //        return ret;
                    //    }
                    //}
                    //else
                    //{
                    // 21:手数料の出金区分行を追加準備
                    this.form.DETAIL_Ichiran.Rows.Add();
                    row = this.form.DETAIL_Ichiran.Rows[this.form.DETAIL_Ichiran.Rows.Count - 2];
                    //}

                    // 21:手数料の出金区分行を追加
                    row.Cells["NYUUSHUKKIN_KBN_CD"].Value = 21;
                    data = this.nyuuShukkinKbnDao.GetDataByCd(21);
                    if (data != null)
                    {
                        row.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = data.NYUUSHUKKIN_KBN_NAME_RYAKU;
                    }
                    row.Cells["KINGAKU"].Value = tesuuryou;
                    //if (this.form.DETAIL_Ichiran.Rows.Count > this.SysInfo.SHUKKIN_IKKATSU_KBN_DISP_SUU.Value)
                    //{
                    //    this.form.DETAIL_Ichiran.AllowUserToAddRows = false;
                    //}
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
                seikyuu = this.ConvertToDecimal(row.Cells["SEISAN_KINGAKU"].Value);
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

                //if (this.form.DETAIL_Ichiran.Rows.Count < this.SysInfo.SHUKKIN_IKKATSU_KBN_DISP_SUU.Value)
                //{
                //    this.form.DETAIL_Ichiran.AllowUserToAddRows = true;
                //}
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
                //if (this.form.DETAIL_Ichiran.Rows.Count < this.SysInfo.SHUKKIN_IKKATSU_KBN_DISP_SUU.Value)
                //{
                if (this.form.DETAIL_Ichiran.CurrentRow == null)
                {
                    this.form.DETAIL_Ichiran.Rows.Add();
                }
                else
                {
                    this.form.DETAIL_Ichiran.Rows.Insert(this.form.DETAIL_Ichiran.CurrentRow.Index, 1);
                }
                //}
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
            ret = accessor.createSystemId((int)DENSHU_KBN.SHUKKIN);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region 出金番号採番

        /// <summary>
        /// 新規に出金番号を取得します
        /// </summary>
        /// <returns></returns>
        public SqlInt64 CreateShukkinNumber()
        {
            LogUtility.DebugMethodStart();

            var ret = SqlInt64.Null;

            var accessor = new DBAccessor();
            ret = accessor.createDenshuNumber((int)DENSHU_KBN.SHUKKIN);

            LogUtility.DebugMethodEnd(ret);

            return ret;
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
                if (name == "SEISAN_DATE")
                {
                    name = "SORT_SEISAN_DATE";
                }
                sb.AppendFormat("{0} {1}", name, item.IsAsc ? "ASC" : "DESC");
            }

            if (sb.Length == 0)
            {
                sb.Append("SORT_SEISAN_DATE ASC,GYOUSHA_CD ASC,GENBA_CD ASC");
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
                this.ShukkinTotal = 0;
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
                        this.ShukkinTotal += kingaku;
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
                this.form.SHUKKIN_AMOUNT_TOTAL.Text = this.FormatKingaku(ShukkinTotal);
                this.form.CHOUSEI_AMOUNT_TOTAL.Text = this.FormatKingaku(chouseiTotal);
                this.form.TOTAL.Text = this.FormatKingaku(chouseiTotal + ShukkinTotal);
                this.form.KESHIKOMIGAKU.Text = this.FormatKingaku(keshikomiTotal);
                // 出金―消込差額
                this.sagaku = chouseiTotal + ShukkinTotal - keshikomiTotal;
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

        #region 精算日付チェック

        /// <summary>
        /// 精算日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool SeisanDateCheck()
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

            returnDate = CheckShimeDate.GetNearShimeDate(checkDate, 2);

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
                if (msgLogic.MessageBoxShow("C084", returnDate[0].dtDATE.ToString("yyyy/MM/dd"), "精算") == DialogResult.Yes)
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
        /// 取得した出金伝票を画面にセットします
        /// </summary>
        internal void SetShukkinData()
        {
            LogUtility.DebugMethodStart();

            this.ClearFormData(true);

            this.GetShukkinData(this.ShukkinNumber, this.seq);
            if (null == this.dto.ShukkinEntry)
            {
                // 該当データなし
                var messageLogic = new MessageBoxShowLogic();
                messageLogic.MessageBoxShow("E045");
                this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                this.SetShukkinNumber(SqlInt64.Null);
                if (!this.WindowInit(true))
                {
                    return;
                }
            }
            else
            {
                // 画面にデータセット
                // 拠点
                this.headerForm.txtKyotenCd.Text = this.dto.ShukkinEntry.KYOTEN_CD.Value.ToString("00");
                var kyoten = this.kyotenDao.GetDataByCd(this.dto.ShukkinEntry.KYOTEN_CD.Value.ToString());
                if (null != kyoten)
                {
                    this.headerForm.txtKyotenName.Text = kyoten.KYOTEN_NAME_RYAKU;
                }
                // 出金番号
                this.form.SHUKKIN_NUMBER.Text = this.dto.ShukkinEntry.SHUKKIN_NUMBER.Value.ToString();
                // 伝票日付
                this.form.DENPYOU_DATE.Value = this.dto.ShukkinEntry.DENPYOU_DATE;
                // 取引先CD
                this.form.TORIHIKISAKI_CD.Text = this.dto.ShukkinEntry.TORIHIKISAKI_CD;

                M_TORIHIKISAKI torihikisaki = new M_TORIHIKISAKI();
                torihikisaki.TORIHIKISAKI_CD = this.dto.ShukkinEntry.TORIHIKISAKI_CD;
                torihikisaki.ISNOT_NEED_DELETE_FLG = true;
                torihikisaki = torihikisakiDao.GetAllValidData(torihikisaki).FirstOrDefault();

                if (null != torihikisaki)
                {
                    this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                }
                // 出金先CD
                this.form.SHUKKINSAKI_CD.Text = this.dto.ShukkinEntry.SHUKKINSAKI_CD;
                var nyuukinsaki = this.ShukkinsakiDao.GetDataByCd(this.dto.ShukkinEntry.SHUKKINSAKI_CD);
                if (null != nyuukinsaki)
                {
                    this.form.SHUKKINSAKI_NAME.Text = nyuukinsaki.SYUKKINSAKI_NAME_RYAKU;
                }
                // 銀行CD
                this.form.BANK_CD.Text = this.dto.ShukkinEntry.BANK_CD;
                var bank = this.bankDao.GetDataByCd(this.dto.ShukkinEntry.BANK_CD);
                if (null != bank)
                {
                    this.form.BANK_NAME_RYAKU.Text = bank.BANK_NAME_RYAKU;
                }
                else
                {
                    this.form.BANK_NAME_RYAKU.Text = "";
                }
                // 銀行支店CD
                this.form.BANK_SHITEN_CD.Text = this.dto.ShukkinEntry.BANK_SHITEN_CD;
                var bankShiten = new M_BANK_SHITEN();
                bankShiten.BANK_CD = this.dto.ShukkinEntry.BANK_CD;
                bankShiten.BANK_SHITEN_CD = this.dto.ShukkinEntry.BANK_SHITEN_CD;
                bankShiten.KOUZA_NO = this.dto.ShukkinEntry.KOUZA_NO;
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
                this.form.KOUZA_SHURUI.Text = this.dto.ShukkinEntry.KOUZA_SHURUI;
                // 口座番号
                this.form.KOUZA_NO.Text = this.dto.ShukkinEntry.KOUZA_NO;
                this.form.KOUZA_NAME.Text = this.dto.ShukkinEntry.KOUZA_NAME;
                //振込先銀行
                if (!this.dto.ShukkinEntry.FURIKOMI_SHUTSURYOKU_KBN.IsNull)
                {
                    this.form.FURIKOMI_SHUTSURYOKU_KBN.Text = this.dto.ShukkinEntry.FURIKOMI_SHUTSURYOKU_KBN.Value.ToString();
                }
                //DAT #162934 S
                var entityToriShiharai = torihikisakiShiharaiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                if (entityToriShiharai != null)
                {
                    if (!entityToriShiharai.TEI_SUU_RYOU_KBN.IsNull)
                    {
                        this.form.TEI_SUU_RYOU_KBN.Text = entityToriShiharai.TEI_SUU_RYOU_KBN.Value.ToString();//}
                    }
                    else
                    {
                        this.form.TEI_SUU_RYOU_KBN.Clear();
                    }
                }
                //DAT #162934 E
                this.form.FURIKOMI_BANK_CD.Text = this.dto.ShukkinEntry.FURIKOMI_BANK_CD;
                this.form.FURIKOMI_BANK_NAME.Text = this.dto.ShukkinEntry.FURIKOMI_BANK_NAME;
                this.form.FURIKOMI_BANK_SHITEN_CD.Text = this.dto.ShukkinEntry.FURIKOMI_BANK_SHITEN_CD;
                this.form.FURIKOMI_BANK_SHITEN_NAME.Text = this.dto.ShukkinEntry.FURIKOMI_BANK_SHITEN_NAME;
                if (!this.dto.ShukkinEntry.FURIKOMI_KOUZA_SHURUI_CD.IsNull)
                {
                    this.form.FURIKOMI_KOUZA_SHURUI_CD.Text = this.dto.ShukkinEntry.FURIKOMI_KOUZA_SHURUI_CD.Value.ToString();
                    this.form.FURIKOMI_KOUZA_SHURUI_NAME.Text = this.dto.ShukkinEntry.FURIKOMI_KOUZA_SHURUI_NAME;
                }
                this.form.FURIKOMI_KOUZA_NO.Text = this.dto.ShukkinEntry.FURIKOMI_KOUZA_NO;
                this.form.FURIKOMI_KOUZA_NAME.Text = this.dto.ShukkinEntry.FURIKOMI_KOUZA_NAME;

                var index = 0;
                if (this.dto.ShukkinDetailList.Count > 0)
                {
                    this.form.DETAIL_Ichiran.Rows.Add(this.dto.ShukkinDetailList.Count);
                    index = 0;
                    foreach (var entity in this.dto.ShukkinDetailList)
                    {
                        var row = this.form.DETAIL_Ichiran.Rows[index];
                        row.Cells["NYUUSHUKKIN_KBN_CD"].Value = entity.NYUUSHUKKIN_KBN_CD.Value;
                        var nyuuShukkinKbn = this.nyuuShukkinKbnDao.GetDataByCd(entity.NYUUSHUKKIN_KBN_CD.Value);
                        if (null != nyuuShukkinKbn)
                        {
                            row.Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = nyuuShukkinKbn.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                        if (!entity.KINGAKU.IsNull)
                        {
                            row.Cells["KINGAKU"].Value = entity.KINGAKU.Value;
                        }
                        row.Cells["MEISAI_BIKOU"].Value = entity.MEISAI_BIKOU;

                        index++;
                    }
                }

                // 出金消込
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

            //// 画面上のデータをクリア
            this.form.DETAIL_Ichiran.AllowUserToAddRows = false;

            // 修正⇒新規モード時などで削除済みデータがある場合、イベントが走ってエラーが出るので一旦手動で出金区分を削除する
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

            if (!this.ShukkinNumber.IsNull)
            {
                this.form.SHUKKIN_NUMBER.Text = this.ShukkinNumber.Value.ToString();
            }
            else
            {
                this.form.SHUKKIN_NUMBER.Text = string.Empty;
            }
            if (isClearDate)
            {
                this.form.DENPYOU_DATE.Value = this.parentForm.sysDate;
            }
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_CD.BackColor = Constans.NOMAL_COLOR;
            this.form.TORIHIKISAKI_NAME.Text = string.Empty;
            this.form.SHUKKINSAKI_CD.Text = string.Empty;
            this.form.SHUKKINSAKI_NAME.Text = string.Empty;

            //並び順をクリア
            this.sortSettingInfo = SortSettingHelper.LoadSortSettingInfo("UIForm.KESHIKOMI_Ichiran");

            this.form.isError = false;
            this.form.beforeTorihikisakiCd = "";
            this.form.beforeBankCd = "";
            this.form.searchDate = "";

            this.form.SHUKKIN_AMOUNT_TOTAL.Text = "0";
            this.form.CHOUSEI_AMOUNT_TOTAL.Text = "0";
            this.form.KESHIKOMIGAKU.Text = "0";
            this.form.TOTAL.Text = "0";
            // 出金―消込差額
            this.form.SAGAKU.Text = "0";
            this.form.SAGAKU.ForeColor = Color.Black;
            //銀行 & 振込先銀行
            this.ClearBankData();
            this.form.FURIKOMI_SHUTSURYOKU_KBN.Text = ConstInfo.FURIKOMI_SHUTSURYOKU_NASHI;
            this.FURIKOMI_SHUTSURYOKU_KBN_TextChanged(this.form.FURIKOMI_SHUTSURYOKU_KBN, new EventArgs());

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面イベント

        #region 出金番号Validated

        /// <summary>
        /// 出金番号Validated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal bool SHUKKIN_NUMBER_Validated(object sender, EventArgs e)
        {
            bool ret = true;
            try
            {
                // 再読み込みするかを示します
                bool isReload = false;

                // 出金蛮行空じゃない場合
                if (!string.IsNullOrEmpty(this.form.SHUKKIN_NUMBER.Text))
                {
                    // 前回値がNULLじゃない場合
                    if (!this.ShukkinNumber.IsNull)
                    {
                        // 出金番号と前回値が同じじゃない場合
                        if (this.ConvertToSqlInt64(this.form.SHUKKIN_NUMBER.Text).Value != this.ShukkinNumber.Value)
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
                    // 出金番号の前回値をクリア
                    this.SetShukkinNumber(SqlInt64.Null);
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

                        this.form.SHUKKIN_NUMBER.Focus();
                        return ret;
                    }

                    var entry = new T_SHUKKIN_ENTRY();
                    entry.SHUKKIN_NUMBER = this.ConvertToSqlInt64(this.form.SHUKKIN_NUMBER.Text);
                    entry.DELETE_FLG = false;
                    entry = this.entryDao.GetShukkinEntry(entry);
                    if (null == entry)
                    {
                        // 該当データなし
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E045");
                        this.form.SHUKKIN_NUMBER.Focus();
                        return ret;
                    }

                    this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.SetShukkinNumber(this.ConvertToSqlInt64(this.form.SHUKKIN_NUMBER.Text));
                    if (!this.WindowInit(true))
                    {
                        ret = false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SHUKKIN_NUMBER_Validated", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHUKKIN_NUMBER_Validated", ex);
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

            // 出金消込データがある場合は、伝票日付変更時にアラート表示
            if (this.form.beforeDenpyouDate != this.form.DENPYOU_DATE.Text)
            {
                if (KeshikomiUmuCheck())
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E326", "伝票日付");
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
                        this.form.SHUKKINSAKI_CD.Text = "";
                        this.form.SHUKKINSAKI_NAME.Text = "";
                        this.form.beforeTorihikisakiCd = "";
                        this.form.KESHIKOMI_Ichiran.Rows.Clear();
                        this.ClearBankData();
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
                        // 変更前の取引先CDの出金消込データが存在する場合
                        this.form.TORIHIKISAKI_NAME.Text = this.form.beforeTorihikisakiName;
                        messageLogic.MessageBoxShow("E233", "取引先");
                        this.form.TORIHIKISAKI_CD.Text = this.form.beforeTorihikisakiCd;
                        this.form.TORIHIKISAKI_CD.Focus();
                        return ret;
                    }

                    var shiharai = this.torihikisakiShiharaiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    if (shiharai == null || shiharai.TORIHIKI_KBN_CD.IsNull || shiharai.TORIHIKI_KBN_CD.Value != 2)
                    {
                        this.form.TORIHIKISAKI_NAME.Text = "";
                        this.ClearBankData();
                        messageLogic.MessageBoxShow("E226");
                        this.form.TORIHIKISAKI_CD.Focus();
                        this.form.isError = true;
                        return ret;
                    }
                    if (shiharai != null)
                    {
                        if (!shiharai.FURIKOMI_EXPORT_KBN.IsNull)
                        {
                            this.form.FURIKOMI_SHUTSURYOKU_KBN.Text = shiharai.FURIKOMI_EXPORT_KBN.Value.ToString();
                        }
                        if (this.form.FURIKOMI_SHUTSURYOKU_KBN.Text == ConstInfo.FURIKOMI_SHUTSURYOKU_ARI)
                        {
                            //銀行
                            if (!string.IsNullOrEmpty(shiharai.FURI_KOMI_MOTO_BANK_CD))
                            {
                                var bank = this.bankDao.GetDataByCd(shiharai.FURI_KOMI_MOTO_BANK_CD);
                                if (null != bank)
                                {
                                    this.form.BANK_CD.Text = bank.BANK_CD;
                                    this.form.BANK_NAME_RYAKU.Text = bank.BANK_NAME_RYAKU;
                                }
                            }
                            if (!string.IsNullOrEmpty(shiharai.FURI_KOMI_MOTO_BANK_CD)
                                && !string.IsNullOrEmpty(shiharai.FURI_KOMI_MOTO_BANK_SHITTEN_CD)
                                && !string.IsNullOrEmpty(shiharai.FURI_KOMI_MOTO_NO))
                            {
                                var bankShiten = new M_BANK_SHITEN();
                                bankShiten.BANK_CD = shiharai.FURI_KOMI_MOTO_BANK_CD;
                                bankShiten.BANK_SHITEN_CD = shiharai.FURI_KOMI_MOTO_BANK_SHITTEN_CD;
                                bankShiten.KOUZA_NO = shiharai.FURI_KOMI_MOTO_NO;
                                bankShiten = this.bankShitenDao.GetDataByCd(bankShiten);

                                if (null != bankShiten)
                                {
                                    this.form.BANK_SHITEN_CD.Text = bankShiten.BANK_SHITEN_CD;
                                    this.form.BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                                    this.form.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                                    this.form.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                                    this.form.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;
                                }
                            }
                            //振込先銀行
                            this.form.FURIKOMI_BANK_CD.Text = shiharai.FURIKOMI_SAKI_BANK_CD;
                            this.form.FURIKOMI_BANK_NAME.Text = shiharai.FURIKOMI_SAKI_BANK_NAME;
                            this.form.FURIKOMI_BANK_SHITEN_CD.Text = shiharai.FURIKOMI_SAKI_BANK_SHITEN_CD;
                            this.form.FURIKOMI_BANK_SHITEN_NAME.Text = shiharai.FURIKOMI_SAKI_BANK_SHITEN_NAME;
                            if (!shiharai.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.IsNull)
                            {
                                this.form.FURIKOMI_KOUZA_SHURUI_CD.Text = shiharai.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Value.ToString();
                                var kouzaShurui = ConstInfo.LIST_KOUZA_SHURUI.Where(s => s.KOUZA_SHURUI_CD == this.form.FURIKOMI_KOUZA_SHURUI_CD.Text).FirstOrDefault();
                                if (kouzaShurui != null)
                                {
                                    this.form.FURIKOMI_KOUZA_SHURUI_NAME.Text = kouzaShurui.KOUZA_SHURUI_NAME;
                                }
                            }
                            this.form.FURIKOMI_KOUZA_NO.Text = shiharai.FURIKOMI_SAKI_BANK_KOUZA_NO;
                            this.form.FURIKOMI_KOUZA_NAME.Text = shiharai.FURIKOMI_SAKI_BANK_KOUZA_NAME;
                        }
                        //手数料
                        //DAT #162934 S
                        if (!shiharai.TEI_SUU_RYOU_KBN.IsNull)
                        {
                            this.form.TEI_SUU_RYOU_KBN.Text = shiharai.TEI_SUU_RYOU_KBN.Value.ToString();
                        }
                        else
                        {
                            this.form.TEI_SUU_RYOU_KBN.Clear();
                        }
                        this.form.BANK_CD.Focus();
                        //DAT #162934 E
                    }
                    this.form.SHUKKINSAKI_CD.Text = shiharai.SYUUKINSAKI_CD;
                    var nyuukinsaki = this.ShukkinsakiDao.GetDataByCd(shiharai.SYUUKINSAKI_CD);
                    if (nyuukinsaki != null)
                    {
                        this.form.SHUKKINSAKI_NAME.Text = nyuukinsaki.SYUKKINSAKI_NAME_RYAKU;
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

                this.form.SHUKKINSAKI_CD.Text = "";
                this.form.SHUKKINSAKI_NAME.Text = "";
                this.form.KESHIKOMI_Ichiran.Rows.Clear();
                this.ClearBankData();

                var shiharai = this.torihikisakiShiharaiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                if (shiharai == null)
                {
                    this.form.TORIHIKISAKI_CD.Focus();
                    return;
                }
                if (shiharai != null)
                {
                    if (!shiharai.FURIKOMI_EXPORT_KBN.IsNull)
                    {
                        this.form.FURIKOMI_SHUTSURYOKU_KBN.Text = shiharai.FURIKOMI_EXPORT_KBN.Value.ToString();
                    }
                    if (this.form.FURIKOMI_SHUTSURYOKU_KBN.Text == ConstInfo.FURIKOMI_SHUTSURYOKU_ARI)
                    {
                        //銀行
                        if (!string.IsNullOrEmpty(shiharai.FURI_KOMI_MOTO_BANK_CD))
                        {
                            var bank = this.bankDao.GetDataByCd(shiharai.FURI_KOMI_MOTO_BANK_CD);
                            if (null != bank)
                            {
                                this.form.BANK_CD.Text = bank.BANK_CD;
                                this.form.BANK_NAME_RYAKU.Text = bank.BANK_NAME_RYAKU;
                            }
                        }
                        if (!string.IsNullOrEmpty(shiharai.FURI_KOMI_MOTO_BANK_CD)
                            && !string.IsNullOrEmpty(shiharai.FURI_KOMI_MOTO_BANK_SHITTEN_CD)
                            && !string.IsNullOrEmpty(shiharai.FURI_KOMI_MOTO_NO))
                        {
                            var bankShiten = new M_BANK_SHITEN();
                            bankShiten.BANK_CD = shiharai.FURI_KOMI_MOTO_BANK_CD;
                            bankShiten.BANK_SHITEN_CD = shiharai.FURI_KOMI_MOTO_BANK_SHITTEN_CD;
                            bankShiten.KOUZA_NO = shiharai.FURI_KOMI_MOTO_NO;
                            bankShiten = this.bankShitenDao.GetDataByCd(bankShiten);

                            if (null != bankShiten)
                            {
                                this.form.BANK_SHITEN_CD.Text = bankShiten.BANK_SHITEN_CD;
                                this.form.BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                                this.form.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                                this.form.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                                this.form.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;
                            }
                        }
                        //振込先銀行
                        this.form.FURIKOMI_BANK_CD.Text = shiharai.FURIKOMI_SAKI_BANK_CD;
                        this.form.FURIKOMI_BANK_NAME.Text = shiharai.FURIKOMI_SAKI_BANK_NAME;
                        this.form.FURIKOMI_BANK_SHITEN_CD.Text = shiharai.FURIKOMI_SAKI_BANK_SHITEN_CD;
                        this.form.FURIKOMI_BANK_SHITEN_NAME.Text = shiharai.FURIKOMI_SAKI_BANK_SHITEN_NAME;
                        if (!shiharai.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.IsNull)
                        {
                            this.form.FURIKOMI_KOUZA_SHURUI_CD.Text = shiharai.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Value.ToString();
                            var kouzaShurui = ConstInfo.LIST_KOUZA_SHURUI.Where(s => s.KOUZA_SHURUI_CD == this.form.FURIKOMI_KOUZA_SHURUI_CD.Text).FirstOrDefault();
                            if (kouzaShurui != null)
                            {
                                this.form.FURIKOMI_KOUZA_SHURUI_NAME.Text = kouzaShurui.KOUZA_SHURUI_NAME;
                            }
                        }
                        this.form.FURIKOMI_KOUZA_NO.Text = shiharai.FURIKOMI_SAKI_BANK_KOUZA_NO;
                        this.form.FURIKOMI_KOUZA_NAME.Text = shiharai.FURIKOMI_SAKI_BANK_KOUZA_NAME;
                    }
                    //手数料
                    //DAT #162934 S
                    if (!shiharai.TEI_SUU_RYOU_KBN.IsNull)
                    {
                        this.form.TEI_SUU_RYOU_KBN.Text = shiharai.TEI_SUU_RYOU_KBN.Value.ToString();
                    }
                    else
                    {
                        this.form.TEI_SUU_RYOU_KBN.Clear();
                    }
                    //DAT #162934 E
                }
                this.form.SHUKKINSAKI_CD.Text = shiharai.SYUUKINSAKI_CD;
                var nyuukinsaki = this.ShukkinsakiDao.GetDataByCd(shiharai.SYUUKINSAKI_CD);
                if (nyuukinsaki != null)
                {
                    this.form.SHUKKINSAKI_NAME.Text = nyuukinsaki.SYUKKINSAKI_NAME_RYAKU;
                }
            }
            this.form.TORIHIKISAKI_CD.Focus();
            return;
        }

        #endregion

        #region 出金消込データの有無チェック

        private bool KeshikomiUmuCheck()
        {
            bool retVal = false;

            T_SHUKKIN_ENTRY entry = new T_SHUKKIN_ENTRY();
            entry.SHUKKIN_NUMBER = this.ConvertToSqlInt64(this.form.SHUKKIN_NUMBER.Text);
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
            entry = this.entryDao.GetShukkinEntry(entry);
            if (entry != null && this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                T_SHUKKIN_KESHIKOMI data = new T_SHUKKIN_KESHIKOMI();
                data.SYSTEM_ID = entry.SYSTEM_ID;
                data.SHUKKIN_NUMBER = this.ConvertToSqlInt64(this.form.SHUKKIN_NUMBER.Text);
                data.DELETE_FLG = false;
                T_SHUKKIN_KESHIKOMI[] datas = this.keshikomiDao.GetKeshikomi(data);
                if (datas != null && datas.Length > 0)
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        #endregion

        #region 出金明細一覧の描画

        /// <summary>
        /// 出金明細一覧の描画時に処理します
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

                        TextRenderer.DrawText(e.Graphics, "出金区分", e.CellStyle.Font, rect, e.CellStyle.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
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

        #region 出金明細一覧CellEnter

        /// <summary>
        /// 出金明細一覧CellEnterイベント
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
                        if (!isShukkinKbnInputError)
                        {
                            this.beforeShukkinKbn = Convert.ToString(this.form.DETAIL_Ichiran.CurrentRow.Cells["NYUUSHUKKIN_KBN_CD"].Value);
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

        #region 出金明細一覧CellValidating

        /// <summary>
        /// 出金消込一覧のバリデート実行時に処理します
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
                        isShukkinKbnInputError = false;
                        string nyuuShukkinKbnCd = Convert.ToString(this.form.DETAIL_Ichiran.CurrentRow.Cells["NYUUSHUKKIN_KBN_CD"].Value);
                        if (nyuuShukkinKbnCd == "51")
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E042", "「51 仮受金」以外");
                            e.Cancel = true;
                        }
                        else if (!string.IsNullOrEmpty(nyuuShukkinKbnCd))
                        {
                            // CellEnterで取得した前回値と値が変わっている場合のみ処理
                            if (!nyuuShukkinKbnCd.Equals(beforeShukkinKbn))
                            {
                                short cd = 0;
                                if (short.TryParse(nyuuShukkinKbnCd, out cd))
                                {
                                    // 入出金区分マスタ検索：検索結果はMAXで1件
                                    M_NYUUSHUKKIN_KBN searchEntity = new M_NYUUSHUKKIN_KBN();
                                    searchEntity.NYUUSHUKKIN_KBN_CD = cd;
                                    M_NYUUSHUKKIN_KBN[] resultEntitys = this.nyuuShukkinKbnDao.GetAllValidData(searchEntity);

                                    if (resultEntitys == null || resultEntitys.Length < 1)
                                    {
                                        // 存在しないデータの場合はエラー)
                                        new MessageBoxShowLogic().MessageBoxShow("E020", "入出金区分");
                                        this.form.DETAIL_Ichiran.Rows[e.RowIndex].Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = null;
                                        isShukkinKbnInputError = true;
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
                                    isShukkinKbnInputError = true;
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

        #region 出金消込一覧CellValidating

        /// <summary>
        /// 出金消込一覧のバリデート実行時に処理します
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
                        string SEISAN_value = Convert.ToString(this.form.KESHIKOMI_Ichiran.CurrentRow.Cells["SEISAN_KINGAKU"].Value);
                        if (!string.IsNullOrEmpty(keshikomi_value))
                        {
                            keshikomi = Convert.ToDecimal(keshikomi_value.Replace(",", ""));
                        }
                        if (!string.IsNullOrEmpty(zenkai_value))
                        {
                            total = Convert.ToDecimal(zenkai_value.Replace(",", "")) + keshikomi;
                        }
                        if (!string.IsNullOrEmpty(SEISAN_value))
                        {
                            seikyuu = Convert.ToDecimal(SEISAN_value.Replace(",", ""));
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

            var bankEnable = !readOnly;
            this.form.pnlFURIKOMI_SHUTSURYOKU_KBN.Enabled = bankEnable;
            var shutsuryouFlg =  this.form.FURIKOMI_SHUTSURYOKU_KBN.Text == ConstInfo.FURIKOMI_SHUTSURYOKU_ARI ? true : false;
            this.EnableBankInfo(bankEnable && shutsuryouFlg);

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

            if (!readOnly)// && this.form.DETAIL_Ichiran.Rows.Count < this.SysInfo.SHUKKIN_IKKATSU_KBN_DISP_SUU.Value)
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

        #region キャッシャ連動

        /// <summary>
        /// キャッシャ情報送信
        /// </summary>
        /// <param name="entity">出金一括伝票</param>
        /// <param name="detailList">出金一括明細List</param>
        private void sendCasher(T_SHUKKIN_ENTRY entity, List<T_SHUKKIN_DETAIL> detailList)
        {
            // 出金額合計算出※現金のもののみ
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
                casherDto.DENPYOU_NUMBER = entity.SHUKKIN_NUMBER.Value;
                casherDto.KINGAKU = kingaku;
                casherDto.BIKOU = (string.IsNullOrEmpty(entity.DENPYOU_BIKOU) ? string.Empty : entity.DENPYOU_BIKOU);
                casherDto.DENSHU_KBN_CD = CommonConst.DENSHU_KBN_SHUKKINN;
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
            //LogUtility.DebugMethodStart(value);

            var ret = String.Empty;

            ret = this.ConvertToDecimal(value).ToString("#,##0");

            //LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlBoolean に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト（変換するオブジェクトが null の場合は False）</returns>
        internal SqlBoolean ConvertToSqlBooleanDefaultFalse(object value)
        {
            //LogUtility.DebugMethodStart(value);

            var ret = SqlBoolean.False;
            if (false == this.IsNullOrEmpty(value))
            {
                ret = SqlBoolean.Parse(value.ToString());
            }

            //LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        //thongh 2015/08/07 #12106 end

        #endregion

        #region 振込先銀行
        /// <summary>
        /// 振込先銀行(名称)をチェック
        /// </summary>
        /// <param name="value">振込先銀行(名称)</param>
        /// <returns></returns>
        private bool OnlineBankingBankNameCheck(string value)
        {
            bool validFlg = true;
            if (!string.IsNullOrEmpty(value))
            {
                //英字26種(A～Z)、数字10種(0～9)、カナ45種(ア～ン、ただし"ヲ"を除く)
                //、濁音、半濁音、記号5種(丸括弧"()",スラッシュ"/",ピリオド".",ハイフン"-")、スペース
                validFlg = System.Text.RegularExpressions.Regex.IsMatch(
                   value,
                   @"^[ 0-9A-Z()/.\-\uFF67-\uFF9F]+$");
            }
            return validFlg;
        }
        /// <summary>
        /// 振込先支店(名称)をチェック
        /// </summary>
        /// <param name="value">振込先支店(名称)</param>
        /// <returns></returns>
        private bool OnlineBankingShitenNameCheck(string value)
        {
            bool validFlg = true;
            if (!string.IsNullOrEmpty(value))
            {
                //英字26種(A～Z)、数字10種(0～9)、カナ45種(ア～ン、ただし"ヲ"を除く)
                //、濁音、半濁音、ハイフン(-)、スペース
                validFlg = System.Text.RegularExpressions.Regex.IsMatch(
                   value,
                   @"^[ 0-9A-Z\-\uFF67-\uFF9F]+$");
            }
            return validFlg;
        }
        /// <summary>
        /// 振込先口座名をチェック
        /// </summary>
        /// <param name="value">振込先口座名</param>
        /// <returns></returns>
        private bool OnlineBankingKouzaNameCheck(string value)
        {
            bool validFlg = true;
            if (!string.IsNullOrEmpty(value))
            {
            //英字26種(A～Z)、数字10種(0～9)、カナ46種(ア～ン)、濁音、半濁音、記号8種(エンマーク"\"
            //,鍵括弧"「」",丸括弧"()",スラッシュ"/",ピリオド".",ハイフン"-")、スペース
            validFlg = System.Text.RegularExpressions.Regex.IsMatch(
               value,
               @"^[ 0-9A-Z\\｢｣()/.\-\uFF66-\uFF9F]+$");
            }
            return validFlg;
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearBankData()
        {
            //DAT 20220505 #162935 S
            //this.form.BANK_CD.Clear();
            //this.form.BANK_NAME_RYAKU.Clear();
            //this.form.BANK_SHITEN_CD.Clear();
            //this.form.BANK_SHIETN_NAME_RYAKU.Clear();
            //this.form.KOUZA_SHURUI.Clear();
            //this.form.KOUZA_NO.Clear();
            //this.form.KOUZA_NAME.Clear();
            //DAT 20220505 #162935 E

            this.form.FURIKOMI_BANK_CD.Clear();
            this.form.FURIKOMI_BANK_NAME.Clear();
            this.form.FURIKOMI_BANK_SHITEN_CD.Clear();
            this.form.FURIKOMI_BANK_SHITEN_NAME.Clear();
            this.form.FURIKOMI_KOUZA_SHURUI_CD.Clear();
            this.form.FURIKOMI_KOUZA_SHURUI_NAME.Clear();
            this.form.FURIKOMI_KOUZA_NO.Clear();
            this.form.FURIKOMI_KOUZA_NAME.Clear();
            this.form.TE_SUURYOU.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enable"></param>
        private void EnableBankInfo(bool enable)
        {
            //DAT 20220505 #162935 S
            //this.form.BANK_CD.Enabled = enable;
            //this.form.BANK_NAME_RYAKU.Enabled = enable;
            //this.form.BANK_SHITEN_CD.Enabled = enable;
            //this.form.BANK_SHIETN_NAME_RYAKU.Enabled = enable;
            //this.form.KOUZA_SHURUI.Enabled = enable;
            //this.form.KOUZA_NO.Enabled = enable;
            //DAT 20220505 #162935 E

            this.form.FURIKOMI_BANK_CD.Enabled = enable;
            this.form.FURIKOMI_BANK_NAME.Enabled = enable;
            this.form.FURIKOMI_BANK_SHITEN_CD.Enabled = enable;
            this.form.FURIKOMI_BANK_SHITEN_NAME.Enabled = enable;
            this.form.FURIKOMI_KOUZA_SHURUI_CD.Enabled = enable;
            this.form.FURIKOMI_KOUZA_SHURUI_NAME.Enabled = enable;
            this.form.FURIKOMI_KOUZA_NO.Enabled = enable;
            this.form.FURIKOMI_KOUZA_NAME.Enabled = enable;
            //this.form.TE_SUURYOU.Enabled = enable;//#162934
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FURIKOMI_SHUTSURYOKU_KBN_TextChanged(object sender, EventArgs e)
        {
            var ctr = sender as CustomTextBox;
            //1.する
            if (ctr.Text == ConstInfo.FURIKOMI_SHUTSURYOKU_ARI)
            {
                this.EnableBankInfo(true);
            }
            //2.しない
            else if (ctr.Text == ConstInfo.FURIKOMI_SHUTSURYOKU_NASHI)
            {

                this.ClearBankData();
                this.EnableBankInfo(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FURIKOMI_KOUZA_SHURUI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var ctr = sender as CustomTextBox;
            if (!string.IsNullOrEmpty(ctr.Text))
            {
                var shurui = ConstInfo.LIST_KOUZA_SHURUI.Where(s => s.KOUZA_SHURUI_CD == ctr.Text).FirstOrDefault();
                if (shurui != null)
                {
                    this.form.FURIKOMI_KOUZA_SHURUI_NAME.Text = shurui.KOUZA_SHURUI_NAME;
                }
                else
                {
                    this.form.FURIKOMI_KOUZA_SHURUI_NAME.Clear();
                    ctr.IsInputErrorOccured = true;
                    this.errmessage.MessageBoxShow("E020", ctr.DisplayItemName);
                    e.Cancel = true;
                }
            }
            else
            {
                this.form.FURIKOMI_KOUZA_SHURUI_NAME.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FURIKOMI_KOUZA_NAME_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var ctr = sender as CustomTextBox;
            if (!this.OnlineBankingKouzaNameCheck(ctr.Text))
            {
                ctr.IsInputErrorOccured = true;
                this.errmessage.MessageBoxShow("E014", ctr.DisplayItemName);
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FURIKOMI_BANK_SHITEN_NAME_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var ctr = sender as CustomTextBox;
            if (!this.OnlineBankingShitenNameCheck(ctr.Text))
            {
                ctr.IsInputErrorOccured = true;
                this.errmessage.MessageBoxShow("E014", ctr.DisplayItemName);
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FURIKOMI_BANK_NAME_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var ctr = sender as CustomTextBox;
            if (!this.OnlineBankingBankNameCheck(ctr.Text))
            {
                ctr.IsInputErrorOccured = true;
                this.errmessage.MessageBoxShow("E014", ctr.DisplayItemName);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BANK_SHITEN_CD_Enter(object sender, EventArgs e)
        {
            this.form.BANK_SHITEN_CD.CausesValidation = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BANK_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.form.BANK_CD.Text) || !this.form.beforeBankCd.Equals(this.form.BANK_CD.Text))
            {
                this.form.BANK_SHITEN_CD.Text = string.Empty;
                this.form.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;
                this.form.KOUZA_SHURUI.Text = string.Empty;
                this.form.KOUZA_NO.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BANK_CD_Enter(object sender, EventArgs e)
        {
            this.form.beforeBankCd = this.form.BANK_CD.Text;
        }
        #endregion
    }
}