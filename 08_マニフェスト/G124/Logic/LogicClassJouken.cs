using System;
using System.Data;
using System.Windows.Forms;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.PaperManifest.ManifestCheckHyo.APP;
using r_framework.Dao;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.ManifestCheckHyo
{
    /// <summary> ビジネスロジック </summary>
    internal class LogicClassJouken : IBuisinessLogic
    {
        /// <summary> Form </summary>
        private JoukenPopupForm form;

        private MessageBoxShowLogic MsgBox;

        private IM_KYOTENDao kyotenDao;
        private IM_GYOUSHADao gyosyaDao;
        private IM_GENBADao genbaDao;
        private IM_CHIIKIDao chiikiDao;

        /// <summary> コンストラクタ </summary>
        public LogicClassJouken(JoukenPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フィールドの初期化
            this.form = targetForm;
            this.MsgBox = new MessageBoxShowLogic();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.gyosyaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.chiikiDao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary> 論理削除処理 </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary> 物理削除処理 </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary> 登録処理 </summary>
        /// <param name="errorFlag"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary> 検索処理 </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary> 更新処理 </summary>
        /// <param name="errorFlag"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary> window初期化 </summary>
        /// <param name="joken">joken</param>
        internal bool WindowInit(JoukenParam joken)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(joken);

                // イベントの初期化処理
                this.EventInit();

                // 画面の初期化
                this.form.CustomNumericTextBox2CheckTaishou.Text = joken.CkTaisyou;
                this.form.CustomNumericTextBox2CheckJouken.Text = joken.CkJouken;
                this.form.customDateTimePickerHidukeHaniShiteiStart.Text = joken.NengappiFrom;
                this.form.customDateTimePickerHidukeHaniShiteiEnd.Text = joken.NengappiTo;
                this.form.CustomNumericTextBox2CheckBunrui.Text = joken.CkBunrui;
                // 20140623 ria EV004852 一覧と抽出条件の変更 start
                this.form.cbxCheckItem1.Checked = joken.CkItem[0];
                this.form.cbxCheckItem2.Checked = joken.CkItem[1];
                this.form.cbxCheckItem3.Checked = joken.CkItem[2];
                this.form.cbxCheckItem4.Checked = joken.CkItem[3];
                this.form.cbxCheckItem5.Checked = joken.CkItem[4];
                this.form.cbxCheckItem6.Checked = joken.CkItem[5];
                this.form.cbxCheckItem7.Checked = joken.CkItem[6];
                this.form.cbxCheckItem8.Checked = joken.CkItem[7];
                this.form.cbxCheckItem9.Checked = joken.CkItem[8];
                this.form.cbxCheckItem10.Checked = joken.CkItem[9];
                this.form.cbxCheckItem11.Checked = joken.CkItem[10];
                this.form.cbxCheckItem12.Checked = joken.CkItem[11];
                this.form.cbxCheckItem13.Checked = joken.CkItem[12];
                this.form.cbxCheckItem14.Checked = joken.CkItem[13];
                this.form.cbxCheckItem15.Checked = joken.CkItem[14];
                // 20140623 ria EV004852 一覧と抽出条件の変更 end
                this.form.cbxCheckItem16.Checked = joken.CkItem[15];
                this.form.cbxCheckItem17.Checked = joken.CkItem[16];
                this.form.cbxCheckItem18.Checked = joken.CkItem[17];
                this.form.cbxCheckItem19.Checked = joken.CkItem[18];
                this.form.cbxCheckItem20.Checked = joken.CkItem[19];
                this.form.cbxCheckItem21.Checked = joken.CkItem[20];
                this.form.cbxCheckItem22.Checked = joken.CkItem[21];
                this.form.cbxCheckItem23.Checked = joken.CkItem[22];
                this.form.cbxCheckItem24.Checked = joken.CkItem[23];
                this.form.cbxCheckItem25.Checked = joken.CkItem[24];
                this.form.cbxCheckItem26.Checked = joken.CkItem[25];
                this.form.cbxCheckItem27.Checked = joken.CkItem[26];
                this.form.cbxCheckItem28.Checked = joken.CkItem[27];

                this.form.KYOTEN_CD.Text = joken.CkKyoten;
                this.KyoTenCheck(this.form.KYOTEN_CD.Text);

                this.form.JYOUKEN_DELETE_FLG.Checked = joken.CkDeleteFlg;

                if (!string.IsNullOrEmpty(this.form.CustomNumericTextBox2CheckTaishou.Text))
                {
                    if (this.form.CustomNumericTextBox2CheckTaishou.Text == "1")
                    {
                        this.form.JYOUKEN_DELETE_FLG.Enabled = false;
                    }
                    else
                    {
                        this.form.JYOUKEN_DELETE_FLG.Enabled = true;
                    }
                }

                this.form.JYOUKEN_YOYAKU_FLG.Checked = joken.CkYoyakuFlg;

                if (!string.IsNullOrEmpty(this.form.CustomNumericTextBox2CheckBunrui.Text))
                {
                    if (this.form.CustomNumericTextBox2CheckBunrui.Text == "1" || this.form.CustomNumericTextBox2CheckBunrui.Text == "2" || this.form.CustomNumericTextBox2CheckBunrui.Text == "3" )
                    {
                        this.form.JYOUKEN_YOYAKU_FLG.Enabled = false;
                    }
                    else
                    {
                        this.form.JYOUKEN_YOYAKU_FLG.Enabled = true;
                    }
                }

                this.form.cantxt_UnpanJyutakuNameCd.Text = joken.CkUPNCD;
                this.UPNCheck(this.form.cantxt_UnpanJyutakuNameCd.Text);
                this.form.cantxt_SyobunJyutakuNameCd.Text = joken.CkSBNJCD;
                this.form.cantxt_UnpanJyugyobaNameCd.Text = joken.CkSBNBCD;
                this.SBNCheck(this.form.cantxt_SyobunJyutakuNameCd.Text, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                this.form.CHIIKI_CD.Text = joken.CkChiikiCD;
                this.ChiikiCheck(this.form.CHIIKI_CD.Text);

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

        /// <summary> 設定値保存 </summary>
        internal bool SaveParams()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var info = new JoukenParam();
                // 初期設定条件を設定
                info.CkTaisyou = this.form.CustomNumericTextBox2CheckTaishou.Text;       // チェック対象
                info.CkJouken = this.form.CustomNumericTextBox2CheckJouken.Text;         // チェック条件
                info.CkKyoten = this.form.KYOTEN_CD.Text;                                // チェック拠点
                info.CkDeleteFlg = this.form.JYOUKEN_DELETE_FLG.Checked;                 // マスタ削除FLG
                info.CkYoyakuFlg = this.form.JYOUKEN_YOYAKU_FLG.Checked;                //電マニ予約FLG

                info.CkUPNCD = this.form.cantxt_UnpanJyutakuNameCd.Text;                //運搬受託者CD
                info.CkSBNJCD = this.form.cantxt_SyobunJyutakuNameCd.Text;              //処分受託者CD
                info.CkSBNBCD = this.form.cantxt_UnpanJyugyobaNameCd.Text;              //処分事業場CD

                // 開始日付
                if (this.form.customDateTimePickerHidukeHaniShiteiStart.Value != null)
                {
                    // nullではなく、かつ空文字列でもない
                    info.NengappiFrom = ((DateTime)this.form.customDateTimePickerHidukeHaniShiteiStart.Value).ToString("yyyy/MM/dd");
                    //string a = this.form.customDateTimePickerHidukeHaniShiteiStart.Value.ToString();
                }
                //else
                //{
                //    // null、もしくは空文字列である
                //    info.NengappiFrom = "1753/01/01";
                //}
                //終了日付
                if (this.form.customDateTimePickerHidukeHaniShiteiEnd.Value != null)
                {
                    // nullではなく、かつ空文字列でもない
                    info.NengappiTo = ((DateTime)this.form.customDateTimePickerHidukeHaniShiteiEnd.Value).ToString("yyyy/MM/dd");
                    //string a = this.form.customDateTimePickerHidukeHaniShiteiEnd.Value.ToString();

                }
                //else
                //{
                //    // null、もしくは空文字列である
                //    info.NengappiTo = "9998/12/31";
                //}
                info.CkBunrui = this.form.CustomNumericTextBox2CheckBunrui.Text;         // チェック分類
                info.CkChiikiCD = this.form.CHIIKI_CD.Text;                              // チェック地域
                // 20140623 ria EV004852 一覧と抽出条件の変更 start
                info.CkItem[0] = this.form.cbxCheckItem1.Checked;
                info.CkItem[1] = this.form.cbxCheckItem2.Checked;
                info.CkItem[2] = this.form.cbxCheckItem3.Checked;
                info.CkItem[3] = this.form.cbxCheckItem4.Checked;
                info.CkItem[4] = this.form.cbxCheckItem5.Checked;
                info.CkItem[5] = this.form.cbxCheckItem6.Checked;
                info.CkItem[6] = this.form.cbxCheckItem7.Checked;
                info.CkItem[7] = this.form.cbxCheckItem8.Checked;
                info.CkItem[8] = this.form.cbxCheckItem9.Checked;
                info.CkItem[9] = this.form.cbxCheckItem10.Checked;
                info.CkItem[10] = this.form.cbxCheckItem11.Checked;
                info.CkItem[11] = this.form.cbxCheckItem12.Checked;
                info.CkItem[12] = this.form.cbxCheckItem13.Checked;
                info.CkItem[13] = this.form.cbxCheckItem14.Checked;
                info.CkItem[14] = this.form.cbxCheckItem15.Checked;
                // 20140623 ria EV004852 一覧と抽出条件の変更 end
                info.CkItem[15] = this.form.cbxCheckItem16.Checked;
                info.CkItem[16] = this.form.cbxCheckItem17.Checked;
                info.CkItem[17] = this.form.cbxCheckItem18.Checked;
                info.CkItem[18] = this.form.cbxCheckItem19.Checked;
                info.CkItem[19] = this.form.cbxCheckItem20.Checked;
                info.CkItem[20] = this.form.cbxCheckItem21.Checked;
                info.CkItem[21] = this.form.cbxCheckItem22.Checked;
                info.CkItem[22] = this.form.cbxCheckItem23.Checked;
                info.CkItem[23] = this.form.cbxCheckItem24.Checked;
                info.CkItem[24] = this.form.cbxCheckItem25.Checked;
                info.CkItem[25] = this.form.cbxCheckItem26.Checked;
                info.CkItem[26] = this.form.cbxCheckItem27.Checked;
                info.CkItem[27] = this.form.cbxCheckItem28.Checked;
                this.form.Joken = info;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveParams", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary> イベント初期化 </summary>
        private void EventInit()    
        {
            LogUtility.DebugMethodStart();

            // UIFormキーイベント生成
            this.form.KeyUp += new KeyEventHandler(this.form.ItemKeyUp);

            // 検索実行ボタン(F7)イベント生成
            this.form.btn_kensakujikkou.DialogResult = DialogResult.OK;
            this.form.btn_kensakujikkou.Click += new EventHandler(this.form.SearchExec);

            // キャンセルボタン(F12)イベント生成
            this.form.btn_cancel.DialogResult = DialogResult.Cancel;
            this.form.btn_cancel.Click += new EventHandler(this.form.FormClose);

            /// 20141128 Houkakou 「マニチェック表」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.customDateTimePickerHidukeHaniShiteiEnd.MouseDoubleClick += new MouseEventHandler(customDateTimePickerHidukeHaniShiteiEnd_MouseDoubleClick);
            /// 20141128 Houkakou 「マニチェック表」のダブルクリックを追加する　end

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // チェック条件「6.紐付不整合チェック」を非表示

            this.form.CustomNumericTextBox2CheckJouken.RangeSetting.Max = 5;

            this.form.customRadioButtonCheckJouken6.Visible = false;

            this.form.lblShobunJ.Visible = false;
            this.form.cantxt_SyobunJyutakuNameCd.Visible = false;
            this.form.ctxt_SyobunJyutakuName.Visible = false;
            this.form.lblShobunJou.Visible = false;
            this.form.cantxt_UnpanJyugyobaNameCd.Visible = false;
            this.form.ctxt_UnpanJyugyobaName.Visible = false;

        }
 
        /// 20141022 Houkakou 「マニチェック表」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            bool isErr = false;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.customDateTimePickerHidukeHaniShiteiStart.BackColor = Constans.NOMAL_COLOR;
                this.form.customDateTimePickerHidukeHaniShiteiEnd.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.customDateTimePickerHidukeHaniShiteiStart.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.customDateTimePickerHidukeHaniShiteiEnd.Text))
                {
                    return isErr;
                }

                DateTime date_from = DateTime.Parse(this.form.customDateTimePickerHidukeHaniShiteiStart.Text);
                DateTime date_to = DateTime.Parse(this.form.customDateTimePickerHidukeHaniShiteiEnd.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.customDateTimePickerHidukeHaniShiteiStart.IsInputErrorOccured = true;
                    this.form.customDateTimePickerHidukeHaniShiteiEnd.IsInputErrorOccured = true;
                    this.form.customDateTimePickerHidukeHaniShiteiStart.BackColor = Constans.ERROR_COLOR;
                    this.form.customDateTimePickerHidukeHaniShiteiEnd.BackColor = Constans.ERROR_COLOR;
                    if (this.form.CustomNumericTextBox2CheckJouken.Text.Equals("1"))
                    {
                        msgLogic.MessageBoxShow("E030", "交付年月日From", "交付年月日To");
                    }
                    else if (this.form.CustomNumericTextBox2CheckJouken.Text.Equals("2"))
                    {
                        msgLogic.MessageBoxShow("E030", "運搬終了日From", "運搬終了日To");
                    }
                    else if (this.form.CustomNumericTextBox2CheckJouken.Text.Equals("3"))
                    {
                        msgLogic.MessageBoxShow("E030", "処分終了日From", "処分終了日To");
                    }
                    else if (this.form.CustomNumericTextBox2CheckJouken.Text.Equals("4"))
                    {
                        msgLogic.MessageBoxShow("E030", "最終処分終了日From", "最終処分終了日To");
                    }
                    else if (this.form.CustomNumericTextBox2CheckJouken.Text.Equals("5"))
                    {
                        msgLogic.MessageBoxShow("E030", "交付年月日なしFrom", "交付年月日なしTo");
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E030", "紐付け不整合チェックFrom", "紐付け不整合チェックTo");
                    }
                    this.form.customDateTimePickerHidukeHaniShiteiStart.Focus();
                    isErr = true;
                }
                else if (this.form.CHIIKI_CD.Enabled)
                {
                    //処分方法の場合提出先が必須条件
                    if (string.IsNullOrEmpty(this.form.CHIIKI_CD.Text))
                    {
                        msgLogic.MessageBoxShow("E012", "処分方法のチェックの為提出先");
                        this.form.CHIIKI_CD.Focus();
                        isErr = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }
        #endregion
        /// 20141022 Houkakou 「マニチェック表」の日付チェックを追加する　end

        /// 20141128 Houkakou 「マニチェック表」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDateTimePickerHidukeHaniShiteiEnd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.customDateTimePickerHidukeHaniShiteiStart;
            var ToTextBox = this.form.customDateTimePickerHidukeHaniShiteiEnd;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「マニチェック表」のダブルクリックを追加する　end

        /// <summary>
        /// 拠点CDの存在チェック
        /// </summary>
        public void KyoTenCheck(string KyotenCd)
        {
            if (!string.IsNullOrEmpty(KyotenCd))
            {
               M_KYOTEN entity = this.kyotenDao.GetDataByCd(KyotenCd);

               if (entity != null)
               {
                   this.form.KYOTEN_NAME.Text = entity.KYOTEN_NAME_RYAKU;
               }
               else
               {
                   this.form.KYOTEN_CD.Text = string.Empty;
                   this.form.KYOTEN_NAME.Text = string.Empty;
               }
            }
        }

        /// <summary>
        /// 運搬受託者CDの存在チェック
        /// </summary>
        public int UPNCheck(string UpanCd)
        {
            int ret = 0;

            this.form.ctxt_UnpanJyutakuName.Text = string.Empty;

            string a = "SELECT * FROM M_GYOUSHA WHERE GYOUSHA_CD = '" + UpanCd + "' AND JISHA_KBN = 1 AND UNPAN_JUTAKUSHA_KAISHA_KBN = 1 AND GYOUSHAKBN_MANI = 1";

            if (!string.IsNullOrEmpty(UpanCd))
            {
                DataTable entity = this.gyosyaDao.GetDateForStringSql(a);
                if (entity.Rows.Count != 0)
                {
                    this.form.ctxt_UnpanJyutakuName.Text = Convert.ToString(entity.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                }
                else
                {
                    ret = 1;
                }
            }
            return ret;
        }

        /// <summary>
        /// 処分受託者/事業場CDの存在チェック
        /// </summary>
        public int SBNCheck(string SbnJCd, string SbnBCd)
        {
            int ret = 0;

            this.form.ctxt_SyobunJyutakuName.Text = string.Empty;
            this.form.ctxt_UnpanJyugyobaName.Text = string.Empty;

            string a = "SELECT * FROM M_GYOUSHA WHERE GYOUSHA_CD = '" + SbnJCd + "' AND JISHA_KBN = 1 AND SHOBUN_NIOROSHI_GYOUSHA_KBN = 1 AND GYOUSHAKBN_MANI = 1";

            if (!string.IsNullOrEmpty(SbnJCd))
            {
                DataTable entity = this.gyosyaDao.GetDateForStringSql(a);

                if (entity.Rows.Count != 0)
                {
                    this.form.ctxt_SyobunJyutakuName.Text = Convert.ToString(entity.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                }
                else
                {
                    ret = 1;
                }

                if (!string.IsNullOrEmpty(SbnBCd))
                {
                    a = "SELECT * FROM M_GENBA WHERE GYOUSHA_CD = '" + SbnJCd + "' AND GENBA_CD = '" + SbnBCd + "' AND JISHA_KBN = 1";
                    DataTable entityb = this.genbaDao.GetDateForStringSql(a);

                    if (entityb.Rows.Count != 0)
                    {
                        this.form.ctxt_UnpanJyugyobaName.Text = Convert.ToString(entityb.Rows[0]["GENBA_NAME_RYAKU"]);
                    }
                    else
                    {
                        ret = 2;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 地域CDの存在チェック
        /// </summary>
        public void ChiikiCheck(string ChiikiCd)
        {
            if (!string.IsNullOrEmpty(ChiikiCd))
            {
                M_CHIIKI entity = this.chiikiDao.GetDataByCd(ChiikiCd);
                if (entity != null)
                {
                    this.form.CHIIKI_NAME.Text = entity.CHIIKI_NAME_RYAKU;
                }
                else
                {
                    this.form.CHIIKI_CD.Text = string.Empty;
                    this.form.CHIIKI_NAME.Text = string.Empty;
                }
            }
        }
    }
}
