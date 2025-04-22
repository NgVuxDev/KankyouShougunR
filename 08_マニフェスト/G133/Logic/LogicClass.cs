using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Report;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.PaperManifest.HaikibutuTyoubo.DAO;

namespace Shougun.Core.PaperManifest.HaikibutuTyoubo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// 廃棄帳簿画面Form
        /// </summary>
        private HaikibutuTyoubo.UIForm form;

        /// <summary>
        /// ParentForm
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// DAO
        /// </summary>
        private DaoClass dao;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// 現場Dao
        /// </summary>
        private r_framework.Dao.IM_GENBADao genbaDao;

        /// <summary>
        /// 業者Dao
        /// </summary>
        private r_framework.Dao.IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 拠点Dao
        /// </summary>
        private r_framework.Dao.IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 廃棄物Dao
        /// </summary>
        private Shougun.Core.PaperManifest.HaikibutuTyoubo.DAO.DaoClass haikibutuDataDao;

        /// <summary>
        /// 廃棄物Dao
        /// </summary>
        private r_framework.Dao.IM_CORP_INFODao corpNameDao;

        /// <summary>
        /// CD・名称Dictionary
        /// </summary>
        private Dictionary<r_framework.CustomControl.CustomTextBox, r_framework.CustomControl.CustomTextBox> allControlDict;

        /// <summary>
        /// Layout種類(列挙)
        /// </summary>
        private enum layout
        {
            LAYOUT1,
            LAYOUT2,
            LAYOUT3,
            LAYOUT4,
            LAYOUT5,
            LAYOUT6
        };

        /// <summary>
        /// Layout種類
        /// </summary>
        private layout layOut;

        /// <summary>共通</summary>
        Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        private MessageBoxShowLogic MsgBox;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// <param name="form">表示する画面</param>
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
            this.haikibutuDataDao = DaoInitUtility.GetComponent<Shougun.Core.PaperManifest.HaikibutuTyoubo.DAO.DaoClass>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            this.dao = DaoInitUtility.GetComponent<DaoClass>();
            this.corpNameDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm);
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ParentFormのSet
                parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 画面表示初期化
                this.SetInitDisp();

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        /// <returns name="ButtonSetting[]">XMLに記載されたButtonのリスト</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            var tmp = buttonSetting.LoadButtonSetting(thisAssembly, Const.UIConstans.ButtonInfoXmlPath);
            return tmp;
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            //ファンクションキーイベント

            //[F9] 実行
            this.form.C_Regist(this.parentForm.bt_func9);
            this.parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);

            // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない start
            // 「Ｆ6 CSV出力ボタン」イベントのイベント生成
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);
            // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない end
            //[F12] 閉じる
            this.parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            /// 20141023 Houkakou 「廃棄物帳簿」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.dtp_DateTo.MouseDoubleClick += new MouseEventHandler(dtp_DateTo_MouseDoubleClick);
            this.form.txt_ShobunJigyojoCD_To.MouseDoubleClick += new MouseEventHandler(txt_ShobunJigyojoCD_To_MouseDoubleClick);
            /// 20141023 Houkakou 「廃棄物帳簿」のダブルクリックを追加する　end

            /// 20141209 teikyou 日付チェックを追加する　start
            this.form.dtp_DateFrom.Leave += new System.EventHandler(dtp_DateFrom_Leave);
            this.form.dtp_DateTo.Leave += new System.EventHandler(dtp_DateTo_Leave);
            /// 20141209 teikyou 日付チェックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面表示初期化
        /// <summary>
        /// 画面表示初期化
        /// </summary>
        private void SetInitDisp()
        {
            //個別指定CDと名称のDictionaryを作成
            this.allControlDict = new Dictionary<r_framework.CustomControl.CustomTextBox, r_framework.CustomControl.CustomTextBox>();
            allControlDict.Add(this.form.txt_ShobunJigyojoCD, this.form.txt_ShobunJigyojoName);
            allControlDict.Add(this.form.txt_ShobunJutakushaCD, this.form.txt_ShobunJutakushaName);
            allControlDict.Add(this.form.txt_KyotenCD, this.form.txt_KyotenName);

            //【初期化の処理】
            //出力内容
            this.form.txt_ShuturyokuNaiyo.Text = string.Empty;

            //中間処理
            this.form.txt_Tyukansyori.Text = string.Empty;

            //出力区分
            this.form.txt_ShuturyokuKbn.Text = string.Empty;

            //廃棄物区分
            this.form.chk_HaikibutsuKbn1.Checked = false;
            this.form.chk_HaikibutsuKbn2.Checked = false;
            this.form.chk_HaikibutsuKbn3.Checked = false;

            //日付種類
            this.form.txt_HitukeKbn.Text = string.Empty;

            //年月日(From)

            //年月日(To)

            //拠点
            this.form.txt_KyotenCD.Text = string.Empty;
            this.form.txt_KyotenName.Text = string.Empty;

            //処分受託者
            this.form.lbl_ShobunJutakusha.Text = string.Empty;
            this.form.txt_ShobunJutakushaCD.Text = string.Empty;
            this.form.txt_ShobunJutakushaName.Text = string.Empty;

            //処分事業場
            this.form.lbl_ShobunJigyojo.Text = string.Empty;

            //処分事業場(From)
            this.form.txt_ShobunJigyojoCD.Text = string.Empty;
            this.form.txt_ShobunJigyojoName.Text = string.Empty;

            //処分事業場(To)
            this.form.txt_ShobunJigyojoCD_To.Text = string.Empty;
            this.form.txt_ShobunJigyojoName_To.Text = string.Empty;


            //【初期値の設定】
            //出力内容
            this.form.txt_ShuturyokuNaiyo.Text = "1";

            //中間処理
            this.form.panel3.Enabled = false;
            this.form.txt_Tyukansyori.Text = "1";

            //出力区分
            this.form.txt_ShuturyokuKbn.Text = "1";

            //廃棄物区分
            this.form.chk_HaikibutsuKbn1.Checked = true;
            this.form.chk_HaikibutsuKbn2.Checked = true;
            this.form.chk_HaikibutsuKbn3.Checked = true;
            this.form.panel5.Enabled = false;

            //日付種類
            this.form.txt_HitukeKbn.Text = "2";

            //年月日(From)
            this.form.dtp_DateFrom_Flg = false;
            this.form.dtp_DateFrom.Value = this.parentForm.sysDate;
            this.form.dtp_DateFrom.TextBoxFrom = this.form.txt_ShobunJigyojoCD;
            this.form.dtp_DateFrom.TextBoxTo = this.form.txt_ShobunJigyojoCD_To;
            this.form.dtp_DateFrom.logic = this;

            //年月日(To)
            this.form.dtp_DateTo_Flg = false;
            this.form.dtp_DateTo.Value = this.parentForm.sysDate;
            this.form.dtp_DateTo.TextBoxFrom = this.form.txt_ShobunJigyojoCD;
            this.form.dtp_DateTo.TextBoxTo = this.form.txt_ShobunJigyojoCD_To;
            this.form.dtp_DateTo.logic = this;

            //拠点
            mlogic.SetKyoten(this.form.txt_KyotenCD, this.form.txt_KyotenName);

            //処分受託者

            //処分事業場

            //処分事業場(From)

            //処分事業場(To)

        }
        #endregion

        #region マニライト(C8)用初期化処理

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 出力内容 「2.中間処理」「3.運搬（自社）」「4.運搬（委託）」「5.処分（委託）」「6.最終処分（処分）」項目非表示。中間処理項目を非表示。
            
            // 出力内容
            this.form.rdo_TyukanShori.Visible = false;
            this.form.rdo_UnpanJisha.Visible = false;
            this.form.rdo_UnpanItaku.Visible = false;
            this.form.rdo_SaishuShobunItaku.Visible = false;
            this.form.rdo_SaishuShobunShobun.Visible = false;
            this.form.rdo_UnpanJisha.Location = new System.Drawing.Point(155, 0);
            this.form.rdo_UnpanItaku.Location = new System.Drawing.Point(305, 0);
            this.form.panel1.Size = new System.Drawing.Size(455, 20);

            this.form.txt_ShuturyokuNaiyo.CharacterLimitList = new char[] { '1' };
            this.form.txt_ShuturyokuNaiyo.Tag = "【1】を入力してください";

            // 中間処理
            this.form.label2.Visible = false;
            this.form.txt_Tyukansyori.Visible = false;
            this.form.panel3.Visible = false;

            // Location調整
            // 日付種類
            LocationAdjustmentForManiLite(this.form.label3);
            LocationAdjustmentForManiLite(this.form.txt_HitukeKbn);
            LocationAdjustmentForManiLite(this.form.panel4);

            // 年月日
            LocationAdjustmentForManiLite(this.form.lbl_Date);
            LocationAdjustmentForManiLite(this.form.dtp_DateFrom);
            LocationAdjustmentForManiLite(this.form.label38);
            LocationAdjustmentForManiLite(this.form.dtp_DateTo);

            // 拠点
            LocationAdjustmentForManiLite(this.form.lbl_Kyoten);
            LocationAdjustmentForManiLite(this.form.txt_KyotenCD);
            LocationAdjustmentForManiLite(this.form.txt_KyotenName);

            // 処分受託者
            LocationAdjustmentForManiLite(this.form.lbl_ShobunJutakusha);
            LocationAdjustmentForManiLite(this.form.txt_ShobunJutakushaCD);
            LocationAdjustmentForManiLite(this.form.txt_ShobunJutakushaName);

            // 処分事業場
            LocationAdjustmentForManiLite(this.form.lbl_ShobunJigyojo);
            LocationAdjustmentForManiLite(this.form.txt_ShobunJigyojoCD);
            LocationAdjustmentForManiLite(this.form.txt_ShobunJigyojoName);
            LocationAdjustmentForManiLite(this.form.label4);
            LocationAdjustmentForManiLite(this.form.txt_ShobunJigyojoCD_To);
            LocationAdjustmentForManiLite(this.form.txt_ShobunJigyojoName_To);
        }

        /// <summary>
        /// マニライト用にLocation調整
        /// </summary>
        /// <param name="ctrl"></param>
        private void LocationAdjustmentForManiLite(Control ctrl)
        {
            ctrl.Location = new System.Drawing.Point(ctrl.Location.X, ctrl.Location.Y - 42);
        }

        #endregion

        #region 帳票内容設定
        /// <summary>
        /// 帳票内容設定
        /// <param name="dto">入力された内容のDTOクラス</param>
        /// <param name="layout">帳票レイアウト</param>
        /// </summary>
        private DataTable GetChouhyouData(DTOClass dto, layout layOut)
        {
            DataTable chouhyouData = new DataTable();
            //出力帳票判定
            switch (layOut)
            {
                case layout.LAYOUT6://1.収集運搬（自社）
                    chouhyouData = CreatChouhyouForLayout6(dto, chouhyouData);
                    break;

                case layout.LAYOUT3://2.中間処理
                    chouhyouData = CreatChouhyouForLayout3(dto, chouhyouData);
                    break;

                case layout.LAYOUT1://3.運搬（自社）
                    chouhyouData = CreatChouhyouForLayout1(dto, chouhyouData);
                    break;

                case layout.LAYOUT2://4.運搬（委託）
                    chouhyouData = CreatChouhyouForLayout2(dto, chouhyouData);
                    break;

                case layout.LAYOUT4://5.処分（委託）
                    chouhyouData = CreatChouhyouForLayout4(dto, chouhyouData);
                    break;

                case layout.LAYOUT5://6.最終処分（処分）
                    chouhyouData = CreatChouhyouForLayout5(dto, chouhyouData);
                    break;
            }

            return chouhyouData;
        }
        #endregion

        #region LAYOUT1(運搬帳票)内容設定
        /// <summary>
        /// LAYOUT1(運搬帳票)内容設定
        /// <param name="dto">入力された内容のDTOクラス</param>
        /// <param name="chouhyouData">帳票データ</param>
        /// </summary>
        private DataTable CreatChouhyouForLayout1(DTOClass dto, DataTable chouhyouData)
        {
            // 出力区分は合算の場合
            if (dto.SYUTURYOKU_KBN.Equals("1"))
            {
                DataTable chouhyouDataDenshi = new DataTable();
                chouhyouData = this.dao.GetPrintUnpanChoubo(dto);
                chouhyouDataDenshi = this.dao.GetPrintDenshiUnpanChoubo(dto);

                try
                {
                    chouhyouData.BeginLoadData();
                    chouhyouData.Merge(chouhyouDataDenshi);

                    // マージ後、並び替え
                    DataTable sortData = chouhyouData.Clone();

                    // 2014.08.12 #1134 MIYA MOD START
                    //DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, HAIKI_KBN_CD ASC, KOUFUNO ASC");
                    DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, " +
                                                                "UNPAN_HOUHOU_CD ASC, " +   //追加
                                                                "UKEIRESAKI ASC, " +        //追加
                                                                "UNPANSAKI ASC, " +         //追加
                                                                "HAIKI_KBN_CD ASC, " +
                                                                "KOUFUNO ASC");
                    // 2014.08.12 #1134 MIYA MOD END

                    foreach (DataRow dr in rows)
                    {
                        sortData.ImportRow(dr);
                    }
                    chouhyouData = sortData;
                    var UniqueRows = chouhyouData.AsEnumerable().Distinct(DataRowComparer.Default);
                    if (UniqueRows.ToList().Count != 0)
                    {
                        chouhyouData = UniqueRows.CopyToDataTable();
                    }
                }
                catch (ConstraintException)
                {
                }
            }
            // 出力区分は紙の場合
            else if (dto.SYUTURYOKU_KBN.Equals("2"))
            {
                chouhyouData = this.dao.GetPrintUnpanChoubo(dto);
            }
            // 出力区分は電子の場合
            else if (dto.SYUTURYOKU_KBN.Equals("3"))
            {
                chouhyouData = this.dao.GetPrintDenshiUnpanChoubo(dto);
            }
            return chouhyouData;
        }
        #endregion

        #region LAYOUT2(運搬委託帳簿)内容設定
        /// <summary>
        /// LAYOUT2(運搬委託帳簿)内容設定
        /// <param name="dto">入力された内容のDTOクラス</param>
        /// <param name="chouhyouData">帳票データ</param>
        /// </summary>
        private DataTable CreatChouhyouForLayout2(DTOClass dto, DataTable chouhyouData)
        {
            // 出力区分は合算の場合
            if (dto.SYUTURYOKU_KBN.Equals("1"))
            {
                DataTable chouhyouDataDenshi = new DataTable();
                chouhyouData = this.dao.GetPrintUnpanItakuChoubo(dto);
                chouhyouDataDenshi = this.dao.GetPrintDenshiUnpanItakuChoubo(dto);

                try
                {
                    chouhyouData.BeginLoadData();
                    chouhyouData.Merge(chouhyouDataDenshi);

                    // マージ後、並び替え
                    DataTable sortData = chouhyouData.Clone();

                    // 2014.08.12 #1134 MIYA MOD START
                    //DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, HAIKI_KBN_CD ASC, KOUFUNO ASC");
                    DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, " +
                                                                "UNPANSAKI ASC, " +      //追加
                                                                "HAIKI_KBN_CD ASC, " +
                                                                "KOUFUNO ASC");
                    // 2014.08.12 #1134 MIYA MOD END

                    foreach (DataRow dr in rows)
                    {
                        sortData.ImportRow(dr);
                    }
                    chouhyouData = sortData;
                    var UniqueRows = chouhyouData.AsEnumerable().Distinct(DataRowComparer.Default);
                    if (UniqueRows.ToList().Count != 0)
                    {
                        chouhyouData = UniqueRows.CopyToDataTable();
                    }
                }
                catch (ConstraintException)
                {
                }
            }
            // 出力区分は紙の場合
            else if (dto.SYUTURYOKU_KBN.Equals("2"))
            {
                chouhyouData = this.dao.GetPrintUnpanItakuChoubo(dto);
            }
            // 出力区分は電子の場合
            else if (dto.SYUTURYOKU_KBN.Equals("3"))
            {
                chouhyouData = this.dao.GetPrintDenshiUnpanItakuChoubo(dto);
            }

            return chouhyouData;
        }
        #endregion

        #region LAYOUT3(中間処理帳簿)内容設定
        /// <summary>
        /// LAYOUT3(中間処理帳簿)内容設定
        /// <param name="dto">入力された内容のDTOクラス</param>
        /// <param name="chouhyouData">帳票データ</param>
        /// </summary>
        private DataTable CreatChouhyouForLayout3(DTOClass dto, DataTable chouhyouData)
        {
            // 指摘があった持出量のみSQL発行時にフォーマットを掛ける
            // r-frameWorkのカスタムフォーマットが切り捨てに対し、Microsoftは基本的に四捨五入。
            // r-frameWorkのカスタムフォーマット処理を四捨五入にするのがベストだと個人的に考える。
            var sysinfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            var sysInfo = sysinfoDao.GetAllData();
            int maniSuuryouFormat = 0;
            if (sysInfo != null && sysInfo.Length > 0)
            {
                var maniFormatCd = sysInfo[0].MANIFEST_SUURYO_FORMAT_CD.ToString();
                if ("3".Equals(maniFormatCd))
                {
                    maniSuuryouFormat = 1;
                }
                else if ("4".Equals(maniFormatCd))
                {
                    maniSuuryouFormat = 2;
                }
                else if ("5".Equals(maniFormatCd))
                {
                    maniSuuryouFormat = 3;
                }
            }

            dto.MANIFEST_SUURYOU_FORMAT = maniSuuryouFormat;

            // 出力区分は合算の場合
            if (dto.SYUTURYOKU_KBN.Equals("1"))
            {
                //中間処理区分が「全て」の場合
                if (dto.TYUKANSYORI.Equals("1"))
                {
                    DataTable chouhyouDataBuf = new DataTable();
                    DataTable chouhyouDataBuf2 = new DataTable();
                    DataTable chouhyouDataBuf3 = new DataTable();
                    chouhyouData = this.dao.GetPrintChuukanshoriChoubo_1(dto);
                    chouhyouDataBuf = this.dao.GetPrintDenshiChuukanshoriChoubo_1(dto);
                    try
                    {
                        chouhyouData.BeginLoadData();
                        chouhyouData.Merge(chouhyouDataBuf);
                        chouhyouDataBuf2 = this.dao.GetPrintChuukanshoriChoubo_2(dto);
                        chouhyouDataBuf3 = this.dao.GetPrintDenshiChuukanshoriChoubo_2(dto);
                        chouhyouData.Merge(chouhyouDataBuf2);
                        chouhyouData.Merge(chouhyouDataBuf3);
                    }
                    catch (ConstraintException)
                    {
                    }
                }
                //中間処理区分が「一次完結のみ」の場合
                if (dto.TYUKANSYORI.Equals("2"))
                {
                    DataTable chouhyouDataDenshi = new DataTable();
                    chouhyouData = this.dao.GetPrintChuukanshoriChoubo_1(dto);
                    chouhyouDataDenshi = this.dao.GetPrintDenshiChuukanshoriChoubo_1(dto);

                    try
                    {
                        chouhyouData.BeginLoadData();
                        chouhyouData.Merge(chouhyouDataDenshi);
                    }
                    catch (ConstraintException)
                    {
                    }
                }
                //中間処理区分が「一次完結除く」の場合
                if (dto.TYUKANSYORI.Equals("3"))
                {
                    DataTable chouhyouDataDenshi = new DataTable();
                    chouhyouData = this.dao.GetPrintChuukanshoriChoubo_2(dto);
                    chouhyouDataDenshi = this.dao.GetPrintDenshiChuukanshoriChoubo_2(dto);

                    try
                    {
                        chouhyouData.BeginLoadData();
                        chouhyouData.Merge(chouhyouDataDenshi);
                    }
                    catch (ConstraintException)
                    {
                    }
                }
            }
            // 出力区分は紙の場合
            else if (dto.SYUTURYOKU_KBN.Equals("2"))
            {
                //中間処理区分が「全て」の場合
                if (dto.TYUKANSYORI.Equals("1"))
                {
                    DataTable chouhyouDataDenshi = new DataTable();
                    chouhyouData = this.dao.GetPrintChuukanshoriChoubo_1(dto);
                    chouhyouDataDenshi = this.dao.GetPrintChuukanshoriChoubo_2(dto);

                    try
                    {
                        chouhyouData.BeginLoadData();
                        chouhyouData.Merge(chouhyouDataDenshi);
                    }
                    catch (ConstraintException)
                    {
                    }
                }
                //中間処理区分が「一次完結のみ」の場合
                if (dto.TYUKANSYORI.Equals("2"))
                {
                    chouhyouData = this.dao.GetPrintChuukanshoriChoubo_1(dto);
                }
                //中間処理区分が「一次完結除く」の場合
                if (dto.TYUKANSYORI.Equals("3"))
                {
                    chouhyouData = this.dao.GetPrintChuukanshoriChoubo_2(dto);
                }
            }
            // 出力区分は電子の場合
            else if (dto.SYUTURYOKU_KBN.Equals("3"))
            {
                //中間処理区分が「全て」の場合
                if (dto.TYUKANSYORI.Equals("1"))
                {
                    DataTable chouhyouDataDenshi = new DataTable();
                    chouhyouData = this.dao.GetPrintDenshiChuukanshoriChoubo_1(dto);
                    chouhyouDataDenshi = this.dao.GetPrintDenshiChuukanshoriChoubo_2(dto);

                    try
                    {
                        chouhyouData.BeginLoadData();
                        chouhyouData.Merge(chouhyouDataDenshi);
                    }
                    catch (ConstraintException)
                    {
                    }

                }
                //中間処理区分が「一次完結のみ」の場合
                if (dto.TYUKANSYORI.Equals("2"))
                {
                    chouhyouData = this.dao.GetPrintDenshiChuukanshoriChoubo_1(dto);
                }
                //中間処理区分が「一次完結除く」の場合
                if (dto.TYUKANSYORI.Equals("3"))
                {
                    chouhyouData = this.dao.GetPrintDenshiChuukanshoriChoubo_2(dto);
                }
            }

            // マージ後、並び替え
            try
            {
                DataTable sortData = chouhyouData.Clone();
                sortData.Columns["KOUFUNO2"].MaxLength = 11;
                // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない start
                sortData.Columns["KOUFUNO2"].AllowDBNull = true;
                // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない end
                sortData.Columns["MOCHIDASHISAKI"].MaxLength = 80;
                // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない start
                sortData.Columns["MOCHIDASHISAKI"].AllowDBNull = true;
                // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない end
                sortData.Columns["MOCHIDASHISAKI_GOUKEI_NAME"].MaxLength = 40;
                // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない start
                sortData.Columns["MOCHIDASHISAKI_GOUKEI_NAME"].AllowDBNull = true;
                // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない end
                DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, HAIKI_KBN_CD ASC, KOUFUNO ASC, KOUFUNO2 ASC");
                foreach (DataRow dr in rows)
                {
                    sortData.ImportRow(dr);
                }
                chouhyouData = sortData;
                var UniqueRows = chouhyouData.AsEnumerable().Distinct(DataRowComparer.Default);
                if (UniqueRows.ToList().Count != 0)
                {
                    chouhyouData = UniqueRows.CopyToDataTable();
                }
            }
            catch (ConstraintException)
            {
            }

            return chouhyouData;
        }
        #endregion

        #region LAYOUT4(最終処分委託帳簿)内容設定
        /// <summary>
        /// LAYOUT4(最終処分委託帳簿)内容設定
        /// <param name="dto">入力された内容のDTOクラス</param>
        /// <param name="chouhyouData">帳票データ</param>
        /// </summary>
        private DataTable CreatChouhyouForLayout4(DTOClass dto, DataTable chouhyouData)
        {
            // 出力区分は合算の場合
            if (dto.SYUTURYOKU_KBN.Equals("1"))
            {
                DataTable chouhyouDataDenshi = new DataTable();
                chouhyouData = this.dao.GetPrintSaishuushobunItakuChoubo(dto);
                chouhyouDataDenshi = this.dao.GetPrintDenshiSaishuushobunItakuChoubo(dto);

                try
                {
                    chouhyouData.BeginLoadData();
                    chouhyouData.Merge(chouhyouDataDenshi);

                    // マージ後、並び替え
                    DataTable sortData = chouhyouData.Clone();

                    // 2014.08.12 #1134 MIYA MOD START
                    //DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, HAIKI_KBN_CD ASC, KOUFUNO ASC, KOUFUNO2 ASC");
                    DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, " +
                                                                "JUTAKUSHAMEI_GOUKEI_NAME ASC, " +  //追加
                                                                "HAIKI_KBN_CD ASC, " +
                                                                "KOUFUNO2 ASC, " +
                                                                "KOUFUNO ASC");
                    // 2014.08.12 #1134 MIYA MOD END

                    foreach (DataRow dr in rows)
                    {
                        sortData.ImportRow(dr);
                    }
                    chouhyouData = sortData;
                    var UniqueRows = chouhyouData.AsEnumerable().Distinct(DataRowComparer.Default);
                    if (UniqueRows.ToList().Count != 0)
                    {
                        chouhyouData = UniqueRows.CopyToDataTable();
                    }
                }
                catch (ConstraintException)
                {
                }

            }
            // 出力区分は紙の場合
            else if (dto.SYUTURYOKU_KBN.Equals("2"))
            {
                chouhyouData = this.dao.GetPrintSaishuushobunItakuChoubo(dto);
            }
            // 出力区分は電子の場合
            else if (dto.SYUTURYOKU_KBN.Equals("3"))
            {
                chouhyouData = this.dao.GetPrintDenshiSaishuushobunItakuChoubo(dto);
            }

            return chouhyouData;
        }
        #endregion

        #region LAYOUT5(最終処分帳簿)内容設定
        /// <summary>
        /// LAYOUT5(最終処分帳簿)内容設定
        /// <param name="dto">入力された内容のDTOクラス</param>
        /// <param name="chouhyouData">帳票データ</param>
        /// </summary>
        private DataTable CreatChouhyouForLayout5(DTOClass dto, DataTable chouhyouData)
        {
            if (dto.SYUTURYOKU_KBN.Equals("1"))
            {
                DataTable chouhyouDataDenshi = new DataTable();
                chouhyouData = this.dao.GetPrintSaishuushobunChoubo(dto);
                chouhyouDataDenshi = this.dao.GetPrintDenshiSaishuushobunChoubo(dto);

                try
                {
                    chouhyouData.BeginLoadData();
                    chouhyouData.Merge(chouhyouDataDenshi);

                    // マージ後、並び替え
                    DataTable sortData = chouhyouData.Clone();

                    // 2014.08.12 #1134 MIYA MOD START 
                    //DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, HAIKI_KBN_CD ASC, KOUFUNO ASC");
                    DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, " +
                                                                "UKEIRESAKI ASC, " +          //追加
                                                                "SHOBUN_HOUHOU_NAME ASC, " +  //追加
                                                                "HAIKI_KBN_CD ASC, " +
                                                                "KOUFUNO ASC");
                    // 2014.08.12 #1134 MIYA MOD START

                    foreach (DataRow dr in rows)
                    {
                        sortData.ImportRow(dr);
                    }
                    chouhyouData = sortData;
                    var UniqueRows = chouhyouData.AsEnumerable().Distinct(DataRowComparer.Default);
                    if (UniqueRows.ToList().Count != 0)
                    {
                        chouhyouData = UniqueRows.CopyToDataTable();
                    }
                }
                catch (ConstraintException)
                {
                }

            }
            // 出力区分は紙の場合
            else if (dto.SYUTURYOKU_KBN.Equals("2"))
            {
                chouhyouData = this.dao.GetPrintSaishuushobunChoubo(dto);
            }
            // 出力区分は電子の場合
            else if (dto.SYUTURYOKU_KBN.Equals("3"))
            {
                chouhyouData = this.dao.GetPrintDenshiSaishuushobunChoubo(dto);
            }

            return chouhyouData;
        }
        #endregion

        #region LAYOUT6(収集運搬帳簿)内容設定
        /// <summary>
        /// LAYOUT6(収集運搬帳簿)内容設定
        /// <param name="dto">入力された内容のDTOクラス</param>
        /// <param name="chouhyouData">帳票データ</param>
        /// </summary>
        private DataTable CreatChouhyouForLayout6(DTOClass dto, DataTable chouhyouData)
        {
            if (dto.SYUTURYOKU_KBN.Equals("1"))
            {
                DataTable chouhyouDataDenshi = new DataTable();
                chouhyouData = this.dao.GetPrintShuushuuunpanChoubo(dto);
                chouhyouDataDenshi = this.dao.GetPrintDenshiShuushuuunpanChoubo(dto);

                try
                {
                    chouhyouData.BeginLoadData();
                    chouhyouData.Merge(chouhyouDataDenshi);

                    // マージ後、並び替え
                    DataTable sortData = chouhyouData.Clone();

                    // 2014.08.11 #1134 MIYA MOD START
                    //DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, HAIKI_KBN_CD ASC, KOUFUNO ASC");
                    DataRow[] rows = chouhyouData.Select("1=1", "HAIKI_SHURUI_CD ASC, " +
                                                                "UNPAN_HOUHOU_CD ASC, " +   // 追加
                                                                "UKEIRESAKI ASC, " +        // 追加
                                                                "UNPANSAKI ASC, " +         // 追加
                                                                "HAIKI_KBN_CD ASC, " +
                                                                "KOUFUNO ASC");
                    // 2014.08.11 #1134 MIYA MOD END

                    foreach (DataRow dr in rows)
                    {
                        sortData.ImportRow(dr);
                    }
                    chouhyouData = sortData;
                    var UniqueRows = chouhyouData.AsEnumerable().Distinct(DataRowComparer.Default);
                    if (UniqueRows.ToList().Count != 0)
                    {
                        chouhyouData = UniqueRows.CopyToDataTable();
                    }
                }
                catch (ConstraintException)
                {
                }

            }
            // 出力区分は紙の場合
            else if (dto.SYUTURYOKU_KBN.Equals("2"))
            {
                chouhyouData = this.dao.GetPrintShuushuuunpanChoubo(dto);
            }
            // 出力区分は電子の場合
            else if (dto.SYUTURYOKU_KBN.Equals("3"))
            {
                chouhyouData = this.dao.GetPrintDenshiShuushuuunpanChoubo(dto);
            }

            return chouhyouData;
        }
        #endregion

        #region 実行ボタンイベント
        /// <summary>
        /// 実行ボタンイベント
        /// </summary>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!this.form.RegistErrorFlag)
            {
                /// 20141209 teikyou 日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return;
                }
                /// 20141209 teikyou 日付チェックを追加する　end
                //DTO作成
                this.dto = new DTOClass();

                //DTO初期化
                this.dto.SYUTURYOKU_KBN = string.Empty;
                this.dto.HAIKIBUTSU_KBN = new List<System.Data.SqlTypes.SqlInt16>();
                this.dto.SYUTURYOKU_NAYIYO = string.Empty;
                this.dto.TYUKANSYORI = string.Empty;
                this.dto.HIDUKESYURUI = string.Empty;
                this.dto.DATE_FROM = string.Empty;
                this.dto.DATE_TO = string.Empty;
                this.dto.KYOTEN_CD = string.Empty;
                this.dto.SBN_GYOUSHA_CD = string.Empty;
                this.dto.SBN_GENBA_CD = string.Empty;
                this.dto.SBN_GENBA_CD_TO = string.Empty;

                //出力区分
                this.dto.SYUTURYOKU_KBN = this.form.txt_ShuturyokuKbn.Text;

                //廃棄物区分
                switch (this.form.txt_ShuturyokuKbn.Text)
                {
                    case "2"://紙のみ
                        //var HaikibbutsuKbn = new DTOClass();

                        //産廃(直行)
                        if (this.form.chk_HaikibutsuKbn1.Checked)
                        {
                            this.dto.HAIKIBUTSU_KBN.Add(Const.UIConstans.Chokkou);
                        }

                        //産廃(積替)
                        if (this.form.chk_HaikibutsuKbn2.Checked)
                        {
                            this.dto.HAIKIBUTSU_KBN.Add(Const.UIConstans.Tsumikae);
                        }

                        //建廃
                        if (this.form.chk_HaikibutsuKbn3.Checked)
                        {
                            this.dto.HAIKIBUTSU_KBN.Add(Const.UIConstans.Kenpai);
                        }

                        //一件も無い時はエラー
                        if (this.dto.HAIKIBUTSU_KBN.Count == 0)
                        {
                            // メッセージの表示
                            msgLogic.MessageBoxShow("E001", this.form.chk_HaikibutsuKbn1.DisplayItemName);
                            this.form.chk_HaikibutsuKbn1.Focus();
                            return;
                        }

                        //全件のときは無指定
                        if (this.dto.HAIKIBUTSU_KBN.Count == 3)
                        {
                            this.dto.HAIKIBUTSU_KBN.Clear();
                        }
                        break;

                    case "1"://合算
                    case "3"://電子のみ
                        break;

                    default:
                        // メッセージの表示
                        msgLogic.MessageBoxShow("E001", this.form.txt_ShuturyokuKbn.DisplayItemName);
                        this.form.txt_ShuturyokuKbn.Focus();
                        return;
                }

                //出力内容
                this.dto.SYUTURYOKU_NAYIYO = this.form.txt_ShuturyokuNaiyo.Text;
                switch (this.dto.SYUTURYOKU_NAYIYO)
                {
                    case "1"://収集運搬帳簿
                        this.layOut = layout.LAYOUT6;
                        break;

                    case "2"://中間処理帳簿
                        this.layOut = layout.LAYOUT3;
                        break;

                    case "3"://運搬帳簿
                        this.layOut = layout.LAYOUT1;
                        break;

                    case "4"://運搬委託帳簿
                        this.layOut = layout.LAYOUT2;
                        break;

                    case "5"://最終処分委託帳簿
                        this.layOut = layout.LAYOUT4;
                        break;

                    case "6"://最終処分帳簿
                        this.layOut = layout.LAYOUT5;
                        break;
                }

                //中間処理
                //出力内容が中間処理の場合
                if (this.form.txt_ShuturyokuNaiyo.Text.Equals("2"))
                {
                    this.dto.TYUKANSYORI = this.form.txt_Tyukansyori.Text;
                }

                //日付種類
                this.dto.HIDUKESYURUI = this.form.txt_HitukeKbn.Text;

                //年月日
                //DATE型変換
                this.dto.DATE_FROM = DateTime.Parse(this.form.dtp_DateFrom.Text).ToShortDateString();
                this.dto.DATE_TO = DateTime.Parse(this.form.dtp_DateTo.Text).ToShortDateString();

                //拠点
                if (this.form.txt_KyotenCD.Text != Const.UIConstans.Zensha)
                {
                    this.dto.KYOTEN_CD = this.form.txt_KyotenCD.Text;
                }

                //処分受託者
                this.dto.SBN_GYOUSHA_CD = this.form.txt_ShobunJutakushaCD.Text;

                //処分事業場(FROM)
                this.dto.SBN_GENBA_CD = this.form.txt_ShobunJigyojoCD.Text;

                //処分事業場(TO)
                this.dto.SBN_GENBA_CD_TO = this.form.txt_ShobunJigyojoCD_To.Text;

                //帳票用データ作成
                DataTable dt = new DataTable();
                dt = GetChouhyouData(this.dto, this.layOut);
                //データ存在判定
                if (dt.Rows.Count != 0)
                {
                    //帳票クラスにデータを渡す
                    ReportInfo reportInfo = new ReportInfo();

                    //reportInfo.Haikibutsu_Report(this.dto.SYUTURYOKU_NAYIYO, corpNameDao.GetCorpName(), this.dto.DATE_FROM, this.dto.DATE_TO, dt);
                    string CorpName = string.Empty;
                    CorpName = ((M_CORP_INFO[])corpNameDao.GetAllValidData(new M_CORP_INFO() { SYS_ID = 0 }))[0].CORP_NAME;
                    reportInfo.Haikibutsu_Report(this.dto.SYUTURYOKU_NAYIYO, CorpName, this.dto.DATE_FROM, this.dto.DATE_TO, dt);

                    //印刷ポツプアップ画面表示
                    using (FormReportPrintPopup report = new FormReportPrintPopup(reportInfo))
                    {

                        //レポートタイトルの設定
                        switch (this.dto.SYUTURYOKU_NAYIYO)
                        {
                            case "1"://収集運搬帳簿
                                report.ReportCaption = "収集運搬帳簿";
                                break;
                            case "2"://中間処理帳簿
                                report.ReportCaption = "中間処理帳簿";
                                break;

                            case "3"://運搬帳簿
                                report.ReportCaption = "運搬帳簿";
                                break;

                            case "4"://運搬委託帳簿
                                report.ReportCaption = "運搬委託帳簿";
                                break;

                            case "5"://最終処分委託帳簿
                                report.ReportCaption = "処分委託帳簿";
                                break;

                            case "6"://最終処分帳簿
                                report.ReportCaption = "最終処分帳簿";
                                break;
                        }

                        report.ShowDialog();
                        report.Dispose();
                    }
                }
                else
                {
                    // メッセージの表示
                    msgLogic.MessageBoxShow("E076");
                }
            }
            else
            {
               this. ForcusErrorControl();
            }

            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion

        #region 閉じるボタンイベント
        /// <summary>
        /// 閉じるボタンイベント
        /// </summary>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //閉じる
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();

            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion

        #region ラジオボタンに対応するテキストボックス更新後処理
        /// <summary>
        /// ラジオボタンに対応するテキストボックス更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void RadioButtonChk(object sender, CancelEventArgs e)
        {
            try
            {
                if (e.Cancel == false)
                {
                    r_framework.CustomControl.CustomNumericTextBox2 tb = (r_framework.CustomControl.CustomNumericTextBox2)sender;
                    if (string.IsNullOrEmpty(tb.Text))
                    {
                        if (this.form.ActiveControl != null)
                        {
                            //ラジオボタン日付種類
                            if (tb.Name.Equals("txt_HitukeKbn"))
                            {
                                if (this.form.ActiveControl.Name != "rdo_KoufuDate"
                                    && this.form.ActiveControl.Name != "rdo_UnpanEndDate"
                                    && this.form.ActiveControl.Name != "rdo_ShobunEndDate")
                                {
                                    var messageShowLogic = new MessageBoxShowLogic();
                                    messageShowLogic.MessageBoxShow("W001", "1", "3");
                                    e.Cancel = true;
                                }
                            }
                            //ラジオボタン出力内容
                            else if (tb.Name.Equals("txt_ShuturyokuNaiyo"))
                            {
                                if (this.form.ActiveControl.Name != "rdo_ShushuUnpanJisha"
                                    && this.form.ActiveControl.Name != "rdo_TyukanShori"
                                    && this.form.ActiveControl.Name != "rdo_UnpanJisha"
                                    && this.form.ActiveControl.Name != "rdo_UnpanItaku"
                                    && this.form.ActiveControl.Name != "rdo_SaishuShobunItaku"
                                    && this.form.ActiveControl.Name != "rdo_SaishuShobunShobun")
                                {
                                    var messageShowLogic = new MessageBoxShowLogic();
                                    messageShowLogic.MessageBoxShow("W001", "1", "6");
                                    e.Cancel = true;
                                }
                            }
                            //ラジオボタン中間処理
                            else if (tb.Name.Equals("txt_Tyukansyori"))
                            {
                                if (this.form.ActiveControl.Name != "rdo_Tyukan1"
                                    && this.form.ActiveControl.Name != "rdo_Tyukan2"
                                    && this.form.ActiveControl.Name != "rdo_Tyukan3")
                                {
                                    var messageShowLogic = new MessageBoxShowLogic();
                                    messageShowLogic.MessageBoxShow("W001", "1", "3");
                                    e.Cancel = true;
                                }
                            }
                            //ラジオボタン出力区分
                            else if (tb.Name.Equals("txt_ShuturyokuKbn"))
                            {
                                if (this.form.ActiveControl.Name != "rdo_ShuturyokuKbn1"
                                    && this.form.ActiveControl.Name != "rdo_ShuturyokuKbn2"
                                    && this.form.ActiveControl.Name != "rdo_ShuturyokuKbn3")
                                {
                                    var messageShowLogic = new MessageBoxShowLogic();
                                    messageShowLogic.MessageBoxShow("W001", "1", "3");
                                    e.Cancel = true;
                                }
                            }

                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RadioButtonChk", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }
        #endregion

        #region ラジオボタンに対応するテキストボックス更新後処理
        /// <summary>
        /// ラジオボタンに対応するテキストボックス更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal Boolean TextBoxFromToChk(Object TargetObject, CustomTextBox TextBoxFrom, CustomTextBox TextBoxTo)
        {
            bool isErr = false;
            try
            {
                if (String.IsNullOrEmpty(TextBoxFrom.Text))
                {
                    return isErr;
                }

                if (String.IsNullOrEmpty(TextBoxTo.Text))
                {
                    return isErr;
                }

                int iCompare = string.Compare(TextBoxFrom.Text, TextBoxTo.Text);
                if (iCompare > 0)
                {
                    //色を白くする
                    r_framework.CustomControl.ICustomAutoChangeBackColor t; //色変え用インターフェース
                    t = TargetObject as r_framework.CustomControl.ICustomAutoChangeBackColor;
                    t.UpdateBackColor(false);
                    TextBoxFrom.Focus();

                    //エラーメッセージ
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E032", TextBoxFrom.DisplayItemName + "(From)", TextBoxTo.DisplayItemName + "(To)");

                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TextBoxFromToChk", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }
        #endregion

        #region 実装が必要メッソド
        /// <summary>
        /// 現場CDの存在チェック
        /// </summary>
        public void setHeaderForm(UIHeader hs)
        {
            this.headerForm = hs;
        }

        /// <summary>
        /// ロジック削除（使用しない）
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除（使用しない）
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録（使用しない）
        /// </summary>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 検索（使用しない）
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新（使用しない）
        /// </summary>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない start
        #region 「F6 CSV出力ボタン」イベント処理
        /// <summary>
        /// 「F6 CSV出力ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                /// 20141209 teikyou 日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return;
                }
                /// 20141209 teikyou 日付チェックを追加する　end
                //ｃｓｖ初期化
                this.csvInit();
                //ｃｓｖデータ取得
                DataTable dtCsv = new DataTable();
                dtCsv = this.GetChouhyouData(this.dto, this.layOut);

                // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
                if (dtCsv.Rows.Count == 0)
                {
                    msgLogic.MessageBoxShow("E044");
                    return;
                }
                //ｃｓｖデータ設定
                this.SetCsv(dtCsv);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        /// <summary>
        /// ｃｓｖデータ設定
        /// </summary>
        private void SetCsv(DataTable dt)
        {
            //1.収集運搬（自社）
            string[] csvHeadSs = { "№", "廃棄物名CD(報告書分類CD)", "廃棄物名(報告書分類名)", "運搬終了日", "交付年月日", "廃棄物区分", "交付者名", "交付番号", "受入先", "運搬先", "受入量", "運搬量", "積替または保管場所", "運搬方法", "搬出量" };
            //2.中間処理
            string[] csvHeadCk = { "№", "廃棄物名CD(報告書分類CD)", "廃棄物名(報告書分類名)", "処分年月日", "交付年月日", "廃棄物区分", "交付者名", "交付番号", "受入先", "処分方法", "受入量", "処分量", "交付番号 (二次)", "持出先", "持出量" };
            //3.運搬（自社）
            string[] csvHeadUpJs = { "№", "廃棄物名CD(報告書分類CD)", "廃棄物名(報告書分類名)", "運搬終了日", "交付年月日", "廃棄物区分", "交付者名", "交付番号", "受入先", "運搬先", "受入量", "運搬量", "積替または保管場所", "運搬方法", "搬出量" };
            //4.運搬（委託）
            string[] csvHeadUpIt = { "№", "廃棄物名CD(報告書分類CD)", "廃棄物名(報告書分類名)", "委託年月日", "交付年月日", "廃棄物区分", "交付番号", "運搬元許可番号", "受託者", "受託者住所", "運搬先許可番号", "運搬先", "委託量" };
            //5.処分（委託）
            string[] csvHeadSb = { "№", "廃棄物名CD(報告書分類CD)", "廃棄物名(報告書分類名)", "委託年月日", "廃棄物区分", "許可番号", "受託者", "受託者住所", "交付年月日（二次）", "交付番号（二次）", "交付者名", "交付年月日", "交付番号", "委託の内容", "委託量" };
            //6.最終処分（処分）
            string[] csvHeadSsSb = { "№", "廃棄物名CD(報告書分類CD)", "廃棄物名(報告書分類名)", "処分年月日", "交付年月日", "廃棄物区分", "交付者名", "交付番号", "受入先", "処分方法", "受入量", "処分量" };

            //csvファイル名とシート名
            string[] csvFileName = { "収集運搬帳簿", "中間処理帳簿", "運搬帳簿", "運搬委託帳簿", "処分委託帳簿", "最終処分帳簿" };

            //出力CSV内容判定
            switch (this.form.txt_ShuturyokuNaiyo.Text)
            {
                case "1"://1.収集運搬（自社）
                    this.SetCsvContent(dt, csvHeadSs, csvFileName[0]);
                    break;

                case "2"://2.中間処理
                    this.SetCsvContent(dt, csvHeadCk, csvFileName[1]);
                    break;

                case "3"://3.運搬（自社）
                    this.SetCsvContent(dt, csvHeadUpJs, csvFileName[2]);
                    break;

                case "4"://4.運搬（委託）
                    this.SetCsvContent(dt, csvHeadUpIt, csvFileName[3]);
                    break;

                case "5"://5.処分（委託）
                    this.SetCsvContent(dt, csvHeadSb, csvFileName[4]);
                    break;

                case "6"://6.最終処分（処分）
                    this.SetCsvContent(dt, csvHeadSsSb, csvFileName[5]);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ｃｓｖHeadTitle　ｃｓｖタイトルと内容を設定
        /// </summary>
        private void SetCsvContent(DataTable dt, string[] strColName, string strCsvFileName)
        {

            if (strColName.Length > 0)
            {
                DataTable csvDT = new DataTable();
                DataRow rowTmp;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //タイトル設定
                for (int i = 0; i < strColName.Length; i++)
                {
                    csvDT.Columns.Add(strColName[i]);
                }

                //ｃｓｖ内容設定
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rowTmp = csvDT.NewRow();
                    //共通項目
                    rowTmp["№"] = (i + 1).ToString();
                    rowTmp["廃棄物名CD(報告書分類CD)"] = dt.Rows[i]["HAIKI_SHURUI_CD"] == null ? string.Empty : dt.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    rowTmp["交付年月日"] = dt.Rows[i]["KOUFU_NENGAPPI"] == null ? string.Empty : dt.Rows[i]["KOUFU_NENGAPPI"].ToString();
                    rowTmp["廃棄物区分"] = dt.Rows[i]["HAIKIBUTU_KBN"].ToString();
                    rowTmp["廃棄物名(報告書分類名)"] = dt.Rows[i]["HAIKI_SHURUI_NAME"] == null ? string.Empty : dt.Rows[i]["HAIKI_SHURUI_NAME"].ToString();
                    rowTmp["交付番号"] = dt.Rows[i]["KOUFUNO"] == null ? string.Empty : dt.Rows[i]["KOUFUNO"].ToString();

                    //出力CSV内容判定
                    switch (this.form.txt_ShuturyokuNaiyo.Text)
                    {
                        case "1"://1.収集運搬（自社）
                        case "3"://3.運搬（自社）

                            if (this.form.txt_ShuturyokuNaiyo.Text.Equals("3"))
                            {
                                rowTmp["運搬終了日"] = dt.Rows[i]["UNPAN_NENGAPPI"] == null ? string.Empty : dt.Rows[i]["UNPAN_NENGAPPI"].ToString();

                            }
                            else
                            {
                                rowTmp["運搬終了日"] = dt.Rows[i]["ITAKU_NENGAPPI"] == null ? string.Empty : dt.Rows[i]["ITAKU_NENGAPPI"].ToString();
                            }
                            rowTmp["交付者名"] = dt.Rows[i]["KOUFUSHAMEI"] == null ? string.Empty : dt.Rows[i]["KOUFUSHAMEI"].ToString();
                            rowTmp["受入先"] = dt.Rows[i]["UKEIRESAKI"] == null ? string.Empty : dt.Rows[i]["UKEIRESAKI"].ToString();
                            rowTmp["運搬先"] = dt.Rows[i]["UNPANSAKI"] == null ? string.Empty : dt.Rows[i]["UNPANSAKI"].ToString();
                            rowTmp["受入量"] = Convert.ToDecimal(dt.Rows[i]["UKEIRERYO"]).ToString("F3") + " " + dt.Rows[i]["UKEIRERYO_TANI"].ToString();
                            rowTmp["運搬量"] = Convert.ToDecimal(dt.Rows[i]["UNPANRYO"]).ToString("F3") + " " + dt.Rows[i]["UNPANRYO_TANI"].ToString();
                            rowTmp["積替または保管場所"] = dt.Rows[i]["TUMIKAEHOKAN"] == null ? string.Empty : dt.Rows[i]["TUMIKAEHOKAN"].ToString();
                            rowTmp["運搬方法"] = dt.Rows[i]["UNPAN_HOUHOU_CD"] == null ? string.Empty : dt.Rows[i]["UNPAN_HOUHOU_CD"].ToString() + " " + dt.Rows[i]["UNPAN_HOUHOU_NAME"] == null ? string.Empty : dt.Rows[i]["UNPAN_HOUHOU_NAME"].ToString();
                            rowTmp["搬出量"] = Convert.ToDecimal(dt.Rows[i]["HANSHUTURYO"]).ToString("F3") + " " + dt.Rows[i]["HANSHUTURYO_TANI"].ToString();

                            break;

                        case "2"://2.中間処理

                            rowTmp["処分年月日"] = dt.Rows[i]["SHOBUN_NENGAPPI"] == null ? string.Empty : dt.Rows[i]["SHOBUN_NENGAPPI"].ToString();
                            rowTmp["交付者名"] = dt.Rows[i]["KOUFUSHAMEI"] == null ? string.Empty : dt.Rows[i]["KOUFUSHAMEI"].ToString();
                            rowTmp["受入先"] = dt.Rows[i]["UKEIRESAKI"].ToString();
                            rowTmp["処分方法"] = dt.Rows[i]["SHOBUN_HOUHOU_NAME"] == null ? string.Empty : dt.Rows[i]["SHOBUN_HOUHOU_NAME"].ToString();
                            rowTmp["受入量"] = Convert.ToDecimal(dt.Rows[i]["UKEIRERYO"]).ToString("F3") + " " + dt.Rows[i]["UKEIRERYO_TANI"].ToString();
                            rowTmp["処分量"] = Convert.ToDecimal(dt.Rows[i]["SHOBUNRYO"]).ToString("F3") + " " + dt.Rows[i]["SHOBUNRYO_TANI"].ToString();
                            rowTmp["交付番号 (二次)"] = dt.Rows[i]["KOUFUNO2"] == null ? string.Empty : dt.Rows[i]["KOUFUNO2"].ToString();
                            rowTmp["持出先"] = dt.Rows[i]["MOCHIDASHISAKI"] == null ? string.Empty : dt.Rows[i]["MOCHIDASHISAKI"].ToString();
                            rowTmp["持出量"] = Convert.ToDecimal(dt.Rows[i]["MOCHIDASHIRYO"]).ToString("F3") + " " + dt.Rows[i]["MOCHIDASHIRYO_TANI"].ToString();

                            break;

                        case "4"://4.運搬（委託）
                            rowTmp["委託年月日"] = dt.Rows[i]["ITAKU_NENGAPPI"] == null ? string.Empty : dt.Rows[i]["ITAKU_NENGAPPI"].ToString();
                            rowTmp["運搬先"] = dt.Rows[i]["UNPANSAKI"] == null ? string.Empty : dt.Rows[i]["UNPANSAKI"].ToString();
                            rowTmp["運搬元許可番号"] = dt.Rows[i]["KYOKANO"] == null ? string.Empty : dt.Rows[i]["KYOKANO"].ToString();
                            rowTmp["運搬先許可番号"] = dt.Rows[i]["UPNSAKI_KYOKANO"] == null ? string.Empty : dt.Rows[i]["UPNSAKI_KYOKANO"].ToString();
                            rowTmp["受託者"] = dt.Rows[i]["JUTAKUSHA"] == null ? string.Empty : dt.Rows[i]["JUTAKUSHA"].ToString();
                            rowTmp["受託者住所"] = dt.Rows[i]["JUTAKUSHA_ADDRESS"] == null ? string.Empty : dt.Rows[i]["JUTAKUSHA_ADDRESS"].ToString();
                            rowTmp["委託量"] = Convert.ToDecimal(dt.Rows[i]["ITAKURYO"]).ToString("F3") + " " + dt.Rows[i]["ITAKURYO_TANI"].ToString();

                            break;

                        case "5"://5.処分（委託）
                            rowTmp["委託年月日"] = dt.Rows[i]["ITAKU_NENGAPPI"] == null ? string.Empty : dt.Rows[i]["ITAKU_NENGAPPI"].ToString();
                            rowTmp["交付者名"] = dt.Rows[i]["KOUFUSHAMEI"] == null ? string.Empty : dt.Rows[i]["KOUFUSHAMEI"].ToString();
                            rowTmp["許可番号"] = dt.Rows[i]["KYOKANO"] == null ? string.Empty : dt.Rows[i]["KYOKANO"].ToString();
                            rowTmp["受託者"] = dt.Rows[i]["JUTAKUSHAMEI"] == null ? string.Empty : dt.Rows[i]["JUTAKUSHAMEI"].ToString();
                            rowTmp["受託者住所"] = dt.Rows[i]["JUTAKUSHA_ADDRESS"] == null ? string.Empty : dt.Rows[i]["JUTAKUSHA_ADDRESS"].ToString();
                            rowTmp["交付年月日（二次）"] = dt.Rows[i]["KOUFU_NENGAPPI2"] == null ? string.Empty : dt.Rows[i]["KOUFU_NENGAPPI2"].ToString();
                            rowTmp["交付番号（二次）"] = dt.Rows[i]["KOUFUNO2"] == null ? string.Empty : dt.Rows[i]["KOUFUNO2"].ToString();
                            rowTmp["委託の内容"] = dt.Rows[i]["ITAKUNAIYOU"] == null ? string.Empty : dt.Rows[i]["ITAKUNAIYOU"].ToString();
                            rowTmp["委託量"] = Convert.ToDecimal(dt.Rows[i]["ITAKURYO"]).ToString("F3") + " " + dt.Rows[i]["ITAKURYO_TANI"].ToString();

                            break;

                        case "6"://6.最終処分（処分）
                            rowTmp["処分年月日"] = dt.Rows[i]["SHOBUN_NENGAPPI"] == null ? string.Empty : dt.Rows[i]["SHOBUN_NENGAPPI"].ToString();
                            rowTmp["交付者名"] = dt.Rows[i]["KOUFUSHAMEI"] == null ? string.Empty : dt.Rows[i]["KOUFUSHAMEI"].ToString();
                            rowTmp["受入先"] = dt.Rows[i]["UKEIRESAKI"].ToString();
                            rowTmp["処分方法"] = dt.Rows[i]["SHOBUN_HOUHOU_NAME"] == null ? string.Empty : dt.Rows[i]["SHOBUN_HOUHOU_NAME"].ToString();
                            rowTmp["受入量"] = Convert.ToDecimal(dt.Rows[i]["UKEIRERYO"]).ToString("F3") + " " + dt.Rows[i]["UKEIRERYO_TANI"].ToString();
                            rowTmp["処分量"] = Convert.ToDecimal(dt.Rows[i]["SHOBUNRYO"]).ToString("F3") + " " + dt.Rows[i]["SHOBUNRYO_TANI"].ToString();
                            break;
                        default:
                            break;
                    }

                    csvDT.Rows.Add(rowTmp);
                }

                this.form.customDataGridView1.DataSource = csvDT;

                //タイトル順設定
                for (int i = 0; i < strColName.Length; i++)
                {
                    this.form.customDataGridView1.Columns[i].DisplayIndex = i;
                }

                // 出力先指定のポップアップを表示させる。             
                if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, strCsvFileName, this.form);
                }

            }
        }

        private void csvInit()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //DTO作成
            this.dto = new DTOClass();
            //DTO初期化
            this.dto.SYUTURYOKU_KBN = string.Empty;
            this.dto.HAIKIBUTSU_KBN = new List<System.Data.SqlTypes.SqlInt16>();
            this.dto.SYUTURYOKU_NAYIYO = string.Empty;
            this.dto.TYUKANSYORI = string.Empty;
            this.dto.HIDUKESYURUI = string.Empty;
            this.dto.DATE_FROM = string.Empty;
            this.dto.DATE_TO = string.Empty;
            this.dto.KYOTEN_CD = string.Empty;
            this.dto.SBN_GYOUSHA_CD = string.Empty;
            this.dto.SBN_GENBA_CD = string.Empty;
            this.dto.SBN_GENBA_CD_TO = string.Empty;

            //出力区分
            this.dto.SYUTURYOKU_KBN = this.form.txt_ShuturyokuKbn.Text;

            //廃棄物区分
            switch (this.form.txt_ShuturyokuKbn.Text)
            {
                case "2"://紙のみ
                    //var HaikibbutsuKbn = new DTOClass();

                    //産廃(直行)
                    if (this.form.chk_HaikibutsuKbn1.Checked)
                    {
                        this.dto.HAIKIBUTSU_KBN.Add(Const.UIConstans.Chokkou);
                    }

                    //産廃(積替)
                    if (this.form.chk_HaikibutsuKbn2.Checked)
                    {
                        this.dto.HAIKIBUTSU_KBN.Add(Const.UIConstans.Tsumikae);
                    }

                    //建廃
                    if (this.form.chk_HaikibutsuKbn3.Checked)
                    {
                        this.dto.HAIKIBUTSU_KBN.Add(Const.UIConstans.Kenpai);
                    }

                    //一件も無い時はエラー
                    if (this.dto.HAIKIBUTSU_KBN.Count == 0)
                    {
                        // メッセージの表示
                        msgLogic.MessageBoxShow("E001", this.form.chk_HaikibutsuKbn1.DisplayItemName);
                        this.form.chk_HaikibutsuKbn1.Focus();
                        return;
                    }

                    //全件のときは無指定
                    if (this.dto.HAIKIBUTSU_KBN.Count == 3)
                    {
                        this.dto.HAIKIBUTSU_KBN.Clear();
                    }
                    break;

                case "1"://合算
                case "3"://電子のみ
                    break;

                default:
                    // メッセージの表示
                    msgLogic.MessageBoxShow("E001", this.form.txt_ShuturyokuKbn.DisplayItemName);
                    this.form.txt_ShuturyokuKbn.Focus();
                    return;
            }

            //出力内容
            this.dto.SYUTURYOKU_NAYIYO = this.form.txt_ShuturyokuNaiyo.Text;
            switch (this.dto.SYUTURYOKU_NAYIYO)
            {
                case "1"://収集運搬帳簿
                    this.layOut = layout.LAYOUT6;
                    break;

                case "2"://中間処理帳簿
                    this.layOut = layout.LAYOUT3;
                    break;

                case "3"://運搬帳簿
                    this.layOut = layout.LAYOUT1;
                    break;

                case "4"://運搬委託帳簿
                    this.layOut = layout.LAYOUT2;
                    break;

                case "5"://最終処分委託帳簿
                    this.layOut = layout.LAYOUT4;
                    break;

                case "6"://最終処分帳簿
                    this.layOut = layout.LAYOUT5;
                    break;
            }

            //中間処理
            //出力内容が中間処理の場合
            if (this.form.txt_ShuturyokuNaiyo.Text.Equals("2"))
            {
                this.dto.TYUKANSYORI = this.form.txt_Tyukansyori.Text;
            }

            //日付種類
            this.dto.HIDUKESYURUI = this.form.txt_HitukeKbn.Text;

            //年月日
            //DATE型変換
            this.dto.DATE_FROM = DateTime.Parse(this.form.dtp_DateFrom.Text).ToShortDateString();
            this.dto.DATE_TO = DateTime.Parse(this.form.dtp_DateTo.Text).ToShortDateString();

            //拠点
            if (this.form.txt_KyotenCD.Text != Const.UIConstans.Zensha)
            {
                this.dto.KYOTEN_CD = this.form.txt_KyotenCD.Text;
            }

            //処分受託者
            this.dto.SBN_GYOUSHA_CD = this.form.txt_ShobunJutakushaCD.Text;

            //処分事業場(FROM)
            this.dto.SBN_GENBA_CD = this.form.txt_ShobunJigyojoCD.Text;

            //処分事業場(TO)
            this.dto.SBN_GENBA_CD_TO = this.form.txt_ShobunJigyojoCD_To.Text;
        }

        // 20140620 syunrei EV004843_廃棄物帳簿（全種類）CSV出力機能がない end


        /// 20141023 Houkakou 「廃棄物帳簿」のダブルクリックを追加する　start
        #region DateToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_DateTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.dtp_DateFrom;
            var ToTextBox = this.form.dtp_DateTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ShobunJigyojoCD_Toダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txt_ShobunJigyojoCD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.txt_ShobunJigyojoCD;
            var ToTextBox = this.form.txt_ShobunJigyojoCD_To;

            ToTextBox.Text = FromTextBox.Text;
            this.form.txt_ShobunJigyojoName_To.Text = this.form.txt_ShobunJigyojoName.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141023 Houkakou 「廃棄物帳簿」のダブルクリックを追加する　end

        /// 20141209 teikyou 日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.dtp_DateFrom.BackColor = Constans.NOMAL_COLOR;
            this.form.dtp_DateTo.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.dtp_DateFrom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.dtp_DateTo.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.form.dtp_DateFrom.Value);
            DateTime date_to = Convert.ToDateTime(this.form.dtp_DateTo.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.dtp_DateFrom.IsInputErrorOccured = true;
                this.form.dtp_DateTo.IsInputErrorOccured = true;
                this.form.dtp_DateFrom.BackColor = Constans.ERROR_COLOR;
                this.form.dtp_DateTo.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = new string[] { };
                switch (this.form.txt_HitukeKbn.Text)
                {
                    case "1":
                        errorMsg = new string[] { "交付年月日From", "交付年月日To" };
                        break;
                    case "2":
                        errorMsg = new string[] { "運搬終了日From", "運搬終了日To" };
                        break;
                    case "3":
                        errorMsg = new string[] { "処分終了日From", "処分終了日To" };
                        break;
                }
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.dtp_DateFrom.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region dtp_DateFrom_Leaveイベント
        /// <summary>
        /// dtp_DateFrom_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_DateFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_DateTo.Text))
            {
                this.form.dtp_DateTo.IsInputErrorOccured = false;
                this.form.dtp_DateTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region dtp_DateTo_Leaveイベント
        /// <summary>
        /// dtp_DateTo_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_DateTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_DateFrom.Text))
            {
                this.form.dtp_DateFrom.IsInputErrorOccured = false;
                this.form.dtp_DateFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141209 teikyou 日付チェックを追加する　end

        #region 20151026 hoanghm #13499
        private void ForcusErrorControl()
        {
            var focusControl = this.form.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
            if (focusControl != null)
            {
                ((Control)focusControl).Focus();
            }
        }
        #endregion
    }
}