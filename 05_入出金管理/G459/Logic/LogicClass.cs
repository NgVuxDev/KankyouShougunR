using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
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
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2
{
    /// <summary>
    /// G459 入金入力 ビジネスロジック
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
        /// 読み込んだ伝票の入金番号
        /// </summary>
        private SqlInt64 nyuukinNumber;

        /// <summary>
        /// 読み込んだ伝票のSEQ
        /// </summary>
        private SqlInt32 seq;

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal BusinessBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        #endregion

        #region プロパティ

        /// <summary>
        /// システム設定エンティティを取得・設定します
        /// </summary>
        internal M_SYS_INFO SysInfo { get; private set; }

        /// <summary>
        /// 読み込んだ伝票の入金先CDを取得・設定します
        /// </summary>
        internal String NyuukinsakiCd { get; set; }

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

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

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

            this.GetSysInfo();

            this.headerForm = headerForm;
            this.form = targetForm;

            // 権限をセット
            var formId = FormManager.GetFormID(System.Reflection.Assembly.GetExecutingAssembly());
            this.AuthCanAdd = Manager.CheckAuthority(formId, WINDOW_TYPE.NEW_WINDOW_FLAG, false);
            this.AuthCanUpdate = Manager.CheckAuthority(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
            this.AuthCanDelete = Manager.CheckAuthority(formId, WINDOW_TYPE.DELETE_WINDOW_FLAG, false);
            this.AuthCanView = Manager.CheckAuthority(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false);
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.NyuukinsakiCd = string.Empty;

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 初期化処理

        /// <summary>
        /// モードに応じて画面を初期化します
        /// </summary>
        /// <param name="isClearDenpyouDate">伝票日付をクリアするかどうかのフラグ</param>
        public bool WindowInit(bool isClearDenpyouDate)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                this.form.HeaderFormInit();

                this.form.ClearFormData(isClearDenpyouDate);

                if (this.form.IsMoveData)
                {
                    this.form.NYUUKINSAKI_CD.Text = this.form.MoveDataNyuukinsakiCd;
                    var nyuukinsaki = this.GetNyuukinsaki(this.form.MoveDataNyuukinsakiCd);
                    if (null != nyuukinsaki)
                    {
                        this.form.NYUUKINSAKI_NAME_RYAKU.Text = nyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                    }
                }

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.dto = new DTOClass();
                        this.SetKyoten();
                        this.SetCorpBank();
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        this.form.SetNyuukinData();
                        this.form.SetSeikyuuDateAndSeikyuuKingaku();
                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.form.SetNyuukinData();
                        this.form.SetSeikyuuDateAndSeikyuuKingaku();
                        break;

                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.form.SetNyuukinData();
                        this.form.SetSeikyuuDateAndSeikyuuKingaku();
                        break;
                }

                this.form.NyuukinNumberReadOnlySwitch();

                if(!this.form.SetBankCheck())
                {
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                this.ButtonInit();
                this.EventInit();

                var isError = false;

                // 締処理中チェック
                // 修正モードで修正権限あり or 削除モードで削除権限あり 時のみチェック
                if (!isError && ((WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && this.AuthCanUpdate) || (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType && this.AuthCanDelete)))
                {
                    var torihikisakiCdList = new List<String>();
                    foreach (var entity in this.dto.NyuukinEntryList)
                    {
                        torihikisakiCdList.Add(entity.TORIHIKISAKI_CD);
                    }

                    if (torihikisakiCdList.Count > 0)
                    {
                        var shimeShoriChuuList = this.GetShimeShoriChuuList(this.dto.NyuukinSumEntry.DENPYOU_DATE.Value, torihikisakiCdList);
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
                    var torihikisakiCdList = new List<String>();
                    var seikyuuDetailList = this.GetSeikyuuDetailListByNyuukinNumber(this.dto.NyuukinSumEntry.NYUUKIN_NUMBER);
                    if (seikyuuDetailList.Count() > 0)
                    {
                        var seikyuuTorihikisakiList = seikyuuDetailList.Select(s => s.TORIHIKISAKI_CD).Distinct();
                        foreach (DataGridViewRow row in this.form.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow))
                        {
                            var seikyuuDetail = seikyuuDetailList.Where(s => s.TORIHIKISAKI_CD == row.Cells["TORIHIKISAKI_CD"].Value.ToString()).FirstOrDefault();
                            if (null != seikyuuDetail)
                            {
                                torihikisakiCdList.Add(seikyuuDetail.TORIHIKISAKI_CD);
                            }
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

                this.form.ChangeControlState();
                this.form.ChangeButtonState();

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

            LogUtility.DebugMethodEnd(ret);
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

            // 消込履歴ボタン(F6)イベント
            parentForm.bt_func6.Click -= new EventHandler(this.form.bt_func6_Click);
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            // 入出一覧ボタン(F7)イベント
            parentForm.bt_func7.Click -= new EventHandler(this.form.bt_func7_Click);
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            // 登録ボタン(F9)イベント
            parentForm.bt_func9.Click -= new EventHandler(this.form.bt_func9_Click);
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            // 行挿入ボタン(F10)イベント
            parentForm.bt_func10.Click -= new EventHandler(this.form.bt_func10_Click);
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            // 行削除ボタン(F11)イベント
            parentForm.bt_func11.Click -= new EventHandler(this.form.bt_func11_Click);
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            // 閉じるボタン(F12)イベント
            parentForm.bt_func12.Click -= new EventHandler(this.form.bt_func12_Click);
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // 入金額一括コピーボタンイベント
            parentForm.bt_process1.Click -= new EventHandler(this.form.bt_process1_Click);
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            // 入金消込情報ボタンイベント
            parentForm.bt_process3.Click -= new EventHandler(this.form.bt_process3_Click);
            parentForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);

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

            if (!String.IsNullOrEmpty(kyotenCd))
            {
                this.headerForm.txtKyotenCd.Text = config.ItemSetVal1.PadLeft(2, '0');
            }

            this.headerForm.txtKyotenName.Text = String.Empty;

            if (false == String.IsNullOrEmpty(this.headerForm.txtKyotenCd.Text))
            {
                var kyoten = this.GetKyoten(this.headerForm.txtKyotenCd.Text);
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
                var bankDao = DaoInitUtility.GetComponent<IM_BANKDao>();
                var banks = bankDao.GetAllValidData(new M_BANK { BANK_CD = Properties.Settings.Default.SET_BANK_CD });
                if (banks != null && banks.Count() > 0)
                {
                    this.form.BANK_CD.Text = banks[0].BANK_CD;
                    this.form.BANK_NAME_RYAKU.Text = banks[0].BANK_NAME_RYAKU;

                    // 銀行支店
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.SET_BANK_SHITEN_CD))
                    {
                        var bankShitenDao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
                        var bankShitens = bankShitenDao.GetAllValidData(
                                            new M_BANK_SHITEN
                                            {
                                                BANK_CD = Properties.Settings.Default.SET_BANK_CD,
                                                BANK_SHITEN_CD = Properties.Settings.Default.SET_BANK_SHITEN_CD,
                                                KOUZA_NO = Properties.Settings.Default.SET_KOUZA_NO
                                            });
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

            M_CORP_INFO corpInfo = null;

            var corpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            corpInfo = corpInfoDao.GetAllDataMinCols().FirstOrDefault();
            if (null != corpInfo
                && (string.IsNullOrEmpty(Properties.Settings.Default.SET_BANK_CD)))
            {
                this.form.BANK_CD.Text = corpInfo.BANK_CD;
                this.form.BANK_NAME_RYAKU.Text = string.Empty;
                this.form.BANK_SHITEN_CD.Text = corpInfo.BANK_SHITEN_CD;
                this.form.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;

                if (false == String.IsNullOrEmpty(corpInfo.BANK_CD))
                {
                    var bank = this.GetBank(corpInfo.BANK_CD);
                    if (null != bank)
                    {
                        this.form.BANK_NAME_RYAKU.Text = bank.BANK_NAME_RYAKU;
                    }
                }
                if (false == String.IsNullOrEmpty(corpInfo.BANK_CD) && false == String.IsNullOrEmpty(corpInfo.BANK_SHITEN_CD) && false == String.IsNullOrEmpty(corpInfo.KOUZA_NO))
                {
                    var bankShiten = this.GetBankShiten(corpInfo.BANK_CD, corpInfo.BANK_SHITEN_CD, corpInfo.KOUZA_NO);
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

        #region メソッド

        /// <summary>
        /// 読み込んだ伝票の入金番号を取得します
        /// </summary>
        /// <returns>入金番号</returns>
        internal SqlInt64 GetNyuukinNumber()
        {
            return this.nyuukinNumber;
        }

        /// <summary>
        /// 読み込んだ伝票のSEQを取得します
        /// </summary>
        /// <returns>SEQ</returns>
        internal SqlInt32 GetSeq()
        {
            return this.seq;
        }

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
            else if (true == String.IsNullOrEmpty(value.ToString()))
            {
                ret = true;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 指定されたオブジェクトをカンマ区切りの文字列にフォーマットします
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>返還後の文字列</returns>
        internal String FormatKingaku(object value, out bool catchErr)
        {
            LogUtility.DebugMethodStart(value);

            var ret = String.Empty;
            catchErr = true;

            try
            {
                ret = this.ConvertToDecimal(value).ToString("#,##0");
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormatKingaku", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        #endregion

        #region 型変換メソッド

        /// <summary>
        /// オブジェクトを String に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal String ConvertToString(object value)
        {
            LogUtility.DebugMethodStart(value);

            String ret = null;
            if (false == this.IsNullOrEmpty(value))
            {
                ret = value.ToString();
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを String に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト（変換するオブジェクトが null の場合は空文字列）</returns>
        internal String ConvertToStringDefaultEmpty(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = String.Empty;
            if (false == this.IsNullOrEmpty(value))
            {
                ret = value.ToString();
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを Decimal に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal Decimal ConvertToDecimal(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = 0m;
            if (false == this.IsNullOrEmpty(value))
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
            if (false == this.IsNullOrEmpty(value))
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
            if (false == this.IsNullOrEmpty(value))
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
            if (false == this.IsNullOrEmpty(value))
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
            if (false == this.IsNullOrEmpty(value))
            {
                ret = SqlInt16.Parse(value.ToString());
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlBoolean に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlBoolean ConvertToSqlBoolean(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = SqlBoolean.Null;
            if (false == this.IsNullOrEmpty(value))
            {
                ret = SqlBoolean.Parse(value.ToString());
            }

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

        #endregion

        #region データ取得メソッド

        /// <summary>
        /// システム設定を取得します
        /// </summary>
        private void GetSysInfo()
        {
            LogUtility.DebugMethodStart();

            var dao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.SysInfo = dao.GetAllData().FirstOrDefault();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 拠点エンティティを取得します
        /// </summary>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns>拠点エンティティ</returns>
        internal M_KYOTEN GetKyoten(string kyotenCd)
        {
            LogUtility.DebugMethodStart(kyotenCd);

            M_KYOTEN ret = null;

            var dao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            ret = dao.GetDataByCd(kyotenCd);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 銀行エンティティを取得します
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <returns>銀行エンティティ</returns>
        internal M_BANK GetBank(string bankCd)
        {
            LogUtility.DebugMethodStart(bankCd);

            M_BANK ret = null;

            var bankDao = DaoInitUtility.GetComponent<IM_BANKDao>();
            ret = bankDao.GetDataByCd(bankCd);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 銀行支店エンティティを取得します
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <param name="kouzaNo">口座番号</param>
        /// <returns>銀行支店エンティティ</returns>
        internal M_BANK_SHITEN GetBankShiten(string bankCd, string bankShitenCd, string kouzaNo)
        {
            LogUtility.DebugMethodStart(bankCd, bankShitenCd, kouzaNo);

            M_BANK_SHITEN ret = null;

            var bankShitenDao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
            ret = bankShitenDao.GetDataByCd(new M_BANK_SHITEN() { BANK_CD = bankCd, BANK_SHITEN_CD = bankShitenCd, KOUZA_NO = kouzaNo });

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 締処理中エンティティリストを取得します
        /// </summary>
        /// <param name="denpyouDate">伝票日付</param>
        /// <param name="torihikisakiCdList">取引先CDリスト</param>
        /// <returns>締処理中エンティティリスト</returns>
        internal List<T_SHIME_SHORI_CHUU> GetShimeShoriChuuList(DateTime denpyouDate, List<string> torihikisakiCdList)
        {
            LogUtility.DebugMethodStart(denpyouDate, torihikisakiCdList);

            List<T_SHIME_SHORI_CHUU> ret = new List<T_SHIME_SHORI_CHUU>();

            var dao = DaoInitUtility.GetComponent<IT_SHIME_SHORI_CHUUDao>();
            ret = dao.GetShimeShoriChuuList(this.dto.NyuukinSumEntry.DENPYOU_DATE.Value, torihikisakiCdList);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 指定された取引先の入金予定日が伝票日付に直近の請求伝票エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="denpyouDate">伝票日付</param>
        /// <returns>請求伝票エンティティ</returns>
        internal DataTable GetLastSeikyuuDenpyouByTorihikisakiCdAndNyuukinYoteiBi(string torihikisakiCd, DateTime denpyouDate)
        {
            LogUtility.DebugMethodStart(torihikisakiCd, denpyouDate);

            DataTable ret = null;

            var dao = DaoInitUtility.GetComponent<IT_SEIKYUU_DENPYOUDao>();
            ret = dao.GetLastSeikyuuDenpyouByTorihikisakiCdAndNyuukinYoteiBi(torihikisakiCd, denpyouDate);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 請求明細エンティティリストを取得します
        /// </summary>
        /// <param name="nyuukinNumber">入金番号</param>
        /// <returns>請求明細エンティティリスト</returns>
        internal List<T_SEIKYUU_DETAIL> GetSeikyuuDetailListByNyuukinNumber(SqlInt64 nyuukinNumber)
        {
            LogUtility.DebugMethodStart(nyuukinNumber);

            List<T_SEIKYUU_DETAIL> ret = new List<T_SEIKYUU_DETAIL>();

            var dao = DaoInitUtility.GetComponent<IT_SEIKYUU_DETAILDao>();
            ret = dao.GetSeikyuuDetailListByNyuukinNumber(nyuukinNumber);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 請求番号で伝票種類が入金の請求明細エンティティリストを取得します
        /// </summary>
        /// <param name="seikyuuNumber">請求番号</param>
        /// <returns>請求明細エンティティリスト</returns>
        internal List<T_SEIKYUU_DETAIL> GetSeikyuuDetailListBySeikyuuNumberAndNyuukinDenpyou(SqlInt64 seikyuuNumber)
        {
            LogUtility.DebugMethodStart(seikyuuNumber);

            List<T_SEIKYUU_DETAIL> ret = new List<T_SEIKYUU_DETAIL>();

            var dao = DaoInitUtility.GetComponent<IT_SEIKYUU_DETAILDao>();
            ret = dao.GetSeikyuuDetailListBySeikyuuNumberAndNyuukinDenpyou(seikyuuNumber);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入出金区分エンティティを取得します
        /// </summary>
        /// <param name="nyuushukkinKbnCd">入出金区分CD</param>
        /// <returns>入出金区分エンティティ</returns>
        internal M_NYUUSHUKKIN_KBN GetNyuushukkinKbn(int nyuushukkinKbnCd)
        {
            LogUtility.DebugMethodStart(nyuushukkinKbnCd);

            M_NYUUSHUKKIN_KBN ret = null;

            var dao = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();
            ret = dao.GetDataByCd(nyuushukkinKbnCd);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入金先エンティティを取得します
        /// </summary>
        /// <param name="nyuukinsakiCd">入金先CD</param>
        /// <returns>入金先エンティティ</returns>
        internal M_NYUUKINSAKI GetNyuukinsaki(string nyuukinsakiCd)
        {
            LogUtility.DebugMethodStart(nyuukinsakiCd);

            M_NYUUKINSAKI ret = null;

            var dao = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
            ret = dao.GetDataByCd(nyuukinsakiCd);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 取引先エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先エンティティ</returns>
        internal M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            M_TORIHIKISAKI ret = null;
            catchErr = true;

            try
            {
                var torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                ret = torihikisakiDao.GetAllValidData(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = torihikisakiCd }).FirstOrDefault();
                if (ret == null)
                {
                    LogUtility.DebugMethodEnd(ret, catchErr);
                    return ret;
                }

                SqlDateTime tekiyouDate = this.parentForm.sysDate;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.DENPYOU_DATE.Text) && DateTime.TryParse(this.form.DENPYOU_DATE.Text, out date))
                {
                    tekiyouDate = date;
                }
                if (ret.TEKIYOU_BEGIN.IsNull && ret.TEKIYOU_END.IsNull)
                {
                    return ret;
                }
                else if (ret.TEKIYOU_BEGIN.IsNull && !ret.TEKIYOU_END.IsNull
                    && tekiyouDate.CompareTo(ret.TEKIYOU_END) <= 0)
                {
                    return ret;
                }
                else if (!ret.TEKIYOU_BEGIN.IsNull && ret.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(ret.TEKIYOU_BEGIN) >= 0)
                {
                    return ret;
                }
                else if (!ret.TEKIYOU_BEGIN.IsNull && !ret.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(ret.TEKIYOU_BEGIN) >= 0
                        && tekiyouDate.CompareTo(ret.TEKIYOU_END) <= 0)
                {
                    return ret;
                }
                else
                {
                    ret = null;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 削除・適用期間外も含めた取引先エンティティを取得します。
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先エンティティ</returns>
        internal M_TORIHIKISAKI GetTorihikisakiAll(string torihikisakiCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            M_TORIHIKISAKI ret = null;
            catchErr = true;

            try
            {
                var torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                ret = torihikisakiDao.GetAllValidData(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = torihikisakiCd, ISNOT_NEED_DELETE_FLG = true }).FirstOrDefault();
                if (ret == null)
                {
                    LogUtility.DebugMethodEnd(ret, catchErr);
                    return ret;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 取引先CDで取引先請求エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先請求エンティティ</returns>
        internal M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuuByTorihikisakicd(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            M_TORIHIKISAKI_SEIKYUU ret = null;

            var torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            ret = torihikisakiSeikyuuDao.GetTorihikisakiSeikyuuByTorihikisakiCd(torihikisakiCd);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 取引先CDと入金先CDで取引先請求エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="nyuukinsakiCd">入金先CD</param>
        /// <returns>取引先請求エンティティ</returns>
        internal M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuuByTorihikisakiCdAndNyuukinsakiCd(string torihikisakiCd, string nyuukinsakiCd, out bool catchErr)
        {
            catchErr = true;
            LogUtility.DebugMethodStart(torihikisakiCd, nyuukinsakiCd);

            M_TORIHIKISAKI_SEIKYUU ret = null;
            try
            {
                var torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                ret = torihikisakiSeikyuuDao.GetTorihikisakiSeikyuuByTorihikisakiCdAndNyuukinsakiCd(torihikisakiCd, nyuukinsakiCd);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisakiSeikyuuByTorihikisakiCdAndNyuukinsakiCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisakiSeikyuuByTorihikisakiCdAndNyuukinsakiCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 入金先CDで取引先請求エンティティリストを取得します
        /// </summary>
        /// <param name="nyuukinsakiCd">入金先CD</param>
        /// <param name="denpyouDate">伝票日付</param>
        /// <param name="catchErrr"></param>
        /// <returns>取引先請求エンティティリスト</returns>
        internal List<M_TORIHIKISAKI_SEIKYUU> GetTorihikisakiSeikyuuListByNyuukinsakiCdAndDenpyouDate(string nyuukinsakiCd, DateTime denpyouDate, out bool catchErrr)
        {
            LogUtility.DebugMethodStart(nyuukinsakiCd, denpyouDate);

            List<M_TORIHIKISAKI_SEIKYUU> ret = new List<M_TORIHIKISAKI_SEIKYUU>();
            catchErrr = true;
            try
            {
                var torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();

                ret = torihikisakiSeikyuuDao.GetTorihikisakiSeikyuuListByNyuukinsakiCdAndDenpyouDate(nyuukinsakiCd, denpyouDate);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisakiSeikyuuListByNyuukinsakiCdAndDenpyouDate", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErrr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisakiSeikyuuListByNyuukinsakiCdAndDenpyouDate", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErrr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErrr);

            return ret;
        }

        /// <summary>
        /// 基準の入金番号より前で最大の入金番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の入金番号</param>
        /// <returns>入金番号</returns>
        internal SqlInt64 GetPrevNyuukinNumber(SqlInt64 nyuukinNumber, out bool catchErr)
        {
            LogUtility.DebugMethodStart(nyuukinNumber);

            var ret = SqlInt64.Null;
            catchErr = true;

            try
            {
                var dao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_ENTRYDao>();
                var number = String.Empty;
                if (false == nyuukinNumber.IsNull)
                {
                    number = nyuukinNumber.Value.ToString();
                }
                string kyotenCd = this.headerForm.txtKyotenCd.Text;
                var nyuukinSumEntry = dao.GetPrevNyuukinNumber(number, kyotenCd);
                if (null != nyuukinSumEntry)
                {
                    ret = nyuukinSumEntry.NYUUKIN_NUMBER;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetPrevNyuukinNumber", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPrevNyuukinNumber", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 基準の入金番号より後で最小の入金番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の入金番号</param>
        /// <returns>入金番号</returns>
        internal SqlInt64 GetNextNyuukinNumber(SqlInt64 nyuukinNumber, out bool catchErr)
        {
            LogUtility.DebugMethodStart(nyuukinNumber);

            var ret = SqlInt64.Null;
            catchErr = true;

            try
            {
                var dao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_ENTRYDao>();
                var number = String.Empty;
                if (false == nyuukinNumber.IsNull)
                {
                    number = nyuukinNumber.Value.ToString();
                }
                string kyotenCd = this.headerForm.txtKyotenCd.Text;
                var nyuukinSumEntry = dao.GetNextNyuukinNumber(number, kyotenCd);
                if (null != nyuukinSumEntry)
                {
                    ret = nyuukinSumEntry.NYUUKIN_NUMBER;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetNextNyuukinNumber", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNyuukinNumber", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

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

            var nyuukinSumEntryDao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_ENTRYDao>();
            var nyuukinSumDetailDao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_DETAILDao>();
            var nyuukinEntryDao = DaoInitUtility.GetComponent<IT_NYUUKIN_ENTRYDao>();
            var nyuukinDetailDao = DaoInitUtility.GetComponent<IT_NYUUKIN_DETAILDao>();
            var nyuukinKeshikomiDao = DaoInitUtility.GetComponent<IT_NYUUKIN_KESHIKOMIDao>();
            var kariukeChouseiDao = DaoInitUtility.GetComponent<IT_KARIUKE_CHOUSEIDao>();
            var kariukeControlDao = DaoInitUtility.GetComponent<IT_KARIUKE_CONTROLDao>();

            this.dto = new DTOClass();

            if (seq.IsNull)
            {
                this.dto.NyuukinSumEntry = nyuukinSumEntryDao.GetNyuukinSumEntry(new T_NYUUKIN_SUM_ENTRY() { NYUUKIN_NUMBER = nyuukinNumber, DELETE_FLG = false });
            }
            else
            {
                this.dto.NyuukinSumEntry = nyuukinSumEntryDao.GetNyuukinSumEntry(new T_NYUUKIN_SUM_ENTRY() { NYUUKIN_NUMBER = nyuukinNumber, SEQ = seq });
            }
            if (null != this.dto.NyuukinSumEntry)
            {
                this.dto.NyuukinSumDetailList = nyuukinSumDetailDao.GetNyuukinSumDetailList(new T_NYUUKIN_SUM_DETAIL() { SYSTEM_ID = this.dto.NyuukinSumEntry.SYSTEM_ID, SEQ = this.dto.NyuukinSumEntry.SEQ });
                this.dto.NyuukinEntryList = nyuukinEntryDao.GetNyuukinEntryList(new T_NYUUKIN_ENTRY() { NYUUKIN_SUM_SYSTEM_ID = this.dto.NyuukinSumEntry.SYSTEM_ID, DELETE_FLG = false });
                foreach (var entity in this.dto.NyuukinEntryList)
                {
                    //20150831 thongh 利用履歴管理　参照ボタン押下時に表示される伝票は、選択した伝票のもの（履歴）#12577 start
                    if (seq.IsNull)
                    {
                        this.dto.NyuukinDetailList.AddRange(nyuukinDetailDao.GetNyuukinDetailList(new T_NYUUKIN_DETAIL() { SYSTEM_ID = entity.SYSTEM_ID, SEQ = entity.SEQ }));
                    }
                    else
                    {
                        this.dto.NyuukinDetailList.AddRange(nyuukinDetailDao.GetNyuukinDetailList(new T_NYUUKIN_DETAIL() { SYSTEM_ID = entity.SYSTEM_ID, SEQ = seq }));
                    }
                    //20150831 thongh 利用履歴管理　参照ボタン押下時に表示される伝票は、選択した伝票のもの（履歴）#12577 end
                }
                this.dto.NyuukinKeshikomiList = nyuukinKeshikomiDao.GetNyuukinKeshikomiByNyuukinNumber(nyuukinNumber);
                this.dto.KariukeChousei = kariukeChouseiDao.GetKariukeChousei(new T_KARIUKE_CHOUSEI() { SYSTEM_ID = this.dto.NyuukinSumEntry.SYSTEM_ID, SEQ = this.dto.NyuukinSumEntry.SEQ, DELETE_FLG = false });
                this.dto.KariukeControl = kariukeControlDao.GetKariukekinByNyukinSakiCd(this.dto.NyuukinSumEntry.NYUUKINSAKI_CD);
                this.dto.BeforeKariukeControl = kariukeControlDao.GetKariukekinByNyukinSakiCd(this.dto.NyuukinSumEntry.NYUUKINSAKI_CD);

                this.NyuukinsakiCd = this.dto.NyuukinSumEntry.NYUUKINSAKI_CD;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金消込リストを再読込します
        /// </summary>
        internal bool ReloadNyuukinKeshikomiList()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var nyuukinKeshikomiDao = DaoInitUtility.GetComponent<IT_NYUUKIN_KESHIKOMIDao>();
                this.dto.NyuukinKeshikomiList = nyuukinKeshikomiDao.GetNyuukinKeshikomiByNyuukinNumber(this.GetNyuukinNumber());
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ReloadNyuukinKeshikomiList", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ReloadNyuukinKeshikomiList", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 仮受金管理を取得します
        /// </summary>
        /// <param name="nyuukinsakiCd">入金先CD</param>
        /// <returns>仮受金管理エンティティ</returns>
        internal T_KARIUKE_CONTROL GetKariukeControl(string nyuukinsakiCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(nyuukinsakiCd);

            T_KARIUKE_CONTROL ret = null;
            catchErr = true;

            try
            {
                var dao = DaoInitUtility.GetComponent<IT_KARIUKE_CONTROLDao>();
                ret = dao.GetKariukekinByNyukinSakiCd(nyuukinsakiCd);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetKariukeControl", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetKariukeControl", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        #endregion

        #region データ登録処理

        /// <summary>
        /// 登録するエンティティを作成します
        /// </summary>
        /// <returns></returns>
        internal DTOClass CreateEntity()
        {
            LogUtility.DebugMethodStart();

            var ret = this.dto.CloneDto(this.dto);
            ret.NyuukinSumDetailList.Clear();
            ret.NyuukinEntryList.Clear();
            ret.NyuukinDetailList.Clear();

            // 入金一括入力エンティティ作成
            var newNyuukinSumEntryBinderLogic = new DataBinderLogic<T_NYUUKIN_SUM_ENTRY>(ret.NyuukinSumEntry);
            newNyuukinSumEntryBinderLogic.SetSystemProperty(ret.NyuukinSumEntry, false);

            if (ret.NyuukinSumEntry.SYSTEM_ID.IsNull)
            {
                ret.NyuukinSumEntry.SYSTEM_ID = this.CreateSystemId();
                ret.NyuukinSumEntry.SEQ = 1;
                ret.NyuukinSumEntry.NYUUKIN_NUMBER = this.CreateNyuukinNumber();
                ret.NyuukinSumEntry.TORI_KOMI_KBN = false;
                ret.NyuukinSumEntry.JIDO_KESHIKOMI_KBN = false;
            }
            else
            {
                ret.NyuukinSumEntry.SEQ = ret.NyuukinSumEntry.SEQ + 1;

                // 作成情報は更新しない
                ret.NyuukinSumEntry.CREATE_USER = this.dto.NyuukinSumEntry.CREATE_USER;
                ret.NyuukinSumEntry.CREATE_DATE = this.dto.NyuukinSumEntry.CREATE_DATE;
                ret.NyuukinSumEntry.CREATE_PC = this.dto.NyuukinSumEntry.CREATE_PC;
                ret.NyuukinSumEntry.JIDO_KESHIKOMI_KBN = this.dto.NyuukinSumEntry.JIDO_KESHIKOMI_KBN;
                ret.NyuukinSumEntry.TORI_KOMI_KBN = this.dto.NyuukinSumEntry.TORI_KOMI_KBN;
            }
            ret.NyuukinSumEntry.KYOTEN_CD = this.ConvertToSqlInt16(this.headerForm.txtKyotenCd.Text);
            ret.NyuukinSumEntry.DENPYOU_DATE = (DateTime)this.form.DENPYOU_DATE.Value;
            ret.NyuukinSumEntry.NYUUKINSAKI_CD = this.form.NYUUKINSAKI_CD.Text;
            if (!String.IsNullOrEmpty(this.form.BANK_CD.Text))
            {
                ret.NyuukinSumEntry.BANK_CD = this.form.BANK_CD.Text;
            }
            else
            {
                ret.NyuukinSumEntry.BANK_CD = null;
            }
            if (!String.IsNullOrEmpty(this.form.BANK_SHITEN_CD.Text))
            {
                ret.NyuukinSumEntry.BANK_SHITEN_CD = this.form.BANK_SHITEN_CD.Text;
            }
            else
            {
                ret.NyuukinSumEntry.BANK_SHITEN_CD = null;
            }
            if (!String.IsNullOrEmpty(this.form.KOUZA_SHURUI.Text))
            {
                ret.NyuukinSumEntry.KOUZA_SHURUI = this.form.KOUZA_SHURUI.Text;
            }
            else
            {
                ret.NyuukinSumEntry.KOUZA_SHURUI = null;
            }
            if (!String.IsNullOrEmpty(this.form.KOUZA_NO.Text))
            {
                ret.NyuukinSumEntry.KOUZA_NO = this.form.KOUZA_NO.Text;
            }
            else
            {
                ret.NyuukinSumEntry.KOUZA_NO = null;
            }
            if (!String.IsNullOrEmpty(this.form.KOUZA_NAME.Text))
            {
                ret.NyuukinSumEntry.KOUZA_NAME = this.form.KOUZA_NAME.Text;
            }
            else
            {
                ret.NyuukinSumEntry.KOUZA_NAME = null;
            }
            ret.NyuukinSumEntry.EIGYOU_TANTOUSHA_CD = null;
            ret.NyuukinSumEntry.DENPYOU_BIKOU = null;
            ret.NyuukinSumEntry.NYUUKIN_AMOUNT_TOTAL = this.ConvertToDecimal(this.form.NYUUKIN_AMOUNT_TOTAL.Text);
            ret.NyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL = this.ConvertToDecimal(this.form.CHOUSEI_AMOUNT_TOTAL.Text);
            ret.NyuukinSumEntry.KARIUKEKIN_WARIATE_TOTAL = this.ConvertToDecimal(this.form.KARIUKEKIN_WARIATE_TOTAL);
            ret.NyuukinSumEntry.SEISAN_SOUSAI_CREATE_KBN = SqlBoolean.False;

            // 入金一括明細エンティティ作成
            SqlInt16 nyuukinSumDetailRowNumber = 1;
            var sumDetailRow = this.form.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow);
            foreach (DataGridViewRow row in sumDetailRow)
            {
                if (row.IsNewRow || string.IsNullOrEmpty(Convert.ToString(row.Cells["KINGAKU"].Value)))
                {
                    continue;
                }
                var nyuukinSumDetail = new T_NYUUKIN_SUM_DETAIL();
                nyuukinSumDetail.SYSTEM_ID = ret.NyuukinSumEntry.SYSTEM_ID;
                nyuukinSumDetail.SEQ = ret.NyuukinSumEntry.SEQ;
                nyuukinSumDetail.DETAIL_SYSTEM_ID = this.CreateSystemId();
                nyuukinSumDetail.ROW_NUMBER = nyuukinSumDetailRowNumber.Value;
                nyuukinSumDetail.NYUUSHUKKIN_KBN_CD = this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD"].Value);
                nyuukinSumDetail.KINGAKU = this.ConvertToSqlDecimal(row.Cells["KINGAKU"].Value);
                nyuukinSumDetail.MEISAI_BIKOU = this.ConvertToString(row.Cells["MEISAI_BIKOU"].Value);

                ret.NyuukinSumDetailList.Add(nyuukinSumDetail);
                nyuukinSumDetailRowNumber = nyuukinSumDetailRowNumber + 1;
            }

            // 入金入力エンティティ作成
            var entryRow = this.form.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && r.ReadOnly == false && this.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);
            foreach (DataGridViewRow row in entryRow)
            {
                // 入金入力エンティティを作成するかのフラグ
                var isRegist = false;

                var columnCount = this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value;
                for (int i = 1; i <= columnCount; i++)
                {
                    // ひとつでも有効なデータがあれば登録対象とする
                    if (!this.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()].Value) || !this.IsNullOrEmpty(row.Cells["NYUUKIN_KINGAKU_" + i.ToString()].Value))
                    {
                        isRegist = true;
                        break;
                    }
                }

                if (!isRegist)
                {
                    continue;
                }

                var nyuukinEntry = new T_NYUUKIN_ENTRY();

                var systemId = row.Cells["SYSTEM_ID"].Value;
                if (null == systemId)
                {
                    // 新規
                    var newNyuukinEntryBinderLogic = new DataBinderLogic<T_NYUUKIN_ENTRY>(nyuukinEntry);
                    newNyuukinEntryBinderLogic.SetSystemProperty(nyuukinEntry, false);

                    nyuukinEntry.SYSTEM_ID = this.CreateSystemId();
                    nyuukinEntry.SEQ = ret.NyuukinSumEntry.SEQ;
                }
                else
                {
                    // 更新時は元のエンティティを複製して使う
                    var originalEntity = this.dto.NyuukinEntryList.Where(n => (n.SYSTEM_ID == this.ConvertToSqlInt64(systemId)).IsTrue).FirstOrDefault();
                    nyuukinEntry = this.dto.CloneNyuukinEntry(originalEntity);

                    var newNyuukinEntryBinderLogic = new DataBinderLogic<T_NYUUKIN_ENTRY>(nyuukinEntry);
                    newNyuukinEntryBinderLogic.SetSystemProperty(nyuukinEntry, false);

                    // 作成情報は更新しない
                    nyuukinEntry.CREATE_PC = originalEntity.CREATE_PC;
                    nyuukinEntry.CREATE_DATE = originalEntity.CREATE_DATE;
                    nyuukinEntry.CREATE_USER = originalEntity.CREATE_USER;

                    nyuukinEntry.SEQ = nyuukinEntry.SEQ + 1;
                }
                nyuukinEntry.KYOTEN_CD = ret.NyuukinSumEntry.KYOTEN_CD;
                nyuukinEntry.NYUUKIN_SUM_SYSTEM_ID = ret.NyuukinSumEntry.SYSTEM_ID;
                nyuukinEntry.NYUUKIN_NUMBER = ret.NyuukinSumEntry.NYUUKIN_NUMBER;
                nyuukinEntry.DENPYOU_DATE = ret.NyuukinSumEntry.DENPYOU_DATE;
                nyuukinEntry.TORIHIKISAKI_CD = this.ConvertToString(row.Cells["TORIHIKISAKI_CD"].Value);
                nyuukinEntry.NYUUKINSAKI_CD = ret.NyuukinSumEntry.NYUUKINSAKI_CD;
                nyuukinEntry.BANK_CD = ret.NyuukinSumEntry.BANK_CD;
                nyuukinEntry.BANK_SHITEN_CD = ret.NyuukinSumEntry.BANK_SHITEN_CD;
                nyuukinEntry.KOUZA_SHURUI = ret.NyuukinSumEntry.KOUZA_SHURUI;
                nyuukinEntry.KOUZA_NO = ret.NyuukinSumEntry.KOUZA_NO;
                nyuukinEntry.KOUZA_NAME = ret.NyuukinSumEntry.KOUZA_NAME;
                nyuukinEntry.EIGYOU_TANTOUSHA_CD = ret.NyuukinSumEntry.EIGYOU_TANTOUSHA_CD;
                nyuukinEntry.KARIUKEKIN = this.ConvertToSqlDecimal(this.form.KARIUKEKIN_WARIATE_TOTAL.Text);
                nyuukinEntry.DENPYOU_BIKOU = ret.NyuukinSumEntry.DENPYOU_BIKOU;
                nyuukinEntry.NYUUKIN_AMOUNT_TOTAL = this.CalcNyuukinAmountTotal(row);
                nyuukinEntry.CHOUSEI_AMOUNT_TOTAL = this.CalcChouseiAmountTotal(row) + this.CalcKariukekinWariateTotal(row);
                nyuukinEntry.KARIUKEKIN_WARIATE_TOTAL = this.CalcKariukekinWariateTotal(row);
                nyuukinEntry.CHOUSEI_DENPYOU_KBN = SqlBoolean.False;
                nyuukinEntry.TOK_INPUT_KBN = SqlBoolean.True;

                ret.NyuukinEntryList.Add(nyuukinEntry);

                // 入金明細エンティティ作成
                SqlInt16 nyuukinDetailRowNumber = 1;
                for (int i = 1; i <= columnCount; i++)
                {
                    if (!this.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()].Value) && !this.IsNullOrEmpty(row.Cells["NYUUKIN_KINGAKU_" + i.ToString()].Value))
                    {
                        var nyuukinDetail = new T_NYUUKIN_DETAIL();
                        nyuukinDetail.SYSTEM_ID = nyuukinEntry.SYSTEM_ID;
                        nyuukinDetail.SEQ = nyuukinEntry.SEQ;
                        nyuukinDetail.DETAIL_SYSTEM_ID = this.CreateSystemId();
                        nyuukinDetail.ROW_NUMBER = nyuukinDetailRowNumber;
                        nyuukinDetail.NYUUSHUKKIN_KBN_CD = this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()].Value);
                        nyuukinDetail.KINGAKU = this.ConvertToSqlDecimal(row.Cells["NYUUKIN_KINGAKU_" + i.ToString()].Value);
                        nyuukinDetail.MEISAI_BIKOU = this.ConvertToString(row.Cells["BIKOU_" + i.ToString()].Value);

                        ret.NyuukinDetailList.Add(nyuukinDetail);
                        nyuukinDetailRowNumber = nyuukinDetailRowNumber + 1;
                    }
                }
            }

            // 仮受金調整エンティティ作成
            if (ret.KariukeChousei.SYSTEM_ID.IsNull)
            {
                ret.KariukeChousei.SYSTEM_ID = ret.NyuukinSumEntry.SYSTEM_ID;
            }
            ret.KariukeChousei.SEQ = ret.NyuukinSumEntry.SEQ;
            ret.KariukeChousei.NYUUKIN_NUMBER = ret.NyuukinSumEntry.NYUUKIN_NUMBER;
            ret.KariukeChousei.NYUUKINSAKI_CD = ret.NyuukinSumEntry.NYUUKINSAKI_CD;
            ret.KariukeChousei.KINGAKU = this.ConvertToDecimal(this.form.KARIUKEKIN_JUUTOU.Text);
            ret.KariukeChousei.DELETE_FLG = false;

            // 仮受金管理エンティティ更新
            var totalKariukekin = this.ConvertToDecimal(this.form.KARIUKEKIN_TOTAL.Text);

            if (null != ret.KariukeControl)
            {
                ret.KariukeControl.NYUUKINSAKI_CD = ret.NyuukinSumEntry.NYUUKINSAKI_CD;
                ret.KariukeControl.KARIUKE_TOTAL_KINGAKU = totalKariukekin;

                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType)
                {
                    // 入金先が変更された場合は、変更前の仮受金管理を更新する
                    if (ret.KariukeControl.NYUUKINSAKI_CD != this.dto.BeforeKariukeControl.NYUUKINSAKI_CD)
                    {
                        ret.BeforeKariukeControl.KARIUKE_TOTAL_KINGAKU = this.dto.BeforeKariukeControl.KARIUKE_TOTAL_KINGAKU.Value - this.dto.KariukeChousei.KINGAKU.Value;
                    }
                    else
                    {
                        ret.BeforeKariukeControl = null;
                    }
                }
            }

            // 元のエンティティを削除するために DELETE_FLG を更新
            if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType || WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType)
            {
                this.dto.NyuukinSumEntry.DELETE_FLG = true;
                // 20141118 Houkakou 「更新日、登録日の見直し」 start
                //var deleteNyuukinSumEntryBinderLogic = new DataBinderLogic<T_NYUUKIN_SUM_ENTRY>(this.dto.NyuukinSumEntry);
                //deleteNyuukinSumEntryBinderLogic.SetSystemProperty(this.dto.NyuukinSumEntry, false);
                // 20141118 Houkakou 「更新日、登録日の見直し」 end

                foreach (var entity in this.dto.NyuukinEntryList)
                {
                    entity.DELETE_FLG = true;
                    // 20141118 Houkakou 「更新日、登録日の見直し」 start
                    //var deleteNyuukinEntryBinderLogic = new DataBinderLogic<T_NYUUKIN_ENTRY>(entity);
                    //deleteNyuukinEntryBinderLogic.SetSystemProperty(entity, false);
                    // 20141118 Houkakou 「更新日、登録日の見直し」 end
                }

                if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType)
                {
                    foreach (var entity in this.dto.NyuukinKeshikomiList)
                    {
                        entity.DELETE_FLG = true;
                        var deleteNyuukinKeshikomiBinderLogic = new DataBinderLogic<T_NYUUKIN_KESHIKOMI>(entity);
                        deleteNyuukinKeshikomiBinderLogic.SetSystemProperty(entity, false);
                    }
                }

                if (null != this.dto.KariukeChousei)
                {
                    this.dto.KariukeChousei.DELETE_FLG = true;
                }
                if (null != this.dto.KariukeControl && null != this.dto.KariukeChousei && WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType)
                {
                    // 仮受金管理を元に戻す
                    this.dto.KariukeControl.KARIUKE_TOTAL_KINGAKU = this.dto.KariukeControl.KARIUKE_TOTAL_KINGAKU.Value - this.dto.KariukeChousei.KINGAKU.Value;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// エンティティを登録します
        /// </summary>
        internal bool RegistEntity()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 必須チェック
                var autoCheckLogic = new AutoRegistCheckLogic(this.form.allControl, this.form.allControl);
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                // 入金一覧行に入力が無い場合はエラー
                if (!this.form.RegistErrorFlag && this.form.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Count == 1)
                {
                    DataGridViewRow row = this.form.NYUUKIN_SUM_DETAIL_Ichiran.Rows[this.form.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Count - 1];
                    if (row.IsNewRow
                        && (string.IsNullOrEmpty(Convert.ToString(row.Cells["NYUUSHUKKIN_KBN_CD"].Value)) || string.IsNullOrEmpty(Convert.ToString(row.Cells["KINGAKU"].Value))))
                    {
                        msgLogic.MessageBoxShow("E001", "入金区分および入金額");
                        this.form.RegistErrorFlag = true;
                    }
                }

                if (!this.form.RegistErrorFlag)
                {
                    // 月次チェック
                    GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                    if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        // 修正は伝票日付が変更されている可能性があるため変更前データと違う場合は画面起動から
                        // 登録までの間に月次処理が行われていないか確認する。
                        // 上記が問題なければ現在表示されている変更後の日付が月次処理期間内かをチェックする
                        DateTime beforeDate = DateTime.Parse(this.dto.NyuukinSumEntry.DENPYOU_DATE.ToString());
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
                            msgLogic.MessageBoxShow("E224", "修正");
                            this.form.RegistErrorFlag = true;
                        }
                    }
                    else
                    {
                        // 新規、削除は画面に表示されている伝票日付を使用
                        DateTime getsujiShoriCheckDate = DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                        // 月次処理中チェック
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
                        // 月次処理ロックチェック
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
                    var seikyuuDetailList = this.GetSeikyuuDetailListByNyuukinNumber(this.dto.NyuukinSumEntry.NYUUKIN_NUMBER);
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

                if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType && false == this.form.RegistErrorFlag)
                {
                    // 入金明細一覧をバリデート
                    this.form.RegistErrorFlag = this.CheckNyuukinIchiran(false);
                }

                if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType && false == this.form.RegistErrorFlag)
                {
                    // 金額をバリデート
                    var total = this.ConvertToDecimal(this.form.TOTAL.Text);
                    var warifuriTotal = this.ConvertToDecimal(this.form.WARIFURIGAKU.Text);
                    if (total < warifuriTotal)
                    {
                        this.form.RegistErrorFlag = true;

                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E182");
                    }
                }

                // 20141112 Houkakou 「入金入力」の締済期間チェックの追加 start
                if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType && false == this.form.RegistErrorFlag)
                {
                    if (!this.SeikyuuDateCheck())
                    {
                        this.form.RegistErrorFlag = true;
                    }
                }
                // 20141112 Houkakou 「入金入力」の締済期間チェックの追加 end

                ///仮受金に変動がないかチェックをする。（同時更新の排他的制御を行う）
                if (!this.form.RegistErrorFlag)
                {
                    if (!this.CheckNoUpdatedKariukeControl())
                    {
                        this.form.RegistErrorFlag = true;
                    }
                }

                if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType && false == this.form.RegistErrorFlag)
                {
                    var kariukekinWariateTotal = this.ConvertToDecimal(this.form.KARIUKEKIN_WARIATE_TOTAL.Text);
                    var kariukekin = this.ConvertToDecimal(this.form.KARIUKEKIN.Text);
                    var kariukekinTotal = this.ConvertToDecimal(this.form.KARIUKEKIN_TOTAL.Text);

                    var isError = false;

                    //仮受金合計がマイナスの場合はエラー
                    if (0 > kariukekinTotal)
                    {
                        isError = true;
                    }

                    if (isError)
                    {
                        this.form.RegistErrorFlag = true;

                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E183");
                    }
                }

                if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType && false == this.form.RegistErrorFlag)
                {
                    // 仮受金をバリデート（借受金がマイナスの場合は、伝票削除できない）
                    var kariukekin = this.ConvertToDecimal(this.form.KARIUKEKIN.Text);
                    if (0 > kariukekin)
                    {
                        this.form.RegistErrorFlag = true;

                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E184");
                    }
                }

                if (false == this.form.RegistErrorFlag)
                {
                    var targetRow = this.form.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && this.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);

                    foreach (DataGridViewRow row in targetRow)
                    {
                        if (false == this.IsNullOrEmpty(row.Cells["TORIHIKISAKI_CD"].Value))
                        {
                            if (this.form.JudgeTorihikisakiKeshikomi(row.Cells["TORIHIKISAKI_CD"].Value.ToString()))
                            {
                                var columnCount = this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value;
                                var cnt = 0;
                                for (int i = 1; i <= columnCount; i++)
                                {
                                    if (true == this.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()].Value) && true == this.IsNullOrEmpty(row.Cells["NYUUKIN_KINGAKU_" + i.ToString()].Value))
                                    {
                                        cnt++;
                                    }
                                }

                                if (cnt == columnCount)
                                {
                                    var messageLogic = new MessageBoxShowLogic();
                                    messageLogic.MessageBoxShow("E233", "入金区分と入金金額");
                                    this.form.RegistErrorFlag = true;
                                    break;
                                }
                            }
                        }
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

                if (false == this.form.RegistErrorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        var registDto = this.CreateEntity();
                        var nyuukinSumEntryDao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_ENTRYDao>();
                        var nyuukinSumDetailDao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_DETAILDao>();
                        var nyuukinEntryDao = DaoInitUtility.GetComponent<IT_NYUUKIN_ENTRYDao>();
                        var nyuukinDetailDao = DaoInitUtility.GetComponent<IT_NYUUKIN_DETAILDao>();
                        var nyuukinKeshikomiDao = DaoInitUtility.GetComponent<IT_NYUUKIN_KESHIKOMIDao>();
                        var kariukeChouseiDao = DaoInitUtility.GetComponent<IT_KARIUKE_CHOUSEIDao>();
                        var kariukeControlDao = DaoInitUtility.GetComponent<IT_KARIUKE_CONTROLDao>();

                        // 以前のレコードを削除
                        if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType || WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType)
                        {
                            nyuukinSumEntryDao.Update(this.dto.NyuukinSumEntry);
                            // 20141118 Houkakou 「更新日、登録日の見直し」 start
                            if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType)
                            {
                                this.dto.NyuukinSumEntry.SEQ = this.dto.NyuukinSumEntry.SEQ + 1;
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //this.dto.NyuukinSumEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                this.dto.NyuukinSumEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                this.dto.NyuukinSumEntry.UPDATE_PC = SystemInformation.ComputerName;
                                this.dto.NyuukinSumEntry.UPDATE_USER = SystemProperty.UserName;
                                nyuukinSumEntryDao.Insert(this.dto.NyuukinSumEntry);

                                foreach (var entity in this.dto.NyuukinSumDetailList)
                                {
                                    entity.SEQ = this.dto.NyuukinSumEntry.SEQ;
                                    nyuukinSumDetailDao.Insert(entity);
                                }
                            }

                            // 20141118 Houkakou 「更新日、登録日の見直し」 end
                            foreach (var entity in this.dto.NyuukinEntryList)
                            {
                                nyuukinEntryDao.Update(entity);
                                // 20141118 Houkakou 「更新日、登録日の見直し」 start
                                if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType)
                                {
                                    entity.SEQ = entity.SEQ + 1;
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                    entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    entity.UPDATE_PC = SystemInformation.ComputerName;
                                    entity.UPDATE_USER = SystemProperty.UserName;
                                    nyuukinEntryDao.Insert(entity);
                                }
                                // 20141118 Houkakou 「更新日、登録日の見直し」 end
                            }
                            // 20141118 Houkakou 「更新日、登録日の見直し」 start
                            if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType)
                            {
                                foreach (var Detail in this.dto.NyuukinDetailList)
                                {
                                    Detail.SEQ = Detail.SEQ + 1;
                                    nyuukinDetailDao.Insert(Detail);
                                }
                            }
                            // 20141118 Houkakou 「更新日、登録日の見直し」 end
                            if (!this.ReloadNyuukinKeshikomiList())
                            {
                                ret = false;
                                return ret;
                            }
                            foreach (var entity in this.dto.NyuukinKeshikomiList)
                            {
                                if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                                {
                                    entity.DELETE_FLG = true;
                                }
                                nyuukinKeshikomiDao.Update(entity);
                            }
                            if (null != this.dto.KariukeChousei)
                            {
                                kariukeChouseiDao.Update(this.dto.KariukeChousei);
                            }
                            // 20141118 Houkakou 「更新日、登録日の見直し」 start
                            if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType)
                            {
                                if (null != this.dto.KariukeChousei)
                                {
                                    this.dto.KariukeChousei.SEQ = this.dto.KariukeChousei.SEQ + 1;
                                    kariukeChouseiDao.Insert(this.dto.KariukeChousei);
                                }
                            }
                            // 20141118 Houkakou 「更新日、登録日の見直し」 end
                            if (null != this.dto.KariukeControl)
                            {
                                kariukeControlDao.Update(this.dto.KariukeControl);
                            }
                        }

                        if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.form.WindowType || WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType)
                        {
                            nyuukinSumEntryDao.Insert(registDto.NyuukinSumEntry);

                            // 入金消込ボタン押下時に使用
                            this.form.BeforeNuukinNumber = registDto.NyuukinSumEntry.NYUUKIN_NUMBER.ToString();

                            foreach (var entity in registDto.NyuukinSumDetailList)
                            {
                                nyuukinSumDetailDao.Insert(entity);
                            }
                            foreach (var entity in registDto.NyuukinEntryList)
                            {
                                nyuukinEntryDao.Insert(entity);
                            }
                            foreach (var entity in registDto.NyuukinDetailList)
                            {
                                nyuukinDetailDao.Insert(entity);
                            }
                            if (null != registDto.KariukeChousei)
                            {
                                kariukeChouseiDao.Insert(registDto.KariukeChousei);
                            }
                            var kariukeControl = kariukeControlDao.GetKariukekinByNyukinSakiCd(registDto.NyuukinSumEntry.NYUUKINSAKI_CD);
                            if (null == kariukeControl)
                            {
                                kariukeControlDao.Insert(registDto.KariukeControl);
                            }
                            else
                            {
                                registDto.KariukeControl.TIME_STAMP = kariukeControl.TIME_STAMP;
                                kariukeControlDao.Update(registDto.KariukeControl);
                            }
                            // 入金先変更時は変更前入金先の仮受金管理も更新
                            if (null != registDto.BeforeKariukeControl)
                            {
                                var beforeKariuktControl = kariukeControlDao.GetKariukekinByNyukinSakiCd(registDto.BeforeKariukeControl.NYUUKINSAKI_CD);
                                registDto.BeforeKariukeControl.TIME_STAMP = beforeKariuktControl.TIME_STAMP;
                                kariukeControlDao.Update(registDto.BeforeKariukeControl);
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

                    var formID = r_framework.FormManager.FormManager.GetFormID(System.Reflection.Assembly.GetExecutingAssembly());
                    if (r_framework.Authority.Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.SetNyuukinNumber(SqlInt64.Null);
                        this.WindowInit(false);
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
                    messageShowLogic.MessageBoxShowError("該当データは他ユーザーにより更新されています。");
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
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

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

        #endregion

        #region バリデートメソッド

        /// <summary>
        /// 入金入力一覧のバリデートを行います
        /// </summary>
        /// <returns>エラーがある場合は、True</returns>
        internal bool CheckNyuukinIchiran(bool isSubFunctionClick)
        {
            LogUtility.DebugMethodStart();

            var ret = false;

            var isErrorTorhikisakiCd = false;
            var isErrorNyuushukkinKbnCd = false;
            var isErrorKingaku = false;
            var isKingakuEmpty = false;
            IEnumerable<DataGridViewRow> targetRow;
            DataGridViewRow checkRow = null;

            if (isSubFunctionClick)
            {
                if (this.form.NYUUKIN_Ichiran.Rows.Count == 1)
                {
                    targetRow = this.form.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => this.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);
                }
                else
                {
                    if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        if (this.form.NYUUKIN_Ichiran.CurrentRow.IsNewRow)
                        {
                            targetRow = this.form.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => r.Index == this.form.NYUUKIN_Ichiran.CurrentRow.Index && this.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);
                        }
                        else
                        {
                            targetRow = this.form.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && this.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);
                        }
                        checkRow = this.form.NYUUKIN_Ichiran.CurrentRow;
                    }
                    else
                    {
                        targetRow = this.form.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && this.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);
                        if (!this.form.NYUUKIN_Ichiran.CurrentRow.IsNewRow)
                        {
                            checkRow = this.form.NYUUKIN_Ichiran.CurrentRow;
                        }
                    }
                }
            }
            else
            {
                targetRow = this.form.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && this.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);
            }
            
            foreach (DataGridViewRow row in targetRow)
            {
                if (true == this.IsNullOrEmpty(row.Cells["TORIHIKISAKI_CD"].Value))
                {
                    // 取引先CDが入力されていない
                    ((DgvCustomTextBoxCell)row.Cells["TORIHIKISAKI_CD"]).IsInputErrorOccured = true;
                    ((DgvCustomTextBoxCell)row.Cells["TORIHIKISAKI_CD"]).UpdateBackColor();
                    ret = true;
                    isErrorTorhikisakiCd = true;
                }
                else
                {
                    var columnCount = this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value;
                    var isEmpty = true;
                    
                    for (int i = 1; i <= columnCount; i++)
                    {
                        if (false == this.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()].Value) && true == this.IsNullOrEmpty(row.Cells["NYUUKIN_KINGAKU_" + i.ToString()].Value))
                        {
                            // 入出金区分が入力されていて金額が入力されていない
                            ((DgvCustomTextBoxCell)row.Cells["NYUUKIN_KINGAKU_" + i.ToString()]).IsInputErrorOccured = true;
                            ((DgvCustomTextBoxCell)row.Cells["NYUUKIN_KINGAKU_" + i.ToString()]).UpdateBackColor();
                            ret = true;
                            isErrorKingaku = true;
                            isEmpty = false;
                        }
                        else if (true == this.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()].Value) && false == this.IsNullOrEmpty(row.Cells["NYUUKIN_KINGAKU_" + i.ToString()].Value))
                        {
                            // 金額が入力されていて入出金区分が入力されていない
                            ((DgvCustomTextBoxCell)row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()]).IsInputErrorOccured = true;
                            ((DgvCustomTextBoxCell)row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()]).UpdateBackColor();
                            ret = true;
                            isErrorNyuushukkinKbnCd = true;
                            isEmpty = false;
                        }
                    }

                    if (!isEmpty)
                    {
                        isErrorNyuushukkinKbnCd = true;
                        isErrorKingaku = true;
                        for (int i = 1; i <= columnCount; i++)
                        {
                            ((DgvCustomTextBoxCell)row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()]).IsInputErrorOccured = true;
                            ((DgvCustomTextBoxCell)row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()]).UpdateBackColor();
                            ((DgvCustomTextBoxCell)row.Cells["NYUUKIN_KINGAKU_" + i.ToString()]).IsInputErrorOccured = true;
                            ((DgvCustomTextBoxCell)row.Cells["NYUUKIN_KINGAKU_" + i.ToString()]).UpdateBackColor();
                        }
                        ret = true;
                    }
                }
            }

            if (isSubFunctionClick && checkRow != null)
            {
                var columnCount = this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value;
                var cnt = 0;

                for (int i = 1; i <= columnCount; i++)
                {
                    if (true == this.IsNullOrEmpty(checkRow.Cells["NYUUKIN_KINGAKU_" + i.ToString()].Value))
                    {
                        cnt++;
                    }
                }

                if (cnt == columnCount)
                {
                    isKingakuEmpty = true;
                }
            }

            var koumoku = String.Empty;
            if (isErrorTorhikisakiCd)
            {
                koumoku += "「取引先」";
            }
            if (isErrorNyuushukkinKbnCd)
            {
                koumoku += "「入出金区分」";
            }
            if (isErrorKingaku)
            {
                koumoku += "「入金金額」";
            }

            if (ret)
            {
                var messageLogic = new MessageBoxShowLogic();
                messageLogic.MessageBoxShow("E001", koumoku);
            }

            if (!ret && isKingakuEmpty && isSubFunctionClick)
            {
                var messageLogic = new MessageBoxShowLogic();
                messageLogic.MessageBoxShowError("選択された取引先は入金額が入力されていないため、消込できません。");
                return isKingakuEmpty;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region 計算メソッド

        /// <summary>
        /// すべての金額を再計算します
        /// </summary>
        internal bool CalcAll()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.CalcNyuukinAmountTotal();
                if (!this.CalcChouseiAmountTotal())
                {
                    ret = false;
                    return ret;
                }
                if (!this.CalcKariukekinWariateTotal())
                {
                    ret = false;
                    return ret;
                }
                if (!this.CalcTotal())
                {
                    ret = false;
                    return ret;
                }
                if (!this.CalcWarifuriTotal())
                {
                    ret = false;
                    return ret;
                }
                if (!this.CalcKariukekinJuutou())
                {
                    ret = false;
                    return ret;
                }
                if (!this.CalcKariukekinTotal())
                {
                    ret = false;
                    return ret;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcAll", ex);
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
        /// 今回入金額を計算します
        /// </summary>
        internal void CalcNyuukinAmountTotal()
        {
            LogUtility.DebugMethodStart();

            // 入出金区分が20以下が対象
            var nyuukinAmountTotal = this.form.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow &&
                                                                                                                  false == this.IsNullOrEmpty(r.Cells["NYUUSHUKKIN_KBN_CD"].Value) &&
                                                                                                                      0 > this.ConvertToSqlInt16(r.Cells["NYUUSHUKKIN_KBN_CD"].Value).CompareTo(21))
                                                                                                      .Sum(r => this.ConvertToDecimal(r.Cells["KINGAKU"].Value));

            bool cacthErr = true;
            this.form.NYUUKIN_AMOUNT_TOTAL.Text = this.FormatKingaku(nyuukinAmountTotal, out cacthErr);
            if (!cacthErr)
            {
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 今回入金額を計算します（入金入力単位）
        /// </summary>
        /// <param name="row">対象の入金入力</param>
        /// <returns>今回入金額</returns>
        internal decimal CalcNyuukinAmountTotal(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            var ret = 0m;
            for (int count = 1; count <= this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value; count++)
            {
                // 入出金区分が20以下が対象
                if (false == this.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + count.ToString()].Value) &&
                    0 > this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD_" + count.ToString()].Value).CompareTo(21))
                {
                    ret += this.ConvertToDecimal(row.Cells["NYUUKIN_KINGAKU_" + count.ToString()].Value);
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 今回調整額を計算します
        /// </summary>
        internal bool CalcChouseiAmountTotal()
        {
            LogUtility.DebugMethodStart();

            // 入出金区分が21以上49以下が対象
            var chouseiAmountTotal = this.form.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow &&
                                                                                                                  false == this.IsNullOrEmpty(r.Cells["NYUUSHUKKIN_KBN_CD"].Value) &&
                                                                                                                     0 < this.ConvertToSqlInt16(r.Cells["NYUUSHUKKIN_KBN_CD"].Value).CompareTo(20) &&
                                                                                                                     0 > this.ConvertToSqlInt16(r.Cells["NYUUSHUKKIN_KBN_CD"].Value).CompareTo(51))
                                                                                                      .Sum(r => this.ConvertToDecimal(r.Cells["KINGAKU"].Value));

            bool catchErr = true;
            this.form.CHOUSEI_AMOUNT_TOTAL.Text = this.FormatKingaku(chouseiAmountTotal, out catchErr);
            if (!catchErr)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 今回調整額を計算します（入金入力単位）
        /// </summary>
        /// <param name="row">対象の入金入力</param>
        /// <returns>今回入金額</returns>
        internal decimal CalcChouseiAmountTotal(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            var ret = 0m;
            for (int count = 1; count <= this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value; count++)
            {
                // 入出金区分が21以上49以下が対象
                if (false == this.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + count.ToString()].Value) &&
                    0 < this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD_" + count.ToString()].Value).CompareTo(20) &&
                    0 > this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD_" + count.ToString()].Value).CompareTo(51))
                {
                    ret += this.ConvertToDecimal(row.Cells["NYUUKIN_KINGAKU_" + count.ToString()].Value);
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 今回仮受金を計算します
        /// </summary>
        internal bool CalcKariukekinWariateTotal()
        {
            LogUtility.DebugMethodStart();

            // 入出金区分が51が対象
            var kariukekinWariateTotal = this.form.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow &&
                                                                                                                      false == this.IsNullOrEmpty(r.Cells["NYUUSHUKKIN_KBN_CD"].Value) &&
                                                                                                                         0 == this.ConvertToSqlInt16(r.Cells["NYUUSHUKKIN_KBN_CD"].Value).CompareTo(51))
                                                                                                          .Sum(r => this.ConvertToDecimal(r.Cells["KINGAKU"].Value));

            bool catchErr = true;
            this.form.KARIUKEKIN_WARIATE_TOTAL.Text = this.FormatKingaku(kariukekinWariateTotal, out catchErr);
            if (!catchErr)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 今回仮受金を計算します（入金入力単位）
        /// </summary>
        /// <param name="row">対象の入金入力</param>
        /// <returns>今回入金額</returns>
        internal decimal CalcKariukekinWariateTotal(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            var ret = 0m;
            for (int count = 1; count <= this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value; count++)
            {
                // 入出金区分が51が対象
                if (false == this.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + count.ToString()].Value) &&
                    0 == this.ConvertToSqlInt16(row.Cells["NYUUSHUKKIN_KBN_CD_" + count.ToString()].Value).CompareTo(51))
                {
                    ret += this.ConvertToDecimal(row.Cells["NYUUKIN_KINGAKU_" + count.ToString()].Value);
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 今回合計額を計算します
        /// </summary>
        internal bool CalcTotal()
        {
            LogUtility.DebugMethodStart();

            var total = this.form.NYUUKIN_SUM_DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow &&
                                                                                                     false == this.IsNullOrEmpty(r.Cells["NYUUSHUKKIN_KBN_CD"].Value))
                                                                                         .Sum(r => this.ConvertToDecimal(r.Cells["KINGAKU"].Value));

            bool catchErr = true;
            this.form.TOTAL.Text = this.FormatKingaku(total, out catchErr);
            if (!catchErr)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 今回仮受金充当を計算します
        /// </summary>
        internal bool CalcKariukekinJuutou()
        {
            LogUtility.DebugMethodStart();

            var total = this.ConvertToDecimal(this.form.TOTAL.Text);
            var warifuri = this.ConvertToDecimal(this.form.WARIFURIGAKU.Text);
            var kariukekin = this.ConvertToDecimal(this.form.KARIUKEKIN_WARIATE_TOTAL.Text);
            bool catchErr = true;

            this.form.KARIUKEKIN_JUUTOU.Text = this.FormatKingaku(total - warifuri - kariukekin, out catchErr);
            if (!catchErr)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 今回割振額を計算します
        /// </summary>
        internal bool CalcWarifuriTotal()
        {
            LogUtility.DebugMethodStart();

            var warifurigaku = 0m;

            var targetRow = this.form.NYUUKIN_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow && this.ConvertToSqlBooleanDefaultFalse(r.Cells["DELETE_FLG"].Value).IsFalse);
            foreach (DataGridViewRow row in targetRow)
            {
                for (int count = 1; count <= this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value; count++)
                {
                    warifurigaku += this.ConvertToDecimal(row.Cells["NYUUKIN_KINGAKU_" + count.ToString()].Value);
                }
            }

            bool catchErr = true;
            this.form.WARIFURIGAKU.Text = this.FormatKingaku(warifurigaku, out catchErr);
            if (!catchErr)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 仮受金の累計額を計算します。
        /// </summary>
        /// <returns></returns>
        internal bool CalcKariukekinTotal()
        {
            LogUtility.DebugMethodStart();

            var jyuutou = 0m;
            var kariukekin = 0m;

            jyuutou = this.ConvertToDecimal(this.form.KARIUKEKIN_JUUTOU.Text);
            kariukekin = this.ConvertToDecimal(this.form.KARIUKEKIN.Text);
            bool catchErr = true;

            this.form.KARIUKEKIN_TOTAL.Text = this.FormatKingaku(jyuutou + kariukekin, out catchErr);
            if (!catchErr)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        #region IBuisinessLogicの実装

        public int Search()
        {
            throw new NotImplementedException();
        }

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

        // 20141112 Houkakou 「入金入力」の締済期間チェックの追加 start

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
            ReturnDate rd = new ReturnDate();
            //CheckDate cd = new CheckDate();

            string strKyotenCd = this.headerForm.txtKyotenCd.Text;
            string strDenpyouDate = this.form.DENPYOU_DATE.Text;

            if (string.IsNullOrEmpty(strDenpyouDate))
            {
                return true;
            }
            if (string.IsNullOrEmpty(strKyotenCd))
            {
                return true;
            }

            DateTime DenpyouDate = Convert.ToDateTime(strDenpyouDate);

            foreach (DataGridViewRow row in this.form.NYUUKIN_Ichiran.Rows)
            {
                if (row.IsNewRow)
                {
                    break;
                }

                CheckDate cd = new CheckDate();
                string strTorihikisakiCd = Convert.ToString(row.Cells["TORIHIKISAKI_CD"].Value);
                Boolean strNyuushukkinFlg = false;
                var columnCount = this.SysInfo.NYUUKIN_IKKATSU_KBN_DISP_SUU.Value;
                for (int i = 1; i <= columnCount; i++)
                {
                    // ひとつでも有効なデータがあれば登録対象とする
                    if (!this.IsNullOrEmpty(row.Cells["NYUUSHUKKIN_KBN_CD_" + i.ToString()].Value) || !this.IsNullOrEmpty(row.Cells["NYUUKIN_KINGAKU_" + i.ToString()].Value))
                    {
                        strNyuushukkinFlg = true;
                        break;
                    }
                }
                Boolean strDeleteFlg = Convert.ToBoolean(row.Cells["DELETE_FLG"].Value);

                if (string.IsNullOrEmpty(strTorihikisakiCd) || !strNyuushukkinFlg
                    || strDeleteFlg)
                {
                    continue;
                }

                cd.TORIHIKISAKI_CD = strTorihikisakiCd;
                cd.CHECK_DATE = DenpyouDate;
                cd.KYOTEN_CD = strKyotenCd;

                checkDate.Add(cd);
            }

            if (checkDate.Count == 0)
            {
                return true;
            }

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

        // 20141112 Houkakou 「入金入力」の締済期間チェックの追加 end

        #region 仮受金更新チェック

        /// <summary>
        /// 他社により仮受金に更新があり変動があった、またはチェック処理でエラーが発生した場合はfalseを返します。
        /// </summary>
        /// <returns>bool</returns>
        internal bool CheckNoUpdatedKariukeControl()
        {
            LogUtility.DebugMethodStart();

            bool retVal = true;

            try
            {
                bool catchErr = true;
                var kariukeControl = GetKariukeControl(this.form.NYUUKINSAKI_CD.Text, out catchErr);
                if (!catchErr)
                {
                    retVal = false;
                    return retVal;
                }
                if (null != kariukeControl)
                {
                    //表示されている[仮受金]の値。読み込み時に設定されている。
                    //比較対象[1]
                    var dispKariukekin = ConvertToDecimal(this.form.KARIUKEKIN.Text);

                    //最新データの[仮受金]の値。
                    //修正時、削除時は前の状態を差し引く。
                    //入金先CDに変更がない場合のみ差し引く。
                    //比較対象[2]
                    var dataKariukekin = ConvertToDecimal(kariukeControl.KARIUKE_TOTAL_KINGAKU.Value);

                    if (this.dto.BeforeKariukeControl == null || this.dto.BeforeKariukeControl.NYUUKINSAKI_CD == this.form.NYUUKINSAKI_CD.Text)
                    {
                        if (null != this.dto.KariukeChousei)
                        {
                            if (!this.dto.KariukeChousei.KINGAKU.IsNull)
                            {
                                dataKariukekin = dataKariukekin - this.dto.KariukeChousei.KINGAKU.Value;
                            }
                        }
                    }

                    //[1]と[2]が一緒だったら、値に変更はないはず
                    if (dispKariukekin != dataKariukekin)
                    {
                        retVal = false;

                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("I019");

                        ///読み直し＆再計算
                        this.dto.KariukeControl = kariukeControl;
                        this.form.KARIUKEKIN.Text = FormatKingaku(dataKariukekin, out catchErr);
                        if (!catchErr)
                        {
                            retVal = false;
                            return retVal;
                        }
                        if (!CalcKariukekinWariateTotal())
                        {
                            retVal = false;
                            return retVal;
                        }
                        if (!CalcKariukekinJuutou())
                        {
                            retVal = false;
                            return retVal;
                        }
                        if (!CalcKariukekinTotal())
                        {
                            retVal = false;
                            return retVal;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                retVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retVal);
            }

            return retVal;
        }

        #endregion

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        #region 入金区分チェック&設定

        /// <summary>
        /// 指定した明細の入金区分セルにおいてマスタの存在チェックおよび名称の設定を行います。
        /// </summary>
        /// <param name="isSumeDetail">True：NYUUKIN_SUM_DETAIL_Ichiran False：NYUUKIN_Ichiran</param>
        /// <param name="rowIndex">チェック対象CellのRowIndex</param>
        /// <param name="cellNameKbn">チェック対象入金区分CellのName</param>
        /// /// <param name="cellNameKbnNm">チェック対象入金区分に対応する入金区分名CellのName</param>
        /// <returns>True：正常終了　Flase：存在しないなどのエラー発生</returns>
        internal bool CheckAndSetNyuukinKbn(bool isSumeDetail, int rowIndex, string cellNameKbn, string cellNameKbnNm)
        {
            bool returnValue = true;

            DataGridViewRow row = null;
            if (isSumeDetail)
            {
                /* NYUUKIN_SUM_DETAIL_Ichiran */
                row = this.form.NYUUKIN_SUM_DETAIL_Ichiran.Rows[rowIndex];
                this.form.isNyuukinSumDetailIchiranInputError = false;
            }
            else
            {
                /* NYUUKIN_Ichiran */
                row = this.form.NYUUKIN_Ichiran.Rows[rowIndex];
                this.form.isNyuukinIchiranInputError = false;
            }

            short kbn = 0;
            string val = Convert.ToString(row.Cells[cellNameKbn].Value);
            if (short.TryParse(val, out kbn))
            {
                var dao = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();
                M_NYUUSHUKKIN_KBN search = new M_NYUUSHUKKIN_KBN();
                search.NYUUSHUKKIN_KBN_CD = kbn;
                M_NYUUSHUKKIN_KBN[] datas = dao.GetAllValidData(search);

                if (datas != null && datas.Length > 0)
                {
                    row.Cells[cellNameKbnNm].Value = datas[0].NYUUSHUKKIN_KBN_NAME_RYAKU;
                }
                else
                {
                    // エラー
                    row.Cells[cellNameKbnNm].Value = null;
                    new MessageBoxShowLogic().MessageBoxShow("E020", "入出金区分");
                    if (isSumeDetail)
                    {
                        this.form.isNyuukinSumDetailIchiranInputError = true;
                    }
                    else
                    {
                        this.form.isNyuukinIchiranInputError = true;
                    }
                    returnValue = false;
                }
            }
            else if (string.IsNullOrEmpty(val))
            {
                // 値無しの場合は名称クリア
                row.Cells[cellNameKbnNm].Value = null;
            }
            else
            {
                // ここへの到達はありえないが念のためエラー表示
                row.Cells[cellNameKbnNm].Value = null;
                new MessageBoxShowLogic().MessageBoxShow("E058");
                if (isSumeDetail)
                {
                    this.form.isNyuukinSumDetailIchiranInputError = true;
                }
                else
                {
                    this.form.isNyuukinIchiranInputError = true;
                }
                returnValue = false;
            }

            return returnValue;
        }

        #endregion
    }
}