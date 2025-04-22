using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace Shougun.Core.PayByProxy.DainoNyuryuku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class G161Logic : IBuisinessLogic
    {
        #region 変数定義

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

        private M_CORP_INFO corpInfoEntity;

        // 代納系エンティティ
        internal T_UR_SH_ENTRY dainouUkeireEntry { get; set; }
        internal T_UR_SH_ENTRY dainouShukkaEntry { get; set; }
        private List<T_UR_SH_DETAIL> dainouUkeireDetail { get; set; }
        private List<T_UR_SH_DETAIL> dainouShukkaDetail { get; set; }
        private T_UR_SH_ENTRY insDainouUkeireEntry { get; set; }
        private T_UR_SH_ENTRY insDainouShukkaEntry { get; set; }
        private List<T_UR_SH_DETAIL> insDainouUkeireDetail { get; set; }
        private List<T_UR_SH_DETAIL> insDainouShukkaDetail { get; set; }
        private T_UR_SH_ENTRY befDainouukeireEntry { get; set; }

        /// <summary>
        /// 品名マスタ検索結果
        /// </summary>
        public DataTable SearchHinmeiResult { get; set; }

        /// <summary>
        /// 受入,出荷明細変更前結果
        /// </summary>
        internal List<DainoNyuryukuDTO> BeforeDetailResultList = new List<DainoNyuryukuDTO>();

        /// <summary>
        /// SystemId
        /// </summary>
        internal long UkeireSystemId;
        /// <summary>
        /// SystemId
        /// </summary>
        internal long ShukkaSystemId;
        /// <summary>
        /// Seq
        /// </summary>
        internal int UkeireSeq;
        /// <summary>
        /// Seq
        /// </summary>
        internal int ShukkaSeq;
        /// <summary>
        /// 明細システムID
        /// </summary>
        internal long UkeireMeisaiSystemId;
        /// <summary>
        /// 明細システムID
        /// </summary>
        internal long ShukkaMeisaiSystemId;
        /// <summary>
        /// 日連番
        /// </summary>
        internal int NumberDay;
        /// <summary>
        /// 年連番
        /// </summary>
        internal int NumberYear;

        /// <summary>
        /// 消費税率
        /// </summary>
        internal decimal ukeireTaxRate;

        /// <summary>
        /// 消費税率
        /// </summary>
        internal decimal shukkaTaxRate;

        /// <summary>
        /// システム設定．売上支払情報差引基準
        /// </summary>
        internal String systemUrShCalcBaseKbn;

        /// <summary>
        /// 画面初期化時伝票日付
        /// </summary>
        internal String beforeDenpyouDate;

        /// <summary>
        /// Form
        /// </summary>
        private G161Form form;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal G161HeaderForm headerForm;

        /// <summary>
        /// 親フォーム
        /// </summary>
        internal BusinessBaseForm ParentForm;

        /// <summary>
        /// 伝票番号
        /// </summary>
        internal long PrmDainouNumber;

        /// <summary>
        /// 運賃用な連携番号
        /// </summary>
        internal long UnchinRenkeiNumber;

        /// <summary>
        /// 運賃用な連携情報
        /// </summary>
        internal T_UR_SH_ENTRY[] UnchinRenkeiInfo;

        /// <summary>
        /// 受入入力専用DBアクセッサー
        /// </summary>
        internal DBAccessor accessor;

        internal Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;

        /// <summary>
        /// 新規行数
        /// </summary>
        internal int newRowNum;

        /// <summary>
        /// 支払締済フラッグ
        /// </summary>
        internal bool ukeireShimeiCheckFlg;

        /// <summary>
        /// 売上締済フラッグ
        /// </summary>
        internal bool shukkaShimeiCheckFlg;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        internal MessageBoxShowLogic errmessage;

        /// <summary>
        /// UIFormの入力コントロール名一覧
        /// </summary>
        private string[] inputUiFormControlNames = { "DAINOU_NUMBER", "DENPYOU_DATE", "NYUURYOKU_TANTOUSHA_CD", "UNPAN_GYOUSHA_CD", "SHASHU_CD", "TAIRYUU_BIKOU", "SHARYOU_CD", "UNTENSHA_CD",
                                                     "SHIHARAI_DATE", "UKEIRE_TORIHIKISAKI_CD", "UKEIRE_GYOUSHA_CD", "UKEIRE_GENBA_CD", "UKEIRE_EIGYOU_TANTOUSHA_CD",
                                                     "URIAGE_DATE", "SHUKKA_TORIHIKISAKI_CD", "SHUKKA_GYOUSHA_CD", "SHUKKA_GENBA_CD", "SHUKKA_EIGYOU_TANTOUSHA_CD", "DENPYOU_BIKOU",
                                                     "SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON", "cbtn_TORIHIKISAKI_SEARCH_UKEIRE", "cbtn_GYOUSHA_SEARCH_UKEIRE", "cbtn_GENBA_SEARCH_UKEIRE",
                                                     "URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON", "cbtn_TORIHIKISAKI_SEARCH_SHUKKA", "cbtn_GYOUSHA_SEARCH_SHUKKA", "cbtn_GENBA_SEARCH_SHUKKA"
                                                   };

        /// <summary>
        /// UIFormの入力コントロール名一覧（参照用）
        /// </summary>
        private string[] refUiFormControlNames = { "DAINOU_NUMBER", "DENPYOU_DATE", "NYUURYOKU_TANTOUSHA_CD", "UNPAN_GYOUSHA_CD", "SHASHU_CD", "TAIRYUU_BIKOU", "SHARYOU_CD", "UNTENSHA_CD",
                                                     "SHIHARAI_DATE", "UKEIRE_TORIHIKISAKI_CD", "UKEIRE_GYOUSHA_CD", "UKEIRE_GENBA_CD", "UKEIRE_EIGYOU_TANTOUSHA_CD",
                                                     "URIAGE_DATE", "SHUKKA_TORIHIKISAKI_CD", "SHUKKA_GYOUSHA_CD", "SHUKKA_GENBA_CD", "SHUKKA_EIGYOU_TANTOUSHA_CD", "DENPYOU_BIKOU",
                                                     "SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON", "cbtn_TORIHIKISAKI_SEARCH_UKEIRE", "cbtn_GYOUSHA_SEARCH_UKEIRE", "cbtn_GENBA_SEARCH_UKEIRE",
                                                     "URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON", "cbtn_TORIHIKISAKI_SEARCH_SHUKKA", "cbtn_GYOUSHA_SEARCH_SHUKKA", "cbtn_GENBA_SEARCH_SHUKKA"
                                                 };

        /// <summary>
        /// HeaderFormの入力コントロール名一覧
        /// </summary>
        private string[] inputHeaderControlNames = { "KYOTEN_CD"};

        /// <summary>
        /// Detailの入力コントロール名一覧
        /// </summary>
        private string[] inputDetailControlNames = { "ROW_NO",
                                                     "UKEIRE_HINMEI_CD", "UKEIRE_STACK_JYUURYOU", "UKEIRE_CHOUSEI_JYUURYOU", "UKEIRE_SUURYOU", "UKEIRE_UNIT_CD", "UKEIRE_TANKA", "UKEIRE_KINGAKU", "UKEIRE_MEISAI_BIKOU",
                                                     "UKEIRE_HINMEI_NAME", "UKEIRE_DENPYOU_KBN_NAME", "UKEIRE_NET_JYUURYOU", "UKEIRE_UNIT_NAME",
                                                     "SHUKKA_HINMEI_CD", "SHUKKA_STACK_JYUURYOU", "SHUKKA_CHOUSEI_JYUURYOU", "SHUKKA_SUURYOU", "SHUKKA_UNIT_CD", "SHUKKA_TANKA", "SHUKKA_KINGAKU", "SHUKKA_MEISAI_BIKOU",
                                                     "SHUKKA_HINMEI_NAME", "SHUKKA_DENPYOU_KBN_NAME", "SHUKKA_NET_JYUURYOU", "SHUKKA_UNIT_NAME"};

        #endregion

        #region 取引先、締日、業者、現場と営業担当者項目
        internal CustomAlphaNumTextBox torihikisakiCd;
        internal CustomTextBox torihikisakiNm;
        internal CustomNumericTextBox2 shimebi1;
        internal CustomNumericTextBox2 shimebi2;
        internal CustomNumericTextBox2 shimebi3;
        internal CustomAlphaNumTextBox gyoushaCd;
        internal CustomTextBox gyoushaNm;
        internal CustomAlphaNumTextBox genbaCd;
        internal CustomTextBox genbaNm;
        internal CustomAlphaNumTextBox eigyoushaCd;
        internal CustomTextBox eigyoushaNm;
        internal string controlTorihikisaki;
        internal string controlGyousha;
        internal string controlGenba;
        internal string controlEigyousha;
        #endregion

        // MAILAN #158994 START
        internal bool isTankaMessageShown = false;
        internal bool isContinueCheck = true;
        // MAILAN #158994 END

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public G161Logic(G161Form targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            //メインフォーム
            this.form = targetForm;

            this.accessor = new DBAccessor();
            this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();

            // Utility
            this.controlUtil = new ControlUtility();
            this.errmessage = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面情報の初期化を行う

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //親フォーム
                this.ParentForm = (BusinessBaseForm)this.form.Parent;
                //ヘッダーForm
                this.headerForm = (G161HeaderForm)this.ParentForm.headerForm;

                this.ChangeEnabledForInputControl(false);

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // システム情報を取得
                this.GetSysInfo();
                this.GetCorpInfo();

                // 確定フラグの初期化
                this.KakuteiInit();

                // 初期表示
                if (!this.DisplayInit())
                {
                    return false;
                }

                // 取引先、締日、業者、現場と営業担当者項目の初期化
                this.TorihikiControlInfoInit();
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
        /// 確定フラグの初期化を行う
        /// </summary>
        internal void KakuteiInit()
        {
            if (this.sysInfoEntity.DAINO_KAKUTEI_USE_KBN == 1)
            {
                this.form.KAKUTEI_KBN_LABEL.Visible = true;
                this.form.KAKUTEI_KBN.Visible = true;
                this.form.KAKUTEI_KBN_NAME.Visible = true;
            }
            else
            {
                this.form.KAKUTEI_KBN_LABEL.Visible = false;
                this.form.KAKUTEI_KBN.Visible = false;
                this.form.KAKUTEI_KBN_NAME.Visible = false;
            }
        }
        #endregion

        #region ボタンの初期化処理
        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.ParentForm, this.form.WindowType);

            // ﾎﾞﾀﾝEnabled制御
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                var cont = controlUtil.FindControl(this.ParentForm, button.ButtonName);
                if (cont != null && !string.IsNullOrEmpty(cont.Text))
                {
                    cont.Enabled = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン情報の設定を行う
        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.ButtonInfoXmlPath);
        }

        #endregion

        #region イベントの初期化処理
        /// <summary>
        ///  イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // 入力担当者
                this.form.NYUURYOKU_TANTOUSHA_CD.Enter += this.form.Control_Enter;
                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Enter += this.form.Control_Enter;
                // 車種
                this.form.SHASHU_CD.Enter += this.form.Control_Enter;
                // 車輌
                this.form.SHARYOU_CD.Enter += this.form.Control_Enter;
                // 運転者
                this.form.UNTENSHA_CD.Enter += this.form.Control_Enter;

                // 取引先
                this.form.UKEIRE_TORIHIKISAKI_CD.Enter += this.form.Control_Enter;
                this.form.SHUKKA_TORIHIKISAKI_CD.Enter += this.form.Control_Enter;
                // 業者
                this.form.UKEIRE_GYOUSHA_CD.Enter += this.form.Control_Enter;
                this.form.SHUKKA_GYOUSHA_CD.Enter += this.form.Control_Enter;
                // 現場
                this.form.UKEIRE_GENBA_CD.Enter += this.form.Control_Enter;
                this.form.SHUKKA_GENBA_CD.Enter += this.form.Control_Enter;
                // 営業担当者
                this.form.UKEIRE_EIGYOU_TANTOUSHA_CD.Enter += this.form.Control_Enter;
                this.form.SHUKKA_EIGYOU_TANTOUSHA_CD.Enter += this.form.Control_Enter;

                // 前
                this.form.previousButton.Click += new EventHandler(this.previousButton_Click);
                // 次
                this.form.nextButton.Click += new EventHandler(this.nextButton_Click);

                // F2　新規
                this.ParentForm.bt_func2.Click += new EventHandler(this.form.NewMode);
                // F3　修正
                this.ParentForm.bt_func3.Click += new EventHandler(this.form.ModifyMode);
                // F7　一覧
                this.ParentForm.bt_func7.Click += new EventHandler(this.form.IchiranHyouji);
                // F9　登録
                this.ParentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                // F10　行挿入
                this.ParentForm.bt_func10.Click += new EventHandler(this.form.AddRow);
                // F11　行削除
                this.ParentForm.bt_func11.Click += new EventHandler(this.form.RemoveRow);
                // F12 閉じる
                this.ParentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // [1]運賃入力
                this.ParentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

                // [2]出荷量セット
                this.ParentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

                // 処理NoESC
                this.ParentForm.txb_process.Tag = "[Enter]を押下すると指定した番号の処理が実行されます";
            }
            catch (Exception ex)
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 初期表示
        ///<summary>
        /// 初期表示
        /// </summary>
        internal bool DisplayInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                switch (this.form.WindowType)
                {
                    // 新規モード
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        #region 新規モードの初期表示
                        // ヘッダ部クリア
                        this.HeaderClear();
                        // ヘッダ部初期設定
                        if (!this.HeaderInit())
                        {
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                        this.SetKateiKbnInfo();
                        // ヘッダーセット
                        this.SetWindowTypeLabel(this.form.WindowType);
                        // 明細クリア
                        this.MeisaiClear();

                        // 伝票番号により処理
                        if (this.PrmDainouNumber != 0)
                        {
                            // 代納ヘッダーデータの取得
                            this.dainouUkeireEntry = this.accessor.GetDainouEntry(this.PrmDainouNumber, 1);
                            this.dainouShukkaEntry = this.accessor.GetDainouEntry(this.PrmDainouNumber, 2);

                            if (this.dainouUkeireEntry != null && this.dainouShukkaEntry != null)
                            {
                                // 画面表示
                                this.GamenHyouji();
                                // 画面の伝票番号は空に
                                this.form.DAINOU_NUMBER.Text = string.Empty;

                                // 代納明細データの取得
                                this.dainouUkeireDetail = this.accessor.GetDainouDetail(this.dainouUkeireEntry);
                                this.dainouShukkaDetail = this.accessor.GetDainouDetail(this.dainouShukkaEntry);
                                // 明細表示
                                if (!this.DainouMeisaiInfoHyouji())
                                {
                                    LogUtility.DebugMethodEnd(false);
                                    return false;
                                }
                            }
                        }

                        // 画面項目制御
                        this.SetEnabledForInputControl(this.form.WindowType);

                        // ボタン活性化
                        this.ButtonControl();

                        // 一時保存前に容器内容クリア
                        this.BeforeDetailResultList.Clear();
                        // 明細検索一覧データの一時保存
                        if (!this.BeforeIchiranChengeValues(0, 0, 0))
                        {
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }

                        if (this.sysInfoEntity.DAINO_KAKUTEI_USE_KBN == 1)
                        {
                            this.form.KAKUTEI_KBN.Text = Convert.ToString(this.sysInfoEntity.DAINO_KAKUTEI_FLAG);
                            this.form.KAKUTEI_KBN_NAME.Text = ConstClass.GetKakuteiKbnName(this.sysInfoEntity.DAINO_KAKUTEI_FLAG.Value);
                        }
                        else
                        {
                            this.form.KAKUTEI_KBN.Text = "1";
                            this.form.KAKUTEI_KBN_NAME.Text = ConstClass.GetKakuteiKbnName(1);
                        }
                        #endregion
                        break;

                    // 修正、参照、削除モード
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        #region 修正、参照、削除モードの初期表示

                        this.SetKateiKbnInfo();

                        // 代納ヘッダーデータの取得
                        this.dainouUkeireEntry = this.accessor.GetDainouEntry(this.PrmDainouNumber, 1);
                        this.dainouShukkaEntry = this.accessor.GetDainouEntry(this.PrmDainouNumber, 2);
                        this.befDainouukeireEntry = this.dainouUkeireEntry;

                        if (!this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
                        {
                            DateTime getsujiShoriCheckDate = this.dainouShukkaEntry.DENPYOU_DATE.Value;
                            GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
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
                            }
                            // 月次処理ロックチェック
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
                            }
                            else
                            {
                                // 締済状況チェック
                                this.CheckAllShimeStatus();
                                if (this.ukeireShimeiCheckFlg || this.shukkaShimeiCheckFlg)
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
                                    msgLogic.MessageBoxShow("I011", messageArg);

                                    this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                }
                            }
                        }

                        // ヘッダーセット
                        this.SetWindowTypeLabel(this.form.WindowType);

                        // 明細クリア
                        this.MeisaiClear();

                        if (this.dainouUkeireEntry != null && this.dainouShukkaEntry != null)
                        {
                            // 画面表示
                            this.GamenHyouji();

                            // 代納明細データの取得
                            this.dainouUkeireDetail = this.accessor.GetDainouDetail(this.dainouUkeireEntry);
                            this.dainouShukkaDetail = this.accessor.GetDainouDetail(this.dainouShukkaEntry);
                            // 明細表示
                            if (!this.DainouMeisaiInfoHyouji())
                            {
                                LogUtility.DebugMethodEnd(false);
                                return false;
                            }
                        }

                        // 画面項目制御
                        this.SetEnabledForInputControl(this.form.WindowType);

                        // ボタン活性化
                        this.ButtonControl();

                        // 一時保存前に容器内容クリア
                        this.BeforeDetailResultList.Clear();
                        // 明細検索一覧データの一時保存
                        if (!this.BeforeIchiranChengeValues(1, 0, 0))
                        {
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                        #endregion
                        break;
                }

                if (this.PrmDainouNumber != 0)
                {
                    // 合計項目
                    if (!this.CalcAllDetailAndTotal())
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DisplayInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DisplayInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;

        }

        /// <summary>
        /// ヘーダ部の初期表示
        /// </summary>
        private bool HeaderInit()
        {
            // 初回登録
            this.headerForm.CreateUser.Text = string.Empty;
            this.headerForm.CreateDate.Text = string.Empty;
            // 最終更新
            this.headerForm.LastUpdateUser.Text = string.Empty;
            this.headerForm.LastUpdateDate.Text = string.Empty;
            // 拠点CD
            const string KYOTEN_CD = "拠点CD";
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.headerForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, KYOTEN_CD);
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text.ToString()))
            {
                this.headerForm.KYOTEN_CD.Text = this.headerForm.KYOTEN_CD.Text.ToString().PadLeft(this.headerForm.KYOTEN_CD.MaxLength, '0');
                CheckKyotenCd();
            }
            // 伝票日付
            this.form.DENPYOU_DATE.Value = this.ParentForm.sysDate;
            // 入力担当者
            this.form.NYUURYOKU_TANTOUSHA_CD.Text = SystemProperty.Shain.CD;
            this.form.NYUURYOKU_TANTOUSHA_NAME_RYAKU.Text = SystemProperty.Shain.Name;
            // 支払日付
            this.form.SHIHARAI_DATE.Value = this.ParentForm.sysDate;
            // 売上日付
            this.form.URIAGE_DATE.Value = this.ParentForm.sysDate;
            // 消費税率
            this.InitShouhizeiRatePopupSetting();
            if (!this.SetShiharaiShouhizeiRate())
            {
                return false;
            }
            if (!this.SetUriageShouhizeiRate())
            {
                return false;
            }
            // 締状況
            this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = string.Empty;
            this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = string.Empty;
            return true;
        }

        #endregion

        #region 取引先、締日、業者、現場と営業担当者項目の初期化
        /// <summary>
        /// 取引先、締日、業者、現場と営業担当者項目の初期化
        /// </summary>
        internal void TorihikiControlInfoInit()
        {
            this.torihikisakiCd = new CustomAlphaNumTextBox();
            this.torihikisakiNm = new CustomTextBox();
            this.shimebi1 = new CustomNumericTextBox2();
            this.shimebi2 = new CustomNumericTextBox2();
            this.shimebi3 = new CustomNumericTextBox2();
            this.gyoushaCd = new CustomAlphaNumTextBox();
            this.gyoushaNm = new CustomTextBox();
            this.genbaCd = new CustomAlphaNumTextBox();
            this.genbaNm = new CustomTextBox();
            this.eigyoushaCd = new CustomAlphaNumTextBox();
            this.eigyoushaNm = new CustomTextBox();
            this.controlTorihikisaki = string.Empty;
            this.controlGyousha = string.Empty;
            this.controlGenba = string.Empty;
        }

        /// <summary>
        /// 取引先、締日、業者、現場と営業担当者項目の設定
        /// </summary>
        internal void SetTorihikiControlInfo(int flg)
        {
            if (flg == 1)
            {
                this.torihikisakiCd = this.form.UKEIRE_TORIHIKISAKI_CD;
                this.torihikisakiNm = this.form.UKEIRE_TORIHIKISAKI_NAME_RYAKU;
                this.shimebi1 = this.form.UKEIRE_SHIMEBI1;
                this.shimebi2 = this.form.UKEIRE_SHIMEBI2;
                this.shimebi3 = this.form.UKEIRE_SHIMEBI3;
                this.gyoushaCd = this.form.UKEIRE_GYOUSHA_CD;
                this.gyoushaNm = this.form.UKEIRE_GYOUSHA_NAME_RYAKU;
                this.genbaCd = this.form.UKEIRE_GENBA_CD;
                this.genbaNm = this.form.UKEIRE_GENBA_NAME_RYAKU;
                this.eigyoushaCd = this.form.UKEIRE_EIGYOU_TANTOUSHA_CD;
                this.eigyoushaNm = this.form.UKEIRE_EIGYOU_TANTOUSHA_NAME_RYAKU;
                this.controlTorihikisaki = ConstClass.CONTROL_UKEIRE_TORIHIKISAKI_CD;
                this.controlGyousha = ConstClass.CONTROL_UKEIRE_GYOUSHA_CD;
                this.controlGenba = ConstClass.CONTROL_UKEIRE_GENBA_CD;
                this.controlEigyousha = ConstClass.CONTROL_UKEIRE_EIGYOU_TANTOUSHA_CD;
            }
            else if (flg == 2)
            {
                this.torihikisakiCd = this.form.SHUKKA_TORIHIKISAKI_CD;
                this.torihikisakiNm = this.form.SHUKKA_TORIHIKISAKI_NAME_RYAKU;
                this.shimebi1 = this.form.SHUKKA_SHIMEBI1;
                this.shimebi2 = this.form.SHUKKA_SHIMEBI2;
                this.shimebi3 = this.form.SHUKKA_SHIMEBI3;
                this.gyoushaCd = this.form.SHUKKA_GYOUSHA_CD;
                this.gyoushaNm = this.form.SHUKKA_GYOUSHA_NAME_RYAKU;
                this.genbaCd = this.form.SHUKKA_GENBA_CD;
                this.genbaNm = this.form.SHUKKA_GENBA_NAME_RYAKU;
                this.eigyoushaCd = this.form.SHUKKA_EIGYOU_TANTOUSHA_CD;
                this.eigyoushaNm = this.form.SHUKKA_EIGYOU_TANTOUSHA_NAME_RYAKU;
                this.controlTorihikisaki = ConstClass.CONTROL_SHUKKA_TORIHIKISAKI_CD;
                this.controlGyousha = ConstClass.CONTROL_SHUKKA_GYOUSHA_CD;
                this.controlGenba = ConstClass.CONTROL_SHUKKA_GENBA_CD;
                this.controlEigyousha = ConstClass.CONTROL_SHUKKA_EIGYOU_TANTOUSHA_CD;
            }
        }
        #endregion

        #region ヘッダーの拠点CDの存在チェック
        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            // 初期化
            this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

            if (string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                return;
            }

            short kyoteCd = -1;
            if (!short.TryParse(string.Format("{0:#,0}", this.headerForm.KYOTEN_CD.Text), out kyoteCd))
            {
                return;
            }

            var kyotens = this.accessor.GetDataByCodeForKyoten(kyoteCd);

            // 存在チェック
            if (kyotens == null || kyotens.Length < 1)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "拠点");
                this.headerForm.KYOTEN_CD.Focus();
                this.headerForm.KYOTEN_CD.IsInputErrorOccured = true;
                return;
            }
            else
            {
                // キーが１つなので複数はヒットしないはず
                M_KYOTEN kyoten = kyotens[0];
                this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
            }
        }
        #endregion

        #region ボタンの活性化
        /// <summary>
        /// ボタンの活性化
        /// </summary>
        internal void ButtonControl()
        {
            LogUtility.DebugMethodStart();

            this.ButtonInit();

            var ParentForm = (BusinessBaseForm)this.form.Parent;

            switch (this.form.WindowType)
            {
                // 新規モード
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.form.DAINOU_NUMBER.Enabled = true;
                    this.form.previousButton.Enabled = true;
                    this.form.nextButton.Enabled = true;
                    this.ParentForm.bt_func2.Enabled = true;
                    this.ParentForm.bt_func3.Enabled = false;
                    this.ParentForm.bt_func7.Enabled = true;
                    this.ParentForm.bt_func9.Enabled = true;
                    this.ParentForm.bt_process1.Enabled = true;
                    this.ParentForm.bt_process2.Enabled = true;
                    break;
                // 参照モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    this.form.DAINOU_NUMBER.Enabled = false;
                    this.form.previousButton.Enabled = true;
                    this.form.nextButton.Enabled = true;
                    this.ParentForm.bt_func2.Enabled = true;
                    this.ParentForm.bt_func3.Enabled = false;
                    this.ParentForm.bt_func10.Enabled = false;
                    this.ParentForm.bt_func11.Enabled = false;
                    this.ParentForm.bt_func7.Enabled = true;
                    this.ParentForm.bt_func9.Enabled = false;
                    this.ParentForm.bt_process1.Enabled = true;
                    this.ParentForm.bt_process2.Enabled = false;
                    break;
                // 修正モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    this.form.DAINOU_NUMBER.Enabled = false;
                    this.form.previousButton.Enabled = true;
                    this.form.nextButton.Enabled = true;
                    this.ParentForm.bt_func2.Enabled = true;
                    this.ParentForm.bt_func3.Enabled = false;
                    this.ParentForm.bt_func7.Enabled = true;
                    this.ParentForm.bt_func9.Enabled = true;
                    this.ParentForm.bt_process1.Enabled = true;
                    this.ParentForm.bt_process2.Enabled = true;
                    break;
                // 削除モード
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.form.DAINOU_NUMBER.Enabled = false;
                    this.form.previousButton.Enabled = false;
                    this.form.nextButton.Enabled = false;
                    this.ParentForm.bt_func2.Enabled = false;
                    this.ParentForm.bt_func3.Enabled = false;
                    this.ParentForm.bt_func7.Enabled = true;
                    this.ParentForm.bt_func9.Enabled = true;
                    this.ParentForm.bt_process1.Enabled = false;
                    this.ParentForm.bt_process2.Enabled = false;
                    break;
                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 「伝票番号」存在チェック
        /// <summary>
        /// 「伝票番号」存在チェック
        /// </summary>
        /// <returns></returns>
        internal int GetDainoNumberExists()
        {
            try
            {
                var dainou = this.accessor.GetDainouUkeireShukkaData(long.Parse(this.form.DAINOU_NUMBER.Text));
                if (dainou == null || dainou.Rows.Count <= 0)
                {
                    return 0;
                }
                else
                {
                    return dainou.Rows.Count;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetDainoNumberExists", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetDainoNumberExists", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return -1;
            }
        }

        /// <summary>
        /// 「伝票番号」存在チェック
        /// </summary>
        /// <returns></returns>
        internal int GetUrshNumberExists()
        {
            try
            {
                var ursh = this.accessor.GetUrshData(long.Parse(this.form.DAINOU_NUMBER.Text));
                if (ursh == null || ursh.Rows.Count <= 0)
                {
                    return 0;
                }
                else
                {
                    return ursh.Rows.Count;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetUrshNumberExists", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetUrshNumberExists", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return -1;
            }
        }
        #endregion

        #region 明細情報の表示
        /// <summary>
        /// 明細情報の表示
        /// </summary>
        internal bool DainouMeisaiInfoHyouji()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // 数量フォーマット
                String systemSuuryouFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT, string.Empty).ToString();
                // 単価フォーマット
                String systemTankaFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_TANKA_FORMAT, string.Empty).ToString();

                // 明細クリア
                this.form.Ichiran.Rows.Clear();
                int ukeireCount = (this.dainouUkeireDetail == null || this.dainouUkeireDetail.Count <= 0) ? 0 : this.dainouUkeireDetail.Count;
                int shukkaCount = (this.dainouShukkaDetail == null || this.dainouShukkaDetail.Count <= 0) ? 0 : this.dainouShukkaDetail.Count;
                if (ukeireCount == 0 && shukkaCount == 0)
                {
                    return true;
                }
                int rowCount = (ukeireCount > shukkaCount) ? ukeireCount : shukkaCount;

                // 画面にデータを表示
                // 明細行を追加
                this.form.Ichiran.Rows.Add(rowCount);
                this.form.Ichiran.ClearSelection();
                this.form.Ichiran.AddSelection(0);

                // 検索結果設定
                for (int i = 0; i < this.form.Ichiran.Rows.Count - newRowNum; i++)
                {
                    // 受入の明細
                    // 行番号
                    this.form.Ichiran[i, ConstClass.CONTROL_ROW_NO].Value = i + 1;

                    #region 受入明細情報
                    if (i < this.dainouUkeireDetail.Count)
                    {
                        // 品名CD
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value = this.dainouUkeireDetail[i].HINMEI_CD;
                        // 品名
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value = this.dainouUkeireDetail[i].HINMEI_NAME;
                        // 伝票CD（非表示）
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value = this.ChgDBNullToValue(this.dainouUkeireDetail[i].DENPYOU_KBN_CD, string.Empty);
                        // 伝票区分
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value = ConstClass.UKEIRE_DENPYOU_KBN_NAME;
                        // 正味
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value = CommonCalc.SuuryouAndTankFormat(this.ChgDBNullToValue(this.dainouUkeireDetail[i].STACK_JYUURYOU, string.Empty), systemSuuryouFormat);
                        // 調整
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_CHOUSEI_JYUURYOU].Value = CommonCalc.SuuryouAndTankFormat(this.ChgDBNullToValue(this.dainouUkeireDetail[i].CHOUSEI_JYUURYOU, string.Empty), systemSuuryouFormat);
                        // 実正味
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_NET_JYUURYOU].Value = CommonCalc.SuuryouAndTankFormat(this.ChgDBNullToValue(this.dainouUkeireDetail[i].NET_JYUURYOU, string.Empty), systemSuuryouFormat);
                        // 数量
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_SUURYOU].Value = CommonCalc.SuuryouAndTankFormat(this.ChgDBNullToValue(this.dainouUkeireDetail[i].SUURYOU, 0), systemSuuryouFormat);
                        // 単位ＣＤ
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_UNIT_CD].Value = this.ChgDBNullToValue(this.dainouUkeireDetail[i].UNIT_CD, string.Empty).ToString();
                        // 単位
                        if (!this.dainouUkeireDetail[i].UNIT_CD.IsNull)
                        {
                            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG ||
                                this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                                this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG ||
                                (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && this.PrmDainouNumber != 0))
                            {
                                // 新規以外の場合は削除データを含む
                                var unit = this.accessor.GetUnit((short)this.dainouUkeireDetail[i].UNIT_CD, false);
                                this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value = (unit == null || unit.Length <= 0) ? string.Empty : unit[0].UNIT_NAME_RYAKU;
                            }
                            else
                            {
                                // 新規の場合は削除データは含まない
                                var unit = this.accessor.GetUnit((short)this.dainouUkeireDetail[i].UNIT_CD);
                                this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value = (unit == null || unit.Length <= 0) ? string.Empty : unit[0].UNIT_NAME_RYAKU;
                            }
                        }
                        // 単価
                        if (!this.dainouUkeireDetail[i].TANKA.IsNull)
                        {
                            this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_TANKA].Value = CommonCalc.SuuryouAndTankFormat(this.dainouUkeireDetail[i].TANKA, systemTankaFormat);
                        }
                        // 金額
                        // 品名別税区分は設定がない場合
                        if (this.dainouUkeireDetail[i].HINMEI_ZEI_KBN_CD.IsNull)
                        {
                            // 金額
                            this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)this.dainouUkeireDetail[i].KINGAKU);
                        }
                        else // 品名別税区分は設定がある場合
                        {
                            // 金額
                            this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)this.dainouUkeireDetail[i].HINMEI_KINGAKU);
                        }
                        // 明細備考
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_MEISAI_BIKOU].Value = this.dainouUkeireDetail[i].MEISAI_BIKOU;
                        // 税区分CD
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value = (this.dainouUkeireDetail[i].HINMEI_ZEI_KBN_CD.IsNull) ?
                            string.Empty : this.dainouUkeireDetail[i].HINMEI_ZEI_KBN_CD.ToString();

                        // 明細システムID
                        this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_DETAIL_SYSTEM_ID].Value = this.ChgDBNullToValue(this.dainouUkeireDetail[i].DETAIL_SYSTEM_ID, string.Empty);

                        // 数量制御
                        if (!this.SetHinmeiSuuryou(i, 1))
                        {
                            return false;
                        }
                    }
                    #endregion

                    #region 出荷明細情報
                    if (i < this.dainouShukkaDetail.Count)
                    {
                        // 品名CD
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value = this.dainouShukkaDetail[i].HINMEI_CD;
                        // 品名
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value = this.dainouShukkaDetail[i].HINMEI_NAME;
                        // 伝票CD（非表示）
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value = this.ChgDBNullToValue(this.dainouShukkaDetail[i].DENPYOU_KBN_CD, string.Empty);
                        // 伝票区分
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value = ConstClass.SHUKKA_DENPYOU_KBN_NAME;
                        // 正味
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value = CommonCalc.SuuryouAndTankFormat(this.ChgDBNullToValue(this.dainouShukkaDetail[i].STACK_JYUURYOU, string.Empty), systemSuuryouFormat);
                        // 調整
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_CHOUSEI_JYUURYOU].Value = CommonCalc.SuuryouAndTankFormat(this.ChgDBNullToValue(this.dainouShukkaDetail[i].CHOUSEI_JYUURYOU, string.Empty), systemSuuryouFormat);
                        // 実正味
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_NET_JYUURYOU].Value = CommonCalc.SuuryouAndTankFormat(this.ChgDBNullToValue(this.dainouShukkaDetail[i].NET_JYUURYOU, string.Empty), systemSuuryouFormat);
                        // 数量
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_SUURYOU].Value = CommonCalc.SuuryouAndTankFormat(this.ChgDBNullToValue(this.dainouShukkaDetail[i].SUURYOU, 0), systemSuuryouFormat);
                        // 単位ＣＤ
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_UNIT_CD].Value = this.ChgDBNullToValue(this.dainouShukkaDetail[i].UNIT_CD, string.Empty).ToString();
                        // 単位
                        if (!this.dainouShukkaDetail[i].UNIT_CD.IsNull)
                        {
                            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG ||
                                this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                                this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG ||
                                (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && this.PrmDainouNumber != 0))
                            {
                                // 新規以外の場合は削除データを含む
                                var unit = this.accessor.GetUnit((short)this.dainouShukkaDetail[i].UNIT_CD, false);
                                this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value = (unit == null || unit.Length <= 0) ? string.Empty : unit[0].UNIT_NAME_RYAKU;
                            }
                            else
                            {
                                // 新規の場合は削除データは含まない
                                var unit = this.accessor.GetUnit((short)this.dainouShukkaDetail[i].UNIT_CD);
                                this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value = (unit == null || unit.Length <= 0) ? string.Empty : unit[0].UNIT_NAME_RYAKU;
                            }
                        }
                        // 単価
                        if (!this.dainouShukkaDetail[i].TANKA.IsNull)
                        {
                            this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_TANKA].Value = CommonCalc.SuuryouAndTankFormat(this.dainouShukkaDetail[i].TANKA, systemTankaFormat);
                        }
                        // 金額
                        // 品名別税区分は設定がない場合
                        if (this.dainouShukkaDetail[i].HINMEI_ZEI_KBN_CD.IsNull)
                        {
                            // 金額
                            this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)this.dainouShukkaDetail[i].KINGAKU);
                        }
                        else // 品名別税区分は設定がある場合
                        {
                            // 金額
                            this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)this.dainouShukkaDetail[i].HINMEI_KINGAKU);
                        }
                        // 明細備考
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_MEISAI_BIKOU].Value = this.dainouShukkaDetail[i].MEISAI_BIKOU;
                        // 税区分CD
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value = (this.dainouShukkaDetail[i].HINMEI_ZEI_KBN_CD.IsNull) ?
                            string.Empty : this.dainouShukkaDetail[i].HINMEI_ZEI_KBN_CD.ToString();

                        // 明細システムID
                        this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_DETAIL_SYSTEM_ID].Value = this.ChgDBNullToValue(this.dainouShukkaDetail[i].DETAIL_SYSTEM_ID, string.Empty);

                        // 数量制御
                        if (!this.SetHinmeiSuuryou(i, 2))
                        {
                            return false;
                        }
                    }
                    #endregion

                    this.form.SetIchiranReadOnly(this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_DETAIL_SYSTEM_ID].RowIndex);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 前ボタンのクリック
        /// <summary>
        /// 前ボタンのクリック
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void previousButton_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                long dainouNumber = 0;

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        dainouNumber = this.accessor.GetMaxDainouNumber(this.headerForm.KYOTEN_CD.Text);
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        if (string.IsNullOrEmpty(this.form.DAINOU_NUMBER.Text))
                        {
                            dainouNumber = this.accessor.GetMaxDainouNumber(this.headerForm.KYOTEN_CD.Text);
                        }
                        else
                        {
                            dainouNumber = this.accessor.GetPreUkeireNumber(long.Parse(this.form.DAINOU_NUMBER.Text), this.headerForm.KYOTEN_CD.Text);
                        }
                        break;
                }

                WINDOW_TYPE tmpType = this.form.WindowType;

                if (dainouNumber != 0)
                {
                    if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) || this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
                    {
                        this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G161", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限がない場合
                            if (r_framework.Authority.Manager.CheckAuthority("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                // 修正権限は無いが参照権限がある場合は参照モードで起動
                                this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            }
                            else
                            {
                                // どちらも無い場合はアラートを表示して処理中断
                                MessageBoxShowLogic msg = new MessageBoxShowLogic();
                                msg.MessageBoxShow("E158", "修正");
                                this.form.WindowType = tmpType;
                                this.form.DAINOU_NUMBER.Focus();
                                return;
                            }
                        }
                    }

                    this.PrmDainouNumber = dainouNumber;
                }
                else
                {
                    //ThangNguyen [Add] 20150814 #11409 Start
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return;
                    //ThangNguyen [Add] 20150814 #11409 End
                }

                this.SetWindowTypeLabel(this.form.WindowType);
                this.DisplayInit();
            }
            catch (Exception ex)
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 次ボタンのクリック
        /// <summary>
        /// 次ボタンのクリック
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void nextButton_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                long dainouNumber = 0;

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        dainouNumber = this.accessor.GetMinDainouNumber(this.headerForm.KYOTEN_CD.Text);
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        if (string.IsNullOrEmpty(this.form.DAINOU_NUMBER.Text))
                        {
                            dainouNumber = this.accessor.GetMinDainouNumber(this.headerForm.KYOTEN_CD.Text);
                        }
                        else
                        {
                            dainouNumber = this.accessor.GetNextUkeireNumber(long.Parse(this.form.DAINOU_NUMBER.Text), this.headerForm.KYOTEN_CD.Text);
                        }
                        break;
                }

                WINDOW_TYPE tmpType = this.form.WindowType;

                if (dainouNumber != 0)
                {
                    if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) || this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
                    {
                        this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G161", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限がない場合
                            if (r_framework.Authority.Manager.CheckAuthority("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                // 修正権限は無いが参照権限がある場合は参照モードで起動
                                this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            }
                            else
                            {
                                // どちらも無い場合はアラートを表示して処理中断
                                MessageBoxShowLogic msg = new MessageBoxShowLogic();
                                msg.MessageBoxShow("E158", "修正");
                                this.form.WindowType = tmpType;
                                this.form.DAINOU_NUMBER.Focus();
                                return;
                            }
                        }
                    }

                    this.PrmDainouNumber = dainouNumber;
                }
                else
                {
                    //ThangNguyen [Add] 20150814 #11409 Start
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return;
                    //ThangNguyen [Add] 20150814 #11409 End
                }
                this.SetWindowTypeLabel(this.form.WindowType);
                this.DisplayInit();
            }
            catch (Exception ex)
            {
                throw;
            }

            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region モードラベルのプロパティの設定
        /// <summary>
        /// モードラベルのプロパティの設定
        /// </summary>
        /// <param name="winType"></param>
        internal void SetWindowTypeLabel(WINDOW_TYPE winType)
        {
            LogUtility.DebugMethodStart(winType);

            switch (winType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.headerForm.windowTypeLabel.Text = "新規";
                    this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                    this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                    this.form.Ichiran.AllowUserToAddRows = true;
                    this.newRowNum = 1;

                    break;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    this.headerForm.windowTypeLabel.Text = "修正";
                    this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                    this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                    this.form.Ichiran.AllowUserToAddRows = true;
                    this.newRowNum = 1;
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.headerForm.windowTypeLabel.Text = "削除";
                    this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Red;
                    this.form.Ichiran.AllowUserToAddRows = false;
                    this.newRowNum = 0;

                    break;
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    this.headerForm.windowTypeLabel.Text = "参照";
                    this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Orange;
                    this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                    this.form.Ichiran.AllowUserToAddRows = false;
                    this.newRowNum = 0;
                    break;
                default:
                    this.headerForm.windowTypeLabel.Text = string.Empty;
                    this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Empty;
                    break;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// ユーザ入力コントロールの活性制御
        /// </summary>
        /// <param name="winType"></param>
        internal void SetEnabledForInputControl(WINDOW_TYPE winType)
        {
            LogUtility.DebugMethodStart(winType);

            switch (winType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    //画面読取専用を取り消し
                    this.ChangeEnabledForInputControl(false);
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    //各項目読取専用
                    this.ChangeEnabledForInputControl(true);
                    break;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ユーザ入力コントロールの活性制御
        /// </summary>
        /// <param name="isLock">ロック状態に設定するbool</param>
        internal void ChangeEnabledForInputControl(bool isLock)
        {
            LogUtility.DebugMethodStart();

            // UIFormのコントロールを制御
            List<string> formControlNameList = new List<string>();
            if (this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
            {
                // 画面モードが参照用の場合
                formControlNameList.AddRange(refUiFormControlNames);
            }
            else
            {
                formControlNameList.AddRange(inputUiFormControlNames);
            }
            formControlNameList.AddRange(inputHeaderControlNames);
            foreach (var controlName in formControlNameList)
            {
                Control control = controlUtil.FindControl(this.form, controlName);

                if (control == null)
                {
                    // headerを検索
                    control = controlUtil.FindControl(this.headerForm, controlName);
                }

                if (control == null)
                {
                    continue;
                }

                var property = control.GetType().GetProperty("Enabled");

                if (property != null)
                {
                    property.SetValue(control, !isLock, null);
                }
            }

            // Detailのコントロールを制御
            foreach (Row row in this.form.Ichiran.Rows)
            {
                //if (row.IsNewRow) { continue; }
                foreach (var detaiControlName in inputDetailControlNames)
                {
                    row.Cells[detaiControlName].Enabled = !isLock;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #region 画面読取専用
        /// <summary>
        /// 画面読取専用
        /// </summary>
        internal void GamenReadOnly()
        {
            LogUtility.DebugMethodStart();
            // 拠点
            this.headerForm.KYOTEN_CD.Enabled = false;
            // 伝票日付
            this.form.DENPYOU_DATE.Enabled = false;
            // 入力担当者
            this.form.NYUURYOKU_TANTOUSHA_CD.Enabled = false;
            // 伝票番号
            this.form.DAINOU_NUMBER.Enabled = false;
            // 運搬業者、車種、車輌、運転者
            this.form.UNPAN_GYOUSHA_CD.Enabled = false;
            this.form.SHASHU_CD.Enabled = false;
            this.form.SHARYOU_CD.Enabled = false;
            this.form.UNTENSHA_CD.Enabled = false;
            // 受入取引先、業者、現場、営業担当者
            this.form.SHIHARAI_DATE.Enabled = false;
            this.form.UKEIRE_TORIHIKISAKI_CD.Enabled = false;
            this.form.UKEIRE_GYOUSHA_CD.Enabled = false;
            this.form.UKEIRE_GENBA_CD.Enabled = false;
            this.form.UKEIRE_EIGYOU_TANTOUSHA_CD.Enabled = false;
            // 出荷取引先、業者、現場、営業担当者
            this.form.URIAGE_DATE.Enabled = false;
            this.form.SHUKKA_TORIHIKISAKI_CD.Enabled = false;
            this.form.SHUKKA_GYOUSHA_CD.Enabled = false;
            this.form.SHUKKA_GENBA_CD.Enabled = false;
            this.form.SHUKKA_EIGYOU_TANTOUSHA_CD.Enabled = false;
            // 受入の取引先、業者、現場の検索ボタン
            this.form.cbtn_TORIHIKISAKI_SEARCH_UKEIRE.Visible = false;
            this.form.cbtn_GYOUSHA_SEARCH_UKEIRE.Visible = false;
            this.form.cbtn_GENBA_SEARCH_UKEIRE.Visible = false;
            // 出荷の取引先、業者、現場の検索ボタン
            this.form.cbtn_TORIHIKISAKI_SEARCH_SHUKKA.Visible = false;
            this.form.cbtn_GYOUSHA_SEARCH_SHUKKA.Visible = false;
            this.form.cbtn_GENBA_SEARCH_SHUKKA.Visible = false;
            // 伝票備考
            this.form.DENPYOU_BIKOU.Enabled = false;

            // 明細
            this.form.Ichiran.Enabled = false;
            //色
            //for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
            //{
            //    for (int j = 0; j < this.form.Ichiran.Columns.Count; j++)
            //    {
            //        this.form.Ichiran.Rows[i][j].Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            //    }
            //}

            //フッダボタン
            this.ButtonInit();

            //処理NoESC
            this.ParentForm.txb_process.Enabled = false;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面読取専用を取り消し
        /// <summary>
        /// 画面読取専用を取り消し
        /// </summary>
        internal void GamenNotReadOnly()
        {
            LogUtility.DebugMethodStart();

            // 拠点
            this.headerForm.KYOTEN_CD.Enabled = true;
            // 伝票日付
            this.form.DENPYOU_DATE.Enabled = true;
            // 入力担当者
            this.form.NYUURYOKU_TANTOUSHA_CD.Enabled = true;
            // 伝票番号
            this.form.DAINOU_NUMBER.Enabled = true;

            // 運搬業者、車種、車輌、運転者
            this.form.SHIHARAI_DATE.Enabled = true;
            this.form.UNPAN_GYOUSHA_CD.Enabled = true;
            this.form.SHASHU_CD.Enabled = true;
            this.form.SHARYOU_CD.Enabled = true;
            this.form.UNTENSHA_CD.Enabled = true;

            // 受入取引先、業者、現場、営業担当者
            this.form.URIAGE_DATE.Enabled = true;
            this.form.UKEIRE_TORIHIKISAKI_CD.Enabled = true;
            this.form.UKEIRE_GYOUSHA_CD.Enabled = true;
            this.form.UKEIRE_GENBA_CD.Enabled = true;
            this.form.UKEIRE_EIGYOU_TANTOUSHA_CD.Enabled = true;

            // 出荷取引先、業者、現場、営業担当者
            this.form.SHUKKA_TORIHIKISAKI_CD.Enabled = true;
            this.form.SHUKKA_GYOUSHA_CD.Enabled = true;
            this.form.SHUKKA_GENBA_CD.Enabled = true;
            this.form.SHUKKA_EIGYOU_TANTOUSHA_CD.Enabled = true;

            // 受入の取引先、業者、現場の検索ボタン
            this.form.cbtn_TORIHIKISAKI_SEARCH_UKEIRE.Visible = true;
            this.form.cbtn_GYOUSHA_SEARCH_UKEIRE.Visible = true;
            this.form.cbtn_GENBA_SEARCH_UKEIRE.Visible = true;

            //出荷の取引先、業者、現場の検索ボタン
            this.form.cbtn_TORIHIKISAKI_SEARCH_SHUKKA.Visible = true;
            this.form.cbtn_GYOUSHA_SEARCH_SHUKKA.Visible = true;
            this.form.cbtn_GENBA_SEARCH_SHUKKA.Visible = true;

            // 伝票備考
            this.form.DENPYOU_BIKOU.Enabled = true;

            // 明細
            this.form.Ichiran.Enabled = true;
            // 色
            //for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
            //{
            //    for (int j = 0; j < this.form.Ichiran.Columns.Count; j++)
            //    {
            //        if (j != 0 && j != 8 && j != 10)
            //        {
            //            this.form.Ichiran.Rows[i][j].Style.BackColor = System.Drawing.Color.Empty;
            //        }
            //    }
            //}

            //フッダボタン
            this.ButtonInit();

            //処理NoESC
            this.ParentForm.txb_process.Enabled = true;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 伝票番号の必須チェック
        /// <summary>
        /// 伝票番号の必須チェック
        /// </summary>
        internal bool DenpyouNumberInputChk()
        {
            LogUtility.DebugMethodStart();
            string denpyouNumber = this.form.DAINOU_NUMBER.Text;
            if (null == denpyouNumber || string.IsNullOrEmpty(denpyouNumber))
            {
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 画面の代納情報表示
        /// <summary>
        /// 画面の代納情報表示
        /// </summary>
        internal void GamenHyouji()
        {
            LogUtility.DebugMethodStart();

            // 拠点CD
            if (!this.dainouUkeireEntry.KYOTEN_CD.IsNull)
            {
                string kyotenCd = this.dainouUkeireEntry.KYOTEN_CD.ToString();
                // ゼロ埋め
                kyotenCd = kyotenCd.PadLeft(2, '0');
                this.headerForm.KYOTEN_CD.Text = kyotenCd;
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG||
                    this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                    this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG ||
                    (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && this.PrmDainouNumber != 0))
                {
                    // 新規以外の場合は削除データを含む
                    var kyoten = this.accessor.GetDataByCodeForKyoten((short)this.dainouUkeireEntry.KYOTEN_CD, false);
                    if (kyoten != null && kyoten.Length > 0)
                    {
                        this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten[0].KYOTEN_NAME_RYAKU;
                    }
                }
                else
                {
                    // 新規の場合は削除データは含まない
                    var kyoten = this.accessor.GetDataByCodeForKyoten((short)this.dainouUkeireEntry.KYOTEN_CD);
                    if (kyoten != null && kyoten.Length > 0)
                    {
                        this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten[0].KYOTEN_NAME_RYAKU;
                    }
                }
            }
            // 初回登録
            this.headerForm.CreateUser.Text = this.dainouUkeireEntry.CREATE_USER;
            if (!this.dainouUkeireEntry.CREATE_DATE.IsNull)
            {
                this.headerForm.CreateDate.Text = ((DateTime)this.dainouUkeireEntry.CREATE_DATE).ToString("yyyy/MM/dd HH:mm:ss");
            }
            // 最終更新
            this.headerForm.LastUpdateUser.Text = this.dainouUkeireEntry.UPDATE_USER;
            if (!this.dainouUkeireEntry.UPDATE_DATE.IsNull)
            {
                this.headerForm.LastUpdateDate.Text = ((DateTime)this.dainouUkeireEntry.UPDATE_DATE).ToString("yyyy/MM/dd HH:mm:ss");
            }
            // 伝票番号
            if (!this.dainouUkeireEntry.UR_SH_NUMBER.IsNull)
            {
                this.form.DAINOU_NUMBER.Text = this.dainouUkeireEntry.UR_SH_NUMBER.ToString();
            }
            // 伝票日付
            if (!this.dainouUkeireEntry.DENPYOU_DATE.IsNull)
            {
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                    || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                    || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    this.form.DENPYOU_DATE.Text = ((DateTime)this.dainouUkeireEntry.DENPYOU_DATE).ToShortDateString();
                    this.beforeDenpyouDate = ((DateTime)this.dainouUkeireEntry.DENPYOU_DATE).ToShortDateString();
                }
                else if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.form.DENPYOU_DATE.Text = this.ParentForm.sysDate.ToString();
                    this.beforeDenpyouDate = this.ParentForm.sysDate.Date.ToString();
                }
            }
            // 確定フラグ
            if (this.sysInfoEntity.DAINO_KAKUTEI_USE_KBN == 1)
            {
                if (!this.dainouUkeireEntry.KAKUTEI_KBN.IsNull)
                {
                    this.form.KAKUTEI_KBN.Text = this.dainouUkeireEntry.KAKUTEI_KBN.ToString();
                    this.form.KAKUTEI_KBN_NAME.Text = ConstClass.GetKakuteiKbnName(this.dainouUkeireEntry.KAKUTEI_KBN.Value);
                }
            }
            else
            {
                this.form.KAKUTEI_KBN.Text = "1";
                this.form.KAKUTEI_KBN_NAME.Text = ConstClass.GetKakuteiKbnName(1);
            }
            // 入力担当者
            this.form.NYUURYOKU_TANTOUSHA_CD.Text = this.dainouUkeireEntry.NYUURYOKU_TANTOUSHA_CD;
            this.form.NYUURYOKU_TANTOUSHA_NAME_RYAKU.Text = this.dainouUkeireEntry.NYUURYOKU_TANTOUSHA_NAME;
            // 運搬業者
            this.form.UNPAN_GYOUSHA_CD.Text = this.dainouUkeireEntry.UNPAN_GYOUSHA_CD;
            this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text = this.dainouUkeireEntry.UNPAN_GYOUSHA_NAME;
            // 車種
            this.form.SHASHU_CD.Text = this.dainouUkeireEntry.SHASHU_CD;
            this.form.SHASHU_NAME_RYAKU.Text = this.dainouUkeireEntry.SHASHU_NAME;
            // 車輌
            this.form.SHARYOU_CD.Text = this.dainouUkeireEntry.SHARYOU_CD;
            this.form.SHARYOU_NAME_RYAKU.Text = this.dainouUkeireEntry.SHARYOU_NAME;
            // 運転者
            this.form.UNTENSHA_CD.Text = this.dainouUkeireEntry.UNTENSHA_CD;
            this.form.UNTENSHA_NAME_RYAKU.Text = this.dainouUkeireEntry.UNTENSHA_NAME;

            #region 受入情報
            // 支払日付
            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                this.form.SHIHARAI_DATE.Text = (this.dainouUkeireEntry.SHIHARAI_DATE.IsNull) ? this.ParentForm.sysDate.ToString() : this.dainouUkeireEntry.SHIHARAI_DATE.ToString();
            }
            else if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.form.SHIHARAI_DATE.Text = this.ParentForm.sysDate.ToString();
            }
            // 支払消費税率
            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                this.form.SHIHARAI_SHOUHIZEI_RATE.Text = (this.dainouUkeireEntry.SHIHARAI_SHOUHIZEI_RATE.IsNull) ? string.Empty : this.dainouUkeireEntry.SHIHARAI_SHOUHIZEI_RATE.ToString();
            }
            // 取引先
            this.form.UKEIRE_TORIHIKISAKI_CD.Text = this.dainouUkeireEntry.TORIHIKISAKI_CD;
            this.form.UKEIRE_TORIHIKISAKI_NAME_RYAKU.Text = this.dainouUkeireEntry.TORIHIKISAKI_NAME;
            // 締日
            this.SetTorihikisakiShimeibi(1);
            // 業者
            this.form.UKEIRE_GYOUSHA_CD.Text = this.dainouUkeireEntry.GYOUSHA_CD;
            this.form.UKEIRE_GYOUSHA_NAME_RYAKU.Text = this.dainouUkeireEntry.GYOUSHA_NAME;
            // 現場
            this.form.UKEIRE_GENBA_CD.Text = this.dainouUkeireEntry.GENBA_CD;
            this.form.UKEIRE_GENBA_NAME_RYAKU.Text = this.dainouUkeireEntry.GENBA_NAME;
            // 営業担当者
            this.form.UKEIRE_EIGYOU_TANTOUSHA_CD.Text = this.dainouUkeireEntry.EIGYOU_TANTOUSHA_CD;
            this.form.UKEIRE_EIGYOU_TANTOUSHA_NAME_RYAKU.Text = this.dainouUkeireEntry.EIGYOU_TANTOUSHA_NAME;
            #endregion

            #region 出荷情報
            // 売上日付
            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                this.form.URIAGE_DATE.Text = (this.dainouShukkaEntry.URIAGE_DATE.IsNull) ? this.ParentForm.sysDate.ToString() : this.dainouShukkaEntry.URIAGE_DATE.ToString();
            }
            else if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.form.URIAGE_DATE.Text = this.ParentForm.sysDate.ToString();
            }
            // 支払消費税率
            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                this.form.URIAGE_SHOUHIZEI_RATE.Text = (this.dainouUkeireEntry.URIAGE_SHOUHIZEI_RATE.IsNull) ? string.Empty : this.dainouUkeireEntry.URIAGE_SHOUHIZEI_RATE.ToString();
            }
            // 取引先
            this.form.SHUKKA_TORIHIKISAKI_CD.Text = this.dainouShukkaEntry.TORIHIKISAKI_CD;
            this.form.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Text = this.dainouShukkaEntry.TORIHIKISAKI_NAME;
            // 締日
            this.SetTorihikisakiShimeibi(2);
            // 業者
            this.form.SHUKKA_GYOUSHA_CD.Text = this.dainouShukkaEntry.GYOUSHA_CD;
            this.form.SHUKKA_GYOUSHA_NAME_RYAKU.Text = this.dainouShukkaEntry.GYOUSHA_NAME;
            // 現場
            this.form.SHUKKA_GENBA_CD.Text = this.dainouShukkaEntry.GENBA_CD;
            this.form.SHUKKA_GENBA_NAME_RYAKU.Text = this.dainouShukkaEntry.GENBA_NAME;
            // 営業担当者
            this.form.SHUKKA_EIGYOU_TANTOUSHA_CD.Text = this.dainouShukkaEntry.EIGYOU_TANTOUSHA_CD;
            this.form.SHUKKA_EIGYOU_TANTOUSHA_NAME_RYAKU.Text = this.dainouShukkaEntry.EIGYOU_TANTOUSHA_NAME;
            #endregion

            //Thang Nguyen 20150626 #10664 Start
            // 締済状況チェック
            CheckAllShimeStatus();
            //Thang Nguyen 20150626 #10664 End
            // 締処理状況(支払)
            if (this.ukeireShimeiCheckFlg)
            {
                this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = ConstClass.SHIMEZUMI;
            }
            else
            {
                this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = ConstClass.MISHIME;
            }

            // 伝票備考
            this.form.DENPYOU_BIKOU.Text = this.dainouUkeireEntry.DENPYOU_BIKOU;

            // 締処理状況(売上)
            if (this.shukkaShimeiCheckFlg)
            {
                this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = ConstClass.SHIMEZUMI;
            }
            else
            {
                this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = ConstClass.MISHIME;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ヘッダー部クリア
        /// <summary>
        /// ヘッダー部クリア
        /// </summary>

        internal void HeaderClear()
        {
            LogUtility.DebugMethodStart();
            // 拠点
            this.headerForm.KYOTEN_CD.Text = string.Empty;
            this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
            this.headerForm.KYOTEN_CD.IsInputErrorOccured = false;
            // 初回登録
            this.headerForm.CreateUser.Text = string.Empty;
            this.headerForm.CreateDate.Text = string.Empty;
            // 最終更新
            this.headerForm.LastUpdateUser.Text = string.Empty;
            this.headerForm.LastUpdateDate.Text = string.Empty;

            // 伝票番号
            this.form.DAINOU_NUMBER.Text = string.Empty;
            // 伝票日付
            this.form.DENPYOU_DATE.Text = string.Empty;
            // 入力担当者
            this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
            this.form.NYUURYOKU_TANTOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.NYUURYOKU_TANTOUSHA_CD.IsInputErrorOccured = false;
            // 運搬業者
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text = string.Empty;
            // 車種
            this.form.SHASHU_CD.Text = string.Empty;
            this.form.SHASHU_NAME_RYAKU.Text = string.Empty;
            // 車輌
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
            // 運転者
            this.form.UNTENSHA_CD.Text = string.Empty;
            this.form.UNTENSHA_NAME_RYAKU.Text = string.Empty;
            // 受入取引先、締日1～3、業者、現場、営業担当者
            this.form.UKEIRE_TORIHIKISAKI_CD.Text = string.Empty;
            this.form.UKEIRE_TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.UKEIRE_TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.form.UKEIRE_SHIMEBI1.Text = string.Empty;
            this.form.UKEIRE_SHIMEBI2.Text = string.Empty;
            this.form.UKEIRE_SHIMEBI3.Text = string.Empty;
            this.form.UKEIRE_GYOUSHA_CD.Text = string.Empty;
            this.form.UKEIRE_GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.UKEIRE_GYOUSHA_CD.IsInputErrorOccured = false;
            this.form.UKEIRE_GENBA_CD.Text = string.Empty;
            this.form.UKEIRE_GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.UKEIRE_EIGYOU_TANTOUSHA_CD.Text = string.Empty;
            this.form.UKEIRE_EIGYOU_TANTOUSHA_NAME_RYAKU.Text = string.Empty;
            // 出荷取引先、締日1～3、業者、現場、営業担当者
            this.form.SHUKKA_TORIHIKISAKI_CD.Text = string.Empty;
            this.form.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.SHUKKA_TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.form.SHUKKA_SHIMEBI1.Text = string.Empty;
            this.form.SHUKKA_SHIMEBI2.Text = string.Empty;
            this.form.SHUKKA_SHIMEBI3.Text = string.Empty;
            this.form.SHUKKA_GYOUSHA_CD.Text = string.Empty;
            this.form.SHUKKA_GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.SHUKKA_GYOUSHA_CD.IsInputErrorOccured = false;
            this.form.SHUKKA_GENBA_CD.Text = string.Empty;
            this.form.SHUKKA_GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.SHUKKA_EIGYOU_TANTOUSHA_CD.Text = string.Empty;
            this.form.SHUKKA_EIGYOU_TANTOUSHA_NAME_RYAKU.Text = string.Empty;
            // 伝票備考
            this.form.DENPYOU_BIKOU.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 明細クリア
        /// <summary>
        /// 明細クリア
        /// </summary>
        internal void MeisaiClear()
        {
            LogUtility.DebugMethodStart();

            // 画面クリア
            this.form.Ichiran.Rows.Clear();

            // 受入金額合計
            this.form.UKEIRE_KINGAKU_SUM.Text = string.Empty;
            // 出荷金額合計
            this.form.SHUKKA_KINGAKU_SUM.Text = string.Empty;
            // 差益金額
            this.form.DIFFERENCE.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 品名CDにより品名情報を取得
        /// <summary>
        /// 品名CDにより品名情報を取得
        /// </summary>
        /// <param name="hinmeiCd"></param>
        /// <param name="flg"></param>
        /// <returns></returns>
        public int HinmeiSearch(string hinmeiCd, int flg)
        {
            LogUtility.DebugMethodStart(hinmeiCd, flg);
            try
            {
                if (flg == 1)
                {
                    this.SearchHinmeiResult = this.accessor.GetHinmeiDataForUkeire(hinmeiCd);
                }
                else if (flg == 2)
                {
                    this.SearchHinmeiResult = this.accessor.GetHinmeiDataForShukka(hinmeiCd);
                }

                if (null != this.SearchHinmeiResult && this.SearchHinmeiResult.Rows.Count > 0)
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    string gyoushaCd = flg == 1 ? this.form.UKEIRE_GYOUSHA_CD.Text : this.form.SHUKKA_GYOUSHA_CD.Text;
                    string genbaCd = flg == 1 ? this.form.UKEIRE_GENBA_CD.Text : this.form.SHUKKA_GENBA_CD.Text;
                    SqlInt16 denpyouKbn = (short)(flg == 1 ? 1 : 2);
                    M_KOBETSU_HINMEI hinmei = this.accessor.GetKobetsuHinmeiDataByCd(gyoushaCd, genbaCd, hinmeiCd, denpyouKbn);
                    if (hinmei == null)
                    {
                        hinmei = this.accessor.GetKobetsuHinmeiDataByCd(gyoushaCd, "", hinmeiCd, denpyouKbn);
                    }
                    if (hinmei != null)
                    {
                        this.SearchHinmeiResult.Rows[0]["HINMEI_NAME"] = hinmei.SEIKYUU_HINMEI_NAME;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    LogUtility.DebugMethodEnd(1);
                    return 1;
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HinmeiSearch", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HinmeiSearch", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }

            LogUtility.DebugMethodEnd(0);
            return 0;
        }
        #endregion

        #region 品名項目をセット
        /// <summary>
        /// 品名項目をセット
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="flg"></param>
        public bool hinmeiSet(int rowIndex, int flg)
        {
            LogUtility.DebugMethodStart(rowIndex, flg);
            try
            {
                if (flg == 1)
                {
                    if (null != this.SearchHinmeiResult && this.SearchHinmeiResult.Rows.Count > 0)
                    {
                        // 品名
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        //if (!this.SearchHinmeiResult.Rows[0].IsNull("HINMEI_NAME_RYAKU"))
                        //{
                        //    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value = this.SearchHinmeiResult.Rows[0].Field<string>("HINMEI_NAME_RYAKU");
                        //}
                        if (!this.SearchHinmeiResult.Rows[0].IsNull("HINMEI_NAME"))
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value = this.SearchHinmeiResult.Rows[0].Field<string>("HINMEI_NAME");
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                        else
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value = string.Empty;
                        }
                        // 単位CD
                        if (!this.SearchHinmeiResult.Rows[0].IsNull("UNIT_CD"))
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value = this.SearchHinmeiResult.Rows[0].Field<Int16>("UNIT_CD").ToString();
                        }
                        else
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value = string.Empty;
                        }
                        // 単位名
                        if (!this.SearchHinmeiResult.Rows[0].IsNull("UNIT_NAME"))
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value = this.SearchHinmeiResult.Rows[0].Field<string>("UNIT_NAME");
                        }
                        else
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value = string.Empty;
                        }
                        // 品名税区分CD
                        if (!this.SearchHinmeiResult.Rows[0].IsNull("ZEI_KBN_CD"))
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value = this.SearchHinmeiResult.Rows[0].Field<Int16>("ZEI_KBN_CD").ToString();
                        }
                        else
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value = string.Empty;
                        }

                        //　選択する品名の単位は"kg"と"t"の場合、数量 = 実正味重量、数量を　入力できないになります
                        if ((this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value.Equals("kg") || this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value.Equals("ｔ"))
                            && this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value != null)
                        {
                            if (this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value.Equals("kg"))
                            {
                                this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].Value = this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value)))
                                {
                                    decimal stackJyuuryou = Convert.ToDecimal(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value);
                                    decimal suuryou = stackJyuuryou / 1000;
                                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].Value = suuryou;
                                }
                            }
                            //this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].ReadOnly = true;
                        }
                        //else
                        //{
                        //    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].ReadOnly = false;
                        //}
                    }
                }
                else if (flg == 2)
                {
                    if (null != this.SearchHinmeiResult && this.SearchHinmeiResult.Rows.Count > 0)
                    {
                        // 品名
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        //if (!this.SearchHinmeiResult.Rows[0].IsNull("HINMEI_NAME_RYAKU"))
                        //{
                        //    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value = this.SearchHinmeiResult.Rows[0].Field<string>("HINMEI_NAME_RYAKU");
                        //}
                        if (!this.SearchHinmeiResult.Rows[0].IsNull("HINMEI_NAME"))
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value = this.SearchHinmeiResult.Rows[0].Field<string>("HINMEI_NAME");
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                        else
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value = string.Empty;
                        }
                        // 単位CD
                        if (!this.SearchHinmeiResult.Rows[0].IsNull("UNIT_CD"))
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value = this.SearchHinmeiResult.Rows[0].Field<Int16>("UNIT_CD").ToString();
                        }
                        else
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value = string.Empty;
                        }
                        // 単位名
                        if (!this.SearchHinmeiResult.Rows[0].IsNull("UNIT_NAME"))
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value = this.SearchHinmeiResult.Rows[0].Field<string>("UNIT_NAME");
                        }
                        else
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value = string.Empty;
                        }
                        // 品名税区分CD
                        if (!this.SearchHinmeiResult.Rows[0].IsNull("ZEI_KBN_CD"))
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value = this.SearchHinmeiResult.Rows[0].Field<Int16>("ZEI_KBN_CD").ToString();
                        }
                        else
                        {
                            this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value = string.Empty;
                        }

                        //　選択する品名の単位は"kg"と"t"の場合、数量 = 実正味重量、数量を　入力できないになります
                        if ((this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value.Equals("kg") || this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value.Equals("ｔ"))
                            && this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value != null)
                        {
                            if (this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value.Equals("kg"))
                            {
                                this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].Value = this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value)))
                                {
                                    decimal stackJyuuryou = Convert.ToDecimal(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value);
                                    decimal suuryou = stackJyuuryou / 1000;
                                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].Value = suuryou;
                                }
                            }
                            //this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].ReadOnly = true;
                        }
                        //else
                        //{
                        //    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].ReadOnly = false;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("hinmeiSet", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 単価設定
        /// <summary>
        /// 単価設定
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="flg"></param>
        internal bool CalcTanka(int rowIndex, int flg)
        {
            bool ret = true;

            LogUtility.DebugMethodStart(rowIndex, flg);
            try
            {
                int denpyouKbnCd = 0;
                int denshuKbnCd = 0;
                string torihikisakiCd = string.Empty;
                string gyoushaCd = string.Empty;
                string genbaCd = string.Empty;
                string hinmeCd = string.Empty;
                string unitCd = string.Empty;
                Row targetRow = this.form.Ichiran.Rows[rowIndex];
                string controlHinmeiCd = string.Empty;
                string controlUnitCd = string.Empty;
                string controlTanka = string.Empty;
                string controlKingaku = string.Empty;
                bool isNotFindTanka = false;
                string denpyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                if (flg == 1)//受入
                {
                    denshuKbnCd = 1;
                    denpyouKbnCd = 2;
                    torihikisakiCd = this.form.UKEIRE_TORIHIKISAKI_CD.Text;
                    gyoushaCd = this.form.UKEIRE_GYOUSHA_CD.Text;
                    genbaCd = this.form.UKEIRE_GENBA_CD.Text;
                    controlHinmeiCd = ConstClass.CONTROL_UKEIRE_HINMEI_CD;
                    controlUnitCd = ConstClass.CONTROL_UKEIRE_UNIT_CD;
                    controlTanka = ConstClass.CONTROL_UKEIRE_TANKA;
                    controlKingaku = ConstClass.CONTROL_UKEIRE_KINGAKU;
                }
                else if (flg == 2)
                {
                    denshuKbnCd = 2;
                    denpyouKbnCd = 1;
                    torihikisakiCd = this.form.SHUKKA_TORIHIKISAKI_CD.Text;
                    gyoushaCd = this.form.SHUKKA_GYOUSHA_CD.Text;
                    genbaCd = this.form.SHUKKA_GENBA_CD.Text;
                    controlHinmeiCd = ConstClass.CONTROL_SHUKKA_HINMEI_CD;
                    controlUnitCd = ConstClass.CONTROL_SHUKKA_UNIT_CD;
                    controlTanka = ConstClass.CONTROL_SHUKKA_TANKA;
                    controlKingaku = ConstClass.CONTROL_SHUKKA_KINGAKU;
                }

                hinmeCd = (targetRow.Cells[controlHinmeiCd].Value == null) ? string.Empty : targetRow.Cells[controlHinmeiCd].Value.ToString();
                unitCd = (targetRow.Cells[controlUnitCd].Value == null) ? string.Empty : targetRow.Cells[controlUnitCd].Value.ToString();

                if (string.IsNullOrEmpty(unitCd))
                {
                    targetRow.Cells[controlTanka].Value = DBNull.Value;
                    if (!targetRow.Cells[controlTanka].ReadOnly)
                    {
                        targetRow.Cells[controlKingaku].Value = DBNull.Value;
                    }

                    return ret;
                }

                var oldTanka = targetRow.Cells[controlTanka].Value == null ? string.Empty : targetRow.Cells[controlTanka].Value.ToString(); // MAILAN #158994 START
                var updateTanka = string.Empty; // MAILAN #158994 START

                // 単価
                decimal tanka = 0;
                // 個別品名単価から取得
                var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                    Convert.ToInt16(denshuKbnCd),
                    Convert.ToInt16(denpyouKbnCd),
                    torihikisakiCd,
                    gyoushaCd,
                    genbaCd,
                    this.form.UNPAN_GYOUSHA_CD.Text, //運搬業者CD
                    null, //荷降業者CD
                    null, //荷降現場CD
                    hinmeCd,
                    Convert.ToInt16(unitCd),
                    denpyouDate
                    );

                // 個別品名単価から情報が取れない場合は基本品名単価の検索
                if (kobetsuhinmeiTanka == null)
                {
                    var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                        Convert.ToInt16(denshuKbnCd),
                        Convert.ToInt16(denpyouKbnCd),
                        this.form.UNPAN_GYOUSHA_CD.Text, //運搬業者CD
                        null, //荷降業者CD
                        null, //荷降現場CD
                        hinmeCd,
                        Convert.ToInt16(unitCd),
                        denpyouDate
                        );
                    if (kihonHinmeiTanka != null)
                    {
                        decimal.TryParse(Convert.ToString(kihonHinmeiTanka.TANKA.Value), out tanka);
                        updateTanka = kihonHinmeiTanka.TANKA.Value.ToString(); // MAILAN #158994 START
                    }
                    else
                    {
                        isNotFindTanka = true;
                        updateTanka = string.Empty; // MAILAN #158994 START
                    }
                }
                else
                {
                    decimal.TryParse(Convert.ToString(kobetsuhinmeiTanka.TANKA.Value), out tanka);
                    updateTanka = kobetsuhinmeiTanka.TANKA.Value.ToString(); // MAILAN #158994 START
                }

                // MAILAN #158994 START
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    decimal oldTankaValue = -1;
                    decimal updateTankaValue = -1;
                    if (oldTanka != null && !string.IsNullOrEmpty(oldTanka.ToString()))
                    {
                        oldTankaValue = decimal.Parse(oldTanka.ToString());
                    }
                    if (updateTanka != null && !string.IsNullOrEmpty(updateTanka.ToString()))
                    {
                        updateTankaValue = decimal.Parse(updateTanka.ToString());
                    }

                    if (!oldTankaValue.Equals(updateTankaValue))
                    {
                        if (!this.isTankaMessageShown)
                        {
                            this.isTankaMessageShown = true;
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            if (msgLogic.MessageBoxShow("C127") == DialogResult.Yes)
                            {
                                targetRow.Cells[controlTanka].Value = updateTanka;
                            }
                            else
                            {
                                //this.ResetTankaCheck();
                                //return false;
                                this.isContinueCheck = false;
                            }
                        }
                        else
                        {
                            if (this.isContinueCheck)
                            {
                                targetRow.Cells[controlTanka].Value = updateTanka;
                            }
                        }
                    }
                }
                // MAILAN #158994 END
                else // ban chuan
                {
                    // 算定した[単価]が「0」以外の場合、単価設定は行う
                    if (!isNotFindTanka)
                    {
                        // 単価を設定
                        targetRow.Cells[controlTanka].Value = tanka;
                        // 金額の計算はこの後のメソッドで 数量×単価 で実行される
                    }
                    else
                    {
                        targetRow.Cells[controlTanka].Value = DBNull.Value;
                        // 元々単価がReadOnlyだった場合は金額を手入力しているため金額はそっとしておく
                        // 単価がReadOnlyでなかった場合は、金額も連動して0にする。
                        // この金額の計算は、後のメソッドで実行されない。
                        if (!targetRow.Cells[controlTanka].ReadOnly)
                        {
                            targetRow.Cells[controlKingaku].Value = DBNull.Value;
                        }
                    }
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcTanka", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcTanka", ex);
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

        #region 明細実正味計算
        /// <summary>
        /// 明細金額計算
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="flg"></param>
        internal bool CalcDetailNetJyuuryou(int rowIndex, int flg)
        {
            LogUtility.DebugMethodStart(rowIndex, flg);

            decimal stackJyuuryou = 0;
            decimal chouseiJyuuryou = 0;
            decimal netJyuurou = 0;
            bool bExcuteCalc1 = false;
            bool bExcuteCalc2 = false;
            string controlNetJyuryou = string.Empty;
            string controlStackJyuryou = string.Empty;
            string controlChouseiJyuryou = string.Empty;

            try
            {
                // 受入
                if (flg == 1)
                {
                    controlNetJyuryou = ConstClass.CONTROL_UKEIRE_NET_JYUURYOU;
                    controlStackJyuryou = ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU;
                    controlChouseiJyuryou = ConstClass.CONTROL_UKEIRE_CHOUSEI_JYUURYOU;
                }
                // 出荷
                else if (flg == 2)
                {
                    controlNetJyuryou = ConstClass.CONTROL_SHUKKA_NET_JYUURYOU;
                    controlStackJyuryou = ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU;
                    controlChouseiJyuryou = ConstClass.CONTROL_SHUKKA_CHOUSEI_JYUURYOU;
                }
                else
                {
                    return true;
                }

                Row targetRow = this.form.Ichiran.Rows[rowIndex];

                // 正味
                if (null != targetRow.Cells[controlStackJyuryou].FormattedValue && !string.IsNullOrEmpty(targetRow.Cells[controlStackJyuryou].FormattedValue.ToString()))
                {
                    decimal.TryParse(Convert.ToString(targetRow.Cells[controlStackJyuryou].FormattedValue), out stackJyuuryou);
                    bExcuteCalc1 = true;
                }

                // 調整
                if (null != targetRow.Cells[controlChouseiJyuryou].FormattedValue && !string.IsNullOrEmpty(targetRow.Cells[controlChouseiJyuryou].FormattedValue.ToString()))
                {
                    decimal.TryParse(Convert.ToString(targetRow.Cells[controlChouseiJyuryou].FormattedValue), out chouseiJyuuryou);
                    bExcuteCalc2 = true;
                }

                // 実正味
                //if (bExcuteCalc1 && bExcuteCalc2)
                if (bExcuteCalc1)
                {
                    //targetRow.Cells[controlChouseiJyuryou].ReadOnly = false;
                    string name = "NET_JYUURYOU";
                    netJyuurou = stackJyuuryou - chouseiJyuuryou;
                    targetRow.Cells[controlNetJyuryou].Value = this.meisaiSuuryouFormat(name, netJyuurou.ToString());

                    // 数量制御
                    if (!this.SetHinmeiSuuryou(rowIndex, flg))
                    {
                        return false;
                    }

                    // 数量計算
                    if (!this.CalcSuuryou(rowIndex, flg))
                    {
                        return false;
                    }

                    // 明細金額計算
                    if (!this.CalcDetailKingaku(rowIndex, flg))
                    {
                        return false;
                    }

                    // 合計系の計算
                    if (!this.CalcAllDetailAndTotal())
                    {
                        return false;
                    }
                }
                else
                {
                    targetRow.Cells[controlNetJyuryou].Value = string.Empty;
                    targetRow.Cells[controlChouseiJyuryou].Value = string.Empty;
                    //targetRow.Cells[controlChouseiJyuryou].ReadOnly = true;
                    // 数量制御
                    if (!this.SetHinmeiSuuryou(rowIndex, flg))
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetailNetJyuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 数量計算

        /// <summary>
        /// 数量計算
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="flg">
        /// 1:受入
        /// 2:出荷
        /// </param>
        internal bool CalcSuuryou(int rowIndex, int flg)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(rowIndex, flg);

                Row targetRow = this.form.Ichiran.Rows[rowIndex];

                if (targetRow == null)
                {
                    return ret;
                }

                string controlNetJyuryou = string.Empty;
                string controlUnitCd = string.Empty;
                string controlSuuryou = string.Empty;

                if (flg == 1)
                {
                    controlNetJyuryou = ConstClass.CONTROL_UKEIRE_NET_JYUURYOU;
                    controlUnitCd = ConstClass.CONTROL_UKEIRE_UNIT_CD;
                    controlSuuryou = ConstClass.CONTROL_UKEIRE_SUURYOU;
                }
                else if (flg == 2)
                {
                    controlNetJyuryou = ConstClass.CONTROL_SHUKKA_NET_JYUURYOU;
                    controlUnitCd = ConstClass.CONTROL_SHUKKA_UNIT_CD;
                    controlSuuryou = ConstClass.CONTROL_SHUKKA_SUURYOU;
                }
                else
                {
                    return ret;
                }

                if (targetRow.Cells[controlNetJyuryou].Value == null ||
                    string.IsNullOrEmpty(targetRow.Cells[controlNetJyuryou].Value.ToString()))
                {
                    return ret;
                }

                /**
                 * 数量設定
                 */
                if (string.Compare(ConstClass.UNIT_CD_KG,
                    Convert.ToString(targetRow.Cells[controlUnitCd].Value), true) == 0)
                {
                    targetRow.Cells[controlSuuryou].Value = targetRow.Cells[controlNetJyuryou].Value;
                }
                else if (string.Compare(ConstClass.UNIT_CD_TON,
                    Convert.ToString(targetRow.Cells[controlUnitCd].Value), true) == 0)
                {
                    decimal kg = Convert.ToDecimal(targetRow.Cells[controlNetJyuryou].Value);
                    decimal ton = kg / 1000;
                    targetRow.Cells[controlSuuryou].Value = ton;
                }
                targetRow.Cells[controlSuuryou].UpdateBackColor(false);

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcSuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 単位kg-品名数量制御処理

        /// <summary>
        /// 単位kg-品名数量制御処理
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="flg">
        /// 1:受入
        /// 2:出荷
        /// </param>
        public bool SetHinmeiSuuryou(int rowIndex, int flg)
        {
            bool ret = true;
            try
            {
                Row targetRow = this.form.Ichiran.Rows[rowIndex];

                string controlNetJyuryou = string.Empty;
                string controlUnitCd = string.Empty;
                string controlSuuryou = string.Empty;

                if (flg == 1)
                {
                    controlNetJyuryou = ConstClass.CONTROL_UKEIRE_NET_JYUURYOU;
                    controlUnitCd = ConstClass.CONTROL_UKEIRE_UNIT_CD;
                    controlSuuryou = ConstClass.CONTROL_UKEIRE_SUURYOU;
                }
                else if (flg == 2)
                {
                    controlNetJyuryou = ConstClass.CONTROL_SHUKKA_NET_JYUURYOU;
                    controlUnitCd = ConstClass.CONTROL_SHUKKA_UNIT_CD;
                    controlSuuryou = ConstClass.CONTROL_SHUKKA_SUURYOU;
                }
                else
                {
                    return ret;
                }

                object jyuuryou = targetRow.Cells[controlNetJyuryou].Value;
                object unitcd = targetRow.Cells[controlUnitCd].Value;

                decimal value = 0;
                if (jyuuryou != null && decimal.TryParse(Convert.ToString(jyuuryou), out value))
                {
                    // 正味重量あり
                    if (unitcd != null && (ConstClass.UNIT_CD_KG.Equals(unitcd.ToString()) || ConstClass.UNIT_CD_TON.Equals(unitcd.ToString())))
                    {
                        // 単位kg、tの場合は品名数量変更不可
                        targetRow.Cells[controlSuuryou].ReadOnly = true;
                    }
                    else
                    {
                        // 単位kg、t以外の場合は、品名数量変更可
                        targetRow.Cells[controlSuuryou].ReadOnly = false;
                    }
                }
                else
                {
                    // 正味重量なしの場合は品名数量手入力可（単位は何でもOK)
                    targetRow.Cells[controlSuuryou].ReadOnly = false;
                }
                targetRow.Cells[controlSuuryou].UpdateBackColor(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHinmeiSuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 明細金額計算
        /// <summary>
        /// 明細金額計算
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="flg"></param>
        internal bool CalcDetailKingaku(int rowIndex, int flg)
        {
            /* 登録実行時に金額計算のチェック(CheckRequiredDataForDeitalメソッド)が実行されます。 　　*/
            /* チェックの計算方法は本メソッドと同じため、修正する際はチェック処理も修正してください。 */

            LogUtility.DebugMethodStart(rowIndex, flg);

            decimal suuryou = 0;
            decimal tanka = 0;
            short kingakuHasuuCd = 3;
            bool bExecuteCalc = false;
            bool ret = true;
            try
            {
                // 受入
                if (flg == 1)
                {
                    // 数量
                    if (null != this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].FormattedValue)
                    {
                        decimal.TryParse(Convert.ToString(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].FormattedValue), out suuryou);
                        bExecuteCalc = true;
                    }

                    // 単価
                    if (null != this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_TANKA].FormattedValue)
                    {
                        decimal.TryParse(Convert.ToString(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_TANKA].FormattedValue), out tanka);
                        bExecuteCalc = true;
                    }

                    if (!bExecuteCalc) { return ret; }

                    // 端数設定
                    kingakuHasuuCd = this.CalcHasuu(rowIndex, flg);

                    // 金額
                    if (this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].FormattedValue != null && !string.IsNullOrEmpty(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].FormattedValue.ToString()) &&
                        this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_TANKA].FormattedValue != null && !string.IsNullOrEmpty(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_TANKA].FormattedValue.ToString()))
                    {
                        /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                        decimal tmpKingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);
                        if (tmpKingaku.ToString().Length > 9)
                        {
                            tmpKingaku = Convert.ToDecimal(tmpKingaku.ToString().Substring(0, 9));
                        }

                        this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value = tmpKingaku;
                    }
                }
                // 出荷
                else if (flg == 2)
                {
                    // 数量
                    if (null != this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].FormattedValue)
                    {
                        decimal.TryParse(Convert.ToString(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].FormattedValue), out suuryou);
                        bExecuteCalc = true;
                    }
                    // 単価
                    if (null != this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_TANKA].FormattedValue)
                    {
                        decimal.TryParse(Convert.ToString(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_TANKA].FormattedValue), out tanka);
                        bExecuteCalc = true;
                    }

                    if (!bExecuteCalc) { return ret; }

                    // 端数設定
                    kingakuHasuuCd = this.CalcHasuu(rowIndex, flg);

                    // 金額
                    if (this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].FormattedValue != null && !string.IsNullOrEmpty(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].FormattedValue.ToString()) &&
                        this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_TANKA].FormattedValue != null && !string.IsNullOrEmpty(this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_TANKA].FormattedValue.ToString()))
                    {
                        /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                        decimal tmpKingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);
                        if (tmpKingaku.ToString().Length > 9)
                        {
                            tmpKingaku = Convert.ToDecimal(tmpKingaku.ToString().Substring(0, 9));
                        }

                        this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value = tmpKingaku;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetailKingaku", ex);
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

        #region 新規/修正/削除処理
        /// <summary>
        /// 新規/修正/削除処理
        /// </summary>
        /// <returns></returns>
        public bool UpdRegist()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 新規
                    Boolean isOK = this.InsUpdAll(WINDOW_TYPE.NEW_WINDOW_FLAG);
                    if (isOK)
                    {
                        //メッセージ
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("I001", "登録");

                        LogUtility.DebugMethodEnd(true);

                        return true;
                    }

                }
                //修正
                else if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    //修正
                    Boolean isOK = this.InsUpdAll(this.form.WindowType);
                    if (isOK)
                    {
                        //メッセージ
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("I001", "更新");

                        LogUtility.DebugMethodEnd(true);

                        return true;
                    }

                }
                //削除
                else if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    //削除
                    Boolean isOK = this.InsUpdAll(this.form.WindowType);
                    if (isOK)
                    {
                        //メッセージ
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("I001", "削除");

                        LogUtility.DebugMethodEnd(true);

                        return true;
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("UpdRegist", ex1);
                this.errmessage.MessageBoxShow("E080", "");;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("UpdRegist", ex2);
                this.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdRegist", ex);
                this.errmessage.MessageBoxShow("E245", "");;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }
        #endregion

        #region テーブル登録・更新・削除
        /// <summary>
        /// テーブル登録・更新・削除
        /// </summary>
        /// 
        /// <returns></returns>
        [Transaction]
        public Boolean InsUpdAll(WINDOW_TYPE windowType)
        {
            LogUtility.DebugMethodStart(windowType);
            try
            {
                using (Transaction tran = new Transaction())
                {
                    // 画面から代納,受入、出荷情報を取得
                    this.GetDainouEntryData(windowType);

                    switch (windowType)
                    {
                        // 新規
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            // 登録
                            this.accessor.InsertDainouEntry(this.insDainouUkeireEntry);
                            this.accessor.InsertDainouEntry(this.insDainouShukkaEntry);
                            break;
                        // 更新
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            // 論理削除
                            this.accessor.UpdateDainouEntry(this.dainouUkeireEntry);
                            this.accessor.UpdateDainouEntry(this.dainouShukkaEntry);

                            // 登録
                            this.accessor.InsertDainouEntry(this.insDainouUkeireEntry);
                            this.accessor.InsertDainouEntry(this.insDainouShukkaEntry);
                            break;
                        // 削除
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            // 論理削除
                            this.accessor.UpdateDainouEntry(this.dainouUkeireEntry);
                            this.accessor.UpdateDainouEntry(this.dainouShukkaEntry);
                            this.dainouUkeireEntry.SEQ = this.dainouUkeireEntry.SEQ + 1;
                            this.dainouShukkaEntry.SEQ = this.dainouShukkaEntry.SEQ + 1;
                            // 登録
                            this.accessor.InsertDainouEntry(this.dainouUkeireEntry);
                            this.accessor.InsertDainouEntry(this.dainouShukkaEntry);

                            break;
                    }

                    // 画面から代納受入、出荷の明細情報を取得
                    this.GetDainouDetailEntryData(windowType);

                    if (windowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) || windowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                    {
                        // 受入明細登録
                        foreach (T_UR_SH_DETAIL detail in this.insDainouUkeireDetail)
                        {
                            this.accessor.InsertDainouDetail(detail);
                        }
                        // 出荷明細登録
                        foreach (T_UR_SH_DETAIL detail in this.insDainouShukkaDetail)
                        {
                            this.accessor.InsertDainouDetail(detail);
                        }
                    }

                    tran.Commit();

                }//トランザクション終了（未コミットの場合ロールバック）
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }

                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 画面から代納入力,受入、出荷の情報を取得する

        /// <summary>
        /// 受入Entry情報の設定
        /// </summary>
        /// <returns></returns>
        public T_UR_SH_ENTRY CreateDainouUkeireEntry()
        {
            // 代納入力_受入エンティティ
            T_UR_SH_ENTRY tDainouUkeireEntry = new T_UR_SH_ENTRY();

            // システムID
            tDainouUkeireEntry.SYSTEM_ID = this.UkeireSystemId;
            // SEQ
            tDainouUkeireEntry.SEQ = this.UkeireSeq;
            // 拠点CD
            tDainouUkeireEntry.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
            // 売上／支払番号
            tDainouUkeireEntry.UR_SH_NUMBER = this.PrmDainouNumber;
            // 日連番
            tDainouUkeireEntry.DATE_NUMBER = this.NumberDay;
            // 年連番
            tDainouUkeireEntry.YEAR_NUMBER = this.NumberYear;
            // 確定区分
            tDainouUkeireEntry.KAKUTEI_KBN = SqlInt16.Parse(this.form.KAKUTEI_KBN.Text);
            // 伝票日付
            tDainouUkeireEntry.DENPYOU_DATE = DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
            // 支払日付
            tDainouUkeireEntry.SHIHARAI_DATE = DateTime.Parse(this.form.SHIHARAI_DATE.Value.ToString());
            // 取引先
            tDainouUkeireEntry.TORIHIKISAKI_CD = this.form.UKEIRE_TORIHIKISAKI_CD.Text;
            tDainouUkeireEntry.TORIHIKISAKI_NAME = this.form.UKEIRE_TORIHIKISAKI_NAME_RYAKU.Text;
            // 業者
            tDainouUkeireEntry.GYOUSHA_CD = this.form.UKEIRE_GYOUSHA_CD.Text;
            tDainouUkeireEntry.GYOUSHA_NAME = this.form.UKEIRE_GYOUSHA_NAME_RYAKU.Text;
            // 現場
            tDainouUkeireEntry.GENBA_CD = this.form.UKEIRE_GENBA_CD.Text;
            tDainouUkeireEntry.GENBA_NAME = this.form.UKEIRE_GENBA_NAME_RYAKU.Text;
            // 荷積業者
            tDainouUkeireEntry.NIZUMI_GYOUSHA_CD = this.form.UKEIRE_GYOUSHA_CD.Text;
            tDainouUkeireEntry.NIZUMI_GYOUSHA_NAME = this.form.UKEIRE_GYOUSHA_NAME_RYAKU.Text;
            // 荷積現場
            tDainouUkeireEntry.NIZUMI_GENBA_CD = this.form.UKEIRE_GENBA_CD.Text;
            tDainouUkeireEntry.NIZUMI_GENBA_NAME = this.form.UKEIRE_GENBA_NAME_RYAKU.Text;
            // 営業担当者
            tDainouUkeireEntry.EIGYOU_TANTOUSHA_CD = string.IsNullOrEmpty(this.form.UKEIRE_EIGYOU_TANTOUSHA_CD.Text) ? null : this.form.UKEIRE_EIGYOU_TANTOUSHA_CD.Text;
            tDainouUkeireEntry.EIGYOU_TANTOUSHA_NAME = string.IsNullOrEmpty(this.form.UKEIRE_EIGYOU_TANTOUSHA_NAME_RYAKU.Text) ? null : this.form.UKEIRE_EIGYOU_TANTOUSHA_NAME_RYAKU.Text;
            // 入力担当者
            tDainouUkeireEntry.NYUURYOKU_TANTOUSHA_CD = this.form.NYUURYOKU_TANTOUSHA_CD.Text;
            tDainouUkeireEntry.NYUURYOKU_TANTOUSHA_NAME = this.form.NYUURYOKU_TANTOUSHA_NAME_RYAKU.Text;
            // 車輌
            tDainouUkeireEntry.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            tDainouUkeireEntry.SHARYOU_NAME = this.form.SHARYOU_NAME_RYAKU.Text;
            // 車種
            tDainouUkeireEntry.SHASHU_CD = this.form.SHASHU_CD.Text;
            tDainouUkeireEntry.SHASHU_NAME = this.form.SHASHU_NAME_RYAKU.Text;
            // 運搬業者
            tDainouUkeireEntry.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            tDainouUkeireEntry.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text;
            // 運転者
            tDainouUkeireEntry.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
            tDainouUkeireEntry.UNTENSHA_NAME = this.form.UNTENSHA_NAME_RYAKU.Text;
            // 伝票備考
            tDainouUkeireEntry.DENPYOU_BIKOU = this.form.DENPYOU_BIKOU.Text;

            // 税計算区分CD
            if (!string.IsNullOrEmpty(this.form.denpyouHakouPopUpDTO.Ukeire_Zeikeisan_Kbn))
            {
                tDainouUkeireEntry.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(this.form.denpyouHakouPopUpDTO.Ukeire_Zeikeisan_Kbn);
            }
            else
            {
                tDainouUkeireEntry.SHIHARAI_ZEI_KEISAN_KBN_CD = 0;
            }
            // 税区分CD
            if (!string.IsNullOrEmpty(this.form.denpyouHakouPopUpDTO.Ukeire_Zei_Kbn))
            {
                tDainouUkeireEntry.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(this.form.denpyouHakouPopUpDTO.Ukeire_Zei_Kbn);
            }
            else
            {
                tDainouUkeireEntry.SHIHARAI_ZEI_KBN_CD = 0;
            }

            // 金額、消費税項目
            this.GetUkrireTotalValues(tDainouUkeireEntry);

            // 月極一括作成区分
            tDainouUkeireEntry.TSUKI_CREATE_KBN = false;
            // 代納フラグ
            tDainouUkeireEntry.DAINOU_FLG = true;
            // 削除フラグ
            tDainouUkeireEntry.DELETE_FLG = false;

            return tDainouUkeireEntry;
        }

        /// <summary>
        /// 出荷Entry情報の設定
        /// </summary>
        /// <returns></returns>
        public T_UR_SH_ENTRY CreateDainouShukkaEntry()
        {
            // 代納入力_出荷エンティティ
            T_UR_SH_ENTRY tDainouShukkaEntry = new T_UR_SH_ENTRY();

            // システムID
            tDainouShukkaEntry.SYSTEM_ID = this.ShukkaSystemId;
            // SEQ
            tDainouShukkaEntry.SEQ = this.ShukkaSeq;
            // 拠点CD
            tDainouShukkaEntry.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
            // 売上／支払番号
            tDainouShukkaEntry.UR_SH_NUMBER = this.PrmDainouNumber;
            // 日連番
            tDainouShukkaEntry.DATE_NUMBER = this.NumberDay;
            // 年連番
            tDainouShukkaEntry.YEAR_NUMBER = this.NumberYear;
            // 確定区分
            tDainouShukkaEntry.KAKUTEI_KBN = SqlInt16.Parse(this.form.KAKUTEI_KBN.Text);
            // 伝票日付
            tDainouShukkaEntry.DENPYOU_DATE = DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
            // 売上日付
            tDainouShukkaEntry.URIAGE_DATE = DateTime.Parse(this.form.URIAGE_DATE.Value.ToString());
            // 取引先
            tDainouShukkaEntry.TORIHIKISAKI_CD = this.form.SHUKKA_TORIHIKISAKI_CD.Text;
            tDainouShukkaEntry.TORIHIKISAKI_NAME = this.form.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Text;
            // 業者
            tDainouShukkaEntry.GYOUSHA_CD = this.form.SHUKKA_GYOUSHA_CD.Text;
            tDainouShukkaEntry.GYOUSHA_NAME = this.form.SHUKKA_GYOUSHA_NAME_RYAKU.Text;
            // 現場
            tDainouShukkaEntry.GENBA_CD = this.form.SHUKKA_GENBA_CD.Text;
            tDainouShukkaEntry.GENBA_NAME = this.form.SHUKKA_GENBA_NAME_RYAKU.Text;
            // 荷積業者
            tDainouShukkaEntry.NIOROSHI_GYOUSHA_CD = this.form.SHUKKA_GYOUSHA_CD.Text;
            tDainouShukkaEntry.NIOROSHI_GYOUSHA_NAME = this.form.SHUKKA_GYOUSHA_NAME_RYAKU.Text;
            // 荷積現場
            tDainouShukkaEntry.NIOROSHI_GENBA_CD = this.form.SHUKKA_GENBA_CD.Text;
            tDainouShukkaEntry.NIOROSHI_GENBA_NAME = this.form.SHUKKA_GENBA_NAME_RYAKU.Text;
            // 営業担当者
            tDainouShukkaEntry.EIGYOU_TANTOUSHA_CD = string.IsNullOrEmpty(this.form.SHUKKA_EIGYOU_TANTOUSHA_CD.Text) ? null : this.form.SHUKKA_EIGYOU_TANTOUSHA_CD.Text;
            tDainouShukkaEntry.EIGYOU_TANTOUSHA_NAME = string.IsNullOrEmpty(this.form.SHUKKA_EIGYOU_TANTOUSHA_NAME_RYAKU.Text) ? null : this.form.SHUKKA_EIGYOU_TANTOUSHA_NAME_RYAKU.Text;
            // 入力担当者
            tDainouShukkaEntry.NYUURYOKU_TANTOUSHA_CD = this.form.NYUURYOKU_TANTOUSHA_CD.Text;
            tDainouShukkaEntry.NYUURYOKU_TANTOUSHA_NAME = this.form.NYUURYOKU_TANTOUSHA_NAME_RYAKU.Text;
            // 車輌
            tDainouShukkaEntry.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            tDainouShukkaEntry.SHARYOU_NAME = this.form.SHARYOU_NAME_RYAKU.Text;
            // 車種
            tDainouShukkaEntry.SHASHU_CD = this.form.SHASHU_CD.Text;
            tDainouShukkaEntry.SHASHU_NAME = this.form.SHASHU_NAME_RYAKU.Text;
            // 運搬業者
            tDainouShukkaEntry.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            tDainouShukkaEntry.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text;
            // 運転者
            tDainouShukkaEntry.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
            tDainouShukkaEntry.UNTENSHA_NAME = this.form.UNTENSHA_NAME_RYAKU.Text;
            // 伝票備考
            tDainouShukkaEntry.DENPYOU_BIKOU = this.form.DENPYOU_BIKOU.Text;

            // 税計算区分CD
            if (!string.IsNullOrEmpty(this.form.denpyouHakouPopUpDTO.Shukka_Zeikeisan_Kbn))
            {
                tDainouShukkaEntry.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(this.form.denpyouHakouPopUpDTO.Shukka_Zeikeisan_Kbn);
            }
            else
            {
                tDainouShukkaEntry.URIAGE_ZEI_KEISAN_KBN_CD = 0;
            }
            // 税区分CD
            if (!string.IsNullOrEmpty(this.form.denpyouHakouPopUpDTO.Shukka_Zei_Kbn))
            {
                tDainouShukkaEntry.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(this.form.denpyouHakouPopUpDTO.Shukka_Zei_Kbn);
            }
            else
            {
                tDainouShukkaEntry.URIAGE_ZEI_KBN_CD = 0;
            }

            // 金額、消費税項目
            this.GetShukkaTotalValues(tDainouShukkaEntry);

            // 月極一括作成区分
            tDainouShukkaEntry.TSUKI_CREATE_KBN = false;
            // 代納フラグ
            tDainouShukkaEntry.DAINOU_FLG = true;
            // 削除フラグ
            tDainouShukkaEntry.DELETE_FLG = false;

            return tDainouShukkaEntry;
        }

        /// <summary>
        /// 画面から代納入力,受入、出荷の情報を取得する
        /// </summary>
        /// 
        /// <returns></returns>
        public void GetDainouEntryData(WINDOW_TYPE windowType)
        {
            LogUtility.DebugMethodStart(windowType);

            SqlDateTime createDate = SqlDateTime.Null;
            string createPc = string.Empty;
            string createUser = string.Empty;

            switch (windowType)
            {
                // 新規
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // システムID
                    this.UkeireSystemId = long.Parse(this.accessor.CreateSystemIdForDainou().ToString());
                    this.ShukkaSystemId = long.Parse(this.accessor.CreateSystemIdForDainou().ToString());
                    // SEQ
                    this.UkeireSeq = 1;
                    this.ShukkaSeq = 1;
                    // 伝票番号
                    this.PrmDainouNumber = long.Parse(this.accessor.CreateDainouNumber().ToString());
                    // 日連番
                    this.NumberDay = int.Parse(this.accessor.CreateDayNumberForDainou(DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text)).ToString());
                    // 年連番
                    SqlInt32 numberedYear = this.GetKaikeiYear(DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), (short)this.corpInfoEntity.KISHU_MONTH);
                    this.NumberYear = int.Parse(this.accessor.CreateYearNumberForDainou(numberedYear, SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text)).ToString());

                    this.insDainouUkeireEntry = this.CreateDainouUkeireEntry();
                    this.insDainouShukkaEntry = this.CreateDainouShukkaEntry();

                    // 更新情報の設定
                    var dataBinderUkeire = new DataBinderLogic<T_UR_SH_ENTRY>(this.insDainouUkeireEntry);
                    var dataBinderShukka = new DataBinderLogic<T_UR_SH_ENTRY>(this.insDainouShukkaEntry);
                    dataBinderUkeire.SetSystemProperty(this.insDainouUkeireEntry, false);
                    dataBinderShukka.SetSystemProperty(this.insDainouShukkaEntry, false);

                    break;

                // 修正
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // システムID
                    this.UkeireSystemId = long.Parse(this.dainouUkeireEntry.SYSTEM_ID.ToString());
                    this.ShukkaSystemId = long.Parse(this.dainouShukkaEntry.SYSTEM_ID.ToString());
                    // SEQ
                    this.UkeireSeq = (int)this.dainouUkeireEntry.SEQ + 1;
                    this.ShukkaSeq = (int)this.dainouShukkaEntry.SEQ + 1;
                    // 伝票番号
                    this.PrmDainouNumber = long.Parse(this.dainouUkeireEntry.UR_SH_NUMBER.ToString());
                    // 日連番
                    if (befDainouukeireEntry.KYOTEN_CD != SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text) ||
                        !befDainouukeireEntry.DENPYOU_DATE.Equals(DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString())))
                    {
                        this.NumberDay = int.Parse(this.accessor.CreateDayNumberForDainou(DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text)).ToString());
                    }
                    else
                    {
                        this.NumberDay = (this.dainouUkeireEntry.DATE_NUMBER.IsNull) ? 0 : int.Parse(this.dainouUkeireEntry.DATE_NUMBER.ToString());
                    }
                    // 年連番
                    SqlInt32 befNumberedYear = this.GetKaikeiYear((DateTime)befDainouukeireEntry.DENPYOU_DATE, (short)this.corpInfoEntity.KISHU_MONTH);
                    SqlInt32 nowNumberedYear = this.GetKaikeiYear(DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), (short)this.corpInfoEntity.KISHU_MONTH);
                    if (befDainouukeireEntry.KYOTEN_CD != SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text) ||
                        this.GetKaikeiYear((DateTime)befDainouukeireEntry.DENPYOU_DATE, (short)befNumberedYear) !=
                        this.GetKaikeiYear(DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), (short)nowNumberedYear))
                    {
                        this.NumberYear = int.Parse(this.accessor.CreateYearNumberForDainou(nowNumberedYear, SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text)).ToString());
                    }
                    else
                    {
                        this.NumberYear = (this.dainouUkeireEntry.YEAR_NUMBER.IsNull) ? 0 : int.Parse(this.dainouUkeireEntry.YEAR_NUMBER.ToString());
                    }
                    createDate = this.dainouUkeireEntry.CREATE_DATE;
                    createUser = this.dainouUkeireEntry.CREATE_USER;
                    createPc = this.dainouUkeireEntry.CREATE_PC;

                    // 論理削除用
                    this.dainouUkeireEntry.DELETE_FLG = true;
                    this.dainouShukkaEntry.DELETE_FLG = true;
                    // 更新情報の設定
                    var dataBinderUkeireUp = new DataBinderLogic<T_UR_SH_ENTRY>(this.dainouUkeireEntry);
                    var dataBinderShukkaUp = new DataBinderLogic<T_UR_SH_ENTRY>(this.dainouShukkaEntry);
                    dataBinderUkeireUp.SetSystemProperty(this.dainouUkeireEntry, false);
                    dataBinderShukkaUp.SetSystemProperty(this.dainouShukkaEntry, false);
                    this.dainouUkeireEntry.CREATE_DATE = createDate;
                    this.dainouUkeireEntry.CREATE_USER = createUser;
                    this.dainouUkeireEntry.CREATE_PC = createPc;
                    this.dainouShukkaEntry.CREATE_DATE = createDate;
                    this.dainouShukkaEntry.CREATE_USER = createUser;
                    this.dainouShukkaEntry.CREATE_PC = createPc;

                    // 登録用
                    this.insDainouUkeireEntry = this.CreateDainouUkeireEntry();
                    this.insDainouShukkaEntry = this.CreateDainouShukkaEntry();
                    // 更新情報の設定
                    var dataBinderUkeireIns = new DataBinderLogic<T_UR_SH_ENTRY>(this.insDainouUkeireEntry);
                    var dataBinderShukkaIns = new DataBinderLogic<T_UR_SH_ENTRY>(this.insDainouShukkaEntry);
                    dataBinderUkeireIns.SetSystemProperty(this.insDainouUkeireEntry, false);
                    dataBinderShukkaIns.SetSystemProperty(this.insDainouShukkaEntry, false);
                    this.insDainouUkeireEntry.CREATE_DATE = createDate;
                    this.insDainouUkeireEntry.CREATE_USER = createUser;
                    this.insDainouUkeireEntry.CREATE_PC = createPc;
                    this.insDainouShukkaEntry.CREATE_DATE = createDate;
                    this.insDainouShukkaEntry.CREATE_USER = createUser;
                    this.insDainouShukkaEntry.CREATE_PC = createPc;

                    break;

                // 削除
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:

                    createDate = this.dainouUkeireEntry.CREATE_DATE;
                    createUser = this.dainouUkeireEntry.CREATE_USER;
                    createPc = this.dainouUkeireEntry.CREATE_PC;

                    // 論理削除用
                    this.dainouUkeireEntry.DELETE_FLG = true;
                    this.dainouShukkaEntry.DELETE_FLG = true;
                    // 更新情報の設定
                    var dataBinderUkeireDel = new DataBinderLogic<T_UR_SH_ENTRY>(this.dainouUkeireEntry);
                    var dataBinderShukkaDel = new DataBinderLogic<T_UR_SH_ENTRY>(this.dainouShukkaEntry);
                    dataBinderUkeireDel.SetSystemProperty(this.dainouUkeireEntry, false);
                    dataBinderShukkaDel.SetSystemProperty(this.dainouShukkaEntry, false);
                    this.dainouUkeireEntry.CREATE_DATE = createDate;
                    this.dainouUkeireEntry.CREATE_USER = createUser;
                    this.dainouUkeireEntry.CREATE_PC = createPc;
                    this.dainouShukkaEntry.CREATE_DATE = createDate;
                    this.dainouShukkaEntry.CREATE_USER = createUser;
                    this.dainouShukkaEntry.CREATE_PC = createPc;
                    break;
            }

            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region 画面から代納受入、出荷の明細情報を取得する

        /// <summary>
        /// 受入明細情報の設定
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public T_UR_SH_DETAIL CreateDainouUkeireDetail(int rowIndex)
        {
            // 代納入力_受入明細
            T_UR_SH_DETAIL tDainouUkeireDetail = new T_UR_SH_DETAIL();

            if (this.form.Ichiran.RowCount <= 0 || rowIndex >= this.form.Ichiran.RowCount)
            {
                return tDainouUkeireDetail;
            }

            GrapeCity.Win.MultiRow.Row crtRow = this.form.Ichiran.Rows[rowIndex];

            // システムID
            tDainouUkeireDetail.SYSTEM_ID = this.UkeireSystemId;
            // SEQ
            tDainouUkeireDetail.SEQ = this.UkeireSeq;
            // 明細システムID
            tDainouUkeireDetail.DETAIL_SYSTEM_ID = this.UkeireMeisaiSystemId;
            // 行番号
            tDainouUkeireDetail.ROW_NO = (Int16)(rowIndex + 1);
            // 代納番号
            tDainouUkeireDetail.UR_SH_NUMBER = this.PrmDainouNumber;
            // 確定区分
            tDainouUkeireDetail.KAKUTEI_KBN = SqlInt16.Parse(this.form.KAKUTEI_KBN.Text); ;
            // 売上支払日付
            tDainouUkeireDetail.URIAGESHIHARAI_DATE = DateTime.Parse(this.form.SHIHARAI_DATE.Value.ToString());
            // 伝票区分
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString()))
            {
                tDainouUkeireDetail.DENPYOU_KBN_CD = SqlInt16.Parse(crtRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString());
            }
            // 受入品名CD
            tDainouUkeireDetail.HINMEI_CD = crtRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value.ToString();
            // 受入品名
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value)
            {
                tDainouUkeireDetail.HINMEI_NAME = crtRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value.ToString();
            }
            // 正味重量
            decimal stackJuryou = 0;
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value), out stackJuryou);
                tDainouUkeireDetail.STACK_JYUURYOU = stackJuryou;
            }
            // 調整
            decimal chouseiJyuuryou = 0;
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_CHOUSEI_JYUURYOU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_CHOUSEI_JYUURYOU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_UKEIRE_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);
                tDainouUkeireDetail.CHOUSEI_JYUURYOU = chouseiJyuuryou;
            }
            // 実正味
            decimal jyuuryou = 0;
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_NET_JYUURYOU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_NET_JYUURYOU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_UKEIRE_NET_JYUURYOU].Value), out jyuuryou);
                tDainouUkeireDetail.NET_JYUURYOU = jyuuryou;
            }
            // 数量(受入)
            decimal suuryou = 0;
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_SUURYOU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_SUURYOU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_UKEIRE_SUURYOU].Value), out suuryou);
            }
            tDainouUkeireDetail.SUURYOU = suuryou;
            // 単位CD(受入)
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString()))
            {
                tDainouUkeireDetail.UNIT_CD = SqlInt16.Parse(crtRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString());
            }
            // 単価(受入)
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_TANKA].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_TANKA].Value.ToString()))
            {
                tDainouUkeireDetail.TANKA = decimal.Parse(Convert.ToString(crtRow[ConstClass.CONTROL_UKEIRE_TANKA].Value));
            }
            
            // 税区分CD
            int uZeiKbnCd = 0;
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value.ToString()))
            {
                uZeiKbnCd = Int16.Parse(crtRow[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value.ToString());
                tDainouUkeireDetail.HINMEI_ZEI_KBN_CD = Int16.Parse(uZeiKbnCd.ToString());
            }
            // 金額(受入)
            decimal kingaku = 0;
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_KINGAKU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_KINGAKU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_UKEIRE_KINGAKU].Value), out kingaku);
            }

            if (uZeiKbnCd == 0)
            {
                tDainouUkeireDetail.KINGAKU = kingaku;
                tDainouUkeireDetail.HINMEI_KINGAKU = 0;
            }
            else
            {
                tDainouUkeireDetail.KINGAKU = 0;
                tDainouUkeireDetail.HINMEI_KINGAKU = kingaku;
            }

            // 備考(受入)
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_MEISAI_BIKOU].Value)
            {
                tDainouUkeireDetail.MEISAI_BIKOU = crtRow[ConstClass.CONTROL_UKEIRE_MEISAI_BIKOU].Value.ToString();
            }

            // 伝票区分CD
            int uDenpyouKbnCd = 0;
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString()))
            {
                uDenpyouKbnCd = Int16.Parse(crtRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString());
            }

            // 金額
            decimal uKingaku = 0;
            if (null != crtRow[ConstClass.CONTROL_UKEIRE_KINGAKU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_UKEIRE_KINGAKU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_UKEIRE_KINGAKU].Value), out uKingaku);

            }
            // 消費税の項目
            this.CalcDetailShouhizei(uDenpyouKbnCd, uZeiKbnCd, uKingaku, tDainouUkeireDetail);

            return tDainouUkeireDetail;
        }

        /// <summary>
        /// 出荷明細情報の設定
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public T_UR_SH_DETAIL CreateDainouShukkaDetail(int rowIndex)
        {
            // 代納入力_受入明細
            T_UR_SH_DETAIL tDainouShukkaDetail = new T_UR_SH_DETAIL();

            if (this.form.Ichiran.RowCount <= 0 || rowIndex >= this.form.Ichiran.RowCount)
            {
                return tDainouShukkaDetail;
            }

            GrapeCity.Win.MultiRow.Row crtRow = this.form.Ichiran.Rows[rowIndex];

            // システムID
            tDainouShukkaDetail.SYSTEM_ID = this.ShukkaSystemId;
            // SEQ
            tDainouShukkaDetail.SEQ = this.ShukkaSeq;
            // 明細システムID
            tDainouShukkaDetail.DETAIL_SYSTEM_ID = this.ShukkaMeisaiSystemId;
            // 行番号
            tDainouShukkaDetail.ROW_NO = (Int16)(rowIndex + 1);
            // 代納番号
            tDainouShukkaDetail.UR_SH_NUMBER = this.PrmDainouNumber;
            // 確定区分
            tDainouShukkaDetail.KAKUTEI_KBN = SqlInt16.Parse(this.form.KAKUTEI_KBN.Text);
            // 売上支払日付
            tDainouShukkaDetail.URIAGESHIHARAI_DATE = DateTime.Parse(this.form.URIAGE_DATE.Value.ToString());
            // 伝票区分
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString()))
            {
                tDainouShukkaDetail.DENPYOU_KBN_CD = SqlInt16.Parse(crtRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString());
            }
            // 受入品名CD
            tDainouShukkaDetail.HINMEI_CD = crtRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value.ToString();
            // 受入品名
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value)
            {
                tDainouShukkaDetail.HINMEI_NAME = crtRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value.ToString();
            }
            // 正味重量
            decimal stackJuryou = 0;
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value), out stackJuryou);
                tDainouShukkaDetail.STACK_JYUURYOU = stackJuryou;
            }
            // 調整
            decimal chouseiJyuuryou = 0;
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_CHOUSEI_JYUURYOU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_CHOUSEI_JYUURYOU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_SHUKKA_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);
                tDainouShukkaDetail.CHOUSEI_JYUURYOU = chouseiJyuuryou;
            }
            // 実正味
            decimal jyuuryou = 0;
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_NET_JYUURYOU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_NET_JYUURYOU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_SHUKKA_NET_JYUURYOU].Value), out jyuuryou);
                tDainouShukkaDetail.NET_JYUURYOU = jyuuryou;
            }
            // 数量(受入)
            decimal suuryou = 0;
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_SUURYOU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_SUURYOU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_SHUKKA_SUURYOU].Value), out suuryou);
            }
            tDainouShukkaDetail.SUURYOU = suuryou;
            // 単位CD(受入)
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value.ToString()))
            {
                tDainouShukkaDetail.UNIT_CD = SqlInt16.Parse(crtRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value.ToString());
            }
            // 単価(受入)
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_TANKA].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_TANKA].Value.ToString()))
            {
                tDainouShukkaDetail.TANKA = decimal.Parse(Convert.ToString(crtRow[ConstClass.CONTROL_SHUKKA_TANKA].Value));
            }
            // 税区分CD
            int uZeiKbnCd = 0;
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value.ToString()))
            {
                uZeiKbnCd = Int16.Parse(crtRow[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value.ToString());
                tDainouShukkaDetail.HINMEI_ZEI_KBN_CD = Int16.Parse(uZeiKbnCd.ToString());
            }
            // 金額(受入)
            decimal kingaku = 0;
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_KINGAKU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_KINGAKU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_SHUKKA_KINGAKU].Value), out kingaku);
            }

            if (uZeiKbnCd == 0)
            {
                tDainouShukkaDetail.KINGAKU = kingaku;
                tDainouShukkaDetail.HINMEI_KINGAKU = 0;
            }
            else
            {
                tDainouShukkaDetail.KINGAKU = 0;
                tDainouShukkaDetail.HINMEI_KINGAKU = kingaku;
            }

            // 備考(受入)
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_MEISAI_BIKOU].Value)
            {
                tDainouShukkaDetail.MEISAI_BIKOU = crtRow[ConstClass.CONTROL_SHUKKA_MEISAI_BIKOU].Value.ToString();
            }

            // 伝票区分CD
            int uDenpyouKbnCd = 0;
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString()))
            {
                uDenpyouKbnCd = Int16.Parse(crtRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString());
            }

            // 金額
            decimal uKingaku = 0;
            if (null != crtRow[ConstClass.CONTROL_SHUKKA_KINGAKU].Value &&
                !string.IsNullOrEmpty(crtRow[ConstClass.CONTROL_SHUKKA_KINGAKU].Value.ToString()))
            {
                decimal.TryParse(Convert.ToString(crtRow[ConstClass.CONTROL_SHUKKA_KINGAKU].Value), out uKingaku);

            }
            // 消費税の項目
            this.CalcDetailShouhizei(uDenpyouKbnCd, uZeiKbnCd, uKingaku, tDainouShukkaDetail);

            return tDainouShukkaDetail;
        }

        /// <summary>
        /// 画面から代納受入、出荷の明細情報を取得する
        /// </summary>
        /// <returns></returns>
        public void GetDainouDetailEntryData(WINDOW_TYPE windowType)
        {
            LogUtility.DebugMethodStart(windowType);
            try
            {
                if (windowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) || windowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    this.insDainouUkeireDetail = new List<T_UR_SH_DETAIL>();
                    this.insDainouShukkaDetail = new List<T_UR_SH_DETAIL>();

                    // 受入
                    for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
                    {
                        // 明細システムID
                        if (windowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) || (i > this.dainouUkeireDetail.Count - 1))
                        {
                            this.UkeireMeisaiSystemId = long.Parse(this.accessor.CreateSystemIdForDainou().ToString());
                        }
                        else
                        {
                            this.UkeireMeisaiSystemId = long.Parse(this.dainouUkeireDetail[i].DETAIL_SYSTEM_ID.ToString());
                        }
                        this.insDainouUkeireDetail.Add(this.CreateDainouUkeireDetail(i));
                    }
                    // 出荷
                    for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
                    {
                        // 明細システムID
                        if (windowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) || (i > this.dainouShukkaDetail.Count - 1))
                        {
                            this.ShukkaMeisaiSystemId = long.Parse(this.accessor.CreateSystemIdForDainou().ToString());
                        }
                        else
                        {
                            this.ShukkaMeisaiSystemId = long.Parse(this.dainouShukkaDetail[i].DETAIL_SYSTEM_ID.ToString());
                        }
                        this.insDainouShukkaDetail.Add(this.CreateDainouShukkaDetail(i));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region 明細消費税計算
        /// <summary>
        /// 明細消費税計算
        /// </summary>
        /// <param name="denpyouKbunCD">伝票区分CD</param>
        /// <param name="pmZeiKbnCD">税区分CD　</param>
        /// <param name="kinkaku">金額</param>
        /// <param name="detail">T_UR_SH_DETAIL</param>
        internal void CalcDetailShouhizei(int denpyouKbunCD, int pmZeiKbnCD, decimal kingaku, T_UR_SH_DETAIL detail)
        {
            LogUtility.DebugMethodStart(denpyouKbunCD, pmZeiKbnCD, kingaku, detail);
            try
            {
                decimal taxRate = 0;
                // 品名別消費税外,内税
                decimal hinmeiTaxSoto = 0;
                decimal hinmeiTaxUchi = 0;
                // 消費税外,内税
                decimal taxSoto = 0;
                decimal taxUchi = 0;

                // 明細．金額
                decimal meisaiKingaku = 0;
                int zeikeisanKbn = 0;
                int zeiKbnCd = 0;
                int taxHasuuCd = 0;

                // 金額
                meisaiKingaku = kingaku;

                // 伝票区分により、取引先情報で税計算区分CD,税区分CD,消費税端数CDを設定
                switch (denpyouKbunCD)
                {
                    case 1:  //売上
                        // 取引先_請求情報マスタ
                        var torihikisakiSeikyuu = this.accessor.GetTorihikisakiSeikyuu(this.form.SHUKKA_TORIHIKISAKI_CD.Text);
                        if (null != torihikisakiSeikyuu)
                        {
                            // 消費税端数CD　
                            int.TryParse(Convert.ToString(torihikisakiSeikyuu.TAX_HASUU_CD), out taxHasuuCd);
                        }
                        // 税計算区分CD　
                        int.TryParse(this.form.denpyouHakouPopUpDTO.Shukka_Zeikeisan_Kbn, out zeikeisanKbn);
                        // 税区分CD　
                        int.TryParse(this.form.denpyouHakouPopUpDTO.Shukka_Zei_Kbn, out zeiKbnCd);

                        taxRate = this.shukkaTaxRate;
                        break;
                    case 2: //支払
                        // 取引先_支払情報マスタ
                        var torihikisakiShiharai = this.accessor.GetTorihikisakiShiharai(this.form.UKEIRE_TORIHIKISAKI_CD.Text);
                        if (null != torihikisakiShiharai)
                        {
                            // 消費税端数CD　
                            int.TryParse(Convert.ToString(torihikisakiShiharai.TAX_HASUU_CD), out taxHasuuCd);
                        }
                        // 税計算区分CD　
                        int.TryParse(this.form.denpyouHakouPopUpDTO.Ukeire_Zeikeisan_Kbn, out zeikeisanKbn);
                        // 税区分CD　
                        int.TryParse(this.form.denpyouHakouPopUpDTO.Ukeire_Zei_Kbn, out zeiKbnCd);

                        taxRate = this.ukeireTaxRate;
                        break;
                    default:
                        break;
                }

                //税区分CD(品名)
                int hinmeiZeiKbnCd = pmZeiKbnCD;

                //消費税
                switch (hinmeiZeiKbnCd)
                {
                    case 1: //外税
                        // 品名別消費税外税
                        hinmeiTaxSoto = CommonCalc.FractionCalc(meisaiKingaku * taxRate, taxHasuuCd);

                        break;

                    case 2: //内税
                        // 品名別消費税内税
                        hinmeiTaxUchi = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                        hinmeiTaxUchi = CommonCalc.FractionCalc((decimal)hinmeiTaxUchi, taxHasuuCd);
                        break;
                    case 3:  //非課税

                        break;

                    case 0:  //品名別税=0
                        //取引先_請求,支払情報マスタからの税区分
                        switch (zeiKbnCd)
                        {
                            case 1:
                                // 消費税外税
                                taxSoto = CommonCalc.FractionCalc(meisaiKingaku * taxRate, taxHasuuCd);

                                break;

                            case 2:
                                // 消費税内税
                                taxUchi = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                                taxUchi = CommonCalc.FractionCalc((decimal)taxUchi, taxHasuuCd);
                                break;

                            case 3:  //非課税

                                break;
                            default:
                                break;
                        }
                        break;
                }

                //品名税区分CDがない場合
                if (pmZeiKbnCD == 0)
                {
                    //金額
                    detail.KINGAKU = meisaiKingaku;

                    //消費税項目をレコードに追加
                    //消費税外税
                    detail.TAX_SOTO = taxSoto;
                    //消費税内税
                    detail.TAX_UCHI = taxUchi;
                    //品名別消費税外税
                    detail.HINMEI_TAX_SOTO = 0;
                    //品名別消費税内税
                    detail.HINMEI_TAX_UCHI = 0;
                    //品名別金額
                    detail.HINMEI_KINGAKU = 0;

                }
                else
                {
                    //金額
                    detail.KINGAKU = 0;

                    //消費税項目をレコードに追加
                    //消費税外税
                    detail.TAX_SOTO = 0;
                    //消費税内税
                    detail.TAX_UCHI = 0;
                    //品名別消費税外税
                    detail.HINMEI_TAX_SOTO = hinmeiTaxSoto;
                    //品名別消費税内税
                    detail.HINMEI_TAX_UCHI = hinmeiTaxUchi;
                    //品名別金額
                    detail.HINMEI_KINGAKU = meisaiKingaku;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 受入合計系の計算
        /// <summary>
        /// 受入合計系の計算
        /// </summary>
        /// <param name="tDainouUkeireEntry">代納入力_受入エンティティ</param>
        internal void GetUkrireTotalValues(T_UR_SH_ENTRY tDainouUkeireEntry)
        {
            LogUtility.DebugMethodStart(tDainouUkeireEntry);

            // 金額合計
            decimal kingakuTotal = 0;
            decimal hinmeiKingakuTotal = 0;

            // 明細に対して、計算を行う
            for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
            {
                //行
                GrapeCity.Win.MultiRow.Row mRow = this.form.Ichiran.Rows[i];

                // 受入 伝票区分
                if (null == mRow.Cells[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value ||
                    string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString()))
                {
                    // 伝票区分がなければ、処理しない
                    continue;
                }

                // 税区分CD
                int zeiKbnCd = 0;
                if (null != mRow.Cells[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value.ToString()))
                {
                    zeiKbnCd = Int16.Parse(mRow.Cells[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value.ToString());
                }

                // 金額
                decimal kingaku = 0;
                if (null != mRow.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value.ToString()))
                {
                    decimal.TryParse(Convert.ToString(mRow.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value), out kingaku);
                }

                if (zeiKbnCd == 0)
                {
                    kingakuTotal += kingaku;
                }
                else
                {
                    hinmeiKingakuTotal += kingaku;
                }
            }

            // 支払金額合計、品名支払金額合計
            tDainouUkeireEntry.SHIHARAI_AMOUNT_TOTAL = kingakuTotal;
            tDainouUkeireEntry.HINMEI_SHIHARAI_KINGAKU_TOTAL = hinmeiKingakuTotal;

            // 取引先が未入力
            if (String.IsNullOrEmpty(this.form.UKEIRE_TORIHIKISAKI_CD.Text))
            {
                return;
            }

            // 正味合計
            decimal netTotal = 0;
            // 支払消費税率									
            decimal shiharaiShouhiZeiRate = this.ukeireTaxRate;
            // 支払伝票毎消費税外税									
            decimal shiharaiTaxSoto = 0;
            // 支払伝票毎消費税内税									
            decimal shiharaiTaxUchi = 0;
            // 支払明細毎消費税外税合計									
            decimal shiharaiTaxSotoTotal = 0;
            // 支払明細毎消費税内税合計									
            decimal shiharaiTaxUchiTotal = 0;
            // 品名別支払金額合計									
            decimal hinmeiShiharaiKingakuTotal = 0;
            // 品名別支払消費税外税合計									
            decimal hinmeiShiharaiTaxSotoTotal = 0;
            // 品名別支払消費税内税合計									
            decimal hinmeiShiharaiTaxUchiTotal = 0;

            // 取引先_支払情報マスタ
            int shiharaiZeikeisanKbn = 0;
            int shiharaiZeiKbnCd = 0;
            int shiharaiKingakuHasuuCd = 0;

            // 取引先CD
            string torihikisakiCd = this.form.UKEIRE_TORIHIKISAKI_CD.Text;

            // 取引先_支払情報マスタ
            var torihikisakiShiharai = this.accessor.GetTorihikisakiShiharai(torihikisakiCd);

            if (null != torihikisakiShiharai)
            {
                // 消費税端数CD　
                int.TryParse(Convert.ToString(torihikisakiShiharai.TAX_HASUU_CD), out shiharaiKingakuHasuuCd);
            }
            // 税計算区分CD　
            int.TryParse(this.form.denpyouHakouPopUpDTO.Ukeire_Zeikeisan_Kbn, out shiharaiZeikeisanKbn);
            // 税区分CD　
            int.TryParse(this.form.denpyouHakouPopUpDTO.Ukeire_Zei_Kbn, out shiharaiZeiKbnCd);

            // 明細に対して、計算を行う
            for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
            {
                // 行
                GrapeCity.Win.MultiRow.Row mRow = this.form.Ichiran.Rows[i];

                T_UR_SH_DETAIL ukeireDetail = new T_UR_SH_DETAIL();

                // 正味合計
                if (null != mRow.Cells[ConstClass.CONTROL_UKEIRE_NET_JYUURYOU].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_UKEIRE_NET_JYUURYOU].Value.ToString()))
                {
                    netTotal = netTotal + decimal.Parse(mRow.Cells[ConstClass.CONTROL_UKEIRE_NET_JYUURYOU].Value.ToString());
                }

                int denpyouKbnCd = 0;

                // 受入 伝票区分
                if (null != mRow.Cells[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString()))
                {
                    denpyouKbnCd = Int16.Parse(mRow.Cells[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString());
                }
                else
                {
                    // 伝票区分がなければ、処理しない
                    continue;
                }
                // 受入明細項目
                decimal kingaku = 0;
                decimal taxSoto = 0;
                decimal taxUchi = 0;
                decimal hinmeiKingaku = 0;
                decimal hinmeiTaxSoto = 0;
                decimal hinmeiTaxUchi = 0;

                // 税区分CD
                int zeiKbnCd = 0;
                if (null != mRow.Cells[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value.ToString()))
                {
                    zeiKbnCd = Int16.Parse(mRow.Cells[ConstClass.CONTROL_UKEIRE_ZEI_KBN_CD].Value.ToString());
                }

                // 金額
                if (null != mRow.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value.ToString()))
                {
                    decimal.TryParse(Convert.ToString(mRow.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value), out kingaku);
                }
                // 税取得
                this.CalcDetailShouhizei(denpyouKbnCd, zeiKbnCd, kingaku, ukeireDetail);

                decimal.TryParse(Convert.ToString(ukeireDetail.KINGAKU), out kingaku);
                decimal.TryParse(Convert.ToString(ukeireDetail.TAX_SOTO), out taxSoto);
                decimal.TryParse(Convert.ToString(ukeireDetail.TAX_UCHI), out taxUchi);
                decimal.TryParse(Convert.ToString(ukeireDetail.HINMEI_KINGAKU), out hinmeiKingaku);
                decimal.TryParse(Convert.ToString(ukeireDetail.HINMEI_TAX_SOTO), out hinmeiTaxSoto);
                decimal.TryParse(Convert.ToString(ukeireDetail.HINMEI_TAX_UCHI), out hinmeiTaxUchi);

                //明細の税のかき集め
                shiharaiTaxSotoTotal += taxSoto;
                shiharaiTaxUchiTotal += taxUchi;
                hinmeiShiharaiTaxSotoTotal += hinmeiTaxSoto;
                hinmeiShiharaiTaxUchiTotal += hinmeiTaxUchi;
            }

            // 伝票毎
            switch ((int)tDainouUkeireEntry.SHIHARAI_ZEI_KBN_CD)
            {
                case 1://外税
                    shiharaiTaxSoto = CommonCalc.FractionCalc(kingakuTotal * this.ukeireTaxRate, shiharaiKingakuHasuuCd);
                    break;

                case 2://内税
                    shiharaiTaxUchi = CommonCalc.FractionCalc(kingakuTotal - (kingakuTotal / (this.ukeireTaxRate + 1)), shiharaiKingakuHasuuCd);
                    break;

                case 3://非課税
                    break;
            }

            // 売上消費税率									
            tDainouUkeireEntry.URIAGE_SHOUHIZEI_RATE = this.shukkaTaxRate;
            // 売上金額合計									
            tDainouUkeireEntry.URIAGE_AMOUNT_TOTAL = 0;
            // 売上伝票毎消費税外税									
            tDainouUkeireEntry.URIAGE_TAX_SOTO = 0;
            // 売上伝票毎消費税内税									
            tDainouUkeireEntry.URIAGE_TAX_UCHI = 0;
            // 売上明細毎消費税外税合計									
            tDainouUkeireEntry.URIAGE_TAX_SOTO_TOTAL = 0;
            // 売上明細毎消費税内税合計									
            tDainouUkeireEntry.URIAGE_TAX_UCHI_TOTAL = 0;
            // 品名別売上金額合計									
            tDainouUkeireEntry.HINMEI_URIAGE_KINGAKU_TOTAL = 0;
            // 品名別売上消費税外税合計									
            tDainouUkeireEntry.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
            // 品名別売上消費税内税合計									
            tDainouUkeireEntry.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
            // 支払消費税率									
            tDainouUkeireEntry.SHIHARAI_SHOUHIZEI_RATE = this.ukeireTaxRate;
            // 支払金額合計	
            tDainouUkeireEntry.SHIHARAI_AMOUNT_TOTAL = kingakuTotal;
            // 支払伝票毎消費税外税									
            tDainouUkeireEntry.SHIHARAI_TAX_SOTO = shiharaiTaxSoto;
            // 支払伝票毎消費税内税									
            tDainouUkeireEntry.SHIHARAI_TAX_UCHI = shiharaiTaxUchi;
            // 支払明細毎消費税外税合計									
            shiharaiTaxSotoTotal = CommonCalc.FractionCalc(shiharaiTaxSotoTotal, shiharaiKingakuHasuuCd);
            tDainouUkeireEntry.SHIHARAI_TAX_SOTO_TOTAL = shiharaiTaxSotoTotal;
            // 支払明細毎消費税内税合計									
            shiharaiTaxUchiTotal = CommonCalc.FractionCalc(shiharaiTaxUchiTotal, shiharaiKingakuHasuuCd);
            tDainouUkeireEntry.SHIHARAI_TAX_UCHI_TOTAL = shiharaiTaxUchiTotal;
            // 品名別支払金額合計									
            hinmeiShiharaiKingakuTotal = CommonCalc.FractionCalc(hinmeiKingakuTotal, shiharaiKingakuHasuuCd);
            tDainouUkeireEntry.HINMEI_SHIHARAI_KINGAKU_TOTAL = hinmeiShiharaiKingakuTotal;
            // 品名別支払消費税外税合計									
            hinmeiShiharaiTaxSotoTotal = CommonCalc.FractionCalc(hinmeiShiharaiTaxSotoTotal, shiharaiKingakuHasuuCd);
            tDainouUkeireEntry.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = hinmeiShiharaiTaxSotoTotal;
            // 品名別支払消費税内税合計									
            hinmeiShiharaiTaxUchiTotal = CommonCalc.FractionCalc(hinmeiShiharaiTaxUchiTotal, shiharaiKingakuHasuuCd);
            tDainouUkeireEntry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = hinmeiShiharaiTaxUchiTotal;
            // 売上税計算区分CD　
            tDainouUkeireEntry.URIAGE_ZEI_KEISAN_KBN_CD = 0;
            // 売上税区分CD　
            tDainouUkeireEntry.URIAGE_ZEI_KBN_CD = 0;
            // 売上取引区分CD
            tDainouUkeireEntry.URIAGE_TORIHIKI_KBN_CD = 2;
            // 支払税計算区分CD
            tDainouUkeireEntry.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(shiharaiZeikeisanKbn.ToString());
            // 支払税区分CD　
            tDainouUkeireEntry.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(shiharaiZeiKbnCd.ToString());
            // 支払取引区分CD
            tDainouUkeireEntry.SHIHARAI_TORIHIKI_KBN_CD = 2;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 出荷合計系の計算
        /// <summary>
        /// 出荷合計系の計算
        /// </summary>
        /// <param name="tDainouShukkaEntry">代納入力_出荷エンティティ</param>
        internal void GetShukkaTotalValues(T_UR_SH_ENTRY tDainouShukkaEntry)
        {
            LogUtility.DebugMethodStart(tDainouShukkaEntry);

            // 金額合計
            decimal kingakuTotal = 0;
            decimal hinmeiKingakuTotal = 0;

            // 明細に対して、計算を行う
            for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
            {
                //行
                GrapeCity.Win.MultiRow.Row mRow = this.form.Ichiran.Rows[i];

                // 受入 伝票区分
                if (null == mRow.Cells[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value ||
                    string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString()))
                {
                    // 伝票区分がなければ、処理しない
                    continue;
                }

                // 税区分CD
                int zeiKbnCd = 0;
                if (null != mRow.Cells[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value.ToString()))
                {
                    zeiKbnCd = Int16.Parse(mRow.Cells[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value.ToString());
                }

                // 金額
                decimal kingaku = 0;
                if (null != mRow.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value.ToString()))
                {
                    decimal.TryParse(Convert.ToString(mRow.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value), out kingaku);
                }

                if (zeiKbnCd == 0)
                {
                    kingakuTotal += kingaku;
                }
                else
                {
                    hinmeiKingakuTotal += kingaku;
                }
            }
            // 売上金額合計、売上品名金額合計
            tDainouShukkaEntry.URIAGE_AMOUNT_TOTAL = kingakuTotal;
            tDainouShukkaEntry.HINMEI_URIAGE_KINGAKU_TOTAL = hinmeiKingakuTotal;

            // 取引先が未入力
            if (String.IsNullOrEmpty(this.form.SHUKKA_TORIHIKISAKI_CD.Text))
            {
                return;
            }

            // 正味合計
            decimal netTotal = 0;

            // 売上消費税率									
            decimal uriageShouhiZeiRate = this.shukkaTaxRate;
            // 売上伝票毎消費税外税									
            decimal uriageTaxSoto = 0;
            // 売上伝票毎消費税内税									
            decimal uriageTaxUchi = 0;
            // 売上明細毎消費税外税合計									
            decimal uriageTaxSotoTotal = 0;
            // 売上明細毎消費税内税合計									
            decimal uriageTaxUchiTotal = 0;
            // 品名別売上金額合計									
            decimal hinmeiUriageKingakuTotal = 0;
            // 品名別売上消費税外税合計									
            decimal hinmeiUriageTaxSotoTotal = 0;
            // 品名別売上消費税内税合計									
            decimal hinmeiUriageTaxUchiTotal = 0;

            // 取引先_請求情報マスタ
            int seikyuuZeikeisanKbn = 0;
            int seikyuuZeiKbnCd = 0;
            int seikyuuKingakuHasuuCd = 0;

            // 取引先CD
            string torihikisakiCd = this.form.SHUKKA_TORIHIKISAKI_CD.Text;

            // 取引先_請求情報マスタ
            var torihikisakiSeikyuu = this.accessor.GetTorihikisakiSeikyuu(torihikisakiCd);

            if (null != torihikisakiSeikyuu)
            {
                // 消費税端数CD　
                int.TryParse(Convert.ToString(torihikisakiSeikyuu.TAX_HASUU_CD), out seikyuuKingakuHasuuCd);
            }

            // 税計算区分CD　
            int.TryParse(this.form.denpyouHakouPopUpDTO.Shukka_Zeikeisan_Kbn, out seikyuuZeikeisanKbn);
            // 税区分CD　
            int.TryParse(this.form.denpyouHakouPopUpDTO.Shukka_Zei_Kbn, out seikyuuZeiKbnCd);

            // 明細に対して、計算を行う
            for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
            {
                //行
                GrapeCity.Win.MultiRow.Row mRow = this.form.Ichiran.Rows[i];

                T_UR_SH_DETAIL shukkaDetail = new T_UR_SH_DETAIL();

                // 正味合計
                if (null != mRow.Cells[ConstClass.CONTROL_SHUKKA_NET_JYUURYOU].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_SHUKKA_NET_JYUURYOU].Value.ToString()))
                {
                    netTotal = netTotal + decimal.Parse(mRow.Cells[ConstClass.CONTROL_SHUKKA_NET_JYUURYOU].Value.ToString());
                }

                // 出荷 伝票区分
                int denpyouKbnCd = 0;
                if (null != mRow.Cells[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString()))
                {
                    denpyouKbnCd = Int16.Parse(mRow.Cells[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString());
                }
                else
                {
                    // 伝票区分がなければ、処理しない
                    continue;
                }
                // 受入明細項目
                decimal kingaku = 0;
                decimal taxSoto = 0;
                decimal taxUchi = 0;
                decimal hinmeiKingaku = 0;
                decimal hinmeiTaxSoto = 0;
                decimal hinmeiTaxUchi = 0;

                // 税区分CD
                int zeiKbnCd = 0;
                if (null != mRow.Cells[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value.ToString()))
                {
                    zeiKbnCd = Int16.Parse(mRow.Cells[ConstClass.CONTROL_SHUKKA_ZEI_KBN_CD].Value.ToString());
                }

                // 金額
                if (null != mRow.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value &&
                    !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value.ToString()))
                {
                    decimal.TryParse(Convert.ToString(mRow.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value), out kingaku);
                }

                // 税取得
                this.CalcDetailShouhizei(denpyouKbnCd, zeiKbnCd, kingaku, shukkaDetail);

                decimal.TryParse(Convert.ToString(shukkaDetail.KINGAKU), out kingaku);
                decimal.TryParse(Convert.ToString(shukkaDetail.TAX_SOTO), out taxSoto);
                decimal.TryParse(Convert.ToString(shukkaDetail.TAX_UCHI), out taxUchi);
                decimal.TryParse(Convert.ToString(shukkaDetail.HINMEI_KINGAKU), out hinmeiKingaku);
                decimal.TryParse(Convert.ToString(shukkaDetail.HINMEI_TAX_SOTO), out hinmeiTaxSoto);
                decimal.TryParse(Convert.ToString(shukkaDetail.HINMEI_TAX_UCHI), out hinmeiTaxUchi);

                //明細の税のかき集め
                uriageTaxSotoTotal += taxSoto;
                uriageTaxUchiTotal += taxUchi;
                hinmeiUriageTaxSotoTotal += hinmeiTaxSoto;
                hinmeiUriageTaxUchiTotal += hinmeiTaxUchi;
            }

            // 伝票毎
            switch ((int)tDainouShukkaEntry.URIAGE_ZEI_KBN_CD)
            {
                case 1://外税
                    uriageTaxSoto = CommonCalc.FractionCalc(kingakuTotal * this.shukkaTaxRate, seikyuuKingakuHasuuCd);
                    break;

                case 2://内税
                    uriageTaxUchi = CommonCalc.FractionCalc(kingakuTotal - (kingakuTotal / (this.shukkaTaxRate + 1)), seikyuuKingakuHasuuCd);
                    break;

                case 3://非課税
                    break;
            }

            // 売上消費税率									
            tDainouShukkaEntry.URIAGE_SHOUHIZEI_RATE = uriageShouhiZeiRate;
            // 売上金額合計									
            tDainouShukkaEntry.URIAGE_AMOUNT_TOTAL = kingakuTotal;
            // 売上伝票毎消費税外税									
            tDainouShukkaEntry.URIAGE_TAX_SOTO = uriageTaxSoto;
            // 売上伝票毎消費税内税									
            tDainouShukkaEntry.URIAGE_TAX_UCHI = uriageTaxUchi;
            // 売上明細毎消費税外税合計									
            uriageTaxSotoTotal = CommonCalc.FractionCalc(uriageTaxSotoTotal, seikyuuKingakuHasuuCd);
            tDainouShukkaEntry.URIAGE_TAX_SOTO_TOTAL = uriageTaxSotoTotal;
            // 売上明細毎消費税内税合計									
            uriageTaxUchiTotal = CommonCalc.FractionCalc(uriageTaxUchiTotal, seikyuuKingakuHasuuCd);
            tDainouShukkaEntry.URIAGE_TAX_UCHI_TOTAL = uriageTaxUchiTotal;
            // 品名別売上金額合計									
            hinmeiUriageKingakuTotal = CommonCalc.FractionCalc(hinmeiKingakuTotal, seikyuuKingakuHasuuCd);
            tDainouShukkaEntry.HINMEI_URIAGE_KINGAKU_TOTAL = hinmeiUriageKingakuTotal;
            // 品名別売上消費税外税合計									
            hinmeiUriageTaxSotoTotal = CommonCalc.FractionCalc(hinmeiUriageTaxSotoTotal, seikyuuKingakuHasuuCd);
            tDainouShukkaEntry.HINMEI_URIAGE_TAX_SOTO_TOTAL = hinmeiUriageTaxSotoTotal;
            // 品名別売上消費税内税合計									
            hinmeiUriageTaxUchiTotal = CommonCalc.FractionCalc(hinmeiUriageTaxUchiTotal, seikyuuKingakuHasuuCd);
            tDainouShukkaEntry.HINMEI_URIAGE_TAX_UCHI_TOTAL = hinmeiUriageTaxUchiTotal;
            // 支払消費税率									
            tDainouShukkaEntry.SHIHARAI_SHOUHIZEI_RATE = this.ukeireTaxRate;
            // 支払金額合計	
            tDainouShukkaEntry.SHIHARAI_AMOUNT_TOTAL = 0;
            // 支払伝票毎消費税外税
            tDainouShukkaEntry.SHIHARAI_TAX_SOTO = 0;
            // 支払伝票毎消費税内税
            tDainouShukkaEntry.SHIHARAI_TAX_UCHI = 0;
            // 支払明細毎消費税外税合計									
            tDainouShukkaEntry.SHIHARAI_TAX_SOTO_TOTAL = 0;
            // 支払明細毎消費税内税合計									
            tDainouShukkaEntry.SHIHARAI_TAX_UCHI_TOTAL = 0;
            // 品名別支払金額合計									
            tDainouShukkaEntry.HINMEI_SHIHARAI_KINGAKU_TOTAL = 0;
            // 品名別支払消費税外税合計									
            tDainouShukkaEntry.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = 0;
            // 品名別支払消費税内税合計									
            tDainouShukkaEntry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = 0;
            // 売上税計算区分CD　
            tDainouShukkaEntry.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(seikyuuZeikeisanKbn.ToString());
            // 売上税区分CD　
            tDainouShukkaEntry.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(seikyuuZeiKbnCd.ToString());
            // 売上取引区分CD(消費税端数CD　)
            tDainouShukkaEntry.URIAGE_TORIHIKI_KBN_CD = 2;
            // 支払税計算区分CD
            tDainouShukkaEntry.SHIHARAI_ZEI_KEISAN_KBN_CD = 0;
            // 支払税区分CD　
            tDainouShukkaEntry.SHIHARAI_ZEI_KBN_CD = 0;
            // 支払取引区分CD(消費税端数CD　)
            tDainouShukkaEntry.SHIHARAI_TORIHIKI_KBN_CD = 2;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 品名関連項目初期化

        /// <summary>
        /// 品名関連項目初期化
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="flg"></param>
        internal bool HinmeiInit(int rowIndex, int flg)
        {
            bool ret = true;
            LogUtility.DebugMethodStart(rowIndex, flg);

            try
            {
                // 受入
                if (flg == 1)
                {
                    // 品名
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value = string.Empty;
                    // 単価
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_TANKA].Value = string.Empty;
                    // 単位
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value = string.Empty;
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value = string.Empty;
                    // 金額
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value = string.Empty;
                }
                // 出荷
                else if (flg == 2)
                {
                    // 品名
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value = string.Empty;
                    // 単価
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_TANKA].Value = string.Empty;
                    // 単位
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value = string.Empty;
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value = string.Empty;
                    // 金額
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value = string.Empty;
                }

                // 数量制御
                if (!this.SetHinmeiSuuryou(rowIndex, flg))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("HinmeiInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 単位項目初期化
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="flg"></param>
        internal bool UnitInit(int rowIndex, int flg)
        {
            bool ret = true;
            LogUtility.DebugMethodStart(rowIndex, flg);

            try
            {
                // 受入
                if (flg == 1)
                {
                    // 単位
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value = string.Empty;
                }
                // 出荷
                else if (flg == 2)
                {
                    // 単位
                    this.form.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value = string.Empty;
                }

                if (!this.SetHinmeiSuuryou(rowIndex, flg))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UnitInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 全ての明細と合計の計算
        /// <summary>
        /// 全ての明細と合計の計算
        /// </summary>
        internal bool CalcAllDetailAndTotal()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 金額合計
                decimal ukeireKingakuTotal = 0;
                decimal shukkaKingakuTotal = 0;

                // 明細に対して、計算を行う
                for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
                {
                    //行
                    GrapeCity.Win.MultiRow.Row mRow = this.form.Ichiran.Rows[i];

                    // 支払金額
                    decimal ukeirekingaku = 0;
                    if (null != mRow.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value &&
                        !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value.ToString()))
                    {
                        decimal.TryParse(Convert.ToString(mRow.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value), out ukeirekingaku);
                    }
                    ukeireKingakuTotal += ukeirekingaku;

                    // 売上金額
                    decimal shukkakingaku = 0;
                    if (null != mRow.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value &&
                        !string.IsNullOrEmpty(mRow.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value.ToString()))
                    {
                        decimal.TryParse(Convert.ToString(mRow.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value), out shukkakingaku);
                    }
                    shukkaKingakuTotal += shukkakingaku;
                }

                decimal ukeireSign = ukeireKingakuTotal < 0 ? -1 : 1;
                decimal shukkaSign = shukkaKingakuTotal < 0 ? -1 : 1;

                // 画面の表示
                this.form.UKEIRE_KINGAKU_SUM.Text = CommonCalc.DecimalFormat(Math.Floor(Math.Abs(ukeireKingakuTotal)) * ukeireSign);
                this.form.SHUKKA_KINGAKU_SUM.Text = CommonCalc.DecimalFormat(Math.Floor(Math.Abs(shukkaKingakuTotal)) * shukkaSign);

                decimal diff;
                // DAINO_CALC_BASE_KBN:1.売上 2.支払
                if (this.sysInfoEntity.DAINO_CALC_BASE_KBN == 1)
                {
                    diff = shukkaKingakuTotal - ukeireKingakuTotal;
                }
                else
                {
                    diff = ukeireKingakuTotal - shukkaKingakuTotal;
                }
                this.form.DIFFERENCE.Text = CommonCalc.DecimalFormat(diff);

            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcAllDetailAndTotal", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion

        #region 画面一覧データの変更後、一時保存
        /// <summary>
        /// 画面一覧データの変更後、一時保存
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="tabFlg">0:空、1:受入出荷明細検索結果からセット；2：画面明細からセット</param>
        /// <param name="flg">1:受入；2：出荷</param>
        internal bool BeforeIchiranChengeValues(int tabFlg, int rowIndex, int flg)
        {
            bool ret = true;
            LogUtility.DebugMethodStart(tabFlg, rowIndex, flg);
            try
            {
                switch (tabFlg)
                {
                    case 0:
                        //空
                        this.BeforeDetailResultList.Clear();

                        break;

                    case 1:
                        // 受入出荷明細検索結果からセット
                        for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
                        {
                            DainoNyuryukuDTO BeforeDetailResult = new DainoNyuryukuDTO();
                            GrapeCity.Win.MultiRow.Row dRow = this.form.Ichiran.Rows[i];

                            // 行の値を保存
                            // 受入品名CD
                            if (null != dRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value.ToString()))
                            {
                                BeforeDetailResult.UKEIRE_HINMEI_CD = dRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value.ToString();
                            }
                            // 受入品名
                            if (null != dRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value)
                            {
                                BeforeDetailResult.UKEIRE_HINMEI_NAME = dRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].ToString();
                            }
                            // 受入伝票区分CD
                            if (null != dRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString()))
                            {
                                BeforeDetailResult.UKEIRE_DENPYOU_KBN_CD = Int16.Parse(dRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString());
                            }
                            // 受入伝票区分名
                            if (null != dRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value)
                            {
                                BeforeDetailResult.UKEIRE_DENPYOU_KBN_NAME = dRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value.ToString();
                            }
                            // 受入単位CD
                            if (null != dRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString()))
                            {
                                BeforeDetailResult.UKEIRE_UNIT_CD = Int16.Parse(dRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString());
                            }
                            // 受入単価
                            if (null != dRow[ConstClass.CONTROL_UKEIRE_TANKA].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_UKEIRE_TANKA].Value.ToString()))
                            {
                                BeforeDetailResult.UKEIRE_TANKA = decimal.Parse(dRow[ConstClass.CONTROL_UKEIRE_TANKA].Value.ToString());
                            }

                            // 出荷品名CD
                            if (null != dRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value.ToString()))
                            {
                                BeforeDetailResult.SHUKKA_HINMEI_CD = dRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value.ToString();
                            }
                            // 出荷品名
                            if (null != dRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value.ToString()))
                            {
                                BeforeDetailResult.SHUKKA_HINMEI_NAME = dRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value.ToString();
                            }
                            // 出荷伝票区分CD
                            if (null != dRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString()))
                            {
                                BeforeDetailResult.SHUKKA_DENPYOU_KBN_CD = Int16.Parse(dRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString());
                            }
                            // 出荷伝票区分名
                            if (null != dRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value.ToString()))
                            {
                                BeforeDetailResult.SHUKKA_DENPYOU_KBN_NAME = dRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value.ToString();
                            }
                            // 出荷単位CD
                            if (null != dRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value.ToString()))
                            {
                                BeforeDetailResult.SHUKKA_UNIT_CD = Int16.Parse(dRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value.ToString());
                            }
                            // 出荷単価
                            if (null != dRow[ConstClass.CONTROL_SHUKKA_TANKA].Value &&
                                !string.IsNullOrEmpty(dRow[ConstClass.CONTROL_SHUKKA_TANKA].Value.ToString()))
                            {
                                BeforeDetailResult.SHUKKA_TANKA = decimal.Parse(dRow[ConstClass.CONTROL_SHUKKA_TANKA].Value.ToString());
                            }

                            // リストに追加
                            this.BeforeDetailResultList.Add(BeforeDetailResult);
                        }

                        break;
                    case 2:
                        // 画面明細からセット
                        // マルチROWの行
                        GrapeCity.Win.MultiRow.Row mRow = this.form.Ichiran.Rows[rowIndex];
                        if (null != mRow)
                        {
                            if (this.BeforeDetailResultList.Count > 0)
                            {
                                if (rowIndex >= this.BeforeDetailResultList.Count)
                                {
                                    int cnt = this.BeforeDetailResultList.Count;

                                    for (int i = cnt; i <= rowIndex; i++)
                                    {
                                        this.BeforeDetailResultList.Add(new DainoNyuryukuDTO());
                                    }
                                }

                                // 受入品名CD
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_HINMEI_CD = mRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value.ToString();
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_HINMEI_CD = string.Empty;
                                }
                                // 受入品名
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_HINMEI_NAME = mRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value.ToString();
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_HINMEI_NAME = string.Empty;
                                }
                                // 受入伝票区分CD
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_DENPYOU_KBN_CD = Int16.Parse(mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString());
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_DENPYOU_KBN_CD = -1;
                                }
                                // 受入伝票区分名
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_DENPYOU_KBN_NAME = mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value.ToString();
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_DENPYOU_KBN_NAME = string.Empty;
                                }
                                // 受入単位CD
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_UNIT_CD = Int16.Parse(mRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString());
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].UKEIRE_UNIT_CD = -1;
                                }
                                //受入単価
                                //if (null != mRow["UKEIRE_TANKA"].Value && !string.IsNullOrEmpty(mRow["UKEIRE_TANKA"].Value.ToString()))
                                //{
                                //    this.BeforeDetailResultList[rowIndex].UKEIRE_TANKA = decimal.Parse(mRow["UKEIRE_TANKA"].Value.ToString());
                                //}

                                // 出荷品名CD
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_HINMEI_CD = mRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value.ToString();
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_HINMEI_CD = string.Empty;
                                }
                                // 出荷品名
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_HINMEI_NAME = mRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value.ToString();
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_HINMEI_NAME = string.Empty;
                                }
                                // 出荷伝票区分CD
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_DENPYOU_KBN_CD = Int16.Parse(mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString());
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_DENPYOU_KBN_CD = -1;
                                }
                                // 出荷伝票区分名
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_DENPYOU_KBN_NAME = mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value.ToString();
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_DENPYOU_KBN_NAME = string.Empty;
                                }
                                // 出荷単位CD
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value.ToString()))
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_UNIT_CD = Int16.Parse(mRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value.ToString());
                                }
                                else
                                {
                                    this.BeforeDetailResultList[rowIndex].SHUKKA_UNIT_CD = -1;
                                }
                                //出荷単価
                                //if (null != mRow["SHUKKA_TANKA"].Value && !string.IsNullOrEmpty(mRow["SHUKKA_TANKA"].Value.ToString()))
                                //{
                                //    this.BeforeDetailResultList[rowIndex].SHUKKA_TANKA = decimal.Parse(mRow["SHUKKA_TANKA"].Value.ToString());
                                //}
                            }
                            else
                            {
                                //新規作成
                                DainoNyuryukuDTO BeforeDetailResult = new DainoNyuryukuDTO();

                                // 受入品名CD
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value.ToString()))
                                {
                                    BeforeDetailResult.UKEIRE_HINMEI_CD = mRow[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value.ToString();
                                }
                                // 受入品名
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value.ToString()))
                                {
                                    BeforeDetailResult.UKEIRE_HINMEI_NAME = mRow[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value.ToString();
                                }
                                // 受入伝票区分CD
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString()))
                                {
                                    BeforeDetailResult.UKEIRE_DENPYOU_KBN_CD = Int16.Parse(mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value.ToString());
                                }
                                // 受入伝票区分名
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value.ToString()))
                                {
                                    BeforeDetailResult.UKEIRE_DENPYOU_KBN_NAME = mRow[ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value.ToString();
                                }
                                // 受入単位CD
                                if (null != mRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString()))
                                {
                                    BeforeDetailResult.UKEIRE_UNIT_CD = Int16.Parse(mRow[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString());
                                }
                                //受入単価
                                //if (null != mRow["UKEIRE_TANKA"].Value && !string.IsNullOrEmpty(mRow["UKEIRE_TANKA"].Value.ToString()))
                                //{
                                //    BeforeDetailResult.UKEIRE_TANKA = decimal.Parse(mRow["UKEIRE_TANKA"].Value.ToString());
                                //}

                                // 出荷品名CD
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value.ToString()))
                                {
                                    BeforeDetailResult.SHUKKA_HINMEI_CD = mRow[ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value.ToString();
                                }
                                // 出荷品名
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value.ToString()))
                                {
                                    BeforeDetailResult.SHUKKA_HINMEI_NAME = mRow[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value.ToString();
                                }
                                // 出荷伝票区分CD
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString()))
                                {
                                    BeforeDetailResult.SHUKKA_DENPYOU_KBN_CD = Int16.Parse(mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value.ToString());
                                }
                                // 出荷伝票区分名
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value.ToString()))
                                {
                                    BeforeDetailResult.SHUKKA_DENPYOU_KBN_NAME = mRow[ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value.ToString();
                                }
                                // 出荷単位CD
                                if (null != mRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value &&
                                    !string.IsNullOrEmpty(mRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value.ToString()))
                                {
                                    BeforeDetailResult.SHUKKA_UNIT_CD = Int16.Parse(mRow[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value.ToString());
                                }
                                //出荷単価
                                //if (null != mRow["SHUKKA_TANKA"].Value && !string.IsNullOrEmpty(mRow["SHUKKA_TANKA"].Value.ToString()))
                                //{
                                //    BeforeDetailResult.SHUKKA_TANKA = decimal.Parse(mRow["SHUKKA_TANKA"].Value.ToString());
                                //}
                                //リストに追加
                                this.BeforeDetailResultList.Add(BeforeDetailResult);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("BeforeIchiranChengeValues", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion

        #region 端数を取得
        /// <summary>
        /// 端数を取得
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="flg">1:受入；2：出荷</param>
        internal short CalcHasuu(int rowIndex, int flg)
        {

            LogUtility.DebugMethodStart(rowIndex, flg);
            //端数CD(四捨五入)
            short kingakuHasuuCd = 3;

            try
            {
                if (rowIndex < 0)
                {
                    return 0;
                }

                GrapeCity.Win.MultiRow.Row multiRow = this.form.Ichiran.Rows[rowIndex];

                if (flg == 1) //受入
                {
                    // 受入取引先が未入力或いは伝票区分が未入力である場合
                    if (String.IsNullOrEmpty(this.form.UKEIRE_TORIHIKISAKI_CD.Text)
                        || multiRow.Cells["UKEIRE_DENPYOU_KBN_NAME"].FormattedValue == null)
                    {
                        return kingakuHasuuCd;
                    }

                    //伝票区分CD
                    int denpyouKbnCd = 0;

                    if ("売上".Equals(multiRow.Cells["UKEIRE_DENPYOU_KBN_NAME"].FormattedValue))
                    {
                        denpyouKbnCd = 1;
                    }
                    else if ("支払".Equals(multiRow.Cells["UKEIRE_DENPYOU_KBN_NAME"].FormattedValue))
                    {
                        denpyouKbnCd = 2;
                    }

                    // 伝票区分により、端数を設定
                    switch (denpyouKbnCd)
                    {
                        case 1:
                            // 取引先請求
                            var torihikisakiSeikyuu = this.accessor.GetTorihikisakiSeikyuu(this.form.UKEIRE_TORIHIKISAKI_CD.Text);
                            short.TryParse(Convert.ToString(torihikisakiSeikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;
                        case 2:
                            // 取引先支払
                            var torihikisakiShiharai = this.accessor.GetTorihikisakiShiharai(this.form.UKEIRE_TORIHIKISAKI_CD.Text);
                            short.TryParse(Convert.ToString(torihikisakiShiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;
                        default:
                            break;
                    }
                }
                else if (flg == 2) //出荷
                {
                    // 出荷取引先が未入力或いは伝票区分が未入力である場合
                    if (String.IsNullOrEmpty(this.form.SHUKKA_TORIHIKISAKI_CD.Text)
                        || multiRow.Cells["SHUKKA_DENPYOU_KBN_NAME"].FormattedValue == null)
                    {
                        return kingakuHasuuCd;
                    }

                    //伝票区分CD
                    int denpyouKbnCd = 0;

                    if ("売上".Equals(multiRow.Cells["SHUKKA_DENPYOU_KBN_NAME"].FormattedValue))
                    {
                        denpyouKbnCd = 1;
                    }
                    else if ("支払".Equals(multiRow.Cells["SHUKKA_DENPYOU_KBN_NAME"].FormattedValue))
                    {
                        denpyouKbnCd = 2;
                    }

                    // 伝票区分により、端数を設定
                    switch (denpyouKbnCd)
                    {
                        case 1:
                            // 取引先請求
                            var torihikisakiSeikyuu = this.accessor.GetTorihikisakiSeikyuu(this.form.SHUKKA_TORIHIKISAKI_CD.Text);
                            short.TryParse(Convert.ToString(torihikisakiSeikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;
                        case 2:
                            // 取引先支払
                            var torihikisakiShiharai = this.accessor.GetTorihikisakiShiharai(this.form.SHUKKA_TORIHIKISAKI_CD.Text);
                            short.TryParse(Convert.ToString(torihikisakiShiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            LogUtility.DebugMethodEnd();

            return kingakuHasuuCd;
        }
        #endregion

        #region 消費税率を取得

        /// <summary>
        /// 売上日付を基に売上消費税率を設定
        /// </summary>
        internal bool SetUriageShouhizeiRate()
        {
            bool ret = true;
            try
            {
                DateTime uriageDate = this.ParentForm.sysDate.Date;
                if (DateTime.TryParse(this.form.URIAGE_DATE.Text, out uriageDate))
                {
                    var shouhizeiRate = this.accessor.GetShouhizeiRate(uriageDate);
                    if (shouhizeiRate != null && !shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.URIAGE_SHOUHIZEI_RATE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                    }
                }
                else
                {
                    this.form.URIAGE_SHOUHIZEI_RATE.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetUriageShouhizeiRate", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUriageShouhizeiRate", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret=  false;
            }
            return ret;
        }

        /// <summary>
        /// 支払日付を基に支払消費税率を設定
        /// </summary>
        internal bool SetShiharaiShouhizeiRate()
        {
            bool ret = true;
            try
            {
                DateTime shiharaiDate = this.ParentForm.sysDate.Date;
                if (DateTime.TryParse(this.form.SHIHARAI_DATE.Text, out shiharaiDate))
                {
                    var shouhizeiRate = this.accessor.GetShouhizeiRate(shiharaiDate);
                    if (shouhizeiRate != null && !shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.SHIHARAI_SHOUHIZEI_RATE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                    }
                }
                else
                {
                    this.form.SHIHARAI_SHOUHIZEI_RATE.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetShiharaiShouhizeiRate", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShiharaiShouhizeiRate", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 売上消費税率を設定
        /// </summary>
        internal void GetUriageShouhizeiRate()
        {
            this.shukkaTaxRate = this.ToDecimalForUriageShouhizeiRate();
        }

        /// <summary>
        /// 支払消費税率を設定
        /// </summary>
        internal void GetShiharaiShouhizeiRate()
        {
            this.ukeireTaxRate = this.ToDecimalForShiharaiShouhizeiRate();
        }

        /// <summary>
        /// 売上、支払消費税率のポップアップ設定初期化
        /// </summary>
        internal void InitShouhizeiRatePopupSetting()
        {
            /**
             * 売上消費税率テキストボックスの設定
             */
            this.form.URIAGE_SHOUHIZEI_RATE.PopupWindowId = WINDOW_ID.M_SHOUHIZEI;
            this.form.URIAGE_SHOUHIZEI_RATE.PopupWindowName = "マスタ共通ポップアップ";
            this.form.URIAGE_SHOUHIZEI_RATE.PopupGetMasterField = "SHOUHIZEI_RATE";
            this.form.URIAGE_SHOUHIZEI_RATE.PopupSetFormField = "URIAGE_SHOUHIZEI_RATE";
            this.form.URIAGE_SHOUHIZEI_RATE.PopupDataHeaderTitle = new string[] { "消費税率" };

            // 表示情報作成
            var shouhizeiRates = this.accessor.GetAllShouhizeiRate();
            var dt = EntityUtility.EntityToDataTable(shouhizeiRates);

            var displayShouhizei = new DataTable();
            foreach (var col in this.form.URIAGE_SHOUHIZEI_RATE.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()))
            {
                displayShouhizei.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);

            }

            foreach (DataRow row in dt.Rows)
            {
                displayShouhizei.Rows.Add(displayShouhizei.Columns.OfType<DataColumn>().Select(s => row[s.ColumnName]).ToArray());
            }

            displayShouhizei.TableName = "消費税率";
            this.form.URIAGE_SHOUHIZEI_RATE.PopupDataSource = displayShouhizei;

            /**
             * ポップアップの設定
             */
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupGetMasterField = this.form.URIAGE_SHOUHIZEI_RATE.PopupGetMasterField;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupSetFormField = this.form.URIAGE_SHOUHIZEI_RATE.PopupSetFormField;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowId = this.form.URIAGE_SHOUHIZEI_RATE.PopupWindowId;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowName = this.form.URIAGE_SHOUHIZEI_RATE.PopupWindowName;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataHeaderTitle = this.form.URIAGE_SHOUHIZEI_RATE.PopupDataHeaderTitle;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataSource = this.form.URIAGE_SHOUHIZEI_RATE.PopupDataSource;

            /**
             * 支払消費税率テキストボックスの設定
             */
            this.form.SHIHARAI_SHOUHIZEI_RATE.PopupWindowId = WINDOW_ID.M_SHOUHIZEI;
            this.form.SHIHARAI_SHOUHIZEI_RATE.PopupWindowName = "マスタ共通ポップアップ";
            this.form.SHIHARAI_SHOUHIZEI_RATE.PopupGetMasterField = "SHOUHIZEI_RATE";
            this.form.SHIHARAI_SHOUHIZEI_RATE.PopupSetFormField = "SHIHARAI_SHOUHIZEI_RATE";
            this.form.SHIHARAI_SHOUHIZEI_RATE.PopupDataHeaderTitle = new string[] { "消費税率" }; ;
            // 売上消費税率と同様のマスタを参照するためデータソースは売上消費税のを流用
            this.form.SHIHARAI_SHOUHIZEI_RATE.PopupDataSource = displayShouhizei;

            /**
             * ポップアップの設定
             */
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupGetMasterField = this.form.SHIHARAI_SHOUHIZEI_RATE.PopupGetMasterField;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupSetFormField = this.form.SHIHARAI_SHOUHIZEI_RATE.PopupSetFormField;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowId = this.form.SHIHARAI_SHOUHIZEI_RATE.PopupWindowId;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowName = this.form.SHIHARAI_SHOUHIZEI_RATE.PopupWindowName;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataHeaderTitle = this.form.SHIHARAI_SHOUHIZEI_RATE.PopupDataHeaderTitle;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataSource = this.form.SHIHARAI_SHOUHIZEI_RATE.PopupDataSource;
        }

        /// <summary>
        /// UIFormの売上消費税率をパーセント表記で取得する
        /// </summary>
        /// <returns>パーセント表示の売上消費税率</returns>
        internal string ToPercentForUriageShouhizeiRate()
        {
            string returnVal = string.Empty;

            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE.Text))
            {
                decimal shouhizeiRate = 0;
                if (!this.form.URIAGE_SHOUHIZEI_RATE.Text.Contains("%")
                    && decimal.TryParse(this.form.URIAGE_SHOUHIZEI_RATE.Text, out shouhizeiRate))
                {
                    returnVal = shouhizeiRate.ToString("P");
                }
                else if (this.form.URIAGE_SHOUHIZEI_RATE.Text.Contains("%"))
                {
                    // 既に%表記ならそのまま返す
                    returnVal = this.form.URIAGE_SHOUHIZEI_RATE.Text;
                }
            }

            return returnVal;
        }

        /// <summary>
        /// UIFormの売上消費税率を小数点表記で取得する
        /// </summary>
        /// <returns>小数点表記の売上消費税率(DBへ格納できる値)</returns>
        internal decimal ToDecimalForUriageShouhizeiRate()
        {
            decimal returnVal = 0;

            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE.Text))
            {
                string tempUriageShouhizeiRate = string.Empty;

                if (!this.form.URIAGE_SHOUHIZEI_RATE.Text.Contains("%"))
                {
                    tempUriageShouhizeiRate = this.form.URIAGE_SHOUHIZEI_RATE.Text;
                }
                else
                {
                    tempUriageShouhizeiRate = this.form.URIAGE_SHOUHIZEI_RATE.Text.Replace("%", "");
                }

                decimal shouhizeiRate = 0;
                if (decimal.TryParse(tempUriageShouhizeiRate, out shouhizeiRate))
                {
                    returnVal = shouhizeiRate / 100m;
                }
            }

            return returnVal;
        }

        /// <summary>
        /// UIFormの支払消費税率をパーセント表記で取得する
        /// </summary>
        /// <returns>パーセント表示の売上消費税率</returns>
        internal string ToPercentForShiharaiShouhizeiRate()
        {
            string returnVal = string.Empty;

            if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE.Text))
            {
                decimal shouhizeiRate = 0;
                if (!this.form.SHIHARAI_SHOUHIZEI_RATE.Text.Contains("%")
                    && decimal.TryParse(this.form.SHIHARAI_SHOUHIZEI_RATE.Text, out shouhizeiRate))
                {
                    returnVal = shouhizeiRate.ToString("P");
                }
                else if (this.form.SHIHARAI_SHOUHIZEI_RATE.Text.Contains("%"))
                {
                    // 既に%表記ならそのまま返す
                    returnVal = this.form.SHIHARAI_SHOUHIZEI_RATE.Text;
                }
            }

            return returnVal;
        }

        /// <summary>
        /// UIFormの支払消費税率を小数点表記で取得する
        /// </summary>
        /// <returns>小数点表記の売上消費税率(DBへ格納できる値)</returns>
        internal decimal ToDecimalForShiharaiShouhizeiRate()
        {
            decimal returnVal = 0;

            if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE.Text))
            {
                string tempUriageShouhizeiRate = string.Empty;

                if (!this.form.SHIHARAI_SHOUHIZEI_RATE.Text.Contains("%"))
                {
                    tempUriageShouhizeiRate = this.form.SHIHARAI_SHOUHIZEI_RATE.Text;
                }
                else
                {
                    tempUriageShouhizeiRate = this.form.SHIHARAI_SHOUHIZEI_RATE.Text.Replace("%", "");
                }

                decimal shouhizeiRate = 0;
                if (decimal.TryParse(tempUriageShouhizeiRate, out shouhizeiRate))
                {
                    returnVal = shouhizeiRate / 100m;
                }
            }

            return returnVal;
        }
        #endregion

        #region 税区分CDより税区分名を取得
        /// <summary>
        /// 税区分CDより税区分名を取得
        /// </summary>
        /// <param name="zeiKbnCD">税区分CD</param>
        /// <returns>税区分名</returns>
        private string GetZeiKbn(string zeiKbnCD)
        {
            LogUtility.DebugMethodStart(zeiKbnCD);

            string ZeiName = "";

            switch (zeiKbnCD)
            {
                case "1":
                    ZeiName = "外税";
                    break;
                case "2":
                    ZeiName = "内税";
                    break;
                case "3":
                    ZeiName = "非課税";
                    break;
                default:
                    ZeiName = string.Empty;
                    break;
            }
            LogUtility.DebugMethodEnd(ZeiName);
            return ZeiName;
        }
        #endregion

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        internal object ChgDBNullToValue(object obj, object value)
        {

            if (null == obj || obj is DBNull || obj.Equals(SqlDecimal.Null) || obj.Equals(SqlInt16.Null))
            {
                return value;
            }
            else
            {
                return obj;
            }
        }
        #endregion

        #region システム情報を取得
        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                var sysInfo = this.accessor.GetSysInfo();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo;
                    // 売上支払情報差引基準を取得(1.売上 or 2.支払)
                    this.systemUrShCalcBaseKbn = this.ChgDBNullToValue(sysInfoEntity.UR_SH_CALC_BASE_KBN, string.Empty).ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.Err);
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetCorpInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                var corpInfo = this.accessor.GetMCorpInfo();
                if (corpInfo != null)
                {
                    this.corpInfoEntity = corpInfo;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.Err);
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ユーザー定義情報取得処理
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
        #endregion

        #region 共通で使用する計算処理クラス
        /// <summary>
        /// 共通で使用する計算処理クラス
        /// </summary>
        public static class CommonCalc
        {
            /// <summary>
            /// 端数処理種別
            /// </summary>
            private enum fractionType : int
            {
                CEILING = 1,	// 切り上げ
                FLOOR,		// 切り捨て
                ROUND,		// 四捨五入
            }

            /// <summary>
            /// 端数処理桁用Enum
            /// </summary>
            private enum hasuKetaType : short
            {
                NONE = 1,       // 1の位
                ONEPOINT,       // 小数第一位
                TOWPOINT,       // 小数第二位
                THREEPOINT,     // 小数第三位
                FOUR,           // 小数第四位
            }

            /// <summary>
            /// 指定された端数CDに従い、金額の端数処理を行う
            /// </summary>
            /// <param name="kingaku">端数処理対象金額</param>
            /// <param name="calcCD">端数CD</param>
            /// <returns name="decimal">端数処理後の金額</returns>
            public static decimal FractionCalc(decimal kingaku, int calcCD)
            {
                LogUtility.DebugMethodStart(kingaku, calcCD);

                decimal returnVal = 0;		// 戻り値
                decimal sign = 1;
                if (kingaku < 0)
                {
                    sign = -1;
                }

                kingaku = Math.Abs(kingaku);

                switch ((fractionType)calcCD)
                {
                    case fractionType.CEILING:
                        returnVal = Math.Ceiling(kingaku);
                        break;
                    case fractionType.FLOOR:
                        returnVal = Math.Floor(kingaku);
                        break;
                    case fractionType.ROUND:
                        returnVal = Math.Round(kingaku, MidpointRounding.AwayFromZero);
                        break;
                    default:
                        // 切捨て
                        returnVal = Math.Floor(kingaku);
                        break;
                }

                returnVal = returnVal * sign;

                LogUtility.DebugMethodEnd();
                return returnVal;
            }

            /// <summary>
            /// 金額の共通フォーマット
            /// </summary>
            /// <param name="num"></param>
            /// <returns></returns>
            public static string DecimalFormat(decimal num)
            {
                string format = "#,##0";
                string returnval = string.Format("{0:" + format + "}", num);
                return returnval;
            }


            /// <summary>
            /// 単価、数量の共通フォーマット
            /// </summary>
            /// <param name="num"></param>
            /// <param name="format"></param>
            /// <returns></returns>
            public static string SuuryouAndTankFormat(object num, String format)
            {
                string returnVal = string.Empty;
                try
                {
                    LogUtility.DebugMethodStart(num, format);

                    returnVal = string.Format("{0:" + format + "}", num);

                    return returnVal;
                }
                catch (Exception ex)
                {
                    LogUtility.Error(ex);
                    throw;
                }
                finally
                {
                    LogUtility.DebugMethodEnd(returnVal);
                }
            }


        }
        #endregion 共通で使用する計算処理クラス

        #region 明細数量項目フォーマット
        /// <summary>
        /// 明細数量項目フォーマット
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        internal string meisaiSuuryouFormat(string name, string value)
        {
            string returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(name, value);
                // 重量フォーマット形式
                String systemJyuryouFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_JYURYOU_FORMAT, string.Empty).ToString();
                // 単価フォーマット
                String systemTankaFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_TANKA_FORMAT, string.Empty).ToString();

                if (!string.IsNullOrEmpty(value))
                {
                    //フォーマット
                    switch (name)
                    {
                        // 正味
                        case "STACK_JYUURYOU":
                        // 調整
                        case "CHOUSEI_JYUURYOU":
                        // 実正味
                        case "NET_JYUURYOU":
                        // 数量
                        case "SUURYOU":
                            returnVal = CommonCalc.SuuryouAndTankFormat(decimal.Parse(value), systemJyuryouFormat);
                            break;

                        // 単価
                        case "TANKA":
                            returnVal = CommonCalc.SuuryouAndTankFormat(decimal.Parse(value), systemTankaFormat);
                            break;
                    }

                }
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 明細行必須入力チェック

        /// <summary>
        /// Detail必須入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckRequiredDataForDeital()
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool dataUmuFlg = false;
                bool kingakuCheckFlg = true;
                string cellvalue = string.Empty;
                string cellName = string.Empty;
                int index = 0;

                foreach (var row in this.form.Ichiran.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    dataUmuFlg = true;

                    // 品名については、r-frameworkの共通処理で必須チェックを行っていることと
                    // 入力時にマスタ存在チェックをしていることからこのメソッドでのチェックは不要と判断する。

                    /* ここで行っている金額チェックの計算方法は明細の金額計算と同様です。 */
                    /* どちらかの変更を行った際にはもう一方も修正してください。           */
                    {
                        // 受入金額　数量*単価=金額のチェック
                        // 金額の計算は数量*単価で行っているため基本ありえないが何か起きた場合のため
                        if (row.Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].FormattedValue.ToString()) &&
                            row.Cells[ConstClass.CONTROL_UKEIRE_TANKA].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[ConstClass.CONTROL_UKEIRE_TANKA].FormattedValue.ToString()))
                        {
                            decimal suryou = decimal.Parse(row.Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].FormattedValue.ToString());
                            decimal tanka = decimal.Parse(row.Cells[ConstClass.CONTROL_UKEIRE_TANKA].FormattedValue.ToString());
                            decimal kingaku = decimal.Parse(row.Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].FormattedValue.ToString());

                            // 端数設定
                            short kingakuHasuuCd = this.CalcHasuu(row.Index, 1);
                            decimal tmpKingaku = decimal.Parse(CommonCalc.DecimalFormat(CommonCalc.FractionCalc((decimal)suryou * tanka, kingakuHasuuCd)));

                            if (!tmpKingaku.Equals(kingaku))
                            {
                                kingakuCheckFlg = false;
                                index = row.Index;
                                cellName = ConstClass.CONTROL_UKEIRE_KINGAKU;
                                break;
                            }
                        }

                        // 出荷金額　数量*単価=金額のチェック
                        // 金額の計算は数量*単価で行っているため基本ありえないが何か起きた場合のため
                        if (row.Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].FormattedValue.ToString()) &&
                            row.Cells[ConstClass.CONTROL_SHUKKA_TANKA].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[ConstClass.CONTROL_SHUKKA_TANKA].FormattedValue.ToString()))
                        {
                            decimal suryou = decimal.Parse(row.Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].FormattedValue.ToString());
                            decimal tanka = decimal.Parse(row.Cells[ConstClass.CONTROL_SHUKKA_TANKA].FormattedValue.ToString());
                            decimal kingaku = decimal.Parse(row.Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].FormattedValue.ToString());

                            // 端数設定
                            short kingakuHasuuCd = this.CalcHasuu(row.Index, 2);
                            decimal tmpKingaku = decimal.Parse(CommonCalc.DecimalFormat(CommonCalc.FractionCalc((decimal)suryou * tanka, kingakuHasuuCd)));

                            if (!tmpKingaku.Equals(kingaku))
                            {
                                kingakuCheckFlg = false;
                                index = row.Index;
                                cellName = ConstClass.CONTROL_SHUKKA_KINGAKU;
                                break;
                            }
                        }
                    }
                }

                if (dataUmuFlg && kingakuCheckFlg)
                {
                    returnVal = true;
                }
                else if (!dataUmuFlg)
                {
                    msgLogic.MessageBoxShow("E001", "明細行");
                }
                else if (!kingakuCheckFlg)
                {
                    msgLogic.MessageBoxShowError("数量と単価の乗算が金額と一致しない明細が存在します。");
                    this.form.Ichiran.Focus();
                    this.form.Ichiran.CurrentCell = this.form.Ichiran.Rows[index].Cells[cellName];
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckRequiredDataForDeital", ex1);
                this.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRequiredDataForDeital", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }

        #endregion

        #region 入力担当者チェック
        /// <summary>
        /// 入力担当者チェック
        /// </summary>
        internal bool CheckNyuuryokuTantousha()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
                {
                    this.form.NYUURYOKU_TANTOUSHA_NAME_RYAKU.Text = string.Empty;
                    return ret;
                }

                // 変更なし場合、何も処理しない
                if (this.form.dicControl.ContainsKey(ConstClass.CONTROL_NYUURYOKU_TANTOUSHA_CD) &&
                    this.form.dicControl[ConstClass.CONTROL_NYUURYOKU_TANTOUSHA_CD].Equals(this.form.NYUURYOKU_TANTOUSHA_CD.Text) &&
                    !string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_NAME_RYAKU.Text))
                {
                    return ret;
                }

                var shainEntity = this.accessor.GetNyuuryokuTantousha(this.form.NYUURYOKU_TANTOUSHA_CD.Text);
                if (shainEntity == null)
                {
                    this.form.NYUURYOKU_TANTOUSHA_NAME_RYAKU.Text = string.Empty;

                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "社員");
                    this.form.NYUURYOKU_TANTOUSHA_CD.Focus();
                    this.form.NYUURYOKU_TANTOUSHA_CD.IsInputErrorOccured = true;
                    return ret;
                }
                else
                {
                    this.form.NYUURYOKU_TANTOUSHA_NAME_RYAKU.Text = shainEntity.SHAIN_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex);
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

        #region 運搬業者チェック
        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        internal bool CheckUnpanGyousha()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // 初期化
                    this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    this.form.SHARYOU_CD.Text = string.Empty;
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;

                    // 変更なし場合、何も処理しない
                    if (this.form.dicControl[ConstClass.CONTROL_UNPAN_GYOUSHA_CD].Equals(this.form.UNPAN_GYOUSHA_CD.Text))
                    {
                        return ret;
                    }

                    // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
                    this.form.Ichiran.Rows.Cast<Row>()
                        .Where(w => !w.IsNewRow).ToList()
                        .ForEach(r =>
                        {
                            this.CalcTanka(r.Index, 1);
                            this.CalcTanka(r.Index, 2);
                            this.SetHinmeiSuuryou(r.Index, 1);
                            this.SetHinmeiSuuryou(r.Index, 2);
                            this.CalcSuuryou(r.Index, 1);
                            this.CalcSuuryou(r.Index, 2);
                            this.CalcDetailKingaku(r.Index, 1);
                            this.CalcDetailKingaku(r.Index, 2);
                        });
                    this.ResetTankaCheck(); // MAILAN #158994 START

                    // 合計系の再計算
                    ret = this.CalcAllDetailAndTotal();

                    return ret;
                }

                // 変更なし場合、何も処理しない
                if (this.form.dicControl.ContainsKey(ConstClass.CONTROL_UNPAN_GYOUSHA_CD) &&
                    this.form.dicControl[ConstClass.CONTROL_UNPAN_GYOUSHA_CD].Equals(this.form.UNPAN_GYOUSHA_CD.Text) &&
                    !string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text))
                {
                    return ret;
                }

                var gyoushaEntity = this.accessor.GetUnpanGyousya(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                if (gyoushaEntity == null)
                {
                    this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text = string.Empty;

                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    return ret;
                }
                else
                {
                    this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;

                    SqlDateTime denpyouDate = SqlDateTime.Null;
                    if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE.Text))
                    {
                        denpyouDate = SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                    }
                    var sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, denpyouDate);
                    if (sharyouEntitys == null || sharyouEntitys.Length <= 0)
                    {
                        // 車輌をクリア
                        this.form.SHARYOU_CD.Text = string.Empty;
                        this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        var sharyouEntity = sharyouEntitys[0];
                        this.form.SHARYOU_CD.Text = sharyouEntity.SHARYOU_CD;
                        this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;

                        // 運転者情報セット
                        var untensha = this.accessor.GetUntensha(sharyouEntity.SHAIN_CD);
                        if (untensha != null)
                        {
                            this.form.UNTENSHA_CD.Text = untensha.SHAIN_CD;
                            this.form.UNTENSHA_NAME_RYAKU.Text = untensha.SHAIN_NAME_RYAKU;
                        }

                        // 車種情報セット
                        var shashuEntity = this.accessor.GetShashu(sharyouEntity.SHASYU_CD);
                        if (shashuEntity != null)
                        {
                            this.form.SHASHU_CD.Text = shashuEntity.SHASHU_CD;
                            this.form.SHASHU_NAME_RYAKU.Text = shashuEntity.SHASHU_NAME_RYAKU;
                        }
                    }

                    // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
                    this.form.Ichiran.Rows.Cast<Row>()
                        .Where(w => !w.IsNewRow).ToList()
                        .ForEach(r =>
                        {
                            this.CalcTanka(r.Index, 1);
                            this.CalcTanka(r.Index, 2);
                            this.SetHinmeiSuuryou(r.Index, 1);
                            this.SetHinmeiSuuryou(r.Index, 2);
                            this.CalcSuuryou(r.Index, 1);
                            this.CalcSuuryou(r.Index, 2);
                            this.CalcDetailKingaku(r.Index, 1);
                            this.CalcDetailKingaku(r.Index, 2);
                        });
                    this.ResetTankaCheck(); // MAILAN #158994 START

                    // 合計系の再計算
                    ret = this.CalcAllDetailAndTotal();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUnpanGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyousha", ex);
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

        #region 車種チェック
        /// <summary>
        /// 車種チェック
        /// </summary>
        internal bool CheckShashu()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    this.form.SHASHU_NAME_RYAKU.Text = string.Empty;
                    return ret;
                }

                // 変更なし場合、何も処理しない
                if (this.form.dicControl.ContainsKey(ConstClass.CONTROL_SHASHU_CD) &&
                    this.form.dicControl[ConstClass.CONTROL_SHASHU_CD].Equals(this.form.SHASHU_CD.Text) &&
                    !string.IsNullOrEmpty(this.form.SHASHU_NAME_RYAKU.Text))
                {
                    return ret;
                }

                var shashuEntity = this.accessor.GetShashu(this.form.SHASHU_CD.Text);
                if (shashuEntity == null)
                {
                    this.form.SHASHU_NAME_RYAKU.Text = string.Empty;

                    // エラーメッセージ
                    this.form.SHASHU_NAME_RYAKU.Text = string.Empty;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "車種");
                    this.form.SHASHU_CD.Focus();
                    this.form.SHASHU_CD.IsInputErrorOccured = true;
                    return ret;
                }
                else
                {
                    this.form.SHASHU_NAME_RYAKU.Text = shashuEntity.SHASHU_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckShashu", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShashu", ex);
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

        #region 車輌チェック

        /// <summary>
        /// 車輌チェック
        /// </summary>
        internal bool CheckSharyou()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                M_SHARYOU[] sharyouEntitys = null;

                // 初期化
                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                    return ret;
                }

                // 変更なし場合、何も処理しない
                if (this.form.dicControl.ContainsKey(ConstClass.CONTROL_SHARYOU_CD) &&
                    this.form.dicControl[ConstClass.CONTROL_SHARYOU_CD].Equals(this.form.SHARYOU_CD.Text) &&
                    !string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
                {
                    return ret;
                }

                SqlDateTime denpyouDate = SqlDateTime.Null;
                if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE.Text))
                {
                    denpyouDate = SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                }
                sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, denpyouDate);

                // マスタ存在チェック
                if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                {
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;

                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "車輌");
                    this.form.SHARYOU_CD.Focus();
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    return ret;
                }

                // 車輌休動チェック
                if (sharyouEntitys.Length == 1 && !this.SharyouDateCheck())
                {
                    return ret;
                }

                // ポップアップから戻ってきたときに運搬業者名が無いため取得
                M_GYOUSHA unpanGyousya = this.accessor.GetUnpanGyousya(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                if (unpanGyousya != null)
                {
                    this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text = unpanGyousya.GYOUSHA_NAME_RYAKU;
                }

                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text))
                {
                    M_SHARYOU sharyou = new M_SHARYOU();

                    // 運搬業者チェック
                    bool isCheck = false;
                    foreach (M_SHARYOU sharyouEntity in sharyouEntitys)
                    {
                        if (sharyouEntity.GYOUSHA_CD.Equals(this.form.UNPAN_GYOUSHA_CD.Text))
                        {
                            isCheck = true;
                            sharyou = sharyouEntity;
                            break;
                        }
                    }

                    if (isCheck)
                    {
                        // 車輌データセット
                        this.SetSharyou(sharyou);
                        return ret;
                    }
                    else
                    {
                        // エラーメッセージ
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E062", "運搬業者");
                        this.form.UNPAN_GYOUSHA_CD.Focus();
                        this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                        return ret;
                    }
                }
                else
                {
                    if (sharyouEntitys.Length > 1)
                    {
                        this.form.SHARYOU_CD.Focus();

                        this.form.FocusOutErrorFlag = true;

                        // この時は車輌CDを検索条件に含める
                        this.PopUpConditionsSharyouSwitch(true);

                        // 検索ポップアップ起動
                        CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);

                        this.PopUpConditionsSharyouSwitch(false);

                        this.form.FocusOutErrorFlag = false;
                    }
                    else
                    {
                        // 一意レコード
                        // 車輌データセット
                        this.SetSharyou(sharyouEntitys[0]);
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckSharyou", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyou", ex);
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
        /// 車輌休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool SharyouDateCheck()
        {
            string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
            string inputSharyouCd = this.form.SHARYOU_CD.Text;
            string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Value.ToString());

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (String.IsNullOrEmpty(inputSagyouDate))
            {
                return true;
            }

            var workclosedsharyouList = this.accessor.GetWorkClosedSharyou(inputUnpanGyoushaCd, inputSharyouCd, inputSagyouDate);

            //取得テータ
            if (workclosedsharyouList != null && workclosedsharyouList.Length >= 1)
            {
                msgLogic.MessageBoxShow("E206", "車輌", "伝票日付：" + (DateTime.Parse(inputSagyouDate)).ToString("yyyy/MM/dd"));
                this.form.SHARYOU_CD.Focus();
                this.form.SHARYOU_CD.IsInputErrorOccured = true;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 車輌PopUpの検索条件に車輌CDを含めるかを引数によって設定します
        /// </summary>
        /// <param name="isPopupConditionsSharyouCD"></param>
        internal void PopUpConditionsSharyouSwitch(bool isPopupConditionsSharyouCD)
        {
            PopupSearchSendParamDto sharyouParam = new PopupSearchSendParamDto();
            sharyouParam.And_Or = CONDITION_OPERATOR.AND;
            sharyouParam.Control = "SHARYOU_CD";
            sharyouParam.KeyName = "key002";

            if (isPopupConditionsSharyouCD)
            {
                if (!this.form.SHARYOU_CD.PopupSearchSendParams.Contains(sharyouParam))
                {
                    this.form.SHARYOU_CD.PopupSearchSendParams.Add(sharyouParam);
                }
            }
            else
            {
                var paramsCount = this.form.SHARYOU_CD.PopupSearchSendParams.Count;
                for (int i = 0; i < paramsCount; i++)
                {
                    if (this.form.SHARYOU_CD.PopupSearchSendParams[i].Control == "SHARYOU_CD" &&
                        this.form.SHARYOU_CD.PopupSearchSendParams[i].KeyName == "key002")
                    {
                        this.form.SHARYOU_CD.PopupSearchSendParams.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 車輌情報をセット
        /// </summary>
        /// <param name="sharyouEntity"></param>
        private void SetSharyou(M_SHARYOU sharyouEntity)
        {
            this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
            this.form.UNTENSHA_CD.Text = sharyouEntity.SHAIN_CD;
            this.form.SHASHU_CD.Text = sharyouEntity.SHASYU_CD;
            this.form.UNPAN_GYOUSHA_CD.Text = sharyouEntity.GYOUSHA_CD;

            // 運転者情報セット
            var untensha = this.accessor.GetUntensha(sharyouEntity.SHAIN_CD);
            if (untensha != null)
            {
                this.form.UNTENSHA_NAME_RYAKU.Text = untensha.SHAIN_NAME_RYAKU;
            }
            else
            {
                this.form.UNTENSHA_NAME_RYAKU.Text = string.Empty;
            }

            // 車種情報セット
            var shashu = this.accessor.GetShashu(this.form.SHASHU_CD.Text);
            if (shashu != null)
            {
                this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                this.form.SHASHU_NAME_RYAKU.Text = shashu.SHASHU_NAME_RYAKU;
            }
            else
            {
                this.form.SHASHU_NAME_RYAKU.Text = string.Empty;
            }

            // 運搬業者名セット
            this.CheckUnpanGyousha();
        }

        #endregion

        #region 運転者チェック
        /// <summary>
        /// 運転者チェック
        /// </summary>
        internal bool CheckUntensha()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    this.form.UNTENSHA_NAME_RYAKU.Text = string.Empty;
                    return ret;
                }

                // 変更なし場合、何も処理しない
                if (this.form.dicControl.ContainsKey(ConstClass.CONTROL_UNTENSHA_CD) &&
                    this.form.dicControl[ConstClass.CONTROL_UNTENSHA_CD].Equals(this.form.UNTENSHA_CD.Text) &&
                    !string.IsNullOrEmpty(this.form.UNTENSHA_NAME_RYAKU.Text))
                {
                    return ret;
                }

                var shashuEntity = this.accessor.GetUntensha(this.form.UNTENSHA_CD.Text);
                if (shashuEntity == null)
                {
                    this.form.UNTENSHA_NAME_RYAKU.Text = string.Empty;

                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "社員");
                    this.form.UNTENSHA_CD.Focus();
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    return ret;
                }
                else if (this.UntenshaDateCheck())
                {
                    this.form.UNTENSHA_NAME_RYAKU.Text = shashuEntity.SHAIN_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUntensha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntensha", ex);
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
        /// 運転者休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool UntenshaDateCheck()
        {
            string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
            string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (String.IsNullOrEmpty(inputSagyouDate))
            {
                return true;
            }

            var workcloseduntenshaList = this.accessor.GetWorkClosedUntensha(inputUntenshaCd, inputSagyouDate);

            //取得テータ
            if (workcloseduntenshaList != null && workcloseduntenshaList.Length >= 1)
            {
                msgLogic.MessageBoxShow("E206", "運転者", "伝票日付：" + (DateTime.Parse(inputSagyouDate)).ToString("yyyy/MM/dd"));
                this.form.UNTENSHA_CD.Focus();
                this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                return false;
            }

            return true;
        }
        #endregion

        #region 取引先チェック
        /// <summary>
        /// 取引先チェック
        /// </summary>
        /// <param name="flg">1:受入;2:出荷</param>
        internal bool CheckTorihikisaki(int flg)
        {
            LogUtility.DebugMethodStart(flg);

            bool returnVal = false;

            try
            {
                this.SetTorihikiControlInfo(flg);

                // 未入力の場合
                if (string.IsNullOrEmpty(this.torihikisakiCd.Text))
                {
                    // 取引先名、締日の初期化
                    this.torihikisakiNm.Text = string.Empty;
                    this.shimebi1.Text = string.Empty;
                    this.shimebi2.Text = string.Empty;
                    this.shimebi3.Text = string.Empty;

                    // 変更ありの場合営業担当者を設定する
                    if (!this.form.dicControl.ContainsKey(this.controlTorihikisaki) ||
                        !this.form.dicControl[this.controlTorihikisaki].Equals(this.torihikisakiCd.Text))
                    {
                        this.SetEigyouTantousha(this.genbaCd.Text, this.gyoushaCd.Text, this.torihikisakiCd.Text);
                    }

                    return true;
                }

                // 変更なし場合、何も処理しない
                if (this.form.dicControl.ContainsKey(this.controlTorihikisaki) &&
                    this.form.dicControl[this.controlTorihikisaki].Equals(this.torihikisakiCd.Text) &&
                    !string.IsNullOrEmpty(this.torihikisakiNm.Text))
                {
                    return true;
                }

                // 取引先
                var torihikisakiEntity = this.accessor.GetTorihikisaki(this.torihikisakiCd.Text, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                M_TORIHIKISAKI_SHIHARAI torihikisakiShiharai = new M_TORIHIKISAKI_SHIHARAI();
                M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuu = new M_TORIHIKISAKI_SEIKYUU();

                if (flg == 1)
                {
                    torihikisakiShiharai = this.accessor.GetTorihikisakiShiharai(this.torihikisakiCd.Text);
                }
                else if (flg == 2)
                {
                    torihikisakiSeikyuu = this.accessor.GetTorihikisakiSeikyuu(this.torihikisakiCd.Text);
                }

                // 取引区分「現金」の取引先は選択不能とする
                if (torihikisakiEntity == null ||
                    (flg == 1 && (torihikisakiShiharai == null || torihikisakiShiharai.TORIHIKI_KBN_CD == 1)) ||
                    (flg == 2 && (torihikisakiSeikyuu == null || torihikisakiSeikyuu.TORIHIKI_KBN_CD == 1)))
                {
                    // 取引先名、締日の初期化
                    this.torihikisakiNm.Text = string.Empty;
                    this.shimebi1.Text = string.Empty;
                    this.shimebi2.Text = string.Empty;
                    this.shimebi3.Text = string.Empty;
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");

                    this.form.UKEIRE_GENBA_CD.Validated -= this.form.UKEIRE_GENBA_CD_Validated;
                    this.form.SHUKKA_GENBA_CD.Validated -= this.form.SHUKKA_GENBA_CD_Validated;
                    this.form.UKEIRE_GYOUSHA_CD.Validated -= this.form.UKEIRE_GYOUSHA_CD_Validated;
                    this.form.SHUKKA_GYOUSHA_CD.Validated -= this.form.SHUKKA_GYOUSHA_CD_Validated;
                    this.torihikisakiCd.Focus();
                    this.form.UKEIRE_GENBA_CD.Validated += this.form.UKEIRE_GENBA_CD_Validated;
                    this.form.SHUKKA_GENBA_CD.Validated += this.form.SHUKKA_GENBA_CD_Validated;
                    this.form.UKEIRE_GYOUSHA_CD.Validated += this.form.UKEIRE_GYOUSHA_CD_Validated;
                    this.form.SHUKKA_GYOUSHA_CD.Validated += this.form.SHUKKA_GYOUSHA_CD_Validated;
                    this.torihikisakiCd.IsInputErrorOccured = true;
                }
                else if (this.CheckTorihikisakiAndKyotenCd(torihikisakiEntity, this.torihikisakiCd))
                {
                    // 取引先名設定
                    this.torihikisakiNm.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;

                    // 締日設定
                    this.SetTorihikisakiShimeibi(flg);

                    // 営業担当者の設定
                    this.SetEigyouTantousha(this.genbaCd.Text, this.gyoushaCd.Text, this.torihikisakiCd.Text);

                    returnVal = true;
                }

                return returnVal;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 取引先の拠点コードと入力された拠点コードの関連チェック
        /// </summary>
        /// <param name="torihikisakiEntity">取引先エンティティ</param>
        /// <param name="TorihikisakiCd">取引先CD</param>
        /// <returns>True：チェックOK False：チェックNG</returns>
        internal bool CheckTorihikisakiAndKyotenCd(M_TORIHIKISAKI torihikisakiEntity, object torihikisakiCd)
        {
            bool returnVal = false;

            try
            {
                var torihikisaki = (CustomAlphaNumTextBox)torihikisakiCd;
                if (string.IsNullOrEmpty(torihikisaki.Text))
                {
                    // 取引先の入力がない場合はチェック対象外
                    return true;
                }

                if (torihikisakiEntity == null)
                {
                    torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisaki.Text, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                }

                if (torihikisakiEntity != null)
                {
                    if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                    {
                        if (SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text) == torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD
                            || torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString().Equals(ConstClass.KYOTEN_ZENSHA))
                        {
                            // 入力画面の拠点コードと取引先の拠点コードが等しいか、取引先の拠点コードが99（全社)の場合
                            returnVal = true;
                        }
                        else
                        {
                            // 取引先名、締日の初期化
                            this.torihikisakiNm.Text = string.Empty;
                            this.shimebi1.Text = string.Empty;
                            this.shimebi2.Text = string.Empty;
                            this.shimebi3.Text = string.Empty;

                            // 入力画面の拠点コードと取引先の拠点コードが等しくない場合
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E146");
                            this.form.UKEIRE_GENBA_CD.Validated -= this.form.UKEIRE_GENBA_CD_Validated;
                            this.form.SHUKKA_GENBA_CD.Validated -= this.form.SHUKKA_GENBA_CD_Validated;
                            this.form.UKEIRE_GYOUSHA_CD.Validated -= this.form.UKEIRE_GYOUSHA_CD_Validated;
                            this.form.SHUKKA_GYOUSHA_CD.Validated -= this.form.SHUKKA_GYOUSHA_CD_Validated;
                            this.torihikisakiCd.Focus();
                            this.form.UKEIRE_GENBA_CD.Validated += this.form.UKEIRE_GENBA_CD_Validated;
                            this.form.SHUKKA_GENBA_CD.Validated += this.form.SHUKKA_GENBA_CD_Validated;
                            this.form.UKEIRE_GYOUSHA_CD.Validated += this.form.UKEIRE_GYOUSHA_CD_Validated;
                            this.form.SHUKKA_GYOUSHA_CD.Validated += this.form.SHUKKA_GYOUSHA_CD_Validated;
                            torihikisaki.IsInputErrorOccured = true;
                        }
                    }
                    else
                    {   // 拠点が指定されていない場合
                        returnVal = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckTorihikisakiAndKyotenCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiAndKyotenCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }

            return returnVal;
        }

        /// <summary>
        /// 締日の設定
        /// </summary>
        /// <param name="flg">1:受入;2:出荷</param>
        private void SetTorihikisakiShimeibi(int flg)
        {
            if (flg == 1)
            {
                var torihikisakiSiharaiEntity = this.accessor.GetTorihikisakiShiharai(this.form.UKEIRE_TORIHIKISAKI_CD.Text);
                if (torihikisakiSiharaiEntity != null)
                {
                    // 締日1
                    if (!torihikisakiSiharaiEntity.SHIMEBI1.IsNull)
                    {
                        this.form.UKEIRE_SHIMEBI1.Text = torihikisakiSiharaiEntity.SHIMEBI1.ToString();
                    }
                    else
                    {
                        this.form.UKEIRE_SHIMEBI1.Text = string.Empty;
                    }

                    // 締日2
                    if (!torihikisakiSiharaiEntity.SHIMEBI2.IsNull)
                    {
                        this.form.UKEIRE_SHIMEBI2.Text = torihikisakiSiharaiEntity.SHIMEBI2.ToString();
                    }
                    else
                    {
                        this.form.UKEIRE_SHIMEBI2.Text = string.Empty;
                    }

                    // 締日3
                    if (!torihikisakiSiharaiEntity.SHIMEBI3.IsNull)
                    {
                        this.form.UKEIRE_SHIMEBI3.Text = torihikisakiSiharaiEntity.SHIMEBI3.ToString();
                    }
                    else
                    {
                        this.form.UKEIRE_SHIMEBI3.Text = string.Empty;
                    }
                }
            }
            else if (flg == 2)
            {
                var torihikisakiSeikyuuEntity = this.accessor.GetTorihikisakiSeikyuu(this.form.SHUKKA_TORIHIKISAKI_CD.Text);
                if (torihikisakiSeikyuuEntity != null)
                {
                    // 締日1
                    if (!torihikisakiSeikyuuEntity.SHIMEBI1.IsNull)
                    {
                        this.form.SHUKKA_SHIMEBI1.Text = torihikisakiSeikyuuEntity.SHIMEBI1.ToString();
                    }
                    else
                    {
                        this.form.SHUKKA_SHIMEBI1.Text = string.Empty;
                    }

                    // 締日2
                    if (!torihikisakiSeikyuuEntity.SHIMEBI2.IsNull)
                    {
                        this.form.SHUKKA_SHIMEBI2.Text = torihikisakiSeikyuuEntity.SHIMEBI2.ToString();
                    }
                    else
                    {
                        this.form.SHUKKA_SHIMEBI2.Text = string.Empty;
                    }

                    // 締日3
                    if (!torihikisakiSeikyuuEntity.SHIMEBI3.IsNull)
                    {
                        this.form.SHUKKA_SHIMEBI3.Text = torihikisakiSeikyuuEntity.SHIMEBI3.ToString();
                    }
                    else
                    {
                        this.form.SHUKKA_SHIMEBI3.Text = string.Empty;
                    }
                }
            }
        }
        #endregion

        #region 業者チェック
        /// <summary>
        /// 業者チェック
        /// </summary>
        /// <param name="flg">1:受入;2:出荷</param>
        internal bool CheckGyousha(int flg)
        {
            LogUtility.DebugMethodStart(flg);

            bool returnVal = false;
            try
            {
                this.SetTorihikiControlInfo(flg);

                // 未入力の場合
                if (string.IsNullOrEmpty(this.gyoushaCd.Text))
                {
                    // 業者、現場と営業担当者の初期化
                    this.gyoushaNm.Text = string.Empty;
                    this.genbaCd.Text = string.Empty;
                    this.genbaNm.Text = string.Empty;

                    // 変更ありの場合営業担当者を設定する
                    if (!this.form.dicControl.ContainsKey(this.controlGyousha) ||
                        !this.form.dicControl[this.controlGyousha].Equals(this.gyoushaCd.Text))
                    {
                        this.SetEigyouTantousha(this.genbaCd.Text, this.gyoushaCd.Text, this.torihikisakiCd.Text);
                    }

                    return true;
                }

                // 変更なし場合、何も処理しない
                if (this.form.dicControl.ContainsKey(this.controlGyousha) &&
                    this.form.dicControl[this.controlGyousha].Equals(this.gyoushaCd.Text) &&
                    !string.IsNullOrEmpty(this.gyoushaNm.Text))
                {
                    return true;
                }

                // 現場の初期化
                this.genbaCd.Text = string.Empty;
                this.genbaNm.Text = string.Empty;

                // 業者を取得
                var gyoushaEntity = this.accessor.GetGyousha(this.gyoushaCd.Text, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);

                // 取得できない場合
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (gyoushaEntity == null)
                {
                    // 業者初期化
                    this.gyoushaNm.Text = string.Empty;
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.gyoushaCd.Focus();
                    this.gyoushaCd.IsInputErrorOccured = true;
                    return returnVal;
                }
                else if ((flg == 1 && !gyoushaEntity.GYOUSHAKBN_UKEIRE) || (flg == 2 && !gyoushaEntity.GYOUSHAKBN_SHUKKA))
                {
                    // 業者初期化
                    this.gyoushaNm.Text = string.Empty;
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E058");
                    this.gyoushaCd.Focus();
                    this.gyoushaCd.IsInputErrorOccured = true;
                    return returnVal;
                }
                else
                {
                    returnVal = true;

                    // 業者名設定
                    this.gyoushaNm.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;

                    // 取引先を取得
                    var torihikisakiEntity = this.accessor.GetTorihikisaki(gyoushaEntity.TORIHIKISAKI_CD, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                    if (null != torihikisakiEntity && !this.torihikisakiCd.IsInputErrorOccured)
                    {
                        this.torihikisakiCd.Text = torihikisakiEntity.TORIHIKISAKI_CD;
                        this.torihikisakiNm.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                        returnVal = this.CheckTorihikisaki(flg);
                        if (!returnVal)
                        {
                            return returnVal;
                        }
                    }
                }

                // 営業担当者の設定
                this.SetEigyouTantousha(this.genbaCd.Text, this.gyoushaCd.Text, this.torihikisakiCd.Text);

                return returnVal;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 現場チェック
        /// <summary>
        /// 現場チェック
        /// </summary>
        /// <param name="flg">1:受入;2:出荷</param>
        internal bool CheckGenba(int flg)
        {
            LogUtility.DebugMethodStart(flg);

            bool returnVal = false;

            try
            {
                this.SetTorihikiControlInfo(flg);

                // 未入力の場合
                if (string.IsNullOrEmpty(this.genbaCd.Text))
                {
                    // 現場の初期化
                    this.genbaCd.Text = string.Empty;
                    this.genbaNm.Text = string.Empty;
                    // 20150918 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    this.genbaCd.IsInputErrorOccured = false;
                    // 20150918 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

                    // 変更ありの場合営業担当者を設定する
                    if (!this.form.dicControl.ContainsKey(this.controlGyousha) ||
                        !this.form.dicControl[this.controlGyousha].Equals(this.gyoushaCd.Text) ||
                        !this.form.dicControl.ContainsKey(this.controlGenba) ||
                        !this.form.dicControl[this.controlGenba].Equals(this.genbaCd.Text))
                    {
                        this.SetEigyouTantousha(this.genbaCd.Text, this.gyoushaCd.Text, this.torihikisakiCd.Text);
                    }

                    return true;
                }

                // 変更なし場合、何も処理しない
                if (this.form.dicControl.ContainsKey(this.controlGyousha) &&
                    this.form.dicControl[this.controlGyousha].Equals(this.gyoushaCd.Text) &&
                    this.form.dicControl.ContainsKey(this.controlGenba) &&
                    this.form.dicControl[this.controlGenba].Equals(this.genbaCd.Text) &&
                    !string.IsNullOrEmpty(this.gyoushaNm.Text) &&
                    !string.IsNullOrEmpty(this.genbaNm.Text))
                {
                    return true;
                }

                // 業者入力されてない場合
                if (string.IsNullOrEmpty(this.gyoushaCd.Text))
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    // 20150918 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    msgLogic.MessageBoxShow("E051", "業者");
                    // 20150918 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    this.genbaCd.Text = string.Empty;
                    this.genbaCd.Focus();
                    this.genbaCd.IsInputErrorOccured = true;
                    // 処理終了
                    return returnVal;
                }

                // 現場情報を取得
                M_GENBA genbaEntity = this.accessor.GetGenba(this.gyoushaCd.Text, this.genbaCd.Text, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                // 取得できない場合
                if (genbaEntity == null)
                {
                    // 現場の初期化
                    this.genbaCd.Text = string.Empty;
                    this.genbaNm.Text = string.Empty;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.genbaCd.Focus();
                    this.genbaCd.IsInputErrorOccured = true;

                    // 処理終了
                    return returnVal;
                }

                // 取引先を取得
                var torihikisakiEntity = this.accessor.GetTorihikisaki(genbaEntity.TORIHIKISAKI_CD, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                if (null != torihikisakiEntity && !this.torihikisakiCd.IsInputErrorOccured)
                {
                    this.torihikisakiCd.Text = torihikisakiEntity.TORIHIKISAKI_CD;
                    this.torihikisakiNm.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                    returnVal = this.CheckTorihikisaki(flg);
                    if (!returnVal)
                    {
                        return returnVal;
                    }
                }

                // 業者を取得
                var gyoushaEntity = this.accessor.GetGyousha(genbaEntity.GYOUSHA_CD, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                if (null != gyoushaEntity && !this.gyoushaCd.IsInputErrorOccured)
                {
                    this.gyoushaCd.Text = gyoushaEntity.GYOUSHA_CD;
                    this.gyoushaNm.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    returnVal = this.CheckGyousha(flg);
                    if (!returnVal)
                    {
                        return returnVal;
                    }
                }

                this.genbaCd.Text = genbaEntity.GENBA_CD;
                this.genbaNm.Text = genbaEntity.GENBA_NAME_RYAKU;
                // 営業担当者の設定
                this.SetEigyouTantousha(this.genbaCd.Text, this.gyoushaCd.Text, this.torihikisakiCd.Text);

                return returnVal;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGenba", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 営業担当者の表示（現場マスタ、業者マスタ、取引先マスタを元に）
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        internal void SetEigyouTantousha(string genbaCd, string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(genbaCd, gyoushaCd, torihikisakiCd);

            M_GENBA genbaEntity = new M_GENBA();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            if (string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd) && string.IsNullOrEmpty(torihikisakiCd))
            {
                return;
            }

            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                // 業者CD入力あり
                if (!string.IsNullOrEmpty(genbaCd))
                {
                    // 現場CD入力あり
                    genbaEntity = this.accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                    if (genbaEntity != null)
                    {
                        // コードに対応する現場マスタが存在する
                        eigyouTantouCd = genbaEntity.EIGYOU_TANTOU_CD;
                        if (!string.IsNullOrEmpty(eigyouTantouCd))
                        {
                            // 現場マスタに営業担当者の設定がある場合
                            shainEntity = this.accessor.GetShain(eigyouTantouCd);
                            if (shainEntity != null)
                            {
                                // 現場CDで取得した現場マスタの営業担当者コードで、社員マスタを取得できた場合
                                if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                                {
                                    // 取得した社員マスタの社員名略が設定されている場合
                                    this.eigyoushaCd.Text = shainEntity.SHAIN_CD;
                                    this.eigyoushaNm.Text = shainEntity.SHAIN_NAME_RYAKU;
                                }
                                else
                                {
                                    // 取得した社員マスタの社員名略が設定されていない場合
                                    GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                                }
                            }
                            else
                            {
                                // 現場CDで取得した現場マスタの営業担当者コードで、社員マスタを取得できない場合
                                GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                            }
                        }
                        else
                        {
                            // 現場マスタに営業担当者の設定がない場合
                            GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                        }
                    }
                }
                else
                {
                    // 現場CD入力なし
                    GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                }
            }
            else
            {
                // 業者CD入力なし
                GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者マスタの営業担当者コードからの営業担当者取得(業者CD入力あり、業者マスタに存在することが前提)
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        private void GetEigyou_TantoushaOfGyousha(string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, torihikisakiCd);

            M_GYOUSHA gyoushaEntity = new M_GYOUSHA();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            gyoushaEntity = this.accessor.GetGyousha(gyoushaCd, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
            if (gyoushaEntity != null)
            {
                // コードに対応する業者マスタが存在する
                eigyouTantouCd = gyoushaEntity.EIGYOU_TANTOU_CD;
                if (!string.IsNullOrEmpty(eigyouTantouCd))
                {
                    // 業者マスタに営業担当者の設定がある場合
                    shainEntity = this.accessor.GetShain(eigyouTantouCd);
                    if (shainEntity != null)
                    {
                        // 業者CDで取得した業者マスタの営業担当者コードで、社員マスタを取得できた場合
                        if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                        {
                            // 取得した社員マスタの社員名略が設定されている場合
                            this.eigyoushaCd.Text = shainEntity.SHAIN_CD;
                            this.eigyoushaNm.Text = shainEntity.SHAIN_NAME_RYAKU;
                        }
                        else
                        {
                            // 取得した社員マスタの社員名略が設定されていない場合
                            GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                        }
                    }
                    else
                    {
                        // 業者CDで取得した業者マスタの営業担当者コードで、社員マスタを取得できない場合
                        GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                    }
                }
                else
                {
                    // 業者マスタに営業担当者の設定がない場合
                    GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                }
            }
            else
            {
                // コードに対応する業者マスタが存在しない
                // ただし、マスタ存在チェックはこの前になされているので、ここを通ることはない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先マスタの営業担当者コードからの営業担当者取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        private void GetEigyou_TantoushaOfTorihikisaki(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            M_TORIHIKISAKI torihikisakiEntity = new M_TORIHIKISAKI();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            if (!string.IsNullOrEmpty(torihikisakiCd))
            {
                // 取引先CD入力あり
                torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisakiCd, this.form.DENPYOU_DATE.Value, this.ParentForm.sysDate.Date);
                if (torihikisakiEntity != null)
                {
                    // コードに対応する取引先マスタが存在する
                    eigyouTantouCd = torihikisakiEntity.EIGYOU_TANTOU_CD;
                    if (!string.IsNullOrEmpty(eigyouTantouCd))
                    {
                        // 取引先マスタに営業担当者の設定がある場合
                        shainEntity = this.accessor.GetShain(eigyouTantouCd);
                        if (shainEntity != null)
                        {
                            // 取引先CDで取得した取引先マスタの営業担当者コードで、社員マスタを取得できた場合
                            if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                            {
                                // 取得した社員マスタの社員名略が設定されている場合
                                this.eigyoushaCd.Text = shainEntity.SHAIN_CD;
                                this.eigyoushaNm.Text = shainEntity.SHAIN_NAME_RYAKU;
                            }
                            else
                            {
                                // 取得した社員マスタの社員名略が設定されていない場合
                                this.eigyoushaCd.Text = string.Empty;
                                this.eigyoushaNm.Text = string.Empty;
                            }
                        }
                        else
                        {
                            // 取引先CDで取得した取引先マスタの営業担当者コードで、社員マスタを取得できない場合
                            this.eigyoushaCd.Text = string.Empty;
                            this.eigyoushaNm.Text = string.Empty;
                        }
                    }
                    else
                    {
                        // 取引先マスタに営業担当者の設定がない場合
                        this.eigyoushaCd.Text = string.Empty;
                        this.eigyoushaNm.Text = string.Empty;
                    }
                }
                else
                {
                    // コードに対応する取引先マスタが存在しない
                    // ただし、マスタ存在チェックはこの前になされているので、ここを通ることはない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    return;
                }
            }
            else
            {
                // 取引先CD入力なし
                this.eigyoushaCd.Text = string.Empty;
                this.eigyoushaNm.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 営業担当者チェック
        /// <summary>
        /// 営業担当者チェック
        /// </summary>
        /// <param name="flg">1:受入;2:出荷</param>
        internal bool CheckEigyousha(int flg)
        {
            LogUtility.DebugMethodStart(flg);

            bool returnVal = false;

            try
            {
                this.SetTorihikiControlInfo(flg);

                // 未入力の場合
                if (string.IsNullOrEmpty(this.eigyoushaCd.Text))
                {
                    // 現場の初期化
                    this.eigyoushaCd.Text = string.Empty;
                    this.eigyoushaNm.Text = string.Empty;
                    return true;
                }

                // 営業車情報を取得
                M_SHAIN eigyoushaEntity = this.accessor.GetEigyousha(this.eigyoushaCd.Text);
                // 取得できない場合
                if (eigyoushaEntity == null)
                {
                    // 現場の初期化
                    this.eigyoushaNm.Text = string.Empty;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "社員");
                    this.eigyoushaCd.Focus();
                    this.eigyoushaCd.IsInputErrorOccured = true;
                }
                else
                {
                    this.eigyoushaNm.Text = eigyoushaEntity.SHAIN_NAME_RYAKU;
                    returnVal = true;
                }

                return returnVal;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckEigyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEigyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 締状況チェック処理
        /// <summary>
        /// 締状況チェック処理
        /// 請求明細、精算明細、在庫明細を確認して、対象の伝票に締済のデータが存在するか確認する。
        /// </summary>
        internal void CheckAllShimeStatus()
        {
            this.ukeireShimeiCheckFlg = false;
            this.shukkaShimeiCheckFlg = false;

            //Thang Nguyen Update 20150626 #10664 Start
            if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }
            //Thang Nguyen Update 20150626 #10664 End

            long systemId = -1;
            int seq = -1;

            // 受入場合
            if (this.dainouUkeireEntry != null && !this.dainouUkeireEntry.SYSTEM_ID.IsNull) systemId = (long)this.dainouUkeireEntry.SYSTEM_ID;
            if (this.dainouUkeireEntry != null && !this.dainouUkeireEntry.SEQ.IsNull) seq = (int)this.dainouUkeireEntry.SEQ;
            if (systemId != -1 && seq != -1)
            {
                // 締処理状況判定用データ取得
                DataTable seisanData = this.accessor.GetSeisanMeisaiData(systemId, seq, -1, this.dainouUkeireEntry.TORIHIKISAKI_CD);

                // 締処理状況(精算明細)
                if (seisanData != null && 0 < seisanData.Rows.Count)
                {
                    this.ukeireShimeiCheckFlg = true;
                }
            }

            systemId = -1;
            seq = -1;

            // 出荷場合
            if (this.dainouShukkaEntry != null && !this.dainouShukkaEntry.SYSTEM_ID.IsNull) systemId = (long)this.dainouShukkaEntry.SYSTEM_ID;
            if (this.dainouShukkaEntry != null && !this.dainouShukkaEntry.SEQ.IsNull) seq = (int)this.dainouShukkaEntry.SEQ;
            if (systemId != -1 && seq != -1)
            {
                // 締処理状況判定用データ取得
                DataTable seikyuuData = this.accessor.GetSeikyuMeisaiData(systemId, seq, -1, this.dainouShukkaEntry.TORIHIKISAKI_CD);

                // 締処理状況(請求明細)
                if (seikyuuData != null && 0 < seikyuuData.Rows.Count)
                {
                    this.shukkaShimeiCheckFlg = true;
                }
            }
        }
        #endregion 締状況チェック処理

        /// <summary>
        /// ゼロサプレス処理
        /// </summary>
        /// <param name="source">入力コントロール</param>
        /// <returns>ゼロサプレス後の文字列</returns>
        private string ZeroSuppress(object source)
        {
            string result = string.Empty;

            // 該当コントロールの最大桁数を取得
            object obj;
            decimal charactersNumber;
            string text = PropertyUtility.GetTextOrValue(source);
            if (!PropertyUtility.GetValue(source, Constans.CHARACTERS_NUMBER, out obj))
                // 最大桁数が取得できない場合はそのまま
                return text;

            charactersNumber = (decimal)obj;
            if (charactersNumber == 0 || source == null || string.IsNullOrEmpty(text))
                // 最大桁数が0または入力値が空の場合はそのまま
                return text;

            var strCharactersUmber = text;
            if (strCharactersUmber.Contains("."))
                // 小数点を含む場合はそのまま
                return text;

            // ゼロサプレスした値を返す
            StringBuilder sb = new StringBuilder((int)charactersNumber);
            string format = sb.Append('#', (int)charactersNumber).ToString();
            long val = 0;
            if (long.TryParse(text, out val))
                result = val.ToString(format);
            else
                // 入力値が数値ではない場合はそのまま
                result = text;

            return result;
        }

        #region 会計年の取得

        /// <summary>
        /// 会計年の取得
        /// </summary>
        /// <param name="denpyouDate"></param>
        /// <param name="kishuMonth"></param>
        /// <returns></returns>
        private SqlInt32 GetKaikeiYear(DateTime denpyouDate, short kishuMonth)
        {
            return CorpInfoUtility.GetCurrentYear(denpyouDate, kishuMonth);
        }

        #endregion

        #region [F10]行挿入処理
        /// <summary>
        /// [F10]行挿入処理
        /// </summary>
        internal void AddNewRow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.Ichiran.CellValidated -= this.form.Ichiran_CellValidated;

                // 行挿入
                this.form.Ichiran.Rows.Insert(this.form.Ichiran.CurrentRow.Index, 1);

                this.form.Ichiran.CellValidated += this.form.Ichiran_CellValidated;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F11]行削除処理
        /// <summary>
        /// [F11]行削除処理
        /// </summary>
        internal bool RemoveSelectedRow()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.Ichiran.Rows.Count <= 1)
                {
                    // 処理終了
                    return ret;
                }

                //新規行は削除されません
                if (this.form.Ichiran.CurrentRow.Index != this.form.Ichiran.Rows.Count - this.newRowNum)
                {
                    // 行削除
                    this.form.Ichiran.Rows.RemoveAt(this.form.Ichiran.CurrentRow.Index);
                }

                // 合計値を再計算
                if (!this.CalcAllDetailAndTotal())
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RemoveSelectedRow", ex);
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

        #region 運賃入力関連処理

        /// <summary>
        /// 運賃入力画面を開く
        /// </summary>
        internal bool UpdateWindowShow()
        {
            bool ret = true;
            try
            {
                if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    this.UnchinRenkeiNumber = (long)this.insDainouUkeireEntry.UR_SH_NUMBER;
                }
                else
                {
                    this.UnchinRenkeiNumber = (long)this.dainouUkeireEntry.UR_SH_NUMBER;
                }
                this.UnchinRenkeiInfo = this.SetUnchinRenkeiInfo();
                int denshuKbn = DENSHU_KBN.DAINOU.GetHashCode();

                FormManager.OpenFormModal("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.UnchinRenkeiNumber, denshuKbn, this.UnchinRenkeiInfo, FormManager.CALLED_MENU);
                //this.CheckUpdateAuthority("G153", this.UnchinRenkeiNumber, denshuKbn, this.UnchinRenkeiInfo);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateWindowShow", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 運賃用な連携情報の設定
        /// </summary>
        /// <returns></returns>
        private T_UR_SH_ENTRY[] SetUnchinRenkeiInfo()
        {
            T_UR_SH_ENTRY[] returnval = new T_UR_SH_ENTRY[2];

            if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                returnval[0] = this.insDainouUkeireEntry;
                returnval[1] = this.insDainouShukkaEntry;
            }
            else
            {
                #region 受入情報
                returnval[0] = new T_UR_SH_ENTRY();
                if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    returnval[0].KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
                }
                returnval[0].UR_SH_NUMBER = this.PrmDainouNumber;
                returnval[0].DENPYOU_DATE = DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    returnval[0].SHARYOU_CD = this.form.SHARYOU_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    returnval[0].SHASHU_CD = this.form.SHASHU_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    returnval[0].UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    returnval[0].UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UKEIRE_TORIHIKISAKI_CD.Text))
                {
                    returnval[0].TORIHIKISAKI_CD = this.form.UKEIRE_TORIHIKISAKI_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UKEIRE_GYOUSHA_CD.Text))
                {
                    returnval[0].GYOUSHA_CD = this.form.UKEIRE_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UKEIRE_GENBA_CD.Text))
                {
                    returnval[0].GENBA_CD = this.form.UKEIRE_GENBA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UKEIRE_GYOUSHA_CD.Text))
                {
                    returnval[0].NIZUMI_GYOUSHA_CD = this.form.UKEIRE_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UKEIRE_GENBA_CD.Text))
                {
                    returnval[0].NIZUMI_GENBA_CD = this.form.UKEIRE_GENBA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UKEIRE_EIGYOU_TANTOUSHA_CD.Text))
                {
                    returnval[0].EIGYOU_TANTOUSHA_CD = this.form.UKEIRE_EIGYOU_TANTOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
                {
                    returnval[0].NYUURYOKU_TANTOUSHA_CD = this.form.NYUURYOKU_TANTOUSHA_CD.Text;
                }
                #endregion

                #region 出荷情報
                returnval[1] = new T_UR_SH_ENTRY();
                if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    returnval[1].KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
                }
                returnval[1].UR_SH_NUMBER = this.PrmDainouNumber;
                returnval[1].DENPYOU_DATE = DateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    returnval[1].SHARYOU_CD = this.form.SHARYOU_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    returnval[1].SHASHU_CD = this.form.SHASHU_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    returnval[1].UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    returnval[1].UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHUKKA_TORIHIKISAKI_CD.Text))
                {
                    returnval[1].TORIHIKISAKI_CD = this.form.SHUKKA_TORIHIKISAKI_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHUKKA_GYOUSHA_CD.Text))
                {
                    returnval[1].GYOUSHA_CD = this.form.SHUKKA_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHUKKA_GENBA_CD.Text))
                {
                    returnval[1].GENBA_CD = this.form.SHUKKA_GENBA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHUKKA_GYOUSHA_CD.Text))
                {
                    returnval[1].NIOROSHI_GYOUSHA_CD = this.form.SHUKKA_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHUKKA_GENBA_CD.Text))
                {
                    returnval[1].NIOROSHI_GENBA_CD = this.form.SHUKKA_GENBA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHUKKA_EIGYOU_TANTOUSHA_CD.Text))
                {
                    returnval[1].EIGYOU_TANTOUSHA_CD = this.form.SHUKKA_EIGYOU_TANTOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
                {
                    returnval[1].NYUURYOKU_TANTOUSHA_CD = this.form.NYUURYOKU_TANTOUSHA_CD.Text;
                }
                #endregion
            }

            return returnval;
        }

        /// <summary>
        /// 修正モード呼出時の権限チェック
        /// </summary>
        /// <param name="strFormId">フォームID</param>
        /// <param name="unchinRenkeiNumber">運賃連携番号</param>
        /// <param name="denshuKbn">伝種区分</param>
        /// <param name="unchinRenkeiInfo">運賃連携情報</param>
        internal void CheckUpdateAuthority(string strFormId, long unchinRenkeiNumber, int denshuKbn, T_UR_SH_ENTRY[] unchinRenkeiInfo)
        {
            // 修正モードの権限チェック
            if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, unchinRenkeiNumber, denshuKbn, unchinRenkeiInfo);
            }
            // 参照モードの権限チェック
            else if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, unchinRenkeiNumber, denshuKbn, unchinRenkeiInfo);
            }
            else
            {
                // 修正モードの権限なしのアラームを上げる
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
            }
        }

        #endregion

        #region 行NO設定
        /// <summary>
        /// 行NO設定
        /// </summary>
        internal void SetRowNo()
        {
            for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
            {
                // 行NO設定
                this.form.Ichiran[i, ConstClass.CONTROL_ROW_NO].Value = i + 1;

                // 伝票区分
                this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_CD].Value = 2;
                this.form.Ichiran[i, ConstClass.CONTROL_UKEIRE_DENPYOU_KBN_NAME].Value = "支払";
                this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_CD].Value = 1;
                this.form.Ichiran[i, ConstClass.CONTROL_SHUKKA_DENPYOU_KBN_NAME].Value = "売上";
            }
        }
        #endregion

        #region システム設定の確定利用区分と確定単位区分による初期表示

        /// <summary>
        /// システム設定の確定利用区分と確定単位区分による初期表示
        /// </summary>
        private void SetKateiKbnInfo()
        {
            if (this.sysInfoEntity.UR_SH_KAKUTEI_USE_KBN == ConstClass.UR_SH_KAKUTEI_USE_KBN_YES)
            {
                if (this.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == ConstClass.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                {
                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = true;
                    this.form.URIAGE_DATE.Visible = true;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = true;
                    this.form.SHIHARAI_DATE.Visible = true;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = true;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = true;
                }
                else
                {
                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = false;
                    this.form.URIAGE_DATE.Visible = false;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = false;
                    this.form.SHIHARAI_DATE.Visible = false;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = false;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = false;
                }
            }
            else if (this.sysInfoEntity.UR_SH_KAKUTEI_USE_KBN == ConstClass.UR_SH_KAKUTEI_USE_KBN_NO)
            {
                if (this.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == ConstClass.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                {
                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = true;
                    this.form.URIAGE_DATE.Visible = true;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = true;
                    this.form.SHIHARAI_DATE.Visible = true;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = true;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = true;
                }
                else
                {
                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = false;
                    this.form.URIAGE_DATE.Visible = false;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = false;
                    this.form.SHIHARAI_DATE.Visible = false;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = false;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = false;
                }
            }
        }

        /// 確定区分チェック
        /// 入力CDから名称を表示する処理も実施
        /// </summary>
        internal bool CheckKakuteiKbn()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
                {
                    this.form.KAKUTEI_KBN_NAME.Text = string.Empty;
                    return ret;
                }

                short kakuteiKbn = 0;
                short.TryParse(this.form.KAKUTEI_KBN.Text, out kakuteiKbn);

                switch (kakuteiKbn)
                {
                    case ConstClass.KAKUTEI_KBN_KAKUTEI:
                    case ConstClass.KAKUTEI_KBN_MIKAKUTEI:
                        this.form.KAKUTEI_KBN_NAME.Text = ConstClass.GetKakuteiKbnName(kakuteiKbn);
                        break;

                    default:
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058");
                        this.form.KAKUTEI_KBN.Focus();
                        break;

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKakuteiKbn", ex);
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

        #region 出荷量セット
        /// <summary>
        /// 出荷量セット
        /// </summary>
        internal bool SetShukkaRyouInfo()
        {
            try
            {
                // 数量、単価フォーマット
                string systemSuuryouFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT, string.Empty).ToString();
                string systemTankaFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_TANKA_FORMAT, string.Empty).ToString();

                bool bExecuteTotalKingakuCalc = false;
                decimal stackJyuuryou = 0;
                decimal chouseiJyuuryou = 0;
                decimal suuRyou = 0;

                // 明細に対して、出荷量セット処理を行う
                for (int i = 0; i < this.form.Ichiran.Rows.Count - this.newRowNum; i++)
                {
                    Row targetRow = this.form.Ichiran.Rows[i];

                    bool bExecuteJyuuryouCalc = false;
                    bool bExecuteKingakuCalc = false;
                    bool bExecuteTankaCalc = false;

                    // 正味重量
                    stackJyuuryou = 0;
                    if (targetRow.Cells[ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value != null &&
                        decimal.TryParse(targetRow.Cells[ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU].Value.ToString(), out stackJyuuryou) &&
                        (targetRow.Cells[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value == null ||
                         string.IsNullOrEmpty(targetRow.Cells[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value.ToString())))
                    {
                        targetRow.Cells[ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU].Value = CommonCalc.SuuryouAndTankFormat(stackJyuuryou, systemSuuryouFormat);
                        bExecuteJyuuryouCalc = true;
                    }

                    // 調整重量
                    chouseiJyuuryou = 0;
                    if (targetRow.Cells[ConstClass.CONTROL_UKEIRE_CHOUSEI_JYUURYOU].Value != null &&
                        decimal.TryParse(targetRow.Cells[ConstClass.CONTROL_UKEIRE_CHOUSEI_JYUURYOU].Value.ToString(), out chouseiJyuuryou) &&
                        (targetRow.Cells[ConstClass.CONTROL_SHUKKA_CHOUSEI_JYUURYOU].Value == null ||
                         string.IsNullOrEmpty(targetRow.Cells[ConstClass.CONTROL_SHUKKA_CHOUSEI_JYUURYOU].Value.ToString())))
                    {
                        targetRow.Cells[ConstClass.CONTROL_SHUKKA_CHOUSEI_JYUURYOU].Value = CommonCalc.SuuryouAndTankFormat(chouseiJyuuryou, systemSuuryouFormat);
                        bExecuteJyuuryouCalc = true;
                    }

                    // 数量
                    suuRyou = 0;
                    if (targetRow.Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].Value != null &&
                        decimal.TryParse(targetRow.Cells[ConstClass.CONTROL_UKEIRE_SUURYOU].Value.ToString(), out suuRyou) &&
                        (targetRow.Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].Value == null ||
                         string.IsNullOrEmpty(targetRow.Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].Value.ToString())))
                    {
                        targetRow.Cells[ConstClass.CONTROL_SHUKKA_SUURYOU].Value = CommonCalc.SuuryouAndTankFormat(suuRyou, systemSuuryouFormat);
                        bExecuteKingakuCalc = true;
                    }

                    // 単位
                    if (targetRow.Cells[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value != null &&
                        !string.IsNullOrEmpty(targetRow.Cells[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString()) &&
                        (targetRow.Cells[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value == null ||
                         string.IsNullOrEmpty(targetRow.Cells[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value.ToString())))
                    {
                        targetRow.Cells[ConstClass.CONTROL_SHUKKA_UNIT_CD].Value = targetRow.Cells[ConstClass.CONTROL_UKEIRE_UNIT_CD].Value.ToString();

                        if (targetRow.Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value != null)
                        {
                            targetRow.Cells[ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value = targetRow.Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value.ToString();
                        }
                        bExecuteTankaCalc = true;
                    }

                    // 単価設定
                    if (bExecuteTankaCalc)
                    {
                        if (!this.CalcTanka(i, 2))
                        {
                            this.ResetTankaCheck(); // MAILAN #158994 START
                            return false;
                        }
                    }

                    // 重量より数量設定
                    if (bExecuteJyuuryouCalc)
                    {
                        if (!this.CalcDetailNetJyuuryou(i, 2))
                        {
                            return false;
                        }
                    }

                    // 数量より金額設定
                    if (bExecuteKingakuCalc)
                    {
                        if (!this.CalcDetailKingaku(i, 2))
                        {
                            return false;
                        }
                    }
                }
                this.ResetTankaCheck(); // MAILAN #158994 START

                // 総合計設定
                if (bExecuteTotalKingakuCalc)
                {
                    if (!this.CalcAllDetailAndTotal())
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShukkaRyouInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        #endregion

        #region 画面ロード時の単価の表示補正
        private void DispTankaCellInit(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            decimal suryou = 0;
            decimal tanka = 0;
            decimal kingaku = 0;

            foreach (DataRow row in dt.Rows)
            {
                decimal.TryParse(row["SUURYOU"].ToString(), out suryou);
                decimal.TryParse(row["TANKA"].ToString(), out tanka);
                decimal.TryParse(row["KINGAKU"].ToString(), out kingaku);

                suryou = 0;
                tanka = 0;
                kingaku = 0;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 未実装
        /// <summary>
        /// 指定した代納番号のデータが存在するか返す
        /// </summary>
        /// <param name="DainouNumber">受入番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistUkeireData(long DainouNumber)
        {
            LogUtility.DebugMethodStart();

            bool returnVal = false;

            if (0 <= DainouNumber)
            {
                var UREntrys = this.accessor.GetDainouEntry(DainouNumber, 1);
                var SHEntrys = this.accessor.GetDainouEntry(DainouNumber, 2);
                if (UREntrys != null && SHEntrys != null)
                {
                    returnVal = true;
                }
            }
            else if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                returnVal = true;
            }

            LogUtility.DebugMethodEnd();
            return returnVal;
        }
        #endregion

        #region 未実装
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
        #region 請求日付チェック
        /// <summary>
        /// 請求日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool SeikyuuDateCheck()
        {
            //受入→精算。出荷→請求をチェック

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
                List<ReturnDate> returnDate = new List<ReturnDate>();
                List<CheckDate> checkDate = new List<CheckDate>();
                ReturnDate rd = new ReturnDate();
                CheckDate cd = new CheckDate();

                bool bDenpyouKbnCheck = false;

                var denpyouKbnSelect = (from temp in this.form.Ichiran.Rows
                                        where Convert.ToString(temp.Cells["SHUKKA_DENPYOU_KBN_NAME"].Value) == "売上"
                                        select temp).ToArray();

                bDenpyouKbnCheck = denpyouKbnSelect != null && denpyouKbnSelect.Length > 0;

                if (bDenpyouKbnCheck == false)
                {
                    return true;
                }

                //nullチェック
                if (string.IsNullOrEmpty(this.form.URIAGE_DATE.Text))
                {
                    return true;
                }
                if (string.IsNullOrEmpty(this.form.SHUKKA_TORIHIKISAKI_CD.Text))
                {
                    return true;
                }

                string strSeikyuuDate = this.form.URIAGE_DATE.Text;
                DateTime seikyuudate = Convert.ToDateTime(strSeikyuuDate);

                cd.CHECK_DATE = seikyuudate;
                cd.TORIHIKISAKI_CD = this.form.SHUKKA_TORIHIKISAKI_CD.Text;
                cd.KYOTEN_CD = this.headerForm.KYOTEN_CD.Text;
                checkDate.Add(cd);
                returnDate = CheckShimeDate.GetNearShimeDate(checkDate, 1);

                if (returnDate.Count == 0)
                {
                    return true;
                }
                else if (returnDate.Count == 1)
                {
                    //例外日付が含まれる
                    if (returnDate[0].dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                    else
                    {
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
                    if (msgLogic.MessageBoxShow("C085", "請求") == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SeikyuuDateCheck", ex2);
                msgLogic.MessageBoxShow("E093");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeikyuuDateCheck", ex);
                msgLogic.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd();
                return false;
            }
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

            try
            {

                ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
                List<ReturnDate> returnDate = new List<ReturnDate>();
                List<CheckDate> checkDate = new List<CheckDate>();
                ReturnDate rd = new ReturnDate();
                CheckDate cd = new CheckDate();

                bool bDenpyouKbnCheck = false;

                var denpyouKbnSelect = (from temp in this.form.Ichiran.Rows
                                        where Convert.ToString(temp.Cells["UKEIRE_DENPYOU_KBN_NAME"].Value) == "支払"
                                        select temp).ToArray();

                bDenpyouKbnCheck = denpyouKbnSelect != null && denpyouKbnSelect.Length > 0;

                if (bDenpyouKbnCheck == false)
                {
                    return true;
                }

                //nullチェック
                if (string.IsNullOrEmpty(this.form.SHIHARAI_DATE.Text))
                {
                    return true;
                }
                if (string.IsNullOrEmpty(this.form.UKEIRE_TORIHIKISAKI_CD.Text))
                {
                    return true;
                }

                string strShiharaiDate = this.form.SHIHARAI_DATE.Text;
                DateTime shiharaidate = Convert.ToDateTime(strShiharaiDate);

                cd.CHECK_DATE = shiharaidate;
                cd.TORIHIKISAKI_CD = this.form.UKEIRE_TORIHIKISAKI_CD.Text;
                cd.KYOTEN_CD = this.headerForm.KYOTEN_CD.Text;
                checkDate.Add(cd);
                returnDate = CheckShimeDate.GetNearShimeDate(checkDate, 2);

                if (returnDate.Count == 0)
                {
                    return true;
                }
                else if (returnDate.Count == 1)
                {
                    //例外日付が含まれる
                    if (returnDate[0].dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                    else
                    {
                        if (msgLogic.MessageBoxShow("C084", returnDate[0].dtDATE.ToString("yyyy/MM/dd"), "支払") == DialogResult.Yes)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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
                    if (msgLogic.MessageBoxShow("C085", "支払") == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SeisanDateCheck", ex2);
                msgLogic.MessageBoxShow("E093");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeisanDateCheck", ex);
                msgLogic.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        // MAILAN #158994 START
        internal void ResetTankaCheck()
        {
            this.isTankaMessageShown = false;
            this.isContinueCheck = true;
        }
        // MAILAN #158994 END
    }
}
