using System;
using System.Data;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using System.Drawing;

namespace Shougun.Core.Allocation.MobileJoukyouIchiran
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// 前回運搬業者
        /// </summary>
        string beforeUnpanGyousha = string.Empty;

        /// <summary>
        /// 前回車種CD
        /// </summary>
        string beforeShasyuCD = string.Empty;

        /// <summary>
        /// 前回車種
        /// </summary>
        string beforeShasyuName = string.Empty;

        /// <summary>
        /// 前回車輌CD
        /// </summary>
        string beforeSharyouCD = string.Empty;

        /// <summary>
        /// 前回車輌CD
        /// </summary>
        internal bool isFukusuPop = false;
        #endregion

        #region コンストラクタ

        public UIForm()
            //コンストラクタ
            : base(WINDOW_ID.T_MOBILE_JOUKYOU_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);
        }

        #endregion

        #region 画面 Loadイベント

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                // 画面情報の初期化
                this.logic.WindowInit();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 値変更処理
        /// <summary>
        /// 配車区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHA_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.HAISHA_KBN.Text))
                {
                    return;
                }
                else
                {
                    haoshaKbnChange();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("HAISHA_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 配車区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHA_KBN_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.HAISHA_KBN.Text))
                {
                    return;
                }
                else
                {
                    haoshaKbnChange();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("HAISHA_KBN_1_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 配車区分変更処理
        /// </summary>
        private void haoshaKbnChange()
        {
            // 明細変化処理
            this.logic.DetailChangeSyori();
            // 業者現場活性処理
            if (this.HAISHA_KBN_1.Checked)
            {
                this.GYOUSHA_CD.Enabled = false;
                this.GYOUSHA_NAME.Enabled = false;
                this.GYOUSHA_BUTTON.Enabled = false;
                this.GENBA_CD.Enabled = false;
                this.GENBA_NAME.Enabled = false;
                this.GENBA_BUTTON.Enabled = false;
                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_NAME.Text = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME.Text = string.Empty;
            }
            else
            {
                this.GYOUSHA_CD.Enabled = true;
                this.GYOUSHA_NAME.Enabled = true;
                this.GYOUSHA_BUTTON.Enabled = true;
                this.GENBA_CD.Enabled = true;
                this.GENBA_NAME.Enabled = true;
                this.GENBA_BUTTON.Enabled = true;
            }
        }


        /// <summary>
        /// 連携区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RENKEI_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.RENKEI_KBN.Text))
                {
                    return;
                }
                else
                {
                    renkeiKbnChange();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("HAISHA_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 連携区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RENKEI_KBN_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.RENKEI_KBN.Text))
                {
                    return;
                }
                else
                {
                    renkeiKbnChange();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RENKEI_KBN_1_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 連携区分変更処理
        /// </summary>
        private void renkeiKbnChange()
        {
            // 明細変化処理
            this.logic.DetailChangeSyori();
            //ボタン活性変化処理
            if (this.RENKEI_KBN_1.Checked || this.RENKEI_KBN_2.Checked)
            {
                // [回収状況]項目　活性
                this.KAISYUU_JYOUKYOU.Enabled = true;
                this.KAISYUU_JYOUKYOU_1.Enabled = true;
                this.KAISYUU_JYOUKYOU_2.Enabled = true;
                this.KAISYUU_JYOUKYOU_3.Enabled = true;
                // [F1]詳細確認ボタン　活性
                this.logic.parentForm.bt_func1.Enabled = true;
                if (this.RENKEI_KBN_1.Checked)
                {
                    // [F2]他車振替ボタン　活性
                    this.logic.parentForm.bt_func2.Enabled = true;
                }
                else
                {
                    // [F2]他車振替ボタン　非活性
                    this.logic.parentForm.bt_func2.Enabled = false;
                }
                // [F4]ﾓﾊﾞｲﾙ削除ボタン　活性
                this.logic.parentForm.bt_func4.Enabled = true;
                // [F9]ﾓﾊﾞｲﾙ登録ボタン　文言変更　→　[F9]実績確定
                this.logic.parentForm.bt_func9.Text = "[F9]\r" + "実績確定";
            }
            else
            {
                // [回収状況]項目　非活性
                this.KAISYUU_JYOUKYOU.Enabled = false;
                this.KAISYUU_JYOUKYOU_1.Enabled = false;
                this.KAISYUU_JYOUKYOU_2.Enabled = false;
                this.KAISYUU_JYOUKYOU_3.Enabled = false;
                // [F1]詳細確認ボタン　非活性
                this.logic.parentForm.bt_func1.Enabled = false;
                // [F2]他車振替ボタン　非活性
                this.logic.parentForm.bt_func2.Enabled = false;
                // [F4]ﾓﾊﾞｲﾙ削除ボタン　非活性
                this.logic.parentForm.bt_func4.Enabled = false;
                // [F9]ﾓﾊﾞｲﾙ登録ボタン　文言変更　→　[F9]ﾓﾊﾞｲﾙ登録
                this.logic.parentForm.bt_func9.Text = "[F9]\r" + "ﾓﾊﾞｲﾙ登録";
            }
        }

        /// <summary>
        /// 作業日付取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_DATE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.SAGYOU_DATE_TO.Text = this.SAGYOU_DATE_FROM.Text;
        }
        #endregion

        #region 全選択チェックボックス処理

        /// <summary>
        /// 全選択チェックボックス状態を切り替える
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.Ichiran.Rows.Count < 1)
                {
                    return;
                }

                foreach (var row in this.Ichiran.Rows)
                {
                    if (this.HAISHA_KBN.Text.Equals(ConstCls.HAISHA_KBN_2) && this.RENKEI_KBN.Text.Equals("3"))
                    {
                        // 現場CDが未入力のデータはチェックを入れない。
                        if (row["GENBA_CD"].Value == null || string.IsNullOrEmpty(row["GENBA_CD"].Value.ToString()))
                        {
                            row.Cells[0].Value = false;
                        }
                        else
                        {
                            row.Cells[0].Value = checkBoxAll.Checked;
                        }
                    }
                    else
                    {
                        row.Cells[0].Value = checkBoxAll.Checked;
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("checkBoxAll_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 全選択チェックボックスの描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellPainting(object sender, GrapeCity.Win.MultiRow.CellPaintingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.CellIndex == 0 && e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(this.checkBoxAll.Width, this.checkBoxAll.Height))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // Bitmapに描画
                        this.checkBoxAll.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                        // 描画位置設定
                        int rightMargin = 10;
                        int x = (e.CellBounds.Width - this.checkBoxAll.Width) - rightMargin;
                        int y = ((e.CellBounds.Height - this.checkBoxAll.Height) / 2);

                        // DataGridViewの現在描画中のセルに描画
                        Point pt = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);
                        e.Paint(e.ClipBounds);
                        e.Graphics.DrawImage(bmp, pt);
                        e.Handled = true;
                    }
                }

                //他社振替
                if (this.RENKEI_KBN_1.Checked)
                {
                    if (e.CellIndex == 1 && e.RowIndex == -1)
                    {
                        using (Bitmap bmp = new Bitmap(this.checkBoxAll2.Width, this.checkBoxAll2.Height))
                        {
                            // チェックボックスの描画領域を確保
                            using (Graphics g = Graphics.FromImage(bmp))
                            {
                                g.Clear(Color.Transparent);
                            }

                            // Bitmapに描画
                            this.checkBoxAll2.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                            // 描画位置設定
                            int rightMargin = 10;
                            int x = (e.CellBounds.Width - this.checkBoxAll2.Width) - rightMargin;
                            int y = ((e.CellBounds.Height - this.checkBoxAll2.Height) / 2);

                            // DataGridViewの現在描画中のセルに描画
                            Point pt = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);
                            e.Paint(e.ClipBounds);
                            e.Graphics.DrawImage(bmp, pt);
                            e.Handled = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellPainting", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 全選択チェックボックス状態を切り替えの描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.IchiranCellClick(sender, e);

            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 全選択チェックボックス状態を切り替えの描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.IchiranCellClick(sender, e);

            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 全選択チェックボックス状態を切り替える
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void checkBoxAll2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.Ichiran.Rows.Count < 1)
                {
                    return;
                }

                foreach (var row in this.Ichiran.Rows)
                {
                    if (row.Cells["KAISHU_JOKYO"].Value.ToString() == "未回収")
                    {
                        row.Cells[1].Value = checkBoxAll2.Checked;
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("checkBoxAll2_CheckedChanged", ex);
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
        /// 運搬業者CD(FocusOutCheckMethodと併用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.UNPAN_GYOUSHA_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
                {
                    this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    return;
                }

                this.logic.UNPAN_GYOUSHA_CDValidated();

            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CD_Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運搬業者前回値設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforeUnpanGyousha = this.UNPAN_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 運搬業者をクリアしても、車輌の情報もクリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Leave(object sender, EventArgs e)
        {
            this.unpanGyoushaCheck();
        }

        /// <summary>
        /// 親子関係チェック
        /// </summary>
        public void unpanGyoushaCheck()
        {
            if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                this.SHARYOU_CD.Text = string.Empty;
                this.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.logic.oldSharyouCD = string.Empty;
            }
            else if (beforeUnpanGyousha != this.UNPAN_GYOUSHA_CD.Text)
            {
                this.SHARYOU_CD.Text = string.Empty;
                this.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.logic.oldSharyouCD = string.Empty;
                beforeUnpanGyousha = this.UNPAN_GYOUSHA_CD.Text;
            }
        }

        /// <summary>
        /// 車輌CDEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            // 車輌CDEnter処理
            this.logic.sharyouCdEnter(sender, e);
        }

        /// <summary>
        /// 車輌有効性チェック 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.logic.oldSharyouCD == this.SHARYOU_CD.Text && !string.IsNullOrEmpty(this.SHARYOU_NAME_RYAKU.Text))
                {
                    return;
                }
                if (!this.logic.CheckSharyouCd())
                {
                    // フォーカス設定
                    this.SHARYOU_CD.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHARYOU_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索ポップアップ共に車輌は車種では絞り込まないよう修正
        /// </summary>
        public void sharyoPopBefore()
        {
            if (!this.isFukusuPop)
            {
                this.beforeSharyouCD = this.SHARYOU_CD.Text;
            }
            else
            {
                this.beforeSharyouCD = string.Empty; ;
            }
            this.beforeShasyuCD = this.SHASHU_CD.Text;
            this.beforeShasyuName = this.SHASHU_NAME.Text;
            this.isFukusuPop = false;
        }

        /// <summary>
        /// 検索ポップアップ共に車輌は車種では絞り込まないよう修正
        /// </summary>
        public void sharyoPopAfter()
        {
            this.SHASHU_CD.Text = this.beforeShasyuCD;
            this.SHASHU_NAME.Text = this.beforeShasyuName;
            if (!string.IsNullOrEmpty(this.SHARYOU_CD.Text) && this.beforeSharyouCD != this.SHARYOU_CD.Text)
            {
                this.logic.CheckSharyouCd();
            }
        }

        /// <summary>
        /// 運転者CD(FocusOutCheckMethodと併用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHAIN_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.SHAIN_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.SHAIN_CD.Text))
                {
                    return;
                }

                this.logic.UNTENSHA_CDValidated();

            }
            catch (Exception ex)
            {
                LogUtility.Error("SHAIN_CD_Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        ///  業者CD有効性チェック 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 業者CD
                string pGosyaCd = this.GYOUSHA_CD.Text.ToString().Trim();

                // 業者CD変更チェック
                if (!this.logic.CheckGyoushaChange())
                {
                    return;
                }

                //業者情報取得
                bool catchErr = true;
                var ret = this.logic.GetGyousyaInfo(pGosyaCd, out catchErr);
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
        /// 業者CD変更チェック
        /// </summary>
        public void GyoushaCdPopUpAfter()
        {
            this.logic.CheckGyoushaChange();
        }

        /// <summary>
        /// 現場CD有効性チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
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
                var ret = this.logic.GetGenbaInfo(pGenbaCd, pGyousyaCD, out catchErr);
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
                        var retGyo = this.logic.GetGyousyaInfo(this.GYOUSHA_CD.Text, out catchErr);
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

        #endregion

        /// <summary>
        /// [1]稼働状況表示ボタンクリック
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.bt_process1_Click(sender, e);
            
            LogUtility.DebugMethodEnd();
        }

    }
}