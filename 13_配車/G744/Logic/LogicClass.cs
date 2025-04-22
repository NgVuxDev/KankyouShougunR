using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Allocation.CarTransferTeiki
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.CarTransferTeiki.Setting.ButtonSetting.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass searchDto;
        internal PopupDAOCls popupDao;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// (配車)伝票番号
        /// </summary>
        internal string haishaDenpyouNo { get; set; }

        /// <summary>
        /// コース名称Dao
        /// </summary>
        private IM_COURSE_NAMEDao courseNameDao;

        /// <summary>
        /// コース名称データ
        /// </summary>
        public M_COURSE_NAME[] mCourseNameAll { get; set; }

        #region 定期
        /// <summary>
        /// 定期配車情報Dao
        /// </summary>
        private IT_TEIKI_HAISHA_ENTRYDao teikiHaishaDao;

        /// <summary>
        /// 定期配車荷降情報Dao
        /// </summary>
        private IT_TEIKI_HAISHA_NIOROSHIDao teikiHaishaNioroshiDao;

        /// <summary>
        /// 定期配車明細情報Dao
        /// </summary>
        private IT_TEIKI_HAISHA_DETAILDao teikiHaishaDetailDao;

        /// <summary>
        /// 定期配車詳細情報Dao
        /// </summary>
        private IT_TEIKI_HAISHA_SHOUSAIDao teikiHaishaShousaiDao;

        /// <summary>
        /// 定期配車実績entryのentityList
        /// </summary>
        private List<T_TEIKI_JISSEKI_ENTRY> entitysTeikiJisekiEntryList { get; set; }

        /// <summary>
        ///  定期配車実績detailのentityList
        /// </summary>
        private List<T_TEIKI_JISSEKI_DETAIL> entitysTeikiJisekiDetailList { get; set; }

        /// <summary>
        ///  定期配車実績荷卸しのentityList
        /// </summary>
        private List<T_TEIKI_JISSEKI_NIOROSHI> entitysTeikiJisekiNioroshiList { get; set; }
        #endregion

        #region 受付
        /// <summary>
        /// 受付（収集）入力のDao
        /// </summary>
        private T_UKETSUKE_SS_ENTRYDao daoUketsukeSSEntry;

        /// <summary>
        /// 受付（収集）明細のDao
        /// </summary>
        private T_UKETSUKE_SS_DETAILDao daoUketsukeSSDetail;

        /// <summary>
        /// コンテナ稼働予定のDao
        /// </summary>
        private T_CONTENA_RESERVEDao daoContenaReserver;

        /// <summary>
        /// 受付（出荷）入力のDao
        /// </summary>
        private T_UKETSUKE_SK_ENTRYDao daoUketsukeSKEntry;

        /// <summary>
        /// 受付（出荷）明細のDao
        /// </summary>
        private T_UKETSUKE_SK_DETAILDao daoUketsukeSKDetail;
        #endregion

        #region 現場メモ
        /// <summary>
        /// 現場メモEntryDao
        /// </summary>
        public GenbamemoEntryDAO genbamemoEntryDAO;

        /// <summary>
        /// 現場メモDetailDao
        /// </summary>
        public GenbamemoDetailDAO genbamemoDetailDAO;

        /// <summary>
        /// 現場メモDao
        /// </summary>
        private IM_FILE_LINK_GENBAMEMO_ENTRYDao fileLinkGenbamemoDao;

        /// <summary>
        /// 現場メモEntity
        /// </summary>
        public T_GENBAMEMO_ENTRY genbamemoEntry;

        /// <summary>
        /// 現場メモ詳細Entity
        /// </summary>
        public List<T_GENBAMEMO_DETAIL> genbamemoDetailList;
        #endregion

        #region モバイル
        /// <summary>
        /// モバイル将軍業務TBLのentity
        /// </summary>
        private T_MOBISYO_RT entitysMobisyoRt { get; set; }

        /// <summary>
        /// モバイル将軍業務詳細TBLのentity
        /// </summary>
        private T_MOBISYO_RT_DTL entitysMobisyoRtDTL { get; set; }

        /// <summary>
        /// モバイル将軍業務搬入TBLのentity
        /// </summary>
        private T_MOBISYO_RT_HANNYUU entitysMobisyoRtHN { get; set; }

        /// <summary>
        /// モバイル将軍業務TBLのDao
        /// </summary>
        private T_MOBISYO_RTDao TmobisyoRtDelDao;

        /// <summary>
        /// モバイル将軍業務TBLのDao
        /// </summary>
        private IT_MOBISYO_RTDao TmobisyoRtDao;

        /// <summary>
        /// モバイル将軍業務詳細TBLのDao
        /// </summary>
        private IT_MOBISYO_RT_DTLDao TmobisyoRtDTLDao;

        /// <summary>
        /// モバイル将軍業務搬入TBLのDao
        /// </summary>
        private IT_MOBISYO_RT_HANNYUUDao TmobisyoRtHNDao;

        /// <summary>
        /// モバイル将軍業務TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT> entitysMobisyoRtList { get; set; }

        /// <summary>
        /// モバイル将軍業務詳細TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT_DTL> entitysMobisyoRtDTLList { get; set; }

        /// <summary>
        /// モバイル将軍業務搬入TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT_HANNYUU> entitysMobisyoRtHNList { get; set; }

        /// <summary>
        /// モバイル将軍業務TBLのDELentityList
        /// </summary>
        private List<T_MOBISYO_RT> entitysMobisyoRtDELList { get; set; }
        #endregion

        /// <summary>
        /// 搬入先休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_HANNYUUSAKIDao workclosedhannyuusakiDao;

        /// <summary>
        /// 検索条件 : 定期配車番号
        /// </summary>
        public String TeikiHaishaNumber { get; set; }

        /// <summary>
        /// モバイル連携用データテーブル
        /// </summary>
        private DataTable ResultTable;

        /// <summary>
        /// モバイル連携用の伝票番号
        /// </summary>
        internal string Renkei_TeikiDetailSystemId;

        /// <summary>
        /// 配車番号の前回値を保持します
        /// </summary>
        internal string beforeHeadHaishaNumber = string.Empty;
        internal string beforeHeadCd = string.Empty;
        internal string beforeHaishaNumber = string.Empty;
        internal string beforeCd = string.Empty;

        internal bool SpaceChk = false;
        internal bool SpaceON = false;
        internal bool isInputError = false;

        internal MessageBoxShowLogic MsgBox;

        private GET_SYSDATEDao dateDao;

        #endregion

        #region プロパティ
        /// <summary>
        /// 検索結果（定期配車入力）
        /// </summary>
        public DataTable searchResultEntry { get; set; }
        /// <summary>
        /// 検索結果（定期配車詳細）
        /// </summary>
        public DataTable searchResultShousai { get; set; }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.searchDto = new DTOClass();
            this.popupDao = DaoInitUtility.GetComponent<PopupDAOCls>();
            this.courseNameDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_COURSE_NAMEDao>();
            this.teikiHaishaDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_ENTRYDao>();
            this.teikiHaishaNioroshiDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_NIOROSHIDao>();
            this.teikiHaishaDetailDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_DETAILDao>();
            this.teikiHaishaShousaiDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_SHOUSAIDao>();
            this.daoUketsukeSSEntry = DaoInitUtility.GetComponent<T_UKETSUKE_SS_ENTRYDao>();
            this.daoUketsukeSSDetail = DaoInitUtility.GetComponent<T_UKETSUKE_SS_DETAILDao>();
            this.daoContenaReserver = DaoInitUtility.GetComponent<T_CONTENA_RESERVEDao>();
            this.daoUketsukeSKEntry = DaoInitUtility.GetComponent<T_UKETSUKE_SK_ENTRYDao>();
            this.daoUketsukeSKDetail = DaoInitUtility.GetComponent<T_UKETSUKE_SK_DETAILDao>();
            this.genbamemoEntryDAO = DaoInitUtility.GetComponent<GenbamemoEntryDAO>();
            this.genbamemoDetailDAO = DaoInitUtility.GetComponent<GenbamemoDetailDAO>();
            this.fileLinkGenbamemoDao = DaoInitUtility.GetComponent<IM_FILE_LINK_GENBAMEMO_ENTRYDao>();
            this.workclosedhannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();

            this.TmobisyoRtDelDao = DaoInitUtility.GetComponent<T_MOBISYO_RTDao>();
            this.TmobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
            this.TmobisyoRtDTLDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_DTLDao>();
            this.TmobisyoRtHNDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_HANNYUUDao>();
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                
                // 親フォーム
                this.parentForm = this.form.Parent as BusinessBaseForm;
                //一括項目初期化
                this.form.checkBoxAll.Visible = false;
                this.form.checkBoxAll2.Visible = false;
                this.form.txtCourseCd.Text = string.Empty;
                this.form.txtCourseNm.Text = string.Empty;
                this.form.txtHaishaNo.Text = string.Empty;
                this.form.txtSagyouDate.Text = string.Empty;
                //ボタンのテキストを初期化
                this.ButtonInit();
                //イベントの初期化処理
                this.EventInit();
                this.TeikiHaishaNumber = this.haishaDenpyouNo;
                // 画面初期表示処理
                this.Search();

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

        #region ボタンの初期化
        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
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
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func1.Click -= new System.EventHandler(this.bt_func1_Click);       //詳細確認
                parentForm.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);       //詳細確認
                parentForm.bt_func9.Click -= new EventHandler(this.bt_func9_Click);              //実行
                parentForm.bt_func9.Click += new EventHandler(this.bt_func9_Click);              //実行
                parentForm.bt_func11.Click -= new EventHandler(this.bt_func11_Click);            //取消
                parentForm.bt_func11.Click += new EventHandler(this.bt_func11_Click);            //取消
                parentForm.bt_func12.Click -= new System.EventHandler(this.bt_func12_Click);     //閉じる
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
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
                LogUtility.Error("ButtonSetting", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F1 詳細確認
        /// <summary>
        /// F1 詳細確認
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.form.Ichiran.CurrentRow != null)
                {
                    string haishaKbn = "0";
                    string haishaDenpyouNo = this.form.Ichiran.CurrentRow.Cells["TEIKI_HAISHA_NUMBER"].Value.ToString();
                    r_framework.FormManager.FormManager.OpenFormModal("G668", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, haishaDenpyouNo, haishaKbn);
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E076");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region F9 実行
        /// <summary>
        /// F9 実行
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                #region 選択チェック
                bool taisyou = false;
                string Err_Msg = string.Empty;
                string Navi_Flg = string.Empty;
                string Logi_Flg = string.Empty;

                string inputSagyouDate = string.Empty;      //作業日
                string Err_holiday = string.Empty;          //休働エラー

                for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                {
                    if ((bool)this.form.Ichiran.Rows[i].Cells[1].Value)
                    {
                        #region データチェック
                        taisyou = true;
                        //振替先配車番号があるか
                        if ((this.form.Ichiran.Rows[i].Cells["HURI_HAISHA_NUMBER"].Value == null)
                            || (string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["HURI_HAISHA_NUMBER"].Value.ToString())))
                        {
                            this.MsgBox.MessageBoxShowError("振替先の配車番号が未入力です。\r\n確認してください。");
                            return;
                        }
                        if ((this.form.Ichiran.Rows[i].Cells["HURI_COURSE_CD"].Value == null)
                            || (string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["HURI_COURSE_CD"].Value.ToString())))
                        {
                            this.MsgBox.MessageBoxShowError("振替先のコースCDが未入力です。\r\n確認してください。");
                            return;
                        }
                        //振替元配車番号<>振替先配車番号か
                        if (this.form.Ichiran.Rows[i].Cells["HURI_HAISHA_NUMBER"].Value.ToString().Equals
                            (this.form.Ichiran.Rows[i].Cells["TEIKI_HAISHA_NUMBER"].Value.ToString()))
                        {
                            this.MsgBox.MessageBoxShowError("元の配車番号と振替先の配車番号が同じです。\r\n確認してください。");

                            return;
                        }
                        #endregion

                        #region 連携チェック
                        //振替元の配車伝票がロジコン連携されているか
                        if (this.RenkeiCheck(3, this.form.Ichiran.Rows[i].Cells["TEIKI_HAISHA_NUMBER"].Value.ToString()))
                        {
                            Err_Msg = Err_Msg + (i + 1) + "行目：定期配車番号 " + this.form.Ichiran.Rows[i].Cells["TEIKI_HAISHA_NUMBER"].Value.ToString() + "\r\n";
                            Logi_Flg = "ロジこんぱす";
                        }

                        //振替元の配車伝票がNAVITIME連携されているか
                        if (this.RenkeiCheck(4, this.form.Ichiran.Rows[i].Cells["TEIKI_HAISHA_NUMBER"].Value.ToString()))
                        {
                            Err_Msg = Err_Msg + (i + 1) + "行目：定期配車番号 " + this.form.Ichiran.Rows[i].Cells["TEIKI_HAISHA_NUMBER"].Value.ToString() + "\r\n";
                            Navi_Flg = "NAVITIME";
                        }

                        //振替先の配車伝票がロジコン連携されているか
                        if (this.RenkeiCheck(3, this.form.Ichiran.Rows[i].Cells["HURI_HAISHA_NUMBER"].Value.ToString()))
                        {
                            Err_Msg = Err_Msg + (i + 1) + "行目：定期配車番号 " + this.form.Ichiran.Rows[i].Cells["HURI_HAISHA_NUMBER"].Value.ToString() + "\r\n";
                            Logi_Flg = "ロジこんぱす";
                        }

                        //振替先の配車伝票がNAVITIME連携されているか
                        if (this.RenkeiCheck(4, this.form.Ichiran.Rows[i].Cells["HURI_HAISHA_NUMBER"].Value.ToString()))
                        {
                            Err_Msg = Err_Msg + (i + 1) + "行目：定期配車番号 " + this.form.Ichiran.Rows[i].Cells["HURI_HAISHA_NUMBER"].Value.ToString() + "\r\n";
                            Navi_Flg = "NAVITIME";
                        }
                        #endregion

                        #region 休働チェック
                        inputSagyouDate = Convert.ToString(this.form.Ichiran.Rows[i].Cells["HURI_SAGYOU_DATE"].Value);
                        //搬入先休働チェック
                        if (!HannyuusakiDateCheck(this.form.Ichiran.Rows[i].Cells["SYSTEM_ID_HIDDEN"].Value.ToString(), this.form.Ichiran.Rows[i].Cells["DETAIL_SYSTEM_ID_HIDDEN"].Value.ToString(), inputSagyouDate))
                        {
                            Err_holiday = Err_holiday + this.form.Ichiran.Rows[i].Cells["ROW_NUMBER"].Value.ToString() + "行目：荷降先休動設定" + "\r\n";
                        }
                        #endregion
                    }
                }

                if (!taisyou)
                {
                    this.MsgBox.MessageBoxShow("E050", "対象");
                    return;
                }

                //休働アラートを表示
                if (!string.IsNullOrEmpty(Err_holiday))
                {
                    Err_holiday = "休動設定が行われている荷降先に対する振替が\r\n行われています。\r\n確認してください。\r\n\r\n" + Err_holiday;
                    this.MsgBox.MessageBoxShowWarn(Err_holiday);
                    return;
                }

                if (!string.IsNullOrEmpty(Err_Msg))
                {
                    if ((!string.IsNullOrEmpty(Logi_Flg)) && (!string.IsNullOrEmpty(Navi_Flg)))
                    {
                        Err_Msg = Logi_Flg + "/" + Navi_Flg + "連携されています。\r\n確認してください。\r\n\r\n" + Err_Msg;
                    }
                    else
                    {
                        Err_Msg = Logi_Flg + Navi_Flg +"連携されています。\r\n確認してください。\r\n\r\n" + Err_Msg;
                    }
                    this.MsgBox.MessageBoxShowError(Err_Msg);
                    return;
                }

                //モバイル連携チェック
                if (!this.MobileRegistCheck_pre())
                {
                    this.MsgBox.MessageBoxShowError("モバイル連携が実行出来ない明細が存在するため\r\n登録処理を実行出来ません。"
                        + "\r\n\r\nモバイル連携を解除するか、データを確認の上\r\n再度実行してください。");
                    return;
                }
                #endregion

                if (MessageBox.Show("対象データの振替登録を実行します。\r\nよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                //初期化
                bool isMobikeChecked = false;
                bool isTaishoChecked = false;
                this.entitysMobisyoRtDELList = new List<T_MOBISYO_RT>();
                this.Renkei_TeikiDetailSystemId = string.Empty;

                using (Transaction tran = new Transaction())
                {
                    // トランザクション開始
                    for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                    {
                        DataGridViewRow dr = this.form.Ichiran.Rows[i];
                        isMobikeChecked = bool.Parse(dr.Cells["MOBILE_RENKEI"].Value.ToString());
                        isTaishoChecked = bool.Parse(dr.Cells["TAISHO_CHECK"].Value.ToString());
                    
                        //１．定期配車登録
                        if (isTaishoChecked)
                        {
                            //定期配車データの振替
                            //→振替先の定期伝票を修正登録(削除登録→該当明細行追加で新規登録）
                            //→現場メモもコピー
                            if (!registTeikihaishaInfo(dr))
                            {
                                return;
                            }
                            //モバイル除外データ取得
                            if (!CreateMobileDelEntity(dr))
                            {
                                return;
                            }
                         }
                    }

                    //モバイル削除/除外登録
                    if (!registMobileInfo())
                    {
                        return;
                    }

                    tran.Commit();
                }

                this.MsgBox.MessageBoxShow("I001", "登録");

                // 画面再表示処理
                this.Search();

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func9_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region F11 取消処理
        /// <summary>
        /// F11 取消処理
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                //検索処理を行う
                this.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func11_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region F12 閉じる処理
        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region データ取得処理
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();
                //チェックボックスの初期化
                this.form.checkBoxAll.Checked = false;
                this.form.checkBoxAll2.Checked = false;

                // 定期配車番号を検索条件に設定する
                this.searchDto.TeikiHaishaNumber = this.TeikiHaishaNumber;
                // 定期配車入力情報を取得する
                this.searchResultEntry = teikiHaishaDao.GetAllTeikiData(this.searchDto);
                count = searchResultEntry.Rows.Count;

                //検索結果を表示する
                this.setIchiran();
                return searchResultEntry.Rows.Count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.MsgBox.MessageBoxShow("E093");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.MsgBox.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return count;
        }

        /// <summary>
        /// 検索結果を表示する
        /// </summary>
        private void setIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //明細をクリアする
                this.form.Ichiran.Rows.Clear();

                //抽出結果をDGVにセット
                if (this.searchResultEntry != null && this.searchResultEntry.Rows.Count > 0)
                {
                    this.form.Ichiran.Rows.Add(this.searchResultEntry.Rows.Count);
                    for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                    {
                        DataGridViewRow row = this.form.Ichiran.Rows[i];
                        DataRow dr = this.searchResultEntry.Rows[i];

                        row.Cells["ROW_NUMBER"].Value = i + 1;                  //No
                        row.Cells["TAISHO_CHECK"].Value = false;                //対象
                        row.Cells["HURI_HAISHA_NUMBER"].Value = string.Empty;
                        row.Cells["HURI_COURSE_CD"].Value = string.Empty;
                        row.Cells["HURI_COURSE_NAME"].Value = string.Empty;
                        row.Cells["MOBILE_RENKEI"].Value = false;               //モバイル連携
                        row.Cells["SAGYOU_DATE"].Value = dr["SAGYOU_DATE"];     //作業日
                        row.Cells["TEIKI_HAISHA_NUMBER"].Value = dr["TEIKI_HAISHA_NUMBER"]; //配車番号
                        row.Cells["COURSE_CD"].Value = dr["COURSE_CD"];         //コースCD
                        row.Cells["COURSE_NAME"].Value = dr["COURSE_NAME"];     //コース名
                        row.Cells["JUNNBANN"].Value = dr["ROW_NUMBER"];         //順番(定期配車明細のROW_NUMBER)
                        row.Cells["ROUND_NO"].Value = dr["ROUND_NO"];           //回数
                        row.Cells["GYOUSHA_CD"].Value = dr["GYOUSHA_CD"];       //業者CD
                        row.Cells["GYOUSHA_NAME"].Value = dr["GYOUSHA_NAME"];   //業者名
                        row.Cells["GENBA_CD"].Value = dr["GENBA_CD"];           //現場CD
                        row.Cells["GENBA_NAME"].Value = dr["GENBA_NAME"];       //現場名
                        row.Cells["HINMEI_INFO"].Value = EditHinmeiInfo(long.Parse(dr["SYSTEM_ID_HIDDEN"].ToString()), int.Parse(dr["SEQ_HIDDEN"].ToString()), long.Parse(dr["DETAIL_SYSTEM_ID_HIDDEN"].ToString()));
                        row.Cells["MEISAI_BIKOU"].Value = dr["MEISAI_BIKOU"];   //明細備考
                        //明細希望時間
                        var kibou = dr["KIBOU_TIME"];
                        DateTime dateTime;
                        if (kibou is DBNull || !DateTime.TryParse(kibou.ToString(), out dateTime))
                        {
                            row.Cells["KIBOU_TIME"].Value = string.Empty;
                        }
                        else
                        {
                            row.Cells["KIBOU_TIME"].Value = dateTime.ToString("HH:mm");
                        }
                        //明細作業時間（分）
                        var kibou2 = dr["SAGYOU_TIME_MINUTE"];
                        if (kibou2 is DBNull)
                        {
                            row.Cells["SAGYOU_TIME_MINUTE"].Value = string.Empty;
                        }
                        else
                        {
                            row.Cells["SAGYOU_TIME_MINUTE"].Value = dr["SAGYOU_TIME_MINUTE"];
                        }
                        row.Cells["SYSTEM_ID_HIDDEN"].Value = dr["SYSTEM_ID_HIDDEN"];       //SYSTEM_ID
                        row.Cells["SEQ_HIDDEN"].Value = dr["SEQ_HIDDEN"];                   //SEQ
                        row.Cells["DETAIL_SYSTEM_ID_HIDDEN"].Value = dr["DETAIL_SYSTEM_ID_HIDDEN"];       //DETAIL_SYSTEM_ID
                        row.Cells["SEQ_NO_HIDDEN"].Value = dr["SEQ_NO_HIDDEN"];             //SEQ_NO
                        row.Cells["UKETSUKE_NUMBER"].Value = dr["UKETSUKE_NUMBER"];             //SEQ_NO
                    }
                }

                //フォーマット類
                foreach (DataGridViewColumn column in this.form.Ichiran.Columns)
                {
                    if (this.form.Ichiran.RowCount == 0)
                    {
                        column.Width = (column.HeaderText.Length * 10) + 55;
                    }

                    switch (column.Name)
                    {
                        case "ROW_NUMBER":
                        case "TEIKI_HAISHA_NUMBER":
                        case "HURI_HAISHA_NUMBER":
                        case "JUNNBANN":
                        case "ROUND_NO":
                            // 数値型ならセル値を右寄せにする
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "SAGYOU_DATE":
                            // 日付型は表示幅を「yyyy/mm/dd」が表示出来るよう固定長で表示を行う
                            column.Width = 107;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 品名情報を編集する（定期配車明細の明細システムIDをもと）
        /// </summary>
        /// <param name="SystemId">SYSTEM_ID</param>
        /// <param name="Seq">SEQ</param>
        /// <param name="detailSystemId">DETAIL_SYSTEM_ID</param>
        /// <returns>品名情報</returns>
        private string EditHinmeiInfo(long SystemId, int Seq, long detailSystemId)
        {
            // 戻り値
            string hinmeiInfo = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(SystemId, Seq, detailSystemId);
                // 明細システムIDを検索条件に設定する
                this.searchDto.SystemId = SystemId;
                this.searchDto.Seq = Seq;
                this.searchDto.DetailSystemId = detailSystemId;
                // 定期配車詳細情報を取得する
                searchResultShousai = teikiHaishaShousaiDao.GetShousaiData(this.searchDto);
                hinmeiInfo = setHinmeiInfo(searchResultShousai);
                return hinmeiInfo;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditHinmeiInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(hinmeiInfo);
            }
        }
        /// <summary>
        /// 品名情報を設定する
        /// </summary>
        /// <param name="dtHinmeiInfo">品名情報テーブル</param>
        /// <returns>品名情報</returns>
        private string setHinmeiInfo(DataTable dtHinmeiInfo)
        {
            // 品名情報格納StringBuilder
            var hinmeiInfo = new StringBuilder();

            try
            {
                LogUtility.DebugMethodStart(dtHinmeiInfo);

                if (dtHinmeiInfo.Rows.Count == 0)
                {
                    return string.Empty;
                }

                // ソートする（品名CD、レコードSEQ）
                dtHinmeiInfo.DefaultView.Sort = ConstCls.ShousaiColName.HINMEI_CD + "," + ConstCls.ShousaiColName.REC_SEQ;
                DataTable dtTemp = dtHinmeiInfo.DefaultView.ToTable();

                // 品名CD
                String strHinmeiCd = string.Empty;
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    if (dtTemp.Rows[i][ConstCls.ShousaiColName.HINMEI_CD].ToString().Equals(strHinmeiCd))
                    {
                        // 半角カンマ
                        hinmeiInfo.Append(",");
                        // 単位名
                        hinmeiInfo.Append(dtTemp.Rows[i][ConstCls.ShousaiColName.UNIT_NAME_RYAKU].ToString());
                    }
                    else
                    {
                        if (i > 0)
                        {
                            // 半角スラッシュ
                            hinmeiInfo.Append("/");
                        }
                        // 品名
                        hinmeiInfo.Append(dtTemp.Rows[i][ConstCls.ShousaiColName.HINMEI_NAME_RYAKU].ToString());
                        // 半角スペース
                        hinmeiInfo.Append(" ");
                        // 単位名
                        hinmeiInfo.Append(dtTemp.Rows[i][ConstCls.ShousaiColName.UNIT_NAME_RYAKU].ToString());
                        // 品名CD再設定
                        strHinmeiCd = dtTemp.Rows[i][ConstCls.ShousaiColName.HINMEI_CD].ToString();
                    }
                }

                // 品名情報を戻す
                return hinmeiInfo.ToString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("setHinmeiInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(hinmeiInfo.ToString());
            }
        }
        #endregion

        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }

        #region 定期配車のSYSTEM_IDを採番
        /// <summary>
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        private SqlInt64 createSystemIdForHaisha()
        {
            SqlInt64 returnVal = 1;

            try
            {
                LogUtility.DebugMethodStart();

                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.TEIKI_HAISHA.GetHashCode();

                // IS_NUMBER_SYSTEMDao(共通)
                IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

                var updateEntity = numberSystemDao.GetNumberSystemData(entity);
                returnVal = numberSystemDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.TEIKI_HAISHA.GetHashCode();
                    updateEntity.CURRENT_NUMBER = returnVal;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    numberSystemDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnVal;
                    numberSystemDao.Update(updateEntity);
                }

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

        #region 定期配車検索
        /// <summary>
        /// 定期配車検索
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="seq"></param>
        private T_TEIKI_HAISHA_ENTRY getTeikihaishaEntryData(int haishaNumber)
        {
            T_TEIKI_HAISHA_ENTRY returnValue = null;
            try
            {
                LogUtility.DebugMethodStart(haishaNumber);

                T_TEIKI_HAISHA_ENTRY teikihaishaEntryParm = new T_TEIKI_HAISHA_ENTRY();

                teikihaishaEntryParm.TEIKI_HAISHA_NUMBER = haishaNumber;

                T_TEIKI_HAISHA_ENTRY[] teikihaishaEntry = teikiHaishaDao.GetAllValidData(teikihaishaEntryParm);

                if (teikihaishaEntry != null)
                {
                    returnValue = teikihaishaEntry[0];
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue);
            }
        }
        #endregion

        #region 受付収集検索
        /// <summary>
        /// 受付収集検索
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="seq"></param>
        private T_UKETSUKE_SS_ENTRY getUketsukeSSEntryData(int uketsukeNumber)
        {
            T_UKETSUKE_SS_ENTRY returnValue = null;
            try
            {
                LogUtility.DebugMethodStart(uketsukeNumber);

                T_UKETSUKE_SS_ENTRY uketsukeSSEntryParm = new T_UKETSUKE_SS_ENTRY();
                uketsukeSSEntryParm.UKETSUKE_NUMBER = uketsukeNumber;

                T_UKETSUKE_SS_ENTRY[] uketsukeSSEntry = daoUketsukeSSEntry.GetDataForEntity(uketsukeSSEntryParm);

                if (uketsukeSSEntry.Length > 0)
                {
                    returnValue = uketsukeSSEntry[0];
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue);
            }

        }
        #endregion

        #region 受付出荷検索
        /// <summary>
        /// 受付出荷検索
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="seq"></param>
        private T_UKETSUKE_SK_ENTRY getUketsukeSKEntryData(int uketsukeNumber)
        {
            T_UKETSUKE_SK_ENTRY returnValue = null;
            try
            {
                LogUtility.DebugMethodStart(uketsukeNumber);

                T_UKETSUKE_SK_ENTRY uketsukeSKEntryParm = new T_UKETSUKE_SK_ENTRY();
                uketsukeSKEntryParm.UKETSUKE_NUMBER = uketsukeNumber;

                T_UKETSUKE_SK_ENTRY[] uketsukeSKEntry = daoUketsukeSKEntry.GetDataForEntity(uketsukeSKEntryParm);

                if (uketsukeSKEntry.Length > 0)
                {
                    returnValue = uketsukeSKEntry[0];
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue);
            }
        }
        #endregion

        #region 定期配車情報登録処理
        /// <summary>
        /// 定期配車情報登録処理
        /// </summary>
        /// <param name="dr"></param>
        private bool registTeikihaishaInfo(DataGridViewRow dr)
        {
            LogUtility.DebugMethodStart(dr);
            bool result = false;
            try
            {
                // 元システムID
                int OldsystemID = int.Parse(dr.Cells["SYSTEM_ID_HIDDEN"].Value.ToString());
                // 元DETAIL_SYSTEM_ID
                int OlddetailsystemID = int.Parse(dr.Cells["DETAIL_SYSTEM_ID_HIDDEN"].Value.ToString());
                // 元SEQ
                int OldSeq = int.Parse(dr.Cells["SEQ_HIDDEN"].Value.ToString());
                // 元配車番号
                int OldhaishaNumber = int.Parse(dr.Cells["TEIKI_HAISHA_NUMBER"].Value.ToString());
                // 元ROW_NUMBER
                int OldrowNumber = int.Parse(dr.Cells["JUNNBANN"].Value.ToString());
                // 元枝番
                int Motoseq = 0;
                // 元受付番号
                int OldUketsukeNo = 0;

                // 受付システムID
                int UkestemID = 0;
                // 受付枝番
                int Ukeseq = 0;

                // 振替先配車番号
                int haishaNumber = int.Parse(dr.Cells["HURI_HAISHA_NUMBER"].Value.ToString());
                // 枝番
                int seq = 0;
                // 回数
                Int32 roundNo = 1;

                // 業者CD(配車明細用）
                string gyoushaCD = dr.Cells["GYOUSHA_CD"].Value.ToString();
                // 現場CD(配車明細用）
                string genbaCD = dr.Cells["GENBA_CD"].Value.ToString();

                //現場メモ用（伝票番号）
                int genba_haishanumber = 0;
                //現場メモ用（行番号）
                int genba_haisharownumber = 0;

                //配車番号より、データ取得
                T_TEIKI_HAISHA_ENTRY teikihaishaEntry = getTeikihaishaEntryData(haishaNumber);

                //振替先データ更新・明細追加
                if (teikihaishaEntry != null)
                {
                    // 枝番
                    seq = (int)teikihaishaEntry.SEQ;
                    
                    #region 定期配車伝票
                    // 1-1.定期配車入力テーブルの更新（論理削除）
                    // システム自動設定のプロパティを設定する
                    // 更新日
                    teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 更新者
                    teikihaishaEntry.UPDATE_USER = SystemProperty.UserName;
                    // 更新PC
                    teikihaishaEntry.UPDATE_PC = SystemInformation.ComputerName;
                    // 削除フラグを設定
                    teikihaishaEntry.DELETE_FLG = true;
                    // データ更新
                    this.teikiHaishaDao.Update(teikihaishaEntry);

                    // 1-2.定期配車入力テーブルの追加
                    // 枝番+1
                    teikihaishaEntry.SEQ = seq + 1;
                    // 削除フラグ
                    teikihaishaEntry.DELETE_FLG = false;
                    // 更新日
                    teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 更新者
                    teikihaishaEntry.UPDATE_USER = SystemProperty.UserName;
                    // 更新PC
                    teikihaishaEntry.UPDATE_PC = SystemInformation.ComputerName;
                    // 定期配車入力テーブルに追加
                    this.teikiHaishaDao.Insert(teikihaishaEntry);
                    #endregion
                    
                    #region 定期配車明細
                    // 2-1.定期配車明細テーブルの追加（コピーイメージ）
                    T_TEIKI_HAISHA_DETAIL teikihaishaDetailParam = new T_TEIKI_HAISHA_DETAIL();
                    // 定期配車番号
                    teikihaishaDetailParam.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // システムID
                    teikihaishaDetailParam.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaDetailParam.SEQ = seq;

                    T_TEIKI_HAISHA_DETAIL[] teikihaishaDetailList = teikiHaishaDetailDao.GetDataForEntity(teikihaishaDetailParam);

                    SqlInt16 rowNumber = 0;
                    if (teikihaishaDetailList != null && teikihaishaDetailList.Length > 0)
                    {
                        foreach (var teikihaishaDetailItem in teikihaishaDetailList)
                        {
                            // rowNumber
                            rowNumber = rowNumber > teikihaishaDetailItem.ROW_NUMBER ? rowNumber : teikihaishaDetailItem.ROW_NUMBER;
                            // システムID
                            teikihaishaDetailItem.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                            // 枝番
                            teikihaishaDetailItem.SEQ = teikihaishaEntry.SEQ;
                            // 更新日
                            teikihaishaDetailItem.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            teikihaishaDetailItem.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            teikihaishaDetailItem.UPDATE_PC = SystemInformation.ComputerName;
                            // 定期配車荷降テーブルに追加
                            this.teikiHaishaDetailDao.Insert(teikihaishaDetailItem);
                        }
                    }

                    // 2-2.定期配車明細テーブルの追加（画面より新規作成）
                    T_TEIKI_HAISHA_DETAIL teikihaishaDetail = new T_TEIKI_HAISHA_DETAIL();
                    // システムID
                    teikihaishaDetail.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaDetail.SEQ = teikihaishaEntry.SEQ;
                    // 明細システムID
                    teikihaishaDetail.DETAIL_SYSTEM_ID = createSystemIdForHaisha();

                    //ﾓﾊﾞｲﾙ連携用に、DETAIL_SYSTEM_IDを集める
                    if ((bool)dr.Cells["MOBILE_RENKEI"].Value)
                    {
                        if (string.IsNullOrEmpty(this.Renkei_TeikiDetailSystemId))
                        {
                            this.Renkei_TeikiDetailSystemId = teikihaishaDetail.DETAIL_SYSTEM_ID.ToString();
                        }
                        else
                        {
                            this.Renkei_TeikiDetailSystemId = this.Renkei_TeikiDetailSystemId + ", " + teikihaishaDetail.DETAIL_SYSTEM_ID.ToString();
                        }
                    }

                    // 定期配車番号
                    teikihaishaDetail.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // 行番号
                    teikihaishaDetail.ROW_NUMBER = rowNumber + 1;

                    //現場メモ用（伝票番号）
                    genba_haishanumber = (int)teikihaishaEntry.SYSTEM_ID;
                    //現場メモ用（行番号）
                    genba_haisharownumber = (int)teikihaishaDetail.DETAIL_SYSTEM_ID;
                    // 業者CD
                    teikihaishaDetail.GYOUSHA_CD = gyoushaCD;
                    // 現場CD
                    teikihaishaDetail.GENBA_CD = genbaCD;
                    //希望時間
                    if (!string.IsNullOrEmpty(dr.Cells["KIBOU_TIME"].FormattedValue.ToString()))
                    {
                        teikihaishaDetail.KIBOU_TIME = Convert.ToDateTime(dr.Cells["KIBOU_TIME"].FormattedValue.ToString());
                    }
                    //作業時間（分）
                    if (!string.IsNullOrEmpty(dr.Cells["SAGYOU_TIME_MINUTE"].FormattedValue.ToString()))
                    {
                        teikihaishaDetail.SAGYOU_TIME_MINUTE = Int16.Parse(dr.Cells["SAGYOU_TIME_MINUTE"].FormattedValue.ToString());
                    }
                    // 明細備考
                    teikihaishaDetail.MEISAI_BIKOU = dr.Cells["MEISAI_BIKOU"].Value.ToString();
                    // 受付番号
                    if ((dr.Cells["UKETSUKE_NUMBER"].Value == null) || (string.IsNullOrEmpty(dr.Cells["UKETSUKE_NUMBER"].Value.ToString())))
                    {
                        teikihaishaDetail.UKETSUKE_NUMBER = SqlInt64.Null;
                    }
                    else
                    {
                        teikihaishaDetail.UKETSUKE_NUMBER = SqlInt64.Parse(dr.Cells["UKETSUKE_NUMBER"].Value.ToString());
                        OldUketsukeNo = int.Parse(dr.Cells["UKETSUKE_NUMBER"].Value.ToString());
                    }
                    // 更新日
                    teikihaishaDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 更新者
                    teikihaishaDetail.UPDATE_USER = SystemProperty.UserName;
                    // 更新PC
                    teikihaishaDetail.UPDATE_PC = SystemInformation.ComputerName;
                    // 回数を算出し、回数をセット
                    if (teikihaishaDetailList != null && teikihaishaDetailList.Length > 0)
                    {
                        // Insert対象の業者CD・現場CDと一致するものを検索
                        var list = teikihaishaDetailList.Where(r => (r.GYOUSHA_CD == gyoushaCD && r.GENBA_CD == genbaCD)).ToArray();

                        if (list != null && list.Length > 0)
                        {
                            // 該当情報が存在すれば、回数の最大値+1をInsert対象の回数とする
                            roundNo = (list.Select(r => r.ROUND_NO).Max() + 1).Value;
                        }
                    }
                    teikihaishaDetail.ROUND_NO = roundNo;

                    // 定期配車明細テーブルに追加
                    this.teikiHaishaDetailDao.Insert(teikihaishaDetail);
                    #endregion

                    #region 定期配車荷卸
                    // 3-1.定期配車荷降テーブルの追加（コピーイメージ）
                    T_TEIKI_HAISHA_NIOROSHI teikihaishaNioroshiParam = new T_TEIKI_HAISHA_NIOROSHI();
                    // 定期配車番号
                    teikihaishaNioroshiParam.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // システムID
                    teikihaishaNioroshiParam.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaNioroshiParam.SEQ = seq;

                    T_TEIKI_HAISHA_NIOROSHI[] teikihaishaNioroshiList = teikiHaishaNioroshiDao.GetDataForEntity(teikihaishaNioroshiParam);

                    Int32 intNListCount = 0;
                    if (teikihaishaNioroshiList != null && teikihaishaNioroshiList.Length > 0)
                    {
                        foreach (var teikihaishaNioroshiDetail in teikihaishaNioroshiList)
                        {
                            // システムID
                            teikihaishaNioroshiDetail.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                            // 枝番
                            teikihaishaNioroshiDetail.SEQ = teikihaishaEntry.SEQ;
                            // 更新日
                            teikihaishaNioroshiDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            teikihaishaNioroshiDetail.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            teikihaishaNioroshiDetail.UPDATE_PC = SystemInformation.ComputerName;
                            // 定期配車荷降テーブルに追加
                            this.teikiHaishaNioroshiDao.Insert(teikihaishaNioroshiDetail);
                        }
                        intNListCount = teikihaishaNioroshiList.Length;
                    }

                    //3-2.定期配車明細テーブルの追加（画面より新規作成）
                    //3-2-1.旧荷卸情報取得（詳細に登録のある荷卸情報を取得）
                    DataTable OldteikiHaishaNList = new DataTable();
                    T_TEIKI_HAISHA_SHOUSAI OldteikihaishaNParam = new T_TEIKI_HAISHA_SHOUSAI();
                    // 定期配車番号
                    OldteikihaishaNParam.TEIKI_HAISHA_NUMBER = OldhaishaNumber;
                    // システムID
                    OldteikihaishaNParam.SYSTEM_ID = OldsystemID;
                    // DETAIL_SYSTEM_ID
                    OldteikihaishaNParam.DETAIL_SYSTEM_ID = OlddetailsystemID;
                    // 枝番
                    OldteikihaishaNParam.SEQ = OldSeq;
                    OldteikiHaishaNList = teikiHaishaShousaiDao.GetNioroshiData(OldteikihaishaNParam);
                    //新しい荷卸Noは、更新可能の設定にしておく
                    DataColumn NewNi = new DataColumn("NEW_NIOROSHI_NUMBER");
                    NewNi.ReadOnly = false;
                    OldteikiHaishaNList.Columns.Add(NewNi);

                    //3-2-2.振替先に、荷卸業者/現場が存在するかチェック
                    //　　　→存在する：変換用の荷卸番号をNEW_NIOROSHI_NUMBERに保存
                    //　　　→存在しない：新しく荷卸番号を採番。NEW_NIOROSHI_NUMBERに保存
                    //　　　　　　　　　　荷卸データの登録
                    if (OldteikiHaishaNList != null && OldteikiHaishaNList.Rows.Count > 0)
                    {
                        T_TEIKI_HAISHA_NIOROSHI OldteikihaishaNioroshiList = new T_TEIKI_HAISHA_NIOROSHI();

                        for (int i = 0; i < OldteikiHaishaNList.Rows.Count; i++)
                        {
                            //振替先荷卸テーブルにデータがあるかチェック
                            string OLDGyoCD = OldteikiHaishaNList.Rows[i]["NIOROSHI_GYOUSHA_CD"].ToString();
                            string OLDGenCD = OldteikiHaishaNList.Rows[i]["NIOROSHI_GENBA_CD"].ToString();
                            var list = teikihaishaNioroshiList.Where(r => (r.NIOROSHI_GYOUSHA_CD == OLDGyoCD && r.NIOROSHI_GENBA_CD == OLDGenCD)).ToArray();
                            if (list != null && list.Length > 0)
                            {
                                //存在する
                                OldteikiHaishaNList.Rows[i]["NEW_NIOROSHI_NUMBER"] = list[0].NIOROSHI_NUMBER.Value;
                            }
                            else
                            {
                                //存在しない→振替先に荷卸データ作成
                                intNListCount = intNListCount + 1;
                                OldteikiHaishaNList.Rows[i]["NEW_NIOROSHI_NUMBER"] = intNListCount;

                                // システムID
                                OldteikihaishaNioroshiList.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                                // 枝番
                                OldteikihaishaNioroshiList.SEQ = teikihaishaEntry.SEQ;
                                // 荷卸番号
                                OldteikihaishaNioroshiList.NIOROSHI_NUMBER = intNListCount;
                                // 定期配車番号
                                OldteikihaishaNioroshiList.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                                // 荷卸業者CD
                                OldteikihaishaNioroshiList.NIOROSHI_GYOUSHA_CD = OLDGyoCD;
                                // 荷卸現場CD
                                OldteikihaishaNioroshiList.NIOROSHI_GENBA_CD = OLDGenCD;
                                // 更新日
                                OldteikihaishaNioroshiList.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 更新者
                                OldteikihaishaNioroshiList.UPDATE_USER = SystemProperty.UserName;
                                // 更新PC
                                OldteikihaishaNioroshiList.UPDATE_PC = SystemInformation.ComputerName;
                                // 定期配車荷降テーブルに追加
                                this.teikiHaishaNioroshiDao.Insert(OldteikihaishaNioroshiList);
                            }
                        }
                    }
                    #endregion

                    #region 定期配車詳細
                    // 4-1.定期配車詳細テーブルの追加（コピーイメージ）
                    T_TEIKI_HAISHA_SHOUSAI teikihaishaShousaiParam = new T_TEIKI_HAISHA_SHOUSAI();
                    // 定期配車番号
                    teikihaishaShousaiParam.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // システムID
                    teikihaishaShousaiParam.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaShousaiParam.SEQ = seq;

                    T_TEIKI_HAISHA_SHOUSAI[] teikiHaishaShousaiList = teikiHaishaShousaiDao.GetDataForEntity(teikihaishaShousaiParam);

                    if (teikiHaishaShousaiList != null && teikiHaishaShousaiList.Length > 0)
                    {
                        foreach (var teikihaishaShousaiItem in teikiHaishaShousaiList)
                        {
                            // システムID
                            teikihaishaShousaiItem.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                            // 枝番
                            teikihaishaShousaiItem.SEQ = teikihaishaEntry.SEQ;
                            // 更新日
                            teikihaishaShousaiItem.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            teikihaishaShousaiItem.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            teikihaishaShousaiItem.UPDATE_PC = SystemInformation.ComputerName;
                            // 定期配車詳細テーブルに追加
                            this.teikiHaishaShousaiDao.Insert(teikihaishaShousaiItem);
                        }
                    }

                    // 4-2.定期配車詳細テーブルの追加（画面→ＤＢより新規作成）
                    //振替前の定期配車詳細を検索
                    DataTable OldteikiHaishaShousaiList = new DataTable();
                    T_TEIKI_HAISHA_SHOUSAI OldteikihaishaShousaiParam = new T_TEIKI_HAISHA_SHOUSAI();
                    // 定期配車番号
                    OldteikihaishaShousaiParam.TEIKI_HAISHA_NUMBER = OldhaishaNumber;
                    // システムID
                    OldteikihaishaShousaiParam.SYSTEM_ID = OldsystemID;
                    // DETAIL_SYSTEM_ID
                    OldteikihaishaShousaiParam.DETAIL_SYSTEM_ID = OlddetailsystemID;
                    // 枝番
                    OldteikihaishaShousaiParam.SEQ = OldSeq;

                    OldteikiHaishaShousaiList = teikiHaishaShousaiDao.GetDetailData(OldteikihaishaShousaiParam);

                    for (int i = 0; i < OldteikiHaishaShousaiList.Rows.Count; i++)
                    {
                        T_TEIKI_HAISHA_SHOUSAI teikihaishaShousai = new T_TEIKI_HAISHA_SHOUSAI();
                        // SYSTEM_ID
                        teikihaishaShousai.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                        // SEQ
                        teikihaishaShousai.SEQ = teikihaishaEntry.SEQ;
                        // DETAIL_SYSTEM_ID
                        teikihaishaShousai.DETAIL_SYSTEM_ID = teikihaishaDetail.DETAIL_SYSTEM_ID;
                        // ROW_NUMBER
                        teikihaishaShousai.ROW_NUMBER = i + 1;
                        // TEIKI_HAISHA_NUMBER
                        teikihaishaShousai.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                        // INPUT_KBN
                        if (!string.IsNullOrEmpty(OldteikiHaishaShousaiList.Rows[i]["INPUT_KBN"].ToString()))
                        {
                            teikihaishaShousai.INPUT_KBN = SqlInt16.Parse(OldteikiHaishaShousaiList.Rows[i]["INPUT_KBN"].ToString());
                        }
                        // NIOROSHI_NUMBER
                        //元のデータに荷卸Noがある場合、振替先の荷卸Noで登録し直す
                        if (!string.IsNullOrEmpty(OldteikiHaishaShousaiList.Rows[i]["NIOROSHI_NUMBER"].ToString()))
                        {
                            var list2 = OldteikiHaishaNList.AsEnumerable().Where(r => (r["NIOROSHI_NUMBER"].ToString() == OldteikiHaishaShousaiList.Rows[i]["NIOROSHI_NUMBER"].ToString())).ToArray();
                            if (list2 != null && list2.Length > 0)
                            {
                                teikihaishaShousai.NIOROSHI_NUMBER = SqlInt32.Parse(list2[0]["NEW_NIOROSHI_NUMBER"].ToString());
                            }
                        }
                        // HINMEI_CD
                        teikihaishaShousai.HINMEI_CD = OldteikiHaishaShousaiList.Rows[i]["HINMEI_CD"].ToString();
                        // UNIT_CD
                        teikihaishaShousai.UNIT_CD = SqlInt16.Parse(OldteikiHaishaShousaiList.Rows[i]["UNIT_CD"].ToString());
                        // KANSANCHI
                        if (!string.IsNullOrEmpty(OldteikiHaishaShousaiList.Rows[i]["KANSANCHI"].ToString()))
                        {
                            teikihaishaShousai.KANSANCHI = SqlMoney.Parse(OldteikiHaishaShousaiList.Rows[i]["KANSANCHI"].ToString());
                        }
                        // KANSAN_UNIT_CD
                        if (!string.IsNullOrEmpty(OldteikiHaishaShousaiList.Rows[i]["KANSAN_UNIT_CD"].ToString()))
                        {
                            teikihaishaShousai.KANSAN_UNIT_CD = SqlInt16.Parse(OldteikiHaishaShousaiList.Rows[i]["KANSAN_UNIT_CD"].ToString());
                        }
                        // KEIYAKU_KBN
                        if (!string.IsNullOrEmpty(OldteikiHaishaShousaiList.Rows[i]["KEIYAKU_KBN"].ToString()))
                        {
                            teikihaishaShousai.KEIYAKU_KBN = SqlInt16.Parse(OldteikiHaishaShousaiList.Rows[i]["KEIYAKU_KBN"].ToString());
                        }
                        // KEIJYOU_KBN
                        if (!string.IsNullOrEmpty(OldteikiHaishaShousaiList.Rows[i]["KEIJYOU_KBN"].ToString()))
                        {
                            teikihaishaShousai.KEIJYOU_KBN = SqlInt16.Parse(OldteikiHaishaShousaiList.Rows[i]["KEIJYOU_KBN"].ToString());
                        }
                        // ADD_FLG
                        teikihaishaShousai.ADD_FLG = (bool)OldteikiHaishaShousaiList.Rows[i]["ADD_FLG"]; 
                        // DENPYOU_KBN_CD
                        if (!string.IsNullOrEmpty(OldteikiHaishaShousaiList.Rows[i]["DENPYOU_KBN_CD"].ToString()))
                        {
                            teikihaishaShousai.DENPYOU_KBN_CD = SqlInt16.Parse(OldteikiHaishaShousaiList.Rows[i]["DENPYOU_KBN_CD"].ToString());
                        }
                        // KANSAN_UNIT_MOBILE_OUTPUT_FLG
                        if (!string.IsNullOrEmpty(OldteikiHaishaShousaiList.Rows[i]["KANSAN_UNIT_MOBILE_OUTPUT_FLG"].ToString()))
                        {
                            teikihaishaShousai.KANSAN_UNIT_MOBILE_OUTPUT_FLG = (bool)OldteikiHaishaShousaiList.Rows[i]["KANSAN_UNIT_MOBILE_OUTPUT_FLG"];
                        }
                        // ANBUN_FLG
                        if (!string.IsNullOrEmpty(OldteikiHaishaShousaiList.Rows[i]["ANBUN_FLG"].ToString()))
                        {
                            teikihaishaShousai.ANBUN_FLG = (bool)OldteikiHaishaShousaiList.Rows[i]["ANBUN_FLG"];
                        }

                        teikiHaishaShousaiDao.Insert(teikihaishaShousai);
                    }
                    #endregion
                }

                //振替元データ更新
                // 受付番号を持ってる定期配車明細を振替した時は、振替元の定期配車明細の受付番号をクリア
                if ((dr.Cells["UKETSUKE_NUMBER"].Value == null) || (string.IsNullOrEmpty(dr.Cells["UKETSUKE_NUMBER"].Value.ToString())))
                {
                    //
                }
                else
                {
                    // 配車番号より、データ取得
                    T_TEIKI_HAISHA_ENTRY teikihaishaMotoEntry = getTeikihaishaEntryData(OldhaishaNumber);
                    if (teikihaishaMotoEntry != null)
                    {
                        // 枝番
                        Motoseq = (int)teikihaishaMotoEntry.SEQ;

                        #region 定期配車伝票
                        // 1-1.定期配車入力テーブルの更新（論理削除）
                        // システム自動設定のプロパティを設定する
                        // 更新日
                        teikihaishaMotoEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                        // 更新者
                        teikihaishaMotoEntry.UPDATE_USER = SystemProperty.UserName;
                        // 更新PC
                        teikihaishaMotoEntry.UPDATE_PC = SystemInformation.ComputerName;
                        // 削除フラグを設定
                        teikihaishaMotoEntry.DELETE_FLG = true;
                        // データ更新
                        this.teikiHaishaDao.Update(teikihaishaMotoEntry);

                        // 1-2.定期配車入力テーブルの追加
                        // 枝番+1
                        teikihaishaMotoEntry.SEQ = Motoseq + 1;
                        // 削除フラグ
                        teikihaishaMotoEntry.DELETE_FLG = false;
                        // 更新日
                        teikihaishaMotoEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                        // 更新者
                        teikihaishaMotoEntry.UPDATE_USER = SystemProperty.UserName;
                        // 更新PC
                        teikihaishaMotoEntry.UPDATE_PC = SystemInformation.ComputerName;
                        // 定期配車入力テーブルに追加
                        this.teikiHaishaDao.Insert(teikihaishaMotoEntry);
                        #endregion

                        #region 定期配車明細
                        // 2-1.定期配車明細テーブルの追加（コピーイメージ）
                        T_TEIKI_HAISHA_DETAIL teikihaishaDetailMotoParam = new T_TEIKI_HAISHA_DETAIL();
                        // 定期配車番号
                        teikihaishaDetailMotoParam.TEIKI_HAISHA_NUMBER = teikihaishaMotoEntry.TEIKI_HAISHA_NUMBER;
                        // システムID
                        teikihaishaDetailMotoParam.SYSTEM_ID = teikihaishaMotoEntry.SYSTEM_ID;
                        // 枝番
                        teikihaishaDetailMotoParam.SEQ = Motoseq;

                        T_TEIKI_HAISHA_DETAIL[] teikihaishaDetailMotoList = teikiHaishaDetailDao.GetDataForEntity(teikihaishaDetailMotoParam);

                        SqlInt16 rowNumber = 0;
                        if (teikihaishaDetailMotoList != null && teikihaishaDetailMotoList.Length > 0)
                        {
                            foreach (var teikihaishaDetailItem in teikihaishaDetailMotoList)
                            {
                                //振替元のデータの場合、受付番号を初期化する
                                if (OlddetailsystemID == teikihaishaDetailItem.DETAIL_SYSTEM_ID.Value)
                                {
                                    teikihaishaDetailItem.UKETSUKE_NUMBER = SqlInt64.Null;
                                }
                                // rowNumber
                                rowNumber = rowNumber > teikihaishaDetailItem.ROW_NUMBER ? rowNumber : teikihaishaDetailItem.ROW_NUMBER;
                                // システムID
                                teikihaishaDetailItem.SYSTEM_ID = teikihaishaMotoEntry.SYSTEM_ID;
                                // 枝番
                                teikihaishaDetailItem.SEQ = teikihaishaMotoEntry.SEQ;
                                // 更新日
                                teikihaishaDetailItem.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 更新者
                                teikihaishaDetailItem.UPDATE_USER = SystemProperty.UserName;
                                // 更新PC
                                teikihaishaDetailItem.UPDATE_PC = SystemInformation.ComputerName;
                                // 定期配車荷降テーブルに追加
                                this.teikiHaishaDetailDao.Insert(teikihaishaDetailItem);
                            }
                        }
                        #endregion

                        #region 定期配車荷卸
                        // 3-1.定期配車荷降テーブルの追加（コピーイメージ）
                        T_TEIKI_HAISHA_NIOROSHI teikihaishaNioroshiMotoParam = new T_TEIKI_HAISHA_NIOROSHI();
                        // 定期配車番号
                        teikihaishaNioroshiMotoParam.TEIKI_HAISHA_NUMBER = teikihaishaMotoEntry.TEIKI_HAISHA_NUMBER;
                        // システムID
                        teikihaishaNioroshiMotoParam.SYSTEM_ID = teikihaishaMotoEntry.SYSTEM_ID;
                        // 枝番
                        teikihaishaNioroshiMotoParam.SEQ = Motoseq;

                        T_TEIKI_HAISHA_NIOROSHI[] teikihaishaNioroshiMotoList = teikiHaishaNioroshiDao.GetDataForEntity(teikihaishaNioroshiMotoParam);

                        if (teikihaishaNioroshiMotoList != null && teikihaishaNioroshiMotoList.Length > 0)
                        {
                            foreach (var teikihaishaNioroshiDetail in teikihaishaNioroshiMotoList)
                            {
                                // システムID
                                teikihaishaNioroshiDetail.SYSTEM_ID = teikihaishaMotoEntry.SYSTEM_ID;
                                // 枝番
                                teikihaishaNioroshiDetail.SEQ = teikihaishaMotoEntry.SEQ;
                                // 更新日
                                teikihaishaNioroshiDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 更新者
                                teikihaishaNioroshiDetail.UPDATE_USER = SystemProperty.UserName;
                                // 更新PC
                                teikihaishaNioroshiDetail.UPDATE_PC = SystemInformation.ComputerName;
                                // 定期配車荷降テーブルに追加
                                this.teikiHaishaNioroshiDao.Insert(teikihaishaNioroshiDetail);
                            }
                        }
                        #endregion

                        #region 定期配車詳細
                        // 4-1.定期配車詳細テーブルの追加（コピーイメージ）
                        T_TEIKI_HAISHA_SHOUSAI teikihaishaShousaiMotoParam = new T_TEIKI_HAISHA_SHOUSAI();
                        // 定期配車番号
                        teikihaishaShousaiMotoParam.TEIKI_HAISHA_NUMBER = teikihaishaMotoEntry.TEIKI_HAISHA_NUMBER;
                        // システムID
                        teikihaishaShousaiMotoParam.SYSTEM_ID = teikihaishaMotoEntry.SYSTEM_ID;
                        // 枝番
                        teikihaishaShousaiMotoParam.SEQ = Motoseq;

                        T_TEIKI_HAISHA_SHOUSAI[] teikiHaishaShousaiMotoList = teikiHaishaShousaiDao.GetDataForEntity(teikihaishaShousaiMotoParam);

                        if (teikiHaishaShousaiMotoList != null && teikiHaishaShousaiMotoList.Length > 0)
                        {
                            foreach (var teikihaishaShousaiItem in teikiHaishaShousaiMotoList)
                            {
                                // システムID
                                teikihaishaShousaiItem.SYSTEM_ID = teikihaishaMotoEntry.SYSTEM_ID;
                                // 枝番
                                teikihaishaShousaiItem.SEQ = teikihaishaMotoEntry.SEQ;
                                // 更新日
                                teikihaishaShousaiItem.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 更新者
                                teikihaishaShousaiItem.UPDATE_USER = SystemProperty.UserName;
                                // 更新PC
                                teikihaishaShousaiItem.UPDATE_PC = SystemInformation.ComputerName;
                                // 定期配車詳細テーブルに追加
                                this.teikiHaishaShousaiDao.Insert(teikihaishaShousaiItem);
                            }
                        }
                        #endregion

                        // システムIDと枝番より、受付（収集）入力テーブルからデータ取得
                        T_UKETSUKE_SS_ENTRY uketsukeSSEntry = getUketsukeSSEntryData(int.Parse(dr.Cells["UKETSUKE_NUMBER"].Value.ToString()));

                        if (uketsukeSSEntry != null)
                        {

                            UkestemID = (int)uketsukeSSEntry.SYSTEM_ID;
                            Ukeseq = (int)uketsukeSSEntry.SEQ;

                            var unpanDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                            var unpan = unpanDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = teikihaishaEntry.UNPAN_GYOUSHA_CD }).DefaultIfEmpty(new M_GYOUSHA()).FirstOrDefault();
                            var shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
                            var shashu = shashuDao.GetAllValidData(new M_SHASHU() { SHASHU_CD = teikihaishaEntry.SHASHU_CD }).DefaultIfEmpty(new M_SHASHU()).FirstOrDefault();
                            var sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
                            var sharyou = sharyouDao.GetAllValidData(new M_SHARYOU() { SHARYOU_CD = teikihaishaEntry.SHARYOU_CD, GYOUSHA_CD = teikihaishaEntry.UNPAN_GYOUSHA_CD}).DefaultIfEmpty(new M_SHARYOU()).FirstOrDefault();
                            var shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
                            var untensha = shainDao.GetAllValidData(new M_SHAIN() { SHAIN_CD = teikihaishaEntry.UNTENSHA_CD }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();
                            var hojoin = shainDao.GetAllValidData(new M_SHAIN() { SHAIN_CD = teikihaishaEntry.HOJOIN_CD }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();

                            #region 受付（収集）
                            // 1-1-1.受付（収集）入力テーブルの更新（論理削除）
                            var WHOSSEntry = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(uketsukeSSEntry);
                            // 削除フラグを設定
                            uketsukeSSEntry.DELETE_FLG = true;
                            // 更新日
                            uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            uketsukeSSEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSSEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // データ更新
                            this.daoUketsukeSSEntry.Update(uketsukeSSEntry);

                            // 1-1-2.受付（収集）入力テーブルの追加
                            // 枝番+1
                            uketsukeSSEntry.SEQ = Ukeseq + 1;
                            //// 配車状況CD
                            //uketsukeSSEntry.HAISHA_JOKYO_CD = 2;
                            //// 配車状況名
                            //uketsukeSSEntry.HAISHA_JOKYO_NAME = "配車";
                            //// 配車種類CD
                            //uketsukeSSEntry.HAISHA_SHURUI_CD = 3;
                            //// 配車種類名
                            //uketsukeSSEntry.HAISHA_SHURUI_NAME = "確定";
                            // コース名CD
                            uketsukeSSEntry.COURSE_NAME_CD = teikihaishaEntry.COURSE_NAME_CD;
                            //運搬業者CD
                            uketsukeSSEntry.UNPAN_GYOUSHA_CD = teikihaishaEntry.UNPAN_GYOUSHA_CD;
                            //運搬業者名
                            uketsukeSSEntry.UNPAN_GYOUSHA_NAME = unpan.GYOUSHA_NAME1;
                            // 車種CD
                            uketsukeSSEntry.SHASHU_CD = teikihaishaEntry.SHASHU_CD;
                            // 車種名
                            uketsukeSSEntry.SHASHU_NAME = shashu.SHASHU_NAME_RYAKU;
                            //車輌CD
                            uketsukeSSEntry.SHARYOU_CD = teikihaishaEntry.SHARYOU_CD;
                            // 車輌名
                            uketsukeSSEntry.SHARYOU_NAME = sharyou.SHARYOU_NAME_RYAKU;
                            // 運転者CD
                            uketsukeSSEntry.UNTENSHA_CD = teikihaishaEntry.UNTENSHA_CD;
                            // 運転者名
                            uketsukeSSEntry.UNTENSHA_NAME = untensha.SHAIN_NAME_RYAKU;
                            // 補助員CD
                            uketsukeSSEntry.HOJOIN_CD = teikihaishaEntry.HOJOIN_CD;
                            // 補助員名
                            uketsukeSSEntry.HOJOIN_NAME = hojoin.SHAIN_NAME_RYAKU;
                            // 削除フラグ
                            uketsukeSSEntry.DELETE_FLG = false;
                            //作業日
                            uketsukeSSEntry.SAGYOU_DATE = teikihaishaEntry.SAGYOU_DATE.ToString();
                            // 更新日
                            uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            uketsukeSSEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSSEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 受付（収集）入力テーブルに追加
                            this.daoUketsukeSSEntry.Insert(uketsukeSSEntry);
                            #endregion

                            #region 受付明細（収集）
                            // 1-1-3.受付（収集）明細テーブルの追加（コピーイメージ）
                            T_UKETSUKE_SS_DETAIL uketsukeSSDetailParam = new T_UKETSUKE_SS_DETAIL();
                            // システムID
                            uketsukeSSDetailParam.SYSTEM_ID = UkestemID;
                            // 枝番
                            uketsukeSSDetailParam.SEQ = Ukeseq;
                            // 取得したキーで読み込み
                            T_UKETSUKE_SS_DETAIL[] uketsukeSSDetailList = this.daoUketsukeSSDetail.GetDataForEntity(uketsukeSSDetailParam);

                            if (uketsukeSSDetailList != null && uketsukeSSDetailList.Length > 0)
                            {
                                foreach (var ssDetail in uketsukeSSDetailList)
                                {
                                    // システムID
                                    ssDetail.SYSTEM_ID = uketsukeSSEntry.SYSTEM_ID;
                                    // 枝番
                                    ssDetail.SEQ = uketsukeSSEntry.SEQ;
                                    // 更新日
                                    ssDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 更新者
                                    ssDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    ssDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // 受付（収集）明細テーブルに追加
                                    this.daoUketsukeSSDetail.Insert(ssDetail);
                                }
                            }
                            #endregion

                            #region コンテナ
                            // 1-1-4.コンテナ稼動予定テーブルの追加（コピーイメージ）
                            T_CONTENA_RESERVE contenaReserveParam = new T_CONTENA_RESERVE();
                            // システムID
                            contenaReserveParam.SYSTEM_ID = UkestemID;
                            // 枝番
                            contenaReserveParam.SEQ = Ukeseq;
                            // 取得したキーで読み込み
                            T_CONTENA_RESERVE[] contenaReserveList = this.daoContenaReserver.GetDataForEntity(contenaReserveParam);

                            if (contenaReserveList != null && contenaReserveList.Length > 0)
                            {
                                foreach (var contenaReserve in contenaReserveList)
                                {
                                    // 削除フラグ
                                    contenaReserve.DELETE_FLG = true;
                                    // 更新日
                                    contenaReserve.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 更新者
                                    contenaReserve.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    contenaReserve.UPDATE_PC = SystemInformation.ComputerName;
                                    // コンテナ稼動予定テーブルに追加
                                    this.daoContenaReserver.Update(contenaReserve);
                                }

                                foreach (var contenaReserve in contenaReserveList)
                                {
                                    // システムID
                                    contenaReserve.SYSTEM_ID = uketsukeSSEntry.SYSTEM_ID;
                                    // 枝番
                                    contenaReserve.SEQ = uketsukeSSEntry.SEQ;
                                    // 削除フラグ
                                    contenaReserve.DELETE_FLG = false;
                                    // 更新日
                                    contenaReserve.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 更新者
                                    contenaReserve.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    contenaReserve.UPDATE_PC = SystemInformation.ComputerName;
                                    // コンテナ稼動予定テーブルに追加
                                    this.daoContenaReserver.Insert(contenaReserve);
                                }
                            }
                            #endregion
                        }

                        // システムIDと枝番より、受付（出荷）入力テーブルからデータ取得
                        T_UKETSUKE_SK_ENTRY uketsukeSKEntry = getUketsukeSKEntryData(int.Parse(dr.Cells["UKETSUKE_NUMBER"].Value.ToString()));
                        if (uketsukeSKEntry != null)
                        {

                            UkestemID = (int)uketsukeSKEntry.SYSTEM_ID;
                            Ukeseq = (int)uketsukeSKEntry.SEQ;

                            var unpanDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                            var unpan = unpanDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = teikihaishaEntry.UNPAN_GYOUSHA_CD }).DefaultIfEmpty(new M_GYOUSHA()).FirstOrDefault();
                            var shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
                            var shashu = shashuDao.GetAllValidData(new M_SHASHU() { SHASHU_CD = teikihaishaEntry.SHASHU_CD }).DefaultIfEmpty(new M_SHASHU()).FirstOrDefault();
                            var sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
                            var sharyou = sharyouDao.GetAllValidData(new M_SHARYOU() { SHARYOU_CD = teikihaishaEntry.SHARYOU_CD, GYOUSHA_CD = teikihaishaEntry.UNPAN_GYOUSHA_CD }).DefaultIfEmpty(new M_SHARYOU()).FirstOrDefault();
                            var shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
                            var untensha = shainDao.GetAllValidData(new M_SHAIN() { SHAIN_CD = teikihaishaEntry.UNTENSHA_CD }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();
                            var hojoin = shainDao.GetAllValidData(new M_SHAIN() { SHAIN_CD = teikihaishaEntry.HOJOIN_CD }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();

                            #region 受付（出荷）
                            // 1-1-1.受付（出荷）入力テーブルの更新（論理削除）
                            var WHOSKEntry = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(uketsukeSKEntry);
                            // 更新日
                            uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            uketsukeSKEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSKEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 削除フラグを設定
                            uketsukeSKEntry.DELETE_FLG = true;
                            this.daoUketsukeSKEntry.Update(uketsukeSKEntry);

                            // 1-1-2.受付（出荷）入力テーブルの追加
                            // 枝番+1
                            uketsukeSKEntry.SEQ = Ukeseq + 1;
                            //// 配車状況CD
                            //uketsukeSKEntry.HAISHA_JOKYO_CD = 2;
                            //// 配車状況名
                            //uketsukeSKEntry.HAISHA_JOKYO_NAME = "配車";
                            //// 配車種類CD
                            //uketsukeSKEntry.HAISHA_SHURUI_CD = 3;
                            //// 配車種類名
                            //uketsukeSKEntry.HAISHA_SHURUI_NAME = "確定";
                            // コース名CD
                            uketsukeSKEntry.COURSE_NAME_CD = teikihaishaEntry.COURSE_NAME_CD;
                            //運搬業者CD
                            uketsukeSKEntry.UNPAN_GYOUSHA_CD = teikihaishaEntry.UNPAN_GYOUSHA_CD;
                            //運搬業者名
                            uketsukeSKEntry.UNPAN_GYOUSHA_NAME = unpan.GYOUSHA_NAME1;
                            // 車種CD
                            uketsukeSKEntry.SHASHU_CD = teikihaishaEntry.SHASHU_CD;
                            // 車種名
                            uketsukeSKEntry.SHASHU_NAME = shashu.SHASHU_NAME_RYAKU;
                            //車輌CD
                            uketsukeSKEntry.SHARYOU_CD = teikihaishaEntry.SHARYOU_CD;
                            // 車輌名
                            uketsukeSKEntry.SHARYOU_NAME = sharyou.SHARYOU_NAME_RYAKU;
                            // 運転者CD
                            uketsukeSKEntry.UNTENSHA_CD = teikihaishaEntry.UNTENSHA_CD;
                            // 運転者名
                            uketsukeSKEntry.UNTENSHA_NAME = untensha.SHAIN_NAME_RYAKU;
                            // 補助員CD
                            uketsukeSKEntry.HOJOIN_CD = teikihaishaEntry.HOJOIN_CD;
                            // 補助員名
                            uketsukeSKEntry.HOJOIN_NAME = hojoin.SHAIN_NAME_RYAKU;
                            // 削除フラグ
                            uketsukeSKEntry.DELETE_FLG = false;
                            // 更新日
                            uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            uketsukeSKEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSKEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 受付（出荷）入力テーブルに追加
                            this.daoUketsukeSKEntry.Insert(uketsukeSKEntry);
                            #endregion

                            #region 受付出荷（明細）
                            // 1-1-3.受付（出荷）明細テーブルの追加（コピーイメージ）
                            T_UKETSUKE_SK_DETAIL uketsukeSKDetailParam = new T_UKETSUKE_SK_DETAIL();
                            // システムID
                            uketsukeSKDetailParam.SYSTEM_ID = UkestemID;
                            // 枝番
                            uketsukeSKDetailParam.SEQ = Ukeseq;
                            // 取得したキーで読み込み
                            T_UKETSUKE_SK_DETAIL[] uketsukeSKDetailList = this.daoUketsukeSKDetail.GetDataForEntity(uketsukeSKDetailParam);

                            if (uketsukeSKDetailList != null && uketsukeSKDetailList.Length > 0)
                            {
                                foreach (var skDetail in uketsukeSKDetailList)
                                {
                                    // システムID
                                    skDetail.SYSTEM_ID = uketsukeSKEntry.SYSTEM_ID;
                                    // 枝番
                                    skDetail.SEQ = uketsukeSKEntry.SEQ;
                                    // 更新日
                                    skDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 更新者
                                    skDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    skDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // 受付（出荷）明細テーブルに追加
                                    this.daoUketsukeSKDetail.Insert(skDetail);
                                }
                            }
                            #endregion
                        }

                    }
                }

                #region 現場メモ
                T_GENBAMEMO_ENTRY GenbaMemoEntry = genbamemoEntryDAO.GetDataByGenbamemoNumber(OldsystemID, OlddetailsystemID);
                
                if (GenbaMemoEntry != null)
                {
                    string genbasystem_id  = GenbaMemoEntry.SYSTEM_ID.ToString();
                    string genbaseq = GenbaMemoEntry.SEQ.ToString();

                    //1-1 変更前の定期配車明細に紐づいてた情報を更新
                    //1-1-1 現場メモ)deleteflgを立てる
                    // 更新日
                    GenbaMemoEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 更新者
                    GenbaMemoEntry.UPDATE_USER = SystemProperty.UserName;
                    // 更新PC
                    GenbaMemoEntry.UPDATE_PC = SystemInformation.ComputerName;
                    // 削除フラグを設定
                    GenbaMemoEntry.DELETE_FLG = true;
                    // データ更新
                    this.genbamemoEntryDAO.Update(GenbaMemoEntry);

                    //1-1-2 現場メモ)非表示登録をする
                    // 枝番+1
                    GenbaMemoEntry.SEQ = Int16.Parse(genbaseq) + 1;
                    // 削除フラグを設定
                    GenbaMemoEntry.HIHYOUJI_FLG = true;
                    // 非表示日を設定
                    GenbaMemoEntry.HIHYOUJI_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 非表示登録者を設定
                    GenbaMemoEntry.HIHYOUJI_TOUROKUSHA_NAME = SystemProperty.UserName;
                    // 削除フラグを設定
                    GenbaMemoEntry.DELETE_FLG = false;
                    // データ更新
                    this.genbamemoEntryDAO.Insert(GenbaMemoEntry);

                    //1-1-3 現場メモ明細)非表示登録の明細を登録
                    this.genbamemoDetailList = genbamemoDetailDAO.GetDataByKey(genbasystem_id, genbaseq);
                    if (this.genbamemoDetailList != null && this.genbamemoDetailList.Count >0)
                    {
                        foreach (var genbaDetailItem in this.genbamemoDetailList)
                        {
                            // システムID
                            genbaDetailItem.SYSTEM_ID = GenbaMemoEntry.SYSTEM_ID;
                            // 枝番
                            genbaDetailItem.SEQ = GenbaMemoEntry.SEQ;
                            // 更新日
                            genbaDetailItem.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            genbaDetailItem.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            genbaDetailItem.UPDATE_PC = SystemInformation.ComputerName;
                            // 現場メモ明細テーブルに追加
                            this.genbamemoDetailDAO.Insert(genbaDetailItem);
                        }
                    }

                    //1-1-4 リンク情報(M_FILE_LINK_GENBAMEMO_ENTRY)を取得する
                    var linkentityList = new List<M_FILE_LINK_GENBAMEMO_ENTRY>();
                    linkentityList = fileLinkGenbamemoDao.GetDataBySystemId(genbasystem_id);

                    //2-1 変更後の定期配車明細でデータを作成
                    //2-1-1 現場メモ）新規でデータを登録する
                    SqlInt16 denshuKbn = SqlInt16.Parse(((int)DENSHU_KBN.GENBA_MEMO).ToString());
                    //SYSTEM_ID
                    GenbaMemoEntry.SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
                    // 枝番
                    GenbaMemoEntry.SEQ = 1;
                    //現場No
                    GenbaMemoEntry.GENBAMEMO_NUMBER = this.createGenbamemoNumber();
                    //発生元伝票番号
                    GenbaMemoEntry.HASSEIMOTO_SYSTEM_ID = genba_haishanumber;
                    //発生元行番号
                    GenbaMemoEntry.HASSEIMOTO_DETAIL_SYSTEM_ID = Int16.Parse(genba_haisharownumber.ToString());
                    // 削除フラグを設定
                    GenbaMemoEntry.HIHYOUJI_FLG = false;
                    // 非表示日を設定
                    GenbaMemoEntry.HIHYOUJI_DATE = SqlDateTime.Null;
                    // 非表示登録者を設定
                    GenbaMemoEntry.HIHYOUJI_TOUROKUSHA_NAME = string.Empty;
                    // 削除フラグを設定
                    GenbaMemoEntry.DELETE_FLG = false;

                    this.genbamemoEntryDAO.Insert(GenbaMemoEntry);

                    //2-1-2 現場メモ明細）新規でデータを作成する
                    if (this.genbamemoDetailList != null && this.genbamemoDetailList.Count > 0)
                    {
                        foreach (var genbaDetailItem in this.genbamemoDetailList)
                        {
                            // システムID
                            genbaDetailItem.SYSTEM_ID = GenbaMemoEntry.SYSTEM_ID;
                            // 枝番
                            genbaDetailItem.SEQ = GenbaMemoEntry.SEQ;
                            // 現場メモテーブルに追加
                            this.genbamemoDetailDAO.Insert(genbaDetailItem);
                        }
                    }

                    //2-1-3 リンク情報(M_FILE_LINK_GENBAMEMO_ENTRY)を作成する
                    foreach (var id in linkentityList)
                    {
                        id.SYSTEM_ID = GenbaMemoEntry.SYSTEM_ID;
                        this.fileLinkGenbamemoDao.Insert(id);
                    }
                }
                #endregion
                result = true;

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion

        #region 現場メモ番号を採番
        /// <summary>
        /// 現場メモ番号を採番
        /// </summary>
        /// <returns>採番した数値</returns>
        private SqlInt64 createGenbamemoNumber()
        {
            SqlInt64 returnVal = -1;

            try
            {
                LogUtility.DebugMethodStart();

                var entity = new S_NUMBER_DENSHU();
                entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.GENBA_MEMO.GetHashCode();

                // IS_NUMBER_DENSHUDao(共通)
                IS_NUMBER_DENSHUDao numberDenshuDao = DaoInitUtility.GetComponent<IS_NUMBER_DENSHUDao>();
                var updateEntity = numberDenshuDao.GetNumberDenshuData(entity);
                returnVal = numberDenshuDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_DENSHU();
                    updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.GENBA_MEMO.GetHashCode();
                    updateEntity.CURRENT_NUMBER = returnVal;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    numberDenshuDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnVal;
                    numberDenshuDao.Update(updateEntity);
                }

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

        #region モバイル登録事前チェック
        /// <summary>
        /// モバイル登録チェック
        /// ※モバイルオプションかつ、作業日＝当日が前提条件
        /// 　かつ、モバイル連携可能条件を満たしている事
        /// </summary>
        /// <returns></returns>
        public bool MobileRegistCheck_pre()
        {

            string CheckGyousha = string.Empty;

            //モバイル連携チェック用
            foreach (DataGridViewRow row in this.form.Ichiran.Rows)
            {

                if ((bool)row.Cells["MOBILE_RENKEI"].Value)
                {
                    string CheckHuriTeikiNumber = row.Cells["HURI_HAISHA_NUMBER"].Value.ToString();

                    //配車番号未入力
                    //NAVITIME連携
                    //ロジコン連携
                    //のチェックは、[対象]チェックONの方で行っているので、二重でチェックは行わない

                    //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                    //[システム日付] != [作業日]の場合はチェックをつけない
                    if ((row.Cells["HURI_SAGYOU_DATE"].Value == null)
                        || (string.IsNullOrEmpty(row.Cells["HURI_SAGYOU_DATE"].Value.ToString())))
                    {
                        return false;
                    }
                    else
                    {
                        if (!(DateTime.Parse(row.Cells["HURI_SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd")).Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                        {
                            return false;
                        }
                    }

                    //6)モバイル作成条件か、7)取引先有無チェック、8)取引先有無チェック
                    if (this.RenkeiCheck(6, CheckHuriTeikiNumber) || this.RenkeiCheck(7, CheckHuriTeikiNumber)
                         || this.RenkeiCheck(8, row.Cells["GYOUSHA_CD"].Value.ToString()))
                    {
                        return false;
                    }
                }
            }

            return true;

        }
        #endregion モバイル登録事前チェック

        #region モバイル採番
        /// <summary>
        /// シーケンシャルナンバー採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 CreateMobileSeqNo()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.MOBILE_RENKEI.GetHashCode(); ;

            // IS_NUMBER_SYSTEMDao(共通)
            IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

            var updateEntity = numberSystemDao.GetNumberSystemData(entity);
            returnInt = numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.MOBILE_RENKEI.GetHashCode(); ;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }
        #endregion モバイル採番

        #region モバイル情報登録処理
        /// <summary>
        /// モバイル登録処理
        /// </summary>
        /// <param name="dr"></param>
        private bool registMobileInfo()
        {
            LogUtility.DebugMethodStart();
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(this.Renkei_TeikiDetailSystemId))
                {
                    //[モバイル連携]にチェックをつけていたデータを一括で処理
                    this.searchDto = new DTOClass();
                    this.searchDto.DETAIL_SYSTEM_ID = this.Renkei_TeikiDetailSystemId;
                    this.searchDto.SAGYOU_DATE_FROM = DateTime.Now.ToString("yyyy/MM/dd");
                    this.searchDto.SAGYOU_DATE_TO = DateTime.Now.ToString("yyyy/MM/dd");
                    this.ResultTable = new DataTable();
                    this.ResultTable = this.teikiHaishaDao.GetDataToMRDataTable(this.searchDto);
                    if (this.ResultTable.Rows.Count > 0)
                    {
                        //Entity作成
                        if (!this.CreateMobileEntity())
                        {
                            return result;
                        }
                    }
                }
                else
                {
                    //[モバイル連携]にチェックが無い場合は、初期化しておく
                    this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                    this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                    this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();
                }

                // モバイル将軍業務TBLテーブル登録
                foreach (T_MOBISYO_RT detail in this.entitysMobisyoRtList)
                {
                    this.TmobisyoRtDao.Insert(detail);
                }
                //定期配車は、コンテナ無し
                // モバイル将軍業務詳細TBLテーブル登録           
                foreach (T_MOBISYO_RT_DTL detail in this.entitysMobisyoRtDTLList)
                {
                    this.TmobisyoRtDTLDao.Insert(detail);
                }
                // モバイル将軍業務搬入TBL テーブル登録           
                foreach (T_MOBISYO_RT_HANNYUU detail in this.entitysMobisyoRtHNList)
                {
                    this.TmobisyoRtHNDao.Insert(detail);
                }
                // モバイル将軍業務TBLテーブル修正（除外登録）
                foreach (T_MOBISYO_RT detail in this.entitysMobisyoRtDELList)
                {
                    this.TmobisyoRtDao.Update(detail);
                }

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion

        #region モバイルentity作成
        /// <summary>
        /// データチェックした時に取得した情報からEntityを作成する
        /// </summary>
        /// <param name="isDelete">True削除:False登録</param>
        /// <returns></returns>
        public bool CreateMobileEntity()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();
                int ZenHaishaNo = -1;
                int HaishaNo;
                int ZenHaishaRowNo = -1;
                int HaishaRowNo;
                DateTime now = DateTime.Now;

                foreach (DataRow tableRow in this.ResultTable.Rows)
                {
                    //モバイル連携にチェックが付いてた明細だけ処理
                    // 定期配車番号
                    HaishaNo = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                    // 定期配車行番号
                    HaishaRowNo = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());

                    #region T_MOBISYO_RT
                    // 定期配車番号行番号をカウント作成
                    if (ZenHaishaNo != HaishaNo || ZenHaishaRowNo != HaishaRowNo)
                    {
                        // entitys作成
                        this.entitysMobisyoRt = new T_MOBISYO_RT();
                        // シーケンシャルナンバー
                        this.entitysMobisyoRt.SEQ_NO = this.CreateMobileSeqNo();

                        // 車種CD
                        if (!string.IsNullOrEmpty(tableRow["SHASHU_CD"].ToString()))
                        {
                            this.entitysMobisyoRt.SHASHU_CD = tableRow["SHASHU_CD"].ToString();
                        }
                        // 車種名
                        if (!string.IsNullOrEmpty(tableRow["SHASHU_NAME"].ToString()))
                        {
                            this.entitysMobisyoRt.SHASHU_NAME = tableRow["SHASHU_NAME"].ToString();
                        }
                        // 車輌CD
                        if (!string.IsNullOrEmpty(tableRow["SHARYOU_CD"].ToString()))
                        {
                            this.entitysMobisyoRt.SHARYOU_CD = tableRow["SHARYOU_CD"].ToString();
                        }
                        // 車輌名
                        if (!string.IsNullOrEmpty(tableRow["SHARYOU_NAME"].ToString()))
                        {
                            this.entitysMobisyoRt.SHARYOU_NAME = tableRow["SHARYOU_NAME"].ToString();
                        }
                        // 運転者名
                        if (!string.IsNullOrEmpty(tableRow["UNTENSHA_NAME"].ToString()))
                        {
                            this.entitysMobisyoRt.UNTENSHA_NAME = tableRow["UNTENSHA_NAME"].ToString();
                        }
                        // 運転者名CD
                        if (!string.IsNullOrEmpty(tableRow["UNTENSHA_CD"].ToString()))
                        {
                            this.entitysMobisyoRt.UNTENSHA_CD = tableRow["UNTENSHA_CD"].ToString();
                        }
                        // (配車)作業日
                        if (!SqlDateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString()).IsNull)
                        {
                            this.entitysMobisyoRt.HAISHA_SAGYOU_DATE = SqlDateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString());
                        }
                        // (配車)伝票番号
                        this.entitysMobisyoRt.HAISHA_DENPYOU_NO = SqlInt64.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                        // (配車)コース名称CD
                        if (!string.IsNullOrEmpty(tableRow["HAISHA_COURSE_NAME_CD"].ToString()))
                        {
                            this.entitysMobisyoRt.HAISHA_COURSE_NAME_CD = tableRow["HAISHA_COURSE_NAME_CD"].ToString();
                        }
                        // (配車)コース名称
                        if (!string.IsNullOrEmpty(tableRow["HAISHA_COURSE_NAME"].ToString()))
                        {
                            this.entitysMobisyoRt.HAISHA_COURSE_NAME = tableRow["HAISHA_COURSE_NAME"].ToString();
                        }
                        // (配車)配車区分 0
                        this.entitysMobisyoRt.HAISHA_KBN = 0;
                        // 登録日時 Insertした日次
                        this.entitysMobisyoRt.GENBA_JISSEKI_CREATEDATE = SqlDateTime.Parse(DateTime.Now.ToString());//parentForm.sysDate;
                        // 修正日時 Insertした日次
                        this.entitysMobisyoRt.GENBA_JISSEKI_UPDATEDATE = SqlDateTime.Parse(DateTime.Now.ToString());
                        // 業者CD
                        if (!string.IsNullOrEmpty(tableRow["GENBA_JISSEKI_GYOUSHACD"].ToString()))
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_GYOUSHACD = tableRow["GENBA_JISSEKI_GYOUSHACD"].ToString();
                        }
                        // 現場CD
                        if (!string.IsNullOrEmpty(tableRow["GENBA_JISSEKI_GENBACD"].ToString()))
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_GENBACD = tableRow["GENBA_JISSEKI_GENBACD"].ToString();
                        }
                        // 追加現場フラグ 基本的には0。データを登録するとき、作業日＝当日の場合、1
                        if (!SqlDateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString()).IsNull &&
                            (DateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd") == (parentForm.sysDate).ToString("yyyy/MM/dd")))
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_ADDGENBAFLG = true;
                        }
                        else
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_ADDGENBAFLG = false;
                        }
                        // 指示確認フラグ 0
                        this.entitysMobisyoRt.SHIJI_FLG = false;
                        // 除外フラグ 0
                        this.entitysMobisyoRt.GENBA_JISSEKI_JYOGAIFLG = false;
                        // マニフェスト区分 0
                        this.entitysMobisyoRt.GENBA_DETAIL_MANIKBN = 0;
                        // ステータス
                        this.entitysMobisyoRt.GENBA_STTS = "0";
                        // 実績登録フラグ
                        this.entitysMobisyoRt.JISSEKI_REGIST_FLG = false;
                        // 運搬業者CD
                        if (!string.IsNullOrEmpty(tableRow["GENBA_JISSEKI_UPNGYOSHACD"].ToString()))
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_UPNGYOSHACD = tableRow["GENBA_JISSEKI_UPNGYOSHACD"].ToString();
                        }
                        else
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_UPNGYOSHACD = string.Empty;
                        }
                        // (配車)行番号
                        this.entitysMobisyoRt.HAISHA_ROW_NUMBER = SqlInt32.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());

                        // 削除フラグ
                        this.entitysMobisyoRt.DELETE_FLG = false;

                        // 20170601 wangjm モバイル将軍#105481 start
                        this.entitysMobisyoRt.KAISHU_NO = SqlInt32.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                        this.entitysMobisyoRt.KAISHU_BIKOU = tableRow["GENBA_MEISAI_BIKOU"].ToString();
                        // 20170601 wangjm モバイル将軍#105481 end

                        // 自動設定
                        var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT>(this.entitysMobisyoRt);
                        dataBinderContenaResult.SetSystemProperty(this.entitysMobisyoRt, false);

                        // Listに追加
                        this.entitysMobisyoRtList.Add(this.entitysMobisyoRt);
                    }
                    #endregion

                    // 前回定期配車番号
                    ZenHaishaNo = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                    // 前回定期配車行番号
                    ZenHaishaRowNo = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                }

                int ZenHaishaNo2 = -1;
                int HaishaNo2;
                int ZenHaishaRowNo2 = -1;
                int HaishaRowNo2;
                string NiorosiNo2;
                int Edaban2 = 0;
                List<NiorosiClass> niorosiList = new List<NiorosiClass>();
                foreach (DataRow tableRow in this.ResultTable.Rows)
                {
                    // 定期配車番号
                    HaishaNo2 = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                    // 定期配車行番号
                    HaishaRowNo2 = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                    // 荷降番号
                    if (string.IsNullOrEmpty(tableRow["NIOROSHI_NUMBER"].ToString()))
                    {
                        NiorosiNo2 = null;
                    }
                    else
                    {
                        NiorosiNo2 = tableRow["NIOROSHI_NUMBER"].ToString();
                    }

                    if (ZenHaishaNo2 != HaishaNo2 || ZenHaishaRowNo2 != HaishaRowNo2)
                    {
                        // 枝番
                        Edaban2 = 0;
                    }

                    if (ZenHaishaNo2 != HaishaNo2 && NiorosiNo2 != null)
                    {
                        niorosiList = new List<NiorosiClass>();
                        DataTable niorosiTable = this.teikiHaishaDao.GetMobilNioroshiData(HaishaNo2, int.Parse(NiorosiNo2));
                        if (niorosiTable != null && niorosiTable.Rows.Count > 0)
                        {
                            foreach (DataRow niorosiRow in niorosiTable.Rows)
                            {
                                NiorosiClass niorosi = new NiorosiClass();
                                niorosi.TEIKI_HAISHA_NUMBER = niorosiRow["HAISHA_DENPYOU_NO"].ToString();
                                niorosi.NIOROSHI_NUMBER = niorosiRow["NIOROSHI_NUMBER"].ToString();
                                niorosi.HANYU_SEQ_NO = SqlInt64.Parse(niorosiRow["HANYU_SEQ_NO"].ToString());
                                niorosiList.Add(niorosi);
                            }
                        }

                    }
                    // 前回定期配車番号
                    ZenHaishaNo2 = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                    // 前回定期配車行番号
                    ZenHaishaRowNo2 = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());

                    // 品名なしの場合、T_MOBISYO_RT_DTLデータを作成しない。
                    if (string.IsNullOrEmpty(tableRow["GENBA_DETAIL_HINMEICD"].ToString()))
                    {
                        continue;
                    }

                    #region T_MOBISYO_RT_DTL
                    // 枝番
                    Edaban2++;

                    // entitys作成
                    this.entitysMobisyoRtDTL = new T_MOBISYO_RT_DTL();
                    // シーケンシャルナンバー

                    List<T_MOBISYO_RT> data = (from temp in entitysMobisyoRtList
                                               where temp.HAISHA_DENPYOU_NO.ToString().Equals(HaishaNo2.ToString()) &&
                                                       temp.HAISHA_ROW_NUMBER.ToString().Equals(HaishaRowNo2.ToString())
                                               select temp).ToList();
                    this.entitysMobisyoRtDTL.SEQ_NO = data[0].SEQ_NO;
                    List<NiorosiClass> niorosiData = null;
                    if (NiorosiNo2 != null)
                    {
                        niorosiData = (from temp in niorosiList
                                       where temp.TEIKI_HAISHA_NUMBER.ToString().Equals(HaishaNo2.ToString()) &&
                                       temp.NIOROSHI_NUMBER.ToString().Equals(NiorosiNo2.ToString())
                                       select temp).ToList();
                    }
                    // 搬入シーケンシャルナンバー
                    if (niorosiData != null && niorosiData.Count > 0 && NiorosiNo2 != null)
                    {
                        this.entitysMobisyoRtDTL.HANYU_SEQ_NO = niorosiData[0].HANYU_SEQ_NO;
                    }
                    else
                    {
                        if (NiorosiNo2 != null)
                        {
                            this.entitysMobisyoRtDTL.HANYU_SEQ_NO = this.CreateMobileSeqNo();
                            NiorosiClass niorosi = new NiorosiClass();
                            niorosi.TEIKI_HAISHA_NUMBER = HaishaNo2.ToString();
                            niorosi.NIOROSHI_NUMBER = NiorosiNo2;
                            niorosi.HANYU_SEQ_NO = this.entitysMobisyoRtDTL.HANYU_SEQ_NO;
                            niorosiList.Add(niorosi);

                            #region T_MOBISYO_RT_HANNYUU
                            // entitys作成
                            this.entitysMobisyoRtHN = new T_MOBISYO_RT_HANNYUU();

                            // 搬入シーケンシャルナンバー
                            this.entitysMobisyoRtHN.HANYU_SEQ_NO = this.entitysMobisyoRtDTL.HANYU_SEQ_NO;
                            // 枝番1を設定する
                            this.entitysMobisyoRtHN.EDABAN = 1;

                            SqlInt64 SYSTEM_ID = SqlInt64.Parse(tableRow["SYSTEM_ID"].ToString());
                            SqlInt32 SEQ = SqlInt32.Parse(tableRow["SEQ"].ToString());
                            SqlInt32 NIOROSHI_NUMBER = SqlInt32.Parse(tableRow["NIOROSHI_NUMBER"].ToString());

                            DataTable NioroshiData = this.teikiHaishaDao.GetTeikiHaishaNioroshiData(SYSTEM_ID, SEQ, NIOROSHI_NUMBER);

                            if (NioroshiData.Rows.Count > 0)
                            {
                                // (搬入)業者CD
                                if (!string.IsNullOrEmpty(NioroshiData.Rows[0]["HANNYUU_GYOUSHACD"].ToString()))
                                {
                                    this.entitysMobisyoRtHN.HANNYUU_GYOUSHACD = NioroshiData.Rows[0]["HANNYUU_GYOUSHACD"].ToString();
                                }

                                // (搬入)現場CD
                                if (!string.IsNullOrEmpty(NioroshiData.Rows[0]["HANNYUU_GENBACD"].ToString()))
                                {
                                    this.entitysMobisyoRtHN.HANNYUU_GENBACD = NioroshiData.Rows[0]["HANNYUU_GENBACD"].ToString();
                                }
                            }
                            // (搬入)搬入量
                            this.entitysMobisyoRtHN.HANNYUU_RYO = SqlDouble.Null;
                            this.entitysMobisyoRtHN.HANNYUU_JISSEKI_RYO = SqlDouble.Null;
                            // 搬入フラグ
                            this.entitysMobisyoRtHN.JISSEKI_REGIST_FLG = false;
                            // 削除フラグ
                            this.entitysMobisyoRtHN.DELETE_FLG = false;

                            // 自動設定
                            var dataBinderContenaResultHN = new DataBinderLogic<T_MOBISYO_RT_HANNYUU>(this.entitysMobisyoRtHN);
                            dataBinderContenaResultHN.SetSystemProperty(this.entitysMobisyoRtHN, false);

                            // Listに追加
                            this.entitysMobisyoRtHNList.Add(this.entitysMobisyoRtHN);

                            #endregion
                        }
                    }

                    // 枝番
                    this.entitysMobisyoRtDTL.EDABAN = Edaban2;
                    // (現場明細)品名CD
                    if (!string.IsNullOrEmpty(tableRow["GENBA_DETAIL_HINMEICD"].ToString()))
                    {
                        this.entitysMobisyoRtDTL.GENBA_DETAIL_HINMEICD = tableRow["GENBA_DETAIL_HINMEICD"].ToString();
                    }
                    // (現場明細)単位１
                    if (!string.IsNullOrEmpty(tableRow["GENBA_DETAIL_UNIT_CD1"].ToString()))
                    {
                        this.entitysMobisyoRtDTL.GENBA_DETAIL_UNIT_CD1 = SqlInt16.Parse(tableRow["GENBA_DETAIL_UNIT_CD1"].ToString());
                    }
                    // (現場明細)単位2
                    if (!string.IsNullOrEmpty(tableRow["GENBA_DETAIL_UNIT_CD2"].ToString()))
                    {
                        if (SqlBoolean.Parse(tableRow["KANSAN_UNIT_MOBILE_OUTPUT_FLG"].ToString()).IsTrue)
                        {
                            this.entitysMobisyoRtDTL.GENBA_DETAIL_UNIT_CD2 = SqlInt16.Parse(tableRow["GENBA_DETAIL_UNIT_CD2"].ToString());
                        }
                    }
                    // (現場明細)数量１
                    this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO1 = SqlDouble.Null;
                    // (現場明細)数量２
                    this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO2 = SqlDouble.Null;
                    // (現場明細)追加品名フラグ
                    this.entitysMobisyoRtDTL.GENBA_DETAIL_ADDHINMEIFLG = false;
                    // 回収実績フラグ
                    this.entitysMobisyoRtDTL.JISSEKI_REGIST_FLG = false;
                    // 削除フラグ
                    this.entitysMobisyoRtDTL.DELETE_FLG = false;

                    // 自動設定
                    var dataBinderContenaResultDTL = new DataBinderLogic<T_MOBISYO_RT_DTL>(this.entitysMobisyoRtDTL);
                    dataBinderContenaResultDTL.SetSystemProperty(this.entitysMobisyoRtDTL, false);

                    // Listに追加
                    this.entitysMobisyoRtDTLList.Add(this.entitysMobisyoRtDTL);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMobileEntity", ex);

                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region モバイル除外登録
        public bool CreateMobileDelEntity(DataGridViewRow dr)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // 元配車番号
                int HAISHA_NO = int.Parse(dr.Cells["TEIKI_HAISHA_NUMBER"].Value.ToString());
                // 元明細行
                int HAISHA_ROW = int.Parse(dr.Cells["JUNNBANN"].Value.ToString());

                // 除外登録するデータを、データ取得
                T_MOBISYO_RT mobileEntry = TmobisyoRtDelDao.GetRtDataByJogai(HAISHA_NO, HAISHA_ROW);
                if (mobileEntry != null)
                {
                    mobileEntry.GENBA_JISSEKI_JYOGAIFLG = true;
                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT>(mobileEntry);
                    dataBinderContenaResult.SetSystemProperty(mobileEntry, false);
                    // Listに追加
                    this.entitysMobisyoRtDELList.Add(mobileEntry);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMobileDelEntity", ex);

                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 配車番号検索
        /// <summary>
        /// 配車番号検索
        /// </summary>
        /// <param name="haishaNmuber"></param>
        public T_TEIKI_HAISHA_ENTRY getHaishaNumberInfo(string haishaNmuber, string sagyouDate, out bool catchErr)
        {
            T_TEIKI_HAISHA_ENTRY returnValue = null;
            catchErr = false;
            try
            {
               
                LogUtility.DebugMethodStart(haishaNmuber, sagyouDate);

                T_TEIKI_HAISHA_ENTRY keyEntity = new T_TEIKI_HAISHA_ENTRY();

                keyEntity.TEIKI_HAISHA_NUMBER = int.Parse(haishaNmuber);
                var courseNameCD = this.teikiHaishaDao.GetAllValidDataByHaishaNumber(keyEntity);

                if (courseNameCD != null && courseNameCD.Length > 0)
                {
                    returnValue = courseNameCD[0];
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("getHaishaNumberInfo", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue, catchErr);
            }
            return returnValue;
        }
        #endregion

        #region コース名検索
        /// <summary>
        /// コース名検索
        /// </summary>
        /// <param name="haishaNmuber"></param>
        public M_COURSE_NAME getCourseNameInfo(string haishaNmuber, out bool catchErr)
        {
            M_COURSE_NAME returnValue = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(haishaNmuber);

                M_COURSE_NAME keyEntity = new M_COURSE_NAME();

                keyEntity.COURSE_NAME_CD = haishaNmuber;

                var courseName = this.courseNameDao.GetAllValidData(keyEntity);

                if (courseName.Length > 0)
                {
                    returnValue = courseName[0];
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("getCourseNameInfo", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue, catchErr);
            }
            return returnValue;
        }
        #endregion

        #region 配車番号入力チェック
        /// <summary>
        /// 配車番号入力チェック処理
        /// </summary>
        /// <param name="e"></param>
        public void txtHaishaNo_Validating(CancelEventArgs e)
        {

            LogUtility.DebugMethodStart(e);

            PopupDTOCls dto = new PopupDTOCls();
            DataTable dt = new DataTable();
            bool catchErr = false;

            string haishaNo = this.form.txtHaishaNo.Text;
            string courseCd = this.form.txtCourseCd.Text;
            isInputError = false;

            if (!string.IsNullOrEmpty(haishaNo))
            {
                var haishaNumberEntity = this.getHaishaNumberInfo(haishaNo, "", out catchErr);
                if (catchErr) 
                {
                    isInputError = true;
                    this.form.txtHaishaNo.Focus();
                    return; 
                }

                if (haishaNumberEntity == null)
                {
                    this.form.txtHaishaNo.IsInputErrorOccured = true;
                    this.MsgBox.MessageBoxShow("E076");
                    e.Cancel = true;
                    isInputError = true;
                    this.form.txtHaishaNo.Focus();
                    return;
                }

                this.isInputError = false;
                if (!string.IsNullOrEmpty(courseCd))
                {
                    dto = new PopupDTOCls();
                    dto.TEIKI_HAISHA_NUMBER = haishaNo;
                    dto.COURSE_NAME_CD = courseCd;
                    dt = this.popupDao.GetPopupData(dto);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        this.MsgBox.MessageBoxShowError("選択されたコースの定期配車が存在しませんでした。");
                        e.Cancel = true;
                        isInputError = true;
                        this.form.txtHaishaNo.Focus();
                    }
                    else
                    {
                        this.form.txtHaishaNo.Text = dt.Rows[0][0].ToString();
                        this.form.txtCourseCd.Text = dt.Rows[0][1].ToString();
                        this.form.txtCourseNm.Text = dt.Rows[0][2].ToString();
                        this.form.txtSagyouDate.Text = dt.Rows[0][3].ToString();
                    }
                }
                else
                {
                    var courseNameEntity = this.getCourseNameInfo(haishaNumberEntity.COURSE_NAME_CD, out catchErr);
                    if (catchErr) 
                    {
                        e.Cancel = true;
                        isInputError = true;
                        this.form.txtHaishaNo.Focus(); 
                    }
                    if (courseNameEntity != null)
                    {
                        // コースCD
                        this.form.txtCourseCd.Text = courseNameEntity.COURSE_NAME_CD;
                        // コース名
                        this.form.txtCourseNm.Text = courseNameEntity.COURSE_NAME;
                        //作業日
                        this.form.txtSagyouDate.Text = haishaNumberEntity.SAGYOU_DATE.ToString();
                    }
                    else
                    {
                        this.MsgBox.MessageBoxShowError("選択された定期配車のコースが存在しませんでした。");
                        e.Cancel = true;
                        isInputError = true;
                        this.form.txtHaishaNo.Focus(); 
                    }
                }
            }
            else
            {
                // コースCD
                this.form.txtCourseCd.Text = string.Empty;
                // コース名
                this.form.txtCourseNm.Text = string.Empty;
                // 作業日
                this.form.txtSagyouDate.Text = string.Empty;
            }
            LogUtility.DebugMethodEnd(e);
        }

        #endregion

        #region コース入力チェック
        /// <summary>
        /// 配車番号入力チェック処理
        /// </summary>
        /// <param name="e"></param>
        public void txtCourseCd_Validating(CancelEventArgs e)
        {

            LogUtility.DebugMethodStart(e);

            PopupDTOCls dto = new PopupDTOCls();
            DataTable dt = new DataTable();

            isInputError = false;

            // キャンセルボタン or 未入力時は何もしない
            if (null != this.form.CancelButton && this.form.ActiveControl == this.form.CancelButton || this.form.txtCourseCd.Text.Trim().Length == 0)
            {
                this.form.txtHaishaNo.Clear();
                this.form.txtCourseNm.Clear();
                this.form.txtSagyouDate.Clear();
                return;
            }

            bool catchErr = false;
            var courseNameEntity = this.getCourseNameInfo(this.form.txtCourseCd.Text, out catchErr);
            if (catchErr) { return; }
            if (courseNameEntity != null)
            {
                // コース名
                this.form.txtCourseNm.Text = courseNameEntity.COURSE_NAME;
            }
            else
            {
                this.form.txtCourseCd.IsInputErrorOccured = true;
                e.Cancel = true;

                //レコードが存在しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "コース");
                isInputError = true;
                this.form.txtCourseCd.SelectAll();
                this.form.txtHaishaNo.Clear();
                this.form.txtCourseNm.Clear();
                this.form.txtSagyouDate.Clear();
                return;
            }

            dto = new PopupDTOCls();

            dto.TEIKI_HAISHA_NUMBER = Convert.ToString(this.form.txtHaishaNo.Text);
            dto.COURSE_NAME_CD = Convert.ToString(this.form.txtCourseCd.Text);
            dt = this.popupDao.GetPopupData(dto);
            if (dt == null || dt.Rows.Count == 0)
            {
                this.MsgBox.MessageBoxShowError("選択されたコースの定期配車が存在しませんでした。");
                isInputError = true;

                e.Cancel = true;
            }
            else if (dt.Rows.Count == 1)
            {
                this.form.txtHaishaNo.Text = dt.Rows[0][0].ToString();
                this.form.txtCourseCd.Text = dt.Rows[0][1].ToString();
                this.form.txtCourseNm.Text = dt.Rows[0][2].ToString();
                this.form.txtSagyouDate.Text = dt.Rows[0][3].ToString();
            }
            else
            {
                this.form.txtCourseCd.Text = dt.Rows[0][1].ToString();
                this.form.txtCourseNm.Text = dt.Rows[0][2].ToString();
                PopupForm form2 = new PopupForm(dto);
                form2.StartPosition = FormStartPosition.CenterScreen;
                form2.ShowDialog();
                if (form2.ReturnParams != null)
                {
                    this.form.txtHaishaNo.Text = form2.ReturnParams[0][0].Value.ToString();
                    if (string.IsNullOrEmpty(Convert.ToString(form2.ReturnParams[1][0].Value)))
                    {
                        this.form.txtCourseNm.Text = string.Empty;
                    }
                    else
                    {
                        this.form.txtCourseNm.Text = form2.ReturnParams[2][0].Value.ToString();
                    }

                    //作業日取得
                    dto = new PopupDTOCls();
                    dto.TEIKI_HAISHA_NUMBER = this.form.txtHaishaNo.Text;
                    dto.COURSE_NAME_CD = this.form.txtCourseCd.Text;
                    dt = this.popupDao.GetPopupData(dto);
                    if (!(dt == null || dt.Rows.Count == 0))
                    {
                        this.form.txtSagyouDate.Text = dt.Rows[0][3].ToString();
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            LogUtility.DebugMethodEnd(e);
        }

        #endregion

        #region POP判定処理
        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        public void CheckPopup(KeyEventArgs e, string KB = "0")
        {
            LogUtility.DebugMethodStart(e,KB);

            if (e.KeyCode == Keys.Space)
            {
                PopupDTOCls dto = new PopupDTOCls();
                //ヘッダ配車番号
                if (KB == "1")
                {
                    if (!string.IsNullOrEmpty(this.form.txtCourseCd.Text))
                    {
                        dto.COURSE_NAME_CD = this.form.txtCourseCd.Text;
                    }
                    PopupForm form = new PopupForm(dto);
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.txtHaishaNo.Text = Convert.ToString(form.ReturnParams[0][0].Value);
                        if (string.IsNullOrEmpty(Convert.ToString(form.ReturnParams[1][0].Value)))
                        {
                            this.form.txtCourseCd.Text= string.Empty;
                        }
                        else
                        {
                            this.form.txtCourseCd.Text = Convert.ToString(form.ReturnParams[1][0].Value).PadLeft(6, '0');
                        }
                        this.form.txtCourseNm.Text = Convert.ToString(form.ReturnParams[2][0].Value);
                    }
                    return;
                }
                //ヘッダコース検索
                if (KB == "2")
                {
                    DataGridViewRow row = this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex];
                    if (!string.IsNullOrEmpty(this.form.txtHaishaNo.Text))
                    {
                        dto.TEIKI_HAISHA_NUMBER = this.form.txtHaishaNo.Text;
                    }
                    dto.courseOnly = true;
                    PopupForm form = new PopupForm(dto);
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(form.ReturnParams[0][0].Value)))
                        {
                            this.form.txtCourseCd.Text = string.Empty;
                            this.form.txtCourseNm.Text = string.Empty;
                        }
                        else
                        {
                            this.form.txtCourseCd.Text = Convert.ToString(form.ReturnParams[0][0].Value).PadLeft(6, '0');
                            this.form.txtCourseNm.Text = Convert.ToString(form.ReturnParams[1][0].Value);
                        }
                    }
                    return;
                }
                if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("HURI_HAISHA_NUMBER"))
                {
                    //明細配車検索
                    DataGridViewRow row = this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex];
                    dto.COURSE_NAME_CD = Convert.ToString(row.Cells["HURI_COURSE_CD"].Value);
                    PopupForm form2 = new PopupForm(dto);
                    form2.StartPosition = FormStartPosition.CenterScreen;
                    form2.ShowDialog();
                    if (form2.ReturnParams != null)
                    {
                        if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("HURI_HAISHA_NUMBER"))
                        {
                            this.form.Ichiran.EditingControl.Text = Convert.ToString(form2.ReturnParams[0][0].Value);
                            this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_HAISHA_NUMBER"].Value = form2.ReturnParams[0][0].Value;
                            if (string.IsNullOrEmpty(Convert.ToString(form2.ReturnParams[1][0].Value)))
                            {
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_COURSE_CD"].Value = string.Empty;
                            }
                            else
                            {
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_COURSE_CD"].Value = Convert.ToString(form2.ReturnParams[1][0].Value).PadLeft(6, '0');
                            }
                        }
                        else
                        {
                            this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_HAISHA_NUMBER"].Value = Convert.ToString(form2.ReturnParams[0][0].Value);
                            if (string.IsNullOrEmpty(Convert.ToString(form2.ReturnParams[1][0].Value)))
                            {
                                this.form.Ichiran.EditingControl.Text = string.Empty;
                            }
                            else
                            {
                                this.form.Ichiran.EditingControl.Text = Convert.ToString(form2.ReturnParams[1][0].Value).PadLeft(6, '0');
                            }
                        }
                        this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_COURSE_NAME"].Value = Convert.ToString(form2.ReturnParams[2][0].Value);
                    }
                }
                else if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("HURI_COURSE_CD"))
                {
                    //明細コース検索
                    DataGridViewRow row = this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex];
                    dto.TEIKI_HAISHA_NUMBER = Convert.ToString(row.Cells["HURI_HAISHA_NUMBER"].Value);
                    dto.courseOnly = true;
                    PopupForm form2 = new PopupForm(dto);
                    form2.StartPosition = FormStartPosition.CenterScreen;
                    form2.ShowDialog();
                    if (form2.ReturnParams != null)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(form2.ReturnParams[0][0].Value)))
                        {
                            this.form.Ichiran.EditingControl.Text = string.Empty;
                        }
                        else
                        {
                            this.form.Ichiran.EditingControl.Text = Convert.ToString(form2.ReturnParams[0][0].Value).PadLeft(6, '0');
                            this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_COURSE_CD"].Value = Convert.ToString(form2.ReturnParams[0][0].Value).PadLeft(6, '0');
                        }
                        this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_COURSE_NAME"].Value = Convert.ToString(form2.ReturnParams[1][0].Value);
                    }
                }
            }
        }
        #endregion

        #region 明細判定処理
        /// <summary>
        /// 入力した配車番号とコースCDのチェック
        /// </summary>
        /// <param name="e"></param>
        public void Ichiran_CellValidating(DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = this.form.Ichiran.Rows[e.RowIndex];
            PopupDTOCls dto = new PopupDTOCls();
            DateTime date = DateTime.Now;
            DataTable dt = new DataTable();
            bool catchErr = false;
            switch (this.form.Ichiran.Columns[e.ColumnIndex].Name)
            {
                case ("HURI_HAISHA_NUMBER"):

                    string haishaNo = Convert.ToString(row.Cells["HURI_HAISHA_NUMBER"].Value);
                    string courseCd = Convert.ToString(row.Cells["HURI_COURSE_CD"].Value);
                    string sagyouDate = Convert.ToString(row.Cells["HURI_SAGYOU_DATE"].Value);
                    if (!string.IsNullOrEmpty(haishaNo))
                    {
                        var haishaNumberEntity = this.getHaishaNumberInfo(haishaNo, sagyouDate, out catchErr);
                        if (catchErr) { return; }

                        if (haishaNumberEntity == null)
                        {
                            var iText = row.Cells[e.ColumnIndex] as ICustomTextBox;
                            iText.IsInputErrorOccured = true;
                            this.MsgBox.MessageBoxShow("E076");
                            e.Cancel = true;
                            this.form.Ichiran.BeginEdit(false);
                            return;
                        }

                        if (!string.IsNullOrEmpty(courseCd))
                        {
                            dto = new PopupDTOCls();
                            dto.TEIKI_HAISHA_NUMBER = haishaNo;
                            dto.COURSE_NAME_CD = courseCd;
                            dt = this.popupDao.GetPopupData(dto);
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                row.Cells["HURI_HAISHA_NUMBER"].Style.BackColor = Constans.ERROR_COLOR;
                                this.MsgBox.MessageBoxShowError("選択されたコースの定期配車が存在しませんでした。");
                                e.Cancel = true;
                                //this.isInputError = true;
                            }
                            else
                            {
                                row.Cells["HURI_HAISHA_NUMBER"].Value = dt.Rows[0][0].ToString();
                                row.Cells["HURI_COURSE_CD"].Value = dt.Rows[0][1].ToString();
                                row.Cells["HURI_COURSE_NAME"].Value = dt.Rows[0][2].ToString();
                                row.Cells["HURI_SAGYOU_DATE"].Value = dt.Rows[0][3].ToString();
                            }
                        }
                        else
                        {
                            var courseNameEntity = this.getCourseNameInfo(haishaNumberEntity.COURSE_NAME_CD, out catchErr);
                            if (catchErr) { return; }
                            if (courseNameEntity != null)
                            {
                                // コースCD
                                this.form.Ichiran.Rows[e.RowIndex].Cells["HURI_COURSE_CD"].Value = courseNameEntity.COURSE_NAME_CD;
                                // コース名
                                this.form.Ichiran.Rows[e.RowIndex].Cells["HURI_COURSE_NAME"].Value = courseNameEntity.COURSE_NAME;
                                //作業日
                                this.form.Ichiran.Rows[e.RowIndex].Cells["HURI_SAGYOU_DATE"].Value = haishaNumberEntity.SAGYOU_DATE;
                            }
                        }
                    }
                    else
                    {
                        // コースCD
                        this.form.Ichiran.Rows[e.RowIndex].Cells["HURI_COURSE_CD"].Value = string.Empty;
                        // コース名
                        this.form.Ichiran.Rows[e.RowIndex].Cells["HURI_COURSE_NAME"].Value = string.Empty;
                        //作業日
                        this.form.Ichiran.Rows[e.RowIndex].Cells["HURI_SAGYOU_DATE"].Value = string.Empty;
                        return;
                    }
                    //項目を更新したら[対象]チェックをONにする
                    if (this.beforeHaishaNumber != row.Cells["HURI_HAISHA_NUMBER"].Value || this.beforeCd != row.Cells["HURI_COURSE_CD"].Value)
                    {
                        row.Cells["TAISHO_CHECK"].Value = true;
                    }
                    break;
                case ("HURI_COURSE_CD"):

                    string courseNameCd = Convert.ToString(row.Cells["HURI_COURSE_CD"].Value);
                    if (courseNameCd == this.beforeCd && !this.isInputError)
                    {
                        return;
                    }
                    this.isInputError = false;
                    row.Cells["HURI_COURSE_CD"].Value = courseNameCd.ToUpper();
                    if (string.IsNullOrEmpty(courseNameCd))
                    {
                        row.Cells["HURI_HAISHA_NUMBER"].Value = DBNull.Value;
                        row.Cells["HURI_COURSE_NAME"].Value = string.Empty;
                        row.Cells["HURI_SAGYOU_DATE"].Value = string.Empty;
                        return;
                    }

                    dto = new PopupDTOCls();
                    dto.TEIKI_HAISHA_NUMBER = Convert.ToString(row.Cells["HURI_HAISHA_NUMBER"].Value);
                    dto.COURSE_NAME_CD = Convert.ToString(row.Cells["HURI_COURSE_CD"].Value);
                    dt = this.popupDao.GetPopupData(dto);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        row.Cells["HURI_COURSE_CD"].Style.BackColor = Constans.ERROR_COLOR;
                        row.Cells["HURI_COURSE_NAME"].Value = string.Empty;
                        row.Cells["HURI_SAGYOU_DATE"].Value = string.Empty;
                        this.MsgBox.MessageBoxShowError("選択されたコースの定期配車が存在しませんでした。");
                        e.Cancel = true;
                        this.isInputError = true;
                    }
                    else if (dt.Rows.Count == 1)
                    {
                        row.Cells["HURI_HAISHA_NUMBER"].Value = dt.Rows[0][0].ToString();
                        row.Cells["HURI_COURSE_CD"].Value = dt.Rows[0][1].ToString();
                        row.Cells["HURI_COURSE_NAME"].Value = dt.Rows[0][2].ToString();
                        row.Cells["HURI_SAGYOU_DATE"].Value = dt.Rows[0][3].ToString();
                    }
                    else
                    {
                        row.Cells["HURI_COURSE_CD"].Value = dt.Rows[0][1].ToString();
                        row.Cells["HURI_COURSE_NAME"].Value = dt.Rows[0][2].ToString();
                        row.Cells["HURI_SAGYOU_DATE"].Value = dt.Rows[0][3].ToString();
                        PopupForm form = new PopupForm(dto);
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.ShowDialog();
                        if (form.ReturnParams != null)
                        {
                            this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_HAISHA_NUMBER"].Value = form.ReturnParams[0][0].Value.ToString();
                            if (string.IsNullOrEmpty(Convert.ToString(form.ReturnParams[1][0].Value)))
                            {
                                this.form.Ichiran.EditingControl.Text = string.Empty;
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_COURSE_NAME"].Value = string.Empty;
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_SAGYOU_DATE"].Value = string.Empty;
                            }
                            else
                            {
                                this.form.Ichiran.EditingControl.Text = form.ReturnParams[1][0].Value.ToString().PadLeft(6, '0');
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["HURI_COURSE_NAME"].Value = form.ReturnParams[2][0].Value.ToString();
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            this.isInputError = true;
                        }
                    }
                    //項目を更新したら[対象]チェックをONにする
                    if (this.beforeHaishaNumber != row.Cells["HURI_HAISHA_NUMBER"].Value || this.beforeCd != row.Cells["HURI_COURSE_CD"].Value)
                    {
                        row.Cells["TAISHO_CHECK"].Value = true;
                    }
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 受付伝票・定期配車伝票のモバイル/ロジコン/NAVITIME連携状況チェック
        /// </summary>
        /// <param name="KB"></param>
        /// <param name="NUMBER"></param>
        /// <returns></returns>
        public bool RenkeiCheck(int KB, string NUMBER = "")
        {
            bool RenkeiCheck = false;
            string selectStr;
            DataTable dt = new DataTable();

            if (NUMBER == "")
            {
                return RenkeiCheck;
            }

            //KB:3定期データロジコン連携チェック
            if (KB == 3)
            {
                // ロジこんぱす連携済みであるかをチェックする。
                Int32 NumberM = Int32.Parse(NUMBER);
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = " + KB  // 1：収集受付 3:定期
                    + " and LDD.REF_DENPYOU_NO = " + NumberM
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";

                // データ取得
                dt = this.teikiHaishaDao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    RenkeiCheck = true;
                }
            }

            //KB:4　定期データNAVITIME連携チェック
            if (KB == 4)
            {
                if (AppConfig.AppOptions.IsNAVITIME())
                {
                    Int32 NumberM = Int32.Parse(NUMBER);
                    selectStr = " SELECT * FROM T_TEIKI_HAISHA_ENTRY T "
                               + " INNER JOIN T_NAVI_DELIVERY D ON T.SYSTEM_ID = D.TEIKI_SYSTEM_ID AND D.DELETE_FLG = 0 "
                               + " INNER JOIN T_NAVI_LINK_STATUS L ON D.SYSTEM_ID = L.SYSTEM_ID AND L.LINK_STATUS != 3 "
                               + " WHERE T.DELETE_FLG = 0 "
                               + " AND T.TEIKI_HAISHA_NUMBER = " + NumberM;
                    dt = this.teikiHaishaDao.GetDateForStringSql(selectStr);
                    if (dt.Rows.Count > 0)
                    {
                        RenkeiCheck = true;
                    }
                }
            }

            //KB:6  定期配車伝票が、モバイル状況一覧に表示される条件か(対象外の場合、RenkeiCheck = true)
            //UNTENSHA_CD、SHARYOU_CD、SHASHU_CD、SHASHU_NAME_RYAKUのしずれかが無し→×。
            if (KB == 6)
            {
                Int32 NumberM = Int32.Parse(NUMBER);
                selectStr = "SELECT * FROM T_TEIKI_HAISHA_ENTRY THE "
                        + " LEFT JOIN M_SHASHU ON M_SHASHU.SHASHU_CD = THE.SHASHU_CD"
                        + " WHERE THE.TEIKI_HAISHA_NUMBER = " + NumberM
                        + " AND THE.DELETE_FLG = 0"
                        + " AND (THE.UNTENSHA_CD IS NOT NULL AND THE.UNTENSHA_CD != '') "
                        + " AND (THE.SHARYOU_CD IS NOT NULL AND THE.SHARYOU_CD != '')"
                        + "	AND (THE.SHASHU_CD IS NOT NULL AND THE.SHASHU_CD != '')"
                        + " AND (M_SHASHU.SHASHU_NAME_RYAKU IS NOT NULL AND M_SHASHU.SHASHU_NAME_RYAKU != '')";
                dt = this.teikiHaishaDao.GetDateForStringSql(selectStr);
                if (dt.Rows.Count <= 0)
                {
                    RenkeiCheck = true;
                }
            }

            //KB:7  定期配車伝票無いに、業者の取引有無が、無のデータがあるか
            if (KB == 7)
            {
                Int32 NumberM = Int32.Parse(NUMBER);
                selectStr = "SELECT ENT.TEIKI_HAISHA_NUMBER FROM "
                       + " T_TEIKI_HAISHA_ENTRY ENT"
                       + " LEFT JOIN T_TEIKI_HAISHA_DETAIL DET ON ENT.SYSTEM_ID = DET.SYSTEM_ID AND ENT.SEQ = DET.SEQ"
                       + " LEFT JOIN M_GYOUSHA ON DET.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD"
                       + " WHERE"
                       + " ENT.TEIKI_HAISHA_NUMBER = " + NumberM
                       + " AND ENT.DELETE_FLG = 0 AND M_GYOUSHA.TORIHIKISAKI_UMU_KBN = 2";
                dt = this.teikiHaishaDao.GetDateForStringSql(selectStr);
                if (dt.Rows.Count > 0)
                {
                    RenkeiCheck = true;
                }
            }

            //KB:8  定期配車伝票無いに、業者の取引有無が、無のデータがあるか
            if (KB == 8)
            {
                selectStr = "SELECT M_GYOUSHA.GYOUSHA_CD FROM M_GYOUSHA"
                       + " WHERE"
                       + " M_GYOUSHA.GYOUSHA_CD = '" + NUMBER + "'"
                       + " AND M_GYOUSHA.DELETE_FLG = 0 AND M_GYOUSHA.TORIHIKISAKI_UMU_KBN = 2";
                dt = this.teikiHaishaDao.GetDateForStringSql(selectStr);
                if (dt.Rows.Count > 0)
                {
                    RenkeiCheck = true;
                }
            }
            return RenkeiCheck;
        }

        #region 搬入先休動チェック
        internal bool HannyuusakiDateCheck(string SystemId, string DetailSystemId, string SagyoHi)
        {
            try
            {

                string inputSagyouDate = SagyoHi;
                string inputNioroshiGyoushaCd = string.Empty;
                string inputNioroshiGenbaCd = string.Empty;

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                DataTable teikiHaishaNList = new DataTable();
                T_TEIKI_HAISHA_SHOUSAI teikihaishaNParam = new T_TEIKI_HAISHA_SHOUSAI();
                // システムID
                teikihaishaNParam.SYSTEM_ID = SqlInt64.Parse(SystemId);
                // DETAIL_SYSTEM_ID
                teikihaishaNParam.DETAIL_SYSTEM_ID = SqlInt64.Parse(DetailSystemId);

                teikiHaishaNList = teikiHaishaShousaiDao.GetNioroshiData(teikihaishaNParam);

                if (teikiHaishaNList != null && teikiHaishaNList.Rows.Count > 0)
                {
                    for (int i = 0; i < teikiHaishaNList.Rows.Count; i++)
                    {
                        M_WORK_CLOSED_HANNYUUSAKI workclosedhannyuusakiEntry = new M_WORK_CLOSED_HANNYUUSAKI();
                        //荷降業者CD取得
                        workclosedhannyuusakiEntry.GYOUSHA_CD = teikiHaishaNList.Rows[i]["NIOROSHI_GYOUSHA_CD"].ToString();
                        //荷降現場CD取得
                        workclosedhannyuusakiEntry.GENBA_CD = teikiHaishaNList.Rows[i]["NIOROSHI_GENBA_CD"].ToString();
                        //作業日取得
                        workclosedhannyuusakiEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                        M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = workclosedhannyuusakiDao.GetAllValidData(workclosedhannyuusakiEntry);

                        //取得テータ
                        if (workclosedhannyuusakiList.Count() >= 1)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex1);
                this.MsgBox.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }

            return true;
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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
