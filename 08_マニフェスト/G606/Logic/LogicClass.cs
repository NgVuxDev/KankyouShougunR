using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.Configuration;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System.IO;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpan
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>ボタンの設定用ファイルパス</summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.JissekiHokokuUnpan.Setting.ButtonSetting.xml";

        /// <summary>
        /// 実績報告書（運搬実績）のForm
        /// </summary>
        public UIForm form { get; set; }

        /// <summary>
        /// 実績報告書（運搬実績）のHeader
        /// </summary>
        public UIHeader headerform { get; set; }

        /// <summary> 親フォーム</summary>
        public BasePopForm parentbaseform { get; set; }

        /// <summary>画面初期表示Flag</summary>
        private bool firstLoadFlg = true;

        /// <summary>Dao</summary>
        public DAOClass dao { get; set; }
        private EntryDAO EntryDao { get; set; }
        private ManiDetailDAO DetailDao { get; set; }
        private UnpanDAO UnpanDao { get; set; }
        private IM_CHIIKIBETSU_BUNRUIDao BunruiDao { get; set; }
        private IM_HAIKI_SHURUIDao HaikishuruiDao { get; set; }
        private IM_GYOUSHADao GyoushaDao { get; set; }
        private IM_GENBADao GenbaDao { get; set; }
        private IM_CHIIKIBETSU_SHOBUNDao ChiikiShobunDao { get; set; }

        /// <summary>
        /// システム情報のDao
        /// </summary>
        public IM_SYS_INFODao sysInfoDao;

        /// <summary>Dao</summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>検索結果</summary>
        public DataTable SearchResult { get; set; }

        /// <summary>検索条件</summary>
        public SearchDto SearchString { get; set; }

        //public ManiDTOClass ManiDTO { get; set; }

        /// <summary>T_JISSEKI_HOUKOKU_ENTRYのEntity</summary>
        public T_JISSEKI_HOUKOKU_ENTRY entry { get; set; }

        /// <summary>T_JISSEKI_HOUKOKU_MANIFEST_DETAILのEntity</summary>
        public List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL> detailList { get; set; }

        /// <summary>T_JISSEKI_HOUKOKU_DETAILのEntity</summary>
        public List<T_JISSEKI_HOUKOKU_UPN_DETAIL> unpanList { get; set; }

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        public M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        private MessageBoxShowLogic MsgBox;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.EntryDao = DaoInitUtility.GetComponent<EntryDAO>();
            this.DetailDao = DaoInitUtility.GetComponent<ManiDetailDAO>();
            this.UnpanDao = DaoInitUtility.GetComponent<UnpanDAO>();
            this.BunruiDao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_BUNRUIDao>();
            this.HaikishuruiDao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();
            this.GyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.GenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.ChiikiShobunDao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_SHOBUNDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm);
        }
        #endregion

        #region 登録/更新/削除
        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            this.LogicalDelete2();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public bool LogicalDelete2()
        {
            LogUtility.DebugMethodStart();

            try
            {
            }
            // 20140625 kayo EV005020 排他チェックのメッセージ不正 end
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registメソッドが正常の場合True
        /// </summary>
        private bool RegistResult = false;
        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            RegistResult = false;
            try
            {
                RegistResult = true;
                // トランザクション開始
                using (Transaction tran = new Transaction())
                {
                    this.EntryDao.Insert(this.entry);
                    foreach (T_JISSEKI_HOUKOKU_MANIFEST_DETAIL detail in this.detailList)
                    {
                        this.DetailDao.Insert(detail);
                    }
                    foreach (T_JISSEKI_HOUKOKU_UPN_DETAIL unpan in this.unpanList)
                    {
                        this.UnpanDao.Insert(unpan);
                    }
                    tran.Commit();
                }

                // 帳票を印刷
                this.PrintView();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// update成功時True
        /// </summary>
        private bool UpdateResult = false;
        /// <summary>
        /// 修正処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            UpdateResult = false;

            try
            {
                UpdateResult = true;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;

            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// 帳票出力
        /// </summary>
        public void PrintView()
        {
            LogUtility.DebugMethodStart();

            if (this.unpanList.Count == 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I008", "データ");
                return;
            }
            DataTable headerData2 = new DataTable();
            headerData2.Columns.Add("HOUKOKU_YEAR");                      // 報告日期
            headerData2.Columns.Add("TEISHUTSU_NAME");                    // 提出先名
            headerData2.Columns.Add("GYOUSHA_ADDRESS");                   // 住所
            headerData2.Columns.Add("GYOUSHA_NAME");                      // 氏名又は名称1(業者名1+業者名2)
            headerData2.Columns.Add("GYOUSHA_DAIHYOU");                   // 氏名又は名称2(代表者)
            headerData2.Columns.Add("GYOUSHA_TEL");                       // 電話番号
            headerData2.Columns.Add("HOUKOKU_TANTO_NAME");                // 担当者の氏名
            headerData2.Columns.Add("HOUKOKU_TITLE1");                    // 報告書見出1
            headerData2.Columns.Add("HOUKOKU_TITLE2");                    // 報告書見出2
            headerData2.Columns.Add("KYOKA_DATA");                        // 許可年月日
            headerData2.Columns.Add("KYOKA_NO");                          // 許可番号
            headerData2.Columns.Add("HOUKOKU_SHOSHIKI");                  // HOKOKU_SYOSHIKI
            headerData2.Columns.Add("HAIKI_KBN");                         // 廃棄区分

            DataTable detailData2 = new DataTable();
            detailData2.Columns.Add("HOUKOKUSHO_BUNRUI_NAME");            // (特別管理)産業廃棄物の種類
            detailData2.Columns.Add("HOUKOKUSHO_BUNRUI_CD");              // 廃棄物コード
            detailData2.Columns.Add("GYOUSHA_NAME");                      // 明細-排出事業者(氏名又は名称)
            detailData2.Columns.Add("HST_GENBA_ADDRESS");                 // 排出事業場(排出元)の所在地
            detailData2.Columns.Add("JYUTAKU_RYOU");                      // 受託量
            detailData2.Columns.Add("JYUTAKU_KBN");                       // 備考
            detailData2.Columns.Add("UNPAN_NAME");                        // 運搬先の事業者の氏名又は名称
            detailData2.Columns.Add("UNPAN_ADDRESS");                     // 運搬先の事業場の所在地
            detailData2.Columns.Add("UNPAN_RYOU");                        // 運搬量
            detailData2.Columns.Add("ITAKUSAKI_KYOKA_NO");                // 許可番号
            detailData2.Columns.Add("ITAKUSAKI_NAME");                    // 委託先の氏名又は名称
            detailData2.Columns.Add("ITAKUSAKI_ADDRESS");                 // 委託先の住所
            detailData2.Columns.Add("ITAKU_RYOU");                        // 委託量

            detailData2.Columns.Add("GROUP_KEY");                         // 排出事業場地域CD
            detailData2.Columns.Add("HST_GENBA_CHIIKI_NAME");             // 排出事業場地域名称

            IM_CHIIKIDao mChiikiDao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
            M_CHIIKI mChiikiData = new M_CHIIKI();

            IM_CHIIKIBETSU_KYOKADao mchiikiBetsuDao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_KYOKADao>();
            M_CHIIKIBETSU_KYOKA mchiikiBetsuData = new M_CHIIKIBETSU_KYOKA();

            IM_GYOUSHADao mGyoshaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            M_GYOUSHA mGyoushaData = new M_GYOUSHA();

            IM_TODOUFUKENDao mTodoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            M_TODOUFUKEN mTodoufukenData = new M_TODOUFUKEN();

            List<T_JISSEKI_HOUKOKU_UPN_DETAIL> detailList = new List<T_JISSEKI_HOUKOKU_UPN_DETAIL>();

            T_JISSEKI_HOUKOKU_ENTRY entry = new T_JISSEKI_HOUKOKU_ENTRY();

            detailList = this.unpanList;
            entry = this.entry;

            DataRow head = headerData2.NewRow();
            // Header情報
            // 提出先名
            mChiikiData = mChiikiDao.GetDataByCd(entry.TEISHUTSU_CHIIKI_CD);

            // 知事または市長名(GOV_OR_MAY_NAME)あります。
            // CHIIKI_NAMEに、xxx県orxxx市があります。
            if (mChiikiData != null)
            {
                if (!string.IsNullOrEmpty(mChiikiData.GOV_OR_MAY_NAME) && !string.IsNullOrEmpty(mChiikiData.CHIIKI_NAME))
                {
                    switch (mChiikiData.CHIIKI_NAME.Substring(mChiikiData.CHIIKI_NAME.Length - 1, 1))
                    {
                        case "県":
                        case "都":
                        case "道":
                        case "府":
                            head["TEISHUTSU_NAME"] = mChiikiData.CHIIKI_NAME + "知事　　" + mChiikiData.GOV_OR_MAY_NAME + "　殿";
                            break;
                        case "市":
                            head["TEISHUTSU_NAME"] = mChiikiData.CHIIKI_NAME + "市長　　" + mChiikiData.GOV_OR_MAY_NAME + "　殿";
                            break;
                    }
                }
            }

            // 産業廃棄物処分業 ・ 特別管理産業廃棄物処分業許可データを取得する
            //mchiikiBetsuData.KYOKA_KBN = entry.GYOUSHA_KBN;
            mchiikiBetsuData.KYOKA_KBN = 1;
            mchiikiBetsuData.GYOUSHA_CD = entry.HOUKOKU_GYOUSHA_CD;
            mchiikiBetsuData.GENBA_CD = String.Empty;
            mchiikiBetsuData.CHIIKI_CD = entry.TEISHUTSU_CHIIKI_CD;
            mchiikiBetsuData = mchiikiBetsuDao.GetDataByPrimaryKey(mchiikiBetsuData);
            // (1.普通)廃棄物
            if (entry.TOKUBETSU_KANRI_KBN.Value == 1 && mchiikiBetsuData != null)
            {
                if (!mchiikiBetsuData.FUTSUU_KYOKA_BEGIN.IsNull)
                {
                    head["KYOKA_DATA"] = mchiikiBetsuData.FUTSUU_KYOKA_BEGIN.Value.ToString();
                }
                head["KYOKA_NO"] = mchiikiBetsuData.FUTSUU_KYOKA_NO;
            }
            // (2.特別)廃棄物
            if (entry.TOKUBETSU_KANRI_KBN.Value == 2 && mchiikiBetsuData != null)
            {
                if (!mchiikiBetsuData.TOKUBETSU_KYOKA_BEGIN.IsNull)
                {
                    head["KYOKA_DATA"] = mchiikiBetsuData.TOKUBETSU_KYOKA_BEGIN.Value.ToString();
                }
                head["KYOKA_NO"] = mchiikiBetsuData.TOKUBETSU_KYOKA_NO;
            }

            mGyoushaData = mGyoshaDao.GetDataByCd(entry.HOUKOKU_GYOUSHA_CD);

            if (mGyoushaData != null)
            {
                mTodoufukenData = mTodoufukenDao.GetDataByCd(mGyoushaData.GYOUSHA_TODOUFUKEN_CD.Value.ToString());
            }

            if (mGyoushaData != null)
            {
                head["GYOUSHA_ADDRESS"] = mTodoufukenData.TODOUFUKEN_NAME + mGyoushaData.GYOUSHA_ADDRESS1 + mGyoushaData.GYOUSHA_ADDRESS2;
                head["GYOUSHA_NAME"] = mGyoushaData.GYOUSHA_NAME1 + mGyoushaData.GYOUSHA_NAME2;
                head["GYOUSHA_DAIHYOU"] = mGyoushaData.GYOUSHA_DAIHYOU;
                head["GYOUSHA_TEL"] = mGyoushaData.GYOUSHA_TEL;
            }
            head["HOUKOKU_YEAR"] = entry.DATE_BEGIN;
            head["HOUKOKU_TANTO_NAME"] = entry.HOUKOKU_TANTO_NAME;
            head["HOUKOKU_TITLE1"] = entry.HOUKOKU_TITLE1;
            head["HOUKOKU_TITLE2"] = entry.HOUKOKU_TITLE2;
            head["HOUKOKU_SHOSHIKI"] = entry.HOUKOKU_SHOSHIKI.Value.ToString();
            head["HAIKI_KBN"] = entry.TOKUBETSU_KANRI_KBN;

            headerData2.Rows.Add(head);
            detailList = (from temp in detailList
                          orderby temp.HST_KEN_KBN ascending,
                                  temp.HST_GENBA_CHIIKI_CD ascending,
                                  temp.HOUKOKUSHO_BUNRUI_CD ascending,
                                  temp.HST_JOU_TODOUFUKEN_CD ascending,
                                  temp.HST_GYOUSHA_CD ascending,
                                  temp.HST_GENBA_CD ascending,
                                  temp.SHOBUN_HOUHOU_CD ascending,
                                  temp.SBN_GENBA_CHIIKI_CD ascending,
                                  temp.SBN_GYOUSHA_CD ascending,
                                  temp.SBN_GENBA_CD ascending,
                                  temp.HIKIWATASHISAKI_CHIIKI_CD ascending,
                                  temp.HIKIWATASHISAKI_CD ascending
                          select temp).ToList();
            // 明細-明細情報
            foreach (T_JISSEKI_HOUKOKU_UPN_DETAIL dt in detailList)
            {
                DataRow row = detailData2.NewRow();

                //(特別管理)産業廃棄物の種類
                row["HOUKOKUSHO_BUNRUI_NAME"] = dt.HOUKOKUSHO_BUNRUI_NAME;

                // 廃棄物コード
                row["HOUKOKUSHO_BUNRUI_CD"] = dt.HOUKOKUSHO_BUNRUI_CD;

                // 氏名又は名称
                if (entry.HST_GYOUSHA_NAME_DISP_KBN == 1)
                {   // 業者名 
                    row["GYOUSHA_NAME"] = dt.HST_GYOUSHA_NAME;
                    // 排出事業場(排出元)の所在地
                    //row["HST_GENBA_ADDRESS"] = dt.HST_GYOUSHA_ADDRESS;
                }
                else if (entry.HST_GYOUSHA_NAME_DISP_KBN == 2)
                {   // 現場名
                    row["GYOUSHA_NAME"] = dt.HST_GENBA_NAME;
                }

                // 排出事業場(排出元)の所在地
                row["HST_GENBA_ADDRESS"] = dt.HST_GENBA_ADDRESS;

                // 受託量
                if (!dt.JYUTAKU_RYOU.IsNull)
                {
                    row["JYUTAKU_RYOU"] = ConvertSuuryo(dt.JYUTAKU_RYOU);
                }

                // 備考
                row["JYUTAKU_KBN"] = dt.JYUTAKU_KBN;

                // 運搬先の事業者の氏名又は名称
                row["UNPAN_NAME"] = dt.SBN_GENBA_NAME;

                // 運搬先の事業場の所在地
                row["UNPAN_ADDRESS"] = dt.SBN_GENBA_ADDRESS;

                // 運搬量
                if (!dt.UPN_RYOU.IsNull)
                {
                    row["UNPAN_RYOU"] = ConvertSuuryo(dt.UPN_RYOU.Value);
                }

                // 許可番号
                row["ITAKUSAKI_KYOKA_NO"] = dt.HIKIWATASHISAKI_KYOKA_NO;

                // 委託先の氏名又は名称
                row["ITAKUSAKI_NAME"] = dt.HIKIWATASHISAKI_NAME;

                // 委託先の住所
                row["ITAKUSAKI_ADDRESS"] = dt.HIKIWATASHISAKI_ADDRESS;

                // 委託量
                if (!dt.HIKIWATASHI_RYOU.IsNull)
                {
                    row["ITAKU_RYOU"] = ConvertSuuryo(dt.HIKIWATASHI_RYOU);
                }

                if (!dt.HST_KEN_KBN.IsNull)
                {
                    if (1 == dt.HST_KEN_KBN.Value)
                    {
                        row["HST_GENBA_CHIIKI_NAME"] = mChiikiData.CHIIKI_NAME + "内間運搬合計";
                    }
                    else if (2 == dt.HST_KEN_KBN.Value)
                    {
                        row["HST_GENBA_CHIIKI_NAME"] = mChiikiData.CHIIKI_NAME + "からの運搬合計";
                    }
                    else if (3 == dt.HST_KEN_KBN.Value)
                    {
                        row["HST_GENBA_CHIIKI_NAME"] = mChiikiData.CHIIKI_NAME + "への運搬合計";
                    }
                    else if (4 == dt.HST_KEN_KBN.Value)
                    {
                        row["HST_GENBA_CHIIKI_NAME"] = "県外間の運搬合計";
                    }
                }

                // GROUPKEY
                //row["GROUP_KEY"] = dt.HST_KEN_KBN.IsNull ? null as int? : dt.HST_KEN_KBN.Value;
                row["GROUP_KEY"] = dt.HOUKOKUSHO_BUNRUI_CD + dt.HST_GENBA_CHIIKI_CD + (dt.HST_KEN_KBN.IsNull ? null as int? : dt.HST_KEN_KBN.Value);

                detailData2.Rows.Add(row);
            }

            ReportInfoR608 reportInfo = new ReportInfoR608();
            reportInfo.Title = "実績報告書（運搬実績）";
            reportInfo.R608_Reprt(headerData2, detailData2);
            FormReportPrintPopup formReport;
            formReport = new FormReportPrintPopup(reportInfo, "R608");
            formReport.Caption = "業務報告書";

            // 印刷アプリ初期動作(プレビュー)
            formReport.PrintInitAction = 2;
            formReport.PrintXPS();
            formReport.Dispose();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 初期化

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.firstLoadFlg)
                {
                    // フォームインスタンスを取得
                    this.parentbaseform = (BasePopForm)this.form.Parent;
                    this.headerform = (UIHeader)parentbaseform.headerForm;

                    // ボタンのテキストを初期化
                    this.ButtonInit();

                    // イベントの初期化処理
                    this.EventInit();

                    // システム設定を読み込む
                    M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
                    if (sysInfo != null)
                    {
                        this.sysInfoEntity = sysInfo[0];
                    }

                    if (AppConfig.IsManiLite)
                    {
                        // マニライト版(C8)の初期化処理
                        ManiLiteInit();
                    }

                    this.allControl = this.form.allControl;
                }

                // コントロールを初期化
                this.ControlInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
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
        /// ラジオボタン初期化
        /// </summary>
        internal void InitRadioButton()
        {
            this.headerform.txt_DenshiManiKbn.Text = "1";
            this.headerform.txt_CSVOutput.Text = "1";
            this.form.txt_GyoshuKbn.Text = "3";
            this.form.txt_TeishutuShoshiki.Text = "1";
            this.form.txt_HaikiKbn.Text = "1";
            this.form.txt_HaishutuJigyoushaName.Text = "1";
            this.form.txt_TumikaeHokanKbn.Text = "2";
            this.form.txt_AddressSearchCondition.Text = "1";
            this.form.txt_UnpanSearchCondition.Text = "1";
            this.form.txt_JishaSearchCondition.Text = "1";
            this.form.txt_TashaSearchCondition.Text = "1";
            this.form.chk_Out_Out.Checked = false;
            this.form.chk_In_In.Checked = true;
            this.form.chk_In_Out.Checked = true;
            this.form.chk_Out_In.Checked = true;
        }

        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        private void SetErrorFocus()
        {
            Control target = null;
            foreach (Control control in this.form.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        if (target != null)
                        {
                            if (target.TabIndex > control.TabIndex)
                            {
                                target = control;
                            }
                        }
                        else
                        {
                            target = control;
                        }
                    }
                }
            }
            //ヘッダーチェック
            foreach (Control control in this.headerform.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        target = control;
                    }
                }
            }
            if (target != null)
            {
                target.Focus();
            }
        }

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        public void ControlInit()
        {
            LogUtility.DebugMethodStart();
            DateTime now = this.parentbaseform.sysDate;
            this.headerform.txt_HoukokuTantoushaName.Text = string.Empty;
            this.form.txt_ChohyouName.Text = string.Empty;
            this.form.cdate_TeishutuDay.Value = now;
            this.form.txt_GyousyaCd.Text = string.Empty;
            this.form.txt_GyousyaName.Text = string.Empty;
            this.form.txt_TeishutuSakiCD.Text = string.Empty;
            this.form.txt_TeishutuSakiName.Text = string.Empty;
            this.form.HIDUKE_FROM.Value = Convert.ToDateTime(string.Format("{0}/04/01", now.Year - 1));
            this.form.HIDUKE_TO.Value = Convert.ToDateTime(string.Format("{0}/04/01", now.Year)).AddDays(-1);

            //前年・翌年・FROM日付変更時に同様の処理を入れているので、見出し１行目の文言変更時はそちらも実施する
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
            ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
            this.form.txt_HokokushoTitle1.SetResultText(Convert.ToDateTime(this.form.HIDUKE_FROM.Value).ToString("gg", ci) + "○○年度の産業廃棄物（特管）の処理実績について、廃棄物の処理及び清掃に関する");
            this.form.txt_HokokushoTitle2.Text = "法律施行細則第１４条第３項の規定により、次のとおり報告します。";
            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BasePopForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BasePopForm parentform = (BasePopForm)this.form.Parent;

            //前年ボタン(F1)イベント生成
            parentform.bt_func1.Click += new EventHandler(this.form.btnPrevious_Click);

            //次年ボタン(F2)イベント生成
            parentform.bt_func2.Click += new EventHandler(this.form.btnNext_Click); 

            //処分実績ボタン(F3)イベント生成
            parentform.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);
            //処理施設ボタン(F4)イベント生成
            parentform.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);
            //ＣＳＶ出力ボタン(F6)イベント生成
            parentform.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);
            //実行ボタン(F9)イベント生成
            parentform.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);
            //報告事業者Validatingイベント生成
            this.form.txt_GyousyaCd.Validating += new CancelEventHandler(this.form.txt_GyousyaCd_Validating);
            //提出先Validatingイベント生成
            this.form.txt_TeishutuSakiCD.Validating += new CancelEventHandler(this.form.txt_TeishutuSakiCD_Validating);

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 処分実績,処理施設画面の遷移機能を非表示

            // 初期化
            BasePopForm parentform = (BasePopForm)this.form.Parent;
            parentform.bt_func3.Text = string.Empty;
            parentform.bt_func4.Text = string.Empty;
            parentform.bt_func3.Enabled = false;
            parentform.bt_func4.Enabled = false;

            // イベント削除
            //処分実績ボタン(F3)イベント
            parentform.bt_func3.Click -= new EventHandler(this.form.bt_func3_Click);
            //処理施設ボタン(F4)イベント
            parentform.bt_func4.Click -= new EventHandler(this.form.bt_func4_Click);
        }

        #endregion

        #region マスタ検索処理
        /// <summary>
        /// 業者検索処理
        /// </summary>
        /// <param name="cd">CD</param>
        internal M_GYOUSHA[] GetGyousha(string cd, out bool catchErr)
        {
            M_GYOUSHA[] results = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(cd);
                M_GYOUSHA dto = new M_GYOUSHA();
                dto.GYOUSHA_CD = cd;
                dto.JISHA_KBN = true;
                // 20151023 BUNN #12040 STR
                dto.UNPAN_JUTAKUSHA_KAISHA_KBN = true;
                dto.GYOUSHAKBN_MANI = true;
                // 20151023 BUNN #12040 END
                dto.ISNOT_NEED_DELETE_FLG = true;
                IM_GYOUSHADao dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                results = dao.GetAllValidData(dto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(results, catchErr);
            }
            return results;
        }

        /// <summary>
        /// 地域検索処理
        /// </summary>
        /// <param name="cd">CD</param>
        internal M_CHIIKI[] GetChiiki(string cd, out bool catchErr)
        {
            M_CHIIKI[] results = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(cd);
                M_CHIIKI dto = new M_CHIIKI();
                dto.CHIIKI_CD = cd;
                dto.ISNOT_NEED_DELETE_FLG = true;
                IM_CHIIKIDao dao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
                results = dao.GetAllValidData(dto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetChiiki", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(results, catchErr);
            }
            return results;
        }
        #endregion

        #region ファンクション処理
        /// <summary>
        /// 処分実績処理
        /// </summary>
        internal void ShobunJiseki()
        {
            try
            {
                LogUtility.DebugMethodStart();
                FormManager.OpenFormWithAuth("G134", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShobunJiseki", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 処分施設処理
        /// </summary>
        internal void ShobunShisetu()
        {
            try
            {
                LogUtility.DebugMethodStart();
                FormManager.OpenFormWithAuth("G603", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShobunShisetu", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        internal bool CSVOutput()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                FormManager.OpenFormModal("G610", this.SearchString);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean InputCheck()
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                var allControlAndHeaderControls = allControl.ToList();
                allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.headerform));
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (this.form.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.SetErrorFocus();
                    isErr = true;
                    LogUtility.DebugMethodEnd(isErr);
                    return isErr;
                }

                if (!this.form.chk_In_In.Checked && !this.form.chk_In_Out.Checked && !this.form.chk_Out_In.Checked && !this.form.chk_Out_Out.Checked)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E027", "運搬場所抽出条件");
                    isErr = true;
                    LogUtility.DebugMethodEnd(isErr);
                    return isErr;
                }

                DateTime dtpFrom = DateTime.Parse(this.form.HIDUKE_FROM.GetResultText());
                DateTime dtpTo = DateTime.Parse(this.form.HIDUKE_TO.GetResultText());
                DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                if (0 < diff)
                {
                    //対象期間内でないならエラーメッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                    this.form.HIDUKE_TO.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E030", this.form.HIDUKE_FROM.DisplayItemName, this.form.HIDUKE_TO.DisplayItemName);
                    this.form.HIDUKE_FROM.Select();
                    this.form.HIDUKE_FROM.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("InputCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

        /// <summary>
        /// 存在チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheck(out bool catchErr)
        {
            bool ret = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();
                SearchString = new SearchDto();
                SearchString.HOUKOKU_TANTOUSHA_NAME = this.headerform.txt_HoukokuTantoushaName.Text;
                SearchString.DENSHI_MANIFEST_KBN = Convert.ToInt16(this.headerform.txt_DenshiManiKbn.Text);
                SearchString.CSV_SHUKEI_KBN = Convert.ToInt16(this.headerform.txt_CSVOutput.Text);
                SearchString.CHOUHYO_NAME = this.form.txt_ChohyouName.Text;
                SearchString.TEISHUTU_DATE = Convert.ToDateTime(this.form.cdate_TeishutuDay.Value);
                SearchString.HOUKOKU_JIGYOUSHA_CD = this.form.txt_GyousyaCd.Text;
                SearchString.JISHA_GYOUSHU_KBN = Convert.ToInt16(this.form.txt_GyoshuKbn.Text);
                SearchString.TEISHUTUSAKI_CD = this.form.txt_TeishutuSakiCD.Text;
                SearchString.HOUKOKUSHO_TITLE1 = this.form.txt_HokokushoTitle1.Text;
                SearchString.HOUKOKUSHO_TITLE2 = this.form.txt_HokokushoTitle2.Text;
                SearchString.HAIKIBUTU_KBN = Convert.ToInt16(this.form.txt_HaikiKbn.Text);
                SearchString.DATE_FROM = Convert.ToDateTime(this.form.HIDUKE_FROM.Value);
                SearchString.DATE_TO = Convert.ToDateTime(this.form.HIDUKE_TO.Value);
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
                ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
                SearchString.DATE_FROM_YEAR = SearchString.DATE_FROM.ToString("gy年", ci);
                SearchString.HST_GYOUSHA_KBN = this.form.txt_HaishutuJigyoushaName.Text;
                SearchString.CHUSHUTU_JOKEN_KBN = (this.form.chk_Out_Out.Checked ? "1" : "0")
                                                + (this.form.chk_Out_In.Checked ? "1" : "0")
                                                + (this.form.chk_In_Out.Checked ? "1" : "0")
                                                + (this.form.chk_In_In.Checked ? "1" : "0");
                List<int> list = new List<int>();
                if (this.form.chk_Out_Out.Checked)
                {
                    list.Add(4);
                }
                if (this.form.chk_Out_In.Checked)
                {
                    list.Add(3);
                }
                if (this.form.chk_In_Out.Checked)
                {
                    list.Add(2);
                }
                if (this.form.chk_In_In.Checked)
                {
                    list.Add(1);
                }
                SearchString.CHUSHUTU_JOKEN_KBN_ARRAY = list.ToArray();
                SearchString.TUMIKAE_HOKAN_KBN = Convert.ToInt16(this.form.txt_TumikaeHokanKbn.Text);
                SearchString.JUSHO_CHUSHUTU_JOKEN = Convert.ToInt16(this.form.txt_AddressSearchCondition.Text);
                SearchString.UNPAN_SAIITAKU_JOKEN = Convert.ToInt16(this.form.txt_UnpanSearchCondition.Text);
                SearchString.JISHA_KBN = Convert.ToInt16(this.form.txt_JishaSearchCondition.Text);
                SearchString.TASHA_ITAKU_KYOKA_NO = Convert.ToInt16(this.form.txt_TashaSearchCondition.Text);
                SearchResult = dao.GetManiData(SearchString);
                if (SearchResult.Rows.Count == 0)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = true;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// データチェック
        /// </summary>
        /// <returns></returns>
        internal Boolean DataCheck(List<ManiDTOClass> dtoList)
        {
            var messageShowLogic = new MessageBoxShowLogic();

            if (dtoList.Count == 0)
            {
                return true;
            }
            for (int i = 0; i < dtoList.Count; i++)
            {
                foreach (T_JISSEKI_HOUKOKU_UPN_DETAIL upn in dtoList[i].upnList)
                {
                    if (string.IsNullOrEmpty(upn.HAIKI_SHURUI_CD)
                        || string.IsNullOrEmpty(upn.HST_GYOUSHA_CD)
                        || string.IsNullOrEmpty(upn.HST_GENBA_CD)
                        )
                    {
                        DialogResult result = messageShowLogic.MessageBoxShow("C077", "マニフェスト伝票", "マニチェック表");
                        if (result == DialogResult.Yes)
                        {
                            FormManager.OpenFormWithAuth("G124", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                            // 20141209 koukouei 実績報告書　フィードバック対応 start
                            return true;
                            // 20141209 koukouei 実績報告書　フィードバック対応 end
                        }
                        // 20141209 koukouei 実績報告書　フィードバック対応 start
                        break;
                        //return true;
                        // 20141209 koukouei 実績報告書　フィードバック対応 end
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Entry登録データ作成
        /// </summary>
        /// <returns></returns>
        internal bool CreateEntry()
        {
            bool ret = true;
            LogUtility.DebugMethodStart();
            try
            {
                this.entry = new T_JISSEKI_HOUKOKU_ENTRY();
                this.entry.SYSTEM_ID = this.createSystemIdForJissekiHokoku();
                this.entry.SEQ = 1;
                this.entry.REPORT_ID = 3;
                this.entry.HOUKOKU_YEAR = Convert.ToDateTime(this.form.HIDUKE_FROM.Value).ToString("yyyy") + "年度";
                this.entry.DENMANI_KBN = Convert.ToInt16(this.headerform.txt_DenshiManiKbn.Text);
                this.entry.HOZON_NAME = this.form.txt_ChohyouName.Text;
                this.entry.TEISHUTU_DATE = Convert.ToDateTime(this.form.cdate_TeishutuDay.Value);
                this.entry.HOUKOKU_GYOUSHA_CD = this.form.txt_GyousyaCd.Text;
                this.entry.GYOUSHA_KBN = Convert.ToInt16(this.form.txt_GyoshuKbn.Text);
                this.entry.TEISHUTSU_CHIIKI_CD = this.form.txt_TeishutuSakiCD.Text;
                this.entry.TEISHUTSU_NAME = this.form.txt_TeishutuSakiName.Text;
                this.entry.HOUKOKU_SHOSHIKI = Convert.ToInt16(this.form.txt_TeishutuShoshiki.Text);
                this.entry.HOUKOKU_TITLE1 = this.form.txt_HokokushoTitle1.Text;
                this.entry.HOUKOKU_TITLE2 = this.form.txt_HokokushoTitle2.Text;
                this.entry.TOKUBETSU_KANRI_KBN = Convert.ToInt16(this.form.txt_HaikiKbn.Text);
                this.entry.DATE_BEGIN = Convert.ToDateTime(this.form.HIDUKE_FROM.Value);
                this.entry.DATE_END = Convert.ToDateTime(this.form.HIDUKE_TO.Value);
                this.entry.HST_GYOUSHA_NAME_DISP_KBN = Convert.ToInt16(this.form.txt_HaishutuJigyoushaName.Text);
                this.entry.ADDRESS_KBN = Convert.ToInt16(this.form.txt_AddressSearchCondition.Text);
                this.entry.SAI_ITAKU_KBN = Convert.ToInt16(this.form.txt_UnpanSearchCondition.Text);
                this.entry.HOUKOKU_TANTO_NAME = this.headerform.txt_HoukokuTantoushaName.Text;
                this.entry.TMH_KBN = Convert.ToInt16(this.form.txt_TumikaeHokanKbn.Text);
                this.entry.JISHA_HST_KBN = Convert.ToInt16(this.form.txt_JishaSearchCondition.Text);
                this.entry.TASHA_KYOKA_KBN = Convert.ToInt16(this.form.txt_TashaSearchCondition.Text);
                this.entry.KEN_KBN = Convert.ToInt32((this.form.chk_Out_Out.Checked ? "1" : "0")
                                        + (this.form.chk_Out_In.Checked ? "1" : "0")
                                        + (this.form.chk_In_In.Checked ? "1" : "0")
                                        + (this.form.chk_In_Out.Checked ? "1" : "0"));

                new DataBinderLogic<T_JISSEKI_HOUKOKU_ENTRY>(this.entry).SetSystemProperty(this.entry, false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntry", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 運搬登録データ作成
        /// </summary>
        /// <returns></returns>
        internal int CreateUnpan()
        {
            int ret = 0;
            LogUtility.DebugMethodStart();
            try
            {
                this.unpanList = new List<T_JISSEKI_HOUKOKU_UPN_DETAIL>();
                this.detailList = new List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL>();

                if (this.SearchResult == null || this.SearchResult.Rows.Count == 0)
                {
                    ret = -1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                List<ManiDTOClass> maniDtoList = new List<ManiDTOClass>();
                Dictionary<string, int> tempDic = new Dictionary<string, int>();

                foreach (DataRow row in this.SearchResult.Rows)
                {
                    if (this.SearchString.TUMIKAE_HOKAN_KBN == 1 || (this.SearchString.TUMIKAE_HOKAN_KBN == 2 && (Convert.ToString(row["HAIKI_KBN_CD"]) == "1")))
                    {
                        ManiDTOClass dto = new ManiDTOClass();
                        dto = this.createManiDto(dto, row, true);
                        maniDtoList.Add(dto);
                    }
                    else
                    {
                        T_JISSEKI_HOUKOKU_UPN_DETAIL unpan = new T_JISSEKI_HOUKOKU_UPN_DETAIL();
                        bool new_data = true;
                        if (tempDic.ContainsKey(Convert.ToString(row["HAIKI_KBN_CD"]) + "_" + Convert.ToString(row["SYSTEM_ID"]) + "_" + Convert.ToInt32(row["DETAIL_SYSTEM_ID"])))
                        {
                            var tempInt = tempDic[Convert.ToString(row["HAIKI_KBN_CD"]) + "_" + Convert.ToString(row["SYSTEM_ID"]) + "_" + Convert.ToInt32(row["DETAIL_SYSTEM_ID"])];
                            maniDtoList[tempInt] = this.createManiDto(maniDtoList[tempInt], row, false);
                            new_data = false;
                        }
                        if (new_data)
                        {
                            ManiDTOClass dto = new ManiDTOClass();
                            dto = this.createManiDto(dto, row, true);
                            maniDtoList.Add(dto);
                            tempDic.Add(Convert.ToString(row["HAIKI_KBN_CD"]) + "_" + Convert.ToString(row["SYSTEM_ID"]) + "_" + Convert.ToInt32(row["DETAIL_SYSTEM_ID"]), maniDtoList.Count() - 1);
                        }
                    }
                }

                // 積替マニ　と電マニを　最終データに変更
                if (this.SearchString.TUMIKAE_HOKAN_KBN == 2)
                {
                    List<ManiDTOClass> newManiDtoList = new List<ManiDTOClass>();
                    foreach (ManiDTOClass maniDto in maniDtoList)
                    {
                        ManiDTOClass newDto = new ManiDTOClass();
                        if (maniDto.HAIKI_KBN_CD[0] == 2)
                        {
                            if (maniDto.JYUTAKU_JISHA_KBN.Count > 1)
                            {
                                if (!maniDto.JYUTAKU_JISHA_KBN[0].IsNull && !maniDto.JYUTAKU_JISHA_KBN[1].IsNull)
                                {
                                    if (maniDto.JYUTAKU_JISHA_KBN[0].Value == 1 && maniDto.JYUTAKU_JISHA_KBN[1].Value == 1)
                                    {
                                        if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniDto.upnList[1].HST_KEN_KBN.Value) != -1)
                                        {
                                            newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                            newDto.SEQ.Add(maniDto.SEQ[0]);
                                            newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                            newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                            newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                            newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                            newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                            newDto.JYUTAKU_JISHA_KBN.Add(1);
                                            newDto.upnList.Add(maniDto.upnList[1]);
                                        }
                                    }
                                    else if (maniDto.JYUTAKU_JISHA_KBN[0].Value == 1 && maniDto.JYUTAKU_JISHA_KBN[1].Value == 0)
                                    {
                                        maniDto.upnList[0].SBN_GENBA_CD = string.Empty;
                                        maniDto.upnList[0].SBN_GENBA_NAME = string.Empty;
                                        maniDto.upnList[0].SBN_GENBA_ADDRESS = string.Empty;
                                        maniDto.upnList[0].SBN_GENBA_CHIIKI_CD = string.Empty;
                                        maniDto.upnList[0].SBN_GENBA_KYOKA_NO = string.Empty;
                                        maniDto.upnList[0].SBN_GYOUSHA_CD = string.Empty;
                                        maniDto.upnList[0].SBN_GYOUSHA_NAME = string.Empty;
                                        maniDto.upnList[0].SBN_GYOUSHA_ADDRESS = string.Empty;
                                        maniDto.upnList[0].UPN_RYOU = SqlDecimal.Null;
                                        if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniDto.upnList[0].HST_KEN_KBN.Value) != -1)
                                        {
                                            newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                            newDto.SEQ.Add(maniDto.SEQ[0]);
                                            newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                            newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                            newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                            newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                            newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                            newDto.JYUTAKU_JISHA_KBN.Add(1);
                                            newDto.upnList.Add(maniDto.upnList[0]);
                                        }
                                    }
                                    else if (maniDto.JYUTAKU_JISHA_KBN[0].Value == 0 && maniDto.JYUTAKU_JISHA_KBN[1].Value == 1)
                                    {
                                        maniDto.upnList[1].HST_GYOUSHA_ADDRESS = maniDto.upnList[0].SBN_GYOUSHA_ADDRESS;
                                        maniDto.upnList[1].HST_GENBA_ADDRESS = maniDto.upnList[0].SBN_GENBA_ADDRESS;
                                        maniDto.upnList[1].HST_JOU_CHIIKI_CD = maniDto.upnList[0].UPNSAKI_JOU_CHIIKI_CD;
                                        if (maniDto.upnList[1].HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniDto.upnList[1].UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniDto.upnList[1].HST_KEN_KBN = 1;
                                        }
                                        else if (maniDto.upnList[1].HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD != maniDto.upnList[1].UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniDto.upnList[1].HST_KEN_KBN = 2;
                                        }
                                        else if (maniDto.upnList[1].HST_JOU_CHIIKI_CD != SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniDto.upnList[1].UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniDto.upnList[1].HST_KEN_KBN = 3;
                                        }
                                        else
                                        {
                                            maniDto.upnList[1].HST_KEN_KBN = 4;
                                        }
                                        if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniDto.upnList[1].HST_KEN_KBN.Value) != -1)
                                        {
                                            newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                            newDto.SEQ.Add(maniDto.SEQ[0]);
                                            newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                            newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                            newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                            newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                            newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                            newDto.JYUTAKU_JISHA_KBN.Add(1);
                                            newDto.upnList.Add(maniDto.upnList[1]);
                                        }
                                    }
                                }
                                else if (maniDto.JYUTAKU_JISHA_KBN[0].IsNull || maniDto.JYUTAKU_JISHA_KBN[1].IsNull)
                                {
                                    if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniDto.upnList[1].HST_KEN_KBN.Value) != -1)
                                    {
                                        newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                        newDto.SEQ.Add(maniDto.SEQ[0]);
                                        newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                        newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                        newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                        newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                        newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                        newDto.JYUTAKU_JISHA_KBN.Add(1);
                                        newDto.upnList.Add(maniDto.upnList[1]);
                                    }
                                }
                            }
                            else
                            {
                                if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniDto.upnList[0].HST_KEN_KBN.Value) != -1)
                                {
                                    newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                    newDto.SEQ.Add(maniDto.SEQ[0]);
                                    newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                    newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                    newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                    newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                    newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                    newDto.JYUTAKU_JISHA_KBN.Add(1);
                                    newDto.upnList.Add(maniDto.upnList[0]);
                                }
                            }
                            if (newDto.upnList.Count > 0)
                            {
                                newManiDtoList.Add(newDto);
                            }
                        }
                        else if (maniDto.HAIKI_KBN_CD[0] == 3 || maniDto.HAIKI_KBN_CD[0] == 4)
                        {
                            int start = 0;
                            bool startFlg = false;
                            for (int i = 0; i < maniDto.JYUTAKU_JISHA_KBN.Count; i++)
                            {
                                T_JISSEKI_HOUKOKU_UPN_DETAIL maniUpn = new T_JISSEKI_HOUKOKU_UPN_DETAIL();
                                this.copyEntity(maniUpn, maniDto.upnList[i]);
                                if (maniDto.JYUTAKU_JISHA_KBN[i] == 1 && !startFlg)
                                {
                                    start = i;
                                    startFlg = true;
                                }

                                if (maniDto.JYUTAKU_JISHA_KBN[i] == 0 && startFlg)
                                {
                                    if (start == 0)
                                    {
                                        if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 1;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD != maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 2;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD != SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 3;
                                        }
                                        else
                                        {
                                            maniUpn.HST_KEN_KBN = 4;
                                        }
                                        if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniUpn.HST_KEN_KBN.Value) != -1)
                                        {
                                            if (newDto.SYSTEM_ID.Count == 0)
                                            {
                                                newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                                newDto.SEQ.Add(maniDto.SEQ[0]);
                                                newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                                newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                                newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                                newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                                newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                                newDto.JYUTAKU_JISHA_KBN.Add(1);
                                            }

                                            maniUpn.SBN_GENBA_CD = string.Empty;
                                            maniUpn.SBN_GENBA_NAME = string.Empty;
                                            maniUpn.SBN_GENBA_ADDRESS = string.Empty;
                                            maniUpn.SBN_GENBA_CHIIKI_CD = string.Empty;
                                            maniUpn.SBN_GENBA_KYOKA_NO = string.Empty;
                                            maniUpn.SBN_GYOUSHA_CD = string.Empty;
                                            maniUpn.SBN_GYOUSHA_NAME = string.Empty;
                                            maniUpn.SBN_GYOUSHA_ADDRESS = string.Empty;
                                            maniUpn.UPN_RYOU = SqlDecimal.Null;
                                            maniUpn.HIKIWATASHISAKI_CD = maniDto.upnList[i - 1].HIKIWATASHISAKI_CD;
                                            maniUpn.HIKIWATASHISAKI_ADDRESS = maniDto.upnList[i - 1].HIKIWATASHISAKI_ADDRESS;
                                            maniUpn.HIKIWATASHISAKI_CHIIKI_CD = maniDto.upnList[i - 1].HIKIWATASHISAKI_CHIIKI_CD;
                                            maniUpn.HIKIWATASHISAKI_KYOKA_NO = maniDto.upnList[i - 1].HIKIWATASHISAKI_KYOKA_NO;
                                            maniUpn.HIKIWATASHISAKI_NAME = maniDto.upnList[i - 1].HIKIWATASHISAKI_NAME;
                                            maniUpn.HIKIWATASHI_RYOU = maniDto.upnList[i - 1].HIKIWATASHI_RYOU;
                                            newDto.upnList.Add(maniUpn);
                                        }
                                    }
                                    else
                                    {
                                        maniUpn.HST_GENBA_ADDRESS = maniDto.upnList[start - 1].SBN_GENBA_ADDRESS;
                                        maniUpn.HST_GENBA_CHIIKI_CD = maniDto.upnList[start - 1].SBN_GENBA_CHIIKI_CD;
                                        maniUpn.HST_GYOUSHA_ADDRESS = maniDto.upnList[start - 1].SBN_GYOUSHA_ADDRESS;
                                        maniUpn.HST_JOU_CHIIKI_CD = maniDto.upnList[start - 1].UPNSAKI_JOU_CHIIKI_CD;
                                        if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniDto.upnList[start].UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 1;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD != maniDto.upnList[start].UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 2;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD != SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniDto.upnList[start].UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 3;
                                        }
                                        else
                                        {
                                            maniUpn.HST_KEN_KBN = 4;
                                        }
                                        if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniUpn.HST_KEN_KBN.Value) != -1)
                                        {
                                            if (newDto.SYSTEM_ID.Count == 0)
                                            {
                                                newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                                newDto.SEQ.Add(maniDto.SEQ[0]);
                                                newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                                newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                                newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                                newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                                newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                                newDto.JYUTAKU_JISHA_KBN.Add(1);
                                            }

                                            maniUpn.HIKIWATASHISAKI_CD = maniDto.upnList[i - 1].HIKIWATASHISAKI_CD;
                                            maniUpn.HIKIWATASHISAKI_ADDRESS = maniDto.upnList[i - 1].HIKIWATASHISAKI_ADDRESS;
                                            maniUpn.HIKIWATASHISAKI_CHIIKI_CD = maniDto.upnList[i - 1].HIKIWATASHISAKI_CHIIKI_CD;
                                            maniUpn.HIKIWATASHISAKI_KYOKA_NO = maniDto.upnList[i - 1].HIKIWATASHISAKI_KYOKA_NO;
                                            maniUpn.HIKIWATASHISAKI_NAME = maniDto.upnList[i - 1].HIKIWATASHISAKI_NAME;
                                            newDto.upnList.Add(maniUpn);
                                        }
                                    }
                                    startFlg = false;
                                }

                                int jishaCount = 0;
                                var tempJishaList = maniDto.JYUTAKU_JISHA_KBN.Where(w => (bool)(!w.IsNull && w == 1)).DefaultIfEmpty().ToArray();
                                if (tempJishaList != null || tempJishaList.Count() > 0)
                                {
                                    jishaCount = tempJishaList.Count();
                                }

                                if (i == jishaCount - 1 && maniDto.JYUTAKU_JISHA_KBN[i] == 1)
                                {
                                    if (start == 0)
                                    {
                                        if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 1;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD != maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 2;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD != SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 3;
                                        }
                                        else
                                        {
                                            maniUpn.HST_KEN_KBN = 4;
                                        }
                                        if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniUpn.HST_KEN_KBN.Value) != -1)
                                        {
                                            if (newDto.SYSTEM_ID.Count == 0)
                                            {
                                                newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                                newDto.SEQ.Add(maniDto.SEQ[0]);
                                                newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                                newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                                newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                                newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                                newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                                newDto.JYUTAKU_JISHA_KBN.Add(1);
                                            }
                                            newDto.upnList.Add(maniUpn);
                                        }
                                    }
                                    else
                                    {
                                        maniUpn.HST_GENBA_ADDRESS = maniDto.upnList[start - 1].SBN_GENBA_ADDRESS;
                                        maniUpn.HST_GENBA_CHIIKI_CD = maniDto.upnList[start - 1].SBN_GENBA_CHIIKI_CD;
                                        maniUpn.HST_GYOUSHA_ADDRESS = maniDto.upnList[start - 1].SBN_GYOUSHA_ADDRESS;
                                        maniUpn.HST_JOU_CHIIKI_CD = maniDto.upnList[start - 1].UPNSAKI_JOU_CHIIKI_CD;
                                        if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 1;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD != maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 2;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD != SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 3;
                                        }
                                        else
                                        {
                                            maniUpn.HST_KEN_KBN = 4;
                                        }
                                        if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniUpn.HST_KEN_KBN.Value) != -1)
                                        {
                                            if (newDto.SYSTEM_ID.Count == 0)
                                            {
                                                newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                                newDto.SEQ.Add(maniDto.SEQ[0]);
                                                newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                                newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                                newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                                newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                                newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                                newDto.JYUTAKU_JISHA_KBN.Add(1);
                                            }
                                            newDto.upnList.Add(maniUpn);
                                        }
                                    }

                                    // 最終区間以外の場合、運搬先をブランクにする
                                    if (i != maniDto.JYUTAKU_JISHA_KBN.Count - 1)
                                    {
                                            maniUpn.SBN_GENBA_CD = string.Empty;
                                            maniUpn.SBN_GENBA_NAME = string.Empty;
                                            maniUpn.SBN_GENBA_ADDRESS = string.Empty;
                                            maniUpn.SBN_GENBA_CHIIKI_CD = string.Empty;
                                            maniUpn.SBN_GENBA_KYOKA_NO = string.Empty;
                                            maniUpn.SBN_GYOUSHA_CD = string.Empty;
                                            maniUpn.SBN_GYOUSHA_NAME = string.Empty;
                                            maniUpn.SBN_GYOUSHA_ADDRESS = string.Empty;
                                            maniUpn.UPN_RYOU = SqlDecimal.Null;
                                    }

                                    startFlg = false;
                                }
                                else if (i == maniDto.JYUTAKU_JISHA_KBN.Count - 1 && maniDto.JYUTAKU_JISHA_KBN[i] == 1)
                                {
                                    if (start == 0)
                                    {
                                        if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 1;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD != maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 2;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD != SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 3;
                                        }
                                        else
                                        {
                                            maniUpn.HST_KEN_KBN = 4;
                                        }
                                        if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniUpn.HST_KEN_KBN.Value) != -1)
                                        {
                                            if (newDto.SYSTEM_ID.Count == 0)
                                            {
                                                newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                                newDto.SEQ.Add(maniDto.SEQ[0]);
                                                newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                                newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                                newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                                newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                                newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                                newDto.JYUTAKU_JISHA_KBN.Add(1);
                                            }
                                            newDto.upnList.Add(maniUpn);
                                        }
                                    }
                                    else
                                    {
                                        maniUpn.HST_GENBA_ADDRESS = maniDto.upnList[start - 1].SBN_GENBA_ADDRESS;
                                        maniUpn.HST_GENBA_CHIIKI_CD = maniDto.upnList[start - 1].SBN_GENBA_CHIIKI_CD;
                                        maniUpn.HST_GYOUSHA_ADDRESS = maniDto.upnList[start - 1].SBN_GYOUSHA_ADDRESS;
                                        maniUpn.HST_JOU_CHIIKI_CD = maniDto.upnList[start - 1].UPNSAKI_JOU_CHIIKI_CD;
                                        if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 1;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD == SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD != maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 2;
                                        }
                                        else if (maniUpn.HST_JOU_CHIIKI_CD != SearchString.TEISHUTUSAKI_CD && SearchString.TEISHUTUSAKI_CD == maniUpn.UPNSAKI_JOU_CHIIKI_CD)
                                        {
                                            maniUpn.HST_KEN_KBN = 3;
                                        }
                                        else
                                        {
                                            maniUpn.HST_KEN_KBN = 4;
                                        }
                                        if (Array.IndexOf(SearchString.CHUSHUTU_JOKEN_KBN_ARRAY, maniUpn.HST_KEN_KBN.Value) != -1)
                                        {
                                            if (newDto.SYSTEM_ID.Count == 0)
                                            {
                                                newDto.SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                                                newDto.SEQ.Add(maniDto.SEQ[0]);
                                                newDto.DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);
                                                newDto.KANRI_ID.Add(maniDto.KANRI_ID[0]);
                                                newDto.DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                                                newDto.MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                                                newDto.HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                                                newDto.JYUTAKU_JISHA_KBN.Add(1);
                                            }
                                            newDto.upnList.Add(maniUpn);
                                        }
                                    }
                                }
                            }
                            List<int> HstKbnList = new List<int>();
                            for (int i = 0; i < newDto.upnList.Count; i++)
                            {
                                bool same = false;
                                int kenKbn = newDto.upnList[i].HST_KEN_KBN.Value;
                                foreach (int hstKbn in HstKbnList)
                                {
                                    if (hstKbn == kenKbn)
                                    {
                                        same = true;
                                        break;
                                    }
                                }
                                if (!same)
                                {
                                    HstKbnList.Add(kenKbn);
                                }
                                else
                                {
                                    if (!newDto.upnList[i].JYUTAKU_RYOU.IsNull)
                                    {
                                        newDto.upnList[i].JYUTAKU_RYOU = SqlDecimal.Null;
                                    }
                                    if (!newDto.upnList[i].UPN_RYOU.IsNull)
                                    {
                                        newDto.upnList[i].UPN_RYOU = SqlDecimal.Null;
                                    }
                                    if (!newDto.upnList[i].HIKIWATASHI_RYOU.IsNull)
                                    {
                                        newDto.upnList[i].HIKIWATASHI_RYOU = SqlDecimal.Null;
                                    }
                                }
                            }
                            if (newDto.upnList.Count > 0)
                            {
                                newManiDtoList.Add(newDto);
                            }
                        }
                        else
                        {
                            newManiDtoList.Add(maniDto);
                        }
                    }

                    maniDtoList = newManiDtoList;
                }

                if (maniDtoList.Count == 0)
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    ret = -1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                else
                {
                    if (this.DataCheck(maniDtoList))
                    {
                        ret = -1;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }
                // 集計
                List<ManiDTOClass> syuukeiDtoList = new List<ManiDTOClass>();
                foreach (ManiDTOClass maniDto in maniDtoList)
                {
                    bool syuukeiNew = true;
                    for (int i = 0; i < syuukeiDtoList.Count; i++)
                    {
                        bool allSame = true;
                        if (maniDto.upnList.Count == syuukeiDtoList[i].upnList.Count)
                        {
                            for (int j = 0; j < maniDto.upnList.Count; j++)
                            {
                                if (maniDto.upnList[j].HOUKOKUSHO_BUNRUI_CD == syuukeiDtoList[i].upnList[j].HOUKOKUSHO_BUNRUI_CD
                             && maniDto.upnList[j].HST_GYOUSHA_CD == syuukeiDtoList[i].upnList[j].HST_GYOUSHA_CD
                             && maniDto.upnList[j].HST_GENBA_CD == syuukeiDtoList[i].upnList[j].HST_GENBA_CD
                             && maniDto.upnList[j].SBN_GYOUSHA_CD == syuukeiDtoList[i].upnList[j].SBN_GYOUSHA_CD
                             && maniDto.upnList[j].SBN_GENBA_CD == syuukeiDtoList[i].upnList[j].SBN_GENBA_CD
                             && maniDto.upnList[j].HIKIWATASHISAKI_CD == syuukeiDtoList[i].upnList[j].HIKIWATASHISAKI_CD
                             && allSame)
                                {
                                    continue;
                                }
                                else
                                {
                                    allSame = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            allSame = false;
                        }

                        if (!allSame)
                        {
                            syuukeiNew = true;
                        }
                        else
                        {
                            syuukeiDtoList[i].SYSTEM_ID.Add(maniDto.SYSTEM_ID[0]);
                            syuukeiDtoList[i].SEQ.Add(maniDto.SEQ[0]);
                            syuukeiDtoList[i].KANRI_ID.Add(maniDto.KANRI_ID[0]);
                            syuukeiDtoList[i].DEN_SEQ.Add(maniDto.DEN_SEQ[0]);
                            syuukeiDtoList[i].MANIFEST_ID.Add(maniDto.MANIFEST_ID[0]);
                            syuukeiDtoList[i].HAIKI_KBN_CD.Add(maniDto.HAIKI_KBN_CD[0]);
                            syuukeiDtoList[i].DETAIL_SYSTEM_ID.Add(maniDto.DETAIL_SYSTEM_ID[0]);

                            for (int j = 0; j < maniDto.upnList.Count; j++)
                            {
                                if (!syuukeiDtoList[i].upnList[j].JYUTAKU_RYOU.IsNull && !maniDto.upnList[j].JYUTAKU_RYOU.IsNull)
                                {
                                    syuukeiDtoList[i].upnList[j].JYUTAKU_RYOU = syuukeiDtoList[i].upnList[j].JYUTAKU_RYOU.Value + maniDto.upnList[j].JYUTAKU_RYOU.Value;
                                }
                                else if (syuukeiDtoList[i].upnList[j].JYUTAKU_RYOU.IsNull && !maniDto.upnList[j].JYUTAKU_RYOU.IsNull)
                                {
                                    syuukeiDtoList[i].upnList[j].JYUTAKU_RYOU = maniDto.upnList[j].JYUTAKU_RYOU.Value;
                                }

                                if (!syuukeiDtoList[i].upnList[j].UPN_RYOU.IsNull && !maniDto.upnList[j].UPN_RYOU.IsNull)
                                {
                                    syuukeiDtoList[i].upnList[j].UPN_RYOU = syuukeiDtoList[i].upnList[j].UPN_RYOU.Value + maniDto.upnList[j].UPN_RYOU.Value;
                                }
                                else if (syuukeiDtoList[i].upnList[j].UPN_RYOU.IsNull && !maniDto.upnList[j].UPN_RYOU.IsNull)
                                {
                                    syuukeiDtoList[i].upnList[j].UPN_RYOU = maniDto.upnList[j].UPN_RYOU.Value;
                                }

                                if (!syuukeiDtoList[i].upnList[j].HIKIWATASHI_RYOU.IsNull && !maniDto.upnList[j].HIKIWATASHI_RYOU.IsNull)
                                {
                                    syuukeiDtoList[i].upnList[j].HIKIWATASHI_RYOU = syuukeiDtoList[i].upnList[j].HIKIWATASHI_RYOU.Value + maniDto.upnList[j].HIKIWATASHI_RYOU.Value;
                                }
                                else if (syuukeiDtoList[i].upnList[j].HIKIWATASHI_RYOU.IsNull && !maniDto.upnList[j].HIKIWATASHI_RYOU.IsNull)
                                {
                                    syuukeiDtoList[i].upnList[j].UPN_RYOU = maniDto.upnList[j].HIKIWATASHI_RYOU.Value;
                                }
                            }
                            syuukeiNew = false;
                            break;
                        }
                    }

                    if (syuukeiNew)
                    {
                        syuukeiDtoList.Add(maniDto);
                    }
                }

                foreach (ManiDTOClass syuukeiDto in syuukeiDtoList)
                {
                    foreach (T_JISSEKI_HOUKOKU_UPN_DETAIL upn in syuukeiDto.upnList)
                    {
                        upn.DETAIL_SYSTEM_ID = this.createSystemIdForJissekiHokoku();
                        this.unpanList.Add(upn);

                        int DETAIL_ROW_NO = 0;
                        for (int i = 0; i < syuukeiDto.SYSTEM_ID.Count; i++)
                        {
                            T_JISSEKI_HOUKOKU_MANIFEST_DETAIL detail = new T_JISSEKI_HOUKOKU_MANIFEST_DETAIL();
                            detail.SYSTEM_ID = this.entry.SYSTEM_ID;
                            detail.SEQ = this.entry.SEQ;
                            detail.DETAIL_SYSTEM_ID = upn.DETAIL_SYSTEM_ID;
                            DETAIL_ROW_NO += 1;
                            detail.DETAIL_ROW_NO = DETAIL_ROW_NO;
                            detail.REPORT_ID = 3;
                            detail.HAIKI_KBN_CD = syuukeiDto.HAIKI_KBN_CD[i];
                            if (!string.IsNullOrWhiteSpace(syuukeiDto.SYSTEM_ID[i]))
                            {
                                detail.MANI_SYSTEM_ID = Convert.ToInt64(syuukeiDto.SYSTEM_ID[i]);
                            }
                            else
                            {
                                detail.MANI_SYSTEM_ID = SqlInt64.Null;
                            }
                            if (!string.IsNullOrWhiteSpace(syuukeiDto.SEQ[i]))
                            {
                                detail.MANI_SEQ = Convert.ToInt32(syuukeiDto.SEQ[i]);
                            }
                            else
                            {
                                detail.MANI_SEQ = SqlInt32.Null;
                            }
                            detail.MANIFEST_ID = syuukeiDto.MANIFEST_ID[i];
                            detail.DEN_MANI_KANRI_ID = syuukeiDto.KANRI_ID[i];
                            if (!string.IsNullOrWhiteSpace(syuukeiDto.DEN_SEQ[i]))
                            {
                                detail.DEN_MANI_SEQ = Convert.ToInt32(syuukeiDto.DEN_SEQ[i]);
                            }
                            else
                            {
                                detail.DEN_MANI_SEQ = SqlInt32.Null;
                            }
                            this.detailList.Add(detail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateUnpan", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// マニデータDto作成
        /// </summary>
        /// <returns></returns>
        private ManiDTOClass createManiDto(ManiDTOClass dto, DataRow row, bool isNew)
        {
            if (isNew)
            {
                dto.SYSTEM_ID.Add(Convert.ToString(row["SYSTEM_ID"]));
                dto.SEQ.Add(Convert.ToString(row["SEQ"]));
                dto.KANRI_ID.Add(Convert.ToString(row["KANRI_ID"]));
                dto.DEN_SEQ.Add(Convert.ToString(row["DEN_SEQ"]));
                dto.HAIKI_KBN_CD.Add(Convert.ToInt16(row["HAIKI_KBN_CD"]));
                dto.MANIFEST_ID.Add(Convert.ToString(row["MANIFEST_ID"]));
                dto.DETAIL_SYSTEM_ID.Add(Convert.ToInt32(row["DETAIL_SYSTEM_ID"]));
            }

            if (!string.IsNullOrEmpty(Convert.ToString(row["JYUTAKU_JISHA_KBN"])))
            {
                dto.JYUTAKU_JISHA_KBN.Add(Convert.ToInt32(row["JYUTAKU_JISHA_KBN"]));
            }
            else
            {
                dto.JYUTAKU_JISHA_KBN.Add(SqlInt32.Null);
            }
            T_JISSEKI_HOUKOKU_UPN_DETAIL unpan = new T_JISSEKI_HOUKOKU_UPN_DETAIL();

            unpan.SYSTEM_ID = this.entry.SYSTEM_ID;
            unpan.SEQ = this.entry.SEQ;
            unpan.REPORT_ID = 3;
            unpan.HOUKOKU_SHOSHIKI_KBN = Convert.ToInt16(this.form.txt_TeishutuShoshiki.Text);
            unpan.HOZON_NAME = this.form.txt_ChohyouName.Text;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
            ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
            unpan.HOUKOKU_YEAR = Convert.ToDateTime(this.form.HIDUKE_FROM.Value).ToString("gy年", ci);
            unpan.TEISHUTSUSAKI_CHIIKI_CD = this.form.txt_TeishutuSakiCD.Text;
            unpan.TOKUBETSU_KANRI_KBN = Convert.ToInt16(this.form.txt_HaikiKbn.Text);
            unpan.JIGYOUJOU_KBN = 1;
            unpan.KEN_KBN = Convert.ToInt32((this.form.chk_Out_Out.Checked ? "1" : "0")
                                    + (this.form.chk_Out_In.Checked ? "1" : "0")
                                    + (this.form.chk_In_In.Checked ? "1" : "0")
                                    + (this.form.chk_In_Out.Checked ? "1" : "0"));
            unpan.HOUKOKUSHO_BUNRUI_CD = Convert.ToString(row["HOUKOKUSHO_BUNRUI_CD"]);
            unpan.HOUKOKUSHO_BUNRUI_NAME = Convert.ToString(row["HOUKOKUSHO_BUNRUI_NAME"]);
            unpan.HAIKI_SHURUI_CD = Convert.ToString(row["HAIKI_SHURUI_CD"]);
            unpan.HAIKI_SHURUI_NAME = Convert.ToString(row["HAIKI_SHURUI_NAME"]);
            unpan.SEKIMEN_KBN = this.ToNBoolean(row["SEKIMEN_KBN"]) ?? SqlBoolean.Null;
            unpan.TOKUTEI_YUUGAI_KBN = this.ToNBoolean(row["TOKUTEI_YUUGAI_KBN"]) ?? SqlBoolean.Null;
            unpan.SAIITAKU_KYOKA_NO = Convert.ToString(row["SAIITAKU_KYOKA_NO"]);
            unpan.HST_GYOUSHA_CD = Convert.ToString(row["HST_GYOUSHA_CD"]);
            unpan.HST_GYOUSHA_NAME = Convert.ToString(row["HST_GYOUSHA_NAME"]);
            unpan.HST_GYOUSHA_ADDRESS = this.SetAddressString(Convert.ToString(row["HST_GYOUSHA_ADDRESS"]));
            unpan.HST_GYOUSHA_GYOUSHU_CD = Convert.ToString(row["HST_GYOUSHA_GYOUSHU_CD"]);
            unpan.HST_GENBA_CD = Convert.ToString(row["HST_GENBA_CD"]);
            unpan.HST_GENBA_NAME = Convert.ToString(row["HST_GENBA_NAME"]);
            unpan.HST_GENBA_ADDRESS = this.SetAddressString(Convert.ToString(row["HST_GENBA_ADDRESS"]));
            unpan.HST_GENBA_GYOUSHU_CD = Convert.ToString(row["HST_GENBA_GYOUSHU_CD"]);
            unpan.HST_GENBA_CHIIKI_CD = Convert.ToString(row["HST_GENBA_GYOUSHU_CD"]);
            unpan.JYUTAKU_RYOU = this.ToNDecimal(row["JYUTAKU_RYOU"]) ?? 0;
            unpan.JYUTAKU_KBN = Convert.ToString(row["JYUTAKU_KBN"]);
            unpan.JYUTAKU_UNIT_NAME = Convert.ToString(row["JYUTAKU_UNIT_NAME"]);
            unpan.SHOBUN_HOUHOU_CD = Convert.ToString(row["SHOBUN_HOUHOU_CD"]);
            unpan.SHOBUN_HOUHOU_NAME = Convert.ToString(row["SHOBUN_HOUHOU_NAME"]);
            unpan.SHOBUN_MOKUTEKI_CD = Convert.ToString(row["SHOBUN_MOKUTEKI_CD"]);
            unpan.SHOBUN_MOKUTEKI_NAME = Convert.ToString(row["SHOBUN_MOKUTEKI_NAME"]);
            unpan.SBN_GYOUSHA_CD = Convert.ToString(row["SBN_GYOUSHA_CD"]);
            unpan.SBN_GYOUSHA_NAME = Convert.ToString(row["SBN_GYOUSHA_NAME"]);
            unpan.SBN_GYOUSHA_ADDRESS = this.SetAddressString(Convert.ToString(row["SBN_GYOUSHA_ADDRESS"]));
            unpan.SBN_GENBA_CD = Convert.ToString(row["SBN_GENBA_CD"]);
            unpan.SBN_GENBA_NAME = Convert.ToString(row["SBN_GENBA_NAME"]);
            unpan.SBN_GENBA_ADDRESS = this.SetAddressString(Convert.ToString(row["SBN_GENBA_ADDRESS"]));
            unpan.SBN_GENBA_CHIIKI_CD = Convert.ToString(row["SBN_GENBA_CHIIKI_CD"]);
            unpan.UPN_RYOU = this.ToNDecimal(row["UPN_RYOU"]) ?? 0;
            unpan.HIKIWATASHISAKI_CD = Convert.ToString(row["HIKIWATASHISAKI_CD"]);
            unpan.HIKIWATASHISAKI_NAME = Convert.ToString(row["HIKIWATASHISAKI_NAME"]);
            unpan.HIKIWATASHISAKI_ADDRESS = this.SetAddressString(Convert.ToString(row["HIKIWATASHISAKI_ADDRESS"]));
            unpan.HIKIWATASHISAKI_CHIIKI_CD = Convert.ToString(row["HIKIWATASHISAKI_CHIIKI_CD"]);
            unpan.HIKIWATASHISAKI_KYOKA_NO = Convert.ToString(row["HIKIWATASHISAKI_KYOKA_NO"]);
            unpan.HIKIWATASHI_RYOU = this.ToNDecimal(row["HIKIWATASHI_RYOU"]) ?? SqlDecimal.Null;
            unpan.HIKIWATASHI_KBN = Convert.ToString(row["HIKIWATASHI_KBN"]);
            unpan.HST_JOU_CHIIKI_CD = Convert.ToString(row["CHIIKI_CD2"]);
            unpan.HST_JOU_TODOUFUKEN_CD = this.ToNInt16(row["TODOUFUKEN_CD"]) ?? SqlInt16.Null;
            unpan.UPNSAKI_JOU_CHIIKI_CD = Convert.ToString(row["CHIIKI_CD3"]);
            unpan.UPNSAKI_JOU_TODOUFUKEN_CD = this.ToNInt16(row["TODOUFUKEN_CD2"]) ?? SqlInt16.Null;
            unpan.HST_KEN_KBN = this.ToNInt16(row["HST_KEN_KBN"]) ?? SqlInt16.Null;
            dto.upnList.Add(unpan);
            return dto;
        }

        /// <summary>
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemIdForJissekiHokoku()
        {
            SqlInt64 returnInt = 1;

            using (Transaction tran = new Transaction())
            {
                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = 400;

                var updateEntity = this.numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = 400;
                    updateEntity.CURRENT_NUMBER = returnInt;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberSystemDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnInt;
                    this.numberSystemDao.Update(updateEntity);
                }

                tran.Commit();
            }

            return returnInt;
        }

        /// <summary>
        /// Entity値のコピー、
        /// </summary>
        /// <param name="entity1">entity1</param>
        /// <param name="entity2">entity2</param>
        private void copyEntity(T_JISSEKI_HOUKOKU_UPN_DETAIL entity1, T_JISSEKI_HOUKOKU_UPN_DETAIL entity2)
        {
            entity1.SYSTEM_ID = entity2.SYSTEM_ID;
            entity1.SEQ = entity2.SEQ;
            entity1.REPORT_ID = entity2.REPORT_ID;
            entity1.DETAIL_SYSTEM_ID = entity2.DETAIL_SYSTEM_ID;
            entity1.HOUKOKU_SHOSHIKI_KBN = entity2.HOUKOKU_SHOSHIKI_KBN;
            entity1.HOZON_NAME = entity2.HOZON_NAME;
            entity1.HOUKOKU_YEAR = entity2.HOUKOKU_YEAR;
            entity1.TEISHUTSUSAKI_CHIIKI_CD = entity2.TEISHUTSUSAKI_CHIIKI_CD;
            entity1.TOKUBETSU_KANRI_KBN = entity2.TOKUBETSU_KANRI_KBN;
            entity1.JIGYOUJOU_KBN = entity2.JIGYOUJOU_KBN;
            entity1.KEN_KBN = entity2.KEN_KBN;
            entity1.HOUKOKUSHO_BUNRUI_CD = entity2.HOUKOKUSHO_BUNRUI_CD;
            entity1.HOUKOKUSHO_BUNRUI_NAME = entity2.HOUKOKUSHO_BUNRUI_NAME;
            entity1.HAIKI_SHURUI_CD = entity2.HAIKI_SHURUI_CD;
            entity1.HAIKI_SHURUI_NAME = entity2.HAIKI_SHURUI_NAME;
            entity1.SEKIMEN_KBN = entity2.SEKIMEN_KBN;
            entity1.TOKUTEI_YUUGAI_KBN = entity2.TOKUTEI_YUUGAI_KBN;
            entity1.SAIITAKU_KYOKA_NO = entity2.SAIITAKU_KYOKA_NO;
            entity1.HST_GYOUSHA_CD = entity2.HST_GYOUSHA_CD;
            entity1.HST_GYOUSHA_NAME = entity2.HST_GYOUSHA_NAME;
            entity1.HST_GYOUSHA_ADDRESS = entity2.HST_GYOUSHA_ADDRESS;
            entity1.HST_GYOUSHA_GYOUSHU_CD = entity2.HST_GYOUSHA_GYOUSHU_CD;
            entity1.HST_GENBA_CD = entity2.HST_GENBA_CD;
            entity1.HST_GENBA_NAME = entity2.HST_GENBA_NAME;
            entity1.HST_GENBA_ADDRESS = entity2.HST_GENBA_ADDRESS;
            entity1.HST_GENBA_GYOUSHU_CD = entity2.HST_GENBA_GYOUSHU_CD;
            entity1.HST_GENBA_CHIIKI_CD = entity2.HST_GENBA_CHIIKI_CD;
            entity1.JYUTAKU_RYOU = entity2.JYUTAKU_RYOU;
            entity1.JYUTAKU_KBN = entity2.JYUTAKU_KBN;
            entity1.JYUTAKU_UNIT_NAME = entity2.JYUTAKU_UNIT_NAME;
            entity1.SHOBUN_HOUHOU_CD = entity2.SHOBUN_HOUHOU_CD;
            entity1.SHOBUN_HOUHOU_NAME = entity2.SHOBUN_HOUHOU_NAME;
            entity1.SHOBUN_MOKUTEKI_CD = entity2.SHOBUN_MOKUTEKI_CD;
            entity1.SHOBUN_MOKUTEKI_NAME = entity2.SHOBUN_MOKUTEKI_NAME;
            entity1.SBN_GYOUSHA_CD = entity2.SBN_GYOUSHA_CD;
            entity1.SBN_GYOUSHA_NAME = entity2.SBN_GYOUSHA_NAME;
            entity1.SBN_GYOUSHA_ADDRESS = entity2.SBN_GYOUSHA_ADDRESS;
            entity1.SBN_GENBA_CD = entity2.SBN_GENBA_CD;
            entity1.SBN_GENBA_NAME = entity2.SBN_GENBA_NAME;
            entity1.SBN_GENBA_ADDRESS = entity2.SBN_GENBA_ADDRESS;
            entity1.SBN_GENBA_CHIIKI_CD = entity2.SBN_GENBA_CHIIKI_CD;
            entity1.SBN_GENBA_KYOKA_NO = entity2.SBN_GENBA_KYOKA_NO;
            entity1.UPN_RYOU = entity2.UPN_RYOU;
            entity1.HIKIWATASHISAKI_CD = entity2.HIKIWATASHISAKI_CD;
            entity1.HIKIWATASHISAKI_NAME = entity2.HIKIWATASHISAKI_NAME;
            entity1.HIKIWATASHISAKI_ADDRESS = entity2.HIKIWATASHISAKI_ADDRESS;
            entity1.HIKIWATASHISAKI_CHIIKI_CD = entity2.HIKIWATASHISAKI_CHIIKI_CD;
            entity1.HIKIWATASHISAKI_KYOKA_NO = entity2.HIKIWATASHISAKI_KYOKA_NO;
            entity1.HIKIWATASHI_RYOU = entity2.HIKIWATASHI_RYOU;
            entity1.HIKIWATASHI_KBN = entity2.HIKIWATASHI_KBN;
            entity1.HST_JOU_CHIIKI_CD = entity2.HST_JOU_CHIIKI_CD;
            entity1.HST_JOU_TODOUFUKEN_CD = entity2.HST_JOU_TODOUFUKEN_CD;
            entity1.UPNSAKI_JOU_CHIIKI_CD = entity2.UPNSAKI_JOU_CHIIKI_CD;
            entity1.UPNSAKI_JOU_TODOUFUKEN_CD = entity2.UPNSAKI_JOU_TODOUFUKEN_CD;
            entity1.HST_KEN_KBN = entity2.HST_KEN_KBN;
        }
        #endregion

        #region 補助ファンクション
        /// <summary>
        /// SqlDouble型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal SqlDecimal? ToSqlDouble(object o)
        {
            SqlDecimal ret = this.ToNDecimal(o) ?? SqlDecimal.Null;
            return ret;
        }

        /// <summary>
        /// float?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal float? ToNFloat(object o)
        {
            float? ret = null;
            float parse = 0;
            if (float.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// double?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal decimal? ToNDecimal(object o)
        {
            decimal? ret = null;
            decimal parse = 0;
            if (decimal.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// Int16?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal Int16? ToNInt16(object o)
        {
            Int16? ret = null;
            Int16 parse = 0;
            if (Int16.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// bool?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal bool? ToNBoolean(object o)
        {
            bool? ret = null;
            bool parse = false;
            if (Boolean.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// bool?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal int FindChar(string obj, string[] lookFor, string kbn)
        {
            int max = -1;
            int min = obj.Length + 1;
            foreach (string str in lookFor)
            {
                int location = -1;
                switch (kbn)
                {
                    case "Max":
                        location = obj.LastIndexOf(str);
                        if (location == -1) { continue; }
                        if (location > max)
                        {
                            max = location;
                        }
                        break;
                    case "Min":
                        location = obj.IndexOf(str);
                        if (location == -1) { continue; }
                        if (location < min)
                        {
                            min = location;
                        }
                        break;
                }
            }
            switch (kbn)
            {
                case "Max":
                    return max == -1 ? -1 : max;
                case "Min":
                    return min == obj.Length + 1 ? -1 : min;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// 数量フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertSuuryo(SqlDecimal value)
        {
            string result = string.Empty;
            string format = "#,###.#";
            if (!string.IsNullOrEmpty(this.sysInfoEntity.MANIFEST_SUURYO_FORMAT))
            {
                format = this.sysInfoEntity.MANIFEST_SUURYO_FORMAT;
            }
            if (!value.IsNull)
            {
                result = value.Value.ToString(format);
            }
            return result;
        }
        #endregion



        // 20150108 陳 現場マスタのAddress1に詳細住所が入力されている可能性もあるの対応 start
        /// <summary>
        /// 住所設定
        /// </summary>
        /// <param name="address"></param>
        private string SetAddressString(string address)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(address))
            {
                if (this.entry != null && this.entry.ADDRESS_KBN == 3)
                {
                    result = Regex.Match(address, ".+?郡.+?町|.+?郡.+?村|.+?市|.+?区|.+?町|.+?村").Value;
                }
                else
                {
                    result = address;
                }
            }

            return result;
        }
        // 20150108 陳 現場マスタのAddress1に詳細住所が入力されている可能性もあるの対応 end
    }
}
