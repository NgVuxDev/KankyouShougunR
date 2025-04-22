using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.PaperManifest.Manifestmeisaihyo
{
    /// <summary>
    /// マニフェスト明細表 条件入力ポップアップロジッククラス
    /// </summary>
    internal class JoukenPopupLogic
    {
        /// <summary>
        /// 条件ポップアップフォーム
        /// </summary>
        private JoukenPopupForm form;

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public JoukenPopupLogic(JoukenPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // イベントの初期化処理
                this.EventInit();

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

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            this.form.btn_kensakujikkou.Click += new EventHandler(this.form.btn_kensakujikkou_Click);
            this.form.btn_cancel.Click += new EventHandler(this.form.btn_cancel_Click);

            /// 20141127 Houkakou 「運マニ明細表」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);

            this.form.KOFU_DATE_TO.MouseDoubleClick += new MouseEventHandler(KOFU_DATE_TO_MouseDoubleClick);
            this.form.UNPAN_DATE_TO.MouseDoubleClick += new MouseEventHandler(UNPAN_DATE_TO_MouseDoubleClick);
            this.form.SHOBUN_END_DATE_TO.MouseDoubleClick += new MouseEventHandler(SHOBUN_END_DATE_TO_MouseDoubleClick);
            this.form.LAST_SHOBUN_END_DATE_TO.MouseDoubleClick += new MouseEventHandler(LAST_SHOBUN_END_DATE_TO_MouseDoubleClick);

            this.form.HAISHUTSU_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(HAISHUTSU_GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.HAISHUTSU_GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(HAISHUTSU_GENBA_CD_TO_MouseDoubleClick);
            this.form.UNPAN_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(UNPAN_GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.SHOBUN_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(SHOBUN_GYOUSHA_CD_TO_MouseDoubleClick);

            this.form.SHOBUN_GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(SHOBUN_GENBA_CD_TO_MouseDoubleClick);
            this.form.LAST_SHOBUN_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(LAST_SHOBUN_GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.LAST_SHOBUN_GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(LAST_SHOBUN_GENBA_CD_TO_MouseDoubleClick);
            this.form.HOUKOKUSHO_CD_TO.MouseDoubleClick += new MouseEventHandler(HOUKOKUSHO_CD_TO_MouseDoubleClick);

            this.form.HAIKIBUTSU_CD_TO.MouseDoubleClick += new MouseEventHandler(HAIKIBUTSU_CD_TO_MouseDoubleClick);
            this.form.NISUGATA_CD_TO.MouseDoubleClick += new MouseEventHandler(NISUGATA_CD_TO_MouseDoubleClick);
            this.form.SHOBUN_HOUHOU_CD_TO.MouseDoubleClick += new MouseEventHandler(SHOBUN_HOUHOU_CD_TO_MouseDoubleClick);
            this.form.TORIHIKISAKI_CD_TO.MouseDoubleClick += new MouseEventHandler(TORIHIKISAKI_CD_TO_MouseDoubleClick);
            /// 20141127 Houkakou 「マニ明細表」のダブルクリックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 抽出範囲 「一次二次区分」「二次紐付有無」項目を非表示
            this.form.label5.Visible = false;
            this.form.customPanel7.Visible = false;
            this.form.label20.Visible = false;
            this.form.customPanel8.Visible = false;

            // Location調整
            // 交付年月日
            LocationAdjustmentForManiLite(this.form.label8);
            LocationAdjustmentForManiLite(this.form.KOFU_DATE_FROM);
            LocationAdjustmentForManiLite(this.form.label38);
            LocationAdjustmentForManiLite(this.form.KOFU_DATE_TO);

            // 運搬終了日
            LocationAdjustmentForManiLite(this.form.label18);
            LocationAdjustmentForManiLite(this.form.UNPAN_DATE_FROM);
            LocationAdjustmentForManiLite(this.form.label39);
            LocationAdjustmentForManiLite(this.form.UNPAN_DATE_TO);

            // 処分終了日
            LocationAdjustmentForManiLite(this.form.label19);
            LocationAdjustmentForManiLite(this.form.SHOBUN_END_DATE_FROM);
            LocationAdjustmentForManiLite(this.form.label40);
            LocationAdjustmentForManiLite(this.form.SHOBUN_END_DATE_TO);

            // 最終処分終了日
            LocationAdjustmentForManiLite(this.form.label23);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_END_DATE_FROM);
            LocationAdjustmentForManiLite(this.form.label41);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_END_DATE_TO);

            // 排出事業者
            LocationAdjustmentForManiLite(this.form.label9);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.lblSyukeiKomoku1Kara);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_CD_TO);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_TO_POPUP);

            // 排出事業場
            LocationAdjustmentForManiLite(this.form.label10);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label27);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_CD_TO);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_TO_POPUP);

            // 運搬受託者
            LocationAdjustmentForManiLite(this.form.label11);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label28);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_CD_TO);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_TO_POPUP);

            // 処分受託者
            LocationAdjustmentForManiLite(this.form.label12);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label29);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_CD_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_TO_POPUP);

            // 処分事業場
            LocationAdjustmentForManiLite(this.form.label13);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label30);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_CD_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_TO_POPUP);

            // 最終処分業者
            LocationAdjustmentForManiLite(this.form.label21);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label35);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_CD_TO);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_TO_POPUP);

            // 最終処分場所
            LocationAdjustmentForManiLite(this.form.label22);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label36);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_CD_TO);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_TO_POPUP);

            // 廃棄物種類(報告書分類)
            LocationAdjustmentForManiLite(this.form.label14);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_CD_FROM);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label31);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_CD_TO);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_NAME_TO);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_TO_POPUP);

            // 廃棄物名称
            LocationAdjustmentForManiLite(this.form.label15);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_CD_FROM);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label32);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_CD_TO);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_NAME_TO);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_TO_POPUP);

            // 荷姿
            LocationAdjustmentForManiLite(this.form.label16);
            LocationAdjustmentForManiLite(this.form.NISUGATA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.NISUGATA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.NISUGATA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label33);
            LocationAdjustmentForManiLite(this.form.NISUGATA_CD_TO);
            LocationAdjustmentForManiLite(this.form.NISUGATA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.NISUGATA_TO_POPUP);

            // 処分方法
            LocationAdjustmentForManiLite(this.form.label17);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_CD_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label34);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_CD_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_NAME_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_TO_POPUP);

            // 取引先
            LocationAdjustmentForManiLite(this.form.label24);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_CD_FROM);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label37);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_CD_TO);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_NAME_TO);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_TO_POPUP);

            this.form.customPanel2.Size = new System.Drawing.Size(this.form.customPanel2.Size.Width, this.form.customPanel2.Size.Height - 24);

            // 並び順
            LocationAdjustmentForManiLite(this.form.customPanel3);

            // 集計単位
            LocationAdjustmentForManiLite(this.form.customPanel4);
        }

        /// <summary>
        /// マニライト用にLocation調整
        /// </summary>
        /// <param name="ctrl"></param>
        private void LocationAdjustmentForManiLite(Control ctrl)
        {
            ctrl.Location = new System.Drawing.Point(ctrl.Location.X, ctrl.Location.Y - 24);
        }


        /// 20141022 Houkakou 「マニ明細表」の日付チェックを追加する　start
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
                this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                {
                    return isErr;
                }

                DateTime date_from = DateTime.Parse(this.form.HIDUKE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.HIDUKE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                    this.form.HIDUKE_TO.IsInputErrorOccured = true;
                    this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "入力日付From", "入力日付To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.HIDUKE_FROM.Focus();
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

        /// <summary>
        /// 交付日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool KofuDateCheck()
        {
            bool isErr = false;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                this.form.KOFU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.KOFU_DATE_TO.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.KOFU_DATE_FROM.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.KOFU_DATE_TO.Text))
                {
                    return isErr;
                }

                DateTime date_from = DateTime.Parse(this.form.KOFU_DATE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.KOFU_DATE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.KOFU_DATE_FROM.IsInputErrorOccured = true;
                    this.form.KOFU_DATE_TO.IsInputErrorOccured = true;
                    this.form.KOFU_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.KOFU_DATE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "交付年月日From", "交付年月日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.KOFU_DATE_FROM.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("KofuDateCheck", ex);
                msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }

        /// <summary>
        /// 運搬日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool UnpanDateCheck()
        {
            bool isErr = false;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                this.form.UNPAN_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.UNPAN_DATE_TO.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.UNPAN_DATE_FROM.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.UNPAN_DATE_TO.Text))
                {
                    return isErr;
                }

                DateTime date_from = DateTime.Parse(this.form.UNPAN_DATE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.UNPAN_DATE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.UNPAN_DATE_FROM.IsInputErrorOccured = true;
                    this.form.UNPAN_DATE_TO.IsInputErrorOccured = true;
                    this.form.UNPAN_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.UNPAN_DATE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "運搬終了日From", "運搬終了日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.UNPAN_DATE_FROM.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UnpanDateCheck", ex);
                msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }

        /// <summary>
        /// 処分日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool ShobunDateCheck()
        {
            bool isErr = false;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                this.form.SHOBUN_END_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.SHOBUN_END_DATE_TO.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.SHOBUN_END_DATE_FROM.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.SHOBUN_END_DATE_TO.Text))
                {
                    return isErr;
                }

                DateTime date_from = DateTime.Parse(this.form.SHOBUN_END_DATE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.SHOBUN_END_DATE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.SHOBUN_END_DATE_FROM.IsInputErrorOccured = true;
                    this.form.SHOBUN_END_DATE_TO.IsInputErrorOccured = true;
                    this.form.SHOBUN_END_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.SHOBUN_END_DATE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "処分終了日From", "処分終了日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.SHOBUN_END_DATE_FROM.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShobunDateCheck", ex);
                msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }

        /// <summary>
        /// 最終処分日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool LastShobunDateCheck()
        {
            bool isErr = false;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                this.form.LAST_SHOBUN_END_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.LAST_SHOBUN_END_DATE_TO.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.LAST_SHOBUN_END_DATE_FROM.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.LAST_SHOBUN_END_DATE_TO.Text))
                {
                    return isErr;
                }

                DateTime date_from = DateTime.Parse(this.form.LAST_SHOBUN_END_DATE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.LAST_SHOBUN_END_DATE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.LAST_SHOBUN_END_DATE_FROM.IsInputErrorOccured = true;
                    this.form.LAST_SHOBUN_END_DATE_TO.IsInputErrorOccured = true;
                    this.form.LAST_SHOBUN_END_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.LAST_SHOBUN_END_DATE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "最終処分終了日From", "最終処分終了日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.LAST_SHOBUN_END_DATE_FROM.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LastShobunDateCheck", ex);
                msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }
        #endregion
        /// 20141022 Houkakou 「マニ明細表」の日付チェックを追加する　end

        /// 20141127 Houkakou 「マニ明細表」のダブルクリックを追加する　start
        #region HIDUKE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HIDUKE_FROM;
            var ToTextBox = this.form.HIDUKE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region KOFU_DATE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOFU_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.KOFU_DATE_FROM;
            var ToTextBox = this.form.KOFU_DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region UNPAN_DATE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.UNPAN_DATE_FROM;
            var ToTextBox = this.form.UNPAN_DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHOBUN_END_DATE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOBUN_END_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHOBUN_END_DATE_FROM;
            var ToTextBox = this.form.SHOBUN_END_DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region LAST_SHOBUN_END_DATE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LAST_SHOBUN_END_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.LAST_SHOBUN_END_DATE_FROM;
            var ToTextBox = this.form.LAST_SHOBUN_END_DATE_TO;
                
            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region HAISHUTSU_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHUTSU_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HAISHUTSU_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.HAISHUTSU_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.HAISHUTSU_GYOUSHA_NAME_TO.Text = this.form.HAISHUTSU_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region HAISHUTSU_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHUTSU_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HAISHUTSU_GENBA_CD_FROM;
            var ToTextBox = this.form.HAISHUTSU_GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.HAISHUTSU_GENBA_NAME_TO.Text = this.form.HAISHUTSU_GENBA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region UNPAN_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.UNPAN_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.UNPAN_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.UNPAN_GYOUSHA_NAME_TO.Text = this.form.UNPAN_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHOBUN_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOBUN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHOBUN_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.SHOBUN_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.SHOBUN_GYOUSHA_NAME_TO.Text = this.form.SHOBUN_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHOBUN_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOBUN_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHOBUN_GENBA_CD_FROM;
            var ToTextBox = this.form.SHOBUN_GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.SHOBUN_GENBA_NAME_TO.Text = this.form.SHOBUN_GENBA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region LAST_SHOBUN_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LAST_SHOBUN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.LAST_SHOBUN_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.LAST_SHOBUN_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.LAST_SHOBUN_GYOUSHA_NAME_TO.Text = this.form.LAST_SHOBUN_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region LAST_SHOBUN_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LAST_SHOBUN_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.LAST_SHOBUN_GENBA_CD_FROM;
            var ToTextBox = this.form.LAST_SHOBUN_GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.LAST_SHOBUN_GENBA_NAME_TO.Text = this.form.LAST_SHOBUN_GENBA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region HOUKOKUSHO_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HOUKOKUSHO_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HOUKOKUSHO_CD_FROM;
            var ToTextBox = this.form.HOUKOKUSHO_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.HOUKOKUSHO_NAME_TO.Text = this.form.HOUKOKUSHO_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region HAIKIBUTSU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKIBUTSU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HAIKIBUTSU_CD_FROM;
            var ToTextBox = this.form.HAIKIBUTSU_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.HAIKIBUTSU_NAME_TO.Text = this.form.HAIKIBUTSU_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NISUGATA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NISUGATA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NISUGATA_CD_FROM;
            var ToTextBox = this.form.NISUGATA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NISUGATA_NAME_TO.Text = this.form.NISUGATA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHOBUN_HOUHOU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOBUN_HOUHOU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHOBUN_HOUHOU_CD_FROM;
            var ToTextBox = this.form.SHOBUN_HOUHOU_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.SHOBUN_HOUHOU_NAME_TO.Text = this.form.SHOBUN_HOUHOU_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region TORIHIKISAKI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.TORIHIKISAKI_CD_FROM;
            var ToTextBox = this.form.TORIHIKISAKI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.TORIHIKISAKI_NAME_TO.Text = this.form.TORIHIKISAKI_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141127 Houkakou 「マニ明細表」のダブルクリックを追加する　end
    }
}
