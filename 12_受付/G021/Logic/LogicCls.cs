// $Id: LogicCls.cs 55371 2015-07-10 11:07:15Z t-thanhson@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.Message;
using Shougun.Core.Reception.UketsukeSyukkaNyuuryoku;
using Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku;
using Shougun.Core.Reception.UketukeiIchiran.CustomControls_Ex;
using Shougun.Core.Reception.UketukeiIchiran.CustomControls_Ex2;
using r_framework.Dto;

namespace Shougun.Core.Reception.UketukeiIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Reception.UketukeiIchiran.Setting.ButtonSetting.xml";
        private readonly string executeSqlFilePath = "Shougun.Core.Reception.UketukeiIchiran.Sql.GetGenbaDataSql.sql";
        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeaderForm headForm;

        /// <summary>
        /// 検索用SQL
        /// </summary>
        public string searchSql { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 一覧検索用のDao
        /// </summary>
        private DAOClass mDetailDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 拠点Dao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// モバイル連携DAO
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        /// <summary>
        /// 受付（出荷）入力のDao
        /// </summary>
        private T_UKETSUKE_SK_ENTRYDao daoUketsukeSKEntry;

        /// <summary>
        /// 受付（収集）入力のDao
        /// </summary>
        private T_UKETSUKE_SS_ENTRYDao daoUketsukeSSEntry;

        //Communicate InxsSubApplication Start
        internal string[] arrDenpyouShuuruiOnlyShougun = new string[] { ConstCls.DENPYOU_SHURUI_CD_SYUKKA, ConstCls.DENPYOU_SHURUI_CD_KUREMU, ConstCls.DENPYOU_SHURUI_CD_SS_SK }; //[伝票種類※]：2.出荷、4.クレーム、5.収集+出荷
        private const string MSG_ERROR_DELETE_DENPYOU = "INXS依頼情報に紐づく受付データは削除できません";
        private const string MSG_CONFIRM_EDIT_DENPYOU = "画面の参照権限がありません。参照モードで開きますか？";
        //Communicate InxsSubApplication End

        /// <summary>
        /// 拠点CD
        /// </summary>
        public string strKYOTEN_CD = string.Empty;

        //2014/01/28 修正 仕様変更 qiao start
        /// <summary>
        /// 伝票種類
        /// </summary>
        public string strDenPyouSyurui = string.Empty;
        //2014/01/28 修正 仕様変更 qiao end

        /// <summary>
        /// 配車状況で使用するデータ
        /// </summary>
        private DataTable haishaJokyoDataTable;

        // No.3822-->
        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// タブストップ用
        /// </summary>
        private string[] tabUiFormControlNames = 
        {   "txtNum_DenPyouSyurui","HAISHA_JOKYO_CD","HAISHA_SHURUI_CD","txtNum_HidukeSentaku","HIDUKE_FROM","HIDUKE_TO",
            "cmbShimebi","cmbShihariaShimebi","TORIHIKISAKI_CD","GYOUSHA_CD","GENBA_CD","UNPAN_GYOUSHA_CD",//CongBinh 20200331 #134987
            "NIOROSHI_GYOUSHA_CD","NIOROSHI_GENBA_CD","customDataGridView1",
            "bt_ptn1","bt_ptn2","bt_ptn3","bt_ptn4","bt_ptn5"
        };

        /// <summary>
        /// チェックボックスのスペースキー対応用
        /// </summary>
        private bool SpaceChk = false;
        private bool SpaceON = false;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者連携現場のDao
        /// </summary>
        private IM_SMS_RECEIVER_LINK_GENBADao smsReceiverLinkGenbaDao;

        #endregion

        #region プロパティ
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }
        /// <summary>
        /// 検索条件
        /// </summary>
        public string searchString { get; set; }
        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public int denShu_Kbn { get; set; }

        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞボタン
        /// </summary>
        internal CustomButton bt_sms;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);
                this.form = targetForm;
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.mDetailDao = DaoInitUtility.GetComponent<DAOClass>();
                this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.daoUketsukeSKEntry = DaoInitUtility.GetComponent<T_UKETSUKE_SK_ENTRYDao>();
                this.daoUketsukeSSEntry = DaoInitUtility.GetComponent<T_UKETSUKE_SS_ENTRYDao>();
                this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
                this.smsReceiverLinkGenbaDao = DaoInitUtility.GetComponent<IM_SMS_RECEIVER_LINK_GENBADao>();
                // Utility
                this.controlUtil = new ControlUtility();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(targetForm);
            }
        }
        #endregion

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
                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダフォームオブジェクト取得
                headForm = (UIHeaderForm)parentForm.headerForm;
                headForm.logic = this;    // No.3123
                this.headForm.form = this.form;

                //ボタンのテキストを初期化
                this.ButtonInit();
                // オプション
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    // mapbox用ボタン無効化
                    parentForm.bt_process3.Text = string.Empty;
                    parentForm.bt_process3.Enabled = false;
                }
                if(AppConfig.AppOptions.IsSMS())
                {
                    // ｼｮｰﾄﾒｯｾｰｼﾞ用ボタン追加
                    this.Bt_SmsInit();
                }

                //イベントの初期化処理
                this.EventInit();

                // No.1598-->
                // 配車状況ポップアップ初期表示処理
                this.HaishaJokyoPopUpDataInit();

                // 配車種類ポップアップ初期表示処理
                this.HaishaShuruiPopUpDataInit();
                // No.1598<--
                // ポップアップ表示条件を設定
                switch (this.form.txtNum_DenPyouSyurui.Text)
                {
                    case ("2"):
                        // 荷積
                        this.form.SetNioroshiNisumiPopupSearchSendParams(2);
                        break;
                    case ("1"):
                    case ("3"):
                    default:
                        // 荷降
                        this.form.SetNioroshiNisumiPopupSearchSendParams(1);
                        break;
                }
                this.allControl = this.form.allControl;
                //Start Sontt 20150710
                this.form.bt_ptn1.Location = new Point(this.form.bt_ptn1.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                this.form.bt_ptn2.Location = new Point(this.form.bt_ptn2.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                this.form.bt_ptn3.Location = new Point(this.form.bt_ptn3.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                this.form.bt_ptn4.Location = new Point(this.form.bt_ptn4.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                this.form.bt_ptn5.Location = new Point(this.form.bt_ptn5.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                //End Sontt 20150710
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// header設定
        /// </summary>
        public void SetHeader(UIHeaderForm headForm)
        {
            try
            {
                LogUtility.DebugMethodStart(headForm);
                this.headForm = headForm;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(headForm);
            }
        }

        /// <summary>
        /// 画面初期表示
        /// </summary>
        public void InitFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //並び替えと明細の設定
                this.form.customSearchHeader1.Visible = true;
                this.form.customSearchHeader1.Location = new System.Drawing.Point(0, 158);
                this.form.customSearchHeader1.Size = new System.Drawing.Size(992, 26);
                this.form.customSortHeader1.Location = new System.Drawing.Point(0, 184);
                this.form.customSortHeader1.Size = new Size(992, 26);
                //明細部：　ブランク
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.TabIndex = 60;
                this.form.customDataGridView1.Location = new Point(0, 209);
                this.form.customDataGridView1.Size = new Size(992, 230);
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();
                //headForm初期
                Init_HeadForm();
                //Heaher初期
                this.form.txtNum_DenPyouSyurui.TextChanged -= new System.EventHandler(this.form.txtNum_DenPyouSyurui_TextChanged);
                Init_Heaher();
                this.form.txtNum_DenPyouSyurui.TextChanged += new System.EventHandler(this.form.txtNum_DenPyouSyurui_TextChanged);
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitFrom", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// HeadForm初期表示
        /// </summary>
        public void Init_HeadForm()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //アラート件数:システム情報を取得する
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.form.SystemId.ToString());
                if (sysInfo != null)
                {
                    //システム情報からアラート件数を取得
                    this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
                    this.headForm.alertNumber.Text = this.alertCount.ToString();
                }
                //遷移元より拠点CD、拠点設定
                if (this.strKYOTEN_CD != string.Empty)
                {
                    var kyotenE = this.kyotenDao.GetDataByCd(this.strKYOTEN_CD);
                    if (kyotenE != null)
                    {
                        this.headForm.KYOTEN_CD.Text = this.strKYOTEN_CD;
                        this.headForm.KYOTEN_NAME_RYAKU.Text = kyotenE.KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        //拠点CD、拠点 : ブランク
                        this.headForm.KYOTEN_CD.Text = string.Empty;
                        this.headForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                    }
                }
                else
                {
                    //拠点CD
                    CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                    string KYOTEN_CD = this.GetUserProfileValue(userProfile, "拠点CD");
                    var kyotenP = this.kyotenDao.GetDataByCd(KYOTEN_CD);
                    //拠点名称
                    if (kyotenP != null && KYOTEN_CD != string.Empty)
                    {
                        this.headForm.KYOTEN_CD.Text = KYOTEN_CD.PadLeft(this.headForm.KYOTEN_CD.MaxLength, '0'); ;
                        this.headForm.KYOTEN_NAME_RYAKU.Text = kyotenP.KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        //拠点CD、拠点 : ブランク
                        this.headForm.KYOTEN_CD.Text = string.Empty;
                        this.headForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                    }
                }
                //伝票日付RadioButton選択状態
                this.headForm.txtNum_HidukeSentaku.Text = ConstCls.HidukeCD_Sagyou;
                this.headForm.radbtnSagyoubi.Checked = true;
                //日付(from) ：当日
                this.headForm.HIDUKE_FROM.Value = parentForm.sysDate;
                //日付(to): 当日
                this.headForm.HIDUKE_TO.Value = parentForm.sysDate;
                //読込データ件数：０ [件]
                this.headForm.readDataNumber.Text = "0";
            }
            catch (Exception ex)
            {
                LogUtility.Error("Init_HeadForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Heaher初期表示
        /// </summary>
        public void Init_Heaher(bool initial = true)
        {
            try
            {
                LogUtility.DebugMethodStart();
                //汎用検索 : ブランク  
                this.form.searchString.Text = string.Empty;
                // 締日
                this.form.cmbShimebi.SelectedIndex = 0; // No.2868
                this.form.cmbShihariaShimebi.SelectedIndex = 0; //CongBinh 20200331 #134987
                //取引先CD、取引先名称 ： ブランク                
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                //業者CD、業者 : ブランク
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME.Text = string.Empty;
                //現場CD、現場 : ブランク               
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME.Text = string.Empty;
                this.form.testGENBA_CD.Text = string.Empty;
                //運搬業者CD、運搬業者 : ブランク
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                //荷降業者CD、荷降 : ブランク               
                this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                //荷降場CD、荷降場 : ブランク               
                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.form.testNIOROSHI_GENBA_CD.Text = string.Empty;
                // No.1598-->
                // 配車状況CD、配車状況：ブランク
                this.form.HAISHA_JOKYO_CD.Text = string.Empty;
                this.form.HAISHA_JOKYO_NAME.Text = string.Empty;
                // 配車種類CD、配車種類：ブランク
                this.form.HAISHA_SHURUI_CD.Text = string.Empty;
                this.form.HAISHA_SHURUI_NAME.Text = string.Empty;
                // No.1598<--
                //日付(from) ：当日
                this.headForm.HIDUKE_FROM.Value = parentForm.sysDate;
                //日付(to): 当日
                this.headForm.HIDUKE_TO.Value = parentForm.sysDate;
                //並び順をクリア
                this.form.customSortHeader1.ClearCustomSortSetting();// No.2292
                this.form.customSearchHeader1.ClearCustomSearchSetting();
                if (!initial)    // No.2292
                {
                    // Init_HeadForm呼んでないのでここでHeadFormの部分クリア
                    //読込データ件数：０ [件]
                    this.headForm.readDataNumber.Text = "0";
                }
                else
                {
                    //SONNT #143061 受付一覧抽出条件の変更 2020/10 START
                    string initDenPyouSyurui = ConstCls.DenPyouDefultCD;
                    if (!string.IsNullOrEmpty(this.strDenPyouSyurui))
                    {
                        initDenPyouSyurui = this.strDenPyouSyurui;
                    }

                    string uketsukeExportkbn = "1";
                    if (r_framework.Configuration.AppConfig.AppOptions.IsInxs())
                    {
                        if (SystemProperty.Shain.InxsTantouFlg
                        && (initDenPyouSyurui == ConstCls.DENPYOU_SHURUI_CD_SYUSYU
                            || initDenPyouSyurui == ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI
                            || initDenPyouSyurui == ConstCls.DENPYOU_SHURUI_CD_SS_MK)
                        )
                        {
                            uketsukeExportkbn = "2";
                        }
                    }
                    else
                    {
                        uketsukeExportkbn = "3";
                    }

                    this.form.UKETSUKE_EXPORT_KBN.TextChanged -= new EventHandler(this.form.UKETSUKE_EXPORT_KBN_TextChanged);
                    this.form.UKETSUKE_EXPORT_KBN.Text = uketsukeExportkbn;
                    this.form.SetActiveDenpyouShuurui();
                    this.form.UKETSUKE_EXPORT_KBN.TextChanged += new EventHandler(this.form.UKETSUKE_EXPORT_KBN_TextChanged);
                    //SONNT #143061 受付一覧抽出条件の変更 2020/10 END

                    //伝票種類RadioButton選択状態
                    //2014/01/28 修正 仕様変更 qiao start
                    //遷移元より伝票種類
                    if (this.strDenPyouSyurui != string.Empty)
                    {
                        this.form.txtNum_DenPyouSyurui.TextChanged += new System.EventHandler(this.form.txtNum_DenPyouSyurui_TextChanged);
                        this.form.txtNum_DenPyouSyurui.Text = this.strDenPyouSyurui;
                        this.form.txtNum_DenPyouSyurui.TextChanged -= new System.EventHandler(this.form.txtNum_DenPyouSyurui_TextChanged);
                    }
                    else
                    {
                        this.form.txtNum_DenPyouSyurui.Text = ConstCls.DenPyouDefultCD;
                        this.form.radbtnSyuusyuu.Checked = true;
                    }
                }
                //2014/01/28 修正 仕様変更 qiao end
                //フォーカス移動
                if (this.form.searchString.Visible == true)
                {
                    this.form.searchString.Focus();
                }
                else
                {
                    this.form.TORIHIKISAKI_CD.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Init_Heaher", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 締日設定
        /// <summary>
        /// 締日設定
        /// </summary>
        private void SimeDateSet()
        {
            LogUtility.DebugMethodStart();
            //取引先_請求情報マスタをJOIN
            System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto> lstJoinMethodDto = new System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>();
            r_framework.Dto.JoinMethodDto cJoinMethodDto = new r_framework.Dto.JoinMethodDto();
            cJoinMethodDto.IsCheckLeftTable = true;
            cJoinMethodDto.IsCheckRightTable = true;
            cJoinMethodDto.Join = r_framework.Const.JOIN_METHOD.INNER_JOIN;
            cJoinMethodDto.LeftKeyColumn = "TORIHIKISAKI_CD";
            cJoinMethodDto.LeftTable = "M_TORIHIKISAKI_SEIKYUU";
            cJoinMethodDto.RightKeyColumn = "TORIHIKISAKI_CD";
            cJoinMethodDto.RightTable = "M_TORIHIKISAKI";
            lstJoinMethodDto.Add(cJoinMethodDto);
            cJoinMethodDto = new r_framework.Dto.JoinMethodDto();
            cJoinMethodDto.IsCheckLeftTable = true;
            cJoinMethodDto.IsCheckRightTable = true;
            cJoinMethodDto.Join = r_framework.Const.JOIN_METHOD.WHERE;
            cJoinMethodDto.LeftKeyColumn = string.Empty;
            cJoinMethodDto.LeftTable = "M_TORIHIKISAKI_SEIKYUU";
            cJoinMethodDto.RightKeyColumn = string.Empty;
            cJoinMethodDto.RightTable = string.Empty;
            System.Collections.ObjectModel.Collection<r_framework.Dto.SearchConditionsDto> lstSearchCondition = new System.Collections.ObjectModel.Collection<r_framework.Dto.SearchConditionsDto>();
            r_framework.Dto.SearchConditionsDto cSearchCondDto = new r_framework.Dto.SearchConditionsDto();
            cSearchCondDto.And_Or = CONDITION_OPERATOR.AND;
            cSearchCondDto.Condition = JUGGMENT_CONDITION.EQUALS;
            cSearchCondDto.LeftColumn = "SHIMEBI1";
            cSearchCondDto.RightColumn = string.Empty;
            cSearchCondDto.Value = "cmbShimebi";
            cSearchCondDto.ValueColumnType = DB_TYPE.IN_SMALLINT;
            lstSearchCondition.Add(cSearchCondDto);
            cSearchCondDto = new r_framework.Dto.SearchConditionsDto();
            cSearchCondDto.And_Or = CONDITION_OPERATOR.OR;
            cSearchCondDto.Condition = JUGGMENT_CONDITION.EQUALS;
            cSearchCondDto.LeftColumn = "SHIMEBI2";
            cSearchCondDto.RightColumn = string.Empty;
            cSearchCondDto.Value = "cmbShimebi";
            cSearchCondDto.ValueColumnType = DB_TYPE.IN_SMALLINT;
            lstSearchCondition.Add(cSearchCondDto);
            cSearchCondDto = new r_framework.Dto.SearchConditionsDto();
            cSearchCondDto.And_Or = CONDITION_OPERATOR.OR;
            cSearchCondDto.Condition = JUGGMENT_CONDITION.EQUALS;
            cSearchCondDto.LeftColumn = "SHIMEBI3";
            cSearchCondDto.RightColumn = string.Empty;
            cSearchCondDto.Value = "cmbShimebi";
            cSearchCondDto.ValueColumnType = DB_TYPE.IN_SMALLINT;
            lstSearchCondition.Add(cSearchCondDto);
            cJoinMethodDto.SearchCondition = lstSearchCondition;
            lstJoinMethodDto.Add(cJoinMethodDto);
            this.form.TORIHIKISAKI_CD.popupWindowSetting = lstJoinMethodDto;
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 配車状況 ポップアップ初期化
        /// <summary>
        /// 配車状況 ポップアップ初期化
        /// </summary>
        public void HaishaJokyoPopUpDataInit()
        {
            LogUtility.DebugMethodStart();
            // 表示用データ設定
            this.haishaJokyoDataTable = new DataTable();
            // タイトル
            this.haishaJokyoDataTable.TableName = ConstCls.POPUP_TITLE_HAISHA_JOKYO;
            // 列定義
            this.haishaJokyoDataTable.Columns.Add(ConstCls.COLUMN_HAISHA_JOKYO_CD, typeof(String));
            this.haishaJokyoDataTable.Columns.Add(ConstCls.COLUMN_HAISHA_JOKYO_NAME, typeof(String));
            // データ
            this.haishaJokyoDataTable.Rows.Add(ConstCls.HAISHA_JOKYO_CD_JUCHU, ConstCls.HAISHA_JOKYO_NAME_JUCHU);
            this.haishaJokyoDataTable.Rows.Add(ConstCls.HAISHA_JOKYO_CD_HAISHA, ConstCls.HAISHA_JOKYO_NAME_HAISHA);
            this.haishaJokyoDataTable.Rows.Add(ConstCls.HAISHA_JOKYO_CD_KEIJO, ConstCls.HAISHA_JOKYO_NAME_KEIJO);
            this.haishaJokyoDataTable.Rows.Add(ConstCls.HAISHA_JOKYO_CD_CANCEL, ConstCls.HAISHA_JOKYO_NAME_CANCEL);
            this.haishaJokyoDataTable.Rows.Add(ConstCls.HAISHA_JOKYO_CD_NASHI, ConstCls.HAISHA_JOKYO_NAME_NASHI);
            // 列名とデータソース設定
            this.form.HAISHA_JOKYO_CD.PopupDataHeaderTitle = new string[] { ConstCls.HEADER_HAISHA_JOKYO_CD, ConstCls.HEADER_HAISHA_JOKYO_NAME };
            this.form.HAISHA_JOKYO_CD.PopupDataSource = this.haishaJokyoDataTable;
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 配車種類 ポップアップ初期化
        /// <summary>
        /// 配車種類 ポップアップ初期化
        /// </summary>
        public void HaishaShuruiPopUpDataInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 表示用データ設定
                DataTable haishaShuruiDataTable = new DataTable();
                // データ（列）
                haishaShuruiDataTable.Columns.Add("HAISHA_SHURUI_CD", Type.GetType("System.String"));
                haishaShuruiDataTable.Columns.Add("HAISHA_SHURUI_NAME", Type.GetType("System.String"));
                // データ（行）
                haishaShuruiDataTable.Rows.Add("1", "通常");
                haishaShuruiDataTable.Rows.Add("2", "仮押");
                haishaShuruiDataTable.Rows.Add("3", "確定");
                // TableNameを設定すれば、ポップアップのタイトルになる
                haishaShuruiDataTable.TableName = "配車種類選択";
                // 列名とデータソース設定
                this.form.HAISHA_SHURUI_CD.PopupDataHeaderTitle = new string[] { "配車種類CD", "配車種類名" };
                this.form.HAISHA_SHURUI_CD.PopupDataSource = haishaShuruiDataTable;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        // No.1598<--

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);
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
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //customTextBoxでのエンターキー押下イベント生成
                this.form.searchString.KeyDown += new KeyEventHandler(SearchStringKeyDown);       //汎用検索(SearchString)
                //Functionボタンのイベント生成
                parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);              // 簡易検索／汎用検索
                parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);              // 新規
                parentForm.bt_func3.Click += new EventHandler(this.bt_func3_Click);              // 修正
                parentForm.bt_func4.Click += new EventHandler(this.bt_func4_Click);              // 削除
                parentForm.bt_func5.Click += new EventHandler(this.bt_func5_Click);              // 複写
                parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);              // CSV出力
                parentForm.bt_func7.Click += new EventHandler(this.bt_func7_Click);              // 条件クリア
                parentForm.bt_func8.Click += new EventHandler(this.bt_func8_Click);              // 検索
                parentForm.bt_func9.Click += new EventHandler(this.bt_func9_Click);              // 現場メモ登録
                parentForm.bt_func10.Click += new EventHandler(this.bt_func10_Click);            // 並び替え
                parentForm.bt_func11.Click += new EventHandler(this.bt_func11_Click);            // フィルタ
                parentForm.bt_func12.Click += new EventHandler(this.bt_func12_Click);            //閉じる
                parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             // パターン一覧画面へ遷移
                parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             // 現場メモ一覧画面へ遷移
                parentForm.bt_process3.Click += new EventHandler(bt_process3_Click);             // 地図表示
                parentForm.bt_process4.Click += new EventHandler(bt_process4_Click);             // 受入or出荷入力画面へ遷移
                parentForm.bt_process5.Click += new EventHandler(bt_process5_Click);             // 売上／支払入力画面へ遷移
                //明細画面上でダブルクリック時のイベント生成
                this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(customDataGridView1_CellDoubleClick);
                this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(customDataGridView1_ColumnHeaderMouseClick);
                // 20141127 teikyou ダブルクリックを追加する　start
                this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
                // 20141127 teikyou ダブルクリックを追加する　end

                this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(customDataGridView1_CellClick);
                this.form.customDataGridView1.PreviewKeyDown += new PreviewKeyDownEventHandler(this.DetailPreviewKeyDown);

                //受入出荷画面サイズ選択取得
                HearerSysInfoInit();

                // ｼｮｰﾄﾒｯｾｰｼﾞボタン追加
                if (bt_sms != null)
                {
                    parentForm.Controls.Add(bt_sms);
                }
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

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void HearerSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }

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
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 検索処理
        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return 0;
                }
                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end
                //検索用SQLの作成
                this.MakeSearchCondition();
                //検索実行
                this.searchResult = new DataTable();
                if (!string.IsNullOrEmpty(this.searchSql))
                {
                    this.searchResult = mDetailDao.getdateforstringsql(this.searchSql);
                    int count = searchResult.Rows.Count;
                    //検索結果が存在しませんの場合、メッセージを表示する
                    if (count == 0)
                    {
                        //検索結果を表示する
                        this.form.ShowData(searchResult);
                        //DataGridViewのプロパティ再設定
                        setDataGridView();
                        //読込データ件数を0にする
                        this.headForm.readDataNumber.Text = count.ToString();
                        MessageBoxUtility.MessageBoxShow("C001");
                        if (this.form.searchString.Visible)
                        {
                            this.form.searchString.Focus();
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_CD.Focus();
                        }
                        return 0;
                    }
                    else
                    {
                        //読込データ件数：０ [件]
                        this.headForm.readDataNumber.Text = "0";
                        //検索結果を表示する
                        this.form.ShowData(searchResult);
                        //読込データ件数の設定
                        this.headForm.readDataNumber.Text = count.ToString();
                        //DataGridViewのプロパティ再設定
                        setDataGridView();
                        //thongh 2015/09/14 #13032 start

                        //読込データ件数の設定
                        if (this.form.customDataGridView1 != null)
                        {
                            this.headForm.readDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();

                            if (this.form.customDataGridView1.Rows.Count > 1)
                            {
                                this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[0, 1];
                                this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[0, 0];
                            }
                        }
                        else
                        {
                            this.headForm.readDataNumber.Text = "0";
                        }
                        //thongh 2015/09/14 #13032 end
                        return searchResult.Rows.Count;
                    }
                }
                else
                {
                    this.form.customDataGridView1.DataSource = null;
                    this.form.customDataGridView1.Columns.Clear();
                    //読込データ件数：０ [件]
                    this.headForm.readDataNumber.Text = "0";
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索用SQLの作成
        /// </summary>
        private void MakeSearchCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                //SELECT句未取得なら検索できない
                if (string.IsNullOrEmpty(this.form.SelectQuery))
                {
                    this.headForm.readDataNumber.Text = "0";
                    this.searchSql = string.Empty;
                    return;
                }

                #region 伝票種類からselect列を設定

                //伝票種類からselect列を設定
                string denpyouSyurui = this.form.txtNum_DenPyouSyurui.Text;
                string summeryKbn = string.Empty;
                if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouSyurui)
                {
                    //伝票種類=「収集」　
                    summeryKbn = ConstCls.SUMMARY_SYUSYU;
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouSyurui)
                {
                    //伝票種類=「出荷」　
                    summeryKbn = ConstCls.SUMMARY_SYUKKA;
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI == denpyouSyurui)
                {
                    //伝票種類=「持込」
                    summeryKbn = ConstCls.SUMMARY_MOCHIKOMI;
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_KUREMU == denpyouSyurui)
                {
                    //伝票種類=「クレーム」
                    summeryKbn = ConstCls.SUMMARY_CUREMU;
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouSyurui)
                {
                    //伝票種類=「収集＋出荷」
                    summeryKbn = ConstCls.SUMMARY_SS_SK;
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_SS_MK == denpyouSyurui)
                {
                    //伝票種類=「収集＋持込」
                    summeryKbn = ConstCls.SUMMARY_SS_MK;
                }
                else
                {
                    this.headForm.readDataNumber.Text = "0";
                    this.searchSql = "";
                    return;
                }

                #endregion

                #region SELECT句
                var isDetail = this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI;
                sql.Append(" SELECT DISTINCT ");
                //出力パターンよりのSQL
                sql.Append(this.form.SelectQuery);
                //システムID
                sql.AppendFormat(" ,{0}_ENTRY.SYSTEM_ID AS {1} ", summeryKbn, ConstCls.HIDDEN_SYSTEM_ID);
                //枝番
                sql.AppendFormat(" ,{0}_ENTRY.SEQ AS {1} ", summeryKbn, ConstCls.HIDDEN_SEQ);
                //受付番号
                sql.AppendFormat(" ,{0}_ENTRY.UKETSUKE_NUMBER AS {1} ", summeryKbn, ConstCls.HIDDEN_UKETSUKE_NUMBER);
                //受付日
                sql.AppendFormat(" ,{0}_ENTRY.UKETSUKE_DATE AS {1} ", summeryKbn, ConstCls.HIDDEN_UKETSUKE_DATE);
                //[伝票種類]＝「1」（収集）、「2」（出荷）、「4」（物販）、「6」（収集＋出荷）、「7」（収集＋持込）の場合
                if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouSyurui
                    || ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouSyurui
                    || ConstCls.DENPYOU_SHURUI_CD_SS_MK == denpyouSyurui
                    || ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouSyurui)
                {
                    //配車状況CD
                    sql.AppendFormat(" ,{0}_ENTRY.HAISHA_JOKYO_CD AS {1} ", summeryKbn, ConstCls.HIDDEN_HAISHA_JOKYO_CD);
                }
                //[伝票種類]＝「1」（収集）、「2」（出荷）、「6」（収集＋出荷）の場合
                if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouSyurui
                    || ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouSyurui
                    || ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouSyurui)
                {
                    //配車種類CD
                    sql.AppendFormat(" ,{0}_ENTRY.HAISHA_SHURUI_CD AS {1} ", summeryKbn, ConstCls.HIDDEN_HAISHA_SHURUI_CD);
                    // 緯度経度
                    sql.AppendFormat(" ,CASE WHEN M_GENBA.GENBA_CD IS NOT NULL AND M_GENBA.GENBA_CD!='' THEN M_GENBA.GENBA_LATITUDE ELSE M_GYOUSHA.GYOUSHA_LATITUDE END AS {0} ", ConstCls.HIDDEN_GENBA_LATITUDE);
                    sql.AppendFormat(" ,CASE WHEN M_GENBA.GENBA_CD IS NOT NULL AND M_GENBA.GENBA_CD!='' THEN M_GENBA.GENBA_LONGITUDE ELSE M_GYOUSHA.GYOUSHA_LONGITUDE END AS {0} ", ConstCls.HIDDEN_GENBA_LONGITUDE);
                }

                // [伝票種類]＝「1」（収集）
                if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouSyurui)
                {
                    // 伝票種類
                    sql.AppendFormat(" ,'収集' AS {0} ", ConstCls.HIDDEN_DENPYOU_SHURUI_NAME);
                }
                // [伝票種類]＝「2」（出荷）
                if (ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouSyurui)
                {
                    // 伝票種類
                    sql.AppendFormat(" ,'出荷' AS {0} ", ConstCls.HIDDEN_DENPYOU_SHURUI_NAME);
                }
                // [伝票種類]＝「6」（収集＋出荷）
                if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouSyurui)
                {
                    // 伝票種類
                    sql.AppendFormat(" ,{0}_ENTRY.DENPYOU_SHURUI_NAME AS {1} ", summeryKbn, ConstCls.HIDDEN_DENPYOU_SHURUI_NAME);
                }

                //Communicate InxsSubApplication Start
                ////[伝票種類]!＝「5」（クレーム）の場合
                //if (isDetail && ConstCls.DENPYOU_SHURUI_CD_KUREMU != denpyouSyurui)
                //{
                //    //[伝票種類]＝「1」（収集）、「2」（出荷）、「3」（持込）、「4」（物販）の場合
                //    if (ConstCls.DENPYOU_SHURUI_CD_SS_SK != denpyouSyurui && ConstCls.DENPYOU_SHURUI_CD_SS_MK != denpyouSyurui)
                //    {
                //        sql.AppendFormat(" ,{0}_DETAIL.DETAIL_SYSTEM_ID AS {1} ", summeryKbn, ConstCls.HIDDEN_DETAIL_SYSTEM_ID);
                //    }
                //    //[伝票種類]＝「6」（収集＋出荷）、「7」（収集＋持込）の場合
                //    else
                //    {
                //        sql.AppendFormat(" ,{0}_ENTRY.DETAIL_SYSTEM_ID AS {1} ", summeryKbn, ConstCls.HIDDEN_DETAIL_SYSTEM_ID);
                //    }
                //}

                //[伝票種類]＝「1」（収集）、「3」（持込）、「7」（収集＋持込）
                if (isDetail && r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke()
                    &&
                    (
                        (denpyouSyurui == ConstCls.DENPYOU_SHURUI_CD_SYUSYU && this.form.SelectQuery.Contains("T_PICKUP_REQUEST_DETAIL_INXS1"))
                        || (denpyouSyurui == ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI && this.form.SelectQuery.Contains("T_CARRY_ON_REQUEST_DETAIL_INXS1"))
                        || (denpyouSyurui == ConstCls.DENPYOU_SHURUI_CD_SS_MK && this.form.SelectQuery.Contains("T_PICKUP_CARRY_ON_REQUEST_DETAIL_INXS1"))
                    ))
                {
                    //Don't use key DETAIL_SYSTEM_ID
                }
                else if (isDetail && ConstCls.DENPYOU_SHURUI_CD_KUREMU != denpyouSyurui)
                {
                    //[伝票種類]＝「1」（収集）、「2」（出荷）、「3」（持込）、「4」（物販）の場合
                    if (ConstCls.DENPYOU_SHURUI_CD_SS_SK != denpyouSyurui && ConstCls.DENPYOU_SHURUI_CD_SS_MK != denpyouSyurui)
                    {
                        sql.AppendFormat(" ,{0}_DETAIL.DETAIL_SYSTEM_ID AS {1} ", summeryKbn, ConstCls.HIDDEN_DETAIL_SYSTEM_ID);
                    }
                    //[伝票種類]＝「6」（収集＋出荷）、「7」（収集＋持込）の場合
                    else
                    {
                        sql.AppendFormat(" ,{0}_ENTRY.DETAIL_SYSTEM_ID AS {1} ", summeryKbn, ConstCls.HIDDEN_DETAIL_SYSTEM_ID);
                    }
                }
                //Communicate InxsSubApplication End

                string errorDefault = "\'0\'";
                //[伝票種類]＝「1」（収集）、[伝票種類]＝「1」（出荷）、「3」（持込）の場合
                if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouSyurui || ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouSyurui || ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI == denpyouSyurui)
                {
                    sql.AppendFormat(" ,{0} AS HST_GYOUSHA_CD_ERROR", errorDefault);
                    sql.AppendFormat(" ,{0} AS HST_GENBA_CD_ERROR", errorDefault);
                    sql.AppendFormat(" ,{0} AS HAIKI_SHURUI_CD_ERROR", errorDefault);
                    sql.AppendFormat(" ,{0} AS HIDDEN_DENPYOU_SHURUI_CD", denpyouSyurui);

                }

                //[伝票種類]＝「6」（収集＋出荷）、「7」（収集＋持込）の場合
                if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouSyurui || ConstCls.DENPYOU_SHURUI_CD_SS_MK == denpyouSyurui)
                {
                    sql.AppendFormat(" ,{0}_ENTRY.DPSR AS HIDDEN_DENPYOU_SHURUI_CD ", summeryKbn);
                }
                #endregion

                #region FROM句
                if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouSyurui)
                {
                    #region FROM句 収集
                    sql.Append(" FROM T_UKETSUKE_SS_ENTRY ");
                    sql.Append(" LEFT JOIN M_GENBA ");
                    sql.Append("        ON T_UKETSUKE_SS_ENTRY.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND T_UKETSUKE_SS_ENTRY.GENBA_CD = M_GENBA.GENBA_CD ");
                    sql.Append(" LEFT JOIN M_GYOUSHA ");
                    sql.Append("        ON T_UKETSUKE_SS_ENTRY.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");

                    if (isDetail)
                    {
                        sql.Append(" LEFT JOIN T_UKETSUKE_SS_DETAIL ");
                        sql.Append("        ON T_UKETSUKE_SS_ENTRY.SYSTEM_ID = T_UKETSUKE_SS_DETAIL.SYSTEM_ID ");
                        sql.Append("       AND T_UKETSUKE_SS_ENTRY.SEQ = T_UKETSUKE_SS_DETAIL.SEQ ");
                    }

                    //Communicate InxsSubApplication Start
                    if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
                    {
                        sql.Append(" LEFT JOIN T_PICKUP_REQUEST_UKETSUKE_SS_INXS ");
                        sql.Append(" ON T_PICKUP_REQUEST_UKETSUKE_SS_INXS.UKETSUKE_SYSTEM_ID = T_UKETSUKE_SS_ENTRY.SYSTEM_ID ");
                        sql.Append(" LEFT JOIN T_PICKUP_REQUEST_INXS AS T_PICKUP_REQUEST_INXS1");
                        sql.Append(" ON T_PICKUP_REQUEST_INXS1.SYSTEM_ID = T_PICKUP_REQUEST_UKETSUKE_SS_INXS.REQUEST_SYSTEM_ID ");
                        sql.Append(" AND T_PICKUP_REQUEST_INXS1.DELETE_FLG = 0 ");
                    }
                    //Communicate InxsSubApplication End

                    // 現場メモEntry
                    sql.Append(" LEFT JOIN T_GENBAMEMO_ENTRY ");
                    sql.Append(" ON T_UKETSUKE_SS_ENTRY.SYSTEM_ID = T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID ");

                    // ｼｮｰﾄﾒｯｾｰｼﾞEntry
                    if (this.form.SelectQuery.Contains("SMS送信"))
                    {
                        sql.Append(" LEFT JOIN T_SMS ");
                        sql.Append(" ON T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER = T_SMS.DENPYOU_NUMBER ");
                    }

                    #endregion
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouSyurui)
                {
                    #region FROM句 出荷
                    sql.Append(" FROM T_UKETSUKE_SK_ENTRY ");
                    sql.Append(" LEFT JOIN M_GENBA ");
                    sql.Append("        ON T_UKETSUKE_SK_ENTRY.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND T_UKETSUKE_SK_ENTRY.GENBA_CD = M_GENBA.GENBA_CD ");
                    sql.Append(" LEFT JOIN M_GYOUSHA ");
                    sql.Append("        ON T_UKETSUKE_SK_ENTRY.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");

                    if (isDetail)
                    {
                        sql.Append(" LEFT JOIN T_UKETSUKE_SK_DETAIL ");
                        sql.Append("        ON T_UKETSUKE_SK_ENTRY.SYSTEM_ID = T_UKETSUKE_SK_DETAIL.SYSTEM_ID ");
                        sql.Append("       AND T_UKETSUKE_SK_ENTRY.SEQ = T_UKETSUKE_SK_DETAIL.SEQ ");
                    }
                    // 現場メモEntry
                    sql.Append(" LEFT JOIN T_GENBAMEMO_ENTRY ");
                    sql.Append(" ON T_UKETSUKE_SK_ENTRY.SYSTEM_ID = T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID ");

                    // ｼｮｰﾄﾒｯｾｰｼﾞEntry
                    if (this.form.SelectQuery.Contains("SMS送信"))
                    {
                        sql.Append(" LEFT JOIN T_SMS ");
                        sql.Append(" ON T_UKETSUKE_SK_ENTRY.UKETSUKE_NUMBER = T_SMS.DENPYOU_NUMBER ");
                    }
                    #endregion
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI == denpyouSyurui)
                {
                    #region FROM句 持込
                    sql.Append(" FROM T_UKETSUKE_MK_ENTRY ");
                    if (isDetail)
                    {
                        sql.Append(" LEFT JOIN T_UKETSUKE_MK_DETAIL ");
                        sql.Append("        ON T_UKETSUKE_MK_ENTRY.SYSTEM_ID = T_UKETSUKE_MK_DETAIL.SYSTEM_ID ");
                        sql.Append("       AND T_UKETSUKE_MK_ENTRY.SEQ = T_UKETSUKE_MK_DETAIL.SEQ ");
                    }

                    //Communicate InxsSubApplication Start
                    if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
                    {
                        sql.Append(" LEFT JOIN T_CARRY_ON_REQUEST_UKETSUKE_MK_INXS ");
                        sql.Append(" ON T_CARRY_ON_REQUEST_UKETSUKE_MK_INXS.UKETSUKE_SYSTEM_ID = T_UKETSUKE_MK_ENTRY.SYSTEM_ID ");
                        sql.Append(" LEFT JOIN T_CARRY_ON_REQUEST_INXS AS T_CARRY_ON_REQUEST_INXS1");
                        sql.Append(" ON T_CARRY_ON_REQUEST_INXS1.SYSTEM_ID = T_CARRY_ON_REQUEST_UKETSUKE_MK_INXS.REQUEST_SYSTEM_ID ");
                        sql.Append(" AND T_CARRY_ON_REQUEST_INXS1.DELETE_FLG = 0 ");
                    }
                    //Communicate InxsSubApplication End

                    // 現場メモEntry
                    sql.Append(" LEFT JOIN T_GENBAMEMO_ENTRY ");
                    sql.Append(" ON T_UKETSUKE_MK_ENTRY.SYSTEM_ID = T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID ");

                    // ｼｮｰﾄﾒｯｾｰｼﾞEntry
                    if (this.form.SelectQuery.Contains("SMS送信"))
                    {
                        sql.Append(" LEFT JOIN T_SMS ");
                        sql.Append(" ON T_UKETSUKE_MK_ENTRY.UKETSUKE_NUMBER = T_SMS.DENPYOU_NUMBER ");
                    }
                    #endregion
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_KUREMU == denpyouSyurui)
                {
                    #region FROM句 クレーム
                    sql.Append(" FROM T_UKETSUKE_CM_ENTRY ");
                    // 現場メモEntry
                    sql.Append(" LEFT JOIN T_GENBAMEMO_ENTRY ");
                    sql.Append(" ON T_UKETSUKE_CM_ENTRY.SYSTEM_ID = T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID ");
                    #endregion
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouSyurui)
                {
                    #region FROM句 収集＋出荷
                    sql.Append(" FROM ( ");
                    sql.Append(MakeUketsukeSsDataTableSSSK());
                    sql.Append(" UNION ALL ");
                    sql.Append(MakeUketsukeSkDataTable());
                    sql.Append(" ) AS T_UKETSUKE_SS_SK_ENTRY ");

                    sql.Append(" LEFT JOIN M_GENBA ");
                    sql.Append("        ON T_UKETSUKE_SS_SK_ENTRY.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND T_UKETSUKE_SS_SK_ENTRY.GENBA_CD = M_GENBA.GENBA_CD ");
                    sql.Append(" LEFT JOIN M_GYOUSHA ");
                    sql.Append("        ON T_UKETSUKE_SS_SK_ENTRY.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");

                    // 現場メモEntry
                    sql.Append(" LEFT JOIN T_GENBAMEMO_ENTRY ");
                    sql.Append(" ON T_UKETSUKE_SS_SK_ENTRY.SYSTEM_ID = T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID ");

                    // ｼｮｰﾄﾒｯｾｰｼﾞEntry
                    if (this.form.SelectQuery.Contains("SMS送信"))
                    {
                        sql.Append(" LEFT JOIN T_SMS ");
                        sql.Append(" ON T_UKETSUKE_SS_SK_ENTRY.UKETSUKE_NUMBER = T_SMS.DENPYOU_NUMBER ");
                    }
                    #endregion
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_SS_MK == denpyouSyurui)
                {
                    #region FROM句 収集＋持込
                    sql.Append(" FROM ( ");
                    sql.Append(MakeUketsukeSsDataTableSSMK());
                    sql.Append(" UNION ALL ");
                    sql.Append(MakeUketsukeMkDataTable());
                    sql.Append(" ) AS T_UKETSUKE_SS_MK_ENTRY ");
                    // 現場メモEntry
                    sql.Append(" LEFT JOIN T_GENBAMEMO_ENTRY ");
                    sql.Append(" ON T_UKETSUKE_SS_MK_ENTRY.SYSTEM_ID = T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID ");

                    // ｼｮｰﾄﾒｯｾｰｼﾞEntry
                    sql.Append(" LEFT JOIN T_SMS ");
                    sql.Append(" ON T_UKETSUKE_SS_MK_ENTRY.UKETSUKE_NUMBER = T_SMS.DENPYOU_NUMBER ");
                    #endregion

                    //Communicate InxsSubApplication Start
                    #region INXS FROM句 収集＋持込
                    if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
                    {
                        sql.Append(@" OUTER APPLY (SELECT 1 AS INXS_DENPYOU_KBN,
                                                           T_PICKUP_REQUEST_INXS.SYSTEM_ID,
                                                           T_PICKUP_REQUEST_INXS.SEQ,
                                                           T_PICKUP_REQUEST_INXS.REQUEST_NUMBER,
                                                           T_PICKUP_REQUEST_INXS.TEMPLATE_ID,
                                                           T_PICKUP_REQUEST_INXS.STATUS,
                                                           T_PICKUP_REQUEST_INXS.USER_ID,
                                                           T_PICKUP_REQUEST_INXS.REQUEST_SETTING_DAY,
                                                           T_PICKUP_REQUEST_INXS.REQUEST_SETTING_HOURS,
                                                           T_PICKUP_REQUEST_INXS.CHANGE_SETTING_DAY,
                                                           T_PICKUP_REQUEST_INXS.CHANGE_SETTING_HOURS,
                                                           T_PICKUP_REQUEST_INXS.CANCEL_SETTING_DAY,
                                                           T_PICKUP_REQUEST_INXS.CANCEL_SETTING_HOURS,
                                                           T_PICKUP_REQUEST_INXS.MESSAGE_FROM_ADMIN,
                                                           T_PICKUP_REQUEST_INXS.MESSAGE_USER_REQUEST,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GYOUSHA_CD,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GYOUSHA_NAME,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GYOUSHA_ADDRESS,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GYOUSHA_BUSHO,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GYOUSHA_TANTOUSHA,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GYOUSHA_TEL,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GENBA_CD,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GENBA_NAME,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GENBA_ADDRESS,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GENBA_BUSHO,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GENBA_TANTOUSHA,
                                                           T_PICKUP_REQUEST_INXS.HAISHUTSU_GENBA_TEL,
                                                           T_PICKUP_REQUEST_INXS.TRUCK_TYPE_CD,
                                                           T_PICKUP_REQUEST_INXS.TRUCK_TYPE,
                                                           T_PICKUP_REQUEST_INXS.NUMBER_OF_TRUCK,
                                                           T_PICKUP_REQUEST_INXS.JOB_TYPE
                                                    FROM   T_PICKUP_REQUEST_UKETSUKE_SS_INXS
                                                           LEFT JOIN T_PICKUP_REQUEST_INXS
                                                                  ON T_PICKUP_REQUEST_INXS.SYSTEM_ID = T_PICKUP_REQUEST_UKETSUKE_SS_INXS.REQUEST_SYSTEM_ID
                                                                     AND T_PICKUP_REQUEST_INXS.DELETE_FLG = 0
                                                    WHERE  T_UKETSUKE_SS_MK_ENTRY.DPSR = 'SS'
                                                           AND T_PICKUP_REQUEST_UKETSUKE_SS_INXS.UKETSUKE_SYSTEM_ID = T_UKETSUKE_SS_MK_ENTRY.SYSTEM_ID
                                                    UNION ALL
                                                    SELECT 2                 AS INXS_DENPYOU_KBN,
                                                           T_CARRY_ON_REQUEST_INXS.SYSTEM_ID,
                                                           T_CARRY_ON_REQUEST_INXS.SEQ,
                                                           T_CARRY_ON_REQUEST_INXS.REQUEST_NUMBER,
                                                           T_CARRY_ON_REQUEST_INXS.TEMPLATE_ID,
                                                           T_CARRY_ON_REQUEST_INXS.STATUS,
                                                           T_CARRY_ON_REQUEST_INXS.USER_ID,
                                                           T_CARRY_ON_REQUEST_INXS.REQUEST_SETTING_DAY,
                                                           T_CARRY_ON_REQUEST_INXS.REQUEST_SETTING_HOURS,
                                                           T_CARRY_ON_REQUEST_INXS.CHANGE_SETTING_DAY,
                                                           T_CARRY_ON_REQUEST_INXS.CHANGE_SETTING_HOURS,
                                                           T_CARRY_ON_REQUEST_INXS.CANCEL_SETTING_DAY,
                                                           T_CARRY_ON_REQUEST_INXS.CANCEL_SETTING_HOURS,
                                                           T_CARRY_ON_REQUEST_INXS.MESSAGE_FROM_ADMIN,
                                                           T_CARRY_ON_REQUEST_INXS.MESSAGE_USER_REQUEST,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GYOUSHA_CD,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GYOUSHA_NAME,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GYOUSHA_ADDRESS,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GYOUSHA_BUSHO,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GYOUSHA_TANTOUSHA,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GYOUSHA_TEL,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GENBA_CD,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GENBA_NAME,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GENBA_ADDRESS,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GENBA_BUSHO,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GENBA_TANTOUSHA,
                                                           T_CARRY_ON_REQUEST_INXS.HAISHUTSU_GENBA_TEL,
                                                           T_CARRY_ON_REQUEST_INXS.TRUCK_TYPE_CD,
                                                           T_CARRY_ON_REQUEST_INXS.TRUCK_TYPE,
                                                           T_CARRY_ON_REQUEST_INXS.NUMBER_OF_TRUCK,
                                                           Cast(NULL AS INT) JOB_TYPE
                                                    FROM   T_CARRY_ON_REQUEST_UKETSUKE_MK_INXS
                                                           LEFT JOIN T_CARRY_ON_REQUEST_INXS
                                                                  ON T_CARRY_ON_REQUEST_INXS.SYSTEM_ID = T_CARRY_ON_REQUEST_UKETSUKE_MK_INXS.REQUEST_SYSTEM_ID
                                                                     AND T_CARRY_ON_REQUEST_INXS.DELETE_FLG = 0
                                                    WHERE  T_UKETSUKE_SS_MK_ENTRY.DPSR = 'MK'
                                                           AND T_CARRY_ON_REQUEST_UKETSUKE_MK_INXS.UKETSUKE_SYSTEM_ID = T_UKETSUKE_SS_MK_ENTRY.SYSTEM_ID
						                      ) T_PICKUP_CARRY_ON_REQUEST_INXS1 ");
                    }
                    #endregion INXS FROM句 収集＋持込
                    //Communicate InxsSubApplication End
                }
                //2013/12/06 修正(製造課題一覧No55) end

                // No.1598-->
                if (this.form.pnlSearchString.Visible)
                {
                    //CongBinh 20200331 #134987 S
                    //締日
                    if (!string.IsNullOrEmpty(this.form.cmbShimebi.Text))
                    {
                        // 取引先請求テーブルJOIN
                        sql.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU ON ");
                        sql.Append(summeryKbn);
                        sql.Append("_ENTRY.TORIHIKISAKI_CD = M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD ");
                    }
                    if (!string.IsNullOrEmpty(this.form.cmbShihariaShimebi.Text))
                    {
                        // 取引先支払テーブルJOIN
                        sql.Append(" LEFT JOIN M_TORIHIKISAKI_SHIHARAI ON ");
                        sql.Append(summeryKbn);
                        sql.Append("_ENTRY.TORIHIKISAKI_CD = M_TORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD ");
                    }
                    //CongBinh 20200331 #134987 E
                }
                // No.1598<--
                // パターンから作成されたJOIN句
                sql.Append(this.form.JoinQuery);
                #endregion

                #region WHERE句
                sql.Append(" WHERE ");
                //削除フラグ
                sql.Append(" " + summeryKbn
                               + "_ENTRY.DELETE_FLG = 0 ");

                //画面で在拠点CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text)
                      && this.headForm.KYOTEN_CD.Text != ConstCls.KyouTenZenSya)
                {
                    sql.Append(" AND " + summeryKbn
                                       + "_ENTRY.KYOTEN_CD = "
                                       + int.Parse(this.headForm.KYOTEN_CD.Text) + " ");
                }
                //画面で日付選択が作業日付の場合
                if (ConstCls.HidukeCD_DenPyou.Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND " + summeryKbn
                                           + "_ENTRY.UKETSUKE_DATE >= '"
                                           + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString()
                                           + " 00:00:00" + "' ");
                    }
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND " + summeryKbn
                                           + "_ENTRY.UKETSUKE_DATE <= '"
                                           + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString()
                                           + " 23:59:59" + "' ");
                    }
                }
                //画面で日付選択が入力日付の場合
                if (ConstCls.HidukeCD_NyuuRyoku.Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND " + summeryKbn
                                           + "_ENTRY.UPDATE_DATE >= '"
                                           + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString()
                                           + " 00:00:00" + "' ");
                    }
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND " + summeryKbn
                                           + "_ENTRY.UPDATE_DATE <= '"
                                           + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString()
                                           + " 23:59:59" + "' ");
                    }
                }
                // 画面で日付選択が作業日の場合（伝票種類が「収集」「出荷」「持込」「収集＋出荷」「収集＋持込」のとき）
                var denpyouShurui = this.form.txtNum_DenPyouSyurui.Text;
                if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU.Equals(denpyouShurui)
                    || ConstCls.DENPYOU_SHURUI_CD_SYUKKA.Equals(denpyouShurui)
                    || ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI.Equals(denpyouShurui)
                    || ConstCls.DENPYOU_SHURUI_CD_SS_SK.Equals(denpyouShurui)
                    || ConstCls.DENPYOU_SHURUI_CD_SS_MK.Equals(denpyouShurui))
                {
                    if (ConstCls.HidukeCD_Sagyou.Equals(this.headForm.txtNum_HidukeSentaku.Text))
                    {
                        if (this.headForm.HIDUKE_FROM.Value != null)
                        {
                            sql.Append(" AND ");
                            sql.Append(summeryKbn);
                            sql.Append("_ENTRY.SAGYOU_DATE >= '");
                            sql.Append(DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString());
                            sql.Append("' ");
                        }
                        if (this.headForm.HIDUKE_TO.Value != null)
                        {
                            sql.Append(" AND ");
                            sql.Append(summeryKbn);
                            sql.Append("_ENTRY.SAGYOU_DATE <= '");
                            sql.Append(DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString());
                            sql.Append("' ");
                        }
                    }
                }

                //SONNT #143061 受付一覧抽出条件の変更 2020/10 START
                if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
                {
                    if (this.form.UKETSUKE_EXPORT_KBN.Text == "1")
                    {
                        if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU.Equals(denpyouShurui))
                        {
                            sql.Append(" AND T_PICKUP_REQUEST_INXS1.SYSTEM_ID IS NULL ");
                        }
                        else if (ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI.Equals(denpyouShurui))
                        {
                            sql.Append(" AND T_CARRY_ON_REQUEST_INXS1.SYSTEM_ID IS NULL ");
                        }
                        else if (ConstCls.DENPYOU_SHURUI_CD_SS_MK.Equals(denpyouShurui))
                        {
                            sql.Append(" AND T_PICKUP_CARRY_ON_REQUEST_INXS1.SYSTEM_ID IS NULL ");
                        }
                    }
                    else if (this.form.UKETSUKE_EXPORT_KBN.Text == "2")
                    {
                        if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU.Equals(denpyouShurui))
                        {
                            sql.Append(" AND T_PICKUP_REQUEST_INXS1.SYSTEM_ID IS NOT NULL ");
                        }
                        else if (ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI.Equals(denpyouShurui))
                        {
                            sql.Append(" AND T_CARRY_ON_REQUEST_INXS1.SYSTEM_ID IS NOT NULL ");
                        }
                        else if (ConstCls.DENPYOU_SHURUI_CD_SS_MK.Equals(denpyouShurui))
                        {
                            sql.Append(" AND T_PICKUP_CARRY_ON_REQUEST_INXS1.SYSTEM_ID IS NOT NULL ");
                        }
                    }
                }
                //SONNT #143061 受付一覧抽出条件の変更 2020/10 END

                //画面条件
                if (this.form.pnlSearchString.Visible)
                {
                    //画面で取引先CDがnull無いの場合
                    if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                    {
                        sql.Append(" AND " + summeryKbn
                                           + "_ENTRY.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
                    }
                    //画面で業者CDがnull無いの場合
                    if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        sql.Append(" AND " + summeryKbn
                                           + "_ENTRY.GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "' ");
                    }
                    //画面で現場CDがnull無いの場合
                    if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        sql.Append(" AND " + summeryKbn
                                           + "_ENTRY.GENBA_CD = '" + this.form.GENBA_CD.Text + "' ");
                    }

                    if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouSyurui
                        || ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI == denpyouSyurui
                        || ConstCls.DENPYOU_SHURUI_CD_SS_MK == denpyouSyurui)
                    {
                        //画面で運搬業者CDがnull無いの場合
                        if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                        {
                            sql.Append(" AND " + summeryKbn
                                               + "_ENTRY.UNPAN_GYOUSHA_CD = '" + this.form.UNPAN_GYOUSHA_CD.Text + "' ");
                        }
                        //画面で荷降業者CDがnull無いの場合
                        if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                        {
                            sql.Append(" AND " + summeryKbn
                                               + "_ENTRY.NIOROSHI_GYOUSHA_CD = '" + this.form.NIOROSHI_GYOUSHA_CD.Text + "' ");
                        }
                        //画面で荷降場CDがnull無いの場合
                        if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                        {
                            sql.Append(" AND " + summeryKbn
                                               + "_ENTRY.NIOROSHI_GENBA_CD = '" + this.form.NIOROSHI_GENBA_CD.Text + "' ");
                        }
                    }
                    else if (ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouSyurui)
                    {
                        //画面で運搬業者CDがnull無いの場合
                        if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                        {
                            sql.Append(" AND " + summeryKbn
                                               + "_ENTRY.UNPAN_GYOUSHA_CD = '" + this.form.UNPAN_GYOUSHA_CD.Text + "' ");
                        }
                        //画面で荷降業者CDがnull無いの場合
                        if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                        {
                            sql.Append(" AND " + summeryKbn
                                               + "_ENTRY.NIZUMI_GYOUSHA_CD = '" + this.form.NIOROSHI_GYOUSHA_CD.Text + "' ");
                        }
                        //画面で荷降場CDがnull無いの場合
                        if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                        {
                            sql.Append(" AND " + summeryKbn
                                               + "_ENTRY.NIZUMI_GENBA_CD = '" + this.form.NIOROSHI_GENBA_CD.Text + "' ");
                        }
                    }
                    else if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouSyurui)
                    {
                        //画面で運搬業者CDがnull無いの場合
                        if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                        {
                            sql.Append(" AND " + summeryKbn
                                               + "_ENTRY.UNPAN_GYOUSHA_CD = '" + this.form.UNPAN_GYOUSHA_CD.Text + "' ");
                        }
                    }
                    if (ConstCls.DENPYOU_SHURUI_CD_SS_MK != denpyouSyurui)
                    {
                        // No.1598-->
                        //画面で配車状況CDがnull無いの場合
                        if (!string.IsNullOrEmpty(this.form.HAISHA_JOKYO_CD.Text))
                        {
                            sql.Append(" AND " + summeryKbn
                                               + "_ENTRY.HAISHA_JOKYO_CD = '" + this.form.HAISHA_JOKYO_CD.Text + "' ");
                        }
                        //画面で配車種類CDがnull無いの場合
                        if (!string.IsNullOrEmpty(this.form.HAISHA_SHURUI_CD.Text))
                        {
                            sql.Append(" AND " + summeryKbn
                                               + "_ENTRY.HAISHA_SHURUI_CD = '" + this.form.HAISHA_SHURUI_CD.Text + "' ");
                        }
                        // No.1598<--
                    }
                    if (this.form.pnlSearchString.Visible)
                    {
                        //CongBinh 20200331 #134987 S
                        //締日
                        if (!string.IsNullOrEmpty(this.form.cmbShimebi.Text))
                        {
                            sql.Append("AND (M_TORIHIKISAKI_SEIKYUU.SHIMEBI1 = '" + this.form.cmbShimebi.Text + "' ");
                            sql.Append("OR M_TORIHIKISAKI_SEIKYUU.SHIMEBI2 = '" + this.form.cmbShimebi.Text + "' ");
                            sql.Append("OR M_TORIHIKISAKI_SEIKYUU.SHIMEBI3 = '" + this.form.cmbShimebi.Text + "') ");
                        }
                        if (!string.IsNullOrEmpty(this.form.cmbShihariaShimebi.Text))
                        {
                            sql.Append("AND ( M_TORIHIKISAKI_SHIHARAI.SHIMEBI1 = '" + this.form.cmbShihariaShimebi.Text + "' ");
                            sql.Append("OR M_TORIHIKISAKI_SHIHARAI.SHIMEBI2 = '" + this.form.cmbShihariaShimebi.Text + "' ");
                            sql.Append("OR M_TORIHIKISAKI_SHIHARAI.SHIMEBI3 = '" + this.form.cmbShihariaShimebi.Text + "') ");
                        }
                        //CongBinh 20200331 #134987 E
                    }
                }
                #endregion

                #region ORDERBY句
                if (!string.IsNullOrEmpty(this.form.OrderByQuery))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(this.form.OrderByQuery);
                }
                #endregion

                this.searchSql = sql.ToString();
                sql.Append("");
            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeSearchCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 収集の検索用SQLの作成（伝票種類CD＝6:収集＋出荷：用）
        /// </summary>
        private string MakeUketsukeSsDataTableSSSK()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append("   '収集' AS DENPYOU_SHURUI_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SYSTEM_ID, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SEQ, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.KYOTEN_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_DATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.HAISHA_JOKYO_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.HAISHA_JOKYO_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.HAISHA_SHURUI_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.HAISHA_SHURUI_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TORIHIKISAKI_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TORIHIKISAKI_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GYOSHA_TEL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENBA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENBA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENBA_ADDRESS1, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENBA_ADDRESS2, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENBA_TEL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TANTOSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TANTOSHA_TEL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UNPAN_GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UNPAN_GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.EIGYOU_TANTOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.EIGYOU_TANTOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SAGYOU_DATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SAGYOU_DATE_BEGIN, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SAGYOU_DATE_END, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENCHAKU_TIME_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENCHAKU_TIME_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENCHAKU_TIME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SAGYOU_TIME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SAGYOU_TIME_BEGIN, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SAGYOU_TIME_END, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHASHU_DAISU_GROUP_NUMBER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHASHU_DAISU_NUMBER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHARYOU_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHARYOU_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHASHU_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHASHU_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UNTENSHA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UNTENSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.HOJOIN_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.HOJOIN_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.MANIFEST_SHURUI_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.MANIFEST_TEHAI_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.COURSE_KUMIKOMI_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.COURSE_NAME_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.HAISHA_SIJISHO_FLG, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.MAIL_SEND_FLG, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_BIKOU1, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_BIKOU2, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_BIKOU3, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UNTENSHA_SIJIJIKOU1, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UNTENSHA_SIJIJIKOU2, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UNTENSHA_SIJIJIKOU3, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.KINGAKU_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHOUHIZEI_RATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TAX_SOTO, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TAX_UCHI, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TAX_SOTO_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TAX_UCHI_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHOUHIZEI_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GOUKEI_KINGAKU_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.CREATE_USER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.CREATE_DATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.CREATE_PC, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UPDATE_USER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UPDATE_DATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UPDATE_PC, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.DELETE_FLG, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TIME_STAMP, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.DETAIL_SYSTEM_ID, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.ROW_NO, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.HINMEI_CD, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.HINMEI_NAME, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.DENPYOU_KBN_CD, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.SUURYOU, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.UNIT_CD, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.TANKA, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.KINGAKU, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.TAX_SOTO AS D_TAX_SOTO, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.TAX_UCHI AS D_TAX_UCHI, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.MEISAI_BIKOU, ");
            sql.Append("   URIAGE.KINGAKU_TOTAL AS URIAGE_KINGAKU_TOTAL, ");
            sql.Append("   SHIHARAI.KINGAKU_TOTAL AS SHIHARAI_KINGAKU_TOTAL, ");
            sql.Append("   'SS' AS DPSR ");
            sql.Append(" FROM T_UKETSUKE_SS_ENTRY ");
            sql.Append(" LEFT JOIN T_UKETSUKE_SS_DETAIL ");
            sql.Append("      ON T_UKETSUKE_SS_ENTRY.SYSTEM_ID = T_UKETSUKE_SS_DETAIL.SYSTEM_ID ");
            sql.Append("      AND T_UKETSUKE_SS_ENTRY.SEQ = T_UKETSUKE_SS_DETAIL.SEQ ");
            sql.Append(" LEFT JOIN ");
            sql.Append("    ( ");
            sql.Append("       SELECT ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ, ");
            sql.Append("       SUM(D.KINGAKU) AS KINGAKU_TOTAL ");
            sql.Append("       FROM ");
            sql.Append("       T_UKETSUKE_SS_ENTRY E, ");
            sql.Append("       T_UKETSUKE_SS_DETAIL D ");
            sql.Append("       WHERE ");
            sql.Append("       E.DELETE_FLG = 0 AND ");
            sql.Append("       E.SYSTEM_ID = D.SYSTEM_ID AND ");
            sql.Append("       E.SEQ = D.SEQ AND ");
            sql.Append("       D.DENPYOU_KBN_CD = 1 ");
            sql.Append("       GROUP BY ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ ");
            sql.Append("    ) URIAGE ON ");
            sql.Append("       T_UKETSUKE_SS_ENTRY.SYSTEM_ID = URIAGE.SYSTEM_ID AND ");
            sql.Append("       T_UKETSUKE_SS_ENTRY.SEQ = URIAGE.SEQ ");
            sql.Append(" LEFT JOIN ");
            sql.Append("       ( ");
            sql.Append("       SELECT ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ, ");
            sql.Append("       SUM(D.KINGAKU) AS KINGAKU_TOTAL ");
            sql.Append("       FROM ");
            sql.Append("       T_UKETSUKE_SS_ENTRY E, ");
            sql.Append("       T_UKETSUKE_SS_DETAIL D ");
            sql.Append("       WHERE ");
            sql.Append("       E.DELETE_FLG = 0 AND ");
            sql.Append("       E.SYSTEM_ID = D.SYSTEM_ID AND ");
            sql.Append("       E.SEQ = D.SEQ AND ");
            sql.Append("       D.DENPYOU_KBN_CD = 2 ");
            sql.Append("       GROUP BY ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ ");
            sql.Append("       ) SHIHARAI ON ");
            sql.Append("       T_UKETSUKE_SS_ENTRY.SYSTEM_ID = SHIHARAI.SYSTEM_ID AND ");
            sql.Append("       T_UKETSUKE_SS_ENTRY.SEQ = SHIHARAI.SEQ ");
            return sql.ToString();
        }

        /// <summary>
        /// 出荷の検索用SQLの作成
        /// </summary>
        private string MakeUketsukeSkDataTable()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append("   '出荷' AS DENPYOU_SHURUI_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SYSTEM_ID, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SEQ, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.KYOTEN_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UKETSUKE_NUMBER, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UKETSUKE_DATE, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.HAISHA_JOKYO_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.HAISHA_JOKYO_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.HAISHA_SHURUI_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.HAISHA_SHURUI_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.TORIHIKISAKI_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.TORIHIKISAKI_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GYOSHA_TEL, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GENBA_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GENBA_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GENBA_ADDRESS1, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GENBA_ADDRESS2, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GENBA_TEL, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.TANTOSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.TANTOSHA_TEL, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UNPAN_GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UNPAN_GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.EIGYOU_TANTOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.EIGYOU_TANTOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SAGYOU_DATE, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SAGYOU_DATE_BEGIN, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SAGYOU_DATE_END, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GENCHAKU_TIME_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GENCHAKU_TIME_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GENCHAKU_TIME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SAGYOU_TIME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SAGYOU_TIME_BEGIN, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SAGYOU_TIME_END, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SHASHU_DAISU_GROUP_NUMBER, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SHASHU_DAISU_NUMBER, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SHARYOU_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SHARYOU_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SHASHU_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SHASHU_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UNTENSHA_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UNTENSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.HOJOIN_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.HOJOIN_NAME, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.MANIFEST_SHURUI_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.MANIFEST_TEHAI_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.COURSE_KUMIKOMI_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.COURSE_NAME_CD, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.HAISHA_SIJISHO_FLG, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.MAIL_SEND_FLG, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UKETSUKE_BIKOU1, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UKETSUKE_BIKOU2, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UKETSUKE_BIKOU3, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UNTENSHA_SIJIJIKOU1, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UNTENSHA_SIJIJIKOU2, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UNTENSHA_SIJIJIKOU3, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.KINGAKU_TOTAL, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SHOUHIZEI_RATE, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.TAX_SOTO, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.TAX_UCHI, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.TAX_SOTO_TOTAL, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.TAX_UCHI_TOTAL, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.SHOUHIZEI_TOTAL, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.GOUKEI_KINGAKU_TOTAL, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.CREATE_USER, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.CREATE_DATE, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.CREATE_PC, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UPDATE_USER, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UPDATE_DATE, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.UPDATE_PC, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.DELETE_FLG, ");
            sql.Append("   T_UKETSUKE_SK_ENTRY.TIME_STAMP, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.DETAIL_SYSTEM_ID, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.ROW_NO, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.HINMEI_CD, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.HINMEI_NAME, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.DENPYOU_KBN_CD, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.SUURYOU, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.UNIT_CD, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.TANKA, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.KINGAKU, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.TAX_SOTO AS D_TAX_SOTO, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.TAX_UCHI AS D_TAX_UCHI, ");
            sql.Append("   T_UKETSUKE_SK_DETAIL.MEISAI_BIKOU, ");
            sql.Append("   URIAGE.KINGAKU_TOTAL AS URIAGE_KINGAKU_TOTAL, ");
            sql.Append("   SHIHARAI.KINGAKU_TOTAL AS SHIHARAI_KINGAKU_TOTAL, ");
            sql.Append("   'SK' AS DPSR ");
            sql.Append(" FROM T_UKETSUKE_SK_ENTRY ");
            sql.Append(" LEFT JOIN T_UKETSUKE_SK_DETAIL ");
            sql.Append("      ON T_UKETSUKE_SK_ENTRY.SYSTEM_ID = T_UKETSUKE_SK_DETAIL.SYSTEM_ID ");
            sql.Append("      AND T_UKETSUKE_SK_ENTRY.SEQ = T_UKETSUKE_SK_DETAIL.SEQ ");
            sql.Append(" LEFT JOIN ");
            sql.Append("    ( ");
            sql.Append("       SELECT ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ, ");
            sql.Append("       SUM(D.KINGAKU) AS KINGAKU_TOTAL ");
            sql.Append("       FROM ");
            sql.Append("       T_UKETSUKE_SK_ENTRY E, ");
            sql.Append("       T_UKETSUKE_SK_DETAIL D ");
            sql.Append("       WHERE ");
            sql.Append("       E.DELETE_FLG = 0 AND ");
            sql.Append("       E.SYSTEM_ID = D.SYSTEM_ID AND ");
            sql.Append("       E.SEQ = D.SEQ AND ");
            sql.Append("       D.DENPYOU_KBN_CD = 1 ");
            sql.Append("       GROUP BY ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ ");
            sql.Append("    ) URIAGE ON ");
            sql.Append("       T_UKETSUKE_SK_ENTRY.SYSTEM_ID = URIAGE.SYSTEM_ID AND ");
            sql.Append("       T_UKETSUKE_SK_ENTRY.SEQ = URIAGE.SEQ ");
            sql.Append(" LEFT JOIN ");
            sql.Append("       ( ");
            sql.Append("       SELECT ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ, ");
            sql.Append("       SUM(D.KINGAKU) AS KINGAKU_TOTAL ");
            sql.Append("       FROM ");
            sql.Append("       T_UKETSUKE_SK_ENTRY E, ");
            sql.Append("       T_UKETSUKE_SK_DETAIL D ");
            sql.Append("       WHERE ");
            sql.Append("       E.DELETE_FLG = 0 AND ");
            sql.Append("       E.SYSTEM_ID = D.SYSTEM_ID AND ");
            sql.Append("       E.SEQ = D.SEQ AND ");
            sql.Append("       D.DENPYOU_KBN_CD = 2 ");
            sql.Append("       GROUP BY ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ ");
            sql.Append("       ) SHIHARAI ON ");
            sql.Append("       T_UKETSUKE_SK_ENTRY.SYSTEM_ID = SHIHARAI.SYSTEM_ID AND ");
            sql.Append("       T_UKETSUKE_SK_ENTRY.SEQ = SHIHARAI.SEQ ");
            return sql.ToString();
        }

        /// <summary>
        /// 収集の検索用SQLの作成（伝票種類CD＝7:収集＋持込：用）
        /// </summary>
        private string MakeUketsukeSsDataTableSSMK()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append("   '収集' AS DENPYOU_SHURUI_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SYSTEM_ID, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SEQ, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.KYOTEN_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_DATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TORIHIKISAKI_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TORIHIKISAKI_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GYOSHA_TEL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENBA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENBA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENBA_TEL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TANTOSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TANTOSHA_TEL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UNPAN_GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UNPAN_GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.NIOROSHI_GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.NIOROSHI_GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.NIOROSHI_GENBA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.NIOROSHI_GENBA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.EIGYOU_TANTOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.EIGYOU_TANTOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SAGYOU_DATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENCHAKU_TIME_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENCHAKU_TIME_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GENCHAKU_TIME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHASHU_DAISU_GROUP_NUMBER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHASHU_DAISU_NUMBER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHARYOU_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHARYOU_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHASHU_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHASHU_NAME, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.MANIFEST_SHURUI_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.MANIFEST_TEHAI_CD, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_BIKOU1, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_BIKOU2, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UKETSUKE_BIKOU3, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.KINGAKU_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHOUHIZEI_RATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TAX_SOTO, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TAX_UCHI, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TAX_SOTO_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TAX_UCHI_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.SHOUHIZEI_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.GOUKEI_KINGAKU_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.CREATE_USER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.CREATE_DATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.CREATE_PC, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UPDATE_USER, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UPDATE_DATE, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.UPDATE_PC, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.DELETE_FLG, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.TIME_STAMP, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.DETAIL_SYSTEM_ID, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.ROW_NO, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.HINMEI_CD, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.HINMEI_NAME, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.DENPYOU_KBN_CD, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.SUURYOU, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.UNIT_CD, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.TANKA, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.KINGAKU, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.TAX_SOTO AS D_TAX_SOTO, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.TAX_UCHI AS D_TAX_UCHI, ");
            sql.Append("   T_UKETSUKE_SS_DETAIL.MEISAI_BIKOU, ");
            sql.Append("   URIAGE.KINGAKU_TOTAL AS URIAGE_KINGAKU_TOTAL, ");
            sql.Append("   SHIHARAI.KINGAKU_TOTAL AS SHIHARAI_KINGAKU_TOTAL, ");
            sql.Append("   T_UKETSUKE_SS_ENTRY.HAISHA_JOKYO_CD, ");
            sql.Append("   'SS' AS DPSR ");
            sql.Append(" FROM T_UKETSUKE_SS_ENTRY ");
            sql.Append(" LEFT JOIN T_UKETSUKE_SS_DETAIL ");
            sql.Append("      ON T_UKETSUKE_SS_ENTRY.SYSTEM_ID = T_UKETSUKE_SS_DETAIL.SYSTEM_ID ");
            sql.Append("      AND T_UKETSUKE_SS_ENTRY.SEQ = T_UKETSUKE_SS_DETAIL.SEQ ");
            sql.Append(" LEFT JOIN ");
            sql.Append("    ( ");
            sql.Append("       SELECT ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ, ");
            sql.Append("       SUM(D.KINGAKU) AS KINGAKU_TOTAL ");
            sql.Append("       FROM ");
            sql.Append("       T_UKETSUKE_SS_ENTRY E, ");
            sql.Append("       T_UKETSUKE_SS_DETAIL D ");
            sql.Append("       WHERE ");
            sql.Append("       E.DELETE_FLG = 0 AND ");
            sql.Append("       E.SYSTEM_ID = D.SYSTEM_ID AND ");
            sql.Append("       E.SEQ = D.SEQ AND ");
            sql.Append("       D.DENPYOU_KBN_CD = 1 ");
            sql.Append("       GROUP BY ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ ");
            sql.Append("    ) URIAGE ON ");
            sql.Append("       T_UKETSUKE_SS_ENTRY.SYSTEM_ID = URIAGE.SYSTEM_ID AND ");
            sql.Append("       T_UKETSUKE_SS_ENTRY.SEQ = URIAGE.SEQ ");
            sql.Append(" LEFT JOIN ");
            sql.Append("       ( ");
            sql.Append("       SELECT ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ, ");
            sql.Append("       SUM(D.KINGAKU) AS KINGAKU_TOTAL ");
            sql.Append("       FROM ");
            sql.Append("       T_UKETSUKE_SS_ENTRY E, ");
            sql.Append("       T_UKETSUKE_SS_DETAIL D ");
            sql.Append("       WHERE ");
            sql.Append("       E.DELETE_FLG = 0 AND ");
            sql.Append("       E.SYSTEM_ID = D.SYSTEM_ID AND ");
            sql.Append("       E.SEQ = D.SEQ AND ");
            sql.Append("       D.DENPYOU_KBN_CD = 2 ");
            sql.Append("       GROUP BY ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ ");
            sql.Append("       ) SHIHARAI ON ");
            sql.Append("       T_UKETSUKE_SS_ENTRY.SYSTEM_ID = SHIHARAI.SYSTEM_ID AND ");
            sql.Append("       T_UKETSUKE_SS_ENTRY.SEQ = SHIHARAI.SEQ ");
            return sql.ToString();
        }

        /// <summary>
        /// 持込の検索用SQLの作成
        /// </summary>
        private string MakeUketsukeMkDataTable()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append("   '持込' AS DENPYOU_SHURUI_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SYSTEM_ID, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SEQ, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.KYOTEN_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UKETSUKE_NUMBER, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UKETSUKE_DATE, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.TORIHIKISAKI_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.TORIHIKISAKI_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GYOSHA_TEL, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GENBA_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GENBA_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GENBA_TEL, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.TANTOSHA_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.TANTOSHA_TEL, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UNPAN_GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UNPAN_GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.NIOROSHI_GYOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.NIOROSHI_GYOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.NIOROSHI_GENBA_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.NIOROSHI_GENBA_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.EIGYOU_TANTOUSHA_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.EIGYOU_TANTOUSHA_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SAGYOU_DATE, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GENCHAKU_TIME_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GENCHAKU_TIME_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GENCHAKU_TIME, "); ;
            sql.Append("   T_UKETSUKE_MK_ENTRY.SHASHU_DAISU_GROUP_NUMBER, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SHASHU_DAISU_NUMBER, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SHARYOU_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SHARYOU_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SHASHU_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SHASHU_NAME, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.MANIFEST_SHURUI_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.MANIFEST_TEHAI_CD, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UKETSUKE_BIKOU1, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UKETSUKE_BIKOU2, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UKETSUKE_BIKOU3, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.KINGAKU_TOTAL, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SHOUHIZEI_RATE, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.TAX_SOTO, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.TAX_UCHI, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.TAX_SOTO_TOTAL, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.TAX_UCHI_TOTAL, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.SHOUHIZEI_TOTAL, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.GOUKEI_KINGAKU_TOTAL, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.CREATE_USER, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.CREATE_DATE, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.CREATE_PC, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UPDATE_USER, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UPDATE_DATE, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.UPDATE_PC, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.DELETE_FLG, ");
            sql.Append("   T_UKETSUKE_MK_ENTRY.TIME_STAMP, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.DETAIL_SYSTEM_ID, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.ROW_NO, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.HINMEI_CD, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.HINMEI_NAME, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.DENPYOU_KBN_CD, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.SUURYOU, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.UNIT_CD, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.TANKA, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.KINGAKU, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.TAX_SOTO AS D_TAX_SOTO, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.TAX_UCHI AS D_TAX_UCHI, ");
            sql.Append("   T_UKETSUKE_MK_DETAIL.MEISAI_BIKOU, ");
            sql.Append("   URIAGE.KINGAKU_TOTAL AS URIAGE_KINGAKU_TOTAL, ");
            sql.Append("   SHIHARAI.KINGAKU_TOTAL AS SHIHARAI_KINGAKU_TOTAL, ");
            sql.Append("   -1 AS HAISHA_JOKYO_CD, ");
            sql.Append("   'MK' AS DPSR ");
            sql.Append(" FROM T_UKETSUKE_MK_ENTRY ");
            sql.Append(" LEFT JOIN T_UKETSUKE_MK_DETAIL ");
            sql.Append("      ON T_UKETSUKE_MK_ENTRY.SYSTEM_ID = T_UKETSUKE_MK_DETAIL.SYSTEM_ID ");
            sql.Append("      AND T_UKETSUKE_MK_ENTRY.SEQ = T_UKETSUKE_MK_DETAIL.SEQ ");
            sql.Append(" LEFT JOIN ");
            sql.Append("    ( ");
            sql.Append("       SELECT ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ, ");
            sql.Append("       SUM(D.KINGAKU) AS KINGAKU_TOTAL ");
            sql.Append("       FROM ");
            sql.Append("       T_UKETSUKE_MK_ENTRY E, ");
            sql.Append("       T_UKETSUKE_MK_DETAIL D ");
            sql.Append("       WHERE ");
            sql.Append("       E.DELETE_FLG = 0 AND ");
            sql.Append("       E.SYSTEM_ID = D.SYSTEM_ID AND ");
            sql.Append("       E.SEQ = D.SEQ AND ");
            sql.Append("       D.DENPYOU_KBN_CD = 1 ");
            sql.Append("       GROUP BY ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ ");
            sql.Append("    ) URIAGE ON ");
            sql.Append("       T_UKETSUKE_MK_ENTRY.SYSTEM_ID = URIAGE.SYSTEM_ID AND ");
            sql.Append("       T_UKETSUKE_MK_ENTRY.SEQ = URIAGE.SEQ ");
            sql.Append(" LEFT JOIN ");
            sql.Append("       ( ");
            sql.Append("       SELECT ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ, ");
            sql.Append("       SUM(D.KINGAKU) AS KINGAKU_TOTAL ");
            sql.Append("       FROM ");
            sql.Append("       T_UKETSUKE_MK_ENTRY E, ");
            sql.Append("       T_UKETSUKE_MK_DETAIL D ");
            sql.Append("       WHERE ");
            sql.Append("       E.DELETE_FLG = 0 AND ");
            sql.Append("       E.SYSTEM_ID = D.SYSTEM_ID AND ");
            sql.Append("       E.SEQ = D.SEQ AND ");
            sql.Append("       D.DENPYOU_KBN_CD = 2 ");
            sql.Append("       GROUP BY ");
            sql.Append("       E.SYSTEM_ID, ");
            sql.Append("       E.SEQ ");
            sql.Append("       ) SHIHARAI ON ");
            sql.Append("       T_UKETSUKE_MK_ENTRY.SYSTEM_ID = SHIHARAI.SYSTEM_ID AND ");
            sql.Append("       T_UKETSUKE_MK_ENTRY.SEQ = SHIHARAI.SEQ ");
            return sql.ToString();
        }

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        public void HeaderCheckBoxSupport()
        {
            LogUtility.DebugMethodStart();
            string denpyouSyurui = this.form.txtNum_DenPyouSyurui.Text;
            //[伝票種類]＝「1」（収集）、「2」（出荷）、「6」（収集＋出荷）の場合、チェックできる
            if (denpyouSyurui.Equals(ConstCls.DENPYOU_SHURUI_CD_SYUSYU)
                || denpyouSyurui.Equals(ConstCls.DENPYOU_SHURUI_CD_SYUKKA)
                || denpyouSyurui.Equals(ConstCls.DENPYOU_SHURUI_CD_SS_SK))
            {
                if (!this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = ConstCls.CELL_CHECKBOX;
                    newColumn.HeaderText = "指   ";
                    newColumn.Width = 60;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "指   ";
                    newColumn.HeaderCell = newheader;
                    newColumn.Resizable = DataGridViewTriState.False;
                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.form.customDataGridView1.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns.Add(newColumn);
                    }
                    this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }
            }
            else
            {
                if (this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
                {
                    this.form.customDataGridView1.Columns.Remove(ConstCls.CELL_CHECKBOX);
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// DataGridViewのプロパティ再設定
        /// </summary>
        private void setDataGridView()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.AllowUserToResizeColumns = true;
                setDataGridViewColumn(this.form.customDataGridView1);
                this.form.customDataGridView1.AllowUserToResizeRows = false;
                string denpyouSyurui = this.form.txtNum_DenPyouSyurui.Text;
                if (denpyouSyurui.Equals(ConstCls.DENPYOU_SHURUI_CD_SYUSYU)
                || denpyouSyurui.Equals(ConstCls.DENPYOU_SHURUI_CD_SYUKKA)
                || denpyouSyurui.Equals(ConstCls.DENPYOU_SHURUI_CD_SS_SK))
                {
                    foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                    {
                        string haishajoukyo_cd = dgvRow.Cells[ConstCls.HIDDEN_HAISHA_JOKYO_CD].Value.ToString();
                        string haishashurui_cd = dgvRow.Cells[ConstCls.HIDDEN_HAISHA_SHURUI_CD].Value.ToString();
                        //配車状況が「1.受注」又は配車状況が「2.配車」かつ配車種類が「3.確定」の場合、チェックができる
                        if (haishajoukyo_cd.Equals("1") || (haishajoukyo_cd.Equals("2") && haishashurui_cd.Equals("3")))
                        {
                            this.form.customDataGridView1.Rows[dgvRow.Index].Cells[0].ReadOnly = false;
                        }
                        //他の場合、チェックができない
                        else
                        {
                            this.form.customDataGridView1.Rows[dgvRow.Index].Cells[0].ReadOnly = true;
                        }
                    }
                }
                else
                {
                    this.form.customDataGridView1.Columns[0].ReadOnly = true;
                }
                this.notReadOnlyColumns();
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDataGridView", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Column非表示設定
        /// </summary>
        /// <param name="dtgv"></param>
        private void setDataGridViewColumn(CustomDataGridView dtgv)
        {
            try
            {
                LogUtility.DebugMethodStart(dtgv);
                //入力画面へ遷移用Column（システムID）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_SYSTEM_ID))
                {
                    dtgv.Columns[ConstCls.HIDDEN_SYSTEM_ID].Visible = false;
                }
                //入力画面へ遷移用Column（枝番）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_SEQ))
                {
                    dtgv.Columns[ConstCls.HIDDEN_SEQ].Visible = false;
                }
                //入力画面へ遷移用Column（受付番号）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_UKETSUKE_NUMBER))
                {
                    dtgv.Columns[ConstCls.HIDDEN_UKETSUKE_NUMBER].Visible = false;
                }
                //入力画面へ遷移用Column（受付日）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_UKETSUKE_DATE))
                {
                    dtgv.Columns[ConstCls.HIDDEN_UKETSUKE_DATE].Visible = false;
                }
                //入力画面へ遷移用Column（配車状況CD）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_HAISHA_JOKYO_CD))
                {
                    dtgv.Columns[ConstCls.HIDDEN_HAISHA_JOKYO_CD].Visible = false;
                }
                //入力画面へ遷移用Column(明細システムID）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_DETAIL_SYSTEM_ID))
                {
                    dtgv.Columns[ConstCls.HIDDEN_DETAIL_SYSTEM_ID].Visible = false;
                }
                //Communicate InxsSubApplication Start
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_INXS_DETAIL_SYSTEM_ID))
                {
                    dtgv.Columns[ConstCls.HIDDEN_INXS_DETAIL_SYSTEM_ID].Visible = false;
                }
                //Communicate InxsSubApplication End
                //入力画面へ遷移用Column(業者ERROR）を非表示にする
                if (dtgv.Columns.Contains("HST_GYOUSHA_CD_ERROR"))
                {
                    dtgv.Columns["HST_GYOUSHA_CD_ERROR"].Visible = false;
                }
                //入力画面へ遷移用Column(業者と現場ERROR）を非表示にする
                if (dtgv.Columns.Contains("HST_GENBA_CD_ERROR"))
                {
                    dtgv.Columns["HST_GENBA_CD_ERROR"].Visible = false;
                }
                //入力画面へ遷移用Column(品名ERROR）を非表示にする
                if (dtgv.Columns.Contains("HAIKI_SHURUI_CD_ERROR"))
                {
                    dtgv.Columns["HAIKI_SHURUI_CD_ERROR"].Visible = false;
                }
                //入力画面へ遷移用Column(伝票種類）を非表示にする
                if (dtgv.Columns.Contains("HIDDEN_DENPYOU_SHURUI_CD"))
                {
                    dtgv.Columns["HIDDEN_DENPYOU_SHURUI_CD"].Visible = false;
                }
                //入力画面へ遷移用Column（配車種類CD）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_HAISHA_SHURUI_CD))
                {
                    dtgv.Columns[ConstCls.HIDDEN_HAISHA_SHURUI_CD].Visible = false;
                }
                //mapbox用Column（緯度）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_GENBA_LATITUDE))
                {
                    dtgv.Columns[ConstCls.HIDDEN_GENBA_LATITUDE].Visible = false;
                }
                //mapbox用Column（経度）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_GENBA_LONGITUDE))
                {
                    dtgv.Columns[ConstCls.HIDDEN_GENBA_LONGITUDE].Visible = false;
                }
                //mapbox用Column（伝票種類）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_DENPYOU_SHURUI_NAME))
                {
                    dtgv.Columns[ConstCls.HIDDEN_DENPYOU_SHURUI_NAME].Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDataGridViewColumn", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dtgv);
            }
        }
        #endregion

        #region Functionボタン 押下処理
        /// <summary>
        /// [F1]指示書
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 明細単位で一覧を表示している場合、同じ受付番号でも複数行表示される。
                // 指示書については1伝票で1回の印刷で良いので、同じ受付番号の指示書が複数回印刷されないように制御する。
                List<SqlInt64> processedUketsukeNumber = new List<SqlInt64>();
                bool printError = false;
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].Value == true)
                    {
                        var uketsukeNumber = SqlInt64.Parse(dgvRow.Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value.ToString());
                        if (processedUketsukeNumber.Contains(uketsukeNumber)) continue;
                        printError = true;
                        var denpyouShurui = this.form.GetDenpyouShuruiCd();
                        //伝票種類が1:収集の場合、
                        if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouShurui)
                        {
                            T_UKETSUKE_SS_ENTRY entity = new T_UKETSUKE_SS_ENTRY();
                            entity.SYSTEM_ID = SqlInt64.Parse(dgvRow.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString());
                            entity.SEQ = SqlInt32.Parse(dgvRow.Cells[ConstCls.HIDDEN_SEQ].Value.ToString());
                            T_UKETSUKE_SS_ENTRY[] list = this.daoUketsukeSSEntry.GetDataForEntity(entity);

                            if (list.Length > 0)
                            {
                                this.SyuusyuuReport(list.ToList());
                            }
                        }
                        //伝票種類が2:出荷の場合、
                        else if (ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouShurui)
                        {
                            T_UKETSUKE_SK_ENTRY entity = new T_UKETSUKE_SK_ENTRY();
                            entity.SYSTEM_ID = SqlInt64.Parse(dgvRow.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString());
                            entity.SEQ = SqlInt32.Parse(dgvRow.Cells[ConstCls.HIDDEN_SEQ].Value.ToString());
                            T_UKETSUKE_SK_ENTRY[] list = this.daoUketsukeSKEntry.GetDataForEntity(entity);

                            if (list.Length > 0)
                            {
                                this.SyukkaReport(list.ToList());
                            }
                        }
                        //伝票種類が6:収集＋出荷の場合、
                        else if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouShurui)
                        {
                            string dpsr = dgvRow.Cells["HIDDEN_DENPYOU_SHURUI_CD"].Value.ToString();

                            //収集のデータの場合
                            if (dpsr.Equals("SS"))
                            {
                                T_UKETSUKE_SS_ENTRY entity = new T_UKETSUKE_SS_ENTRY();
                                entity.SYSTEM_ID = SqlInt64.Parse(dgvRow.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString());
                                entity.SEQ = SqlInt32.Parse(dgvRow.Cells[ConstCls.HIDDEN_SEQ].Value.ToString());
                                T_UKETSUKE_SS_ENTRY[] list = this.daoUketsukeSSEntry.GetDataForEntity(entity);

                                if (list.Length > 0)
                                {
                                    this.SyuusyuuReport(list.ToList());
                                }
                            }
                            //出荷のデータの場合
                            else if (dpsr.Equals("SK"))
                            {
                                T_UKETSUKE_SK_ENTRY entity = new T_UKETSUKE_SK_ENTRY();
                                entity.SYSTEM_ID = SqlInt64.Parse(dgvRow.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString());
                                entity.SEQ = SqlInt32.Parse(dgvRow.Cells[ConstCls.HIDDEN_SEQ].Value.ToString());
                                T_UKETSUKE_SK_ENTRY[] list = this.daoUketsukeSKEntry.GetDataForEntity(entity);

                                if (list.Length > 0)
                                {
                                    this.SyukkaReport(list.ToList());
                                }
                            }
                        }
                        processedUketsukeNumber.Add(uketsukeNumber);
                    }
                }
                //該当するデータがありません
                if (printError == false)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E076");
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
        /// <summary>
        /// F2 新規
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //入力画面へ遷移する（新規モード）
                forwardNyuuryoku(WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func2_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E076");
                }
                else
                {
                    //Communicate InxsSubApplication Start
                    if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke()
                        && this.form.UKETSUKE_EXPORT_KBN.Text == "2" //INXS
                        && !SystemProperty.Shain.InxsTantouFlg)
                    {
                        if (DialogResult.Yes != errmessage.MessageBoxShowConfirm(MSG_CONFIRM_EDIT_DENPYOU))
                        {
                            return;
                        }
                    }
                    //Communicate InxsSubApplication End

                    //入力画面へ遷移する（修正モード）
                    forwardNyuuryoku(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func3_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E076");
                }
                else
                {
                    ////Communicate InxsSubApplication Start
                    //if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke()
                    //    && this.form.chkShougunInxs.Checked
                    //    && !SystemProperty.Shain.InxsTantouFlg)
                    //{
                    //    errmessage.MessageBoxShowError(MSG_DELETE_DENPYOU);
                    //    return;
                    //}
                    ////Communicate InxsSubApplication End

                    //SONNT #142899 受付一覧 2020/10 START
                    bool catchErr;
                    if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
                    {
                        bool isReference = CheckReferenceInxs(out catchErr);
                        if (catchErr) return;

                        if (isReference)
                        {
                            errmessage.MessageBoxShowError(MSG_ERROR_DELETE_DENPYOU);
                            return;
                        }
                    }
                    //SONNT #142899 受付一覧 2020/10 END

                    //入力画面へ遷移する（削除モード）
                    forwardNyuuryoku(WINDOW_TYPE.DELETE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func4_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        //SONNT #142899 受付一覧 2020/10 START
        private bool CheckReferenceInxs(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = false;
            bool isReference = false;
            try
            {
                DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
                string denpyouShurui = form.txtNum_DenPyouSyurui.Text;
                if (denpyouShurui == ConstCls.DENPYOU_SHURUI_CD_SS_MK)
                {
                    string dpsr = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["HIDDEN_DENPYOU_SHURUI_CD"].Value.ToString();
                    if (dpsr.Equals(ConstCls.DATA_SYUUSYUU))
                    {
                        denpyouShurui = ConstCls.DENPYOU_SHURUI_CD_SYUSYU;
                    }
                    else if (dpsr.Equals(ConstCls.DATA_MOCHIKOMI))
                    {
                        denpyouShurui = ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI;
                    }
                }

                StringBuilder sql = new StringBuilder();
                if (denpyouShurui == ConstCls.DENPYOU_SHURUI_CD_SYUSYU)
                {
                    sql.Append(" SELECT UKETSUKE_SYSTEM_ID ");
                    sql.Append(" FROM T_PICKUP_REQUEST_UKETSUKE_SS_INXS ");
                    sql.AppendFormat(" WHERE UKETSUKE_SYSTEM_ID = {0} ", this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_SYSTEM_ID].Value);
                }
                else if (denpyouShurui == ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI)
                {
                    sql.Append(" SELECT UKETSUKE_SYSTEM_ID ");
                    sql.Append(" FROM T_CARRY_ON_REQUEST_UKETSUKE_MK_INXS ");
                    sql.AppendFormat(" WHERE UKETSUKE_SYSTEM_ID = {0} ", this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_SYSTEM_ID].Value);
                }
                if (sql.Length > 0)
                {
                    var dtReturn = this.mDetailDao.getdateforstringsql(sql.ToString());
                    isReference = dtReturn != null && dtReturn.Rows.Count > 0;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckReferenceInxs", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckReferenceInxs", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isReference, catchErr);
            }

            return isReference;
        }
        //SONNT #142899 受付一覧 2020/10 END

        /// <summary>
        /// F5 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E076");
                }
                else
                {
                    //入力画面へ遷移する（新規モード）
                    forwardNyuuryoku(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func5_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、CSV出力処理はしない
                    MessageBoxUtility.MessageBoxShow("E044");
                }
                else
                {
                    //CSV出力確認メッセージを表示する
                    if (MessageBoxUtility.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        //共通部品を利用して、画面に表示されているデータをCSVに出力する
                        CSVExport CSVExport = new CSVExport();
                        CSVExport.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, ConstCls.CSV_NAME, this.form);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F7 条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.Init_Heaher(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func7_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F8検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // パターン未登録の場合検索処理を行わない
                if (this.form.PatternNo == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                //Start Sontt 20150710
                if (this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
                {
                    DataGridViewCheckBoxHeaderCell header = this.form.customDataGridView1.Columns[ConstCls.CELL_CHECKBOX].HeaderCell as DataGridViewCheckBoxHeaderCell;
                    if (header != null)
                    {
                        header._checked = false;
                    }
                }
                //End Sontt 20150710
                this.HeaderCheckBoxFalse();

                //検索処理を行う
                this.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F9 現場メモ登録
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            DataTable dt = null;

            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row != null)
            {
                // パラメータを設定する。
                T_GENBAMEMO_ENTRY paramEntry = new T_GENBAMEMO_ENTRY();
                paramEntry.HASSEIMOTO_SYSTEM_ID = SqlInt64.Parse(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString());

                if (this.form.txtNum_DenPyouSyurui.Text.Equals("1")
                    || this.form.txtNum_DenPyouSyurui.Text.Equals("2")
                    || this.form.txtNum_DenPyouSyurui.Text.Equals("3")
                    || this.form.txtNum_DenPyouSyurui.Text.Equals("4"))
                {
                    if (this.form.txtNum_DenPyouSyurui.Text.Equals("1"))
                    {
                        paramEntry.HASSEIMOTO_CD = "2";
                        paramEntry.HASSEIMOTO_NAME = "収集受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "1");
                    }
                    else if (this.form.txtNum_DenPyouSyurui.Text.Equals("2"))
                    {
                        paramEntry.HASSEIMOTO_CD = "3";
                        paramEntry.HASSEIMOTO_NAME = "出荷受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "2");
                    }
                    else if (this.form.txtNum_DenPyouSyurui.Text.Equals("3"))
                    {
                        paramEntry.HASSEIMOTO_CD = "4";
                        paramEntry.HASSEIMOTO_NAME = "持込受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "3");
                    }
                    else if (this.form.txtNum_DenPyouSyurui.Text.Equals("4"))
                    {
                        paramEntry.HASSEIMOTO_CD = "2";
                        paramEntry.HASSEIMOTO_NAME = "収集受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "4");
                    }
                }
                else
                {
                    string denpyouShurui = row.Cells["HIDDEN_DENPYOU_SHURUI_CD"].Value.ToString();
                    if (denpyouShurui.Equals("SS"))
                    {
                        paramEntry.HASSEIMOTO_CD = "2";
                        paramEntry.HASSEIMOTO_NAME = "収集受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "1");
                    }
                    else if (denpyouShurui.Equals("SK"))
                    {
                        paramEntry.HASSEIMOTO_CD = "3";
                        paramEntry.HASSEIMOTO_NAME = "出荷受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "2");
                    }
                    else if (denpyouShurui.Equals("MK"))
                    {
                        paramEntry.HASSEIMOTO_CD = "4";
                        paramEntry.HASSEIMOTO_NAME = "持込受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "3");
                    }

                }

                // 取引先、業者、現場を設定
                if (dt != null)
                {
                    paramEntry.TORIHIKISAKI_CD = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                    paramEntry.TORIHIKISAKI_NAME = dt.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                    paramEntry.GYOUSHA_CD = dt.Rows[0]["GYOUSHA_CD"].ToString();
                    paramEntry.GYOUSHA_NAME = dt.Rows[0]["GYOUSHA_NAME"].ToString();
                    paramEntry.GENBA_CD = dt.Rows[0]["GENBA_CD"].ToString();
                    paramEntry.GENBA_NAME = dt.Rows[0]["GENBA_NAME"].ToString();
                }

                WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                FormManager.OpenFormWithAuth("G741", windowType, windowType, string.Empty, string.Empty, paramEntry, WINDOW_ID.T_UKETSUKE_ICHIRAN.ToString());
            }
            else
            {
                // 明細が未選択の場合は、エラーとする。
                msgLogic.MessageBoxShow("E051", "対象データ");
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F10並び替え
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E076");
                }
                else
                {
                    //ソート設定ダイアログを呼び出す
                    this.form.customSortHeader1.ShowCustomSortSettingDialog();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func10_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //フィルタ設定ダイアログを呼び出す
                this.form.customSearchHeader1.ShowCustomSearchSettingDialog();
                //読込データ件数           #13032
                if (this.form.customDataGridView1 != null)
                {
                    this.headForm.readDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headForm.readDataNumber.Text = "0";
                }
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

        #region 明細データダブルクリックイベント
        private void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (e.RowIndex < 0)
                {
                    // ヘッダダブルクリックの場合は標準の動きをさせる為にここでは何もしない
                    return;
                }
                CustomDataGridView customDataGridView = (CustomDataGridView)sender;
                if (customDataGridView.RowCount != 0)
                {
                    //Communicate InxsSubApplication Start
                    if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke()
                         && this.form.UKETSUKE_EXPORT_KBN.Text == "2" //INXS
                        && !SystemProperty.Shain.InxsTantouFlg)
                    {
                        if (DialogResult.Yes != errmessage.MessageBoxShowConfirm(MSG_CONFIRM_EDIT_DENPYOU))
                        {
                            return;
                        }
                    }
                    //Communicate InxsSubApplication End

                    //入力画面へ遷移する（修正モード）
                    forwardNyuuryoku(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("customDataGridView1_MouseDoubleClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        /// <summary>
        /// ヘッダーのチェックボックスクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.form.customDataGridView1.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                DataGridViewCheckBoxHeaderCell header = col.HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                }
            }
        }

        #region 画面遷移処理
        /// <summary>
        /// 入力画面へ遷移する
        /// </summary>
        private void forwardNyuuryoku(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);
                //伝票番号を初期化
                String chouseiNumber = String.Empty;
                //2013/12/04 仕様変更対応 修正 start
                //画面区分
                string gamenKbn = this.form.txtNum_DenPyouSyurui.Text.ToString();
                //修正、削除、複写の場合
                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(windowType)
                    || WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(windowType)
                     || WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(windowType))
                {
                    //選択されたレコードを取得する
                    DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
                    //受付番号に設定
                    chouseiNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value.ToString();
                    if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == gamenKbn || ConstCls.DENPYOU_SHURUI_CD_SS_MK == gamenKbn)
                    {
                        string dpsr = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["HIDDEN_DENPYOU_SHURUI_CD"].Value.ToString();

                        if (dpsr.Equals(ConstCls.DATA_SYUUSYUU))
                        {
                            gamenKbn = ConstCls.DENPYOU_SHURUI_CD_SYUSYU;
                        }
                        else if (dpsr.Equals(ConstCls.DATA_SYUKKA))
                        {
                            gamenKbn = ConstCls.DENPYOU_SHURUI_CD_SYUKKA;
                        }
                        else if (dpsr.Equals(ConstCls.DATA_MOCHIKOMI))
                        {
                            gamenKbn = ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI;
                        }
                    }
                    //ＤＢ存在チックを行う
                    if (!IsExistData(chouseiNumber, gamenKbn))
                    {
                        //受付番号が登録されていない場合
                        return;
                    }
                    if (WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(windowType))
                    {
                        //複写の場合、新規モードを設定する
                        windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    }
                    var tTeikiHaishaDetailDao = DaoInitUtility.GetComponent<TeikiHaishaDetailDao>();
                    var teikiHaishaDetailCount = tTeikiHaishaDetailDao.GetTeikiHaishaDetailCount(new T_TEIKI_HAISHA_DETAIL() { UKETSUKE_NUMBER = Int64.Parse(chouseiNumber) });
                    if (teikiHaishaDetailCount > 0)
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == windowType)
                        {
                            messageLogic.MessageBoxShow("E161", "修正");
                            windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }
                        else if (WINDOW_TYPE.DELETE_WINDOW_FLAG == windowType)
                        {
                            messageLogic.MessageBoxShow("E161", "削除");
                            return;
                        }
                    }
                }
                //2013/12/04 仕様変更対応 修正 end
                //画面遷移
                switch (gamenKbn)
                {
                    case "1":
                        //受付（収集）入力
                        // 権限チェック
                        if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                            !r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            if (!r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                MessageBoxUtility.MessageBoxShow("E158", "修正");
                                return;
                            }
                            windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }
                        FormManager.OpenFormWithAuth("G015", windowType, windowType, chouseiNumber);
                        break;
                    case "2":
                        //受付（出荷）入力
                        // 権限チェック
                        if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                            !r_framework.Authority.Manager.CheckAuthority("G016", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            if (!r_framework.Authority.Manager.CheckAuthority("G016", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                MessageBoxUtility.MessageBoxShow("E158", "修正");
                                return;
                            }
                            windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }
                        FormManager.OpenFormWithAuth("G016", windowType, windowType, chouseiNumber);
                        break;
                    case "3":
                        //受付（持込）入力
                        // 権限チェック
                        if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                            !r_framework.Authority.Manager.CheckAuthority("G018", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            if (!r_framework.Authority.Manager.CheckAuthority("G018", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                MessageBoxUtility.MessageBoxShow("E158", "修正");
                                return;
                            }
                            windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }
                        FormManager.OpenFormWithAuth("G018", windowType, windowType, chouseiNumber);
                        break;
                    case "4":
                        //受付（クレーム）入力
                        // 権限チェック
                        if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                            !r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            if (!r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                MessageBoxUtility.MessageBoxShow("E158", "修正");
                                return;
                            }
                            windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }
                        FormManager.OpenFormWithAuth("G020", windowType, windowType, chouseiNumber);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("forwardNyuuryoku", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(windowType);
            }
        }

        /// <summary>
        /// 計量入力画面を開きます
        /// </summary>
        /// <param name="selectedRow">選択している行</param>
        /// <param name="isCheckHaishaJokyoCd">配車状況CDのチェックを行う場合は、True</param>
        private void ShowKeiryoNyuryoku(DataGridViewRow selectedRow, bool isCheckHaishaJokyoCd)
        {
            LogUtility.DebugMethodStart(selectedRow, isCheckHaishaJokyoCd);
            bool isShow = true;
            var uketsukeNumber = Int64.Parse(selectedRow.Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value.ToString());
            var haishaJokyoCd = String.Empty;
            if (isCheckHaishaJokyoCd)
            {
                haishaJokyoCd = selectedRow.Cells[ConstCls.HIDDEN_HAISHA_JOKYO_CD].Value.ToString();
                // 配車状況が「1:受注」「2:配車」「3:計上」以外は遷移できない
                if (ConstCls.HAISHA_JOKYO_CD_CANCEL == haishaJokyoCd || ConstCls.HAISHA_JOKYO_CD_NASHI == haishaJokyoCd)
                {
                    isShow = false;
                }
            }
            if (isShow)
            {
                // arg[0] WINDOW_TYPE　モード　新規/修正/削除
                // arg[1] long 計量番号 -1:なし 0以上:有効な受入番号
                // arg[2] long 受付番号 -1:なし 0以上:有効な受付番号
                // arg[3] true:滞留登録,false:滞留登録なし
                FormManager.OpenForm("G045", WINDOW_TYPE.NEW_WINDOW_FLAG, -1L, uketsukeNumber);
            }
            else
            {
                MessageBoxUtility.MessageBoxShow("E101", "伝票", "売上");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受入入力画面を開きます
        /// </summary>
        /// <param name="selectedRow">選択している行</param>
        /// <param name="isCheckHaishaJokyoCd">配車状況CDのチェックを行う場合は、True</param>
        private void ShowUkeireNyuryoku(DataGridViewRow selectedRow, bool isCheckHaishaJokyoCd)
        {
            LogUtility.DebugMethodStart(selectedRow, isCheckHaishaJokyoCd);
            bool isShow = true;
            var uketsukeNumber = Int64.Parse(selectedRow.Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value.ToString());
            var haishaJokyoCd = String.Empty;
            if (isCheckHaishaJokyoCd)
            {
                haishaJokyoCd = selectedRow.Cells[ConstCls.HIDDEN_HAISHA_JOKYO_CD].Value.ToString();
                // 配車状況が「1:受注」「2:配車」「3:計上」以外は遷移できない
                if (ConstCls.HAISHA_JOKYO_CD_CANCEL == haishaJokyoCd || ConstCls.HAISHA_JOKYO_CD_NASHI == haishaJokyoCd)
                {
                    isShow = false;
                }
            }
            if (isShow)
            {
                // arg[0] WINDOW_TYPE　モード　新規/修正/削除
                // arg[1] long 受入番号 -1:なし 0以上:有効な受入番号
                // arg[2] 実行メソッド delegate
                // arg[3] long 受付番号 -1:なし 0以上:有効な受付番号
                // arg[4] long 計量番号 -1:なし 0以上:有効な計量番号
                // arg[5] bool 継続計量 true:する false:しない
                if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                {
                    FormManager.OpenFormWithAuth("G051", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, -1, null, uketsukeNumber);
                }
                else
                {
                    FormManager.OpenFormWithAuth("G721", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, -1, null, uketsukeNumber);
                }
            }
            else
            {
                MessageBoxUtility.MessageBoxShow("E101", "伝票", "売上");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷入力画面を開きます
        /// </summary>
        /// <param name="selectedRow">選択している行</param>
        /// <param name="isCheckHaishaJokyoCd">配車状況CDのチェックを行う場合は、True</param>
        private void ShowShukkaNyuryoku(DataGridViewRow selectedRow, bool isCheckHaishaJokyoCd)
        {
            LogUtility.DebugMethodStart(selectedRow, isCheckHaishaJokyoCd);
            bool isShow = true;
            var uketsukeNumber = Int64.Parse(selectedRow.Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value.ToString());
            var haishaJokyoCd = String.Empty;
            if (isCheckHaishaJokyoCd)
            {
                haishaJokyoCd = selectedRow.Cells[ConstCls.HIDDEN_HAISHA_JOKYO_CD].Value.ToString();
                // 配車状況が「1:受注」「2:配車」「3:計上」以外は遷移できない
                if (ConstCls.HAISHA_JOKYO_CD_CANCEL == haishaJokyoCd || ConstCls.HAISHA_JOKYO_CD_NASHI == haishaJokyoCd)
                {
                    isShow = false;
                }
            }
            if (isShow)
            {
                // arg[0] WINDOW_TYPE　モード　新規/修正/削除
                // arg[1] long 出荷番号 -1:なし 0以上:有効な出荷番号
                // arg[2] 実行メソッド delegate
                // arg[3] long 受付番号 -1:なし 0以上:有効な受付番号
                // arg[4] long 計量番号 -1:なし 0以上:有効な計量番号
                // arg[5] bool 継続計量 true:する false:しない
                if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                {
                    FormManager.OpenFormWithAuth("G053", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, -1, null, uketsukeNumber);
                }
                else
                {
                    FormManager.OpenFormWithAuth("G722", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, -1, null, uketsukeNumber);
                }
            }
            else
            {
                MessageBoxUtility.MessageBoxShow("E101", "伝票", "売上");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 売上／支払入力画面を開きます
        /// </summary>
        /// <param name="selectedRow">選択している行</param>
        /// <param name="isCheckHaishaJokyoCd">配車状況CDのチェックを行う場合は、True</param>
        private void ShowUriageShiharaiNyuryoku(DataGridViewRow selectedRow, bool isCheckHaishaJokyoCd)
        {
            LogUtility.DebugMethodStart(selectedRow, isCheckHaishaJokyoCd);
            bool isShow = true;
            var uketsukeNumber = Int64.Parse(selectedRow.Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value.ToString());
            var haishaJokyoCd = String.Empty;
            if (isCheckHaishaJokyoCd)
            {
                haishaJokyoCd = selectedRow.Cells[ConstCls.HIDDEN_HAISHA_JOKYO_CD].Value.ToString();
                // 配車状況が「1:受注」「2:配車」「3:計上」「5:回収なし」以外は遷移できない
                if (ConstCls.HAISHA_JOKYO_CD_CANCEL == haishaJokyoCd)
                {
                    isShow = false;
                }
            }
            if (isShow)
            {
                // arg[0] WINDOW_TYPE　モード　新規/修正/削除
                // arg[1] long 売上/出荷番号 -1:なし 0以上:有効な出荷番号
                // arg[2] 実行メソッド delegate
                // arg[3] long 受付番号 -1:なし 0以上:有効な受付番号
                FormManager.OpenFormWithAuth("G054", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, -1, null, uketsukeNumber);
            }
            else
            {
                MessageBoxUtility.MessageBoxShow("E101", "伝票", "売上");
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        //2013/12/04 仕様変更対応 追加 start
        #region 指定した受付番号のデータが存在するか返す
        /// <summary>
        /// 指定した受付番号のデータが存在するか返す
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistData(string uketsukeNumber, string GamenKbn)
        {
            // 戻り値初期化
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart(uketsukeNumber, GamenKbn);
                if (!string.IsNullOrEmpty(uketsukeNumber))
                {
                    //SQL文格納StringBuilder
                    var chkSql = new StringBuilder();
                    chkSql.Append(" SELECT * FROM ");
                    switch (GamenKbn)
                    {
                        case "1":
                            //受付（収集）入力
                            chkSql.Append(" T_UKETSUKE_SS_ENTRY AS ENTRY ");
                            break;
                        case "2":
                            //受付（出荷）入力
                            chkSql.Append(" T_UKETSUKE_SK_ENTRY AS ENTRY ");
                            break;
                        case "3":
                            //受付（持込）入力
                            chkSql.Append(" T_UKETSUKE_MK_ENTRY AS ENTRY ");
                            break;
                        case "4":
                            //受付（クレーム）入力
                            chkSql.Append(" T_UKETSUKE_CM_ENTRY AS ENTRY ");
                            break;
                    }
                    chkSql.Append(" WHERE ");
                    chkSql.Append(" ENTRY.UKETSUKE_NUMBER = " + uketsukeNumber);
                    chkSql.Append(" AND ENTRY.DELETE_FLG = 0 ");
                    // データを検索
                    var dtReturn = this.mDetailDao.getdateforstringsql(chkSql.ToString());
                    // 0件の場合
                    if (dtReturn.Rows.Count > 0)
                    {
                        // 戻り値
                        returnVal = true;
                    }
                    else
                    {
                        //アラートを表示し、画面遷移しない
                        returnVal = false;
                        MessageBoxUtility.MessageBoxShow("E045");
                    }
                }
                LogUtility.DebugMethodEnd(returnVal);

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
        //2013/12/04 仕様変更対応 追加 end

        #region プロセスボタン押下処理
        /// <summary>
        /// サブファンクションボタン[1]をクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var sysID = this.form.OpenPatternIchiran();
                // 適用ボタンが押された場合
                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData(this.form.Table);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 現場メモ一覧画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            DataTable dt = null;

            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row != null)
            {
                // パラメータを設定する。
                T_GENBAMEMO_ENTRY paramEntry = new T_GENBAMEMO_ENTRY();
                paramEntry.HASSEIMOTO_SYSTEM_ID = SqlInt64.Parse(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString());

                if (this.form.txtNum_DenPyouSyurui.Text.Equals("1")
                    || this.form.txtNum_DenPyouSyurui.Text.Equals("2")
                    || this.form.txtNum_DenPyouSyurui.Text.Equals("3")
                    || this.form.txtNum_DenPyouSyurui.Text.Equals("4"))
                {
                    if (this.form.txtNum_DenPyouSyurui.Text.Equals("1"))
                    {
                        paramEntry.HASSEIMOTO_CD = "2";
                        paramEntry.HASSEIMOTO_NAME = "収集受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "1");
                    }
                    else if (this.form.txtNum_DenPyouSyurui.Text.Equals("2"))
                    {
                        paramEntry.HASSEIMOTO_CD = "3";
                        paramEntry.HASSEIMOTO_NAME = "出荷受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "2");
                    }
                    else if (this.form.txtNum_DenPyouSyurui.Text.Equals("3"))
                    {
                        paramEntry.HASSEIMOTO_CD = "4";
                        paramEntry.HASSEIMOTO_NAME = "持込受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "3");
                    }
                    else if (this.form.txtNum_DenPyouSyurui.Text.Equals("4"))
                    {
                        paramEntry.HASSEIMOTO_CD = "2";
                        paramEntry.HASSEIMOTO_NAME = "収集受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "4");
                    }
                }
                else
                {
                    string denpyouShurui = row.Cells["HIDDEN_DENPYOU_SHURUI_CD"].Value.ToString();
                    if (denpyouShurui.Equals("SS"))
                    {
                        paramEntry.HASSEIMOTO_CD = "2";
                        paramEntry.HASSEIMOTO_NAME = "収集受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "1");
                    }
                    else if (denpyouShurui.Equals("SK"))
                    {
                        paramEntry.HASSEIMOTO_CD = "3";
                        paramEntry.HASSEIMOTO_NAME = "出荷受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "2");
                    }
                    else if (denpyouShurui.Equals("MK"))
                    {
                        paramEntry.HASSEIMOTO_CD = "4";
                        paramEntry.HASSEIMOTO_NAME = "持込受付";
                        dt = this.getEntryInfo(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString(), row.Cells[ConstCls.HIDDEN_SEQ].Value.ToString(), "3");
                    }

                }

                // 取引先、業者、現場を設定
                if (dt.Rows.Count > 0)
                {
                    paramEntry.TORIHIKISAKI_CD = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                    paramEntry.TORIHIKISAKI_NAME = dt.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                    paramEntry.GYOUSHA_CD = dt.Rows[0]["GYOUSHA_CD"].ToString();
                    paramEntry.GYOUSHA_NAME = dt.Rows[0]["GYOUSHA_NAME"].ToString();
                    paramEntry.GENBA_CD = dt.Rows[0]["GENBA_CD"].ToString();
                    paramEntry.GENBA_NAME = dt.Rows[0]["GENBA_NAME"].ToString();
                }

                FormManager.OpenFormWithAuth("G742", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, paramEntry, WINDOW_ID.T_UKETSUKE_ICHIRAN.ToString());

            }
            else
            {
                // 明細が未選択の場合は、画面遷移のみとする。
                FormManager.OpenFormWithAuth("G742", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 収集受付、出荷受付、持込受付、クレーム受付からデータを取得する。
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="SEQ"></param>
        /// <param name="denpyouShuruids"></param>
        /// <returns></returns>
        private DataTable getEntryInfo(string systemId, string SEQ, string denpyouShurui)
        {
            DataTable dt = null;

            var chkSql = new StringBuilder();
            chkSql.Append(" SELECT TORIHIKISAKI_CD, TORIHIKISAKI_NAME, GYOUSHA_CD, GYOUSHA_NAME, GENBA_CD, GENBA_NAME FROM ");

            switch (denpyouShurui)
            {
                case "1":
                    //受付（収集）入力
                    chkSql.Append(" T_UKETSUKE_SS_ENTRY ");
                    break;
                case "2":
                    //受付（出荷）入力
                    chkSql.Append(" T_UKETSUKE_SK_ENTRY ");
                    break;
                case "3":
                    //受付（持込）入力
                    chkSql.Append(" T_UKETSUKE_MK_ENTRY ");
                    break;
                case "4":
                    //受付（クレーム）入力
                    chkSql.Append(" T_UKETSUKE_CM_ENTRY ");
                    break;
            }

            chkSql.Append(" WHERE DELETE_FLG = 0 AND SYSTEM_ID = '" + systemId + "' AND SEQ = "  + SEQ);
            dt = this.mDetailDao.getdateforstringsql(chkSql.ToString());

            return dt;

        }
        
        /// <summary>
        /// サブファンクションボタン[3]をクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void bt_process3_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                try
                {
                    // 件数チェック
                    if (!this.CheckForCheckBox())
                    {
                        return;
                    }

                    if (this.errmessage.MessageBoxShowConfirm("地図を表示しますか？" +
                        Environment.NewLine + "※緯度/経度が登録されていない現場は表示されません。") == DialogResult.No)
                    {
                        return;
                    }

                    MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                    // 地図に渡すDTO作成
                    List<mapDtoList> dtos = new List<mapDtoList>();
                    dtos = this.createMapboxDto();
                    if (dtos.Count == 0)
                    {
                        this.errmessage.MessageBoxShowError("表示する対象がありません。");
                        return;
                    }

                    // 地図表示
                    gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.T_UKETSUKE_ICHIRAN);
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
            catch (Exception ex)
            {
                LogUtility.Error("bt_process2_Click", ex);
                this.errmessage.MessageBoxShowError(ex.Message);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// サブファンクションボタン[4]をクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void bt_process4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var row = this.form.GetSelectedRow();
            if (null != row)
            {
                var denpyouShurui = this.form.GetDenpyouShuruiCd();
                if (ConstCls.DENPYOU_SHURUI_CD_SS_SK == denpyouShurui || ConstCls.DENPYOU_SHURUI_CD_SS_MK == denpyouShurui)
                {
                    string haishajoukyo_cd = this.form.customDataGridView1.Rows[row.Index].Cells[ConstCls.HIDDEN_HAISHA_JOKYO_CD].Value.ToString();

                    if (haishajoukyo_cd.Equals(ConstCls.NOT_CHECK_HAISHA_JYOUKYOU_CD))
                    {
                        denpyouShurui = ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI;
                    }
                    else
                    {
                        string dpsr = this.form.customDataGridView1.Rows[row.Index].Cells["HIDDEN_DENPYOU_SHURUI_CD"].Value.ToString();

                        if (dpsr.Equals(ConstCls.DATA_SYUUSYUU))
                        {
                            denpyouShurui = ConstCls.DENPYOU_SHURUI_CD_SYUSYU;
                        }
                        else if (dpsr.Equals(ConstCls.DATA_SYUKKA))
                        {
                            denpyouShurui = ConstCls.DENPYOU_SHURUI_CD_SYUKKA;
                        }
                    }
                }
                string uketsukeNumber = Convert.ToString(row.Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value);
                if (ConstCls.DENPYOU_SHURUI_CD_SYUSYU == denpyouShurui)
                {
                    if (!RenkeiCheck(uketsukeNumber))
                    {
                        return;
                    }
                    this.ShowUkeireNyuryoku(row, true);
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_SYUKKA == denpyouShurui)
                {
                    if (!RenkeiCheckSyukka(uketsukeNumber))
                    {
                        return;
                    }
                    this.ShowShukkaNyuryoku(row, true);
                }
                else if (ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI == denpyouShurui)
                {
                    this.ShowUkeireNyuryoku(row, false);
                }
            }
            else
            {
                MessageBoxUtility.MessageBoxShow("E076");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// サブファンクションボタン[5]をクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void bt_process5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var row = this.form.GetSelectedRow();
            if (null != row)
            {
                var denpyouShurui = this.form.GetDenpyouShuruiCd();
                string uketsukeNumber = Convert.ToString(row.Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value);
                if (ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI != denpyouShurui)
                {
                    if (this.form.customDataGridView1.Columns.Contains("HIDDEN_DENPYOU_SHURUI_CD"))
                    {
                        string dpsr = this.form.customDataGridView1.Rows[row.Index].Cells["HIDDEN_DENPYOU_SHURUI_CD"].Value.ToString();
                        if (dpsr.Equals(ConstCls.DATA_SYUUSYUU) || dpsr.Equals(ConstCls.DENPYOU_SHURUI_CD_SYUSYU))
                        {
                            if (!RenkeiCheck(uketsukeNumber))
                            {
                                return;
                            }
                        }

                        if (dpsr.Equals(ConstCls.DATA_SYUKKA) || dpsr.Equals(ConstCls.DENPYOU_SHURUI_CD_SYUKKA))
                        {
                            if (!RenkeiCheckSyukka(uketsukeNumber))
                            {
                                return;
                            }
                        }
                    }
                    this.ShowUriageShiharaiNyuryoku(row, true);
                }
                else
                {
                    this.ShowUriageShiharaiNyuryoku(row, false);
                }
            }
            else
            {
                MessageBoxUtility.MessageBoxShow("E076");
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 汎用検索(SearchString)内でのエンターキー押下イベント
        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void SearchStringKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(this.form.searchString.Text))
                    {
                        string getSearchString = this.form.searchString.Text.Replace("\r", "").Replace("\n", "");
                        this.searchString = getSearchString;
                        this.Search();
                    }
                    else
                    {
                        this.form.searchString.Text = "";
                        this.form.searchString.SelectionLength = this.form.searchString.Text.Length;
                        this.form.searchString.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchStringKeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region 関連チェック
        /// <summary>
        /// 取引先情報取得
        /// </summary>
        /// <param name="toriCd">取引先CD</param>
        /// <returns>取引先マスタエンティティのリスト（実質一つ）</returns>
        public M_TORIHIKISAKI[] GetTorihikisakiInfo(string toriCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(toriCd);
            var toriEntitys = new M_TORIHIKISAKI[0];
            catchErr = true;
            try
            {
                var tDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                var toriEntity = new M_TORIHIKISAKI();
                toriEntity.TORIHIKISAKI_CD = toriCd;
                toriEntity.ISNOT_NEED_DELETE_FLG = true;
                // エンティティから取引先情報を絞り込んで取得
                toriEntitys = tDao.GetAllValidData(toriEntity);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisakiInfo", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisakiInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(toriEntitys, catchErr);
            }
            return toriEntitys;
        }

        /// <summary>
        /// 取引先CDを元に締日の有効チェックを行う
        /// </summary>
        /// <param name="toriCd">取引先CD</param>
        /// <returns>true:有効, false:無効</returns>
        public bool CheckShimebi(string toriCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(toriCd);
            var seikyuEntity = new M_TORIHIKISAKI_SEIKYUU();
            var shiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
            var ret = false;
            catchErr = true;
            try
            {
                var seDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                var shDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
                // 取引先CDを元に取引請求及び取引先支払マスタのエンティティを取得
                seikyuEntity = seDao.GetDataByCd(toriCd);
                shiharaiEntity = shDao.GetDataByCd(toriCd);

                // 取引先請求でチェック
                if (seikyuEntity != null &&
                    ((!seikyuEntity.SHIMEBI1.IsNull && seikyuEntity.SHIMEBI1.ToString() == this.form.cmbShimebi.Text) ||
                     (!seikyuEntity.SHIMEBI2.IsNull && seikyuEntity.SHIMEBI2.ToString() == this.form.cmbShimebi.Text) ||
                     (!seikyuEntity.SHIMEBI3.IsNull && seikyuEntity.SHIMEBI3.ToString() == this.form.cmbShimebi.Text)))
                {
                    ret = true;
                }
                //CongBinh 20200331 #134987 S
                // 取引先支払でチャック
                if (shiharaiEntity != null &&
                   ((!shiharaiEntity.SHIMEBI1.IsNull && shiharaiEntity.SHIMEBI1.ToString() == this.form.cmbShihariaShimebi.Text) ||
                   (!shiharaiEntity.SHIMEBI2.IsNull && shiharaiEntity.SHIMEBI2.ToString() == this.form.cmbShihariaShimebi.Text) ||
                   (!shiharaiEntity.SHIMEBI3.IsNull && shiharaiEntity.SHIMEBI3.ToString() == this.form.cmbShihariaShimebi.Text)))
                {
                    ret = true;
                }
                //CongBinh 20200331 #134987 E
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckShimebi", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShimebi", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 業者情報取得
        /// </summary>
        /// <param name="gosyaCd">業者CD</param>
        public M_GYOUSHA[] GetGyousyaInfo(string gosyaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gosyaCd);
                IM_GYOUSHADao gDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                M_GYOUSHA gEntity = new M_GYOUSHA();
                gEntity.GYOUSHA_CD = gosyaCd;
                gEntity.ISNOT_NEED_DELETE_FLG = true;
                //業者情報取得
                var returnEntitys = gDao.GetAllValidData(gEntity);
                LogUtility.DebugMethodEnd(returnEntitys, catchErr);
                return returnEntitys;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousyaInfo", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousyaInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }


        /// <summary>
        /// 現場情報取得
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="gosyaCd">業者CD</param>
        /// <returns></returns>
        public M_GENBA[] GetGenbaInfo(string genbaCd, string gosyaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(genbaCd, gosyaCd);
                IM_GENBADao gDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                M_GENBA gEntity = new M_GENBA();
                //現場CD
                gEntity.GENBA_CD = genbaCd;
                //業者CD
                if (gosyaCd != "")
                {
                    gEntity.GYOUSHA_CD = gosyaCd;
                }
                gEntity.ISNOT_NEED_DELETE_FLG = true;
                //現場情報取得
                //現場マスタ（M_GENBA）を[業者CD]、[現場CD]で検索する
                M_GENBA[] returnEntitys = gDao.GetAllValidData(gEntity);
                LogUtility.DebugMethodEnd(returnEntitys, catchErr);
                return returnEntitys;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenbaInfo", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbaInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 荷現場情報取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <param name="gosyaCd"></param>
        /// <returns></returns>
        public DataTable GetNioroshiGenbaInfo(string genbaCd, string gosyaCd, out bool catchErr)
        {
            catchErr = true;
            DataTable dt = new DataTable();
            try
            {
                LogUtility.DebugMethodStart(genbaCd, gosyaCd);
                IM_GENBADao gDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                string whereStr = " ";
                //現場CD
                whereStr = whereStr + " WHERE M_GENBA.GENBA_CD =" + "'" + genbaCd + "'";
                //業者CD
                if (gosyaCd != "")
                {
                    whereStr = whereStr + " AND M_GENBA.GYOUSHA_CD =" + "'" + gosyaCd + "'";
                }
                //現場情報取得
                //現場マスタ（M_GENBA）を[業者CD]、[現場CD]で検索する
                var thisAssembly = Assembly.GetExecutingAssembly();
                using (var resourceStream = thisAssembly.GetManifestResourceStream(this.executeSqlFilePath))
                {
                    using (var sqlStr = new StreamReader(resourceStream))
                    {
                        dt = gDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);
                        sqlStr.Close();
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetNioroshiGenbaInfo", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNioroshiGenbaInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dt, catchErr);
            }
            return dt;
        }
        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region 使うない

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

        /// <summary>
        /// 配車状況CDチェック
        /// </summary>
        /// <param name="haishaJokyoCd">チェック対象の配車CD</param>
        /// <returns>チェック結果</returns>
        internal bool CheckHaishaJokyoCd(string haishaJokyoCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(haishaJokyoCd);
            bool ret = false;
            catchErr = true;
            try
            {
                if (String.IsNullOrEmpty(haishaJokyoCd))
                {
                    ret = true;
                }
                var count = this.haishaJokyoDataTable.Rows.Cast<DataRow>().Where(r => !String.IsNullOrEmpty(haishaJokyoCd) && r.ItemArray[0].ToString() == haishaJokyoCd).Count();
                if (0 < count)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHaishaJokyoCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// パラメータのCDに対応した配車状況を取得します
        /// </summary>
        /// <param name="haishaJokyoCd">配車状況CD</param>
        /// <returns>配車状況</returns>
        internal string GetHaishaJokyo(string haishaJokyoCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(haishaJokyoCd);
            string ret = String.Empty;
            catchErr = true;
            try
            {
                ret = this.haishaJokyoDataTable.Rows.Cast<DataRow>().Where(r => r.ItemArray[0].ToString() == haishaJokyoCd).Select(r => r.ItemArray[1].ToString()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHaishaJokyo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(ret, catchErr);
            return ret;
        }

        /// <summary>
        /// 受付番号に該当する受入入力の件数を取得します
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <returns>件数</returns>
        private int GetUkeireEntryCount(Int64 uketsukeNumber)
        {
            LogUtility.DebugMethodStart(uketsukeNumber);
            int ret = 0;
            var dao = DaoInitUtility.GetComponent<UkeireDAOClass>();
            var keyEntity = new T_UKEIRE_ENTRY();
            keyEntity.UKETSUKE_NUMBER = uketsukeNumber;
            ret = dao.GetUkeireCount(keyEntity);
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 受付番号に該当する出荷入力の件数を取得します
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <returns>件数</returns>
        private int GetShukkaEntryCount(Int64 uketsukeNumber)
        {
            LogUtility.DebugMethodStart(uketsukeNumber);
            int ret = 0;
            var dao = DaoInitUtility.GetComponent<ShukkaDAOClass>();
            var keyEntity = new T_SHUKKA_ENTRY();
            keyEntity.UKETSUKE_NUMBER = uketsukeNumber;
            ret = dao.GetShukkaCount(keyEntity);
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 受付番号に該当する売上支払入力の件数を取得します
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <returns>件数</returns>
        private int GetUriageShiharaiEntryCount(Int64 uketsukeNumber)
        {
            LogUtility.DebugMethodStart(uketsukeNumber);
            int ret = 0;
            var dao = DaoInitUtility.GetComponent<UR_SHDAOClass>();
            var keyEntity = new T_UR_SH_ENTRY();
            keyEntity.UKETSUKE_NUMBER = uketsukeNumber;
            ret = dao.GetUR_ShCount(keyEntity);
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        // No.3123-->
        /// <summary>
        /// 次のタブストップのコントロールにフォーカス移動
        /// </summary>
        /// <param name="foward"></param>
        public void GotoNextControl(bool foward)
        {
            Control control = NextFormControl(foward);
            if (control != null)
            {
                control.Focus();
            }
        }

        public void HeaderFocus()
        {
            this.headForm.HIDUKE_TO.Focus();
        }

        /// <summary>
        /// 現在のコントロールの次のタブストップコントールを探す
        /// </summary>
        /// <param name="foward"></param>
        /// <returns></returns>
        public Control NextFormControl(bool foward)
        {
            try
            {
                Control control = null;
                bool startflg = false;
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(tabUiFormControlNames);
                if (foward == false)
                {
                    formControlNameList.Reverse();
                }
                foreach (var controlName in formControlNameList)
                {
                    control = controlUtil.FindControl(this.form, controlName);
                    if (control != null)
                    {
                        if (startflg)
                        {
                            // 次のコントロール
                            if (control.TabStop == true && control.Visible == true && control.Enabled == true)
                            {
                                return control;
                            }
                        }
                        else if (this.form.ActiveControl != null && this.form.ActiveControl.Equals(control))
                        {   // 現在のactiveコントロ－ル
                            startflg = true;
                        }
                    }
                    else
                    {
                        control = controlUtil.FindControl(this.headForm, controlName);
                        if (control != null)
                        {
                            if (startflg)
                            {
                                // 次のコントロール
                                if (control.TabStop == true && control.Visible == true && control.Enabled == true)
                                {
                                    return control;
                                }
                            }
                            else if (this.headForm.ActiveControl != null && this.headForm.ActiveControl.Equals(control))
                            {   // 現在のactiveコントロ－ル
                                startflg = true;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                // 最後までみつからない場合、最初から探す
                foreach (var controlName in formControlNameList)
                {
                    control = controlUtil.FindControl(this.form, controlName);
                    if (control == null)
                    {
                        control = controlUtil.FindControl(this.headForm, controlName);
                    }
                    if (control != null)
                    {
                        if (control.TabStop == true && control.Visible == true && control.Enabled == true)
                        {
                            break;
                        }
                    }
                }
                return control;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
        }
        // No.3123<--

        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.headForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.headForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text))
            {
                return false;
            }
            DateTime date_from = DateTime.Parse(this.headForm.HIDUKE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.headForm.HIDUKE_TO.Text);
            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.headForm.HIDUKE_FROM.IsInputErrorOccured = true;
                this.headForm.HIDUKE_TO.IsInputErrorOccured = true;
                this.headForm.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.headForm.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                string errorMsg = "日付範囲の設定を見直してください。";
                MessageBoxUtility.MessageBoxShowError(errorMsg);
                this.headForm.HIDUKE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

        /// <summary>
        /// 伝票種類が「4.物販」用のプロパティを設定します
        /// </summary>
        /// <param name="bppanFlg">
        /// 物販用のプロパティを設定するかを示します
        /// true = 伝票種類が物販の場合プロパティを設定
        /// false = 伝票種類が物販以外の場合のプロパティを設定
        /// </param>
        internal void BppanForPropertySetting(bool bppanFlg)
        {
            if (bppanFlg)
            {
                // 日付選択３の名称
                this.headForm.radbtnSagyoubi.Text = "3.納品予定日";
                // 日付選択のパネル
                this.headForm.panel_HidukeSentaku.Size = new Size(310, 20);
                // 日付選択３のヒントテキスト
                this.headForm.radbtnSagyoubi.Tag = "日付種類が「3.納品予定日」の場合にはチェックを付けてください";
                // 日付のテキスト
                if (this.headForm.radbtnSagyoubi.Checked)
                {
                    this.headForm.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_NouhinYotei;
                }
            }
            else
            {
                // 日付選択３の名称
                this.headForm.radbtnSagyoubi.Text = "3.作業日";
                // 日付選択のパネル
                this.headForm.panel_HidukeSentaku.Size = new Size(283, 20);
                // 日付選択３のヒントテキスト
                this.headForm.radbtnSagyoubi.Tag = "日付種類が「3.作業日」の場合にはチェックを付けてください";
                // 日付のテキスト
                if (this.headForm.radbtnDenpyouHiduke.Checked)
                {
                    this.headForm.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_DenPyou;
                }
                else if (this.headForm.radbtnNyuuryokuHiduke.Checked)
                {
                    this.headForm.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_NyuuRyoku;
                }
                else if (this.headForm.radbtnSagyoubi.Checked)
                {
                    this.headForm.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_Sagyou;
                }
            }
        }
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141127 teikyou ダブルクリックを追加する　start
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var hidukeFromTextBox = this.headForm.HIDUKE_FROM;
            var hidukeToTextBox = this.headForm.HIDUKE_TO;
            hidukeToTextBox.Text = hidukeFromTextBox.Text;
            LogUtility.DebugMethodEnd();
        }
        // 20141127 teikyou ダブルクリックを追加する　end
        #endregion

        /// <summary>
        /// 収集データの印刷
        /// </summary>
        public bool SyuusyuuReport(List<T_UKETSUKE_SS_ENTRY> insEntryEntityList)
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();
                string hikaePrintFlg;
                // レポートのWindowID
                WINDOW_ID windowID = WINDOW_ID.R_SAGYOU_SIJISYO;
                // 控え印刷フラグ
                if (this.IsHikaePrint())
                {
                    // 正・控え（二枚印刷）
                    hikaePrintFlg = "1";
                }
                else
                {
                    // 正のみ印刷
                    hikaePrintFlg = "0";
                }
                // 受付収集入力レコードをループし、印刷を行う(新規登録の場合、複数レコードを更新)
                foreach (T_UKETSUKE_SS_ENTRY entity in insEntryEntityList)
                {
                    // printクラスを呼出
                    ReportInfoR345_R350 reportInfoR345_R350 = new ReportInfoR345_R350(windowID, this.daoUketsukeSSEntry);
                    // 控え印刷(0：正のみ　1:正・控え２部)
                    reportInfoR345_R350.ParameterList["HikaeType"] = hikaePrintFlg;
                    // 伝票番号(受付番号)
                    reportInfoR345_R350.ParameterList["DenpyouNumber"] = entity.UKETSUKE_NUMBER.ToString();
                    // 受付種類(1：収集　2:出荷)
                    reportInfoR345_R350.ParameterList["UketukeType"] = "1";
                    reportInfoR345_R350.Create(@".\Template\R345_R350-Form.xml", "LAYOUT1", new DataTable());
                    reportInfoR345_R350.Title = "指示書";
                    FormReportPrintPopup formReport = new FormReportPrintPopup(reportInfoR345_R350, "R350", WINDOW_ID.UKETSUKE_SHUSHU);
                    // 印刷アプリ初期動作(直印刷)
                    formReport.PrintInitAction = 1;
                    //直接印刷
                    formReport.PrintXPS();
                    // 印刷されてない場合、印刷フラグを更新
                    if (entity.HAISHA_SIJISHO_FLG == false)
                    {
                        if (this.form.HAISHA_JOKYO_CD.Text != "1")
                        {
                            // 配車状況が受注のとき以外の場合に更新
                            // 配車指示書フラグを更新（印刷された場合、更新を行う）
                            this.UpdPrintFlgForSS((long)entity.SYSTEM_ID, (int)entity.SEQ);
                        }
                    }
                }
                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 出荷データの印刷
        /// </summary>
        public bool SyukkaReport(List<T_UKETSUKE_SK_ENTRY> insEntryEntityList)
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();
                string hikaePrintFlg;
                // レポートのWindowID
                WINDOW_ID windowID = WINDOW_ID.R_SAGYOU_SIJISYO;
                // 控え印刷フラグ
                if (this.IsHikaePrint())
                {
                    // 正・控え（二枚印刷）
                    hikaePrintFlg = "1";
                }
                else
                {
                    // 正のみ印刷
                    hikaePrintFlg = "0";
                }
                // 受付収集入力レコードをループし、印刷を行う(新規登録の場合、複数レコードを更新)
                foreach (T_UKETSUKE_SK_ENTRY entity in insEntryEntityList)
                {
                    // printクラスを呼出
                    ReportInfoR345_R350 reportInfoR345_R350 = new ReportInfoR345_R350(windowID, this.daoUketsukeSKEntry);
                    // 控え印刷(0：正のみ　1:正・控え２部)
                    reportInfoR345_R350.ParameterList["HikaeType"] = hikaePrintFlg;
                    // 伝票番号(受付番号)
                    reportInfoR345_R350.ParameterList["DenpyouNumber"] = entity.UKETSUKE_NUMBER.ToString();
                    // 受付種類(1：収集　2:出荷)
                    reportInfoR345_R350.ParameterList["UketukeType"] = "2";
                    reportInfoR345_R350.Create(@".\Template\R345_R350-Form.xml", "LAYOUT1", new DataTable());
                    reportInfoR345_R350.Title = "指示書";
                    FormReportPrintPopup formReport = new FormReportPrintPopup(reportInfoR345_R350, "R350", WINDOW_ID.UKETSUKE_SHUKKA);
                    // 印刷アプリ初期動作(直印刷)
                    formReport.PrintInitAction = 1;
                    //直接印刷
                    formReport.PrintXPS();
                    // 印刷されてない場合、印刷フラグを更新
                    if (entity.HAISHA_SIJISHO_FLG == false)
                    {
                        if (this.form.HAISHA_JOKYO_CD.Text != "1")
                        {
                            // 配車状況が受注のとき以外の場合に更新
                            // 配車指示書フラグを更新（印刷された場合、更新を行う）
                            this.UpdPrintFlgForSK((long)entity.SYSTEM_ID, (int)entity.SEQ);
                        }
                    }
                }
                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 受付指示書(控え)印刷するか
        /// </summary>
        /// <returns>true:印刷 false:印刷しない</returns>
        internal bool IsHikaePrint()
        {
            bool retResult = false;
            try
            {
                LogUtility.DebugMethodStart();
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                M_SYS_INFO sysInfoEntity = new M_SYS_INFO();
                if (sysInfo != null)
                {
                    sysInfoEntity = sysInfo[0];
                }
                if (sysInfoEntity != null)
                {
                    // 1の場合
                    if (this.ChgDBNullToValue(sysInfoEntity.UKETSUKE_SIJISHO_SUB_PRINT_KBN, string.Empty).ToString().Equals("1"))
                    {
                        // trueを返す
                        retResult = true;
                    }
                }
                return retResult;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retResult);
            }
        }

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            try
            {
                if (obj is DBNull)
                {
                    return value;
                }
                else if (obj.GetType().Namespace.Equals("System.Data.SqlTypes"))
                {
                    INullable objChk = (INullable)obj;
                    if (objChk.IsNull)
                    {
                        return value;
                    }
                    else
                    {
                        return obj;
                    }
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 指示書印刷フラグを更新
        /// </summary>
        /// <param name="systemID">システムID</param>
        /// <param name="SEQ">枝番</param>
        [Transaction]
        private int UpdPrintFlgForSS(SqlInt64 systemID, int SEQ)
        {
            int returnVal = 0;
            try
            {
                LogUtility.DebugMethodStart(systemID, SEQ);
                using (Transaction tran = new Transaction())
                {
                    // データ更新
                    returnVal = this.daoUketsukeSSEntry.UpdatePrintFlg(systemID, SEQ);
                    // コミット
                    tran.Commit();
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

        /// <summary>
        /// 指示書印刷フラグを更新
        /// </summary>
        /// <param name="systemID">システムID</param>
        /// <param name="SEQ">枝番</param>
        [Transaction]
        private int UpdPrintFlgForSK(SqlInt64 systemID, int SEQ)
        {
            int returnVal = 0;
            try
            {
                LogUtility.DebugMethodStart(systemID, SEQ);
                using (Transaction tran = new Transaction())
                {
                    // データ更新
                    returnVal = this.daoUketsukeSKEntry.UpdatePrintFlg(systemID, SEQ);
                    // コミット
                    tran.Commit();
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

        #region 連携チェック
        internal bool RenkeiCheck(string uketsukeNum)
        {
            bool ret = true;
            try
            {
                if (string.IsNullOrEmpty(uketsukeNum))
                {
                    return true;
                }
                DataTable dt = this.mobisyoRtDao.GetRenkeiData("1", uketsukeNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E262", "現在回収中", "完了後、実績取込にて、売上/支払データを作成");
                    return false;
                }
                // ロジこんぱす連携済みであるかをチェックする。
                string selectStr;
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = 1"  // 1：収集受付
                    + " and LDD.REF_DENPYOU_NO = " + uketsukeNum
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";
                // データ取得
                dt = this.mDetailDao.getdateforstringsql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.errmessage.MessageBoxShow("E261", "ロジこんぱす連携中", "受入または売上/支払データを作成");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                ret = false;
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return true;
        }

        internal bool RenkeiCheckSyukka(string uketsukeNum)
        {
            bool ret = true;
            try
            {
                if (string.IsNullOrEmpty(uketsukeNum))
                {
                    return true;
                }
                DataTable dt = this.mobisyoRtDao.GetRenkeiData("1", uketsukeNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E262", "現在運搬中", "完了後、実績取込にて、売上/支払データを作成");
                    return false;
                }
                // ロジこんぱす連携済みであるかをチェックする。
                string selectStr;
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = 2"  // 2：出荷受付
                    + " and LDD.REF_DENPYOU_NO = " + uketsukeNum
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";
                // データ取得
                dt = this.mDetailDao.getdateforstringsql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.errmessage.MessageBoxShow("E261", "ロジこんぱす連携中", "出荷または売上/支払データを作成");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                ret = false;
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return true;
        }
        #endregion

        #region mapbox連携

        #region 抽出

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        private List<mapDtoList> createMapboxDto()
        {
            try
            {
                int layerId = 0;

                List<SummaryKeyCode> summaryKeyCodeList = new List<SummaryKeyCode>();

                // 出力対象となる受付テーブルのPK情報を取得
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].IsNewRow)
                    {
                        continue;
                    }

                    // チェックなしデータを排除する
                    if (this.form.customDataGridView1.Rows[i].Cells[ConstCls.DATA_TAISHO].Value == null) continue;
                    if ((bool)this.form.customDataGridView1.Rows[i].Cells[ConstCls.DATA_TAISHO].Value == false) continue;

                    SummaryKeyCode summaryKeyCode = new SummaryKeyCode();
                    summaryKeyCode.HIDDEN_DENPYOU_SHURUI_NAME = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[ConstCls.HIDDEN_DENPYOU_SHURUI_NAME].Value);
                    summaryKeyCode.HIDDEN_UKETSUKE_NUMBER = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value);
                    summaryKeyCodeList.Add(summaryKeyCode);
                }

                List<mapDtoList> dtoLists = new List<mapDtoList>();

                #region 収集データをLayer0として集計

                // レイヤー追加
                mapDtoList dtoList = new mapDtoList();
                layerId = 0;
                dtoList.layerId = layerId;

                List<mapDto> dtos = new List<mapDto>();

                // LINQでグループ化する
                var roopList = summaryKeyCodeList.GroupBy(a => new { HIDDEN_DENPYOU_SHURUI_NAME = a.HIDDEN_DENPYOU_SHURUI_NAME, HIDDEN_UKETSUKE_NUMBER = a.HIDDEN_UKETSUKE_NUMBER });
                foreach (var group in roopList)
                {
                    if (group.Key.HIDDEN_DENPYOU_SHURUI_NAME != "収集")
                    {
                        continue;
                    }

                    string sql = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT ");
                    sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", ConstCls.GYOUSHA_CD);
                    sb.AppendFormat(",ENT.GYOUSHA_NAME AS {0} ", ConstCls.GYOUSHA_NAME_RYAKU);  // マスタではなく伝票の業者名を抽出する
                    sb.AppendFormat(",GEN.GENBA_CD AS {0} ", ConstCls.GENBA_CD);
                    sb.AppendFormat(",ENT.GENBA_NAME AS {0} ", ConstCls.GENBA_NAME_RYAKU);      // マスタではなく伝票の現場名を抽出する
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN TDF.TODOUFUKEN_NAME ELSE TDF2.TODOUFUKEN_NAME  END AS {0} ", ConstCls.TODOUFUKEN_NAME);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_ADDRESS1  ELSE GYO.GYOUSHA_ADDRESS1  END AS {0} ", ConstCls.ADDRESS1);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_ADDRESS2  ELSE GYO.GYOUSHA_ADDRESS2  END AS {0} ", ConstCls.ADDRESS2);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_LATITUDE  ELSE GYO.GYOUSHA_LATITUDE  END AS {0} ", ConstCls.LATITUDE);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_LONGITUDE ELSE GYO.GYOUSHA_LONGITUDE END AS {0} ", ConstCls.LONGITUDE);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_POST      ELSE GYO.GYOUSHA_POST      END AS {0} ", ConstCls.POST);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_TEL       ELSE GYO.GYOUSHA_TEL       END AS {0} ", ConstCls.TEL);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.BIKOU1          ELSE GYO.BIKOU1            END AS {0} ", ConstCls.BIKOU1);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.BIKOU2          ELSE GYO.BIKOU2            END AS {0} ", ConstCls.BIKOU2);
                    sb.AppendFormat(",ENT.SAGYOU_DATE AS {0}", ConstCls.SAGYOU_DATE);
                    sb.AppendFormat(",ENT.GENCHAKU_TIME_NAME AS {0}", ConstCls.GENCHAKU_TIME_NAME);
                    sb.AppendFormat(",ENT.GENCHAKU_TIME AS {0}", ConstCls.GENCHAKU_TIME);
                    sb.AppendFormat(",ENT.SHASHU_NAME AS {0}", ConstCls.SHASHU_NAME);
                    sb.AppendFormat(",ENT.SHARYOU_NAME AS {0}", ConstCls.SHARYOU_NAME);
                    sb.AppendFormat(",ENT.UNTENSHA_NAME AS {0}", ConstCls.UNTENSHA_NAME);
                    sb.AppendFormat(",ENT.UNTENSHA_SIJIJIKOU1 AS {0}", ConstCls.UNTENSHA_SIJIJIKOU1);
                    sb.AppendFormat(",ENT.UNTENSHA_SIJIJIKOU2 AS {0}", ConstCls.UNTENSHA_SIJIJIKOU2);
                    sb.AppendFormat(",ENT.UNTENSHA_SIJIJIKOU3 AS {0}", ConstCls.UNTENSHA_SIJIJIKOU3);
                    sb.AppendFormat(",ENT.UKETSUKE_NUMBER");
                    sb.AppendFormat(",ENT.SYSTEM_ID");
                    sb.AppendFormat(",ENT.SEQ");
                    // 収集受付の場合
                    sb.AppendFormat(",'収集' AS DENSHU_KBN");
                    sb.AppendFormat(" FROM T_UKETSUKE_SS_ENTRY AS ENT ");
                    sb.AppendFormat(" LEFT JOIN M_GYOUSHA    GYO  ON ENT.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_GENBA      GEN  ON ENT.GYOUSHA_CD = GEN.GYOUSHA_CD AND ENT.GENBA_CD = GEN.GENBA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF  ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                    sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF2 ON GYO.GYOUSHA_TODOUFUKEN_CD = TDF2.TODOUFUKEN_CD ");
                    sb.AppendFormat(" WHERE ENT.DELETE_FLG = 0 ");
                    sb.AppendFormat(" AND   ENT.UKETSUKE_NUMBER = {0}", group.Key.HIDDEN_UKETSUKE_NUMBER);

                    DataTable dt = this.mDetailDao.getdateforstringsql(sb.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            // 緯度経度なしデータを排除する
                            if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][ConstCls.LATITUDE]))) continue;

                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = "収集受付";
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = string.Empty;
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = string.Empty;
                            dto.genbaName = Convert.ToString(dt.Rows[j][ConstCls.GENBA_NAME_RYAKU]);
                            dto.post = Convert.ToString(dt.Rows[j][ConstCls.POST]);
                            dto.address = Convert.ToString(dt.Rows[j][ConstCls.TODOUFUKEN_NAME]) + Convert.ToString(dt.Rows[j][ConstCls.ADDRESS1]) + Convert.ToString(dt.Rows[j][ConstCls.ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][ConstCls.TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU2]);
                            dto.latitude = Convert.ToString(dt.Rows[j][ConstCls.LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][ConstCls.LONGITUDE]);
                            string sagyouDate = string.Empty;
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][ConstCls.SAGYOU_DATE])))
                                sagyouDate = Convert.ToDateTime(Convert.ToString(dt.Rows[j][ConstCls.SAGYOU_DATE])).ToString("yyyy/MM/dd(ddd)");
                            dto.sagyouDate = sagyouDate;
                            string time = Convert.ToString(dt.Rows[j][ConstCls.GENCHAKU_TIME_NAME]);
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][ConstCls.GENCHAKU_TIME])))
                                time += Convert.ToDateTime(Convert.ToString(dt.Rows[j][ConstCls.GENCHAKU_TIME])).ToString("HH:mm");
                            dto.genbaChaku = time;
                            dto.shasyu = Convert.ToString(dt.Rows[j][ConstCls.SHASHU_NAME]);
                            dto.sharyou = Convert.ToString(dt.Rows[j][ConstCls.SHARYOU_NAME]);
                            dto.driver = Convert.ToString(dt.Rows[j][ConstCls.UNTENSHA_NAME]);
                            dto.sijijikou1 = Convert.ToString(dt.Rows[j][ConstCls.UNTENSHA_SIJIJIKOU1]);
                            dto.sijijikou2 = Convert.ToString(dt.Rows[j][ConstCls.UNTENSHA_SIJIJIKOU2]);
                            dto.sijijikou3 = Convert.ToString(dt.Rows[j][ConstCls.UNTENSHA_SIJIJIKOU3]);
                            dto.teikiHaishaNo = group.Key.HIDDEN_UKETSUKE_NUMBER;

                            sql = " SELECT H.HINMEI_NAME_RYAKU, SS.SUURYOU, U.UNIT_NAME_RYAKU FROM T_UKETSUKE_SS_DETAIL SS "
                                + " LEFT JOIN M_HINMEI H ON SS.HINMEI_CD = H.HINMEI_CD "
                                + " LEFT JOIN M_UNIT U ON SS.UNIT_CD = U.UNIT_CD"
                                + " WHERE SYSTEM_ID = " + Convert.ToInt64(dt.Rows[j]["SYSTEM_ID"])
                                + "   AND SEQ = " + Convert.ToInt32(dt.Rows[j]["SEQ"]);
                            DataTable hinmeiDt = this.sysInfoDao.GetDateForStringSql(sql);
                            string hinmei = string.Empty;
                            foreach (DataRow dr in hinmeiDt.Rows)
                            {
                                string suuryou = string.Empty;
                                if (!string.IsNullOrEmpty(this.ChgDBNullToValue(dr["SUURYOU"], string.Empty).ToString()))
                                {
                                    suuryou = Convert.ToDecimal(dr["SUURYOU"]).ToString(this.sysInfoEntity.SYS_SUURYOU_FORMAT);
                                }

                                if (string.IsNullOrEmpty(hinmei))
                                {
                                    hinmei += dr["HINMEI_NAME_RYAKU"] + " " + suuryou + dr["UNIT_NAME_RYAKU"];
                                }
                                else
                                {
                                    hinmei += "/" + dr["HINMEI_NAME_RYAKU"] + " " + suuryou + dr["UNIT_NAME_RYAKU"];
                                }
                            }
                            dto.hinmei = hinmei;

                            dtos.Add(dto);
                        }
                        // 1コース終わったらリストにセット
                        dtoList.dtos = dtos;
                    }
                }

                if (dtoList.dtos != null)
                {
                    if (dtoList.dtos.Count != 0)
                    {
                        dtoLists.Add(dtoList);
                    }
                }

                #endregion

                #region 出荷データをLayer0として集計

                // レイヤー追加
                dtoList = new mapDtoList();
                layerId = 1;
                dtoList.layerId = layerId;

                dtos = new List<mapDto>();

                // LINQでグループ化する
                roopList = summaryKeyCodeList.GroupBy(a => new { HIDDEN_DENPYOU_SHURUI_NAME = a.HIDDEN_DENPYOU_SHURUI_NAME, HIDDEN_UKETSUKE_NUMBER = a.HIDDEN_UKETSUKE_NUMBER });
                foreach (var group in roopList)
                {
                    if (group.Key.HIDDEN_DENPYOU_SHURUI_NAME == "収集")
                    {
                        continue;
                    }

                    string sql = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT ");
                    sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", ConstCls.GYOUSHA_CD);
                    sb.AppendFormat(",ENT.GYOUSHA_NAME AS {0} ", ConstCls.GYOUSHA_NAME_RYAKU);  // マスタではなく伝票の業者名を抽出する
                    sb.AppendFormat(",GEN.GENBA_CD AS {0} ", ConstCls.GENBA_CD);
                    sb.AppendFormat(",ENT.GENBA_NAME AS {0} ", ConstCls.GENBA_NAME_RYAKU);      // マスタではなく伝票の現場名を抽出する
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN TDF.TODOUFUKEN_NAME ELSE TDF2.TODOUFUKEN_NAME  END AS {0} ", ConstCls.TODOUFUKEN_NAME);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_ADDRESS1  ELSE GYO.GYOUSHA_ADDRESS1  END AS {0} ", ConstCls.ADDRESS1);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_ADDRESS2  ELSE GYO.GYOUSHA_ADDRESS2  END AS {0} ", ConstCls.ADDRESS2);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_LATITUDE  ELSE GYO.GYOUSHA_LATITUDE  END AS {0} ", ConstCls.LATITUDE);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_LONGITUDE ELSE GYO.GYOUSHA_LONGITUDE END AS {0} ", ConstCls.LONGITUDE);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_POST      ELSE GYO.GYOUSHA_POST      END AS {0} ", ConstCls.POST);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_TEL       ELSE GYO.GYOUSHA_TEL       END AS {0} ", ConstCls.TEL);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.BIKOU1          ELSE GYO.BIKOU1            END AS {0} ", ConstCls.BIKOU1);
                    sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.BIKOU2          ELSE GYO.BIKOU2            END AS {0} ", ConstCls.BIKOU2);
                    sb.AppendFormat(",ENT.SAGYOU_DATE AS {0}", ConstCls.SAGYOU_DATE);
                    sb.AppendFormat(",ENT.GENCHAKU_TIME_NAME AS {0}", ConstCls.GENCHAKU_TIME_NAME);
                    sb.AppendFormat(",ENT.GENCHAKU_TIME AS {0}", ConstCls.GENCHAKU_TIME);
                    sb.AppendFormat(",ENT.SHASHU_NAME AS {0}", ConstCls.SHASHU_NAME);
                    sb.AppendFormat(",ENT.SHARYOU_NAME AS {0}", ConstCls.SHARYOU_NAME);
                    sb.AppendFormat(",ENT.UNTENSHA_NAME AS {0}", ConstCls.UNTENSHA_NAME);
                    sb.AppendFormat(",ENT.UNTENSHA_SIJIJIKOU1 AS {0}", ConstCls.UNTENSHA_SIJIJIKOU1);
                    sb.AppendFormat(",ENT.UNTENSHA_SIJIJIKOU2 AS {0}", ConstCls.UNTENSHA_SIJIJIKOU2);
                    sb.AppendFormat(",ENT.UNTENSHA_SIJIJIKOU3 AS {0}", ConstCls.UNTENSHA_SIJIJIKOU3);
                    sb.AppendFormat(",ENT.UKETSUKE_NUMBER");
                    sb.AppendFormat(",ENT.SYSTEM_ID");
                    sb.AppendFormat(",ENT.SEQ");
                    // 出荷受付の場合
                    sb.AppendFormat(",'出荷' AS DENSHU_KBN");
                    sb.AppendFormat(" FROM T_UKETSUKE_SK_ENTRY AS ENT ");
                    sb.AppendFormat(" LEFT JOIN M_GYOUSHA    GYO  ON ENT.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_GENBA      GEN  ON ENT.GYOUSHA_CD = GEN.GYOUSHA_CD AND ENT.GENBA_CD = GEN.GENBA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF  ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                    sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF2 ON GYO.GYOUSHA_TODOUFUKEN_CD = TDF2.TODOUFUKEN_CD ");
                    sb.AppendFormat(" WHERE ENT.DELETE_FLG = 0 ");
                    sb.AppendFormat(" AND   ENT.UKETSUKE_NUMBER = {0}", group.Key.HIDDEN_UKETSUKE_NUMBER);

                    DataTable dt = this.mDetailDao.getdateforstringsql(sb.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            // 緯度経度なしデータを排除する
                            if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][ConstCls.LATITUDE]))) continue;

                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = "出荷受付";
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = string.Empty;
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = string.Empty;
                            dto.genbaName = Convert.ToString(dt.Rows[j][ConstCls.GENBA_NAME_RYAKU]);
                            dto.post = Convert.ToString(dt.Rows[j][ConstCls.POST]);
                            dto.address = Convert.ToString(dt.Rows[j][ConstCls.TODOUFUKEN_NAME]) + Convert.ToString(dt.Rows[j][ConstCls.ADDRESS1]) + Convert.ToString(dt.Rows[j][ConstCls.ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][ConstCls.TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU2]);
                            dto.latitude = Convert.ToString(dt.Rows[j][ConstCls.LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][ConstCls.LONGITUDE]);
                            string sagyouDate = string.Empty;
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][ConstCls.SAGYOU_DATE])))
                                sagyouDate = Convert.ToDateTime(Convert.ToString(dt.Rows[j][ConstCls.SAGYOU_DATE])).ToString("yyyy/MM/dd(ddd)");
                            dto.sagyouDate = sagyouDate;
                            string time = Convert.ToString(dt.Rows[j][ConstCls.GENCHAKU_TIME_NAME]);
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][ConstCls.GENCHAKU_TIME])))
                                time += Convert.ToDateTime(Convert.ToString(dt.Rows[j][ConstCls.GENCHAKU_TIME])).ToString("HH:mm");
                            dto.genbaChaku = time;
                            dto.shasyu = Convert.ToString(dt.Rows[j][ConstCls.SHASHU_NAME]);
                            dto.sharyou = Convert.ToString(dt.Rows[j][ConstCls.SHARYOU_NAME]);
                            dto.driver = Convert.ToString(dt.Rows[j][ConstCls.UNTENSHA_NAME]);
                            dto.sijijikou1 = Convert.ToString(dt.Rows[j][ConstCls.UNTENSHA_SIJIJIKOU1]);
                            dto.sijijikou2 = Convert.ToString(dt.Rows[j][ConstCls.UNTENSHA_SIJIJIKOU2]);
                            dto.sijijikou3 = Convert.ToString(dt.Rows[j][ConstCls.UNTENSHA_SIJIJIKOU3]);
                            dto.teikiHaishaNo = group.Key.HIDDEN_UKETSUKE_NUMBER;

                            sql = " SELECT H.HINMEI_NAME_RYAKU, SK.SUURYOU, U.UNIT_NAME_RYAKU FROM T_UKETSUKE_SK_DETAIL SK "
                                + " LEFT JOIN M_HINMEI H ON SK.HINMEI_CD = H.HINMEI_CD "
                                + " LEFT JOIN M_UNIT U ON SK.UNIT_CD = U.UNIT_CD"
                                + " WHERE SYSTEM_ID = " + Convert.ToInt64(dt.Rows[j]["SYSTEM_ID"])
                                + "   AND SEQ = " + Convert.ToInt32(dt.Rows[j]["SEQ"]);
                            DataTable hinmeiDt = this.sysInfoDao.GetDateForStringSql(sql);
                            string hinmei = string.Empty;
                            foreach (DataRow dr in hinmeiDt.Rows)
                            {
                                string suuryou = string.Empty;
                                if (!string.IsNullOrEmpty(this.ChgDBNullToValue(dr["SUURYOU"], string.Empty).ToString()))
                                {
                                    suuryou = Convert.ToDecimal(dr["SUURYOU"]).ToString(this.sysInfoEntity.SYS_SUURYOU_FORMAT);
                                }

                                if (string.IsNullOrEmpty(hinmei))
                                {
                                    hinmei += dr["HINMEI_NAME_RYAKU"] + " " + suuryou + dr["UNIT_NAME_RYAKU"];
                                }
                                else
                                {
                                    hinmei += "/" + dr["HINMEI_NAME_RYAKU"] + " " + suuryou + dr["UNIT_NAME_RYAKU"];
                                }
                            }
                            dto.hinmei = hinmei;

                            dtos.Add(dto);
                        }
                        // 1コース終わったらリストにセット
                        dtoList.dtos = dtos;
                    }
                }

                if (dtoList.dtos != null)
                {
                    if (dtoList.dtos.Count != 0)
                    {
                        dtoLists.Add(dtoList);
                    }
                }

                #endregion

                return dtoLists;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createMapboxDto", ex);
                this.errmessage.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        #endregion

        #region 地図表示件数チェック

        /// <summary>
        /// 一覧で選択がチェックされているか確認する。
        /// また、チェックされた行で地図表示できるデータがない場合もアラートを出す。
        /// </summary>
        /// <returns></returns>
        internal bool CheckForCheckBox()
        {
            bool ret = false;

            // チェックが1件もない場合のチェック
            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                // 選択のチェックボックスの値を取得する。
                if (row.Cells[ConstCls.DATA_TAISHO].Value != null)
                {
                    ret = bool.Parse(Convert.ToString(row.Cells[ConstCls.DATA_TAISHO].Value));
                    if (ret)
                    {
                        break;
                    }
                }
            }
            if (!ret)
            {
                this.errmessage.MessageBoxShowError("地図表示対象の明細がありません。");
                return ret;
            }

            bool latlon = false;
            // チェックしたデータに緯度経度があるかチェック
            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                // 選択のチェックボックスの値を取得する。
                if (row.Cells[ConstCls.DATA_TAISHO].Value != null)
                {
                    if ((bool)row.Cells[ConstCls.DATA_TAISHO].Value)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.HIDDEN_GENBA_LATITUDE].Value)))
                        {
                            latlon = true;
                        }
                    }
                }
            }
            if (!latlon)
            {
                this.errmessage.MessageBoxShowError("緯度経度が登録されているデータがないため、地図を表示できません。");
                ret = false;
            }

            return ret;
        }

        #endregion

        #region 明細ヘッダーにチェックボックスを追加

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        internal void HeaderCheckBoxSupport2()
        {

            LogUtility.DebugMethodStart();

            string denpyouSyurui = this.form.txtNum_DenPyouSyurui.Text;
            //[伝票種類]＝「1」（収集）、「2」（出荷）、「6」（収集＋出荷）の場合、チェックできる
            if (denpyouSyurui.Equals(ConstCls.DENPYOU_SHURUI_CD_SYUSYU)
                || denpyouSyurui.Equals(ConstCls.DENPYOU_SHURUI_CD_SYUKKA)
                || denpyouSyurui.Equals(ConstCls.DENPYOU_SHURUI_CD_SS_SK))
            {

                if (!this.form.customDataGridView1.Columns.Contains(ConstCls.DATA_TAISHO))
                {
                    {
                        DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                        newColumn.Name = ConstCls.DATA_TAISHO;
                        newColumn.HeaderText = "地図";
                        newColumn.DataPropertyName = "TAISHO";
                        newColumn.Width = 70;
                        DataGridViewCheckBoxHeaderCell2 newheader = new DataGridViewCheckBoxHeaderCell2(0, true);
                        newheader.Value = "地図   ";
                        newColumn.HeaderCell = newheader;
                        newColumn.Resizable = DataGridViewTriState.False;
                        newColumn.ReadOnly = false;

                        if (this.form.customDataGridView1.Columns.Count > 0)
                        {
                            this.form.customDataGridView1.Columns.Insert(0, newColumn);
                        }
                        else
                        {
                            this.form.customDataGridView1.Columns.Add(newColumn);
                        }
                        this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                    }
                }
            }
            else
            {
                if (this.form.customDataGridView1.Columns.Contains(ConstCls.DATA_TAISHO))
                {
                    this.form.customDataGridView1.Columns.Remove(ConstCls.DATA_TAISHO);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 明細ヘッダーのチェックボックス解除

        /// <summary>
        /// 明細ヘッダーチェックボックスを解除する
        /// </summary>
        internal void HeaderCheckBoxFalse()
        {
            if (this.form.customDataGridView1.Columns.Contains(ConstCls.DATA_TAISHO))
            {
                DataGridViewCheckBoxHeaderCell2 header = this.form.customDataGridView1.Columns[ConstCls.DATA_TAISHO].HeaderCell as DataGridViewCheckBoxHeaderCell2;
                if (header != null)
                {
                    header._checked = false;
                }
            }
        }

        #endregion

        #region 明細データクリックイベント

        /// <summary>
        /// 明細データクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0 || e.ColumnIndex != 1)
                {
                    return;
                }

                //スペースで、OFFの場合は抜ける
                if (this.SpaceChk && !this.SpaceON)
                {
                    return;
                }
                this.SpaceON = false;

                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (this.form.customDataGridView1.CurrentCell.Value == null)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.HIDDEN_GENBA_LATITUDE].Value)))
                    {
                        this.errmessage.MessageBoxShowError("緯度経度が登録されているデータがないため、地図を表示できません。");
                        if (!this.SpaceChk)
                        {
                            row.Cells[ConstCls.DATA_TAISHO].Value = true;
                        }
                        this.SpaceChk = false;
                        return;
                    }
                    if (this.SpaceChk)
                    {
                        if (this.form.customDataGridView1[1, e.RowIndex].Value == null)
                        {
                            this.form.customDataGridView1[1, e.RowIndex].Value = true;
                        }
                        else
                        {
                            this.form.customDataGridView1[1, e.RowIndex].Value = !(bool)this.form.customDataGridView1[1, e.RowIndex].Value;
                        }
                        this.SpaceChk = false;
                    }
                }
                else if (this.form.customDataGridView1.CurrentCell.Value.Equals(false))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.HIDDEN_GENBA_LATITUDE].Value)))
                    {
                        this.errmessage.MessageBoxShowError("緯度経度が登録されているデータがないため、地図を表示できません。");
                        if (!this.SpaceChk)
                        {
                            row.Cells[ConstCls.DATA_TAISHO].Value = true;
                        }
                        this.SpaceChk = false;
                        return;
                    }
                    if (this.SpaceChk)
                    {
                        if (this.form.customDataGridView1[1, e.RowIndex].Value == null)
                        {
                            this.form.customDataGridView1[1, e.RowIndex].Value = true;
                        }
                        else
                        {
                            this.form.customDataGridView1[1, e.RowIndex].Value = !(bool)this.form.customDataGridView1[1, e.RowIndex].Value;
                        }
                        this.SpaceChk = false;
                    }
                }

                this.form.customDataGridView1.RefreshEdit();
                this.form.customDataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                LogUtility.Error("customDataGridView1_CellClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 明細チェックボックスのスペースキー押下時の制御

        /// <summary>
        /// [地図]で、スペースキーでチェック処理が走るように下準備
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DataGridViewCell curCell = this.form.customDataGridView1.CurrentCell;

                if (curCell.RowIndex < 0 || curCell.ColumnIndex != 1)
                {
                    return;
                }

                this.SpaceChk = true;
                this.SpaceON = false;
                //[地図]OFFにする場合は、何もしない。
                //[地図]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                if (this.form.customDataGridView1[1, curCell.RowIndex].Value == null)
                {
                    this.SpaceON = true;
                    this.form.customDataGridView1[1, curCell.RowIndex].Value = true;
                }
                else
                {
                    if (!(bool)this.form.customDataGridView1[1, curCell.RowIndex].Value)
                    {
                        this.SpaceON = true;
                        this.form.customDataGridView1[1, curCell.RowIndex].Value = !(bool)this.form.customDataGridView1[1, curCell.RowIndex].Value;
                    }
                }
                this.form.customDataGridView1.Refresh();
            }
        }

        #endregion

        /// <summary>
        /// 地図表示のチェックボックスを使用可能にする
        /// </summary>
        internal void notReadOnlyColumns()
        {
            foreach (DataGridViewColumn col in this.form.customDataGridView1.Columns)
            {
                // 現状「地図表示」のチェックのみ
                if (col.Name == ConstCls.DATA_TAISHO)
                {
                    col.ReadOnly = false;
                }
            }
        }

        #endregion

        #region ｼｮｰﾄﾒｯｾｰｼﾞ
        private void Bt_SmsInit()
        {
            this.bt_sms = new r_framework.CustomControl.CustomButton();

            this.bt_sms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_sms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_sms.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_sms.Enabled = false;
            this.bt_sms.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_sms.Location = new System.Drawing.Point(1027, 498);
            this.bt_sms.Name = "bt_sms";
            this.bt_sms.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_sms.Size = new System.Drawing.Size(150, 30);
            this.bt_sms.TabIndex = 0;
            this.bt_sms.Tag = "";
            this.bt_sms.Text = "ｼｮｰﾄﾒｯｾｰｼﾞ";
            this.bt_sms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_sms.UseVisualStyleBackColor = false;

            this.bt_sms.Click += new EventHandler(bt_sms_Click); 
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞボタンをクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void bt_sms_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す伝票情報
                string[] smsparamList = new string[7];
                // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す受信者リスト
                List<int> smsReceiverList = new List<int>();

                DataTable getDataDt = new DataTable();

                var row = this.form.GetSelectedRow();
                if (null != row)
                {
                    // SEQ、受付番号は先に設定
                    smsparamList[1] = Convert.ToString(row.Cells[ConstCls.HIDDEN_SEQ].Value);
                    smsparamList[2] = Convert.ToString(row.Cells[ConstCls.HIDDEN_UKETSUKE_NUMBER].Value);

                    var denshu = this.form.GetDenpyouShuruiCd();
                    if (denshu == ConstCls.DENPYOU_SHURUI_CD_SYUSYU || 
                        denshu == ConstCls.DENPYOU_SHURUI_CD_SYUKKA || 
                        denshu == ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI)
                    {
                        // 伝票種類が収集、出荷、持込のいずれかである場合
                        smsparamList[0] = denshu;
                    }
                    else
                    {
                        // 伝票種類が5．収集＋出荷もしくは6．収集＋持込である場合、収集チェック
                        string shushuChkSql = string.Format("SELECT * FROM T_UKETSUKE_SS_ENTRY WHERE UKETSUKE_NUMBER = '{0}'", smsparamList[2]);

                        DataTable chkdDt = this.daoUketsukeSSEntry.GetDateForStringSql(shushuChkSql);
                        if (chkdDt.Rows.Count > 0)
                        {
                            // 収集受付にデータが存在する場合は「収集」
                            smsparamList[0] = ConstCls.DENPYOU_SHURUI_CD_SYUSYU;
                        }
                        else
                        {
                            if (denshu == ConstCls.DENPYOU_SHURUI_CD_SS_SK)
                            {
                                // 収集受付にデータが存在せず
                                // 画面上の伝票種類が5．収集＋出荷の場合は「出荷」
                                smsparamList[0] = ConstCls.DENPYOU_SHURUI_CD_SYUKKA;
                            }
                            else
                            {
                                // 収集受付にデータが存在せず
                                // 画面上の伝票種類が6．収集＋持込の場合は「持込」
                                smsparamList[0] = ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI;
                            }
                        }
                    }

                    // 伝票種類を確認
                    string tableName = string.Empty;
                    if (smsparamList[0] == ConstCls.DENPYOU_SHURUI_CD_SYUSYU)
                    {
                        tableName = ConstCls.SUMMARY_SYUSYU;
                    }
                    else if (smsparamList[0] == ConstCls.DENPYOU_SHURUI_CD_SYUKKA)
                    {
                        tableName = ConstCls.SUMMARY_SYUKKA;
                    }
                    else if (smsparamList[0] == ConstCls.DENPYOU_SHURUI_CD_MOCHIKOMI)
                    {
                        tableName = ConstCls.SUMMARY_MOCHIKOMI;
                    }

                    // SEQ、受付番号を元に対象の伝票の情報取得
                    string getDataSql = string.Format("SELECT * FROM {0}_ENTRY WHERE SEQ = '{1}' AND UKETSUKE_NUMBER = '{2}'", tableName, smsparamList[1], smsparamList[2]);
                    getDataDt = this.daoUketsukeSSEntry.GetDateForStringSql(getDataSql);
                    if (getDataDt.Rows.Count == 0)
                    {
                        MessageBoxUtility.MessageBoxShow("E045");
                        return;
                    }

                    // ｼｮｰﾄﾒｯｾｰｼﾞ受信者チェック
                    var dao = this.smsReceiverLinkGenbaDao.CheckDataForSmsNyuuryoku(getDataDt.Rows[0]["GYOUSHA_CD"].ToString(), getDataDt.Rows[0]["GENBA_CD"].ToString());
                    if(dao == null)
                    {
                        MessageBoxUtility.MessageBoxShowError("現場入力（マスタ）に受信者情報が登録されていません。\r\n受信者情報を登録してください。");
                        return;
                    }

                    smsparamList[3] = getDataDt.Rows[0]["GYOUSHA_CD"].ToString();
                    smsparamList[4] = getDataDt.Rows[0]["GENBA_CD"].ToString();
                    smsparamList[5] = ((DateTime)getDataDt.Rows[0]["SAGYOU_DATE"]).ToString("yyyy/MM/dd(ddd)");
                
                    smsReceiverList = this.SmsReceiverListSetting(smsparamList[3], smsparamList[4]);

                    smsparamList[6] = null;

                    // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面を起動
                    FormManager.OpenForm("G767", smsReceiverList, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_ID.T_UKETSUKE_SHUSHU, smsparamList);
                }
                else
                {
                    MessageBoxUtility.MessageBoxShow("E076");
                }
            }
            catch(Exception ex)
            {
                LogUtility.Error("bt_sms_Click", ex);
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト取得
        /// </summary>
        /// <returns></returns>
        private List<int> SmsReceiverListSetting(string gyoushaCd, string genbaCd)
        {
            List<int> list = null;
            List<M_SMS_RECEIVER_LINK_GENBA> smsReceiverLink = null;

            // 選択行の値を参照
            smsReceiverLink = this.smsReceiverLinkGenbaDao.GetDataForSmsNyuuryoku(gyoushaCd, genbaCd);
            
            if (smsReceiverLink != null)
            {
                list = smsReceiverLink.Select(n => n.SYSTEM_ID.Value).ToList();
            }

            return list;
        }
        
        #endregion
    }
}