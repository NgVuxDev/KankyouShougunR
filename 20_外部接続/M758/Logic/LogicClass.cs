using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukaiWanSign
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
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukaiWanSign.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// フォームオブジェクト
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        internal UIHeader headerForm;

        /// <summary>
        /// メッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 電子契約APIロジッククラス
        /// </summary>
        private DenshiWanSignLogic denshiWanSignLogic;

        /// <summary>
        /// システム設定Entity
        /// </summary>
        private M_SYS_INFO sysInfo;

        /// <summary>
        /// WAN-Sign文書詳細情報Dao
        /// </summary>
        private IM_WANSIGN_KEIYAKU_INFODAO wanSignKeiyakuInfoDao;

        /// <summary>
        /// WANSIGN文書状態Dao
        /// </summary>
        private IM_ITAKU_LINK_WANSIGN_KEIYAKUDAO linkWanSignKeiyakuDao;

        /// <summary>
        /// 委託契約基本Dao
        /// </summary>
        private IM_ITAKU_KEIYAKU_KIHONDao itakuKeiyakuKihonDao;
        /// <summary>
        /// 業者Dao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// 現場Dao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// WAN-Sign文書詳細情報リスト
        /// </summary>
        private List<M_WANSIGN_KEIYAKU_INFO> listKeiyakuInfo = null;

        /// <summary>
        /// システムIDリスト
        /// </summary>
        internal Dictionary<string, List<long>> dicWanSignSysId = null;

        /// <summary>
        /// 紐付SystemID
        /// </summary>
        internal string oldSystemId = string.Empty;

        /// <summary>
        /// エラー
        /// </summary>
        internal bool isErr = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">UIForm</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            // フォーム初期化
            this.parentForm = (BusinessBaseForm)this.form.Parent;
            this.headerForm = (UIHeader)parentForm.headerForm;

            // メッセージ用
            this.msgLogic = new MessageBoxShowLogic();

            this.wanSignKeiyakuInfoDao = DaoInitUtility.GetComponent<IM_WANSIGN_KEIYAKU_INFODAO>();
            this.linkWanSignKeiyakuDao = DaoInitUtility.GetComponent<IM_ITAKU_LINK_WANSIGN_KEIYAKUDAO>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.itakuKeiyakuKihonDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>();
            this.denshiWanSignLogic = new DenshiWanSignLogic();
            this.dicWanSignSysId = new Dictionary<string, List<long>>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //条件ｸﾘｱ
                this.Clear();

                // システム設定の読み込み
                this.GetSysInfo();

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// ボタン設定の読み込み
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonsetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            return buttonsetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// ファンクションボタンの制御
        /// </summary>
        internal void ButtonEnabledControl()
        {
            // ヘッダ初期化
            this.headerForm.readDataNumber.Text = "0";

            // 初期化
            this.parentForm.bt_func1.Enabled = true;
            this.parentForm.bt_func3.Enabled = true;
            this.parentForm.bt_func5.Enabled = true;
            this.parentForm.bt_func7.Enabled = true;
            this.parentForm.bt_func8.Enabled = true;
            this.parentForm.bt_func9.Enabled = true;
            this.parentForm.bt_func10.Enabled = true;
            this.parentForm.bt_func11.Enabled = true;
            this.parentForm.bt_func12.Enabled = true;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            // 電子契約照会（WAN-Sign）(F1)イベント作成
            this.parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);

            // 修正(F3)イベント作成
            this.parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);

            // 契約参照(F5)イベント作成
            this.parentForm.bt_func5.Click += new EventHandler(this.form.bt_func5_Click);

            // 条件クリア(F7)イベント作成
            this.parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            // 検索(F8)イベント作成
            this.parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            // 登録(F9)イベント作成
            this.parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            // 並び替え(F10)イベント作成
            this.parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            // フィルタ(F11)イベント作成
            this.parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            // 閉じる(F12)イベント作成
            this.parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // パターン登録
            this.parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            // 契約書ダウンロード
            this.parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

            // 紐付補助
            this.parentForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);

            //customDataGridView1_CellValidating
            this.form.customDataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(this.form.customDataGridView1_CellValidating);

            //customDataGridView1_CellClick
            this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(this.form.customDataGridView1_CellClick);

            //customDataGridView1_CellEnter
            this.form.customDataGridView1.CellEnter += new DataGridViewCellEventHandler(this.form.customDataGridView1_CellEnter);

            //customDataGridView1_CellFormatting
            this.form.customDataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.form.customDataGridView1_CellFormatting);

            //customDataGridView1_CellDoubleClick
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.form.customDataGridView1_CellDoubleClick);

        }

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            this.sysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData().FirstOrDefault();

            if (this.sysInfo != null)
            {
                this.headerForm.AlertNumber.Text = this.sysInfo.ICHIRAN_ALERT_KENSUU.ToString();
            }
        }

        #endregion

        #region F1 電子契約照会（WAN-Sign）
        /// <summary>
        /// 電子契約最新照会（WAN-Sign）
        /// </summary>
        internal void DenshiKeiyakuShoukaiWanSign()
        {
            //パターン一覧
            if (this.form.Table == null ||
                this.form.PatternNo == 0)
            {
                return;
            }

            #region 1．アクセストークン生成API
            //アクセストークン取得
            var accessToken = this.denshiWanSignLogic.GetAccessTokenWanSign();

            if (accessToken == null)
            {
                return;
            }

            var dateTimeOut = DateTime.Now.AddMinutes(30);
            #endregion

            #region 2．関連コード取得API
            //関連コード取得
            var controlNumber = this.denshiWanSignLogic.GetControlNumberWanSign(accessToken.Result.Access_Token);

            if (controlNumber == null ||
                controlNumber.Result == null)
            {
                return;
            }

            //APIレスポンス-satus≠０（0以外）
            if (!"0".Equals(controlNumber.Status))
            {
                //メッセージB表示
                this.msgLogic.MessageBoxShowError(string.Format(ConstCls.MsgB, controlNumber.Status));

                return;
            }
            #endregion

            #region 3．文書詳細情報取得API
            //文書詳細情報リスト
            this.listKeiyakuInfo = new List<M_WANSIGN_KEIYAKU_INFO>();

            //関連コードリスト
            var listControlNumber = new List<string>();

            //関連コード取得リスト
            if (controlNumber.Result.Document != null && controlNumber.Result.Document.Count > 0)
            {
                foreach (var item in controlNumber.Result.Document)
                {
                    //アクセストークンの有効期限は発行された時間を起点として30分です。
                    //各APIで有効期限が切れていると判定された場合は当APIを実行し新たなアクセストークンを取得してください。
                    var dateNow = DateTime.Now;

                    if (dateNow > dateTimeOut)
                    {
                        accessToken = this.denshiWanSignLogic.GetAccessTokenWanSign();
                        if (accessToken == null)
                        {
                            this.listKeiyakuInfo = null;

                            return;
                        }
                    }

                    //３．１関連コードチェック
                    //関連コード＝BLANK
                    if (string.IsNullOrEmpty(item.Control_Number) ||
                        listControlNumber.Contains(item.Control_Number))
                    {
                        continue;
                    }

                    //関連コードリスト
                    listControlNumber.Add(item.Control_Number);

                    //トランザクションID＝有り
                    //３．２文書詳細情報取得API　
                    var keiyakuInfo = this.denshiWanSignLogic.GetKeyakuInfoWanSign(accessToken.Result.Access_Token, item.Control_Number);

                    if (keiyakuInfo == null ||
                        keiyakuInfo.Result == null)
                    {
                        this.listKeiyakuInfo = null;

                        return;
                    }

                    //APIレスポンス-satus≠０（0以外）
                    if (!"0".Equals(keiyakuInfo.Status))
                    {
                        //メッセージC表示
                        this.msgLogic.MessageBoxShowError(string.Format(ConstCls.MsgC, keiyakuInfo.Status));

                        this.listKeiyakuInfo = null;
                        return;
                    }

                    //レスポンスーステータス＝0  
                    if (keiyakuInfo.Result.Document_Detail_Info != null &&
                        keiyakuInfo.Result.Document_Detail_Info.Count > 0)
                    {
                        foreach (var ite in keiyakuInfo.Result.Document_Detail_Info)
                        {
                            var tmp = this.CreateWanSignKeiyakuInfo(item.Xid, ite, item.Signing_Date, item.Created_Date);
                            var dataBinderLogic = new DataBinderLogic<M_WANSIGN_KEIYAKU_INFO>(tmp);

                            dataBinderLogic.SetSystemProperty(tmp, false);

                            this.listKeiyakuInfo.Add(tmp);
                        }
                    }
                }
            }

            //WAN-Sign文書詳細情報のデータを登録
            if (this.RegistWanSignKeiyakuInfo())
            {
                this.form.bt_func8_Click(null, null);
            }
            #endregion
        }

        /// <summary>
        /// 文書詳細情報
        /// </summary>
        /// <param name="xid"></param>
        /// <param name="data"></param>
        /// <param name="signingDate"></param>
        /// <param name="createdDate"></param>
        /// <returns></returns>
        internal M_WANSIGN_KEIYAKU_INFO CreateWanSignKeiyakuInfo(string xid, KEIYAKU_INFO_WAN_SIGN data, string signingDate, string createdDate)
        {
            var dto = new M_WANSIGN_KEIYAKU_INFO();

            //システムID
            dto.WANSIGN_SYSTEM_ID = SqlInt64.Null;
            //トランザクションID
            dto.XID = xid;
            //関連コード
            dto.CONTROL_NUMBER = data.Control_Number;
            //管理番号
            dto.ORIGINAL_CONTROL_NUMBER = data.Original_Control_Number;
            //DocumentID
            dto.DOCUMENT_ID = data.Document_Id;
            //文書管理ラベル表示済
            dto.IS_VIEW_DOC_CONTROL = data.Is_View_Doc_Control_Label;
            //箱番号
            dto.BOX_NUMBER = data.Box_Number;
            //有効無効
            dto.IS_VALID = (short)(string.IsNullOrEmpty(data.Is_Valid) ? 0 : (data.Is_Valid.Equals(ConstCls.INVALID) ? 1 : 2));
            //文書名
            dto.DOCUMENT_NAME = data.Document_Name;
            //契約日
            dto.CONTRACT_DATE = string.IsNullOrEmpty(data.Contract_Date) ? SqlDateTime.Null : SqlDateTime.Parse(data.Contract_Date);
            //契約満了日
            dto.CONTRACT_EXPIRATION_DATE = string.IsNullOrEmpty(data.Contract_Expiration_Date) ? SqlDateTime.Null : SqlDateTime.Parse(data.Contract_Expiration_Date);
            //自動更新有無
            dto.IS_AUTO_UPDATING = string.IsNullOrEmpty(data.Is_Auto_Updating) ? 0 : SqlInt16.Parse(data.Is_Auto_Updating);
            //更新期間
            dto.RENEWWAL_PERIOD = data.Renewal == null ? string.Empty : data.Renewal.Period;
            //更新単位
            dto.RENEWWAL_PERIOD_UNIT = data.Renewal == null ? string.Empty : data.Renewal.Period_Unit;
            //解約通知期間
            dto.CANCEL_PERIOD = data.Cancel == null ? string.Empty : data.Cancel.Period;
            //解約通知単位
            dto.CANCEL_PERIOD_UNIT = data.Cancel == null ? string.Empty : data.Cancel.Period_Unit;
            //リマインド通知有無
            dto.IS_REMINDER = string.IsNullOrEmpty(data.Is_Reminder) ? 0 : SqlInt16.Parse(data.Is_Reminder);
            //リマインド通知期間
            dto.REMINDER_PERIOD = data.Reminder == null ? string.Empty : data.Reminder.Period;
            //リマインド通知単位
            dto.REMINDER_PERIOD_UNIT = data.Reminder == null ? string.Empty : data.Reminder.Period_Unit;
            //所属
            dto.POST_NM = data.Post_Name;
            //送信者
            dto.NAME_NM = data.Soushin_Name;
            //契約金額
            var kingaku = SqlDecimal.Null;
            if (!string.IsNullOrEmpty(data.Contract_Decimal))
            {
                decimal kingakuTmp = 0;
                if (decimal.TryParse(data.Contract_Decimal, out kingakuTmp))
                {
                    kingaku = (SqlDecimal)kingakuTmp;
                }
            }
            dto.CONTRACT_DECIMAL = kingaku;
            //保管場所
            dto.STORAGE_LOCATION = data.Storage_Location;
            //備考１
            dto.COMMENT_1 = data.Comment_1.Length > 380 ? data.Comment_1.Substring(0, 380) : data.Comment_1;
            //備考２
            dto.COMMENT_2 = data.Comment_2.Length > 380 ? data.Comment_2.Substring(0, 380) : data.Comment_2;
            //備考３
            dto.COMMENT_3 = data.Comment_3.Length > 380 ? data.Comment_3.Substring(0, 380) : data.Comment_3;
            //フィールド1
            dto.FIELD_1 = data.Field_1.Length > 380 ? data.Field_1.Substring(0, 380) : data.Field_1;
            //フィールド2
            dto.FIELD_2 = data.Field_2.Length > 380 ? data.Field_2.Substring(0, 380) : data.Field_2;
            //フィールド3
            dto.FIELD_3 = data.Field_3.Length > 380 ? data.Field_3.Substring(0, 380) : data.Field_3;
            //フィールド4
            dto.FIELD_4 = data.Field_4.Length > 380 ? data.Field_4.Substring(0, 380) : data.Field_4;
            //フィールド5
            dto.FIELD_5 = data.Field_5.Length > 380 ? data.Field_5.Substring(0, 380) : data.Field_5;
            //相手方
            dto.PARTNER_ORGANIZE_NM = (data.Partner != null && data.Partner.Count > 0) ? StringExtension.SubStringByByte(data.Partner[0].Partner_Organize_Name, 120) : string.Empty;

            //#161407 20220314 CongBinh S
            dto.PARTNER_ORGANIZE_NM2 = (data.Partner != null && data.Partner.Count > 1) ? StringExtension.SubStringByByte(data.Partner[1].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM3 = (data.Partner != null && data.Partner.Count > 2) ? StringExtension.SubStringByByte(data.Partner[2].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM4 = (data.Partner != null && data.Partner.Count > 3) ? StringExtension.SubStringByByte(data.Partner[3].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM5 = (data.Partner != null && data.Partner.Count > 4) ? StringExtension.SubStringByByte(data.Partner[4].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM6 = (data.Partner != null && data.Partner.Count > 5) ? StringExtension.SubStringByByte(data.Partner[5].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM7 = (data.Partner != null && data.Partner.Count > 6) ? StringExtension.SubStringByByte(data.Partner[6].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM8 = (data.Partner != null && data.Partner.Count > 7) ? StringExtension.SubStringByByte(data.Partner[7].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM9 = (data.Partner != null && data.Partner.Count > 8) ? StringExtension.SubStringByByte(data.Partner[8].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM10 = (data.Partner != null && data.Partner.Count > 9) ? StringExtension.SubStringByByte(data.Partner[9].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM11 = (data.Partner != null && data.Partner.Count > 10) ? StringExtension.SubStringByByte(data.Partner[10].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM12 = (data.Partner != null && data.Partner.Count > 11) ? StringExtension.SubStringByByte(data.Partner[11].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM13 = (data.Partner != null && data.Partner.Count > 12) ? StringExtension.SubStringByByte(data.Partner[12].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM14 = (data.Partner != null && data.Partner.Count > 13) ? StringExtension.SubStringByByte(data.Partner[13].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM15 = (data.Partner != null && data.Partner.Count > 14) ? StringExtension.SubStringByByte(data.Partner[14].Partner_Organize_Name, 120) : string.Empty;
            dto.PARTNER_ORGANIZE_NM16 = (data.Partner != null && data.Partner.Count > 15) ? StringExtension.SubStringByByte(data.Partner[15].Partner_Organize_Name, 120) : string.Empty;
            //#161407 20220314 CongBinh E

            //署名完了日時
            dto.SIGNING_DATETIME = string.IsNullOrEmpty(signingDate) ? SqlDateTime.Null : SqlDateTime.Parse(signingDate);
            //文書登録日時
            dto.CREATED_AT = string.IsNullOrEmpty(createdDate) ? SqlDateTime.Null : SqlDateTime.Parse(createdDate);
            //親契約ー関連コード
            dto.PARENT_CONTROL_NUMBER = data.Parent_Document == null ? string.Empty : data.Parent_Document.Control_Number;
            //親契約ー文書名
            dto.PARENT_DOCUMENT_NAME = data.Parent_Document == null ? string.Empty : data.Parent_Document.Document_Name;
            //子契約ー関連コード
            dto.CHILD_CONTROL_NUMBER = (data.Child_Document == null || data.Child_Document.Count == 0) ? string.Empty : data.Child_Document[0].Control_Number;
            //子契約ー文書名
            dto.CHILD_DOCUMENT_NAME = (data.Child_Document == null || data.Child_Document.Count == 0) ? string.Empty : data.Child_Document[0].Document_Name;
            //登録者名
            dto.REGISTERED_USER_NAME = data.Registered_User_Name;
            //削除フラグ
            dto.DELETE_FLG = false;

            return dto;
        }

        /// <summary>
        /// WAN-Sign文書詳細情報のデータを登録
        /// </summary>
        /// <returns></returns>
        internal bool RegistWanSignKeiyakuInfo()
        {
            LogUtility.DebugMethodStart();
            bool ret = false;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    //WAN-Sign文書詳細情報のデータを登録
                    if (this.listKeiyakuInfo != null &&
                        this.listKeiyakuInfo.Count > 0)
                    {
                        var sysId = this.wanSignKeiyakuInfoDao.GetMaxPlusKey();

                        foreach (var item in this.listKeiyakuInfo)
                        {
                            //WAN-Sign文書詳細情報
                            var keiyakuInfo = this.wanSignKeiyakuInfoDao.GetDataByControlNumber(item.CONTROL_NUMBER);

                            if (keiyakuInfo != null)
                            {
                                item.WANSIGN_SYSTEM_ID = keiyakuInfo.WANSIGN_SYSTEM_ID;
                                item.CREATE_USER = keiyakuInfo.CREATE_USER;
                                item.CREATE_DATE = keiyakuInfo.CREATE_DATE;
                                item.CREATE_PC = keiyakuInfo.CREATE_PC;
                                item.TIME_STAMP = keiyakuInfo.TIME_STAMP;

                                this.wanSignKeiyakuInfoDao.Update(item);
                            }
                            else
                            {
                                item.WANSIGN_SYSTEM_ID = sysId++;

                                this.wanSignKeiyakuInfoDao.Insert(item);
                            }

                            //４．委託契約書、電子契約書 自動紐付処理（自動紐付条件１）
                            //委託契約書
                            var itakuKeiyakuKihon = this.itakuKeiyakuKihonDao.GetDataByKeiyakuNo(item.ORIGINAL_CONTROL_NUMBER);

                            //　未紐付状態のWANSIGN文書詳細情報（新規テーブル）と委託契約基本情報（M_ITAKU_KEIYAKU_KIHON）紐付　
                            if (itakuKeiyakuKihon != null &&
                                itakuKeiyakuKihon.Length == 1)
                            {
                                //委託契約WANSIGN連携
                                var linkWanSign = this.linkWanSignKeiyakuDao.GetDataBySystemId(item.WANSIGN_SYSTEM_ID.Value, long.Parse(itakuKeiyakuKihon[0].SYSTEM_ID));

                                if (linkWanSign == null)
                                {
                                    linkWanSign = new M_ITAKU_LINK_WANSIGN_KEIYAKU();

                                    linkWanSign.WANSIGN_SYSTEM_ID = item.WANSIGN_SYSTEM_ID;
                                    linkWanSign.SYSTEM_ID = long.Parse(itakuKeiyakuKihon[0].SYSTEM_ID);
                                    linkWanSign.DOCUMENT_ID = item.DOCUMENT_ID;

                                    this.linkWanSignKeiyakuDao.Insert(linkWanSign);
                                }
                            }
                        }
                    }

                    tran.Commit();
                    ret = true;

                    //メッセージEを表示
                    this.msgLogic.MessageBoxShowInformation(ConstCls.MsgE);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistWanSignKeiyakuInfo", ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret;

        }

        #endregion

        #region F3 修正
        /// <summary>
        /// 電子詳細情報入力画面を表示
        /// </summary>
        internal void OpenDenshiKeiyaku()
        {
            //明細行＝0件の状態で押下した場合
            if (this.form.customDataGridView1.RowCount == 0)
            {
                //メッセージF表示
                this.msgLogic.MessageBoxShow("E339");

                return;
            }

            //PhuocLoc 2022/01/18 #158901, #158902, #158903, #158904 -Start
            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            string WansignSystemId = string.Empty;
            string KeiyakuSystemId = string.Empty;

            if (row.Cells[ConstCls.HIDDEN_WANSIGN_SYSTEM_ID].Value != null)
            {
                WansignSystemId = row.Cells[ConstCls.HIDDEN_WANSIGN_SYSTEM_ID].Value.ToString();
            }

            if (row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value != null)
            {
                KeiyakuSystemId = row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString();
            }

            //明細行を選択している場合
            //電子詳細情報入力画面を表示
            FormManager.OpenFormWithAuth("M760", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WansignSystemId, KeiyakuSystemId);
            //PhuocLoc 2022/01/18 #158901, #158902, #158903, #158904 -End
        }
        #endregion

        #region F5 契約参照
        /// <summary>
        /// 委託契約情報検索を表示
        /// </summary>
        internal void OpenItakuKeiyakuKensaku()
        {
            //明細行＝0件の状態で押下した場合
            if (this.form.customDataGridView1.RowCount == 0)
            {
                //メッセージF表示
                this.msgLogic.MessageBoxShow("E339");

                return;
            }

            //明細行を選択している場合
            //委託契約情報検索を表示
            if (this.form.customDataGridView1.CurrentRow != null)
            {
                var index = this.form.customDataGridView1.CurrentRow.Index;

                this.SetItakuKeiyakuKensaku(index);
            }
        }

        /// <summary>
        /// 委託契約情報検索
        /// </summary>
        /// <param name="index"></param>
        internal void SetItakuKeiyakuKensaku(int index)
        {
            try
            {
                LogUtility.DebugMethodStart(index);

                // 委託契約情報検索(G475)をモーダル表示
                var popUpHeadForm = new Shougun.Core.Common.ItakuKeiyakuSearch.HeaderForm();
                var popUpForm = new Shougun.Core.Common.ItakuKeiyakuSearch.ItakuKeiyakuSearchForm(popUpHeadForm);
                BasePopForm popForm = new BasePopForm(popUpForm, popUpHeadForm);
                var documentId = Convert.ToString(this.form.customDataGridView1[ConstCls.HIDDEN_DOCUMENT_ID, index].Value);

                popForm.ShowDialog();

                // 実行結果
                switch (popUpForm.DialogResult)
                {
                    case DialogResult.OK:
                        this.oldSystemId = popUpForm.retSystemId;

                        foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                        {
                            var documentIdTmp = Convert.ToString(row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value);

                            if (documentId.Equals(documentIdTmp))
                            {
                                row.Cells[ConstCls.CELL_SYSTEM_ID].Value = popUpForm.retSystemId;
                            }
                        }

                        this.form.customDataGridView1.RefreshEdit();

                        break;

                    case DialogResult.Cancel:
                        // 何もしない
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetItakuKeiyakuKensaku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 入力した紐付SystemIDで委託契約書マスタを検索
        /// </summary>
        /// <param name="sysId">紐付SystemID</param>
        /// <param name="documentId">ドキュメントID</param>
        /// <param name="controlNumber">関連コード</param>
        /// <returns></returns>
        internal bool CheckExistsItakuKeiyaku(string sysId, string documentId, string controlNumber)
        {
            this.isErr = false;

            if (!string.IsNullOrEmpty(sysId))
            {
                var keiyaku = this.itakuKeiyakuKihonDao.GetDataBySystemId(new M_ITAKU_KEIYAKU_KIHON() { SYSTEM_ID = sysId });

                //検索結果＝0件（入力した管理番号の委託契約書がない）
                if (keiyaku == null)
                {
                    //メッセージH　を表示
                    this.msgLogic.MessageBoxShowError(ConstCls.MsgH);
                    this.isErr = true;

                    return false;
                }
            }

            //電検索結果＝1件（入力したSystemIDの委託契約書が１対１の場合）
            var lstWanSign = this.dicWanSignSysId[documentId];

            if (lstWanSign.Count > 1 && 
                !string.IsNullOrEmpty(this.oldSystemId) &&
                !string.IsNullOrEmpty(sysId) &&
                !this.oldSystemId.Equals(sysId))
            {
                var res = this.msgLogic.MessageBoxShowConfirm(string.Format(ConstCls.MsgI, controlNumber));

                if (res == DialogResult.No)
                {
                    this.isErr = true;

                    return true;
                }
            }

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                var wanSignSysId = Convert.ToInt64(row.Cells[ConstCls.HIDDEN_WANSIGN_SYSTEM_ID].Value);

                if (lstWanSign.Contains(wanSignSysId))
                {
                    row.Cells[ConstCls.CELL_SYSTEM_ID].Value = sysId;
                }
            }       

            this.form.customDataGridView1.RefreshEdit();

            return true;
        }
        #endregion

        #region F7 条件クリア

        /// <summary>
        /// 検索条件クリア
        /// </summary>
        internal void Clear()
        {
            LogUtility.DebugMethodStart();

            //管理番号（WAN）
            this.form.ORIGINAL_CONTROL_NUMBER.Text = string.Empty;

            //紐付の状態
            this.form.HIMODZUKE_JOUTAI.Text = "3";

            //排出事業者（CD）
            this.form.GYOUSHA_CD.Text = string.Empty;

            //業者名
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;

            //排出事業場（CD）
            this.form.GENBA_CD.Text = string.Empty;

            //現場名
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;

            //相手方名称
            this.form.PARTNER_ORGANIZE_NM.Text = string.Empty;

            //文書名
            this.form.DOCUMENT_NAME.Text = string.Empty;

            //契約日（WAN）（From）
            this.form.CONTRACT_DATE_FROM.Text = string.Empty;

            //契約日（WAN）（To）
            this.form.CONTRACT_DATE_TO.Text = string.Empty;

            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            // フォーカスを契約番号にする。
            this.form.ORIGINAL_CONTROL_NUMBER.Focus();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F8 検索

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            this.form.Table = null;

            if (!string.IsNullOrEmpty(this.form.SelectQuery))
            {
                // 検索文字列の作成
                var sql = this.MakeSearchCondition();

                this.form.Table = this.daoGyousha.GetDateForStringSql(sql);
            }

            // 検索結果を画面に表示
            this.form.ShowData();

            this.form.customDataGridView1.IsBrowsePurpose = false;

            // 紐付SystemIDの制御処理
            this.SetActiveCol();

            // 紐付不可理由
            this.SetImpossibleReason();

            // 読込データ件数を表示する。
            if (this.form.customDataGridView1 != null)
            {
                this.headerForm.readDataNumber.Text = this.form.customDataGridView1.RowCount.ToString();
            }
            else
            {
                this.headerForm.readDataNumber.Text = "0";
            }

            this.SetListWanSignSysId();

            return this.form.customDataGridView1 != null ? this.form.customDataGridView1.RowCount : 0;

        }

        /// <summary>
        /// 契約日（WAN）チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.CONTRACT_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.CONTRACT_DATE_TO.BackColor = Constans.NOMAL_COLOR;

            //入力されない場合
            if (string.IsNullOrEmpty(this.form.CONTRACT_DATE_FROM.Text) ||
                string.IsNullOrEmpty(this.form.CONTRACT_DATE_TO.Text))
            {
                return true;
            }

            DateTime date_from = DateTime.Parse(this.form.CONTRACT_DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.CONTRACT_DATE_TO.Text);

            //契約日（WAN）FROM > 契約日（WAN）TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.CONTRACT_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.CONTRACT_DATE_TO.BackColor = Constans.ERROR_COLOR;

                string[] errorMsg = { "契約日（WAN）From", "契約日（WAN）To" };
                this.msgLogic.MessageBoxShow("E030", errorMsg);

                this.form.CONTRACT_DATE_FROM.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// 紐付SystemIDの制御処理
        /// </summary>
        internal void SetActiveCol()
        {
            //紐付SystemID
            if (this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_SYSTEM_ID))
            {
                DataGridViewColumn column = this.form.customDataGridView1.Columns[ConstCls.CELL_SYSTEM_ID];
                this.form.customDataGridView1.Columns.Remove(column);

                DgvCustomAlphaNumTextBoxColumn newColumn = new DgvCustomAlphaNumTextBoxColumn();

                newColumn.Name = ConstCls.CELL_SYSTEM_ID;
                newColumn.MaxInputLength = 9;
                newColumn.CharactersNumber = 9;
                newColumn.DataPropertyName = column.DataPropertyName;
                newColumn.ZeroPaddengFlag = true;
                newColumn.AlphabetLimitFlag = false;

                this.form.customDataGridView1.Columns.Insert(column.Index, newColumn);
            }

            //紐付不可理由
            if (this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_HIMODZUKE_FUKA_RIYUU))
            {
                DataGridViewColumn column = this.form.customDataGridView1.Columns[ConstCls.CELL_HIMODZUKE_FUKA_RIYUU];
                this.form.customDataGridView1.Columns.Remove(column);

                DgvCustomTextBoxColumn newColumn = new DgvCustomTextBoxColumn();

                newColumn.Name = ConstCls.CELL_HIMODZUKE_FUKA_RIYUU;
                newColumn.DataPropertyName = column.DataPropertyName;
                newColumn.ReadOnly = true;
                newColumn.MaxInputLength = 40;
                newColumn.CharactersNumber = 40;

                this.form.customDataGridView1.Columns.Insert(column.Index, newColumn);
            }

        }

        /// <summary>
        /// 紐付不可理由
        /// </summary>
        private void SetImpossibleReason()
        {

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                var sysId = Convert.ToString(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value);
                var kanrid = Convert.ToString(row.Cells[ConstCls.HIDDEN_ORIGINAL_CONTROL_NUMBER].Value);
                var sql = " SELECT SYSTEM_ID FROM M_ITAKU_KEIYAKU_KIHON WHERE ITAKU_KEIYAKU_NO = '";//#161240 20220308 CongBinh
                //紐付不可理由
                if (string.IsNullOrEmpty(sysId) &&
                    this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_HIMODZUKE_FUKA_RIYUU))
                {
                    //管理番号＝入力有
                    if (!string.IsNullOrEmpty(kanrid))
                    {
                        //#161240 20220308 CongBinh S
                        sql += kanrid + "'";
                        var tmp = this.daoGyousha.GetDateForStringSql(sql);
                        if (tmp != null && tmp.Rows.Count == 1)
                        {
                            row.Cells[ConstCls.CELL_SYSTEM_ID].Value = tmp.Rows[0]["SYSTEM_ID"];
                            row.Cells[ConstCls.CELL_HIMODZUKE_FUKA_RIYUU].Value = ConstCls.KANRINUMER_EXISTS_1;
                        }
                        else if (tmp != null && tmp.Rows.Count > 1)
                        {
                            row.Cells[ConstCls.CELL_HIMODZUKE_FUKA_RIYUU].Value = ConstCls.KANRINUMER_EXISTS_2;
                        }
                        else
                        {
                            row.Cells[ConstCls.CELL_HIMODZUKE_FUKA_RIYUU].Value = ConstCls.KANRINUMER_EXISTS;
                        }
                        //#161240 20220308 CongBinh E
                    }
                    //管理番号＝無（BLANK）
                    else
                    {
                        row.Cells[ConstCls.CELL_HIMODZUKE_FUKA_RIYUU].Value = ConstCls.KANRINUMER_NOT_EXISTS;
                    }
                }
            }
        }

        /// <summary>
        /// 検索文字列を作成
        /// </summary>
        private string MakeSearchCondition()
        {
            //SELECT
            var selectQuery = this.CreateSelectQuery();
            //FROM
            var fromQuery = this.CreateFromQuery();
            //WHERE
            var whereQuery = this.CreateWhereQuery();
            //ORDER BY
            var orderByQuery = this.CreateOrderByQuery();

            return selectQuery + fromQuery + whereQuery + orderByQuery;
        }

        #region SELECT
        /// <summary>
        /// Select句作成
        /// </summary>
        /// <returns></returns>
        private string CreateSelectQuery()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(this.form.SelectQuery))
            {
                //表示用項目
                sb.AppendLine("SELECT DISTINCT ");

                //システムID
                sb.AppendFormat(" WANSIGN_KEIYAKU_INFO.WANSIGN_SYSTEM_ID AS {0}, ", ConstCls.HIDDEN_WANSIGN_SYSTEM_ID);

                //紐付SystemID
                sb.AppendFormat(" ITAKU_KEIYAKU_KIHON.SYSTEM_ID AS {0}, ", ConstCls.HIDDEN_SYSTEM_ID);

                //管理番号（WAN）
                sb.AppendFormat(" WANSIGN_KEIYAKU_INFO.ORIGINAL_CONTROL_NUMBER AS {0}, ", ConstCls.HIDDEN_ORIGINAL_CONTROL_NUMBER);

                //ドキュメントID
                sb.AppendFormat(" WANSIGN_KEIYAKU_INFO.DOCUMENT_ID AS {0}, ", ConstCls.HIDDEN_DOCUMENT_ID);

                //関連コード
                sb.AppendFormat(" WANSIGN_KEIYAKU_INFO.CONTROL_NUMBER AS {0}, ", ConstCls.HIDDEN_CONTROL_NUMBER);

                //契約状況（WAN）
                sb.AppendFormat(" WANSIGN_KEIYAKU_INFO.SIGNING_DATETIME AS {0}, ", ConstCls.HIDDEN_SIGNING_DATETIME);

                //パターン一覧からの表示列
                sb.AppendLine(this.form.SelectQuery);
            }

            return sb.ToString();
        }
        #endregion

        #region FROM
        /// <summary>
        /// From句作成
        /// </summary>
        /// <returns></returns>
        private string CreateFromQuery()
        {
            var sb = new StringBuilder();

            // WAN-Sign文書詳細情報
            sb.AppendLine(" FROM M_WANSIGN_KEIYAKU_INFO WANSIGN_KEIYAKU_INFO ");

            // 委託契約WANSIGN連携
            sb.AppendLine(" LEFT JOIN  M_ITAKU_LINK_WANSIGN_KEIYAKU ITAKU_LINK_WANSIGN_KEIYAKU ");
            sb.AppendLine(" ON WANSIGN_KEIYAKU_INFO.WANSIGN_SYSTEM_ID = ITAKU_LINK_WANSIGN_KEIYAKU.WANSIGN_SYSTEM_ID ");

            // 契約書
            sb.AppendLine(" LEFT JOIN M_ITAKU_KEIYAKU_KIHON ITAKU_KEIYAKU_KIHON ");
            sb.AppendLine(" ON ITAKU_LINK_WANSIGN_KEIYAKU.SYSTEM_ID = ITAKU_KEIYAKU_KIHON.SYSTEM_ID ");

            // 業者
            sb.AppendLine(" LEFT JOIN M_GYOUSHA ");
            sb.AppendLine(" ON ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");

            // 業者都道府県
            sb.AppendLine(" LEFT JOIN M_TODOUFUKEN TODOUFUKEN_GYOUSHA ");
            sb.AppendLine(" ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = TODOUFUKEN_GYOUSHA.TODOUFUKEN_CD ");

            // 現場
            sb.AppendLine(" LEFT JOIN M_GENBA ");
            sb.AppendLine(" ON ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUSHA_CD = M_GENBA.GYOUSHA_CD AND ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUJOU_CD = M_GENBA.GENBA_CD ");

            // 現場都道府県
            sb.AppendLine(" LEFT JOIN M_TODOUFUKEN TODOUFUKEN_GENBA  ");
            sb.AppendLine(" ON M_GENBA.GENBA_TODOUFUKEN_CD = TODOUFUKEN_GENBA.TODOUFUKEN_CD ");

            // パターンから作成したJOIN句
            sb.AppendLine(this.form.JoinQuery);

            return sb.ToString();
        }
        #endregion

        #region WHERE

        /// <summary>
        /// 検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        private string CreateWhereQuery()
        {
            var sb = new StringBuilder();

            //管理番号（WAN）
            if (!string.IsNullOrEmpty(this.form.ORIGINAL_CONTROL_NUMBER.Text))
            {
                sb.AppendFormat(" AND WANSIGN_KEIYAKU_INFO.ORIGINAL_CONTROL_NUMBER LIKE '%{0}%'", this.form.ORIGINAL_CONTROL_NUMBER.Text);
            }

            //紐付の状態
            if ("1".Equals(this.form.HIMODZUKE_JOUTAI.Text))
            {
                sb.AppendLine(" AND ISNULL(ITAKU_KEIYAKU_KIHON.SYSTEM_ID, '') <> '' ");
            }
            else if ("2".Equals(this.form.HIMODZUKE_JOUTAI.Text))
            {
                sb.AppendLine(" AND ISNULL(ITAKU_KEIYAKU_KIHON.SYSTEM_ID, '') = '' ");
            }

            //相手方名称
            if (!string.IsNullOrEmpty(this.form.PARTNER_ORGANIZE_NM.Text))
            {
                //#161407 20220314 CongBinh S
                sb.AppendFormat(" AND (WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM2 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM3 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM4 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM5 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM6 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM7 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM8 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM9 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM10 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM11 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM12 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM13 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM14 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM15 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendFormat("  OR WANSIGN_KEIYAKU_INFO.PARTNER_ORGANIZE_NM16 LIKE '%{0}%'", this.form.PARTNER_ORGANIZE_NM.Text);
                sb.AppendLine(" ) ");
                //#161407 20220314 CongBinh E
            }

            //文書名
            if (!string.IsNullOrEmpty(this.form.DOCUMENT_NAME.Text))
            {
                sb.AppendFormat(" AND WANSIGN_KEIYAKU_INFO.DOCUMENT_NAME LIKE '%{0}%'", this.form.DOCUMENT_NAME.Text);
            }

            //排出事業者
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                sb.AppendFormat(" AND ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUSHA_CD = '{0}'", this.form.GYOUSHA_CD.Text);
            }

            //排出事業場
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                sb.AppendFormat(" AND ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUJOU_CD = '{0}'", this.form.GENBA_CD.Text);
            }

            //契約日（WAN）（From）
            if (!string.IsNullOrEmpty(this.form.CONTRACT_DATE_FROM.Text))
            {
                sb.AppendFormat(" AND WANSIGN_KEIYAKU_INFO.CONTRACT_DATE >= '{0}'", this.form.CONTRACT_DATE_FROM.Value.ToString());
            }

            //契約日（WAN）（To）
            if (!string.IsNullOrEmpty(this.form.CONTRACT_DATE_TO.Text))
            {
                sb.AppendFormat(" AND WANSIGN_KEIYAKU_INFO.CONTRACT_DATE <= '{0}'", this.form.CONTRACT_DATE_TO.Value.ToString());
            }

            return sb.Length > 0 ? sb.Insert(0, " WHERE 1 = 1 ").ToString() : string.Empty;
        }

        #endregion

        #region ORDER BY
        /// <summary>
        /// OrderBy句作成
        /// </summary>
        /// <returns></returns>
        private string CreateOrderByQuery()
        {
            //順序
            return string.IsNullOrWhiteSpace(this.form.OrderByQuery) ? string.Empty : " ORDER BY " + this.form.OrderByQuery;
        }
        #endregion

        #endregion

        #region F9 電子契約登録
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <returns></returns>
        internal bool RegistData()
        {
            LogUtility.DebugMethodStart();
            bool ret = false;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                    {
                        //チェックオンのチェックボックス＞0件（チェンクオンのチェックボックスが存在）
                        var value = row.Cells[ConstCls.CELL_CHECKBOX].Value;

                        if (value != null &&
                            bool.Parse(value.ToString()))
                        {
                            //紐付SystemID
                            var sysId = Convert.ToString(row.Cells[ConstCls.CELL_SYSTEM_ID].Value);

                            //ドキュメントID
                            var documentId = Convert.ToString(row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value);

                            //SystemIDリスト
                            var lstWanSignSysId = this.dicWanSignSysId[documentId];

                            foreach (var item in lstWanSignSysId)
                            {
                                this.linkWanSignKeiyakuDao.DeleteByWanSignSystemId(item);

                                //委託契約WANSIGN連携（新テーブル）にWANSIGN_システムIDと委託契約書基本情報のSYSTEMIDを登録
                                if (!string.IsNullOrEmpty(sysId))
                                {
                                    var linkWanSign = new M_ITAKU_LINK_WANSIGN_KEIYAKU();

                                    linkWanSign.WANSIGN_SYSTEM_ID = item;
                                    linkWanSign.SYSTEM_ID = long.Parse(sysId);
                                    linkWanSign.DOCUMENT_ID = documentId;

                                    this.linkWanSignKeiyakuDao.Insert(linkWanSign);
                                }
                            }
                        }
                    }

                    tran.Commit();
                    ret = true;

                    //メッセージJを表示
                    this.msgLogic.MessageBoxShowInformation(ConstCls.MsgJ);
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistData", ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret;
        }

        #endregion

        #region subF1 パターン一覧
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        internal void OpenPatternIchiran()
        {
            try
            {
                //パターン一覧を呼び出します。
                //システムID（適用ボタンが押された場合）
                var sysID = this.form.OpenPatternIchiran();

                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData();
                }

                //一覧内のチェックボックスの設定
                this.HeaderCheckBoxSupport();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("OpenPatternIchiran", ex);
                throw;
            }
        }
        #endregion

        #region subF2 契約書ダウンロード
        /// <summary>
        /// 契約書関連ファイルの出力ダイアログ処理
        /// </summary>
        internal string SetOutputFolder()
        {
            var directoryName = string.Empty;

            using (var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder())
            {
                var title = "ファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;

                directoryName = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);
            }

            return directoryName;
        }

        /// <summary>
        /// 契約書ダウンロード
        /// </summary>
        /// <param name="dir">フォルダパス</param>
        /// <returns></returns>
        internal bool KeiyakuDownLoad(string dir)
        {
            bool ret = false;

            #region アクセストークン生成API
            //アクセストークン取得
            var accessToken = this.denshiWanSignLogic.GetAccessTokenWanSign();
            if (accessToken == null)
            {
                return ret;
            }

            var dateTimeOut = DateTime.Now.AddMinutes(30);
            #endregion

            //関連コードリスト
            var listControlNumber = new List<string>();

            //明細行＞0件（電子契約データが明細に表示有（検索処理後））
            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                var value = row.Cells[ConstCls.CELL_CHECKBOX].Value;

                if (value != null &&
                    bool.Parse(value.ToString()))
                {
                    //アクセストークンの有効期限は発行された時間を起点として30分です。
                    //各APIで有効期限が切れていると判定された場合は当APIを実行し新たなアクセストークンを取得してください。
                    var dateNow = DateTime.Now;

                    if (dateNow > dateTimeOut)
                    {
                        accessToken = this.denshiWanSignLogic.GetAccessTokenWanSign();

                        if (accessToken == null)
                        {
                            ret = false;

                            return ret;
                        }
                    }

                    //関連コード
                    var controlNumber = Convert.ToString(row.Cells[ConstCls.HIDDEN_CONTROL_NUMBER].Value);

                    if (!string.IsNullOrEmpty(controlNumber))
                    {
                        if (!listControlNumber.Contains(controlNumber))
                        {
                            //関連コードリスト
                            listControlNumber.Add(controlNumber);

                            #region ２．２文書取得API
                            ret = this.denshiWanSignLogic.DownLoadKeyakuWanSign(dir, accessToken.Result.Access_Token, controlNumber);

                            if (!ret)
                            {
                                break;
                            }
                            #endregion
                        }
                    }
                }
            }

            return ret;
        }

        #endregion

        #region subF3 紐付補助
        /// <summary>
        /// 電子契約紐付補助画面
        /// </summary>
        internal void OpenHimodzukeHojo()
        {
            //メッセージMを表示
            var res = this.msgLogic.MessageBoxShowConfirm(ConstCls.MsgM);

            if (res == DialogResult.Yes)
            {
                var dic = new Dictionary<string, List<string>>();

                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    var value = row.Cells[ConstCls.CELL_CHECKBOX].Value;
                    if (value != null && bool.Parse(value.ToString()))
                    {
                        var sysId = Convert.ToString(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value);

                        if (string.IsNullOrEmpty(sysId))
                        {
                            var documentId = Convert.ToString(row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value);

                            if (!dic.Keys.Contains(documentId))
                            {
                                dic.Add(documentId, null);
                            }
                        }
                    }
                }

                var popUpForm = new DenshiKeiyakuHimodzukeHojo.App.UIForm();
                popUpForm.InOutSysId = dic;

                var result = popUpForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var dicTmp = popUpForm.InOutSysId;
                    if (dicTmp != null &&
                        dicTmp.Count > 0)
                    {
                        foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                        {
                            var documentId = Convert.ToString(row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value);

                            if (dicTmp.Keys.Contains(documentId) &&
                                dicTmp[documentId] != null)
                            {
                                row.Cells[ConstCls.CELL_SYSTEM_ID].Value = dicTmp[documentId][0];

                                if (this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_HIMODZUKE_FUKA_RIYUU))
                                {
                                    row.Cells[ConstCls.CELL_HIMODZUKE_FUKA_RIYUU].Value = dicTmp[documentId][1];
                                }
                            }
                        }

                        this.form.customDataGridView1.RefreshEdit();
                    }
                }
            }
        }
        #endregion

        #region 汎用
        /// <summary>
        /// 一覧で選択がチェックされているか確認する。
        /// </summary>
        /// <returns></returns>
        internal bool CheckForCheckBox(short functionId)
        {
            bool ret = false;
            bool isSumi = true;
            bool isSubF3 = false;
            bool isStatus = true;
            bool isSubF2 = false;
            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                // 選択のチェックボックスの値を取得する。
                if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                {
                    ret = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                    var sysId = Convert.ToString(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value);
                    var status = Convert.ToString(row.Cells[ConstCls.HIDDEN_SIGNING_DATETIME].Value);
                  
                    //[2]契約書ダウンロード
                    if (functionId == 14)
                    {
                        if (string.IsNullOrEmpty(status) && ret)
                        {
                            isStatus = false;
                            break;
                        }
                        if (ret)
                        {
                            isSubF2 = ret;
                        }
                    }
                    //[3]紐付補助
                    else if (functionId == 15)
                    {
                        if (!string.IsNullOrEmpty(sysId) && ret)
                        {
                            isSumi = false;
                            break;
                        }
                        if (ret)
                        {
                            isSubF3 = ret;
                        }
                    }
                    else if (ret)
                    {
                        break;
                    }
                }
            }

            if (functionId == 14)
            {
                //[2]契約書ダウンロード
                if (ret && !isStatus)
                {
                    //メッセージRを表示
                    this.msgLogic.MessageBoxShowError(ConstCls.MsgR);

                    return isStatus;
                }
                else
                {
                    ret = isSubF2;
                }
            }
            else if (functionId == 15)
            {
                //[3]紐付補助
                if (ret && !isSumi)
                {
                    //メッセージPを表示
                    this.msgLogic.MessageBoxShowError(ConstCls.MsgP);

                    return isSumi;
                }
                else
                {
                    ret = isSubF3;
                }
            }

            if (!ret)
            {
                switch (functionId)
                {
                    //[F9]登録
                    case 9:
                        //メッセージGを表示
                        this.msgLogic.MessageBoxShowError(ConstCls.MsgG);
                        break;

                    //[2]契約書ダウンロード
                    case 14:
                        //メッセージKを表示
                        this.msgLogic.MessageBoxShowError(ConstCls.MsgK);
                        break;

                    //[3]紐付補助
                    case 15:
                        //メッセージOを表示
                        this.msgLogic.MessageBoxShowError(ConstCls.MsgO);
                        break;

                    default:
                        break;

                }
            }

            return ret;
        }

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        internal void HeaderCheckBoxSupport()
        {
            try
            {
                //選択
                if (this.form.PatternNo != 0 &&
                    !this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                    newColumn.Name = ConstCls.CELL_CHECKBOX;
                    newColumn.HeaderText = "選択";
                    newColumn.Width = 70;

                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);

                    newheader.Value = "選択   ";
                    newColumn.HeaderCell = newheader;
                    newColumn.MinimumWidth = 70;
                    newColumn.Resizable = DataGridViewTriState.False;

                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.form.customDataGridView1.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns.Add(newColumn);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("HeaderCheckBoxSupport", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// システムIDリスト
        /// </summary>
        private void SetListWanSignSysId()
        {

            if (this.form.Table != null &&
                this.form.Table.Rows.Count > 0)
            {
                //ドキュメントIDリスト
                var lstTmp = this.form.Table.Rows.Cast<DataRow>().GroupBy(s => s.Field<string>(ConstCls.HIDDEN_DOCUMENT_ID));

                foreach (var item in lstTmp)
                {
                    //システムIDリスト
                    var sysIds = this.form.Table.Rows.Cast<DataRow>().Where(s => item.Key.Equals(s.Field<string>(ConstCls.HIDDEN_DOCUMENT_ID))).Select(s => s.Field<long>(ConstCls.HIDDEN_WANSIGN_SYSTEM_ID)).ToList();

                    if (this.dicWanSignSysId.Keys.Contains(item.Key))
                    {
                        this.dicWanSignSysId[item.Key] = sysIds;
                    }
                    else
                    {
                        this.dicWanSignSysId.Add(item.Key, sysIds);
                    }
                }
            }
        }
        #endregion

        #region 未使用

        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 論理削除
        /// <summary>
        /// 論理削除
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 物理削除
        /// <summary>
        /// 物理削除
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region 排出事業者
        /// <summary>
        /// 排出事業者を取得
        /// </summary>
        /// <returns></returns>
        internal bool CheckGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 業者を取得
                M_GYOUSHA entity = new M_GYOUSHA();

                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;

                var gyousha = this.daoGyousha.GetAllValidData(entity).FirstOrDefault();

                if (null == gyousha)
                {
                    // 業者名設定
                    this.msgLogic.MessageBoxShow("E020", "業者");

                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    this.form.GYOUSHA_CD.Focus();
                    this.form.oldGyoushaCd = string.Empty;

                    return false;
                }

                this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGyousha", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);

                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 排出事業場
        /// <summary>
        /// 排出事業場を取得
        /// </summary>
        /// <returns></returns>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.msgLogic.MessageBoxShow("E051", "排出事業者");

                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_CD.Focus();

                    return false;
                }

                M_GENBA keyEntity = new M_GENBA();

                keyEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                keyEntity.GENBA_CD = this.form.GENBA_CD.Text;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;

                var genba = this.daoGenba.GetAllValidData(keyEntity).FirstOrDefault();

                if (genba == null)
                {
                    this.msgLogic.MessageBoxShow("E020", "現場");

                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    this.form.GENBA_CD.Focus();
                    this.form.oldGenbaCd = string.Empty;

                    return false;
                }
                else
                {
                    this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                }

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);

                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion
    }
}
