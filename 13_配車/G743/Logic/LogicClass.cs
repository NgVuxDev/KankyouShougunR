using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Allocation.CarTransferSpot
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.CarTransferSpot.Setting.ButtonSetting.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass searchDto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;
        /// <summary>
        /// 不正な入力をされたかを示します
        /// </summary>
        internal bool isInputError = false;

        /// <summary>
        /// 拠点マスタDao
        /// </summary>
        private IM_KYOTENDao kyotenDao;
        /// <summary>
        /// 業者マスタDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;
        /// <summary>
        /// 車輌マスタのDao
        /// </summary>
        private IM_SHARYOUDao sharyouDao;
        /// <summary>
        /// 車輌マスタのDao
        /// </summary>
        private M_SHARYOUDao cussharyouDao;
        /// <summary>
        /// 車種マスタのDao
        /// </summary>
        private IM_SHASHUDao shashuDao;
        /// <summary>
        /// 社員マスタのDao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// 明細項目の更新前情報
        /// </summary>
        internal string oldKyotenCD;
        internal string oldSagyouDate;
        internal string oldUnpanGyoushaCD;
        internal string oldUnpanGyoushaName;
        internal string oldShasyuCD;
        internal string oldSharyouCD;
        internal string oldUntenshaCD;

        /// <summary>
        /// (配車)伝票番号
        /// </summary>
        internal string haishaDenpyouNo { get; set; }

        /// <summary>
        /// 検索条件 : 受付番号
        /// </summary>
        public String UketsukeNumber { get; set; }

        private GET_SYSDATEDao dateDao;
        
        internal MessageBoxShowLogic MsgBox;

        /// <summary>
        /// チェックボックスのスペースキー対応用
        /// </summary>
        internal bool SpaceChk = false;
        internal bool SpaceON = false;

        /// <summary>
        /// 取得した社員エンティティを保持する
        /// </summary>
        private List<M_SHAIN> shainList = new List<M_SHAIN>();

        #region 休動
        /// <summary>
        /// 車輌休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workclosedsharyouDao;

        /// <summary>
        /// 運転者休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workcloseduntenshaDao;

        /// <summary>
        /// 搬入先休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_HANNYUUSAKIDao workclosedhannyuusakiDao;
        #endregion

        #region 受付
        /// <summary>
        /// 受付（収集）入力のDao
        /// </summary>
        private T_UKETSUKE_SS_ENTRYDao UketsukeSSdao;

        /// <summary>
        /// 受付（収集）明細のDao
        /// </summary>
        private T_UKETSUKE_SS_DETAILDao UketsukeSSDetaildao;

        /// <summary>
        /// コンテナ稼働予定のDao
        /// </summary>
        private T_CONTENA_RESERVEDao Contenadao;
        
        /// <summary>
        /// 受付（収集）入力のDao
        /// </summary>
        private T_UKETSUKE_SK_ENTRYDao UketsukeSKdao;

        /// <summary>
        /// 受付（収集）明細のDao
        /// </summary>
        private T_UKETSUKE_SK_DETAILDao UketsukeSKDetaildao;

        /// <summary>
        /// 受付収集入力Entityを格納
        /// </summary>
        private List<T_UKETSUKE_SS_ENTRY> SSEntryEntityList = new List<T_UKETSUKE_SS_ENTRY>();

        /// <summary>
        /// 受付収集明細Entityを格納
        /// </summary>
        private List<T_UKETSUKE_SS_DETAIL> SSDetailEntityList = new List<T_UKETSUKE_SS_DETAIL>();

        /// <summary>
        /// コンテナ指定Entityを格納
        /// </summary>
        private List<T_CONTENA_RESERVE> SSContenaEntityList = new List<T_CONTENA_RESERVE>();

        /// <summary>
        /// 受付収集入力Entityを格納
        /// </summary>
        private List<T_UKETSUKE_SK_ENTRY> SKEntryEntityList = new List<T_UKETSUKE_SK_ENTRY>();

        /// <summary>
        /// 受付収集明細Entityを格納
        /// </summary>
        private List<T_UKETSUKE_SK_DETAIL> SKDetailEntityList = new List<T_UKETSUKE_SK_DETAIL>();
        #endregion

        #region モバイル

        /// <summary>
        /// モバイル将軍業務TBLのentity
        /// </summary>
        private T_MOBISYO_RT entitysMobisyoRt { get; set; }

        /// <summary>
        /// モバイル将軍業務TBLのDao
        /// </summary>
        private IT_MOBISYO_RTDao TmobisyoRtDao;

        /// <summary>
        /// モバイル将軍業務コンテナTBLのDao
        /// </summary>
        private IT_MOBISYO_RT_CONTENADao TmobisyoCTNDao;

        /// <summary>
        /// モバイル将軍業務詳細TBLのDao
        /// </summary>
        private IT_MOBISYO_RT_DTLDao TmobisyoRtDTLDao;

        /// <summary>
        /// モバイル将軍業務搬入TBLのDao
        /// </summary>
        private IT_MOBISYO_RT_HANNYUUDao TmobisyoRtHNDao;

        /// <summary>
        /// モバイル将軍業務TBLのDao
        /// </summary>
        private T_MOBISYO_RTDao mTmobisyoRtDao;

        /// <summary>
        /// モバイル将軍業務コンテナTBLのDao
        /// </summary>
        private T_MOBISYO_RT_CONTENADao mTmobisyoRtCTNDao;

        /// <summary>
        /// モバイル将軍業務詳細TBLのDao
        /// </summary>
        private T_MOBISYO_RT_DTLDao mTmobisyoRtDTLDao;

        /// <summary>
        /// モバイル将軍業務搬入TBLのDao
        /// </summary>
        private T_MOBISYO_RT_HANNYUUDao mTmobisyoRtHNDao;

        /// <summary>
        /// モバイル将軍業務TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT> entitysMobisyoRtList { get; set; }

        /// <summary>
        /// モバイル将軍業務コンテナTBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT_CONTENA> entitysMobisyoRtCTNList { get; set; }

        /// <summary>
        /// モバイル将軍業務詳細TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT_DTL> entitysMobisyoRtDTLList { get; set; }

        /// <summary>
        /// モバイル将軍業務搬入TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT_HANNYUU> entitysMobisyoRtHNList { get; set; }

        #endregion

        #endregion

        #region プロパティ
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResultEntry { get; set; }
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResultDetail { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.searchDto = new DTOClass();
            this.oldKyotenCD = string.Empty;
            this.oldSagyouDate = string.Empty;
            this.oldUnpanGyoushaCD = string.Empty;
            this.oldUnpanGyoushaName = string.Empty;
            this.oldShasyuCD = string.Empty;
            this.oldSharyouCD = string.Empty;
            this.oldUntenshaCD = string.Empty;

            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();
            this.cussharyouDao = DaoInitUtility.GetComponent<M_SHARYOUDao>();
            this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();
            this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();

            this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
            this.workclosedhannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();

            this.UketsukeSSdao = DaoInitUtility.GetComponent<T_UKETSUKE_SS_ENTRYDao>();
            this.UketsukeSSDetaildao = DaoInitUtility.GetComponent<T_UKETSUKE_SS_DETAILDao>();
            this.Contenadao = DaoInitUtility.GetComponent<T_CONTENA_RESERVEDao>();
            this.UketsukeSKdao = DaoInitUtility.GetComponent<T_UKETSUKE_SK_ENTRYDao>();
            this.UketsukeSKDetaildao = DaoInitUtility.GetComponent<T_UKETSUKE_SK_DETAILDao>();

            this.TmobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
            this.TmobisyoCTNDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_CONTENADao>();
            this.TmobisyoRtDTLDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_DTLDao>();
            this.TmobisyoRtHNDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_HANNYUUDao>();
            this.mTmobisyoRtDao = DaoInitUtility.GetComponent<T_MOBISYO_RTDao>();
            this.mTmobisyoRtCTNDao = DaoInitUtility.GetComponent<T_MOBISYO_RT_CONTENADao>();
            this.mTmobisyoRtDTLDao = DaoInitUtility.GetComponent<T_MOBISYO_RT_DTLDao>();
            this.mTmobisyoRtHNDao = DaoInitUtility.GetComponent<T_MOBISYO_RT_HANNYUUDao>();
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
                this.form.checkBoxAll.Visible = false;
                this.form.checkBoxAll2.Visible = false;
                //ボタンのテキストを初期化
                this.ButtonInit();
                //イベントの初期化処理
                this.EventInit();
                this.UketsukeNumber = this.haishaDenpyouNo;
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
                    string haishaKbn = "1"; //受付
                    string DenpyouNo = this.form.Ichiran.CurrentRow.Cells["UKETSUKE_NUMBER"].Value.ToString();
                    r_framework.FormManager.FormManager.OpenFormModal("G668", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DenpyouNo, haishaKbn);
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

                // 選択チェック
                bool taisyou = false;
                string inputSagyouDate = string.Empty;      //作業日
                string inputUnpanGyoushaCd = string.Empty;  //運搬業者
                string inputSharyouCd = string.Empty;       //車輌
                string inputUntenshaCd = string.Empty;      //運転者
                string inputHojoCd = string.Empty;          //補助員
                string inputHanGyoushaCd = string.Empty;    //搬入業者CD
                string inputHanGenCd = string.Empty;        //搬入現場
                string Err_holiday = string.Empty;          //休働エラー
                string S_holiday = string.Empty;            //車輌休働カウント
                string U_holiday = string.Empty;            //運転者休働カウント
                string H_holiday = string.Empty;            //補助員休働カウント
                string N_holiday = string.Empty;            //荷卸先休働カウント
                string Err_Msg = string.Empty;              //ロジコンチェック用アラート

                for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                {
                    if ((bool)this.form.Ichiran.Rows[i].Cells[1].Value)
                    {
                        taisyou = true;

                        //拠点CDがあるかチェック
                        if (string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["KYOTEN_CD"].Value.ToString()))
                        {
                            this.MsgBox.MessageBoxShow("E012", "拠点CD");
                            return;
                        }

                        #region 休働チェック
                        //初期化
                        S_holiday = string.Empty;
                        U_holiday = string.Empty;
                        H_holiday = string.Empty;
                        N_holiday = string.Empty;
                        inputUnpanGyoushaCd = "";
                        inputUntenshaCd = "";
                        inputSharyouCd = "";
                        inputHojoCd = "";
                        inputHanGyoushaCd = "";
                        inputHanGenCd = "";

                        inputSagyouDate = Convert.ToString(this.form.Ichiran.Rows[i].Cells["SAGYOU_DATE"].Value);
                        if (this.form.Ichiran.Rows[i].Cells["UNPAN_GYOUSHA_CD"].Value != null)
                        {
                            inputUnpanGyoushaCd = this.form.Ichiran.Rows[i].Cells["UNPAN_GYOUSHA_CD"].Value.ToString();
                        }
                        if (this.form.Ichiran.Rows[i].Cells["UNTENSHA_CD"].Value != null)
                        {
                            inputUntenshaCd = this.form.Ichiran.Rows[i].Cells["UNTENSHA_CD"].Value.ToString();
                        }
                        if (this.form.Ichiran.Rows[i].Cells["SHARYOU_CD"].Value != null)
                        {
                            inputSharyouCd = this.form.Ichiran.Rows[i].Cells["SHARYOU_CD"].Value.ToString();
                        }
                        if (this.form.Ichiran.Rows[i].Cells["HOJOIN_CD"].Value != null)
                        {
                            inputHojoCd = this.form.Ichiran.Rows[i].Cells["HOJOIN_CD"].Value.ToString();
                        }
                        if (this.form.Ichiran.Rows[i].Cells["NIOROSHI_GYOUSHA_CD"].Value != null)
                        {
                            inputHanGyoushaCd = this.form.Ichiran.Rows[i].Cells["NIOROSHI_GYOUSHA_CD"].Value.ToString();
                        }
                        if (this.form.Ichiran.Rows[i].Cells["NIOROSHI_GENBA_CD"].Value != null)
                        {
                            inputHanGenCd = this.form.Ichiran.Rows[i].Cells["NIOROSHI_GENBA_CD"].Value.ToString();
                        }

                        //車輌休働チェック
                        if (!SharyouDateCheck(2, inputUnpanGyoushaCd, inputSharyouCd, inputSagyouDate))
                        {
                            S_holiday = "車輌休動設定、";
                        }
                        //運転者休働チェック
                        if (!UntenshaDateCheck(2, inputUntenshaCd, inputSagyouDate))
                        {
                            U_holiday = "運転者休動設定、";
                        }
                        //補助員休働チェック
                        if (!UntenshaDateCheck(2, inputHojoCd, inputSagyouDate))
                        {
                            H_holiday = "補助員休動設定、";
                        }

                        //搬入先休働チェック
                        if (!HannyuusakiDateCheck(inputHanGyoushaCd, inputHanGenCd, inputSagyouDate))
                        {
                            N_holiday = "荷降先休動設定、";
                        }

                        if ((!string.IsNullOrEmpty(S_holiday)) || (!string.IsNullOrEmpty(U_holiday)) || (!string.IsNullOrEmpty(H_holiday)) || (!string.IsNullOrEmpty(N_holiday)))
                        {
                            inputUnpanGyoushaCd = S_holiday + U_holiday + H_holiday + N_holiday;
                            Err_holiday = Err_holiday + this.form.Ichiran.Rows[i].Cells["No"].Value.ToString() + "行目：";
                            Err_holiday = Err_holiday + inputUnpanGyoushaCd.Substring(0,inputUnpanGyoushaCd.Length-1) + "\r\n";
                        }
                        #endregion 休働チェック

                        //受付伝票がロジコン連携されているか
                        if (this.RenkeiCheck(3, this.form.Ichiran.Rows[i].Cells["UKETSUKE_NUMBER"].Value.ToString()))
                        {
                            Err_Msg = Err_Msg + (i + 1) + "行目：受付番号 " + this.form.Ichiran.Rows[i].Cells["UKETSUKE_NUMBER"].Value.ToString() + "\r\n";
                        }

                    }
                }

                //対象データが存在するか
                if (!taisyou)
                {
                    this.MsgBox.MessageBoxShow("E050", "対象");
                    return;
                }

                //休働アラートを表示
                if (!string.IsNullOrEmpty(Err_holiday))
                {
                    Err_holiday = "休動設定が行われている車輌、運転者、荷降先に対する振替が\r\n行われています。\r\n確認してください。\r\n\r\n" + Err_holiday;
                    this.MsgBox.MessageBoxShowWarn(Err_holiday);
                    return;
                }

                //ロジコン連携を表示
                if (!string.IsNullOrEmpty(Err_Msg))
                {
                    Err_Msg = "ロジこんぱす連携されています。\r\n確認してください。\r\n\r\n" + Err_Msg;
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

                //実行チェックに引っかからなかったら、振替実行のメッセージを出す
                if (MessageBox.Show("対象データの振替登録を実行します。\r\nよろしいですか？","確認",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                bool isMobikeChecked = false;
                bool isTaishoChecked = false;

                using (Transaction tran = new Transaction())
                {
                    // トランザクション開始
                    for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                    {
                        DataGridViewRow dr = this.form.Ichiran.Rows[i];
                        // モバイル連携フラグ
                        isMobikeChecked = bool.Parse(dr.Cells["MOBILE_RENKEI"].Value.ToString());

                        isTaishoChecked = bool.Parse(dr.Cells["TAISHO_CHECK"].Value.ToString());

                        if (isTaishoChecked)
                        {
                            //１．受付データ更新
                            if (!UketsukeInfo(dr))
                            {
                                return;
                            }
                            if (isMobikeChecked)
                            {
                                //モバイル更新
                                if (!this.CreateMobileEntity(dr))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                //モバイル削除
                                if (!this.CreateMobileDelEntity(dr))
                                {
                                    return;
                                }
                            }
                        }
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

                // 受付番号を検索条件に設定する
                this.searchDto.UketsukeNumber = this.UketsukeNumber;
                // 定期配車入力情報を取得する
                this.searchResultEntry = UketsukeSSdao.GetAllUketsukeData(this.searchDto);
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

                        row.Cells["No"].Value = i + 1;                      //No
                        row.Cells["TAISHO_CHECK"].Value = false;                    //対象
                        row.Cells["MOBILE_RENKEI"].Value = false;                   //モバイル連携

                        row.Cells["DENSHU_KBN"].Value = dr["DENSHU_KBN"];           //伝種区分(収集or出荷)
                        row.Cells["UKETSUKE_NUMBER"].Value = dr["UKETSUKE_NUMBER"];     //受付番号
                        row.Cells["KYOTEN_CD"].Value = dr["KYOTEN_CD"];             //拠点CD
                        row.Cells["KYOTEN_NAME"].Value = dr["KYOTEN_NAME"];         //拠点名
                        row.Cells["SAGYOU_DATE"].Value = dr["SAGYOU_DATE"];         //作業日
                        row.Cells["GYOUSHA_CD"].Value = dr["GYOUSHA_CD"];           //業者CD
                        row.Cells["GYOUSHA_NAME"].Value = dr["GYOUSHA_NAME"];       //業者名
                        row.Cells["GENBA_CD"].Value = dr["GENBA_CD"];               //現場CD
                        row.Cells["GENBA_NAME"].Value = dr["GENBA_NAME"];           //現場名
                        row.Cells["UNPAN_GYOUSHA_CD"].Value = dr["UNPAN_GYOUSHA_CD"];       //運搬業者CD
                        row.Cells["UNPAN_GYOUSHA_NAME"].Value = dr["UNPAN_GYOUSHA_NAME"];   //運搬業者名
                        row.Cells["SHARYOU_CD"].Value = dr["SHARYOU_CD"];           //車輌CD
                        row.Cells["SHARYOU_NAME"].Value = dr["SHARYOU_NAME"];       //車輌名
                        row.Cells["SHASHU_CD"].Value = dr["SHASHU_CD"];             //車種CD
                        row.Cells["SHASHU_NAME"].Value = dr["SHASHU_NAME"];         //車種名
                        row.Cells["UNTENSHA_CD"].Value = dr["UNTENSHA_CD"];         //運転者CD
                        row.Cells["UNTENSHA_NAME"].Value = dr["UNTENSHA_NAME"];     //運転者名
                        row.Cells["SYSTEM_ID_HIDDEN"].Value = dr["SYSTEM_ID_HIDDEN"];       //SYSTEM_ID
                        row.Cells["SEQ_HIDDEN"].Value = dr["SEQ_HIDDEN"];                   //SEQ
                        row.Cells["SHOKUCHI_KBN_HIDDEN"].Value = dr["SHOKUCHI_KBN_HIDDEN"];
                        row.Cells["TORIHIKISAKI_CD"].Value = dr["TORIHIKISAKI_CD"];
                        row.Cells["HOJOIN_CD"].Value = dr["HOJOIN_CD"];
                        row.Cells["NIOROSHI_GYOUSHA_CD"].Value = dr["NIOROSHI_GYOUSHA_CD"];
                        row.Cells["NIOROSHI_GENBA_CD"].Value = dr["NIOROSHI_GENBA_CD"];
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
                        case "No":
                        case "UKETSUKE_NUMBER":
                            // 数値型ならセル値を右寄せにする
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "SAGYOU_DATE":
                            // 日付型は表示幅を「yyyy/mm/dd」が表示出来るよう固定長で表示を行う
                            column.Width = 107;
                            break;
                    }
                }

                //運搬業者のreadonlyを設定する
                for (int i = 0; i < searchResultEntry.Rows.Count; i++)
                {
                    if ((bool)this.form.Ichiran.Rows[i].Cells["SHOKUCHI_KBN_HIDDEN"].Value)
                    {
                        this.form.Ichiran.Rows[i].Cells["UNPAN_GYOUSHA_NAME"].ReadOnly = false;
                    }
                    else
                    {
                        this.form.Ichiran.Rows[i].Cells["UNPAN_GYOUSHA_NAME"].ReadOnly = true;
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

        ///////////////////////////
        //受付情報修正
        ///////////////////////////
        #region 受付収集検索
        /// <summary>
        /// 受付収集検索
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="seq"></param>
        private T_UKETSUKE_SS_ENTRY getUketsukeSSEntryData(int systemID)
        {
            T_UKETSUKE_SS_ENTRY returnValue = null;
            try
            {
                LogUtility.DebugMethodStart(systemID);

                T_UKETSUKE_SS_ENTRY uketsukeSSEntryParm = new T_UKETSUKE_SS_ENTRY();
                uketsukeSSEntryParm.SYSTEM_ID = systemID;

                T_UKETSUKE_SS_ENTRY[] uketsukeSSEntry = UketsukeSSdao.GetDataForEntity(uketsukeSSEntryParm);

                if (uketsukeSSEntry != null)
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
        private T_UKETSUKE_SK_ENTRY getUketsukeSKEntryData(int systemID)
        {
            T_UKETSUKE_SK_ENTRY returnValue = null;
            try
            {
                LogUtility.DebugMethodStart(systemID);

                T_UKETSUKE_SK_ENTRY uketsukeSKEntryParm = new T_UKETSUKE_SK_ENTRY();
                uketsukeSKEntryParm.SYSTEM_ID = systemID;

                T_UKETSUKE_SK_ENTRY[] uketsukeSKEntry = UketsukeSKdao.GetDataForEntity(uketsukeSKEntryParm);

                if (uketsukeSKEntry != null)
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

        #region 受付情報修正
        /// <summary>
        /// 受付情報修正
        /// </summary>
        /// <param name="dr"></param>
        private bool UketsukeInfo(DataGridViewRow dr)
        {
            LogUtility.DebugMethodStart(dr);
            bool result = false;
            try
            {
                // エンティティ区分
                string entityKbn = dr.Cells["DENSHU_KBN"].Value.ToString();
                // システムID
                int systemID = int.Parse(dr.Cells["SYSTEM_ID_HIDDEN"].Value.ToString());
                // 枝番
                int seq = int.Parse(dr.Cells["SEQ_HIDDEN"].Value.ToString());

                switch (entityKbn)
                {
                    // 受付（収集）の場合
                    case "収集":
                        #region 収集
                        // システムIDと枝番より、データ取得
                        T_UKETSUKE_SS_ENTRY uketsukeSSEntry = getUketsukeSSEntryData(systemID);

                        if (uketsukeSSEntry != null)
                        {
                            seq = (int)uketsukeSSEntry.SEQ;
                            // 3-1-1.受付（収集）入力テーブルの更新（論理削除）
                            var WHOSSEntry = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(uketsukeSSEntry);
                            // システム自動設定のプロパティを設定する
                            // 更新日
                            uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            uketsukeSSEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSSEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 削除フラグを設定
                            uketsukeSSEntry.DELETE_FLG = true;
                            // データ更新
                            this.UketsukeSSdao.Update(uketsukeSSEntry);

                            // 3-1-2.受付（収集）入力テーブルの追加
                            // 枝番+1
                            uketsukeSSEntry.SEQ = seq + 1;
                            //拠点CD
                            uketsukeSSEntry.KYOTEN_CD = Int16.Parse(dr.Cells["KYOTEN_CD"].Value.ToString());
                            //作業日
                            if (dr.Cells["SAGYOU_DATE"].Value == null)
                            {
                                uketsukeSSEntry.SAGYOU_DATE = null;
                            }
                            else
                            {
                                uketsukeSSEntry.SAGYOU_DATE = ((DateTime)dr.Cells["SAGYOU_DATE"].Value).ToShortDateString();
                            }
                            //運搬業者CD
                            if (dr.Cells["UNPAN_GYOUSHA_CD"].Value == null)
                            {
                                uketsukeSSEntry.UNPAN_GYOUSHA_CD = string.Empty;
                            }
                            else
                            {
                                uketsukeSSEntry.UNPAN_GYOUSHA_CD = dr.Cells["UNPAN_GYOUSHA_CD"].Value.ToString();
                            }
                            //運搬業者名
                            if (dr.Cells["UNPAN_GYOUSHA_NAME"].Value == null)
                            {
                                uketsukeSSEntry.UNPAN_GYOUSHA_NAME = string.Empty;
                            }
                            else
                            {
                                uketsukeSSEntry.UNPAN_GYOUSHA_NAME = dr.Cells["UNPAN_GYOUSHA_NAME"].Value.ToString();
                            }
                            // 車種CD
                            if (dr.Cells["SHASHU_CD"].Value == null)
                            {
                                uketsukeSSEntry.SHASHU_CD = string.Empty;
                            }
                            else
                            {
                                uketsukeSSEntry.SHASHU_CD = dr.Cells["SHASHU_CD"].Value.ToString();
                            }
                            // 車種名
                            if (dr.Cells["SHASHU_NAME"].Value == null)
                            {
                                uketsukeSSEntry.SHASHU_NAME = string.Empty;
                            }
                            else
                            {
                                uketsukeSSEntry.SHASHU_NAME = dr.Cells["SHASHU_NAME"].Value.ToString();
                            }
                            //車輌CD
                            if (dr.Cells["SHARYOU_CD"].Value == null)
                            {
                                uketsukeSSEntry.SHARYOU_CD = string.Empty;
                            }
                            else
                            {
                                uketsukeSSEntry.SHARYOU_CD = dr.Cells["SHARYOU_CD"].Value.ToString();
                            }
                            // 車輌名
                            if (dr.Cells["SHARYOU_NAME"].Value == null)
                            {
                                uketsukeSSEntry.SHARYOU_NAME = string.Empty;
                            }
                            else
                            {
                                uketsukeSSEntry.SHARYOU_NAME = dr.Cells["SHARYOU_NAME"].Value.ToString();
                            }
                            // 運転者CD
                            if (dr.Cells["UNTENSHA_CD"].Value == null)
                            {
                                uketsukeSSEntry.UNTENSHA_CD = string.Empty;
                            }
                            else
                            {
                                uketsukeSSEntry.UNTENSHA_CD = dr.Cells["UNTENSHA_CD"].Value.ToString();
                            }
                            // 運転者名
                            if (dr.Cells["UNTENSHA_NAME"].Value == null)
                            {
                                uketsukeSSEntry.UNTENSHA_NAME = string.Empty;
                            }
                            else
                            {
                                uketsukeSSEntry.UNTENSHA_NAME = dr.Cells["UNTENSHA_NAME"].Value.ToString();
                            }

                            // 配車状況
                            // 車輌もしくは運転者が空の場合、配車状況を「受注」とする。
                            if ((dr.Cells["SHARYOU_CD"].Value == null || String.IsNullOrEmpty(dr.Cells["SHARYOU_CD"].Value.ToString()))
                                || (dr.Cells["UNTENSHA_CD"].Value == null || String.IsNullOrEmpty(dr.Cells["UNTENSHA_CD"].Value.ToString())))
                            {
                                uketsukeSSEntry.HAISHA_JOKYO_CD = 1;
                                uketsukeSSEntry.HAISHA_JOKYO_NAME = "受注";
                            }

                            // 削除フラグ
                            uketsukeSSEntry.DELETE_FLG = false;
                            // 更新日
                            uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            uketsukeSSEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSSEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 受付（収集）入力テーブルに追加
                            this.UketsukeSSdao.Insert(uketsukeSSEntry);

                            // 3-1-3.受付（収集）明細テーブルの追加（コピーイメージ）
                            T_UKETSUKE_SS_DETAIL uketsukeSSDetailParam = new T_UKETSUKE_SS_DETAIL();
                            // システムID
                            uketsukeSSDetailParam.SYSTEM_ID = systemID;
                            // 枝番
                            uketsukeSSDetailParam.SEQ = seq;

                            // 受付（収集）明細データ取得
                            T_UKETSUKE_SS_DETAIL[] uketsukeSSDetailList = UketsukeSSDetaildao.GetDataForEntity(uketsukeSSDetailParam);

                            if (uketsukeSSDetailList != null && uketsukeSSDetailList.Length > 0)
                            {
                                foreach (var ssDetail in uketsukeSSDetailList)
                                {
                                    // システムID
                                    ssDetail.SYSTEM_ID = uketsukeSSEntry.SYSTEM_ID;
                                    // 枝番
                                    ssDetail.SEQ = uketsukeSSEntry.SEQ;
                                    // システム自動設定のプロパティを設定する
                                    // 更新日
                                    ssDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 更新者
                                    ssDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    ssDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // 受付（収集）明細テーブルに追加
                                    this.UketsukeSSDetaildao.Insert(ssDetail);
                                }
                            }

                            // 3-1-4.コンテナ稼動予定テーブルの追加（コピーイメージ）
                            T_CONTENA_RESERVE contenaReserveParam = new T_CONTENA_RESERVE();
                            // システムID
                            contenaReserveParam.SYSTEM_ID = systemID;
                            // 枝番
                            contenaReserveParam.SEQ = seq;

                            T_CONTENA_RESERVE[] contenaReserveList = Contenadao.GetDataForEntity(contenaReserveParam);

                            if (contenaReserveList != null && contenaReserveList.Length > 0)
                            {

                                foreach (var contenareserveDetail in contenaReserveList)
                                {
                                    // 削除フラグ
                                    contenareserveDetail.DELETE_FLG = true;
                                    // 更新日
                                    contenareserveDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 更新者
                                    contenareserveDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    contenareserveDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // コンテナ稼動予定テーブルに追加
                                    this.Contenadao.Update(contenareserveDetail);
                                }

                                foreach (var contenareserveDetail in contenaReserveList)
                                {
                                    // システムID
                                    contenareserveDetail.SYSTEM_ID = uketsukeSSEntry.SYSTEM_ID;
                                    // 枝番
                                    contenareserveDetail.SEQ = uketsukeSSEntry.SEQ;
                                    // 削除フラグ
                                    contenareserveDetail.DELETE_FLG = false;
                                    // 更新日
                                    contenareserveDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 更新者
                                    contenareserveDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    contenareserveDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // コンテナ稼動予定テーブルに追加
                                    this.Contenadao.Insert(contenareserveDetail);
                                }
                            }
                        }
                        break;
                        #endregion
                    // 受付（出荷）の場合
                    case "出荷":
                        #region 出荷
                        // システムIDと枝番より、データ取得
                        T_UKETSUKE_SK_ENTRY uketsukeSKEntry = getUketsukeSKEntryData(systemID);

                        if (uketsukeSKEntry != null)
                        {
                            seq = (int)uketsukeSKEntry.SEQ;
                            // 3-2-1.受付（出荷）入力テーブルの更新（論理削除）
                            var WHOSKEntry = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(uketsukeSKEntry);
                            // 更新日
                            uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            uketsukeSKEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSKEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 削除フラグを設定
                            uketsukeSKEntry.DELETE_FLG = true;
                            // データ更新
                            this.UketsukeSKdao.Update(uketsukeSKEntry);

                            // 3-2-2.受付（出荷）入力テーブルの追加
                            // 枝番+1
                            uketsukeSKEntry.SEQ = seq + 1;
                            //拠点CD
                            uketsukeSKEntry.KYOTEN_CD = Int16.Parse(dr.Cells["KYOTEN_CD"].Value.ToString());
                            //作業日
                            if (dr.Cells["SAGYOU_DATE"].Value == null)
                            {
                                uketsukeSKEntry.SAGYOU_DATE = null;
                            }
                            else
                            {
                                uketsukeSKEntry.SAGYOU_DATE = ((DateTime)dr.Cells["SAGYOU_DATE"].Value).ToShortDateString();
                            }
                            //運搬業者CD
                            if (dr.Cells["UNPAN_GYOUSHA_CD"].Value == null)
                            {
                                uketsukeSKEntry.UNPAN_GYOUSHA_CD = string.Empty;
                            }
                            else
                            {
                                uketsukeSKEntry.UNPAN_GYOUSHA_CD = dr.Cells["UNPAN_GYOUSHA_CD"].Value.ToString();
                            }
                            //運搬業者名
                            if (dr.Cells["UNPAN_GYOUSHA_NAME"].Value == null)
                            {
                                uketsukeSKEntry.UNPAN_GYOUSHA_NAME = string.Empty;
                            }
                            else
                            {
                                uketsukeSKEntry.UNPAN_GYOUSHA_NAME = dr.Cells["UNPAN_GYOUSHA_NAME"].Value.ToString();
                            }
                            // 車種CD
                            if (dr.Cells["SHASHU_CD"].Value == null)
                            {
                                uketsukeSKEntry.SHASHU_CD = string.Empty;
                            }
                            else
                            {
                                uketsukeSKEntry.SHASHU_CD = dr.Cells["SHASHU_CD"].Value.ToString();
                            }
                            // 車種名
                            if (dr.Cells["SHASHU_NAME"].Value == null)
                            {
                                uketsukeSKEntry.SHASHU_NAME = string.Empty;
                            }
                            else
                            {
                                uketsukeSKEntry.SHASHU_NAME = dr.Cells["SHASHU_NAME"].Value.ToString();
                            }
                            //車輌CD
                            if (dr.Cells["SHARYOU_CD"].Value == null)
                            {
                                uketsukeSKEntry.SHARYOU_CD = string.Empty;
                            }
                            else
                            {
                                uketsukeSKEntry.SHARYOU_CD = dr.Cells["SHARYOU_CD"].Value.ToString();
                            }
                            // 車輌名
                            if (dr.Cells["SHARYOU_NAME"].Value == null)
                            {
                                uketsukeSKEntry.SHARYOU_NAME = string.Empty;
                            }
                            else
                            {
                                uketsukeSKEntry.SHARYOU_NAME = dr.Cells["SHARYOU_NAME"].Value.ToString();
                            }
                            // 運転者CD
                            if (dr.Cells["UNTENSHA_CD"].Value == null)
                            {
                                uketsukeSKEntry.UNTENSHA_CD = string.Empty;
                            }
                            else
                            {
                                uketsukeSKEntry.UNTENSHA_CD = dr.Cells["UNTENSHA_CD"].Value.ToString();
                            }
                            // 運転者名
                            if (dr.Cells["UNTENSHA_NAME"].Value == null)
                            {
                                uketsukeSKEntry.UNTENSHA_NAME = string.Empty;
                            }
                            else
                            {
                                uketsukeSKEntry.UNTENSHA_NAME = dr.Cells["UNTENSHA_NAME"].Value.ToString();
                            }

                            // 配車状況
                            // 車輌もしくは運転者が空の場合、配車状況を「受注」とする。
                            if ((dr.Cells["SHARYOU_CD"].Value == null || String.IsNullOrEmpty(dr.Cells["SHARYOU_CD"].Value.ToString()))
                                || (dr.Cells["UNTENSHA_CD"].Value == null || String.IsNullOrEmpty(dr.Cells["UNTENSHA_CD"].Value.ToString())))
                            {
                                uketsukeSKEntry.HAISHA_JOKYO_CD = 1;
                                uketsukeSKEntry.HAISHA_JOKYO_NAME = "受注";
                            }

                            // 削除フラグ
                            uketsukeSKEntry.DELETE_FLG = false;
                            // 更新日
                            uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 更新者
                            uketsukeSKEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSKEntry.UPDATE_PC = SystemInformation.ComputerName;

                            this.UketsukeSKdao.Insert(uketsukeSKEntry);

                            // 3-2-3.受付（出荷）明細テーブルの追加（コピーイメージ）
                            T_UKETSUKE_SK_DETAIL uketsukeSKDetailParam = new T_UKETSUKE_SK_DETAIL();
                            // システムID
                            uketsukeSKDetailParam.SYSTEM_ID = systemID;
                            // 枝番
                            uketsukeSKDetailParam.SEQ = seq;

                            // 受付（出荷）明細データ取得
                            T_UKETSUKE_SK_DETAIL[] uketsukeSKDetailList = UketsukeSKDetaildao.GetDataForEntity(uketsukeSKDetailParam);

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
                                    this.UketsukeSKDetaildao.Insert(skDetail);
                                }
                            }
                        }

                        break;
                        #endregion
                    default:
                        break;
                }

                result = true;

                return result;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("UketsukeInfo", ex1);
                this.MsgBox.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("UketsukeInfo", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UketsukeInfo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion

        //////////////////////////
        //モバイル連携データ修正
        //////////////////////////
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
                    string CheckUketsukeNumber = row.Cells["UKETSUKE_NUMBER"].Value.ToString();

                    //ロジコン連携
                    //のチェックは、[対象]チェックONの方で行っているので、二重でチェックは行わない

                    //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                    //[システム日付] != [作業日]の場合はチェックをつけない
                    if ((row.Cells["SAGYOU_DATE"].Value == null)
                        || (string.IsNullOrEmpty(row.Cells["SAGYOU_DATE"].Value.ToString())))
                    {
                        return false;
                    }
                    else
                    {
                        if (!(DateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd")).Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                        {
                            return false;
                        }
                    }

                    //変更するデータが入力されているか
                    //UNTENSHA_CD、SHARYOU_CD、SHASHU_CD、取引先CDのしずれかが無し→×。
                    if ((row.Cells["UNTENSHA_CD"].Value == null)
                        || (row.Cells["SHARYOU_CD"].Value == null)
                        || (row.Cells["SHASHU_CD"].Value == null)
                        || (row.Cells["SHASHU_NAME"].Value == null)
                        || (row.Cells["TORIHIKISAKI_CD"].Value == null))
                    {
                        return false;
                    }

                    if ((string.IsNullOrEmpty(row.Cells["UNTENSHA_CD"].Value.ToString()))
                        || (string.IsNullOrEmpty(row.Cells["SHARYOU_CD"].Value.ToString()))
                        || (string.IsNullOrEmpty(row.Cells["SHASHU_CD"].Value.ToString()))
                        || (string.IsNullOrEmpty(row.Cells["SHASHU_NAME"].Value.ToString()))
                        || (string.IsNullOrEmpty(row.Cells["TORIHIKISAKI_CD"].Value.ToString())))
                    {
                        return false;
                    }
                }
            }

            return true;

        }
        #endregion モバイル登録事前チェック

        #region モバイル連携データ削除
        /// <summary>
        /// 登録済みのモバイルデータを削除する
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public bool CreateMobileDelEntity(DataGridViewRow dr)
        {
            bool ret = true;
            try
            {
                // 受付番号
                int HAISHA_NO = int.Parse(dr.Cells["UKETSUKE_NUMBER"].Value.ToString());

                LogUtility.DebugMethodStart();
                this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                this.entitysMobisyoRtCTNList = new List<T_MOBISYO_RT_CONTENA>();
                this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();

                //T_MOBISYO_RT                
                var List = this.mTmobisyoRtDao.GetRtDataByCDList(HAISHA_NO, 1);
                foreach (T_MOBISYO_RT count in List)
                {
                    count.DELETE_FLG = true;
                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT>(count);
                    dataBinderContenaResult.SetSystemProperty(count, false);
                    this.entitysMobisyoRtList.Add(count);
                    this.TmobisyoRtDao.Update(count);
                }

                //T_MOBISYO_RT_CONTENA
                foreach (T_MOBISYO_RT count in this.entitysMobisyoRtList)
                {
                    SqlInt64 SEQ_NO = count.SEQ_NO;
                    var ListDetail = this.mTmobisyoRtCTNDao.GetRtCTNDataByCD(SEQ_NO);
                    if (ListDetail != null)
                    {
                        foreach (T_MOBISYO_RT_CONTENA countDetail in ListDetail)
                        {
                            countDetail.DELETE_FLG = true;
                            // 自動設定
                            var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT_CONTENA>(countDetail);
                            dataBinderContenaResult.SetSystemProperty(countDetail, false);
                            //this.entitysMobisyoRtCTNList.Add(countDetail);
                            this.TmobisyoCTNDao.Update(countDetail);
                        }
                    }
                }

                // T_MOBISYO_RT_DTL
                foreach (T_MOBISYO_RT count in this.entitysMobisyoRtList)
                {
                    SqlInt64 SEQ_NO = count.SEQ_NO;
                    var ListDetail = this.mTmobisyoRtDTLDao.GetDtlDataByCD(SEQ_NO);
                    foreach (T_MOBISYO_RT_DTL countDetail in ListDetail)
                    {
                        countDetail.DELETE_FLG = true;
                        // 自動設定
                        var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT_DTL>(countDetail);
                        dataBinderContenaResult.SetSystemProperty(countDetail, false);
                        this.entitysMobisyoRtDTLList.Add(countDetail);
                        this.TmobisyoRtDTLDao.Update(countDetail);
                    }
                }

                // T_MOBISYO_RT_HANNYUU
                foreach (T_MOBISYO_RT_DTL count in this.entitysMobisyoRtDTLList)
                {
                    SqlInt64 HANYU_SEQ_NO = count.HANYU_SEQ_NO;
                    var ListHannyuu = this.mTmobisyoRtHNDao.GetHannyuuDataByCD(HANYU_SEQ_NO);
                    foreach (T_MOBISYO_RT_HANNYUU countHannyuu in ListHannyuu)
                    {
                        countHannyuu.DELETE_FLG = true;
                        // 自動設定
                        var dataBinderContenaResulthn = new DataBinderLogic<T_MOBISYO_RT_HANNYUU>(countHannyuu);
                        dataBinderContenaResulthn.SetSystemProperty(countHannyuu, false);
                        //this.entitysMobisyoRtHNList.Add(countHannyuu);
                        this.TmobisyoRtHNDao.Update(countHannyuu);
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("CreateMobileDelEntity", ex1);
                this.MsgBox.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CreateMobileDelEntity", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMobileDelEntity", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region モバイル連携データ修正
        /// <summary>
        /// 登録済みのモバイルデータを更新する。
        /// （T_MOBISYO_RTのみ）
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public bool CreateMobileEntity(DataGridViewRow dr)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();

                // 受付番号
                int HAISHA_NO = int.Parse(dr.Cells["UKETSUKE_NUMBER"].Value.ToString());

                // 修正するデータ取得
                T_MOBISYO_RT mobileEntry = mTmobisyoRtDao.GetRtDataByCD(HAISHA_NO, 1);
                if (mobileEntry != null)
                {
                    // 車種CD
                    if (!string.IsNullOrEmpty(dr.Cells["SHASHU_CD"].Value.ToString()))
                    {
                        mobileEntry.SHASHU_CD = dr.Cells["SHASHU_CD"].Value.ToString();
                    }
                    // 車種名
                    if (!string.IsNullOrEmpty(dr.Cells["SHASHU_NAME"].Value.ToString()))
                    {
                        mobileEntry.SHASHU_NAME = dr.Cells["SHASHU_NAME"].Value.ToString();
                    }
                    // 車輌CD
                    if (!string.IsNullOrEmpty(dr.Cells["SHARYOU_CD"].Value.ToString()))
                    {
                        mobileEntry.SHARYOU_CD = dr.Cells["SHARYOU_CD"].Value.ToString();
                    }
                    // 車輌名
                    if (!string.IsNullOrEmpty(dr.Cells["SHARYOU_NAME"].Value.ToString()))
                    {
                        mobileEntry.SHARYOU_NAME = dr.Cells["SHARYOU_NAME"].Value.ToString();
                    }
                    // 運転者名
                    if (!string.IsNullOrEmpty(dr.Cells["UNTENSHA_NAME"].Value.ToString()))
                    {
                        mobileEntry.UNTENSHA_NAME = dr.Cells["UNTENSHA_NAME"].Value.ToString();
                    }
                    // 運転者名CD
                    if (!string.IsNullOrEmpty(dr.Cells["UNTENSHA_CD"].Value.ToString()))
                    {
                        mobileEntry.UNTENSHA_CD = dr.Cells["UNTENSHA_CD"].Value.ToString();
                    }
                    // (配車)作業日
                    if (!SqlDateTime.Parse(dr.Cells["SAGYOU_DATE"].Value.ToString()).IsNull)
                    {
                        mobileEntry.HAISHA_SAGYOU_DATE = SqlDateTime.Parse(dr.Cells["SAGYOU_DATE"].Value.ToString());
                    }
                    // 運搬業者CD
                    if (!string.IsNullOrEmpty(dr.Cells["UNPAN_GYOUSHA_CD"].Value.ToString()))
                    {
                        mobileEntry.GENBA_JISSEKI_UPNGYOSHACD = dr.Cells["UNPAN_GYOUSHA_CD"].Value.ToString();
                    }
                    else
                    {
                        mobileEntry.GENBA_JISSEKI_UPNGYOSHACD = string.Empty;
                    }
                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT>(mobileEntry);
                    dataBinderContenaResult.SetSystemProperty(mobileEntry, false);
                    // Listに追加
                    //this.entitysMobisyoRtList.Add(mobileEntry);
                    this.TmobisyoRtDao.Update(mobileEntry);
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("CreateMobileEntity", ex1);
                this.MsgBox.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CreateMobileEntity", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMobileEntity", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        //////////////////////////
        //各種項目
        //////////////////////////
        #region 車輌休動チェック
        internal bool SharyouDateCheck(int KBN, string UnpanCD, string SharyoCD, string SagyoHi)
        {
            try
            {
                string inputUnpanGyoushaCd = UnpanCD;
                string inputSharyouCd = SharyoCD;
                string inputSagyouDate = SagyoHi;

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
                //運搬業者CD
                workclosedsharyouEntry.GYOUSHA_CD = inputUnpanGyoushaCd;
                //車輌CD取得
                workclosedsharyouEntry.SHARYOU_CD = inputSharyouCd;
                //作業日取得
                workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

                //取得テータ
                if (workclosedsharyouList.Count() >= 1)
                {
                    if (KBN <= 1)
                    {
                        MsgBox.MessageBoxShow("E206", "車輌", "作業日：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    }
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SharyouDateCheck", ex1);
                this.MsgBox.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharyouDateCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 運転者(補助員)休動チェック
        /// <summary>
        /// KBN 0:ヘッダの一括項目、1:明細項目
        /// UntenCD:運転者CD
        /// SagyoHi:作業日
        /// 明細の方は、呼び出し元でアラート出す？
        /// </summary>
        /// <param name="KBN"></param>
        /// <param name="UntenCD"></param>
        /// <param name="SagyoHi"></param>
        /// <returns></returns>
        internal bool UntenshaDateCheck(int KBN,string UntenCD, string SagyoHi)
        {
            try
            {
                string inputUntenshaCd = UntenCD;
                string inputSagyouDate = SagyoHi;

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
                //運転者CD取得
                workcloseduntenshaEntry.SHAIN_CD = inputUntenshaCd;
                //作業日取得
                workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

                //取得テータ
                if (workcloseduntenshaList.Count() >= 1)
                {
                    if (KBN <= 1)
                    {
                        MsgBox.MessageBoxShow("E206", "運転者", "作業日：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    }
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UntenshaDateCheck", ex1);
                this.MsgBox.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UntenshaDateCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 搬入先休動チェック
        internal bool HannyuusakiDateCheck(string NGYOUCD,string NGENCD, string SagyoHi)
        {
            try
            {
                string inputNioroshiGyoushaCd = NGYOUCD;
                string inputNioroshiGenbaCd = NGENCD;
                string inputSagyouDate = SagyoHi;

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_HANNYUUSAKI workclosedhannyuusakiEntry = new M_WORK_CLOSED_HANNYUUSAKI();
                //荷降業者CD取得
                workclosedhannyuusakiEntry.GYOUSHA_CD = inputNioroshiGyoushaCd;
                //荷降現場CD取得
                workclosedhannyuusakiEntry.GENBA_CD = inputNioroshiGenbaCd;
                //作業日取得
                workclosedhannyuusakiEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = workclosedhannyuusakiDao.GetAllValidData(workclosedhannyuusakiEntry);

                //取得テータ
                if (workclosedhannyuusakiList.Count() >= 1)
                {
                    return false;
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

        //////////////////////////////
        #region 拠点CDバリデート
        /// <summary>
        /// 拠点CDバリデート
        /// </summary>
        public bool ChechiKyotenCd(DataGridViewRow row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                String kyotenCd = row.Cells["KYOTEN_CD"].FormattedValue.ToString();
                if (string.IsNullOrEmpty(kyotenCd))
                {
                    ret = true;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_KYOTEN keyEntity = new M_KYOTEN();
                keyEntity.KYOTEN_CD = Int16.Parse(kyotenCd);
                var kyoten = this.kyotenDao.GetAllValidData(keyEntity);
                if (kyoten == null || kyoten.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "拠点");
                    this.isInputError = true;
                    row.Cells["KYOTEN_NAME"].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (this.form.Ichiran.CurrentRow.Index >= 0)
                {
                    if ((kyoten[0].KYOTEN_CD != 99) && (!kyoten[0].DELETE_FLG))
                    {
                        row.Cells["KYOTEN_NAME"].Value = kyoten[0].KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "拠点");
                        this.isInputError = true;
                        row.Cells["KYOTEN_NAME"].Value = string.Empty;
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }

                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechiKyotenCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 運搬受託者CDバリデート
        /// <summary>
        /// 車輌CDバリデート
        /// </summary>
        public bool CheckunpanCd(DataGridViewRow row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                row.Cells["UNPAN_GYOUSHA_NAME"].ReadOnly = true;

                String gyoushaCd = row.Cells["UNPAN_GYOUSHA_CD"].FormattedValue.ToString();
                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    row.Cells["SHARYOU_CD"].Value = string.Empty;
                    row.Cells["SHARYOU_NAME"].Value = string.Empty;
                    row.Cells["UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                    ret = true;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);
                if (gyousha == null || gyousha.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運搬業者");
                    this.isInputError = true;
                    row.Cells["UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                    row.Cells["UNPAN_GYOUSHA_CD"].Style.BackColor = Constans.ERROR_COLOR;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (this.form.Ichiran.CurrentRow.Index >= 0)
                {
                    if (!gyousha[0].DELETE_FLG)
                    {
                        if (!gyousha[0].UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        {
                            // 背景色変更
                            var msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E062", "運搬業者");

                            this.isInputError = true;
                            row.Cells["UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                            row.Cells["UNPAN_GYOUSHA_CD"].Style.BackColor = Constans.ERROR_COLOR;
                            ret = false;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;  
                        }

                        if ((row.Cells["DENSHU_KBN"].Value.ToString().Equals("収集") && gyousha[0].GYOUSHAKBN_UKEIRE)
                            || (row.Cells["DENSHU_KBN"].Value.ToString().Equals("出荷") && gyousha[0].GYOUSHAKBN_SHUKKA))
                        {

                            row.Cells["SHARYOU_CD"].Value = string.Empty;
                            row.Cells["SHARYOU_NAME"].Value = string.Empty;
                            row.Cells["UNPAN_GYOUSHA_NAME"].Value = gyousha[0].GYOUSHA_NAME_RYAKU;
                            if (gyousha[0].SHOKUCHI_KBN)
                            {
                                row.Cells["UNPAN_GYOUSHA_NAME"].ReadOnly = false;
                            }
                            else
                            {
                                row.Cells["UNPAN_GYOUSHA_NAME"].ReadOnly = true;
                            }
                        }
                        else
                        {
                          //受入出荷がずれてるとき
                            var msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShowError("運搬業者の業者区分が正しくありません");
                            this.isInputError = true;
                            row.Cells["UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                            row.Cells["UNPAN_GYOUSHA_CD"].Style.BackColor = Constans.ERROR_COLOR;
                            ret = false;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;  
                        }
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "運搬業者");
                        this.isInputError = true;
                        row.Cells["UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                        row.Cells["UNPAN_GYOUSHA_CD"].Style.BackColor = Constans.ERROR_COLOR;
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechkunpanCd", ex); 
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 車輌CDバリデート
        /// <summary>
        /// 車輌CDバリデート
        /// </summary>
        public bool ChecksharyouCd(DataGridViewRow row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                String sharyoCd = row.Cells["SHARYOU_CD"].FormattedValue.ToString();
                if (string.IsNullOrEmpty(sharyoCd))
                {
                    row.Cells["SHARYOU_NAME"].Value = string.Empty;
                    ret = true;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                // 一旦クリア
                row.Cells["SHARYOU_NAME"].Value = string.Empty;
                string inputSagyouDate = string.Empty;
                //作業日
                if (row.Cells["SAGYOU_DATE"].Value != null)
                {
                    inputSagyouDate = row.Cells["SAGYOU_DATE"].Value.ToString();
                }

                // 車輌CD取得
                if (false == string.IsNullOrEmpty(sharyoCd))
                {
                    var findEntity = new M_SHARYOU();

                    // 運搬業者CD取得
                    if (null != row.Cells["UNPAN_GYOUSHA_CD"].Value)
                    {
                        var unpanGyoushaCD = row.Cells["UNPAN_GYOUSHA_CD"].Value.ToString();
                        if (false == string.IsNullOrEmpty(unpanGyoushaCD))
                        {
                            // 運搬業者CDセット
                            findEntity.GYOUSHA_CD = unpanGyoushaCD;
                        }
                    }

                    // 車種CD取得
                    if (null != row.Cells["SHASHU_CD"].Value)
                    {
                        var shashuCD = row.Cells["SHASHU_CD"].Value.ToString();
                        if (false == string.IsNullOrEmpty(shashuCD))
                        {
                            // 車種CDセット
                            findEntity.SHASYU_CD = shashuCD;
                        }
                    }

                    // 車輌情報取得
                    findEntity.SHARYOU_CD = sharyoCd;
                    SqlDateTime sagyouDate = SqlDateTime.Null;
                    if (row.Cells["SAGYOU_DATE"].Value != null && !string.IsNullOrEmpty(row.Cells["SAGYOU_DATE"].Value.ToString()))
                    {
                        sagyouDate = SqlDateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString());
                    }
                    var SharyouNameSearchResult = this.cussharyouDao.GetSharyouNameData(findEntity, sagyouDate);

                    if (SharyouNameSearchResult.Rows.Count == 0)
                    {
                        // 該当CDがない時
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "車輌");
                        this.isInputError = true;
                        row.Cells["SHARYOU_NAME"].Value = string.Empty;
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    else
                    {
                        if (SharyouNameSearchResult.Rows.Count == 1)
                        {
                            // 休動チェック
                            if (!SharyouDateCheck(1, SharyouNameSearchResult.Rows[0]["UNPAN_GYOUSHA_CD"].ToString(), SharyouNameSearchResult.Rows[0]["SHARYOU_CD"].ToString(), inputSagyouDate))
                            {
                                this.isInputError = true;
                                ret = false;
                            }
                            else
                            {
                                row.Cells["UNPAN_GYOUSHA_CD"].Value = SharyouNameSearchResult.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                                row.Cells["UNPAN_GYOUSHA_NAME"].Value = SharyouNameSearchResult.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString();
                                row.Cells["SHARYOU_NAME"].Value = SharyouNameSearchResult.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                                row.Cells["SHASHU_CD"].Value = SharyouNameSearchResult.Rows[0]["SHASYU_CD"].ToString();
                                row.Cells["SHASHU_NAME"].Value = SharyouNameSearchResult.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                                row.Cells["UNTENSHA_CD"].Value = SharyouNameSearchResult.Rows[0]["SHAIN_CD"].ToString();
                                row.Cells["UNTENSHA_NAME"].Value = SharyouNameSearchResult.Rows[0]["SHAIN_NAME_RYAKU"].ToString();
                                ret = true;
                            }
                        }
                        else
                        {
                            // ヒット数が複数件の場合、ポップアップ表示
                            ret = false;
                            SendKeys.Send(" ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechisharyouCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 車種CDバリデート
        /// <summary>
        /// 車種CDバリデート
        /// </summary>
        public bool CheckshashuCd(DataGridViewRow row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                String shashuCd = row.Cells["SHASHU_CD"].FormattedValue.ToString();
                if (string.IsNullOrEmpty(shashuCd))
                {
                    ret = true;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_SHASHU keyEntity = new M_SHASHU();
                keyEntity.SHASHU_CD = shashuCd;
                var shashu = this.shashuDao.GetAllValidData(keyEntity);
                if (shashu == null || shashu.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "車種");
                    this.isInputError = true;
                    row.Cells["SHASHU_NAME"].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (this.form.Ichiran.CurrentRow.Index >= 0)
                {
                    if (!shashu[0].DELETE_FLG)
                    {

                        String sharyouCd = row.Cells["SHARYOU_CD"].FormattedValue.ToString();
                        if (!string.IsNullOrEmpty(sharyouCd))
                        {
                            var findEntity = new M_SHARYOU();
                            findEntity.SHARYOU_CD = row.Cells["SHARYOU_CD"].Value.ToString();
                            var SharyouNameSearchResult = this.cussharyouDao.GetSharyouNameData(findEntity, SqlDateTime.Null);

                            if (SharyouNameSearchResult.Rows.Count > 0)
                            {
                                // 車種一致チェックを行う
                                if (!String.IsNullOrEmpty(SharyouNameSearchResult.Rows[0]["SHASYU_CD"].ToString()) && row.Cells["SHASHU_CD"].Value.ToString() != SharyouNameSearchResult.Rows[0]["SHASYU_CD"].ToString())
                                {
                                    // 背景色変更
                                    //this.form.SHASHU_CD.IsInputErrorOccured = true;
                                    // メッセージ表示
                                    var msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E104", "車輌CD", "車種");
                                    this.isInputError = true;
                                    row.Cells["SHASHU_NAME"].Value = string.Empty;
                                    ret = false;
                                    LogUtility.DebugMethodEnd(ret);
                                    return ret;
                                }
                                else
                                {
                                    row.Cells["SHASHU_NAME"].Value = shashu[0].SHASHU_NAME_RYAKU;
                                }
                            }
                        }

                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "車種");
                        this.isInputError = true;
                        row.Cells["SHASHU_NAME"].Value = string.Empty;
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }

                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechishashuCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 運転者CDバリデート
        /// <summary>
        /// 運転者CDバリデート
        /// </summary>
        public bool CheckUntenCd(DataGridViewRow row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                String untenshaCd = row.Cells["UNTENSHA_CD"].FormattedValue.ToString();
                if (string.IsNullOrEmpty(untenshaCd))
                {
                    ret = true;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_SHAIN keyEntity = new M_SHAIN();
                keyEntity.SHAIN_CD = untenshaCd;
                var untensha = this.shainDao.GetAllValidData(keyEntity);
                if (untensha == null || untensha.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");
                    this.isInputError = true;
                    row.Cells["UNTENSHA_NAME"].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                string inputSagyouDate = string.Empty;
                string inputUntenshaCd = string.Empty;
                if (!string.IsNullOrEmpty(row.Cells["SAGYOU_DATE"].Value.ToString()))
                {
                    inputSagyouDate = row.Cells["SAGYOU_DATE"].Value.ToString();      //作業日
                }
                if (!string.IsNullOrEmpty(row.Cells["UNTENSHA_CD"].Value.ToString()))
                {
                    inputUntenshaCd = row.Cells["UNTENSHA_CD"].Value.ToString();     //運転者
                }
 
                if (!UntenshaDateCheck(1, inputUntenshaCd, inputSagyouDate))
                {
                    this.isInputError = true;
                    //row.Cells["UNTENSHA_NAME"].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (this.form.Ichiran.CurrentRow.Index >= 0)
                {
                    if ((untensha[0].UNTEN_KBN) && (!untensha[0].DELETE_FLG))
                    {
                        row.Cells["UNTENSHA_NAME"].Value = untensha[0].SHAIN_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "運転者");
                        this.isInputError = true;
                        row.Cells["UNTENSHA_NAME"].Value = string.Empty;
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }
              
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechishashuCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// 受付伝票のモバイル/ロジコン/NAVITIME連携状況チェック
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

            //KB:1受付データロジコン連携チェック
            if (KB == 1 )
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
                dt = this.UketsukeSSdao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    RenkeiCheck = true;
                }
            }
           

            return RenkeiCheck;
        }
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
