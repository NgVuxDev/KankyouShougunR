using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Report;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.PaperManifest.InsatsuBusuSettei;
using Shougun.Core.PaperManifest.InsatsuBusuSettei.Const;
using System.Numerics;
using Shougun.Core.Common.BusinessCommon.Dto;
using System.Data.SqlTypes;
using r_framework.CustomControl;
using r_framework.Configuration; //BigInteger用

namespace Shougun.Core.PaperManifest.InsatsuBusuSettei.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class InsatsuBusuSetteiLogic
    {
        #region フィールド

        /// <summary>
        /// 交付番号検索Dao
        /// </summary>
        private GetKoufuDaoCls GetKoufuDao;

        /// <summary>
        /// Form
        /// </summary>
        private InsatsuBusuSettei form;

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 共通
        /// </summary>
        ManifestoLogic mlogic = null;

        /// <summary>単位一覧</summary>
        private M_UNIT[] unitData;

        /// <summary>車輛一覧</summary>
        private M_SHARYOU[] sharyouData;

        /// <summary>車種一覧</summary>
        private M_SHASHU[] shashuData;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InsatsuBusuSetteiLogic(InsatsuBusuSettei targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            try
            {
                this.form = targetForm;

                this.GetKoufuDao = DaoInitUtility.GetComponent<GetKoufuDaoCls>();

                this.unitData = DaoInitUtility.GetComponent<IM_UNITDao>().GetAllData();
                this.sharyouData = DaoInitUtility.GetComponent<IM_SHARYOUDao>().GetAllData();
                this.shashuData = DaoInitUtility.GetComponent<IM_SHASHUDao>().GetAllData();

                this.mlogic = new ManifestoLogic();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd(targetForm);
        }

        #region 初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //ボタンの初期化
                this.ButtonInit();

                //Headerの初期化
                this.SetHeaderText();

                //コントロール初期化
                this.ControlInit();

                this.form.retDto = new ItakuErrorDTO();
                //ロジック初期化
                this.msgLogic = new MessageBoxShowLogic();

                //交付番号とラジオボタン
                this.SetupKoufuNo(this.form.Entrylist[0].KOUFU_KBN, this.form.txt_KoufuNo);
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        private void ControlInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.form.txt_KoufuNo.Text = string.Empty;

                //部数の初期値を設定する
                if (Properties.Settings.Default.insatsuMode.Equals(Const.ConstCls.INSATST_MODE_TANHYOU))
                {
                    this.form.txt_InsatsuBusu.Text = Const.ConstCls.INSATSU_BUSU_DEFAULT;
                }
                else
                {
                    this.form.txt_InsatsuBusu.Text = string.Empty;
                }

                this.form.cbx_ManifestToroku.Checked = false;
                if (Properties.Settings.Default.syoriMode.Equals((int)WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    //新規モード
                    this.form.txt_KoufuNo.ReadOnly = false; //2013.11.23 naitou update 入力制限をReadOnlyで設定
                    this.form.txt_KoufuNo.TabStop = true;   //2013.11.23 naitou update 入力制限をReadOnlyで設定
                    this.form.cbx_ManifestToroku.Checked = true;
                }
                else
                {
                    // 新規以外
                    this.form.txt_KoufuNo.ReadOnly = true; //2013.11.23 naitou update 入力制限をReadOnlyで設定
                    this.form.txt_KoufuNo.TabStop = false; //2013.11.23 naitou update 入力制限をReadOnlyで設定
                }

                //遷移元の交付番号を表示
                if (this.form.Entrylist != null && this.form.Entrylist.Count > 0)
                {
                    this.form.txt_KoufuNo.Text = this.form.Entrylist[0].MANIFEST_ID;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Headerの初期化を行う
        /// </summary>
        private void SetHeaderText()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //ヘッダを設定する
                this.form.lb_title.Text = Const.ConstCls.FORM_HEADER_TITLE;

                // Formタイトルの設定
                this.form.Text = Const.ConstCls.FORM_HEADER_TITLE;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.form.bt_func1.Text = Const.ConstCls.BUTTON_PRINT_NAME;
                this.form.bt_func4.Text = Const.ConstCls.BUTTON_TOROKU_NAME;
                this.form.bt_func9.Text = Const.ConstCls.BUTTON_JIKKOU_NAME;
                this.form.bt_func12.Text = Const.ConstCls.BUTTON_CLOSE_NAME;
                this.form.bt_func11.Text = Const.ConstCls.BUTTON_OPEN_PRINT_NAME;

                this.form.bt_func1.Enabled = true;
                this.form.bt_func12.Enabled = true;
                if (Properties.Settings.Default.syoriMode.Equals((int)WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    this.form.bt_func4.Enabled = true;
                    this.form.bt_func9.Enabled = true;
                }
                else
                {
                    this.form.bt_func4.Enabled = false;
                    this.form.bt_func9.Enabled = false;
                }

                if (!AppConfig.IsTerminalMode)
                {
                    this.form.bt_func11.Enabled = true;
                }
                else
                {
                    this.form.bt_func11.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //印刷登録ボタン(F9)イベント生成
                this.form.bt_func9.Click += new EventHandler(this.form.Jikkou);

                //取消ボタン(F11)イベント生成
                this.form.bt_func12.Click += new EventHandler(this.form.FormClose);

                this.form.txt_InsatsuBusu.KeyUp += new KeyEventHandler(this.form.ControlKeyUp);

                //印刷ボタン(F11)イベント生成
                this.form.bt_func1.Click += new EventHandler(this.form.Print);

                //登録ボタン(F11)イベント生成
                this.form.bt_func4.Click += new EventHandler(this.form.Toroku);

                this.form.bt_func11.Click += new EventHandler(this.form.OpenPrintSettingGamen);

                this.form.txt_KoufuNo.KeyUp += new KeyEventHandler(this.form.ControlKeyUp);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 実行処理
        /// <summary>
        /// 実行処理
        /// </summary>
        internal ConstCls.JikkouResult Jikkou()
        {
            var result = ConstCls.JikkouResult.YES;
            try
            {
                LogUtility.DebugMethodStart();

                // 20141031 koukouei 委託契約チェック start
                if (this.form.cbx_ManifestToroku.Checked)
                {
                    if (!this.CheckBlank() || (this.form.ManiFlag == 1 && !this.CheckItakukeiyaku()))
                    {
                        result = ConstCls.JikkouResult.NO;
                        LogUtility.DebugMethodEnd(result);
                        return result;
                    }
                }
                // 20141031 koukouei 委託契約チェック end

                //確認メッセージを表示する
                bool isPrint = true;
                if (this.msgLogic.MessageBoxShow("C042").Equals(DialogResult.No))
                {
                    isPrint = false;
                }

                // 印刷部数が100部以上の場合、確認のポップアップを出す（初期フォーカスは「いいえ」）
                int busuu = 0;
                if (int.TryParse(this.form.txt_InsatsuBusu.Text, out busuu))
                {
                    if (busuu >= 100)
                    {
                        if (this.msgLogic.MessageBoxShow("C091", MessageBoxDefaultButton.Button2, "100枚以上のマニフェスト") == DialogResult.No)
                        {
                            this.form.txt_InsatsuBusu.Focus();
                            this.form.txt_InsatsuBusu.SelectAll();
                            this.form.txt_InsatsuBusu.IsInputErrorOccured = true;
                            result = ConstCls.JikkouResult.NO;
                            LogUtility.DebugMethodEnd(result);
                            return result;
                        }
                    }
                }

                // 印刷部数
                int insatsuBusu = Convert.ToInt32(this.form.txt_InsatsuBusu.Text);

                // 新規モードかつマニ登録するかどうか
                bool isNewEntryMode = Properties.Settings.Default.syoriMode.Equals((int)WINDOW_TYPE.NEW_WINDOW_FLAG);
                if (isNewEntryMode && this.form.cbx_ManifestToroku.Checked)
                {
                    // マニフェスト登録(重複登録チェック)
                    if (Regist(true, insatsuBusu))
                    {
                        result = ConstCls.JikkouResult.DUPLICATION;
                        LogUtility.DebugMethodEnd(result);
                        return result;
                    }
                }

                // 印刷用データ作成
                if (isPrint)
                {
                    ReportInfoBase reportInfo = null;
                    var insatsuMode = (ConstCls.InsatsuMode)Properties.Settings.Default.insatsuMode;
                    var dentaneMode = (ConstCls.enumFormKbn)Properties.Settings.Default.dentaneMode;
                    switch (dentaneMode)
                    {
                        case ConstCls.enumFormKbn.Chokkou:
                            {
                                if (this.form.manifest_mercury_check)
                                {
                                    //直行水銀の場合
                                    var dt = this.CreateChokkouMercuryManiPrintingData();
                                    var reportR691 = new ReportInfoR691();
                                    reportR691.OutputFormFullPathName =
                                        this.GetTemplateFileName(ConstCls.enumFormKbn.Chokkou, insatsuMode);
                                    reportR691.R691_Report(dt);
                                    reportR691.Title = "産廃（直行）水銀マニ";
                                    reportInfo = reportR691;
                                }
                                else
                                {
                                    //直行の場合
                                    var dt = this.CreateChokkouManiPrintingData();
                                    var reportR493 = new ReportInfoR493();
                                    reportR493.OutputFormFullPathName =
                                        this.GetTemplateFileName(ConstCls.enumFormKbn.Chokkou, insatsuMode);
                                    reportR493.R493_Report(dt);
                                    reportR493.Title = "産廃（直行）マニフェスト";
                                    reportInfo = reportR493;
                                }
                            }
                            break;
                        case ConstCls.enumFormKbn.Kenhai:
                            {
                                if (this.form.manifest_mercury_check)
                                {
                                    //建廃水銀の場合
                                    var dt = this.CreateKenpaiMercuryManiPrintingData();
                                    // インスタンス追加
                                    var reportR692 = new ReportInfoR692();
                                    reportR692.OutputFormFullPathName =
                                        this.GetTemplateFileName(ConstCls.enumFormKbn.Kenhai, insatsuMode);
                                    reportR692.R692_Report(dt);
                                    reportR692.Title = "建廃水銀マニ";
                                    reportInfo = reportR692;
                                }
                                else
                                {
                                    //建廃の場合
                                    var dt = this.CreateKenpaiManiPrintingData();
                                    // インスタンス追加
                                    var reportR495 = new ReportInfoR495();
                                    reportR495.OutputFormFullPathName =
                                        this.GetTemplateFileName(ConstCls.enumFormKbn.Kenhai, insatsuMode);
                                    reportR495.R495_Report(dt);
                                    reportR495.Title = "建廃マニフェスト";
                                    reportInfo = reportR495;
                                }
                                
                            }
                            break;
                        case ConstCls.enumFormKbn.Sekitai:
                            {
                                //積替の場合
                                var dt = this.CreateSekitaiManiPrintingData();
                                var reportR494 = new ReportInfoR494();
                                reportR494.OutputFormFullPathName =
                                    this.GetTemplateFileName(ConstCls.enumFormKbn.Sekitai, insatsuMode);
                                reportR494.R494_Report(dt);
                                reportR494.Title = "積替マニフェスト";
                                reportInfo = reportR494;
                            }
                            break;
                    }

                    //印刷処理
                    if (reportInfo != null)
                    {
                        var printPopup = new FormReportPrintPopup(reportInfo, reportInfo.ReportID);
                        if (printPopup != null)
                        {
                            printPopup.IsManifestReport = true;
                            printPopup.IsTanpyou = (insatsuMode == ConstCls.InsatsuMode.Single);
                            printPopup.Copie = insatsuBusu;
                            printPopup.PrintInitAction = 1;    // 印刷アプリ初期動作(直印刷)
                            printPopup.PrintXPS();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Jikkou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                result = ConstCls.JikkouResult.ERROR;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
            return result;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag">エラーフラグ</param>
        /// <param name="dt">データテーブル</param>
        [Transaction]
        public virtual bool Regist(bool errorFlag, int insatsuBusu)
        {
            LogUtility.DebugMethodStart(errorFlag, insatsuBusu);

            try
            {
                if (!errorFlag)
                {
                    LogUtility.DebugMethodEnd(errorFlag);
                    return true;
                }

                List<T_MANIFEST_ENTRY> entrylist = this.form.Entrylist;
                List<T_MANIFEST_UPN> upnlist = this.form.Upnlist;
                List<T_MANIFEST_PRT> prtlist = this.form.Prtlist;
                List<T_MANIFEST_DETAIL_PRT> detailprtlist = this.form.Detailprtlist;
                List<T_MANIFEST_DETAIL> detaillist = this.form.Detaillist;
                List<T_MANIFEST_RET_DATE> retdatelist = this.form.Retdatelist;
                List<T_MANIFEST_KP_KEIJYOU> keijyoulist = this.form.Keijyoulist;
                List<T_MANIFEST_KP_NISUGATA> nisugatalist = this.form.Nisugatalist;
                List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist = this.form.Houhoulist;

                //交付番号の重複チェック
                GetKoufuDtoCls serch = new GetKoufuDtoCls();
                DataTable chkdt = new DataTable();

                //データ更新
                using (Transaction tran = new Transaction())
                {
                    //交付番号を設定
                    string koufuNo = this.form.txt_KoufuNo.Text;

                    //３６進かどうか //IとOの入力はNGのため36でない
                    bool is36 = false;

                    //0埋めする桁
                    int keta = koufuNo.Length;

                    //CDより前を数値化した値(例外はCD無し）
                    BigInteger startKoufuNo;
                    if (System.Text.RegularExpressions.Regex.IsMatch(this.form.txt_KoufuNo.Text, "^[0-9]+$"))
                    {
                        if (this.form.KoufuKbn.Equals("1"))
                        {
                            startKoufuNo = Convert.ToInt64(koufuNo.Substring(0, 10));
                        }
                        else
                        {
                            //特殊の場合11ケタ全部使う
                            startKoufuNo = BigInteger.Parse(koufuNo);
                        }
                    }
                    else
                    {
                        startKoufuNo = this.MyConvertTo10(koufuNo, 36); //36進数で変換
                        is36 = true;
                    }

                    for (int i = 0; i < insatsuBusu; i++)
                    {

                        //交付区分が1(通常)の場合
                        if (this.form.KoufuKbn.Equals("1"))
                        {

                            string noCD = (startKoufuNo + i).ToString().PadLeft(10, '0') + "0";

                            string CD = ManifestoLogic.ChkDigitKoufuNo(noCD);

                            if (string.IsNullOrEmpty(CD))
                            {
                                koufuNo = noCD; //CDが0の場合
                            }
                            else
                            {
                                koufuNo = noCD.Substring(0,10) +  CD ; //CDが0以外の場合
                            }
                        }
                        else
                        {
                            //交付区分が2(例外)の場合
                            if (is36)
                            {
                                koufuNo = this.MyConvertTo36(startKoufuNo + (BigInteger)i, 36).PadLeft(keta, '0');
                            }
                            else
                            {
                                koufuNo = (startKoufuNo + i).ToString().PadLeft(keta, '0');
                            }
                        }

                        this.SetKoufubangoAndSystemId(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref detaillist,
                            ref retdatelist, ref keijyoulist, ref nisugatalist, ref houhoulist, koufuNo);

                        //重複チェック
                        if (entrylist[0].MANIFEST_ID != string.Empty)
                        {
                            serch.MANIFEST_ID = entrylist[0].MANIFEST_ID;
                            serch.HAIKI_KBN_CD = entrylist[0].HAIKI_KBN_CD;
                            chkdt = this.SerchKoufuNo(serch);
                            if (chkdt.Rows.Count > 0)
                            {
                                this.msgLogic.MessageBoxShow("E031", "交付番号");
                                this.form.txt_KoufuNo.Focus();
                                LogUtility.DebugMethodEnd(errorFlag);
                                return true;
                            }
                        }

                        mlogic.Insert(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, nisugatalist, houhoulist,
                           detaillist, retdatelist);
                    }

                    tran.Commit();
                }

                //更新後は完了メッセージを表示する
                msgLogic.MessageBoxShow("I001", "登録");
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            LogUtility.DebugMethodEnd(errorFlag);

            return false;
        }

        /// <summary>
        /// テンプレートファイルパスの取得
        /// </summary>
        /// <param name="kbn">マニフェスト種類(直行/積替/建廃)</param>
        /// <param name="insatsu">印刷モード</param>
        /// <returns></returns>
        private string GetTemplateFileName(ConstCls.enumFormKbn kbn, ConstCls.InsatsuMode insatsu)
        {
            LogUtility.DebugMethodStart(kbn, insatsu);

            string templateFile = String.Empty;

            string printMode = String.Empty;
            string terminal = String.Empty;

            //if (insatsu == ConstCls.InsatsuMode.Multi)
            //{
            //    // 連票
            //    printMode = "_2";
            //}

            //// マニ帳票はクラウド用テンプレートファイル名の末尾を「C」とする
            //if (r_framework.Dto.SystemProperty.IsTerminalMode)
            //{
            //    terminal = "C";
            //}

            if (kbn == ConstCls.enumFormKbn.Chokkou)
            {
                if (this.form.manifest_mercury_check)
                {
                    //直行水銀の場合
                    templateFile = String.Format("Template/R691{0}-Form{1}.xml", printMode, terminal);
                }
                else
                {
                    //直行の場合
                    templateFile = String.Format("Template/R493{0}-Form{1}.xml", printMode, terminal);
                }
            }
            else if (kbn == ConstCls.enumFormKbn.Kenhai)
            {
                if (this.form.manifest_mercury_check)
                {
                    //建廃水銀の場合
                    templateFile = String.Format("Template/R692{0}-Form{1}.xml", printMode, terminal);
                }
                else
                {
                    //建廃の場合
                    templateFile = String.Format("Template/R495{0}-Form{1}.xml", printMode, terminal);
                } 
            }
            else if (kbn == ConstCls.enumFormKbn.Sekitai)
            {
                //積替の場合
                templateFile = String.Format("Template/R494{0}-Form{1}.xml", printMode, terminal);
            }

            LogUtility.DebugMethodEnd(templateFile);
            return templateFile;
        }

        /// <summary>
        /// 登録用にSYSTEMIDと交付番号を設定する
        /// </summary>
        /// <param name="entrylist">マニフェスト</param>
        /// <param name="upnlist">マニ収集運搬</param>
        /// <param name="prtlist">マニ印字</param>
        /// <param name="detailprtlist">マニ印字明細</param>
        /// <param name="detaillist">マニ明細</param>
        /// <param name="retdatelist">マニ返却日</param>
        /// <param name="keijyoulist">マニ印字_建廃_形状</param>
        /// <param name="nisugatalist">マニ印字_建廃_荷姿</param>
        /// <param name="houhoulist">マニ印字_建廃_処分方法</param>
        /// <param name="koufuNo">交付番号</param>
        public void SetKoufubangoAndSystemId(ref List<T_MANIFEST_ENTRY> entrylist
            , ref List<T_MANIFEST_UPN> upnlist
            , ref List<T_MANIFEST_PRT> prtlist
            , ref List<T_MANIFEST_DETAIL_PRT> detailprtlist
            , ref List<T_MANIFEST_DETAIL> detaillist
            , ref List<T_MANIFEST_RET_DATE> retdatelist
            , ref List<T_MANIFEST_KP_KEIJYOU> keijyoulist
            , ref List<T_MANIFEST_KP_NISUGATA> nisugatalist
            , ref List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist
            , string koufuNo)
        {
            LogUtility.DebugMethodStart(entrylist, upnlist, prtlist, detailprtlist, detaillist, retdatelist
                , keijyoulist, nisugatalist, houhoulist, koufuNo);

            long lSysId = 0;
            int iSeq = 0;

            Common.BusinessCommon.DBAccessor dba = null;
            dba = new Common.BusinessCommon.DBAccessor();

            lSysId = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);

            this.form.SystemId = lSysId; // 最後のシステムIDを画面に渡す（登録後に修正で呼び出すため）

            iSeq++;

            //データ更新
            if (entrylist != null && entrylist.Count() > 0)
            {
                for (int i = 0; i < entrylist.Count; i++)
                {
                    // システムID 
                    entrylist[i].SYSTEM_ID = lSysId;
                    // 枝番 
                    entrylist[i].SEQ = iSeq;
                    // 交付番号 
                    entrylist[i].MANIFEST_ID = koufuNo;
                }
            }

            //マニ収集運搬
            if (upnlist != null && upnlist.Count() > 0)
            {
                foreach (T_MANIFEST_UPN upn in upnlist)
                {
                    // システムID 
                    upn.SYSTEM_ID = lSysId;
                    // 枝番 
                    upn.SEQ = iSeq;
                }
            }

            //マニ印字
            if (prtlist != null && prtlist.Count() > 0)
            {
                foreach (T_MANIFEST_PRT prt in prtlist)
                {
                    // システムID 
                    prt.SYSTEM_ID = lSysId;
                    // 枝番 
                    prt.SEQ = iSeq;
                }
            }

            //マニ印字明細
            if (detailprtlist != null && detailprtlist.Count() > 0)
            {
                foreach (T_MANIFEST_DETAIL_PRT detailprt in detailprtlist)
                {
                    // システムID 
                    detailprt.SYSTEM_ID = lSysId;
                    // 枝番 
                    detailprt.SEQ = iSeq;
                }
            }

            //マニ印字_建廃_形状
            if (keijyoulist != null && keijyoulist.Count() > 0)
            {
                foreach (T_MANIFEST_KP_KEIJYOU keijyou in keijyoulist)
                {
                    // システムID 
                    keijyou.SYSTEM_ID = lSysId;
                    // 枝番 
                    keijyou.SEQ = iSeq;
                }
            }

            //マニ印字_建廃_荷姿
            if (nisugatalist != null && nisugatalist.Count() > 0)
            {
                foreach (T_MANIFEST_KP_NISUGATA nisugata in nisugatalist)
                {
                    // システムID 
                    nisugata.SYSTEM_ID = lSysId;
                    // 枝番 
                    nisugata.SEQ = iSeq;
                }
            }

            //マニ印字_建廃_処分方法
            if (houhoulist != null && houhoulist.Count() > 0)
            {
                foreach (T_MANIFEST_KP_SBN_HOUHOU houhou in houhoulist)
                {
                    // システムID 
                    houhou.SYSTEM_ID = lSysId;
                    // 枝番 
                    houhou.SEQ = iSeq;
                }
            }

            //マニ明細
            dba = new Common.BusinessCommon.DBAccessor();

            if (detaillist != null && detaillist.Count() > 0)
            {
                foreach (T_MANIFEST_DETAIL detail in detaillist)
                {
                    // システムID 
                    detail.SYSTEM_ID = lSysId;
                    // 枝番 
                    detail.SEQ = iSeq;
                    //明細システムID
                    detail.DETAIL_SYSTEM_ID = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);
                }
            }

            //マニ返却日
            if (retdatelist != null && retdatelist.Count() > 0)
            {
                foreach (T_MANIFEST_RET_DATE retdate in retdatelist)
                {
                    // システムID 
                    retdate.SYSTEM_ID = lSysId;
                    // 枝番 
                    retdate.SEQ = iSeq;
                }
            }
            LogUtility.DebugMethodEnd(entrylist, upnlist, prtlist, detailprtlist, detaillist, retdatelist
                , keijyoulist, nisugatalist, houhoulist, koufuNo);
        }

        #endregion

        #region 検索
        /// <summary>
        /// 交付番号検索
        /// </summary>
        [Transaction]
        public virtual DataTable SerchKoufuNo(GetKoufuDtoCls serch)
        {
            LogUtility.DebugMethodStart(serch);

            DataTable dt = new DataTable();

            try
            {
                dt = GetKoufuDao.GetDataForEntity(serch);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd(serch);

            return dt;
        }
        #endregion

        #region チェック
        /// <summary>
        /// 画面入力チェック
        /// </summary>
        /// <param name="chkKbn"></param>
        /// <param name="buttonKbn"></param>
        /// <returns></returns>
        public bool CheckInputItem(Const.ConstCls.enumChkKbn chkKbn, Const.ConstCls.enumButtonKbn buttonKbn)
        {
            LogUtility.DebugMethodStart(chkKbn, buttonKbn);

            string errorMsg = string.Empty;
            Const.ConstCls.enumChkErrorKbn chkErrorKbn = Const.ConstCls.enumChkErrorKbn.None;
            var msgList = new List<string>();
            bool isErr = false;

            try
            {
                //check  印刷部数 when [F1]印刷
                if (buttonKbn.Equals(Const.ConstCls.enumButtonKbn.Print))
                {
                    //印刷部数が空白の場合
                    if (string.IsNullOrEmpty(this.form.txt_InsatsuBusu.Text))
                    {
                        msgList.Add(string.Format(Message.MessageUtility.GetMessageString("E012"), "印刷部数"));
                        chkErrorKbn = ConstCls.enumChkErrorKbn.Insatsu; //エラーメッセージ後のフォーカスを設定
                        this.form.txt_InsatsuBusu.IsInputErrorOccured = true;
                    }
                }
                //check 交付番号 when [F4]登録
                else if (buttonKbn.Equals(Const.ConstCls.enumButtonKbn.Toroku))
                {
                    this.form.txt_InsatsuBusu.IsInputErrorOccured = false;

                    //印刷部数が空白の場合
                    if (string.IsNullOrEmpty(this.form.txt_InsatsuBusu.Text))
                    {
                        msgList.Add(string.Format(Message.MessageUtility.GetMessageString("E012"), "印刷部数"));
                        chkErrorKbn = ConstCls.enumChkErrorKbn.Insatsu; //エラーメッセージ後のフォーカスを設定
                        this.form.txt_InsatsuBusu.IsInputErrorOccured = true;
                    }

                    if (this.form.cbx_ManifestToroku.Checked)
                    {
                        //交付番号が未入力の場合
                        if (string.IsNullOrEmpty(this.form.txt_KoufuNo.Text))
                        {
                            msgList.Add(string.Format(Message.MessageUtility.GetMessageString("E012"), "交付番号"));
                            chkErrorKbn = ConstCls.enumChkErrorKbn.Koufu;
                            this.form.txt_KoufuNo.IsInputErrorOccured = true;
                        }
                        else
                        {

                            string errmsg = ManifestoLogic.ChkKoufuNo(this.form.txt_KoufuNo.Text, this.form.KoufuKbn.Equals("1"));
                            if (!string.IsNullOrEmpty(errmsg))
                            {
                                msgList.Add(errmsg);
                                chkErrorKbn = ConstCls.enumChkErrorKbn.Koufu;
                                this.form.txt_KoufuNo.IsInputErrorOccured = true;
                            }
                        }
                    }
                }
                //[F9]印刷登録
                else
                {
                    if (chkKbn.Equals(Const.ConstCls.enumChkKbn.All))
                    {
                        this.form.txt_InsatsuBusu.IsInputErrorOccured = false;

                        //印刷部数が空白の場合
                        if (string.IsNullOrEmpty(this.form.txt_InsatsuBusu.Text))
                        {
                            msgList.Add(string.Format(Message.MessageUtility.GetMessageString("E012"), "印刷部数"));
                            chkErrorKbn = ConstCls.enumChkErrorKbn.Insatsu; //エラーメッセージ後のフォーカスを設定
                            this.form.txt_InsatsuBusu.IsInputErrorOccured = true;
                        }
                    }

                    //交付番号ロストフォーカス
                    if (chkKbn.Equals(Const.ConstCls.enumChkKbn.KoufuOnly) && this.form.txt_KoufuNo.ReadOnly == false) //2013.11.23 naitou update 入力制限をReadOnlyで設定
                    {
                        this.form.txt_KoufuNo.IsInputErrorOccured = false;

                        if (this.form.txt_KoufuNo.Text == string.Empty)
                        {
                            LogUtility.DebugMethodEnd(isErr);
                            return isErr;
                        }

                        if (Properties.Settings.Default.syoriMode.Equals((int)WINDOW_TYPE.NEW_WINDOW_FLAG)
                            && string.IsNullOrEmpty(this.form.txt_KoufuNo.Text)
                            && this.form.txt_InsatsuBusu.Text.Equals(Const.ConstCls.INSATSU_BUSU_DEFAULT))
                        {
                            LogUtility.DebugMethodEnd(isErr);
                            return isErr;
                        }
                    }

                    if (chkKbn.Equals(Const.ConstCls.enumChkKbn.KoufuOnly)
                        && this.form.txt_KoufuNo.ReadOnly == false) //2013.11.23 naitou update 入力制限をReadOnlyで設定
                    {
                        this.form.txt_KoufuNo.IsInputErrorOccured = false;

                        //交付番号が未入力の場合
                        if (string.IsNullOrEmpty(this.form.txt_KoufuNo.Text))
                        {
                            msgList.Add(string.Format(Message.MessageUtility.GetMessageString("E012"), "交付番号"));
                            chkErrorKbn = ConstCls.enumChkErrorKbn.Koufu;
                            this.form.txt_KoufuNo.IsInputErrorOccured = true;
                        }
                        else
                        {

                            string errmsg = ManifestoLogic.ChkKoufuNo(this.form.txt_KoufuNo.Text, this.form.KoufuKbn.Equals("1"));
                            if (!string.IsNullOrEmpty(errmsg))
                            {
                                msgList.Add(errmsg);
                                chkErrorKbn = ConstCls.enumChkErrorKbn.Koufu;
                                this.form.txt_KoufuNo.IsInputErrorOccured = true;
                            }
                        }
                    }

                    if (chkKbn.Equals(Const.ConstCls.enumChkKbn.All) && this.form.cbx_ManifestToroku.Checked)
                    {
                        this.form.txt_KoufuNo.IsInputErrorOccured = false;

                        //交付番号が未入力の場合
                        if (string.IsNullOrEmpty(this.form.txt_KoufuNo.Text))
                        {
                            msgList.Add(string.Format(Message.MessageUtility.GetMessageString("E012"), "交付番号"));
                            chkErrorKbn = ConstCls.enumChkErrorKbn.Koufu;
                            this.form.txt_KoufuNo.IsInputErrorOccured = true;
                        }
                        else
                        {

                            string errmsg = ManifestoLogic.ChkKoufuNo(this.form.txt_KoufuNo.Text, this.form.KoufuKbn.Equals("1"));
                            if (!string.IsNullOrEmpty(errmsg))
                            {
                                msgList.Add(errmsg);
                                chkErrorKbn = ConstCls.enumChkErrorKbn.Koufu;
                                this.form.txt_KoufuNo.IsInputErrorOccured = true;
                            }
                        }
                    }
                }

                //エラー存在する場合
                if (msgList.Count > 0)
                {

                    //結合してまとめて出す
                    Message.MessageBoxUtility.MessageBoxShowError(string.Join("\n", msgList));

                    //フォーカス制御
                    switch (chkErrorKbn)
                    {
                        case Const.ConstCls.enumChkErrorKbn.Insatsu:
                            this.form.txt_InsatsuBusu.Focus();
                            this.form.txt_InsatsuBusu.SelectAll(); //空なのであまり意味ないですが念のため。
                            break;
                        case Const.ConstCls.enumChkErrorKbn.Koufu:
                            this.form.txt_KoufuNo.Focus();
                            this.form.txt_KoufuNo.SelectAll();
                            break;
                        default:
                            break;

                    }
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckInputItem", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }


        /// <summary>
        /// 印刷部数チェック
        /// </summary>
        public bool ChkInsatsuBusu()
        {
            LogUtility.DebugMethodStart();
            bool isErr = false;
            try
            {
                //入力値が"0"の場合
                if (!string.IsNullOrEmpty(this.form.txt_InsatsuBusu.Text)
                    && Convert.ToInt32(this.form.txt_InsatsuBusu.Text) == Const.ConstCls.INSATSU_BUSU_ZERO)
                {
                    //「1」を設定する
                    this.form.txt_InsatsuBusu.Text = Const.ConstCls.INSATSU_BUSU_DEFAULT;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkInsatsuBusu", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

        #endregion

        #region 印刷処理
        /// <summary>
        /// 単位正式名称取得
        /// </summary>
        /// <param name="unitCd">単位CD</param>
        /// <returns>単位正式名</returns>
        private string GetUnitName(System.Data.SqlTypes.SqlInt16 unitCd)
        {
            LogUtility.DebugMethodStart(unitCd);

            string retVal = string.Empty;

            var unitEnt = this.unitData.FirstOrDefault(s => s.UNIT_CD.Value == (unitCd.IsNull ? -1 : unitCd.Value));
            if (unitEnt != null)
            {
                retVal = unitEnt.UNIT_NAME;
            }

            LogUtility.DebugMethodEnd(retVal);
            return retVal;
        }

        /// <summary>
        /// 明細から最終処分終了年月日を取得
        /// </summary>
        /// <returns></returns>
        private string GetLastSbnEndDateString()
        {
            LogUtility.DebugMethodStart();
            string retVal = string.Empty;

            if (this.form.Detaillist != null && this.form.Detaillist.Any(s => !s.LAST_SBN_END_DATE.IsNull))
            {
                retVal = this.form.Detaillist.Max(s => s.LAST_SBN_END_DATE).Value.ToString();
            }

            LogUtility.DebugMethodEnd(retVal);
            return retVal;
        }

        /// <summary>
        /// 明細から処分終了年月日を取得
        /// </summary>
        /// <returns></returns>
        private string GetSbnEndDateString()
        {
            LogUtility.DebugMethodStart();
            string retVal = string.Empty;

            if (this.form.Detaillist != null && this.form.Detaillist.Any(s => !s.SBN_END_DATE.IsNull))
            {
                retVal = this.form.Detaillist.Max(s => s.SBN_END_DATE).Value.ToString();
            }

            LogUtility.DebugMethodEnd(retVal);
            return retVal;
        }

        /// <summary>
        /// 帳票データ、1行分の文字列作成
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string CreateRowString(List<object> param)
        {
            return String.Join(",", param.Select(s => String.Format("\"{0}\"", Convert.ToString(s))));
        }

        /// <summary>
        /// 産廃(直行)マニフェストの場合、印刷データを作成する
        /// </summary>
        /// <param name="dt">印刷用データテーブル</param>
        private DataTable CreateChokkouManiPrintingData()
        {
            LogUtility.DebugMethodStart();
            
            var dt = new DataTable();
            var dc = new DataColumn();
            dc.ColumnName = "Item";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            // dtのスキーマや制約をコピー
            DataRow dr = dt.NewRow();

            #region①－１
            var record = new List<object>(){
                "1-1"
                ,this.form.Entrylist[0].KOUFU_DATE
                ,this.form.Entrylist[0].SEIRI_ID
                ,this.form.Entrylist[0].KOUFU_TANTOUSHA
                ,this.form.Entrylist[0].HST_GYOUSHA_NAME
                ,this.form.Entrylist[0].HST_GYOUSHA_POST
                ,this.form.Entrylist[0].HST_GYOUSHA_TEL
                ,this.form.Entrylist[0].HST_GYOUSHA_ADDRESS
                ,this.form.Entrylist[0].HST_GENBA_NAME
                ,this.form.Entrylist[0].HST_GENBA_POST
                ,this.form.Entrylist[0].HST_GENBA_TEL
                ,this.form.Entrylist[0].HST_GENBA_ADDRESS
                ,this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.IsNull ? string.Empty : this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.ToString()
                ,this.form.Entrylist[0].CHUUKAN_HAIKI
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.IsNull ? string.Empty : this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.ToString()
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_NAME
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_POST
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_TEL
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_ADDRESS
                ,this.form.Entrylist[0].SBN_GYOUSHA_NAME
                ,this.form.Entrylist[0].SBN_GYOUSHA_POST
                ,this.form.Entrylist[0].SBN_GYOUSHA_TEL
                ,this.form.Entrylist[0].SBN_GYOUSHA_ADDRESS
                ,this.form.Entrylist[0].TMH_GENBA_NAME
                ,this.form.Entrylist[0].TMH_GENBA_POST
                ,this.form.Entrylist[0].TMH_GENBA_TEL
                ,this.form.Entrylist[0].TMH_GENBA_ADDRESS
                ,this.form.Entrylist[0].CHECK_B2
                ,this.form.Entrylist[0].CHECK_D
                ,this.form.Entrylist[0].CHECK_E
                ,this.form.Prtlist[0].PRT_FUTSUU_HAIKIBUTSU ? "1" :"0"
                ,this.form.Prtlist[0].PRT_TOKUBETSU_HAIKIBUTSU ? "1" :"0"
                ,this.form.txt_InsatsuBusu.Text
                ,this.form.Entrylist[0].HST_GYOUSHA_CD
                ,this.form.Entrylist[0].HST_GENBA_CD
                ,this.form.Entrylist[0].TMH_GYOUSHA_CD
                ,this.form.Entrylist[0].TMH_GENBA_CD
                ,this.form.Entrylist[0].SBN_GYOUSHA_CD
                ,this.form.Entrylist[0].MANIFEST_KAMI_CHECK
            };
            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ②－１
            dr = dt.NewRow();
            record.Clear();
            record.Add("2-1");
            foreach (T_MANIFEST_UPN upn in this.form.Upnlist)
            {
                record.AddRange(new object[]{
                     upn.UPN_GYOUSHA_NAME
                    ,upn.UPN_GYOUSHA_POST
                    ,upn.UPN_GYOUSHA_TEL
                    ,upn.UPN_GYOUSHA_ADDRESS
                    ,upn.UPN_SAKI_GENBA_NAME
                    ,upn.UPN_SAKI_GENBA_POST
                    ,upn.UPN_SAKI_GENBA_TEL
                    ,upn.UPN_SAKI_GENBA_ADDRESS
                    ,upn.UPN_JYUTAKUSHA_NAME
                    ,upn.UNTENSHA_NAME
                    ,upn.UPN_END_DATE
                    ,upn.YUUKA_SUU.IsNull ? string.Empty: upn.YUUKA_SUU.Value.ToString() //12
                    ,this.GetUnitName(upn.YUUKA_UNIT_CD) //13
                    ,upn.UPN_GYOUSHA_CD
                    ,upn.UPN_SAKI_GENBA_CD
                });
            }

            record.AddRange(new object[]{
                this.form.Entrylist[0].SBN_JYUTAKUSHA_NAME //14
                ,this.form.Entrylist[0].SBN_TANTOU_NAME //15
                ,this.GetSbnEndDateString() //16
                ,this.GetLastSbnEndDateString() //17
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NAME //18
                ,this.form.Entrylist[0].LAST_SBN_GENBA_POST //19
                ,this.form.Entrylist[0].LAST_SBN_GENBA_TEL //20
                ,this.form.Entrylist[0].LAST_SBN_GENBA_ADDRESS //21
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NUMBER // 22
            });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ③－１
            foreach (T_MANIFEST_DETAIL_PRT detailprt in this.form.Detailprtlist)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                     "3-1"
                    ,detailprt.REC_NO
                    // 2014/03/24 廃棄物種類コードは非表示
                    //,detailprt.HAIKI_SHURUI_CD
                    ,String.Empty
                    ,detailprt.HAIKI_SHURUI_NAME
                    ,detailprt.PRT_FLG
                });

                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }
            #endregion

            #region ④－１
            dr = dt.NewRow();
            record.Clear();
            record.AddRange(new object[]{
                     "4-1"
                    ,this.form.Prtlist[0].PRT_SUU.IsNull ? string.Empty : this.form.Prtlist[0].PRT_SUU.ToString()
                    ,this.GetUnitName(this.form.Prtlist[0].PRT_UNIT_CD)
                    ,this.form.Prtlist[0].PRT_NISUGATA_NAME
                    ,this.form.Prtlist[0].PRT_HAIKI_NAME
                    ,this.form.Prtlist[0].PRT_YUUGAI_NAME
                    ,this.form.Prtlist[0].PRT_SBN_HOUHOU_NAME
                    ,this.form.Entrylist[0].BIKOU
                });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region 7-1 斜線
            dr = dt.NewRow();
            record.Clear();
            record.AddRange(new object[]{
                    "7-1",
                    this.form.Prtlist[0].SLASH_YUUGAI_FLG,
                    this.form.Prtlist[0].SLASH_BIKOU_FLG,
                    this.form.Prtlist[0].SLASH_CHUUKAN_FLG,
                    this.form.Prtlist[0].SLASH_TSUMIHO_FLG,
                    this.form.Prtlist[0].SLASH_JIZENKYOUGI_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA3_FLG,
                    this.form.Prtlist[0].SLASH_B1_FLG,
                    this.form.Prtlist[0].SLASH_B2_FLG,
                    this.form.Prtlist[0].SLASH_B4_FLG,
                    this.form.Prtlist[0].SLASH_B6_FLG,
                    this.form.Prtlist[0].SLASH_D_FLG,
                    this.form.Prtlist[0].SLASH_E_FLG
                });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion


            LogUtility.DebugMethodEnd(dt);
            return dt;
        }

        /// <summary>
        /// 産廃マニフェスト(積替用)の場合、印刷データを作成する
        /// </summary>
        /// <param name="dt">印刷用データテーブル</param>
        private DataTable CreateSekitaiManiPrintingData()
        {
            LogUtility.DebugMethodStart();

            var dt = new DataTable();
            var dc = new DataColumn();
            dc.ColumnName = "Item";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            DataRow dr = dt.NewRow();

            #region①－１
            var record = new List<object>(){
                    "1-1"
                    ,this.form.Entrylist[0].KOUFU_DATE // 1
                    ,this.form.Entrylist[0].SEIRI_ID // 2
                    ,this.form.Entrylist[0].KOUFU_TANTOUSHA // 3
                    ,this.form.Entrylist[0].HST_GYOUSHA_NAME // 4
                    ,this.form.Entrylist[0].HST_GYOUSHA_POST // 5
                    ,this.form.Entrylist[0].HST_GYOUSHA_TEL // 6
                    ,this.form.Entrylist[0].HST_GYOUSHA_ADDRESS // 7
                    ,this.form.Entrylist[0].HST_GENBA_NAME // 8
                    ,this.form.Entrylist[0].HST_GENBA_POST // 9
                    ,this.form.Entrylist[0].HST_GENBA_TEL // 10
                    ,this.form.Entrylist[0].HST_GENBA_ADDRESS // 11
                    ,this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.IsNull ? string.Empty : this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.ToString() // 12
                    ,this.form.Entrylist[0].CHUUKAN_HAIKI // 13
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.IsNull ? string.Empty : this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.ToString() // 14
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_NAME // 15
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_POST // 16
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_TEL // 17
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_ADDRESS // 18
                    ,this.form.Entrylist[0].SBN_GYOUSHA_NAME // 19
                    ,this.form.Entrylist[0].SBN_GYOUSHA_POST // 20
                    ,this.form.Entrylist[0].SBN_GYOUSHA_TEL // 21
                    ,this.form.Entrylist[0].SBN_GYOUSHA_ADDRESS // 22
                    ,this.form.Entrylist[0].BIKOU // 23
                    ,this.form.Entrylist[0].CHECK_B2 // 24
                    ,this.form.Entrylist[0].CHECK_B4 // 25
                    ,this.form.Entrylist[0].CHECK_B6 // 26
                    ,this.form.Entrylist[0].CHECK_D // 27
                    ,this.form.Entrylist[0].CHECK_E // 28
                    ,this.form.txt_InsatsuBusu.Text // 29
            };
            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ②－１
            dr = dt.NewRow();
            record.Clear();
            record.Add("2-1");
            foreach (T_MANIFEST_UPN upn in this.form.Upnlist)
            {
                record.AddRange(new object[]{
                     upn.UPN_SAKI_KBN.IsNull ? string.Empty : upn.UPN_SAKI_KBN.ToString() // 1
                    ,upn.UPN_GYOUSHA_NAME // 2 16 
                    ,upn.UPN_GYOUSHA_POST // 3 17
                    ,upn.UPN_GYOUSHA_TEL // 4 
                    ,upn.UPN_GYOUSHA_ADDRESS // 5
                    ,upn.UPN_SAKI_GENBA_NAME // 6
                    ,upn.UPN_SAKI_GENBA_POST // 7
                    ,upn.UPN_SAKI_GENBA_TEL // 8
                    ,upn.UPN_SAKI_GENBA_ADDRESS // 9

                    ,upn.UPN_JYUTAKUSHA_NAME // 10
                    ,upn.UNTENSHA_NAME // 11
                    ,upn.UPN_END_DATE // 12
                    ,upn.YUUKA_SUU.IsNull ? string.Empty: upn.YUUKA_SUU.Value.ToString() // 13
                    ,this.GetUnitName(upn.YUUKA_UNIT_CD) // 14
                });
            }

            // 積替え又は保管&処分の受託&最終処分を行った場所
            record.AddRange(new object[]{
                 this.form.Entrylist[0].SBN_JYUTAKUSHA_NAME // 43
                ,this.form.Entrylist[0].SBN_TANTOU_NAME // 44
                ,this.GetSbnEndDateString() // 45
                ,this.GetLastSbnEndDateString() // 46
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NAME // 47
                ,this.form.Entrylist[0].LAST_SBN_GENBA_POST // 48
                ,this.form.Entrylist[0].LAST_SBN_GENBA_TEL // 49
                ,this.form.Entrylist[0].LAST_SBN_GENBA_ADDRESS // 50
                ,this.form.Entrylist[0].TMH_GENBA_NAME // 51
                ,this.form.Entrylist[0].TMH_GENBA_POST // 52
                ,this.form.Entrylist[0].TMH_GENBA_TEL // 53
                ,this.form.Entrylist[0].TMH_GENBA_ADDRESS // 54
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NUMBER // 55
            });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ③－１
            dr = dt.NewRow();
            record.Clear();
            record.AddRange(new object[]{
                "3-1"
                // 20140620 kayo 不具合No.4923 廃棄物種類が印字されない start
                //,this.form.Detailprtlist[0].HAIKI_SHURUI_NAME
                ,this.form.Prtlist[0].PRT_HAIKI_SHURUI_NAME
                // 20140620 kayo 不具合No.4923 廃棄物種類が印字されない end
                ,this.form.Prtlist[0].PRT_SUU.IsNull ? string.Empty : this.form.Prtlist[0].PRT_SUU.ToString()
                ,this.GetUnitName(this.form.Prtlist[0].PRT_UNIT_CD)
                ,this.form.Prtlist[0].PRT_HAIKI_NAME
                ,this.form.Prtlist[0].PRT_NISUGATA_NAME
                ,this.form.Prtlist[0].PRT_YUUGAI_NAME
                ,this.form.Prtlist[0].PRT_SBN_HOUHOU_NAME
            });
            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region 7-1 斜線
            dr = dt.NewRow();
            record.Clear();
            record.AddRange(new object[]{
                    "7-1",
                    this.form.Prtlist[0].SLASH_YUUGAI_FLG,
                    this.form.Prtlist[0].SLASH_BIKOU_FLG,
                    this.form.Prtlist[0].SLASH_CHUUKAN_FLG,
                    this.form.Prtlist[0].SLASH_TSUMIHO_FLG,
                    this.form.Prtlist[0].SLASH_JIZENKYOUGI_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA3_FLG,
                    this.form.Prtlist[0].SLASH_B1_FLG,
                    this.form.Prtlist[0].SLASH_B2_FLG,
                    this.form.Prtlist[0].SLASH_B4_FLG,
                    this.form.Prtlist[0].SLASH_B6_FLG,
                    this.form.Prtlist[0].SLASH_D_FLG,
                    this.form.Prtlist[0].SLASH_E_FLG
                });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            LogUtility.DebugMethodEnd(dt);
            return dt;
        }

        /// <summary>
        /// 建廃マニフェスト入力の場合、印刷データを作成する
        /// </summary>
        /// <param name="dt">印刷用データテーブル</param>
        /// <returns></returns>
        private DataTable CreateKenpaiManiPrintingData()
        {
            LogUtility.DebugMethodStart();

            var dt = new DataTable();
            var dc = new DataColumn();
            dc.ColumnName = "Item";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            DataRow dr = dt.NewRow();

            #region ①－１
            var record = new List<object>(){
                    "1-1"
                    ,this.form.Entrylist[0].KOUFU_DATE // 1
                    ,this.form.Entrylist[0].SEIRI_ID // 2
                    ,this.form.Entrylist[0].KOUFU_TANTOUSHA // 3
                    ,this.form.Entrylist[0].JIZEN_NUMBER // 4
                    ,this.form.Entrylist[0].JIZEN_DATE // 5
                    ,this.form.Entrylist[0].HST_GYOUSHA_NAME // 6
                    ,this.form.Entrylist[0].HST_GYOUSHA_POST // 7
                    ,this.form.Entrylist[0].HST_GYOUSHA_TEL  // 8
                    ,this.form.Entrylist[0].HST_GYOUSHA_ADDRESS  // 9
                    ,this.form.Entrylist[0].HST_GENBA_NAME  // 10
                    ,this.form.Entrylist[0].HST_GENBA_POST // 11
                    ,this.form.Entrylist[0].HST_GENBA_TEL // 12
                    ,this.form.Entrylist[0].HST_GENBA_ADDRESS // 13
                    ,this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.IsNull?string.Empty:this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.ToString() // 14
                    ,this.form.Entrylist[0].CHUUKAN_HAIKI // 15
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.IsNull?string.Empty:this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.ToString() // 16
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_NAME // 17
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_ADDRESS // 18
                    ,this.form.Entrylist[0].SBN_GYOUSHA_NAME // 19
                    ,this.form.Entrylist[0].SBN_GYOUSHA_POST // 20
                    ,this.form.Entrylist[0].SBN_GYOUSHA_TEL // 21
                    ,this.form.Entrylist[0].SBN_GYOUSHA_ADDRESS // 22
                    ,this.form.Entrylist[0].TMH_GENBA_POST // 23
                    ,this.form.Entrylist[0].TMH_GENBA_TEL // 24
                    ,this.form.Entrylist[0].TMH_GENBA_ADDRESS // 25
                    ,this.form.Entrylist[0].YUUKA_KBN.IsNull?string.Empty:this.form.Entrylist[0].YUUKA_KBN.ToString() // 26
                    ,this.form.Entrylist[0].YUUKA_SUU.IsNull ? string.Empty: this.form.Entrylist[0].YUUKA_SUU.Value.ToString() // 実績数量 // 27
                    ,this.form.Entrylist[0].YUUKA_UNIT_CD.IsNull ? string.Empty : this.form.Entrylist[0].YUUKA_UNIT_CD.ToString() // 実績数量単位 // 28
                    ,this.form.Entrylist[0].BIKOU // 29
                    ,this.form.Entrylist[0].CHECK_B1 // 30
                    ,this.form.Entrylist[0].CHECK_B2 // 31
                    ,this.form.Entrylist[0].CHECK_D // 32
                    ,this.form.Entrylist[0].CHECK_E // 33
                    ,this.form.Prtlist[0].PRT_UNIT_CD // 34
                    ,this.form.Prtlist[0].PRT_SUU.IsNull?string.Empty:this.form.Prtlist[0].PRT_SUU.ToString() // 35
                    ,this.form.txt_InsatsuBusu.Text // 36
                    ,this.form.Entrylist[0].KOUFU_TANTOUSHA_SHOZOKU // 36 交付担当者所属追加
                    ,this.form.Entrylist[0].HST_GYOUSHA_CD
                    ,this.form.Entrylist[0].HST_GENBA_CD
                    ,this.form.Entrylist[0].TMH_GYOUSHA_CD
                    ,this.form.Entrylist[0].TMH_GENBA_CD
                    ,this.form.Entrylist[0].SBN_GYOUSHA_CD
                    ,this.form.Entrylist[0].MANIFEST_KAMI_CHECK
                };
            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ②－１
            dr = dt.NewRow();
            record.Clear();
            record.Add("2-1");
            foreach (T_MANIFEST_UPN upn in this.form.Upnlist)
            {
                record.AddRange(new object[]{
                     upn.UPN_GYOUSHA_NAME // 1
                    ,upn.UPN_GYOUSHA_POST // 2
                    ,upn.UPN_GYOUSHA_TEL  // 3
                    ,upn.UPN_GYOUSHA_ADDRESS  // 4
                    ,upn.TMH_KBN.IsNull ? string.Empty : upn.TMH_KBN.ToString()  // 5
                    ,upn.SHARYOU_NAME // 6
                    ,upn.SHASHU_NAME // 7
                    ,upn.UPN_SAKI_GENBA_NAME // 8
                    ,upn.UPN_SAKI_GENBA_POST // 9
                    ,upn.UPN_SAKI_GENBA_TEL  // 10
                    ,upn.UPN_SAKI_GENBA_ADDRESS  // 11
                    ,upn.UPN_JYUTAKUSHA_NAME  // 12
                    ,upn.UNTENSHA_NAME // 13
                    ,upn.UPN_END_DATE // 14
                    ,upn.UPN_GYOUSHA_CD
                    ,upn.UPN_SAKI_GENBA_CD
                });
            }

            record.AddRange(new object[]{
                // 処分の受託（受領）
                 this.form.Entrylist[0].SBN_JYURYOUSHA_NAME // 29
                ,this.form.Entrylist[0].SBN_JYURYOU_TANTOU_NAME // 30
                ,this.form.Entrylist[0].SBN_JYURYOU_DATE // 31
                // 処分の受託（処分）
                ,this.form.Entrylist[0].SBN_JYUTAKUSHA_NAME // 32
                ,this.form.Entrylist[0].SBN_TANTOU_NAME // 33
                ,this.GetSbnEndDateString() // 34
                // 最終処分終了日
                ,this.GetLastSbnEndDateString() // 35
                ,this.form.Entrylist[0].LAST_SBN_CHECK_NAME // 36
                // 最終処分
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NAME // 37
                ,this.form.Entrylist[0].LAST_SBN_GENBA_POST // 38
                ,this.form.Entrylist[0].LAST_SBN_GENBA_ADDRESS // 39
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NUMBER // 40
            });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ③④⑤⑥
            //③－１
            foreach (T_MANIFEST_DETAIL_PRT detailprt in this.form.Detailprtlist)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                    "3-1"
                    ,detailprt.REC_NO
                    // 2014/03/24 廃棄物種類コードは非表示(空白)
                    //,detailprt.HAIKI_SHURUI_CD
                    ,string.Empty
                    ,detailprt.HAIKI_SHURUI_NAME
                    ,detailprt.HAIKI_SUURYOU.IsNull ? string.Empty : detailprt.HAIKI_SUURYOU.ToString()
                    ,detailprt.PRT_FLG
                });
                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }

            //④－１
            foreach (T_MANIFEST_KP_KEIJYOU keijyou in this.form.Keijyoulist)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                    "4-1"
                    ,keijyou.REC_NO
                    // 2014/03/24 形状コードは非表示(空白)
                    //,keijyou.KEIJOU_CD
                    ,string.Empty
                    ,keijyou.KEIJOU_NAME
                    ,keijyou.PRT_FLG
                });
                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }

            //⑤－１
            foreach (T_MANIFEST_KP_NISUGATA niugata in this.form.Nisugatalist)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                    "5-1"
                    ,niugata.REC_NO
                    // 2014/03/24 荷姿コードは非表示(空白)
                    //,niugata.NISUGATA_CD
                    ,string.Empty
                    ,niugata.NISUGATA_NAME
                    ,niugata.PRT_FLG
                });
                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }

            //⑥－１
            for (int i = 0; i < this.form.Houhoulist.Count; i++)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                    "6-1"
                    ,this.form.Houhoulist[i].REC_NO
                    ,this.form.Houhoulist[i].SHOBUN_HOUHOU_CD
                    ,this.form.Houhoulist[i].SHOBUN_HOUHOU_NAME
                    ,this.form.Houhoulist[i].PRT_FLG
                });
                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }
            #endregion

            #region 7-1 斜線
            dr = dt.NewRow();
            record.Clear();
            record.AddRange(new object[]{
                    "7-1",
                    false, //this.form.Prtlist[0].SLASH_YUUGAI_FLG, //有害がないためnullになっているのでfalseを直接利用
                    this.form.Prtlist[0].SLASH_BIKOU_FLG,
                    this.form.Prtlist[0].SLASH_CHUUKAN_FLG,
                    this.form.Prtlist[0].SLASH_TSUMIHO_FLG,
                    this.form.Prtlist[0].SLASH_JIZENKYOUGI_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA3_FLG,
                    this.form.Prtlist[0].SLASH_B1_FLG,
                    this.form.Prtlist[0].SLASH_B2_FLG,
                    this.form.Prtlist[0].SLASH_B4_FLG,
                    this.form.Prtlist[0].SLASH_B6_FLG,
                    this.form.Prtlist[0].SLASH_D_FLG,
                    this.form.Prtlist[0].SLASH_E_FLG
                });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion
            LogUtility.DebugMethodEnd(dt);
            return dt;
        }

        /// <summary>
        /// 産廃(直行)水銀マニフェストの場合、印刷データを作成する
        /// </summary>
        /// <param name="dt">印刷用データテーブル</param>
        private DataTable CreateChokkouMercuryManiPrintingData()
        {
            LogUtility.DebugMethodStart();

            var dt = new DataTable();
            var dc = new DataColumn();
            dc.ColumnName = "Item";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            // dtのスキーマや制約をコピー
            DataRow dr = dt.NewRow();

            #region①－１
            var record = new List<object>(){
                "1-1"
                ,this.form.Entrylist[0].KOUFU_DATE
                ,this.form.Entrylist[0].SEIRI_ID
                ,this.form.Entrylist[0].KOUFU_TANTOUSHA
                ,this.form.Entrylist[0].HST_GYOUSHA_NAME
                ,this.form.Entrylist[0].HST_GYOUSHA_POST
                ,this.form.Entrylist[0].HST_GYOUSHA_TEL
                ,this.form.Entrylist[0].HST_GYOUSHA_ADDRESS
                ,this.form.Entrylist[0].HST_GENBA_NAME
                ,this.form.Entrylist[0].HST_GENBA_POST
                ,this.form.Entrylist[0].HST_GENBA_TEL
                ,this.form.Entrylist[0].HST_GENBA_ADDRESS
                ,this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.IsNull ? string.Empty : this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.ToString()
                ,this.form.Entrylist[0].CHUUKAN_HAIKI
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.IsNull ? string.Empty : this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.ToString()
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_NAME
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_POST
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_TEL
                ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_ADDRESS
                ,this.form.Entrylist[0].SBN_GYOUSHA_NAME
                ,this.form.Entrylist[0].SBN_GYOUSHA_POST
                ,this.form.Entrylist[0].SBN_GYOUSHA_TEL
                ,this.form.Entrylist[0].SBN_GYOUSHA_ADDRESS
                ,this.form.Entrylist[0].TMH_GENBA_NAME
                ,this.form.Entrylist[0].TMH_GENBA_POST
                ,this.form.Entrylist[0].TMH_GENBA_TEL
                ,this.form.Entrylist[0].TMH_GENBA_ADDRESS
                ,this.form.Entrylist[0].CHECK_B2
                ,this.form.Entrylist[0].CHECK_D
                ,this.form.Entrylist[0].CHECK_E
                ,this.form.Prtlist[0].PRT_FUTSUU_HAIKIBUTSU ? "1" :"0"
                ,this.form.Prtlist[0].PRT_TOKUBETSU_HAIKIBUTSU ? "1" :"0"
                ,this.form.txt_InsatsuBusu.Text
                ,this.form.Entrylist[0].HST_GYOUSHA_CD
                ,this.form.Entrylist[0].HST_GENBA_CD
                ,this.form.Entrylist[0].TMH_GYOUSHA_CD
                ,this.form.Entrylist[0].TMH_GENBA_CD
                ,this.form.Entrylist[0].SBN_GYOUSHA_CD
                ,this.form.Entrylist[0].MANIFEST_KAMI_CHECK
            };
            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ②－１
            dr = dt.NewRow();
            record.Clear();
            record.Add("2-1");
            foreach (T_MANIFEST_UPN upn in this.form.Upnlist)
            {
                record.AddRange(new object[]{
                     upn.UPN_GYOUSHA_NAME
                    ,upn.UPN_GYOUSHA_POST
                    ,upn.UPN_GYOUSHA_TEL
                    ,upn.UPN_GYOUSHA_ADDRESS
                    ,upn.UPN_SAKI_GENBA_NAME
                    ,upn.UPN_SAKI_GENBA_POST
                    ,upn.UPN_SAKI_GENBA_TEL
                    ,upn.UPN_SAKI_GENBA_ADDRESS
                    ,upn.UPN_JYUTAKUSHA_NAME
                    ,upn.UNTENSHA_NAME
                    ,upn.UPN_END_DATE
                    ,upn.YUUKA_SUU.IsNull ? string.Empty: upn.YUUKA_SUU.Value.ToString() //12
                    ,this.GetUnitName(upn.YUUKA_UNIT_CD) //13
                    ,upn.UPN_GYOUSHA_CD
                    ,upn.UPN_SAKI_GENBA_CD
                });
            }

            record.AddRange(new object[]{
                this.form.Entrylist[0].SBN_JYUTAKUSHA_NAME //14
                ,this.form.Entrylist[0].SBN_TANTOU_NAME //15
                ,this.GetSbnEndDateString() //16
                ,this.GetLastSbnEndDateString() //17
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NAME //18
                ,this.form.Entrylist[0].LAST_SBN_GENBA_POST //19
                ,this.form.Entrylist[0].LAST_SBN_GENBA_TEL //20
                ,this.form.Entrylist[0].LAST_SBN_GENBA_ADDRESS //21
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NUMBER // 22
            });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ③－１
            foreach (T_MANIFEST_DETAIL_PRT detailprt in this.form.Detailprtlist)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                     "3-1"
                    ,detailprt.REC_NO
                    // 2014/03/24 廃棄物種類コードは非表示
                    //,detailprt.HAIKI_SHURUI_CD
                    ,String.Empty
                    ,detailprt.HAIKI_SHURUI_NAME
                    ,detailprt.PRT_FLG
                });

                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }
            #endregion

            #region ④－１
            dr = dt.NewRow();
            record.Clear();
            record.AddRange(new object[]{
                     "4-1"
                    ,this.form.Prtlist[0].PRT_SUU.IsNull ? string.Empty : this.form.Prtlist[0].PRT_SUU.ToString()
                    ,this.GetUnitName(this.form.Prtlist[0].PRT_UNIT_CD)
                    ,this.form.Prtlist[0].PRT_NISUGATA_NAME
                    ,this.form.Prtlist[0].PRT_HAIKI_NAME
                    ,this.form.Prtlist[0].PRT_YUUGAI_NAME
                    ,this.form.Prtlist[0].PRT_SBN_HOUHOU_NAME
                    ,this.form.Entrylist[0].BIKOU
                    ,this.form.Entrylist[0].MERCURY_USED_SEIHIN_HAIKIBUTU_CHECK
                    ,this.form.Entrylist[0].MERCURY_BAIJINNADO_HAIKIBUTU_CHECK
                    ,this.form.Entrylist[0].ISIWAKANADO_HAIKIBUTU_CHECK
                    ,this.form.Entrylist[0].TOKUTEI_SANGYOU_HAIKIBUTU_CHECK
                });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region 7-1 斜線
            dr = dt.NewRow();
            record.Clear();
            record.AddRange(new object[]{
                    "7-1",
                    this.form.Prtlist[0].SLASH_YUUGAI_FLG,
                    this.form.Prtlist[0].SLASH_BIKOU_FLG,
                    this.form.Prtlist[0].SLASH_CHUUKAN_FLG,
                    this.form.Prtlist[0].SLASH_TSUMIHO_FLG,
                    this.form.Prtlist[0].SLASH_JIZENKYOUGI_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA3_FLG,
                    this.form.Prtlist[0].SLASH_B1_FLG,
                    this.form.Prtlist[0].SLASH_B2_FLG,
                    this.form.Prtlist[0].SLASH_B4_FLG,
                    this.form.Prtlist[0].SLASH_B6_FLG,
                    this.form.Prtlist[0].SLASH_D_FLG,
                    this.form.Prtlist[0].SLASH_E_FLG,
                    this.form.Prtlist[0].SLASH_MERCURY_FLG
                });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion


            LogUtility.DebugMethodEnd(dt);
            return dt;
        }

        /// <summary>
        /// 建廃水銀マニフェスト入力の場合、印刷データを作成する
        /// </summary>
        /// <param name="dt">印刷用データテーブル</param>
        /// <returns></returns>
        private DataTable CreateKenpaiMercuryManiPrintingData()
        {
            LogUtility.DebugMethodStart();

            var dt = new DataTable();
            var dc = new DataColumn();
            dc.ColumnName = "Item";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            DataRow dr = dt.NewRow();

            #region ①－１
            var record = new List<object>(){
                    "1-1"
                    ,this.form.Entrylist[0].KOUFU_DATE // 1
                    ,this.form.Entrylist[0].SEIRI_ID // 2
                    ,this.form.Entrylist[0].KOUFU_TANTOUSHA // 3
                    ,this.form.Entrylist[0].JIZEN_NUMBER // 4
                    ,this.form.Entrylist[0].JIZEN_DATE // 5
                    ,this.form.Entrylist[0].HST_GYOUSHA_NAME // 6
                    ,this.form.Entrylist[0].HST_GYOUSHA_POST // 7
                    ,this.form.Entrylist[0].HST_GYOUSHA_TEL  // 8
                    ,this.form.Entrylist[0].HST_GYOUSHA_ADDRESS  // 9
                    ,this.form.Entrylist[0].HST_GENBA_NAME  // 10
                    ,this.form.Entrylist[0].HST_GENBA_POST // 11
                    ,this.form.Entrylist[0].HST_GENBA_TEL // 12
                    ,this.form.Entrylist[0].HST_GENBA_ADDRESS // 13
                    ,this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.IsNull?string.Empty:this.form.Entrylist[0].CHUUKAN_HAIKI_KBN.ToString() // 14
                    ,this.form.Entrylist[0].CHUUKAN_HAIKI // 15
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.IsNull?string.Empty:this.form.Entrylist[0].LAST_SBN_YOTEI_KBN.ToString() // 16
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_NAME // 17
                    ,this.form.Entrylist[0].LAST_SBN_YOTEI_GENBA_ADDRESS // 18
                    ,this.form.Entrylist[0].SBN_GYOUSHA_NAME // 19
                    ,this.form.Entrylist[0].SBN_GYOUSHA_POST // 20
                    ,this.form.Entrylist[0].SBN_GYOUSHA_TEL // 21
                    ,this.form.Entrylist[0].SBN_GYOUSHA_ADDRESS // 22
                    ,this.form.Entrylist[0].TMH_GENBA_POST // 23
                    ,this.form.Entrylist[0].TMH_GENBA_TEL // 24
                    ,this.form.Entrylist[0].TMH_GENBA_ADDRESS // 25
                    ,this.form.Entrylist[0].YUUKA_KBN.IsNull?string.Empty:this.form.Entrylist[0].YUUKA_KBN.ToString() // 26
                    ,this.form.Entrylist[0].YUUKA_SUU.IsNull ? string.Empty: this.form.Entrylist[0].YUUKA_SUU.Value.ToString() // 実績数量 // 27
                    ,this.form.Entrylist[0].YUUKA_UNIT_CD.IsNull ? string.Empty : this.form.Entrylist[0].YUUKA_UNIT_CD.ToString() // 実績数量単位 // 28
                    ,this.form.Entrylist[0].BIKOU // 29
                    ,this.form.Entrylist[0].CHECK_B1 // 30
                    ,this.form.Entrylist[0].CHECK_B2 // 31
                    ,this.form.Entrylist[0].CHECK_D // 32
                    ,this.form.Entrylist[0].CHECK_E // 33
                    ,this.form.Prtlist[0].PRT_UNIT_CD // 34
                    ,this.form.Prtlist[0].PRT_SUU.IsNull?string.Empty:this.form.Prtlist[0].PRT_SUU.ToString() // 35
                    ,this.form.txt_InsatsuBusu.Text // 36
                    ,this.form.Entrylist[0].KOUFU_TANTOUSHA_SHOZOKU // 36 交付担当者所属追加
                    ,this.form.Entrylist[0].HST_GYOUSHA_CD
                    ,this.form.Entrylist[0].HST_GENBA_CD
                    ,this.form.Entrylist[0].TMH_GYOUSHA_CD
                    ,this.form.Entrylist[0].TMH_GENBA_CD
                    ,this.form.Entrylist[0].SBN_GYOUSHA_CD
                    ,this.form.Entrylist[0].MANIFEST_KAMI_CHECK
                };
            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ②－１
            dr = dt.NewRow();
            record.Clear();
            record.Add("2-1");
            foreach (T_MANIFEST_UPN upn in this.form.Upnlist)
            {
                record.AddRange(new object[]{
                     upn.UPN_GYOUSHA_NAME // 1
                    ,upn.UPN_GYOUSHA_POST // 2
                    ,upn.UPN_GYOUSHA_TEL  // 3
                    ,upn.UPN_GYOUSHA_ADDRESS  // 4
                    ,upn.TMH_KBN.IsNull ? string.Empty : upn.TMH_KBN.ToString()  // 5
                    ,upn.SHARYOU_NAME // 6
                    ,upn.SHASHU_NAME // 7
                    ,upn.UPN_SAKI_GENBA_NAME // 8
                    ,upn.UPN_SAKI_GENBA_POST // 9
                    ,upn.UPN_SAKI_GENBA_TEL  // 10
                    ,upn.UPN_SAKI_GENBA_ADDRESS  // 11
                    ,upn.UPN_JYUTAKUSHA_NAME  // 12
                    ,upn.UNTENSHA_NAME // 13
                    ,upn.UPN_END_DATE // 14
                    ,upn.UPN_GYOUSHA_CD// 15
                    ,upn.UPN_SAKI_GENBA_CD// 16
                });
            }

            record.AddRange(new object[]{
                // 処分の受託（受領）
                 this.form.Entrylist[0].SBN_JYURYOUSHA_NAME // 33
                ,this.form.Entrylist[0].SBN_JYURYOU_TANTOU_NAME // 34
                ,this.form.Entrylist[0].SBN_JYURYOU_DATE // 35
                // 処分の受託（処分）
                ,this.form.Entrylist[0].SBN_JYUTAKUSHA_NAME // 36
                ,this.form.Entrylist[0].SBN_TANTOU_NAME // 37
                ,this.GetSbnEndDateString() // 38
                // 最終処分終了日
                ,this.GetLastSbnEndDateString() // 39
                ,this.form.Entrylist[0].LAST_SBN_CHECK_NAME // 40
                // 最終処分
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NAME // 41
                ,this.form.Entrylist[0].LAST_SBN_GENBA_POST // 42
                ,this.form.Entrylist[0].LAST_SBN_GENBA_ADDRESS // 43
                ,this.form.Entrylist[0].LAST_SBN_GENBA_NUMBER // 44
            });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion

            #region ③④⑤⑥
            //③－１
            foreach (T_MANIFEST_DETAIL_PRT detailprt in this.form.Detailprtlist)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                    "3-1"
                    ,detailprt.REC_NO
                    // 2014/03/24 廃棄物種類コードは非表示(空白)
                    //,detailprt.HAIKI_SHURUI_CD
                    ,string.Empty
                    ,detailprt.HAIKI_SHURUI_NAME
                    ,detailprt.HAIKI_SUURYOU.IsNull ? string.Empty : detailprt.HAIKI_SUURYOU.ToString()
                    ,detailprt.PRT_FLG
                });
                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }

            //④－１
            foreach (T_MANIFEST_KP_KEIJYOU keijyou in this.form.Keijyoulist)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                    "4-1"
                    ,keijyou.REC_NO
                    // 2014/03/24 形状コードは非表示(空白)
                    //,keijyou.KEIJOU_CD
                    ,string.Empty
                    ,keijyou.KEIJOU_NAME
                    ,keijyou.PRT_FLG
                });
                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }

            //⑤－１
            foreach (T_MANIFEST_KP_NISUGATA niugata in this.form.Nisugatalist)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                    "5-1"
                    ,niugata.REC_NO
                    // 2014/03/24 荷姿コードは非表示(空白)
                    //,niugata.NISUGATA_CD
                    ,string.Empty
                    ,niugata.NISUGATA_NAME
                    ,niugata.PRT_FLG
                });
                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }

            //⑥－１
            for (int i = 0; i < this.form.Houhoulist.Count; i++)
            {
                dr = dt.NewRow();
                record.Clear();
                record.AddRange(new object[]{
                    "6-1"
                    ,this.form.Houhoulist[i].REC_NO
                    ,this.form.Houhoulist[i].SHOBUN_HOUHOU_CD
                    ,this.form.Houhoulist[i].SHOBUN_HOUHOU_NAME
                    ,this.form.Houhoulist[i].PRT_FLG
                });
                dr[0] = this.CreateRowString(record);
                dt.Rows.Add(dr);
            }
            #endregion

            #region 7-1 斜線
            dr = dt.NewRow();
            record.Clear();
            record.AddRange(new object[]{
                    "7-1",
                    false, //this.form.Prtlist[0].SLASH_YUUGAI_FLG, //有害がないためnullになっているのでfalseを直接利用
                    this.form.Prtlist[0].SLASH_BIKOU_FLG,
                    this.form.Prtlist[0].SLASH_CHUUKAN_FLG,
                    this.form.Prtlist[0].SLASH_TSUMIHO_FLG,
                    this.form.Prtlist[0].SLASH_JIZENKYOUGI_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_GYOUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_JYUTAKUSHA3_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA2_FLG,
                    this.form.Prtlist[0].SLASH_UPN_SAKI_GENBA3_FLG,
                    this.form.Prtlist[0].SLASH_B1_FLG,
                    this.form.Prtlist[0].SLASH_B2_FLG,
                    this.form.Prtlist[0].SLASH_B4_FLG,
                    this.form.Prtlist[0].SLASH_B6_FLG,
                    this.form.Prtlist[0].SLASH_D_FLG,
                    this.form.Prtlist[0].SLASH_E_FLG
                });

            dr[0] = this.CreateRowString(record);
            dt.Rows.Add(dr);
            #endregion
            LogUtility.DebugMethodEnd(dt);
            return dt;
        }

        #endregion

        /// <summary>
        /// 10進数→36進数
        /// </summary>
        /// <param name="value">10進数</param>
        /// <param name="mode">進数モード36</param>
        /// <returns></returns>
        private string MyConvertTo36(BigInteger value, BigInteger mode)
        {
            LogUtility.DebugMethodStart(value, mode);
            string retVal = string.Empty;

            if (value / mode == 0)
            {
                retVal = Convert.ToString(Const.ConstCls.BASES[(int)(value % mode)]);
            }
            else
            {
                retVal = this.MyConvertTo36(value / mode, mode) + Convert.ToString(Const.ConstCls.BASES[(int)(value % mode)]);
            }
            LogUtility.DebugMethodEnd(retVal);
            return retVal;
        }

        /// <summary>
        /// 36進数→10進数
        /// </summary>
        /// <param name="value">36進数</param>
        /// <param name="mode">進数モード</param>
        /// <returns></returns>
        private BigInteger MyConvertTo10(string value, int mode)
        {
            LogUtility.DebugMethodStart(value, mode);
            BigInteger ret = 0;

            for (int i = 0; i < value.Length; i++)
            {
                //ALL Z 20ケタとかだと、桁あふれがあるため BigIntegerを利用
                var seki = BigInteger.Multiply(
                    (BigInteger)Const.ConstCls.BASES_36.IndexOf(value.Substring(value.Length - i - 1, 1)),
                    BigInteger.Pow((BigInteger)mode, i));

                ret = ret + seki;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #region 印刷設定
        /// <summary>
        /// 対象となる帳票種類を表す文字列を取得処理
        /// </summary>
        /// <param name="dentaneMode">伝種区分</param>
        /// <param name="insatsuMode">印刷モード</param>
        /// <returns>対象となる帳票種類を表す文字列</returns>
        private string GetTargetReportName(int dentaneMode, int insatsuMode)
        {
            LogUtility.DebugMethodStart(dentaneMode, insatsuMode);

            string targetReportName = string.Empty;

            switch (dentaneMode)
            {
                case (int)Const.ConstCls.enumFormKbn.Chokkou:
                    if (Const.ConstCls.INSATST_MODE_TANHYOU.Equals(insatsuMode))
                    {
                        targetReportName = Const.ConstCls.REPORT_NAME_CHOKKOU_TANHYOU;
                    }
                    else
                    {
                        targetReportName = Const.ConstCls.REPORT_NAME_CHOKKOU_RENHYOU;
                    }
                    break;
                case (int)Const.ConstCls.enumFormKbn.Kenhai:
                    if (Const.ConstCls.INSATST_MODE_TANHYOU.Equals(insatsuMode))
                    {
                        targetReportName =
                           Const.ConstCls.REPORT_NAME_KENHAI_TANHYOU;
                    }
                    else
                    {
                        targetReportName = Const.ConstCls.REPORT_NAME_KENHAI_RENHYOU;
                    }
                    break;
                case (int)Const.ConstCls.enumFormKbn.Sekitai:
                    if (Const.ConstCls.INSATST_MODE_TANHYOU.Equals(insatsuMode))
                    {
                        targetReportName = Const.ConstCls.REPORT_NAME_SEKITAI_TANHYOU;
                    }
                    else
                    {
                        targetReportName = Const.ConstCls.REPORT_NAME_SEKITAI_RENHYOU;
                    }
                    break;
                default:
                    break;
            }

            LogUtility.DebugMethodEnd();

            return targetReportName;

        }
        #endregion

        // 20141031 koukouei 委託契約チェック start
        #region ブランクチェック
        /// <summary>
        /// ブランクチェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckBlank()
        {
            var msgLogic = new MessageBoxShowLogic();
            bool isBlank = true;
            string haikiShuruiCd = "";

            // 空行のみの場合
            if (this.form.Detaillist.Count <= 0)
            {
                isBlank = false;
            }
            
            // 廃棄物種類チェック
            foreach (T_MANIFEST_DETAIL detail in this.form.Detaillist)
            {
                haikiShuruiCd = detail.HAIKI_SHURUI_CD;
                if (!string.IsNullOrEmpty(haikiShuruiCd))
                {
                    isBlank = false;
                }
            }

            if (isBlank && this.form.ManiFlag != 2)
            {
                msgLogic.MessageBoxShow("E176");
                return false;
            }

            return true;
        }
        #endregion

        #region 委託契約書チェック

        /// <summary>
        /// 委託契約書チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckItakukeiyaku()
        {
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                M_SYS_INFO sysInfo = new M_SYS_INFO();
                IM_SYS_INFODao sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

                M_SYS_INFO[] sysInfos = sysInfoDao.GetAllData();
                if (sysInfos != null && sysInfos.Length > 0)
                {
                    sysInfo = sysInfos[0];
                }
                else
                {
                    return true;
                }

                //委託契約チェックDtoを取得
                ItakuCheckDTO checkDto = new ItakuCheckDTO();
                checkDto.MANIFEST_FLG = true;
                checkDto.HAIKI_KBN_CD = (int)Properties.Settings.Default.dentaneMode;
                checkDto.GYOUSHA_CD = this.form.Entrylist[0].HST_GYOUSHA_CD;
                checkDto.GENBA_CD = this.form.Entrylist[0].HST_GENBA_CD;
                checkDto.SAGYOU_DATE = this.form.Entrylist[0].KOUFU_DATE.IsNull ? string.Empty : this.form.Entrylist[0].KOUFU_DATE.Value.ToString();
                checkDto.LIST_HINMEI_HAIKISHURUI = new List<DetailDTO>();

                IM_HAIKI_SHURUIDao daoHaikiShurui = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();
                M_HAIKI_SHURUI cond = new M_HAIKI_SHURUI();
                cond.HAIKI_KBN_CD = (short)checkDto.HAIKI_KBN_CD;

                foreach (T_MANIFEST_DETAIL detail in this.form.Detaillist)
                {
                    DetailDTO detailDto = new DetailDTO();
                    detailDto.CD = detail.HAIKI_SHURUI_CD;

                    cond.HAIKI_SHURUI_CD = detail.HAIKI_SHURUI_CD;
                    var data = daoHaikiShurui.GetDataByCd(cond);
                    if (data != null)
                    {
                        detailDto.NAME = data.HAIKI_SHURUI_NAME_RYAKU;
                    }
                    
                    checkDto.LIST_HINMEI_HAIKISHURUI.Add(detailDto);
                }

                ItakuKeiyakuCheckLogic itakuLogic = new ItakuKeiyakuCheckLogic();
                bool isCheck = itakuLogic.IsCheckItakuKeiyaku(sysInfo, checkDto);
                //委託契約チェックを処理しない場合
                if (isCheck == false)
                {
                    return true;
                }

                //委託契約チェック
                ItakuErrorDTO error = itakuLogic.ItakuKeiyakuCheck(checkDto);

                //エラーなし
                if (error.ERROR_KBN == (short)ITAKU_ERROR_KBN.NONE)
                {
                    return true;
                }

                //元画面に戻る時、エラー背景色設定
                if (sysInfo.ITAKU_KEIYAKU_ALERT_AUTH != 1)
                {
                    this.form.retDto.ERROR_KBN = error.ERROR_KBN;
                    this.form.retDto.DETAIL_ERROR = error.DETAIL_ERROR.ToList();
                }

                bool ret = itakuLogic.ShowError(error, sysInfo.ITAKU_KEIYAKU_ALERT_AUTH, checkDto.MANIFEST_FLG, null, null, null, null, null);
                return ret;
            }
            catch (Seasar.Framework.Exceptions.SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckItakukeiyaku", ex2);
                msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckItakukeiyaku", ex);
                msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        #endregion 委託契約書チェック
        // 20141031 koukouei 委託契約チェック end

        /// <summary>
        /// Toroku
        /// </summary>
        /// <returns></returns>
        internal ConstCls.JikkouResult Toroku()
        {
            var result = ConstCls.JikkouResult.YES;
            try
            {
                LogUtility.DebugMethodStart();
                if (this.form.cbx_ManifestToroku.Checked)
                {
                    if (!this.CheckBlank() || (this.form.ManiFlag == 1 && !this.CheckItakukeiyaku()))
                    {
                        result = ConstCls.JikkouResult.NO;
                        LogUtility.DebugMethodEnd(result);
                        return result;
                    }
                }

                // 印刷部数
                int insatsuBusu = Convert.ToInt32(this.form.txt_InsatsuBusu.Text);
                // 新規モードかつマニ登録するかどうか
                bool isNewEntryMode = Properties.Settings.Default.syoriMode.Equals((int)WINDOW_TYPE.NEW_WINDOW_FLAG);
                if (isNewEntryMode && this.form.cbx_ManifestToroku.Checked)
                {
                    // マニフェスト登録(重複登録チェック)
                    if (Regist(true, insatsuBusu))
                    {
                        result = ConstCls.JikkouResult.DUPLICATION;
                        LogUtility.DebugMethodEnd(result);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Toroku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                result = ConstCls.JikkouResult.ERROR;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
            return result;
        }

        /// <summary>
        /// Print
        /// </summary>
        /// <returns></returns>
        internal ConstCls.JikkouResult Print()
        {
            var result = ConstCls.JikkouResult.YES;
            try
            {
                LogUtility.DebugMethodStart();
                //確認メッセージを表示する
                if (this.msgLogic.MessageBoxShow("C042").Equals(DialogResult.No))
                {
                    result = ConstCls.JikkouResult.NO;
                    LogUtility.DebugMethodEnd(result);
                    return result;
                        
                }

                // 印刷部数が100部以上の場合、確認のポップアップを出す（初期フォーカスは「いいえ」）
                int busuu = 0;
                if (int.TryParse(this.form.txt_InsatsuBusu.Text, out busuu))
                {
                    if (busuu >= 100)
                    {
                        if (this.msgLogic.MessageBoxShow("C091", MessageBoxDefaultButton.Button2, "100枚以上のマニフェスト") == DialogResult.No)
                        {
                            this.form.txt_InsatsuBusu.Focus();
                            this.form.txt_InsatsuBusu.SelectAll();
                            this.form.txt_InsatsuBusu.IsInputErrorOccured = true;
                            result = ConstCls.JikkouResult.NO;
                            LogUtility.DebugMethodEnd(result);
                            return result;
                        }
                    }
                }

                // 印刷部数
                int insatsuBusu = Convert.ToInt32(this.form.txt_InsatsuBusu.Text);
                
                // 印刷用データ作成
                ReportInfoBase reportInfo = null;
                var insatsuMode = (ConstCls.InsatsuMode)Properties.Settings.Default.insatsuMode;
                var dentaneMode = (ConstCls.enumFormKbn)Properties.Settings.Default.dentaneMode;
                switch (dentaneMode)
                {
                    case ConstCls.enumFormKbn.Chokkou:
                        {
                            if (this.form.manifest_mercury_check)
                            {
                                //直行水銀の場合
                                var dt = this.CreateChokkouMercuryManiPrintingData();
                                var reportR691 = new ReportInfoR691();
                                reportR691.OutputFormFullPathName =
                                    this.GetTemplateFileName(ConstCls.enumFormKbn.Chokkou, insatsuMode);
                                reportR691.R691_Report(dt);
                                reportR691.Title = "産廃（直行）水銀マニ";
                                reportInfo = reportR691;
                            }
                            else
                            {
                                //直行の場合
                                var dt = this.CreateChokkouManiPrintingData();
                                var reportR493 = new ReportInfoR493();
                                reportR493.OutputFormFullPathName =
                                    this.GetTemplateFileName(ConstCls.enumFormKbn.Chokkou, insatsuMode);
                                reportR493.R493_Report(dt);
                                reportR493.Title = "産廃（直行）マニフェスト";
                                reportInfo = reportR493;
                            }
                        }
                        break;
                    case ConstCls.enumFormKbn.Kenhai:
                        {
                            if (this.form.manifest_mercury_check)
                            {
                                //建廃水銀の場合
                                var dt = this.CreateKenpaiMercuryManiPrintingData();
                                // インスタンス追加
                                var reportR692 = new ReportInfoR692();
                                reportR692.OutputFormFullPathName =
                                    this.GetTemplateFileName(ConstCls.enumFormKbn.Kenhai, insatsuMode);
                                reportR692.R692_Report(dt);
                                reportR692.Title = "建廃水銀マニ";
                                reportInfo = reportR692;
                            }
                            else
                            {
                                //建廃の場合
                                var dt = this.CreateKenpaiManiPrintingData();
                                // インスタンス追加
                                var reportR495 = new ReportInfoR495();
                                reportR495.OutputFormFullPathName =
                                    this.GetTemplateFileName(ConstCls.enumFormKbn.Kenhai, insatsuMode);
                                reportR495.R495_Report(dt);
                                reportR495.Title = "建廃マニフェスト";
                                reportInfo = reportR495;
                            }  
                        }
                        break;
                    case ConstCls.enumFormKbn.Sekitai:
                        {
                            //積替の場合
                            var dt = this.CreateSekitaiManiPrintingData();
                            var reportR494 = new ReportInfoR494();
                            reportR494.OutputFormFullPathName =
                            this.GetTemplateFileName(ConstCls.enumFormKbn.Sekitai, insatsuMode);
                            reportR494.R494_Report(dt);
                            reportR494.Title = "積替マニフェスト";
                            reportInfo = reportR494;
                        }
                        break;
                }

                //印刷処理
                if (reportInfo != null)
                {
                    var printPopup = new FormReportPrintPopup(reportInfo, reportInfo.ReportID);
                    if (printPopup != null)
                    {
                        printPopup.IsManifestReport = true;
                        printPopup.IsTanpyou = (insatsuMode == ConstCls.InsatsuMode.Single);
                        printPopup.Copie = insatsuBusu;
                        printPopup.PrintInitAction = 1;    // 印刷アプリ初期動作(直印刷)
                        printPopup.PrintXPS();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Jikkou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                result = ConstCls.JikkouResult.ERROR;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
            return result;
        }

        /// <summary>
        /// 交付番号のチェック
        /// </summary>
        /// <param name="Tujyo"></param>
        /// <param name="koufuNo"></param>
        /// <returns></returns>
        public void SetupKoufuNo(SqlInt16 Tujyo, CustomAlphaNumTextBox koufuNo)
        {
            if (Tujyo == 1)
            {
                //通常は11ケタ
                this.form.txt_KoufuNo.CharactersNumber = 11;
                this.form.txt_KoufuNo.MaxLength = 11;
                this.form.txt_KoufuNo.AlphabetLimitFlag = false; //alphabet入力不可
            }
        }
    }
}
