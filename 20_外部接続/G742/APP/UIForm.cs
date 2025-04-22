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

namespace Shougun.Core.ExternalConnection.GenbamemoIchiran
{
    [Implementation]
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド

        /// <summary>
        /// ロジック
        /// </summary>
        private GenbamemoIchiran.LogicCls IchiranLogic;

        /// <summary>
        /// UIHeader
        /// </summary>
        private UIHeaderForm header_new;

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

        /// <summary>
        /// 遷移元画面からのパラメータEntry
        /// </summary>
        public T_GENBAMEMO_ENTRY paramEntry { get; set; }

        /// <summary>
        /// 遷移元画面からのパラメータWindows_ID
        /// </summary>
        public string winId { get; set; }

        #endregion

        #region メソッド

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="entry">entry</param>
        /// <param name="winId">winId</param>
        public UIForm(T_GENBAMEMO_ENTRY entry, string winId)
            : base(DENSHU_KBN.GENBA_MEMO_ICHIRAN, false)
        {
            try
            {
                this.InitializeComponent();
                isLoaded = false;

                this.paramEntry = entry;
                this.winId = winId;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
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

                base.OnLoad(e);

                if (this.IchiranLogic == null)
                {
                    //ロジックに、Header部情報を設定する            
                    this.IchiranLogic = new LogicCls(this);
                    // 完全に固定。ここには変更を入れない
                    QuillInjector.GetInstance().Inject(this);
                    //社員コードを取得すること
                    this.ShainCd = SystemProperty.Shain.CD;
                    //Main画面で社員コード値を取得すること
                    this.IchiranLogic.syainCode = SystemProperty.Shain.CD;
                    //伝種区分を取得すること
                    this.IchiranLogic.denShu_Kbn = (int)DENSHU_KBN.GENBA_MEMO_ICHIRAN;

                    if (!this.IchiranLogic.WindowInit())
                    {
                        return;
                    }
                    this.header_new = this.IchiranLogic.headForm;
                    //画面初期表示
                    this.IchiranLogic.InitFrom();

                    this.customSearchHeader1.Location = new System.Drawing.Point(4, 160);
                    this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customSortHeader1.Location = new System.Drawing.Point(4, 190);
                    this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                    this.customDataGridView1.Location = new System.Drawing.Point(3, 220);
                    this.customDataGridView1.Size = new System.Drawing.Size(997, 210);

                    // Anchorの設定は必ずOnLoadで行うこと
                    if (this.customDataGridView1 != null)
                    {
                        this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    }

                }

                this.PatternReload();

                // フィルタの初期化
                this.customSearchHeader1.ClearCustomSearchSetting();
                this.customSortHeader1.ClearCustomSortSetting();

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
                else
                {
                    this.TORIHIKISAKI_NAME.Text = ret[0].TORIHIKISAKI_NAME_RYAKU;
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
        /// 現場メモ分類CD有効性チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBAMEMO_BUNRUI_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //現場メモ分類CD
                string cd = this.GENBAMEMO_BUNRUI_CD.Text.ToString().Trim();
                if (cd != "")
                {
                    cd = cd.PadLeft(3, '0');
                }
                else
                {
                    this.GENBAMEMO_BUNRUI_NAME_RYAKU.Clear();
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool bRet = true;
                // 現場メモ分類マスタチェック
                bool catchErr = true;
                var ret = this.IchiranLogic.GetGenbamemoBunrui(cd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (ret == null)
                {
                    // 現場メモ分類マスタにデータ存在しない
                    this.GENBAMEMO_BUNRUI_CD.IsInputErrorOccured = true;
                    this.GENBAMEMO_BUNRUI_NAME_RYAKU.Clear();
                    msgLogic.MessageBoxShow("E020", "現場メモ分類");
                    bRet = false;
                }
                else
                {
                    this.GENBAMEMO_BUNRUI_NAME_RYAKU.Text = ret.GENBAMEMO_BUNRUI_NAME_RYAKU;
                }
                //存在しない
                if (!bRet)
                {
                    this.GENBAMEMO_BUNRUI_CD.SelectAll();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBAMEMO_BUNRUI_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 発生元CD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HASSEIMOTO_CD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                string cd = this.txtNum_HasseimotoSentaku.Text.ToString();
                if (cd.Equals("1"))
                {
                    // チェックボックス群を非活性にする。
                    this.chkHasseimotoNashi.Enabled = false;
                    this.chkShuushuuUketsuke.Enabled = false;
                    this.chkShukkaUketsuke.Enabled = false;
                    this.chkMochikomiUketsuke.Enabled = false;
                    this.chkTeikiHaisha.Enabled = false;
                    // チェックボックス群を全てONにする。
                    this.chkHasseimotoNashi.Checked = true;
                    this.chkShuushuuUketsuke.Checked = true;
                    this.chkShukkaUketsuke.Checked = true;
                    this.chkMochikomiUketsuke.Checked = true;
                    this.chkTeikiHaisha.Checked = true;
                }
                else
                {
                    // チェックボックス群を活性にする。
                    this.chkHasseimotoNashi.Enabled = true;
                    this.chkShuushuuUketsuke.Enabled = true;
                    this.chkShukkaUketsuke.Enabled = true;
                    this.chkMochikomiUketsuke.Enabled = true;
                    this.chkTeikiHaisha.Enabled = true;
                    // チェックボックス群を全てOFFにする。
                    this.chkHasseimotoNashi.Checked = false;
                    this.chkShuushuuUketsuke.Checked = false;
                    this.chkShukkaUketsuke.Checked = false;
                    this.chkMochikomiUketsuke.Checked = false;
                    this.chkTeikiHaisha.Checked = false;
                }

                // 発生元番号、発生元明細番号は非活性する。
                this.HASSEIMOTO_NUMBER.Enabled = false;
                this.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;
                // 発生元番号、発生元明細番号はクリアする。
                this.HASSEIMOTO_NUMBER.Text = string.Empty;
                this.HASSEIMOTO_MEISAI_NUMBER.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HASSEIMOTO_CD_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 発生元チェックボックスによる明細Noの制御
        /// </summary>
        private void HasseimotoMeisaiNoControl()
        {
            bool SSU = this.chkShuushuuUketsuke.Checked;
            bool SKU = this.chkShukkaUketsuke.Checked;
            bool MKU = this.chkMochikomiUketsuke.Checked;
            bool TKH = this.chkTeikiHaisha.Checked;

            // 発生元が「2.指定」の場合
            if (this.txtNum_HasseimotoSentaku.Text.Equals("2"))
            {
                if ((SSU || SKU || MKU) && !TKH)
                {
                    // 明細Noを非活性にする。
                    this.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;
                }
                else if (TKH)
                {
                    // 明細Noを活性にする。
                    this.HASSEIMOTO_MEISAI_NUMBER.Enabled = true;
                }
            }
            // 発生元番号、発生元明細番号はクリアする。
            this.HASSEIMOTO_NUMBER.Text = string.Empty;
            this.HASSEIMOTO_MEISAI_NUMBER.Text = string.Empty;
        }

        /// <summary>
        /// 発生元無し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hasseimoto_Nashi_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 発生元が「2.指定」の場合
                if (this.txtNum_HasseimotoSentaku.Text.Equals("2"))
                {
                    string senderVal = sender.ToString();
                    if (senderVal.Contains("CheckState: 1"))
                    {
                        // チェックをONにした要素以外はOFFにする。
                        this.chkShuushuuUketsuke.Checked = false;
                        this.chkShukkaUketsuke.Checked = false;
                        this.chkMochikomiUketsuke.Checked = false;
                        this.chkTeikiHaisha.Checked = false;

                        // 発生元番号と発生元明細番号を非活性にする。
                        this.HASSEIMOTO_NUMBER.Enabled = false;
                        this.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;
                    }
                }

                this.HasseimotoMeisaiNoControl();
                
            }
            catch (Exception ex)
            {
                LogUtility.Error("Hasseimoto_Nashi_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 発生元(収集受付)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hasseimoto_Uketsuke_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 発生元が「2.指定」の場合
                if (this.txtNum_HasseimotoSentaku.Text.Equals("2"))
                {
                    string senderVal = sender.ToString();
                    if (senderVal.Contains("CheckState: 1"))
                    {
                        // チェックをONにした要素以外はOFFにする。
                        this.chkHasseimotoNashi.Checked = false;
                        this.chkShukkaUketsuke.Checked = false;
                        this.chkMochikomiUketsuke.Checked = false;
                        this.chkTeikiHaisha.Checked = false;

                        // 発生元番号を活性化する。
                        this.HASSEIMOTO_NUMBER.Enabled = true;
                        // 発生元明細番号を非活性にする。
                        this.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;
                    }
                }

                this.HasseimotoMeisaiNoControl();

            }
            catch (Exception ex)
            {
                LogUtility.Error("Hasseimoto_Uketsuke_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 発生元(出荷受付)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hasseimoto_Shukka_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 発生元が「2.指定」の場合
                if (this.txtNum_HasseimotoSentaku.Text.Equals("2"))
                {
                    string senderVal = sender.ToString();
                    if (senderVal.Contains("CheckState: 1"))
                    {
                        // チェックをONにした要素以外はOFFにする。
                        this.chkHasseimotoNashi.Checked = false;
                        this.chkShuushuuUketsuke.Checked = false;
                        this.chkMochikomiUketsuke.Checked = false;
                        this.chkTeikiHaisha.Checked = false;

                        // 発生元番号を活性化する。
                        this.HASSEIMOTO_NUMBER.Enabled = true;
                        // 発生元明細番号を非活性にする。
                        this.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;
                    }
                }

                this.HasseimotoMeisaiNoControl();

            }
            catch (Exception ex)
            {
                LogUtility.Error("Hasseimoto_Shukka_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 発生元(持込受付)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hasseimoto_Mochikomi_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 発生元が「2.指定」の場合
                if (this.txtNum_HasseimotoSentaku.Text.Equals("2"))
                {
                    string senderVal = sender.ToString();
                    if (senderVal.Contains("CheckState: 1"))
                    {
                        // チェックをONにした要素以外はOFFにする。
                        this.chkHasseimotoNashi.Checked = false;
                        this.chkShuushuuUketsuke.Checked = false;
                        this.chkShukkaUketsuke.Checked = false;
                        this.chkTeikiHaisha.Checked = false;

                        // 発生元番号を活性化する。
                        this.HASSEIMOTO_NUMBER.Enabled = true;
                        // 発生元明細番号を非活性にする。
                        this.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;
                    }
                }

                this.HasseimotoMeisaiNoControl();

            }
            catch (Exception ex)
            {
                LogUtility.Error("Hasseimoto_Mochikomi_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 発生元(定期配車)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hasseimoto_Teiihaisha_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 発生元が「2.指定」の場合
                if (this.txtNum_HasseimotoSentaku.Text.Equals("2"))
                {
                    string senderVal = sender.ToString();
                    if (senderVal.Contains("CheckState: 1"))
                    {
                        // チェックをONにした要素以外はOFFにする。
                        this.chkHasseimotoNashi.Checked = false;
                        this.chkShuushuuUketsuke.Checked = false;
                        this.chkShukkaUketsuke.Checked = false;
                        this.chkMochikomiUketsuke.Checked = false;

                        // 発生元番号と発生元明細番号を活性にする。
                        this.HASSEIMOTO_NUMBER.Enabled = true;
                        this.HASSEIMOTO_MEISAI_NUMBER.Enabled = true;
                    }
                }

                this.HasseimotoMeisaiNoControl();

            }
            catch (Exception ex)
            {
                LogUtility.Error("Hasseimoto_Teiihaisha_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 初回登録者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOKAI_TOUROKUSHA_CD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                string cd = this.txtNum_ShokaiTourokushaSentaku.Text.ToString();
                if (cd.Equals("2"))
                {
                    // 初回登録者の社員検索条件を変更する。
                    this.SHAIN_CD.popupWindowSetting.Clear();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.SearchConditionsDto searchDto2 = new r_framework.Dto.SearchConditionsDto();
                    // 条件１
                    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto1.LeftColumn = "NYUURYOKU_TANTOU_KBN";
                    searchDto1.Value = "TRUE";
                    searchDto1.ValueColumnType = DB_TYPE.BIT;
                    methodDto.SearchCondition.Add(searchDto1);
                    // 条件２
                    searchDto2.And_Or = CONDITION_OPERATOR.AND;
                    searchDto2.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto2.LeftColumn = "DELETE_FLG";
                    searchDto2.Value = "FALSE";
                    searchDto2.ValueColumnType = DB_TYPE.BIT;
                    methodDto.SearchCondition.Add(searchDto2);

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_SHAIN";
                    
                    this.SHAIN_CD.popupWindowSetting.Add(methodDto);
                }
                else if (cd.Equals("3"))
                {
                    // 初回登録者の社員検索条件を変更する。
                    this.SHAIN_CD.popupWindowSetting.Clear();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.SearchConditionsDto searchDto2 = new r_framework.Dto.SearchConditionsDto();
                    // 条件１
                    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto1.LeftColumn = "UNTEN_KBN";
                    searchDto1.Value = "TRUE";
                    searchDto1.ValueColumnType = DB_TYPE.BIT;
                    methodDto.SearchCondition.Add(searchDto1);
                    // 条件２
                    searchDto2.And_Or = CONDITION_OPERATOR.AND;
                    searchDto2.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto2.LeftColumn = "DELETE_FLG";
                    searchDto2.Value = "FALSE";
                    searchDto2.ValueColumnType = DB_TYPE.BIT;
                    methodDto.SearchCondition.Add(searchDto2);

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_SHAIN";

                    this.SHAIN_CD.popupWindowSetting.Add(methodDto);
                }
                else if (cd.Equals("4"))
                {
                    // 初回登録者の社員検索条件を変更する。
                    this.SHAIN_CD.popupWindowSetting.Clear();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.SearchConditionsDto searchDto2 = new r_framework.Dto.SearchConditionsDto();
                    // 条件１
                    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto1.LeftColumn = "EIGYOU_TANTOU_KBN";
                    searchDto1.Value = "TRUE";
                    searchDto1.ValueColumnType = DB_TYPE.BIT;
                    methodDto.SearchCondition.Add(searchDto1);
                    // 条件２
                    searchDto2.And_Or = CONDITION_OPERATOR.AND;
                    searchDto2.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto2.LeftColumn = "DELETE_FLG";
                    searchDto2.Value = "FALSE";
                    searchDto2.ValueColumnType = DB_TYPE.BIT;
                    methodDto.SearchCondition.Add(searchDto2);

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_SHAIN";

                    this.SHAIN_CD.popupWindowSetting.Add(methodDto);
                }
                else
                {
                    // 初回登録者の社員検索条件を変更する。
                    this.SHAIN_CD.popupWindowSetting.Clear();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                    searchDto.And_Or = CONDITION_OPERATOR.AND;
                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto.LeftColumn = "DELETE_FLG";
                    searchDto.Value = "FALSE";
                    searchDto.ValueColumnType = DB_TYPE.BIT;
                    methodDto.SearchCondition.Add(searchDto);

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_SHAIN";

                    this.SHAIN_CD.popupWindowSetting.Add(methodDto);
                }

                // 初回登録者のCD、名称をクリアする。
                this.SHAIN_CD.Text = string.Empty;
                this.SHAIN_NAME.Text = string.Empty;

            }
            catch (Exception ex)
            {
                LogUtility.Error("SHOKAI_TOUROKUSHA_CD_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 表示区分の入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_Hyoujikubun_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (string.IsNullOrEmpty(this.txtNum_HyoujiKubunSentaku.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.txtNum_HyoujiKubunSentaku.Focus();
                this.txtNum_HyoujiKubunSentaku.Text = "1";
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "3");
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 発生元の入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_Hasseimoto_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (string.IsNullOrEmpty(this.txtNum_HasseimotoSentaku.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.txtNum_HasseimotoSentaku.Focus();
                this.txtNum_HasseimotoSentaku.Text = "1";
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "3");
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 初回登録者の入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_ShokaiTourokusha_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (string.IsNullOrEmpty(this.txtNum_ShokaiTourokushaSentaku.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.txtNum_ShokaiTourokushaSentaku.Focus();
                this.txtNum_ShokaiTourokushaSentaku.Text = "1";
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "4");
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion


        #endregion

        /// <summary>
        /// 初回表出イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            base.OnShown(e);
            //this.txtNum_DenPyouSyurui.Focus();  // 初期フォーカス
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
            //ret = this.txtNum_DenPyouSyurui.Text;
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// 業者CDのポップアップから戻ってきたときの処理
        /// </summary>
        public void GyoushaCdPopUpAfter()
        {
            CheckGyoushaChange();
        }
    }
}