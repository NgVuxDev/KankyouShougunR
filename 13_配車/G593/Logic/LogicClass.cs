using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;
using r_framework.CustomControl;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Allocation.KaraContenaIchiranHyou.Report;
using CommonChouhyouPopup.App;
using System.Drawing;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Allocation.KaraContenaIchiranHyou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数
        /// <summary>「受注配車含む」の値</summary>
        private readonly string JuchuHaishaHukumu = "1";
        private readonly string JuchuHaishaHukumanai = "2";
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region Fileds
        /// <summary>ボタン情報を格納しているＸＭＬファイルのパス（リソース）を保持するフィールド</summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.Allocation.KaraContenaIchiranHyou.Setting.ButtonSetting.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        private M_SYS_INFO sysInfoEntity;
        private MessageBoxShowLogic MsgBox;
        #endregion

        #region Constructors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            this.form = targetForm;
            this.dto = new DTOClass();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.MsgBox = new MessageBoxShowLogic();
        }
        #endregion

        #region init
        internal void WindowInit()
        {
            try
            {
                // ボタンのテキスト初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                /**
                 * 表示データ初期値設定
                 */
                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Kijunbi.Value = parentForm.sysDate;
                this.form.JuchuHaishaHukumu.Text = this.JuchuHaishaHukumanai;

                // システム情報を取得し、初期値をセットする
                this.GetSysInfoInit();

                // コンテナ管理方法によってデザインを変更する
                this.ChangeLabelAndLayout();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
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
            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 表示ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

            // 表示ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            // 20141127 teikyou ダブルクリックを追加する　start
            this.form.ContenaShuruiCDEnd.MouseDoubleClick += new MouseEventHandler(ContenaShuruiCDEnd_MouseDoubleClick);
            // 20141127 teikyou ダブルクリックを追加する　end
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
            int kontenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;

            if (kontenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                // 個体管理

                // レイアウト調整
                this.form.label1.Visible = false;
                this.form.Kijunbi.Visible = false;
                this.form.label2.Visible = false;
                this.form.customPanel2.Visible = false;

                this.form.customPanelSyukeiKomoku8.Location = new Point(21, 25);
            }
            else
            {
                // 数量管理
                // デザイナのほうは数量管理用のデザインになっているためここでは何もしない
            }
        }
        #endregion
        #endregion

        #region 条件空チェック
        /// <summary>
        /// 画面の条件をチェックし空だった場合はデフォルト値をセットする
        /// </summary>
        /// <returns></returns>
        internal bool CheckAndInitCondition()
        {
            bool ret = true;
            try
            {
                // 基準日
                var parentForm = (BusinessBaseForm)this.form.Parent;
                if (this.form.Kijunbi.Value == null)
                {
                    this.form.Kijunbi.Value = parentForm.sysDate;
                }

                // 受注・配車含む
                if (string.IsNullOrEmpty(this.form.JuchuHaishaHukumu.Text))
                {
                    this.form.JuchuHaishaHukumu.Text = this.JuchuHaishaHukumanai;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckAndInitCondition", ex);
                this.MsgBox.MessageBoxShow("E245", "");
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
        internal bool CheckEmptyForContenaShuruiCd()
        {
            bool ret = true;
            try
            {
                // StartとEndが空なら最小値～最大値を設定→どちらか空なら、それぞれ最小値と最大値を設定
                if (string.IsNullOrEmpty(this.form.ContenaShuruiCDStart.Text)
                    || string.IsNullOrEmpty(this.form.ContenaShuruiCDEnd.Text))
                {
                    var contenaShuruiDao = DaoInitUtility.GetComponent<IM_CONTENA_SHURUIDao>();
                    var allContenaShuruiCd = contenaShuruiDao.GetAllData();

                    if (allContenaShuruiCd.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(this.form.ContenaShuruiCDStart.Text))
                        {
                            var minContenaShurui = allContenaShuruiCd.Where(c => c.CONTENA_SHURUI_CD == allContenaShuruiCd.Min(co => co.CONTENA_SHURUI_CD)).FirstOrDefault();
                            this.form.ContenaShuruiCDStart.Text = minContenaShurui.CONTENA_SHURUI_CD;
                            this.form.ContenaShuruiMeiStart.Text = minContenaShurui.CONTENA_SHURUI_NAME_RYAKU;
                        }

                        if (string.IsNullOrEmpty(this.form.ContenaShuruiCDEnd.Text))
                        {
                            var maxContenaShurui = allContenaShuruiCd.Where(c => c.CONTENA_SHURUI_CD == allContenaShuruiCd.Max(co => co.CONTENA_SHURUI_CD)).FirstOrDefault();

                            this.form.ContenaShuruiCDEnd.Text = maxContenaShurui.CONTENA_SHURUI_CD;
                            this.form.ContenaShuruiMeiEnd.Text = maxContenaShurui.CONTENA_SHURUI_NAME_RYAKU;
 
                        }

                    }
                }

                if (!this.CheckCodeFromTo(this.form.ContenaShuruiCDStart, this.form.ContenaShuruiCDEnd))
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E032", "コンテナ種類CDFrom", "コンテナ種類To");
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEmptyForContenaShuruiCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion

        #region Util
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

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns>CSV出力</returns>
        public int CSVPrint()
        {
            int returnVal = 0;
            try
            {
                // 基準日
                if (this.form.Kijunbi.Value != null)
                {
                    this.dto.kijunbi = (DateTime)this.form.Kijunbi.Value;
                }

                // 受注・配車含む
                if (!string.IsNullOrEmpty(this.form.JuchuHaishaHukumu.Text))
                {
                    this.dto.juchuHaishaHukumu = this.form.JuchuHaishaHukumu.Text;
                }

                // コンテナ種類Start
                if (!string.IsNullOrEmpty(this.form.ContenaShuruiCDStart.Text))
                {
                    this.dto.contenaChuruiCdStart = this.form.ContenaShuruiCDStart.Text;
                }

                // コンテナ種類End
                if (!string.IsNullOrEmpty(this.form.ContenaShuruiCDEnd.Text))
                {
                    this.dto.contenaChuruiCdEnd = this.form.ContenaShuruiCDEnd.Text;
                }

                var dao = DaoInitUtility.GetComponent<DAOClass>();
                DataTable printHeaderData = new DataTable();
                DataTable printDetailData = new DataTable();
                string layoutName;

                int kontenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                    ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;

                DataTable csvDT = new DataTable();
                DataRow rowTmp;

                if (kontenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
                {
                    #region コンテナ個体管理
                    // コンテナ個体管理
                    var contenaData = dao.GetContenaData(dto);

                    string[] csvHead = { "コンテナ種類CD", "コンテナ種類名", "コンテナCD", "コンテナ名"};

                    for (int i = 0; i < csvHead.Length; i++)
                    {
                        csvDT.Columns.Add(csvHead[i]);
                    }
                    foreach (var contena in contenaData)
                    {
                        rowTmp = csvDT.NewRow();
                        rowTmp["コンテナ種類CD"] = contena.CONTENA_SHURUI_CD;
                        rowTmp["コンテナ種類名"] = contena.CONTENA_SHURUI_NAME_RYAKU;
                        rowTmp["コンテナCD"] = contena.CONTENA_CD;
                        rowTmp["コンテナ名"] = contena.CONTENA_NAME_RYAKU;

                        csvDT.Rows.Add(rowTmp);
                    }
                    #endregion
                }
                else
                {
                    #region コンテナ数量管理
                    // コンテナ数量管理
                    var reserveAndResultData = dao.GetContenaReserveAndResultData(dto);

                    // コンテナ種類CD毎に集計
                    var contenaShuruiDao = DaoInitUtility.GetComponent<IM_CONTENA_SHURUIDao>();
                    var allContenaShurui = contenaShuruiDao.GetAllValidData(new M_CONTENA_SHURUI());

                    string[] csvHead = { "コンテナ種類CD", "コンテナ種類名", "残数量" };

                    var targetContenaShuruiCds = allContenaShurui.Where(w =>
                                                w.CONTENA_SHURUI_CD.CompareTo(dto.contenaChuruiCdStart) >= 0
                                                && w.CONTENA_SHURUI_CD.CompareTo(dto.contenaChuruiCdEnd) <= 0
                                            ).Select(s => new { s.CONTENA_SHURUI_CD }).GroupBy(g => new { g.CONTENA_SHURUI_CD });

                    for (int i = 0; i < csvHead.Length; i++)
                    {
                        csvDT.Columns.Add(csvHead[i]);
                    }

                    foreach (var targetShuruiCd in targetContenaShuruiCds)
                    {
                        int shoyuuDaisuu = 0;
                        var contenaShuruiMaserData
                            = allContenaShurui.Where(w => w.CONTENA_SHURUI_CD.ToString().Equals(targetShuruiCd.Key.CONTENA_SHURUI_CD)).FirstOrDefault();

                        // マスタデータが存在しない場合は表示しない。
                        // SQLのWHERE句でチェックすると時間が掛かりそうなのでLogicClassでチェック
                        if (contenaShuruiMaserData == null
                            || contenaShuruiMaserData.DELETE_FLG.IsTrue)
                        {
                            continue;
                        }

                        if (!contenaShuruiMaserData.SHOYUU_DAISUU.IsNull)
                        {
                            shoyuuDaisuu = (int)contenaShuruiMaserData.SHOYUU_DAISUU;
                        }

                        // コンテナ数量の計算
                        var targetContenaData
                            = reserveAndResultData.Where(w => w.CONTENA_SHURUI_CD.ToString().Equals(targetShuruiCd.Key.CONTENA_SHURUI_CD));

                        foreach (var tempContenaData in targetContenaData)
                        {
                            if (tempContenaData == null
                                || tempContenaData.DAISUU_CNT.IsNull
                                || tempContenaData.CONTENA_SET_KBN.IsNull)
                            {
                                continue;
                            }

                            if (tempContenaData.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                            {
                                // 減算
                                shoyuuDaisuu -= (int)tempContenaData.DAISUU_CNT;
                            }
                            else if (tempContenaData.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                            {
                                // 加算
                                shoyuuDaisuu += (int)tempContenaData.DAISUU_CNT;
                            }
                        }

                        // CSVデータ(Detail)一行分作成
                        rowTmp = csvDT.NewRow();
                        rowTmp["コンテナ種類CD"] = contenaShuruiMaserData.CONTENA_SHURUI_CD;
                        rowTmp["コンテナ種類名"] = contenaShuruiMaserData.CONTENA_SHURUI_NAME;
                        rowTmp["残数量"] = shoyuuDaisuu;

                        csvDT.Rows.Add(rowTmp);
                    }
                    #endregion
                }

                if (csvDT == null || csvDT.Rows.Count == 0)
                {
                    this.MsgBox.MessageBoxShow("E044");
                    return -1;
                }

                // 出力先指定のポップアップを表示させる。
                if (this.MsgBox.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_KARA_CONTENA_ICHIRAN_HYOU), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVPrint", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = -1;
            }
            return returnVal;
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns>検索件数</returns>
        public int Search()
        {
            int returnVal = 0;
            try
            {
                // 基準日
                if (this.form.Kijunbi.Value != null)
                {
                    this.dto.kijunbi = (DateTime)this.form.Kijunbi.Value;
                }

                // 受注・配車含む
                if (!string.IsNullOrEmpty(this.form.JuchuHaishaHukumu.Text))
                {
                    this.dto.juchuHaishaHukumu = this.form.JuchuHaishaHukumu.Text;
                }

                // コンテナ種類Start
                if (!string.IsNullOrEmpty(this.form.ContenaShuruiCDStart.Text))
                {
                    this.dto.contenaChuruiCdStart = this.form.ContenaShuruiCDStart.Text;
                }

                // コンテナ種類End
                if (!string.IsNullOrEmpty(this.form.ContenaShuruiCDEnd.Text))
                {
                    this.dto.contenaChuruiCdEnd = this.form.ContenaShuruiCDEnd.Text;
                }

                var dao = DaoInitUtility.GetComponent<DAOClass>();
                DataTable printHeaderData = new DataTable();
                DataTable printDetailData = new DataTable();
                string layoutName;

                int kontenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                    ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;

                if (kontenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
                {
                    #region コンテナ個体管理
                    // コンテナ個体管理
                    layoutName = "LAYOUT2";
                    var contenaData = dao.GetContenaData(dto);

                    #region 印刷データ定義 - Detail
                    // コンテナ種類CD
                    printDetailData.Columns.Add("PHY_CONTENA_SHURUI_CD_FLB");
                    // コンテナ種類名
                    printDetailData.Columns.Add("PHY_CONTENA_SHURUI_NAME_FLB");
                    // コンテナCD
                    printDetailData.Columns.Add("PHY_CONTENA_CD_FLB");
                    // コンテナ名
                    printDetailData.Columns.Add("PHY_CONTENA_NAME_FLB");
                    #endregion

                    #region 印刷データセット - Detail
                    foreach (var contena in contenaData)
                    {
                        DataRow printDetailRow = printDetailData.NewRow();
                        printDetailRow["PHY_CONTENA_SHURUI_CD_FLB"] = contena.CONTENA_SHURUI_CD;
                        printDetailRow["PHY_CONTENA_SHURUI_NAME_FLB"] = contena.CONTENA_SHURUI_NAME_RYAKU;
                        printDetailRow["PHY_CONTENA_CD_FLB"] = contena.CONTENA_CD;
                        printDetailRow["PHY_CONTENA_NAME_FLB"] = contena.CONTENA_NAME_RYAKU;
                        printDetailData.Rows.Add(printDetailRow);
                    }
                    #endregion

                    #endregion
                }
                else
                {
                    #region コンテナ数量管理
                    // コンテナ数量管理
                    layoutName = "LAYOUT1";
                    var reserveAndResultData = dao.GetContenaReserveAndResultData(dto);

                    // コンテナ種類CD毎に集計
                    var contenaShuruiDao = DaoInitUtility.GetComponent<IM_CONTENA_SHURUIDao>();
                    var allContenaShurui = contenaShuruiDao.GetAllValidData(new M_CONTENA_SHURUI());

                    #region 印刷データ定義 - Detail
                    // コンテナ種類CD
                    printDetailData.Columns.Add("PHY_CONTENA_SHURUI_CD_FLB");
                    // コンテナ種類名
                    printDetailData.Columns.Add("PHY_CONTENA_SHURUI_NAME_FLB");
                    // 残数量
                    printDetailData.Columns.Add("PHY_CONTENA_SHURUI_ZAN_SUURYOU_FLB");
                    #endregion

                    var targetContenaShuruiCds = allContenaShurui.Where(w =>
                                                w.CONTENA_SHURUI_CD.CompareTo(dto.contenaChuruiCdStart) >= 0
                                                && w.CONTENA_SHURUI_CD.CompareTo(dto.contenaChuruiCdEnd) <= 0
                                            ).Select(s => new { s.CONTENA_SHURUI_CD }).GroupBy(g => new { g.CONTENA_SHURUI_CD });

                    foreach (var targetShuruiCd in targetContenaShuruiCds)
                    {
                        int shoyuuDaisuu = 0;
                        var contenaShuruiMaserData
                            = allContenaShurui.Where(w => w.CONTENA_SHURUI_CD.ToString().Equals(targetShuruiCd.Key.CONTENA_SHURUI_CD)).FirstOrDefault();

                        // マスタデータが存在しない場合は表示しない。
                        // SQLのWHERE句でチェックすると時間が掛かりそうなのでLogicClassでチェック
                        if (contenaShuruiMaserData == null
                            || contenaShuruiMaserData.DELETE_FLG.IsTrue)
                        {
                            continue;
                        }

                        if (!contenaShuruiMaserData.SHOYUU_DAISUU.IsNull)
                        {
                            shoyuuDaisuu = (int)contenaShuruiMaserData.SHOYUU_DAISUU;
                        }

                        // コンテナ数量の計算
                        var targetContenaData
                            = reserveAndResultData.Where(w => w.CONTENA_SHURUI_CD.ToString().Equals(targetShuruiCd.Key.CONTENA_SHURUI_CD));

                        foreach (var tempContenaData in targetContenaData)
                        {
                            if (tempContenaData == null
                                || tempContenaData.DAISUU_CNT.IsNull
                                || tempContenaData.CONTENA_SET_KBN.IsNull)
                            {
                                continue;
                            }

                            if (tempContenaData.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                            {
                                // 減算
                                shoyuuDaisuu -= (int)tempContenaData.DAISUU_CNT;
                            }
                            else if (tempContenaData.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                            {
                                // 加算
                                shoyuuDaisuu += (int)tempContenaData.DAISUU_CNT;
                            }
                        }

                        // 印刷データ(Detail)一行分作成
                        DataRow printDetailRow = printDetailData.NewRow();
                        printDetailRow["PHY_CONTENA_SHURUI_CD_FLB"] = contenaShuruiMaserData.CONTENA_SHURUI_CD;
                        printDetailRow["PHY_CONTENA_SHURUI_NAME_FLB"] = contenaShuruiMaserData.CONTENA_SHURUI_NAME;
                        printDetailRow["PHY_CONTENA_SHURUI_ZAN_SUURYOU_FLB"] = shoyuuDaisuu;
                        printDetailData.Rows.Add(printDetailRow);
                    }
                    #endregion
                }

                #region 印刷データ定義 - Header
                // 自社名
                printHeaderData.Columns.Add("FH_CORP_RYAKU_NAME_VLB");
                // 発行日(年月日時分秒)
                printHeaderData.Columns.Add("FH_PRINT_DATE_VLB");
                // 検索コンテナ種類CD From
                printHeaderData.Columns.Add("FH_CONTENA_SHURUI_CD_START_CTL");
                // 検索コンテナ種類名 From
                printHeaderData.Columns.Add("FH_CONTENA_SHURUI_NAME_START_CTL");
                // 検索コンテナ種類CD To
                printHeaderData.Columns.Add("FH_CONTENA_SHURUI_CD_END_CTL");
                // 検索コンテナ種類名 From
                printHeaderData.Columns.Add("FH_CONTENA_SHURUI_NAME_END_CTL");
                #endregion

                /* 印刷データ(Header)作成 */
                DataRow printHeaderRow = printHeaderData.NewRow();
                // 自社情報
                printHeaderRow["FH_CORP_RYAKU_NAME_VLB"] = GetCorpName();
                // 発効日
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //DateTime dt = DateTime.Now;
                DateTime dt = this.getDBDateTime();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                string dateTime = dt.ToString("yyyy/MM/dd HH:mm:ss");
                printHeaderRow["FH_PRINT_DATE_VLB"] = dateTime;
                // 検索コンテナ種類CD From
                printHeaderRow["FH_CONTENA_SHURUI_CD_START_CTL"] = this.form.ContenaShuruiCDStart.Text;
                // 検索コンテナ種類名 From
                printHeaderRow["FH_CONTENA_SHURUI_NAME_START_CTL"] = this.form.ContenaShuruiMeiStart.Text;
                // 検索コンテナ種類CD To
                printHeaderRow["FH_CONTENA_SHURUI_CD_END_CTL"] = this.form.ContenaShuruiCDEnd.Text;
                // 検索コンテナ種類名 From
                printHeaderRow["FH_CONTENA_SHURUI_NAME_END_CTL"] = this.form.ContenaShuruiMeiEnd.Text;
                printHeaderData.Rows.Add(printHeaderRow);

                // プリント
                ReportInfoR594 reportInfo = new ReportInfoR594(printHeaderData, printDetailData, layoutName);
                reportInfo.R594_Reprt();
                reportInfo.Title = "待機コンテナ一覧表";

                FormReportPrintPopup report = new FormReportPrintPopup(reportInfo, "R594");
                report.PrintInitAction = 2;
                report.PrintXPS();
                report.Dispose();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = -1;
            }
            return returnVal;
        }

        /// <summary>
        /// 自社情報 - 自社名を取得します
        /// </summary>
        /// <returns>自社名</returns>
        private string GetCorpName()
        {
            IM_CORP_INFODao dao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            M_CORP_INFO searchCorpInfo = new M_CORP_INFO();
            M_CORP_INFO[] corpInfo;
            corpInfo = (M_CORP_INFO[])dao.GetAllData();
            return corpInfo[0].CORP_NAME;
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141127 teikyou ダブルクリックを追加する　start
        private void ContenaShuruiCDEnd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var contenaShuruiCDStartTextBox = this.form.ContenaShuruiCDStart;
            var contenaShuruiCDEndTextBox = this.form.ContenaShuruiCDEnd;
            var ContenaShuruiMeiStartTextBox = this.form.ContenaShuruiMeiStart;
            var ContenaShuruiMeiEndTextBox = this.form.ContenaShuruiMeiEnd;
            contenaShuruiCDEndTextBox.Text = contenaShuruiCDStartTextBox.Text;
            ContenaShuruiMeiEndTextBox.Text = ContenaShuruiMeiStartTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141127 teikyou ダブルクリックを追加する　end
        #endregion

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
    }
}
