using System;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Accessor;

namespace Shougun.Core.BusinessManagement.DenshiShinseiIchiran
{
    /// <summary>
    /// G280 申請一覧ロジック
    /// </summary>
    internal class DenshiShinseiIchiranLogic : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.BusinessManagement.DenshiShinseiIchiran.Setting.DenshiShinseiIchiranButtonSetting.xml";

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private HeaderBaseForm header;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private DenshiShinseiIchiranUIForm form;

        /// <summary>
        /// 画面のデータを保持するDTOを取得・設定します
        /// </summary>
        internal DenshiShinseiIchiranDto Dto { get; set; }

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public DenshiShinseiIchiranLogic(DenshiShinseiIchiranUIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.Dto = new DenshiShinseiIchiranDto();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダーを初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                var ribbon = (RibbonMainMenu)((BusinessBaseForm)this.form.Parent).ribbonForm;
                this.Dto.ShainCd = ribbon.GlobalCommonInformation.CurrentShain.SHAIN_CD;
                this.Dto.ShainName = ribbon.GlobalCommonInformation.CurrentShain.SHAIN_NAME_RYAKU;

                if (!this.SetHeaderInitData()) { ret = false; }
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

        /// <summary>
        /// ヘッダ初期データを設定します
        /// </summary>
        public bool SetHeaderInitData()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var configProfile = new XMLAccessor().XMLReader_CurrentUserCustomConfigProfile();
                this.Dto.KyotenCd = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));

                var kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                var kyoten = kyotenDao.GetDataByCd(configProfile.ItemSetVal1);
                if (null != kyoten)
                {
                    this.Dto.KyotenName = kyoten.KYOTEN_NAME_RYAKU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeaderInitData", ex);
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
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            HeaderBaseForm targetHeader = (HeaderBaseForm)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

            DenshiShinseiIchiranUIHeaderForm ichiranHeaderForm = parentForm.headerForm as DenshiShinseiIchiranUIHeaderForm;
            if (ichiranHeaderForm != null)
            {
                // タイトルの横幅調整（※横幅の最大値を拠点ラベルのロケーションで設定）
                ControlUtility.AdjustTitleSize(this.header.lb_title, ichiranHeaderForm.label1.Location.X);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定を作成します
        /// </summary>
        /// <returns>ボタン設定</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            ButtonSetting[] ret;

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            ret = buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// ボタンを初期化します
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 内容確認(F3)イベント追加
            parentForm.bt_func3.Click += new EventHandler(this.form.ButtonFunc3_Clicked);

            // 条件取消ボタン(F7)イベント追加
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

            // 検索ボタン(F8)イベント追加
            parentForm.bt_func8.Click += new EventHandler(this.form.ButtonFunc8_Clicked);

            // 申請表示ボタン(F9)イベント追加
            parentForm.bt_func9.Click += new EventHandler(this.form.ButtonFunc9_Clicked);

            // 並び替えボタン(F10)イベント追加
            parentForm.bt_func10.Click += new EventHandler(this.form.ButtonFunc10_Clicked);

            // 閉じるボタン(F12)イベント追加
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 申請対象のマスタ情報を表示する。
        /// </summary>
        public void ShowReferenceDisplay()
        {
            try
            {
                var row = this.form.DENSHI_SHINSEI_ICHIRAN.CurrentRow;
                if (row == null)
                {
                    return;
                }

                if ((row.Cells["HIKIAI_GENBA_CD"].Value != null && !string.IsNullOrEmpty(row.Cells["HIKIAI_GENBA_CD"].FormattedValue.ToString()))
                    || (row.Cells["GENBA_CD"].Value != null && !string.IsNullOrEmpty(row.Cells["GENBA_CD"].FormattedValue.ToString())))
                {
                    bool hikiaFlg = false;
                    string gyoushaCd = string.Empty;
                    string genbaCd = string.Empty;
                    bool useKariData = false;

                    if (row.Cells["HIKIAI_GYOUSHA_USE_FLG"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["HIKIAI_GYOUSHA_USE_FLG"].FormattedValue.ToString()))
                    {
                        if (row.Cells["HIKIAI_GYOUSHA_USE_FLG"].Value.ToString() == "1")
                        {
                            hikiaFlg = true;
                        }
                        else
                        {
                            hikiaFlg = false;
                        }
                    }
                    // 現場
                    if (row.Cells["HIKIAI_GENBA_CD"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["HIKIAI_GENBA_CD"].FormattedValue.ToString())
                        && row.Cells["HIKIAI_GYOUSHA_CD"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["HIKIAI_GYOUSHA_CD"].FormattedValue.ToString())
                        && row.Cells["GYOUSHA_CD"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["GYOUSHA_CD"].FormattedValue.ToString()))
                    {
                        // 引合現場(本登録後 引合業者)
                        //hikiaFlg = false;
                        gyoushaCd = row.Cells["GYOUSHA_CD"].FormattedValue.ToString();
                        genbaCd = row.Cells["HIKIAI_GENBA_CD"].FormattedValue.ToString();
                    }
                    else if (row.Cells["HIKIAI_GENBA_CD"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["HIKIAI_GENBA_CD"].FormattedValue.ToString())
                        && row.Cells["HIKIAI_GYOUSHA_CD"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["HIKIAI_GYOUSHA_CD"].FormattedValue.ToString()))
                    {
                        // 引合現場(引合業者)
                        //hikiaFlg = true;
                        gyoushaCd = row.Cells["HIKIAI_GYOUSHA_CD"].FormattedValue.ToString();
                        genbaCd = row.Cells["HIKIAI_GENBA_CD"].FormattedValue.ToString();
                    }
                    else if (row.Cells["HIKIAI_GENBA_CD"].Value != null
                            && !string.IsNullOrEmpty(row.Cells["HIKIAI_GENBA_CD"].FormattedValue.ToString())
                            && row.Cells["GYOUSHA_CD"].Value != null
                            && !string.IsNullOrEmpty(row.Cells["GYOUSHA_CD"].FormattedValue.ToString()))
                    {
                        // 引合現場(既存業者)
                        //hikiaFlg = false;
                        gyoushaCd = row.Cells["GYOUSHA_CD"].FormattedValue.ToString();
                        genbaCd = row.Cells["HIKIAI_GENBA_CD"].FormattedValue.ToString();
                    }
                    else if (row.Cells["GENBA_CD"].Value != null
                            && !string.IsNullOrEmpty(row.Cells["GENBA_CD"].FormattedValue.ToString()))
                    {
                        // 既存現場
                        gyoushaCd = row.Cells["GYOUSHA_CD"].FormattedValue.ToString();
                        genbaCd = row.Cells["GENBA_CD"].FormattedValue.ToString();
                        useKariData = true;
                    }
                    FormManager.OpenForm("G614", WINDOW_TYPE.NONE, hikiaFlg, gyoushaCd, genbaCd, useKariData);
                }
                else if (row.Cells["HIKIAI_GYOUSHA_CD"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["HIKIAI_GYOUSHA_CD"].FormattedValue.ToString()))
                {
                    // 引合業者
                    FormManager.OpenForm("G613", WINDOW_TYPE.NONE, row.Cells["HIKIAI_GYOUSHA_CD"].FormattedValue.ToString(), 1);
                }
                else if (row.Cells["GYOUSHA_CD"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["GYOUSHA_CD"].FormattedValue.ToString()))
                {
                    // 既存業者
                    FormManager.OpenForm("G613", WINDOW_TYPE.NONE, row.Cells["GYOUSHA_CD"].FormattedValue.ToString(), 0);
                }
                else if (row.Cells["HIKIAI_TORIHIKISAKI_CD"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["HIKIAI_TORIHIKISAKI_CD"].FormattedValue.ToString()))
                {
                    // 引合取引先
                    FormManager.OpenForm("G612", WINDOW_TYPE.NONE, row.Cells["HIKIAI_TORIHIKISAKI_CD"].FormattedValue.ToString(), 1);
                }
                else if (row.Cells["TORIHIKISAKI_CD"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["TORIHIKISAKI_CD"].FormattedValue.ToString()))
                {
                    // 引合取引先
                    FormManager.OpenForm("G612", WINDOW_TYPE.NONE, row.Cells["TORIHIKISAKI_CD"].FormattedValue.ToString(), 0);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowReferenceDisplay", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// データ抽出処理を行います
        /// </summary>
        /// <returns>抽出したデータの件数</returns>
        public int Search()
        {
            var ret = 0;
            try
            {
                LogUtility.DebugMethodStart();

                if (false == this.form.RegistErrorFlag)
                {
                    var dao = DaoInitUtility.GetComponent<IDenshiShinseiIchiranDao>();
                    var res = dao.GetDenshiShinseiIchiran(this.Dto);

                    this.form.SetDenshiShinseiIchiranDataSource(res);
                    ret = res.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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
    }
}
