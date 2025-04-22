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
using r_framework.Entity;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.Dto;

namespace Shougun.Core.SalesPayment.TankaRirekiIchiran
{
    /// <summary>
    /// G280 申請一覧ロジック
    /// </summary>
    internal class TankaRirekiIchiranLogic : IBuisinessLogic
    {
        #region 宣言
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private string formId;
        private string kyotenCd;
        private string torihikisakiCd;
        private string gyoushaCd;
        private string genbaCd;
        private string unpanGyoushaCd;
        private string nizumiGyoushaCd;
        private string nizumiGenbaCd;
        private string nioroshiGyoushaCd;
        private string nioroshiGenbaCd;
        private string hinmeiCd;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private TankaRirekiIchiranUIForm form;

        private ITankaRirekiIchiranDao ichiranDao;
        private IM_KYOTENDao kyotenDao;
        private IM_TORIHIKISAKIDao torihikisakiDao;
        private IM_GYOUSHADao gyoushaDao;
        private IM_GENBADao genbaDao;
        private IM_HINMEIDao hinmeiDao;
        /// <summary>
        /// 画面のデータを保持するDTOを取得・設定します
        /// </summary>
        internal TankaRirekiIchiranDto Dto { get; set; }
        private MessageBoxShowLogic MsgBox;
        internal string beforeGyoushaCd = string.Empty;
        internal bool errorValidating = false;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public TankaRirekiIchiranLogic(TankaRirekiIchiranUIForm targetForm, string formId,
            string kyotenCd, string torihikisakiCd, string gyoushaCd, string genbaCd, string unpanGyoushaCd,
            string nizumiGyoushaCd, string nizumiGenbaCd, string nioroshiGyoushaCd, string nioroshiGenbaCd, string hinmeiCd)
        {
            LogUtility.DebugMethodStart(targetForm, formId, kyotenCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, hinmeiCd);
            this.form = targetForm;
            this.formId = formId;
            this.kyotenCd = kyotenCd;
            this.torihikisakiCd = torihikisakiCd;
            this.gyoushaCd = gyoushaCd;
            this.genbaCd = genbaCd;
            this.unpanGyoushaCd = unpanGyoushaCd;
            this.nizumiGyoushaCd = nizumiGyoushaCd;
            this.nizumiGenbaCd = nizumiGenbaCd;
            this.nioroshiGyoushaCd = nioroshiGyoushaCd;
            this.nioroshiGenbaCd = nioroshiGenbaCd;
            this.hinmeiCd = hinmeiCd;
            this.MsgBox = new MessageBoxShowLogic();
            this.ichiranDao = DaoInitUtility.GetComponent<ITankaRirekiIchiranDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
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
                // イベントの初期化処理
                this.EventInit();
                this.ClearValueForm();
                this.LoadCondition();
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
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();
            // 前月(F1)イベント追加
            this.form.bt_func1.Click += new EventHandler(this.form.ButtonFunc1_Clicked);
            // 次月(F2)イベント追加
            this.form.bt_func2.Click += new EventHandler(this.form.ButtonFunc2_Clicked);
            // 条件取消ボタン(F7)イベント追加
            this.form.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);
            // 検索ボタン(F8)イベント追加
            this.form.bt_func8.Click += new EventHandler(this.form.ButtonFunc8_Clicked);
            // 確定登録ボタン(F9)イベント追加
            this.form.bt_func9.Click += new EventHandler(this.form.ButtonFunc9_Clicked);
            // 並び替えボタン(F10)イベント追加
            this.form.bt_func10.Click += new EventHandler(this.form.ButtonFunc10_Clicked);
            // フィルタボタン(F11)イベント追加
            this.form.bt_func11.Click += new EventHandler(this.form.ButtonFunc11_Clicked);
            // 閉じるボタン(F12)イベント追加
            this.form.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        internal void LoadCondition()
        {
            //拠点
            if (!string.IsNullOrEmpty(this.kyotenCd))
            {
                var kyotenEntity = this.kyotenDao.GetDataByCd(this.kyotenCd);
                if (kyotenEntity != null)
                {
                    this.form.KYOTEN_CD.Text = this.kyotenCd;
                    this.form.KYOTEN_NAME_RYAKU.Text = kyotenEntity.KYOTEN_NAME_RYAKU;
                }
            }
            //取引先
            if (!string.IsNullOrEmpty(this.torihikisakiCd))
            {
                var torihikisakiEntity = this.torihikisakiDao.GetDataByCd(this.torihikisakiCd);
                if (torihikisakiEntity != null)
                {
                    this.form.TORIHIKISAKI_CD.Text = this.torihikisakiCd;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                }
            }
            //業者
            if (!string.IsNullOrEmpty(this.gyoushaCd))
            {
                var gyoushaEntity = this.gyoushaDao.GetDataByCd(this.gyoushaCd);
                if (gyoushaEntity != null)
                {
                    this.form.GYOUSHA_CD.Text = this.gyoushaCd;
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                }
            }
            //現場
            if (!string.IsNullOrEmpty(this.genbaCd) && !string.IsNullOrEmpty(this.gyoushaCd))
            {
                var condition = new M_GENBA();
                condition.GYOUSHA_CD = this.gyoushaCd;
                condition.GENBA_CD = this.genbaCd;
                var genbaEntity = this.genbaDao.GetDataByCd(condition);
                if (genbaEntity != null)
                {
                    this.form.GENBA_CD.Text = this.genbaCd;
                    this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                }
            }
            //運搬業者
            if (!string.IsNullOrEmpty(this.unpanGyoushaCd))
            {
                var gyoushaEntity = this.gyoushaDao.GetDataByCd(this.unpanGyoushaCd);
                if (gyoushaEntity != null)
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = this.unpanGyoushaCd;
                    this.form.UNPAN_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                }
            }
            //荷積業者
            if (!string.IsNullOrEmpty(this.nizumiGyoushaCd))
            {
                var gyoushaEntity = this.gyoushaDao.GetDataByCd(this.nizumiGyoushaCd);
                if (gyoushaEntity != null)
                {
                    this.form.NIZUMI_GYOUSHA_CD.Text = this.nizumiGyoushaCd;
                    this.form.NIZUMI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                }
            }
            //荷積現場
            if (!string.IsNullOrEmpty(this.nizumiGenbaCd) && !string.IsNullOrEmpty(this.nizumiGyoushaCd))
            {
                var condition = new M_GENBA();
                condition.GYOUSHA_CD = this.nizumiGyoushaCd;
                condition.GENBA_CD = this.nizumiGenbaCd;
                var genbaEntity = this.genbaDao.GetDataByCd(condition);
                if (genbaEntity != null)
                {
                    this.form.NIZUMI_GENBA_CD.Text = this.nizumiGenbaCd;
                    this.form.NIZUMI_GENBA_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                }
            }
            //荷降業者
            if (!string.IsNullOrEmpty(this.nioroshiGyoushaCd))
            {
                var gyoushaEntity = this.gyoushaDao.GetDataByCd(this.nioroshiGyoushaCd);
                if (gyoushaEntity != null)
                {
                    this.form.NIOROSHI_GYOUSHA_CD.Text = this.nioroshiGyoushaCd;
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                }
            }
            //荷降現場
            if (!string.IsNullOrEmpty(this.nioroshiGenbaCd) && !string.IsNullOrEmpty(this.nioroshiGyoushaCd))
            {
                var condition = new M_GENBA();
                condition.GYOUSHA_CD = this.nioroshiGyoushaCd;
                condition.GENBA_CD = this.nioroshiGenbaCd;
                var genbaEntity = this.genbaDao.GetDataByCd(condition);
                if (genbaEntity != null)
                {
                    this.form.NIOROSHI_GENBA_CD.Text = this.nioroshiGenbaCd;
                    this.form.NIOROSHI_GENBA_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                }
            }

            if (!string.IsNullOrEmpty(this.hinmeiCd))
            {
                var hinmeiEntity = this.hinmeiDao.GetDataByCd(this.hinmeiCd);
                if (hinmeiEntity != null)
                {
                    this.form.HINMEI_CD.Text = this.hinmeiCd;
                    this.form.HINMEI_NAME.Text = hinmeiEntity.HINMEI_NAME_RYAKU;
                }
            }
        }

        /// <summary>
        /// ヘッダ初期データを設定します
        /// </summary>
        public void InitHeader()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var configProfile = new XMLAccessor().XMLReader_CurrentUserCustomConfigProfile();
                string kyotenCd = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));

                if (!string.IsNullOrEmpty(kyotenCd))
                {
                    var kyotenEntity = this.kyotenDao.GetDataByCd(kyotenCd);
                    if (kyotenEntity != null && kyotenEntity.DELETE_FLG.IsFalse)
                    {
                        this.form.KYOTEN_CD.Text = kyotenCd;
                        this.form.KYOTEN_NAME_RYAKU.Text = kyotenEntity.KYOTEN_NAME_RYAKU;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeaderInitData", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitDate()
        {
            var dateTime = this.GetDBDateTime();
            this.form.HIDZUKE_FROM.Text = dateTime.AddMonths(-1).ToString("yyyy/MM/dd(ddd)");
            this.form.HIDZUKE_TO.Text = dateTime.ToString("yyyy/MM/dd(ddd)");
        }

        /// <summary>
        /// 
        /// </summary>
        private void EnabledControl()
        {
            if (!string.IsNullOrEmpty(this.formId))
            {
                switch (this.formId)
                { 
                    case "G721":
                    case "G051":
                    case "G015":
                    case "G018":
                        this.form.NIZUMI_GYOUSHA_CD.Enabled = false;
                        this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.Enabled = false;
                        this.form.NIZUMI_GENBA_CD.Enabled = false;
                        this.form.NIZUMI_GENBA_SEARCH_BUTTON.Enabled = false;
                        this.form.NIOROSHI_GYOUSHA_CD.Enabled = true;
                        this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.Enabled = true;
                        this.form.NIOROSHI_GENBA_CD.Enabled = true;
                        this.form.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = true;
                        break;
                    case "G722":
                    case "G053":
                    case "G016":
                    case "G157":
                        this.form.NIZUMI_GYOUSHA_CD.Enabled = true;
                        this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.Enabled = true;
                        this.form.NIZUMI_GENBA_CD.Enabled = true;
                        this.form.NIZUMI_GENBA_SEARCH_BUTTON.Enabled = true;
                        this.form.NIOROSHI_GYOUSHA_CD.Enabled = false;
                        this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.Enabled = false;
                        this.form.NIOROSHI_GENBA_CD.Enabled = false;
                        this.form.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = false;
                        break;
                    case "G054":
                        this.form.NIZUMI_GYOUSHA_CD.Enabled = true;
                        this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.Enabled = true;
                        this.form.NIZUMI_GENBA_CD.Enabled = true;
                        this.form.NIZUMI_GENBA_SEARCH_BUTTON.Enabled = true;
                        this.form.NIOROSHI_GYOUSHA_CD.Enabled = true;
                        this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.Enabled = true;
                        this.form.NIOROSHI_GENBA_CD.Enabled = true;
                        this.form.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = true;
                        break;
                    case "G161_1":
                    case "G161_2":
                        this.form.NIZUMI_GYOUSHA_CD.Enabled = false;
                        this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.Enabled = false;
                        this.form.NIZUMI_GENBA_CD.Enabled = false;
                        this.form.NIZUMI_GENBA_SEARCH_BUTTON.Enabled = false;
                        this.form.NIOROSHI_GYOUSHA_CD.Enabled = false;
                        this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.Enabled = false;
                        this.form.NIOROSHI_GENBA_CD.Enabled = false;
                        this.form.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = false;
                        break;
                }

                if (this.formId == "G015" || this.formId == "G016" || this.formId == "G018")
                {
                    this.form.KAKUTEI_KBN.Enabled = false;
                    this.form.KAKUTEI_KBN_1.Enabled = false;
                    this.form.KAKUTEI_KBN_2.Enabled = false;
                    this.form.KAKUTEI_KBN_3.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DateTime GetDBDateTime()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        #endregion

        #region 前月(F1)ボタン
        /// <summary>
        /// 前月ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonMonthPrevious()
        {
            if (!string.IsNullOrEmpty(this.form.HIDZUKE_FROM.Text)
                && !string.IsNullOrEmpty(this.form.HIDZUKE_TO.Text))
            {
                DateTime hidzukeFrom = DateTime.Parse(this.form.HIDZUKE_FROM.Text).AddMonths(-1);
                this.form.HIDZUKE_FROM.Text = hidzukeFrom.ToString("yyyy/MM/dd(ddd)");
                DateTime hidzukeTo = DateTime.Parse(this.form.HIDZUKE_TO.Text).AddMonths(-1);
                this.form.HIDZUKE_TO.Text = hidzukeTo.ToString("yyyy/MM/dd(ddd)");
            }
        }
        #endregion

        #region 次月(F2)ボタン
        /// <summary>
        /// 次月ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonMonthNext()
        {
            if (!string.IsNullOrEmpty(this.form.HIDZUKE_FROM.Text)
                && !string.IsNullOrEmpty(this.form.HIDZUKE_TO.Text))
            {
                DateTime hidzukeFrom = DateTime.Parse(this.form.HIDZUKE_FROM.Text).AddMonths(1);
                this.form.HIDZUKE_FROM.Text = hidzukeFrom.ToString("yyyy/MM/dd(ddd)");
                DateTime hidzukeTo = DateTime.Parse(this.form.HIDZUKE_TO.Text).AddMonths(1);
                this.form.HIDZUKE_TO.Text = hidzukeTo.ToString("yyyy/MM/dd(ddd)");
            }
        }
        #endregion

        #region 条件取消(F7)ボタン
        /// <summary>
        /// 
        /// </summary>
        internal void ClearValueForm()
        {
            this.InitHeader();
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
            this.form.DENPYOU_KBN.Text = "3";
            this.form.KAKUTEI_KBN.Text = "3";

            this.form.HINMEI_CD.Text = string.Empty;
            this.form.HINMEI_NAME.Text = string.Empty;

            this.form.TANKA_RIREKI_ICHIRAN.DataSource = null;
            this.InitDate();
            this.EnabledControl();
            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();
        }
        #endregion

        #region 検索(F8)ボタン
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool IsInputRequest()
        {
            bool isOk = false;
            StringBuilder errorString = new StringBuilder();
            this.form.KYOTEN_CD.BackColor = Constans.NOMAL_COLOR;
            this.form.HIDZUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.HIDZUKE_TO.BackColor = Constans.NOMAL_COLOR;
            this.form.DENPYOU_KBN.BackColor = Constans.NOMAL_COLOR;
            this.form.KAKUTEI_KBN.BackColor = Constans.NOMAL_COLOR;

            if (string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
            {
                errorString.AppendLine("拠点は必須項目です。入力してください。");
                this.form.KYOTEN_CD.BackColor = Constans.ERROR_COLOR;
                isOk = true;
            }
            if (string.IsNullOrEmpty(this.form.HIDZUKE_FROM.Text))
            {
                errorString.AppendLine("伝票日付(From)は必須項目です。入力してください。");
                this.form.HIDZUKE_FROM.BackColor = Constans.ERROR_COLOR;
                isOk = true;
            }
            if (string.IsNullOrEmpty(this.form.HIDZUKE_TO.Text))
            {
                errorString.AppendLine("伝票日付(To)は必須項目です。入力してください。");
                this.form.HIDZUKE_TO.BackColor = Constans.ERROR_COLOR;
                isOk = true;
            }
            if (string.IsNullOrEmpty(this.form.DENPYOU_KBN.Text))
            {
                errorString.AppendLine("伝票区分は必須項目です。入力してください。");
                this.form.DENPYOU_KBN.BackColor = Constans.ERROR_COLOR;
                isOk = true;
            }
            if (string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
            {
                errorString.AppendLine("確定区分は必須項目です。入力してください。");
                this.form.KAKUTEI_KBN.BackColor = Constans.ERROR_COLOR;
                isOk = true;
            }
            if (isOk)
            {
                this.MsgBox.MessageBoxShowError(errorString.ToString());
                if (string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
                {
                    this.form.KYOTEN_CD.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.HIDZUKE_FROM.Text))
                {
                    this.form.HIDZUKE_FROM.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.HIDZUKE_TO.Text))
                {
                    this.form.HIDZUKE_TO.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.DENPYOU_KBN.Text))
                {
                    this.form.DENPYOU_KBN.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
                {
                    this.form.KAKUTEI_KBN.Focus();
                }
            }
            return isOk;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateConditionSearch()
        {
            this.Dto = new TankaRirekiIchiranDto();
            if (!string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
            {
                this.Dto.KYOTEN_CD = this.form.KYOTEN_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.Dto.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                this.Dto.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.Dto.GENBA_CD = this.form.GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                this.Dto.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
            {
                this.Dto.NIZUMI_GYOUSHA_CD = this.form.NIZUMI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
            {
                this.Dto.NIZUMI_GENBA_CD = this.form.NIZUMI_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
            {
                this.Dto.NIOROSHI_GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                this.Dto.NIOROSHI_GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.HIDZUKE_FROM.Text))
            {
                this.Dto.HIDZUKE_FROM = SqlDateTime.Parse(this.form.HIDZUKE_FROM.Value.ToString());
            }
            if (!string.IsNullOrEmpty(this.form.HIDZUKE_TO.Text))
            {
                this.Dto.HIDZUKE_TO = SqlDateTime.Parse(this.form.HIDZUKE_TO.Value.ToString());
            }
            if (!string.IsNullOrEmpty(this.form.DENPYOU_KBN.Text))
            {
                this.Dto.DENPYOU_KBN = this.form.DENPYOU_KBN.Text;
            }
            if (!string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
            {
                this.Dto.KAKUTEI_KBN = this.form.KAKUTEI_KBN.Text;
            }
            if (!string.IsNullOrEmpty(this.form.HINMEI_CD.Text))
            {
                this.Dto.HINMEI_CD = this.form.HINMEI_CD.Text;
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
                var result = new DataTable();
                this.CreateConditionSearch();
                switch (this.formId)
                { 
                    case "G721":
                    case "G051":
                        result = this.ichiranDao.GetDataUkeireForTankaRirekiIchiran(this.Dto);
                        break;
                    case "G722":
                    case "G053":
                        result = this.ichiranDao.GetDataShukkaForTankaRirekiIchiran(this.Dto);
                        break;
                    case "G054":
                        result = this.ichiranDao.GetDataUrShForTankaRirekiIchiran(this.Dto);
                        break;
                    case "G015":
                        result = this.ichiranDao.GetDataUketsukeSSForTankaRirekiIchiran(this.Dto);
                        break;
                    case "G016":
                        result = this.ichiranDao.GetDataUketsukeSKForTankaRirekiIchiran(this.Dto);
                        break;
                    case "G018":
                        result = this.ichiranDao.GetDataUketsukeMKForTankaRirekiIchiran(this.Dto);
                        break;
                    case "G157":
                        result = this.ichiranDao.GetDataKenshuForTankaRirekiIchiran(this.Dto);
                        break;
                    case "G161_1":
                        result = this.ichiranDao.GetDataDainoUkeireForTankaRirekiIchiran(this.Dto);
                        break;
                    case "G161_2":
                        result = this.ichiranDao.GetDataDainoShukkaForTankaRirekiIchiran(this.Dto);
                        break;
                }
                this.SetTankaRirekiIchiranDataSource(result);
                ret = result.Rows.Count;
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

        /// <summary>
        /// 単価履歴一覧のDataSourceにデータをセットします
        /// </summary>
        /// <param name="table"></param>
        private void SetTankaRirekiIchiranDataSource(DataTable table)
        {
            LogUtility.DebugMethodStart();
            // ヘッダを残して全行クリアした状態を描画して見せる
            var ds = this.form.TANKA_RIREKI_ICHIRAN.DataSource as DataTable;
            if (ds != null)
            {
                ds.Clear();
                this.form.TANKA_RIREKI_ICHIRAN.DataSource = ds;
                this.form.TANKA_RIREKI_ICHIRAN.Refresh();
            }

            this.form.TANKA_RIREKI_ICHIRAN.SuspendLayout();
            // 表示用のソート
            this.form.customSortHeader1.SortDataTable(table);
            // 表示時の場合のみ適用
            if (this.form.customSearchHeader1.Visible)
            {
                this.form.customSearchHeader1.SearchDataTable(table);
            }

            // ここで一度nullで初期化しないと列順がおかしくなる（DGVの列の自動生成の不具合？）
            this.form.TANKA_RIREKI_ICHIRAN.DataSource = null;
            this.form.TANKA_RIREKI_ICHIRAN.DataSource = table;

            foreach (DataGridViewColumn column in this.form.TANKA_RIREKI_ICHIRAN.Columns)
            {
                if (this.form.TANKA_RIREKI_ICHIRAN.RowCount == 0)
                {
                    column.Width = (column.HeaderText.Length * 10) + 55;
                }
                column.ReadOnly = true;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

                if (column.ValueType != null)
                {
                    switch (column.ValueType.Name)
                    {
                        case "Int16":
                        case "Int32":
                        case "Int64":
                        case "UInt32":
                        case "UInt64":
                        case "Single":
                        case "Double":
                        case "Decimal":
                            // 数値型ならセル値を右寄せにする
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "DateTime":
                            // 日付型は表示幅を「yyyy/mm/dd」が表示出来るよう固定長で表示を行う
                            column.Width = 110;
                            break;
                    }
                }

                if (column.Name.Equals("TANKA_COL"))
                {
                    column.DefaultCellStyle.Format = "#,##0.###";
                    column.DefaultCellStyle.NullValue = "0";
                }
            }

            this.form.TANKA_RIREKI_ICHIRAN.ResumeLayout();
            // 各フォームでこのメソッド呼び出し直後に列をいじったりカレントセルを無効にする
            // 処理をいろいろやっているのでBeginInvokeで非同期に実行させる。
            if (this.form.TANKA_RIREKI_ICHIRAN.RowCount > 0 && table.Rows.Count > 0)
            {
                this.form.BeginInvoke((MethodInvoker)adjustColumnSize);
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 各列の幅調整と先頭セルへのフォーカス設定
        /// 各フォームで列を追加したり独自処理がいろいろあるので、それが終わった後に処理すべく、
        /// CreateDataGridViewからBeginInvokeで非同期に実行される。
        /// </summary>
        private void adjustColumnSize()
        {
            var dgv = this.form.TANKA_RIREKI_ICHIRAN;
            if (dgv == null || dgv.ColumnCount == 0)
            {
                return;
            }
            if (dgv.RowCount == 0 || dgv.DataSource == null || ((DataTable)dgv.DataSource).Rows.Count == 0)
            {
                return;
            }
            dgv.SuspendLayout();

            // TIME_STAMP列はバイナリなのでDataGridViewImageColumnとなり、AutoResizeColumnsメソッドでエラーとなってしまう
            // そのため、列名が"TIME_STAMP"でDataGridViewImageColumn以外をリサイズ対象とする
            // また、入力項目についてはリサイズを行わない(入力項目は初期状態ブランクの場合、幅が小さくなってしまため)
            // ※画面によってはCheckBoxも影響を受けてしまうため、返却日入力用にDgvCustomDataTimeColumnだけリサイズしないようにしている。
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                if (c.Visible && !(c is DataGridViewImageColumn) && !c.Name.Equals("TIME_STAMP") && !c.Name.Equals("DENPYOU_DATE_COL")
                    && (c.ReadOnly || c.GetType() != typeof(DgvCustomDataTimeColumn)))
                {
                    dgv.AutoResizeColumn(c.Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
                }
            }
            dgv.ResumeLayout();
        }
        #endregion

        #region 取引先, 業者, 現場
        /// <summary>
        /// 
        /// </summary>
        private void GetTorihikisakiInfo(string torihikisakiCd)
        {
            if (!string.IsNullOrEmpty(torihikisakiCd))
            {
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                var torihikisakiEntity = this.torihikisakiDao.GetDataByCd(torihikisakiCd);

                if (torihikisakiEntity != null)
                {
                    this.form.TORIHIKISAKI_CD.Text = torihikisakiCd;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void GetTorihikisakiInfoByGyousha()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                if (!this.form.GYOUSHA_CD.Text.Equals(this.beforeGyoushaCd))
                {
                    var gyoushaEntity = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                    if (gyoushaEntity != null)
                    {
                        this.GetTorihikisakiInfo(gyoushaEntity.TORIHIKISAKI_CD);
                    }
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        internal void GyoushaCdEnter(bool isButtonSearch = false)
        {
            if (this.formId.Equals("G721") || this.formId.Equals("G722") || this.formId.Equals("G051")
                 || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018")
                 || this.formId.Equals("G053") || this.formId.Equals("G016") || this.formId.Equals("G157")
                 || this.formId.Equals("G161_2"))
            {
                this.form.GYOUSHA_CD.PopupSearchSendParams.Clear();
                PopupSearchSendParamDto paramDto = new PopupSearchSendParamDto();
                paramDto.And_Or = CONDITION_OPERATOR.AND;
                if (this.formId.Equals("G721") || this.formId.Equals("G051")
                    || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                {
                    paramDto.KeyName = "GYOUSHAKBN_UKEIRE";
                }
                else if (this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016") 
                    || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                {
                    paramDto.KeyName = "GYOUSHAKBN_SHUKKA";
                }
                paramDto.Value = "TRUE";
                if (isButtonSearch)
                {
                    this.form.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto);
                }
                else
                {
                    this.form.GYOUSHA_CD.PopupSearchSendParams.Add(paramDto);
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        internal void GyoushaCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            this.errorValidating = false;
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                if (!this.form.GYOUSHA_CD.Text.Equals(this.beforeGyoushaCd))
                {
                    bool isDataExisted = false;
                    var gyoushaEntity = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                    if (gyoushaEntity != null )
                    {
                        if ((this.formId.Equals("G721") || this.formId.Equals("G051")
                            || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                            && gyoushaEntity.GYOUSHAKBN_UKEIRE.IsTrue)
                        {
                            isDataExisted = true;
                        }
                        else if ((this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016")
                            || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                            && gyoushaEntity.GYOUSHAKBN_SHUKKA.IsTrue)
                        {
                            isDataExisted = true;
                        }
                        else if (this.formId.Equals("G054"))
                        {
                            isDataExisted = true;
                        }
                    }

                    if (isDataExisted)
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        this.beforeGyoushaCd = this.form.GYOUSHA_CD.Text;
                        this.GetTorihikisakiInfo(gyoushaEntity.TORIHIKISAKI_CD);
                    }
                    else
                    {
                        this.MsgBox.MessageBoxShow("E020", "業者");
                        this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        this.beforeGyoushaCd = string.Empty;
                        e.Cancel = true;
                        this.errorValidating = true;
                    }
                }
            }
            else
            {
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.beforeGyoushaCd = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void GetTorihikisakiInfoByGenba()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text)
                && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                var condition = new M_GENBA();
                condition.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                condition.GENBA_CD = this.form.GENBA_CD.Text;
                var genbaEntity = this.genbaDao.GetDataByCd(condition);
                if (genbaEntity != null)
                {
                    this.GetTorihikisakiInfo(genbaEntity.TORIHIKISAKI_CD);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isButtonSearch"></param>
        internal void GenbaCdEnter(bool isButtonSearch = false)
        {
            if (this.formId.Equals("G721") || this.formId.Equals("G722") || this.formId.Equals("G051")
                 || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018")
                 || this.formId.Equals("G053") || this.formId.Equals("G016") || this.formId.Equals("G157")
                 || this.formId.Equals("G161_2"))
            {
                this.form.GENBA_CD.PopupSearchSendParams.Clear();
                PopupSearchSendParamDto paramDto1 = new PopupSearchSendParamDto();
                paramDto1.And_Or = CONDITION_OPERATOR.AND;
                paramDto1.Control = "GYOUSHA_CD";
                paramDto1.KeyName = "GYOUSHA_CD";
                PopupSearchSendParamDto paramDto2 = new PopupSearchSendParamDto();
                paramDto2.And_Or = CONDITION_OPERATOR.AND;
                if (this.formId.Equals("G721") || this.formId.Equals("G051")
                     || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                {
                    paramDto2.KeyName = "M_GYOUSHA.GYOUSHAKBN_UKEIRE";
                }
                else if (this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016") 
                    || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                {
                    paramDto2.KeyName = "M_GYOUSHA.GYOUSHAKBN_SHUKKA";
                }
                paramDto2.Value = "TRUE";
                if (isButtonSearch)
                {
                    this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto1);
                    this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto2);
                }
                else
                {
                    this.form.GENBA_CD.PopupSearchSendParams.Add(paramDto1);
                    this.form.GENBA_CD.PopupSearchSendParams.Add(paramDto2);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        internal void GenbaCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.MsgBox.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    this.form.GYOUSHA_CD.Focus();
                }
                else
                {
                    var condition = new M_GENBA();
                    condition.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    condition.GENBA_CD = this.form.GENBA_CD.Text;
                    var genbaEntity = this.genbaDao.GetDataByCd(condition);
                    if (genbaEntity != null)
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                        this.GetTorihikisakiInfo(genbaEntity.TORIHIKISAKI_CD);
                    }
                    else
                    {
                        this.MsgBox.MessageBoxShow("E020", "現場");
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }
        #endregion

        #region 運搬業者
        /// <summary>
        /// 
        /// </summary>
        internal void UnpanGyoushaCdEnter(bool isButtonSearch = false)
        {
            if (this.formId.Equals("G721") || this.formId.Equals("G722") || this.formId.Equals("G051")
                 || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018")
                 || this.formId.Equals("G053") || this.formId.Equals("G016") || this.formId.Equals("G157")
                 || this.formId.Equals("G161_2"))
            {
                this.form.UNPAN_GYOUSHA_CD.PopupSearchSendParams.Clear();
                PopupSearchSendParamDto paramDto1 = new PopupSearchSendParamDto();
                paramDto1.And_Or = CONDITION_OPERATOR.AND;
                paramDto1.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                paramDto1.Value = "TRUE";
                PopupSearchSendParamDto paramDto2 = new PopupSearchSendParamDto();
                paramDto2.And_Or = CONDITION_OPERATOR.AND;
                if (this.formId.Equals("G721") || this.formId.Equals("G051")
                    || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                {
                    paramDto2.KeyName = "GYOUSHAKBN_UKEIRE";
                }
                else if (this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016") 
                    || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                {
                    paramDto2.KeyName = "GYOUSHAKBN_SHUKKA";
                }
                paramDto2.Value = "TRUE";
                if (isButtonSearch)
                {
                    this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto1);
                    this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto2);
                }
                else
                {
                    this.form.UNPAN_GYOUSHA_CD.PopupSearchSendParams.Add(paramDto1);
                    this.form.UNPAN_GYOUSHA_CD.PopupSearchSendParams.Add(paramDto2);
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UnpanGyoushaCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                bool isDataExisted = false;
                var gyoushaEntity = this.gyoushaDao.GetDataByCd(this.form.UNPAN_GYOUSHA_CD.Text);
                if (gyoushaEntity != null && gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    if ((this.formId.Equals("G721") || this.formId.Equals("G051")
                            || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                            && gyoushaEntity.GYOUSHAKBN_UKEIRE.IsTrue)
                    {
                        isDataExisted = true;
                    }
                    else if ((this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016")
                            || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                            && gyoushaEntity.GYOUSHAKBN_SHUKKA.IsTrue)
                    {
                        isDataExisted = true;
                    }
                    else if (this.formId.Equals("G054"))
                    {
                        isDataExisted = true;
                    }
                }

                if (isDataExisted)
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E020", "業者");
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    e.Cancel = true;
                }
            }
            else
            {
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            }
        }
        #endregion

        #region 荷積業者, 荷積現場
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isButtonSearch"></param>
        internal void NizumiGyoushaCdEnter(bool isButtonSearch = false)
        {
            if (this.formId.Equals("G721") || this.formId.Equals("G722") || this.formId.Equals("G051")
                 || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018")
                 || this.formId.Equals("G053") || this.formId.Equals("G016") || this.formId.Equals("G157")
                 || this.formId.Equals("G161_2"))
            {
                this.form.NIZUMI_GYOUSHA_CD.PopupSearchSendParams.Clear();
                PopupSearchSendParamDto paramDto1 = new PopupSearchSendParamDto();
                paramDto1.And_Or = CONDITION_OPERATOR.AND;
                PopupSearchSendParamDto paramDto11 = new PopupSearchSendParamDto();
                paramDto11.And_Or = CONDITION_OPERATOR.AND;
                paramDto11.KeyName = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                paramDto11.Value = "TRUE";
                PopupSearchSendParamDto paramDto12 = new PopupSearchSendParamDto();
                paramDto12.And_Or = CONDITION_OPERATOR.OR;
                paramDto12.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                paramDto12.Value = "TRUE";
                paramDto1.SubCondition.Add(paramDto11);
                paramDto1.SubCondition.Add(paramDto12);

                PopupSearchSendParamDto paramDto2 = new PopupSearchSendParamDto();
                paramDto2.And_Or = CONDITION_OPERATOR.AND;
                if (this.formId.Equals("G721") || this.formId.Equals("G051")
                    || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                {
                    paramDto2.KeyName = "GYOUSHAKBN_UKEIRE";
                }
                else if (this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016") 
                    || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                {
                    paramDto2.KeyName = "GYOUSHAKBN_SHUKKA";
                }
                paramDto2.Value = "TRUE";
                if (isButtonSearch)
                {
                    this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto1);
                    this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto2);
                }
                else
                {
                    this.form.NIZUMI_GYOUSHA_CD.PopupSearchSendParams.Add(paramDto1);
                    this.form.NIZUMI_GYOUSHA_CD.PopupSearchSendParams.Add(paramDto2);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        internal void NizumiGyoushaCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            this.errorValidating = false;
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
            {
                if (!this.form.NIZUMI_GYOUSHA_CD.Text.Equals(this.beforeGyoushaCd))
                {
                    bool isDataExisted = false;
                    var gyoushaEntity = this.gyoushaDao.GetDataByCd(this.form.NIZUMI_GYOUSHA_CD.Text);
                    if (gyoushaEntity != null
                        && (gyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue 
                            || gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                    {
                        if ((this.formId.Equals("G721") || this.formId.Equals("G051")
                            || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                            && gyoushaEntity.GYOUSHAKBN_UKEIRE.IsTrue)
                        {
                            isDataExisted = true;
                        }
                        else if ((this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016")
                            || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                            && gyoushaEntity.GYOUSHAKBN_SHUKKA.IsTrue)
                        {
                            isDataExisted = true;
                        }
                        else if (this.formId.Equals("G054"))
                        {
                            isDataExisted = true;
                        }
                    }
                    
                    if (isDataExisted)
                    {
                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        this.beforeGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;
                    }
                    else
                    {
                        this.MsgBox.MessageBoxShow("E020", "業者");
                        this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        this.beforeGyoushaCd = string.Empty;
                        e.Cancel = true;
                        this.errorValidating = true;
                    }
                }
            }
            else
            {
                this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                this.beforeGyoushaCd = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isButtonSearch"></param>
        internal void NizumiGenbaCdEnter(bool isButtonSearch = false)
        {
            if (this.formId.Equals("G721") || this.formId.Equals("G722") || this.formId.Equals("G051")
                 || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018")
                 || this.formId.Equals("G053") || this.formId.Equals("G016") || this.formId.Equals("G157")
                 || this.formId.Equals("G161_2"))
            {
                this.form.NIOROSHI_GENBA_CD.PopupSearchSendParams.Clear();
                PopupSearchSendParamDto paramDto1 = new PopupSearchSendParamDto();
                paramDto1.And_Or = CONDITION_OPERATOR.AND;
                PopupSearchSendParamDto paramDto11 = new PopupSearchSendParamDto();
                paramDto11.And_Or = CONDITION_OPERATOR.AND;
                paramDto11.KeyName = "M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                paramDto11.Value = "TRUE";
                PopupSearchSendParamDto paramDto12 = new PopupSearchSendParamDto();
                paramDto12.And_Or = CONDITION_OPERATOR.OR;
                paramDto12.KeyName = "M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN";
                paramDto12.Value = "TRUE";
                paramDto1.SubCondition.Add(paramDto11);
                paramDto1.SubCondition.Add(paramDto12);

                PopupSearchSendParamDto paramDto2 = new PopupSearchSendParamDto();
                paramDto2.And_Or = CONDITION_OPERATOR.AND;
                PopupSearchSendParamDto paramDto21 = new PopupSearchSendParamDto();
                paramDto21.And_Or = CONDITION_OPERATOR.AND;
                paramDto21.KeyName = "M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN";
                paramDto21.Value = "TRUE";
                PopupSearchSendParamDto paramDto22 = new PopupSearchSendParamDto();
                paramDto22.And_Or = CONDITION_OPERATOR.OR;
                paramDto22.KeyName = "M_GENBA.TSUMIKAEHOKAN_KBN";
                paramDto22.Value = "TRUE";
                paramDto2.SubCondition.Add(paramDto21);
                paramDto2.SubCondition.Add(paramDto22);

                PopupSearchSendParamDto paramDto3 = new PopupSearchSendParamDto();
                paramDto3.And_Or = CONDITION_OPERATOR.AND;
                if (this.formId.Equals("G721") || this.formId.Equals("G051")
                    || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                {
                    paramDto3.KeyName = "M_GYOUSHA.GYOUSHAKBN_UKEIRE";
                }
                else if (this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016") 
                    || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                {
                    paramDto3.KeyName = "M_GYOUSHA.GYOUSHAKBN_SHUKKA";
                }
                paramDto3.Value = "TRUE";

                PopupSearchSendParamDto paramDto4 = new PopupSearchSendParamDto();
                paramDto4.And_Or = CONDITION_OPERATOR.AND;
                paramDto4.Control = "NIZUMI_GYOUSHA_CD";
                paramDto4.KeyName = "GYOUSHA_CD";

                if (isButtonSearch)
                {
                    this.form.NIZUMI_GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto1);
                    this.form.NIZUMI_GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto2);
                    this.form.NIZUMI_GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto3);
                    this.form.NIZUMI_GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto4);
                }
                else
                {
                    this.form.NIZUMI_GENBA_CD.PopupSearchSendParams.Add(paramDto1);
                    this.form.NIZUMI_GENBA_CD.PopupSearchSendParams.Add(paramDto2);
                    this.form.NIZUMI_GENBA_CD.PopupSearchSendParams.Add(paramDto3);
                    this.form.NIZUMI_GENBA_CD.PopupSearchSendParams.Add(paramDto4);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        internal void NizumiGenbaCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
            {
                if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                {
                    this.MsgBox.MessageBoxShow("E051", "業者");
                    this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                    this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                    this.form.NIZUMI_GYOUSHA_CD.Focus();
                }
                else
                {
                    var condition = new M_GENBA();
                    condition.GYOUSHA_CD = this.form.NIZUMI_GYOUSHA_CD.Text;
                    condition.GENBA_CD = this.form.NIZUMI_GENBA_CD.Text;
                    var genbaEntity = this.genbaDao.GetDataByCd(condition);
                    if (genbaEntity != null
                        && (genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue 
                            || genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue))
                    {
                        this.form.NIZUMI_GENBA_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.MsgBox.MessageBoxShow("E020", "現場");
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
            }
        }
        #endregion

        #region 荷降業者, 荷降現場
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isButtonSearch"></param>
        internal void NioroshiGyoushaCdEnter(bool isButtonSearch = false)
        {
            if (this.formId.Equals("G721") || this.formId.Equals("G722") || this.formId.Equals("G051")
                 || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018")
                 || this.formId.Equals("G053") || this.formId.Equals("G016") || this.formId.Equals("G157")
                 || this.formId.Equals("G161_2"))
            {
                this.form.NIOROSHI_GYOUSHA_CD.PopupSearchSendParams.Clear();
                PopupSearchSendParamDto paramDto1 = new PopupSearchSendParamDto();
                paramDto1.And_Or = CONDITION_OPERATOR.AND;
                PopupSearchSendParamDto paramDto11 = new PopupSearchSendParamDto();
                paramDto11.And_Or = CONDITION_OPERATOR.AND;
                paramDto11.KeyName = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                paramDto11.Value = "TRUE";
                PopupSearchSendParamDto paramDto12 = new PopupSearchSendParamDto();
                paramDto12.And_Or = CONDITION_OPERATOR.OR;
                paramDto12.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                paramDto12.Value = "TRUE";
                paramDto1.SubCondition.Add(paramDto11);
                paramDto1.SubCondition.Add(paramDto12);

                PopupSearchSendParamDto paramDto2 = new PopupSearchSendParamDto();
                paramDto2.And_Or = CONDITION_OPERATOR.AND;
                if (this.formId.Equals("G721") || this.formId.Equals("G051")
                    || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                {
                    paramDto2.KeyName = "GYOUSHAKBN_UKEIRE";
                }
                else if (this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016") 
                    || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                {
                    paramDto2.KeyName = "GYOUSHAKBN_SHUKKA";
                }
                paramDto2.Value = "TRUE";
                if (isButtonSearch)
                {
                    this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto1);
                    this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto2);
                }
                else
                {
                    this.form.NIOROSHI_GYOUSHA_CD.PopupSearchSendParams.Add(paramDto1);
                    this.form.NIOROSHI_GYOUSHA_CD.PopupSearchSendParams.Add(paramDto2);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        internal void NioroshiGyoushaCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            this.errorValidating = false;
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
            {
                if (!this.form.NIOROSHI_GYOUSHA_CD.Text.Equals(this.beforeGyoushaCd))
                {
                    bool isDataExisted = false;
                    var gyoushaEntity = this.gyoushaDao.GetDataByCd(this.form.NIOROSHI_GYOUSHA_CD.Text);
                    if (gyoushaEntity != null
                        && (gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue 
                            || gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                    {
                        if ((this.formId.Equals("G721") || this.formId.Equals("G051")
                            || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                            && gyoushaEntity.GYOUSHAKBN_UKEIRE.IsTrue)
                        {
                            isDataExisted = true;
                        }
                        else if ((this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016")
                            || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                            && gyoushaEntity.GYOUSHAKBN_SHUKKA.IsTrue)
                        {
                            isDataExisted = true;
                        }
                        else if (this.formId.Equals("G054"))
                        {
                            isDataExisted = true;
                        }
                    }

                    if (isDataExisted)
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                        this.beforeGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;
                    }
                    else
                    {
                        this.MsgBox.MessageBoxShow("E020", "業者");
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                        this.beforeGyoushaCd = string.Empty;
                        e.Cancel = true;
                        this.errorValidating = true;
                    }
                }
            }
            else
            {
                this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.beforeGyoushaCd = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isButtonSearch"></param>
        internal void NioroshiGenbaCdEnter(bool isButtonSearch = false)
        {
            if (this.formId.Equals("G721") || this.formId.Equals("G722") || this.formId.Equals("G051")
                 || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018")
                 || this.formId.Equals("G053") || this.formId.Equals("G016") || this.formId.Equals("G157")
                 || this.formId.Equals("G161_2"))
            {
                this.form.NIOROSHI_GENBA_CD.PopupSearchSendParams.Clear();
                PopupSearchSendParamDto paramDto1 = new PopupSearchSendParamDto();
                paramDto1.And_Or = CONDITION_OPERATOR.AND;
                PopupSearchSendParamDto paramDto11 = new PopupSearchSendParamDto();
                paramDto11.And_Or = CONDITION_OPERATOR.AND;
                paramDto11.KeyName = "M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN";
                paramDto11.Value = "TRUE";
                PopupSearchSendParamDto paramDto12 = new PopupSearchSendParamDto();
                paramDto12.And_Or = CONDITION_OPERATOR.OR;
                paramDto12.KeyName = "M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN";
                paramDto12.Value = "TRUE";
                paramDto1.SubCondition.Add(paramDto11);
                paramDto1.SubCondition.Add(paramDto12);

                PopupSearchSendParamDto paramDto2 = new PopupSearchSendParamDto();
                paramDto2.And_Or = CONDITION_OPERATOR.AND;
                PopupSearchSendParamDto paramDto21 = new PopupSearchSendParamDto();
                paramDto21.And_Or = CONDITION_OPERATOR.AND;
                paramDto21.KeyName = "M_GENBA.SHOBUN_NIOROSHI_GENBA_KBN";
                paramDto21.Value = "TRUE";
                PopupSearchSendParamDto paramDto22 = new PopupSearchSendParamDto();
                paramDto22.And_Or = CONDITION_OPERATOR.OR;
                paramDto22.KeyName = "M_GENBA.SAISHUU_SHOBUNJOU_KBN";
                paramDto22.Value = "TRUE";
                PopupSearchSendParamDto paramDto23 = new PopupSearchSendParamDto();
                paramDto23.And_Or = CONDITION_OPERATOR.OR;
                paramDto23.KeyName = "M_GENBA.TSUMIKAEHOKAN_KBN";
                paramDto23.Value = "TRUE";
                paramDto2.SubCondition.Add(paramDto21);
                paramDto2.SubCondition.Add(paramDto22);
                paramDto2.SubCondition.Add(paramDto23);

                PopupSearchSendParamDto paramDto3 = new PopupSearchSendParamDto();
                paramDto3.And_Or = CONDITION_OPERATOR.AND;
                if (this.formId.Equals("G721") || this.formId.Equals("G051")
                    || this.formId.Equals("G161_1") || this.formId.Equals("G015") || this.formId.Equals("G018"))
                {
                    paramDto3.KeyName = "M_GYOUSHA.GYOUSHAKBN_UKEIRE";
                }
                else if (this.formId.Equals("G722") || this.formId.Equals("G053") || this.formId.Equals("G016") 
                    || this.formId.Equals("G157") || this.formId.Equals("G161_2"))
                {
                    paramDto3.KeyName = "M_GYOUSHA.GYOUSHAKBN_SHUKKA";
                }
                paramDto3.Value = "TRUE";

                PopupSearchSendParamDto paramDto4 = new PopupSearchSendParamDto();
                paramDto4.And_Or = CONDITION_OPERATOR.AND;
                paramDto4.Control = "NIOROSHI_GYOUSHA_CD";
                paramDto4.KeyName = "GYOUSHA_CD";

                if (isButtonSearch)
                {
                    this.form.NIOROSHI_GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto1);
                    this.form.NIOROSHI_GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto2);
                    this.form.NIOROSHI_GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto3);
                    this.form.NIOROSHI_GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(paramDto4);
                }
                else
                {
                    this.form.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(paramDto1);
                    this.form.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(paramDto2);
                    this.form.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(paramDto3);
                    this.form.NIOROSHI_GENBA_CD.PopupSearchSendParams.Add(paramDto4);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        internal void NioroshiGenbaCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    this.MsgBox.MessageBoxShow("E051", "業者");
                    this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                    this.form.NIOROSHI_GYOUSHA_CD.Focus();
                }
                else
                {
                    var condition = new M_GENBA();
                    condition.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                    condition.GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
                    var genbaEntity = this.genbaDao.GetDataByCd(condition);
                    if (genbaEntity != null
                        && (genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue 
                            || genbaEntity.SAISHUU_SHOBUNJOU_KBN.IsTrue
                            || genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue))
                    {
                        this.form.NIOROSHI_GENBA_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.MsgBox.MessageBoxShow("E020", "現場");
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }
        }
        #endregion

        #region NotDelete
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
        #endregion

        public void SetSelectedData(int RowIndex)
        {
            if (RowIndex < 0)
            {
                return;
            }

            this.form.dialogResult = DialogResult.OK;

            if (!string.IsNullOrEmpty(Convert.ToString(this.form.TANKA_RIREKI_ICHIRAN.Rows[RowIndex].Cells["TANKA_COL"].Value)))
            {
                this.form.returnTanka = decimal.Parse(Convert.ToString(this.form.TANKA_RIREKI_ICHIRAN.Rows[RowIndex].Cells["TANKA_COL"].Value));
            }

            this.form.returnUnitCd = Convert.ToString(this.form.TANKA_RIREKI_ICHIRAN.Rows[RowIndex].Cells["UNIT_CD_COL"].Value);

            this.form.Close();
        }
    }
}
