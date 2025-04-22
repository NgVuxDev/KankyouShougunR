using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
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
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Function.ShougunCSCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Carriage.UnchinNyuuRyoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class UnchinNyuuryokuLogic : IBuisinessLogic
    {
        /// <summary>
        /// DTO
        /// </summary>
        private UnchinEntryDTOClass dto;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dtoUnchin;

        /// <summary>
        /// 運賃（収集）入力のDao
        /// </summary>
        private UnchinEntryDAOClass daoUnchinEntry;

        /// <summary>
        /// 運賃（収集）明細のDao
        /// </summary>
        private UnchinDetailDAOClass daoUnchinDetail;

        /// <summary>
        /// 運賃（収集）入力のDao
        /// </summary>
        private T_UNCHIN_ENTRYDao ueDao;

        /// <summary>
        /// 運賃（収集）明細のDao
        /// </summary>
        private T_UNCHIN_DETAILDao udDao;

        /// <summary>
        /// IM_SHARYOUDao
        /// </summary>
        r_framework.Dao.IM_SHARYOUDao sharyouDao;

        /// <summary>
        /// 搬入先休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_HANNYUUSAKIDao workclosedhannyuusakiDao;

        /// <summary>
        /// Form
        /// </summary>
        private UnchinNyuuryokuForm form;

        /// <summary>
        /// ヘッダー
        /// </summary>
        internal UIHeaderForm headerForm;
        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 運賃（収集）入力検索結果
        /// </summary>
        private T_UNCHIN_ENTRY dtUnchinEntry;

        /// <summary>
        /// 運賃（収集）明細検索結果
        /// </summary>
        private T_UNCHIN_DETAIL[] dtUnchinDetail;

        /// <summary>
        /// 明細DataGridView
        /// </summary>
        private DataGridView dgvDetail;

        /// <summary>
        /// 支払入力専用DBアクセッサー
        /// </summary>
        private DBAccessor accessor;

        /// <summary>
        /// 車輌選択ポップアップ選択中フラグ
        /// </summary>
        internal bool isSelectingSharyouCd = false;

        /// <summary>
        ///運賃収集入力Entityを格納
        /// </summary>
        private List<T_UNCHIN_ENTRY> insEntryEntityList = new List<T_UNCHIN_ENTRY>();

        /// <summary>
        /// 運賃入力
        /// </summary>
        T_UNCHIN_ENTRY EntryEntity = new T_UNCHIN_ENTRY();

        /// <summary>
        ///運賃明細Entityを格納
        /// </summary>
        private List<T_UNCHIN_DETAIL> insDetailEntityList = new List<T_UNCHIN_DETAIL>();

        /// <summary>
        /// 運賃入力を削除Entity
        /// </summary>
        private T_UNCHIN_ENTRY delEntryEntity;

        /// <summary>
        /// IM_UNITDao
        /// </summary>
        private IM_UNITDao unitDao;

        private M_UNIT[] units;

        // No2634-->
        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        // No2634<--

        /// <summary>取引先マスタ</summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>取引先マスタ</summary>
        private T_UKEIRE_ENTRYDao ukeireDao;

        /// <summary>取引先マスタ</summary>
        private T_SHUKKA_ENTRYDao shukkaDao;

        /// <summary>取引先マスタ</summary>
        private T_UR_SH_ENTRYDao urshDao;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// コントロールFocus時値格納
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        string[] columns = { "UNCHIN_HINMEI_CD", "NET_JYUURYOU", "SUURYOU", "UNIT_CD", "TANKA", "KINGAKU", "MEISAI_BIKOU" };

        /// <summary>
        /// BusinessCommonのDBAccesser
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;

        /// <summary>画面初期表示Flag</summary>
        private bool firstLoadFlg = true;
        internal Control[] allControl;
        internal MessageBoxShowLogic MsgBox;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "Shougun.Core.Carriage.UnchinNyuuRyoku.Setting.ButtonSetting.xml";

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm parentbaseform { get; set; }

        internal Color sharyouCdBackColor = Color.FromArgb(255, 235, 160);
        internal Color sharyouCdBackColorBlue = Color.FromArgb(0, 255, 255);

        private string tmpNidumiGyoushaCd = string.Empty;
        private string tmpNidumiGenbaCd = string.Empty;
        private string tmpNioroshiGyoushaCd = string.Empty;
        private string tmpNioroshiGenbaCd = string.Empty;
        private string tmpUnpanGyoushaCd = string.Empty;
        private string sharyouCd = string.Empty;
        private string shaShuCd = string.Empty;
        private string unpanGyousha = string.Empty;

        /// <summary>
        /// 運搬業者CD初期セット
        /// </summary>
        internal void UnpanGyoushaCdSet()
        {
            tmpUnpanGyoushaCd = this.form.txt_GyoushaCd.Text;
        }

        /// <summary>
        /// 車輌CD初期セット
        /// </summary>
        internal void ShayouCdSet()
        {
            sharyouCd = this.form.txt_SHARYOU_CD.Text;
        }

        /// <summary>
        /// 車種Cd初期セット
        /// </summary>
        internal void ShashuCdSet()
        {
            shaShuCd = this.form.txt_ShashuCd.Text;
        }

        /// <summary>
        /// 荷降業者CD初期セット
        /// </summary>
        internal void NioroshiGyoushaCdSet()
        {
            tmpNioroshiGyoushaCd = this.form.txt_NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷降現場CD初期セット
        /// </summary>
        internal void NioroshiGenbaCdSet()
        {
            tmpNioroshiGenbaCd = this.form.txt_NIOROSHI_GENBA_CD.Text;
        }

        /// <summary>
        /// 荷積業者CD初期セット
        /// </summary>
        internal void NidumiGyoushaCdSet()
        {
            tmpNidumiGyoushaCd = this.form.txt_NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積現場CD初期セット
        /// </summary>
        internal void NidumiGenbaCdSet()
        {
            tmpNidumiGenbaCd = this.form.txt_NIOROSHI_GENBA_CD.Text;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnchinNyuuryokuLogic(UnchinNyuuryokuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            this.form = targetForm;

            // dto
            this.dtoUnchin = new DTOClass();

            this.dto = new UnchinEntryDTOClass();
            // 運賃（持込）入力のDao
            this.daoUnchinEntry = DaoInitUtility.GetComponent<UnchinEntryDAOClass>();

            // 運賃（持込）入力明細のDao
            this.daoUnchinDetail = DaoInitUtility.GetComponent<UnchinDetailDAOClass>();

            this.ueDao = DaoInitUtility.GetComponent<T_UNCHIN_ENTRYDao>();
            this.udDao = DaoInitUtility.GetComponent<T_UNCHIN_DETAILDao>();

            this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();// r_framework.Dao.IM_SHARYOUDao
            this.workclosedhannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.dgvDetail = this.form.dgvDetail;

            this.accessor = new Shougun.Core.Carriage.UnchinNyuuRyoku.DBAccessor();
            this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();

            // メインフォーム
            this.form = targetForm;
            // DataGridViewコントロール
            this.dgvDetail = this.form.dgvDetail;
            // メッセージ表示オブジェクト
            MsgBox = new MessageBoxShowLogic();

            // No2634-->
            // システム情報Dao
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            // No2634<--

            // システム情報Dao
            this.unitDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNITDao>();

            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.ukeireDao = DaoInitUtility.GetComponent<T_UKEIRE_ENTRYDao>();
            this.shukkaDao = DaoInitUtility.GetComponent<T_SHUKKA_ENTRYDao>();
            this.urshDao = DaoInitUtility.GetComponent<T_UR_SH_ENTRYDao>();

            this.units = this.unitDao.GetAllValidData(new M_UNIT() { ISNOT_NEED_DELETE_FLG = true });

            LogUtility.DebugMethodEnd(targetForm);
        }

        /// <summary>
        /// 
        /// </summary>
        public void LogicalDelete()
        {
            this.LogicalDelete2();
        }

        #region 登録/更新/削除
        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public bool LogicalDelete2()
        {
            return true;
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registメソッドが正常の場合True
        /// </summary>
        public bool RegistResult = false;
        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            this.RegistResult = false;
            try
            {
                using (Transaction tran = new Transaction())
                {
                    // 運賃入力レコードをループ
                    foreach (T_UNCHIN_ENTRY entity in this.insEntryEntityList)
                    {
                        // 登録処理を行う
                        var temp = this.ueDao.Insert(entity);
                    }

                    // 運賃明細レコードをループ
                    foreach (T_UNCHIN_DETAIL entity in this.insDetailEntityList)
                    {

                        // 登録処理を行う
                        var temp = this.udDao.Insert(entity);
                    }

                    tran.Commit();
                }
                this.RegistResult = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.RegistResult = false;
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd(errorFlag);
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            int result = 0;
            if (!this.form.Renkei_Number.IsNull && this.form.Renkei_Denshu_Kbn_cd.IsNull)
            {
                this.dto.DENPYOU_NUMBER = this.form.Renkei_Number;
            }
            else
            {
                this.dto.RENKEI_NUMBER = this.form.Renkei_Number;
                this.dto.DENSHU_KBN_CD = this.form.Renkei_Denshu_Kbn_cd;
            }

            // 入力データを検索
            this.dtUnchinEntry = this.ueDao.GetDataToDataTable(this.dto);
            // 件数
            result = this.dtUnchinEntry != null ? 1 : 0;
            if (result == 0)
            {
                return result;
            }

            this.dto.SYSTEM_ID = this.dtUnchinEntry.SYSTEM_ID;
            this.dto.SEQ = this.dtUnchinEntry.SEQ;

            // 明細データを検索
            this.dtUnchinDetail = this.udDao.GetDataToDataTable(this.dto);
            LogUtility.DebugMethodEnd();
            return result;
        }

        /// <summary>
        /// update成功時True
        /// </summary>
        public bool UpdateResult = false;
        /// <summary>
        /// 修正処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            UpdateResult = false;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    // 運賃持込入力の元レコードを論理削除
                    this.dtUnchinEntry.DELETE_FLG = true;
                    this.ueDao.Update(this.dtUnchinEntry);

                    // 運賃持込入力レコードをループ
                    foreach (T_UNCHIN_ENTRY entity in this.insEntryEntityList)
                    {
                        // 登録処理を行う
                        entity.DELETE_FLG = false;
                        this.ueDao.Insert(entity);
                    }

                    // 運賃持込明細レコードをループ
                    foreach (T_UNCHIN_DETAIL entity in this.insDetailEntityList)
                    {
                        // 登録処理を行う
                        this.udDao.Insert(entity);
                    }
                    // コミット
                    tran.Commit();
                }
                UpdateResult = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.UpdateResult = false;
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd(errorFlag);
            }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.firstLoadFlg)
                {
                    // フォームインスタンスを取得
                    this.parentbaseform = (BusinessBaseForm)this.form.Parent;
                    this.headerForm = (UIHeaderForm)parentbaseform.headerForm;

                    // ボタンのテキストを初期化
                    this.ButtonInit();

                    // イベントの初期化処理
                    this.EventInit();

                    this.allControl = this.form.allControl;
                    this.firstLoadFlg = false;
                }

                // コントロールを初期化
                if (!this.ControlInit()) { return false; }
                if (this.form.Renkei_Denshu_Kbn_cd.IsNull)
                {
                    if (!this.form.Renkei_Number.IsNull)
                    {
                        this.Search();
                        this.SetValueToForm();
                    }
                }
                else
                {
                    if (this.Search() == 0)
                    {
                        this.SetValueToForm(this.form.Renkei_Denshu_Kbn_cd.Value);
                        this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    }
                    else
                    {
                        this.SetValueToForm();
                        this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    }
                }
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.form.txt_DenpyouBango.Text = "";
                }
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正権限がない場合
                        if (r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            // 修正権限は無いが参照権限がある場合は参照モードで起動
                            this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }
                    }
                }
                if (!this.ControlLock()) { return false; }
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

        internal bool CheckAuth()
        {
            bool ret = true;
            try
            {
                WINDOW_TYPE wt = this.form.WindowType;
                if (!this.form.Renkei_Denshu_Kbn_cd.IsNull)
                {
                    if (this.Search() == 0)
                    {
                        wt = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    }
                    else
                    {
                        wt = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    }
                }
                switch (wt)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            MessageBoxShowLogic msg = new MessageBoxShowLogic();
                            msg.MessageBoxShow("E158", "新規");
                            return false;
                        }
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限がない場合
                            if (!r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                // どちらも無い場合はアラートを表示して処理中断
                                MessageBoxShowLogic msg = new MessageBoxShowLogic();
                                msg.MessageBoxShow("E158", "修正");
                                return false;
                            }
                        }
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.DELETE_WINDOW_FLAG, false))
                        {
                            MessageBoxShowLogic msg = new MessageBoxShowLogic();
                            msg.MessageBoxShow("E158", "削除");
                            return false;
                        }
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            MessageBoxShowLogic msg = new MessageBoxShowLogic();
                            msg.MessageBoxShow("E158", "参照");
                            return false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckAuth", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        internal void SetErrorFocus()
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
        /// コントロール初期化処理
        /// </summary>
        /// <returns></returns>
        public bool ControlInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.form.txt_Denpyousyurui.Text = "160";

                this.form.txt_DenpyousyuruiMei.Text = "運賃";

                this.form.txt_RenkeiNumber.Text = "";

                //「伝票日付」
                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.DENPYOU_DATE.Value = parentForm.sysDate;

                //「伝票番号」
                this.form.txt_DenpyouBango.Text = "";

                // 「拠点CD」
                this.headerForm.txt_KYOTEN_CD.Text = "";

                // 「拠点名」
                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = "";

                // 「業者CD」
                this.form.txt_GyoushaCd.Text = "";

                // 「業者名」
                this.form.txt_GyoushaMei.Text = "";

                // 「車種CD」
                this.form.txt_ShashuCd.Text = "";

                // 「車種名」
                this.form.txt_ShashuMei.Text = "";

                // 「車輌CD」
                this.form.txt_SHARYOU_CD.Text = "";

                // 「車輌名」
                this.form.txt_SHARYOU_MEI.Text = "";

                // 「運転者CD」
                this.form.txt_UntenshaCd.Text = "";

                // 「運転者名」
                this.form.txt_UntenshaMei.Text = "";

                this.form.txt_Bikou.Text = "";
                this.form.KEITAI_KBN_CD.Text = "";
                this.form.KEITAI_KBN_NAME_RYAKU.Text = "";

                // 「荷積業者CD」
                this.form.txt_NidumiGyoushaCd.Text = "";

                // 「荷積業者名」
                this.form.txt_NidumiGyoushaMei.Text = "";

                // 「荷積現場CD」
                this.form.txt_NidumiGenbaCd.Text = "";

                // 「荷積現場名」
                this.form.txt_NidumiGenbaMei.Text = "";

                // 「荷降業者CD」
                this.form.txt_NIOROSHI_GYOUSHA_CD.Text = "";

                // 「荷降業者名」
                this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = "";

                // 「荷降現場CD」
                this.form.txt_NIOROSHI_GENBA_CD.Text = "";

                // 「荷降現場名」
                this.form.txt_NIOROSHI_GENBA_Mei.Text = "";

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        this.form.dgvDetail.AllowUserToAddRows = true;
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.form.dgvDetail.AllowUserToAddRows = false;
                        break;
                }
                //グリッドビュー運賃明細
                this.form.dgvDetail.Rows.Clear();
                if (this.form.dgvDetail.Rows.Count > 0)
                {
                    this.form.dgvDetail.CurrentCell = this.form.dgvDetail.Rows[0].Cells["UNCHIN_HINMEI_CD"];
                }
                //正味合計
                this.form.txt_KingakuTotal.Text = "0";
                //合計金額
                this.form.txt_Goukeikingaku.Text = "0";

                // 拠点
                string KYOTEN_CD = "拠点CD";
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                this.headerForm.txt_KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, KYOTEN_CD);
                if (!string.IsNullOrEmpty(this.headerForm.txt_KYOTEN_CD.Text))
                {
                    this.headerForm.txt_KYOTEN_CD.Text = this.headerForm.txt_KYOTEN_CD.Text
                        .PadLeft(this.headerForm.txt_KYOTEN_CD.MaxLength, '0');
                    CheckKyotenCd();
                }
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ControlInit", ex);
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
        /// コントロール初期化処理
        /// </summary>
        /// <returns></returns>
        public bool ControlLock()
        {
            bool ret = true;

            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;

                this.form.txt_SHARYOU_CD.AutoChangeBackColorEnabled = true;
                this.form.txt_SHARYOU_CD.UpdateBackColor();
                this.form.txt_SHARYOU_MEI.ReadOnly = true;
                this.form.txt_SHARYOU_MEI.Tag = string.Empty;
                this.form.txt_SHARYOU_MEI.TabStop = false;

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        //「伝票日付」
                        this.form.DENPYOU_DATE.Enabled = true;
                        //「伝票番号」
                        this.form.txt_DenpyouBango.Enabled = true;
                        this.form.txt_DenpyouBango.ReadOnly = false;
                        // 「拠点CD」
                        this.headerForm.txt_KYOTEN_CD.Enabled = true;
                        // 「業者CD」
                        this.form.txt_GyoushaCd.Enabled = true;
                        // 「車種CD」
                        this.form.txt_ShashuCd.Enabled = true;
                        // 「車輌CD」
                        this.form.txt_SHARYOU_CD.Enabled = true;
                        if (!string.IsNullOrEmpty(this.form.txt_SHARYOU_CD.Text))
                        {
                            SqlDateTime gettekiyouDate = SqlDateTime.Null;
                            if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE.Text))
                            {
                                gettekiyouDate = SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                            }
                            var sharyouEntitys = this.accessor.GetSharyou(this.form.txt_SHARYOU_CD.Text, this.form.txt_GyoushaCd.Text, null, null, gettekiyouDate);

                            // マスタ存在チェック
                            if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                            {
                                // 車輌名を編集可
                                this.ChangeShokuchiSharyouDesign();
                            }
                        }
                        // 「運転者CD」
                        this.form.txt_UntenshaCd.Enabled = true;
                        // 「荷積業者CD」
                        this.form.txt_NidumiGyoushaCd.Enabled = true;
                        // 「荷積現場CD」
                        this.form.txt_NidumiGenbaCd.Enabled = true;
                        // 「荷降業者CD」
                        this.form.txt_NIOROSHI_GYOUSHA_CD.Enabled = true;
                        // 「荷降現場CD」
                        this.form.txt_NIOROSHI_GENBA_CD.Enabled = true;
                        // 「備考」
                        this.form.txt_Bikou.Enabled = true;
                        // 「形態区分」
                        this.form.KEITAI_KBN_CD.Enabled = true;
                        //グリッドビュー運賃明細
                        // Detailのコントロールを制御
                        foreach (DataGridViewRow row in this.form.dgvDetail.Rows)
                        {
                            foreach (String cellName in columns)
                            {
                                if (cellName == "SUURYOU")
                                {
                                    if ((Convert.ToString(row.Cells["UNIT_NAME_RYAKU"].Value) == "kg" || Convert.ToString(row.Cells["UNIT_NAME_RYAKU"].Value) == "ｔ") &&
                                        row.Cells["NET_JYUURYOU"].Value != null && !string.IsNullOrEmpty(row.Cells["NET_JYUURYOU"].Value.ToString()))
                                    {
                                        row.Cells[cellName].ReadOnly = true;
                                    }
                                    else
                                    {
                                        row.Cells[cellName].ReadOnly = false;
                                    }
                                }
                                else
                                {
                                    row.Cells[cellName].ReadOnly = false;
                                }
                            }
                        }
                        this.form.dgvDetail.AllowUserToAddRows = true;
                        this.form.btn_Gyousha.Enabled = true;
                        this.form.btn_SHARYOU.Enabled = true;
                        this.form.btn_Shashu.Enabled = true;
                        this.form.btn_Untensha.Enabled = true;
                        this.form.btn_Mae.Enabled = true;
                        this.form.btn_Tsugi.Enabled = true;
                        this.form.KEITAI_KBN_SEARCH_BUTTON.Enabled = true;
                        this.form.btn_NidumiGyousha.Enabled = true;
                        this.form.btn_NidumiGenba.Enabled = true;
                        this.form.btn_NIOROSHI_GYOUSHA_SEARCH.Enabled = true;
                        this.form.btn_NIOROSHI_GENBA_SEARCH.Enabled = true;
                        parentForm.bt_func2.Enabled = true;
                        parentForm.bt_func7.Enabled = true;
                        parentForm.bt_func9.Enabled = true;
                        parentForm.bt_func10.Enabled = true;
                        parentForm.bt_func11.Enabled = true;
                        parentForm.bt_func12.Enabled = true;
                        // 単価と金額の活性/非活性制御
                        this.SetDetailReadOnly();
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        //「伝票日付」
                        this.form.DENPYOU_DATE.Enabled = true;
                        //「伝票番号」
                        this.form.txt_DenpyouBango.Enabled = true;
                        this.form.txt_DenpyouBango.ReadOnly = true;
                        // 「拠点CD」
                        this.headerForm.txt_KYOTEN_CD.Enabled = true;
                        // 「業者CD」
                        this.form.txt_GyoushaCd.Enabled = true;
                        // 「車種CD」
                        this.form.txt_ShashuCd.Enabled = true;
                        // 「車輌CD」
                        this.form.txt_SHARYOU_CD.Enabled = true;
                        if (!string.IsNullOrEmpty(this.form.txt_SHARYOU_CD.Text))
                        {
                            SqlDateTime gettekiyouDate = SqlDateTime.Null;
                            if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE.Text))
                            {
                                gettekiyouDate = SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                            }
                            var sharyouEntitys = this.accessor.GetSharyou(this.form.txt_SHARYOU_CD.Text, this.form.txt_GyoushaCd.Text, null, null, gettekiyouDate);

                            // マスタ存在チェック
                            if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                            {
                                // 車輌名を編集可
                                this.ChangeShokuchiSharyouDesign();
                            }
                        }
                        // 「運転者CD」
                        this.form.txt_UntenshaCd.Enabled = true;
                        // 「荷積業者CD」
                        this.form.txt_NidumiGyoushaCd.Enabled = true;
                        // 「荷積現場CD」
                        this.form.txt_NidumiGenbaCd.Enabled = true;
                        // 「荷降業者CD」
                        this.form.txt_NIOROSHI_GYOUSHA_CD.Enabled = true;
                        // 「荷降現場CD」
                        this.form.txt_NIOROSHI_GENBA_CD.Enabled = true;
                        // 「備考」
                        this.form.txt_Bikou.Enabled = true;
                        // 「形態区分」
                        this.form.KEITAI_KBN_CD.Enabled = true;
                        //グリッドビュー運賃明細
                        foreach (DataGridViewRow row in this.form.dgvDetail.Rows)
                        {
                            foreach (String cellName in columns)
                            {
                                if (cellName == "SUURYOU")
                                {
                                    if ((Convert.ToString(row.Cells["UNIT_NAME_RYAKU"].Value) == "kg" || Convert.ToString(row.Cells["UNIT_NAME_RYAKU"].Value) == "ｔ") &&
                                        row.Cells["NET_JYUURYOU"].Value != null && !string.IsNullOrEmpty(row.Cells["NET_JYUURYOU"].Value.ToString()))
                                    {
                                        row.Cells[cellName].ReadOnly = true;
                                    }
                                    else
                                    {
                                        row.Cells[cellName].ReadOnly = false;
                                    }
                                }
                                else
                                {
                                    row.Cells[cellName].ReadOnly = false;
                                }
                            }
                        }
                        this.form.dgvDetail.AllowUserToAddRows = true;
                        this.form.btn_Gyousha.Enabled = true;
                        this.form.btn_SHARYOU.Enabled = true;
                        this.form.btn_Shashu.Enabled = true;
                        this.form.btn_Untensha.Enabled = true;
                        this.form.btn_Mae.Enabled = true;
                        this.form.btn_Tsugi.Enabled = true;
                        this.form.KEITAI_KBN_SEARCH_BUTTON.Enabled = true;
                        this.form.btn_NidumiGyousha.Enabled = true;
                        this.form.btn_NidumiGenba.Enabled = true;
                        this.form.btn_NIOROSHI_GYOUSHA_SEARCH.Enabled = true;
                        this.form.btn_NIOROSHI_GENBA_SEARCH.Enabled = true;
                        parentForm.bt_func2.Enabled = true;
                        parentForm.bt_func7.Enabled = true;
                        parentForm.bt_func9.Enabled = true;
                        parentForm.bt_func10.Enabled = true;
                        parentForm.bt_func11.Enabled = true;
                        parentForm.bt_func12.Enabled = true;
                        // 単価と金額の活性/非活性制御
                        this.SetDetailReadOnly();
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        //「伝票日付」
                        this.form.DENPYOU_DATE.Enabled = false;
                        //「伝票番号」
                        this.form.txt_DenpyouBango.Enabled = false;
                        // 「拠点CD」
                        this.headerForm.txt_KYOTEN_CD.Enabled = false;
                        // 「業者CD」
                        this.form.txt_GyoushaCd.Enabled = false;
                        // 「車種CD」
                        this.form.txt_ShashuCd.Enabled = false;
                        // 「車輌CD」
                        this.form.txt_SHARYOU_CD.Enabled = false;
                        // 「運転者CD」
                        this.form.txt_UntenshaCd.Enabled = false;
                        // 「荷積業者CD」
                        this.form.txt_NidumiGyoushaCd.Enabled = false;
                        // 「荷積現場CD」
                        this.form.txt_NidumiGenbaCd.Enabled = false;
                        // 「荷降業者CD」
                        this.form.txt_NIOROSHI_GYOUSHA_CD.Enabled = false;
                        // 「荷降現場CD」
                        this.form.txt_NIOROSHI_GENBA_CD.Enabled = false;
                        // 「備考」
                        this.form.txt_Bikou.Enabled = false;
                        // 「形態区分」
                        this.form.KEITAI_KBN_CD.Enabled = false;
                        //グリッドビュー運賃明細
                        foreach (DataGridViewRow row in this.form.dgvDetail.Rows)
                        {
                            foreach (String cellName in columns)
                            {
                                row.Cells[cellName].ReadOnly = true;
                            }
                        }
                        this.form.dgvDetail.AllowUserToAddRows = false;
                        this.form.btn_Gyousha.Enabled = false;
                        this.form.btn_SHARYOU.Enabled = false;
                        this.form.btn_Shashu.Enabled = false;
                        this.form.btn_Untensha.Enabled = false;
                        this.form.btn_Mae.Enabled = false;
                        this.form.btn_Tsugi.Enabled = false;
                        this.form.KEITAI_KBN_SEARCH_BUTTON.Enabled = false;
                        this.form.btn_NidumiGyousha.Enabled = false;
                        this.form.btn_NidumiGenba.Enabled = false;
                        this.form.btn_NIOROSHI_GYOUSHA_SEARCH.Enabled = false;
                        this.form.btn_NIOROSHI_GENBA_SEARCH.Enabled = false;
                        parentForm.bt_func2.Enabled = false;
                        parentForm.bt_func7.Enabled = true;
                        parentForm.bt_func9.Enabled = true;
                        parentForm.bt_func10.Enabled = false;
                        parentForm.bt_func11.Enabled = false;
                        parentForm.bt_func12.Enabled = true;
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        //「伝票日付」
                        this.form.DENPYOU_DATE.Enabled = false;
                        //「伝票番号」
                        this.form.txt_DenpyouBango.Enabled = false;
                        // 「拠点CD」
                        this.headerForm.txt_KYOTEN_CD.Enabled = false;
                        // 「業者CD」
                        this.form.txt_GyoushaCd.Enabled = false;
                        // 「車種CD」
                        this.form.txt_ShashuCd.Enabled = false;
                        // 「車輌CD」
                        this.form.txt_SHARYOU_CD.Enabled = false;
                        // 「運転者CD」
                        this.form.txt_UntenshaCd.Enabled = false;
                        // 「荷積業者CD」
                        this.form.txt_NidumiGyoushaCd.Enabled = false;
                        // 「荷積現場CD」
                        this.form.txt_NidumiGenbaCd.Enabled = false;
                        // 「荷降業者CD」
                        this.form.txt_NIOROSHI_GYOUSHA_CD.Enabled = false;
                        // 「荷降現場CD」
                        this.form.txt_NIOROSHI_GENBA_CD.Enabled = false;
                        // 「備考」
                        this.form.txt_Bikou.Enabled = false;
                        // 「形態区分」
                        this.form.KEITAI_KBN_CD.Enabled = false;
                        //グリッドビュー運賃明細
                        foreach (DataGridViewRow row in this.form.dgvDetail.Rows)
                        {
                            foreach (String cellName in columns)
                            {
                                row.Cells[cellName].ReadOnly = true;
                            }
                        }
                        this.form.dgvDetail.AllowUserToAddRows = false;
                        this.form.btn_Gyousha.Enabled = false;
                        this.form.btn_SHARYOU.Enabled = false;
                        this.form.btn_Shashu.Enabled = false;
                        this.form.btn_Untensha.Enabled = false;
                        this.form.btn_Mae.Enabled = true;
                        this.form.btn_Tsugi.Enabled = true;
                        this.form.KEITAI_KBN_SEARCH_BUTTON.Enabled = false;
                        this.form.btn_NidumiGyousha.Enabled = false;
                        this.form.btn_NidumiGenba.Enabled = false;
                        this.form.btn_NIOROSHI_GYOUSHA_SEARCH.Enabled = false;
                        this.form.btn_NIOROSHI_GENBA_SEARCH.Enabled = false;
                        parentForm.bt_func2.Enabled = true;
                        parentForm.bt_func7.Enabled = true;
                        parentForm.bt_func9.Enabled = false;
                        parentForm.bt_func10.Enabled = false;
                        parentForm.bt_func11.Enabled = false;
                        parentForm.bt_func12.Enabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ControlLock", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// ボタン初期化処理
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            parentform.bt_func2.Click += new EventHandler(this.form.bt_func02_Click);
            //parentform.bt_func3.Click += new EventHandler(this.form.bt_func03_Click);
            parentform.bt_func7.Click += new EventHandler(this.form.bt_func07_Click);
            //登録ボタン(F9)イベント生成
            this.C_Regist(parentform.bt_func9);
            parentform.bt_func9.Click += new EventHandler(this.form.bt_func09_Click);

            parentform.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);
            parentform.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);
            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //運搬業者
            this.form.txt_GyoushaCd.Validating += new CancelEventHandler(this.form.txt_GyoushaCd_Validating);

            //運転者
            this.form.txt_UntenshaCd.Validating += new CancelEventHandler(this.form.txt_UntenshaCd_Validating);

            // 「荷積業者CD」のイベント生成
            this.form.txt_NidumiGyoushaCd.Validating += new CancelEventHandler(this.form.txt_NidumiGyoushaCd_Validating);
            //  「荷積現場CD」のイベント生成
            this.form.txt_NidumiGenbaCd.Validating += new CancelEventHandler(this.form.txt_NidumiGenbaCd_Validating);

            // 「荷降業者CD」のイベント生成
            this.form.txt_NIOROSHI_GYOUSHA_CD.Validating += new CancelEventHandler(this.form.txt_NIOROSHI_GYOUSHA_CD_Validating);
            // 「荷降現場CD」のイベント生成
            this.form.txt_NIOROSHI_GENBA_CD.Validating += new CancelEventHandler(this.form.txt_NIOROSHI_GENBA_CD_Validating);

            //車輌
            this.form.txt_SHARYOU_CD.Enter += new System.EventHandler(this.form.txt_SHARYOU_CD_Enter);
            this.form.txt_SHARYOU_CD.Validating += new CancelEventHandler(this.form.txt_SHARYOU_CD_Validating);
            this.form.txt_SHARYOU_CD.Validated += new EventHandler(this.form.txt_SHARYOU_CD_Validated);
            this.form.txt_SHARYOU_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.form.PreviewKeyDownForShokuchikbnCheck);

            // 車種
            this.form.txt_ShashuCd.Validating += new CancelEventHandler(this.form.txt_ShashuCd_Validating);

            // 伝票番号
            this.form.txt_DenpyouBango.Validating += new CancelEventHandler(this.form.txt_DenpyouBango_Validating);

            #region CustomGridview
            this.form.dgvDetail.CellValidating += new DataGridViewCellValidatingEventHandler(this.form.dgvDetail_CellValidating);
            this.form.dgvDetail.RowsAdded += new DataGridViewRowsAddedEventHandler(this.form.dgvDetail_RowsAdded);
            this.form.dgvDetail.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.form.dgvDetail_RowsRemoved);
            this.form.dgvDetail.CellEnter += new DataGridViewCellEventHandler(this.form.dgvDetail_CellEnter);
            this.form.dgvDetail.CellPainting += new DataGridViewCellPaintingEventHandler(this.form.dgvDetail_CellPainting);
            this.form.dgvDetail.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.form.dgvDetail_ColumnWidthChanged);
            this.form.dgvDetail.SizeChanged += new EventHandler(this.form.dgvDetail_SizeChanged);
            #endregion
            ////前ボタンのイベント生成
            this.form.btn_Mae.Click += new EventHandler(this.form.btn_Mae_Click);
            ////次ボタンのイベント生成
            this.form.btn_Tsugi.Click += new EventHandler(this.form.btn_Tsugi_Click);

            this.form.txt_ShashuCd.Enter += new EventHandler(this.form.SHARYOU_NAME_RYAKU_Enter);

            // 形態区分
            this.form.KEITAI_KBN_CD.Validating += new CancelEventHandler(this.form.KEITAI_KBN_CD_Validating);

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Clickイベントメソッドの上書き
        /// </summary>
        public void C_Regist(Control c)
        {
            c.Click += this.form.C_Regist;
        }
        #endregion

        #region 補助ファンクション
        /// <summary>
        /// Int16?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal Int16? ToNInt16(object o)
        {
            Int16? ret = null;
            Int16 parse = 0;
            if (Int16.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// Int32?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal int? ToNInt32(object o)
        {
            int? ret = null;
            int parse = 0;
            if (int.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// Int64?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal Int64? ToNInt64(object o)
        {
            Int64? ret = null;
            Int64 parse = 0;
            if (Int64.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        ///// <summary>
        ///// double?型に転換する
        ///// </summary>
        ///// <param name="o">o</param>
        //internal double? ToNDouble(object o)
        //{
        //    double? ret = null;
        //    double parse = 0;
        //    if (double.TryParse(Convert.ToString(o), out parse))
        //    {
        //        ret = parse;
        //    }
        //    return ret;
        //}

        /// <summary>
        /// decimal?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal decimal? ToNDecimal(object o)
        {
            decimal? ret = null;
            decimal parse = 0;
            if (decimal.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// bool?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal bool? ToNBoolean(object o)
        {
            bool? ret = null;
            bool parse = false;
            if (Boolean.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// DateTime?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal DateTime? ToNDateTime(object o)
        {
            DateTime? ret = null;
            var parentForm = (BusinessBaseForm)this.form.Parent;
            DateTime parse = parentForm.sysDate;
            if (DateTime.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }
        #endregion

        #region 検索結果を画面に表示
        /// <summary>
        /// 検索結果を画面に表示
        /// </summary>
        internal void SetValueToForm(Int16 denshuKbn)
        {
            bool catchErr = false;
            LogUtility.DebugMethodStart();
            switch (denshuKbn)
            {
                case (short)DENSHU_KBN.UKEIRE:
                    T_UKEIRE_ENTRY ukeire = this.form.entitys == null || this.form.entitys.Length == 0 ? null : this.form.entitys[0] as T_UKEIRE_ENTRY;
                    if (ukeire == null || ukeire.SYSTEM_ID.IsNull)
                    {
                        ukeire = this.ukeireDao.GetUkeire(new T_UKEIRE_ENTRY() { UKEIRE_NUMBER = this.form.Renkei_Number.Value });
                    }
                    if (ukeire != null)
                    {
                        // ヘッダフォーム設定
                        // 拠点
                        this.headerForm.txt_KYOTEN_CD.Text = ukeire.KYOTEN_CD.IsNull ? "" : Convert.ToString(ukeire.KYOTEN_CD.Value).PadLeft(2, '0');
                        if (!ukeire.KYOTEN_CD.IsNull)
                        {
                            var kyotens = this.accessor.GetAllDataByCodeForKyoten(ukeire.KYOTEN_CD.Value);
                            if (kyotens != null && kyotens.Length > 0)
                            {
                                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = kyotens[0].KYOTEN_NAME_RYAKU;
                            }
                            else
                            {
                                this.headerForm.txt_KYOTEN_CD.Text = "";
                                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = "";
                            }
                        }
                        this.form.txt_Denpyousyurui.Text = "1";
                        M_DENSHU_KBN uk_den = this.accessor.GetdenshuKbn(1);
                        this.form.txt_DenpyousyuruiMei.Text = uk_den == null ? "" : uk_den.DENSHU_KBN_NAME_RYAKU;
                        this.form.txt_RenkeiNumber.Text = this.form.Renkei_Number.IsNull ? "" : this.form.Renkei_Number.Value.ToString();

                        // 伝票日付(指定)
                        this.form.DENPYOU_DATE.Value = ukeire.DENPYOU_DATE.Value;

                        // 業者
                        this.form.txt_GyoushaCd.Text = ukeire.UNPAN_GYOUSHA_CD;
                        M_GYOUSHA uk_Gyo = this.accessor.GetGyousha(ukeire.UNPAN_GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (uk_Gyo != null && uk_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_GyoushaMei.Text = uk_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_GyoushaCd.Text = "";
                            this.form.txt_GyoushaMei.Text = "";
                        }

                        // 車種
                        this.form.txt_ShashuCd.Text = ukeire.SHASHU_CD;
                        M_SHASHU uk_shu = this.accessor.GetShashu(ukeire.SHASHU_CD);
                        if (uk_shu != null)
                        {
                            this.form.txt_ShashuMei.Text = uk_shu.SHASHU_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_ShashuCd.Text = "";
                            this.form.txt_ShashuMei.Text = "";
                        }

                        // 車輌
                        this.form.txt_SHARYOU_CD.Text = ukeire.SHARYOU_CD;
                        this.form.txt_SHARYOU_MEI.Text = ukeire.SHARYOU_NAME;

                        // 運転者
                        this.form.txt_UntenshaCd.Text = ukeire.UNTENSHA_CD;
                        M_SHAIN uk_shain = this.accessor.GetShain(ukeire.UNTENSHA_CD);
                        if (uk_shain != null)
                        {
                            this.form.txt_UntenshaMei.Text = uk_shain.SHAIN_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_UntenshaCd.Text = "";
                            this.form.txt_UntenshaMei.Text = "";
                        }

                        // 形態区分
                        this.form.KEITAI_KBN_CD.Text = ukeire.KEITAI_KBN_CD.IsNull ? "" : ukeire.KEITAI_KBN_CD.Value.ToString();
                        if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                        {
                            M_KEITAI_KBN ketai = this.accessor.GetkeitaiKbn(ukeire.KEITAI_KBN_CD.Value);
                            if (ketai != null)
                            {
                                this.form.KEITAI_KBN_NAME_RYAKU.Text = ketai.KEITAI_KBN_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.KEITAI_KBN_CD.Text = "";
                                this.form.KEITAI_KBN_NAME_RYAKU.Text = "";
                            }
                        }

                        // 荷降業者
                        this.form.txt_NIOROSHI_GYOUSHA_CD.Text = ukeire.NIOROSHI_GYOUSHA_CD;
                        uk_Gyo = this.accessor.GetGyousha(ukeire.NIOROSHI_GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (uk_Gyo != null && (uk_Gyo.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || uk_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = uk_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_NIOROSHI_GYOUSHA_CD.Text = "";
                            this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = "";
                        }

                        // 荷降場
                        M_GENBA uk_Gen = null;
                        if (!string.IsNullOrEmpty(this.form.txt_NIOROSHI_GYOUSHA_CD.Text))
                        {
                            this.form.txt_NIOROSHI_GENBA_CD.Text = ukeire.NIOROSHI_GENBA_CD;
                            uk_Gen = this.accessor.GetGenba(ukeire.NIOROSHI_GYOUSHA_CD, ukeire.NIOROSHI_GENBA_CD);
                            // 20151104 BUNN #12040 STR
                            if (uk_Gen != null && (uk_Gen.SAISHUU_SHOBUNJOU_KBN.IsTrue || uk_Gen.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || uk_Gen.TSUMIKAEHOKAN_KBN.IsTrue))
                            // 20151104 BUNN #12040 END
                            {
                                this.form.txt_NIOROSHI_GENBA_Mei.Text = uk_Gen.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.txt_NIOROSHI_GENBA_CD.Text = "";
                                this.form.txt_NIOROSHI_GENBA_Mei.Text = "";
                            }
                        }

                        // 荷積業者
                        this.form.txt_NidumiGyoushaCd.Text = ukeire.GYOUSHA_CD;
                        uk_Gyo = this.accessor.GetGyousha(ukeire.GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (uk_Gyo != null && (uk_Gyo.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || uk_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_NidumiGyoushaMei.Text = uk_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_NidumiGyoushaCd.Text = "";
                            this.form.txt_NidumiGyoushaMei.Text = "";
                        }

                        //荷積現場
                        if (!string.IsNullOrEmpty(this.form.txt_NidumiGyoushaCd.Text))
                        {
                            this.form.txt_NidumiGenbaCd.Text = ukeire.GENBA_CD;
                            uk_Gen = this.accessor.GetGenba(ukeire.GYOUSHA_CD, ukeire.GENBA_CD);
                            // 20151104 BUNN #12040 STR
                            if (uk_Gen != null && (uk_Gen.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || uk_Gen.TSUMIKAEHOKAN_KBN.IsTrue))
                            // 20151104 BUNN #12040 END
                            {
                                this.form.txt_NidumiGenbaMei.Text = uk_Gen.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.txt_NidumiGenbaCd.Text = "";
                                this.form.txt_NidumiGenbaMei.Text = "";
                            }
                        }

                        if (!ukeire.NET_TOTAL.IsNull && ukeire.NET_TOTAL.Value != 0)
                        {
                            this.dgvDetail.Rows.Clear();// 2021/09/21 GODA ADD #155418
                            // 明細行を追加
                            this.dgvDetail.Rows.Add(1);

                            // 正味
                            this.dgvDetail["NET_JYUURYOU", 0].Value = this.SuuryouAndTankFormat(this.ToNDecimal(ukeire.NET_TOTAL) ?? 0, SystemProperty.Format.Jyuryou, out catchErr);
                            if (catchErr) { return; }

                            if (!this.KingakuTotal()) { return; }
                        }
                    }
                    break;
                case (short)DENSHU_KBN.SHUKKA:
                    T_SHUKKA_ENTRY shukka = this.form.entitys == null || this.form.entitys.Length == 0 ? null : this.form.entitys[0] as T_SHUKKA_ENTRY;
                    if (shukka == null || shukka.SYSTEM_ID.IsNull)
                    {
                        shukka = this.shukkaDao.GetShukka(new T_SHUKKA_ENTRY() { SHUKKA_NUMBER = this.form.Renkei_Number.Value });
                    }
                    if (shukka != null)
                    {
                        // ヘッダフォーム設定
                        // 拠点
                        this.headerForm.txt_KYOTEN_CD.Text = shukka.KYOTEN_CD.IsNull ? "" : Convert.ToString(shukka.KYOTEN_CD.Value).PadLeft(2, '0');
                        if (!shukka.KYOTEN_CD.IsNull)
                        {
                            var kyotens = this.accessor.GetAllDataByCodeForKyoten(shukka.KYOTEN_CD.Value);
                            if (kyotens != null && kyotens.Length > 0)
                            {
                                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = kyotens[0].KYOTEN_NAME_RYAKU;
                            }
                            else
                            {
                                this.headerForm.txt_KYOTEN_CD.Text = "";
                                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = "";
                            }
                        }
                        this.form.txt_Denpyousyurui.Text = "2";
                        M_DENSHU_KBN sk_den = this.accessor.GetdenshuKbn(2);
                        this.form.txt_DenpyousyuruiMei.Text = sk_den == null ? "" : sk_den.DENSHU_KBN_NAME_RYAKU;
                        this.form.txt_RenkeiNumber.Text = this.form.Renkei_Number.IsNull ? "" : this.form.Renkei_Number.Value.ToString();

                        // 伝票日付(指定)
                        this.form.DENPYOU_DATE.Value = shukka.DENPYOU_DATE.Value;

                        // 業者
                        this.form.txt_GyoushaCd.Text = shukka.UNPAN_GYOUSHA_CD;
                        M_GYOUSHA sk_Gyo = this.accessor.GetGyousha(shukka.UNPAN_GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (sk_Gyo != null && sk_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_GyoushaMei.Text = sk_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_GyoushaCd.Text = "";
                            this.form.txt_GyoushaMei.Text = "";
                        }

                        // 車種
                        this.form.txt_ShashuCd.Text = shukka.SHASHU_CD;
                        M_SHASHU sk_shu = this.accessor.GetShashu(shukka.SHASHU_CD);
                        if (sk_shu != null)
                        {
                            this.form.txt_ShashuMei.Text = sk_shu.SHASHU_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_ShashuCd.Text = "";
                            this.form.txt_ShashuMei.Text = "";
                        }

                        // 車輌
                        this.form.txt_SHARYOU_CD.Text = shukka.SHARYOU_CD;
                        this.form.txt_SHARYOU_MEI.Text = shukka.SHARYOU_NAME;

                        // 運転者
                        this.form.txt_UntenshaCd.Text = shukka.UNTENSHA_CD;
                        M_SHAIN sk_shain = this.accessor.GetShain(shukka.UNTENSHA_CD);
                        if (sk_shain != null && sk_shain.UNTEN_KBN.IsTrue)
                        {
                            this.form.txt_UntenshaMei.Text = sk_shain.SHAIN_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_UntenshaCd.Text = "";
                            this.form.txt_UntenshaMei.Text = "";
                        }

                        // 形態区分
                        this.form.KEITAI_KBN_CD.Text = shukka.KEITAI_KBN_CD.IsNull ? "" : shukka.KEITAI_KBN_CD.Value.ToString();
                        if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                        {
                            M_KEITAI_KBN ketai = this.accessor.GetkeitaiKbn(shukka.KEITAI_KBN_CD.Value);
                            if (ketai != null)
                            {
                                this.form.KEITAI_KBN_NAME_RYAKU.Text = ketai.KEITAI_KBN_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.KEITAI_KBN_CD.Text = "";
                                this.form.KEITAI_KBN_NAME_RYAKU.Text = "";
                            }
                        }

                        // 荷降業者
                        this.form.txt_NIOROSHI_GYOUSHA_CD.Text = shukka.GYOUSHA_CD;
                        sk_Gyo = this.accessor.GetGyousha(shukka.GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (sk_Gyo != null && (sk_Gyo.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || sk_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = sk_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_NIOROSHI_GYOUSHA_CD.Text = "";
                            this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = "";
                        }

                        // 荷降場
                        M_GENBA sk_Gen = null;
                        if (!string.IsNullOrEmpty(this.form.txt_NIOROSHI_GYOUSHA_CD.Text))
                        {
                            this.form.txt_NIOROSHI_GENBA_CD.Text = shukka.GENBA_CD;
                            sk_Gen = this.accessor.GetGenba(shukka.GYOUSHA_CD, shukka.GENBA_CD);
                            // 20151104 BUNN #12040 STR
                            if (sk_Gen != null && (sk_Gen.SAISHUU_SHOBUNJOU_KBN.IsTrue || sk_Gen.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || sk_Gen.TSUMIKAEHOKAN_KBN.IsTrue))
                            // 20151104 BUNN #12040 END
                            {
                                this.form.txt_NIOROSHI_GENBA_Mei.Text = sk_Gen.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.txt_NIOROSHI_GENBA_CD.Text = "";
                                this.form.txt_NIOROSHI_GENBA_Mei.Text = "";
                            }
                        }

                        // 荷積業者
                        this.form.txt_NidumiGyoushaCd.Text = shukka.NIZUMI_GYOUSHA_CD;
                        sk_Gyo = this.accessor.GetGyousha(shukka.NIZUMI_GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (sk_Gyo != null && (sk_Gyo.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || sk_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_NidumiGyoushaMei.Text = sk_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_NidumiGyoushaCd.Text = "";
                            this.form.txt_NidumiGyoushaMei.Text = "";
                        }

                        //荷積現場
                        if (!string.IsNullOrEmpty(this.form.txt_NidumiGyoushaCd.Text))
                        {
                            this.form.txt_NidumiGenbaCd.Text = shukka.NIZUMI_GENBA_CD;
                            sk_Gen = this.accessor.GetGenba(shukka.NIZUMI_GYOUSHA_CD, shukka.NIZUMI_GENBA_CD);
                            // 20151104 BUNN #12040 STR
                            if (sk_Gen != null && (sk_Gen.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || sk_Gen.TSUMIKAEHOKAN_KBN.IsTrue))
                            // 20151104 BUNN #12040 END
                            {
                                this.form.txt_NidumiGenbaMei.Text = sk_Gen.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.txt_NidumiGenbaCd.Text = "";
                                this.form.txt_NidumiGenbaMei.Text = "";
                            }
                        }

                        if (!shukka.NET_TOTAL.IsNull && shukka.NET_TOTAL.Value != 0)
                        {
                            this.dgvDetail.Rows.Clear();// 2021/09/21 GODA ADD #155418
                            // 明細行を追加
                            this.dgvDetail.Rows.Add(1);

                            // 正味
                            this.dgvDetail["NET_JYUURYOU", 0].Value = this.SuuryouAndTankFormat(this.ToNDecimal(shukka.NET_TOTAL) ?? 0, SystemProperty.Format.Jyuryou, out catchErr);
                            if (catchErr) { return; }

                            if (!this.KingakuTotal()) { return; }
                        }
                    }
                    break;
                case (short)DENSHU_KBN.URIAGE_SHIHARAI:
                    T_UR_SH_ENTRY ursh = this.form.entitys == null || this.form.entitys.Length == 0 ? null : this.form.entitys[0] as T_UR_SH_ENTRY;
                    if (ursh == null || ursh.SYSTEM_ID.IsNull)
                    {
                        ursh = this.urshDao.GetUrsh(new T_UR_SH_ENTRY() { UR_SH_NUMBER = this.form.Renkei_Number.Value });
                    }
                    if (ursh != null)
                    {
                        // ヘッダフォーム設定
                        // 拠点
                        this.headerForm.txt_KYOTEN_CD.Text = ursh.KYOTEN_CD.IsNull ? "" : Convert.ToString(ursh.KYOTEN_CD.Value).PadLeft(2, '0');
                        if (!ursh.KYOTEN_CD.IsNull)
                        {
                            var kyotens = this.accessor.GetAllDataByCodeForKyoten(ursh.KYOTEN_CD.Value);
                            if (kyotens != null && kyotens.Length > 0)
                            {
                                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = kyotens[0].KYOTEN_NAME_RYAKU;
                            }
                            else
                            {
                                this.headerForm.txt_KYOTEN_CD.Text = "";
                                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = "";
                            }
                        }
                        this.form.txt_Denpyousyurui.Text = "3";
                        M_DENSHU_KBN ur_den = this.accessor.GetdenshuKbn(3);
                        this.form.txt_DenpyousyuruiMei.Text = ur_den == null ? "" : ur_den.DENSHU_KBN_NAME_RYAKU;
                        this.form.txt_RenkeiNumber.Text = this.form.Renkei_Number.IsNull ? "" : this.form.Renkei_Number.Value.ToString();

                        // 伝票日付(指定)
                        this.form.DENPYOU_DATE.Value = ursh.DENPYOU_DATE.Value;

                        // 業者
                        this.form.txt_GyoushaCd.Text = ursh.UNPAN_GYOUSHA_CD;
                        M_GYOUSHA ur_Gyo = this.accessor.GetGyousha(ursh.UNPAN_GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (ur_Gyo != null && ur_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_GyoushaMei.Text = ur_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_GyoushaCd.Text = "";
                            this.form.txt_GyoushaMei.Text = "";
                        }

                        // 車種
                        this.form.txt_ShashuCd.Text = ursh.SHASHU_CD;
                        M_SHASHU ur_shu = this.accessor.GetShashu(ursh.SHASHU_CD);
                        if (ur_shu != null)
                        {
                            this.form.txt_ShashuMei.Text = ur_shu.SHASHU_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_ShashuCd.Text = "";
                            this.form.txt_ShashuMei.Text = "";
                        }

                        // 車輌
                        this.form.txt_SHARYOU_CD.Text = ursh.SHARYOU_CD;
                        this.form.txt_SHARYOU_MEI.Text = ursh.SHARYOU_NAME;

                        // 運転者
                        this.form.txt_UntenshaCd.Text = ursh.UNTENSHA_CD;
                        M_SHAIN ur_shain = this.accessor.GetShain(ursh.UNTENSHA_CD);
                        if (ur_shain != null && ur_shain.UNTEN_KBN.IsTrue)
                        {
                            this.form.txt_UntenshaMei.Text = ur_shain.SHAIN_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_UntenshaCd.Text = "";
                            this.form.txt_UntenshaMei.Text = "";
                        }

                        // 形態区分
                        this.form.KEITAI_KBN_CD.Text = ursh.KEITAI_KBN_CD.IsNull ? "" : ursh.KEITAI_KBN_CD.Value.ToString();
                        if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                        {
                            M_KEITAI_KBN ketai = this.accessor.GetkeitaiKbn(ursh.KEITAI_KBN_CD.Value);
                            if (ketai != null)
                            {
                                this.form.KEITAI_KBN_NAME_RYAKU.Text = ketai.KEITAI_KBN_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.KEITAI_KBN_CD.Text = "";
                                this.form.KEITAI_KBN_NAME_RYAKU.Text = "";
                            }
                        }

                        // 荷降業者
                        this.form.txt_NIOROSHI_GYOUSHA_CD.Text = ursh.NIOROSHI_GYOUSHA_CD;
                        ur_Gyo = this.accessor.GetGyousha(ursh.NIOROSHI_GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (ur_Gyo != null && (ur_Gyo.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || ur_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = ur_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_NIOROSHI_GYOUSHA_CD.Text = "";
                            this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = "";
                        }

                        // 荷降現場
                        M_GENBA ur_Gen = null;
                        if (!string.IsNullOrEmpty(this.form.txt_NIOROSHI_GYOUSHA_CD.Text))
                        {
                            this.form.txt_NIOROSHI_GENBA_CD.Text = ursh.NIOROSHI_GENBA_CD;
                            ur_Gen = this.accessor.GetGenba(ursh.NIOROSHI_GYOUSHA_CD, ursh.NIOROSHI_GENBA_CD);
                            // 20151104 BUNN #12040 STR
                            if (ur_Gen != null && (ur_Gen.SAISHUU_SHOBUNJOU_KBN.IsTrue || ur_Gen.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || ur_Gen.TSUMIKAEHOKAN_KBN.IsTrue))
                            // 20151104 BUNN #12040 END
                            {
                                this.form.txt_NIOROSHI_GENBA_Mei.Text = ur_Gen.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.txt_NIOROSHI_GENBA_CD.Text = "";
                                this.form.txt_NIOROSHI_GENBA_Mei.Text = "";
                            }
                        }

                        // 荷積業者
                        this.form.txt_NidumiGyoushaCd.Text = !string.IsNullOrEmpty(ursh.NIZUMI_GYOUSHA_CD) ? ursh.NIZUMI_GYOUSHA_CD : ursh.GYOUSHA_CD;
                        ur_Gyo = this.accessor.GetGyousha(this.form.txt_NidumiGyoushaCd.Text);
                        // 20151104 BUNN #12040 STR
                        if (ur_Gyo != null && (ur_Gyo.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || ur_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_NidumiGyoushaMei.Text = ur_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_NidumiGyoushaCd.Text = "";
                            this.form.txt_NidumiGyoushaMei.Text = "";
                        }

                        // 荷積現場
                        if (!string.IsNullOrEmpty(this.form.txt_NidumiGyoushaCd.Text))
                        {
                            this.form.txt_NidumiGenbaCd.Text = !string.IsNullOrEmpty(ursh.NIZUMI_GYOUSHA_CD) || !string.IsNullOrEmpty(ursh.NIZUMI_GENBA_CD) ? ursh.NIZUMI_GENBA_CD : ursh.GENBA_CD;
                            ur_Gen = this.accessor.GetGenba(this.form.txt_NidumiGyoushaCd.Text, this.form.txt_NidumiGenbaCd.Text);
                            // 20151104 BUNN #12040 STR
                            if (ur_Gen != null && (ur_Gen.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || ur_Gen.TSUMIKAEHOKAN_KBN.IsTrue))
                            // 20151104 BUNN #12040 END
                            {
                                this.form.txt_NidumiGenbaMei.Text = ur_Gen.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.txt_NidumiGenbaCd.Text = "";
                                this.form.txt_NidumiGenbaMei.Text = "";
                            }
                        }
                    }
                    break;
                case (short)DENSHU_KBN.DAINOU:
                    T_UR_SH_ENTRY u = this.form.entitys == null || this.form.entitys.Length == 0 ? null : this.form.entitys[0] as T_UR_SH_ENTRY;

                    if (u != null || u.SYSTEM_ID.IsNull)
                    {
                        // ヘッダフォーム設定
                        // 拠点
                        this.headerForm.txt_KYOTEN_CD.Text = u.KYOTEN_CD.IsNull ? "" : Convert.ToString(u.KYOTEN_CD.Value).PadLeft(2, '0');
                        if (!u.KYOTEN_CD.IsNull)
                        {
                            var kyotens = this.accessor.GetAllDataByCodeForKyoten(u.KYOTEN_CD.Value);
                            if (kyotens != null && kyotens.Length > 0)
                            {
                                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = kyotens[0].KYOTEN_NAME_RYAKU;
                            }
                            else
                            {
                                this.headerForm.txt_KYOTEN_CD.Text = "";
                                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = "";
                            }
                        }
                        this.form.txt_Denpyousyurui.Text = "170";
                        M_DENSHU_KBN dai_den = this.accessor.GetdenshuKbn(170);
                        this.form.txt_DenpyousyuruiMei.Text = dai_den == null ? "" : dai_den.DENSHU_KBN_NAME_RYAKU;
                        this.form.txt_RenkeiNumber.Text = this.form.Renkei_Number.IsNull ? "" : this.form.Renkei_Number.Value.ToString();

                        // 伝票日付(指定)
                        this.form.DENPYOU_DATE.Value = u.DENPYOU_DATE.Value;

                        // 業者
                        this.form.txt_GyoushaCd.Text = u.UNPAN_GYOUSHA_CD;
                        M_GYOUSHA daiu_Gyo = this.accessor.GetGyousha(u.UNPAN_GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (daiu_Gyo != null && daiu_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_GyoushaMei.Text = daiu_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_GyoushaCd.Text = "";
                            this.form.txt_GyoushaMei.Text = "";
                        }

                        // 車種
                        this.form.txt_ShashuCd.Text = u.SHASHU_CD;
                        M_SHASHU daiu_shu = this.accessor.GetShashu(u.SHASHU_CD);
                        if (daiu_shu != null)
                        {
                            this.form.txt_ShashuMei.Text = daiu_shu.SHASHU_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_ShashuCd.Text = "";
                            this.form.txt_ShashuMei.Text = "";
                        }

                        // 車輌
                        this.form.txt_SHARYOU_CD.Text = u.SHARYOU_CD;
                        SqlDateTime gettekiyouDate = SqlDateTime.Null;
                        if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE.Text))
                        {
                            gettekiyouDate = SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                        }
                        M_SHARYOU[] daiu_sha = this.accessor.GetSharyou(u.SHARYOU_CD, u.UNPAN_GYOUSHA_CD, "", "", gettekiyouDate);
                        this.form.txt_SHARYOU_MEI.Text = daiu_sha == null || daiu_sha.Length == 0 ? "" : daiu_sha[0].SHARYOU_NAME;

                        // 運転者
                        this.form.txt_UntenshaCd.Text = u.UNTENSHA_CD;
                        M_SHAIN daiu_shain = this.accessor.GetShain(u.UNTENSHA_CD);
                        if (daiu_shain != null && daiu_shain.UNTEN_KBN.IsTrue)
                        {
                            this.form.txt_UntenshaMei.Text = daiu_shain.SHAIN_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_UntenshaCd.Text = "";
                            this.form.txt_UntenshaMei.Text = "";
                        }

                        // 荷積業者
                        this.form.txt_NidumiGyoushaCd.Text = u.GYOUSHA_CD;
                        daiu_Gyo = this.accessor.GetGyousha(u.GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (daiu_Gyo != null && (daiu_Gyo.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || daiu_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_NidumiGyoushaMei.Text = daiu_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_NidumiGyoushaCd.Text = "";
                            this.form.txt_NidumiGyoushaMei.Text = "";
                        }

                        // 荷積現場
                        if (!string.IsNullOrEmpty(this.form.txt_NidumiGyoushaCd.Text))
                        {
                            this.form.txt_NidumiGenbaCd.Text = u.GENBA_CD;
                            M_GENBA daiu_Gen = this.accessor.GetGenba(u.GYOUSHA_CD, u.GENBA_CD);
                            // 20151104 BUNN #12040 STR
                            if (daiu_Gen != null && (daiu_Gen.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || daiu_Gen.TSUMIKAEHOKAN_KBN.IsTrue))
                            // 20151104 BUNN #12040 END
                            {
                                this.form.txt_NidumiGenbaMei.Text = daiu_Gen.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.txt_NidumiGenbaCd.Text = "";
                                this.form.txt_NidumiGenbaMei.Text = "";
                            }
                        }
                    }

                    T_UR_SH_ENTRY s = this.form.entitys == null || this.form.entitys.Length < 2 ? null : this.form.entitys[1] as T_UR_SH_ENTRY;

                    if (s != null)
                    {
                        // 荷降業者
                        this.form.txt_NIOROSHI_GYOUSHA_CD.Text = s.GYOUSHA_CD;
                        M_GYOUSHA dais_Gyo = this.accessor.GetGyousha(s.GYOUSHA_CD);
                        // 20151104 BUNN #12040 STR
                        if (dais_Gyo != null && (dais_Gyo.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || dais_Gyo.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                        // 20151104 BUNN #12040 END
                        {
                            this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = dais_Gyo.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.txt_NIOROSHI_GYOUSHA_CD.Text = "";
                            this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = "";
                        }

                        // 荷降現場
                        if (!string.IsNullOrEmpty(this.form.txt_NIOROSHI_GYOUSHA_CD.Text))
                        {
                            this.form.txt_NIOROSHI_GENBA_CD.Text = s.GENBA_CD;
                            M_GENBA dais_Gen = this.accessor.GetGenba(s.GYOUSHA_CD, s.GENBA_CD);
                            // 20151104 BUNN #12040 STR
                            if (dais_Gen != null && (dais_Gen.SAISHUU_SHOBUNJOU_KBN.IsTrue || dais_Gen.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || dais_Gen.TSUMIKAEHOKAN_KBN.IsTrue))
                            // 20151104 BUNN #12040 END
                            {
                                this.form.txt_NIOROSHI_GENBA_Mei.Text = dais_Gen.GENBA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.txt_NIOROSHI_GENBA_CD.Text = "";
                                this.form.txt_NIOROSHI_GENBA_Mei.Text = "";
                            }
                        }
                    }
                    break;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索結果を画面に表示
        /// </summary>
        internal void SetValueToForm()
        {
            bool catchErr = false;
            LogUtility.DebugMethodStart();
            try
            {
                if (this.dtUnchinEntry == null)
                {
                    return;
                }

                // No2634-->
                // 数量フォーマット
                String systemSuuryouFormat = SystemProperty.Format.Suuryou ?? "";
                // 単価フォーマット
                String systemTankaFormat = SystemProperty.Format.Tanka ?? "";
                // 重量フォーマット
                String systemJyuryouFormat = SystemProperty.Format.Jyuryou ?? "";
                // No2634<--

                #region Header
                // ヘッダフォーム設定
                // 拠点
                this.headerForm.txt_KYOTEN_CD.Text = this.dtUnchinEntry.KYOTEN_CD.IsNull ? "" : Convert.ToString(this.dtUnchinEntry.KYOTEN_CD.Value).PadLeft(2, '0');
                if (!this.dtUnchinEntry.KYOTEN_CD.IsNull)
                {
                    var kyotens = this.accessor.GetAllDataByCodeForKyoten(this.dtUnchinEntry.KYOTEN_CD.Value);
                    if (kyotens != null && kyotens.Length > 0)
                    {
                        this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = kyotens[0].KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = "";
                    }
                }
                // メインフォーム設定
                #endregion

                #region Detail_Header
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.form.txt_Denpyousyurui.Text = "160";
                    this.form.txt_DenpyousyuruiMei.Text = "運賃";
                }
                else
                {
                    switch (this.dtUnchinEntry.DENSHU_KBN_CD.Value)
                    {
                        case 1:
                            this.form.txt_Denpyousyurui.Text = "1";
                            this.form.txt_DenpyousyuruiMei.Text = "受入";
                            break;
                        case 2:
                            this.form.txt_Denpyousyurui.Text = "2";
                            this.form.txt_DenpyousyuruiMei.Text = "出荷";
                            break;
                        case 3:
                            this.form.txt_Denpyousyurui.Text = "3";
                            this.form.txt_DenpyousyuruiMei.Text = "売上支払";
                            break;
                        case 170:
                            this.form.txt_Denpyousyurui.Text = "170";
                            this.form.txt_DenpyousyuruiMei.Text = "代納";
                            break;
                        case 160:
                            this.form.txt_Denpyousyurui.Text = "160";
                            this.form.txt_DenpyousyuruiMei.Text = "運賃";
                            break;
                    }
                    this.form.txt_RenkeiNumber.Text = this.dtUnchinEntry.RENKEI_NUMBER.IsNull ? "" : this.dtUnchinEntry.RENKEI_NUMBER.Value.ToString();
                    // 伝票日付(指定)
                    this.form.DENPYOU_DATE.Value = this.dtUnchinEntry.DENPYOU_DATE.Value;
                }
                //伝票番号
                this.form.txt_DenpyouBango.Text = this.dtUnchinEntry.DENPYOU_NUMBER.Value.ToString();

                // 業者
                this.form.txt_GyoushaCd.Text = this.dtUnchinEntry.UNPAN_GYOUSHA_CD;
                this.form.txt_GyoushaMei.Text = this.dtUnchinEntry.UNPAN_GYOUSHA_NAME;

                // 車種
                this.form.txt_ShashuCd.Text = this.dtUnchinEntry.SHASHU_CD;
                this.form.txt_ShashuMei.Text = this.dtUnchinEntry.SHASHU_NAME;

                // 車輌
                this.form.txt_SHARYOU_CD.Text = this.dtUnchinEntry.SHARYOU_CD;
                this.form.txt_SHARYOU_MEI.Text = this.dtUnchinEntry.SHARYOU_NAME;

                // 運転者
                this.form.txt_UntenshaCd.Text = this.dtUnchinEntry.UNTENSHA_CD;
                this.form.txt_UntenshaMei.Text = this.dtUnchinEntry.UNTENSHA_NAME;
                // 荷降業者
                this.form.txt_NIOROSHI_GYOUSHA_CD.Text = this.dtUnchinEntry.NIOROSHI_GYOUSHA_CD;
                this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = this.dtUnchinEntry.NIOROSHI_GYOUSHA_NAME;


                // 荷降場
                this.form.txt_NIOROSHI_GENBA_CD.Text = this.dtUnchinEntry.NIOROSHI_GENBA_CD;
                this.form.txt_NIOROSHI_GENBA_Mei.Text = this.dtUnchinEntry.NIOROSHI_GENBA_NAME;

                // 荷積業者
                this.form.txt_NidumiGyoushaCd.Text = this.dtUnchinEntry.NIZUMI_GYOUSHA_CD;
                this.form.txt_NidumiGyoushaMei.Text = this.dtUnchinEntry.NIZUMI_GYOUSHA_NAME;

                //荷積現場
                this.form.txt_NidumiGenbaCd.Text = this.dtUnchinEntry.NIZUMI_GENBA_CD;
                this.form.txt_NidumiGenbaMei.Text = this.dtUnchinEntry.NIZUMI_GENBA_NAME;
                //金額計
                if (this.dtUnchinEntry.NET_TOTAL.IsNull)
                {
                    this.dtUnchinEntry.NET_TOTAL = 0;
                }
                this.form.txt_KingakuTotal.Text = this.SuuryouAndTankFormat(this.ToNDecimal(this.dtUnchinEntry.NET_TOTAL), systemJyuryouFormat, out catchErr);
                if (catchErr) { return; }
                if (string.IsNullOrEmpty(this.form.txt_KingakuTotal.Text)) this.form.txt_KingakuTotal.Text = "0";

                // 「備考」
                this.form.txt_Bikou.Text = this.dtUnchinEntry.DENPYOU_BIKOU;
                // 「形態区分」
                this.form.KEITAI_KBN_CD.Text = this.dtUnchinEntry.KEITAI_KBN_CD.IsNull ? "" : this.dtUnchinEntry.KEITAI_KBN_CD.Value.ToString();
                this.form.KEITAI_KBN_NAME_RYAKU.Text = this.dtUnchinEntry.KEITAI_KBN_NAME;
                #endregion

                #region Detail_Detail
                // 明細データを設定
                // 明細クリア
                this.dgvDetail.Rows.Clear();
                if (this.dtUnchinDetail.Length == 0)
                {
                    return;
                }

                #region //  合計金額  &&  消費税

                this.form.txt_Goukeikingaku.Text = Convert.ToString(this.dtUnchinEntry.KINGAKU_TOTAL);
                if (string.IsNullOrEmpty(this.form.txt_Goukeikingaku.Text)) this.form.txt_Goukeikingaku.Text = "0";

                #endregion

                // 画面にデータを表示

                // 明細行を追加
                this.dgvDetail.Rows.Add(this.dtUnchinDetail.Length);
                // 検索結果設定
                for (int i = 0; i < this.dtUnchinDetail.Length; i++)
                {
                    // 品名CD
                    this.dgvDetail["UNCHIN_HINMEI_CD", i].Value = this.dtUnchinDetail[i].UNCHIN_HINMEI_CD;
                    // 品名
                    this.dgvDetail["UNCHIN_HINMEI_NAME", i].Value = this.dtUnchinDetail[i].UNCHIN_HINMEI_NAME;
                    // 正味
                    if (!this.dtUnchinDetail[i].NET_JYUURYOU.IsNull)
                    {
                        this.dgvDetail["NET_JYUURYOU", i].Value = this.SuuryouAndTankFormat(this.ToNDecimal(this.dtUnchinDetail[i].NET_JYUURYOU) ?? 0, systemJyuryouFormat, out catchErr);
                        if (catchErr) { return; }
                    }
                    // 数量
                    if (!this.dtUnchinDetail[i].SUURYOU.IsNull)
                    {
                        this.dgvDetail["SUURYOU", i].Value = this.SuuryouAndTankFormat(this.ToNDecimal(this.dtUnchinDetail[i].SUURYOU) ?? 0, systemSuuryouFormat, out catchErr);
                        if (catchErr) { return; }
                    }
                    // 単位ＣＤ
                    this.dgvDetail["UNIT_CD", i].Value = this.dtUnchinDetail[i].UNIT_CD.IsNull ? "" : this.dtUnchinDetail[i].UNIT_CD.Value.ToString();
                    if (!this.dtUnchinDetail[i].UNIT_CD.IsNull && this.units != null && this.units.Length > 0)
                    {
                        // 単位
                        var unit = (from temp in this.units
                                    where temp.UNIT_CD.Value == this.dtUnchinDetail[i].UNIT_CD.Value
                                    select temp).ToArray();
                        if (unit != null && unit.Length > 0)
                        {
                            this.dgvDetail["UNIT_NAME_RYAKU", i].Value = unit[0].UNIT_NAME_RYAKU;
                        }
                        else
                        {
                            this.dgvDetail["UNIT_NAME_RYAKU", i].Value = "";
                        }
                    }
                    else
                    {
                        this.dgvDetail["UNIT_NAME_RYAKU", i].Value = "";
                    }
                    // 単価
                    if (!this.dtUnchinDetail[i].TANKA.IsNull)
                    {
                        // No2634-->
                        this.dgvDetail["TANKA", i].Value = this.SuuryouAndTankFormat(this.dtUnchinDetail[i].TANKA.Value, systemTankaFormat, out catchErr);
                        if (catchErr) { return; }
                        // No2634<--
                    }

                    // 明細備考
                    this.dgvDetail["MEISAI_BIKOU", i].Value = this.dtUnchinDetail[i].MEISAI_BIKOU;

                    //金額
                    if (!this.dtUnchinDetail[i].KINGAKU.IsNull)
                    {
                        this.dgvDetail["KINGAKU", i].Value = CommonCalc.DecimalFormat(this.ToNDecimal(this.dtUnchinDetail[i].KINGAKU) ?? 0).ToString();
                    }
                    // 明細システムID
                    this.dgvDetail["DETAIL_SYSTEM_ID", i].Value = this.dtUnchinDetail[i].DETAIL_SYSTEM_ID.Value;
                }
                #endregion
                this.CalcDetailGoukeikingaku();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }

        #region 単価、数量の共通フォーマット
        /// <summary>
        /// 単価、数量の共通フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <param name="format"></param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        public string SuuryouAndTankFormat(object num, String format, out bool catchErr)
        {
            string returnVal = string.Empty;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(num, format);

                returnVal = string.Format("{0:" + format + "}", num);

            }
            catch (Exception ex)
            {
                LogUtility.Error("SuuryouAndTankFormat", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }
        #endregion

        /// <summary>
        /// 単位設定のグリッドビュー
        /// </summary>
        internal String SetUnitToDGV(int unitCd)
        {
            LogUtility.DebugMethodStart(unitCd);
            try
            {
                M_UNIT targetUnit = null;

                M_UNIT[] units = this.GetUnit((short)unitCd);

                if (units == null)
                {
                    return "";
                }
                else
                {
                    targetUnit = units[0];
                }

                if (string.IsNullOrEmpty(targetUnit.UNIT_NAME))
                {
                    return "";
                }
                LogUtility.DebugMethodEnd();
                return targetUnit.UNIT_NAME_RYAKU.ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 単位区分取得
        /// </summary>
        /// <param name="unitCd">単位区分CD</param>
        /// <returns></returns>
        public M_UNIT[] GetUnit(short unitCd)
        {
            LogUtility.DebugMethodStart(unitCd);
            try
            {
                if (unitCd < 0)
                {
                    LogUtility.DebugMethodEnd(null);
                    return null;
                }

                M_UNIT keyEntity = new M_UNIT();
                keyEntity.UNIT_CD = unitCd;
                var units = this.unitDao.GetAllValidData(keyEntity);
                if (units == null || units.Length < 1)
                {
                    LogUtility.DebugMethodEnd(null);
                    return null;
                }
                LogUtility.DebugMethodEnd(units);
                return units;
            }
            catch
            {
                throw;
            }
        }

        #region 運賃合計金額  CalcDetailGoukeikingaku
        /// <summary>
        /// 運賃合計金額
        /// </summary>
        /// <returns></returns>
        internal bool CalcDetailGoukeikingaku()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                DataGridView dgvDetail = this.form.dgvDetail;
                decimal HinmeiKingakuTotal = 0;
                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    DataGridViewRow dr = this.form.dgvDetail.Rows[i];

                    HinmeiKingakuTotal += this.ToNDecimal(dgvDetail.Rows[i].Cells["KINGAKU"].Value) ?? 0;
                }
                this.form.txt_Goukeikingaku.Text = CommonCalc.DecimalFormat(HinmeiKingakuTotal).ToString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetailGoukeikingaku", ex);
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

        #region チェック
        internal bool HannyuusakiDateCheck()
        {
            bool ret = true;

            try
            {
                string inputNioroshiGyoushaCd = this.form.txt_NIOROSHI_GYOUSHA_CD.Text;
                string inputNioroshiGenbaCd = this.form.txt_NIOROSHI_GENBA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);
                string inputUntenshaCd = this.form.txt_UntenshaCd.Text;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return ret;
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
                    this.form.txt_NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E206", "荷降現場", "伝票日付：" + workclosedhannyuusakiEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    ret = false;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("HannyuusakiDateCheck", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal bool CheckSharyou(CancelEventArgs e)
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart(e);

                M_SHARYOU[] sharyouEntitys = null;

                // 初期化
                this.form.txt_SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.txt_SHARYOU_CD.AutoChangeBackColorEnabled = true;
                if (string.IsNullOrEmpty(this.form.txt_SHARYOU_CD.Text))
                {
                    this.form.txt_SHARYOU_MEI.Text = string.Empty;
                }
                this.form.txt_SHARYOU_MEI.ReadOnly = true;
                this.form.txt_SHARYOU_MEI.Tag = string.Empty;
                this.form.txt_SHARYOU_MEI.TabStop = false;

                if (string.IsNullOrEmpty(this.form.txt_SHARYOU_CD.Text))
                {
                    return ret;
                }
                SqlDateTime gettekiyouDate = SqlDateTime.Null;
                if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE.Text))
                {
                    gettekiyouDate = SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                }
                sharyouEntitys = this.accessor.GetSharyou(this.form.txt_SHARYOU_CD.Text, this.form.txt_GyoushaCd.Text, null, null, gettekiyouDate);

                // マスタ存在チェック
                if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                {
                    // 車輌名を編集可
                    this.ChangeShokuchiSharyouDesign();
                    // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                    this.form.txt_SHARYOU_MEI.Text = ZeroSuppress(this.form.txt_SHARYOU_CD);
                    if (!this.form.isSelectingSharyouCd)
                    {
                        this.form.isSelectingSharyouCd = true;
                    }
                    return ret;
                }

                // ポップアップから戻ってきたときに運搬業者名が無いため取得
                M_GYOUSHA unpanGyousya = this.accessor.GetGyousha(this.form.txt_GyoushaCd.Text);
                // 20151104 BUNN #12040 STR
                if (unpanGyousya != null && unpanGyousya.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151104 BUNN #12040 END
                {
                    this.form.txt_GyoushaMei.Text = unpanGyousya.GYOUSHA_NAME_RYAKU;
                }

                if (!string.IsNullOrEmpty(this.form.txt_GyoushaMei.Text))
                {
                    M_SHARYOU sharyou = new M_SHARYOU();

                    // 運搬業者チェック
                    bool isCheck = false;
                    foreach (M_SHARYOU sharyouEntity in sharyouEntitys)
                    {
                        if (sharyouEntity.GYOUSHA_CD.Equals(this.form.txt_GyoushaCd.Text) && sharyouEntity.DELETE_FLG.IsFalse)
                        {
                            isCheck = true;
                            sharyou = sharyouEntity;
                            break;
                        }
                    }

                    if (isCheck)
                    {
                        // 車輌データセット
                        SetSharyou(sharyou);
                        return ret;
                    }
                    else
                    {
                        // エラーメッセージ
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "運搬業者");
                        e.Cancel = true;
                        return ret;
                    }
                }
                else
                {
                    if (sharyouEntitys.Length > 1)
                    {
                        if (!this.form.isSelectingSharyouCd)
                        {
                            this.form.isSelectingSharyouCd = true;

                            this.form.FocusOutErrorFlag = true;

                            // この時は車輌CDを検索条件に含める
                            this.PopUpConditionsSharyouSwitch(true);

                            // 検索ポップアップ起動
                            CustomControlExtLogic.PopUp(this.form.txt_SHARYOU_CD);
                            this.PopUpConditionsSharyouSwitch(false);

                            this.form.FocusOutErrorFlag = false;
                            if (string.IsNullOrEmpty(this.form.txt_SHARYOU_MEI.Text))
                            {
                                e.Cancel = true;
                                this.form.isSelectingSharyouCd = false;
                            }
                            return ret;
                        }
                    }
                    else
                    {
                        // 一意レコード
                        // 車輌データセット
                        SetSharyou(sharyouEntitys[0]);
                    }
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckSharyou", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyou", ex);
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
        /// 車輌CD、車輌名を諸口状態のデザインへ変更する
        /// </summary>
        internal void ChangeShokuchiSharyouDesign()
        {
            this.form.txt_SHARYOU_MEI.ReadOnly = false;
            this.form.txt_SHARYOU_MEI.TabStop = true;
            this.form.txt_SHARYOU_MEI.Tag = "全角10文字以内で入力してください。";
            // 自由入力可能であるため車輌名の色を変更
            this.form.txt_SHARYOU_CD.AutoChangeBackColorEnabled = false;
            this.form.txt_SHARYOU_CD.BackColor = sharyouCdBackColor;
        }

        /// <summary>
        /// ゼロサプレス処理
        /// </summary>
        /// <param name="source">入力コントロール</param>
        /// <returns>ゼロサプレス後の文字列</returns>
        private string ZeroSuppress(object source)
        {
            string result = string.Empty;

            // 該当コントロールの最大桁数を取得
            object obj;
            decimal charactersNumber;
            string text = PropertyUtility.GetTextOrValue(source);
            if (!PropertyUtility.GetValue(source, Constans.CHARACTERS_NUMBER, out obj))
                // 最大桁数が取得できない場合はそのまま
                return text;

            charactersNumber = (decimal)obj;
            if (charactersNumber == 0 || source == null || string.IsNullOrEmpty(text))
                // 最大桁数が0または入力値が空の場合はそのまま
                return text;

            var strCharactersUmber = text;
            if (strCharactersUmber.Contains("."))
                // 小数点を含む場合はそのまま
                return text;

            // ゼロサプレスした値を返す
            StringBuilder sb = new StringBuilder((int)charactersNumber);
            string format = sb.Append('#', (int)charactersNumber).ToString();
            long val = 0;
            if (long.TryParse(text, out val))
                result = val == 0 ? "0" : val.ToString(format);
            else
                // 入力値が数値ではない場合はそのまま
                result = text;

            return result;
        }

        /// <summary>
        /// 車輌情報をセット
        /// </summary>
        /// <param name="sharyouEntity"></param>
        private void SetSharyou(M_SHARYOU sharyouEntity)
        {
            bool catchErr = false;
            this.form.txt_SHARYOU_MEI.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
            this.form.txt_UntenshaCd.Text = sharyouEntity.SHAIN_CD;
            this.form.txt_ShashuCd.Text = sharyouEntity.SHASYU_CD;
            this.form.txt_GyoushaCd.Text = sharyouEntity.GYOUSHA_CD;

            // 運転者情報セット
            var untensha = this.accessor.GetShain(sharyouEntity.SHAIN_CD);
            if (untensha != null)
            {
                this.form.txt_UntenshaMei.Text = untensha.SHAIN_NAME_RYAKU;
            }
            else
            {
                this.form.txt_UntenshaMei.Text = string.Empty;
            }

            //車種情報セット
            var shashu = this.accessor.GetShashu(sharyouEntity.SHASYU_CD);
            if (shashu != null)
            {
                this.form.txt_ShashuCd.Text = shashu.SHASHU_CD;
                this.form.txt_ShashuMei.Text = shashu.SHASHU_NAME_RYAKU;
            }
            else
            {
                this.form.txt_ShashuCd.Text = string.Empty;
                this.form.txt_ShashuMei.Text = string.Empty;
            }

            // 運搬業者名セット
            this.CheckUnpanGyoushaCd(this.form.txt_GyoushaCd.Text, null, out catchErr);
            if (catchErr)
            {
                throw (new Exception());
            }
        }

        /// <summary>
        /// 車輌PopUpの検索条件に車輌CDを含めるかを引数によって設定します
        /// </summary>
        /// <param name="isPopupConditionsSharyouCD"></param>
        internal void PopUpConditionsSharyouSwitch(bool isPopupConditionsSharyouCD)
        {
            PopupSearchSendParamDto sharyouParam = new PopupSearchSendParamDto();
            sharyouParam.And_Or = CONDITION_OPERATOR.AND;
            sharyouParam.Control = "txt_SHARYOU_CD";
            sharyouParam.KeyName = "key002";

            if (isPopupConditionsSharyouCD)
            {
                if (!this.form.txt_SHARYOU_CD.PopupSearchSendParams.Contains(sharyouParam))
                {
                    this.form.txt_SHARYOU_CD.PopupSearchSendParams.Add(sharyouParam);
                }
            }
            else
            {
                var paramsCount = this.form.txt_SHARYOU_CD.PopupSearchSendParams.Count;
                for (int i = 0; i < paramsCount; i++)
                {
                    if (this.form.txt_SHARYOU_CD.PopupSearchSendParams[i].Control == "txt_SHARYOU_CD" &&
                        this.form.txt_SHARYOU_CD.PopupSearchSendParams[i].KeyName == "key002")
                    {
                        this.form.txt_SHARYOU_CD.PopupSearchSendParams.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 運搬業者CDの存在チェック
        /// </summary>
        internal M_GYOUSHA CheckUnpanGyoushaCd(string gyousha_cd, CancelEventArgs e, out bool catchErr)
        {
            catchErr = false;
            M_GYOUSHA gyousha = new M_GYOUSHA();
            try
            {
                LogUtility.DebugMethodStart(gyousha_cd, e);

                var msgLogic = new MessageBoxShowLogic();

                gyousha = this.accessor.GetGyousha(gyousha_cd);

                if (!string.IsNullOrEmpty(this.form.txt_GyoushaCd.Text))
                {
                    if (gyousha != null)
                    {
                        // 20151023 BUNN #12040 STR
                        if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        // 20151023 BUNN #12040 END
                        {
                            var parentForm = (BusinessBaseForm)this.form.ParentForm;
                            SqlDateTime tekiyouDate = parentForm.sysDate.Date;
                            DateTime date;
                            if (!string.IsNullOrWhiteSpace(this.form.DENPYOU_DATE.Text) && DateTime.TryParse(this.form.DENPYOU_DATE.Text, out date))
                            {
                                tekiyouDate = date;
                            }
                            if (gyousha.TEKIYOU_BEGIN.IsNull && gyousha.TEKIYOU_END.IsNull)
                            {
                                this.form.txt_GyoushaMei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                            }
                            else if (gyousha.TEKIYOU_BEGIN.IsNull && !gyousha.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyousha.TEKIYOU_END) <= 0)
                            {
                                this.form.txt_GyoushaMei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                            }
                            else if (!gyousha.TEKIYOU_BEGIN.IsNull && gyousha.TEKIYOU_END.IsNull
                                    && tekiyouDate.CompareTo(gyousha.TEKIYOU_BEGIN) >= 0)
                            {
                                this.form.txt_GyoushaMei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                            }
                            else if (!gyousha.TEKIYOU_BEGIN.IsNull && !gyousha.TEKIYOU_END.IsNull
                                    && tekiyouDate.CompareTo(gyousha.TEKIYOU_BEGIN) >= 0
                                    && tekiyouDate.CompareTo(gyousha.TEKIYOU_END) <= 0)
                            {
                                this.form.txt_GyoushaMei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                            }
                            else
                            {
                                if (e != null)
                                {
                                    msgLogic.MessageBoxShow("E020", "業者");
                                    e.Cancel = true;
                                    this.form.txt_GyoushaCd.IsInputErrorOccured = true;
                                }
                                return null;
                            }
                            M_SHARYOU[] sharyouEntitys = null;
                            SqlDateTime gettekiyouDate = SqlDateTime.Null;
                            if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE.Text))
                            {
                                gettekiyouDate = SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                            }

                            sharyouEntitys = this.accessor.GetSharyou(this.form.txt_SHARYOU_CD.Text, this.form.txt_GyoushaCd.Text, null, null, gettekiyouDate);

                            if (sharyouEntitys != null && sharyouEntitys.Length > 0)
                            {
                                var sharyouEntity = sharyouEntitys[0];
                                this.form.txt_SHARYOU_CD.Text = sharyouEntity.SHARYOU_CD;
                                this.form.txt_SHARYOU_MEI.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
                                this.form.txt_SHARYOU_CD.BackColor = SystemColors.Window;
                                this.form.txt_SHARYOU_CD.AutoChangeBackColorEnabled = true;
                                this.form.txt_SHARYOU_MEI.ReadOnly = true;
                                this.form.txt_SHARYOU_MEI.Tag = string.Empty;
                                this.form.txt_SHARYOU_MEI.TabStop = false;
                                // 運転者情報セット
                                var untensha = this.accessor.GetShain(sharyouEntity.SHAIN_CD);
                                if (untensha != null)
                                {
                                    this.form.txt_UntenshaCd.Text = untensha.SHAIN_CD;
                                    this.form.txt_UntenshaMei.Text = untensha.SHAIN_NAME_RYAKU;
                                }
                                else
                                {
                                    this.form.txt_UntenshaCd.Text = string.Empty;
                                    this.form.txt_UntenshaMei.Text = string.Empty;
                                }

                                // 車輌情報セット
                                var shashuEntity = this.accessor.GetShashu(sharyouEntity.SHASYU_CD);
                                if (shashuEntity != null)
                                {
                                    this.form.txt_ShashuCd.Text = shashuEntity.SHASHU_CD;
                                    this.form.txt_ShashuMei.Text = shashuEntity.SHASHU_NAME_RYAKU;
                                }
                                else
                                {
                                    this.form.txt_ShashuCd.Text = string.Empty;
                                    this.form.txt_ShashuMei.Text = string.Empty;
                                }
                            }
                            else
                            {
                                if (this.form.txt_SHARYOU_MEI.ReadOnly)
                                {
                                    this.form.txt_SHARYOU_CD.Text = "";
                                    this.form.txt_SHARYOU_MEI.Text = "";
                                }
                            }

                            this.form.txt_GyoushaMei.Text = gyousha.GYOUSHA_NAME_RYAKU;

                        }
                        else
                        {
                            if (e != null)
                            {
                                msgLogic.MessageBoxShow("E020", "業者");
                                e.Cancel = true;
                                this.form.txt_GyoushaCd.IsInputErrorOccured = true;
                            }
                        }
                    }
                    else
                    {
                        if (e != null)
                        {
                            msgLogic.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                            this.form.txt_GyoushaCd.IsInputErrorOccured = true;
                        }
                    }
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(gyousha, catchErr);
            }
            return gyousha;
        }

        /// <summary>
        /// 荷降現場CD の存在チェック
        /// </summary>
        internal void checkNioroshiGenba(CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            // 初期化
            this.form.txt_NIOROSHI_GENBA_Mei.Text = string.Empty;

            if (string.IsNullOrEmpty(this.form.txt_NIOROSHI_GENBA_CD.Text))
            {
                this.form.txt_NIOROSHI_GENBA_Mei.Text = string.Empty;
                return;
            }
            this.form.txt_NIOROSHI_GENBA_CD.Text = this.form.txt_NIOROSHI_GENBA_CD.Text.PadLeft(6, '0');
            if (string.IsNullOrEmpty(this.form.txt_NIOROSHI_GYOUSHA_CD.Text))
            {

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "荷降業者");
                this.form.txt_NIOROSHI_GENBA_CD.Text = string.Empty;
                e.Cancel = true;
                return;
            }

            M_GENBA genba = new M_GENBA();
            genba = this.accessor.GetGenba(this.form.txt_NIOROSHI_GYOUSHA_CD.Text, this.form.txt_NIOROSHI_GENBA_CD.Text);

            //マスタ存在チェック
            if (genba == null)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "荷降現場");
                e.Cancel = true;
                return;
            }
            //現場マスタのチェック
            // 20151023 BUNN #12040 STR
            else if (genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
            // 20151023 BUNN #12040 END
            {
                var parentForm = (BusinessBaseForm)this.form.ParentForm;
                SqlDateTime tekiyouDate = parentForm.sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.DENPYOU_DATE.Text) && DateTime.TryParse(this.form.DENPYOU_DATE.Text, out date))
                {
                    tekiyouDate = date;
                }
                if (genba.TEKIYOU_BEGIN.IsNull && genba.TEKIYOU_END.IsNull)
                {
                    this.form.txt_NIOROSHI_GENBA_Mei.Text = genba.GENBA_NAME_RYAKU;
                }
                else if (genba.TEKIYOU_BEGIN.IsNull && !genba.TEKIYOU_END.IsNull
                    && tekiyouDate.CompareTo(genba.TEKIYOU_END) <= 0)
                {
                    this.form.txt_NIOROSHI_GENBA_Mei.Text = genba.GENBA_NAME_RYAKU;
                }
                else if (!genba.TEKIYOU_BEGIN.IsNull && genba.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba.TEKIYOU_BEGIN) >= 0)
                {
                    this.form.txt_NIOROSHI_GENBA_Mei.Text = genba.GENBA_NAME_RYAKU;
                }
                else if (!genba.TEKIYOU_BEGIN.IsNull && !genba.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba.TEKIYOU_BEGIN) >= 0
                        && tekiyouDate.CompareTo(genba.TEKIYOU_END) <= 0)
                {
                    this.form.txt_NIOROSHI_GENBA_Mei.Text = genba.GENBA_NAME_RYAKU;
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "荷降現場");
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "荷降現場");
                e.Cancel = true;
                return;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降業者CD の存在チェック
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal bool CheckNioroshiGyoushaCD(CancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                // 初期化
                this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.txt_NIOROSHI_GYOUSHA_CD.Text))
                {
                    return ret;
                }
                this.form.txt_NIOROSHI_GYOUSHA_CD.Text = this.form.txt_NIOROSHI_GYOUSHA_CD.Text.PadLeft(6, '0');

                this.accessor = new DBAccessor();
                var gyousha = this.accessor.GetGyousha(this.form.txt_NIOROSHI_GYOUSHA_CD.Text);
                if (gyousha == null)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "荷降業者");
                    e.Cancel = true;
                    return ret;
                }
                // 20151023 BUNN #12040 STR
                else if (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151023 BUNN #12040 END
                {
                    var parentForm = (BusinessBaseForm)this.form.ParentForm;
                    SqlDateTime tekiyouDate = parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.DENPYOU_DATE.Text) && DateTime.TryParse(this.form.DENPYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    if (gyousha.TEKIYOU_BEGIN.IsNull && gyousha.TEKIYOU_END.IsNull)
                    {
                        this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else if (gyousha.TEKIYOU_BEGIN.IsNull && !gyousha.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(gyousha.TEKIYOU_END) <= 0)
                    {
                        this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousha.TEKIYOU_BEGIN.IsNull && gyousha.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha.TEKIYOU_BEGIN) >= 0)
                    {
                        this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousha.TEKIYOU_BEGIN.IsNull && !gyousha.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha.TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(gyousha.TEKIYOU_END) <= 0)
                    {
                        this.form.txt_NIOROSHI_GYOUSHA_Mei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "荷降業者");
                        e.Cancel = true;
                        return ret;
                    }
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "荷降業者");
                    e.Cancel = true;
                    return ret;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckNioroshiGyoushaCD", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyoushaCD", ex);
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
        /// 荷積業者CD の存在チェック
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal bool CheckNizumiGyoushaCdFrom(CancelEventArgs e)
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart(e);
                this.form.txt_NidumiGyoushaMei.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.txt_NidumiGyoushaCd.Text))
                {
                    return ret;
                }
                this.accessor = new DBAccessor();
                var gyousha = this.accessor.GetGyousha(this.form.txt_NidumiGyoushaCd.Text.PadLeft(6, '0'));
                if (gyousha == null)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "荷積業者");
                    e.Cancel = true;
                    return ret;
                }
                // 20151023 BUNN #12040 STR
                else if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151023 BUNN #12040 END
                {
                    var parentForm = (BusinessBaseForm)this.form.ParentForm;
                    SqlDateTime tekiyouDate = parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.DENPYOU_DATE.Text) && DateTime.TryParse(this.form.DENPYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    if (gyousha.TEKIYOU_BEGIN.IsNull && gyousha.TEKIYOU_END.IsNull)
                    {
                        this.form.txt_NidumiGyoushaCd.Text = this.form.txt_NidumiGyoushaCd.Text.PadLeft(6, '0');
                        this.form.txt_NidumiGyoushaMei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else if (gyousha.TEKIYOU_BEGIN.IsNull && !gyousha.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(gyousha.TEKIYOU_END) <= 0)
                    {
                        this.form.txt_NidumiGyoushaCd.Text = this.form.txt_NidumiGyoushaCd.Text.PadLeft(6, '0');
                        this.form.txt_NidumiGyoushaMei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousha.TEKIYOU_BEGIN.IsNull && gyousha.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha.TEKIYOU_BEGIN) >= 0)
                    {
                        this.form.txt_NidumiGyoushaCd.Text = this.form.txt_NidumiGyoushaCd.Text.PadLeft(6, '0');
                        this.form.txt_NidumiGyoushaMei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousha.TEKIYOU_BEGIN.IsNull && !gyousha.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha.TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(gyousha.TEKIYOU_END) <= 0)
                    {
                        this.form.txt_NidumiGyoushaCd.Text = this.form.txt_NidumiGyoushaCd.Text.PadLeft(6, '0');
                        this.form.txt_NidumiGyoushaMei.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "荷積業者");
                        e.Cancel = true;
                        return ret;
                    }
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "荷積業者");
                    e.Cancel = true;
                    return ret;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckNizumiGyoushaCdFrom", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGyoushaCdFrom", ex);
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
        /// 荷積現場CD From の存在チェック
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal bool CheckNizumiGenbaCdFrom(CancelEventArgs e)
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart(e);
                // 初期化
                this.form.txt_NidumiGenbaMei.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.txt_NidumiGenbaCd.Text))
                {
                    this.form.txt_NidumiGenbaMei.Text = string.Empty;
                    return ret;
                }
                this.form.txt_NidumiGenbaCd.Text = this.form.txt_NidumiGenbaCd.Text.PadLeft(6, '0');
                if (string.IsNullOrEmpty(this.form.txt_NidumiGyoushaCd.Text))
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E051", "荷積業者");
                    this.form.txt_NidumiGenbaCd.Text = string.Empty;
                    e.Cancel = true;
                    return ret;
                }

                M_GENBA genba = new M_GENBA();
                genba = this.accessor.GetGenba(this.form.txt_NidumiGyoushaCd.Text, this.form.txt_NidumiGenbaCd.Text);

                if (genba == null)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "荷積現場");
                    e.Cancel = true;
                    return ret;
                }
                // 20151023 BUNN #12040 STR
                else if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                // 20151023 BUNN #12040 END
                {
                    var parentForm = (BusinessBaseForm)this.form.ParentForm;
                    SqlDateTime tekiyouDate = parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.DENPYOU_DATE.Text) && DateTime.TryParse(this.form.DENPYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    if (genba.TEKIYOU_BEGIN.IsNull && genba.TEKIYOU_END.IsNull)
                    {
                        this.form.txt_NidumiGenbaMei.Text = genba.GENBA_NAME_RYAKU;
                    }
                    else if (genba.TEKIYOU_BEGIN.IsNull && !genba.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba.TEKIYOU_END) <= 0)
                    {
                        this.form.txt_NidumiGenbaMei.Text = genba.GENBA_NAME_RYAKU;
                    }
                    else if (!genba.TEKIYOU_BEGIN.IsNull && genba.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba.TEKIYOU_BEGIN) >= 0)
                    {
                        this.form.txt_NidumiGenbaMei.Text = genba.GENBA_NAME_RYAKU;
                    }
                    else if (!genba.TEKIYOU_BEGIN.IsNull && !genba.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba.TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(genba.TEKIYOU_END) <= 0)
                    {
                        this.form.txt_NidumiGenbaMei.Text = genba.GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "荷積現場");
                        e.Cancel = true;
                        return ret;
                    }
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "荷積現場");
                    e.Cancel = true;
                    return ret;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckNizumiGenbaCdFrom", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGenbaCdFrom", ex);
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
        /// 品名チェック
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        internal bool CheckHinmeiCd(int rowIndex)
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart(rowIndex);
                DataGridView dgv = this.form.dgvDetail;

                var cellHINMEI_CD = dgv.Rows[rowIndex].Cells["UNCHIN_HINMEI_CD"];
                var cellHINMEI_NAME = dgv.Rows[rowIndex].Cells["UNCHIN_HINMEI_NAME"];
                var cellUNIT_CD = dgv.Rows[rowIndex].Cells["UNIT_CD"];
                var cellUNIT_NAME = dgv.Rows[rowIndex].Cells["UNIT_NAME_RYAKU"];

                cellHINMEI_NAME.Value = string.Empty;

                if (string.IsNullOrEmpty(cellHINMEI_CD.EditedFormattedValue.ToString()))
                {
                    return isErr;
                }
                var hinmei = this.accessor.GetHinmei(cellHINMEI_CD.EditedFormattedValue.ToString().PadLeft(6, '0'));
                if (hinmei == null)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運賃品名");
                    //e.Cancel = true;
                    isErr = true;
                    return isErr;
                }
                else
                {
                    cellHINMEI_NAME.Value = hinmei.UNCHIN_HINMEI_NAME;
                    cellUNIT_CD.Value = hinmei.UNIT_CD.IsNull ? "" : hinmei.UNIT_CD.ToString();
                    cellUNIT_NAME.Value = hinmei.UNIT_NAME;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckHinmeiCd", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                isErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

        internal void checkUnit(DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                DataGridView dgv = this.form.dgvDetail;

                var cellUnit_CD = dgv.Rows[e.RowIndex].Cells["UNIT_CD"];
                var cellUnit_NAME = dgv.Rows[e.RowIndex].Cells["UNIT_NAME_RYAKU"];

                cellUnit_NAME.Value = string.Empty;

                if (string.IsNullOrEmpty(cellUnit_CD.EditedFormattedValue.ToString()))
                {
                    return;
                }
                var unit = this.accessor.GetUnit(SqlInt16.Parse(cellUnit_CD.EditedFormattedValue.ToString().PadLeft(3, '0')));
                if (unit == null)
                {
                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.dgvDetail.CurrentCell, true);
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "単位CD");
                    e.Cancel = true;
                }
                else
                {
                    cellUnit_NAME.Value = unit.UNIT_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("checkUnit", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkUnit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                e.Cancel = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運転者CD の存在チェック
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal bool CheckUntenshaCd(CancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                this.form.txt_UntenshaMei.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.txt_UntenshaCd.Text))
                {
                    return ret;
                }

                this.accessor = new DBAccessor();
                var gyousha = this.accessor.GetShain(this.form.txt_UntenshaCd.Text.PadLeft(6, '0'));
                if (gyousha == null || gyousha.DELETE_FLG.IsTrue)
                {
                    this.form.txt_UntenshaCd.BackColor = Constans.ERROR_COLOR;
                    e.Cancel = true;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");

                    return ret;
                }
                else if (gyousha.UNTEN_KBN.IsTrue)
                {
                    this.form.txt_UntenshaMei.Text = gyousha.SHAIN_NAME_RYAKU;
                }
                else
                {
                    this.form.txt_UntenshaCd.BackColor = Constans.ERROR_COLOR;
                    e.Cancel = true;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");

                    return ret;
                }
                this.form.txt_UntenshaCd.Text = this.form.txt_UntenshaCd.Text.PadLeft(6, '0');
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckUntenshaCd", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntenshaCd", ex);
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

        #region ユーザー定義情報取得処理
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);
            try
            {
                string result = string.Empty;

                foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
                {
                    if (item.Name.Equals(key))
                    {
                        result = item.Value;
                    }
                }

                LogUtility.DebugMethodEnd(result);
                return result;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ヘッダーの拠点CDの存在チェック
        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // 初期化
                this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.headerForm.txt_KYOTEN_CD.Text))
                {
                    this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = string.Empty;
                    return;
                }

                short kyoteCd = -1;
                if (!short.TryParse(string.Format("{0:#,0}", this.headerForm.txt_KYOTEN_CD.Text), out kyoteCd))
                {
                    return;
                }

                var kyotens = this.accessor.GetAllDataByCodeForKyoten(kyoteCd);

                // 存在チェック
                if (kyotens == null || kyotens.Length < 1)
                {
                    // 存在しない
                    return;
                }
                else
                {
                    // キーが１つなので複数はヒットしないはず
                    M_KYOTEN kyoten = kyotens[0];
                    this.headerForm.txt_KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        internal bool IsExistData(Int64 DenpyouNumber)
        {
            LogUtility.DebugMethodStart(DenpyouNumber);
            this.dto = new UnchinEntryDTOClass();
            this.dto.DENPYOU_NUMBER = DenpyouNumber;
            T_UNCHIN_ENTRY entry = this.ueDao.GetDataToDataTableNow(this.dto);
            if (entry == null)
            {
                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }

        internal bool SearchDataAll(string DenpyouNumber, out bool catchErr, CancelEventArgs e = null, string Kbn = "")
        {
            bool ret = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(DenpyouNumber, catchErr, e, Kbn);
                this.dto = new UnchinEntryDTOClass();
                this.dto.DENPYOU_NUMBER = this.ToNInt64(DenpyouNumber) ?? SqlInt64.Null;
                // 入力データを検索
                switch (Kbn)
                {
                    case "Pre":
                        this.dto.KYOTEN = this.ToNInt16(this.headerForm.txt_KYOTEN_CD.Text) ?? SqlInt16.Null;
                        this.dtUnchinEntry = this.ueDao.GetDataToDataTablePre(this.dto);
                        break;
                    case "Next":
                        this.dto.KYOTEN = this.ToNInt16(this.headerForm.txt_KYOTEN_CD.Text) ?? SqlInt16.Null;
                        this.dtUnchinEntry = this.ueDao.GetDataToDataTableNext(this.dto);
                        break;
                    case "":
                    default:
                        this.dtUnchinEntry = this.ueDao.GetDataToDataTableNow(this.dto);
                        break;
                }

                if (this.dtUnchinEntry == null)
                {
                    if (e != null)
                    {
                        // メッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E045");

                        // 入力受付番号クリア
                        this.form.txt_DenpyouBango.Text = "";
                        e.Cancel = true;
                    }
                    return ret;
                }
                else
                {
                    this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 修正権限がない場合
                    if (r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        // 修正権限は無いが参照権限がある場合は参照モードで起動
                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }
                    else
                    {
                        // どちらも無い場合はアラートを表示して処理中断
                        MessageBoxShowLogic msg = new MessageBoxShowLogic();
                        msg.MessageBoxShow("E158", "修正");
                        e.Cancel = true;
                        return ret;
                    }
                }

                // 検索条件設定
                //運賃（収集）明細データを検索
                this.dto.SYSTEM_ID = this.dtUnchinEntry.SYSTEM_ID.Value;
                this.dto.SEQ = this.dtUnchinEntry.SEQ.Value;

                // 明細データを検索
                this.dtUnchinDetail = this.udDao.GetDataToDataTable(this.dto);
                SetValueToForm();

                ret = true;
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("SearchDataAll", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchDataAll", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        #region 単価設定 CalcTanka
        /// <summary>
        /// 単価設定
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns></returns>
        internal bool CalcTanka(DataGridViewRow targetRow)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(targetRow);
                if (targetRow == null)
                {
                    return ret;
                }
                if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["UNCHIN_HINMEI_CD"].EditedFormattedValue)))
                {
                    return ret;
                }
                if (String.IsNullOrEmpty(this.form.txt_GyoushaCd.Text))
                {
                    return ret;
                }
                if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["UNIT_CD"].EditedFormattedValue)))
                {
                    return ret;
                }

                string gyoushaCd = this.form.txt_GyoushaCd.Text;
                string hinmeiCd = Convert.ToString(targetRow.Cells["UNCHIN_HINMEI_CD"].EditedFormattedValue);
                SqlInt16 unitCd = Convert.ToInt16(Convert.ToString(targetRow.Cells["UNIT_CD"].EditedFormattedValue));
                string shashuCd = this.form.txt_ShashuCd.Text;

                // 単価
                decimal tanka = 0;
                // 単価を設定したかの判定用
                bool isSetTanka = false;

                // 個別品名単価から取得    
                var kobetsuhinmeiTanka = this.accessor.GetHinmeiTanka(gyoushaCd, hinmeiCd, unitCd, shashuCd);

                // 個別品名単価から情報が取れない場合は基本品名単価の検索
                if (kobetsuhinmeiTanka != null)
                {
                    if (decimal.TryParse(Convert.ToString(kobetsuhinmeiTanka.TANKA.Value), out tanka))
                    {
                        isSetTanka = true;
                    }
                }
                else
                {
                    kobetsuhinmeiTanka = this.accessor.GetHinmeiTanka(gyoushaCd, hinmeiCd, unitCd, "");
                    if (kobetsuhinmeiTanka != null)
                    {
                        if (decimal.TryParse(Convert.ToString(kobetsuhinmeiTanka.TANKA.Value), out tanka))
                        {
                            isSetTanka = true;
                        }
                    }
                }

                // 単価を設定
                //targetRow.Cells["TANKA"].Value = this.SuuryouAndTankFormat(tanka, SystemProperty.Format.Tanka ?? "");
                if (isSetTanka)
                {
                    targetRow.Cells["TANKA"].Value = tanka;
                }
                else
                {
                    targetRow.Cells["TANKA"].Value = null;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CalcTanka", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcTanka", ex);
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

        #region 正味合計    KingakuTotal
        /// <summary>
        /// 正味合計
        /// </summary>
        /// <returns></returns>
        internal bool KingakuTotal()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // No2634-->
                // 重量フォーマット
                String systemJyuryouFormat = SystemProperty.Format.Jyuryou ?? "";
                // No2634<--
                DataGridView dgvDetail = this.form.dgvDetail;
                decimal KingakuTotal = 0;
                for (int i = 0; i < dgvDetail.Rows.Count - 1; i++)
                {
                    DataGridViewRow dr = this.form.dgvDetail.Rows[i];
                    if (!string.IsNullOrEmpty(dgvDetail.Rows[i].Cells["NET_JYUURYOU"].FormattedValue.ToString()))
                    {
                        KingakuTotal += Convert.ToDecimal(dgvDetail.Rows[i].Cells["NET_JYUURYOU"].FormattedValue.ToString());
                    }
                }
                this.form.txt_KingakuTotal.Text = KingakuTotal.ToString("N");
                CustomTextBoxLogic customTextBoxLogic = new CustomTextBoxLogic(this.form.txt_KingakuTotal);
                customTextBoxLogic.Format(this.form.txt_KingakuTotal);
            }
            catch (Exception ex)
            {
                LogUtility.Error("KingakuTotal", ex);
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

        #region 明細金額計算
        /// <summary>
        /// 明細金額計算
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns></returns>
        internal bool CalcDetailKingaku(DataGridViewRow targetRow)
        {
            /* 登録実行時に金額計算のチェック(KingakuCheckメソッド)が実行されます。 　　　　　　　　　*/
            /* チェックの計算方法は本メソッドと同じため、修正する際はチェック処理も修正してください。 */
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(targetRow);
                if (targetRow == null)
                {
                    return ret;
                }

                decimal suuryou = 0;
                decimal tanka = 0;

                // 金額端数(四捨五入)
                short kingakuHasuuCd = 3;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells["SUURYOU"].Value), out suuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells["TANKA"].Value), out tanka))
                {
                    decimal kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);

                    /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                    if (kingaku.ToString().Length > 9)
                    {
                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                    }
                    targetRow.Cells["KINGAKU"].Value = kingaku;
                }
                else
                {
                    // 数量と単価どちらかがNull、かつ単価が編集可能の場合、金額をクリアする
                    if (!targetRow.Cells["TANKA"].ReadOnly)
                    {
                        targetRow.Cells["KINGAKU"].Value = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetailKingaku", ex);
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

        /// <summary>
        /// 明細のデータに対しROW_NOを採番します
        /// </summary>
        public void NumberingDetailRowNo()
        {
            try
            {
                if (this.form.dgvDetail.Rows.Count == 0) { return; }
                for (int i = 0; i < this.form.dgvDetail.Rows.Count - 1; i++)
                {
                    this.form.dgvDetail.Rows[i].Cells["No"].Value = i + 1;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NumberingDetailRowNo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        #region 前の運賃番号を取得
        /// <summary>
        /// 前の伝票番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">伝票番号</param>
        /// <returns>前の伝票番号</returns>

        internal String GetPreviousNumber(String tableName, String fieldName, String numberValue)
        {
            String returnVal;
            DataTable dt = new DataTable();
            string selectStr;
            if (String.IsNullOrEmpty(numberValue))
            {
                return String.Empty;
            }

            // SQL文作成(冗長にならないためsqlファイルで管理しない)

            selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
            selectStr += " WHERE " + fieldName + " < " + long.Parse(numberValue);
            selectStr += "   AND DELETE_FLG = 0 ";

            // データ取得
            dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
            if (Convert.ToString(dt.Rows[0]["MAX_NUMBER"]) == "")
            {
                selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                // データ取得
                dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
            }

            // MAX_UKETSUKE_NUMBERをセット
            returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);

            return returnVal;
        }

        internal string getPreviousNumber1(string tableName, string fieldName, string numberValue)
        {
            this.ControlInit();
            string returnVal1 = string.Empty;
            DataTable dt = new DataTable();
            string selectStr = string.Empty;
            if (String.IsNullOrEmpty(numberValue))
            {
                selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                // データ取得
                dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
                returnVal1 = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
                return returnVal1;
            }

            // SQL文作成(冗長にならないためsqlファイルで管理しない)

            selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
            selectStr += " WHERE " + fieldName + " < " + long.Parse(numberValue);
            selectStr += "   AND DELETE_FLG = 0 ";

            // データ取得
            dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
            if (Convert.ToString(dt.Rows[0]["MAX_NUMBER"]) == "")
            {
                selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                // データ取得
                dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
            }

            returnVal1 = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);

            return returnVal1;
        }

        #endregion

        #region 次の運賃番号を取得
        /// <summary>
        /// 次の伝票番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">伝票番号</param>
        /// <returns>次の伝票番号</returns>
        internal String GetNextNumber(String tableName, String fieldName, String numberValue)
        {
            String returnVal;
            DataTable dt = new DataTable();
            string selectStr;
            if (String.IsNullOrEmpty(numberValue))
            {
                return String.Empty;
            }

            // SQL文作成(冗長にならないためsqlファイルで管理しない)
            selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
            selectStr += " WHERE " + fieldName + " > " + long.Parse(numberValue);
            selectStr += "   AND DELETE_FLG = 0 ";

            // データ取得
            dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
            if (Convert.ToString(dt.Rows[0]["MIN_NUMBER"]) == "")
            {
                selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                // データ取得
                dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
            }

            // Min_UKETSUKE_NUMBERをセット
            returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);

            return returnVal;
        }

        /// <summary>
        /// 次の伝票番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">伝票番号</param>
        /// <returns>次の伝票番号</returns>
        internal string GetNextNumber1(string tableName, string fieldName, string numberValue)
        {
            this.ControlInit();
            string returnVal1 = string.Empty;
            DataTable dt = new DataTable();
            string selectStr = string.Empty;
            this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
            if (String.IsNullOrEmpty(numberValue))
            {
                selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                // データ取得
                dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
                returnVal1 = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
                return returnVal1;
            }

            // SQL文作成(冗長にならないためsqlファイルで管理しない)
            selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
            selectStr += " WHERE " + fieldName + " > " + long.Parse(numberValue);
            selectStr += "   AND DELETE_FLG = 0 ";

            // データ取得
            dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
            if (Convert.ToString(dt.Rows[0]["MIN_NUMBER"]) == "")
            {
                selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                // データ取得
                dt = this.daoUnchinEntry.GetDateForStringSql(selectStr);
            }

            returnVal1 = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);

            return returnVal1;
        }
        #endregion

        #region Entitysデータを作成
        /// <summary>
        /// Entitysデータを作成
        /// </summary>
        /// <returns></returns>
        public bool CreateEntitys()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.insEntryEntityList.Clear();
                this.insDetailEntityList.Clear();

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 運賃入力Entityを作成
                        EntryEntity = this.CreateEntryEntity();

                        // 運賃入力Entityをリストに追加
                        this.insEntryEntityList.Add(EntryEntity);
                        // 運賃明細Entityを作成
                        this.CreateDetailEntity((int)EntryEntity.SEQ, EntryEntity.SYSTEM_ID, EntryEntity.DENPYOU_NUMBER);
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 運賃入力Entityを作成
                        string updSystemID = string.Empty;
                        T_UNCHIN_ENTRY updEntryEntity = this.CreateEntryEntity();
                        int maxSEQ = int.Parse(updEntryEntity.SEQ.ToString());

                        // 運賃入力Entityをリストに追加
                        this.insEntryEntityList.Add(updEntryEntity);

                        // 運賃明細Entityを作成
                        this.CreateDetailEntity(maxSEQ, updEntryEntity.SYSTEM_ID, updEntryEntity.DENPYOU_NUMBER);
                        //}
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:

                        // 運賃入力Entityを作成
                        string delSystemID = string.Empty;
                        T_UNCHIN_ENTRY delEntryEntity = this.CreateEntryEntity();

                        int delMaxSEQ = int.Parse(delEntryEntity.SEQ.ToString());

                        // 運賃入力Entityをリストに追加
                        this.insEntryEntityList.Add(delEntryEntity);

                        // 運賃明細Entityを作成
                        this.CreateDetailEntity(delMaxSEQ, delEntryEntity.SYSTEM_ID, delEntryEntity.DENPYOU_NUMBER);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntitys", ex);
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

        private T_UNCHIN_ENTRY CreateEntryEntity()
        {
            LogUtility.DebugMethodStart();
            try
            {
                T_UNCHIN_ENTRY entryEntity = new T_UNCHIN_ENTRY();

                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    //SYSTEM_IDの採番
                    entryEntity.SYSTEM_ID = dtUnchinEntry.SYSTEM_ID;
                    // SEQ
                    entryEntity.SEQ = dtUnchinEntry.SEQ + 1;
                }
                else
                {


                    //SYSTEM_IDの採番
                    entryEntity.SYSTEM_ID = createSystemIdForUnchin();
                    // SEQ
                    entryEntity.SEQ = 1;
                }
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    entryEntity.DENPYOU_NUMBER = this.createDenpyouNumber();
                }
                else
                {
                    entryEntity.DENPYOU_NUMBER = this.ToNInt64(this.form.txt_DenpyouBango.Text) ?? SqlInt64.Null;
                }

                switch (this.form.txt_Denpyousyurui.Text)
                {
                    case "1":
                        entryEntity.DENSHU_KBN_CD = 1;
                        break;
                    case "2":
                        entryEntity.DENSHU_KBN_CD = 2;
                        break;
                    case "3":
                        entryEntity.DENSHU_KBN_CD = 3;
                        break;
                    case "170":
                        entryEntity.DENSHU_KBN_CD = 170;
                        break;
                    case "160":
                        entryEntity.DENSHU_KBN_CD = 160;
                        break;

                    default:
                        break;
                }
                if (!String.IsNullOrEmpty(this.form.txt_RenkeiNumber.Text))
                {
                    entryEntity.RENKEI_NUMBER = Convert.ToInt64(this.form.txt_RenkeiNumber.Text);
                }

                // 伝票付日
                if (this.form.DENPYOU_DATE.Value != null)
                {
                    entryEntity.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value);
                }

                // 拠点
                if (!string.IsNullOrEmpty(this.headerForm.txt_KYOTEN_CD.Text))
                {
                    entryEntity.KYOTEN_CD = SqlInt16.Parse(this.headerForm.txt_KYOTEN_CD.Text);
                }

                // 業者
                if (!string.IsNullOrEmpty(this.form.txt_GyoushaCd.Text))
                {
                    entryEntity.UNPAN_GYOUSHA_CD = this.form.txt_GyoushaCd.Text;
                    entryEntity.UNPAN_GYOUSHA_NAME = this.form.txt_GyoushaMei.Text;

                }

                //車両
                if (!string.IsNullOrEmpty(this.form.txt_SHARYOU_CD.Text))
                {
                    entryEntity.SHARYOU_CD = this.form.txt_SHARYOU_CD.Text;
                    entryEntity.SHARYOU_NAME = this.form.txt_SHARYOU_MEI.Text;
                }

                // 車種
                if (!string.IsNullOrEmpty(this.form.txt_ShashuCd.Text))
                {
                    entryEntity.SHASHU_CD = this.form.txt_ShashuCd.Text;
                    entryEntity.SHASHU_NAME = this.form.txt_ShashuMei.Text;
                }

                // 運転者
                if (!string.IsNullOrEmpty(this.form.txt_UntenshaCd.Text))
                {
                    entryEntity.UNTENSHA_CD = this.form.txt_UntenshaCd.Text;
                    entryEntity.UNTENSHA_NAME = this.form.txt_UntenshaMei.Text;
                }


                // 荷積業者
                if (!string.IsNullOrEmpty(this.form.txt_NidumiGyoushaCd.Text))
                {
                    entryEntity.NIZUMI_GYOUSHA_CD = this.form.txt_NidumiGyoushaCd.Text;
                    entryEntity.NIZUMI_GYOUSHA_NAME = this.form.txt_NidumiGyoushaMei.Text;

                }

                //  荷積現場
                if (!string.IsNullOrEmpty(this.form.txt_NidumiGenbaCd.Text))
                {
                    entryEntity.NIZUMI_GENBA_CD = this.form.txt_NidumiGenbaCd.Text;
                    entryEntity.NIZUMI_GENBA_NAME = this.form.txt_NidumiGenbaMei.Text;
                }

                // 荷降業者
                if (!string.IsNullOrEmpty(this.form.txt_NIOROSHI_GYOUSHA_CD.Text))
                {
                    entryEntity.NIOROSHI_GYOUSHA_CD = this.form.txt_NIOROSHI_GYOUSHA_CD.Text;
                    entryEntity.NIOROSHI_GYOUSHA_NAME = this.form.txt_NIOROSHI_GYOUSHA_Mei.Text;

                }

                //  荷降現場
                if (!string.IsNullOrEmpty(this.form.txt_NIOROSHI_GENBA_CD.Text))
                {
                    entryEntity.NIOROSHI_GENBA_CD = this.form.txt_NIOROSHI_GENBA_CD.Text;
                    entryEntity.NIOROSHI_GENBA_NAME = this.form.txt_NIOROSHI_GENBA_Mei.Text;
                }

                //正味合計
                if (!string.IsNullOrEmpty(this.form.txt_KingakuTotal.Text))
                {
                    decimal kingakuTotal;
                    decimal.TryParse(this.form.txt_KingakuTotal.Text, out kingakuTotal);
                    entryEntity.NET_TOTAL = kingakuTotal;
                }

                //金額合計
                if (!string.IsNullOrEmpty(this.form.txt_Goukeikingaku.Text))
                {
                    decimal kingakuTotal;
                    decimal.TryParse(this.form.txt_Goukeikingaku.Text, out kingakuTotal);
                    entryEntity.KINGAKU_TOTAL = kingakuTotal;
                }

                //形態区分
                if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                {
                    entryEntity.KEITAI_KBN_CD = Convert.ToInt16(this.form.KEITAI_KBN_CD.Text);
                    entryEntity.KEITAI_KBN_NAME = this.form.KEITAI_KBN_NAME_RYAKU.Text;
                }

                entryEntity.DENPYOU_BIKOU = this.form.txt_Bikou.Text;

                // 削除フラグ
                entryEntity.DELETE_FLG = false;

                var WHO = new DataBinderLogic<T_UNCHIN_ENTRY>(entryEntity);
                WHO.SetSystemProperty(entryEntity, false);
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    //SYSTEM_IDの採番
                    entryEntity.CREATE_DATE = dtUnchinEntry.CREATE_DATE;
                    entryEntity.CREATE_PC = dtUnchinEntry.CREATE_PC;
                    entryEntity.CREATE_USER = dtUnchinEntry.CREATE_USER;
                }
                LogUtility.DebugMethodEnd(entryEntity);
                return entryEntity;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 登録用のDetailEntityを作成
        /// </summary>
        /// <param name="SEQ">枝番</param>
        /// <param name="systemID">システムID</param>
        /// <param name="uketsukeNumber">運賃番号</param>
        private T_UNCHIN_DETAIL CreateDetailEntity(int SEQ, SqlInt64 systemID, SqlInt64 DenpyouBango)
        {
            LogUtility.DebugMethodStart(SEQ, systemID, DenpyouBango);
            try
            {
                T_UNCHIN_DETAIL detailEntity = new T_UNCHIN_DETAIL();
                for (int i = 0; i < this.dgvDetail.RowCount - 1; i++)
                {
                    detailEntity = new T_UNCHIN_DETAIL();
                    DataGridViewRow row = this.dgvDetail.Rows[i];

                    // 運賃収集入力テーブルのSYSTEM_ID
                    detailEntity.SYSTEM_ID = systemID;

                    // 受入番号の採番
                    detailEntity.DENPYOU_NUMBER = DenpyouBango;

                    // SEQ
                    detailEntity.SEQ = SEQ;

                    if (row.Cells["DETAIL_SYSTEM_ID"].Value == null || string.IsNullOrEmpty(this.ToNInt64(row.Cells["DETAIL_SYSTEM_ID"].Value).ToString()))
                    {
                        detailEntity.DETAIL_SYSTEM_ID = createSystemIdForUnchin();
                    }
                    else
                    {
                        if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && string.IsNullOrEmpty(this.form.txt_DenpyouBango.Text))
                        {
                            detailEntity.DETAIL_SYSTEM_ID = createSystemIdForUnchin();
                        }
                        else
                        {
                            detailEntity.DETAIL_SYSTEM_ID = this.ToNInt64(row.Cells["DETAIL_SYSTEM_ID"].Value) ?? createSystemIdForUnchin();
                        }
                    }

                    detailEntity.ROW_NO = this.ToNInt16(row.Cells["No"].Value) ?? SqlInt16.Null;

                    // 品名CD（必須項目）
                    detailEntity.UNCHIN_HINMEI_CD = Convert.ToString(row.Cells["UNCHIN_HINMEI_CD"].Value);

                    // 品名（必須項目）
                    detailEntity.UNCHIN_HINMEI_NAME = Convert.ToString(row.Cells["UNCHIN_HINMEI_NAME"].Value);

                    // 正味
                    detailEntity.NET_JYUURYOU = this.ToNDecimal(row.Cells["NET_JYUURYOU"].Value) ?? SqlDecimal.Null;

                    // 数量（必須項目）
                    detailEntity.SUURYOU = this.ToNDecimal(row.Cells["SUURYOU"].Value) ?? SqlDecimal.Null;

                    // 単位CD（必須項目）
                    detailEntity.UNIT_CD = this.ToNInt16(row.Cells["UNIT_CD"].Value) ?? SqlInt16.Null;

                    // 単価（必須項目）
                    detailEntity.TANKA = this.ToNDecimal(row.Cells["TANKA"].Value) ?? SqlDecimal.Null;

                    //備考
                    detailEntity.MEISAI_BIKOU = Convert.ToString(row.Cells["MEISAI_BIKOU"].Value);

                    //金額
                    detailEntity.KINGAKU = this.ToNDecimal(row.Cells["KINGAKU"].Value) ?? SqlDecimal.Null;

                    var WHO = new DataBinderLogic<T_UNCHIN_DETAIL>(detailEntity);
                    WHO.SetSystemProperty(detailEntity, false);

                    // リストに追加
                    this.insDetailEntityList.Add(detailEntity);


                }
                LogUtility.DebugMethodEnd();
                return detailEntity;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 形態区分チェック処理
        /// </summary>
        /// <returns></returns>
        internal void CheckKeitaiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                {
                    return;
                }

                short keitaiKbnCd;

                if (!short.TryParse(this.form.KEITAI_KBN_CD.Text, out keitaiKbnCd))
                {
                    return;
                }

                M_KEITAI_KBN kakuteiKbn = this.accessor.GetkeitaiKbn(keitaiKbnCd);
                if (kakuteiKbn == null || kakuteiKbn.DELETE_FLG.IsTrue)
                {
                    // エラーメッセージ
                    this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                    this.form.KEITAI_KBN_CD.BackColor = Constans.ERROR_COLOR;
                    this.form.KEITAI_KBN_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "形態区分");
                    this.form.KEITAI_KBN_CD.Focus();
                    return;
                }
                this.form.KEITAI_KBN_NAME_RYAKU.Text = kakuteiKbn.KEITAI_KBN_NAME_RYAKU;
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckKeitaiKbn", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKeitaiKbn", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #region 論理削除のEntryEntityを作成
        /// <summary>
        /// 論理削除のEntityを作成
        /// </summary>
        private void CreateDelEntryEntity()
        {
            LogUtility.DebugMethodStart();

            // 初期化
            this.delEntryEntity = new T_UNCHIN_ENTRY();

            // SYSTEM_ID(元データのシステムID)
            this.delEntryEntity.SYSTEM_ID = dtUnchinEntry.SYSTEM_ID;

            // SEQ(元データのSEQ)
            this.delEntryEntity.SEQ = dtUnchinEntry.SEQ;

            this.delEntryEntity.DENPYOU_NUMBER = dtUnchinEntry.DENPYOU_NUMBER;

            // 作成と更新情報設定
            var dbLogic = new DataBinderLogic<r_framework.Entity.T_UNCHIN_ENTRY>(this.delEntryEntity);
            dbLogic.SetSystemProperty(this.delEntryEntity, false);

            // 削除フラグ
            this.delEntryEntity.DELETE_FLG = true;

            // TIME_STAMP
            this.delEntryEntity.TIME_STAMP = dtUnchinEntry.TIME_STAMP;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SYSTEM_IDを採番
        /// <summary>
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        private SqlInt64 createSystemIdForUnchin()
        {
            LogUtility.DebugMethodStart();
            SqlInt64 returnInt = 1;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    var entity = new S_NUMBER_SYSTEM();
                    entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UNCHIN.GetHashCode();

                    // IS_NUMBER_SYSTEMDao(共通)
                    IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

                    var updateEntity = numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                    returnInt = numberSystemDao.GetMaxPlusKey(entity);

                    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                    {
                        updateEntity = new S_NUMBER_SYSTEM();
                        updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UNCHIN.GetHashCode();
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

                    tran.Commit();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnInt);
            }

            return returnInt;
        }
        #endregion

        #region 運賃番号を採番
        /// <summary>
        /// 運賃番号を採番
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createDenpyouNumber()
        {
            LogUtility.DebugMethodStart();
            SqlInt64 returnInt = -1;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    var entity = new S_NUMBER_DENSHU();
                    entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UNCHIN.GetHashCode();

                    // IS_NUMBER_DENSHUDao(共通)
                    IS_NUMBER_DENSHUDao numberDenshuDao = DaoInitUtility.GetComponent<IS_NUMBER_DENSHUDao>();
                    var updateEntity = numberDenshuDao.GetNumberDenshuDataWithTableLock(entity);
                    returnInt = numberDenshuDao.GetMaxPlusKey(entity);

                    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                    {
                        updateEntity = new S_NUMBER_DENSHU();
                        updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UNCHIN.GetHashCode();
                        updateEntity.CURRENT_NUMBER = returnInt;
                        updateEntity.DELETE_FLG = false;
                        var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                        dataBinderEntry.SetSystemProperty(updateEntity, false);

                        numberDenshuDao.Insert(updateEntity);
                    }
                    else
                    {
                        updateEntity.CURRENT_NUMBER = returnInt;
                        numberDenshuDao.Update(updateEntity);
                    }

                    tran.Commit();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnInt);
            }

            return returnInt;
        }
        #endregion

        #endregion

        #region 論理削除処理  LogicalDeleteData
        ///// <summary>
        ///// 論理削除処理
        ///// </summary>
        [Transaction]
        public bool LogicalDeleteData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {
                    this.dtUnchinEntry.DELETE_FLG = true;
                    this.ueDao.Update(this.dtUnchinEntry);
                    this.dtUnchinEntry.SEQ = this.dtUnchinEntry.SEQ.Value + 1;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //this.dtUnchinEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    this.dtUnchinEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    this.dtUnchinEntry.UPDATE_PC = SystemInformation.ComputerName;
                    this.dtUnchinEntry.UPDATE_USER = SystemProperty.UserName;
                    this.dtUnchinEntry.TIME_STAMP = null;
                    this.ueDao.Insert(this.dtUnchinEntry);
                    // コミット
                    tran.Commit();
                }
                this.RegistResult = true;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                this.RegistResult = false;
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                return false;

            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 単価と金額の活性/非活性制御

        /// <summary>
        /// 明細全行で項目のReadOnly設定を行います
        /// </summary>
        internal void SetDetailReadOnly()
        {
            LogUtility.DebugMethodStart();
            foreach (DataGridViewRow row in this.form.dgvDetail.Rows)
            {
                SetDetailReadOnly(row.Index);
            }
            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 指定された明細行で項目のReadOnly設定を行います
        /// 現在は単価と金額の設定のみ
        /// </summary>
        /// <param name="rowIndex">行番号</param>
        internal void SetDetailReadOnly(int rowIndex)
        {
            LogUtility.DebugMethodStart(rowIndex);

            if (rowIndex < 0)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            string tankaCellName = "TANKA";
            string kingakuCellName = "KINGAKU";
            DataGridViewCell tankaCell = this.dgvDetail.Rows[rowIndex].Cells[tankaCellName];
            DataGridViewCell kingakuCell = this.dgvDetail.Rows[rowIndex].Cells[kingakuCellName];

            var row = this.dgvDetail.Rows[rowIndex];

            if ((row.Cells[tankaCellName].Value == null || string.IsNullOrEmpty(row.Cells[tankaCellName].Value.ToString())) &&
                (row.Cells[kingakuCellName].Value == null || string.IsNullOrEmpty(row.Cells[kingakuCellName].Value.ToString())))
            {
                // 「単価」、「金額」どちらも空の場合、両方操作可
                this.dgvDetail.Rows[rowIndex].Cells[tankaCellName].ReadOnly = false;
                this.dgvDetail.Rows[rowIndex].Cells[kingakuCellName].ReadOnly = false;
            }
            else if (row.Cells[tankaCellName].Value != null && !string.IsNullOrEmpty(row.Cells[tankaCellName].Value.ToString()))
            {
                // 「単価」のみ入力済みの場合、「金額」操作不可
                this.dgvDetail.Rows[rowIndex].Cells[tankaCellName].ReadOnly = false;
                this.dgvDetail.Rows[rowIndex].Cells[kingakuCellName].ReadOnly = true;
            }
            else if (row.Cells[kingakuCellName].Value != null && !string.IsNullOrEmpty(row.Cells[kingakuCellName].Value.ToString()))
            {
                // 「金額」のみ入力済みの場合、「単価」操作不可
                this.dgvDetail.Rows[rowIndex].Cells[tankaCellName].ReadOnly = true;
                this.dgvDetail.Rows[rowIndex].Cells[kingakuCellName].ReadOnly = false;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 登録前チェック
        /// <summary>
        /// 【登録前チェック処理】数量*単価=金額になるかのチェックを行います
        /// </summary>
        /// <returns>true:正常</returns>
        internal bool KingakuCheck(out bool catchErr)
        {
            /* ここの計算方法は明細の金額計算と同様です。　　　　　　 */
            /* どちらかの変更を行った際にはもう一方も修正してください */

            bool val = true;
            catchErr = false;
            try
            {
                string suryouCellName = "SUURYOU";
                string tankaCellName = "TANKA";
                string kingakuCellName = "KINGAKU";

                foreach (DataGridViewRow row in this.form.dgvDetail.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    if (row.Cells[suryouCellName].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[suryouCellName].FormattedValue.ToString()) &&
                        row.Cells[tankaCellName].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[tankaCellName].FormattedValue.ToString()))
                    {
                        decimal suryo = decimal.Parse(row.Cells[suryouCellName].FormattedValue.ToString());
                        decimal tanka = decimal.Parse(row.Cells[tankaCellName].FormattedValue.ToString());
                        decimal kingaku = decimal.Parse(row.Cells[kingakuCellName].FormattedValue.ToString());
                        short kingakuHasuuCd = 3;

                        decimal tmpKingaku = CommonCalc.FractionCalc(suryo * tanka, kingakuHasuuCd);

                        if (!tmpKingaku.Equals(kingaku))
                        {
                            val = false;
                            break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("KingakuCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }

            return val;
        }
        #endregion

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
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
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}
