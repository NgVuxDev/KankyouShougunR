using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;
using Shougun.Core.Common.BusinessCommon.Const;
using r_framework.CustomControl;
using System.Data;
using Shougun.Core.Allocation.ContenaRirekiIchiranHyou.Report;
using CommonChouhyouPopup.App;
using System.Drawing;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Allocation.ContenaRirekiIchiranHyou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数
        /// <summary>レポートフルパス名</summary>
        const string SuuryouKanriReportFullPathName = "./Template/R596-Form.xml";
        const string KotaiKanriReportFullPathName = "./Template/R596ForKotaiKanri-Form.xml";
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region Fields
        /// <summary>ボタン情報を格納しているＸＭＬファイルのパス（リソース）を保持するフィールド</summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.Allocation.ContenaRirekiIchiranHyou.Setting.ButtonSetting.xml";

        /// <summary>操作区分</summary>
        private readonly string sousaKbn_secchi = "1";
        private readonly string sousaKbn_hikiage = "2";

        /// <summary>業者／現場設定</summary>
        private readonly string gyoushaGenbaSetting_Hikiage = "1";

        /// <summary>出力内容</summary>
        private readonly string outputSetting_Hiduke = "1";

        /// <summary>
        /// 画面の情報から生成した検索条件
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// 検索結果
        /// </summary>
        private List<SearchResultDTO> searchReslut;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>M_SYS_INO</summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>コンテナ管理方法</summary>
        private Int16 contenaKanriHouhou;

        /// <summary>コンテナマスタDAO</summary>
        private IM_CONTENADao contenaDao;

        /// <summary>変更前業者CD(From)</summary>
        internal string beforeGyoushaCdFrom;

        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 取得した現場エンティティを保持する
        /// </summary>
        private List<M_GENBA> genbaList = new List<M_GENBA>();

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;
        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        #endregion

        #region Constructors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOClass();
            // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            // メッセージ表示オブジェクト
            msgLogic = new MessageBoxShowLogic();
            // 現場Dao
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region Inits
        /// <summary>
        /// 初期化
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                // ボタンのテキスト初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                /**
                 * 表示データ初期化
                 */
                this.form.kyoten.Text = CommonConst.KYOTEN_CD_ZENSHA.ToString();
                this.contenaDao = DaoInitUtility.GetComponent<IM_CONTENADao>();
                var kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                var kyoten = kyotenDao.GetDataByCd(CommonConst.KYOTEN_CD_ZENSHA.ToString());
                if (kyoten != null)
                {
                    this.form.kyotenMei.Text = kyoten.KYOTEN_NAME_RYAKU;
                }
                this.form.sousaKbn.Text = this.sousaKbn_secchi;
                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.denpyouHidukeFrom.Value = parentForm.sysDate;
                this.form.denpyouHidukeTo.Value = parentForm.sysDate;
                this.form.gyoushaGenbaSetting.Text = this.gyoushaGenbaSetting_Hikiage;
                this.form.outputSetting.Text = this.outputSetting_Hiduke;

                // システム情報を取得し、初期値をセットする
                this.GetSysInfoInit();

                // コンテナ管理方法をセット
                this.contenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                    ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (Int16)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;

                // コンテナ管理方法によってデザインを変更する
                this.ChangeLabelAndLayout();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>ボタン初期化処理</summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
        }

        /// <summary>ボタン設定の読込</summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                throw e;
            }
        }

        /// <summary>イベントの初期化処理</summary>
        private void EventInit()
        {
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;

                // CSV出力ボタン(F5)イベント生成
                this.form.C_Regist(parentForm.bt_func5);
                parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

                // 明細項目表示ボタン(F7)イベント生成
                this.form.C_Regist(parentForm.bt_func7);
                parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

                // 20141127 teikyou ダブルクリックを追加する　start
                this.form.denpyouHidukeTo.MouseDoubleClick += new MouseEventHandler(denpyouHidukeTo_MouseDoubleClick);
                this.form.contenaShuruiTo.MouseDoubleClick += new MouseEventHandler(contenaShuruiTo_MouseDoubleClick);
                this.form.gyoushaTo.MouseDoubleClick += new MouseEventHandler(gyoushaTo_MouseDoubleClick);
                this.form.genbaTo.MouseDoubleClick += new MouseEventHandler(genbaTo_MouseDoubleClick);
                this.form.contenaTo.MouseDoubleClick += new MouseEventHandler(contenaTo_MouseDoubleClick);
                // 20141127 teikyou ダブルクリックを追加する　end

                /// 20141203 Houkakou 「コンテナ履歴一覧表」の日付チェックを追加する　start
                this.form.denpyouHidukeFrom.Leave += new System.EventHandler(denpyouHidukeFrom_Leave);
                this.form.denpyouHidukeTo.Leave += new System.EventHandler(denpyouHidukeTo_Leave);
                /// 20141203 Houkakou 「コンテナ履歴一覧表」の日付チェックを追加する　end
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #region システム情報を取得し、初期値をセットする
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }
        #endregion

        #region on コンテナ管理方法によるラベルとレイアウトの変更
        /// <summary>
        /// コンテナ管理方法によりラベルとレイアウトを変更する
        /// </summary>
        private void ChangeLabelAndLayout()
        {
            if (this.contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                // 個体管理

                // レイアウト調整(抽出条件)
                this.form.customPanel12.Visible = false;
                this.form.customPanel13.Location = new Point(21, 25);
                this.form.customPanel3.Location = new Point(21, 47);

                // レイアウト調整(抽出範囲)
                this.form.customPanel15.Visible = true;
                this.form.customPanel15.Location = new Point(21, 47);
                this.form.customPanel5.Location = new Point(21, 69);
                this.form.customPanel6.Location = new Point(21, 91);


                // レイアウト調整(出力内容)
                this.form.outputSetting_ContenaName.Visible = true;
                int contenaNameLeft = this.form.outputSetting2.Left + this.form.outputSetting2.Width + 5;
                this.form.outputSetting_ContenaName.Location = new Point(contenaNameLeft, 0);
                int gyoushaLeft = this.form.outputSetting_ContenaName.Left + this.form.outputSetting_ContenaName.Width + 5;
                this.form.outputSetting3.Location = new Point(gyoushaLeft, 0);
                int genbaLeft = this.form.outputSetting3.Left + this.form.outputSetting3.Width + 5;
                this.form.outputSetting4.Location = new Point(genbaLeft, 0);
                this.form.customPanel14.Width += 10;
                this.form.customPanel8.Width += 10;
                this.form.customPanel11.Width += 10;

                // 出力内容の情報更新
                this.form.outputSetting.LinkedRadioButtonArray = new string[] { "outputSetting1", "outputSetting2", "outputSetting_ContenaName", "outputSetting3", "outputSetting4" };
                this.form.outputSetting.Tag = "【1、2、3、4、5】のいずれかで入力してください";
                this.form.outputSetting3.Value = "4";
                this.form.outputSetting3.Text = "4.業者別";
                this.form.outputSetting4.Value = "5";
                this.form.outputSetting4.Text = "5.現場別";
                //this.form.outputSetting.CharacterLimitList = new char[5] { '1', '2', '3', '4', '5' };
                r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
                rangeSettingDto4.Max = new decimal(new int[] {
                                                               5,
                                                               0,
                                                               0,
                                                               0});
                rangeSettingDto4.Min = new decimal(new int[] {
                                                               1,
                                                               0,
                                                               0,
                                                               0});
                this.form.outputSetting.RangeSetting = rangeSettingDto4;
            }
            else
            {
                // 数量管理
                // デザイナのほうは数量管理用のデザインになっているためここでは何もしない
            }
        }
        #endregion
        #endregion

        #region チェックメソッド

        #region 業者チェック
        /// <summary>
        /// 業者のFromとToに最小値と最大値をセットします
        /// </summary>
        internal bool SetGyoushaCdFromTo()
        {
            bool ret = true;
            try
            {
                var dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                var mGyoushaArray = dao.GetAllData();

                if (mGyoushaArray.Count() > 0)
                {
                    var minGyousha = mGyoushaArray.Where(g => g.GYOUSHA_CD == mGyoushaArray.Min(gy => gy.GYOUSHA_CD)).FirstOrDefault();
                    var maxGyousha = mGyoushaArray.OrderByDescending(m => m.GYOUSHA_CD).Where(g => g.GYOUSHA_CD == mGyoushaArray.Max(gy => gy.GYOUSHA_CD)).FirstOrDefault();

                    if (String.IsNullOrEmpty(this.form.gyoushaFrom.Text))
                    {
                        this.form.gyoushaFrom.Text = minGyousha.GYOUSHA_CD;
                        this.form.gyoushaMeiFrom.Text = minGyousha.GYOUSHA_NAME_RYAKU;
                    }

                    if (this.form.gyoushaTo.Enabled
                        && String.IsNullOrEmpty(this.form.gyoushaTo.Text))
                    {
                        this.form.gyoushaTo.Text = maxGyousha.GYOUSHA_CD;
                        this.form.gyoushaMeiTo.Text = maxGyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyoushaCdFromTo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion

        #region 現場チェック
        /// <summary>
        /// 現場のFromとToに最小値と最大値をセットします
        /// </summary>
        internal bool SetGenbaCdFromTo()
        {
            bool ret = true;
            try
            {
                // 無駄なDBアセスをさせないため現場範囲項目が有効かチェック
                if (!this.form.genbaTo.Enabled
                    && !this.form.genbaFrom.Enabled)
                {
                    return ret;
                }

                if (string.IsNullOrEmpty(this.form.gyoushaFrom.Text))
                {
                    return ret;
                }

                var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                var genbaCondition = new M_GENBA();
                genbaCondition.GYOUSHA_CD = this.form.gyoushaFrom.Text;
                genbaCondition.ISNOT_NEED_DELETE_FLG = true;
                var mGenbaArray = dao.GetAllValidData(genbaCondition);

                if (mGenbaArray.Count() > 0)
                {
                    var minGenba = mGenbaArray.Where(g => g.GENBA_CD == mGenbaArray.Min(gy => gy.GENBA_CD)).FirstOrDefault();
                    var maxGenba = mGenbaArray.OrderByDescending(m => m.GENBA_CD).Where(g => g.GENBA_CD == mGenbaArray.Max(gy => gy.GENBA_CD)).FirstOrDefault();

                    if (this.form.genbaTo.Enabled
                        && String.IsNullOrEmpty(this.form.genbaFrom.Text))
                    {
                        this.form.genbaFrom.Text = minGenba.GENBA_CD;
                        this.form.genbaMeiFrom.Text = minGenba.GENBA_NAME_RYAKU;
                    }

                    if (this.form.genbaTo.Enabled
                        && String.IsNullOrEmpty(this.form.genbaTo.Text))
                    {
                        this.form.genbaTo.Text = maxGenba.GENBA_CD;
                        this.form.genbaMeiTo.Text = maxGenba.GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGenbaCdFromTo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion

        #region コンテナ種類CD空チェック
        /// <summary>
        /// コンテナ種類CD空チェック
        /// </summary>
        /// <returns>true:問題無、false:問題有</returns>
        internal bool SetContenaShuruiCdFromTo()
        {
            bool ret = true;
            try
            {
                var dao = DaoInitUtility.GetComponent<IM_CONTENA_SHURUIDao>();
                var mContenaShuruiArray = dao.GetAllData();

                if (mContenaShuruiArray.Count() > 0)
                {
                    var minContenaShurui = mContenaShuruiArray.Where(g => g.CONTENA_SHURUI_CD == mContenaShuruiArray.Min(gy => gy.CONTENA_SHURUI_CD)).FirstOrDefault();
                    var maxContenaShurui = mContenaShuruiArray.Where(g => g.CONTENA_SHURUI_CD == mContenaShuruiArray.Max(gy => gy.CONTENA_SHURUI_CD)).FirstOrDefault();

                    if (String.IsNullOrEmpty(this.form.contenaShuruiFrom.Text))
                    {
                        this.form.contenaShuruiFrom.Text = minContenaShurui.CONTENA_SHURUI_CD;
                        this.form.contenaShuruiMeiFrom.Text = minContenaShurui.CONTENA_SHURUI_NAME_RYAKU;
                    }

                    if (String.IsNullOrEmpty(this.form.contenaShuruiTo.Text))
                    {
                        this.form.contenaShuruiTo.Text = maxContenaShurui.CONTENA_SHURUI_CD;
                        this.form.contenaShuruiMeiTo.Text = maxContenaShurui.CONTENA_SHURUI_NAME_RYAKU;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetContenaShuruiCdFromTo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion

        #region 抽出範囲チェック
        /// <summary>
        /// 抽出範囲項目の指定チェック
        /// </summary>
        /// <returns>エラーが発生した入力項目</returns>
        internal string CheckErr(out bool catchErr)
        {
            string errMsg = string.Empty;
            catchErr = false;
            try
            {
                if (!this.CheckCodeFromTo(this.form.contenaShuruiFrom, this.form.contenaShuruiTo))
                {
                    errMsg = "コンテナ種類";
                    return errMsg;
                }

                if (this.form.gyoushaTo.Enabled
                    && !this.CheckCodeFromTo(this.form.gyoushaFrom, this.form.gyoushaTo))
                {
                    errMsg = "業者";
                    return errMsg;
                }

                if (this.form.genbaFrom.Enabled
                    && this.form.genbaTo.Enabled
                    && !this.CheckCodeFromTo(this.form.genbaFrom, this.form.genbaTo))
                {
                    errMsg = "現場";
                    return errMsg;
                }

                if (this.checkContenaNameEnabled()
                    && !this.CheckCodeFromTo(this.form.contenaFrom, this.form.contenaTo))
                {
                    errMsg = "コンテナ名";
                    return errMsg;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckErr", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                errMsg = string.Empty;
                catchErr = true;
            }
            return errMsg;
        }
        #endregion

        /// <summary>
        /// 各CDのFromToの関係をチェック
        /// </summary>
        /// <param name="TextFrom"></param>
        /// <param name="TextTo"></param>
        /// <returns></returns>
        private bool CheckCodeFromTo(CustomAlphaNumTextBox TextFrom, CustomAlphaNumTextBox TextTo)
        {
            bool ret = true;
            var cdFrom = TextFrom.Text;
            var cdTo = TextTo.Text;
            TextFrom.IsInputErrorOccured = false;
            TextTo.IsInputErrorOccured = false;

            if (!String.IsNullOrEmpty(cdFrom) && !String.IsNullOrEmpty(cdTo))
            {
                if (cdFrom.CompareTo(cdTo) > 0)
                {
                    // Fromの方がToより大きい場合エラー
                    TextFrom.IsInputErrorOccured = true;
                    TextTo.IsInputErrorOccured = true;
                    TextFrom.Focus();
                    ret = false;
                }
            }

            return ret;
        }
        #endregion

        #region 業者／現場設定変更処理
        /// <summary>
        /// 業者／現場設定の状態によって入力項目を制限する
        /// </summary>
        internal void ChangeGyoushaAndGenbaEnabled()
        {
            if (this.gyoushaGenbaSetting_Hikiage.Equals(this.form.gyoushaGenbaSetting.Text))
            {
                // 業者From
                this.form.gyoushaTo.Enabled = false;
                this.form.gyoushaToSearch.Enabled = false;
                // 現場From～To
                this.form.genbaFrom.Enabled = true;
                this.form.genbaMeiFrom.Enabled = true;
                this.form.genbaFromSearch.Enabled = true;
                this.form.genbaTo.Enabled = true;
                this.form.genbaMeiTo.Enabled = true;
                this.form.genbaToSearch.Enabled = true;
            }
            else
            {
                // 業者From
                this.form.gyoushaTo.Enabled = true;
                this.form.gyoushaToSearch.Enabled = true;
                // 現場From～To
                this.form.genbaFrom.Enabled = false;
                this.form.genbaMeiFrom.Enabled = false;
                this.form.genbaFromSearch.Enabled = false;
                this.form.genbaTo.Enabled = false;
                this.form.genbaMeiTo.Enabled = false;
                this.form.genbaToSearch.Enabled = false;
            }
        }
        #endregion

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        /// 
        internal void CSVPrint()
        {
            try
            {
                // 検索
                var count = this.Search();

                // 検索結果0件ならメッセージ表示
                if (count < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                    return;
                }

                // 出力
                this.CreateCSVPrintData();
            }
            catch (Exception ex)
            {
                LogUtility.Error("PrintCSV", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }

        }

        /// <summary>
        /// CSV出力データ作成
        /// </summary>
        private void CreateCSVPrintData()
        {
            try
            {
                SearchResultDTO[] printData = { new SearchResultDTO() };

                //明細部データ
                DataTable printDetailData = new DataTable();
                printDetailData = CreateDetailData(printDetailData, printData);
                DataTable csvDT = new DataTable();
                string output_option = this.form.outputSetting.Text;
                string out_put_head = string.Empty;
                string strHiduke = "";
                // 日付文言
                if (this.form.outputSetting.Text.Equals(this.outputSetting_Hiduke))
                {
                   
                    if (this.form.sousaKbn.Text.Equals(this.sousaKbn_secchi))
                    {
                        strHiduke = "設置日";
                    }
                    else if (this.form.sousaKbn.Text.Equals(this.sousaKbn_hikiage))
                    {
                        strHiduke = "引揚日";
                    }
                    else
                    {
                        strHiduke = "設置・引揚日";
                    }
                   
                }
                // 個体管理
                if (this.contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
                {
                    switch (output_option)
                    {
                        // 日付別
                        case "1":
                            out_put_head = strHiduke+",コンテナ操作,業者CD,業者名,現場CD,現場名,コンテナ種類CD,コンテナ種類名,コンテナCD,コンテナ名,更新日付";
                            break;
                        //コンテナ種類別
                        case "2":
                            out_put_head = "コンテナ種類CD,コンテナ種類名,コンテナ操作,発生日付,コンテナCD,コンテナ名,業者CD,業者名,現場CD,現場名,更新日付";
                            break;
                        //コンテナ名別
                        case "3":
                            out_put_head = "コンテナ種類CD,コンテナ種類名,コンテナCD,コンテナ名,コンテナ操作,発生日付,業者CD,業者名,現場CD,現場名,更新日付";
                            break;
                        //コンテナ業者別
                        case "4":
                            out_put_head = "業者CD,業者名,コンテナ操作,発生日付,コンテナ種類CD,コンテナ種類名,コンテナCD,コンテナ名,現場CD,現場名,更新日付";
                            break;
                        //コンテナ現場別
                        case "5":
                            out_put_head = "業者CD,業者名,現場CD,現場名,コンテナ操作,発生日付,コンテナ種類CD,コンテナ種類名,コンテナCD,コンテナ名,更新日付";
                            break;
                    }
                }
                // 数量管理
                else
                {
                    switch (output_option)
                    {
                        // 日付別
                        case "1":
                            out_put_head =strHiduke+ ",コンテナ操作,業者CD,業者名,現場CD,現場名,コンテナ種類CD,コンテナ種類名,数量,更新日付";
                            break;
                        //コンテナ種類別
                        case "2":
                            out_put_head = "コンテナ種類CD,コンテナ種類名,コンテナ操作,発生日付,数量,業者CD,業者名,現場CD,現場名,更新日付";
                            break;
                        //コンテナ業者別
                        case "3":
                            out_put_head = "業者CD,業者名,コンテナ操作,発生日付,コンテナ種類CD,コンテナ種類名,数量,現場CD,現場名,更新日付";
                            break;
                        //コンテナ現場別
                        case "4":
                            out_put_head = "業者CD,業者名,現場CD,現場名,コンテナ操作,発生日付,コンテナ種類CD,コンテナ種類名,数量,更新日付";
                            break;
                    }
                }

                string[] out_put_head_array = out_put_head.Split(',');

                foreach (var n in out_put_head_array)
                {
                    csvDT.Columns.Add(n);
                }

                //明細部データを対したCSV出力データに与え
                for (int i = 0; i < printDetailData.Rows.Count; i++)
                {
                    DataRow row = csvDT.NewRow();
                    for (int m = 0; m < csvDT.Columns.Count; m++)
                    {
                        switch (csvDT.Columns[m].ColumnName)
                        {
                            case "設置日": row[m] = printDetailData.Rows[i]["SECCHI_DATE"]; break;
                            case "引揚日": row[m] = printDetailData.Rows[i]["SECCHI_DATE"]; break;
                            case "設置・引揚日": row[m] = printDetailData.Rows[i]["SECCHI_DATE"]; break;
                            case "コンテナ操作": row[m] = printDetailData.Rows[i]["CONTENA_SOUSA"]; break;
                            case "発生日付": row[m] = printDetailData.Rows[i]["SECCHI_DATE"].ToString().Split(' ')[0]; break;
                            case "業者CD": row[m] = printDetailData.Rows[i]["GYOUSHA_CD"]; break;
                            case "業者名": row[m] = printDetailData.Rows[i]["GYOUSHA_NAME"]; break;
                            case "現場CD": row[m] = printDetailData.Rows[i]["GENBA_CD"]; break;
                            case "現場名": row[m] = printDetailData.Rows[i]["GENBA_NAME"]; break;
                            case "コンテナ種類CD": row[m] = printDetailData.Rows[i]["CONTENA_SHURUI_CD"]; break;
                            case "コンテナ種類名": row[m] = printDetailData.Rows[i]["CONTENA_SHURUI_NAME"]; break;
                            case "コンテナCD": row[m] = printDetailData.Rows[i]["CONTENA_CD"]; break;
                            case "コンテナ名": row[m] = printDetailData.Rows[i]["CONTENA_NAME"]; break;
                            case "更新日付": row[m] = printDetailData.Rows[i]["UPDATE_DATE"]; break;
                            case "数量": row[m] = printDetailData.Rows[i]["SUURYOU"]; break;

                        }
                    }
                    csvDT.Rows.Add(row);
                }
                if (csvDT == null || csvDT.Rows.Count == 0)
                {
                    this.msgLogic.MessageBoxShow("E044");

                }
                // 出力先指定のポップアップを表示させる。
                if (this.msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    // CSV出力
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_CONTENA_RIREKI_ICHIRAN_HYOU), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVPrint", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }
        #endregion

        #region 表示処理
        /// <summary>
        /// 表示
        /// </summary>
        internal void Print()
        {
            try
            {
                // 検索
                var count = this.Search();

                // 検索結果0件ならメッセージ表示
                if (count < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                    return;
                }

                // 出力
                this.CreatePrintData();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        #region 検索
        /// <summary>
        /// 検索メソッド
        /// </summary>
        /// <returns>検索件数</returns>
        public int Search()
        {
            int searchCount = 0;

            // 検索条件生成
            this.dto = this.CreateDto();

            // 検索
            var dao = DaoInitUtility.GetComponent<DAOClass>();
            // 解析しやすいように検索条件をログに出力
            LogUtility.Info("コンテナ履歴一覧表 " + this.dto);

            if (this.contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                this.searchReslut = dao.GetIchiranDataSqlForKotaiKanri(this.dto);
            }
            else
            {
                this.searchReslut = dao.GetIchiranDataSqlForSuuryouKanri(this.dto);
            }
            searchCount = this.searchReslut.Count;

            return searchCount;

        }
        #endregion

        #region 帳票出力データ作成
        /// <summary>
        /// 帳票出力データ作成
        /// </summary>
        private void CreatePrintData()
        {
            SearchResultDTO[] printData = { new SearchResultDTO() };

            #region 表示用抽出条件のセット
            /**
             * 抽出条件
             */
            DataTable printHeaderData = new DataTable();
            // 自社名
            printHeaderData.Columns.Add("FH_CORP_NAME_RYAKU_VLB");
            // 拠点
            printHeaderData.Columns.Add("FH_KYOTEN_NAME_RYAKU_VLB");
            // 発行日(年月日時分秒)
            printHeaderData.Columns.Add("FH_PRINT_DATE_VLB");
            // 日付
            printHeaderData.Columns.Add("FH_DATE_FROM_VLB");
            printHeaderData.Columns.Add("FH_DATE_TO_VLB");
            // コンテナ種類
            printHeaderData.Columns.Add("FH_CONTENA_SHURUI_CD_FROM_VLB");
            printHeaderData.Columns.Add("FH_CONTENA_SHURUI_NAME_FROM_VLB");
            printHeaderData.Columns.Add("FH_CONTENA_SHURUI_CD_FROM_TO_VLB");
            printHeaderData.Columns.Add("FH_CONTENA_SHURUI_NAME_TO_VLB");
            // 業者
            printHeaderData.Columns.Add("FH_GYOUSHA_CD_FROM_VLB");
            printHeaderData.Columns.Add("FH_GYOUSHA_NAME_FROM_VLB");
            printHeaderData.Columns.Add("FH_GYOUSHA_CD_TO_VLB");
            printHeaderData.Columns.Add("FH_GYOUSHA_NAME_TO_VLB");
            // 現場
            printHeaderData.Columns.Add("FH_GENBA_CD_FROM_VLB");
            printHeaderData.Columns.Add("FH_GENBA_NAME_FROM_VLB");
            printHeaderData.Columns.Add("FH_GENBA_CD_TO_VLB");
            printHeaderData.Columns.Add("FH_GENBA_NAME_TO_VLB");
            // コンテナ名
            printHeaderData.Columns.Add("FH_CONTENA_CD_FROM_VLB");
            printHeaderData.Columns.Add("FH_CONTENA_NAME_FROM_VLB");
            printHeaderData.Columns.Add("FH_CONTENA_CD_FROM_TO_VLB");
            printHeaderData.Columns.Add("FH_CONTENA_NAME_TO_VLB");

            // 日付文言
            printHeaderData.Columns.Add("PH_SECCHI_DATE_FLB");

            // データセット
            DataRow printHeaderRow = printHeaderData.NewRow();

            // 自社情報
            IM_CORP_INFODao dao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            M_CORP_INFO[] corpInfo;
            corpInfo = (M_CORP_INFO[])dao.GetAllData();
            if (corpInfo != null && corpInfo.Count() > 0)
            {
                printHeaderRow["FH_CORP_NAME_RYAKU_VLB"] = corpInfo[0].CORP_NAME;
            }

            printHeaderRow["FH_KYOTEN_NAME_RYAKU_VLB"] = this.form.kyotenMei.Text;

            // 発効日
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //DateTime dt = DateTime.Now;
            DateTime dt = this.getDBDateTime();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            string dateTime = dt.ToString("yyyy/MM/dd HH:mm:ss");
            printHeaderRow["FH_PRINT_DATE_VLB"] = dateTime;

            // 日付
            DateTime tempDate = (DateTime)this.form.denpyouHidukeFrom.Value;
            printHeaderRow["FH_DATE_FROM_VLB"] = tempDate.Date.ToShortDateString();
            tempDate = (DateTime)this.form.denpyouHidukeTo.Value;
            printHeaderRow["FH_DATE_TO_VLB"] = tempDate.Date.ToShortDateString();

            // コンテナ種類
            printHeaderRow["FH_CONTENA_SHURUI_CD_FROM_VLB"] = this.form.contenaShuruiFrom.Text;
            printHeaderRow["FH_CONTENA_SHURUI_NAME_FROM_VLB"] = this.form.contenaShuruiMeiFrom.Text;
            printHeaderRow["FH_CONTENA_SHURUI_CD_FROM_TO_VLB"] = this.form.contenaShuruiTo.Text;
            printHeaderRow["FH_CONTENA_SHURUI_NAME_TO_VLB"] = this.form.contenaShuruiMeiTo.Text;

            // 業者
            printHeaderRow["FH_GYOUSHA_CD_FROM_VLB"] = this.form.gyoushaFrom.Text;
            printHeaderRow["FH_GYOUSHA_NAME_FROM_VLB"] = this.form.gyoushaMeiFrom.Text;
            printHeaderRow["FH_GYOUSHA_CD_TO_VLB"] = this.form.gyoushaTo.Text;
            printHeaderRow["FH_GYOUSHA_NAME_TO_VLB"] = this.form.gyoushaMeiTo.Text;

            // 現場
            printHeaderRow["FH_GENBA_CD_FROM_VLB"] = this.form.genbaFrom.Text;
            printHeaderRow["FH_GENBA_NAME_FROM_VLB"] = this.form.genbaMeiFrom.Text;
            printHeaderRow["FH_GENBA_CD_TO_VLB"] = this.form.genbaTo.Text;
            printHeaderRow["FH_GENBA_NAME_TO_VLB"] = this.form.genbaMeiTo.Text;

            // コンテナ名
            printHeaderRow["FH_CONTENA_CD_FROM_VLB"] = this.form.contenaFrom.Text;
            printHeaderRow["FH_CONTENA_NAME_FROM_VLB"] = this.form.contenaMeiFrom.Text;
            printHeaderRow["FH_CONTENA_CD_FROM_TO_VLB"] = this.form.contenaTo.Text;
            printHeaderRow["FH_CONTENA_NAME_TO_VLB"] = this.form.contenaMeiTo.Text;

            // 日付文言
            if (this.form.outputSetting.Text.Equals(this.outputSetting_Hiduke))
            {
                string strHiduke = "日付";
                if (this.form.sousaKbn.Text.Equals(this.sousaKbn_secchi))
                {
                    strHiduke = "設置日";
                }
                else if (this.form.sousaKbn.Text.Equals(this.sousaKbn_hikiage))
                {
                    strHiduke = "引揚日";
                }
                else
                {
                    strHiduke = "設置・引揚日";
                }
                printHeaderRow["PH_SECCHI_DATE_FLB"] = strHiduke;
            }

            printHeaderData.Rows.Add(printHeaderRow);
            #endregion

            DataTable printDetailData = new DataTable();
            printDetailData = CreateDetailData(printDetailData, printData);

            // プリント
            string reportPath;
            if (this.contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                reportPath = LogicClass.KotaiKanriReportFullPathName;
            }
            else
            {
                reportPath = LogicClass.SuuryouKanriReportFullPathName;
            }

            ReportInfoR596 reportInfo = new ReportInfoR596(printHeaderData, printDetailData, this.form.outputSetting.Text, reportPath, this.contenaKanriHouhou);
            reportInfo.R594_Reprt();
            reportInfo.Title = "コンテナ履歴一覧表";

            FormReportPrintPopup report = new FormReportPrintPopup(reportInfo, "R596");
            report.PrintInitAction = 2;
            report.PrintXPS();
            report.Dispose();
        }
        #endregion

        #region DTO生成
        /// <summary>
        /// 画面の情報から検索用のDTOを作成する
        /// </summary>
        /// <returns>DTOClass</returns>
        private DTOClass CreateDto()
        {
            DTOClass dto = new DTOClass();
            dto.gyoushaGenbaSetting = this.form.gyoushaGenbaSetting.Text;
            dto.outputSetting = this.form.outputSetting.Text;
            dto.sousaKbn = this.form.sousaKbn.Text;

            if (!CommonConst.KYOTEN_CD_ZENSHA.ToString().Equals(this.form.kyoten.Text))
            {
                dto.kyotenCd = this.form.kyoten.Text;
            }

            var parentForm = (BusinessBaseForm)this.form.Parent;
            DateTime tempDate = parentForm.sysDate;
            if (this.form.denpyouHidukeFrom.Value != null
                && !string.IsNullOrEmpty(this.form.denpyouHidukeFrom.Value.ToString()))
            {
                tempDate = (DateTime)this.form.denpyouHidukeFrom.Value;
                dto.dateFrom = tempDate.ToShortDateString();
            }

            if (this.form.denpyouHidukeTo.Value != null
                && !string.IsNullOrEmpty(this.form.denpyouHidukeTo.Value.ToString()))
            {
                tempDate = (DateTime)this.form.denpyouHidukeTo.Value;
                dto.dateTo = tempDate.ToShortDateString();
            }

            dto.contenaShuruiFrom = this.form.contenaShuruiFrom.Text;
            dto.contenaShuruiTo = this.form.contenaShuruiTo.Text;
            dto.gyoushaFrom = this.form.gyoushaFrom.Text;
            dto.gyoushaTo = this.form.gyoushaTo.Text;
            dto.genbaFrom = this.form.genbaFrom.Text;
            dto.genbaTo = this.form.genbaTo.Text;
            if (this.form.contenaFrom.Enabled)
            {
                dto.contenaFrom = this.form.contenaFrom.Text;
            }

            if (this.form.contenaTo.Enabled)
            {
                dto.contenaTo = this.form.contenaTo.Text;
            }

            return dto;
        }
        #endregion

        #endregion

        #region コンテナ名制御
        /// <summary>
        /// コンテナ名整合性チェック
        /// </summary>
        /// <param name="contenaCD">入力されたコンテナCD</param>
        /// <param name="contenaName">略称名</param>
        /// <returns name="bool">TRUE:成功, FALSE:失敗</returns>
        internal bool checkContenaNameValidate(string contenaCD, ref string contenaName)
        {
            bool bRet = true;
            try
            {
                // コンテナ名は「個体」管理のみ
                if (this.contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
                {
                    // コンテナ種類CD、コンテナCDに紐付くCDを取得
                    var findEntity = new M_CONTENA();
                    findEntity.CONTENA_SHURUI_CD = this.form.contenaShuruiFrom.Text;
                    findEntity.CONTENA_CD = contenaCD;
                    findEntity.ISNOT_NEED_DELETE_FLG = true;
                    var entitys = this.contenaDao.GetAllValidData(findEntity);

                    if ((entitys != null) && (entitys.Length > 0))
                    {
                        // 存在した場合は略称名をセット
                        // ※キー検索のため存在するCDは唯一
                        contenaName = entitys[0].CONTENA_NAME_RYAKU;
                        bRet = true;
                    }
                    else
                    {
                        // 存在しないためエラー
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "コンテナ");
                        contenaName = string.Empty;
                        bRet = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkContenaNameValidate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                bRet = false;
            }
            return bRet;
        }

        /// <summary>
        /// コンテナ名Ctrlの状態チェック・更新
        /// </summary>
        internal void checkContenaNameCtrlStatus()
        {
            try
            {
                // コンテナ名有効状態チェック
                if (true == this.checkContenaNameEnabled())
                {
                    // コンテナ種類From == コンテナ種類To の場合はコンテナ名を有効化する
                    this.setContenaNameEnabled(true);

                    // データソース更新
                    this.setContenaNameDataSource(this.form.contenaShuruiFrom.Text);
                }
                else
                {
                    // コンテナ種類From != コンテナ種類To の場合はコンテナ名を無効化する
                    this.setContenaNameEnabled(false);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkContenaNameCtrlStatus", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// コンテナ名有効状態チェック
        /// </summary>
        /// <return name="bool">TRUE:有効, FALSE:無効</param>
        private bool checkContenaNameEnabled()
        {
            bool bRet = false;

            // コンテナ名は「個体」管理のみ
            if (this.contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                // コンテナ種類From, Toそれぞれセット
                var shuruiFrom = this.form.contenaShuruiFrom.Text;
                var shuruiTo = this.form.contenaShuruiTo.Text;

                if ((false == string.IsNullOrEmpty(shuruiFrom)) && (false == string.IsNullOrEmpty(shuruiTo)))
                {
                    if (true == shuruiFrom.Equals(shuruiTo))
                    {
                        // コンテナ種類From == コンテナ種類To の場合はコンテナ名有効
                        bRet = true;
                    }
                }
            }

            return bRet;
        }

        /// <summary>
        /// コンテナ名の状態制御
        /// </summary>
        /// <param name="bStatus">TRUE:有効, FALSE:無効</param>
        private void setContenaNameEnabled(bool bStatus)
        {
            // 有効/無効をセット
            this.form.contenaFrom.Enabled = bStatus;
            this.form.contenaMeiFrom.Enabled = bStatus;
            this.form.contenaFromSearch.Enabled = bStatus;
            this.form.contenaTo.Enabled = bStatus;
            this.form.contenaMeiTo.Enabled = bStatus;
            this.form.contenaToSearch.Enabled = bStatus;
        }

        /// <summary>
        /// コンテナ名のDataSourceを作成・格納する
        /// </summary>
        /// <param name="shuruiCD">コンテナ種類CD</param>
        private void setContenaNameDataSource(string shuruiCD)
        {
            // キーとなるコンテナ種類CDに紐付くCDを全取得
            var findEntity = new M_CONTENA();
            findEntity.CONTENA_SHURUI_CD = shuruiCD;
            var entitys = this.contenaDao.GetAllValidData(findEntity);

            if ((entitys != null) && (entitys.Length > 0))
            {
                // DataTableに変換
                var table = EntityUtility.EntityToDataTable(entitys);

                // 該当情報があればDataSourceのセット
                this.form.contenaFrom.PopupDataHeaderTitle = new string[] { "コンテナCD", "コンテナ名" };
                this.form.contenaFrom.PopupDataSource = table;
                this.form.contenaFromSearch.PopupDataHeaderTitle = new string[] { "コンテナCD", "コンテナ名" };
                this.form.contenaFromSearch.PopupDataSource = table;
                this.form.contenaTo.PopupDataSource = table;
                this.form.contenaTo.PopupDataHeaderTitle = new string[] { "コンテナCD", "コンテナ名" };
                this.form.contenaToSearch.PopupDataSource = table;
                this.form.contenaToSearch.PopupDataHeaderTitle = new string[] { "コンテナCD", "コンテナ名" };
            }
            else
            {
                // 該当情報が無ければDataSourceをクリア
                this.form.contenaFrom.PopupDataHeaderTitle = null;
                this.form.contenaFrom.PopupDataSource = null;
                this.form.contenaFromSearch.PopupDataSource = null;
                this.form.contenaFromSearch.PopupDataHeaderTitle = null;
                this.form.contenaTo.PopupDataSource = null;
                this.form.contenaTo.PopupDataHeaderTitle = null;
                this.form.contenaToSearch.PopupDataSource = null;
                this.form.contenaToSearch.PopupDataHeaderTitle = null;
            }
        }

        /// <summary>
        /// コンテナのFromとToに最小値と最大値をセットします
        /// </summary>
        internal bool SetContenaCdFromTo()
        {
            bool ret = true;
            try
            {
                // コンテナ名有効状態チェック
                if (true == this.checkContenaNameEnabled())
                {
                    // キーとなるコンテナ種類CDに紐付くCDを全取得
                    var findEntity = new M_CONTENA();
                    findEntity.CONTENA_SHURUI_CD = this.form.contenaShuruiFrom.Text;
                    findEntity.ISNOT_NEED_DELETE_FLG = true;
                    var entitys = this.contenaDao.GetAllValidData(findEntity);

                    if ((entitys != null) && (entitys.Length > 0))
                    {
                        // コンテナCDの最小値・最大値を取得
                        var minContena = entitys.Where(c => c.CONTENA_CD == entitys.Min(cy => cy.CONTENA_CD)).FirstOrDefault();
                        var maxContena = entitys.Where(c => c.CONTENA_CD == entitys.Max(cy => cy.CONTENA_CD)).FirstOrDefault();

                        // Fromに最小値のセット
                        if (true == string.IsNullOrEmpty(this.form.contenaFrom.Text))
                        {
                            this.form.contenaFrom.Text = minContena.CONTENA_CD;
                            this.form.contenaMeiFrom.Text = minContena.CONTENA_NAME_RYAKU;
                        }

                        // Toに最大値のセット
                        if (true == string.IsNullOrEmpty(this.form.contenaTo.Text))
                        {
                            this.form.contenaTo.Text = maxContena.CONTENA_CD;
                            this.form.contenaMeiTo.Text = maxContena.CONTENA_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetContenaCdFromTo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion

        #region 業者チェック
        /// <summary>
        /// 業者CD(From)チェック
        /// 空の場合、子の現場名をクリア
        /// </summary>
        internal void CheckGyoushaCdFrom()
        {
            if (this.form.gyoushaFrom.Text.Equals(this.beforeGyoushaCdFrom))
            {
                return;
            }

            if (string.IsNullOrEmpty(this.form.gyoushaFrom.Text)
                || !this.form.gyoushaFrom.Text.Equals(this.beforeGyoushaCdFrom))
            {
                // 子をクリア
                this.form.genbaFrom.Text = string.Empty;
                this.form.genbaMeiFrom.Text = string.Empty;
                this.form.genbaTo.Text = string.Empty;
                this.form.genbaMeiTo.Text = string.Empty;
            }
        }
        #endregion

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141127 teikyou ダブルクリックを追加する　start
        //日付範囲のダブルクリック
        private void denpyouHidukeTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var denpyouHidukeFromTextBox = this.form.denpyouHidukeFrom;
            var denpyouHidukeToTextBox = this.form.denpyouHidukeTo;
            denpyouHidukeToTextBox.Text = denpyouHidukeFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        //コンテナ種類のダブルクリック
        private void contenaShuruiTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var contenaShuruiFromTextBox = this.form.contenaShuruiFrom;
            var contenaShuruiToTextBox = this.form.contenaShuruiTo;
            var contenaShuruiMeiFromTextBox = this.form.contenaShuruiMeiFrom;
            var contenaShuruiMeiToTextBox = this.form.contenaShuruiMeiTo;
            contenaShuruiToTextBox.Text = contenaShuruiFromTextBox.Text;
            contenaShuruiMeiToTextBox.Text = contenaShuruiMeiFromTextBox.Text;


            LogUtility.DebugMethodEnd();
        }
        //業者のダブルクリック
        private void gyoushaTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaFromTextBox = this.form.gyoushaFrom;
            var gyoushaToTextBox = this.form.gyoushaTo;
            var gyoushaMeiFromTextBox = this.form.gyoushaMeiFrom;
            var gyoushaMeiToTextBox = this.form.gyoushaMeiTo;

            gyoushaToTextBox.Text = gyoushaFromTextBox.Text;
            gyoushaMeiToTextBox.Text = gyoushaMeiFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        //現場のダブルクリック
        private void genbaTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var genbaFromTextBox = this.form.genbaFrom;
            var genbaToTextBox = this.form.genbaTo;
            var genbaMeiFromTextBox = this.form.genbaMeiFrom;
            var genbaMeiToTextBox = this.form.genbaMeiTo;
            genbaToTextBox.Text = genbaFromTextBox.Text;
            genbaMeiToTextBox.Text = genbaMeiFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        //コンテナ名のダブルクリック
        private void contenaTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var contenaFromTextBox = this.form.contenaFrom;
            var contenaToTextBox = this.form.contenaTo;
            var contenaMeiFromTextBox = this.form.contenaMeiFrom;
            var contenaMeiToTextBox = this.form.contenaMeiTo;
            contenaToTextBox.Text = contenaFromTextBox.Text;
            contenaMeiToTextBox.Text = contenaMeiFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141127 teikyou ダブルクリックを追加する　end
        #endregion

        /// 20141203 Houkakou 「コンテナ履歴一覧表」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            bool isErr = false;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                this.form.denpyouHidukeFrom.BackColor = Constans.NOMAL_COLOR;
                this.form.denpyouHidukeTo.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.denpyouHidukeFrom.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.denpyouHidukeTo.Text))
                {
                    return isErr;
                }

                DateTime date_from = Convert.ToDateTime(this.form.denpyouHidukeFrom.Value);
                DateTime date_to = Convert.ToDateTime(this.form.denpyouHidukeTo.Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.denpyouHidukeFrom.IsInputErrorOccured = true;
                    this.form.denpyouHidukeTo.IsInputErrorOccured = true;
                    this.form.denpyouHidukeFrom.BackColor = Constans.ERROR_COLOR;
                    this.form.denpyouHidukeTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "日付範囲From", "日付範囲To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.denpyouHidukeFrom.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }
        #endregion

        #region denpyouHidukeFrom_Leaveイベント
        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void denpyouHidukeFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.denpyouHidukeTo.Text))
            {
                this.form.denpyouHidukeTo.IsInputErrorOccured = false;
                this.form.denpyouHidukeTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region denpyouHidukeTo_Leaveイベント
        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void denpyouHidukeTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.denpyouHidukeFrom.Text))
            {
                this.form.denpyouHidukeFrom.IsInputErrorOccured = false;
                this.form.denpyouHidukeFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「コンテナ履歴一覧表」の日付チェックを追加する　end

        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        #region 現場CDチェック
        /// <summary>
        /// 現場CDエラーチェック
        /// </summary>
        /// <returns></returns>
        internal bool ErrorCheckGenba(string gyoushaCd, string genbaCd, int fromToFlg)
        {
            bool ren = true;
            try
            {
                // 業者入力されてない場合
                if (String.IsNullOrEmpty(gyoushaCd) && !String.IsNullOrEmpty(genbaCd))
                {
                    if (fromToFlg == 1)
                    {
                        this.msgLogic.MessageBoxShow("E051", "業者");
                        this.form.genbaFrom.Text = String.Empty;
                        this.form.genbaMeiFrom.Text = String.Empty;
                        return false;
                    }
                    else if (fromToFlg == 2)
                    {
                        this.msgLogic.MessageBoxShow("E051", "業者");
                        this.form.genbaTo.Text = String.Empty;
                        this.form.genbaMeiTo.Text = String.Empty;
                        return false;
                    }
                }

                if (!String.IsNullOrEmpty(gyoushaCd))
                {
                    // 現場情報を取得
                    if (!string.IsNullOrEmpty(genbaCd))
                    {
                        bool catchErr = false;
                        var genab = this.GetGenba(gyoushaCd, genbaCd, out catchErr);
                        if (catchErr) { return false; }
                        if (genab.Count() == 0)
                        {
                            // マスタに現場が存在しない場合
                            // 現場の関連情報をクリア
                            if (fromToFlg == 1)
                            {
                                this.form.genbaFrom.Text = String.Empty;
                                this.form.genbaMeiFrom.Text = String.Empty;
                                this.msgLogic.MessageBoxShow("E020", "現場");
                                return false;
                            }
                            else if (fromToFlg == 2)
                            {
                                this.form.genbaTo.Text = String.Empty;
                                this.form.genbaMeiTo.Text = String.Empty;
                                this.msgLogic.MessageBoxShow("E020", "現場");
                                return false;
                            }
                        }
                        else
                        {
                            // 現場名を取得
                            if (fromToFlg == 1)
                            {
                                this.form.genbaMeiFrom.Text = genab[0].GENBA_NAME_RYAKU;
                            }
                            else if (fromToFlg == 2)
                            {
                                this.form.genbaMeiTo.Text = genab[0].GENBA_NAME_RYAKU;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ErrorCheckGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ren = false;
            }
            return ren;
        }

        /// <summary>
        /// 現場CDで現場リストを取得します
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <returns>現場エンティティリスト</returns>
        public M_GENBA[] GetGenba(string gyoushaCd, string genbaCd, out bool catchErr)
        {
            catchErr = false;
            M_GENBA[] returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                // 取得済みの現場リストから取得
                var gList = this.genbaList.Where(g => g.GYOUSHA_CD == gyoushaCd && g.GENBA_CD == genbaCd);
                if (gList.Count() == 0)
                {
                    // なければDBから取得
                    var keyEntity = new M_GENBA();
                    keyEntity.GENBA_CD = genbaCd;
                    keyEntity.GYOUSHA_CD = gyoushaCd;
                    keyEntity.ISNOT_NEED_DELETE_FLG = true;
                    var genbaEntities = this.genbaDao.GetAllValidData(keyEntity);
                    if (null != genbaEntities)
                    {
                        this.genbaList.AddRange(genbaEntities);
                        gList = genbaEntities;
                    }
                }
                returnVal = gList.ToArray();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }
        #endregion
        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// 明細部データを作り
        /// </summary>
        /// <param name="printDetailData"></param>
        /// <param name="printData"></param>
        /// <returns></returns>
        private DataTable CreateDetailData(DataTable printDetailData, SearchResultDTO[] printData)
        {
            /**
             * 明細部
             */
            // 出力内容からソートを掛けて出力(グルーピングはC1Reportに任せる)
            if (this.contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                // 個体管理
                switch (this.form.outputSetting.Text)
                {
                    case "1":
                        // 日付別
                        printData = this.searchReslut.
                                            OrderBy(o => o.SECCHI_DATE).
                                            ThenBy(o => o.GYOUSHA_CD).
                                            ThenBy(o => o.GENBA_CD).
                                            ThenBy(o => o.CONTENA_SHURUI_CD).ToArray();
                        break;

                    case "2":
                        // コンテナ種類別
                        printData = this.searchReslut.
                                            OrderBy(o => o.CONTENA_SHURUI_CD).
                                            ThenBy(o => o.SECCHI_DATE).
                                            ThenBy(o => o.GYOUSHA_CD).
                                            ThenBy(o => o.GENBA_CD).ToArray();
                        break;

                    case "3":
                        printData = this.searchReslut.
                                            OrderBy(o => o.CONTENA_SHURUI_CD).
                                            ThenBy(o => o.CONTENA_CD).
                                            ThenBy(o => o.SECCHI_DATE).
                                            ThenBy(o => o.GYOUSHA_CD).
                                            ThenBy(o => o.GENBA_CD).ToArray();
                        break;

                    case "4":
                        // 業者別
                        printData = this.searchReslut.
                                            OrderBy(o => o.GYOUSHA_CD).
                                            ThenBy(o => o.SECCHI_DATE).
                                            ThenBy(o => o.CONTENA_SHURUI_CD).
                                            ThenBy(o => o.GENBA_CD).ToArray();
                        break;

                    case "5":
                        // 現場別
                        printData = this.searchReslut.
                                            OrderBy(o => o.GYOUSHA_CD).
                                            ThenBy(o => o.GENBA_CD).
                                            ThenBy(o => o.SECCHI_DATE).
                                            ThenBy(o => o.CONTENA_SHURUI_CD).ToArray();
                        break;
                }
            }
            else
            {
                // 数量管理
                switch (this.form.outputSetting.Text)
                {
                    case "1":
                        // 日付別
                        printData = this.searchReslut.
                                            OrderBy(o => o.SECCHI_DATE).
                                            ThenBy(o => o.GYOUSHA_CD).
                                            ThenBy(o => o.GENBA_CD).
                                            ThenBy(o => o.CONTENA_SHURUI_CD).ToArray();
                        break;

                    case "2":
                        // コンテナ種類別
                        printData = this.searchReslut.
                                            OrderBy(o => o.CONTENA_SHURUI_CD).
                                            ThenBy(o => o.SECCHI_DATE).
                                            ThenBy(o => o.GYOUSHA_CD).
                                            ThenBy(o => o.GENBA_CD).ToArray();
                        break;

                    case "3":
                        // 業者別
                        printData = this.searchReslut.
                                            OrderBy(o => o.GYOUSHA_CD).
                                            ThenBy(o => o.SECCHI_DATE).
                                            ThenBy(o => o.CONTENA_SHURUI_CD).
                                            ThenBy(o => o.GENBA_CD).ToArray();
                        break;

                    case "4":
                        // 現場別
                        printData = this.searchReslut.
                                            OrderBy(o => o.GYOUSHA_CD).
                                            ThenBy(o => o.GENBA_CD).
                                            ThenBy(o => o.SECCHI_DATE).
                                            ThenBy(o => o.CONTENA_SHURUI_CD).ToArray();
                        break;
                }
            }

            // 明細部カラム追加
            printDetailData.Columns.Add("SECCHI_DATE");
            printDetailData.Columns.Add("CONTENA_SOUSA");
            printDetailData.Columns.Add("GYOUSHA_CD");
            printDetailData.Columns.Add("GYOUSHA_NAME");
            printDetailData.Columns.Add("GENBA_CD");
            printDetailData.Columns.Add("GENBA_NAME");
            printDetailData.Columns.Add("CONTENA_SHURUI_CD");
            printDetailData.Columns.Add("CONTENA_SHURUI_NAME");
            printDetailData.Columns.Add("UPDATE_DATE");

            // 個体管理、数量管理で切り分け
            if (this.contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                printDetailData.Columns.Add("CONTENA_CD");
                printDetailData.Columns.Add("CONTENA_NAME");
            }
            else
            {
                printDetailData.Columns.Add("SUURYOU");
            }

            // 印刷データセット
            foreach (var row in printData)
            {
                DataRow printDetailRow = printDetailData.NewRow();
                printDetailRow["SECCHI_DATE"] = row.SECCHI_DATE;
                string contenaSousa = string.Empty;
                switch (row.CONTENA_SET_KBN)
                {
                    case 1:
                        contenaSousa = "設置";
                        break;

                    case 2:
                        contenaSousa = "引揚";
                        break;
                }
                printDetailRow["CONTENA_SOUSA"] = contenaSousa;
                printDetailRow["GYOUSHA_CD"] = row.GYOUSHA_CD;
                printDetailRow["GYOUSHA_NAME"] = row.GYOUSHA_NAME_RYAKU;
                printDetailRow["GENBA_CD"] = row.GENBA_CD;
                printDetailRow["GENBA_NAME"] = row.GENBA_NAME_RYAKU;
                printDetailRow["CONTENA_SHURUI_CD"] = row.CONTENA_SHURUI_CD;
                printDetailRow["CONTENA_SHURUI_NAME"] = row.CONTENA_SHURUI_NAME_RYAKU;
                if (!row.UPDATE_DATE.IsNull)
                {
                    printDetailRow["UPDATE_DATE"] = row.UPDATE_DATE.ToString();
                }

                // 個体管理、数量管理で切り分け
                if (this.contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
                {
                    printDetailRow["CONTENA_CD"] = row.CONTENA_CD;
                    printDetailRow["CONTENA_NAME"] = row.CONTENA_NAME_RYAKU;
                }
                else
                {
                    printDetailRow["SUURYOU"] = row.DAISUU_CNT;
                }

                printDetailData.Rows.Add(printDetailRow);
            }
            return printDetailData;
        }
    }
}
