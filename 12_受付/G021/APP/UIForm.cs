// $Id: UIForm.cs 55371 2015-07-10 11:07:15Z t-thanhson@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Drawing;
using r_framework.Entity;
using Seasar.Framework.Exceptions;
using System.Linq;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using r_framework.Configuration;

namespace Shougun.Core.Reception.UketukeiIchiran
{
    [Implementation]
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド

        /// <summary>
        /// ロジック
        /// </summary>
        private UketukeiIchiran.LogicCls IchiranLogic;

        /// <summary>
        /// UIHeader
        /// </summary>
        private UIHeaderForm header_new;

        private string msgNioGyo = "荷降業者";
        private string msgNioGen = "荷降現場";
        /// <summary>
        /// コントロールFocus時値格納
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// 画面情報初期化フラグ
        /// </summary>
        private Boolean isLoaded;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region メソッド

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="KYOTEN_CD"></param>
        /// <param name="Num_DenPyouSyurui"></param>
        public UIForm(string KYOTEN_CD, string Num_DenPyouSyurui)
            : base(DENSHU_KBN.UKETSUKE_SS_ICHIRAN, false)
        {
            try
            {
                LogUtility.DebugMethodStart(KYOTEN_CD, Num_DenPyouSyurui);
                this.InitializeComponent();
                //ロジックに、Header部情報を設定する            
                this.IchiranLogic = new LogicCls(this);
                this.IchiranLogic.strKYOTEN_CD = KYOTEN_CD;
                //2014/01/28 修正 仕様変更 qiao start
                this.IchiranLogic.strDenPyouSyurui = Num_DenPyouSyurui;
                //2014/01/28 修正 仕様変更 qiao end
                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
                //社員コードを取得すること
                this.ShainCd = SystemProperty.Shain.CD;
                //Main画面で社員コード値を取得すること
                this.IchiranLogic.syainCode = SystemProperty.Shain.CD;
                //伝種区分を取得すること
                this.IchiranLogic.denShu_Kbn = (int)DENSHU_KBN.UKETSUKE_SS_ICHIRAN;
                isLoaded = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(KYOTEN_CD, Num_DenPyouSyurui);
            }
        }
        #endregion

        #region 画面情報
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                //2012/12/18 追加 PTバグトラブル管理表_東北ICのNo1968対応 start
                this.customSortHeader1.ClearCustomSortSetting();
                //2012/12/18 追加 PTバグトラブル管理表_東北ICのNo1968対応 end
                base.OnLoad(e);
                //画面情報の初期化
                if (!isLoaded)
                {
                    if (!this.IchiranLogic.WindowInit())
                    {
                        return;
                    }
                    this.header_new = this.IchiranLogic.headForm;
                    //画面初期表示
                    this.IchiranLogic.InitFrom();
                    this.SetHeaderTitle();
                }
                this.PatternReload(!isLoaded);
                // フィルタの初期化
                this.customSearchHeader1.ClearCustomSearchSetting();

                // オプション
                if (AppConfig.AppOptions.IsMAPBOX())
                {
                    // 一覧内のチェックボックスの設定
                    this.IchiranLogic.HeaderCheckBoxSupport2();
                    this.IchiranLogic.notReadOnlyColumns();
                }
                if (AppConfig.AppOptions.IsInxsUketsuke())
                {
                    this.label3.Visible = true;
                    this.UKETSUKE_EXPORT_KBN.Visible = true;
                    this.customPanel2.Visible = true;
                }
                else
                {
                    this.label3.Visible = false;
                    this.UKETSUKE_EXPORT_KBN.Visible = false;
                    this.customPanel2.Visible = false;
                }

                //Start Sontt 20150710
                this.IchiranLogic.HeaderCheckBoxSupport();
                //End Sontt 20150710

                //共通からSQL文でDataGridViewの列名とソート順を取得する
                this.IchiranLogic.selectQuery = this.logic.SelectQeury;
                this.IchiranLogic.orderByQuery = this.logic.OrderByQuery;
                isLoaded = true;
                //base.OnLoad時にtableに設定されたヘッダー情報をグリッドに表示する
                if (!this.DesignMode && !string.IsNullOrEmpty(this.IchiranLogic.selectQuery))
                {
                    this.Table = this.logic.GetColumnHeaderOnlyDataTable();
                    if (this.Table != null)
                    {
                        this.logic.CreateDataGridView(this.Table);
                    }
                }
                //thongh 2015/10/16 #13526 start
                //読込データ件数の設定
                if (this.customDataGridView1 != null)
                {
                    this.header_new.readDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.header_new.readDataNumber.Text = "0";
                }
                //thongh 2015/10/16 #13526 end
                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(e);
            }
        }

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData(DataTable searchResult)
        {
            try
            {
                LogUtility.DebugMethodStart(searchResult);
                this.Table = searchResult;
                if (!this.DesignMode)
                {
                    //アラート件数を設定する（カンマを除く）
                    int alertCount = 0;
                    if (!string.IsNullOrEmpty(this.header_new.alertNumber.Text))
                    {
                        alertCount = int.Parse(this.header_new.alertNumber.Text.Replace(",", ""));
                    }
                    this.logic.AlertCount = alertCount;
                    //DataGridViewに値の設定を行う
                    this.logic.CreateDataGridView(this.Table);
                    //CreateGridView(searchResult);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 一覧画面再表示
        /// </summary>
        public void IchiranUpdate()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //再検索処理を行う
                this.IchiranLogic.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranUpdate", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region LostFoucs関連チェック
        /// <summary>
        /// 伝票種類LostFoucs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_DenPyouSyurui_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (string.IsNullOrEmpty(this.txtNum_DenPyouSyurui.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.txtNum_DenPyouSyurui.Focus();
                this.txtNum_DenPyouSyurui.Text = "1";
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "6");
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 取引先CD有効性チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //取引先CD
                string pToriCd = this.TORIHIKISAKI_CD.Text.ToString().Trim();
                if (pToriCd != "")
                {
                    pToriCd = pToriCd.PadLeft(6, '0');
                }
                else
                {
                    this.TORIHIKISAKI_NAME.Clear();
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool bRet = true;
                // 取引先マスタチェック
                bool catchErr = true;
                var ret = this.IchiranLogic.GetTorihikisakiInfo(pToriCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (ret.Length == 0)
                {
                    // 取引先マスタにデータ存在しない
                    this.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    this.TORIHIKISAKI_NAME.Clear();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    bRet = false;
                }
                // 締日チェック
                if (bRet)
                {
                    bool isOk = this.IchiranLogic.CheckShimebi(pToriCd, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if ((string.IsNullOrEmpty(this.cmbShimebi.Text) && string.IsNullOrEmpty(this.cmbShihariaShimebi.Text)) || isOk)
                    {
                        // ヒットするのは一件のはず
                        this.TORIHIKISAKI_NAME.Text = ret[0].TORIHIKISAKI_NAME_RYAKU;
                    }
                    else
                    {
                        this.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        this.TORIHIKISAKI_NAME.Clear();
                        msgLogic.MessageBoxShow("E062", "締日");
                        bRet = false;
                    }
                }
                //存在しない
                if (!bRet)
                {
                    this.TORIHIKISAKI_CD.SelectAll();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKISAKI_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 業者CD有効性チェック 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 業者CD
                string pGosyaCd = this.GYOUSHA_CD.Text.ToString().Trim();
                // 業者CD変更チェック
                if (!this.CheckGyoushaChange())
                {
                    return;
                }
                //取引先CD
                string pCd = this.TORIHIKISAKI_CD.Text.ToString().Trim();
                //業者情報取得
                bool catchErr = true;
                var ret = this.IchiranLogic.GetGyousyaInfo(pGosyaCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool bRet = true;
                if (ret.Length == 0)
                {
                    //業者マスタにデータ存在しない
                    this.GYOUSHA_CD.IsInputErrorOccured = true;
                    this.GYOUSHA_NAME.Clear();
                    msgLogic.MessageBoxShow("E020", "業者");
                    bRet = false;
                }
                else
                {
                    this.GYOUSHA_NAME.Text = ret[0].GYOUSHA_NAME_RYAKU;
                }
                //存在しない
                if (!bRet)
                {
                    this.GYOUSHA_CD.SelectAll();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 業者CDが変わったかチェック
        /// </summary>
        /// <returns>
        /// true = 変更あり
        /// false = 変更なし
        /// </returns>
        internal bool CheckGyoushaChange()
        {
            bool ren = true;
            try
            {
                //業者CD
                string gyoushaCd = this.GYOUSHA_CD.Text.ToString().Trim();
                if (gyoushaCd != "")
                {
                    gyoushaCd = gyoushaCd.PadLeft(6, '0');
                }
                if (this.testGYOUSHA_CD.Text != gyoushaCd)
                {
                    //前回値に比較
                    this.GENBA_CD.Text = string.Empty;
                    this.GENBA_NAME.Text = string.Empty;
                    this.testGENBA_CD.Text = string.Empty;
                    this.testGYOUSHA_CD.Text = gyoushaCd;
                    ren = true;
                }
                if (gyoushaCd == "")
                {
                    this.GYOUSHA_NAME.Clear();
                    ren = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGyoushaChange", ex1);
                this.IchiranLogic.errmessage.MessageBoxShow("E093", "");
                ren = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaChange", ex);
                this.IchiranLogic.errmessage.MessageBoxShow("E245", "");
                ren = false;
            }
            return ren;
        }

        /// <summary>
        /// 運搬業者CD有効性チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //運搬業者CD
                string pUnpanGosyaCd = this.UNPAN_GYOUSHA_CD.Text.ToString().Trim();
                if (pUnpanGosyaCd != "")
                {
                    pUnpanGosyaCd = pUnpanGosyaCd.PadLeft(6, '0');
                }
                else
                {
                    this.UNPAN_GYOUSHA_NAME.Clear();
                    return;
                }
                //運搬業者情報取得
                bool catchErr = true;
                var ret = this.IchiranLogic.GetGyousyaInfo(pUnpanGosyaCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool bRet = true;
                if (ret.Length > 0)
                {
                    //運搬業者関連チェック
                    // 20151026 BUNN #12040 STR
                    if (ret[0].UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    // 20151026 BUNN #12040 END
                    {
                        this.UNPAN_GYOUSHA_NAME.Text = ret[0].GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        this.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                        this.UNPAN_GYOUSHA_NAME.Clear();
                        msgLogic.MessageBoxShow("E062", "運搬業者");
                        bRet = false;
                    }
                }
                else
                {
                    //業者マスタにデータ存在しない
                    this.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.UNPAN_GYOUSHA_NAME.Clear();
                    msgLogic.MessageBoxShow("E020", "業者");
                    bRet = false;
                }
                //存在しない
                if (!bRet)
                {
                    this.UNPAN_GYOUSHA_CD.SelectAll();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        /// <summary>
        /// 荷降業者CD有効性チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 荷降業者CD
                string pNioroshiGosyaCd = this.NIOROSHI_GYOUSHA_CD.Text.ToString().Trim();

                // 荷降業者CD変更チェック
                if (!this.CheckNioroshiGyoushaChange())
                {
                    return;
                }
                //業者情報取得
                bool catchErr = true;
                var ret = this.IchiranLogic.GetGyousyaInfo(pNioroshiGosyaCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool bRet = true;

                if (ret.Length > 0)
                {
                    // 伝票種類に応じて荷降/荷積業者チェックをします
                    if (CheckNioroshiNizumiGyoushaCd(ret[0]))
                    {
                        this.NIOROSHI_GYOUSHA_NAME.Text = ret[0].GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        this.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = true;
                        this.NIOROSHI_GYOUSHA_NAME.Clear();
                        msgLogic.MessageBoxShow("E062", msgNioGyo);
                        bRet = false;
                    }
                }
                else
                {
                    //業者マスタにデータ存在しない
                    this.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.NIOROSHI_GYOUSHA_NAME.Clear();
                    msgLogic.MessageBoxShow("E020", "業者");
                    bRet = false;
                }
                //存在しない
                if (!bRet)
                {
                    this.NIOROSHI_GYOUSHA_CD.SelectAll();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NIOROSHI_GYOUSHA_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 荷降/荷積業者CDが変わったかチェック
        /// </summary>
        /// <returns>
        /// true = 変更あり
        /// false = 変更なし
        /// </returns>
        internal bool CheckNioroshiGyoushaChange()
        {
            bool ren = true;
            //業者CD
            string nioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text.ToString().Trim();
            if (nioroshiGyoushaCd != "")
            {
                nioroshiGyoushaCd = nioroshiGyoushaCd.PadLeft(6, '0');
            }
            if (this.testNIOROSHI_GYOUSHA_CD.Text != nioroshiGyoushaCd)
            {
                //前回値に比較
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.testNIOROSHI_GENBA_CD.Text = string.Empty;
                this.testNIOROSHI_GYOUSHA_CD.Text = nioroshiGyoushaCd;
                ren = true;
            }
            if (nioroshiGyoushaCd == "")
            {
                this.NIOROSHI_GYOUSHA_NAME.Clear();
                ren = false;
            }
            return ren;
        }

        /// <summary>
        /// 荷降/荷積業者が有効かチェックします
        /// </summary>
        /// <returns></returns>
        private bool CheckNioroshiNizumiGyoushaCd(M_GYOUSHA gyousha)
        {
            var ren = false;
            // ポップアップ表示条件を設定
            switch (this.txtNum_DenPyouSyurui.Text)
            {
                case ("2"):
                    // 荷積業者
                    // 20151026 BUNN #12040 STR
                    if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    // 20151026 BUNN #12040 END
                    {
                        ren = true;
                    }
                    break;
                case ("1"):
                case ("3"):
                default:
                    // 荷降業者
                    // 20151026 BUNN #12040 STR
                    if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                    // 20151026 BUNN #12040 END
                    {
                        ren = true;
                    }
                    break;
            }
            return ren;
        }


        /// <summary>
        /// 現場CD有効性チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //現場CD
                string pGenbaCd = this.GENBA_CD.Text.ToString().Trim();
                if (pGenbaCd != "")
                {
                    pGenbaCd = pGenbaCd.PadLeft(6, '0');
                }
                else
                {
                    this.testGENBA_CD.Text = pGenbaCd;
                    this.GENBA_NAME.Clear();
                    return;
                }
                // 業者入力されてない場合
                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.GENBA_CD.Text = string.Empty;
                    this.testGENBA_CD.Text = string.Empty;
                    e.Cancel = true;
                    return;
                }
                //業者CD
                string pGyousyaCD = this.GYOUSHA_CD.Text.ToString().Trim();
                if (pGyousyaCD != "")
                {
                    pGyousyaCD = pGyousyaCD.PadLeft(6, '0');
                }
                //前回値に比較
                if (this.testGENBA_CD.Text == pGenbaCd)
                {
                    return;
                }
                //取引先、業者と関連チェック
                bool catchErr = true;
                var ret = this.IchiranLogic.GetGenbaInfo(pGenbaCd, pGyousyaCD, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                switch (ret.Length)
                {
                    case 0:
                        //<レコードが存在しない
                        this.GENBA_CD.IsInputErrorOccured = true;
                        this.GENBA_NAME.Clear();
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.GENBA_CD.SelectAll();
                        e.Cancel = true;
                        break;
                    case 1:
                        this.testGENBA_CD.Text = pGenbaCd;
                        this.GENBA_NAME.Text = ret[0].GENBA_NAME_RYAKU;
                        this.GYOUSHA_CD.Text = ret[0].GYOUSHA_CD;
                        var retGyo = this.IchiranLogic.GetGyousyaInfo(this.GYOUSHA_CD.Text, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        this.GYOUSHA_NAME.Text = retGyo[0].GYOUSHA_NAME_RYAKU;
                        this.testGYOUSHA_CD.Text = ret[0].GYOUSHA_CD;
                        break;
                    default:
                        SendKeys.Send(" ");
                        e.Cancel = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 荷降現場CD有効性チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //荷降現場CD
                string pNioroshiGenbaCd = this.NIOROSHI_GENBA_CD.Text.ToString().Trim();
                if (pNioroshiGenbaCd != "")
                {
                    pNioroshiGenbaCd = pNioroshiGenbaCd.PadLeft(6, '0');
                }
                else
                {
                    this.testNIOROSHI_GENBA_CD.Text = pNioroshiGenbaCd;
                    this.NIOROSHI_GENBA_NAME.Clear();
                    return;
                }
                // 業者入力されてない場合
                if (string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text))
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E051", msgNioGyo);
                    this.NIOROSHI_GENBA_CD.Text = string.Empty;
                    this.testNIOROSHI_GENBA_CD.Text = string.Empty;
                    e.Cancel = true;
                    return;
                }
                //業者CD
                string pNioroshiGyousyaCD = this.NIOROSHI_GYOUSHA_CD.Text.ToString().Trim();
                if (pNioroshiGyousyaCD != "")
                {
                    pNioroshiGyousyaCD = pNioroshiGyousyaCD.PadLeft(6, '0');
                }
                //前回値に比較
                if (this.testNIOROSHI_GENBA_CD.Text == pNioroshiGenbaCd)
                {
                    return;
                }
                //荷降現場情報取得
                bool catchErr = true;
                var ret = this.IchiranLogic.GetNioroshiGenbaInfo(pNioroshiGenbaCd, pNioroshiGyousyaCD, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (ret == null || ret.Rows.Count == 0)
                {
                    //<レコードが存在しない
                    this.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    this.NIOROSHI_GENBA_NAME.Clear();
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.NIOROSHI_GENBA_CD.SelectAll();
                    e.Cancel = true;
                    return;
                }
                if (ret.Rows.Count <= 2)
                {
                    // 伝票種類に応じて荷降/荷積現場チェックをします
                    if (CheckNioroshiNizumiGenbaCd(ret, out catchErr))
                    {
                        if (!catchErr)
                        {
                            return;
                        }
                        this.testNIOROSHI_GENBA_CD.Text = pNioroshiGenbaCd;
                        this.NIOROSHI_GENBA_NAME.Text = ret.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                        this.NIOROSHI_GYOUSHA_CD.Text = ret.Rows[0]["GYOUSHA_CD"].ToString();
                        this.NIOROSHI_GYOUSHA_NAME.Text = ret.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                        this.testNIOROSHI_GYOUSHA_CD.Text = ret.Rows[0]["GYOUSHA_CD"].ToString();
                    }
                    else
                    {
                        if (!catchErr)
                        {
                            return;
                        }
                        //<レコードが存在しない
                        this.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                        this.NIOROSHI_GENBA_NAME.Clear();
                        msgLogic.MessageBoxShow("E062", msgNioGen);
                        this.NIOROSHI_GENBA_CD.SelectAll();
                        e.Cancel = true;
                    }
                }
                else
                {
                    SendKeys.Send(" ");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NIOROSHI_GENBA_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 荷降/荷積現場が有効かチェックします
        /// </summary>
        /// <returns>
        /// True：ＯＫ
        /// False：エラー
        /// </returns>
        private bool CheckNioroshiNizumiGenbaCd(DataTable genba, out bool catchErr)
        {
            var ren = false;
            catchErr = true;
            try
            {
                // ポップアップ表示条件を設定
                switch (this.txtNum_DenPyouSyurui.Text)
                {
                    case ("2"):
                        // 荷積業者
                        // 20151026 BUNN #12040 STR
                        if ((!string.IsNullOrEmpty(genba.Rows[0]["HAISHUTSU_NIZUMI_GENBA_KBN"].ToString()) && (bool)genba.Rows[0]["HAISHUTSU_NIZUMI_GENBA_KBN"]) ||
                            (!string.IsNullOrEmpty(genba.Rows[0]["TSUMIKAEHOKAN_KBN"].ToString()) && (bool)genba.Rows[0]["TSUMIKAEHOKAN_KBN"]))
                        // 20151026 BUNN #12040 END
                        {
                            ren = true;
                        }
                        break;
                    case ("1"):
                    case ("3"):
                    default:
                        // 荷降業者
                        // 20151026 BUNN #12040 STR
                        if ((!string.IsNullOrEmpty(genba.Rows[0]["TSUMIKAEHOKAN_KBN"].ToString()) && (bool)genba.Rows[0]["TSUMIKAEHOKAN_KBN"]) ||
                            (!string.IsNullOrEmpty(genba.Rows[0]["SHOBUN_NIOROSHI_GENBA_KBN"].ToString()) && (bool)genba.Rows[0]["SHOBUN_NIOROSHI_GENBA_KBN"]) ||
                            (!string.IsNullOrEmpty(genba.Rows[0]["SAISHUU_SHOBUNJOU_KBN"].ToString()) && (bool)genba.Rows[0]["SAISHUU_SHOBUNJOU_KBN"]))
                        // 20151026 BUNN #12040 END
                        {
                            ren = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiNizumiGenbaCd", ex);
                this.IchiranLogic.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return ren;
        }
        #endregion

        #region　伝票種類変更関連
        /// <summary>
        /// 伝票種類が変更された時に処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void txtNum_DenPyouSyurui_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            // 読込データ件数：０ [件]
            this.header_new.readDataNumber.Text = "0";
            string denpyouShurui = this.txtNum_DenPyouSyurui.Text.ToString();
            // VUNGUYEN 20150703 START
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.bt_func1.Enabled = false;
            parentForm.bt_func2.Enabled = true;
            // VUNGUYEN 20150703 END

            #region 伝種切り替えによってグリッドを更新しない
            //// dataGridViewヘーダ部再設定
            //DataTable table = this.IchiranLogic.GetColumnHeaderOnlyDataTable();

            //// dataGridViewデータをクリア
            //this.Table = table;

            //// dataGridView再表示
            //if (table != null)
            //{
            //    this.logic.CreateDataGridView(this.Table);
            //}
            #endregion

            if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouShurui)
            {
                this.SetNioroshiGyousha();
                this.ChangeNizumioroshiEnabled(true);
                this.ChangeHaishaJokyoEnabled(true);
                this.header_new.radbtnSagyoubi.Enabled = true;
                this.DenshuKbn = DENSHU_KBN.UKETSUKE_SS_ICHIRAN;
                this.IchiranLogic.BppanForPropertySetting(false);
                // VUNGUYEN 20150703 START
                parentForm.bt_func1.Enabled = true;
                // VUNGUYEN 20150703 END
            }
            else if (ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouShurui)
            {
                this.SetNizumiGyousha();
                this.ChangeNizumioroshiEnabled(true);
                this.ChangeHaishaJokyoEnabled(true);
                this.header_new.radbtnSagyoubi.Enabled = true;
                this.DenshuKbn = DENSHU_KBN.UKETSUKE_SK_ICHIRAN;
                this.IchiranLogic.BppanForPropertySetting(false);
                // VUNGUYEN 20150703 START
                parentForm.bt_func1.Enabled = true;
                // VUNGUYEN 20150703 END
            }
            else if (ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI == denpyouShurui)
            {
                this.SetNioroshiGyousha();
                this.ChangeNizumioroshiEnabled(true);
                this.ChangeHaishaJokyoEnabled(false);
                this.header_new.radbtnSagyoubi.Enabled = true;
                this.DenshuKbn = DENSHU_KBN.UKETUSKE_MK_ICHIRAN;
                this.IchiranLogic.BppanForPropertySetting(false);
            }
            else if (ConstCls.DENPYOU_SHURUI_CD_KUREMU == denpyouShurui)
            {
                this.ChangeNizumioroshiEnabled(false);
                this.ChangeHaishaJokyoEnabled(false);
                this.header_new.txtNum_HidukeSentaku.Text = ConstCls.HidukeCD_DenPyou;
                this.header_new.radbtnSagyoubi.Enabled = false;
                this.DenshuKbn = DENSHU_KBN.UKETSUKE_CM_ICHIRAN;
                this.IchiranLogic.BppanForPropertySetting(false);
            }
            // VUNGUYEN 20150703 START
            else if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouShurui)
            {
                this.ChangeNizumioroshiEnabled(false);
                this.ChangeHaishaJokyoEnabled(true);
                this.header_new.radbtnSagyoubi.Enabled = true;
                this.DenshuKbn = DENSHU_KBN.UKETUSKE_SS_SK_ICHIRAN;
                this.IchiranLogic.BppanForPropertySetting(false);
                parentForm.bt_func1.Enabled = true;
                parentForm.bt_func2.Enabled = false;
            }
            else if (ConstCls.DENPYOU_SHURUI_CD_SS_MK == denpyouShurui)
            {
                this.SetNioroshiGyousha();
                this.ChangeNizumioroshiEnabled(true);
                this.ChangeHaishaJokyoEnabled(false);
                this.header_new.radbtnSagyoubi.Enabled = true;
                this.DenshuKbn = DENSHU_KBN.UKETUSKE_SS_MK_ICHIRAN;
                this.IchiranLogic.BppanForPropertySetting(false);
                parentForm.bt_func2.Enabled = false;
            }
            // VUNGUYEN 20150703 END

            this.SetHeaderTitle();
            this.ChangeSubFunctionButtonEnabled(denpyouShurui);
            this.PatternReload(true);
            // オプション
            if (AppConfig.AppOptions.IsMAPBOX())
            {
                // 一覧内のチェックボックスの設定
                this.IchiranLogic.HeaderCheckBoxSupport2();
                this.IchiranLogic.notReadOnlyColumns();
            }
            //Start Sontt 20150710
            this.IchiranLogic.HeaderCheckBoxSupport();
            //End Sontt 20150710
            // 20140624 syunrei EV004430_受付一覧で検索後、伝票種類を変更してからサブファンクションで伝票紐付をするとシステムエラー　start
            if (this.customDataGridView1 != null && this.customDataGridView1.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.customDataGridView1.DataSource;
                dt.Clear();
                this.customDataGridView1.DataSource = dt;
            }
            // 20140624 syunrei EV004430_受付一覧で検索後、伝票種類を変更してからサブファンクションで伝票紐付をするとシステムエラー　end

            // オプション
            if (AppConfig.AppOptions.IsSMS())
            {
                // ｼｮｰﾄﾒｯｾｰｼﾞボタンの活性・非活性設定
                if (this.txtNum_DenPyouSyurui.Text == ConstCls.DENPYOU_SHURUI_CD_KUREMU)
                {
                    this.IchiranLogic.bt_sms.Enabled = false;
                }
                else
                {
                    this.IchiranLogic.bt_sms.Enabled = true;
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// ヘッダのタイトルとウィンドウタイトルバーに伝種区分毎のタイトルを設定します。
        /// </summary>
        private void SetHeaderTitle()
        {
            this.header_new.lb_title.Text = this.DenshuKbn.ToTitleString();
            this.ParentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.header_new.lb_title.Text);
            // VUNGUYEN 20150703 START
            string denpyouShurui = this.txtNum_DenPyouSyurui.Text.ToString();
            if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouShurui || ConstCls.DENPYOU_SHURUI_CD_SS_MK == denpyouShurui
                 || ConstCls.DENPYOU_SHURUI_CD_KUREMU == denpyouShurui)
            {
                this.header_new.label1.Location = new Point(302, 2);
                this.header_new.label1.Size = new Size(90, 20);
            }
            else
            {
                this.header_new.label1.Location = new Point(282, 2);
                this.header_new.label1.Size = new Size(110, 20);
            }
            // VUNGUYEN 20150703 END
        }

        /// <summary>
        /// 伝票種類によってサブファンクションボタンの状態を変更します
        /// </summary>
        /// <param name="denpyouShurui">伝票種類</param>
        private void ChangeSubFunctionButtonEnabled(string denpyouShurui)
        {
            LogUtility.DebugMethodStart(denpyouShurui);
            var parentForm = (BusinessBaseForm)this.Parent;
            if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouShurui)
            {
                // オプション
                if (AppConfig.AppOptions.IsMAPBOX())
                {
                    parentForm.bt_process3.Enabled = true;
                }
                parentForm.bt_process4.Enabled = true;
                parentForm.bt_process5.Enabled = true;
                parentForm.bt_process4.Text = ConstCls.PROCESS_BUTTON_TEXT_UKEIRE;
            }
            else if (ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouShurui)
            {
                // オプション
                if (AppConfig.AppOptions.IsMAPBOX())
                {
                    parentForm.bt_process3.Enabled = true;
                }
                parentForm.bt_process4.Enabled = true;
                parentForm.bt_process5.Enabled = true;
                parentForm.bt_process4.Text = ConstCls.PROCESS_BUTTON_TEXT_SHUKKA;
            }
            else if (ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI == denpyouShurui)
            {
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process4.Enabled = true;
                parentForm.bt_process5.Enabled = true;
                parentForm.bt_process4.Text = ConstCls.PROCESS_BUTTON_TEXT_UKEIRE;
            }
            else if (ConstCls.DENPYOU_SHURUI_CD_KUREMU == denpyouShurui)
            {
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process4.Enabled = false;
                parentForm.bt_process5.Enabled = false;
            }
            // VUNGUYEN 20150703 START
            else if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouShurui)
            {
                // オプション
                if (AppConfig.AppOptions.IsMAPBOX())
                {
                    parentForm.bt_process3.Enabled = true;
                }
                parentForm.bt_process4.Enabled = true;
                parentForm.bt_process5.Enabled = true;
                parentForm.bt_process4.Text = ConstCls.PROCESS_BUTTON_TEXT_UKEIRE_SHUKKA;
            }
            else if (ConstCls.DENPYOU_SHURUI_CD_SS_MK == denpyouShurui)
            {
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process4.Enabled = true;
                parentForm.bt_process5.Enabled = true;
                parentForm.bt_process4.Text = ConstCls.PROCESS_BUTTON_TEXT_UKEIRE;
            }
            // VUNGUYEN 20150703 END
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積・荷降業者のコントロールを設定します（荷積に）
        /// </summary>
        private void SetNizumiGyousha()
        {
            LogUtility.DebugMethodStart();
            this.labelNioGyo.Text = "荷積業者";
            this.lableNioGen.Text = "荷積現場";
            this.msgNioGyo = "荷積業者";
            this.msgNioGen = "荷積現場";
            this.NIOROSHI_GENBA_CD.Tag = "荷積現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NIOROSHI_GYOUSHA_CD.Tag = "荷積業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            // ポップアップ表示条件を設定
            this.SetNioroshiNisumiPopupSearchSendParams(2);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積・荷降業者のコントロールを設定します（荷降に）
        /// </summary>
        private void SetNioroshiGyousha()
        {
            LogUtility.DebugMethodStart();
            this.labelNioGyo.Text = "荷降業者";
            this.lableNioGen.Text = "荷降現場";
            this.msgNioGyo = "荷降業者";
            this.msgNioGen = "荷降現場";
            this.NIOROSHI_GENBA_CD.Tag = "荷降現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NIOROSHI_GYOUSHA_CD.Tag = "荷降業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            // ポップアップ表示条件を設定
            this.SetNioroshiNisumiPopupSearchSendParams(1);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降業者・現場及び荷積業者・現場のポップアップ表示条件を設定します
        /// </summary>
        /// <param name="kbn">
        /// 荷降か荷積かの区分
        /// １＝荷降
        /// ２＝荷積
        /// </param>
        internal void SetNioroshiNisumiPopupSearchSendParams(int kbn)
        {
            // 初期化
            var gyoushaSendParams = new PopupSearchSendParamDto();
            var gyoushaSubSendParams = new PopupSearchSendParamDto();
            var genbaSendParams = new PopupSearchSendParamDto();
            var genbaSubSendParams = new PopupSearchSendParamDto();
            this.NIOROSHI_GYOUSHA_CD.PopupSearchSendParams.Clear();
            this.NIOROSHI_GENBA_CD.PopupSearchSendParams.Clear();
            switch (kbn)
            {
                case (1):
                    // 荷降業者ポップアップの条件をセット
                    gyoushaSendParams.And_Or = CONDITION_OPERATOR.AND;
                    gyoushaSubSendParams = new PopupSearchSendParamDto();
                    gyoushaSubSendParams.And_Or = CONDITION_OPERATOR.AND;
                    gyoushaSubSendParams.KeyName = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                    gyoushaSubSendParams.Value = "TRUE";
                    gyoushaSendParams.SubCondition.Add(gyoushaSubSendParams);
                    gyoushaSubSendParams = new PopupSearchSendParamDto();
                    gyoushaSubSendParams.And_Or = CONDITION_OPERATOR.OR;
                    gyoushaSubSendParams.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                    gyoushaSubSendParams.Value = "TRUE";
                    gyoushaSendParams.SubCondition.Add(gyoushaSubSendParams);
                    this.NIOROSHI_GYOUSHA_CD.PopupSearchSendParams.Add(gyoushaSendParams);
                    // 荷降現場ポップアップの条件をセット
                    genbaSendParams = new PopupSearchSendParamDto();
                    genbaSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSendParams.Control = "NIOROSHI_GYOUSHA_CD";
                    genbaSendParams.KeyName = "GYOUSHA_CD";
                    this.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(genbaSendParams);
                    genbaSendParams = new PopupSearchSendParamDto();
                    genbaSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSubSendParams = new PopupSearchSendParamDto();
                    genbaSubSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSubSendParams.KeyName = "M_GENBA.SHOBUN_NIOROSHI_GENBA_KBN";
                    genbaSubSendParams.Value = "TRUE";
                    genbaSendParams.SubCondition.Add(genbaSubSendParams);
                    genbaSubSendParams = new PopupSearchSendParamDto();
                    genbaSubSendParams.And_Or = CONDITION_OPERATOR.OR;
                    genbaSubSendParams.KeyName = "M_GENBA.TSUMIKAEHOKAN_KBN";
                    genbaSubSendParams.Value = "TRUE";
                    genbaSendParams.SubCondition.Add(genbaSubSendParams);
                    genbaSubSendParams = new PopupSearchSendParamDto();
                    genbaSubSendParams.And_Or = CONDITION_OPERATOR.OR;
                    genbaSubSendParams.KeyName = "M_GENBA.SAISHUU_SHOBUNJOU_KBN";
                    genbaSubSendParams.Value = "TRUE";
                    genbaSendParams.SubCondition.Add(genbaSubSendParams);
                    this.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(genbaSendParams);
                    genbaSendParams = new PopupSearchSendParamDto();
                    genbaSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSubSendParams = new PopupSearchSendParamDto();
                    genbaSubSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSubSendParams.KeyName = "M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN";
                    genbaSubSendParams.Value = "TRUE";
                    genbaSendParams.SubCondition.Add(genbaSubSendParams);
                    genbaSubSendParams = new PopupSearchSendParamDto();
                    genbaSubSendParams.And_Or = CONDITION_OPERATOR.OR;
                    genbaSubSendParams.KeyName = "M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN";
                    genbaSubSendParams.Value = "TRUE";
                    genbaSendParams.SubCondition.Add(genbaSubSendParams);
                    this.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(genbaSendParams);
                    break;
                case (2):
                    // 荷積業者ポップアップの条件をセット
                    this.NIOROSHI_GYOUSHA_CD.PopupSearchSendParams.Clear();
                    gyoushaSendParams.And_Or = CONDITION_OPERATOR.AND;
                    gyoushaSubSendParams = new PopupSearchSendParamDto();
                    gyoushaSubSendParams.And_Or = CONDITION_OPERATOR.AND;
                    gyoushaSubSendParams.KeyName = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                    gyoushaSubSendParams.Value = "TRUE";
                    gyoushaSendParams.SubCondition.Add(gyoushaSubSendParams);
                    gyoushaSubSendParams = new PopupSearchSendParamDto();
                    gyoushaSubSendParams.And_Or = CONDITION_OPERATOR.OR;
                    gyoushaSubSendParams.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                    gyoushaSubSendParams.Value = "TRUE";
                    gyoushaSendParams.SubCondition.Add(gyoushaSubSendParams);
                    this.NIOROSHI_GYOUSHA_CD.PopupSearchSendParams.Add(gyoushaSendParams);
                    // 荷積現場ポップアップの条件をセット
                    genbaSendParams = new PopupSearchSendParamDto();
                    genbaSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSendParams.Control = "NIOROSHI_GYOUSHA_CD";
                    genbaSendParams.KeyName = "GYOUSHA_CD";
                    this.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(genbaSendParams);
                    genbaSendParams = new PopupSearchSendParamDto();
                    genbaSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSubSendParams = new PopupSearchSendParamDto();
                    genbaSubSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSubSendParams.KeyName = "M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN";
                    genbaSubSendParams.Value = "TRUE";
                    genbaSendParams.SubCondition.Add(genbaSubSendParams);
                    genbaSubSendParams = new PopupSearchSendParamDto();
                    genbaSubSendParams.And_Or = CONDITION_OPERATOR.OR;
                    genbaSubSendParams.KeyName = "M_GENBA.TSUMIKAEHOKAN_KBN";
                    genbaSubSendParams.Value = "TRUE";
                    genbaSendParams.SubCondition.Add(genbaSubSendParams);
                    this.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(genbaSendParams);
                    genbaSendParams = new PopupSearchSendParamDto();
                    genbaSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSubSendParams = new PopupSearchSendParamDto();
                    genbaSubSendParams.And_Or = CONDITION_OPERATOR.AND;
                    genbaSubSendParams.KeyName = "M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                    genbaSubSendParams.Value = "TRUE";
                    genbaSendParams.SubCondition.Add(genbaSubSendParams);
                    genbaSubSendParams = new PopupSearchSendParamDto();
                    genbaSubSendParams.And_Or = CONDITION_OPERATOR.OR;
                    genbaSubSendParams.KeyName = "M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN";
                    genbaSubSendParams.Value = "TRUE";
                    genbaSendParams.SubCondition.Add(genbaSubSendParams);
                    this.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(genbaSendParams);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 運搬業者・荷積降業者・荷積降現場のコントロールの活性を切り替えます
        /// </summary>
        /// <param name="enabled">活性</param>
        private void ChangeNizumioroshiEnabled(bool enabled)
        {
            LogUtility.DebugMethodStart(enabled);
            // VUNGUYEN 20150703 START
            string denpyouShurui = this.txtNum_DenPyouSyurui.Text.ToString();
            if (ConstCls.DENPYOU_SHURUI_CD_SS_SK != denpyouShurui && ConstCls.DENPYOU_SHURUI_CD_SS_SK != denpyouShurui)
            {
                this.UNPAN_GYOUSHA_CD.Enabled = enabled;
                this.UNPAN_GYOUSHA_NAME.Enabled = enabled;
            }
            else
            {
                this.UNPAN_GYOUSHA_CD.Enabled = true;
                this.UNPAN_GYOUSHA_NAME.Enabled = true;
            }
            // VUNGUYEN 20150703 END
            this.NIOROSHI_GYOUSHA_CD.Enabled = enabled;
            this.NIOROSHI_GYOUSHA_NAME.Enabled = enabled;
            this.NIOROSHI_GENBA_CD.Enabled = enabled;
            this.NIOROSHI_GENBA_NAME.Enabled = enabled;
            if (false == enabled)
            {
                // VUNGUYEN 20150703 START
                if (ConstCls.DENPYOU_SHURUI_CD_SS_SK != denpyouShurui && ConstCls.DENPYOU_SHURUI_CD_SS_SK != denpyouShurui)
                {
                    this.UNPAN_GYOUSHA_CD.Text = String.Empty;
                    this.UNPAN_GYOUSHA_NAME.Text = String.Empty;
                }
                // VUNGUYEN 20150703 END

                this.NIOROSHI_GYOUSHA_CD.Text = String.Empty;
                this.NIOROSHI_GYOUSHA_NAME.Text = String.Empty;
                this.NIOROSHI_GENBA_CD.Text = String.Empty;
                this.NIOROSHI_GENBA_NAME.Text = String.Empty;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 配車状況・配車種類のコントロールの活性を切り替えます
        /// </summary>
        /// <param name="enabled">活性</param>
        private void ChangeHaishaJokyoEnabled(bool enabled)
        {
            LogUtility.DebugMethodStart(enabled);
            this.HAISHA_JOKYO_CD.Enabled = enabled;
            this.HAISHA_JOKYO_NAME.Enabled = enabled;
            this.HAISHA_SHURUI_CD.Enabled = enabled;
            this.HAISHA_SHURUI_NAME.Enabled = enabled;
            if (false == enabled)
            {
                this.HAISHA_JOKYO_CD.Text = String.Empty;
                this.HAISHA_JOKYO_NAME.Text = String.Empty;
                this.HAISHA_SHURUI_CD.Text = String.Empty;
                this.HAISHA_SHURUI_NAME.Text = String.Empty;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 配車状況 Validatingイベント
        /// <summary>
        /// 配車状況　Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHA_JOKYO_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var haishaJokyoCd = this.HAISHA_JOKYO_CD.Text;
            bool catchErr = true;
            bool isOk = this.IchiranLogic.CheckHaishaJokyoCd(haishaJokyoCd, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (!isOk)
            {
                this.HAISHA_JOKYO_NAME.Text = String.Empty;
                this.HAISHA_JOKYO_CD.IsInputErrorOccured = true;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E011", "配車状況");
                this.HAISHA_JOKYO_CD.Focus();
            }
            else
            {
                var haishaJokyo = this.IchiranLogic.GetHaishaJokyo(haishaJokyoCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                this.HAISHA_JOKYO_NAME.Text = haishaJokyo;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 配車種類　Validatingイベント
        /// <summary>
        /// 配車種類　Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHA_SHURUI_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.HAISHA_SHURUI_NAME.Text = "";
                if (!string.IsNullOrEmpty(this.HAISHA_SHURUI_CD.Text))
                {
                    int cd;
                    int.TryParse(this.HAISHA_SHURUI_CD.Text, out cd);
                    switch (cd)
                    {
                        case 1:
                            this.HAISHA_SHURUI_NAME.Text = "通常";
                            break;
                        case 2:
                            this.HAISHA_SHURUI_NAME.Text = "仮押";
                            break;
                        case 3:
                            this.HAISHA_SHURUI_NAME.Text = "確定";
                            break;
                        default:
                            this.HAISHA_SHURUI_NAME.Text = string.Empty;
                            this.HAISHA_SHURUI_NAME.IsInputErrorOccured = true;
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E011", "配車種類");
                            this.HAISHA_SHURUI_CD.Focus();
                            break;
                    }
                    this.HAISHA_SHURUI_CD.Text = cd.ToString();
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 締日が変更されたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void shimebiChanged(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_CD.Text = String.Empty;
            this.TORIHIKISAKI_NAME.Text = String.Empty;
            this.cmbShihariaShimebi.SelectedIndexChanged -= new EventHandler(this.cmbShihariaShimebi_SelectedIndexChanged);
            this.cmbShihariaShimebi.SelectedIndex = 0;//CongBinh 20200331 #134987
            this.cmbShihariaShimebi.SelectedIndexChanged += new EventHandler(this.cmbShihariaShimebi_SelectedIndexChanged);
        }
        #endregion

        /// <summary>
        /// 初回表出イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            base.OnShown(e);
            this.txtNum_DenPyouSyurui.Focus();  // 初期フォーカス
            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }

        /// <summary>
        /// 現在選択されている行を取得します
        /// </summary>
        /// <returns>選択されている行</returns>
        internal DataGridViewRow GetSelectedRow()
        {
            LogUtility.DebugMethodStart();
            DataGridViewRow ret = null;
            ret = this.customDataGridView1.CurrentRow;
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 現在選択されている伝票種類を取得します
        /// </summary>
        /// <returns>伝票種類</returns>
        internal string GetDenpyouShuruiCd()
        {
            LogUtility.DebugMethodStart();
            var ret = String.Empty;
            ret = this.txtNum_DenPyouSyurui.Text;
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        // No.3123-->
        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
        }
        // No.3123

        /// <summary>
        /// 業者CDのポップアップから戻ってきたときの処理
        /// </summary>
        public void GyoushaCdPopUpAfter()
        {
            CheckGyoushaChange();
        }

        /// <summary>
        /// 荷降/荷積業者CDのポップアップから戻ってきたときの処理
        /// </summary>
        public void NioroshiGyoushaCdPopUpAfter()
        {
            CheckNioroshiGyoushaChange();
        }

        //CongBinh 20200331 #134987 S
        private void cmbShihariaShimebi_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_CD.Text = String.Empty;
            this.TORIHIKISAKI_NAME.Text = String.Empty;
            this.cmbShimebi.SelectedIndexChanged -= new EventHandler(this.shimebiChanged);
            this.cmbShimebi.SelectedIndex = 0;
            this.cmbShimebi.SelectedIndexChanged += new EventHandler(this.shimebiChanged);
        }
        //CongBinh 20200331 #134987 E

        #region Communicate InxsSubApplication

        internal void SetActiveDenpyouShuurui()
        {
            bool shougunInxsFlg = UKETSUKE_EXPORT_KBN.Text == "2";
            if (shougunInxsFlg)
            {
                this.radbtnSyukka.Enabled = false; //2.出荷
                this.radbtnKuremu.Enabled = false; //4.クレーム
                this.radbtnSsSk.Enabled = false; //5.収集+出荷 

                //Set [伝票種類※]
                if (this.IchiranLogic.arrDenpyouShuuruiOnlyShougun.Contains(this.txtNum_DenPyouSyurui.Text)) //5.収集+出荷 
                {
                    if (!this.IchiranLogic.arrDenpyouShuuruiOnlyShougun.Contains(ConstCls.DenPyouDefultCD))
                    {
                        this.txtNum_DenPyouSyurui.Text = ConstCls.DenPyouDefultCD;
                    }
                    else
                    {
                        this.txtNum_DenPyouSyurui.Text = ConstCls.DENPYOU_SHURUI_CD_SYUSYU;
                    }
                }
            }
            else
            {
                this.radbtnSyukka.Enabled = true; //2.出荷
                this.radbtnKuremu.Enabled = true; //4.クレーム
                this.radbtnSsSk.Enabled = true; //5.収集+出荷 
            }
        }

        #endregion

        //SONNT #143061 受付一覧抽出条件の変更 2020/10 START
        internal void UKETSUKE_EXPORT_KBN_TextChanged(object sender, EventArgs e)
        {
            this.SetActiveDenpyouShuurui();
            //Clear Ichiran
            if (this.customDataGridView1 != null && this.customDataGridView1.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.customDataGridView1.DataSource;
                dt.Clear();
                this.customDataGridView1.DataSource = dt;
            }
        }
        //SONNT #143061 受付一覧抽出条件の変更 2020/10 END
    }
}