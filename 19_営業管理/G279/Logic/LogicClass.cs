using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.DenshiShinseiNyuuryoku
{
    internal class LogicClass : IBuisinessLogic
    {
        #region Property

        /// <summary>T_DENSHI_SHINSEI_ENTRY.SYSTEM_ID</summary>
        internal long SysId { get; set; }

        /// <summary>T_DENSHI_SHINSEI_ENTRY.SEQ</summary>
        internal int Seq { get; set; }

        /// <summary>初期化用DTO</summary>
        internal DenshiShinseiNyuuryokuInitDTO InitDto { get; set; }

        /// <summary>
        /// 起動元画面へ返すT_DENSHI_SHINSEI_ENTRY
        /// 新規登録時の場合、このENTRYをそのままDBへInsertする
        /// </summary>
        internal T_DENSHI_SHINSEI_ENTRY DenshiShinseiEntry { get; set; }

        /// <summary>
        /// 起動元画面へ返すT_DENSHI_SHINSEI_DETAIL
        /// 新規登録時の場合、このDETAILをそのままDBへInsertする
        /// </summary>
        internal List<T_DENSHI_SHINSEI_DETAIL> DenshiShinseiDetailList { get; set; }

        /// <summary>【承認・否認時排他制御などに使用する最新データ】電子申請状態</summary>
        private T_DENSHI_SHINSEI_STATUS DenshiShinseiStatus { get; set; }

        #endregion

        #region Field

        /// <summary>ヘッダフォーム</summary>
        private UIHeader headerForm;

        /// <summary>メインフォーム</summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>T_DENSHI_SHINSEI_ENTRY用Dao</summary>
        private DenshiShinseiNyuuryoku.DAO.IT_DENSHI_SHINSEI_ENTRYDao denshiShinseiEntryDao;

        /// <summary>T_DENSHI_SHINSEI_DETAIL用Dao</summary>
        private DenshiShinseiNyuuryoku.DAO.IT_DENSHI_SHINSEI_DETAILDao denshiShinseiDetailDao;

        /// <summary>T_DENSHI_SHINSEI_ACTION用Dao</summary>
        private IT_DENSHI_SHINSEI_DETAIL_ACTIONDao denshiShinseiDetailActionDao;

        /// <summary>T_DENSHI_SHINSEI_ROUTE用Dao</summary>
        private IM_DENSHI_SHINSEI_ROUTEDao denshiShinseiRouteDao;

        /// <summary>M_DENSHI_SHINSEI_ROUTE_NAME用Dao</summary>
        private IM_DENSHI_SHINSEI_ROUTE_NAMEDao denshiShinseiRouteNameDao;

        /// <summary>DETAIL - 部署CD 前回値</summary>
        private string beforeBushoCd = string.Empty;

        internal bool isInputError = false;

        private MessageBoxShowLogic MsgBox;
        #endregion

        #region Constant

        /* DETAIL Columns Name */
        /// <summary>明細 列名 - No</summary>
        private const string DETAIL_CELL_NAME_NO = "No";
        /// <summary>明細 列名 - 部署CD</summary>
        private const string DETAIL_CELL_NAME_BUSHOCD = "BushoCd";
        /// <summary>明細 列名 - 部署名</summary>
        private const string DETAIL_CELL_NAME_BUSHONAME = "BushoName";
        /// <summary>明細 列名 - 社員CD</summary>
        private const string DETAIL_CELL_NAME_SHAINCD = "ShainCd";
        /// <summary>明細 列名 - 社員名</summary>
        private const string DETAIL_CELL_NAME_SHAINNAME = "ShainName";
        /// <summary>明細 列名 - 確認日</summary>
        private const string DETAIL_CELL_NAME_KAKUNINDATE = "KakuninDate";
        /// <summary>明細 列名 - 決裁</summary>
        private const string DETAIL_CELL_NAME_KESSAI = "Kessai";
        /// <summary>明細 列名 - コメント</summary>
        private const string DETAIL_CELL_NAME_COMMENT = "Comment";
        /// <summary>明細 列名 - 明細システムID</summary>
        private const string DETAIL_CELL_NAME_DETAILSYSTEMID = "DetailSystemId";
        /// /// <summary>明細 列名 - 枝番</summary>
        private const string DETAIL_CELL_NAME_SEQ = "Seq";
        /// /// <summary>明細 列名 - 申請番号</summary>
        private const string DETAIL_CELL_NAME_SHINSEINUMBER = "ShinseiNumber";
        /// /// <summary>明細 列名 - 行番号</summary>
        private const string DETAIL_CELL_NAME_ROWNO = "RowNo";

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="header">ヘッダフォーム</param>
        /// <param name="form">メインフォーム</param>
        public LogicClass(UIHeader header, UIForm form)
        {
            LogUtility.DebugMethodStart(header, form);

            this.headerForm = header;
            this.form = form;

            this.denshiShinseiEntryDao = DaoInitUtility.GetComponent<DenshiShinseiNyuuryoku.DAO.IT_DENSHI_SHINSEI_ENTRYDao>();
            this.denshiShinseiDetailDao = DaoInitUtility.GetComponent<DenshiShinseiNyuuryoku.DAO.IT_DENSHI_SHINSEI_DETAILDao>();
            this.denshiShinseiDetailActionDao = DaoInitUtility.GetComponent<IT_DENSHI_SHINSEI_DETAIL_ACTIONDao>();
            this.denshiShinseiRouteDao = DaoInitUtility.GetComponent<IM_DENSHI_SHINSEI_ROUTEDao>();
            this.denshiShinseiRouteNameDao = DaoInitUtility.GetComponent<IM_DENSHI_SHINSEI_ROUTE_NAMEDao>();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Event
        /// <summary>
        /// 見積書添付の参照ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void referenceButton_Click(object sender, EventArgs e)
        {
            this.FileRefClick();
        }

        /// <summary>
        /// 見積書添付の閲覧ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseButton_Click(object sender, EventArgs e)
        {
            this.BrowseClick();
        }

        /// <summary>
        /// [F1]内容確認ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            if (this.DenshiShinseiStatus != null
                && (this.DenshiShinseiStatus.SHINSEI_STATUS_CD == (short)DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPLYING
                || (this.DenshiShinseiStatus.SHINSEI_STATUS_CD == (short)DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL)
                || (this.DenshiShinseiStatus.SHINSEI_STATUS_CD == (short)DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL_CONF))
            )
            {
                this.ShowReferenceDisplay();
            }
            else
            {
                // 否認、移行済みの場合は完了済み申請のため内容確認はできないようにする。

                DenshiShinseiUtility utility = new DenshiShinseiUtility();
                MessageBoxUtility.MessageBoxShow("E210", utility.ToString((int)this.DenshiShinseiStatus.SHINSEI_STATUS_CD));
            }
        }

        /// <summary>
        /// [F2]承認ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            KessaiPopupForm popupFprm = new KessaiPopupForm(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL);
            if (DialogResult.OK == popupFprm.ShowDialog())
            {
                bool isCommit = this.KessaiRegist(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL, popupFprm.Comment);
                if (!isCommit)
                {
                    // 排他エラー時は画面を最新状態にリロード
                    this.WindowInit();
                    return;
                }

                MessageBoxUtility.MessageBoxShow("I017", "申請を承認");

                popupFprm.Dispose();
                this.Close();

                FormManager.UpdateForm("G280");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F3]否認確認ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool isCommit = this.DenialConfirmationRegist();
            if (!isCommit)
            {
                // 排他エラー時は画面を最新状態にリロード
                this.WindowInit();
                return;
            }

            MessageBoxUtility.MessageBoxShow("I017", "否認内容を確認");

            this.Close();
            FormManager.UpdateForm("G280");

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F4]否認ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            KessaiPopupForm popupFprm = new KessaiPopupForm(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL);
            if (DialogResult.OK == popupFprm.ShowDialog())
            {
                bool isCommit = this.KessaiRegist(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL, popupFprm.Comment);
                if (!isCommit)
                {
                    // 排他エラー時は画面を最新状態にリロード
                    this.WindowInit();
                    return;
                }

                MessageBoxUtility.MessageBoxShow("I017", "申請を否認");

                popupFprm.Dispose();
                this.Close();

                FormManager.UpdateForm("G280");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F9]申請ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.RegistCheck();

            if (!this.form.RegistErrorFlag)
            {
                this.SetData();
                this.Close();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F10]行挿入ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.form.DETAIL.Rows.Count == 0)
            {
                return;
            }

            int index = this.form.DETAIL.SelectedCells[0].RowIndex;
            this.form.DETAIL.Rows.Insert(index, 1);
            this.DetailRowNumberReset();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F11]行削除ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.form.DETAIL.Rows.Count <= 1)
            {
                return;
            }

            int index = this.form.DETAIL.SelectedCells[0].RowIndex;

            if (this.form.DETAIL.Rows[index].IsNewRow)
            {
                return;
            }

            this.form.DETAIL.Rows.RemoveAt(index);
            this.DetailRowNumberReset();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F12]閉じるボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Method

        #region 画面Close

        private void Close()
        {
            var parent = (BusinessBaseForm)this.form.Parent;
            parent.Close();
        }

        #endregion

        #region 画面初期表示

        /// <summary>
        /// 画面情報の初期化を行います
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.PropertyInit();
                this.ButtonInit();
                this.EventInit();
                // ※注意　DETAILがreadOnly時に初期表示時に色が変わるようにするためデータロード前に行う
                this.SetDetailReference(this.form.WindowType);

                if (this.SysId > 0 && this.Seq > 0)
                {
                    // SystemId、Seqがある場合は保存データ読み込み
                    this.SetScreenBySaveData();
                }
                else
                {
                    this.SetHeaderInitData();
                    this.SetMainFormInitData();

                    // InitDtoより初期値読み込み
                    this.SetScreenByInitData();
                }

                // ※注意　データ依存でボタン使用可否があるためデータロード後に行う
                this.SetItemsReference(this.form.WindowType);
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
        /// プロパティの初期化処理を行います
        /// </summary>
        private void PropertyInit()
        {
            this.DenshiShinseiStatus = null;
        }

        /// <summary>
        /// ボタン初期化処理を行います
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
        /// ボタン設定の読込を行います
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            Type cType = this.GetType();
            string strButtonInfoXmlPath = cType.Namespace;
            strButtonInfoXmlPath += ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath));

            return buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath);
        }

        /// <summary>
        /// イベント処理の初期化を行います
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // ファイル参照、閲覧
            this.form.referenceButton.Click += new EventHandler(this.referenceButton_Click);
            this.form.browseButton.Click += new EventHandler(this.browseButton_Click);

            //Functionボタンのイベント生成
            parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);      //内容確認
            parentForm.bt_func2.Click += new EventHandler(bt_func2_Click);      //承認
            parentForm.bt_func3.Click += new EventHandler(bt_func3_Click);      //否認確認
            parentForm.bt_func4.Click += new EventHandler(bt_func4_Click);      //否認
            parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);      //申請
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;
            parentForm.bt_func10.Click += new EventHandler(bt_func10_Click);    //行挿入
            parentForm.bt_func11.Click += new EventHandler(bt_func11_Click);    //行削除
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);    //閉じる

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// WindowTypeによってDETAIL以外の各項目の参照設定を行います
        /// ※入力セルをReadOnlyにした場合に色の変更を行わさせるため、データロード前に使用してください
        /// </summary>
        /// <param name="type"></param>
        private void SetDetailReference(WINDOW_TYPE type)
        {
            switch (type)
            {
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    this.form.DETAIL.AllowUserToAddRows = false;
                    this.form.DETAIL.ReadOnly = true;
                    this.form.DETAIL.TabStop = false;
                    break;
                default:
                    // Nothing
                    break;
            }
        }

        /// <summary>
        /// WindowTypeによってDETAIL以外の各項目の参照設定を行います
        /// ※修正モード時にデータによってボタン使用の可否を設定しているのでデータロード後に使用してください
        /// </summary>
        /// <param name="type">WINDOW_TYPE</param>
        private void SetItemsReference(WINDOW_TYPE type)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            switch (type)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    parentForm.bt_func1.Enabled = false;
                    parentForm.bt_func2.Enabled = false;
                    parentForm.bt_func3.Enabled = false;
                    parentForm.bt_func4.Enabled = false;
                    break;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    if (!this.CheckUseApproval())
                    {
                        // ログインしている社員CDが最新未承認明細の社員CDではない場合、承認・否認出来ない
                        parentForm.bt_func2.Enabled = false;
                        parentForm.bt_func4.Enabled = false;
                    }
                    if (!this.CheckUseDenialConfirmation())
                    {
                        // 現在のステータスが否認確認でない場合、否認確認出来ない
                        parentForm.bt_func3.Enabled = false;
                    }
                    parentForm.bt_func9.Enabled = false;
                    parentForm.bt_func10.Enabled = false;
                    parentForm.bt_func11.Enabled = false;
                    this.headerForm.txtKyotenCd.ReadOnly = true;
                    this.headerForm.txtKyotenCd.TabStop = false;
                    this.form.SHINSEI_DATE.ReadOnly = true;
                    this.form.SHINSEI_DATE.TabStop = false;
                    this.form.JYUUYOUDO_CD.ReadOnly = true;
                    this.form.JYUUYOUDO_CD.TabStop = false;
                    this.form.SHINSEI_NAIYOU_CD.ReadOnly = true;
                    this.form.SHINSEI_NAIYOU_CD.TabStop = false;
                    this.form.SHINSEI_KEIRO_CD.ReadOnly = true;
                    this.form.SHINSEI_KEIRO_CD.TabStop = false;
                    this.form.MITSUMORISHO_PATH.ReadOnly = true;
                    this.form.MITSUMORISHO_PATH.TabStop = false;
                    this.form.referenceButton.Enabled = false;
                    this.form.SHINSEISHA_COMMENT.ReadOnly = true;
                    this.form.SHINSEISHA_COMMENT.TabStop = false;
                    break;
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    parentForm.bt_func2.Enabled = false;
                    parentForm.bt_func3.Enabled = false;
                    parentForm.bt_func4.Enabled = false;
                    parentForm.bt_func9.Enabled = false;
                    parentForm.bt_func10.Enabled = false;
                    parentForm.bt_func11.Enabled = false;
                    this.headerForm.txtKyotenCd.ReadOnly = true;
                    this.headerForm.txtKyotenCd.TabStop = false;
                    this.form.SHINSEI_DATE.ReadOnly = true;
                    this.form.SHINSEI_DATE.TabStop = false;
                    this.form.JYUUYOUDO_CD.ReadOnly = true;
                    this.form.JYUUYOUDO_CD.TabStop = false;
                    this.form.SHINSEI_NAIYOU_CD.ReadOnly = true;
                    this.form.SHINSEI_NAIYOU_CD.TabStop = false;
                    this.form.SHINSEI_KEIRO_CD.ReadOnly = true;
                    this.form.SHINSEI_KEIRO_CD.TabStop = false;
                    this.form.MITSUMORISHO_PATH.ReadOnly = true;
                    this.form.MITSUMORISHO_PATH.TabStop = false;
                    this.form.referenceButton.Enabled = false;
                    this.form.SHINSEISHA_COMMENT.ReadOnly = true;
                    this.form.SHINSEISHA_COMMENT.TabStop = false;
                    break;
                default:
                    // Nothing
                    break;
            }
        }

        /// <summary>
        /// 承認・否認操作が可能かをチェックします
        /// </summary>
        /// <returns>操作可：True　操作不可：False</returns>
        private bool CheckUseApproval()
        {
            bool val = true;

            /* 現在のステータスが申請中かのチェック */
            if (this.form.WindowType != WINDOW_TYPE.UPDATE_WINDOW_FLAG ||
                this.DenshiShinseiStatus == null ||
                !this.DenshiShinseiStatus.SHINSEI_STATUS_CD.Equals(SqlInt16.Parse(((short)DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPLYING).ToString())))
            {
                val = false;
            }

            if (val)
            {
                /* 最新未承認明細行がログインユーザの社員CDと一致しているかのチェック */
                int index = this.SearchLatestUnapprovedDeatailRow();
                if (index >= 0)
                {
                    string loginShainCd = SystemProperty.Shain.CD;
                    if (this.form.DETAIL.Rows[index].Cells[DETAIL_CELL_NAME_SHAINCD].Value == null ||
                        !this.form.DETAIL.Rows[index].Cells[DETAIL_CELL_NAME_SHAINCD].Value.ToString().Equals(loginShainCd))
                    {
                        val = false;
                    }
                }
            }

            return val;
        }

        /// <summary>
        /// 否認確認操作が可能かをチェックします
        /// </summary>
        /// <returns>操作可：True　操作不可：False</returns>
        private bool CheckUseDenialConfirmation()
        {
            bool val = true;

            /* 現在のステータスが否認確認かのチェック */
            if (this.form.WindowType != WINDOW_TYPE.UPDATE_WINDOW_FLAG ||
                this.DenshiShinseiStatus == null ||
                !this.DenshiShinseiStatus.SHINSEI_STATUS_CD.Equals(SqlInt16.Parse(((short)DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL_CONF).ToString())) ||
                !SystemProperty.Shain.CD.Equals(this.form.SHINSEISHA_CD.Text))
            {
                val = false;
            }

            return val;
        }

        /// <summary>
        /// Header部の初期値を設定します
        /// </summary>
        private void SetHeaderInitData()
        {
            XMLAccessor fileAccess = new XMLAccessor();
            CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

            if (!string.IsNullOrEmpty(configProfile.ItemSetVal1))
            {
                IM_KYOTENDao kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                M_KYOTEN kyoten = kyotenDao.GetDataByCd(configProfile.ItemSetVal1);
                if (kyoten != null && !string.IsNullOrEmpty(kyoten.KYOTEN_NAME_RYAKU))
                {
                    this.headerForm.txtKyotenCd.Text = kyoten.KYOTEN_CD.ToString().PadLeft(2, '0');
                    this.headerForm.txtKyotenName.Text = kyoten.KYOTEN_NAME_RYAKU;
                }
            }
        }

        /// <summary>
        /// メインフォームの初期値を設定します
        /// </summary>
        private void SetMainFormInitData()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.SHINSEI_DATE.Text = parentForm.sysDate.ToString();

            this.form.SHINSEISHA_CD.Text = SystemProperty.Shain.CD;
            this.form.SHINSEISHA_NAME.Text = SystemProperty.Shain.Name;

            // 重要度マスタより初期値を設定
            IM_DENSHI_SHINSEI_JYUYOUDODao dao = DaoInitUtility.GetComponent<IM_DENSHI_SHINSEI_JYUYOUDODao>();
            M_DENSHI_SHINSEI_JYUYOUDO searchData = new M_DENSHI_SHINSEI_JYUYOUDO();
            searchData.JYUYOUDO_DEFAULT = true;
            M_DENSHI_SHINSEI_JYUYOUDO[] denshiShinseiJyuyoudo = dao.GetAllValidData(searchData);
            if (denshiShinseiJyuyoudo != null && denshiShinseiJyuyoudo.Length > 0)
            {
                // デフォルトは1件
                this.form.JYUUYOUDO_CD.Text = denshiShinseiJyuyoudo[0].JYUYOUDO_CD.ToString();
                this.form.JYUUYOUDO_NAME.Text = denshiShinseiJyuyoudo[0].JYUYOUDO_NAME;
            }
        }

        /// <summary>
        /// 初期化用データオブジェクト(InitDto)のデータで画面を設定します
        /// </summary>
        private void SetScreenByInitData()
        {
            if (this.InitDto != null)
            {
                this.form.HIKIAI_TORIHIKISAKI_CD.Text = this.InitDto.HikiaiTorihikisakiCd;
                this.form.HIKIAI_TORIHIKISAKI_NAME.Text = this.InitDto.HikiaiTorihikisakiNameRyaku;
                this.form.HIKIAI_GYOUSHA_CD.Text = this.InitDto.HikiaiGyoushaCd;
                this.form.HIKIAI_GYOUSHA_NAME.Text = this.InitDto.HikiaiGyoushaNameRyaku;
                this.form.HIKIAI_GENBA_CD.Text = this.InitDto.HikiaiGenbaCd;
                this.form.HIKIAI_GENBA_NAME.Text = this.InitDto.HikiaiGenbaNameRyaku;
                this.form.TORIHIKISAKI_CD.Text = this.InitDto.TorihikisakiCd;
                this.form.TORIHIKISAKI_NAME.Text = this.InitDto.TorihikisakiNameRyaku;
                this.form.GYOUSHA_CD.Text = this.InitDto.GyoushaCd;
                this.form.GYOUSHA_NAME.Text = this.InitDto.GyoushaNameRyaku;
                this.form.GENBA_CD.Text = this.InitDto.GenbaCd;
                this.form.GENBA_NAME.Text = this.InitDto.GenbaNameRyaku;
                this.form.SHINSEI_NAIYOU_CD.Text = this.InitDto.NaiyouCd;

                if(!string.IsNullOrEmpty(this.InitDto.NaiyouCd))
                {
                    var dao = DaoInitUtility.GetComponent<IM_DENSHI_SHINSEI_NAIYOUDao>();
                    var entity = dao.GetDataByCd(this.InitDto.NaiyouCd);
                    if (entity != null)
                    {
                        this.form.SHINSEI_NAIYOU_NAME.Text = entity.NAIYOU_NAME;
                    }
                }
            }
        }

        /// <summary>
        /// 保存データで画面を設定します
        /// </summary>
        private void SetScreenBySaveData()
        {
            try
            {
                if (this.SysId < 1 || this.Seq < 1)
                {
                    return;
                }

                /* T_DENSHI_SHINSEI_ENTRY */
                T_DENSHI_SHINSEI_ENTRY searchEntryData = new T_DENSHI_SHINSEI_ENTRY();
                searchEntryData.SYSTEM_ID = this.SysId;
                searchEntryData.SEQ = this.Seq;
                DataTable entryTable = this.denshiShinseiEntryDao.GetDataByKey(searchEntryData);

                /* T_DENSHI_SHINSEI_DETAIL */
                T_DENSHI_SHINSEI_DETAIL searcDetailData = new T_DENSHI_SHINSEI_DETAIL();
                searcDetailData.SYSTEM_ID = this.SysId;
                searcDetailData.SEQ = this.Seq;
                DataTable detailTable = this.denshiShinseiDetailDao.GetDataByKey(searcDetailData);

                if (entryTable != null)
                {
                    this.headerForm.txtKyotenCd.Text = (entryTable.Rows[0]["KYOTEN_CD"] == null) ? null : entryTable.Rows[0]["KYOTEN_CD"].ToString().PadLeft(2, '0');
                    this.headerForm.txtKyotenName.Text = (entryTable.Rows[0]["KYOTEN_NAME_RYAKU"] == null) ? null : entryTable.Rows[0]["KYOTEN_NAME_RYAKU"].ToString();
                    this.form.SHINSEI_NUMBER.Text = (entryTable.Rows[0]["SHINSEI_NUMBER"] == null) ? null : entryTable.Rows[0]["SHINSEI_NUMBER"].ToString();
                    this.form.SHINSEI_DATE.Value = (entryTable.Rows[0]["SHINSEI_DATE"] == null) ? SqlDateTime.Null : DateTime.Parse(entryTable.Rows[0]["SHINSEI_DATE"].ToString());
                    this.form.SHINSEISHA_CD.Text = (entryTable.Rows[0]["SHINSEISHA_CD"] == null) ? null : entryTable.Rows[0]["SHINSEISHA_CD"].ToString();
                    this.form.SHINSEISHA_NAME.Text = (entryTable.Rows[0]["SHAIN_NAME_RYAKU"] == null) ? null : entryTable.Rows[0]["SHAIN_NAME_RYAKU"].ToString();
                    this.form.JYUUYOUDO_CD.Text = (entryTable.Rows[0]["JYUYOUDO_CD"] == null) ? null : entryTable.Rows[0]["JYUYOUDO_CD"].ToString();
                    this.form.JYUUYOUDO_NAME.Text = (entryTable.Rows[0]["JYUYOUDO_NAME"] == null) ? null : entryTable.Rows[0]["JYUYOUDO_NAME"].ToString();
                    this.form.SHINSEI_NAIYOU_CD.Text = (entryTable.Rows[0]["NAIYOU_CD"] == null) ? null : entryTable.Rows[0]["NAIYOU_CD"].ToString();
                    this.form.SHINSEI_NAIYOU_NAME.Text = (entryTable.Rows[0]["NAIYOU_NAME"] == null) ? null : entryTable.Rows[0]["NAIYOU_NAME"].ToString();
                    this.form.STATUS.Text = (entryTable.Rows[0]["SHINSEI_STATUS"] == null) ? null : entryTable.Rows[0]["SHINSEI_STATUS"].ToString();
                    this.form.SHINSEI_KEIRO_CD.Text = (entryTable.Rows[0]["DENSHI_SHINSEI_ROUTE_CD"] == null) ? null : entryTable.Rows[0]["DENSHI_SHINSEI_ROUTE_CD"].ToString();
                    this.form.SHINSEI_KEIRO_NAME.Text = (entryTable.Rows[0]["DENSHI_SHINSEI_ROUTE_NAME"] == null) ? null : entryTable.Rows[0]["DENSHI_SHINSEI_ROUTE_NAME"].ToString();
                    this.form.SHINSEISHA_COMMENT.Text = (entryTable.Rows[0]["SHINSEISHA_COMMENT"] == null) ? null : entryTable.Rows[0]["SHINSEISHA_COMMENT"].ToString();
                    this.form.MITSUMORISHO_PATH.Text = (entryTable.Rows[0]["MITSUMORI_TENPU"] == null) ? null : entryTable.Rows[0]["MITSUMORI_TENPU"].ToString();
                    this.form.HIKIAI_TORIHIKISAKI_CD.Text = (entryTable.Rows[0]["HIKIAI_TORIHIKISAKI_CD"] == null) ? null : entryTable.Rows[0]["HIKIAI_TORIHIKISAKI_CD"].ToString();
                    this.form.HIKIAI_TORIHIKISAKI_NAME.Text = (entryTable.Rows[0]["HIKIAI_TORIHIKISAKI_NAME"] == null) ? null : entryTable.Rows[0]["HIKIAI_TORIHIKISAKI_NAME"].ToString();
                    this.form.HIKIAI_GYOUSHA_CD.Text = (entryTable.Rows[0]["HIKIAI_GYOUSHA_CD"] == null) ? null : entryTable.Rows[0]["HIKIAI_GYOUSHA_CD"].ToString();
                    this.form.HIKIAI_GYOUSHA_NAME.Text = (entryTable.Rows[0]["HIKIAI_GYOUSHA_NAME"] == null) ? null : entryTable.Rows[0]["HIKIAI_GYOUSHA_NAME"].ToString();
                    this.form.HIKIAI_GENBA_CD.Text = (entryTable.Rows[0]["HIKIAI_GENBA_CD"] == null) ? null : entryTable.Rows[0]["HIKIAI_GENBA_CD"].ToString();
                    this.form.HIKIAI_GENBA_NAME.Text = (entryTable.Rows[0]["HIKIAI_GENBA_NAME"] == null) ? null : entryTable.Rows[0]["HIKIAI_GENBA_NAME"].ToString();
                    this.form.TORIHIKISAKI_CD.Text = (entryTable.Rows[0]["TORIHIKISAKI_CD"] == null) ? null : entryTable.Rows[0]["TORIHIKISAKI_CD"].ToString();
                    this.form.TORIHIKISAKI_NAME.Text = (entryTable.Rows[0]["TORIHIKISAKI_NAME"] == null) ? null : entryTable.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                    this.form.GYOUSHA_CD.Text = (entryTable.Rows[0]["GYOUSHA_CD"] == null) ? null : entryTable.Rows[0]["GYOUSHA_CD"].ToString();
                    this.form.GYOUSHA_NAME.Text = (entryTable.Rows[0]["GYOUSHA_NAME"] == null) ? null : entryTable.Rows[0]["GYOUSHA_NAME"].ToString();
                    this.form.GENBA_CD.Text = (entryTable.Rows[0]["GENBA_CD"] == null) ? null : entryTable.Rows[0]["GENBA_CD"].ToString();
                    this.form.GENBA_NAME.Text = (entryTable.Rows[0]["GENBA_NAME"] == null) ? null : entryTable.Rows[0]["GENBA_NAME"].ToString();

                    // 承認・否認・否認確認操作時用に電子申請状態のデータを保持しておく
                    T_DENSHI_SHINSEI_STATUS denshiShinseiStatus = new T_DENSHI_SHINSEI_STATUS();
                    denshiShinseiStatus.SYSTEM_ID = SqlInt64.Parse(entryTable.Rows[0]["SYSTEM_ID"].ToString());
                    denshiShinseiStatus.SEQ = SqlInt32.Parse(entryTable.Rows[0]["SEQ"].ToString());
                    denshiShinseiStatus.UPDATE_NUM = SqlInt16.Parse(entryTable.Rows[0]["UPDATE_NUM"].ToString());
                    denshiShinseiStatus.SHINSEI_STATUS_CD = SqlInt16.Parse(entryTable.Rows[0]["SHINSEI_STATUS_CD"].ToString());
                    denshiShinseiStatus.SHINSEI_STATUS = entryTable.Rows[0]["SHINSEI_STATUS"].ToString();
                    denshiShinseiStatus.CREATE_USER = entryTable.Rows[0]["STATUS_CREATE_USER"].ToString();
                    denshiShinseiStatus.CREATE_DATE = SqlDateTime.Parse(entryTable.Rows[0]["STATUS_CREATE_DATE"].ToString());
                    denshiShinseiStatus.CREATE_PC = entryTable.Rows[0]["STATUS_CREATE_PC"].ToString();
                    denshiShinseiStatus.TIME_STAMP = (byte[])entryTable.Rows[0]["STATUS_TIME_STAMP"];
                    this.DenshiShinseiStatus = denshiShinseiStatus;
                }

                if (detailTable != null)
                {
                    this.form.DETAIL.Rows.Clear();
                    for (int i = 0; i < detailTable.Rows.Count; i++)
                    {
                        this.form.DETAIL.Rows.Add();
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_NO].Value = detailTable.Rows[i]["ROW_NO"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_BUSHOCD].Value = detailTable.Rows[i]["BUSHO_CD"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_BUSHONAME].Value = detailTable.Rows[i]["BUSHO_NAME_RYAKU"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_SHAINCD].Value = detailTable.Rows[i]["SHAIN_CD"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_SHAINNAME].Value = detailTable.Rows[i]["SHAIN_NAME_RYAKU"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_KAKUNINDATE].Value = detailTable.Rows[i]["CHECK_DATE"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_KESSAI].Value = detailTable.Rows[i]["KESSAI"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_COMMENT].Value = detailTable.Rows[i]["ACTION_COMMENT"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_DETAILSYSTEMID].Value = detailTable.Rows[i]["DETAIL_SYSTEM_ID"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_SEQ].Value = detailTable.Rows[i]["SEQ"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_SHINSEINUMBER].Value = detailTable.Rows[i]["SHINSEI_NUMBER"];
                        this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_ROWNO].Value = detailTable.Rows[i]["ROW_NO"];
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw;
            }
        }

        #endregion

        #region IME制御(DETAIL)

        /// <summary>
        /// DETAILコントロールのIME制御を行います
        /// </summary>
        /// <param name="index">CellIndex</param>
        public void ChangeDetailImeMode(int index)
        {
            string cellName = this.form.DETAIL.Columns[index].Name;
            switch (cellName)
            {
                case DETAIL_CELL_NAME_BUSHOCD:
                case DETAIL_CELL_NAME_SHAINCD:
                    this.form.DETAIL.ImeMode = ImeMode.Disable;
                    break;
                default:
                    // Nothing
                    break;
            }
        }

        #endregion

        #region 明細No列再設定

        /// <summary>
        /// DETAILのNo列を再設定します
        /// </summary>
        private void DetailRowNumberReset()
        {
            int rowCount = this.form.DETAIL.Rows.Count;
            for (int i = 0; i < rowCount - 1; i++)
            {
                this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_NO].Value = i + 1;
            }
        }

        #endregion

        #region 最新未承認明細行検索

        /// <summary>
        /// 最新未承認明細行検索し該当行のIndexを取得します
        /// </summary>
        /// <returns>存在する場合は0以上の値、存在しない場合はマイナス値</returns>
        private int SearchLatestUnapprovedDeatailRow()
        {
            int val = -1;
            foreach (DataGridViewRow row in this.form.DETAIL.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells[DETAIL_CELL_NAME_KESSAI].Value == null ||
                    row.Cells[DETAIL_CELL_NAME_KESSAI].Value.ToString().Equals(string.Empty))
                {
                    val = row.Index;
                    break;
                }
            }
            return val;
        }

        #endregion

        #region DETAIL - CELL ENTER EVENT処理

        /// <summary>
        /// DETAIL - CELL_ENTER_EVENT
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        internal void CellEnter(int rowIndex, int columnIndex)
        {
            try
            {
                string cellName = this.form.DETAIL.Columns[columnIndex].Name;
                var row = this.form.DETAIL.Rows[rowIndex];

                if (string.IsNullOrEmpty(cellName) || row == null)
                {
                    return;
                }

                switch (cellName)
                {
                    case DETAIL_CELL_NAME_BUSHOCD:
                        this.beforeBushoCd = (row.Cells[DETAIL_CELL_NAME_BUSHOCD].FormattedValue != null) ? row.Cells[DETAIL_CELL_NAME_BUSHOCD].FormattedValue.ToString() : string.Empty;
                        break;
                    default:
                        // Nothing
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CellEnter", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        #endregion

        #region CELLの値検証
        /// <summary>
        /// セルの値検証
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns>true:正常、false:異常</returns>
        internal bool CellValueValidating(int rowIndex, int columnIndex, out bool catchErr)
        {
            bool returnVal = true;
            catchErr = false;
            try
            {
                string cellName = this.form.DETAIL.Columns[columnIndex].Name;
                var row = this.form.DETAIL.Rows[rowIndex];

                if (string.IsNullOrEmpty(cellName) || row == null)
                {
                    return returnVal;
                }

                var bushoCd = (row.Cells[LogicClass.DETAIL_CELL_NAME_BUSHOCD].FormattedValue != null) ? Convert.ToString(row.Cells[LogicClass.DETAIL_CELL_NAME_BUSHOCD].FormattedValue) : string.Empty;
                var shainCd = (row.Cells[LogicClass.DETAIL_CELL_NAME_SHAINCD].FormattedValue != null) ? Convert.ToString(row.Cells[LogicClass.DETAIL_CELL_NAME_SHAINCD].FormattedValue) : string.Empty;

                switch (cellName)
                {
                    case DETAIL_CELL_NAME_BUSHOCD:

                        if (!bushoCd.Equals(beforeBushoCd))
                        {
                            // 部署と社員は関連付いていないといけないので社員の情報をクリアする
                            row.Cells[LogicClass.DETAIL_CELL_NAME_SHAINCD].Value = string.Empty;
                            row.Cells[LogicClass.DETAIL_CELL_NAME_SHAINNAME].Value = string.Empty;
                        }

                        if (CommonConst.BUSHO_CD_ZENBUSHO.Equals(bushoCd))
                        {
                            MessageBoxUtility.MessageBoxShow("E028");
                            returnVal = false;
                        }

                        break;

                    case DETAIL_CELL_NAME_SHAINCD:

                        if (!this.CheckShainCd(shainCd, bushoCd))
                        {
                            returnVal = false;
                        }
                        else if (string.IsNullOrEmpty(bushoCd))
                        {
                            // 部署CDがｶﾗなら、部署CDと部署名をセットする
                            if (!this.GetBushoData())
                            {
                                returnVal = false;
                                catchErr = true;
                            }
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CellValueValidating", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                returnVal = false;
                catchErr = true;
            }
            finally
            {
            }
            return returnVal;
        }
        #endregion

        #region 申請経路マスタ読み込み

        public void RouteDataSet(string routeCd)
        {
            try
            {
                this.form.SHINSEI_KEIRO_NAME.Text = string.Empty;

                if (!string.IsNullOrEmpty(routeCd))
                {
                    // マスタ存在チェック
                    M_DENSHI_SHINSEI_ROUTE_NAME entity = new M_DENSHI_SHINSEI_ROUTE_NAME();
                    entity.DENSHI_SHINSEI_ROUTE_CD = routeCd;
                    var denshiShinseiRoute = this.denshiShinseiRouteNameDao.GetAllValidData(entity);
                    if (denshiShinseiRoute == null || denshiShinseiRoute.Length < 1)
                    {
                        // マスタに存在しない値
                        this.form.SHINSEI_KEIRO_CD.IsInputErrorOccured = true;
                        this.form.SHINSEI_KEIRO_CD.UpdateBackColor();
                        this.MsgBox.MessageBoxShow("E020", "申請経路名");
                        this.isInputError = true;
                        this.form.SHINSEI_KEIRO_CD.Focus();
                        return;
                    }
                    else
                    {
                        this.form.SHINSEI_KEIRO_NAME.Text = denshiShinseiRoute[0].DENSHI_SHINSEI_ROUTE_NAME;
                    }
                }

                DataTable dataTable = this.RouteDataLoad(routeCd);

                if (dataTable == null || dataTable.Rows.Count == 0) return;

                if ((string.IsNullOrEmpty(this.form.beforeShinseiKeiroCd))
                    || DialogResult.Yes == MessageBoxUtility.MessageBoxShow("C045", "申請経路名", "申請経路マスタの値で上書き"))
                {
                    this.DetailCreateByRouteData(dataTable);
                }

                this.isInputError = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RouteDataSet", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 申請経路マスタのデータを取得します
        /// </summary>
        /// <param name="routeCd">申請経路名CD</param>
        /// <returns>申請経路マスタ検索結果のDataTable</returns>
        private DataTable RouteDataLoad(string routeCd)
        {
            if (string.IsNullOrEmpty(routeCd))
            {
                return null;
            }

            string sql = CreateDenshiShinseiRouteSQL(routeCd);
            DataTable data = this.denshiShinseiRouteDao.GetDateForStringSql(sql);
            return data;
        }

        /// <summary>
        /// 申請経路マスタ検索SQLを生成します
        /// </summary>
        /// <param name="routeCd">申請経路CD</param>
        /// <returns>申請経路マスタ検索SQL</returns>
        private string CreateDenshiShinseiRouteSQL(string routeCd)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT ");
            sql.Append("DSR.DENSHI_SHINSEI_ROUTE_CD ");
            sql.Append(",DSR.DENSHI_SHINSEI_ROW_NO ");
            sql.Append(",DSR.BUSHO_CD ");
            sql.Append(",BU.BUSHO_NAME_RYAKU ");
            sql.Append(",DSR.SHAIN_CD ");
            sql.Append(",SH.SHAIN_NAME_RYAKU ");

            sql.Append("FROM ");
            sql.Append("M_DENSHI_SHINSEI_ROUTE DSR ");
            sql.Append("LEFT JOIN M_BUSHO BU ON BU.BUSHO_CD = DSR.BUSHO_CD ");
            sql.Append("LEFT JOIN M_SHAIN SH ON SH.SHAIN_CD = DSR.SHAIN_CD ");

            sql.Append("WHERE ");
            sql.Append("DSR.DELETE_FLG = 0 ");
            sql.Append("AND DSR.DENSHI_SHINSEI_ROUTE_CD = " + routeCd + " ");

            sql.Append("ORDER BY DSR.DENSHI_SHINSEI_ROW_NO");

            return sql.ToString();
        }

        /// <summary>
        /// 申請経路マスタのデータを元にDETAILを作成します
        /// </summary>
        /// <param name="dataTable">申請経路マスタ検索結果</param>
        private void DetailCreateByRouteData(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0) return;

            this.form.DETAIL.Rows.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                this.form.DETAIL.Rows.Add();
                this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_NO].Value = i + 1;
                this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_BUSHOCD].Value = row["BUSHO_CD"];
                this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_BUSHONAME].Value = row["BUSHO_NAME_RYAKU"];
                this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_SHAINCD].Value = row["SHAIN_CD"];
                this.form.DETAIL.Rows[i].Cells[DETAIL_CELL_NAME_SHAINNAME].Value = row["SHAIN_NAME_RYAKU"];
            }
        }

        #endregion

        #region RegistCheck(申請)

        /// <summary>
        /// 申請イベント時の必須チェックを行います
        /// </summary>
        private void RegistCheck()
        {
            //this.headerForm.txtKyotenCd.IsInputErrorOccured = false;
            var autoCheckLogic = new AutoRegistCheckLogic(this.form.GetAllControl(), this.form.GetAllControl());
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (this.form.RegistErrorFlag)
            {
                this.SetErrorFocus();
            }

            // DETAILチェック
            if (!this.form.RegistErrorFlag)
            {
                bool isCheck = RegistCheckDetail();
                if (!isCheck)
                {
                    this.form.RegistErrorFlag = true;
                }
            }
        }

        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        private void SetErrorFocus()
        {
            Control target = null;
            foreach (Control control in this.form.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        if (target != null)
                        {
                            if (target.TabIndex > control.TabIndex)
                            {
                                target = control;
                            }
                        }
                        else
                        {
                            target = control;
                        }
                    }
                }
            }
            //ヘッダーチェック
            foreach (Control control in this.headerForm.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        target = control;
                    }
                }
            }
            if (target != null)
            {
                target.Focus();
            }
        }

        /// <summary>
        /// 申請イベント時の申請経路(DETAIL)の必須チェックを行います
        /// </summary>
        /// <returns>正常：True　エラー：False</returns>
        private bool RegistCheckDetail()
        {
            bool val = true;

            string shinseiShainCd = this.form.SHINSEISHA_CD.Text;
            int rowCount = this.form.DETAIL.Rows.Count;
            if (rowCount <= 1)
            {
                /* 申請経路未入力チェック */
                MessageBoxUtility.MessageBoxShow("E001", "明細行");
                val = false;
            }
            else
            {
                /* 申請経路に申請者以外が存在するかチェック */
                bool isExist = false;
                string shainCd = this.form.SHINSEISHA_CD.Text;
                foreach (DataGridViewRow row in this.form.DETAIL.Rows)
                {
                    if (row.Cells[DETAIL_CELL_NAME_SHAINCD].Value != null &&
                        !row.Cells[DETAIL_CELL_NAME_SHAINCD].Value.ToString().Equals(shainCd))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (!isExist)
                {
                    MessageBoxUtility.MessageBoxShow("E169", "申請経路が不正", "申請");
                    val = false;
                }
            }

            return val;
        }

        #endregion

        #region EntityDataSet(申請)

        /// <summary>
        /// 画面表示情報から各返却用Entityデータを作成します
        /// </summary>
        private void SetData()
        {
            /* T_DENSHI_SHINSEI_ENTRY */
            this.DenshiShinseiEntry.KYOTEN_CD = SqlInt16.Parse(this.headerForm.txtKyotenCd.Text);
            this.DenshiShinseiEntry.SHINSEI_DATE = SqlDateTime.Parse(this.form.SHINSEI_DATE.Text.Substring(0, 10));
            this.DenshiShinseiEntry.SHINSEISHA_CD = this.form.SHINSEISHA_CD.Text;
            this.DenshiShinseiEntry.JYUYOUDO_CD = this.form.JYUUYOUDO_CD.Text;
            this.DenshiShinseiEntry.NAIYOU_CD = SqlInt16.Parse(this.form.SHINSEI_NAIYOU_CD.Text);
            this.DenshiShinseiEntry.DENSHI_SHINSEI_ROUTE_CD = this.form.SHINSEI_KEIRO_CD.Text;
            this.DenshiShinseiEntry.SHINSEISHA_COMMENT = this.form.SHINSEISHA_COMMENT.Text;
            this.DenshiShinseiEntry.MITSUMORI_TENPU = this.form.MITSUMORISHO_PATH.Text;
            this.DenshiShinseiEntry.HIKIAI_TORIHIKISAKI_CD = this.form.HIKIAI_TORIHIKISAKI_CD.Text;
            this.DenshiShinseiEntry.HIKIAI_GYOUSHA_CD = this.form.HIKIAI_GYOUSHA_CD.Text;
            this.DenshiShinseiEntry.HIKIAI_GENBA_CD = this.form.HIKIAI_GENBA_CD.Text;
            this.DenshiShinseiEntry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            this.DenshiShinseiEntry.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            this.DenshiShinseiEntry.GENBA_CD = this.form.GENBA_CD.Text;

            /* T_DENSHI_SHINSEI_DETAIL */
            // DenshiShinseiDetailListは起動元の画面の方で、Insertするため、参照渡しとなっている。
            // そのため、参照が切れないようにnew等はしない
            this.DenshiShinseiDetailList.Clear();
            foreach (DataGridViewRow row in this.form.DETAIL.Rows)
            {
                if (!row.IsNewRow)
                {
                    T_DENSHI_SHINSEI_DETAIL data = new T_DENSHI_SHINSEI_DETAIL();
                    data.ROW_NO = SqlInt16.Parse(row.Cells[DETAIL_CELL_NAME_NO].Value.ToString());
                    data.BUSHO_CD = row.Cells[DETAIL_CELL_NAME_BUSHOCD].Value.ToString();
                    data.SHAIN_CD = row.Cells[DETAIL_CELL_NAME_SHAINCD].Value.ToString();
                    this.DenshiShinseiDetailList.Add(data);
                }
            }

            return;
        }

        #endregion

        #region 承認・否認

        /// <summary>
        /// 承認・否認登録処理
        /// </summary>
        /// <param name="status"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        private bool KessaiRegist(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS status, string comment)
        {
            LogUtility.DebugMethodStart(status, comment);

            int index = this.SearchLatestUnapprovedDeatailRow();
            DataGridViewRow row = this.form.DETAIL.Rows[index];

            /* T_DENSHI_SHINSEI_DETAIL_ACTION */
            T_DENSHI_SHINSEI_DETAIL_ACTION insertActionData = CreateDetailActionEntity(status, comment, row);
            if (this.IsExistDetailActionRecord(insertActionData.DETAIL_SYSTEM_ID.ToString(), insertActionData.SEQ.ToString()))
            {
                // レコードが存在する場合は別画面(多重起動、別PC)から更新されているため、エラー
                MessageBoxUtility.MessageBoxShow("E080");
                return false;
            }

            /* T_DENSHI_SHINSEI_STATUS */
            T_DENSHI_SHINSEI_STATUS insertStatusData = null;
            T_DENSHI_SHINSEI_STATUS updateStatusData = null;
            if (index == (this.form.DETAIL.Rows.Count - 1) && status == DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL)
            {
                // 承認経路最終ユーザの承認時は電子申請状態テーブルの状態を「承認」に更新するため電子申請状態テーブルInsert用Entity作成
                insertStatusData = this.CreateInsertStatusEntity(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL);
            }
            else if (status == DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL)
            {
                // 否認の場合は電子申請状態テーブルの状態を「否認確認」に更新するため電子申請状態テーブルInsert用Entity作成
                insertStatusData = this.CreateInsertStatusEntity(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL_CONF);
            }

            if (insertStatusData != null)
            {
                // 電子申請状態テーブルの最新データ論理削除用Entity作成
                updateStatusData = this.AddSysPropatiesForStatusEntity(this.DenshiShinseiStatus);
            }

            try
            {
                IT_DENSHI_SHINSEI_DETAIL_ACTIONDao actionDao = DaoInitUtility.GetComponent<IT_DENSHI_SHINSEI_DETAIL_ACTIONDao>();
                IT_DENSHI_SHINSEI_STATUSDao statusDao = DaoInitUtility.GetComponent<IT_DENSHI_SHINSEI_STATUSDao>();

                using (Transaction tran = new Transaction())
                {
                    if (insertActionData != null)
                    {
                        actionDao.Insert(insertActionData);
                    }

                    if (updateStatusData != null && insertStatusData != null)
                    {
                        statusDao.Update(updateStatusData);
                        statusDao.Insert(insertStatusData);
                    }

                    tran.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    //排他エラー
                    MessageBoxUtility.MessageBoxShow("E080");
                    return false;
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 否認確認登録処理
        /// </summary>
        /// <returns></returns>
        private bool DenialConfirmationRegist()
        {
            LogUtility.DebugMethodStart();

            bool returnval = false;

            try
            {
                /* T_DENSHI_SHINSEI_STATUS */
                // 否認確認 ⇒ 否認
                T_DENSHI_SHINSEI_STATUS insertStatusData = this.CreateInsertStatusEntity(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL);
                // 旧データ論理削除用
                T_DENSHI_SHINSEI_STATUS updateStatusData = this.AddSysPropatiesForStatusEntity(this.DenshiShinseiStatus); ;

                IT_DENSHI_SHINSEI_STATUSDao statusDao = DaoInitUtility.GetComponent<IT_DENSHI_SHINSEI_STATUSDao>();
                using (Transaction tran = new Transaction())
                {
                    statusDao.Update(updateStatusData);
                    statusDao.Insert(insertStatusData);
                    tran.Commit();
                }

                returnval = true;
                return returnval;
            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    //排他エラー
                    LogUtility.Error(ex);
                    MessageBoxUtility.MessageBoxShow("E080");
                    return returnval;
                }
                else
                {
                    LogUtility.Fatal(ex);
                    throw;
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnval);
            }
        }

        #region Insert用Entity作成

        /// <summary>
        /// 電子申請明細承認否認Insert用Entityを作成します
        /// </summary>
        /// <param name="status">承認or否認</param>
        /// <param name="comment">決裁コメント</param>
        /// <param name="row">更新対象のDETAIL行</param>
        /// <returns>Insert用Entity</returns>
        private T_DENSHI_SHINSEI_DETAIL_ACTION CreateDetailActionEntity(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS status, string comment, DataGridViewRow row)
        {
            if (row == null) return null;

            T_DENSHI_SHINSEI_DETAIL_ACTION data = new T_DENSHI_SHINSEI_DETAIL_ACTION();
            data.DETAIL_SYSTEM_ID = SqlInt64.Parse(row.Cells[DETAIL_CELL_NAME_DETAILSYSTEMID].Value.ToString());
            data.SEQ = 1;
            data.SHINSEI_NUMBER = SqlInt64.Parse(row.Cells[DETAIL_CELL_NAME_SHINSEINUMBER].Value.ToString());
            data.ROW_NO = SqlInt16.Parse(row.Cells[DETAIL_CELL_NAME_ROWNO].Value.ToString());
            switch (status)
            {
                case DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL:
                    data.ACTION_FLG = 1;
                    break;
                case DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL:
                    data.ACTION_FLG = 2;
                    break;
                default:
                    // Nothing
                    break;
            }
            data.ACTION_COMMENT = string.IsNullOrEmpty(comment) ? null : comment;

            DataBinderLogic<T_DENSHI_SHINSEI_DETAIL_ACTION> logic = new DataBinderLogic<T_DENSHI_SHINSEI_DETAIL_ACTION>(data);
            logic.SetSystemProperty(data, false);

            data.CHECK_DATE = data.UPDATE_DATE;

            return data;
        }

        /// <summary>
        /// 電子申請状態Insert用Entityを作成します
        /// </summary>
        /// <param name="status">承認or否認</param>
        /// <returns>Insert用Entity</returns>
        private T_DENSHI_SHINSEI_STATUS CreateInsertStatusEntity(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS status)
        {
            T_DENSHI_SHINSEI_STATUS data = new T_DENSHI_SHINSEI_STATUS();
            data.SYSTEM_ID = this.DenshiShinseiStatus.SYSTEM_ID;
            data.SEQ = this.DenshiShinseiStatus.SEQ;
            data.UPDATE_NUM = this.DenshiShinseiStatus.UPDATE_NUM + 1;
            data.SHINSEI_STATUS_CD = (short)status;
            DenshiShinseiUtility util = new DenshiShinseiUtility();
            data.SHINSEI_STATUS = util.ToString(status);

            DataBinderLogic<T_DENSHI_SHINSEI_STATUS> logic = new DataBinderLogic<T_DENSHI_SHINSEI_STATUS>(data);
            logic.SetSystemProperty(data, false);

            return data;
        }

        /// <summary>
        /// 指定されたEntityに論理削除用システム項目を付与します
        /// </summary>
        /// <param name="data">削除用Entity</param>
        /// <returns>削除用項目データを設定したEntity</returns>
        private T_DENSHI_SHINSEI_STATUS AddSysPropatiesForStatusEntity(T_DENSHI_SHINSEI_STATUS data)
        {
            string createUser = data.CREATE_USER;
            string createPc = data.CREATE_PC;
            SqlDateTime createDate = data.CREATE_DATE;

            DataBinderLogic<T_DENSHI_SHINSEI_STATUS> logic = new DataBinderLogic<T_DENSHI_SHINSEI_STATUS>(data);
            logic.SetSystemProperty(data, false);

            data.CREATE_USER = createUser;
            data.CREATE_PC = createPc;
            data.CREATE_DATE = createDate;
            data.DELETE_FLG = SqlBoolean.True;

            return data;
        }

        #endregion

        #region レコード存在チェック(電子申請明細承認否認)

        /// <summary>
        /// 電子申請明細承認否認テーブルに指定したキーに合致するデータが存在するかを確認します
        /// </summary>
        /// <param name="detailSystemId">DETAIL_SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>存在する：True　存在しない：False</returns>
        private bool IsExistDetailActionRecord(string detailSystemId, string seq)
        {
            bool val = false;

            T_DENSHI_SHINSEI_DETAIL_ACTION data = this.denshiShinseiDetailActionDao.GetDataByKey(detailSystemId, seq);
            if (data != null)
            {
                val = true;
            }

            return val;
        }

        #endregion

        #endregion

        #region 見積書参照
        /// <summary>
        /// ファイル選択ダイアログを表示し、見積書添付にパスを設定する。
        /// </summary>
        private void FileRefClick()
        {
            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var title = "参照するファイルを選択してください。";
            var initialPath = @"C:\Temp";
            var windowHandle = this.form.Handle;
            var isFileSelect = true;
            var isTerminalMode = SystemProperty.IsTerminalMode;
            var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

            browserForFolder = null;

            if (false == String.IsNullOrEmpty(filePath))
            {
                this.form.MITSUMORISHO_PATH.Text = filePath;
            }
        }
        #endregion

        #region 見積書閲覧
        /// <summary>
        /// 見積書添付で指定したファイルを閲覧する
        /// </summary>
        public void BrowseClick()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.form.MITSUMORISHO_PATH.Text))
                {
                    if (SystemProperty.IsTerminalMode)
                    {
                        if (string.IsNullOrEmpty(Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectoryNonMsg()))
                        {
                            MessageBox.Show("閲覧を行う前に、印刷設定の出力先フォルダを指定してください。",
                                            "アラート",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                            return;
                        }
                        // クラウド環境でもオンプレと同じようにプロセス起動する
                        string clientFilePathInfo = Path.Combine(Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectory(), "ClientFilePathInfo.txt");
                        
                        // 5回ファイル作成を試す
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                using (var writer = new StreamWriter(clientFilePathInfo, false, Encoding.UTF8))
                                {
                                    writer.Write(this.form.MITSUMORISHO_PATH.Text);
                                }
                                break;
                            }
                            catch (Exception e)
                            {
                                System.Threading.Thread.Sleep(100);
                                continue;
                            }
                        }
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(this.form.MITSUMORISHO_PATH.Text);
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                // ファイルが存在しない場合にはアラート表示
                LogUtility.Error("見積書添付 参照ボタン", ex);
                MessageBoxUtility.MessageBoxShowError(ex.Message);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }
        #endregion

        #region 内容確認用マスタ表示処理
        /// <summary>
        /// 電子申請対象のマスタ情報を表示する。
        /// </summary>
        private void ShowReferenceDisplay()
        {
            LogUtility.DebugMethodStart();

            if (!string.IsNullOrEmpty(this.form.HIKIAI_GENBA_CD.Text)
                || !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                bool hikiaFlg = false;
                string gyoushaCd = string.Empty;
                string genbaCd = string.Empty;
                bool useKariData = false;

                // 現場
                if (!string.IsNullOrEmpty(this.form.HIKIAI_GENBA_CD.Text)
                    && !string.IsNullOrEmpty(this.form.HIKIAI_GYOUSHA_CD.Text))
                {
                    // 引合現場(引合業者)
                    hikiaFlg = true;
                    gyoushaCd = this.form.HIKIAI_GYOUSHA_CD.Text;
                    genbaCd = this.form.HIKIAI_GENBA_CD.Text;
                }
                else if (!string.IsNullOrEmpty(this.form.HIKIAI_GENBA_CD.Text)
                    && !string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    // 引合現場(既存業者)
                    hikiaFlg = false;
                    gyoushaCd = this.form.GYOUSHA_CD.Text;
                    genbaCd = this.form.HIKIAI_GENBA_CD.Text;
                }
                else if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    // 既存現場
                    gyoushaCd = this.form.GYOUSHA_CD.Text;
                    genbaCd = this.form.GENBA_CD.Text;
                    useKariData = true;
                }
                FormManager.OpenForm("G614", WINDOW_TYPE.NONE, hikiaFlg, gyoushaCd, genbaCd, useKariData);
            }
            else if (!string.IsNullOrEmpty(this.form.HIKIAI_GYOUSHA_CD.Text))
            {
                // 引合業者
                FormManager.OpenForm("G613", WINDOW_TYPE.NONE, this.form.HIKIAI_GYOUSHA_CD.Text, 1);
            }
            else if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                // 既存業者
                FormManager.OpenForm("G613", WINDOW_TYPE.NONE, this.form.GYOUSHA_CD.Text, 0);
            }
            else if (!string.IsNullOrEmpty(this.form.HIKIAI_TORIHIKISAKI_CD.Text))
            {
                // 引合取引先
                FormManager.OpenForm("G612", WINDOW_TYPE.NONE, this.form.HIKIAI_TORIHIKISAKI_CD.Text, 1);
            }
            else if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                // 引合取引先
                FormManager.OpenForm("G612", WINDOW_TYPE.NONE, this.form.TORIHIKISAKI_CD.Text, 0);
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 社員検索用DataSourceセット
        /// <summary>
        /// 社員検索ポップアップ用のDataSourceをセットする
        /// </summary>
        internal void SetShainPopupProperty()
        {
            try
            {
                var shainCd = this.form.DETAIL.CurrentRow.Cells[LogicClass.DETAIL_CELL_NAME_SHAINCD] as DgvCustomTextBoxCell;
                var bushoCd = this.form.DETAIL.CurrentRow.Cells[LogicClass.DETAIL_CELL_NAME_BUSHOCD] as DgvCustomTextBoxCell;
                if (shainCd != null)
                {
                    shainCd.PopupWindowId = WINDOW_ID.M_SHAIN;
                    shainCd.PopupWindowName = "マスタ共通ポップアップ";
                    shainCd.PopupGetMasterField = "SHAIN_CD, SHAIN_NAME";
                    shainCd.PopupSetFormField = string.Format("{0}, {1}"
                            , LogicClass.DETAIL_CELL_NAME_SHAINCD, LogicClass.DETAIL_CELL_NAME_SHAINNAME);
                    shainCd.PopupDataHeaderTitle = new string[] { "社員CD", "社員名" };
                    string strBushoCd = (bushoCd != null) ? Convert.ToString(bushoCd.Value) : string.Empty;
                    shainCd.PopupDataSource = this.CreateShianDataSource(strBushoCd
                        , shainCd.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()).ToArray());
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShainPopupProperty", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 社員検索用DataSourceを生成する
        /// </summary>
        /// <param name="bushoCd">部署CD</param>
        /// <param name="dispColumn">表示カラム</param>
        /// <returns></returns>
        private DataTable CreateShianDataSource(string bushoCd, string[] dispColumn)
        {
            var returnVal = new DataTable();

            var allShainData = DaoInitUtility.GetComponent<IM_SHAINDao>().GetAllValidData(new M_SHAIN());
            var searchShainData = allShainData.Where(w => !string.IsNullOrWhiteSpace(w.LOGIN_ID));
            // 部署があれば絞込み条件に加える
            if (!string.IsNullOrEmpty(bushoCd))
            {
                searchShainData = searchShainData.Where(w => w.BUSHO_CD.Equals(bushoCd));
            }

            var dt = EntityUtility.EntityToDataTable(searchShainData.ToArray());

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (var col in dispColumn)
                {
                    // 表示対象の列だけを順番に追加
                    returnVal.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
                }

                foreach (DataRow r in dt.Rows)
                {
                    returnVal.Rows.Add(returnVal.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }

            }

            return returnVal;
        }
        #endregion

        #region 部署CD、部署名取得
        /// <summary>
        /// 社員CDから関連する部署CD、部署名を取得し明細行にセットする
        /// </summary>
        internal bool GetBushoData()
        {
            bool ret = true;
            try
            {
                var shainCd = this.form.DETAIL.CurrentRow.Cells[LogicClass.DETAIL_CELL_NAME_SHAINCD] as DgvCustomTextBoxCell;
                var bushoCd = this.form.DETAIL.CurrentRow.Cells[LogicClass.DETAIL_CELL_NAME_BUSHOCD] as DgvCustomTextBoxCell;
                var bushoName = this.form.DETAIL.CurrentRow.Cells[LogicClass.DETAIL_CELL_NAME_BUSHONAME] as DgvCustomTextBoxCell;
                if (shainCd != null
                    && !string.IsNullOrEmpty(Convert.ToString(shainCd.Value)))
                {
                    string sql = "SELECT M_BUSHO.BUSHO_CD, M_BUSHO.BUSHO_NAME_RYAKU "
                                + "FROM M_SHAIN LEFT JOIN M_BUSHO ON M_SHAIN.BUSHO_CD = M_BUSHO.BUSHO_CD "
                                + "WHERE M_SHAIN.SHAIN_CD = '" + shainCd.Value.ToString() + "'";
                    DataTable bushoData = DaoInitUtility.GetComponent<IM_SHAINDao>().GetDateForStringSql(sql);

                    if (bushoData != null
                        && bushoData.Rows.Count > 0)
                    {
                        string strBushoCd = (bushoData.Rows[0]["BUSHO_CD"] != null) ? bushoData.Rows[0]["BUSHO_CD"].ToString() : string.Empty;
                        string strBushoName = (bushoData.Rows[0]["BUSHO_CD"] != null) ? bushoData.Rows[0]["BUSHO_NAME_RYAKU"].ToString() : string.Empty;
                        bushoCd.SetResultText(strBushoCd);
                        bushoName.SetResultText(strBushoName);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetBushoData", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
            }
            return ret;
        }
        #endregion

        #region 社員CDチェック
        /// <summary>
        /// 社員CDチェック
        /// メッセージついてはこのメソッド内で表示する。
        /// </summary>
        /// <param name="shainCd"></param>
        /// <param name="bushoCd"></param>
        /// <returns>true:正常な社員CD、false:不正な社員CD</returns>
        private bool CheckShainCd(string shainCd, string bushoCd)
        {
            bool returnVal = true;

            if (string.IsNullOrEmpty(shainCd))
            {
                return returnVal;
            }

            var shainData = DaoInitUtility.GetComponent<IM_SHAINDao>().GetDataByCd(shainCd);

            // DELETE_FLGや適用日についてはFWのフォーカスアウトチェックに任せるため
            // それ以外のチェックを実装
            if (string.IsNullOrWhiteSpace(shainData.LOGIN_ID))
            {
                MessageBoxUtility.MessageBoxShow("E028");
                returnVal = false;
            }
            else if (!string.IsNullOrEmpty(bushoCd)
                && !bushoCd.Equals(shainData.BUSHO_CD))
            {
                // 入力されている部署チェック
                MessageBoxUtility.MessageBoxShow("E062", "部署CD");
                returnVal = false;
            }

            return returnVal;
        }
        #endregion

        #endregion

        #region Utility

        private string DbNullToEmpty(object val)
        {
            if (val == null)
            {
                return string.Empty;
            }

            return val.ToString();
        }

        #endregion

        #region Not Use

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
