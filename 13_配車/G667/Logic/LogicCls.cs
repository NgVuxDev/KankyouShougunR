// $Id: LogicCls.cs 54491 2015-07-03 03:56:01Z quocthang@e-mall.co.jp $
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Data.SqlTypes;
using Seasar.Framework.Exceptions;
using Seasar.Dao;
using System.Windows.Forms;
using Seasar.Quill.Attrs;
using System.Collections.Generic;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using System.Text;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.GeoCodingAPI;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;

using MapboxConfidentialClassLibrary;
using GrapeCity.Win.MultiRow;

namespace Shougun.Core.Allocation.MobileJoukyouIchiran
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.MobileJoukyouIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// G283専用DBアクセッサー
        /// </summary>
        public Shougun.Core.Allocation.MobileShougunTorikomi.Accessor.DBAccessor accessor;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// システム設定． 数量フォーマット
        /// </summary>
        private String systemSuuryouFormatCD;

        /// <summary>
        /// システム設定の売上支払情報確定フラグ
        /// </summary>
        private SqlInt16 ursh_kakutei_flg;

        /// <summary>共通</summary>
        ManifestoLogic mlogic = null;

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 画面上に表示するメッセージボックス
        /// </summary>
        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// モバイル状況一覧DTO
        /// </summary>
        internal DTOClass dto = new DTOClass();

        /// <summary>
        /// システムID採番Dao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// 業者マスタDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場マスタDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 取引先マスタDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// 取引先請求情報のDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao torihikiSeikyuDao;
        /// <summary>
        /// 取引先支払情報のDao
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao torihikiShiharaiDao;

        /// <summary>
        /// 車輌マスタのDao
        /// </summary>
        private IM_SHARYOUDao sharyouDao;
        /// <summary>
        /// 車種マスタのDao
        /// </summary>
        private IM_SHASHUDao shashuDao;
        /// <summary>
        /// 社員マスタのDao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// 一覧検索用のDao
        /// </summary>
        private ITeikihaishaDao TeikihaishaDao;

        /// <summary>
        /// モバイル将軍業務詳細TBLのDao
        /// </summary>
        private T_MOBISYO_RTDao mTeikihaishaDao;

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
        /// 定期配車実績entryのDao
        /// </summary>
        private IT_TEIKI_JISSEKI_ENTRYDao TtjeDao;

        /// <summary>
        /// 定期配車実績detailのDao
        /// </summary>
        private IT_TEIKI_JISSEKI_DETAILDao TtjdDao;

        /// <summary>
        /// 定期配車実績niorosiのDao
        /// </summary>
        private IT_TEIKI_JISSEKI_NIOROSHIDao TtjnDao;

        /// <summary>
        /// 売上支払entryのDao
        /// </summary>
        private IT_UR_SH_ENTRYDao UrsheDao;

        /// <summary>
        /// 売上支払detailのDao
        /// </summary>
        private IT_UR_SH_DETAILDao UrshdDao;

        /// <summary>
        /// 伝種採番Dao
        /// </summary>
        private IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// 会社情報エンティティ
        /// </summary>
        private M_CORP_INFO mCorpInfo;

        /// <summary>
        /// モバイル将軍業務TBLのentity
        /// </summary>
        private T_MOBISYO_RT entitysMobisyoRt { get; set; }

        /// <summary>
        /// モバイル将軍業務コンテナTBLのentity
        /// </summary>
        private T_MOBISYO_RT_CONTENA entitysMobisyoRtCTN { get; set; }

        /// <summary>
        /// モバイル将軍業務詳細TBLのentity
        /// </summary>
        private T_MOBISYO_RT_DTL entitysMobisyoRtDTL { get; set; }

        /// <summary>
        /// モバイル将軍業務搬入TBLのentity
        /// </summary>
        private T_MOBISYO_RT_HANNYUU entitysMobisyoRtHN { get; set; }

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

        /// <summary>
        /// 売上支払entryのentityList
        /// </summary>
        private List<T_UR_SH_ENTRY> entitysUrShEntryList { get; set; }

        /// <summary>
        /// 売上支払detailのentityList
        /// </summary>
        private List<T_UR_SH_DETAIL> entitysUrShDetailList { get; set; }

        /// <summary>
        /// 収集受付入力entryのentity
        /// </summary>
        private IT_UKETSUKE_SS_ENTRYDao ssEntryDao { get; set; }

        /// <summary>
        /// 収集受付入力detailのentity
        /// </summary>
        private IT_UKETSUKE_SS_DETAILDao ssDetailDao { get; set; }

        /// <summary>
        /// 収集受付コンテナのentity
        /// </summary>
        private IT_CONTENA_RESERVEDao contenaReseveDao { get; set; }

        /// <summary>
        /// 売上支払コンテナのentity
        /// </summary>
        private IT_CONTENA_RESULTDao contenaResultDao { get; set; }

        /// <summary>
        /// コンテナマスタのentity
        /// </summary>
        private IM_CONTENADao contenaDao { get; set; }

        /// <summary>
        /// 収集受付entityのentityList
        /// </summary>
        private List<T_UKETSUKE_SS_ENTRY> delSSEntryList { get; set; }

        /// <summary>
        /// 収集受付entityのentityList
        /// </summary>
        private List<T_UKETSUKE_SS_ENTRY> intSSEntryList { get; set; }

        /// <summary>
        /// 売上支払detailのentityList
        /// </summary>
        private List<T_UKETSUKE_SS_DETAIL> delSSDetailList { get; set; }

        /// <summary>
        /// 収集受付detailのentityList
        /// </summary>
        private List<T_UKETSUKE_SS_DETAIL> intSSDetailList { get; set; }

        /// <summary>
        /// 収集受付コンテナのentityList
        /// </summary>
        private List<T_CONTENA_RESERVE> delContenaReserveList { get; set; }

        /// <summary>
        /// 収集受付コンテナのentityList
        /// </summary>
        private List<T_CONTENA_RESERVE> intContenaReserveList { get; set; }

        /// <summary>
        /// 売上支払コンテナのentityList
        /// </summary>
        private List<T_CONTENA_RESULT> intContenaResultList { get; set; }

        // 20170612 wangjm モバイル将軍#105481 start

        /// <summary>
        /// 出荷受付入力entryのentity
        /// </summary>
        private IT_UKETSUKE_SK_ENTRYDao skEntryDao { get; set; }

        /// <summary>
        /// 出荷受付入力detailのentity
        /// </summary>
        private IT_UKETSUKE_SK_DETAILDao skDetailDao { get; set; }

        /// <summary>
        /// 出荷受付entityのentityList
        /// </summary>
        private List<T_UKETSUKE_SK_ENTRY> delSKEntryList { get; set; }

        /// <summary>
        /// 出荷受付entityのentityList
        /// </summary>
        private List<T_UKETSUKE_SK_ENTRY> intSKEntryList { get; set; }

        /// <summary>
        /// 出荷受付detailのentityList
        /// </summary>
        private List<T_UKETSUKE_SK_DETAIL> intSKDetailList { get; set; }
        // 20170612 wangjm モバイル将軍#105481 end

        /// <summary>
        /// コンテナのentityList
        /// </summary>
        private List<M_CONTENA> upContenaist { get; set; }

        /// <summary>
        /// 稼働状況のentity
        /// </summary>
        private List<T_MOBISYO_RT_KADOUJYOUKYOU> kadoujyoukyouList { get; set; }

        /// <summary>
        /// 稼働状況Dao
        /// </summary>
        private IT_MOBISYO_RT_KADOUJYOUKYOUDao kadoujyoukyouDao { get; set; }

        #endregion

        #region プロパティ
        /// <summary>
        /// 車輌CD前回値
        /// </summary>
        internal string oldSharyouCD;

        /// <summary>
        /// 明細Datatable
        /// </summary>
        private DataTable ResultTable;

        // 20170601 wangjm モバイル将軍#105481 start
        private String uketsukeKbn;
        // 20170601 wangjm モバイル将軍#105481 end

        /// <summary>
        /// モバイルに未登録のデータがあるか判断
        /// </summary>
        private bool MobileMitorokiFlg = false;

        /// <summary>
        /// 未回収の実績確定データがあるか判断
        /// </summary>
        private bool kakuteiForMikaishu = false;

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
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
                this.form = targetForm;
                this.oldSharyouCD = string.Empty;
                this.mTeikihaishaDao = DaoInitUtility.GetComponent<T_MOBISYO_RTDao>();
                this.mTmobisyoRtCTNDao = DaoInitUtility.GetComponent<T_MOBISYO_RT_CONTENADao>();
                this.mTmobisyoRtDTLDao = DaoInitUtility.GetComponent<T_MOBISYO_RT_DTLDao>();
                this.mTmobisyoRtHNDao = DaoInitUtility.GetComponent<T_MOBISYO_RT_HANNYUUDao>();
                this.TeikihaishaDao = DaoInitUtility.GetComponent<ITeikihaishaDao>();
                this.TmobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
                this.TmobisyoCTNDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_CONTENADao>();
                this.TmobisyoRtDTLDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_DTLDao>();
                this.TmobisyoRtHNDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_HANNYUUDao>();
                this.TtjeDao = DaoInitUtility.GetComponent<IT_TEIKI_JISSEKI_ENTRYDao>();
                this.TtjdDao = DaoInitUtility.GetComponent<IT_TEIKI_JISSEKI_DETAILDao>();
                this.TtjnDao = DaoInitUtility.GetComponent<IT_TEIKI_JISSEKI_NIOROSHIDao>();
                this.UrsheDao = DaoInitUtility.GetComponent<IT_UR_SH_ENTRYDao>();
                this.UrshdDao = DaoInitUtility.GetComponent<IT_UR_SH_DETAILDao>();
                this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
                this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
                this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
                this.torihikiSeikyuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                this.torihikiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
                this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();
                this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();
                this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
                this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
                this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
                this.MsgBox = new MessageBoxShowLogic();
                var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                this.mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();
                this.accessor = new Shougun.Core.Allocation.MobileShougunTorikomi.Accessor.DBAccessor();
                this.ssEntryDao = DaoInitUtility.GetComponent<IT_UKETSUKE_SS_ENTRYDao>();
                this.ssDetailDao = DaoInitUtility.GetComponent<IT_UKETSUKE_SS_DETAILDao>();
                this.contenaReseveDao = DaoInitUtility.GetComponent<IT_CONTENA_RESERVEDao>();
                this.contenaResultDao = DaoInitUtility.GetComponent<IT_CONTENA_RESULTDao>();
                this.contenaDao = DaoInitUtility.GetComponent<IM_CONTENADao>();
                // 20170601 wangjm モバイル将軍#105481 start
                this.skEntryDao = DaoInitUtility.GetComponent<IT_UKETSUKE_SK_ENTRYDao>();
                this.skDetailDao = DaoInitUtility.GetComponent<IT_UKETSUKE_SK_DETAILDao>();
                // 20170601 wangjm モバイル将軍#105481 start
                this.kadoujyoukyouDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_KADOUJYOUKYOUDao>();

            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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

                // 親フォーム
                this.parentForm = this.form.Parent as BusinessBaseForm;

                // 画面初期表示設定
                this.InitializeScreen();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
                // システム情報を取得
                this.GetSysInfoInit();

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

        #region 画面初期表示設定
        /// <summary>
        /// 画面初期表示設定
        /// </summary>
        private void InitializeScreen(bool isKuria = false)
        {
            if (!isKuria)
            {
                // 配車区分
                this.form.RENKEI_KBN_1.Checked = true;
                this.form.HAISHA_KBN.Text = ConstCls.HAISHA_KBN_1;
                // 連携
                this.form.RENKEI_KBN_1.Checked = true;
                this.form.RENKEI_KBN.Text = ConstCls.RENKEI_KBN_1;
            }
            // 作業日
            this.form.SAGYOU_DATE_FROM.Value = parentForm.sysDate;
            this.form.SAGYOU_DATE_TO.Value = parentForm.sysDate;
            // 業者
            if (this.form.HAISHA_KBN.Text == ConstCls.HAISHA_KBN_1)
            {
                this.form.GYOUSHA_CD.Enabled = false;
                this.form.GYOUSHA_NAME.Enabled = false;
                this.form.GYOUSHA_BUTTON.Enabled = false;
            }
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME.Text = string.Empty;
            // 現場
            if (this.form.HAISHA_KBN.Text == ConstCls.HAISHA_KBN_1)
            {
                this.form.GENBA_CD.Enabled = false;
                this.form.GENBA_NAME.Enabled = false;
                this.form.GENBA_BUTTON.Enabled = false;
            }
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME.Text = string.Empty;
            // 運搬業者
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            // 車輌
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
            // 車種
            this.form.SHASHU_CD.Text = string.Empty;
            this.form.SHASHU_NAME.Text = string.Empty;
            // 運転者
            this.form.SHAIN_CD.Text = string.Empty;
            this.form.SHAIN_NAME.Text = string.Empty;
            // 回収実績
            this.form.KAISYUU_JYOUKYOU_3.Checked = true;
            this.form.KAISYUU_JYOUKYOU.Text = "3";
            // 明細項目
            this.form.Ichiran.Rows.Clear();
            if (!isKuria)
            {
                this.form.Ichiran.Template = this.form.tourokuSumiHaishaDetail1;
            }
            // 初期表示時に自動調整をし、その後は手での明細幅変更をできるようにする。
            this.form.Ichiran.HorizontalAutoSizeMode = ((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode)((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.CellsInColumnHeader | GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.DisplayedCellsInRow)));
            this.form.Ichiran.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;

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

                this.form.C_Regist(parentForm.bt_func8);
                parentForm.bt_func1.Click -= new System.EventHandler(this.bt_func1_Click);       //詳細確認
                parentForm.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);       //詳細確認
                parentForm.bt_func2.Click -= new System.EventHandler(this.bt_func2_Click);       //他社振替
                parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);       //他社振替
                parentForm.bt_func4.Click -= new System.EventHandler(this.bt_func4_Click);       //ﾓﾊﾞｲﾙ削除
                parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);       //ﾓﾊﾞｲﾙ削除
                parentForm.bt_func7.Click -= new System.EventHandler(this.bt_func7_Click);       //条件クリア
                parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //条件クリア
                parentForm.bt_func8.Click -= new System.EventHandler(this.bt_func8_Click);       //検索
                parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //検索
                parentForm.bt_func9.Click -= new EventHandler(this.bt_func9_Click);              //登録(ﾓﾊﾞｲﾙ登録/実績確定)
                parentForm.bt_func9.Click += new EventHandler(this.bt_func9_Click);              //登録(ﾓﾊﾞｲﾙ登録/実績確定)
                parentForm.bt_func12.Click -= new System.EventHandler(this.bt_func12_Click);     //閉じる
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる
                parentForm.bt_process1.Click += new System.EventHandler(this.bt_process1_Click); //稼働状況表示

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

        #region システム情報を取得
        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    // 数量フォーマット
                    this.systemSuuryouFormatCD = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT_CD, string.Empty).ToString();
                    // システム設定の売上支払情報確定フラグ
                    this.ursh_kakutei_flg = this.sysInfoEntity.UR_SH_KAKUTEI_FLAG;

                    // mapboxのアクセストークンまたはスタイルが空の場合、稼働状況表示ボタンを非表示にする。
                    if ((this.sysInfoEntity.MAPBOX_ACCESS_TOKEN == null || this.sysInfoEntity.MAPBOX_ACCESS_TOKEN.Equals(""))
                        || (this.sysInfoEntity.MAPBOX_MAP_STYLE == null || this.sysInfoEntity.MAPBOX_MAP_STYLE.Equals("")))
                    {
                        parentForm.bt_process1.Text = "";
                        parentForm.bt_process1.Click -= new System.EventHandler(this.bt_process1_Click);
                    }

                }
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
                    string haishaDenpyouNo = this.form.Ichiran.CurrentRow["HAISHA_DENPYOU_NO"].Value.ToString();
                    if (dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                    {
                        haishaKbn = "0";
                    }
                    else
                    {
                        haishaKbn = "1";
                    }
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

        #region F2 他社振替
        /// <summary>
        /// F2 他社振替
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {

            string haishaDenpyouNo = "";

            if (this.dto.RENKEI_KBN == null)
            {
                this.MsgBox.MessageBoxShowError("他車振替対象の明細がありません。");
                return;
            }

            if (!this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_1))
            {
                return;
            }

            //明細に何もなければエラー
            if (this.form.Ichiran.Rows.Count <= 0)
            {
                this.MsgBox.MessageBoxShowError("他車振替対象の明細がありません。");
                return;
            }

            // 選択チェック
            int taisyou = 0;

            foreach (var row in this.form.Ichiran.Rows)
            {
                if ((bool)row["TASHA_CHECK"].Value)
                {
                    taisyou = taisyou + 1;
                    if (string.IsNullOrEmpty(haishaDenpyouNo))
                    {
                        haishaDenpyouNo = row["HAISHA_DENPYOU_NO"].Value.ToString();
                    }
                    else
                    {
                        haishaDenpyouNo = haishaDenpyouNo + ", " + row["HAISHA_DENPYOU_NO"].Value.ToString();
                    }
                    //break;
                }
            }
            if (taisyou == 0)
            {
                this.MsgBox.MessageBoxShowError("他車振替対象の明細がありません。");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(taisyou + " 行が選択されています。\r\n他車振替登録画面を表示しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            LogUtility.DebugMethodStart(sender, e);
            if (this.form.Ichiran.CurrentRow != null)
            {
                if (dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                {
                    //定期配車
                    r_framework.FormManager.FormManager.OpenFormModal("G744", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, haishaDenpyouNo);
                }
                else
                {
                    //スポット
                    r_framework.FormManager.FormManager.OpenFormModal("G743", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, haishaDenpyouNo);
                }
            }
            else
            {
                this.MsgBox.MessageBoxShow("E076");
                return;
            }

        }
        #endregion

        #region F4 ﾓﾊﾞｲﾙ削除処理
        /// <summary>
        /// F4 ﾓﾊﾞｲﾙ削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //明細に何もなければエラー
                if (this.form.Ichiran.Rows.Count <= 0)
                {
                    this.MsgBox.MessageBoxShow("E076");
                    return;
                }

                // 選択チェック
                bool taisyou = false;

                foreach (var row in this.form.Ichiran.Rows)
                {
                    if ((bool)row[0].Value)
                    {
                        taisyou = true;
                        break;
                    }
                }
                if (!taisyou)
                {
                    this.MsgBox.MessageBoxShow("E050", "対象");
                    return;
                }

                if (!this.KaisyuuJissekiCheck())
                {
                    if (this.MsgBox.MessageBoxShow("C092", "実績の登録が", "削除") == DialogResult.Yes)
                    {
                        // データ削除処理
                        this.Delete();
                    }
                }
                else
                {
                    if (this.MsgBox.MessageBoxShow("C026") == DialogResult.Yes)
                    {
                        // データ削除処理
                        this.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func4_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [対象]項目にチェックがついているレコードを対象にする
        /// </summary>
        /// <returns>0件True:1件以上False</returns>
        internal bool KaisyuuJissekiCheck()
        {
            bool ret = true;

            foreach (var row in this.form.Ichiran.Rows)
            {
                if ((bool)row[0].Value)
                {
                    SqlInt16 HAISHA_KBN = SqlInt16.Parse(dto.HAISHA_KBN) - 1;
                    SqlInt64 HAISHA_DENPYOU_NO = SqlInt64.Parse(row["HAISHA_DENPYOU_NO"].Value.ToString());
                    DataTable count = this.TeikihaishaDao.GetKaisyuuJissekiCount(HAISHA_KBN, HAISHA_DENPYOU_NO);

                    if (count != null && count.Rows.Count > 0 && int.Parse(count.Rows[0][0].ToString()) > 0)
                    {
                        ret = false;
                        return ret;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// データ削除処理
        /// </summary>
        [Transaction]
        internal virtual void Delete()
        {
            try
            {
                if (!this.CreateEntity(true))
                {
                    return;
                }

                using (Transaction tran = new Transaction())
                {

                    // モバイル将軍業務TBLテーブル登録
                    foreach (T_MOBISYO_RT detail in this.entitysMobisyoRtList)
                    {
                        this.TmobisyoRtDao.Update(detail);
                    }
                    // モバイル将軍業務コンテナTBLテーブル登録
                    if (entitysMobisyoRtCTNList.Count > 0)
                    {
                        foreach (T_MOBISYO_RT_CONTENA detail in this.entitysMobisyoRtCTNList)
                        {
                            this.TmobisyoCTNDao.Update(detail);
                        }
                    }
                    // モバイル将軍業務詳細TBLテーブル登録           
                    foreach (T_MOBISYO_RT_DTL detail in this.entitysMobisyoRtDTLList)
                    {
                        this.TmobisyoRtDTLDao.Update(detail);
                    }
                    // モバイル将軍業務搬入TBL テーブル登録           
                    foreach (T_MOBISYO_RT_HANNYUU detail in this.entitysMobisyoRtHNList)
                    {
                        this.TmobisyoRtHNDao.Update(detail);
                    }
                    // トランザクション終了
                    tran.Commit();
                }

                this.MsgBox.MessageBoxShow("I001", "削除");

                //検索処理を行う
                if (0 < this.Search())
                {
                    // 画面反映
                    this.SetResultData();
                }
                else
                {
                    // 明細クリア
                    this.form.Ichiran.Rows.Clear();
                }
                
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Delete", ex1);
                this.MsgBox.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Delete", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Delete", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        #endregion

        #region F7 条件クリア
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

                // 画面初期表示設定
                this.InitializeScreen(true);
               
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func7_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F8検索処理
        /// <summary>
        /// F8検索
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                // 20170601 wangjm モバイル将軍#105481 start
                this.form.checkBoxAll.Checked = false;
                this.form.checkBoxAll2.Checked = false;
                // 20170601 wangjm モバイル将軍#105481 end

                LogUtility.DebugMethodStart(sender, e);
                if (this.form.RegistErrorFlag)
                {
                    // 必須アラートを表示させた後、フォーカスが項目に移動
                    if (this.form.HAISHA_KBN.IsInputErrorOccured)
                    {
                        this.form.HAISHA_KBN.Focus();
                    }
                    else if (this.form.RENKEI_KBN.IsInputErrorOccured)
                    {
                        this.form.RENKEI_KBN.Focus();
                    }
                    else if (this.form.SAGYOU_DATE_FROM.IsInputErrorOccured)
                    {
                        this.form.SAGYOU_DATE_FROM.Focus();
                    }
                    else if (this.form.SAGYOU_DATE_TO.IsInputErrorOccured)
                    {
                        this.form.SAGYOU_DATE_TO.Focus();
                    }
                    else if (this.form.KAISYUU_JYOUKYOU.IsInputErrorOccured)
                    {
                        this.form.KAISYUU_JYOUKYOU.Focus();
                    }

                    return;
                }
                // 入力チェック
                if (!this.CheckInputDate())
                {
                    return;
                }

                // 検索用dtoを作成
                this.dto = new DTOClass();
                this.dto.HAISHA_KBN = this.form.HAISHA_KBN.Text;
                this.dto.RENKEI_KBN = this.form.RENKEI_KBN.Text;
                this.dto.SAGYOU_DATE_FROM = ((DateTime)this.form.SAGYOU_DATE_FROM.Value).ToString("yyyy/MM/dd");
                this.dto.SAGYOU_DATE_TO = ((DateTime)this.form.SAGYOU_DATE_TO.Value).ToString("yyyy/MM/dd");
                this.dto.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                this.dto.GENBA_CD = this.form.GENBA_CD.Text;
                this.dto.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                this.dto.SHARYOU_CD = this.form.SHARYOU_CD.Text;
                this.dto.SHASHU_CD = this.form.SHASHU_CD.Text;
                this.dto.UNTENSHA_CD = this.form.SHAIN_CD.Text;
                this.dto.KAISYUU_JYOUKYOU = this.form.KAISYUU_JYOUKYOU.Text;

                //検索処理を行う
                if (0 < this.Search())
                {
                    // 画面反映
                    this.SetResultData();
                    // 実績確定がされいる定期配車番号のデータは、未登録の検索では表示させない。
                    if (this.form.Ichiran.RowCount < 1)
                    {
                        // 明細クリア
                        this.form.Ichiran.Rows.Clear();
                        // ゼロ件メッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001", "検索結果");
                    }
                }
                else
                {
                    // 明細クリア
                    this.form.Ichiran.Rows.Clear();
                    // ゼロ件メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001", "検索結果");
                }
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
        /// 検索処理を行う
        /// </summary>
        /// <returns>件数</returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.ResultTable = new DataTable();

                if (this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_1))
                {
                    // 読込系配車明細を表示する
                    if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                    {
                        this.ResultTable = this.TeikihaishaDao.GetDetailForTourokuSumiHaisha(dto);
                    }
                    // 読込系受付明細を表示する
                    else
                    {
                        this.ResultTable = this.TeikihaishaDao.GetDetailForTourokuSumiUketuke(dto);
                    }
                }
                else if (this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_2))
                {
                    // 読込系配車明細を表示する
                    if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                    {
                        this.ResultTable = this.TeikihaishaDao.GetDetailForTourokuSumiHaishaDetail(dto);
                    }
                    // 読込系受付明細を表示する
                    else
                    {
                        this.ResultTable = this.TeikihaishaDao.GetDetailForTourokuSumiUketukeDetail(dto);
                    }
                }
                else
                {
                    // 登録系配車明細を表示する
                    if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                    {
                        this.ResultTable = this.TeikihaishaDao.GetDetailForMiTourokuHaisha(dto);
                    }
                    // 登録系受付明細を表示する
                    else
                    {
                        this.ResultTable = this.TeikihaishaDao.GetDetailForMiTourokuUketuke(dto);
                    }
                }

                return this.ResultTable.Rows.Count;
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
        /// 検索データを明細画面に反映する
        /// </summary>
        private void SetResultData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 明細クリア
                this.form.Ichiran.Rows.Clear();

                int rowIndex = 0;
                int ZenHaishaNo = -1;
                int HaishaNo;

                if (this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_1))
                {
                    #region 読込系配車明細を表示する
                    if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                    {
                        foreach (DataRow row in this.ResultTable.Rows)
                        {
                            HaishaNo = int.Parse(row["HAISHA_DENPYOU_NO"].ToString());
                            if (HaishaNo != ZenHaishaNo)
                            {
                                this.form.Ichiran.Rows.Add();
                                this.form.Ichiran.Rows[rowIndex].Cells["TAISYOU_CHECK"].Value = false;
                                this.form.Ichiran.Rows[rowIndex].Cells["TASHA_CHECK"].Value = false;

                                if (!string.IsNullOrEmpty(row["MIKAISHU_CNT"].ToString()))
                                {
                                    this.form.Ichiran.Rows[rowIndex].Cells["KAISHU_JOKYO"].Value = "未回収";
                                }
                                else
                                {
                                    this.form.Ichiran.Rows[rowIndex].Cells["KAISHU_JOKYO"].Value = "回収済";
                                }
                                
                                this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_SAGYOU_DATE"].Value = string.IsNullOrEmpty(row["HAISHA_SAGYOU_DATE"].ToString()) ? string.Empty : Convert.ToDateTime(row["HAISHA_SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd(ddd)");
                                this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_DENPYOU_NO"].Value = row["HAISHA_DENPYOU_NO"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_COURSE_NAME_CD"].Value = row["HAISHA_COURSE_NAME_CD"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_COURSE_NAME"].Value = row["HAISHA_COURSE_NAME"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["SHASHU_NAME"].Value = row["SHASHU_NAME"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["SHARYOU_NAME"].Value = row["SHARYOU_NAME"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["UNTENSHA_NAME"].Value = row["UNTENSHA_NAME"].ToString();
                                rowIndex++;
                            }
                            
                            ZenHaishaNo = int.Parse(row["HAISHA_DENPYOU_NO"].ToString());
                        }
                    }
                    #endregion

                    #region 読込系受付明細を表示する
                    else
                    {
                        foreach (DataRow row in this.ResultTable.Rows)
                        {
                            HaishaNo = int.Parse(row["HAISHA_DENPYOU_NO"].ToString());
                            if (HaishaNo != ZenHaishaNo)
                            {
                                this.form.Ichiran.Rows.Add();
                                this.form.Ichiran.Rows[rowIndex].Cells["TAISYOU_CHECK"].Value = false;
                                this.form.Ichiran.Rows[rowIndex].Cells["TASHA_CHECK"].Value = false;
                                this.form.Ichiran.Rows[rowIndex].Cells["KAISHU_JOKYO"].Value = row["KAISHU_JOKYO"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_SAGYOU_DATE"].Value = string.IsNullOrEmpty(row["HAISHA_SAGYOU_DATE"].ToString()) ? string.Empty : Convert.ToDateTime(row["HAISHA_SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd(ddd)");
                                this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_DENPYOU_NO"].Value = row["HAISHA_DENPYOU_NO"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["GENBA_JISSEKI_GYOUSHACD"].Value = row["GENBA_JISSEKI_GYOUSHACD"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["GYOUSHA_NAME"].Value = row["GYOUSHA_NAME"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["GENBA_JISSEKI_GENBACD"].Value = row["GENBA_JISSEKI_GENBACD"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["GENBA_NAME"].Value = row["GENBA_NAME"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["SHARYOU_NAME"].Value = row["SHARYOU_NAME"].ToString();
                                this.form.Ichiran.Rows[rowIndex].Cells["UNTENSHA_NAME"].Value = row["UNTENSHA_NAME"].ToString();
                                // 20170601 wangjm モバイル将軍#105481 start
                                this.form.Ichiran.Rows[rowIndex].Cells["UKETSUKE_KBN"].Value = row["UKETSUKE_KBN"].ToString();
                                // 20170601 wangjm モバイル将軍#105481 end
                                rowIndex++;
                            }
                            ZenHaishaNo = int.Parse(row["HAISHA_DENPYOU_NO"].ToString());
                        }
                    }
                    #endregion
                }
                else if (this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_2))
                {
                    #region 読込系配車明細を表示する
                    if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                    {
                        foreach (DataRow row in this.ResultTable.Rows)
                        {
                            this.form.Ichiran.Rows.Add();
                            this.form.Ichiran.Rows[rowIndex].Cells["TAISYOU_CHECK"].Value = false;

                            // 回収状況
                            if (!string.IsNullOrEmpty(row["MIKAISHU_CNT"].ToString()))
                            {
                                this.form.Ichiran.Rows[rowIndex].Cells["KAISHU_JOKYO"].Value = "未回収";
                            }
                            else
                            {
                                this.form.Ichiran.Rows[rowIndex].Cells["KAISHU_JOKYO"].Value = "回収済";
                            }

                            this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_SAGYOU_DATE"].Value = string.IsNullOrEmpty(row["HAISHA_SAGYOU_DATE"].ToString()) ? string.Empty : Convert.ToDateTime(row["HAISHA_SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd(ddd)");
                            this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_DENPYOU_NO"].Value = row["HAISHA_DENPYOU_NO"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_COURSE_NAME_CD"].Value = row["HAISHA_COURSE_NAME_CD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_COURSE_NAME"].Value = row["HAISHA_COURSE_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["SHASHU_NAME"].Value = row["SHASHU_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["SHARYOU_NAME"].Value = row["SHARYOU_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["UNTENSHA_NAME"].Value = row["UNTENSHA_NAME"].ToString();
                            // 詳細追加項目
                            this.form.Ichiran.Rows[rowIndex].Cells["DETAIL_NO"].Value = row["DETAIL_NO"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["JYUNBAN"].Value = row["JYUNBAN"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["ROUND_NO"].Value = row["ROUND_NO"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GYOUSHA_CD"].Value = row["GYOUSHA_CD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GYOUSHA_NAME"].Value = row["GYOUSHA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GENBA_CD"].Value = row["GENBA_CD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GENBA_NAME"].Value = row["GENBA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HINMEI_CD"].Value = row["HINMEI_CD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HINMEI_NAME"].Value = row["HINMEI_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["SUURYOU"].Value = row["SUURYOU"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["UNIT_NAME"].Value = row["UNIT_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["KAISHU_JOKYO_GENBA"].Value = row["KAISHU_JOKYO"].ToString();

                            rowIndex++;
                        }
                    }
                    #endregion

                    #region 読込系受付明細を表示する
                    else
                    {
                        foreach (DataRow row in this.ResultTable.Rows)
                        {
                            this.form.Ichiran.Rows.Add();
                            this.form.Ichiran.Rows[rowIndex].Cells["TAISYOU_CHECK"].Value = false;
                            this.form.Ichiran.Rows[rowIndex].Cells["KAISHU_JOKYO"].Value = row["KAISHU_JOKYO"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_SAGYOU_DATE"].Value = string.IsNullOrEmpty(row["HAISHA_SAGYOU_DATE"].ToString()) ? string.Empty : Convert.ToDateTime(row["HAISHA_SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd(ddd)");
                            this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_DENPYOU_NO"].Value = row["HAISHA_DENPYOU_NO"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GENBA_JISSEKI_GYOUSHACD"].Value = row["GENBA_JISSEKI_GYOUSHACD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GYOUSHA_NAME"].Value = row["GYOUSHA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GENBA_JISSEKI_GENBACD"].Value = row["GENBA_JISSEKI_GENBACD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GENBA_NAME"].Value = row["GENBA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["SHARYOU_NAME"].Value = row["SHARYOU_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["UNTENSHA_NAME"].Value = row["UNTENSHA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["UKETSUKE_KBN"].Value = row["UKETSUKE_KBN"].ToString();
                            // 詳細追加項目
                            this.form.Ichiran.Rows[rowIndex].Cells["DETAIL_NO"].Value = row["DETAIL_NO"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HINMEI_CD"].Value = row["HINMEI_CD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HINMEI_NAME"].Value = row["HINMEI_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["SUURYOU"].Value = row["SUURYOU"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["UNIT_NAME"].Value = row["UNIT_NAME"].ToString();
                            rowIndex++;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 登録系配車明細を表示する
                    if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                    {
                        foreach (DataRow row in this.ResultTable.Rows)
                        {
                            DataTable Table = this.TeikihaishaDao.GetTeikiHaishaJissekiNo(SqlInt64.Parse(row["HAISHA_DENPYOU_NO"].ToString()));
                            if (Table.Rows.Count > 0)
                            {
                                continue;
                            }
                            Table = this.TeikihaishaDao.GetTeikiHaishaTorihikisakiUmu(SqlInt64.Parse(row["HAISHA_DENPYOU_NO"].ToString()));
                            if (Table.Rows.Count > 0)
                            {
                                continue;
                            }
                            this.form.Ichiran.Rows.Add();
                            this.form.Ichiran.Rows[rowIndex].Cells["TAISYOU_CHECK"].Value = false;
                            this.form.Ichiran.Rows[rowIndex].Cells["SAGYOU_DATE"].Value = string.IsNullOrEmpty(row["HAISHA_SAGYOU_DATE"].ToString()) ? string.Empty : Convert.ToDateTime(row["HAISHA_SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd(ddd)");
                            this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_DENPYOU_NO"].Value = row["HAISHA_DENPYOU_NO"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["COURSE_NAME_CD"].Value = row["HAISHA_COURSE_NAME_CD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["COURSE_NAME"].Value = row["HAISHA_COURSE_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["SHARYOU_NAME"].Value = row["SHARYOU_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["SHAIN_NAME"].Value = row["UNTENSHA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GYOUSHA_CD"].Value = row["GENBA_JISSEKI_GYOUSHACD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GYOUSHA_NAME"].Value = row["GYOUSHA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GENBA_CD"].Value = row["GENBA_JISSEKI_GENBACD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GENBA_NAME"].Value = row["GENBA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HINMEI_CD"].Value = row["GENBA_DETAIL_HINMEICD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HINMEI_NAME"].Value = row["HINMEI_NAME"].ToString();
                            // 非表示
                            this.form.Ichiran.Rows[rowIndex].Cells["ROW_NO"].Value = row["ROW_NO"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["HAISHA_ROW_NUMBER"].Value = row["HAISHA_ROW_NUMBER"].ToString();
                            rowIndex++;
                        }
                    }
                    #endregion

                    #region 登録系受付明細を表示する
                    else
                    {
                        foreach (DataRow row in this.ResultTable.Rows)
                        {
                            this.form.Ichiran.Rows.Add();
                            this.form.Ichiran.Rows[rowIndex].Cells["TAISYOU_CHECK"].Value = false;
                            this.form.Ichiran.Rows[rowIndex].Cells["SAGYOU_DATE"].Value = string.IsNullOrEmpty(row["SAGYOU_DATE"].ToString()) ? string.Empty : Convert.ToDateTime(row["SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd(ddd)");
                            this.form.Ichiran.Rows[rowIndex].Cells["UKETSUKE_NUMBER"].Value = row["UKETSUKE_NUMBER"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GYOUSHA_CD"].Value = row["GYOUSHA_CD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GYOUSHA_NAME"].Value = row["GYOUSHA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GENBA_CD"].Value = row["GENBA_CD"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["GENBA_NAME"].Value = row["GENBA_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["SHARYOU_NAME"].Value = row["SHARYOU_NAME"].ToString();
                            this.form.Ichiran.Rows[rowIndex].Cells["UNTENSHA_NAME"].Value = row["UNTENSHA_NAME"].ToString();
                            // 非表示
                            this.form.Ichiran.Rows[rowIndex].Cells["ROW_NO"].Value = row["ROW_NO"].ToString();
                            // 20170601 wangjm モバイル将軍#105481 start
                            //this.form.Ichiran.Rows[rowIndex].Cells["GENCHAKU_TIME_NAME"].Value = row["GENCHAKU_TIME_NAME"].ToString();
                            //this.form.Ichiran.Rows[rowIndex].Cells["GENCHAKU_TIME"].Value = row["GENCHAKU_TIME"].ToString();
                            // 20170601 wangjm モバイル将軍#105481 end
                            rowIndex++;
                        }
                    }
                    #endregion
                }
            // 初期表示時に自動調整をし、その後は手での明細幅変更をできるようにする。
                this.form.Ichiran.HorizontalAutoSizeMode = ((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode)((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.CellsInColumnHeader | GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.DisplayedCellsInRow)));
                this.form.Ichiran.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetResultData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckInputDate()
        {
            LogUtility.DebugMethodStart();

            // 整合性チェック 伝票日付 
            DateTime toDate = (DateTime)this.form.SAGYOU_DATE_TO.Value;
            DateTime fromDate = (DateTime)this.form.SAGYOU_DATE_FROM.Value;

            // 時間情報削除
            toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);

            if (fromDate > toDate)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.SAGYOU_DATE_FROM.IsInputErrorOccured = true;
                this.form.SAGYOU_DATE_TO.IsInputErrorOccured = true;
                this.form.SAGYOU_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.SAGYOU_DATE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "作業日From", "作業日To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.SAGYOU_DATE_FROM.Focus();
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region F9登録(ﾓﾊﾞｲﾙ登録/実績確定)処理
        /// <summary>
        /// F9登録(ﾓﾊﾞｲﾙ登録/実績確定)
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //明細に何もなければエラー
                if (this.form.Ichiran.Rows.Count <= 0)
                {
                    this.MsgBox.MessageBoxShow("E061");
                    return;
                }

                // 選択チェック
                bool taisyou = false;

                foreach (var row in this.form.Ichiran.Rows)
                {
                    if ((bool)row[0].Value)
                    {
                        taisyou = true;
                        break;
                    }
                }
                if (!taisyou)
                {
                    this.MsgBox.MessageBoxShow("E050", "対象");
                    return;
                }

                //登録チェック
                if (!this.Regist_Check())
                {
                    return;
                }

                // データ登録処理
                this.Regist();
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
        /// データ登録処理
        /// </summary>
        [Transaction]
        public virtual void Regist()
        {
            try
            {
                if (this.MsgBox.MessageBoxShow("C055", "登録") == DialogResult.Yes)
                {
                    //登録チェック
                    if (!this.Regist_Check())
                    {
                        return;
                    }

                    if (!this.CreateEntity(false))
                    {
                        return;
                    }

                    using (Transaction tran = new Transaction())
                    {


                        if (this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_1) || this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_2))
                        {
                            // スポットの場合は現場そのものが未回収の場合、定期の場合は同コースに未回収の現場がある場合
                            // 実績確定の確認ダイアログを表示
                            if (this.kakuteiForMikaishu)
                            {
                                this.kakuteiForMikaishu = false;

                                if (this.MsgBox.MessageBoxShowConfirm(
                                    "回収品名情報が不足しているデータが含まれています。\n\n対象のデータは確定処理されませんが、処理を続行しますか？"
                                    , MessageBoxDefaultButton.Button1)
                                    == DialogResult.Yes)
                                {
                                    // 処理続行
                                }
                                else
                                {
                                    return;
                                }
                            }

                            if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                            {
                                if (this.MobileMitorokiFlg)
                                {
                                    this.MobileMitorokiFlg = false;

                                    if (this.MsgBox.MessageBoxShow("C046", "モバイルに未登録のデータがある配車番号が見つかりました。実績確定を実施") == DialogResult.Yes)
                                    {
                                        // モバイル将軍業務TBLテーブル登録
                                        foreach (T_MOBISYO_RT detail in this.entitysMobisyoRtList)
                                        {
                                            this.TmobisyoRtDao.Update(detail);
                                        }

                                        // 定期配車実績entryテーブル登録
                                        if (this.entitysTeikiJisekiEntryList.Count > 0)
                                        {
                                            foreach (T_TEIKI_JISSEKI_ENTRY detail in this.entitysTeikiJisekiEntryList)
                                            {
                                                this.TtjeDao.Insert(detail);
                                            }
                                        }
                                        // 定期配車実績detailテーブル登録
                                        if (this.entitysTeikiJisekiDetailList.Count > 0)
                                        {
                                            foreach (T_TEIKI_JISSEKI_DETAIL detail in this.entitysTeikiJisekiDetailList)
                                            {
                                                this.TtjdDao.Insert(detail);
                                            }
                                        }

                                        // 定期配車実績niorosoテーブル登録
                                        if (this.entitysTeikiJisekiNioroshiList.Count > 0)
                                        {
                                            foreach (T_TEIKI_JISSEKI_NIOROSHI detail in this.entitysTeikiJisekiNioroshiList)
                                            {
                                                this.TtjnDao.Insert(detail);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    // モバイル将軍業務TBLテーブル登録
                                    foreach (T_MOBISYO_RT detail in this.entitysMobisyoRtList)
                                    {
                                        this.TmobisyoRtDao.Update(detail);
                                    }

                                    // 定期配車実績entryテーブル登録
                                    if (this.entitysTeikiJisekiEntryList.Count > 0)
                                    {
                                        foreach (T_TEIKI_JISSEKI_ENTRY detail in this.entitysTeikiJisekiEntryList)
                                        {
                                            this.TtjeDao.Insert(detail);
                                        }
                                    }
                                    // 定期配車実績detailテーブル登録
                                    if (this.entitysTeikiJisekiDetailList.Count > 0)
                                    {
                                        foreach (T_TEIKI_JISSEKI_DETAIL detail in this.entitysTeikiJisekiDetailList)
                                        {
                                            this.TtjdDao.Insert(detail);
                                        }
                                    }

                                    // 定期配車実績niorosoテーブル登録
                                    if (this.entitysTeikiJisekiNioroshiList.Count > 0)
                                    {
                                        foreach (T_TEIKI_JISSEKI_NIOROSHI detail in this.entitysTeikiJisekiNioroshiList)
                                        {
                                            this.TtjnDao.Insert(detail);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // モバイル将軍業務TBLテーブル登録
                                foreach (T_MOBISYO_RT detail in this.entitysMobisyoRtList)
                                {
                                    this.TmobisyoRtDao.Update(detail);
                                }

                                // 収集受付EntrySEQ
                                if (delSSEntryList.Count > 0)
                                {
                                    foreach (T_UKETSUKE_SS_ENTRY detail in this.delSSEntryList)
                                    {
                                        this.ssEntryDao.Update(detail);
                                    }
                                }

                                // 収集受付Entry計上
                                if (this.intSSEntryList.Count > 0)
                                {
                                    foreach (T_UKETSUKE_SS_ENTRY detail in this.intSSEntryList)
                                    {
                                        this.ssEntryDao.Insert(detail);
                                    }
                                }

                                // 収集受付Detail
                                if (this.intSSDetailList.Count > 0)
                                {
                                    foreach (T_UKETSUKE_SS_DETAIL detail in this.intSSDetailList)
                                    {
                                        this.ssDetailDao.Insert(detail);
                                    }
                                }

                                // 収集受付コンテナ
                                if (this.delContenaReserveList.Count > 0)
                                {
                                    foreach (T_CONTENA_RESERVE detail in this.delContenaReserveList)
                                    {
                                        this.contenaReseveDao.Update(detail);
                                    }
                                }

                                // 収集受付コンテナ
                                if (this.intContenaReserveList.Count > 0)
                                {
                                    foreach (T_CONTENA_RESERVE detail in this.intContenaReserveList)
                                    {
                                        this.contenaReseveDao.Insert(detail);
                                    }
                                }

                                // 20170612 wangjm モバイル将軍#105481 start 
                                // 出荷受付EntrySEQ
                                if (delSKEntryList.Count > 0)
                                {
                                    foreach (T_UKETSUKE_SK_ENTRY detail in this.delSKEntryList)
                                    {
                                        this.skEntryDao.Update(detail);
                                    }
                                }

                                // 出荷受付Entry計上
                                if (this.intSKEntryList.Count > 0)
                                {
                                    foreach (T_UKETSUKE_SK_ENTRY detail in this.intSKEntryList)
                                    {
                                        this.skEntryDao.Insert(detail);
                                    }
                                }

                                // 出荷受付Detail
                                if (this.intSKDetailList.Count > 0)
                                {
                                    foreach (T_UKETSUKE_SK_DETAIL detail in this.intSKDetailList)
                                    {
                                        this.skDetailDao.Insert(detail);
                                    }
                                }
                                // 20170612 wangjm モバイル将軍#105481 end

                                if (this.entitysUrShEntryList.Count > 0)
                                {
                                    // 売上支払entryテーブル登録
                                    foreach (T_UR_SH_ENTRY detail in this.entitysUrShEntryList)
                                    {
                                        this.UrsheDao.Insert(detail);
                                    }
                                }

                                // 売上支払detailテーブル登録
                                if (this.entitysUrShDetailList.Count > 0)
                                {
                                    foreach (T_UR_SH_DETAIL detail in this.entitysUrShDetailList)
                                    {
                                        this.UrshdDao.Insert(detail);
                                    }
                                }

                                // 売上支払コンテナ
                                if (this.intContenaResultList.Count > 0)
                                {
                                    foreach (T_CONTENA_RESULT detail in this.intContenaResultList)
                                    {
                                        this.contenaResultDao.Insert(detail);
                                    }
                                }

                                // コンテナ
                                if (this.upContenaist.Count > 0)
                                {
                                    foreach (M_CONTENA detail in this.upContenaist)
                                    {
                                        this.contenaDao.Update(detail);
                                    }
                                }
                            }
                           
                        }
                        else
                        {
                            // モバイル将軍業務TBLテーブル登録
                            foreach (T_MOBISYO_RT detail in this.entitysMobisyoRtList)
                            {
                                this.TmobisyoRtDao.Insert(detail);
                            }
                            // モバイル将軍業務詳細TBLテーブル登録     
                            if (entitysMobisyoRtCTNList.Count > 0)
                            {
                                foreach (T_MOBISYO_RT_CONTENA detail in this.entitysMobisyoRtCTNList)
                                {
                                    this.TmobisyoCTNDao.Insert(detail);
                                }
                            }
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
                        }
                        

                        // トランザクション終了
                        tran.Commit();
                    }

                    this.MsgBox.MessageBoxShow("I001", "登録");

                    //検索処理を行う
                    if (0 < this.Search())
                    {
                        // 画面反映
                        this.SetResultData();
                    }
                    else
                    {
                        // 明細クリア
                        this.form.Ichiran.Rows.Clear();
                    }

                    // ヘッダーの一括チェックボックスはOFFにする。
                    this.form.checkBoxAll.Checked = false;
                    this.form.checkBoxAll2.Checked = false;
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Regist", ex1);
                this.MsgBox.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="isDelete">True削除:False登録</param>
        /// <returns></returns>
        public bool CreateEntity(bool isDelete)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_1) || this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_2))
                {
                    if (isDelete)
                    {
                        #region 読込系モバイル削除Entityを作成
                        this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                        this.entitysMobisyoRtCTNList = new List<T_MOBISYO_RT_CONTENA>();
                        this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                        this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();

                        #region T_MOBISYO_RT
                        SqlInt64 HAISHA_DENPYOU_NO = -1;
                        SqlInt64 ZEN_HAISHA_DENPYOU_NO = -1;
                        foreach (var row in this.form.Ichiran.Rows)
                        {
                            // 配車伝票番号が同じ場合は処理をスキップする。
                            if (HAISHA_DENPYOU_NO.ToString().Equals(row["HAISHA_DENPYOU_NO"].Value.ToString()))
                            {
                                continue;
                            }

                            if ((bool)row[0].Value)
                            {
                                HAISHA_DENPYOU_NO = SqlInt64.Parse(row["HAISHA_DENPYOU_NO"].Value.ToString());
                                #region T_MOBISYO_RT_HANNYUU
                                if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                                {
                                    DataTable hannyuu = this.TeikihaishaDao.GetDeleteHannyuuDataByCD(HAISHA_DENPYOU_NO, false);
                                    if (hannyuu != null && hannyuu.Rows.Count > 0)
                                    {
                                        foreach (DataRow hannyuuRow in hannyuu.Rows)
                                        {
                                            if (hannyuuRow["HANYU_SEQ_NO"] == null || string.IsNullOrEmpty(hannyuuRow["HANYU_SEQ_NO"].ToString()))
                                            {
                                                continue;
                                            }
                                            var ListHannyuu = this.mTmobisyoRtHNDao.GetHannyuuDataByCD(SqlInt64.Parse(hannyuuRow["HANYU_SEQ_NO"].ToString()));
                                            ListHannyuu[0].DELETE_FLG = true;
                                            // 自動設定
                                            var dataBinderContenaResulthn = new DataBinderLogic<T_MOBISYO_RT_HANNYUU>(ListHannyuu[0]);
                                            dataBinderContenaResulthn.SetSystemProperty(ListHannyuu[0], false);
                                            this.entitysMobisyoRtHNList.Add(ListHannyuu[0]);
                                        }
                                    }
                                }
                                #endregion
                                int HAISHA_KBN =0;
                                if(!this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                                {
                                    HAISHA_KBN = 1;
                                }
                                var List = this.mTeikihaishaDao.GetRtDataByCD(HAISHA_DENPYOU_NO, HAISHA_KBN);
                                foreach (T_MOBISYO_RT count in List)
                                {
                                    count.DELETE_FLG = true;
                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT>(count);
                                    dataBinderContenaResult.SetSystemProperty(count, false);
                                    this.entitysMobisyoRtList.Add(count);


                                }
                            }
                        }
                        #endregion

                        #region T_MOBISYO_RT_CONTENA
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
                                    this.entitysMobisyoRtCTNList.Add(countDetail);
                                }
                            }
                        }
                        #endregion

                        #region T_MOBISYO_RT_DTL
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
                            }
                        }
                        #endregion

                        #region T_MOBISYO_RT_HANNYUU
                        if (!this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                        {
                            T_MOBISYO_RT_HANNYUU beforeCount = null;
                            foreach (T_MOBISYO_RT_DTL count in this.entitysMobisyoRtDTLList)
                            {
                                SqlInt64 HANYU_SEQ_NO = count.HANYU_SEQ_NO;
                                SqlInt64 HANYU_JISSEKI_SEQ_NO = count.HANYU_JISSEKI_SEQ_NO;
                                var ListHannyuu = this.mTmobisyoRtHNDao.GetHannyuuDataByCD(HANYU_SEQ_NO);
                                foreach (T_MOBISYO_RT_HANNYUU countHannyuu in ListHannyuu)
                                {
                                    if (beforeCount != null && countHannyuu.HANYU_SEQ_NO != beforeCount.HANYU_SEQ_NO)
                                    {
                                        countHannyuu.DELETE_FLG = true;
                                        // 自動設定
                                        var dataBinderContenaResulthn = new DataBinderLogic<T_MOBISYO_RT_HANNYUU>(countHannyuu);
                                        dataBinderContenaResulthn.SetSystemProperty(countHannyuu, false);
                                        this.entitysMobisyoRtHNList.Add(countHannyuu);
                                        if (!string.IsNullOrEmpty(HANYU_JISSEKI_SEQ_NO.ToString()) && countHannyuu.HANYU_SEQ_NO != HANYU_JISSEKI_SEQ_NO)
                                        {
                                            var ListHannyuuJisseki = this.mTmobisyoRtHNDao.GetHannyuuDataByCD(HANYU_JISSEKI_SEQ_NO);
                                            ListHannyuuJisseki[0].DELETE_FLG = true;
                                            var dataBinderContenaResulthnJisseki = new DataBinderLogic<T_MOBISYO_RT_HANNYUU>(ListHannyuuJisseki[0]);
                                            dataBinderContenaResulthnJisseki.SetSystemProperty(ListHannyuuJisseki[0], false);
                                            this.entitysMobisyoRtHNList.Add(ListHannyuuJisseki[0]);
                                        }

                                    }
                                    beforeCount = countHannyuu;
                                }
                            }
                        }
                        
                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region 読込系配車明細Entityを作成
                        if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                        {

                            #region 読込系モバイル実績登録Entityを作成
                            this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                            this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                            this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();
                            this.entitysTeikiJisekiEntryList = new List<T_TEIKI_JISSEKI_ENTRY>();
                            this.entitysTeikiJisekiDetailList = new List<T_TEIKI_JISSEKI_DETAIL>();
                            this.entitysTeikiJisekiNioroshiList = new List<T_TEIKI_JISSEKI_NIOROSHI>();
                            List<NiorosiClass> niorosiList = new List<NiorosiClass>();
                            SqlInt32 roundNo = 1;
                            SqlInt16 rowNo = 0;
                            int entitysMobisyoRtListIndex = -1;

                            #region T_MOBISYO_RTとT_TEIKI_JISSEKI_ENTRYとT_TEIKI_JISSEKI_NIOROSHI
                            SqlInt64 HAISHA_DENPYOU_NO = -1;
                            foreach (var row in this.form.Ichiran.Rows)
                            {
                                // 配車伝票番号が同じ場合は処理をスキップする。
                                if (HAISHA_DENPYOU_NO.ToString().Equals(row["HAISHA_DENPYOU_NO"].Value.ToString()))
                                {
                                    continue;
                                }

                                if ((bool)row[0].Value)
                                {
                                    HAISHA_DENPYOU_NO = SqlInt64.Parse(row["HAISHA_DENPYOU_NO"].Value.ToString());
                                    DataTable taikihaishaNO = this.TeikihaishaDao.GetTeikiHaishaNo(HAISHA_DENPYOU_NO);
                                    if (taikihaishaNO.Rows.Count > 0)
                                    {
                                        this.MobileMitorokiFlg = true;
                                    }
                                    var List = this.mTeikihaishaDao.GetRtDataByCD(HAISHA_DENPYOU_NO,0);
                                    foreach (T_MOBISYO_RT count in List)
                                    {
                                        count.JISSEKI_REGIST_FLG = true;
                                        // 自動設定
                                        var dataBinderContenaResultRt = new DataBinderLogic<T_MOBISYO_RT>(count);
                                        dataBinderContenaResultRt.SetSystemProperty(count, false);
                                        this.entitysMobisyoRtList.Add(count);
                                    }

                                    #region T_TEIKI_JISSEKI_ENTRY
                                    bool jyougaiFlg = true;
                                    int detailCont = 0;
                                    foreach (T_MOBISYO_RT count in List)
                                    {
                                        if (count.GENBA_JISSEKI_JYOGAIFLG == false || string.IsNullOrEmpty(count.GENBA_JISSEKI_JYOGAIFLG.ToString()))
                                        {
                                            jyougaiFlg = false;
                                        }

                                        // 全ての明細に数量が１つも含まれていない場合
                                        if (this.mTmobisyoRtDTLDao.GetDtlDataByCD((SqlInt64)count.SEQ_NO)
                                            .All(c => c.GENBA_DETAIL_SUURYO1.IsNull && c.GENBA_DETAIL_SUURYO2.IsNull))
                                        {
                                            detailCont++;
                                        }

                                    }
                                    // 回収状況が「回収無し」もしくは すべての明細に数量が含まれない場合のデータについては、実績取込の処理は実行しない。
                                    if (jyougaiFlg || List.Count() == detailCont)
                                    {
                                        continue;
                                    }
                                    T_TEIKI_JISSEKI_ENTRY ttje = new T_TEIKI_JISSEKI_ENTRY();
                                    ttje.SYSTEM_ID = this.createSystemIdForTeikiJisseki();
                                    ttje.SEQ = 1;
                                    // 拠点、振替配車、 曜日CD
                                    DataTable kyoutei = this.TeikihaishaDao.GetTeikiHaishaKyouteiData(HAISHA_DENPYOU_NO);
                                    if (kyoutei.Rows.Count > 0)
                                    {
                                        // 拠点
                                        if (!string.IsNullOrEmpty(kyoutei.Rows[0]["KYOTEN_CD"].ToString()))
                                        {
                                            ttje.KYOTEN_CD = SqlInt16.Parse(kyoutei.Rows[0]["KYOTEN_CD"].ToString());
                                        }
                                        // 振替配車
                                        if (kyoutei.Rows[0]["FURIKAE_HAISHA_KBN"] != null && !string.IsNullOrEmpty(kyoutei.Rows[0]["FURIKAE_HAISHA_KBN"].ToString()))
                                        {
                                            ttje.FURIKAE_HAISHA_KBN = SqlInt16.Parse(kyoutei.Rows[0]["FURIKAE_HAISHA_KBN"].ToString());
                                        }
                                        // 曜日CD
                                        if (kyoutei.Rows[0]["DAY_CD"] != null && !string.IsNullOrEmpty(kyoutei.Rows[0]["DAY_CD"].ToString()))
                                        {
                                            ttje.DAY_CD = SqlInt16.Parse(kyoutei.Rows[0]["DAY_CD"].ToString());
                                        }
                                    }
                                    ttje.TEIKI_JISSEKI_NUMBER = this.createTeikiJissekiNumber();
                                    ttje.WEATHER = string.Empty;
                                    ttje.DENPYOU_DATE = List[0].HAISHA_SAGYOU_DATE;
                                    ttje.SAGYOU_DATE = List[0].HAISHA_SAGYOU_DATE;
                                    ttje.COURSE_NAME_CD = List[0].HAISHA_COURSE_NAME_CD;
                                    ttje.SHARYOU_CD = List[0].SHARYOU_CD;
                                    ttje.SHASHU_CD = List[0].SHASHU_CD;
                                    ttje.UNTENSHA_CD = List[0].UNTENSHA_CD;
                                    ttje.UNPAN_GYOUSHA_CD = List[0].GENBA_JISSEKI_UPNGYOSHACD;
                                    ttje.HOJOIN_CD = string.Empty;
                                    ttje.TEIKI_HAISHA_NUMBER = HAISHA_DENPYOU_NO;
                                    ttje.MOBILE_SHOGUN_FILE_NAME = string.Empty;
                                    ttje.DELETE_FLG = false;

                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_TEIKI_JISSEKI_ENTRY>(ttje);
                                    dataBinderContenaResult.SetSystemProperty(ttje, false);

                                    // Listに追加
                                    this.entitysTeikiJisekiEntryList.Add(ttje);


                                    #endregion

                                    DataTable hannyuu = this.TeikihaishaDao.GetDeleteHannyuuDataByCD(HAISHA_DENPYOU_NO, true);
                                    if (hannyuu != null && hannyuu.Rows.Count > 0)
                                    {
                                        int niorosiRowNo = 0;
                                        foreach (DataRow hannyuuRow in hannyuu.Rows)
                                        {
                                            T_TEIKI_JISSEKI_NIOROSHI ttjn = new T_TEIKI_JISSEKI_NIOROSHI();
                                            SqlInt64 HANYU_JISSEKI_SEQ_NO = 0;
                                            if (!string.IsNullOrEmpty(hannyuuRow["HANYU_JISSEKI_SEQ_NO"].ToString()))
                                            {
                                                HANYU_JISSEKI_SEQ_NO = SqlInt64.Parse(hannyuuRow["HANYU_JISSEKI_SEQ_NO"].ToString());
                                                var ListHannyuu = this.mTmobisyoRtHNDao.GetHannyuuDataByCD(HANYU_JISSEKI_SEQ_NO);
                                                ttjn.NIOROSHI_GYOUSHA_CD = ListHannyuu[0].HANNYUU_JISSEKI_GYOUSHACD;
                                                ttjn.NIOROSHI_GENBA_CD = ListHannyuu[0].HANNYUU_JISSEKI_GENBACD;
                                                if (!string.IsNullOrEmpty(ListHannyuu[0].HANNYUU_JISSEKI_RYO.ToString()))
                                                {
                                                    ttjn.NIOROSHI_RYOU = SqlDecimal.Parse(ListHannyuu[0].HANNYUU_JISSEKI_RYO.ToString());
                                                }
                                                ttjn.HANNYUU_DATE = ListHannyuu[0].HANNYUU_HANNYUUDATE;
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                            
                                            ttjn.SYSTEM_ID = ttje.SYSTEM_ID;
                                            ttjn.SEQ = 1;
                                            niorosiRowNo++;
                                            ttjn.TEIKI_JISSEKI_NUMBER = ttje.TEIKI_JISSEKI_NUMBER;
                                            ttjn.NIOROSHI_NUMBER = niorosiRowNo;
                                            ttjn.ROW_NUMBER = SqlInt16.Parse(niorosiRowNo.ToString());

                                            // 自動設定
                                            var dataBinderContenaResultN = new DataBinderLogic<T_TEIKI_JISSEKI_NIOROSHI>(ttjn);
                                            dataBinderContenaResultN.SetSystemProperty(ttjn, false);

                                            // Listに追加
                                            this.entitysTeikiJisekiNioroshiList.Add(ttjn);

                                            NiorosiClass niorosi = new NiorosiClass();
                                            niorosi.TEIKI_HAISHA_NUMBER = HAISHA_DENPYOU_NO.ToString();
                                            niorosi.NIOROSHI_NUMBER = ttjn.NIOROSHI_NUMBER.Value.ToString();
                                            if (!string.IsNullOrEmpty(hannyuuRow["HANYU_JISSEKI_SEQ_NO"].ToString()))
                                            {
                                                niorosi.HANYU_SEQ_NO = HANYU_JISSEKI_SEQ_NO;
                                            }
                                            niorosiList.Add(niorosi);
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region T_TEIKI_JISSEKI_DETAIL
                            foreach (T_MOBISYO_RT count in this.entitysMobisyoRtList)
                            {

                                entitysMobisyoRtListIndex++;

                                if (count.GENBA_JISSEKI_JYOGAIFLG == true)
                                {
                                    continue;
                                }
                                SqlInt64 SEQ_NO = count.SEQ_NO;
                                var ListDetail = this.mTmobisyoRtDTLDao.GetDtlDataByCD(SEQ_NO);

                                // 数量が１つも含まれていない場合はDetailを生成しない
                                if (ListDetail.All(c => c.GENBA_DETAIL_SUURYO1.IsNull && c.GENBA_DETAIL_SUURYO2.IsNull))
                                {
                                    continue;
                                }

                                int rowNoIndex = 0;


                                if (entitysMobisyoRtListIndex > 0)
                                {
                                    if (count.HAISHA_DENPYOU_NO != this.entitysMobisyoRtList[entitysMobisyoRtListIndex - 1].HAISHA_DENPYOU_NO)
                                    {
                                        rowNo = 0;
                                        roundNo = 1;
                                        rowNoIndex = 0;
                                    }
                                    else
                                    {
                                        if (count.HAISHA_ROW_NUMBER != this.entitysMobisyoRtList[entitysMobisyoRtListIndex - 1].HAISHA_ROW_NUMBER)
                                        {
                                            List<T_MOBISYO_RT> data = (from temp in entitysMobisyoRtList
                                                                       where temp.HAISHA_DENPYOU_NO.ToString().Equals(count.HAISHA_DENPYOU_NO.ToString()) &&
                                                                             temp.GENBA_JISSEKI_GYOUSHACD.ToString().Equals(count.GENBA_JISSEKI_GYOUSHACD.ToString()) &&
                                                                             temp.GENBA_JISSEKI_GENBACD.ToString().Equals(count.GENBA_JISSEKI_GENBACD.ToString())
                                                                       select temp).ToList();

                                            if (data.Count > 1)
                                            {

                                                foreach (var row in data)
                                                {
                                                    rowNoIndex++;
                                                    if (row.HAISHA_ROW_NUMBER == count.HAISHA_ROW_NUMBER)
                                                    {
                                                        roundNo = rowNoIndex;
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                roundNo = 1;
                                            }
                                        }
                                    }
                                }

                                foreach (T_MOBISYO_RT_DTL countDetail in ListDetail.Where(c => !c.GENBA_DETAIL_SUURYO1.IsNull || !c.GENBA_DETAIL_SUURYO2.IsNull))
                                {
                                    rowNo = rowNo + 1;
                                    T_TEIKI_JISSEKI_DETAIL ttjd = new T_TEIKI_JISSEKI_DETAIL();
                                    List<T_TEIKI_JISSEKI_ENTRY> data = (from temp in this.entitysTeikiJisekiEntryList
                                                                        where temp.TEIKI_HAISHA_NUMBER.ToString().Equals(count.HAISHA_DENPYOU_NO.ToString())
                                                                        select temp).ToList();
                                    ttjd.SYSTEM_ID = data[0].SYSTEM_ID;
                                    ttjd.SEQ = 1;
                                    ttjd.DETAIL_SYSTEM_ID = this.createSystemIdForTeikiJisseki();
                                    ttjd.TEIKI_JISSEKI_NUMBER = data[0].TEIKI_JISSEKI_NUMBER;
                                    ttjd.ROW_NUMBER = rowNo;
                                    ttjd.ROUND_NO = roundNo;
                                    ttjd.GYOUSHA_CD = count.GENBA_JISSEKI_GYOUSHACD;
                                    ttjd.GENBA_CD = count.GENBA_JISSEKI_GENBACD;
                                    ttjd.HINMEI_CD = countDetail.GENBA_DETAIL_HINMEICD;
                                    if (!string.IsNullOrEmpty(countDetail.GENBA_DETAIL_SUURYO1.ToString()))
                                    {
                                        ttjd.SUURYOU = SqlDecimal.Parse(countDetail.GENBA_DETAIL_SUURYO1.ToString());
                                    }
                                    ttjd.UNIT_CD = countDetail.GENBA_DETAIL_UNIT_CD1;
                                    if (!string.IsNullOrEmpty(countDetail.GENBA_DETAIL_SUURYO2.ToString()))
                                    {
                                        ttjd.KANSAN_SUURYOU = SqlDecimal.Parse(countDetail.GENBA_DETAIL_SUURYO2.ToString());
                                    }
                                    ttjd.KANSAN_UNIT_CD = countDetail.GENBA_DETAIL_UNIT_CD2;
                                    List<NiorosiClass> niorosiData = (from temp in niorosiList
                                                                      where temp.HANYU_SEQ_NO.ToString().Equals(countDetail.HANYU_JISSEKI_SEQ_NO.ToString())
                                                                        select temp).ToList();
                                    if (niorosiData.Count > 0)
                                    {
                                        ttjd.NIOROSHI_NUMBER = SqlInt32.Parse(niorosiData[0].NIOROSHI_NUMBER);
                                    }
                                    ttjd.SHUUSHUU_TIME = count.GENBA_JISSEKI_SHUUSHUUTIME;
                                    ttjd.KAISHUU_BIKOU = string.Empty;
                                    ttjd.HINMEI_BIKOU = string.Empty;

                                    // マスタの検索条件設定（業者CD、現場CD、品名CD、単位CD、行番号）
                                    MobileShougunTorikomiDTOClass dto = new MobileShougunTorikomiDTOClass();
                                    dto.UNIT_CD = ttjd.UNIT_CD;
                                    dto.GYOUSHA_CD = ttjd.GYOUSHA_CD;
                                    dto.HINMEI_CD = ttjd.HINMEI_CD;
                                    dto.GENBA_CD = ttjd.GENBA_CD;
                                    dto.ROW_NO = SqlInt16.Parse(count.HAISHA_ROW_NUMBER.ToString());
                                    dto.HAISHA_DENPYOU_NO = int.Parse(count.HAISHA_DENPYOU_NO.ToString());

                                    // 品名情報取得
                                    var hinmeiInfo = this.getTeikiHinmeiInfo(dto);
                                    ttjd.DENPYOU_KBN_CD = hinmeiInfo.DENPYOU_KBN_CD;
                                    ttjd.KEIYAKU_KBN = hinmeiInfo.KEIYAKU_KBN;
                                    ttjd.TSUKIGIME_KBN = hinmeiInfo.KEIJYOU_KBN;
                                    ttjd.ANBUN_FLG = hinmeiInfo.ANBUN_FLG;

                                    // INPUT_KBN(入力区分)
                                    if (countDetail.GENBA_DETAIL_ADDHINMEIFLG.IsTrue)
                                    {
                                        // モバイル将軍で新規追加の場合は「直接入力」固定
                                        ttjd.INPUT_KBN = ConstCls.INPUT_KBN_1;
                                    }
                                    else
                                    {
                                        // 検索条件に紐付く、定期配車情報を取得
                                        ttjd.INPUT_KBN = getGenbaTeikiHinmeiInfo(dto);
                                    }

                                    // 換算後単位CD、換算数量、換算後単位モバイル出力フラグ
                                    var kansanData = this.GetKansanData(ttjd, ttjd.TEIKI_JISSEKI_NUMBER.ToString());
                                    if (kansanData != null)
                                    {
                                        if (false == kansanData.KANSAN_UNIT_CD.IsNull && true == ttjd.KANSAN_UNIT_CD.IsNull)
                                        {
                                            ttjd.KANSAN_UNIT_CD = kansanData.KANSAN_UNIT_CD;
                                        }
                                        if (false == kansanData.KANSAN_SUURYOU.IsNull && true == ttjd.KANSAN_SUURYOU.IsNull)
                                        {
                                            ttjd.KANSAN_SUURYOU = kansanData.KANSAN_SUURYOU;
                                        }
                                        ttjd.KANSAN_UNIT_MOBILE_OUTPUT_FLG = kansanData.KANSAN_UNIT_MOBILE_OUTPUT_FLG;
                                    }
                                   
                                    if (!ttjd.KANSAN_SUURYOU.IsNull)
                                    {
                                        ttjd.KANSAN_SUURYOU = mlogic.GetSuuryoRound(decimal.Parse(ttjd.KANSAN_SUURYOU.ToString()), this.systemSuuryouFormatCD);
                                    }
                                    // Ver2.2から新たに追加された項目 KAKUTEI_FLG（確定フラグ）False（固定値）
                                    ttjd.KAKUTEI_FLG = false;

                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_TEIKI_JISSEKI_DETAIL>(ttjd);
                                    dataBinderContenaResult.SetSystemProperty(ttjd, false);

                                    // Listに追加
                                    this.entitysTeikiJisekiDetailList.Add(ttjd);
                                }

                                //entitysMobisyoRtListIndex++;  //処理を先頭に移動
                            }
                            #endregion

                            #endregion
                        }
                        #endregion

                        #region 読込系受付明細Entityを作成
                        else
                        {
                            this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                            this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                            this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();
                            this.entitysUrShEntryList = new List<T_UR_SH_ENTRY>();
                            this.entitysUrShDetailList = new List<T_UR_SH_DETAIL>();
                            this.delSSEntryList = new List<T_UKETSUKE_SS_ENTRY>();
                            this.intSSEntryList = new List<T_UKETSUKE_SS_ENTRY>();
                            this.intSSDetailList = new List<T_UKETSUKE_SS_DETAIL>();
                            this.delContenaReserveList = new List<T_CONTENA_RESERVE>();
                            this.intContenaReserveList = new List<T_CONTENA_RESERVE>();
                            this.intContenaResultList = new List<T_CONTENA_RESULT>();
                            this.upContenaist = new List<M_CONTENA>();

                            // 20170612 wangjm モバイル将軍#105481 start
                            this.delSKEntryList = new List<T_UKETSUKE_SK_ENTRY>();
                            this.intSKEntryList = new List<T_UKETSUKE_SK_ENTRY>();
                            this.intSKDetailList = new List<T_UKETSUKE_SK_DETAIL>();
                            // 20170612 wangjm モバイル将軍#105481 start

                            SqlInt64 HAISHA_DENPYOU_NO = -1;
                            foreach (var row in this.form.Ichiran.Rows)
                            {
                                // 配車伝票番号が同じ場合は処理をスキップする。
                                if (HAISHA_DENPYOU_NO.ToString().Equals(row["HAISHA_DENPYOU_NO"].Value.ToString()))
                                {
                                    continue;
                                }

                                if ((bool)row[0].Value)
                                {
                                    // 回収状況が「未回収」の場合は処理をスキップする。
                                    if (ConstCls.KAISHU_JOKYO_MI.Equals(row["KAISHU_JOKYO"].Value.ToString()))
                                    {
                                        this.kakuteiForMikaishu = true;
                                        continue;
                                    }

                                    SqlInt16 rowNo = 0;
                                    HAISHA_DENPYOU_NO = SqlInt64.Parse(row["HAISHA_DENPYOU_NO"].Value.ToString());
                                    string UKETSUKE_KBN = row["UKETSUKE_KBN"].Value.ToString();
                                    DataTable data = this.TeikihaishaDao.GetUrShDataEntity(HAISHA_DENPYOU_NO, UKETSUKE_KBN);
                                    var List = this.mTeikihaishaDao.GetRtDataByCD(HAISHA_DENPYOU_NO,1);
                                    foreach (T_MOBISYO_RT count in List)
                                    {
                                        count.JISSEKI_REGIST_FLG = true;
                                        // 自動設定
                                        var dataBinderContenaResultRt = new DataBinderLogic<T_MOBISYO_RT>(count);
                                        dataBinderContenaResultRt.SetSystemProperty(count, false);
                                        this.entitysMobisyoRtList.Add(count);
                                    }
                                    #region T_UR_SH_ENTRYとT_UR_SH_DETAIL
                                    // 回収状況が「回収無し」のデータについては、実績取込の処理は実行しない。
                                    bool jyougaiFlg = true;
                                    foreach (T_MOBISYO_RT count in List)
                                    {
                                        if (count.GENBA_JISSEKI_JYOGAIFLG == false || string.IsNullOrEmpty(count.GENBA_JISSEKI_JYOGAIFLG.ToString()))
                                        {
                                            jyougaiFlg = false;
                                        }

                                    }
                                    // 回収状況が「回収無し」のデータについては、実績取込の処理は実行しない。
                                    if (jyougaiFlg)
                                    {
                                        if (row["UKETSUKE_KBN"].Value.ToString().Equals("1"))
                                        {
                                            #region 収集受付伝票の配車状況が計上になる
                                            // T_UKETSUKE_SS_ENTRY
                                            T_UKETSUKE_SS_ENTRY ssEntry = new T_UKETSUKE_SS_ENTRY();
                                            ssEntry.UKETSUKE_NUMBER = HAISHA_DENPYOU_NO;
                                            ssEntry.DELETE_FLG = true;
                                            var delSSentry = this.ssEntryDao.GetDataForEntity(ssEntry);
                                            if (delSSentry != null)
                                            {
                                                delSSentry.DELETE_FLG = false;
                                                delSSEntryList.Add(delSSentry);
                                            }

                                            var intSSentry = this.ssEntryDao.GetDataForEntity(ssEntry);
                                            if (intSSentry != null)
                                            {
                                                intSSentry.SEQ = intSSentry.SEQ + 1;
                                                intSSentry.HAISHA_JOKYO_CD = ConstCls.HAISHA_JOKYO_CD_5;
                                                intSSentry.HAISHA_JOKYO_NAME = ConstCls.HAISHA_JOKYO_NAME_5;
                                                string CREATE_USER = intSSentry.CREATE_USER;
                                                SqlDateTime CREATE_DATE = intSSentry.CREATE_DATE;
                                                string CREATE_PC = intSSentry.CREATE_PC;
                                                var dataBinderContenaResultSSentry = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(intSSentry);
                                                dataBinderContenaResultSSentry.SetSystemProperty(intSSentry, false);
                                                intSSentry.CREATE_USER = CREATE_USER;
                                                intSSentry.CREATE_DATE = CREATE_DATE;
                                                intSSentry.CREATE_PC = CREATE_PC;

                                                intSSentry.CONTENA_SOUSA_CD = SqlInt16.Null;
                                                intSSEntryList.Add(intSSentry);
                                            }

                                            // T_UKETSUKE_SS_DETAIL
                                            T_UKETSUKE_SS_DETAIL ssDetail = new T_UKETSUKE_SS_DETAIL();
                                            ssDetail.SYSTEM_ID = delSSentry.SYSTEM_ID;
                                            ssDetail.SEQ = delSSentry.SEQ;
                                            var intSSDetail = this.ssDetailDao.GetDataForEntity(ssDetail);
                                            if (intSSDetail.Length > 0)
                                            {
                                                foreach (T_UKETSUKE_SS_DETAIL detail in intSSDetail)
                                                {
                                                    detail.SEQ = detail.SEQ + 1;
                                                    var dataBinderContenaResultSSdetail = new DataBinderLogic<T_UKETSUKE_SS_DETAIL>(detail);
                                                    dataBinderContenaResultSSdetail.SetSystemProperty(detail, false);
                                                    intSSDetailList.Add(detail);
                                                }
                                            }

                                            // T_CONTENA_RESERVE
                                            T_CONTENA_RESERVE contenaR = new T_CONTENA_RESERVE();
                                            contenaR.SYSTEM_ID = delSSentry.SYSTEM_ID;
                                            contenaR.SEQ = delSSentry.SEQ;
                                            contenaR.DELETE_FLG = true;
                                            var delcontenaR = this.contenaReseveDao.GetContenaDataByMobisyo(contenaR);
                                            if (delcontenaR.Rows.Count > 0)
                                            {
                                                foreach (DataRow contena in delcontenaR.Rows)
                                                {
                                                    if (String.IsNullOrEmpty(contena["SEQ_NO"].ToString()))
                                                    {
                                                        continue;
                                                    }

                                                    var result = this.contenaReseveDao.GetContenaDetail(SqlInt64.Parse(contena["SEQ_NO"].ToString()));
                                                    if (result.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rows in result.Rows)
                                                        {                                                            
                                                            if ((!string.IsNullOrEmpty(rows["CONTENA_SHURUI_CD1"].ToString()) && string.IsNullOrEmpty(rows["CONTENA_SHURUI_CD2"].ToString())) ||
                                                                    (!string.IsNullOrEmpty(rows["CONTENA_SHURUI_CD1"].ToString()) && !string.IsNullOrEmpty(rows["CONTENA_SHURUI_CD2"].ToString())
                                                                        && rows["CONTENA_SHURUI_CD1"].ToString() == rows["CONTENA_SHURUI_CD2"].ToString() && rows["CONTENA_CD1"].ToString() == rows["CONTENA_CD2"].ToString()
                                                                        && rows["CONTENA_SET_KBN1"].ToString() == rows["CONTENA_SET_KBN2"].ToString()))
                                                            {
                                                                var updateContenaR = this.contenaReseveDao.GetDataForEntity(contenaR);
                                                                if (updateContenaR.Length > 0)
                                                                {
                                                                    foreach (T_CONTENA_RESERVE target in updateContenaR)
                                                                    {                                                                   
                                                                        target.DELETE_FLG = false;
                                                                        var checkDel = delContenaReserveList.FindAll(x => x.CONTENA_CD.Equals(target.CONTENA_CD.ToString()) 
                                                                                                                    && x.CONTENA_SHURUI_CD.Equals(target.CONTENA_SHURUI_CD.ToString())
                                                                                                                    && x.CONTENA_SET_KBN.Value == target.CONTENA_SET_KBN.Value);
                                                                        if (checkDel.Count == 0) delContenaReserveList.Add(target);

                                                                        var resultContena = new T_CONTENA_RESERVE();
                                                                        resultContena.SYSTEM_ID = contenaR.SYSTEM_ID;
                                                                        resultContena.SEQ = contenaR.SEQ + 1;
                                                                        resultContena.CONTENA_CD = target.CONTENA_CD;
                                                                        resultContena.CONTENA_SHURUI_CD = target.CONTENA_SHURUI_CD;
                                                                        resultContena.CONTENA_SET_KBN = target.CONTENA_SET_KBN;
                                                                        resultContena.DAISUU_CNT = target.DAISUU_CNT;
                                                                        var CREATE_DATE = target.CREATE_DATE;
                                                                        var CREATE_PC = target.CREATE_PC;
                                                                        var CREATE_USER = target.CREATE_USER;
                                                                        var dataBinderContenaResultSSentry = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(intSSentry);
                                                                        dataBinderContenaResultSSentry.SetSystemProperty(resultContena, false);
                                                                        resultContena.CREATE_DATE = CREATE_DATE;
                                                                        resultContena.CREATE_PC = CREATE_PC;
                                                                        resultContena.CREATE_USER = CREATE_USER;
                                                                        resultContena.TIME_STAMP = target.TIME_STAMP;

                                                                        var checkInt = intContenaReserveList.FindAll(x => x.CONTENA_CD.Equals(resultContena.CONTENA_CD.ToString())
                                                                                                                        && x.CONTENA_SHURUI_CD.Equals(resultContena.CONTENA_SHURUI_CD.ToString())
                                                                                                                        && x.CONTENA_SET_KBN.Value == resultContena.CONTENA_SET_KBN.Value);
                                                                        if (checkInt.Count == 0) intContenaReserveList.Add(resultContena);
                                                                    }
                                                                }
                                                            }
                                                            else if (!string.IsNullOrEmpty(rows["CONTENA_SHURUI_CD2"].ToString()))
                                                            {
                                                                if (!string.IsNullOrEmpty(rows["CONTENA_SHURUI_CD1"].ToString()))
                                                                {
                                                                    var updateContenaR = this.contenaReseveDao.GetDataForEntity(contenaR);
                                                                    if (updateContenaR.Length > 0)
                                                                    {
                                                                        foreach (T_CONTENA_RESERVE target in updateContenaR)
                                                                        {
                                                                            target.DELETE_FLG = true;
                                                                            var checkDel = delContenaReserveList.FindAll(x => x.CONTENA_CD.Equals(target.CONTENA_CD.ToString())
                                                                                                                    && x.CONTENA_SHURUI_CD.Equals(target.CONTENA_SHURUI_CD.ToString())
                                                                                                                    && x.CONTENA_SET_KBN.Value == target.CONTENA_SET_KBN.Value);
                                                                            if (checkDel.Count == 0) delContenaReserveList.Add(target);
                                                                        }
                                                                    }
                                                                }

                                                                var resultContena = new T_CONTENA_RESERVE();
                                                                resultContena.SYSTEM_ID = SqlInt64.Parse(contena["SYSTEM_ID"].ToString());
                                                                resultContena.SEQ = contenaR.SEQ + 1;
                                                                resultContena.CONTENA_CD = rows["CONTENA_CD2"].ToString();
                                                                resultContena.CONTENA_SHURUI_CD = rows["CONTENA_SHURUI_CD2"].ToString();
                                                                resultContena.CONTENA_SET_KBN = SqlInt16.Parse(rows["CONTENA_SET_KBN2"].ToString());
                                                                resultContena.DAISUU_CNT = SqlInt32.Parse(rows["DAISUU_CNT2"].ToString());
                                                                var CREATE_DATE = Convert.ToDateTime(contena["CREATE_DATE"].ToString());
                                                                var CREATE_PC = contena["CREATE_PC"].ToString();
                                                                var CREATE_USER = contena["CREATE_USER"].ToString();
                                                                var dataBinderContenaResultSSentry = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(intSSentry);
                                                                dataBinderContenaResultSSentry.SetSystemProperty(resultContena, false);
                                                                resultContena.CREATE_DATE = CREATE_DATE;
                                                                resultContena.CREATE_PC = CREATE_PC;
                                                                resultContena.CREATE_USER = CREATE_USER;
                                                                resultContena.TIME_STAMP = Encoding.UTF8.GetBytes(contena["TIME_STAMP"].ToString());
                                                                var checkInt = intContenaReserveList.FindAll(x => x.CONTENA_CD.Equals(resultContena.CONTENA_CD.ToString())
                                                                                                                        && x.CONTENA_SHURUI_CD.Equals(resultContena.CONTENA_SHURUI_CD.ToString())
                                                                                                                        && x.CONTENA_SET_KBN.Value == resultContena.CONTENA_SET_KBN.Value);
                                                                if (checkInt.Count == 0) intContenaReserveList.Add(resultContena);
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            //収集受付のコンテナ状況更新用
                                            if (this.intSSEntryList.Count != 0 && this.intSSEntryList != null)
                                            {
                                                for (int i = 0; i < intSSEntryList.Count; i++)
                                                {
                                                    //コンテナ操作CDがある場合はスキップ
                                                    if (!intSSEntryList[i].CONTENA_SOUSA_CD.IsNull)
                                                    {
                                                        continue;
                                                    }

                                                    SqlInt16 secchiCnt = 0;
                                                    SqlInt16 hikiageCnt = 0;
                                                    for (int s = 0; s < intContenaReserveList.Count; s++)
                                                    {
                                                        if (intSSEntryList[i].SYSTEM_ID == intContenaReserveList[s].SYSTEM_ID && intSSEntryList[i].SEQ == intContenaReserveList[s].SEQ)
                                                        {
                                                            //台数カウント
                                                            if (!intContenaReserveList[s].DAISUU_CNT.IsNull)
                                                            {
                                                                if (intContenaReserveList[s].CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                                                                {
                                                                    secchiCnt += intContenaReserveList[s].CONTENA_SET_KBN;
                                                                }
                                                                if (intContenaReserveList[s].CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                                                                {
                                                                    hikiageCnt += intContenaReserveList[s].CONTENA_SET_KBN;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    // コンテナ操作CD
                                                    if (0 < secchiCnt && 0 < hikiageCnt)
                                                    {
                                                        // 交換
                                                        intSSEntryList[i].CONTENA_SOUSA_CD = 1;
                                                        continue;
                                                    }
                                                    else if (0 < secchiCnt && 0 == hikiageCnt)
                                                    {
                                                        // 設置
                                                        intSSEntryList[i].CONTENA_SOUSA_CD = 2;
                                                        continue;
                                                    }
                                                    else if (0 == secchiCnt && 0 < hikiageCnt)
                                                    {
                                                        // 引揚
                                                        intSSEntryList[i].CONTENA_SOUSA_CD = 3;
                                                        continue;
                                                    }
                                                }
                                            }
                                            #endregion

                                        }
                                        else
                                        {
                                            #region 出荷受付伝票の配車状況が計上になる
                                            // T_UKETSUKE_SK_ENTRY
                                            T_UKETSUKE_SK_ENTRY skEntry = new T_UKETSUKE_SK_ENTRY();
                                            skEntry.UKETSUKE_NUMBER = HAISHA_DENPYOU_NO;
                                            skEntry.DELETE_FLG = true;
                                            var delSKentry = this.skEntryDao.GetDataForEntity(skEntry);
                                            if (delSKentry != null)
                                            {
                                                delSKentry.DELETE_FLG = false;
                                                delSKEntryList.Add(delSKentry);
                                            }

                                            var intSKentry = this.skEntryDao.GetDataForEntity(skEntry);
                                            if (intSKentry != null)
                                            {
                                                intSKentry.SEQ = intSKentry.SEQ + 1;
                                                intSKentry.HAISHA_JOKYO_CD = ConstCls.HAISHA_JOKYO_CD_5;
                                                intSKentry.HAISHA_JOKYO_NAME = ConstCls.HAISHA_JOKYO_NAME_5;
                                                string CREATE_USER = intSKentry.CREATE_USER;
                                                SqlDateTime CREATE_DATE = intSKentry.CREATE_DATE;
                                                string CREATE_PC = intSKentry.CREATE_PC;
                                                var dataBinderContenaResultSKentry = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(intSKentry);
                                                dataBinderContenaResultSKentry.SetSystemProperty(intSKentry, false);
                                                intSKentry.CREATE_USER = CREATE_USER;
                                                intSKentry.CREATE_DATE = CREATE_DATE;
                                                intSKentry.CREATE_PC = CREATE_PC;

                                                intSKEntryList.Add(intSKentry);
                                            }

                                            // T_UKETSUKE_SK_DETAIL
                                            T_UKETSUKE_SK_DETAIL skDetail = new T_UKETSUKE_SK_DETAIL();
                                            skDetail.SYSTEM_ID = delSKentry.SYSTEM_ID;
                                            skDetail.SEQ = delSKentry.SEQ;
                                            var intSKDetail = this.skDetailDao.GetDataForEntity(skDetail);
                                            if (intSKDetail.Length > 0)
                                            {
                                                foreach (T_UKETSUKE_SK_DETAIL detail in intSKDetail)
                                                {
                                                    detail.SEQ = detail.SEQ + 1;
                                                    var dataBinderContenaResultSKdetail = new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(detail);
                                                    dataBinderContenaResultSKdetail.SetSystemProperty(detail, false);
                                                    intSKDetailList.Add(detail);
                                                }
                                            }
                                            #endregion

                                        }
                                        continue;
                                    }

                                    // 数量がない品名は実績登録しないこと伝票は作成しないこと
                                    if (data.Rows.Count < 1)
                                    {
                                        continue;
                                    }

                                    if (row["UKETSUKE_KBN"].Value.ToString().Equals("1"))
                                    {
                                        #region 収集受付伝票の配車状況が計上になる
                                        // T_UKETSUKE_SS_ENTRY
                                        T_UKETSUKE_SS_ENTRY ssEntry = new T_UKETSUKE_SS_ENTRY();
                                        ssEntry.UKETSUKE_NUMBER = HAISHA_DENPYOU_NO;
                                        ssEntry.DELETE_FLG = true;
                                        var delSSentry = this.ssEntryDao.GetDataForEntity(ssEntry);
                                        if (delSSentry != null)
                                        {
                                            delSSentry.DELETE_FLG = false;
                                            delSSEntryList.Add(delSSentry);
                                        }

                                        var intSSentry = this.ssEntryDao.GetDataForEntity(ssEntry);
                                        if (intSSentry != null)
                                        {
                                            intSSentry.SEQ = intSSentry.SEQ + 1;
                                            intSSentry.HAISHA_JOKYO_CD = ConstCls.HAISHA_JOKYO_CD;
                                            intSSentry.HAISHA_JOKYO_NAME = ConstCls.HAISHA_JOKYO_NAME;
                                            string CREATE_USER = intSSentry.CREATE_USER;
                                            SqlDateTime CREATE_DATE = intSSentry.CREATE_DATE;
                                            string CREATE_PC = intSSentry.CREATE_PC;
                                            var dataBinderContenaResultSSentry = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(intSSentry);
                                            dataBinderContenaResultSSentry.SetSystemProperty(intSSentry, false);
                                            intSSentry.CREATE_USER = CREATE_USER;
                                            intSSentry.CREATE_DATE = CREATE_DATE;
                                            intSSentry.CREATE_PC = CREATE_PC;

                                            intSSentry.CONTENA_SOUSA_CD = SqlInt16.Null;
                                            intSSEntryList.Add(intSSentry);
                                        }

                                        // T_UKETSUKE_SS_DETAIL
                                        T_UKETSUKE_SS_DETAIL ssDetail = new T_UKETSUKE_SS_DETAIL();
                                        ssDetail.SYSTEM_ID = delSSentry.SYSTEM_ID;
                                        ssDetail.SEQ = delSSentry.SEQ;
                                        var intSSDetail = this.ssDetailDao.GetDataForEntity(ssDetail);
                                        if (intSSDetail.Length > 0)
                                        {
                                            foreach (T_UKETSUKE_SS_DETAIL detail in intSSDetail)
                                            {
                                                detail.SEQ = detail.SEQ + 1;
                                                var dataBinderContenaResultSSdetail = new DataBinderLogic<T_UKETSUKE_SS_DETAIL>(detail);
                                                dataBinderContenaResultSSdetail.SetSystemProperty(detail, false);
                                                intSSDetailList.Add(detail);
                                            }
                                        }

                                        // T_CONTENA_RESERVE
                                        T_CONTENA_RESERVE contenaR = new T_CONTENA_RESERVE();
                                        contenaR.SYSTEM_ID = delSSentry.SYSTEM_ID;
                                        contenaR.SEQ = delSSentry.SEQ;
                                        var delcontenaR = this.contenaReseveDao.GetDataForEntity(contenaR);
                                        if (delcontenaR.Length > 0)
                                        {
                                            if (delcontenaR.Length > 0)
                                            {
                                                foreach (T_CONTENA_RESERVE contena in delcontenaR)
                                                {
                                                    contena.CALC_DAISUU_FLG = false;
                                                    string CREATE_USER = contena.CREATE_USER;
                                                    SqlDateTime CREATE_DATE = contena.CREATE_DATE;
                                                    string CREATE_PC = contena.CREATE_PC;
                                                    var dataBinderContenaResultcontenaR = new DataBinderLogic<T_CONTENA_RESERVE>(contena);
                                                    dataBinderContenaResultcontenaR.SetSystemProperty(contena, false);
                                                    contena.CREATE_USER = CREATE_USER;
                                                    contena.CREATE_DATE = CREATE_DATE;
                                                    contena.CREATE_PC = CREATE_PC;
                                                    delContenaReserveList.Add(contena);
                                                }
                                            }
                                        }

                                        #endregion

                                    }
                                    else
                                    {
                                        #region 出荷受付伝票の配車状況が計上になる
                                        // T_UKETSUKE_SK_ENTRY
                                        T_UKETSUKE_SK_ENTRY skEntry = new T_UKETSUKE_SK_ENTRY();
                                        skEntry.UKETSUKE_NUMBER = HAISHA_DENPYOU_NO;
                                        skEntry.DELETE_FLG = true;
                                        var delSKentry = this.skEntryDao.GetDataForEntity(skEntry);
                                        if (delSKentry != null)
                                        {
                                            delSKentry.DELETE_FLG = false;
                                            delSKEntryList.Add(delSKentry);
                                        }

                                        var intSKentry = this.skEntryDao.GetDataForEntity(skEntry);
                                        if (intSKentry != null)
                                        {
                                            intSKentry.SEQ = intSKentry.SEQ + 1;
                                            intSKentry.HAISHA_JOKYO_CD = ConstCls.HAISHA_JOKYO_CD;
                                            intSKentry.HAISHA_JOKYO_NAME = ConstCls.HAISHA_JOKYO_NAME;
                                            string CREATE_USER = intSKentry.CREATE_USER;
                                            SqlDateTime CREATE_DATE = intSKentry.CREATE_DATE;
                                            string CREATE_PC = intSKentry.CREATE_PC;
                                            var dataBinderContenaResultSKentry = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(intSKentry);
                                            dataBinderContenaResultSKentry.SetSystemProperty(intSKentry, false);
                                            intSKentry.CREATE_USER = CREATE_USER;
                                            intSKentry.CREATE_DATE = CREATE_DATE;
                                            intSKentry.CREATE_PC = CREATE_PC;
                                         
                                            intSKEntryList.Add(intSKentry);
                                        }

                                        // T_UKETSUKE_SK_DETAIL
                                        T_UKETSUKE_SK_DETAIL skDetail = new T_UKETSUKE_SK_DETAIL();
                                        skDetail.SYSTEM_ID = delSKentry.SYSTEM_ID;
                                        skDetail.SEQ = delSKentry.SEQ;
                                        var intSKDetail = this.skDetailDao.GetDataForEntity(skDetail);
                                        if (intSKDetail.Length > 0)
                                        {
                                            foreach (T_UKETSUKE_SK_DETAIL detail in intSKDetail)
                                            {
                                                detail.SEQ = detail.SEQ + 1;
                                                var dataBinderContenaResultSKdetail = new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(detail);
                                                dataBinderContenaResultSKdetail.SetSystemProperty(detail, false);
                                                intSKDetailList.Add(detail);
                                            }
                                        }                                       
                                        #endregion

                                    }

                                   
                                    string HANYU_SEQ_NO = "-1";

                                    foreach (DataRow urshRow in data.Rows)
                                    {
                                        if(HANYU_SEQ_NO == urshRow["HANYU_SEQ_NO"].ToString())
                                        {
                                            continue;
                                        }
                                        HANYU_SEQ_NO = urshRow["HANYU_SEQ_NO"].ToString();
                                        T_UR_SH_ENTRY urshe = new T_UR_SH_ENTRY();
                                        // 取引先請求支払情報
                                        M_TORIHIKISAKI_SEIKYUU torihikiSeikyuInfo = null;
                                        M_TORIHIKISAKI_SHIHARAI torihikiShiharaiInfo = null;
                                        short kyotenCd = -1;    // 拠点CD
                                        urshe.SYSTEM_ID = this.createSystemIdForUrsh();
                                        urshe.SEQ = 1;
                                        if (urshRow["KYOTEN_CD"] != null && !string.IsNullOrEmpty(urshRow["KYOTEN_CD"].ToString()))
                                        {
                                            urshe.KYOTEN_CD = SqlInt16.Parse(urshRow["KYOTEN_CD"].ToString());
                                            kyotenCd = short.Parse(urshRow["KYOTEN_CD"].ToString());
                                        }
                                        urshe.UR_SH_NUMBER = this.createUrshNumber();
                                        urshe.KAKUTEI_KBN = this.ursh_kakutei_flg;
                                        urshe.DENPYOU_DATE = SqlDateTime.Parse(urshRow["HAISHA_SAGYOU_DATE"].ToString());
                                        urshe.URIAGE_DATE = SqlDateTime.Parse(urshRow["HAISHA_SAGYOU_DATE"].ToString());
                                        urshe.SHIHARAI_DATE = SqlDateTime.Parse(urshRow["HAISHA_SAGYOU_DATE"].ToString());
                                        urshe.TORIHIKISAKI_CD = urshRow["TORIHIKISAKI_CD"].ToString();
                                        urshe.TORIHIKISAKI_NAME = urshRow["TORIHIKISAKI_NAME"].ToString();
                                        urshe.GYOUSHA_CD = urshRow["GENBA_JISSEKI_GYOUSHACD"].ToString();
                                        urshe.GYOUSHA_NAME = urshRow["GYOUSHA_NAME"].ToString();
                                        urshe.GENBA_CD = urshRow["GENBA_JISSEKI_GENBACD"].ToString();
                                        urshe.GENBA_NAME = urshRow["GENBA_NAME"].ToString();
                                        //urshe.NIZUMI_GYOUSHA_CD = string.Empty;
                                        //urshe.NIZUMI_GYOUSHA_NAME = string.Empty;
                                        //urshe.NIZUMI_GENBA_CD = string.Empty;
                                        //urshe.NIZUMI_GENBA_NAME = string.Empty;

                                        if (row["UKETSUKE_KBN"].Value.ToString().Equals("1"))
                                        {
                                            urshe.NIOROSHI_GYOUSHA_CD = urshRow["HANNYUU_JISSEKI_GYOUSHACD"].ToString();
                                            urshe.NIOROSHI_GYOUSHA_NAME = urshRow["NIOROSHI_GYOUSHA_NAME"].ToString();
                                            urshe.NIOROSHI_GENBA_CD = urshRow["HANNYUU_JISSEKI_GENBACD"].ToString();
                                            urshe.NIOROSHI_GENBA_NAME = urshRow["NIOROSHI_GENBA_NAME"].ToString();
                                        }
                                      
                                        urshe.EIGYOU_TANTOUSHA_CD = urshRow["EIGYOU_TANTOUSHA_CD"].ToString();
                                        urshe.EIGYOU_TANTOUSHA_NAME = urshRow["EIGYOU_TANTOUSHA_NAME"].ToString();
                                        urshe.NYUURYOKU_TANTOUSHA_CD = SystemProperty.Shain.CD;
                                        urshe.NYUURYOKU_TANTOUSHA_NAME = SystemProperty.Shain.Name;
                                        urshe.SHARYOU_CD = urshRow["SHARYOU_CD"].ToString();
                                        urshe.SHARYOU_NAME = urshRow["SHARYOU_NAME"].ToString();
                                        urshe.SHASHU_CD = urshRow["SHASHU_CD"].ToString();
                                        urshe.SHASHU_NAME = urshRow["SHASHU_NAME"].ToString();
                                        urshe.UNPAN_GYOUSHA_CD = urshRow["GENBA_JISSEKI_UPNGYOSHACD"].ToString();
                                        urshe.UNPAN_GYOUSHA_NAME = urshRow["UNPAN_GYOUSHA_NAME"].ToString();
                                        urshe.UNTENSHA_CD = urshRow["UNTENSHA_CD"].ToString();
                                        urshe.UNTENSHA_NAME = urshRow["UNTENSHA_NAME"].ToString();
                                        if (!string.IsNullOrEmpty(urshRow["MANIFEST_SHURUI_CD"].ToString()))
                                        {
                                            urshe.MANIFEST_SHURUI_CD = SqlInt16.Parse(urshRow["MANIFEST_SHURUI_CD"].ToString());
                                        }
                                        if (!string.IsNullOrEmpty(urshRow["MANIFEST_TEHAI_CD"].ToString()))
                                        {
                                            urshe.MANIFEST_TEHAI_CD = SqlInt16.Parse(urshRow["MANIFEST_TEHAI_CD"].ToString());
                                        }
                                        //urshe.DENPYOU_BIKOU = string.Empty;
                                        urshe.UKETSUKE_NUMBER = HAISHA_DENPYOU_NO;
                                        urshe.DAINOU_FLG = false;

                                        M_GYOUSHA gyoushaEntity = null;
                                        M_GENBA genbaEntity = null;

                                        // 業者CD
                                        if (false == string.IsNullOrEmpty(urshRow["GENBA_JISSEKI_GYOUSHACD"].ToString()))
                                        {

                                            // 業者名
                                            gyoushaEntity = this.gyoushaDao.GetDataByCd(urshRow["GENBA_JISSEKI_GYOUSHACD"].ToString());

                                        }

                                        // 現場CD
                                        if (false == string.IsNullOrEmpty(urshRow["GENBA_JISSEKI_GENBACD"].ToString()))
                                        {
                                            // 現場名
                                            var findEntity = new M_GENBA();
                                            findEntity.GYOUSHA_CD = urshRow["GENBA_JISSEKI_GYOUSHACD"].ToString();
                                            findEntity.GENBA_CD = urshRow["GENBA_JISSEKI_GENBACD"].ToString();
                                            genbaEntity = this.genbaDao.GetDataByCd(findEntity);

                                        }

                                        // 受付番号から受付収集伝票に紐付く取引先が取得出来ていなかった場合
                                        if (true == string.IsNullOrEmpty(urshe.TORIHIKISAKI_CD))
                                        {
                                            if (genbaEntity != null)
                                            {
                                                // 現場マスタから取引先CDを取得
                                                urshe.TORIHIKISAKI_CD = genbaEntity.TORIHIKISAKI_CD;
                                            }
                                            else
                                            {
                                                // 現場マスタが取得出来なかった場合
                                                if (gyoushaEntity != null)
                                                {
                                                    // 業者マスタから取引先CDを取得
                                                    urshe.TORIHIKISAKI_CD = gyoushaEntity.TORIHIKISAKI_CD;
                                                }
                                            }
                                        }

                                        if (false == string.IsNullOrEmpty(urshe.TORIHIKISAKI_CD))
                                        {
                                            // 取引マスタより取引先名取得
                                            //var entity = this.torihikisakiDao.GetDataByCd(urshe.TORIHIKISAKI_CD);
                                            //if (entity != null)
                                            //{
                                            //    if (entity.SHOKUCHI_KBN.IsFalse)
                                            //    {
                                            //        // 取引先名
                                            //        if (false == string.IsNullOrEmpty(entity.TORIHIKISAKI_NAME_RYAKU))
                                            //        {
                                            //            urshe.TORIHIKISAKI_NAME = entity.TORIHIKISAKI_NAME_RYAKU;
                                            //        }
                                            //    }
                                            //}

                                            // 取引先請求情報取得
                                            torihikiSeikyuInfo = this.torihikiSeikyuDao.GetDataByCd(urshe.TORIHIKISAKI_CD);
                                            if (torihikiSeikyuInfo != null)
                                            {
                                                // 売上税計算区分CD
                                                if (false == torihikiSeikyuInfo.ZEI_KEISAN_KBN_CD.IsNull)
                                                {
                                                    urshe.URIAGE_ZEI_KEISAN_KBN_CD = torihikiSeikyuInfo.ZEI_KEISAN_KBN_CD;
                                                }

                                                // 売上税区分CD
                                                if (false == torihikiSeikyuInfo.ZEI_KBN_CD.IsNull)
                                                {
                                                    urshe.URIAGE_ZEI_KBN_CD = torihikiSeikyuInfo.ZEI_KBN_CD;
                                                }

                                                // 売上取引区分CD
                                                if (false == torihikiSeikyuInfo.TORIHIKI_KBN_CD.IsNull)
                                                {
                                                    urshe.URIAGE_TORIHIKI_KBN_CD = torihikiSeikyuInfo.TORIHIKI_KBN_CD;
                                                }
                                            }

                                            // 取引先支払情報取得
                                            torihikiShiharaiInfo = this.torihikiShiharaiDao.GetDataByCd(urshe.TORIHIKISAKI_CD);
                                            if (torihikiShiharaiInfo != null)
                                            {
                                                // 支払税計算区分CD
                                                if (false == torihikiShiharaiInfo.ZEI_KEISAN_KBN_CD.IsNull)
                                                {
                                                    urshe.SHIHARAI_ZEI_KEISAN_KBN_CD = torihikiShiharaiInfo.ZEI_KEISAN_KBN_CD;
                                                }

                                                // 支払税区分CD
                                                if (false == torihikiShiharaiInfo.ZEI_KBN_CD.IsNull)
                                                {
                                                    urshe.SHIHARAI_ZEI_KBN_CD = torihikiShiharaiInfo.ZEI_KBN_CD;
                                                }

                                                // 支払取引区分CD
                                                if (false == torihikiShiharaiInfo.TORIHIKI_KBN_CD.IsNull)
                                                {
                                                    urshe.SHIHARAI_TORIHIKI_KBN_CD = torihikiShiharaiInfo.TORIHIKI_KBN_CD;
                                                }
                                            }
                                        }

                                        // 日連番取得
                                        urshe.DATE_NUMBER = this.GetDateNum(DateTime.Parse(urshRow["HAISHA_SAGYOU_DATE"].ToString()), kyotenCd);

                                        // 年連番取得
                                        urshe.YEAR_NUMBER = this.GetYearNum(DateTime.Parse(urshRow["HAISHA_SAGYOU_DATE"].ToString()), kyotenCd);

                                        if (row["UKETSUKE_KBN"].Value.ToString().Equals("1"))
                                        {
                                            // T_CONTENA_RESULT
                                            DataTable contenaData = this.TeikihaishaDao.GetUrShContenaData(List[0].SEQ_NO);
                                            if (contenaData.Rows.Count > 0)
                                            {
                                                SqlInt16 secchiCnt = 0;
                                                SqlInt16 hikiageCnt = 0;
                                                foreach (DataRow contenaRow in contenaData.Rows)
                                                {
                                                    T_CONTENA_RESULT contenaDetail = new T_CONTENA_RESULT();
                                                    // 伝種区分CD 3を設定
                                                    contenaDetail.DENSHU_KBN_CD = 3;
                                                    // システムID
                                                    contenaDetail.SYSTEM_ID = urshe.SYSTEM_ID;
                                                    // システムSEQ
                                                    contenaDetail.SEQ = urshe.SEQ;
                                                    // 設置引揚区分
                                                    contenaDetail.CONTENA_SET_KBN = SqlInt16.Parse(contenaRow["CONTENA_SET_KBN"].ToString());
                                                    // コンテナ種類CD
                                                    contenaDetail.CONTENA_SHURUI_CD = contenaRow["CONTENA_SHURUI_CD"].ToString();
                                                    // コンテナCD
                                                    contenaDetail.CONTENA_CD = contenaRow["CONTENA_CD"].ToString();
                                                    // 台数
                                                    contenaDetail.DAISUU_CNT = SqlInt16.Parse(contenaRow["DAISUU_CNT"].ToString());
                                                    // 削除フラグ
                                                    contenaDetail.DELETE_FLG = false;
                                                    var dataBinderContenaResultCTNdetail = new DataBinderLogic<T_CONTENA_RESULT>(contenaDetail);
                                                    dataBinderContenaResultCTNdetail.SetSystemProperty(contenaDetail, false);

                                                    this.intContenaResultList.Add(contenaDetail);

                                                    // 台数計算
                                                    if (!contenaDetail.DAISUU_CNT.IsNull)
                                                    {
                                                        // 設置
                                                        if (contenaDetail.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                                                        {
                                                            secchiCnt += contenaDetail.CONTENA_SET_KBN;
                                                        }
                                                        // 引揚
                                                        else if (contenaDetail.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                                                        {
                                                            hikiageCnt += contenaDetail.CONTENA_SET_KBN;
                                                        }
                                                    }

                                                    // M_CONTENA
                                                    if (string.IsNullOrEmpty(contenaDetail.CONTENA_SHURUI_CD) || string.IsNullOrEmpty(contenaDetail.CONTENA_CD))
                                                    {
                                                        continue;
                                                    }
                                                    M_CONTENA contenaMtr = this.contenaDao.GetContenaMasterEntity(contenaDetail.CONTENA_SHURUI_CD, contenaDetail.CONTENA_CD);
                                                    if (contenaMtr != null)
                                                    {
                                                        // 設置日、引揚日をチェック
                                                        if ((!contenaMtr.SECCHI_DATE.IsNull
                                                            && contenaMtr.SECCHI_DATE.Value.Date > SqlDateTime.Parse(urshRow["HAISHA_SAGYOU_DATE"].ToString()))
                                                            || (!contenaMtr.HIKIAGE_DATE.IsNull
                                                            && contenaMtr.HIKIAGE_DATE.Value.Date > SqlDateTime.Parse(urshRow["HAISHA_SAGYOU_DATE"].ToString())))
                                                        {
                                                            // 設置日、引揚日が作業日より新しい場合は何もしない。
                                                            continue;
                                                        }

                                                        // 画面の入力内容をコンテナマスタに反映させる
                                                        if (!string.IsNullOrEmpty(urshe.GYOUSHA_CD))
                                                        {
                                                            contenaMtr.GYOUSHA_CD = urshe.GYOUSHA_CD;
                                                        }
                                                        if (!string.IsNullOrEmpty(urshe.GENBA_CD))
                                                        {
                                                            contenaMtr.GENBA_CD = urshe.GENBA_CD;
                                                        }
                                                        contenaMtr.SHARYOU_CD = string.Empty;
                                                        if (contenaDetail.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                                                        {
                                                            // 設置の場合
                                                            contenaMtr.SECCHI_DATE = SqlDateTime.Parse(urshRow["HAISHA_SAGYOU_DATE"].ToString());
                                                            contenaMtr.HIKIAGE_DATE = SqlDateTime.Null;
                                                            contenaMtr.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_SECCHI;
                                                        }
                                                        else if (contenaDetail.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                                                        {
                                                            // 引揚の場合
                                                            contenaMtr.HIKIAGE_DATE = SqlDateTime.Parse(urshRow["HAISHA_SAGYOU_DATE"].ToString());
                                                            contenaMtr.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_HIKIAGE;
                                                        }
                                                        // 自動設定項目
                                                        string createUser = contenaMtr.CREATE_USER;
                                                        SqlDateTime createDate = contenaMtr.CREATE_DATE;
                                                        string createPC = contenaMtr.CREATE_PC;
                                                        var dataBinderUkeireEntry = new DataBinderLogic<M_CONTENA>(contenaMtr);
                                                        dataBinderUkeireEntry.SetSystemProperty(contenaMtr, false);
                                                        // Create情報は前の状態を引き継ぐ
                                                        contenaMtr.CREATE_USER = createUser;
                                                        contenaMtr.CREATE_DATE = createDate;
                                                        contenaMtr.CREATE_PC = createPC;

                                                        this.upContenaist.Add(contenaMtr);
                                                    }

                                                }
                                                // コンテナ操作CD
                                                if(0 < secchiCnt && 0 < hikiageCnt)
                                                {
                                                    // 交換
                                                    urshe.CONTENA_SOUSA_CD = 1;
                                                }
                                                else if (0 < secchiCnt && 0 == hikiageCnt)
                                                {
                                                    // 設置
                                                    urshe.CONTENA_SOUSA_CD = 2;
                                                }
                                                else if (0 == secchiCnt && 0 < hikiageCnt)
                                                {
                                                    // 引揚
                                                    urshe.CONTENA_SOUSA_CD = 3;
                                                }
                                            }
                                        }

                                        List<T_UR_SH_DETAIL> tempUrShDetail = new List<T_UR_SH_DETAIL>();
                                        #region T_UR_SH_DETAIL
                                        foreach (DataRow dataRow in data.Rows)
                                        {
                                            if (dataRow["HANYU_SEQ_NO"].ToString() != urshRow["HANYU_SEQ_NO"].ToString())
                                            {
                                                continue;
                                            }
                                            rowNo = rowNo + 1;
                                            T_UR_SH_DETAIL urshd = new T_UR_SH_DETAIL();

                                            urshd.SYSTEM_ID = urshe.SYSTEM_ID;
                                            urshd.SEQ = 1;
                                            urshd.DETAIL_SYSTEM_ID = this.createSystemIdForUrsh();
                                            urshd.UR_SH_NUMBER = urshe.UR_SH_NUMBER;
                                            urshd.ROW_NO = rowNo;
                                            urshd.KAKUTEI_KBN = this.ursh_kakutei_flg;
                                            urshd.URIAGESHIHARAI_DATE = SqlDateTime.Parse(dataRow["HAISHA_SAGYOU_DATE"].ToString());
                                            // 品名CD
                                            if (false == string.IsNullOrEmpty(dataRow["GENBA_DETAIL_HINMEICD"].ToString()))
                                            {
                                                urshd.HINMEI_CD = dataRow["GENBA_DETAIL_HINMEICD"].ToString();

                                                // 品名、伝票区分CD、税区分CDを取得
                                                var findEntity = new MobileShougunTorikomiDTOClass();
                                                findEntity.HINMEI_CD = urshd.HINMEI_CD;
                                                var dataResultHinmei = this.TeikihaishaDao.GetHinmeiDataForEntity(findEntity);
                                                var findKobetsuEntity = new MobileShougunTorikomiDTOClass();
                                                findKobetsuEntity.GYOUSHA_CD = urshe.GYOUSHA_CD;
                                                findKobetsuEntity.GENBA_CD = urshe.GENBA_CD;
                                                findKobetsuEntity.HINMEI_CD = urshd.HINMEI_CD;
                                                var dataResultKobetsuHinmei = this.TeikihaishaDao.GetKobetsuHinmeiDataForEntity(findKobetsuEntity);
                                                if (dataResultHinmei.Rows.Count != 0)
                                                {
                                                    if (dataResultKobetsuHinmei.Rows.Count != 0)
                                                    {
                                                        urshd.HINMEI_NAME = Convert.ToString(dataResultKobetsuHinmei.Rows[0]["SEIKYUU_HINMEI_NAME"]);
                                                    }
                                                    else
                                                    {
                                                        var findKobetsuEntity2 = new MobileShougunTorikomiDTOClass();
                                                        findKobetsuEntity2.GYOUSHA_CD = urshe.GYOUSHA_CD;
                                                        findKobetsuEntity2.GENBA_CD = "";
                                                        findKobetsuEntity2.HINMEI_CD = urshd.HINMEI_CD;
                                                        var dataResultKobetsuHinmei2 = this.TeikihaishaDao.GetKobetsuHinmeiDataForEntity(findKobetsuEntity2);
                                                        if (dataResultKobetsuHinmei2.Rows.Count != 0)
                                                        {
                                                            urshd.HINMEI_NAME = Convert.ToString(dataResultKobetsuHinmei2.Rows[0]["SEIKYUU_HINMEI_NAME"]);
                                                        }
                                                        else
                                                        {
                                                            // 品名
                                                            if (false == string.IsNullOrEmpty(dataResultHinmei.Rows[0]["HINMEI_NAME"].ToString()))
                                                            {
                                                                urshd.HINMEI_NAME = dataResultHinmei.Rows[0]["HINMEI_NAME"].ToString();
                                                            }
                                                        }
                                                    }

                                                    // 税区分CD
                                                    if (false == string.IsNullOrEmpty(dataResultHinmei.Rows[0]["ZEI_KBN_CD"].ToString()))
                                                    {
                                                        urshd.HINMEI_ZEI_KBN_CD = Int16.Parse(dataResultHinmei.Rows[0]["ZEI_KBN_CD"].ToString());
                                                    }

                                                    // 伝票区分CD
                                                    if (false == string.IsNullOrEmpty(dataResultHinmei.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                                                    {
                                                        // 伝票区分：支払以外は全て売上として扱う
                                                        if (CommonConst.DENPYOU_KBN_SHIHARAI == Int16.Parse(dataResultHinmei.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                                                        {
                                                            // 伝票区分：支払
                                                            urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_SHIHARAI;
                                                        }
                                                        else
                                                        {
                                                            // 伝票区分：売上
                                                            urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                                                        }
                                                    }
                                                }
                                            }

                                            // 伝票区分の設定値は「品名マスタ」から取得する前に、収集受付(出荷受付)から同一の品名ＣＤがあればそこから取得すること
                                            DataTable uketukeHinmeiDenyouKbn = this.TeikihaishaDao.GetDenpyoKbnByUktukeNo(urshe.UKETSUKE_NUMBER, urshd.HINMEI_CD, SqlInt16.Null);
                                            if (uketukeHinmeiDenyouKbn.Rows.Count == 1)
                                            {
                                                // 伝票区分：支払以外は全て売上として扱う
                                                if (CommonConst.DENPYOU_KBN_SHIHARAI == Int16.Parse(uketukeHinmeiDenyouKbn.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                                                {
                                                    // 伝票区分：支払
                                                    urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_SHIHARAI;
                                                }
                                                else
                                                {
                                                    // 伝票区分：売上
                                                    urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                                                }
                                            }
                                            else if (uketukeHinmeiDenyouKbn.Rows.Count > 1)
                                            {
                                                // 多件場合、枝番を条件にする検索
                                                DataTable uketukeHinmeiDenyouKbnEdaban = this.TeikihaishaDao.GetDenpyoKbnByUktukeNo(urshe.UKETSUKE_NUMBER, urshd.HINMEI_CD, SqlInt16.Parse(dataRow["DTL_EDABAN"].ToString()));
                                                if (uketukeHinmeiDenyouKbnEdaban.Rows.Count > 0)
                                                {
                                                    // 伝票区分：支払以外は全て売上として扱う
                                                    if (CommonConst.DENPYOU_KBN_SHIHARAI == Int16.Parse(uketukeHinmeiDenyouKbnEdaban.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                                                    {
                                                        // 伝票区分：支払
                                                        urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_SHIHARAI;
                                                    }
                                                    else
                                                    {
                                                        // 伝票区分：売上
                                                        urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                                                    }
                                                }

                                            }

                                            // 数量
                                            if (false == string.IsNullOrEmpty(dataRow["GENBA_DETAIL_SUURYO1"].ToString()))
                                            {
                                                urshd.SUURYOU = decimal.Parse(dataRow["GENBA_DETAIL_SUURYO1"].ToString());
                                            }

                                            // 単位CD
                                            if (false == string.IsNullOrEmpty(dataRow["GENBA_DETAIL_UNIT_CD1"].ToString()))
                                            {
                                                urshd.UNIT_CD = Int16.Parse(dataRow["GENBA_DETAIL_UNIT_CD1"].ToString());
                                            }
                                            SqlDecimal kingaku = SqlDecimal.Null;

                                            if (row["UKETSUKE_KBN"].Value.ToString().Equals("1"))
                                            {
                                                // 受付番号に紐付く受付収集伝票が存在する場合
                                                if (!string.IsNullOrEmpty(dataRow["SYSTEM_ID_TUSE"].ToString()))
                                                {
                                                    // 受付番号と品名CD、単位CD、伝票区分CDをキーに受付(収集)明細テーブルから単価を取得
                                                    if ((false == urshe.UKETSUKE_NUMBER.IsNull) &&
                                                       (false == urshd.ROW_NO.IsNull) &&
                                                       (false == urshd.HINMEI_CD.Equals(string.Empty)) &&
                                                       (false == urshd.UNIT_CD.IsNull) &&
                                                       (false == urshd.DENPYOU_KBN_CD.IsNull))
                                                    {
                                                        UketsukeSsDTOClass findDTO = new UketsukeSsDTOClass();
                                                        findDTO.SYSTEM_ID = Int64.Parse(dataRow["SYSTEM_ID_TUSE"].ToString());
                                                        findDTO.SEQ = Int32.Parse(dataRow["SEQ_TUSE"].ToString());
                                                        findDTO.UKETSUKE_NUMBER = (Int64)urshe.UKETSUKE_NUMBER;
                                                        findDTO.ROW_NO = urshd.ROW_NO.Value;
                                                        findDTO.HINMEI_CD = urshd.HINMEI_CD;
                                                        findDTO.UNIT_CD = (Int16)urshd.UNIT_CD;
                                                        urshd.TANKA = this.GetTanka((Int16)urshd.DENPYOU_KBN_CD, findDTO, out kingaku);
                                                    }
                                                }
                                                else
                                                {
                                                    // 伝票登録情報をキーに単価を取得
                                                    if ((false == string.IsNullOrEmpty(urshd.HINMEI_CD)) &&
                                                       (false == urshd.UNIT_CD.IsNull) &&
                                                       (false == urshd.DENPYOU_KBN_CD.IsNull))
                                                    {
                                                        urshd.TANKA = this.GetTanka((Int16)urshd.DENPYOU_KBN_CD,
                                                                                                         urshe,
                                                                                                         urshd.HINMEI_CD,
                                                                                                         (Int16)urshd.UNIT_CD);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // 受付番号に紐付く受付出荷伝票が存在する場合
                                                if (!string.IsNullOrEmpty(dataRow["SYSTEM_ID_TUSE"].ToString()))
                                                {
                                                    // 受付番号と品名CD、単位CD、伝票区分CDをキーに受付(出荷)明細テーブルから単価を取得
                                                    if ((false == urshe.UKETSUKE_NUMBER.IsNull) &&
                                                       (false == urshd.ROW_NO.IsNull) &&
                                                       (false == urshd.HINMEI_CD.Equals(string.Empty)) &&
                                                       (false == urshd.UNIT_CD.IsNull) &&
                                                       (false == urshd.DENPYOU_KBN_CD.IsNull))
                                                    {
                                                        UketsukeSkDTOClass findDTO = new UketsukeSkDTOClass();
                                                        findDTO.SYSTEM_ID = Int64.Parse(dataRow["SYSTEM_ID_TUSE"].ToString());
                                                        findDTO.SEQ = Int32.Parse(dataRow["SEQ_TUSE"].ToString());
                                                        findDTO.UKETSUKE_NUMBER = (Int64)urshe.UKETSUKE_NUMBER;
                                                        findDTO.ROW_NO = urshd.ROW_NO.Value;
                                                        findDTO.HINMEI_CD = urshd.HINMEI_CD;
                                                        findDTO.UNIT_CD = (Int16)urshd.UNIT_CD;
                                                        urshd.TANKA = this.GetTanka((Int16)urshd.DENPYOU_KBN_CD, findDTO, out kingaku);
                                                    }
                                                }
                                                else
                                                {
                                                    // 伝票登録情報をキーに単価を取得
                                                    if ((false == string.IsNullOrEmpty(urshd.HINMEI_CD)) &&
                                                       (false == urshd.UNIT_CD.IsNull) &&
                                                       (false == urshd.DENPYOU_KBN_CD.IsNull))
                                                    {
                                                        urshd.TANKA = this.GetTanka((Int16)urshd.DENPYOU_KBN_CD,
                                                                                                         urshe,
                                                                                                         urshd.HINMEI_CD,
                                                                                                         (Int16)urshd.UNIT_CD);
                                                    }
                                                }
                                            }
                                            
                                            urshd.MEISAI_BIKOU = string.Empty;
                                            // 数量
                                            if (false == string.IsNullOrEmpty(dataRow["GENBA_DETAIL_SUURYO2"].ToString()))
                                            {
                                                urshd.NISUGATA_SUURYOU = decimal.Parse(dataRow["GENBA_DETAIL_SUURYO2"].ToString());
                                            }

                                            // 単位CD
                                            if (false == string.IsNullOrEmpty(dataRow["GENBA_DETAIL_UNIT_CD2"].ToString()))
                                            {
                                                urshd.NISUGATA_UNIT_CD = Int16.Parse(dataRow["GENBA_DETAIL_UNIT_CD2"].ToString());
                                            }

                                            // 伝票区分によった消費税端数CDの格納
                                            Int16 taxHasuCD = 0;
                                            Int16 kinHasuCD = 0;
                                            if (false == urshd.DENPYOU_KBN_CD.IsNull)
                                            {
                                                if (CommonConst.DENPYOU_KBN_SHIHARAI == urshd.DENPYOU_KBN_CD)
                                                {
                                                    if (torihikiShiharaiInfo != null)
                                                    {
                                                        // 取引先支払情報より端数CDの取得
                                                        taxHasuCD = (Int16)torihikiShiharaiInfo.TAX_HASUU_CD;
                                                        kinHasuCD = (Int16)torihikiShiharaiInfo.KINGAKU_HASUU_CD;
                                                    }
                                                }
                                                else
                                                {
                                                    if (torihikiSeikyuInfo != null)
                                                    {
                                                        // 取引先請求情報より端数CDの取得
                                                        taxHasuCD = (Int16)torihikiSeikyuInfo.TAX_HASUU_CD;
                                                        kinHasuCD = (Int16)torihikiSeikyuInfo.KINGAKU_HASUU_CD;
                                                    }
                                                }
                                            }

                                            // 明細金額再計算
                                            this.DetailCalc(urshe, urshd, taxHasuCD, kinHasuCD, kingaku);

                                            // 売上_支払明細テーブル登録
                                            entitysUrShDetailList.Add(urshd);
                                            tempUrShDetail.Add(urshd);
                                        }
                                        #endregion

                                        // 総計取得
                                        this.GetMoneyTotal(urshe, tempUrShDetail);

                                        // 端数CDの取得
                                        Int16 shiTaxHasuCD = 0;
                                        Int16 uriTaxHasuCD = 0;
                                        if (torihikiShiharaiInfo != null)
                                        {
                                            // 取引先支払情報より端数CDの取得
                                            shiTaxHasuCD = (Int16)torihikiShiharaiInfo.TAX_HASUU_CD;
                                        }
                                        if (torihikiSeikyuInfo != null)
                                        {
                                            // 取引先請求情報より端数CDの取得
                                            uriTaxHasuCD = (Int16)torihikiSeikyuInfo.TAX_HASUU_CD;
                                        }

                                        // 伝票消費税計算
                                        this.EntryTaxCalc(urshe, shiTaxHasuCD, "支払");
                                        this.EntryTaxCalc(urshe, uriTaxHasuCD, "売上");

                                        // 売上支払伝票登録
                                        var bindLogic = new DataBinderLogic<T_UR_SH_ENTRY>(urshe);
                                        bindLogic.SetSystemProperty(urshe, false);
                                        urshe.DELETE_FLG = false;
                                        // 自動設定
                                        var dataBinderContenaResultUs = new DataBinderLogic<T_UR_SH_ENTRY>(urshe);
                                        dataBinderContenaResultUs.SetSystemProperty(urshe, false);
                                        entitysUrShEntryList.Add(urshe);
                                    }
                                }
                                #endregion
                            }

                        }
                        #endregion
                    }
                    
                }
                else
                {
                    #region 登録系配車明細Entityを作成
                    if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                    {
                        this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                        this.entitysMobisyoRtCTNList = new List<T_MOBISYO_RT_CONTENA>();
                        this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                        this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();
                        int ZenHaishaNo = -1;
                        int HaishaNo;
                        int ZenHaishaRowNo = -1;
                        int HaishaRowNo;
                        foreach (var row in this.form.Ichiran.Rows)
                        {
                            if ((bool)row[0].Value)
                            {
                                // 選択行データ
                                DataRow tableRow = this.ResultTable.Rows[int.Parse(row["ROW_NO"].Value.ToString()) - 1];
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
                                    this.entitysMobisyoRt.SEQ_NO = this.CreateSeqNo();

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
                                    this.entitysMobisyoRt.GENBA_JISSEKI_CREATEDATE = parentForm.sysDate;
                                    // 修正日時 Insertした日次
                                    this.entitysMobisyoRt.GENBA_JISSEKI_UPDATEDATE = parentForm.sysDate;
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
                        }

                        int ZenHaishaNo2 = -1;
                        int HaishaNo2;
                        int ZenHaishaRowNo2 = -1;
                        int HaishaRowNo2;
                        string NiorosiNo2;
                        int Edaban2 = 0;
                        List<NiorosiClass> niorosiList = new List<NiorosiClass>();
                        foreach (var row in this.form.Ichiran.Rows)
                        {
                            if ((bool)row[0].Value)
                            {
                                // 選択行データ
                                DataRow tableRow = this.ResultTable.Rows[int.Parse(row["ROW_NO"].Value.ToString()) - 1];
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
                                    DataTable niorosiTable = this.TeikihaishaDao.GetMobilNioroshiData(HaishaNo2, int.Parse(NiorosiNo2));
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
                                        this.entitysMobisyoRtDTL.HANYU_SEQ_NO = this.CreateSeqNo();
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

                                        DataTable NioroshiData = this.TeikihaishaDao.GetTeikiHaishaNioroshiData(SYSTEM_ID, SEQ, NIOROSHI_NUMBER);

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
                    }
                    #endregion

                    #region 登録系受付明細Entityを作成
                    else
                    {
                        this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                        this.entitysMobisyoRtCTNList = new List<T_MOBISYO_RT_CONTENA>();
                        this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                        this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();

                        foreach (var row in this.form.Ichiran.Rows)
                        {
                            if ((bool)row[0].Value)
                            {
                                // 選択行データ
                                DataRow tableRow = this.ResultTable.Rows[int.Parse(row["ROW_NO"].Value.ToString()) - 1];

                                // 20170601 wangjm モバイル将軍#105481 start
                                this.uketsukeKbn = tableRow["UKETSUKE_KBN"].ToString();
                                // 20170601 wangjm モバイル将軍#105481 end

                                #region T_MOBISYO_RT
                                // entitys作成
                                this.entitysMobisyoRt = new T_MOBISYO_RT();
                                // シーケンシャルナンバー
                                this.entitysMobisyoRt.SEQ_NO = this.CreateSeqNo();

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
                                if (!SqlDateTime.Parse(tableRow["SAGYOU_DATE"].ToString()).IsNull)
                                {
                                    this.entitysMobisyoRt.HAISHA_SAGYOU_DATE = SqlDateTime.Parse(tableRow["SAGYOU_DATE"].ToString());
                                }
                                // (配車)伝票番号
                                this.entitysMobisyoRt.HAISHA_DENPYOU_NO = SqlInt64.Parse(tableRow["UKETSUKE_NUMBER"].ToString());
                                // (配車)コース名称CD BLANK
                                this.entitysMobisyoRt.HAISHA_COURSE_NAME_CD = string.Empty;
                                // (配車)コース名称 BLANK
                                this.entitysMobisyoRt.HAISHA_COURSE_NAME = string.Empty;
                                // (配車)配車区分 1
                                this.entitysMobisyoRt.HAISHA_KBN = 1;
                                // 登録日時 Insertした日次
                                this.entitysMobisyoRt.GENBA_JISSEKI_CREATEDATE = parentForm.sysDate;
                                // 修正日時 Insertした日次
                                this.entitysMobisyoRt.GENBA_JISSEKI_UPDATEDATE = parentForm.sysDate;
                                // 業者CD
                                if (!string.IsNullOrEmpty(tableRow["GYOUSHA_CD"].ToString()))
                                {
                                    this.entitysMobisyoRt.GENBA_JISSEKI_GYOUSHACD = tableRow["GYOUSHA_CD"].ToString();
                                }
                                // 現場CD
                                if (!string.IsNullOrEmpty(tableRow["GENBA_CD"].ToString()))
                                {
                                    this.entitysMobisyoRt.GENBA_JISSEKI_GENBACD = tableRow["GENBA_CD"].ToString();
                                }
                                // 運搬業者CD
                                if (!string.IsNullOrEmpty(tableRow["UNPAN_GYOUSHA_CD"].ToString()))
                                {
                                    this.entitysMobisyoRt.GENBA_JISSEKI_UPNGYOSHACD = tableRow["UNPAN_GYOUSHA_CD"].ToString();
                                }
                                else
                                {
                                    this.entitysMobisyoRt.GENBA_JISSEKI_UPNGYOSHACD = string.Empty;
                                }
                                // 追加現場フラグ 基本的には0。データを登録するとき、作業日＝当日の場合、1
                                if (!SqlDateTime.Parse(tableRow["SAGYOU_DATE"].ToString()).IsNull &&
                                    (DateTime.Parse(tableRow["SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd") == (parentForm.sysDate).ToString("yyyy/MM/dd")))
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
                                // (配車)行番号
                                this.entitysMobisyoRt.HAISHA_ROW_NUMBER = 1;

                                // 削除フラグ
                                this.entitysMobisyoRt.DELETE_FLG = false;

                                // 20170601 wangjm モバイル将軍#105481 start
                                this.entitysMobisyoRt.GENCHAKU_TIME_NAME = tableRow["GENCHAKU_TIME_NAME"].ToString();

                                if(!String.IsNullOrEmpty(tableRow["GENCHAKU_TIME"].ToString()))
                                {
                                    this.entitysMobisyoRt.GENCHAKU_TIME = tableRow["GENCHAKU_TIME"].ToString();
                                }
                                
                                this.entitysMobisyoRt.GENBA_TEL = tableRow["GENBA_TEL"].ToString();
                                this.entitysMobisyoRt.GENBA_TANTOU_NAME = tableRow["TANTOSHA_NAME"].ToString();
                                this.entitysMobisyoRt.GENBA_TANTOU_TEL = tableRow["TANTOSHA_TEL"].ToString();
                                this.entitysMobisyoRt.GENBA_EIGYOU_TANTOU = tableRow["EIGYOU_TANTOUSHA_NAME"].ToString();
                                this.entitysMobisyoRt.UKETSUKE_BIKOU1 = tableRow["UKETSUKE_BIKOU1"].ToString();
                                this.entitysMobisyoRt.UKETSUKE_BIKOU2 = tableRow["UKETSUKE_BIKOU2"].ToString();
                                this.entitysMobisyoRt.UKETSUKE_BIKOU3 = tableRow["UKETSUKE_BIKOU3"].ToString();
                                this.entitysMobisyoRt.UKETSUKE_SIJIJIKOU1 = tableRow["UNTENSHA_SIJIJIKOU1"].ToString();
                                this.entitysMobisyoRt.UKETSUKE_SIJIJIKOU2 = tableRow["UNTENSHA_SIJIJIKOU2"].ToString();
                                this.entitysMobisyoRt.UKETSUKE_SIJIJIKOU3 = tableRow["UNTENSHA_SIJIJIKOU3"].ToString();
                                this.entitysMobisyoRt.UKETSUKE_KBN = SqlInt16.Parse(tableRow["UKETSUKE_KBN"].ToString());
                                // 20170601 wangjm モバイル将軍#105481 end

                                // 自動設定
                                var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT>(this.entitysMobisyoRt);
                                dataBinderContenaResult.SetSystemProperty(this.entitysMobisyoRt, false);

                                // Listに追加
                                this.entitysMobisyoRtList.Add(this.entitysMobisyoRt);
                                #endregion

                                #region T_MOBISYO_RT_CONTENA

                                SqlInt64 SYSTEM_ID = SqlInt64.Parse(tableRow["SYSTEM_ID"].ToString());
                                SqlInt32 SEQ = SqlInt32.Parse(tableRow["SEQ"].ToString());

                                if(this.uketsukeKbn.Equals("1"))
                                {                                    
                                    DataTable ContenaData = this.TeikihaishaDao.GetSyuusyuuUketukeContenaData(SYSTEM_ID, SEQ);
                                    foreach (DataRow ctnRow in ContenaData.Rows)
                                    {
                                        this.entitysMobisyoRtCTN = new T_MOBISYO_RT_CONTENA();
                                        // シーケンシャルナンバー
                                        this.entitysMobisyoRtCTN.SEQ_NO = this.entitysMobisyoRt.SEQ_NO;
                                        // コンテナシーケンシャルナンバー 伝種区分125で最新のID + 1を採番
                                        this.entitysMobisyoRtCTN.CONTENA_SEQ_NO = this.CreateSeqNo();
                                        // 予実フラグ 初期値１（予定）
                                        this.entitysMobisyoRtCTN.JISSEKI_FLG = 1;
                                        // 設置引揚区分
                                        if (!string.IsNullOrEmpty(ctnRow["CONTENA_SET_KBN"].ToString()))
                                        {
                                            this.entitysMobisyoRtCTN.CONTENA_SET_KBN = SqlInt32.Parse(ctnRow["CONTENA_SET_KBN"].ToString());
                                        }
                                        else
                                        {
                                            this.entitysMobisyoRtCTN.CONTENA_SET_KBN = 9;
                                        }
                                        // コンテナ種類CD
                                        if (!string.IsNullOrEmpty(ctnRow["CONTENA_SHURUI_CD"].ToString()))
                                        {
                                            this.entitysMobisyoRtCTN.CONTENA_SHURUI_CD = ctnRow["CONTENA_SHURUI_CD"].ToString();
                                        }
                                        else
                                        {
                                            this.entitysMobisyoRtCTN.CONTENA_SHURUI_CD = "";
                                        }
                                        // コンテナCD
                                        if (!string.IsNullOrEmpty(ctnRow["CONTENA_CD"].ToString()))
                                        {
                                            this.entitysMobisyoRtCTN.CONTENA_CD = ctnRow["CONTENA_CD"].ToString();
                                        }
                                        else
                                        {
                                            this.entitysMobisyoRtCTN.CONTENA_CD = "";
                                        }
                                        // 台数
                                        if (!string.IsNullOrEmpty(ctnRow["DAISUU_CNT"].ToString()))
                                        {
                                            this.entitysMobisyoRtCTN.DAISUU_CNT = SqlInt16.Parse(ctnRow["DAISUU_CNT"].ToString());
                                        }
                                        // 削除フラグ 初期値０
                                        this.entitysMobisyoRtCTN.DELETE_FLG = false;

                                        // 自動設定
                                        var dataBinderContenaResultDTL = new DataBinderLogic<T_MOBISYO_RT_CONTENA>(this.entitysMobisyoRtCTN);
                                        dataBinderContenaResultDTL.SetSystemProperty(this.entitysMobisyoRtCTN, false);

                                        // Listに追加
                                        this.entitysMobisyoRtCTNList.Add(this.entitysMobisyoRtCTN);

                                    }
                                }
                                
                                #endregion

                                if (this.uketsukeKbn.Equals("1"))
                                {
                                    SqlInt64 HANYU_SEQ_NO = -1;
                                    DataTable SyuusyuuUketukeDetailData = this.TeikihaishaDao.GetSyuusyuuUketukeDetailData(SYSTEM_ID, SEQ);
                                    if (!string.IsNullOrEmpty(SyuusyuuUketukeDetailData.Rows[0]["HANNYUU_GENBACD"].ToString()))
                                    {
                                        HANYU_SEQ_NO = this.CreateSeqNo();
                                    }

                                    foreach (DataRow dtlRow in SyuusyuuUketukeDetailData.Rows)
                                    {
                                        #region T_MOBISYO_RT_DTL
                                        // entitys作成
                                        this.entitysMobisyoRtDTL = new T_MOBISYO_RT_DTL();
                                        // シーケンシャルナンバー
                                        this.entitysMobisyoRtDTL.SEQ_NO = this.entitysMobisyoRt.SEQ_NO;
                                        // 搬入シーケンシャルナンバー
                                        if (HANYU_SEQ_NO > 0)
                                        {
                                            this.entitysMobisyoRtDTL.HANYU_SEQ_NO = HANYU_SEQ_NO;
                                        }
                                        // 枝番
                                        this.entitysMobisyoRtDTL.EDABAN = SqlInt64.Parse(dtlRow["ROW_NO"].ToString());
                                        // (現場明細)品名CD
                                        if (!string.IsNullOrEmpty(dtlRow["GENBA_DETAIL_HINMEICD"].ToString()))
                                        {
                                            this.entitysMobisyoRtDTL.GENBA_DETAIL_HINMEICD = dtlRow["GENBA_DETAIL_HINMEICD"].ToString();
                                        }
                                        // (現場明細)数量１
                                        this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO1 = SqlDouble.Null;
                                        // (現場明細)数量２
                                        this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO2 = SqlDouble.Null;
                                        // (現場明細)単位１
                                        if (!string.IsNullOrEmpty(dtlRow["GENBA_DETAIL_UNIT_CD1"].ToString()))
                                        {
                                            this.entitysMobisyoRtDTL.GENBA_DETAIL_UNIT_CD1 = SqlInt16.Parse(dtlRow["GENBA_DETAIL_UNIT_CD1"].ToString());
                                        }
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

                                    #region T_MOBISYO_RT_HANNYUU
                                    // entitys作成
                                    this.entitysMobisyoRtHN = new T_MOBISYO_RT_HANNYUU();

                                    // 搬入シーケンシャルナンバー
                                    this.entitysMobisyoRtHN.HANYU_SEQ_NO = HANYU_SEQ_NO;
                                    // 枝番1を設定する
                                    this.entitysMobisyoRtHN.EDABAN = 1;

                                    // (搬入)業者CD
                                    if (!string.IsNullOrEmpty(SyuusyuuUketukeDetailData.Rows[0]["HANNYUU_GYOUSHACD"].ToString()))
                                    {
                                        this.entitysMobisyoRtHN.HANNYUU_GYOUSHACD = SyuusyuuUketukeDetailData.Rows[0]["HANNYUU_GYOUSHACD"].ToString();
                                    }

                                    // (搬入)現場CD
                                    if (!string.IsNullOrEmpty(SyuusyuuUketukeDetailData.Rows[0]["HANNYUU_GENBACD"].ToString()))
                                    {
                                        this.entitysMobisyoRtHN.HANNYUU_GENBACD = SyuusyuuUketukeDetailData.Rows[0]["HANNYUU_GENBACD"].ToString();
                                    }
                                    else
                                    {
                                        continue;
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
                                else
                                {
                                    DataTable ShukkaUketukeDetailData = this.TeikihaishaDao.GetShukkaUketukeDetailData(SYSTEM_ID, SEQ);
                                    foreach (DataRow dtlRow in ShukkaUketukeDetailData.Rows)
                                    {
                                        #region T_MOBISYO_RT_DTL
                                        // entitys作成
                                        this.entitysMobisyoRtDTL = new T_MOBISYO_RT_DTL();
                                        // シーケンシャルナンバー
                                        this.entitysMobisyoRtDTL.SEQ_NO = this.entitysMobisyoRt.SEQ_NO;                                       
                                        // 枝番
                                        this.entitysMobisyoRtDTL.EDABAN = SqlInt64.Parse(dtlRow["ROW_NO"].ToString());
                                        // (現場明細)品名CD
                                        if (!string.IsNullOrEmpty(dtlRow["GENBA_DETAIL_HINMEICD"].ToString()))
                                        {
                                            this.entitysMobisyoRtDTL.GENBA_DETAIL_HINMEICD = dtlRow["GENBA_DETAIL_HINMEICD"].ToString();
                                        }
                                        // (現場明細)数量１
                                        this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO1 = SqlDouble.Null;
                                        // (現場明細)数量２
                                        this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO2 = SqlDouble.Null;
                                        // (現場明細)単位１
                                        if (!string.IsNullOrEmpty(dtlRow["GENBA_DETAIL_UNIT_CD1"].ToString()))
                                        {
                                            this.entitysMobisyoRtDTL.GENBA_DETAIL_UNIT_CD1 = SqlInt16.Parse(dtlRow["GENBA_DETAIL_UNIT_CD1"].ToString());
                                        }
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
                                
                            }

                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
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
        /// シーケンシャルナンバー採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 CreateSeqNo()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = ConstCls.MOBILE_RENKEI;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = ConstCls.MOBILE_RENKEI;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 定期実績システムID採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int createSystemIdForTeikiJisseki(int addCount = 0)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 戻り値を初期化
                int returnInt = 1;

                // 処理区分：130（定期実績）
                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_JISSEKI;

                // 処理区分をもとに削除されていないシステムID採番のデータを取得する
                var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
                // システムIDの最大値+1を取得する
                returnInt = this.numberSystemDao.GetMaxPlusKey(entity);
                if (addCount != 0)
                {
                    //指定数分追加して先行でSYSTEM_IDの採番を行う
                    returnInt = returnInt + addCount;
                }

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_JISSEKI;
                    updateEntity.CURRENT_NUMBER = returnInt;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberSystemDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnInt;
                    this.numberSystemDao.Update(updateEntity);
                }
                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanSystemId", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期実績番号採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int createTeikiJissekiNumber()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 戻り値を初期化
                int returnInt = -1;

                // 処理区分：130（定期実績）
                var entity = new S_NUMBER_DENSHU();
                entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_JISSEKI;

                // 処理区分をもとに削除されていない伝種採番のデータを取得する
                var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
                // 伝種連番の最大値+1を取得する
                returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_DENSHU();
                    updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_JISSEKI;
                    updateEntity.CURRENT_NUMBER = returnInt;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberDenshuDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnInt;
                    this.numberDenshuDao.Update(updateEntity);
                }
                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanSystemId", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 受入入力用のSYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemIdForUrsh()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.URIAGE_SHIHARAI;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.URIAGE_SHIHARAI;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 売上支払番号採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public SqlInt64 createUrshNumber()
        {
            SqlInt64 returnInt = -1;

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.URIAGE_SHIHARAI;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.URIAGE_SHIHARAI;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberDenshuDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberDenshuDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 日連番取得
        /// </summary>
        /// <param name="denpyouDate">伝票日付</param>
        /// <param name="kyotenCD">拠点CD</param>
        /// <returns name="int">日連番</returns>
        /// <remarks>日連番を取得し、連番管理Tableの更新を行う</remarks>
        private int GetDateNum(DateTime denpyouDate, Int16 kyotenCD)
        {
            byte[] numberDayTimeStamp = null;
            int retNum = 1;
            // 日連番取得
            DataTable numberDays = null;
            numberDays = this.accessor.GetNumberDay(denpyouDate.Date, (Int16)r_framework.Const.DENSHU_KBN.URIAGE_SHIHARAI, kyotenCD);

            if ((numberDays == null) || (numberDays.Rows.Count == 0))
            {
                // 該当データが無い場合は初期値
                retNum = 1;
            }
            else
            {
                // 最新の番号に+1
                retNum = (int)numberDays.Rows[0]["CURRENT_NUMBER"] + 1;
                numberDayTimeStamp = (byte[])numberDays.Rows[0]["TIME_STAMP"];
            }

            // S_NUMBER_DAYテーブル情報セット
            var numberDay = new S_NUMBER_DAY();
            numberDay.NUMBERED_DAY = denpyouDate.Date;
            numberDay.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.URIAGE_SHIHARAI;
            numberDay.KYOTEN_CD = kyotenCD;
            numberDay.CURRENT_NUMBER = retNum;
            numberDay.DELETE_FLG = false;
            if (numberDayTimeStamp != null)
            {
                numberDay.TIME_STAMP = numberDayTimeStamp;
            }
            var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(numberDay);
            dataBinderNumberDay.SetSystemProperty(numberDay, false);

            // DBセット
            if (numberDay.CURRENT_NUMBER == 1)
            {
                // 初期値の場合はDB追加
                this.accessor.InsertNumberDay(numberDay);
            }
            else
            {
                // 初期値以降の場合はDB更新
                this.accessor.UpdateNumberDay(numberDay);
            }

            // 日連番を返却
            return retNum;
        }

        /// <summary>
        /// 年連番取得
        /// </summary>
        /// <param name="denpyouDate">伝票日付</param>
        /// <param name="kyotenCD">拠点CD</param>
        /// <returns name="int">年連番</returns>
        /// <remarks>年連番を取得し、連番管理Tableの更新を行う</remarks>
        private int GetYearNum(DateTime denpyouDate, Int16 kyotenCD)
        {
            byte[] numberYearTimeStamp = null;
            int retNum = 1;

            // 会社情報取得
            var corpInfo = new M_CORP_INFO();
            var corpInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();
            var corpInfos = corpInfoDao.GetAllData();
            if ((corpInfos != null) && (corpInfos.Length != 0))
            {
                corpInfo = corpInfos[0];
            }

            // 年連番取得(S_NUMBER_YEARテーブルから情報取得 + 年度の生成処理を追加)
            DataTable numberYeas = null;
            SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(denpyouDate.Date, (short)corpInfo.KISHU_MONTH);
            numberYeas = this.accessor.GetNumberYear(numberedYear, (Int16)r_framework.Const.DENSHU_KBN.URIAGE_SHIHARAI, kyotenCD);

            if (numberYeas == null || numberYeas.Rows.Count == 0)
            {
                // 該当データが無い場合は初期値
                retNum = 1;
            }
            else
            {
                // 最新の番号に+1
                retNum = (int)numberYeas.Rows[0]["CURRENT_NUMBER"] + 1;
                numberYearTimeStamp = (byte[])numberYeas.Rows[0]["TIME_STAMP"];
            }

            // S_NUMBER_YEARテーブル情報セット
            var numberYear = new S_NUMBER_YEAR();
            numberYear.NUMBERED_YEAR = numberedYear;
            numberYear.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.URIAGE_SHIHARAI;
            numberYear.KYOTEN_CD = kyotenCD;
            numberYear.CURRENT_NUMBER = retNum;
            numberYear.DELETE_FLG = false;
            if (numberYearTimeStamp != null)
            {
                numberYear.TIME_STAMP = numberYearTimeStamp;
            }
            var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(numberYear);
            dataBinderNumberYear.SetSystemProperty(numberYear, false);

            // DBセット
            if (numberYear.CURRENT_NUMBER == 1)
            {
                // 初期値の場合はDB追加
                this.accessor.InsertNumberYear(numberYear);
            }
            else
            {
                // 初期値以降の場合はDB更新
                this.accessor.UpdateNumberYear(numberYear);
            }

            // 年連番を返却
            return retNum;
        }

       

        /// <summary>
        /// 定期における品名詳細を取得する
        /// </summary>
        /// <param name="dto">検索条件</param>
        /// <returns name='M_GENBA_TEIKI_HINMEI'>定期品名詳細情報</returns>
        /// <remarks>
        /// 定期配車 > 現場定期品名 > 品名マスタ の順で参照する
        /// </remarks>
        internal M_GENBA_TEIKI_HINMEI getTeikiHinmeiInfo(MobileShougunTorikomiDTOClass dto)
        {
            var retEntity = new M_GENBA_TEIKI_HINMEI();
            bool anbunGetFlag = false;

            // 検索条件に紐付く、定期配車情報を取得
            DataTable table = this.TeikihaishaDao.GetTeikiHinmeiInfo(dto);
            if ((table != null) && (table.Rows.Count > 0))
            {
                // 伝票区分
                var denpyouKbnCD = table.Rows[0]["DENPYOU_KBN_CD"].ToString();
                if (false == string.IsNullOrEmpty(denpyouKbnCD))
                {
                    retEntity.DENPYOU_KBN_CD = Int16.Parse(denpyouKbnCD);
                }

                // 契約区分
                var keiyakuKbn = table.Rows[0]["KEIYAKU_KBN"].ToString();
                if (false == string.IsNullOrEmpty(keiyakuKbn))
                {
                    retEntity.KEIYAKU_KBN = Int16.Parse(keiyakuKbn);

                }

                // 按分フラグ
                anbunGetFlag = true;
                var anbunFlag = table.Rows[0]["ANBUN_FLG"].ToString();
                if (false == string.IsNullOrEmpty(anbunFlag))
                {
                    retEntity.ANBUN_FLG = Boolean.Parse(anbunFlag);
                }

                // 契約区分が単価の場合のみ計上区分をセットする
                if (retEntity.KEIYAKU_KBN == CommonConst.KEIYAKU_KBN_TANKA)
                {
                    // 計上区分(月極区分)
                    var keijyouKbn = table.Rows[0]["KEIJYOU_KBN"].ToString();
                    if (false == string.IsNullOrEmpty(keijyouKbn))
                    {
                        retEntity.KEIJYOU_KBN = Int16.Parse(keijyouKbn);
                    }
                }
            }
            else
            {
                // 定期配車情報が存在しなかった場合、現場定期品名から取得
                table = this.TeikihaishaDao.GetGenbaTeikiHinmeiDataForEntity(dto);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    if (table.Rows.Count == 1)
                    {
                        // 伝票区分
                        var denpyouKbnCD = table.Rows[0]["DENPYOU_KBN_CD"].ToString();
                        if (false == string.IsNullOrEmpty(denpyouKbnCD))
                        {
                            retEntity.DENPYOU_KBN_CD = Int16.Parse(denpyouKbnCD);
                        }
                    }
                    else
                    {
                        // 該当情報が複数件の場合は、1.売上をセット
                        retEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                    }

                    // 契約区分
                    var keiyakuKbn = table.Rows[0]["KEIYAKU_KBN"].ToString();
                    if (false == string.IsNullOrEmpty(keiyakuKbn))
                    {
                        retEntity.KEIYAKU_KBN = Int16.Parse(keiyakuKbn);

                    }

                    // 契約区分が単価の場合のみ計上区分をセットする
                    if (retEntity.KEIYAKU_KBN == CommonConst.KEIYAKU_KBN_TANKA)
                    {
                        // 計上区分(月極区分)
                        var keijyouKbn = table.Rows[0]["KEIJYOU_KBN"].ToString();
                        if (false == string.IsNullOrEmpty(keijyouKbn))
                        {
                            retEntity.KEIJYOU_KBN = Int16.Parse(keijyouKbn);
                        }
                    }

                    // 按分フラグ
                    anbunGetFlag = true;
                    var anbunFlag = table.Rows[0]["ANBUN_FLG"].ToString();
                    if (false == string.IsNullOrEmpty(anbunFlag))
                    {
                        retEntity.ANBUN_FLG = Boolean.Parse(anbunFlag);
                    }
                }
                else
                {
                    // 現場定期品名情報が存在しなかった場合、品名マスタから取得
                    table = this.TeikihaishaDao.GetHinmeiDataForEntity(dto);
                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        // 伝票区分
                        var denpyouKbnCD = table.Rows[0]["DENPYOU_KBN_CD"].ToString();
                        if (false == string.IsNullOrEmpty(denpyouKbnCD))
                        {
                            var cd = Int16.Parse(denpyouKbnCD);
                            if (cd == CommonConst.DENPYOU_KBN_KYOUTSUU)
                            {
                                // 9.共通の場合は、1.売上をセット
                                retEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                            }
                            else
                            {
                                retEntity.DENPYOU_KBN_CD = cd;
                            }
                        }

                        // 契約区分は単価をセット
                        retEntity.KEIYAKU_KBN = CommonConst.KEIYAKU_KBN_TANKA;

                        // 計上区分は伝票をセット
                        retEntity.KEIJYOU_KBN = CommonConst.KEIJYOU_KBN_DENPYOU;
                    }
                }
            }

            if ((table == null) || (table.Rows.Count <= 0))
            {
                // 該当情報が無ければ、デフォルト値をセット

                // 伝票区分は売上をセット
                retEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;

                // 契約区分は単価をセット
                retEntity.KEIYAKU_KBN = CommonConst.KEIYAKU_KBN_TANKA;

                // 計上区分は伝票をセット
                retEntity.KEIJYOU_KBN = CommonConst.KEIJYOU_KBN_DENPYOU;
            }

            // 按分フラグは現場定期品名から取得する
            if (anbunGetFlag == false)
            {
                table = this.TeikihaishaDao.GetGenbaTeikiHinmeiDataForEntity(dto);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    var anbunFlag = table.Rows[0]["ANBUN_FLG"].ToString();
                    if (false == string.IsNullOrEmpty(anbunFlag))
                    {
                        retEntity.ANBUN_FLG = Boolean.Parse(anbunFlag);
                    }
                }
            }

            return retEntity;
        }

        /// <summary>
        /// 定期における現場品名入力区分を取得する
        /// </summary>
        /// <param name="dto">検索条件</param>
        /// <returns name='SqlInt16'>定期品名詳細情報</returns>
        /// <remarks>
        /// 定期配車 > 現場定期品名
        /// </remarks>
        internal SqlInt16 getGenbaTeikiHinmeiInfo(MobileShougunTorikomiDTOClass dto)
        {
            SqlInt16 ret = ConstCls.INPUT_KBN_1;

            // 定期配車情報が存在しなかった場合、現場定期品名から取得
            var table = this.TeikihaishaDao.GetGenbaTeikiHinmeiDataForEntity(dto);
            if ((table != null) && (table.Rows.Count > 0))
            {
                ret = ConstCls.INPUT_KBN_2;
            }
            else
            {
                ret = ConstCls.INPUT_KBN_1;
            }

            return ret;
        }

        /// <summary>換算値を取得する</summary>
        /// <param name="data">登録しようとしている実績明細情報</param>
        /// <param name="teikiHaishaNumber">定期配車番号</param>
        /// <returns name="DataTable">条件にヒットした換算値&換算単位を格納したEntity</returns>
        /// <remarks>該当するものが無い場合はnullを返却</remarks>
        private T_TEIKI_JISSEKI_DETAIL GetKansanData(T_TEIKI_JISSEKI_DETAIL data, string teikiHaishaNumber)
        {
            // 単位CD:[Kg]
            const string unitCdKg = "3";
            var returnData = new T_TEIKI_JISSEKI_DETAIL();

            // 要記入フラグ
            if (data.KANSAN_UNIT_MOBILE_OUTPUT_FLG.IsNull)
            {
                returnData.KANSAN_UNIT_MOBILE_OUTPUT_FLG = SqlBoolean.False;
            }
            else
            {
                returnData.KANSAN_UNIT_MOBILE_OUTPUT_FLG = data.KANSAN_UNIT_MOBILE_OUTPUT_FLG;
            }

            // 取込データに換算後単位があればそれを使用（ここでは処理する必要なし）
            // 回収品名詳細に換算後単位があればそれを使用
            // 現場定期品名に換算後単位があればそれを使用

            // ① 他の単位をKgへ換算の場合
            // ② Kgを他の単位へ換算の場合
            // ③ Kg→Kgへの単位換算の場合
            //《公式》:[換算値] × [数量] = [換算後数量]
            // 数量が未入力の場合は換算後数量の計算を行わない

            // 現場定期品名から取得する
            var genbaTeikiEntity = this.TeikihaishaDao.GetGenbaTeikiHinmeiDataForEntity(new MobileShougunTorikomiDTOClass()
            {
                GYOUSHA_CD = data.GYOUSHA_CD,
                GENBA_CD = data.GENBA_CD,
                HINMEI_CD = data.HINMEI_CD,
                UNIT_CD = data.UNIT_CD,
                DENPYOU_KBN_CD = data.DENPYOU_KBN_CD
            });
            if (genbaTeikiEntity.Rows.Count > 0)
            {
                var kansanUnitCd = genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSAN_UNIT_CD")];
                if (null != kansanUnitCd && !String.IsNullOrEmpty(kansanUnitCd.ToString()))
                {
                    returnData.KANSAN_UNIT_CD = SqlInt16.Parse(kansanUnitCd.ToString());
                    if (false == data.SUURYOU.IsNull
                        && (null != genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSANCHI")] && !String.IsNullOrEmpty(genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSANCHI")].ToString()))
                        && (unitCdKg == returnData.KANSAN_UNIT_CD.ToString() || unitCdKg == data.UNIT_CD.ToString())
                        )
                    {
                        returnData.KANSAN_SUURYOU = SqlDecimal.Parse(genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSANCHI")].ToString()) * data.SUURYOU;
                    }
                }
            }

            // 回収品名詳細から取得する
            var dtKansanData = this.TeikihaishaDao.GetKansanData(new MobileShougunTorikomiDTOClass()
            {
                HAISHA_DENPYOU_NO = Int32.Parse(teikiHaishaNumber),
                GYOUSHA_CD = data.GYOUSHA_CD,
                GENBA_CD = data.GENBA_CD,
                HINMEI_CD = data.HINMEI_CD,
                UNIT_CD = data.UNIT_CD,
                DENPYOU_KBN_CD = data.DENPYOU_KBN_CD
            });
            if (dtKansanData.Rows.Count > 0)
            {
                var kansanUnitCd = dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSAN_UNIT_CD")];
                if (null != kansanUnitCd && !String.IsNullOrEmpty(kansanUnitCd.ToString()))
                {
                    returnData.KANSAN_UNIT_CD = SqlInt16.Parse(kansanUnitCd.ToString());
                    if (false == data.SUURYOU.IsNull
                        && (null != dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSANCHI")] && !String.IsNullOrEmpty(dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSANCHI")].ToString()))
                        && (unitCdKg == returnData.KANSAN_UNIT_CD.ToString() || unitCdKg == data.UNIT_CD.ToString())
                        )
                    {
                        returnData.KANSAN_SUURYOU = SqlDecimal.Parse(dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSANCHI")].ToString()) * data.SUURYOU;
                    }
                }
            }

            if (false == data.KANSAN_UNIT_CD.IsNull)
            {
                returnData.KANSAN_UNIT_CD = data.KANSAN_UNIT_CD;
            }

            return returnData;
        }

        /// <summary>
        /// 単価取得処理
        /// </summary>
        /// <param name="kbnCD">伝票区分CD</param>
        /// <param name="entryEntity">Entry伝票</param>
        /// <param name="hinmeiCD">品名CD</param>
        /// <param name="unitCD">単位CD</param>
        /// <returns name="decimal">単価</returns>
        /// <remarks>対象項目を直接指定し単価を取得する。個別品名単価⇒基本品名単価の順で取得する</remarks>
        internal decimal GetTanka(Int16 kbnCD, T_UR_SH_ENTRY entryEntity, string hinmeiCD, Int16 unitCD)
        {
            var CommonDBAccessor = new Common.BusinessCommon.DBAccessor();
            decimal tanka = 0;

            // 個別品名単価から取得
            var kobetsuEntity = CommonDBAccessor.GetKobetsuhinmeiTanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                kbnCD,
                                                                entryEntity.TORIHIKISAKI_CD,
                                                                entryEntity.GYOUSHA_CD,
                                                                entryEntity.GENBA_CD,
                                                                entryEntity.UNPAN_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GENBA_CD,
                                                                hinmeiCD,
                                                                unitCD);
            if (kobetsuEntity != null)
            {
                // 単価をセット
                if (false == string.IsNullOrEmpty(kobetsuEntity.TANKA.ToString()))
                {
                    tanka = decimal.Parse(kobetsuEntity.TANKA.ToString());
                }
            }
            else
            {
                // 基本品名単価から取得
                var kihonEntity = CommonDBAccessor.GetKihonHinmeitanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                kbnCD,
                                                                entryEntity.UNPAN_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GENBA_CD,
                                                                hinmeiCD,
                                                                unitCD);
                if (kihonEntity != null)
                {
                    // 単価をセット
                    if (false == string.IsNullOrEmpty(kihonEntity.TANKA.ToString()))
                    {
                        tanka = decimal.Parse(kihonEntity.TANKA.ToString());
                    }
                }
                else
                {
                    // 登録情報がない場合は0
                    tanka = 0;
                }
            }

            return tanka;
        }

        /// <summary>
        /// 単価取得処理
        /// </summary>
        /// <param name="kbnCD">伝票区分CD</param>
        /// <param name="findDTO">検索指定項目</param>
        /// <returns name="decimal">単価</returns>
        /// <remarks>検索項目に該当する単価を取得する。受付明細⇒個別品名単価⇒基本品名単価の順で取得する</remarks>
        internal SqlDecimal GetTanka(Int16 kbnCD, UketsukeSsDTOClass findDTO, out SqlDecimal kingaku)
        {
            var table = new DataTable();
            var CommonDBAccessor = new Common.BusinessCommon.DBAccessor();
            SqlDecimal tanka = 0;
            kingaku = SqlDecimal.Null;
            // 受付明細から取得
            table = this.TeikihaishaDao.GetUketsukeSsDetailForEntity(findDTO);
            if (table.Rows.Count != 0)
            {
                // ヒットしたレコードを返却
                if (false == string.IsNullOrEmpty(table.Rows[0]["TANKA"].ToString()))
                {
                    tanka = decimal.Parse(table.Rows[0]["TANKA"].ToString());
                }
                else
                {
                    tanka = SqlDecimal.Null;
                    if (false == string.IsNullOrEmpty(table.Rows[0]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        if (false == string.IsNullOrEmpty(table.Rows[0]["HINMEI_KINGAKU"].ToString()))
                        {
                            kingaku = decimal.Parse(table.Rows[0]["HINMEI_KINGAKU"].ToString());
                        }
                    }
                    else
                    {
                        if (false == string.IsNullOrEmpty(table.Rows[0]["KINGAKU"].ToString()))
                        {
                            kingaku = decimal.Parse(table.Rows[0]["KINGAKU"].ToString());
                        }
                    }
                   
                }
            }
            else
            {
                // 受付番号に該当する収集受付伝票取得
                var dto = new UketsukeSsDTOClass();
                dto.UKETSUKE_NUMBER = findDTO.UKETSUKE_NUMBER;
                table = this.TeikihaishaDao.GetUketsukeSsEntryForEntity(dto);
                // ※SYSTEM_ID, SEQの最大値の伝票を取得するため、データは唯一となる

                if (table.Rows.Count != 0)
                {
                    // DataTableからEntityを生成
                    T_UKETSUKE_SS_ENTRY[] entryResult = EntityUtility.DataTableToEntity<T_UKETSUKE_SS_ENTRY>(table);

                    // 個別品名単価から取得
                    var kobetsuEntity = CommonDBAccessor.GetKobetsuhinmeiTanka(kbnCD,
                                                                        entryResult[0].TORIHIKISAKI_CD,
                                                                        entryResult[0].GYOUSHA_CD,
                                                                        entryResult[0].GENBA_CD,
                                                                        entryResult[0].UNPAN_GYOUSHA_CD,
                                                                        entryResult[0].NIOROSHI_GYOUSHA_CD,
                                                                        entryResult[0].NIOROSHI_GENBA_CD,
                                                                        findDTO.HINMEI_CD,
                                                                        findDTO.UNIT_CD);
                    if (kobetsuEntity != null)
                    {
                        // 単価をセット
                        if (false == string.IsNullOrEmpty(kobetsuEntity.TANKA.ToString()))
                        {
                            tanka = decimal.Parse(kobetsuEntity.TANKA.ToString());
                        }
                    }
                    else
                    {
                        // 基本品名単価から取得
                        var kihonEntity = CommonDBAccessor.GetKihonHinmeitanka(kbnCD,
                                                                        entryResult[0].UNPAN_GYOUSHA_CD,
                                                                        entryResult[0].NIOROSHI_GYOUSHA_CD,
                                                                        entryResult[0].NIOROSHI_GENBA_CD,
                                                                        findDTO.HINMEI_CD,
                                                                        findDTO.UNIT_CD);
                        if (kihonEntity != null)
                        {
                            // 単価をセット
                            if (false == string.IsNullOrEmpty(kihonEntity.TANKA.ToString()))
                            {
                                tanka = decimal.Parse(kihonEntity.TANKA.ToString());
                            }
                        }
                        else
                        {
                            // 登録情報がない場合は0
                            tanka = 0;
                        }
                    }
                }
                else
                {
                    // 登録情報がない場合は0
                    tanka = 0;
                }
            }

            return tanka;
        }

        // 20170612 wangjm モバイル将軍#105481 start
        /// <summary>
        /// 単価取得処理
        /// </summary>
        /// <param name="kbnCD">伝票区分CD</param>
        /// <param name="findDTO">検索指定項目</param>
        /// <returns name="decimal">単価</returns>
        /// <remarks>検索項目に該当する単価を取得する。受付明細⇒個別品名単価⇒基本品名単価の順で取得する</remarks>
        internal SqlDecimal GetTanka(Int16 kbnCD, UketsukeSkDTOClass findDTO, out SqlDecimal kingaku)
        {
            var table = new DataTable();
            var CommonDBAccessor = new Common.BusinessCommon.DBAccessor();
            SqlDecimal tanka = 0;
            kingaku = SqlDecimal.Null;
            // 受付明細から取得
            table = this.TeikihaishaDao.GetUketsukeSkDetailForEntity(findDTO);
            if (table.Rows.Count != 0)
            {
                // ヒットしたレコードを返却
                if (false == string.IsNullOrEmpty(table.Rows[0]["TANKA"].ToString()))
                {
                    tanka = decimal.Parse(table.Rows[0]["TANKA"].ToString());
                }
                else
                {
                    tanka = SqlDecimal.Null;
                    if (false == string.IsNullOrEmpty(table.Rows[0]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        if (false == string.IsNullOrEmpty(table.Rows[0]["HINMEI_KINGAKU"].ToString()))
                        {
                            kingaku = decimal.Parse(table.Rows[0]["HINMEI_KINGAKU"].ToString());
                        }
                    }
                    else
                    {
                        if (false == string.IsNullOrEmpty(table.Rows[0]["KINGAKU"].ToString()))
                        {
                            kingaku = decimal.Parse(table.Rows[0]["KINGAKU"].ToString());
                        }
                    }

                }
            }
            else
            {
                // 受付番号に該当する収集受付伝票取得
                var dto = new UketsukeSkDTOClass();
                dto.UKETSUKE_NUMBER = findDTO.UKETSUKE_NUMBER;
                table = this.TeikihaishaDao.GetUketsukeSkEntryForEntity(dto);
                // ※SYSTEM_ID, SEQの最大値の伝票を取得するため、データは唯一となる

                if (table.Rows.Count != 0)
                {
                    // DataTableからEntityを生成
                    T_UKETSUKE_SK_ENTRY[] entryResult = EntityUtility.DataTableToEntity<T_UKETSUKE_SK_ENTRY>(table);

                    // 個別品名単価から取得
                    var kobetsuEntity = CommonDBAccessor.GetKobetsuhinmeiTanka(kbnCD,
                                                                        entryResult[0].TORIHIKISAKI_CD,
                                                                        entryResult[0].GYOUSHA_CD,
                                                                        entryResult[0].GENBA_CD,
                                                                        entryResult[0].UNPAN_GYOUSHA_CD,
                                                                        "",
                                                                        "",
                                                                        findDTO.HINMEI_CD,
                                                                        findDTO.UNIT_CD);
                    if (kobetsuEntity != null)
                    {
                        // 単価をセット
                        if (false == string.IsNullOrEmpty(kobetsuEntity.TANKA.ToString()))
                        {
                            tanka = decimal.Parse(kobetsuEntity.TANKA.ToString());
                        }
                    }
                    else
                    {
                        // 基本品名単価から取得
                        var kihonEntity = CommonDBAccessor.GetKihonHinmeitanka(kbnCD,
                                                                        entryResult[0].UNPAN_GYOUSHA_CD,
                                                                        "",
                                                                        "",
                                                                        findDTO.HINMEI_CD,
                                                                        findDTO.UNIT_CD);
                        if (kihonEntity != null)
                        {
                            // 単価をセット
                            if (false == string.IsNullOrEmpty(kihonEntity.TANKA.ToString()))
                            {
                                tanka = decimal.Parse(kihonEntity.TANKA.ToString());
                            }
                        }
                        else
                        {
                            // 登録情報がない場合は0
                            tanka = 0;
                        }
                    }
                }
                else
                {
                    // 登録情報がない場合は0
                    tanka = 0;
                }
            }

            return tanka;
        }
        // 20170612 wangjm モバイル将軍#105481 end

        /// <summary>
        /// 明細金額再計算
        /// </summary>
        /// <param name="entryEntity">Entry伝票</param>
        /// <param name="detailEntity">Detail伝票</param>
        /// <param name="taxHasuCD">消費税端数CD</param>
        /// <param name="kinHasuCD">金額端数CD</param>
        /// <remarks>
        /// 明細金額の再計算を行い
        /// 明細金額、消費税を格納する
        /// </remarks>
        private void DetailCalc(T_UR_SH_ENTRY entryEntity, T_UR_SH_DETAIL detailEntity, Int16 taxHasuCD, Int16 kinHasuCD, SqlDecimal detailKingaku)
        {
            // 一旦初期化
            detailEntity.HINMEI_KINGAKU = 0;
            detailEntity.HINMEI_TAX_SOTO = 0;
            detailEntity.HINMEI_TAX_UCHI = 0;
            detailEntity.KINGAKU = 0;
            detailEntity.TAX_SOTO = 0;
            detailEntity.TAX_UCHI = 0;

            // 明細金額計算
            decimal kingaku = 0;
            if ((false == detailEntity.SUURYOU.IsNull) && (false == detailEntity.TANKA.IsNull))
            {
                // 数量x単価
                decimal tempSuuryou = 0;
                decimal.TryParse(detailEntity.SUURYOU.ToString(), out tempSuuryou);
                kingaku = tempSuuryou * (decimal)detailEntity.TANKA;
            }
            else
            {
                // 数量もしくは単価が未格納の場合は0
                kingaku = 0;
            }

            if (true == detailEntity.TANKA.IsNull)
            {
                if (false == detailKingaku.IsNull)
                {
                    kingaku = decimal.Parse(detailKingaku.ToString());
                }
                else
                {
                    kingaku = 0;
                    detailKingaku = 0;
                }
            }

            // 税区分取得
            Int16 zeiKbn = 0;
            if (false == detailEntity.HINMEI_ZEI_KBN_CD.IsNull)
            {
                // 品名税区分が登録されている場合
                zeiKbn = (Int16)detailEntity.HINMEI_ZEI_KBN_CD;
            }
            else
            {
                // 伝票区分によった税区分の格納
                if (false == detailEntity.DENPYOU_KBN_CD.IsNull)
                {
                    if (CommonConst.DENPYOU_KBN_SHIHARAI == detailEntity.DENPYOU_KBN_CD)
                    {
                        // 支払税区分
                        if (false == entryEntity.SHIHARAI_ZEI_KBN_CD.IsNull)
                        {
                            zeiKbn = (Int16)entryEntity.SHIHARAI_ZEI_KBN_CD;
                        }
                    }
                    else
                    {
                        // 売上税区分
                        if (false == entryEntity.URIAGE_ZEI_KBN_CD.IsNull)
                        {
                            zeiKbn = (Int16)entryEntity.URIAGE_ZEI_KBN_CD;
                        }
                    }
                }
            }

            // 一旦初期化
            entryEntity.URIAGE_SHOUHIZEI_RATE = 0;
            entryEntity.SHIHARAI_SHOUHIZEI_RATE = 0;

            // 消費税計算
            decimal sotoZei = 0;
            decimal uchiZei = 0;
            if (zeiKbn != 0)
            {
                // 消費税率の取得
                var rate = this.accessor.GetShouhizeiRate((DateTime)entryEntity.DENPYOU_DATE);
                entryEntity.URIAGE_SHOUHIZEI_RATE = rate;
                entryEntity.SHIHARAI_SHOUHIZEI_RATE = rate;

                // 税区分によった消費税の格納
                var zei = this.TaxCalc(zeiKbn, rate, kingaku);
                if (CommonConst.ZEI_KBN_SOTO == zeiKbn)
                {
                    // 外税
                    sotoZei = zei;
                }
                else if (CommonConst.ZEI_KBN_UCHI == zeiKbn)
                {
                    // 内税
                    uchiZei = zei;
                }
                else
                {
                    // それ以外(非課税等)は0
                    sotoZei = 0;
                    uchiZei = 0;
                }
            }

            // 消費税端数処理
            if (taxHasuCD != 0)
            {
                sotoZei = CommonCalc.FractionCalc(sotoZei, taxHasuCD);
                uchiZei = CommonCalc.FractionCalc(uchiZei, taxHasuCD);
            }

            // 金額端数処理
            if (kinHasuCD != 0)
            {
                kingaku = CommonCalc.FractionCalc(kingaku, kinHasuCD);
            }

            // 金額、消費税のセット
            if (false == detailEntity.HINMEI_ZEI_KBN_CD.IsNull)
            {
                // 品名税区分CDが登録されていた場合
                if (false == detailEntity.TANKA.IsNull)
                {
                    detailEntity.HINMEI_KINGAKU = kingaku;
                }
                else
                {
                    detailEntity.HINMEI_KINGAKU = detailKingaku;
                }
                detailEntity.HINMEI_TAX_SOTO = sotoZei;
                detailEntity.HINMEI_TAX_UCHI = uchiZei;
            }
            else
            {
                // 品名税区分CDが登録されていなかった場合
                if (false == detailEntity.TANKA.IsNull)
                {
                    detailEntity.KINGAKU = kingaku;
                }
                else
                {
                    detailEntity.KINGAKU = detailKingaku;
                }
                detailEntity.TAX_SOTO = sotoZei;
                detailEntity.TAX_UCHI = uchiZei;
            }
        }

        /// <summary>
        /// 消費税計算
        /// </summary>
        /// <param name="zeiKbn">税区分</param>
        /// <param name="rate">消費税率</param>
        /// <param name="kingaku">算出対象金額</param>
        /// <remarks>
        /// 税区分に従い、消費税の計算を行う
        /// </remarks>
        private decimal TaxCalc(Int16 zeiKbn, decimal rate, decimal kingaku)
        {
            decimal zei = 0;

            if (zeiKbn != 0)
            {
                // 税区分によった消費税の格納
                if (CommonConst.ZEI_KBN_SOTO == zeiKbn)
                {
                    // 外税
                    zei = kingaku * rate;
                }
                else if (CommonConst.ZEI_KBN_UCHI == zeiKbn)
                {
                    // 内税
                    zei = kingaku - (kingaku / (rate + 1));
                }
                else
                {
                    // それ以外(非課税等)は0
                    zei = 0;
                }
            }

            return zei;
        }

        /// <summary>
        /// 金額・消費税総計取得
        /// </summary>
        /// <param name="detailList">算出対象明細のリスト</param>
        /// <param name="entryEntity">格納先のEntry伝票</param>
        /// <remarks>
        /// 明細のListより、金額・消費税の総計を算出し、
        /// Entry伝票に格納する
        /// </remarks>
        private void GetMoneyTotal(T_UR_SH_ENTRY entryEntity, List<T_UR_SH_DETAIL> detailList)
        {
            // 一旦初期化
            entryEntity.URIAGE_AMOUNT_TOTAL = 0;
            entryEntity.URIAGE_TAX_SOTO_TOTAL = 0;
            entryEntity.URIAGE_TAX_UCHI_TOTAL = 0;
            entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = 0;
            entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
            entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
            entryEntity.SHIHARAI_AMOUNT_TOTAL = 0;
            entryEntity.SHIHARAI_TAX_SOTO_TOTAL = 0;
            entryEntity.SHIHARAI_TAX_UCHI_TOTAL = 0;
            entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = 0;
            entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = 0;
            entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = 0;

            foreach (T_UR_SH_DETAIL detail in detailList)
            {
                if (CommonConst.DENPYOU_KBN_SHIHARAI == detail.DENPYOU_KBN_CD)
                {
                    // 支払金額を積算
                    entryEntity.SHIHARAI_AMOUNT_TOTAL += detail.KINGAKU;
                    entryEntity.SHIHARAI_TAX_SOTO_TOTAL += detail.TAX_SOTO;
                    entryEntity.SHIHARAI_TAX_UCHI_TOTAL += detail.TAX_UCHI;
                    entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL += detail.HINMEI_KINGAKU;
                    entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL += detail.HINMEI_TAX_SOTO;
                    entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL += detail.HINMEI_TAX_UCHI;
                }
                else
                {
                    // 売上金額を積算
                    entryEntity.URIAGE_AMOUNT_TOTAL += detail.KINGAKU;
                    entryEntity.URIAGE_TAX_SOTO_TOTAL += detail.TAX_SOTO;
                    entryEntity.URIAGE_TAX_UCHI_TOTAL += detail.TAX_UCHI;
                    entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL += detail.HINMEI_KINGAKU;
                    entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL += detail.HINMEI_TAX_SOTO;
                    entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL += detail.HINMEI_TAX_UCHI;
                }
            }
        }

        /// <summary>
        /// Entry伝票消費税計算
        /// </summary>
        /// <param name="entryEntity">Entry伝票</param>
        /// <param name="taxHasuCD">消費税端数CD</param>
        /// <param name="strProcType">処理種別("売上"or"支払")</param>
        /// <remarks>
        /// Entry伝票の消費税計算を行う
        /// </remarks>
        private void EntryTaxCalc(T_UR_SH_ENTRY entryEntity, Int16 taxHasuCD, string strProcType)
        {
            Int16 zeiKbn = 0;
            decimal rate = 0;
            decimal kingaku = 0;

            if (true == strProcType.Equals("支払"))
            {
                // 一旦初期化
                entryEntity.SHIHARAI_TAX_SOTO = 0;
                entryEntity.SHIHARAI_TAX_UCHI = 0;

                // 支払消費税計算
                if (false == entryEntity.SHIHARAI_ZEI_KBN_CD.IsNull)
                {
                    zeiKbn = (Int16)entryEntity.SHIHARAI_ZEI_KBN_CD;
                }
                rate = (decimal)entryEntity.SHIHARAI_SHOUHIZEI_RATE;
                kingaku = (decimal)entryEntity.SHIHARAI_AMOUNT_TOTAL;
            }
            else
            {
                // 一旦初期化
                entryEntity.URIAGE_TAX_SOTO = 0;
                entryEntity.URIAGE_TAX_UCHI = 0;

                // 売上消費税計算
                if (false == entryEntity.URIAGE_ZEI_KBN_CD.IsNull)
                {
                    zeiKbn = (Int16)entryEntity.URIAGE_ZEI_KBN_CD;
                }
                rate = (decimal)entryEntity.URIAGE_SHOUHIZEI_RATE;
                kingaku = (decimal)entryEntity.URIAGE_AMOUNT_TOTAL;
            }

            // 消費税計算
            decimal sotoZei = 0;
            decimal uchiZei = 0;
            var zei = this.TaxCalc(zeiKbn, rate, kingaku);
            if (zeiKbn != 0)
            {
                // 税区分によった消費税の格納
                if (CommonConst.ZEI_KBN_SOTO == zeiKbn)
                {
                    // 外税
                    sotoZei = zei;
                }
                else if (CommonConst.ZEI_KBN_UCHI == zeiKbn)
                {
                    // 内税
                    uchiZei = zei;
                }
                else
                {
                    // それ以外(非課税等)は0
                    sotoZei = 0;
                    uchiZei = 0;
                }
            }

            // 消費税端数処理
            if (taxHasuCD != 0)
            {
                sotoZei = CommonCalc.FractionCalc(sotoZei, taxHasuCD);
                uchiZei = CommonCalc.FractionCalc(uchiZei, taxHasuCD);
            }

            // 消費税のセット
            if (true == strProcType.Equals("支払"))
            {
                // 支払
                entryEntity.SHIHARAI_TAX_SOTO = sotoZei;
                entryEntity.SHIHARAI_TAX_UCHI = uchiZei;
            }
            else
            {
                // 売上
                entryEntity.URIAGE_TAX_SOTO = sotoZei;
                entryEntity.URIAGE_TAX_UCHI = uchiZei;
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

        #region process1 稼働状況表示処理
        /// <summary>
        /// process1 稼働状況表示
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_process1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                
                // 稼働状況テーブルを検索する。
                var kadoujyoukyouList = this.kadoujyoukyouDao.GetDataForSystemDate();
                // データが存在する場合にはmapboxにより地図表示を行う。
                if (kadoujyoukyouList.Length != 0)
                {
                    DialogResult dialogRet = this.MsgBox.MessageBoxShowConfirm("モバイル将軍利用者の稼働状況(位置情報)を表示します。\n"
                                                                                + "よろしいですか？\n"
                                                                                + "\n"
                                                                                + "※本日分のみの表示となります。\n"
                                                                                + "※緯度経度を元にした住所表記の為、実際の住所と異なる場合があります。",
                                                                                MessageBoxDefaultButton.Button1);
                    if (dialogRet == System.Windows.Forms.DialogResult.Yes)
                    {
                        // 地図表示処理
                        this.openMap(kadoujyoukyouList);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    this.MsgBox.MessageBoxShowError("表示するデータがありません。");
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
        /// 地図表示処理
        /// </summary>
        private void openMap(T_MOBISYO_RT_KADOUJYOUKYOU[] datas)
        {
            // 出力処理
            MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

            // 出力前にファイルを削除
            gljsLogic.htmlFileDelete();

            // S_MAPBOX_ACCESS_TOKENに登録
            gljsLogic.accessCountRegist(WINDOW_ID.M_GENBA_ICHIRAN);

            // 地図に渡すDTO作成
            List<ArrayListKadoujyoukyou> dto = this.createMapboxDto(datas);
            if (dto == null)
            {
                return;
            }

            // 地図表示
            gljsLogic.createHTML2Kadoujyoukyou(dto, WINDOW_ID.T_MOBILE_JOUKYOU_ICHIRAN);
        }

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        private List<ArrayListKadoujyoukyou> createMapboxDto(T_MOBISYO_RT_KADOUJYOUKYOU[] datas)
        {
            try
            {
                List<ArrayListKadoujyoukyou> dto = new List<ArrayListKadoujyoukyou>();
                int i = 0;

                foreach(T_MOBISYO_RT_KADOUJYOUKYOU data in datas)
                {
                    ArrayListKadoujyoukyou parentDto = new ArrayListKadoujyoukyou();

                    // 親のリストを生成
                    parentDto.type = "Feature";

                    Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.KadoujyoukyouProperties property = new Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.KadoujyoukyouProperties();
                    
                    // 運転者
                    M_SHAIN untensha = this.GetShain(data.UNTENSHA_CD);
                    if (untensha != null)
                    {
                        property.untenshaCd = untensha.SHAIN_CD;
                        property.untenshaName = untensha.SHAIN_NAME_RYAKU;
                    }
                    else
                    {
                        property.untenshaCd = string.Empty;
                        property.untenshaName = string.Empty;
                    }
                    
                    // 時間
                    string updateTime = data.ICHI_UPDATE_DATE.ToString();
                    string time = updateTime.Substring(10, updateTime.Length - 10);
                    string changeTime = "";
                    if (time.Length == 8)
                    {
                        changeTime = time.Substring(0, 2) + "時" + time.Substring(3, 2) + "分";
                    }
                    else if (time.Length == 9)
                    {
                        changeTime = time.Substring(0, 3) + "時" + time.Substring(4, 2) + "分";
                    }
                    property.updateTime = changeTime;
                    // 住所
                    string ichiParam = data.LONGITUDE + ", " + data.LATITUDE;
                    string URL = string.Format(MapboxConst.geocoding_uri_address, ichiParam, MapboxConfInfo.token);
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
                    req.Method = Enum.GetName(typeof(HTTP_METHOD), HTTP_METHOD.GET);
                    // レスポンスを取得
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    GeoCodingAPI result = null;
                    using (res)
                    {
                        using (var resStream = res.GetResponseStream())
                        {
                            var serializer = new DataContractJsonSerializer(typeof(GeoCodingAPI));
                            result = (GeoCodingAPI)serializer.ReadObject(resStream);
                        }
                    }
                    if (result != null)
                    {
                        string repAddress = result.features[0].place_name;
                        if (repAddress.Contains("日本,"))
                        {
                            repAddress = repAddress.Replace("日本,", "");
                        }
                        property.address = repAddress;
                    }
                    else
                    {
                        property.address = string.Empty;
                    }
                    
                    // 状況
                    int cntMi = 0;
                    int cntZumi = 0;
                    int cntJyo = 0;
                    String beforeCourseNameCd = "";

                    // 未回収の回収情報を取得する。
                    DataTable kaisyuuInfo = this.kadoujyoukyouDao.GetKaisyuuInfo(
                        data.UNTENSHA_CD, data.SHARYOU_CD, data.GYOUSHA_CD, data.ICHI_UPDATE_DATE.ToString());
                    foreach (DataRow row in kaisyuuInfo.Rows)
                    {
                        if (row["genbaSTTS"].ToString().Equals("0") && row["jyogaiFlg"].ToString().Equals("False"))
                        {
                            cntMi++;
                        }
                    }
                    // 回収済みと除外の回収情報を取得する。
                    foreach (DataRow row in kaisyuuInfo.Rows)
                    {
                        String zumiJyoken = "1";
                        String jogaiJyoken = "2";
                        if (string.IsNullOrEmpty(row["courseNameCd"].ToString()))
                        {
                            DataTable kaisyuuZumi = this.kadoujyoukyouDao.GetKaisyuuInfoForGenbaCnt(
                                data.UNTENSHA_CD, data.SHARYOU_CD, "", "", zumiJyoken, data.ICHI_UPDATE_DATE.ToString());
                            cntZumi += kaisyuuZumi.Rows.Count;
                            DataTable kaisyuuJyogai = this.kadoujyoukyouDao.GetKaisyuuInfoForGenbaCnt(
                                data.UNTENSHA_CD, data.SHARYOU_CD, "", "", jogaiJyoken, data.ICHI_UPDATE_DATE.ToString());
                            cntJyo += kaisyuuJyogai.Rows.Count;
                            break;
                        }
                        else
                        {
                            if (beforeCourseNameCd.Equals(row["courseNameCd"].ToString()))
                            {
                                continue;
                            }
                            DataTable kaisyuuZumi = this.kadoujyoukyouDao.GetKaisyuuInfoForGenbaCnt(
                                data.UNTENSHA_CD, data.SHARYOU_CD, row["courseNameCd"].ToString(), "", zumiJyoken, data.ICHI_UPDATE_DATE.ToString());
                            cntZumi += kaisyuuZumi.Rows.Count;
                            DataTable kaisyuuJyogai = this.kadoujyoukyouDao.GetKaisyuuInfoForGenbaCnt(
                                data.UNTENSHA_CD, data.SHARYOU_CD, row["courseNameCd"].ToString(), "", jogaiJyoken, data.ICHI_UPDATE_DATE.ToString());
                            cntJyo += kaisyuuJyogai.Rows.Count;

                            beforeCourseNameCd = row["courseNameCd"].ToString();
                        }
                    }

                    property.jyoukyouMi = cntMi.ToString();
                    property.jyoukyouZumi = cntZumi.ToString();
                    property.jyoukyouJyo = cntJyo.ToString();
                    property.jyoukyouTotal = (cntMi + cntZumi + cntJyo).ToString();

                    // 車輌
                    if (data.SHARYOU_CD != "" && data.SHARYOU_CD != null)
                    {
                        M_SHARYOU sharyou = new M_SHARYOU();
                        sharyou.SHARYOU_CD = data.SHARYOU_CD;
                        M_SHARYOU[] sharyouDatas = this.sharyouDao.GetAllValidData(sharyou);
                        if (sharyouDatas.Length != 0)
                        {
                            property.sharyouName = sharyouDatas[0].SHARYOU_NAME_RYAKU;
                        }
                    }
                    else
                    {
                        property.sharyouName = string.Empty;
                    }

                    // 車種
                    if (data.SHASHU_CD != "" && data.SHASHU_CD != null)
                    {
                        M_SHASHU shashu = new M_SHASHU();
                        shashu.SHASHU_CD = data.SHASHU_CD;
                        M_SHASHU[] shashuDatas = this.shashuDao.GetAllValidData(shashu);
                        if (shashuDatas.Length != 0)
                        {
                            property.shashuName = shashuDatas[0].SHASHU_NAME_RYAKU;
                        }
                    }
                    else
                    {
                        if (data.SHARYOU_CD != "" && data.SHARYOU_CD != null)
                        {
                            // 車種CDが取得できない場合、車輌CDと業者CDから取得する。
                            M_SHARYOU sharyou = new M_SHARYOU();
                            sharyou.SHARYOU_CD = data.SHARYOU_CD;
                            sharyou.GYOUSHA_CD = data.GYOUSHA_CD;
                            M_SHARYOU sharyouData = this.sharyouDao.GetDataByCd(sharyou);
                            if (sharyouData != null)
                            {
                                M_SHASHU shashu = new M_SHASHU();
                                shashu.SHASHU_CD = sharyouData.SHASYU_CD;
                                M_SHASHU[] shashuDatas = this.shashuDao.GetAllValidData(shashu);
                                if (shashuDatas.Length != 0)
                                {
                                    property.shashuName = shashuDatas[0].SHASHU_NAME_RYAKU;
                                }
                            }
                        }
                        else
                        {
                            property.shashuName = string.Empty;
                        }
                    }

                    property.description = this.createDiscription(property);
                    property.iata_code = i++;

                    Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.Geometry geometry = new Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.Geometry();
                    geometry.type = "Point";
                    geometry.zoom = "4";
                    geometry.Latitude = data.LATITUDE;
                    geometry.Longitude = data.LONGITUDE;
                    List<double> coordinatesList = new List<double>();
                    // 順番に注意　先にLONGITUDE(経度)をセットしないとマーカーの位置がおかしくなる
                    coordinatesList.Add(Convert.ToDouble(data.LONGITUDE));
                    coordinatesList.Add(Convert.ToDouble(data.LATITUDE));
                    geometry.coordinates = coordinatesList;

                    parentDto.properties = property;
                    parentDto.geometry = geometry;
                    dto.Add(parentDto);
                }

                return dto;

            }
            catch (Exception ex)
            {
                this.MsgBox.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Discriptionに埋め込む内容を作成
        /// </summary>
        /// <returns></returns>

        private string createDiscription(KadoujyoukyouProperties properties)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<p>");
            sb.AppendFormat("{0} 【 {1} 】", properties.untenshaName, properties.updateTime);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("場所：{0} 付近", properties.address);
            sb.AppendFormat("<br>状況：未 {0} 済 {1} 除 {2} (計 {3})", properties.jyoukyouMi, properties.jyoukyouZumi, properties.jyoukyouJyo, properties.jyoukyouTotal);

            if (properties.shashuName == null || properties.shashuName.Equals(""))
            {
                sb.AppendFormat("<br>車輌：{0}", properties.sharyouName, properties.shashuName);
            }
            else
            {
                sb.AppendFormat("<br>車輌：{0} ( {1} )", properties.sharyouName, properties.shashuName);
            }

            sb.Append("</p>");

            return sb.ToString();
        }

        #endregion


        #region 明細変化処理
        /// <summary>
        /// 明細変化処理
        /// </summary>
        internal void DetailChangeSyori()
        {
            // 明細クリア
            this.form.Ichiran.Rows.Clear();
            // 全選択クリア
            this.form.checkBoxAll.Checked = false;
            this.form.checkBoxAll2.Checked = false;

            if (this.form.RENKEI_KBN_1.Checked)
            {
                if (this.form.HAISHA_KBN_1.Checked)
                {
                    // 読込系配車明細を表示する
                    this.form.Ichiran.Template = this.form.tourokuSumiHaishaDetail1;
                }
                else
                {
                    // 読込系受付明細を表示する
                    this.form.Ichiran.Template = this.form.tourokuSumiUketukeDetail1;
                }
            }
            else if (this.form.RENKEI_KBN_2.Checked)
            {
                if (this.form.HAISHA_KBN_1.Checked)
                {
                    // 読込系配車明細(詳細表示)を表示する
                    this.form.Ichiran.Template = this.form.tourokuSumiHaishaDetail1Hyouji;
                }
                else
                {
                    // 読込系受付明細(詳細表示)を表示する
                    this.form.Ichiran.Template = this.form.tourokuSumiUketukeDetail1Hyouji;
                }
            }
            else
            {
                if (this.form.HAISHA_KBN_1.Checked)
                {
                    // 登録系配車明細を表示する
                    this.form.Ichiran.Template = this.form.miTourokuHaishaDetail1;
                }
                else
                {
                    // 登録系受付明細を表示する
                    this.form.Ichiran.Template = this.form.miTourokuUketukeDetail1;
                }
            }
            // 初期表示時に自動調整をし、その後は手での明細幅変更をできるようにする。
            this.form.Ichiran.HorizontalAutoSizeMode = ((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode)((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.CellsInColumnHeader | GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.DisplayedCellsInRow)));
            this.form.Ichiran.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
        }

        #endregion

        #region 関連チェック
        /// <summary>
        /// 運搬業者CDバリデート
        /// </summary>
        internal void UNPAN_GYOUSHA_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.UNPAN_GYOUSHA_NAME.Text = "";
                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var unpanGyousha = this.gyoushaDao.GetAllValidData(keyEntity).FirstOrDefault();
                if (unpanGyousha == null || !unpanGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    // エラーメッセージ
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.form.UNPAN_GYOUSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    return;
                }

                // 名称セット
                this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CDValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 車輌CDEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void sharyouCdEnter(object sender, EventArgs e)
        {
            // 前回値を保持する
            var ctrl = (CustomAlphaNumTextBox)sender;
            this.oldSharyouCD = ctrl.Text;
        }

        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckSharyouCd()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 車輌名をクリア
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                // 車輌情報取得
                var sharyou = this.GetSharyou(this.form.SHARYOU_CD.Text);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (sharyou == null)
                {
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E020", "車輌");
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                // 車輌名設定
                this.form.SHARYOU_NAME_RYAKU.Text = sharyou.SHARYOU_NAME_RYAKU;

                // 車種入力されてない場合
                // 車種情報取得
                var shashu = this.GetSharshu(sharyou.SHASYU_CD);
                if (shashu != null)
                {
                    // 車種情報設定
                    this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                    this.form.SHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
                }

                // 運転者入力されてない場合
                if (string.IsNullOrEmpty(this.form.SHAIN_CD.Text))
                {
                    // 社員情報取得
                    var shain = this.GetShain(sharyou.SHAIN_CD);
                    if (shain != null)
                    {
                        // 運転者情報設定
                        this.form.SHAIN_CD.Text = shain.SHAIN_CD;
                        this.form.SHAIN_NAME.Text = shain.SHAIN_NAME_RYAKU;
                    }
                }

                // 運搬業者が入力されてない場合
                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // 業者情報取得
                    var gyousha = this.GetGyousha(sharyou.GYOUSHA_CD);
                    if (gyousha != null)
                    {
                        // 業者情報設定
                        this.form.UNPAN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 処理終了
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyouCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// 車輌情報取得
        /// </summary>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns></returns>
        internal M_SHARYOU GetSharyou(string sharyouCd)
        {
            M_SHARYOU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(sharyouCd);

                if (string.IsNullOrEmpty(sharyouCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHARYOU keyEntity = new M_SHARYOU();
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    keyEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                keyEntity.SHARYOU_CD = sharyouCd;
                //// 車種入力されている場合
                //if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                //{
                //    keyEntity.SHASYU_CD = this.form.SHASHU_CD.Text;
                //}
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                // [運搬業者CD,車輌CD,車種CD]でM_SHARYOUを検索する
                var returnEntitys = sharyouDao.GetAllValidDataForGyoushaKbn(keyEntity, "9", SqlDateTime.Null, true, true, false);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    if (returnEntitys.Length == 1)
                    {
                        // 1件
                        returnVal = returnEntitys[0];
                    }
                    else
                    {
                        // ヒット数が複数件の場合、ポップアップ表示
                        this.form.SHARYOU_CD.Focus();
                        this.form.isFukusuPop = true;
                        SendKeys.Send(" ");

                        // 返却値は空白をセット
                        returnVal = new M_SHARYOU();
                        returnVal.SHARYOU_NAME_RYAKU = "";
                        returnVal.SHASYU_CD = "";
                        returnVal.SHAIN_CD = "";
                        returnVal.GYOUSHA_CD = "";
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharyou", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 車種情報取得
        /// </summary>
        /// <param name="shashuCd">車種CD</param>
        /// <returns></returns>
        internal M_SHASHU GetSharshu(string shashuCd)
        {
            M_SHASHU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shashuCd);

                if (string.IsNullOrEmpty(shashuCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHASHU keyEntity = new M_SHASHU();
                keyEntity.SHASHU_CD = shashuCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;

                // [車種CD]でM_SHASHUを検索する
                var returnEntitys = this.shashuDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = returnEntitys[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharshu", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 社員情報取得
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <returns></returns>
        internal M_SHAIN GetShain(string shainCd)
        {
            M_SHAIN returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shainCd);

                if (string.IsNullOrEmpty(shainCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHAIN keyEntity = new M_SHAIN();
                keyEntity.SHAIN_CD = shainCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;

                // [社員CD,運転者フラグ=true]でM_SHAINを検索する
                var returnEntitys = this.shainDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = returnEntitys[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetShain", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            M_GYOUSHA returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

                if (gyousha != null && gyousha.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = gyousha[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 運転者CDバリデート
        /// </summary>
        internal void UNTENSHA_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.SHAIN_NAME.Text = "";
                M_SHAIN keyEntity = new M_SHAIN();
                keyEntity.UNTEN_KBN = true;
                keyEntity.SHAIN_CD = this.form.SHAIN_CD.Text;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var untenShain = this.shainDao.GetAllValidData(keyEntity).FirstOrDefault();
                if (untenShain == null)
                {
                    // エラーメッセージ
                    this.form.SHAIN_CD.IsInputErrorOccured = true;
                    this.form.SHAIN_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");
                    this.form.SHAIN_CD.Focus();
                    return;
                }

                this.form.SHAIN_NAME.Text = untenShain.SHAIN_NAME_RYAKU;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNTENSHA_CDValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CDが変わったかチェック
        /// </summary>
        /// <returns>
        /// true = 変更あり
        /// false = 変更なし
        /// </returns>
        internal bool CheckGyoushaChange()
        {
            bool ren = true;

            try
            {
                //業者CD
                string gyoushaCd = this.form.GYOUSHA_CD.Text.ToString().Trim();
                if (gyoushaCd != "")
                {
                    gyoushaCd = gyoushaCd.PadLeft(6, '0');
                }
                if (this.form.testGYOUSHA_CD.Text != gyoushaCd)
                {
                    //前回値に比較
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME.Text = string.Empty;
                    this.form.testGENBA_CD.Text = string.Empty;
                    this.form.testGYOUSHA_CD.Text = gyoushaCd;
                    ren = true;
                }
                if (gyoushaCd == "")
                {
                    this.form.GYOUSHA_NAME.Clear();
                    ren = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaChange", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ren = false;
            }
            return ren;
        }

        /// <summary>
        /// 業者情報取得
        /// </summary>
        /// <param name="gosyaCd">業者CD</param>
        internal M_GYOUSHA[] GetGyousyaInfo(string gosyaCd, out bool catchErr)
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
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousyaInfo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
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
        internal M_GENBA[] GetGenbaInfo(string genbaCd, string gosyaCd, out bool catchErr)
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
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbaInfo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 全選択チェックボックス状態を切り替えの描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void IchiranCellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.CellIndex == 0 && e.RowIndex == -1)
            {
                this.form.checkBoxAll.Focus();
                // 全選択チェックボックスが押下された場合、チェックボックス状態を反転する
                this.form.checkBoxAll.Checked = !this.form.checkBoxAll.Checked;
                // 再描画
                var parent = (BusinessBaseForm)this.form.Parent;
            }
            // 検索条件の連携が「2.登録済(詳細表示)」の場合
            // 定期の場合は定期配車番号、スポットの場合は受付番号が同じ明細は、チェックボックスの値を連動する
            else if (e.CellIndex == 0 && e.RowIndex > -1 && this.dto.RENKEI_KBN.Equals("2"))
            {
                bool check = !(bool)this.form.Ichiran.Rows[e.RowIndex].Cells[e.CellIndex].Value;
                // 同じ行品名選択
                foreach (var row in this.form.Ichiran.Rows)
                {
                    if (row["HAISHA_DENPYOU_NO"].Value.ToString() == this.form.Ichiran.Rows[e.RowIndex].Cells["HAISHA_DENPYOU_NO"].Value.ToString())
                    {
                        row[0].Value = check;
                    }
                }
            }

            //他社振替のチェック
            if (e.CellIndex == 1 && e.RowIndex == -1)
            {
                if (string.IsNullOrEmpty(this.dto.RENKEI_KBN))
                {
                    //検索実行が行われてない状態では処理を抜ける
                    return;
                }
                else if (this.dto.RENKEI_KBN.Equals("1"))
                {
                    this.form.checkBoxAll2.Focus();
                    // 全選択チェックボックスが押下された場合、チェックボックス状態を反転する
                    this.form.checkBoxAll2.Checked = !this.form.checkBoxAll2.Checked;
                    // 再描画
                    var parent = (BusinessBaseForm)this.form.Parent;
                }
            }
            else if (e.CellIndex == 1 && e.RowIndex > -1 && this.dto.RENKEI_KBN.Equals("1"))
            {
                  this.form.Ichiran.CurrentCellPosition = new CellPosition(e.RowIndex, 0);
                //チェック更新されないようにしたい。
                bool check = (bool)this.form.Ichiran.Rows[e.RowIndex].Cells[1].Value;
                if ((check) && (this.form.Ichiran.Rows[e.RowIndex].Cells["KAISHU_JOKYO"].Value.ToString() != "未回収"))
                {
                    this.form.Ichiran.Rows[e.RowIndex].Cells[1].Value = false;
                    this.form.Ichiran.CurrentCellPosition = new CellPosition(e.RowIndex, 1);
                    this.MsgBox.MessageBoxShowError("未回収分のみ、振替が可能です。");
                }
                else
                {
                    this.form.Ichiran.CurrentCellPosition = new CellPosition(e.RowIndex, 1);
                }
            }

            // 現場CDなしの行の対象をクリックした際の処理
            if (this.form.HAISHA_KBN.Text.Equals(ConstCls.HAISHA_KBN_2) && this.form.RENKEI_KBN.Text.Equals("3"))
            {
                if (e.CellIndex == 0 && e.RowIndex > -1)
                {
                    string genbaCd = this.form.Ichiran.Rows[e.RowIndex].Cells["GENBA_CD"].Value.ToString();

                    // 現場CDが空の場合、対象チェックボックスをOFFにする。
                    if (string.IsNullOrEmpty(genbaCd))
                    {
                        this.form.Ichiran.CurrentCellPosition = new CellPosition(e.RowIndex, 1);
                        bool check = (bool)this.form.Ichiran.Rows[e.RowIndex].Cells[0].Value;
                        if (check)
                        {
                            this.form.Ichiran.Rows[e.RowIndex].Cells[0].Value = false;
                            //this.form.Ichiran.CurrentCellPosition = new CellPosition(e.RowIndex, 0);
                            this.MsgBox.MessageBoxShowError("現場が未入力のデータの為、連携出来ません。\n\n確認してください。");
                        }
                        else
                        {
                            this.form.Ichiran.CurrentCellPosition = new CellPosition(e.RowIndex, 0);
                        }                        
                    }
                }
            }
        }


        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else
            {
                return obj;
            }
        }

        #endregion

        #region 自動生成（実装なし）
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
        /// 登録チェック（複数人で同じ伝票を処理しないようにチェック）
        /// </summary>
        /// <returns></returns>
        public bool Regist_Check()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                SqlInt64 HaishaNo;
                SqlInt64 HaishaRowNo;
                int intHAISHA_KBN;
                int CntN;

                if (this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_1) || this.dto.RENKEI_KBN.Equals(ConstCls.RENKEI_KBN_2))
                {
                    #region 登録済み
                    //1伝票1明細
                    foreach (var row in this.form.Ichiran.Rows)
                    {
                        CntN = 0;
                        if ((bool)row[0].Value)
                        {
                            HaishaNo = SqlInt64.Parse(row["HAISHA_DENPYOU_NO"].Value.ToString());
                            if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                            {
                                intHAISHA_KBN = 0;  //定期
                            }
                            else
                            {
                                intHAISHA_KBN = 1;  //スポット
                            }

                            //実績登録されてないデータがあるかチェック
                            var List = this.mTeikihaishaDao.GetRtDataByCDM(HaishaNo, intHAISHA_KBN);
                            foreach (T_MOBISYO_RT count in List)
                            {
                                CntN = 1;
                                break;
                            }

                            //該当伝票番号のT_MOBISYO_RTが全て確定済みの場合アラートを出す
                            if (CntN==0)
                            {
                                this.MsgBox.MessageBoxShowWarn("既に確定済みの伝票があります。再度検索をしてから確定を行ってください。");
                                ret = false;
                                break;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 未登録
                    //定期配車→1伝票複数明細
                    foreach (var row in this.form.Ichiran.Rows)
                    {
                        if ((bool)row[0].Value)
                        {
                            // 選択行データ ※ROW_NOは頭から連番。
                            DataRow tableRow = this.ResultTable.Rows[int.Parse(row["ROW_NO"].Value.ToString()) - 1];
                            if (this.dto.HAISHA_KBN.Equals(ConstCls.HAISHA_KBN_1))
                            {
                                intHAISHA_KBN = 0;  //定期
                                // 定期配車番号
                                HaishaNo = SqlInt64.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                                // 定期配車行番号
                                HaishaRowNo = SqlInt64.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                            }
                            else
                            {
                                intHAISHA_KBN = 1;  //スポット
                                //（配車）伝票番号
                                HaishaNo = SqlInt64.Parse(tableRow["UKETSUKE_NUMBER"].ToString());
                                HaishaRowNo = 1;
                            }

                            //選択した行が[ﾓﾊﾞｲﾙ登録]されているかチェック
                            var List = this.mTeikihaishaDao.GetRtDataByCDR(HaishaNo, HaishaRowNo, intHAISHA_KBN);
                            foreach (T_MOBISYO_RT count in List)
                            {
                                this.MsgBox.MessageBoxShowWarn("既に登録済みのものがあります。再度検索をしてから登録を行ってください。");
                                ret = false;
                                break;
                            }
                            if (!ret)
                            {
                                break;
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist_Check", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

    }
}