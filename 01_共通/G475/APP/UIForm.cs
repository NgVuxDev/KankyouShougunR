// $Id: UIForm.cs 52350 2015-06-15 11:08:20Z minhhoang@e-mall.co.jp $
using System;
using System.ComponentModel;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Utility;


namespace Shougun.Core.Common.ItakuKeiyakuSearch
{
    public partial class ItakuKeiyakuSearchForm : SuperForm
    {
        /// <summary>
        /// 現場マスタのDao
        /// </summary>
        private IM_GENBADao Dao;

        internal HeaderForm header;
        internal bool haishutsuShaKbn;


        /// <summary>
        /// 戻り値用変数
        /// </summary>
        public string retHaishutsuShaCD = "";
        public string retHaishutsuJouCD = "";
        public string retUnpanShaCD = "";
        public string retShobunShaCD = "";
        public string retShobunJouCD = "";
        public string retSaishuuShobunShaCD = "";
        public string retSaishuuShobunJouCD = "";

        /// <summary>
        /// 初期変数
        /// </summary>
        public string initHstGyoushaCd = "";
        public string initHstGyoushaName = "";
        public string initHstGenbaCd = "";
        public string initHstGenbaName = "";
        public string initUpnGyoushaCd = "";
        public string initUpnGyoushaName = "";
        public string initSbnGenbaCd = "";
        public string initSbnGenbaName = "";

        private string previousGyousha { get; set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private ItakuKeiyakuSearchLogic logic;
        #region フォーム設定
        public ItakuKeiyakuSearchForm(HeaderForm headerForm)
            : base(WINDOW_ID.C_ITAKU_KEIYAKU_JOUHOU_KENSAKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ItakuKeiyakuSearchLogic(this);

            //ヘッダ
            this.header = headerForm;
            this.logic.SetHeaderInfo(this.header);
            tb_JigyoushaCd.BackColor = Constans.ERROR_COLOR;
            Dao = DaoInitUtility.GetComponent<IM_GENBADao>();
            
        }
        #endregion
        #region 画面コントロールイベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnLoad(e);

                // 初期情報保存
                this.initHstGyoushaCd = this.tb_JigyoushaCd.Text;
                this.initHstGyoushaName = this.tb_JigyoushaName.Text;
                this.initHstGenbaCd = this.tb_JigyouJouCd.Text;
                this.initHstGenbaName = this.tb_JigyouJouName.Text;
                this.initUpnGyoushaCd = this.tb_UnpanshaCd.Text;
                this.initUpnGyoushaName = this.tb_UnpanshaName.Text;
                this.initSbnGenbaCd = this.tb_ShobunshaCd.Text;
                this.initSbnGenbaName = this.tb_ShobunshaName.Text;

                this.logic.WindowInit();        // 画面情報の初期化
                this.rdbStAll.Checked = true;
                this.rdbDayHidukeNasi.Checked = true;
                this.logic.gyoshaGetData(this.tb_JigyoushaCd.Text, 0);
                this.logic.gyoshaGetData(this.tb_UnpanshaCd.Text, 1);
                this.logic.gyoshaGetData(this.tb_ShobunshaCd.Text, 2);
                this.logic.genbaGetData(this.tb_JigyoushaCd.Text, this.tb_JigyouJouCd.Text);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 業者CDにより業者情報を取得
        ///// <summary>
        ///// 業者CDにより業者情報を取得
        ///// </summary>
        //public void GyoushaFORGyoushaCD_Select()
        //{
        //    LogUtility.DebugMethodStart();
        //    try
        //    {
        //        ////取引先CD
        //        //string torihikisaki_cd = this.TORIHIKISAKI_CD.Text;
        //        //業者CD
        //        string gyousha_cd = this.tb_JigyoushaCd.Text;

        //        if (null == gyousha_cd || "".Equals(gyousha_cd) || DBNull.Value.Equals(gyousha_cd))
        //        {
        //            //業者、現場が空にする
        //            this.logic.GyoushaCrear();
        //            this.logic.GenbaCrear();
        //        }
        //        else
        //        {
        //            //業者マスタ検索
        //            if (this.logic.GyoushaSearch() == 0)
        //            {
        //                var messageShowLogic = new MessageBoxShowLogic();
        //                messageShowLogic.MessageBoxShow("E020", "業者マスタ");
        //                //業者、現場を空欄にする
        //                this.logic.GyoushaCrear();
        //                this.logic.GenbaCrear();
        //                //フォーカス
        //                this.GYOUSHA_CD.Focus();

        //                LogUtility.DebugMethodEnd();
        //                return;
        //            }

        //            //表示
        //            this.logic.GyoushaSet();

        //            ////フッタの[F2取引先],[F3業者],[F4現場]を利用可にする
        //            //this.logic.F2F3F4_Enabled();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    LogUtility.DebugMethodEnd();
        //}
        #endregion
        #region 日付範囲チェンジイベント
        //日付範囲チェンジイベント
        private void txtDayHani_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.txtDayHani_TextChanged(sender, e);
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 排出事業者CDロストフォーカス
        private void tb_JigyoushaCd_lostFocus(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.HaishutsuShaLostFocus();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 運搬事業者CDロストフォーカス
        private void tb_UnpanCd_lostFocus(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.UnpanCdLostFocus();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 処分受託者ロストフォーカス
        private void tb_ShobunCd_lostFocus(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.ShobunCdLostFocus();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
   
        #region 排出事業者区分判定用
        public void JigyoushaPopupBef()
        {
            this.previousGyousha = this.tb_JigyoushaCd.Text;
        }

        public void SetJigyoushaKbn()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // 業者変更があれば現場を初期化する
                if (this.previousGyousha != this.tb_JigyoushaCd.Text)
                {
                    this.tb_JigyouJouCd.Text = string.Empty;
                    this.tb_JigyouJouName.Text = string.Empty;
                }

                this.tb_JigyoushaKbn.Text = "False";
                if (!this.tb_JigyoushaCd.Text.Equals(""))
                {
                    this.tb_JigyoushaKbn.Text = "True";
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 運搬事業者区分判定用
        public void SetUnpanshaKbn()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.tb_UnpanshaKbn.Text = "False";
                if (!this.tb_UnpanshaCd.Text.Equals(""))
                {
                    this.tb_UnpanshaKbn.Text = "True";
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 処分受託者区分判定用
        public void SetShobunshaKbn()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.tb_ShobunshaKbn.Text = "False";
                if (!this.tb_ShobunshaCd.Text.Equals(""))
                {
                    this.tb_ShobunshaKbn.Text = "True";
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 排出事業場区分判定用
        public void SetJigyouJouKbn()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var msgLogic = new MessageBoxShowLogic();
                if (this.tb_JigyoushaCd.Text.Equals(""))
                {
                    this.tb_UnpanshaName.Text = string.Empty;
                    msgLogic.MessageBoxShow("E001", "排出事業者");

                    this.tb_JigyouJouCd.Text = string.Empty;
                    this.tb_JigyouJouName.Text = string.Empty;
                    this.tb_JigyoushaCd.Focus();
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 日付Fromロストフォーカス
        private void dtpDayFrom_LostFocus(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.DayFromLostFocus(e);
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 日付Toロストフォーカス
        private void dtpDayTo_LostFocus(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.DayToLostFocus(e);
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 20150615 #1629 [F9]確定ボタンだけでなくダブルクリックでも一覧を選択することが出来るようにする。
        private void customDataGridView1_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var parentForm = (BusinessBaseForm)this.Parent;
                parentForm.bt_func9.PerformClick();
            }
        }
        #endregion

        public string retKeiyakuNumber = "";//PhuocLoc 2022/01/18 #158902
        public string retSystemId = string.Empty;//#160047 20220328 CongBinh

        /// <summary>
        /// 排出事業場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tb_JigyouJouCd_Validated(object sender, EventArgs e)
        {
            this.logic.JigyouJouCdValidated();
        }

        /// <summary>
        /// 排出事業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tb_JigyoushaCd_Enter(object sender, EventArgs e)
        {
            this.logic.gyoshaCd_Def = this.tb_JigyoushaCd.Text;
        }
    }
}
