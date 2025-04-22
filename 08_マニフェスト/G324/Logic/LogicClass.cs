using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using GrapeCity.Win.MultiRow;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using System.Text.RegularExpressions;
using Shougun.Core.Common.BusinessCommon.Accessor;

namespace Shougun.Core.PaperManifest.HensoSakiAnnaisho
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.HensoSakiAnnaisho.Setting.ButtonSetting.xml";

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeader headForm;

        /// <summary>
        /// DAOClass
        /// </summary>
        private DAOClass mDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 拠点情報
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        ///
        /// </summary>
        private MessageBoxShowLogic messageShowLogic;

        /// <summary>
        /// 帳票情報データ
        /// </summary>
        private DataTable mReportInfoData;

        /// <summary>
        /// 帳票情報Header部データ
        /// </summary>
        private DataTable mReportHeaderInfo;

        /// <summary>
        ///  帳票情報Detail部データ
        /// </summary>
        private DataTable mReportDetailInfo;

        /// <summary>
        ///  帳票情報Footer部データ
        /// </summary>
        private DataTable mReportFooterInfo;

        /// <summary>
        /// 検索条件 DTO
        /// </summary>
        public DTOClass searchDtoCondition { get; set; }

        /// <summary>
        /// 総データ検索結果
        /// </summary>
        public DataTable searchAllIchiranDetailData { get; set; }

        /// <summary>
        /// 返却先について画面表示検索結果
        /// </summary>
        public DataTable searchIchiranDetailData { get; set; }

        /// <summary>
        /// 交付番号毎返却先について画面表示検索結果
        /// </summary>
        public DataTable searchKohuBangoDataByTorisaki { get; set; }

        /// <summary>
        /// 現場毎返送先について画面表示検索結果
        /// </summary>
        public DataTable searchGenbaDataByTorisaki { get; set; }

        /// <summary>
        /// 廃棄物種類検索結果
        /// </summary>
        public DataTable searchHaikiShuruiData { get; set; }

        /// <summary>
        /// 返却先につて現場マスタ（A～E票）使用可否情報データ検索結果
        /// </summary>
        public DataTable searchGenbaUseInfoData { get; set; }

        /// <summary>
        /// 返却先集計データ検索結果
        /// </summary>
        public DataTable searchHenkyakusakiShukeiData { get; set; }

        /// <summary>
        /// 帳票情報を保持するプロパティ
        /// </summary>
        public ReportInfoR407 ReportInfo { get; set; }

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        public M_SYS_INFO sysInfoEntity { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// 拠点情報
        /// </summary>
        public M_KYOTEN mKyotenInfo { get; set; }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region 画面初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.mDao = DaoInitUtility.GetComponent<DAOClass>(); //DAO
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();//システム情報のDao
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>(); //拠点情報のDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            messageShowLogic = new MessageBoxShowLogic();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダー項目
                this.headForm = (UIHeader)((BusinessBaseForm)this.form.ParentForm).headerForm;
                // システム情報を取得
                this.GetSysInfoInit();
                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);
                // イベントの初期化処理
                this.EventInit(parentForm);
                //画面初期化処理
                this.DisplyInit();
                //拠点情報取得
                //155770 S
                this.mKyotenInfo = this.GetKyotenProfileValue();
                this.SetKyoten();
                //155770 E
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    //// 数量フォーマット
                    //this.systemSuuryouFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT, string.Empty).ToString();
                    if (this.sysInfoEntity != null)
                    {
                        //システム情報からアラート件数を取得
                        this.alertCount = (int)this.sysInfoEntity.ICHIRAN_ALERT_KENSUU;
                        this.headForm.alertNumber.Text = this.alertCount.ToString();
                    }
                }
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

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private M_KYOTEN GetKyotenProfileValue()
        {
            try
            {
                LogUtility.DebugMethodStart();
                Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile profile = Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile.Load();
                string result = string.Empty;

                foreach (Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
                {
                    if (item.Name.Equals("拠点CD"))
                    {
                        result = item.Value;
                    }
                }
                //拠点情報
                M_KYOTEN kyotenP = null;
                if (!string.IsNullOrEmpty(result))
                {
                    kyotenP = kyotenDao.GetDataByCd(result);
                }
                return kyotenP;
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

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        ///<summary>
        ///イベントの初期化処理
        ///</summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 前ボタン(F1)イベント
                parentForm.bt_func1.Click += new EventHandler(this.form.previousNumber_Click);

                // 後ボタン(F2)イベント
                parentForm.bt_func2.Click += new EventHandler(this.form.nextNumber_Click);

                // 印刷ボタン(F5)イベント
                parentForm.bt_func5.Click += new EventHandler(this.form.Print);

                // CSV出力ボタン(F6)イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

                // 検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Search);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new System.EventHandler(this.form.FormClose);
                // parentForm.ProcessButtonPanel.Visible = false;
                parentForm.txb_process.Enabled = false;

                // 20141128 Houkakou 「運賃集計表」のダブルクリックを追加する start
                // 「To」のイベント生成
                this.form.txtHinnkyakuDateTO.MouseDoubleClick += new MouseEventHandler(txtHinnkyakuDateTO_MouseDoubleClick);
                this.form.txtHaishutsuJigyoushaCdTO.MouseDoubleClick += new MouseEventHandler(txtHaishutsuJigyoushaCdTO_MouseDoubleClick);
                this.form.txtHaishutsuGenbaCdTO.MouseDoubleClick += new MouseEventHandler(txtHaishutsuGenbaCdTO_MouseDoubleClick);
                this.form.txtTORIHIKISAKI_CD_TO.MouseDoubleClick += new MouseEventHandler(txtTORIHIKISAKI_CD_TO_MouseDoubleClick);
                this.form.txtGYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(txtGYOUSHA_CD_TO_MouseDoubleClick);
                this.form.txtGENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(txtGENBA_CD_TO_MouseDoubleClick);
                // 20141128 Houkakou 「運賃集計表」のダブルクリックを追加する end
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

        ///<summary>
        ///画面初期化処理
        ///</summary>
        private void DisplyInit()
        {
            //印刷区分
            this.form.txt_InsatuKubunCD.Text = "1";
            //出力内容
            this.form.txt_ShuturyokuNaiyoCD.Text = "1";

            // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(帳票タイプ) STR
            this.form.ChecktHaishutsuGenba();
            this.form.CheckGenba();
            // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(帳票タイプ) END
        }

        #endregion

        #region [F1]前を取得

        /// <summary>
        /// 前の番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="ManiHensousakiNameValue">番号</param>
        /// <returns>前の番号</returns>
        internal void GetPreviousNumber()
        {
            String returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart();
                var dtIchiranData = this.searchAllIchiranDetailData;
                if (dtIchiranData == null)
                {
                    return;
                }
                int count = int.Parse(this.form.txtManiHensousakiCount.Tag.ToString()) - 1;

                if (count < 0)
                {
                    return;
                }
                //返却先情報設定
                string maniHensousakiKbnName;
                this.SetManiHensousakiName(count, out maniHensousakiKbnName);

                string condition = string.Empty;
                if (string.IsNullOrEmpty(this.form.txtManiHensousakiName.Tag.ToString()))
                {
                    condition = "MANI_HENSOUSAKI_NAME IS NULL OR  MANI_HENSOUSAKI_NAME = ''";
                }
                else
                {
                    string hensousakiName = this.form.txtManiHensousakiName.Tag.ToString().Replace("'", "''");
                    condition = "MANI_HENSOUSAKI_NAME  = '" + hensousakiName + "'";
                }

                if (!string.IsNullOrEmpty(maniHensousakiKbnName))
                {
                    condition += " AND (MANI_HENSOUSAKI_NAME_KBN ='" + maniHensousakiKbnName + "')";
                }

                //検索データ取得
                this.searchIchiranDetailData = dtIchiranData.Select(condition).CopyToDataTable();
                //Headarのアラート件数を処理する。
                // 20140623 kayo 不具合#4972 アラート件数判断不正 start
                //DialogResult result = DialogResult.Yes;
                //string strAlertCount = this.headForm.alertNumber.Text.ToString().Replace(",", "");
                //if (!string.IsNullOrEmpty(strAlertCount) && !strAlertCount.Equals("0") && int.Parse(strAlertCount) < this.searchIchiranDetailData.Rows.Count)
                //{
                //    //検索件数がアラート件数を超えました。<br>表示を行いますか？
                //    result = this.messageShowLogic.MessageBoxShow("C025");
                //}
                //if (result != DialogResult.Yes)
                //{
                //    return ;
                //}
                ////交付番号毎検索結果を画面に設定
                //this.SetDataForKohuBangoGRD(this.searchIchiranDetailData);
                //// 現場検索結果を画面に設定
                //this.SetDataForGenbaGRD(this.searchIchiranDetailData);

                //交付番号毎検索結果を整理
                this.searchKohuBangoDataByTorisaki = this.GetDataForKohuBangoDT(this.searchIchiranDetailData);
                // 現場検索結果を整理
                this.searchGenbaDataByTorisaki = this.GetDataForGenbaDT(this.searchIchiranDetailData);

                if (this.form.txt_ShuturyokuNaiyoCD.Text == "1")
                {
                    //交付番号毎検索結果を画面に設定
                    if (!this.SetDataForKohuBangoGRD()) { return; }
                }
                else
                {
                    //交付番号毎検索結果を画面に設定
                    if (!this.SetDataForGenbaGRD()) { return; }
                }
                // 20140623 kayo 不具合#4972 アラート件数判断不正 end
                //今取引先Index保存
                this.form.txtManiHensousakiCount.Tag = count.ToString();
                //今取引先第何件数設定
                this.form.txtManiHensousakiCount.Text = (count + 1).ToString();
                if (this.form.KohuBangoGRD.Visible)
                {
                    //読込データ件数設定
                    this.headForm.readDataNumber.Text = this.form.KohuBangoGRD.Rows.Count == 0 ? "0" : this.form.KohuBangoGRD.RowCount.ToString("#,###"); //this.searchIchiranDetailData.Rows.Count.ToString("#,###");
                }
                else
                {
                    //読込データ件数設定
                    this.headForm.readDataNumber.Text = this.form.GenbaGRD.Rows.Count == 0 ? "0" : this.form.GenbaGRD.RowCount.ToString("#,###");
                }
                return;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousNumber", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F2]次を取得

        /// <summary>
        /// 次の番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">番号</param>
        /// <returns>次の番号</returns>
        internal void GetNextNumber()
        {
            String returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart();
                var dtIchiranData = this.searchAllIchiranDetailData;
                if (dtIchiranData == null)
                {
                    return;
                }
                int count = int.Parse(this.form.txtManiHensousakiCount.Tag.ToString()) + 1;
                if (count >= int.Parse(this.form.txtAllManiHensousakiCount.Tag.ToString()))
                {
                    return;
                }
                //返却先情報設定
                string maniHensousakiKbnName;
                this.SetManiHensousakiName(count, out maniHensousakiKbnName);

                //検索条件設定
                string condition = string.Empty;
                if (string.IsNullOrEmpty(this.form.txtManiHensousakiName.Tag.ToString()))
                {
                    condition = "MANI_HENSOUSAKI_NAME IS NULL OR  MANI_HENSOUSAKI_NAME = ''";
                }
                else
                {
                    string hensousakiName = this.form.txtManiHensousakiName.Tag.ToString().Replace("'", "''");
                    condition = "MANI_HENSOUSAKI_NAME  = '" + hensousakiName + "'";
                }

                if (!string.IsNullOrEmpty(maniHensousakiKbnName))
                {
                    condition += " AND (MANI_HENSOUSAKI_NAME_KBN ='" + maniHensousakiKbnName + "')";
                }

                //検索データ取得
                this.searchIchiranDetailData = dtIchiranData.Select(condition).CopyToDataTable();
                //Headarのアラート件数を処理する。
                // 20140623 kayo 不具合#4972 アラート件数判断不正 start
                //DialogResult result = DialogResult.Yes;
                //string strAlertCount = this.headForm.alertNumber.Text.ToString().Replace(",", "");
                //if (!string.IsNullOrEmpty(strAlertCount) && !strAlertCount.Equals("0") && int.Parse(strAlertCount) < this.searchIchiranDetailData.Rows.Count)
                //{
                //    //検索件数がアラート件数を超えました。<br>表示を行いますか？
                //    result = this.messageShowLogic.MessageBoxShow("C025");
                //}
                //if (result != DialogResult.Yes)
                //{
                //    return ;
                //}
                ////交付番号毎検索結果を画面に設定
                //this.SetDataForKohuBangoGRD(this.searchIchiranDetailData);
                //// 現場検索結果を画面に設定
                //this.SetDataForGenbaGRD(this.searchIchiranDetailData);

                //交付番号毎検索結果を整理
                this.searchKohuBangoDataByTorisaki = this.GetDataForKohuBangoDT(this.searchIchiranDetailData);
                // 現場検索結果を整理
                this.searchGenbaDataByTorisaki = this.GetDataForGenbaDT(this.searchIchiranDetailData);

                if (this.form.txt_ShuturyokuNaiyoCD.Text == "1")
                {
                    //交付番号毎検索結果を画面に設定
                    if (!this.SetDataForKohuBangoGRD()) { return; }
                }
                else
                {
                    //交付番号毎検索結果を画面に設定
                    if (!this.SetDataForGenbaGRD()) { return; }
                }
                // 20140623 kayo 不具合#4972 アラート件数判断不正 end
                //今取引先Index保存
                this.form.txtManiHensousakiCount.Tag = count.ToString();
                //今取引先第何件数設定
                this.form.txtManiHensousakiCount.Text = (count + 1).ToString();
                if (this.form.KohuBangoGRD.Visible)
                {
                    //読込データ件数設定
                    this.headForm.readDataNumber.Text = this.form.KohuBangoGRD.Rows.Count == 0 ? "0" : this.form.KohuBangoGRD.RowCount.ToString("#,###"); //this.searchIchiranDetailData.Rows.Count.ToString("#,###");
                }
                else
                {
                    //読込データ件数設定
                    this.headForm.readDataNumber.Text = this.form.GenbaGRD.Rows.Count == 0 ? "0" : this.form.GenbaGRD.RowCount.ToString("#,###");
                }

                return;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNumber", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F5]印刷

        /// <summary>
        /// [F5]印刷 処理
        /// </summary>
        internal void Print()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 画面表示名称（種類)
                string layoutName = string.Empty;

                //レポートＩＮＦＯ（DataTableList）作成
                this.ReportInfo = new ReportInfoR407(WINDOW_ID.R_HENSOU_ANNAISYO);

                // 1.直行用
                if (this.form.txt_InsatuKubunCD.Text.Equals("1"))
                {
                    //this.ReportInfo.InsatuKbn = ReportInfoR407.InsatuKbnDef.Tyokkoyo;
                    // 画面種類
                    //交付番号毎
                    if (this.form.txt_ShuturyokuNaiyoCD.Text.Equals("1"))
                    {
                        //this.ReportInfo.OutputType = ReportInfoR407.OutputTypeDef.KohuBango;
                        layoutName = "LAYOUT2";
                        if (this.form.KohuBangoGRD.Rows.Count <= 0)
                        {
                            //読込データ件数を0にする
                            messageShowLogic.MessageBoxShow("W002", "マニフェスト返送案内");
                            return;
                        }
                    }//現場毎
                    else if (this.form.txt_ShuturyokuNaiyoCD.Text.Equals("2"))
                    {
                        //this.ReportInfo.OutputType = ReportInfoR407.OutputTypeDef.Genba;
                        layoutName = "LAYOUT1";
                        if (this.form.GenbaGRD.Rows.Count <= 0)
                        {
                            //読込データ件数を0にする
                            messageShowLogic.MessageBoxShow("W002", "マニフェスト返送案内");
                            return;
                        }
                    }//返却先集計非表示
                    else if (this.form.txt_ShuturyokuNaiyoCD.Text.Equals("3"))
                    {
                        //this.ReportInfo.OutputType = ReportInfoR407.OutputTypeDef.HenkyakusakiShukei;
                        layoutName = "LAYOUT3";
                        if (this.form.HenkyakusakiShukeiGRD.Rows.Count <= 0)
                        {
                            //読込データ件数を0にする
                            messageShowLogic.MessageBoxShow("W002", "マニフェスト返送案内");
                            return;
                        }
                    }
                } // 2.積替用
                else if (this.form.txt_InsatuKubunCD.Text.Equals("2"))
                {
                    //this.ReportInfo.InsatuKbn = ReportInfoR407.InsatuKbnDef.Tumikaeyo;
                    // 画面種類
                    //交付番号毎
                    if (this.form.txt_ShuturyokuNaiyoCD.Text.Equals("1"))
                    {
                        //this.ReportInfo.OutputType = ReportInfoR407.OutputTypeDef.KohuBango;
                        layoutName = "LAYOUT5";
                        if (this.form.KohuBangoGRD.Rows.Count <= 0)
                        {
                            //読込データ件数を0にする
                            messageShowLogic.MessageBoxShow("W002", "マニフェスト返送案内");
                            return;
                        }
                    }//現場毎
                    else if (this.form.txt_ShuturyokuNaiyoCD.Text.Equals("2"))
                    {
                        //this.ReportInfo.OutputType = ReportInfoR407.OutputTypeDef.Genba;
                        layoutName = "LAYOUT4";
                        if (this.form.GenbaGRD.Rows.Count <= 0)
                        {
                            //読込データ件数を0にする
                            messageShowLogic.MessageBoxShow("W002", "マニフェスト返送案内");
                            return;
                        }
                    }//返却先集計非表示
                    else if (this.form.txt_ShuturyokuNaiyoCD.Text.Equals("3"))
                    {
                        //this.ReportInfo.OutputType = ReportInfoR407.OutputTypeDef.HenkyakusakiShukei;
                        layoutName = "LAYOUT6";
                        if (this.form.HenkyakusakiShukeiGRD.Rows.Count <= 0)
                        {
                            //読込データ件数を0にする
                            messageShowLogic.MessageBoxShow("W002", "マニフェスト返送案内");
                            return;
                        }
                    }
                }

                this.mKyotenInfo = this.kyotenDao.GetDataByCd(this.headForm.HAKKOU_KYOTEN_CD.Text);

                if (this.form.txt_ShuturyokuNaiyoCD.Text == "3")
                {
                    //帳票Header情報取得
                    this.CreateReportHeaderInfo();
                    //帳票詳細情報取得
                    this.CreateDetailDataTable();
                    //帳票Footer情報取得
                    this.CreateFooterDataTable();

                    this.ReportInfo.Create(@".\Template\R407-Form.xml", layoutName, new DataTable());
                    using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(this.ReportInfo, WINDOW_ID.R_HENSOU_ANNAISYO))
                    {

                        //レポートタイトルの設定
                        formReportPrintPopup.ReportCaption = "返送案内書";

                        formReportPrintPopup.ShowDialog();
                        formReportPrintPopup.Dispose();
                    }
                }
                else
                {
                    List<ReportInfoR407> reportInfoList = new List<ReportInfoR407>();

                    var ManiHensousakiNameInfo = this.searchAllIchiranDetailData.AsEnumerable().GroupBy(r => string.Format("{0},{1}", r.Field<string>("MANI_HENSOUSAKI_NAME_KBN"), r.Field<string>("MANI_HENSOUSAKI_NAME")),
                      (k, g) => new
                      {
                          MANI_HENSOUSAKI_NAME_KBN = g.First().Field<string>("MANI_HENSOUSAKI_NAME_KBN"),
                          MANI_HENSOUSAKI_NAME = g.First().Field<string>("MANI_HENSOUSAKI_NAME"),
                      }).ToList();

                    for (int i = 0; i < ManiHensousakiNameInfo.Count; i++)
                    {
                        //検索条件設定
                        string condition = string.Empty;
                        if (string.IsNullOrEmpty(this.form.txtManiHensousakiName.Tag.ToString()))
                        {
                            condition = "MANI_HENSOUSAKI_NAME IS NULL OR  MANI_HENSOUSAKI_NAME = ''";
                        }
                        else
                        {
                            string hensousakiName = ManiHensousakiNameInfo[i].MANI_HENSOUSAKI_NAME.Replace("'", "''");
                            condition = "MANI_HENSOUSAKI_NAME  = '" + hensousakiName + "'";
                        }

                        if (!string.IsNullOrEmpty(ManiHensousakiNameInfo[i].MANI_HENSOUSAKI_NAME_KBN))
                        {
                            condition += " AND (MANI_HENSOUSAKI_NAME_KBN ='" + ManiHensousakiNameInfo[i].MANI_HENSOUSAKI_NAME_KBN + "')";
                        }

                        DataTable tempIchiranDetailData = this.searchAllIchiranDetailData.Select(condition).CopyToDataTable();
                        ReportInfoR407 tempReportInfo = new ReportInfoR407(WINDOW_ID.R_HENSOU_ANNAISYO);
                        // ヘーダデータ作成
                        tempReportInfo.DataTableList.Add("Header", this.CreateReportHeaderInfoD(tempIchiranDetailData));

                        // 明細一覧作成
                        tempReportInfo.DataTableList.Add("Detail", this.CreateDetailDataTableD(tempIchiranDetailData));

                        // フッタデータ作成
                        tempReportInfo.DataTableList.Add("Footer", this.CreateFooterDataTableD(tempIchiranDetailData));

                        tempReportInfo.Create(@".\Template\R407-Form.xml", layoutName, new DataTable());

                        reportInfoList.Add(tempReportInfo);
                    }

                    using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(reportInfoList.ToArray(), WINDOW_ID.R_HENSOU_ANNAISYO))
                    {

                        //レポートタイトルの設定
                        formReportPrintPopup.ReportCaption = "返送案内書";

                        formReportPrintPopup.ShowDialog();
                        formReportPrintPopup.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 帳票情報取得

        /// <summary>帳票Header情報取得</summary>
        public void CreateReportHeaderInfo()
        {
            #region - Header -

            DataTable dataTableTmp;
            DataRow rowTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Header";
            // 発行日時
            dataTableTmp.Columns.Add("PRINT_DATE");
            //①－１
            //返送先-名称
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_NAME_REPORT");
            //返送先ー郵便番号
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_POST");
            //返送先ー住所1
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_ADDRESS1");
            //返送先ー住所2
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_ADDRESS2");
            //返送先-名称1
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_NAME1_REPORT");
            //返送先-敬称1
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_KEISHOU1");
            //返送先-名称2
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_NAME2");
            //返送先-敬称2
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_KEISHOU2");
            //返送先-部署
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_BUSHO");
            //返送先ーマニフェスト担当
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_TANTOU");

            //①－２
            //連絡先1
            dataTableTmp.Columns.Add("MANI_RENRAKUSAKI1");
            //連絡先2
            dataTableTmp.Columns.Add("MANI_RENRAKUSAKI2");

            //①－３
            //返送元-名称
            dataTableTmp.Columns.Add("MANI_HENSOUMODO_NAME");
            //返送元ー郵便番号
            dataTableTmp.Columns.Add("KYOTEN_POST");
            //返送元ー住所1
            dataTableTmp.Columns.Add("KYOTEN_ADDRESS1");
            //返送元ー住所2
            dataTableTmp.Columns.Add("KYOTEN_ADDRESS2");
            //返送元ー電話番号
            dataTableTmp.Columns.Add("KYOTEN_TEL");
            //返送元ーFAX番号
            dataTableTmp.Columns.Add("KYOTEN_FAX");
            //①－４
            //捺印タイトル1
            dataTableTmp.Columns.Add("BLANK1");
            //捺印タイトル2
            dataTableTmp.Columns.Add("BLANK2");

            rowTmp = dataTableTmp.NewRow();
            //if (this.searchIchiranDetailData != null && this.searchIchiranDetailData.Rows.Count > 0)
            //{
            //    DataRow dtRow = this.searchIchiranDetailData.Rows[0];
            //    // 発行日時
            //    rowTmp["PRINT_DATE"] = "発行日 " + DateTime.Today.ToShortDateString();
            //    //①－１
            //    //返送先-名称
            //    rowTmp["MANI_HENSOUSAKI_NAME"] = dtRow["MANI_HENSOUSAKI_NAME"].ToString();
            //    //返送先ー郵便番号
            //    rowTmp["MANI_HENSOUSAKI_POST"] = dtRow["MANI_HENSOUSAKI_POST"].ToString();
            //    //返送先ー住所1
            //    rowTmp["MANI_HENSOUSAKI_ADDRESS1"] = dtRow["MANI_HENSOUSAKI_ADDRESS1"].ToString();
            //    //返送先ー住所2
            //    rowTmp["MANI_HENSOUSAKI_ADDRESS2"] = dtRow["MANI_HENSOUSAKI_ADDRESS2"].ToString();
            //    //返送先-名称1
            //    rowTmp["MANI_HENSOUSAKI_NAME1"] = dtRow["MANI_HENSOUSAKI_NAME1"].ToString();
            //    //返送先-敬称1
            //    rowTmp["MANI_HENSOUSAKI_KEISHOU1"] = dtRow["MANI_HENSOUSAKI_KEISHOU1"].ToString();
            //    //返送先-名称2
            //    rowTmp["MANI_HENSOUSAKI_NAME2"] = dtRow["MANI_HENSOUSAKI_NAME2"].ToString();
            //    //返送先-敬称2
            //    rowTmp["MANI_HENSOUSAKI_KEISHOU2"] = dtRow["MANI_HENSOUSAKI_KEISHOU2"].ToString();
            //    //返送先-部署
            //    rowTmp["MANI_HENSOUSAKI_BUSHO"] = dtRow["MANI_HENSOUSAKI_BUSHO"].ToString();
            //    //返送先ーマニフェスト担当
            //    rowTmp["MANI_HENSOUSAKI_TANTOU"] = dtRow["MANI_HENSOUSAKI_TANTOU"].ToString();

            //    //①－２
            //    //連絡先1
            //    rowTmp["MANI_RENRAKUSAKI1"] = this.sysInfoEntity.MANIFEST_HENSOU_RENRAKU_1;
            //    //連絡先2
            //    rowTmp["MANI_RENRAKUSAKI2"] = this.sysInfoEntity.MANIFEST_HENSOU_RENRAKU_2;
            //    //①－３
            //    // 会社略称情報
            //    DataTable dataTable = mDao.GetDateForStringSql("SELECT M_CORP_INFO.CORP_NAME from M_CORP_INFO");
            //    if (dataTable != null && dataTable.Rows.Count > 0)
            //    {
            //        //返送元-名称
            //        rowTmp["MANI_HENSOUMODO_NAME"] = (string)dataTable.Rows[0].ItemArray[0];
            //    }

            //    //拠点名称
            //    if (this.mKyotenInfo != null)
            //    {
            //        //返送元ー郵便番号
            //        rowTmp["KYOTEN_POST"] = this.mKyotenInfo.KYOTEN_POST;
            //        //返送元ー住所1
            //        rowTmp["KYOTEN_ADDRESS1"] = this.mKyotenInfo.KYOTEN_ADDRESS1;
            //        //返送元ー住所2
            //        rowTmp["KYOTEN_ADDRESS2"] = this.mKyotenInfo.KYOTEN_ADDRESS2;
            //        //返送元ー電話番号
            //        rowTmp["KYOTEN_TEL"] = this.mKyotenInfo.KYOTEN_TEL;
            //        //返送元ーFAX番号
            //        rowTmp["KYOTEN_FAX"] = this.mKyotenInfo.KYOTEN_FAX;
            //    }

            //    //①－４
            //    //捺印タイトル1
            //    rowTmp["BLANK1"] = this.sysInfoEntity.MANIFEST_HENSOU_NATSUIN_1;
            //    //捺印タイトル2
            //    rowTmp["BLANK2"] = this.sysInfoEntity.MANIFEST_HENSOU_NATSUIN_2;

            //    dataTableTmp.Rows.Add(rowTmp);

            //}
            //else
            //{
            // 発行日時
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //rowTmp["PRINT_DATE"] = "発行日 " + DateTime.Today.ToShortDateString();
            rowTmp["PRINT_DATE"] = "発行日 " + this.getDBDateTime().Date.ToShortDateString();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            dataTableTmp.Rows.Add(rowTmp);
            //}
            mReportHeaderInfo = dataTableTmp;

            // データを設定する処理を入れる
            this.ReportInfo.DataTableList.Add("Header", this.mReportHeaderInfo);

            #endregion - Header -
        }

        /// <summary>帳票詳細情報取得 </summary>
        public void CreateDetailDataTable()
        {
            string ShuturyokuNaiyo = this.form.txt_ShuturyokuNaiyoCD.Text;
            #region - Detail -

            DataTable dataTableTmp;
            DataRow rowTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";
            //②－１
            //行番号
            dataTableTmp.Columns.Add(ConstCls.ReportColName.ROW_NO);
            //排出事業者1
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HST_GYOUSHA_CD1);
            //排出事業者2
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HST_GYOUSHA_CD2);
            //排出事業場1
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HST_GENBA_CD1);
            //排出事業場2
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HST_GENBA_CD2);
            //廃棄物種類
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HAIKI_SHURUI_CD);
            //交付年月日
            dataTableTmp.Columns.Add(ConstCls.ReportColName.KOUFU_DATE);
            //交付番号
            dataTableTmp.Columns.Add(ConstCls.ReportColName.MANIFEST_ID);
            //返日
            dataTableTmp.Columns.Add(ConstCls.ReportColName.REF_DATE);
            //返送先
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HENSOUSAKI_NAME);
            //A票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_A);
            //B1票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B1);
            //B2票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B2);
            //B4票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B4);
            //B6票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B6);
            //C1票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_C1);
            //C2票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_C2);
            //D票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_D);
            //E票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_E);

            if (ShuturyokuNaiyo.Equals("1"))
            {
                foreach (DataGridViewRow dtRow in this.form.KohuBangoGRD.Rows)
                {
                    rowTmp = dataTableTmp.NewRow();
                    //行番号
                    rowTmp[ConstCls.ReportColName.ROW_NO] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.ROW_NO].Value, string.Empty);
                    //交付年月日
                    rowTmp[ConstCls.ReportColName.KOUFU_DATE] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.KOUFU_DATE].Value, string.Empty);
                    //交付番号
                    rowTmp[ConstCls.ReportColName.MANIFEST_ID] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.MANIFEST_ID].Value, string.Empty);
                    //A票
                    rowTmp[ConstCls.ReportColName.SEND_A] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.SEND_A].Value, string.Empty);
                    //B1票
                    rowTmp[ConstCls.ReportColName.SEND_B1] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.SEND_B1].Value, string.Empty);
                    //B2票
                    rowTmp[ConstCls.ReportColName.SEND_B2] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.SEND_B2].Value, string.Empty);
                    //B4票
                    rowTmp[ConstCls.ReportColName.SEND_B4] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.SEND_B4].Value, string.Empty);
                    //B6票
                    rowTmp[ConstCls.ReportColName.SEND_B6] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.SEND_B6].Value, string.Empty);
                    //C1票
                    rowTmp[ConstCls.ReportColName.SEND_C1] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.SEND_C1].Value, string.Empty);
                    //C2票
                    rowTmp[ConstCls.ReportColName.SEND_C2] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.SEND_C2].Value, string.Empty);
                    //D票
                    rowTmp[ConstCls.ReportColName.SEND_D] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.SEND_D].Value, string.Empty);
                    //E票
                    rowTmp[ConstCls.ReportColName.SEND_E] = this.ChgDBNullToDateTimeValue(dtRow.Cells[ConstCls.KohuBangoGRDColName.SEND_E].Value, string.Empty);

                    dataTableTmp.Rows.Add(rowTmp);
                }
            }
            else if (ShuturyokuNaiyo.Equals("2"))
            {
                foreach (Row dtRow in this.form.GenbaGRD.Rows)
                {
                    rowTmp = dataTableTmp.NewRow();
                    //行番号
                    rowTmp[ConstCls.ReportColName.ROW_NO] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.ROW_NO].Value, string.Empty);
                    //排出事業者1
                    rowTmp[ConstCls.ReportColName.HST_GYOUSHA_CD1] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.HST_GYOUSHA_CD1].Value, string.Empty);
                    //排出事業者2
                    rowTmp[ConstCls.ReportColName.HST_GYOUSHA_CD2] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.HST_GYOUSHA_CD2].Value, string.Empty);
                    //排出事業場1
                    rowTmp[ConstCls.ReportColName.HST_GENBA_CD1] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.HST_GENBA_CD1].Value, string.Empty);
                    //排出事業場2
                    rowTmp[ConstCls.ReportColName.HST_GENBA_CD2] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.HST_GENBA_CD2].Value, string.Empty);
                    //廃棄物種類
                    rowTmp[ConstCls.ReportColName.HAIKI_SHURUI_CD] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.HAIKI_SHURUI_CD].Value, string.Empty);
                    //交付年月日
                    rowTmp[ConstCls.ReportColName.KOUFU_DATE] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.KOUFU_DATE].Value, string.Empty);
                    //交付番号
                    rowTmp[ConstCls.ReportColName.MANIFEST_ID] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.MANIFEST_ID].Value, string.Empty);
                    //A票
                    rowTmp[ConstCls.ReportColName.SEND_A] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.SEND_A].Value, string.Empty);
                    //B1票
                    rowTmp[ConstCls.ReportColName.SEND_B1] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.SEND_B1].Value, string.Empty);
                    //B2票
                    rowTmp[ConstCls.ReportColName.SEND_B2] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.SEND_B2].Value, string.Empty);
                    //B4票
                    rowTmp[ConstCls.ReportColName.SEND_B4] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.SEND_B4].Value, string.Empty);
                    //B6票
                    rowTmp[ConstCls.ReportColName.SEND_B6] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.SEND_B6].Value, string.Empty);
                    //C1票
                    rowTmp[ConstCls.ReportColName.SEND_C1] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.SEND_C1].Value, string.Empty);
                    //C2票
                    rowTmp[ConstCls.ReportColName.SEND_C2] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.SEND_C2].Value, string.Empty);
                    //D票
                    rowTmp[ConstCls.ReportColName.SEND_D] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.SEND_D].Value, string.Empty);
                    //E票
                    rowTmp[ConstCls.ReportColName.SEND_E] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.GenbaGRDColName.SEND_E].Value, string.Empty);

                    dataTableTmp.Rows.Add(rowTmp);
                }
            }
            else if (ShuturyokuNaiyo.Equals("3"))
            {
                foreach (DataGridViewRow dtRow in this.form.HenkyakusakiShukeiGRD.Rows)
                {
                    rowTmp = dataTableTmp.NewRow();
                    //行番号
                    rowTmp[ConstCls.ReportColName.ROW_NO] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.ROW_NO].Value, string.Empty);
                    //年月日
                    rowTmp[ConstCls.ReportColName.REF_DATE] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.REF_DATE].Value, string.Empty);
                    //返送先
                    rowTmp[ConstCls.ReportColName.HENSOUSAKI_NAME] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.HENSOUSAKI_NAME].Value, string.Empty);
                    //A票
                    rowTmp[ConstCls.ReportColName.SEND_A] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.SEND_A].Value, string.Empty);
                    //B1票
                    rowTmp[ConstCls.ReportColName.SEND_B1] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B1].Value, string.Empty);
                    //B2票
                    rowTmp[ConstCls.ReportColName.SEND_B2] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B2].Value, string.Empty);
                    //B4票
                    rowTmp[ConstCls.ReportColName.SEND_B4] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B4].Value, string.Empty);
                    //B6票
                    rowTmp[ConstCls.ReportColName.SEND_B6] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B6].Value, string.Empty);
                    //C1票
                    rowTmp[ConstCls.ReportColName.SEND_C1] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.SEND_C1].Value, string.Empty);
                    //C2票
                    rowTmp[ConstCls.ReportColName.SEND_C2] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.SEND_C2].Value, string.Empty);
                    //D票
                    rowTmp[ConstCls.ReportColName.SEND_D] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.SEND_D].Value, string.Empty);
                    //E票
                    rowTmp[ConstCls.ReportColName.SEND_E] = this.ChgDBNullToValue(dtRow.Cells[ConstCls.HenkyakusakiShukeiGRDColName.SEND_E].Value, string.Empty);

                    dataTableTmp.Rows.Add(rowTmp);
                }
            }

            this.mReportDetailInfo = dataTableTmp;
            // データを設定する処理を入れる
            this.ReportInfo.DataTableList.Add("Detail", this.mReportDetailInfo);
            #endregion - Detail -
        }

        /// <summary>/帳票Footer情報取得</summary>
        public void CreateFooterDataTable()
        {
            string ShuturyokuNaiyo = this.form.txt_ShuturyokuNaiyoCD.Text;
            #region - Footer -
            DataTable dataTableTmp;
            DataRow rowTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";
            //②－１

            //A票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_A);
            //B1票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B1);
            //B2票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B2);
            //B4票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B4);
            //B6票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B6);
            //C1票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_C1);
            //C2票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_C2);
            //D票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_D);
            //E票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_E);
            //合計
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SUM_NUM);
            rowTmp = dataTableTmp.NewRow();
            int CountA = 0;
            int CountB1 = 0;
            int CountB2 = 0;
            int CountB4 = 0;
            int CountB6 = 0;
            int CountC1 = 0;
            int CountC2 = 0;
            int CountD = 0;
            int CountE = 0;
            if (!ShuturyokuNaiyo.Equals("3"))
            {
                foreach (DataRow dt in this.searchIchiranDetailData.Rows)
                {
                    CountA = CountA + this.GetDateTimeCount(dt["SEND_A"], 0);
                    CountB1 = CountB1 + this.GetDateTimeCount(dt["SEND_B1"], 0);//;
                    CountB2 = CountB2 + this.GetDateTimeCount(dt["SEND_B2"], 0); //;
                    CountB4 = CountB4 + this.GetDateTimeCount(dt["SEND_B4"], 0);//SEND_B4;
                    CountB6 = CountB6 + this.GetDateTimeCount(dt["SEND_B6"], 0); //SEND_B6;
                    CountC1 = CountC1 + this.GetDateTimeCount(dt["SEND_C1"], 0); //SEND_C1;
                    CountC2 = CountC2 + this.GetDateTimeCount(dt["SEND_C2"], 0); //SEND_C2;
                    CountD = CountD + this.GetDateTimeCount(dt["SEND_D"], 0); //M_SEND_D;
                    CountE = CountE + this.GetDateTimeCount(dt["SEND_E"], 0); //SEND_E;
                }
            }
            else if (ShuturyokuNaiyo.Equals("3"))
            {
                // A
                CountA = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_A"));
                CountB1 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_B1"));
                CountB2 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_B2"));
                CountB4 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_B4"));
                CountB6 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_B6"));
                CountC1 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_C1"));
                CountC2 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_C2"));
                CountD = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_D"));
                CountE = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_E"));
            }
            //A票
            rowTmp[ConstCls.ReportColName.SEND_A] = CountA;
            //B1票
            rowTmp[ConstCls.ReportColName.SEND_B1] = CountB1;
            //B2票
            rowTmp[ConstCls.ReportColName.SEND_B2] = CountB2;
            //B4票
            rowTmp[ConstCls.ReportColName.SEND_B4] = CountB4;
            //B6票
            rowTmp[ConstCls.ReportColName.SEND_B6] = CountB6;
            //C1票
            rowTmp[ConstCls.ReportColName.SEND_C1] = CountC1;
            //C2票
            rowTmp[ConstCls.ReportColName.SEND_C2] = CountC2;
            //D票
            rowTmp[ConstCls.ReportColName.SEND_D] = CountD;
            //E票
            rowTmp[ConstCls.ReportColName.SEND_E] = CountE;
            if (this.form.txt_InsatuKubunCD.Text.Equals("1"))
            {
                //合計
                rowTmp[ConstCls.ReportColName.SUM_NUM] = CountA + CountB1 + CountB2 + CountC1 + CountC2 + CountD + CountE;
            }
            else
            {
                //合計
                rowTmp[ConstCls.ReportColName.SUM_NUM] = CountA + CountB2 + CountB4 + CountB6 + CountC1 + CountC2 + CountD + CountE;
            }

            dataTableTmp.Rows.Add(rowTmp);
            this.mReportFooterInfo = dataTableTmp;
            this.ReportInfo.DataTableList.Add("Footer", this.mReportFooterInfo);

            #endregion - Footer -
        }

        /// <summary>帳票Header情報取得(複数)</summary>
        public DataTable CreateReportHeaderInfoD(DataTable tempDT)
        {
            #region - Header -

            DataTable dataTableTmp;
            DataRow rowTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Header";
            // 発行日時
            dataTableTmp.Columns.Add("PRINT_DATE");
            //①－１
            //返送先-名称
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_NAME_REPORT");
            //返送先ー郵便番号
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_POST");
            //返送先ー住所1
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_ADDRESS1");
            //返送先ー住所2
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_ADDRESS2");
            //返送先-名称1
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_NAME1_REPORT");
            //返送先-敬称1
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_KEISHOU1");
            //返送先-名称2
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_NAME2");
            //返送先-敬称2
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_KEISHOU2");
            //返送先-部署
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_BUSHO");
            //返送先ーマニフェスト担当
            dataTableTmp.Columns.Add("MANI_HENSOUSAKI_TANTOU");

            //①－２
            //連絡先1
            dataTableTmp.Columns.Add("MANI_RENRAKUSAKI1");
            //連絡先2
            dataTableTmp.Columns.Add("MANI_RENRAKUSAKI2");

            //①－３
            //返送元-名称
            dataTableTmp.Columns.Add("MANI_HENSOUMODO_NAME");
            //返送元ー郵便番号
            dataTableTmp.Columns.Add("KYOTEN_POST");
            //返送元ー住所1
            dataTableTmp.Columns.Add("KYOTEN_ADDRESS1");
            //返送元ー住所2
            dataTableTmp.Columns.Add("KYOTEN_ADDRESS2");
            //返送元ー電話番号
            dataTableTmp.Columns.Add("KYOTEN_TEL");
            //返送元ーFAX番号
            dataTableTmp.Columns.Add("KYOTEN_FAX");
            //①－４
            //捺印タイトル1
            dataTableTmp.Columns.Add("BLANK1");
            //捺印タイトル2
            dataTableTmp.Columns.Add("BLANK2");

            rowTmp = dataTableTmp.NewRow();
            if (tempDT != null && tempDT.Rows.Count > 0)
            {
                DataRow dtRow = tempDT.Rows[0];
                // 発行日時
                rowTmp["PRINT_DATE"] = "発行日 " + this.getDBDateTime().Date.ToShortDateString();
                //①－１
                //返送先-名称
                rowTmp["MANI_HENSOUSAKI_NAME_REPORT"] = dtRow["MANI_HENSOUSAKI_NAME_REPORT"].ToString();
                //返送先ー郵便番号
                rowTmp["MANI_HENSOUSAKI_POST"] = dtRow["MANI_HENSOUSAKI_POST"].ToString();
                //返送先ー住所1
                rowTmp["MANI_HENSOUSAKI_ADDRESS1"] = dtRow["MANI_HENSOUSAKI_ADDRESS1"].ToString();
                //返送先ー住所2
                rowTmp["MANI_HENSOUSAKI_ADDRESS2"] = dtRow["MANI_HENSOUSAKI_ADDRESS2"].ToString();
                //返送先-名称1
                rowTmp["MANI_HENSOUSAKI_NAME1_REPORT"] = dtRow["MANI_HENSOUSAKI_NAME1_REPORT"].ToString();
                //返送先-敬称1
                rowTmp["MANI_HENSOUSAKI_KEISHOU1"] = dtRow["MANI_HENSOUSAKI_KEISHOU1"].ToString();
                //返送先-名称2
                rowTmp["MANI_HENSOUSAKI_NAME2"] = dtRow["MANI_HENSOUSAKI_NAME2"].ToString();
                //返送先-敬称2
                rowTmp["MANI_HENSOUSAKI_KEISHOU2"] = dtRow["MANI_HENSOUSAKI_KEISHOU2"].ToString();
                //返送先-部署
                rowTmp["MANI_HENSOUSAKI_BUSHO"] = dtRow["MANI_HENSOUSAKI_BUSHO"].ToString();
                //返送先ーマニフェスト担当
                rowTmp["MANI_HENSOUSAKI_TANTOU"] = dtRow["MANI_HENSOUSAKI_TANTOU"].ToString();

                //①－２
                //連絡先1
                rowTmp["MANI_RENRAKUSAKI1"] = this.sysInfoEntity.MANIFEST_HENSOU_RENRAKU_1;
                //連絡先2
                rowTmp["MANI_RENRAKUSAKI2"] = this.sysInfoEntity.MANIFEST_HENSOU_RENRAKU_2;
                //①－３
                // 会社略称情報
                DataTable dataTable = mDao.GetDateForStringSql("SELECT M_CORP_INFO.CORP_NAME from M_CORP_INFO");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    //返送元-名称
                    rowTmp["MANI_HENSOUMODO_NAME"] = (string)dataTable.Rows[0].ItemArray[0];
                }

                //拠点名称
                if (this.mKyotenInfo != null)
                {
                    //返送元ー郵便番号
                    rowTmp["KYOTEN_POST"] = this.mKyotenInfo.KYOTEN_POST;
                    //返送元ー住所1
                    rowTmp["KYOTEN_ADDRESS1"] = this.mKyotenInfo.KYOTEN_ADDRESS1;
                    //返送元ー住所2
                    rowTmp["KYOTEN_ADDRESS2"] = this.mKyotenInfo.KYOTEN_ADDRESS2;
                    //返送元ー電話番号
                    rowTmp["KYOTEN_TEL"] = this.mKyotenInfo.KYOTEN_TEL;
                    //返送元ーFAX番号
                    rowTmp["KYOTEN_FAX"] = this.mKyotenInfo.KYOTEN_FAX;
                }

                //①－４
                //捺印タイトル1
                rowTmp["BLANK1"] = this.sysInfoEntity.MANIFEST_HENSOU_NATSUIN_1;
                //捺印タイトル2
                rowTmp["BLANK2"] = this.sysInfoEntity.MANIFEST_HENSOU_NATSUIN_2;

                dataTableTmp.Rows.Add(rowTmp);
            }
            return dataTableTmp;

            #endregion - Header -
        }

        /// <summary>帳票詳細情報取得（複数） </summary>
        public DataTable CreateDetailDataTableD(DataTable tempDT)
        {
            string ShuturyokuNaiyo = this.form.txt_ShuturyokuNaiyoCD.Text;
            #region - Detail -

            DataTable dataTableTmp;
            DataRow rowTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";
            //②－１
            //行番号
            dataTableTmp.Columns.Add(ConstCls.ReportColName.ROW_NO);
            //排出事業者1
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HST_GYOUSHA_CD1);
            //排出事業者2
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HST_GYOUSHA_CD2);
            //排出事業場1
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HST_GENBA_CD1);
            //排出事業場2
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HST_GENBA_CD2);
            //廃棄物種類
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HAIKI_SHURUI_CD);
            //交付年月日
            dataTableTmp.Columns.Add(ConstCls.ReportColName.KOUFU_DATE);
            //交付番号
            dataTableTmp.Columns.Add(ConstCls.ReportColName.MANIFEST_ID);
            //返日
            dataTableTmp.Columns.Add(ConstCls.ReportColName.REF_DATE);
            //返送先
            dataTableTmp.Columns.Add(ConstCls.ReportColName.HENSOUSAKI_NAME);
            //A票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_A);
            //B1票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B1);
            //B2票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B2);
            //B4票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B4);
            //B6票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B6);
            //C1票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_C1);
            //C2票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_C2);
            //D票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_D);
            //E票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_E);

            DataTable detailDT;
            if (ShuturyokuNaiyo.Equals("1"))
            {
                //交付番号毎検索結果を整理
                detailDT = this.GetDataForKohuBangoDT(tempDT);
            }
            else
            {
                // 現場検索結果を整理
                detailDT = this.GetDataForGenbaDT(tempDT);
            }

            for (int i = 0; i < detailDT.Rows.Count; i++)
            {
                DataRow dtRow = detailDT.Rows[i];
                rowTmp = dataTableTmp.NewRow();

                //行番号
                rowTmp[ConstCls.ReportColName.ROW_NO] = i + 1;
                //交付年月日
                rowTmp[ConstCls.ReportColName.KOUFU_DATE] = this.ChgDBNullToValue(dtRow["KOUFU_DATE"], string.Empty);
                //交付番号
                rowTmp[ConstCls.ReportColName.MANIFEST_ID] = this.ChgDBNullToValue(dtRow["MANIFEST_ID"], string.Empty);
                //A票
                rowTmp[ConstCls.ReportColName.SEND_A] = this.ChgDBNullToValue(dtRow["SEND_A"], string.Empty);
                //B1票
                rowTmp[ConstCls.ReportColName.SEND_B1] = this.ChgDBNullToValue(dtRow["SEND_B1"], string.Empty);
                //B2票
                rowTmp[ConstCls.ReportColName.SEND_B2] = this.ChgDBNullToValue(dtRow["SEND_B2"], string.Empty);
                //B4票
                rowTmp[ConstCls.ReportColName.SEND_B4] = this.ChgDBNullToValue(dtRow["SEND_B4"], string.Empty);
                //B6票
                rowTmp[ConstCls.ReportColName.SEND_B6] = this.ChgDBNullToValue(dtRow["SEND_B6"], string.Empty);
                //C1票
                rowTmp[ConstCls.ReportColName.SEND_C1] = this.ChgDBNullToValue(dtRow["SEND_C1"], string.Empty);
                //C2票
                rowTmp[ConstCls.ReportColName.SEND_C2] = this.ChgDBNullToValue(dtRow["SEND_C2"], string.Empty);
                //D票
                rowTmp[ConstCls.ReportColName.SEND_D] = this.ChgDBNullToValue(dtRow["SEND_D"], string.Empty);
                //E票
                rowTmp[ConstCls.ReportColName.SEND_E] = this.ChgDBNullToValue(dtRow["SEND_E"], string.Empty);
                if (ShuturyokuNaiyo.Equals("2"))
                {
                    //排出事業者1
                    rowTmp[ConstCls.ReportColName.HST_GYOUSHA_CD1] = this.ChgDBNullToValue(dtRow["HST_GYOUSHA_NAME1"], string.Empty);
                    //排出事業者2
                    rowTmp[ConstCls.ReportColName.HST_GYOUSHA_CD2] = this.ChgDBNullToValue(dtRow["HST_GYOUSHA_NAME2"], string.Empty);
                    //排出事業場1
                    rowTmp[ConstCls.ReportColName.HST_GENBA_CD1] = this.ChgDBNullToValue(dtRow["HST_GENBA_NAME1"], string.Empty);
                    //排出事業場2
                    rowTmp[ConstCls.ReportColName.HST_GENBA_CD2] = this.ChgDBNullToValue(dtRow["HST_GENBA_NAME2"], string.Empty);
                    //廃棄物種類
                    rowTmp[ConstCls.ReportColName.HAIKI_SHURUI_CD] = this.ChgDBNullToValue(dtRow["HAIKI_SHURUI_NAME"], string.Empty);
                }
                dataTableTmp.Rows.Add(rowTmp);
            }

            return dataTableTmp;
            #endregion - Detail -
        }

        /// <summary>/帳票Footer情報取得（複数）</summary>
        public DataTable CreateFooterDataTableD(DataTable tempDT)
        {
            string ShuturyokuNaiyo = this.form.txt_ShuturyokuNaiyoCD.Text;
            #region - Footer -
            DataTable dataTableTmp;
            DataRow rowTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";
            //②－１

            //A票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_A);
            //B1票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B1);
            //B2票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B2);
            //B4票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B4);
            //B6票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_B6);
            //C1票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_C1);
            //C2票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_C2);
            //D票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_D);
            //E票
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SEND_E);
            //合計
            dataTableTmp.Columns.Add(ConstCls.ReportColName.SUM_NUM);
            rowTmp = dataTableTmp.NewRow();
            int CountA = 0;
            int CountB1 = 0;
            int CountB2 = 0;
            int CountB4 = 0;
            int CountB6 = 0;
            int CountC1 = 0;
            int CountC2 = 0;
            int CountD = 0;
            int CountE = 0;
            if (!ShuturyokuNaiyo.Equals("3"))
            {
                foreach (DataRow dt in tempDT.Rows)
                {
                    CountA = CountA + this.GetDateTimeCount(dt["SEND_A"], 0);
                    CountB1 = CountB1 + this.GetDateTimeCount(dt["SEND_B1"], 0);//;
                    CountB2 = CountB2 + this.GetDateTimeCount(dt["SEND_B2"], 0); //;
                    CountB4 = CountB4 + this.GetDateTimeCount(dt["SEND_B4"], 0);//SEND_B4;
                    CountB6 = CountB6 + this.GetDateTimeCount(dt["SEND_B6"], 0); //SEND_B6;
                    CountC1 = CountC1 + this.GetDateTimeCount(dt["SEND_C1"], 0); //SEND_C1;
                    CountC2 = CountC2 + this.GetDateTimeCount(dt["SEND_C2"], 0); //SEND_C2;
                    CountD = CountD + this.GetDateTimeCount(dt["SEND_D"], 0); //M_SEND_D;
                    CountE = CountE + this.GetDateTimeCount(dt["SEND_E"], 0); //SEND_E;
                }
            }
            else if (ShuturyokuNaiyo.Equals("3"))
            {
                // A
                CountA = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_A"));
                CountB1 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_B1"));
                CountB2 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_B2"));
                CountB4 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_B4"));
                CountB6 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_B6"));
                CountC1 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_C1"));
                CountC2 = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_C2"));
                CountD = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_D"));
                CountE = this.searchHenkyakusakiShukeiData.AsEnumerable().Sum(r => r.Field<int>("M_SEND_E"));
            }
            //A票
            rowTmp[ConstCls.ReportColName.SEND_A] = CountA;
            //B1票
            rowTmp[ConstCls.ReportColName.SEND_B1] = CountB1;
            //B2票
            rowTmp[ConstCls.ReportColName.SEND_B2] = CountB2;
            //B4票
            rowTmp[ConstCls.ReportColName.SEND_B4] = CountB4;
            //B6票
            rowTmp[ConstCls.ReportColName.SEND_B6] = CountB6;
            //C1票
            rowTmp[ConstCls.ReportColName.SEND_C1] = CountC1;
            //C2票
            rowTmp[ConstCls.ReportColName.SEND_C2] = CountC2;
            //D票
            rowTmp[ConstCls.ReportColName.SEND_D] = CountD;
            //E票
            rowTmp[ConstCls.ReportColName.SEND_E] = CountE;
            if (this.form.txt_InsatuKubunCD.Text.Equals("1"))
            {
                //合計
                rowTmp[ConstCls.ReportColName.SUM_NUM] = CountA + CountB1 + CountB2 + CountC1 + CountC2 + CountD + CountE;
            }
            else
            {
                //合計
                rowTmp[ConstCls.ReportColName.SUM_NUM] = CountA + CountB2 + CountB4 + CountB6 + CountC1 + CountC2 + CountD + CountE;
            }
            dataTableTmp.Rows.Add(rowTmp);
            return dataTableTmp;

            #endregion - Footer -
        }

        #endregion

        #endregion

        #region [F6]CSV出力処理

        /// <summary>
        /// GridViewからCSVFile出力
        /// </summary>
        internal void CsvOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();
                CustomDataGridView objDataGridView = null;
                //交付番号毎表示
                if (this.form.KohuBangoGRD.Visible)
                {
                    objDataGridView = this.form.KohuBangoGRD;
                } //現場毎非表示
                else if (this.form.GenbaGRD.Visible)
                {
                    //現場毎表示
                    this.CsvMultiRowOutput();
                    return;
                }  //返却先集計非表示
                else if (this.form.HenkyakusakiShukeiGRD.Visible)
                {
                    objDataGridView = this.form.HenkyakusakiShukeiGRD;
                }

                //一覧に明細行がない場合
                if (objDataGridView == null || objDataGridView.RowCount == 0)
                {
                    //アラートを表示し、CSV出力処理はしない
                    this.messageShowLogic.MessageBoxShow("E044");
                }
                else
                {
                    //CSV出力確認メッセージを表示する
                    if (this.messageShowLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        //共通部品を利用して、画面に表示されているデータをCSVに出力する
                        CSVExport CSVExport = new CSVExport();
                        objDataGridView.Columns[0].HeaderText = "行番号";
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //CSVExport.ConvertCustomDataGridViewToCsv(objDataGridView, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_HENSO_ANNAI) + "(" + objDataGridView.Tag + ")_" + DateTime.Now.ToString("yyyyMMddHHmmss"), this.form);
                        CSVExport.ConvertCustomDataGridViewToCsv(objDataGridView, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_HENSO_ANNAI) + "(" + objDataGridView.Tag + ")_" + this.getDBDateTime().ToString("yyyyMMddHHmmss"), this.form);
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        objDataGridView.Columns[0].HeaderText = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CsvOutput", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region MulthiRowからCSVFile出力

        /// <summary>
        /// MulthiRowからCSVFile出力
        /// </summary>
        internal void CsvMultiRowOutput()
        {
            DialogResult dr = DialogResult.None; //保存をキャンセルされたか
            string csvpath = "未設定"; //出力ファイルパス
            try
            {
                LogUtility.DebugMethodStart();
                //一覧に明細行がない場合
                if (this.form.GenbaGRD == null || this.form.GenbaGRD.RowCount == 0)
                {
                    //アラートを表示し、CSV出力処理はしない
                    this.messageShowLogic.MessageBoxShow("E044");
                }
                else
                {
                    if (this.messageShowLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        this.OutPutCsvMeisaiData();
                    }
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                r_framework.Utility.LogUtility.Debug("ダイアログ結果：" + dr.ToString());
                r_framework.Utility.LogUtility.Debug("出力CSVパス：" + csvpath);
                r_framework.Utility.LogUtility.DebugMethodEnd();
            }
        }

        #region Csv明細ヘッダ文字列を作成

        /// <summary>
        /// Csv明細ヘッダ文字列を作成
        /// </summary>
        /// <returns>明細ヘッダ文字列（カンマで区切済み）</returns>
        private string GetCsvHeaderString()
        {
            string returnVal = string.Empty;

            try
            {
                LogUtility.DebugMethodStart();

                List<string> csvItems = new List<string>();
                //印刷区分「直行用」の場合
                if (this.form.txt_InsatuKubunCD.Text.Equals("1"))
                {
                    // 明細
                    csvItems.Add("行番号");
                    csvItems.Add("排出事業者1");
                    csvItems.Add("排出事業者2");
                    csvItems.Add("排出事業場1");
                    csvItems.Add("排出事業場2");
                    csvItems.Add("廃棄物種類");
                    csvItems.Add("交付年月日");
                    csvItems.Add("交付番号");
                    csvItems.Add("A票");
                    csvItems.Add("B1票");
                    csvItems.Add("B2票");
                    csvItems.Add("C1票");
                    csvItems.Add("C2票");
                    csvItems.Add("D票");
                    csvItems.Add("E票");
                }
                else if (this.form.txt_InsatuKubunCD.Text.Equals("2"))
                {
                    // 明細
                    csvItems.Add("行番号");
                    csvItems.Add("排出事業者1");
                    csvItems.Add("排出事業者2");
                    csvItems.Add("排出事業場1");
                    csvItems.Add("排出事業場2");
                    csvItems.Add("廃棄物種類");
                    csvItems.Add("交付年月日");
                    csvItems.Add("交付番号");
                    csvItems.Add("A票");
                    csvItems.Add("B2票");
                    csvItems.Add("B4票");
                    csvItems.Add("B6票");
                    csvItems.Add("C1票");
                    csvItems.Add("C2票");
                    csvItems.Add("D票");
                    csvItems.Add("E票");
                }

                // カンマで区切り
                returnVal = string.Join(",", csvItems);

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

        #region Csv明細データ文字列を作成

        /// <summary>
        /// Csv明細データ文字列を作成
        /// </summary>
        /// <returns>明細データ文字列（カンマで区切済み）</returns>
        private void OutPutCsvMeisaiData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //詳細テーブル
                // 対象テーブル存在しない場合
                var dt = this.form.GenbaGRD;
                if (dt == null || dt.Rows.Count < 1)
                {
                    // 処理終了
                    return;
                }

                // 出力項目名
                List<string> outputItems = null;
                //印刷区分「直行用」の場合
                if (this.form.txt_InsatuKubunCD.Text.Equals("1"))
                {
                    // 出力項目名
                    outputItems = new List<string> { "G_ROWNO"	            //行番号
                                                            , "G_HST_GYOUSHA_CD1"	    //排出事業者1
                                                            , "G_HST_GYOUSHA_CD2"	        //排出事業者2
                                                            , "G_HST_GENBA_CD1"	    //荷排出事業場1
                                                            , "G_HST_GENBA_CD2"	        //荷排出事業場2
                                                            , "G_HAIKI_SHURUI_CD"	        //廃棄物種類
                                                            , "G_KOUFU_DATE"	        //交付年月日CD
                                                            , "G_MANIFEST_ID"	       //交付番号
                                                            , "G_SEND_A"	        //A票
                                                            , "G_SEND_B1"	        //B1票
                                                            , "G_SEND_B2"	        //B2票
                                                            , "G_SEND_C1"	            //C1票
                                                            , "G_SEND_C2"	        //C1票
                                                            , "G_SEND_D"	            //D票
                                                            , "G_SEND_E"	        //E票
                                                            };
                }
                else if (this.form.txt_InsatuKubunCD.Text.Equals("2"))
                {
                    // 出力項目名
                    outputItems = new List<string> { "G_ROWNO"	            //行番号
                                                            , "G_HST_GYOUSHA_CD1"	    //排出事業者1
                                                            , "G_HST_GYOUSHA_CD2"	        //排出事業者2
                                                            , "G_HST_GENBA_CD1"	    //荷排出事業場1
                                                            , "G_HST_GENBA_CD2"	        //荷排出事業場2
                                                            , "G_HAIKI_SHURUI_CD"	        //廃棄物種類
                                                            , "G_KOUFU_DATE"	        //交付年月日CD
                                                            , "G_MANIFEST_ID"	       //交付番号
                                                            , "G_SEND_A"	        //A票
                                                            , "G_SEND_B2"	        //B2票
                                                            , "G_SEND_B4"	            //B4票
                                                            , "G_SEND_B6"	            //B6票
                                                            , "G_SEND_C1"	            //C1票
                                                            , "G_SEND_C2"	        //C1票
                                                            , "G_SEND_D"	            //D票
                                                            , "G_SEND_E"	        //E票
                                                            };
                }

                // 値出力
                GcCustomMultiRow dtDetail = this.form.GenbaGRD;//ReportInfo.DataTableList["Detail"];

                using (CustomDataGridView tempGrv = new CustomDataGridView())
                {
                    DataTable tempDt = new DataTable();
                    this.form.Controls.Add(tempGrv);
                    tempGrv.Visible = false;

                    // ヘッダカラム作成
                    foreach (var colunName in outputItems)
                    {
                        // DataGridViewのColumn
                        DataGridViewTextBoxColumn tempDgvCol = new DataGridViewTextBoxColumn();
                        tempDgvCol.DataPropertyName = colunName;
                        tempDgvCol.Name = colunName;
                        switch (colunName)
                        {
                            case "G_ROWNO":
                                tempDgvCol.HeaderText = "行番号";
                                break;

                            case "G_HST_GYOUSHA_CD1":
                                tempDgvCol.HeaderText = "排出事業者1";
                                break;

                            case "G_HST_GYOUSHA_CD2":
                                tempDgvCol.HeaderText = "排出事業者2";
                                break;

                            case "G_HST_GENBA_CD1":
                                tempDgvCol.HeaderText = "排出事業場1";
                                break;

                            case "G_HST_GENBA_CD2":
                                tempDgvCol.HeaderText = "排出事業場2";
                                break;

                            case "G_HAIKI_SHURUI_CD":
                                tempDgvCol.HeaderText = "廃棄物種類";
                                break;

                            case "G_KOUFU_DATE":
                                tempDgvCol.HeaderText = "交付年月日";
                                break;

                            case "G_MANIFEST_ID":
                                tempDgvCol.HeaderText = "交付番号";
                                break;

                            case "G_SEND_A":
                                tempDgvCol.HeaderText = "A票";
                                break;

                            case "G_SEND_B1":
                                tempDgvCol.HeaderText = "B1票";
                                break;

                            case "G_SEND_B2":
                                tempDgvCol.HeaderText = "B2票";
                                break;

                            case "G_SEND_B4":
                                tempDgvCol.HeaderText = "B4票";
                                break;

                            case "G_SEND_B6":
                                tempDgvCol.HeaderText = "B6票";
                                break;

                            case "G_SEND_C1":
                                tempDgvCol.HeaderText = "C1票";
                                break;

                            case "G_SEND_C2":
                                tempDgvCol.HeaderText = "C2票";
                                break;

                            case "G_SEND_D":
                                tempDgvCol.HeaderText = "D票";
                                break;

                            case "G_SEND_E":
                                tempDgvCol.HeaderText = "E票";
                                break;
                        }
                        tempGrv.Columns.Add(tempDgvCol);

                        // DataTableのColumn
                        tempDt.Columns.Add(colunName);
                    }

                    foreach (Row row in dtDetail.Rows)
                    {
                        DataRow tempNewRow = tempDt.NewRow();

                        foreach (var item in outputItems)
                        {
                            tempNewRow[item] = row.Cells[item].Value == null ? string.Empty : row.Cells[item].Value.ToString();
                        }

                        tempDt.Rows.Add(tempNewRow);
                    }

                    tempGrv.DataSource = tempDt;
                    tempGrv.Refresh();

                    CSVExport CSVExport = new CSVExport();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //CSVExport.ConvertCustomDataGridViewToCsv(tempGrv, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_HENSO_ANNAI) + "(" + this.form.GenbaGRD.Tag + ")_" + DateTime.Now.ToString("yyyyMMddHHmmss"), this.form);
                    CSVExport.ConvertCustomDataGridViewToCsv(tempGrv, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_HENSO_ANNAI) + "(" + this.form.GenbaGRD.Tag + ")_" + this.getDBDateTime().ToString("yyyyMMddHHmmss"), this.form);
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    this.form.Controls.Remove(tempGrv);
                }
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
        #endregion

        #endregion

        #region [F8]検索

        public int Search()
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //検索実行前チェック処理
                if (CheckSearchCondition())
                {
                    //検索条件取得
                    this.GetSearchCondition();
                    //検索データ取得と検索データ処理
                    this.IchiranDataInit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                if (ex is SQLRuntimeException)
                {
                    this.messageShowLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E245", "");
                }
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 検索条件取得
        /// </summary>
        private void GetSearchCondition()
        {
            this.searchDtoCondition = new DTOClass();
            //返却日
            this.searchDtoCondition.RET_DATE_FROM = string.IsNullOrEmpty(this.form.txtHinnkyakuDateFROM.Text.ToString()) ? string.Empty : DateTime.Parse(this.form.txtHinnkyakuDateFROM.Text.ToString()).ToShortDateString();
            this.searchDtoCondition.RET_DATE_TO = string.IsNullOrEmpty(this.form.txtHinnkyakuDateTO.Text.ToString()) ? string.Empty : DateTime.Parse(this.form.txtHinnkyakuDateTO.Text.ToString()).ToShortDateString();

            //排出業者
            this.searchDtoCondition.HST_GYOUSHA_CD_FROM = this.form.txtHaishutsuJigyoushaCd.Text.Trim();
            this.searchDtoCondition.HST_GYOUSHA_CD_TO = this.form.txtHaishutsuJigyoushaCdTO.Text.Trim();
            this.searchDtoCondition.HST_GENBA_CD_FROM = this.form.txtHaishutsuGenbaCd.Text.Trim();
            this.searchDtoCondition.HST_GENBA_CD_TO = this.form.txtHaishutsuGenbaCdTO.Text.Trim();

            //返却先取引先
            this.searchDtoCondition.TORIHIKISAKI_CD_FROM = this.form.txtTORIHIKISAKI_CD.Text.Trim();
            this.searchDtoCondition.TORIHIKISAKI_CD_TO = this.form.txtTORIHIKISAKI_CD_TO.Text.Trim();

            //返却先業者
            this.searchDtoCondition.GYOUSHA_CD_FROM = this.form.txtGYOUSHA_CD.Text.Trim();
            this.searchDtoCondition.GYOUSHA_CD_TO = this.form.txtGYOUSHA_CD_TO.Text.Trim();
            this.searchDtoCondition.GENBA_CD_FROM = this.form.txtGENBA_CD.Text.Trim();
            this.searchDtoCondition.GENBA_CD_TO = this.form.txtGENBA_CD_TO.Text.Trim();
            this.searchDtoCondition.INSATU_KBN = this.form.txt_InsatuKubunCD.Text.Trim();
            this.searchDtoCondition.OUTPUT_KBN = this.form.txt_ShuturyokuNaiyoCD.Text.Trim();

            //拠点
            if (this.headForm.KYOTEN_CD.Text != "99")
            {
                this.searchDtoCondition.KYOTEN_CD = this.headForm.KYOTEN_CD.Text.Trim();//155770
            }
        }

        /// <summary>
        /// 実行前チェック処理
        /// </summary>
        private bool CheckSearchCondition()
        {
            bool resultFlg = true;
            string strMessage = string.Empty;
            //返却日
            if (!string.IsNullOrEmpty(this.form.txtHinnkyakuDateFROM.Text) || !string.IsNullOrEmpty(this.form.txtHinnkyakuDateTO.Text))
            {
                if (string.IsNullOrEmpty(this.form.txtHinnkyakuDateFROM.Text))
                {
                    strMessage = "返却日(From)";
                    resultFlg = false;
                }
                else if (string.IsNullOrEmpty(this.form.txtHinnkyakuDateTO.Text))
                {
                    strMessage = "返却日(To)";
                    resultFlg = false;
                }
            }
            //排出業者
            if (!string.IsNullOrEmpty(this.form.txtHaishutsuJigyoushaCd.Text) || !string.IsNullOrEmpty(this.form.txtHaishutsuJigyoushaCdTO.Text))
            {
                if (string.IsNullOrEmpty(this.form.txtHaishutsuJigyoushaCd.Text))
                {
                    strMessage = "排出事業者CD(From)";
                    resultFlg = false;
                }
                else if (string.IsNullOrEmpty(this.form.txtHaishutsuJigyoushaCdTO.Text))
                {
                    strMessage = "排出事業者CD(To)";
                    resultFlg = false;
                }
            }
            //排出現場
            if (!string.IsNullOrEmpty(this.form.txtHaishutsuGenbaCd.Text) || !string.IsNullOrEmpty(this.form.txtHaishutsuGenbaCdTO.Text))
            {
                if (string.IsNullOrEmpty(this.form.txtHaishutsuGenbaCd.Text))
                {
                    strMessage = "排出事業場CD(From)";
                    resultFlg = false;
                }
                else if (string.IsNullOrEmpty(this.form.txtHaishutsuGenbaCdTO.Text))
                {
                    strMessage = "排出事業場CD(To)";
                    resultFlg = false;
                }
            }
            //返送先(取引先)CD
            if (!string.IsNullOrEmpty(this.form.txtTORIHIKISAKI_CD.Text) || !string.IsNullOrEmpty(this.form.txtTORIHIKISAKI_CD_TO.Text))
            {
                if (string.IsNullOrEmpty(this.form.txtTORIHIKISAKI_CD.Text))
                {
                    strMessage = "返送先(取引先)CD(From)";
                    resultFlg = false;
                }
                else if (string.IsNullOrEmpty(this.form.txtTORIHIKISAKI_CD_TO.Text))
                {
                    strMessage = "返送先(取引先)CD(To)";
                    resultFlg = false;
                }
            }
            //返送先(業者)
            if (!string.IsNullOrEmpty(this.form.txtGYOUSHA_CD.Text) || !string.IsNullOrEmpty(this.form.txtGYOUSHA_CD_TO.Text))
            {
                if (string.IsNullOrEmpty(this.form.txtGYOUSHA_CD.Text))
                {
                    strMessage = "返送先業者CD(From)";
                    resultFlg = false;
                }
                else if (string.IsNullOrEmpty(this.form.txtGYOUSHA_CD_TO.Text))
                {
                    strMessage = "返送先業者CD(To)";
                    resultFlg = false;
                }
            }
            //返送先現場
            if (!string.IsNullOrEmpty(this.form.txtGENBA_CD.Text) || !string.IsNullOrEmpty(this.form.txtGENBA_CD_TO.Text))
            {
                if (string.IsNullOrEmpty(this.form.txtGENBA_CD.Text))
                {
                    strMessage = "返送先現場CD(From)";
                    resultFlg = false;
                }
                else if (string.IsNullOrEmpty(this.form.txtGENBA_CD_TO.Text))
                {
                    strMessage = "返送先現場CD(To)";
                    resultFlg = false;
                }
            }
            //印刷区分
            if (string.IsNullOrEmpty(this.form.txt_InsatuKubunCD.Text))
            {
                strMessage = "印刷区分";
                resultFlg = false;
            }
            //出力内容
            if (string.IsNullOrEmpty(this.form.txt_ShuturyokuNaiyoCD.Text))
            {
                strMessage = "出力内容";
                resultFlg = false;
            }

            if (!resultFlg)
            {
                messageShowLogic.MessageBoxShow("E034", strMessage);
            }

            return resultFlg;
        }

        /// <summary>
        /// 一覧データ処理
        /// </summary>
        private void IchiranDataInit()
        {
            string strShuturyokuNaiyoCD = string.Empty;
            int count = 0;
            strShuturyokuNaiyoCD = this.form.txt_ShuturyokuNaiyoCD.Text;
            //廃棄物種類検索結果
            this.searchHaikiShuruiData = this.mDao.GetHaikiShuruiData(this.searchDtoCondition);
            //総Detail取得
            this.searchAllIchiranDetailData = this.mDao.GetIchiranData(this.searchDtoCondition);
            //返却先集計検索データ取得
            this.searchHenkyakusakiShukeiData = this.mDao.GetHenkyakusakiShukeiData(this.searchDtoCondition);
            //返却先につて現場マスタ（A～E票）使用可否情報検索データ取得
            this.searchGenbaUseInfoData = this.mDao.GetGenbaUseInfoData(this.searchDtoCondition);

            if (this.form.txt_ShuturyokuNaiyoCD.Text == "3")
            {
                if ((this.searchHenkyakusakiShukeiData == null || this.searchHenkyakusakiShukeiData.Rows.Count <= 0))
                {
                    this.messageShowLogic.MessageBoxShow("C001");
                    return;
                }
            }
            else
            {
                if ((this.searchAllIchiranDetailData == null || this.searchAllIchiranDetailData.Rows.Count <= 0))
                {
                    this.messageShowLogic.MessageBoxShow("C001");
                    return;
                }
            }

            //返却先情報設定
            int AllManiHensousakiCount = 0;

            if (this.searchAllIchiranDetailData != null && this.searchAllIchiranDetailData.Rows.Count > 0)
            {
                //総Detail取得
                DataView dvSort = this.searchAllIchiranDetailData.AsDataView();
                dvSort.Sort = "MANI_HENSOUSAKI_SORT,MANI_HENSOUSAKI_NAME";
                this.searchAllIchiranDetailData = dvSort.ToTable();

                //返却先情報設定
                string maniHensousakiKbnName;
                AllManiHensousakiCount = this.SetManiHensousakiName(count, out maniHensousakiKbnName);

                //返送先条件取得
                string condition = string.Empty;
                if (string.IsNullOrEmpty(this.form.txtManiHensousakiName.Tag.ToString()))
                {
                    condition = "( MANI_HENSOUSAKI_NAME IS NULL OR  MANI_HENSOUSAKI_NAME = '')";
                }
                else
                {
                    string hensousakiName = this.form.txtManiHensousakiName.Tag.ToString().Replace("'", "''");
                    condition = "MANI_HENSOUSAKI_NAME  = '" + hensousakiName + "'";
                }

                if (!string.IsNullOrEmpty(maniHensousakiKbnName))
                {
                    condition += " AND (MANI_HENSOUSAKI_NAME_KBN ='" + maniHensousakiKbnName + "')";
                }
                //返送先名称ついて返送先名称のデータ取得
                this.searchIchiranDetailData = this.searchAllIchiranDetailData.Select(condition).CopyToDataTable();

                //交付番号毎検索結果を整理
                this.searchKohuBangoDataByTorisaki = this.GetDataForKohuBangoDT(this.searchIchiranDetailData);
                // 現場検索結果を整理
                this.searchGenbaDataByTorisaki = this.GetDataForGenbaDT(this.searchIchiranDetailData);
            }

            //Headarのアラート件数を処理する。
            // 20140623 kayo 不具合#4972 アラート件数判断不正 start
            //DialogResult result = DialogResult.Yes;
            //string strAlertCount = this.headForm.alertNumber.Text.ToString().Replace(",", "");
            //if (!string.IsNullOrEmpty(strAlertCount) && !strAlertCount.Equals("0") && int.Parse(strAlertCount) < this.searchIchiranDetailData.Rows.Count)
            //{
            //    result = this.messageShowLogic.MessageBoxShow("C025");
            //}
            //if (result != DialogResult.Yes)
            //{
            //    return;
            //}
            ////交付番号毎検索結果を画面に設定
            //this.SetDataForKohuBangoGRD(this.searchIchiranDetailData);
            //// 現場毎検索結果を画面に設定
            //this.SetDataForGenbaGRD(this.searchIchiranDetailData);
            //// 返却先集計検索結果を画面に設定
            //this.SetDataForHenkyakusakiShukeiGRD(this.searchHenkyakusakiShukeiData);

            if (this.form.txt_ShuturyokuNaiyoCD.Text == "1")
            {
                //交付番号毎検索結果を画面に設定
                if (!this.SetDataForKohuBangoGRD()) { return; }
            }
            else if (this.form.txt_ShuturyokuNaiyoCD.Text == "2")
            {
                //現場毎検索結果を画面に設定
                if (!this.SetDataForGenbaGRD()) { return; }
            }
            else
            {
                // 返却先集計検索結果を画面に設定
                if (!this.SetDataForHenkyakusakiShukeiGRD()) { return; }
            }
            // 20140623 kayo 不具合#4972 アラート件数判断不正 end

            if (this.form.txt_ShuturyokuNaiyoCD.Text.Equals("3"))
            {
                //第一の返送先名称設定
                this.form.txtManiHensousakiName.Text = string.Empty;
                //返送先総件数
                this.form.txtAllManiHensousakiCount.Text = string.Empty;
                //今表示の返送先数設定
                this.form.txtManiHensousakiCount.Text = string.Empty;
                //読込データ件数設定
                this.headForm.readDataNumber.Text = this.searchHenkyakusakiShukeiData.Rows.Count.ToString("#,###");
            }
            else
            {
                //返送先総件数
                this.form.txtAllManiHensousakiCount.Text = AllManiHensousakiCount.ToString();

                //今表示の返送先数設定
                this.form.txtManiHensousakiCount.Text = (count + 1).ToString();

                if (this.form.KohuBangoGRD.Visible)
                {
                    //読込データ件数設定
                    this.headForm.readDataNumber.Text = this.form.KohuBangoGRD.Rows.Count == 0 ? "0" : this.form.KohuBangoGRD.RowCount.ToString("#,###"); //this.searchIchiranDetailData.Rows.Count.ToString("#,###");
                }
                else
                {
                    //読込データ件数設定
                    this.headForm.readDataNumber.Text = this.form.GenbaGRD.Rows.Count == 0 ? "0" : this.form.GenbaGRD.RowCount.ToString("#,###");
                }
            }
            //返送先総件数
            this.form.txtAllManiHensousakiCount.Tag = AllManiHensousakiCount.ToString();
            //今表示の返送先Index保存
            this.form.txtManiHensousakiCount.Tag = count.ToString();
        }

        /// <summary>
        /// 交付番号毎データを取得
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public DataTable GetKohuBangoGRDData(DataTable Data)
        {
            DataTable tempSearchAllData = Data;
            DataTable SearchData = tempSearchAllData.Clone();
            //返却先情報GROUP BY
            var ManiHensousakiNameInfo = tempSearchAllData.AsEnumerable().GroupBy(r => string.Format("{0},{1},{2},{3},{4},{5}",
                                            r.Field<Int64>("SYSTEM_ID"),
                                            r.Field<Int32>("SEQ"),
                                            r.Field<string>("KOUFU_DATE"),
                                            r.Field<string>("MANIFEST_ID"),
                                             r.Field<string>("HST_GYOUSHA_CD"),
                                              r.Field<string>("HST_GENBA_CD"))).ToList();
            if (ManiHensousakiNameInfo.Count > 0)
            {
                for (int i = 0; i < ManiHensousakiNameInfo.Count; i++)
                {
                    string[] arrTmp = ManiHensousakiNameInfo[i].Key.ToString().Split(',');
                    DataRow newRow = SearchData.NewRow();
                    newRow["SYSTEM_ID"] = arrTmp[0];
                    newRow["SEQ"] = arrTmp[1];
                    newRow["KOUFU_DATE"] = arrTmp[2];
                    newRow["MANIFEST_ID"] = arrTmp[3];
                    newRow["HST_GYOUSHA_CD"] = arrTmp[4];
                    newRow["HST_GENBA_CD"] = arrTmp[5];
                    string condition = "( SYSTEM_ID  = '" + arrTmp[0] + "' AND  SEQ = '" + arrTmp[1] + "')";
                    //新データ取得
                    DataTable temp = tempSearchAllData.Select(condition).CopyToDataTable();
                    foreach (DataRow dR in temp.Rows)
                    {
                        if (dR["SYSTEM_ID"].ToString().Equals(newRow["SYSTEM_ID"].ToString()) && dR["SEQ"].ToString().Equals(newRow["SEQ"].ToString()))
                        {
                            //A～E票使用区分
                            DataTable GenbaUseInfo = this.searchGenbaUseInfoData.AsEnumerable().Where(
                                             r => r.Field<Int64>("SYSTEM_ID").ToString().Equals(arrTmp[0]) &&
                                        r.Field<Int32>("SEQ").ToString().Equals(arrTmp[1])).CopyToDataTable();

                            foreach (DataColumn dC in temp.Columns)
                            {
                                if (!dC.ColumnName.Equals("SYSTEM_ID") && !dC.ColumnName.Equals("SEQ")
                                    && !dC.ColumnName.Equals("KOUFU_DATE") && !dC.ColumnName.Equals("MANIFEST_ID")
                                     && !dC.ColumnName.Equals("HST_GYOUSHA_CD") && !dC.ColumnName.Equals("HST_GENBA_CD"))
                                {
                                    if (dR[dC.ColumnName] != null && !string.IsNullOrEmpty(dR[dC.ColumnName].ToString()))
                                    {
                                        newRow[dC.ColumnName] = dR[dC.ColumnName];
                                    }
                                    else
                                    {
                                        if (GenbaUseInfo.Rows[0]["MANI_HENSOUSAKI_USE" + dC.ColumnName.Substring(4)].ToString().Equals("2"))
                                        {
                                            newRow[dC.ColumnName] = "-";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    SearchData.Rows.Add(newRow);
                    SearchData.AcceptChanges();
                }
            }
            return SearchData;
        }

        /// <summary>
        /// 現場毎データを取得
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public DataTable GetGenbaGRDData(DataTable Data)
        {
            DataTable tempSearchAllData = Data;
            DataTable SearchData = tempSearchAllData.Clone();
            //返却先情報GROUP BY
            //var ManiHensousakiNameInfo = tempSearchAllData.AsEnumerable().GroupBy(r => string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
            //                                r.Field<Int64>("SYSTEM_ID"),
            //                                r.Field<Int32>("SEQ"),
            //                                r.Field<string>("KOUFU_DATE"),
            //                                r.Field<string>("MANIFEST_ID"),
            //                                r.Field<string>("HST_GYOUSHA_NAME1"),
            //                                r.Field<string>("HST_GYOUSHA_NAME2"),
            //                                r.Field<string>("HST_GENBA_NAME1"),
            //                                r.Field<string>("HST_GENBA_NAME2"),
            //                                r.Field<string>("HAIKI_SHURUI_NAME"),
            //                                 r.Field<string>("HST_GYOUSHA_CD"),
            //                                  r.Field<string>("HST_GENBA_CD"))).ToList();
            var ManiHensousakiNameInfo = tempSearchAllData.AsEnumerable()
                .GroupBy(r => new {
                    SYSTEM_ID = r.Field<Int64>("SYSTEM_ID"),
                    SEQ = r.Field<Int32>("SEQ"),
                    KOUFU_DATE = r.Field<string>("KOUFU_DATE"),
                    MANIFEST_ID = r.Field<string>("MANIFEST_ID"),
                    HST_GYOUSHA_NAME1 = r.Field<string>("HST_GYOUSHA_NAME1"),
                    HST_GYOUSHA_NAME2 = r.Field<string>("HST_GYOUSHA_NAME2"),
                    HST_GENBA_NAME1 = r.Field<string>("HST_GENBA_NAME1"),
                    HST_GENBA_NAME2 = r.Field<string>("HST_GENBA_NAME2"),
                    HAIKI_SHURUI_NAME = r.Field<string>("HAIKI_SHURUI_NAME"),
                    HST_GYOUSHA_CD = r.Field<string>("HST_GYOUSHA_CD"),
                    HST_GENBA_CD = r.Field<string>("HST_GENBA_CD")
                });
            if (ManiHensousakiNameInfo.Count() > 0)
            {
                foreach (var grp in ManiHensousakiNameInfo)
                {
                    //for (int i = 0; i < ManiHensousakiNameInfo.Count; i++)
                    //{
                    //string[] arrTmp = ManiHensousakiNameInfo[i].Key.ToString().Split(',');
                    DataRow newRow = SearchData.NewRow();
                    newRow["SYSTEM_ID"] = grp.Key.SYSTEM_ID;// arrTmp[0];
                    newRow["SEQ"] = grp.Key.SEQ;//arrTmp[1];
                    newRow["KOUFU_DATE"] = grp.Key.KOUFU_DATE;//arrTmp[2];
                    newRow["MANIFEST_ID"] = grp.Key.MANIFEST_ID;//arrTmp[3];
                    newRow["HST_GYOUSHA_NAME1"] = grp.Key.HST_GYOUSHA_NAME1;//arrTmp[4];
                    newRow["HST_GYOUSHA_NAME2"] = grp.Key.HST_GYOUSHA_NAME2;//arrTmp[5];
                    newRow["HST_GENBA_NAME1"] = grp.Key.HST_GENBA_NAME1;//arrTmp[6];
                    newRow["HST_GENBA_NAME2"] = grp.Key.HST_GENBA_NAME2;//arrTmp[7];
                    newRow["HAIKI_SHURUI_NAME"] = grp.Key.HAIKI_SHURUI_NAME;//arrTmp[8];
                    newRow["HST_GYOUSHA_CD"] = grp.Key.HST_GYOUSHA_CD;//arrTmp[9];
                    newRow["HST_GENBA_CD"] = grp.Key.HST_GENBA_CD;//arrTmp[10];
                    //string condition = "( SYSTEM_ID  = '" + grp.Key.SYSTEM_ID + "' AND  SEQ = '" + arrTmp[1] + "')";
                    //新データ取得
                    //DataTable temp = tempSearchAllData.Select(condition).CopyToDataTable();
                    DataTable temp = grp.CopyToDataTable();
                    foreach (DataRow dR in temp.Rows)
                    {
                        if (dR["SYSTEM_ID"].ToString().Equals(newRow["SYSTEM_ID"].ToString()) && dR["SEQ"].ToString().Equals(newRow["SEQ"].ToString()))
                        {
                            //A～E票使用区分
                            DataTable GenbaUseInfo = this.searchGenbaUseInfoData.AsEnumerable().Where(
                                             r => r.Field<Int64>("SYSTEM_ID") == grp.Key.SYSTEM_ID &&
                                        r.Field<Int32>("SEQ") == grp.Key.SEQ).CopyToDataTable();
                            foreach (DataColumn dC in temp.Columns)
                            {
                                if (!dC.ColumnName.Equals("SYSTEM_ID") && !dC.ColumnName.Equals("SEQ")
                                    && !dC.ColumnName.Equals("KOUFU_DATE") && !dC.ColumnName.Equals("MANIFEST_ID")
                                    && !dC.ColumnName.Equals("HST_GYOUSHA_NAME1") && !dC.ColumnName.Equals("HST_GYOUSHA_NAME2")
                                    && !dC.ColumnName.Equals("HST_GENBA_NAME1") && !dC.ColumnName.Equals("HST_GENBA_NAME2")
                                    && !dC.ColumnName.Equals("HAIKI_SHURUI_NAME")
                                    && !dC.ColumnName.Equals("HST_GYOUSHA_CD") && !dC.ColumnName.Equals("HST_GENBA_CD"))
                                {
                                    if (dR[dC.ColumnName] != null && !string.IsNullOrEmpty(dR[dC.ColumnName].ToString()))
                                    {
                                        newRow[dC.ColumnName] = dR[dC.ColumnName];
                                    }
                                    else
                                    {
                                        if (GenbaUseInfo.Rows[0]["MANI_HENSOUSAKI_USE" + dC.ColumnName.Substring(4)].ToString().Equals("2"))
                                        {
                                            newRow[dC.ColumnName] = "-";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    SearchData.Rows.Add(newRow);
                    SearchData.AcceptChanges();
                }
            }
            return SearchData;
        }

        /// <summary>
        /// 交付番号毎検索結果を画面に設定
        /// </summary>
        public bool SetDataForKohuBangoGRD()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                CustomDataGridView objGrid = this.form.KohuBangoGRD;
                if (objGrid != null)
                {
                    // 明細クリア
                    objGrid.Rows.Clear();
                }

                if (this.searchKohuBangoDataByTorisaki == null || !this.CheckDisplayCount(this.searchKohuBangoDataByTorisaki.Rows.Count))
                {
                    LogUtility.DebugMethodStart(ret);
                    return ret;
                }

                objGrid.IsBrowsePurpose = false;

                // 検索結果
                this.searchKohuBangoDataByTorisaki.BeginLoadData();

                if (this.searchKohuBangoDataByTorisaki.Rows.Count > 0)
                {
                    // 明細行を追加
                    objGrid.Rows.Add(this.searchKohuBangoDataByTorisaki.Rows.Count);
                    // 検索結果設定
                    for (int j = 0; j < this.searchKohuBangoDataByTorisaki.Rows.Count; j++)
                    {
                        DataRow dtRow = this.searchKohuBangoDataByTorisaki.Rows[j];
                        // 行番号
                        objGrid[ConstCls.KohuBangoGRDColName.ROW_NO, j].Value = j + 1;
                        // 交付日付
                        objGrid[ConstCls.KohuBangoGRDColName.KOUFU_DATE, j].Value = this.ChgDBNullToValue(dtRow["KOUFU_DATE"], string.Empty);
                        // 交付番号
                        objGrid[ConstCls.KohuBangoGRDColName.MANIFEST_ID, j].Value = this.ChgDBNullToValue(dtRow["MANIFEST_ID"], string.Empty);
                        // A票
                        objGrid[ConstCls.KohuBangoGRDColName.SEND_A, j].Value = this.ChgDBNullToValue(dtRow["SEND_A"], string.Empty);
                        // B1票
                        objGrid[ConstCls.KohuBangoGRDColName.SEND_B1, j].Value = this.ChgDBNullToValue(dtRow["SEND_B1"], string.Empty);
                        // B2票
                        objGrid[ConstCls.KohuBangoGRDColName.SEND_B2, j].Value = this.ChgDBNullToValue(dtRow["SEND_B2"], string.Empty);
                        // B4票
                        objGrid[ConstCls.KohuBangoGRDColName.SEND_B4, j].Value = this.ChgDBNullToValue(dtRow["SEND_B4"], string.Empty);
                        // B6票
                        objGrid[ConstCls.KohuBangoGRDColName.SEND_B6, j].Value = this.ChgDBNullToValue(dtRow["SEND_B6"], string.Empty);
                        // C1票
                        objGrid[ConstCls.KohuBangoGRDColName.SEND_C1, j].Value = this.ChgDBNullToValue(dtRow["SEND_C1"], string.Empty);
                        // C2票
                        objGrid[ConstCls.KohuBangoGRDColName.SEND_C2, j].Value = this.ChgDBNullToValue(dtRow["SEND_C2"], string.Empty);
                        // D票
                        objGrid[ConstCls.KohuBangoGRDColName.SEND_D, j].Value = this.ChgDBNullToValue(dtRow["SEND_D"], string.Empty);
                        // E票
                        objGrid[ConstCls.KohuBangoGRDColName.SEND_E, j].Value = this.ChgDBNullToValue(dtRow["SEND_E"], string.Empty);
                    }
                }

                objGrid.IsBrowsePurpose = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDataForKohuBangoGRD", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        // 20140623 kayo 不具合#4972 アラート件数判断不正 start
        /// <summary>
        /// 交付番号毎検索結果を整理
        /// </summary>
        private DataTable GetDataForKohuBangoDT(DataTable Data)
        {
            try
            {
                LogUtility.DebugMethodStart(Data);

                DataView obj = Data.DefaultView;
                obj.Sort = "KOUFU_DATE,MANIFEST_ID,SYSTEM_ID";
                DataTable objData = obj.ToTable(true, "SYSTEM_ID", "SEQ", "HST_GYOUSHA_CD", "HST_GENBA_CD", "KOUFU_DATE",
                         "MANIFEST_ID",
                         "SEND_A",
                         "SEND_B1",
                         "SEND_B2",
                         "SEND_B4",
                         "SEND_B6",
                         "SEND_C1",
                         "SEND_C2",
                         "SEND_D",
                         "SEND_E");

                objData = this.GetKohuBangoGRDData(objData);

                return objData;
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
        // 20140623 kayo 不具合#4972 アラート件数判断不正 end

        /// <summary>
        /// 現場毎検索結果を画面に設定
        /// </summary>
        public bool SetDataForGenbaGRD()
        {
            bool ret = true;
            try
            {
                GcCustomMultiRow objMultiRow = this.form.GenbaGRD;
                if (objMultiRow != null)
                {
                    // 明細クリア
                    objMultiRow.Rows.Clear();
                }

                LogUtility.DebugMethodStart();
                if (this.searchGenbaDataByTorisaki == null || !this.CheckDisplayCount(this.searchGenbaDataByTorisaki.Rows.Count))
                {
                    LogUtility.DebugMethodStart(ret);
                    return ret;
                }

                objMultiRow.IsBrowsePurpose = false;

                // 検索結果
                this.searchGenbaDataByTorisaki.BeginLoadData();

                if (this.searchGenbaDataByTorisaki.Rows.Count > 0)
                {
                    // 明細行を追加
                    objMultiRow.Rows.Add(this.searchGenbaDataByTorisaki.Rows.Count);
                    // 検索結果設定
                    for (int j = 0; j < this.searchGenbaDataByTorisaki.Rows.Count; j++)
                    {
                        DataRow dtRow = this.searchGenbaDataByTorisaki.Rows[j];

                        // 行番号
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.ROW_NO].Value = j + 1;
                        //  排出事業者
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.HST_GYOUSHA_CD1].Value = this.ChgDBNullToValue(dtRow["HST_GYOUSHA_NAME1"], string.Empty);
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.HST_GYOUSHA_CD2].Value = this.ChgDBNullToValue(dtRow["HST_GYOUSHA_NAME2"], string.Empty);
                        //  排出事業場
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.HST_GENBA_CD1].Value = this.ChgDBNullToValue(dtRow["HST_GENBA_NAME1"], string.Empty);
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.HST_GENBA_CD2].Value = this.ChgDBNullToValue(dtRow["HST_GENBA_NAME2"], string.Empty);
                        //  廃棄物種類
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.HAIKI_SHURUI_CD].Value = this.ChgDBNullToValue(dtRow["HAIKI_SHURUI_NAME"], string.Empty);
                        // 交付日付
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.KOUFU_DATE].Value = this.ChgDBNullToDateTimeValue(dtRow["KOUFU_DATE"], string.Empty);
                        // 交付番号
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.MANIFEST_ID].Value = this.ChgDBNullToValue(dtRow["MANIFEST_ID"], string.Empty);
                        // A票
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.SEND_A].Value = this.ChgDBNullToDateTimeValue(dtRow["SEND_A"], string.Empty);
                        // B1票
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.SEND_B1].Value = this.ChgDBNullToDateTimeValue(dtRow["SEND_B1"], string.Empty);
                        // B2票
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.SEND_B2].Value = this.ChgDBNullToDateTimeValue(dtRow["SEND_B2"], string.Empty);
                        // B4票
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.SEND_B4].Value = this.ChgDBNullToDateTimeValue(dtRow["SEND_B4"], string.Empty);
                        // B6票
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.SEND_B6].Value = this.ChgDBNullToDateTimeValue(dtRow["SEND_B6"], string.Empty);
                        // C1票
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.SEND_C1].Value = this.ChgDBNullToDateTimeValue(dtRow["SEND_C1"], string.Empty);
                        // C2票
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.SEND_C2].Value = this.ChgDBNullToDateTimeValue(dtRow["SEND_C2"], string.Empty);
                        // D票
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.SEND_D].Value = this.ChgDBNullToDateTimeValue(dtRow["SEND_D"], string.Empty);
                        // E票
                        objMultiRow.Rows[j][ConstCls.GenbaGRDColName.SEND_E].Value = this.ChgDBNullToDateTimeValue(dtRow["SEND_E"], string.Empty);
                    }
                }

                objMultiRow.IsBrowsePurpose = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDataForGenbaGRD", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        // 20140623 kayo 不具合#4972 アラート件数判断不正 start
        /// <summary>
        /// 現場毎検索結果を整理
        /// </summary>
        private DataTable GetDataForGenbaDT(DataTable Data)
        {
            try
            {
                LogUtility.DebugMethodStart(Data);

                foreach (DataRow dtRow in Data.Rows)
                {
                    //  廃棄物種類
                    if (this.searchHaikiShuruiData != null && this.searchHaikiShuruiData.Rows.Count > 0)
                    {
                        string contition = " SYSTEM_ID = '" + this.ChgDBNullToValue(dtRow["SYSTEM_ID"], string.Empty)
                            + "' AND SEQ = '" + this.ChgDBNullToValue(dtRow["SEQ"], string.Empty)
                            + "' AND HAIKI_KBN_CD = '" + this.ChgDBNullToValue(dtRow["HAIKI_KBN_CD"], string.Empty) + "'"
                             + " AND HAIKI_SHURUI_NAME IS NOT NULL";
                        DataRow[] HAIKI_SHURUIinfo = this.searchHaikiShuruiData.Select(contition);
                        string strHAIKI_SHURUINm = string.Empty;
                        if (HAIKI_SHURUIinfo.Length > 0 && HAIKI_SHURUIinfo[0] != null)
                        {
                            strHAIKI_SHURUINm = HAIKI_SHURUIinfo[0]["HAIKI_SHURUI_NAME"].ToString();
                            if (HAIKI_SHURUIinfo.Length > 1 && HAIKI_SHURUIinfo[1] != null)
                            {
                                if (string.IsNullOrEmpty(strHAIKI_SHURUINm))
                                {
                                    strHAIKI_SHURUINm = HAIKI_SHURUIinfo[1]["HAIKI_SHURUI_NAME"].ToString();
                                }
                                else
                                    strHAIKI_SHURUINm = strHAIKI_SHURUINm + "、" + HAIKI_SHURUIinfo[1]["HAIKI_SHURUI_NAME"].ToString();
                            }
                            if (HAIKI_SHURUIinfo.Length > 2 && HAIKI_SHURUIinfo[2] != null)
                            {
                                if (string.IsNullOrEmpty(strHAIKI_SHURUINm))
                                {
                                    strHAIKI_SHURUINm = HAIKI_SHURUIinfo[2]["HAIKI_SHURUI_NAME"].ToString();
                                }
                                else
                                    strHAIKI_SHURUINm = strHAIKI_SHURUINm + "、" + HAIKI_SHURUIinfo[2]["HAIKI_SHURUI_NAME"].ToString();
                            }
                            if (HAIKI_SHURUIinfo.Length > 3 && HAIKI_SHURUIinfo[3] != null)
                            {
                                if (string.IsNullOrEmpty(strHAIKI_SHURUINm))
                                {
                                    strHAIKI_SHURUINm = HAIKI_SHURUIinfo[3]["HAIKI_SHURUI_NAME"].ToString();
                                }
                                else
                                    strHAIKI_SHURUINm = strHAIKI_SHURUINm + "、" + HAIKI_SHURUIinfo[3]["HAIKI_SHURUI_NAME"].ToString();
                            }
                            if (HAIKI_SHURUIinfo.Length > 4 && HAIKI_SHURUIinfo[4] != null)
                            {
                                if (string.IsNullOrEmpty(strHAIKI_SHURUINm))
                                {
                                    strHAIKI_SHURUINm = HAIKI_SHURUIinfo[4]["HAIKI_SHURUI_NAME"].ToString();
                                }
                                else
                                    strHAIKI_SHURUINm = strHAIKI_SHURUINm + "、" + HAIKI_SHURUIinfo[4]["HAIKI_SHURUI_NAME"].ToString();
                            }
                        }

                        dtRow["HAIKI_SHURUI_NAME"] = strHAIKI_SHURUINm;
                    }
                }
                //ソート順（交付年月日、交付番号、システムId）
                DataView obj = Data.DefaultView;
                obj.Sort = "KOUFU_DATE,MANIFEST_ID,SYSTEM_ID";
                //DataTable objData = obj.ToTable();
                //Distinctをかける
                DataTable objData = obj.ToTable(true, "SYSTEM_ID", "SEQ", "HST_GYOUSHA_CD", "HST_GENBA_CD", "HST_GYOUSHA_NAME1", "HST_GYOUSHA_NAME2",
                                               "HST_GENBA_NAME1", "HST_GENBA_NAME2",
                                               "HAIKI_SHURUI_NAME", "KOUFU_DATE", "MANIFEST_ID",
                                              "SEND_A",
                                              "SEND_B1",
                                              "SEND_B2",
                                              "SEND_B4",
                                              "SEND_B6",
                                              "SEND_C1",
                                              "SEND_C2",
                                              "SEND_D",
                                              "SEND_E");
                objData = this.GetGenbaGRDData(objData);
                return objData;
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
        // 20140623 kayo 不具合#4972 アラート件数判断不正 end

        /// <summary>
        /// 返却先集計検索結果を画面に設定
        /// </summary>
        public bool SetDataForHenkyakusakiShukeiGRD()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                CustomDataGridView objGrid = this.form.HenkyakusakiShukeiGRD;
                if (objGrid != null)
                {
                    // 明細クリア
                    objGrid.Rows.Clear();
                }

                if (this.searchHenkyakusakiShukeiData == null || !this.CheckDisplayCount(this.searchHenkyakusakiShukeiData.Rows.Count))
                {
                    LogUtility.DebugMethodStart(ret);
                    return ret;
                }

                objGrid.IsBrowsePurpose = false;

                DataTable objTempData = this.searchHenkyakusakiShukeiData.AsDataView().ToTable();
                objTempData.Columns.Add("MANI_HENSOUSAKI_SORT");
                foreach (DataRow dt in objTempData.Rows)
                {
                    // 返送先表示名称
                    string ManiHensousakiSort = string.Empty;
                    string tempName = this.ChgDBNullToValue(dt["MANI_HENSOUSAKI_CD_NAME"], string.Empty).ToString();
                    if (!string.IsNullOrEmpty(tempName))
                    {
                        string[] name = tempName.Split(',');
                        switch (name[name.Length - 1])
                        {
                            case "返送先取引先":
                                DataRow[] RowData = this.searchAllIchiranDetailData.Select(" MANI_HENSOUSAKI_TORIHIKISAKI_CD = '" + name[0] + "'");
                                if (RowData != null && RowData.Length > 0)
                                {
                                    ManiHensousakiSort = name[0];
                                }
                                break;

                            case "返送先業者":
                                DataRow[] RowData1 = this.searchAllIchiranDetailData.Select(" MANI_HENSOUSAKI_GYOUSHA_CD = '" + name[0] + "' AND MANI_HENSOUSAKI_GENBA_CD IS NULL ");
                                if (RowData1 != null && RowData1.Length > 0)
                                {
                                    ManiHensousakiSort = "AAAAAA" + name[0];
                                }
                                break;

                            case "返送先現場":
                                DataRow[] RowData2 = this.searchAllIchiranDetailData.Select(" MANI_HENSOUSAKI_GENBA_CD = '" + name[0] + "'");
                                if (RowData2 != null && RowData2.Length > 0)
                                {
                                    ManiHensousakiSort = "AAAAAAAAAAAA" + name[0];
                                }
                                break;

                            default:
                                ManiHensousakiSort = "AAAAAAAAAAAAAAAAAAAAAAAA";
                                break;
                        }
                    }
                    dt["MANI_HENSOUSAKI_SORT"] = ManiHensousakiSort;
                }

                //総Detail取得
                DataView dvSort = objTempData.AsDataView();
                dvSort.Sort = "KOUFU_DATE,MANI_HENSOUSAKI_SORT,MANI_HENSOUSAKI";
                DataTable objData = dvSort.ToTable();

                // 検索結果
                objData.BeginLoadData();

                if (objData.Rows.Count > 0)
                {
                    // 明細行を追加
                    objGrid.Rows.Add(objData.Rows.Count);
                    // 検索結果設定
                    for (int j = 0; j < objData.Rows.Count; j++)
                    {
                        DataRow dtRow = objData.Rows[j];
                        // 行番号
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.ROW_NO, j].Value = j + 1;
                        // 返却日
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.REF_DATE, j].Value = this.ChgDBNullToValue(dtRow["KOUFU_DATE"], string.Empty);
                        // 返送先表示名称
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.HENSOUSAKI_NAME, j].Value = this.ChgDBNullToValue(dtRow["MANI_HENSOUSAKI"], string.Empty);
                        // A票
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.SEND_A, j].Value = this.ChgDBNullToValue(dtRow["M_SEND_A"], string.Empty);
                        // B1票
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B1, j].Value = this.ChgDBNullToValue(dtRow["M_SEND_B1"], string.Empty);
                        // B2票
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B2, j].Value = this.ChgDBNullToValue(dtRow["M_SEND_B2"], string.Empty);
                        // B4票
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B4, j].Value = this.ChgDBNullToValue(dtRow["M_SEND_B4"], string.Empty);
                        // B6票
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.SEND_B6, j].Value = this.ChgDBNullToValue(dtRow["M_SEND_B6"], string.Empty);
                        // C1票
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.SEND_C1, j].Value = this.ChgDBNullToValue(dtRow["M_SEND_C1"], string.Empty);
                        // C2票
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.SEND_C2, j].Value = this.ChgDBNullToValue(dtRow["M_SEND_C2"], string.Empty);
                        // D票
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.SEND_D, j].Value = this.ChgDBNullToValue(dtRow["M_SEND_D"], string.Empty);
                        // E票
                        objGrid[ConstCls.HenkyakusakiShukeiGRDColName.SEND_E, j].Value = this.ChgDBNullToValue(dtRow["M_SEND_E"], string.Empty);
                    }
                }

                objGrid.IsBrowsePurpose = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDataForHenkyakusakiShukeiGRD", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 返送先設定処理
        /// </summary>
        /// <param name="count"></param>
        /// <param name="maniHensousakiKbnName"></param>
        public int SetManiHensousakiName(int count, out string maniHensousakiKbnName)
        {
            int retValue = 0;
            maniHensousakiKbnName = string.Empty;

            if (this.searchAllIchiranDetailData == null || this.searchAllIchiranDetailData.Rows.Count <= 0)
            {
                return retValue;
            }
            //DataView dvSort = this.searchAllIchiranDetailData.AsDataView();
            //dvSort.Sort = "MANI_HENSOUSAKI_SORT";
            //this.searchAllIchiranDetailData = dvSort.ToTable();
            //返却先情報GROUP BY
            var ManiHensousakiNameInfo = this.searchAllIchiranDetailData.AsEnumerable().GroupBy(r => string.Format("{0},{1}", r.Field<string>("MANI_HENSOUSAKI_NAME_KBN"), r.Field<string>("MANI_HENSOUSAKI_NAME")),
                      (k, g) => new
                      {
                          MANI_HENSOUSAKI_NAME_KBN = g.First().Field<string>("MANI_HENSOUSAKI_NAME_KBN"),
                          MANI_HENSOUSAKI_NAME = g.First().Field<string>("MANI_HENSOUSAKI_NAME"),
                          MANI_HENSOUSAKI_TORIHIKISAKI_NAME_RYAKU = g.First().Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_NAME_RYAKU"),
                          MANI_HENSOUSAKI_GYOUSHA_NAME_RYAKU = g.First().Field<string>("MANI_HENSOUSAKI_GYOUSHA_NAME_RYAKU"),
                          MANI_HENSOUSAKI_GENBA_NAME_RYAKU = g.First().Field<string>("MANI_HENSOUSAKI_GENBA_NAME_RYAKU"),
                          MANI_HENSOUSAKI_TORIHIKISAKI_CD = g.First().Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD"),
                          MANI_HENSOUSAKI_GYOUSHA_CD = g.First().Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD"),
                          MANI_HENSOUSAKI_GENBA_CD = g.First().Field<string>("MANI_HENSOUSAKI_GENBA_CD"),
                          MANI_HENSOUSAKI_NAME1 = g.First().Field<string>("MANI_HENSOUSAKI_NAME1"),
                      }).ToList();
            if (ManiHensousakiNameInfo.Count > count && count >= 0)
            {
                //第一の返送先名称設定
                this.form.txtManiHensousakiName.Tag = ManiHensousakiNameInfo.Count > 0 && ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_NAME == null ? string.Empty : ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_NAME.ToString();

                if (!string.IsNullOrEmpty(this.form.txtManiHensousakiName.Tag.ToString()))
                {
                    //表示名称
                    string ManiHensousakiName = string.Empty;
                    string ManiHensousakiCd = string.Empty;
                    if (!string.IsNullOrEmpty(ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_GENBA_CD))
                    {
                        ManiHensousakiName = ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_NAME1;
                        ManiHensousakiCd = "業者CD：" + ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_GYOUSHA_CD + ", 現場CD："+ ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_GENBA_CD;
                    }
                    else if (!string.IsNullOrEmpty(ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_GYOUSHA_CD)
                        && string.IsNullOrEmpty(ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_GENBA_CD))
                    {
                        ManiHensousakiName = ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_NAME1;
                        ManiHensousakiCd = "業者CD：" + ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_GYOUSHA_CD;
                    }
                    else if (!string.IsNullOrEmpty(ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_TORIHIKISAKI_CD))
                    {
                        ManiHensousakiName = ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_NAME1;
                        ManiHensousakiCd = "取引先CD：" + ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                    }
                    else
                    {
                        ManiHensousakiName = this.form.txtManiHensousakiName.Tag.ToString();
                        ManiHensousakiCd = string.Empty;
                    }

                    if (string.IsNullOrEmpty(ManiHensousakiCd))
                    {
                        this.form.txtManiHensousakiName.Text = ManiHensousakiName;
                    }
                    else
                    {
                        this.form.txtManiHensousakiName.Text = ManiHensousakiCd + "　" + ManiHensousakiName;
                    }
                }
                else
                {
                    this.form.txtManiHensousakiName.Tag = string.Empty;
                    this.form.txtManiHensousakiName.Text = string.Empty;
                    this.form.txtManiHensousakiName.Text = ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_NAME1;
                }
                maniHensousakiKbnName = ManiHensousakiNameInfo[count].MANI_HENSOUSAKI_NAME_KBN;
                retValue = ManiHensousakiNameInfo.Count;
            }

            return retValue;
        }

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToDateTimeValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else if (string.IsNullOrEmpty(obj.ToString()))
            {
                return value;
            }
            else
            {
                return obj.ToString();//((DateTime)obj).ToShortDateString();
            }
        }

        /// <summary>
        /// DateTime TO Count数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private int GetDateTimeCount(object obj, int value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else if (string.IsNullOrEmpty(obj.ToString()))
            {
                return value;
            }
            else if (obj.ToString().Equals("-"))
            {
                return value;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// クリアー詳細データ情報
        /// </summary>
        public void ClearDetailData()
        {
            this.form.KohuBangoGRD.Rows.Clear();
            this.form.HenkyakusakiShukeiGRD.Rows.Clear();
            this.form.GenbaGRD.Rows.Clear();
            //第一の返送先名称設定
            this.form.txtManiHensousakiName.Text = string.Empty;
            this.form.txtManiHensousakiName.Tag = string.Empty;
            //返送先総件数
            this.form.txtAllManiHensousakiCount.Text = "0";
            this.form.txtAllManiHensousakiCount.Tag = "0";
            //今表示の返送先数設定
            this.form.txtManiHensousakiCount.Text = "0";
            this.form.txtManiHensousakiCount.Tag = "0";
            //読込データ件数設定
            this.headForm.readDataNumber.Text = "0";
            this.searchAllIchiranDetailData = null;

            if (this.searchKohuBangoDataByTorisaki != null)
            {
                this.searchKohuBangoDataByTorisaki.Clear();
            }

            if (this.searchGenbaDataByTorisaki != null)
            {
                this.searchGenbaDataByTorisaki.Clear();
            }

            if (this.searchAllIchiranDetailData != null)
            {
                this.searchAllIchiranDetailData.Clear();
            }

            if (this.searchHenkyakusakiShukeiData != null)
            {
                this.searchHenkyakusakiShukeiData.Clear();
            }
        }

        #endregion

        #region [F12]閉じる

        /// <summary>
        /// [F12]閉じる 処理
        /// </summary>
        internal void CloseForm()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.form.Parent;

                this.form.Close();
                parentForm.Close();
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

        #region  現場毎明細のレイアウト位置調整

        /// <summary>
        /// 明細のレイアウト調整
        /// 非表示にしたコントロールが空白で表示されるため調整する
        /// </summary>
        internal bool ExecuteAlignmentForDetail(bool VisibleFlg, string strInsatuKubunCD)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(VisibleFlg, strInsatuKubunCD);

                this.form.GenbaGRD.SuspendLayout();
                var newTemplate = this.form.GenbaGRD.Template;

                // B1票
                var B1Hedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader5"];
                var B1Cell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_B1];

                // B4票
                var B4Hedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader7"];
                var B4Cell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_B4];
                //  B6票
                var B6Hedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader8"];
                var B6Cell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_B6];
                int chengeHedaderWidth = 0;
                int chengeCellWidth = 0;

                if (strInsatuKubunCD.Equals("1") && B4Hedader.Visible != VisibleFlg)
                {
                    //非表示
                    B4Hedader.Visible = false;
                    B4Cell.Visible = false;
                    B6Hedader.Visible = false;
                    B6Cell.Visible = false;
                }
                else if (strInsatuKubunCD.Equals("2"))
                {
                    B1Hedader.Visible = false;
                }
                else
                {
                    return ret;
                }

                // 位置調整
                if (VisibleFlg)
                {
                    chengeHedaderWidth = B1Hedader.Size.Width;
                    chengeCellWidth = B1Hedader.Size.Width;

                    // B2票
                    var B2Hedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader6"];
                    var B2Cell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_B2];
                    B2Hedader.Location = new Point(B2Hedader.Location.X - chengeHedaderWidth, B2Hedader.Location.Y);
                    B2Cell.Location = new Point(B2Cell.Location.X - chengeCellWidth, B2Cell.Location.Y);

                    // B4票
                    B4Hedader.Location = new Point(B2Hedader.Location.X + B2Hedader.Size.Width, B4Hedader.Location.Y);
                    B4Cell.Location = new Point(B2Cell.Location.X + B2Hedader.Size.Width, B4Cell.Location.Y);
                    // B6票
                    B6Hedader.Location = new Point(B4Hedader.Location.X + B4Hedader.Size.Width, B6Hedader.Location.Y);
                    B6Cell.Location = new Point(B4Cell.Location.X + B4Cell.Size.Width, B6Cell.Location.Y);

                    // C1票
                    var C1Hedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader9"];
                    var C1Cell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_C1];
                    C1Hedader.Location = new Point(B6Hedader.Location.X + B6Hedader.Size.Width, C1Hedader.Location.Y);
                    C1Cell.Location = new Point(B6Cell.Location.X + B6Cell.Size.Width, C1Cell.Location.Y);
                    // C2票
                    var C2Hedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader10"];
                    var C2Cell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_C2];
                    C2Hedader.Location = new Point(C1Hedader.Location.X + C1Hedader.Size.Width, C2Hedader.Location.Y);
                    C2Cell.Location = new Point(C1Cell.Location.X + C1Cell.Size.Width, C2Cell.Location.Y);
                    // D票
                    var DHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader11"];
                    var DCell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_D];
                    DHedader.Location = new Point(C2Hedader.Location.X + C2Hedader.Size.Width, DHedader.Location.Y);
                    DCell.Location = new Point(C2Cell.Location.X + C2Cell.Size.Width, DCell.Location.Y);
                    // E票
                    var EHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader12"];
                    var ECell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_E];
                    EHedader.Location = new Point(DHedader.Location.X + DHedader.Size.Width, EHedader.Location.Y);
                    ECell.Location = new Point(DCell.Location.X + DHedader.Size.Width, ECell.Location.Y);

                    // 排出現場
                    var Hst_GenBaHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader02"];
                    var Hst_Gen1BaCell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.HST_GENBA_CD1];
                    var Hst_Gen2BaCell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.HST_GENBA_CD2];
                    Hst_GenBaHedader.Size = new Size(Hst_GenBaHedader.Width - chengeHedaderWidth, Hst_GenBaHedader.Height);
                    Hst_Gen1BaCell.Size = new Size(Hst_Gen1BaCell.Width - chengeHedaderWidth, Hst_Gen1BaCell.Height);
                    Hst_Gen2BaCell.Size = new Size(Hst_Gen2BaCell.Width - chengeHedaderWidth, Hst_Gen2BaCell.Height);
                    // 廃棄物種類
                    var HAIKI_KBNHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader03"];
                    var HAIKI_KBNBaCell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.HAIKI_SHURUI_CD];
                    HAIKI_KBNHedader.Size = new Size(HAIKI_KBNHedader.Width - chengeHedaderWidth, HAIKI_KBNHedader.Height);
                    HAIKI_KBNBaCell.Size = new Size(HAIKI_KBNBaCell.Width - chengeHedaderWidth, HAIKI_KBNBaCell.Height);

                    this.form.GenbaGRD.Template.Width = this.form.GenbaGRD.Template.Width - chengeHedaderWidth;
                }
                else
                {
                    chengeHedaderWidth = B4Hedader.Size.Width + B6Hedader.Size.Width;
                    chengeCellWidth = B4Hedader.Size.Width + B6Hedader.Size.Width;

                    // C1票
                    var C1Hedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader9"];
                    var C1Cell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_C1];
                    C1Hedader.Location = new Point(C1Hedader.Location.X - chengeHedaderWidth, C1Cell.Location.Y);
                    C1Cell.Location = new Point(C1Cell.Location.X - chengeHedaderWidth, C1Cell.Location.Y);
                    // C2票
                    var C2Hedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader10"];
                    var C2Cell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_C2];
                    C2Hedader.Location = new Point(C1Hedader.Location.X + C1Hedader.Size.Width, C2Hedader.Location.Y);
                    C2Cell.Location = new Point(C1Cell.Location.X + C1Cell.Size.Width, C2Cell.Location.Y);
                    // D票
                    var DHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader11"];
                    var DCell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_D];
                    DHedader.Location = new Point(C2Hedader.Location.X + C2Hedader.Size.Width, DHedader.Location.Y);
                    DCell.Location = new Point(C2Cell.Location.X + C2Cell.Size.Width, DCell.Location.Y);
                    // E票
                    var EHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader12"];
                    var ECell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.SEND_E];
                    EHedader.Location = new Point(DHedader.Location.X + DHedader.Size.Width, EHedader.Location.Y);
                    ECell.Location = new Point(DCell.Location.X + DHedader.Size.Width, ECell.Location.Y);

                    // 排出現場
                    var Hst_GenBaHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader02"];
                    var Hst_Gen1BaCell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.HST_GENBA_CD1];
                    var Hst_Gen2BaCell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.HST_GENBA_CD2];
                    Hst_GenBaHedader.Size = new Size(Hst_GenBaHedader.Width - chengeHedaderWidth, Hst_GenBaHedader.Height);
                    Hst_Gen1BaCell.Size = new Size(Hst_Gen1BaCell.Width - chengeHedaderWidth, Hst_Gen1BaCell.Height);
                    Hst_Gen2BaCell.Size = new Size(Hst_Gen2BaCell.Width - chengeHedaderWidth, Hst_Gen2BaCell.Height);
                    // 廃棄物種類
                    var HAIKI_SHURUIHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader03"];
                    var HAIKI_SHURUICell = newTemplate.Row.Cells[ConstCls.GenbaGRDColName.HAIKI_SHURUI_CD];
                    HAIKI_SHURUIHedader.Size = new Size(HAIKI_SHURUIHedader.Width - chengeHedaderWidth, HAIKI_SHURUIHedader.Height);
                    HAIKI_SHURUICell.Size = new Size(HAIKI_SHURUICell.Width - chengeHedaderWidth, HAIKI_SHURUICell.Height);

                    this.form.GenbaGRD.Template.Width = this.form.GenbaGRD.Template.Width - chengeHedaderWidth;
                }

                this.form.GenbaGRD.Template = newTemplate;

                this.form.GenbaGRD.ResumeLayout();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExecuteAlignmentForDetail", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region  明細表示、非表示チェック
        // 20140623 kayo 不具合#4972 アラート件数判断不正 start
        /// <summary>
        /// 明細表示、非表示チェック
        /// </summary>
        /// <param name="count">明細行数</param>
        /// <returns>true:明細要 false：明細表示不要</returns>
        internal bool CheckDisplayCount(int count)
        {
            bool isDisplay = true;
            DialogResult result = DialogResult.Yes;
            string strAlertCount = this.headForm.alertNumber.Text.ToString().Replace(",", "");
            if (!string.IsNullOrEmpty(strAlertCount) && !strAlertCount.Equals("0") && int.Parse(strAlertCount) < count)
            {
                //検索件数がアラート件数を超えました。<br>表示を行いますか？
                result = this.messageShowLogic.MessageBoxShow("C025");
            }
            if (result != DialogResult.Yes)
            {
                isDisplay = false;
            }

            return isDisplay;
        }
        // 20140623 kayo 不具合#4972 アラート件数判断不正 start
        #endregion

        // 20141022 Houkakou 「返送案内書」の日付チェックを追加する start
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
                this.form.txtHinnkyakuDateFROM.BackColor = Constans.NOMAL_COLOR;
                this.form.txtHinnkyakuDateTO.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.txtHinnkyakuDateFROM.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.txtHinnkyakuDateTO.Text))
                {
                    return isErr;
                }

                DateTime date_from = DateTime.Parse(this.form.txtHinnkyakuDateFROM.Text);
                DateTime date_to = DateTime.Parse(this.form.txtHinnkyakuDateTO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.txtHinnkyakuDateFROM.IsInputErrorOccured = true;
                    this.form.txtHinnkyakuDateTO.IsInputErrorOccured = true;
                    this.form.txtHinnkyakuDateFROM.BackColor = Constans.ERROR_COLOR;
                    this.form.txtHinnkyakuDateTO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "返却日From", "返却日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.txtHinnkyakuDateFROM.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }

        #endregion
        // 20141022 Houkakou 「返送案内書」の日付チェックを追加する end

        /// <summary>
        /// 抽出範囲項目の指定チェック
        /// </summary>
        /// <returns></returns>
        internal string CheckErr(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            string errMsg = string.Empty;
            catchErr = false;
            try
            {
                if (!this.CheckCodeFromTo(this.form.txtHaishutsuJigyoushaCd, this.form.txtHaishutsuJigyoushaCdTO))
                {
                    errMsg = "排出事業者";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.form.txtHaishutsuGenbaCd, this.form.txtHaishutsuGenbaCdTO))
                {
                    errMsg = "排出事業場";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.form.txtTORIHIKISAKI_CD, this.form.txtTORIHIKISAKI_CD_TO))
                {
                    errMsg = "返送先取引先";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.form.txtGYOUSHA_CD, this.form.txtGYOUSHA_CD_TO))
                {
                    errMsg = "返送先業者";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.form.txtGENBA_CD, this.form.txtGENBA_CD_TO))
                {
                    errMsg = "返送先現場";
                    return errMsg;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckErr", ex);
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(errMsg, catchErr);
            }
            return errMsg;
        }

        /// <summary>
        /// 各CDのFromToの関係をチェック
        /// </summary>
        /// <param name="TextFrom"></param>
        /// <param name="TextTo"></param>
        /// <returns></returns>
        private bool CheckCodeFromTo(CustomAlphaNumTextBox TextFrom, CustomAlphaNumTextBox TextTo)
        {
            LogUtility.DebugMethodStart(TextFrom, TextTo);

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

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        // 20141128 Houkakou 「返送案内書」のダブルクリックを追加する start
        #region txtHinnkyakuDateTOダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHinnkyakuDateTO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.txtHinnkyakuDateFROM;
            var ToTextBox = this.form.txtHinnkyakuDateTO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region txtHaishutsuJigyoushaCdTOダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHaishutsuJigyoushaCdTO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.txtHaishutsuJigyoushaCd;
            var ToTextBox = this.form.txtHaishutsuJigyoushaCdTO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.txtHaishutsuJigyoushaNmTO.Text = this.form.txtHaishutsuJigyoushaNm.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region txtHaishutsuGenbaCdTOダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHaishutsuGenbaCdTO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.txtHaishutsuGenbaCd;
            var ToTextBox = this.form.txtHaishutsuGenbaCdTO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.txtHaishutsuGenbaNmTO.Text = this.form.txtHaishutsuGenbaNm.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region txtTORIHIKISAKI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.txtTORIHIKISAKI_CD;
            var ToTextBox = this.form.txtTORIHIKISAKI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.txtTORIHIKISAKI_NAME_TO.Text = this.form.txtTORIHIKISAKI_NAME.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region txtGYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.txtGYOUSHA_CD;
            var ToTextBox = this.form.txtGYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.txtGYOUSHA_NAME_TO.Text = this.form.txtGYOUSHA_NAME.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region txtGENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.txtGENBA_CD;
            var ToTextBox = this.form.txtGENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.txtGENBA_NAME_TO.Text = this.form.txtGENBA_NAME.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion
        // 20141128 Houkakou 「返送案内書」のダブルクリックを追加する end

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
        #region MOD NHU 20211004 #155770
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
                this.headForm.KYOTEN_CD.Text = config.ItemSetVal1.PadLeft(2, '0');
                this.headForm.beforeKyotenCd = this.headForm.KYOTEN_CD.Text;
                this.headForm.HAKKOU_KYOTEN_CD.Text = this.headForm.KYOTEN_CD.Text;
            }

            this.headForm.KYOTEN_NAME.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
            {
                var kyoten = this.kyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                if (null != kyoten)
                {
                    this.headForm.KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;
                    this.headForm.HAKKOU_KYOTEN_NAME.Text = this.headForm.KYOTEN_NAME.Text;
                }
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheck()
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                var allControlAndHeaderControls = this.form.controlUtil.GetAllControls(this.headForm);
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (this.form.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.headForm.KYOTEN_CD.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchCheck", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }
        #endregion
        #region

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

        //public int Search()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}