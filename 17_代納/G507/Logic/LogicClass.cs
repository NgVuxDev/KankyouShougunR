using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.APP;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.Const;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DAO;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic, IDisposable
    {
        #region 内部変数

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// E001フォーマット({0}は必須項目です。入力してください)
        /// </summary>
        private string errorMsgFormat;

        /// <summary>
        /// 初期化済み(true：初期化済み、false：未処理）
        /// </summary>
        private bool isInited;

        private MessageBoxShowLogic errmessage;

        #endregion

        #region 内部変数（DAO）

        /// <summary>
        /// DBアクセス共通クラス
        /// </summary>
        private DBAccessor dbAccesser;

        /// <summary>
        /// 会社情報DAO
        /// </summary>
        private IM_CORP_INFODao corpInfoDao;

        /// <summary>
        /// 代納明細(前回)DAO
        /// </summary>
        private DainoDetailZenkaiDao zenkaiDao;

        /// <summary>
        /// 取引先_請求情報マスタ
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao TorihikisakiSeikyuDao;

        /// <summary>
        /// 取引先_支払情報マスタ
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao TorihikisakiShiharaiDao;

        /// <summary>
        /// 請求伝票
        /// </summary>
        private TSEIKYUUDENPYOUDao SeikyuuDenpyouDao;
        #endregion

        #region 内部変数（DB検索結果）

        /// <summary>
        /// システム設定
        /// </summary>
        private M_SYS_INFO sysInfo;

        /// <summary>
        /// 会社設定
        /// </summary>
        private M_CORP_INFO corpInfo;

        /// <summary>
        /// 代納基本情報
        /// </summary>
        private List<MeiseiDTOClass> EntryList;

        private DisplayDataRow ukeireRow = new DisplayDataRow();

        private DisplayDataRow shukkaRow = new DisplayDataRow();

        /// <summary>
        /// エラー表示用品名CD
        /// </summary>
        internal string ErrHinmeiCD;

        /// <summary>
        /// 端数処理種別
        /// </summary>
        private enum fractionType : int
        {
            CEILING = 1,	// 切り上げ
            FLOOR,		// 切り捨て
            ROUND,		// 四捨五入
        }
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.errmessage = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // DAO初期化
                this.InitDao();

                // ボタンのテキストを初期化
                this.InitButton();

                // イベントの初期化処理
                this.InitEvent();

                // システムデータからラジオデザイナ設定、ラジオ初期値設定
                this.InitSetSystemData();

                // 画面項目制御
                this.BottonControl();

                // エントリーデータからラジオ初期値取得、設定
                this.InitSetEntryData();

                // 明細データから金額データ取得、設定
                this.InitSetDetailData();

                this.form.SaveOldZei();
                //メッセージE
                //明細毎外税
                if (("3".Equals(this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Text) && "1".Equals(this.form.numtxt_UkeireSeikyuZeiKbn.Text))
                    || ("3".Equals(this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text) && "1".Equals(this.form.numtxt_ShukkaSeikyuZeiKbn.Text)))
                {
                    errmessage.MessageBoxShowWarn("税計算区分＝3.明細毎　は、\r適格請求書の要件を満たした請求書になりません。\r税計算区分、税区分の見直しを行ってください。");
                }
                //請求/支払毎
                //メッセージD
                this.SeiMaiSoto_Check();

                // 初期化済みON
                this.isInited = true;
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
        #endregion

        #region Daoの初期化

        /// <summary>
        /// Daoの初期化
        /// </summary>
        private void InitDao()
        {
            LogUtility.DebugMethodStart();

            // 共通データアクセス
            this.dbAccesser = new DBAccessor();

            // DAO初期化
            this.corpInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();
            this.TorihikisakiSeikyuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.TorihikisakiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.zenkaiDao = DaoInitUtility.GetComponent<DainoDetailZenkaiDao>();
            this.SeikyuuDenpyouDao = DaoInitUtility.GetComponent<TSEIKYUUDENPYOUDao>();

            // F9：実行キーで使用する必須エラー定義を取得
            MessageUtility dbMsgUtl = new MessageUtility();
            this.errorMsgFormat = dbMsgUtl.GetMessage("E001").MESSAGE;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void InitButton()
        {
            LogUtility.DebugMethodStart();

            //プロセスボタンを非表示設定
            var businessBaseForm = (BasePopForm)this.form.ParentPopForm;
            businessBaseForm.ProcessButtonPanel.Visible = false;

            // Fキーボタンの表示・非表示化
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, businessBaseForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            businessBaseForm.bt_func1.Enabled = false;
            businessBaseForm.bt_func2.Enabled = false;
            businessBaseForm.bt_func3.Enabled = false;
            businessBaseForm.bt_func4.Enabled = false;
            businessBaseForm.bt_func5.Enabled = false;
            businessBaseForm.bt_func6.Enabled = false;
            businessBaseForm.bt_func7.Enabled = false;
            businessBaseForm.bt_func8.Enabled = false;
            businessBaseForm.bt_func9.Enabled = true;
            businessBaseForm.bt_func10.Enabled = false;
            businessBaseForm.bt_func11.Enabled = false;
            businessBaseForm.bt_func12.Enabled = true;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            ButtonSetting[] res = buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.ButtonInfoXmlPath);

            LogUtility.DebugMethodStart(res);
            return res;
        }

        #endregion

        #region イベントの初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void InitEvent()
        {
            LogUtility.DebugMethodStart();

            // ベースフォーム用のイベント生成
            var parentForm = this.form.ParentPopForm;
            parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);                   //残高取得
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new System.EventHandler(this.bt_func9_Click);            //実行
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる
            
            // 画面表示イベント生成
            parentForm.Shown += new EventHandler(UI_Shown);

            // 税計算区分ラジオ変更イベント
            this.form.numtxt_UkeireSeikyuZeiKeisanKbn.TextChanged += new EventHandler(ZeiKeisanKbn_TextChanged);
            this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.TextChanged += new EventHandler(ZeiKeisanKbn_TextChanged);

            // 税区分ラジオ変更イベント
            this.form.numtxt_UkeireSeikyuZeiKbn.TextChanged += new EventHandler(ZeiKbn_TextChanged);
            this.form.numtxt_ShukkaSeikyuZeiKbn.TextChanged += new EventHandler(ZeiKbn_TextChanged);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベント処理の削除を行う
        /// </summary>
        private void EventDelete()
        {
            LogUtility.DebugMethodStart();

            // ベースフォーム用のイベント生成
            var parentForm = this.form.ParentPopForm;
            parentForm.bt_func9.Click -= new System.EventHandler(this.bt_func9_Click);            //実行
            parentForm.bt_func12.Click -= new System.EventHandler(this.bt_func12_Click);          //閉じる

            // 画面表示イベント生成
            parentForm.Shown -= new EventHandler(UI_Shown);

            // 税計算区分ラジオ変更イベント
            this.form.numtxt_UkeireSeikyuZeiKeisanKbn.TextChanged -= new EventHandler(ZeiKeisanKbn_TextChanged);
            this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.TextChanged -= new EventHandler(ZeiKeisanKbn_TextChanged);

            // 税区分ラジオ変更イベント
            this.form.numtxt_UkeireSeikyuZeiKbn.TextChanged -= new EventHandler(ZeiKbn_TextChanged);
            this.form.numtxt_ShukkaSeikyuZeiKbn.TextChanged -= new EventHandler(ZeiKbn_TextChanged);
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面表示後イベント

        /// <summary>
        /// 画面表示後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UI_Shown(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 初期フォーカス(受入請求-税計算区分)
                this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Focus();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region 画面項目制御
        /// <summary>
        /// 画面項目制御
        /// </summary>
        private void BottonControl()
        {
            bool zaenkaiFlg = true;
            if (this.form.ParameterDTO_IN.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                zaenkaiFlg = true;
            }
            else
            {
                zaenkaiFlg = false;
            }
            // 項目制御
            if (this.sysInfo.DAINO_ZANDAKA_JIDOU_KBN == 1)
            {
                // 残高設定
                this.SetZenkaiZentaka(zaenkaiFlg);
                this.form.ParentPopForm.bt_func1.Enabled = false;
            }
            else
            {
                this.form.ParentPopForm.bt_func1.Enabled = zaenkaiFlg;
            }
        }
        #endregion

        #region システム設定から設定

        /// <summary>
        /// システム設定から設定
        /// </summary>
        private void InitSetSystemData()
        {
            LogUtility.DebugMethodStart();

            // -------------------------------------------
            // DB検索
            // システム設定を取得
            this.sysInfo = this.dbAccesser.GetSysInfo();
            // 会社情報取得
            this.corpInfo = this.GetCorpInfo();

            // ラジオボタン 必須チェックエラー文言設定
            this.InitSetRequiredErrorMessage();
            // -------------------------------------------
            // 税計算区分 ラジオボタン 初期値設定
            this.InitSetZeiKeisanKbn(this.sysInfo);

            // ラジオボタン 初期値設定
            // 税計算区分
            this.InitSetRangeZeiKbnRadio();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// M_CORP_INFOを取得する
        /// </summary>
        /// <returns></returns>
        private M_CORP_INFO GetCorpInfo()
        {
            // TODO: ログイン時に共通メンバでM_CORP_INFOの情報を保持する可能性があるため、
            //       その場合、このメソッドは必要なくなる。
            M_CORP_INFO[] returnEntity = corpInfoDao.GetAllData();
            return returnEntity[0];
        }
        #endregion

        #region 必須入力エラー文言設定

        /// <summary>
        /// ラジオボタン 必須入力エラー文言設定
        /// </summary>
        private void InitSetRequiredErrorMessage()
        {
            LogUtility.DebugMethodStart();

            Collection<SelectCheckDto> dtoCollection;

            // -- 受入請求 ----------------------------------------------- 
            // 受入請求 税計算区分
            dtoCollection = this.form.numtxt_UkeireSeikyuZeiKeisanKbn.RegistCheckMethod;
            this.SetErrorMessage(dtoCollection, ConstClass.DEF_ERROR_MSG_ZEI_KEISAN_KBN);

            // 受入請求 税区分
            dtoCollection = this.form.numtxt_UkeireSeikyuZeiKbn.RegistCheckMethod;
            this.SetErrorMessage(dtoCollection, ConstClass.DEF_ERROR_MSG_ZEI_KBN);

            // -- 出荷請求 ----------------------------------------------- 
            // 出荷請求 税計算区分
            dtoCollection = this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.RegistCheckMethod;
            this.SetErrorMessage(dtoCollection, ConstClass.DEF_ERROR_MSG_ZEI_KEISAN_KBN);

            // 出荷請求 税区分
            dtoCollection = this.form.numtxt_ShukkaSeikyuZeiKbn.RegistCheckMethod;
            this.SetErrorMessage(dtoCollection, ConstClass.DEF_ERROR_MSG_ZEI_KBN);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 必須エラー時の表示メッセージの定義
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="name"></param>
        private void SetErrorMessage(Collection<SelectCheckDto> dtoCollection, string name)
        {
            LogUtility.DebugMethodStart(dtoCollection, name);

            if (dtoCollection.Count > 0)
            {
                SelectCheckDto dto = dtoCollection[0];
                dto.DisplayMessage = string.Format(this.errorMsgFormat, name);
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 代納入力データを取得、ラジオボタン設定

        /// <summary>
        /// 代納番号を用いた検索結果を画面コントロールへ設定
        /// </summary>
        private void InitSetEntryData()
        {
            LogUtility.DebugMethodStart();

            // ラジオボタン 初期値設定
            // 税区分 ラジオボタン 初期値設定
            this.InitSetZeiKbn(this.form.ParameterDTO_IN);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ラジオボタン設定
        /// <summary>
        /// 税計算区分設定
        /// </summary>
        /// <param name="sysInfo"></param>
        private void InitSetZeiKeisanKbn(M_SYS_INFO sysInfo)
        {
            LogUtility.DebugMethodStart(sysInfo);

            int zeiKeisanKbn = 1;
            // 税計算区分を取得
            if (!string.IsNullOrEmpty(this.form.ParameterDTO_IN.Ukeire_Zeikeisan_Kbn))
            {
                zeiKeisanKbn = int.Parse(this.form.ParameterDTO_IN.Ukeire_Zeikeisan_Kbn);
            }

            this.SetZeiKeisanKbn(sysInfo, this.form.numtxt_UkeireSeikyuZeiKeisanKbn
                                                    , this.form.rdb_UkeireSeikyuZeiKeisanKbnDenpyo
                                                    , this.form.rdb_UkeireSeikyuZeiKeisanKbnSeikyu
                                                    , this.form.rdb_UkeireSeikyuZeiKeisanKbnMeisai
                                                    , zeiKeisanKbn);

            // 取引先請求情報取得
            M_TORIHIKISAKI_SEIKYUU mtorihikisakiseikyuu = (M_TORIHIKISAKI_SEIKYUU)TorihikisakiSeikyuDao.GetDataByCd(this.form.ParameterDTO_IN.Shukka_Torihikisaki_Cd);

            zeiKeisanKbn = 1;

            // 税計算区分を取得
            if (!string.IsNullOrEmpty(this.form.ParameterDTO_IN.Shukka_Zeikeisan_Kbn))
            {
                zeiKeisanKbn = int.Parse(this.form.ParameterDTO_IN.Shukka_Zeikeisan_Kbn);
            }

            this.SetZeiKeisanKbn(sysInfo, this.form.numtxt_ShukkaSeikyuZeiKeisanKbn
                                                    , this.form.rdb_ShukkaSeikyuZeiKeisanKbnDenpyo
                                                    , this.form.rdb_ShukkaSeikyuZeiKeisanKbnSeikyu
                                                    , this.form.rdb_ShukkaSeikyuZeiKeisanKbnMeisai
                                                    , zeiKeisanKbn);

            LogUtility.DebugMethodEnd(sysInfo);
        }

        /// <summary>
        /// 税計算区分ラジオ設定
        /// </summary>
        /// <param name="zeiKeisanKbn"></param>
        /// <param name="selectNumTxt"></param>
        /// <param name="denpyoRadio"></param>
        /// <param name="seikyuRadio"></param>
        /// <param name="meisaiRadio"></param>
        private void SetZeiKeisanKbn(M_SYS_INFO sysInfo
                                                , CustomNumericTextBox2 selectNumTxt
                                                , CustomRadioButton denpyoRadio
                                                , CustomRadioButton seikyuRadio
                                                , CustomRadioButton meisaiRadio
                                                , int keisanKbn)
        {

            LogUtility.DebugMethodStart(sysInfo, selectNumTxt, denpyoRadio, seikyuRadio, meisaiRadio, keisanKbn);

            ConstClass.ZeiKeisanKbn zeiKeisanKbn = ConstClass.ZeiKeisanKbn.None;

            zeiKeisanKbn = (ConstClass.ZeiKeisanKbn)keisanKbn;

            //// 税計算を取得
            //if (!sysInfo.SYS_ZEI_KEISAN_KBN_USE_KBN.IsNull)
            //{
            //    zeiKeisanKbn = (ConstClass.ZeiKeisanKbn)sysInfo.SYS_ZEI_KEISAN_KBN_USE_KBN.Value;
            //}
            //else
            //{
            //    zeiKeisanKbn = ConstClass.ZeiKeisanKbn.Denpyo;
            //}

            // 1.伝票
            if (ConstClass.ZeiKeisanKbn.Denpyo == zeiKeisanKbn)
            {
                denpyoRadio.Checked = true;
                seikyuRadio.Checked = false;
                meisaiRadio.Checked = false;

            }
            // 2.請求
            else if (ConstClass.ZeiKeisanKbn.Seikyu == zeiKeisanKbn)
            {
                denpyoRadio.Checked = false;
                seikyuRadio.Checked = true;
                meisaiRadio.Checked = false;
            }
            // 3.明細
            else if (ConstClass.ZeiKeisanKbn.Meisai == zeiKeisanKbn)
            {
                denpyoRadio.Checked = false;
                seikyuRadio.Checked = false;
                meisaiRadio.Checked = true;
            }
            // その他
            else
            {
                denpyoRadio.Checked = false;
                seikyuRadio.Checked = false;
                meisaiRadio.Checked = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 税区分 選択可能範囲 設定
        /// </summary>
        /// <param name="sysInfo"></param>
        private void InitSetRangeZeiKbnRadio()
        {
            LogUtility.DebugMethodStart(sysInfo);

            int ZeiKeisanKbn = 0;
            if (!string.IsNullOrEmpty(this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Text))
            {
                ZeiKeisanKbn = int.Parse(this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Text);
            }
            // 受入支払取引
            this.SetRangeZeiKbnRadio(this.form.numtxt_UkeireSeikyuZeiKbn
                                      , this.form.rdb_UkeireSeikyuZeiKbnSotozei
                                      , this.form.rdb_UkeireSeikyuZeiKbnUtizei
                                      , this.form.rdb_UkeireSeikyuZeiKbnHikazei
                                                    , ZeiKeisanKbn);

            ZeiKeisanKbn = 0;
            if (!string.IsNullOrEmpty(this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text))
            {
                ZeiKeisanKbn = int.Parse(this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text);
            }
            // 出荷請求
            this.SetRangeZeiKbnRadio(this.form.numtxt_ShukkaSeikyuZeiKbn
                                      , this.form.rdb_ShukkaSeikyuZeiKbnSotozei
                                      , this.form.rdb_ShukkaSeikyuZeiKbnUtizei
                                      , this.form.rdb_ShukkaSeikyuZeiKbnHikazei
                                                    , ZeiKeisanKbn);

            LogUtility.DebugMethodEnd(sysInfo);
        }

        /// <summary>
        /// 税区分ラジオ 選択可能範囲設定
        /// </summary>
        /// <param name="selectNumTxt"></param>
        /// <param name="soto"></param>
        /// <param name="uchi"></param>
        /// <param name="hikazei"></param>
        /// <param name="zeiKbn"></param>
        private void SetRangeZeiKbnRadio(CustomNumericTextBox2 selectNumTxt,
                                         CustomRadioButton soto,
                                         CustomRadioButton uchi,
                                         CustomRadioButton hikazei,
                                         int ZeiKeisanKbn)
        {
            LogUtility.DebugMethodStart(selectNumTxt, soto, uchi, hikazei, ZeiKeisanKbn);

            // システム税計算区分利用形態を取得
            ConstClass.ZeiKeisanKbn useKbn = ConstClass.ZeiKeisanKbn.None;

            useKbn = (ConstClass.ZeiKeisanKbn)ZeiKeisanKbn;

            // 「1.伝票毎」または「2.請求毎」の場合
            if (ConstClass.ZeiKeisanKbn.Denpyo == useKbn || ConstClass.ZeiKeisanKbn.Seikyu == useKbn)
            {
                if (uchi.Checked)
                {
                    soto.Checked = true;
                }
                uchi.Enabled = false;
                selectNumTxt.Tag = ConstClass.TOOL_TIP_TXT_4;
                //selectNumTxt.CharacterLimitList = new Char[] { '1', '3' };
            }
            else
            {
                uchi.Enabled = true;
                selectNumTxt.Tag = ConstClass.TOOL_TIP_TXT_3;
                //selectNumTxt.CharacterLimitList = new Char[] { '1', '2', '3' };
            }

            LogUtility.DebugMethodEnd(selectNumTxt, soto, uchi, hikazei, ZeiKeisanKbn);
        }

        /// <summary>
        /// 税区分設定
        /// </summary>
        /// <param name="ParameterDTO"></param>
        private void InitSetZeiKbn(ParameterDTOClass ParameterDTO)
        {
            LogUtility.DebugMethodStart(ParameterDTO);

            int zeiKbn = 1;
            // 受入支払取引
            if (!string.IsNullOrEmpty(ParameterDTO.Ukeire_Zei_Kbn))
            {
                zeiKbn = int.Parse(ParameterDTO.Ukeire_Zei_Kbn);
            }

            this.SetZeiKbn(this.form.rdb_UkeireSeikyuZeiKbnSotozei,
                           this.form.rdb_UkeireSeikyuZeiKbnUtizei,
                           this.form.rdb_UkeireSeikyuZeiKbnHikazei,
                           zeiKbn);

            // 出荷売上取引
            zeiKbn = 1;
            if (!string.IsNullOrEmpty(ParameterDTO.Shukka_Zei_Kbn))
            {
                zeiKbn = int.Parse(ParameterDTO.Shukka_Zei_Kbn);
            }
            this.SetZeiKbn(this.form.rdb_ShukkaSeikyuZeiKbnSotozei,
                           this.form.rdb_ShukkaSeikyuZeiKbnUtizei,
                           this.form.rdb_ShukkaSeikyuZeiKbnHikazei,
                           zeiKbn);

            LogUtility.DebugMethodEnd(ParameterDTO);
        }

        /// <summary>
        /// 税区分ラジオ設定
        /// </summary>
        /// <param name="soto">外税</param>
        /// <param name="uchi">内税</param>
        /// <param name="hikazei">非課税</param>
        /// <param name="zeiKbn">税区分</param>
        private void SetZeiKbn(CustomRadioButton soto,
                                CustomRadioButton uchi,
                                CustomRadioButton hikazei,
                                int zeiKbn)
        {
            LogUtility.DebugMethodStart(soto, uchi, hikazei, zeiKbn);

            ConstClass.ZeiKbnCd zeikbnCd = (ConstClass.ZeiKbnCd)zeiKbn;

            // 外税の場合
            if (ConstClass.ZeiKbnCd.SotoZei.Equals(zeikbnCd))
            {
                soto.Checked = true;
                uchi.Checked = false;
                hikazei.Checked = false;
            }

            // 内税の場合
            else if (ConstClass.ZeiKbnCd.UchiZei.Equals(zeikbnCd))
            {
                soto.Checked = false;
                uchi.Checked = true;
                hikazei.Checked = false;
            }

            // 非課税の場合
            else if (ConstClass.ZeiKbnCd.HikaZei.Equals(zeikbnCd))
            {
                soto.Checked = false;
                uchi.Checked = false;
                hikazei.Checked = true;
            }

            // その他
            else
            {
                soto.Checked = false;
                uchi.Checked = false;
                hikazei.Checked = false;
            }

            LogUtility.DebugMethodEnd(soto, uchi, hikazei, zeiKbn);
        }

        #endregion

        #region 代納明細データを取得、金額テキスト設定
        
        /// <summary>
        /// 金額設定（受入支払分・出荷売上分）
        /// </summary>
        private void InitSetDetailData()
        {
            LogUtility.DebugMethodStart();

            this.ukeireRow = new DisplayDataRow();
            this.shukkaRow = new DisplayDataRow();

            // 前回残高設定
            decimal ukeireZandaka = 0;
            decimal.TryParse(this.form.numtxt_KingakuUkeireShiharaiPrivZan.Text, out ukeireZandaka);
            this.ukeireRow.ZenkaiZandaka = ukeireZandaka;
            decimal shukkaZandaka = 0;
            decimal.TryParse(this.form.numtxt_KingakuShukkaShiharaiPrivZan.Text, out shukkaZandaka);
            this.shukkaRow.ZenkaiZandaka = shukkaZandaka;

            // 受入支払
            this.SetKingakuUkeireShiharai();

            // 出荷支払
            this.SetKingakuShukkaShiharai();

            // 合計値
            this.SetTotalKingaku();

            LogUtility.DebugMethodEnd();
        }

        #endregion


        #region 金額値をテキストフィールドへ設定
        /// <summary>
        /// 受入支払金額設定
        /// </summary>
        private void SetKingakuUkeireShiharai()
        {
            LogUtility.DebugMethodStart();

            this.GetUkeireDisplayData();

            // 金額設定先
            CustomNumericTextBox2 priv = this.form.numtxt_KingakuUkeireShiharaiPrivZan;
            CustomNumericTextBox2 nowKin = this.form.numtxt_KingakuUkeireShiharaiNowKin;
            CustomNumericTextBox2 nowZei = this.form.numtxt_KingakuUkeireShiharaiNowZei;
            CustomNumericTextBox2 nowTori = this.form.numtxt_KingakuUkeireShiharaiNowTori;
            CustomNumericTextBox2 nowZan = this.form.numtxt_KingakuUkeireShiharaiZandaka;

            // コントロールへ反映（前回残高・今回金額・今回税額・今回取引・差引残高）
            this.SetKingakuTextField(this.ukeireRow,
                                     priv,
                                     nowKin,
                                     nowZei,
                                     nowTori,
                                     nowZan);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷支払金額設定
        /// </summary>
        private void SetKingakuShukkaShiharai()
        {
            LogUtility.DebugMethodStart();

            this.GetShukkaDisplayData();

            // 金額設定先
            CustomNumericTextBox2 priv = this.form.numtxt_KingakuShukkaShiharaiPrivZan;
            CustomNumericTextBox2 nowKin = this.form.numtxt_KingakuShukkaShiharaiNowKin;
            CustomNumericTextBox2 nowZei = this.form.numtxt_KingakuShukkaShiharaiNowZei;
            CustomNumericTextBox2 nowTori = this.form.numtxt_KingakuShukkaShiharaiNowTori;
            CustomNumericTextBox2 nowZan = this.form.numtxt_KingakuShukkaShiharaiZandaka;

            // コントロールへ反映（前回残高・今回金額・今回税額・今回取引・差引残高）
            this.SetKingakuTextField(this.shukkaRow,
                                        priv,
                                        nowKin,
                                        nowZei,
                                        nowTori,
                                        nowZan);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 指定フィールドに金額設定
        /// </summary>
        /// <param name="record">データ</param>
        /// <param name="priv">前回残高</param>
        /// <param name="nowKin">今回金額</param>
        /// <param name="nowZei">今回税額</param>
        /// <param name="nowTori">今回取引</param>
        /// <param name="nowZan">差引残高</param>
        private void SetKingakuTextField(DisplayDataRow record
                                        , CustomNumericTextBox2 priv
                                        , CustomNumericTextBox2 nowKin
                                        , CustomNumericTextBox2 nowZei
                                        , CustomNumericTextBox2 nowTori
                                        , CustomNumericTextBox2 nowZan)
        {
            LogUtility.DebugMethodStart(record, priv, nowKin, nowZei, nowTori, nowZan);

            // 前回残高
            priv.Text = record.ZenkaiZandaka.ToString(ConstClass.DEF_TOSTRING_FORMAT_COMMA_VALIE);

            // 今回金額
            nowKin.Text = record.KonkaiKingaku.ToString(ConstClass.DEF_TOSTRING_FORMAT_COMMA_VALIE);

            // 今回税額
            nowZei.Text = record.KonkaiZeigaku.ToString(ConstClass.DEF_TOSTRING_FORMAT_COMMA_VALIE);

            // 今回取引
            nowTori.Text = record.KonkaiTorihiki.ToString(ConstClass.DEF_TOSTRING_FORMAT_COMMA_VALIE);

            // 差引残高
            nowZan.Text = record.SasihikiZandaka.ToString(ConstClass.DEF_TOSTRING_FORMAT_COMMA_VALIE);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 合計フィールドに設定
        /// </summary>
        private void SetTotalKingaku()
        {
            LogUtility.DebugMethodStart();

            CustomNumericTextBox2 con;
            decimal value = 0;

            // 今回金額
            con = this.form.numtxt_KingakuTotalNowKin;


            if (this.sysInfo.DAINO_CALC_BASE_KBN == 1)
            {
                value = shukkaRow.KonkaiKingaku - ukeireRow.KonkaiKingaku;
            }
            else
            {
                value = ukeireRow.KonkaiKingaku - shukkaRow.KonkaiKingaku;
            }
            con.Text = value.ToString(ConstClass.DEF_TOSTRING_FORMAT_COMMA_VALIE);
            ConstClass.SetForeColor(con, value);

            // 今回税額
            con = this.form.numtxt_KingakuTotalNowZei;
            if (this.sysInfo.DAINO_CALC_BASE_KBN == 1)
            {
                value = shukkaRow.KonkaiZeigaku - ukeireRow.KonkaiZeigaku;
            }
            else
            {
                value = ukeireRow.KonkaiZeigaku - shukkaRow.KonkaiZeigaku;
            }
            con.Text = value.ToString(ConstClass.DEF_TOSTRING_FORMAT_COMMA_VALIE);
            ConstClass.SetForeColor(con, value);

            // 今回取引
            con = this.form.numtxt_KingakuTotalNowTori;
            if (this.sysInfo.DAINO_CALC_BASE_KBN == 1)
            {
                value = shukkaRow.KonkaiTorihiki - ukeireRow.KonkaiTorihiki;
            }
            else
            {
                value = ukeireRow.KonkaiTorihiki - shukkaRow.KonkaiTorihiki;
            }
            con.Text = value.ToString(ConstClass.DEF_TOSTRING_FORMAT_COMMA_VALIE);
            ConstClass.SetForeColor(con, value);

            // 差引残高
            con = this.form.numtxt_KingakuTotalZandaka;
            if (this.sysInfo.DAINO_CALC_BASE_KBN == 1)
            {
                if (this.sysInfo.DAINO_CALC_BASE_KBN == 1)
                {
                    value = shukkaRow.SasihikiZandaka - ukeireRow.SasihikiZandaka;
                }
                else
                {
                    value = ukeireRow.SasihikiZandaka - shukkaRow.SasihikiZandaka;
                }
            }
            con.Text = value.ToString(ConstClass.DEF_TOSTRING_FORMAT_COMMA_VALIE);
            ConstClass.SetForeColor(con, value);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ラジオ変更イベント

        /// <summary>
        /// 税区分ラジオ変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ZeiKbn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 初期化済みを確認
                if (this.isInited)
                {
                    CustomNumericTextBox2 numBox = sender as CustomNumericTextBox2;

                    //適格請求書用チェック
                    //税計算区分=3 : 明細毎
                    //税区分 = 1:外税
                    //メッセージA
                    if (this.isInited)
                    {
                        string FLG = string.Empty;
                        if (this.form.numtxt_UkeireSeikyuZeiKbn.Equals(numBox))
                        {
                            if (("3".Equals(this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Text)) && ("1".Equals(this.form.numtxt_UkeireSeikyuZeiKbn.Text)))
                            {
                                FLG = "支払明細書";
                            }
                        }
                        else
                        {
                            if (("3".Equals(this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text)) && ("1".Equals(this.form.numtxt_ShukkaSeikyuZeiKbn.Text)))
                            {
                                FLG = "請求書";
                            }
                        }

                        if (FLG.Length >= 1)
                        {
                            DialogResult result = 0;

                            result = MessageBox.Show(string.Format("税計算区分＝3.明細毎 は、\r適格請求書の要件を満たした{0}になりませんがよろしいでしょうか？", FLG), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.No)
                            {
                                //税計算区分、税区分を元に戻す
                                if (this.form.numtxt_UkeireSeikyuZeiKbn.Equals(numBox))
                                {
                                    this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Text = this.form.tempShiZeikeisanKBN;
                                    this.form.numtxt_UkeireSeikyuZeiKbn.Text = this.form.tempShiZeiKBN;
                                }
                                else
                                {
                                    this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text = this.form.tempSeiZeikeisanKBN;
                                    this.form.numtxt_ShukkaSeikyuZeiKbn.Text = this.form.tempSeiZeiKBN;
                                }
                                return;
                            }
                        }
                    }

                    //税計算区分、税区分の保持
                    this.form.SaveOldZei();
                    this.ErrHinmeiCD = string.Empty;  //エラー品名用

                    // 受入請求・受入支払の場合
                    if (this.form.numtxt_UkeireSeikyuZeiKbn.Equals(numBox))
                    {
                        // 金額データ設定
                        this.SetKingakuUkeireShiharai();
                        this.SetTotalKingaku();
                    }

                    // 出荷請求・出荷支払の場合
                    else if (this.form.numtxt_ShukkaSeikyuZeiKbn.Equals(numBox))
                    {
                        // 金額データ設定
                        this.SetKingakuShukkaShiharai();
                        this.SetTotalKingaku();
                    }

                    //メッセージD
                    if (this.isInited)
                    {
                        SeiMaiSoto_Check();
                    }

                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 税計算区分変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ZeiKeisanKbn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 初期化済みを確認
                if (this.isInited)
                {
                    CustomNumericTextBox2 numBox = sender as CustomNumericTextBox2;

                    //適格請求書用チェック
                    //税計算区分=3 : 明細毎
                    //税区分 = 1:外税
                    //メッセージA
                    if (this.isInited)
                    {
                        string FLG = string.Empty;
                        if (this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Equals(numBox))
                        {
                            if (("3".Equals(this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Text)) && ("1".Equals(this.form.numtxt_UkeireSeikyuZeiKbn.Text)))
                            {
                                FLG = "支払明細書";
                            }
                        }
                        else
                        {
                            if (("3".Equals(this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text)) && ("1".Equals(this.form.numtxt_ShukkaSeikyuZeiKbn.Text)))
                            {
                                FLG = "請求書";
                            }
                        }

                        if (FLG.Length >= 1)
                        {
                            DialogResult result = 0;

                            result = MessageBox.Show(string.Format("税計算区分＝3.明細毎 は、\r適格請求書の要件を満たした{0}になりませんがよろしいでしょうか？",FLG), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.No)
                            {
                                //税計算区分、税区分を元に戻す
                                if (this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Equals(numBox))
                                {
                                    this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Text = this.form.tempShiZeikeisanKBN;
                                    this.form.numtxt_UkeireSeikyuZeiKbn.Text = this.form.tempShiZeiKBN;
                                }
                                else
                                {
                                    this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text = this.form.tempSeiZeikeisanKBN;
                                    this.form.numtxt_ShukkaSeikyuZeiKbn.Text = this.form.tempSeiZeiKBN;
                                }
                                return;
                            }
                        }
                    }
                    //チェック終わってから
                    //税計算区分、税区分の保持
                    this.form.SaveOldZei();
                    this.ErrHinmeiCD = string.Empty;  //エラー品名用

                    // 税計算区分の場合
                    if (this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Equals(numBox) ||
                        this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Equals(numBox))
                    {
                        // 金額データ再取得、設定
                        this.InitSetDetailData();
                    }

                    int ZeiKeisanKbn = 0;

                    if (!string.IsNullOrEmpty(numBox.Text))
                    {
                        ZeiKeisanKbn = int.Parse(numBox.Text);
                    }

                    if (this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Equals(numBox))
                    {
                        this.SetRangeZeiKbnRadio(this.form.numtxt_UkeireSeikyuZeiKbn,
                                       this.form.rdb_UkeireSeikyuZeiKbnSotozei,
                                       this.form.rdb_UkeireSeikyuZeiKbnUtizei,
                                       this.form.rdb_UkeireSeikyuZeiKbnHikazei,
                                       ZeiKeisanKbn);
                    }
                    else
                    {
                        this.SetRangeZeiKbnRadio(this.form.numtxt_ShukkaSeikyuZeiKbn,
                                                 this.form.rdb_ShukkaSeikyuZeiKbnSotozei,
                                                 this.form.rdb_ShukkaSeikyuZeiKbnUtizei,
                                                 this.form.rdb_ShukkaSeikyuZeiKbnHikazei,
                                                 ZeiKeisanKbn);
                    }

                    //メッセージD
                    if (this.isInited)
                    {
                        SeiMaiSoto_Check();
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region [F1キー] 押下イベント

        /// <summary>
        /// F1 残高取得
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 残高設定
                this.SetZenkaiZentaka(true);

                // 明細データから金額データ取得、設定
                this.InitSetDetailData();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion


        #region [F9キー] 押下イベント

        /// <summary>
        /// F9 実行
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 入力チェックOK
                if (!this.form.RegistErrorFlag)
                {
                    //実施
                    this.Jikou();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 実行処理
        /// </summary>
        public void Jikou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.ParameterDTO = new ParameterDTOClass();

                this.form.ParameterDTO.Shukka_Zeikeisan_Kbn = this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text;
                this.form.ParameterDTO.Shukka_Zei_Kbn = this.form.numtxt_ShukkaSeikyuZeiKbn.Text;
                this.form.ParameterDTO.Ukeire_Zeikeisan_Kbn = this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Text;
                this.form.ParameterDTO.Ukeire_Zei_Kbn = this.form.numtxt_UkeireSeikyuZeiKbn.Text;

                //ダイアログClose処理
                this.form.Close();
                this.form.ParentPopForm.DialogResult = DialogResult.OK;
                this.form.ParentPopForm.Close();
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

        #region [F12キー] 押下イベント

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentBaseForm = this.form.ParentPopForm;
                this.form.Close();
                parentBaseForm.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        /// <summary>
        /// [画面IF]
        /// 指定した表示タイプの金額データを返却
        /// UIラジオ設定に応じた金額を返却
        /// </summary>
        private void GetUkeireDisplayData()
        {
            LogUtility.DebugMethodStart();

            List<MeiseiDTOClass> meisai = new List<MeiseiDTOClass>();
            meisai = this.form.ParameterDTO_IN.Ukeire_Out_Tenpyo_Cnt;
            DateTime ukeireDate;
            int ZeiKeisanKbn = 0;
            int zeiKbn = 0;

            CustomNumericTextBox2 numBox = this.form.numtxt_UkeireSeikyuZeiKeisanKbn;

            if (!string.IsNullOrEmpty(numBox.Text))
            {
                ZeiKeisanKbn = int.Parse(numBox.Text);
            }

            numBox = this.form.numtxt_UkeireSeikyuZeiKbn;

            if (!string.IsNullOrEmpty(numBox.Text))
            {
                zeiKbn = int.Parse(numBox.Text);
            }

            ConstClass.ZeiKbnCd zeikbnCd = (ConstClass.ZeiKbnCd)zeiKbn;
            ConstClass.ZeiKeisanKbn zeiKeisanKbnCd = (ConstClass.ZeiKeisanKbn)ZeiKeisanKbn;

            if (meisai != null)
            {
                // 取引先支払情報取得
                M_TORIHIKISAKI_SHIHARAI mtorihikisakishiharai = (M_TORIHIKISAKI_SHIHARAI)TorihikisakiShiharaiDao.GetDataByCd(this.form.ParameterDTO_IN.Ukeire_Torihikisaki_Cd);

                int hasuu = 0;
                // 端数処理
                if (mtorihikisakishiharai != null)
                {
                    if (mtorihikisakishiharai.TAX_HASUU_CD.IsNull == false)
                    {
                        hasuu = int.Parse(mtorihikisakishiharai.TAX_HASUU_CD.ToString());
                    }
                }

                if (DateTime.TryParse(this.form.ParameterDTO_IN.Ukeire_Date, out ukeireDate))
                {
                    // 当日の消費税率を取得
                    decimal rate = 0;
                    if (!decimal.TryParse(this.form.ParameterDTO_IN.Shiharai_Shouhizei_Rate, out rate))
                    {
                        decimal.TryParse(this.zenkaiDao.GetSyohizei(ukeireDate.ToShortDateString()), out rate);
                    }

                    this.GetKingaku(meisai, zeikbnCd, zeiKeisanKbnCd, this.ukeireRow, hasuu, rate);
                }
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 指定した表示タイプの金額データを返却
        /// </summary>
        /// <returns></returns>
        private void GetShukkaDisplayData()
        {
            LogUtility.DebugMethodStart();
            List<MeiseiDTOClass> meisai = new List<MeiseiDTOClass>();
            meisai = this.form.ParameterDTO_IN.Shukka_Out_Tenpyo_Cnt;
            DateTime shukkaDate;
            int ZeiKeisanKbn = 0;
            int zeiKbn = 0;

            CustomNumericTextBox2 numBox = this.form.numtxt_ShukkaSeikyuZeiKeisanKbn;

            if (!string.IsNullOrEmpty(numBox.Text))
            {
                ZeiKeisanKbn = int.Parse(numBox.Text);
            }

            numBox = this.form.numtxt_ShukkaSeikyuZeiKbn;

            if (!string.IsNullOrEmpty(numBox.Text))
            {
                zeiKbn = int.Parse(numBox.Text);
            }

            ConstClass.ZeiKbnCd zeikbnCd = (ConstClass.ZeiKbnCd)zeiKbn;
            ConstClass.ZeiKeisanKbn zeiKeisanKbnCd = (ConstClass.ZeiKeisanKbn)ZeiKeisanKbn;

            if (meisai != null)
            {

                // 取引先請求情報取得
                M_TORIHIKISAKI_SEIKYUU mtorihikisakiseikyuu = (M_TORIHIKISAKI_SEIKYUU)TorihikisakiSeikyuDao.GetDataByCd(this.form.ParameterDTO_IN.Shukka_Torihikisaki_Cd);

                int hasuu = 0;
                // 端数処理
                if (mtorihikisakiseikyuu != null)
                {
                    if (mtorihikisakiseikyuu.TAX_HASUU_CD.IsNull == false)
                    {
                        hasuu = int.Parse(mtorihikisakiseikyuu.TAX_HASUU_CD.ToString());
                    }
                }

                if (DateTime.TryParse(this.form.ParameterDTO_IN.Shukka_Date, out shukkaDate))
                {
                    // 当日の消費税率を取得
                    decimal rate = 0;
                    if (!decimal.TryParse(this.form.ParameterDTO_IN.Uriage_Shouhizei_Rate, out rate))
                    {
                        decimal.TryParse(this.zenkaiDao.GetSyohizei(shukkaDate.ToShortDateString()), out rate);
                    }

                    this.GetKingaku(meisai, zeikbnCd, zeiKeisanKbnCd, this.shukkaRow, hasuu, rate);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 各金額取得
        /// </summary>
        /// <param name="meisai">明細</param>
        /// <param name="zeikbnCd">税区分</param>
        /// <param name="zeiKeisanKbnCd">税計算区分</param>
        /// <param name="dataRow">データ</param>
        /// <param name="hasuu">端数</param>
        /// <param name="rate">消費税率</param>
        private void GetKingaku(List<MeiseiDTOClass> meisai, ConstClass.ZeiKbnCd zeikbnCd, ConstClass.ZeiKeisanKbn zeiKeisanKbnCd, DisplayDataRow dataRow, int hasuu, decimal rate)
        {
            // 各金額を積算
            decimal uriKin = 0;
            decimal uriKinhinmei = 0;
            decimal uriKinHikazei = 0;
            decimal hinmeiZei = 0;
            decimal meiZei = 0;
            decimal seiKin = 0;
            decimal uriKinMeisai = 0;

            //[税計算区分]が[1.伝票毎]の場合
            if (ConstClass.ZeiKeisanKbn.Denpyo == (ConstClass.ZeiKeisanKbn)zeiKeisanKbnCd)
            {
                foreach (MeiseiDTOClass row in meisai)
                {
                    // 売上金額を積算
                    if (!string.IsNullOrEmpty(row.Kingaku.ToString()))
                    {
                        if (string.IsNullOrEmpty(row.Hinmei_Zei_Kbn_Cd.ToString()))
                        {
                            if (zeikbnCd == ConstClass.ZeiKbnCd.SotoZei)
                            {
                                //品名税無、外税
                                uriKin = uriKin + StringToInt(row.Kingaku);
                            }
                            else if (zeikbnCd == ConstClass.ZeiKbnCd.HikaZei)
                            {
                                //品名税無、非課税
                                uriKinHikazei = uriKinHikazei + StringToInt(row.Kingaku);
                            }
                        }
                        else
                        {
                            ConstClass.ZeiKbnCd tmpZeiKbn = (ConstClass.ZeiKbnCd)int.Parse(row.Hinmei_Zei_Kbn_Cd);
                            if (tmpZeiKbn == ConstClass.ZeiKbnCd.HikaZei)
                            {
                                //品名非課税
                                uriKinHikazei = uriKinHikazei + StringToInt(row.Kingaku);
                            }
                            else
                            {
                                //品名外税 or 品名内税
                                uriKin = uriKin + (StringToInt(row.Kingaku) - StringToInt(row.Hinmei_Tax_Uchi));
                            }
                        }
                    }
                }
                seiKin = (uriKin) * rate;

                seiKin = FractionCalc(seiKin, hasuu);

                //退避
                if (ConstClass.ZeiKbnCd.HikaZei == zeikbnCd)
                {
                    hinmeiZei = seiKin;
                    seiKin = 0;
                }
            }
            //[税計算区分]が[2.請求毎]の場合
            else if (ConstClass.ZeiKeisanKbn.Seikyu == (ConstClass.ZeiKeisanKbn)zeiKeisanKbnCd)
            {
                foreach (MeiseiDTOClass row in meisai)
                {
                    // 売上金額を積算
                    if (!string.IsNullOrEmpty(row.Kingaku.ToString()))
                    {
                        if (string.IsNullOrEmpty(row.Hinmei_Zei_Kbn_Cd.ToString()))
                        {
                            uriKin += StringToInt(row.Kingaku);
                        }
                        else
                        {
                            uriKinhinmei += (StringToInt(row.Kingaku) - StringToInt(row.Hinmei_Tax_Uchi));
                        }
                    }
                }
            }
            //[税計算区分]が[3.明細毎]の場合
            else
            {
                foreach (MeiseiDTOClass row in meisai)
                {
                    // 売上金額を積算
                    if (!string.IsNullOrEmpty(row.Kingaku.ToString()))
                    {
                        // 品名税区分無し
                        if (string.IsNullOrEmpty(row.Hinmei_Zei_Kbn_Cd.ToString()))
                        {
                            if (ConstClass.ZeiKbnCd.UchiZei == zeikbnCd)
                            {
                                //内税
                                uriKinMeisai += StringToInt(row.Kingaku) - FractionCalc((StringToInt(row.Kingaku) * rate) / (1 + rate), hasuu);
                            }
                            else
                            {
                                uriKin += StringToInt(row.Kingaku);
                                if (ConstClass.ZeiKbnCd.SotoZei == zeikbnCd)
                                {
                                    //外税
                                    meiZei = StringToInt(row.Kingaku) * rate;
                                    hinmeiZei += FractionCalc(meiZei, hasuu);
                                }
                            }
                        }
                        else
                        {
                            ConstClass.ZeiKbnCd tmpZeiKbn = (ConstClass.ZeiKbnCd)int.Parse(row.Hinmei_Zei_Kbn_Cd);
                            if (tmpZeiKbn == ConstClass.ZeiKbnCd.UchiZei)
                            {
                                //品名内税
                                uriKinMeisai += (StringToInt(row.Kingaku) - StringToInt(row.Hinmei_Tax_Uchi));
                            }
                            else
                            {
                                //品名外税 or 品名非課税
                                int hinZei = StringToInt(row.Hinmei_Tax_Soto) + StringToInt(row.Hinmei_Tax_Uchi);
                                hinmeiZei += hinZei;
                                uriKinhinmei += (StringToInt(row.Kingaku) - StringToInt(row.Hinmei_Tax_Uchi));
                            }
                        }
                    }
                }

                //明細毎内税の算出
                seiKin = (uriKinMeisai) * rate;
                hinmeiZei += FractionCalc(seiKin, hasuu);
                seiKin = 0;

                //明細毎内税（税抜金額）を加算
                uriKin += uriKinMeisai;
            }

            if (ConstClass.ZeiKbnCd.UchiZei == zeikbnCd)
            {
                uriKin = uriKin - seiKin;
            }

            if (ConstClass.ZeiKbnCd.HikaZei == zeikbnCd)
            {
                seiKin = 0;
            }

            dataRow.KonkaiKingaku = uriKin + uriKinhinmei + uriKinHikazei;
            dataRow.KonkaiZeigaku = seiKin + hinmeiZei;
            dataRow.KonkaiTorihiki = dataRow.KonkaiKingaku + dataRow.KonkaiZeigaku;
            dataRow.SasihikiZandaka = dataRow.ZenkaiZandaka + dataRow.KonkaiTorihiki;
        }

        #region 前回残高
        /// <summary>
        /// 前回残高設定
        /// </summary>
        private void SetZenkaiZentaka(bool isBottonClickFlg = false)
        {
            DateTime shukkaDate;
            DateTime ukeireDate;
            string denshuKbn = DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode().ToString();

            // 初期化
            this.form.numtxt_KingakuUkeireShiharaiPrivZan.Text = "0";
            this.form.numtxt_KingakuShukkaShiharaiPrivZan.Text = "0";

            if (!isBottonClickFlg) { return; }

            if (DateTime.TryParse(this.form.ParameterDTO_IN.Shukka_Date, out shukkaDate))
            {
                try
                {
                    this.form.numtxt_KingakuShukkaShiharaiPrivZan.Text = SetComma(GetSeikyuZengetsuZandaka(this.form.ParameterDTO_IN.Shukka_Torihikisaki_Cd, shukkaDate, 0, 0, denshuKbn, this.form.ParameterDTO_IN.Shukka_SystemId).ToString());
                }
                catch
                {
                    this.form.numtxt_KingakuShukkaShiharaiPrivZan.Text = "0";
                };
            }
            if (DateTime.TryParse(this.form.ParameterDTO_IN.Ukeire_Date, out ukeireDate))
            {
                try
                {
                    this.form.numtxt_KingakuUkeireShiharaiPrivZan.Text = SetComma(GetShiharaiZengetsuZandaka(this.form.ParameterDTO_IN.Ukeire_Torihikisaki_Cd, ukeireDate, 0, 0, denshuKbn, this.form.ParameterDTO_IN.Ukeire_SystemId).ToString());
                }
                catch
                {
                    this.form.numtxt_KingakuUkeireShiharaiPrivZan.Text = "0";
                };
            }
        }

        /// <summary>
        /// 指定された取引先CDの前月繰越残高を取得
        /// G064\Accessor\DBAccessor.cs - GetZengetsuZandaka() を参照
        /// リアルタイム残高は多少の改変が発生
        /// ：今回伝票額を含むか含まないか（→請求毎税の算出）　パラメータ konkaiKingakuForEmptyZeiKbnCd により加算
        /// ：[当日から見た最後の締データ]＋[当日から見た最後の締翌日～前日までの伝票データ]→[当日から見た最後の締データ]＋[当日の段階で未締の伝票データ]
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <param name="konkaiKingakuForEmptyZeiKbnCd">今回伝票額(品名.税区分CDが無しの金額)</param>
        /// <param name="konkaiKingaku">今回取引額(品名.税区分CDが有りの金額)</param>
        /// <param name="densyuKbn">伝種区分</param>
        /// <param name="systemID">システムID</param>
        /// <returns name="decimal">前月繰越残高</returns>
        private decimal GetSeikyuZengetsuZandaka(string torihikisakiCD, DateTime startDay, decimal konkaiKingakuForEmptyZeiKbnCd, decimal konkaiKingaku, string densyuKbn, string systemID)
        {
            decimal zandaka = 0;
            decimal seikyuuZandaka = 0;

            // Dateから日付を文字列にて取得
            string sDay = startDay.Date.ToString();

            // 指定された取引先CDの開始伝票日付より直近かつ請求番号が最大の請求データから請求差引残高、請求日付を抽出
            DataTable table = this.SeikyuuDenpyouDao.GetSeikyuZenkaiZentaka(torihikisakiCD, sDay);

            // 取引先請求情報取得                //取引先_請求情報マスタ
            M_TORIHIKISAKI_SEIKYUU mtorihikisakiseikyuu = (M_TORIHIKISAKI_SEIKYUU)TorihikisakiSeikyuDao.GetDataByCd(torihikisakiCD);

            // 請求差引残高を基に繰越残高取得
            if (table.Rows.Count != 0)
            {
                // 請求差引残高取得
                // ※直近の請求データのため、該当する取引先は単一
                seikyuuZandaka = decimal.Parse(table.Rows[0]["SEIKYUU_ZANDAKA"].ToString());
            }
            else
            {
                if (!mtorihikisakiseikyuu.KAISHI_URIKAKE_ZANDAKA.IsNull)
                {
                    // 請求差引残高が取得出来なかった場合、取引先請求情報より開始売掛残高を前月繰越残高とする
                    seikyuuZandaka = decimal.Parse(mtorihikisakiseikyuu.KAISHI_URIKAKE_ZANDAKA.ToString());
                }
            }

            // 当日までの未締の売上/入金データテーブルの取得(単一取引先CD)
            DataTable uriageTbl = this.SeikyuuDenpyouDao.GetSeikyuIchiranData(torihikisakiCD, torihikisakiCD, sDay, sDay, densyuKbn, systemID);

            string oldDenNum = string.Empty;
            string oldDenshu = string.Empty;
            decimal oldshohizeirate = 100;

            // 各金額を積算
            decimal uriKin = 0;　       //品名外税金額、伝票毎外税金額、明細毎外税金額(A+F+J)
            decimal uriSotoKin = 0;     //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)
            decimal meiZei = 0;         //品名外税、明細毎外税(B+K)
            decimal denZei = 0;         //伝票毎外税(G)
            decimal nyuKin = 0;
            decimal hikazeikin = 0;     //非課税金額(E+I+N+Q)
            decimal tempkazeikin = 0;   //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)→別途税率を加算用
            decimal kazeikin_tax = 0;   //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)の税率
            ConstClass.ZeiKbnCd tmpZeiKbn = Const.ConstClass.ZeiKbnCd.None;
            ConstClass.ZeiKeisanKbn tmpZeiKeisanKbn = Const.ConstClass.ZeiKeisanKbn.None;

            //税率でソート
            foreach (DataRow row in uriageTbl.Rows)
            {
                if ("10" != row["DENSHU_KBN"].ToString())
                {
                    //初回
                    if (oldshohizeirate == 100)
                    {
                        oldshohizeirate = decimal.Parse(row["URIAGE_SHOUHIZEI_RATE"].ToString());
                    }

                    //税率が変わった場合、税計算を行う
                    if (oldshohizeirate != decimal.Parse(row["URIAGE_SHOUHIZEI_RATE"].ToString()))
                    {
                        //税計算
                        //tempkazeikinに対して税率を算出し、kazeikin_taxに加算、tempkazeikinは初期化
                        kazeikin_tax += FractionCalc(tempkazeikin * oldshohizeirate, int.Parse(mtorihikisakiseikyuu.TAX_HASUU_CD.ToString()));
                        tempkazeikin = 0;
                        oldshohizeirate = decimal.Parse(row["URIAGE_SHOUHIZEI_RATE"].ToString());
                    }

                    // 売上金額を積算
                    if (false == string.IsNullOrEmpty(row["URIAGE_KINGAKU"].ToString()))
                    {
                        // 品名税区分が無く、税計算区分が請求毎税、税区分が外税で登録されていた場合、その金額は請求毎外税の算出対象となる
                        if (true == string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            tmpZeiKbn = (ConstClass.ZeiKbnCd)int.Parse(row["ZEI_KBN_CD"].ToString());
                            switch (tmpZeiKbn)
                            {
                                case ConstClass.ZeiKbnCd.SotoZei:   //外税
                                    tmpZeiKeisanKbn = (ConstClass.ZeiKeisanKbn)int.Parse(row["ZEI_KEISAN_KBN_CD"].ToString());
                                    if (ConstClass.ZeiKeisanKbn.Seikyu == tmpZeiKeisanKbn)
                                    {
                                        //請求毎 O
                                        uriSotoKin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                        tempkazeikin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    }
                                    else
                                    {
                                        //伝票毎・明細毎　F+J
                                        uriKin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    }
                                    break;
                                case ConstClass.ZeiKbnCd.UchiZei:  //内税 L
                                    uriSotoKin += decimal.Parse(KigakuSubstr(row["URIAGE_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    tempkazeikin += decimal.Parse(KigakuSubstr(row["URIAGE_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    break;
                                case ConstClass.ZeiKbnCd.HikaZei: //非課税 I+N+Q
                                    hikazeikin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    break;
                            }
                        }
                        else
                        {
                            //品名税区分あり
                            tmpZeiKbn = (ConstClass.ZeiKbnCd)int.Parse(row["HINMEI_ZEI_KBN_CD"].ToString());
                            switch (tmpZeiKbn)
                            {
                                case ConstClass.ZeiKbnCd.SotoZei: //外税　A
                                    uriKin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    break;
                                case ConstClass.ZeiKbnCd.UchiZei: //内税　C
                                    uriSotoKin += decimal.Parse(KigakuSubstr(row["URIAGE_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    tempkazeikin += decimal.Parse(KigakuSubstr(row["URIAGE_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    break;
                                case ConstClass.ZeiKbnCd.HikaZei: //非課税　E
                                    hikazeikin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    break;
                            }
                        }
                    }

                    // 明細毎税を積算 B+K
                    if (false == string.IsNullOrEmpty(row["SHOUHI_SOTO_ZEI"].ToString()))
                    {
                        meiZei += decimal.Parse(row["SHOUHI_SOTO_ZEI"].ToString());
                    }

                    // 伝票毎税を積算 G
                    if ((oldDenNum != row["DENPYOU_NUMBER"].ToString()) || (oldDenshu != row["DENSHU_KBN"].ToString()))
                    {
                        if (false == string.IsNullOrEmpty(row["DENPYOU_MAI_SOTO_ZEI"].ToString()))
                        {
                            denZei += decimal.Parse(row["DENPYOU_MAI_SOTO_ZEI"].ToString());
                        }
                    }
                }
                else
                {
                    // 入金金額を積算
                    if (false == string.IsNullOrEmpty(row["NYUUKIN_KINGAKU"].ToString()))
                    {
                        nyuKin += decimal.Parse(row["NYUUKIN_KINGAKU"].ToString());
                    }
                }

                // 比較値を更新
                oldDenNum = row["DENPYOU_NUMBER"].ToString();
                oldDenshu = row["DENSHU_KBN"].ToString();
            }

            //税計算
            //tempkazeikinに対して税率を算出し、kazeikin_taxに加算、tempkazeikinは初期化
            kazeikin_tax += FractionCalc(tempkazeikin * oldshohizeirate, int.Parse(mtorihikisakiseikyuu.TAX_HASUU_CD.ToString()));

            // 繰越残高を算出
            // ((請求差引残高＋非課税金額(E+I+N+Q)+売上金額(A+F+J)＋明細毎税(B+K)＋伝票毎税(G)＋税計算ありの金額(C+L+O)＋税計算ありの金額の消費税((C+L+O)*税率))‐入金額)
            zandaka = (seikyuuZandaka + hikazeikin + uriKin + meiZei + denZei + uriSotoKin + kazeikin_tax) - nyuKin;

            return zandaka;
        }

        /// <summary>
        /// 金額加算
        /// </summary>
        private string KigakuAdd(string a, string b)
        {
            return (Convert.ToDecimal(a == null || a.Equals("") ? "0" : a) + Convert.ToDecimal(b == null || b.Equals("") ? "0" : b)).ToString();

        }
        /// <summary>
        /// 金額引算
        /// </summary>
        private string KigakuSubstr(string a, string b)
        {
            return (Convert.ToDecimal(a == null || a.Equals("") ? "0" : a) - Convert.ToDecimal(b == null || b.Equals("") ? "0" : b)).ToString();
        }

        /// <summary>
        /// 指定された取引先CDの前月繰越残高を取得
        /// G073\Accessor\DBAccessor.cs - GetZengetsuZandaka() と同等とすること（名前空間や関数名は伝票発行画面による独自実装）
        /// リアルタイム残高は多少の改変が発生
        /// ：今回伝票額を含むか含まないか（→請求毎税の算出）　パラメータ konkaiKingakuForEmptyZeiKbnCd により加算
        /// ：[当日から見た最後の締データ]＋[当日から見た最後の締翌日～前日までの伝票データ]→[当日から見た最後の締データ]＋[当日の段階で未締の伝票データ]
        /// ：[当日の段階で未締の伝票データ]のうち、自身を含まない
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <param name="konkaiKingakuForEmptyZeiKbnCd">今回伝票額(品名.税区分CDが無しの金額)</param>
        /// <param name="konkaiKingaku">今回取引額(品名.税区分CDが有りの金額)</param>
        /// <param name="densyuKbn">伝種区分</param>
        /// <param name="systemID">システムID</param>
        /// <returns name="decimal">前月繰越残高</returns>
        private decimal GetShiharaiZengetsuZandaka(string torihikisakiCD, DateTime startDay, decimal konkaiKingakuForEmptyZeiKbnCd, decimal konkaiKingaku, string densyuKbn, string systemID)
        {
            decimal zandaka = 0;
            decimal seisanZandaka = 0;

            // Dateから日付を文字列にて取得
            string sDay = startDay.Date.ToString();

            // 指定された取引先CDの開始伝票日付より直近かつ精算番号が最大の精算データから精算差引残高、精算日付を抽出
            DataTable table = this.SeikyuuDenpyouDao.GetShiharaiZenkaiZentaka(torihikisakiCD, sDay);

            // 取引先支払情報取得    //取引先_支払情報マスタ
            M_TORIHIKISAKI_SHIHARAI mtorihikisakishiharai = (M_TORIHIKISAKI_SHIHARAI)TorihikisakiShiharaiDao.GetDataByCd(torihikisakiCD);

            // 精算差引残高を基に繰越残高取得
            if (table.Rows.Count != 0)
            {
                // 精算差引残高取得
                // ※直近の精算データのため、該当する取引先は単一
                seisanZandaka = decimal.Parse(table.Rows[0]["SEISAN_ZANDAKA"].ToString());
            }
            else
            {
                if (!mtorihikisakishiharai.KAISHI_KAIKAKE_ZANDAKA.IsNull)
                {
                    // 精算差引残高が取得出来なかった場合、取引先支払情報より開始売掛残高を前月繰越残高とする
                    seisanZandaka = decimal.Parse(mtorihikisakishiharai.KAISHI_KAIKAKE_ZANDAKA.ToString());
                }
            }

            // 当日までの未締の支払/出金データテーブルの取得(単一取引先CD)
            DataTable shiharaiTbl = this.SeikyuuDenpyouDao.GetShiharaiIchiranData(torihikisakiCD, torihikisakiCD, sDay, sDay, densyuKbn, systemID);

            string oldDenNum = string.Empty;
            string oldDenshu = string.Empty;
            decimal oldshohizeirate = 100;
            ConstClass.ZeiKbnCd tmpZeiKbn = Const.ConstClass.ZeiKbnCd.None;
            ConstClass.ZeiKeisanKbn tmpZeiKeisanKbn = Const.ConstClass.ZeiKeisanKbn.None;

            // 各金額を積算
            decimal shiKin = 0;         //品名外税金額、伝票毎外税金額、明細毎外税金額(A+F+J)
            decimal shiSotoKin = 0;     //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)
            decimal meiZei = 0;         //品名外税、明細毎外税(B+K)
            decimal denZei = 0;         //伝票毎外税(G)
            decimal shuKin = 0;
            decimal hikazeikin = 0;     //非課税金額(E+I+N+Q)
            decimal tempkazeikin = 0;   //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)→別途税率を加算用
            decimal kazeikin_tax = 0;   //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)の税率

            //税率でソート
            foreach (DataRow row in shiharaiTbl.Rows)
            {
                if ("20" != row["DENSHU_KBN"].ToString())
                {
                    //初回
                    if (oldshohizeirate == 100)
                    {
                        oldshohizeirate = decimal.Parse(row["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                    }

                    //税率が変わった場合、税計算を行う
                    if (oldshohizeirate != decimal.Parse(row["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                    {
                        //税計算
                        //tempkazeikinに対して税率を算出し、kazeikin_taxに加算、tempkazeikinは初期化
                        kazeikin_tax += FractionCalc(tempkazeikin * oldshohizeirate, int.Parse(mtorihikisakishiharai.TAX_HASUU_CD.ToString()));
                        tempkazeikin = 0;
                        oldshohizeirate = decimal.Parse(row["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                    }

                    // 支払金額を積算
                    if (false == string.IsNullOrEmpty(row["SHIHARAI_KINGAKU"].ToString()))
                    {
                        // 品名税区分が無く、税計算区分が請求毎税、税区分が外税で登録されていた場合、その金額は請求毎外税の算出対象となる
                        if (true == string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            tmpZeiKbn = (ConstClass.ZeiKbnCd)int.Parse(row["ZEI_KBN_CD"].ToString());
                            switch (tmpZeiKbn)
                            {
                                case ConstClass.ZeiKbnCd.SotoZei:   //外税
                                    tmpZeiKeisanKbn = (ConstClass.ZeiKeisanKbn)int.Parse(row["ZEI_KEISAN_KBN_CD"].ToString());
                                    if (Const.ConstClass.ZeiKeisanKbn.Seikyu == tmpZeiKeisanKbn)
                                    {
                                        //請求毎 O
                                        shiSotoKin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                        tempkazeikin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    }
                                    else
                                    {
                                        //伝票毎・明細毎　F+J
                                        shiKin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    }
                                    break;
                                case ConstClass.ZeiKbnCd.UchiZei:   //内税 L
                                    shiSotoKin += decimal.Parse(KigakuSubstr(row["SHIHARAI_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    tempkazeikin += decimal.Parse(KigakuSubstr(row["SHIHARAI_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    break;
                                case ConstClass.ZeiKbnCd.HikaZei:   //非課税I+N+Q
                                    hikazeikin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    break;
                            }
                        }
                        else
                        {
                            tmpZeiKbn = (ConstClass.ZeiKbnCd)int.Parse(row["HINMEI_ZEI_KBN_CD"].ToString());
                            //品名税区分あり
                            switch (tmpZeiKbn)
                            {
                                case ConstClass.ZeiKbnCd.SotoZei://外税　A
                                    shiKin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    break;
                                case ConstClass.ZeiKbnCd.UchiZei://内税　C
                                    shiSotoKin += decimal.Parse(KigakuSubstr(row["SHIHARAI_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    tempkazeikin += decimal.Parse(KigakuSubstr(row["SHIHARAI_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    break;
                                case ConstClass.ZeiKbnCd.HikaZei://非課税　E
                                    hikazeikin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    break;
                            }
                        }
                    }

                    // 明細毎税を積算 B+K
                    if (false == string.IsNullOrEmpty(row["SHOUHI_SOTO_ZEI"].ToString()))
                    {
                        meiZei += decimal.Parse(row["SHOUHI_SOTO_ZEI"].ToString());
                    }

                    // 伝票毎税を積算 G
                    if ((oldDenNum != row["DENPYOU_NUMBER"].ToString()) || (oldDenshu != row["DENSHU_KBN"].ToString()))
                    {
                        if (false == string.IsNullOrEmpty(row["DENPYOU_MAI_SOTO_ZEI"].ToString()))
                        {
                            denZei += decimal.Parse(row["DENPYOU_MAI_SOTO_ZEI"].ToString());
                        }
                    }
                }
                else
                {
                    // 出金金額を積算
                    if (false == string.IsNullOrEmpty(row["SHUKKIN_KINGAKU"].ToString()))
                    {
                        shuKin += decimal.Parse(row["SHUKKIN_KINGAKU"].ToString());
                    }
                }

                // 比較値を更新
                oldDenNum = row["DENPYOU_NUMBER"].ToString();
                oldDenshu = row["DENSHU_KBN"].ToString();
            }

            //税計算
            //tempkazeikinに対して税率を算出し、kazeikin_taxに加算、tempkazeikinは初期化
            kazeikin_tax += FractionCalc(tempkazeikin * oldshohizeirate, int.Parse(mtorihikisakishiharai.TAX_HASUU_CD.ToString()));

            // 繰越残高を算出
            // ((精算差引残高＋非課税金額(E+I+N+Q)＋支払金額(A+F+J)＋明細毎税(B+K)＋伝票毎税(G)＋税計算ありの金額(C+L+O)＋税計算ありの金額の消費税((C+L+O)*税率))‐出金額)
            zandaka = (seisanZandaka + hikazeikin + shiKin + meiZei + denZei + shiSotoKin + kazeikin_tax) - shuKin;

            return zandaka;

        }

        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private string SetComma(string value)
        {
            if (value == null)
            {
                return "0";
            }
            else
            {
                return string.Format("{0:#,0}", Convert.ToDecimal(value));
            }
        }

        /// <summary>
        /// 数字に変更
        /// </summary>
        /// <returns></returns>
        private int StringToInt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            else
            {
                return int.Parse(value);
            }
        }

        /// <summary>
        /// 指定された端数CDに従い、金額の端数処理を行う
        /// </summary>
        /// <param name="kingaku">端数処理対象金額</param>
        /// <param name="calcCD">端数CD</param>
        /// <returns name="decimal">端数処理後の金額</returns>
        public static decimal FractionCalc(decimal kingaku, int calcCD)
        {
            decimal returnVal = 0;		// 戻り値
            decimal sign = 1;           // 1(正) or -1(負)

            if (kingaku < 0)
                sign = -1;

            switch ((fractionType)calcCD)
            {
                case fractionType.CEILING:
                    returnVal = Math.Ceiling(Math.Abs(kingaku)) * sign;
                    break;
                case fractionType.FLOOR:
                    returnVal = Math.Floor(Math.Abs(kingaku)) * sign;
                    break;
                case fractionType.ROUND:
                    returnVal = Math.Round(Math.Abs(kingaku), 0, MidpointRounding.AwayFromZero) * sign;
                    break;
                default:
                    // 何もしない
                    returnVal = kingaku;
                    break;
            }

            return returnVal;
        }
        #endregion


        /// <summary>
        /// 品名外税明細あり、税計算区分：請求/支払毎の場合、アラート
        /// </summary>
        private void SeiMaiSoto_Check()
        {
            //メッセージD
            string ErrUHinmeiCD = string.Empty;
            string ErrSHinmeiCD = string.Empty;
            string ErrKBN = string.Empty;

            if ("2".Equals(this.form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text))
            {
                if (this.form.ParameterDTO_IN.Shukka_Out_Tenpyo_Cnt != null)
                {
                    foreach (MeiseiDTOClass meiseiDto in this.form.ParameterDTO_IN.Shukka_Out_Tenpyo_Cnt)
                    {
                        ConstClass.ZeiKbnCd tmpZeiKbn = Const.ConstClass.ZeiKbnCd.None;
                        if (!string.IsNullOrEmpty(meiseiDto.Hinmei_Zei_Kbn_Cd))
                        {
                            tmpZeiKbn = (ConstClass.ZeiKbnCd)int.Parse(meiseiDto.Hinmei_Zei_Kbn_Cd);
                            if (ConstClass.ZeiKbnCd.SotoZei.Equals(tmpZeiKbn))
                            {
                                //エラー表示用品目CDセット
                                ErrUHinmeiCD = ErrUHinmeiCD + meiseiDto.Hinmei_Cd + "、";
                            }
                        }
                    }
                }
            }
            if ("2".Equals(this.form.numtxt_UkeireSeikyuZeiKeisanKbn.Text))
            {
                if (this.form.ParameterDTO_IN.Ukeire_Out_Tenpyo_Cnt != null)
                {
                    foreach (MeiseiDTOClass meiseiDto in this.form.ParameterDTO_IN.Ukeire_Out_Tenpyo_Cnt)
                    {
                        ConstClass.ZeiKbnCd tmpZeiKbn = Const.ConstClass.ZeiKbnCd.None;
                        if (!string.IsNullOrEmpty(meiseiDto.Hinmei_Zei_Kbn_Cd))
                        {
                            tmpZeiKbn = (ConstClass.ZeiKbnCd)int.Parse(meiseiDto.Hinmei_Zei_Kbn_Cd);
                            if (ConstClass.ZeiKbnCd.SotoZei.Equals(tmpZeiKbn))
                            {
                                //エラー表示用品目CDセット
                                ErrSHinmeiCD = ErrSHinmeiCD + meiseiDto.Hinmei_Cd + "、";
                            }
                        }
                    }
                }
            }

            ErrHinmeiCD = ErrUHinmeiCD + ErrSHinmeiCD;

            if (!string.IsNullOrEmpty(ErrUHinmeiCD))
            {
                ErrKBN = ErrKBN + "請求書/";
            }
            if (!string.IsNullOrEmpty(ErrSHinmeiCD))
            {
                ErrKBN = ErrKBN + "支払明細書/";
            }

            if (!string.IsNullOrEmpty(ErrHinmeiCD))
            {
                ErrHinmeiCD = ErrHinmeiCD.Substring(0, ErrHinmeiCD.Length - 1);
                ErrKBN = ErrKBN.Substring(0, ErrKBN.Length - 1);
                errmessage.MessageBoxShowWarn(string.Format("税区分が登録されている品名は、\r適格請求書の要件を満たした{0}になりません。\r（品名CD={1}）", ErrKBN, ErrHinmeiCD));
            }
        }

        #region IBuisinessLogic(未実装)
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
        public bool Equals(LogicClass other)
        {
            return this.Equals(other);
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

        #region IDisposable

        /// <summary>
        /// 開放処理
        /// </summary>
        public void Dispose()
        {
            this.EventDelete();
        }

        #endregion
    }
}
